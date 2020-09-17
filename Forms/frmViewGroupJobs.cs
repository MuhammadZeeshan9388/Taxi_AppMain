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

    


    public partial class frmViewGroupJobs :Form
    {


        long _GroupId;

        public long GroupId
        {
            get { return _GroupId; }
            set { _GroupId = value; }
        }
        long _SelectedJobId;

        public long SelectedJobId
        {
            get { return _SelectedJobId; }
            set { _SelectedJobId = value; }
        }




        public frmViewGroupJobs(long groupId,long selectedJobId)
        {
            InitializeComponent();
            FormatGroupGrid();
            FormatJobsGrid();

            this.GroupId = groupId;
            this.SelectedJobId = selectedJobId;
            PopulateData(groupId, selectedJobId);
  
        }




        private void PopulateData(long groupId, long selectedJobId)
        {
            try
            {


            

                var objGroup = General.GetObject<BookingGroup>(c => c.Id == groupId);


                if (objGroup != null)
                {

                    if(grdGroupDetails.Rows.Count==0)
                       grdGroupDetails.Rows.AddNew();
                  
                    
                    
                    grdGroupDetails.Rows[0].Cells["Id"].Value = objGroup.Id;
                    grdGroupDetails.Rows[0].Cells["GroupName"].Value = objGroup.GroupName;
                    grdGroupDetails.Rows[0].Cells["FlightDeparture"].Value = objGroup.FlightDepartureDate;
                    grdGroupDetails.Rows[0].Cells["Destination"].Value = objGroup.Gen_Location.DefaultIfEmpty().LocationName;
                    grdGroupDetails.Rows[0].Cells["DestinationId"].Value = objGroup.DestinationId;

                    grdGroupDetails.Rows[0].Cells["Zone"].Value = objGroup.Gen_ShuttleZone.DefaultIfEmpty().ZoneName.ToStr();
                    grdGroupDetails.Rows[0].Cells["TotalSeats"].Value = objGroup.NoOfSeats.ToInt();
                    grdGroupDetails.Rows[0].Cells["AvailSeats"].Value =objGroup.NoOfSeats.ToInt()- objGroup.Bookings.Sum(c=>c.NoofPassengers).ToInt();


                    grdGroupDetails.Rows[0].Cells["TotalJobs"].Value = objGroup.Bookings.Count;
                    grdGroupDetails.Rows[0].Cells["Status"].Value = objGroup.BookingTripStatus.DefaultIfEmpty().StatusName.ToStr();



                    if (objGroup.FlightDepartureDate == null)
                    {
                        grdGroupDetails.Columns["FlightDeparture"].IsVisible = false;
                        grdGroupDetails.Columns["Destination"].Width = 420;

                    }
                 

                    var jobs = (from a in General.GetQueryable<Booking>(c => c.GroupJobId == groupId)
                                select new
                                {
                                    Id = a.Id,
                                    RefNo = a.BookingNo,
                                    Passenger = a.CustomerName,
                                    PickupDateTime = a.PickupDateTime,
                                    PickupPoint = a.FromAddress,
                                    Status = a.BookingStatus.StatusName,
                                    Drv = a.Fleet_Driver != null ? a.Fleet_Driver.DriverNo : "",
                                    TotalPax=a.NoofPassengers
                                }
                             ).ToList();



                    grdJobDetails.RowCount = jobs.Count;

                    for (int i = 0; i < grdJobDetails.RowCount; i++)
                    {

                        grdJobDetails.Rows[i].Cells["Id"].Value = jobs[i].Id;
                        grdJobDetails.Rows[i].Cells["Passenger"].Value = jobs[i].Passenger.ToStr();
                        grdJobDetails.Rows[i].Cells["PickupDateTime"].Value = jobs[i].PickupDateTime;
                        grdJobDetails.Rows[i].Cells["PickupPoint"].Value = jobs[i].PickupPoint;
                        grdJobDetails.Rows[i].Cells["NoOfPax"].Value = jobs[i].TotalPax.ToInt();
                        grdJobDetails.Rows[i].Cells["Drv"].Value = jobs[i].Drv;
                        grdJobDetails.Rows[i].Cells["Status"].Value = jobs[i].Status;


                    }



                    if (grdJobDetails.RowCount > 0 && this.SelectedJobId > 0)
                    {
                        ConditionalFormattingObject objCondition = new ConditionalFormattingObject();
                        objCondition.ApplyToRow = true;
                        objCondition.RowBackColor = Color.LightGreen;
                        objCondition.ConditionType = ConditionTypes.Equal;
                        objCondition.TValue1 = selectedJobId.ToStr();

                        grdJobDetails.Columns["Id"].ConditionalFormattingObjectList.Add(objCondition);
                        
                        grdJobDetails.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdJobDetails_ContextMenuOpening);                       

                    }



                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }



             
          


              
               

             


            
        }

        RadDropDownMenu contextMenu = null;
        void grdJobDetails_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {

            if (contextMenu == null)
            {
                contextMenu = new RadDropDownMenu();


                RadMenuItem firstContextMenuItem1 = new RadMenuItem("Detach Job");

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

                if(grdJobDetails.CurrentRow!=null && grdJobDetails.CurrentRow is GridViewDataRowInfo)
                {

                    BookingBO objBookingMaster = new BookingBO();
                    objBookingMaster.GetByPrimaryKey(grdJobDetails.CurrentRow.Cells["Id"].Value.ToLong());


                    if (objBookingMaster.Current != null)
                    {

                        try
                        {

                            objBookingMaster.CheckDataValidation = false;
                            objBookingMaster.CheckCustomerValidation = false;

                            objBookingMaster.Current.GroupJobId = null;
                            objBookingMaster.Save();

                            PopulateData(this.GroupId,this.SelectedJobId);

                        }
                        catch (Exception ex)
                        {
                            if (objBookingMaster.Errors.Count > 0)
                                ENUtils.ShowMessage(objBookingMaster.ShowErrors());
                            else
                            {
                                ENUtils.ShowMessage(ex.Message);
                            }

                        }

                    }
                }

            }
            catch (Exception ex)
            {
                //   ENUtils.ShowMessage(ex.Message);

            }
        }


        private void FormatGroupGrid()
        {

            grdGroupDetails.ShowRowHeaderColumn = false;
            grdGroupDetails.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
         
            grdGroupDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Group No";
            col.Name = "GroupName";
            col.Width = 100;
            grdGroupDetails.Columns.Add(col);


            GridViewDateTimeColumn colD = new GridViewDateTimeColumn();
            colD.HeaderText = "Departure Date Time";
            colD.Name = "FlightDeparture";
            colD.Width = 150;
            colD.CustomFormat = "dd/MM/yyyy HH:mm";
            colD.FormatString = "{0:dd/MM/yyyy HH:mm}";

            grdGroupDetails.Columns.Add(colD);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Zone";
            col.Name = "Zone";
            col.Width = 90;
            grdGroupDetails.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = "Destination";
            col.Width = 215;
            grdGroupDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "DestinationId";
            grdGroupDetails.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "No of Seats";
            col.Name = "TotalSeats";
            col.Width = 90;
            grdGroupDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Available Seats";
            col.Name = "AvailSeats";
            col.Width = 115;
            grdGroupDetails.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Total Jobs";
            col.Name = "TotalJobs";
            col.Width = 80;
            grdGroupDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Status";
            col.Name = "Status";
            col.Width = 90;
            grdGroupDetails.Columns.Add(col);
           

         
        }



        private void FormatJobsGrid()
        {

            grdGroupDetails.ShowGroupPanel = false;
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdJobDetails.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Passenger";
            col.Name = "Passenger";
            col.Width = 100;
            grdJobDetails.Columns.Add(col);


            GridViewDateTimeColumn colD = new GridViewDateTimeColumn();
            colD.HeaderText = "Pickup Date Time";
            colD.Name = "PickupDateTime";
            colD.Width = 130;
            grdJobDetails.Columns.Add(colD);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup Point";
            col.Name = "PickupPoint";
            col.Width = 400;
            grdJobDetails.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "No of Pax.";
            col.Name = "NoOfPax";
            col.Width = 100;
            grdJobDetails.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Drv";
            col.Width = 70;
            col.Name = "Drv";
            grdJobDetails.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Status";
            col.Width = 100;
            col.Name = "Status";
            grdJobDetails.Columns.Add(col);          



        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

       
      


    }
}
