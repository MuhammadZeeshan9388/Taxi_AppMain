using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls;
using Telerik.Data;

namespace Taxi_AppMain
{
    public partial class frmEscortList : UI.SetupBase
    {
        Gen_EscortBO objMaster=null;
      //  int pageSize = 0;



        public frmEscortList()
        {
            try
            {
                InitializeComponent();
                this.Load += new EventHandler(frmCompanyList_Load);
                LoadCompanyList();
                //skip = 0;
                //txtSearch.Text = string.Empty;
               // PopulateData();

                grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
                grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
                objMaster = new Gen_EscortBO();

                this.SetProperties((INavigation)objMaster);
                grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

                grdLister.ShowRowHeaderColumn = false;
                //this.Shown += new EventHandler(frmCompanyList_Shown);


                //PopulateData();
                //---- adil
                grdLister.ShowGroupPanel = false;
              //  pageSize = AppVars.objPolicyConfiguration.ListingPagingSize.ToInt();

                //grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception ex)
            {
                //ex.Message
                ENUtils.ShowMessage(ex.Message);
            }

        }




        private void AddCreateCompanyColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 80;

            col.Name = "btnCreateCompany";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Add New Escort";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

        private void BindProperties()
        {

        }

        public static void AddEditColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.BestFit();


            col.Name = "ColEdit";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Edit";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);


        }

        private void AddDeleteColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.BestFit();

            col.Name = "ColDelete";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Delete";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }


        public override void RefreshData()
        {
            PopulateData();
        }

        //----- adil  14/5/13
        public override void PopulateData()
        {

            try
            {
                string searchTxt = txtSearch.Text.ToLower().Trim();
                string col = ddlColumns.Text.Trim().ToLower();


                bool col_name = false;
                bool col_email = false;
                bool col_address = false;
                bool col_telephone = false;
                bool col_mobile = false;
                //bool col_contactname = false;

                if (col == string.Empty)
                {
                    col_name = true;
                }

                if (col == "escortname")
                {
                    col_name = true;
                }

                else if (col == "email")
                {
                    col_email = true;
                }

                else if (col == "address")
                {
                    col_address = true;
                }
                else if (col == "telephone")
                {
                    col_telephone = true;
                }
                else if (col == "mobile")
                {
                    col_mobile = true;
                }
                


                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;



                var data1 = General.GetQueryable<Gen_Escort>(null).OrderBy(c => c.EscortName);

                //int cnt = data1.Count();
                //if (skip + pageSize > cnt && cnt - pageSize > 0)
                //    skip = cnt - pageSize;
                //else if (cnt <= pageSize)
                //    skip = 0;

                var query = from a in data1.AsEnumerable()

                            where
                                (col_name && (a.EscortName.ToStr().ToLower().Contains(searchTxt)
                                || searchTxt == string.Empty))
                                || (col_name && (a.EscortName.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_email && (a.EmailAddress.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_address && (a.AddressLine1.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_telephone && (a.TelephoneNo.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                || (col_mobile && (a.MobileNo.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                               // || (col_contactname && (a.ContactName.ToStr().ToLower().Contains(searchTxt) || searchTxt == string.Empty) && (a.HasEscort == true))

                            select new
                            {
                                Id = a.Id,
                                EscortName = a.EscortName,
                                //AccountType = a.AccountType.AccountTypeName,
                                Address = a.AddressLine1,
                                Email = a.EmailAddress,
                                Telephone = a.TelephoneNo,
                                Mobile = a.MobileNo,

                                //Address = a.Address.ToStr() != string.Empty ? a.Address + " - " + a.Address1 : a.Address1,
                            };

                // this.grdLister.TableElement.BeginUpdate();

                //  if (excludeSkip == false)
                //        grdLister.DataSource = query.Skip(skip).Take(pageSize).ToList();
                //    else
                grdLister.DataSource = query.ToList();

                //    this.grdLister.TableElement.EndUpdate();

                //     lblTotal.Text = "Total Companies : " + cnt.ToStr();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        void frmCompanyList_Load(object sender, EventArgs e)
        {
            try
            {
                //this.InitializeForm("frmEscortList");
                // adil
                LoadCompanyList();
                PopulateData();

                grdLister.Columns["Id"].IsVisible = false;

                grdLister.Columns["EscortName"].Width = 160;
                grdLister.Columns["EscortName"].HeaderText = "Escort Name";



                //grdLister.Columns["AccountType"].Width = 80;
                //grdLister.Columns["AccountType"].HeaderText = "Account Type";


                grdLister.Columns["Email"].Width = 160;
                grdLister.Columns["Address"].Width = 200;

                grdLister.Columns["Telephone"].Width = 140;
                grdLister.Columns["Mobile"].Width = 140;
                //grdLister.Columns["ContactName"].Width = 140;
                //grdLister.Columns["ContactName"].HeaderText = "Contact Name";

                grdLister.AddEditColumn();

                //if (this.CanDelete)
                //{
                    grdLister.AddDeleteColumn();
                //}

                ddlColumns.Items.AddRange(grdLister.Columns.Where(c => c.Name != "Id" && c.Name != "btnEdit" && c.Name != "btnDelete" && c.Name != "btnCreateCompany")
                                          .Select(c => c.Name));
                ddlColumns.SelectedIndex = 0;
            }
            catch (Exception ex)
            {


            }

        }

        private void LoadCompanyList()
        {


            grdLister.AllowAutoSizeColumns = true;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;


            var data1 = General.GetQueryable<Gen_Escort>(null).OrderBy(c => c.EscortName);


            var query = from a in data1


                        select new
                        {
                            Id = a.Id,
                            EscortName = a.EscortName,
                            Address = a.AddressLine1,
                            Email = a.EmailAddress,
                            Telephone = a.TelephoneNo,
                            Mobile = a.MobileNo,
                        };


            grdLister.DataSource = query.ToList();

            // this.SetRefreshingProperties(AppVars.BLData.GetCommand(query), grdLister, false);

            //grdLister.Columns["ColDelete"].Width = 70;


        }

        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Company ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {


                    RadGridView grid = gridCell.GridControl;
                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "btnedit")
            {
                ViewDetailForm();


            }
        }



        void frmLocationList_Load(object sender, EventArgs e)
        {
        }

        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ViewDetailForm();
        }

        private void ViewDetailForm()
        {

            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {

                //frmEscortList frm = new frmEscortList(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                //frm.ShowDialog();
                ShowEscortForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }


        private void ShowEscortForm(int id)
        {


            frmEscort frm = new frmEscort();
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();

        }


        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

              //  objMaster = new Gen_EscortBO();
                this.SetProperties((INavigation)objMaster);


                try
                {


                    objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
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

        private void btnFind_Click(object sender, EventArgs e)
        {

            skip = 0;
            PopulateData();
        }

        private void btnShowAllCompanies_Click(object sender, EventArgs e)
        {
            skip = 0;
            txtSearch.Text = string.Empty;
            PopulateData();
        }


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                PopulateData();

            }
        }

        private void btnAddNewCompany_Click(object sender, EventArgs e)
        {
            frmEscort frm = new frmEscort();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.Show();

        }
    }
}

