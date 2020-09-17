using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmInvalidPDAIPWarning : Form
    {
        private string ReminderValue;
        public frmInvalidPDAIPWarning()
        {
            InitializeComponent();

          
            this.Shown += new EventHandler(frmReminder_Shown);

            txtHeader.Text = "PDA LISTENER IP is not defined in Settings";
        }


        public frmInvalidPDAIPWarning(string officeIP)
        {
            InitializeComponent();


            this.Shown += new EventHandler(frmReminder_Shown);

            try
            {
                txtHeader.Text = "PDA LISTENER IP is InCorrect";
                txtOfficeIP.Text += " " + officeIP;
                txtListenerIP.Text+=" "+ AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim();
                txtOfficeIP.Visible = true;
                txtListenerIP.Visible = true;
                txtMismatch.Visible = true;
            }
            catch
            {


            }
        }

        void frmReminder_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void ShowReminder()
        {
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            Close();
        }

       
    }
}
