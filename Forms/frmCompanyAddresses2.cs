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
using DAL;
using Utils;
using Telerik.WinControls.UI;
namespace Taxi_AppMain
{
    public partial class frmCompanyAddresses : UI.SetupBase
    {
        //CompanyDepartmentBO objMaster = null;
        CompanyAddressesBO objCompanyAddresses = null;
        private int _CompanyId;

        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        public frmCompanyAddresses(int companyId)
        {
            InitializeComponent();
            ComboFunctions.FillCompanyCombo(ddlCompany);
          
            this.Shown += new EventHandler(frmCompanyDepartments_Shown);
            objCompanyAddresses = new CompanyAddressesBO();
            this.SetProperties((INavigation)objCompanyAddresses);
            ddlCompany.SelectedValue = companyId;
            this.CompanyId = companyId;

           

        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            
        }
     
       

        private bool saved = false;

        public bool Saved
        {
            get { return saved; }
            set { saved = value; }
        }
        void frmCompanyDepartments_Shown(object sender, EventArgs e)
        {
            OnNew();
        }




       

        public override void Save()
        {
            try
            {
                if (objCompanyAddresses.PrimaryKeyValue == null)
                {
                    objCompanyAddresses.New();
                }
                else
                {
                    objCompanyAddresses.Edit();
                }

                objCompanyAddresses.Current.Address = txtAddress.Text.ToStr().Trim();

                objCompanyAddresses.Current.CompanyId = ddlCompany.SelectedValue.ToInt();

                objCompanyAddresses.Save();


                this.saved = true;
            }
            catch (Exception ex)
            {
                if (objCompanyAddresses.Errors.Count > 0)
                    ENUtils.ShowMessage(objCompanyAddresses.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }

        public override void DisplayRecord()
        {
            if (objCompanyAddresses.Current == null) return;

            txtAddress.Text = objCompanyAddresses.Current.Address.ToStr();
            ddlCompany.SelectedValue = objCompanyAddresses.Current.CompanyId;


        }

        public override void AddNew()
        {
            txtAddress.Text = "";
        }

        public override void OnNew()
        {
            txtAddress.Focus();
        }
    }
}
