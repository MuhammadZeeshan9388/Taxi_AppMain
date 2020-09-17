using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Utils;
using Taxi_Model;
using Taxi_BLL;

namespace Taxi_AppMain
{
    public partial class frmLockingPwd : UI.SetupBase
    {
        public string ReturnValue1 { get; set; }

        public frmLockingPwd()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Process();
        }
        void Process()
        {
           

                string pwd = "euro1234euro";
           

                if (txtPassword.Text == pwd)
                {
                    this.ReturnValue1 = "euro1234euro";
                    this.Close();
                }
                else
                {
                    ENUtils.ShowMessage("Enter Unlock Password");
                    this.ReturnValue1 = "";
                }
           
        }
        private void btnExits_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ReturnValue1 = "";
        }


        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Process();

            }
        }
    }
}
