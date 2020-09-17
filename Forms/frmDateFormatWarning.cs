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
    public partial class frmDateFormatWarning : Form
    {
        private string ReminderValue;
        public frmDateFormatWarning()
        {
            InitializeComponent();

          
            this.Shown += new EventHandler(frmReminder_Shown);
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
