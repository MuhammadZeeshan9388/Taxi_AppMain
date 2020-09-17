using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using UI;
using Telerik.WinControls.UI;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmAddNewAddress : UI.SetupBase
    {
        long EntityID = 0;
        string OldAddress = string.Empty;
        int? OldZoneId = null;

        public frmAddNewAddress()
        {
            InitializeComponent();
        }

        public frmAddNewAddress(long Id, string Address,int? zoneId)
        {
            InitializeComponent();
            EntityID = Id;
            txtAddress.Text = Address;
            OldAddress = Address;
            this.OldZoneId = zoneId;
      
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAddres();
        }
        private void SaveAddres()
        {
            try
            {

                
                string Address = string.Empty;
                string PostCode = string.Empty;
                string Error = string.Empty;

                bool IsInvalidChar = false;
                bool PostCodeSpace = false;

                IsInvalidChar = txtAddress.Text.Contains(",");
                if (IsInvalidChar == true)
                {
                    ENUtils.ShowMessage("Address contains ',' Invalid Character");
                    return;
                }
                IsInvalidChar = txtAddress.Text.Contains("=");
                if (IsInvalidChar == true)
                {
                    ENUtils.ShowMessage("Address contains '=' Invalid Character");
                    return;
                }
                IsInvalidChar = txtAddress.Text.Contains(":");
                if (IsInvalidChar == true)
                {
                    ENUtils.ShowMessage("Address contains ':' Invalid Character");
                    return;
                }


                Address = txtAddress.Text.Trim().ToUpper();
                PostCode = General.GetPostCodeMatch(txtAddress.Text.Trim().ToUpper());
                PostCodeSpace = PostCode.Contains(" ");

                if (string.IsNullOrEmpty(Address))
                {
                    Error = "Required : Address";
                }
                if (string.IsNullOrEmpty(PostCode))
                {
                    Error += Environment.NewLine + "Required : PostCode";
                }
                if (PostCodeSpace == false)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Please Enter Correct PostCode";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Please Enter Correct PostCode";
                    }

                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }




                int? zoneId = ddlZone.SelectedValue.ToIntorNull();

                long Entity = 0;


                if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        Address = Address.Replace(PostCode, "");
                        // db.stp_SaveRoadLevelAddress(EntityID,PostCode,Address,"");    
                    }
                }
                else
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        Entity = db.stp_SaveGeneralAddress(EntityID, Address, PostCode, zoneId.ToStr()).FirstOrDefault().Entity.ToLong();
                    }

                    AppVars.listOfAddress.Insert(0, new stp_GetFullAddressesResult { AddressLine1 = Address.ToUpper(), PostalCode = PostCode.ToUpper(), ZoneId = zoneId });

                    if (Entity == 0)
                    {
                        ENUtils.ShowMessage("Address already exist");
                        return;
                    }

                }

                this.Close();


                if (!string.IsNullOrEmpty(OldAddress) && OldAddress != Address)
                {
                    ((frmAddNewAddress)Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmAddNewAddress")).RefreshData();
                }


            }
            catch (OverflowException ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        
        private void btnExit1_Click(object sender, EventArgs e)
        {
            GC.Collect();
            this.Close();
        }

        private void frmAddNewAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                GC.Collect();
                this.Close();
            }
        }

        private void frmAddNewAddress_Load(object sender, EventArgs e)
        {
            ComboFunctions.FillZonesCombo(ddlZone);


            ddlZone.SelectedValue = this.OldZoneId;

        }
    }
}
