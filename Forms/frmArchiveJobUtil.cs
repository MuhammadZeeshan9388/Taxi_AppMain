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
using Telerik.WinControls.UI;
using Utils;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmArchiveJobUtil : Form
    {
        public frmArchiveJobUtil()
        {
            InitializeComponent();
           

            comboBox1.SelectedIndex=1;


           

        }

       
        void frmJobClearingUtil_Load(object sender, EventArgs e)
        {


           

        }

      

        

       
        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMoveJobs_Click(object sender, EventArgs e)
        {
        
        }
    }
}
