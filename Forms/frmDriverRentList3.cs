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
    public partial class frmDriverRentList3 : UI.SetupBase
    {
        DriverRentBO objMaster = null;

        RadDropDownMenu AddRentItems = null;

        public frmDriverRentList3()
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new DriverRentBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);


            

                // for menus
                grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);

                AddRentItems = new RadDropDownMenu();
                AddRentItems.BackColor = Color.Orange;

                RadMenuItem AddRentItems1 = new RadMenuItem("Add Driver Rent");
                AddRentItems1.ForeColor = Color.DarkBlue;
                AddRentItems1.BackColor = Color.Orange;
                AddRentItems1.Font = new Font("Tahoma", 10, FontStyle.Bold);
                AddRentItems1.Click += new EventHandler(AddRentItems1_Click);
                AddRentItems.Items.Add(AddRentItems1);


                RadMenuItem AddCommisionItems3 = new RadMenuItem("Edit Last Statement");
                AddCommisionItems3.ForeColor = Color.DarkBlue;
                AddCommisionItems3.BackColor = Color.Orange;
                AddCommisionItems3.Font = new Font("Tahoma", 10, FontStyle.Bold);
                AddCommisionItems3.Click += new EventHandler(AddCommisionItems3_Click);
            AddRentItems.Items.Add(AddCommisionItems3);

            RadMenuItem AddRentItems2 = new RadMenuItem("Driver Rent List");
                AddRentItems2.ForeColor = Color.DarkBlue;
                AddRentItems2.BackColor = Color.Orange;
                AddRentItems2.Font = new Font("Tahoma", 10, FontStyle.Bold);
                AddRentItems2.Click += new EventHandler(AddRentItems2_Click);
                AddRentItems.Items.Add(AddRentItems2);
           
        }

        void AddCommisionItems3_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    long? lastTransId = 0;
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        lastTransId = db.DriverRents.Where(c => c.DriverId == id).Select(c => c.Id).OrderByDescending(c => c).FirstOrDefault();

                    }

                    if (lastTransId.ToLong() == 0)
                    {
                        MessageBox.Show("no record found");
                    }
                    else
                    {

                       
                            frmDriverRentDebitCredit3 frm = new frmDriverRentDebitCredit3();
                            frm.OnDisplayRecord(lastTransId);


                            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRentDebitCredit31");

                            if (doc != null)
                            {
                                doc.Close();
                            }

                            MainMenuForm.MainMenuFrm.ShowForm(frm);
                        

                       
                    }
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
                    e.ContextMenu = AddRentItems;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        void AddRentItems1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    //if (AppVars.listUserRights.Count(c => c.formName == "frmDriverRent") > 0)
                    //{
                    //    frmDriverRent frm = new frmDriverRent(id);

                    //    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRent1");

                    //    if (doc != null)
                    //    {
                    //        doc.Close();
                    //    }

                    //    MainMenuForm.MainMenuFrm.ShowForm(frm);

                    //}
                    //else
                    //{
                        if (AppVars.listUserRights.Count(c => c.formName == "frmDriverRentDebitCredit3") > 0)
                        {
                            frmDriverRentDebitCredit3 frm = new frmDriverRentDebitCredit3(id);

                            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRentDebitCredit21");

                            if (doc != null)
                            {
                                doc.Close();
                            }

                            MainMenuForm.MainMenuFrm.ShowForm(frm);
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void AddRentItems2_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    ShowForm(id);
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


            try
            {

                this.InitializeForm("frmDriverRentDebitCredit3");

            }
            catch
            {


            }
         
                
                LoadRentList();

            //grdLister.AddEditColumn();

            if (this.CanDelete)
            {
                //grdLister.AddDeleteColumn();
                //grdLister.Columns["btnDelete"].Width = 70;
            }


            grdLister.Columns["Id"].IsVisible = false;
            grdLister.Columns["TotalRentCount"].IsVisible = false;



            grdLister.Columns["DriverNo"].HeaderText = "Driver No";
            grdLister.Columns["DriverNo"].Width = 50;

            grdLister.Columns["DriverName"].HeaderText = "Driver Name";
            grdLister.Columns["DriverName"].Width = 130;


            grdLister.Columns["MobileNo"].Width = 80;
            grdLister.Columns["MobileNo"].HeaderText = "Mobile No";


            grdLister.Columns["TransDate"].Width = 50;
            grdLister.Columns["TransDate"].HeaderText = "Last Statement Date";
            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

            grdLister.Columns["TotalRentCount"].Width = 50;
            grdLister.Columns["TotalRentCount"].HeaderText = "Total Rent Count";

            UI.GridFunctions.SetFilter(grdLister);

        }


        private void LoadRentList()
        {
            try
            {
               


                using (TaxiDataContext db = new TaxiDataContext())
                {

                    //var query = (from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true && c.DriverTypeId == 1).AsEnumerable()

                    //             where

                    //        (col_No && (a.DriverNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                    //        || (col_DriverName && (a.DriverName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                    //             select new
                    //             {
                    //                 Id = a.Id,
                    //                 No = a.DriverNo,
                    //                 Name = a.DriverName,
                    //                 MobileNo = a.MobileNo,
                    //                 LastDate = a.DriverRents.Count > 0 ? a.DriverRents[a.DriverRents.Count - 1].TransDate : null,
                    //                 TotalRentCount = a.DriverRents.Count > 0 ? Convert.ToSingle(a.DriverRents.Count).ToStr() : ""
                    //             }).ToList();


                    
                  //  grdLister.DataSource = query.AsEnumerable().OrderBy(item => item.No, new NaturalSortComparer<string>()).ToList();
                    grdLister.DataSource =db.stp_getdriverrentlist().ToList().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>()).ToList();
                }
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
            //else if (gridCell.ColumnInfo.Name.ToLower() == "btnedit")
            //{
            //    ViewDetailForm();
            //}//
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
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();

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

        frmRentList3 frm = null;
        void ShowForm(int id)
        {

            try
            {
                if (frm != null)
                {

                    try
                    {
                        frm.Close();
                        frm.Dispose();


                    }
                    catch
                    {


                    }

                }

                frm = new frmRentList3(id);
                frm.Show();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
           

            //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRent1");

            //if (doc != null)
            //{
            //    doc.Close();
            //}

            //MainMenuForm.MainMenuFrm.ShowForm(frm);

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

      
   

        private void btnLastStatement_Click(object sender, EventArgs e)
        {
            frmDriverRentLastStatement3 frm = new frmDriverRentLastStatement3();
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();


            frm.Dispose();

            GC.Collect();
        }




    }
}

