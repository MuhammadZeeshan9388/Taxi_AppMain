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
    public partial class frmCompanyBookedBy : UI.SetupBase
    {
        //CompanyDepartmentBO objMaster = null;
        CompanyBookedByBO objCompanyBookedBy = null;
        private int _CompanyId;

        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        public frmCompanyBookedBy(int companyId)
        {
            InitializeComponent();
            ComboFunctions.FillCompanyCombo(ddlCompany);
          
            this.Shown += new EventHandler(frmCompanyDepartments_Shown);
            objCompanyBookedBy = new CompanyBookedByBO();
            this.SetProperties((INavigation)objCompanyBookedBy);
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
                if (objCompanyBookedBy.PrimaryKeyValue == null)
                {
                    objCompanyBookedBy.New();
                }
                else
                {
                    objCompanyBookedBy.Edit();
                }

                objCompanyBookedBy.Current.BookedBy = txtBookedBy.Text.ToStr().Trim();
                objCompanyBookedBy.Current.EmailAddress = txtEmail.Text.ToStr().Trim();
                objCompanyBookedBy.Current.CompanyId = ddlCompany.SelectedValue.ToInt();

                objCompanyBookedBy.Save();


                this.saved = true;
            }
            catch (Exception ex)
            {
                if (objCompanyBookedBy.Errors.Count > 0)
                    ENUtils.ShowMessage(objCompanyBookedBy.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }

        public override void DisplayRecord()
        {
            if (objCompanyBookedBy.Current == null) return;

            txtBookedBy.Text = objCompanyBookedBy.Current.BookedBy.ToStr();
            txtEmail.Text = objCompanyBookedBy.Current.EmailAddress.ToStr();
            ddlCompany.SelectedValue = objCompanyBookedBy.Current.CompanyId;


        }

        public override void AddNew()
        {
            txtBookedBy.Text = "";
        }

        public override void OnNew()
        {
            txtBookedBy.Focus();
        }
    }
}
