using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Telerik.WinControls.UI;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls;
using System.Net;
using UI;
using System.Xml.Linq;
using Telerik.WinControls.Enumerations;
using System.Data.Linq;
using System.Xml;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class xfrmAdvanceBookingCustomization : RadForm
    {
        public struct COLS
        {
            public static string ID = "ID";
            public static string EXCLUDED = "EXCLUDED";
            public static string CHANGED = "CHANGED";   

            public static string DAY = "Day";
            public static string FromLocTypeId = "FromLocTypeId";
            public static string FromLocId = "FromLocId";
            public static string FromAddress = "From";
            public static string ToLocTypeId = "ToLocTypeId";
            public static string ToLocId = "ToLocId";
            public static string ToAddress = "To";
            public static string Account = "A/C";
            public static string AccountName = "AccountName";

            public static string Fare = "Fare";
            public static string ReturnFare = "Return Fare";


            public static string MASTERBOOKING = "Master";
            public static string PickupDate = "PickupDate";
            public static string ReturnPickupDate = "ReturnPickupDate";
            public static string JourneyTypeId = "JourneyTypeId";

            public static string MasterJobId = "MasterJobId";

            public static string AutoDespatch = "AutoDespatch";
            public static string AutoDespatchDateTime = "AutoDespatchDateTime";
            public static string DriverId = "DriverId";


            public static string FromDoorNo = "FromDoorNo";
            public static string FromStreet = "FromStreet";
            public static string FromPostCode = "FromPostCode";
            public static string ToDoorNo = "ToDoorNo";
            public static string ToStreet = "ToStreet";
            public static string ToPostCode = "ToPostCode";
            public static string StatusId = "StatusId";

            public static string ChangeBooking = "ChangeBooking";
            public static string ViaPoint = "ViaPoint";
        }

        public struct COLS_PICKUPS
        {
            public static string ID = "ID";    
            public static string DAY = "Day";     

            public static string PickupDate = "PickupDate";
            public static string PickupTime = "PickupTime";
            public static string ReturnPickupDate = "ReturnPickupDate";
            public static string ReturnPickupTime = "ReturnPickupTime";
            public static string Fare = "Fare";
            public static string RetFare = "RetFare";

        }
        
        private Thread th = null;

        BookingBO objMaster;
        private AdvanceBooking ObjAdvanceBooking;


        private Booking _objBookiing;

        public Booking ObjBookiing
        {
            get { return _objBookiing; }
            set { _objBookiing = value; }
        }


        public xfrmAdvanceBookingCustomization(long id)
        {
            InitializeComponent();
            this.ObjAdvanceBooking = General.GetObject<AdvanceBooking>(c=>c.Id==id);




            InitializeFormSettings();


         
         }




        private void InitializeFormSettings()
        {
            try
            {

            
             //   optDaily.ToggleState = ToggleState.On;
                FormatBookingGrid();

                FormatPickupsGrid(grdPickupDates);
                FormatPickupsGrid(grdExcludePickupDates);

                grdBookings.ViewCellFormatting += new CellFormattingEventHandler(grdBookings_ViewCellFormatting);
                this.Shown += new EventHandler(frmMultiBooking_Shown);

                MapType = AppVars.objPolicyConfiguration.MapType.ToInt();




                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);



                txtFromAddress.ListBoxElement.Width = 610;
                txtFromAddress.ListBoxElement.Height = 400;
                txtToAddress.ListBoxElement.Width = 610;



                Font font = new Font("Tahoma", 11, FontStyle.Bold);
                txtFromAddress.ListBoxElement.Font = font;
                txtToAddress.ListBoxElement.Font = font;


                txtFromAddress.ListBoxElement.ItemHeight = 30;
                txtToAddress.ListBoxElement.ItemHeight = 30;
                txtFromAddress.ListBoxElement.DrawMode = DrawMode.OwnerDrawVariable;
                txtFromAddress.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);

                txtToAddress.ListBoxElement.DrawMode = DrawMode.OwnerDrawVariable;
                txtToAddress.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);


                ddlFromLocType.SelectedIndexChanged += new EventHandler(ddlFromLocType_SelectedIndexChanged);  

                ddlToLocType.SelectedIndexChanged+=new EventHandler(ddlToLocType_SelectedIndexChanged);

                ComboFunctions.FillLocationTypeCombo(ddlFromLocType);
                ComboFunctions.FillLocationTypeCombo(ddlToLocType);


                SetFromAddress(ToggleState.Off);
                SetToAddress(ToggleState.Off);




            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            }

            // Draw a rectangle in blue around each item.
            e.Graphics.DrawRectangle(Pens.Blue, e.Bounds);

            // Draw the text in the item.
            e.Graphics.DrawString((sender as ListBox).Items[e.Index].ToString(),
                e.Font, Brushes.Black, e.Bounds.X, e.Bounds.Y);

            // Draw the focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }

        private void FillFromLocations()
        {
            int locTypeId = ddlFromLocType.SelectedValue.ToInt();
            if (locTypeId == 0)
                return;
            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
            {
                txtFromAddress.Visible = true;

                DetachLocationsSelectionEvent(ddlFromLocation);
                ddlFromLocation.SelectedValue = null;
                ddlFromLocation.Visible = false;
                AttachLocationSelectionEvent(ddlFromLocation);

                txtFromPostCode.Text = string.Empty;
                txtFromPostCode.Visible = false;

                lblFromDoorFlightNo.Text = "Pickup Notes";
                lblFromDoorFlightNo.Visible = true;
                //   lblFromDoorFlightNo.Location=new Point(lblFromDoorFlightNo
             //   lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.NewFromDoorNoLoc.Y + 1);

                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 170;

                txtFromFlightDoorNo.Text = string.Empty;
                txtFromFlightDoorNo.Visible = true;
             //   txtFromFlightDoorNo.Location = this.NewFromDoorNoLoc;


                txtFromStreetComing.Text = string.Empty;
                txtFromStreetComing.Visible = false;

                // lblFromDoorFlightNo.Visible = false;
                lblFromStreetComing.Visible = false;

                lblFromLoc.Text = "Pickup Point";

                if (locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    txtFromAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                    txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    SetPickupZone(txtFromAddress.Text);
                }
            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {

              //  LoadPostCodes();

                txtFromAddress.Text = string.Empty;
                txtFromAddress.Visible = false;

                DetachLocationsSelectionEvent(ddlFromLocation);
                ddlFromLocation.SelectedValue = null;
                ddlFromLocation.Visible = false;
                AttachLocationSelectionEvent(ddlFromLocation);

                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 170;

           //     lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.OldfromDoorNoLoc.Y);

           //     txtFromFlightDoorNo.Location = this.OldfromDoorNoLoc;
                txtFromPostCode.Visible = true;
                txtFromFlightDoorNo.Visible = true;
                txtFromStreetComing.Visible = true;
                lblFromDoorFlightNo.Visible = true;
                lblFromStreetComing.Visible = true;

                lblFromLoc.Text = "From PostCode";

                lblFromDoorFlightNo.Text = "Door #";
                lblFromStreetComing.Text = "From Street";

            }

            else if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                txtFromAddress.Text = string.Empty;
                txtFromAddress.Visible = false;

                ddlFromLocation.Visible = true;

                txtFromPostCode.Text = string.Empty;
                txtFromPostCode.Visible = false;

                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 170;

            ////    lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.OldfromDoorNoLoc.Y);
           //     txtFromFlightDoorNo.Location = this.OldfromDoorNoLoc;
                txtFromFlightDoorNo.Visible = true;
                txtFromStreetComing.Visible = true;
                lblFromDoorFlightNo.Visible = true;
                lblFromStreetComing.Visible = true;

                lblFromLoc.Text = "From Location";

                lblFromDoorFlightNo.Text = "Flight No";
                lblFromStreetComing.Text = "Coming From";
                DetachLocationsSelectionEvent(ddlFromLocation);
                ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);
                ddlFromLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlFromLocation);
            }


            else
            {
                lblFromLoc.Text = "From Location";

                txtFromPostCode.Text = string.Empty;
                txtFromPostCode.Visible = false;

                txtFromFlightDoorNo.Text = string.Empty;
                txtFromFlightDoorNo.Visible = false;

                txtFromStreetComing.Text = string.Empty;
                txtFromStreetComing.Visible = false;


                lblFromDoorFlightNo.Text = "Pickup Notes";
                lblFromDoorFlightNo.Visible = true;
                //   lblFromDoorFlightNo.Location=new Point(lblFromDoorFlightNo
                // lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.NewFromDoorNoLoc.Y + 1);

                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 200;

                txtFromFlightDoorNo.Text = string.Empty;
                txtFromFlightDoorNo.Visible = true;
              //  txtFromFlightDoorNo.Location = this.NewFromDoorNoLoc;

                // lblFromDoorFlightNo.Visible = false;
                lblFromStreetComing.Visible = false;

                txtFromAddress.Text = string.Empty;
                txtFromAddress.Visible = false;

                ddlFromLocation.Visible = true;
                DetachLocationsSelectionEvent(ddlFromLocation);
                ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);
                ddlFromLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlFromLocation);
                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 120;

            }
        }


        private void DetachLocationsSelectionEvent(DJComboBox djcbo)
        {
            //if (djcbo.Name == "ddlFromLocation")
            //    djcbo.SelectedValueChanged -= new EventHandler(ddlFromLocation_SelectedValueChanged);
            //else
            //    djcbo.SelectedValueChanged -= new EventHandler(ddlToLocation_SelectedValueChanged);

        }


        private void AttachLocationSelectionEvent(DJComboBox djcbo)
        {
            //if (djcbo.Name == "ddlFromLocation")
            //    djcbo.SelectedValueChanged += new EventHandler(ddlFromLocation_SelectedValueChanged);
            //else
            //    djcbo.SelectedValueChanged += new EventHandler(ddlToLocation_SelectedValueChanged);

        }

        private void SetOthersToLocation()
        {
            txtToPostCode.Text = string.Empty;
            txtToPostCode.Visible = false;

            txtToFlightDoorNo.Text = string.Empty;
            txtToFlightDoorNo.Visible = false;

            txtToStreetComing.Text = string.Empty;
            txtToStreetComing.Visible = false;

            lblToDoorFlightNo.Visible = false;
            lblToStreetComing.Visible = false;

            txtToAddress.Text = string.Empty;
            txtToAddress.Visible = false;


            ddlToLocation.Visible = true;
            lblToLoc.Text = "To Location";


            // txtToFlightDoorNo.MaxLength = 100;
            // txtToFlightDoorNo.Width = 170;

            //


            lblToDoorFlightNo.Text = "Dest. Notes";
            lblToDoorFlightNo.Visible = true;
            // lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.NewtoDoorNoLoc.Y + 1);

            txtToFlightDoorNo.MaxLength = 100;
            txtToFlightDoorNo.Width = 200;

            txtToFlightDoorNo.Text = string.Empty;
            txtToFlightDoorNo.Visible = true;
          //  txtToFlightDoorNo.Location = this.NewtoDoorNoLoc;



            //


         //   ddlReturnFromAirport.SelectedValue = null;
        //    ddlReturnFromAirport.Visible = false;
        //    lblReturnFromAirport.Visible = false;

        }


        private void SetReturnAirportJob(ToggleState toggle)
        {
            if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
            {

                if (toggle == ToggleState.On)
                {

                    txtToAddress.Text = string.Empty;
                    txtToAddress.Visible = false;

                    ddlToLocation.Visible = true;

                    txtToPostCode.Text = string.Empty;
                    txtToPostCode.Visible = false;

                    txtToFlightDoorNo.MaxLength = 100;
                    txtToFlightDoorNo.Width = 170;

              //      lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.OldtoDoorNoLoc.Y);
             //       txtToFlightDoorNo.Location = this.OldtoDoorNoLoc;
                    txtToFlightDoorNo.Visible = true;
                    txtToStreetComing.Visible = true;

                    lblToDoorFlightNo.Visible = true;
                    lblToStreetComing.Visible = true;

                    lblToLoc.Text = "To Location";

                    lblToDoorFlightNo.Text = "Flight No";
                    lblToStreetComing.Text = "Coming From";


                 //   lblReturnFromAirport.Visible = true;
                 //   ddlReturnFromAirport.Visible = true;
                 //   ComboFunctions.FillLocationsCombo(ddlReturnFromAirport, c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT);
                 //   ddlReturnFromAirport.SelectedIndex = -1;
                }
                else
                {


                    SetOthersToLocation();

                }



            }
        }

        private void ddlToLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillToLocations();


        }

        private void FillToLocations()
        {


            int locTypeId = ddlToLocType.SelectedValue.ToInt();
            if (locTypeId == 0)
                return;

            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
            {
                lblToLoc.Text = "Destination";
                txtToAddress.Visible = true;

                DetachLocationsSelectionEvent(ddlToLocation);
                ddlToLocation.SelectedValue = null;
                ddlToLocation.Visible = false;
                AttachLocationSelectionEvent(ddlToLocation);

                txtToPostCode.Text = string.Empty;
                txtToPostCode.Visible = false;


                lblToDoorFlightNo.Text = "Dest. Notes";
                lblToDoorFlightNo.Visible = true;
             //   lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.NewtoDoorNoLoc.Y + 1);

                txtToFlightDoorNo.MaxLength = 100;
                txtToFlightDoorNo.Width = 170;

                txtToFlightDoorNo.Text = string.Empty;
                txtToFlightDoorNo.Visible = true;
            //    txtToFlightDoorNo.Location = this.NewtoDoorNoLoc;

                txtToStreetComing.Text = string.Empty;
                txtToStreetComing.Visible = false;


                // lblToDoorFlightNo.Visible = false;
                lblToStreetComing.Visible = false;


                if (locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    txtToAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                    txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                }
            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtToAddress.Text = string.Empty;
                txtToAddress.Visible = false;

                DetachLocationsSelectionEvent(ddlToLocation);
                ddlToLocation.SelectedValue = null;
                ddlToLocation.Visible = false;
                AttachLocationSelectionEvent(ddlToLocation);

              //  txtToFlightDoorNo.Location = this.OldtoDoorNoLoc;
                txtToPostCode.Visible = true;
                txtToFlightDoorNo.Visible = true;
                txtToStreetComing.Visible = true;
                lblToDoorFlightNo.Visible = true;
                lblToStreetComing.Visible = true;

                lblToLoc.Text = "To PostCode";

             //   lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.OldtoDoorNoLoc.Y);
                lblToDoorFlightNo.Text = "To Door No";
                lblToStreetComing.Text = "To Street";

                txtToFlightDoorNo.MaxLength = 100;
                txtToFlightDoorNo.Width = 170;

            }

            else if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                //SetReturnAirportJob(opt_JReturnWay.ToggleState);
                DetachLocationsSelectionEvent(ddlToLocation);
                ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
                ddlToLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlToLocation);
            }


            else
            {



                SetOthersToLocation();

                DetachLocationsSelectionEvent(ddlToLocation);
                ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
                ddlToLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlToLocation);
            }
        }


          

        private int _MapType;

        public int MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }


        private void FormatDailyPickupsGrid()
        {
        //    FormatPickupsGrid(grdPickupDates);


        }
        private void FormatWeeklyPickupsGrid()
        {
          //  FormatPickupsGrid(grdWeeklyPickupGrid);


        }

        private void FormatPickupsGrid(RadGridView grid)
        {
            grid.Font = new Font("Tahoma", 10, FontStyle.Bold);
            grid.AllowAddNewRow = false;
            grid.ShowGroupPanel = false;
            grid.ShowRowHeaderColumn = false;
            grid.AllowEditRow = false;
            grid.EnableHotTracking = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = COLS_PICKUPS.DAY;
            col.Name = COLS_PICKUPS.DAY;
            col.Width = 90;
            grid.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS_PICKUPS.ID;          
            grid.Columns.Add(col);

            GridViewDateTimeColumn colDate = new GridViewDateTimeColumn();
            colDate.HeaderText = "Pickup Date";
            colDate.Width = 140;
            colDate.CustomFormat = "dd/MM/yyyy HH:mm";
            colDate.FormatString = "{0:dd/MM/yyyy HH:mm}";
            colDate.AllowSort = true;
            colDate.AllowResize = false;      
         
            colDate.SortOrder = RadSortOrder.Ascending;
            colDate.Name = COLS_PICKUPS.PickupDate;
            grid.Columns.Add(colDate);


            grid.AllowColumnResize = false;
            grid.SortDescriptors.Add(COLS_PICKUPS.PickupDate, ListSortDirection.Ascending);


        }


        private void DisplayBookings()
        {

            try
            {
                bool ShowVia = false;
                grdExcludePickupDates.Rows.Clear();


                string contact = ObjAdvanceBooking.CustomerMobileNo.ToStr().Trim() + "/" + ObjAdvanceBooking.CustomerTelephoneNo.ToStr().Trim();




                txtBookedBy.Text = "Booked by " + ObjAdvanceBooking.AddLog.ToStr() + " on " + string.Format("{0:dd/MM/yyyy @ HH:mm}", ObjAdvanceBooking.AddOn);




                if (contact.EndsWith("/"))
                    contact = contact.Remove(contact.LastIndexOf('/'));



                if (contact.StartsWith("/"))
                    contact = contact.Substring(1, contact.Length - 1);



                txtCustomerName.Text = ObjAdvanceBooking.CustomerName.ToStr().Trim() + " - " + contact;


                var bookinglist = General.GetQueryable<Booking>(c => c.AdvanceBookingId == ObjAdvanceBooking.Id).ToList();




                grdBookings.RowCount = bookinglist.Count;
                grdPickupDates.RowCount = grdBookings.RowCount;


                int rowCnt = grdBookings.Rows.Count;

                for (int i = 0; i < rowCnt; i++)
                {
                    if (i == 0)
                    {
                        dtpPickupTime.Value = bookinglist[i].PickupDateTime;


                        dtpStartingAt.Value = bookinglist[i].PickupDateTime;


                        ddlFromLocType.SelectedValue = bookinglist[i].FromLocTypeId;
                        ddlToLocType.SelectedValue = bookinglist[i].ToLocTypeId;

                        txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        txtFromAddress.Text = bookinglist[i].FromAddress.ToStr();



                        txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                        txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        txtToAddress.Text = bookinglist[i].ToAddress.ToStr();
                        txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                        txtFromFlightDoorNo.Text = bookinglist[i].FromDoorNo.ToStr();
                        txtToFlightDoorNo.Text = bookinglist[i].ToDoorNo.ToStr();


                        ddlFromLocation.SelectedValue = bookinglist[i].FromLocId;
                        ddlToLocation.SelectedValue = bookinglist[i].ToLocId;

                        txtFromStreetComing.Text = bookinglist[i].FromStreet.ToStr();






                    }


                    if (i == (rowCnt - 1))
                    {

                        dtpEndingAt.Value = bookinglist[i].PickupDateTime;
                    }

                    grdPickupDates.Rows[i].Cells[COLS_PICKUPS.ID].Value = bookinglist[i].Id;
                    if (bookinglist[i].PickupDateTime != null)
                    {

                        grdPickupDates.Rows[i].Cells[COLS_PICKUPS.DAY].Value = string.Format("{0:dddd}", bookinglist[i].PickupDateTime);
                        grdPickupDates.Rows[i].Cells[COLS_PICKUPS.PickupDate].Value = bookinglist[i].PickupDateTime;


                        grdBookings.Rows[i].Cells[COLS.ID].Value = bookinglist[i].Id;

                        DateTime? dtPickup = bookinglist[i].PickupDateTime.ToDateTime();
                        // string dt = string.Format("{0:dd/MM/yyyy HH:mm}", dtPickup).ToDateTimeorNull();
                        grdBookings.Rows[i].Cells[COLS.DAY].Value = string.Format("{0:dddd}", bookinglist[i].PickupDateTime);
                        grdBookings.Rows[i].Cells[COLS.PickupDate].Value = bookinglist[i].PickupDateTime.ToDateTime();//string.Format("{0:dd/MM/yyyy HH:mm}", bookinglist[i].PickupDateTime.ToDateTime());
                        //grdBookings.Rows[i].Cells[COLS.PickupDate].Value = bookinglist[i].PickupDateTime.ToDateTime() != null ? "" + bookinglist[i].PickupDateTime.ToDateTime() : "";
                    }

                    if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                    {

                        grdBookings.Rows[i].Cells[COLS.FromAddress].Value = bookinglist[i].FromAddress.ToStr();
                    }
                    else if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        grdBookings.Rows[i].Cells[COLS.FromAddress].Value = bookinglist[i].FromPostCode.ToStr();
                    }
                    else
                    {
                        grdBookings.Rows[i].Cells[COLS.FromAddress].Value = bookinglist[i].Gen_Location1.DefaultIfEmpty().LocationName.ToStr().ToUpper();
                        //NC
                        if (bookinglist[i].Gen_Location1 == null)
                        {
                            grdBookings.Rows[i].Cells[COLS.FromAddress].Value = bookinglist[i].FromAddress.ToStr();
                        }


                    }

                    //  grdBookings.Rows[i].Cells[COLS.FromAddress].Value = bookinglist[i].FromAddress;

                    if (bookinglist[i].MasterJobId != null)
                    {



                        if (bookinglist[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || bookinglist[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                        {

                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].ToAddress.ToStr();
                        }
                        else if (bookinglist[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].ToPostCode.ToStr();
                        }
                        else
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].Gen_Location2.DefaultIfEmpty().LocationName.ToStr().ToUpper();
                            //NC
                            if (bookinglist[i].Gen_Location2 == null)
                            {
                                grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].ToAddress.ToStr();
                            }
                        }


                    }
                    else
                    {

                        if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                        {

                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].ToAddress.ToStr();
                        }
                        else if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].ToPostCode.ToStr();
                        }
                        else
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].Gen_Location2.DefaultIfEmpty().LocationName.ToStr().ToUpper();
                            //NC
                            if (bookinglist[i].Gen_Location2 == null)
                            {
                                grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].ToAddress.ToStr();
                            }
                        }
                    }






                    //    grdBookings.Rows[i].Cells[COLS.ToAddress].Value = bookinglist[i].ToAddress;
                    if (bookinglist[i].Booking_ViaLocations.Count > 0)
                    {
                        grdBookings.Rows[i].Cells[COLS.ViaPoint].Value = "Yes";
                    }
                    else
                    {
                        grdBookings.Rows[i].Cells[COLS.ViaPoint].Value = "No";
                    }
                    grdBookings.Rows[i].Cells[COLS.MasterJobId].Value = bookinglist[i].MasterJobId;


                    grdBookings.Rows[i].Cells[COLS.Fare].Value = bookinglist[i].FareRate.ToDecimal();

                    grdBookings.Rows[i].Cells[COLS.FromLocTypeId].Value = bookinglist[i].FromLocTypeId;
                    grdBookings.Rows[i].Cells[COLS.FromLocId].Value = bookinglist[i].FromLocId;

                    grdBookings.Rows[i].Cells[COLS.ToLocTypeId].Value = bookinglist[i].ToLocTypeId;
                    grdBookings.Rows[i].Cells[COLS.ToLocId].Value = bookinglist[i].ToLocId;

                    grdBookings.Rows[i].Cells[COLS.FromDoorNo].Value = bookinglist[i].FromDoorNo.ToStr();
                    grdBookings.Rows[i].Cells[COLS.FromStreet].Value = bookinglist[i].FromStreet.ToStr();
                    grdBookings.Rows[i].Cells[COLS.FromPostCode].Value = bookinglist[i].FromPostCode.ToStr();

                    grdBookings.Rows[i].Cells[COLS.ToDoorNo].Value = bookinglist[i].ToDoorNo.ToStr();
                    grdBookings.Rows[i].Cells[COLS.ToStreet].Value = bookinglist[i].ToStreet.ToStr();
                    grdBookings.Rows[i].Cells[COLS.ToPostCode].Value = bookinglist[i].ToPostCode.ToStr();

                    grdBookings.Rows[i].Cells[COLS.Account].Value = bookinglist[i].CompanyId;

                    grdBookings.Rows[i].Cells[COLS.JourneyTypeId].Value = bookinglist[i].JourneyTypeId.ToInt();
                    if (bookinglist[i].ReturnPickupDateTime != null)
                    {
                        //grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value =string.Format("{0:dd/MM/yyyy HH:mm}",bookinglist[i].ReturnPickupDateTime) != null ? "" +string.Format("{0:dd/MM/yyyy HH:mm}",bookinglist[i].ReturnPickupDateTime) : "";


                        string Date = string.Format("{0:dd/MM/yyyy HH:mm}", bookinglist[i].ReturnPickupDateTime);
                        grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value = Date;
                        // DateTime dtReturn =Convert.ToDateTime(Date);
                        //grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value = string.Format("{0:dd/MM/yyyy HH:mm}", dtReturnPickup);
                        //grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value= string.Format("{0:dd/MM/yyyy HH:mm}", bookinglist[i].ReturnPickupDateTime).ToDateTime();
                        grdBookings.Rows[i].Cells[COLS.ReturnFare].Value = bookinglist[i].ReturnFareRate.ToDecimal();
                        grdBookings.Rows[i].Cells[COLS.ReturnFare].ReadOnly = false;
                        grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].ReadOnly = false;
                    }
                    else
                    {
                        grdBookings.Rows[i].Cells[COLS.ReturnFare].ReadOnly = true;
                    }



                    grdBookings.Rows[i].Cells[COLS.MASTERBOOKING].Value = bookinglist[i].AdvanceBookingId;
                    grdBookings.Rows[i].Cells[COLS.StatusId].Value = bookinglist[i].BookingStatusId;


                    grdBookings.Rows[i].Cells[COLS.EXCLUDED].Value = "";
                    grdBookings.Rows[i].Cells[COLS.CHANGED].Value = "";
                    objMaster = new BookingBO();
                    objMaster.GetByPrimaryKey(bookinglist[i].Id.ToInt());

                    if (bookinglist[i].BookingStatusId != 2 && ShowVia == false)
                    {
                        DisplayBooking_ViaLocations();
                        ShowVia = true;
                    }
                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }


        }

        void frmMultiBooking_Shown(object sender, EventArgs e)
        {
          
                chkStartingAt.Checked = true;
                chkEndingAt.Checked = true;

                DisplayBookings();

             
            
        }







      

      //  string cellValue = string.Empty;
        void grdBookings_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {

            //if (e.CellElement is GridHeaderCellElement)
            //{
            //    //    e.CellElement
            //    e.CellElement.BorderColor = _HeaderRowBorderColor;
            //    e.CellElement.BorderColor2 = _HeaderRowBorderColor;
            //    e.CellElement.BorderColor3 = _HeaderRowBorderColor;
            //    e.CellElement.BorderColor4 = _HeaderRowBorderColor;


            //    // e.CellElement.DrawBorder = false;
            //    e.CellElement.BackColor = _HeaderRowBackColor;
            //    e.CellElement.NumberOfColors = 1;
            //    e.CellElement.Font = newFont;
            //    e.CellElement.ForeColor = Color.White;
            //    e.CellElement.DrawFill = true;
            //    e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            //}

            if (e.CellElement is GridDataCellElement)
            {


                if (e.Row.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED)
                {
                    e.CellElement.RowElement.NumberOfColors = 1;
                    e.CellElement.RowElement.BackColor = Color.Gainsboro;
                    e.CellElement.RowElement.Enabled = false;
                    e.CellElement.RowElement.DrawFill = true;
                }


                //e.CellElement.BorderColor = Color.DarkSlateBlue;
                //e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                //e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                //e.CellElement.BorderColor4 = Color.DarkSlateBlue;
                //e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                //e.CellElement.ForeColor = Color.Black;
                //e.CellElement.Font = oldFont;


                //cellValue = e.CellElement.RowInfo.Cells[COLS.MASTERBOOKING].Value.ToStr();
     
                //if (e.CellElement.RowElement.IsSelected == true && cellValue!=COLS.MASTERBOOKING)
                //{

                //    e.CellElement.RowElement.NumberOfColors = 1;
                //    e.CellElement.RowElement.BackColor = Color.DeepSkyBlue;

                //    e.CellElement.NumberOfColors = 1;
                //    e.CellElement.BackColor = Color.DeepSkyBlue;
                //    e.CellElement.ForeColor = Color.White;
                //    e.CellElement.Font = newFont;

                //}

                
                //else
                //{
                //    if(cellValue!=COLS.MASTERBOOKING)
                //      e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.All);

                //}

                //if (cellValue == COLS.MASTERBOOKING)
                //{

                //    e.CellElement.RowElement.NumberOfColors = 1;
                //    e.CellElement.RowElement.BackColor = Color.LightPink;

                //    e.CellElement.NumberOfColors = 1;
                //    e.CellElement.BackColor = Color.LightPink;
                //    e.CellElement.ForeColor = Color.Black;
                //    e.CellElement.Font = newFont;

                //}
            }
        }

        private void FormatBookingGrid()
        {
            grdBookings.AllowAddNewRow = false;
            grdBookings.ShowGroupPanel = false;
            grdBookings.ShowRowHeaderColumn = false;
            //grdBookings.AllowEditRow = false;
            grdBookings.EnableHotTracking = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.DAY;
            col.Name = COLS.DAY;
            col.Width = 70;
            col.ReadOnly = true;
            grdBookings.Columns.Add(col);


            GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();
            dtcol.HeaderText = "Pickup Date Time";
            dtcol.ReadOnly = false;
            dtcol.Width = 120;
        //    col.FormatString = "dd/MM/yyyy HH:mm";
            dtcol.Name = COLS.PickupDate;
            grdBookings.Columns.Add(dtcol);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Return Pickup Date Time";
            col.Width = 160;
           // col.FormatString = "dd/MM/yyyy HH:mm";
            col.ReadOnly = true;
            col.Name = COLS.ReturnPickupDate;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ID;
            grdBookings.Columns.Add(col);


            

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.MasterJobId;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.EXCLUDED;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.CHANGED;
            grdBookings.Columns.Add(col);
            

            col = new GridViewTextBoxColumn();
            col.IsVisible =false;
            col.Name = COLS.FromLocTypeId;
            grdBookings.Columns.Add(col);
            

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromLocId;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Master";
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromDoorNo;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromStreet;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromPostCode;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToDoorNo;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToStreet;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToPostCode;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.JourneyTypeId;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToLocTypeId;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToLocId;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.FromAddress;
            col.Name = COLS.FromAddress;
            col.Width = 250;
            col.ReadOnly = true;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Via";
            col.Name = COLS.ViaPoint;
            col.Width = 45;

            col.ReadOnly = true;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.ToAddress;
            col.Name = COLS.ToAddress;
            col.ReadOnly = true;
            col.Width = 250;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.Account;
            col.Name = COLS.Account;
            col.IsVisible = false;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "A/C";
            col.Name = COLS.AccountName;
            col.IsVisible = false;
            col.Width = 70;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.Fare;
            col.Name = COLS.Fare;
            col.Width = 50;
            col.ReadOnly = false;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Ret. Fare";
            col.Name = COLS.ReturnFare;
            col.Width = 75;
            grdBookings.Columns.Add(col);





            GridViewCommandColumn cmdCol = new GridViewCommandColumn();
            cmdCol.Width = 50;
            cmdCol.Name = "Update";
            cmdCol.ImageLayout=
            cmdCol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdCol.DefaultText = "Update";
            cmdCol.UseDefaultText = true;
            cmdCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdBookings.Columns.Add(cmdCol);
            cmdCol=new GridViewCommandColumn ();
            cmdCol.Width = 50;
            grdBookings.CommandCellClick += new CommandCellClickEventHandler(grdBookings_CommandCellClick);
            cmdCol.Name = "View";
            cmdCol.UseDefaultText = true;
            cmdCol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdCol.DefaultText = "View";
            cmdCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdBookings.Columns.Add(cmdCol);

            

            cmdCol = new GridViewCommandColumn();
            col.BestFit();

            cmdCol.Name = "btnDelete";
            cmdCol.UseDefaultText = true;
            cmdCol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdCol.DefaultText = "Delete";
            cmdCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdBookings.Columns.Add(cmdCol);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.StatusId;
            grdBookings.Columns.Add(col);



            ConditionalFormattingObject objExcludedJobs = new ConditionalFormattingObject();

            objExcludedJobs.ApplyToRow = true;
            objExcludedJobs.RowBackColor = Color.LightPink;
            objExcludedJobs.ConditionType = ConditionTypes.Equal;
            objExcludedJobs.TValue1 = "1";
            objExcludedJobs.TValue2 = "1";

            grdBookings.Columns[COLS.EXCLUDED].ConditionalFormattingObjectList.Add(objExcludedJobs);



            ConditionalFormattingObject objChangedJobs = new ConditionalFormattingObject();

            objChangedJobs.ApplyToRow = true;
            objChangedJobs.RowBackColor = Color.YellowGreen;
            objChangedJobs.ConditionType = ConditionTypes.Equal;
            objChangedJobs.TValue1 = "1";
            objChangedJobs.TValue2 = "1";

            grdBookings.Columns[COLS.CHANGED].ConditionalFormattingObjectList.Add(objChangedJobs);

        }
        public void UpdateBooking()
        {
            try
            {
                objMaster = new BookingBO();
                // BookingBO booking = new BookingBO();
                long Id = grdBookings.CurrentRow.Cells[COLS.ID].Value.ToLong();
                //booking.GetByPrimaryKey(grdBookings.CurrentRow.Cells[COLS.ID].Value.ToLong());
                objMaster.GetByPrimaryKey(Id);
                //booking.Edit();
                objMaster.Edit();
                objMaster.Current.PickupDateTime = grdBookings.CurrentRow.Cells[COLS.PickupDate].Value.ToDateTimeorNull();

                objMaster.Current.ReturnPickupDateTime = grdBookings.CurrentRow.Cells[COLS.ReturnPickupDate].Value.ToDateTimeorNull();


                objMaster.Current.FareRate = grdBookings.CurrentRow.Cells[COLS.Fare].Value.ToDecimal();
                objMaster.Current.ReturnFareRate = grdBookings.CurrentRow.Cells[COLS.ReturnFare].Value.ToDecimal();

                // New Working 

                int FromLocTypeId = grdBookings.CurrentRow.Cells[COLS.FromLocTypeId].Value.ToInt();

                int ToLocTypeId = grdBookings.CurrentRow.Cells[COLS.ToLocTypeId].Value.ToInt();

                objMaster.Current.FromLocTypeId = FromLocTypeId;
                objMaster.Current.ToLocTypeId = ToLocTypeId;
                objMaster.Current.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
                objMaster.Current.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();


                //     objMaster.Current.ReturnFromLocId = ddlReturnFromAirport.SelectedValue.ToIntorNull();


                if (FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromLocTypeId == Enums.LOCATION_TYPES.BASE)
                    objMaster.Current.FromAddress = grdBookings.CurrentRow.Cells[COLS.FromAddress].Value.ToStr();

                else if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    objMaster.Current.FromAddress = grdBookings.CurrentRow.Cells[COLS.FromPostCode].Value.ToStr();
                else
                    objMaster.Current.FromAddress = grdBookings.CurrentRow.Cells[COLS.FromAddress].Value.ToStr();
                //else
                //{
                //    booking.Current.FromAddress = ddlFromLocation.Text.Trim();
                //}



                objMaster.Current.FromDoorNo = grdBookings.CurrentRow.Cells[COLS.FromDoorNo].Value.ToStr();
                objMaster.Current.FromStreet = grdBookings.CurrentRow.Cells[COLS.FromStreet].Value.ToStr();
                objMaster.Current.FromPostCode = grdBookings.CurrentRow.Cells[COLS.FromPostCode].Value.ToStr();



                if (ToLocTypeId == Enums.LOCATION_TYPES.ADDRESS || ToLocTypeId == Enums.LOCATION_TYPES.BASE)
                    objMaster.Current.ToAddress = grdBookings.CurrentRow.Cells[COLS.ToAddress].Value.ToStr();

                else if (ToLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    objMaster.Current.ToAddress = grdBookings.CurrentRow.Cells[COLS.ToAddress].Value.ToStr();
                else
                    objMaster.Current.ToAddress = grdBookings.CurrentRow.Cells[COLS.ToAddress].Value.ToStr();
                //objMaster.Current.FromAddress = "ABC";
                //objMaster.Current.ToAddress = "XYZ";
                //else
                //{
                //    booking.Current.ToAddress = ddlToLocation.Text.Trim();
                //}

                //if (AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool())
                //{

                //    if (ddlPickupPlot.SelectedValue == null)
                //        objMaster.Current.ZoneId = GetZoneId(objMaster.Current.FromAddress);
                //    else
                //        objMaster.Current.ZoneId = ddlPickupPlot.SelectedValue.ToIntorNull();

                //    if (ddlDropOffPlot.SelectedValue == null)
                //        objMaster.Current.DropOffZoneId = GetZoneId(objMaster.Current.ToAddress);
                //    else
                //        objMaster.Current.DropOffZoneId = ddlDropOffPlot.SelectedValue.ToIntorNull();
                //}


                objMaster.Current.ToDoorNo = grdBookings.CurrentRow.Cells[COLS.ToDoorNo].Value.ToStr();
                objMaster.Current.ToStreet = grdBookings.CurrentRow.Cells[COLS.ToStreet].Value.ToStr();
                objMaster.Current.ToPostCode = grdBookings.CurrentRow.Cells[COLS.ToPostCode].Value.ToStr();

                objMaster.Current.FareRate = grdBookings.CurrentRow.Cells[COLS.Fare].Value.ToDecimal();

                objMaster.Current.PickupDateTime = grdBookings.CurrentRow.Cells[COLS.PickupDate].Value.ToDateTime();
                objMaster.Current.ReturnFareRate = grdBookings.CurrentRow.Cells[COLS.ReturnFare].Value.ToDecimal();
                //
                //if (grdVia != null)
                //{

                //    string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                //    IList<Booking_ViaLocation> savedList = objMaster.Current.Booking_ViaLocations;
                //    List<Booking_ViaLocation> listofDetail = (from r in grdVia.Rows
                //                                              select new Booking_ViaLocation
                //                                              {
                //                                                  Id = r.Cells["ID"].Value.ToLong(),
                //                                                  BookingId = r.Cells["MASTERID"].Value.ToLong(),
                //                                                  ViaLocTypeId = r.Cells["FROMVIALOCTYPEID"].Value.ToIntorNull(),
                //                                                  ViaLocTypeLabel = r.Cells["FROMTYPELABEL"].Value.ToStr(),
                //                                                  ViaLocTypeValue = r.Cells["FROMTYPEVALUE"].Value.ToStr(),

                //                                                  ViaLocId = r.Cells["VIALOCATIONID"].Value.ToIntorNull(),
                //                                                  ViaLocLabel = r.Cells["VIALOCATIONLABEL"].Value.ToStr(),
                //                                                  ViaLocValue = r.Cells["VIALOCATIONVALUE"].Value.ToStr()

                //                                              }).ToList();


                //    Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);
                //}

                objMaster.Save();

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }
        void grdBookings_CommandCellClick(object sender, EventArgs e)
        {

            try
            {


                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == "Update")
                {
                    UpdateBooking();
                }
                else if (gridCell.ColumnInfo.Name.ToStr().ToLower() == "view")
                {
                       ViewDetailForm();
                }

                else if (gridCell.ColumnInfo.Name.ToStr().ToLower() == "btndelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {


                        BookingBO objBO = new BookingBO();
                        try
                        {
                            long jobId = gridCell.RowInfo.Cells["Id"].Value.ToLong();



                            objBO.GetByPrimaryKey(jobId);
                            if (objBO.Current != null)
                            {

                                objBO.Delete(objBO.Current);

                                gridCell.RowInfo.Delete();

                                GridViewRowInfo pickupRow = grdPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS_PICKUPS.ID].Value.ToLong() == jobId);
                                if (pickupRow != null)
                                    pickupRow.Delete();

                                GridViewRowInfo excpickupRow = grdExcludePickupDates.Rows.FirstOrDefault(c => c.Cells[COLS_PICKUPS.ID].Value.ToLong() == jobId);
                                if (excpickupRow != null)
                                    excpickupRow.Delete();



                            }

                        }
                        catch (Exception ex)
                        {

                            if (objBO.Errors.Count > 0)
                                ENUtils.ShowMessage(objBO.ShowErrors());
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



                ENUtils.ShowMessage(ex.Message);

            }               
            
        }


        private void ViewDetailForm()
        {

            if (grdBookings.CurrentRow != null && grdBookings.CurrentRow is GridViewDataRowInfo)
            {
                ShowBookingForm(grdBookings.CurrentRow.Cells["Id"].Value.ToInt());
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }


        private void ShowBookingForm(int id)
        {


            frmBooking frm = new frmBooking();
            frm.OnDisplayRecord(id);
            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();


          

        }

        private void AddBooking(GridViewRowInfo row, string day, DateTime pickupDate,DateTime? returnPickupDate,int? fromLocTypeId,int? fromLocId,
                                string fromAddress,int? toLocTypeId,int? toLocId, string toAddress, decimal fareRate,decimal returnFareRate)
        {

            row.Cells[COLS.DAY].Value = day;
            row.Cells[COLS.PickupDate].Value = string.Format("{0:dd/MM/yyyy HH:mm}", pickupDate);
            row.Cells[COLS.FromAddress].Value = fromAddress;
            row.Cells[COLS.ToAddress].Value = toAddress;
            row.Cells[COLS.Fare].Value = fareRate;

            row.Cells[COLS.FromLocTypeId].Value = fromLocTypeId;
            row.Cells[COLS.FromLocId].Value = fromLocId;

            row.Cells[COLS.ToLocTypeId].Value = toLocTypeId;
            row.Cells[COLS.ToLocId].Value = toLocId;

            row.Cells[COLS.FromDoorNo].Value = ObjBookiing.FromDoorNo.ToStr();
            row.Cells[COLS.FromStreet].Value = ObjBookiing.FromStreet.ToStr();
            row.Cells[COLS.FromPostCode].Value = ObjBookiing.FromPostCode.ToStr();

            row.Cells[COLS.ToDoorNo].Value = ObjBookiing.ToDoorNo.ToStr();
            row.Cells[COLS.ToStreet].Value = ObjBookiing.ToStreet.ToStr();
            row.Cells[COLS.ToPostCode].Value = ObjBookiing.ToPostCode.ToStr();





            row.Cells[COLS.Account].Value = ObjBookiing.CompanyId;


            if (returnPickupDate != null)
            {
                row.Cells[COLS.JourneyTypeId].Value = Enums.JOURNEY_TYPES.RETURN;
                row.Cells[COLS.ReturnPickupDate].Value = string.Format("{0:dd/MM/yyyy HH:mm}", returnPickupDate);
                row.Cells[COLS.ReturnFare].Value = returnFareRate;
            }
            else
            {
                row.Cells[COLS.JourneyTypeId].Value = Enums.JOURNEY_TYPES.ONEWAY;

            }

            if (day == "1")
            {
                row.Cells[COLS.MASTERBOOKING].Value = COLS.MASTERBOOKING;

            }
           
        }

        private void btnCreateBooking_Click(object sender, EventArgs e)
        {
            try
            {
            //    grdBookings.Rows.Clear();
              

               // int days = numDays.Value.ToInt();

                //ObjBookiing.IsCompanyWise = chkIsCompanyRates.Checked;
                //ObjBookiing.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                //ObjBookiing.DepartmentId = ddlDepartment.SelectedValue.ToIntorNull();
                //ObjBookiing.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();


               // CreateBooking(days, chkSame.Checked, chkOrigin.Checked, chkDestination.Checked);

           

                grdBookings.CurrentRow = grdBookings.RowCount == 1 ? grdBookings.Rows[0] : grdBookings.Rows[1];
                SelectRowDetail(grdBookings.CurrentRow);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void CreateBooking(int days,bool sameAsMasterBooking,bool sameOrigin,bool sameDestination  )
        {


            //string fromAddress = ObjBookiing.FromAddress;
            //string toAddress = ObjBookiing.ToAddress;
            //decimal fareRate = ObjBookiing.FareRate.ToDecimal();
            //decimal retFareRate = ObjBookiing.ReturnFareRate.ToDecimal();
       
            //int? fromLocTypeId=ObjBookiing.FromLocTypeId;
            //int? fromLocId=ObjBookiing.FromLocId;

            //int? toLocTypeId=ObjBookiing.ToLocTypeId;
            //int? toLocId=ObjBookiing.ToLocId;


           
            

            //if (sameAsMasterBooking == false)
            //{

            //    if (sameOrigin == false && sameDestination == false)
            //    {
            //        fromLocId = null;
            //        fromAddress = string.Empty;

            //        toLocId = null;
            //        toAddress = string.Empty;
            //    }


            //    if (sameOrigin == false)
            //    {
            //        fromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
            //        fromLocId = null;
            //        fromAddress = string.Empty;
            //    }

            //    if (sameDestination == false)
            //    {

            //        toLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
            //        toLocId = null;
            //        toAddress = string.Empty;
            //    }


            //    if (sameDestination == false || sameOrigin==false)
            //    {
            //        fareRate = 0;

            //    }
            //}

            //grdBookings.RowCount = days;

            //DateTime? pickupDate = null;
            //TimeSpan pickupTime = TimeSpan.Zero;


            //DateTime? returnpickupDate = null;
            //TimeSpan returnpickupTime = TimeSpan.Zero;

            //string day=string.Empty;
            //GridViewRowInfo row = null;
            //string returnPickupTimeStr = string.Empty;

            //bool skipWeekend = chkSkipWeekEnd.Checked;
            //int cnter = 0;

            //for (int i = 1; i <= days; i++)
            //{
            //     day= i.ToStr();
            //     returnpickupDate = null;
            //     retFareRate = 0;
            //      row=   grdPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS_PICKUPS.DAY].Value.ToStr() == day);

            //      if (row != null)
            //      {
            //          TimeSpan.TryParse(row.Cells[COLS_PICKUPS.PickupTime].Value.ToStr(), out pickupTime);
            //          pickupDate = row.Cells[COLS_PICKUPS.PickupDate].Value.ToDate() + pickupTime;

            //          returnPickupTimeStr = row.Cells[COLS_PICKUPS.ReturnPickupTime].Value.ToStr().Trim();
            //          if (!string.IsNullOrEmpty(returnPickupTimeStr))
            //          {

            //              TimeSpan.TryParse(returnPickupTimeStr, out returnpickupTime);
            //              returnpickupDate = row.Cells[COLS_PICKUPS.ReturnPickupDate].Value.ToDate() + returnpickupTime;
            //          }

            //          fareRate = row.Cells[COLS_PICKUPS.Fare].Value.ToDecimal();
            //          retFareRate = row.Cells[COLS_PICKUPS.RetFare].Value.ToDecimal();
            //      }
            //      else
            //      {


            //          pickupDate = ObjBookiing.PickupDateTime.Value.AddDays((i + cnter) - 1);


            //          if (ObjBookiing.ReturnPickupDateTime != null)
            //          {

            //              returnpickupDate =pickupDate.Value.Date+ ObjBookiing.ReturnPickupDateTime.Value.TimeOfDay;
            //              retFareRate = ObjBookiing.ReturnFareRate.ToDecimal();
            //          }
                     

                     
            //          //if (cnter > 0)
            //          //{
            //          //    cnter = 0;
            //          //}

            //         if(skipWeekend)
            //         {
            //             if (pickupDate.Value.DayOfWeek == DayOfWeek.Saturday)
            //             {
            //                 pickupDate = pickupDate.Value.AddDays(2);
            //                 cnter += 2;
            //             }
            //             else if (pickupDate.Value.DayOfWeek == DayOfWeek.Sunday)
            //             {
            //                 pickupDate = pickupDate.Value.AddDays(1);
            //                 cnter+= 1;

            //             }
            //         }


            //          fareRate = ObjBookiing.FareRate.ToDecimal();
            //          retFareRate = ObjBookiing.ReturnFareRate.ToDecimal();
            //      }

            //      AddBooking(grdBookings.Rows[i-1], day, pickupDate.ToDateTime(),returnpickupDate.ToDateTimeorNull(), fromLocTypeId, fromLocId,
            //                            fromAddress, toLocTypeId, toLocId, toAddress, fareRate, retFareRate);

            //}


          
        }



        List<DateTime> listOfPickUpDateTime = new List<DateTime>();
        DateTime nowDate;
        private bool Save()
        {

            bool IsSuccess = true;
            try
            {
                for (int i = 0; i < grdBookings.Rows.Count; i++)
                {
                    if (grdBookings.Rows[i].Cells[COLS.EXCLUDED].Value.ToStr() == "1")
                    {
                        // BookingBO booking = new BookingBO();
                        objMaster = new BookingBO();

                        objMaster.GetByPrimaryKey(grdBookings.Rows[i].Cells[COLS.ID].Value.ToLong());

                        if (objMaster.Current != null)
                        {
                            objMaster.Delete(objMaster.Current);
                        }
                    }
                    else
                    {
                        if (grdBookings.Rows[i].Cells[COLS.CHANGED].Value.ToStr().Trim() == ""
                            || grdBookings.Rows[i].Cells[COLS.PickupDate].Style.BackColor == Color.Gainsboro)
                            continue;
                        else
                        {
                            //BookingBO booking = new BookingBO();
                            objMaster = new BookingBO();
                            objMaster.GetByPrimaryKey(grdBookings.Rows[i].Cells[COLS.ID].Value.ToLong());

                            objMaster.Edit();
                            objMaster.Current.PickupDateTime = grdBookings.Rows[i].Cells[COLS.PickupDate].Value.ToDateTimeorNull();

                            objMaster.Current.ReturnPickupDateTime = grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value.ToDateTimeorNull();


                            objMaster.Current.FareRate = grdBookings.Rows[i].Cells[COLS.Fare].Value.ToDecimal();
                            objMaster.Current.ReturnFareRate = grdBookings.Rows[i].Cells[COLS.ReturnFare].Value.ToDecimal();

                            // New Working 

                            int FromLocTypeId = grdBookings.Rows[i].Cells[COLS.FromLocTypeId].Value.ToInt();

                            int ToLocTypeId = grdBookings.Rows[i].Cells[COLS.ToLocTypeId].Value.ToInt();

                            objMaster.Current.FromLocTypeId = FromLocTypeId;
                            objMaster.Current.ToLocTypeId = ToLocTypeId;
                            objMaster.Current.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
                            objMaster.Current.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();


                            //     objMaster.Current.ReturnFromLocId = ddlReturnFromAirport.SelectedValue.ToIntorNull();


                            if (FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromLocTypeId == Enums.LOCATION_TYPES.BASE)
                                objMaster.Current.FromAddress = grdBookings.Rows[i].Cells[COLS.FromAddress].Value.ToStr();

                            else if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                                objMaster.Current.FromAddress = grdBookings.Rows[i].Cells[COLS.FromPostCode].Value.ToStr();
                            //else
                            //{
                            //    booking.Current.FromAddress = ddlFromLocation.Text.Trim();
                            //}



                            objMaster.Current.FromDoorNo = grdBookings.Rows[i].Cells[COLS.FromDoorNo].Value.ToStr();
                            objMaster.Current.FromStreet = grdBookings.Rows[i].Cells[COLS.FromStreet].Value.ToStr();
                            objMaster.Current.FromPostCode = grdBookings.Rows[i].Cells[COLS.FromPostCode].Value.ToStr();



                            if (ToLocTypeId == Enums.LOCATION_TYPES.ADDRESS || ToLocTypeId == Enums.LOCATION_TYPES.BASE)
                                objMaster.Current.ToAddress = grdBookings.Rows[i].Cells[COLS.ToAddress].Value.ToStr();

                            else if (ToLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                                objMaster.Current.ToAddress = grdBookings.Rows[i].Cells[COLS.ToAddress].Value.ToStr();
                            //else
                            //{
                            //    booking.Current.ToAddress = ddlToLocation.Text.Trim();
                            //}

                            //if (AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool())
                            //{

                            //    if (ddlPickupPlot.SelectedValue == null)
                            //        objMaster.Current.ZoneId = GetZoneId(objMaster.Current.FromAddress);
                            //    else
                            //        objMaster.Current.ZoneId = ddlPickupPlot.SelectedValue.ToIntorNull();

                            //    if (ddlDropOffPlot.SelectedValue == null)
                            //        objMaster.Current.DropOffZoneId = GetZoneId(objMaster.Current.ToAddress);
                            //    else
                            //        objMaster.Current.DropOffZoneId = ddlDropOffPlot.SelectedValue.ToIntorNull();
                            //}


                            objMaster.Current.ToDoorNo = grdBookings.Rows[i].Cells[COLS.ToDoorNo].Value.ToStr();
                            objMaster.Current.ToStreet = grdBookings.Rows[i].Cells[COLS.ToStreet].Value.ToStr();
                            objMaster.Current.ToPostCode = grdBookings.Rows[i].Cells[COLS.ToPostCode].Value.ToStr();

                            objMaster.Current.FareRate = grdBookings.Rows[i].Cells[COLS.Fare].Value.ToDecimal();





                            // DateTime dt = objMaster.Current.PickupDateTime.ToDateTime();
                            objMaster.Current.PickupDateTime = grdBookings.Rows[i].Cells[COLS.PickupDate].Value.ToDateTime();
                            //
                            if (grdBookings.Rows[i].Cells[COLS.StatusId].Value.ToInt() != 2)
                            {
                                if (dtpEndingAt.Value != null && objMaster.Current.PickupDateTime.ToDate() >= dtpStartingAt.Value.ToDate() && dtpEndingAt.Value != null && objMaster.Current.PickupDateTime.ToDate() <= dtpEndingAt.Value.ToDate())
                                {
                                   if (grdVia != null)
                                    {

                                        string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                                        IList<Booking_ViaLocation> savedList = objMaster.Current.Booking_ViaLocations;
                                        List<Booking_ViaLocation> listofDetail = (from r in grdVia.Rows
                                                                                  select new Booking_ViaLocation
                                                                                  {
                                                                                      Id = r.Cells["ID"].Value.ToLong(),
                                                                                      BookingId = r.Cells["MASTERID"].Value.ToLong(),
                                                                                      ViaLocTypeId = r.Cells["FROMVIALOCTYPEID"].Value.ToIntorNull(),
                                                                                      ViaLocTypeLabel = r.Cells["FROMTYPELABEL"].Value.ToStr(),
                                                                                      ViaLocTypeValue = r.Cells["FROMTYPEVALUE"].Value.ToStr(),

                                                                                      ViaLocId = r.Cells["VIALOCATIONID"].Value.ToIntorNull(),
                                                                                      ViaLocLabel = r.Cells["VIALOCATIONLABEL"].Value.ToStr(),
                                                                                      ViaLocValue = r.Cells["VIALOCATIONVALUE"].Value.ToStr()

                                                                                  }).ToList();


                                        Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);
                                    }
                                }

                            }
                            objMaster.Save();

                        }


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
                IsSuccess = false;
            }

            return IsSuccess;

        }

        private bool SendAdvanceBookingConfirmationText(string mobileNo)
        {
            if(string.IsNullOrEmpty(mobileNo))return false;

            bool rtn = true;

            // Advance Booking Confirmation Text
           bool   enableAdvBookingText = AppVars.objPolicyConfiguration.EnableAdvanceBookingSMSConfirmation.ToBool();

            if (enableAdvBookingText  && listOfPickUpDateTime.Count > 0 && this.ObjBookiing!=null && this.ObjBookiing.IsQuotation.ToBool()==false )
            {

                int afterMins = AppVars.objPolicyConfiguration.AdvanceBookingSMSConfirmationMins.ToInt();

                int minDifference=0;
                bool foundAny = false;
                int dayDiff = 0;
                foreach (var pickupTime in listOfPickUpDateTime)
                {
                    dayDiff = pickupTime.Date.Subtract(DateTime.Now.Date).Days;
                    minDifference = pickupTime.TimeOfDay.Subtract(nowDate.TimeOfDay).Minutes;

                    if (dayDiff>0 || afterMins > 0 && minDifference >= afterMins)
                    {
                        foundAny = true;
                        break;
                    }
                }

              

                if (foundAny)
                {
                    string msg = AppVars.objPolicyConfiguration.AdvanceBookingSMSText.ToStr().Trim();
                    object propertyValue = string.Empty;

                    foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                    {
                        switch (tag.TagObjectName)
                        {
                            case "booking":

                                if (tag.TagPropertyValue.Contains('.'))
                                {

                                    string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                    object parentObj = ObjBookiing.GetType().GetProperty(val[0]).GetValue(ObjBookiing, null);

                                    if (parentObj != null)
                                    {
                                        propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                    }
                                    else
                                        propertyValue = string.Empty;


                                    break;
                                }
                                else
                                {
                                    propertyValue = ObjBookiing.GetType().GetProperty(tag.TagPropertyValue).GetValue(ObjBookiing, null);
                                }


                                if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                                {
                                    propertyValue = ObjBookiing.GetType().GetProperty(tag.TagPropertyValue2).GetValue(ObjBookiing, null);
                                }
                                break;


                            default:
                                propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                                break;

                        }


                        msg = msg.Replace(tag.TagMemberValue,
                            tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                    }


                    msg.Replace("\n\n", "\n");

                    string refMsg = "";
                    rtn = General.SendAdvanceBookingSMS(mobileNo, ref refMsg, msg, ObjBookiing.SMSType.ToInt());

                }
            }

            return rtn;

            //

        }

        private void grdBookings_Click(object sender, EventArgs e)
        {

        }

        private void radPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool _Saved;

        public bool Saved
        {
            get { return _Saved; }
            set { _Saved = value; }
        }


        private void btnSaveBooking_Click(object sender, EventArgs e)
        {
            Saved = Save();
            
                this.Close();
            
        }

        private void radCheckBox1_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            // btnCreateBooking.Enabled= args.NewValue== Telerik.WinControls.Enumerations.ToggleState.On;

            //if (args.NewValue == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    chkOrigin.Visible = false;
            //    chkDestination.Visible = false;

            //    chkOrigin.Checked = true;
            //    chkDestination.Checked = true;
            //}
            //else
            //{
            //    chkOrigin.Visible = true;
            //    chkDestination.Visible = true;

            //}
        }


       

        private void grdBookings_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            SelectRowDetail(e.Row);
        }


        private void SelectRowDetail(GridViewRowInfo row)
        {
            //if (row != null && row is GridViewDataRowInfo)
            //{
            //    lblBookingDay.Text = "Day " + row.Cells[COLS.DAY].Value.ToStr();
            //    lblBookingDay.Tag = row.Cells[COLS.DAY].Value.ToInt();

            //    ddlFromLocType.SelectedValue = row.Cells[COLS.FromLocTypeId].Value;
            //    ddlToLocType.SelectedValue = row.Cells[COLS.ToLocTypeId].Value;
            //    ddlFromLocation.SelectedValue = row.Cells[COLS.FromLocId].Value;
            //    ddlToLocation.SelectedValue = row.Cells[COLS.ToLocId].Value;


            //    txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            //    txtFromAddress.Text = row.Cells[COLS.FromAddress].Value.ToStr();
            //    txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            //    txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            //    txtToAddress.Text = row.Cells[COLS.ToAddress].Value.ToStr();
            //    txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            //    dtpPickupDate.Value = row.Cells[COLS.PickupDate].Value.ToDateorNull();
            //    dtpPickupTime.Value = row.Cells[COLS.PickupDate].Value.ToDateTimeorNull();


            //    if (row.Cells[COLS.ReturnPickupDate].Value != numReturnFareRate)
            //    {
            //        dtpReturnPickupDate.Value = row.Cells[COLS.ReturnPickupDate].Value.ToDate();
            //        dtpReturnPickupTime.Value = row.Cells[COLS.ReturnPickupDate].Value.ToDateTime();
            //        numReturnFareRate.Value = row.Cells[COLS.ReturnFare].Value.ToDecimal();
            //    }              

            //}
        }

        private void chkAutoDespatch_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //numBeforeMinutes.Enabled = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
        }

        private void btnAddPickupDate_Click(object sender, EventArgs e)
        {
            AddPickupDate();       
        }

        private void AddPickupDate()
        {
            //string day = ddlPickupDay.SelectedValue.ToStr().Trim();
            //DateTime? pickupdate = dtp_StartPickupDate.Value;
            //DateTime? pickuptime = dtp_StartPickupTime.Value;
            //DateTime? returnpickupdate = dtp_ReturnStartPickupDate.Value;
            //DateTime? returnpickuptime = dtp_ReturnStartPickupTime.Value;
            //decimal fare = numFareRate.Value.ToDecimal();
            //decimal retFare = numReturnFare.Value.ToDecimal();

            //bool IsReturn = chkIsReturn.Checked;

            //string error = string.Empty;



            //if (string.IsNullOrEmpty(day))
            //{
            //    error += "Required : Day" + Environment.NewLine;
            //}
            //if (pickupdate == null)
            //{

            //    error += "Required : Pickup Date" + Environment.NewLine;

            //}

            //if (pickuptime == null)
            //{

            //    error += "Required : Pickup Time";

            //}

            //if (IsReturn)
            //{
            //    if (returnpickupdate == null)
            //    {

            //        error += "Required : Return Pickup Date" + Environment.NewLine;

            //    }

            //    if (returnpickuptime == null)
            //    {

            //        error += "Required : Return Pickup Time";

            //    }
            //}

            //if (!string.IsNullOrEmpty(error))
            //{
            //    ENUtils.ShowMessage(error);
            //    return;
            //}


            //pickupdate=pickupdate.ToDate();
            //returnpickupdate = returnpickupdate.ToDateorNull();



            //if (IsReturn)
            //{
            //    if (pickupdate > returnpickupdate || (pickupdate>=returnpickupdate && pickuptime.Value.TimeOfDay>returnpickuptime.Value.TimeOfDay))
            //    {
            //        ENUtils.ShowMessage("Day " + day + " :" + " Pickup Date Time must be less than Return Pickup Date Time");
            //        return;
            //    }
            //}


            //GridViewRowInfo row = null;
            //if (grdPickupDates.CurrentRow != null)
            //{
            //    if (grdPickupDates.CurrentRow is GridViewNewRowInfo)
            //        grdPickupDates.CurrentRow = null;

            //    if (grdPickupDates.CurrentRow is GridViewDataRowInfo)
            //        row = grdPickupDates.CurrentRow;

            //}






            //if (grdPickupDates.Rows.Count(c => c.Cells[COLS.DAY].Value.ToStr().Equals(day)) > 0)
            //    row = grdPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS.DAY].Value.ToStr() == day);
            //else
            //{

            //    if (grdPickupDates.Rows.Count >= day.ToInt())
            //    {
            //        ENUtils.ShowMessage("Total Days Limit exceed." + Environment.NewLine + "You have to Enter more Days before Adding it into a Grid");
            //        return;
            //    }

            //    row =grdPickupDates.Rows.AddNew();

            //}


            //row.Cells[COLS_PICKUPS.DAY].Value = day;
            //row.Cells[COLS_PICKUPS.PickupDate].Value =string.Format("{0:dd/MM/yyyy}",pickupdate);
            //row.Cells[COLS_PICKUPS.PickupTime].Value = string.Format("{0:HH:mm}", pickuptime);

            //row.Cells[COLS_PICKUPS.ReturnPickupDate].Value = string.Format("{0:dd/MM/yyyy}", returnpickupdate);
            //row.Cells[COLS_PICKUPS.ReturnPickupTime].Value = string.Format("{0:HH:mm}", returnpickuptime);

            //row.Cells[COLS_PICKUPS.Fare].Value = fare;
            //row.Cells[COLS_PICKUPS.RetFare].Value = retFare;


            //ddlPickupDay.SelectedIndex = ++ddlPickupDay.SelectedIndex;


            //SetPickupDateGridNull();

        }

        private void SetPickupDateGridNull()
        {

            //grdPickupDates.CurrentRow = null;
            //dtp_StartPickupDate.Focus();
        }



        


 

        private void numDays_Validated(object sender, EventArgs e)
        {


            FillDaysCombo();

        }

        private void FillDaysCombo()
        {


            //int days = numDays.Value.ToInt();

            //ddlPickupDay.Items.Clear();
       

            //RadListDataItem item = null;
       
            //for (int i = 1; i <= days; i++)
            //{

            //    item = new RadListDataItem();
            //    item.Text = i.ToStr();
            //    item.Value = item.Text;             

            //    ddlPickupDay.Items.Add(item);
     
            //}


            //ddlPickupDay.SelectedValue = "1";
    
        }

        private void chkIsReturn_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {

            SetReturnPickupDate(args.NewValue);
        }

        private void SetReturnPickupDate(ToggleState toggle)
        {

            //if (toggle == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    pnlReturn.Enabled = true;


            //    dtp_ReturnStartPickupDate.Value = dtp_StartPickupDate.Value;
            //    dtp_ReturnStartPickupTime.Value = dtp_StartPickupTime.Value;
            //    numReturnFare.Enabled = true;
            //}
            //else
            //{
              

            //    pnlReturn.Enabled = false;
            //    dtp_ReturnStartPickupDate.Value = null;
            //    dtp_ReturnStartPickupTime.Value = null;

            //    numReturnFare.Enabled = false;
            //    numReturnFare.Value = 0;

            //}

        }

        private void ddlPickupDay_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //string day = ddlPickupDay.SelectedValue.ToStr().Trim();



            //if (string.IsNullOrEmpty(day)) return;


            //GridViewRowInfo row = grdPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS_PICKUPS.DAY].Value.ToStr() == day);

            //if (row != null)
            
            //{
            //    DateTime? pickupDate = row.Cells[COLS_PICKUPS.PickupDate].Value.ToDate();
            //    DateTime pickupTime = row.Cells[COLS_PICKUPS.PickupTime].Value.ToDateTime();

            //    dtp_StartPickupDate.Value = pickupDate;
            //    dtp_StartPickupTime.Value = pickupTime;

            //    if (row.Cells[COLS_PICKUPS.ReturnPickupDate].Value.ToStr() != string.Empty)
            //    {
            //        pickupDate = row.Cells[COLS_PICKUPS.ReturnPickupDate].Value.ToDate();

            //        pickupTime = row.Cells[COLS_PICKUPS.ReturnPickupTime].Value.ToDateTime();

            //        dtp_ReturnStartPickupDate.Value = pickupDate;
            //        dtp_ReturnStartPickupTime.Value = pickupTime;
                    


            //    }



            //}

        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            try
            {


                

           //     CalculateFares();

            

        

            }
            catch (Exception ex)
            {

             

            }
        }



        List<decimal> milesList = new List<decimal>();

      

        char[] separatorArr = new char[] { ' ' };
        private decimal GetSurchargeRate(string postCode)
        {
            decimal percentage = 0.00m;
            string[] splitPostCode = postCode.Split(separatorArr);
            if (splitPostCode.Count() > 1)
            {

                Gen_SysPolicy_SurchargeRate obj = General.GetObject<Gen_SysPolicy_SurchargeRate>(c => c.SysPolicyId != null && c.PostCode.Trim().ToLower() == splitPostCode[0].Trim().ToLower());

                if (obj != null)
                    percentage = obj.Percentage.ToDecimal();
            }

            return percentage;

        }

        private decimal CalculateDistance(string origin, string destination)
        {
            decimal miles = 0.00m;

                     

             string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + "&sensor=false";



                XmlTextReader reader = new XmlTextReader(url2);
                reader.WhitespaceHandling = WhitespaceHandling.Significant;
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
                DataTable dt = ds.Tables["distance"];
                if (dt != null)
                {
                    var rows = dt.Rows.OfType<DataRow>().Where(c => c[0].ToStr().Trim() == c[1].ToStr().Strip("m").Trim()).ToList();

                    decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                    decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                    decimal milKM = 0.621m;
                    decimal milMeter = 0.00062137119m;

                    miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                }
           

            return miles;
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            //if (grdPickupDates.CurrentRow == null || grdPickupDates.CurrentRow is GridViewDataRowInfo == false) return;


            //grdPickupDates.CurrentRow.Delete();
           
        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCalculateFares_Click(object sender, EventArgs e)
        {
         //   CalculateFares();
        }

       

        private void frmMultiBooking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void radRadioButton1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (args.ToggleState == ToggleState.On)
            //{
            //    radPageView1.Pages[1].Item.Visibility = ElementVisibility.Collapsed;
            //    radPageView1.SelectedPage = pg_Daily;
            //}
            //else
            //{
            //    radPageView1.Pages[1].Item.Visibility = ElementVisibility.Visible;


            //}

        }

        private void optWeekly_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (args.ToggleState == ToggleState.On)
            //{
            //    radPageView1.Pages[0].Item.Visibility = ElementVisibility.Collapsed;
            //    radPageView1.SelectedPage = pg_weekly;
            //}
            //else
            //{
            //    radPageView1.Pages[0].Item.Visibility = ElementVisibility.Visible;

            //}
        }
        
        private void btnWeeklyCreateBooking_Click(object sender, EventArgs e)
        {
            try
            {
             
                //ObjBookiing.IsCompanyWise = chkIsCompanyRates.Checked;
                //ObjBookiing.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                //ObjBookiing.DepartmentId = ddlDepartment.SelectedValue.ToIntorNull();
                //ObjBookiing.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();


                if (dtpPickupTime.Value == null)
                {
                    ENUtils.ShowMessage("Required Pickup Time");
                    return;

                }

               
                                
                
                CreateWeeklyBooking();

               // grdBookings.CurrentRow = grdBookings.RowCount == 1 ? grdBookings.Rows[0] : grdBookings.Rows[1];
               // SelectRowDetail(grdBookings.CurrentRow);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void CreateWeeklyBooking()
        {
            try
            {


                string fromAddress = ObjBookiing.FromAddress;

                string toAddress = ObjBookiing.ToAddress;
                decimal fareRate = numPickupFares.Value;
                decimal retFareRate = numReturnFares.Value;

                int? fromLocTypeId = ObjBookiing.FromLocTypeId;
                int? fromLocId = ObjBookiing.FromLocId;

                int? toLocTypeId = ObjBookiing.ToLocTypeId;
                int? toLocId = ObjBookiing.ToLocId;


                int bookingsToCreate = 0;
            //    int weeksCnt = numWeeks.Value.ToInt();

               // int weekDaysCnt = 0;

                DateTime? startingAt = dtpStartingAt.Value.Value.Date;


                List<string> daysList = new List<string>();


                //if (chkAutoRecurred.Checked == false)
                //{

                //    if (chkMon.Checked)
                //    {
                //        weekDaysCnt++;

                //        daysList.Add("monday");

                //    }
                //    if (chkTue.Checked)
                //    {
                //        weekDaysCnt++;
                //        daysList.Add("tuesday");
                //    }

                //    if (chkWed.Checked)
                //    {
                //        weekDaysCnt++;
                //        daysList.Add("wednesday");
                //    }

                //    if (chkThurs.Checked)
                //    {
                //        weekDaysCnt++;
                //        daysList.Add("thursday");
                //    }


                //    if (chkFri.Checked)
                //    {
                //        weekDaysCnt++;
                //        daysList.Add("friday");
                //    }

                //    if (chkSat.Checked)
                //    {
                //        weekDaysCnt++;
                //        daysList.Add("saturday");
                //    }


                //    if (chkSun.Checked)
                //    {
                //        weekDaysCnt++;
                //        daysList.Add("sunday");
                //    }

                //    bookingsToCreate = weeksCnt * weekDaysCnt;


                //}


                if (bookingsToCreate == 0)
                {
                    ENUtils.ShowMessage("No Bookings To Create");
                    return;
                }

                if (startingAt == null)
                {
                    ENUtils.ShowMessage("Please specify starting at");
                    return;

                }
                else
                {
                    DateTime? matchStartDate = GetStartingPoint();

                    if (startingAt < matchStartDate)
                    {
                        ENUtils.ShowMessage("Starting Date must be Start from " + string.Format("{0:dd/MM/yyyy}", matchStartDate));
                        return;
                    }

                }


                DateTime? pickupDate = startingAt;
               
               // DateTime? returnpickupDate = null;
                
                string day = string.Empty;

                string returnPickupTimeStr = string.Empty;

                //bool skipWeekend = chkSkipWeekEnd.Checked;

                //string dayOfWeek = string.Empty;

                //grdBookings.RowCount = bookingsToCreate;

                //int rowCnt = 0;

                //for (int i = 1; i <= weeksCnt; i++)
                //{

                //    for (int j = 0; j < daysList.Count; j++)
                //    {
                //        while (pickupDate.Value.DayOfWeek.ToStr().ToLower() != daysList[j])
                //            pickupDate = pickupDate.Value.AddDays(1);
                        

                //        pickupDate = pickupDate.Value.Date + ((TimeSpan)dtpWeekPickupTime.Value.Value.TimeOfDay);




                //        if (chkReturnWeekJourney.Checked && dtpWeekReturnPickupTime.Value!=null)
                //        {
                //            returnpickupDate = pickupDate.Value.Date;
                //            returnpickupDate = returnpickupDate.Value.ToDate() + ((TimeSpan)dtpWeekReturnPickupTime.Value.Value.TimeOfDay);


                //        }

                //        AddBooking(grdBookings.Rows[rowCnt], string.Format("{0:ddd}",pickupDate), pickupDate.ToDateTime(), returnpickupDate.ToDateTimeorNull(), fromLocTypeId, fromLocId,
                //                             fromAddress, toLocTypeId, toLocId, toAddress, fareRate, retFareRate);


                //        rowCnt++;
                //    }

                //    pickupDate = pickupDate.Value.AddDays(1);

                //}

              
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }


        }

        private void chkMon_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            DateTime currentDate = DateTime.Now;

            //if (chkMon.Checked)
            //    SetStartingPoint(ref currentDate, "monday");

           
            //else  if(chkTue.Checked)
            //   SetStartingPoint(ref currentDate, "tuesday");

            //else if (chkWed.Checked)
            //    SetStartingPoint(ref currentDate, "wednesday");


            //else if (chkThurs.Checked)
            //    SetStartingPoint(ref currentDate, "thursday");


            //else if (chkFri.Checked)
            //    SetStartingPoint(ref currentDate, "friday");


            //else if (chkSat.Checked)
            //    SetStartingPoint(ref currentDate, "saturday");


            //else if (chkSun.Checked)
            //    SetStartingPoint(ref currentDate, "sunday");


            dtpStartingAt.Value = currentDate;
           
        }


        private void SetStartingPoint(ref DateTime currentDate,string day)
        {

            while (currentDate.DayOfWeek.ToStr().ToLower() != day)
                currentDate = currentDate.AddDays(1);
           

        }


        private DateTime GetStartingPoint()
        {

            DateTime currentDate = DateTime.Now;
            string day = string.Empty;

            //if (chkMon.Checked)
            //    day = "monday";
            


            //else if (chkTue.Checked)
            //    day = "tuesday";

            //else if (chkWed.Checked)
            //      day = "wednesday";



            //else if (chkThurs.Checked)
            //    day = "thursday";


            //else if (chkFri.Checked)
            //    day = "friday";


            //else if (chkSat.Checked)
            //    day = "satruday";


            //else if (chkSun.Checked)
            //    day = "sunday";





            while (currentDate.DayOfWeek.ToStr().ToLower() != day)
                currentDate = currentDate.AddDays(1);

           

            return currentDate.Date;

        }

        private void btnCalculateWeekFare_Click(object sender, EventArgs e)
        {
          //  CalculateWeekFares();
        }

        private void chkReturnWeekJourney_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            //if (chkReturnWeekJourney.ToggleState == ToggleState.On)
            //{

            //    numWeekFareRateReturn.Enabled = true;
            //    dtpWeekReturnPickupTime.Enabled = true;
            //    dtpWeekReturnPickupTime.Value = DateTime.Now;
            //}
            //else
            //{
            //    numWeekFareRateReturn.Value = 0;
            //    dtpWeekReturnPickupTime.Value = null;

            //}
        }

        private void btnExclude_Click(object sender, EventArgs e)
        {
            ExcludeBooking(grdPickupDates.CurrentRow);


        }


        private void ExcludeBooking(GridViewRowInfo row)
        {
            if (row!=null && row is GridViewDataRowInfo)
            {
               GridViewRowInfo excRow=    grdExcludePickupDates.Rows.AddNew();

               excRow.Cells[COLS_PICKUPS.ID].Value = row.Cells[COLS_PICKUPS.ID].Value;
               excRow.Cells[COLS_PICKUPS.DAY].Value = row.Cells[COLS_PICKUPS.DAY].Value;
               excRow.Cells[COLS_PICKUPS.PickupDate].Value = row.Cells[COLS_PICKUPS.PickupDate].Value;


               grdPickupDates.Rows.Remove(row);

            }


        }


        private void IncludeBooking(GridViewRowInfo row)
        {
            if (row != null && row is GridViewDataRowInfo)
            {
                GridViewRowInfo incRow = grdPickupDates.Rows.AddNew();

                incRow.Cells[COLS_PICKUPS.ID].Value = row.Cells[COLS_PICKUPS.ID].Value;
                incRow.Cells[COLS_PICKUPS.DAY].Value = row.Cells[COLS_PICKUPS.DAY].Value;
                incRow.Cells[COLS_PICKUPS.PickupDate].Value = row.Cells[COLS_PICKUPS.PickupDate].Value;


                grdExcludePickupDates.Rows.Remove(row);

            }


        }

        private void btnInclude_Click(object sender, EventArgs e)
        {
            IncludeBooking(grdExcludePickupDates.CurrentRow);
        }

        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            ApplyChanges();
        }


        private void ApplyChanges()
        {
            try
            {

                grdBookings.Rows.ToList().ForEach(c => c.Cells[COLS.EXCLUDED].Value = "");
                grdBookings.Rows.ToList().ForEach(c => c.Cells[COLS.CHANGED].Value = "");

                GridViewRowInfo row = null;
                for (int i = 0; i < grdExcludePickupDates.Rows.Count; i++)
                {
                    row = grdBookings.Rows.FirstOrDefault(c => c.Cells[COLS.ID].Value.ToLong() == grdExcludePickupDates.Rows[i].Cells[COLS_PICKUPS.ID].Value.ToLong());

                    if (row != null)
                    {

                        row.Cells[COLS.EXCLUDED].Value = "1";

                    }


                }




                if (chkStartingAt.Enabled == false && dtpStartingAt.Value == null)
                {
                    ENUtils.ShowMessage("Required : Beginning From");
                    return;


                }


                if (chkEndingAt.Enabled == false && dtpEndingAt.Value == null)
                {
                    ENUtils.ShowMessage("Required : Beginning From");
                    return;


                }



                if (chkPickupTime.Checked && dtpPickupTime.Value == null)
                {
                    ENUtils.ShowMessage("Required : Please Enter Pickup Time to Change");
                    return;

                }


                if (chkReturnPickupTime.Checked && dtpReturnPickupTime.Value == null)
                {
                    ENUtils.ShowMessage("Required : Please Enter Return Pickup Time to Change");
                    return;

                }




                int journeyTypeId = ddlJourneyType.SelectedIndex == 1 ? 2 : 1;




                int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
                int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
                int? fromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
                int? toLocId = ddlToLocation.SelectedValue.ToIntorNull();

                string fromAddress = txtFromAddress.Text.Trim();
                string toAddress = txtToAddress.Text.Trim();

                string fromDoorNo = txtFromFlightDoorNo.Text.Trim();
                string toDoorNo = txtToFlightDoorNo.Text.Trim();
                string fromStreet = txtFromStreetComing.Text.Trim();
                string fromPostCode = txtFromPostCode.Text.Trim();
                string toPostCode = txtToPostCode.Text.Trim();
                string toStreet = txtToStreetComing.Text.Trim();



                if (chkStartingAt.Checked && chkEndingAt.Checked)
                {



                    foreach (var gRow in grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == ""))
                    {

                        if (chkPickupTime.Enabled && dtpPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;
                        }


                        if (chkPickupFares.Enabled)
                        {

                            gRow.Cells[COLS.Fare].Value = numPickupFares.Value;
                        }




                        if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;


                            

                        }


                        if (gRow.Cells[COLS.MasterJobId].Value.ToLong() > 0 && chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                        }


                        if (chkReturnFares.Enabled)
                        {
                            gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                        }



                        if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                           || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                        {

                            if (chkPickup.Checked)
                            {

                                gRow.Cells[COLS.FromLocTypeId].Value = fromLocTypeId;
                                gRow.Cells[COLS.FromLocId].Value = fromLocId;
                                gRow.Cells[COLS.FromDoorNo].Value = fromDoorNo;
                                gRow.Cells[COLS.FromStreet].Value = fromStreet;


                                if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                    gRow.Cells[COLS.FromAddress].Value = fromAddress;
                                else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                {

                                    gRow.Cells[COLS.FromAddress].Value = fromPostCode;
                                }
                                else
                                {

                                    gRow.Cells[COLS.FromAddress].Value = ddlFromLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }

                            if (chkDestination.Checked)
                            {

                                gRow.Cells[COLS.ToLocTypeId].Value = toLocTypeId;
                                gRow.Cells[COLS.ToLocId].Value = toLocId;
                                gRow.Cells[COLS.ToDoorNo].Value = toDoorNo;
                                gRow.Cells[COLS.ToStreet].Value = toStreet;
                                //   gRow.Cells[COLS.ToAddress].Value = toAddress;


                                if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                    gRow.Cells[COLS.ToAddress].Value = toAddress;
                                else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                {

                                    gRow.Cells[COLS.ToAddress].Value = toPostCode;
                                }
                                else
                                {

                                    gRow.Cells[COLS.ToAddress].Value = ddlToLocation.Text.ToStr().ToUpper().Trim();
                                }


                                if (gRow.Cells[COLS.MasterJobId].Value != null)
                                {


                                }

                            }

                        }
                        //else
                        //{
                        //    continue;

                        //}



                        gRow.Cells[COLS.CHANGED].Value = "1";


                    }
                }
                else if (chkStartingAt.Checked && chkEndingAt.Checked == false)
                {
                    DateTime endingAt = dtpEndingAt.Value.ToDate();

                    foreach (var gRow in grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == "" && c.Cells[COLS.PickupDate].Value.ToDate() <= endingAt))
                    {

                        if (chkPickupTime.Enabled && dtpPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;
                        }

                        if (chkPickupFares.Enabled)
                        {
                            gRow.Cells[COLS.Fare].Value = numPickupFares.Value;
                        }

                        if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                        }


                        if (gRow.Cells[COLS.MasterJobId].Value.ToLong() > 0 && chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                        }


                        if (chkReturnFares.Enabled)
                        {
                            gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                        }


                        if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                          || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                        {
                            if (chkPickup.Checked)
                            {

                                gRow.Cells[COLS.FromLocTypeId].Value = fromLocTypeId;
                                gRow.Cells[COLS.FromLocId].Value = fromLocId;
                                gRow.Cells[COLS.FromDoorNo].Value = fromDoorNo;
                                gRow.Cells[COLS.FromStreet].Value = fromStreet;


                                if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                    gRow.Cells[COLS.FromAddress].Value = fromAddress;
                                else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                {

                                    gRow.Cells[COLS.FromAddress].Value = fromPostCode;
                                }
                                else
                                {

                                    gRow.Cells[COLS.FromAddress].Value = ddlFromLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }


                            if (chkDestination.Checked)
                            {

                                gRow.Cells[COLS.ToLocTypeId].Value = toLocTypeId;
                                gRow.Cells[COLS.ToLocId].Value = toLocId;
                                gRow.Cells[COLS.ToDoorNo].Value = toDoorNo;
                                gRow.Cells[COLS.ToStreet].Value = toStreet;
                                //   gRow.Cells[COLS.ToAddress].Value = toAddress;


                                if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                    gRow.Cells[COLS.ToAddress].Value = toAddress;
                                else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                {

                                    gRow.Cells[COLS.ToAddress].Value = toPostCode;
                                }
                                else
                                {

                                    gRow.Cells[COLS.ToAddress].Value = ddlToLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }
                        }

                        gRow.Cells[COLS.CHANGED].Value = "1";
                    }
                }
                else if (chkStartingAt.Checked == false && chkEndingAt.Checked == true)
                {
                    DateTime startingAt = dtpStartingAt.Value.ToDate();

                    foreach (var gRow in grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == "" && c.Cells[COLS.PickupDate].Value.ToDate() >= startingAt))
                    {

                        if (chkPickupTime.Enabled && dtpPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;
                        }

                        if (chkPickupFares.Enabled)
                        {
                            gRow.Cells[COLS.Fare].Value = numPickupFares.Value;
                        }

                        if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                        }


                        if (gRow.Cells[COLS.MasterJobId].Value.ToLong() > 0 && chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                        }


                        if (chkReturnFares.Enabled)
                        {
                            gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                        }



                        if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                         || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                        {
                            if (chkPickup.Checked)
                            {

                                gRow.Cells[COLS.FromLocTypeId].Value = fromLocTypeId;
                                gRow.Cells[COLS.FromLocId].Value = fromLocId;
                                gRow.Cells[COLS.FromDoorNo].Value = fromDoorNo;
                                gRow.Cells[COLS.FromStreet].Value = fromStreet;


                                if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                    gRow.Cells[COLS.FromAddress].Value = fromAddress;
                                else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                {

                                    gRow.Cells[COLS.FromAddress].Value = fromPostCode;
                                }
                                else
                                {

                                    gRow.Cells[COLS.FromAddress].Value = ddlFromLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }


                            if (chkDestination.Checked)
                            {

                                gRow.Cells[COLS.ToLocTypeId].Value = toLocTypeId;
                                gRow.Cells[COLS.ToLocId].Value = toLocId;
                                gRow.Cells[COLS.ToDoorNo].Value = toDoorNo;
                                gRow.Cells[COLS.ToStreet].Value = toStreet;
                                //   gRow.Cells[COLS.ToAddress].Value = toAddress;


                                if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                    gRow.Cells[COLS.ToAddress].Value = toAddress;
                                else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                {

                                    gRow.Cells[COLS.ToAddress].Value = toPostCode;
                                }
                                else
                                {

                                    gRow.Cells[COLS.ToAddress].Value = ddlToLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }
                        }

                        gRow.Cells[COLS.CHANGED].Value = "1";
                    }
                }
                else
                {

                    DateTime startingAt = dtpStartingAt.Value.ToDate();

                    DateTime endingAt = dtpEndingAt.Value.ToDate();


                    if (startingAt > endingAt)
                    {
                        ENUtils.ShowMessage("Beginning Date must be less than Ending Date");
                        return;

                    }

                    foreach (var gRow in grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == ""
                        && (c.Cells[COLS.PickupDate].Value.ToDate() >= startingAt && c.Cells[COLS.PickupDate].Value.ToDate() <= endingAt)))
                    {

                        if (chkPickupTime.Enabled && dtpPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;
                        }

                        if (chkPickupFares.Enabled)
                        {
                            gRow.Cells[COLS.Fare].Value = numPickupFares.Value;
                        }

                        if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value!=null)
                        {
                            gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                        }

                        if (gRow.Cells[COLS.MasterJobId].Value.ToLong() > 0 && chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                        {
                            gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                        }


                        if (chkReturnFares.Enabled)
                        {
                            gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                        }


                        if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                          || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                        {
                            if (chkPickup.Checked)
                            {

                                gRow.Cells[COLS.FromLocTypeId].Value = fromLocTypeId;
                                gRow.Cells[COLS.FromLocId].Value = fromLocId;
                                gRow.Cells[COLS.FromDoorNo].Value = fromDoorNo;
                                gRow.Cells[COLS.FromStreet].Value = fromStreet;


                                if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                    gRow.Cells[COLS.FromAddress].Value = fromAddress;
                                else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                {

                                    gRow.Cells[COLS.FromAddress].Value = fromPostCode;
                                }
                                else
                                {

                                    gRow.Cells[COLS.FromAddress].Value = ddlFromLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }


                            if (chkDestination.Checked)
                            {
                                gRow.Cells[COLS.ToLocTypeId].Value = toLocTypeId;
                                gRow.Cells[COLS.ToLocId].Value = toLocId;
                                gRow.Cells[COLS.ToDoorNo].Value = toDoorNo;
                                gRow.Cells[COLS.ToStreet].Value = toStreet;
                                //   gRow.Cells[COLS.ToAddress].Value = toAddress;


                                if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                    gRow.Cells[COLS.ToAddress].Value = toAddress;
                                else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                {

                                    gRow.Cells[COLS.ToAddress].Value = toPostCode;
                                }
                                else
                                {

                                    gRow.Cells[COLS.ToAddress].Value = ddlToLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }
                        }

                        gRow.Cells[COLS.CHANGED].Value = "1";
                    }

                }




            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }



        }

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpStartingAt.Enabled = false;
               
            }
            else
            {

                dtpStartingAt.Enabled = true;

            }
        }

        private void chkEndingAt_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpEndingAt.Enabled = false;
               
            }
            else
            {

                dtpEndingAt.Enabled = true;

            }
        }

        private void chkPickupTime_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            dtpPickupTime.Enabled = args.ToggleState == ToggleState.On;
        }

        private void chkReturnPickupTime_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            dtpReturnPickupTime.Enabled = args.ToggleState == ToggleState.On;
        }

        private void chkPickupFares_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            numPickupFares.SpinElement.TextBoxItem.Enabled = args.ToggleState == ToggleState.On;
            numPickupFares.SpinElement.Enabled = args.ToggleState == ToggleState.On;
            numPickupFares.Enabled = args.ToggleState == ToggleState.On;

        }

        private void chkReturnFares_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            numReturnFares.SpinElement.TextBoxItem.Enabled = args.ToggleState == ToggleState.On;
            numReturnFares.SpinElement.Enabled = args.ToggleState == ToggleState.On;
            numReturnFares.Enabled = args.ToggleState == ToggleState.On;

        }

        private void btnRevertChanges_Click(object sender, EventArgs e)
        {
            DisplayBookings();
        }



        #region Pickup and Destination Settings
        string[] res = null;
        string searchTxt = "";
     
        bool IsKeyword = false;
        AutoCompleteTextBox aTxt;
        WebClient wc = null;

        void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (aTxt == null || IsKeyword)
                {
                    timer1.Stop();
                    return;
                }

                timer1.Stop();

                searchTxt = searchTxt.ToUpper();


                //if (EnablePOI && searchTxt.StartsWith("P="))
                //{
                //    if (searchTxt.Trim().Length > 2)
                //    {
                //        IsPOI = true;
                //        searchTxt = searchTxt.Replace("P=", "").Trim();
                //    }

                //}


                string postCode = General.GetPostCodeMatch(searchTxt);
                if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                    postCode = string.Empty;

                string street = searchTxt;


                int IsAsc = 0;
                if (!string.IsNullOrEmpty(postCode))
                {
                    street = street.Replace(postCode, "").Trim();

                    //   street = street.Remove(street.IndexOf(postCode)).Trim();


                    if (postCode.Contains(' ') == false)
                    {
                        if (postCode.Length == 3 && Char.IsNumber(postCode[2]))
                        {

                            IsAsc = 1;
                        }
                        else if (postCode.Length == 2 && Char.IsNumber(postCode[1]))
                        {

                            IsAsc = 1;
                        }
                        else if (postCode.Length > 3 && Char.IsNumber(postCode[3]))
                        {

                            IsAsc = 2;
                        }


                    }

                }




                if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(postCode) && street.IsAlpha() == false && street.Length < 4 && searchTxt.IndexOf(postCode) < searchTxt.IndexOf(street))
                {
                    street = "";
                    postCode = searchTxt;
                }


                //if (EnablePOI && IsPOI)
                //{

                //    res = (from a in AppVars.listofPOI

                //           where (a.FullAddress.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))
                //           select a.FullAddress
                //                       ).Take(1000).ToArray<string>();
                //}
                //else
                //{


                    if (IsAsc == 1)
                    {



                        if (!string.IsNullOrEmpty(street))
                        {


                            res = (from a in AppVars.listOfAddress

                                   where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                                   orderby a.PostalCode

                                   select a.AddressLine1

                                           ).Take(1000).ToArray<string>();

                        }
                        else
                        {

                            res = (from a in AppVars.listOfAddress

                                   where a.PostalCode.StartsWith(postCode)

                                   orderby a.PostalCode

                                   select a.AddressLine1

                                 ).Take(600).ToArray<string>();
                        }

                    }
                    else if (IsAsc == 2)
                    {


                        res = (from a in AppVars.listOfAddress

                               where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                               orderby a.PostalCode descending

                               select a.AddressLine1

                                       ).Take(500).ToArray<string>();




                        if (street.Contains(' ') && res.Count() == 0)
                        {

                            string[] vals = street.Split(' ');
                            int valCnt = vals.Count();

                            res = (from a in AppVars.listOfAddress

                                   where (vals.Count(c => a.AddressLine1.Contains(c)) == valCnt)

                                   select a.AddressLine1

                                 ).Take(30).ToArray<string>();


                        }


                    }
                    else
                    {

                        if (postCode.Contains(' '))
                        {

                            res = (from a in AppVars.listOfAddress

                                   where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))



                                   select a.AddressLine1

                                      ).Take(500).ToArray<string>();


                        }
                        else
                        {


                            if (street.Length == 3 && street.IsAlpha() && !string.IsNullOrEmpty(AppVars.objPolicyConfiguration.CountyString))
                            {


                                string[] areas = AppVars.objPolicyConfiguration.CountyString.Split(',');

                                string last = street[2].ToStr();
                                street = street.Remove(2);

                                res = (from b in AppVars.listOfAddress.Where(a => areas.Any(c => a.AddressLine1.Contains(c)) && a.AddressLine1.Split(' ').Count() > 5)
                                       //  let x = (areas.Any(c => b.Address.Contains(c)) ? b.Address.Split(' ') : null)
                                       let x = b.AddressLine1.Split(' ')
                                       where

                                          (

                                       (x.ElementAt(0).StartsWith(street) && x.ElementAt(1).StartsWith(last))
                                    || (x.ElementAt(0).StartsWith(street) && areas.Contains(x.ElementAt(2)) == false && x.ElementAt(2).StartsWith(last))
                                        )

                                       select b.AddressLine1

                                          ).Take(200).ToArray<string>();



                            }
                            else
                            {


                                if (street.WordCount() == 1 && street.ContainsNoSpaces())
                                {
                                    //  street = street + " ";

                                    res = (from a in AppVars.listOfAddress

                                           where (a.AddressLine1.StartsWith(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))
                                           select a.AddressLine1

                                         ).Take(500).ToArray<string>();
                                }
                                else
                                {



                                    res = (from a in AppVars.listOfAddress

                                           where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))



                                           select a.AddressLine1

                                        ).Take(1000).ToArray<string>();

                                }






                            }



                            if (street.Contains(' ') && res.Count() == 0)
                            {



                                string[] vals = street.Split(' ');
                                int valCnt = vals.Count();


                                res = (from a in AppVars.listOfAddress

                                       where (vals.Count(c => a.AddressLine1.Contains(c)) == valCnt)



                                       select a.AddressLine1

                                     ).Take(30).ToArray<string>();


                            }



                        }



                    }


                    //if (!chkAutoDespatch.Enabled && res.Count() == 0)
                    //{

                    //    CancelWebClientAsync();
                    //    //  wc.CancelAsync();
                    //    wc = new WebClient();
                    //    wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                    //    wc.DownloadStringAsync(new Uri("http://maps.googleapis.com/maps/api/geocode/xml?address=" + searchTxt + ", UK&sensor=false"));


                    //    return;

                    //}
              //  }

                ShowAddresses();

            }
            catch (Exception ex)
            {


            }

        }

        private void ShowAddresses()
        {
            int sno = 1;




            //var finalList = (from a in AppVars.zonesList
            //                 from b in res
            //                 where b.Contains(a) && (b.Substring(b.IndexOf(a)).Split(' ')[0] == a && b[b.IndexOf(a) - 1] == ' ')
            //                 select b).ToArray<string>();

            var finalList = (from a in AppVars.zonesList
                             from b in res
                             where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && b[b.IndexOf(a) - 1] == ' ')
                             select b).ToArray<string>();

            if (finalList.Count() > 0)
            {
                finalList = finalList.Union(res).ToArray<string>();

                //  finalList = finalList.OrderBy(c=>AppVars.zonesList. c== AppVars.zonesList) c =.Union(res).ToArray<string>();

            }
            else
                finalList = res;


            if (finalList.Count() < 10)
            {

                finalList = finalList.Select(args => (sno++) + ". " + args).ToArray();

            }



            aTxt.ListBoxElement.Items.Clear();
            aTxt.ListBoxElement.Items.AddRange(finalList);


            if (aTxt.ListBoxElement.Items.Count == 0)
                aTxt.ResetListBox();
            else
            {

                //  aTxt.ListBoxElement.Visible = true;

                aTxt.ShowListBox();


            }

            if (searchTxt != aTxt.FormerValue.ToLower())
            {
                aTxt.FormerValue = aTxt.Text;

            }
        }

        private void ddlFromLocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillFromLocations();

        }

        private void ddlToLocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillToLocations();
          //  SetBookingTypeDetails(ddlBookingType.SelectedValue.ToInt());
        }


      

        void TextBoxElement_TextChanged(object sender, EventArgs e)
        {


            try
            {

              
                IsKeyword = false;
                timer1.Stop();

                aTxt = (AutoCompleteTextBox)sender;
                aTxt.ResetListBox();

                if (aTxt.Name == "txtFromAddress")
                    txtToAddress.SendToBack();

                else if (aTxt.Name == "txtToAddress")
                    txtToAddress.BringToFront();


                string text = aTxt.Text;

                string doorNo = string.Empty;

                if (text.Length > 2)
                {

                    if (aTxt.SelectedItem == null || (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() != aTxt.Text.ToLower()))
                    {


                        for (int i = 0; i <= 2; i++)
                        {
                            if (char.IsNumber(text[i]))
                                doorNo += text[i];
                            else
                                break;

                        }
                        text = text.Remove(text.IndexOf(doorNo), doorNo.Length);
                    }
                }


                if (text.Length > 2 && text != "BASX")
                {
                    if (text.EndsWith("   "))
                    {
                        if (aTxt.Name == "txtFromAddress")
                        {
                            FocusOnPickupPlot();
                        }
                        else if (aTxt.Name == "txtToAddress")
                        {
                            FocusOnDropOffPlot();
                        }

                        return;

                    }

                    else if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() == aTxt.Text.ToLower())
                    {
                        aTxt.ListBoxElement.Items.Clear();

                        aTxt.ResetListBox();

                        string locName = aTxt.SelectedItem.ToLower();
                        int commaIndex = aTxt.SelectedItem.LastIndexOf(',');
                        if (commaIndex != -1)
                        {
                            locName = locName.Remove(commaIndex);
                        }


                        string formerValue = aTxt.FormerValue.ToLower().Trim();

                        int? loctypeId = 0;
                        if (AppVars.keyLocations.Contains(formerValue) || aTxt.FormerValue.EndsWith("  ")
                        || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 3 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                        {
                            //if (AppVars.keyLocations.Contains(formerValue) || aTxt.FormerValue.EndsWith("  ")
                            // ||   (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <=2 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                            //{
                            Gen_Location loc = null;


                            if (aTxt.FormerValue.EndsWith("  ") || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 2 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                            {
                                loc = General.GetObject<Gen_Location>(c => c.LocationName.ToLower() == locName);
                            }
                            else
                                loc = General.GetObject<Gen_Location>(c => c.ShortCutKey == formerValue && c.LocationName.ToLower() == locName);

                            if (loc != null)
                            {
                                loctypeId = loc.LocationTypeId;
                            }
                        }

                        if (loctypeId != 0)
                        {

                            if (aTxt.Name == "txtFromAddress")
                            {

                                ddlFromLocType.SelectedValue = loctypeId;
                                RadComboBoxItem item = (RadComboBoxItem)ddlFromLocation.Items.FirstOrDefault(b => b.Text.Equals(aTxt.SelectedItem));
                                if (item != null)
                                {
                                    ddlFromLocation.SelectedValue = item.Value;
                                    if (commaIndex > 0 && ddlFromLocation.Text != item.Text)
                                    {

                                        SetPickupZone(item.Text);

                                    }

                                }

                                if (loctypeId != Enums.LOCATION_TYPES.POSTCODE || loctypeId != Enums.LOCATION_TYPES.ADDRESS
                                    || loctypeId != Enums.LOCATION_TYPES.AIRPORT || loctypeId != Enums.LOCATION_TYPES.BASE)
                                {

                                    txtToAddress.Focus();

                                }
                            }
                            else if (aTxt.Name == "txtToAddress")
                            {

                                ddlToLocType.SelectedValue = loctypeId;
                                RadComboBoxItem item = (RadComboBoxItem)ddlToLocation.Items.FirstOrDefault(b => b.Text.Equals(aTxt.SelectedItem));
                                if (item != null)
                                {
                                    ddlToLocation.SelectedValue = item.Value;

                                    if (commaIndex > 0)
                                    {
                                        SetDropOffZone(item.Text);

                                    }

                                }

                                if (loctypeId == Enums.LOCATION_TYPES.POSTCODE || loctypeId == Enums.LOCATION_TYPES.ADDRESS)
                                {
                                    txtToFlightDoorNo.Focus();
                                }
                                else
                                {
                                  //  ddlCustomerName.Focus();
                                }
                            }
                        }
                        else if (aTxt.Text.Contains('.'))
                        {

                            RemoveNumbering(doorNo);

                            if (aTxt.Name == "txtFromAddress")
                            {

                                SetPickupZone(aTxt.SelectedItem);
                                txtFromFlightDoorNo.Focus();

                            }

                            else if (aTxt.Name == "txtToAddress")
                            {
                                SetDropOffZone(aTxt.SelectedItem);
                                txtToFlightDoorNo.Focus();

                            }
                        }
                        else if (!string.IsNullOrEmpty(doorNo))
                        {
                            aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                            aTxt.Text = doorNo + " " + aTxt.Text;
                            aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                        }
                        else
                        {
                            if (aTxt.Name == "txtFromAddress")
                            {

                                SetPickupZone(aTxt.SelectedItem);
                                //  txtFromFlightDoorNo.Focus();

                            }

                            else if (aTxt.Name == "txtToAddress")
                            {
                                SetDropOffZone(aTxt.SelectedItem);
                                //  txtToFlightDoorNo.Focus();

                            }


                        }

                        aTxt.FormerValue = string.Empty;


                        return;
                    }



                    if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                    {

                        CancelWebClientAsync();
                        // wc.CancelAsync();
                        aTxt.Values = null;

                    }
                    text = text.ToLower();

                    if (AppVars.keyLocations.Contains(text) || (text.Length <= 4 && (text.EndsWith("  ") || (text[1] == ' ' || (char.IsLetter(text[1]) && text[2] == ' ' && text.Trim().WordCount() == 2))))
                       || (text.Length < 13 && text.WordCount() == 2 && text.Remove(text.IndexOf(' ')).Trim().Length <= 3 && text.Strip(' ').IsAlpha()))
                    {

                        //if (AppVars.keyLocations.Contains(text) || (text.Length<=4 && (text.EndsWith("  ") || (text[1]==' ' || (char.IsLetter(text[1]) && text[2]==' ' && text.Trim().WordCount()==2))))
                        //    || (text.Length<13 && text.WordCount()==2 && text.Remove(text.IndexOf(' ')).Trim().Length<=2 && text.Strip(' ').IsAlpha()))
                        //{





                        aTxt.ListBoxElement.Items.Clear();


                        string[] res = null;

                        if (text.EndsWith("  "))
                        {

                            text = text.Trim();

                            res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey.StartsWith(text))
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                    ).ToArray<string>();


                        }
                        else
                        {
                            if (text.Contains(' ') && text.Substring(text.IndexOf(' ')).Trim().Length > 1)
                            {
                                string shortcut = text.Remove(text.IndexOf(' ')).Trim();

                                string locName = text.Substring(text.IndexOf(' ')).Trim().ToLower();

                                res = (from a in General.GetQueryable<Gen_Location>(c => c.Gen_LocationType.ShortCutKey == shortcut &&
                                            c.LocationName.ToLower().Contains(locName))
                                       select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                          ).ToArray<string>();

                            }
                            else
                            {


                                res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey == text)
                                       select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                           ).ToArray<string>();
                            }
                        }


                        if (res.Count() > 0)
                        {
                            IsKeyword = true;


                            var finalList = (from a in AppVars.zonesList
                                             from b in res
                                             where b.Contains(a)
                                             select b).ToArray<string>();


                            if (finalList.Count() > 0)
                                finalList = finalList.Union(res).ToArray<string>();

                            else
                                finalList = res;


                            aTxt.ListBoxElement.Items.AddRange(finalList);


                            aTxt.ShowListBox();
                        }


                        if (aTxt.Text != aTxt.FormerValue)
                        {
                            aTxt.FormerValue = aTxt.Text;
                        }
                    }


                    if (MapType == Enums.MAP_TYPE.NONE) return;

                    StartAddressTimer(text);

                }
                else
                {
                    if (MapType == Enums.MAP_TYPE.NONE) return;
                    aTxt.ResetListBox();
                    //  aTxt.ListBoxElement.Visible = false;
                    aTxt.ListBoxElement.Items.Clear();

                    CancelWebClientAsync();
                    //  wc.CancelAsync();
                    aTxt.Values = null;

                }


            }
            catch (Exception ex)
            {

            }
        }


        private void FocusOnPickupPlot()
        {
         //   ddlPickupPlot.Focus();
        }

        private void FocusOnDropOffPlot()
        {
          //  ddlDropOffPlot.Focus();
        }

        private void FocusOnFromAddress()
        {
            txtFromAddress.Focus();

        }

        private void FocusOnFromPostCode()
        {
            txtFromPostCode.Focus();
        }

        private void FocusOnFromLocation()
        {
            ddlFromLocation.Focus();
        }


        private void CancelWebClientAsync()
        {
            if (wc != null)
            {
                wc.CancelAsync();

            }

        }


        private void SetPickupZone(string val)
        {

            //ddlPickupPlot.SelectedValue = GetZoneId(val.ToStr().ToUpper()).ToInt();

        }

        private void SetDropOffZone(string val)
        {
            //ddlDropOffPlot.SelectedValue = GetZoneId(val.ToStr().ToUpper()).ToInt();
        }

        private void RemoveNumbering(string formerVal)
        {

            aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            aTxt.Text = formerVal.ToStr() + " " + aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
            aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

        }

        private void StartAddressTimer(string text)
        {

            text = text.ToLower();
            searchTxt = text;

            timer1.Start();

        }

        #endregion

        private void chkPickup_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetFromAddress(args.ToggleState);
        }

        private void SetFromAddress(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                ddlFromLocType.Enabled = true;
                ddlFromLocation.Enabled = true;
                txtFromAddress.Enabled = true;
                txtFromFlightDoorNo.Enabled = true;
                txtFromStreetComing.Enabled = true;

            }
            else
            {
                ddlFromLocType.Enabled = false;
                ddlFromLocation.Enabled = false;
                txtFromAddress.Enabled = false;
                txtFromFlightDoorNo.Enabled = false;
                txtFromStreetComing.Enabled = false;

            }

        }

        private void chkDestination_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetToAddress(args.ToggleState);
        }


        private void SetToAddress(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                ddlToLocType.Enabled = true;
                ddlToLocation.Enabled = true;
                txtToAddress.Enabled = true;
                txtToFlightDoorNo.Enabled = true;
                txtToStreetComing.Enabled = true;

            }
            else
            {
                ddlToLocType.Enabled = false;
                ddlToLocation.Enabled = false;
                txtToAddress.Enabled = false;
                txtToFlightDoorNo.Enabled = false;
                txtToStreetComing.Enabled = false;

            }

        }

        private void btnSelectVia_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            CreateViaPanel();

            if (args.ToggleState == ToggleState.On)
            {


                //btnSelectVia.Text = "Hide Via Point";
                btnSelectVia.Text = "Hide Via Point(" + grdVia.Rows.Count + ")";
                //radToggleButton1.Text = "Hide Via Point(" + grdVia.Rows.Count + ")";
                pnlVia.Visible = true;
                //pnlBottom.Location = this.PnlNewBottomLocation;
                txtViaAddress.Select();
            }
            else
            {
                //btnSelectVia.Text = "Show Via Point";
                btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                // radToggleButton1.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                pnlVia.Visible = false;
                radPanel5.RootElement.Opacity = 1;
               // pnlMain.RootElement.Opacity = 1;
                //pnlBottom.Location = this.PnlOldBottomLocation;

                //ddlCustomerName.Select();

            }
        }
        //Via Point Code

        private void CreateViaPanel()
        {

            if (pnlVia != null)
                return;


            this.pnlVia = new Telerik.WinControls.UI.RadPanel();

            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtViaAddress = new UI.AutoCompleteTextBox();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.btnAddVia = new Telerik.WinControls.UI.RadButton();
            this.ddlViaLocation = new UI.MyDropDownList();
            this.grdVia = new Telerik.WinControls.UI.RadGridView();
            this.txtviaPostCode = new UI.AutoCompleteTextBox();
            this.lblViaLoc = new Telerik.WinControls.UI.RadLabel();
            this.lblFromViaPoint = new Telerik.WinControls.UI.RadLabel();
            this.ddlViaFromLocType = new UI.MyDropDownList();

            ((System.ComponentModel.ISupportInitialize)(this.pnlVia)).BeginInit();
            this.pnlVia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtViaAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddVia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlViaLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVia.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtviaPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblViaLoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromViaPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlViaFromLocType)).BeginInit();

            //this.pnlMain.Controls.Add(this.pnlVia);
            this.radPanel5.Controls.Add(this.pnlVia);


            this.pnlVia.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlVia.Controls.Add(this.label7);
            this.pnlVia.Controls.Add(this.label3);
            this.pnlVia.Controls.Add(this.txtViaAddress);
            this.pnlVia.Controls.Add(this.btnClear);
            this.pnlVia.Controls.Add(this.btnAddVia);
            this.pnlVia.Controls.Add(this.ddlViaLocation);
            this.pnlVia.Controls.Add(this.grdVia);
            this.pnlVia.Controls.Add(this.txtviaPostCode);
            this.pnlVia.Controls.Add(this.lblViaLoc);
            this.pnlVia.Controls.Add(this.lblFromViaPoint);
            this.pnlVia.Controls.Add(this.ddlViaFromLocType);
            this.pnlVia.Location = new System.Drawing.Point(9, 230);
            this.pnlVia.Name = "pnlVia";
            // 
            // 
            // 
            this.pnlVia.RootElement.Opacity = 1;
            this.pnlVia.Size = new System.Drawing.Size(910, 220);
            this.pnlVia.TabIndex = 1;
            this.pnlVia.Visible = false;
            txtViaAddress.ListBoxElement.Font = new Font("Tahoma", 11, FontStyle.Bold);
            ((Telerik.WinControls.UI.RadPanelElement)(this.pnlVia.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.pnlVia.GetChildAt(0).GetChildAt(1))).Width = 2F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.pnlVia.GetChildAt(0).GetChildAt(1))).BottomWidth = 1F;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Navy;
            this.label7.Image = global::Taxi_AppMain.Properties.Resources.delete;
            this.label7.Location = new System.Drawing.Point(864, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 24);
            this.label7.TabIndex = 8;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Navy;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(910, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Via Locations";
            //this.btnAddVia.Click += new System.EventHandler(this.btnAddVia_Click);
            this.label3.Click += new EventHandler(label3_Click);
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtViaAddress
            // pn
            this.txtViaAddress.BackColor = System.Drawing.Color.White;
            this.txtViaAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtViaAddress.DefaultHeight = 60;
            this.txtViaAddress.DefaultWidth = 370;
            this.txtViaAddress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViaAddress.ForceListBoxToUpdate = false;
            this.txtViaAddress.FormerValue = "";
            this.txtViaAddress.Location = new System.Drawing.Point(491, 38);
            this.txtViaAddress.Multiline = true;
            this.txtViaAddress.Name = "txtViaAddress";
            // 
            // 
            // 
            this.txtViaAddress.RootElement.StretchVertically = true;
            this.txtViaAddress.SelectedItem = null;
            this.txtViaAddress.Size = new System.Drawing.Size(257, 53);
            this.txtViaAddress.TabIndex = 2;
            this.txtViaAddress.TabStop = false;
            this.txtViaAddress.Values = null;
            this.txtViaAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtViaAddress_KeyDown);
            this.txtViaAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromAddress_KeyPress);
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtViaAddress.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtViaAddress.GetChildAt(0).GetChildAt(2))).Width = 1F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtViaAddress.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtViaAddress.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtViaAddress.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtViaAddress.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtViaAddress.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(771, 65);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(82, 24);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnAddVia
            // 
            this.btnAddVia.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAddVia.Location = new System.Drawing.Point(771, 33);
            this.btnAddVia.Name = "btnAddVia";
            this.btnAddVia.Size = new System.Drawing.Size(82, 24);
            this.btnAddVia.TabIndex = 3;
            this.btnAddVia.Text = "Add";
            this.btnAddVia.Click += new System.EventHandler(this.btnAddVia_Click);
            // 
            // ddlViaLocation
            // 
            this.ddlViaLocation.Caption = null;
            this.ddlViaLocation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlViaLocation.Location = new System.Drawing.Point(491, 38);
            this.ddlViaLocation.Name = "ddlViaLocation";
            this.ddlViaLocation.Property = null;
            this.ddlViaLocation.ShowDownArrow = true;
            this.ddlViaLocation.Size = new System.Drawing.Size(250, 27);
            this.ddlViaLocation.TabIndex = 0;
            // 
            // grdVia
            // 
            this.grdVia.Location = new System.Drawing.Point(7, 93);
            this.grdVia.Name = "grdVia";
            this.grdVia.Size = new System.Drawing.Size(881, 120);
            this.grdVia.TabIndex = 5;
            this.grdVia.Text = "radGridView1";
            this.grdVia.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdVia_CellDoubleClick);
            // 
            // txtviaPostCode
            // 
            this.txtviaPostCode.BackColor = System.Drawing.Color.White;
            this.txtviaPostCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtviaPostCode.DefaultHeight = 90;
            this.txtviaPostCode.DefaultWidth = 185;
            this.txtviaPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtviaPostCode.ForceListBoxToUpdate = false;
            this.txtviaPostCode.FormerValue = "";
            this.txtviaPostCode.Location = new System.Drawing.Point(492, 40);
            this.txtviaPostCode.MaxLength = 100;
            this.txtviaPostCode.Name = "txtviaPostCode";
            this.txtviaPostCode.SelectedItem = null;
            this.txtviaPostCode.Size = new System.Drawing.Size(195, 26);
            this.txtviaPostCode.TabIndex = 0;
            this.txtviaPostCode.TabStop = false;
            this.txtviaPostCode.Values = null;
            this.txtviaPostCode.TextChanged += new System.EventHandler(this.txtviaPostCode_TextChanged);
            // 
            // lblViaLoc
            // 
            this.lblViaLoc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViaLoc.Location = new System.Drawing.Point(349, 41);
            this.lblViaLoc.Name = "lblViaLoc";
            this.lblViaLoc.Size = new System.Drawing.Size(90, 22);
            this.lblViaLoc.TabIndex = 138;
            this.lblViaLoc.Text = "Via Location";
            // 
            // lblFromViaPoint
            // 
            this.lblFromViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromViaPoint.Location = new System.Drawing.Point(4, 42);
            this.lblFromViaPoint.Name = "lblFromViaPoint";
            this.lblFromViaPoint.Size = new System.Drawing.Size(28, 22);
            this.lblFromViaPoint.TabIndex = 137;
            this.lblFromViaPoint.Text = "Via";
            // 
            // ddlViaFromLocType
            // 
            this.ddlViaFromLocType.Caption = null;
            this.ddlViaFromLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlViaFromLocType.Location = new System.Drawing.Point(112, 40);
            this.ddlViaFromLocType.Name = "ddlViaFromLocType";
            this.ddlViaFromLocType.Property = null;
            this.ddlViaFromLocType.ShowDownArrow = true;
            this.ddlViaFromLocType.Size = new System.Drawing.Size(168, 23);
            this.ddlViaFromLocType.TabIndex = 1;
            this.ddlViaFromLocType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlViaFromLocType_SelectedIndexChanged);


            txtViaAddress.ListBoxElement.Width = 425;
            txtViaAddress.ListBoxElement.Height = 130;
            this.txtViaAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            txtViaAddress.DefaultHeight = 110;

            ((System.ComponentModel.ISupportInitialize)(this.pnlVia)).EndInit();
            this.pnlVia.ResumeLayout(false);
            this.pnlVia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtViaAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddVia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlViaLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVia.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdVia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtviaPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblViaLoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromViaPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlViaFromLocType)).EndInit();


            FormatViaGrid();

            ComboFunctions.FillLocationTypeCombo(ddlViaFromLocType);
            ddlViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;

            pnlVia.BringToFront();
        }

        void label3_Click(object sender, EventArgs e)
        {
            btnSelectVia.ToggleState = ToggleState.Off;
           // radToggleButton1.ToggleState = ToggleState.Off;
            pnlVia.Visible = false;
            //radToggleButton1.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
            btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
        }
        private void FormatViaGrid()
        {


            grdVia.RowsChanged += new GridViewCollectionChangedEventHandler(grdVia_RowsChanged);
            grdVia.AutoSizeRows = true;
            grdVia.TableElement.TableHeaderHeight = 0;
            grdVia.ShowGroupPanel = false;
            grdVia.AllowAddNewRow = false;
            grdVia.AllowEditRow = false;
            grdVia.ShowRowHeaderColumn = false;

            grdVia.TableElement.BorderWidth = 0;
            grdVia.TableElement.BorderColor = Color.Transparent;

            grdVia.EnableHotTracking = false;
            grdVia.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdVia.EnableAlternatingRowColor = true;
            grdVia.TableElement.AlternatingRowColor = Color.AliceBlue;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "ID";
            grdVia.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "MASTERID";
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "FROMVIALOCTYPEID";
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "VIALOCATIONID";
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "FROMTYPELABEL";
            col.HeaderText = "";
            col.Width = 100;
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "FROMTYPEVALUE";
            col.Width = 150;
            col.HeaderText = "";
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "EMPTY";
            col.IsVisible = false;
            col.Width = 100;
            grdVia.Columns.Add(col);


            AddReverceFromColumn(grdVia);
            AddReverceDestinationColumn(grdVia);


            col = new GridViewTextBoxColumn();
            col.Name = "VIALOCATIONLABEL";
            col.HeaderText = "";
            col.Width = 120;
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "VIALOCATIONVALUE";
            col.Width = 280;
            col.HeaderText = "";
            grdVia.Columns.Add(col);

            AddColumn(grdVia, "ColMoveUp", "Delete", Resources.Resource1.lc_moveup);
            AddColumn(grdVia, "ColMoveDown", "Delete", Resources.Resource1.lc_movedown);

            AddColumn(grdVia, "ColDelete", "Delete", null);

            grdVia.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdVia.CellFormatting += new CellFormattingEventHandler(grdVia_CellFormatting);
        }

        private void txtViaAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {

                AddViaPoint();
                FocusOnViAddress();

            }
        }


        #region VIA POINT REVERSE
        private void AddReverceFromColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 70;

            col.Name = "ColRervP";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Reverse P";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

            grid.NewRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.EnterMovesToNextRow;
        }
        private void AddReverceDestinationColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 70;

            col.Name = "ColRervD";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Reverse D";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

            grid.NewRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.EnterMovesToNextRow;
        }
        private void ddlViaFromLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillViaLocations();
        }



        void ReverceToPickUpPoint()
        {
            try
            {
                // for via variables
                string viapointText = grdVia.CurrentRow.Cells["VIALOCATIONVALUE"].Value.ToString();
                int viaLocTypeId = grdVia.CurrentRow.Cells["FROMVIALOCTYPEID"].Value.ToInt();
                string viapointLabel = grdVia.CurrentRow.Cells["FROMTYPEVALUE"].Value.ToString();

                // for Top Variables

                string FromAddress = "";
                int FromLocationId = grdVia.CurrentRow.Cells["VIALOCATIONID"].Value.ToInt();
                if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                {
                    FromAddress = txtFromAddress.Text.ToStr();
                }
                else
                {
                    FromAddress = ddlFromLocation.Text.ToStr();
                    FromLocationId = ddlFromLocation.SelectedValue.ToInt();
                }

                ReverseVia(viapointText, viaLocTypeId, viapointLabel, FromAddress, ddlFromLocType.Text.ToStr(), ddlFromLocType.SelectedValue.ToIntorNull(), true, FromLocationId);

            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }
        void ReverceToDestination()
        {
            try
            {

                // for via variables
                string viapointText = grdVia.CurrentRow.Cells["VIALOCATIONVALUE"].Value.ToString();
                int viaLocTypeId = grdVia.CurrentRow.Cells["FROMVIALOCTYPEID"].Value.ToInt();
                string viapointLabel = grdVia.CurrentRow.Cells["FROMTYPEVALUE"].Value.ToString();

                // for Top Variables

                string ToAddress = "";
                int ToLocationId = grdVia.CurrentRow.Cells["VIALOCATIONID"].Value.ToInt();
                if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                {
                    ToAddress = txtToAddress.Text.ToStr();
                }
                else
                {
                    ToAddress = ddlToLocation.Text.ToStr();
                    ToLocationId = ddlToLocation.SelectedValue.ToInt();
                }

                ReverseVia(viapointText, viaLocTypeId, viapointLabel, ToAddress, ddlToLocType.Text.ToStr(), ddlToLocType.SelectedValue.ToIntorNull(), false, ToLocationId);


            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }
        private void FocusOnFromDoor()
        {

            txtFromFlightDoorNo.Focus();


        }
        private void btnClear_Click(object sender, EventArgs e)
        {

            ClearViaDetails();

        }
        private void grdVia_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdVia.CurrentRow != null && grdVia.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdVia.CurrentRow;

                ddlViaFromLocType.SelectedValue = row.Cells["FROMVIALOCTYPEID"].Value.ToInt();

                string locValue = row.Cells["VIALOCATIONVALUE"].Value.ToStr();

                txtViaAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtViaAddress.Text = locValue;
                txtViaAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtviaPostCode.TextChanged -= new EventHandler(txtviaPostCode_TextChanged);
                txtviaPostCode.Text = locValue;
                txtviaPostCode.TextChanged += new EventHandler(txtviaPostCode_TextChanged);

                ddlViaLocation.SelectedValue = row.Cells["VIALOCATIONID"].Value.ToInt();


            }
        }

        private void txtviaPostCode_TextChanged(object sender, EventArgs e)
        {

            if (MapType == Enums.MAP_TYPE.NONE) return;

            AutoCompleteTextBox viaPostCode = (AutoCompleteTextBox)sender;


            string temp = string.Empty;
            string text = viaPostCode.Text;
            if (text.Length > 2)
            {


                temp = text.ToUpper();



                if (viaPostCode.SelectedItem != null && viaPostCode.SelectedItem == viaPostCode.Text)
                {
                    viaPostCode.Values = null;
                    viaPostCode.ResetListBox();
                    return;


                }


                text = text.ToLower();
                viaPostCode.ListBoxElement.Items.Clear();

                viaPostCode.ListBoxElement.Items.AddRange((from a in AppVars.listOfAddress
                                                           where a.PostalCode.ToLower().StartsWith(text)
                                                           select a.PostalCode
                              ).OrderBy(a => a).Distinct().ToArray<string>());

                if (viaPostCode.ListBoxElement.Items.Count > 0)
                {

                    viaPostCode.ShowListBox();
                }
                else
                    viaPostCode.ResetListBox();

            }
        }
        private void txtFromAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == '1' || e.KeyChar == '2' || e.KeyChar == '3' || e.KeyChar == '4'
                    || e.KeyChar == '5' || e.KeyChar == '6' || e.KeyChar == '7'
                    || e.KeyChar == '8' || e.KeyChar == '9')
                {




                    AutoCompleteTextBox txtData = (AutoCompleteTextBox)sender;
                    if (txtData.Text.StartsWith("W1"))
                        return;



                    if (txtData.ListBoxElement.Visible == true && txtData.ListBoxElement.Items.Count < 10)
                    {
                        string idx = e.KeyChar.ToStr();

                        txtData.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        string item = txtData.ListBoxElement.Items[idx.ToInt() - 1].ToStr();

                        string doorNo = string.Empty;
                        for (int i = 0; i <= 2; i++)
                        {
                            if (char.IsNumber(txtData.FormerValue[i]))
                                doorNo += txtData.FormerValue[i];
                            else
                                break;

                        }


                        txtData.Text = (doorNo + " " + item.Remove(0, item.IndexOf('.') + 1).Trim()).Trim();
                        if (txtData.Name == "txtFromAddress")
                        {
                            SetPickupZone(txtData.Text);
                            FocusOnFromDoor();
                        }
                        else if (txtData.Name == "txtToAddress")
                        {
                            SetDropOffZone(txtData.Text);
                            FocusOnToDoor();
                        }
                        else if (txtData.Name == "txtViaAddress")
                        {
                            txtData.ResetListBox();
                            AddViaPoint();

                        }
                        txtData.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                        e.Handled = true;

                        aTxt.ResetListBox();
                        aTxt.ListBoxElement.Items.Clear();
                        //   txtViaAddress.Focus();
                    }



                }
            }
            catch (Exception ex)
            {


            }
        }
        private void FocusOnToDoor()
        {
            txtToFlightDoorNo.Focus();

        }
        private void label7_Click(object sender, EventArgs e)
        {
            btnSelectVia.ToggleState = ToggleState.Off;
        }



        void ReverseVia(string viaText, int viaLoctypeId, string viaLabel, string FromAddress, string fromLocType, int? fromlocTypeId, bool IsFrom, int locationId)
        {
            try
            {

                string ViaTextTemp = viaText;
                string FromAddressTemp = FromAddress;
                int? fromLocIdTemp = fromlocTypeId;
                int VialocIdTemp = viaLoctypeId;




                if (IsFrom == true)
                {
                    this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);


                    if (VialocIdTemp == Enums.LOCATION_TYPES.ADDRESS || VialocIdTemp == Enums.LOCATION_TYPES.BASE)
                    {
                        txtFromAddress.Text = ViaTextTemp;
                        ddlFromLocType.SelectedValue = VialocIdTemp;
                    }
                    else
                    {
                        ddlFromLocType.SelectedValue = VialocIdTemp;
                        ddlFromLocation.SelectedValue = locationId;
                    }

                    GridViewRowInfo row;

                    if (grdVia.CurrentRow != null && grdVia.CurrentRow is GridViewNewRowInfo)
                        grdVia.CurrentRow = null;


                    if (grdVia.CurrentRow != null)
                    {

                        row = grdVia.CurrentRow;
                    }

                    grdVia.CurrentRow.Cells["VIALOCATIONVALUE"].Value = FromAddressTemp;
                    grdVia.CurrentRow.Cells["FROMVIALOCTYPEID"].Value = fromLocIdTemp;

                    grdVia.CurrentRow.Cells["FROMTYPEVALUE"].Value = fromLocType;


                    if (locationId != 0)
                        grdVia.CurrentRow.Cells["VIALOCATIONID"].Value = locationId;
                    else
                        grdVia.CurrentRow.Cells["VIALOCATIONID"].Value = null;



                    this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                }
                else
                {
                    this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);

                    if (VialocIdTemp == Enums.LOCATION_TYPES.ADDRESS || VialocIdTemp == Enums.LOCATION_TYPES.BASE)
                    {
                        txtToAddress.Text = ViaTextTemp;
                        ddlToLocType.SelectedValue = VialocIdTemp;
                    }
                    else
                    {
                        ddlToLocType.SelectedValue = VialocIdTemp;
                        ddlToLocation.SelectedValue = locationId;
                    }

                    GridViewRowInfo row;

                    if (grdVia.CurrentRow != null && grdVia.CurrentRow is GridViewNewRowInfo)
                        grdVia.CurrentRow = null;


                    if (grdVia.CurrentRow != null)
                    {
                        row = grdVia.CurrentRow;
                    }
                    else
                    {

                    }

                    grdVia.CurrentRow.Cells["VIALOCATIONVALUE"].Value = FromAddressTemp;
                    grdVia.CurrentRow.Cells["FROMVIALOCTYPEID"].Value = fromLocIdTemp;

                    grdVia.CurrentRow.Cells["FROMTYPEVALUE"].Value = fromLocType;


                    if (locationId != 0)
                        grdVia.CurrentRow.Cells["VIALOCATIONID"].Value = locationId;
                    else
                        grdVia.CurrentRow.Cells["VIALOCATIONID"].Value = null;




                    this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }


        #endregion


        private void ddlToLocation_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                SetDropOffZone(ddlToLocation.Text);


            }
            catch (Exception ex)
            {


            }

        }



        #region



        private void ddlMilesDrvs2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.IsHandleCreated == false)
                return;

            ShowMapAndNearestDrivers();
        }



        private void ShowMapAndNearestDrivers()
        {

            //if (btnExit.Focused)
            //    return;

            //object o = "LoadNearestMap";
            //SendAsyncRequest(o);

            //LoadNearestMap();

        }

        //private void LoadNearestMap()
        //{
        //    new Thread(new ThreadStart(LoadNearestJobDrivers)).Start();

        //}


        delegate void UIDelegate();
        //private void LoadNearestJobDrivers()
        //{

        //    try
        //    {

        //        if (this.InvokeRequired)
        //        {

        //            UIDelegate d = new UIDelegate(LoadNearest);
        //            this.BeginInvoke(d);
        //        }
        //        else
        //        {
        //            LoadNearest();

        //        }




        //    }
        //    catch (Exception ex)
        //    {


        //    }

        //}
        private void FocusOnViAddress()
        {
            ddlViaFromLocType.Select();
            SendKeys.Send("{TAB}");
            //txtViaAddress.Select();
        }

        private void AddViaPoint()
        {

            try
            {
                int LocTypeId = ddlViaFromLocType.SelectedValue.ToInt();

                string fromViaLabel = lblFromViaPoint.Text.Trim();
                string fromViaValue = ddlViaFromLocType.Text.Trim();

                int? toViaLocId = ddlViaLocation.SelectedValue.ToIntorNull();
                string ToViaLocLabel = lblViaLoc.Text.Trim();
                string toViaLoc = "";


                string msg = string.Empty;
                string msg2 = string.Empty;

                if (LocTypeId == 0)
                {
                    msg += "Required : Via Point." + Environment.NewLine;

                }
                else
                {

                    if (LocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                    {
                        toViaLoc = txtViaAddress.Text.Trim();
                        msg2 += "Required : Via Address." + Environment.NewLine;
                    }
                    else if (LocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        toViaLoc = txtviaPostCode.Text.Trim();
                        msg2 += "Required : Via PostCode." + Environment.NewLine;

                    }
                    else
                    {

                        toViaLoc = ddlViaLocation.Text.Trim();
                        msg2 += "Required : Via Location." + Environment.NewLine;

                    }

                    if (string.IsNullOrEmpty(toViaLoc))
                    {
                        msg += msg2;
                    }

                }

                if (!string.IsNullOrEmpty(msg))
                {
                    ENUtils.ShowMessage(msg);
                    return;

                }

                GridViewRowInfo row;

                if (grdVia.CurrentRow != null && grdVia.CurrentRow is GridViewNewRowInfo)
                    grdVia.CurrentRow = null;


                if (grdVia.CurrentRow != null)
                    row = grdVia.CurrentRow;
                else
                    row = grdVia.Rows.AddNew();



                row.Cells["FROMVIALOCTYPEID"].Value = LocTypeId;
                //  row.Cells[COLS.FROMTYPELABEL].Value = fromViaLabel;
                row.Cells["FROMTYPELABEL"].Value = "Via";

                row.Cells["FROMTYPEVALUE"].Value = fromViaValue;

                row.Cells["VIALOCATIONID"].Value = toViaLocId;
                row.Cells["VIALOCATIONLABEL"].Value = ToViaLocLabel;
                row.Cells["VIALOCATIONVALUE"].Value = toViaLoc;





                ClearViaDetails();


                CalculateAutoFares();


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void ClearViaDetails()
        {
            pnlVia.Visible = true;
            ddlViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
            grdVia.CurrentRow = null;
            txtViaAddress.Text = string.Empty;
            txtviaPostCode.Text = string.Empty;
            ddlViaLocation.SelectedValue = null;
            txtViaAddress.Select();
            txtFromAddress.Text = string.Empty;
        }

        private void btnAddVia_Click(object sender, EventArgs e)
        {
            AddViaPoint();
            FocusOnViAddress();
        }
        //private void LoadNearest()
        //{
        //    try
        //    {







        //        int? jobStatusId = objMaster.PrimaryKeyValue != null ? objMaster.Current.BookingStatusId : Enums.BOOKINGSTATUS.WAITING;


        //        string fromAddress = General.GetPostCodeMatch(txtFromAddress.Text.Trim().ToUpper());
        //        string toAddress = General.GetPostCodeMatch(txtToAddress.Text.ToStr().Trim().ToUpper());

        //        int fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
        //        int toLocTypeId = ddlToLocType.SelectedValue.ToInt();

        //        if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.BASE)
        //        {
        //            if (txtFromAddress.Text.Trim().Length > 2 && txtFromAddress.Text.Contains(' ') == false && txtFromAddress.SelectedItem != null)
        //            {
        //                fromAddress = General.GetPostCodeMatch(txtFromAddress.SelectedItem.Trim());


        //            }
        //        }
        //        else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
        //        {
        //            fromAddress = txtFromPostCode.Text.Trim();

        //        }
        //        else
        //        {
        //            fromAddress = General.GetPostCodeMatch(ddlFromLocation.Text.ToStr().ToUpper().Trim());

        //        }



        //        if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
        //        {
        //            if (txtToAddress.Text.Trim().Length > 2 && txtToAddress.Text.Contains(' ') == false && txtToAddress.SelectedItem != null)
        //                toAddress = General.GetPostCodeMatch(txtToAddress.SelectedItem.Trim());
        //        }
        //        else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
        //        {
        //            toAddress = txtToPostCode.Text.Trim();

        //        }
        //        else
        //        {
        //            toAddress = General.GetPostCodeMatch(ddlToLocation.Text.ToStr().ToUpper().Trim());

        //        }


        //        if (string.IsNullOrEmpty(fromAddress))
        //            return;


        //        if (!string.IsNullOrEmpty(toAddress))
        //            toAddress += " UK";


        //        string pickupPoint = string.Empty;
        //        string pickupPointImageUrl = string.Empty;

        //        Gen_Coordinate pickupCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == fromAddress);


        //        StringBuilder nearestDrvLocations = new StringBuilder();
        //        double jobLatitude = 0;
        //        double jobLongitude = 0;
        //        double milesAway = 5;


        //        if (ddlMilesDrvs2 == null)
        //        {
        //            this.ddlMilesDrvs2 = new System.Windows.Forms.ComboBox();


        //            this.ddlMilesDrvs2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        //            this.ddlMilesDrvs2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        //            this.ddlMilesDrvs2.FormattingEnabled = true;
        //            this.ddlMilesDrvs2.Items.AddRange(new object[] {
        //            "within 3 miles away",
        //            "within 5 miles away",
        //            "within 10 miles away"});
        //            this.ddlMilesDrvs2.Location = new System.Drawing.Point(929, 349);
        //            this.ddlMilesDrvs2.Name = "ddlMilesDrvs2";
        //            this.ddlMilesDrvs2.Size = new System.Drawing.Size(283, 22);
        //            this.ddlMilesDrvs2.TabIndex = 227;
        //            //this.ddlMilesDrvs2.Visible = false;
        //            this.ddlMilesDrvs2.SelectedIndexChanged += new System.EventHandler(this.ddlMilesDrvs2_SelectedIndexChanged);
        //            ddlMilesDrvs2.Visible = true;

        //            ddlMilesDrvs2.SelectedItem = ddlMilesDrvs2.Items[1];


        //            this.pnlMain.Controls.Add(this.ddlMilesDrvs2);
        //        }

        //        if (ddlMilesDrvs2.SelectedIndex == 0)
        //            milesAway = 3;
        //        else if (ddlMilesDrvs2.SelectedIndex == 2)
        //            milesAway = 10;


        //        if (!string.IsNullOrEmpty(fromAddress) && string.IsNullOrEmpty(toAddress))
        //        {



        //            if (pickupCoord != null)
        //            {

        //                pickupPoint = "['<h4>test</h4>'," + pickupCoord.Latitude + "," + pickupCoord.Longitude + "],";

        //                pickupPointImageUrl = "'http://google.com/mapfiles/kml/paddle/A.png',";
        //                jobLatitude = Convert.ToDouble(pickupCoord.Latitude);
        //                jobLongitude = Convert.ToDouble(pickupCoord.Longitude);

        //            }
        //            else
        //                return;

        //        }


        //        if (webBrowser1 == null)
        //        {
        //            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
        //            this.webBrowser1.Location = new System.Drawing.Point(929, 36);
        //            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 0);
        //            this.webBrowser1.Name = "webBrowser1";
        //            this.webBrowser1.ScrollBarsEnabled = false;
        //            this.webBrowser1.Size = new System.Drawing.Size(286, 310);
        //            this.webBrowser1.TabIndex = 225;
        //            this.webBrowser1.Visible = false;
        //            this.pnlMain.Controls.Add(this.webBrowser1);

        //        }





        //        if (grdDrivers == null)
        //        {

        //            this.grdDrivers = new System.Windows.Forms.DataGridView();
        //            // ((System.ComponentModel.ISupportInitialize)(this.grdDrivers)).BeginInit();



        //            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        //            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        //            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        //            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();



        //            this.DriverId = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //            this.details = new System.Windows.Forms.DataGridViewTextBoxColumn();
        //            this.btnDespatchJob = new System.Windows.Forms.DataGridViewButtonColumn();

        //            this.DriverId.HeaderText = "DriverId";
        //            this.DriverId.Name = "DriverId";
        //            this.DriverId.ReadOnly = true;
        //            this.DriverId.Visible = false;
        //            // 
        //            // details
        //            // 
        //            this.details.HeaderText = "details";
        //            this.details.Name = "details";
        //            this.details.ReadOnly = true;
        //            this.details.Width = 200;
        //            // 
        //            // btnDespatchJob
        //            // 
        //            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        //            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
        //            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
        //            this.btnDespatchJob.DefaultCellStyle = dataGridViewCellStyle2;
        //            this.btnDespatchJob.HeaderText = "btnDespatchJob";
        //            this.btnDespatchJob.Name = "btnDespatchJob";
        //            this.btnDespatchJob.ReadOnly = true;
        //            this.btnDespatchJob.Text = "Despatch";
        //            this.btnDespatchJob.UseColumnTextForButtonValue = true;
        //            this.btnDespatchJob.Width = 80;


        //            this.grdDrivers.AllowUserToAddRows = false;
        //            this.grdDrivers.AllowUserToDeleteRows = false;
        //            this.grdDrivers.BackgroundColor = System.Drawing.Color.FloralWhite;
        //            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        //            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SteelBlue;
        //            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        //            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
        //            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        //            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        //            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        //            this.grdDrivers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        //            this.grdDrivers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        //            this.grdDrivers.ColumnHeadersVisible = false;
        //            this.grdDrivers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
        //            this.DriverId,
        //            this.details,
        //            this.btnDespatchJob});
        //            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        //            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
        //            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        //            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
        //            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FloralWhite;
        //            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
        //            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        //            this.grdDrivers.DefaultCellStyle = dataGridViewCellStyle3;
        //            this.grdDrivers.Location = new System.Drawing.Point(929, 36);
        //            this.grdDrivers.Name = "grdDrivers";
        //            this.grdDrivers.ReadOnly = true;
        //            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        //            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
        //            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F);
        //            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
        //            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
        //            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        //            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        //            this.grdDrivers.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
        //            this.grdDrivers.RowHeadersVisible = false;
        //            this.grdDrivers.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FloralWhite;
        //            this.grdDrivers.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
        //            this.grdDrivers.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FloralWhite;
        //            this.grdDrivers.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
        //            this.grdDrivers.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        //            this.grdDrivers.RowTemplate.Height = 50;
        //            this.grdDrivers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        //            this.grdDrivers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        //            this.grdDrivers.Size = new System.Drawing.Size(286, 650);
        //            this.grdDrivers.TabIndex = 226;
        //           // this.grdDrivers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDrivers_CellClick);



        //            //  ((System.ComponentModel.ISupportInitialize)(this.grdDrivers)).EndInit();

        //            this.pnlMain.Controls.Add(this.grdDrivers);
        //        }

        //        if (webBrowser1.Visible == false)
        //        {
        //            webBrowser1.Visible = true;
        //            grdDrivers.Size = new Size(grdDrivers.Size.Width, 300);
        //            grdDrivers.Location = new Point(grdDrivers.Location.X, 372);

        //            grdDrivers.Font = new Font("Tahoma", 11, FontStyle.Bold);
        //            ddlMilesDrvs2.Visible = true;
        //        }

        //        if (pickupCoord != null)
        //        {
        //            jobLatitude = Convert.ToDouble(pickupCoord.Latitude);
        //            jobLongitude = Convert.ToDouble(pickupCoord.Longitude);
        //        }


        //        if (jobStatusId == Enums.BOOKINGSTATUS.WAITING)
        //        {

        //            var ListofAvailDrvs = (from a in AppVars.BLData.GetAll<Fleet_DriverQueueList>(c => c.Status == true && c.Fleet_Driver.HasPDA == true &&
        //                            (c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)).AsEnumerable()
        //                                   join b in AppVars.BLData.GetAll<Fleet_Driver_Location>(c => c.Latitude != 0).AsEnumerable()
        //                                   on a.DriverId equals b.DriverId
        //                                   select new
        //                                   {
        //                                       DriverId = a.DriverId,
        //                                       DriverNo = a.Fleet_Driver.DriverNo,
        //                                       DriverLocation = b.LocationName,
        //                                       Latitude = b.Latitude,
        //                                       Longitude = b.Longitude
        //                                   }).ToList();

        //            var nearestDrivers = ListofAvailDrvs.Select(args => new
        //            {
        //                args.DriverId,
        //                // MilesAwayFromPickup = GetNearestDistance(args.DriverLocation,fromAddress) ,
        //                MilesAwayFromPickup = new DotNetCoords.LatLng(args.Latitude, args.Longitude).DistanceMiles(new DotNetCoords.LatLng(jobLatitude, jobLongitude)),
        //                args.DriverNo,
        //                Latitude = args.Latitude,
        //                Longitude = args.Longitude,
        //                Location = args.DriverLocation

        //            }).OrderBy(args => args.MilesAwayFromPickup).Where(c => c.MilesAwayFromPickup <= milesAway).Take(3).ToList();



        //            grdDrivers.Rows.Clear();
        //            for (int i = 0; i < nearestDrivers.Count; i++)
        //            {
        //                nearestDrvLocations.Append("['<h4>" + nearestDrivers[i].Location + "</h4>'," + nearestDrivers[i].Latitude + "," + nearestDrivers[i].Longitude + "],");


        //                grdDrivers.Rows.Add(nearestDrivers[i].DriverId, nearestDrivers[i].DriverNo + " is " + Math.Round(nearestDrivers[i].MilesAwayFromPickup, 1) + " miles away");

        //            }


        //            if (nearestDrvLocations.Length > 0)
        //                nearestDrvLocations[nearestDrvLocations.Length - 1] = ' ';
        //            else
        //            {
        //                if (pickupPoint.Length > 0)
        //                    pickupPoint = pickupPoint.Remove(pickupPoint.LastIndexOf(','));


        //            }
        //        }
        //        else
        //        {



        //            if (pickupPoint.Length > 0)
        //                pickupPoint = pickupPoint.Remove(pickupPoint.LastIndexOf(','));


        //        }

        //        string text = "<!DOCTYPE html>" +
        //                        "<html>" +
        //                        "<head>" +
        //                          "<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" />" +

        //                          "<script src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>" +
        //                          "<script src=\"http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.1.min.js\"></script>" +
        //                        "</head>" +
        //                        "<body>" +
        //                          "<div id=\"map\" style=\"width: 300px; height: 310px;Left:-10px;Top:-13px\"></div>" +

        //                          "<script type=\"text/javascript\">" +



        //                            "var locations = [" +
        //                               pickupPoint +
        //                              nearestDrvLocations.ToString() +

        //                            "];" +


        //                           " var iconURLPrefix = 'http://maps.google.com/mapfiles/ms/icons/';" +




        //                           " var icons = [" +
        //                           pickupPointImageUrl +


        //                             " iconURLPrefix + 'green-dot.png'," +
        //                             " iconURLPrefix + 'green-dot.png'," +
        //                             " iconURLPrefix + 'green-dot.png'," +
        //                             " iconURLPrefix + 'green-dot.png'," +

        //                           " ];" +
        //                          "  var icons_length = icons.length;" +


        //                          "  var shadow = {" +
        //                           "   anchor: new google.maps.Point(15,33)," +
        //                          "    url: iconURLPrefix + 'msmarker.shadow.png'" +
        //                          "  };" +

        //                           " var map = new google.maps.Map(document.getElementById('map'), {" +
        //                            "  zoom: 14," +
        //                           "   center: new google.maps.LatLng(" + jobLatitude + "," + jobLongitude + ")," +
        //                           "   mapTypeId: google.maps.MapTypeId.ROADMAP," +
        //                           "   mapTypeControl: false," +
        //                           "   streetViewControl: false," +
        //                           "   panControl: false," +
        //                           "   zoomControlOptions: {" +
        //                           "      position: google.maps.ControlPosition.LEFT_BOTTOM" +
        //                          "    }" +
        //                          "  });" +

        //                         "   var infowindow = new google.maps.InfoWindow({" +
        //                          "    maxWidth: 160" +
        //                          "  });" +

        //                         "   var marker;" +
        //                          "  var markers = new Array();" +

        //                          "  var iconCounter = 0;" +

        //                       " var directionsDisplay= new google.maps.DirectionsRenderer();" +

        //                       " var directionsService = new google.maps.DirectionsService();" +
        //                       " directionsDisplay.setMap(map);" +


        //                           " for (var i = 0; i < locations.length; i++) {  " +
        //                           "   marker = new google.maps.Marker({" +
        //                           "     position: new google.maps.LatLng(locations[i][1], locations[i][2])," +
        //                           "     map: map," +
        //                          "      icon : icons[iconCounter]," +
        //                           "     shadow: shadow" +
        //                           "   });" +

        //                           "   markers.push(marker);" +

        //                           "   google.maps.event.addListener(marker, 'click', (function(marker, i) {" +
        //                           "     return function() {" +
        //                             "     infowindow.setContent(locations[i][0]);" +
        //                            "      infowindow.open(map, marker);" +
        //                            "    }" +
        //                            "  })(marker, i));" +

        //                           "   iconCounter++;" +
        //                           "   if(iconCounter >= icons_length){" +
        //                           "     iconCounter = 0;" +
        //                           "   }" +
        //                           " }" +

        //                            "function AutoCenter() {" +
        //                             "  var bounds = new google.maps.LatLngBounds();" +

        //                             " $.each(markers, function (index, marker) {" +
        //                            "    bounds.extend(marker.position);" +
        //                            "  });";

        //        //  "   map.fitBounds(bounds);";

        //        if (!string.IsNullOrEmpty(fromAddress) && !string.IsNullOrEmpty(toAddress))
        //        {
        //            text += "   map.fitBounds(bounds);";

        //            text += "  var start = '" + fromAddress + " UK';" +
        //              "    var end = '" + toAddress + "';" +
        //              "    var request = {" +
        //              "      origin: start," +
        //               "     destination: end," +
        //              "     travelMode: google.maps.TravelMode.DRIVING" +
        //              "    };" +



        //              "    directionsService.route(request, function(response, status) {" +
        //              "      if (status == google.maps.DirectionsStatus.OK) {" +
        //               "       directionsDisplay.setDirections(response);" +
        //               "     }" +
        //               "   });";
        //        }


        //        text += " };" + "  AutoCenter();" +

        //         "    </script> " +
        //         "  </body>" +
        //           "</html>";


        //        webBrowser1.DocumentText = text;


        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //}

        //private void FillDriversCombo()
        //{
        //    if (ddlDriver.DataSource == null)
        //    {

        //        ComboFunctions.FillDriverNoQueueCombo(ddlDriver);


        //    }
        //}




        #endregion

        
        void grdVia_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            try
            {
                if (e.CellElement is GridDataCellElement)
                {
                    if (e.Column is GridViewCommandColumn)
                    {

                        if (e.Column.Name == "ColMoveUp")
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.lc_moveup;

                        else if (e.Column.Name == "ColMoveDown")
                            ((RadButtonElement)e.CellElement.Children[0]).Image = Resources.Resource1.lc_movedown;
                    }


                }
            }
            catch
            {


            }
        }
        void grid_CommandCellClick(object sender, EventArgs e)
        {
            try
            {

                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == "ColDelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a via Address ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();

                        CalculateAutoFares();
                    }
                }
                else if (gridCell.ColumnInfo.Name == "ColRervP")
                {
                    if (txtFromAddress.Text != "" || ddlFromLocation.SelectedValue != null)
                    {
                        ReverceToPickUpPoint();

                        CalculateAutoFares();
                    }

                }
                else if (gridCell.ColumnInfo.Name == "ColRervD")
                {
                    if (txtToAddress.Text != "" || ddlToLocation.SelectedValue != null)
                    {
                        ReverceToDestination();
                        CalculateAutoFares();
                    }
                }
                else if (gridCell.ColumnInfo.Name == "ColMoveUp")
                {
                    MoveRow(true);
                }
                else if (gridCell.ColumnInfo.Name == "ColMoveDown")
                {
                    MoveRow(false);
                }
            }
            catch
            {


            }

        }

        private void MoveRow(bool moveUp)
        {
            try
            {
                GridViewRowInfo currentRow = this.grdVia.CurrentRow;
                if (currentRow == null)
                {
                    return;
                }

                int index = moveUp ? currentRow.Index - 1 : currentRow.Index + 1;

                if (index < 0 || index >= this.grdVia.RowCount)
                {
                    return;
                }
                this.grdVia.Rows.Move(index, currentRow.Index);
                this.grdVia.CurrentRow = this.grdVia.Rows[index];
            }
            catch
            {


            }
        }

        private void CalculateAutoFareUI()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UIDelegate(CalculateTotalFares));


            }
            else
            {
                CalculateTotalFares();
            }

        }
        private void MileageError()
        {
            lblMap.Text = "Mileage not found";

        }
        private void CalculateTotalFares()
        {

            try
            {



            //    int? vehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();

                int? fromLocationId = ddlFromLocation.SelectedValue.ToIntorNull();
                int? fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();

                int? toLocTypeId = ddlToLocType.SelectedValue.ToInt();
                int? toLocationId = ddlToLocation.SelectedValue.ToIntorNull();

                string fromLocName = ddlFromLocation.SelectedItem != null ? (ddlFromLocation.SelectedItem as RadComboBoxItem).Text.Trim() : ddlFromLocation.Text.Trim();
                string toLocName = ddlToLocation.SelectedItem != null ? (ddlToLocation.SelectedItem as RadComboBoxItem).Text.Trim() : ddlToLocation.Text.Trim();

                string fromAddress = txtFromAddress.Text.Trim().ToUpper();
                string toAddress = txtToAddress.Text.Trim().ToUpper();
                string fromPostCode = txtFromPostCode.Text.Trim().ToUpper();
                string toPostCode = txtToPostCode.Text.Trim().ToUpper();

                string tempFromPostCode = string.Empty;
                string tempToPostCode = string.Empty;

                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    tempFromPostCode = General.GetPostCodeMatch(fromAddress);

                }
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    tempFromPostCode = fromPostCode;
                }
                else
                {
                    tempFromPostCode = General.GetPostCodeMatch(fromLocName);
                }



                if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    tempToPostCode = General.GetPostCodeMatch(toAddress);

                }
                else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    tempToPostCode = toPostCode;
                }
                else
                {
                    tempToPostCode = General.GetPostCodeMatch(toLocName);
                }


                if (objMaster.PrimaryKeyValue != null && objMaster.Current != null)
                {



                    if ((objMaster.Current.FromAddress.ToStr().ToUpper().Equals(fromAddress)
                        && objMaster.Current.ToAddress.ToStr().ToUpper().Equals(toAddress)) && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                    {
                        return;


                    }

                }

                lblMap.Text = string.Empty;

                mileageError = false;
                CalculateFares();


                string via = string.Empty;

                if (grdVia != null && grdVia.Rows.Count > 0)
                {

                    via = "&waypoints=";

                    via += string.Join("|", grdVia.Rows.Select(c => General.GetPostCodeMatch(c.Cells["VIALOCATIONVALUE"].Value.ToStr().ToUpper()) + ", UK").ToArray<string>());


                }



                string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + tempFromPostCode + ", UK&destination=" + tempToPostCode + ", UK" + via + "&sensor=false";

                using (System.Data.DataSet ds = new System.Data.DataSet())
                {
                    using (XmlTextReader reader = new XmlTextReader(url2))
                    {

                        ds.ReadXml(reader);
                    }
                    if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
                    {
                        url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
                        MileageError();
                    }
                    else
                    {






                        decimal miles = milesList.Sum();
                        if (miles > 10000 || mileageError)
                            MileageError();
                        else
                        {

                            string time = string.Empty;

                            if (string.IsNullOrEmpty(via))
                            {
                                DataTable dt = ds.Tables["duration"];
                                DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
                                time = row.ItemArray[1].ToString();
                                dt.Dispose();
                                dt = null;
                            }
                            else
                            {
                                var rows = ds.Tables["duration"].Rows.OfType<DataRow>().Where(c => c[2].ToStr() == string.Empty);

                                time = (Math.Round((rows.Sum(c => Convert.ToDouble(c[0])) / 60), 0)).ToStr();
                                time += " mins";


                            }


                            lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
                        }
                    }
                }

                CalculateTotalCharges();


            }
            catch (Exception ex)
            {

            }
        }

        private void CalculateTotalCharges()
        {
            try
            {
                numTotalChrgs.Value = numFareRate.Value + numParkingChrgs.Value + numWaitingChrgs.Value + numExtraChrgs.Value + numCongChrgs.Value + numMeetCharges.Value;

                if (numCustomerFares.Value == 0)
                {
                    numCustomerFares.Value = numFareRate.Value;
                }


                if (ddlCompany.SelectedValue != null && numCompanyFares != null && numCompanyFares.Value == 0)
                {



                    decimal fare = numFareRate.Value.ToDecimal();

                    if (companyPricePercentage > 0)
                    {
                        fare += (fare * companyPricePercentage) / 100;

                    }

                    numCompanyFares.Value = fare;
                }



            }
            catch (Exception ex)
            {


            }

        }
        private bool mileageError = false;
        private void CalculateAutoFares()
        {
            try
            {
                if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                {


                    th = new System.Threading.Thread(new ThreadStart(CalculateAutoFareUI));
                    th.IsBackground = true;
                    th.Start();

                }
            }
            catch (Exception ex)
            {

            }

        }

        int companyPricePercentage = 0;

        private void CalculateFares()
        {
            milesList.Clear();
            int? vehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
            int companyId = ddlCompany.SelectedValue.ToInt();
            int? fromLocationId = ddlFromLocation.SelectedValue.ToIntorNull();
            int? fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
            DateTime bookingDate = DateTime.Now.ToDate();

            int? toLocTypeId = ddlToLocType.SelectedValue.ToInt();
            int? toLocationId = ddlToLocation.SelectedValue.ToIntorNull();

            string fromLocName = ddlFromLocation.SelectedItem != null ? (ddlFromLocation.SelectedItem as RadComboBoxItem).Text.Trim() : ddlFromLocation.Text.Trim();
            string toLocName = ddlToLocation.SelectedItem != null ? (ddlToLocation.SelectedItem as RadComboBoxItem).Text.Trim() : ddlToLocation.Text.Trim();

            string fromAddress = txtFromAddress.Text.Trim().ToUpper();
            string toAddress = txtToAddress.Text.Trim().ToUpper();
            string fromPostCode = txtFromPostCode.Text.Trim().ToUpper();
            string toPostCode = txtToPostCode.Text.Trim().ToUpper();




            if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
            {

                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS && string.IsNullOrEmpty(General.GetPostCodeMatch(fromAddress)) && !string.IsNullOrEmpty(txtFromAddress.ListBoxElement.Text))
                {

                    fromAddress = txtFromAddress.ListBoxElement.Text.Trim();
                }

                if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS && string.IsNullOrEmpty(General.GetPostCodeMatch(toAddress)) && !string.IsNullOrEmpty(txtToAddress.ListBoxElement.Text))
                {

                    toAddress = txtToAddress.ListBoxElement.Text.Trim();
                }
            }




            int fromZoneId = ddlPickupPlot.SelectedValue.ToInt();
            int toZoneId = ddlDropOffPlot.SelectedValue.ToInt();

            DateTime? pickupdateTime = dtpPickupTime.Value;

            List<string> errors = new List<string>();

            int orderId = 2;



            if (vehicleTypeId == null)
            {
                errors.Add("Required : Vehicle Type");

            }



            if (errors.Count > 0)
            {
                ENUtils.ShowMessage(string.Join(Environment.NewLine, errors.Select(c => c).ToArray<string>()));
                //    return false;
                return;
            }



            bool IsZoneWise = false;


            if (ddlFromLocType.Items.Count(c => c.Text == "Zone") > 0)
                IsZoneWise = true;



            int tempFromLocId = 0;
            int tempToLocId = 0;
            string tempFromPostCode = "";
            string tempToPostCode = "";
            string errorMsg = string.Empty;

            decimal fareVal = 0.00m;
            decimal deadMileage = AppVars.objPolicyConfiguration.DeadMileage.ToDecimal();





            if (pnlVia != null)
            {
                var viaList = (from r in grdVia.Rows
                               select new
                               {
                                   OrderNo = orderId++,
                                   FromTypeId = r.Cells["FROMVIALOCTYPEID"].Value.ToInt(),
                                   LocId = r.Cells["VIALOCATIONID"].Value.ToInt(),
                                   LocAddressPostCode = r.Cells["VIALOCATIONVALUE"].Value.ToStr()
                               }).ToList();

                for (int i = 0; i < viaList.Count(); i++)
                {
                    var item = viaList[i];

                    if (item.OrderNo == 2)
                    {
                        tempFromLocId = fromLocationId.ToInt();
                        if (tempFromLocId != 0)
                        {
                            tempFromPostCode = fromLocName;
                        }
                        else
                            tempFromPostCode = fromAddress != string.Empty ? fromAddress : fromPostCode;

                    }
                    else
                    {
                        tempFromLocId = viaList[i - 1].LocId;
                        tempFromPostCode = viaList[i - 1].LocAddressPostCode;

                    }

                    tempToLocId = item.LocId;
                    tempToPostCode = item.LocAddressPostCode;



                    // Fare Calculation
              //      fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, true, IsZoneWise, pickupdateTime, ref deadMileage);




                    if (errorMsg == "Error")
                        break;

                }
            }




            if (errorMsg == "Error")
            {
                numFareRate.Value = 0;
                // return false;
                return;
            }

            if (tempToLocId == 0 && string.IsNullOrEmpty(tempToPostCode))
            {
                tempFromLocId = fromLocationId.ToInt();
                if (tempFromLocId != 0)
                {
                    tempFromPostCode = fromLocName;
                }
                else
                    tempFromPostCode = fromAddress != string.Empty ? fromAddress : fromPostCode;

            }
            else
            {
                tempFromLocId = tempToLocId;
                tempFromPostCode = tempToPostCode;

            }

            tempToLocId = toLocationId.ToInt();
            if (tempToLocId != 0)
                tempToPostCode = toLocName;
            else
                tempToPostCode = toAddress != string.Empty ? toAddress : toPostCode;









            // Fare Calculation
       //     fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, IsZoneWise, pickupdateTime, ref deadMileage);



            if (errorMsg == "Error")
            {

                numFareRate.Value = 0;
                //return false;
                return;
            }



            decimal dd;

            if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool() == false)
            {
                dd = fareVal.ToDecimal();
            }
            else
            {
                string ff = string.Format("{0:#}", fareVal);
                if (ff == string.Empty)
                    ff = "0";

                dd = ff.ToDecimal();
            }

            decimal airportPickupChrgs = AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();
            //// Add Airport Pickup Charges If Pickup Point is From Airport...
            if (companyId == 0 && ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT && errorMsg == "Reverse found")
            {


                if (AppVars.objPolicyConfiguration.HasMultipleAirportPickupCharges.ToBool() && fromLocationId != null)
                {
                    airportPickupChrgs = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == fromLocationId).DefaultIfEmpty().Charges.ToDecimal();
                    // dd += General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == fromLocationId).DefaultIfEmpty().Charges.ToDecimal();
                    dd += airportPickupChrgs;
                }
                else
                {

                    dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();
                }


            }

            // Set Increment Fares

            var objInc = General.GetObject<Fare_IncrementSetting>(c => c.Id != 0 && c.EnableIncrement != null && c.EnableIncrement == true);

            if (objInc != null)
            {

                bool IsExist = false;


                if ((objInc.CriteriaBy.ToInt() == 1 && DateTime.Now >= objInc.FromDate && DateTime.Now <= objInc.TillDate)
                || (objInc.CriteriaBy.ToInt() == 2 && DateTime.Now.ToDate() >= objInc.FromDate.ToDate() && DateTime.Now.ToDate() <= objInc.TillDate.ToDate())
                    )
                {

                    IsExist = true;

                }
                else if (objInc.CriteriaBy.ToInt() == 3)
                {
                    string str = dtpPickupTime.Value.Value.TimeOfDay.ToStr();
                    // string str = DateTime.Now.TimeOfDay.ToStr();

                    str = str.Substring(0, str.LastIndexOf(':'));
                    str = str.Replace(":", "").Trim();

                    int time = str.ToInt();


                    str = objInc.FromDate.Value.TimeOfDay.ToStr();
                    str = str.Substring(0, str.LastIndexOf(':'));
                    str = str.Replace(":", "").Trim();
                    int fromTime = str.ToInt();


                    str = objInc.TillDate.Value.TimeOfDay.ToStr();
                    str = str.Substring(0, str.LastIndexOf(':'));
                    str = str.Replace(":", "").Trim();
                    int toTime = str.ToInt();




                    if (time < 1000)
                    {

                        // PEAK FARES

                        if (fromTime < 1000 && toTime < 1000)
                        {
                            if (time >= fromTime && time <= toTime)
                            {
                                IsExist = true;
                            }
                        }
                        // 6 AM (600) TO 15 PM (1500)
                        else if (fromTime < 1000 && toTime > 1000)
                        {
                            if (time >= fromTime && time <= toTime)
                            {
                                IsExist = true;
                            }
                        }

                        // 6 PM (1800) TO 6 AM (600)
                        else if (fromTime > 1000 && toTime < 1000)
                        {

                            if (time <= toTime)
                            {
                                IsExist = true;
                            }
                        }

                        // OFF PEAK FARES

                        if (fromTime < 1000 && toTime < 1000)
                        {
                            if (time >= fromTime
                                    && time <= toTime)
                            {
                                IsExist = true;
                            }
                        }
                        // 6 AM (600) TO 15 PM (1500)
                        else if (fromTime < 1000 && toTime > 1000)
                        {
                            if (time >= fromTime
                                    && time <= toTime)
                            {
                                IsExist = true;
                            }
                        }

                        // 6 PM (1800) TO 6 AM (600)
                        else if (fromTime > 1000 && toTime < 1000)
                        {

                            if (time <= toTime)
                            {
                                IsExist = true;
                            }
                        }

                    }

                    else if (time >= 1000)
                    {
                        if ((fromTime < 1000 && toTime >= 1000)
                                || (fromTime >= 1000 && toTime >= 1000))
                        {

                            // 6 AM (600) TO 6PM (1700)
                            if (time >= fromTime && time <= toTime)
                            {
                                IsExist = true;
                            }

                            else if ((fromTime >= 1000 && toTime < 1000))
                            {

                                if (time >= fromTime)
                                {
                                    IsExist = true;
                                }
                            }
                            else if ((toTime > fromTime && time < (toTime - fromTime))
                                || (fromTime > toTime && time > (fromTime - toTime)))
                            {
                                IsExist = true;

                            }

                        }

                        else if ((fromTime < 1000 && toTime >= 1000)
                                || (fromTime >= 1000 && toTime >= 1000))
                        {

                            // 6 AM (600) TO 6PM (1700)
                            if (time >= fromTime
                                    && time <= toTime)
                            {
                                IsExist = true;
                            }

                        }

                        else if ((fromTime >= 1000 && toTime < 1000))
                        {

                            // 6 AM (600) TO 6PM (1700)
                            if (time >= fromTime)
                            {
                                IsExist = true;
                            }

                        }

                    }

                }



                if (IsExist)
                {

                    if (objInc.IncrementType.ToStr() == "percent")
                    {
                        dd = dd + ((dd * objInc.IncrementRate.ToDecimal()) / 100);

                        if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                        {

                            dd = Math.Ceiling(dd);
                        }
                    }
                    else
                    {
                        dd += objInc.IncrementRate.ToDecimal();

                    }
                }
            }

            //


            numFareRate.Value = dd;

            numCustomerFares.Value = numFareRate.Value;

            if (companyId != 0 && numCompanyFares != null)
            {

                if (companyPricePercentage > 0)
                {
                    dd += (dd * companyPricePercentage) / 100;

                }



                numCompanyFares.Value = dd;
            }

            if (opt_JReturnWay.ToggleState == ToggleState.On && numReturnFare != null)
            {
                // numReturnFare.Value = numFareRate.Value;

                numReturnFare.Value = numFareRate.Value - ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForReturnedJourneyPercent.ToInt()) / 100);

                if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    numReturnFare.Value -= airportPickupChrgs;


                else if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    numReturnFare.Value += airportPickupChrgs;


            }
            else if (opt_WaitandReturn.ToggleState == ToggleState.On)
            {
                numFareRate.Value = numFareRate.Value + ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForWRJourneyPercent.ToInt()) / 100);
            }
            else
            {
                if (numReturnFare != null)
                    numReturnFare.Value = 0;
            }



            //return true;

        }
        public void AddColumn(RadGridView grid, string name, string headerText, Bitmap img)
        {
            try
            {
                GridViewCommandColumn col = new GridViewCommandColumn();
                col.Name = name;

                if (img == null)
                {
                    col.UseDefaultText = true;
                    col.DefaultText = headerText;
                    col.BestFit();
                }
                else
                {
                    col.Width = 30;
                }

                col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
                col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                grid.Columns.Add(col);

                grid.NewRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.EnterMovesToNextRow;
            }
            catch
            {


            }
        }

        void grdVia_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                grdVia.CurrentRow = null;
            }
        }
        private void FillViaLocations()
        {


            int locTypeId = ddlViaFromLocType.SelectedValue.ToInt();

            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
            {
                lblViaLoc.Text = "Via Address";
                txtViaAddress.Visible = true;

                ddlViaLocation.SelectedValue = null;
                ddlViaLocation.Visible = false;

                txtviaPostCode.Text = string.Empty;
                txtviaPostCode.Visible = false;


                if (locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    txtViaAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    txtViaAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                    txtViaAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                }


            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtViaAddress.Text = string.Empty;
                txtViaAddress.Visible = false;

                ddlViaLocation.SelectedValue = null;
                ddlViaLocation.Visible = false;

                txtviaPostCode.Visible = true;


                lblViaLoc.Text = "Via PostCode";



            }


            else
            {
                txtviaPostCode.Text = string.Empty;
                txtviaPostCode.Visible = false;

                txtViaAddress.Text = string.Empty;
                txtViaAddress.Visible = false;


                ddlViaLocation.Visible = true;
                lblViaLoc.Text = "Via Location";
                ComboFunctions.FillLocationsCombo(ddlViaLocation, c => c.LocationTypeId == locTypeId);

            }
        }

        void ddlViaLocation_OnRefreshing(object sender, EventArgs e)
        {
            FillViaLocations();
        }
        private void DisplayBooking_ViaLocations()
        {
            if (objMaster.Current.Booking_ViaLocations.Count > 0)
            {
                CreateViaPanel();


                GridViewRowInfo row = null;
                foreach (var item in objMaster.Current.Booking_ViaLocations)
                {
                    row = grdVia.Rows.AddNew();
                    row.Cells["ID"].Value = item.Id;
                    row.Cells["MASTERID"].Value = item.BookingId;
                    row.Cells["FROMTYPELABEL"].Value = "Via";
                    // row.Cells[COLS.FROMTYPELABEL].Value = item.ViaLocTypeLabel;
                    row.Cells["FROMTYPEVALUE"].Value = item.ViaLocTypeValue;
                    row.Cells["FROMVIALOCTYPEID"].Value = item.ViaLocTypeId;

                    row.Cells["VIALOCATIONID"].Value = item.ViaLocId;
                    row.Cells["VIALOCATIONLABEL"].Value = item.ViaLocLabel;
                    row.Cells["VIALOCATIONVALUE"].Value = item.ViaLocValue;

                }


                grdVia.CurrentRow = null;

               
                ClearViaDetails();

               // radToggleButton1.Text = "Show Via Point(" + grdVia.Rows.Count + ")";

                btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                btnSelectVia.ButtonElement.ButtonFillElement.BackColor = Color.DarkOrange;
                btnSelectVia.ButtonElement.ButtonFillElement.NumberOfColors = 1;
                pnlVia.Visible = false;
            }

        }

        private void ddlVehicleType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void frmAdvanceBookingCustomization_Load(object sender, EventArgs e)
        {

        }

    }
}
