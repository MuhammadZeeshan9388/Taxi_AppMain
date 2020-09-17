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
    public partial class frmAdvanceBookingCustomization : RadForm
    {
        bool IsPaymentTypeLoaded = false;
        string ViaPoint = string.Empty;
        //List<Booking> MasterBookingList;
        List<Booking> oneWayBookingList;
        List<Booking> returnBookingList;

        bool IsApplyChanges = false;
        //OneWayVariables
        bool Destination = false;
        bool cPickup = false;
        string cCustomerName = string.Empty;
        string CustomerEmail = string.Empty;
        string CustomerMobileNo = string.Empty;
        string CustomerPhoneNo = string.Empty;
        int? NoofLuggages = 0;
        int? NoofPassengers = 0;
        int? CompanyId = 0;
        int? PaymentTypeId = 0;
        int? VehicleTypeId = 0;
        bool cAllocateOnewayDrv = false;
        bool cChangeAction = false;
        bool cDestination = false;
        //cDestination= chkDestination.Checked
        bool cPickupFares = false;

        int? DriverId = 0;

        bool cCustomerPrice = false;
        decimal CustomerPrice = 0;
        bool cCompanyPrice = false;
        decimal CompanyPrice = 0;
        string SpecialRequirements = string.Empty;
        string OrderNo = string.Empty;
        int? FromLocId = 0;
        bool cReturnDestination = false;
        int? ToLocId = 0;
        //ToLocId = ddlReturnToLocation.SelectedValue.ToIntorNull();
        //

        //ReturnWayVariables
        bool ReturnDestination = false;
        bool ReturncPickup = false;
        string ReturncCustomerName = string.Empty;
        string ReturnCustomerEmail = string.Empty;
        string ReturnCustomerMobileNo = string.Empty;
        string ReturnCustomerPhoneNo = string.Empty;
        int? ReturnNoofLuggages = 0;
        int? ReturnNoofPassengers = 0;
        int? ReturnCompanyId = 0;
        int? ReturnPaymentTypeId = 0;
        int? ReturnVehicleTypeId = 0;
        bool ReturncAllocateOnewayDrv = false;
        bool ReturncChangeAction = false;
        bool ReturncDestination = false;
        //cDestination= chkDestination.Checked
        bool ReturncPickupFares = false;
        bool ReturnAllocateDrv = false;
        int? ReturnDriverId = 0;

        bool ReturncCustomerPrice = false;
        decimal ReturnCustomerPrice = 0;
        bool ReturncCompanyPrice = false;
        decimal ReturnCompanyPrice = 0;
        string ReturnSpecialRequirements = string.Empty;
        string ReturnOrderNo = string.Empty;
        int? ReturnFromLocId = 0;
        bool ReturncReturnDestination = false;
        int? ReturnToLocId = 0;
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
            public static string DriverNo = "Drv";


            public static string FromDoorNo = "FromDoorNo";
            public static string FromStreet = "FromStreet";
            public static string FromPostCode = "FromPostCode";
            public static string ToDoorNo = "ToDoorNo";
            public static string ToStreet = "ToStreet";
            public static string ToPostCode = "ToPostCode";
            public static string StatusId = "StatusId";

            public static string ChangeBooking = "ChangeBooking";
            public static string ViaPoint = "ViaPoint";
            public static string BookingStatus = "BookingStatus";
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


        private bool EnablePOI;
        private Booking _objBookiing;

        public Booking ObjBookiing
        {
            get { return _objBookiing; }
            set { _objBookiing = value; }
        }

        BackgroundWorker worker = null;
        class CurrentRow
        {
            public int index;
            public string UpdateValue;
            public int Total;
        }
        public frmAdvanceBookingCustomization(long id)
        {
            InitializeComponent();
            this.ObjAdvanceBooking = General.GetObject<AdvanceBooking>(c => c.Id == id);
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.ProgressChanged += Worker_ProgressChanged;
            InitializeFormSettings();
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentRow cr = e.UserState as CurrentRow;
            if (cr != null)
            {
                lblBookingNo.Text = "Saving (" + cr.UpdateValue + ") " + (cr.index) + " out of " + cr.Total + "";
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (Saved)
            {
                this.Close();
            }
            else
            {
                ENUtils.ShowMessage(ErrorMessage);
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Save();
        }

        private void InitializeFormSettings()
        {
            try
            {



                //

                //   optDaily.ToggleState = ToggleState.On;
                FormatBookingGrid();

                FormatPickupsGrid(grdPickupDates);
                FormatPickupsGrid(grdExcludePickupDates);


                grdBookings.ViewCellFormatting += new CellFormattingEventHandler(grdBookings_ViewCellFormatting);
                this.Shown += new EventHandler(frmMultiBooking_Shown);
                this.FormClosing += new FormClosingEventHandler(frmAdvanceBookingCustomization_FormClosing);
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

                ddlToLocType.SelectedIndexChanged += new EventHandler(ddlToLocType_SelectedIndexChanged);


                ComboFunctions.FillLocationTypeCombo(ddlFromLocType);
                ComboFunctions.FillLocationTypeCombo(ddlToLocType);


                SetFromAddress(ToggleState.Off);
                SetToAddress(ToggleState.Off);
                FillCombo();





                // Return Booking Tab Details

                FormatReturnBookingGrid();
                grdReturnBookings.ViewCellFormatting += new CellFormattingEventHandler(grdBookings_ViewCellFormatting);

                //FormatReturnPickupsGrid(grdReturnPickupDates);
                //FormatPickupsGrid(grdExcludeReturnPickupDates);
                FormatRetunPickupsGrid(grdReturnPickupDates);
                FormatRetunPickupsGrid(grdExcludeReturnPickupDates);

                this.txtReturnFromAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);
                this.txtReturnToAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);


                txtReturnFromAddress.ListBoxElement.Width = 610;
                txtReturnFromAddress.ListBoxElement.Height = 400;
                txtReturnToAddress.ListBoxElement.Width = 610;



                font = new Font("Tahoma", 11, FontStyle.Bold);
                txtReturnFromAddress.ListBoxElement.Font = font;
                txtReturnToAddress.ListBoxElement.Font = font;


                txtReturnFromAddress.ListBoxElement.ItemHeight = 30;
                txtReturnToAddress.ListBoxElement.ItemHeight = 30;
                txtReturnFromAddress.ListBoxElement.DrawMode = DrawMode.OwnerDrawVariable;
                txtReturnFromAddress.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);

                //txtReturnFromAddress.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBoxElement_DrawItem);
                txtReturnToAddress.ListBoxElement.DrawMode = DrawMode.OwnerDrawVariable;
                txtReturnToAddress.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
                //txtReturnToAddress.ListBoxElement.DrawItem+=new DrawItemEventHandler(ListBoxElement_DrawItem);
                //Return Events

                ddlReturnFromLocType.SelectedIndexChanged += new EventHandler(ddlReturnFromLocType_SelectedIndexChanged);

                ddlReturnToLocType.SelectedIndexChanged += new EventHandler(ddlReturnToLocType_SelectedIndexChanged);

                ComboFunctions.FillLocationTypeCombo(ddlReturnFromLocType);
                ComboFunctions.FillLocationTypeCombo(ddlReturnToLocType);


                SetReturnFromAddress(ToggleState.Off);
                SetReturnToAddress(ToggleState.Off);

                FillReturnCombo();


                EnablePOI = AppVars.objPolicyConfiguration.EnablePOI.ToBool();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        void frmAdvanceBookingCustomization_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                if (worker != null)
                {
                    if (worker.IsBusy)
                    {
                        if (DialogResult.No == MessageBox.Show("Bookings are not fully saved! " + Environment.NewLine + "Do you still want to close this form ?", "Save Bookings", MessageBoxButtons.YesNo))
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            DisposeObjects();
                            return;
                        }

                    }
                    if (IsApplyChanges == false)
                    {
                        //IsFormClosed = true;
                        worker.CancelAsync();
                        worker.Dispose();
                        worker = null;
                    }
                }


            }
            catch
            {


            }
            if (IsApplyChanges == true)
            {

                if (DialogResult.Yes == MessageBox.Show("Do you want to Save Changes" + Environment.NewLine + "Press YES to Save Changes", "Message", MessageBoxButtons.YesNo))
                {
                    //worker.RunWorkerAsync();
                    //if (SaveChanges() == false)
                    //{


                    //    e.Cancel = true;

                    //}
                    //else
                    //{

                    //    DisposeObjects();
                    //}
                    e.Cancel = true;
                    SaveChanges();

                }
                else
                {
                    DisposeObjects();

                }






            }
        }

        private void DisposeObjects()
        {
            try
            {
                if (timer1 != null)
                {
                    timer1.Stop();
                    timer1.Dispose();
                    timer1 = null;

                    if (POIWorker != null)
                    {
                        if (POIWorker.IsBusy)
                        {

                            POIWorker.CancelAsync();
                        }


                        POIWorker.Dispose();
                        POIWorker = null;
                        GC.Collect();

                    }


                    if (worker != null)
                    {
                        if (worker.IsBusy)
                        {

                            worker.CancelAsync();
                        }


                        worker.Dispose();
                        worker = null;
                        GC.Collect();

                    }


                }
            }
            catch
            {

            }

        }

        void ListBoxElement_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        void ddlReturnToLocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillReturnToLocations();
        }

        void ddlReturnFromLocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillReturnFromLocations();
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

        private void FillReturnFromLocations()
        {
            int locTypeId = ddlReturnFromLocType.SelectedValue.ToInt();
            if (locTypeId == 0)
                return;
            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
            {
                txtReturnFromAddress.Visible = true;

                DetachLocationsSelectionEvent(ddlReturnFromLocation);
                ddlReturnFromLocation.SelectedValue = null;
                ddlReturnFromLocation.Visible = false;
                AttachLocationSelectionEvent(ddlReturnFromLocation);

                txtReturnFromPostCode.Text = string.Empty;
                txtReturnFromPostCode.Visible = false;

                lblReturnFromDoorFlightNo.Text = "Pickup Notes";
                lblReturnFromDoorFlightNo.Visible = true;

                //  radLabel39.Text=
                //  lblFromDoorFlightNo.Text = "Pickup Notes";
                //  lblFromDoorFlightNo.Visible = true;
                //   lblFromDoorFlightNo.Location=new Point(lblFromDoorFlightNo
                //   lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.NewFromDoorNoLoc.Y + 1);

                txtReturnFromFlightDoorNo.MaxLength = 100;
                txtReturnFromFlightDoorNo.Width = 170;

                txtReturnFromFlightDoorNo.Text = string.Empty;
                txtReturnFromFlightDoorNo.Visible = true;
                //   txtFromFlightDoorNo.Location = this.NewFromDoorNoLoc;


                txtReturnFromStreetComing.Text = string.Empty;
                txtReturnFromStreetComing.Visible = false;

                // lblFromDoorFlightNo.Visible = false;
                lblReturnFromStreetComing.Visible = false;

                lblReturnFromLoc.Text = "Pickup Point";

                if (locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    txtReturnFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    txtReturnFromAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                    txtReturnFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    SetPickupZone(txtReturnFromAddress.Text);
                }
            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {

                //  LoadPostCodes();

                txtReturnFromAddress.Text = string.Empty;
                txtReturnFromAddress.Visible = false;

                DetachLocationsSelectionEvent(ddlReturnFromLocation);
                ddlReturnFromLocation.SelectedValue = null;
                ddlReturnFromLocation.Visible = false;
                AttachLocationSelectionEvent(ddlReturnFromLocation);

                txtReturnFromFlightDoorNo.MaxLength = 100;
                txtReturnFromFlightDoorNo.Width = 170;

                //     lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.OldfromDoorNoLoc.Y);

                //     txtFromFlightDoorNo.Location = this.OldfromDoorNoLoc;
                txtReturnFromPostCode.Visible = true;
                txtReturnFromFlightDoorNo.Visible = true;
                txtReturnFromStreetComing.Visible = true;
                lblReturnFromDoorFlightNo.Visible = true;
                lblReturnFromStreetComing.Visible = true;

                lblReturnFromLoc.Text = "From PostCode";

                lblReturnFromDoorFlightNo.Text = "Door #";
                lblReturnFromStreetComing.Text = "From Street";

            }

            else if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                txtReturnFromAddress.Text = string.Empty;
                ///

                txtReturnFromAddress.Visible = false;

                ddlReturnFromLocation.Visible = true;

                txtReturnFromPostCode.Text = string.Empty;
                txtReturnFromPostCode.Visible = false;

                txtReturnFromFlightDoorNo.MaxLength = 100;
                txtReturnFromFlightDoorNo.Width = 170;

                ////    lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.OldfromDoorNoLoc.Y);
                //     txtFromFlightDoorNo.Location = this.OldfromDoorNoLoc;
                txtReturnFromFlightDoorNo.Visible = true;
                txtReturnFromStreetComing.Visible = true;
                lblReturnFromDoorFlightNo.Visible = true;
                lblReturnFromStreetComing.Visible = true;

                lblReturnFromLoc.Text = "From Location";

                lblReturnFromDoorFlightNo.Text = "Flight No";
                lblReturnFromStreetComing.Text = "Coming From";
                DetachLocationsSelectionEvent(ddlReturnFromLocation);
                ComboFunctions.FillLocationsCombo(ddlReturnFromLocation, c => c.LocationTypeId == locTypeId);
                ddlReturnFromLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlReturnFromLocation);
            }


            else
            {
                lblReturnFromLoc.Text = "From Location";

                txtReturnFromPostCode.Text = string.Empty;
                txtReturnFromPostCode.Visible = false;

                txtReturnFromFlightDoorNo.Text = string.Empty;
                txtReturnFromFlightDoorNo.Visible = false;

                txtReturnFromStreetComing.Text = string.Empty;
                txtReturnFromStreetComing.Visible = false;


                lblReturnFromDoorFlightNo.Text = "Pickup Notes";
                lblReturnFromDoorFlightNo.Visible = true;
                //   lblFromDoorFlightNo.Location=new Point(lblFromDoorFlightNo
                // lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.NewFromDoorNoLoc.Y + 1);

                txtReturnFromFlightDoorNo.MaxLength = 100;
                txtReturnFromFlightDoorNo.Width = 200;

                txtReturnFromFlightDoorNo.Text = string.Empty;
                txtReturnFromFlightDoorNo.Visible = true;
                //  txtFromFlightDoorNo.Location = this.NewFromDoorNoLoc;

                // lblFromDoorFlightNo.Visible = false;
                lblReturnFromStreetComing.Visible = false;

                txtReturnFromAddress.Text = string.Empty;
                txtReturnFromAddress.Visible = false;

                ddlReturnFromLocation.Visible = true;
                DetachLocationsSelectionEvent(ddlReturnFromLocation);
                ComboFunctions.FillLocationsCombo(ddlReturnFromLocation, c => c.LocationTypeId == locTypeId);
                ddlReturnFromLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlReturnFromLocation);
                txtReturnFromFlightDoorNo.MaxLength = 100;
                txtReturnFromFlightDoorNo.Width = 120;

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
        private void SetReturnOthersToLocation()
        {
            txtReturnToPostCode.Text = string.Empty;
            txtReturnToPostCode.Visible = false;

            txtReturnToFlightDoorNo.Text = string.Empty;
            txtReturnToFlightDoorNo.Visible = false;

            txtReturnToStreetComing.Text = string.Empty;
            txtReturnToStreetComing.Visible = false;

            lblToDoorFlightNo.Visible = false;
            lblToStreetComing.Visible = false;

            txtReturnToAddress.Text = string.Empty;
            txtReturnToAddress.Visible = false;


            ddlReturnToLocation.Visible = true;
            lblToLoc.Text = "To Location";


            // txtToFlightDoorNo.MaxLength = 100;
            // txtToFlightDoorNo.Width = 170;

            //


            lblToDoorFlightNo.Text = "Dest. Notes";
            lblToDoorFlightNo.Visible = true;
            // lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.NewtoDoorNoLoc.Y + 1);

            txtReturnToFlightDoorNo.MaxLength = 100;
            txtReturnToFlightDoorNo.Width = 200;

            txtReturnToFlightDoorNo.Text = string.Empty;
            txtReturnToFlightDoorNo.Visible = true;
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
        private void FillCombo()
        {
            ComboFunctions.FillVehicleTypeCombo(ddlVehicle);
            ComboFunctions.FillCompanyCombo(ddlAccount);
            ComboFunctions.FillPaymentTypeCombo(ddlPaymentType);
            ComboFunctions.FillDriverNoCombo(ddlDriver);
            IsPaymentTypeLoaded = true;
        }

        private void FillReturnCombo()
        {
            ComboFunctions.FillVehicleTypeCombo(ddlReturnVehicle);
            ComboFunctions.FillCompanyCombo(ddlReturnAccount);
            ComboFunctions.FillPaymentTypeCombo(ddlReturnPaymentType);
            ComboFunctions.FillDriverNoCombo(ddlReturnDriver);
            IsPaymentTypeLoaded = true;

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
        private void FillReturnToLocations()
        {


            int locTypeId = ddlReturnToLocType.SelectedValue.ToInt();
            if (locTypeId == 0)
                return;

            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
            {
                lblReturnToLoc.Text = "Destination";
                txtReturnToAddress.Visible = true;

                DetachLocationsSelectionEvent(ddlReturnToLocation);
                ddlReturnToLocation.SelectedValue = null;
                ddlReturnToLocation.Visible = false;
                AttachLocationSelectionEvent(ddlReturnToLocation);

                txtReturnToPostCode.Text = string.Empty;
                txtReturnToPostCode.Visible = false;


                lblReturnToDoorFlightNo.Text = "Dest. Notes";
                lblReturnToDoorFlightNo.Visible = true;
                //   lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.NewtoDoorNoLoc.Y + 1);

                txtReturnToFlightDoorNo.MaxLength = 100;
                txtReturnToFlightDoorNo.Width = 170;

                txtReturnToFlightDoorNo.Text = string.Empty;
                txtReturnToFlightDoorNo.Visible = true;
                //    txtToFlightDoorNo.Location = this.NewtoDoorNoLoc;

                txtReturnToStreetComing.Text = string.Empty;
                txtReturnToStreetComing.Visible = false;


                // lblToDoorFlightNo.Visible = false;
                lblReturnToStreetComing.Visible = false;


                if (locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    txtReturnToAddress.TextChanged -= new EventHandler(TextReturnBoxElement_TextChanged);
                    txtReturnToAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                    txtReturnToAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);

                }
            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtReturnToAddress.Text = string.Empty;
                txtReturnToAddress.Visible = false;

                DetachLocationsSelectionEvent(ddlReturnToLocation);
                ddlReturnToLocation.SelectedValue = null;
                ddlReturnToLocation.Visible = false;
                AttachLocationSelectionEvent(ddlReturnToLocation);

                //  txtToFlightDoorNo.Location = this.OldtoDoorNoLoc;
                txtReturnToPostCode.Visible = true;
                txtReturnToFlightDoorNo.Visible = true;
                txtReturnToStreetComing.Visible = true;
                lblReturnToDoorFlightNo.Visible = true;
                lblReturnToStreetComing.Visible = true;

                lblReturnToLoc.Text = "To PostCode";

                //   lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.OldtoDoorNoLoc.Y);
                lblReturnToDoorFlightNo.Text = "To Door No";
                lblReturnToStreetComing.Text = "To Street";

                txtReturnToFlightDoorNo.MaxLength = 100;
                txtReturnToFlightDoorNo.Width = 170;

            }

            else if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                //SetReturnAirportJob(opt_JReturnWay.ToggleState);
                DetachLocationsSelectionEvent(ddlReturnToLocation);
                ComboFunctions.FillLocationsCombo(ddlReturnToLocation, c => c.LocationTypeId == locTypeId);
                ddlReturnToLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlReturnToLocation);
            }


            else
            {



                SetReturnOthersToLocation();

                DetachLocationsSelectionEvent(ddlReturnToLocation);
                ComboFunctions.FillLocationsCombo(ddlReturnToLocation, c => c.LocationTypeId == locTypeId);
                ddlReturnToLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlReturnToLocation);
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
        private void FormatRetunPickupsGrid(RadGridView grid)
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

                grdExcludePickupDates.Rows.Clear();

                string contact = ObjAdvanceBooking.CustomerMobileNo.ToStr().Trim() + "/" + ObjAdvanceBooking.CustomerTelephoneNo.ToStr().Trim();

                txtBookedBy.Text = "Booked by " + ObjAdvanceBooking.AddLog.ToStr() + " on " + string.Format("{0:dd/MM/yyyy @ HH:mm}", ObjAdvanceBooking.AddOn);

                if (contact.EndsWith("/"))
                    contact = contact.Remove(contact.LastIndexOf('/'));

                if (contact.StartsWith("/"))
                    contact = contact.Substring(1, contact.Length - 1);



                CustomerName.Text = ObjAdvanceBooking.CustomerName.ToStr().Trim() + " - " + contact;


                var bookinglist = General.GetQueryable<Booking>(c => c.AdvanceBookingId == ObjAdvanceBooking.Id
                    //   (c.BookingStatusId==Enums.BOOKINGSTATUS.WAITING || c.BookingStatusId==Enums.BOOKINGSTATUS.ONHOLD
                    //|| c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING_START)
                    )
                    //       && c.BookingStatusId != Enums.BOOKINGSTATUS.CANCELLED && c.MasterJobId==null )
                    .OrderBy(c => c.PickupDateTime).ToList();



                //var oneWayBookingList = bookinglist.Where(c => c.MasterJobId == null).ToList();
                oneWayBookingList = bookinglist.Where(c => c.MasterJobId == null).ToList();
                DisplayOneWayBooking();
                returnBookingList = bookinglist.Where(c => c.MasterJobId != null).ToList();
                DisplayReturnBooking();


                this.ddlAccount.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlAccount_SelectedIndexChanged);
                this.ddlPaymentType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlPaymentType_SelectedIndexChanged);


                this.ddlReturnAccount.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlReturnAccount_SelectedIndexChanged);
                ddlReturnPaymentType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlReturnPaymentType_SelectedIndexChanged);


                chkShowOneWayDispatchedBookings.ToggleState = ToggleState.Off;
                chkShowReturnDispatchedBookings.ToggleState = ToggleState.Off;

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }


        }
        private void DisplayOneWayBooking()
        {
            try
            {
                bool ShowVia = false;
                //   bool showDispatchedBookings = false;



                grdBookings.Rows.Clear();
                grdPickupDates.Rows.Clear();
                grdBookings.RowCount = oneWayBookingList.Count;
                grdPickupDates.RowCount = grdBookings.RowCount;



                int rowCnt = oneWayBookingList.Count;

                grdBookings.BeginUpdate();
                grdPickupDates.BeginUpdate();

                Booking objFirstBooking = oneWayBookingList.FirstOrDefault(c=>c.BookingStatusId == Enums.BOOKINGSTATUS.WAITING);


                if(objFirstBooking==null)
                {
                    objFirstBooking = oneWayBookingList.FirstOrDefault();
                }

                ddlDriver.Enabled = false;

                int? firstAllocatedDrv = oneWayBookingList.Where(c => c.BookingStatusId == Enums.BOOKINGSTATUS.WAITING && c.DriverId != null).FirstOrDefault().DefaultIfEmpty().DriverId;

                for (int i = 0; i < rowCnt; i++)
                {
                    if (i == 0)
                    {
                        if (objFirstBooking == null)
                            objFirstBooking = oneWayBookingList[i];


                        dtpPickupTime.Value = objFirstBooking.PickupDateTime;
                        dtpDayWisePickupTime.Value = objFirstBooking.PickupDateTime;

                        dtpStartingAt.Value = objFirstBooking.PickupDateTime;
                        dtpStartingCancel.Value = objFirstBooking.PickupDateTime;
                        ddlFromLocType.SelectedValue = objFirstBooking.FromLocTypeId;
                        ddlToLocType.SelectedValue = objFirstBooking.ToLocTypeId;


                        ddlFromLocation.SelectedValue = objFirstBooking.FromLocId;
                        ddlToLocation.SelectedValue = objFirstBooking.ToLocId;

                        txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        txtFromAddress.Text = objFirstBooking.FromAddress.ToStr();
                        txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                        txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        txtToAddress.Text = objFirstBooking.ToAddress.ToStr();
                        txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                        txtFromFlightDoorNo.Text = objFirstBooking.FromDoorNo.ToStr();
                        txtToFlightDoorNo.Text = objFirstBooking.ToDoorNo.ToStr();




                        txtFromStreetComing.Text = objFirstBooking.FromStreet.ToStr();

                        txtCustomerName.Text = objFirstBooking.CustomerName.ToStr();
                        txtEmail.Text = objFirstBooking.CustomerEmail.ToStr();
                        txtMobileNo.Text = objFirstBooking.CustomerMobileNo.ToStr();
                        txtTelephoneNo.Text = objFirstBooking.CustomerPhoneNo.ToStr();
                        num_TotalPassengers.Value = objFirstBooking.NoofPassengers.ToDecimal();
                        numTotalLuggages.Value = objFirstBooking.NoofLuggages.ToDecimal();
                        numCustomerPrice.Value = objFirstBooking.CustomerPrice.ToDecimal();
                        numCompanyPrice.Value = objFirstBooking.CompanyPrice.ToDecimal();
                        numPickupFares.Value = objFirstBooking.FareRate.ToDecimal();

                        ddlPaymentType.SelectedValue = objFirstBooking.PaymentTypeId.ToInt();


                        chkCancelBooking.Checked = false;
                        gbCancelBooking.Visible = false;


                        ddlAccount.SelectedValue = objFirstBooking.CompanyId;

                        ddlVehicle.SelectedValue = objFirstBooking.VehicleTypeId.ToInt();

                        ddlDriver.SelectedValue = firstAllocatedDrv;


                        txtOrderNo.Text = objFirstBooking.OrderNo.ToStr();


                        //if (bookinglist[i].JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY)
                        //{
                        txtSpecialReq.Text = objFirstBooking.SpecialRequirements.ToStr();
                        numCustomerPrice.Value = objFirstBooking.CustomerPrice.ToDecimal();
                        numCompanyPrice.Value = objFirstBooking.CompanyPrice.ToDecimal();
                        //    }





                        if (ShowVia == false)
                        {


                            DisplayBooking_ViaLocations(objFirstBooking.Booking_ViaLocations);
                            ShowVia = true;
                        }
                    }


                    else if (i == (rowCnt - 1))
                    {

                        dtpEndingAt.Value = oneWayBookingList[i].PickupDateTime;

                        dtpEndingCancel.Value = oneWayBookingList[i].PickupDateTime;
                    }




                    grdPickupDates.Rows[i].Cells[COLS_PICKUPS.ID].Value = oneWayBookingList[i].Id;
                    grdBookings.Rows[i].Cells[COLS.BookingStatus].Value = oneWayBookingList[i].BookingStatusId;

                    if (oneWayBookingList[i].PickupDateTime != null)
                    {
                        grdPickupDates.Rows[i].Cells[COLS_PICKUPS.DAY].Value = string.Format("{0:dddd}", oneWayBookingList[i].PickupDateTime);
                        grdPickupDates.Rows[i].Cells[COLS_PICKUPS.PickupDate].Value = oneWayBookingList[i].PickupDateTime;
                        grdBookings.Rows[i].Cells[COLS.ID].Value = oneWayBookingList[i].Id;


                        grdBookings.Rows[i].Cells[COLS.DAY].Value = string.Format("{0:dddd}", oneWayBookingList[i].PickupDateTime);
                        grdBookings.Rows[i].Cells[COLS.PickupDate].Value = oneWayBookingList[i].PickupDateTime.ToDateTime();//string.Format("{0:dd/MM/yyyy HH:mm}", bookinglist[i].PickupDateTime.ToDateTime());
                        //grdBookings.Rows[i].Cells[COLS.PickupDate].Value = bookinglist[i].PickupDateTime.ToDateTime() != null ? "" + bookinglist[i].PickupDateTime.ToDateTime() : "";
                    }



                    if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                    {
                        grdBookings.Rows[i].Cells[COLS.FromAddress].Value = oneWayBookingList[i].FromAddress.ToStr();
                    }
                    else if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        grdBookings.Rows[i].Cells[COLS.FromAddress].Value = oneWayBookingList[i].FromPostCode.ToStr();
                    }
                    else
                    {
                        grdBookings.Rows[i].Cells[COLS.FromAddress].Value = oneWayBookingList[i].Gen_Location1.DefaultIfEmpty().LocationName.ToStr().ToUpper();
                        //NC
                        if (oneWayBookingList[i].Gen_Location1 == null)
                        {
                            grdBookings.Rows[i].Cells[COLS.FromAddress].Value = oneWayBookingList[i].FromAddress.ToStr();
                        }
                    }

                    ////  grdBookings.Rows[i].Cells[COLS.FromAddress].Value = bookinglist[i].FromAddress;

                    if (oneWayBookingList[i].MasterJobId != null)
                    {
                        if (oneWayBookingList[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || oneWayBookingList[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = oneWayBookingList[i].ToAddress.ToStr();
                        }
                        else if (oneWayBookingList[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = oneWayBookingList[i].ToPostCode.ToStr();
                        }
                        else
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = oneWayBookingList[i].Gen_Location2.DefaultIfEmpty().LocationName.ToStr().ToUpper();
                            //NC
                            if (oneWayBookingList[i].Gen_Location2 == null)
                            {
                                grdBookings.Rows[i].Cells[COLS.ToAddress].Value = oneWayBookingList[i].ToAddress.ToStr();
                            }
                        }
                    }
                    else
                    {
                        if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = oneWayBookingList[i].ToAddress.ToStr();
                        }
                        else if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = oneWayBookingList[i].ToPostCode.ToStr();
                        }
                        else
                        {
                            grdBookings.Rows[i].Cells[COLS.ToAddress].Value = oneWayBookingList[i].Gen_Location2.DefaultIfEmpty().LocationName.ToStr().ToUpper();
                            //NC
                            if (oneWayBookingList[i].Gen_Location2 == null)
                            {
                                grdBookings.Rows[i].Cells[COLS.ToAddress].Value = oneWayBookingList[i].ToAddress.ToStr();
                            }
                        }
                    }







                    grdBookings.Rows[i].Cells[COLS.MasterJobId].Value = oneWayBookingList[i].MasterJobId;


                    grdBookings.Rows[i].Cells[COLS.Fare].Value = oneWayBookingList[i].FareRate.ToDecimal();

                    grdBookings.Rows[i].Cells[COLS.FromLocTypeId].Value = oneWayBookingList[i].FromLocTypeId;
                    grdBookings.Rows[i].Cells[COLS.FromLocId].Value = oneWayBookingList[i].FromLocId;

                    grdBookings.Rows[i].Cells[COLS.ToLocTypeId].Value = oneWayBookingList[i].ToLocTypeId;
                    grdBookings.Rows[i].Cells[COLS.ToLocId].Value = oneWayBookingList[i].ToLocId;

                    grdBookings.Rows[i].Cells[COLS.FromDoorNo].Value = oneWayBookingList[i].FromDoorNo.ToStr();
                    grdBookings.Rows[i].Cells[COLS.FromStreet].Value = oneWayBookingList[i].FromStreet.ToStr();
                    grdBookings.Rows[i].Cells[COLS.FromPostCode].Value = oneWayBookingList[i].FromPostCode.ToStr();

                    grdBookings.Rows[i].Cells[COLS.ToDoorNo].Value = oneWayBookingList[i].ToDoorNo.ToStr();
                    grdBookings.Rows[i].Cells[COLS.ToStreet].Value = oneWayBookingList[i].ToStreet.ToStr();
                    grdBookings.Rows[i].Cells[COLS.ToPostCode].Value = oneWayBookingList[i].ToPostCode.ToStr();

                    grdBookings.Rows[i].Cells[COLS.Account].Value = oneWayBookingList[i].CompanyId;

                    grdBookings.Rows[i].Cells[COLS.JourneyTypeId].Value = oneWayBookingList[i].JourneyTypeId.ToInt();
                    if (oneWayBookingList[i].ReturnPickupDateTime != null)
                    {

                        grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value = string.Format("{0:dd/MM/yyyy HH:mm}", oneWayBookingList[i].ReturnPickupDateTime);
                        grdBookings.Rows[i].Cells[COLS.ReturnFare].Value = oneWayBookingList[i].ReturnFareRate.ToDecimal();
                        grdBookings.Rows[i].Cells[COLS.ReturnFare].ReadOnly = false;
                        grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].ReadOnly = false;
                    }
                    else
                    {
                        grdBookings.Rows[i].Cells[COLS.ReturnFare].ReadOnly = true;
                    }



                    grdBookings.Rows[i].Cells[COLS.MASTERBOOKING].Value = oneWayBookingList[i].AdvanceBookingId;
                    grdBookings.Rows[i].Cells[COLS.StatusId].Value = oneWayBookingList[i].BookingStatusId;


                    grdBookings.Rows[i].Cells[COLS.EXCLUDED].Value = "";
                    grdBookings.Rows[i].Cells[COLS.CHANGED].Value = "";


                    grdBookings.Rows[i].Cells[COLS.ViaPoint].Value = ViaPoint;



                    //if (oneWayBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE
                    //    || oneWayBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                    //    || oneWayBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.POB
                    //    || oneWayBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.STC
                    //    || oneWayBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                    //    )
                    //{

                    //    grdBookings.Rows[i].IsVisible = false;
                    //    showDispatchedBookings = true;
                    //}


                    if (oneWayBookingList[i].DriverId != null)
                        grdBookings.Rows[i].Cells[COLS.DriverNo].Value = oneWayBookingList[i].Fleet_Driver.DefaultIfEmpty().DriverNo.ToStr();




                }

                grdBookings.EndUpdate();
                grdPickupDates.EndUpdate();

                //   chkShowOneWayDispatchedBookings.Visible = showDispatchedBookings;

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void DisplayReturnBooking()
        {

            if (returnBookingList.Count == 0)
                return;

            try
            {
                bool ShowReturnVia = false;
                grdReturnBookings.Rows.Clear();
                grdReturnPickupDates.Rows.Clear();
                grdReturnBookings.RowCount = returnBookingList.Count;
                grdReturnPickupDates.RowCount = grdReturnBookings.RowCount;

                //   bool showDispatchedBookings = false;

                int rowCnt = returnBookingList.Count;

                grdReturnBookings.BeginUpdate();
                grdReturnPickupDates.BeginUpdate();


                Booking objFirstBooking = returnBookingList.FirstOrDefault();

                if(objFirstBooking.BookingStatusId.ToInt()!=Enums.BOOKINGSTATUS.WAITING)
                {
                    objFirstBooking = returnBookingList.FirstOrDefault(c=>c.BookingStatusId==Enums.BOOKINGSTATUS.WAITING);

                    if(objFirstBooking==null)
                    {
                        objFirstBooking = returnBookingList.FirstOrDefault();
                    }
                }

                ddlReturnDriver.Enabled = false;

                int? firstAllocatedDrv = returnBookingList.Where(c => c.BookingStatusId == Enums.BOOKINGSTATUS.WAITING && c.DriverId != null).FirstOrDefault().DefaultIfEmpty().DriverId;


                for (int i = 0; i < rowCnt; i++)
                {

                    try
                    {


                        if (i == 0)
                        {


                            if (objFirstBooking == null)
                                objFirstBooking = returnBookingList[i];


                            dtpReturnPickupTime.Value = objFirstBooking.PickupDateTime;
                            dtpReturnDayWisePickupTime.Value = objFirstBooking.PickupDateTime;

                            dtpReturnStartingAt.Value = objFirstBooking.PickupDateTime;
                            dtpReturnStartingCancel.Value = objFirstBooking.PickupDateTime;
                            ddlReturnFromLocType.SelectedValue = objFirstBooking.FromLocTypeId;
                            ddlReturnToLocType.SelectedValue = objFirstBooking.ToLocTypeId;


                            txtReturnFromFlightDoorNo.Text = objFirstBooking.FromDoorNo.ToStr();
                            txtReturnToFlightDoorNo.Text = objFirstBooking.ToDoorNo.ToStr();


                            ddlReturnFromLocation.SelectedValue = objFirstBooking.FromLocId;
                            ddlReturnToLocation.SelectedValue = objFirstBooking.ToLocId;

                            txtReturnFromStreetComing.Text = objFirstBooking.FromStreet.ToStr();


                            txtReturnFromAddress.TextChanged -= new EventHandler(TextReturnBoxElement_TextChanged);
                            txtReturnFromAddress.Text = objFirstBooking.FromAddress.ToStr();
                            txtReturnFromAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);

                            txtReturnToAddress.TextChanged -= new EventHandler(TextReturnBoxElement_TextChanged);
                            txtReturnToAddress.Text = objFirstBooking.ToAddress.ToStr();
                            txtReturnToAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);




                            txtReturnCustomerName.Text = objFirstBooking.CustomerName.ToStr();
                            txtReturnEmail.Text = objFirstBooking.CustomerEmail.ToStr();
                            txtReturnMobileNo.Text = objFirstBooking.CustomerMobileNo.ToStr();
                            txtReturnTelephoneNo.Text = objFirstBooking.CustomerPhoneNo.ToStr();
                            numReturn_TotalPassengers.Value = objFirstBooking.NoofPassengers.ToDecimal();
                            numReturnTotalLuggages.Value = objFirstBooking.NoofLuggages.ToDecimal();
                            numReturnCustomerPrice.Value = objFirstBooking.CustomerPrice.ToDecimal();
                            numReturnCompanyPrice.Value = objFirstBooking.CompanyPrice.ToDecimal();
                            numReturnPickupFares.Value = objFirstBooking.FareRate.ToDecimal();

                            ddlReturnPaymentType.SelectedValue = objFirstBooking.PaymentTypeId.ToInt();
                            ddlReturnDriver.SelectedValue = firstAllocatedDrv;

                            chkReturnCancelBooking.Checked = false;
                            pnlCancelReturnBooking.Visible = false;


                            //if (returnBookingList[i].CompanyId != null)
                            //{

                            ddlReturnAccount.SelectedValue = objFirstBooking.CompanyId;
                            txtReturnOrderNo.Text = objFirstBooking.OrderNo.ToStr();
                            //}
                            //else if (returnBookingList[returnBookingList.Count - 1].CompanyId != null)
                            //{
                            //    ddlReturnAccount.SelectedValue = returnBookingList[returnBookingList.Count - 1].CompanyId;
                            //    txtReturnOrderNo.Text = returnBookingList[returnBookingList.Count - 1].OrderNo.ToStr();

                            //}

                            ddlReturnVehicle.SelectedValue = objFirstBooking.VehicleTypeId.ToInt();

                            //   txtReturnOrderNo.Text = returnBookingList[i].OrderNo.ToStr();

                            txtReturnSpecialReq.Text = objFirstBooking.SpecialRequirements.ToStr();
                            numReturnCustomerPrice.Value = objFirstBooking.CustomerPrice.ToDecimal();
                            numReturnCompanyPrice.Value = objFirstBooking.WaitingMins.ToDecimal();
                            numReturnPickupFares.Value = objFirstBooking.ReturnFareRate.ToDecimal();



                            if (ShowReturnVia == false)
                            {


                                DisplayReturnBooking_ViaLocations(objFirstBooking.Booking_ViaLocations);
                                ShowReturnVia = true;
                            }
                        }


                        else if (i == (rowCnt - 1))
                        {

                            dtpReturnEndingAt.Value = returnBookingList[i].PickupDateTime;

                            dtpReturnEndingCancel.Value = returnBookingList[i].PickupDateTime;
                        }




                        grdReturnPickupDates.Rows[i].Cells[COLS_PICKUPS.ID].Value = returnBookingList[i].Id;
                        grdReturnBookings.Rows[i].Cells[COLS.BookingStatus].Value = returnBookingList[i].BookingStatusId;

                        if (returnBookingList[i].PickupDateTime != null)
                        {
                            grdReturnPickupDates.Rows[i].Cells[COLS_PICKUPS.DAY].Value = string.Format("{0:dddd}", returnBookingList[i].PickupDateTime);
                            grdReturnPickupDates.Rows[i].Cells[COLS_PICKUPS.PickupDate].Value = returnBookingList[i].PickupDateTime;

                            grdReturnBookings.Rows[i].Cells[COLS.ID].Value = returnBookingList[i].Id;


                            grdReturnBookings.Rows[i].Cells[COLS.DAY].Value = string.Format("{0:dddd}", returnBookingList[i].PickupDateTime);
                            grdReturnBookings.Rows[i].Cells[COLS.PickupDate].Value = returnBookingList[i].PickupDateTime.ToDateTime();//string.Format("{0:dd/MM/yyyy HH:mm}", bookinglist[i].PickupDateTime.ToDateTime());
                            grdReturnBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value = returnBookingList[i].ReturnPickupDateTime.ToDateTimeorNull();
                            //grdBookings.Rows[i].Cells[COLS.PickupDate].Value = bookinglist[i].PickupDateTime.ToDateTime() != null ? "" + bookinglist[i].PickupDateTime.ToDateTime() : "";
                        }



                        if (ddlReturnFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlReturnFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                        {
                            grdReturnBookings.Rows[i].Cells[COLS.FromAddress].Value = returnBookingList[i].FromAddress.ToStr();
                        }
                        else if (ddlReturnFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        {
                            grdReturnBookings.Rows[i].Cells[COLS.FromAddress].Value = returnBookingList[i].FromPostCode.ToStr();
                        }
                        else
                        {
                            grdReturnBookings.Rows[i].Cells[COLS.FromAddress].Value = returnBookingList[i].Gen_Location1.DefaultIfEmpty().LocationName.ToStr().ToUpper();
                            //NC
                            if (returnBookingList[i].Gen_Location1 == null)
                            {
                                grdReturnBookings.Rows[i].Cells[COLS.FromAddress].Value = returnBookingList[i].FromAddress.ToStr();
                            }
                        }

                        if (returnBookingList[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || returnBookingList[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                        {
                            grdReturnBookings.Rows[i].Cells[COLS.ToAddress].Value = returnBookingList[i].ToAddress.ToStr();
                        }
                        else if (returnBookingList[i].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        {
                            grdReturnBookings.Rows[i].Cells[COLS.ToAddress].Value = returnBookingList[i].ToPostCode.ToStr();
                        }
                        else
                        {
                            grdReturnBookings.Rows[i].Cells[COLS.ToAddress].Value = returnBookingList[i].Gen_Location2.DefaultIfEmpty().LocationName.ToStr().ToUpper();
                            //NC
                            if (returnBookingList[i].Gen_Location2 == null)
                            {
                                grdReturnBookings.Rows[i].Cells[COLS.ToAddress].Value = returnBookingList[i].ToAddress.ToStr();
                            }
                        }







                        grdReturnBookings.Rows[i].Cells[COLS.MasterJobId].Value = returnBookingList[i].MasterJobId;


                        grdReturnBookings.Rows[i].Cells[COLS.Fare].Value = returnBookingList[i].FareRate.ToDecimal();

                        grdReturnBookings.Rows[i].Cells[COLS.FromLocTypeId].Value = returnBookingList[i].FromLocTypeId;
                        grdReturnBookings.Rows[i].Cells[COLS.FromLocId].Value = returnBookingList[i].FromLocId;

                        grdReturnBookings.Rows[i].Cells[COLS.ToLocTypeId].Value = returnBookingList[i].ToLocTypeId;
                        grdReturnBookings.Rows[i].Cells[COLS.ToLocId].Value = returnBookingList[i].ToLocId;

                        grdReturnBookings.Rows[i].Cells[COLS.FromDoorNo].Value = returnBookingList[i].FromDoorNo.ToStr();
                        grdReturnBookings.Rows[i].Cells[COLS.FromStreet].Value = returnBookingList[i].FromStreet.ToStr();
                        grdReturnBookings.Rows[i].Cells[COLS.FromPostCode].Value = returnBookingList[i].FromPostCode.ToStr();

                        grdReturnBookings.Rows[i].Cells[COLS.ToDoorNo].Value = returnBookingList[i].ToDoorNo.ToStr();
                        grdReturnBookings.Rows[i].Cells[COLS.ToStreet].Value = returnBookingList[i].ToStreet.ToStr();
                        grdReturnBookings.Rows[i].Cells[COLS.ToPostCode].Value = returnBookingList[i].ToPostCode.ToStr();

                        grdReturnBookings.Rows[i].Cells[COLS.Account].Value = returnBookingList[i].CompanyId;

                        grdReturnBookings.Rows[i].Cells[COLS.JourneyTypeId].Value = returnBookingList[i].JourneyTypeId.ToInt();
                        if (returnBookingList[i].ReturnPickupDateTime != null)
                        {

                            grdReturnBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value = string.Format("{0:dd/MM/yyyy HH:mm}", returnBookingList[i].ReturnPickupDateTime);
                            grdReturnBookings.Rows[i].Cells[COLS.ReturnFare].Value = returnBookingList[i].ReturnFareRate.ToDecimal();
                            grdReturnBookings.Rows[i].Cells[COLS.ReturnFare].ReadOnly = false;
                            grdReturnBookings.Rows[i].Cells[COLS.ReturnPickupDate].ReadOnly = false;
                        }
                        else
                        {
                            grdReturnBookings.Rows[i].Cells[COLS.ReturnFare].ReadOnly = true;
                        }



                        grdReturnBookings.Rows[i].Cells[COLS.MASTERBOOKING].Value = returnBookingList[i].AdvanceBookingId;
                        grdReturnBookings.Rows[i].Cells[COLS.StatusId].Value = returnBookingList[i].BookingStatusId;


                        grdReturnBookings.Rows[i].Cells[COLS.EXCLUDED].Value = "";
                        grdReturnBookings.Rows[i].Cells[COLS.CHANGED].Value = "";


                        grdReturnBookings.Rows[i].Cells[COLS.ViaPoint].Value = ViaPoint;



                        //if (returnBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE
                        //            || returnBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                        //            || returnBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.POB
                        //            || returnBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.STC
                        //            || returnBookingList[i].BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                        //            )
                        //{

                        //    grdReturnBookings.Rows[i].IsVisible = false;
                        //    showDispatchedBookings = true;
                        //}



                        if (returnBookingList[i].DriverId != null)
                            grdReturnBookings.Rows[i].Cells[COLS.DriverNo].Value = returnBookingList[i].Fleet_Driver.DefaultIfEmpty().DriverNo.ToStr();

                    }
                    catch (Exception ex)
                    {


                    }
                }

                grdReturnBookings.EndUpdate();
                grdReturnPickupDates.EndUpdate();


                //  chkShowReturnDispatchedBookings.Visible = showDispatchedBookings;

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
            chkReturnStartingAt.Checked = true;
            chkReturnEndingAt.Checked = true;

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


            //if (e.CellElement is GridDataCellElement)
            //{


            //    if (e.Row.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
            //        || e.Row.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ARRIVED
            //        || e.Row.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.POB
            //        || e.Row.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.STC
            //        || e.Row.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.FOJ
            //        || e.Row.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.PENDING
            //        || e.Row.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED)
            //    {
            //        e.CellElement.RowElement.NumberOfColors = 1;
            //        e.CellElement.RowElement.BackColor = Color.Gainsboro;
            //        e.CellElement.RowElement.Enabled = false;
            //        e.CellElement.RowElement.DrawFill = true;
            //    }
            //    else
            //    {

            //        e.CellElement.RowElement.DrawFill = false;


            //    }


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
            //  }
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
            col.Width = 75;
            col.ReadOnly = true;
            grdBookings.Columns.Add(col);


            GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();
            dtcol.HeaderText = "Pickup Date Time";
            dtcol.ReadOnly = false;
            dtcol.Width = 120;
            dtcol.CustomFormat = "dd/MM/yyyy HH:mm";
            dtcol.FormatString = "{0:dd/MM/yyyy HH:mm}";
            //    col.FormatString = "dd/MM/yyyy HH:mm";
            dtcol.Name = COLS.PickupDate;
            grdBookings.Columns.Add(dtcol);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Return Pickup Date Time";
            col.Width = 150;
            //  col.CustomFormat = "dd/MM/yyyy HH:mm";
            //  col.FormatString = "{0:dd/MM/yyyy HH:mm}";
            // col.FormatString = "dd/MM/yyyy HH:mm";
            col.ReadOnly = true;
            col.IsVisible = false;
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
            col.IsVisible = false;
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
            col.Width = 275;
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
            col.Width = 230;
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

            col = new GridViewTextBoxColumn();
            col.Name = COLS.BookingStatus;
            col.IsVisible = false;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Drv";
            col.Name = COLS.DriverNo;
            col.IsVisible = false;
            col.Width = 50;
            grdBookings.Columns.Add(col);



            GridViewCommandColumn cmdCol = new GridViewCommandColumn();
            cmdCol.Width = 50;
            cmdCol.Name = "Update";
            cmdCol.ImageLayout =
            cmdCol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdCol.DefaultText = "Update";
            cmdCol.UseDefaultText = true;
            cmdCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdBookings.Columns.Add(cmdCol);
            cmdCol = new GridViewCommandColumn();
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

            ConditionalFormattingObject objCancel = new ConditionalFormattingObject();
            objCancel.ApplyToRow = true;
            objCancel.RowBackColor = Color.Red;
            objCancel.ConditionType = ConditionTypes.Equal;
            objCancel.TValue1 = "2";
            objCancel.TValue2 = "2";
            grdBookings.Columns[COLS.CHANGED].ConditionalFormattingObjectList.Add(objCancel);



            //ConditionalFormattingObject objcompletedJobs1 = new ConditionalFormattingObject();

            //objcompletedJobs1.ApplyToRow = true;
            //objcompletedJobs1.RowBackColor = Color.Gainsboro;
            //objcompletedJobs1.ConditionType = ConditionTypes.Between;
            //objcompletedJobs1.TValue1 = "2";
            //objcompletedJobs1.TValue2 = "10";

            //grdBookings.Columns[COLS.StatusId].ConditionalFormattingObjectList.Add(objcompletedJobs1);


            //ConditionalFormattingObject objcompletedJobs2 = new ConditionalFormattingObject();

            //objExcludedJobs.ApplyToRow = true;
            //objExcludedJobs.RowBackColor = Color.Gainsboro;
            //objExcludedJobs.ConditionType = ConditionTypes.Between;
            //objExcludedJobs.TValue1 = "2";
            //objExcludedJobs.TValue2 = "10";

            //grdBookings.Columns[COLS.StatusId].ConditionalFormattingObjectList.Add(objcompletedJobs1);



        }



        private void FormatReturnBookingGrid()
        {
            grdReturnBookings.AllowAddNewRow = false;
            grdReturnBookings.ShowGroupPanel = false;
            grdReturnBookings.ShowRowHeaderColumn = false;
            //grdBookings.AllowEditRow = false;
            grdReturnBookings.EnableHotTracking = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.DAY;
            col.Name = COLS.DAY;
            col.Width = 120;
            col.ReadOnly = true;
            grdReturnBookings.Columns.Add(col);


            GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();
            dtcol.HeaderText = "Pickup Date Time";
            dtcol.ReadOnly = false;
            dtcol.Width = 160;
            dtcol.CustomFormat = "dd/MM/yyyy HH:mm";
            dtcol.FormatString = "{0:dd/MM/yyyy HH:mm}";
            //    col.FormatString = "dd/MM/yyyy HH:mm";
            dtcol.Name = COLS.PickupDate;
            grdReturnBookings.Columns.Add(dtcol);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Return Pickup Date Time";
            col.Width = 150;
            //  dtcol.CustomFormat = "dd/MM/yyyy HH:mm";
            //  dtcol.FormatString = "{0:dd/MM/yyyy HH:mm}";
            // col.FormatString = "dd/MM/yyyy HH:mm";
            col.IsVisible = false;
            col.Name = COLS.ReturnPickupDate;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ID;
            grdReturnBookings.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.MasterJobId;
            grdReturnBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.EXCLUDED;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.CHANGED;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromLocTypeId;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromLocId;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Master";
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromDoorNo;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromStreet;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromPostCode;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToDoorNo;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToStreet;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToPostCode;
            grdReturnBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.JourneyTypeId;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToLocTypeId;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToLocId;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.FromAddress;
            col.Name = COLS.FromAddress;
            col.Width = 275;
            col.ReadOnly = true;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Via";
            col.Name = COLS.ViaPoint;
            col.Width = 60;

            col.ReadOnly = true;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.ToAddress;
            col.Name = COLS.ToAddress;
            col.ReadOnly = true;
            col.Width = 230;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.Account;
            col.Name = COLS.Account;
            col.IsVisible = false;
            grdReturnBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "A/C";
            col.Name = COLS.AccountName;
            col.IsVisible = false;
            col.Width = 70;
            grdReturnBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.Fare;
            col.Name = COLS.Fare;
            col.Width = 80;
            col.ReadOnly = false;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Ret. Fare";
            col.Name = COLS.ReturnFare;
            col.IsVisible = false;
            col.Width = 75;
            grdReturnBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.BookingStatus;
            col.IsVisible = false;
            grdReturnBookings.Columns.Add(col);




            col = new GridViewTextBoxColumn();
            col.HeaderText = "Drv";
            col.Name = COLS.DriverNo;
            col.IsVisible = false;
            col.Width = 50;
            grdReturnBookings.Columns.Add(col);



            GridViewCommandColumn cmdCol = new GridViewCommandColumn();
            cmdCol.Width = 80;
            cmdCol.Name = "Update";
            cmdCol.ImageLayout =
            cmdCol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdCol.DefaultText = "Update";
            cmdCol.UseDefaultText = true;
            cmdCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdReturnBookings.Columns.Add(cmdCol);

            cmdCol = new GridViewCommandColumn();
            cmdCol.Width = 80;
            grdReturnBookings.CommandCellClick += new CommandCellClickEventHandler(grdReturnBookings_CommandCellClick);
            cmdCol.Name = "View";
            cmdCol.UseDefaultText = true;
            cmdCol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdCol.DefaultText = "View";
            cmdCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdReturnBookings.Columns.Add(cmdCol);



            cmdCol = new GridViewCommandColumn();
            col.BestFit();

            cmdCol.Name = "btnDelete";
            cmdCol.UseDefaultText = true;
            cmdCol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdCol.DefaultText = "Delete";
            cmdCol.Width = 80;
            cmdCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdReturnBookings.Columns.Add(cmdCol);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.StatusId;
            grdReturnBookings.Columns.Add(col);



            ConditionalFormattingObject objExcludedJobs = new ConditionalFormattingObject();

            objExcludedJobs.ApplyToRow = true;
            objExcludedJobs.RowBackColor = Color.LightPink;
            objExcludedJobs.ConditionType = ConditionTypes.Equal;
            objExcludedJobs.TValue1 = "1";
            objExcludedJobs.TValue2 = "1";

            grdReturnBookings.Columns[COLS.EXCLUDED].ConditionalFormattingObjectList.Add(objExcludedJobs);



            ConditionalFormattingObject objChangedJobs = new ConditionalFormattingObject();

            objChangedJobs.ApplyToRow = true;
            objChangedJobs.RowBackColor = Color.YellowGreen;
            objChangedJobs.ConditionType = ConditionTypes.Equal;
            objChangedJobs.TValue1 = "1";
            objChangedJobs.TValue2 = "1";

            grdReturnBookings.Columns[COLS.CHANGED].ConditionalFormattingObjectList.Add(objChangedJobs);

            ConditionalFormattingObject objCancel = new ConditionalFormattingObject();
            objCancel.ApplyToRow = true;
            objCancel.RowBackColor = Color.Red;
            objCancel.ConditionType = ConditionTypes.Equal;
            objCancel.TValue1 = "2";
            objCancel.TValue2 = "2";
            grdReturnBookings.Columns[COLS.CHANGED].ConditionalFormattingObjectList.Add(objCancel);
        }

        void grdReturnBookings_CommandCellClick(object sender, EventArgs e)
        {
            try
            {


                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == "Update")
                {
                    UpdateReturnBooking();
                }
                else if (gridCell.ColumnInfo.Name.ToStr().ToLower() == "view")
                {
                    ViewReturnDetailForm();
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

                                GridViewRowInfo pickupRow = grdReturnPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS_PICKUPS.ID].Value.ToLong() == jobId);
                                if (pickupRow != null)
                                    pickupRow.Delete();

                                GridViewRowInfo excpickupRow = grdExcludeReturnPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS_PICKUPS.ID].Value.ToLong() == jobId);
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


        public void UpdateBooking()
        {
            try
            {
                objMaster = new BookingBO();
                long Id = grdBookings.CurrentRow.Cells[COLS.ID].Value.ToLong();
                objMaster.GetByPrimaryKey(Id);

                objMaster.Edit();
                objMaster.Current.PickupDateTime = grdBookings.CurrentRow.Cells[COLS.PickupDate].Value.ToDateTimeorNull();





                objMaster.Current.FareRate = grdBookings.CurrentRow.Cells[COLS.Fare].Value.ToDecimal();

                objMaster.Current.PickupDateTime = grdBookings.CurrentRow.Cells[COLS.PickupDate].Value.ToDateTime();

                //if (objBooking.BookingReturns.Count > 0)
                //{
                //    objBooking.ReturnFareRate = grdBookings.CurrentRow.Cells[COLS.ReturnFare].Value.ToDecimal();
                //    objBooking.ReturnPickupDateTime = grdBookings.CurrentRow.Cells[COLS.ReturnPickupDate].Value.ToDateTimeorNull();
                //}

                objMaster.DisableUpdateReturnJob = true;
                objMaster.CheckCustomerValidation = false;
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

        public void UpdateReturnBooking()
        {
            try
            {

                long Id = grdReturnBookings.CurrentRow.Cells[COLS.ID].Value.ToLong();
                //booking.GetByPrimaryKey(grdBookings.CurrentRow.Cells[COLS.ID].Value.ToLong());
                objMaster = new BookingBO();
                objMaster.GetByPrimaryKey(Id);
                objMaster.Edit();

                objMaster.Current.PickupDateTime = grdReturnBookings.CurrentRow.Cells[COLS.PickupDate].Value.ToDateTimeorNull();
                objMaster.Current.FareRate = grdReturnBookings.CurrentRow.Cells[COLS.Fare].Value.ToDecimal();

                objMaster.CheckCustomerValidation = false;
                objMaster.DisableUpdateReturnJob = false;
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
        private void ViewReturnDetailForm()
        {

            if (grdReturnBookings.CurrentRow != null && grdReturnBookings.CurrentRow is GridViewDataRowInfo)
            {
                ShowBookingForm(grdReturnBookings.CurrentRow.Cells["Id"].Value.ToInt());
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

        private void CreateBooking(int days, bool sameAsMasterBooking, bool sameOrigin, bool sameDestination)
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
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    bool IsSuccess = SaveOneWayBookings(db);
                    IsSuccess = SaveReturnBookings(db);

                 //   db.SubmitChanges();

                    Saved = IsSuccess;
                    IsApplyChanges = false;
                    //  HasError = IsSuccess;




                    if (IsSuccess)
                    {

                        try
                        {

                            if (chkPickup.Checked || chkDestination.Checked)
                            {

                                AdvanceBooking objAdv = db.AdvanceBookings.FirstOrDefault(c => c.Id == ObjAdvanceBooking.Id);

                                if (objAdv != null)
                                {
                                    objAdv.CustomerName = txtCustomerName.Text;
                                    if(txtFromAddress.Text.ToStr().Trim().Length>0)
                                        objAdv.FromAddress = txtFromAddress.Text;

                                    if (txtToAddress.Text.ToStr().Trim().Length > 0)
                                        objAdv.ToAddress = txtToAddress.Text;


                                    db.SubmitChanges();



                                }
                            }
                        }
                        catch
                        {


                        }


                     
                    }






                    return IsSuccess;

                }
            }
            catch (Exception exe)
            {
                // HasError = true;
                ErrorMessage = exe.Message;
                return false;
            }

        }


        private string LastPickupPostCode = string.Empty;
        private int? lastPickupZoneId = null;
        private decimal lastDeadMileage;
        private decimal lastExtraMile;

        private bool SaveOneWayBookings(TaxiDataContext db)
        {
            bool IsSuccess = true;



            List<long> ListofBookingIDs = grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() != "1"
                                            && c.Cells[COLS.CHANGED].Value.ToStr().Trim() != "" && c.IsVisible == true).Select(c => c.Cells[COLS.ID].Value.ToLong()).ToList<long>();

            var listofTotalBookings = db.Bookings.Where(c => ListofBookingIDs.Contains(c.Id)).ToList();

            //var listofTotalBookings = (from a in db.Bookings
            //                          join b in ListofBookingIDs on a.Id equals b
            //                          select a).ToList();


            int FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
            int ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
            int cnt = grdBookings.Rows.Count;
            try
            {
                int savedCounter = 0;
                for (int i = 0; i < cnt; i++)
                {
                    Booking objBooking = null;

                    if (worker == null)
                    {
                        break;
                    }

                    if (grdBookings.Rows[i].Cells[COLS.EXCLUDED].Value.ToStr() == "1")
                    {
                        /*
                        objMaster = new BookingBO();

                        objMaster.GetByPrimaryKey(grdBookings.Rows[i].Cells[COLS.ID].Value.ToLong());

                        if (objBooking != null)
                        {
                            objMaster.Delete(objBooking);
                        }
                        */
                        if (objBooking == null)
                        {
                            long bookingId = 0;
                            if (long.TryParse(grdBookings.Rows[i].Cells[COLS.ID].Value.ToStr(), out bookingId))
                            {
                                objBooking = db.Bookings.FirstOrDefault(m => m.Id ==bookingId);
                            }
                        }
                        db.Bookings.DeleteOnSubmit(objBooking);
                    }
                    else
                    {
                        long bookingId = 0;

                        if (grdBookings.Rows[i].Cells[COLS.CHANGED].Value.ToStr().Trim() == "" || grdBookings.Rows[i].IsVisible == false)
                            continue;
                        else if (long.TryParse(grdBookings.Rows[i].Cells[COLS.ID].Value.ToStr(), out bookingId))
                        {
                            if (objBooking == null)
                            {
                                objBooking = listofTotalBookings.FirstOrDefault(m => m.Id == bookingId);
                                // objBooking = db.Bookings.FirstOrDefault(m => m.Id == bookingId);
                            }
                          
                            BookingNo = objBooking.BookingNo;

                            objBooking.PickupDateTime = grdBookings.Rows[i].Cells[COLS.PickupDate].Value.ToDateTimeorNull();
                            if (grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value != null)
                            {
                                objBooking.ReturnPickupDateTime = grdBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value.ToDateTimeorNull();
                            }

                            objBooking.CustomerName = cCustomerName;
                            objBooking.CustomerEmail = CustomerEmail;
                            objBooking.CustomerMobileNo = CustomerMobileNo;
                            objBooking.CustomerPhoneNo = CustomerPhoneNo;
                            objBooking.NoofLuggages = NoofLuggages;
                            objBooking.NoofPassengers = NoofPassengers;
                            objBooking.CompanyId = CompanyId;
                            objBooking.PaymentTypeId = PaymentTypeId;
                            objBooking.IsCompanyWise = objBooking.CompanyId != null;

                            objBooking.VehicleTypeId = VehicleTypeId;// ddlVehicle.SelectedValue.ToIntorNull();


                            if (cAllocateOnewayDrv &&
                                (objBooking.DriverId == null || objBooking.BookingStatusId == Enums.BOOKINGSTATUS.WAITING))
                            {
                                objBooking.DriverId = DriverId;
                                objBooking.IsConfirmedDriver = true;

                            }

                            objBooking.SpecialRequirements = SpecialRequirements;



                            if (grdBookings.Rows[i].Cells[COLS.MasterJobId].Value == null)
                            {

                                if (cPickupFares)
                                {
                                    objBooking.FareRate = grdBookings.Rows[i].Cells[COLS.Fare].Value.ToDecimal();

                                }

                                if (cCustomerPrice)
                                {
                                    objBooking.CustomerPrice = CustomerPrice;
                                }
                                if (cCompanyPrice)
                                {
                                    objBooking.CompanyPrice = CompanyPrice;
                                }

                                objBooking.SpecialRequirements = SpecialRequirements;
                            }

                            objBooking.OrderNo = txtOrderNo.Text.Trim();


                            //      objBooking.BookingStatusId = grdBookings.Rows[i].Cells[COLS.BookingStatus].Value.ToIntorNull();

                            if (cPickup)
                            {

                                FromLocTypeId = grdBookings.Rows[i].Cells[COLS.FromLocTypeId].Value.ToInt();

                                objBooking.FromLocTypeId = FromLocTypeId;

                                objBooking.FromLocId = FromLocId;// ddlFromLocation.SelectedValue.ToIntorNull();


                                if (FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromLocTypeId == Enums.LOCATION_TYPES.BASE)
                                    objBooking.FromAddress = grdBookings.Rows[i].Cells[COLS.FromAddress].Value.ToStr();
                                else if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                                    objBooking.FromAddress = grdBookings.Rows[i].Cells[COLS.FromPostCode].Value.ToStr();
                                else
                                {
                                    objBooking.FromAddress = grdBookings.Rows[i].Cells[COLS.FromAddress].Value.ToStr();

                                }


                                objBooking.FromDoorNo = grdBookings.Rows[i].Cells[COLS.FromDoorNo].Value.ToStr();
                                objBooking.FromStreet = grdBookings.Rows[i].Cells[COLS.FromStreet].Value.ToStr();
                                objBooking.FromPostCode = General.GetPostCodeMatch(objBooking.FromAddress.ToStr().Trim());


                                if (objBooking.FromPostCode.ToStr().Trim() != string.Empty)
                                {
                                    if ((LastPickupPostCode == string.Empty || LastPickupPostCode != objBooking.FromPostCode.ToStr().Trim()))
                                    {

                                        objBooking.ZoneId = General.GetZoneId(objBooking.FromPostCode);
                                        LastPickupPostCode = objBooking.FromPostCode.ToStr().Trim();
                                        lastPickupZoneId = objBooking.ZoneId;


                                        //if (AppVars.objPolicyConfiguration.AutoBookingDueAlert.ToBool())
                                        //{

                                        //    //if(!string.IsNullOrEmpty(objBooking.FromPostCode.ToStr().ToUpper()  ObjBooking.FromPostCode.ToStr().ToUpper()==objBooking.FromPostCode.ToStr().ToUpper()
                                        //    decimal mile = General.CalculateDistanceFromBaseFull(objBooking.FromAddress.ToStr());
                                        //    objBooking.DeadMileage = mile;

                                        //    lastDeadMileage = objBooking.DeadMileage.ToDecimal();

                                        //    if (mile > 0 && mile < 1)
                                        //        mile = 1;

                                        //    else
                                        //        mile = Math.Round(mile, 0);

                                        //    objBooking.ExtraMile = mile;

                                        //    lastExtraMile = mile;

                                        //}
                                    }
                                    else
                                    {

                                        objBooking.ZoneId = lastPickupZoneId;

                                     //   objBooking.ExtraMile = lastExtraMile;
                                       // objBooking.DeadMileage = lastDeadMileage;
                                    }
                                }



                            }


                            if (cDestination)
                            {
                                ToLocTypeId = grdBookings.Rows[i].Cells[COLS.ToLocTypeId].Value.ToInt();

                                objBooking.ToLocTypeId = ToLocTypeId;
                                objBooking.ToLocId = ToLocId; //ddlToLocation.SelectedValue.ToIntorNull();

                                if (ToLocTypeId == Enums.LOCATION_TYPES.ADDRESS || ToLocTypeId == Enums.LOCATION_TYPES.BASE)
                                    objBooking.ToAddress = grdBookings.Rows[i].Cells[COLS.ToAddress].Value.ToStr();

                                else if (ToLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                                    objBooking.ToAddress = grdBookings.Rows[i].Cells[COLS.ToAddress].Value.ToStr();
                                else
                                {
                                    objBooking.ToAddress = grdBookings.Rows[i].Cells[COLS.ToAddress].Value.ToStr();

                                }



                                objBooking.ToDoorNo = grdBookings.Rows[i].Cells[COLS.ToDoorNo].Value.ToStr();
                                objBooking.ToStreet = grdBookings.Rows[i].Cells[COLS.ToStreet].Value.ToStr();
                                objBooking.ToPostCode = General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim());
                            }


                            //objBooking.PickupDateTime = grdBookings.Rows[i].Cells[COLS.PickupDate].Value.ToDateTime();



                            if (grdVia != null && updateVia == true)
                            {



                                string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                                IList<Booking_ViaLocation> savedList = objBooking.Booking_ViaLocations;
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



                                //  Via 1 : PENTAX H SOUTH HILL AVENUE HARROW HA2 0DU , Via 2 : HEATHROW TERMINAL 4, TW6 2GA , Via 3 : GOURMET BURGER 333 PUTNEY BRIDGE RD, SW15 2PG , Via 4 : VICTORIA ROAD RUISLIP HA4 0AA
                                if (listofDetail.Count > 0)
                                {
                                    int cnter = 0;
                                    objBooking.ViaString = string.Join(",", listofDetail.Select(args => "Via " + (++cnter) + " : " + args.ViaLocValue.Replace(",", " ")).ToArray<string>());


                                }
                                else
                                    objBooking.ViaString = "";




                            }


                            //objMaster.CheckCustomerValidation = false;

                            ////    objMaster.DisableFromPostCodeDeadMileage = !(chkPickup.Enabled == false);

                            //objMaster.DisableUpdateReturnJob = true;


                            //objMaster.Save();

                            WithInSave(objBooking);



                            savedCounter++;

                            if (savedCounter % 50==0 || savedCounter>=ListofBookingIDs.Count)
                            {
                                UpdateWorker(savedCounter,ListofBookingIDs.Count,"One Way Bookings ");
                                db.SubmitChanges();
                              //  savedCounter = 0;
                            }

                          



                            // MasterBookingList.Add(objBooking);
                        }


                    }



                }
                // HasError = false;
            }
            catch (Exception ex)
            {
                //if (objMaster.Errors.Count > 0)
                //    ErrorMessage = objMaster.ShowErrors();
                //// ENUtils.ShowMessage(objMaster.ShowErrors());
                //else
                //{
                // HasError = true;
                ErrorMessage = ex.Message;
                //ENUtils.ShowMessage(ex.Message);
                //}
                IsSuccess = false;
            }

            return IsSuccess;

        }

        private void UpdateWorker(int totalSaved,int totalBookings,string type)
        {
            cr = new CurrentRow();

            //int OneWay = grdBookings.Rows.Where(c => ((c.Cells[COLS.CHANGED].Value.ToStr() == "1"))).Count();

            //int Return = grdReturnBookings.Rows.Where(c => ((c.Cells[COLS.CHANGED].Value.ToStr() == "1"))).Count();
            cr.Total = (totalBookings);

            if (worker != null)
            {
                //cr.UpdateValue = objBooking.BookingNo;
                cr.UpdateValue = type;
              
                cr.index = totalSaved;
                worker.ReportProgress(cnter, cr);
            }
        }

        int cnter = 0;
        CurrentRow cr = null;
        string BookingNo = string.Empty;

        private bool SaveReturnBookings(TaxiDataContext db)
        {
            bool IsSuccess = true;


            int FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
            int ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
            int cnt = grdReturnBookings.Rows.Count;
            try
            {

                int savedCounter = 0;
                List<long> ListofBookingIDs = grdReturnBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() != "1"
                                           && c.Cells[COLS.CHANGED].Value.ToStr().Trim() != "" && c.IsVisible == true).Select(c => c.Cells[COLS.ID].Value.ToLong()).ToList<long>();

                List<Booking> listofTotalBookings = null;


                for (int i = 0; i < cnt; i++)
                {
                    Booking objBooking = null;

                    if (worker == null)
                    {
                        break;
                    }

                    if (grdReturnBookings.Rows[i].Cells[COLS.EXCLUDED].Value.ToStr() == "1")
                    {
                        /*
                        objMaster = new BookingBO();

                        objMaster.GetByPrimaryKey(grdReturnBookings.Rows[i].Cells[COLS.ID].Value.ToLong());

                        if (objBooking != null)
                        {
                            objMaster.Delete(objBooking);
                        }
                        */

                        if (objBooking == null)
                        {
                            long bookingId = 0;
                            if (long.TryParse(grdReturnBookings.Rows[i].Cells[COLS.ID].Value.ToStr(), out bookingId))
                            {
                                objBooking = db.Bookings.FirstOrDefault(m => m.Id == bookingId);
                            }
                        }
                        db.Bookings.DeleteOnSubmit(objBooking);
                     
                    }
                    else
                    {
                        long bookingId = 0;

                        if (grdReturnBookings.Rows[i].Cells[COLS.CHANGED].Value.ToStr().Trim() == "" || grdReturnBookings.Rows[i].IsVisible == false)
                            continue;
                        else if (long.TryParse(grdReturnBookings.Rows[i].Cells[COLS.ID].Value.ToStr(), out bookingId))
                        {

                            if (listofTotalBookings == null)
                            {
                                listofTotalBookings = db.Bookings.Where(c => ListofBookingIDs.Contains(c.Id)).ToList();
                            }


                            if (objBooking == null)
                            {
                                objBooking = listofTotalBookings.FirstOrDefault(m => m.Id == bookingId);

                                //  objBooking = db.Bookings.FirstOrDefault(m => m.Id == bookingId);
                            }

                          
                            BookingNo = objBooking.BookingNo;
                            objBooking.PickupDateTime = grdReturnBookings.Rows[i].Cells[COLS.PickupDate].Value.ToDateTimeorNull();
                            //if (grdReturnBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value != null)
                            //{
                            //    objBooking.ReturnPickupDateTime = grdReturnBookings.Rows[i].Cells[COLS.ReturnPickupDate].Value.ToDateTime();
                            //}

                            objBooking.CustomerName = ReturncCustomerName;
                            objBooking.CustomerEmail = ReturnCustomerEmail;
                            objBooking.CustomerMobileNo = ReturnCustomerMobileNo;
                            objBooking.CustomerPhoneNo = ReturnCustomerPhoneNo;
                            objBooking.NoofLuggages = ReturnNoofLuggages;
                            objBooking.NoofPassengers = ReturnNoofPassengers;
                            objBooking.CompanyId = ReturnCompanyId;

                            objBooking.PaymentTypeId = ReturnPaymentTypeId;
                            objBooking.IsCompanyWise = objBooking.CompanyId != null;

                            objBooking.VehicleTypeId = ReturnVehicleTypeId;
                            //objMaster.ReturnConfirmAllocaedDriver = ddlReturnDriver.SelectedValue != null;

                            if (ReturnAllocateDrv && (objBooking.DriverId == null || objBooking.BookingStatusId == Enums.BOOKINGSTATUS.WAITING))
                            {
                                objBooking.DriverId = ReturnDriverId;
                                objBooking.IsConfirmedDriver = true;


                            }
                            objBooking.SpecialRequirements = ReturnSpecialRequirements;

                            if (ReturncCompanyPrice)
                            {
                                objBooking.CompanyPrice = ReturnCompanyPrice;
                            }
                            if (ReturncCustomerPrice)
                            {
                                objBooking.CustomerPrice = ReturnCustomerPrice;
                            }


                            if (ReturncPickupFares)
                            {
                                objBooking.FareRate = grdReturnBookings.Rows[i].Cells[COLS.Fare].Value.ToDecimal();
                            }


                            objBooking.OrderNo = ReturnOrderNo;


                            //       objBooking.BookingStatusId = grdReturnBookings.Rows[i].Cells[COLS.BookingStatus].Value.ToIntorNull();


                            if (ReturncPickup)
                            {

                                FromLocTypeId = grdReturnBookings.Rows[i].Cells[COLS.FromLocTypeId].Value.ToInt();

                                objBooking.FromLocTypeId = FromLocTypeId;

                                objBooking.FromLocId = ReturnFromLocId;//FromLocation.SelectedValue.ToIntorNull();


                                if (FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromLocTypeId == Enums.LOCATION_TYPES.BASE)
                                    objBooking.FromAddress = grdReturnBookings.Rows[i].Cells[COLS.FromAddress].Value.ToStr();
                                else if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                                    objBooking.FromAddress = grdReturnBookings.Rows[i].Cells[COLS.FromPostCode].Value.ToStr();
                                else
                                {
                                    objBooking.FromAddress = grdReturnBookings.Rows[i].Cells[COLS.FromAddress].Value.ToStr();

                                }


                                objBooking.FromDoorNo = grdReturnBookings.Rows[i].Cells[COLS.FromDoorNo].Value.ToStr();
                                objBooking.FromStreet = grdReturnBookings.Rows[i].Cells[COLS.FromStreet].Value.ToStr();
                                objBooking.FromPostCode = General.GetPostCodeMatch(objBooking.FromPostCode.ToStr().Trim());


                                if (objBooking.FromPostCode.ToStr().Trim() != string.Empty)
                                {
                                    if ((LastPickupPostCode == string.Empty || LastPickupPostCode != objBooking.FromPostCode.ToStr().Trim()))
                                    {

                                        objBooking.ZoneId = General.GetZoneId(objBooking.FromPostCode);
                                        LastPickupPostCode = objBooking.FromPostCode.ToStr().Trim();
                                        lastPickupZoneId = objBooking.ZoneId;


                                        //if (AppVars.objPolicyConfiguration.AutoBookingDueAlert.ToBool())
                                        //{

                                        //    decimal mile = General.CalculateDistanceFromBaseFull(objBooking.FromAddress.ToStr());
                                        //    objBooking.DeadMileage = mile;

                                        //    lastDeadMileage = objBooking.DeadMileage.ToDecimal();

                                        //    if (mile > 0 && mile < 1)
                                        //        mile = 1;

                                        //    else
                                        //        mile = Math.Round(mile, 0);

                                        //    objBooking.ExtraMile = mile;

                                        //    lastExtraMile = mile;

                                        //}
                                    }
                                    else
                                    {

                                        objBooking.ZoneId = lastPickupZoneId;

                                       // objBooking.ExtraMile = lastExtraMile;
                                      //  objBooking.DeadMileage = lastDeadMileage;
                                    }
                                }


                            }


                            if (ReturnDestination)
                            {
                                ToLocTypeId = grdReturnBookings.Rows[i].Cells[COLS.ToLocTypeId].Value.ToInt();

                                objBooking.ToLocTypeId = ToLocTypeId;
                                objBooking.ToLocId = ReturnToLocId; //ddlReturnToLocation.SelectedValue.ToIntorNull();

                                if (ToLocTypeId == Enums.LOCATION_TYPES.ADDRESS || ToLocTypeId == Enums.LOCATION_TYPES.BASE)
                                    objBooking.ToAddress = grdReturnBookings.Rows[i].Cells[COLS.ToAddress].Value.ToStr();

                                else if (ToLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                                    objBooking.ToAddress = grdReturnBookings.Rows[i].Cells[COLS.ToAddress].Value.ToStr();
                                else
                                {
                                    objBooking.ToAddress = grdReturnBookings.Rows[i].Cells[COLS.ToAddress].Value.ToStr();

                                }


                                objBooking.ToDoorNo = grdReturnBookings.Rows[i].Cells[COLS.ToDoorNo].Value.ToStr();
                                objBooking.ToStreet = grdReturnBookings.Rows[i].Cells[COLS.ToStreet].Value.ToStr();
                                objBooking.ToPostCode = General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim());
                            }


                            //objBooking.PickupDateTime = grdBookings.Rows[i].Cells[COLS.PickupDate].Value.ToDateTime();



                            if (grdReturnVia != null && updateReturnVia == true)
                            {



                                string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                                IList<Booking_ViaLocation> savedList = objBooking.Booking_ViaLocations;
                                List<Booking_ViaLocation> listofDetail = (from r in grdReturnVia.Rows
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


                                //  Via 1 : PENTAX H SOUTH HILL AVENUE HARROW HA2 0DU , Via 2 : HEATHROW TERMINAL 4, TW6 2GA , Via 3 : GOURMET BURGER 333 PUTNEY BRIDGE RD, SW15 2PG , Via 4 : VICTORIA ROAD RUISLIP HA4 0AA
                                if (listofDetail.Count > 0)
                                {
                                    int cnter = 0;
                                    objBooking.ViaString = string.Join(",", listofDetail.Select(args => "Via " + (++cnter) + " : " + args.ViaLocValue.Replace(",", " ")).ToArray<string>());


                                }
                                else
                                    objBooking.ViaString = "";

                            }


                            //objMaster.CheckCustomerValidation = false;

                            // objMaster.DisableFromPostCodeDeadMileage = !(chkReturnPickup.Enabled == false);

                            //objMaster.DisableUpdateReturnJob = true;

                            WithInSave(objBooking);
                            // objMaster.Save();
                            //  UpdateWorker();

                            savedCounter++;


                           

                            if (savedCounter % 50 == 0 || savedCounter >= ListofBookingIDs.Count)
                            {
                                UpdateWorker(savedCounter, ListofBookingIDs.Count, "Return Bookings ");
                                db.SubmitChanges();
                               
                            }
                            // MasterBookingList.Add(objBooking);
                        }


                    }



                }
                // HasError = false;
            }
            catch (Exception ex)
            {
                //if (objMaster.Errors.Count > 0)
                //    ErrorMessage = objMaster.ShowErrors();
                // ENUtils.ShowMessage(objMaster.ShowErrors());
                //else
                //{
                //HasError = true;
                ErrorMessage = ex.Message;
                //ENUtils.ShowMessage(ex.Message);
                // }
                IsSuccess = false;
            }

            return IsSuccess;

        }

        bool updateVia = false;

        bool updateReturnVia = false;

        string ErrorMessage = string.Empty;
        //bool HasError = false;
        private bool SendAdvanceBookingConfirmationText(string mobileNo)
        {
            if (string.IsNullOrEmpty(mobileNo)) return false;

            bool rtn = true;

            // Advance Booking Confirmation Text
            bool enableAdvBookingText = AppVars.objPolicyConfiguration.EnableAdvanceBookingSMSConfirmation.ToBool();

            if (enableAdvBookingText && listOfPickUpDateTime.Count > 0 && this.ObjBookiing != null && this.ObjBookiing.IsQuotation.ToBool() == false)
            {

                int afterMins = AppVars.objPolicyConfiguration.AdvanceBookingSMSConfirmationMins.ToInt();

                int minDifference = 0;
                bool foundAny = false;
                int dayDiff = 0;
                foreach (var pickupTime in listOfPickUpDateTime)
                {
                    dayDiff = pickupTime.Date.Subtract(DateTime.Now.Date).Days;
                    minDifference = pickupTime.TimeOfDay.Subtract(nowDate.TimeOfDay).Minutes;

                    if (dayDiff > 0 || afterMins > 0 && minDifference >= afterMins)
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



        private bool SaveChanges()
        {

            int VehicleId = ddlVehicle.SelectedValue.ToInt();
            int PaymentTypeId = ddlPaymentType.SelectedValue.ToInt();
            string CustomerName = txtCustomerName.Text.ToStr().Trim();
            string Error = string.Empty;
            if (VehicleId == 0)
            {
                Error = "Required : Vehicle";
            }
            if (PaymentTypeId == 0)
            {
                if (string.IsNullOrEmpty(Error))
                {
                    Error = "Required : Payment Type";
                }
                else
                {
                    Error += Environment.NewLine + "Required : Payment Type";
                }
            }
            if (string.IsNullOrEmpty(CustomerName))
            {
                if (string.IsNullOrEmpty(Error))
                {
                    Error = "Required : Customer Name";
                }
                else
                {
                    Error += Environment.NewLine + "Required : Customer Name";
                }
            }

            if (!string.IsNullOrEmpty(Error))
            {
                ENUtils.ShowMessage(Error);
                return false;
            }

            btnSaveBooking.Enabled = false;
            btnApplyChanges.Enabled = false;
            btnReturnApplyChanges.Enabled = false;

            btnApplyCancelChanges.Enabled = false;
            btnApplyChanges.Enabled = false;
            btnRevertChanges.Enabled = false;
            btnSaveChanges.Enabled = false;


            btnReturnSaveChanges.Enabled = false;
            btnReturnApplyCancelChanges.Enabled = false;
            btnReturnApplyChanges.Enabled = false;
            btnReturnRevertChanges.Enabled = false;


            Destination = chkDestination.Checked;
            cPickup = chkPickup.Checked;
            cCustomerName = txtCustomerName.Text.Trim();
            CustomerEmail = txtEmail.Text.Trim();
            CustomerMobileNo = txtMobileNo.Text.Trim();
            CustomerPhoneNo = txtTelephoneNo.Text.Trim();
            NoofLuggages = numTotalLuggages.Value.ToIntorNull();
            NoofPassengers = num_TotalPassengers.Value.ToIntorNull();
            CompanyId = ddlAccount.SelectedValue.ToIntorNull();
            PaymentTypeId = ddlPaymentType.SelectedValue.ToInt();
            VehicleTypeId = ddlVehicle.SelectedValue.ToIntorNull();
            cAllocateOnewayDrv = chkAllocateOnewayDrv.Checked;
            cChangeAction = chkPickupFares.Checked;
            cDestination = chkDestination.Checked;
            cPickupFares = chkPickupFares.Checked;
            DriverId = ddlDriver.SelectedValue.ToIntorNull();

            cCustomerPrice = chkCustomerPrice.Checked;
            CustomerPrice = numCustomerPrice.Value.ToDecimal();
            cCompanyPrice = chkCompanyPrice.Checked;
            CompanyPrice = numCompanyPrice.Value.ToDecimal();
            SpecialRequirements = txtSpecialReq.Text.Trim();
            OrderNo = txtOrderNo.Text.Trim();
            FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();

            cReturnDestination = chkReturnDestination.Checked;

            ToLocId = ddlToLocation.SelectedValue.ToIntorNull();

            //Return
            ReturnDestination = chkReturnDestination.Checked;
            ReturncPickup = chkReturnPickup.Checked;
            ReturncCustomerName = txtReturnCustomerName.Text.Trim();
            ReturnCustomerEmail = txtReturnEmail.Text.Trim();
            ReturnCustomerMobileNo = txtReturnMobileNo.Text.Trim();
            ReturnCustomerPhoneNo = txtReturnTelephoneNo.Text.Trim();
            ReturnNoofLuggages = numReturnTotalLuggages.Value.ToIntorNull();
            ReturnNoofPassengers = numReturn_TotalPassengers.Value.ToIntorNull();
            ReturnCompanyId = ddlReturnAccount.SelectedValue.ToIntorNull();
            ReturnPaymentTypeId = ddlReturnPaymentType.SelectedValue.ToInt();
            ReturnVehicleTypeId = ddlReturnVehicle.SelectedValue.ToIntorNull();
            ReturnAllocateDrv = chkReturnAllocateDrv.Checked;
            ReturncChangeAction = chkReturnPickupFares.Checked;
            ReturncDestination = chkReturnDestination.Checked;

            ReturnDriverId = ddlReturnDriver.SelectedValue.ToIntorNull();

            ReturncCustomerPrice = chkReturnCustomerPrice.Checked;
            ReturnCustomerPrice = numReturnCustomerPrice.Value.ToDecimal();
            ReturncCompanyPrice = chkReturnCompanyPrice.Checked;
            ReturnCompanyPrice = numReturnCompanyPrice.Value.ToDecimal();
            ReturnSpecialRequirements = txtReturnSpecialReq.Text.Trim();
            ReturnOrderNo = txtReturnOrderNo.Text.Trim();
            ReturnFromLocId = ddlReturnFromLocation.SelectedValue.ToIntorNull();
            ReturncPickupFares = chkReturnPickupFares.Checked;
            ReturncReturnDestination = chkReturnDestination.Checked;
            ReturncAllocateOnewayDrv = chkReturnAllocateDrv.Checked;
            ReturnToLocId = ddlReturnToLocation.SelectedValue.ToIntorNull();



            worker.RunWorkerAsync();
            //  Saved = Save();

            if (Saved)
                IsApplyChanges = false;

            return Saved;
        }

        private void btnSaveBooking_Click(object sender, EventArgs e)
        {

            SaveChanges();
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
            dtpStartingCancel.Value = currentDate;
        }


        private void SetStartingPoint(ref DateTime currentDate, string day)
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
            if (row != null && row is GridViewDataRowInfo)
            {
                GridViewRowInfo excRow = grdExcludePickupDates.Rows.AddNew();

                excRow.Cells[COLS_PICKUPS.ID].Value = row.Cells[COLS_PICKUPS.ID].Value;
                excRow.Cells[COLS_PICKUPS.DAY].Value = row.Cells[COLS_PICKUPS.DAY].Value;
                excRow.Cells[COLS_PICKUPS.PickupDate].Value = row.Cells[COLS_PICKUPS.PickupDate].Value;


                grdPickupDates.Rows.Remove(row);

            }


        }

        private void ExcludeReturnBooking(GridViewRowInfo row)
        {
            if (row != null && row is GridViewDataRowInfo)
            {
                GridViewRowInfo excRow = grdExcludeReturnPickupDates.Rows.AddNew();

                excRow.Cells[COLS_PICKUPS.ID].Value = row.Cells[COLS_PICKUPS.ID].Value;
                excRow.Cells[COLS_PICKUPS.DAY].Value = row.Cells[COLS_PICKUPS.DAY].Value;
                excRow.Cells[COLS_PICKUPS.PickupDate].Value = row.Cells[COLS_PICKUPS.PickupDate].Value;

                // row.Delete();

                grdReturnPickupDates.Rows.Remove(row);

                //grdReturnPickupDates.CurrentRow.Delete();
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
        private void IncludeReturnBooking(GridViewRowInfo row)
        {
            if (row != null && row is GridViewDataRowInfo)
            {
                GridViewRowInfo incRow = grdReturnPickupDates.Rows.AddNew();

                incRow.Cells[COLS_PICKUPS.ID].Value = row.Cells[COLS_PICKUPS.ID].Value;
                incRow.Cells[COLS_PICKUPS.DAY].Value = row.Cells[COLS_PICKUPS.DAY].Value;
                incRow.Cells[COLS_PICKUPS.PickupDate].Value = row.Cells[COLS_PICKUPS.PickupDate].Value;


                grdExcludeReturnPickupDates.Rows.Remove(row);

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
                int VehicleId = ddlVehicle.SelectedValue.ToInt();
                PaymentTypeId = ddlPaymentType.SelectedValue.ToInt();
                string CustomerName = txtCustomerName.Text.ToStr().Trim();
                string Error = string.Empty;
                if (VehicleId == 0)
                {
                    Error = "Required : Vehicle";
                }
                if (PaymentTypeId == 0)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : Payment Type";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : Payment Type";
                    }
                }
                if (string.IsNullOrEmpty(CustomerName))
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : Customer Name";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : Customer Name";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                if (chkDayWise.Checked.ToBool() == false)
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

                    //if (chkReturnPickupTime.Checked && dtpReturnPickupTime.Value == null)
                    //{
                    //    ENUtils.ShowMessage("Required : Please Enter Return Pickup Time to Change");
                    //    return;
                    //}
                    if (chkCancelBooking.Checked && dtpStartingCancel.Value == null)
                    {
                        ENUtils.ShowMessage("Required :  Cancel From");
                        return;
                    }
                    if (chkCancelBooking.Checked && dtpEndingCancel.Value == null)
                    {
                        ENUtils.ShowMessage("Required :  Cancel Till");
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

                    bool IsChanging = false;


                    if (chkStartingAt.Checked && chkEndingAt.Checked)
                    {

                        foreach (var gRow in grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == ""))
                        {

                            IsChanging = false;

                            if (gRow.Cells[COLS.MasterJobId].Value == null)
                            {

                                if (chkPickupTime.Enabled && dtpPickupTime.Value != null)
                                {

                                    gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;
                                }


                                if (chkPickupFares.Checked.ToBool())
                                {
                                    gRow.Cells[COLS.Fare].Value = numPickupFares.Value;

                                }





                                IsChanging = true;

                            }
                            else
                            {


                                IsChanging = true;


                            }





                            if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                               || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                            {

                                IsChanging = true;

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


                                //   gRow.Cells[COLS.DriverNo].Value

                            }




                            if (IsChanging == true)
                            {

                                gRow.Cells[COLS.CHANGED].Value = "1";
                            }
                            else
                            {
                                gRow.Cells[COLS.CHANGED].Value = "";

                            }

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

                            //if (chkPickupFares.Enabled)
                            if (chkPickupFares.Checked.ToBool())
                            {
                                gRow.Cells[COLS.Fare].Value = numPickupFares.Value;
                            }



                            //if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                            //{
                            //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            //}



                            //if (chkReturnFares.Checked.ToBool())
                            //{
                            //    gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                            //}


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

                            //if (chkPickupFares.Enabled)
                            if (chkPickupFares.Checked.ToBool())
                            {
                                gRow.Cells[COLS.Fare].Value = numPickupFares.Value;
                            }

                            //if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                            //{
                            //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            //}



                            //if (chkReturnFares.Checked.ToBool())
                            //{
                            //    gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                            //}



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
                        //New

                        //dtpEndingCancel.Value.ToDate();


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

                            //if (chkPickupFares.Enabled)
                            if (chkPickupFares.Checked.ToBool())
                            {
                                gRow.Cells[COLS.Fare].Value = numPickupFares.Value;
                            }

                            //if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                            //{
                            //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            //}



                            //if (chkReturnFares.Checked.ToBool())
                            //{
                            //    gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                            //}


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
                else
                {
                    //NC
                    string DayName = string.Empty;
                    if (rdoMon.IsChecked.ToBool())
                    {
                        DayName = rdoMon.Text.ToStr();
                    }
                    else if (rdoTue.IsChecked.ToBool())
                    {
                        DayName = rdoTue.Text.ToStr();
                    }
                    else if (rdoWed.IsChecked.ToBool())
                    {
                        DayName = rdoWed.Text.ToStr();
                    }
                    else if (rdoThu.IsChecked.ToBool())
                    {
                        DayName = rdoThu.Text.ToStr();
                    }
                    else if (rdoFri.IsChecked.ToBool())
                    {
                        DayName = rdoFri.Text.ToStr();
                    }
                    else if (rdoSat.IsChecked.ToBool())
                    {
                        DayName = rdoSat.Text.ToStr();
                    }
                    else if (rdoSun.IsChecked.ToBool())
                    {
                        DayName = rdoSun.Text.ToStr();
                    }
                    if (string.IsNullOrEmpty(DayName))
                    {
                        ENUtils.ShowMessage("Required : Day");
                        return;
                    }
                    foreach (var gRow in grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == ""))
                    {
                        if (gRow.Cells[COLS.DAY].Value.ToStr().StartsWith(DayName))
                        {
                            if (dtpDayWisePickupTime.Value != null)
                            {
                                //gRow.Cells[COLS.PickupDate].Value = dtpDayWisePickUpDate.Value.ToDate() + dtpDayWisePickupTime.Value.Value.TimeOfDay;
                                gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpDayWisePickupTime.Value.Value.TimeOfDay;
                            }

                            //if (dtpDayWiseReturnPickUpTime.Value != null && gRow.Cells[COLS.ReturnPickupDate].Value != null)
                            //{
                            //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpDayWiseReturnPickUpTime.Value.Value.TimeOfDay;
                            //}

                            gRow.Cells[COLS.CHANGED].Value = "1";

                        }
                    }

                }

                lblMessage.Visible = true;
                IsApplyChanges = true;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }



        }

        private void ApplyReturnChanges()
        {
            try
            {
                int VehicleId = ddlReturnVehicle.SelectedValue.ToInt();
                int PaymentTypeId = ddlReturnPaymentType.SelectedValue.ToInt();
                string CustomerName = txtReturnCustomerName.Text.ToStr().Trim();
                string Error = string.Empty;
                if (VehicleId == 0)
                {
                    Error = "Required : Vehicle";
                }
                if (PaymentTypeId == 0)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : Payment Type";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : Payment Type";
                    }
                }
                if (string.IsNullOrEmpty(CustomerName))
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : Customer Name";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : Customer Name";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                if (chkReturnDayWise.Checked.ToBool() == false)
                {

                    grdReturnBookings.Rows.ToList().ForEach(c => c.Cells[COLS.EXCLUDED].Value = "");
                    grdReturnBookings.Rows.ToList().ForEach(c => c.Cells[COLS.CHANGED].Value = "");

                    GridViewRowInfo row = null;
                    for (int i = 0; i < grdExcludeReturnPickupDates.Rows.Count; i++)
                    {
                        row = grdReturnBookings.Rows.FirstOrDefault(c => c.Cells[COLS.ID].Value.ToLong() == grdExcludeReturnPickupDates.Rows[i].Cells[COLS_PICKUPS.ID].Value.ToLong());
                        if (row != null)
                        {
                            row.Cells[COLS.EXCLUDED].Value = "1";
                        }
                    }
                    if (chkReturnStartingAt.Enabled == false && dtpReturnStartingAt.Value == null)
                    {
                        ENUtils.ShowMessage("Required : Beginning From");
                        return;
                    }
                    if (chkReturnEndingAt.Enabled == false && dtpReturnEndingAt.Value == null)
                    {
                        ENUtils.ShowMessage("Required : Beginning From");
                        return;
                    }
                    if (chkReturnPickupTime.Checked && dtpReturnPickupTime.Value == null)
                    {
                        ENUtils.ShowMessage("Required : Please Enter Pickup Time to Change");
                        return;
                    }

                    //if (chkReturnPickupTime.Checked && dtpReturnPickupTime.Value == null)
                    //{
                    //    ENUtils.ShowMessage("Required : Please Enter Return Pickup Time to Change");
                    //    return;
                    //}
                    if (chkReturnCancelBooking.Checked && dtpReturnStartingCancel.Value == null)
                    {
                        ENUtils.ShowMessage("Required :  Cancel From");
                        return;
                    }
                    if (chkReturnCancelBooking.Checked && dtpReturnEndingCancel.Value == null)
                    {
                        ENUtils.ShowMessage("Required :  Cancel Till");
                        return;
                    }
                    int journeyTypeId = ddlReturnJourneyType.SelectedIndex == 1 ? 2 : 1;
                    int? fromLocTypeId = ddlReturnFromLocType.SelectedValue.ToIntorNull();
                    int? toLocTypeId = ddlReturnToLocType.SelectedValue.ToIntorNull();
                    int? fromLocId = ddlReturnFromLocation.SelectedValue.ToIntorNull();
                    int? toLocId = ddlReturnToLocation.SelectedValue.ToIntorNull();
                    string fromAddress = txtReturnFromAddress.Text.Trim();
                    string toAddress = txtReturnToAddress.Text.Trim();
                    string fromDoorNo = txtReturnFromFlightDoorNo.Text.Trim();
                    string toDoorNo = txtReturnToFlightDoorNo.Text.Trim();
                    string fromStreet = txtReturnFromStreetComing.Text.Trim();
                    string fromPostCode = txtReturnFromPostCode.Text.Trim();
                    string toPostCode = txtReturnToPostCode.Text.Trim();
                    string toStreet = txtReturnToStreetComing.Text.Trim();

                    bool IsChanging = false;


                    if (chkReturnStartingAt.Checked && chkReturnEndingAt.Checked)
                    {

                        foreach (var gRow in grdReturnBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == ""))
                        {

                            IsChanging = false;

                            if (chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                            {
                                gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                                // gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;

                            }

                            if (chkReturnPickupFares.Checked.ToBool())
                            {
                                gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;

                            }


                            IsChanging = true;

                            //if (gRow.Cells[COLS.MasterJobId].Value == null)
                            //{

                            //    if (chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                            //    {

                            //        gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            //    }


                            //    // if (chkPickupFares.Enabled)
                            //    if (chkReturnPickupFares.Checked.ToBool())
                            //    {
                            //        gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;

                            //    }


                            //    //if (chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                            //    //{

                            //    //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;

                            //    //}

                            //    //if (chkReturnFares.Checked.ToBool())
                            //    //{
                            //    //    gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;

                            //    //}


                            //    IsChanging = true;

                            //}
                            //else
                            //{

                            //    // if (chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                            //    //{

                            //    //    gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;

                            //    //}

                            //    // if (chkReturnFares.Checked.ToBool())
                            //    // {
                            //    //     gRow.Cells[COLS.Fare].Value = numReturnFares.Value;

                            //    // }

                            //    IsChanging = true;



                            //}




                            //if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                            //{
                            //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            //}



                            //if (chkReturnFares.Checked.ToBool())
                            //{
                            //    gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                            //}




                            //if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                            //   || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                            {

                                IsChanging = true;

                                if (chkReturnPickup.Checked)
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
                                        gRow.Cells[COLS.FromAddress].Value = ddlReturnFromLocation.Text.ToStr().ToUpper().Trim();
                                    }
                                }
                                if (chkReturnDestination.Checked)
                                {
                                    gRow.Cells[COLS.ToLocTypeId].Value = toLocTypeId;
                                    gRow.Cells[COLS.ToLocId].Value = toLocId;
                                    gRow.Cells[COLS.ToDoorNo].Value = toDoorNo;
                                    gRow.Cells[COLS.ToStreet].Value = toStreet;


                                    if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                                        gRow.Cells[COLS.ToAddress].Value = toAddress;
                                    else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                                    {
                                        gRow.Cells[COLS.ToAddress].Value = toPostCode;
                                    }
                                    else
                                    {
                                        gRow.Cells[COLS.ToAddress].Value = ddlReturnToLocation.Text.ToStr().ToUpper().Trim();
                                    }

                                }
                            }




                            if (IsChanging == true)
                            {

                                gRow.Cells[COLS.CHANGED].Value = "1";
                            }
                            else
                            {
                                gRow.Cells[COLS.CHANGED].Value = "";

                            }

                        }
                    }
                    else if (chkReturnStartingAt.Checked && chkReturnEndingAt.Checked == false)
                    {
                        DateTime endingAt = dtpReturnEndingAt.Value.ToDate();

                        foreach (var gRow in grdReturnBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == "" && c.Cells[COLS.PickupDate].Value.ToDate() <= endingAt))
                        {

                            if (chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                            {
                                //gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                                gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            }

                            //if (chkPickupFares.Enabled)
                            if (chkReturnPickupFares.Checked.ToBool())
                            {
                                gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;
                                //gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;
                            }



                            //if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                            //{
                            //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            //}



                            //if (chkReturnFares.Checked.ToBool())
                            //{
                            //    gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                            //}


                            //if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                            //  || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                            //{
                            if (chkReturnPickup.Checked)
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

                                    gRow.Cells[COLS.FromAddress].Value = ddlReturnFromLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }


                            if (chkReturnDestination.Checked)
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

                                    gRow.Cells[COLS.ToAddress].Value = ddlReturnToLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }
                            // }

                            gRow.Cells[COLS.CHANGED].Value = "1";
                        }
                    }
                    else if (chkReturnStartingAt.Checked == false && chkReturnEndingAt.Checked == true)
                    {
                        DateTime startingAt = dtpReturnStartingAt.Value.ToDate();

                        foreach (var gRow in grdReturnBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == "" && c.Cells[COLS.PickupDate].Value.ToDate() >= startingAt))
                        {

                            if (chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                            {
                                //gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                                gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            }

                            //if (chkPickupFares.Enabled)
                            if (chkReturnPickupFares.Checked.ToBool())
                            {
                                gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;
                                //gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;
                            }

                            //if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                            //{
                            //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            //}



                            //if (chkReturnFares.Checked.ToBool())
                            //{
                            //    gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                            //}



                            //if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                            // || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                            //{
                            if (chkReturnPickup.Checked)
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

                                    gRow.Cells[COLS.FromAddress].Value = ddlReturnFromLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }


                            if (chkReturnDestination.Checked)
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

                                    gRow.Cells[COLS.ToAddress].Value = ddlReturnToLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }
                            //}

                            gRow.Cells[COLS.CHANGED].Value = "1";
                        }
                    }
                    else
                    {

                        DateTime startingAt = dtpReturnStartingAt.Value.ToDate();

                        DateTime endingAt = dtpReturnEndingAt.Value.ToDate();
                        //New

                        //dtpEndingCancel.Value.ToDate();


                        if (startingAt > endingAt)
                        {
                            ENUtils.ShowMessage("Beginning Date must be less than Ending Date");
                            return;

                        }

                        foreach (var gRow in grdReturnBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == ""
                            && (c.Cells[COLS.PickupDate].Value.ToDate() >= startingAt && c.Cells[COLS.PickupDate].Value.ToDate() <= endingAt)))
                        {

                            if (chkReturnPickupTime.Enabled && dtpReturnPickupTime.Value != null)
                            {
                                gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            }

                            //if (chkPickupFares.Enabled)
                            if (chkReturnPickupFares.Checked.ToBool())
                            {
                                gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;
                                // gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;
                            }

                            //if (chkReturnPickupTime.Enabled && gRow.Cells[COLS.ReturnPickupDate].Value != null && dtpReturnPickupTime.Value != null)
                            //{
                            //    gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            //}



                            //if (chkReturnFares.Checked.ToBool())
                            //{
                            //    gRow.Cells[COLS.ReturnFare].Value = numReturnFares.Value;
                            //}
                            if (chkReturnPickupFares.Checked.ToBool())
                            {
                                gRow.Cells[COLS.ReturnFare].Value = numReturnPickupFares.Value;
                                //gRow.Cells[COLS.Fare].Value = numReturnPickupFares.Value;
                            }

                            //if ((journeyTypeId == 1 && gRow.Cells[COLS.MasterJobId].Value == null)
                            //  || (gRow.Cells[COLS.MasterJobId].Value != null && journeyTypeId == 2))
                            //{
                            if (chkReturnPickup.Checked)
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

                                    gRow.Cells[COLS.FromAddress].Value = ddlReturnFromLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }


                            if (chkReturnDestination.Checked)
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

                                    gRow.Cells[COLS.ToAddress].Value = ddlReturnToLocation.Text.ToStr().ToUpper().Trim();
                                }
                            }
                            //  }

                            gRow.Cells[COLS.CHANGED].Value = "1";
                        }

                    }

                    //if (chkCancelBooking.Checked)
                    //{
                    //    DateTime startingAt = dtpStartingCancel.Value.ToDate();

                    //    DateTime endingAt = dtpEndingCancel.Value.ToDate();


                    //    if (startingAt > endingAt)
                    //    {
                    //        ENUtils.ShowMessage("Beginning Date must be less than Ending Date");
                    //        return;

                    //    }
                    //    foreach (var gRow in grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == "" && c.Cells[COLS.PickupDate].Value.ToDate() >= startingAt && c.Cells[COLS.PickupDate].Value.ToDate() <= endingAt))
                    //    {
                    //        gRow.Cells[COLS.BookingStatus].Value = Enums.BOOKINGSTATUS.CANCELLED;

                    //        gRow.Cells[COLS.CHANGED].Value = "1";
                    //    }
                    //}
                    //else
                    //{
                    //    string BookingId = string.Empty;

                    //    if (grdBookings != null && grdBookings.Rows.Count > 0)
                    //    {

                    //        // via = "&waypoints=";

                    //        // via += string.Join("|", grdVia.Rows.Select(c => General.GetPostCodeMatch(c.Cells["VIALOCATIONVALUE"].Value.ToStr().ToUpper()) + ", UK").ToArray<string>());
                    //        BookingId += string.Join(",", grdBookings.Rows.Select(c => c.Cells["ID"].Value.ToStr()).ToArray<string>());

                    //    }
                    //}
                }
                else
                {
                    //NC
                    string DayName = string.Empty;
                    if (rdoReturnMon.IsChecked.ToBool())
                    {
                        DayName = rdoReturnMon.Text.ToStr();
                    }
                    else if (rdoReturnTue.IsChecked.ToBool())
                    {
                        DayName = rdoReturnTue.Text.ToStr();
                    }
                    else if (rdoReturnWed.IsChecked.ToBool())
                    {
                        DayName = rdoReturnWed.Text.ToStr();
                    }
                    else if (rdoReturnThu.IsChecked.ToBool())
                    {
                        DayName = rdoReturnThu.Text.ToStr();
                    }
                    else if (rdoReturnFri.IsChecked.ToBool())
                    {
                        DayName = rdoReturnFri.Text.ToStr();
                    }
                    else if (rdoReturnSat.IsChecked.ToBool())
                    {
                        DayName = rdoReturnSat.Text.ToStr();
                    }
                    else if (rdoReturnSun.IsChecked.ToBool())
                    {
                        DayName = rdoReturnSun.Text.ToStr();
                    }
                    if (string.IsNullOrEmpty(DayName))
                    {
                        ENUtils.ShowMessage("Required : Day");
                        return;
                    }
                    foreach (var gRow in grdReturnBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == ""))
                    {
                        if (gRow.Cells[COLS.DAY].Value.ToStr().StartsWith(DayName))
                        {
                            //if (dtpDayWisePickupTime.Value != null)
                            //{
                            //    //gRow.Cells[COLS.PickupDate].Value = dtpDayWisePickUpDate.Value.ToDate() + dtpDayWisePickupTime.Value.Value.TimeOfDay;
                            //    gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpDayWisePickupTime.Value.Value.TimeOfDay;
                            //}

                            if (dtpReturnDayWisePickupTime.Value != null && gRow.Cells[COLS.PickupDate].Value != null)
                            {
                                gRow.Cells[COLS.PickupDate].Value = gRow.Cells[COLS.PickupDate].Value.ToDate() + dtpReturnDayWisePickupTime.Value.Value.TimeOfDay;
                                //gRow.Cells[COLS.ReturnPickupDate].Value = gRow.Cells[COLS.ReturnPickupDate].Value.ToDate() + dtpReturnDayWisePickupTime.Value.Value.TimeOfDay;
                            }

                            gRow.Cells[COLS.CHANGED].Value = "1";

                        }
                    }

                }

                lblMessage.Visible = true;
                IsApplyChanges = true;
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
            if (args.ToggleState == ToggleState.On)
            {
                dtpPickupTime.Enabled = true;
                chkDayWise.Checked = false;
                groupDayWise.Visible = false;
            }
            else
            {
                dtpPickupTime.Enabled = false;
            }
            // dtpPickupTime.Enabled = args.ToggleState == ToggleState.On;
        }



        private void chkPickupFares_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            // numPickupFares.SpinElement.TextBoxItem.Enabled = args.ToggleState == ToggleState.On;
            // numPickupFares.SpinElement.Enabled = args.ToggleState == ToggleState.On;
            if (args.ToggleState == ToggleState.On)
            {
                numPickupFares.Enabled = true; //== ToggleState.On;
            }
            else
            {
                numPickupFares.Enabled = false;
            }
        }

        private void chkReturnFares_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            if (args.ToggleState == ToggleState.On)
            {
                numReturnPickupFares.Enabled = true;
            }
            else
            {
                numReturnPickupFares.Enabled = false;
            }

        }

        private void btnRevertChanges_Click(object sender, EventArgs e)
        {
            DisplayOneWayBooking();
        }



        #region Pickup and Destination Settings
        string[] res = null;
        string searchTxt = "";

        bool IsKeyword = false;
        AutoCompleteTextBox aTxt;
        WebClient wc = null;


        private void ShowAddresses()
        {
            int sno = 1;




            //var finalList = (from a in AppVars.zonesList
            //                 from b in res
            //                 where b.Contains(a) && (b.Substring(b.IndexOf(a)).Split(' ')[0] == a && b[b.IndexOf(a) - 1] == ' ')
            //                 select b).ToArray<string>();


            var finalList = (from a in AppVars.zonesList
                             from b in res
                             where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && (b.IndexOf(a) - 1) >= 0 && b[b.IndexOf(a) - 1] == ' ' && GeneralBLL.GetHalfPostCodeMatch(b) == a)




                             select b).ToArray<string>();


            //var finalList = (from a in AppVars.zonesList
            //                 from b in res
            //                 where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && b[b.IndexOf(a) - 1] == ' ')
            //                 select b).ToArray<string>();

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







        void TextReturnBoxElement_TextChanged(object sender, EventArgs e)
        {

            try
            {

                IsKeyword = false;

                InitializeTimer();
                timer1.Stop();

                aTxt = (AutoCompleteTextBox)sender;
                aTxt.ResetListBox();

                if (aTxt.Name == "txtReturnFromAddress")
                    txtToAddress.SendToBack();

                else if (aTxt.Name == "txtReturnToAddress")
                    txtToAddress.BringToFront();



                if (EnablePOI)
                {

                    InitializeSearchPOIWorker();

                    if (POIWorker.IsBusy)
                    {
                        POIWorker.CancelAsync();

                        POIWorker.Dispose();
                        POIWorker = null;
                        GC.Collect();
                        InitializeSearchPOIWorker();

                    }


                    AddressTextChangePOI();
                }
                else
                {

                    AddressTextChangeWOPOI();
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

        //private void RemoveNumbering(string formerVal)
        //{

        //    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
        //    aTxt.Text = formerVal.ToStr() + " " + aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
        //    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

        //}

        private void RemoveNumbering(string formerVal)
        {

            aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);

            if (aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
            {

                aTxt.Text = (formerVal.ToStr() + " " + aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim()).Trim();
            }
            else
            {
                if (EnablePOI)
                {

                    aTxt.Text = aTxt.Text.ToStr().Trim();
                }
                else
                {
                    aTxt.Text = (formerVal.ToStr() + " " + aTxt.Text.ToStr().Trim()).Trim();

                }
            }

            aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

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


        private void SetReturnFromAddress(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                ddlReturnFromLocType.Enabled = true;
                ddlReturnFromLocation.Enabled = true;
                txtReturnFromAddress.Enabled = true;
                txtReturnFromFlightDoorNo.Enabled = true;
                txtReturnFromStreetComing.Enabled = true;

            }
            else
            {

                ddlReturnFromLocType.Enabled = false;
                ddlReturnFromLocation.Enabled = false;
                txtReturnFromAddress.Enabled = false;
                txtReturnFromFlightDoorNo.Enabled = false;
                txtReturnFromStreetComing.Enabled = false;

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


        private void SetReturnToAddress(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                ddlReturnToLocType.Enabled = true;
                ddlReturnToLocation.Enabled = true;
                txtReturnToAddress.Enabled = true;
                txtReturnToFlightDoorNo.Enabled = true;
                txtReturnToStreetComing.Enabled = true;

            }
            else
            {
                ddlReturnToLocType.Enabled = false;
                ddlReturnToLocation.Enabled = false;
                txtReturnToAddress.Enabled = false;
                txtReturnToFlightDoorNo.Enabled = false;
                txtReturnToStreetComing.Enabled = false;

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

            this.lblCustomerNameViaPoint = new Telerik.WinControls.UI.RadLabel();
            this.lblMobileNoViaPoint = new Telerik.WinControls.UI.RadLabel();


            this.txtCustomerNameViaPoint = new Telerik.WinControls.UI.RadTextBox();
            this.txtMobileNoViaPoint = new Telerik.WinControls.UI.RadTextBox();

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

            ((System.ComponentModel.ISupportInitialize)(this.lblCustomerNameViaPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMobileNoViaPoint)).BeginInit();


            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerNameViaPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobileNoViaPoint)).BeginInit();

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

            this.pnlVia.Controls.Add(this.lblCustomerNameViaPoint);
            this.pnlVia.Controls.Add(this.lblMobileNoViaPoint);

            this.pnlVia.Controls.Add(this.txtCustomerNameViaPoint);
            this.pnlVia.Controls.Add(this.txtMobileNoViaPoint);

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
            this.lblViaLoc.Location = new System.Drawing.Point(390, 41);
            this.lblViaLoc.Name = "lblViaLoc";
            this.lblViaLoc.Size = new System.Drawing.Size(80, 22);
            this.lblViaLoc.TabIndex = 138;
            this.lblViaLoc.Text = "Via Location";

            // 
            // lblMobileNoViaPoint
            // 
            this.lblMobileNoViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobileNoViaPoint.Location = new System.Drawing.Point(242, 65);
            this.lblMobileNoViaPoint.Name = "lblMobileNoViaPoint";
            this.lblMobileNoViaPoint.Size = new System.Drawing.Size(80, 22);
            this.lblMobileNoViaPoint.TabIndex = 138;
            this.lblMobileNoViaPoint.Text = "Mobile No";

            // 
            // txtMobileNoViaPoint
            // 
            this.txtMobileNoViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNoViaPoint.Location = new System.Drawing.Point(340, 65);
            this.txtMobileNoViaPoint.MaxLength = 100;
            this.txtMobileNoViaPoint.Name = "txtMobileNoViaPoint";
            this.txtMobileNoViaPoint.Size = new System.Drawing.Size(130, 22);
            this.txtMobileNoViaPoint.TabIndex = 266;
            this.txtMobileNoViaPoint.TabStop = false;

            // 
            // lblFromViaPoint
            // 
            this.lblFromViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromViaPoint.Location = new System.Drawing.Point(4, 42);
            this.lblFromViaPoint.Name = "lblFromViaPoint";
            this.lblFromViaPoint.Size = new System.Drawing.Size(28, 22);
            this.lblFromViaPoint.TabIndex = 137;
            this.lblFromViaPoint.Text = "Via";




            //      private Telerik.WinControls.UI.RadLabel lblCustomerNameViaPoint;
            //private Telerik.WinControls.UI.RadLabel lblMobileNoViaPoint;
            // 
            // lblCustomerNameViaPoint
            // 
            this.lblCustomerNameViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerNameViaPoint.Location = new System.Drawing.Point(4, 65);
            this.lblCustomerNameViaPoint.Name = "lblCustomerNameViaPoint";
            this.lblCustomerNameViaPoint.Size = new System.Drawing.Size(28, 22);
            this.lblCustomerNameViaPoint.TabIndex = 137;
            this.lblCustomerNameViaPoint.Text = "Name";

            // 
            // txtCustomerNameViaPoint
            // 
            this.txtCustomerNameViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerNameViaPoint.Location = new System.Drawing.Point(80, 65);
            this.txtCustomerNameViaPoint.MaxLength = 100;
            this.txtCustomerNameViaPoint.Name = "txtCustomerNameViaPoint";
            this.txtCustomerNameViaPoint.Size = new System.Drawing.Size(150, 22);
            this.txtCustomerNameViaPoint.TabIndex = 266;
            this.txtCustomerNameViaPoint.TabStop = false;


            // 
            // ddlViaFromLocType
            // 
            this.ddlViaFromLocType.Caption = null;
            this.ddlViaFromLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlViaFromLocType.Location = new System.Drawing.Point(80, 40);
            this.ddlViaFromLocType.Name = "ddlViaFromLocType";
            this.ddlViaFromLocType.Property = null;
            this.ddlViaFromLocType.ShowDownArrow = true;
            this.ddlViaFromLocType.Size = new System.Drawing.Size(150, 23);
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
            ((System.ComponentModel.ISupportInitialize)(this.lblCustomerNameViaPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMobileNoViaPoint)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerNameViaPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobileNoViaPoint)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.ddlViaFromLocType)).EndInit();


            FormatViaGrid();

            ComboFunctions.FillLocationTypeCombo(ddlViaFromLocType);
            ddlViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;

            pnlVia.BringToFront();
        }


        private void btnReturnSelectVia_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            CreateReturnViaPanel();

            if (args.ToggleState == ToggleState.On)
            {


                //btnSelectVia.Text = "Hide Via Point";
                btnReturnSelectVia.Text = "Hide Via Point(" + grdReturnVia.Rows.Count + ")";
                //radToggleButton1.Text = "Hide Via Point(" + grdVia.Rows.Count + ")";

                pnlReturnVia.Visible = true;
                pnlReturnVia.BringToFront();
                //pnlBottom.Location = this.PnlNewBottomLocation;
                txtReturnViaAddress.Select();
            }
            else
            {
                //btnSelectVia.Text = "Show Via Point";
                btnReturnSelectVia.Text = "Show Via Point(" + grdReturnVia.Rows.Count + ")";
                // radToggleButton1.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                pnlReturnVia.Visible = false;
                radPanel5.RootElement.Opacity = 1;
                // pnlMain.RootElement.Opacity = 1;
                //pnlBottom.Location = this.PnlOldBottomLocation;

                //ddlCustomerName.Select();

            }
        }

        Telerik.WinControls.UI.RadPanel pnlReturnVia = null;

        System.Windows.Forms.Label labelReturn7 = null;
        System.Windows.Forms.Label labelReturn3 = null;
        UI.AutoCompleteTextBox txtReturnViaAddress = null;
        Telerik.WinControls.UI.RadButton btnReturnClear = null;
        Telerik.WinControls.UI.RadButton btnReturnAddVia = null;
        UI.MyDropDownList ddlReturnViaLocation = null;
        Telerik.WinControls.UI.RadGridView grdReturnVia = null;
        UI.AutoCompleteTextBox txtReturnviaPostCode = null;
        Telerik.WinControls.UI.RadLabel lblReturnViaLoc = null;
        Telerik.WinControls.UI.RadLabel lblReturnFromViaPoint = null;
        UI.MyDropDownList ddlReturnViaFromLocType = null;


        private void CreateReturnViaPanel()
        {

            if (pnlReturnVia != null)
                return;


            this.pnlReturnVia = new Telerik.WinControls.UI.RadPanel();

            labelReturn7 = new System.Windows.Forms.Label();
            labelReturn3 = new System.Windows.Forms.Label();
            this.txtReturnViaAddress = new UI.AutoCompleteTextBox();
            this.btnReturnClear = new Telerik.WinControls.UI.RadButton();
            this.btnReturnAddVia = new Telerik.WinControls.UI.RadButton();
            this.ddlReturnViaLocation = new UI.MyDropDownList();
            this.grdReturnVia = new Telerik.WinControls.UI.RadGridView();
            this.txtReturnviaPostCode = new UI.AutoCompleteTextBox();
            this.lblReturnViaLoc = new Telerik.WinControls.UI.RadLabel();
            this.lblReturnFromViaPoint = new Telerik.WinControls.UI.RadLabel();
            this.ddlReturnViaFromLocType = new UI.MyDropDownList();

            this.lblCustomerNameReturnViaPoint = new Telerik.WinControls.UI.RadLabel();
            this.lblMobileNoReturnViaPoint = new Telerik.WinControls.UI.RadLabel();


            this.txtCustomerNameReturnViaPoint = new Telerik.WinControls.UI.RadTextBox();
            this.txtMobileNoReturnViaPoint = new Telerik.WinControls.UI.RadTextBox();


            ((System.ComponentModel.ISupportInitialize)(this.pnlReturnVia)).BeginInit();
            this.pnlReturnVia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnViaAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReturnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReturnAddVia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnViaLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReturnVia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReturnVia.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnviaPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnViaLoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnFromViaPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnViaFromLocType)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.lblCustomerNameReturnViaPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMobileNoReturnViaPoint)).BeginInit();


            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerNameReturnViaPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobileNoReturnViaPoint)).BeginInit();



            //this.pnlMain.Controls.Add(this.pnlVia);
            this.radPanel7.Controls.Add(this.pnlReturnVia);


            this.pnlReturnVia.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlReturnVia.Controls.Add(this.labelReturn7);
            this.pnlReturnVia.Controls.Add(this.labelReturn3);
            this.pnlReturnVia.Controls.Add(this.txtReturnViaAddress);
            this.pnlReturnVia.Controls.Add(this.btnReturnClear);
            this.pnlReturnVia.Controls.Add(this.btnReturnAddVia);
            this.pnlReturnVia.Controls.Add(this.ddlReturnViaLocation);
            this.pnlReturnVia.Controls.Add(this.grdReturnVia);
            this.pnlReturnVia.Controls.Add(this.txtReturnviaPostCode);
            this.pnlReturnVia.Controls.Add(this.lblReturnViaLoc);
            this.pnlReturnVia.Controls.Add(this.lblReturnFromViaPoint);
            this.pnlReturnVia.Controls.Add(this.ddlReturnViaFromLocType);

            this.pnlReturnVia.Controls.Add(this.lblCustomerNameReturnViaPoint);
            this.pnlReturnVia.Controls.Add(this.lblMobileNoReturnViaPoint);

            this.pnlReturnVia.Controls.Add(this.txtCustomerNameReturnViaPoint);
            this.pnlReturnVia.Controls.Add(this.txtMobileNoReturnViaPoint);


            this.pnlReturnVia.Location = new System.Drawing.Point(9, 230);
            this.pnlReturnVia.Name = "pnlVia";
            // 
            // 
            // 
            this.pnlReturnVia.RootElement.Opacity = 1;
            this.pnlReturnVia.Size = new System.Drawing.Size(910, 220);
            this.pnlReturnVia.TabIndex = 1;
            this.pnlReturnVia.Visible = false;
            txtReturnViaAddress.ListBoxElement.Font = new Font("Tahoma", 11, FontStyle.Bold);
            ((Telerik.WinControls.UI.RadPanelElement)(this.pnlReturnVia.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.pnlReturnVia.GetChildAt(0).GetChildAt(1))).Width = 2F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.pnlReturnVia.GetChildAt(0).GetChildAt(1))).BottomWidth = 1F;
            // 
            // label7
            // 
            this.labelReturn7.BackColor = System.Drawing.Color.Navy;
            this.labelReturn7.Image = global::Taxi_AppMain.Properties.Resources.delete;
            this.labelReturn7.Location = new System.Drawing.Point(864, 0);
            this.labelReturn7.Name = "label7";
            this.labelReturn7.Size = new System.Drawing.Size(35, 24);
            this.labelReturn7.TabIndex = 8;
            this.labelReturn7.Click += new System.EventHandler(this.labelReturn7_Click);
            // 
            // label3
            // 
            this.labelReturn3.BackColor = System.Drawing.Color.Navy;
            this.labelReturn3.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelReturn3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReturn3.ForeColor = System.Drawing.Color.White;
            this.labelReturn3.Location = new System.Drawing.Point(0, 0);
            this.labelReturn3.Name = "label3";
            this.labelReturn3.Size = new System.Drawing.Size(910, 25);
            this.labelReturn3.TabIndex = 7;
            this.labelReturn3.Text = "Via Locations";

            this.labelReturn3.Click += new EventHandler(labelReturn3_Click);
            this.labelReturn3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblCustomerNameReturnViaPoint
            // 
            this.lblCustomerNameReturnViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerNameReturnViaPoint.Location = new System.Drawing.Point(4, 65);
            this.lblCustomerNameReturnViaPoint.Name = "lblCustomerNameReturnViaPoint";
            this.lblCustomerNameReturnViaPoint.Size = new System.Drawing.Size(28, 22);
            this.lblCustomerNameReturnViaPoint.TabIndex = 137;
            this.lblCustomerNameReturnViaPoint.Text = "Name";


            // 
            // txtCustomerNameReturnViaPoint
            // 
            this.txtCustomerNameReturnViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerNameReturnViaPoint.Location = new System.Drawing.Point(80, 65);
            this.txtCustomerNameReturnViaPoint.MaxLength = 100;
            this.txtCustomerNameReturnViaPoint.Name = "txtCustomerNameReturnViaPoint";
            this.txtCustomerNameReturnViaPoint.Size = new System.Drawing.Size(150, 22);
            this.txtCustomerNameReturnViaPoint.TabIndex = 266;
            this.txtCustomerNameReturnViaPoint.TabStop = false;



            // 
            // lblMobileNoReturnViaPoint
            // 
            this.lblMobileNoReturnViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMobileNoReturnViaPoint.Location = new System.Drawing.Point(240, 65);
            this.lblMobileNoReturnViaPoint.Name = "lblMobileNoReturnViaPoint";
            this.lblMobileNoReturnViaPoint.Size = new System.Drawing.Size(95, 22);
            this.lblMobileNoReturnViaPoint.TabIndex = 138;
            this.lblMobileNoReturnViaPoint.Text = "Mobile No";

            // 
            // txtMobileNoReturnViaPoint
            // 
            this.txtMobileNoReturnViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNoReturnViaPoint.Location = new System.Drawing.Point(340, 65);
            this.txtMobileNoReturnViaPoint.MaxLength = 100;
            this.txtMobileNoReturnViaPoint.Name = "txtMobileNoReturnViaPoint";
            this.txtMobileNoReturnViaPoint.Size = new System.Drawing.Size(130, 22);
            this.txtMobileNoReturnViaPoint.TabIndex = 266;
            this.txtMobileNoReturnViaPoint.TabStop = false;


            // 
            // txtViaAddress
            // pn
            this.txtReturnViaAddress.BackColor = System.Drawing.Color.White;
            this.txtReturnViaAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReturnViaAddress.DefaultHeight = 60;
            this.txtReturnViaAddress.DefaultWidth = 370;
            this.txtReturnViaAddress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnViaAddress.ForceListBoxToUpdate = false;
            this.txtReturnViaAddress.FormerValue = "";
            this.txtReturnViaAddress.Location = new System.Drawing.Point(491, 38);
            this.txtReturnViaAddress.Multiline = true;
            this.txtReturnViaAddress.Name = "txtReturnViaAddress";
            // 
            // 
            // 
            this.txtReturnViaAddress.RootElement.StretchVertically = true;
            this.txtReturnViaAddress.SelectedItem = null;
            this.txtReturnViaAddress.Size = new System.Drawing.Size(257, 53);
            this.txtReturnViaAddress.TabIndex = 2;
            this.txtReturnViaAddress.TabStop = false;
            this.txtReturnViaAddress.Values = null;
            this.txtReturnViaAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReturnViaAddress_KeyDown);
            this.txtReturnViaAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromAddress_KeyPress);
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtReturnViaAddress.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnViaAddress.GetChildAt(0).GetChildAt(2))).Width = 1F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnViaAddress.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnViaAddress.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnViaAddress.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnViaAddress.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnViaAddress.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // btnClear
            // 
            this.btnReturnClear.Location = new System.Drawing.Point(771, 65);
            this.btnReturnClear.Name = "btnClear";
            this.btnReturnClear.Size = new System.Drawing.Size(82, 24);
            this.btnReturnClear.TabIndex = 4;
            this.btnReturnClear.Text = "Clear";
            this.btnReturnClear.Click += new System.EventHandler(this.btnReturnClear_Click);
            // 
            // btnAddVia
            // 
            this.btnReturnAddVia.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnReturnAddVia.Location = new System.Drawing.Point(771, 33);
            this.btnReturnAddVia.Name = "btnAddVia";
            this.btnReturnAddVia.Size = new System.Drawing.Size(82, 24);
            this.btnReturnAddVia.TabIndex = 3;
            this.btnReturnAddVia.Text = "Add";
            this.btnReturnAddVia.Click += new System.EventHandler(this.btnReturnAddVia_Click);

            this.ddlReturnViaLocation.Caption = null;
            this.ddlReturnViaLocation.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlReturnViaLocation.Location = new System.Drawing.Point(491, 38);
            this.ddlReturnViaLocation.Name = "ddlViaLocation";
            this.ddlReturnViaLocation.Property = null;
            this.ddlReturnViaLocation.ShowDownArrow = true;
            this.ddlReturnViaLocation.Size = new System.Drawing.Size(250, 27);
            this.ddlReturnViaLocation.TabIndex = 0;
            // 
            // grdVia
            // 
            this.grdReturnVia.Location = new System.Drawing.Point(7, 93);
            this.grdReturnVia.Name = "grdVia";
            this.grdReturnVia.Size = new System.Drawing.Size(881, 120);
            this.grdReturnVia.TabIndex = 5;
            this.grdReturnVia.Text = "radGridView1";
            this.grdReturnVia.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdReturnVia_CellDoubleClick);
            // 
            // txtviaPostCode
            // 
            this.txtReturnviaPostCode.BackColor = System.Drawing.Color.White;
            this.txtReturnviaPostCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReturnviaPostCode.DefaultHeight = 90;
            this.txtReturnviaPostCode.DefaultWidth = 185;
            this.txtReturnviaPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnviaPostCode.ForceListBoxToUpdate = false;
            this.txtReturnviaPostCode.FormerValue = "";
            this.txtReturnviaPostCode.Location = new System.Drawing.Point(492, 40);
            this.txtReturnviaPostCode.MaxLength = 100;
            this.txtReturnviaPostCode.Name = "txtviaPostCode";
            this.txtReturnviaPostCode.SelectedItem = null;
            this.txtReturnviaPostCode.Size = new System.Drawing.Size(195, 26);
            this.txtReturnviaPostCode.TabIndex = 0;
            this.txtReturnviaPostCode.TabStop = false;
            this.txtReturnviaPostCode.Values = null;
            this.txtReturnviaPostCode.TextChanged += new System.EventHandler(this.txtReturnviaPostCode_TextChanged);

            this.lblReturnViaLoc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnViaLoc.Location = new System.Drawing.Point(390, 41);
            this.lblReturnViaLoc.Name = "lblViaLoc";
            this.lblReturnViaLoc.Size = new System.Drawing.Size(90, 22);
            this.lblReturnViaLoc.TabIndex = 138;
            this.lblReturnViaLoc.Text = "Via Location";
            // 
            // lblFromViaPoint
            // 
            this.lblReturnFromViaPoint.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnFromViaPoint.Location = new System.Drawing.Point(4, 42);
            this.lblReturnFromViaPoint.Name = "lblFromViaPoint";
            this.lblReturnFromViaPoint.Size = new System.Drawing.Size(28, 22);
            this.lblReturnFromViaPoint.TabIndex = 137;
            this.lblReturnFromViaPoint.Text = "Via";
            // 
            // ddlViaFromLocType
            // 
            this.ddlReturnViaFromLocType.Caption = null;
            this.ddlReturnViaFromLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlReturnViaFromLocType.Location = new System.Drawing.Point(80, 40);
            this.ddlReturnViaFromLocType.Name = "ddlReturnViaFromLocType";
            this.ddlReturnViaFromLocType.Property = null;
            this.ddlReturnViaFromLocType.ShowDownArrow = true;
            this.ddlReturnViaFromLocType.Size = new System.Drawing.Size(150, 23);
            this.ddlReturnViaFromLocType.TabIndex = 1;
            this.ddlReturnViaFromLocType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlReturnViaFromLocType_SelectedIndexChanged);


            txtReturnViaAddress.ListBoxElement.Width = 425;
            txtReturnViaAddress.ListBoxElement.Height = 130;
            this.txtReturnViaAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);
            txtReturnViaAddress.DefaultHeight = 110;

            ((System.ComponentModel.ISupportInitialize)(this.pnlReturnVia)).EndInit();
            this.pnlReturnVia.ResumeLayout(false);
            this.pnlReturnVia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnViaAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReturnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReturnAddVia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnViaLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReturnVia.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReturnVia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnviaPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnViaLoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnFromViaPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnViaFromLocType)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.lblCustomerNameReturnViaPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblMobileNoReturnViaPoint)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerNameReturnViaPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobileNoReturnViaPoint)).EndInit();


            FormatReturnViaGrid();

            ComboFunctions.FillLocationTypeCombo(ddlReturnViaFromLocType);
            ddlReturnViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;

            pnlReturnVia.BringToFront();
        }


        void label3_Click(object sender, EventArgs e)
        {
            btnSelectVia.ToggleState = ToggleState.Off;
            // radToggleButton1.ToggleState = ToggleState.Off;
            pnlVia.Visible = false;
            //radToggleButton1.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
            btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
        }

        void labelReturn3_Click(object sender, EventArgs e)
        {
            btnReturnSelectVia.ToggleState = ToggleState.Off;
            // radToggleButton1.ToggleState = ToggleState.Off;
            pnlReturnVia.Visible = false;
            //radToggleButton1.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
            btnReturnSelectVia.Text = "Show Via Point(" + grdReturnVia.Rows.Count + ")";
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

            AddColumn(grdVia, "ColMoveUp", "MoveUp", Resources.Resource1.lc_moveup);
            AddColumn(grdVia, "ColMoveDown", "MoveDown", Resources.Resource1.lc_movedown);

            AddColumn(grdVia, "ColDelete", "Delete", null);

            //    grdVia.Columns["ColMoveUp"].IsVisible = false;
            //   grdVia.Columns["ColMoveDown"].IsVisible = false;
            grdVia.Columns["ColRervP"].IsVisible = false;
            grdVia.Columns["ColRervD"].IsVisible = false;

            grdVia.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdVia.CellFormatting += new CellFormattingEventHandler(grdVia_CellFormatting);
        }


        private void FormatReturnViaGrid()
        {


            grdReturnVia.RowsChanged += new GridViewCollectionChangedEventHandler(grdVia_RowsChanged);
            grdReturnVia.AutoSizeRows = true;
            grdReturnVia.TableElement.TableHeaderHeight = 0;
            grdReturnVia.ShowGroupPanel = false;
            grdReturnVia.AllowAddNewRow = false;
            grdReturnVia.AllowEditRow = false;
            grdReturnVia.ShowRowHeaderColumn = false;

            grdReturnVia.TableElement.BorderWidth = 0;
            grdReturnVia.TableElement.BorderColor = Color.Transparent;

            grdReturnVia.EnableHotTracking = false;
            grdReturnVia.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdReturnVia.EnableAlternatingRowColor = true;
            grdReturnVia.TableElement.AlternatingRowColor = Color.AliceBlue;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "ID";
            grdReturnVia.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "MASTERID";
            grdReturnVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "FROMVIALOCTYPEID";
            grdReturnVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "VIALOCATIONID";
            grdReturnVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "FROMTYPELABEL";
            col.HeaderText = "";
            col.Width = 100;
            grdReturnVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "FROMTYPEVALUE";
            col.Width = 150;
            col.HeaderText = "";
            grdReturnVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "EMPTY";
            col.IsVisible = false;
            col.Width = 100;
            grdReturnVia.Columns.Add(col);


            AddReverceFromColumn(grdReturnVia);
            AddReverceDestinationColumn(grdReturnVia);


            col = new GridViewTextBoxColumn();
            col.Name = "VIALOCATIONLABEL";
            col.HeaderText = "";
            col.Width = 120;
            grdReturnVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "VIALOCATIONVALUE";
            col.Width = 280;
            col.HeaderText = "";
            grdReturnVia.Columns.Add(col);

            AddColumn(grdReturnVia, "ColMoveUp", "MoveUp", Resources.Resource1.lc_moveup);
            AddColumn(grdReturnVia, "ColMoveDown", "MoveDown", Resources.Resource1.lc_movedown);

            AddColumn(grdReturnVia, "ColDelete", "Delete", null);

            //  grdReturnVia.Columns["ColMoveUp"].IsVisible = false;
            //  grdReturnVia.Columns["ColMoveDown"].IsVisible = false;
            grdReturnVia.Columns["ColRervP"].IsVisible = false;
            grdReturnVia.Columns["ColRervD"].IsVisible = false;

            grdReturnVia.CommandCellClick += new CommandCellClickEventHandler(gridReturn_CommandCellClick);
            grdReturnVia.CellFormatting += new CellFormattingEventHandler(grdVia_CellFormatting);
        }


        private void txtViaAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {

                AddViaPoint();
                FocusOnViAddress();

            }
        }

        private void txtReturnViaAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {

                AddReturnViaPoint();
                //FocusOnViAddress();

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


        private void ddlReturnViaFromLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillReturnViaLocations();
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

                ReverseReturnVia(viapointText, viaLocTypeId, viapointLabel, ToAddress, ddlToLocType.Text.ToStr(), ddlToLocType.SelectedValue.ToIntorNull(), false, ToLocationId);


            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }




        void ReverceReturnToPickUpPoint()
        {
            try
            {
                // for via variables
                string viapointText = grdReturnVia.CurrentRow.Cells["VIALOCATIONVALUE"].Value.ToString();
                int viaLocTypeId = grdReturnVia.CurrentRow.Cells["FROMVIALOCTYPEID"].Value.ToInt();
                string viapointLabel = grdReturnVia.CurrentRow.Cells["FROMTYPEVALUE"].Value.ToString();

                // for Top Variables

                string FromAddress = "";
                int FromLocationId = grdReturnVia.CurrentRow.Cells["VIALOCATIONID"].Value.ToInt();
                if (ddlReturnFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlReturnFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                {
                    FromAddress = txtReturnFromAddress.Text.ToStr();
                }
                else
                {
                    FromAddress = ddlReturnFromLocation.Text.ToStr();
                    FromLocationId = ddlReturnFromLocation.SelectedValue.ToInt();
                }

                ReverseReturnVia(viapointText, viaLocTypeId, viapointLabel, FromAddress, ddlReturnFromLocType.Text.ToStr(), ddlReturnFromLocType.SelectedValue.ToIntorNull(), true, FromLocationId);

            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }
        void ReverceReturnToDestination()
        {
            try
            {

                // for via variables
                string viapointText = grdReturnVia.CurrentRow.Cells["VIALOCATIONVALUE"].Value.ToString();
                int viaLocTypeId = grdReturnVia.CurrentRow.Cells["FROMVIALOCTYPEID"].Value.ToInt();
                string viapointLabel = grdReturnVia.CurrentRow.Cells["FROMTYPEVALUE"].Value.ToString();

                // for Top Variables

                string ToAddress = "";
                int ToLocationId = grdReturnVia.CurrentRow.Cells["VIALOCATIONID"].Value.ToInt();
                if (ddlReturnToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlReturnToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                {
                    ToAddress = txtReturnToAddress.Text.ToStr();
                }
                else
                {
                    ToAddress = ddlReturnToLocation.Text.ToStr();
                    ToLocationId = ddlReturnToLocation.SelectedValue.ToInt();
                }

                ReverseReturnVia(viapointText, viaLocTypeId, viapointLabel, ToAddress, ddlReturnToLocType.Text.ToStr(), ddlReturnToLocType.SelectedValue.ToIntorNull(), false, ToLocationId);


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

        private void btnReturnClear_Click(object sender, EventArgs e)
        {

            ClearReturnViaDetails();

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

                //row.Cells["FROMTYPELABEL"].Value = fromViaPaxName;
                //row.Cells["FROMTYPEVALUE"].Value = fromViaMobileNo;
                txtCustomerNameViaPoint.Text = row.Cells["FROMTYPELABEL"].Value.ToStr();
                txtMobileNoViaPoint.Text = row.Cells["FROMTYPEVALUE"].Value.ToStr();


            }
        }


        private void grdReturnVia_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdReturnVia.CurrentRow != null && grdReturnVia.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdReturnVia.CurrentRow;

                ddlReturnViaFromLocType.SelectedValue = row.Cells["FROMVIALOCTYPEID"].Value.ToInt();

                string locValue = row.Cells["VIALOCATIONVALUE"].Value.ToStr();

                txtReturnViaAddress.TextChanged -= new EventHandler(TextReturnBoxElement_TextChanged);
                txtReturnViaAddress.Text = locValue;
                txtReturnViaAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);

                txtReturnviaPostCode.TextChanged -= new EventHandler(txtReturnviaPostCode_TextChanged);
                txtReturnviaPostCode.Text = locValue;
                txtReturnviaPostCode.TextChanged += new EventHandler(txtReturnviaPostCode_TextChanged);

                ddlReturnViaLocation.SelectedValue = row.Cells["VIALOCATIONID"].Value.ToInt();


                //row.Cells["FROMTYPELABEL"].Value = fromViaPaxName;
                //row.Cells["FROMTYPEVALUE"].Value = fromViaMobileNo;
                txtCustomerNameReturnViaPoint.Text = row.Cells["FROMTYPELABEL"].Value.ToStr();
                txtMobileNoReturnViaPoint.Text = row.Cells["FROMTYPEVALUE"].Value.ToStr();


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


        private void txtReturnviaPostCode_TextChanged(object sender, EventArgs e)
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

                        if (txtData.Name == "txtViaAddress")
                        {
                            txtData.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        }
                        else if (txtData.Name == "txtReturnViaAddress")
                        {
                            txtData.TextChanged -= new EventHandler(TextReturnBoxElement_TextChanged);
                        }



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



                        if (txtData.Name == "txtViaAddress")
                        {
                            txtData.ResetListBox();
                            AddViaPoint();
                            txtData.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                        }
                        else if (txtData.Name == "txtReturnViaAddress")
                        {
                            txtData.ResetListBox();
                            AddReturnViaPoint();
                            txtData.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);

                        }






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

        private void labelReturn7_Click(object sender, EventArgs e)
        {
            btnReturnSelectVia.ToggleState = ToggleState.Off;
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



        void ReverseReturnVia(string viaText, int viaLoctypeId, string viaLabel, string FromAddress, string fromLocType, int? fromlocTypeId, bool IsFrom, int locationId)
        {
            try
            {

                string ViaTextTemp = viaText;
                string FromAddressTemp = FromAddress;
                int? fromLocIdTemp = fromlocTypeId;
                int VialocIdTemp = viaLoctypeId;




                if (IsFrom == true)
                {
                    this.txtReturnFromAddress.TextChanged -= new EventHandler(TextReturnBoxElement_TextChanged);


                    if (VialocIdTemp == Enums.LOCATION_TYPES.ADDRESS || VialocIdTemp == Enums.LOCATION_TYPES.BASE)
                    {
                        txtFromAddress.Text = ViaTextTemp;
                        ddlReturnFromLocType.SelectedValue = VialocIdTemp;
                    }
                    else
                    {
                        ddlReturnFromLocType.SelectedValue = VialocIdTemp;
                        ddlReturnFromLocation.SelectedValue = locationId;
                    }

                    GridViewRowInfo row;

                    if (grdReturnVia.CurrentRow != null && grdReturnVia.CurrentRow is GridViewNewRowInfo)
                        grdReturnVia.CurrentRow = null;


                    if (grdReturnVia.CurrentRow != null)
                    {

                        row = grdReturnVia.CurrentRow;
                    }

                    grdReturnVia.CurrentRow.Cells["VIALOCATIONVALUE"].Value = FromAddressTemp;
                    grdReturnVia.CurrentRow.Cells["FROMVIALOCTYPEID"].Value = fromLocIdTemp;

                    grdReturnVia.CurrentRow.Cells["FROMTYPEVALUE"].Value = fromLocType;


                    if (locationId != 0)
                        grdReturnVia.CurrentRow.Cells["VIALOCATIONID"].Value = locationId;
                    else
                        grdReturnVia.CurrentRow.Cells["VIALOCATIONID"].Value = null;



                    this.txtReturnFromAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);
                }
                else
                {
                    this.txtReturnToAddress.TextChanged -= new EventHandler(TextReturnBoxElement_TextChanged);

                    if (VialocIdTemp == Enums.LOCATION_TYPES.ADDRESS || VialocIdTemp == Enums.LOCATION_TYPES.BASE)
                    {
                        txtReturnToAddress.Text = ViaTextTemp;
                        ddlReturnToLocType.SelectedValue = VialocIdTemp;
                    }
                    else
                    {
                        ddlReturnToLocType.SelectedValue = VialocIdTemp;
                        ddlReturnToLocation.SelectedValue = locationId;
                    }

                    GridViewRowInfo row;

                    if (grdReturnVia.CurrentRow != null && grdReturnVia.CurrentRow is GridViewNewRowInfo)
                        grdReturnVia.CurrentRow = null;


                    if (grdReturnVia.CurrentRow != null)
                    {
                        row = grdReturnVia.CurrentRow;
                    }
                    else
                    {

                    }

                    grdReturnVia.CurrentRow.Cells["VIALOCATIONVALUE"].Value = FromAddressTemp;
                    grdReturnVia.CurrentRow.Cells["FROMVIALOCTYPEID"].Value = fromLocIdTemp;

                    grdReturnVia.CurrentRow.Cells["FROMTYPEVALUE"].Value = fromLocType;


                    if (locationId != 0)
                        grdReturnVia.CurrentRow.Cells["VIALOCATIONID"].Value = locationId;
                    else
                        grdReturnVia.CurrentRow.Cells["VIALOCATIONID"].Value = null;




                    this.txtReturnToAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);


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

                //  SetDropOffZone(ddlToLocation.Text);


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


                string fromViaPaxName = txtCustomerNameViaPoint.Text.Trim();
                string fromViaMobileNo = txtMobileNoViaPoint.Text.Trim();


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
                //row.Cells["FROMTYPELABEL"].Value = "Via";

                // row.Cells["FROMTYPEVALUE"].Value = fromViaValue;

                row.Cells["VIALOCATIONID"].Value = toViaLocId;
                row.Cells["VIALOCATIONLABEL"].Value = ToViaLocLabel;
                row.Cells["VIALOCATIONVALUE"].Value = toViaLoc;


                row.Cells["FROMTYPELABEL"].Value = fromViaPaxName;
                row.Cells["FROMTYPEVALUE"].Value = fromViaMobileNo;





                ClearViaDetails();


                CalculateAutoFares();


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void AddReturnViaPoint()
        {

            try
            {
                int LocTypeId = ddlReturnViaFromLocType.SelectedValue.ToInt();

                string fromViaLabel = lblReturnFromViaPoint.Text.Trim();
                string fromViaValue = ddlReturnViaFromLocType.Text.Trim();

                string CustomerName = txtCustomerNameReturnViaPoint.Text.Trim();
                string MobileNo = txtMobileNoReturnViaPoint.Text.Trim();

                int? toViaLocId = ddlReturnViaLocation.SelectedValue.ToIntorNull();
                string ToViaLocLabel = lblReturnViaLoc.Text.Trim();
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
                        toViaLoc = txtReturnViaAddress.Text.Trim();
                        msg2 += "Required : Via Address." + Environment.NewLine;
                    }
                    else if (LocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        toViaLoc = txtReturnviaPostCode.Text.Trim();
                        msg2 += "Required : Via PostCode." + Environment.NewLine;

                    }
                    else
                    {

                        toViaLoc = ddlReturnViaLocation.Text.Trim();
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

                if (grdReturnVia.CurrentRow != null && grdReturnVia.CurrentRow is GridViewNewRowInfo)
                    grdReturnVia.CurrentRow = null;


                if (grdReturnVia.CurrentRow != null)
                    row = grdReturnVia.CurrentRow;
                else
                    row = grdReturnVia.Rows.AddNew();



                row.Cells["FROMVIALOCTYPEID"].Value = LocTypeId;
                //  row.Cells[COLS.FROMTYPELABEL].Value = fromViaLabel;
                //row.Cells["FROMTYPELABEL"].Value = "Via";

                row.Cells["FROMTYPELABEL"].Value = CustomerName;
                row.Cells["FROMTYPEVALUE"].Value = MobileNo;
                // row.Cells["FROMTYPEVALUE"].Value = fromViaValue;

                row.Cells["VIALOCATIONID"].Value = toViaLocId;
                row.Cells["VIALOCATIONLABEL"].Value = ToViaLocLabel;
                row.Cells["VIALOCATIONVALUE"].Value = toViaLoc;





                ClearReturnViaDetails();


                //  CalculateAutoFares();


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void ClearReturnViaDetails()
        {
            pnlReturnVia.Visible = true;
            ddlReturnViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
            grdReturnVia.CurrentRow = null;
            txtReturnViaAddress.Text = string.Empty;
            txtReturnviaPostCode.Text = string.Empty;
            ddlReturnViaLocation.SelectedValue = null;
            txtReturnViaAddress.Select();
            txtReturnViaAddress.Text = string.Empty;
            txtMobileNoReturnViaPoint.Text = string.Empty;
            txtCustomerNameReturnViaPoint.Text = string.Empty;
        }


        private void ClearViaDetails()
        {
            if (pnlVia != null)
            {
                pnlVia.Visible = true;
                ddlViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
                grdVia.CurrentRow = null;
                txtViaAddress.Text = string.Empty;
                txtviaPostCode.Text = string.Empty;
                ddlViaLocation.SelectedValue = null;
                txtViaAddress.Select();
                txtViaAddress.Text = string.Empty;
                txtMobileNoViaPoint.Text = string.Empty;
                txtCustomerNameViaPoint.Text = string.Empty;
            }
        }

        private void btnAddVia_Click(object sender, EventArgs e)
        {
            updateVia = true;
            AddViaPoint();
            FocusOnViAddress();
        }


        private void btnReturnAddVia_Click(object sender, EventArgs e)
        {
            updateReturnVia = true;
            AddReturnViaPoint();
            // FocusOnViAddress();
        }



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
                updateVia = true;

                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == "ColDelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a via Address ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();


                        //  CalculateAutoFares();
                    }
                }
                else if (gridCell.ColumnInfo.Name == "ColRervP")
                {
                    if (txtFromAddress.Text != "" || ddlFromLocation.SelectedValue != null)
                    {
                        ReverceToPickUpPoint();

                        //   CalculateAutoFares();
                    }

                }
                else if (gridCell.ColumnInfo.Name == "ColRervD")
                {
                    if (txtToAddress.Text != "" || ddlToLocation.SelectedValue != null)
                    {
                        ReverceToDestination();
                        //  CalculateAutoFares();
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
                if (currentRow == null || currentRow.Index == -1)
                {
                    return;
                }

                //if(currentRow!=null)
                //{
                //    if(currentRow.Index==-1)
                //        currentRow
                //}

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






        void gridReturn_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                updateReturnVia = true;

                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == "ColDelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a via Address ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();


                        //  CalculateAutoFares();
                    }
                }
                else if (gridCell.ColumnInfo.Name == "ColRervP")
                {
                    if (txtReturnFromAddress.Text != "" || ddlReturnFromLocation.SelectedValue != null)
                    {
                        ReverceReturnToPickUpPoint();

                        //   CalculateAutoFares();
                    }

                }
                else if (gridCell.ColumnInfo.Name == "ColRervD")
                {
                    if (txtReturnToAddress.Text != "" || ddlReturnToLocation.SelectedValue != null)
                    {
                        ReverceReturnToDestination();
                        //  CalculateAutoFares();
                    }
                }
                else if (gridCell.ColumnInfo.Name == "ColMoveUp")
                {
                    MoveReturnRow(true);
                }
                else if (gridCell.ColumnInfo.Name == "ColMoveDown")
                {
                    MoveReturnRow(false);
                }
            }
            catch
            {


            }

        }

        private void MoveReturnRow(bool moveUp)
        {
            try
            {
                GridViewRowInfo currentRow = this.grdReturnVia.CurrentRow;
                if (currentRow == null)
                {
                    return;
                }



                int index = moveUp ? currentRow.Index - 1 : currentRow.Index + 1;

                int newIndex = currentRow.Index;



                if (moveUp == false)
                {
                    if (newIndex == -1)
                        newIndex = 0;


                    if (index == 0 && newIndex == 0)
                        newIndex = 1;

                }

                if (index < 0 || index >= this.grdReturnVia.RowCount)
                {
                    return;
                }
                this.grdReturnVia.Rows.Move(index, newIndex);
                this.grdReturnVia.CurrentRow = this.grdReturnVia.Rows[index];
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



                int? vehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();

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



            //   bool IsZoneWise = false;


            //if (ddlFromLocType.Items.Count(c => c.Text == "Zone") > 0)
            //    IsZoneWise = true;



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
                    bool IsCompanyFareExists = false;
                    string estimatedTimes = string.Empty;

                    fareVal += General.GetFareRate(ObjBookiing.SubcompanyId.ToInt(), companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, false, pickupdateTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExists, ref estimatedTimes);




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
            bool IsCompanyFareExist = false;
            string estimatedTime = string.Empty;

            fareVal += General.GetFareRate(ObjBookiing.SubcompanyId.ToInt(), companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, false, pickupdateTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime);



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
                txtCustomerNameViaPoint.Text = string.Empty;
                txtMobileNoViaPoint.Text = string.Empty;

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
                txtCustomerNameViaPoint.Text = string.Empty;
                txtMobileNoViaPoint.Text = string.Empty;

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

                txtCustomerNameViaPoint.Text = string.Empty;
                txtMobileNoViaPoint.Text = string.Empty;

                ddlViaLocation.Visible = true;
                lblViaLoc.Text = "Via Location";
                ComboFunctions.FillLocationsCombo(ddlViaLocation, c => c.LocationTypeId == locTypeId);

            }
        }


        private void FillReturnViaLocations()
        {


            int locTypeId = ddlReturnViaFromLocType.SelectedValue.ToInt();

            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
            {
                lblReturnViaLoc.Text = "Via Address";
                txtReturnViaAddress.Visible = true;

                ddlReturnViaLocation.SelectedValue = null;
                ddlReturnViaLocation.Visible = false;

                txtReturnviaPostCode.Text = string.Empty;
                txtReturnviaPostCode.Visible = false;
                txtCustomerNameReturnViaPoint.Text = string.Empty;
                txtMobileNoReturnViaPoint.Text = string.Empty;

                if (locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    txtReturnViaAddress.TextChanged -= new EventHandler(TextReturnBoxElement_TextChanged);
                    txtReturnViaAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                    txtReturnViaAddress.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);
                }


            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtReturnViaAddress.Text = string.Empty;
                txtReturnViaAddress.Visible = false;
                txtCustomerNameReturnViaPoint.Text = string.Empty;
                txtMobileNoReturnViaPoint.Text = string.Empty;

                ddlReturnViaLocation.SelectedValue = null;
                ddlReturnViaLocation.Visible = false;

                txtReturnviaPostCode.Visible = true;


                lblReturnViaLoc.Text = "Via PostCode";



            }


            else
            {
                txtReturnviaPostCode.Text = string.Empty;
                txtReturnviaPostCode.Visible = false;

                txtReturnViaAddress.Text = string.Empty;
                txtReturnViaAddress.Visible = false;
                txtCustomerNameReturnViaPoint.Text = string.Empty;
                txtMobileNoReturnViaPoint.Text = string.Empty;


                ddlReturnViaLocation.Visible = true;
                lblReturnViaLoc.Text = "Via Location";
                ComboFunctions.FillLocationsCombo(ddlReturnViaLocation, c => c.LocationTypeId == locTypeId);

            }
        }


        void ddlViaLocation_OnRefreshing(object sender, EventArgs e)
        {
            FillViaLocations();
        }
        private void DisplayBooking_ViaLocations(IList<Booking_ViaLocation> list)
        {
            if (list.Count > 0)
            {
                CreateViaPanel();

                grdVia.Rows.Clear();

                GridViewRowInfo row = null;
             //   grdVia.BeginUpdate();
                foreach (var item in list)

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
              //  grdVia.CurrentRow = null;

                grdVia.EndUpdate();


                ClearViaDetails();

                // radToggleButton1.Text = "Show Via Point(" + grdVia.Rows.Count + ")";


                btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                btnSelectVia.ButtonElement.ButtonFillElement.BackColor = Color.DarkOrange;
                btnSelectVia.ButtonElement.ButtonFillElement.NumberOfColors = 1;
                pnlVia.Visible = false;
                ViaPoint = "Yes";
            }
            else
            {
                ViaPoint = "No";
            }

        }


        private void DisplayReturnBooking_ViaLocations(IList<Booking_ViaLocation> list)
        {
            if (list.Count > 0)
            {
                CreateReturnViaPanel();

                grdReturnVia.Rows.Clear();

                GridViewRowInfo row = null;
              //  grdReturnVia.BeginUpdate();
                foreach (var item in list)
                {
                    row = grdReturnVia.Rows.AddNew();
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


                grdReturnVia.CurrentRow = null;
             //   grdReturnVia.EndUpdate();


                ClearReturnViaDetails();

                // radToggleButton1.Text = "Show Via Point(" + grdVia.Rows.Count + ")";


                btnReturnSelectVia.Text = "Show Via Point(" + grdReturnVia.Rows.Count + ")";
                btnReturnSelectVia.ButtonElement.ButtonFillElement.BackColor = Color.DarkOrange;
                btnReturnSelectVia.ButtonElement.ButtonFillElement.NumberOfColors = 1;
                pnlReturnVia.Visible = false;
                ViaPoint = "Yes";
            }
            else
            {
                ViaPoint = "No";
            }

        }

        private void chkCancelBooking_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                //dtpStartingCancel.Enabled = true;
                //dtpEndingCancel.Enabled = true;
                gbCancelBooking.Visible = true;
            }
            else
            {
                gbCancelBooking.Visible = false;
                //dtpStartingCancel.Enabled = false;
                //dtpEndingCancel.Enabled = false;
            }
        }

        private void btnApplyCancelChanges_Click(object sender, EventArgs e)
        {
            ApplyCancelChanges();
        }
        private void ApplyCancelChanges()
        {
            try
            {
                if (chkCancelBooking.Checked)
                {
                    DateTime startingAt = dtpStartingCancel.Value.ToDate();

                    DateTime endingAt = dtpEndingCancel.Value.ToDate();

                    if (startingAt == null)
                    {
                        ENUtils.ShowMessage("Beginning Date can't null");
                        return;
                    }
                    if (endingAt == null)
                    {
                        ENUtils.ShowMessage("Ending Date can't null");
                        return;
                    }
                    if (startingAt > endingAt)
                    {
                        ENUtils.ShowMessage("Beginning Date must be less than Ending Date");
                        return;

                    }
                    foreach (var gRow in grdBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == "" && c.Cells[COLS.PickupDate].Value.ToDate() >= startingAt && c.Cells[COLS.PickupDate].Value.ToDate() <= endingAt))
                    {
                        if (gRow.Cells[COLS.BookingStatus].Value.ToIntorNull() != Enums.BOOKINGSTATUS.DISPATCHED)
                        {
                            gRow.Cells[COLS.BookingStatus].Value = Enums.BOOKINGSTATUS.CANCELLED;
                            gRow.Cells[COLS.CHANGED].Value = "2";
                        }
                        //gRow.Cells[COLS.PickupDate].Style.BackColor = Color.Red;
                    }
                }
                else
                {
                    string BookingId = string.Empty;

                    if (grdBookings != null && grdBookings.Rows.Count > 0)
                    {
                        // via += string.Join("|", grdVia.Rows.Select(c => General.GetPostCodeMatch(c.Cells["VIALOCATIONVALUE"].Value.ToStr().ToUpper()) + ", UK").ToArray<string>());
                        // BookingId += string.Join(",", grdBookings.Rows.Select(c => c.Cells["ID"].Value.ToStr()).ToArray<string>());
                        for (int i = 0; i < grdBookings.Rows.Count; i++)
                        {
                            if (grdBookings.Rows[i].Cells[COLS.BookingStatus].Value.ToIntorNull() != Enums.BOOKINGSTATUS.DISPATCHED)
                            {
                                grdBookings.Rows[i].Cells[COLS.CHANGED].Value = "2";
                                grdBookings.Rows[i].Cells[COLS.BookingStatus].Value = Enums.BOOKINGSTATUS.CANCELLED;
                            }
                            //   grdBookings.Rows[i].Cells[COLS.PickupDate].Style.BackColor = Color.Red;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        private void ApplyReturnCancelChanges()
        {
            try
            {
                if (chkReturnCancelBooking.Checked)
                {
                    DateTime startingAt = dtpReturnStartingCancel.Value.ToDate();

                    DateTime endingAt = dtpReturnEndingCancel.Value.ToDate();

                    if (startingAt == null)
                    {
                        ENUtils.ShowMessage("Beginning Date can't null");
                        return;
                    }
                    if (endingAt == null)
                    {
                        ENUtils.ShowMessage("Ending Date can't null");
                        return;
                    }
                    if (startingAt > endingAt)
                    {
                        ENUtils.ShowMessage("Beginning Date must be less than Ending Date");
                        return;

                    }
                    foreach (var gRow in grdReturnBookings.Rows.Where(c => c.Cells[COLS.EXCLUDED].Value.ToStr() == "" && c.Cells[COLS.PickupDate].Value.ToDate() >= startingAt && c.Cells[COLS.PickupDate].Value.ToDate() <= endingAt))
                    {
                        if (gRow.Cells[COLS.BookingStatus].Value.ToIntorNull() != Enums.BOOKINGSTATUS.DISPATCHED)
                        {
                            gRow.Cells[COLS.BookingStatus].Value = Enums.BOOKINGSTATUS.CANCELLED;
                            gRow.Cells[COLS.CHANGED].Value = "2";
                        }
                        //gRow.Cells[COLS.PickupDate].Style.BackColor = Color.Red;
                    }
                }
                else
                {
                    string BookingId = string.Empty;

                    if (grdReturnBookings != null && grdReturnBookings.Rows.Count > 0)
                    {
                        // via += string.Join("|", grdVia.Rows.Select(c => General.GetPostCodeMatch(c.Cells["VIALOCATIONVALUE"].Value.ToStr().ToUpper()) + ", UK").ToArray<string>());
                        // BookingId += string.Join(",", grdBookings.Rows.Select(c => c.Cells["ID"].Value.ToStr()).ToArray<string>());
                        for (int i = 0; i < grdReturnBookings.Rows.Count; i++)
                        {
                            if (grdReturnBookings.Rows[i].Cells[COLS.BookingStatus].Value.ToIntorNull() != Enums.BOOKINGSTATUS.DISPATCHED)
                            {
                                grdReturnBookings.Rows[i].Cells[COLS.CHANGED].Value = "2";
                                grdReturnBookings.Rows[i].Cells[COLS.BookingStatus].Value = Enums.BOOKINGSTATUS.CANCELLED;
                            }
                            //   grdBookings.Rows[i].Cells[COLS.PickupDate].Style.BackColor = Color.Red;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        public void UpdateCancelChanges()
        {
            bool IsSuccess = false;


            try
            {
                //for (int i = 0; i < grdBookings.Rows.Count; i++)
                //{
                //    if (grdBookings.Rows[i].Cells[COLS.CHANGED].Value.ToStr().Trim() == ""
                //        || grdBookings.Rows[i].Cells[COLS.PickupDate].Style.BackColor == Color.Gainsboro)
                //        continue;
                //    else
                //    {




                //    }



                //}


                string BookingId = string.Empty;

                if (grdBookings != null && grdBookings.Rows.Count > 0)
                {
                    //if (grdReturnBookings.Rows[i].Cells[COLS.BookingStatus].Value.ToIntorNull() == Enums.BOOKINGSTATUS.CANCELLED)
                    //{
                    string query = string.Empty;
                    foreach (var item in grdBookings.Rows.Where(c => c.Cells[COLS.CHANGED].Value.ToStr() == "2"))
                    {

                        if (query == string.Empty)
                        {
                            query = item.Cells[COLS.ID].Value.ToStr();
                        }
                        else
                        {
                            query += "," + item.Cells[COLS.ID].Value.ToStr();
                        }
                    }
                    if (query.Length > 0)
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            string FinalQuery = "update Booking set BookingStatusId=3 where masterjobid is  null and Id in (" + query + ")";
                            db.stp_RunProcedure(FinalQuery);
                            IsSuccess = true;
                        }
                    }
                }



                if (IsSuccess)
                    Close();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }


        }

        public void UpdateReturnCancelChanges()
        {
            bool IsSuccess = false;
            try
            {
                //for (int i = 0; i < grdReturnBookings.Rows.Count; i++)
                //{
                //    if (grdReturnBookings.Rows[i].Cells[COLS.CHANGED].Value.ToStr().Trim() == ""
                //        || grdReturnBookings.Rows[i].Cells[COLS.PickupDate].Style.BackColor == Color.Gainsboro)
                //        continue;
                //    else
                //    {





                //    }



                //}


                string BookingId = string.Empty;

                if (grdReturnBookings != null && grdReturnBookings.Rows.Count > 0)
                {
                    //if (grdReturnBookings.Rows[i].Cells[COLS.BookingStatus].Value.ToIntorNull() == Enums.BOOKINGSTATUS.CANCELLED)
                    //{
                    string query = string.Empty;
                    foreach (var item in grdReturnBookings.Rows.Where(c => c.Cells[COLS.CHANGED].Value.ToStr() == "2"))
                    {

                        if (query == string.Empty)
                        {
                            query = item.Cells[COLS.ID].Value.ToStr();
                        }
                        else
                        {
                            query += "," + item.Cells[COLS.ID].Value.ToStr();
                        }
                    }
                    if (query.Length > 0)
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            string FinalQuery = "update Booking set BookingStatusId=3 where masterjobid is not null and Id in (" + query + ")";
                            db.stp_RunProcedure(FinalQuery);
                            IsSuccess = true;
                        }
                    }
                }

                if (IsSuccess)
                    Close();


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            UpdateCancelChanges();
            DisplayBookings();
        }

        private void chkCustomerPrice_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                numCustomerPrice.Enabled = true;
            }
            else
            {
                numCustomerPrice.Enabled = false;
            }
        }


        private void chkCompanyPrice_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                numCompanyPrice.Enabled = true;
            }
            else
            {
                numCompanyPrice.Enabled = false;
            }
        }


        private void chkDayWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                groupDayWise.Visible = true;
                dtpPickupTime.Enabled = false;
                chkPickupTime.Checked = false;
            }
            else
            {
                groupDayWise.Visible = false;
                chkPickupTime.Checked = true;
                dtpPickupTime.Enabled = true;
            }
        }

        private void ddlPaymentType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (IsPaymentTypeLoaded == false) return;
                int PaymentId = ddlPaymentType.SelectedValue.ToInt();
                if (PaymentId == Enums.PAYMENT_TYPES.CASH)
                {
                    ddlAccount.SelectedValue = null;
                    //ddlAccount.Enabled = false;
                }

            }
            catch (Exception ex)
            { }
        }


        private void ddlReturnPaymentType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (IsPaymentTypeLoaded == false) return;
                int PaymentId = ddlReturnPaymentType.SelectedValue.ToInt();
                if (PaymentId == Enums.PAYMENT_TYPES.CASH)
                {
                    ddlReturnAccount.SelectedValue = null;
                    //ddlAccount.Enabled = false;
                }

            }
            catch (Exception ex)
            { }
        }


        private void ddlAccount_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (IsPaymentTypeLoaded == false) return;
                int AccountId = ddlAccount.SelectedValue.ToInt();
                int accountTypeId = General.GetObject<Gen_Company>(c => c.Id == AccountId).DefaultIfEmpty().AccountTypeId.ToInt();
                if (accountTypeId == Enums.ACCOUNT_TYPE.CASH)
                {
                    //  ddlCompany.SelectedValue = null;
                    //  chkIsCompanyRates.Checked = false;
                    ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;
                }
                else
                {
                    ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.BANK_ACCOUNT;
                }
            }
            catch (Exception ex)
            {
            }
        }



        private void ddlReturnAccount_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (IsPaymentTypeLoaded == false) return;
                int AccountId = ddlReturnAccount.SelectedValue.ToInt();
                int accountTypeId = General.GetObject<Gen_Company>(c => c.Id == AccountId).DefaultIfEmpty().AccountTypeId.ToInt();
                if (accountTypeId == Enums.ACCOUNT_TYPE.CASH)
                {
                    //  ddlCompany.SelectedValue = null;
                    //  chkIsCompanyRates.Checked = false;
                    ddlReturnPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;

                }
                else
                {
                    ddlReturnPaymentType.SelectedValue = Enums.PAYMENT_TYPES.BANK_ACCOUNT;
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void btnReturnRevertChanges_Click(object sender, EventArgs e)
        {
            DisplayReturnBooking();
        }

        private void btnReturnApplyChanges_Click(object sender, EventArgs e)
        {
            ApplyReturnChanges();
        }

        private void chkReturnDayWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                groupReturnDayWise.Visible = true;
            }
            else
            {
                groupReturnDayWise.Visible = false;
            }
        }

        private void chkReturnStartingAt_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpReturnStartingAt.Enabled = false;
            }
            else
            {
                dtpReturnStartingAt.Enabled = true;
            }
        }

        private void chkReturnPickupTime_ToggleStateChanged_1(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpReturnPickupTime.Enabled = true;
            }
            else
            {
                dtpReturnPickupTime.Enabled = false;
            }
        }

        private void chkReturnPickupFares_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                numReturnPickupFares.Enabled = true;
            }
            else
            {
                numReturnPickupFares.Enabled = false;
            }
        }

        private void chkReturnCustomerPrice_ToggleStateChanged_1(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                numReturnCustomerPrice.Enabled = true;
            }
            else
            {
                numReturnCustomerPrice.Enabled = false;
            }
        }

        private void chkReturnCompanyPrice_ToggleStateChanged_1(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                numReturnCompanyPrice.Enabled = true;
            }
            else
            {
                numReturnCompanyPrice.Enabled = false;
            }
        }

        private void chkReturnEndingAt_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                dtpReturnEndingAt.Enabled = false;
            }
            else
            {
                dtpReturnEndingAt.Enabled = true;
            }
        }

        private void chkReturnPickup_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetReturnFromAddress(args.ToggleState);
        }

        private void chkReturnDestination_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetReturnToAddress(args.ToggleState);
        }

        private void chkReturnCancelBooking_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                pnlCancelReturnBooking.Visible = true;
            }
            else
            {
                pnlCancelReturnBooking.Visible = false;
            }

        }

        private void btnReturnSaveChanges_Click(object sender, EventArgs e)
        {
            UpdateReturnCancelChanges();
            DisplayBookings();
        }

        private void btnReturnApplyCancelChanges_Click(object sender, EventArgs e)
        {
            ApplyReturnCancelChanges();
        }

        private void btnReturnExclude_Click(object sender, EventArgs e)
        {
            ExcludeReturnBooking(grdReturnPickupDates.CurrentRow);
            //grdReturnPickupDates.CurrentRow.Delete();
        }

        private void btnReturnInclude_Click(object sender, EventArgs e)
        {
            IncludeReturnBooking(grdExcludeReturnPickupDates.CurrentRow);
            //grdExcludeReturnPickupDates.CurrentRow.Delete();
        }

        private void chkAllocateOnewayDrv_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlDriver.Enabled = true;

            }
            else
            {
                ddlDriver.SelectedValue = null;
                ddlDriver.Enabled = false;

            }
        }

        private void chkReturnAllocateDrv_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlReturnDriver.Enabled = true;

            }
            else
            {
                ddlReturnDriver.SelectedValue = null;
                ddlReturnDriver.Enabled = false;

            }
        }

        private void chkShowOneWayDispatchedBookings_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                //grdBookings.Rows.Where(c => c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
                //   || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
                //   || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ARRIVED
                //   || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.POB
                //   || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.STC
                //    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.CANCELLED
                //     || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.NOPICKUP
                //      || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.STC
                //       || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.STC

                //   || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED).ToList().ForEach(c => c.IsVisible = true);


                grdBookings.Rows.Where(c => c.Cells[COLS.StatusId].Value.ToInt() != Enums.BOOKINGSTATUS.WAITING
                                        ).ToList().ForEach(c => c.IsVisible = true);
            }
            else
            {
                //grdBookings.Rows.Where(c => c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
                //    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
                //    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ARRIVED
                //    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.POB
                //    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.STC
                //    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED).ToList().ForEach(c => c.IsVisible = false);

                grdBookings.Rows.Where(c => c.Cells[COLS.StatusId].Value.ToInt() != Enums.BOOKINGSTATUS.WAITING
                     ).ToList().ForEach(c => c.IsVisible = false);
            }
        }

        private void chkShowReturnDispatchedBookings_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                grdReturnBookings.Rows.Where(c => c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ARRIVED
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.POB
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.STC
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED).ToList().ForEach(c => c.IsVisible = true);
            }
            else
            {
                grdReturnBookings.Rows.Where(c => c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ONROUTE
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.ARRIVED
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.POB
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.STC
                    || c.Cells[COLS.StatusId].Value.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED).ToList().ForEach(c => c.IsVisible = false);
            }
        }




        #region TextChanged

        void TextBoxElement_TextChanged(object sender, EventArgs e)
        {


            try
            {

                IsKeyword = false;

                InitializeTimer();
                timer1.Stop();

                aTxt = (AutoCompleteTextBox)sender;
                aTxt.ResetListBox();

                if (aTxt.Name == "txtFromAddress")
                    txtToAddress.SendToBack();

                else if (aTxt.Name == "txtToAddress")
                    txtToAddress.BringToFront();



                if (EnablePOI)
                {

                    InitializeSearchPOIWorker();

                    if (POIWorker.IsBusy)
                    {
                        POIWorker.CancelAsync();

                        POIWorker.Dispose();
                        POIWorker = null;
                        GC.Collect();
                        InitializeSearchPOIWorker();

                    }


                    AddressTextChangePOI();
                }
                else
                {

                    AddressTextChangeWOPOI();
                }
            }
            catch (Exception ex)
            {

            }
        }

        BackgroundWorker POIWorker = null;
        private void InitializeSearchPOIWorker()
        {
            if (POIWorker == null)
            {
                POIWorker = new BackgroundWorker();
                POIWorker.WorkerSupportsCancellation = true;
                POIWorker.DoWork += new DoWorkEventHandler(POIWorker_DoWork);
                POIWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(POIWorker_RunWorkerCompleted);
            }



        }

        void POIWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Result == null || e.Cancelled || (sender as BackgroundWorker) == null)
                    return;

                try
                {


                    ShowAddressesPOI((string[])e.Result);

                }
                catch
                {


                }

            }catch
            {

            }
        }

        void POIWorker_DoWork(object sender, DoWorkEventArgs e)
        {


            string searchValue = e.Argument.ToStr();
            try
            {
                if (POIWorker == null)
                {
                    e.Cancel = true;
                    return;


                }

                //   Console.WriteLine("Start work : " + searchValue);

                string postCode = General.GetPostCodeMatchOpt(searchValue);

                string doorNo = string.Empty;
                string place = string.Empty;

                if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                    postCode = string.Empty;

                string street = searchValue;

                if (postCode.Length > 0)
                {
                    street = street.Replace(postCode, "").Trim();
                }


                if (!string.IsNullOrEmpty(street) && !string.IsNullOrEmpty(postCode) && street.IsAlpha() == false && street.Length < 4 && searchValue.IndexOf(postCode) < searchValue.IndexOf(street))
                {
                    street = "";
                    postCode = searchTxt;
                }


                if (street.Length > 0)
                {

                    if (char.IsNumber(street[0]))
                    {

                        for (int i = 0; i <= 3; i++)
                        {
                            if (char.IsNumber(street[i]) || (doorNo.Length > 0 && doorNo.Length == i && char.IsLetter(street[i])))
                                doorNo += street[i];
                            else
                                break;
                        }
                    }
                }


                if (street.EndsWith("#"))
                {
                    street = street.Replace("#", "").Trim();
                    place = "p=";
                }

                if (doorNo.Length > 0 && place.Length == 0)
                {
                    street = street.Replace(doorNo, "").Trim();


                }


                if (postCode.Length == 0 && street.Length < 3)
                {
                    e.Cancel = true;
                    return;

                }


                if (street.Length > 1 || postCode.Length > 0)
                {
                    if (postCode.Length > 0)
                    {
                        if (doorNo.Length > 0 && postCode == General.GetPostCodeMatch(postCode))
                        {
                            doorNo = string.Empty;
                        }

                    }

                    if (postCode.Length >= 5 && postCode.Contains(" ") == false)
                    {
                        string resultPostCode = AppVars.listOfAddress.FirstOrDefault(a => a.PostalCode.Strip(' ') == postCode).DefaultIfEmpty().PostalCode.ToStr().Trim();


                        if (resultPostCode.Length >= 5 && resultPostCode.Contains(" "))
                        {
                            postCode = resultPostCode;

                        }

                    }


                    if (POIWorker == null || POIWorker.CancellationPending || ((sender as BackgroundWorker) == null || (sender as BackgroundWorker).CancellationPending))
                    {
                        e.Cancel = true;
                        return;
                    }



                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        e.Result = db.stp_GetByRoadLevelData(postCode, doorNo, street, place).Select(c => c.AddressLine1).ToArray<string>();

                    }

                    if (POIWorker == null || POIWorker.CancellationPending || ((sender as BackgroundWorker) == null || (sender as BackgroundWorker).CancellationPending))
                    {
                        e.Cancel = true;
                        return;
                    }




                    //   Console.WriteLine("end work : " + searchValue);

                }
            }
            catch
            {
                //     Console.WriteLine("Start work catch: " + searchValue);

            }
        }



        private void AddressTextChangeWOPOI()
        {
            string text = aTxt.Text;
            string doorNo = string.Empty;

            if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool())
            {
                if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower())
                {
                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();


                    if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                    {

                        aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                    }

                    aTxt.SelectedItem = aTxt.Text.Trim();
                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    //    }               

                }

            }

            if (text.Length > 2 && text.EndsWith(".") == false && text.EndsWith(",") == false)
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
                    text = text.Remove(text.IndexOf(doorNo), doorNo.Length).TrimStart(new char[] { ' ' });
                }
            }



            if (text.Length > 1 && text != "BASX")
            {
                if (text.EndsWith("   "))
                {
                    //if (aTxt.Name == "txtFromAddress")
                    //{
                    //    FocusOnPickupPlot();
                    //}
                    //else if (aTxt.Name == "txtToAddress")
                    //{
                    //    FocusOnDropOffPlot();
                    //}

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
                    Gen_Location loc = null;
                    if (AppVars.keyLocations.Contains(formerValue) || aTxt.FormerValue.EndsWith("  ")
                    || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 3 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                    {


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
                            RadComboBoxItem item = (RadComboBoxItem)ddlFromLocation.Items.FirstOrDefault(b => b.Text.ToUpper().Equals(aTxt.SelectedItem.ToUpper()));
                            if (item != null)
                            {
                                ddlFromLocation.SelectedValue = item.Value;
                                //if (commaIndex > 0 && ddlFromLocation.Text.ToUpper() != item.Text.ToUpper())
                                //{

                                //    SetPickupZone(item.Text);

                                //}


                                //if (loc != null && loc.ZoneId != null && ddlPickupPlot.SelectedValue == null)
                                //{
                                //    ddlPickupPlot.SelectedValue = loc.ZoneId;
                                //}




                                //if (ddlFromLocation.SelectedValue != null && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                                //{
                                //    UpdateAutoCalculateFares();

                                //}

                            }
                            else
                            {
                                if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                                {
                                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                    aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                    aTxt.Text = aTxt.Text.Trim();
                                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                                    //if (aTxt.Name == "txtFromAddress")
                                    //{
                                    //    SetPickupZone(aTxt.Text);

                                    //    UpdateAutoCalculateFares();
                                    //}


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
                            RadComboBoxItem item = (RadComboBoxItem)ddlToLocation.Items.FirstOrDefault(b => b.Text.ToUpper().Equals(aTxt.SelectedItem.ToUpper()));
                            if (item != null)
                            {
                                ddlToLocation.SelectedValue = item.Value;

                                //if (commaIndex > 0 && ddlToLocation.Text.ToUpper() != item.Text.ToUpper())
                                //{
                                //    SetDropOffZone(item.Text);

                                //}

                                //if (loc != null && loc.ZoneId != null && ddlDropOffPlot.SelectedValue == null)
                                //{
                                //    ddlDropOffPlot.SelectedValue = loc.ZoneId;
                                //}

                                //if (ddlToLocation.SelectedValue != null && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                                //{
                                //    UpdateAutoCalculateFares();

                                //}

                            }
                            else
                            {
                                if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                                {
                                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                    aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                    aTxt.Text = aTxt.Text.Trim();
                                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                                    //  SetDropOffZone(aTxt.Text);
                                    //  UpdateAutoCalculateFares();


                                }

                            }

                            if (loctypeId == Enums.LOCATION_TYPES.POSTCODE || loctypeId == Enums.LOCATION_TYPES.ADDRESS)
                            {
                                txtToFlightDoorNo.Focus();
                            }
                            //else
                            //{
                            //    ddlCustomerName.Focus();
                            //}
                        }
                    }
                    else if (aTxt.Text.Contains('.'))
                    {

                        //   RemoveNumbering(doorNo);

                        if (aTxt.Name == "txtFromAddress")
                        {

                            //  SetPickupZone(aTxt.SelectedItem);
                            txtFromFlightDoorNo.Focus();

                        }

                        else if (aTxt.Name == "txtToAddress")
                        {
                            //   SetDropOffZone(aTxt.SelectedItem);
                            txtToFlightDoorNo.Focus();

                        }
                    }
                    else if (!string.IsNullOrEmpty(doorNo))
                    {
                        aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                        aTxt.Text = doorNo + " " + aTxt.Text;
                        aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    }
                    //else
                    //{
                    //    if (aTxt.Name == "txtFromAddress")
                    //    {


                    //        SetPickupZone(aTxt.SelectedItem);


                    //    }

                    //    else if (aTxt.Name == "txtToAddress")
                    //    {
                    //        SetDropOffZone(aTxt.SelectedItem);


                    //    }

                    //    if (aTxt.SelectedItem.ToStr().Trim() != string.Empty)
                    //    {
                    //        UpdateAutoCalculateFares();

                    //    }


                    //}

                    aTxt.FormerValue = string.Empty;


                    return;
                }



                if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                {

                    //   CancelWebClientAsync();
                    // wc.CancelAsync();
                    aTxt.Values = null;

                }
                text = text.ToLower();

                if (AppVars.keyLocations.Contains(text) || (text.Length <= 4 && (text.EndsWith("  ") || (text[1] == ' ' || (text.Length > 2 && char.IsLetter(text[1]) && text[2] == ' ' && text.Trim().WordCount() == 2))))
                   || (text.Length < 13 && text.WordCount() == 2 && text.Remove(text.IndexOf(' ')).Trim().Length <= 3 && text.Strip(' ').IsAlpha()))
                {


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
            else if (text.Length > 0)
            {
                if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                {

                    //   CancelWebClientAsync();
                    // wc.CancelAsync();
                    aTxt.Values = null;

                }
                text = text.ToLower();

                if (AppVars.keyLocations.Contains(text))
                {

                    aTxt.ListBoxElement.Items.Clear();


                    string[] res = null;

                    if (text.EndsWith("  "))
                    {

                        text = text.Trim();

                        res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey.ToLower() == text)
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

                        if (text == "." && finalList.Count() == 1)
                        {
                            aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                            aTxt.Text = finalList[0];
                            aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                            if (aTxt.Name == "txtFromAddress")
                            {

                                //  SetPickupZone(aTxt.Text);
                                txtFromFlightDoorNo.Focus();

                            }

                            else if (aTxt.Name == "txtToAddress")
                            {
                                // SetDropOffZone(aTxt.Text);
                                txtToFlightDoorNo.Focus();

                            }
                        }
                        else
                        {

                            aTxt.ShowListBox();
                        }
                    }


                    if (aTxt.Text != aTxt.FormerValue)
                    {
                        aTxt.FormerValue = aTxt.Text;
                    }




                    StartAddressTimer(text);
                }


            }
            else
            {
                if (MapType == Enums.MAP_TYPE.NONE) return;
                aTxt.ResetListBox();
                //  aTxt.ListBoxElement.Visible = false;
                aTxt.ListBoxElement.Items.Clear();

                //   CancelWebClientAsync();
                //  wc.CancelAsync();
                aTxt.Values = null;

            }



        }

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


                if (EnablePOI)
                {

                    if (POIWorker.IsBusy)
                        POIWorker.CancelAsync();



                    POIWorker.RunWorkerAsync(searchTxt);
                }
                else
                {

                    PerformAddressChangeTimerWOPOI();
                }


            }
            catch (Exception ex)
            {


            }

        }


        private void StartAddressTimer(string text)
        {
            text = text.ToLower();
            searchTxt = text;
            InitializeTimer();
            timer1.Start();
        }

        private void InitializeTimer()
        {
            if (this.timer1 == null)
            {
                this.timer1 = new System.Windows.Forms.Timer();
                this.timer1.Tick += timer1_Tick;
                this.timer1.Interval = 500;
            }

        }








        private void AddressTextChangePOI()
        {
            string text = aTxt.Text;
            string doorNo = string.Empty;

            if (aTxt.SelectedItem != null && aTxt.ListBoxElement.SelectedItem != null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower()
               && aTxt.Text.Length > 0)
            {

                if (aTxt.Name == "txtReturnViaAddress")
                {

                    aTxt.TextChanged -= TextReturnBoxElement_TextChanged;

                    aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();

                    if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                    {
                        aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                    }

                    aTxt.SelectedItem = aTxt.Text.Trim();
                    aTxt.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);

                }
                else
                {

                    if (aTxt.Name == "txtReturnFromAddress" || aTxt.Name == "txtReturnToAddress")
                    {
                        aTxt.TextChanged -= TextReturnBoxElement_TextChanged;

                        aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();

                        if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                        {
                            aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                        }

                        aTxt.SelectedItem = aTxt.Text.Trim();
                        aTxt.TextChanged += new EventHandler(TextReturnBoxElement_TextChanged);


                    }
                    else
                    {

                        aTxt.TextChanged -= TextBoxElement_TextChanged;

                        aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();

                        if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                        {
                            aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                        }

                        aTxt.SelectedItem = aTxt.Text.Trim();
                        aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    }
                }
            }




            for (int i = 0; i <= 2; i++)
            {
                if (char.IsNumber(text[i]))
                    doorNo += text[i];
                else
                    break;
            }





            if (text.Length > 1 && text != "BASX")
            {
                if (text.EndsWith("   "))
                {
                    //if (aTxt.Name == "txtFromAddress")
                    //{
                    //    FocusOnPickupPlot();
                    //}
                    //else if (aTxt.Name == "txtToAddress")
                    //{
                    //    FocusOnDropOffPlot();
                    //}
                    //return;
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
                    Gen_Location loc = null;
                    if (AppVars.keyLocations.Contains(formerValue) || aTxt.FormerValue.EndsWith("  ")
                    || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 3 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                    {
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
                            RadComboBoxItem item = (RadComboBoxItem)ddlFromLocation.Items.FirstOrDefault(b => b.Text.ToUpper().Equals(aTxt.SelectedItem.ToUpper()));
                            if (item != null)
                            {
                                ddlFromLocation.SelectedValue = item.Value;
                                //if (commaIndex > 0 && ddlFromLocation.Text.ToUpper() != item.Text.ToUpper())
                                //{
                                //    SetPickupZone(item.Text);
                                //}


                                //if (loc != null && loc.ZoneId != null && ddlPickupPlot.SelectedValue == null)
                                //{
                                //    ddlPickupPlot.SelectedValue = loc.ZoneId;
                                //}

                                //if (ddlFromLocation.SelectedValue != null && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                                //{
                                //    UpdateAutoCalculateFares();

                                //}
                            }
                            else
                            {
                                if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                                {
                                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                    aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                    aTxt.Text = aTxt.Text.Trim();
                                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                                    //if (aTxt.Name == "txtFromAddress")
                                    //{
                                    //    SetPickupZone(aTxt.Text);

                                    //    UpdateAutoCalculateFares();
                                    //}
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
                            RadComboBoxItem item = (RadComboBoxItem)ddlToLocation.Items.FirstOrDefault(b => b.Text.ToUpper().Equals(aTxt.SelectedItem.ToUpper()));
                            if (item != null)
                            {
                                ddlToLocation.SelectedValue = item.Value;

                                //if (commaIndex > 0 && ddlToLocation.Text.ToUpper() != item.Text.ToUpper())
                                //{
                                //    SetDropOffZone(item.Text);
                                //}

                                //if (loc != null && loc.ZoneId != null && ddlDropOffPlot.SelectedValue == null)
                                //{
                                //    ddlDropOffPlot.SelectedValue = loc.ZoneId;
                                //}

                                //if (ddlToLocation.SelectedValue != null && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                                //{
                                //    UpdateAutoCalculateFares();
                                //}
                            }
                            else
                            {
                                if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                                {
                                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                    aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                    aTxt.Text = aTxt.Text.Trim();
                                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                                    //  SetDropOffZone(aTxt.Text);
                                    //   UpdateAutoCalculateFares();
                                }

                            }

                            if (loctypeId == Enums.LOCATION_TYPES.POSTCODE || loctypeId == Enums.LOCATION_TYPES.ADDRESS)
                            {
                                txtToFlightDoorNo.Focus();
                            }
                            //else
                            //{
                            //    ddlCustomerName.Focus();
                            //}
                        }
                    }
                    else if (aTxt.Text.Contains('.'))
                    {
                        // RemoveNumbering(doorNo);

                        if (aTxt.Name == "txtFromAddress")
                        {

                            //  SetPickupZone(aTxt.SelectedItem);
                            txtFromFlightDoorNo.Focus();

                        }

                        else if (aTxt.Name == "txtToAddress")
                        {
                            //  SetDropOffZone(aTxt.SelectedItem);
                            txtToFlightDoorNo.Focus();

                        }
                    }
                    //else if (!string.IsNullOrEmpty(doorNo))
                    //{
                    //    aTxt.TextChanged -= TextBoxElement_TextChanged;
                    //    aTxt.Text = aTxt.Text;
                    //    aTxt.TextChanged += TextBoxElement_TextChanged;
                    //}
                    //else
                    //{
                    //    if (aTxt.Name == "txtFromAddress")
                    //    {
                    //        SetPickupZone(aTxt.SelectedItem);
                    //    }

                    //    else if (aTxt.Name == "txtToAddress")
                    //    {
                    //        SetDropOffZone(aTxt.SelectedItem);

                    //    }

                    //    if (aTxt.SelectedItem.ToStr().Trim() != string.Empty)
                    //    {
                    //        UpdateAutoCalculateFares();
                    //    }
                    //}

                    aTxt.FormerValue = string.Empty;
                    return;
                }

                if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                {

                    aTxt.Values = null;

                }

                text = text.ToLower();

                if (AppVars.keyLocations.Contains(text) || (text.Length <= 4 && (text.EndsWith("  ") || (text[1] == ' ' || (text.Length > 2 && char.IsLetter(text[1]) && text[2] == ' ' && text.Trim().WordCount() == 2))))
                   || (text.Length < 13 && text.WordCount() == 2 && text.Remove(text.IndexOf(' ')).Trim().Length <= 3 && text.Strip(' ').IsAlpha()))
                {


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
            else if (text.Length > 0)
            {
                if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                {

                    aTxt.Values = null;

                }
                text = text.ToLower();

                if (AppVars.keyLocations.Contains(text))
                {

                    aTxt.ListBoxElement.Items.Clear();


                    string[] res = null;

                    if (text.EndsWith("  "))
                    {

                        text = text.Trim();

                        res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey.ToLower() == text)
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

                        if (text == "." && finalList.Count() == 1)
                        {
                            aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                            aTxt.Text = finalList[0];
                            aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                            if (aTxt.Name == "txtFromAddress")
                            {

                                // SetPickupZone(aTxt.Text);
                                txtFromFlightDoorNo.Focus();

                            }

                            else if (aTxt.Name == "txtToAddress")
                            {
                                //  SetDropOffZone(aTxt.Text);
                                txtToFlightDoorNo.Focus();

                            }
                        }
                        else
                        {

                            aTxt.ShowListBox();
                        }
                    }


                    if (aTxt.Text != aTxt.FormerValue)
                    {
                        aTxt.FormerValue = aTxt.Text;
                    }

                    StartAddressTimer(text);
                }


            }
            else
            {
                //if (MapType == Enums.MAP_TYPE.NONE)
                //    return;
                aTxt.ResetListBox();
                aTxt.ListBoxElement.Items.Clear();
                aTxt.Values = null;

            }



        }






        private void ShowAddressesPOI(string[] resValue)
        {
            int sno = 1;

            // var finalList = resValue;



            var finalList = (from a in AppVars.zonesList
                             from b in resValue
                             where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && (b.IndexOf(a) - 1) >= 0 && b[b.IndexOf(a) - 1] == ' ' && GeneralBLL.GetHalfPostCodeMatch(b) == a)

                             select b).ToArray<string>();


            if (finalList.Count() > 0)
            {
                finalList = finalList.Union(resValue).ToArray<string>();

            }
            else
                finalList = resValue;



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


                aTxt.ShowListBox();


            }

            if (searchTxt != aTxt.FormerValue.ToLower())
            {
                aTxt.FormerValue = aTxt.Text;

            }
        }




        private void PerformAddressChangeTimerWOPOI()
        {

            string postCode = General.GetPostCodeMatch(searchTxt);
            string fullPostCode = postCode;


            if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                postCode = string.Empty;


            string street = searchTxt;



            int IsAsc = 0;
            if (!string.IsNullOrEmpty(postCode))
            {
                street = street.Replace(postCode, "").Trim();

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

                    res = null;

                    if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool()
                        && AppVars.zonesList.Count() > 0
                        && fullPostCode.Length > 0)
                    {

                        fullPostCode = General.GetPostCodeMatch(fullPostCode);

                        if (fullPostCode.Length > 0 && searchTxt.Trim() == fullPostCode)
                        {


                            string[] res1 = (from a in AppVars.listOfAddress

                                             where a.PostalCode == postCode

                                             select a.AddressLine1

                                       ).Take(1).ToArray<string>();





                            res = (from a in new TaxiDataContext().stp_GetRoadLevelData(fullPostCode)
                                   select a.AddressLine1).ToArray<string>();


                            res = res1.Union(res).Distinct().ToArray<string>();


                        }


                        if (res.Count() == 0)
                        {
                            res = (from a in AppVars.listOfAddress

                                   where a.PostalCode.StartsWith(postCode)

                                   orderby a.PostalCode

                                   select a.AddressLine1

                                  ).Take(100).ToArray<string>();


                        }




                    }
                    else
                    {

                        res = (from a in AppVars.listOfAddress

                               where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))

                               select a.AddressLine1

                                  ).Take(500).ToArray<string>();
                    }
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




                            if (AppVars.zonesList.Count() == 0)
                            {
                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.StartsWith(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))
                                       select a.AddressLine1

                                ).Take(500).ToArray<string>();
                            }
                            else
                            {
                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.StartsWith(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))
                                       select a.AddressLine1

                               ).Take(100).ToArray<string>();

                            }


                            if (AppVars.zonesList.Count() > 0)
                            {

                                string[] res2 = (from a in AppVars.listOfAddress

                                                 where (a.AddressLine1.StartsWith(street))
                                                 && AppVars.zonesList.Count(c => a.PostalCode.StartsWith(c)) > 0
                                                 select a.AddressLine1

                                    ).Take(200).ToArray<string>();

                                res = res2.Union(res).Distinct().ToArray<string>();


                            }










                        }
                        else
                        {



                            if (AppVars.zonesList.Count() > 0)
                            {




                                if (postCode.Length == 0)
                                {

                                    res = (from a in AppVars.listOfAddress


                                           where

                                           (a.AddressLine1.StartsWith(street))
                                           select a.AddressLine1

                                       ).Take(500).ToArray<string>();
                                }
                                else
                                {
                                    res = (from a in AppVars.listOfAddress


                                           where

                                           ((a.AddressLine1.StartsWith(street))
                                        && ((a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))

                                           select a.AddressLine1

                                      ).Take(500).ToArray<string>();


                                }


                                res = res.Union((from a in AppVars.listOfAddress


                                                 where

                                                 (a.AddressLine1.Contains(street)
                                                 && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))




                                                 select a.AddressLine1

                                   ).Take(2000)
                                     ).Distinct().ToArray<string>();




                            }
                            else
                            {

                                res = (from a in AppVars.listOfAddress

                                       where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))



                                       select a.AddressLine1

                                    ).Take(1000).ToArray<string>();
                            }

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

            ShowAddresses();

        }



        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
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



                    if (txtData.Text.Length > 4 && txtData.ListBoxElement.Visible == true && txtData.ListBoxElement.Items.Count < 10)
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


                        if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool())
                        {
                            txtData.Text = (item.Remove(0, item.IndexOf('.') + 1).Trim()).Trim();
                        }
                        else
                        {

                            txtData.Text = (doorNo + " " + item.Remove(0, item.IndexOf('.') + 1).Trim()).Trim();
                        }


                        //if (txtData.Name == "txtFromAddress")
                        //{
                        //    SetPickupZone(txtData.Text);
                        //    FocusOnFromDoor();
                        //}
                        //else if (txtData.Name == "txtToAddress")
                        //{
                        //    SetDropOffZone(txtData.Text);
                        //    FocusOnToDoor();
                        //}
                        //else if (txtData.Name == "txtViaAddress")
                        //{
                        //    txtData.ResetListBox();
                        //    AddViaPoint();

                        //}
                        txtData.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                        e.Handled = true;

                        aTxt.ResetListBox();
                        aTxt.ListBoxElement.Items.Clear();


                        //   UpdateAutoCalculateFares();
                        //   txtViaAddress.Focus();
                    }



                }
            }
            catch (Exception ex)
            {


            }
        }


        #endregion



        protected void WithInSave(Booking Current)
        {
            string mobNo = Current.CustomerMobileNo.ToStr().Trim();
            string telNo = Current.CustomerPhoneNo.ToStr().Trim();

            string customerName = Current.CustomerName.ToStr().Trim();

            string email = Current.CustomerEmail.ToStr().Trim();

            long id = Current.Id;
            //if (id == 0)
            //{
            //    IsNewbooking = true;

            //    if (Current.BookingTypeId == Enums.BOOKING_TYPES.WEB || Current.BookingTypeId == Enums.BOOKING_TYPES.ONLINE)
            //    {
            //        this.WebRefNo = Current.BookingNo;


            //        if (!string.IsNullOrEmpty(this.WebRefNo) && this.WebRefNo.ToStr().EndsWith("_") == true)
            //        {

            //            if (Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.ONEWAY)
            //            {
            //                Current.BookingNo = this.WebRefNo.Replace("_", "").Trim();

            //                Current.BookingNo = new SysPolicy_DocumentNumberSetupBO().GetWebSingleSequenceNumber(Enums.GEN_DOCUMENTS.BOOKINGNO, this.WebRefNo, Current.SubcompanyId);

            //                if (Current.BookingNo.ToStr().EndsWith("_"))
            //                {
            //                    Current.BookingNo = Current.BookingNo.ToStr().Replace("_", "").Trim();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.THIRDPARTY)
            //            {
            //                Current.BookingNo = new SysPolicy_DocumentNumberSetupBO(this.db.Connection.ConnectionString).GetWebSequenceNumber(Enums.GEN_DOCUMENTS.BOOKINGNO, Current.OnlineBookingId.ToStr(), Current.SubcompanyId);

            //            }
            //            else
            //            {


            //                Current.BookingNo = new SysPolicy_DocumentNumberSetupBO().GetWebSequenceNumber(Enums.GEN_DOCUMENTS.BOOKINGNO, this.WebRefNo, Current.SubcompanyId);

            //            }
            //        }
            //        if (Current.CompanyId != null)
            //        {
            //            Current.CompanyPrice = Current.FareRate.ToDecimal();

            //        }

            //    }
            //    else
            //    {
            //        Current.BookingNo = new SysPolicy_DocumentNumberSetupBO().GetSequenceNumber(Enums.GEN_DOCUMENTS.BOOKINGNO, Current.SubcompanyId);
            //    }

            //    //if (Current.SpecialRequirements.ToStr().Trim().Length > 0)
            //    //    Current.SpecialRequirements = Current.BookingNo.ToStr() + ", " + Current.SpecialRequirements.ToStr().Trim();
            //    //else
            //    //    Current.SpecialRequirements = Current.BookingNo.ToStr();
            //}
            //else
            //{

            if (Current.MasterJobId != null && Current.Booking1 != null)
            {
                Current.Booking1.ReturnFareRate = Current.FareRate.ToDecimal();
                Current.Booking1.ReturnPickupDateTime = Current.PickupDateTime.ToDateTimeorNull();
                Current.Booking1.WaitingMins = Current.CompanyPrice.ToDecimal();
                //  Current.Booking1.customerpri = Current.FareRate.ToDecimal();


            }


            //}

            //if (this.CheckCustomerValidation)
            //{

            //    SaveCustomer(customerName, telNo, mobNo, email);
            //}

            if (Current.BookingStatusId == null)
                Current.BookingStatusId = Enums.BOOKINGSTATUS.WAITING;


            Current.CustomerMobileNo = mobNo;
            Current.CustomerPhoneNo = telNo;

            Current.ParkingCharges = Current.ParkingCharges.ToDecimal();
            Current.WaitingCharges = Current.WaitingCharges.ToDecimal();
            Current.ExtraDropCharges = Current.ExtraDropCharges.ToDecimal();
            Current.MeetAndGreetCharges = Current.MeetAndGreetCharges.ToDecimal();
            Current.CongtionCharges = Current.CongtionCharges.ToDecimal();

            if (Current.PaymentTypeId != Enums.PAYMENT_TYPES.CASH)
                Current.ServiceCharges = 0.00m;

            if (Current.CompanyId == null)
            {
                Current.TotalCharges = Current.FareRate.ToDecimal()
                             + Current.ExtraDropCharges.ToDecimal() + Current.MeetAndGreetCharges.ToDecimal() + Current.CongtionCharges.ToDecimal();
            }
            else
            {
                Current.TotalCharges = Current.CompanyPrice.ToDecimal() + Current.ParkingCharges.ToDecimal() + Current.WaitingCharges.ToDecimal() + Current.ExtraDropCharges.ToDecimal();

            }

            int paymentTypeId = Current.PaymentTypeId.ToInt();
            int? companyId = Current.CompanyId;
            if ((paymentTypeId != Enums.PAYMENT_TYPES.CASH && companyId == null))
            {


                if (Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD || Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                {

                    Gen_Company objCompany = GeneralBLL.GetObject<Gen_Company>(c => c.SysGenId == Enums.SYSGEN_COMPANY.CREDITCARD);
                    if (objCompany != null)
                        Current.CompanyId = objCompany.Id;
                }
                else
                {
                    if (Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.PAYPAL)
                    {
                        Gen_Company objCompany = GeneralBLL.GetObject<Gen_Company>(c => c.CompanyName.ToUpper() == "PAYPAL");

                        if (objCompany != null)
                            Current.CompanyId = objCompany.Id;
                    }
                    else if (Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT)
                    {
                        Gen_Company objCompany = GeneralBLL.GetObject<Gen_Company>(c => c.CompanyName.ToUpper() == "ACCOUNT");

                        if (objCompany != null)
                            Current.CompanyId = objCompany.Id;
                    }
                    else if (Current.PaymentTypeId.ToInt() == 8)
                    {
                        Gen_Company objCompany = GeneralBLL.GetObject<Gen_Company>(c => c.CompanyName.ToUpper() == "Pay by Phone");

                        if (objCompany != null)
                            Current.CompanyId = objCompany.Id;
                    }



                }


                if (Current.CompanyId != null)
                {

                    Current.IsCompanyWise = true;

                }
            }


            if (Current.IsBidding == null)
                Current.IsBidding = false;

            if (Current.InvoicePaymentTypeId == null)
                Current.InvoicePaymentTypeId = Enums.INVOICE_PAYMENTTYPES.UNPAID;


            if (Current.BookingTypeId == null)
            {
                Current.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;
            }


            if (Current.IsQuotation == null)
            {
                Current.IsQuotation = false;
            }


            if (string.IsNullOrEmpty(Current.FromPostCode.ToStr()) || Current.FromAddress.ToStr().Contains(Current.FromPostCode.ToStr()) == false)
            {
                Current.FromPostCode = GeneralBLL.GetPostCodeMatch(Current.FromAddress.ToStr().ToUpper());
            }

            if (string.IsNullOrEmpty(Current.ToPostCode.ToStr()) || Current.ToAddress.ToStr().Contains(Current.ToPostCode.ToStr()) == false)
            {
                Current.ToPostCode = GeneralBLL.GetPostCodeMatch(Current.ToAddress.ToStr());
            }




            Current.FromOther = !string.IsNullOrEmpty(Current.FromPostCode.ToStr()) ? Current.FromAddress.ToStr().Trim().Replace(Current.FromPostCode, "").ToStr().Trim() : Current.FromAddress.ToStr().Trim();
            Current.ToOther = !string.IsNullOrEmpty(Current.ToPostCode.ToStr()) ? Current.ToAddress.ToStr().Trim().Replace(Current.ToPostCode, "").ToStr().Trim() : Current.ToAddress.ToStr().Trim();

            if (Current.Id > 0 && Current.MasterJobId != null && Current.Booking1 != null)
            {
                Current.Booking1.ReturnFareRate = Current.FareRate.ToDecimal();

                Current.Booking1.WaitingMins = Current.CompanyPrice.ToDecimal();

            }


            //  Via 1 : PENTAX H SOUTH HILL AVENUE HARROW HA2 0DU , Via 2 : HEATHROW TERMINAL 4, TW6 2GA , Via 3 : GOURMET BURGER 333 PUTNEY BRIDGE RD, SW15 2PG , Via 4 : VICTORIA ROAD RUISLIP HA4 0AA
            //if (Current.Booking_ViaLocations.Count > 0)
            //{
            //    int cnter = 0;
            //    Current.ViaString = string.Join(",", Current.Booking_ViaLocations.Select(args => "Via " + (++cnter) + " : " + args.ViaLocValue.Replace(",", " ")).ToArray<string>());


            //}

            //if (Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
            //{
            //    CreateReturnJob();
            //}




            if (Current.SubcompanyId == null)
            {
                int subCompanyId = GeneralBLL.GetObject<Gen_SubCompany>(c => c.IsSysGen != null && c.IsSysGen == true).DefaultIfEmpty().Id;

                if (subCompanyId > 0)
                {
                    Current.SubcompanyId = subCompanyId;

                }
            }

        }

    }
}
