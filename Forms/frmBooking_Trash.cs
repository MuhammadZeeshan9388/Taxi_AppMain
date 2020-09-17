using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using Utils;
using UI.UserControls;
using Telerik.WinControls.UI;
using System.Data.Linq;
using System.Xml.Linq;
using Taxi_BLL;
using Taxi_Model;
using Telerik.WinControls.Enumerations;
using DAL;
using Telerik.WinControls;
using System.Text.RegularExpressions;
using System.Net.Cache;
using System.Threading;
using UI;
using System.Collections;
using Telerik.WinControls.UI.Docking;
using System.Data.Linq.SqlClient;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class frmBooking_Trash : UI.SetupBase
    {

        private int? _PickBookingTypeId = Enums.BOOKING_TYPES.LOCAL;

        public int? PickBookingTypeId
        {
            get { return _PickBookingTypeId; }
            set { _PickBookingTypeId = value; }
        }


        frmDespatchJob frm = null;

       

        RadTextBox txt = null;

        bool IsKeyword = false;




        //int? prevDriverId = null;
        //int? newDriverId = null;
        private bool saved = false;


        private int _MapType;

        public int MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }

        private Point _PnlOldBottomLocation;
        
        public Point PnlOldBottomLocation
        {
            get { return _PnlOldBottomLocation; }
            set { _PnlOldBottomLocation = value; }
        }

        private Point _PnlNewBottomLocation;

        public Point PnlNewBottomLocation
        {
            get { return _PnlNewBottomLocation; }
            set { _PnlNewBottomLocation = value; }
        }


        private Point _OldfromDoorNoLoc;

        public Point OldfromDoorNoLoc
        {
            get { return _OldfromDoorNoLoc; }
            set { _OldfromDoorNoLoc = value; }
        }

        private Point _NewFromDoorNoLoc;

        public Point NewFromDoorNoLoc
        {
            get { return _NewFromDoorNoLoc; }
            set { _NewFromDoorNoLoc = value; }
        }

        private Point _OldtoDoorNoLoc;

        public Point OldtoDoorNoLoc
        {
            get { return _OldtoDoorNoLoc; }
            set { _OldtoDoorNoLoc = value; }
        }


        private Point _NewtoDoorNoLoc;

        public Point NewtoDoorNoLoc
        {
            get { return _NewtoDoorNoLoc; }
            set { _NewtoDoorNoLoc = value; }
        }



        BookingBO objMaster;

        Booking_Payment objBookingPayment = null;

        bool UseGoogleMap = true;

        public frmBooking_Trash()
        {
            InitializeComponent();
            InitializeConstructor();
        }
        int BookingId = 0;
        public frmBooking_Trash(int Id)
        {
            InitializeComponent();
            InitializeConstructor();
            BookingId = Id;
            //Display(Id);
        }
        Booking_Trash objTrash = null;
        Booking_ViaLocations_Trash objTrashViaLoc = null;
      
        public frmBooking_Trash(string name, string phone)
        {

        

            InitializeComponent();
            InitializeConstructor();
            name = name.ToProperCase();
            ddlCustomerName.Text = name;



            if (phone.StartsWith("07"))
            {
                txtCustomerMobileNo.Text = phone;
            }
            else
            {
                txtCustomerPhoneNo.Text = phone;
            }
        }

        public frmBooking_Trash(string name, string phone, string doorNo, string address)
        {
        
            

            InitializeComponent();
            InitializeConstructor();
            name = name.ToProperCase();
            ddlCustomerName.Text = name;

            //  btnSearch.Visible = false;

            if (phone.StartsWith("07"))
            {
                txtCustomerMobileNo.Text = phone;
            }
            else
            {
                txtCustomerPhoneNo.Text = phone;
            }

            txtFromFlightDoorNo.Text = doorNo;
            txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            txtFromAddress.Text = address;
            txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);



        }

        public frmBooking_Trash(string name, string phone, string mobileNo, string doorNo, string address)
        {


            InitializeComponent();
            InitializeConstructor();

            name = name.ToProperCase();
            ddlCustomerName.Text = name;


            txtCustomerPhoneNo.Text = phone;
            txtCustomerMobileNo.Text = mobileNo;


            txtFromFlightDoorNo.Text = doorNo;
            txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            txtFromAddress.Text = address;
            txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
        }

        public frmBooking_Trash(string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                          int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, string doorNo)
        {



            InitializeComponent();
            InitializeConstructor();

            txtFromFlightDoorNo.Text = doorNo;
            PickBooking(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse);

            btnSearch.Visible = false;
        }


        public frmBooking_Trash(string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                    int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, string fromDoorNo, string toDoorNo)
        {



            InitializeComponent();
            InitializeConstructor();

            txtFromFlightDoorNo.Text = fromDoorNo.ToStr().Trim();
            txtToFlightDoorNo.Text = toDoorNo.ToStr().Trim();

            PickBooking(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse);

            btnSearch.Visible = false;
        }

        private void PickBooking(string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                          int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse)
        {

            name = name.ToProperCase();
            ddlCustomerName.Text = name;
            if (phone.StartsWith("07"))
            {
                txtCustomerMobileNo.Text = phone;
            }
            else
            {
                txtCustomerPhoneNo.Text = phone;
            }

            if (IsReverse)
            {
                fromLocTypeId = fromLocTypeId ^ toLocTypeId;
                toLocTypeId = toLocTypeId ^ fromLocTypeId;
                fromLocTypeId = fromLocTypeId ^ toLocTypeId;

                if (fromLocId != null && toLocId != null)
                {

                    fromLocId = fromLocId ^ toLocId;
                    toLocId = toLocId ^ fromLocId;
                    fromLocId = fromLocId ^ toLocId;
                }

                if (fromLocId == null)
                    fromLocId = toLocId;

                if (toLocId == null)
                    toLocId = fromLocId;

                string tempAddress = fromAddress;
                fromAddress = toAddress;
                toAddress = tempAddress;



            }

            ddlFromLocType.SelectedValue = fromLocTypeId;
            ddlToLocType.SelectedValue = toLocTypeId;

            if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
            {
                this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = fromAddress;
                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }

            else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtFromPostCode.Text = fromAddress;
            }
            else
            {
                ddlFromLocation.SelectedValue = fromLocId;
            }


            if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
            {
                this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = toAddress;
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }
            else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtToPostCode.Text = toAddress;
            }
            else
            {
                ddlToLocation.SelectedValue = toLocId;
            }




            numFareRate.Value = fare;
            ddlCustomerName.Focus();


        }



        private void PickBookingComplete(string name, string phone, string mobileNo, int? fromLocTypeId, int? toLocTypeId,
                         int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, int? bookingTypeId)
        {
            OnPickDetails(name, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse);

            txtCustomerPhoneNo.Text = phone;
            txtCustomerMobileNo.Text = mobileNo;


            ddlBookingType.SelectedValue = bookingTypeId;
        }

        private void OnPickDetails(string name, int? fromLocTypeId, int? toLocTypeId,
                         int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse)
        {
            name = name.ToProperCase();
            ddlCustomerName.Text = name;

            if (IsReverse)
            {
                fromLocTypeId = fromLocTypeId ^ toLocTypeId;
                toLocTypeId = toLocTypeId ^ fromLocTypeId;
                fromLocTypeId = fromLocTypeId ^ toLocTypeId;

                if (fromLocId != null && toLocId != null)
                {

                    fromLocId = fromLocId ^ toLocId;
                    toLocId = toLocId ^ fromLocId;
                    fromLocId = fromLocId ^ toLocId;
                }

                if (fromLocId == null)
                    fromLocId = toLocId;

                if (toLocId == null)
                    toLocId = fromLocId;

                string tempAddress = fromAddress;
                fromAddress = toAddress;
                toAddress = tempAddress;



            }

            ddlFromLocType.SelectedValue = fromLocTypeId;
            ddlToLocType.SelectedValue = toLocTypeId;

            if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
            {
                this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = fromAddress;
                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }
            else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
            {
                ddlFromLocation.SelectedValue = fromLocId;
            }
            else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtFromPostCode.Text = fromAddress;
            }
            else
            {
                ddlFromLocation.SelectedValue = fromLocId;
            }


            if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
            {
                this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = toAddress;
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }
            else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtToPostCode.Text = toAddress;
            }
            else
            {
                ddlToLocation.SelectedValue = toLocId;
            }


            numFareRate.Value = fare;
            ddlCustomerName.Focus();


        }



        private void InitializeConstructor()
        {
            try
            {
                this.Load += new EventHandler(frmBooking_Load);
                // this.FormClosing += new FormClosingEventHandler(frmBooking_FormClosing);
                this.FormClosed += new FormClosedEventHandler(frmBooking_FormClosed);
                numFareRate.SpinElement.TextBoxItem.KeyDown += new KeyEventHandler(TextBoxItem_KeyDown);

                ddlDriver.LostFocus += new EventHandler(ddlDriver_LostFocus);
                ddlDriver.Leave += new EventHandler(ddlDriver_Leave);

                btnCancelBooking.Enabled = false;

                //btnDespatchScreen.Enabled = false;


                this.OldfromDoorNoLoc = txtFromFlightDoorNo.Location;
                this.NewFromDoorNoLoc = new Point(txtFromFlightDoorNo.Location.X, txtFromFlightDoorNo.Location.Y - 56);



                this.OldtoDoorNoLoc = txtToFlightDoorNo.Location;
                this.NewtoDoorNoLoc = new Point(txtToFlightDoorNo.Location.X, txtToFlightDoorNo.Location.Y - 56);

                if (AppVars.objPolicyConfiguration != null)
                {
                    timer1.Tick += new EventHandler(timer1_Tick);
                    if (AppVars.objPolicyConfiguration.MapType.ToInt() == Enums.MAP_TYPE.MAPPOINT)
                    {

                        UseGoogleMap = false;
                    }
                    MapType = AppVars.objPolicyConfiguration.MapType.ToInt();

                    //chkAutoDespatch.Enabled = AppVars.objPolicyConfiguration.EnablePDA.ToBool() ? true : false;

                }


                PnlOldBottomLocation = pnlBottom.Location;
                PnlNewBottomLocation = new Point(PnlOldBottomLocation.X, PnlOldBottomLocation.Y + 120);

                this.Shown += new EventHandler(frmBooking_Shown);
                FillCombos();

                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                this.txtViaAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtFromAddress.ListBoxElement.Width = txtToAddress.ListBoxElement.Width = 400;

                txtViaAddress.ListBoxElement.Width = txtViaAddress.DefaultWidth;
                txtViaAddress.ListBoxElement.Height = txtViaAddress.DefaultHeight;


                Font font = new Font("Tahoma", 10, FontStyle.Regular);
                txtFromAddress.ListBoxElement.Font = font;
                txtToAddress.ListBoxElement.Font = font;
                txtviaPostCode.ListBoxElement.Font = font;



                objMaster = new BookingBO();
                this.SetProperties((INavigation)objMaster);


                FormatViaGrid();

                pnlOtherCharges.Visible = AppVars.objPolicyConfiguration.EnableBookingOtherCharges.ToBool();

                OnNew();


                EnablePOI = AppVars.objPolicyConfiguration.EnablePOI.ToBool();

                SetDefaultCommission();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void LoadPostCodes()
        {

            if (txtFromPostCode.AutoCompleteCustomSource.Count > 0 && txtToPostCode.AutoCompleteCustomSource.Count > 0
                && txtviaPostCode.AutoCompleteCustomSource.Count > 0) return;

            var postcodes = (from a in AppVars.listOfAddress

                             select a.PostalCode
                                  ).Distinct().ToArray<string>();


            txtFromPostCode.AutoCompleteCustomSource.Clear();
            txtFromPostCode.AutoCompleteCustomSource.AddRange(postcodes);

            txtToPostCode.AutoCompleteCustomSource.Clear();
            txtToPostCode.AutoCompleteCustomSource.AddRange(postcodes);

        }


   //     private bool IsExistData = false;

        private void SetDefaultCommission()
        {

            numDriverCommission.Value = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal();
            ddlCommissionType.SelectedValue = "Percent";
        }

        string[] res = null;
        string searchTxt = "";
        bool IsPOI = false;
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


                if (EnablePOI && searchTxt.StartsWith("P="))
                {
                    if (searchTxt.Trim().Length > 2)
                    {
                        IsPOI = true;
                        searchTxt = searchTxt.Replace("P=", "").Trim();
                    }

                }


                string postCode = General.GetPostCodeMatch(searchTxt);
                if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                    postCode = string.Empty;

                string street = searchTxt;
                if (!string.IsNullOrEmpty(postCode))
                    street = street.Replace(postCode, "").Trim();



                //if (EnablePOI && IsPOI)
                //{

                //    res = (from a in AppVars.listofPOI

                //           where (a.FullAddress.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))
                //           select a.FullAddress
                //                       ).Take(1000).ToArray<string>();
                //}
                //else
                //{

                    res = (from a in AppVars.listOfAddress

                           where (a.AddressLine1.Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode) || a.PostalCode.Strip(' ').StartsWith(postCode))))
                           select a.AddressLine1

                                   ).Take(1000).ToArray<string>();



                    if (UseGoogleMap && res.Count() == 0)
                    {


                        string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + searchTxt + ", UK&sensor=false";

                        wc.CancelAsync();

                        wc = new WebClient();
                        wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                        wc.DownloadStringAsync(new Uri(url));


                        return;

                    }
            //    }

                ShowAddresses();

            }
            catch
            {


            }

        }

        void frmBooking_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

                if (saved && (frm == null || (frm != null && frm.SuccessDespatched == false)))
                {
                    General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");
                    General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
                }

                if (frm != null)
                {
                    if (frm.SmsThread != null)
                        frm.SmsThread.Start();


                    frm.Dispose();
                }


            


                General.DisposeForm(this);
            }
            catch 
            {


            }
            //  this.Dispose(true);
        }

        void ddlDriver_LostFocus(object sender, EventArgs e)
        {

        }

        void ddlDriver_Leave(object sender, EventArgs e)
        {
            FocusOnSave();
        }



        void btn_pickLocals_Click(object sender, EventArgs e)
        {
            PickHospital();
        }


        private void PickHospital()
        {
            //string type = ddlHospitalType.Text.ToLower().Trim();
            //if (type == "from")
            //{
            //    ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.HOSPITAL;
            //    ddlFromLocation.SelectedValue = ddlHospitals.SelectedValue;
            //    txtToAddress.Focus();
            //}
            //else if (type == "to")
            //{
            //    ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.HOSPITAL;
            //    ddlToLocation.SelectedValue = ddlHospitals.SelectedValue;
            //    ddlCustomerName.Focus();
            //}

            //HideHospitals();

        }

        void btn_PickStations_Click(object sender, EventArgs e)
        {
            PickStation();
        }

        private void PickStation()
        {

            //string type = ddlStationType.Text.ToLower().Trim();
            //if (type == "from")
            //{
            //    ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;
            //    ddlFromLocation.SelectedValue = ddlStations.SelectedValue;

            //    txtToAddress.Focus();
            //}
            //else if (type == "to")
            //{
            //    ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;
            //    ddlToLocation.SelectedValue = ddlStations.SelectedValue;
            //    ddlCustomerName.Focus();
            //}


            //HideStations();

        }

        void btnAirport_Pick_Click(object sender, EventArgs e)
        {
            PickAirport();
        }

        private void PickAirport()
        {

            //string type = ddlAirportType.Text.ToLower().Trim();
            //if (type == "from")
            //{
            //    ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.AIRPORT;
            //    ddlFromLocation.SelectedValue = ddlAirPorts.SelectedValue;

            //    txtToAddress.Focus();
            //}
            //else if (type == "to")
            //{
            //    ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.AIRPORT;
            //    ddlToLocation.SelectedValue = ddlAirPorts.SelectedValue;
            //    ddlCustomerName.Focus();
            //}

            //HideAirports();

        }



        void txtPostCode_TextChanged(object sender, EventArgs e)
        {
            if (MapType == Enums.MAP_TYPE.NONE) return;
            try
            {

                if (UseGoogleMap == false)
                {
                    return;
                }


                txt = (RadTextBox)sender;


                string temp = string.Empty;
                string text = txt.Text;
                if (text.Length > 2)
                {


                    temp = text.ToUpper();

                    if (txt.AutoCompleteCustomSource.Contains(temp)) return;

                    text = text.Replace(" ", "+");
                    string url = "http://api.zoopla.co.uk/api/v1/zoopla_estimates?postcode=" + text + "&api_key=xwvgd6ckeb4r5ejfk3wzgkg2";


                    wc = new WebClient();

                    string s = wc.DownloadString(new Uri(url));

                    XDocument doc = XDocument.Parse(s);
                    var res = (from a in doc.Descendants("postcode")
                               select a.Value.ToStr()).ToArray<string>();

                    txt.AutoCompleteCustomSource.AddRange(res);



                }

            }
            catch
            {


            }
        }



        private void InitializeMap()
        {
            if (AppVars.objPolicyConfiguration.MapType.ToInt() == Enums.MAP_TYPE.MAPPOINT)
            {
                UseGoogleMap = false;
            }

        }





        //void TextBoxElement_TextChanged(object sender, EventArgs e)
        //{


        //    try
        //    {
        //        IsPOI = false;
        //        IsKeyword = false;
        //        timer1.Stop();
        //        IsExistData = false;
        //        aTxt = (AutoCompleteTextBox)sender;
        //        aTxt.ResetListBox();

        //        if (aTxt.Name == "txtFromAddress")
        //        {

        //            txtToAddress.SendToBack();
        //            pnlBottom.SendToBack();
        //            pnlVia.SendToBack();

        //        }
        //        else if (aTxt.Name == "txtToAddress")
        //        {
        //            txtToAddress.BringToFront();
        //        }
        //        else if (aTxt.Name == "txtViaAddress")
        //        {
        //            pnlVia.SendToBack();

        //        }



        //        string text = aTxt.Text;
        //        if (text.Length > 2)
        //        {

        //            if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() == aTxt.Text.ToLower())
        //            {
        //                aTxt.ListBoxElement.Items.Clear();
        //                // aTxt.Values = null;
        //                aTxt.ResetListBox();


        //                string formerValue = aTxt.FormerValue.ToLower().Trim();

        //                int? loctypeId = 0;
        //                if (AppVars.keyLocations.Contains(formerValue))
        //                {
        //                    Gen_Location loc = General.GetObject<Gen_Location>(c => c.ShortCutKey == formerValue);
        //                    if (loc != null)
        //                    {
        //                        loctypeId = loc.LocationTypeId;
        //                    }
        //                }

        //                if (loctypeId != 0)
        //                {

        //                    if (aTxt.Name == "txtFromAddress")
        //                    {


        //                        ddlFromLocType.SelectedValue = loctypeId;
        //                        RadComboBoxItem item = (RadComboBoxItem)ddlFromLocation.Items.FirstOrDefault(b => b.Text.Equals(aTxt.SelectedItem));
        //                        if (item != null)
        //                        {

        //                            ddlFromLocation.SelectedValue = item.Value;
        //                        }

        //                        if (loctypeId != Enums.LOCATION_TYPES.POSTCODE || loctypeId != Enums.LOCATION_TYPES.ADDRESS
        //                            || loctypeId != Enums.LOCATION_TYPES.AIRPORT || loctypeId != Enums.LOCATION_TYPES.BASE)
        //                        {
        //                            txtToAddress.Focus();
        //                        }
        //                    }
        //                    else if (aTxt.Name == "txtToAddress")
        //                    {

        //                        ddlToLocType.SelectedValue = loctypeId;
        //                        RadComboBoxItem item = (RadComboBoxItem)ddlToLocation.Items.FirstOrDefault(b => b.Text.Equals(aTxt.SelectedItem));
        //                        if (item != null)
        //                        {
        //                            ddlToLocation.SelectedValue = item.Value;

        //                        }

        //                        if (loctypeId == Enums.LOCATION_TYPES.POSTCODE || loctypeId == Enums.LOCATION_TYPES.ADDRESS)
        //                        {
        //                            txtToFlightDoorNo.Focus();
        //                        }
        //                        else
        //                        {
        //                            ddlCustomerName.Focus();
        //                        }
        //                    }
        //                }

        //                aTxt.FormerValue = string.Empty;

        //                // new code
        //                lblMap.Text = string.Empty;
        //                //string frmaddress = txtFromAddress.Text == null ? "null" : txtFromAddress.Text;
        //                //string Toaddress = txtToAddress.Text == "" ? "null" : txtToAddress.Text;

        //                string frmaddress = ddlFromLocation.Visible == false ? txtFromAddress.Text == null ? "null" : txtFromAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlFromLocation.SelectedItem)).Text.ToStr();
        //                string Toaddress = ddlToLocation.Visible == false ? txtToAddress.Text == null ? "null" : txtToAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlToLocation.SelectedItem)).Text.ToStr();
                       
        //                if (txtFromPostCode.Visible == true)
        //                {
        //                    frmaddress = txtFromPostCode.Text;
        //                }
        //                else if (txtToPostCode.Visible == true)
        //                {
        //                    Toaddress = txtToPostCode.Text;
        //                }

        //                CalculateFares();

        //                string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + frmaddress + "&destination=" + Toaddress + "&sensor=false";


        //                XmlTextReader reader = new XmlTextReader(url2);
        //                System.Data.DataSet ds = new System.Data.DataSet();
        //                ds.ReadXml(reader);
        //                string Status = ds.Tables[0].Rows[0]["status"].ToString();
        //                if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
        //                //if (Status == "ZERO_RESULTS")
        //                {
        //                    url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
        //                    MileageError();
        //                }
        //                else
        //                {

        //                    DataTable dt = ds.Tables["duration"];

        //                    DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
        //                    string time = row.ItemArray[1].ToString();

        //                    decimal miles = milesList.Sum();
        //                    if (miles > 10000 || mileageError)
        //                        MileageError();
        //                    else
        //                    {
        //                        lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
        //                    }
        //                }

        //                CalculateTotalCharges();
        //                return;

        //            }



        //            if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
        //            {

        //                if (UseGoogleMap)
        //                {
        //                    wc.CancelAsync();
        //                    aTxt.Values = null;
        //                }
        //            }
        //            text = text.ToLower();


        //            if (AppVars.keyLocations.Contains(text))
        //            {


        //                aTxt.ListBoxElement.Items.Clear();
        //                var res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey == text)
        //                           select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
        //                            ).ToArray<string>();


        //                IsKeyword = true;

        //                aTxt.ListBoxElement.Items.AddRange(res);
        //                aTxt.ShowListBox();

        //                if (this.Text != aTxt.FormerValue)
        //                {
        //                    aTxt.FormerValue = aTxt.Text;
        //                }
        //            }


        //            if (MapType == Enums.MAP_TYPE.NONE) return;

        //            StartAddressTimer(text);

        //        }
        //        else
        //        {
        //            if (MapType == Enums.MAP_TYPE.NONE) return;


        //            if (UseGoogleMap)
        //            {
        //                wc.CancelAsync();
        //                aTxt.Values = null;
        //            }
        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        void TextBoxElement_TextChanged(object sender, EventArgs e)
        {


            try
            {
                IsPOI = false;
                IsKeyword = false;
                timer1.Stop();
            //    IsExistData = false;
                aTxt = (AutoCompleteTextBox)sender;
                aTxt.ResetListBox();

                if (aTxt.Name == "txtFromAddress")
                {

                    txtToAddress.SendToBack();
                    pnlBottom.SendToBack();
                    pnlVia.SendToBack();

                }
                else if (aTxt.Name == "txtToAddress")
                {
                    txtToAddress.BringToFront();
                }
                else if (aTxt.Name == "txtViaAddress")
                {
                    pnlVia.SendToBack();

                }



                string text = aTxt.Text;
                if (text.Length > 2)
                {

                    if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() == aTxt.Text.ToLower())
                    {
                        aTxt.ListBoxElement.Items.Clear();
                        // aTxt.Values = null;
                        aTxt.ResetListBox();


                        string formerValue = aTxt.FormerValue.ToLower().Trim();

                        int? loctypeId = 0;
                        if (AppVars.keyLocations.Contains(formerValue))
                        {
                            Gen_Location loc = General.GetObject<Gen_Location>(c => c.ShortCutKey == formerValue);
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

                                }

                                if (loctypeId == Enums.LOCATION_TYPES.POSTCODE || loctypeId == Enums.LOCATION_TYPES.ADDRESS)
                                {
                                    txtToFlightDoorNo.Focus();
                                }
                                else
                                {
                                    ddlCustomerName.Focus();
                                }
                            }
                        }

                        aTxt.FormerValue = string.Empty;

                        return;
                    }



                    if (MapType == Enums.MAP_TYPE.GOOGLEMAPS)
                    {

                        if (UseGoogleMap)
                        {
                            wc.CancelAsync();
                            aTxt.Values = null;
                        }
                    }
                    text = text.ToLower();


                    if (AppVars.keyLocations.Contains(text))
                    {


                        aTxt.ListBoxElement.Items.Clear();
                        var res = (from a in General.GetQueryable<Gen_Location>(c => c.ShortCutKey == text)
                                   select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                                    ).ToArray<string>();


                        IsKeyword = true;

                        aTxt.ListBoxElement.Items.AddRange(res);
                        aTxt.ShowListBox();

                        if (this.Text != aTxt.FormerValue)
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


                    if (UseGoogleMap)
                    {
                        wc.CancelAsync();
                        aTxt.Values = null;
                    }
                }


            }
            catch 
            {

            }
        }

        private void StartAddressTimer(string text)
        {

            text = text.ToLower();
            searchTxt = text;

            timer1.Start();

        }



        private void DisplayBooking_Map()
        {
            string fromAddress = txtFromAddress.Text.ToStr().ToUpper();
            string[] viaLocs = grdVia.Rows.Select(c => c.Cells[COLS.VIALOCATIONVALUE].Value.ToStr()).ToArray<string>();
            string toAddress = txtToAddress.Text.ToStr().ToUpper();

            mileageError = false;
            milesList.Clear();

            decimal distance = CalculateTotalDistance(fromAddress, viaLocs, toAddress);

            milesList.Add(distance);

            if (this.InvokeRequired)
            {
                DisplayMilesHandler d = new DisplayMilesHandler(ShowMilesFromGoogle);
                this.BeginInvoke(d);
            }
            else
            {
                ShowMilesFromGoogle();

            }


        }

        private void ShowMilesFromGoogle()
        {
            if (mileageError)
                MileageError();
            else
                lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles" ;
        }
        
        List<decimal> milesList = new List<decimal>();

        private void CalculateMiles()
        {
            //string milesVar = string.Join(",", milesList.Select(c => c.ToStr()).ToArray<string>());
            //int? vehicletypeId = ddlVehicleType.SelectedValue.ToIntorNull();
            //int? companyId = ddlCompany.SelectedValue.ToIntorNull();

            //ISingleResult<ClsFares> obj = General.SP_CalculateFares(vehicletypeId, companyId, milesVar,null);
            //if (obj != null)
            //{
            //    ClsFares fare = obj.FirstOrDefault();
            //    numFareRate.Value = fare.totalFares;

            //}

        }


        private bool EnablePOI = false;

        void frmBooking_Load(object sender, EventArgs e)
        {
            try
            {
                if (objMaster.Current == null)
                {
                    ddlBookingType.SelectedValue = this.PickBookingTypeId;

                    ComboFunctions.FillDriverNoQueueCombo(ddlDriver);
                    ComboFunctions.FillDriverNoQueueCombo(ddlReturnDriver);

                    //  ddlBookingType.Visible = ddlBookingType.Items.Count > 1;

                    return;
                }

                int? driverId = objMaster.Current.DriverId;
                string driverName = "";

                driverId = objMaster.Current.DriverId;
                if (driverId != null)
                    driverName = objMaster.Current.Fleet_Driver.DriverNo + " - " + objMaster.Current.Fleet_Driver.DriverName;

                ComboFunctions.FillDriverNoQueueCombo(ddlDriver, driverId, driverName);



                driverId = objMaster.Current.ReturnDriverId;
                if (driverId != null)
                    driverName = objMaster.Current.Fleet_Driver1.DriverNo + " - " + objMaster.Current.Fleet_Driver1.DriverName;

                ComboFunctions.FillDriverNoQueueCombo(ddlReturnDriver, driverId, driverName);




            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }


        }

        public struct COLS
        {
            public static string ID = "ID";
            public static string MASTERID = "MASTERID";

            public static string FROMVIALOCTYPEID = "FROMVIALOCTYPEID";
            public static string FROMTYPELABEL = "FROMTYPELABEL";
            public static string FROMTYPEVALUE = "FROMTYPEVALUE";


            public static string VIALOCATIONID = "VIALOCATIONID";
            public static string VIALOCATIONLABEL = "VIALOCATIONLABEL";
            public static string VIALOCATIONVALUE = "VIALOCATIONVALUE";



        }


        Font oldFont = new Font("Tahoma", 9, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 9, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }

        void grdVia_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {

            e.CellElement.RowElement.BackColor = Color.Pink;
            e.CellElement.RowElement.NumberOfColors = 1;

            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderWidth = 0;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                e.CellElement.BackColor = Color.Pink;
                e.CellElement.NumberOfColors = 1;
            }

            else if (e.CellElement is GridDataCellElement)
            {


                e.CellElement.BorderWidth = 0;
                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                if (e.CellElement.ColumnInfo.Name == COLS.FROMTYPELABEL || e.CellElement.ColumnInfo.Name == COLS.VIALOCATIONLABEL)
                {
                    e.CellElement.Font = newFont;
                }
                else
                    e.CellElement.Font = oldFont;



            }


        }

        private void FormatViaGrid()
        {

            grdVia.ViewCellFormatting += new CellFormattingEventHandler(grdVia_ViewCellFormatting);
            grdVia.RowsChanged += new GridViewCollectionChangedEventHandler(grdVia_RowsChanged);
            grdVia.AutoSizeRows = true;
            grdVia.TableElement.TableHeaderHeight = 0;
            grdVia.ShowGroupPanel = false;
            grdVia.AllowAddNewRow = false;
            grdVia.AllowEditRow = false;
            grdVia.ShowRowHeaderColumn = false;

            grdVia.TableElement.BorderWidth = 0;
            grdVia.TableElement.BorderColor = Color.Transparent;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ID;
            grdVia.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.MASTERID;
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FROMVIALOCTYPEID;
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.VIALOCATIONID;
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.FROMTYPELABEL;
            col.HeaderText = "";
            col.Width = 100;
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.FROMTYPEVALUE;
            col.Width = 150;
            col.HeaderText = "";
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "EMPTY";
            col.Width = 100;
            grdVia.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.VIALOCATIONLABEL;
            col.HeaderText = "";
            col.Width = 120;
            grdVia.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.VIALOCATIONVALUE;
            col.Width = 280;
            col.HeaderText = "";
            grdVia.Columns.Add(col);

            AddDeleteColumn(grdVia);

        }

        public static void AddDeleteColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.BestFit();

            col.Name = "ColDelete";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Delete";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

            grid.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);

            grid.NewRowEnterKeyMode = RadGridViewNewRowEnterKeyMode.EnterMovesToNextRow;
        }



        static void grid_CommandCellClick(object sender, EventArgs e)
        {


            if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a via Address ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
            {

                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                RadGridView grid = gridCell.GridControl;
                grid.CurrentRow.Delete();
            }
        }


        void grdVia_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                grdVia.CurrentRow = null;
            }
        }

        void ddlViaLocation_OnRefreshing(object sender, EventArgs e)
        {
            FillViaLocations();
        }

        public override void AddNew()
        {
            OnNew();
        }


        public override void OnNew()
        {
            ddlVehicleType.SelectedValue = AppVars.objPolicyConfiguration.DefaultVehicleTypeId;
            ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;
         //   chkQuotation.Visible = AppVars.objPolicyConfiguration.EnableQuotation.ToBool();

            SetJourneyWise(ToggleState.On);
            UseCompanyRates(ToggleState.Off);
            txtBookingNo.Text = "Not Allocated";
            txtBookingDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            dtpPickupDate.Value = DateTime.Now.ToDate();
            dtpPickupTime.Value = DateTime.Now;
            txtFromAddress.Focus();

            RadListDataItem item = new RadListDataItem();
            item.Text = "Percent";
            item.Value = "Percent";
            item.Selected = true;

            ddlCommissionType.Items.Add(item);

            item = new RadListDataItem();
            item.Text = "Amount";
            item.Value = "Amount";

            ddlCommissionType.Items.Add(item);


            ShowAutoDespatchLabels(false);
            IsAutoDespatchEnabled(AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool());

        }

        private void IsAutoDespatchEnabled(bool enabled)
        {
            //if (enabled)
            //{
            //    chkAutoDespatch.Visible = true;
            //    chkAutoDespatch.Checked = true;
            //}
            //else
            //{
            //    chkAutoDespatch.Visible = false;
            //    chkAutoDespatch.Checked = false;

            //}

        }

        private void ShowAutoDespatchLabels(bool show)
        {
            //lblAutoDespLabel1.Visible = show;
            //lblAutoDespLabel3.Visible = show;
            //numBeforeMinutes.Visible = show;


        }

        AutoCompleteTextBox aTxt;
        WebClient wc = new WebClient();


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



        void ddlToLocation_OnRefreshing(object sender, EventArgs e)
        {
            FillToLocations();
        }

        private void FillToLocations()
        {


            int locTypeId = ddlToLocType.SelectedValue.ToInt();

            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
            {
                lblToLoc.Text = "Destination";
                txtToAddress.Visible = true;

                ddlToLocation.SelectedValue = null;
                ddlToLocation.Visible = false;

                txtToPostCode.Text = string.Empty;
                txtToPostCode.Visible = false;


                lblToDoorFlightNo.Text = "Door #";
                lblToDoorFlightNo.Visible = true;
                lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.NewtoDoorNoLoc.Y + 1);

                txtToFlightDoorNo.MaxLength = 30;
                txtToFlightDoorNo.Width = 170;

                txtToFlightDoorNo.Text = string.Empty;
                txtToFlightDoorNo.Visible = true;
                txtToFlightDoorNo.Location = this.NewtoDoorNoLoc;

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

                ddlToLocation.SelectedValue = null;
                ddlToLocation.Visible = false;

                txtToFlightDoorNo.Location = this.OldtoDoorNoLoc;
                txtToPostCode.Visible = true;
                txtToFlightDoorNo.Visible = true;
                txtToStreetComing.Visible = true;
                lblToDoorFlightNo.Visible = true;
                lblToStreetComing.Visible = true;

                lblToLoc.Text = "To PostCode";

                lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.OldtoDoorNoLoc.Y);
                lblToDoorFlightNo.Text = "To Door No";
                lblToStreetComing.Text = "To Street";

                txtToFlightDoorNo.MaxLength = 30;
                txtToFlightDoorNo.Width = 170;

            }

            else if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                SetReturnAirportJob(opt_JReturnWay.ToggleState);
                ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);

            }


            else
            {
                SetOthersToLocation();


                ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);

            }
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


            txtToFlightDoorNo.MaxLength = 50;
            txtToFlightDoorNo.Width = 170;


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

                    txtToFlightDoorNo.MaxLength = 50;
                    txtToFlightDoorNo.Width = 170;

                    lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.OldtoDoorNoLoc.Y);
                    txtToFlightDoorNo.Location = this.OldtoDoorNoLoc;
                    txtToFlightDoorNo.Visible = true;
                    txtToStreetComing.Visible = true;

                    lblToDoorFlightNo.Visible = true;
                    lblToStreetComing.Visible = true;

                    lblToLoc.Text = "To Location";

                    lblToDoorFlightNo.Text = "Flight No";
                    lblToStreetComing.Text = "Coming From";



                }
                else
                {


                    SetOthersToLocation();

                }



            }
        }

        void ddlFromLocation_OnRefreshing(object sender, EventArgs e)
        {
            FillFromLocations();

        }

        private void FillFromLocations()
        {
            int locTypeId = ddlFromLocType.SelectedValue.ToInt();
            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
            {
                txtFromAddress.Visible = true;

                ddlFromLocation.SelectedValue = null;
                ddlFromLocation.Visible = false;

                txtFromPostCode.Text = string.Empty;
                txtFromPostCode.Visible = false;

                lblFromDoorFlightNo.Text = "Door #";
                lblFromDoorFlightNo.Visible = true;
                //   lblFromDoorFlightNo.Location=new Point(lblFromDoorFlightNo
                lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.NewFromDoorNoLoc.Y + 1);

                txtFromFlightDoorNo.MaxLength = 30;
                txtFromFlightDoorNo.Width = 170;

                txtFromFlightDoorNo.Text = string.Empty;
                txtFromFlightDoorNo.Visible = true;
                txtFromFlightDoorNo.Location = this.NewFromDoorNoLoc;


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
                }
            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {

                LoadPostCodes();

                txtFromAddress.Text = string.Empty;
                txtFromAddress.Visible = false;

                ddlFromLocation.SelectedValue = null;
                ddlFromLocation.Visible = false;

                txtFromFlightDoorNo.MaxLength = 30;
                txtFromFlightDoorNo.Width = 170;

                lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.OldfromDoorNoLoc.Y);

                txtFromFlightDoorNo.Location = this.OldfromDoorNoLoc;
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

                txtFromFlightDoorNo.MaxLength = 50;
                txtFromFlightDoorNo.Width = 170;

                lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.OldfromDoorNoLoc.Y);
                txtFromFlightDoorNo.Location = this.OldfromDoorNoLoc;
                txtFromFlightDoorNo.Visible = true;
                txtFromStreetComing.Visible = true;
                lblFromDoorFlightNo.Visible = true;
                lblFromStreetComing.Visible = true;

                lblFromLoc.Text = "From Location";

                lblFromDoorFlightNo.Text = "Flight No";
                lblFromStreetComing.Text = "Coming From";
                ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);

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


                lblFromDoorFlightNo.Visible = false;
                lblFromStreetComing.Visible = false;

                txtFromAddress.Text = string.Empty;
                txtFromAddress.Visible = false;

                ddlFromLocation.Visible = true;
                ComboFunctions.FillLocationsCombo(ddlFromLocation, c => c.LocationTypeId == locTypeId);

                txtFromFlightDoorNo.MaxLength = 50;
                txtFromFlightDoorNo.Width = 120;

            }
        }

        private void FillCombos()
        {
            ComboFunctions.FillLocationTypeCombo(ddlFromLocType);
            ComboFunctions.FillLocationTypeCombo(ddlToLocType);
            ComboFunctions.FillLocationTypeCombo(ddlViaFromLocType);


            ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
            ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
            ddlViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;

            ComboFunctions.FillBookingTypeCombo(ddlBookingType);

            if (PickBookingTypeId == null)
                PickBookingTypeId = Enums.BOOKING_TYPES.LOCAL;

            ddlBookingType.SelectedValue = PickBookingTypeId;

            ComboFunctions.FillCompanyCombo(ddlCompany);
            ComboFunctions.FillVehicleTypeCombo(ddlVehicleType);


            ComboFunctions.FillPaymentTypeCombo(ddlPaymentType);


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

        void frmBooking_Shown(object sender, EventArgs e)
        {
            try
            {

                int? LocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();

                if ((LocTypeId == Enums.LOCATION_TYPES.ADDRESS || LocTypeId == Enums.LOCATION_TYPES.BASE)
                    )
                {

                    FocusOnFromAddress();
                }
                else if (LocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    FocusOnFromPostCode();
                }
                else
                {
                    FocusOnFromLocation();

                }



                txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
                txtCustomerPhoneNo.TextChanged += new EventHandler(txtCustomerPhoneNo_TextChanged);



                objTrash   = General.GetObject<Booking_Trash>(c => c.Id == BookingId);
                objTrashViaLoc = General.GetObject<Booking_ViaLocations_Trash>(c => c.BookingId == BookingId);


                DisplayRecord();
            }
            catch 
            {


            }
        }



        private void txtCustomerMobileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerMobileNo.Text.Trim().Length == 11)
                {
                    if (IsBookingExistForContact())
                    {

                    //    txtCustomerMobileNo.TextChanged -= new EventHandler(txtCustomerMobileNo_TextChanged);

                      //  SearchBooking();


                   //     txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
                    }
                }
            }
            catch 
            {


            }
        }

        void txtCustomerPhoneNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerPhoneNo.Text.Trim().Length == 11)
                {
                    if (IsBookingExistForContact())
                    {



                      //  txtCustomerPhoneNo.TextChanged -= new EventHandler(txtCustomerPhoneNo_TextChanged);



                     //   SearchBooking();
                     //   txtCustomerPhoneNo.TextChanged += new EventHandler(txtCustomerPhoneNo_TextChanged);

                    }
                }
            }
            catch
            {


            }
        }

        private bool IsBookingExistForContact()
        {
            bool isExist = false;

            try
            {
                var data1 = General.GetQueryable<Booking>(c => c.CustomerMobileNo != null && c.CustomerPhoneNo != null);


                string telNo = txtCustomerPhoneNo.Text.Trim().ToLower();
                string mobNo = txtCustomerMobileNo.Text.Trim().ToLower();


                isExist = data1.Where(a => (a.CustomerPhoneNo == telNo || telNo == string.Empty) &&
                             (a.CustomerMobileNo == mobNo || mobNo == string.Empty)).Count() > 0;




            }
            catch
            {


            }
            return isExist;

        }




        private decimal CalculateDistance(string origin, string destination)
        {
            decimal miles = 0.00m;

            try
            {

                string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + "&sensor=false";



                XmlTextReader reader = new XmlTextReader(url2);
                reader.WhitespaceHandling = WhitespaceHandling.Significant;
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
                DataTable dt = ds.Tables["distance"];

                if (dt != null)
                {
                    //  var rows = dt.Rows.OfType<DataRow>().Where(c => c[0].ToStr().Trim() == c[1].ToStr().Strip("m").Trim()).ToList();

                    decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                    decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                    decimal milKM = 0.621m;
                    decimal milMeter = 0.00062137119m;

                    miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                }
                else
                {

                    mileageError = true;

                }

            }
            catch
            {

                mileageError = true;
            }


            return miles;
        }


        private void MileageError()
        {
            lblMap.Text = "Mileage not found";

        }




        private decimal CalculateTotalDistance(string origin, string via, string destination)
        {


            decimal miles = 0.00m;

            //     string via = string.Join("|", grdVia.Rows.Select(c => c.Cells[COLS.VIALOCATIONVALUE].Value.ToStr()).ToArray<string>());


            //if (UseGoogleMap)
            //{
            origin = General.GetPostCodeMatch(origin);
            destination = General.GetPostCodeMatch(destination);

            string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + "&sensor=false";

            if (!string.IsNullOrEmpty(via))
            {
                url2 += "&waypoints=" + via;

            }

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
            //}

            return miles;

        }



        private decimal CalculateTotalDistance(string origin, string[] via, string destination)
        {


            decimal miles = 0.00m;

            origin = General.GetPostCodeMatch(origin);
            destination = General.GetPostCodeMatch(destination);

            string actualOrigin = origin;
            string actualDestination = destination;
            for (int i = 0; i < via.Count(); i++)
            {
                if (i == 0)
                {

                    destination = General.GetPostCodeMatch(via[i].ToStr());
                }
                else
                {
                    origin = General.GetPostCodeMatch(via[i - 1].ToStr());

                    destination = General.GetPostCodeMatch(via[i].ToStr());
                }


                miles += CalculateDistance(origin, destination);

            }

            if (via.Count() > 0)
            {
                origin = destination;
            }
            else
            {
                origin = actualOrigin;
            }

            destination = actualDestination;


            miles += CalculateDistance(origin, destination);

            return miles;

        }

        private void ddlFromLocType_SelectedIndexChange(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillFromLocations();
        }

        private void ddlToLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillToLocations();
        }

        private void btnDetailMap_Click(object sender, EventArgs e)
        {
            int? locTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
            int? locId = ddlFromLocation.SelectedValue.ToIntorNull();


            string origin = "";
            string destination = "";

            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId==Enums.LOCATION_TYPES.BASE)
                origin = txtFromAddress.Text.Trim();
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
                origin = txtFromPostCode.Text.Trim();
            else
                origin = General.GetPostCodeMatch(ddlFromLocation.Text.Trim());

            if (origin == string.Empty)
            {
                ENUtils.ShowMessage("Map not found");
                return;
            }


            locTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            locId = ddlToLocation.SelectedValue.ToIntorNull();


            if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
                destination = txtToAddress.Text.Trim();
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
                destination = txtToPostCode.Text.Trim();
            else
                destination = General.GetPostCodeMatch(ddlToLocation.Text.Trim());




            if (destination == string.Empty)
            {
                ENUtils.ShowMessage("Map not found");
                return;
            }


         //   origin = General.GetPostCodeMatch(origin);
          //  destination = General.GetPostCodeMatch(destination);




            if (General.GetPostCodeMatch(origin) == string.Empty || General.GetPostCodeMatch(destination) == string.Empty)
            {
                ENUtils.ShowMessage("Map not found");
                return;
            }




            string[] viaLocs = grdVia.Rows.Select(c => c.Cells[COLS.VIALOCATIONVALUE].Value.ToStr()).ToArray<string>();

            if (UseGoogleMap)
            {




                string viaLocations = "";
                if (viaLocs.Count() > 0)
                {
                    viaLocs = viaLocs.Select(c => General.GetPostCodeMatch(c)).Where(c => c.Length > 0).ToArray<string>();
                    if (viaLocs.Count() == 0)
                    {
                        ENUtils.ShowMessage("Map not found");
                        return;

                    }
                    viaLocations = "+to:" + string.Join("+to:", viaLocs) + "+to:";

                }


                frmMap frm = new frmMap(origin, viaLocs, destination);
                frm.Show();
            }
          



        }




        #region Overridden Methods


        public override void Save()
        {

        //    try
        //    {

        //        if (dtpPickupDate.Value == null || dtpPickupTime.Value == null)
        //        {
        //            ENUtils.ShowMessage("Required : Pickup Date Time");
        //            return;
        //        }



        //        bool IsAddMode = false;
        //        DateTime nowDate = DateTime.Now;



        //        if (objMaster.PrimaryKeyValue == null)
        //        {


        //            objMaster.New();

        //            objMaster.Current.BookingDate = nowDate;
        //            IsAddMode = true;

        //            //      objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;

        //        }
        //        else
        //        {
        //            objMaster.Edit();

        //            (new TaxiDataContext()).stp_Insert_BookingUpdates(txtBookingNo.Text);
        //        }


        //        if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && this.objBookingPayment != null)
        //        {
        //            if (objMaster.Current.BookingPayment == null)
        //                objMaster.Current.BookingPayment = new Booking_Payment();



        //            objMaster.Current.BookingPayment.OrderDescription = this.objBookingPayment.OrderDescription;
        //            objMaster.Current.BookingPayment.NameOnCard = this.objBookingPayment.NameOnCard;
        //            objMaster.Current.BookingPayment.CardNumber = this.objBookingPayment.CardNumber;
        //            objMaster.Current.BookingPayment.CardExpiryDate = this.objBookingPayment.CardExpiryDate;
        //            objMaster.Current.BookingPayment.CardStartDate = this.objBookingPayment.CardStartDate;
        //            objMaster.Current.BookingPayment.IssueNumber = this.objBookingPayment.IssueNumber;
        //            objMaster.Current.BookingPayment.CV2 = this.objBookingPayment.CV2;
        //            objMaster.Current.BookingPayment.Email = this.objBookingPayment.Email;
        //            objMaster.Current.BookingPayment.PhoneNo = this.objBookingPayment.PhoneNo;
        //            objMaster.Current.BookingPayment.Address = this.objBookingPayment.Address;
        //            objMaster.Current.BookingPayment.City = this.objBookingPayment.City;
        //            objMaster.Current.BookingPayment.State = this.objBookingPayment.State;
        //            objMaster.Current.BookingPayment.PostCode = this.objBookingPayment.PostCode;
        //            objMaster.Current.BookingPayment.Status = this.objBookingPayment.Status;
        //            objMaster.Current.BookingPayment.AuthCode = this.objBookingPayment.AuthCode;
        //        }




        //        objMaster.Current.CashRate = numComcab_Cash.Value.ToDecimal();
        //        objMaster.Current.AccountRate = numComcab_Account.Value.ToDecimal();
        //        objMaster.Current.ExtraMile = numComcab_ExtraMile.Value.ToDecimal();
        //        objMaster.Current.WaitingMins = numComcab_WaitingMin.Value.ToDecimal();




        //        // 31 Jan 13 Changes. Booking Type

        //        //  objMaster.Current.IsVIPBooking = chkVipBooking.Checked;
        //        objMaster.Current.BookingTypeId = ddlBookingType.SelectedValue.ToIntorNull();


        //        //    objMaster.Current.RemovalDescription = "";
        //        ///    objMaster.Current.RemovalCode = "";
        //        //

        //        int? driverId = ddlDriver.SelectedValue.ToIntorNull();
        //        //prevDriverId = objMaster.Current.DriverId;                    
        //        objMaster.Current.DriverId = driverId;
        //        //newDriverId = driverId;

        //        objMaster.Current.ReturnDriverId = ddlReturnDriver.SelectedValue.ToIntorNull();
        //        objMaster.Current.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
        //        objMaster.Current.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();
        //        objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
        //        objMaster.Current.DepartmentId = ddlDepartment.SelectedValue.ToLongorNull();
        //        objMaster.Current.CostCenterId = ddlCostCenter.SelectedValue.ToIntorNull();

        //        objMaster.Current.IsCompanyWise = chkIsCompanyRates.Checked;
        //        //  objMaster.Current.JourneyTypeId = opt_JOneWay.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? 1 : 2;

        //        int journeyTypeId = Enums.JOURNEY_TYPES.ONEWAY;

        //        if (opt_JReturnWay.ToggleState == ToggleState.On)
        //        {
        //            journeyTypeId = Enums.JOURNEY_TYPES.RETURN;

        //        }
        //        else if (opt_WaitandReturn.ToggleState == ToggleState.On)
        //        {

        //            journeyTypeId = Enums.JOURNEY_TYPES.WAITANDRETURN;
        //        }


        //        objMaster.Current.JourneyTypeId = journeyTypeId;

        //        // Quotation

        //        objMaster.Current.IsQuotation = chkQuotation.Checked;

        //        //


        //        objMaster.Current.OrderNo = txtOrderNo.Text.ToStr().Trim();
        //        objMaster.Current.PupilNo = txtPupilNo.Text.ToStr().Trim();


        ////        objMaster.Current.ProviderName = txtProvider.Text.Trim();


        //        objMaster.Current.PickupDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay).ToDateTime();


        //        if (dtpReturnPickupDate.Value != null && dtpReturnPickupTime.Value != null)
        //        {
        //            objMaster.Current.ReturnPickupDateTime = dtpReturnPickupDate.Value.ToDateorNull() + dtpReturnPickupTime.Value.Value.TimeOfDay;
        //        }
        //        else
        //            objMaster.Current.ReturnPickupDateTime = null;

        //        // New. 31 jan 13 For Return Booking.
        //        if (objMaster.Current.MasterJobId != null && objMaster.Current.Booking1 != null)
        //        {
        //            objMaster.Current.Booking1.ReturnPickupDateTime = objMaster.Current.PickupDateTime;

        //        }





        //        objMaster.Current.NoofPassengers = num_TotalPassengers.Value.ToInt();
        //        objMaster.Current.NoofLuggages = numTotalLuggages.Value.ToInt();

        //        //objMaster.Current.NoofHandLuggages = numTotalHandLuggages.Value.ToInt();
        //        objMaster.Current.FareRate = numFareRate.Value.ToDecimal();
        //        objMaster.Current.ReturnFareRate = numReturnFare.Value.ToDecimal();
        //        objMaster.Current.CompanyPrice = numCompanyFares.Value.ToDecimal();

        //        objMaster.Current.ParkingCharges = numParkingChrgs.Value.ToDecimal();
        //        objMaster.Current.WaitingCharges = numWaitingChrgs.Value.ToDecimal();
        //        objMaster.Current.ExtraDropCharges = numExtraChrgs.Value.ToDecimal();
        //        objMaster.Current.MeetAndGreetCharges = numMeetCharges.Value.ToDecimal();
        //        objMaster.Current.CongtionCharges = numCongChrgs.Value.ToDecimal();

        //        objMaster.OldCustomerName = objMaster.Current.CustomerName.ToStr().Trim();
        //        objMaster.Current.CustomerId = ddlCustomerName.SelectedValue.ToIntorNull();
        //        objMaster.Current.CustomerName = ddlCustomerName.Text.ToStr().Trim();

        //        //   objMaster.Current.CustomerEmail=txtCustomerEmail.Text.Trim();
        //        objMaster.Current.CustomerPhoneNo = txtCustomerPhoneNo.Text.Trim();
        //        objMaster.Current.CustomerMobileNo = txtCustomerMobileNo.Text.Trim();
        //        objMaster.Current.SpecialRequirements = txtSpecialRequirements.Text.Trim();


        //        int FromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
        //        int ToLocTypeId = ddlToLocType.SelectedValue.ToInt();

        //        objMaster.Current.FromLocTypeId = FromLocTypeId;
        //        objMaster.Current.ToLocTypeId = ToLocTypeId;
        //        objMaster.Current.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
        //        objMaster.Current.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();


        //        if (FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromLocTypeId == Enums.LOCATION_TYPES.BASE)
        //            objMaster.Current.FromAddress = txtFromAddress.Text.Trim();

        //        else if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
        //            objMaster.Current.FromAddress = txtFromPostCode.Text.Trim();
        //        else
        //        {
        //            objMaster.Current.FromAddress = ddlFromLocation.Text.Trim();
        //        }



        //        objMaster.Current.FromDoorNo = txtFromFlightDoorNo.Text.Trim();
        //        objMaster.Current.FromStreet = txtFromStreetComing.Text.Trim();
        //        objMaster.Current.FromPostCode = txtFromPostCode.Text.Trim();



        //        if (ToLocTypeId == Enums.LOCATION_TYPES.ADDRESS || ToLocTypeId == Enums.LOCATION_TYPES.BASE)
        //            objMaster.Current.ToAddress = txtToAddress.Text.StripNewLine().Trim();

        //        else if (ToLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
        //            objMaster.Current.ToAddress = txtToPostCode.Text.Trim();
        //        else
        //        {
        //            objMaster.Current.ToAddress = ddlToLocation.Text.Trim();
        //        }


        //        objMaster.Current.ToDoorNo = txtToFlightDoorNo.Text.Trim();
        //        objMaster.Current.ToStreet = txtToStreetComing.Text.Trim();
        //        objMaster.Current.ToPostCode = txtToPostCode.Text.Trim();


        //        objMaster.Current.AutoDespatch = chkAutoDespatch.Checked;

        //        // objMaster.Current.DisableDespatchSMS = chkDisabledSMS.Checked;

        //        objMaster.Current.DisableDriverSMS = chkDisableDriverSMS.Checked;
        //        objMaster.Current.DisablePassengerSMS = chkDisablePassengerSMS.Checked;
                

        //        objMaster.Current.IsCommissionWise = chkIsCommissionWise.Checked;
        //        objMaster.Current.DriverCommission = numDriverCommission.Value.ToDecimal();
        //        objMaster.Current.DriverCommissionType = ddlCommissionType.SelectedValue.ToStr().Trim();
        //     //   objMaster.Current.TrashBookings = false;

        //        objMaster.Current.DistanceString = lblMap.Text.ToStr();

        //        string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
        //        IList<Booking_ViaLocation> savedList = objMaster.Current.Booking_ViaLocations;
        //        List<Booking_ViaLocation> listofDetail = (from r in grdVia.Rows
        //                                                  select new Booking_ViaLocation
        //                                                 {
        //                                                     Id = r.Cells[COLS.ID].Value.ToLong(),
        //                                                     BookingId = r.Cells[COLS.MASTERID].Value.ToLong(),
        //                                                     ViaLocTypeId = r.Cells[COLS.FROMVIALOCTYPEID].Value.ToIntorNull(),
        //                                                     ViaLocTypeLabel = r.Cells[COLS.FROMTYPELABEL].Value.ToStr(),
        //                                                     ViaLocTypeValue = r.Cells[COLS.FROMTYPEVALUE].Value.ToStr(),

        //                                                     ViaLocId = r.Cells[COLS.VIALOCATIONID].Value.ToIntorNull(),
        //                                                     ViaLocLabel = r.Cells[COLS.VIALOCATIONLABEL].Value.ToStr(),
        //                                                     ViaLocValue = r.Cells[COLS.VIALOCATIONVALUE].Value.ToStr()

        //                                                 }).ToList();


        //        Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

        //        objMaster.Save();

        //        saved = true;
        //        string mobileNo = objMaster.Current.CustomerMobileNo.ToStr().Trim();



        //        if (objMaster.Current.AutoDespatch.ToBool() == false && driverId != null
        //            && (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.WAITING || objMaster.Current.BookingStatusId == null))
        //        {

        //            frm = new frmDespatchJob(objMaster.Current);
        //            frm.ShowDialog();

        //        }


        //        // Advance Booking Confirmation Text
        //        // enableAdvBookingText = AppVars.objPolicyConfiguration.EnableAdvanceBookingSMSConfirmation.ToBool();

        //        DateTime? pickupdateTime = objMaster.Current.PickupDateTime;
        //        if (AppVars.objPolicyConfiguration.EnableAdvanceBookingSMSConfirmation.ToBool()
        //            && IsAddMode && pickupdateTime != null && objMaster.Current.IsQuotation.ToBool() == false)
        //        {
        //            string pickupSpan = string.Format("{0:HH:mm}", pickupdateTime);

        //            TimeSpan picktime = TimeSpan.Parse(pickupSpan);

        //            string nowP = string.Format("{0:HH:mm}", nowDate);
        //            TimeSpan nowSpantime = TimeSpan.Parse(nowP);

        //            int afterMins = AppVars.objPolicyConfiguration.AdvanceBookingSMSConfirmationMins.ToInt();
        //            int minDifference = picktime.Subtract(nowSpantime).Minutes;
        //            int dayDiff = pickupdateTime.Value.Date.Subtract(DateTime.Now.Date).Days;
        //            if (afterMins >= 0 && (dayDiff > 0 || minDifference >= afterMins || minDifference < 0))
        //            {
        //                string msg = AppVars.objPolicyConfiguration.AdvanceBookingSMSText.ToStr().Trim();
        //                object propertyValue = string.Empty;

        //                foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
        //                {
        //                    switch (tag.TagObjectName)
        //                    {
        //                        case "booking":

        //                            if (tag.TagPropertyValue.Contains('.'))
        //                            {

        //                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

        //                                object parentObj = objMaster.Current.GetType().GetProperty(val[0]).GetValue(objMaster.Current, null);

        //                                if (parentObj != null)
        //                                {
        //                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
        //                                }
        //                                else
        //                                    propertyValue = string.Empty;


        //                                break;
        //                            }
        //                            else
        //                            {
        //                                propertyValue = objMaster.Current.GetType().GetProperty(tag.TagPropertyValue).GetValue(objMaster.Current, null);
        //                            }


        //                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
        //                            {
        //                                propertyValue = objMaster.Current.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objMaster.Current, null);
        //                            }
        //                            break;


        //                        default:
        //                            propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
        //                            break;

        //                    }


        //                    msg = msg.Replace(tag.TagMemberValue,
        //                        tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

        //                }


        //                msg.Replace("\n\n", "\n");

        //                string refMsg = "";
        //                General.SendAdvanceBookingSMS(mobileNo, ref refMsg, msg);

        //            }
        //        }




        //        //if(prevDriverId==null || (prevDriverId!=newDriverId))
        //        //{
        //        //      if(newDriverId!=null)
        //        //           General.UpdateDriverQueue(newDriverId.ToIntorNull());

        //        //}


        //        if (frm != null && frm.SuccessDespatched)
        //        {
        //           // General.RefreshDriversGrids();
        //            //  General.RefreshWaitingDrivers();
        //            // AppVars.frmDashBoard.RefreshDriverGrids();

        //        }

              



        //        IsSave = true;



        //    }
        //    catch (Exception ex)
        //    {
        //        IsSave = false;
        //        if (objMaster.Errors.Count > 0)
        //            ENUtils.ShowMessage(objMaster.ShowErrors());
        //        else
        //        {
        //            ENUtils.ShowMessage(ex.Message);
        //        }
        //    }
        }










        public override void DisplayRecord()
        {
            if (objTrash == null) return;


            try
            {


                btnCancelBooking.Enabled = true;
                btnPrintJob.Enabled = true;

                btnSearch.Enabled = false;
                btnMultiBooking.Enabled = false;

                btnMultiVehicle.Visible = false;


               

                // 31 jan 13
                ddlBookingType.SelectedValue = objTrash.BookingTypeId;
                //   txtRemovalDescription.Text = "";
                //     txtRemovalCode.Text ="";

                //
             //   chkQuotation.Checked = objTrash.IsQuotation.ToBool();

                txtBookingNo.Text = objTrash.BookingNo.ToStr();
                txtBookingDate.Text = string.Format("{0:dd/MM/yyyy}", objTrash.BookingDate.ToDate());

                ddlFromLocType.SelectedValue = objTrash.FromLocTypeId;
                ddlToLocType.SelectedValue = objTrash.ToLocTypeId;
                ddlFromLocation.SelectedValue = objTrash.FromLocId;
                ddlToLocation.SelectedValue = objTrash.ToLocId;

                ddlVehicleType.SelectedValue = objTrash.VehicleTypeId;

                ddlReturnDriver.SelectedValue = objTrash.ReturnDriverId;

                ddlCustomerName.Text = objTrash.CustomerName;
                txtCustomerMobileNo.Text = objTrash.CustomerMobileNo;
                txtCustomerPhoneNo.Text = objTrash.CustomerPhoneNo;

                txtSpecialRequirements.Text = objTrash.SpecialRequirements;


                int journeyTypeId = objTrash.JourneyTypeId.ToInt();

                if (journeyTypeId == Enums.JOURNEY_TYPES.ONEWAY)
                    opt_JOneWay.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                else if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN)
                {
                    opt_JReturnWay.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                }
                else if (journeyTypeId == Enums.JOURNEY_TYPES.WAITANDRETURN)
                {

                    opt_WaitandReturn.ToggleState = ToggleState.On;
                }


                if (objTrash.MasterJobId != null)
                {
                    pnlReturnJobNo.Visible = true;
                    //pnlReturnJobNo.Text = "Return From Job # " + objTrash.Booking1.DefaultIfEmpty().BookingNo.ToStr();


                }

                chkIsCompanyRates.Checked = objTrash.IsCompanyWise.ToBool();
                ddlCompany.SelectedValue = objTrash.CompanyId;

                ddlPaymentType.SelectedValue = objTrash.PaymentTypeId;

                //if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD
                //    && !string.IsNullOrEmpty(objTrash.BookingPayment.DefaultIfEmpty().AuthCode))
                //{
                //    btnPayment.Enabled = false;
                //    lblPaymentHeading.Text += "(Payment AuthCode is : " + objTrash.BookingPayment.DefaultIfEmpty().AuthCode + ")";
                //}






                txtOrderNo.Text = objTrash.OrderNo.ToStr();
                txtPupilNo.Text = objTrash.PupilNo.ToStr();
                ddlDepartment.SelectedValue = objTrash.DepartmentId;
                ddlCostCenter.SelectedValue = objTrash.CostCenterId;

                dtpPickupDate.Value = objTrash.PickupDateTime.ToDate();
                dtpPickupTime.Value = objTrash.PickupDateTime;
                dtpReturnPickupDate.Value = objTrash.ReturnPickupDateTime.ToDateorNull();
                dtpReturnPickupTime.Value = objTrash.ReturnPickupDateTime.ToDateTimeorNull();


          //      txtProvider.Text = objMaster.Current.ProviderName.ToStr().Trim();

                num_TotalPassengers.Value = objTrash.NoofPassengers.ToDecimal();
                numTotalLuggages.Value = objTrash.NoofLuggages.ToDecimal();

                numFareRate.Value = objTrash.FareRate.ToDecimal();
                numReturnFare.Value = objTrash.ReturnFareRate.ToDecimal();
                numCompanyFares.Value = objTrash.CompanyPrice.ToDecimal();

                numParkingChrgs.Value = objTrash.ParkingCharges.ToDecimal();
                numWaitingChrgs.Value = objTrash.WaitingCharges.ToDecimal();
                numExtraChrgs.Value = objTrash.ExtraDropCharges.ToDecimal();
                numMeetCharges.Value = objTrash.MeetAndGreetCharges.ToDecimal();
                numCongChrgs.Value = objTrash.CongtionCharges.ToDecimal();

                numTotalChrgs.Value = objTrash.TotalCharges.ToDecimal();


                numComcab_Cash.Value = objTrash.CashRate.ToDecimal();
                numComcab_Account.Value = objTrash.AccountRate.ToDecimal();
                numComcab_ExtraMile.Value = objTrash.ExtraMile.ToDecimal();
                numComcab_WaitingMin.Value = objTrash.WaitingMins.ToDecimal();



                txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = objTrash.FromAddress.ToStr();
                txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtFromFlightDoorNo.Text = objTrash.FromDoorNo.ToStr();
                txtFromStreetComing.Text = objTrash.FromStreet.ToStr();

                //     txtFromPostCode.TextChanged -= new EventHandler(txtPostCode_TextChanged);
                txtFromPostCode.Text = objTrash.FromPostCode.ToStr();
                //    txtFromPostCode.TextChanged += new EventHandler(txtPostCode_TextChanged);

                txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = objTrash.ToAddress.ToStr();
                txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                txtToFlightDoorNo.Text = objTrash.ToDoorNo.ToStr();
                txtToStreetComing.Text = objTrash.ToStreet.ToStr();

                //    txtToPostCode.TextChanged -= new EventHandler(txtPostCode_TextChanged);
                txtToPostCode.Text = objTrash.ToPostCode.ToStr();
                //    txtToPostCode.TextChanged += new EventHandler(txtPostCode_TextChanged);



                chkIsCommissionWise.Checked = objTrash.IsCommissionWise.ToBool();
                ddlCommissionType.SelectedValue = objTrash.DriverCommissionType.ToStr().Trim();
                numDriverCommission.Value = objTrash.DriverCommission.ToDecimal();




              //  chkAutoDespatch.Checked = objTrash.AutoDespatch.ToBool();
                DateTime? pickUpDate = objTrash.PickupDateTime;


                if (objTrash.AutoDespatchTime != null)
                {
                    DateTime? autoDespatchDate = objTrash.AutoDespatchTime;
                    int mins = pickUpDate.Value.TimeOfDay.Subtract(autoDespatchDate.Value.TimeOfDay).Minutes.ToInt();
                //    numBeforeMinutes.Value = mins < 0 ? 10 : mins;
                }
                else
                 //   numBeforeMinutes.Value = 10;

                //    ShowAutoDespatchLabels(AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool());


                if (objTrash.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED)
                {
                   // pnlAutoDespatch.Enabled = false;
                  //  chkQuotation.Enabled = false;
                    //   btnProofOfDelivery.Visible = true;
                }



                int fromLocTypeId = objTrash.FromLocTypeId.ToInt();

                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                    txtFromAddress.Focus();
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    txtFromPostCode.Focus();
                else
                    ddlFromLocation.Focus();


            //    chkDisableDriverSMS.Checked = objTrash.DisableDriverSMS.ToBool();
            //    chkDisablePassengerSMS.Checked = objTrash.DisablePassengerSMS.ToBool();


                DisplayBooking_ViaLocations();

                //InitializeMap();

             //   th = new System.Threading.Thread(new ThreadStart(DisplayBooking_Map));
              //  th.IsBackground = true;
             //   th.Start();

                if(objTrash.JobDeletedBy.ToStr().Length > 0)
                lblFooter.Text="Job deleted by : "+ objTrash.JobDeletedBy+ " on " +string.Format("{0:dd/MMM/yyyy HH:mm}",objTrash.JobDeletedOn);


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }




        delegate void DisplayMilesHandler();

        private void DisplayBooking_ViaLocations()
        {

            var list = General.GetQueryable<Booking_ViaLocations_Trash>(c => c.BookingId == objTrash.Id).ToList();



            GridViewRowInfo row = null;
            foreach (var item in list)
            {
                row = grdVia.Rows.AddNew();
                row.Cells[COLS.ID].Value = item.Id;
                row.Cells[COLS.MASTERID].Value = item.BookingId;
                row.Cells[COLS.FROMTYPELABEL].Value = item.ViaLocTypeLabel;
                row.Cells[COLS.FROMTYPEVALUE].Value = item.ViaLocTypeValue;
                row.Cells[COLS.FROMVIALOCTYPEID].Value = item.ViaLocTypeId;

                row.Cells[COLS.VIALOCATIONID].Value = item.ViaLocId;
                row.Cells[COLS.VIALOCATIONLABEL].Value = item.ViaLocLabel;
                row.Cells[COLS.VIALOCATIONVALUE].Value = item.ViaLocValue;

            }

            ClearViaDetails();


            btnSelectVia.ToggleState = grdVia.RowCount > 0 ? ToggleState.On : ToggleState.Off;
        }


        #endregion

        private void opt_JOneWay_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {

            if (CheckReturnBooking(args.NewValue) == false)
                args.Canceled = true;


        }

        private bool CheckReturnBooking(ToggleState toggle)
        {
            bool rtn = true;

            try
            {
                if (objMaster.PrimaryKeyValue != null && objMaster.Current.BookingReturns.Count > 0 && toggle == ToggleState.On)
                {
                    if (DialogResult.OK == MessageBox.Show("There is a Return Job '" + objMaster.Current.BookingReturns.FirstOrDefault().DefaultIfEmpty().BookingNo
                                                        + "' exist againt this Job" + Environment.NewLine
                                                        + "If you press OK then Return Job will be delete" + Environment.NewLine
                                                        + "Are you sure you want to Delete its Return Job?", "Booking and Dispatch System"
                                                            , MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {

                        new TaxiDataContext().stp_DeleteReturnBooking(objMaster.Current.BookingReturns.FirstOrDefault().DefaultIfEmpty().MasterJobId);

                    }
                    else
                    {
                        rtn = false;

                    }


                }
            }
            catch (Exception ex)
            {
                rtn = false;
                ENUtils.ShowMessage(ex.Message);

            }

            return rtn;


        }


        private void SetJourneyWise(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                lblReturnDriver.Visible = false;
                ddlReturnDriver.Visible = false;
                ddlReturnDriver.SelectedValue = null;

                lblReturnPickupDate.Visible = false;
                lblReturnPickupTime.Visible = false;
                dtpReturnPickupDate.Visible = false;
                dtpReturnPickupTime.Visible = false;
                dtpReturnPickupDate.Value = null;
                dtpReturnPickupTime.Value = null;

                numReturnFare.Enabled = false;
                numReturnFare.Value = 0;
            }
            else
            {

                lblReturnDriver.Visible = true;
                ddlReturnDriver.Visible = true;
                ddlReturnDriver.SelectedValue = null;

                lblReturnPickupDate.Visible = true;
                lblReturnPickupTime.Visible = true;

                dtpReturnPickupDate.Visible = true;
                dtpReturnPickupTime.Visible = true;
                dtpReturnPickupDate.Value = DateTime.Now.ToDate();
                dtpReturnPickupTime.Value = DateTime.Now;



                numReturnFare.Enabled = true;

            }


        }

        private void chkIsCompanyRates_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            UseCompanyRates(args.ToggleState);
        }

        private void UseCompanyRates(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                SetAccountPaymentType();
                numCompanyFares.Enabled = true;
            }
            else
            {
                SetCashPaymentType();

                txtOrderNo.Text = string.Empty;
                txtPupilNo.Text = string.Empty;
                pnlOrderNo.Visible = false;
                numCompanyFares.Value = 0;
                numCompanyFares.Enabled = false;
            }
            ddlCompany.Enabled = toggle == ToggleState.On;
            ddlCompany.SelectedValue = toggle == ToggleState.Off ? null : ddlCompany.SelectedValue;
        }

        private void SetAccountPaymentType()
        {
            ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.BANK_ACCOUNT;

        }


        private void SetCashPaymentType()
        {
            ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;


        }

        private void btnPickFares_Click(object sender, EventArgs e)
        {
            CalculateTotalFares();
        }


        private void CalculateTotalFares()
        {

            try
            {
                lblMap.Text = string.Empty;
                string frmaddress = txtFromAddress.Text == null ? "null" : txtFromAddress.Text;
                string Toaddress = txtToAddress.Text == "" ? "null" : txtToAddress.Text;

                mileageError = false;
                CalculateFares();

                string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + frmaddress + "&destination=" + Toaddress + "&sensor=false";


                XmlTextReader reader = new XmlTextReader(url2);
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
                string Status = ds.Tables[0].Rows[0]["status"].ToString();
                if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
                //if (Status == "ZERO_RESULTS")
                {
                    url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
                    MileageError();
                }
                else
                {

                    DataTable dt = ds.Tables["duration"];

                    DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
                    string time = row.ItemArray[1].ToString();

                    decimal miles = milesList.Sum();
                    if (miles > 10000 || mileageError)
                        MileageError();
                    else
                    {
                        lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
                    }
                }

                CalculateTotalCharges();


            }
            catch 
            {

            }
        }




        private bool CalculateFares()
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
            List<string> errors = new List<string>();

            int orderId = 2;
            var viaList = (from r in grdVia.Rows
                           select new
                           {
                               OrderNo = orderId++,
                               FromTypeId = r.Cells[COLS.FROMVIALOCTYPEID].Value.ToInt(),
                               LocId = r.Cells[COLS.VIALOCATIONID].Value.ToInt(),
                               LocAddressPostCode = r.Cells[COLS.VIALOCATIONVALUE].Value.ToStr()
                           }).ToList();



            if (vehicleTypeId == null)
            {
                errors.Add("Required : Vehicle Type");

            }

            //if (fromLocationId == null && string.IsNullOrEmpty(fromPostCode) && string.IsNullOrEmpty(fromAddress))
            //{

            //    errors.Add("Required : From Address");

            //}


            //if (toLocationId==null && string.IsNullOrEmpty(toPostCode) && string.IsNullOrEmpty(toAddress))
            //{
            //   errors.Add("Required : To Address");

            //}


            if (errors.Count > 0)
            {
                ENUtils.ShowMessage(string.Join(Environment.NewLine, errors.Select(c => c).ToArray<string>()));
                return false;
            }



            InitializeMap();





            
            int tempFromLocId = 0;
            int tempToLocId = 0;
            string tempFromPostCode = "";
            string tempToPostCode = "";
            string errorMsg = string.Empty;
            decimal deadMileage = AppVars.objPolicyConfiguration.DeadMileage.ToDecimal();

            decimal fareVal = 0.00m;
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
             //   fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, true, false, null, ref deadMileage);


               

                if (errorMsg == "Error")
                    break;

            }


         

            if (errorMsg == "Error")
            {
                numFareRate.Value = 0;
                return false;
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
           // fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, false, null, ref deadMileage);


            if (errorMsg == "Error")
            {

                numFareRate.Value = 0;
                return false;
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

            // Add Airport Pickup Charges If Pickup Point is From Airport...
            if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();

            numFareRate.Value = dd;


            if (companyId != 0)
            {
                numCompanyFares.Value = dd;
            }

            if (opt_JReturnWay.ToggleState == ToggleState.On)
            {
                // numReturnFare.Value = numFareRate.Value;

                numReturnFare.Value = numFareRate.Value - ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForReturnedJourneyPercent.ToInt()) / 100);

                if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    numReturnFare.Value += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();


            }
            else if (opt_WaitandReturn.ToggleState == ToggleState.On)
            {
                numFareRate.Value = numFareRate.Value + ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForWRJourneyPercent.ToInt()) / 100);
            }
            else
            {
                numReturnFare.Value = 0;
            }



            return true;

        }


        private bool mileageError = false;


        private void btnSaveNew_Click(object sender, EventArgs e)
        {
            //this.SaveAndClose();
            //if (pnlAccpassword.Visible == true)
            //{
            //    int? companyId = ddlCompany.SelectedValue.ToIntorNull();

            //    Gen_Company obj = General.GetObject<Gen_Company>(c => c.Id == companyId);
            //    string AccountPassword = obj.PasswordAccount.ToStr();

            //    if (txtAccPassword.Text.ToStr().ToLower() == AccountPassword.ToStr().ToLower())
            //    {
            //        this.SaveAndClose();
            //    }
            //    else
            //    {
            //        RadMessageBox.Show("Please Enter Correct Company Password!");
            //    }
            //}
            //else
            //{
            //    this.SaveAndClose();
            //}
        }



        private void radToggleButton1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                btnSelectVia.Text = "Hide Via Point";
                pnlVia.Visible = true;


                pnlBottom.Location = this.PnlNewBottomLocation;
            }
            else
            {
                btnSelectVia.Text = "Show Via Point";
                pnlVia.Visible = false;

                pnlBottom.Location = this.PnlOldBottomLocation;

            }
        }

        private void ddlViaFromLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillViaLocations();
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



        private void btnAddVia_Click(object sender, EventArgs e)
        {
            AddViaPoint();
        }

        private void AddViaPoint()
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
            //if (grdVia.CurrentRow == null &&
            //    grdVia.Rows.Count(c =>
            //                            c.Cells[COLS_DETAILS.FROMLOCATIONID].Value.ToInt() == fromLocId
            //                        && c.Cells[COLS_DETAILS.TOLOCATIONID].Value.ToInt() == toLocId
            //                         ) > 0)
            //{
            //    ENUtils.ShowMessage("From Location and To Location already exist");
            //    ddlFromLocation.Focus();
            //    return;

            //}

            if (grdVia.CurrentRow != null && grdVia.CurrentRow is GridViewNewRowInfo)
                grdVia.CurrentRow = null;


            if (grdVia.CurrentRow != null)
                row = grdVia.CurrentRow;
            else
                row = grdVia.Rows.AddNew();



            row.Cells[COLS.FROMVIALOCTYPEID].Value = LocTypeId;
            row.Cells[COLS.FROMTYPELABEL].Value = fromViaLabel;
            row.Cells[COLS.FROMTYPEVALUE].Value = fromViaValue;

            row.Cells[COLS.VIALOCATIONID].Value = toViaLocId;
            row.Cells[COLS.VIALOCATIONLABEL].Value = ToViaLocLabel;
            row.Cells[COLS.VIALOCATIONVALUE].Value = toViaLoc;



            ClearViaDetails();

        }


        private void ClearViaDetails()
        {
            ddlViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
            grdVia.CurrentRow = null;
            txtViaAddress.Text = string.Empty;
            txtviaPostCode.Text = string.Empty;
            ddlViaLocation.SelectedValue = null;
        }

        private void grdVia_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdVia.CurrentRow != null && grdVia.CurrentRow is GridViewDataRowInfo)
            {
                GridViewRowInfo row = grdVia.CurrentRow;

                ddlViaFromLocType.SelectedValue = row.Cells[COLS.FROMVIALOCTYPEID].Value.ToInt();

                string locValue = row.Cells[COLS.VIALOCATIONVALUE].Value.ToStr();

                txtViaAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtViaAddress.Text = locValue;
                txtViaAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtviaPostCode.TextChanged -= new EventHandler(txtviaPostCode_TextChanged);
                txtviaPostCode.Text = locValue;
                txtviaPostCode.TextChanged += new EventHandler(txtviaPostCode_TextChanged);

                ddlViaLocation.SelectedValue = row.Cells[COLS.VIALOCATIONID].Value.ToInt();


            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBooking();

        }


        private void SearchBooking()
        {
            //try
            //{


            //    frmSearchBooking frm = new frmSearchBooking(ddlCustomerName.Text, txtCustomerPhoneNo.Text.Trim(), txtCustomerMobileNo.Text.Trim());
            //    frm.ShowDialog();



            //    if (frm.IsSelected)
            //    {
            //        PickBookingComplete(frm.CustomerName, frm.phoneNo, frm.mobileNo, frm.fromLocTypeId, frm.toLocTypeId, frm.fromLocId, frm.toLocId, frm.from, frm.to, frm.fare, false, frm.bookingTypeId);
            //    }



            //}
            //catch (Exception ex)
            //{

            //}

        }






        private void ddlDriver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            int? driverId = ddlDriver.SelectedValue.ToIntorNull();
            if (objMaster != null && objMaster.PrimaryKeyValue == null && driverId != null)
            {
                btnSaveNew.Text = "Save and Dispatch";

            }
            else
            {
                btnSaveNew.Text = "Save Booking";
            }




        }

        private void btnMultiBooking_Click(object sender, EventArgs e)
        {
            ShowMultiBooking();

        }

        private void ShowMultiBooking()
        {

            string customerName = ddlCustomerName.Text.ToStr().Trim();
            string MobileNo = txtCustomerMobileNo.Text.Trim();
            string telephoneNo = txtCustomerPhoneNo.Text.Trim();

            string error = string.Empty;
            if (string.IsNullOrEmpty(customerName))
            {

                error += "Required : Customer Name " + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(MobileNo) && string.IsNullOrEmpty(telephoneNo))
            {
                error += "Required : Phone No" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;

            }


            Booking obj = new Booking();

            obj.BookingTypeId = ddlBookingType.SelectedValue.ToIntorNull();


            int FromlocTypeId = ddlFromLocType.SelectedValue.ToInt();
            int TolocTypeId = ddlToLocType.SelectedValue.ToInt();


            obj.FromLocTypeId = FromlocTypeId.ToIntorNull();
            obj.ToLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            obj.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
            obj.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();
            obj.DriverId = ddlDriver.SelectedValue.ToIntorNull();
            obj.ReturnDriverId = ddlReturnDriver.SelectedValue.ToIntorNull();
            obj.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
            obj.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();
            obj.CompanyId = ddlCompany.SelectedValue.ToIntorNull();

         //   obj.IsQuotation = chkQuotation.Checked;

            obj.IsCompanyWise = chkIsCompanyRates.Checked;
            //  obj.JourneyTypeId = opt_JOneWay.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? 1 : 2;


            if (opt_JOneWay.ToggleState == ToggleState.On)
            {
                obj.JourneyTypeId = Enums.JOURNEY_TYPES.ONEWAY;
            }
            else if (opt_JReturnWay.ToggleState == ToggleState.On)
            {
                obj.JourneyTypeId = Enums.JOURNEY_TYPES.RETURN;
            }
            else if (opt_WaitandReturn.ToggleState == ToggleState.On)
            {
                obj.JourneyTypeId = Enums.JOURNEY_TYPES.WAITANDRETURN;

            }


            obj.PickupDateTime = dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;

            if (dtpReturnPickupDate.Value != null && dtpReturnPickupTime.Value != null)
            {
                obj.ReturnPickupDateTime = dtpReturnPickupDate.Value.ToDateorNull() + dtpReturnPickupTime.Value.Value.TimeOfDay;
            }
            else
                obj.ReturnPickupDateTime = null;


            obj.FareRate = numFareRate.Value.ToDecimal();
            obj.ReturnFareRate = numFareRate.Value.ToDecimal();

            obj.CustomerId = ddlCustomerName.SelectedValue.ToIntorNull();

            obj.CustomerName = customerName;

            //   obj.CustomerEmail=txtCustomerEmail.Text.Trim();
            obj.CustomerPhoneNo = telephoneNo;
            obj.CustomerMobileNo = MobileNo;
            obj.SpecialRequirements = txtSpecialRequirements.Text.Trim();


            if (FromlocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                obj.FromAddress = txtFromAddress.Text.Trim();

            else if (FromlocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                obj.FromAddress = txtFromPostCode.Text.Trim();
            else
            {
                obj.FromAddress = ddlFromLocation.Text.Trim();
            }



            obj.FromDoorNo = txtFromFlightDoorNo.Text.Trim();
            obj.FromStreet = txtFromStreetComing.Text.Trim();
            obj.FromPostCode = txtFromPostCode.Text.Trim();


            if (TolocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                obj.ToAddress = txtToAddress.Text.Trim();

            else if (TolocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                obj.ToAddress = txtToPostCode.Text.Trim();
            else
            {
                obj.ToAddress = ddlToLocation.Text.Trim();
            }


            obj.ToDoorNo = txtToFlightDoorNo.Text.Trim();
            obj.ToStreet = txtToStreetComing.Text.Trim();
            obj.ToPostCode = txtToPostCode.Text.Trim();

            obj.DistanceString = lblMap.Text;
       //     obj.AutoDespatch = chkAutoDespatch.Checked;



            obj.IsCommissionWise = chkIsCommissionWise.Checked;
            obj.DriverCommission = numDriverCommission.Value.ToDecimal();
            obj.DriverCommissionType = ddlCommissionType.SelectedValue.ToStr().Trim();


            string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
            IList<Booking_ViaLocation> savedList = obj.Booking_ViaLocations;
            List<Booking_ViaLocation> listofDetail = (from r in grdVia.Rows
                                                      select new Booking_ViaLocation
                                                      {
                                                          Id = r.Cells[COLS.ID].Value.ToLong(),
                                                          BookingId = r.Cells[COLS.MASTERID].Value.ToLong(),
                                                          ViaLocTypeId = r.Cells[COLS.FROMVIALOCTYPEID].Value.ToIntorNull(),
                                                          ViaLocTypeLabel = r.Cells[COLS.FROMTYPELABEL].Value.ToStr(),
                                                          ViaLocTypeValue = r.Cells[COLS.FROMTYPEVALUE].Value.ToStr(),

                                                          ViaLocId = r.Cells[COLS.VIALOCATIONID].Value.ToIntorNull(),
                                                          ViaLocLabel = r.Cells[COLS.VIALOCATIONLABEL].Value.ToStr(),
                                                          ViaLocValue = r.Cells[COLS.VIALOCATIONVALUE].Value.ToStr()

                                                      }).ToList();


            Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

            frmMultiBooking frm = new frmMultiBooking(obj);
            frm.ShowDialog();


            if (frm.Saved)
            {

                General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");
                General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
                this.Close();
            }

            //         frm.Dispose();

        }



        private void chkAutoDespatch_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            //numBeforeMinutes.Enabled = args.NewValue == ToggleState.On;


            if (args.NewValue == ToggleState.On)
            {
                // ddlDriver.SelectedValue = null;
                // ddlReturnDriver.SelectedValue = null;
             //   numBeforeMinutes.Enabled = true;
                //  ddlDriver.Enabled = false;
                //   ddlReturnDriver.Enabled = false;
            }
            else
            {
             //   numBeforeMinutes.Enabled = false;
                //  ddlDriver.Enabled = true;
                //  ddlReturnDriver.Enabled = true;

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {

            ClearViaDetails();

        }



        private void txtFromPostCode_Validated(object sender, EventArgs e)
        {

            try
            {

                if (MapType == Enums.MAP_TYPE.NONE) return;




                string text = txtFromPostCode.Text.ToStr().Trim();
                string street = string.Empty;
                if (!string.IsNullOrEmpty(text))
                {
                    //if (UseGoogleMap)
                    //{
                    FillStreetFromPostCode(txtFromStreetComing, text, ref street);


                    if (!string.IsNullOrEmpty(street)) return;

                    string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + text + " UK&sensor=false";

                    wc = new WebClient();
                    string xml = wc.DownloadString(new Uri(url));

                    var xmlElm = XElement.Parse(xml);

                    street = (from elm in xmlElm.Descendants()
                              where elm.Name == "formatted_address"
                              select elm.Value).FirstOrDefault().ToStr();


                    street = street.Replace(text, "");
                    txtFromStreetComing.Text = street;


                }

            }
            catch
            {


            }
        }



        private void FillStreetFromPostCode(RadTextBox streetTextBox, string text, ref string street)
        {
            street = AppVars.listOfAddress.FirstOrDefault(c => c.PostalCode.ToLower() == text.ToLower()).DefaultIfEmpty().AddressLine1;

            if (!string.IsNullOrEmpty(street) && street.Contains(text))
            {
                street = street.Remove(street.IndexOf(text));

            }
            streetTextBox.Text = street;


        }


        string google_Street = string.Empty;




        private void txtToPostCode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (MapType == Enums.MAP_TYPE.NONE) return;


                string text = txtToPostCode.Text.ToStr().Trim();
                string street = string.Empty;
                if (!string.IsNullOrEmpty(text))
                {

                    //if (UseGoogleMap)
                    //{
                    FillStreetFromPostCode(txtToStreetComing, text, ref street);

                    if (string.IsNullOrEmpty(street)) return;

                    string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + text + " UK&sensor=false";

                    wc = new WebClient();
                    string xml = wc.DownloadString(new Uri(url));

                    var xmlElm = XElement.Parse(xml);

                    street = (from elm in xmlElm.Descendants()
                              where elm.Name == "formatted_address"
                              select elm.Value).FirstOrDefault().ToStr();


                    street = street.Replace(text, "");
                    txtToStreetComing.Text = street;



                }
            }
            catch
            {


            }
        }








        private void radToggleButton1_ToggleStateChanged_1(object sender, StateChangedEventArgs args)
        {
            SetFromBase();
        }

        private void ShowHospitals(ToggleState toggle)
        {

            HideAirports();
            HideLocals();
            HideStations();

         //   pnlHospital.Visible = toggle == ToggleState.On;

            if (toggle == ToggleState.On)
            {
                if (LastFocus == 1)
                {
                //    ddlHospitalType.Text = "From";
                }

                if (LastFocus == 2)
                {

                  //  ddlHospitalType.Text = "To";
                }


                //if (ddlHospitals.DataSource == null)
                //{
                //    FillKeyLocations(ddlHospitals, General.GetHospitalsList());

                //}

                //ddlHospitals.Focus();
            }

         //   ddlHospitals.CloseDropDown();
        }



        private void ShowAirports(ToggleState toggle)
        {
            //HideLocals();
            //HideStations();
            //HideHospitals();

            //pnlAirport.Visible = toggle == ToggleState.On;

            //if (toggle == ToggleState.On)
            //{
            //    if (LastFocus == 1)
            //    {
            //        ddlAirportType.Text = "From";
            //    }

            //    if (LastFocus == 2)
            //    {
            //        ddlAirportType.Text = "To";
            //    }

            //    if (ddlAirPorts.DataSource == null)
            //    {

            //        //   listofAirports= General.GetAirportsLocations();
            //        FillKeyLocations(ddlAirPorts, General.GetAirportsList());

            //    }


            //    ddlAirPorts.Focus();
            //}


            //ddlAirPorts.CloseDropDown();

        }

        public static IList GetAirportsList()
        {

            var list = (from a in General.GetQueryable<Gen_Location>(null)
                        where a.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT
                        orderby a.LocationName
                        select new
                        {
                            Id = a.Id,
                            Location = a.LocationName

                        }).ToList();

            return list;



        }


        private void HideLocals()
        {

          //  pnlLocals.Visible = false;

        }

        private void HideAirports()
        {
            //pnlAirport.Visible = false;
            //ddlAirPorts.SelectedValue = null;
            //ddlAirPorts.CloseDropDown();

            btnAirport.ToggleStateChanged -= new StateChangedEventHandler(btnAirport_ToggleStateChanged);
            btnAirport.ToggleState = ToggleState.Off;
            btnAirport.ToggleStateChanged += new StateChangedEventHandler(btnAirport_ToggleStateChanged);

        }

        private void HideStations()
        {

            //pnlStations.Visible = false;
            //ddlStations.SelectedValue = null;
            //ddlStations.CloseDropDown();

            btnStations.ToggleStateChanged -= new StateChangedEventHandler(btnStations_ToggleStateChanged);
            btnStations.ToggleState = ToggleState.Off;
            btnStations.ToggleStateChanged += new StateChangedEventHandler(btnStations_ToggleStateChanged);

        }

        private void HideHospitals()
        {
            //pnlHospital.Visible = false;
            //ddlHospitals.SelectedValue = null;
            //ddlHospitals.CloseDropDown();
            btnHospital.ToggleStateChanged -= new StateChangedEventHandler(btnHospital_ToggleStateChanged);
            btnHospital.ToggleState = ToggleState.Off;
            btnHospital.ToggleStateChanged += new StateChangedEventHandler(btnHospital_ToggleStateChanged);
        }

        private void ShowStations(ToggleState toggle)
        {
            //   ShowLocals(ToggleState.Off);
            //   ShowAirports(ToggleState.Off);
            HideAirports();
            HideLocals();
            HideHospitals();

            //pnlStations.Visible = toggle == ToggleState.On;

            //if (toggle == ToggleState.On)
            //{
            //    if (LastFocus == 1)
            //    {
            //        ddlStationType.Text = "From";
            //    }

            //    if (LastFocus == 2)
            //    {
            //        ddlStationType.Text = "To";
            //    }


            //    if (ddlStations.DataSource == null)
            //    {
            //        FillKeyLocations(ddlStations, General.GetStationsList());
            //    }

            //    ddlStations.Focus();


            //}

            //ddlStations.CloseDropDown();


        }


        private void FillKeyLocations(RadDropDownList combo, IList datasource)
        {

            combo.DataSource = datasource;
            combo.DisplayMember = "Location";
            combo.ValueMember = "Id";

            combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            combo.DropDownStyle = RadDropDownStyle.DropDown;
            combo.SelectedIndex = -1;
            combo.NullText = "Select";
        }




        private void frmBooking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    SaveAndClose();

                }

            }


            if (e.KeyCode == Keys.F1)
            {
                SetFromBase();

            }
            else if (e.KeyCode == Keys.F2)
            {
                btnAirport.ToggleState = btnHospital.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;

                ShowAirports(btnAirport.ToggleState);

            }

            else if (e.KeyCode == Keys.F3)
            {
                btnStations.ToggleState = btnHospital.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;
                ShowStations(btnStations.ToggleState);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btn_Locals.ToggleState = btnHospital.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;
                ShowLocals(btn_Locals.ToggleState);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnHospital.ToggleState = btnHospital.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;

                ShowHospitals(btnHospital.ToggleState);

            }
           
            else if (e.KeyCode == Keys.F7)
            {
                SearchBooking();
            }
            else if (e.KeyCode == Keys.F8)
            {
                if (btnMultiBooking.Enabled)
                {
                    ShowMultiBooking();
                }
            }
            else if (e.KeyCode == Keys.F9)
            {
                SendEmail();

            }
            else if (e.KeyCode == Keys.F12)
            {
                SaveAndClose();
            }

        }

       

        private void SetFromBase()
        {

            if (ddlFromLocType.SelectedValue.ToInt() != Enums.LOCATION_TYPES.BASE)
            {
                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.BASE;
            }
            else
            {
                txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            }

        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            //if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to cancel this Booking ?", "Cancel Booking", MessageBoxButtons.YesNo))
            //{

            //    if (objMaster.PrimaryKeyValue != null)
            //    {
            //        //(new TaxiDataContext()).stp_CancelJob(objMaster.PrimaryKeyValue.ToLong());

            //        General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");
            //        General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");

            //        this.Close();
            //    }

            //}

            try
            {

              

                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to Cancel Booking ?", "Cancel Booking", MessageBoxButtons.YesNo))
                {

                    if (objMaster.PrimaryKeyValue != null)
                    {
                 
                        frmCancelReason frm = new frmCancelReason(objMaster.PrimaryKeyValue.ToLong(), objMaster.Current.CancelReason.ToStr());
                        frm.ShowDialog();
                        frm.Dispose();
                        //(new TaxiDataContext()).stp_CancelJob(objMaster.PrimaryKeyValue.ToLong());

                        General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");
                        General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");

                        this.Close();
                    }

                }

            }

            catch 
            {


            }

        }



        private void ddlFromLocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillFromLocations();
        }

        private void ddlToLocType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillToLocations();
        }

        private void ddlCustomerName_Validated(object sender, EventArgs e)
        {
            SetCustomerNameInProperCase(ddlCustomerName.Text.ToStr().Trim());
        }

        private void SetCustomerNameInProperCase(string customerName)
        {
            ddlCustomerName.Text = customerName.ToProperCase();


        }



        private void btn_Locals_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ShowLocals(args.ToggleState);
        }


        private void ShowLocals(ToggleState toggle)
        {
            //HideHospitals();
            //HideAirports();
            //HideStations();

            //if (LastFocus == 1)
            //{
            //    ddlLocalType.Text = "From";
            //}

            //if (LastFocus == 2)
            //{
            //    ddlLocalType.Text = "To";
            //}

            //pnlLocals.Visible = toggle == ToggleState.On;
            //txtLocalAddress.Focus();

        }

        private void btnPickAddress_Click(object sender, EventArgs e)
        {
            PickLocal();
            //pnlLocals.Visible = false;
            //btn_Locals.ToggleState = ToggleState.Off;
            //txtLocalAddress.Text = string.Empty;
        }

        private void PickLocal()
        {
            //string type = ddlLocalType.Text.ToLower().Trim();
            //if (type == "from")
            //{
            //    ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
            //    txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            //    txtFromAddress.Text = txtLocalAddress.Text;
            //    txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            //    txtToAddress.Focus();
            //}
            //else if (type == "to")
            //{
            //    ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
            //    txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            //    txtToAddress.Text = txtLocalAddress.Text;
            //    txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            //    ddlCustomerName.Focus();

            //}

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

        private void btnCustomerLister_Click(object sender, EventArgs e)
        {
            SearchBooking();
        }

        private void txtFromAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnFromDoor();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  ddlAirPorts.Focus();
        }

        private void btnAirport_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ShowAirports(args.ToggleState);
        }

        private void btnStations_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ShowStations(args.ToggleState);
        }

        private void btnHospital_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            ShowHospitals(args.ToggleState);
        }

        int LastFocus = 0;

        private void txtFromAddress_Enter(object sender, EventArgs e)
        {
            LastFocus = 1;

        }

        private void txtToAddress_Enter(object sender, EventArgs e)
        {
            LastFocus = 2;

        }

        private void ddlAirPorts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PickAirport();

            }
        }



        private void ddlStations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PickStation();

            }
        }

        private void ddlHospitals_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PickHospital();

            }
        }

        private void txtLocalAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PickLocal();

            }
        }



        private void txtToAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               // CalculateTotalFares();
                FocusOnToDoor();
            }
        }


        private void FocusOnToDoor()
        {
            txtToFlightDoorNo.Focus();

        }

        private void btnMultiVehicle_Click(object sender, EventArgs e)
        {
            ShowMultiVehicleBooking();
        }


        private void ShowMultiVehicleBooking()
        {

            string customerName = ddlCustomerName.Text.ToStr().Trim();
            string MobileNo = txtCustomerMobileNo.Text.Trim();
            string telephoneNo = txtCustomerPhoneNo.Text.Trim();

            string error = string.Empty;
            if (string.IsNullOrEmpty(customerName))
            {

                error += "Required : Customer Name " + Environment.NewLine;
            }
            if (string.IsNullOrEmpty(MobileNo) && string.IsNullOrEmpty(telephoneNo))
            {
                error += "Required : Phone No" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;

            }


            Booking obj = new Booking();



            obj.BookingTypeId = ddlBookingType.SelectedValue.ToIntorNull();

            int FromlocTypeId = ddlFromLocType.SelectedValue.ToInt();
            int TolocTypeId = ddlToLocType.SelectedValue.ToInt();

            obj.BookingDate = DateTime.Now;

            obj.FromLocTypeId = FromlocTypeId.ToIntorNull();
            obj.ToLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            obj.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
            obj.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();
            obj.DriverId = ddlDriver.SelectedValue.ToIntorNull();
            obj.ReturnDriverId = ddlReturnDriver.SelectedValue.ToIntorNull();
            obj.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
            obj.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();
            obj.CompanyId = ddlCompany.SelectedValue.ToIntorNull();

           // obj.IsQuotation = chkQuotation.Checked;

            obj.IsCompanyWise = chkIsCompanyRates.Checked;

            if (opt_JOneWay.ToggleState == ToggleState.On)
            {
                obj.JourneyTypeId = Enums.JOURNEY_TYPES.ONEWAY;
            }
            else if (opt_JReturnWay.ToggleState == ToggleState.On)
            {
                obj.JourneyTypeId = Enums.JOURNEY_TYPES.RETURN;
            }
            else if (opt_WaitandReturn.ToggleState == ToggleState.On)
            {
                obj.JourneyTypeId = Enums.JOURNEY_TYPES.WAITANDRETURN;

            }

            // obj.JourneyTypeId = opt_JOneWay.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? 1 : 2;

            obj.PickupDateTime = dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;

            if (dtpReturnPickupDate.Value != null && dtpReturnPickupTime.Value != null)
            {
                obj.ReturnPickupDateTime = dtpReturnPickupDate.Value.ToDateorNull() + dtpReturnPickupTime.Value.Value.TimeOfDay;
            }
            else
                obj.ReturnPickupDateTime = null;


            obj.FareRate = numFareRate.Value.ToDecimal();
            obj.CompanyPrice = numCompanyFares.Value.ToDecimal(); 

            obj.CustomerId = ddlCustomerName.SelectedValue.ToIntorNull();

            obj.CustomerName = customerName;
            obj.CustomerPhoneNo = telephoneNo;
            obj.CustomerMobileNo = MobileNo;
            obj.SpecialRequirements = txtSpecialRequirements.Text.Trim();


            if (FromlocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromlocTypeId == Enums.LOCATION_TYPES.BASE)
                obj.FromAddress = txtFromAddress.Text.Trim();

            else if (FromlocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                obj.FromAddress = txtFromPostCode.Text.Trim();
            else
            {
                obj.FromAddress = ddlFromLocation.Text.Trim();
            }



            obj.FromDoorNo = txtFromFlightDoorNo.Text.Trim();
            obj.FromStreet = txtFromStreetComing.Text.Trim();
            obj.FromPostCode = txtFromPostCode.Text.Trim();


            if (TolocTypeId == Enums.LOCATION_TYPES.ADDRESS || TolocTypeId == Enums.LOCATION_TYPES.BASE)
                obj.ToAddress = txtToAddress.Text.Trim();

            else if (TolocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                obj.ToAddress = txtToPostCode.Text.Trim();
            else
            {
                obj.ToAddress = ddlToLocation.Text.Trim();
            }


            obj.ToDoorNo = txtToFlightDoorNo.Text.Trim();
            obj.ToStreet = txtToStreetComing.Text.Trim();
            obj.ToPostCode = txtToPostCode.Text.Trim();


            obj.IsCommissionWise = chkIsCommissionWise.Checked;
            obj.DriverCommission = numDriverCommission.Value.ToDecimal();
            obj.DriverCommissionType = ddlCommissionType.SelectedValue.ToStr().Trim();


            if (string.IsNullOrEmpty(obj.FromAddress))
            {
                error += "Required : Pickup Point" + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(obj.ToAddress))
            {
                error += "Required : Destination" + Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;

            }


          //  obj.AutoDespatch = chkAutoDespatch.Checked;

            obj.DistanceString = lblMap.Text;

            string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
            IList<Booking_ViaLocation> savedList = obj.Booking_ViaLocations;
            List<Booking_ViaLocation> listofDetail = (from r in grdVia.Rows
                                                      select new Booking_ViaLocation
                                                      {
                                                          Id = r.Cells[COLS.ID].Value.ToLong(),
                                                          BookingId = r.Cells[COLS.MASTERID].Value.ToLong(),
                                                          ViaLocTypeId = r.Cells[COLS.FROMVIALOCTYPEID].Value.ToIntorNull(),
                                                          ViaLocTypeLabel = r.Cells[COLS.FROMTYPELABEL].Value.ToStr(),
                                                          ViaLocTypeValue = r.Cells[COLS.FROMTYPEVALUE].Value.ToStr(),

                                                          ViaLocId = r.Cells[COLS.VIALOCATIONID].Value.ToIntorNull(),
                                                          ViaLocLabel = r.Cells[COLS.VIALOCATIONLABEL].Value.ToStr(),
                                                          ViaLocValue = r.Cells[COLS.VIALOCATIONVALUE].Value.ToStr()

                                                      }).ToList();


            Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

            frmMultiVehicleBooking frm = new frmMultiVehicleBooking(obj);
            frm.ShowDialog();


            if (frm.Saved)
            {

                General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");
                General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
                this.Close();
            }

            frm.Dispose();


        }

        private void chkReverse_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (wc != null)
            {
                wc.CancelAsync();

            }

            SetReverseAddress(ddlFromLocType.SelectedValue.ToIntorNull(), ddlToLocType.SelectedValue.ToIntorNull(),
                             ddlFromLocation.SelectedValue.ToIntorNull(), ddlToLocation.SelectedValue.ToIntorNull(), txtFromFlightDoorNo.Text.ToStr(),
                               txtFromAddress.Text.ToStr(), txtFromPostCode.Text.ToStr(), txtFromStreetComing.Text.ToStr(), txtToFlightDoorNo.Text.ToStr(), txtToAddress.Text.ToStr(),
                               txtToStreetComing.Text.ToStr(), txtToPostCode.Text.ToStr());

        }


        private void SetReverseAddress(int? fromLocTypeId, int? toLocTypeId, int? fromLocId, int? toLocId, string fromDoorNo, string fromAddress, string fromPostCode,
                                        string fromStreet, string toDoorNo, string toAddress, string toStreet, string toPostCode)
        {

            fromLocTypeId = fromLocTypeId ^ toLocTypeId;
            toLocTypeId = toLocTypeId ^ fromLocTypeId;
            fromLocTypeId = fromLocTypeId ^ toLocTypeId;

            if (fromLocId != null && toLocId != null)
            {

                fromLocId = fromLocId ^ toLocId;
                toLocId = toLocId ^ fromLocId;
                fromLocId = fromLocId ^ toLocId;
            }

            if (fromLocId == null)
                fromLocId = toLocId;

            if (toLocId == null)
                toLocId = fromLocId;

            string temp = fromAddress;
            fromAddress = toAddress;
            toAddress = temp;


            temp = fromDoorNo;
            fromDoorNo = toDoorNo;
            toDoorNo = temp;


            temp = fromStreet;
            fromStreet = toStreet;
            toStreet = temp;


            temp = fromPostCode;
            fromPostCode = toPostCode;
            toPostCode = temp;


            ddlFromLocType.SelectedValue = fromLocTypeId;
            ddlToLocType.SelectedValue = toLocTypeId;

            if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
            {
                txtFromFlightDoorNo.Text = fromDoorNo;

                this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = fromAddress;
                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }
            else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
            {
                ddlFromLocation.SelectedValue = fromLocId;
                txtFromFlightDoorNo.Text = fromDoorNo;
                txtFromStreetComing.Text = fromStreet;
            }
            else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                //   txtFromPostCode.TextChanged -= new EventHandler(txtPostCode_TextChanged);
                txtFromPostCode.Text = fromPostCode;
                //   txtFromPostCode.TextChanged += new EventHandler(txtPostCode_TextChanged);


                txtFromFlightDoorNo.Text = fromDoorNo;
                txtFromStreetComing.Text = fromStreet;

            }
            else
            {
                ddlFromLocation.SelectedValue = fromLocId;
            }


            if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
            {
                this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = toAddress;
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtToFlightDoorNo.Text = toDoorNo;

            }
            else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                //   txtToPostCode.TextChanged -= new EventHandler(txtPostCode_TextChanged);
                txtToPostCode.Text = toPostCode;
                //   txtToPostCode.TextChanged += new EventHandler(txtPostCode_TextChanged);

                txtToFlightDoorNo.Text = toDoorNo;
                txtToStreetComing.Text = toStreet;
            }
            else
            {
                ddlToLocation.SelectedValue = toLocId;
            }


        }



        private void txtToFlightDoorNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();

                if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
                {

                    FocusOnCustomer();
                }
                else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    FocusOnToStreet();
                }

            }

        }


        private void FocusOnCustomer()
        {
            ddlCustomerName.Focus();
        }

        private void FocusOnToStreet()
        {
            txtToStreetComing.Focus();

        }

        private void FocusOnFromStreet()
        {
            txtFromStreetComing.Focus();

        }

        private void FocusOnFromDoor()
        {
            txtFromFlightDoorNo.Focus();

        }

        private void txtFromFlightDoorNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int? LocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
                int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
                if ((LocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)

                     )
                {
                    if ((toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE))
                    {
                        FocusOnToAddress();

                    }
                    else if ((toLocTypeId == Enums.LOCATION_TYPES.POSTCODE))
                    {
                        FocusOnToPostCode();

                    }
                    else
                    {

                        FocusOnToLocation();
                    }
                }


                else if (LocTypeId == Enums.LOCATION_TYPES.POSTCODE || LocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                {
                    FocusOnFromStreet();
                }
            }

        }

        private void FocusOnToAddress()
        {
            txtToAddress.Focus();

        }

        private void ddlFromLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int? LocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
                int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
                if (LocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                {

                    FocusOnFromDoor();
                }
                else
                {
                    if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
                    {
                        FocusOnToAddress();
                    }
                    else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        FocusOnToPostCode();
                    }
                    else
                    {
                        FocusOnToLocation();
                    }


                }
            }
        }

        private void FocusOnToLocation()
        {
            ddlToLocation.Focus();

        }

        private void FocusOnToPostCode()
        {
            txtToPostCode.Focus();

        }

        private void txtFromStreetComing_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                int? LocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
                int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
                if (LocTypeId == Enums.LOCATION_TYPES.AIRPORT || LocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
                    {
                        FocusOnToAddress();
                    }
                    else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        FocusOnToPostCode();
                    }
                    else
                    {
                        FocusOnToLocation();
                    }




                }

            }
        }

        private void txtToPostCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnToDoor();

            }
        }

        private void txtToStreetComing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnCustomer();

            }
        }

        private void txtFromPostCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnFromDoor();

            }
        }

        private void ddlCustomerName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                string telNo = txtCustomerPhoneNo.Text.ToStr().Trim();
                string mobNo = txtCustomerMobileNo.Text.ToStr().Trim();

                if (!string.IsNullOrEmpty(telNo) && !string.IsNullOrEmpty(mobNo))
                {

                    FocusOnPickupDate();
                }
                else if (string.IsNullOrEmpty(telNo) && string.IsNullOrEmpty(mobNo))
                {
                    FocusOnTelNo();
                }
                else if (!string.IsNullOrEmpty(telNo) && string.IsNullOrEmpty(mobNo))
                {
                    FocusOnPickupDate();
                }

                else if (string.IsNullOrEmpty(telNo) && !string.IsNullOrEmpty(mobNo))
                {
                    FocusOnPickupDate();
                }



            }

        }

        private void FocusOnTelNo()
        {
            txtCustomerPhoneNo.Focus();

        }

        private void FocusOnMobNo()
        {
            txtCustomerMobileNo.Focus();

        }

        private void FocusOnPickupDate()
        {
            dtpPickupDate.Focus();

        }

        private void FocusOnPickupTime()
        {
            dtpPickupTime.Focus();

        }


        private void FocusOnFare()
        {
            numFareRate.Focus();

        }

        private void FocusOnDriver()
        {
            ddlDriver.Focus();

        }

        private void txtCustomerPhoneNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string telNo = txtCustomerPhoneNo.Text.ToStr().Trim();
                string mobNo = txtCustomerMobileNo.Text.ToStr().Trim();

                if (!string.IsNullOrEmpty(telNo) && !string.IsNullOrEmpty(mobNo))
                {

                    FocusOnPickupDate();
                }
                else if (string.IsNullOrEmpty(telNo) && string.IsNullOrEmpty(mobNo))
                {
                    FocusOnMobNo();
                }
                else if (!string.IsNullOrEmpty(telNo) && string.IsNullOrEmpty(mobNo))
                {
                    FocusOnPickupDate();
                }

                else if (string.IsNullOrEmpty(telNo) && !string.IsNullOrEmpty(mobNo))
                {
                    FocusOnPickupDate();
                }

            }
        }

        private void txtCustomerMobileNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                FocusOnPickupDate();





            }
        }

        private void dtpPickupDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnPickupTime();
            }

        }

        private void dtpPickupTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnFare();

            }
        }


        private void FocusOnSave()
        {
            btnSaveNew.ButtonElement.Focus();
            // btnSaveNew.Focus();
            //   (btnSaveNew.RootElement.Children[0] as RadButtonElement).Focus();

        }

        private void ddlDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.P)
            {


                frmPlotDriver frm = new frmPlotDriver(true);
                frm.ShowDialog();

                if (frm.Plotted)
                {
                    ComboFunctions.FillDriverNoQueueCombo(ddlDriver);

                    ddlDriver.SelectedValue = frm.PlottedDriverId;
                }



            }
        }






        void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnDriver();
            }
        }

        private void ddlToLocation_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {


                FocusOnCustomer();


            }

        }

        private void ddlFromLocType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int locTypeId = ddlFromLocType.SelectedValue.ToInt();

                if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    FocusOnFromAddress();

                }
                else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    FocusOnFromPostCode();

                }
                else
                {
                    FocusOnFromLocation();

                }

            }

        }

        private void ddlToLocType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int locTypeId = ddlToLocType.SelectedValue.ToInt();

                if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    FocusOnToAddress();

                }
                else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    FocusOnToPostCode();

                }
                else
                {
                    FocusOnToLocation();

                }

            }

        }

        private void opt_JReturnWay_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {


            SetReturnAirportJob(args.NewValue);

        }

        private void ddlCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chkIsCompanyRates.Checked)
            {
                int? companyId = ddlCompany.SelectedValue.ToIntorNull();


                if (companyId == null)
                {
                    HideOrderNoPanel();
                    ClearDepartment();
                    ClearCostCenter();
                }
                else
                {
                    Gen_Company obj = General.GetObject<Gen_Company>(c => c.Id == companyId);
                    if (obj != null)
                    {
                        FillDepartmentsCombo(obj.Id);
                        FillCostCentersCombo(obj.Id);

                        bool orderNo = obj.HasOrderNo.ToBool();
                        bool pupilNo = obj.HasPupilNo.ToBool();

                        if (orderNo || pupilNo)
                        {
                            if (orderNo == false)
                            {
                                lblOrderNo.Visible = false;
                                txtOrderNo.Visible = false;

                            }

                            if (pupilNo == false)
                            {
                                lblPupilNo.Visible = false;
                                txtPupilNo.Visible = false;

                            }

                            pnlOrderNo.Visible = true;



                        }
                        else
                        {
                            HideOrderNoPanel();
                        }

                        if (obj.HasComcabCharges.ToBool())
                        {
                            HideOrderNoPanel();
                            ShowHideCostCenter();
                            ShowComcabCharges(true);
                        }
                        else
                        {
                            ShowComcabCharges(false);
                      
                        }

                        SetCashAccount(obj.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH);
                //        pnlAccpassword.Visible = obj.PasswordEnable.ToBool();
                    }
                }
            }
            else
            {
                HideOrderNoPanel();
                ClearDepartment();
                ClearCostCenter();
                ShowComcabCharges(false);
            }

        }

        private void ShowComcabCharges(bool show)
        {
            pnlComcab.Visible = show;
        }


        private void SetCashAccount(bool setToCash)
        {
            if (setToCash)
            {
                chkIsCommissionWise.Checked = true;
                ddlCommissionType.SelectedValue = "Amount";
                ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;
            }
            else
            {
                chkIsCommissionWise.Checked = false;
                SetAccountPaymentType();
            }

        }

        private void ClearDepartment()
        {
            ddlDepartment.DataSource = null;
        }


        private void FillDepartmentsCombo(int companyId)
        {
            ComboFunctions.FillCompanyDepartmentCombo(ddlDepartment, c => c.CompanyId == companyId);
        }

        private void ClearCostCenter()
        {
            ddlCostCenter.DataSource = null;
        }


        private void FillCostCentersCombo(int companyId)
        {
            ComboFunctions.FillCompanyCostCentersCombo(ddlCostCenter, c => c.CompanyId == companyId);
            ShowHideCostCenter();

        }


        private void ShowHideCostCenter()
        {
            bool show = ddlCostCenter.Items.Count > 0;

            lblCostCenter.Visible = show;
            ddlCostCenter.Visible = show;


        }




        private void HideOrderNoPanel()
        {
            pnlOrderNo.Visible = false;
            txtOrderNo.Text = string.Empty;
            txtPupilNo.Text = string.Empty;


        }

        private void dtpPickupTime_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }


        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintJob_Click(object sender, EventArgs e)
        {
            Print();
        }

        public override void Print()
        {
            long id = objMaster.Current.Id;


            var list = General.GetQueryable<Vu_BookingDetail>(c => c.Id == id).ToList();




            UM_Form_Template objReport = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "rptfrmJobDetails" && c.IsDefault == true);
            rptfrmJobDetails frm = null;
            rptfrmJobDetails2 frm2 = null;
            rptfrmJobDetails3 frm3 = null;
            if (objReport != null)
            {
                switch (objReport.TemplateValue)
                {
                    case "rptfrmJobDetails":
                        frm = new rptfrmJobDetails();
                        frm.DataSource = list;
                        frm.GenerateReport();

                        break;


                    case "rptfrmJobDetails2":
                        frm2 = new rptfrmJobDetails2();
                        frm2.DataSource = list;
                        frm2.GenerateReport();

                        break;
                    case "rptfrmJobDetails3":
                        frm3 = new rptfrmJobDetails3();
                        frm3.DataSource = list;
                        frm3.GenerateReport();

                        break;

                }
            }



            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName(objReport.TemplateValue + "1");

            if (doc != null)
            {
                doc.Close();
            }


            if (frm != null)
            {
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.Size = new Size(800, 800);
                frm.ControlBox = true;
                frm.MaximizeBox = true;
                frm.MinimizeBox = true;
                frm.ShowDialog();

                frm.Dispose();
            }
            else if (frm2 != null)
            {
                frm2.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm2.Size = new Size(850, 800);
                frm2.ControlBox = true;
                frm2.MaximizeBox = true;
                frm2.MinimizeBox = true;
                frm2.ShowDialog();

                frm2.Dispose();


            }
            else if (frm3 != null)
            {
                frm3.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm3.Size = new Size(850, 800);
                frm3.ControlBox = true;
                frm3.MaximizeBox = true;
                frm3.MinimizeBox = true;
                frm3.ShowDialog();

                frm3.Dispose();


            }
            //   UI.MainMenuForm.MainMenuFrm.ShowForm(frm);

        }

        private void opt_JOneWay_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetJourneyWise(args.ToggleState);
        }

        private void chkIsCommissionWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            SetCommissionWise(args.ToggleState);
        }


        private void SetCommissionWise(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                numDriverCommission.Enabled = true;
                ddlCommissionType.Enabled = true;
            }
            else
            {
                numDriverCommission.Enabled = false;
                ddlCommissionType.Enabled = false;
            }

            SetDefaultCommission();
        }




        private void btnDespatchView_Click(object sender, EventArgs e)
        {
          
        }

        private void ddlCommissionType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {


            if (ddlCommissionType.SelectedValue.ToStr().Trim() == "Percent")
            {
                numDriverCommission.DecimalPlaces = 0;
                numDriverCommission.Maximum = 100;
                SetDefaultCommission();
            }
            else
            {
                numDriverCommission.DecimalPlaces = 2;
                numDriverCommission.Maximum = 99999999;
                SetDefaultCommissionAmount();
            }

        }


        private void SetDefaultCommissionAmount()
        {
            numDriverCommission.Value = (numFareRate.Value * AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal()) / 100;


        }

        private void chkQuotation_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetQuotation(args.ToggleState);
        }

        private void SetQuotation(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                ddlDriver.Enabled = false;
                ddlReturnDriver.Enabled = false;


                btnSaveNew.Text = "Save Quotation";

                if (objMaster.PrimaryKeyValue != null)
                {
                    btnCancelBooking.SendToBack();
                    btnConfirmBooking.BringToFront();
                }
            }
            else
            {
                btnSaveNew.Text = "Save Booking";
                ddlDriver.Enabled = true;
                ddlReturnDriver.Enabled = true;

            }

        }

        private void btnConfirmBooking_Click(object sender, EventArgs e)
        {
          //  chkQuotation.Checked = false;
            SaveAndClose();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {

            SendEmail();

        }


        private void SendEmail()
        {
            try
            {

                Save();
                if (objMaster.PrimaryKeyValue != null)
                {


                    frmEmailBooking frm = new frmEmailBooking(objMaster.Current);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnProofOfDelivery_Click(object sender, EventArgs e)
        {
            //if (objMaster.PrimaryKeyValue != null && objMaster.Current.BookingStatusId==Enums.BOOKINGSTATUS.DISPATCHED)
            //{
            //    frmBookingPOD frm = new frmBookingPOD(objMaster.PrimaryKeyValue.ToLong());
            //    frm.ShowDialog();
            //}
        }

        private void ddlPaymentType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD)
            {
                if (numExtraChrgs.Value == 0)
                {
                    numExtraChrgs.Value = AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal();

                }

                btnPayment.Enabled = true;
            }
            else
            {
                btnPayment.Enabled = false;
            }
        }
        bool IsSave = false;
        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                List<Gen_SysPolicy_PaymentDetail> objMerchantInfo = General.GetQueryable<Gen_SysPolicy_PaymentDetail>(null).ToList();
                if (objMerchantInfo.Count == 0)
                {

                    ENUtils.ShowMessage("Merchant Details is not defined in Settings.");
                    return;

                }

                if (objMaster.Current != null && objMaster.Current.BookingPayment != null)
                {
                    objBookingPayment = objMaster.Current.BookingPayment;
                }
                else
                {

                    objBookingPayment = new Booking_Payment();

                }


                decimal fare = numFareRate.Value.ToDecimal();
                decimal extraCharges = numExtraChrgs.Value.ToDecimal();
                decimal total = fare.ToDecimal() + extraCharges.ToDecimal();
                this.SaveAndClose();
                string BookingNO = objMaster.Current.BookingNo.ToStr();
                int? BookingId = objMaster.Current.Id.ToInt();

                if (IsSave == true)
                {
                    //frmBookingPayment2 frm = new frmBookingPayment2(objBookingPayment, objMerchantInfo, total, BookingNO, BookingId);
                    //frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                    //frm.StartPosition = FormStartPosition.CenterScreen;
                    //frm.ShowDialog();
                    //frm.Dispose();
                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void ddlBookingType_SelectedValueChanged(object sender, EventArgs e)
        {
            //int bookingtypeId = ddlBookingType.SelectedValue.ToInt();

            //if (bookingtypeId == Enums.BOOKING_TYPES.REMOVAL)
            //{
            //    pnlRemoval.Visible = true;
            //}
            //else
            //{
            //    pnlRemoval.Visible = false;

            //}

        }


        private void btnAddViewRemovalDesc_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    frmAddRemDescription frmRem = new frmAddRemDescription();
            //    frmRem.Description = txtRemovalDescription.Text.Trim();
            //    frmRem.StartPosition = FormStartPosition.CenterScreen;
            //    frmRem.ShowDialog();

            //    if (frmRem.Saved)
            //    {
            //        txtRemovalDescription.Text = frmRem.Description.ToStr();

            //    }

            //    frmRem.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    ENUtils.ShowMessage(ex.Message);

            //}
        }

        private void numFareRate_Validated(object sender, EventArgs e)
        {
            CalculateTotalCharges();
        }

        private void CalculateTotalCharges()
        {
            numTotalChrgs.Value = numFareRate.Value + numParkingChrgs.Value + numWaitingChrgs.Value + numExtraChrgs.Value + numCongChrgs.Value + numMeetCharges.Value;


        }

        private void opt_WaitandReturn_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetJourneyWise(ToggleState.On);
        }

        private void numComcab_Cash_Validated(object sender, EventArgs e)
        {

            if (pnlComcab.Visible == true)
            {
                decimal fare = numFareRate.Value.ToDecimal();

                if (fare > 0 && (fare - numComcab_Cash.Value) > 00)
                {
                    numComcab_Account.Value = fare - numComcab_Cash.Value;

                }

            }

        }

        private void btnPickCustomerAddress_Click(object sender, EventArgs e)
        {
            PickCustomerAddress(txtCustomerPhoneNo.Text.Trim(), txtCustomerMobileNo.Text.Trim());

        }


        private void PickCustomerAddress(string phoneNo, string MobNo)
        {
            try
            {

                if (string.IsNullOrEmpty(phoneNo) && string.IsNullOrEmpty(MobNo))
                    return;



                Customer cust = General.GetObject<Customer>(c => ((!string.IsNullOrEmpty(phoneNo) && c.TelephoneNo == phoneNo)
                                                              || (!string.IsNullOrEmpty(MobNo) && c.MobileNo == MobNo)));


                if (cust != null)
                {
                    ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;

                    txtFromFlightDoorNo.Text = cust.DoorNo.ToStr();

                    txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    txtFromAddress.Text = cust.Address1.ToStr();
                    txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                }

            }
            catch
            {


            }

        }

        private void ddlFromLocation_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {


                //decimal ExtraCommission = 0.00m;
                //int locId = ddlFromLocation.SelectedValue.ToInt();

                //if (locId != 0)
                //{
                //    ExtraCommission = General.GetObject<Gen_Location>(c => c.Id == locId).DefaultIfEmpty().ExtraCommission.ToDecimal();
                //}

                //numExtraChrgs.Value = ExtraCommission;

                //CalculateTotalCharges();

               // lblMap.Text = string.Empty;
               // string frmaddress = ddlFromLocation.Visible == false ? txtFromAddress.Text == null ? "null" : txtFromAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlFromLocation.SelectedItem)).Text.ToStr();
               // string Toaddress = ddlToLocation.Visible == false ? txtToAddress.Text == null ? "null" : txtToAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlToLocation.SelectedItem)).Text.ToStr();

               // if (txtFromPostCode.Visible == true)
               // {
               //     frmaddress = txtFromPostCode.Text;
               // }
               // else if (txtToPostCode.Visible == true)
               // {
               //     Toaddress = txtToPostCode.Text;
               // }

               // string time = string.Empty;
               //// CalculateFares();

               // string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + frmaddress + "&destination=" + Toaddress + "&sensor=false";


               // XmlTextReader reader = new XmlTextReader(url2);
               // System.Data.DataSet ds = new System.Data.DataSet();
               // ds.ReadXml(reader);
               // string Status = ds.Tables[0].Rows[0]["status"].ToString();
               // if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
               // //if (Status == "ZERO_RESULTS")
               // {
               //     url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
               //     MileageError();
               // }
               // else
               // {

               //     DataTable dt = ds.Tables["duration"];

               //     DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
               //     time = row.ItemArray[1].ToString();

               //     decimal miles = milesList.Sum();
               //     if (miles > 10000 || mileageError)
               //         MileageError();
               //     else
               //     {
               //         lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
               //     }
               // }

               // CalculateTotalCharges();
            }
            catch
            {



            }


        }

        private void dtpPickupTime_ValueChanged(object sender, EventArgs e)
        {

        }
        int i = 0;
        private void dtpPickupTime_KeyPress(object sender, KeyPressEventArgs e)
        {
             i++;
          //  dtpPickupTime.RootElement.Children
             RadMaskedEditBoxItem editItem = (RadMaskedEditBoxItem)(dtpPickupTime.DateTimePickerElement.ChildrenHierarchy.ElementAt(20) as RadMaskedEditBoxElement).Children[0];
            
            if (i == 2)
             {
                 SendKeys.Send("{right}");
                 i = 0;
             }

            if (editItem.SelectionStart > 2)
            {
                i = 0;
            }
        }

       
        private void txtToAddress_Leave(object sender, EventArgs e)
        {
           // CalculateTotalFares();
        }
       

        //private void GetAutoFares()
        //{

        //    try
        //    {
        //        milesList.Clear();
        //        lblMap.Text = string.Empty;

        //        string frmaddress = ddlFromLocation.Visible == false ? txtFromAddress.Text == null ? "null" : txtFromAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlFromLocation.SelectedItem)).Text.ToStr();
        //        string Toaddress = ddlToLocation.Visible == false ? txtToAddress.Text == null ? "null" : txtToAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlToLocation.SelectedItem)).Text.ToStr();

        //        if (txtFromPostCode.Visible == true)
        //        {
        //            frmaddress = txtFromPostCode.Text;
        //        }
        //        else if (txtToPostCode.Visible == true)
        //        {
        //            Toaddress = txtToPostCode.Text;
        //        }

        //        string time = string.Empty;
        //        CalculateFares();

        //        string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + frmaddress + "&destination=" + Toaddress + "&sensor=false";


        //        XmlTextReader reader = new XmlTextReader(url2);
        //        System.Data.DataSet ds = new System.Data.DataSet();
        //        ds.ReadXml(reader);
        //        string Status = ds.Tables[0].Rows[0]["status"].ToString();
        //        if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
        //        //if (Status == "ZERO_RESULTS")
        //        {
        //            url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
        //            MileageError();
        //        }
        //        else
        //        {

        //            DataTable dt = ds.Tables["duration"];

        //            DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
        //            time = row.ItemArray[1].ToString();

        //            decimal miles = milesList.Sum();
        //            if (miles > 10000 || mileageError)
        //                MileageError();
        //            else
        //            {
        //                lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
        //            }
        //        }

        //        CalculateTotalCharges();
        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //}

        private void txtFromAddress_Leave(object sender, EventArgs e)
        {
            //lblMap.Text = string.Empty;
            ////string frmaddress = txtFromAddress.Text == null ? "null" : txtFromAddress.Text;
            ////string Toaddress = txtToAddress.Text == "" ? "null" : txtToAddress.Text;
            //string frmaddress = ddlFromLocation.Visible == false ? txtFromAddress.Text == null ? "null" : txtFromAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlFromLocation.SelectedItem)).Text.ToStr();
            //string Toaddress = ddlToLocation.Visible == false ? txtToAddress.Text == null ? "null" : txtToAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlToLocation.SelectedItem)).Text.ToStr();

            //    if (txtFromPostCode.Visible == true)
            //    {
            //        frmaddress = txtFromPostCode.Text;
            //    }
            //    else if (txtToPostCode.Visible == true)
            //    {
            //        Toaddress = txtToPostCode.Text;
            //    }

            //CalculateFares();

            //string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + frmaddress + "&destination=" + Toaddress + "&sensor=false";


            //XmlTextReader reader = new XmlTextReader(url2);
            //System.Data.DataSet ds = new System.Data.DataSet();
            //ds.ReadXml(reader);
            //string Status = ds.Tables[0].Rows[0]["status"].ToString();
            //if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
            ////if (Status == "ZERO_RESULTS")
            //{
            //    url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
            //    MileageError();
            //}
            //else
            //{

            //    DataTable dt = ds.Tables["duration"];

            //    DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
            //    string time = row.ItemArray[1].ToString();

            //    decimal miles = milesList.Sum();
            //    if (miles > 10000 || mileageError)
            //        MileageError();
            //    else
            //    {
            //        lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
            //    }
            //}

            //CalculateTotalCharges();
        }


        private void chkAutoDespatch_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

        }




        private void ddlToLocation_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {


                //decimal ExtraCommission = 0.00m;
                //int locId = ddlFromLocation.SelectedValue.ToInt();

                //if (locId != 0)
                //{
                //    ExtraCommission = General.GetObject<Gen_Location>(c => c.Id == locId).DefaultIfEmpty().ExtraCommission.ToDecimal();
                //}

                //numExtraChrgs.Value = ExtraCommission;

                //CalculateTotalCharges();

                //lblMap.Text = string.Empty;

                //string frmaddress = ddlFromLocation.Visible == false ? txtFromAddress.Text == null ? "null" : txtFromAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlFromLocation.SelectedItem)).Text.ToStr();
                //string Toaddress = ddlToLocation.Visible == false ? txtToAddress.Text == null ? "null" : txtToAddress.Text : ddlToLocation.SelectedText.ToStr();

                //if (txtFromPostCode.Visible == true)
                //{
                //    frmaddress = txtFromPostCode.Text;
                //}
                //else if (txtToPostCode.Visible == true)
                //{
                //    Toaddress = txtToPostCode.Text;
                //}

                
                //string time = string.Empty;
                //CalculateFares();

                //string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + frmaddress + "&destination=" + Toaddress + "&sensor=false";


                //XmlTextReader reader = new XmlTextReader(url2);
                //System.Data.DataSet ds = new System.Data.DataSet();
                //ds.ReadXml(reader);
                //string Status = ds.Tables[0].Rows[0]["status"].ToString();
                //if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
                ////if (Status == "ZERO_RESULTS")
                //{
                //    url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
                //    MileageError();
                //}
                //else
                //{

                //    DataTable dt = ds.Tables["duration"];

                //    DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
                //    time = row.ItemArray[1].ToString();

                //    decimal miles = milesList.Sum();
                //    if (miles > 10000 || mileageError)
                //        MileageError();
                //    else
                //    {
                //        lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
                //    }
                //}

                //CalculateTotalCharges();
            }
            catch 
            {



            }
        }

        private void txtFromPostCode_Leave(object sender, EventArgs e)
        {
            //lblMap.Text = string.Empty;

            //string frmaddress = ddlFromLocation.Visible == false ? txtFromAddress.Text == null ? "null" : txtFromAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlFromLocation.SelectedItem)).Text.ToStr();
            //string Toaddress = ddlToLocation.Visible == false ? txtToAddress.Text == null ? "null" : txtToAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlToLocation.SelectedItem)).Text.ToStr();

            //if (txtFromPostCode.Visible == true)
            //{
            //    frmaddress = txtFromPostCode.Text;
            //}
            //if (txtToPostCode.Visible == true)
            //{
            //    Toaddress = txtToPostCode.Text;
            //}


            //string time = string.Empty;
            //CalculateFares();

            //string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + frmaddress + "&destination=" + Toaddress + "&sensor=false";


            //XmlTextReader reader = new XmlTextReader(url2);
            //System.Data.DataSet ds = new System.Data.DataSet();
            //ds.ReadXml(reader);
            //string Status = ds.Tables[0].Rows[0]["status"].ToString();
            //if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
            ////if (Status == "ZERO_RESULTS")
            //{
            //    url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
            //    MileageError();
            //}
            //else
            //{

            //    DataTable dt = ds.Tables["duration"];

            //    DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
            //    time = row.ItemArray[1].ToString();

            //    decimal miles = milesList.Sum();
            //    if (miles > 10000 || mileageError)
            //        MileageError();
            //    else
            //    {
            //        lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
            //    }
            //}

            //CalculateTotalCharges();
        }

        private void txtToPostCode_Leave(object sender, EventArgs e)
        {
            //lblMap.Text = string.Empty;

            //string frmaddress = ddlFromLocation.Visible == false ? txtFromAddress.Text == null ? "null" : txtFromAddress.Text : ((Telerik.WinControls.UI.RadComboBoxItem)(ddlFromLocation.SelectedItem)).Text.ToStr();
            //string Toaddress = ddlToLocation.Visible == false ? txtToAddress.Text == null ? "null" : txtToAddress.Text : ddlToLocation.SelectedText.ToStr();

            //if (txtFromPostCode.Visible == true)
            //{
            //    frmaddress = txtFromPostCode.Text;
            //}
            //if (txtToPostCode.Visible == true)
            //{
            //    Toaddress = txtToPostCode.Text;
            //}


            //string time = string.Empty;
            //CalculateFares();

            //string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + frmaddress + "&destination=" + Toaddress + "&sensor=false";


            //XmlTextReader reader = new XmlTextReader(url2);
            //System.Data.DataSet ds = new System.Data.DataSet();
            //ds.ReadXml(reader);
            //string Status = ds.Tables[0].Rows[0]["status"].ToString();
            //if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
            ////if (Status == "ZERO_RESULTS")
            //{
            //    url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=null&destination=null&sensor=false";
            //    MileageError();
            //}
            //else
            //{

            //    DataTable dt = ds.Tables["duration"];

            //    DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
            //    time = row.ItemArray[1].ToString();

            //    decimal miles = milesList.Sum();
            //    if (miles > 10000 || mileageError)
            //        MileageError();
            //    else
            //    {
            //        lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles. Time :" + time + "";
            //    }
            //}

            //CalculateTotalCharges();
        }

        private void btnJobRoutePath_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (objMaster.PrimaryKeyValue != null && objMaster.Current.Booking_RoutePaths.Count > 0)
            //    {

            //        rptJobRouthPath rptRoute = new rptJobRouthPath(objMaster.Current);
            //        rptRoute.StartPosition = FormStartPosition.CenterScreen;
            //        rptRoute.ShowDialog();

            //    }
            //    else
            //    {
            //        ENUtils.ShowMessage("Map Route Details not found");

            //    }
            //}
            //catch(Exception ex)
            //{

            //}
        }

        private void btnLogDetail_Click(object sender, EventArgs e)
        {
            try
            {
                rptfrmJobLog jobLog = new rptfrmJobLog(txtBookingNo.Text);
                jobLog.FormBorderStyle = FormBorderStyle.FixedSingle;
                jobLog.StartPosition = FormStartPosition.CenterScreen;
                jobLog.ShowDialog();
                jobLog.Dispose();
            }
            catch 
            {


            }
        }
    }
}

