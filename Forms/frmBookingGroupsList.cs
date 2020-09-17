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
using Taxi_AppMain.Classes;


namespace Taxi_AppMain
{
    public partial class frmBookingGroupsList  : UI.SetupBase
    {
         BookingGroupBO objMaster;



         public frmBookingGroupsList()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmLocationList_Load);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
             grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
             objMaster = new BookingGroupBO();
           
            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmLocationList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            this.FormClosed += new FormClosedEventHandler(frmBookingGroupsList_FormClosed);

        }

         void frmBookingGroupsList_FormClosed(object sender, FormClosedEventArgs e)
         {
             this.Dispose(true);
         }

       
         void frmLocationList_Shown(object sender, EventArgs e)
         {

           //  grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;




             this.InitializeForm("frmCreateGroup");
             LoadData();

          
             if (this.CanDelete)
             {
                 grdLister.AddDeleteColumn();
                 grdLister.Columns["btnDelete"].Width = 70;
             }                         


             grdLister.Columns["Id"].IsVisible = false;
           

             grdLister.Columns["GroupName"].HeaderText = "Group No";
             grdLister.Columns["GroupName"].Width = 120;

             grdLister.Columns["FlightDeparture"].HeaderText = "Departure Date/Time";
             grdLister.Columns["FlightDeparture"].Width =170;

             grdLister.Columns["Zone"].Width =100;
             grdLister.Columns["Destination"].Width =180;
          //   grdLister.Columns["Drv"].Width =80;

             grdLister.Columns["TotalSeats"].HeaderText ="No of Seats";
             grdLister.Columns["TotalSeats"].Width =102;

             grdLister.Columns["AvailSeats"].HeaderText ="Available Seats";
             grdLister.Columns["AvailSeats"].Width =115;

           //  grdLister.Columns["TotalJobs"].HeaderText ="Total Jobs";

           //  grdLister.Columns["TotalJobs"].Width =100;
             grdLister.Columns["Status"].Width =90;

     
      

             UI.GridFunctions.SetFilter(grdLister);


           

         }

         private void LoadData()
         {





             var list = (from a in General.GetQueryable<BookingGroup>(null)
                         select new
                         {
                             Id = a.Id,
                             GroupName = a.GroupName,
                             Zone = a.Gen_ShuttleZone.ZoneName,
                             Destination = a.Gen_Location.LocationName,
                          //   DestinationId = a.DestinationId,
                             FlightDeparture = a.FlightDepartureDate,
                           //  Drv = a.DriverId != null ? a.Fleet_Driver.DriverNo : "",
                             TotalSeats = a.NoOfSeats,
                             AvailSeats = a.NoOfSeats - a.Bookings.Sum(c => c.NoofPassengers),
                        //     TotalJobs = a.Bookings.Count,
                             Status=a.BookingTripStatus.StatusName


                         }).ToList();


             grdLister.DataSource = list;
            // this.SetRefreshingProperties(AppVars.BLData.GetCommand(query), grdLister, false);

            

         }

         private void grid_CommandCellClick(object sender, EventArgs e)
         {
             GridCommandCellElement gridCell = (GridCommandCellElement)sender;
             if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
             {

                 if (General.GetQueryable<Booking>(c => c.GroupJobId == grdLister.CurrentRow.Cells["Id"].Value.ToLong()).Count() > 0)
                 {
                     ENUtils.ShowMessage("Cannot Delete a Group" + Environment.NewLine + "Jobs are attached from this Group");
                     return;
                 }


                 if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Group ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
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
                 ShowDetailForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
             }
             else
             {
                 ENUtils.ShowMessage("Please select a record");
             }
         }


         private void ShowDetailForm(int id)
         {


             frmCreateGroup frm = new frmCreateGroup();
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();
            frm.Dispose();


            PopulateData();
         }


         void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
         {
             if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
             {

                 objMaster = new BookingGroupBO();

                     try
                     {

                         objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                        
                         if (objMaster.Current != null)
                         {
                          
                             objMaster.Delete(objMaster.Current);

                            
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

            LoadData();

        }

       
        private void btnAddZone_Click(object sender, EventArgs e)
        {
            ShowZoneForm();
        }

        private void ShowZoneForm()
        {
            frmZones frm = new frmZones();
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.MaximizeBox = false;
            frm.ControlBox = true;
            frm.ShowIcon = false;
            frm.ShowDialog();
        }

        private void btnDrawPlot_Click(object sender, EventArgs e)
        {
          //  General.ShowDrawZoneForm(0);
        }

        private void btnDrawEditZone_Click(object sender, EventArgs e)
        {
            //if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            //{

            //    General.ShowDrawZoneForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            //}
        }


     

    }
}

