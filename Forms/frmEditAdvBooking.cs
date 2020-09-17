using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using System.Net;
using UI;
using Taxi_Model;
using System.Xml.Linq;
using Telerik.WinControls.UI;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmEditAdvBooking : Form
    {

        private Booking _objBooking;

        public Booking ObjBooking
        {
            get { return _objBooking; }
            set { _objBooking = value; }
        }

        string[] res = null;
        string searchTxt = "";
        bool IsKeyword = false;
        private bool EnablePOI = false;

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



                if (EnablePOI && searchTxt.ToUpper().StartsWith("P="))
                {
                    if (searchTxt.Trim().Length > 2)
                    {
                        IsPOI = true;
                        searchTxt = searchTxt.ToUpper().Replace("P=", "").Trim();
                    }

                }


                string postCode = General.GetPostCodeMatch(searchTxt.ToUpper());
                if (!string.IsNullOrEmpty(postCode) && postCode.IsAlpha() == true)
                    postCode = string.Empty;

                string street = searchTxt;
                if (!string.IsNullOrEmpty(postCode))
                    street = street.ToLower().Replace(postCode.ToLower(), "").Trim();



                //if (EnablePOI && IsPOI)
                //{

                //    res = (from a in AppVars.listofPOI

                //           where (a.FullAddress.Contains(street.ToUpper()) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))
                //           select a.FullAddress
                //                       ).Take(1000).ToArray<string>();



                //}
                //else
                //{
                    //if (UseGoogleMap == false || IsExistData)
                    //{
                    res = (from a in AppVars.listOfAddress

                           where (a.AddressLine1.ToLower().Contains(street) && ((postCode == string.Empty || a.PostalCode.StartsWith(postCode))))
                           select a.AddressLine1

                                   ).Take(1000).ToArray<string>();

                    //}

                    if (UseGoogleMap && res.Count() == 0)
                    {


                        string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + searchTxt + " UK&sensor=false";
                        IsAutoComplete = IsAutoComplete == true ? false : true;

                        wc.CancelAsync();

                        wc = new WebClient();
                        wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
                        wc.DownloadStringAsync(new Uri(url));


                        return;
                    }
            //    }
                // Zone Working

                //   searchTxt = searchTxt.Substring(0, 3);


                ShowAddresses();
            }
            catch (Exception ex)
            {


            }
        }

        AutoCompleteTextBox aTxt = null;
        bool IsAutoComplete = false;
        WebClient wc = new WebClient();
        bool IsPOI = false;
        void TextBoxElement_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IsPOI = false;
                IsKeyword = false;
                timer1.Stop();
              //  IsExistData = false;
                aTxt = (AutoCompleteTextBox)sender;
                aTxt.ResetListBox();

                if (aTxt.Name == "txtFromAddress")
                {

                    txtToAddress.SendToBack();

                }
                else if (aTxt.Name == "txtToAddress")
                {
                    txtToAddress.BringToFront();
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

                            }
                        }

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
            catch (Exception ex)
            {

            }
        }

     //   private bool IsExistData = false;

        private void StartAddressTimer(string text)
        {

            text = text.ToLower();
            searchTxt = text;

            timer1.Start();

        }

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



        public frmEditAdvBooking(Booking obj,string day)
        {
            InitializeComponent();
            this.ObjBooking = obj;
            lblHeading.Text ="Day " +day;
            InitializeConstructor();
        }


        private void InitializeConstructor()
        {

      



        
            ComboFunctions.FillLocationTypeCombo(ddlFromLocType);
            ComboFunctions.FillLocationTypeCombo(ddlToLocType);



            numFareRate.Value = this.ObjBooking.FareRate.ToDecimal();
            numReturnFareRate.Value = ObjBooking.ReturnFareRate.ToDecimal();

            SetLocation(this.ObjBooking.FromLocTypeId, this.ObjBooking.FromDoorNo, this.ObjBooking.FromAddress, this.ObjBooking.FromStreet, this.ObjBooking.FromPostCode, this.ObjBooking.FromLocId,
                         this.ObjBooking.ToLocTypeId, this.ObjBooking.ToDoorNo, this.ObjBooking.ToAddress, this.ObjBooking.ToStreet, this.ObjBooking.ToPostCode, this.ObjBooking.ToLocId);


            this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

         

            if (AppVars.objPolicyConfiguration.MapType.ToInt() == Enums.MAP_TYPE.GOOGLEMAPS)
            {
                UseGoogleMap = true;
            }

            dtpPickupDate.Value = ObjBooking.PickupDateTime;
            dtpPickupTime.Value = ObjBooking.PickupDateTime;

            if (ObjBooking.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
            {
                chkIsReturn.Checked = true;
                dtpReturnPickupDate.Value = ObjBooking.ReturnPickupDateTime;
                dtpReturnPickupTime.Value = ObjBooking.ReturnPickupDateTime;
                SetReturnPickupDate(ToggleState.On);
            }
            else
                SetReturnPickupDate(ToggleState.Off);


            timer1.Tick += new EventHandler(timer1_Tick);

            if (AppVars.objPolicyConfiguration != null)
            {
                MapType = AppVars.objPolicyConfiguration.MapType.ToInt();
            }


            txtFromAddress.ListBoxElement.Width = txtFromAddress.DefaultWidth;
            txtToAddress.ListBoxElement.Width = txtToAddress.DefaultWidth;

        }

       

        private void ddlFromLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillFromLocations();
        }

        private void ddlToLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillToLocations();
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

                txtFromFlightDoorNo.Text = string.Empty;
                txtFromFlightDoorNo.Visible = false;

                txtFromStreetComing.Text = string.Empty;
                txtFromStreetComing.Visible = false;

                lblFromDoorFlightNo.Visible = false;
                lblFromStreetComing.Visible = false;

                lblFromLoc.Text = "Pickup Point";
            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtFromAddress.Text = string.Empty;
                txtFromAddress.Visible = false;

                ddlFromLocation.SelectedValue = null;
                ddlFromLocation.Visible = false;

                txtFromPostCode.Visible = true;
                txtFromFlightDoorNo.Visible = true;
                txtFromStreetComing.Visible = true;
                lblFromDoorFlightNo.Visible = true;
                lblFromStreetComing.Visible = true;

                lblFromLoc.Text = "From PostCode";

                lblFromDoorFlightNo.Text = "From Door No";
                lblFromStreetComing.Text = "From Street";

            }

            else if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                txtFromAddress.Text = string.Empty;
                txtFromAddress.Visible = false;

                ddlFromLocation.Visible = true;

                txtFromPostCode.Text = string.Empty;
                txtFromPostCode.Visible = false;

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
            }
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

                txtToFlightDoorNo.Text = string.Empty;
                txtToFlightDoorNo.Visible = false;

                txtToStreetComing.Text = string.Empty;
                txtToStreetComing.Visible = false;


                lblToDoorFlightNo.Visible = false;
                lblToStreetComing.Visible = false;
            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtToAddress.Text = string.Empty;
                txtToAddress.Visible = false;

                ddlToLocation.SelectedValue = null;
                ddlToLocation.Visible = false;

                txtToPostCode.Visible = true;
                txtToFlightDoorNo.Visible = true;
                txtToStreetComing.Visible = true;
                lblToDoorFlightNo.Visible = true;
                lblToStreetComing.Visible = true;

                lblToLoc.Text = "To PostCode";

                lblToDoorFlightNo.Text = "To Door No";
                lblToStreetComing.Text = "To Street";

            }


            else
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
                ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);

            }
        }


        private void SetLocation(int? fromLocTypeId, string fromDoorNo, string fromAddress, string fromStreet, string fromPostCode, int? fromLocId,
                        int? toLocTypeId, string ToDoorNo, string ToAddress, string ToStreet, string ToPostCode, int? ToLocId)
        {

            ddlFromLocType.SelectedValue = fromLocTypeId;
            ddlToLocType.SelectedValue = toLocTypeId;

            if (fromLocTypeId == Enums.LOCATION_TYPES.BASE || fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
            {
                txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = fromAddress;
                txtFromFlightDoorNo.Text = fromDoorNo;
                txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            }
            else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtFromPostCode.Text = fromPostCode;
                txtFromStreetComing.Text = fromStreet;
                txtFromFlightDoorNo.Text = fromDoorNo;
            }
            else if (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                ddlFromLocation.SelectedValue = fromLocId;
                txtFromStreetComing.Text = fromStreet;
                txtFromFlightDoorNo.Text = fromDoorNo;
            }
            else
            {
                ddlFromLocation.SelectedValue = fromLocId;

            }


            // To

            if (toLocTypeId == Enums.LOCATION_TYPES.BASE || toLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
            {
                txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = ToAddress;

                txtToFlightDoorNo.Text = ToDoorNo;

                txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            }
            else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtToPostCode.Text = ToPostCode;
                txtToStreetComing.Text = ToStreet;
                txtToFlightDoorNo.Text = ToDoorNo;
            }
            else if (toLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                ddlToLocation.SelectedValue = ToLocId;
                //txtFromStreetComing.Text = fromStreet;
                //txtFromFlightDoorNo.Text = fromDoorNo;
            }
            else
            {
                ddlToLocation.SelectedValue = ToLocId;

            }



        }

        private void btnSave_Click(object sender, EventArgs e)
        {

     

            int FromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
            int ToLocTypeId = ddlToLocType.SelectedValue.ToInt();

            ObjBooking.FromLocTypeId = FromLocTypeId;
            ObjBooking.ToLocTypeId = ToLocTypeId;
            ObjBooking.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
            ObjBooking.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();


            if (FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromLocTypeId == Enums.LOCATION_TYPES.BASE)
                ObjBooking.FromAddress = txtFromAddress.Text.Trim();
            else if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                ObjBooking.FromAddress = txtFromPostCode.Text.Trim();
            else
            {
                ObjBooking.FromAddress = ddlFromLocation.Text.Trim();
            }



            ObjBooking.FromDoorNo = txtFromFlightDoorNo.Text.Trim();
            ObjBooking.FromStreet = txtFromStreetComing.Text.Trim();
            ObjBooking.FromPostCode = txtFromPostCode.Text.Trim();


            if (ToLocTypeId == Enums.LOCATION_TYPES.ADDRESS || ToLocTypeId==Enums.LOCATION_TYPES.BASE)
                ObjBooking.ToAddress = txtToAddress.Text.StripNewLine().Trim();

            else if (ToLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                ObjBooking.ToAddress = txtToPostCode.Text.Trim();
            else
            {
                ObjBooking.ToAddress = ddlToLocation.Text.Trim();
            }


            ObjBooking.ToDoorNo = txtToFlightDoorNo.Text.Trim();
            ObjBooking.ToStreet = txtToStreetComing.Text.Trim();
            ObjBooking.ToPostCode = txtToPostCode.Text.Trim();




            this.ObjBooking.PickupDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay).ToDateTime();

           
            if (dtpReturnPickupDate.Value != null && dtpReturnPickupTime.Value != null)
            {
                this.ObjBooking.ReturnPickupDateTime = dtpReturnPickupDate.Value.ToDateorNull() + dtpReturnPickupTime.Value.Value.TimeOfDay;
            }
            else
                 this.ObjBooking.ReturnPickupDateTime = null;



            this.ObjBooking.FareRate = numFareRate.Value;
            this.ObjBooking.ReturnFareRate = numReturnFareRate.Value;

            this.Close();
        }

        private void SetReturnPickupDate(ToggleState toggle)
        {

            if (toggle == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                dtpReturnPickupDate.Value = dtpPickupDate.Value;
                dtpReturnPickupDate.Value = dtpPickupDate.Value;
                numReturnFareRate.Enabled = true;
            }
            else
            {

                dtpReturnPickupDate.Enabled = false;
                dtpReturnPickupTime.Enabled = false;
                numReturnFareRate.Enabled = false;

                dtpReturnPickupDate.Value = null;
                dtpReturnPickupDate.Value = null;    

                numReturnFareRate.Value = 0;
            }

        }

        private void chkIsReturn_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            SetReturnPickupDate(args.NewValue);
        }

        private void btnCalculateFare_Click(object sender, EventArgs e)
        {
            try
            {

                CalculateFares();

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }

        }
        List<decimal> milesList = new List<decimal>();


        private bool CalculateFares()
        {
            milesList.Clear();
            int? vehicleTypeId = ObjBooking.VehicleTypeId.ToIntorNull();
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



            int tempFromLocId = 0;
            int tempToLocId = 0;
            string tempFromPostCode = "";
            string tempToPostCode = "";
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



            bool IsCompanyFareExist = false;
            string estimatedTime = string.Empty;

            fareVal += General.GetFareRate(ObjBooking.SubcompanyId.ToInt(), companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, false, ObjBooking.PickupDateTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime);
                
            

            if (errorMsg == "Error")
            {

               numFareRate.Value = 0;
                return false;
            }



            string ff = string.Format("{0:#}", fareVal);
            if (ff == string.Empty)
                ff = "0";
            decimal dd = ff.ToDecimal();

            // Add Airport Pickup Charges If Pickup Point is From Airport...
            if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();

            numFareRate.Value = dd;


            if (chkIsReturn.Checked)
            {
                int discountReturn = AppVars.objPolicyConfiguration.DiscountForReturnedJourneyPercent.ToInt();

                numReturnFareRate.Value = numFareRate.Value - ((numFareRate.Value * discountReturn) / 100);

                if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    numReturnFareRate.Value += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();


            }
            else
                numReturnFareRate.Value = 0;


            return true;

        }



    }
}
