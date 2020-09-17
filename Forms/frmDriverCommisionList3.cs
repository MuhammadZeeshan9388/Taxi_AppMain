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
    public partial class frmDriverCommisionList3 : UI.SetupBase
    {
        DriverCommisionBO objMaster = null;
        RadDropDownMenu AddCommisionItems = null;

        public frmDriverCommisionList3()
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
                    frmDriverCommissionDebitCredit3 frm = new frmDriverCommissionDebitCredit3(id);

                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommissionDebitCredit31");

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
                    frmCommisionList3 frm = new frmCommisionList3(id);
                    frm.Show();
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
                this.InitializeForm("frmDriverCommissionDebitCredit3");
                

            //}
            
            LoadCommisionList();

            
            grdLister.Columns["Id"].IsVisible = false;



            grdLister.Columns["No"].HeaderText = "Driver No";
            grdLister.Columns["No"].Width = 50;

            grdLister.Columns["Name"].HeaderText = "Driver Name";
            grdLister.Columns["Name"].Width = 130;


            grdLister.Columns["MobileNo"].Width = 80;
            grdLister.Columns["MobileNo"].HeaderText = "Mobile No";


            grdLister.Columns["LastDate"].Width = 50;
            grdLister.Columns["LastDate"].HeaderText = "Last Commision Paid On";
            (grdLister.Columns["LastDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["LastDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

       

            UI.GridFunctions.SetFilter(grdLister);

            btnLastStatement.Click += new EventHandler(btnLastStatement_Click);

        }

        void btnLastStatement_Click(object sender, EventArgs e)
        {

            frmDriverCommissionLastStatement3 frm = new frmDriverCommissionLastStatement3();
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
                string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                string col = ddlColumns.Text.ToStr().Trim().ToLower();

                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;


                bool col_No = false;
                bool col_DriverName = false;
                if (col == "driver no")
                {
                    col_No = true;
                }
                if (col == "driver name")
                {
                    col_DriverName = true;
                }

                var query = from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true && c.DriverTypeId == 2).AsEnumerable()

                            where

                       (col_No && (a.DriverNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                       || (col_DriverName && (a.DriverName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                            select new
                            {
                                Id = a.Id,
                                No = a.DriverNo,
                                Name = a.DriverName,
                                MobileNo = a.MobileNo,
                                LastDate = a.Fleet_DriverCommisions.Count > 0 ? a.Fleet_DriverCommisions[a.Fleet_DriverCommisions.Count - 1].TransDate : null
                            };


                grdLister.DataSource = query.AsEnumerable().OrderBy(item => item.No, new NaturalSortComparer<string>()).ToList();
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
            frmCommisionList3 frm = new frmCommisionList3(id);
            frm.Show();

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

