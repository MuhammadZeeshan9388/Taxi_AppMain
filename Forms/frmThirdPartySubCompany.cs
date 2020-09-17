using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using DAL;
using Taxi_Model;
using UI;
using Telerik.WinControls.UI;
using Utils;
using Telerik.WinControls;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class frmThirdPartySubCompany : UI.SetupBase
    {
       


        SubCompanyBO objMaster;
        public frmThirdPartySubCompany()
        {
            InitializeComponent();

            objMaster = new SubCompanyBO();
            this.SetProperties((INavigation)objMaster);
            this.Shown += new EventHandler(frmCompany_Shown);

            this.Load += new EventHandler(frmSubCompany_Load);
          
            this.grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            this.grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            this.grdLister.RowsChanging += new GridViewCollectionChangingEventHandler(grdLister_RowsChanging);
            this.grdLister.RowsChanged += new GridViewCollectionChangedEventHandler(grdLister_RowsChanged);
            this.Shown += new EventHandler(frmSubCompany_Shown);


           // FillMapIconCombo();

            OnNew();

            if (this.ThemeName.ToStr().ToLower() != "control default")
                grdLister.AutoCellFormatting = false;


          

        }

        void grdLister_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                PopulateData();
            }
        }


        //private void FillMapIconCombo()
        //{
        //    try
        //    {

            
        //        string path = System.Windows.Forms.Application.StartupPath + "\\VehicleImages\\";
        //        RadListDataItem radItem = null;
        //        foreach (var item in General.GetQueryable<Gen_MapIcon>(null).ToList())
        //        {
        //            radItem = new RadListDataItem();
        //            radItem.Font = new Font("Tahoma", 12, FontStyle.Bold);
        //            radItem.Text = item.MapIconName;
        //            radItem.Value = item.MapIconName+"_";

        //            if (System.IO.File.Exists(path + item.MapIconName + ".png"))
        //            {
        //                radItem.Image = Image.FromFile(path + item.MapIconName + ".png");
        //            }
        //        //    radItem.Height = 40;
        //            ddlMapIcon.Items.Add(radItem);
        //        }

        //        ddlMapIcon.DropDownListElement.ItemHeight = 30;
        //        ddlMapIcon.Items[0].Height = 30;
        //    }
        //    catch (Exception ex)
        //    {

        //        ENUtils.ShowMessage(ex.Message);
        //    }


        //}

        void frmSubCompany_Shown(object sender, EventArgs e)
        {
            FocusOnCompanyName();
        }

        private void FocusOnCompanyName()
        {

            txtCompanyName.Focus();
        }

        void grdLister_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {


                objMaster = new SubCompanyBO();

                try
                {

                    objMaster.GetByPrimaryKey((e.NewItems[0] as GridViewRowInfo).Cells["Id"].Value.ToInt());
                    objMaster.Delete(objMaster.Current);


                    
                }
                catch (Exception ex)
                {
                    if (objMaster.Errors.Count > 0)
                        ENUtils.ShowMessage(objMaster.ShowErrors());
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }
                    e.Cancel = true;

                }


            }
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
              
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                RadGridView grid = gridCell.GridControl;
                if (gridCell.ColumnInfo.Name == "btnDelete")
                {

                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Third Party Company ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                      gridCell.RowInfo.Delete();
                   
                    }
                }
                else if (gridCell.ColumnInfo.Name == "btnEdit" && grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                    DisplayRecord();

                }
            }
            catch (Exception ex)
            {

                //ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {

            if (e.Row != null && e.Row is GridViewDataRowInfo)
            {

                objMaster.GetByPrimaryKey(e.Row.Cells["Id"].Value.ToInt());
                DisplayRecord();
            }
        }

        void frmSubCompany_Load(object sender, EventArgs e)
        {
            PopulateData();

            grdLister.AddEditColumn();
            grdLister.AddDeleteColumn();
        }



        void frmCompany_Shown(object sender, EventArgs e)
        {
            txtCompanyName.Focus();
        }  

     


        public override void PopulateData()
        {
            try
            {

                var query = from a in General.GetQueryable<Gen_SubCompany>(c=>c.IsTpCompany==true)
                           
                            select new
                            {
                                Id = a.Id,
                                Name=a.CompanyName,
                                Email=a.EmailAddress,
                                TelephoneNo=a.TelephoneNo,
                                Address=a.Address,
                                Fax=a.Fax,
                                IsSysGen=a.IsSysGen
                            };


                grdLister.DataSource = query.ToList();

                grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                grdLister.AllowAutoSizeColumns = true;

                grdLister.Columns["Id"].IsVisible=false;
                grdLister.Columns["IsSysGen"].IsVisible = false;
                grdLister.Columns["TelephoneNo"].HeaderText = "Telephone No";


            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

    


        #region Overridden Methods


        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;


            

            txtCompanyName.Text = objMaster.Current.CompanyName.ToStr();
            txtCompanyEmail.Text = objMaster.Current.EmailAddress.ToStr();
            //txtCompanyFax.Text = objMaster.Current.Fax.ToStr();
            txtTelNo.Text = objMaster.Current.TelephoneNo.ToStr();
            txtCompanyEmergencyNo.Text = objMaster.Current.TelephoneNo.ToStr();
            txtCompanyWebsite.Text = objMaster.Current.WebsiteUrl.ToStr();
            txtAddress.Text = objMaster.Current.Address.ToStr();
            //txtEmailCC.Text = objMaster.Current.EmailCC.ToStr().Trim();

            //if (objMaster.Current.CompanyLogo != null)
            //    pb_logo.SetImage(objMaster.Current.CompanyLogo.ToArray());
            //else
            //    pb_logo.Clear();

            //txtSortCode.Text = objMaster.Current.SortCode.ToStr().Trim();
            //txtAccountNo.Text = objMaster.Current.AccountNo.ToStr().Trim();
            //txtAccountTitle.Text = objMaster.Current.AccountTitle.ToStr().Trim();
            //txtBank.Text = objMaster.Current.BankName.ToStr().Trim();
            //txtCompanyNumber.Text = objMaster.Current.CompanyNumber.ToStr().Trim();
            //txtVATNumber.Text = objMaster.Current.CompanyVatNumber.ToStr().Trim();

       
            if (objMaster.Current.BackgroundColor!=0)
            {
                Color clr = Color.FromArgb(objMaster.Current.BackgroundColor.ToInt());
                txtBgColor.BackColor = clr;
                txtBgColor.Tag = clr.ToArgb();
            }



            //ddlMapIcon.SelectedValue = objMaster.Current.MapIcon.ToStr().Trim();


            //smtpchkIsSecureConn.Checked = objMaster.Current.SmtpHasSSL.ToBool();
            //SmtpHost.Text = objMaster.Current.SmtpHost.ToStr().Trim();
            //smtpPassword.Text = objMaster.Current.SmtpPassword.ToStr().Trim();
            //smtpPort.Text = objMaster.Current.SmtpPort.ToStr().Trim();
            //smtpUserName.Text = objMaster.Current.SmtpUserName.ToStr().Trim();
            //chkDisableAcceptEmail.Checked = objMaster.Current.DisableAcceptEmail.ToBool();
            //chkDisableDeclineEmail.Checked = objMaster.Current.DisableDeclineEmail.ToBool();

        }

      


        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {
            txtAddress.Text = string.Empty;
 
            txtBgColor.Text = string.Empty;
            txtCompanyEmail.Text = string.Empty;
            txtCompanyEmergencyNo.Text = string.Empty;
 
            txtCompanyName.Text = string.Empty;
            txtCompanyWebsite.Text = string.Empty;
            txtTelNo.Text = string.Empty;
            //txtVATNumber.Text = string.Empty;
            //smtpUserName.Text = string.Empty;
            //smtpPort.Text = string.Empty;
            //smtpPassword.Text = string.Empty;
            //SmtpHost.Text = string.Empty;
            //smtpchkIsSecureConn.Checked = false;
            //chkDisableDeclineEmail.Checked = false;
            //chkDisableAcceptEmail.Checked = false;

            //pb_logo.Clear();
            FocusOnCompanyName();


            //if (ddlMapIcon.Items.Count > 0)
            //{
            //    ddlMapIcon.SelectedItem = ddlMapIcon.Items[0];
            //  //  pb_mapicon.Image = ddlMapIcon.Items[0].Image;

            //}


            objMaster.Clear();
        }


        public override void Save()
        {
            OnSave();
           
        }


        public bool OnSave()
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


                objMaster.Current.CompanyName = txtCompanyName.Text.Trim();
                objMaster.Current.EmailAddress = txtCompanyEmail.Text.Trim();
                objMaster.Current.Address = txtAddress.Text.Trim();
                objMaster.Current.TelephoneNo = txtTelNo.Text.Trim();
                objMaster.Current.EmergencyNo = txtCompanyEmergencyNo.Text.Trim();
                objMaster.Current.WebsiteUrl =txtCompanyWebsite.Text.Trim();
                
                //objMaster.Current.EmailCC = txtEmailCC.Text.Trim();
                objMaster.Current.IsSysGen = false;

                objMaster.Current.BackgroundColor = txtBgColor.Tag.ToInt();
                objMaster.Current.IsTpCompany = true;

                //string mapIcon = ddlMapIcon.SelectedValue.ToStr().Trim();
                //if (string.IsNullOrEmpty(mapIcon))
                //    mapIcon = ddlMapIcon.Items[0].Value.ToStr();


                //objMaster.Current.MapIcon = mapIcon;


                //objMaster.Current.SmtpHasSSL = smtpchkIsSecureConn.Checked;
                //objMaster.Current.SmtpHost = SmtpHost.Text.Trim();
                //objMaster.Current.SmtpPassword = smtpPassword.Text.Trim();
                //objMaster.Current.SmtpPort = smtpPort.Text.Trim();
                //objMaster.Current.SmtpUserName = smtpUserName.Text.Trim();
                //objMaster.Current.DisableAcceptEmail = chkDisableAcceptEmail.Checked;
                //objMaster.Current.DisableDeclineEmail = chkDisableDeclineEmail.Checked;

                objMaster.Save();


                OnNew();

                PopulateData();

                return true;

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
                return false;
            }


        }

  


        

        #endregion

        private void frmCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
         
        }

       

       


        private void btnPickBgColor_Click(object sender, EventArgs e)
        {
            SetColor(txtBgColor);
        }

        private void btnClearBgColor_Click(object sender, EventArgs e)
        {
            ClearColor(txtBgColor);
        }

       

        // Adil 28/5/13
        private void SetColor(TextBox txt)
        {
            if (DialogResult.OK == colorDialog1.ShowDialog())
            {

                txt.BackColor = colorDialog1.Color;
                txt.Tag = colorDialog1.Color.ToArgb();
            }

        }
        private void ClearColor(TextBox txt)
        {

            txt.BackColor = Color.White;
            txt.Tag = null;


        }

        private void frmCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        //private void ddlMapIcon_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        //{
        //    if (ddlMapIcon.SelectedItem != null)
        //    {
        //        pb_mapicon.Image = ddlMapIcon.SelectedItem.Image;
        //    }
        //    else
        //    {

        //        pb_mapicon.Image = null;
        //    }
        //}

       


    }



}
