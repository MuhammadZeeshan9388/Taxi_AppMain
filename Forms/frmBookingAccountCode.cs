using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmBookingAccountCode : UI.SetupBase
    {
        public int? ID = 0;
     //   public string accountAddress = string.Empty;
        public frmBookingAccountCode()
        {
            InitializeComponent();
            

        }
        private void frmBookingAccountCode_Shown(object sender, EventArgs e)
        {
            txtAccountCode.Focus();
        }
        private void frmBookingAccountCode_Load(object sender, EventArgs e)
        {
            txtAccountCode.Focus();
        }
        void Check()
        {
            string Code = txtAccountCode.Text.ToString();
            Gen_Company obj = General.GetObject<Gen_Company>(c => c.CompanyCode == Code);
            if (obj != null)
            {
                string CompanyName = obj.CompanyName.ToString();
          //      accountAddress = obj.Address.ToStr().Trim();
                int CompanyId = obj.Id.ToInt();
                ID = CompanyId;
                this.Close();
            }
            else
            {
                ENUtils.ShowMessage("Enter Valid Company Code..");
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAccountCode.Text != "")
            {
                Check();
            }
            else
            {
                ENUtils.ShowMessage("Required: Company Code");
            }
        }

        private void txtAccountCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Check();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }




    }
}
