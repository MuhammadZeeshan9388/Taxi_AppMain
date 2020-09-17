using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Telerik.WinControls.UI;
using System.Runtime.InteropServices;

namespace Taxi_AppMain
{
    public partial class frmVoiceRequest : Form
    {
        private string _ContactName;

        public string ContactName
        {
            get { return _ContactName; }
            set { _ContactName = value; }
        }

        private string _PhoneNumber;

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }



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

        delegate void UIDelegate();




        public frmVoiceRequest(string name,string phone)
        {

            InitializeComponent();
            this.ContactName = name;
            this.PhoneNumber = phone;
            this.Shown += new EventHandler(frmVoiceRequest_Shown);

            //if (this.InvokeRequired)
            //{
            //    this.BeginInvoke(new UIDelegate(LoadData));

            //}
            //else
            //{

                LoadData();
          //  }

          
        }

        private void LoadData()
        {


            this.Text = this.ContactName + " VR";
            this.txtTitle.Text = "Driver " + this.ContactName + " - VOICE REQUEST";
          
        }

        void frmVoiceRequest_Shown(object sender, EventArgs e)
        {
            SetForegroundWindow(this.Handle);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            try
            {

                RingBackCall(this.ContactName, this.PhoneNumber);

              
            }
            catch (Exception ex)
            {


            }

            
        }



        private void RingBackCall( string name, string phoneNumber)
        {
            new Thread(delegate()
            {
                using (ClsRingBack clsRing = new ClsRingBack())
                {
                    string msg = clsRing.MakeCall(ClsRingBack.eCallTo.Driver, name, phoneNumber);


                    if (!string.IsNullOrEmpty(msg))
                    {
                        MethodInvoker mi = new MethodInvoker(delegate() { this.RingBackCallNotification("Call to Driver" + " " + name + " (" + phoneNumber + ")", msg); });
                        this.Invoke(mi);



                    }



                }
            }).Start();

        }


        private void RingBackCallNotification(string caption, string content)
        {
            try
            {

                RadDesktopAlert ringAlert = new Telerik.WinControls.UI.RadDesktopAlert();


                ringAlert.AutoCloseDelay = 8;
                ringAlert.FadeAnimationSpeed = 1;
                ringAlert.FadeAnimationType = FadeAnimationType.FadeIn;

                ringAlert.ShowOptionsButton = false;
                ringAlert.ShowPinButton = false;

                ringAlert.FixedSize = new Size(300, 100);

                ringAlert.CaptionText = caption;
                ringAlert.ContentText = content;
                //  ringAlert.ContentImage = contentImg;
                ringAlert.Show();
                this.Close();
            }
            catch (Exception ex)
            {


            }

        }

    }
}
