using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Utils;
using Telerik.WinControls;
using Taxi_Model;
using Taxi_BLL;
using System.Net;
using UI;
using System.Xml.Linq;
using System.Xml;
using System.Data.Linq;
namespace Taxi_AppMain
{
    public partial class frmMultiVehicleBooking : Form
    {

        public int AdvbookingTypeId = 1;
        private decimal _ReturnCustomerFares;

        public decimal ReturnCustomerFares
        {
            get { return _ReturnCustomerFares; }
            set { _ReturnCustomerFares = value; }
        }



        private string _ReturnSpecialReq;

        public string ReturnSpecialReq
        {
            get { return _ReturnSpecialReq; }
            set { _ReturnSpecialReq = value; }
        }


        private int? _ReturnVehicleTypeId;

        public int? ReturnVehicleTypeId
        {
            get { return _ReturnVehicleTypeId; }
            set { _ReturnVehicleTypeId = value; }
        }


        private Booking _objBooking;

        public Booking ObjBooking
        {
            get { return _objBooking; }
            set { _objBooking = value; }
        }

        public struct COLS
        {
            public static string SNO = "SNO";
            public static string VEHICLETYPEID = "VEHICLETYPEID";
            public static string VEHICLETYPENAME = "VEHICLETYPENAME";

            public static string DistanceMiles = "DistanceMiles";

            public static string FromLocTypeId = "FromLocTypeId";
            public static string FromLocId = "FromLocId";
            public static string FromAddress = "From";
            public static string ToLocTypeId = "ToLocTypeId";
            public static string ToLocId = "ToLocId";
            public static string ToAddress = "To";


            public static string FromPostCode = "FromPostCode";
            public static string FromDoor = "FromDoor";
            public static string FromStreet = "FromStreet";

            public static string ToPostCode = "ToPostCode";
            public static string ToDoor = "ToDoor";
            public static string ToStreet = "ToStreet";



            public static string Fare = "Fare";
            public static string RetFare = "RetFare";
            public static string CompanyPrice = "CompanyPrice";

            public static string DRIVERID = "DRIVERID";
            public static string DRIVERNAME = "DRIVERNAME";        

          
        }


        public frmMultiVehicleBooking(Booking obj)
        {
            InitializeComponent();
            FormatGrid();
      
            grdBookings.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdBookings.RowsChanged += new GridViewCollectionChangedEventHandler(grdBookings_RowsChanged);
            this.ObjBooking = obj;



            ComboFunctions.FillVehicleTypeCombo(ddlVehicleType);
            ComboFunctions.FillDriverNoQueueCombo(ddlDriver);


            ComboFunctions.FillLocationTypeCombo(ddlFromLocType);
            ComboFunctions.FillLocationTypeCombo(ddlToLocType);

         
            ddlVehicleType.SelectedValue = obj.VehicleTypeId;
            ddlDriver.SelectedValue = obj.DriverId;

            numFareRate.Value = obj.FareRate.ToDecimal();
            numReturnFare.Value = obj.ReturnFareRate.ToDecimal();
            numCompanyFares.Value = obj.CompanyPrice.ToDecimal();

            SetLocation(obj.FromLocTypeId, obj.FromDoorNo, obj.FromAddress, obj.FromStreet, obj.FromPostCode, obj.FromLocId,
                         obj.ToLocTypeId, obj.ToDoorNo, obj.ToAddress, obj.ToStreet, obj.ToPostCode, obj.ToLocId);


            this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            lblMap.Text = obj.DistanceString.ToStr();

            if (AppVars.objPolicyConfiguration.MapType.ToInt() == Enums.MAP_TYPE.GOOGLEMAPS)
            {
                UseGoogleMap = true;


                //   this.txtFromAddress.ForceListBoxToUpdate = true;
                //   this.txtToAddress.ForceListBoxToUpdate = true;
            }
           
            timer1.Tick+=new EventHandler(timer1_Tick);

           



            if (AppVars.objPolicyConfiguration != null)
            {

                MapType = AppVars.objPolicyConfiguration.MapType.ToInt();

            }

            this.FormClosing += new FormClosingEventHandler(frmMultiVehicleBooking_FormClosing);


            txtFromAddress.ListBoxElement.Width = txtFromAddress.DefaultWidth;
            txtToAddress.ListBoxElement.Width = txtToAddress.DefaultWidth;
            EnablePOI = AppVars.objPolicyConfiguration.EnablePOI.ToBool();
            txtFromAddress.KeyPress += new KeyPressEventHandler(txtAddress_KeyPress);
            txtToAddress.KeyPress += new KeyPressEventHandler(txtAddress_KeyPress);
        }

