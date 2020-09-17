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
    public partial class frmCompanyContacts  : UI.SetupBase
    {
        CompanyContactBO objMaster = null;
        private int _CompanyId;

        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        public frmCompanyContacts(int companyId)
        {
            InitializeComponent();
            ComboFunctions.FillCompanyCombo(ddlCompany);
            this.Shown += new EventHandler(frmCompanyDepartments_Shown);
            objMaster = new CompanyContactBO();
            this.SetProperties((INavigation)objMaster);
            ddlCompany.SelectedValue = companyId;
            this.CompanyId = companyId;

           

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
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }

                objMaster.Current.ContactName = txtContactName.Text.ToStr().Trim();

                objMaster.Current.Email = txtEmail.Text.ToStr().Trim();
                objMaster.Current.TelephoneNo = txtTelNo.Text.ToStr().Trim();
                objMaster.Current.MobileNo = txtMobNo.Text.ToStr().Trim();
                
                objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToInt();

                objMaster.Current.IsDefault = false;

               // objMaster.Current.Passwrd = txtPwd.Text.Trim();

                objMaster.Save();


                this.saved = true;
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }

        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            txtContactName.Text = objMaster.Current.ContactName.ToStr();
            txtEmail.Text = objMaster.Current.Email.ToStr();

            txtTelNo.Text = objMaster.Current.TelephoneNo.ToStr();
            txtMobNo.Text = objMaster.Current.MobileNo.ToStr();

            ddlCompany.SelectedValue = objMaster.Current.CompanyId;


          //  txtPwd.Text = objMaster.Current.Passwrd;

        }

        public override void AddNew()
        {
          
        }

        public override void OnNew()
        {
            txtContactName.Focus();
        }
    }
}

