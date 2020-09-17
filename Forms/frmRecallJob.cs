using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_AppMain.Forms;
using Utils;
using Taxi_Model;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmRecallJob : BaseForm
    {
        public frmRecallJob()
        {
            InitializeComponent();
            InitializeSettings();
        }



        public frmRecallJob(int driverId)
        {
            InitializeComponent();
            InitializeSettings();


            ddl_Driver.SelectedValue = driverId;
        }

        private void InitializeSettings()
        {

            ComboFunctions.FillNonAvailPDADLoginDriverCombo(ddl_Driver);

            ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown += new KeyEventHandler(ddl_Driver_KeyDown);
            this.Shown += new EventHandler(frmRecallJob_Shown);
        }

        void frmRecallJob_Shown(object sender, EventArgs e)
        {
            
            SendKeys.Send("{TAB}");
        }


        void ddl_Driver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                ShowView();
            }
        }

        void ddl_Driver_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                ShowView();
            }
        }

       

        private void btnTrack_Click(object sender, EventArgs e)
        {

            ShowView();

        }


        private void ShowView()
        {
            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();
                string driverNo = ddl_Driver.Text.Trim();

                if (driverId != 0)
                {
                 long jobId=General.GetQueryable<Fleet_DriverQueueList>(c=>c.DriverId==driverId && c.Status==true).OrderByDescending(c=>c.Id).FirstOrDefault().DefaultIfEmpty().CurrentJobId.ToLong();

                 //if (jobId == 0)
                 //{

                 //    jobId= General.GetQueryable<Booking>(c => c.DriverId == driverId && c.AcceptedDateTime != null).OrderByDescending(c => c.AcceptedDateTime).FirstOrDefault().DefaultIfEmpty().Id;
                 //}
                    if(jobId!=0)
                    {

                        new Thread(delegate()
                        {
                            int loopCnt = 1;
                            while (loopCnt < 3)
                            {

                                bool success = General.ReCallBooking(jobId, driverId);

                                if (success)
                                {
                                    break;

                                }
                                else
                                    loopCnt++;



                            }
                        }).Start(); 

                        //new Thread(delegate()
                        //{
                        //    General.ReCallBooking(jobId, driverId);

                        //}).Start();

                        Thread.Sleep(500);

                       
                        (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshAllActiveData();
                        this.Close();

                         using (TaxiDataContext db = new TaxiDataContext())
                         {
                              db.stp_BookingLog(jobId, AppVars.LoginObj.UserName.ToStr(), "Job is Recovered from Driver (" + driverNo + ")");
                         }
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
    }
}
