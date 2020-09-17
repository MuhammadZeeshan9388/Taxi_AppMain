using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_BLL;
using DAL;
using Utils;
using Taxi_Model;
using Telerik.WinControls.Enumerations;
using System.Data.Linq;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmEditFarePrice : UI.SetupBase
    {
        private int EditType;

        long Id = 0;
        public frmEditFarePrice(long FareId, string FromAddress,string ToAddress,decimal Price,int editfareType)
        {
            InitializeComponent();
            lblFromAddress.Text = FromAddress;
            lblToAddress.Text = ToAddress;
            spnFarePrice.Value = Price;
            Id = FareId;
            EditType = editfareType;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {
            try
            {
                decimal Rate = spnFarePrice.Value.ToDecimal();
                if (Rate == 0)
                {
                    ENUtils.ShowMessage("Required : Fare Rate");
                    return;
                }   
                using (TaxiDataContext db = new TaxiDataContext())
                {

                    if (EditType == 1)
                    {

                        db.stp_UpdateFare_ChargesDetails(Id, Rate);


                    }
                    else if (EditType == 2)
                    {
                       Fare_ZoneWisePricing objFare=   db.Fare_ZoneWisePricings.FirstOrDefault(c => c.Id == Id);

                       if (objFare != null)
                       {
                           objFare.Price = Rate.ToDecimal();
                           db.SubmitChanges();

                       }

                    }


                    General.RefreshListWithoutSelected<frmFaresList>("frmFaresList1");
                    this.Close();
                    
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