        void frmMultiVehicleBooking_FormClosing(object sender, FormClosingEventArgs e)
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
                }
            }
            catch
            {


            }
        }


        private void SetLocation(int? fromLocTypeId, string fromDoorNo, string fromAddress, string fromStreet, string fromPostCode, int? fromLocId,
                                int? toLocTypeId, string ToDoorNo, string ToAddress, string ToStreet, string ToPostCode, int? ToLocId)
        {

            ddlFromLocType.SelectedValue = fromLocTypeId;
            ddlToLocType.SelectedValue = toLocTypeId;

            //if (fromLocTypeId == Enums.LOCATION_TYPES.BASE || fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
            //{
                txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = fromAddress;
                txtFromFlightDoorNo.Text = fromDoorNo;
                txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            //}
            //else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            //{
            //    txtFromPostCode.Text = fromPostCode;
            //    txtFromStreetComing.Text = fromStreet;
            //    txtFromFlightDoorNo.Text = fromDoorNo;
            //}
            //else if (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
            //{
            //    ddlFromLocation.SelectedValue = fromLocId;
            //    txtFromStreetComing.Text = fromStreet;
            //    txtFromFlightDoorNo.Text = fromDoorNo;
            //}
            //else
            //{
            //    ddlFromLocation.SelectedValue = fromLocId;

            //}


            // To

            //if (toLocTypeId == Enums.LOCATION_TYPES.BASE || toLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
            //{
                txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = ToAddress;

                txtToFlightDoorNo.Text = ToDoorNo;

                txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            //}
            //else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            //{
            //    txtToPostCode.Text = ToPostCode;
            //    txtToStreetComing.Text = ToStreet;
            //    txtToFlightDoorNo.Text = ToDoorNo;
            //}
            //else if (toLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
            //{
            //    ddlToLocation.SelectedValue = ToLocId;
            //    //txtFromStreetComing.Text = fromStreet;
            //    //txtFromFlightDoorNo.Text = fromDoorNo;
            //}
            //else
            //{
            //    ddlToLocation.SelectedValue = ToLocId;

            //}



        }

        void grdBookings_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                ReBindSerials();


            }
        }

        private void ReBindSerials()
        {
            int sno=1;
            foreach (GridViewRowInfo row in grdBookings.Rows)
            {
                row.Cells[COLS.SNO].Value = sno++;
                
            }


        }


        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            RadGridView grid = gridCell.GridControl;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {

                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    grid.CurrentRow.Delete();
                }
            }
          
        }

        private void FillFromLocations()
        {
            int locTypeId = ddlFromLocType.SelectedValue.ToInt();
            //if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId==Enums.LOCATION_TYPES.BASE)
            //{
                txtFromAddress.Visible = true;

                ddlFromLocation.SelectedValue = null;
                ddlFromLocation.Visible = false;

                txtFromPostCode.Text = string.Empty;
                txtFromPostCode.Visible = false;

                txtFromFlightDoorNo.Text = string.Empty;
                txtFromFlightDoorNo.Visible = false;

                txtFromStreetComing.Text = string.Empty;
                txtFromStreetComing.Visible = false;

                lblFromDoorFlightNo.Visible = false;
                lblFromStreetComing.Visible = false;

                lblFromLoc.Text = "Pickup Point";
            //}
            //else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            //{
            //    txtFromAddress.Text = string.Empty;
            //    txtFromAddress.Visible = false;

            //    ddlFromLocation.SelectedValue = null;
            //    ddlFromLocation.Visible = false;

            //    txtFromPostCode.Visible = true;
            //    txtFromFlightDoorNo.Visible = true;
            //    txtFromStreetComing.Visible = true;
            //    lblFromDoorFlightNo.Visible = true;
            //    lblFromStreetComing.Visible = true;

            //    lblFromLoc.Text = "From PostCode";

            //    lblFromDoorFlightNo.Text = "From Door No";
            //    lblFromStreetComing.Text = "From Street";

            //}

            //else if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            //{
            //    txtFromAddress.Text = string.Empty;
            //    txtFromAddress.Visible = false;

            //    ddlFromLocation.Visible = true;

            //    txtFromPostCode.Text = string.Empty;
            //    txtFromPostCode.Visible = false;

            //    txtFromFlightDoorNo.Visible = true;
            //    txtFromStreetComing.Visible = true;
            //    lblFromDoorFlightNo.Visible = true;
            //    lblFromStreetComing.Visible = true;

            //    lblFromLoc.Text = "From Location";


            //    lblFromDoorFlightNo.Text = "Flight No";
            //    lblFromStreetComing.Text = "Coming From";
            //    ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);

            //}

            //else
            //{
            //    lblFromLoc.Text = "From Location";

            //    txtFromPostCode.Text = string.Empty;
            //    txtFromPostCode.Visible = false;

            //    txtFromFlightDoorNo.Text = string.Empty;
            //    txtFromFlightDoorNo.Visible = false;

            //    txtFromStreetComing.Text = string.Empty;
            //    txtFromStreetComing.Visible = false;


            //    lblFromDoorFlightNo.Visible = false;
            //    lblFromStreetComing.Visible = false;

            //    txtFromAddress.Text = string.Empty;
            //    txtFromAddress.Visible = false;

            //    ddlFromLocation.Visible = true;
            //    ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);
            //}
        }

        private void FillToLocations()
        {


            int locTypeId = ddlToLocType.SelectedValue.ToInt();

            //if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId==Enums.LOCATION_TYPES.BASE)
            //{
                lblToLoc.Text = "Destination";
                txtToAddress.Visible = true;

                ddlToLocation.SelectedValue = null;
                ddlToLocation.Visible = false;

                txtToPostCode.Text = string.Empty;
                txtToPostCode.Visible = false;

                txtToFlightDoorNo.Text = string.Empty;
                txtToFlightDoorNo.Visible = false;

                txtToStreetComing.Text = string.Empty;
                txtToStreetComing.Visible = false;


                lblToDoorFlightNo.Visible = false;
                lblToStreetComing.Visible = false;
            //}
            //else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            //{
            //    txtToAddress.Text = string.Empty;
            //    txtToAddress.Visible = false;

            //    ddlToLocation.SelectedValue = null;
            //    ddlToLocation.Visible = false;

            //    txtToPostCode.Visible = true;
            //    txtToFlightDoorNo.Visible = true;
            //    txtToStreetComing.Visible = true;
            //    lblToDoorFlightNo.Visible = true;
            //    lblToStreetComing.Visible = true;

            //    lblToLoc.Text = "To PostCode";

            //    lblToDoorFlightNo.Text = "To Door No";
            //    lblToStreetComing.Text = "To Street";

            //}


            //else
            //{
            //    txtToPostCode.Text = string.Empty;
            //    txtToPostCode.Visible = false;

            //    txtToFlightDoorNo.Text = string.Empty;
            //    txtToFlightDoorNo.Visible = false;

            //    txtToStreetComing.Text = string.Empty;
            //    txtToStreetComing.Visible = false;

            //    lblToDoorFlightNo.Visible = false;
            //    lblToStreetComing.Visible = false;

            //    txtToAddress.Text = string.Empty;
            //    txtToAddress.Visible = false;


            //    ddlToLocation.Visible = true;
            //    lblToLoc.Text = "To Location";
            //    ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);

            //}
        }

 

        private void FormatGrid()
        {

            grdBookings.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdBookings.AllowAddNewRow = false;
            grdBookings.ShowGroupPanel = false;
            grdBookings.ShowRowHeaderColumn = false;
            grdBookings.AllowEditRow = false;
            grdBookings.EnableHotTracking = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = "Sno";
            col.Name = COLS.SNO;
            col.Width = 50;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;      
            col.Name = COLS.VEHICLETYPEID;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.DRIVERID;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.DistanceMiles;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Vehicle Type";
            col.Width = 170;
            col.Name = COLS.VEHICLETYPENAME;
            grdBookings.Columns.Add(col);




            // Locations


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromPostCode;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromDoor;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromStreet;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromLocTypeId;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromLocId;
            grdBookings.Columns.Add(col);



            // To

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToPostCode;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToDoor;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToStreet;
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
            col.HeaderText = "Pickup Point"; ;
            col.Name = COLS.FromAddress;
            col.Width = 210;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = COLS.ToAddress;
            col.Width = 210;
            grdBookings.Columns.Add(col);



            //


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.Fare;
            col.Name = COLS.Fare;
            col.Width = 110;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.RetFare;
            col.Name = COLS.RetFare;
            col.IsVisible = false;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "C. Price";
            col.Name = COLS.CompanyPrice;
            col.Width = 110;
            col.IsVisible = true;
            grdBookings.Columns.Add(col);


            
            col = new GridViewTextBoxColumn();
            col.HeaderText = "Driver";
            col.Width = 200;
            col.Name = COLS.DRIVERNAME;
            
            grdBookings.Columns.Add(col);


            grdBookings.AddDeleteColumn();
            grdBookings.Columns["btnDelete"].Width = 80;
       

        }

        private void btnAddBooking_Click(object sender, EventArgs e)
        {
            AddBooking();

        }

        private void AddBooking()
        {

            try
            {
                int? vehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                int? driverId = ddlDriver.SelectedValue.ToIntorNull();
                string vehicleTypeName = ddlVehicleType.Text.ToStr().Trim();
                string driverName = ddlDriver.Text.ToStr().Trim();

                string fromLocName = ddlFromLocation.Text.ToStr().Trim();
                string toLocName = ddlToLocation.Text.ToStr().Trim();

                int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
                int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
                int? fromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
                int? toLocId = ddlToLocation.SelectedValue.ToIntorNull();
                string fromAddress = txtFromAddress.Text.Trim();
                string toAddress = txtToAddress.Text.Trim();
                string fromPostCode = txtFromPostCode.Text.Trim();
                string toPostCode = txtToPostCode.Text.Trim();
                string fromDoorNo = txtFromFlightDoorNo.Text.Trim();
                string fromStreet = txtFromStreetComing.Text.Trim();

                string toDoorNo = txtToFlightDoorNo.Text.Trim();
                string toStreet = txtToStreetComing.Text.Trim();


            //    int fare = numFareRate.SpinElement.Text == string.Empty ? 0 : numFareRate.SpinElement.Text.ToInt();
                string distance = lblMap.Text.ToStr();

                decimal fare = numFareRate.Value.ToDecimal();

                string error = string.Empty;

                if (vehicleTypeId == null)
                {
                    error += "Required : Vehicle Type";
                }


                //if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                //{
                //    fromAddress = fromPostCode;
                //}
                //else if (fromLocTypeId != Enums.LOCATION_TYPES.POSTCODE && fromLocTypeId != Enums.LOCATION_TYPES.ADDRESS && fromLocTypeId != Enums.LOCATION_TYPES.BASE)
                //{
                //    fromAddress = fromLocName;
                //}


                if (string.IsNullOrEmpty(fromAddress.ToStr().Trim()))
                {

                    if (error != string.Empty)
                        error += Environment.NewLine;

                    error += "Required : Pickup Point";

                }



                //if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                //{
                //    toAddress = toPostCode;
                //}
                //else if (toLocTypeId != Enums.LOCATION_TYPES.POSTCODE && toLocTypeId != Enums.LOCATION_TYPES.ADDRESS && toLocTypeId != Enums.LOCATION_TYPES.BASE)
                //{
                //    toAddress = toLocName;
                //}



                if (string.IsNullOrEmpty(toAddress.ToStr().Trim()))
                {

                    if (error != string.Empty)
                        error += Environment.NewLine;

                    error += "Required : Destination";

                }


                if (driverId != null)
                {
                    int rowIndex = grdBookings.CurrentRow != null ? grdBookings.CurrentRow.Index : -1;

                    if (grdBookings.Rows.Where(c => c.Cells[COLS.DRIVERID].Value != null).Count(c => c.Cells[COLS.DRIVERID].Value.ToInt() == driverId
                                        && (rowIndex == -1 || c.Index != rowIndex)) > 0)
                    {
                        if (error != string.Empty)
                            error += Environment.NewLine;

                        error += "Driver already exist..";

                    }

                }




                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;


                }

                GridViewRowInfo row = null;

                int sno = 0;
                GridViewRowInfo lastRow = grdBookings.Rows.LastOrDefault();
                if (lastRow != null)
                {
                    sno = lastRow.Cells[COLS.SNO].Value.ToInt();

                }

                if (grdBookings.CurrentRow != null && grdBookings.CurrentRow is GridViewNewRowInfo)
                {
                    grdBookings.CurrentRow = null;

                }

                if (grdBookings.CurrentRow != null)
                {
                    row = grdBookings.CurrentRow;
                }

                else
                {
                    row = grdBookings.Rows.AddNew();
                    row.Cells[COLS.SNO].Value = ++sno;
                }



                row.Cells[COLS.FromPostCode].Value = fromPostCode;




                row.Cells[COLS.FromLocTypeId].Value = fromLocTypeId;
                row.Cells[COLS.FromLocId].Value = fromLocId;

                row.Cells[COLS.FromAddress].Value = fromAddress;

                row.Cells[COLS.FromDoor].Value = fromDoorNo;
                row.Cells[COLS.FromStreet].Value = fromStreet;

                row.Cells[COLS.DistanceMiles].Value.ToStr();
                row.Cells[COLS.Fare].Value = fare;

                row.Cells[COLS.RetFare].Value = numReturnFare.Value.ToDecimal();

                row.Cells[COLS.CompanyPrice].Value = numCompanyFares.Value.ToDecimal();



                row.Cells[COLS.ToAddress].Value = toAddress;


                row.Cells[COLS.ToPostCode].Value = toPostCode;
                row.Cells[COLS.ToStreet].Value = toStreet;
                row.Cells[COLS.ToDoor].Value = toDoorNo;

                row.Cells[COLS.ToLocTypeId].Value = toLocTypeId;
                row.Cells[COLS.ToLocId].Value = toLocId;

                row.Cells[COLS.VEHICLETYPEID].Value = vehicleTypeId;
                row.Cells[COLS.DRIVERID].Value = driverId;
                row.Cells[COLS.VEHICLETYPENAME].Value = vehicleTypeName;
                row.Cells[COLS.DRIVERNAME].Value = driverName;


                ClearBooking();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        string[] res = null;
        string searchTxt = "";
        bool IsKeyword = false;
        private bool EnablePOI = false;

  
        AutoCompleteTextBox aTxt = null;
        bool IsAutoComplete = false;
        WebClient wc = new WebClient();
        bool IsPOI = false;
     
        private bool IsExistData = false;

   
        private int _MapType;

        public int MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }


        private bool _UseGoogleMap;

        public bool UseGoogleMap
        {
            get { return _UseGoogleMap; }
            set { _UseGoogleMap = value; }
        }


        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {

            if (e.Cancelled)
            {
                return;
            }


            var xmlElm = XElement.Parse(e.Result);


            res = (from elm in xmlElm.Descendants()

                   // where elm.Name == "description"
                   //&& (elm.Value.ToLower().Contains("united kingdom") || elm.Value.ToLower().Contains("uk"))
                   where elm.Name == "formatted_address"
                   select elm.Value).ToArray<string>();


            ShowAddresses();
            }
            catch
            {


            }

        }

        private void ShowAddresses()
        {

            var finalList = (from a in AppVars.zonesList
                             from b in res
                             where b.Contains(a)

                             select b).ToArray<string>();


            if (finalList.Count() > 0)
                finalList = finalList.Union(res).ToArray<string>();

            else
                finalList = res;


            aTxt.ListBoxElement.Items.Clear();
            aTxt.ListBoxElement.Items.AddRange(finalList);


            if (aTxt.ListBoxElement.Items.Count == 0)
                aTxt.ResetListBox();
            else
                aTxt.ShowListBox();


            if (searchTxt != aTxt.FormerValue.ToLower())
            {
                aTxt.FormerValue = aTxt.Text;

            }


        }




        private void ClearBooking()
        {
            if (chkOrigin.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
                txtFromAddress.Text = string.Empty;
                txtFromFlightDoorNo.Text = string.Empty;
                txtFromPostCode.Text = string.Empty;
                txtFromStreetComing.Text = string.Empty;
                ddlFromLocation.SelectedValue = null;

                numFareRate.Value = 0;
                lblMap.Text = string.Empty;
            }

            if (chkDestination.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlToLocType.SelectedValue= Enums.LOCATION_TYPES.ADDRESS;
                txtToAddress.Text = string.Empty;
                txtToFlightDoorNo.Text = string.Empty;
                txtToPostCode.Text = string.Empty;
                txtToStreetComing.Text = string.Empty;
                ddlToLocation.SelectedValue = null;

                numFareRate.Value = 0;
                lblMap.Text = string.Empty;
            }



            grdBookings.CurrentRow = null;
            ddlVehicleType.SelectedValue = AppVars.objPolicyConfiguration.DefaultVehicleTypeId;
            ddlDriver.SelectedValue = null;

            ddlVehicleType.Focus();
           
        }

        private void grdBookings_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            SelectBooking(e.Row);
        }

        private void SelectBooking(GridViewRowInfo row)
        {
            if (row is GridViewDataRowInfo)
            {
                ddlVehicleType.SelectedValue = row.Cells[COLS.VEHICLETYPEID].Value.ToInt();
                ddlDriver.SelectedValue = row.Cells[COLS.DRIVERID].Value.ToInt();


                SetLocation(row.Cells[COLS.FromLocTypeId].Value.ToIntorNull(), row.Cells[COLS.FromDoor].Value.ToStr(), row.Cells[COLS.FromAddress].Value.ToStr(),
                            row.Cells[COLS.FromStreet].Value.ToStr(), row.Cells[COLS.FromPostCode].Value.ToStr(), row.Cells[COLS.FromLocId].Value.ToIntorNull(),

                            row.Cells[COLS.ToLocTypeId].Value.ToIntorNull(), row.Cells[COLS.ToDoor].Value.ToStr(), row.Cells[COLS.ToAddress].Value.ToStr(),
                            row.Cells[COLS.ToStreet].Value.ToStr(), row.Cells[COLS.ToPostCode].Value.ToStr(), row.Cells[COLS.ToLocId].Value.ToIntorNull());

                numFareRate.Value = row.Cells[COLS.Fare].Value.ToDecimal();

                lblMap.Text = row.Cells[COLS.DistanceMiles].Value.ToStr();
            }




        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearBooking();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _Saved;

        public bool Saved
        {
            get { return _Saved; }
            set { _Saved = value; }
        }



        List<DateTime> listOfPickUpDateTime = new List<DateTime>();
        DateTime? nowDate;
        private bool Save()
        {

            bool IsSuccess = true;
            try
            {
                bool enableAutoDespatch = ObjBooking.AutoDespatch.ToBool();

                bool IsCompanyWise = false;

                int? companyId = ObjBooking.CompanyId;

                if (companyId != null)
                {
                    IsCompanyWise = true;
                }


                int? bookingTypeId = Enums.BOOKING_TYPES.LOCAL;

                int? vehicleTypeId = ObjBooking.VehicleTypeId;
                int? paymentTypeId = ObjBooking.PaymentTypeId;
                int totalPassengers = ObjBooking.NoofPassengers.ToInt();
                int totalLuggages = ObjBooking.NoofLuggages.ToInt();
                int totalHandLuggages = ObjBooking.NoofHandLuggages.ToInt();
                string customerName = ObjBooking.CustomerName.ToStr();
                string customerPhoneNo = ObjBooking.CustomerPhoneNo.ToStr();
                string customerMobileNo = ObjBooking.CustomerMobileNo.ToStr().Trim();

                decimal fareRate = ObjBooking.FareRate.ToDecimal();
                decimal companyPrice = ObjBooking.CompanyPrice.ToDecimal();

                int? fromLocTypeId = ObjBooking.FromLocTypeId;
                int? toLocTypeId = ObjBooking.ToLocTypeId;

                int? fromLocId = ObjBooking.FromLocId;
                int? toLocId = ObjBooking.ToLocId;
                int? returnFromLocId =ObjBooking.ReturnFromLocId;



                string fromAddress = ObjBooking.FromAddress;
                string fromDoorNo = ObjBooking.FromDoorNo.ToStr();
                string fromStreet = ObjBooking.FromStreet.ToStr();
                string fromPostCode = ObjBooking.FromPostCode.ToStr();

                string toAddress = ObjBooking.ToAddress;
                string toDoorNo = ObjBooking.ToDoorNo.ToStr();
                string toStreet = ObjBooking.ToStreet.ToStr();
                string toPostCode = ObjBooking.ToPostCode.ToStr();


                DateTime? PickUpDateTime = ObjBooking.PickupDateTime;

                int? journeyTypeId = ObjBooking.JourneyTypeId;

                // AutoDespatch
              
                int? driverId = ddlDriver.SelectedValue.ToIntorNull();
                //


                //decimal extraMile = 0.00m;

                //if (AppVars.objPolicyConfiguration.AutoBookingDueAlert.ToBool())
                //{

                //    // need to comment
                //    if (ObjBooking.FromLocTypeId.ToInt() != Enums.LOCATION_TYPES.AIRPORT)
                //    {
                //        extraMile = General.CalculateDistanceFromBase(ObjBooking.FromAddress.ToStr().Trim().ToUpper());
                //    }

                //}

                BookingBO objMaster = null;

                nowDate = ObjBooking.BookingDate;

                bool AllSuccess = true;


                AdvanceBookingBO objAdvBO = new AdvanceBookingBO();
                objAdvBO.New();
                objAdvBO.Current.CustomerName = customerName;
                objAdvBO.Current.CustomerTelephoneNo = customerPhoneNo;
                objAdvBO.Current.CustomerMobileNo = customerMobileNo;
                objAdvBO.Current.CustomerEmail = ObjBooking.CustomerEmail.ToStr().Trim();
                objAdvBO.Current.FromAddress = ObjBooking.FromAddress.ToStr().Trim();
                objAdvBO.Current.ToAddress = ObjBooking.ToAddress.ToStr().Trim();
                objAdvBO.Current.AdvBookingTypeId = AdvbookingTypeId;


                objAdvBO.Current.AddOn = DateTime.Now;
                objAdvBO.Current.AddLog = AppVars.LoginObj.UserName.ToStr();
                objAdvBO.Current.AddBy = AppVars.LoginObj.LuserId.ToIntorNull();
                
                objAdvBO.Save();
                long? advanceBookingId = objAdvBO.Current.Id;
                foreach (GridViewRowInfo row in grdBookings.Rows)
            	{
		 
	
                    try
                    {
                      
                        objMaster = new BookingBO();
                        objMaster.New();

                        objMaster.Current.CompanyId = companyId;
                        objMaster.Current.IsCompanyWise = IsCompanyWise;

                        objMaster.Current.BookingDate = nowDate;

                        objMaster.Current.BookingTypeId = bookingTypeId;


                        objMaster.Current.SubcompanyId = ObjBooking.SubcompanyId;

                        objMaster.Current.JourneyTypeId = journeyTypeId;
                        objMaster.Current.PaymentTypeId = paymentTypeId;
                      
                        objMaster.Current.CustomerName = customerName;
                        objMaster.Current.CustomerPhoneNo = customerPhoneNo;
                        objMaster.Current.CustomerMobileNo = customerMobileNo;
                        objMaster.Current.CustomerEmail = ObjBooking.CustomerEmail.ToStr().Trim();

                        int FromLocTypeId =row.Cells[COLS.FromLocTypeId].Value.ToInt();
                        int ToLocTypeId = row.Cells[COLS.ToLocTypeId].Value.ToInt();

                        objMaster.Current.FromLocTypeId = FromLocTypeId;
                        objMaster.Current.ToLocTypeId = ToLocTypeId;
                        objMaster.Current.FromLocId = row.Cells[COLS.FromLocId].Value.ToIntorNull();
                        objMaster.Current.ToLocId = row.Cells[COLS.ToLocId].Value.ToIntorNull();
                        objMaster.Current.ReturnFromLocId = returnFromLocId;

                     
                       objMaster.Current.FromAddress = row.Cells[COLS.FromAddress].Value.ToStr().Trim();
                       objMaster.Current.FromDoorNo = row.Cells[COLS.FromDoor].Value.ToStr().Trim();
                       objMaster.Current.FromStreet = row.Cells[COLS.FromStreet].Value.ToStr().Trim();
                       objMaster.Current.FromPostCode = row.Cells[COLS.FromPostCode].Value.ToStr().Trim();


                      
                       objMaster.Current.ToAddress = row.Cells[COLS.ToAddress].Value.ToStr().Trim();
                       objMaster.Current.ToDoorNo = row.Cells[COLS.ToDoor].Value.ToStr().Trim();
                       objMaster.Current.ToStreet = row.Cells[COLS.ToStreet].Value.ToStr().Trim();
                       objMaster.Current.ToPostCode = row.Cells[COLS.ToPostCode].Value.ToStr().Trim();


                       objMaster.Current.DistanceString = lblMap.Text.ToStr().Trim();
                       objMaster.Current.AutoDespatch = ObjBooking.AutoDespatch.ToBool();



                        objMaster.Current.PickupDateTime = PickUpDateTime;

                      

                        objMaster.Current.BookedBy = ObjBooking.BookedBy.ToStr();

                        objMaster.Current.AutoDespatchTime = objMaster.Current.PickupDateTime.Value.AddMinutes(-ObjBooking.DeadMileage.ToDecimal().ToInt()).ToDateTime();
                        objMaster.Current.DeadMileage = ObjBooking.DeadMileage.ToDecimal();



                        objMaster.Current.FareRate = row.Cells[COLS.Fare].Value.ToDecimal();

                        objMaster.Current.ReturnPickupDateTime = ObjBooking.ReturnPickupDateTime.ToDateTimeorNull();
                        objMaster.Current.ReturnFareRate = row.Cells[COLS.RetFare].Value.ToDecimal();
                        objMaster.Current.CompanyPrice = row.Cells[COLS.CompanyPrice].Value.ToDecimal();

                        objMaster.Current.DriverId = row.Cells[COLS.DRIVERID].Value.ToIntorNull();
                        objMaster.Current.VehicleTypeId = row.Cells[COLS.VEHICLETYPEID].Value.ToIntorNull();

                        objMaster.Current.AutoDespatch = enableAutoDespatch;

                        objMaster.Current.IsCommissionWise = ObjBooking.IsCommissionWise.ToBool();
                        objMaster.Current.DriverCommission = ObjBooking.DriverCommission.ToDecimal();
                        objMaster.Current.DriverCommissionType =ObjBooking.DriverCommissionType.ToStr();

                        objMaster.Current.IsQuotation = ObjBooking.IsQuotation.ToBool();


                        objMaster.Current.AgentCommission = ObjBooking.AgentCommission.ToDecimal();
                        objMaster.Current.AgentCommissionPercent = ObjBooking.AgentCommissionPercent;
                        objMaster.Current.JobTakenByCompany = ObjBooking.JobTakenByCompany.ToBool();
                        objMaster.Current.FromFlightNo = ObjBooking.FromFlightNo.ToStr();

                        objMaster.Current.AddOn = DateTime.Now;
                        objMaster.Current.AddLog = AppVars.LoginObj.UserName.ToStr();
                        objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToIntorNull();


                        objMaster.Current.SpecialRequirements = ObjBooking.SpecialRequirements.ToStr();
                        objMaster.ReturnSpecialRequirement = this.ReturnSpecialReq.ToStr();
                        objMaster.ReturnVehicleTypeId = this.ReturnVehicleTypeId;
                        //objMaster.Current.ExtraMile = extraMile;


                        objMaster.Current.WaitingMins = ObjBooking.WaitingMins.ToDecimal();

                        objMaster.Current.CustomerPrice = ObjBooking.CustomerPrice.ToDecimal();
                        objMaster.ReturnCustomerPrice = this.ReturnCustomerFares;
                      

                        objMaster.Current.AutoDespatch = ObjBooking.AutoDespatch.ToBool();
                        objMaster.Current.IsBidding = ObjBooking.IsBidding.ToBool();


                        if (AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool() && AppVars.objPolicyConfiguration.EnablePDA.ToBool())
                        {
                            if (objMaster.Current.FromPostCode.ToStr().Trim() == string.Empty)
                                objMaster.Current.FromPostCode = General.GetPostCodeMatch(objMaster.Current.FromAddress.ToStr().Trim());


                            if (objMaster.Current.FromPostCode.ToStr().Trim() != string.Empty)
                                
                            {
                                if ((LastPickupPostCode == string.Empty || LastPickupPostCode != objMaster.Current.FromPostCode.ToStr().Trim()))
                                {

                                    objMaster.Current.ZoneId = General.GetZoneId(objMaster.Current.FromPostCode);
                                    LastPickupPostCode = objMaster.Current.FromPostCode.ToStr().Trim();
                                    lastPickupZoneId = objMaster.Current.ZoneId;

                                   
                                }
                                else
                                {
                                   
                                    objMaster.Current.ZoneId = lastPickupZoneId;

                                  //  objMaster.Current.ExtraMile = ObjBooking.ExtraMile.ToDecimal();
                                   // objMaster.Current.DeadMileage = ObjBooking.DeadMileage.ToDecimal();
                                }
                            }


                            if (objMaster.Current.ToPostCode.ToStr().Trim() == string.Empty)
                                objMaster.Current.ToPostCode = General.GetPostCodeMatch(objMaster.Current.ToAddress.ToStr().Trim());


                            if (objMaster.Current.ToPostCode.ToStr().Trim() != string.Empty)
                             
                            {
                                if ((LastDropOffPostCode == string.Empty || LastDropOffPostCode != objMaster.Current.ToPostCode.ToStr().Trim()))
                                {

                                    objMaster.Current.DropOffZoneId = General.GetZoneId(objMaster.Current.ToPostCode);
                                    LastDropOffPostCode = objMaster.Current.ToPostCode.ToStr().Trim();
                                    lastDropOffZoneId = objMaster.Current.DropOffZoneId;
                                }
                                else
                                {
                                    objMaster.Current.DropOffZoneId = lastDropOffZoneId;
                                }
                            }
                        }


                        objMaster.Current.AdvanceBookingId = advanceBookingId;
                        objMaster.Current.CallRefNo = ObjBooking.CallRefNo.ToStr().Trim();

                        objMaster.CheckServiceCharges = AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool();


                        if (ObjBooking.EscortId != null)
                        {
                            
                            objMaster.Current.EscortId = ObjBooking.EscortId;
                        }

                        objMaster.Current.EscortPrice = ObjBooking.EscortPrice.ToDecimal();

                        objMaster.Save();


                        if (objMaster.Current.DriverId != null)
                        {

                            frmDespatchJob frm = null;
                            if ( (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.WAITING
                                   || objMaster.Current.BookingStatusId == null))
                            {


                                frm = new frmDespatchJob(objMaster.Current);
                                frm.ShowDialog();

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
                        AllSuccess = false;
                        break;
                    }

                }


                if (AllSuccess)
                {

                    // Advance Booking Confirmation Text
                    bool enableAdvBookingText = AppVars.objPolicyConfiguration.EnableAdvanceBookingSMSConfirmation.ToBool();

                    DateTime? pickupdateTime = PickUpDateTime;
                    if (enableAdvBookingText && pickupdateTime != null)
                    {
                        string pickupSpan = string.Format("{0:HH:mm}", pickupdateTime);

                        TimeSpan picktime = TimeSpan.Parse(pickupSpan);


                        string nowP = string.Format("{0:HH:mm}", nowDate);
                        TimeSpan nowSpantime = TimeSpan.Parse(nowP);


                        int afterMins = AppVars.objPolicyConfiguration.AdvanceBookingSMSConfirmationMins.ToInt();
                        int minDifference = picktime.Subtract(nowSpantime).Minutes;
                        int dayDiff = pickupdateTime.Value.Date.Subtract(DateTime.Now.Date).Days;
                        if (afterMins >= 0 && (dayDiff > 0 || minDifference >= afterMins))
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

                                            object parentObj = ObjBooking.GetType().GetProperty(val[0]).GetValue(ObjBooking, null);

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
                                            propertyValue = ObjBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(ObjBooking, null);
                                        }


                                        if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                                        {
                                            propertyValue = ObjBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(ObjBooking, null);
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
                            General.SendAdvanceBookingSMS(customerMobileNo, ref refMsg, msg, ObjBooking.SMSType.ToInt());

                        }
                    }

                    //
                }
            

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
                IsSuccess = false;
            }

            return IsSuccess;

        }

        private string LastPickupPostCode = string.Empty;
        private string LastDropOffPostCode = string.Empty;
        private int? lastPickupZoneId = null;
        private int? lastDropOffZoneId = null;

        private void btnSave_Click(object sender, EventArgs e)
        {
            Saved = Save();

            this.Close();
        }

        private void ddlFromLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillFromLocations();
        }

        private void ddlToLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillToLocations();
        }

        private void chkOrigin_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
                txtFromAddress.Text = string.Empty;
                txtFromFlightDoorNo.Text = string.Empty;
                txtFromPostCode.Text = string.Empty;
                txtFromStreetComing.Text = string.Empty;
                ddlFromLocation.SelectedValue = null;

                txtFromAddress.Focus();
                numFareRate.Value = 0;
                lblMap.Text = string.Empty;
            }

            
        }

        private void chkDestination_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
                txtToAddress.Text = string.Empty;
                txtToFlightDoorNo.Text = string.Empty;
                txtToPostCode.Text = string.Empty;
                txtToStreetComing.Text = string.Empty;
                ddlToLocation.SelectedValue = null;

                txtToAddress.Focus();

                numFareRate.Value = 0;
                lblMap.Text = string.Empty;

            }
        }


        List<decimal> milesList = new List<decimal>();

        private bool CalculateFares()
        {
            milesList.Clear();
            int? vehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
            int companyId = ObjBooking.CompanyId.ToInt();
            int? fromLocationId = ddlFromLocation.SelectedValue.ToIntorNull();
            int? fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
            DateTime bookingDate = DateTime.Now.ToDate();

            int? toLocTypeId = ddlToLocType.SelectedValue.ToInt();
            int? toLocationId = ddlToLocation.SelectedValue.ToIntorNull();

            string fromLocName = ddlFromLocation.Text.Trim();
            string toLocName = ddlToLocation.Text.Trim();

            string fromAddress = txtFromAddress.Text.Trim();
            string toAddress = txtToAddress.Text.Trim();
            string fromPostCode = txtFromPostCode.Text.Trim();
            string toPostCode = txtToPostCode.Text.Trim();

            DateTime? pickupTime=ObjBooking.PickupDateTime;

            List<string> errors = new List<string>();

         

            if (vehicleTypeId == null)
            {
                errors.Add("Required : Vehicle Type");

            }

            if (fromLocationId == null && string.IsNullOrEmpty(fromPostCode) && string.IsNullOrEmpty(fromAddress))
            {

                errors.Add("Required : From Address");

            }


            if (toLocationId == null && string.IsNullOrEmpty(toPostCode) && string.IsNullOrEmpty(toAddress))
            {
                errors.Add("Required : To Address");

            }


            if (errors.Count > 0)
            {
                ENUtils.ShowMessage(string.Join(Environment.NewLine, errors.Select(c => c).ToArray<string>()));
                return false;
            }      


            // Calculating Fares


            //    lblMsgCalculateFares.Visible = true;

            int tempFromLocId = 0;
            int tempToLocId = 0;
            int tempActualToLocid = 0;
            string tempFromPostCode = "";
            string tempToPostCode = "";
            string tempactualToPostCode = "";
          
            string errorMsg = string.Empty;

            decimal fareVal = 0.00m;
            decimal deadMileage = AppVars.objPolicyConfiguration.DeadMileage.ToDecimal();

         
            if (errorMsg == "Error")
            {
         
                numFareRate.Value = 0;
                return false;
            }

            if (tempToLocId == 0)
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



            tempActualToLocid = tempToLocId;
            tempactualToPostCode = tempToPostCode;

            bool IsCompanyFareExist = false;
            string estimatedTime = string.Empty;


            var viaList = ObjBooking.Booking_ViaLocations.ToList();


            for (int i = 0; i < viaList.Count(); i++)
            {
                var item = viaList[i].ViaLocValue;


                if (i == 0)
                {

                    tempToLocId = viaList[i].ViaLocId.ToInt();
                    tempToPostCode = viaList[i].ViaLocValue;
                }
                else
                {

                    tempFromLocId = viaList[i-1].ViaLocId.ToInt();
                    tempFromPostCode = viaList[i-1].ViaLocValue;
                   
                    tempToLocId = viaList[i].ViaLocId.ToInt();
                    tempToPostCode = viaList[i].ViaLocValue;                 

                }


                tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                tempToPostCode = General.GetPostCodeMatch(tempToPostCode);


                // Fare Calculation
                fareVal += General.GetFareRate(ObjBooking.SubcompanyId.ToInt(), companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, true, false,pickupTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime);

                if (errorMsg == "Error")
                    break;

            }



            if (viaList.Count() > 0)
            {

                tempFromLocId = tempToLocId;
                tempFromPostCode = tempToPostCode;



                tempToLocId = tempActualToLocid;
                tempToPostCode =tempactualToPostCode ;


            }




            fareVal += General.GetFareRate(ObjBooking.SubcompanyId.ToInt(), companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, false, pickupTime, ref deadMileage,fromLocTypeId.ToInt(),toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime);


        //    fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, ddlFromLocType.Items.Count(c => c.Text == "Zone") > 0, pickupTime, ref deadMileage);


            if (errorMsg == "Error")
            {
            

                numFareRate.Value = 0;
                return false;
            }

   


            // Add Airport Pickup Charges If Pickup Point is From Airport...
            if (companyId == 0 && fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT && errorMsg == "Reverse found")
                fareVal += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();



            var objInc = General.GetObject<Fare_IncrementSetting>(c => c.Id != 0 && c.EnableIncrement != null && c.EnableIncrement == true);

            if (objInc != null && DateTime.Now.ToDate() >= objInc.FromDate.ToDate() && DateTime.Now.ToDate() <= objInc.TillDate.ToDate())
            {
                if (objInc.IncrementType.ToStr() == "percent")
                {
                    fareVal = fareVal + ((fareVal * objInc.IncrementRate.ToDecimal()) / 100);

                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                        fareVal = Math.Ceiling(fareVal);
                    }
                }
                else
                {
                    fareVal += objInc.IncrementRate.ToDecimal();

                }

            }



            numFareRate.Value = fareVal;
            


            // New


            int journeyTypeId = ObjBooking.JourneyTypeId.ToInt();

            int discountReturn = AppVars.objPolicyConfiguration.DiscountForReturnedJourneyPercent.ToInt();

            if (journeyTypeId==Enums.JOURNEY_TYPES.RETURN)
            {
              
                numReturnFare.Value = numFareRate.Value - ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForReturnedJourneyPercent.ToInt()) / 100);

                if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT )
                    numReturnFare.Value += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();

            }

            else if (journeyTypeId == Enums.JOURNEY_TYPES.WAITANDRETURN)
            {
                numFareRate.Value = numFareRate.Value + ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForWRJourneyPercent.ToInt()) / 100);
            }
            else
            {
                numReturnFare.Value = 0;
            }

            //

            if (companyId != 0)
            {
                numCompanyFares.Value = numFareRate.Value;
            }


            return true;

        }



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

            // string via = string.Join("|", grdVia.Rows.Select(c => c.Cells[COLS.VIALOCATIONVALUE].Value.ToStr()).ToArray<string>());


            if (UseGoogleMap)
            {

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
            }

            return miles;
        }

        private void btnCalculateFare_Click(object sender, EventArgs e)
        {
            try
            {
                CalculateFares();

             
               lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles";              

            }
            catch (Exception ex)
            {

             

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
            if (e.Result == null || e.Cancelled || (sender as BackgroundWorker) == null)
                return;

            try
            {


                ShowAddressesPOI((string[])e.Result);

            }
            catch
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

            if (aTxt.SelectedItem != null && aTxt.ListBoxElement.SelectedItem!=null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower()
               && aTxt.Text.Length > 0 && aTxt.Text[0].ToStr().IsNumeric())
            {
                aTxt.TextChanged -= TextBoxElement_TextChanged;
              //  aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();

                if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
                {
                    aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
                }

                aTxt.SelectedItem = aTxt.Text.Trim();
                aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
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






    }
}
