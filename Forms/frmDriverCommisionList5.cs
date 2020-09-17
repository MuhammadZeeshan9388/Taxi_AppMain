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
    public partial class frmDriverCommisionList5 : UI.SetupBase
    {
        DriverCommisionBO objMaster = null;
        RadDropDownMenu AddCommisionItems = null;

        public frmDriverCommisionList5()
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new DriverCommisionBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

            // for menus
            grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);

            AddCommisionItems = new RadDropDownMenu();
            AddCommisionItems.BackColor = Color.Orange;

            RadMenuItem AddCommisionItems1 = new RadMenuItem("Add Driver Commision");
            AddCommisionItems1.ForeColor = Color.DarkBlue;
            AddCommisionItems1.BackColor = Color.Orange;
            AddCommisionItems1.Font = new Font("Tahoma", 10, FontStyle.Bold);
            AddCommisionItems1.Click += new EventHandler(AddCommisionItems1_Click);
            AddCommisionItems.Items.Add(AddCommisionItems1);


            RadMenuItem AddCommisionItems3 = new RadMenuItem("Edit Last Statement");
            AddCommisionItems3.ForeColor = Color.DarkBlue;
            AddCommisionItems3.BackColor = Color.Orange;
            AddCommisionItems3.Font = new Font("Tahoma", 10, FontStyle.Bold);
            AddCommisionItems3.Click += new EventHandler(AddCommisionItems3_Click);
            AddCommisionItems.Items.Add(AddCommisionItems3);

            RadMenuItem AddCommisionItems2 = new RadMenuItem("Driver Commision List");
            AddCommisionItems2.ForeColor = Color.DarkBlue;
            AddCommisionItems2.BackColor = Color.Orange;
            AddCommisionItems2.Font = new Font("Tahoma", 10, FontStyle.Bold);
            AddCommisionItems2.Click += new EventHandler(AddCommisionItems2_Click);
            AddCommisionItems.Items.Add(AddCommisionItems2);
            //RadMenuItem AddCommisionItems3 = new RadMenuItem("Driver Commis");
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
                    frmDriverCommissionDebitCredit5 frm = new frmDriverCommissionDebitCredit5(id);

                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommissionDebitCredit51");

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

        frmCommisionList5 frm = null;

        void AddCommisionItems2_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();


                    ShowCommissionDetailForm(id);
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void ShowCommissionDetailForm(int Id)
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

                frm = new frmCommisionList5(Id);
                frm.Show();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

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
                        lastTransId= db.Fleet_DriverCommisions.Where(c=>c.DriverId==id).Select(c => c.Id).OrderByDescending(c => c).FirstOrDefault();

                    }

                    if (lastTransId.ToLong() == 0)
                    {
                        MessageBox.Show("no record found");
                    }
                    else
                    {

                        frmDriverCommissionDebitCredit5 frm = new frmDriverCommissionDebitCredit5();
                        frm.OnDisplayRecord(lastTransId);
                        DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommissionDebitCredit51");
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





        void frmCompanyInvoiceList_Shown(object sender, EventArgs e)
        {

            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


            //if (AppVars.listUserRights.Count(c => c.formName == "frmDriverCommision") > 0)
            //{
            //    this.InitializeForm("frmDriverCommision");
            //}
            //else
            //{
                this.InitializeForm("frmDriverCommissionDebitCredit5");
                

            //}
            
            LoadCommisionList();

            
            grdLister.Columns["Id"].IsVisible = false;
            grdLister.Columns["TotalRentCount"].IsVisible = false;



            grdLister.Columns["DriverNo"].HeaderText = "Driver No";
            grdLister.Columns["DriverNo"].Width = 50;

            grdLister.Columns["DriverName"].HeaderText = "Driver Name";
            grdLister.Columns["DriverName"].Width = 130;


            grdLister.Columns["MobileNo"].Width = 80;
            grdLister.Columns["MobileNo"].HeaderText = "Mobile No";


            grdLister.Columns["TransDate"].HeaderText = "Last Commision Paid On";
            grdLister.Columns["TransDate"].Width = 50;
          //  grdLister.Columns["LastDate"].HeaderText = "Last Commision Paid On";
            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["TransDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

       

            UI.GridFunctions.SetFilter(grdLister);

            btnLastStatement.Click += new EventHandler(btnLastStatement_Click);

        }

        void btnLastStatement_Click(object sender, EventArgs e)
        {

            frmDriverCommissionLastStatement5 frm = new frmDriverCommissionLastStatement5();
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();


            frm.Dispose();
            GC.Collect();
        }

        private void LoadCommisionList()
        {
            try
            {
              

               

                    using (TaxiDataContext db = new TaxiDataContext())
                {


                    //var query = from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true && c.DriverTypeId == 2).AsEnumerable()

                    //            where

                    //       (col_No && (a.DriverNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                    //       || (col_DriverName && (a.DriverName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                    //            select new
                    //            {
                    //                Id = a.Id,
                    //                No = a.DriverNo,
                    //                Name = a.DriverName,
                    //                MobileNo = a.MobileNo,
                    //                LastDate = a.Fleet_DriverCommisions.Count > 0 ? a.Fleet_DriverCommisions[a.Fleet_DriverCommisions.Count - 1].TransDate : null
                    //            };


                    //grdLister.DataSource = query.AsEnumerable().OrderBy(item => item.No, new NaturalSortComparer<string>()).ToList();

                    //var query = from a in db.stp_getdrivercommlist()




                    //            select new
                    //            {
                    //                Id = a.Id,
                    //                No = a.DriverNo,
                    //                Name = a.DriverName,
                    //                MobileNo = a.MobileNo,
                    //                LastDate = a.l a.Fleet_DriverCommisions.Count > 0 ? a.Fleet_DriverCommisions[a.Fleet_DriverCommisions.Count - 1].TransDate : null
                    //            };

                   
                       

                    grdLister.DataSource = db.stp_getdrivercommlist().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>()).ToList();

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
        void ShowForm(int id)
        {

            ShowCommissionDetailForm(id);

            

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

      



    }
}

