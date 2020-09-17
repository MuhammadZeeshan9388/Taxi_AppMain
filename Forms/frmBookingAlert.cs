using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmBookingAlert : UI.SetupBase
    {
        System.Media.SoundPlayer sp = new System.Media.SoundPlayer(System.Windows.Forms.Application.StartupPath + "\\sound\\Startup.wav");
        public frmBookingAlert()
        {
            InitializeComponent();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            sp.Stop();
            this.Close();
        }

        private void frmBookingAlert_Load(object sender, EventArgs e)
        {
            sp.PlayLooping();
        }

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
