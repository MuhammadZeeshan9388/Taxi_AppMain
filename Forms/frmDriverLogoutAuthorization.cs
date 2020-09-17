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
using Utils;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace Taxi_AppMain
{
    public partial class frmDriverLogoutAuthorization : Form
    {
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void bringToFront(string title)
        {
            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == IntPtr.Zero)
            {
                return;
            }

            // Make Calculator the foreground application
            SetForegroundWindow(handle);
        }


        System.Media.SoundPlayer sp = new System.Media.SoundPlayer(System.Windows.Forms.Application.StartupPath + "\\sound\\auth.wav");
        private DateTime _OpenedDateTime;

        public DateTime OpenedDateTime
        {
            get { return _OpenedDateTime; }
            set { _OpenedDateTime = value; }
        }






        private int _DriverId;

        public int DriverId
        {
            get { return _DriverId; }
            set { _DriverId = value; }
        }



        private string _DriverNo;

        public string DriverNo
        {
            get { return _DriverNo; }
            set { _DriverNo = value; }
        }






        private string _AuthorizedBy;

        public string AuthorizedBy
        {
            get { return _AuthorizedBy; }
            set { _AuthorizedBy = value; }
        }

        private bool _IsAuthorized;

        public bool IsAuthorized
        {
            get { return _IsAuthorized; }
            set { _IsAuthorized = value; }
        }


       






        public frmDriverLogoutAuthorization(int driverId,string driverNo)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmAuthorization_Load);

           
            this.DriverId = driverId;
            this.DriverNo = driverNo;
            this.OpenedDateTime = DateTime.Now;
            this.KeyDown += new KeyEventHandler(frmAuthorization_KeyDown);
            this.KeyPreview = true;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 475, 25);
        }

        void frmAuthorization_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                Allow();

            }
            else if (e.KeyCode == Keys.D)
            {
                Deny();

            }
        }

        void frmAuthorization_Load(object sender, EventArgs e)
        {
            try
            {

                if (DriverId == 0)
                {
                    CloseForm();

                }
                else
                {
                   

                    txtAction.Text="Driver : " + this.DriverNo+ " Logout Authorization";


                    //txtDriver.Text = "Driver : " + this.ObjBooking.Fleet_Driver.DriverNo;
                    //txtPickupPoint.Text = this.ObjBooking.FromAddress;
                    //txtDestination.Text = this.ObjBooking.ToAddress;
                 //   PlaySound();
                }

            }
            catch (Exception ex)
            {



            }
        }

        private void btnYes_Click(object sender, EventArgs e)
        {

            Allow();

        
        }

        private void Allow()
        {

            try
            {
                this.IsAuthorized = true;

                (new TaxiDataContext()).stp_LoginLogoutDriver(DriverId, false,null);


                new Thread(delegate()
                {
                    AuthorizationPermit();
                  
                }).Start();



                Thread.Sleep(2000);
                CloseForm();

            }
            catch (Exception ex)
            {


            }

        }


        private bool AuthorizationPermit()
        {
            bool rtn=false;
            try
            {
                string currentTime =string.Format("{0:dd/MM/yyyy hh:mm:ss}",DateTime.Now);

                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {


                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {
                        // For Google Map Use Socket To Send/Receive Data
                       rtn= General.SendMessageToPDA("request pda=" + this.DriverId + "=" + this.DriverNo + "=logout auth status>>yes>>" + DriverId + ">>" + currentTime+ "=10", this.DriverId.ToString()).Result.ToBool();

                       try
                       {
                           File.AppendAllText(Application.StartupPath + "\\logoutauth.txt", DateTime.Now + " : " + rtn.ToStr() + Environment.NewLine);

                           rtn = General.SendMessageToPDA("request pda=" + this.DriverId + "=" + this.DriverNo + "=logout auth status>>yes>>" + DriverId + ">>" + currentTime + "=10", this.DriverId.ToString()).Result.ToBool();
                       }
                       catch
                       {


                       }

                    }
                    else
                    {
                        // For Map Point Use GCM to Send Data
                        //     General.SendGCMMessage("auth status=yes=" + this.DriverId, this.ObjBooking.Fleet_Driver.DeviceId);
                    }
                }
                else
                {
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {
                        // For Google Map Use Socket To Send/Receive Data
                        General.SendMessageToPDA("request pda=" + DriverId + "=" + DriverNo + "=logout auth status>>yes>>" + DriverId + ">>" + currentTime+  "=10");
                    }
                    else
                    {
                        // For Map Point Use GCM to Send Data
                        //     General.SendGCMMessage("auth status=yes=" + this.DriverId, this.ObjBooking.Fleet_Driver.DeviceId);
                    }
                }



                Thread.Sleep(1000);
                new Thread(delegate()
                {
                    General.SendMessageToPDA("request force logout=" + this.DriverNo);
                }).Start();



                return rtn;
                //(new TaxiDataContext()).stp_LoginLogoutDriver(DriverId, false);

                // (new TaxiDataContext()).stp_UpdateJob(this.JobId, this.DriverId, this.JobStatusId.ToIntorNull(), this.DriverStatusId.ToIntorNull(), AppVars.objPolicyConfiguration.SinBinTimer.ToInt());
            }
            catch (Exception ex)
            {
                return false;

            }

        }


       

        private void btnNo_Click(object sender, EventArgs e)
        {
            Deny();
            
        }


        private void Deny()
        {

            try
            {
                this.IsAuthorized = true;

                new Thread(delegate()
                {
                    AuthorizationDenied();
                }).Start();


                Thread.Sleep(1000);
                CloseForm();

            }
            catch (Exception ex)
            {


            }

        }



        private void AuthorizationDenied()
        {
            string currentTime = string.Format("{0:dd/MM/yyyy hh:mm:ss}", DateTime.Now);

            if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
            {
                if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                {
                    // For Google Map Use Socket To Send/Receive Data
                    General.SendMessageToPDA("request pda=" + DriverId + "=" + DriverNo + "=logout auth status>>no>>" + DriverId + ">>" + currentTime + "=10", DriverId.ToString());
                }
                else
                {
                    // For Map Point Use GCM to Send Data
                  //  General.SendGCMMessage("auth status=no=" + this.DriverId, this.ObjBooking.Fleet_Driver.DeviceId);
                }
            }
            else
            {
                if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                {
                    // For Google Map Use Socket To Send/Receive Data
                    General.SendMessageToPDA("request pda=" + DriverId + "=" + DriverNo + "=logout auth status>>no>>" + DriverId + ">>" + currentTime + "=10", DriverId.ToString());
                }
                else
                {
                    // For Map Point Use GCM to Send Data
                //    General.SendGCMMessage("auth status=no=" + this.DriverId, this.ObjBooking.Fleet_Driver.DeviceId);
                }
            } 

        }
       

        public void CloseForm()
        {
            try
            {
                StopSound();



                Thread.Sleep(1000);

                new BroadcasterData().BroadCastToAll("**broadcast close logout auth>>" + Environment.MachineName);


                AppVars.frmMDI.RefreshDashboardDrivers();


                timer1.Stop();
                timer2.Stop();

                timer1.Dispose();
                timer2.Dispose();


                this.Close();
            }
            catch
            {


            }
        }


        private void StopSound()
        {

            sp.Stop();
        }

        private void PlaySound()
        {
           // sp.Play();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            if (DateTime.Now.Subtract(this.OpenedDateTime).TotalSeconds > 59)
            {
                CloseForm();
            }
            else
            {
                PlaySound();
                SetForegroundWindow(this.Handle);

               

            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            txtTimer.Text = (txtTimer.Text.ToInt() - 1).ToStr();
        }
    }
}
