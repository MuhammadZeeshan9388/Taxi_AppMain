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
    public partial class frmReminder : Form
    {
        private string ReminderValue;
        public frmReminder(string reminder)
        {
            InitializeComponent();

            this.ReminderValue = reminder;
            this.Shown += new EventHandler(frmReminder_Shown);
        }

        void frmReminder_Shown(object sender, EventArgs e)
        {
            ShowReminder();
        }

        private void ShowReminder()
        {
            try
            {

                txtReminder.Text = this.ReminderValue;
                BringToFront();
                using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer(System.Windows.Forms.Application.StartupPath + "\\sound\\startup.wav"))
                {

                    sp.Play();

                }
            }
            catch
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            Close();
        }

       
    }
}
