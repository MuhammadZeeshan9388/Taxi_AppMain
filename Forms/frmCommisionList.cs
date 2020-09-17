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

namespace Taxi_AppMain
{
    public partial class frmCommisionList : UI.SetupBase
    {
        DriverCommisionBO objMaster = null;
        int DriverId = 0;
        public frmCommisionList()
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new DriverCommisionBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            
        }

        public frmCommisionList(int id)
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new DriverCommisionBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            DriverId = id;
        }

        private void AddCommandColumn(RadGridView grid, string colName, string caption)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 60;

            col.Name = colName;
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = caption;
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

        void frmCompanyInvoiceList_Shown(object sender, EventArgs e)
        {

            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;



            if (AppVars.listUserRights.Count(c => c.formName == "frmDriverCommision") > 0)
            {
                this.InitializeForm("frmDriverCommision");
            }
            else
            {

                this.InitializeForm("frmDriverCommissionDebitCredit");
            }
            LoadCommisionList();


            AddCommandColumn(grdLister, "btnEdit", "Edit");

      //      grdLister.AddEditColumn();

            if (this.CanDelete)
            {
                AddCommandColumn(grdLister, "btnDelete", "Delete");

            //    grdLister.AddDeleteColumn();
                grdLister.Columns["btnDelete"].Width = 70;
            }


            grdLister.Columns["Id"].IsVisible = false;
            grdLister.Columns["DriverID"].IsVisible = false;
            

            grdLister.Columns["TransNo"].HeaderText = "Transaction No";
            grdLister.Columns["TransNo"].Width = 70;

            grdLister.Columns["TransDate"].HeaderText = "Transaction Date";
            grdLister.Columns["TransDate"].Width = 80;

            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";





            grdLister.Columns["Owed"].Width = 90;
            grdLister.Columns["Owed"].HeaderText = "Driver Owed";
            
            grdLister.Columns["DriverNo"].Width = 50;
            grdLister.Columns["Balance"].Width = 60;

            grdLister.Columns["TransactionFor"].Width = 80;
            grdLister.Columns["TransactionFor"].HeaderText = "Transaction For";

            grdLister.Columns["Driver"].Width = 120;

            grdLister.Columns["btnEdit"].Width = 70;
       

            UI.GridFunctions.SetFilter(grdLister);

        }

        private void LoadCommisionList()
        {
            try
            {
                string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                string col = ddlColumns.Text.ToStr().Trim().ToLower();

                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;


               
                var query = from a in General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId)
                            select new
                            {
                                Id = a.Id,
                                TransNo = a.TransNo,
                                TransDate = a.TransDate,
                                DriverNo = a.Fleet_Driver.DriverNo,
                                Driver = a.Fleet_Driver.DriverName,
                                DriverID = a.DriverId,
                                Owed = a.DriverOwed,
                                Balance = a.Balance,
                                TransactionFor = a.TransFor
                                //JobsTotal = a.JobsTotal
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
                    string Transaction = grdLister.CurrentRow.Cells["TransNo"].Value.ToStr();
                    int DriverId = grdLister.CurrentRow.Cells["DriverID"].Value.ToInt();

                    var query = General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

                    if (query != null)
                    {
                        string Transno = query.TransNo.ToStr();

                        if (Transno == Transaction)
                        {
                            RadGridView grid = gridCell.GridControl;
                            grid.CurrentRow.Delete();   
                        }
                        else
                        {
                            ENUtils.ShowMessage("You Can not delete a record..");
                        }
                    }
                    
                    
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
                    long id=grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                    if (AppVars.listUserRights.Count(c => c.formName == "frmDriverCommissionDebitCredit") > 0)
                    {
                        ShowDriverCommissionDebitCredit(id);
                    }
                    else
                    {
                        ShowForm(id);
                    }
                  
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


            frmDriverCommision frm = new frmDriverCommision();
            frm.OnDisplayRecord(id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommision1");

            if (doc != null)
            {
                doc.Close();
            }

            MainMenuForm.MainMenuFrm.ShowForm(frm);

        }
       private void ShowDriverCommissionDebitCredit(long Id)
        {
            frmDriverCommissionDebitCredit frm = new frmDriverCommissionDebitCredit();
            frm.OnDisplayRecord(Id);
            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommissionDebitCredit1");
            if (doc != null)
            {
                doc.Close();
            }
            MainMenuForm.MainMenuFrm.ShowForm(frm);
        }

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new DriverCommisionBO();

                try
                {

                    objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());

                    string Transaction = grdLister.CurrentRow.Cells["TransNo"].Value.ToStr();
                    int DriverId = grdLister.CurrentRow.Cells["DriverID"].Value.ToInt();
                    
                    var query = General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

                    if (query != null)
                    {
                        string Transno = query.TransNo.ToStr();

                        if (Transno == Transaction)
                        {
                            objMaster.Delete(objMaster.Current);
                        }
                        else
                        {
                            ENUtils.ShowMessage("You Can not delete a record..");
                        }
                    }
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

            LoadCommisionList();

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadCommisionList();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

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




    }
}

