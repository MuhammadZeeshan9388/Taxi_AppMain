using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Utils;
using Taxi_BLL;

namespace Taxi_AppMain
{
    public partial class frmWebBooking : Forms.BaseForm
    {
       

        DateTime? fromBookingDate;
        DateTime? tillBookingDate;
        bool HasWebBookingTabbed;


        public frmWebBooking(DateTime fromDate, DateTime tillDate,bool hasWebBookingTab)
        {
            this.fromBookingDate = fromDate;
            this.tillBookingDate = tillDate;
            HasWebBookingTabbed = hasWebBookingTab;
            InitializeConstructor();

        }


        private void InitializeConstructor()
        {

            this.Load += new EventHandler(frmWebBooking_Load);
          //  AppVars.frmDashBoard.timer_WebBooking.Stop();
        }

        void frmWebBooking_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = null;
            dtpTillDate.Value = null;
        }


        public frmWebBooking(bool hasWebBookingTab)
        {
            InitializeComponent();

            HasWebBookingTabbed = hasWebBookingTab;
            InitializeConstructor();
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
           
        }

        private void ShowOnlineBookingsPopup(List<Booking> listofJobs)
        {

            frmFetchedOnlineBookingsPopup frmOnline = new frmFetchedOnlineBookingsPopup(listofJobs);
            frmOnline.StartPosition = FormStartPosition.CenterScreen;
            frmOnline.ShowDialog();

            frmOnline.Dispose();
      

        }




        private void radButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWebBooking_FormClosed(object sender, FormClosedEventArgs e)
        {
           // AppVars.frmDashBoard.timer_WebBooking.Start();
            General.DisposeForm(this);
        }

        private void chkFetchAll_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            pnlCriteria.Enabled = args.NewValue == Telerik.WinControls.Enumerations.ToggleState.Off;

            if (!pnlCriteria.Enabled)
            {
                dtpFromDate.Value = null;
                dtpTillDate.Value = null;

            }
            else
            {

                dtpFromDate.Value = DateTime.Now;
                dtpTillDate.Value = DateTime.Now;

            }
        }
    }
}
