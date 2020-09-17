using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using DAL;
using Taxi_Model;
using Telerik.WinControls.UI;
using Utils;
using System.Threading;
using System.Diagnostics;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;
using System.Data.SqlClient;
using Taxi_AppMain.Classes;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using Telerik.WinControls.Enumerations;
using System.IO;
using System.Collections;
using Taxi_AppMain.Forms;
using UI;
using System.Web.UI;


namespace Taxi_AppMain
{

    


    public partial class frmPickGroup :UI.SetupBase
    {
       


      

        private BookingGroup _SelectedGroup;

        public BookingGroup SelectedGroup
        {
            get { return _SelectedGroup; }
            set { _SelectedGroup = value; }
        }







       

        private int _DestinationLocTypeId;

        public int DestinationLocTypeId
        {
            get { return _DestinationLocTypeId; }
            set { _DestinationLocTypeId = value; }
        }


        private int TotalAllocatingPax;

        public frmPickGroup(int destLocTypeId, int destinationId,int vehicleTypeId,int ZoneId,int passengerAllocationRequired)
        {
            InitializeComponent();
            this.DestinationLocTypeId = destLocTypeId;

            FormatGrid();
            InitializeConstructor(destLocTypeId, destinationId, vehicleTypeId, ZoneId);

            this.TotalAllocatingPax = passengerAllocationRequired;
           
        }

       



        private void InitializeConstructor(int destLocTypeId,int destinationId,int vehicleTypeId,int zoneId)
        {

          

             var list =(from a in General.GetQueryable<BookingGroup>(c => c.DestinationId == destinationId
                                && c.VehicleTypeId == vehicleTypeId && (c.TripStatusId!=null && c.TripStatusId==Enums.BOOKING_TRIPSTATUS.WAITING)
                                && (c.PickupZoneId == zoneId || c.Gen_ShuttleZone.Gen_ShuttleZone_AllowedZones.Count(a => a.AllowedZoneId == zoneId) > 0))
                        select new
                            {
                                Id = a.Id,
                                GroupName = a.GroupName,
                                Zone=a.Gen_ShuttleZone.ZoneName,
                                Destination = a.Gen_Location.LocationName,
                                DestinationId=a.DestinationId,
                                FlightDeparture = a.FlightDepartureDate,
                                Drv=a.DriverId!=null ? a.Fleet_Driver.DriverNo :"",
                                TotalSeats=a.NoOfSeats,
                                AvailSeats=a.NoOfSeats - a.Bookings.Sum(c=>c.NoofPassengers),
                                TotalJobs = a.Bookings.Count



                            }).ToList();



             int count = list.Count;
             grdLister.RowCount = count;

             for(int i=0; i<count; i++)
             {

                 grdLister.Rows[i].Cells["Id"].Value = list[i].Id;
                 grdLister.Rows[i].Cells["GroupName"].Value = list[i].GroupName.ToStr();
                 grdLister.Rows[i].Cells["Zone"].Value = list[i].Zone.ToStr();
                 grdLister.Rows[i].Cells["Destination"].Value = list[i].Destination.ToStr();
                 grdLister.Rows[i].Cells["DestinationId"].Value = list[i].DestinationId.ToInt();
                 grdLister.Rows[i].Cells["FlightDeparture"].Value = list[i].FlightDeparture;
                 grdLister.Rows[i].Cells["Drv"].Value = list[i].Drv.ToStr();


                 grdLister.Rows[i].Cells["TotalSeats"].Value = list[i].TotalSeats.ToStr();
                 grdLister.Rows[i].Cells["AvailSeats"].Value = list[i].AvailSeats.ToStr() == string.Empty ? list[i].TotalSeats.ToStr() : list[i].AvailSeats.ToStr();


                 grdLister.Rows[i].Cells["TotalJobs"].Value = list[i].TotalJobs.ToStr();



             }

          

              

       

             grdLister.CellDoubleClick += new GridViewCellEventHandler(grdSMSTemplets_CellDoubleClick);

             grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdJobDetails_ContextMenuOpening);



            
        }


