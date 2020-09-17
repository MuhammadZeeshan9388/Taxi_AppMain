using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using System.Runtime.InteropServices;

namespace Taxi_AppMain
{
    public partial class frmPopupInternalMessage : Form
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

        private bool IsWelcomeMsg;
        private long messageId;
        private string msgValue;


        public frmPopupInternalMessage(string msg,bool IsWelcomeMessage,long msgId)
        {
            InitializeComponent();
            this.IsWelcomeMsg = IsWelcomeMessage;
            this.messageId = msgId;
            this.Shown += new EventHandler(frmAccJobsReminder_Shown);

            string senderName = string.Empty;
            if(msg.Contains("sender>>"))
            {
                string[] arr = msg.Split(new string[] { "sender>>" },StringSplitOptions.RemoveEmptyEntries);

                msg = arr[0];
                senderName = arr[1];


            }

            txtMessage.Text = msg;
            this.msgValue = msg;
            lblSenderName.Text = senderName;
        }

        void frmAccJobsReminder_Shown(object sender, EventArgs e)
        {
            SetForegroundWindow(this.Handle);


            PlaySound();
        }

        private void PlaySound()
        {
            try
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(System.Windows.Forms.Application.StartupPath + "\\sound\\Message1.wav");
                sp.Play();
            }
            catch (Exception ex)
            {


            }
       }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.IsWelcomeMsg)
            {
                new Taxi_Model.TaxiDataContext().stp_SaveInternalMessage(this.messageId, "", this.msgValue.ToStr(), true, true,null);

            }

            Close();
        }

       
    }
}
