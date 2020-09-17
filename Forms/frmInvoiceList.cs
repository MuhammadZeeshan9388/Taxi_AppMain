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
    public partial class frmInvoiceList : UI.SetupBase
    {
       // DriverCommisionBO objMaster = null;
        RadDropDownMenu AddCommisionItems = null;

        public frmInvoiceList()
        {
            InitializeComponent();
            grdLister.ShowRowHeaderColumn = false;
            grdLister.AllowEditRow = false;
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            //objMaster = new DriverCommisionBO();

            //this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

            // for menus
            grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);

            AddCommisionItems = new RadDropDownMenu();
            AddCommisionItems.BackColor = Color.Orange;

            RadMenuItem AddCommisionItems1 = new RadMenuItem("Add Company Invoice");
            AddCommisionItems1.ForeColor = Color.Green;
            AddCommisionItems1.BackColor = Color.Green;
            AddCommisionItems1.Font = new Font("Tahoma", 10, FontStyle.Bold);
            AddCommisionItems1.Click += new EventHandler(AddCommisionItems1_Click);
            AddCommisionItems.Items.Add(AddCommisionItems1);

            RadMenuItem AddCommisionItems2 = new RadMenuItem("Company Invoice List");
            AddCommisionItems2.ForeColor = Color.Red;
            AddCommisionItems2.BackColor = Color.Red;
            AddCommisionItems2.Font = new Font("Tahoma", 10, FontStyle.Bold);
            AddCommisionItems2.Click += new EventHandler(AddCommisionItems2_Click);
            AddCommisionItems.Items.Add(AddCommisionItems2);


            RadMenuItem AddCommisionItems4 = new RadMenuItem("UnPaid Invoice List");
            AddCommisionItems4.ForeColor = Color.DarkBlue;
            AddCommisionItems4.BackColor = Color.Orange;
            AddCommisionItems4.Font = new Font("Tahoma", 10, FontStyle.Bold);
            AddCommisionItems4.Click += AddCommisionItems4_Click;
            AddCommisionItems.Items.Add(AddCommisionItems4);

            RadMenuItem AddCommisionItems3 = new RadMenuItem("Paid Invoice List");
            AddCommisionItems3.ForeColor = Color.DarkBlue;
            AddCommisionItems3.BackColor = Color.Orange;
            AddCommisionItems3.Font = new Font("Tahoma", 10, FontStyle.Bold);
            AddCommisionItems3.Click += AddCommisionItems3_Click;
            AddCommisionItems.Items.Add(AddCommisionItems3);

            //RadMenuItem AddCommisionItems3 = new RadMenuItem("Driver Commis");
        }
        frmCompanyPendingInvoice frmCompanyPending = null;
        private void AddCommisionItems4_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    if (frmCompanyPending != null)
                    {
                        frmCompanyPending.Close();
                    }
                    frmCompanyPending = new frmCompanyPendingInvoice(id);
                    frmCompanyPending.ControlBox = true;
                    frmCompanyPending.FormBorderStyle = FormBorderStyle.Fixed3D;
                    frmCompanyPending.MaximizeBox = false;
                    frmCompanyPending.Show();

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        frmCompanyInvoicePaymentList frmCompanyInvoicePayment=null;
        private void AddCommisionItems3_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    if (frmCompanyInvoicePayment != null)
                    {
                        frmCompanyInvoicePayment.Close();
                    }
                    frmCompanyInvoicePayment = new frmCompanyInvoicePaymentList(id);


                    frmCompanyInvoicePayment.ControlBox = true;
                    frmCompanyInvoicePayment.FormBorderStyle = FormBorderStyle.Fixed3D;
                    frmCompanyInvoicePayment.MaximizeBox = false;
                    frmCompanyInvoicePayment.Show();

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                if (cell == null)
                    return;

                else if (cell.GridControl.Name == "grdLister")
                {
                    e.ContextMenu = AddCommisionItems;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        void AddCommisionItems1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    frmInvoice frm = new frmInvoice(id);

                    

                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmInvoice1");

                    if (doc != null)
                    {
                        doc.Close();
                    }

                    MainMenuForm.MainMenuFrm.ShowForm(frm);
                   
                    //if (AppVars.listUserRights.Count(c => c.formName == "frmDriverCommision") > 0)
                    //{
                    //    frmDriverCommision frm = new frmDriverCommision(id);

                    //    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommision1");

                    //    if (doc != null)
                    //    {
                    //        doc.Close();
                    //    }

                    //    MainMenuForm.MainMenuFrm.ShowForm(frm);
                    //}
                    //else
                    //{
                    //    frmDriverCommissionDebitCredit frm = new frmDriverCommissionDebitCredit(id);

                    //    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommissionDebitCredit1");

                    //    if (doc != null)
                    //    {
                    //        doc.Close();
                    //    }

                    //    MainMenuForm.MainMenuFrm.ShowForm(frm);
                    //}
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        

        void AddCommisionItems2_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();

                    ShowForm(id);

                    //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmCompanyInvoiceList1");

                    //if (doc != null)
                    //{
                    //    doc.Close();
                    //}

                    //MainMenuForm.MainMenuFrm.ShowForm(frm);


                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void frmCompanyInvoiceList_Shown(object sender, EventArgs e)
        {

            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            
            PopulateData();
            
            grdLister.Columns["Id"].IsVisible = false;

         
            grdLister.Columns["TelephoneNo"].IsVisible = false;

            grdLister.Columns["CompanyName"].HeaderText = "Account Name";
            grdLister.Columns["CompanyName"].Width = 180;

            grdLister.Columns["CompanyCode"].Width =80;
            grdLister.Columns["CompanyCode"].HeaderText = "Company Code";

            grdLister.Columns["Email"].Width = 120;
            grdLister.Columns["Address"].Width = 180;

            grdLister.Columns["LastInvoiceDate"].HeaderText = "Last Invoice Date";
            grdLister.Columns["LastInvoiceDate"].Width = 120;

            grdLister.Columns["TotalInvoices"].HeaderText = "Total Invoices";
            grdLister.Columns["TotalInvoices"].Width = 100;


            (grdLister.Columns["LastInvoiceDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["LastInvoiceDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

            // grdLister.Columns["Telephone"].Width = 120;


            UI.GridFunctions.SetFilter(grdLister);

            btnLastStatement.Click += new EventHandler(btnLastStatement_Click);

        }

        void btnLastStatement_Click(object sender, EventArgs e)
        {

            frmCompanyInvoiceLastStatement frm = new frmCompanyInvoiceLastStatement();
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();


            frm.Dispose();
            GC.Collect();
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
                    int id=grdLister.CurrentRow.Cells["Id"].Value.ToInt();

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
        frmCompanyInvoiceList frm = null;
        void ShowForm(int id)
        {
            if (frm != null)
            {
                frm.Close();
            }
            frm = new frmCompanyInvoiceList(id);
            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.Show();

            //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmCompanyInvoiceList1");

            //if (doc != null)
            //{
            //    doc.Close();
            //}

            //MainMenuForm.MainMenuFrm.ShowForm(frm);

            //frmCompanyInvoiceList frm = new frmCompanyInvoiceList(id);
            //frm.Show();

            //frmDriverCommision frm = new frmDriverCommision();
            //frm.OnDisplayRecord(id);


            //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommision1");

            //if (doc != null)
            //{
            //    doc.Close();
            //}

            //MainMenuForm.MainMenuFrm.ShowForm(frm);

        }

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            //if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            //{

            //    objMaster = new DriverCommisionBO();

            //    try
            //    {

            //        objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());

            //        string Transaction = grdLister.CurrentRow.Cells["TransNo"].Value.ToStr();
            //        int DriverId = grdLister.CurrentRow.Cells["DriverID"].Value.ToInt();
                    
            //        var query = General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

            //        if (query != null)
            //        {
            //            string Transno = query.TransNo.ToStr();

            //            if (Transno == Transaction)
            //            {
            //                objMaster.Delete(objMaster.Current);
            //            }
            //            else
            //            {
            //                ENUtils.ShowMessage("You Can not delete a record..");
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        if (objMaster.Errors.Count > 0)
            //            ENUtils.ShowMessage(objMaster.ShowErrors());
            //        else
            //        {
            //            ENUtils.ShowMessage(ex.Message);

            //        }
            //        e.Cancel = true;

            //    }
            //}
        }







        public override void RefreshData()
        {
            PopulateData();
        }



        public override void PopulateData()
        {

            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_GetCompanyWithLastInvoice().ToList();



                    grdLister.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }

      



    }
}