        RadDropDownMenu contextMenu = null;
        void grdJobDetails_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {

            if (contextMenu == null)
            {
                contextMenu = new RadDropDownMenu();


                RadMenuItem firstContextMenuItem1 = new RadMenuItem("View Jobs");

                firstContextMenuItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);

                firstContextMenuItem1.Click += new EventHandler(firstContextMenuItem1_Click);
                contextMenu.Items.Add(firstContextMenuItem1);

            }

            e.ContextMenu = contextMenu;


        }

        void firstContextMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {

                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {

                    ViewGroupJobs(grdLister.CurrentRow.Cells["Id"].Value.ToLong(), 0);

                }

            }
            catch (Exception ex)
            {
                //   ENUtils.ShowMessage(ex.Message);

            }
        }


        private void ViewGroupJobs(long groupId, long selectedJobId)
        {
            try
            {
                frmViewGroupJobs frmGroupJobs = new frmViewGroupJobs(groupId, selectedJobId);
                frmGroupJobs.StartPosition = FormStartPosition.CenterScreen;
                frmGroupJobs.ShowDialog();


                frmGroupJobs.Dispose();
                frmGroupJobs = null;
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }


        }


        private void FormatGrid()
        {
            grdLister.ShowRowHeaderColumn = false;

            grdLister.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
         
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Group No";
            col.Name = "GroupName";
            col.Width = 100;
            grdLister.Columns.Add(col);


            GridViewDateTimeColumn colD = new GridViewDateTimeColumn();

            colD.IsVisible = this.DestinationLocTypeId == Enums.LOCATION_TYPES.AIRPORT;
            colD.CustomFormat = "dd/MM/yyyy HH:mm";
            colD.FormatString = "{0:dd/MM/yyyy HH:mm}";

            colD.HeaderText = "Departure Date Time";
            colD.Name = "FlightDeparture";
            colD.Width = 150;
            grdLister.Columns.Add(colD);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Zone";
            col.Name = "Zone";
            col.Width = 90;
            grdLister.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = "Destination";
            col.Width =colD.IsVisible? 160:260;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Drv";
            col.Name = "Drv";
            col.Width = 80;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "DestinationId";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "No of Seats";
            col.Name = "TotalSeats";
            col.Width = 80;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Available Seats";
            col.Name = "AvailSeats";
            col.Width = 95;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Total Jobs";
            col.Name = "TotalJobs";
            col.Width = 80;
            grdLister.Columns.Add(col);
           

         
        }
        

        void grdSMSTemplets_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {


                string totalSeats = grdLister.CurrentRow.Cells["TotalSeats"].Value.ToStr();
                int totalNoOfSeats = 0;
                int availableSeats=0;
                if (!string.IsNullOrEmpty(totalSeats))
                {
                    totalNoOfSeats = totalSeats.ToInt();

                    if(!string.IsNullOrEmpty( grdLister.CurrentRow.Cells["AvailSeats"].Value.ToStr().Trim()))
                    {

                        availableSeats = grdLister.CurrentRow.Cells["AvailSeats"].Value.ToInt();
                    }

                }

                if (availableSeats - this.TotalAllocatingPax < 0)
                {
                    ENUtils.ShowMessage("Available seats are not enough to Allocate "+this.TotalAllocatingPax+" Passenger(s) in this Group");

                    return;
                }


                this.SelectedGroup = new BookingGroup();
                this.SelectedGroup.Id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                this.SelectedGroup.GroupName = grdLister.CurrentRow.Cells["GroupName"].Value.ToStr();
                this.SelectedGroup.FlightDepartureDate = grdLister.CurrentRow.Cells["FlightDeparture"].Value.ToDateTime();



            }


            this.Close();
        }
        private void frmDrivertemplet_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmDrivertemplet_Load(object sender, EventArgs e)
        {
           
        }

       
     

        private void frmDrivertemplet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
            else if (e.KeyCode == Keys.Enter)
            {
                if(grdLister.CurrentRow!=null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    this.SelectedGroup = new BookingGroup();
                    this.SelectedGroup.Id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                    this.SelectedGroup.GroupName = grdLister.CurrentRow.Cells["GroupName"].Value.ToStr();
                    this.SelectedGroup.FlightDepartureDate = grdLister.CurrentRow.Cells["FlightDeparture"].Value.ToDateTime();


                    
                }
              
                    
                 this.Close();
            }

        }

      


    }
}
