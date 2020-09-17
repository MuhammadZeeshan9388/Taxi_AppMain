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
using Taxi_AppMain.Classes;
using Telerik.WinControls.UI.Export;

namespace Taxi_AppMain
{
    public partial class frmCompanyList : UI.SetupBase
    {
        CompanyBO objMaster;
        int pageSize = 0;
        RadGridViewExcelExporter exporter = null;


        public frmCompanyList()
        {
            try
            {
                InitializeComponent();
                this.Load += new EventHandler(frmCompanyList_Load);
                //LoadCompanyList();
                ////skip = 0;
                ////txtSearch.Text = string.Empty;
                //PopulateData();

                grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
                grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
                objMaster = new CompanyBO();

                this.SetProperties((INavigation)objMaster);
                grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

                grdLister.ShowRowHeaderColumn = false;
                //this.Shown += new EventHandler(frmCompanyList_Shown);


                //PopulateData();
                //---- adil
                grdLister.ShowGroupPanel = false;
                pageSize = AppVars.objPolicyConfiguration.ListingPagingSize.ToInt();

                grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

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
            col.DefaultText = "Create Company";
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

            //   LoadCompanyList();
            try
            {
                string searchTxt = txtSearch.Text.ToLower().Trim();
                string col = ddlColumns.Text.Trim().ToLower();


                bool col_name = false;
                bool col_account = false;
                bool col_email = false;
                bool col_address = false;
                bool col_telephone = false;
                bool col_mobile = false;
                bool col_contactname = false;
                bool col_accountcode = false;

                if (col == string.Empty)
                {
                    col_name = true;
                }

                if (col == "accountname")
                {
                    col_name = true;
                }

                if (col == "code")
                {
                    col_accountcode = true;

                }

                else if (col == "accounttype")
                {
                    col_account = true;
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
                else if (col == "contactname")
                {
                    col_contactname = true;
                }


                if (searchTxt.Length < 1)
                    searchTxt = string.Empty;



                long JobIndex = grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo ? grdLister.CurrentRow.Cells["Id"].Value.ToLong() : -1;
                int val = grdLister.TableElement.VScrollBar.Value;


                using (TaxiDataContext db = new TaxiDataContext())
                {

                   // var data1 =d.OrderBy(c => c.CompanyName);

                    var query = from a in db.Gen_Companies
                                join b in db.Gen_SubCompanies  on a.SubCompanyId equals b.Id into table2
                                from b in table2.DefaultIfEmpty()
                                where
                                    (col_name && (a.CompanyName.ToLower().Contains(searchTxt)
                                    || searchTxt == string.Empty))
                                    || (col_account && (a.AccountType.AccountTypeName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                     || (col_accountcode && (a.CompanyCode.ToLower() == searchTxt || searchTxt == string.Empty))
                                    || (col_email && (a.Email.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                    || (col_address && (a.Address.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                    || (col_telephone && (a.TelephoneNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                    || (col_mobile && (a.MobileNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                    || (col_contactname && (a.ContactName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))

                                select new
                                {
                                    Id = a.Id,
                                    Code = a.CompanyCode,
                                    AccountName = a.CompanyName,
                                    AccountType = a.AccountType.AccountTypeName,
                                    Address = a.Address,
                                    Email = a.Email,
                                    Telephone = a.TelephoneNo,
                                    Mobile = a.MobileNo,
                                    ContactName = a.ContactName,
                                    SubCompany = b!=null ? b.CompanyName : ""
                                };


                    grdLister.DataSource = query.ToList().OrderBy(item => item.AccountName, new NaturalSortComparer<string>()).ToList();


                }


                if (JobIndex > 0)
                {
                    grdLister.CurrentRow = grdLister.Rows.FirstOrDefault(c => c.Cells["Id"].Value.ToLong() == JobIndex);

                }
                grdLister.TableElement.VScrollBar.Value = val;

            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);

            }
        }


        void frmCompanyList_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    this.InitializeForm("frmCompany");
                }
                catch
                {


                }
                // adil

                PopulateData();

                grdLister.Columns["Id"].IsVisible = false;

                grdLister.Columns["AccountName"].HeaderText = "Account Name";
                grdLister.Columns["AccountName"].Width = 140;

                grdLister.Columns["Code"].Width = 60;
                //   grdLister.Columns["AccountName"].HeaderText = "Company Name";



                grdLister.Columns["AccountType"].Width = 50;
                grdLister.Columns["AccountType"].HeaderText = "Acc Type";


                grdLister.Columns["Email"].Width = 100;
                grdLister.Columns["Address"].Width = 140;

                grdLister.Columns["Telephone"].Width = 70;
                grdLister.Columns["Mobile"].Width = 70;
                grdLister.Columns["ContactName"].Width = 70;
                grdLister.Columns["ContactName"].HeaderText = "Contact Name";

                grdLister.Columns["SubCompany"].Width = 120;

                grdLister.AddEditColumn();

                if (this.CanDelete)
                {
                    grdLister.AddDeleteColumn();
                }

                ddlColumns.Items.AddRange(grdLister.Columns.Where(c => c.Name != "Id" && c.Name!="SubCompany" && c.Name != "btnEdit" && c.Name != "btnDelete" && c.Name != "btnCreateCompany")
                                          .Select(c => c.Name));
                ddlColumns.SelectedIndex = 0;


                UI.GridFunctions.SetFilter(grdLister);
            }
            catch (Exception ex)
            {


            }

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
                ShowCompanyForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }


        private void ShowCompanyForm(int id)
        {


            frmCompany frm = new frmCompany();
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

                objMaster = new CompanyBO();

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








      


        //public override void PopulateData()
        //{

        //    LoadCompanyList();

        //}

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

        private void InitializeExportGrid()
        {
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();

            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();


            this.radGridView1.Location = new System.Drawing.Point(405, 60);
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(240, 150);
            this.radGridView1.TabIndex = 18;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.Visible = false;

            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();

            this.radPanel1.Controls.Add(this.radGridView1);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    if (radGridView1 == null)
                        InitializeExportGrid();


                    radGridView1.Columns.Clear();
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("AccountName", "AccountName"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Address", "Address"));


                    //this.radGridView1.Columns["Telephone"].ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Text;
                    //this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Custom;

                    //   radGridView1.DataSource = query;


                    var list = (from a in General.GetQueryable<Gen_Company>(null)
                                orderby a.CompanyName
                                select new
                                {
                                    CompanyName = a.CompanyName,
                                    Address = a.Address
                                }).ToList();


                    radGridView1.RowCount = list.Count;
                    for (int i = 0; i < list.Count; i++)
                    {
                        radGridView1.Rows[i].Cells["AccountName"].Value = list[i].CompanyName;
                        radGridView1.Rows[i].Cells["Address"].Value = list[i].Address;

                        //  radGridView1.Rows[i].Cells["Telephone"].ViewInfo.;
                        //radGridView1.Rows[i].Cells["Telephone"].Value = (radGridView1.Rows[i].Cells["Telephone"].Value.ToString().Replace(".", ""));
                    }

                    // this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Fixed;
                    //this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Custom;
                    ExportToExcelML export = new ExportToExcelML(this.radGridView1);
                    export.ExportVisualSettings = true;
                    export.HiddenColumnOption = HiddenOption.ExportAsHidden;
                    export.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                    export.ExcelCellFormatting += new Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventHandler(export_ExcelCellFormatting);
                    //

                    //
                    //radGridView1.Columns["PickupDate"].HeaderText = "Pickup Date-Time";

                    //radGridView1.Columns["From"].HeaderText = "Pick-up Address";
                    //radGridView1.Columns["To"].HeaderText = "Drop-off Address";
                    radGridView1.Columns["AccountName"].HeaderText = "Account Name";

                    //Me.RadGridView1.Columns(0).ExcelExportType = Export.DisplayFormatType.Text

                    //CompanyName
                    exporter = new RadGridViewExcelExporter();

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                    worker.RunWorkerAsync(saveFileDialog1.FileName);
                    exporter.Progress += new ProgressHandler(exportProgress);

                    this.btnExport.Enabled = false;

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        void export_ExcelCellFormatting(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {

            e.ExcelStyleElement.AlignmentElement.WrapText = false;
        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.IsDisposed)
            {
                e.Cancel = true;
                return;

            }



            exporter.Export(this.radGridView1, (String)e.Argument, "Account List");


        }

        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {



            this.btnExport.Enabled = true;
            this.radProgressBar1.Value1 = 0;

            ENUtils.ShowMessage("Export successfully.");

        }
        //Update the progress bar with the export progress    
        private void exportProgress(object sender, ProgressEventArgs e)
        {

            if (this.IsDisposed)
                return;
            // Call InvokeRequired to check if thread needs marshalling, to access properly the UI thread.
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(
                    delegate
                    {
                        if (e.ProgressValue <= 100)
                        {
                            radProgressBar1.Value1 = e.ProgressValue;
                        }
                        else
                        {
                            radProgressBar1.Value1 = 100;
                        }
                    }));
                }
                else
                {
                    if (e.ProgressValue <= 100)
                    {
                        radProgressBar1.Value1 = e.ProgressValue;
                    }
                    else
                    {
                        radProgressBar1.Value1 = 100;
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

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
            frmCompany _frmCompany = new frmCompany();
            _frmCompany.StartPosition = FormStartPosition.CenterScreen;
            _frmCompany.FormBorderStyle = FormBorderStyle.FixedSingle;
            _frmCompany.Show();

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {

                frmCompany _frmCompany = new frmCompany(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                _frmCompany.StartPosition = FormStartPosition.CenterScreen;
                _frmCompany.FormBorderStyle = FormBorderStyle.FixedSingle;
                _frmCompany.Show();

            }
        }
    }
}

