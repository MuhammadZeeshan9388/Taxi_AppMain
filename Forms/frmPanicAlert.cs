using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Taxi_AppMain.Forms
{
    public partial class frmPanicAlert : Form
    {

        public string driverNo;

        private int _DriverId;

        public int DriverId
        {
            get { return _DriverId; }
            set { _DriverId = value; }
        }



        System.Media.SoundPlayer sp = new System.Media.SoundPlayer(System.Windows.Forms.Application.StartupPath + "\\sound\\Panic.wav");
         public frmPanicAlert()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmPanicAlert_Load);
            this.FormClosed += new FormClosedEventHandler(frmPanicAlert_FormClosed);
           
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
        }

       

        void frmPanicAlert_FormClosed(object sender, FormClosedEventArgs e)
         {
             StopSound();
         }

         private void StopSound()
         {

             sp.Stop();
         }

         private void PlaySound()
         {
             sp.PlayLooping();

         }

         void frmPanicAlert_Load(object sender, EventArgs e)
         {
             PlaySound();
          //  ShowDriversInfo();


         }

        public void ShowDriversInfo()
        {

            try
            {


                if (driverNo.ToString().Trim().Length > 0)
                {
                    if (lblDrivers.Text.Length == 0)
                        lblDrivers.Text = "Driver " + driverNo;
                    else
                        lblDrivers.Text += "," + driverNo;
                }
            }
            catch
            {


            }

        }

       

        private void btnStop_Click_1(object sender, EventArgs e)
        {
          
            this.Close();
        }

        private void btnMute_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                StopSound();

                btnMute.Text = "UnMute";
            }
            else
            {
                PlaySound();
                btnMute.Text = "Mute";

            }
        }

        private void btnCancelAlarm_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            CancelAlarm();
        }


        private void CancelAlarm()
        {
            try
            {
                if (DriverId != 0)
                {
                    new Taxi_Model.TaxiDataContext().stp_PanicUnPanicDriver(DriverId, false);

                   
                 //   (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshWaitingAndOnBoardDrivers();

                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshDashBoardDrivers();

                }

                this.Close();
            }
            catch (Exception ex)
            {


            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelAlarm();
        }

       
    }
}
