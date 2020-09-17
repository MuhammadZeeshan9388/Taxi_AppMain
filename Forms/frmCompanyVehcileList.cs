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
    public partial class frmCompanyVehcileList : UI.SetupBase
    {
        CompanyVehcileBO objMaster = null;
        public frmCompanyVehcileList()
        {
         
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new CompanyVehcileBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyVehcileList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

        }

        void frmCompanyVehcileList_Shown(object sender, EventArgs e)
        {
            try
            {
                this.InitializeForm("frmCompanyVehcile");

            }
            catch
            {

            }
                grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            LoadVehicleList();

            grdLister.AddEditColumn();

            if (this.CanDelete)
            {
                grdLister.AddDeleteColumn();
                grdLister.Columns["btnDelete"].Width = 70;
            }

            grdLister.Columns["Id"].IsVisible = false;
            grdLister.Columns["VehicleNo"].HeaderText = "Vehicle No";
            grdLister.Columns["VehicleID"].HeaderText = "Vehicle ID";
            grdLister.Columns["Vehicle"].HeaderText = "Vehicle Type";
            grdLister.Columns["btnEdit"].Width = 70;


            (grdLister.Columns["Mot"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["Mot"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";


            grdLister.Columns["RoadTax"].HeaderText = "Road Tax";
            (grdLister.Columns["RoadTax"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["RoadTax"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";



            (grdLister.Columns["Insurance"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["Insurance"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";



            (grdLister.Columns["Plate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["Plate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";



            UI.GridFunctions.SetFilter(grdLister);

        }

        private void LoadVehicleList()
        {
            try
            {
                string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
                string col = ddlColumns.Text.ToStr().Trim().ToLower();

                if (searchTxt.Length < 3)
                    searchTxt = string.Empty;


                bool col_VehNo = false;
                bool col_Qwner = false;
                bool col_Model = false;
                bool col_vehType = false;
                if (col == "vehicle no")
                {
                    col_VehNo = true;
                }
                if (col == "owner")
                {
                    col_Qwner = true;
                }
                if (col == "model no")
                {
                    col_Model = true;
                }
                if (col == "vehicle type")
                {
                    col_vehType = true;
                }

                var query = from a in General.GetQueryable<Fleet_Master>(null)
                            where
                       (col_VehNo && (a.VehicleNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                       || (col_Qwner && (a.VehicleOwner.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                       || (col_Model && (a.VehicleModel.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                       ||(col_vehType && (a.Fleet_VehicleType.VehicleType.ToLower().Contains(searchTxt) || searchTxt == string.Empty))

                            select new
                            {
                                Id = a.Id,
                                VehicleID=a.VehicleID,
                                PlateNo = a.Plateno,
                                VehicleNo = a.VehicleNo,
                                Vehicle = a.Fleet_VehicleType.VehicleType,
                                Owner = a.VehicleOwner,
                                Make = a.VehicleMake,
                                Model = a.VehicleModel,
                                Mot= a.MOTExpiryDate,
                                RoadTax=a.RoadTaxExpDate,
                                Insurance=a.InsuranceExpiry,
                                Plate=a.PLateExpiryDate
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
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Vehicle ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
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
            frmCompanyVehcile frm = new frmCompanyVehcile();
            frm.OnDisplayRecord(id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmCompanyVehcile1");

            if (doc != null)
            {
                doc.Close();
            }

            frm.ShowDialog();
            frm.Dispose();

        }

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new CompanyVehcileBO();

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
            LoadVehicleList();
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadVehicleList();
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

        private void btnVehicle_Click(object sender, EventArgs e)
        {
            frmCompanyVehcile frm = new frmCompanyVehcile();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btnShowAll_Click_1(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            PopulateData();

        }
    }
}

