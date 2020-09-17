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
    public partial class frmRentList : UI.SetupBase
    {
        DriverRentBO objMaster = null;
        int DriverId = 0;
        public frmRentList()
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new DriverRentBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

        }

        public frmRentList(int id)
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new DriverRentBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            DriverId = id;
        }

        void frmCompanyInvoiceList_Shown(object sender, EventArgs e)
        {

            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;




            this.InitializeForm("frmDriverRentDebitCredit");
            LoadRentList();
            

            grdLister.AddEditColumn();

            if (this.CanDelete)
            {
                grdLister.AddDeleteColumn();
                grdLister.Columns["btnDelete"].Width = 70;
            }


            grdLister.Columns["Id"].IsVisible = false;
            grdLister.Columns["DriverID"].IsVisible = false;
            

            grdLister.Columns["TransNo"].HeaderText = "Statement No";
            grdLister.Columns["TransNo"].Width = 80;

            grdLister.Columns["TransDate"].HeaderText = "Statement Date";
            grdLister.Columns["TransDate"].Width = 100;

            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";



            grdLister.Columns["From"].Width = 100;

            (grdLister.Columns["From"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["From"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";


            grdLister.Columns["Till"].Width = 100;

            (grdLister.Columns["Till"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["Till"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";


          //  grdLister.Columns["Rent"].Width = 60;
          //  grdLister.Columns["Rent"].HeaderText = "Driver rent";

          //  grdLister.Columns["TransactionFor"].Width = 80;
          //  grdLister.Columns["TransactionFor"].HeaderText = "Transaction For";
            

            grdLister.Columns["Driver"].Width = 100;
           // grdLister.Columns["DriverNo"].Width = 80;
            
            grdLister.Columns["Balance"].Width = 80;

            grdLister.Columns["btnEdit"].Width = 70;
       

            UI.GridFunctions.SetFilter(grdLister);

        }

        private void LoadRentList()
        {
            try
            {
                string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                string col = ddlColumns.Text.ToStr().Trim().ToLower();

                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;


            

                var query = from a in General.GetQueryable<DriverRent>(c => c.DriverId == DriverId)
                            select new
                            {
                                Id = a.Id,
                                Driver = a.Fleet_Driver.DriverNo,                               
                                TransNo = a.TransNo,
                                TransDate = a.TransDate,
                                From=a.FromDate,
                                Till=a.ToDate,

                                DriverID = a.DriverId,
                            
                                Balance = a.Balance,
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

                    var query = General.GetQueryable<DriverRent>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

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
                    //int InvoiceId = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    //invoice_Payment obj = General.GetObject<invoice_Payment>(c => c.invoiceId == InvoiceId);
                    //if (obj == null)
                    //{
                    //    RadGridView grid = gridCell.GridControl;
                    //    grid.CurrentRow.Delete();
                    //}
                    //else
                    //{
                    //    ENUtils.ShowMessage("You Cannot Delete a Record Payment Exits..");
                    //}
                    
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

            //if (AppVars.listUserRights.Count(c => c.formName == "frmDriverRent") > 0)
            //{
            //    frmDriverRent frm = new frmDriverRent();
            //    frm.OnDisplayRecord(id);


            //    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRent1");

            //    if (doc != null)
            //    {
            //        doc.Close();
            //    }

            //    MainMenuForm.MainMenuFrm.ShowForm(frm);
            //}
            //else
            //{
                //frmDriverRentDebitCredit
                if (AppVars.listUserRights.Count(c => c.formName == "frmDriverRentDebitCredit") > 0)
                {
                    frmDriverRentDebitCredit frm = new frmDriverRentDebitCredit();
                    frm.OnDisplayRecord(id);


                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRentDebitCredit1");

                    if (doc != null)
                    {
                        doc.Close();
                    }

                    MainMenuForm.MainMenuFrm.ShowForm(frm);
                } 
            //}

        }

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new DriverRentBO();

                try
                {

                    objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());

                    string Transaction = grdLister.CurrentRow.Cells["TransNo"].Value.ToStr();
                    int DriverId = grdLister.CurrentRow.Cells["DriverID"].Value.ToInt();
                    
                    var query = General.GetQueryable<DriverRent>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

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

            LoadRentList();

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
          //  txtSearch.Text = string.Empty;
            LoadRentList();
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

