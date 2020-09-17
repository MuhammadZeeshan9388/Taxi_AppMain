using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Taxi_AppMain
{
    public partial class frmTrashBooking : UI.SetupBase
    {
         BookingBO objMaster;
        
        public frmTrashBooking()
        {
            InitializeComponent();
            InitializeConstructor();

            this.Load += new EventHandler(frmLocations_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new BookingBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmTrashBooking_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
        }

        void frmTrashBooking_Shown(object sender, EventArgs e)
        {

            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            CanDelete = true;
            if (this.CanDelete)
            {
                grdLister.AddDeleteColumn();
                grdLister.Columns["btnDelete"].Width = 70;
                AddCommandColumn(grdLister, "btnCancel", "Post Regular Booking");
            }
            UI.GridFunctions.SetFilter(grdLister);

            txtSearch.Focus();
        }
        private void AddCommandColumn(RadGridView grid, string colName, string caption)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 100;
            col.Name = colName;
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = caption;
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }
        
        void frmLocations_Load(object sender, EventArgs e)
        {
            PopulateData();
            grdLister.Columns["ID"].IsVisible = false;
            grdLister.Columns["RefNo"].Width = 50;
            grdLister.Columns["RefNo"].HeaderText = "Ref #";
            grdLister.Columns["PickUpDate"].HeaderText = "PickUp Date";
            grdLister.Columns["PickUpDate"].Width = 110;
            grdLister.Columns["CustomerName"].HeaderText = "Customer Name";
            grdLister.Columns["CustomerName"].Width = 100;
            grdLister.Columns["FromAddress"].HeaderText = "PickUp Point";
            grdLister.Columns["FromAddress"].Width = 220;
            grdLister.Columns["ToAddress"].HeaderText = "Destination";
            grdLister.Columns["ToAddress"].Width = 220;
            grdLister.Columns["Fares"].HeaderText = "Fare(£)";
            grdLister.Columns["Fares"].Width = 70;

        }
        protected override void OnClosed(EventArgs e)
        {
            General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
        }
        private void InitializeConstructor()
        {
            this.Load += new EventHandler(frmLocations_Load);
            objMaster = new BookingBO();
            this.SetProperties((INavigation)objMaster);
            this.Shown += new EventHandler(frmLocations_Shown);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new BookingBO();

            
            

        }
        private void FocusOnSave()
        {
            //btnSaveNew.ButtonElement.Focus();
        }
        void frmLocations_Shown(object sender, EventArgs e)
        {

        }

        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {

        }

        // Show Data
        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {

                //ViewDetailForm();
                objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());

                DisplayRecord();
            }
            catch (Exception ex)
            {
            }
        }


        private void LoadLocationsList()
        {
            string searchTxt = txtSearch.Text.ToStr().ToLower().Trim();
            string col = ddlColumns.Text.ToStr().Trim().ToLower();

            if (searchTxt.Length < 3)
                searchTxt = string.Empty;

            bool col_ref = false;
            bool col_name = false;
            bool col_date = false;

            if (col == "refrence no")
            {
                col_ref = true;
            }
            if (col == "customer name")
            {
                col_name = true;
            }
            if (col == "pickup date")
            {
                col_date = true;
            }

            var data1 = General.GetQueryable<Booking_Trash>(null);

            var query = from a in data1

                        where
                        (col_ref && (a.BookingNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                        || (col_name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                        || (col_date && (a.PickupDateTime.ToStr().Contains(searchTxt) || searchTxt == string.Empty))

                        select new
                        {
                            ID = a.Id,
                            RefNo = a.BookingNo,
                            PickUpDate = a.PickupDateTime,
                            CustomerName = a.CustomerName,
                            FromAddress = a.FromAddress,
                            ToAddress = a.ToAddress,
                            Fares = a.FareRate,                            

                        };

            grdLister.DataSource = query.OrderByDescending(c=>c.PickUpDate).ToList();
            grdLister.CurrentRow = null;

        }

        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btncancel")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to Post Regular Booking ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    // booking go to regular Bookings

                    try
                    {
                        long jobId = gridCell.GridControl.CurrentRow.Cells["Id"].Value.ToLong();
                        (new TaxiDataContext()).stp_TrashBooking(1, jobId);
                    }
                    catch (Exception ex)
                    {
                        ENUtils.ShowMessage(ex.Message);
                    }

                    RadGridView grid = gridCell.GridControl;
                    grid.CurrentRow.Delete();
                }
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Trash Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {                    
                    long jobId = gridCell.GridControl.CurrentRow.Cells["Id"].Value.ToLong();
                    (new TaxiDataContext()).stp_TrashBooking(2, jobId);
                    RadGridView grid = gridCell.GridControl;
                    grid.CurrentRow.Delete();
                }
            }

            
        }

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new BookingBO();

                try
                {

                    objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                    if (objMaster.Current != null)
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

        public override void PopulateData()
        {

            LoadLocationsList();
        }

        private void ddlDriver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadLocationsList();
        }

        private void grdLister_CellDoubleClick_1(object sender, GridViewCellEventArgs e)
        {
           ViewDetailForm(e.Row);
        }
        private void ViewDetailForm(GridViewRowInfo row)
        {
            try
            {

                if (row != null && row is GridViewDataRowInfo)
                {
                    frmBooking_Trash frm = new frmBooking_Trash(row.Cells["Id"].Value.ToInt());
                    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                    frm.Dispose();
                    //frm.ShowDialog();
                }
                else
                {
                    ENUtils.ShowMessage("Please select a record");
                }
            }
            catch (Exception ex)
            {


            }
        }

        private void ShowBookingForm(int id, bool showOnDialog)
        {
            General.ShowBookingForm(id, showOnDialog, "", "", Enums.BOOKING_TYPES.LOCAL);


        }
    }
}