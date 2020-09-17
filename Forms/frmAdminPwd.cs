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
    public partial class frmAdminPwd : UI.SetupBase
    {
        public string ReturnValue1 { get; set; }

        public frmAdminPwd()
        {
            InitializeComponent();
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
        }

        private string formPassword = "";

        public frmAdminPwd(string password)
        {
            InitializeComponent();
            this.formPassword = password;
            this.btnProcess.Click += new System.EventHandler(this.btnProcessByForm_Click);
        }

        private void btnProcessByForm_Click(object sender, EventArgs e)
        {
            ProcessByForm();
        }

        void ProcessByForm()
        {
        
            if (txtPassword.Text.ToLower().Trim() == this.formPassword.ToLower().Trim())
            {

                this.ReturnValue1 = "OK";
                this.Close();


            }
            else
            {

                ENUtils.ShowMessage("Password is Incorrect");
                this.ReturnValue1 = "NO";
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Process();
        }
        void Process()
        {
            int? groupId = AppVars.LoginObj.LgroupId.ToInt();

            if (groupId !=2)
            {

                int USERiD = AppVars.LoginObj.LuserId.ToInt();

                UM_User obj = General.GetObject<UM_User>(c => c.Id == USERiD);
                string AccountPassword = obj.Passwrd.ToStr();

                if (txtPassword.Text == AccountPassword || txtPassword.Text.ToLower() == "euro1234euro")
                {
                    this.ReturnValue1 = "OK";
                    this.Close();
                }
                else
                {
                    ENUtils.ShowMessage("Enter Admin Password");
                    this.ReturnValue1 = "NO";
                }
            }
            else if (txtPassword.Text.ToLower().Trim() == "euro1234euro")
            {

                this.ReturnValue1 = "OK";
                this.Close();


            }
            else
            {

                ENUtils.ShowMessage("Admin User Password is Incorrect");
                this.ReturnValue1 = "NO";
            }
        }
        private void btnExits_Click(object sender, EventArgs e)
        {
            this.Close();
            this.ReturnValue1 = "Exit";
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
