using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using System.Net;
using System.Xml.Linq;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmEscort : UI.SetupBase
    {
        Gen_EscortBO objMaster;
        public frmEscort()
        {
            InitializeComponent();
            InitializeConstructor();
            this.Text = "Escort";
          
        }


        
        private void InitializeConstructor()
        {

            objMaster = new Gen_EscortBO();
            this.SetProperties((INavigation)objMaster);



            //timer1.Tick += new EventHandler(timer1_Tick);

            txtAddress1.ListBoxElement.Width = 400;
            //txtAddress2.ListBoxElement.Width = 400;

            this.Shown += new EventHandler(frmCustomer_Shown);            
        }







        void frmCustomer_Shown(object sender, EventArgs e)
        {
            txtEscortName.Focus();
          
        }

     
      


        #region Overridden Methods


        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {
            txtEscortName.Text = "";
            txtEmail.Text = "";
            txtMobileNo.Text = "";
            txtTelephoneNo.Text = "";
            txtAddress1.Text = "";
        }

      

        public override void Save()
        {
            try
            {
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                    objMaster.Current.EditBy = AppVars.LoginObj.LuserId.ToInt();
                    objMaster.Current.EditLog = AppVars.LoginObj.UserName.ToStr();
                    objMaster.Current.EditOn = DateTime.Now.ToDateTime();

                }


             
                objMaster.Current.EscortName = txtEscortName.Text.Trim();
                objMaster.Current.EmailAddress = txtEmail.Text.Trim();
                objMaster.Current.TelephoneNo = txtTelephoneNo.Text.Trim();
                objMaster.Current.MobileNo = txtMobileNo.Text.Trim();
                objMaster.Current.AddressLine1 = txtAddress1.Text.Trim();
                objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();
                objMaster.Current.AddLog = AppVars.LoginObj.UserName.ToStr();
                objMaster.Current.AddOn = DateTime.Now.ToDateTime();
                objMaster.Save();
                General.RefreshListWithoutSelected<frmEscortList>("frmEscortList1");
                this.Close();
                //OnNew();

                //General.RefreshListWithoutSelected<frmCustomersList>("frmCustomersList1");
               // General.RefreshListWithoutSelected<frmBlackCustomersList>("frmBlackCustomersList1");


              
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }


        }

        public override void DisplayRecord()
        {
            try
            {
                if (objMaster.Current == null) return;
              

                txtEscortName.Text = objMaster.Current.EscortName.ToStr();
                txtEmail.Text = objMaster.Current.EmailAddress.ToStr();
                txtTelephoneNo.Text = objMaster.Current.TelephoneNo.ToStr();
                txtMobileNo.Text = objMaster.Current.MobileNo.ToStr();

                //txtAddress1.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtAddress1.Text = objMaster.Current.AddressLine1.ToStr();
                //txtAddress1.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                //txtAddress2.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                //txtAddress2.Text = objMaster.Current.Address2.ToStr();
                //txtAddress2.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                //txtDoorNo.Text = objMaster.Current.DoorNo.ToStr();

                //chkBlackList.Checked = objMaster.Current.BlackList.ToBool();

                //if (chkBlackList.Checked == false)
                //{
                //    txtResion.Text = "";
                //}
                //else
                //{
                //    txtResion.Text = objMaster.Current.BlackListResion.ToStr();
                //}


                //txtTotalCalls.Value = objMaster.Current.TotalCalls.ToDecimal();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

       

        #endregion

        //private void txtName_Validated(object sender, EventArgs e)
        //{
        //    txtEscortName.Text =txtEscortName.Text.ToStr().ToProperCase();
        //}


        private void frmCustomer_FormClosed(object sender, FormClosedEventArgs e)
        {
            General.DisposeForm(this);

            GC.Collect();
        }
    }
}
