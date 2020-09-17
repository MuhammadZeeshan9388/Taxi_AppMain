using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using DAL;
using Utils;
using Telerik.WinControls.UI;

namespace Taxi_AppMain
{
    public partial class frmSearchDriverRentPaymentHistory : Form
    {
        private string DriverNo;

        private List<DriverRent_PaymentHistory> listofPaymentHistory = null;


        public frmSearchDriverRentPaymentHistory(string drvNo, List<DriverRent_PaymentHistory> list)
        {
            InitializeComponent();

            this.listofPaymentHistory = list;
            this.DriverNo = drvNo;
            this.Load += new EventHandler(frmSearchDriverRentPaymentHistory_Load);
        }

        void frmSearchDriverRentPaymentHistory_Load(object sender, EventArgs e)
        {

            grdPaymentHistory.GroupDescriptors.Expression = "Week";
            grdPaymentHistory.AutoExpandGroups = true;


           ShowPaymentHistory();


        }

       private void ShowPaymentHistory()
        {

            try
            {

                if (listofPaymentHistory == null)
                    return;

                grdPaymentHistory.Rows.Clear();

              

                grdPaymentHistory.RowCount = listofPaymentHistory.Count;


                for(int i=0; i<grdPaymentHistory.RowCount; i++)
                {

                    grdPaymentHistory.Rows[i].Cells["Week"].Value = string.Format(" {0:dd/MMM/yyyy} to {1:dd/MMM/yyyy}", listofPaymentHistory[i].DriverRent.FromDate.ToDateorNull(), listofPaymentHistory[i].DriverRent.ToDate.ToDateorNull());

                    grdPaymentHistory.Rows[i].Cells["PaymentDate"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", listofPaymentHistory[i].PaymentDate.ToDateTime());

                    grdPaymentHistory.Rows[i].Cells["Balance"].Value = listofPaymentHistory[i].OldBalance.ToDecimal();
                    grdPaymentHistory.Rows[i].Cells["RentPay"].Value = listofPaymentHistory[i].RentPay.ToDecimal();
                    grdPaymentHistory.Rows[i].Cells["BalanceDue"].Value = listofPaymentHistory[i].BalanceDue.ToDecimal();
                    grdPaymentHistory.Rows[i].Cells["Reason"].Value = listofPaymentHistory[i].DriverRentPayReason.DefaultIfEmpty().RentReason.ToStr();


                }
            }
            catch(Exception ex)
            {

            }

        }
    }
}
