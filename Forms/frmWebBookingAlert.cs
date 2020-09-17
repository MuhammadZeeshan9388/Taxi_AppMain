using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Taxi_AppMain
{
    public partial class frmWebBookingAlert : Form
    {
        string filePath = System.Windows.Forms.Application.StartupPath + "\\sound\\alert.wav";

        System.Media.SoundPlayer sp = null;
        public bool IsMute = false;



        public frmWebBookingAlert(string msg)
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmWebBookingAlert_FormClosing);


            SetMessage(msg);

            sp = new System.Media.SoundPlayer(filePath);


            this.Load += new EventHandler(frmWebBookingAlert_Load);
        }

       

        void frmWebBookingAlert_Load(object sender, EventArgs e)
        {
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - 255,30);
            PlaySound();
      
        }

        public bool IsMuted()
        {

            return IsMute;

        }

        public void SetMuted(bool muteSnd)
        {
            try
            {
                IsMute = muteSnd;
                btnmute.Text = IsMute ? "UN-MUTE" : "MUTE";
            }
            catch (Exception ex)
            {


            }
        }

        public void SetMessage(string msg)
        {

            txtMsg.Text = msg;
        }

        void frmWebBookingAlert_FormClosing(object sender, FormClosingEventArgs e)
        {
            sp.Stop();

          
          
           

            this.Dispose();
        }

      

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
          
        }

       
        private void frmLicenseAlert_MouseHover(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
        }

        private void frmLicenseAlert_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.9;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //  if (btnmute.Text == "MUTE")
            

                PlaySound();
            
        }

        private void PlaySound()
        {
            if (IsMute == false)
            {
                if (File.Exists(filePath))
                {
                    sp.Play();
                }
            }
        }

        private void btnmute_Click(object sender, EventArgs e)
        {
            btnmute.Text = btnmute.Text == "MUTE" ? "UN-MUTE" : "MUTE";

            
           
            IsMute=btnmute.Text == "UN-MUTE" ? true:false;

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
