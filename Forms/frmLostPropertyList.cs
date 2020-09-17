using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using Telerik.WinControls.UI.Docking;
using Utils;
using Telerik.WinControls;
using UI;
using Telerik.Data;

namespace Taxi_AppMain
{
    public partial class frmLostPropertyList : UI.SetupBase
    {
        LostPropertyBO objMaster = null;
        public frmLostPropertyList()
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new LostPropertyBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

        }

        void frmCompanyInvoiceList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.InitializeForm("frmLostProperty");

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage("You do not have Lost Property Form Rights");
                this.Close();

            }


            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


            LoadLostList();

            grdLister.AddEditColumn();

            if (this.CanDelete)
            {
                grdLister.AddDeleteColumn();
                grdLister.Columns["btnDelete"].Width = 70;
            }


            grdLister.Columns["Id"].IsVisible = false;

            grdLister.Columns["LPNo"].HeaderText = "Lost No";
            grdLister.Columns["LPNo"].Width = 80;

            grdLister.Columns["LPDate"].HeaderText = "Report Date";
            grdLister.Columns["LPDate"].Width = 80;

            grdLister.Columns["LostDate"].HeaderText = "Lost Date";
            grdLister.Columns["LostDate"].Width = 80;

            grdLister.Columns["CustomerName"].HeaderText = "Customer Name";
            grdLister.Columns["CustomerName"].Width = 90;

            grdLister.Columns["ItemDecs"].HeaderText = "Item Description";
            grdLister.Columns["ItemDecs"].Width = 120;


            (grdLister.Columns["LPDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["LPDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

            (grdLister.Columns["LostDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["LostDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";



            grdLister.Columns["btnEdit"].Width = 70;


            UI.GridFunctions.SetFilter(grdLister);



        }

        private void LoadLostList()
        {
            try
            {
                string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                string col = ddlColumns.Text.ToStr().Trim().ToLower();

                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;


                bool col_lost = false;
                bool col_Decs = false;
                bool col_Name = false;
                if (col == "lost no")
                {
                    col_lost = true;
                }
                if (col == "item description")
                {
                    col_Decs = true;
                }
                if (col == "customer")
                {
                    col_Name = true;
                }

                var query = from a in General.GetQueryable<LostProperty>(null)
                            where
                       (col_lost && (a.LPNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                       || (col_Decs && (a.Complaint.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                       || (col_Name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            select new
                            {
                                Id = a.Id,
                                LPNo = a.LPNo,
                                LPDate = a.ReportedDate,
                                LostDate = a.LostDate,
                                CustomerName = a.CustomerName,
                                ItemDecs = a.Complaint
                            };
                grdLister.DataSource = query.ToList();
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }

        }

        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
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
            try
            {


                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    long id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();

                    ShowForm(id);
                }
                else
                {
                    ENUtils.ShowMessage("Please select a record");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        void ShowForm(long id)
        {


            frmLostProperty frm = new frmLostProperty();
            frm.OnDisplayRecord(id);


            //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmLoseProperty1");

            //if (doc != null)
            //{
            //    doc.Close();
            //}

            //MainMenuForm.MainMenuFrm.ShowForm(frm);
            frm.ShowDialog();
            frm.Dispose();

        }

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new LostPropertyBO();

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


        public override void RefreshData()
        {
            PopulateData();
        }



        public override void PopulateData()
        {

            LoadLostList();

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadLostList();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PopulateData();

            }
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void btnShowAll_Click_1(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            PopulateData();
        }

        private void btnLost_Click(object sender, EventArgs e)
        {
            frmLostProperty frm = new frmLostProperty();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void frmLostPropertyList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }
        RadGridViewExcelExporter exporter = null;
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    radGridView1.Columns.Clear();
                    radGridView1.Rows.Clear();
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("RefNo", "RefNo"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("LostDate", "LostDate"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Complaint", "Complaint"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Disposition", "Disposition"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Result", "Result"));
                    string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                    string col = ddlColumns.Text.ToStr().Trim().ToLower();

                    if (searchTxt.Length < 3)
                        searchTxt = string.Empty;


                    bool col_lost = false;
                    bool col_Decs = false;
                    bool col_Name = false;
                    if (col == "lost no")
                    {
                        col_lost = true;
                    }
                    if (col == "item description")
                    {
                        col_Decs = true;
                    }
                    if (col == "customer")
                    {
                        col_Name = true;
                    }

                    var list = (from a in General.GetQueryable<LostProperty>(null)
                                where
                           (col_lost && (a.LPNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                           || (col_Decs && (a.Complaint.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                           || (col_Name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                                select new
                                {
                                    //Id = a.Id,
                                    RefNo = a.LPNo,
                                    LPDate = " " + string.Format(" {0:dd/MM/yyyy} ", a.ReportedDate) + "  ",
                                    LostDate = " " + string.Format(" {0:dd/MM/yyyy} ", a.LostDate) + "  ",
                                    Complaint=a.Complaint,
                                    Disposition=a.Disposition,
                                    Result=a.Result
                                    //CustomerName = a.CustomerName,
                                    //ItemDecs = a.Complaint
                                }).ToList();
                    // this.radGridView1.DataSource = list;

                    radGridView1.RowCount = list.Count;
                    for (int i = 0; i < list.Count; i++)
                    {
                        radGridView1.Rows[i].Cells["RefNo"].Value = list[i].RefNo;
                        radGridView1.Rows[i].Cells["LostDate"].Value = list[i].LostDate;
                        radGridView1.Rows[i].Cells["Complaint"].Value = list[i].Complaint;
                        radGridView1.Rows[i].Cells["Disposition"].Value = list[i].Disposition;
                        radGridView1.Rows[i].Cells["Result"].Value = list[i].Result;
                    }
                    exporter = new RadGridViewExcelExporter();
                    BackgroundWorker worker = new BackgroundWorker();
                    //worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    //worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    worker.RunWorkerAsync(saveFileDialog1.FileName);
                    exporter.Progress += new ProgressHandler(exportProgress);

                    this.btnExport.Enabled = false;
                }
            }

            catch (Exception ex)
            {

            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnExport.Enabled = true;
            this.radProgressBar1.Value1 = 0;
            //   RadMessageBox.SetThemeName(this.grdLister.ThemeName);   
            //RadMessageBox.Show("The data in the grid was exported successfully.", "Export to Excel");  
            ENUtils.ShowMessage("Export successfully.");
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.IsDisposed)
                {
                    e.Cancel = true;
                    return;
                }
                exporter.Export(this.radGridView1, (String)e.Argument, "Lost Property");
            }
            catch (Exception ex)
            { }
        }

        private void exportProgress(object sender, ProgressEventArgs e)
        {
            if (this.IsDisposed)
                return;
            // Call InvokeRequired to check if thread needs marshalling, to access properly the UI thread.
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


    }
}

