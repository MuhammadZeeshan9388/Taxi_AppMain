﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Xml;
using Utils;

using Telerik.WinControls.UI;
using System.Data.Linq;

using Taxi_BLL;
using Taxi_Model;
using Telerik.WinControls.Enumerations;
using DAL;
using Telerik.WinControls;

using System.Threading;
using UI;
using System.Collections;

using System.Runtime.InteropServices;
using System.Diagnostics;
using DotNetCoords;
using System.Net;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Taxi_AppMain.Classes;



namespace Taxi_AppMain
{
    public partial class frmBooking : Form
    {
        private bool WasQuotiation;

        private bool IsDespatched;

        private int? _PickBookingTypeId = AppVars.objPolicyConfiguration.DefaultBookingTypeId;

        public int? PickBookingTypeId
        {
            get { return _PickBookingTypeId; }
            set { _PickBookingTypeId = value; }
        }


        private bool _IsAccountCalled;

        public bool IsAccountCalled
        {
            get { return _IsAccountCalled; }
            set { _IsAccountCalled = value; }
        }

        frmDespatchJob frm = null;



        bool IsKeyword = false;

        private Thread th = null;

        private bool saved = false;
        private int _MapType;
        public int MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }



        private string _CallRefNo;

        public string CallRefNo
        {
            get { return _CallRefNo; }
            set { _CallRefNo = value; }
        }



        private Point OldfromDoorNoLoc;
        private Point NewFromDoorNoLoc;
        private Point OldtoDoorNoLoc;
        private Point NewtoDoorNoLoc;



        BookingBO objMaster;
        Booking_Payment objBookingPayment = null;

        private int? _PickSubCompanyId;

        public int? PickSubCompanyId
        {
            get { return _PickSubCompanyId; }
            set { _PickSubCompanyId = value; }
        }

        public frmBooking()
        {
            InitializeComponent();

            InitializeConstructor();
        }


        private int openedFrom;

        public frmBooking(int openFrom)
        {
            InitializeComponent();

            this.openedFrom = openFrom;


            InitializeConstructor();
        }



        public frmBooking(string name, string phone,int? AccountId,bool IsAccountCall)
        {


            InitializeComponent();
            InitializeConstructor();
            name = name.ToProperCase();

            if (IsAccountCall == false)
            {
                ddlCustomerName.Text = name;

            }
    
            if (phone.StartsWith("07"))
            {
                txtCustomerMobileNo.Text = phone;
            }
            else
            {
                txtCustomerPhoneNo.Text = phone;
            }

           
            //else
            //{
            if (AccountId != null)
            {
                IsDisplayingRecord = true;
                chkIsCompanyRates.Checked = true;
                ddlCompany.SelectedValue = AccountId;
              
                IsDisplayingRecord = false;

            }
        //    }


          //      SetAccountByCustomer();
           


            PickCustomerCreditCardNo(name, phone);

        }

        public frmBooking(string name, string phone, string doorNo, string address,int? AccountId,bool IsAccountCall)
        {


            InitializeComponent();
            InitializeConstructor();
            name = name.ToProperCase();

            if (IsAccountCall == false)
            {
                ddlCustomerName.Text = name;
            }
            else
            {
                if (AccountId != null)
                {
                    IsDisplayingRecord = true;
                    chkIsCompanyRates.Checked = true;
                    ddlCompany.SelectedValue = AccountId;
                    IsDisplayingRecord = false;

                }

            }
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


          //  SetAccountByCustomer();

            PickCustomerCreditCardNo(name, phone);

        }


      
        public frmBooking(string name, string phone, string mobileNo, string doorNo, string address, string email,int? AccountId,bool IsAccountCall)
        {


            InitializeComponent();
            InitializeConstructor();

           
                name = name.ToProperCase();

                if (IsAccountCall == false)
                {
                    ddlCustomerName.Text = name;
                }
                else
                {
                    if (AccountId != null)
                    {
                        IsDisplayingRecord = true;
                        chkIsCompanyRates.Checked = true;
                        ddlCompany.SelectedValue = AccountId;
                        IsDisplayingRecord = false;
                    }
                }

            txtCustomerPhoneNo.Text = phone;
            txtCustomerMobileNo.Text = mobileNo;
            txtEmail.Text = email;

            txtFromFlightDoorNo.Text = doorNo;
            txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            txtFromAddress.Text = address;
            txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
        }

        public frmBooking(string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                          int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, string doorNo)
        {



            InitializeComponent();
            InitializeConstructor();

            txtFromFlightDoorNo.Text = doorNo;
            PickBooking(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse);

            btnSearch.Visible = false;


            PickCustomerCreditCardNo(name, phone);
        }


        public frmBooking(string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                    int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, string fromDoorNo, string toDoorNo, string email, int? companyId,bool IsAccountCall)
        {



            InitializeComponent();
            InitializeConstructor();
            IsDisplayingRecord = true;

            txtFromFlightDoorNo.Text = fromDoorNo.ToStr().Trim();
            txtToFlightDoorNo.Text = toDoorNo.ToStr().Trim();

            txtEmail.Text = email;

            this.IsAccountCalled = IsAccountCall;

            if (companyId != null)
            {
               
                chkIsCompanyRates.Checked = true;
                ddlCompany.SelectedValue = companyId;
                numCompanyFares.Value = fare;
             
            }


            PickBooking(name, phone, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse);


            btnSearch.Visible = false;


            PickCustomerCreditCardNo(name, phone);

            IsDisplayingRecord = false;
        }


        private void PickCustomerCreditCardNo(string name, string phoneNo)
        {

            //if (AppVars.objPolicyConfiguration.BookingPaymentDetailsType.ToInt() == 2)
            //{

            //    try
            //    {
            //        txtCustomerCreditCardNo.Text = General.GetObject<Customer>(c => c.Name.ToLower() == name.ToLower() && (c.MobileNo == phoneNo || c.TelephoneNo == phoneNo)).DefaultIfEmpty().CreditCardDetails.ToStr().Trim();

            //    }
            //    catch
            //    {


            //    }
            //}


        }


        private void PickBooking(string name, string phone, int? fromLocTypeId, int? toLocTypeId,
                          int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse)
        {


            name = name.ToProperCase();


            if (IsAccountCalled == false)
            {

                ddlCustomerName.Text = name;

            }
          
            
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

            if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
            {
                this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = fromAddress;
                SetPickupZone(txtFromAddress.Text);
                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }

            else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtFromPostCode.Text = fromAddress;
            }
            else
            {
                DetachLocationsSelectionEvent(ddlFromLocation);
                ddlFromLocation.SelectedValue = fromLocId;
                AttachLocationSelectionEvent(ddlFromLocation);
            }


            if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
            {
                this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = toAddress;
                SetDropOffZone(txtToAddress.Text);
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }
            else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtToPostCode.Text = toAddress;
            }
            else
            {
                DetachLocationsSelectionEvent(ddlToLocation);
                ddlToLocation.SelectedValue = toLocId;
                AttachLocationSelectionEvent(ddlToLocation);
            }




            numFareRate.Value = fare;




            ddlCustomerName.Focus();


        }



        private void PickBookingComplete(string name, string phone, string mobileNo, int? fromLocTypeId, int? toLocTypeId,
                         int? fromLocId, int? toLocId, string fromAddress, string toAddress, decimal fare, bool IsReverse, int? bookingTypeId, string email)
        {
            OnPickDetails(name, fromLocTypeId, toLocTypeId, fromLocId, toLocId, fromAddress, toAddress, fare, IsReverse);

            txtCustomerPhoneNo.Text = phone;
            txtCustomerMobileNo.Text = mobileNo;
            txtEmail.Text = email.ToStr().Trim();

            ddlBookingType.SelectedValue = bookingTypeId;


            PickCustomerCreditCardNo(name, phone);

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

            if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
            {
                this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = fromAddress;
                SetPickupZone(txtFromAddress.Text);
                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }
            else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
            {
                DetachLocationsSelectionEvent(ddlFromLocation);

                ddlFromLocation.SelectedValue = fromLocId;
                SetPickupZone(ddlFromLocation.Text);
                AttachLocationSelectionEvent(ddlFromLocation);
            }
            else if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtFromPostCode.Text = fromAddress;
            }
            else
            {
                ddlFromLocation.SelectedValue = fromLocId;
            }


            if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
            {
                this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = toAddress;
                SetDropOffZone(txtToAddress.Text);
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            }
            else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {
                txtToPostCode.Text = toAddress;
            }
            else
            {
                DetachLocationsSelectionEvent(ddlToLocation);
                ddlToLocation.SelectedValue = toLocId;
                SetDropOffZone(ddlToLocation.Text);
                AttachLocationSelectionEvent(ddlToLocation);
            }


            numFareRate.Value = fare;
            ddlCustomerName.Focus();


        }



        private void InitializeConstructor()
        {



            try
            {





                ddlVehicleType.KeyDown += new KeyEventHandler(ddlVehicleType_KeyDown);
                btnMultiVehicle.Click += new EventHandler(btnMultiVehicle_Click);
                ddlCustomerName.KeyDown += new KeyEventHandler(ddlCustomerName_KeyDown);
                ddlCustomerName.Validated += new EventHandler(ddlCustomerName_Validated);
                txtCustomerMobileNo.KeyDown += new KeyEventHandler(txtCustomerMobileNo_KeyDown);
                txtCustomerPhoneNo.KeyDown += new KeyEventHandler(txtCustomerPhoneNo_KeyDown);
                btnCustomerLister.Click += new EventHandler(btnCustomerLister_Click);
                btnPickCustomerAddress.Click += new EventHandler(btnPickCustomerAddress_Click);
                btn_notes.Click += new EventHandler(btn_notes_Click);
                dtpPickupDate.KeyDown += new KeyEventHandler(dtpPickupDate_KeyDown);



                ddlPickupPlot.KeyDown += new KeyEventHandler(ddlPickupPlot_KeyDown);


                ddlDropOffPlot.KeyDown += new KeyEventHandler(ddlDropOffPlot_KeyDown);



                //if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool()
                //    && AppVars.listUserRights.Count(c => c.functionId == "DISABLE PLOT WISE FARE LIST" && c.formName == "frmFares") == 0)
                //{
                //    ddlPickupPlot.SelectedValueChanged += new EventHandler(ddlPlot_SelectedValueChanged);
                //    ddlDropOffPlot.SelectedValueChanged += new EventHandler(ddlPlot_SelectedValueChanged);
                //}


                dtpPickupTime.Leave += new EventHandler(dtpPickupTime_Leave);
                dtpPickupTime.DateTimePickerElement.TextBoxElement.TextBoxItem.KeyPress += new KeyPressEventHandler(dtpPickupTime_KeyPress);
                dtpPickupTime.DateTimePickerElement.TextBoxElement.TextBoxItem.KeyDown += new KeyEventHandler(dtpPickupTime_KeyDown);


                ddlDriver.GotFocus += new EventHandler(ddlDriver_GotFocus);
                ddlDriver.KeyDown += new KeyEventHandler(ddlDriver_KeyDown);
                ddlDriver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlDriver_SelectedIndexChanged);



                opt_JOneWay.ToggleState = ToggleState.On;
                opt_JOneWay.ToggleStateChanged += new StateChangedEventHandler(opt_JOneWay_ToggleStateChanged);
                opt_JOneWay.ToggleStateChanging += new StateChangingEventHandler(opt_JOneWay_ToggleStateChanging);


                opt_JReturnWay.ToggleStateChanging += new StateChangingEventHandler(opt_JReturnWay_ToggleStateChanging);

                opt_WaitandReturn.ToggleStateChanged += new StateChangedEventHandler(opt_WaitandReturn_ToggleStateChanged);

                chkIsCompanyRates.ToggleStateChanged += new StateChangedEventHandler(chkIsCompanyRates_ToggleStateChanged);
                ddlCompany.KeyDown += new KeyEventHandler(ddlCompany_KeyDown);
          //      ddlCompany.Leave += new EventHandler(ddlCompany_Leave);


                btnSms.Click += new EventHandler(btnSms_Click);



                chkAutoDespatch.ToggleStateChanging += new StateChangingEventHandler(chkAutoDespatch_ToggleStateChanging);

                txtSpecialRequirements.KeyDown += new KeyEventHandler(txtSpecialRequirements_KeyDown);
                btnSaveNew.Click += new EventHandler(btnSaveNew_Click);
                btnCancelBooking.Click += new EventHandler(btnCancelBooking_Click);
                btnExitForm.Click += new EventHandler(btnExitForm_Click);

                btnConfirmBooking.Click += new EventHandler(btnConfirmBooking_Click);

                //ddlPaymentType.SelectedValueChanged += new EventHandler(ddlPaymentType_SelectedValueChanged);
                btnPayment.Click += new EventHandler(btnPayment_Click);


                chkIsCommissionWise.ToggleStateChanged += new StateChangedEventHandler(chkIsCommissionWise_ToggleStateChanged);
                ddlCommissionType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlCommissionType_SelectedIndexChanged);

                btnPickFares.Click += new EventHandler(btnPickFares_Click);

                numFareRate.Validated += new EventHandler(numFareRate_Validated);
                numParkingChrgs.Validated += new EventHandler(numFareRate_Validated);
                numWaitingChrgs.Validated += new EventHandler(numFareRate_Validated);
                numMeetCharges.Validated += new EventHandler(numFareRate_Validated);
                numCongChrgs.Validated += new EventHandler(numFareRate_Validated);
                numExtraChrgs.Validated += new EventHandler(numFareRate_Validated);

                btnDetailMap.Click += new EventHandler(btnDetailMap_Click);

                RadTextBoxItem TotalPass = ((RadSpinElement)num_TotalPassengers.RootElement.Children[0]).TextBoxItem;
                TotalPass.KeyDown += new KeyEventHandler(child_KeyDown);

                RadTextBoxItem totalLugg = ((RadSpinElement)numTotalLuggages.RootElement.Children[0]).TextBoxItem;
                totalLugg.KeyDown += new KeyEventHandler(totalLugg_KeyDown);

                numFareRate.SpinElement.TextBoxItem.KeyDown += new KeyEventHandler(TextBoxItem_KeyDown);
                ddlDriver.Leave += new EventHandler(ddlDriver_Leave);

                txtEmail.KeyDown += new KeyEventHandler(txtEmail_KeyDown);

                this.Load += new EventHandler(frmBooking_Load);
                this.FormClosed += new FormClosedEventHandler(frmBooking_FormClosed);
                this.Shown += new EventHandler(frmBooking_Shown);

                btnCancelBooking.Enabled = false;


                this.OldfromDoorNoLoc = txtFromFlightDoorNo.Location;
                this.NewFromDoorNoLoc = new Point(txtFromFlightDoorNo.Location.X, txtFromFlightDoorNo.Location.Y - 56);

                this.OldtoDoorNoLoc = txtToFlightDoorNo.Location;
                this.NewtoDoorNoLoc = new Point(txtToFlightDoorNo.Location.X, txtToFlightDoorNo.Location.Y - 56);

                //needtouncomment

                this.txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtFromAddress.TextBoxElement.KeyDown += new KeyEventHandler(TextBoxFromAddressElement_KeyDown);





                txtFromAddress.ListBoxElement.Width = 610;
                txtFromAddress.ListBoxElement.Height = 400;
                txtToAddress.ListBoxElement.Width = 610;


                Font font = new Font("Tahoma", 11, FontStyle.Bold);
                txtFromAddress.ListBoxElement.Font = font;
                txtToAddress.ListBoxElement.Font = font;

                txtFromAddress.ListBoxElement.ItemHeight = 30;
                txtToAddress.ListBoxElement.ItemHeight = 30;

                //2needtouncomment
                txtFromAddress.ListBoxElement.DrawMode = DrawMode.OwnerDrawVariable;
                txtFromAddress.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);

                txtToAddress.ListBoxElement.DrawMode = DrawMode.OwnerDrawVariable;
                txtToAddress.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);


                txtToAddress.TextBoxElement.KeyDown += new KeyEventHandler(TextBoxElement_KeyDown);


                //  txtToAddress.TextBoxElement.KeyDown

                if (txtReturnFrom != null)
                {
                    txtReturnFrom.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                    txtReturnFrom.ListBoxElement.Width = 610;
                    txtReturnFrom.ListBoxElement.Height = 400;
                    txtReturnFrom.ListBoxElement.Font = font;

                    txtReturnFrom.ListBoxElement.DrawMode = DrawMode.OwnerDrawVariable;
                    txtReturnFrom.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
                    txtReturnFrom.ListBoxElement.ItemHeight = 30;
                }

                if (txtReturnTo != null)
                {
                    txtReturnTo.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    txtReturnTo.ListBoxElement.Width = 610;
                    txtReturnTo.ListBoxElement.Height = 400;
                    txtReturnTo.ListBoxElement.Font = font;

                    txtReturnTo.ListBoxElement.DrawMode = DrawMode.OwnerDrawVariable;
                    txtReturnTo.ListBoxElement.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
                    txtReturnTo.ListBoxElement.ItemHeight = 30;
                }


                if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                {

                    ddlFromLocation.Leave += new EventHandler(txtFromAddress_Leave);
                    ddlToLocation.Leave += new EventHandler(txtFromAddress_Leave);
                    txtFromAddress.Leave += new EventHandler(txtFromAddress_Leave);
                    txtToAddress.Leave += new EventHandler(txtFromAddress_Leave);


                    // txtToAddress.Validated += new EventHandler(txtToAddress_Validated);
                }





                objMaster = new BookingBO();
                // this.SetProperties((INavigation)objMaster);// need to uncomment again

                FillCombos();

                pnlOtherCharges.Visible = AppVars.objPolicyConfiguration.EnableBookingOtherCharges.ToBool();




                OnNew();

                ddlVehicleType.SelectedValueChanged += new EventHandler(ddlVehicleType_SelectedValueChanged);

                ddlPaymentType.SelectedValueChanged += new EventHandler(ddlPaymentType_SelectedValueChanged);

                ddlBookingType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlBookingType_SelectedIndexChanged);

                SetBookingTypeDetails(ddlBookingType.SelectedValue.ToInt());

                EnablePOI = AppVars.objPolicyConfiguration.EnablePOI.ToBool();


                SetDefaultCommission();

                if (!AppVars.objPolicyConfiguration.EnablePDA.ToBool())
                {
                    btnNearestDrv.Visible = false;
                    btnViewMapReport.Visible = false;

                }


                MapType = AppVars.objPolicyConfiguration.MapType.ToInt();
                chkAutoDespatch.Enabled = AppVars.objPolicyConfiguration.EnablePDA.ToBool() ? true : false;


                if (chkAutoDespatch.Enabled)
                {
                    lblPickupPlot.Visible = true;
                    lblDropOffPlot.Visible = true;
                }



                chkIsCommissionWise.Enabled = !AppVars.objPolicyConfiguration.DisableDriverCommissionTick.ToBool();


                if (AppVars.objPolicyConfiguration.DisableDriverCommissionTick.ToBool())
                {
                    chkIsCommissionWise.Visible = false;
                    ddlCommissionType.Visible = false;
                    numDriverCommission.Visible = false;


                }


                chkSecondaryPaymentType.ToggleStateChanged += new StateChangedEventHandler(chkSecondaryPaymentType_ToggleStateChanged);


                numDrvWaitingMins.SpinElement.ValueChanging += new ValueChangingEventHandler(NumDrvWaitingMinsSpinElement_ValueChanging);
             //   numDrvWaitingMins.Enabled = false;

               // btnDespatchView.Visible = false;



                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "E.E-Car" || AppVars.objPolicyConfiguration.DefaultClientId.ToStr() =="local")
                {

                    InitializeAccountBookedBy(true, null);

                }
                       

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        void SpinElement_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            GetFareByJourneyTime(e.NewValue.ToDecimal());
        }

        void ddlCompany_Leave(object sender, EventArgs e)
        {
         
          //  if (ddlCompany.SelectedValue != null && ddlCompany.Text.Length > 0)
              //  ddlCompany.ListElement.drop.AutoEllipsis = true;
         //   ddlCompany.DropDownListElement.EditableElement.TextBox.autoel = true;
        }

        void NumDrvWaitingMinsSpinElement_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (IsDisplayingRecord)
                return;

            if (e.NewValue != null && ddlVehicleType.SelectedValue != null)
            {

                try
                {
                    decimal val = e.NewValue.ToInt();


                    if (val == 0)
                    {
                        numMeetCharges.Value = 0.00m;

                        if (ddlCompany.SelectedValue != null && numWaitingChrgs.Enabled)
                        {

                            numWaitingChrgs.Value = 0.00m;
                        }

                        CalculateTotalCharges();

                    }
                    else
                    {

                        var objVehicle = General.GetObject<Fleet_VehicleType>(c => c.Id == ddlVehicleType.SelectedValue.ToInt());

                        if (objVehicle != null)
                        {
                            numMeetCharges.Value = Math.Round(((val * objVehicle.DriverWaitingChargesPerHour.ToDecimal())), 1);



                            if (ddlCompany.SelectedValue != null && numWaitingChrgs.Enabled)
                            {

                                numWaitingChrgs.Value = Math.Round(((val * objVehicle.AccountWaitingChargesPerHour.ToDecimal())), 1);


                            }

                            CalculateTotalCharges();

                        }

                    }

                }
                catch
                {


                }


            }
        }



        void ddlVehicleType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlVehicleType.SelectedValue != null)
            {


                PickVehicleDetails();


                UpdateAutoCalculateFares();
            }
        }


        private void PickVehicleDetails()
        {

            if (AppVars.objPolicyConfiguration.PickVehicleDetailsOnBooking.ToBool())
            {

                var objVeh = General.GetObject<Fleet_VehicleType>(c => c.Id == ddlVehicleType.SelectedValue.ToInt());

                if (objVeh != null)
                {
                    num_TotalPassengers.Value = objVeh.NoofPassengers.ToDecimal();
                    numTotalLuggages.Value = objVeh.NoofLuggages.ToDecimal();

                }
            }

        }

        void ddlPlot_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculatePlotWiseFares();

        }


        private void CalculatePlotWiseFares()
        {

            try
            {
                int fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
                int toLocTypeId = ddlToLocType.SelectedValue.ToInt();

                if (
                    (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS && txtFromAddress.Text.Length > 0)
                     || (fromLocTypeId == Enums.LOCATION_TYPES.BASE && txtFromAddress.Text.Length > 0)
                    || (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE && txtFromPostCode.Text.Length > 0)
                     || (ddlFromLocation.SelectedValue != null)

                    )
                    return;

                if (
                (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS && txtToAddress.Text.Length > 0)
                 || (toLocTypeId == Enums.LOCATION_TYPES.BASE && txtToAddress.Text.Length > 0)
                || (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE && txtToPostCode.Text.Length > 0)
                 || (ddlToLocation.SelectedValue != null)

                )
                    return;



                int vehicleTypeId = ddlVehicleType.SelectedValue.ToInt();


                int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
                bool IsMoreFareWise = false;
                if (vehicleTypeId != defaultVehicleId)
                {
                    vehicleTypeId = defaultVehicleId;
                    IsMoreFareWise = true;
                }

                int fromZoneId = ddlPickupPlot.SelectedValue.ToInt();
                int toZoneId = ddlDropOffPlot.SelectedValue.ToInt();
                decimal fareVal = 0.00m;



                decimal returnFares = 0.00m;



                if (fromZoneId != 0 && toZoneId != 0)
                {


                    var objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == fromZoneId && c.ToZoneId == toZoneId);

                    if (objPlotFare == null)
                        objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == toZoneId && c.ToZoneId == fromZoneId);


                    if (objPlotFare != null)
                    {
                        fareVal = objPlotFare.Price.ToDecimal();


                        objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == toZoneId && c.ToZoneId == fromZoneId);
                        if (objPlotFare == null)
                        {

                            returnFares = fareVal;
                        }
                        else
                            returnFares = objPlotFare.Price.ToDecimal();


                    }


                }




                decimal peakFares = 0.00m;
                decimal rtnPeakFares = 0.00m;


                DateTime? pickupDateTime = DateTime.Now;
                if (fareVal > 0)
                {



                    pickupDateTime = dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;
                    TimeSpan pickupTime = new TimeSpan(pickupDateTime.Value.TimeOfDay.Hours, pickupDateTime.Value.Minute, 0);
                    int pickupDay = (int)pickupDateTime.Value.DayOfWeek;



                    DateTime? returnpickupdateTime = null;
                    TimeSpan returnpickupTime = TimeSpan.Zero;
                    int returnpickupDay = 0;

                    if (opt_JReturnWay.ToggleState == ToggleState.On && dtpReturnPickupDate != null && dtpReturnPickupDate.Value != null
                        && dtpReturnPickupTime != null && dtpReturnPickupTime.Value != null)
                    {

                        returnpickupdateTime = dtpReturnPickupDate.Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                        returnpickupTime = new TimeSpan(returnpickupdateTime.Value.TimeOfDay.Hours, returnpickupdateTime.Value.Minute, 0);
                        returnpickupDay = (int)pickupDateTime.Value.DayOfWeek;
                    }


                    foreach (var item in General.GetQueryable<PeakTimeSetting>(null))
                    {

                        int fromDay = item.FromDay.ToInt();
                        int tillDay = item.ToDay.ToInt();

                        TimeSpan fromTime = new TimeSpan(item.FromTime.Value.Hour, item.FromTime.Value.Minute, 0);
                        TimeSpan toTime = new TimeSpan(item.ToTill.Value.Hour, item.ToTill.Value.Minute, 0);

                        if (peakFares == 0 && fromDay <= pickupDay && (tillDay >= pickupDay || fromDay > tillDay))
                        {
                            if (
                                (
                                ((fromTime.Hours > 12 && toTime.Hours < 12) &&
                                 ((fromTime >= pickupTime && pickupTime <= toTime)
                                     || (fromTime <= pickupTime && pickupTime >= toTime)))

                                    ||
                                ((toTime.Hours > fromTime.Hours) &&
                                 (pickupTime >= fromTime && pickupTime <= toTime))


                                     )
                         
                                && (item.ZoneId == fromZoneId || item.ZoneId == null || item.ZoneId == 0))
                            {


                                if (item.ZoneId.ToInt() != 0)
                                {

                                    if (fromZoneId != 0 && item.ZoneId.ToInt() == fromZoneId)
                                    {

                                        if (item.IsAmountWise.ToBool() == false)
                                        {

                                            peakFares = ((fareVal * item.IncrementPercent.ToDecimal()) / 100);
                                        }
                                        else
                                        {

                                            peakFares = item.Amount.ToDecimal();
                                        }

                                    }
                                }
                                else
                                {
                                    if (item.IsAmountWise.ToBool() == false)
                                    {

                                        peakFares = ((fareVal * item.IncrementPercent.ToDecimal()) / 100);
                                    }
                                    else
                                    {
                                        peakFares = item.Amount.ToDecimal();

                                    }
                                }
                            }
                        }


                        if (returnpickupdateTime != null)
                        {

                            if (rtnPeakFares == 0 && fromDay <= returnpickupDay && (tillDay >= returnpickupDay || fromDay > tillDay))
                            {
                                if (

                                      (
                                        ((fromTime.Hours > 12 && toTime.Hours < 12) &&
                                         ((fromTime >= returnpickupTime && returnpickupTime <= toTime)
                                             || (fromTime <= returnpickupTime && returnpickupTime >= toTime)))

                                            ||
                                        ((toTime.Hours > fromTime.Hours) &&
                                         (returnpickupTime >= fromTime && returnpickupTime <= toTime))


                                       )

                                    && (item.ZoneId == toZoneId || item.ZoneId == null || item.ZoneId == 0)

                                    )
                                {


                                    if (item.ZoneId.ToInt() != 0)
                                    {
                                        if (fromZoneId != 0 && item.ZoneId.ToInt() == toZoneId)
                                        {
                                            if (item.IsAmountWise.ToBool() == false)
                                            {
                                                rtnPeakFares = ((returnFares * item.IncrementPercent.ToDecimal()) / 100);
                                            }
                                            else
                                            {

                                                rtnPeakFares = item.Amount.ToDecimal();
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (item.IsAmountWise.ToBool() == false)
                                        {
                                            rtnPeakFares = ((returnFares * item.IncrementPercent.ToDecimal()) / 100);
                                        }
                                        else
                                        {
                                            rtnPeakFares = item.Amount.ToDecimal();
                                        }
                                        //  returnFares += rtnPeakFares;

                                    }

                                }
                            }

                        }


                     
                    }
                }


                fareVal += peakFares;
                returnFares += rtnPeakFares;

                if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                {

                    fareVal = Math.Round(fareVal, 1);
                    returnFares = Math.Round(returnFares, 1);
                    // fareVal = (Math.Round((fareVal * 2), MidpointRounding.AwayFromZero)) / 2;
                    // returnFares = (Math.Round((returnFares * 2), MidpointRounding.AwayFromZero)) / 2;
                }


                if (IsMoreFareWise)
                {

                    decimal AddedAmount = 0.00m;
                    decimal returnAddedAmount = 0.00m;
                    string op = string.Empty;


                    Gen_SysPolicy_FaresSetting objFare = General.GetObject<Gen_SysPolicy_FaresSetting>(c => c.VehicleTypeId == ddlVehicleType.SelectedValue.ToInt());

                    if (objFare != null)
                    {
                        op = objFare.Operator.ToStr();


                        if (objFare.IsAmountWise == false)
                        {
                            AddedAmount = (fareVal * objFare.Percentage.ToDecimal()) / 100;
                            returnAddedAmount = (returnFares * objFare.Percentage.ToDecimal()) / 100;
                        }
                        else
                        {
                            AddedAmount = objFare.Amount.ToDecimal();
                            returnAddedAmount = (returnFares * objFare.Percentage.ToDecimal()) / 100;

                        }
                    }

                    switch (op)
                    {
                        case "+":
                            fareVal = fareVal + AddedAmount;
                            returnFares = returnFares + returnAddedAmount;

                            break;

                        case "-":
                            fareVal = fareVal - AddedAmount;
                            returnFares = returnFares + returnAddedAmount;
                            break;

                        default:
                            fareVal = fareVal + AddedAmount;
                            returnFares = returnFares + returnAddedAmount;
                            break;
                    }
                }

                decimal dd = fareVal.ToDecimal();


                decimal airportPickupChrgs = AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();
                /// Add Airport Pickup Charges If Pickup Point is From Airport...
                int? fromLocationId = ddlFromLocation.SelectedValue.ToIntorNull();



                if (AppVars.objPolicyConfiguration.HasAirportDropOffCharges.ToBool())
                {

                    if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        int? toLocId = ddlToLocation.SelectedValue.ToIntorNull();

                        numExtraChrgs.Value = General.GetObject<Gen_SysPolicy_AirportDropOffCharge>(c => c.AirportId == toLocId).DefaultIfEmpty().Charges.ToDecimal();

                    }
                }
                else
                {


                    if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        if (AppVars.objPolicyConfiguration.HasMultipleAirportPickupCharges.ToBool() && fromLocationId != null)
                        {
                            airportPickupChrgs = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == fromLocationId).DefaultIfEmpty().Charges.ToDecimal();
                            dd += airportPickupChrgs;
                        }
                        else
                        {
                            dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();
                        }
                    }
                }






                numFareRate.Value = dd;

                numCustomerFares.Value = numFareRate.Value;

                int companyId = ddlCompany.SelectedValue.ToInt();
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

                    numReturnFare.Value = returnFares;

                    if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                        numReturnFare.Value -= airportPickupChrgs;


                    else if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                        numReturnFare.Value += airportPickupChrgs;


                }
                else if (opt_WaitandReturn.ToggleState == ToggleState.On)
                {
                    numFareRate.Value = numFareRate.Value * 2;
                }
                else
                {
                    if (numReturnFare != null)
                        numReturnFare.Value = 0;
                }

            }
            catch (Exception ex)
            {


            }


        }

        void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {

                numTotalLuggages.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                FocusOnMobNo();

            }
            else if (e.KeyCode == Keys.Down)
            {
                FocusOnPickupDate();

            }

        }

        void TextBoxFromAddressElement_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Up)
            //{
            //    if (txtToAddress.SelectionStart <= 18)
            //        SendKeys.Send("{TAB}");
            //}
            if (e.KeyCode == Keys.Down)
            {


                if (txtFromAddress.SelectionStart + 18 > txtToAddress.TextLength && txtFromAddress.ListBoxElement.Visible == false)
                {

                    //   txtSpecialRequirements.Focus();
                    // // if (txtToAddress.Text.Length == 0)
                    ////  {

                    //  //    FocusOnCustomer();

                    //  //    SendKeys.Send("{TAB}");

                    SendKeys.Send("{Enter}");
                    SendKeys.Send("{Enter}");
                    //  SendKeys.Send("{Enter}");

                    SendKeys.Send("{Down}");
                    SendKeys.Send("{Down}");

                }
            }


        }

        void TextBoxElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (txtToAddress.SelectionStart <= 18 && txtToAddress.ListBoxElement.Visible == false)
                    SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Down)
            {


                if (txtToAddress.SelectionStart + 18 > txtToAddress.TextLength && txtToAddress.ListBoxElement.Visible == false)
                {
                    // // if (txtToAddress.Text.Length == 0)
                    ////  {

                    //  //    FocusOnCustomer();

                    //  //    SendKeys.Send("{TAB}");

                    SendKeys.Send("{Enter}");
                    SendKeys.Send("{Enter}");
                }
            }


        }

        void chkSecondaryPaymentType_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
                numCashPaymentFares.Enabled = true;
            else
            {
                numCashPaymentFares.Value = 0.00m;
                numCashPaymentFares.Enabled = false;
            }
        }



        void dtpPickupDate_Leave(object sender, EventArgs e)
        {
            dtpPickupDate.Tag = null;
            SetReturnPickupDate();


            UpdateAutoCalculateFares();
        }

        private void SetReturnPickupDate()
        {
            try
            {
                if (opt_JReturnWay.ToggleState == ToggleState.On && dtpReturnPickupDate != null && dtpPickupDate.DateTimePickerElement.Value.ToDate() > dtpReturnPickupDate.Value.ToDate())
                {
                    dtpReturnPickupDate.Value = dtpPickupDate.DateTimePickerElement.Value.ToDate();

                }
            }
            catch 
            {

            }


        }

        void ddlBookingType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            SetBookingTypeDetails(ddlBookingType.SelectedValue.ToInt());




        }


        private void SetBookingTypeDetails(int bookingtypeId)
        {
            if (bookingtypeId == Enums.BOOKING_TYPES.SHUTTLE)
            {

                InitializeGroupJobPanel();



                if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT || ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.HOTELS)
                {
                    ShowGroupJobPanel(true, ddlToLocType.SelectedValue.ToInt());

                }
                else
                {
                    ShowGroupJobPanel(false, ddlToLocType.SelectedValue.ToInt());

                }

                SetPaymentFooterLabel("Payment && Charges Details");
            }
            //else if (bookingtypeId == Enums.BOOKING_TYPES.COURIER)
            //{

            //    InitializeCourierItemsPanel();
            //    SetPaymentFooterLabel("Items Details :");
            //    SetDefaultSettings();
            //}
            else
            {
                ShowGroupJobPanel(false, ddlToLocType.SelectedValue.ToInt());
                SetPaymentFooterLabel("Payment && Charges Details");
                SetDefaultSettings();
            }
        }


        private void SetDefaultSettings()
        {

            if (!dtpPickupDate.Enabled)
            {

                dtpPickupDate.Enabled = true;

                lblPickupTime.Location = new Point(594, 335);
                dtpPickupTime.Location = new Point(641, 334);
                lblPickupTime.Font = new Font("Tahoma", 11, FontStyle.Bold);
                dtpPickupTime.Font = new Font("Tahoma", 11, FontStyle.Bold);

                lblPickupTime.Text = "Time";

                btnSelectVia.Location = new Point(800, 129);
                btn_notes.Location = new Point(749, 282);

            }
        }

        private void SetPaymentFooterLabel(string heading)
        {
            try
            {
                lblPaymentHeading.Text = heading;


                if (heading == "Payment && Charges Details")
                {
                    pnlPaymentMode.Visible = true;
                    pnlOtherCharges.Visible = true;
                    pnlOtherCharges.BringToFront();



                }
                else
                {
                    pnlOtherCharges.SendToBack();



                    pnlPaymentMode.Visible = false;
                    pnlOtherCharges.Visible = false;
                }

            }
            catch (Exception ex)
            {

            }
        }






        private void ShowGroupJobRoomAndFlightDetails(bool canShow, int locTypeId)
        {

            if (this.dtpFlightDepDate == null)
                return;


            this.lblRoomNo.Visible = false;
            this.txtRoomNo.Visible = false;

            if (canShow == true)
            {




                if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
                {
                    canShow = true;

                }
                else if (locTypeId == Enums.LOCATION_TYPES.HOTELS)
                {
                    canShow = false;

                    this.lblRoomNo.Visible = true;
                    this.txtRoomNo.Visible = true;
                }

            }



            if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                dtpPickupDate.Enabled = false;
                lblPickupTime.Location = new Point(764, 149);
                dtpPickupTime.Location = new Point(853, 148);
                lblPickupTime.Text = "Pickup Time";
                lblPickupTime.Font = new Font("Tahoma", 10, FontStyle.Bold);
                dtpPickupTime.Font = new Font("Tahoma", 10, FontStyle.Bold);
            }
            else
            {
                dtpPickupDate.Enabled = true;

                lblPickupTime.Location = new Point(594, 335);
                dtpPickupTime.Location = new Point(641, 334);
                lblPickupTime.Font = new Font("Tahoma", 11, FontStyle.Bold);
                dtpPickupTime.Font = new Font("Tahoma", 11, FontStyle.Bold);

                lblPickupTime.Text = "Time";

            }


            this.dtpFlightDepDate.Visible = canShow;
            this.dtpFlightDepTime.Visible = canShow;
            this.dtpFlightDepDate.Value = null;
            this.dtpFlightDepTime.Value = null;
            this.lblFlightDepDateTime.Visible = canShow;
            this.lblFlightDepTime.Visible = canShow;






        }

        private void ShowGroupJobPanel(bool canShow, int locTypeId)
        {

            if (this.btnClearGroup == null)
                return;


            this.btnClearGroup.Visible = canShow;
            this.btnAddGroup.Visible = canShow;

            this.btnViewGroup.Visible = canShow;

            this.lblPickGroup.Visible = canShow;
            this.txtGroupJobNo.Visible = canShow;
            this.txtGroupJobNo.Text = string.Empty;
            this.txtGroupJobNo.Tag = null;



            if (canShow)
            {
                btnSelectVia.Location = new Point(749, 280);
                btn_notes.Location = new Point(btn_notes.Location.X, 350);

            }


            ShowGroupJobRoomAndFlightDetails(canShow, locTypeId);

        }





        private void InitializeGroupJobPanel()
        {
            if (this.btnClearGroup != null)
                return;


            chkReverse.Location = new Point(725, 70);

            pnlAutoDespatch.Visible = false;

            this.btnClearGroup = new Telerik.WinControls.UI.RadButton();
            this.lblPickGroup = new Telerik.WinControls.UI.RadLabel();
            this.txtGroupJobNo = new Telerik.WinControls.UI.RadTextBox();
            this.btnViewGroup = new Telerik.WinControls.UI.RadButton();
            this.btnAddGroup = new Telerik.WinControls.UI.RadButton();
            this.dtpFlightDepDate = new UI.MyDatePicker();
            this.lblFlightDepDateTime = new Telerik.WinControls.UI.RadLabel();
            this.dtpFlightDepTime = new UI.MyDatePicker();
            this.lblFlightDepTime = new Telerik.WinControls.UI.RadLabel();



            this.lblRoomNo = new Telerik.WinControls.UI.RadLabel();
            this.txtRoomNo = new Telerik.WinControls.UI.RadTextBox();


            ((System.ComponentModel.ISupportInitialize)(this.btnClearGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupJobNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFlightDepDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFlightDepDateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFlightDepTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFlightDepTime)).BeginInit();

            ((System.ComponentModel.ISupportInitialize)(this.lblRoomNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoomNo)).BeginInit();




            // 
            // btnClearGroup
            // 
            this.btnClearGroup.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.btnClearGroup.Location = new System.Drawing.Point(888, 215);
            this.btnClearGroup.Name = "btnClearGroup";
            this.btnClearGroup.Size = new System.Drawing.Size(47, 20);
            this.btnClearGroup.TabIndex = 276;
            this.btnClearGroup.Text = "Detach";
            this.btnClearGroup.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClearGroup.Click += new System.EventHandler(this.btnClearGroup_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearGroup.GetChildAt(0))).Text = "Detach";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearGroup.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearGroup.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblPickGroup
            // 
            this.lblPickGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPickGroup.Location = new System.Drawing.Point(705, 186);
            this.lblPickGroup.Name = "lblPickGroup";
            this.lblPickGroup.Size = new System.Drawing.Size(95, 19);
            this.lblPickGroup.TabIndex = 275;
            this.lblPickGroup.Text = "Attached Group #";
            // 
            // txtGroupJobNo
            // 
            this.txtGroupJobNo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtGroupJobNo.Location = new System.Drawing.Point(725, 211);
            this.txtGroupJobNo.MaxLength = 50;
            this.txtGroupJobNo.Name = "txtGroupJobNo";
            this.txtGroupJobNo.Size = new System.Drawing.Size(87, 22);
            this.txtGroupJobNo.TabIndex = 274;
            this.txtGroupJobNo.TabStop = false;
            // 
            // btnViewGroup
            // 
            this.btnViewGroup.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewGroup.Location = new System.Drawing.Point(814, 215);
            this.btnViewGroup.Name = "btnViewGroup";
            this.btnViewGroup.Size = new System.Drawing.Size(68, 20);
            this.btnViewGroup.TabIndex = 273;
            this.btnViewGroup.Text = "Attach";
            this.btnViewGroup.Click += new System.EventHandler(this.btnViewGroup_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewGroup.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewGroup.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewGroup.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewGroup.GetChildAt(0))).Text = "Attach";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewGroup.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewGroup.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //  this.btnAddGroup.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAddGroup.Location = new System.Drawing.Point(833, 184);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(111, 28);
            this.btnAddGroup.TabIndex = 271;
            this.btnAddGroup.Text = "Create new Group";
            this.btnAddGroup.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddGroup.GetChildAt(0))).Text = "Create new Group";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddGroup.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddGroup.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpFlightDepDate
            // 
            this.dtpFlightDepDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFlightDepDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFlightDepDate.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpFlightDepDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFlightDepDate.Location = new System.Drawing.Point(708, 124);
            this.dtpFlightDepDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFlightDepDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFlightDepDate.Name = "dtpFlightDepDate";
            this.dtpFlightDepDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFlightDepDate.Size = new System.Drawing.Size(102, 22);
            this.dtpFlightDepDate.TabIndex = 267;
            this.dtpFlightDepDate.TabStop = false;
            this.dtpFlightDepDate.Text = "myDatePicker1";
            this.dtpFlightDepDate.Value = null;
            // 
            // lblFlightDepDateTime
            // 
            this.lblFlightDepDateTime.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblFlightDepDateTime.ForeColor = System.Drawing.Color.Red;
            this.lblFlightDepDateTime.Location = new System.Drawing.Point(709, 98);
            this.lblFlightDepDateTime.Name = "lblFlightDepDateTime";
            // 
            // 
            // 
            this.lblFlightDepDateTime.RootElement.ForeColor = System.Drawing.Color.Red;
            this.lblFlightDepDateTime.Size = new System.Drawing.Size(199, 20);
            this.lblFlightDepDateTime.TabIndex = 269;
            this.lblFlightDepDateTime.Text = "Flight Departure Date/Time";
            // 
            // dtpFlightDepTime
            // 
            this.dtpFlightDepTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFlightDepTime.CustomFormat = "HH:mm";
            this.dtpFlightDepTime.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dtpFlightDepTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFlightDepTime.Location = new System.Drawing.Point(854, 123);
            this.dtpFlightDepTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFlightDepTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFlightDepTime.Name = "dtpFlightDepTime";
            this.dtpFlightDepTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFlightDepTime.ShowUpDown = true;
            this.dtpFlightDepTime.Size = new System.Drawing.Size(65, 22);
            this.dtpFlightDepTime.TabIndex = 268;
            this.dtpFlightDepTime.TabStop = false;
            this.dtpFlightDepTime.Text = "myDatePicker1";
            this.dtpFlightDepTime.Value = null;
            // 
            // lblFlightDepTime
            // 
            this.lblFlightDepTime.BackColor = System.Drawing.Color.Transparent;
            this.lblFlightDepTime.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblFlightDepTime.Location = new System.Drawing.Point(815, 124);
            this.lblFlightDepTime.Name = "lblFlightDepTime";
            this.lblFlightDepTime.Size = new System.Drawing.Size(41, 20);
            this.lblFlightDepTime.TabIndex = 270;
            this.lblFlightDepTime.Text = "Time";



            // 
            // lblRoomNo
            // 
            this.lblRoomNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNo.Location = new System.Drawing.Point(721, 114);
            this.lblRoomNo.Name = "lblRoomNo";
            this.lblRoomNo.Size = new System.Drawing.Size(75, 22);
            this.lblRoomNo.TabIndex = 267;
            this.lblRoomNo.Text = "Room No ";
            this.lblRoomNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRoomNo
            // 
            this.txtRoomNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomNo.Location = new System.Drawing.Point(799, 113);
            this.txtRoomNo.MaxLength = 50;
            this.txtRoomNo.Name = "txtRoomNo";
            this.txtRoomNo.Size = new System.Drawing.Size(115, 24);
            this.txtRoomNo.TabIndex = 266;
            this.txtRoomNo.TabStop = false;


            ((System.ComponentModel.ISupportInitialize)(this.btnClearGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupJobNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFlightDepDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFlightDepDateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFlightDepTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFlightDepTime)).EndInit();


            ((System.ComponentModel.ISupportInitialize)(this.lblRoomNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRoomNo)).EndInit();



            this.pnlMain.Controls.Add(this.btnClearGroup);
            this.pnlMain.Controls.Add(this.lblPickGroup);
            this.pnlMain.Controls.Add(this.txtGroupJobNo);
            this.pnlMain.Controls.Add(this.btnViewGroup);
            this.pnlMain.Controls.Add(this.btnAddGroup);
            this.pnlMain.Controls.Add(this.dtpFlightDepDate);
            this.pnlMain.Controls.Add(this.lblFlightDepDateTime);
            this.pnlMain.Controls.Add(this.dtpFlightDepTime);
            this.pnlMain.Controls.Add(this.lblFlightDepTime);


            this.pnlMain.Controls.Add(this.lblRoomNo);
            this.pnlMain.Controls.Add(this.txtRoomNo);





        }


        void txtFromAddress_Leave(object sender, EventArgs e)
        {

            if (sender is AutoCompleteTextBox)
            {
                if ((sender as AutoCompleteTextBox).Tag.ToStr().Length > 0 && ((sender as AutoCompleteTextBox).Tag.ToStr() != (sender as AutoCompleteTextBox).Text.Trim()) && (sender as AutoCompleteTextBox).Text.Length > 5)
                {
                    //  string tag = (sender as AutoCompleteTextBox).Tag.ToStr();

                    UpdateAutoCalculateFares();
                    (sender as AutoCompleteTextBox).Tag = null;
                }

            }
            else
            {

                UpdateAutoCalculateFares();

            }
        }


      
        private void UpdateAutoCalculateFares()
        {

         



            if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool() == false)
            {

                //if (Debugger.IsAttached)
                //{
                //    AppVars.objPolicyConfiguration.AutoCalculateFares = true;

                //    return;
                //}
                //else
                    return;


            }

            if (IsDisplayingRecord || (ddlCompany != null && ddlCompany.Tag.ToBool()))
                return;

            try
            {


                int fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
                int toLocTypeId = ddlToLocType.SelectedValue.ToInt();
                bool isOk = false;

                if (((fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.BASE)
                    && !string.IsNullOrEmpty(txtFromAddress.Text.Trim()))

                    &&

                    ((toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
                    && !string.IsNullOrEmpty(txtToAddress.Text.Trim())))
                {

                    isOk = true;
                }

                else if (((fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.BASE)
                && !string.IsNullOrEmpty(txtFromAddress.Text.Trim()))

                &&

                (toLocTypeId != Enums.LOCATION_TYPES.ADDRESS
                && toLocTypeId != Enums.LOCATION_TYPES.BASE
                && ddlToLocation.SelectedValue != null))
                {

                    isOk = true;
                }


                else if ((fromLocTypeId != Enums.LOCATION_TYPES.ADDRESS && fromLocTypeId != Enums.LOCATION_TYPES.BASE && ddlFromLocation.SelectedValue != null)

                &&

                ((toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
                && !string.IsNullOrEmpty(txtToAddress.Text.Trim())))
                {

                    isOk = true;
                }

                else if ((fromLocTypeId != Enums.LOCATION_TYPES.ADDRESS
                && fromLocTypeId != Enums.LOCATION_TYPES.BASE
                && ddlFromLocation.SelectedValue != null)

                &&

                (toLocTypeId != Enums.LOCATION_TYPES.ADDRESS
                && toLocTypeId != Enums.LOCATION_TYPES.BASE
                && ddlToLocation.SelectedValue != null))
                {

                    isOk = true;
                }


                if (isOk)
                {

                    if (AppVars.keyLocations.Count(c => c.ToStr().Trim().ToLower() == txtFromAddress.Text.Trim().ToLower()) == 0
                        &&
                        AppVars.keyLocations.Count(c => c.ToStr().Trim().ToLower() == txtToAddress.Text.Trim().ToLower()) == 0
                        )
                    {


                        if ( ddlToLocType.SelectedValue.ToInt()==Enums.LOCATION_TYPES.ADDRESS && txtToAddress.Text.ToStr().Trim().ToLower() == "as directed"
                            && numJourneyTime!=null)
                        {

                            GetFareByJourneyTime(numJourneyTime.Value);
                        }
                        else
                        {

                            th = new System.Threading.Thread(new ThreadStart(CalculateAutoFareUI));
                          //  th.IsBackground = true;
                            th.Priority = ThreadPriority.Highest;
                            th.Start();

                        }
                    }
                    //object o = "AutoCalculateTotalFares";
                    //SendAsyncRequest(o);



                }




            }
            catch (Exception ex)
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

        void ddlPlot_TextChanged(object sender, EventArgs e)
        {

            ComboBox cboPlot = (ComboBox)sender;

            if (cboPlot.Text.Length > 0 && cboPlot.Text[0].ToStr().IsAlpha())
            {
                try
                {

                    Object item = cboPlot.Items.Cast<Object>().FirstOrDefault(c => c.ToStr().ToUpper().Substring(c.ToStr().IndexOf('.') + 1).Trim().StartsWith(cboPlot.Text.ToUpper()));

                    if (item != null)
                    {

                        string oldText = cboPlot.Text[0].ToStr();

                        cboPlot.BeginUpdate();

                        cboPlot.Text = item.ToStr().Substring((item.ToStr().IndexOf("ZoneName = ") + 10)).Trim().Replace("}", "").Trim().ToStr();
                        cboPlot.SelectedItem = item;
                        cboPlot.SelectionStart = cboPlot.Text.IndexOf('.') + 1;
                        cboPlot.SelectionLength = cboPlot.Text.Length + (cboPlot.Text.IndexOf('.') - 1);

                        cboPlot.EndUpdate();
                        cboPlot.Refresh();

                    }
                }
                catch
                {

                }

            }

        }


        private void ddlPickupPlot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                    FocusOnToAddress();
                else if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    FocusOnToPostCode();
                else
                    FocusOnToLocation();

            }
        }

        private void ddlDropOffPlot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnCustomer();

            }
        }

        void ddlDriver_GotFocus(object sender, EventArgs e)
        {
            FillDriversCombo();
        }

        private void FillDriversCombo()
        {
            if (ddlDriver.DataSource == null)
            {

                ComboFunctions.FillDriverNoQueueCombo(ddlDriver);


            }
        }


        void dtpPickupTime_Leave(object sender, EventArgs e)
        {
            dtpPickupTime.Tag = null;
            i = 0;

            UpdateAutoCalculateFares();
        }


        private string[] priorityPostCodes = null;

        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
            }
            else
            {

                if (AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Length == 0)
                {

                    e.Graphics.FillRectangle(Brushes.White, e.Bounds);

                }

                else
                {


                    if (priorityPostCodes == null)
                    {
                        priorityPostCodes = AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Split(new char[] { ',' });
                    }


                    if (AppVars.zonesList.Count(c => ((sender as ListBox).Items[e.Index].ToString()).Contains(c)) > 0)
                    {

                        if (priorityPostCodes != null && priorityPostCodes.Count(c => GeneralBLL.GetHalfPostCodeMatch((sender as ListBox).Items[e.Index].ToString()) == c) > 0)
                        {
                            e.Graphics.FillRectangle(Brushes.White, e.Bounds);


                        }
                        else
                            e.Graphics.FillRectangle(Brushes.LightPink, e.Bounds);


                    }


                    else
                    {
                        e.Graphics.FillRectangle(Brushes.Red, e.Bounds);
                    }
                }
            }

            // Draw a rectangle in blue around each item.
            e.Graphics.DrawRectangle(Pens.Blue, e.Bounds);

            // Draw the text in the item.
            e.Graphics.DrawString((sender as ListBox).Items[e.Index].ToString(),
                e.Font, Brushes.Black, e.Bounds.X, e.Bounds.Y);

            // Draw the focus rectangle around the selected item.
            e.DrawFocusRectangle();
        }




        private void LoadPostCodes()
        {


            //try
            //{

            //    //if (txtFromPostCode.AutoCompleteCustomSource.Count > 0 && txtToPostCode.AutoCompleteCustomSource.Count > 0
            //    //    &&  (txtviaPostCode==null || txtviaPostCode.AutoCompleteCustomSource.Count > 0))
            //    //    return;




            //    //var postcodes = (from a in AppVars.listOfAddress

            //    //                 select a.PostalCode
            //    //                      ).Distinct().ToArray<string>();


            //    //txtFromPostCode.AutoCompleteCustomSource.Clear();
            //    //txtFromPostCode.AutoCompleteCustomSource.AddRange(postcodes);

            //    //txtToPostCode.AutoCompleteCustomSource.Clear();
            //    //txtToPostCode.AutoCompleteCustomSource.AddRange(postcodes);
            //}
            //catch (Exception ex)
            //{


            //}
        }




        private void SetDefaultCommission()
        {

            numDriverCommission.Value = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal();
            ddlCommissionType.SelectedValue = "Percent";
        }

        string[] res = null;
        string searchTxt = "";
      //  bool IsPOI = false;


        private void InitializeTimer()
        {
            if (this.timer1 == null)
            {
                this.timer1 = new System.Windows.Forms.Timer();
                this.timer1.Tick += timer1_Tick;
                this.timer1.Interval = 500;
            }

        }



    



        private bool IsOnClosed = false;

        void frmBooking_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                IsOnClosed = true;
                //   CancelPing();
                this.WindowState = FormWindowState.Minimized;

                if (saved && (frm == null || (frm != null && frm.SuccessDespatched == false)))
                {

                    if ((objMaster.PrimaryKeyValue == null)
                        || (objMaster.PrimaryKeyValue != null && objMaster.Current != null && objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.WAITING))
                    {



                        if (objMaster.Current.IsQuotation.ToBool() || WasQuotiation)
                        {
                            new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ONLY_DASHBOARD);

                        }
                        else
                        {

                            int days = AppVars.objPolicyConfiguration.DaysInTodayBooking.ToInt() / 24;

                            if (objMaster.Current != null && objMaster.Current.PickupDateTime <= DateTime.Now.AddDays(days))
                            {

                                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ACTIVEBOOKINGS_DASHBOARD);
                            }
                            else
                            {

                                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_REQUIRED_DASHBOARD);
                            }

                        }


                    }
                    else
                    {

                        if (this.openedFrom == 0)
                        {

                            new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);
                        }
                        else if (this.openedFrom == 1)
                        {
                            if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.REJECTED)
                            {
                                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DECLINEDWEBBOOKINGS_DASHBOARD);
                            }
                            else
                            {

                                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).PopulateWebBookingsGrid();

                                //  new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_WEBBOOKINGS_DASHBOARD);
                            }

                        }
                        else if (this.openedFrom == 2)
                        {
                            new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_BOOKINGHISTORY_DASHBOARD);

                        }


                    }

                }

                if (frm != null)
                {
                    if (frm.SmsThread != null)
                        frm.SmsThread.Start();


                    frm.Dispose();
                }

                DisposeGDIObjects();



                this.Dispose(true);


                GC.Collect();


                //General.DisposeForm(this);
            }
            catch (Exception ex)
            {


            }

        }




        private void DisposeGDIObjects()
        {

            //    this.Font.Dispose();

            try
            {
                ddlSubCompany.Dispose();
                btnPasteBooking.Dispose();
                btnDespatchView.Dispose();

               // lblBookingType.Font.Dispose();
                lblBookingType.Dispose();
                ddlBookingType.Dispose();
               // label1.Controls.Clear();
                label1.Dispose();
               
              
                txtBookingNo.Font.Dispose();
                txtCustomerMobileNo.Font.Dispose();
                txtCustomerPhoneNo.Font.Dispose();

                txtFromAddress.Font.Dispose();
                txtFromAddress.ListBoxElement.Font.Dispose();
                txtToAddress.ListBoxElement.Font.Dispose();
                txtFromAddress.ListBoxElement.Items.Clear();
                txtFromAddress.ListBoxElement.Dispose();
                txtToAddress.ListBoxElement.Items.Clear();
                txtToAddress.ListBoxElement.Dispose();

                txtToAddress.Font.Dispose();
                ddlFromLocation.Font.Dispose();
                ddlToLocation.Font.Dispose();
              
                lblFromLoc.Font.Dispose();
                lblFromStreetComing.Font.Dispose();

                if (lblDepartment != null)
                    lblDepartment.Font.Dispose();

                if (lblBookedBy != null)
                {
                    lblBookedBy.Font.Dispose();
                }

                lblMap.Font.Dispose();
                txtSpecialRequirements.Font.Dispose();
              
                btnSaveNew.Image.Dispose();
                btnPrintJob.Image.Dispose();
                btnExit.Image.Dispose();
                btnExitForm.Image.Dispose();

                btnDetailMap.Image.Dispose();
                btnHospital.Dispose();
                btnAirport.Dispose();
                btnStations.Dispose();

                pnlMain.Dispose();


        


                if (webBrowser1 != null)
                {
                    webBrowser1.Stop();

                    webBrowser1.Dispose();
                }





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
            string type = ddlHospitalType.Text.ToLower().Trim();
            if (type == "from")
            {
                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.HOSPITAL;
                ddlFromLocation.SelectedValue = ddlHospitals.SelectedValue;
                txtToAddress.Focus();
            }
            else if (type == "to")
            {
                ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.HOSPITAL;
                ddlToLocation.SelectedValue = ddlHospitals.SelectedValue;
                ddlCustomerName.Focus();
            }

            HideHospitals();


            UpdateAutoCalculateFares();

        }

        void btn_PickStations_Click(object sender, EventArgs e)
        {
            PickStation();
        }

        private void PickStation()
        {

            string type = ddlStationType.Text.ToLower().Trim();
            if (type == "from")
            {
                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;
                ddlFromLocation.SelectedValue = ddlStations.SelectedValue;

                txtToAddress.Focus();
            }
            else if (type == "to")
            {
                ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.UNDERGROUNDSTATION;
                ddlToLocation.SelectedValue = ddlStations.SelectedValue;
                ddlCustomerName.Focus();
            }



            HideStations();

            UpdateAutoCalculateFares();

        }

        void btnAirport_Pick_Click(object sender, EventArgs e)
        {
            PickAirport();
        }

        private void PickAirport()
        {

            string type = ddlAirportType.Text.ToLower().Trim();
            if (type == "from")
            {
                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.AIRPORT;
                ddlFromLocation.SelectedValue = ddlAirPorts.SelectedValue;

                txtToAddress.Focus();
            }
            else if (type == "to")
            {
                ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.AIRPORT;
                ddlToLocation.SelectedValue = ddlAirPorts.SelectedValue;
                ddlCustomerName.Focus();
            }

            HideAirports();

            UpdateAutoCalculateFares();

        }








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
            if (e.Result == null || e.Cancelled ||  (sender as BackgroundWorker)==null  )
                return;

            try
            {


                ShowAddressesPOI((string[]) e.Result);

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
               if (POIWorker == null || IsOnClosed)
               {
                   e.Cancel = true;
                   return;


               }
              
          

               string postCode = General.GetPostCodeMatchOpt(searchValue);

                string doorNo = string.Empty;
                string place = string.Empty;


               

                if (postCode.Length==0 && searchValue.Trim().Contains(" ") && searchValue.Trim().Contains(".")==false && searchValue.Trim().Contains("#")==false
                  && searchValue[0].ToStr().IsAlpha()     && searchValue.Split(new char[] { ' ' }).Any(c=>c.IsAlpha()==false))
                //    && (searchValue.Trim().Substring(0, searchValue.Trim().IndexOf(' ')).ToStr().IsAlpha() == false || searchValue.Trim().Substring(searchValue.Trim().IndexOf(' ') + 1)[0].ToStr().IsAlpha()))
                {
                    var arrData = searchValue.Split(new char[] { ' ' });

                   

                        if (arrData.Count() == 2)
                        {
                            postCode = General.GetPostCodeMatchOpt(arrData.FirstOrDefault(c=>c.IsAlpha()==false));

                        }
                        else if (arrData.Count() > 2)
                        {
                          
                            if(arrData[1][0].ToStr().IsNumeric())
                                postCode = General.GetPostCodeMatchOpt((arrData.FirstOrDefault(c => c.IsAlpha() == false) + " " + arrData[1]).Trim());
                            else if(arrData[1].ToStr().IsAlpha()==false && arrData[2].ToStr().IsAlpha()==false)
                                postCode = General.GetPostCodeMatchOpt((arrData.FirstOrDefault(c => c.IsAlpha() == false) + " " + arrData[2]).Trim());
                            else
                                postCode = General.GetPostCodeMatchOpt(arrData.FirstOrDefault(c => c.IsAlpha() == false));
                        }
                    

                }

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

                if (doorNo.Length > 0 && place.Length==0)
                {
                    street = street.Replace(doorNo, "").Trim();


                }


                if (postCode.Length==0 && street.Length < 3)
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


                    if (POIWorker == null ||  POIWorker.CancellationPending ||  ((sender as BackgroundWorker)==null || (sender as BackgroundWorker).CancellationPending))
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


            if (AppVars.objPolicyConfiguration.EnableReplaceNoToZoneSuggesstion.ToBool() && text.Length <= 3 && text.Length > 0 && text.EndsWith("."))
            {
                string itemFound = string.Empty;

                foreach (object item in ddlPickupPlot.Items)
                {

                    if (item.ToString().Contains(text))
                    {

                        itemFound = item.ToString().Substring(item.ToString().IndexOf("ZoneName =")).ToStr().Replace("ZoneName =", "").Trim().Replace("}", "").Trim().ToStr();
                        if (itemFound.StartsWith(text))
                        {
                            itemFound = itemFound.Replace(text, "").Trim();

                            break;

                        }


                    }

                    //  var itemFound = ddlPickupPlot.Items.OfType<Gen_Zone>().FirstOrDefault(c => c.ZoneName.StartsWith(text));
                }

                if (!string.IsNullOrEmpty(itemFound.ToStr().Trim()))
                {
                    aTxt.Text = itemFound;

                    return;
                }


            }


            if (text.Length > 1 && text != "BASX")
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
                    Gen_Location loc = null;
                    if (AppVars.keyLocations.Contains(formerValue) || aTxt.FormerValue.EndsWith("  ")
                    || (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <= 3 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                    {
                        //if (AppVars.keyLocations.Contains(formerValue) || aTxt.FormerValue.EndsWith("  ")
                        // ||   (aTxt.FormerValue.Length < 13 && aTxt.FormerValue.WordCount() == 2 && aTxt.FormerValue.Remove(aTxt.FormerValue.IndexOf(' ')).Trim().Length <=2 && aTxt.FormerValue.Strip(' ').IsAlpha()))
                        //{



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
                                if (commaIndex > 0 && ddlFromLocation.Text.ToUpper() != item.Text.ToUpper())
                                {

                                    SetPickupZone(item.Text);

                                }


                                if (loc != null && loc.ZoneId != null && ddlPickupPlot.SelectedValue == null)
                                {
                                    ddlPickupPlot.SelectedValue = loc.ZoneId;
                                }




                                if (ddlFromLocation.SelectedValue != null && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                                {
                                    UpdateAutoCalculateFares();

                                }

                            }
                            else
                            {
                                if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                                {
                                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                    aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                    aTxt.Text = aTxt.Text.Trim();
                                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                                    if (aTxt.Name == "txtFromAddress")
                                    {
                                        SetPickupZone(aTxt.Text);

                                        UpdateAutoCalculateFares();
                                    }


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

                                if (commaIndex > 0 && ddlToLocation.Text.ToUpper() != item.Text.ToUpper())
                                {
                                    SetDropOffZone(item.Text);

                                }

                                if (loc != null && loc.ZoneId != null && ddlDropOffPlot.SelectedValue == null)
                                {
                                    ddlDropOffPlot.SelectedValue = loc.ZoneId;
                                }

                                if (ddlToLocation.SelectedValue != null && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                                {
                                    UpdateAutoCalculateFares();

                                }

                            }
                            else
                            {
                                if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                                {
                                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                    aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                    aTxt.Text = aTxt.Text.Trim();
                                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                                    SetDropOffZone(aTxt.Text);
                                    UpdateAutoCalculateFares();


                                }

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

                        if (aTxt.SelectedItem.ToStr().Trim() != string.Empty)
                        {
                            UpdateAutoCalculateFares();

                        }


                    }

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

                                SetPickupZone(aTxt.Text);
                                txtFromFlightDoorNo.Focus();

                            }

                            else if (aTxt.Name == "txtToAddress")
                            {
                                SetDropOffZone(aTxt.Text);
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


        #region ROAD LEVEL DATA ADDRESS SEARCHING

        


        private void AddressTextChangePOI()
        {
            string text = aTxt.Text;
            string doorNo = string.Empty;


            //if (AppVars.objPolicyConfiguration.StripDoorNoOnAddress.ToBool())
            //{
            //    if (aTxt.SelectedItem != null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower())
            //    {
            //        aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            //        aTxt.Text = aTxt.ListBoxElement.SelectedItem.ToStr().Trim().ToUpper().Trim();


            //        if (aTxt.Text.Contains(".") && aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".") - 1]))
            //        {

            //            aTxt.Text = aTxt.Text.Remove(0, aTxt.Text.IndexOf('.') + 1).Trim();
            //        }

            //        aTxt.SelectedItem = aTxt.Text.Trim();
            //        aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            //        //    }               

            //    }

            //}

            //if (text.Length > 2 && text.EndsWith(".") == false && text.EndsWith(",") == false)
            //{

            //    if (aTxt.SelectedItem == null || (aTxt.SelectedItem != null && aTxt.SelectedItem.ToLower() != aTxt.Text.ToLower()))
            //    {


            //        for (int i = 0; i <= 2; i++)
            //        {
            //            if (char.IsNumber(text[i]))
            //                doorNo += text[i];
            //            else
            //                break;

            //        }
            //        text = text.Remove(text.IndexOf(doorNo), doorNo.Length).TrimStart(new char[] { ' ' });
            //    }
            //}


            if ( aTxt.SelectedItem != null && aTxt.SelectedItem.ToStr().ToLower() == aTxt.Text.ToLower()
               && aTxt.Text.Length > 0)
               //&& aTxt.Text[0].ToStr().IsNumeric() )
            {
                aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
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
                                if (commaIndex > 0 && ddlFromLocation.Text.ToUpper() != item.Text.ToUpper())
                                {
                                   SetPickupZone(item.Text);
                                }


                                if (loc != null && loc.ZoneId != null && ddlPickupPlot.SelectedValue == null)
                                {
                                    ddlPickupPlot.SelectedValue = loc.ZoneId;
                                }

                                if (ddlFromLocation.SelectedValue != null && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                                {
                                    UpdateAutoCalculateFares();

                                }
                            }
                            else
                            {
                                if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                                {
                                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                    aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                    aTxt.Text = aTxt.Text.Trim();
                                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                                    if (aTxt.Name == "txtFromAddress")
                                    {
                                        SetPickupZone(aTxt.Text);

                                        UpdateAutoCalculateFares();
                                    }
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

                                if (commaIndex > 0 && ddlToLocation.Text.ToUpper() != item.Text.ToUpper())
                                {
                                    SetDropOffZone(item.Text);
                                }

                                if (loc != null && loc.ZoneId != null && ddlDropOffPlot.SelectedValue == null)
                                {
                                    ddlDropOffPlot.SelectedValue = loc.ZoneId;
                                }

                                if (ddlToLocation.SelectedValue != null && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                                {
                                    UpdateAutoCalculateFares();
                                }
                            }
                            else
                            {
                                if (loctypeId == Enums.LOCATION_TYPES.ADDRESS && aTxt.SelectedItem.ToStr().Length > 0)
                                {
                                    aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                                    aTxt.Text = doorNo + " " + aTxt.SelectedItem.ToStr().Trim();
                                    aTxt.Text = aTxt.Text.Trim();
                                    aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                                    SetDropOffZone(aTxt.Text);
                                    UpdateAutoCalculateFares();
                                }

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
                        aTxt.Text =aTxt.Text;
                        aTxt.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                    }
                    else
                    {
                        if (aTxt.Name == "txtFromAddress")
                        {
                            SetPickupZone(aTxt.SelectedItem);                         
                        }

                        else if (aTxt.Name == "txtToAddress")
                        {
                            SetDropOffZone(aTxt.SelectedItem);                         

                        }

                        if (aTxt.SelectedItem.ToStr().Trim() != string.Empty)
                        {
                            UpdateAutoCalculateFares();
                        }
                    }

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

                                SetPickupZone(aTxt.Text);
                                txtFromFlightDoorNo.Focus();

                            }

                            else if (aTxt.Name == "txtToAddress")
                            {
                                SetDropOffZone(aTxt.Text);
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



        private void PerformAddressChangeTimerPOI()
        {


          

            //res = (from a in new TaxiDataContext().stp_GetByRoadLevelData(fullPostCode, doorNo, street, place)
            //       select a.AddressLine1).ToArray<string>();


           

        }


        private void ShowAddressesPOI(string[] resValue)
        {
            int sno = 1;

           // var finalList = resValue;

            try
            {


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

                if (finalList.Count() > 1 && AppVars.objPolicyConfiguration.RecentAddressesFrequency.ToInt() > 0)
                {

                    var list = General.GetQueryable<Gen_RecentAddress>(null).OrderByDescending(c => c.SearchedOn).Take(50)
                        .Where(c => c.AddressLine1.Contains(searchTxt) && (ddlCompany.SelectedValue == null || c.CompanyId == ddlCompany.SelectedValue.ToIntorNull()))
                        .Distinct().Select(c => c.AddressLine1).ToArray<string>();

                    if (list.Count() > 0)
                    {


                        try
                        {

                            list = (from a in XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\"?><adds>" + String.Join(" ", list) + "</adds>").Element("adds").Nodes()
                                    where (a as XElement).Value.Contains(searchTxt)
                                    select (a as XElement).Value).Distinct().ToArray<string>();


                            finalList = list.Union(finalList).ToArray<string>();
                        }
                        catch
                        {


                        }
                    }
                }


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
            catch (Exception ex)
            {


            }
        }


        #endregion

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


                        //  string text = aTxt.Text.ToStr().ToUpper();

                        //string door = string.Empty;

                        //for (int i = 0; i <= 2; i++)
                        //{
                        //    if (char.IsNumber(text[i]))
                        //        door += text[i];
                        //    else
                        //        break;

                        //}

                        //string actualPostcode = (door + " " + fullPostCode).Trim();



                        //    AddressFindByTermApi.PostcodeAnywhere_SoapClient a = new Taxi_AppMain.AddressFindByTermApi.PostcodeAnywhere_SoapClient();
                        //    var res2 = a.CapturePlus_Interactive_Find_v2_00("DZ83-FP68-FC49-TD83", actualPostcode, "", "Everything", "GBR", "EN");



                        //    if (res2.Count > 0)
                        //    {


                        //            AddressRetrieveByIdApi.PostcodeAnywhere_SoapClient ssc = new AddressRetrieveByIdApi.PostcodeAnywhere_SoapClient();

                        //            res = new string[res2.Count];



                        //            for (int i = 0; i < res2.Count; i++)
                        //            {

                        //                var addr = res2[i].Text;

                        //                var splitArr = addr.Split(',');
                        //                if (splitArr.Count() == 5)
                        //                {

                        //                    addr = splitArr[2] +" "+ splitArr[3] + " "+ splitArr[4] + " "+ splitArr[0];

                        //                }
                        //                else if (splitArr.Count() == 6)
                        //                {
                        //                    addr = splitArr[2] + " " + splitArr[3] + " " + splitArr[4] + " " + splitArr[5] + " " + splitArr[0];

                        //                }
                        //                else if (splitArr.Count() == 7)
                        //                {
                        //                    addr = splitArr[3] + " " + splitArr[4] + " " + splitArr[5] + " " + splitArr[6] + " " + splitArr[0];

                        //                }
                        //                else if (splitArr.Count() == 4)
                        //                {
                        //                    addr = splitArr[1] + " " + splitArr[2] + " " + splitArr[3] + " " + splitArr[0];

                        //                }



                        //                res[i] = addr.ToUpper().Trim();
                        //              //  var addr = ssc.CapturePlus_Interactive_Retrieve_v2_10("DZ83-FP68-FC49-TD83", res2[i].Id);


                        //               // if (addr != null && addr.Count > 0 && addr[0].PostalCode.ToStr().Length > 0)
                        //               //     res[i] = addr[0].Label.Replace("M I N D\n", "").Trim().Replace("\nUNITED KINGDOM", "").Trim().Replace("\n", " ").Trim().ToUpper();

                        //            }


                        //            res.ToList().RemoveAll(c => c.ToStr().Trim().Length == 0);
                        //       // }
                        //        //  txtResult.Text = fullAddress[0].Label.Replace("M I N D\n", "").Trim().Replace("\nUNITED KINGDOM", "").Trim().Replace("\n", " ").Trim().ToUpper();
                        //    }




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
        

        private void SetPickupZone(string val)
        {

            ddlPickupPlot.SelectedValue = GetZoneId(val.ToStr().ToUpper()).ToInt();

        }

        private void SetDropOffZone(string val)
        {
            ddlDropOffPlot.SelectedValue = GetZoneId(val.ToStr().ToUpper()).ToInt();
        }

        private void RemoveNumbering(string formerVal)
        {

            aTxt.TextChanged -= new EventHandler(TextBoxElement_TextChanged);

            if (aTxt.Text.IndexOf(".") < 3 && aTxt.Text.IndexOf(".") > 0 && char.IsNumber(aTxt.Text[aTxt.Text.IndexOf(".")-1]))
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

        



        private void DisplayBooking_Map()
        {
            try
            {
                string fromAddress = txtFromAddress.Text.ToStr().ToUpper();

                if (objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                {
                    fromAddress = txtFromAddress.Text.ToStr().ToUpper();
                }

                else if (objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                {
                    fromAddress = objMaster.Current.FromPostCode.ToStr().Trim();

                }
                else
                {
                    fromAddress = ddlFromLocation.Text.Trim().ToUpper();

                }

                string[] viaLocs = new string[0];

                if (pnlVia != null)
                {
                    viaLocs = grdVia.Rows.Select(c => c.Cells["VIALOCATIONVALUE"].Value.ToStr()).ToArray<string>();
                }
                string toAddress = txtToAddress.Text.ToStr().ToUpper();

                if (objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
                {
                    toAddress = txtToAddress.Text.ToStr().ToUpper();
                }

                else if (objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                {
                    toAddress = objMaster.Current.ToPostCode.ToStr().Trim();

                }
                else
                {
                    toAddress = ddlToLocation.Text.Trim().ToUpper();

                }


                mileageError = false;
                milesList.Clear();

                if (fromAddress.Length > 0 && toAddress.Length > 0)
                {

                    milesList.Add(CalculateTotalDistance(fromAddress, viaLocs, toAddress));

                    if (this.IsHandleCreated)
                    {


                        DisplayMilesHandler d = new DisplayMilesHandler(ShowMilesFromGoogle);
                        this.BeginInvoke(d);

                    }
                }
            }
            catch (Exception ex)
            {


            }

        }

        private void ShowMilesFromGoogle()
        {

            if (mileageError)
                MileageError();
            else
                lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles";
        }

        List<decimal> milesList = new List<decimal>();


        private bool EnablePOI = false;

        void frmBooking_Load(object sender, EventArgs e)
        {
            try
            {

                if (objMaster.Current == null)
                {
                    ddlBookingType.SelectedValue = this.PickBookingTypeId;

                    PickVehicleDetails();

                    if(this.PickSubCompanyId!=null && this.PickSubCompanyId!=ddlSubCompany.SelectedValue.ToInt())
                         ddlSubCompany.SelectedValue = this.PickSubCompanyId;


                   
                }

                else if (objMaster.Current.DriverId != null)
                    ComboFunctions.FillDriverNoQueueCombo(ddlDriver, objMaster.Current.DriverId, objMaster.Current.Fleet_Driver.DriverNo + " - " + objMaster.Current.Fleet_Driver.DriverName);


            }
            catch 
            {

                
            }


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
            col.Name = "IsUpdated";
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
                this.grdVia.CurrentRow.Cells["IsUpdated"].Value = "1";
            }
            catch
            {


            }
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

        private void AddNew()
        {
            OnNew();
        }


        private void OnNew()
        {
            ddlVehicleType.SelectedValue = AppVars.objPolicyConfiguration.DefaultVehicleTypeId;


            SetCashPaymentType();

            chkQuotedPrice.Visible = false;
            chkQuotation.Visible = AppVars.objPolicyConfiguration.EnableQuotation.ToBool();

            SetJourneyWise(ToggleState.On);
            UseCompanyRates(ToggleState.Off);
            txtBookingNo.Text = "Not Allocated";


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


            if (AppVars.objPolicyConfiguration.AutoCloseDrvPopup.ToBool())
            {

                IsAutoDespatchEnabled(AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool());

                IsBiddingEnabled(AppVars.objPolicyConfiguration.EnableBidding.ToBool());

            }
            else
            {
                chkAutoDespatch.Visible = false;
                chkAutoDespatch.Checked = false;
                pnlAutoDespatch.Height = 118;

                chkBidding.Visible = false;
                chkBidding.Checked = false;


            }

            if (AppVars.objPolicyConfiguration.EnableBabySeats.ToBool())
            {
                InitializeBabySeats();

                if (ddlBabyseat1 != null)
                {

                    ddlBabyseat1.SelectedIndex = 0;
                    ddlbabyseat2.SelectedIndex = 0;
                }
            }



            if (AppVars.objPolicyConfiguration.ShowBlankPickupDateAsDefault.ToBool() == false)
            {

                dtpPickupDate.Value = DateTime.Now.ToDate();
                dtpPickupTime.Value = DateTime.Now;
                dtpPickupDate.Leave += new EventHandler(dtpPickupDate_Leave);
            }




            if (AppVars.objPolicyConfiguration.BookingPaymentDetailsType.ToInt() == 1)
            {
                lblPaymentRef.Visible = true;
                txtPaymentReference.Visible = true;


                //lblCompanyCreditCard.Visible = false;
                //txtCompanyCreditCardNo.Visible = false;

                //lblCustCreditCard.Visible = false;
                //txtCustomerCreditCardNo.Visible = false;

            }

            else if (AppVars.objPolicyConfiguration.BookingPaymentDetailsType.ToInt() == 2)
            {
                lblPaymentRef.Visible = false;
                txtPaymentReference.Visible = false;


                //lblCompanyCreditCard.Visible = true;
                //txtCompanyCreditCardNo.Visible = true;

                //lblCustCreditCard.Visible = true;
                //txtCustomerCreditCardNo.Visible = true;


                //lblCompanyCreditCard.BringToFront();
                //txtCompanyCreditCardNo.BringToFront();

            }
            else if (AppVars.objPolicyConfiguration.BookingPaymentDetailsType.ToInt() == 3)
            {


                lblPaymentRef.Visible = false;
                txtPaymentReference.Visible = false;
                InitializeAgentPanel();
                //lblCompanyCreditCard.Visible = false;
                //txtCompanyCreditCardNo.Visible = false;

                //lblCustCreditCard.Visible = false;
                //txtCustomerCreditCardNo.Visible = false;
                ddlAgentCommissionType.SelectedIndex = 1;
            }



            //if(AppVars.objPolicyConfiguration.FareMeterType!=null)
            //{
            //    chkHasFareMeter.Visible = true;



            //    btnPickFares.Location = new Point(764, btnPickFares.Location.Y);


            //}


          

                chkGenerateToken.Visible = AppVars.objPolicyConfiguration.EnableGhostJob.ToBool();
                //txtTokenNo.Visible = true;
//
          //  }
        }



        private void InitializeBabySeats()
        {
            this.ddlBabyseat1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ddlbabyseat2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();


            this.pnlMain.Controls.Add(this.ddlbabyseat2);
            this.pnlMain.Controls.Add(this.label9);
            this.pnlMain.Controls.Add(this.ddlBabyseat1);
            this.pnlMain.Controls.Add(this.label8);


            // 
            // ddlBabyseat1
            // 
            this.ddlBabyseat1.BackColor = System.Drawing.Color.White;
            this.ddlBabyseat1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlBabyseat1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlBabyseat1.FormattingEnabled = true;


            object[] arr = General.GetQueryable<Gen_BabySeat>(null).OrderBy(c => c.Id).Select(a => a.BabySeatName).ToArray<string>();


            this.ddlBabyseat1.Items.AddRange(arr);

            //this.ddlBabyseat1.Items.AddRange(new object[] {
            //"No child seat required",
            //"Rear-facing infant seat (suitable for babies)",
            //"Forward-facing upring child seat (for toddlers and smaller children)",
            //"Child booster seat"});
            this.ddlBabyseat1.Location = new System.Drawing.Point(132, 436);
            this.ddlBabyseat1.Name = "ddlBabyseat1";
            this.ddlBabyseat1.Size = new System.Drawing.Size(219, 24);
            this.ddlBabyseat1.TabIndex = 271;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 437);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 18);
            this.label8.TabIndex = 270;
            this.label8.Text = "First Child Seat";
            // 
            // ddlbabyseat2
            // 
            this.ddlbabyseat2.BackColor = System.Drawing.Color.White;
            this.ddlbabyseat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlbabyseat2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlbabyseat2.FormattingEnabled = true;
            //this.ddlbabyseat2.Items.AddRange(new object[] {
            //"No child seat required",
            //"Rear-facing infant seat (suitable for babies)",
            //"Forward-facing upring child seat (for toddlers and smaller children)",
            //"Child booster seat"});
            this.ddlbabyseat2.Items.AddRange(arr);


            this.ddlbabyseat2.Location = new System.Drawing.Point(132, 467);
            this.ddlbabyseat2.Name = "ddlbabyseat2";
            this.ddlbabyseat2.Size = new System.Drawing.Size(219, 24);
            this.ddlbabyseat2.TabIndex = 273;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 468);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(123, 18);
            this.label9.TabIndex = 272;
            this.label9.Text = "Second Child Seat";


        }

        private void IsBiddingEnabled(bool enabled)
        {

            if (enabled)
            {
                chkBidding.Visible = true;
                chkBidding.Checked = true;
            }
            else
            {
                // chkBidding.Visible = false;
                chkBidding.Checked = false;

            }
        }

        private void IsAutoDespatchEnabled(bool enabled)
        {
            if (enabled)
            {
                chkAutoDespatch.Visible = true;
                chkAutoDespatch.Checked = true;
                pnlAutoDespatch.Height = 148;


            }
            else
            {
                // chkAutoDespatch.Visible = false;
                chkAutoDespatch.Checked = false;

                // pnlAutoDespatch.Height = 118;

                pnlAutoDespatch.Height = 148;



            }

            ShowAutoDespatchLabels(enabled);

        }

        private void ShowAutoDespatchLabels(bool show)
        {

            //  numBeforeMinutes.Visible = show;


            numBeforeMinutes.Value = AppVars.objPolicyConfiguration.BookingExpiryNoticeInMins.ToInt()
                                    + AppVars.objPolicyConfiguration.AutoDespatchMinsBeforeDue.ToInt();


        }

        AutoCompleteTextBox aTxt;



        private void ShowAddresses()
        {
            int sno = 1;

            var finalList = (from a in AppVars.zonesList
                             from b in res
                             where b.Contains(a) && (b.Substring(b.IndexOf(a), a.Length) == a && (b.IndexOf(a) - 1) >= 0 && b[b.IndexOf(a) - 1] == ' ' && GeneralBLL.GetHalfPostCodeMatch(b) == a)

                             select b).ToArray<string>();


            if (finalList.Count() > 0)
            {
                finalList = finalList.Union(res).ToArray<string>();

            }
            else
                finalList = res;



            if (finalList.Count() > 1 && AppVars.objPolicyConfiguration.RecentAddressesFrequency.ToInt() > 0)
            {

                var list = General.GetQueryable<Gen_RecentAddress>(null).OrderByDescending(c => c.SearchedOn).Take(50)
                    .Where(c => c.AddressLine1.Contains(searchTxt) && (ddlCompany.SelectedValue == null || c.CompanyId == ddlCompany.SelectedValue.ToIntorNull()))
                    .Distinct().Select(c => c.AddressLine1).ToArray<string>();

                if (list.Count() > 0)
                {


                    try
                    {

                        list = (from a in XDocument.Parse("<?xml version=\"1.0\" encoding=\"utf-8\"?><adds>" + String.Join(" ", list) + "</adds>").Element("adds").Nodes()
                                where (a as XElement).Value.Contains(searchTxt)
                                select (a as XElement).Value).Distinct().ToArray<string>();


                        finalList = list.Union(finalList).ToArray<string>();
                    }
                    catch
                    {


                    }
                }
            }


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



        void ddlToLocation_OnRefreshing(object sender, EventArgs e)
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

                //needtouncomment
                lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.NewtoDoorNoLoc.Y + 1);

                txtToFlightDoorNo.MaxLength = 100;
                txtToFlightDoorNo.Width = 170;

                txtToFlightDoorNo.Text = string.Empty;
                txtToFlightDoorNo.Visible = true;

                //needtouncomment
                txtToFlightDoorNo.Location = this.NewtoDoorNoLoc;

                txtToStreetComing.Text = string.Empty;
                txtToStreetComing.Visible = false;


                // lblToDoorFlightNo.Visible = false;
                lblToStreetComing.Visible = false;


                if (locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    //txtToAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                    txtToAddress.Text = AppVars.objSubCompany.Address.ToStr().ToUpper().Trim();
                    txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                    UpdateAutoCalculateFares();
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

                //needtouncomment
                txtToFlightDoorNo.Location = this.OldtoDoorNoLoc;
                txtToPostCode.Visible = true;
                txtToFlightDoorNo.Visible = true;
                txtToStreetComing.Visible = true;
                lblToDoorFlightNo.Visible = true;
                lblToStreetComing.Visible = true;

                lblToLoc.Text = "To PostCode";

                //needtouncomment
                lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.OldtoDoorNoLoc.Y);
                lblToDoorFlightNo.Text = "To Door No";
                lblToStreetComing.Text = "To Street";

                txtToFlightDoorNo.MaxLength = 100;
                txtToFlightDoorNo.Width = 170;

            }

            else if (locTypeId == Enums.LOCATION_TYPES.AIRPORT)
            {
                SetReturnAirportJob(opt_JReturnWay.ToggleState);
                DetachLocationsSelectionEvent(ddlToLocation);
                ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
                ddlToLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlToLocation);

                SetReturnFrom(ToggleState.On);
            }


            else
            {



                SetOthersToLocation();

                DetachLocationsSelectionEvent(ddlToLocation);
                ComboFunctions.FillLocationsCombo(ddlToLocation, c => c.LocationTypeId == locTypeId);
                ddlToLocation.SelectedIndex = -1;
                AttachLocationSelectionEvent(ddlToLocation);



                SetReturnFrom(ToggleState.Off);

                if (opt_JReturnWay.ToggleState == ToggleState.Off)
                {
                    btnReturnFrom.Text = "Drop Off Plot";
                    txtReturnFrom.Visible = false;
                    ddlDropOffPlot.Visible = true;

                }
            }




        }


        private void DetachLocationsSelectionEvent(RadComboBox djcbo)
        {
            if (djcbo.Name == "ddlFromLocation")
                djcbo.SelectedValueChanged -= new EventHandler(ddlFromLocation_SelectedValueChanged);
            else
                djcbo.SelectedValueChanged -= new EventHandler(ddlToLocation_SelectedValueChanged);

        }


        private void AttachLocationSelectionEvent(RadComboBox djcbo)
        {
            if (djcbo.Name == "ddlFromLocation")
                djcbo.SelectedValueChanged += new EventHandler(ddlFromLocation_SelectedValueChanged);
            else
                djcbo.SelectedValueChanged += new EventHandler(ddlToLocation_SelectedValueChanged);

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





            lblToDoorFlightNo.Text = "Dest. Notes";
            lblToDoorFlightNo.Visible = true;
            // lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.NewtoDoorNoLoc.Y + 1);

            txtToFlightDoorNo.MaxLength = 100;
            txtToFlightDoorNo.Width = 200;

            txtToFlightDoorNo.Text = string.Empty;
            txtToFlightDoorNo.Visible = true;

            //needtouncomment
            txtToFlightDoorNo.Location = this.NewtoDoorNoLoc;



            //

            if (ddlReturnFromAirport != null)
            {

                ddlReturnFromAirport.SelectedValue = null;
                ddlReturnFromAirport.Visible = false;
                lblReturnFromAirport.Visible = false;
            }
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

                    //needtouncomment
                    lblToDoorFlightNo.Location = new Point(lblToDoorFlightNo.Location.X, this.OldtoDoorNoLoc.Y);
                    txtToFlightDoorNo.Location = this.OldtoDoorNoLoc;
                    txtToFlightDoorNo.Visible = true;
                    txtToStreetComing.Visible = true;

                    lblToDoorFlightNo.Visible = true;
                    lblToStreetComing.Visible = true;

                    lblToLoc.Text = "To Location";

                    lblToDoorFlightNo.Text = "Flight No";
                    lblToStreetComing.Text = "Coming From";


                    if (ddlReturnFromAirport != null)
                    {
                        lblReturnFromAirport.Visible = true;
                        ddlReturnFromAirport.Visible = true;
                        if (ddlReturnFromAirport.DataSource == null)
                            ComboFunctions.FillLocationsCombo(ddlReturnFromAirport, c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT);

                        ddlReturnFromAirport.SelectedIndex = -1;
                    }
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


                //needtouncomment
                lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.NewFromDoorNoLoc.Y + 1);

                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 170;

                txtFromFlightDoorNo.Text = string.Empty;
                txtFromFlightDoorNo.Visible = true;

                //needtouncomment
                txtFromFlightDoorNo.Location = this.NewFromDoorNoLoc;


                txtFromStreetComing.Text = string.Empty;
                txtFromStreetComing.Visible = false;


                lblFromStreetComing.Visible = false;

                lblFromLoc.Text = "Pickup Point";

                if (locTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    PickFromBase();

                    SetPickupZone(txtFromAddress.Text);


                    UpdateAutoCalculateFares();

                }
            }
            else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {

                LoadPostCodes();

                txtFromAddress.Text = string.Empty;
                txtFromAddress.Visible = false;

                DetachLocationsSelectionEvent(ddlFromLocation);
                ddlFromLocation.SelectedValue = null;
                ddlFromLocation.Visible = false;
                AttachLocationSelectionEvent(ddlFromLocation);

                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 170;


                //needtouncomment
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

                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 170;


                //needtouncomment
                lblFromDoorFlightNo.Location = new Point(lblFromDoorFlightNo.Location.X, this.OldfromDoorNoLoc.Y);
                txtFromFlightDoorNo.Location = this.OldfromDoorNoLoc;
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

                txtFromFlightDoorNo.MaxLength = 100;
                txtFromFlightDoorNo.Width = 200;

                txtFromFlightDoorNo.Text = string.Empty;
                txtFromFlightDoorNo.Visible = true;

                //needtouncomment
                txtFromFlightDoorNo.Location = this.NewFromDoorNoLoc;


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

        private void FillSubCompanyCombo(ComboBox cbo)
        {
            cbo.DisplayMember = "CompanyName";
            cbo.ValueMember = "Id";
            cbo.DataSource = General.GetQueryable<Gen_SubCompany>(null).OrderBy(c => c.CompanyName).ToList();

            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbo.SelectedValue = AppVars.objSubCompany.Id;


            if (cbo.Items.Count == 1 || AppVars.DefaultBookingSubCompanyId != 0)
            {
                cbo.Visible = false;

            }
        }
        private void FillPlotCombo(ComboBox cbo, IList list)
        {
            cbo.DisplayMember = "ZoneName";
            cbo.ValueMember = "Id";
            //   cbo.DataSource = General.GetQueryable<Gen_Zone>(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { args.Id, ZoneName = args.OrderNo + ". " + args.ZoneName }).ToList();
            cbo.DataSource = list;



            cbo.DropDownStyle = ComboBoxStyle.DropDown;

            cbo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cbo.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbo.SelectedIndex = -1;
        }

        private void FillCombos()
        {




            using (TaxiDataContext db = new TaxiDataContext())
            {

                var locTypeList = db.GetTable<Gen_LocationType>().OrderBy(c => c.LocationType).Select(args => new { args.Id, args.LocationType }).ToList();



                //   ComboFunctions.FillCombo(db.GetTable<Gen_LocationType>().OrderBy(c => c.LocationType).Select(args => new { args.Id, args.LocationType }).ToList(), ddlFromLocType, "LocationType", "Id");

                //ComboFunctions.FillCombo(db.GetTable<Gen_LocationType>().OrderBy(c => c.LocationType).Select(args => new {args.Id,args.LocationType }).ToList(), ddlToLocType, "LocationType", "Id");

                ComboFunctions.FillCombo(locTypeList, ddlFromLocType, "LocationType", "Id");

                ComboFunctions.FillCombo(locTypeList.ToList(), ddlToLocType, "LocationType", "Id");




                ddlSubCompany.DisplayMember = "CompanyName";
                ddlSubCompany.ValueMember = "Id";
                ddlSubCompany.DataSource = db.GetTable<Gen_SubCompany>().Select(args => new { args.Id, args.CompanyName }).ToList();

                ddlSubCompany.DropDownStyle = ComboBoxStyle.DropDownList;


               

                ddlSubCompany.SelectedValue = AppVars.objSubCompany.Id;
             

                if (ddlSubCompany.Items.Count == 1 || AppVars.DefaultBookingSubCompanyId != 0)
                    ddlSubCompany.Visible = false;


                if (ddlSubCompany.Items.Count > 1 && AppVars.CanTransferJob)
                {
                    ddlSubCompany.Visible = true;

                }


                if (ddlFromLocType.SelectedValue.ToInt() != Enums.LOCATION_TYPES.ADDRESS)
                    ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;

                if (ddlToLocType.SelectedValue.ToInt() != Enums.LOCATION_TYPES.ADDRESS)
                    ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;

                if (AppVars.objPolicyConfiguration.EnablePDA.ToBool())
                {
                    var zonesList = db.GetTable<Gen_Zone>().Where(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { args.Id, ZoneName = args.OrderNo + ". " + args.ZoneName }).ToList();

                    FillPlotCombo(ddlPickupPlot, zonesList);
                    FillPlotCombo(ddlDropOffPlot, zonesList.ToList());


                    if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool()
                        && AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool() && AppVars.objPolicyConfiguration.ZoneWiseFareType.ToInt()==1)
                    {
                        ddlPickupPlot.SelectedValueChanged += new EventHandler(ddlPlot_SelectedValueChanged);
                        ddlDropOffPlot.SelectedValueChanged += new EventHandler(ddlPlot_SelectedValueChanged);
                    }

                    ddlPickupPlot.TextChanged += new EventHandler(ddlPlot_TextChanged);
                    ddlDropOffPlot.TextChanged += new EventHandler(ddlPlot_TextChanged);
                    //FillPlotCombo(ddlPickupPlot,db.GetTable<Gen_Zone>().Where(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { args.Id, ZoneName = args.OrderNo + ". " + args.ZoneName }).ToList() );
                    //FillPlotCombo(ddlDropOffPlot, db.GetTable<Gen_Zone>().Where(c => c.MinLatitude != null).OrderBy(c => c.OrderNo).Select(args => new { args.Id, ZoneName = args.OrderNo + ". " + args.ZoneName }).ToList());

                }

                //Fill Booking Type Combo
                ComboFunctions.FillCombo<BookingType>(db.GetTable<BookingType>().Where(c => c.IsVisible==true).ToList(), ddlBookingType, "BookingTypeName", "Id");

              

                if (PickBookingTypeId == null)
                    PickBookingTypeId = Enums.BOOKING_TYPES.LOCAL;

                ddlBookingType.SelectedValue = PickBookingTypeId;


                ComboFunctions.FillCombo(db.GetTable<Fleet_VehicleType>().OrderBy(c => c.OrderNo).Select(args => new { args.Id, args.VehicleType }).ToList(), ddlVehicleType, "VehicleType", "Id", false);


                ComboFunctions.FillCombo<Gen_PaymentType>(db.GetTable<Gen_PaymentType>().Where(c => c.IsVisible == true).OrderBy(c => c.PaymentType).ToList(), ddlPaymentType, "PaymentType", "Id");

              //  EnableRoomCharges(false);

            }

        }

        private void FocusOnPickupPlot()
        {
            ddlPickupPlot.Focus();
        }

        private void FocusOnDropOffPlot()
        {
            ddlDropOffPlot.Focus();
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

                if ((LocTypeId == Enums.LOCATION_TYPES.ADDRESS || LocTypeId == Enums.LOCATION_TYPES.BASE))
                {
                    if (objMaster.PrimaryKeyValue == null)
                    {

                        FocusOnFromAddress();
                    }
                    else
                    {
                        FocusOnFromDoor();

                    }
                }
                else if (LocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    if (objMaster.PrimaryKeyValue == null)
                    {
                        FocusOnFromPostCode();
                    }
                    
                }
                else
                {
                    if (objMaster.PrimaryKeyValue == null)
                    {
                        FocusOnFromLocation();
                    }
                    else
                    {
                        FocusOnFromDoor();

                    }

                }

                txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
                txtCustomerPhoneNo.TextChanged += new EventHandler(txtCustomerPhoneNo_TextChanged);


                if (this.Size.Height == 750)
                    this.Size = new Size(this.Size.Width, 784);


                if (AppVars.listUserRights.Count(c => c.functionId == "SHOW SETFARES") > 0)
                {

                    btnSetFares.Visible = true;
                }



                if (AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                {
                    numCustomerFares.Validated += new EventHandler(numCustomerFares_Validated);
                    numReturnCustFare.Validated += new EventHandler(numReturnCustFare_Validated);


                }

                if (objMaster != null && objMaster.PrimaryKeyValue != null)
                {

                    th = new System.Threading.Thread(new ThreadStart(DisplayBooking_Map));
                    th.IsBackground = true;
                    th.Start();
                }
            }
            catch (Exception ex)
            {


            }
        }

        void numReturnCustFare_Validated(object sender, EventArgs e)
        {
            try
            {

                // only for gbc cars
                //if (AppVars.objPolicyConfiguration.SendDirectBookingConfirmationEmail.ToBool())
                //{

                if (numReturnCustFare.Text.Length > 0 && numReturnCustFare.Text.ToDecimal() > 0)
                {

                //if (numReturnCustFare.Value > 0)
                //{
                    decimal serviceCharge = 0.00m;


                    decimal price = numReturnCustFare.Text.ToDecimal();

                    if (ddlCompany.SelectedValue != null)
                        price = numReturnCompanyFares.Value;

                    Gen_ServiceCharge objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => (price >= c.FromValue && price <= c.ToValue) && (ddlCompany.SelectedValue.ToInt() == 0 || (c.IsAccount != null && c.IsAccount == true)));

                    if (objServiceCharge != null)
                    {

                        if (objServiceCharge.AmountWise.ToBool())
                        {
                            serviceCharge = objServiceCharge.ServiceChargeAmount.ToDecimal();
                        }
                        else
                        {
                            if (ddlCompany.SelectedValue != null)
                                serviceCharge = (numReturnCompanyFares.Value * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;
                            else
                                serviceCharge = (price * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;



                        }
                     
                        if (ddlCompany.SelectedValue != null)
                            numReturnFare.Value = numReturnCompanyFares.Value - serviceCharge;
                        else
                            numReturnFare.Value = price - serviceCharge;
                    }
                }

                //}            
            }
            catch
            {

            }
        }

        void numCustomerFares_Validated(object sender, EventArgs e)
        {
            try
            {

                // only for gbc cars
                //if (AppVars.objPolicyConfiguration.SendDirectBookingConfirmationEmail.ToBool())
                //{


                if (numCustomerFares.Text.Length > 0 && numCustomerFares.Text.ToDecimal() > 0)
                {
                    decimal serviceCharge = 0.00m;


                    decimal price = numCustomerFares.Text.ToDecimal();

                    if (ddlCompany.SelectedValue != null)
                        price = numCompanyFares.Value;

                    Gen_ServiceCharge objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => (price >= c.FromValue && price <= c.ToValue) && (ddlCompany.SelectedValue.ToInt() == 0 || (c.IsAccount != null && c.IsAccount == true)));

                    if (objServiceCharge != null)
                    {

                        if (objServiceCharge.AmountWise.ToBool())
                        {
                            serviceCharge = objServiceCharge.ServiceChargeAmount.ToDecimal();
                        }
                        else
                        {
                            if (ddlCompany.SelectedValue != null)
                                serviceCharge = (numCompanyFares.Value * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;
                            else
                                serviceCharge = (price * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;



                        }

                        if (ddlCompany.SelectedValue != null)
                            numFareRate.Value = numCompanyFares.Value - serviceCharge;
                        else
                            numFareRate.Value = price - serviceCharge;
                    }
                }

                //}            
            }
            catch
            {

            }
        }


        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;

        private void txtCustomerMobileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerMobileNo.Text.Trim().Length == 11)
                {
                    if (IsBookingExistForContact())
                    {
                        txtCustomerPhoneNo.TextChanged -= new EventHandler(txtCustomerPhoneNo_TextChanged);
                        txtCustomerMobileNo.TextChanged -= new EventHandler(txtCustomerMobileNo_TextChanged);

                        SearchBooking();

                        txtCustomerPhoneNo.TextChanged += new EventHandler(txtCustomerPhoneNo_TextChanged);
                        txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
                    }




                }

                

                if (!string.IsNullOrEmpty(txtCustomerMobileNo.Text.Trim()))
                {

                    if (errorProvider1 == null)
                    {
                        this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
                        ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();

                        this.errorProvider1.ContainerControl = this;
                        ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();

                    }

                    if (txtCustomerMobileNo.Text.Trim().Length == 11)
                    {

                        errorProvider1.Icon = Resources.Resource1.verified2;
                        errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                        errorProvider1.SetError(txtCustomerMobileNo, "Mobile No is Verified");

                     
                    }
                    else
                    {

                        errorProvider1.Icon = Resources.Resource1.warning;
                        errorProvider1.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                        errorProvider1.SetError(txtCustomerMobileNo, "Invalid " + "Mobile No!" + Environment.NewLine + "Please enter 11 digits number");


                    }
                }


            }
            catch (Exception ex)
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



                        txtCustomerPhoneNo.TextChanged -= new EventHandler(txtCustomerPhoneNo_TextChanged);
                        txtCustomerMobileNo.TextChanged -= new EventHandler(txtCustomerMobileNo_TextChanged);



                        SearchBooking();


                        txtCustomerPhoneNo.TextChanged += new EventHandler(txtCustomerPhoneNo_TextChanged);
                        txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
                    }




                }




                if (!string.IsNullOrEmpty(txtCustomerPhoneNo.Text.Trim()))
                {


                    if (errorProvider2 == null)
                    {
                        this.errorProvider2 = new System.Windows.Forms.ErrorProvider();
                        ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();

                        this.errorProvider2.ContainerControl = this;
                        ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();

                    }

                    if (txtCustomerPhoneNo.Text.Trim().Length == 11)
                    {

                        errorProvider2.Icon = Resources.Resource1.verified2;
                        errorProvider2.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                        errorProvider2.SetError(txtCustomerPhoneNo, "Telephone No is Verified");


                    }
                    else
                    {

                        errorProvider2.Icon = Resources.Resource1.warning;
                        errorProvider2.BlinkStyle = ErrorBlinkStyle.NeverBlink;
                        errorProvider2.SetError(txtCustomerPhoneNo, "Invalid " + "Telephone No!" + Environment.NewLine + "Please enter 11 digits number");


                    }
                }


            }
            catch (Exception ex)
            {


            }
        }

        private bool IsBookingExistForContact()
        {
            bool isExist = false;

            try
            {
                string telNo = txtCustomerPhoneNo.Text.Trim().ToLower();
                string mobNo = txtCustomerMobileNo.Text.Trim().ToLower();



                isExist = General.GetQueryable<Booking>(null).Count(c => (c.CustomerMobileNo != null && c.CustomerPhoneNo != null)
                         && (((c.CustomerPhoneNo == telNo || telNo == string.Empty) &&
                             (c.CustomerMobileNo == mobNo || mobNo == string.Empty))
                             ||

                              ((c.CustomerPhoneNo == mobNo || mobNo == string.Empty) &&
                             (c.CustomerMobileNo == telNo || telNo == string.Empty))
                             )
                             ) > 0;




                //= data1.Where(a => (a.CustomerPhoneNo == telNo || telNo == string.Empty) &&
                //            (a.CustomerMobileNo == mobNo || mobNo == string.Empty)).Count() > 0;




            }
            catch (Exception ex)
            {


            }
            return isExist;

        }




        //private decimal CalculateDistance(string origin, string destination)
        //{
        //    decimal miles = 0.00m;
        //    bool exist = false;
        //    try
        //    {


        //            //try
        //            //{


        //            //    if ( objTaxiService != null)
        //            //    {

        //            //        miles = objTaxiService.GetDistanceAndTime(origin, destination, ref estimatedTime);

        //            //        if (miles > 0)
        //            //            exist = true;

        //            //    }
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    if (ex.Message.StartsWith("Could not connect"))
        //            //    {

        //            //        startserviceWCF();

        //            //    }

        //            //}


        //            if (exist == false)
        //            {

        //                string url2 = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + ", UK&destinations=" + destination + ", UK&mode=driving&units=imperial";

        //                //string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + ", UK&destination=" + destination + ", UK&sensor=false";

        //                using (System.Data.DataSet ds = new System.Data.DataSet())
        //                {

        //                    using (XmlTextReader reader = new XmlTextReader(url2))
        //                    {
        //                        reader.WhitespaceHandling = WhitespaceHandling.Significant;

        //                        ds.ReadXml(reader);

        //                        reader.Close();
        //                    }

        //                    DataTable dt = ds.Tables["distance"];

        //                    if (dt != null)
        //                    {
        //                        miles = dt.Rows[0][1].ToStr().Replace("mi", "").ToStr().Trim().ToDecimal();
        //                        dt.Dispose();
        //                        dt = null;


        //                    }
        //                    else
        //                    {

        //                        mileageError = true;

        //                    }
        //                }
        //            }


        //    }
        //    catch
        //    {

        //        mileageError = true;
        //    }


        //    return miles;
        //}


        private void MileageError()
        {
            lblMap.Text = "Mileage not found";

        }




        //private decimal CalculateTotalDistance(string origin, string via, string destination)
        //{


        //    decimal miles = 0.00m;


        //    origin = General.GetPostCodeMatch(origin);
        //    destination = General.GetPostCodeMatch(destination);

        //    string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + "&sensor=false";

        //    if (!string.IsNullOrEmpty(via))
        //    {
        //        url2 += "&waypoints=" + via;

        //    }

        //    XmlTextReader reader = new XmlTextReader(url2);
        //    reader.WhitespaceHandling = WhitespaceHandling.Significant;
        //    System.Data.DataSet ds = new System.Data.DataSet();
        //    ds.ReadXml(reader);
        //    DataTable dt = ds.Tables["distance"];
        //    if (dt != null)
        //    {
        //        var rows = dt.Rows.OfType<DataRow>().Where(c => c[0].ToStr().Trim() == c[1].ToStr().Strip("m").Trim()).ToList();

        //        decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
        //        decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

        //        decimal milKM = 0.621m;
        //        decimal milMeter = 0.00062137119m;

        //        miles = (milKM * distanceKm) + (milMeter * distanceMeter);

        //    }


        //    return miles;

        //}



        private decimal CalculateTotalDistance(string origin, string[] via, string destination)
        {


            decimal miles = 0.00m;

            if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
            {
                origin = General.GetPostCodeMatch(origin);
                destination = General.GetPostCodeMatch(destination);
            }

            string actualOrigin = origin;
            string actualDestination = destination;


            if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
            {

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

                    miles += General.CalculateDistance(origin, destination);
                }
            }
            else
            {
                for (int i = 0; i < via.Count(); i++)
                {
                    if (i == 0)
                    {
                        destination = via[i].ToStr();
                    }
                    else
                    {
                        origin = via[i - 1].ToStr();
                        destination = via[i].ToStr();
                    }

                    miles += General.CalculateDistance(origin, destination);
                }

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


            miles += General.CalculateDistance(origin, destination);

            return miles;

        }



        private void ddlToLocType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            FillToLocations();


        }

        private void btnDetailMap_Click(object sender, EventArgs e)
        {


            ShowMap();

        }


        private void ShowMap()
        {
            try
            {
                int? locTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
                int? locId = ddlFromLocation.SelectedValue.ToIntorNull();


                string origin = "";
                string destination = "";

                if (locTypeId == Enums.LOCATION_TYPES.ADDRESS || locTypeId == Enums.LOCATION_TYPES.BASE)
                    origin = txtFromAddress.Text.Trim();
                else if (locTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    origin = txtFromPostCode.Text.Trim();
                else
                    origin = General.GetPostCodeMatch(ddlFromLocation.ComboBoxElement.TextBoxElement.TextBoxItem.Text.Trim());

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
                    destination = General.GetPostCodeMatch(ddlToLocation.ComboBoxElement.TextBoxElement.TextBoxItem.Text.Trim());





                if (destination == string.Empty)
                {
                    destination = origin;
                    //ENUtils.ShowMessage("Map not found");
                    //return;
                }



                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
                {

                    if (General.GetPostCodeMatch(origin) == string.Empty || General.GetPostCodeMatch(destination) == string.Empty)
                    {
                        ENUtils.ShowMessage("Map not found");
                        return;
                    }


                }


                string[] viaLocs = new string[0];

                if (pnlVia != null)
                {


                    viaLocs = grdVia.Rows.Select(c => c.Cells["VIALOCATIONVALUE"].Value.ToStr()).ToArray<string>();

                }



                //  string viaLocations = "";
                if (viaLocs.Count() > 0)
                {

                    if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
                    {

                        viaLocs = viaLocs.Select(c => General.GetPostCodeMatch(c)).Where(c => c.Length > 0).ToArray<string>();
                        if (viaLocs.Count() == 0)
                        {
                            ENUtils.ShowMessage("Map not found");
                            return;

                        }
                    }
                    // viaLocations = "+to:" + string.Join("+to:", viaLocs) + "+to:";

                }




                string viaStr = "**";

                if (viaLocs != null && viaLocs.Count() > 0)
                {
                    viaStr = string.Join(">>>", viaLocs);

                    viaStr = viaStr.Replace(" ", "**").Trim();

                }


                string connString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionStringRouteMap"].ToStr();

                if (!string.IsNullOrEmpty(connString))
                {

                    connString = Application.StartupPath + "\\TreasureRouteMap.exe";
                }


                if (connString.ToStr().Length > 0 && File.Exists(connString) == true)
                {
                    Process proc = Process.GetProcesses().FirstOrDefault(c => c.ProcessName.Contains("TreasureRouteMap"));

                    if (proc != null)
                    {
                        proc.Kill();
                        proc.CloseMainWindow();
                        proc.Close();
                    }

                    string conn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");


                    string arg = "frmmap" + " " + conn + " " + origin.Replace(" ", "**") + " " + viaStr.Replace(" ", "**") + " " + destination.Replace(" ", "**");
                    Process.Start(connString, arg);
                }
                else
                {


                    frmMap frm = new frmMap(origin, viaLocs, destination);

                    frm.ShowDialog();

                }




            }
            catch (Exception ex)
            {


            }



        }




        private bool ValidateOptionalMandatoryFields()
        {

            bool rtn = true;

            if (numFareRate.Value == 0)
            {
                if ((ddlCompany.Enabled && numCompanyFares.Value == 0))
                {

                    if (DialogResult.No == MessageBox.Show("Please enter a Fares,Company Price..." + Environment.NewLine + "Do you still want to save a booking ? " + Environment.NewLine, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        radLabel5.ForeColor = Color.Magenta;
                        lblCompanyPrice.ForeColor = Color.Magenta;

                        rtn = false;
                    }
                }
                else
                {

                    if (DialogResult.No == MessageBox.Show("Please enter a Fares..." + Environment.NewLine + "Do you still want to save a booking ? " + Environment.NewLine, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {

                        radLabel5.ForeColor = Color.Magenta;
                        rtn = false;
                    }
                }
            }
            else if ((ddlCompany.Enabled && numCompanyFares.Value == 0))
            {

                if (DialogResult.No == MessageBox.Show("Please enter a Company Price..." + Environment.NewLine + "Do you still want to save a booking ? " + Environment.NewLine, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    lblCompanyPrice.ForeColor = Color.Magenta;

                    rtn = false;
                }
            }







            return rtn;

        }

        #region Overridden Methods

        int? bookingstatusId;
        private bool Save()
        {

            //  Close();
            try
            {


                if (AppVars.objPolicyConfiguration.ValidateOptionalFaresOnBooking.ToBool() && ValidateOptionalMandatoryFields() == false)
                    return false;


                bool IsAddMode = false;
                DateTime nowDate = DateTime.Now;



                string accountName = "";
                string paymentType = "";
                string journeyType = "";


                string FromAdd = "";
                string ToAdd = "";
                string Customer = "";
                decimal FareRate = 0;
                string Vehicle = "";
                string From = "";
                string To = "";
                string Phone = "";
                string Mobile = "";
                int pickupZoneId = 0;
                int dropOffZoneId = 0;
                DateTime? OldPickupDateTime = null;
                int? oldPaymentTypeId = null;
                string via = "";

                string special = string.Empty;
                //


                if (objMaster.PrimaryKeyValue == null)
                {


                    objMaster.New();

                    objMaster.Current.BookingDate = nowDate;
                    objMaster.Current.AddOn = nowDate;
                    objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToIntorNull();
                    objMaster.Current.AddLog = AppVars.LoginObj.UserName.ToStr();
                    objMaster.Current.CallRefNo = this.CallRefNo;
                    IsAddMode = true;


                }
                else
                {

                    pickupZoneId = objMaster.Current.ZoneId.ToInt();
                    dropOffZoneId = objMaster.Current.DropOffZoneId.ToInt();
                    FromAdd = objMaster.Current.FromAddress.ToStr();
                    ToAdd = objMaster.Current.ToAddress.ToStr();


                    if (objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT || objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        FromAdd = objMaster.Current.FromStreet.ToStr() + " " + objMaster.Current.FromAddress.ToStr();

                    if (objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT || objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        ToAdd = objMaster.Current.ToStreet.ToStr() + " " + objMaster.Current.ToAddress.ToStr();



                    Customer = objMaster.Current.CustomerName.ToStr();
                    FareRate = objMaster.Current.FareRate.ToDecimal();
                  //  FareRate = objMaster.Current.TotalCharges.ToDecimal();


                    if(AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().Trim()!="")
                        FareRate = objMaster.Current.GetType().GetProperty(AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().Trim()).GetValue(objMaster.Current, null).ToDecimal();


                    Vehicle = objMaster.Current.Fleet_VehicleType.VehicleType.ToStr();
                    From = objMaster.Current.Gen_LocationType.LocationType.ToStr();
                    To = objMaster.Current.Gen_LocationType1.LocationType.ToStr();
                    Phone = objMaster.Current.CustomerPhoneNo.ToStr();
                    Mobile = objMaster.Current.CustomerMobileNo.ToStr();
                    OldPickupDateTime = objMaster.Current.PickupDateTime;
                    special = objMaster.Current.SpecialRequirements.ToStr().Trim();


                    accountName = objMaster.Current.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                    journeyType = "O/W";

                    if (objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                        journeyType = "Return";
                    else if (objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.WAITANDRETURN)
                        journeyType = "W/R";


                    oldPaymentTypeId = objMaster.Current.PaymentTypeId;
                    paymentType = objMaster.Current.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr();
                    //paymentType = objMaster.Current.Gen_PaymentType.PaymentCategoryId == null ? objMaster.Current.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr()
                    //            : objMaster.Current.Gen_PaymentType.Gen_PaymentCategory.CategoryName.ToStr();

                    if (objMaster.Current.Booking_ViaLocations.Count > 0)
                    {
                        int i = 1;
                        via = string.Join(" * ", objMaster.Current.Booking_ViaLocations.Select(c => "(" + i++.ToStr() + ")" + c.ViaLocValue.ToStr()).ToArray<string>());
                    }

                    objMaster.Edit();

                    objMaster.Current.EditOn = nowDate;
                    objMaster.Current.EditBy = AppVars.LoginObj.LuserId.ToIntorNull();
                    objMaster.Current.EditLog = AppVars.LoginObj.UserName.ToStr();

                }


                if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD && this.objBookingPayment != null)
                {
                    if (objMaster.Current.BookingPayment == null)
                        objMaster.Current.BookingPayment = new Booking_Payment();




                    objMaster.Current.BookingPayment.OrderDescription = this.objBookingPayment.OrderDescription;
                    objMaster.Current.BookingPayment.NameOnCard = this.objBookingPayment.NameOnCard;
                    objMaster.Current.BookingPayment.CardNumber = this.objBookingPayment.CardNumber;
                    objMaster.Current.BookingPayment.CardExpiryDate = this.objBookingPayment.CardExpiryDate;
                    objMaster.Current.BookingPayment.CardStartDate = this.objBookingPayment.CardStartDate;
                    objMaster.Current.BookingPayment.IssueNumber = this.objBookingPayment.IssueNumber;
                    objMaster.Current.BookingPayment.CV2 = this.objBookingPayment.CV2;
                    objMaster.Current.BookingPayment.Email = this.objBookingPayment.Email;
                    objMaster.Current.BookingPayment.PhoneNo = this.objBookingPayment.PhoneNo;
                    objMaster.Current.BookingPayment.Address = this.objBookingPayment.Address;
                    objMaster.Current.BookingPayment.City = this.objBookingPayment.City;
                    objMaster.Current.BookingPayment.State = this.objBookingPayment.State;
                    objMaster.Current.BookingPayment.PostCode = this.objBookingPayment.PostCode;
                    objMaster.Current.BookingPayment.Status = this.objBookingPayment.Status;
                    objMaster.Current.BookingPayment.AuthCode = this.objBookingPayment.AuthCode;
                }



                if (pnlComcab != null)
                {

                    objMaster.Current.CashRate = numComcab_Cash.Value.ToDecimal();
                    objMaster.Current.AccountRate = numComcab_Account.Value.ToDecimal();
                    objMaster.Current.ExtraMile = numComcab_ExtraMile.Value.ToDecimal();
                    objMaster.Current.WaitingMins = numComcab_WaitingMin.Value.ToDecimal();
                }



                if (ddlBabyseat1 != null && ddlbabyseat2 != null)
                {

                    string babyseats = string.Empty;



                    if (ddlBabyseat1.SelectedIndex > 0)
                        babyseats = ddlBabyseat1.SelectedItem.ToStr();


                    if (ddlbabyseat2.SelectedIndex > 0)
                        babyseats += "<<<" + ddlbabyseat2.SelectedItem.ToStr();

                    else
                    {
                        if (ddlBabyseat1.SelectedIndex > 0)
                            babyseats += "<<<";
                    }


                    objMaster.Current.BabySeats = babyseats;

                }


                objMaster.Current.BookingTypeId = ddlBookingType.SelectedValue.ToIntorNull();


                int? driverId = ddlDriver.SelectedValue.ToIntorNull();


                if (objMaster.Current.Id > 0
                    && (driverId == null && objMaster.Current.DriverId != null)
                    && (objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE
                    || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED))
                {

                    driverId = objMaster.Current.DriverId;
                    objMaster.Current.DriverId = driverId;

                }
                else
                {
                    objMaster.Current.DriverId = driverId;
                }


                objMaster.Current.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                objMaster.Current.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();
                objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();

            //    objMaster.Current.CustomerCreditCardDetails = txtCustomerCreditCardNo.Text.Trim();
            //    objMaster.Current.CompanyCreditCardDetails = txtCompanyCreditCardNo.Text.Trim();


                // Added on 19/08/2015 Danish (For Half Card and Half Cash)
                if (chkSecondaryPaymentType != null)
                {
                    if (chkSecondaryPaymentType.Checked)
                    {

                        objMaster.Current.SecondaryPaymentTypeId = Enums.PAYMENT_TYPES.CASH;
                        objMaster.Current.CashFares = numCashPaymentFares.Value.ToDecimal();
                    }
                    else
                    {

                        objMaster.Current.SecondaryPaymentTypeId = null;
                        objMaster.Current.CashFares = 0.00m;
                    }

                }
                //



                if (ddlDepartment != null)
                    objMaster.Current.DepartmentId = ddlDepartment.SelectedValue.ToLongorNull();



                if (ddlEscort != null)
                {
                    objMaster.Current.EscortId = ddlEscort.SelectedValue.ToLongorNull();

                    objMaster.Current.EscortPrice = numEscortPrice.Value.ToDecimal();

                }

                objMaster.Current.IsCompanyWise = chkIsCompanyRates.Checked;

                int journeyTypeId = Enums.JOURNEY_TYPES.ONEWAY;

                if (opt_JReturnWay.ToggleState == ToggleState.On)
                {
                    journeyTypeId = Enums.JOURNEY_TYPES.RETURN;

                }
                else if (opt_WaitandReturn.ToggleState == ToggleState.On)
                {

                    journeyTypeId = Enums.JOURNEY_TYPES.WAITANDRETURN;
                }


                objMaster.Current.JourneyTypeId = journeyTypeId;


                objMaster.Current.IsQuotation = chkQuotation.Checked;


                if (pnlOrderNo != null)
                {

                    objMaster.Current.OrderNo = txtOrderNo.Text.ToStr().Trim();
                    objMaster.Current.PupilNo = txtPupilNo.Text.ToStr().Trim();
                }


                if (dtpPickupDate.Enabled)
                {

                    objMaster.Current.PickupDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay).ToDateTime();
                }
                else
                {
                    if (dtpFlightDepDate != null)
                    {
                        objMaster.Current.PickupDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", dtpFlightDepDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay).ToDateTime();
                    }

                }

                if (dtpReturnPickupDate != null)
                {
                    if (dtpReturnPickupDate.Value != null && dtpReturnPickupTime.Value != null)
                    {
                        objMaster.Current.ReturnPickupDateTime = dtpReturnPickupDate.Value.ToDateorNull() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                    }
                    else
                        objMaster.Current.ReturnPickupDateTime = null;

                    objMaster.Current.ReturnDriverId = ddlReturnDriver.SelectedValue.ToIntorNull();


                    objMaster.AddedBy = AppVars.LoginObj.UserName.ToStr();



                    if (txtReturnSpecialReq != null)
                    {

                        objMaster.ReturnSpecialRequirement = txtReturnSpecialReq.Text.Trim();
                    }


                    if (ddlReturnVehicleType != null)
                    {
                        objMaster.ReturnVehicleTypeId = ddlReturnVehicleType.SelectedValue.ToIntorNull();
                    }

                }

                if (objMaster.Current.MasterJobId != null && objMaster.Current.Booking1 != null)
                {
                    objMaster.Current.Booking1.ReturnPickupDateTime = objMaster.Current.PickupDateTime;

                }

                objMaster.Current.SubcompanyId = ddlSubCompany.SelectedValue.ToIntorNull();

                objMaster.Current.NoofPassengers = num_TotalPassengers.Value.ToInt();
                objMaster.Current.NoofLuggages = numTotalLuggages.Value.ToInt();


                //    objMaster.Current.EnableFareMeter = chkHasFareMeter.Checked;

                objMaster.Current.FareRate = numFareRate.Value.ToDecimal();

                if (numReturnFare != null)
                    objMaster.Current.ReturnFareRate = numReturnFare.Value.ToDecimal();


                objMaster.Current.CompanyPrice = numCompanyFares != null ? numCompanyFares.Value.ToDecimal() : 0;
                objMaster.Current.CustomerPrice = numCustomerFares.Value.ToDecimal();

                if (opt_JReturnWay.ToggleState == ToggleState.On)
                {
                    if (numReturnCompanyFares != null)
                        objMaster.Current.WaitingMins = numReturnCompanyFares.Value;

                    if (numReturnCustFare != null)
                        objMaster.ReturnCustomerPrice = numReturnCustFare.Value.ToDecimal();
                }

                objMaster.Current.ParkingCharges = numParkingChrgs.Value.ToDecimal();
                objMaster.Current.WaitingCharges = numWaitingChrgs.Value.ToDecimal();
                objMaster.Current.ExtraDropCharges = numExtraChrgs.Value.ToDecimal();
                objMaster.Current.MeetAndGreetCharges = numMeetCharges.Value.ToDecimal();
                objMaster.Current.CongtionCharges = numCongChrgs.Value.ToDecimal();

                // Add Drv Waiting Mins (Request of Double o cars)
                objMaster.Current.DriverWaitingMins = numDrvWaitingMins.Value.ToInt();


                objMaster.OldCustomerName = objMaster.Current.CustomerName.ToStr().Trim();
                objMaster.Current.CustomerId = ddlCustomerName.Tag.ToIntorNull();
                objMaster.Current.CustomerName = ddlCustomerName.Text.ToStr().Trim();


                objMaster.OldPhoneNo = objMaster.Current.CustomerPhoneNo.ToStr().Trim();
                objMaster.OldMobileNo = objMaster.Current.CustomerMobileNo.ToStr().Trim();

                objMaster.Current.CustomerPhoneNo = txtCustomerPhoneNo.Text.Trim();
                objMaster.Current.CustomerMobileNo = txtCustomerMobileNo.Text.Trim();


                objMaster.OldEmail = objMaster.Current.CustomerEmail.ToStr().Trim();
                objMaster.Current.CustomerEmail = txtEmail.Text.Trim();

                objMaster.Current.SpecialRequirements = txtSpecialRequirements.Text.Trim();


                int FromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
                int ToLocTypeId = ddlToLocType.SelectedValue.ToInt();

                objMaster.Current.FromLocTypeId = FromLocTypeId;
                objMaster.Current.ToLocTypeId = ToLocTypeId;
                objMaster.Current.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
                objMaster.Current.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();


                objMaster.Current.PaymentComments = txtPaymentReference.Text.Trim();

                if (ddlReturnFromAirport != null)
                    objMaster.Current.ReturnFromLocId = ddlReturnFromAirport.SelectedValue.ToIntorNull();


                if (FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromLocTypeId == Enums.LOCATION_TYPES.BASE)
                    objMaster.Current.FromAddress = txtFromAddress.Text.Trim();

                else if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    objMaster.Current.FromAddress = txtFromPostCode.Text.Trim();
                else
                {
                    objMaster.Current.FromAddress = ddlFromLocation.ComboBoxElement.TextBoxElement.TextBoxItem.Text.Trim();
                }



                objMaster.Current.FromDoorNo = txtFromFlightDoorNo.Text.Trim();
                objMaster.Current.FromStreet = txtFromStreetComing.Text.Trim();
                objMaster.Current.FromPostCode = txtFromPostCode.Text.Trim();



                if (ToLocTypeId == Enums.LOCATION_TYPES.ADDRESS || ToLocTypeId == Enums.LOCATION_TYPES.BASE)
                    objMaster.Current.ToAddress = txtToAddress.Text.StripNewLine().Trim();

                else if (ToLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    objMaster.Current.ToAddress = txtToPostCode.Text.Trim();
                else
                {
                    objMaster.Current.ToAddress = ddlToLocation.ComboBoxElement.TextBoxElement.TextBoxItem.Text.Trim();
                }

                if (AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool())
                {

                    if (ddlPickupPlot.SelectedValue == null)
                        objMaster.Current.ZoneId = GetZoneId(objMaster.Current.FromAddress);
                    else
                        objMaster.Current.ZoneId = ddlPickupPlot.SelectedValue.ToIntorNull();

                    if (ddlDropOffPlot.SelectedValue == null)
                        objMaster.Current.DropOffZoneId = GetZoneId(objMaster.Current.ToAddress);
                    else
                        objMaster.Current.DropOffZoneId = ddlDropOffPlot.SelectedValue.ToIntorNull();
                }


                objMaster.Current.ToDoorNo = txtToFlightDoorNo.Text.Trim();
                objMaster.Current.ToStreet = txtToStreetComing.Text.Trim();
                objMaster.Current.ToPostCode = txtToPostCode.Text.Trim();


                objMaster.Current.AutoDespatch = chkAutoDespatch.Checked;
                objMaster.Current.IsBidding = chkBidding.Checked;

                objMaster.Current.DisableDriverSMS = chkDisableDriverSMS.Checked;
                objMaster.Current.DisablePassengerSMS = chkDisablePassengerSMS.Checked;


                objMaster.Current.IsCommissionWise = chkIsCommissionWise.Checked;
                objMaster.Current.DriverCommission = numDriverCommission.Value.ToDecimal();
                objMaster.Current.DriverCommissionType = ddlCommissionType.SelectedValue.ToStr().Trim();
                objMaster.Current.DistanceString = lblMap.Text.ToStr();

                if (chkTakenByAgent != null)
                {

                    objMaster.Current.JobTakenByCompany = chkTakenByAgent.Checked;
                    objMaster.Current.AgentCommissionPercent = numAgentCommissionPercent.Value.ToInt();
                    objMaster.Current.AgentCommission = numAgentCommission.Value.ToDecimal();
                    objMaster.Current.FromFlightNo = ddlAgentCommissionType.Text.Trim();
                }


                if (numJourneyTime != null)
                {
                    objMaster.Current.JourneyTimeInMins = numJourneyTime.Value.ToDecimal();

                }



                objMaster.Current.BookedBy = txtAccountBookedBy != null ? txtAccountBookedBy.Text.Trim() : "";

                if (ddlDirection != null)
                    objMaster.Current.BoundType = ddlDirection.Text.ToStr().Trim();


                // Only in case of Shuttle Booking

                //if (txtGroupJobNo != null)
                //{
                //    objMaster.Current.GroupJobId = txtGroupJobNo.Tag.ToLongorNull();


                //    if (dtpFlightDepDate.Value != null && dtpFlightDepTime.Value != null)
                //    {

                //        objMaster.Current.FlightDepartureDate = string.Format("{0:dd/MM/yyyy HH:mm}", dtpFlightDepDate.Value.ToDate() + dtpFlightDepTime.Value.Value.TimeOfDay).ToDateTime();
                //    }
                //    else if (dtpFlightDepDate.Value != null && dtpFlightDepTime.Value == null)
                //    {
                //        objMaster.Current.FlightDepartureDate = dtpFlightDepDate.Value.ToDateorNull();

                //    }


                //    objMaster.Current.RoomNo = txtRoomNo.Text.Trim();
                //}



                if (grdVia != null)
                {

                    if (grdVia.Rows.Count(c => c.Cells["IsUpdated"].Value.ToInt() == 1) > 0)
                    {
                        objMaster.Current.Booking_ViaLocations.Clear();

                        grdVia.Rows.ToList().ForEach(c => c.Cells["ID"].Value = 0);

                    }
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



                if (objMaster.PrimaryKeyValue != null)
                {

                    string A_FromAdd = objMaster.Current.FromAddress.ToStr();
                    string A_ToAdd = objMaster.Current.ToAddress.ToStr();


                    if (objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT || objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        A_FromAdd = objMaster.Current.FromStreet.ToStr() + " " + objMaster.Current.FromAddress.ToStr();

                    if (objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT || objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                        A_ToAdd = objMaster.Current.ToStreet.ToStr() + " " + objMaster.Current.ToAddress.ToStr();


                    string A_Customer = objMaster.Current.CustomerName.ToStr();
                    decimal A_FareRate = objMaster.Current.FareRate.ToDecimal();

                    if (AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().Trim().Length > 0)
                    {
                        A_FareRate = objMaster.Current.GetType().GetProperty(AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().Trim()).GetValue(objMaster.Current, null).ToDecimal();
                    }
                    
                    
                    //  decimal A_FareRate = objMaster.Current.TotalCharges.ToDecimal();
                    
                    string A_Vehicle = objMaster.Current.Fleet_VehicleType.VehicleType.ToStr();
                    string A_From = objMaster.Current.Gen_LocationType.LocationType.ToStr();
                    string A_To = objMaster.Current.Gen_LocationType1.LocationType.ToStr();
                    string A_Phone = objMaster.Current.CustomerPhoneNo.ToStr();
                    string A_Mobile = objMaster.Current.CustomerMobileNo.ToStr();
               

                    string A_journeyType = "O/W";

                    if (opt_JReturnWay.ToggleState == ToggleState.On)
                        A_journeyType = "Return";
                    else if (opt_WaitandReturn.ToggleState == ToggleState.On)
                        A_journeyType = "W/R";



                    string Get = "";
                    string old = "";

                    if (FromAdd != A_FromAdd)
                    {
                        Get += " Pickup Point: " + A_FromAdd + ": ";
                        old += " Pickup Point: " + FromAdd + ": ";
                    }
                    if (ToAdd != A_ToAdd)
                    {
                        Get += " Destination: " + A_ToAdd + ": ";
                        old += " Destination: " + ToAdd + ": ";


                        //  update destination in karhoo


                        if (objMaster.Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.THIRDPARTY)
                        {

                            string status = "confirmed";



                            if (objMaster.Current.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.CANCELLED
                                && objMaster.Current.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.NOPICKUP
                                && objMaster.Current.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.DISPATCHED)
                            {

                                if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.WAITING)
                                {
                                    if (objMaster.Current.DriverId == null)
                                        status = "confirmed";
                                    else
                                        status = "allocated";

                                }
                                else
                                {
                                    if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.ONROUTE)
                                        status = "allocated";
                                    else
                                    {

                                        status = objMaster.Current.BookingStatus.StatusName.ToStr();

                                    }
                                }
                            }


                            General.UpdateKarhoData(objMaster.Current, objMaster.Current.Id, status);
                        }
                        //



                    }
                    if (Customer != A_Customer)
                    {
                        Get += " Customer Name: " + objMaster.Current.CustomerName.ToStr() + ": ";
                        old += " Customer Name: " + Customer + ": ";
                    }
                    if (FareRate != A_FareRate)
                    {
                        Get += " Fares: " + objMaster.Current.FareRate.ToDecimal() + ": ";
                        old += " Fares: " + FareRate + ": ";
                    }
                    if (Vehicle != A_Vehicle)
                    {
                        Get += " Vehicle: " + objMaster.Current.Fleet_VehicleType.VehicleType.ToStr() + ": ";
                        old += " Vehicle: " + Vehicle + ": ";
                    }
                    if (From != A_From)
                    {
                        Get += " From: " + objMaster.Current.Gen_LocationType.LocationType.ToStr() + ": ";
                        old += " From: " + From + ": ";
                    }
                    if (To != A_To)
                    {
                        Get += " To: " + objMaster.Current.Gen_LocationType1.LocationType.ToStr() + ": ";
                        old += " To: " + To + ": ";                       
                       

                    }

                    if (Phone != A_Phone)
                    {
                        Get += " Phone No: " + objMaster.Current.CustomerPhoneNo.ToStr() + ": ";
                        old += " Phone No: " + Phone + ": ";
                    }

                    if (Mobile != A_Mobile)
                    {
                        Get += " Mobile No: " + objMaster.Current.CustomerMobileNo.ToStr() + ": ";
                        old += " Mobile No: " + Mobile + ": ";
                    }

                    if (dtpPickupDate.Value != null && OldPickupDateTime.Value != null &&
                        (OldPickupDateTime.Value.Date != dtpPickupDate.Value)
                        || (dtpPickupTime.Value != null && OldPickupDateTime.Value.TimeOfDay != dtpPickupTime.Value.Value.TimeOfDay))
                    {
                        Get += " PickupDate/Time: " + string.Format("{0:dd/MM/yyyy HH:mm}", dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay) + ": ";
                        old += " PickupDate/Time: " + string.Format("{0:dd/MM/yyyy HH:mm}", OldPickupDateTime.Value) + ": ";
                    }




                    if (ddlPickupPlot.SelectedValue != null && ddlPickupPlot.SelectedValue.ToInt() != pickupZoneId)
                    {
                        Get += " PickupPlot: " + ddlPickupPlot.Text.ToStr().ToUpper() + ": ";
                        old += " PickupPlot: " + objMaster.Current.Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr() + ": ";

                    }


                    if (ddlDropOffPlot.SelectedValue != null && ddlDropOffPlot.SelectedValue.ToInt() != dropOffZoneId)
                    {
                        Get += " DropOffPlot: " + ddlDropOffPlot.Text.ToStr().ToUpper() + ": ";
                        old += " DropOffPlot: " + objMaster.Current.Gen_Zone.DefaultIfEmpty().ZoneName.ToStr() + ": ";

                    }


                    if (accountName != ddlCompany.Text.Trim())
                    {
                        Get += " Account: " + ddlCompany.Text.Trim() + ": ";
                        old += " Account: " + accountName + ": ";

                    }


                    if (paymentType != ddlPaymentType.Text.Trim())
                    {
                        Get += " Payment Type: " + ddlPaymentType.Text.Trim() + ": ";
                        old += " Payment Type: " + paymentType + ": ";



                        if (objMaster.Current.Gen_PaymentType.DefaultIfEmpty().PaymentCategoryId != null)
                        {
                            paymentType = General.GetObject<Gen_PaymentType>(c => c.PaymentType == ddlPaymentType.Text.Trim()).DefaultIfEmpty().Gen_PaymentCategory.DefaultIfEmpty().CategoryName.ToStr().Trim();

                            if (string.IsNullOrEmpty(paymentType.ToStr().Trim()))
                                paymentType = ddlPaymentType.Text.Trim();

                        }
                        else
                        {

                            paymentType = ddlPaymentType.Text.Trim();
                        }

                    }
                    else
                    {
                        if (objMaster.Current.Gen_PaymentType.DefaultIfEmpty().PaymentCategoryId != null)
                        {

                            paymentType = objMaster.Current.Gen_PaymentType.Gen_PaymentCategory.CategoryName.ToStr();
                        }


                    }



                    if (journeyType != A_journeyType)
                    {
                        Get += " Journey Type: " + A_journeyType + ": ";
                        old += " Journey Type: " + journeyType + ": ";

                    }


                    if (special != txtSpecialRequirements.Text.Trim())
                    {
                        Get += " Special Instruction: " + txtSpecialRequirements.Text.Trim() + ": ";
                        old += " Special Instruction: " + special + ": ";
                    }


                    string A_Via = " ";

                    if (grdVia != null && grdVia.Rows.Count > 0)
                    {
                        int i = 1;
                        A_Via = string.Join(" * ", grdVia.Rows.Select(c => "(" + i++.ToStr() + ")" + c.Cells["VIALOCATIONVALUE"].Value.ToStr()).ToArray<string>());
                    }



                    if (via != A_Via.ToStr().Trim())
                    {
                        Get += " Via: " + via + ": ";
                        old += " Via: " + A_Via + ": ";
                    }


                    var NewRec = Get.TrimEnd(':', ' ', '\n');
                    var oldRec = old.TrimEnd(':', ' ', '\n');

                    if (NewRec != "" && old != "")
                    {
                        int? LoginID = AppVars.LoginObj.LgroupId.ToInt();
                        long BokingID = objMaster.PrimaryKeyValue.ToLong();
                        DateTime now = DateTime.Now.ToDateTime();

                        objMaster.Current.Booking_Logs.Add(new Booking_Log { BookingId = BokingID, User = AppVars.LoginObj.LoginName, BeforeUpdate = oldRec, AfterUpdate = NewRec, UpdateDate = now });

                        string msg = string.Empty;
                        string pickUpPlot = string.Empty;
                        string dropOffPlot = string.Empty;

                        if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "AbbeyCarsleeds")
                        {
                            pickUpPlot = objMaster.Current.ZoneId != null ? "<<<" + objMaster.Current.Gen_Zone1.ZoneName.ToStr() : "";
                            dropOffPlot = objMaster.Current.DropOffZoneId != null ? "<<<" + objMaster.Current.Gen_Zone.ZoneName.ToStr() : "";
                        }

                        string mobNo = objMaster.Current.CustomerMobileNo;
                        if (string.IsNullOrEmpty(mobNo))
                            mobNo = " ";


                        else if (!string.IsNullOrEmpty(A_Phone))
                        {
                            mobNo = mobNo + "/" + A_Phone;
                        }


                        string A_Special = txtSpecialRequirements.Text.Trim();

                        if (string.IsNullOrEmpty(A_Special))
                        {
                            A_Special = " ";

                        }


                        if (objMaster.Current.SecondaryPaymentTypeId != null && objMaster.Current.CashFares.ToDecimal() > 0)
                        {
                            A_Special += " , Additional Cash Payment : " + objMaster.Current.CashFares.ToDecimal();
                        }


                        string A_Account = ddlCompany.Text.Trim();

                        if (string.IsNullOrEmpty(A_Account))
                        {
                            A_Account = " ";
                        }


                     

                        string babySeats = objMaster.Current.BabySeats.ToStr().Trim();

                        if (string.IsNullOrEmpty(babySeats))
                            babySeats = " ";

                        string fares = string.Format("{0:#.##}", numFareRate.Value);

                        msg = (!string.IsNullOrEmpty(objMaster.Current.FromDoorNo) ? objMaster.Current.FromDoorNo + "-" + A_FromAdd + pickUpPlot : A_FromAdd + pickUpPlot) +
                            ">>" +
                            (!string.IsNullOrEmpty(objMaster.Current.ToDoorNo) ? objMaster.Current.ToDoorNo + "-" + A_ToAdd + dropOffPlot : A_ToAdd + dropOffPlot) +
                            ">>" +
                            string.Format("{0:dd/MM/yyyy   HH:mm}", objMaster.Current.PickupDateTime) +
                             ">>" +
                             objMaster.Current.CustomerName +
                             ">>" +
                             mobNo +
                             ">>" + A_Special
                         //    + ">>" + numFareRate.SpinElement.Text
                                + ">>" +A_FareRate
                             + ">>" + A_Vehicle
                             + ">>" + A_Account
                             + ">>" + A_journeyType
                             + ">>" + paymentType
                             + ">>" + A_Via
                             + ">>" + num_TotalPassengers.Value.ToInt()
                             + ">>" + numTotalLuggages.Value.ToInt()
                             + ">>" + babySeats;

                        //   +">>" +  ToStr();


                        // Special Requirement
                        // Vehicle
                        // Account
                        // journery type
                        // Payment Type
                        // Baby Seats
                        // No of Pax
                        // No of Luggage


                        // For TCP Connection
                        if (driverId != null &&
                            (objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.PENDING_START || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                            || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.POB || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.STC)
                            &&  (objMaster.Current.Fleet_Driver.Fleet_Driver_PDASettings.Count == 0  || objMaster.Current.Fleet_Driver.Fleet_Driver_PDASettings[0].CurrentPdaVersion<=15.30m)
                            && AppVars.objPolicyConfiguration.IsListenAll.ToBool() && !string.IsNullOrEmpty(AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim()))
                        {
                            new Thread(delegate()
                            {
                                General.SendPDAMessage("request pda=" + driverId + "=" + objMaster.Current.Id + "=" + "Update Job>>" + driverId + ">>" + objMaster.Current.Id + ">>" + msg + "=8");
                            }).Start();


                            if (AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool())
                            {
                                using (TaxiDataContext db = new TaxiDataContext())
                                {
                                    db.stp_SaveOfflineMessage(objMaster.Current.Id, driverId, "", AppVars.LoginObj.LoginName.ToStr(), "Update Job>>" + driverId + ">>" + objMaster.Current.Id + ">>" + msg + "=8");
                                }

                            }
                        }


                        if (FareRate != A_FareRate)
                        {

                            General.UpdateOnlineBookingFares(objMaster.Current.OnlineBookingId.ToLong(), A_FareRate, objMaster.Current.BookingTypeId.ToInt());
                        }
                    }
                }


                if (bookingstatusId != null)
                    objMaster.Current.BookingStatusId = bookingstatusId;


                objMaster.AutoDespatchBeforeMins = numBeforeMinutes.Value.ToInt();

                if (AppVars.objPolicyConfiguration.AutoBookingDueAlert.ToBool())
                {

                    if (objMaster.PrimaryKeyValue == null || (FromAdd.ToLower().ToStr().Trim() != objMaster.Current.FromAddress.ToLower().ToStr().Trim()))
                    {
                        decimal mile = 0.00m;
                       

                        mile = General.CalculateDistanceFromBaseFull(objMaster.Current.FromAddress.ToStr());

                        objMaster.Current.DeadMileage = mile;

                        if (mile > 0 && mile < 1)
                        {
                            mile = 1;
                        }
                        else
                        {
                            mile = Math.Round(mile, 0);
                        }


                        objMaster.Current.ExtraMile = mile;
                    }
                }


                if (objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                {
                    if (!string.IsNullOrEmpty(txtReturnFrom.Text.Trim()))
                    {
                        objMaster.ReturnFromAddress = txtReturnFrom.Text.Trim();
                    }
                    else if (ddlReturnFromAirport != null && ddlReturnFromAirport.SelectedValue != null)
                    {
                        objMaster.Current.ReturnFromLocId = ddlReturnFromAirport.SelectedValue.ToIntorNull();

                    }


                    if (!string.IsNullOrEmpty(txtReturnTo.Text.Trim()))
                    {
                        objMaster.ReturnToAddress = txtReturnTo.Text.Trim();
                    }
                    else if (ddlReturnTo.SelectedValue != null)
                    {
                        objMaster.ReturnToLocIdv = ddlReturnTo.SelectedValue.ToIntorNull();

                    }
                }

                objMaster.Current.IsReverse = chkReverse.Checked;


                if (chkGenerateToken.Checked && txtTokenNo.Text.Length > 0)
                    objMaster.Current.JobCode = txtTokenNo.Text.Trim();

                objMaster.Current.TipAmount = numTipAmount.Value.ToDecimal();

                objMaster.CheckServiceCharges = AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool();


                objMaster.Current.IsQuotedPrice = chkQuotedPrice.Checked;
                objMaster.Save();


              

                saved = true;
                string mobileNo = objMaster.Current.CustomerMobileNo.ToStr().Trim();


                UpdateSetFareLog(setFareLogMsg.ToStr().Trim());

                if (driverId != null
                    && (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.WAITING || objMaster.Current.BookingStatusId == null))
                {

                    frm = new frmDespatchJob(objMaster.Current);
                    frm.ShowDialog();
                    this.IsDespatched = frm.SuccessDespatched;
                }





                // Advance Booking Confirmation Text


                DateTime? pickupdateTime = objMaster.Current.PickupDateTime;
                if (AppVars.objPolicyConfiguration.EnableAdvanceBookingSMSConfirmation.ToBool()
                    && IsAddMode && pickupdateTime != null && objMaster.Current.IsQuotation.ToBool() == false
                    && (objMaster.Current.CompanyId==null || objMaster.Current.Gen_Company.DefaultIfEmpty().DisableAdvanceText.ToBool()==false))
                {
                    string msg = AppVars.objPolicyConfiguration.AdvanceBookingSMSText.ToStr().Trim();


             

                    string pickupSpan = string.Format("{0:HH:mm}", pickupdateTime);

                    TimeSpan picktime = TimeSpan.Parse(pickupSpan);

                    string nowP = string.Format("{0:HH:mm}", nowDate);
                    TimeSpan nowSpantime = TimeSpan.Parse(nowP);

                    int afterMins = AppVars.objPolicyConfiguration.AdvanceBookingSMSConfirmationMins.ToInt();
                    double minDifference = picktime.Subtract(nowSpantime).TotalMinutes;
                    int dayDiff = pickupdateTime.Value.Date.Subtract(DateTime.Now.Date).Days;
                    if (afterMins == 0 || (dayDiff > 0 || minDifference >= afterMins || minDifference < 0))
                    {
                        object propertyValue = string.Empty;

                        foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                        {
                            switch (tag.TagObjectName)
                            {
                                case "booking":

                                    if (tag.TagPropertyValue.Contains('.'))
                                    {

                                        string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                        object parentObj = objMaster.Current.GetType().GetProperty(val[0]).GetValue(objMaster.Current, null);

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
                                        propertyValue = objMaster.Current.GetType().GetProperty(tag.TagPropertyValue).GetValue(objMaster.Current, null);
                                    }


                                    if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                                    {
                                        propertyValue = objMaster.Current.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objMaster.Current, null);
                                    }
                                    break;


                                case "Booking_ViaLocations":
                                    if (tag.TagPropertyValue == "ViaLocValue")
                                    {


                                        string[] VilLocs = null;
                                        int cnt = 1;
                                        VilLocs = objMaster.Current.Booking_ViaLocations.Select(c => cnt++.ToStr() + ". " + c.ViaLocValue).ToArray();
                                        if (VilLocs.Count() > 0)
                                        {

                                            string Locations = "VIA POINT(s) : \n" + string.Join("\n", VilLocs);
                                            propertyValue = Locations;
                                        }
                                        else
                                            propertyValue = string.Empty;

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
                        if (General.SendAdvanceBookingSMS(mobileNo, ref refMsg, msg))
                        {

                            if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                            {

                                RadDesktopAlert alert = new RadDesktopAlert();
                                alert.CaptionText = "Booking saved and Confirmation Text Sent successfully!";


                                string txt = objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN ?
                                     "1st Ref # : " + objMaster.Current.BookingNo.ToStr()
                                    + Environment.NewLine + "2nd Ref # : " + objMaster.Current.BookingReturns[0].BookingNo.ToStr() : "Ref # " + objMaster.Current.BookingNo.ToStr();


                                alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>" + txt + "</span></b></html>";
                                alert.ContentImage = Resources.Resource1.email;
                                alert.Show();
                            }

                        }
                        else
                        {

                            if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                            {

                                RadDesktopAlert alert = new RadDesktopAlert();
                                alert.CaptionText = "Booking saved successfully!";


                                string txt = objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN ?
                                     "1st Ref # : " + objMaster.Current.BookingNo.ToStr()
                                    + Environment.NewLine + "2nd Ref # : " + objMaster.Current.BookingReturns[0].BookingNo.ToStr() : "Ref # " + objMaster.Current.BookingNo.ToStr();


                                alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>" + txt + "</span></b></html>";
                                alert.ContentImage = Resources.Resource1.save_Tick;
                                alert.Show();
                            }

                        }

                    }
                    else
                    {
                        if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                        {

                            RadDesktopAlert alert = new RadDesktopAlert();
                            alert.CaptionText = "Booking saved successfully!";


                            string txt = objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN ?
                                 "1st Ref # : " + objMaster.Current.BookingNo.ToStr()
                                + Environment.NewLine + "2nd Ref # : " + objMaster.Current.BookingReturns[0].BookingNo.ToStr() : "Ref # " + objMaster.Current.BookingNo.ToStr();


                            alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>" + txt + "</span></b></html>";
                            alert.ContentImage = Resources.Resource1.save_Tick;
                            alert.Show();
                        }


                    }
                }
                else
                {
                    if (IsAddMode)
                    {
                        if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                        {
                            RadDesktopAlert alert = new RadDesktopAlert();
                            alert.CaptionText = "Booking saved successfully!";




                            string txt = objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN ?
                                 "1st Ref # : " + objMaster.Current.BookingNo.ToStr()
                                + Environment.NewLine + "2nd Ref # : " + objMaster.Current.BookingReturns[0].BookingNo.ToStr() : "Ref # " + objMaster.Current.BookingNo.ToStr();


                            alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>" + txt + "</span></b></html>";
                            alert.ContentImage = Resources.Resource1.save_Tick;
                            alert.Show();
                        }





                    }

                }





                if (frm != null && frm.SuccessDespatched && frm.IsPDADriver.ToBool() == false)
                {
                    General.RefreshDriversGrids();
                }


                if (IsAddMode)
                {


                    // For jewel airports
                    //if (objMaster.Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                    //{

                    //    SendDirectPaymentEmail();
                    //}
                    //else if (objMaster.Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD)
                    //{

                    //    SendPendingPaymentEmail();
                    //}
                    //else
                    //{
                    //    SendBookingConfirmationEmail();

                    //}

                    if ((txtEmail.Text.Trim().Length > 0 || objMaster.Current.CompanyId!=null) && AppVars.listUserRights.Count(c => c.functionId == "SEND DIRECT CONFIRMATION EMAIL") > 0)
                    {

                        SendEmail(false);


                    }

                    // For PinkApple cars
                    else if (AppVars.objPolicyConfiguration.SendDirectBookingConfirmationEmail.ToBool())
                    {

                        if (chkQuotation.Checked)
                        {
                            SendBookingQuotationEmail();

                            General.AddUserLog("Quotation Saved", 4);
                        }
                        else
                        {

                            SendBookingConfirmationEmail();
                        }
                    }

                    if (objMaster.Current.JobCode.ToStr().Trim().Length > 0)
                    {


                        if (File.Exists(Application.StartupPath + "\\Configuration.xml"))
                        {

                            XmlDocument d = new XmlDocument();
                            d.Load(Application.StartupPath + "\\Configuration.xml");

                            if (d.GetElementsByTagName("ENABLEPRINTER").Count > 0)
                            {

                                PrintBookingNo(objMaster.Current.JobCode);
                            }
                          
                        }

                    }
                }
                else
                {

                    if (objMaster.Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD_PAID && oldPaymentTypeId != objMaster.Current.PaymentTypeId.ToInt())
                    {

                        SendLaterPaymentEmail();
                    }


                }


                UpdateRecentAddresses();


                if (IsAddMode == false)
                {
                    UpdateJobToDriverPDA();
                }


                IsSave = true;
                return IsSave;
            }
            catch (Exception ex)
            {
                IsSave = false;
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
                return IsSave;
            }
        }


        private void UpdateJobToDriverPDA()
        {
            try
            {

            

                if (AppVars.objPolicyConfiguration.TcpConnectionType.ToInt()==1 && objMaster.Current.DriverId != null &&
                   (objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.PENDING_START
                   || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                   || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.POB || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.STC
                   || objMaster.Current.BookingStatusId==Enums.BOOKINGSTATUS.FOJ
                   )
                      && (objMaster.Current.Fleet_Driver.Fleet_Driver_PDASettings.Count > 0 && objMaster.Current.Fleet_Driver.Fleet_Driver_PDASettings[0].CurrentPdaVersion > 15.40m)
                      && AppVars.objPolicyConfiguration.IsListenAll.ToBool() && !string.IsNullOrEmpty(AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim()))
                {


                    string paymentType = objMaster.Current.Gen_PaymentType.PaymentCategoryId == null ? objMaster.Current.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr()
                            : objMaster.Current.Gen_PaymentType.Gen_PaymentCategory.CategoryName.ToStr();


                    string journey = "O/W";


                    if (objMaster.Current.JourneyTypeId.ToInt() == 3)
                    {
                        journey = "W/R";
                    }


                    string IsExtra = (objMaster.Current.CompanyId != null || objMaster.Current.FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT || objMaster.Current.ToLocTypeId == Enums.LOCATION_TYPES.AIRPORT) ? "1" : "0";
                    int i = 1;
                    string viaP = "";



                    if (objMaster.Current.Booking_ViaLocations.Count > 0)
                    {

                        viaP = string.Join(" * ", objMaster.Current.Booking_ViaLocations.Select(c => "(" + i++.ToStr() + ")" + c.ViaLocValue.ToStr()).ToArray<string>());
                    }


                    string mobileNo = objMaster.Current.CustomerMobileNo.ToStr();
                    string telNo = objMaster.Current.CustomerPhoneNo.ToStr();



                    if (string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(telNo))
                    {
                        mobileNo = telNo;
                    }
                    else if (!string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(telNo))
                    {
                        mobileNo += "/" + telNo;
                    }


                    string pickUpPlot = "";
                    string dropOffPlot = "";
                    string companyName = string.Empty;

                    if (objMaster.Current.CompanyId != null && objMaster.Current.Gen_Company.DefaultIfEmpty().AccountTypeId.ToInt() != Enums.ACCOUNT_TYPE.CASH)
                        companyName = objMaster.Current.Gen_Company.DefaultIfEmpty().CompanyName;
                    else
                        companyName = objMaster.Current.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();


                    //error in 13.4 => if its a plot job, then pickup point is hiding in pda.

                    pickUpPlot = objMaster.Current.ZoneId != null ? "<<<" + objMaster.Current.Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr() : "";
                    dropOffPlot = objMaster.Current.DropOffZoneId != null ? "<<<" + objMaster.Current.Gen_Zone.DefaultIfEmpty().ZoneName.ToStr() : "";


                    string fromAddress = objMaster.Current.FromAddress.ToStr().Trim();
                    string toAddress = objMaster.Current.ToAddress.ToStr().Trim();

                    if (objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE || objMaster.Current.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        fromAddress = objMaster.Current.FromStreet.ToStr() + " " + objMaster.Current.FromAddress.ToStr();

                    }

                    if (objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE || objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        toAddress = objMaster.Current.ToStreet.ToStr() + " " + objMaster.Current.ToAddress.ToStr();
                    }

                    //half card and cash
                    string specialRequirements = objMaster.Current.SpecialRequirements.ToStr();
                    if (objMaster.Current.SecondaryPaymentTypeId != null && objMaster.Current.CashFares.ToDecimal() > 0)
                    {

                        specialRequirements += " , Additional Cash Payment : " + objMaster.Current.CashFares.ToDecimal();
                    }

                    decimal pdafares = objMaster.Current.GetType().GetProperty(AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().Trim()).GetValue(objMaster.Current, null).ToDecimal();





                    string showFaresValue = objMaster.Current.Gen_PaymentType.ShowFaresOnPDA.ToStr().Trim();

                    string showFares = ",\"ShowFares\":\"" + showFaresValue + "\"";
                    string showSummary = ",\"ShowSummary\":\"" + showFaresValue + "\"";


                    //string showFares = ",\"ShowFares\":\"" + objMaster.Current.Gen_PaymentType.ShowFaresOnPDA.ToStr().Trim() + "\"";
                    //string showSummary = ",\"ShowSummary\":\"" + "1" + "\"";
                

                   string agentDetails = string.Empty;
                   string parkingandWaiting = string.Empty;
                   if (objMaster.Current.CompanyId != null)
                   {
                       agentDetails = ",\"AgentFees\":\"" + String.Format("{0:0.00}", objMaster.Current.AgentCommission) + "\"";
                       parkingandWaiting = ",\"Parking\":\"" + string.Format("{0:0.00}", objMaster.Current.ParkingCharges) + "\",\"Waiting\":\"" + String.Format("{0:0.00}", objMaster.Current.WaitingCharges) + "\"";

                   }
                   else
                   {

                       parkingandWaiting = ",\"Parking\":\"" + string.Format("{0:0.00}", objMaster.Current.CongtionCharges) + "\",\"Waiting\":\"" + String.Format("{0:0.00}", objMaster.Current.MeetAndGreetCharges) + "\"";
                       //

                   }



                   string msg = "Update Job>>" + "{ \"JobId\" :\"" + objMaster.Current.Id.ToStr() +
                                          "\", \"Pickup\":\"" + (!string.IsNullOrEmpty(objMaster.Current.FromDoorNo) ? objMaster.Current.FromDoorNo + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot) +
                                          "\", \"Destination\":\"" + (!string.IsNullOrEmpty(objMaster.Current.ToDoorNo) ? objMaster.Current.ToDoorNo + "-" + toAddress + dropOffPlot : toAddress + dropOffPlot) + "\"," +
                                          "\"PickupDateTime\":\"" + string.Format("{0:dd/MM/yyyy   HH:mm}", objMaster.Current.PickupDateTime) + "\"" +
                                          ",\"Cust\":\"" + objMaster.Current.CustomerName + "\",\"Mob\":\"" + mobileNo + " " + "\",\"Fare\":\"" + string.Format("{0:0.00}", pdafares) + "\",\"Vehicle\":\"" + objMaster.Current.Fleet_VehicleType.VehicleType + "\",\"Account\":\"" + companyName + " " + "\"" +
                                            ",\"Lug\":\"" + objMaster.Current.NoofLuggages.ToInt() + "\",\"Passengers\":\"" + objMaster.Current.NoofPassengers.ToInt() + "\",\"Journey\":\"" + journey + "\",\"Payment\":\"" + paymentType + "\",\"Special\":\"" + specialRequirements + " " + "\",\"Extra\":\"" + IsExtra + "\",\"Via\":\"" + viaP + " " + "\"" +
                                         parkingandWaiting + ",\"DriverFares\":\"" + String.Format("{0:0.00}", objMaster.Current.FareRate) + "\"" +
                                         agentDetails +
                                            ",\"Did\":\"" + objMaster.Current.DriverId + "\",\"BabySeats\":\"" + objMaster.Current.BabySeats.ToStr() + "\"" + showFares + showSummary + " }";
                                




                    new Thread(delegate()
                    {
                        General.SendPDAMessage("request pda=" + objMaster.Current.DriverId + "=" + objMaster.Current.Id + "=" + msg + "=8");
                    }).Start();




                    if (AppVars.objPolicyConfiguration.DespatchOfflineJobs.ToBool())
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_SaveOfflineMessage(objMaster.Current.Id, objMaster.Current.DriverId, "", AppVars.LoginObj.LoginName.ToStr(), "Update Job>>" + objMaster.Current.DriverId + ">>" + objMaster.Current.Id + ">>" + msg + "=8");
                        }

                    }
                }
            }
            catch (Exception ex)
            {


            }


        }


        private void UpdateSetFareLog(string msg)
        {


            try
            {
                if (objMaster.Current.Id > 0 && msg.Length > 0)
                {

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_BookingLog(objMaster.Current.Id, AppVars.LoginObj.LoginName.ToStr().Trim(), msg);

                    }


                }
            }
            catch
            {

            }

        }

        private void PrintBookingNo(string JobNo)
        {
            try
            {
                rptfrmJobNo frm = null;

                if (!string.IsNullOrEmpty(JobNo))
                {

                    ReportPrintDocument rpt = null;

                    frm = new rptfrmJobNo(JobNo);

                    frm.LaodReport();
                    rpt = new ReportPrintDocument(frm.reportViewer1.LocalReport);
                    rpt.Print();
                    rpt.Dispose();
                }


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void UpdateRecentAddresses()
        {

            if (AppVars.objPolicyConfiguration.RecentAddressesFrequency.ToInt() > 0)
            {

                try
                {

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_SaveRecentAddresses("<add>" + objMaster.Current.FromAddress + "</add><add>" + objMaster.Current.ToAddress + "</add>", objMaster.Current.ToAddress, objMaster.Current.CompanyId);

                    }
                }
                catch 
                {


                }
            }
        }


        private void SendDirectPaymentEmail()
        {

            if (objMaster.PrimaryKeyValue != null && (AppVars.listUserRights.Count(c => c.functionId == "EMAIL - PC") > 0))
            {

                new Thread(delegate()
                {

                    JATEmail.SendDirectPaymentConfirmationEmail(General.GetObject<Booking>(c => c.Id == objMaster.PrimaryKeyValue.ToLong()));
                }).Start();

            }
        }



        private void SendPendingPaymentEmail()
        {

            if (objMaster.PrimaryKeyValue != null && (AppVars.listUserRights.Count(c => c.functionId == "EMAIL - PP") > 0))
            {

                new Thread(delegate()
                {

                    JATEmail.SendPaymentPendingEmail(General.GetObject<Booking>(c => c.Id == objMaster.PrimaryKeyValue.ToLong()));
                }).Start();

            }
        }

        private void SendLaterPaymentEmail()
        {

            if (objMaster.PrimaryKeyValue != null && (AppVars.listUserRights.Count(c => c.functionId == "EMAIL - PC") > 0))
            {

                new Thread(delegate()
                {

                    JATEmail.SendPaymentLaterConfirmationEmail(General.GetObject<Booking>(c => c.Id == objMaster.PrimaryKeyValue.ToLong()));
                }).Start();

            }
        }

        // For pinkapple cars
        private void SendBookingConfirmationEmail()
        {

            if (objMaster.PrimaryKeyValue != null)
            {

                new Thread(delegate()
                {

                    JATEmail.SendDirectBookingConfirmationEmail(objMaster.Current);
                }).Start();

            }
        }



        private void SendBookingQuotationEmail()
        {

            if (objMaster.PrimaryKeyValue != null)
            {

                new Thread(delegate()
                {

                    JATEmail.SendBookingQuotationEmail(objMaster.Current);
                }).Start();

            }
        }


        // For jewel airports
        //private void SendBookingConfirmationEmail()
        //{

        //    if (objMaster.PrimaryKeyValue != null && (AppVars.listUserRights.Count(c => c.functionId == "EMAIL - BC") > 0))
        //    {

        //        new Thread(delegate()
        //        {

        //            JATEmail.SendBookingConfirmationEmail(General.GetObject<Booking>(c => c.Id == objMaster.PrimaryKeyValue.ToLong()));
        //        }).Start();

        //    }
        //}












        private int? GetZoneId(string address)
        {

            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() == false)
                return null;

            if (address != "AS DIRECTED" && string.IsNullOrEmpty(General.GetPostCodeMatch(address)))
                return null;

            if (address.Contains(", UK"))
                address = address.Remove(address.LastIndexOf(", UK"));



            int? zoneId = null;

            try
            {
                if (address == "AS DIRECTED")
                {
                    zoneId = General.GetObject<Gen_Zone>(c => c.ZoneName == address).DefaultIfEmpty().Id;

                    if (zoneId == 0)
                        zoneId = null;
                }
                else
                {
                    // if (AppVars.listOfAddress.Count(c=>c.AddressLine1.Contains(address.ToStr().ToUpper()))

                    if (AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Trim().Length > 0)
                        zoneId = AppVars.listOfAddress.FirstOrDefault(c => c.AddressLine1.Contains(address.ToStr().ToUpper())).DefaultIfEmpty().ZoneId;

                    if (zoneId == null)
                    {

                        string postCode = General.GetPostCode(address);


                        if (address.Contains(",") && AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Trim().Length > 0)
                        {

                            string addr = address.Substring(0, address.LastIndexOf(',')).Trim();

                            if (addr.ToStr().Trim() != string.Empty)
                            {
                                zoneId = General.GetObject<Gen_Location>(c => c.PostCode == postCode && c.LocationName == addr).DefaultIfEmpty().ZoneId;
                            }

                        }

                        if (zoneId == null)
                        {
                            //   string postCode = General.GetPostCode(address);
                            Gen_Coordinate objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == postCode);
                            if (objCoord != null)
                            {
                                double latitude = 0, longitude = 0;

                                latitude = Convert.ToDouble(objCoord.Latitude);
                                longitude = Convert.ToDouble(objCoord.Longitude);


                                int[] plot = null;

                                if (AppVars.objPolicyConfiguration.ZoneWiseFareType.ToInt()==2)
                                {

                                    plot = (from a in General.GetQueryable<Gen_Zone>(c => (c.ShapeType != null && c.ShapeType == "circle") || (c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                                     && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude)))
                                            orderby a.PlotKind descending

                                            select a.Id).ToArray<int>();

                                }
                                else
                                {

                                     plot = (from a in General.GetQueryable<Gen_Zone>(c => (c.ShapeType != null && c.ShapeType == "circle") || (c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                                       && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude)))
                                                orderby a.PlotKind

                                                select a.Id).ToArray<int>();
                                }

                                if (plot.Count() > 0)
                                {

                                    //if (AppVars.listofVertices!=null)
                                    //{
                                    //    foreach (int plotId in plot)
                                    //    {
                                    //        if (FindPoint(latitude, longitude, AppVars.listofVertices.Where(c => c.ZoneId == plotId).ToList()))
                                    //        {
                                    //            zoneId = plotId;
                                    //            break;

                                    //        }
                                    //    }
                                    //}
                                    //else
                                    //{
                                        var list = (from p in plot
                                                    join a in General.GetQueryable<Gen_Zone_PolyVertice>(null) on p equals a.ZoneId
                                                    select a).ToList();


                                        foreach (int plotId in plot)
                                        {
                                            if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                            {
                                                zoneId = plotId;
                                                break;

                                            }
                                        }
                                 //   }
                                }
                                else
                                {

                                    if (AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Length > 0)
                                    {

                                        double distPick = Convert.ToDouble(AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal());

                                        if (distPick > 0)
                                        {

                                            string[] arr = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });

                                            if (objCoord.PostCode.ToStr().Contains(" ") && arr.Contains(objCoord.PostCode.Split(new char[] { ' ' })[0]))
                                            {
                                                var zone = (from a in General.GetQueryable<Gen_Zone_PolyVertice>(null).AsEnumerable()
                                                            select new
                                                            {

                                                                a.Gen_Zone.Id,
                                                                a.Gen_Zone.ZoneName,
                                                                DistanceMin = new LatLng(Convert.ToDouble(a.Latitude), Convert.ToDouble(a.Longitude)).DistanceMiles(new LatLng(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude))),


                                                            }).OrderBy(c => c.DistanceMin).Where(c => c.DistanceMin <= distPick).FirstOrDefault();



                                                if (zone != null)
                                                    zoneId = zone.Id;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {


            }

            return zoneId;

        }




        private int? GetOuterZoneId(string address)
        {

            if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() == false)
                return null;

            if (address != "AS DIRECTED" && string.IsNullOrEmpty(General.GetPostCodeMatch(address)))
                return null;

            if (address.Contains(", UK"))
                address = address.Remove(address.LastIndexOf(", UK"));



            int? zoneId = null;

            try
            {
                if (address == "AS DIRECTED")
                {
                    zoneId = General.GetObject<Gen_Zone>(c => c.ZoneName == address).DefaultIfEmpty().Id;

                    if (zoneId == 0)
                        zoneId = null;
                }
                else
                {
                    // if (AppVars.listOfAddress.Count(c=>c.AddressLine1.Contains(address.ToStr().ToUpper()))

                    if (AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Trim().Length > 0)
                        zoneId = AppVars.listOfAddress.FirstOrDefault(c => c.AddressLine1.Contains(address.ToStr().ToUpper())).DefaultIfEmpty().ZoneId;

                    if (zoneId == null)
                    {

                        string postCode = General.GetPostCode(address);


                        if (address.Contains(",") && AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Trim().Length > 0)
                        {

                            string addr = address.Substring(0, address.LastIndexOf(',')).Trim();

                            if (addr.ToStr().Trim() != string.Empty)
                            {
                                zoneId = General.GetObject<Gen_Location>(c => c.PostCode == postCode && c.LocationName == addr).DefaultIfEmpty().ZoneId;
                            }

                        }

                        if (zoneId == null)
                        {
                            //   string postCode = General.GetPostCode(address);
                            Gen_Coordinate objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == postCode);
                            if (objCoord != null)
                            {
                                double latitude = 0, longitude = 0;

                                latitude = Convert.ToDouble(objCoord.Latitude);
                                longitude = Convert.ToDouble(objCoord.Longitude);


                                if (AppVars.objPolicyConfiguration.ZoneWiseFareType.ToInt() == 2 || AppVars.objPolicyConfiguration.ZoneWiseFareType.ToInt() == 4)
                                {

                                    var plot = (from a in General.GetQueryable<Gen_Zone>(c => (c.ShapeType != null && c.ShapeType == "circle") || (c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                                       && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude)))
                                                orderby a.PlotKind descending

                                                select a.Id).ToArray<int>();


                                    if (plot.Count() > 0)
                                    {
                                        var list = (from p in plot
                                                    join a in General.GetQueryable<Gen_Zone_PolyVertice>(null) on p equals a.ZoneId
                                                    select a).ToList();


                                        foreach (int plotId in plot)
                                        {
                                            if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                            {
                                                zoneId = plotId;
                                                break;

                                            }
                                        }
                                    }
                                }
                                else if (AppVars.objPolicyConfiguration.ZoneWiseFareType.ToInt() == 3)
                                {


                                    var plot = (from a in General.GetQueryable<Gen_Zone>(c => (c.ShapeType != null && c.ShapeType == "circle") || (c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                                                      && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude)))
                                                orderby a.PlotKind

                                                select a.Id).ToArray<int>();


                                    if (plot.Count() > 0)
                                    {
                                        var list = (from p in plot
                                                    join a in General.GetQueryable<Gen_Zone_PolyVertice>(null) on p equals a.ZoneId
                                                    select a).ToList();


                                        foreach (int plotId in plot)
                                        {
                                            if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                            {
                                                zoneId = plotId;
                                                break;

                                            }
                                        }
                                    }


                                    //using (TaxiDataContext db = new TaxiDataContext())
                                    //{

                                    //    var plot = (from a in db.GetTable<Gen_Zone>().w(c => (c.ShapeType != null && c.ShapeType == "circle") || (c.MinLatitude != null && (latitude >= c.MinLatitude && latitude <= c.MaxLatitude)
                                    //                                  && (longitude <= c.MaxLongitude && longitude >= c.MinLongitude)))
                                    //            orderby a.PlotKind

                                    //            select a.Id).ToArray<int>();


                                    //if (plot.Count() > 0)
                                    //{
                                    //    var list = (from p in plot
                                    //                join a in db.GetTable<Gen_Zone_PolyVertice>() on p equals a.ZoneId
                                    //                select a).ToList();


                                    //    foreach (int plotId in plot)
                                    //    {
                                    //        if (FindPoint(latitude, longitude, list.Where(c => c.ZoneId == plotId).ToList()))
                                    //        {
                                    //            zoneId = plotId;
                                    //            break;

                                    //        }
                                    //    }
                                    //}
                                    //}

                                }
                                else
                                {

                                    //if (AppVars.objPolicyConfiguration.PriorityPostCodes.ToStr().Length > 0)
                                    //{

                                    //    double distPick = Convert.ToDouble(AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToDecimal());

                                    //    if (distPick > 0)
                                    //    {

                                    //        string[] arr = AppVars.objPolicyConfiguration.PriorityPostCodes.Split(new char[] { ',' });

                                    //        if (objCoord.PostCode.ToStr().Contains(" ") && arr.Contains(objCoord.PostCode.Split(new char[] { ' ' })[0]))
                                    //        {
                                    //            var zone = (from a in General.GetQueryable<Gen_Zone_PolyVertice>(null).AsEnumerable()
                                    //                        select new
                                    //                        {

                                    //                            a.Gen_Zone.Id,
                                    //                            a.Gen_Zone.ZoneName,
                                    //                            DistanceMin = new LatLng(Convert.ToDouble(a.Latitude), Convert.ToDouble(a.Longitude)).DistanceMiles(new LatLng(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude))),


                                    //                        }).OrderBy(c => c.DistanceMin).Where(c => c.DistanceMin <= distPick).FirstOrDefault();



                                    //            if (zone != null)
                                    //                zoneId = zone.Id;
                                    //        }
                                    //    }
                                    //}
                                }
                            }
                        }
                    }

                }


            }
            catch (Exception ex)
            {


            }

            return zoneId;

        }






        private bool IsDisplayingRecord = false;

        public void OnDisplayRecord(long Id)
        {

            objMaster.GetByPrimaryKey(Id);
            DisplayRecord();

        }


        private void DisplayRecord()
        {
            if (objMaster.Current == null) return;


            try
            {
                IsDisplayingRecord = true;

                int notesCnt = objMaster.Current.Booking_Notes.Count;

                if (notesCnt > 0)
                {
                    btn_notes.Text = "Notes(" + notesCnt + ")";

                    btn_notes.ButtonElement.ButtonFillElement.BackColor = Color.Red;
                    btn_notes.ButtonElement.ButtonFillElement.NumberOfColors = 1;
                    btn_notes.ButtonElement.ForeColor = Color.White;
                }
                else
                {
                    btn_notes.Text = "Notes(0)";
                }

                btnPasteBooking.Visible = false;
                btnComplaint.Visible = true;
                btnLostProperty.Visible = true;

                btnCancelBooking.Enabled = true;
                btnPrintJob.Enabled = true;

                //     btnSearch.Enabled = false;
                btnMultiBooking.Enabled = false;
                btnSms.Visible = true;
                btnConfirmationSMS.Visible = true;


                chkReverse.ToggleStateChanging -= new StateChangingEventHandler(chkReverse_ToggleStateChanging);

                chkReverse.Checked = objMaster.Current.IsReverse.ToBool();
                chkReverse.ToggleStateChanging += new StateChangingEventHandler(chkReverse_ToggleStateChanging);

                //chkHasFareMeter.Checked = objMaster.Current.EnableFareMeter.ToBool();

                //if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.POB
                //    || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.STC
                //    || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.COMPLETED)
                //{

                //    chkHasFareMeter.Enabled = false;

                //}

                
                txtTokenNo.Text = objMaster.Current.JobCode.ToStr().Trim();

                if (txtTokenNo.Text.Length > 0)
                {
                    txtTokenNo.Tag = txtTokenNo.Text.Trim();
                    chkGenerateToken.Checked = true;
                    chkGenerateToken.Enabled = false;
                    chkGenerateToken.Text = "Generated Token #";

                }

                txtVehicleNo.Text = objMaster.Current.Fleet_Master.DefaultIfEmpty().Plateno.ToStr();

                ddlSubCompany.SelectedValue = objMaster.Current.SubcompanyId.ToInt();


                
                ddlBookingType.SelectedValue = objMaster.Current.BookingTypeId;


                if (objMaster.Current.BookingTypeId != null && ddlBookingType.SelectedValue.ToInt() != objMaster.Current.BookingTypeId  )
                {
                    
                        var list =(List<BookingType>) ddlBookingType.DataSource;

                        list.Add(new BookingType { Id = objMaster.Current.BookingTypeId.ToInt(), BookingTypeName = objMaster.Current.BookingType.BookingTypeName });

                     //   ddlBookingType.Items.Add(new RadListDataItem { Text=objMaster.Current.BookingType.BookingTypeName, Value=objMaster.Current.BookingTypeId });

                        ddlBookingType.DataSource = null;

                        ComboFunctions.FillCombo<BookingType>(list, ddlBookingType, "BookingTypeName", "Id");


                        ddlBookingType.SelectedValue = objMaster.Current.BookingTypeId;
                        ddlBookingType.Enabled = false;
                    
                }



                chkQuotation.Location = new Point(chkQuotation.Location.X, chkQuotation.Location.Y + 20);
                chkQuotation.Checked = objMaster.Current.IsQuotation.ToBool();
                WasQuotiation = chkQuotation.Checked;

                txtBookingNo.Text = objMaster.Current.BookingNo.ToStr();

                this.Text = "Ref # " + txtBookingNo.Text;

                ddlFromLocType.SelectedValue = objMaster.Current.FromLocTypeId;
                ddlToLocType.SelectedValue = objMaster.Current.ToLocTypeId;

                DetachLocationsSelectionEvent(ddlFromLocation);
                ddlFromLocation.SelectedValue = objMaster.Current.FromLocId;
                AttachLocationSelectionEvent(ddlFromLocation);

                DetachLocationsSelectionEvent(ddlToLocation);
                ddlToLocation.SelectedValue = objMaster.Current.ToLocId;
                AttachLocationSelectionEvent(ddlToLocation);

                ddlVehicleType.SelectedValue = objMaster.Current.VehicleTypeId;


                ddlCustomerName.Text = objMaster.Current.CustomerName;
                txtCustomerMobileNo.Text = objMaster.Current.CustomerMobileNo;
                txtCustomerPhoneNo.Text = objMaster.Current.CustomerPhoneNo;
                txtEmail.Text = objMaster.Current.CustomerEmail.ToStr().Trim();
                numCustomerFares.Value = objMaster.Current.CustomerPrice.ToDecimal();

                txtSpecialRequirements.Text = objMaster.Current.SpecialRequirements;


                int journeyTypeId = objMaster.Current.JourneyTypeId.ToInt();

                opt_JOneWay.ToggleStateChanging -= opt_JOneWay_ToggleStateChanging;


                if (journeyTypeId == Enums.JOURNEY_TYPES.ONEWAY)
                    opt_JOneWay.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                else if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN)
                {
                    opt_JReturnWay.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;

                    label1.Text = " Booking(Return)";



                }
                else if (journeyTypeId == Enums.JOURNEY_TYPES.WAITANDRETURN)
                {

                    opt_WaitandReturn.ToggleState = ToggleState.On;
                }

                if (ddlReturnFromAirport != null)
                    ddlReturnFromAirport.SelectedValue = objMaster.Current.ReturnFromLocId;



                if (objMaster.Current.MasterJobId != null)
                {
                    pnlReturnJobNo.Visible = true;
                    pnlReturnJobNo.Text = "Return From Job # " + objMaster.Current.Booking1.DefaultIfEmpty().BookingNo.ToStr();



                    if (objMaster.Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.WEB && objMaster.Current.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {

                        lblToDoorFlightNo.Text = "Flight No";

                    }


                }

                opt_JOneWay.ToggleStateChanging += new StateChangingEventHandler(opt_JOneWay_ToggleStateChanging);

                if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.CANCELLED)
                {
                    btnCancelBooking.Text = "Cancel Notes";
                    //   IsCancel = true;

                }

                chkIsCompanyRates.Checked = objMaster.Current.IsCompanyWise.ToBool();

                if (objMaster.Current.CompanyId != null && objMaster.Current.Gen_Company.IsClosed.ToBool())
                {

                    var data = (List<Gen_Company>)ddlCompany.DataSource;
                    data.Add(objMaster.Current.Gen_Company);
                    ddlCompany.SelectedValueChanged -= new EventHandler(ddlCompany_SelectedValueChanged);
                    ComboFunctions.FillCompanyCombo(ddlCompany, data);
                    ddlCompany.SelectedValueChanged += new EventHandler(ddlCompany_SelectedValueChanged);
                }


                ddlCompany.SelectedValue = objMaster.Current.CompanyId;

                ddlPaymentType.SelectedValue = objMaster.Current.PaymentTypeId;

                if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD
                    && !string.IsNullOrEmpty(objMaster.Current.BookingPayment.DefaultIfEmpty().AuthCode))
                {
                    btnPayment.Enabled = false;
                    lblPaymentHeading.Text += "(Payment AuthCode is : " + objMaster.Current.BookingPayment.DefaultIfEmpty().AuthCode + ")";
                    ddlPaymentType.Enabled = false;
                }



                //txtCustomerCreditCardNo.Text = objMaster.Current.CustomerCreditCardDetails.ToStr().Trim();
                //txtCompanyCreditCardNo.Text = objMaster.Current.CompanyCreditCardDetails.ToStr().Trim();


                // Added on 18/08/2015 Danish (For Half Card and Half Cash)
                if (chkSecondaryPaymentType != null)
                {
                    if (objMaster.Current.SecondaryPaymentTypeId != null)
                    {
                        chkSecondaryPaymentType.Checked = true;
                        numCashPaymentFares.Value = objMaster.Current.CashFares.ToDecimal();
                    }
                }
                //




                if (pnlOrderNo != null)
                {

                    txtOrderNo.Text = objMaster.Current.OrderNo.ToStr();
                    txtPupilNo.Text = objMaster.Current.PupilNo.ToStr();

                }

                if (txtAccountBookedBy != null)
                {
                    txtAccountBookedBy.Text = objMaster.Current.BookedBy.ToStr().Trim();
                }


                if (chkTakenByAgent != null)
                {

                    chkTakenByAgent.Checked = objMaster.Current.JobTakenByCompany.ToBool();
                    numAgentCommissionPercent.Value = objMaster.Current.AgentCommissionPercent.ToInt();
                    ddlAgentCommissionType.SelectedIndex = objMaster.Current.FromFlightNo.ToStr().Trim() == "Percent" ? 0 : 1;
                    numAgentCommission.Value = objMaster.Current.AgentCommission.ToDecimal();
                }

                if (numJourneyTime != null)
                {
                   numJourneyTime.Value= objMaster.Current.JourneyTimeInMins.ToDecimal();

                }

                if (ddlDepartment != null)
                    ddlDepartment.SelectedValue = objMaster.Current.DepartmentId;


                if (ddlEscort != null)
                {
                    ddlEscort.SelectedValue = objMaster.Current.EscortId;

                    numEscortPrice.Value = objMaster.Current.EscortPrice.ToDecimal();

                }
                dtpPickupDate.Value = objMaster.Current.PickupDateTime.ToDate();
                dtpPickupTime.Value = objMaster.Current.PickupDateTime;

                if (dtpReturnPickupDate != null)
                {
                    dtpReturnPickupDate.Value = objMaster.Current.ReturnPickupDateTime.ToDateorNull();
                    dtpReturnPickupTime.Value = objMaster.Current.ReturnPickupDateTime.ToDateTimeorNull();
                    ddlReturnDriver.SelectedValue = objMaster.Current.ReturnDriverId;
                }


                num_TotalPassengers.Value = objMaster.Current.NoofPassengers.ToDecimal();
                numTotalLuggages.Value = objMaster.Current.NoofLuggages.ToDecimal();

                numFareRate.Value = objMaster.Current.FareRate.ToDecimal();

                if (numReturnFare != null)
                    numReturnFare.Value = objMaster.Current.ReturnFareRate.ToDecimal();

                if (numCompanyFares != null)
                {
                    numCompanyFares.Value = objMaster.Current.CompanyPrice.ToDecimal();

                    if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN)
                    {
                        numReturnCompanyFares.Value = objMaster.Current.WaitingMins.ToDecimal();

                    }
                }

                numParkingChrgs.Value = objMaster.Current.ParkingCharges.ToDecimal();
                numWaitingChrgs.Value = objMaster.Current.WaitingCharges.ToDecimal();
                numExtraChrgs.Value = objMaster.Current.ExtraDropCharges.ToDecimal();
                numMeetCharges.Value = objMaster.Current.MeetAndGreetCharges.ToDecimal();
                numCongChrgs.Value = objMaster.Current.CongtionCharges.ToDecimal();

                numTotalChrgs.Value = objMaster.Current.TotalCharges.ToDecimal();

              //  numDrvWaitingMins.Enabled = true;

                numDrvWaitingMins.Value = objMaster.Current.DriverWaitingMins.ToDecimal();


                if (pnlComcab != null)
                {
                    numComcab_Cash.Value = objMaster.Current.CashRate.ToDecimal();
                    numComcab_Account.Value = objMaster.Current.AccountRate.ToDecimal();
                    numComcab_ExtraMile.Value = objMaster.Current.ExtraMile.ToDecimal();
                    numComcab_WaitingMin.Value = objMaster.Current.WaitingMins.ToDecimal();
                }


                txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = objMaster.Current.FromAddress.ToStr();
                txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                txtFromFlightDoorNo.Text = objMaster.Current.FromDoorNo.ToStr();
                txtFromStreetComing.Text = objMaster.Current.FromStreet.ToStr();

                txtFromPostCode.Text = objMaster.Current.FromPostCode.ToStr();

                txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = objMaster.Current.ToAddress.ToStr();
                txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                txtToFlightDoorNo.Text = objMaster.Current.ToDoorNo.ToStr();
                txtToStreetComing.Text = objMaster.Current.ToStreet.ToStr();

                txtToPostCode.Text = objMaster.Current.ToPostCode.ToStr();



                chkIsCommissionWise.Checked = objMaster.Current.IsCommissionWise.ToBool();
                ddlCommissionType.SelectedValue = objMaster.Current.DriverCommissionType.ToStr().Trim();
                numDriverCommission.Value = objMaster.Current.DriverCommission.ToDecimal();

                chkAutoDespatch.Checked = objMaster.Current.AutoDespatch.ToBool();
                chkBidding.Checked = objMaster.Current.IsBidding.ToBool();

                DateTime? pickUpDate = objMaster.Current.PickupDateTime;


                if (objMaster.Current.AutoDespatchTime != null)
                {
                    DateTime? autoDespatchDate = objMaster.Current.AutoDespatchTime;
                    int mins = pickUpDate.Value.TimeOfDay.Subtract(autoDespatchDate.Value.TimeOfDay).Minutes.ToInt();
                    numBeforeMinutes.Value = mins < 0 ? 10 : mins;
                }
                else
                    numBeforeMinutes.Value = 10;

                //    ShowAutoDespatchLabels(AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool());


                if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED
                    || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.CANCELLED
                    || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.NOPICKUP)
                {
                    chkDisableDriverSMS.Enabled = false;
                    chkDisablePassengerSMS.Enabled = false;

                    chkQuotation.Enabled = false;


                    //if (AppVars.listUserRights.Count(c => c.formName == "frmBooking" &&
                    //    (c.functionId == "LOCK COMPLETED BOOKING" || c.functionId == "LOCK CANCELLED BOOKING"
                    //    || c.functionId == "LOCK NOFARE BOOKING")) > 0)
                    //{


                    //    btnSaveNew.Enabled = false;
                    //    btnCancelBooking.Enabled = false;
                    //    btnMultiVehicle.Enabled = false;

                    //}


                    if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED)
                    {
                        if (AppVars.listUserRights.Count(c => c.formName == "frmBooking" && (c.functionId == "LOCK COMPLETED BOOKING")) > 0)
                        {

                            btnSaveNew.Enabled = false;
                            btnCancelBooking.Enabled = false;
                            btnMultiVehicle.Enabled = false;

                        }



                    }
                    else if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.CANCELLED)
                    {
                        if (AppVars.listUserRights.Count(c => c.formName == "frmBooking" && (c.functionId == "LOCK CANCELLED BOOKING")) > 0)
                        {

                            btnSaveNew.Enabled = false;
                            btnCancelBooking.Enabled = false;
                            btnMultiVehicle.Enabled = false;

                        }

                    }
                    else if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.NOPICKUP)
                    {
                        if (AppVars.listUserRights.Count(c => c.formName == "frmBooking" && (c.functionId == "LOCK NOFARE BOOKING")) > 0)
                        {

                            btnSaveNew.Enabled = false;
                            btnCancelBooking.Enabled = false;
                            btnMultiVehicle.Enabled = false;

                        }

                    }


                    if (objMaster.Current.Fleet_DriverCommision_Charges.Count > 0)
                    {
                        btnSaveNew.Enabled = false;
                        btnCancelBooking.Enabled = false;
                        btnMultiVehicle.Enabled = false;
                        chkIsCompanyRates.Enabled = false;
                        ddlCompany.Enabled = false;

                    }

                }




                int fromLocTypeId = objMaster.Current.FromLocTypeId.ToInt();

                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                    txtFromAddress.Focus();
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    txtFromPostCode.Focus();
                else
                    ddlFromLocation.Focus();


                chkDisableDriverSMS.Checked = objMaster.Current.DisableDriverSMS.ToBool();
                chkDisablePassengerSMS.Checked = objMaster.Current.DisablePassengerSMS.ToBool();



                if (chkAutoDespatch.Enabled)
                {
                    ddlPickupPlot.SelectedValue = objMaster.Current.ZoneId.ToInt();
                    ddlDropOffPlot.SelectedValue = objMaster.Current.DropOffZoneId.ToInt();
                    // txtPickupPlot.Text = objMaster.Current.Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr();
                    // txtDropOffZone.Text = objMaster.Current.Gen_Zone.DefaultIfEmpty().ZoneName.ToStr();

                }

                if (objMaster.Current.PartyId != null && objMaster.Current.TransferJobId != null)
                {
                    lblBookedBy.Text = "Job Received from : " + objMaster.Current.Gen_Party.CompanyName.ToStr() + " on " + string.Format("{0:dd/MM/yyyy HH:mm}", objMaster.Current.AddOn.ToDateTime());
                    ddlBookingType.Enabled = false;
                }
                else
                {
                    lblBookedBy.Text = "Job booked by : " + objMaster.Current.AddLog.ToStr() + " on " + string.Format("{0:dd/MM/yyyy HH:mm}", objMaster.Current.AddOn.ToDateTime());


                }

                if (!string.IsNullOrEmpty(objMaster.Current.EditLog))
                {
                    lblBookedBy.Text += " , Edit by : " + objMaster.Current.EditLog.ToStr() + " on " + string.Format("{0:dd/MM/yyyy HH:mm}", objMaster.Current.EditOn.ToDateTime());
                }


                if (!string.IsNullOrEmpty(objMaster.Current.Despatchby))
                {
                    lblBookedBy.Text += " , Despatched by : " + objMaster.Current.Despatchby.ToStr() + " on " + string.Format("{0:dd/MM/yyyy HH:mm}", objMaster.Current.DespatchDateTime.ToDateTime());
                }



                // Shuttle Job Working

                //if (objMaster.Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.SHUTTLE)
                //{
                //    //InitializeGroupJobPanel();

                //    if (txtGroupJobNo != null)
                //    {

                //        dtpFlightDepDate.Value = objMaster.Current.FlightDepartureDate.ToDateorNull();
                //        dtpFlightDepTime.Value = objMaster.Current.FlightDepartureDate.ToDateTimeorNull();
                //        txtRoomNo.Text = objMaster.Current.RoomNo.ToStr();

                //        if (objMaster.Current.GroupJobId != null)
                //        {
                //            txtGroupJobNo.Tag = objMaster.Current.GroupJobId;
                //            txtGroupJobNo.Text = objMaster.Current.BookingGroup.GroupName.ToStr();


                //            LockUnLockShuttleGroupDetails(true);

                //        }
                //    }
                //}


                if (ddlDirection != null)
                    ddlDirection.Text = objMaster.Current.BoundType.ToStr().Trim();

                if (objMaster.Current.FaresPostedFrom.ToStr().Trim().Length == 0 && objMaster.Current.CompanyId!=null && objMaster.Current.AccountRate.ToDecimal() > 0)
                {
                    txtFaresPostedFrom.Text = "Mileage " + objMaster.Current.AccountRate.ToDecimal();
                }
                else
                {

                    txtFaresPostedFrom.Text = objMaster.Current.FaresPostedFrom.ToStr();
                }
                if (!string.IsNullOrEmpty(txtFaresPostedFrom.Text))
                    txtFaresPostedFrom.Visible = true;



                if (ddlBabyseat1 != null && ddlbabyseat2 != null)
                {
                    string babyseats = objMaster.Current.BabySeats.ToStr();
                    if (!string.IsNullOrEmpty(babyseats) && babyseats.Contains("<<<"))
                    {

                        string[] arr = babyseats.Split(new string[] { "<<<" }, StringSplitOptions.None);

                        if (arr.Count() == 2)
                        {
                            ddlBabyseat1.SelectedItem = arr[0].ToStr().Trim();
                            ddlbabyseat2.SelectedItem = arr[1].ToStr().Trim();

                        }
                    }
                }

                DisplayBooking_ViaLocations();






                //th = new System.Threading.Thread(new ThreadStart(DisplayBooking_Map));
                //th.IsBackground = true;
                //th.Start();

                if (txtReturnSpecialReq != null)
                {
                    if (objMaster.Current.BookingReturns.Count > 0)
                    {
                        txtReturnSpecialReq.Text = objMaster.Current.BookingReturns[0].DefaultIfEmpty().SpecialRequirements.ToStr();

                        if (ddlReturnVehicleType != null)
                            ddlReturnVehicleType.SelectedValue = objMaster.Current.BookingReturns[0].DefaultIfEmpty().VehicleTypeId;



                        if (objMaster.Current.BookingReturns[0].ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS
                            && objMaster.Current.BookingReturns[0].ToAddress.ToStr().Trim().ToLower() != txtFromAddress.Text.Trim().ToStr().ToLower())
                        {

                            SetReturnTo(ToggleState.Off);

                            txtReturnTo.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                            txtReturnTo.Text = objMaster.Current.BookingReturns[0].ToAddress.ToStr().Trim();
                            txtReturnTo.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                        }
                        else if (objMaster.Current.BookingReturns[0].ToLocTypeId.ToInt() != Enums.LOCATION_TYPES.ADDRESS && objMaster.Current.BookingReturns[0].ToLocId != ddlFromLocation.SelectedValue.ToIntorNull())
                        {

                            SetReturnTo(ToggleState.On);
                            ddlReturnTo.SelectedValue = objMaster.Current.BookingReturns[0].ToLocId;
                        }




                        if (objMaster.Current.BookingReturns[0].FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS
                         && objMaster.Current.BookingReturns[0].FromAddress.ToStr().Trim().ToLower() != txtToAddress.Text.Trim().ToStr().ToLower())
                        {

                            SetReturnFrom(ToggleState.Off);

                            txtReturnFrom.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                            txtReturnFrom.Text = objMaster.Current.BookingReturns[0].FromAddress.ToStr().Trim();
                            txtReturnFrom.TextChanged += new EventHandler(TextBoxElement_TextChanged);
                        }
                        else if (objMaster.Current.BookingReturns[0].FromLocTypeId.ToInt() != Enums.LOCATION_TYPES.ADDRESS && objMaster.Current.BookingReturns[0].FromLocId != ddlToLocation.SelectedValue.ToIntorNull())
                        {

                            SetReturnFrom(ToggleState.On);
                            if (ddlReturnFromAirport != null)
                                ddlReturnFromAirport.SelectedValue = objMaster.Current.BookingReturns[0].FromLocId;
                        }


                        numReturnCustFare.Value = objMaster.Current.BookingReturns[0].CustomerPrice.ToDecimal();

                    }
                }





                 

                if (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED)
                {

                    btnSendInvoice.Visible = true;
                    btnDespatchView.Visible = false;

                    ddlDriver.Enabled = false;
                    if (ddlReturnDriver != null)
                        ddlReturnDriver.Enabled = false;

                    if (objMaster.Current.IsProcessed.ToBool())
                    {

                       


                        ddlVehicleType.Enabled = false;
                        if (ddlReturnVehicleType != null)
                            ddlReturnVehicleType.Enabled = false;

                        dtpPickupDate.Enabled = false;
                        dtpPickupTime.Enabled = false;


                        if (objMaster.Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                        {
                            dtpReturnPickupDate.Enabled = false;
                            dtpReturnPickupTime.Enabled = false;

                        }

                        numFareRate.Enabled = false;

                        if (numCompanyFares != null)
                            numCompanyFares.Enabled = false;
                        numCustomerFares.Enabled = false;
                        numParkingChrgs.Enabled = false;
                        numWaitingChrgs.Enabled = false;
                        numExtraChrgs.Enabled = false;
                        numMeetCharges.Enabled = false;
                        numCongChrgs.Enabled = false;

                        txtFromAddress.ReadOnly = false;
                        txtToAddress.ReadOnly = false;
                        ddlFromLocation.Enabled = false;

                        btnSaveNew.Enabled = false;
                        btnCancelBooking.Enabled = false;
                    }
                    else
                    {
                        if (AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool())
                        {

                            btnSaveAndReady.Visible = true;
                        }


                    }
                }



                if (this.openedFrom == 1)
                {
                    btnMultiBooking.Visible = false;
                    btnMultiVehicle.Visible = false;
                    ddlDriver.Enabled = false;
                    opt_JOneWay.Enabled = false;
                    opt_JReturnWay.Enabled = false;
                    opt_WaitandReturn.Enabled = false;

                    chkQuotation.Enabled = false;
                }


                if (objMaster.Current.DriverId != null &&
                    (objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                    || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.POB || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.STC
                    || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING_START))
                {
                    ddlDriver.Enabled = false;

                    if (objMaster.Current.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.PENDING && objMaster.Current.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.PENDING_START)
                        btnTrackDriver.Visible = true;
                }

                //    DisableAccountCheck();

                numTipAmount.Value = objMaster.Current.TipAmount.ToDecimal();


                // Enable for elite cars

                if (objMaster.Current.DriverId != null && objMaster.Current.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.WAITING && AppVars.objPolicyConfiguration.DefaultClientId.ToStr()=="Elt.Cbs")
                {
                    lblTip.Visible = true;
                    numTipAmount.Visible = true;
                }
          


                chkIsCommissionWise.Enabled = !AppVars.objPolicyConfiguration.DisableDriverCommissionTick.ToBool();
                txtPaymentReference.Text = objMaster.Current.PaymentComments.ToStr().Trim();
                chkQuotedPrice.Checked = objMaster.Current.IsQuotedPrice.ToBool();


                if (objMaster.Current.CallRefNo.ToStr().Trim().Length > 0 && AppVars.listUserRights.Count(c => c.functionId == "SHOW RECORDING") > 0)
                {
                    btnPlayRecording.Visible = true;
                    this.CallRefNo = objMaster.Current.CallRefNo.ToStr().Trim();
                }



                IsDisplayingRecord = false;

                DisplayCourierSignature();


                if (objMaster.Current.ToAddress.ToStr().Trim() == "AS DIRECTED")
                {

                    InitializeJourneyTimePanel();
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
                IsDisplayingRecord = false;

            }

        }


        private string pathwav;

        public void GetRecordingFile(string path, DateTime StartPoint, DateTime EndPoint, string Number)
        {
            try
            {
                DataSet dS = null;
                XmlReader xmlFile = null;
                string number = string.Empty;
                 
                var readfiles = new DirectoryInfo(path).GetFiles("*.xml",
                         SearchOption.AllDirectories).Where(d => d.LastWriteTime.ToLocalTime() <= EndPoint.ToLocalTime() && d.LastWriteTime >= StartPoint.ToLocalTime());

                foreach (var item in readfiles)
                {

                    xmlFile = XmlReader.Create(item.FullName, new XmlReaderSettings());
                    dS = new DataSet();
                  
                    dS.ReadXml(xmlFile);
                    number = dS.Tables["Party"].Rows[0]["number"].ToString();
                    if (number.Length == 3)
                    {
                        number = dS.Tables["Party"].Rows[1]["number"].ToString();

                    }


                    if (number == Number && number.Length >= 9)
                    {
                        btnPlayRecording.Visible = true;

                        pathwav = item.FullName.Replace(".xml", ".wav");
                      

                    }


                }

                GC.Collect();
            }


            catch (Exception ex)
            {
            }


        }



        private void DisplayCourierSignature()
        {


            if (objMaster.Current.Booking_CourierSignatures != null && objMaster.Current.Booking_CourierSignatures.CourierSignature.Length>1)
            {


                if (objMaster.Current.Booking_CourierSignatures.CourierSignature != null)
                {
                    InitializeCourierPanel();


                    pic_Signature.Image = General.byteArrayToImage(objMaster.Current.Booking_CourierSignatures.CourierSignature.ToArray());


                    if (objMaster.Current.Booking_CourierSignatures.SignatureDateTime != null)
                        txtCourierSignedOn.Text = "Signed on :" + string.Format("{0:dd/MM/yyyy HH:mm}", objMaster.Current.Booking_CourierSignatures.SignatureDateTime);

                }

                //if (txtCourierSignaturedBy != null)
                //{

                //    txtCourierSignaturedBy.Text = objMaster.Current.Booking_CourierSignatures.SignaturedBy.ToStr();
                //    dtpCourierSignatureDate.Value = objMaster.Current.Booking_CourierSignatures.SignatureDateTime.ToDateorNull();
                //    dtpCourierSignatureTime.Value = objMaster.Current.Booking_CourierSignatures.SignatureDateTime.ToDateTimeorNull();
                //    numCourierItems.Value = objMaster.Current.Booking_CourierSignatures.NoOfItems.ToDecimal();
                //}

               
            }

        }



        private void InitializeCourierPanel()
        {

            this.pic_Signature = new System.Windows.Forms.PictureBox();
            this.lblCourierHeader = new System.Windows.Forms.Label();
            this.txtCourierSignedOn = new System.Windows.Forms.Label();


            this.pnlMain.Controls.Add(this.txtCourierSignedOn);
            this.pnlMain.Controls.Add(this.lblCourierHeader);
            this.pnlMain.Controls.Add(this.pic_Signature);


            ((System.ComponentModel.ISupportInitialize)(this.pic_Signature)).BeginInit();
            // 
            // pic_Signature
            // 
            this.pic_Signature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Signature.Location = new System.Drawing.Point(929, 370);
            this.pic_Signature.Name = "pic_Signature";
            this.pic_Signature.Size = new System.Drawing.Size(278, 151);
            this.pic_Signature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Signature.TabIndex = 266;
            this.pic_Signature.TabStop = false;
            // 
            // lblCourierHeader
            // 
            this.lblCourierHeader.BackColor = System.Drawing.Color.Crimson;
            this.lblCourierHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCourierHeader.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCourierHeader.ForeColor = System.Drawing.Color.White;
            this.lblCourierHeader.Location = new System.Drawing.Point(929, 349);
            this.lblCourierHeader.Name = "lblCourierHeader";
            this.lblCourierHeader.Size = new System.Drawing.Size(278, 22);
            this.lblCourierHeader.TabIndex = 267;
            this.lblCourierHeader.Text = "Customer Signature";
            // 
            // txtCourierSignedOn
            // 
            this.txtCourierSignedOn.BackColor = System.Drawing.Color.Black;
            this.txtCourierSignedOn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCourierSignedOn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCourierSignedOn.ForeColor = System.Drawing.Color.White;
            this.txtCourierSignedOn.Location = new System.Drawing.Point(929, 521);
            this.txtCourierSignedOn.Name = "txtCourierSignedOn";
            this.txtCourierSignedOn.Size = new System.Drawing.Size(278, 22);
            this.txtCourierSignedOn.TabIndex = 268;
            this.txtCourierSignedOn.Text = "Signed On :";
            this.txtCourierSignedOn.TextAlign = System.Drawing.ContentAlignment.TopCenter;


            ((System.ComponentModel.ISupportInitialize)(this.pic_Signature)).EndInit();
        }



        delegate void DisplayMilesHandler();


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
                    //   row.Cells["FROMTYPELABEL"].Value = "Via";
                    row.Cells["FROMTYPELABEL"].Value = item.ViaLocTypeLabel;
                    row.Cells["FROMTYPEVALUE"].Value = item.ViaLocTypeValue;
                    row.Cells["FROMVIALOCTYPEID"].Value = item.ViaLocTypeId;

                    row.Cells["VIALOCATIONID"].Value = item.ViaLocId;
                    row.Cells["VIALOCATIONLABEL"].Value = item.ViaLocLabel;
                    row.Cells["VIALOCATIONVALUE"].Value = item.ViaLocValue;

                }

                ClearViaDetails();


                btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                btnSelectVia.ButtonElement.ButtonFillElement.BackColor = Color.DarkOrange;
                btnSelectVia.ButtonElement.ButtonFillElement.NumberOfColors = 1;

            }

        }


        #endregion

        private void opt_JOneWay_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {

            if (CheckReturnBooking(args.NewValue) == false)
                args.Canceled = true;

            FocusToPassenger();
        }

        private void FocusToPassenger()
        {

            //num_TotalPassengers.Focus();
            num_TotalPassengers.Select();
        }




        private bool CheckReturnBooking(ToggleState toggle)
        {
            bool rtn = true;

            try
            {
                if (objMaster.PrimaryKeyValue != null && objMaster.Current.BookingReturns.Count > 0 && toggle == ToggleState.On)
                {
                    if (DialogResult.OK == MessageBox.Show("There is a Return Job '" + objMaster.Current.BookingReturns.FirstOrDefault().DefaultIfEmpty().BookingNo
                                                        + "' exist against this Job" + Environment.NewLine
                                                        + "If you press OK then Return Job will be delete" + Environment.NewLine
                                                        + "Are you sure you want to Delete its Return Job?", "Booking and Dispatch System"
                                                            , MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                    {

                        new TaxiDataContext().stp_DeleteFullReturnBooking(objMaster.Current.BookingReturns.FirstOrDefault().DefaultIfEmpty().MasterJobId, true);

                        //  new TaxiDataContext().stp_DeleteReturnBooking(objMaster.Current.BookingReturns.FirstOrDefault().DefaultIfEmpty().MasterJobId);

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

                if (dtpReturnPickupTime != null)
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


                    lblReturnCustFare.Visible = false;
                    numReturnCustFare.Visible = false;

                    if (txtReturnSpecialReq != null)
                    {
                        lblReturnSpecialReq.Visible = false;
                        txtReturnSpecialReq.Visible = false;
                    }


                    if (ddlReturnVehicleType != null)
                    {
                        lblReturnVehicle.Visible = false;
                        ddlReturnVehicleType.Visible = false;
                    }


                    if (lblRetFares != null)
                    {
                        lblRetFares.Visible = false;
                        numReturnFare.Visible = false;


                    }


                }

                if (lblRetFares != null)
                {
                    numReturnFare.Enabled = false;
                    numReturnFare.Value = 0;
                }

                if (numReturnCompanyFares != null)
                {
                    numReturnCompanyFares.Enabled = false;

                }


                btn_notes.Location = new Point(749, 282);
                pnlAutoDespatch.Location = new Point(749, 332);



                ddlPickupPlot.Visible = true;
                ddlDropOffPlot.Visible = true;
                lblPickupPlot.Text = "Pickup Plot";
                lblDropOffPlot.Text = "Drop Off Plot";
                btnReturnFrom.Visible = false;
                btnReturnTo.Visible = false;

                if (txtReturnFrom != null)
                    txtReturnFrom.Visible = false;

                if (txtReturnTo != null)
                    txtReturnTo.Visible = false;

            }
            else
            {
                InitializeReturnPanel();


                ddlPickupPlot.Visible = false;
                ddlDropOffPlot.Visible = false;
                lblPickupPlot.Text = "Return To";
                lblDropOffPlot.Text = "Return From";
                btnReturnFrom.Visible = true;
                btnReturnTo.Visible = true;

                lblReturnDriver.Visible = true;
                ddlReturnDriver.Visible = true;
                ddlReturnDriver.SelectedValue = null;

                lblReturnPickupDate.Visible = true;
                lblReturnPickupTime.Visible = true;

                dtpReturnPickupDate.Visible = true;
                dtpReturnPickupTime.Visible = true;


                lblReturnCustFare.Visible = true;
                numReturnCustFare.Visible = true;



                if (AppVars.objPolicyConfiguration.ShowBlankPickupDateAsDefault.ToBool() == false)
                {
                    dtpReturnPickupDate.Value = dtpPickupDate.Value.ToDate();
                    dtpReturnPickupTime.Value = DateTime.Now;
                }


                if (lblRetFares != null)
                {
                    numReturnFare.Enabled = true;


                    lblRetFares.Visible = true;
                    numReturnFare.Visible = true;



                }
                if (numReturnCompanyFares != null)
                {
                    numReturnCompanyFares.Enabled = true;

                }

                btn_notes.Location = new Point(749, 222);
                pnlAutoDespatch.Location = new Point(749, 282);


                if (txtReturnSpecialReq != null)
                {

                    lblReturnSpecialReq.Visible = true;
                    txtReturnSpecialReq.Visible = true;
                }


                if (ddlReturnVehicleType != null)
                {
                    if (ddlReturnVehicleType.DataSource == null)
                        ComboFunctions.FillVehicleTypeCombo(ddlReturnVehicleType);


                    if (ddlReturnVehicleType.SelectedValue == null)
                        ddlReturnVehicleType.SelectedValue = ddlVehicleType.SelectedValue;

                    lblReturnVehicle.Visible = true;
                    ddlReturnVehicleType.Visible = true;

                }


                if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {

                    if (ddlReturnFromAirport != null)
                    {

                        SetReturnFrom(ToggleState.On);
                        //lblReturnFromAirport.Visible = true;
                        ddlReturnFromAirport.Visible = true;
                        if (ddlReturnFromAirport.DataSource == null)
                        {
                            ComboFunctions.FillLocationsCombo(ddlReturnFromAirport, c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT);
                            ddlReturnFromAirport.SelectedIndex = -1;
                        }

                    }

                }
                else
                {
                    SetReturnFrom(ToggleState.Off);

                }

                if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {

                    SetReturnTo(ToggleState.On);

                    if (ddlReturnTo.DataSource == null)
                    {
                        ComboFunctions.FillLocationsCombo(ddlReturnTo, c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT);
                        ddlReturnTo.SelectedIndex = -1;
                    }


                }
                else
                {

                    SetReturnTo(ToggleState.Off);
                }

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
                if (ddlCompany.DataSource == null)
                {

                    InitializeCompanyPrice();

                    ComboFunctions.FillCompanyCombo(ddlCompany);
                    ddlCompany.SelectedValueChanged += new EventHandler(ddlCompany_SelectedValueChanged);


                }

             //   SetAccountPaymentType();

                if (numCompanyFares != null)
                    numCompanyFares.Enabled = true;
            }
            else
            {
              //  SetCashPaymentType();

                if (pnlOrderNo != null)
                {
                    txtOrderNo.Text = string.Empty;
                    txtPupilNo.Text = string.Empty;
                    pnlOrderNo.Visible = false;
                }

                if (numCompanyFares != null)
                {
                    numCompanyFares.Value = 0;
                    numCompanyFares.Enabled = false;
                }


                if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT)
                    ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;


                ShowAgentDetails(false);
                ShowPaymentReference();

            
            }


            ddlCompany.Enabled = toggle == ToggleState.On;
            ddlCompany.SelectedValue = toggle == ToggleState.Off ? null : ddlCompany.SelectedValue;



        }

        private void InitializeCompanyPrice()
        {
            if (lblCompanyPrice != null)
                return;

            try
            {

                this.lblCompanyPrice = new Telerik.WinControls.UI.RadLabel();
                this.numCompanyFares = new Telerik.WinControls.UI.RadSpinEditor();
                ((System.ComponentModel.ISupportInitialize)(this.lblCompanyPrice)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numCompanyFares)).BeginInit();


                // 
                // lblCompanyPrice
                // 
                this.lblCompanyPrice.AutoSize = false;
                this.lblCompanyPrice.BackColor = System.Drawing.Color.Orange;
                this.lblCompanyPrice.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
                this.lblCompanyPrice.ForeColor = System.Drawing.Color.Black;
                this.lblCompanyPrice.Location = new System.Drawing.Point(716, 63);
                this.lblCompanyPrice.Name = "lblCompanyPrice";
                // 
                // 
                // 
                this.lblCompanyPrice.RootElement.ForeColor = System.Drawing.Color.Black;
                this.lblCompanyPrice.Size = new System.Drawing.Size(135, 19);
                this.lblCompanyPrice.TabIndex = 242;
                this.lblCompanyPrice.Text = "Company Price  £";
                this.lblCompanyPrice.Visible = true;
                // 
                // numCompanyFares
                // 
                this.numCompanyFares.DecimalPlaces = 2;
                this.numCompanyFares.EnableKeyMap = true;
                this.numCompanyFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numCompanyFares.ForeColor = System.Drawing.Color.Red;
                this.numCompanyFares.InterceptArrowKeys = false;
                this.numCompanyFares.Location = new System.Drawing.Point(854, 61);
                this.numCompanyFares.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
                this.numCompanyFares.Name = "numCompanyFares";
                // 
                // 
                // 
                this.numCompanyFares.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
                this.numCompanyFares.RootElement.ForeColor = System.Drawing.Color.Red;
                this.numCompanyFares.ShowBorder = true;
                this.numCompanyFares.ShowUpDownButtons = false;
                this.numCompanyFares.Size = new System.Drawing.Size(62, 24);
                this.numCompanyFares.TabIndex = 241;
                this.numCompanyFares.TabStop = false;
                this.numCompanyFares.Visible = true;

            //    if(AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                 this.numCompanyFares.Validated += new EventHandler(numCompanyFares_Validated);
             
                this.numCompanyFares.Enter += new EventHandler(numCompanyFares_Enter);
             

                ((Telerik.WinControls.UI.RadSpinElement)(this.numCompanyFares.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCompanyFares.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCompanyFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCompanyFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                ((System.ComponentModel.ISupportInitialize)(this.lblCompanyPrice)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numCompanyFares)).EndInit();

                this.radPanel1.Controls.Add(this.lblCompanyPrice);
                this.radPanel1.Controls.Add(this.numCompanyFares);

                this.lblCompanyPrice.BringToFront();
                this.numCompanyFares.BringToFront();



                this.lblReturnCompanyPrice = new Telerik.WinControls.UI.RadLabel();
                this.numReturnCompanyFares = new Telerik.WinControls.UI.RadSpinEditor();
                ((System.ComponentModel.ISupportInitialize)(this.lblReturnCompanyPrice)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numReturnCompanyFares)).BeginInit();


                // 
                // lblCompanyPrice
                // 
                this.lblReturnCompanyPrice.AutoSize = false;
                this.lblReturnCompanyPrice.BackColor = System.Drawing.Color.Orange;
                this.lblReturnCompanyPrice.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
                this.lblReturnCompanyPrice.ForeColor = System.Drawing.Color.Black;
                this.lblReturnCompanyPrice.Location = new System.Drawing.Point(702, 93);
                this.lblReturnCompanyPrice.Name = "lblCompanyPrice";
                // 
                // 
                // 
                this.lblReturnCompanyPrice.RootElement.ForeColor = System.Drawing.Color.Black;
                this.lblReturnCompanyPrice.Size = new System.Drawing.Size(135, 19);
                this.lblReturnCompanyPrice.TabIndex = 242;
                this.lblReturnCompanyPrice.Text = "Rt Company Price £";
                this.lblReturnCompanyPrice.Visible = true;
                // 
                // numCompanyFares
                // 
                this.numReturnCompanyFares.DecimalPlaces = 2;
                this.numReturnCompanyFares.EnableKeyMap = true;
                this.numReturnCompanyFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numReturnCompanyFares.ForeColor = System.Drawing.Color.Red;
                this.numReturnCompanyFares.InterceptArrowKeys = false;
                this.numReturnCompanyFares.Location = new System.Drawing.Point(860, 89);
                this.numReturnCompanyFares.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
                this.numReturnCompanyFares.Name = "numReturnCompanyFares";
                if(AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                   this.numReturnCompanyFares.Validated += new EventHandler(numReturnCompanyFares_Validated);
                // 
                // 
                // 
                this.numReturnCompanyFares.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
                this.numReturnCompanyFares.RootElement.ForeColor = System.Drawing.Color.Red;
                this.numReturnCompanyFares.ShowBorder = true;
                this.numReturnCompanyFares.ShowUpDownButtons = false;
                this.numReturnCompanyFares.Size = new System.Drawing.Size(55, 24);
                this.numReturnCompanyFares.TabIndex = 241;
                this.numReturnCompanyFares.TabStop = false;
                this.numReturnCompanyFares.Visible = true;


                this.numReturnCompanyFares.Enabled = opt_JReturnWay.ToggleState == ToggleState.On;



                // this.numCompanyFares.Validated += new EventHandler(numCompanyFares_Validated);

                ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnCompanyFares.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numReturnCompanyFares.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnCompanyFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnCompanyFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                ((System.ComponentModel.ISupportInitialize)(this.lblReturnCompanyPrice)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numReturnCompanyFares)).EndInit();

                this.radPanel1.Controls.Add(this.lblReturnCompanyPrice);
                this.radPanel1.Controls.Add(this.numReturnCompanyFares);

                this.lblReturnCompanyPrice.BringToFront();
                this.numReturnCompanyFares.BringToFront();
            }
            catch (Exception ex)
            {


            }

        }

       

        decimal oldCompanyFare = 0.00m;

        void numCompanyFares_Enter(object sender, EventArgs e)
        {
            oldCompanyFare = numCompanyFares.Value;
        }

        void numCompanyFares_Validated(object sender, EventArgs e)
        {
            try
            {

                // only for gbc cars
                if (AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                {
                    if (numCompanyFares.Text.Trim().Length > 0 && numCompanyFares.Text.Trim().ToDecimal() > 0)
                    {

                    //if (numCompanyFares.Value > 0)
                    //{
                        decimal serviceCharge = 0.00m;


                        decimal price = numCustomerFares.Value;

                        if (ddlCompany.SelectedValue != null)
                            price = numCompanyFares.Text.Trim().ToDecimal();

                        Gen_ServiceCharge objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => (price >= c.FromValue && price <= c.ToValue) && (ddlCompany.SelectedValue.ToInt() == 0 || (c.IsAccount != null && c.IsAccount == true)));

                        if (objServiceCharge != null)
                        {

                            if (objServiceCharge.AmountWise.ToBool())
                            {
                                serviceCharge = objServiceCharge.ServiceChargeAmount.ToDecimal();
                            }
                            else
                            {
                                if (ddlCompany.SelectedValue != null)
                                    serviceCharge = (price * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;
                                else
                                    serviceCharge = (numCustomerFares.Value * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;



                            }

                            if (ddlCompany.SelectedValue != null)
                                numFareRate.Value = price - serviceCharge;
                            else
                                numFareRate.Value = numCustomerFares.Value - serviceCharge;
                        }
                    }

                }


                if (AppVars.objPolicyConfiguration.DisableDriverCommissionTick.ToBool() == true)
                {

                    if (oldCompanyFare == 0 || oldCompanyFare != numCompanyFares.Value)
                    {

                        CalculateAgentFeesAndDriverFares();
                    }

                }

                CalculateTotalCharges();
            }
            catch
            {

            }
        }


        void numReturnCompanyFares_Validated(object sender, EventArgs e)
        {
            try
            {
                if (numReturnCompanyFares.Text.Trim().Length > 0 && numReturnCompanyFares.Text.Trim().ToDecimal() > 0)
                {

                //if (numReturnCompanyFares.Value > 0)
                //{
                    decimal serviceCharge = 0.00m;


                    decimal price = numReturnCustFare.Value;

                    if (ddlCompany.SelectedValue != null)
                        price = numReturnCompanyFares.Text.Trim().ToDecimal();

                    Gen_ServiceCharge objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => (price >= c.FromValue && price <= c.ToValue) && (ddlCompany.SelectedValue.ToInt() == 0 || (c.IsAccount != null && c.IsAccount == true)));

                    if (objServiceCharge != null)
                    {

                        if (objServiceCharge.AmountWise.ToBool())
                        {
                            serviceCharge = objServiceCharge.ServiceChargeAmount.ToDecimal();
                        }
                        else
                        {
                            if (ddlCompany.SelectedValue != null)
                                serviceCharge = (price * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;
                            else
                                serviceCharge = (numReturnCustFare.Value * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;



                        }

                        if (ddlCompany.SelectedValue != null)
                            numReturnFare.Value = price - serviceCharge;
                        else
                            numReturnFare.Value = numReturnCustFare.Value - serviceCharge;
                    }
                }

                //}
            }
            catch
            {


            }

        }


        private void CalculateAgentFeesAndDriverFares()
        {

            if (numAgentCommission != null && numAgentCommission.Visible==true)
            {
                numAgentCommission.Value = (numCompanyFares.Value * numAgentCommissionPercent.Value.ToInt()) / 100;

                if (numAgentCommission.Value > 0)
                {
                    //     numAgentCommission.Value = (decimal)(Math.Ceiling(Convert.ToDouble(numAgentCommission.Value) / 0.25) * 0.25);
                    numAgentCommission.Value = Math.Round(numAgentCommission.Value * 4, MidpointRounding.ToEven) / 4;

                }
                if (numCompanyFares.Value - numAgentCommission.Value >= 0)
                {

                    numFareRate.Value =  numCompanyFares.Value - numAgentCommission.Value;
                    numCustomerFares.Value = numFareRate.Value;
                }
            }
        }


        private void CalculateDriverFaresAndCompanyPrice()
        {
            if (numAgentCommission != null && numAgentCommission.Visible == true)
            {
                if (numCompanyFares.Value - numAgentCommission.Value >= 0)
                {
                    numFareRate.Value =  numCompanyFares.Value - numAgentCommission.Value;
                    numCompanyFares.Value = numFareRate.Value + numAgentCommission.Value;
                }
            }
        }

        private void CalculateAgentFees()
        {

            if (numAgentCommission != null && numAgentCommissionPercent.Value.ToInt()>0)
            {
                numAgentCommission.Value = (numCompanyFares.Value * numAgentCommissionPercent.Value.ToInt()) / 100;

                if (numAgentCommission.Value > 0)
                {

                    //   numAgentCommission.Value = (decimal)(Math.Ceiling(Convert.ToDouble(numAgentCommission.Value) / 0.25) * 0.25);
                    numAgentCommission.Value = Math.Round(numAgentCommission.Value * 4, MidpointRounding.ToEven) / 4;
                }
            }
        }

        private void SetAccountPaymentType()
        {
            ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.BANK_ACCOUNT;

        }


        private void SetCashPaymentType()
        {

            if (objMaster.PrimaryKeyValue == null)
            {

                if (AppVars.objPolicyConfiguration.ShowBlankPaymentTypeAsDefault.ToBool() == false)
                    ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;

            }
            else
            {
                ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;

            }



        }

        bool IsClickableFares = false;
        private void btnPickFares_Click(object sender, EventArgs e)
        {
            IsClickableFares = true;
            CalculateTotalFares();
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


                if (string.IsNullOrEmpty(fromLocName))
                {
                    fromLocName = txtFromAddress.ListBoxElement.SelectedItem.ToStr().Trim();

                }


                if (string.IsNullOrEmpty(toLocName))
                {
                    toLocName = txtToAddress.ListBoxElement.SelectedItem.ToStr().Trim();

                }



                string fromAddress = txtFromAddress.Text.Trim().ToUpper();
                string toAddress = txtToAddress.Text.Trim().ToUpper();
                string fromPostCode = txtFromPostCode.Text.Trim().ToUpper();
                string toPostCode = txtToPostCode.Text.Trim().ToUpper();

                string tempFromPostCode = string.Empty;
                string tempToPostCode = string.Empty;

                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    tempFromPostCode = General.GetPostCodeMatch(fromAddress);


                    if (string.IsNullOrEmpty(tempFromPostCode) && !string.IsNullOrEmpty(fromLocName))
                        tempFromPostCode = General.GetPostCodeMatch(fromLocName);

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


                    if (string.IsNullOrEmpty(tempToPostCode) && !string.IsNullOrEmpty(toLocName))
                        tempToPostCode = General.GetPostCodeMatch(toLocName);
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

                        if (objMaster.Current.CompanyId != ddlCompany.SelectedValue.ToIntorNull())
                        {
                            IsClickableFares = true;
                        }

                        if (objMaster.Current.VehicleTypeId != ddlVehicleType.SelectedValue.ToIntorNull())
                        {
                            IsClickableFares = true;
                        }

                        if (IsClickableFares == false)
                        {
                            return;
                        }

                    }

                }

                lblMap.Text = string.Empty;

                IsClickableFares = false;
                mileageError = false;
                CalculateFares();


                string via = string.Empty;

                if (AppVars.objPolicyConfiguration.EnableOfflineDistance.ToBool() == false && grdVia != null && grdVia.Rows.Count > 0)
                {
                    via = "&waypoints=";


                    via += string.Join("|", grdVia.Rows.Select(c => General.GetPostCodeMatch(c.Cells["VIALOCATIONVALUE"].Value.ToStr().ToUpper()) + ", UK").ToArray<string>());

                    estimatedTime = General.CalculateEstimatedTime(tempFromPostCode, tempToPostCode, via);
                }





                if (estimatedTime.ToStr().Length > 0)
                    estimatedTime = "Time :" + estimatedTime;


                lblMap.Text = "Distance : " + string.Format("{0:#.##}", milesList.Sum()) + " miles " + estimatedTime;




                CalculateTotalCharges();




            }
            catch (Exception ex)
            {

            }
        }



     

        string estimatedTime = string.Empty;
        string setFareLogMsg = string.Empty;
        private void CalculateFares()
        {

            if (ddlCompany.SelectedValue.ToInt()!=0 && ResetAllFares)
                return;

            if (ddlCompany.SelectedValue.ToInt() != 0 && ddlPaymentType.SelectedValue.ToInt() == 0)
            {

                numFareRate.Value = 0.00m;
                numCustomerFares.Value = 0.00m;

                if (numCompanyFares != null)
                {
                    numCompanyFares.Value = 0.00m;

                }


                if (numAgentCommission != null)
                {
                    numAgentCommission.Value = 0.00m;
                }


                if (numReturnFare != null)
                {
                    numReturnFare.Value = 0.00m;

                }

                if (numReturnCustFare != null)
                {
                    numReturnCustFare.Value = 0.00m;

                }

                if (numReturnCompanyFares != null)
                {
                    numReturnCompanyFares.Value = 0.00m;

                }

                return;


            }
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


            int paymentTypeId = ddlPaymentType.SelectedValue.ToInt();
           


            string fromSetFareVal = fromAddress;
            string toSetFareVal = toAddress;

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


            if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {

                fromSetFareVal = txtFromPostCode.Text;
            }
            else if (fromLocTypeId != Enums.LOCATION_TYPES.ADDRESS && fromLocTypeId != Enums.LOCATION_TYPES.BASE)
            {
                fromSetFareVal = ddlFromLocation.ComboBoxElement.TextBoxElement.TextBoxItem.Text.Trim();

            }


            if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
            {

                toSetFareVal = txtToPostCode.Text;
            }
            else if (toLocTypeId != Enums.LOCATION_TYPES.ADDRESS && fromLocTypeId != Enums.LOCATION_TYPES.BASE)
            {
                toSetFareVal = ddlToLocation.ComboBoxElement.TextBoxElement.TextBoxItem.Text.Trim();

            }







            int fromZoneId = ddlPickupPlot.SelectedValue.ToInt();
            int toZoneId = ddlDropOffPlot.SelectedValue.ToInt();

            DateTime? pickupdateTime = dtpPickupTime.Value;
            DateTime? pickupdateTimeMileageWise = string.Format("{0:dd/MM/yyyy HH:mm}", new DateTime(1900, 1, 1, 0, 0, 0).ToDate() + dtpPickupTime.Value.Value.TimeOfDay).ToDateTime();


            List<string> errors = null;


            if (errors == null)
                errors = new List<string>();
            else
                errors.Clear();



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


            //if (ddlFromLocType.Items.Count(c => c.Text == "Zone") > 0)
            //    IsZoneWise = true;



            int tempFromLocId = 0;
            int tempToLocId = 0;
            string tempFromPostCode = "";
            string tempToPostCode = "";
            string errorMsg = string.Empty;

            decimal fareVal = 0.00m;
            decimal deadMileage = AppVars.objPolicyConfiguration.DeadMileage.ToDecimal();

            estimatedTime = string.Empty;





            // Pick Custom Journey Fares

            string via = string.Empty;


            if (grdVia != null)
            {
                via = string.Join(",", grdVia.Rows.Select(c => c.Cells["VIALOCATIONVALUE"].Value.ToStr().ToUpper()).ToArray<string>());

            }


            int? tCompanyId = ddlCompany.SelectedValue.ToIntorNull();

            var objCustomFare = General.GetQueryable<Fare_CustomJourney>(c => (c.Pickup == fromSetFareVal && c.Destination == toSetFareVal)
                && (c.CompanyId == tCompanyId || tCompanyId == null)
                && (c.ViaPoints == via || via == string.Empty)).OrderByDescending(c => c.Id).FirstOrDefault();


            if (objCustomFare != null)
            {
                numFareRate.Value = objCustomFare.DriverFares.ToDecimal();
                numFareRate.SpinElement.TextBoxItem.BackColor = Color.Pink;

                string msg = "Drv Fare : " + numFareRate.Value;
                if (numReturnFare != null)
                {
                    numReturnFare.Value = objCustomFare.DriverRtnFares.ToDecimal();
                    numReturnFare.SpinElement.TextBoxItem.BackColor = Color.Pink;

                }

                numCustomerFares.Value = objCustomFare.CustomerFares.ToDecimal();
                numCustomerFares.SpinElement.TextBoxItem.BackColor = Color.Pink;
                msg += " , Cust Fare : " + numCustomerFares.Value;

                if (numReturnCustFare != null)
                {
                    numReturnCustFare.Value = objCustomFare.CustomerRtnFares.ToDecimal();
                    numReturnCustFare.SpinElement.TextBoxItem.BackColor = Color.Pink;

                }

                if (numCompanyFares != null)
                {

                    numCompanyFares.Value = objCustomFare.CompanyFares.ToDecimal();
                    numCompanyFares.SpinElement.TextBoxItem.BackColor = Color.Pink;
                    msg += " , Company Fare : " + numCompanyFares.Value;
                    if (numReturnCompanyFares != null)
                    {
                        numReturnCompanyFares.Value = objCustomFare.CompanyRtnFares.ToDecimal();
                        numReturnCompanyFares.SpinElement.TextBoxItem.BackColor = Color.Pink;


                    }
                }

                milesList.Clear();

                decimal miles = CalculateTotalDistance(fromSetFareVal, via.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray<string>(), toSetFareVal);


                milesList.Add(miles);


                if (objMaster.Current == null)
                {
                    setFareLogMsg = "Calculate Fares from (Set Fare : " + msg + ")";
                }
                else
                {

                    UpdateSetFareLog("Calculate Fares from (Set Fare : " + msg + ")");
                }

                return;

            }
            else
            {

                //
                numFareRate.SpinElement.TextBoxItem.BackColor = Color.White;
                if (numReturnFare != null)
                    numReturnFare.SpinElement.TextBoxItem.BackColor = Color.White;

                numCustomerFares.SpinElement.TextBoxItem.BackColor = Color.White;

                if (numReturnCustFare != null)
                    numReturnCustFare.SpinElement.TextBoxItem.BackColor = Color.White;

                if (numCompanyFares != null)
                    numCompanyFares.SpinElement.TextBoxItem.BackColor = Color.White;

                if (numCompanyFares != null)
                    numReturnCompanyFares.SpinElement.TextBoxItem.BackColor = Color.White;


                bool IsCompanyFareExist = false;
              

              



                if (AppVars.objPolicyConfiguration.EnableZoneWiseFares.ToBool()==false)
                {

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


                            estimatedTime = " ";
                            // Fare Calculation
                            fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, true, IsZoneWise, pickupdateTimeMileageWise, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime);

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

                //if (AppVars.listUserRights.Count(c => c.functionId == "DISABLE PLOT WISE FARE LIST" && c.formName == "frmFares") > 0)
                //{

                    // Fare Calculation
                    // fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, IsZoneWise, pickupdateTime, ref deadMileage);

                    if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS && tempToPostCode.Trim().Contains(" ") == false)
                        return;
                    
                    
                    fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, IsZoneWise, pickupdateTimeMileageWise, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime);



                    //if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool() && companyId != 0 && IsCompanyFareExist == false && numCompanyFares != null &&
                    //    (numAgentCommission == null || numAgentCommission.Visible == false) && (DisableAutoCalculateFares == false || numCompanyFares.Value == 0.00m))
                    //{
                    //    DisableAutoCalculateFares = true;
                    //    numFareRate.Value = 0.00m;
                    //    numCustomerFares.Value = 0.00m;

                    //    if (numCompanyFares != null)
                    //    {
                    //        numCompanyFares.Value = 0.00m;
                    //    }

                    //    if (numReturnFare != null)
                    //    {
                    //        numReturnFare.Value = 0.00m;
                    //    }

                    //    if (numReturnCustFare != null)
                    //    {
                    //        numReturnCustFare.Value = 0.00m;
                    //    }

                    //    if (numReturnCompanyFares != null)
                    //    {
                    //        numReturnCompanyFares.Value = 0.00m;
                    //    }

                    //    return;

                    //}
                    //else
                    //{
                    //    if (companyId != 0 && DisableAutoCalculateFares)
                    //        return;

                    //}


                    if (errorMsg == "Error")
                    {

                        numFareRate.Value = 0;
                        //return false;
                        return;
                    }

                    if (estimatedTime.ToStr().Trim().Length > 0 && estimatedTime.IsNumeric())
                    {

                        estimatedTime += " mins";
                    }


                    decimal dd;

                    

                        if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool() == false)
                        {
                            dd = fareVal.ToDecimal();


                            decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                            if (roundUp > 0)
                            {
                               // fareVal = (decimal)Math.Ceiling(fareVal / roundUp) * roundUp;

                                fareVal = (decimal)Math.Ceiling(fareVal / roundUp) * roundUp;

                            }   

                           
                        }
                        else
                        {
                            string ff = string.Format("{0:f2}", fareVal);
                            if (ff == string.Empty)
                                ff = "0";

                            dd = ff.ToDecimal();
                        }


                   

                    decimal airportPickupChrgs = AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();
                    //// Add Airport Pickup Charges If Pickup Point is From Airport...
                    //if (companyId == 0 && ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT && errorMsg == "Reverse found"
                    //    && AppVars.objPolicyConfiguration.HasAirportDropOffCharges.ToBool() == false)
                    //{
                    if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                      && AppVars.objPolicyConfiguration.HasAirportDropOffCharges.ToBool() == false)
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
                  

                    var objInc = General.GetObject<Fare_IncrementSetting>(c => c.Id != 0 && c.EnableIncrement != null && c.EnableIncrement == true);

                    if (objInc != null)
                    {

                        bool IsExist = false;

                        DateTime? pickupDateTime = (dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay).ToDateTime();


                        if ((objInc.CriteriaBy.ToInt() == 1 && pickupDateTime >= objInc.FromDate && pickupDateTime <= objInc.TillDate)
                        || (objInc.CriteriaBy.ToInt() == 2 && pickupDateTime.ToDate() >= objInc.FromDate.ToDate() && pickupDateTime.ToDate() <= objInc.TillDate.ToDate())
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

                   
                    decimal serviceCharge = 0.00m;

               
                    // NEED TO UNCOMMENT FOR GBC CARS
                    Gen_ServiceCharge objServiceCharge = null;
                    if (AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                    {
                         objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => (dd >= c.FromValue && dd <= c.ToValue) && (companyId == 0 || (c.IsAccount != null && c.IsAccount == true)));

                        if (objServiceCharge != null)
                        {

                            if (objServiceCharge.AmountWise.ToBool())
                            {
                                serviceCharge = objServiceCharge.ServiceChargeAmount.ToDecimal();
                            }
                            else
                                serviceCharge = (dd * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;

                        }
                    }

                    numFareRate.Value = dd - serviceCharge;

                    numCustomerFares.Value = dd;

                    if (companyId != 0 && numCompanyFares != null)
                    {

                        if (companyPricePercentage > 0)
                        {
                            dd += (dd * companyPricePercentage) / 100;
                        }


                        var objCompanyFare=  General.GetObject<Gen_Company_FareSetting>(c => c.CompanyId == companyId && pickupdateTime >= c.FromDate && pickupdateTime <= c.TillDate);

                        if (objCompanyFare != null)
                        {

                            if (objCompanyFare.OperatorType.ToInt() == 1) //increment
                            {
                                if (objCompanyFare.ActionType.ToStr().ToLower() == "percent")
                                {

                                    dd = dd - ((dd * objCompanyFare.Amount.ToDecimal()) / 100);

                                }
                                else
                                {
                                    dd = dd + objCompanyFare.Amount.ToDecimal();

                                }

                            }

                            else if (objCompanyFare.OperatorType.ToInt() == 2) //discount
                            {
                                if (objCompanyFare.ActionType.ToStr().ToLower() == "percent")
                                {

                                    dd = dd-  ((dd * objCompanyFare.Amount.ToDecimal()) / 100);

                                }
                                else
                                {
                                    dd = dd + objCompanyFare.Amount.ToDecimal();

                                }
                            }

                           

                        }

                        numCompanyFares.Value = dd;




                        if (numAgentCommission != null)
                        {

                            if (numAgentCommission.Visible == true)
                            {

                                Gen_Company_AgentCommission objComm = null;

                                if (paymentTypeId == Enums.PAYMENT_TYPES.CASH && 
                                    (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT || toLocTypeId == Enums.LOCATION_TYPES.AIRPORT))
                                {


                                    int? locId = 0;
                                    if ((fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT))
                                    {
                                        locId = fromLocationId;
                                    }
                                    else
                                    {
                                        locId = toLocationId;
                                    }


                                    objComm = General.GetObject<Gen_Company_AgentCommission>(c => c.FareId != null && c.CompanyId == companyId && c.LocationId == locId && c.VehicleTypeId == vehicleTypeId && c.CompanyPrice > 0);



                                    if (objComm != null)
                                    {

                                        if (objComm.CommissionOnPercent.ToBool())
                                        {

                                            numAgentCommissionPercent.Value = objComm.CommissionPercent.ToDecimal();
                                        }
                                        else
                                        {

                                            numAgentCommissionPercent.Value = 0;
                                            numAgentCommission.Value = objComm.CommissionAmount.ToDecimal();
                                        }

                                    }


                                }
                                //else
                                //{
                                if (objComm == null)
                                {
                                    var objCompany = General.GetObject<Gen_Company>(c => c.Id == companyId);
                                    if (objCompany != null)
                                    {
                                        if (paymentTypeId == Enums.PAYMENT_TYPES.CASH && objCompany.IsAgent.ToBool())
                                        {
                                            if (objCompany.IsAmountWiseComm.ToBool())
                                            {
                                                numAgentCommissionPercent.Value = 0.00m;
                                                numAgentCommission.Value = objCompany.CommissionPerBooking.ToDecimal();

                                            }
                                            else
                                            {

                                                numAgentCommissionPercent.Value = objCompany.CommissionPerBooking.ToDecimal();
                                            }
                                        }
                                        else
                                        {
                                            numAgentCommissionPercent.Value = objCompany.DriverFareReductionValue.ToInt();

                                        }
                                    }
                                }
                            }
                            else
                            {
                                numAgentCommissionPercent.Value = 0.00m;
                                numAgentCommission.Value = 0.00m;
                            }
                        }
                        

                        CalculateAgentFees();

                    }

                    if (opt_JReturnWay.ToggleState == ToggleState.On && numReturnFare != null)
                    {
                        // numReturnFare.Value = numFareRate.Value;

                        numReturnFare.Value = dd - ((dd * AppVars.objPolicyConfiguration.DiscountForReturnedJourneyPercent.ToInt()) / 100);

                        if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                            numReturnFare.Value -= airportPickupChrgs;


                        else if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                            numReturnFare.Value += airportPickupChrgs;


                        serviceCharge = 0.00m;
                        if (AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                        {
                            //NEED TO UNCOMMENT FOR GBC CARS
                            objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => (numReturnFare.Value >= c.FromValue && numReturnFare.Value <= c.ToValue) && (companyId == 0 || (c.IsAccount != null && c.IsAccount == true)));

                            if (objServiceCharge != null)
                            {

                                if (objServiceCharge.AmountWise.ToBool())
                                {
                                    serviceCharge = objServiceCharge.ServiceChargeAmount.ToDecimal();
                                }
                                else
                                    serviceCharge = (numReturnFare.Value * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;

                            }
                        }


                        numReturnCustFare.Value = numReturnFare.Value;
                        numReturnFare.Value = numReturnFare.Value - serviceCharge;


                        if (companyId != 0 && numReturnCompanyFares != null)
                        {

                            numReturnCompanyFares.Value = numCompanyFares.Value;

                        }

                    }
                    else if (opt_WaitandReturn.ToggleState == ToggleState.On)
                    {

                        decimal val = numFareRate.Value + ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForWRJourneyPercent.ToInt()) / 100);
                        val = (decimal)Math.Ceiling(val / 0.5m) * 0.5m;

                        serviceCharge = 0.00m;

                        //NEED TO UNCOMMENT FOR GBC CARS
                        if (AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                        {
                            objServiceCharge = General.GetObject<Gen_ServiceCharge>(c => val >= c.FromValue && val <= c.ToValue && (companyId == 0 || (c.IsAccount != null && c.IsAccount == true)));

                            if (objServiceCharge != null)
                            {

                                if (objServiceCharge.AmountWise.ToBool())
                                {
                                    serviceCharge = objServiceCharge.ServiceChargeAmount.ToDecimal();
                                }
                                else
                                    serviceCharge = (val * objServiceCharge.ServiceChargePercent.ToDecimal()) / 100;

                            }
                        }

                        numFareRate.Value = val-serviceCharge;
                        numCustomerFares.Value = val;
                    }
                    else
                    {
                        if (numReturnFare != null)
                            numReturnFare.Value = 0;
                    }



                    if (companyId != 0)
                    {
                        drvFareReductionValue = 0.00m;
                        drvFareReductionType = "Percent";


                        if (paymentTypeId==Enums.PAYMENT_TYPES.CASH &&
                            (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT   || toLocTypeId == Enums.LOCATION_TYPES.AIRPORT ))
                        {
                            int? locId=0;
                            if((fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT))
                            {
                                locId=fromLocationId;
                            }
                            else
                            {
                                  locId=toLocationId;
                            }


                            var obj = General.GetObject<Gen_Company_AgentCommission>(c =>c.FareId!=null && c.LocationId == locId && c.CompanyId == companyId && c.VehicleTypeId==vehicleTypeId && c.CompanyPrice>0);
                            if (obj != null)
                            {
                                if(obj.CommissionOnPercent.ToBool())
                                {

                                    drvFareReductionValue = obj.CommissionPercent.ToDecimal();
                                   drvFareReductionType = "percent";
                                }
                                else
                                {

                                    drvFareReductionValue = obj.CommissionAmount.ToDecimal();
                                    drvFareReductionType = "amount";
                                }                            

                            }
                        }
                        else
                        {

                            var objCompany=  General.GetObject<Gen_Company>(c => c.Id == companyId);
                            if (objCompany != null)
                            {
                                if (objCompany.IsAgent.ToBool())
                                {
                                    if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                                        || paymentTypeId==Enums.PAYMENT_TYPES.CREDIT_CARD || paymentTypeId==Enums.PAYMENT_TYPES.ROOM_CHARGE
                                       )
                                    {

                                        drvFareReductionValue = objCompany.DriverFareReductionValue.ToDecimal();
                                        drvFareReductionType = objCompany.DriverFareReductionType.ToStr().Trim().ToLower();


                                    }
                                    else
                                    {
                                        drvFareReductionValue = objCompany.CommissionPerBooking.ToDecimal();
                                        drvFareReductionType = objCompany.IsAmountWiseComm.ToBool() ? "amount" : "percent";

                                    }
                                }
                                else
                                {
                                    //if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT)
                                    //{

                                        drvFareReductionValue = objCompany.DriverFareReductionValue.ToDecimal();
                                        drvFareReductionType = objCompany.DriverFareReductionType.ToStr().Trim().ToLower();


                                 //   }

                                }

                             

                            }
                        }

                        if (drvFareReductionValue > 0 && numCompanyFares!=null && numCompanyFares.Value > 0 && IsCompanyFareExist)
                        {

                            decimal reductionAmount=0.00m;
                            if (drvFareReductionType == "amount")
                            {

                              
                                   reductionAmount = numFareRate.Value - drvFareReductionValue;

                                  
                            }
                            else
                            {
                                reductionAmount= (decimal)(Convert.ToDouble(((numCompanyFares.Value * drvFareReductionValue) / 100)));

                            }

                            if(reductionAmount>0)
                                reductionAmount = Math.Round(reductionAmount * 4, MidpointRounding.ToEven) / 4;


                                if (paymentTypeId == Enums.PAYMENT_TYPES.CASH && (numAgentCommission!=null && numAgentCommission.Visible))
                                {

                                    CalculateDriverFaresAndCompanyPrice();

                                }
                                else
                                {
                                    numFareRate.Value -= reductionAmount;

                                }
                                        

                                numCustomerFares.Value = numFareRate.Value;
                               
                                
                                if (numReturnFare != null && numReturnFare.Value > 0)
                                {
                                    numReturnFare.Value -= (numReturnCompanyFares.Value * drvFareReductionValue) / 100;

                                    if (numReturnCustFare != null)
                                    {
                                        numReturnCustFare.Value = numReturnFare.Value;
                                    }
                                }


                                //decimal  reductionAmount = numFareRate.Value - drvFareReductionValue;

                                //if (reductionAmount > 0)
                                //{
                                //    numFareRate.Value = numCompanyFares.Value - drvFareReductionValue;
                                //    numCustomerFares.Value = numFareRate.Value;

                                //    if (numReturnFare != null && numReturnFare.Value > 0)
                                //    {
                                //        numReturnFare.Value = numReturnCompanyFares.Value - drvFareReductionValue;

                                //        if (numReturnCustFare != null)
                                //        {
                                //            numReturnCustFare.Value = numReturnFare.Value;
                                //        }
                                //    }                                
                                //}
                          //  }
                            //else if (drvFareReductionType == "percent")
                            //{


                            

                            //    //    numFareRate.Value -= (decimal)(Math.Ceiling(Convert.ToDouble(((numCompanyFares.Value * drvFareReductionValue) / 100)) / 0.25) * 0.25);

                            //    if (paymentTypeId == Enums.PAYMENT_TYPES.CASH && (numAgentCommission!=null && numAgentCommission.Visible))
                            //    {

                            //        CalculateDriverFaresAndCompanyPrice();

                            //    }
                            //    else
                            //    {
                            //        numFareRate.Value -= (decimal)(Convert.ToDouble(((numCompanyFares.Value * drvFareReductionValue) / 100)));

                            //    }
                                        

                            //    numCustomerFares.Value = numFareRate.Value;
                               
                                
                            //    if (numReturnFare != null && numReturnFare.Value > 0)
                            //    {
                            //        numReturnFare.Value -= (numReturnCompanyFares.Value * drvFareReductionValue) / 100;

                            //        if (numReturnCustFare != null)
                            //        {
                            //            numReturnCustFare.Value = numReturnFare.Value;
                            //        }
                            //    }
                            //}
                        }


                      
                    }
                    else
                    {

                        if (numCompanyFares != null)
                        {
                            numCompanyFares.Value = 0.00m;

                        }

                        if (numReturnCompanyFares != null)
                        {
                            numReturnCompanyFares.Value = 0.00m;

                        }

                    }



                }
                else
                {
                    int defaultVehicleId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
                    bool IsMoreFareWise = false;
                   

                   


                    bool hasVia = false;
                    decimal returnFares = 0.00m;

                    if (pnlVia != null)
                    {
                        hasVia = grdVia.Rows.Count > 0;
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


                    if (AppVars.objPolicyConfiguration.ZoneWiseFareType.ToInt() == 1)
                    {
                        if (vehicleTypeId != defaultVehicleId)
                        {
                            vehicleTypeId = defaultVehicleId;
                            IsMoreFareWise = true;
                        }

                        if (ddlPickupPlot.SelectedValue == null)
                            fromZoneId = GetZoneId(tempFromPostCode).ToInt();
                        else
                            fromZoneId = ddlPickupPlot.SelectedValue.ToInt();


                        if (ddlDropOffPlot.SelectedValue == null)
                            toZoneId = GetZoneId(tempToPostCode).ToInt();
                        else
                            toZoneId = ddlDropOffPlot.SelectedValue.ToInt();

                        

                        if ((fromZoneId != 0 && toZoneId != 0))
                        {

                            var objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == fromZoneId && c.ToZoneId == toZoneId);

                            if (objPlotFare == null)
                                objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == toZoneId && c.ToZoneId == fromZoneId);


                            if (objPlotFare != null)
                            {
                                fareVal = objPlotFare.Price.ToDecimal();


                                objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == toZoneId && c.ToZoneId == fromZoneId);
                                if (objPlotFare == null)
                                {

                                    returnFares = fareVal;
                                }
                                else
                                    returnFares = objPlotFare.Price.ToDecimal();
                            }


                            milesList.Clear();


                            if (chkReverse.ToggleState == ToggleState.On)
                            {
                                string temp = tempFromPostCode;
                                tempFromPostCode = tempToPostCode;
                                tempToPostCode = temp;

                            }

                            tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                            tempToPostCode = General.GetPostCodeMatch(tempToPostCode);


                            if (hasVia)
                            {
                                var viaList = grdVia.Rows.Select(a => a.Cells["VIALOCATIONVALUE"].Value.ToStr()).ToArray<string>();
                                milesList.Add(CalculateTotalDistance(tempFromPostCode, viaList, tempToPostCode));

                                fareVal = fareVal + (viaList.Count() * 2.00m);

                            }
                            else
                            {
                                milesList.Add(General.CalculateDistance(tempFromPostCode, tempToPostCode));
                            }
                        }
                        else if (hasVia == false)
                        {
                            var objVeh = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty();
                            decimal startRate = objVeh.StartRate.ToDecimal();
                            decimal startRateMiles = objVeh.StartRateValidMiles.ToDecimal();


                            milesList.Clear();
                            decimal miles = 0.00m;

                            if (chkReverse.ToggleState == ToggleState.On)
                            {
                                string temp = tempFromPostCode;
                                tempFromPostCode = tempToPostCode;
                                tempToPostCode = temp;
                            }


                            tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                            tempToPostCode = General.GetPostCodeMatch(tempToPostCode);

                            if ((fromZoneId != 0 && toZoneId == 0) || (fromZoneId == 0 && toZoneId != 0))
                            {

                                if (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT || toLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                                {

                                    int? locId = 0;

                                    if (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                                        locId = fromLocationId;

                                    else if (toLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                                        locId = toLocationId;


                                    fareVal = General.GetObject<Fare_ChargesDetail>(c => c.DestinationId == locId).DefaultIfEmpty().Rate.ToDecimal();
                                    returnFares = fareVal;

                                    milesList.Add(General.CalculateDistance(tempFromPostCode, tempToPostCode));
                                }
                                else
                                {




                                    if ((lastOrigin.Length > 0 && lastDestination.Length > 0
                                       && tempFromPostCode == lastOrigin && tempToPostCode == lastDestination) && lastMileage > 0 && lastJourneyMileage > 0)
                                    {


                                        miles = lastMileage;
                                        milesList.Add(lastJourneyMileage);
                                    }
                                    else
                                    {
                                        decimal towntoPickup = General.CalculateDistance("CO1 1PJ", tempFromPostCode);
                                        decimal destToTown = (General.CalculateDistance(tempToPostCode, "CO1 1PJ"));


                                        decimal journeyMilage = General.CalculateDistance(tempFromPostCode, tempToPostCode);
                                        milesList.Add(journeyMilage);

                                        //  miles = journeyMilage;
                                        //  miles += (General.CalculateDistance(tempToPostCode, General.GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr().ToUpper().Trim())) / 2);

                                        //    miles = invisibileMiles + journeyMilage;

                                        if ((towntoPickup + destToTown) > journeyMilage)
                                        {


                                            miles = (towntoPickup + journeyMilage + destToTown) / 2;
                                        }
                                        else
                                        {
                                            //     miles = journeyMilage;
                                            miles = journeyMilage + ((destToTown) / 2);
                                            // miles = journeyMilage + ((destToTown) / 2);
                                        }


                                        miles = Math.Round(miles, 1);
                                    }


                                    var fare = new TaxiDataContext().stp_CalculateFares(vehicleTypeId, 0, miles.ToStr());
                                    if (fare != null)
                                    {
                                        fareVal = startRate + fare.FirstOrDefault().DefaultIfEmpty().totalFares;

                                    }

                                    returnFares = fareVal;


                                    lastOrigin = tempFromPostCode;
                                    lastDestination = tempToPostCode;
                                    lastMileage = miles;
                                    lastJourneyMileage = milesList.Sum();



                                }
                            }
                            else if (fromZoneId == 0 && toZoneId == 0)
                            {


                                tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                                tempToPostCode = General.GetPostCodeMatch(tempToPostCode);


                                if ((lastOrigin.Length > 0 && lastDestination.Length > 0
                                 && tempFromPostCode == lastOrigin && tempToPostCode == lastDestination) && lastMileage > 0 && lastJourneyMileage > 0)
                                {


                                    miles = lastMileage;
                                    milesList.Add(lastJourneyMileage);
                                }
                                else
                                {


                                    decimal towntoPickup = General.CalculateDistance("CO1 1PJ", tempFromPostCode);
                                    decimal destToTown = (General.CalculateDistance(tempToPostCode, "CO1 1PJ"));


                                    decimal journeyMilage = General.CalculateDistance(tempFromPostCode, tempToPostCode);
                                    milesList.Add(journeyMilage);



                                    if ((towntoPickup + destToTown) > journeyMilage)
                                    {


                                        miles = (towntoPickup + journeyMilage + destToTown) / 2;
                                    }
                                    else
                                    {

                                        miles = journeyMilage + ((destToTown) / 2);
                                        //    miles = journeyMilage;
                                    }



                                    miles = Math.Round(miles, 1);


                                }

                                var fare = new TaxiDataContext().stp_CalculateFares(vehicleTypeId, 0, miles.ToStr());

                                if (fare != null)
                                {
                                    fareVal = startRate + fare.FirstOrDefault().DefaultIfEmpty().totalFares;


                                    lastOrigin = tempFromPostCode;
                                    lastDestination = tempToPostCode;
                                    lastMileage = miles;
                                    lastJourneyMileage = milesList.Sum();
                                }


                                returnFares = fareVal;
                                //    }

                            }
                        }

                        else if (hasVia == true)
                        {

                            if (chkReverse.ToggleState == ToggleState.On)
                            {
                                string temp = tempFromPostCode;
                                tempFromPostCode = tempToPostCode;
                                tempToPostCode = temp;

                            }

                            tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                            tempToPostCode = General.GetPostCodeMatch(tempToPostCode);
                            var viaList = grdVia.Rows.Select(a => a.Cells["VIALOCATIONVALUE"].Value.ToStr()).ToArray<string>();


                            milesList.Clear();

                            var objVeh = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty();
                            decimal startRate = objVeh.StartRate.ToDecimal();

                            decimal miles = 0.00m;

                            if (chkReverse.ToggleState == ToggleState.On)
                            {
                                string temp = tempFromPostCode;
                                tempFromPostCode = tempToPostCode;
                                tempToPostCode = temp;


                                temp = fromAddress;
                                fromAddress = toAddress;
                                toAddress = temp;

                            }


                            tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                            tempToPostCode = General.GetPostCodeMatch(tempToPostCode);


                            decimal towntoPickup = General.CalculateDistance("CO1 1PJ", tempFromPostCode);
                            decimal destToTown = (General.CalculateDistance(tempToPostCode, "CO1 1PJ"));


                            decimal journeyMilage = CalculateTotalDistance(tempFromPostCode, viaList, tempToPostCode);
                            milesList.Add(journeyMilage);



                            if ((towntoPickup + destToTown) > journeyMilage)
                            {


                                miles = (towntoPickup + journeyMilage + destToTown) / 2;
                            }
                            else
                            {
                                //  miles = journeyMilage;
                                miles = journeyMilage + ((destToTown) / 2);
                            }



                            miles = Math.Round(miles, 1);

                            var fare = new TaxiDataContext().stp_CalculateFares(vehicleTypeId, 0, miles.ToStr());


                            if (fare != null)
                            {
                                fareVal = fare.FirstOrDefault().DefaultIfEmpty().totalFares;

                                fareVal = startRate + fareVal + (viaList.Count() * 2.00m);

                            }


                            returnFares = fareVal;
                        }


                    }
                    else if(AppVars.objPolicyConfiguration.ZoneWiseFareType.ToInt()==2)
                    {

                        if (ddlPickupPlot.SelectedValue == null)
                            fromZoneId = GetOuterZoneId(tempFromPostCode).ToInt();
                        else
                            fromZoneId = ddlPickupPlot.SelectedValue.ToInt();


                        if (ddlDropOffPlot.SelectedValue == null)
                            toZoneId = GetOuterZoneId(tempToPostCode).ToInt();
                        else
                            toZoneId = ddlDropOffPlot.SelectedValue.ToInt();

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


                        bool IsMajorZone = false;
                        if (fromZoneId != 0 && toZoneId != 0 && fromZoneId == toZoneId)
                        {
                            if (General.GetQueryable<Gen_Zone>(null).Count(c => c.Id==fromZoneId && c.ZoneTypeId != null && c.ZoneTypeId == 1) > 0)
                            {
                                IsMajorZone = true;

                            }
                            

                        }

                        if (hasVia == false && (fromZoneId != 0 && toZoneId != 0) && IsMajorZone==false)
                        {
                            
                           

                                var objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == fromZoneId && c.ToZoneId == toZoneId);

                                if (objPlotFare == null)
                                    objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == toZoneId && c.ToZoneId == fromZoneId);


                                if (objPlotFare != null)
                                {
                                    fareVal = objPlotFare.Price.ToDecimal();


                                    objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => c.FromZoneId == toZoneId && c.ToZoneId == fromZoneId);
                                    if (objPlotFare == null)
                                    {

                                        returnFares = fareVal;
                                    }
                                    else
                                        returnFares = objPlotFare.Price.ToDecimal();
                                }


                                milesList.Clear();


                                if (chkReverse.ToggleState == ToggleState.On)
                                {
                                    string temp = tempFromPostCode;
                                    tempFromPostCode = tempToPostCode;
                                    tempToPostCode = temp;

                                }

                                tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                                tempToPostCode = General.GetPostCodeMatch(tempToPostCode);

                              
                                
                                 milesList.Add(General.CalculateDistance(tempFromPostCode, tempToPostCode));
                                


                           
                        }
                        else if (hasVia == false)
                        {
                          
                            fareVal = General.GetSimpleFareRate(companyId, ddlVehicleType.SelectedValue.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, true, IsZoneWise, pickupdateTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime,fromZoneId,toZoneId,ref IsMoreFareWise);
                            returnFares = fareVal;                           

                        }
                        else
                        {
                            milesList.Clear();


                            if (chkReverse.ToggleState == ToggleState.On)
                            {
                                string temp = tempFromPostCode;
                                tempFromPostCode = tempToPostCode;
                                tempToPostCode = temp;

                            }

                            tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                            tempToPostCode = General.GetPostCodeMatch(tempToPostCode);


                            
                                var viaList = grdVia.Rows.Select(a => a.Cells["VIALOCATIONVALUE"].Value.ToStr()).ToArray<string>();
                                milesList.Add(CalculateTotalDistance(tempFromPostCode, viaList, tempToPostCode));


                                decimal totalMiles = milesList.Sum();

                                var objFare = new TaxiDataContext().stp_CalculateGeneralFares(vehicleTypeId, companyId, totalMiles, DateTime.Now);

                                if (objFare != null)
                                {
                                    var f = objFare.FirstOrDefault();

                                    if (f.Result == "Success")
                                    {
                                        fareVal = f.totalFares.ToDecimal();

                                     //   companyFareExist = f.CompanyFareExist.ToBool();
                                    }
                                    else
                                        errorMsg = "Error";
                                }
                                else
                                    errorMsg = "Error";





                                if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                                {

                                    decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == vehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                                    if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                                    {

                                        //  rtnFare = Math.Ceiling((rtnFare);
                                        fareVal = Math.Ceiling(fareVal);
                                    }
                                }
                                else
                                {

                                    decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                                    if (roundUp > 0)
                                    {
                                        fareVal = (decimal)Math.Ceiling(fareVal / roundUp) * roundUp;
                                    
                                    }                     


                                }

                                fareVal = fareVal + (viaList.Count() * AppVars.objPolicyConfiguration.ViaPointExtraCharges.ToDecimal());


                                returnFares = fareVal;                  


                        }
                    }
                    else
                    {
                        int defaultVehicleTypeId = AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt();
                        if (vehicleTypeId.ToInt() != AppVars.objPolicyConfiguration.DefaultVehicleTypeId.ToInt())
                        {
                            IsMoreFareWise = true;


                        }


                        if (ddlPickupPlot.SelectedValue == null)
                            fromZoneId = GetOuterZoneId(tempFromPostCode).ToInt();
                        else
                            fromZoneId = ddlPickupPlot.SelectedValue.ToInt();


                        if (ddlDropOffPlot.SelectedValue == null)
                            toZoneId = GetOuterZoneId(tempToPostCode).ToInt();
                        else
                            toZoneId = ddlDropOffPlot.SelectedValue.ToInt();

                        //if (tempToLocId == 0 && string.IsNullOrEmpty(tempToPostCode))
                        //{
                        //    tempFromLocId = fromLocationId.ToInt();
                        //    if (tempFromLocId != 0)
                        //    {
                        //        tempFromPostCode = fromLocName;
                        //    }
                        //    else
                        //        tempFromPostCode = fromAddress != string.Empty ? fromAddress : fromPostCode;

                        //}
                        //else
                        //{
                        //    tempFromLocId = tempFromLocId;
                        //    tempFromPostCode = tempFromPostCode;

                        //}

                        tempToLocId = toLocationId.ToInt();
                        if (tempToLocId != 0)
                            tempToPostCode = toLocName;
                        else
                            tempToPostCode = toAddress != string.Empty ? toAddress : toPostCode;

                        int subCompanyId = ddlSubCompany.SelectedValue.ToInt();
                       

                        if (hasVia == false && (fromZoneId != 0 && toZoneId != 0) )
                        {



                            var objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => (c.FromZoneId == fromZoneId && c.ToZoneId == toZoneId) && c.Fare.VehicleTypeId == defaultVehicleTypeId && c.Fare.SubCompanyId == subCompanyId);

                            if (objPlotFare == null)
                                objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => (c.FromZoneId == toZoneId && c.ToZoneId == fromZoneId) && c.Fare.VehicleTypeId == defaultVehicleTypeId && c.Fare.SubCompanyId == subCompanyId);


                           


                            if (chkReverse.ToggleState == ToggleState.On)
                            {
                                string temp = tempFromPostCode;
                                tempFromPostCode = tempToPostCode;
                                tempToPostCode = temp;

                            }

                            tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                            tempToPostCode = General.GetPostCodeMatch(tempToPostCode);


                            milesList.Clear();
                            milesList.Add(General.CalculateDistance(tempFromPostCode, tempToPostCode));

                            if (objPlotFare != null)
                            {
                                fareVal = objPlotFare.Price.ToDecimal();


                                objPlotFare = General.GetObject<Fare_ZoneWisePricing>(c => (c.FromZoneId == toZoneId && c.ToZoneId == fromZoneId) && c.Fare.VehicleTypeId == defaultVehicleTypeId && c.Fare.SubCompanyId == subCompanyId);
                                if (objPlotFare == null)
                                {

                                    returnFares = fareVal;
                                }
                                else
                                    returnFares = objPlotFare.Price.ToDecimal();



                               // decimal mileageFares = General.GetSimpleFareRateBySubCompany(companyId, defaultVehicleTypeId, tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, true, IsZoneWise, pickupdateTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime, fromZoneId, toZoneId, ref IsMoreFareWise, ddlSubCompany.SelectedValue.ToIntorNull());


                                decimal totalMiles = milesList.Sum();
                                decimal mileageFares = 0.00m;
                                var objFare = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(defaultVehicleTypeId, companyId, totalMiles, DateTime.Now, subCompanyId);

                                if (objFare != null)
                                {
                                    var f = objFare.FirstOrDefault();

                                    if (f.Result == "Success")
                                    {
                                        mileageFares = f.totalFares.ToDecimal();

                                        //   companyFareExist = f.CompanyFareExist.ToBool();
                                    }
                                    else
                                        errorMsg = "Error";
                                }
                                else
                                    errorMsg = "Error";

                                if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                                {

                                    decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == defaultVehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                                    if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                                    {

                                        //  rtnFare = Math.Ceiling((rtnFare);
                                        mileageFares = Math.Ceiling(mileageFares);
                                    }
                                }
                                else
                                {

                                    decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                                    if (roundUp > 0)
                                    {
                                        mileageFares = (decimal)Math.Ceiling(mileageFares / roundUp) * roundUp;

                                    }
                                }



                                if (mileageFares < fareVal || fareVal == 0)
                                {
                                    fareVal = mileageFares;

                                    returnFares = fareVal;
                                }

                            }
                            else
                            {
                                if ( fareVal == 0)
                                {
                                   // decimal mileageFares = General.GetSimpleFareRateBySubCompany(companyId, defaultVehicleTypeId, tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, true, IsZoneWise, pickupdateTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime, fromZoneId, toZoneId, ref IsMoreFareWise, ddlSubCompany.SelectedValue.ToIntorNull());

                                    decimal totalMiles = milesList.Sum();

                                    var objFare = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(defaultVehicleTypeId, companyId, totalMiles, DateTime.Now, subCompanyId);

                                    if (objFare != null)
                                    {
                                        var f = objFare.FirstOrDefault();

                                        if (f.Result == "Success")
                                        {
                                            fareVal = f.totalFares.ToDecimal();

                                            //   companyFareExist = f.CompanyFareExist.ToBool();
                                        }
                                        else
                                            errorMsg = "Error";
                                    }
                                    else
                                        errorMsg = "Error";

                                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                                    {

                                        decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == defaultVehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                                        if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                                        {

                                            //  rtnFare = Math.Ceiling((rtnFare);
                                            fareVal = Math.Ceiling(fareVal);
                                        }
                                    }
                                    else
                                    {

                                        decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                                        if (roundUp > 0)
                                        {
                                            fareVal = (decimal)Math.Ceiling(fareVal / roundUp) * roundUp;

                                        }
                                    }




                                   // fareVal = mileageFares;

                                    returnFares = fareVal;
                                }

                            }


          




                        }
                        else if (hasVia == false || fromLocTypeId==Enums.LOCATION_TYPES.AIRPORT || toLocTypeId==Enums.LOCATION_TYPES.AIRPORT)
                        {

                            fareVal = General.GetSimpleFareRateBySubCompany(companyId, ddlVehicleType.SelectedValue.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, true, IsZoneWise, pickupdateTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime, fromZoneId, toZoneId, ref IsMoreFareWise,subCompanyId);
                            returnFares = fareVal;

                        }
                        else
                        {
                            milesList.Clear();


                            if (chkReverse.ToggleState == ToggleState.On)
                            {
                                string temp = tempFromPostCode;
                                tempFromPostCode = tempToPostCode;
                                tempToPostCode = temp;

                            }

                            tempFromPostCode = General.GetPostCodeMatch(tempFromPostCode);
                            tempToPostCode = General.GetPostCodeMatch(tempToPostCode);



                            //if(hasVia)
                            //    viaList = grdVia.Rows.Select(a => a.Cells["VIALOCATIONVALUE"].Value.ToStr()).ToArray<string>();

                          
                            //milesList.Add(CalculateTotalDistance(tempFromPostCode, viaList, tempToPostCode));

                            string[] viaList = new string[0];

                            if (hasVia)
                            {
                                viaList = grdVia.Rows.Select(a => a.Cells["VIALOCATIONVALUE"].Value.ToStr()).ToArray<string>();
                                milesList.Add(CalculateTotalDistance(tempFromPostCode, viaList, tempToPostCode));

                               

                            }
                            else
                            {
                                milesList.Add(General.CalculateDistance(tempFromPostCode, tempToPostCode));
                            }





                            decimal totalMiles = milesList.Sum();

                            var objFare = new TaxiDataContext().stp_CalculateGeneralFaresBySubCompany(defaultVehicleTypeId, companyId, totalMiles, DateTime.Now, subCompanyId);

                            if (objFare != null)
                            {
                                var f = objFare.FirstOrDefault();

                                if (f.Result == "Success")
                                {
                                    fareVal = f.totalFares.ToDecimal();

                                    //   companyFareExist = f.CompanyFareExist.ToBool();
                                }
                                else
                                    errorMsg = "Error";
                            }
                            else
                                errorMsg = "Error";





                            if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                            {

                                decimal startRateTillMiles = General.GetObject<Fleet_VehicleType>(c => c.Id == defaultVehicleTypeId).DefaultIfEmpty().StartRateValidMiles.ToDecimal();
                                if (startRateTillMiles > 0 && totalMiles > startRateTillMiles)
                                {

                                    //  rtnFare = Math.Ceiling((rtnFare);
                                    fareVal = Math.Ceiling(fareVal);
                                }
                            }
                            else
                            {

                                decimal roundUp = AppVars.objPolicyConfiguration.RoundUpTo.ToDecimal();

                                if (roundUp > 0)
                                {
                                    fareVal = (decimal)Math.Ceiling(fareVal / roundUp) * roundUp;

                                }


                            }

                            fareVal = fareVal + (viaList.Count() * AppVars.objPolicyConfiguration.ViaPointExtraCharges.ToDecimal());


                            returnFares = fareVal;


                        }
                    }
                   


                    if (IsMoreFareWise)
                    {

                        decimal AddedAmount = 0.00m;
                        decimal returnAddedAmount = 0.00m;
                        string op = string.Empty;


                        Gen_SysPolicy_FaresSetting objFare = General.GetObject<Gen_SysPolicy_FaresSetting>(c =>c.SysPolicyId!=null && c.VehicleTypeId == ddlVehicleType.SelectedValue.ToInt());

                        if (objFare != null)
                        {
                            op = objFare.Operator.ToStr();


                            if (objFare.IsAmountWise == false)
                            {
                                AddedAmount = (fareVal * objFare.Percentage.ToDecimal()) / 100;
                                returnAddedAmount = (returnFares * objFare.Percentage.ToDecimal()) / 100;
                            }
                            else
                            {
                                AddedAmount = objFare.Amount.ToDecimal();
                                returnAddedAmount = (returnFares * objFare.Percentage.ToDecimal()) / 100;

                            }
                        }

                        switch (op)
                        {
                            case "+":
                                fareVal = (decimal)Math.Ceiling((fareVal + AddedAmount) / 0.1m) * 0.1m;
                                returnFares = (decimal)Math.Ceiling((returnFares + returnAddedAmount) / 0.1m) * 0.1m;

                                break;

                            case "-":
                                //fareVal = fareVal - AddedAmount;
                                //returnFares = returnFares + returnAddedAmount;
                                fareVal = (decimal)Math.Ceiling((fareVal - AddedAmount) / 0.1m) * 0.1m;
                                returnFares = (decimal)Math.Ceiling((returnFares - returnAddedAmount) / 0.1m) * 0.1m;
                                break;

                            default:
                                fareVal = (decimal)Math.Ceiling((fareVal + AddedAmount) / 0.1m) * 0.1m;
                                returnFares = (decimal)Math.Ceiling((returnFares + returnAddedAmount) / 0.1m) * 0.1m;
                                break;


                            //   rtnFare = (decimal)Math.Ceiling(rtnFare / 0.5m) * 0.5m;

                        }
                    }


                    decimal peakFares = 0.00m;
                    decimal rtnPeakFares = 0.00m;

                    if (fareVal > 0)
                    {


                        pickupdateTime = dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay;
                        TimeSpan pickupTime = new TimeSpan(pickupdateTime.Value.TimeOfDay.Hours, pickupdateTime.Value.Minute, 0);
                        int pickupDay = (int)pickupdateTime.Value.DayOfWeek;



                        DateTime? returnpickupdateTime = null;
                        TimeSpan returnpickupTime = TimeSpan.Zero;
                        int returnpickupDay = 0;

                        if (opt_JReturnWay.ToggleState == ToggleState.On && dtpReturnPickupDate != null && dtpReturnPickupDate.Value != null
                            && dtpReturnPickupTime != null && dtpReturnPickupTime.Value != null)
                        {

                            returnpickupdateTime = dtpReturnPickupDate.Value.ToDate() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                            returnpickupTime = new TimeSpan(returnpickupdateTime.Value.TimeOfDay.Hours, returnpickupdateTime.Value.Minute, 0);
                            returnpickupDay = (int)pickupdateTime.Value.DayOfWeek;


                        }


                        //decimal peakFares = 0.00m;
                        //decimal rtnPeakFares = 0.00m;
                        foreach (var item in General.GetQueryable<PeakTimeSetting>(null))
                        {

                            int fromDay = item.FromDay.ToInt();
                            int tillDay = item.ToDay.ToInt();

                            TimeSpan fromTime = new TimeSpan(item.FromTime.Value.Hour, item.FromTime.Value.Minute, 0);
                            TimeSpan toTime = new TimeSpan(item.ToTill.Value.Hour, item.ToTill.Value.Minute, 0);

                            if (peakFares == 0 && fromDay <= pickupDay && (tillDay >= pickupDay || fromDay > tillDay))
                            {
                                if (
                                    (
                                    ((fromTime.Hours>12 && toTime.Hours<12) &&
                                     ((fromTime >= pickupTime && pickupTime <= toTime)
                                         || (fromTime <= pickupTime && pickupTime >= toTime)))

                                        ||
                                    ((toTime.Hours > fromTime.Hours) &&
                                     (pickupTime>=fromTime   && pickupTime <= toTime))


                                         )
                                    //(
                                    //(toTime.Hours >= 12 && fromTime <= pickupTime && toTime >= pickupTime)
                                    //||  (toTime.Hours < 12 && ((((fromDay == DateTime.Now.DayOfWeek.ToInt()) && fromTime <= pickupTime)

                                    //     || ((tillDay == DateTime.Now.DayOfWeek.ToInt()) && pickupTime <= toTime)

                                    //     || (fromTime>=pickupTime && pickupTime <= toTime)
                                    //     || (fromTime <= pickupTime && pickupTime >= toTime)
                                    //)                                 

                                    //)

                                    //)
                                    //)

                                    && (item.ZoneId==fromZoneId || item.ZoneId==null || item.ZoneId==0))

                                {


                                    if (item.ZoneId.ToInt() != 0)
                                    {

                                        if (fromZoneId != 0 && item.ZoneId.ToInt() == fromZoneId)
                                        {

                                            if (item.IsAmountWise.ToBool() == false)
                                            {

                                                peakFares = ((fareVal * item.IncrementPercent.ToDecimal()) / 100);
                                            }
                                            else
                                            {

                                                peakFares = item.Amount.ToDecimal();
                                            }

                                        }
                                    }
                                    else
                                    {
                                        if (item.IsAmountWise.ToBool() == false)
                                        {

                                            peakFares = ((fareVal * item.IncrementPercent.ToDecimal()) / 100);
                                        }
                                        else
                                        {
                                            peakFares = item.Amount.ToDecimal();

                                        }
                                    }
                                }
                            }


                            if (returnpickupdateTime != null)
                            {

                                if (rtnPeakFares == 0 && fromDay <= returnpickupDay && (tillDay >= returnpickupDay || fromDay > tillDay))
                                {
                                    if (
                                        
                                        //(
                                        //(toTime.Hours >= 12 && fromTime <= returnpickupTime && toTime >= returnpickupTime)
                                        //||

                                        //(toTime.Hours < 12 &&

                                        //(
                                        //(
                                        //((fromDay == DateTime.Now.Day) && fromTime <= returnpickupTime)

                                        // || ((tillDay == DateTime.Now.Day) && returnpickupTime <= toTime)
                                        //  || (returnpickupTime >= fromTime && returnpickupTime <= toTime)
                                        //)

                                       
                                      


                                        //)

                                        //)
                                        //)

                                          (
                                            ((fromTime.Hours > 12 && toTime.Hours < 12) &&
                                             ((fromTime >= returnpickupTime && returnpickupTime <= toTime)
                                                 || (fromTime <= returnpickupTime && returnpickupTime >= toTime)))

                                                ||
                                            ((toTime.Hours > fromTime.Hours) &&
                                             (returnpickupTime >= fromTime && returnpickupTime <= toTime))


                                           )

                                        && (item.ZoneId == toZoneId || item.ZoneId==null || item.ZoneId==0 )

                                        )
                                    {


                                        if (item.ZoneId.ToInt() != 0)
                                        {
                                            if (fromZoneId != 0 && item.ZoneId.ToInt() == toZoneId)
                                            {
                                                if (item.IsAmountWise.ToBool() == false)
                                                {
                                                    rtnPeakFares = ((returnFares * item.IncrementPercent.ToDecimal()) / 100);
                                                }
                                                else
                                                {

                                                    rtnPeakFares = item.Amount.ToDecimal();
                                                }
                                            }

                                        }
                                        else
                                        {
                                            if (item.IsAmountWise.ToBool() == false)
                                            {
                                                rtnPeakFares = ((returnFares * item.IncrementPercent.ToDecimal()) / 100);
                                            }
                                            else
                                            {
                                                rtnPeakFares = item.Amount.ToDecimal();
                                            }
                                            //  returnFares += rtnPeakFares;

                                        }

                                    }
                                }

                            }
                        }
                    }


                    fareVal += peakFares;
                    returnFares += rtnPeakFares;


                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                        fareVal = Math.Round(fareVal, 1);
                        returnFares = Math.Round(returnFares, 1);
                        // fareVal = (Math.Round((fareVal * 2), MidpointRounding.AwayFromZero)) / 2;
                        // returnFares = (Math.Round((returnFares * 2), MidpointRounding.AwayFromZero)) / 2;
                    }



                    decimal dd = fareVal.ToDecimal();


                    decimal airportPickupChrgs = AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();
                    /// Add Airport Pickup Charges If Pickup Point is From Airport...
                    if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        if (AppVars.objPolicyConfiguration.HasMultipleAirportPickupCharges.ToBool() && fromLocationId != null)
                        {
                            airportPickupChrgs = General.GetObject<Gen_SysPolicy_AirportPickupCharge>(c => c.AirportId == fromLocationId).DefaultIfEmpty().Charges.ToDecimal();
                            dd += airportPickupChrgs;
                        }
                        else
                        {
                            dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();
                        }


                    }


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

                        numReturnFare.Value = returnFares;

                        if (ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                            numReturnFare.Value -= airportPickupChrgs;


                        else if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                            numReturnFare.Value += airportPickupChrgs;


                        if (companyId != 0 && numReturnCompanyFares != null)
                        {

                            numReturnCompanyFares.Value = numCompanyFares.Value;

                        }

                        if (numReturnCustFare != null)
                        {
                            numReturnCustFare.Value = numReturnFare.Value;


                        }

                    }
                    else if (opt_WaitandReturn.ToggleState == ToggleState.On)
                    {
                        numFareRate.Value = numFareRate.Value * 2;

                        if(numCustomerFares!=null)
                            numCustomerFares.Value = numCustomerFares.Value*2;


                        if (companyId != 0 && numCompanyFares != null)
                        {
                            numCompanyFares.Value = numCompanyFares.Value*2;
                        }
                    }
                    else
                    {
                        if (numReturnFare != null)
                            numReturnFare.Value = 0;
                    }


                }

            }



            //return true;

        }


        private bool mileageError = false;

        private string lastOrigin = string.Empty;
        private string lastDestination = string.Empty;
        private decimal lastMileage = 0.00m;
        private decimal lastJourneyMileage = 0.00m;

        private void ResetBookingStatusId()
        {

            this.bookingstatusId = null;

        }

        private void btnSaveNew_Click(object sender, EventArgs e)
        {



            ResetBookingStatusId();


            SaveAndClose();
        }

        private void SaveAndClose()
        {
            if (CheckDefaultValidation())
            {
                if (Save())
                    Close();


            }

        }


        private bool CheckDefaultValidation()
        {
            try
            {

                if (txtGroupJobNo != null && txtGroupJobNo.Tag != null && !string.IsNullOrEmpty(txtGroupJobNo.AccessibleDescription))
                {
                    int noOfSeats = txtGroupJobNo.AccessibleDescription.ToInt();

                    if (noOfSeats - num_TotalPassengers.Value < 0)
                    {
                        ENUtils.ShowMessage("Available seats (" + noOfSeats + ") are not enough to Allocate " + num_TotalPassengers.Value + " Passenger(s) in this Group");
                        return false;

                    }
                }


                if (chkQuotation.Checked==false && ddlFromLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT
                    && txtFromFlightDoorNo.Text.Trim().Length == 0 && ddlPaymentType.Items.Count(c => c.Value.ToInt() == Enums.PAYMENT_TYPES.ROOM_CHARGE) > 0)
                {

                    ENUtils.ShowMessage("Required : Flight No");
                    return false;

                }


                if (ddlFromLocType.SelectedValue==null)
                {

                    ENUtils.ShowMessage("Required : Pickup Location Type");
                    return false;

                }

                if (ddlToLocType.SelectedValue == null)
                {

                    ENUtils.ShowMessage("Required : DropOff Location Type");
                    return false;

                }


                if (objMaster.PrimaryKeyValue == null && ddlPaymentType.Tag == null
                    && AppVars.listUserRights.Count(c=>c.functionId=="CHECK PAYMENT TYPE MESSAGE ON NEWBOOKING")>0)
                {
                    ddlPaymentType.Tag = "1";
                    ENUtils.ShowMessage("Check Payment Type");
                    return false;

                }


                if (chkQuotation.Checked && txtEmail.Text.Trim().Length==0 && AppVars.objPolicyConfiguration.SendDirectBookingConfirmationEmail.ToBool())
                {
                    if (DialogResult.No == MessageBox.Show("Customer Email is required for Quotation Email"+Environment.NewLine+"do you still want to save a booking without Quotation Email ?", "Warning", MessageBoxButtons.YesNo))
                    {
                        return false;
                    }
                }               




                if (chkIsCompanyRates.Checked && ddlCompany.SelectedValue.ToInt() == 0)
                {
                    ENUtils.ShowMessage(("Required : Company"));
                    return false;

                }
                else
                {
                    if (ddlCompany.SelectedValue.ToIntorNull() != null)
                    {
                        int? companyId = ddlCompany.SelectedValue.ToIntorNull();
                        Gen_Company obj = null;

                        if (txtOrderNo != null && txtOrderNo.Text.ToStr().Trim().Length == 0)
                        {
                            obj = General.GetObject<Gen_Company>(c => c.Id == companyId);

                            if (obj.HasOrderNo.ToBool() && obj.MandatoryOrderNo.ToBool())
                            {
                                ENUtils.ShowMessage(("Required : Order No"));
                                return false;
                            }

                        }


                        if (companyId != null && txtAccountBookedBy != null && txtAccountBookedBy.Text.Trim().Length == 0 && txtAccountBookedBy.Visible == true)
                        {
                            ENUtils.ShowMessage(("Required : Booked By"));
                            return false;

                        }

                        if (pnlAccpassword != null && pnlAccpassword.Visible == true)
                        {



                            if (obj == null)
                                obj = General.GetObject<Gen_Company>(c => c.Id == companyId);

                            if (obj != null)
                            {

                                string AccountPassword = obj.PasswordAccount.ToStr();

                                if (txtAccPassword.Text.ToStr().ToLower() == AccountPassword.ToStr().ToLower())
                                {
                                    if (ddlFromLocType.SelectedValue.ToInt() != Enums.LOCATION_TYPES.AIRPORT && ddlCompany.SelectedValue.ToIntorNull() != null && ddlCustomerName.Text.Trim().Length == 0)
                                    {
                                        ddlCustomerName.Text = ddlCompany.Text.Trim();


                                    }

                                    return true;
                                    //  this.SaveAndClose();
                                }
                                else
                                {
                                    ENUtils.ShowMessage(("Please Enter Correct Company Password!"));
                                    return false;
                                }
                            }


                        }
                    }
                }


                if (dtpPickupDate.Enabled == false && dtpFlightDepDate != null)
                {
                    dtpPickupDate.Value = dtpFlightDepDate.Value;

                }


                //if (string.IsNullOrEmpty(txtCustomerMobileNo.Text.Trim()) && string.IsNullOrEmpty(txtCustomerPhoneNo.Text.Trim()))
                //{
                //    ENUtils.ShowMessage("Required : Phone No");

                //    return false;
                //}

          
                    
       


                if (dtpPickupDate.Value == null || dtpPickupTime.Value == null)
                {
                    ENUtils.ShowMessage("Required : Pickup Date Time");
                    return false;
                }
                else
                {
                    if (dtpPickupDate.Value.Value.Date < DateTime.Now.Date)
                    {



                        if (DialogResult.No == RadMessageBox.Show("do you want to save the booking for previous date?", "Warning", MessageBoxButtons.YesNo))
                        {

                            return false;
                        }
                        else
                        {

                            if (ddlFromLocType.SelectedValue.ToInt() != Enums.LOCATION_TYPES.AIRPORT && ddlCompany.SelectedValue.ToIntorNull() != null && ddlCustomerName.Text.Trim().Length == 0)
                            {
                                ddlCustomerName.Text = ddlCompany.Text.Trim();


                            }

                            return true;


                        }
                    }
                    else
                    {

                        if (ddlFromLocType.SelectedValue.ToInt()!=Enums.LOCATION_TYPES.AIRPORT && ddlCompany.SelectedValue.ToIntorNull() != null && ddlCustomerName.Text.Trim().Length == 0)
                        {
                            ddlCustomerName.Text = ddlCompany.Text.Trim();


                        }

                        return true;
                    }
                }


            


            }

            catch (Exception ex)
            {

                return true;
            }







        }


        Label lblViaCustName = null;
        Label lblViaCustMobName = null;
        TextBox txtViaCustName = null;
        TextBox txtViaCustMobName = null;
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

            this.pnlMain.Controls.Add(this.pnlVia);



            // Add Via Customer Name and Mobile No Controls
            lblViaCustName = new Label();
            lblViaCustMobName = new Label();
            txtViaCustName = new TextBox();
            txtViaCustMobName = new TextBox();



            this.lblViaCustName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViaCustName.Location = new System.Drawing.Point(4, 30);
            this.lblViaCustName.Name = "lblViaCustName";
            this.lblViaCustName.Size = new System.Drawing.Size(80, 22);
            this.lblViaCustName.TabIndex = 137;
            this.lblViaCustName.Text = "Name";


            this.lblViaCustMobName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViaCustMobName.Location = new System.Drawing.Point(4, 65);
            this.lblViaCustMobName.Name = "lblViaCustMobName";
            this.lblViaCustMobName.Size = new System.Drawing.Size(80, 22);
            this.lblViaCustMobName.TabIndex = 137;
            this.lblViaCustMobName.Text = "Mobile No";



            this.txtViaCustName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViaCustName.Location = new System.Drawing.Point(90, 30);
            this.txtViaCustName.Name = "lblViaCustName";
            this.txtViaCustName.Size = new System.Drawing.Size(200, 22);
            this.txtViaCustName.TabIndex = 137;
            this.txtViaCustName.Text = "";


            this.txtViaCustMobName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtViaCustMobName.Location = new System.Drawing.Point(90, 65);
            this.txtViaCustMobName.Name = "lblViaCustName";
            this.txtViaCustMobName.Size = new System.Drawing.Size(200, 22);
            this.txtViaCustMobName.TabIndex = 137;
            this.txtViaCustMobName.Text = "";


            this.pnlVia.Controls.Add(this.lblViaCustName);
            this.pnlVia.Controls.Add(this.lblViaCustMobName);
            this.pnlVia.Controls.Add(this.txtViaCustName);
            this.pnlVia.Controls.Add(this.txtViaCustMobName);


            //


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
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtViaAddress
            // 
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
            this.lblFromViaPoint.Visible = false;
            // 
            // ddlViaFromLocType
            // 
            this.ddlViaFromLocType.Caption = null;
            this.ddlViaFromLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlViaFromLocType.Location = new System.Drawing.Point(1, 2);
            this.ddlViaFromLocType.Name = "ddlViaFromLocType";
            this.ddlViaFromLocType.Property = null;
            this.ddlViaFromLocType.ShowDownArrow = true;
            this.ddlViaFromLocType.Size = new System.Drawing.Size(100, 18);
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

            this.ddlViaFromLocType.BringToFront();

            pnlVia.BringToFront();
        }


        private void radToggleButton1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            CreateViaPanel();

            if (args.ToggleState == ToggleState.On)
            {


                //btnSelectVia.Text = "Hide Via Point";
                btnSelectVia.Text = "Hide Via Point(" + grdVia.Rows.Count + ")";
                pnlVia.Visible = true;
                //pnlBottom.Location = this.PnlNewBottomLocation;
                txtViaAddress.Select();
            }
            else
            {
                //btnSelectVia.Text = "Show Via Point";
                btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                pnlVia.Visible = false;
                pnlMain.RootElement.Opacity = 1;
                //pnlBottom.Location = this.PnlOldBottomLocation;

                ddlCustomerName.Select();

            }
        }

        private void FocusOnViaAddress()
        {
            txtViaAddress.Select();

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
                    //   txtViaAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                    txtViaAddress.Text = AppVars.objSubCompany.Address.ToStr().ToUpper().Trim();
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
            FocusOnViAddress();
        }

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

                string viaCustName = txtViaCustName.Text.Trim().ToProperCase();
                string viaMobNo = txtViaCustMobName.Text.Trim();

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
                row.Cells["FROMTYPELABEL"].Value = viaCustName;

                row.Cells["FROMTYPEVALUE"].Value = viaMobNo;

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
            ddlViaFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
            grdVia.CurrentRow = null;
            txtViaAddress.Text = string.Empty;
            txtviaPostCode.Text = string.Empty;
            ddlViaLocation.SelectedValue = null;
            txtViaCustName.Text = string.Empty;
            txtViaCustMobName.Text = string.Empty;

            txtViaAddress.Select();
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



                txtViaCustName.Text = row.Cells["FROMTYPELABEL"].Value.ToStr();
                txtViaCustMobName.Text = row.Cells["FROMTYPEVALUE"].Value.ToStr();


            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBooking();

        }


        private void SearchBooking()
        {
            try
            {


                frmSearchBooking frm = new frmSearchBooking(ddlCustomerName.Text, txtCustomerPhoneNo.Text.Trim(), txtCustomerMobileNo.Text.Trim());

                frm.IsBookingEditMode = objMaster.PrimaryKeyValue == null ? false : true;
                frm.ShowDialog();


                if (frm.IsPickDetails && frm.IsSelected)
                {

                    ddlCustomerName.Text = frm.CustomerName.ToStr().Trim();
                    txtCustomerMobileNo.Text = frm.mobileNo.ToStr().Trim();
                    txtCustomerPhoneNo.Text = frm.phoneNo.ToStr().Trim();
                }
                else
                {

                    if (frm.IsSelected)
                    {

                        if (frm.SelectedCompanyId != null)
                        {
                            chkIsCompanyRates.Checked = true;
                            ddlCompany.SelectedValue = frm.SelectedCompanyId;
                        }


                        if (ddlBookingType.SelectedValue != null)
                        {

                            frm.bookingTypeId = ddlBookingType.SelectedValue.ToInt();

                        }

                        PickBookingComplete(frm.CustomerName, frm.phoneNo, frm.mobileNo, frm.fromLocTypeId, frm.toLocTypeId, frm.fromLocId, frm.toLocId, frm.from, frm.to, frm.fare, false, frm.bookingTypeId, frm.CustEmail);

                        numCustomerFares.Value = frm.customerFare;


                        if (numCompanyFares != null)
                            numCompanyFares.Value = frm.companyFare;


                        if (frm.viaString.ToStr().Trim().Length > 0)
                        {

                            if (grdVia == null)
                            {
                                CreateViaPanel();

                            }

                            string[] viaArr = frm.viaString.ToStr().Trim().Split(new char[] { ',' });

                            grdVia.Rows.Clear();

                            GridViewRowInfo row = null;
                            for (int i = 0; i < viaArr.Count(); i++)
                            {


                                row = grdVia.Rows.AddNew();
                                row.Cells["FROMVIALOCTYPEID"].Value = Enums.LOCATION_TYPES.ADDRESS;
                                row.Cells["FROMTYPELABEL"].Value = "";

                                row.Cells["FROMTYPEVALUE"].Value = "";

                                row.Cells["VIALOCATIONID"].Value = null;
                                row.Cells["VIALOCATIONLABEL"].Value = "Address";
                                row.Cells["VIALOCATIONVALUE"].Value = viaArr[i].Replace("Via " + (i + 1) + " : ", "").Trim();
                            }

                            btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                            btnSelectVia.ButtonElement.ButtonFillElement.BackColor = Color.DarkOrange;
                            btnSelectVia.ButtonElement.ButtonFillElement.NumberOfColors = 1;
                        }
                    }
                }

                frm.Dispose();

            }
            catch (Exception ex)
            {

            }

        }






        private void ddlDriver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            int? driverId = ddlDriver.SelectedValue.ToIntorNull();
            if (objMaster != null && objMaster.PrimaryKeyValue == null && driverId != null && chkAutoDespatch.Checked == false)
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
            ResetBookingStatusId();

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

            if (ddlPaymentType.SelectedValue == null)
            {

                error += "Required : Payment Type";
            }


            // ADDED ON 19/APRIL/2016 ON REQUEST OF DOUBLE O CARS (BOOKED BY SHOULD BE MANDATORY)
            if (ddlCompany.SelectedValue != null && txtAccountBookedBy != null && txtAccountBookedBy.Text.Trim().Length == 0 && txtAccountBookedBy.Visible == true)
            {
                error += "Required : Booked By";
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

            obj.SubcompanyId = ddlSubCompany.SelectedValue.ToIntorNull();

            obj.FromLocTypeId = FromlocTypeId.ToIntorNull();
            obj.ToLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
            obj.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
            obj.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();
            obj.DriverId = ddlDriver.SelectedValue.ToIntorNull();


            obj.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
            obj.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();
            obj.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
            obj.DepartmentId = ddlDepartment != null ? ddlDepartment.SelectedValue.ToIntorNull() : null;
            obj.IsQuotation = chkQuotation.Checked;

            obj.IsCompanyWise = chkIsCompanyRates.Checked;

            obj.BookedBy = txtAccountBookedBy != null ? txtAccountBookedBy.Text.Trim() : "";


            obj.OrderNo = txtOrderNo != null ? txtOrderNo.Text.Trim() : "";
            obj.PupilNo = txtPupilNo != null ? txtPupilNo.Text.Trim() : "";


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

            if (dtpReturnPickupDate != null)
            {

                if (dtpReturnPickupDate.Value != null && dtpReturnPickupTime.Value != null)
                {
                    obj.ReturnPickupDateTime = dtpReturnPickupDate.Value.ToDateorNull() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                }
                else
                    obj.ReturnPickupDateTime = null;

                obj.ReturnDriverId = ddlReturnDriver.SelectedValue.ToIntorNull();

                if (numReturnFare != null)
                    obj.ReturnFareRate = numReturnFare.Value.ToDecimal();

            }


            obj.FareRate = numFareRate.Value.ToDecimal();


            if (numReturnFare != null)
                obj.ReturnFareRate = numReturnFare.Value.ToDecimal();

            obj.CustomerPrice = numCustomerFares.Value.ToDecimal();


            obj.CompanyPrice = numCompanyFares != null ? numCompanyFares.Value.ToDecimal() : 0.00m;

            if (lblReturnCompanyPrice != null && opt_JReturnWay.ToggleState == ToggleState.On)
            {

                obj.WaitingMins = numReturnCompanyFares.Value;
                //   obj.ReturnCustomerPrice = numReturnCustFare.Value.ToDecimal();
            }



            obj.CustomerId = ddlCustomerName.Tag.ToIntorNull();

            obj.CustomerName = customerName;

            obj.CustomerEmail = txtEmail.Text.Trim();
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


            if (AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool())
            {

                if (ddlPickupPlot.SelectedValue == null)
                    obj.ZoneId = GetZoneId(obj.FromAddress);
                else
                    obj.ZoneId = ddlPickupPlot.SelectedValue.ToIntorNull();

                if (ddlDropOffPlot.SelectedValue == null)
                    obj.DropOffZoneId = GetZoneId(obj.ToAddress);
                else
                    obj.DropOffZoneId = ddlDropOffPlot.SelectedValue.ToIntorNull();
            }




            obj.ToDoorNo = txtToFlightDoorNo.Text.Trim();
            obj.ToStreet = txtToStreetComing.Text.Trim();
            obj.ToPostCode = txtToPostCode.Text.Trim();

            obj.DistanceString = lblMap.Text;
            obj.AutoDespatch = chkAutoDespatch.Checked;


            int mins = numBeforeMinutes.Value.ToInt();

            if (mins == 0)
            {

                if (obj.FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                {
                    mins = AppVars.objPolicyConfiguration.AirportBookingExpiryNoticeInMins.ToInt() + AppVars.objPolicyConfiguration.AutoDespatchMinsBeforeDue.ToInt();
                }
                else
                {
                    mins = AppVars.objPolicyConfiguration.BookingExpiryNoticeInMins.ToInt() + AppVars.objPolicyConfiguration.AutoDespatchMinsBeforeDue.ToInt();
                }

            }

            obj.AutoDespatchTime = obj.PickupDateTime.Value.AddMinutes(-mins);

            obj.AutoDespatch = chkAutoDespatch.Checked;
            obj.IsBidding = chkBidding.Checked;

            obj.IsCommissionWise = chkIsCommissionWise.Checked;
            obj.DriverCommission = numDriverCommission.Value.ToDecimal();
            obj.DriverCommissionType = ddlCommissionType.SelectedValue.ToStr().Trim();
            obj.CallRefNo = this.CallRefNo;



            if (chkTakenByAgent != null)
            {
              

                obj.AgentCommission = numAgentCommission.Value;
                obj.JobTakenByCompany = chkTakenByAgent.Checked;
                obj.AgentCommissionPercent = numAgentCommissionPercent.Value.ToInt();
                obj.FromFlightNo = ddlAgentCommissionType.Text.Trim();
            }

            if (AppVars.objPolicyConfiguration.AutoBookingDueAlert.ToBool())
            {

                decimal mile = General.CalculateDistanceFromBaseFull(obj.FromAddress.ToStr());

                obj.DeadMileage = mile;

                if (mile > 0 && mile < 1)
                {
                    mile = 1;
                }
                else
                {
                    mile = Math.Round(mile, 0);
                }

                obj.ExtraMile = mile;

            }



            if (pnlVia != null)
            {
                string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                IList<Booking_ViaLocation> savedList = obj.Booking_ViaLocations;
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

            frmMultiBooking frm = new frmMultiBooking(obj);

            frm.ReturnCustomerFares = numReturnCustFare.Value.ToDecimal();


            //if (txtReturnSpecialReq != null)
            //{

            //}


            frm.ShowDialog();


            if (frm.Saved)
            {


                AppVars.frmMDI.RefreshRequiredDashBoard();

                //  General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
                this.Close();
            }

            frm.Dispose();
            frm = null;

            GC.Collect();

        }



        private void chkAutoDespatch_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {


            if (args.NewValue == ToggleState.On)
            {

                numBeforeMinutes.Enabled = true;

            }
            else
            {
                numBeforeMinutes.Enabled = false;

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

                    string xml = string.Empty;
                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        xml = wc.DownloadString(new Uri(url));

                    }

                    var xmlElm = System.Xml.Linq.XElement.Parse(xml);

                    street = (from elm in xmlElm.Descendants()
                              where elm.Name == "formatted_address"
                              select elm.Value).FirstOrDefault().ToStr();


                    street = street.Replace(text, "");
                    txtFromStreetComing.Text = street;


                    UpdateAutoCalculateFares();
                }

            }
            catch
            {


            }
        }



        private void FillStreetFromPostCode(RadTextBox streetTextBox, string text, ref string street)
        {
            try
            {
                if (EnablePOI)
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        street=  db.stp_GetByRoadLevelData(text, "", "", "s=").FirstOrDefault().DefaultIfEmpty().AddressLine1.ToStr().ToUpper();

                    }

                }
                else
                {
                    street = AppVars.listOfAddress.FirstOrDefault(c => c.PostalCode.ToLower() == text.ToLower()).DefaultIfEmpty().AddressLine1;
                }

                if (!string.IsNullOrEmpty(street) && street.Contains(text))
                {
                    street = street.Remove(street.IndexOf(text)).Trim();

                }

                if (string.IsNullOrEmpty(streetTextBox.Text))
                    streetTextBox.Text = street;
            }
            catch
            {


            }

        }




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

                    if (street.ToStr().Trim().Length > 0) return;

                    string url = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + text + " UK&sensor=false";
                    string xml = string.Empty;

                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        xml = wc.DownloadString(new Uri(url));

                    }

                    var xmlElm = System.Xml.Linq.XElement.Parse(xml);


                    street = (from elm in xmlElm.Descendants()
                              where elm.Name == "formatted_address"
                              select elm.Value).FirstOrDefault().ToStr();


                    street = street.Replace(text, "");


                    if (string.IsNullOrEmpty(txtToStreetComing.Text))
                        txtToStreetComing.Text = street;



                    UpdateAutoCalculateFares();

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

            InitializeHospitalPanel();


            HideAirports();

            HideStations();
            HideHotels();
            HideTowns();

            pnlHospital.Visible = toggle == ToggleState.On;

            if (toggle == ToggleState.On)
            {
                if (LastFocus == 1)
                {
                    ddlHospitalType.Text = "From";
                }

                if (LastFocus == 2)
                {

                    ddlHospitalType.Text = "To";
                }


                if (ddlHospitals.DataSource == null)
                {
                    FillKeyLocations(ddlHospitals, General.GetHospitalsList());

                }

                ddlHospitals.Focus();
            }

            ddlHospitals.CloseDropDown();
        }



        private void ShowAirports(ToggleState toggle)
        {
            InitializeAirportPanel();


            HideStations();
            HideHospitals();
            HideHotels();
            HideTowns();

            pnlAirport.Visible = toggle == ToggleState.On;

            if (toggle == ToggleState.On)
            {
                if (LastFocus == 1)
                {
                    ddlAirportType.Text = "From";
                }

                if (LastFocus == 2)
                {
                    ddlAirportType.Text = "To";
                }

                if (ddlAirPorts.DataSource == null)
                {

                    //   listofAirports= General.GetAirportsLocations();
                    FillKeyLocations(ddlAirPorts, General.GetAirportsList());

                }


                ddlAirPorts.Focus();
            }


            ddlAirPorts.CloseDropDown();

        }

        public IList GetAirportsList()
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




        private void HideAirports()
        {
            InitializeAirportPanel();

            pnlAirport.Visible = false;
            ddlAirPorts.SelectedValue = null;
            ddlAirPorts.CloseDropDown();

            btnAirport.ToggleStateChanged -= new StateChangedEventHandler(btnAirport_ToggleStateChanged);
            btnAirport.ToggleState = ToggleState.Off;
            btnAirport.ToggleStateChanged += new StateChangedEventHandler(btnAirport_ToggleStateChanged);

        }

        private void HideStations()
        {
            InitializeStationsPanel();

            pnlStations.Visible = false;
            ddlStations.SelectedValue = null;
            ddlStations.CloseDropDown();

            btnStations.ToggleStateChanged -= new StateChangedEventHandler(btnStations_ToggleStateChanged);
            btnStations.ToggleState = ToggleState.Off;
            btnStations.ToggleStateChanged += new StateChangedEventHandler(btnStations_ToggleStateChanged);

        }

        private void HideHospitals()
        {
            InitializeHospitalPanel();

            pnlHospital.Visible = false;
            ddlHospitals.SelectedValue = null;
            ddlHospitals.CloseDropDown();
            btnHospital.ToggleStateChanged -= new StateChangedEventHandler(btnHospital_ToggleStateChanged);
            btnHospital.ToggleState = ToggleState.Off;
            btnHospital.ToggleStateChanged += new StateChangedEventHandler(btnHospital_ToggleStateChanged);
        }

        private void HideHotels()
        {
            InitializeHotelPanel();

            pnlHotels.Visible = false;
            ddlHotels.SelectedValue = null;
            ddlHotels.CloseDropDown();
            btnHotels.ToggleStateChanged -= new StateChangedEventHandler(btnHotels_ToggleStateChanged);
            btnHotels.ToggleState = ToggleState.Off;
            btnHotels.ToggleStateChanged += new StateChangedEventHandler(btnHotels_ToggleStateChanged);
        }

        private void ShowStations(ToggleState toggle)
        {

            InitializeStationsPanel();

            HideAirports();

            HideHospitals();
            HideHotels();
            HideTowns();

            pnlStations.Visible = toggle == ToggleState.On;

            if (toggle == ToggleState.On)
            {
                if (LastFocus == 1)
                {
                    ddlStationType.Text = "From";
                }

                if (LastFocus == 2)
                {
                    ddlStationType.Text = "To";
                }


                if (ddlStations.DataSource == null)
                {
                    FillKeyLocations(ddlStations, General.GetStationsList());
                }

                ddlStations.Focus();


            }

            ddlStations.CloseDropDown();


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
            try
            {
                if (e.Control)
                {
                    if (e.KeyCode == Keys.S)
                    {

                        if (objMaster.Current != null && objMaster.Current.IsProcessed.ToBool() || btnSaveNew.Enabled == false)
                        {
                            return;
                        }

                        if (objMaster.Current != null && (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.CANCELLED || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.NOPICKUP))
                        {
                            if (AppVars.listUserRights.Count(c => c.formName == "frmBooking" &&
                            (c.functionId == "LOCK COMPLETED BOOKING" || c.functionId == "LOCK CANCELLED BOOKING" || c.functionId == "LOCK NOFARE BOOKING")) > 0)
                            {
                                return;
                            }
                        }


                        SaveAndClose();


                    }
                    else if (e.KeyCode == Keys.A)
                    {

                        //  if (DisableAccountCheck() == false)
                        // {

                        if (chkIsCompanyRates.Checked == false)
                        {
                            chkIsCompanyRates.Checked = true;
                            ddlCompany.ShowDropDown();
                            ddlCompany.Select();
                        }
                        else
                        {
                            chkIsCompanyRates.Checked = false;
                            ddlCompany.CloseDropDown();

                        }
                        //  }
                    }
                    //if (e.KeyCode == Keys.OemQuestion && ddlCustomerName.Text.Trim().Length == 0)
                    //{
                    //    ddlCustomerName.Text = "XXX";
                    //    FocusOnCustomer();

                    //}
                }
                else if (e.Alt)
                {
                    if (e.KeyCode == Keys.V)
                    {
                        if (btnSelectVia.ToggleState == ToggleState.Off)
                        {
                            btnSelectVia.ToggleState = ToggleState.On;
                        }
                        else
                        {
                            btnSelectVia.ToggleState = ToggleState.Off;
                        }

                    }
                    else if (e.KeyCode == Keys.M)
                    {
                        if (chkAutoDespatch.Checked)
                        {
                            chkAutoDespatch.Checked = false;

                        }
                        else
                        {

                            chkAutoDespatch.Checked = true;
                        }
                    }
                    else if (e.KeyCode == Keys.B)
                    {
                        if (chkBidding.Checked)
                        {
                            chkBidding.Checked = false;

                        }
                        else
                        {

                            chkBidding.Checked = true;
                        }

                    }
                }
               

                else
                {

                    if (e.KeyCode == Keys.F1)
                    {
                        if (txtToAddress.FocusedElement != null && txtToAddress.FocusedElement.IsFocused)
                        {
                            SetToBase();
                        }
                        else
                        {
                            SetFromBase();
                        }

                        UpdateAutoCalculateFares();
                    }
                    else if (e.KeyCode == Keys.F2)
                    {
                        InitializeAirportPanel();

                        btnAirport.ToggleState = btnHospital.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;

                        ShowAirports(btnAirport.ToggleState);

                    }

                    else if (e.KeyCode == Keys.F3)
                    {

                        InitializeStationsPanel();

                        btnStations.ToggleState = btnHospital.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;
                        ShowStations(btnStations.ToggleState);
                    }

                    else if (e.KeyCode == Keys.F5)
                    {
                        InitializeHospitalPanel();

                        btnHospital.ToggleState = btnHospital.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;

                        ShowHospitals(btnHospital.ToggleState);

                    }
                    else if (e.KeyCode == Keys.F6)
                    {

                        InitializeHotelPanel();

                        btnHotels.ToggleState = btnHotels.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;

                        ShowHotels(btnHotels.ToggleState);


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
                        SendEmail(true);

                    }
                    else if (e.KeyCode == Keys.Insert)
                    {
                        SetAsDirected();
                    }
                    else if (e.KeyCode == Keys.F10)
                    {
                        try
                        {
                            //if (DisableAccountCheck() == false)
                            //{

                            frmBookingAccountCode Acccode = new frmBookingAccountCode();
                            Acccode.ShowDialog();

                            if (Acccode.ID != 0)
                            {
                                chkIsCompanyRates.Checked = true;
                                ddlCompany.SelectedValue = Acccode.ID;
                            }
                            Acccode.Dispose();
                            //  }
                        }
                        catch (Exception ex)
                        {

                            ENUtils.ShowMessage(ex.Message);
                        }
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {



                        if (pnlAirport != null && pnlAirport.Visible)
                        {
                            HideAirports();
                            SetLastFocus();
                            return;

                        }

                        if (pnlHospital != null && pnlHospital.Visible)
                        {
                            HideHospitals();
                            SetLastFocus();
                            return;

                        }

                        if (pnlHotels != null && pnlHotels.Visible)
                        {
                            HideHotels();
                            SetLastFocus();
                            return;

                        }


                        if (pnlStations != null && pnlStations.Visible)
                        {
                            HideStations();
                            SetLastFocus();
                            return;

                        }

                        if (pnltowns != null && pnltowns.Visible)
                        {
                            HideTowns();
                            SetLastFocus();
                            return;

                        }


                        if (pnlVia == null || pnlVia.Visible == false)
                        {
                            if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to close?? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                            {
                                this.Close();
                            }
                        }
                        else
                        {
                            btnSelectVia.ToggleState = ToggleState.Off;
                        }


                    }
                    else if (e.KeyCode == Keys.F11)
                    {
                        if (chkReverse.Checked == false)
                        {
                            chkReverse.Checked = true;
                        }
                        else
                        {
                            chkReverse.Checked = false;
                        }
                    }
                    else if (e.KeyCode == Keys.F4)
                    {
                        //if (AppVars.objPolicyConfiguration.EnablePDA.ToBool())
                        //{
                        //    object o = "LoadNearestMap";
                        //    SendAsyncRequest(o);
                        //}
                        //  LoadNearestMap();

                        InitializetownsPanel();

                        btnTowns.ToggleState = btnTowns.ToggleState == ToggleState.On ? ToggleState.Off : ToggleState.On;

                        Showtowns(btnTowns.ToggleState);



                    }
                    else if (e.KeyCode == Keys.F12)
                    {

                        LoadNearestMap();
                    }
                    else if (e.KeyCode == Keys.Home)
                    {


                        if (objMaster.Current != null && objMaster.Current.IsProcessed.ToBool() || btnSaveNew.Enabled == false)
                        {
                            return;
                        }


                        if (objMaster.Current != null && (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.CANCELLED || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.NOPICKUP))
                        {
                            if (AppVars.listUserRights.Count(c => c.formName == "frmBooking" &&
                            (c.functionId == "LOCK COMPLETED BOOKING" || c.functionId == "LOCK CANCELLED BOOKING" || c.functionId == "LOCK NOFARE BOOKING")) > 0)
                            {

                                return;
                            }
                        }


                        if (objMaster.Current == null || objMaster.Current.BookingStatusId == Enums.BOOKINGSTATUS.WAITING)
                            this.bookingstatusId = Enums.BOOKINGSTATUS.WAITING;

                        else
                            ResetBookingStatusId();

                        FocusOnCustomer();



                        SaveAndClose();

                    }
                    else if (e.KeyCode == Keys.End)
                    {

                        if (objMaster.Current != null && objMaster.Current.IsProcessed.ToBool() || btnSaveNew.Enabled == false)
                        {
                            return;
                        }


                        if (objMaster.Current != null && (objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.DISPATCHED || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.CANCELLED || objMaster.Current.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.NOPICKUP))
                        {
                            if (AppVars.listUserRights.Count(c => c.formName == "frmBooking" && c.functionId == "LOCK COMPLETED BOOKING") > 0)
                            {
                                return;
                            }
                        }

                        FocusOnCustomer();

                        //if (AppVars.listUserRights.Count(c => c.functionId == "DISABLE PLOT WISE FARE LIST" && c.formName == "frmFares") == 0)
                        //{
                        chkBidding.Checked = false;
                        chkAutoDespatch.Checked = false;
                        //  }

                        SaveAndClose();


                        //if (AppVars.listUserRights.Count(c => c.functionId == "DISABLE PLOT WISE FARE LIST" && c.formName == "frmFares") > 0
                        //    &&

                        //    objMaster.PrimaryKeyValue != null && objMaster.Errors.Count == 0 && objMaster.Current.ZoneId != null)
                        //{



                        //    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard)
                        //        .ReleaseJob(objMaster.Current.Gen_Zone1.ShortName, objMaster.Current.Id, ref frm, true, objMaster.Current.Fleet_VehicleType.DefaultIfEmpty().VehicleType, objMaster.Current.FromAddress);


                        //    Close();
                        //    new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);


                        //}
                        //else
                        //{

                        //   Close();
                        // }


                    }
                }
            }
            catch (Exception ex)
            {


            }

        }


        private void SetLastFocus()
        {

            if (LastFocus == 1)
            {
                FocusOnFromAddress();

            }
            else if (LastFocus == 2)
            {
                FocusOnToAddress();

            }
            else
            {
                FocusOnCustomer();

            }


        }

        private void SetAsDirected()
        {
            if (txtToAddress.FocusedElement.IsFocused)
            {
                txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = "AS DIRECTED";
                SetDropOffZone("AS DIRECTED");
                FocusOnCustomer();
                txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                InitializeJourneyTimePanel();

            }

        }

        private void InitializeJourneyTimePanel()
        {

            if (lblJourneyTime != null) return;

            this.lblJourneyTime = new Telerik.WinControls.UI.RadLabel();
            this.numJourneyTime = new Telerik.WinControls.UI.RadSpinEditor();

            ((System.ComponentModel.ISupportInitialize)(this.lblJourneyTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJourneyTime)).BeginInit();



            // 
            // lblJourneyTime
            // 
            this.lblJourneyTime.BackColor = this.pnlOtherCharges.BackColor;
            this.lblJourneyTime.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.lblJourneyTime.ForeColor = System.Drawing.Color.Black;
            this.lblJourneyTime.Location = new System.Drawing.Point(350, 5);
            this.lblJourneyTime.Name = "lblJourneyTime";
            this.lblJourneyTime.Visible = true;
            // 
            // 
            // 
            this.lblJourneyTime.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblJourneyTime.Size = new System.Drawing.Size(149, 20);
            this.lblJourneyTime.TabIndex = 240;
            this.lblJourneyTime.Text = "Journey Time(mins)";
            this.lblJourneyTime.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // numJourneyTime
            // 
            this.numJourneyTime.EnableKeyMap = true;
            this.numJourneyTime.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numJourneyTime.InterceptArrowKeys = false;
            this.numJourneyTime.Visible = true;
            this.numJourneyTime.Location = new System.Drawing.Point(500, 5);
            this.numJourneyTime.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numJourneyTime.Name = "numJourneyTime";
            // 
            // 
            // 
            this.numJourneyTime.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numJourneyTime.ShowBorder = true;
            this.numJourneyTime.ShowUpDownButtons = false;
            this.numJourneyTime.Size = new System.Drawing.Size(64, 24);
            this.numJourneyTime.TabIndex = 239;
            this.numJourneyTime.TabStop = false;
            this.numJourneyTime.DecimalPlaces = 0;
            this.numJourneyTime.SpinElement.ValueChanging += new ValueChangingEventHandler(SpinElement_ValueChanging);
            ((Telerik.WinControls.UI.RadSpinElement)(this.numJourneyTime.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numJourneyTime.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numJourneyTime.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numJourneyTime.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            this.pnlPaymentMode.Controls.Add(lblJourneyTime);
            this.pnlPaymentMode.Controls.Add(numJourneyTime);


            ((System.ComponentModel.ISupportInitialize)(this.lblJourneyTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numJourneyTime)).EndInit();
           // this.ResumeLayout(false);

           
          

        }

        private void GetFareByJourneyTime(decimal journeyMins)
        {

            if (IsDisplayingRecord) return;

            try
            {

                int vehicleTypeId = ddlVehicleType.SelectedValue.ToInt();
                int companyId = ddlCompany.SelectedValue.ToInt();


                decimal fares = 0.00m;
                decimal agentFares = 0.00m;
                decimal companyFares = 0.00m;

                using (TaxiDataContext db = new TaxiDataContext())
                {

                  //  var objFare = db.Fares.FirstOrDefault(c => c.VehicleTypeId == vehicleTypeId && (companyId == 0 || c.CompanyId == companyId));
                    var objFare = db.Fares.FirstOrDefault(c => c.VehicleTypeId == vehicleTypeId && ((companyId == 0 && c.CompanyId==null) || c.CompanyId == companyId));

                    if (objFare != null)
                    {

                        decimal charges = journeyMins * objFare.PerMinJourneyCharges.ToDecimal();

                        if (companyId != 0)
                        {
                            var objCompany = db.Gen_Companies.FirstOrDefault(c => c.Id == companyId);

                            if (objCompany != null)
                            {
                                if (objCompany.IsAgent.ToBool() && ddlPaymentType.SelectedValue.ToInt()==Enums.PAYMENT_TYPES.CASH)
                                {
                                    companyFares = charges;

                                       if (objCompany.IsAmountWiseComm.ToBool())
                                           agentFares =  objCompany.CommissionPerBooking.ToDecimal();
                                  
                                      else
                                           agentFares = (charges * objCompany.CommissionPerBooking.ToDecimal()) / 100;


                                       agentFares = Math.Round(agentFares * 4, MidpointRounding.ToEven) / 4;
                                    
                                    fares = companyFares - agentFares;

                                }
                                else
                                {
                                    companyFares = charges;

                                    if (objCompany.DriverFareReductionType.ToStr().ToLower() == "percent")
                                        fares = charges - (charges * objCompany.DriverFareReductionValue.ToDecimal()) / 100;
                                    else
                                        fares = charges - objCompany.DriverFareReductionValue.ToDecimal();


                                    fares = Math.Round(fares * 4, MidpointRounding.ToEven) / 4;
                                }



                            }


                        }
                        else
                        {

                            fares = charges;

                        }


                        numFareRate.Value = fares;

                        if (numReturnFare != null)
                        {
                            numReturnFare.Value = fares;

                        }


                        numCustomerFares.Value = fares;

                        if (numReturnCustFare != null)
                        {
                            numReturnCustFare.Value = fares;
                        }


                        if (numAgentCommission != null)
                        {
                            numAgentCommission.Value = agentFares;

                        }


                        if (numCompanyFares != null)
                        {
                            numCompanyFares.Value = companyFares;

                        }

                        if (numReturnCompanyFares != null)
                        {
                            numReturnCompanyFares.Value = companyFares;

                        }



                    }


                }

            }
            catch
            {


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
                PickFromBase();

                //txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                //txtFromAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                //txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);
            }

        }


        private void PickFromBase()
        {
            txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            txtFromAddress.Text = AppVars.objSubCompany.Address.ToStr().ToUpper().Trim();
            txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


        }


        private void SetToBase()
        {

            if (ddlToLocType.SelectedValue.ToInt() != Enums.LOCATION_TYPES.BASE)
            {
                ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.BASE;
            }
            else
            {
                PickToBase();
            }

        }


        private void PickToBase()
        {
            txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            txtToAddress.Text = AppVars.objSubCompany.Address.ToStr().ToUpper().Trim();
            txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

        }



        private void btnCancelBooking_Click(object sender, EventArgs e)
        {

            try
            {


                if (objMaster.Current.BookingStatusId.ToInt() != Enums.BOOKINGSTATUS.CANCELLED)
                {

                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to Cancel Booking ?", "Cancel Booking", MessageBoxButtons.YesNo))
                    {

                        if (objMaster.PrimaryKeyValue != null)
                        {

                            frmCancelReason frm = new frmCancelReason(objMaster.PrimaryKeyValue.ToLong(), objMaster.Current.CancelReason.ToStr());
                            frm.ShowDialog();
                            frm.Dispose();

                            GC.Collect();


                          //  RefreshBookingList();
                          //  AppVars.frmMDI.RefreshDashBoard();

                        }

                    }
                }
                else
                {
                    if (objMaster.PrimaryKeyValue != null)
                    {

                        frmCancelReason frm = new frmCancelReason(objMaster.PrimaryKeyValue.ToLong(), objMaster.Current.CancelReason.ToStr());
                        frm.ShowDialog();
                        frm.Dispose();
                    }
                }

            }

            catch (Exception ex)
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

            if (ddlBookingType.SelectedValue != null)
                SetBookingTypeDetails(ddlBookingType.SelectedValue.ToInt());
        }

        private void ddlCustomerName_Validated(object sender, EventArgs e)
        {
            SetCustomerNameInProperCase(ddlCustomerName.Text.ToStr().Trim());        
            
           
        }

        private void SetCustomerNameInProperCase(string customerName)
        {
            ddlCustomerName.Text = customerName.ToUpper();


           //  SetAccountByCustomer();

        }


        //private void SetAccountByCustomer()
        //{
        //    if (ddlPaymentType.Items.Count(c => c.Text.Contains("Room Charge")) > 0)
        //    {

        //        if (ddlCustomerName.Text.Trim().Length > 0 && ddlCompany.SelectedValue == null)
        //        {


        //            int? companyId = General.GetObject<Customer>(c => c.CompanyId != null && c.Name.ToUpper() == ddlCustomerName.Text.Trim()).DefaultIfEmpty().CompanyId;


        //            if (companyId != null)
        //            {
        //                chkIsCompanyRates.Checked = true;
        //                ddlCompany.SelectedValue = companyId;

        //            }

        //        }
        //    }

        //}





        private void txtviaPostCode_TextChanged(object sender, EventArgs e)
        {

            if (MapType == Enums.MAP_TYPE.NONE) return;

            AutoCompleteTextBox viaPostCode = (AutoCompleteTextBox)sender;


            string temp = string.Empty;
            string text = viaPostCode.Text;
            //  if (text.Length > 0)
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
            try
            {

                if (e.KeyCode == Keys.Enter)
                {

                    if (txtFromAddress.Text == "BASX")
                    {
                        ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.BASE;

                    }

                    else if (AppVars.keyLocations.Contains(txtFromAddress.Text.ToStr().ToLower().Trim()) && txtFromAddress.ListBoxElement.Items.Count == 1)
                    {
                        txtFromAddress.SelectedItem = txtFromAddress.ListBoxElement.Items[0].ToStr();
                        txtFromAddress.Text = txtFromAddress.SelectedItem;

                    }

                    // DetachFromLeaveEvent();
                    FocusOnFromDoor();
                    // AttachFromLeaveEvent();

                }
                //if (e.KeyCode == Keys.Home)
                //{
                //    ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.BASE;
                //    txtFromAddress.Text = AppVars.objPolicyConfiguration.BaseAddress.ToStr().Trim();
                //}
                if (e.KeyCode == Keys.Up && !txtFromAddress.ListBoxElement.Visible)
                {
                    SendKeys.Send("{Left}");
                }
                else if (e.KeyCode == Keys.Left)
                {
                    int Position = txtFromAddress.SelectionStart;
                    if (Position == 0)
                    {

                        FocusOnFromDoor();
                    }
                }
                else if (e.KeyCode == Keys.Down && !txtFromAddress.ListBoxElement.Visible)
                {
                    txtSpecialRequirements.Focus();
                }
                else if (e.KeyCode == Keys.Right)
                {
                    if (txtFromAddress.Text.Length == 0)
                    {
                        FocusOnToAddress();
                    }
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ddlAirPorts.Focus();
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

            txtFromAddress.Tag = txtFromAddress.Text;
        }

        private void txtToAddress_Enter(object sender, EventArgs e)
        {
            LastFocus = 2;
            txtToAddress.Tag = txtToAddress.Text;

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




        private void txtToAddress_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {

                if (e.KeyCode == Keys.Enter)
                {

                    if (txtToAddress.Text == "BASX")
                    {
                        ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.BASE;
                    }
                    else if (AppVars.keyLocations.Contains(txtToAddress.Text.ToStr().ToLower().Trim()) && txtToAddress.ListBoxElement.Items.Count == 1)
                    {
                        txtToAddress.SelectedItem = txtToAddress.ListBoxElement.Items[0].ToStr();
                        txtToAddress.Text = txtToAddress.SelectedItem;
                    }
                    // CalculateTotalFares();
                    FocusOnToDoor();
                }
                //else if (e.KeyCode == Keys.Up && !txtFromAddress.ListBoxElement.Visible)
                //{
                //    if (!txtToAddress.ListBoxElement.Visible)
                //    {
                //      //  FocusOnToDoor();
                //        SendKeys.Send("{Left}");
                //    }
                //}
                //else if (e.KeyCode == Keys.Down)
                //{

                //}
                else if (e.KeyCode == Keys.Left)
                {
                    int Position = txtToAddress.SelectionStart;
                    if (Position == 0)
                    {
                        FocusOnFromAddress();
                    }
                }
                //else if (e.KeyCode == Keys.Down)
                //{


                //   // int a = txtToAddress.SelectionStart = txtToAddress.Text.Length;
                //    if (!txtToAddress.ListBoxElement.Visible)
                //    {


                //        txtToFlightDoorNo.Focus();


                //    }
                //}
            }
            catch (Exception ex)
            {


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

            try
            {
                ResetBookingStatusId();

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

                if (ddlPaymentType.SelectedValue == null)
                {

                    error += "Required : Payment Type";
                }

                // ADDED ON 19/APRIL/2016 ON REQUEST OF DOUBLE O CARS (BOOKED BY SHOULD BE MANDATORY)
                if (ddlCompany.SelectedValue != null && txtAccountBookedBy != null && txtAccountBookedBy.Text.Trim().Length == 0 && txtAccountBookedBy.Visible == true)
                {
                    error += "Required : Booked By";
                }


                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }


                Booking obj = new Booking();





                obj.SubcompanyId = ddlSubCompany.SelectedValue.ToIntorNull();
                obj.BookingTypeId = ddlBookingType.SelectedValue.ToIntorNull();

                int FromlocTypeId = ddlFromLocType.SelectedValue.ToInt();
                int TolocTypeId = ddlToLocType.SelectedValue.ToInt();

                obj.BookingDate = DateTime.Now;

                obj.FromLocTypeId = FromlocTypeId.ToIntorNull();
                obj.ToLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();
                obj.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
                obj.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();


                if (ddlReturnFromAirport != null)
                    obj.ReturnFromLocId = ddlReturnFromAirport.SelectedValue.ToIntorNull();

                obj.DriverId = ddlDriver.SelectedValue.ToIntorNull();

                obj.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                obj.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();
                obj.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                obj.DepartmentId = ddlDepartment != null ? ddlDepartment.SelectedValue.ToIntorNull() : null;


                obj.IsQuotation = chkQuotation.Checked;

                obj.IsCompanyWise = chkIsCompanyRates.Checked;


                obj.BookedBy = txtAccountBookedBy != null ? txtAccountBookedBy.Text.Trim() : "";


                obj.OrderNo = txtOrderNo != null ? txtOrderNo.Text.Trim() : "";
                obj.PupilNo = txtPupilNo != null ? txtPupilNo.Text.Trim() : "";


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


                if (dtpReturnPickupDate != null)
                {

                    if (dtpReturnPickupDate.Value != null && dtpReturnPickupTime.Value != null)
                    {
                        obj.ReturnPickupDateTime = dtpReturnPickupDate.Value.ToDateorNull() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                    }
                    else
                        obj.ReturnPickupDateTime = null;

                    obj.ReturnDriverId = ddlReturnDriver.SelectedValue.ToIntorNull();
                }


                obj.FareRate = numFareRate.Value.ToDecimal();


                if (numCompanyFares != null)
                    obj.CompanyPrice = numCompanyFares.Value.ToDecimal();


                obj.CustomerPrice = numCustomerFares.Value.ToDecimal();

                if (numReturnFare != null)
                    obj.ReturnFareRate = numReturnFare.Value.ToDecimal();


                if (lblReturnCompanyPrice != null && opt_JReturnWay.ToggleState == ToggleState.On)
                {
                    obj.WaitingMins = numReturnCompanyFares.Value;
                    //  obj.ReturnCustomerPrice = numReturnCustFare.Value.ToDecimal();
                }

                obj.CustomerId = ddlCustomerName.Tag.ToIntorNull();

                obj.CustomerName = customerName;
                obj.CustomerPhoneNo = telephoneNo;
                obj.CustomerMobileNo = MobileNo;
                obj.CustomerEmail = txtEmail.Text.Trim();

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


                if (AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool())
                {

                    if (ddlPickupPlot.SelectedValue == null)
                        obj.ZoneId = GetZoneId(obj.FromAddress);
                    else
                        obj.ZoneId = ddlPickupPlot.SelectedValue.ToIntorNull();

                    if (ddlDropOffPlot.SelectedValue == null)
                        obj.DropOffZoneId = GetZoneId(obj.ToAddress);
                    else
                        obj.DropOffZoneId = ddlDropOffPlot.SelectedValue.ToIntorNull();
                }


                obj.ToDoorNo = txtToFlightDoorNo.Text.Trim();
                obj.ToStreet = txtToStreetComing.Text.Trim();
                obj.ToPostCode = txtToPostCode.Text.Trim();


                obj.IsCommissionWise = chkIsCommissionWise.Checked;
                obj.DriverCommission = numDriverCommission.Value.ToDecimal();
                obj.DriverCommissionType = ddlCommissionType.SelectedValue.ToStr().Trim();
                obj.CallRefNo = this.CallRefNo;

                obj.BookedBy = txtAccountBookedBy != null ? txtAccountBookedBy.Text.Trim() : "";


                if (chkTakenByAgent != null)
                {


                    obj.AgentCommission = numAgentCommission.Value;
                    obj.JobTakenByCompany = chkTakenByAgent.Checked;
                    obj.AgentCommissionPercent = numAgentCommissionPercent.Value.ToInt();
                    obj.FromFlightNo = ddlAgentCommissionType.Text.Trim();
                }


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


                obj.AutoDespatch = chkAutoDespatch.Checked;


                int mins = numBeforeMinutes.Value.ToInt();

                if (mins == 0)
                {

                    if (obj.FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                    {
                        mins = AppVars.objPolicyConfiguration.AirportBookingExpiryNoticeInMins.ToInt() + AppVars.objPolicyConfiguration.AutoDespatchMinsBeforeDue.ToInt();
                    }
                    else
                    {
                        mins = AppVars.objPolicyConfiguration.BookingExpiryNoticeInMins.ToInt() + AppVars.objPolicyConfiguration.AutoDespatchMinsBeforeDue.ToInt();
                    }

                }

                obj.AutoDespatchTime = obj.PickupDateTime.Value.AddMinutes(-mins);

                obj.AutoDespatch = chkAutoDespatch.Checked;
                obj.IsBidding = chkBidding.Checked;

                if (AppVars.objPolicyConfiguration.AutoBookingDueAlert.ToBool())
                {

                    decimal mile = General.CalculateDistanceFromBaseFull(obj.FromAddress.ToStr());

                    obj.DeadMileage = mile;

                    if (mile > 0 && mile < 1)
                    {
                        mile = 1;
                    }
                    else
                    {
                        mile = Math.Round(mile, 0);
                    }

                    obj.ExtraMile = mile;

                }




                obj.DistanceString = lblMap.Text;

                if (pnlVia != null)
                {
                    string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                    IList<Booking_ViaLocation> savedList = obj.Booking_ViaLocations;
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


                frmMultiVehicleBooking frm = new frmMultiVehicleBooking(obj);



                if (txtReturnSpecialReq != null)
                {
                    frm.ReturnSpecialReq = txtReturnSpecialReq.Text.Trim();
                    frm.ReturnCustomerFares = numReturnCustFare.Value.ToDecimal();


                }

                //if (obj.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
                //{
                //    if (!string.IsNullOrEmpty(txtReturnFrom.Text.Trim()))
                //    {
                //        obj.ReturnFromAddress = txtReturnFrom.Text.Trim();
                //    }


                //    if (!string.IsNullOrEmpty(txtReturnTo.Text.Trim()))
                //    {
                //        obj.ReturnToAddress = txtReturnTo.Text.Trim();
                //    }
                //    else if (ddlReturnTo.SelectedValue != null)
                //    {
                //        obj.ReturnToLocIdv = ddlReturnTo.SelectedValue.ToIntorNull();

                //    }
                //}

                frm.ShowDialog();


                if (frm.Saved)
                {



                    AppVars.frmMDI.RefreshRequiredDashBoard();

                    // General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
                    this.Close();
                }

                frm.Dispose();

                frm = null;

                GC.Collect();
            }
            catch (Exception ex)
            {


            }
        }




        bool IsAutoCalcFares = false;
        private void chkReverse_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            //  CancelWebClientAsync();



            if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
            {
                IsAutoCalcFares = true;
                AppVars.objPolicyConfiguration.AutoCalculateFares = false;

            }

            SetReverseAddress(ddlFromLocType.SelectedValue.ToIntorNull(), ddlToLocType.SelectedValue.ToIntorNull(),
                             ddlFromLocation.SelectedValue.ToIntorNull(), ddlToLocation.SelectedValue.ToIntorNull(), txtFromFlightDoorNo.Text.ToStr(),
                               txtFromAddress.Text.ToStr(), txtFromPostCode.Text.ToStr(), txtFromStreetComing.Text.ToStr(), txtToFlightDoorNo.Text.ToStr(), txtToAddress.Text.ToStr(),
                               txtToStreetComing.Text.ToStr(), txtToPostCode.Text.ToStr());



            AppVars.objPolicyConfiguration.AutoCalculateFares = IsAutoCalcFares;

            if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
            {
                CalculateAutoFares();

            }
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

            if (fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
            {
                txtFromFlightDoorNo.Text = fromDoorNo;

                this.txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtFromAddress.Text = fromAddress;

                SetPickupZone(fromAddress.ToStr().ToUpper());
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

                txtFromPostCode.Text = fromPostCode;
                SetPickupZone(fromPostCode.ToStr().ToUpper());


                txtFromFlightDoorNo.Text = fromDoorNo;
                txtFromStreetComing.Text = fromStreet;

            }
            else
            {
                ddlFromLocation.SelectedValue = fromLocId;
            }


            if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId.ToInt() == Enums.LOCATION_TYPES.BASE)
            {
                this.txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                txtToAddress.Text = toAddress;
                SetDropOffZone(toAddress.ToStr().ToUpper());
                this.txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                txtToFlightDoorNo.Text = toDoorNo;

            }
            else if (toLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
            {

                txtToPostCode.Text = toPostCode;

                SetDropOffZone(toPostCode);
                txtToFlightDoorNo.Text = toDoorNo;
                txtToStreetComing.Text = toStreet;
            }
            else
            {
                ddlToLocation.SelectedValue = toLocId;
            }




            if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
            {
                UpdateAutoCalculateFares();
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
                else
                {

                    FocusOnCustomer();
                }

            }
            else if (e.KeyCode == Keys.Left)
            {
                txtFromFlightDoorNo.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtFromAddress.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtToAddress.Focus();
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
                if ((LocTypeId == Enums.LOCATION_TYPES.ADDRESS || LocTypeId == Enums.LOCATION_TYPES.BASE)

                     )
                {
                    SetPickupZone(txtFromAddress.Text);



                    // FocusToPassenger();

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
                    SetPickupZone(ddlFromLocation.Text);
                    FocusOnFromStreet();
                }
                else
                {
                    SetPickupZone(ddlFromLocation.Text);

                    FocusToPassenger();

                    //if ((toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE))
                    //{
                    //    FocusOnToAddress();

                    //}
                    //else if ((toLocTypeId == Enums.LOCATION_TYPES.POSTCODE))
                    //{
                    //    FocusOnToPostCode();

                    //}
                    //else
                    //{

                    //    FocusOnToLocation();
                    //}
                }

            }
            else if (e.KeyCode == Keys.Right)
            {
                txtToFlightDoorNo.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtFromAddress.Focus();
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
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    //string telNo = txtCustomerPhoneNo.Text.ToStr().Trim();
                    //string mobNo = txtCustomerMobileNo.Text.ToStr().Trim();

                    //if (!string.IsNullOrEmpty(telNo) && !string.IsNullOrEmpty(mobNo))
                    //{

                    //    FocusOnPickupDate();
                    //}
                    //else if (string.IsNullOrEmpty(telNo) && string.IsNullOrEmpty(mobNo))
                    //{
                    //    FocusOnTelNo();
                    //}
                    //else if (!string.IsNullOrEmpty(telNo) && string.IsNullOrEmpty(mobNo))
                    //{
                    //    FocusOnPickupDate();
                    //}

                    //else if (string.IsNullOrEmpty(telNo) && !string.IsNullOrEmpty(mobNo))
                    //{
                    //    FocusOnPickupDate();
                    //}
                    FocusOnPickupTime();



                }
                else if (e.KeyCode == Keys.Up)
                {

                    if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS || ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.BASE)
                    {

                        txtToAddress.Focus();
                    }
                    else if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.POSTCODE)
                    {
                        FocusOnToPostCode();
                    }
                    else
                    {
                        FocusOnToDoor();

                    }
                }
                else if (e.KeyCode == Keys.Left)
                {

                    int Position = ddlCustomerName.SelectionStart;
                    if (Position == 0)
                    {
                        ddlVehicleType.Focus();
                    }
                }
                else if (e.KeyCode == Keys.Down)
                {
                    txtCustomerPhoneNo.Focus();
                }
            }
            catch (Exception ex)
            {


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

        private void FocusOnReturnPickupTime()
        {
            if (dtpReturnPickupTime != null)
                dtpReturnPickupTime.Focus();

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
            else if (e.KeyCode == Keys.Up)
            {
                ddlCustomerName.Focus();
            }
            else if (e.KeyCode == Keys.Down)
            {
                txtCustomerMobileNo.Focus();
            }
        }

        private void txtCustomerMobileNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                FocusOnPickupDate();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtCustomerPhoneNo.Focus();
            }

            else if (e.KeyCode == Keys.Left)
            {
                if (txtCustomerMobileNo.SelectionStart == 0)
                {
                    numTotalLuggages.Focus();
                }

            }
            else if (e.KeyCode == Keys.Down)
            {
                txtEmail.Focus();
            }
        }

        private void dtpPickupDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnPickupTime();

                SetReturnPickupDate();
                dtpPickupDate.Tag = null;

            }
            else if (e.KeyCode == Keys.Left)
            {

                if (dtpPickupDate.Tag.ToStr() == "right")
                    dtpPickupDate.Tag = null;

                int position = dtpPickupDate.DateTimePickerElement.TextBoxElement.TextBoxItem.SelectionStart;
                if (position == 0)
                {
                    if (dtpPickupDate.Tag == null || dtpPickupDate.Tag.ToStr() == "right")
                        dtpPickupDate.Tag = "left";
                    else if (dtpPickupDate.Tag.ToStr() == "left")
                        txtEmail.Focus();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (dtpPickupDate.Tag.ToStr() == "left")
                    dtpPickupDate.Tag = null;

                int position = dtpPickupDate.DateTimePickerElement.TextBoxElement.TextBoxItem.SelectionStart;
                if (position == 6)
                {
                    if (dtpPickupDate.Tag == null || dtpPickupDate.Tag.ToStr() == "left")
                        dtpPickupDate.Tag = "right";
                    else if (dtpPickupDate.Tag.ToStr() == "right")
                        FocusOnPickupTime();
                }
            }
            else
                dtpPickupDate.Tag = null;

        }

        private void dtpPickupTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpPickupTime.Tag = null;
                // FocusOnFare();
                FocusToPassenger();
            }
            else if (e.KeyCode == Keys.Left)
            {
                int position = dtpPickupTime.DateTimePickerElement.TextBoxElement.TextBoxItem.SelectionStart;
                if (position == 0)
                {
                    if (dtpPickupTime.Tag == null || dtpPickupTime.Tag.ToStr() == "right")
                        dtpPickupTime.Tag = "left";
                    else
                        FocusOnPickupDate();
                    // ddlCustomerName.Focus();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {

                if (dtpPickupTime.DateTimePickerElement.TextBoxElement.TextBoxItem.SelectionStart == 3)
                {
                    if (dtpPickupTime.Tag == null || dtpPickupTime.Tag.ToStr() == "left")
                        dtpPickupTime.Tag = "right";
                    else
                        FocusOnFare();
                    // ddlCustomerName.Focus();
                }
            }
            else
                dtpPickupTime.Tag = null;
        }


        private void FocusOnSave()
        {
            btnSaveNew.ButtonElement.Focus();

        }

        private void ddlDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //btnSaveOn.TabIndex = 234;
                //btnSaveAndClose.TabIndex = 235;
                //btnSaveNew.TabIndex = 236;
                //btnSaveAndNew.TabIndex = 238;
                radPanel3.Focus();
                SendKeys.Send("{TAB}");


                //ddlDriver_Leave(ddlDriver, new EventArgs());




            }

        }






        void TextBoxItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnDriver();
            }
            else if (e.KeyCode == Keys.Up)
            {

                FocusOnPickupTime();
                // ddlCustomerName.Focus();

            }

            else if (e.KeyCode == Keys.Left)
            {
                int position = numFareRate.SpinElement.TextBoxItem.SelectionStart;
                if (position == 0)
                {
                    FocusOnPickupTime();
                    // ddlCustomerName.Focus();
                }
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
            if (opt_JOneWay.ToggleState == ToggleState.Off)
            {
                SetJourneyWise(args.NewValue == ToggleState.On ? ToggleState.Off : ToggleState.On);
            }

            SetReturnAirportJob(args.NewValue);

            FocusToPassenger();

            if (args.NewValue == ToggleState.On && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
            {
                UpdateAutoCalculateFares();
            }
        }

        int companyPricePercentage = 0;
        decimal drvFareReductionValue = 0.00m;
        string drvFareReductionType = "Amount";


        private void ShowSecondaryPaymentType(bool show)
        {
            if (chkIsCommissionWise.Enabled == true || (objMaster.Current!=null && objMaster.Current.ToAddress.ToStr()=="AS DIRECTED") || (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt()==4 ||  AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt()==7))
                return;


            chkSecondaryPaymentType.Visible = show;
            numCashPaymentFares.Visible = show;


        }

        private void ShowPaymentReference()
        {
            if (AppVars.objPolicyConfiguration.BookingPaymentDetailsType.ToInt() == 1)
            {

                txtPaymentReference.Visible = true;
                lblPaymentRef.Visible = true;

            }
        }

        private void ClearBookedByDataSource()
        {

            if (txtAccountBookedBy != null)
            {
                txtAccountBookedBy.AutoCompleteCustomSource.Clear();
            

            }

        }


        private void ClearOrderNoDataSource()
        {

            if (txtOrderNo != null)
            {
                txtOrderNo.AutoCompleteCustomSource.Clear();


            }

        }


        private void SetPaymentTypeByAccount(List<Gen_Company_PaymentType> listofPaymentTypes)
        {
            if (listofPaymentTypes.Count > 0)
            {

                foreach (var item in ddlPaymentType.Items)
                {

                    if (listofPaymentTypes.Count(c => c.PaymentTypeId == item.Value.ToInt()) == 0)
                    {

                        item.Enabled = false;


                        if (ddlPaymentType.SelectedValue.ToInt() == item.Value.ToInt())
                        {
                            ddlPaymentType.SelectedValue = null;
                        }

                    }
                    else
                    {

                        item.Enabled = true;
                    }


                }
            }

            
        }


        private void EnableAllPaymentTypes(bool enable)
        {
            foreach (var item in ddlPaymentType.Items)
            {
                item.Enabled = enable;
            }

           
        }



        private bool ResetAllFares = false;

        private void ddlCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
              
                ResetAllFares = false;
                if (numCompanyFares != null && IsDisplayingRecord==false)
                    numCompanyFares.Value = 0.00m;

                if (chkIsCompanyRates.Checked)
                {
                    int? companyId = ddlCompany.SelectedValue.ToIntorNull();


                    if (companyId == null)
                    {
                        HideOrderNoPanel();
                        ClearDepartment();

                        txtPaymentReference.Location = new Point(txtPaymentReference.Location.X, 78);
                        txtPaymentReference.Size = new Size(txtPaymentReference.Size.Width, 61);
                        lblPaymentRef.Location = new Point(lblPaymentRef.Location.X, 59);
                        ShowSecondaryPaymentType(false);



                    //    txtCompanyCreditCardNo.Text = string.Empty;
                        ResetFareReductionValues();

                        UpdateAutoCalculateFares();

                        ClearOrderNoDataSource();
                        ddlCompany.Tag = false;
                     //   EnableRoomCharges(false);
                        EnableAllPaymentTypes(true);
                    }
                    else
                    {
                        ShowSecondaryPaymentType(true);

                        Gen_Company obj = General.GetObject<Gen_Company>(c => c.Id == companyId);
                        if (obj != null)
                        {

                            if (ddlSubCompany != null && obj.SubCompanyId!=null)
                                ddlSubCompany.SelectedValue = obj.SubCompanyId;

                            if (IsDisplayingRecord==false && AppVars.objPolicyConfiguration.PickCompanyAddressOnBooking.ToBool())
                            {

                                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.ADDRESS;
                                txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);

                                string address = obj.Address.ToStr().ToUpper().Trim();

                                if (address.Contains(","))
                                {
                                    address = address.Replace(",", "").Trim().Replace("  ", " ").Trim();

                                }

                                txtFromAddress.Text = address;
                                txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                            }


                     //       txtCompanyCreditCardNo.Text = obj.CreditCardDetails.ToStr().Trim();

                            if (obj.HasEscort.ToBool())
                            {
                                InitializeEscort();

                                lblEscort.Visible = true;
                                ddlEscort.Visible = true;


                                lblEscortPrice.Visible = true;
                                numEscortPrice.Visible = true;

                                FillEscortsCombo();

                                if (obj.IsAgent.ToBool())
                                {

                                    txtPaymentReference.Visible = false;
                                    lblPaymentRef.Visible = false;
                                }
                                else
                                {
                                    ShowPaymentReference();

                                    // txtPaymentReference.Visible = true;
                                    // lblPaymentRef.Visible = true;
                                }





                                if (ddlDepartment != null)
                                {
                                    lblDepartment.Visible = false;
                                    ddlDepartment.Visible = false;

                                }

                                txtPaymentReference.Location = new Point(txtPaymentReference.Location.X, 100);
                                txtPaymentReference.Size = new Size(txtPaymentReference.Size.Width, 40);
                                lblPaymentRef.Location = new Point(lblPaymentRef.Location.X, 85);



                            }
                            else
                            {

                                if (lblEscort != null)
                                {

                                    lblEscort.Visible = false;
                                    ddlEscort.Visible = false;


                                    lblEscortPrice.Visible = false;
                                    numEscortPrice.Visible = false;
                                }

                                InitializeDepartmentCombo();

                                FillDepartmentsCombo(obj.Id);

                                if (ddlDepartment != null)
                                {
                                    lblDepartment.Visible = true;
                                    ddlDepartment.Visible = true;

                                }


                                if (obj.IsAgent.ToBool())
                                {

                                    txtPaymentReference.Visible = false;
                                    lblPaymentRef.Visible = false; 
                                }
                                else
                                {

                                    ShowPaymentReference();
                                    //  txtPaymentReference.Visible = true;
                                    //  lblPaymentRef.Visible = true;
                                }

                               

                                txtPaymentReference.Location = new Point(txtPaymentReference.Location.X, 78);
                                txtPaymentReference.Size = new Size(txtPaymentReference.Size.Width, 61);
                                lblPaymentRef.Location = new Point(lblPaymentRef.Location.X, 59);

                            }
                            // FillCostCentersCombo(obj.Id);

                            companyPricePercentage = obj.CompanyPricePercent.ToInt();
                            drvFareReductionType = obj.DriverFareReductionType.ToStr().Trim().ToLower();
                            drvFareReductionValue = obj.DriverFareReductionValue.ToDecimal();

                            bool orderNo = obj.HasOrderNo.ToBool();
                            bool pupilNo = obj.HasPupilNo.ToBool();
                            bool HasSingleOrderNo = obj.HasSingleOrderNo.ToBool();


                            if (orderNo || pupilNo)
                            {



                                InitializeOrderNoPanel();
                                InitializePupilNo();

                                if (orderNo == false || HasSingleOrderNo)
                                {
                                    lblOrderNo.Visible = false;
                                    txtOrderNo.Visible = false;

                                }


                                ClearOrderNoDataSource();

                                if (orderNo == true)
                                {
                                    lblOrderNo.Visible = true;
                                    txtOrderNo.Visible = true;

                                    txtOrderNo.AutoCompleteCustomSource.AddRange(obj.Gen_Company_OrderNumbers.Select(c => c.OrderNo).ToArray<string>());
                                    txtOrderNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                                    txtOrderNo.AutoCompleteSource = AutoCompleteSource.CustomSource;


                                }


                                if (pupilNo == false)
                                {
                                    lblPupilNo.Visible = false;
                                    txtPupilNo.Visible = false;

                                }

                                pnlOrderNo.Visible = true;

                                //  txtSpecialRequirements.Location = new Point(111, 488);
                                // txtSpecialRequirements.Size = new Size(240, 80);

                            }
                            else
                            {
                                HideOrderNoPanel();

                            }

                            if (obj.HasComcabCharges.ToBool())
                            {
                                HideOrderNoPanel();
                                //   ShowHideCostCenter();
                                ShowComcabCharges(true);
                            }
                            else
                            {
                                ShowComcabCharges(false);

                            }

                            SetCashAccount(obj.AccountTypeId.ToInt());

                            InitializeAccPassowrdPanel();
                            pnlAccpassword.Visible = obj.PasswordEnable.ToBool();



                            string[] bookedByDataSource = null;

                            if (obj.HasBookedBy.ToBool())
                            {
                                bookedByDataSource = obj.Gen_Company_BookedBies.Select(c => c.BookedBy).ToArray<string>();

                            }

                            

                            InitializeAccountBookedBy(obj.HasBookedBy.ToBool(),bookedByDataSource);


                            if (obj.IsAgent.ToBool())
                            {
                                InitializeAgentPanel();

                                ddlAgentCommissionType.SelectedIndex = 1;                         
                              
                            }


                            ShowAgentDetails(obj.IsAgent.ToBool());

                           // EnableRoomCharges(obj.HasRoomCharge.ToBool());

                            SetPaymentTypeByAccount(obj.Gen_Company_PaymentTypes.ToList());



                            if (obj.DisableCompanyFaresForController.ToBool() && AppVars.LoginObj.LgroupId == 2)
                            {

                                if (numCompanyFares != null)
                                {
                                    lblCompanyPrice.Visible = false;
                                    numCompanyFares.Visible = false;
                                }

                                if (numReturnCompanyFares != null)
                                {
                                    lblReturnCompanyPrice.Visible = false;
                                    numReturnCompanyFares.Visible = false;
                                }
                            }
                            else
                            {
                                if (numCompanyFares != null)
                                {
                                    lblCompanyPrice.Visible = true;
                                    numCompanyFares.Visible = true;
                                }

                                if (numReturnCompanyFares != null)
                                {
                                    lblReturnCompanyPrice.Visible = true;
                                    numReturnCompanyFares.Visible = true;
                                }


                            }

                            if (obj.ShowExtraCharges.ToBool())
                            {
                               
                                numWaitingChrgs.Enabled = false;
                                numParkingChrgs.Enabled = false;
                                numExtraChrgs.Enabled = false;

                                foreach (var item in obj.Gen_Company_ExtraCharges)
                                {
                                    if (item.Charges.ToInt() == 2)
                                    {
                                        numWaitingChrgs.Enabled = true;

                                    }
                                    else if (item.Charges.ToInt() == 3)
                                    {
                                        numParkingChrgs.Enabled = true;

                                    }
                                    else if (item.Charges.ToInt() == 4)
                                    {
                                        numExtraChrgs.Enabled = true;

                                    }                                    
                                }
                            }




                            if (AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
                            {

                                if (obj.ResetAllFares.ToBool())
                                {
                                    ResetAllFares = true;
                                    numFareRate.Value = 0.00m;

                                    if (numReturnFare != null)
                                        numReturnFare.Value = 0.00m;

                                    numCustomerFares.Value = 0.00m;
                                    if (numReturnCustFare != null)
                                    {
                                        numReturnCustFare.Value = 0.00m;
                                    }

                                    if (numCompanyFares != null)
                                    {
                                        numCompanyFares.Value = 0.00m;

                                        if (numReturnCompanyFares != null)
                                        {

                                            numReturnCompanyFares.Value = 0.00m;
                                        }
                                        ddlCompany.Tag = true;
                                    }


                                }
                                else
                                {
                                    ddlCompany.Tag = false;
                                    UpdateAutoCalculateFares();

                                }
                            }



                            if (IsDisplayingRecord == false)
                            {

                                if (obj.CompanyInformation.ToStr().Trim().Length > 0)
                                {

                                    frmInformation frmInfo = new frmInformation(obj.Id, obj.CompanyName, obj.CompanyInformation.ToStr().Trim());
                                    frmInfo.StartPosition = FormStartPosition.CenterScreen;
                                    frmInfo.ShowDialog();

                                    frmInfo.Dispose();
                                }

                                if (AppVars.objPolicyConfiguration.PickCompanyAddressOnBooking.ToBool() && obj.Gen_Company_Addresses.Count > 0)
                                {

                                    txtFromAddress.ListBoxElement.Items.Clear();
                                    txtFromAddress.ListBoxElement.Items.AddRange(obj.Gen_Company_Addresses.Select(args => args.Address).ToArray<string>());
                                    txtFromAddress.ShowListBox();
                                    txtFromAddress.BringToFront();
                                }
                            }
                        }
                    }
                }
                else
                {
                   
                    HideOrderNoPanel();
                    ClearDepartment();
                    //   ClearCostCenter();
                    ShowComcabCharges(false);

                    ShowSecondaryPaymentType(false);


                    ResetFareReductionValues();

                    ddlCompany.Tag = false;
                   // EnableRoomCharges(false);

                    EnableAllPaymentTypes(true);

                    UpdateAutoCalculateFares();

                }
            }
            catch (Exception ex)
            {


            }

        }

        private void ResetFareReductionValues()
        {

            companyPricePercentage = 0;
            drvFareReductionType = "amount";
            drvFareReductionValue = 0.00m;
        }



        UI.MyDropDownList ddlEscort = null;
        private void InitializeEscort()
        {
            if (lblEscort != null)
                return;

            this.lblEscort = new Telerik.WinControls.UI.RadLabel();
            this.ddlEscort = new UI.MyDropDownList();

            this.lblEscortPrice = new Telerik.WinControls.UI.RadLabel();
            this.numEscortPrice = new Telerik.WinControls.UI.RadSpinEditor();

            ((System.ComponentModel.ISupportInitialize)(this.lblEscort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlEscort)).BeginInit();


            this.pnlMain.Controls.Add(this.lblEscort);
            this.pnlMain.Controls.Add(this.ddlEscort);

            this.radPanel1.Controls.Add(this.numEscortPrice);
            this.radPanel1.Controls.Add(this.lblEscortPrice);

            ((System.ComponentModel.ISupportInitialize)(this.lblEscortPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEscortPrice)).BeginInit();


            // 
            // lblEscort
            // 
            this.lblEscort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEscort.Location = new System.Drawing.Point(17, 356);
            this.lblEscort.Name = "lblEscort";
            this.lblEscort.Size = new System.Drawing.Size(49, 22);
            this.lblEscort.TabIndex = 271;
            this.lblEscort.Text = "Escort";
            // 
            // ddlEscort
            // 
            this.ddlEscort.Caption = null;
            this.ddlEscort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlEscort.Location = new System.Drawing.Point(111, 353);
            this.ddlEscort.Name = "ddlDepartment";
            this.ddlEscort.Property = null;
            this.ddlEscort.ShowDownArrow = true;
            this.ddlEscort.Size = new System.Drawing.Size(208, 26);
            this.ddlEscort.TabIndex = 241;
            this.ddlEscort.KeyDown += new KeyEventHandler(ddlEscort_KeyDown);




            // 
            // lblEscortPrice
            // 
            this.lblEscortPrice.AutoSize = false;
            this.lblEscortPrice.BackColor = System.Drawing.Color.Orange;
            this.lblEscortPrice.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.lblEscortPrice.ForeColor = System.Drawing.Color.Black;
            this.lblEscortPrice.Location = new System.Drawing.Point(923, 57);
            this.lblEscortPrice.Name = "lblEscortPrice";
            // 
            // 
            // 
            this.lblEscortPrice.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblEscortPrice.Size = new System.Drawing.Size(289, 30);
            this.lblEscortPrice.TabIndex = 245;
            this.lblEscortPrice.Text = "     Escort Price  £";
            this.lblEscortPrice.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numEscortPrice
            // 
            this.numEscortPrice.DecimalPlaces = 2;
            this.numEscortPrice.EnableKeyMap = true;
            this.numEscortPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numEscortPrice.ForeColor = System.Drawing.Color.Red;
            this.numEscortPrice.InterceptArrowKeys = false;
            this.numEscortPrice.Location = new System.Drawing.Point(1078, 60);
            this.numEscortPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numEscortPrice.Name = "numEscortPrice";
            // 
            // 
            // 
            this.numEscortPrice.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numEscortPrice.RootElement.ForeColor = System.Drawing.Color.Red;
            this.numEscortPrice.ShowBorder = true;
            this.numEscortPrice.ShowUpDownButtons = false;
            this.numEscortPrice.Size = new System.Drawing.Size(59, 24);
            this.numEscortPrice.TabIndex = 244;
            this.numEscortPrice.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numEscortPrice.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numEscortPrice.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numEscortPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numEscortPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));




            ((System.ComponentModel.ISupportInitialize)(this.lblEscort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlEscort)).EndInit();


            ((System.ComponentModel.ISupportInitialize)(this.lblEscortPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEscortPrice)).EndInit();
        }


        private void InitializeDepartmentCombo()
        {
            if (lblDepartment != null)
                return;

            this.lblDepartment = new Telerik.WinControls.UI.RadLabel();
            this.ddlDepartment = new UI.MyDropDownList();

            ((System.ComponentModel.ISupportInitialize)(this.lblDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDepartment)).BeginInit();


            this.pnlMain.Controls.Add(this.lblDepartment);
            this.pnlMain.Controls.Add(this.ddlDepartment);



            // 
            // lblDepartment
            // 
            this.lblDepartment.BackColor = System.Drawing.Color.Transparent;
            this.lblDepartment.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.ForeColor = System.Drawing.Color.Black;
            this.lblDepartment.Location = new System.Drawing.Point(5, 354);
            this.lblDepartment.Name = "lblDepartment";
            // 
            // 
            // 
            this.lblDepartment.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblDepartment.Size = new System.Drawing.Size(88, 22);
            this.lblDepartment.TabIndex = 250;
            this.lblDepartment.Text = "Department";
            // 
            // ddlDepartment
            // 
            this.ddlDepartment.Caption = null;
            this.ddlDepartment.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDepartment.Location = new System.Drawing.Point(111, 353);
            this.ddlDepartment.Name = "ddlDepartment";
            this.ddlDepartment.Property = null;
            this.ddlDepartment.ShowDownArrow = true;
            this.ddlDepartment.Size = new System.Drawing.Size(208, 26);
            this.ddlDepartment.TabIndex = 241;
            this.ddlDepartment.KeyDown += new KeyEventHandler(ddlDepartment_KeyDown);

            ((System.ComponentModel.ISupportInitialize)(this.lblDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDepartment)).EndInit();
        }

        private void InitializeAccountBookedBy(bool hasBookedBy,string[] bookedByDataSource)
        {
            try
            {
                if (hasBookedBy)
                {

                    if (lblAccountBookedBy != null)
                    {
                        lblAccountBookedBy.Visible = true;
                        txtAccountBookedBy.Visible = true;


                        if (pnlAccpassword != null)
                        {

                            this.lblAccountBookedBy.Location = new System.Drawing.Point(6, 465);
                            this.txtAccountBookedBy.Location = new System.Drawing.Point(112, 464);


                        }
                        else
                        {
                            this.lblAccountBookedBy.Location = new System.Drawing.Point(6, 385);
                            this.txtAccountBookedBy.Location = new System.Drawing.Point(112, 384);
                        }

                        // txtSpecialRequirements.Location = new Point(111, 458);
                        //  txtSpecialRequirements.Location = new Point(111, 488);
                        //  txtSpecialRequirements.Size = new Size(240, 50);
                    }
                    else
                    {

                        if (this.lblAccountBookedBy == null)
                        {

                            this.lblAccountBookedBy = new Telerik.WinControls.UI.RadLabel();
                            this.txtAccountBookedBy = new Telerik.WinControls.UI.RadTextBox();


                            ((System.ComponentModel.ISupportInitialize)(this.lblAccountBookedBy)).BeginInit();
                            ((System.ComponentModel.ISupportInitialize)(this.txtAccountBookedBy)).BeginInit();

                            // 
                            // lblAccountBookedBy
                            // 
                            this.lblAccountBookedBy.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            this.lblAccountBookedBy.Name = "lblAccountBookedBy";
                            this.lblAccountBookedBy.Size = new System.Drawing.Size(79, 22);
                            this.lblAccountBookedBy.TabIndex = 267;
                            this.lblAccountBookedBy.Text = "Booked By";
                            // 
                            // txtAccountBookedBy
                            // 
                            this.txtAccountBookedBy.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                            this.txtAccountBookedBy.MaxLength = 30;
                            this.txtAccountBookedBy.Name = "txtAccountBookedBy";
                            this.txtAccountBookedBy.Size = new System.Drawing.Size(204, 24);
                            this.txtAccountBookedBy.TabIndex = 266;
                            this.txtAccountBookedBy.TabStop = false;

                            ((System.ComponentModel.ISupportInitialize)(this.lblAccountBookedBy)).EndInit();
                            ((System.ComponentModel.ISupportInitialize)(this.txtAccountBookedBy)).EndInit();


                            this.pnlMain.Controls.Add(this.lblAccountBookedBy);
                            this.pnlMain.Controls.Add(this.txtAccountBookedBy);
                        }




                        if (pnlAccpassword != null)
                        {

                            this.lblAccountBookedBy.Location = new System.Drawing.Point(6, 465);
                            this.txtAccountBookedBy.Location = new System.Drawing.Point(112, 464);


                        }
                        else
                        {
                            this.lblAccountBookedBy.Location = new System.Drawing.Point(6, 385);
                            this.txtAccountBookedBy.Location = new System.Drawing.Point(112, 384);
                        }




                        // txtSpecialRequirements.Location = new Point(111, 458);
                        //txtSpecialRequirements.Location = new Point(111, 488);
                        //  txtSpecialRequirements.Size = new Size(240, 50);
                    }


                    if (bookedByDataSource != null)
                    {

                        txtAccountBookedBy.AutoCompleteCustomSource.AddRange(bookedByDataSource);
                        txtAccountBookedBy.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtAccountBookedBy.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                    else
                    {
                        ClearBookedByDataSource();

                    }

                }
                else if (hasBookedBy == false && lblAccountBookedBy != null)
                {



                    lblAccountBookedBy.Visible = false;
                    txtAccountBookedBy.Visible = false;

                    txtAccountBookedBy.Text = string.Empty;

                    ClearBookedByDataSource();

                    //txtSpecialRequirements.Location = new Point(111, 458);
                    //  txtSpecialRequirements.Size = new Size(240, 90);


                }
            }
            catch (Exception ex)
            {


            }

        }



        private void ShowAgentDetails(bool IsAgent)
        {
            if (chkTakenByAgent != null)
            {

                if (IsAgent==true && ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT)
                    IsAgent = false;

                chkTakenByAgent.Visible = IsAgent;
                radLabel32.Visible = IsAgent;
                radLabel34.Visible = IsAgent;
                numAgentCommissionPercent.Visible = IsAgent;
                numAgentCommission.Visible = IsAgent;
                ddlAgentCommissionType.Visible = IsAgent;


                if (IsAgent == false && ddlCompany.SelectedValue != null)
                {
                    numAgentCommission.Value = 0.00m;
                }
            }
        }

        private void ShowComcabCharges(bool show)
        {
            if (pnlComcab == null)
                InitializeComCabCharges();

            pnlComcab.Visible = show;
        }

        private void InitializeReturnPanel()
        {

            if (lblReturnDriver != null)
                return;



            this.lblReturnDriver = new Telerik.WinControls.UI.RadLabel();
            this.ddlReturnDriver = new UI.MyDropDownList();
            this.lblReturnPickupDate = new Telerik.WinControls.UI.RadLabel();
            this.dtpReturnPickupDate = new UI.MyDatePicker();
            this.dtpReturnPickupDate.KeyDown += new KeyEventHandler(dtpReturnPickupDate_KeyDown);
            this.dtpReturnPickupDate.Leave += new EventHandler(dtpReturnPickupDate_Leave);
            this.dtpReturnPickupTime = new UI.MyDatePicker();
            this.lblReturnPickupTime = new Telerik.WinControls.UI.RadLabel();

            this.lblRetFares = new Telerik.WinControls.UI.RadLabel();
            this.numReturnFare = new Telerik.WinControls.UI.RadSpinEditor();

            this.lblReturnVehicle = new Telerik.WinControls.UI.RadLabel();
            this.ddlReturnVehicleType = new UI.MyDropDownList();

            this.lblReturnFromAirport = new Telerik.WinControls.UI.RadLabel();
            this.ddlReturnFromAirport = new RadComboBox();

            ((System.ComponentModel.ISupportInitialize)(this.lblReturnDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnPickupDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReturnPickupDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReturnPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRetFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnVehicleType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnFromAirport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnFromAirport)).BeginInit();





            this.pnlMain.Controls.Add(this.lblReturnDriver);
            this.pnlMain.Controls.Add(this.ddlReturnDriver);
            this.pnlMain.Controls.Add(this.lblReturnPickupDate);
            this.pnlMain.Controls.Add(this.dtpReturnPickupDate);
            this.pnlMain.Controls.Add(this.dtpReturnPickupTime);
            this.pnlMain.Controls.Add(this.lblReturnPickupTime);
            this.pnlMain.Controls.Add(this.lblReturnVehicle);
            this.pnlMain.Controls.Add(this.ddlReturnVehicleType);
            this.pnlMain.Controls.Add(this.lblReturnFromAirport);
            this.pnlMain.Controls.Add(this.ddlReturnFromAirport);


            this.radPanel1.Controls.Add(this.lblRetFares);
            this.radPanel1.Controls.Add(this.numReturnFare);



            // 
            // lblReturnDriver
            // 
            this.lblReturnDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnDriver.Location = new System.Drawing.Point(355, 424);
            this.lblReturnDriver.Name = "lblReturnDriver";
            this.lblReturnDriver.Size = new System.Drawing.Size(99, 22);
            this.lblReturnDriver.TabIndex = 168;
            this.lblReturnDriver.Text = "Return Driver";
            // 
            // ddlReturnDriver
            // 
            this.ddlReturnDriver.Caption = null;
            this.ddlReturnDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlReturnDriver.Location = new System.Drawing.Point(488, 422);
            this.ddlReturnDriver.Name = "ddlReturnDriver";
            this.ddlReturnDriver.Property = null;
            this.ddlReturnDriver.ShowDownArrow = true;
            this.ddlReturnDriver.Size = new System.Drawing.Size(240, 26);
            this.ddlReturnDriver.TabIndex = 23;
            // 
            // lblReturnPickupDate
            // 
            this.lblReturnPickupDate.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnPickupDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnPickupDate.Location = new System.Drawing.Point(355, 396);
            this.lblReturnPickupDate.Name = "lblReturnPickupDate";
            this.lblReturnPickupDate.Size = new System.Drawing.Size(121, 22);
            this.lblReturnPickupDate.TabIndex = 159;
            this.lblReturnPickupDate.Text = "Ret. Pickup Date";
            // 
            // dtpReturnPickupDate
            // 
            this.dtpReturnPickupDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpReturnPickupDate.CustomFormat = "dd/MM/yyyy";
            this.dtpReturnPickupDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnPickupDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpReturnPickupDate.Location = new System.Drawing.Point(488, 393);
            this.dtpReturnPickupDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpReturnPickupDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpReturnPickupDate.Name = "dtpReturnPickupDate";
            this.dtpReturnPickupDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpReturnPickupDate.Size = new System.Drawing.Size(102, 24);
            this.dtpReturnPickupDate.TabIndex = 21;
            this.dtpReturnPickupDate.TabStop = false;
            this.dtpReturnPickupDate.Text = "myDatePicker1";
            this.dtpReturnPickupDate.Value = null;
            // 
            // dtpReturnPickupTime
            // 
            this.dtpReturnPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpReturnPickupTime.CustomFormat = "HH:mm";
            this.dtpReturnPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpReturnPickupTime.Location = new System.Drawing.Point(643, 393);
            this.dtpReturnPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpReturnPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpReturnPickupTime.Name = "dtpReturnPickupTime";
            this.dtpReturnPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpReturnPickupTime.ShowUpDown = true;
            this.dtpReturnPickupTime.Size = new System.Drawing.Size(82, 24);
            this.dtpReturnPickupTime.TabIndex = 22;
            this.dtpReturnPickupTime.TabStop = false;
            this.dtpReturnPickupTime.Text = "myDatePicker2";

            this.dtpReturnPickupTime.Value = null;

            this.dtpReturnPickupTime.Leave += new EventHandler(dtpPickupTime_Leave);
            this.dtpReturnPickupTime.DateTimePickerElement.TextBoxElement.TextBoxItem.KeyPress += new KeyPressEventHandler(dtpPickupTime_KeyPress);

            // 
            // lblReturnPickupTime
            // 
            this.lblReturnPickupTime.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnPickupTime.Location = new System.Drawing.Point(596, 396);
            this.lblReturnPickupTime.Name = "lblReturnPickupTime";
            this.lblReturnPickupTime.Size = new System.Drawing.Size(41, 22);
            this.lblReturnPickupTime.TabIndex = 161;
            this.lblReturnPickupTime.Text = "Time";

            ((System.ComponentModel.ISupportInitialize)(this.lblReturnDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnPickupDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReturnPickupDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReturnPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnPickupTime)).EndInit();


            this.lblReturnSpecialReq = new Telerik.WinControls.UI.RadLabel();
            this.txtReturnSpecialReq = new Telerik.WinControls.UI.RadTextBox();


            ((System.ComponentModel.ISupportInitialize)(this.lblReturnSpecialReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnSpecialReq)).BeginInit();

            this.pnlMain.Controls.Add(this.lblReturnSpecialReq);
            this.pnlMain.Controls.Add(this.txtReturnSpecialReq);


            // 
            // lblReturnSpecialReq
            // 
            this.lblReturnSpecialReq.AutoSize = false;
            this.lblReturnSpecialReq.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnSpecialReq.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));



            this.lblReturnSpecialReq.Name = "lblReturnSpecialReq";
            this.lblReturnSpecialReq.Size = new System.Drawing.Size(116, 62);
            this.lblReturnSpecialReq.TabIndex = 267;
            this.lblReturnSpecialReq.Text = "Return Special Requirements";
            // 
            // txtReturnSpecialReq
            // 
            this.txtReturnSpecialReq.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnSpecialReq.MaxLength = 500;
            this.txtReturnSpecialReq.Multiline = true;
            this.txtReturnSpecialReq.Name = "txtReturnSpecialReq";
            // 
            // 
            // 
            this.txtReturnSpecialReq.RootElement.StretchVertically = true;
            this.txtReturnSpecialReq.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReturnSpecialReq.Size = new System.Drawing.Size(229, 45);
            this.txtReturnSpecialReq.TabIndex = 266;
            this.txtReturnSpecialReq.TabStop = false;



            if (AppVars.objPolicyConfiguration.AutoCloseDrvPopup.ToBool() == false)
            {
                this.lblReturnSpecialReq.Location = new System.Drawing.Point(736, 396);
                this.txtReturnSpecialReq.Location = new System.Drawing.Point(858, 406);

            }
            else
            {
                this.lblReturnSpecialReq.Location = new System.Drawing.Point(736, 426);
                this.txtReturnSpecialReq.Location = new System.Drawing.Point(858, 436);

            }

            // 
            // lblRetFares
            // 
            this.lblRetFares.BackColor = System.Drawing.Color.Orange;
            this.lblRetFares.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.lblRetFares.ForeColor = System.Drawing.Color.Black;
            this.lblRetFares.Location = new System.Drawing.Point(525, 92);
            this.lblRetFares.Name = "lblRetFares";
            // 
            // 
            // 
            this.lblRetFares.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblRetFares.Size = new System.Drawing.Size(111, 20);
            this.lblRetFares.TabIndex = 239;
            this.lblRetFares.Text = "Return Fares £";
            // 
            // numReturnFare
            // 
            this.numReturnFare.DecimalPlaces = 2;
            this.numReturnFare.Enabled = false;
            this.numReturnFare.EnableKeyMap = true;
            this.numReturnFare.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReturnFare.InterceptArrowKeys = false;
            this.numReturnFare.Location = new System.Drawing.Point(637, 89);
            this.numReturnFare.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numReturnFare.Name = "numReturnFare";
            // 
            // 
            // 
            this.numReturnFare.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numReturnFare.ShowBorder = true;
            this.numReturnFare.ShowUpDownButtons = false;
            this.numReturnFare.Size = new System.Drawing.Size(62, 24);

            this.numReturnFare.TabIndex = 238;
            this.numReturnFare.Validated += new EventHandler(numReturnFare_Validated);
            this.numReturnFare.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnFare.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numReturnFare.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnFare.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnFare.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            // 
            // lblReturnVehicle
            // 
            this.lblReturnVehicle.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnVehicle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblReturnVehicle.Location = new System.Drawing.Point(3, 241);
            this.lblReturnVehicle.Name = "lblReturnVehicle";
            this.lblReturnVehicle.Size = new System.Drawing.Size(108, 20);
            this.lblReturnVehicle.TabIndex = 274;
            this.lblReturnVehicle.Text = "Return Vehicle";
            this.lblReturnVehicle.Visible = false;
            // 
            // ddlReturnVehicleType
            // 
            this.ddlReturnVehicleType.Caption = null;
            this.ddlReturnVehicleType.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ddlReturnVehicleType.Location = new System.Drawing.Point(113, 240);
            this.ddlReturnVehicleType.Name = "ddlReturnVehicleType";
            this.ddlReturnVehicleType.Property = null;
            this.ddlReturnVehicleType.ShowDownArrow = true;
            this.ddlReturnVehicleType.Size = new System.Drawing.Size(124, 24);
            this.ddlReturnVehicleType.TabIndex = 273;
            this.ddlReturnVehicleType.Visible = false;



            // 
            // lblReturnFromAirport
            // 
            this.lblReturnFromAirport.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnFromAirport.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnFromAirport.Location = new System.Drawing.Point(363, 68);
            this.lblReturnFromAirport.Name = "lblReturnFromAirport";
            this.lblReturnFromAirport.Size = new System.Drawing.Size(98, 22);
            this.lblReturnFromAirport.TabIndex = 209;
            //   this.lblReturnFromAirport.Text = "Return From ";
            this.lblReturnFromAirport.Text = "";
            this.lblReturnFromAirport.Visible = false;
            // 
            // ddlReturnFromAirport
            // 
            this.ddlReturnFromAirport.BackColor = System.Drawing.Color.White;
            this.ddlReturnFromAirport.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //  this.ddlReturnFromAirport.Location = new System.Drawing.Point(496, 69);
            this.ddlReturnFromAirport.Location = ddlDropOffPlot.Location;
            this.ddlReturnFromAirport.Name = "ddlReturnFromAirport";
            //  this.ddlReturnFromAirport.NewValue = null;
            //   this.ddlReturnFromAirport.OldValue = null;
            // 
            // 
            // 
            this.ddlReturnFromAirport.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            //    this.ddlReturnFromAirport.ShowDropDownArrow = Telerik.WinControls.ElementVisibility.Visible;
            this.ddlReturnFromAirport.Size = new System.Drawing.Size(215, 23);
            this.ddlReturnFromAirport.TabIndex = 208;
            this.ddlReturnFromAirport.TabStop = false;
            this.ddlReturnFromAirport.Visible = false;


            ((System.ComponentModel.ISupportInitialize)(this.lblReturnSpecialReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnSpecialReq)).EndInit();

            ((System.ComponentModel.ISupportInitialize)(this.lblRetFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnFare)).EndInit();


            ((System.ComponentModel.ISupportInitialize)(this.lblReturnVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnVehicleType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnFromAirport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnFromAirport)).EndInit();




            this.lblRetFares.BringToFront();
            this.numReturnFare.BringToFront();
            if (objMaster.PrimaryKeyValue != null && objMaster.Current != null && objMaster.Current.ReturnDriverId != null)
            {
                ComboFunctions.FillDriverNoQueueCombo(ddlReturnDriver, objMaster.Current.ReturnDriverId
                            , objMaster.Current.Fleet_Driver1.DriverNo + " - " + objMaster.Current.Fleet_Driver1.DriverName);

            }
            else
            {


                ComboFunctions.FillDriverNoQueueCombo(ddlReturnDriver);
            }
        }

        void numReturnFare_Validated(object sender, EventArgs e)
        {

            if (numReturnFare.Value > 0 && numReturnCustFare != null)
                numReturnCustFare.Value = numReturnFare.Value;
        }

        void dtpReturnPickupDate_Leave(object sender, EventArgs e)
        {
            UpdateAutoCalculateFares();
        }

        void dtpReturnPickupDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnReturnPickupTime();
            }
        }



        private void InitializeAirportPanel()
        {
            if (pnlAirport != null) return;


            this.pnlAirport = new System.Windows.Forms.Panel();
            this.ddlAirPorts = new UI.MyDropDownList();
            this.btn_pickAirport = new Telerik.WinControls.UI.RadButton();
            this.label4 = new System.Windows.Forms.Label();
            this.ddlAirportType = new UI.MyDropDownList();

            this.pnlAirport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAirPorts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pickAirport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAirportType)).BeginInit();

            this.pnlMain.Controls.Add(this.pnlAirport);


            // 
            // pnlAirport
            // 
            this.pnlAirport.BackColor = System.Drawing.Color.Transparent;
            this.pnlAirport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAirport.Controls.Add(this.ddlAirPorts);
            this.pnlAirport.Controls.Add(this.btn_pickAirport);
            this.pnlAirport.Controls.Add(this.label4);
            this.pnlAirport.Controls.Add(this.ddlAirportType);
            this.pnlAirport.Location = new System.Drawing.Point(86, 36);
            this.pnlAirport.Name = "pnlAirport";
            this.pnlAirport.Size = new System.Drawing.Size(358, 133);
            this.pnlAirport.TabIndex = 202;
            this.pnlAirport.Visible = false;
            // 
            // ddlAirPorts
            // 
            this.ddlAirPorts.Caption = null;
            this.ddlAirPorts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlAirPorts.Location = new System.Drawing.Point(12, 60);
            this.ddlAirPorts.Name = "ddlAirPorts";
            this.ddlAirPorts.Property = null;
            this.ddlAirPorts.ShowDownArrow = true;
            this.ddlAirPorts.Size = new System.Drawing.Size(302, 22);
            this.ddlAirPorts.TabIndex = 2;
            this.ddlAirPorts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ddlAirPorts_KeyDown);
            // 
            // btn_pickAirport
            // 
            this.btn_pickAirport.Location = new System.Drawing.Point(229, 99);
            this.btn_pickAirport.Name = "btn_pickAirport";
            this.btn_pickAirport.Size = new System.Drawing.Size(122, 21);
            this.btn_pickAirport.TabIndex = 3;
            this.btn_pickAirport.Text = "Pick";
            this.btn_pickAirport.Click += new System.EventHandler(this.btnAirport_Pick_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.MintCream;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(2, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(353, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "Airports";
            // 
            // ddlAirportType
            // 
            this.ddlAirportType.Caption = null;
            this.ddlAirportType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem6 = new Telerik.WinControls.UI.RadListDataItem();
            radListDataItem5.Selected = true;
            radListDataItem5.Text = "From";
            radListDataItem5.TextWrap = true;
            radListDataItem6.Text = "To";
            radListDataItem6.TextWrap = true;
            this.ddlAirportType.Items.Add(radListDataItem5);
            this.ddlAirportType.Items.Add(radListDataItem6);
            this.ddlAirportType.Location = new System.Drawing.Point(12, 7);
            this.ddlAirportType.Name = "ddlAirportType";
            this.ddlAirportType.Property = null;
            this.ddlAirportType.ShowDownArrow = true;
            this.ddlAirportType.Size = new System.Drawing.Size(104, 22);
            this.ddlAirportType.TabIndex = 1;
            this.ddlAirportType.Text = "From";


            this.pnlAirport.ResumeLayout(false);
            this.pnlAirport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAirPorts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_pickAirport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAirportType)).EndInit();

            pnlAirport.BringToFront();
        }

        private void InitializeStationsPanel()
        {
            if (pnlStations != null) return;

            this.pnlStations = new System.Windows.Forms.Panel();
            this.ddlStations = new UI.MyDropDownList();
            this.btnPickStations = new Telerik.WinControls.UI.RadButton();
            this.label5 = new System.Windows.Forms.Label();
            this.ddlStationType = new UI.MyDropDownList();

            this.pnlStations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickStations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStationType)).BeginInit();

            this.pnlMain.Controls.Add(this.pnlStations);


            // 
            // pnlStations
            // 
            this.pnlStations.BackColor = System.Drawing.Color.Transparent;
            this.pnlStations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStations.Controls.Add(this.ddlStations);
            this.pnlStations.Controls.Add(this.btnPickStations);
            this.pnlStations.Controls.Add(this.label5);
            this.pnlStations.Controls.Add(this.ddlStationType);
            this.pnlStations.Location = new System.Drawing.Point(188, 37);
            this.pnlStations.Name = "pnlStations";
            this.pnlStations.Size = new System.Drawing.Size(358, 133);
            this.pnlStations.TabIndex = 203;
            this.pnlStations.Visible = false;
            // 
            // ddlStations
            // 
            this.ddlStations.Caption = null;
            this.ddlStations.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStations.Location = new System.Drawing.Point(12, 60);
            this.ddlStations.Name = "ddlStations";
            this.ddlStations.Property = null;
            this.ddlStations.ShowDownArrow = true;
            this.ddlStations.Size = new System.Drawing.Size(302, 22);
            this.ddlStations.TabIndex = 2;
            this.ddlStations.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ddlStations_KeyDown);
            // 
            // btnPickStations
            // 
            this.btnPickStations.Location = new System.Drawing.Point(229, 99);
            this.btnPickStations.Name = "btnPickStations";
            this.btnPickStations.Size = new System.Drawing.Size(122, 21);
            this.btnPickStations.TabIndex = 3;
            this.btnPickStations.Text = "Pick";
            this.btnPickStations.Click += new System.EventHandler(this.btn_PickStations_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.MintCream;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(2, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(353, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "Stations";
            // 
            // ddlStationType
            // 
            this.ddlStationType.Caption = null;
            this.ddlStationType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();

            radListDataItem3.Selected = true;
            radListDataItem3.Text = "From";
            radListDataItem3.TextWrap = true;
            radListDataItem4.Text = "To";
            radListDataItem4.TextWrap = true;
            this.ddlStationType.Items.Add(radListDataItem3);
            this.ddlStationType.Items.Add(radListDataItem4);
            this.ddlStationType.Location = new System.Drawing.Point(12, 7);
            this.ddlStationType.Name = "ddlStationType";
            this.ddlStationType.Property = null;
            this.ddlStationType.ShowDownArrow = true;
            this.ddlStationType.Size = new System.Drawing.Size(104, 22);
            this.ddlStationType.TabIndex = 1;
            this.ddlStationType.Text = "From";


            this.pnlStations.ResumeLayout(false);
            this.pnlStations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickStations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStationType)).EndInit();

            pnlStations.BringToFront();
        }


        private void InitializeHospitalPanel()
        {
            if (pnlHospital != null) return;

            this.pnlHospital = new System.Windows.Forms.Panel();
            this.ddlHospitals = new UI.MyDropDownList();
            this.btnPickHospital = new Telerik.WinControls.UI.RadButton();
            this.label6 = new System.Windows.Forms.Label();
            this.ddlHospitalType = new UI.MyDropDownList();

            this.pnlHospital.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlHospitals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickHospital)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlHospitalType)).BeginInit();




            // 
            // pnlHospital
            // 
            this.pnlHospital.BackColor = System.Drawing.Color.Transparent;
            this.pnlHospital.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHospital.Controls.Add(this.ddlHospitals);
            this.pnlHospital.Controls.Add(this.btnPickHospital);
            this.pnlHospital.Controls.Add(this.label6);
            this.pnlHospital.Controls.Add(this.ddlHospitalType);
            this.pnlHospital.Location = new System.Drawing.Point(321, 37);
            this.pnlHospital.Name = "pnlHospital";
            this.pnlHospital.Size = new System.Drawing.Size(358, 133);
            this.pnlHospital.TabIndex = 204;
            this.pnlHospital.Visible = false;
            // 
            // ddlHospitals
            // 
            this.ddlHospitals.Caption = null;
            this.ddlHospitals.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlHospitals.Location = new System.Drawing.Point(12, 60);
            this.ddlHospitals.Name = "ddlHospitals";
            this.ddlHospitals.Property = null;
            this.ddlHospitals.ShowDownArrow = true;
            this.ddlHospitals.Size = new System.Drawing.Size(302, 22);
            this.ddlHospitals.TabIndex = 2;
            this.ddlHospitals.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ddlHospitals_KeyDown);
            // 
            // btnPickHospital
            // 
            this.btnPickHospital.Location = new System.Drawing.Point(229, 99);
            this.btnPickHospital.Name = "btnPickHospital";
            this.btnPickHospital.Size = new System.Drawing.Size(122, 21);
            this.btnPickHospital.TabIndex = 3;
            this.btnPickHospital.Text = "Pick";
            this.btnPickHospital.Click += new System.EventHandler(this.btn_pickLocals_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.MintCream;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Navy;
            this.label6.Location = new System.Drawing.Point(2, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(353, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Hospitals";
            // 
            // ddlHospitalType
            // 
            this.ddlHospitalType.Caption = null;
            this.ddlHospitalType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();

            radListDataItem1.Selected = true;
            radListDataItem1.Text = "From";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "To";
            radListDataItem2.TextWrap = true;
            this.ddlHospitalType.Items.Add(radListDataItem1);
            this.ddlHospitalType.Items.Add(radListDataItem2);
            this.ddlHospitalType.Location = new System.Drawing.Point(12, 7);
            this.ddlHospitalType.Name = "ddlHospitalType";
            this.ddlHospitalType.Property = null;
            this.ddlHospitalType.ShowDownArrow = true;
            this.ddlHospitalType.Size = new System.Drawing.Size(104, 22);
            this.ddlHospitalType.TabIndex = 1;
            this.ddlHospitalType.Text = "From";


            this.pnlHospital.ResumeLayout(false);
            this.pnlHospital.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlHospitals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickHospital)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlHospitalType)).EndInit();


            this.pnlMain.Controls.Add(this.pnlHospital);

            pnlHospital.BringToFront();
        }





        private void InitializeAgentPanel()
        {
            if (chkTakenByAgent == null)
            {

                this.chkTakenByAgent = new Telerik.WinControls.UI.RadCheckBox();
                this.numAgentCommission = new Telerik.WinControls.UI.RadSpinEditor();
                this.radLabel32 = new Telerik.WinControls.UI.RadLabel();
                this.numAgentCommissionPercent = new Telerik.WinControls.UI.RadSpinEditor();
                this.radLabel34 = new Telerik.WinControls.UI.RadLabel();
                this.ddlAgentCommissionType = new UI.MyDropDownList();

                ((System.ComponentModel.ISupportInitialize)(this.chkTakenByAgent)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numAgentCommission)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel32)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numAgentCommissionPercent)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel34)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.ddlAgentCommissionType)).BeginInit();

                this.radPanel1.Controls.Add(this.chkTakenByAgent);
                this.radPanel1.Controls.Add(this.radLabel34);
                this.radPanel1.Controls.Add(this.numAgentCommissionPercent);
                this.radPanel1.Controls.Add(this.radLabel32);
                //  this.radPanel1.Controls.Add(this.radLabel30);
                this.radPanel1.Controls.Add(this.numAgentCommission);
                this.radPanel1.Controls.Add(this.ddlAgentCommissionType);


                // 
                // chkTakenByAgent
                // 
                this.chkTakenByAgent.BackColor = System.Drawing.Color.Transparent;
                this.chkTakenByAgent.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.chkTakenByAgent.ForeColor = System.Drawing.Color.Black;
                this.chkTakenByAgent.Location = new System.Drawing.Point(932, 64);
                this.chkTakenByAgent.Name = "chkTakenByAgent";
                // 
                // 
                // 
                this.chkTakenByAgent.RootElement.ForeColor = System.Drawing.Color.Black;
                this.chkTakenByAgent.Size = new System.Drawing.Size(157, 22);
                this.chkTakenByAgent.TabIndex = 266;
                this.chkTakenByAgent.Text = "Payment Taken By Agent";
                this.chkTakenByAgent.TextWrap = true;
                this.chkTakenByAgent.Visible = true;
                // 
                // numAgentCommission
                // 
                this.numAgentCommission.BackColor = System.Drawing.Color.Transparent;
                this.numAgentCommission.DecimalPlaces = 2;
                this.numAgentCommission.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numAgentCommission.Location = new System.Drawing.Point(1101, 91);
                this.numAgentCommission.Name = "numAgentCommission";
                this.numAgentCommission.Maximum = 1000;
                this.numAgentCommission.Validated += new EventHandler(numAgentCommission_Validated);
                // 
                // 
                // 
                this.numAgentCommission.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
                this.numAgentCommission.ShowBorder = true;
                this.numAgentCommission.ShowUpDownButtons = false;
                this.numAgentCommission.Size = new System.Drawing.Size(75, 24);
                this.numAgentCommission.TabIndex = 267;
                this.numAgentCommission.TabStop = false;
                ((Telerik.WinControls.UI.RadSpinElement)(this.numAgentCommission.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommission.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.Primitives.BorderPrimitive)(this.numAgentCommission.GetChildAt(0).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.Layouts.BoxLayout)(this.numAgentCommission.GetChildAt(0).GetChildAt(2).GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommission.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommission.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommission.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommission.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numAgentCommission.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numAgentCommission.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // 
                // radLabel32
                // 
                this.radLabel32.AutoSize = false;
                this.radLabel32.BackColor = System.Drawing.Color.Transparent;
                this.radLabel32.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
                this.radLabel32.ForeColor = System.Drawing.Color.Black;
                this.radLabel32.Location = new System.Drawing.Point(932, 93);
                this.radLabel32.Name = "radLabel32";
                // 
                // 
                // 
                this.radLabel32.RootElement.ForeColor = System.Drawing.Color.Black;
                this.radLabel32.Size = new System.Drawing.Size(99, 22);
                this.radLabel32.TabIndex = 268;
                this.radLabel32.Text = "Commission";
                // 
                // numAgentCommissionPercent
                // 
                this.numAgentCommissionPercent.BackColor = System.Drawing.Color.Transparent;
                this.numAgentCommissionPercent.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numAgentCommissionPercent.Location = new System.Drawing.Point(1026, 92);
                this.numAgentCommissionPercent.Name = "numAgentCommissionPercent";
                // 
                // 
                // 
                this.numAgentCommissionPercent.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
                this.numAgentCommissionPercent.ShowBorder = true;
                this.numAgentCommissionPercent.ShowUpDownButtons = false;
                this.numAgentCommissionPercent.Size = new System.Drawing.Size(36, 24);
                this.numAgentCommissionPercent.TabIndex = 269;
                this.numAgentCommissionPercent.TabStop = false;
                this.numAgentCommissionPercent.Validated += new EventHandler(numAgentCommissionPercent_Validated);

                ((Telerik.WinControls.UI.RadSpinElement)(this.numAgentCommissionPercent.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.Primitives.BorderPrimitive)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.Layouts.BoxLayout)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(2).GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(1).GetChildAt(0))).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numAgentCommissionPercent.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // 
                // radLabel34
                // 
                this.radLabel34.BackColor = System.Drawing.Color.Transparent;
                this.radLabel34.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radLabel34.Location = new System.Drawing.Point(1063, 93);
                this.radLabel34.Name = "radLabel34";
                this.radLabel34.Size = new System.Drawing.Size(26, 22);
                this.radLabel34.TabIndex = 270;
                this.radLabel34.Text = "%";



                // 
                // ddlAgentCommissionType
                // 
                this.ddlAgentCommissionType.Caption = null;
                this.ddlAgentCommissionType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
                this.ddlAgentCommissionType.Enabled = false;
                this.ddlAgentCommissionType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.ddlAgentCommissionType.ForeColor = System.Drawing.Color.Black;

                Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
                Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
                radListDataItem1.Text = "Percent";
                radListDataItem1.TextWrap = true;
                radListDataItem2.Text = "Amount";
                radListDataItem2.TextWrap = true;
                this.ddlAgentCommissionType.Items.Add(radListDataItem1);
                this.ddlAgentCommissionType.Items.Add(radListDataItem2);
                this.ddlAgentCommissionType.Location = new System.Drawing.Point(1000, 117);
                this.ddlAgentCommissionType.Name = "ddlAgentCommissionType";
                this.ddlAgentCommissionType.Property = null;
                // 
                // 
                // 
                this.ddlAgentCommissionType.RootElement.ForeColor = System.Drawing.Color.Black;
                this.ddlAgentCommissionType.ShowDownArrow = true;
                this.ddlAgentCommissionType.Size = new System.Drawing.Size(108, 26);
                this.ddlAgentCommissionType.TabIndex = 243;

                // Added on 15 sept 2014
                //  this.ddlAgentCommissionType.SelectedIndex = 1;
                //

                this.ddlAgentCommissionType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlAgentCommissionType_SelectedIndexChanged);
                this.ddlAgentCommissionType.Enabled = true;

                ((System.ComponentModel.ISupportInitialize)(this.chkTakenByAgent)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numAgentCommission)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel32)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numAgentCommissionPercent)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel34)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.ddlAgentCommissionType)).EndInit();
            }
        }

        void numAgentCommission_Validated(object sender, EventArgs e)
        {
            if (AppVars.objPolicyConfiguration.DisableDriverCommissionTick.ToBool() == true)
            {

                CalculateDriverFaresAndCompanyPrice();
            }
        }

        void numAgentCommissionPercent_Validated(object sender, EventArgs e)
        {
            CalculationAgentCommissionPercent();
        }


        private void CalculationAgentCommissionPercent()
        {
            try
            {
                if (numAgentCommission != null)
                {

                    numAgentCommission.Value = (numCompanyFares.Value * numAgentCommissionPercent.Value) / 100;

                    if (AppVars.objPolicyConfiguration.DisableDriverCommissionTick.ToBool() == true)
                    {
                        CalculateDriverFaresAndCompanyPrice();
                    }

                }
            }
            catch (Exception ex)
            {


            }
        }

      

        void ddlAgentCommissionType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {


            try
            {

                if (ddlAgentCommissionType.Text.Trim() == "Percent")
                {
                    numAgentCommissionPercent.Enabled = true;
                    numAgentCommission.Enabled = false;
                }
                else
                {
                    numAgentCommissionPercent.Enabled = false;
                    numAgentCommission.Enabled = true;

                }

            }
            catch (Exception ex)
            {


            }

        }

        private void InitializeAccPassowrdPanel()
        {
            if (pnlAccpassword != null)
                return;

            try
            {



                this.pnlAccpassword = new System.Windows.Forms.Panel();
                this.txtAccPassword = new Telerik.WinControls.UI.RadTextBox();
                this.radLabel33 = new Telerik.WinControls.UI.RadLabel();

                this.pnlAccpassword.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.txtAccPassword)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel33)).BeginInit();



                // 
                // pnlAccpassword
                // 
                this.pnlAccpassword.BackColor = System.Drawing.Color.Transparent;
                this.pnlAccpassword.Controls.Add(this.txtAccPassword);
                this.pnlAccpassword.Controls.Add(this.radLabel33);
                this.pnlAccpassword.Location = new System.Drawing.Point(7, 382);
                this.pnlAccpassword.Name = "pnlAccpassword";
                this.pnlAccpassword.Size = new System.Drawing.Size(322, 30);
                this.pnlAccpassword.TabIndex = 221;
                this.pnlAccpassword.Visible = false;
                // 
                // txtAccPassword
                // 
                this.txtAccPassword.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.txtAccPassword.Location = new System.Drawing.Point(104, 3);
                this.txtAccPassword.MaxLength = 50;
                this.txtAccPassword.Name = "txtAccPassword";
                this.txtAccPassword.PasswordChar = '*';
                this.txtAccPassword.Size = new System.Drawing.Size(208, 21);
                this.txtAccPassword.TabIndex = 215;
                this.txtAccPassword.TabStop = false;
                // 
                // radLabel33
                // 
                this.radLabel33.BackColor = System.Drawing.Color.Transparent;
                this.radLabel33.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.radLabel33.ForeColor = System.Drawing.Color.Black;
                this.radLabel33.Location = new System.Drawing.Point(1, 3);
                this.radLabel33.Name = "radLabel33";
                // 
                // 
                // 
                this.radLabel33.RootElement.ForeColor = System.Drawing.Color.Black;
                this.radLabel33.Size = new System.Drawing.Size(83, 22);
                this.radLabel33.TabIndex = 214;
                this.radLabel33.Text = "Password";


                this.pnlMain.Controls.Add(this.pnlAccpassword);

                this.pnlAccpassword.ResumeLayout(false);
                this.pnlAccpassword.PerformLayout();


                pnlAccpassword.BringToFront();

                ((System.ComponentModel.ISupportInitialize)(this.txtAccPassword)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel33)).EndInit();
            }
            catch (Exception ex)
            {

            }
        }





        private void InitializeOrderNoPanel()
        {

            if (this.pnlOrderNo != null)
            {
                return;
            }

            try
            {


                this.pnlOrderNo = new System.Windows.Forms.Panel();
                this.lblOrderNo = new Telerik.WinControls.UI.RadLabel();

                this.txtOrderNo = new Telerik.WinControls.UI.RadTextBox();



                this.pnlOrderNo.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.lblOrderNo)).BeginInit();

                ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo)).BeginInit();


                this.pnlMain.Controls.Add(this.pnlOrderNo);

                // 
                // pnlOrderNo
                // 
                this.pnlOrderNo.Controls.Add(this.lblOrderNo);

                this.pnlOrderNo.Controls.Add(this.txtOrderNo);

                this.pnlOrderNo.Location = new System.Drawing.Point(8, 410);
                this.pnlOrderNo.Name = "pnlOrderNo";
                this.pnlOrderNo.Size = new System.Drawing.Size(345, 54);
                this.pnlOrderNo.TabIndex = 205;

                // 
                // lblOrderNo
                // 
                this.lblOrderNo.BackColor = System.Drawing.Color.Transparent;
                this.lblOrderNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblOrderNo.Location = new System.Drawing.Point(16, 3);
                this.lblOrderNo.Name = "lblOrderNo";
                this.lblOrderNo.Size = new System.Drawing.Size(70, 22);
                this.lblOrderNo.TabIndex = 202;
                this.lblOrderNo.Text = "Order No";


                // 
                // txtOrderNo
                // 
                this.txtOrderNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                this.txtOrderNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                this.txtOrderNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.txtOrderNo.Location = new System.Drawing.Point(105, 2);
                this.txtOrderNo.MaxLength = 50;
                this.txtOrderNo.Name = "txtOrderNo";
                this.txtOrderNo.Size = new System.Drawing.Size(208, 24);
                this.txtOrderNo.TabIndex = 201;
                this.txtOrderNo.TabStop = false;



                this.pnlOrderNo.ResumeLayout(false);
                this.pnlOrderNo.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.lblOrderNo)).EndInit();

                ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo)).EndInit();


                pnlOrderNo.BringToFront();

            }
            catch (Exception ex)
            {


            }

        }



        private void InitializePupilNo()
        {
            try
            {

                if (pnlOrderNo == null)
                {

                    this.pnlOrderNo = new System.Windows.Forms.Panel();
                    this.pnlOrderNo.SuspendLayout();

                    this.pnlOrderNo.Location = new System.Drawing.Point(8, 410);
                    this.pnlOrderNo.Name = "pnlOrderNo";
                    this.pnlOrderNo.Size = new System.Drawing.Size(345, 54);
                    this.pnlOrderNo.TabIndex = 205;


                    this.pnlMain.Controls.Add(this.pnlOrderNo);


                    this.pnlOrderNo.ResumeLayout(false);
                    this.pnlOrderNo.PerformLayout();
                }




                if (this.txtPupilNo == null)
                {

                    this.txtPupilNo = new Telerik.WinControls.UI.RadTextBox();

                    this.lblPupilNo = new Telerik.WinControls.UI.RadLabel();


                    ((System.ComponentModel.ISupportInitialize)(this.txtPupilNo)).BeginInit();
                    ((System.ComponentModel.ISupportInitialize)(this.lblPupilNo)).BeginInit();



                    // 
                    // pnlOrderNo
                    // 


                    this.pnlOrderNo.Controls.Add(this.txtPupilNo);

                    this.pnlOrderNo.Controls.Add(this.lblPupilNo);


                    // 
                    // lblOrderNo
                    // 


                    // 
                    // txtPupilNo
                    // 
                    this.txtPupilNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
                    this.txtPupilNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
                    this.txtPupilNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.txtPupilNo.Location = new System.Drawing.Point(105, 28);
                    this.txtPupilNo.MaxLength = 10;
                    this.txtPupilNo.Name = "txtPupilNo";
                    this.txtPupilNo.Size = new System.Drawing.Size(154, 24);
                    this.txtPupilNo.TabIndex = 203;
                    this.txtPupilNo.TabStop = false;
                    // 
                    // txtOrderNo
                    // 

                    // 
                    // lblPupilNo
                    // 
                    this.lblPupilNo.BackColor = System.Drawing.Color.Transparent;
                    this.lblPupilNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.lblPupilNo.Location = new System.Drawing.Point(24, 29);
                    this.lblPupilNo.Name = "lblPupilNo";
                    this.lblPupilNo.Size = new System.Drawing.Size(64, 22);
                    this.lblPupilNo.TabIndex = 204;
                    this.lblPupilNo.Text = "Pupil No";

                    ((System.ComponentModel.ISupportInitialize)(this.txtPupilNo)).EndInit();

                    ((System.ComponentModel.ISupportInitialize)(this.lblPupilNo)).EndInit();



                }


                pnlOrderNo.BringToFront();
            }
            catch (Exception ex)
            {


            }
        }


        private void InitializeComCabCharges()
        {


            if (pnlComcab != null)
                return;


            try
            {


                this.pnlComcab = new System.Windows.Forms.Panel();
                this.radLabel29 = new Telerik.WinControls.UI.RadLabel();
                this.numComcab_WaitingMin = new Telerik.WinControls.UI.RadSpinEditor();
                this.radLabel31 = new Telerik.WinControls.UI.RadLabel();
                this.numComcab_ExtraMile = new Telerik.WinControls.UI.RadSpinEditor();
                this.radLabel28 = new Telerik.WinControls.UI.RadLabel();
                this.numComcab_Account = new Telerik.WinControls.UI.RadSpinEditor();
                this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
                this.numComcab_Cash = new Telerik.WinControls.UI.RadSpinEditor();

                this.pnlComcab.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel29)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numComcab_WaitingMin)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel31)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numComcab_ExtraMile)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel28)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numComcab_Account)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).BeginInit();
                ((System.ComponentModel.ISupportInitialize)(this.numComcab_Cash)).BeginInit();
                this.pnlMain.Controls.Add(this.pnlComcab);

                this.pnlComcab.Controls.Add(this.radLabel29);
                this.pnlComcab.Controls.Add(this.numComcab_WaitingMin);
                this.pnlComcab.Controls.Add(this.radLabel31);
                this.pnlComcab.Controls.Add(this.numComcab_ExtraMile);
                this.pnlComcab.Controls.Add(this.radLabel28);
                this.pnlComcab.Controls.Add(this.numComcab_Account);
                this.pnlComcab.Controls.Add(this.radLabel8);
                this.pnlComcab.Controls.Add(this.numComcab_Cash);
                this.pnlComcab.Location = new System.Drawing.Point(3, 405);
                this.pnlComcab.Name = "pnlComcab";
                this.pnlComcab.Size = new System.Drawing.Size(349, 81);
                this.pnlComcab.TabIndex = 216;
                this.pnlComcab.Visible = false;
                // 
                // radLabel29
                // 
                this.radLabel29.BackColor = System.Drawing.Color.Transparent;
                this.radLabel29.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
                this.radLabel29.ForeColor = System.Drawing.Color.Black;
                this.radLabel29.Location = new System.Drawing.Point(1, 59);
                this.radLabel29.Name = "radLabel29";
                // 
                // 
                // 
                this.radLabel29.RootElement.ForeColor = System.Drawing.Color.Black;
                this.radLabel29.Size = new System.Drawing.Size(92, 20);
                this.radLabel29.TabIndex = 136;
                this.radLabel29.Text = "Waiting Min";
                // 
                // numComcab_WaitingMin
                // 
                this.numComcab_WaitingMin.EnableKeyMap = true;
                this.numComcab_WaitingMin.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numComcab_WaitingMin.ForeColor = System.Drawing.Color.Red;
                this.numComcab_WaitingMin.InterceptArrowKeys = false;
                this.numComcab_WaitingMin.Location = new System.Drawing.Point(107, 57);
                this.numComcab_WaitingMin.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
                this.numComcab_WaitingMin.Name = "numComcab_WaitingMin";
                // 
                // 
                // 
                this.numComcab_WaitingMin.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
                this.numComcab_WaitingMin.RootElement.ForeColor = System.Drawing.Color.Red;
                this.numComcab_WaitingMin.ShowBorder = true;
                this.numComcab_WaitingMin.ShowUpDownButtons = false;
                this.numComcab_WaitingMin.Size = new System.Drawing.Size(76, 24);
                this.numComcab_WaitingMin.TabIndex = 135;
                this.numComcab_WaitingMin.TabStop = false;
                ((Telerik.WinControls.UI.RadSpinElement)(this.numComcab_WaitingMin.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numComcab_WaitingMin.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numComcab_WaitingMin.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numComcab_WaitingMin.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // 
                // radLabel31
                // 
                this.radLabel31.BackColor = System.Drawing.Color.Transparent;
                this.radLabel31.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
                this.radLabel31.ForeColor = System.Drawing.Color.Black;
                this.radLabel31.Location = new System.Drawing.Point(13, 31);
                this.radLabel31.Name = "radLabel31";
                // 
                // 
                // 
                this.radLabel31.RootElement.ForeColor = System.Drawing.Color.Black;
                this.radLabel31.Size = new System.Drawing.Size(78, 20);
                this.radLabel31.TabIndex = 134;
                this.radLabel31.Text = "Extra Mile";
                // 
                // numComcab_ExtraMile
                // 
                this.numComcab_ExtraMile.DecimalPlaces = 2;
                this.numComcab_ExtraMile.EnableKeyMap = true;
                this.numComcab_ExtraMile.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numComcab_ExtraMile.ForeColor = System.Drawing.Color.Red;
                this.numComcab_ExtraMile.InterceptArrowKeys = false;
                this.numComcab_ExtraMile.Location = new System.Drawing.Point(106, 29);
                this.numComcab_ExtraMile.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
                this.numComcab_ExtraMile.Name = "numComcab_ExtraMile";
                // 
                // 
                // 
                this.numComcab_ExtraMile.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
                this.numComcab_ExtraMile.RootElement.ForeColor = System.Drawing.Color.Red;
                this.numComcab_ExtraMile.ShowBorder = true;
                this.numComcab_ExtraMile.ShowUpDownButtons = false;
                this.numComcab_ExtraMile.Size = new System.Drawing.Size(77, 24);
                this.numComcab_ExtraMile.TabIndex = 133;
                this.numComcab_ExtraMile.TabStop = false;
                ((Telerik.WinControls.UI.RadSpinElement)(this.numComcab_ExtraMile.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numComcab_ExtraMile.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numComcab_ExtraMile.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numComcab_ExtraMile.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // 
                // radLabel28
                // 
                this.radLabel28.BackColor = System.Drawing.Color.Transparent;
                this.radLabel28.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
                this.radLabel28.ForeColor = System.Drawing.Color.Black;
                this.radLabel28.Location = new System.Drawing.Point(197, 3);
                this.radLabel28.Name = "radLabel28";
                // 
                // 
                // 
                this.radLabel28.RootElement.ForeColor = System.Drawing.Color.Black;
                this.radLabel28.Size = new System.Drawing.Size(64, 20);
                this.radLabel28.TabIndex = 132;
                this.radLabel28.Text = "Account";
                // 
                // numComcab_Account
                // 
                this.numComcab_Account.DecimalPlaces = 2;
                this.numComcab_Account.EnableKeyMap = true;
                this.numComcab_Account.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numComcab_Account.ForeColor = System.Drawing.Color.Red;
                this.numComcab_Account.InterceptArrowKeys = false;
                this.numComcab_Account.Location = new System.Drawing.Point(272, 1);
                this.numComcab_Account.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
                this.numComcab_Account.Name = "numComcab_Account";
                // 
                // 
                // 
                this.numComcab_Account.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
                this.numComcab_Account.RootElement.ForeColor = System.Drawing.Color.Red;
                this.numComcab_Account.ShowBorder = true;
                this.numComcab_Account.ShowUpDownButtons = false;
                this.numComcab_Account.Size = new System.Drawing.Size(70, 24);
                this.numComcab_Account.TabIndex = 131;
                this.numComcab_Account.TabStop = false;
                ((Telerik.WinControls.UI.RadSpinElement)(this.numComcab_Account.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numComcab_Account.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numComcab_Account.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numComcab_Account.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // 
                // radLabel8
                // 
                this.radLabel8.BackColor = System.Drawing.Color.Transparent;
                this.radLabel8.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
                this.radLabel8.ForeColor = System.Drawing.Color.Black;
                this.radLabel8.Location = new System.Drawing.Point(46, 3);
                this.radLabel8.Name = "radLabel8";
                // 
                // 
                // 
                this.radLabel8.RootElement.ForeColor = System.Drawing.Color.Black;
                this.radLabel8.Size = new System.Drawing.Size(41, 20);
                this.radLabel8.TabIndex = 130;
                this.radLabel8.Text = "Cash";
                // 
                // numComcab_Cash
                // 
                this.numComcab_Cash.DecimalPlaces = 2;
                this.numComcab_Cash.EnableKeyMap = true;
                this.numComcab_Cash.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.numComcab_Cash.ForeColor = System.Drawing.Color.Red;
                this.numComcab_Cash.InterceptArrowKeys = false;
                this.numComcab_Cash.Location = new System.Drawing.Point(108, 1);
                this.numComcab_Cash.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
                this.numComcab_Cash.Name = "numComcab_Cash";
                // 
                // 
                // 
                this.numComcab_Cash.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
                this.numComcab_Cash.RootElement.ForeColor = System.Drawing.Color.Red;
                this.numComcab_Cash.ShowBorder = true;
                this.numComcab_Cash.ShowUpDownButtons = false;
                this.numComcab_Cash.Size = new System.Drawing.Size(75, 24);
                this.numComcab_Cash.TabIndex = 129;
                this.numComcab_Cash.TabStop = false;
                this.numComcab_Cash.Validated += new System.EventHandler(this.numComcab_Cash_Validated);
                ((Telerik.WinControls.UI.RadSpinElement)(this.numComcab_Cash.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
                ((Telerik.WinControls.Primitives.FillPrimitive)(this.numComcab_Cash.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numComcab_Cash.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
                ((Telerik.WinControls.UI.RadTextBoxItem)(this.numComcab_Cash.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                // 
                // opt_WaitandReturn
                // 

                this.pnlComcab.ResumeLayout(false);
                this.pnlComcab.PerformLayout();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel29)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numComcab_WaitingMin)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel31)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numComcab_ExtraMile)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel28)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numComcab_Account)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).EndInit();
                ((System.ComponentModel.ISupportInitialize)(this.numComcab_Cash)).EndInit();

                pnlComcab.BringToFront();

            }
            catch (Exception ex)
            {


            }
        }


        private void SetCashAccount(int accTypeId)
        {
            if (accTypeId == Enums.ACCOUNT_TYPE.CASH)
            {

                if (AppVars.objPolicyConfiguration.DisableDriverCommissionTick.ToBool() == false && chkIsCommissionWise.Visible)
                {
                    chkIsCommissionWise.Checked = true;
                    ddlCommissionType.SelectedValue = "Amount";
                    ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;
                }
              //  ddlPaymentType.SelectedValue = null;

                //var item = ddlPaymentType.Items.FirstOrDefault(c => c.Value.ToInt() == Enums.PAYMENT_TYPES.ROOM_CHARGE);
                //if (item != null)
                //{
                //    item.Enabled = true;
                //}


            }
            else if (accTypeId == 3)
            {
                SetCreditCardPaymentType();
            }

            else
            {
                chkIsCommissionWise.Checked = false;
                SetAccountPaymentType();

                //var item = ddlPaymentType.Items.FirstOrDefault(c => c.Value.ToInt() == Enums.PAYMENT_TYPES.ROOM_CHARGE);
                //if (item != null)
                //{
                //    item.Enabled = false;
                //}
            }

        }


        private void SetCreditCardPaymentType()
        {

            ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CREDIT_CARD;
        }

        private void ClearDepartment()
        {
            if (ddlDepartment != null)
                ddlDepartment.DataSource = null;
        }


        private void FillDepartmentsCombo(int companyId)
        {
            ComboFunctions.FillCompanyDepartmentCombo(ddlDepartment, c => c.CompanyId == companyId);
        }


        private void FillEscortsCombo()
        {
            if (ddlEscort.DataSource == null)
                ComboFunctions.FillEscortCombo(ddlEscort, null);
        }



        private void HideOrderNoPanel()
        {
            if (pnlOrderNo == null)
                return;

            pnlOrderNo.Visible = false;
            txtOrderNo.Text = string.Empty;
            txtPupilNo.Text = string.Empty;



            //if (txtSpecialRequirements.Location.Y != 458)
            //{
            //     txtSpecialRequirements.Location = new Point(111, 458);
            //     txtSpecialRequirements.Size = new Size(240,50);
            //}
        }




        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrintJob_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void Print()
        {

            if (objMaster.Current == null) return;


            long id = objMaster.Current.Id;





            var list = General.GetQueryable<Vu_BookingDetail>(c => c.Id == id || c.MasterJobId == id).ToList();



            UM_Form_Template objReport = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "rptfrmJobDetails" && c.IsDefault == true);
            rptfrmJobDetails frm = null;
            rptfrmJobDetails2 frm2 = null;
            rptfrmJobDetails3 frm3 = null;
            rptfrmJobDetails4 frm4 = null;
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


                    case "rptfrmJobDetails4":
                        frm4 = new rptfrmJobDetails4();
                        frm4.DataSource = list;
                        frm4.GenerateReport();

                        break;

                }
            }



            Telerik.WinControls.UI.Docking.DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName(objReport.TemplateValue + "1");

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

            else if (frm4 != null)
            {
                frm4.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm4.Size = new Size(850, 800);
                frm4.ControlBox = true;
                frm4.MaximizeBox = true;
                frm4.MinimizeBox = true;
                frm4.ShowDialog();

                frm4.Dispose();


            }


        }

        private void opt_JOneWay_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetJourneyWise(args.ToggleState);

            if (args.ToggleState == ToggleState.On && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
            {
                UpdateAutoCalculateFares();
            }
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


            ShowRouteSuggesstion();
        }


        private void ShowRouteSuggesstion()
        {
            try
            {

                string FromAddress = "";
                string ToAddress = "";
                string[] via = new string[0];

                if (pnlVia != null)
                {
                    via = grdVia.Rows.Select(c =>General.GetPostCodeMatch(c.Cells["VIALOCATIONVALUE"].Value.ToStr())).ToArray<string>();
                }


                int fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();

                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.BASE)
                    FromAddress = txtFromAddress.Text.ToStr().Trim();
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    FromAddress = txtFromPostCode.Text.ToStr().Trim();
                }
                else
                {

                    FromAddress = ddlFromLocation.Text.ToStr().Trim();
                }



                int toLocTypeId = ddlToLocType.SelectedValue.ToInt();

                if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
                    ToAddress = txtToAddress.Text.ToStr().Trim();
                else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    ToAddress = txtToPostCode.Text.ToStr().Trim();
                }
                else
                {

                    ToAddress = ddlToLocation.Text.ToStr().Trim();
                }






                if (string.IsNullOrEmpty(FromAddress) || string.IsNullOrEmpty(ToAddress))
                {

                    ENUtils.ShowMessage("Required FromAddress or ToAddress");
                    return;
                }



                int? VehicleId = ddlVehicleType.SelectedValue.ToInt();
                int? CompanyId = ddlCompany.SelectedValue.ToInt();







                string viaStr = "**";

                if (via != null && via.Count() > 0)
                {
                    viaStr = string.Join(">>>", via);

                    viaStr = viaStr.Replace(" ", "**").Trim();

                }


                string connString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionStringRouteMap"].ToStr();

                if (!string.IsNullOrEmpty(connString))
                {

                    connString = Application.StartupPath + "\\TreasureRouteMap.exe";
                    //connString = @"C:\Program Files(86)\Eurosoft Tech\Treasure RouteMap Setup\TreasureRouteMap.exe";

                    //if (File.Exists(connString) == false)
                    //{
                    //    connString = @"C:\Program Files\Eurosoft Tech\Treasure RouteMap Setup\TreasureRouteMap.exe";
                    //}

                }





                //string connString= System.Configuration.ConfigurationSettings.AppSettings["ConnectionStringRouteMap"].ToStr();

                //if (string.IsNullOrEmpty(connString))
                //{




                //    connString=@"C:\Program Files(86)\Eurosoft Tech\Treasure RouteMap Setup\TreasureRouteMap.exe";

                //    if(File.Exists(connString)==false)
                //    {
                //       connString=@"C:\Program Files\Eurosoft Tech\Treasure RouteMap Setup\TreasureRouteMap.exe";
                //    }              


                //}


                DateTime? pickupdatetime = dtpPickupTime.Value;

                if (pickupdatetime == null)
                    pickupdatetime = DateTime.Now;


                if (connString.ToStr().Length > 0 && File.Exists(connString) == true)
                {
                    Process proc = Process.GetProcesses().FirstOrDefault(c => c.ProcessName.Contains("TreasureRouteMap"));

                    if (proc != null)
                    {
                        proc.Kill();
                        proc.CloseMainWindow();
                        proc.Close();
                    }



                    string conn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");




                    FromAddress = General.GetPostCodeMatch(FromAddress)+", UK";
                    ToAddress = General.GetPostCodeMatch(ToAddress) + ", UK";



                    string arg = "frmroutsuggestions" + " " + conn + " " + FromAddress.Replace(" ", "**") + " " + ToAddress.Replace(" ", "**") + " " + viaStr + " " + VehicleId + " " + CompanyId + " " + fromLocTypeId.ToStr() + " " + string.Format("{0:dd/MM/yyyy HH:mm}", pickupdatetime).Replace(" ", "**");
                    Process.Start(connString, arg);
                }
                else
                {


                    frmRoutSuggestions frm = new frmRoutSuggestions(FromAddress, via, ToAddress, VehicleId, CompanyId, fromLocTypeId, pickupdatetime);
                    frm.ShowDialog();

                    if (frm.SelectedFares != 0)
                    {
                        numFareRate.Value = frm.SelectedFares;
                        lblMap.Text = "Distance : " + frm.SeletedMiles + ". Time :" + frm.SelectedTime + "";
                    }

                    frm.Dispose();
                    frm = null;
                    GC.Collect();
                }

            }
            catch (Exception ex)
            {


            }


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

                if (ddlReturnDriver != null)
                    ddlReturnDriver.Enabled = false;


                btnSaveNew.Text = "Save Quotation";

                if (objMaster.PrimaryKeyValue != null)
                {
                    btnCancelBooking.Visible = false;
                    btnConfirmBooking.Visible = true;
                    // btnCancelBooking.SendToBack();
                    btnConfirmBooking.BringToFront();
                }
            }
            else
            {
                btnSaveNew.Text = "Save Booking";
                ddlDriver.Enabled = true;
                if (ddlReturnDriver != null)
                    ddlReturnDriver.Enabled = true;

            }

        }

        private void btnConfirmBooking_Click(object sender, EventArgs e)
        {
            try
            {
                chkQuotation.Checked = false;
                if (CheckDefaultValidation())
                {
                    if (Save())
                    {



                        if (AppVars.objPolicyConfiguration.SendDirectBookingConfirmationEmail.ToBool())
                        {
                            SendBookingConfirmationEmail();
                        }

                        General.AddBookingLog(objMaster.Current.Id, "Quotation "+objMaster.Current.BookingNo+ " Confirmed by " + AppVars.LoginObj.UserName);

                        Close();
                    }
                }
            }
            catch
            {


            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {

            SendEmail(true);

        }


        private void SendEmail(bool saveBooking)
        {
            try
            {

                if(saveBooking)
                  Save();
                if (objMaster.PrimaryKeyValue != null)
                {


                    frmEmailBooking frm = new frmEmailBooking(objMaster.Current);
                    frm.IsOpenedFromBooking = true;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }



        private void ddlPaymentType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD)
                {
                    btnPayment.Enabled = true;

                    ShowAgentDetails(true);
                }
                else
                {
                    btnPayment.Enabled = false;


                    if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.CASH && chkIsCompanyRates.Checked)
                    {
                        if (ddlCompany.SelectedValue != null)
                        {
                            if (AppVars.objPolicyConfiguration.DisableDriverCommissionTick.ToBool())
                            {


                                if (objMaster.Current != null && objMaster.Current.CompanyId.ToInt() == ddlCompany.SelectedValue.ToInt() &&
                                    (objMaster.Current.Gen_Company.Gen_Company_PaymentTypes.Count(c =>
                                                    c.PaymentTypeId == ddlPaymentType.SelectedValue.ToInt()) == 0))
                                //objMaster.Current.Gen_Company.DefaultIfEmpty().AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.ACCOUNT)
                                {
                                    ddlCompany.SelectedValue = null;
                                    chkIsCompanyRates.Checked = false;

                                }
                                else
                                {

                                    if (General.GetQueryable<Gen_Company_PaymentType>(null).Count(c => c.CompanyId == ddlCompany.SelectedValue.ToInt() &&
                                                    c.PaymentTypeId == ddlPaymentType.SelectedValue.ToInt()) == 0)
                                    {
                                        ddlCompany.SelectedValue = null;
                                        chkIsCompanyRates.Checked = false;

                                    }

                                }
                            }
                        }
                        else
                        {
                            chkIsCompanyRates.Checked = false;
                        }


                        ShowAgentDetails(true);
                    }
                    else
                    {

                        if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.ROOM_CHARGE && chkIsCompanyRates.Checked)
                        {

                            ShowAgentDetails(true);

                            if (chkTakenByAgent != null && chkTakenByAgent.Visible)
                            {

                                chkTakenByAgent.Checked = true;
                            }

                        }
                        else if (ddlPaymentType.SelectedValue.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT)
                        {

                            ShowAgentDetails(false);

                            // General.GetObject<Gen_Company>(c => c.Id == ddlCompany.SelectedValue.ToInt());

                        }
                    }

                }


                UpdateAutoCalculateFares();

            }
            catch
            {


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


                this.SaveAndClose();


                if (objMaster.Current != null)
                {

                    string BookingNO = objMaster.Current.BookingNo.ToStr();
                    int? BookingId = objMaster.Current.Id.ToInt();

                    if (IsSave == true)
                    {
                        frmBookingPayment2 frm = new frmBookingPayment2(objBookingPayment, objMerchantInfo, fare, BookingNO, BookingId);
                        frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        frm.Dispose();


                        GC.Collect();
                    }
                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }
        }



        private void numFareRate_Validated(object sender, EventArgs e)
        {
            CalculateTotalCharges();
        }

        private void CalculateTotalCharges()
        {
            try
            {
                if (ddlCompany.SelectedValue != null && numCompanyFares != null)
                {
                    if (numCompanyFares.Value == 0)
                    {

                        decimal fare = numFareRate.Value.ToDecimal();

                        if (companyPricePercentage > 0)
                        {
                            fare += (fare * companyPricePercentage) / 100;
                        }

                        if (fare > 0)
                        {
                            numCompanyFares.Value = fare;
                        }
                    }

                    numTotalChrgs.Value = numCompanyFares.Value + numParkingChrgs.Value + numWaitingChrgs.Value;                 

                }
                else
                {


                    numTotalChrgs.Value = numFareRate.Value +  numExtraChrgs.Value + numCongChrgs.Value + numMeetCharges.Value;

                    if (numCustomerFares.Value == 0)
                    {
                        if (numFareRate.Value > 0)
                            numCustomerFares.Value = numFareRate.Value;
                    }

                }



           



            }
            catch (Exception ex)
            {


            }

        }

        private void opt_WaitandReturn_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetJourneyWise(args.ToggleState);



            if (args.ToggleState == ToggleState.On && AppVars.objPolicyConfiguration.AutoCalculateFares.ToBool())
            {
                UpdateAutoCalculateFares();
            }

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
            catch (Exception ex)
            {


            }

        }

        private void ddlFromLocation_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                SetPickupZone(ddlFromLocation.Text);

            }
            catch (Exception ex)
            {


            }


        }




        int i = 0;
        private void dtpPickupTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                i++;

                RadMaskedEditBoxItem editItem = (RadMaskedEditBoxItem)sender;

                if (i == 2)
                {
                    SendKeys.Send("{right}");
                    i = 0;
                }


                if (editItem.SelectionStart > 2)
                {
                    i = 0;
                }
                //  }
            }
            catch (Exception ex)
            {


            }
        }






        private void btnJobRoutePath_Click(object sender, EventArgs e)
        {
            try
            {
                if (objMaster.PrimaryKeyValue != null && objMaster.Current.Booking_RoutePaths.Count > 0)
                {
                    rptJobRouthPathGoogle rptRoute = new rptJobRouthPathGoogle(objMaster.Current, false);
                    rptRoute.StartPosition = FormStartPosition.CenterScreen;
                    rptRoute.ShowDialog();

                }
                else
                {
                    ENUtils.ShowMessage("Map Route Details not found");

                }
            }
            catch (Exception ex)
            {

            }
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
            catch (Exception ex)
            {


            }
        }

        private void btnSms_Click(object sender, EventArgs e)
        {
            try
            {
                frmSMSAll sms = new frmSMSAll(txtCustomerMobileNo.Text);
                sms.ShowDialog();
                sms.Dispose();

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btn_notes_Click(object sender, EventArgs e)
        {
            try
            {
                ResetBookingStatusId();

                if (pnlAccpassword != null && pnlAccpassword.Visible == true)
                {
                    int? companyId = ddlCompany.SelectedValue.ToIntorNull();

                    Gen_Company obj = General.GetObject<Gen_Company>(c => c.Id == companyId);
                    string AccountPassword = obj.PasswordAccount.ToStr();

                    if (txtAccPassword.Text.ToStr().ToLower() == AccountPassword.ToStr().ToLower())
                    {
                        this.Save();
                        if (IsSave == true)
                        {
                            frmBookingNotes Note = new frmBookingNotes(objMaster.PrimaryKeyValue.ToInt());
                            Note.ShowDialog();
                            Note.Dispose();
                        }
                    }
                    else
                    {
                        RadMessageBox.Show("Please Enter Correct Company Password!");
                    }
                }
                else
                {
                    this.Save();
                    if (IsSave == true)
                    {
                        frmBookingNotes Note = new frmBookingNotes(objMaster.PrimaryKeyValue.ToInt());
                        Note.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }




        private void ddlVehicleType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
            else if (e.KeyCode == Keys.Right)
            {
                ddlCustomerName.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {

                if (ddlVehicleType.SelectionStart == 0)
                {
                    FocusOnFromAddress();
                }
            }
        }





        void child_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                numTotalLuggages.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                ddlVehicleType.Focus();

            }
            else if (e.KeyCode == Keys.Right)
            {
                if (num_TotalPassengers.SpinElement.TextBoxItem.SelectionStart == num_TotalPassengers.SpinElement.TextBoxItem.Text.Length)
                {
                    numTotalLuggages.Focus();
                }

            }
            else if (e.KeyCode == Keys.Down)
            {
                if (num_TotalPassengers.SpinElement.TextBoxItem.SelectionStart == num_TotalPassengers.SpinElement.TextBoxItem.Text.Length)
                {
                    txtSpecialRequirements.Focus();
                }

            }

        }
        void totalLugg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSpecialRequirements.Focus();

                //int? LocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
                //int? toLocTypeId = ddlToLocType.SelectedValue.ToIntorNull();


                //if (string.IsNullOrEmpty(txtToAddress.Text.ToStr().Trim()))
                //{

                //    if ((toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE))
                //    {


                //        FocusOnToAddress();

                //    }
                //    else if ((toLocTypeId == Enums.LOCATION_TYPES.POSTCODE))
                //    {
                //        FocusOnToPostCode();

                //    }
                //    else
                //    {

                //        FocusOnToLocation();
                //    }
                //}
                //else
                //{


                //    if (chkIsCompanyRates.Checked)
                //    {
                //        if (ddlDepartment != null)
                //            ddlDepartment.Select();
                //    }
                //    else
                //    {
                //        txtSpecialRequirements.Focus();
                //    }
                //}
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (numTotalLuggages.SpinElement.TextBoxItem.SelectionStart == numTotalLuggages.SpinElement.TextBoxItem.Text.Length)
                {
                    FocusOnMobNo();
                }

            }
            else if (e.KeyCode == Keys.Left)
            {
                if (numTotalLuggages.SpinElement.TextBoxItem.SelectionStart == 0)
                {
                    FocusToPassenger();
                }

            }
        }

        private void ddlCompany_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void ddlEscort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void ddlDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private void txtSpecialRequirements_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FocusOnPickupDate();
                // FocusOnCustomer();
                //btnSaveNew.Focus();
            }
            else if (e.KeyCode == Keys.Left)
            {
                int position = txtSpecialRequirements.SelectionStart;
                if (position == 0)
                {
                    FocusOnCustomer();
                    // ddlCustomerName.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                int position = txtSpecialRequirements.SelectionStart;
                if (position == 0)
                {
                    SendKeys.Send("{Enter}");
                    // ddlCustomerName.Focus();
                }
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


                        UpdateAutoCalculateFares();
                        //   txtViaAddress.Focus();
                    }



                }
            }
            catch (Exception ex)
            {


            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            btnSelectVia.ToggleState = ToggleState.Off;
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

            if (btnExit.Focused)
                return;

            //object o = "LoadNearestMap";
            //SendAsyncRequest(o);

            LoadNearestMap();

        }

        private void LoadNearestMap()
        {
            new Thread(new ThreadStart(LoadNearestJobDrivers)).Start();

        }


        delegate void UIDelegate();
        private void LoadNearestJobDrivers()
        {

            try
            {

                if (this.InvokeRequired)
                {

                    UIDelegate d = new UIDelegate(LoadNearest);
                    this.BeginInvoke(d);
                }
                else
                {
                    LoadNearest();

                }




            }
            catch (Exception ex)
            {


            }

        }



        private void LoadNearest()
        {
            try
            {







                int? jobStatusId = objMaster.PrimaryKeyValue != null ? objMaster.Current.BookingStatusId : Enums.BOOKINGSTATUS.WAITING;


                string fromAddress = General.GetPostCodeMatch(txtFromAddress.Text.Trim().ToUpper());
                string toAddress = General.GetPostCodeMatch(txtToAddress.Text.ToStr().Trim().ToUpper());

                int fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
                int toLocTypeId = ddlToLocType.SelectedValue.ToInt();

                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    if (txtFromAddress.Text.Trim().Length > 2 && txtFromAddress.Text.Contains(' ') == false && txtFromAddress.SelectedItem != null)
                    {
                        fromAddress = General.GetPostCodeMatch(txtFromAddress.SelectedItem.Trim());


                    }
                }
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    fromAddress = txtFromPostCode.Text.Trim();

                }
                else
                {
                    fromAddress = General.GetPostCodeMatch(ddlFromLocation.Text.ToStr().ToUpper().Trim());

                }



                if (toLocTypeId == Enums.LOCATION_TYPES.ADDRESS || toLocTypeId == Enums.LOCATION_TYPES.BASE)
                {
                    if (txtToAddress.Text.Trim().Length > 2 && txtToAddress.Text.Contains(' ') == false && txtToAddress.SelectedItem != null)
                        toAddress = General.GetPostCodeMatch(txtToAddress.SelectedItem.Trim());
                }
                else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    toAddress = txtToPostCode.Text.Trim();

                }
                else
                {
                    toAddress = General.GetPostCodeMatch(ddlToLocation.Text.ToStr().ToUpper().Trim());

                }


                if (string.IsNullOrEmpty(fromAddress))
                    return;


                if (!string.IsNullOrEmpty(toAddress))
                    toAddress += " UK";


                string pickupPoint = string.Empty;
                string pickupPointImageUrl = string.Empty;

                Gen_Coordinate pickupCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == fromAddress);


                if (pickupCoord == null)
                {

                    var coord = GetDistance.PostCodeToLongLat(fromAddress, "GBP");


                    if (coord != null)
                    {
                        pickupCoord = new Gen_Coordinate();
                        pickupCoord.PostCode = fromAddress;
                        pickupCoord.Latitude = coord.Value.Latitude;
                        pickupCoord.Longitude = coord.Value.Longitude;

                    }
                }


                StringBuilder nearestDrvLocations = new StringBuilder();
                double jobLatitude = 0;
                double jobLongitude = 0;
                double milesAway = 5;


                if (ddlMilesDrvs2 == null)
                {
                    this.ddlMilesDrvs2 = new System.Windows.Forms.ComboBox();


                    this.ddlMilesDrvs2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                    this.ddlMilesDrvs2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.ddlMilesDrvs2.FormattingEnabled = true;
                    this.ddlMilesDrvs2.Items.AddRange(new object[] {
                    "within 3 miles away",
                    "within 5 miles away",
                    "within 10 miles away"});
                    this.ddlMilesDrvs2.Location = new System.Drawing.Point(929, 349);
                    this.ddlMilesDrvs2.Name = "ddlMilesDrvs2";
                    this.ddlMilesDrvs2.Size = new System.Drawing.Size(283, 22);
                    this.ddlMilesDrvs2.TabIndex = 227;
                    //this.ddlMilesDrvs2.Visible = false;

                    ddlMilesDrvs2.Visible = true;

                    ddlMilesDrvs2.SelectedItem = ddlMilesDrvs2.Items[1];

                    this.ddlMilesDrvs2.SelectedIndexChanged += new System.EventHandler(this.ddlMilesDrvs2_SelectedIndexChanged);


                    this.pnlMain.Controls.Add(this.ddlMilesDrvs2);
                }

                if (ddlMilesDrvs2.SelectedIndex == 0)
                    milesAway = 3;
                else if (ddlMilesDrvs2.SelectedIndex == 2)
                    milesAway = 10;


                if (!string.IsNullOrEmpty(fromAddress) && string.IsNullOrEmpty(toAddress))
                {



                    if (pickupCoord != null)
                    {

                        pickupPoint = "['<h4>test</h4>'," + pickupCoord.Latitude + "," + pickupCoord.Longitude + "],";

                        pickupPointImageUrl = "'http://google.com/mapfiles/kml/paddle/A.png',";
                        jobLatitude = Convert.ToDouble(pickupCoord.Latitude);
                        jobLongitude = Convert.ToDouble(pickupCoord.Longitude);

                    }
                    else
                        return;

                }



                if (webBrowser1 == null)
                {
                    this.webBrowser1 = new System.Windows.Forms.WebBrowser();
                    this.webBrowser1.Location = new System.Drawing.Point(929, 36);
                    this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 0);
                    this.webBrowser1.Name = "webBrowser1";
                    this.webBrowser1.ScrollBarsEnabled = false;
                    this.webBrowser1.Size = new System.Drawing.Size(320, 310);
                    this.webBrowser1.TabIndex = 225;
                    this.webBrowser1.Visible = false;
                    this.pnlMain.Controls.Add(this.webBrowser1);

                }





                if (grdDrivers == null)
                {

                    this.grdDrivers = new System.Windows.Forms.DataGridView();
                    // ((System.ComponentModel.ISupportInitialize)(this.grdDrivers)).BeginInit();



                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
                    System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();



                    this.DriverId = new System.Windows.Forms.DataGridViewTextBoxColumn();
                    this.details = new System.Windows.Forms.DataGridViewTextBoxColumn();
                    this.btnDespatchJob = new System.Windows.Forms.DataGridViewButtonColumn();

                    this.DriverId.HeaderText = "DriverId";
                    this.DriverId.Name = "DriverId";
                    this.DriverId.ReadOnly = true;
                    this.DriverId.Visible = false;
                    // 
                    // details
                    // 
                    this.details.HeaderText = "details";
                    this.details.Name = "details";
                    this.details.ReadOnly = true;
                    this.details.Width = 200;
                    // 
                    // btnDespatchJob
                    // 
                    dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                    dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
                    dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
                    this.btnDespatchJob.DefaultCellStyle = dataGridViewCellStyle2;
                    this.btnDespatchJob.HeaderText = "btnDespatchJob";
                    this.btnDespatchJob.Name = "btnDespatchJob";
                    this.btnDespatchJob.ReadOnly = true;
                    this.btnDespatchJob.Text = "Despatch";
                    this.btnDespatchJob.UseColumnTextForButtonValue = true;
                    this.btnDespatchJob.Width = 80;


                    this.grdDrivers.AllowUserToAddRows = false;
                    this.grdDrivers.AllowUserToDeleteRows = false;
                    this.grdDrivers.BackgroundColor = System.Drawing.Color.FloralWhite;
                    dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                    dataGridViewCellStyle1.BackColor = System.Drawing.Color.SteelBlue;
                    dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
                    dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
                    dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
                    dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                    dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                    this.grdDrivers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
                    this.grdDrivers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                    this.grdDrivers.ColumnHeadersVisible = false;
                    this.grdDrivers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                    this.DriverId,
                    this.details,
                    this.btnDespatchJob});
                    dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                    dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
                    dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
                    dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
                    dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FloralWhite;
                    dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
                    dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
                    this.grdDrivers.DefaultCellStyle = dataGridViewCellStyle3;
                    this.grdDrivers.Location = new System.Drawing.Point(929, 36);
                    this.grdDrivers.Name = "grdDrivers";
                    this.grdDrivers.ReadOnly = true;
                    dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                    dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
                    dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 8.25F);
                    dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
                    dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
                    dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                    dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                    this.grdDrivers.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
                    this.grdDrivers.RowHeadersVisible = false;
                    this.grdDrivers.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FloralWhite;
                    this.grdDrivers.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                    this.grdDrivers.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FloralWhite;
                    this.grdDrivers.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                    this.grdDrivers.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
                    this.grdDrivers.RowTemplate.Height = 50;
                    this.grdDrivers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
                    this.grdDrivers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
                    this.grdDrivers.Size = new System.Drawing.Size(286, 650);
                    this.grdDrivers.TabIndex = 226;
                    this.grdDrivers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDrivers_CellClick);



                    //  ((System.ComponentModel.ISupportInitialize)(this.grdDrivers)).EndInit();

                    this.pnlMain.Controls.Add(this.grdDrivers);
                }

                if (webBrowser1.Visible == false)
                {
                    webBrowser1.Visible = true;
                    grdDrivers.Size = new Size(grdDrivers.Size.Width, 300);
                    grdDrivers.Location = new Point(grdDrivers.Location.X, 372);

                    grdDrivers.Font = new Font("Tahoma", 11, FontStyle.Bold);
                    ddlMilesDrvs2.Visible = true;
                }

                if (pickupCoord != null)
                {
                    jobLatitude = Convert.ToDouble(pickupCoord.Latitude);
                    jobLongitude = Convert.ToDouble(pickupCoord.Longitude);
                }


                if (jobStatusId == Enums.BOOKINGSTATUS.WAITING)
                {
                    // IList ListofAvailDrvs = null;


                    grdDrivers.Rows.Clear();

                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        var ListofAvailDrvs = (from a in db.GetTable<Fleet_DriverQueueList>().Where(c => c.Status == true && c.Fleet_Driver.HasPDA == true &&
                                      (c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE))
                                               join b in db.GetTable<Fleet_Driver_Location>().Where(c => c.Latitude != 0)
                                               on a.DriverId equals b.DriverId
                                               select new
                                               {
                                                   DriverId = a.DriverId,
                                                   DriverNo = a.Fleet_Driver.DriverNo,
                                                   DriverLocation = b.LocationName,
                                                   Latitude = b.Latitude,
                                                   Longitude = b.Longitude
                                               }).ToList();








                        //ListofAvailDrvs = (from a in AppVars.BLData.GetAll<Fleet_DriverQueueList>(c => c.Status == true && c.Fleet_Driver.HasPDA == true &&
                        //               (c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)).AsEnumerable()
                        //                      join b in AppVars.BLData.GetAll<Fleet_Driver_Location>(c => c.Latitude != 0).AsEnumerable()
                        //                      on a.DriverId equals b.DriverId
                        //                      select new
                        //                      {
                        //                          DriverId = a.DriverId,
                        //                          DriverNo = a.Fleet_Driver.DriverNo,
                        //                          DriverLocation = b.LocationName,
                        //                          Latitude = b.Latitude,
                        //                          Longitude = b.Longitude
                        //                      }).ToList();

                        var nearestDrivers = ListofAvailDrvs.Select(args => new
                        {
                            args.DriverId,
                            // MilesAwayFromPickup = GetNearestDistance(args.DriverLocation,fromAddress) ,
                            MilesAwayFromPickup = new DotNetCoords.LatLng(args.Latitude, args.Longitude).DistanceMiles(new DotNetCoords.LatLng(jobLatitude, jobLongitude)),
                            args.DriverNo,
                            Latitude = args.Latitude,
                            Longitude = args.Longitude,
                            Location = args.DriverLocation

                        }).OrderBy(args => args.MilesAwayFromPickup).Where(c => c.MilesAwayFromPickup <= milesAway).Take(3).ToList();

                        for (int i = 0; i < nearestDrivers.Count; i++)
                        {
                            nearestDrvLocations.Append("['<h4>" + nearestDrivers[i].Location + "</h4>'," + nearestDrivers[i].Latitude + "," + nearestDrivers[i].Longitude + "],");
                            grdDrivers.Rows.Add(nearestDrivers[i].DriverId, nearestDrivers[i].DriverNo + " is " + Math.Round(nearestDrivers[i].MilesAwayFromPickup, 1) + " miles away");

                        }

                    }


                    if (nearestDrvLocations.Length > 0)
                        nearestDrvLocations[nearestDrvLocations.Length - 1] = ' ';
                    else
                    {
                        if (pickupPoint.Length > 0)
                            pickupPoint = pickupPoint.Remove(pickupPoint.LastIndexOf(','));


                    }
                }
                else
                {



                    if (pickupPoint.Length > 0)
                        pickupPoint = pickupPoint.Remove(pickupPoint.LastIndexOf(','));


                }

                string text = "<!DOCTYPE html>" +
                                "<html>" +
                                "<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">" +
                                  "<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" />" +

                                  "<script src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>" +
                                  "<script src=\"http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.1.min.js\"></script>" +
                                "</head>" +
                                "<body>" +
                                  "<div id=\"map\" style=\"width: 300px; height: 310px;Left:-10px;Top:-13px\"></div>" +

                                  "<script type=\"text/javascript\">" +



                                    "var locations = [" +
                                       pickupPoint +
                                      nearestDrvLocations.ToString() +

                                    "];" +


                                   " var iconURLPrefix = 'http://maps.google.com/mapfiles/ms/icons/';" +




                                   " var icons = [" +
                                   pickupPointImageUrl +


                                     " iconURLPrefix + 'green-dot.png'," +
                                     " iconURLPrefix + 'green-dot.png'," +
                                     " iconURLPrefix + 'green-dot.png'," +
                                     " iconURLPrefix + 'green-dot.png'," +

                                   " ];" +
                                  "  var icons_length = icons.length;" +


                                  "  var shadow = {" +
                                   "   anchor: new google.maps.Point(15,33)," +
                                  "    url: iconURLPrefix + 'msmarker.shadow.png'" +
                                  "  };" +

                                   " var map = new google.maps.Map(document.getElementById('map'), {" +
                                    "  zoom: 14," +
                                   "   center: new google.maps.LatLng(" + jobLatitude + "," + jobLongitude + ")," +
                                   "   mapTypeId: google.maps.MapTypeId.ROADMAP," +
                                   "   mapTypeControl: false," +
                                   "   streetViewControl: false," +
                                   "   panControl: false," +
                                   "   zoomControlOptions: {" +
                                   "      position: google.maps.ControlPosition.LEFT_BOTTOM" +
                                  "    }" +
                                  "  });" +

                                 "   var infowindow = new google.maps.InfoWindow({" +
                                  "    maxWidth: 160" +
                                  "  });" +

                                 "   var marker;" +
                                  "  var markers = new Array();" +

                                  "  var iconCounter = 0;" +

                               " var directionsDisplay= new google.maps.DirectionsRenderer();" +

                               " var directionsService = new google.maps.DirectionsService();" +
                               " directionsDisplay.setMap(map);" +


                                   " for (var i = 0; i < locations.length; i++) {  " +
                                   "   marker = new google.maps.Marker({" +
                                   "     position: new google.maps.LatLng(locations[i][1], locations[i][2])," +
                                   "     map: map," +
                                  "      icon : icons[iconCounter]," +
                                   "     shadow: shadow" +
                                   "   });" +

                                   "   markers.push(marker);" +

                                   "   google.maps.event.addListener(marker, 'click', (function(marker, i) {" +
                                   "     return function() {" +
                                     "     infowindow.setContent(locations[i][0]);" +
                                    "      infowindow.open(map, marker);" +
                                    "    }" +
                                    "  })(marker, i));" +

                                   "   iconCounter++;" +
                                   "   if(iconCounter >= icons_length){" +
                                   "     iconCounter = 0;" +
                                   "   }" +
                                   " }" +

                                    "function AutoCenter() {" +
                                     "  var bounds = new google.maps.LatLngBounds();" +

                                     " $.each(markers, function (index, marker) {" +
                                    "    bounds.extend(marker.position);" +
                                    "  });";

                //  "   map.fitBounds(bounds);";

                if (!string.IsNullOrEmpty(fromAddress) && !string.IsNullOrEmpty(toAddress))
                {
                    text += "   map.fitBounds(bounds);";

                    text += "  var start = '" + fromAddress + " UK';" +
                      "    var end = '" + toAddress + "';" +
                      "    var request = {" +
                      "      origin: start," +
                       "     destination: end," +
                      "     travelMode: google.maps.TravelMode.DRIVING" +
                      "    };" +



                      "    directionsService.route(request, function(response, status) {" +
                      "      if (status == google.maps.DirectionsStatus.OK) {" +
                       "       directionsDisplay.setDirections(response);" +
                       "     }" +
                       "   });";
                }


                text += " };" + "  AutoCenter();" +

                 "    </script> " +
                 "  </body>" +
                   "</html>";



                webBrowser1.DocumentText = text;



                // webBrowser1.Refresh();
                GC.Collect();
            }
            catch (Exception ex)
            {


            }
        }






        #endregion

        private void grdDrivers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdDrivers.Columns[e.ColumnIndex].Name == "btnDespatchJob" && grdDrivers.CurrentCell is DataGridViewButtonCell)
            {
                if (RadMessageBox.Show("Are you sure you want to Despatch the job?", "Despatch", MessageBoxButtons.YesNo, RadMessageIcon.Question) == DialogResult.Yes)
                {
                    FillDriversCombo();

                    ddlDriver.SelectedValue = grdDrivers.CurrentRow.Cells["DriverId"].Value.ToInt();
                    Save();

                    if (!this.IsDespatched)
                    {
                        ddlDriver.SelectedValue = null;
                    }
                    else
                    {

                        Close();
                    }
                }


            }
        }

        private void btnNearestDrv_Click(object sender, EventArgs e)
        {
            //object o = "LoadNearestMap";
            //SendAsyncRequest(o);
            LoadNearestMap();
        }


        private bool is_in_circle(double circle_x, double circle_y, double r, double x, double y)
        {

            double d = new LatLng(Convert.ToDouble(circle_x), Convert.ToDouble(circle_y)).DistanceMiles(new LatLng(Convert.ToDouble(x), Convert.ToDouble(y)));

            //double d = Math.Sqrt(((circle_x - x) * (circle_x - x)) + ((circle_y - y) * (circle_y - y)));
            return d <= r;
        }



        private bool FindPoint(double pointLat, double pointLng, List<Gen_Zone_PolyVertice> PontosPolig)
        {//                             X               y               
            int sides = PontosPolig.Count();
            int j = sides - 1;
            bool pointStatus = false;


            if (sides == 1)
            {

                double radius = Convert.ToDouble(PontosPolig[0].Diameter) / 2;
                double lat = Convert.ToDouble(PontosPolig[0].Latitude);
                double lng = Convert.ToDouble(PontosPolig[0].Longitude);


                //double temp = ((lat - pointLat) * (lat - pointLat)) + ((lng - pointLng) * (lng - pointLng));

                //double dist = SqrRoot(temp);

                pointStatus = is_in_circle(pointLat, pointLng, radius, lat, lng);
                //  pointStatus = is_in_circle(lat, lng, radius, pointLat, pointLng);

                //if (dist <= radius)
                //    pointStatus = true;
            }
            else
            {

                for (int i = 0; i < sides; i++)
                {
                    if (PontosPolig[i].Longitude < pointLng && PontosPolig[j].Longitude >= pointLng ||
                        PontosPolig[j].Longitude < pointLng && PontosPolig[i].Longitude >= pointLng)
                    {
                        if (PontosPolig[i].Latitude + (pointLng - PontosPolig[i].Longitude) /
                            (PontosPolig[j].Longitude - PontosPolig[i].Longitude) * (PontosPolig[j].Latitude - PontosPolig[i].Latitude) < pointLat)
                        {
                            pointStatus = !pointStatus;
                        }
                    }
                    j = i;
                }
            }
            return pointStatus;
        }

        public static double SqrRoot(double t)
        {

            double lb = 0, ub = t, temp = 0;
            int count = 50;

            while (count != 0)
            {
                temp = (lb + ub) / 2;

                if (temp * temp == t)
                {

                    return temp;
                }
                else if (temp * temp > t)
                {
                    ub = temp;
                }
                else
                {

                    lb = temp;

                }



                count--;
            }

            return temp;


        }

        private void menu_JobReceipt_Click(object sender, EventArgs e)
        {
            try
            {
                long Id = objMaster.Current.Id;


                rptfrmJobReceiptDetails frm4 = new rptfrmJobReceiptDetails();
                frm4.DataSource = General.GetQueryable<Vu_BookingDetail>(d => d.Id == Id).ToList(); ;
                frm4.GenerateReport();

                if (frm4 != null)
                {
                    frm4.FormBorderStyle = FormBorderStyle.FixedSingle;
                    frm4.Size = new Size(800, 800);
                    frm4.ControlBox = true;
                    frm4.MaximizeBox = true;
                    frm4.MinimizeBox = true;
                    frm4.ShowDialog();
                    frm4.Dispose();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnPrintJob_DropDownOpening(object sender, EventArgs e)
        {
            try
            {


                if (this.menu_JobReceipt == null && AppVars.frmMDI.ListofUserRights.Count(c => c.formName == "rptfrmJobsListReceipts") > 0)
                {

                    this.menu_JobReceipt = new Telerik.WinControls.UI.RadMenuItem();
                    this.menu_JobReceipt.Name = "menu_JobReceipt";
                    this.menu_JobReceipt.Text = "Job Receipt";
                    this.menu_JobReceipt.Click += new System.EventHandler(this.menu_JobReceipt_Click);
                    this.btnPrintJob.Items.Add(menu_JobReceipt);
                }


            }
            catch (Exception ex)
            {


            }
        }

        private void btnConfirmationSMS_Click(object sender, EventArgs e)
        {
            SendConfirmationSMS();
        }


        private void SendConfirmationSMS()
        {
            try
            {
                if (objMaster.PrimaryKeyValue != null)
                {


                    string custNo = objMaster.Current.CustomerMobileNo.ToStr().Trim();

                    if (!string.IsNullOrEmpty(custNo))
                    {

                        // Send To Driver

                        EuroSMS objSMS = new EuroSMS();

                        string smsError2 = "";
                        string msg = string.Empty;

                        //if (Debugger.IsAttached == false)
                        //{

                        int idx = -1;
                        if (custNo.StartsWith("044") == true)
                        {
                            idx = custNo.IndexOf("044");
                            custNo = custNo.Substring(idx + 3);
                            custNo = custNo.Insert(0, "+44");
                        }

                        if (custNo.StartsWith("07"))
                        {
                            custNo = custNo.Substring(1);
                        }

                        if (custNo.StartsWith("044") == false || custNo.StartsWith("+44") == false)
                            custNo = custNo.Insert(0, "+44");
                        //  }

                        msg += GetMessage(AppVars.objPolicyConfiguration.ConfirmationSMSText.ToStr().Trim());



                        objSMS.ToNumber = custNo;
                        objSMS.Message = msg;

                        new Thread(delegate()
                        {
                            objSMS.Send(ref smsError2);
                        }).Start();

                        General.SaveSentSMS("Confirmation Text : " + msg, custNo);


                        Thread.Sleep(1000);


                        if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                        {

                            RadDesktopAlert alert = new RadDesktopAlert();
                            alert.AutoCloseDelay = 5;
                            alert.FadeAnimationType = FadeAnimationType.None;
                            alert.FixedSize = new Size(320, 130);
                            alert.CaptionText = "Confirmation SMS has been sent Successfully";
                            alert.ContentText = msg;
                            alert.Show();
                        }

                    }
                }

            }
            catch (Exception ex)
            {


            }
        }


        private string GetMessage(string message)
        {
            try
            {


                string msg = message;

                object propertyValue = string.Empty;
                foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                {


                    switch (tag.TagObjectName)
                    {
                        case "booking":

                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = objMaster.Current.GetType().GetProperty(val[0]).GetValue(objMaster.Current, null);

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
                                if (!string.IsNullOrEmpty(tag.ConditionNotNull) && objMaster.Current.GetType().GetProperty(tag.ConditionNotNull) != null)
                                {

                                    propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                }
                                else
                                {

                                    propertyValue = objMaster.Current.GetType().GetProperty(tag.TagPropertyValue).GetValue(objMaster.Current, null);
                                }
                            }


                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = objMaster.Current.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objMaster.Current, null);
                            }
                            break;



                        case "driver":

                            if (objMaster.Current.DriverId != null)
                            {
                                var ObjDriver = objMaster.Current.Fleet_Driver.DefaultIfEmpty();

                                if (tag.TagPropertyValue.Contains('.'))
                                {

                                    string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                    object parentObj = ObjDriver.GetType().GetProperty(val[0]).GetValue(ObjDriver, null);

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
                                    propertyValue = ObjDriver.GetType().GetProperty(tag.TagPropertyValue).GetValue(ObjDriver, null);
                                }

                                if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                                {
                                    propertyValue = ObjDriver.GetType().GetProperty(tag.TagPropertyValue2).GetValue(ObjDriver, null);
                                }
                            }
                            else
                            {

                                propertyValue = " - ";

                            }
                            break;



                        default:
                            propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                            break;

                    }




                    msg = msg.Replace(tag.TagMemberValue,
                        tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                }


                return msg.Replace("\n\n", "\n");
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);
                return "";
            }
        }




        private void btnAddGroup_Click(object sender, EventArgs e)
        {


            try
            {

                int? destinationId = ddlToLocation.SelectedValue.ToIntorNull();
                int? vehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                DateTime? flightDepartureDateTime = null;

                string pickupPoint = string.Empty;

                if (destinationId == null)
                {
                    ENUtils.ShowMessage("Required : Destination");
                    return;

                }


                if (vehicleTypeId == null)
                {
                    ENUtils.ShowMessage("Required : Vehicle Type");
                    return;

                }


                int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();


                if (fromLocTypeId == null)
                {
                    ENUtils.ShowMessage("Required : From Loc Type");
                    return;


                }

                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {

                    pickupPoint = txtFromAddress.Text.Trim();
                }
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    pickupPoint = txtFromPostCode.Text.Trim();
                }
                else
                {
                    pickupPoint = ddlFromLocation.Text.Trim();

                }


                if (string.IsNullOrEmpty(pickupPoint))
                {

                    ENUtils.ShowMessage("Required : Pickup Point");
                    return;
                }

                if (ddlToLocType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {

                    if (dtpFlightDepDate.Value == null)
                    {
                        ENUtils.ShowMessage("Required : Flight Departure Date");
                        return;
                    }


                    if (dtpFlightDepTime.Value == null)
                    {

                        ENUtils.ShowMessage("Required : Flight Departure Time");
                        return;
                    }


                    flightDepartureDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", dtpFlightDepDate.Value.ToDate()
                                                                               + dtpFlightDepTime.Value.Value.TimeOfDay).ToDateTime();


                }





                frmCreateGroup frmGroup = new frmCreateGroup(flightDepartureDateTime, vehicleTypeId, destinationId, pickupPoint.ToStr().Trim().ToUpper());
                frmGroup.ShowDialog();



                if (frmGroup.GroupId != 0)
                {

                    txtGroupJobNo.Tag = frmGroup.GroupId;
                    txtGroupJobNo.Text = frmGroup.GroupName;
                    txtGroupJobNo.AccessibleDescription = frmGroup.NoOfSeats.ToStr();


                    LockUnLockShuttleGroupDetails(true);

                    num_TotalPassengers.Enabled = true;
                }


                frmGroup.Dispose();





            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }


        }


        private void LockUnLockShuttleGroupDetails(bool locked)
        {
            ddlBookingType.Enabled = !locked;
            ddlToLocType.Enabled = !locked;
            chkReverse.Enabled = !locked;
            ddlToLocation.Enabled = !locked;
            ddlVehicleType.Enabled = !locked;
            num_TotalPassengers.Enabled = !locked;

        }

        private void btnViewGroup_Click(object sender, EventArgs e)
        {
            try
            {
                int destinationId = ddlToLocation.SelectedValue.ToInt();
                int vehicleTypeId = ddlVehicleType.SelectedValue.ToInt();
                int totalPassengers = num_TotalPassengers.Value.ToInt();

                int destLocTypeId = ddlToLocType.SelectedValue.ToInt();

                if (destinationId == 0)
                {
                    ENUtils.ShowMessage("Required : Destination");
                    return;
                }

                if (vehicleTypeId == 0)
                {
                    ENUtils.ShowMessage("Required : Vehicle");
                    return;

                }


                if (destLocTypeId == 0)
                {
                    ENUtils.ShowMessage("Required : To Locaion Type");
                    return;

                }


                int? fromLocTypeId = ddlFromLocType.SelectedValue.ToIntorNull();
                string pickupPoint = string.Empty;


                if (fromLocTypeId == null)
                {
                    ENUtils.ShowMessage("Required : From Loc Type");
                    return;


                }




                if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {

                    pickupPoint = txtFromAddress.Text.Trim();
                }
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {
                    pickupPoint = txtFromPostCode.Text.Trim();
                }
                else
                {
                    pickupPoint = ddlFromLocation.Text.Trim();

                }


                if (string.IsNullOrEmpty(pickupPoint))
                {

                    ENUtils.ShowMessage("Required : Pickup Point");
                    return;
                }


                if (totalPassengers == 0)
                {
                    ENUtils.ShowMessage("Required : No of Passengers");
                    return;

                }


                string postcode = General.GetPostCodeMatch(pickupPoint.ToUpper());
                int zoneId = 0;
                if (postcode.Contains(' '))
                {

                    postcode = postcode.Split(' ')[0];

                }

                postcode = General.CheckIfSpecialPostCode(postcode);


                if (!string.IsNullOrEmpty(postcode))
                {
                    zoneId = General.GetQueryable<Gen_ShuttleZone_AssociatedPostCode>(c => c.PostCode == postcode).FirstOrDefault().DefaultIfEmpty().ZoneId;
                }





                if (General.GetQueryable<BookingGroup>(c => c.DestinationId == destinationId && c.VehicleTypeId == vehicleTypeId
                    && (c.TripStatusId != null && c.TripStatusId == Enums.BOOKING_TRIPSTATUS.WAITING)
                   && (c.PickupZoneId == zoneId || c.Gen_ShuttleZone.Gen_ShuttleZone_AllowedZones.Count(a => a.AllowedZoneId == zoneId) > 0)).Count() == 0)
                {
                    ENUtils.ShowMessage("No Group found from the above details");
                    return;
                }





                frmPickGroup frmGroup = new frmPickGroup(destLocTypeId, destinationId, vehicleTypeId, zoneId, totalPassengers);

                frmGroup.StartPosition = FormStartPosition.CenterScreen;
                frmGroup.ShowDialog();






                BookingGroup grp = frmGroup.SelectedGroup;

                if (grp != null)
                {

                    txtGroupJobNo.Text = grp.GroupName.ToStr();

                    txtGroupJobNo.Tag = grp.Id;

                    //   txtGroupJobNo.AccessibleDescription

                }


                frmGroup.Dispose();
                frmGroup = null;







            }
            catch (Exception ex)
            {


            }
        }

        private void btnClearGroup_Click(object sender, EventArgs e)
        {
            txtGroupJobNo.Text = string.Empty;
            txtGroupJobNo.Tag = null;


            LockUnLockShuttleGroupDetails(false);


        }

        private void btnSendInvoice_Click(object sender, EventArgs e)
        {
            SendInvoice();



        }

        private void SendInvoice()
        {

            if (objMaster.Current == null)
                return;

            try
            {
             


                if (AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool())
                {

                    JATEmail.SendBookingCompletionEmail(objMaster.Current);

                }
                else
                {


                    if (objMaster.Current.CompanyId != null)
                    {
                        SendCompanyInvoice();
                    }
                    else
                    {

                        SendCustomerInvoice();
                    }

                }



            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void SendCustomerInvoice()
        {

            long jobId = objMaster.Current.Id;

            var obj = General.GetObject<Invoice_Charge>(c => c.BookingId == jobId);

            if (obj != null)
            {

                if (General.GetQueryable<Invoice_Charge>(c => c.InvoiceId == obj.InvoiceId).Count() <= 2)
                {
                    SendCustomerEmail(obj.InvoiceId.ToLong(), obj.Invoice.DefaultIfEmpty().InvoiceNo.ToStr().Trim());
                }

            }
            else
            {

                int? customerId = objMaster.Current.CustomerId;
                if (customerId == null)
                {
                    string customerName = ddlCustomerName.Text.ToStr().Trim();
                    string mobNo = txtCustomerMobileNo.Text.ToStr().Trim();
                    string telNo = txtCustomerPhoneNo.Text.ToStr().Trim();


                    if (string.IsNullOrEmpty(mobNo) && string.IsNullOrEmpty(telNo))
                    {
                        ENUtils.ShowMessage("Please enter Telephone or Mobile No");
                        return;
                    }


                    var objCust = General.GetObject<Customer>(c => c.Name.ToUpper() == customerName &&
                      ((c.MobileNo == mobNo || mobNo == string.Empty) || (c.TelephoneNo == telNo)));

                    if (objCust != null)
                    {

                        customerId = objCust.Id;
                    }
                    else
                    {
                        CustomerBO objCustBO = new CustomerBO();
                        objCustBO.New();
                        objCustBO.Current.Name = customerName;
                        objCustBO.Current.Email = txtEmail.Text.Trim();
                        objCustBO.Current.Address1 = txtFromAddress.Text.Trim();

                        if (string.IsNullOrEmpty(objCustBO.Current.Email.ToStr()))
                            objCustBO.Current.Email = "abc@xyz.com";

                        objCustBO.Save();


                        customerId = objCustBO.Current.Id;

                    }
                }

                InvoiceBO invBO = new InvoiceBO();
                try
                {


                    invBO.New();

                    invBO.Current.InvoiceDate = DateTime.Now.ToDate();
                    invBO.Current.CustomerId = customerId;
                    invBO.Current.InvoiceTypeId = Enums.INVOICE_TYPE.CUSTOMER;

                    decimal invoiceTotal = objMaster.Current.CustomerPrice.ToDecimal();


                    invoiceTotal += objMaster.Current.CongtionCharges.ToDecimal() + objMaster.Current.MeetAndGreetCharges.ToDecimal() + objMaster.Current.ExtraDropCharges.ToDecimal();

                    invBO.Current.InvoiceTotal = invoiceTotal;

                    invBO.Current.Invoice_Charges.Add(new Invoice_Charge
                    {
                        BookingId = jobId


                    });


                    invBO.Save();


                    SendCustomerEmail(invBO.Current.Id, invBO.Current.InvoiceNo.ToStr());
                }
                catch (Exception ex)
                {
                    if (invBO.Errors.Count > 0)
                    {
                        ENUtils.ShowMessage(invBO.ShowErrors());

                    }
                    else
                    {

                        ENUtils.ShowMessage(ex.Message);
                    }


                }

            }

        }

        private void SendCompanyInvoice()
        {
            long jobId = objMaster.Current.Id;

            var obj = General.GetObject<Invoice_Charge>(c => c.BookingId == jobId);

            if (obj != null)
            {

                if (General.GetQueryable<Invoice_Charge>(c => c.InvoiceId == obj.InvoiceId).Count() <= 2)
                {
                    SendCompanyEmail(obj.InvoiceId.ToLong(), obj.Invoice.DefaultIfEmpty().InvoiceNo.ToStr().Trim());
                }

            }
            else
            {

                int? companyId = objMaster.Current.CompanyId;


                InvoiceBO invBO = new InvoiceBO();
                try
                {


                    invBO.New();

                    invBO.Current.InvoiceDate = DateTime.Now.ToDate();
                    invBO.Current.CompanyId = companyId;
                    invBO.Current.InvoiceTypeId = Enums.INVOICE_TYPE.ACCOUNT;

                    decimal invoiceTotal = objMaster.Current.CompanyPrice.ToDecimal();


                    invoiceTotal += objMaster.Current.ParkingCharges.ToDecimal() + objMaster.Current.WaitingCharges.ToDecimal()+ objMaster.Current.TipAmount.ToDecimal();

                    invBO.Current.InvoiceTotal = invoiceTotal;

                    invBO.Current.Invoice_Charges.Add(new Invoice_Charge
                    {
                        BookingId = jobId


                    });


                    invBO.Save();


                    SendCustomerEmail(invBO.Current.Id, invBO.Current.InvoiceNo.ToStr());
                }
                catch (Exception ex)
                {
                    if (invBO.Errors.Count > 0)
                    {
                        ENUtils.ShowMessage(invBO.ShowErrors());

                    }
                    else
                    {

                        ENUtils.ShowMessage(ex.Message);
                    }


                }

            }

        }

        private void SendCompanyEmail(long invoiceId, string invoiceNo)
        {
            try
            {


                if (invoiceId == 0)
                    return;

                frmInvoiceReport frm = new frmInvoiceReport();
                var list = General.GetQueryable<vu_Invoice>(a => a.Id == invoiceId).ToList();
                int count = list.Count;
                frm.DataSource = list;
                frm.ObjInvoice =General.GetObject<Invoice>(c=>c.Id==invoiceId);
                frm.GenerateReport();
                frm.SendEmail(invoiceNo, objMaster.Current.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim());


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private void SendCustomerEmail(long invoiceId, string invoiceNo)
        {
            try
            {


                if (invoiceId == 0)
                    return;

                rptfrmCustomerInvoice frm = new rptfrmCustomerInvoice(invoiceId);
                var list = General.GetQueryable<vu_Invoice>(a => a.Id == invoiceId).ToList();
                int count = list.Count;
                frm.DataSource = list;
                frm.GenerateReport();
                frm.SendEmail(invoiceNo, txtEmail.Text.Trim());


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void btnPickAccountBooking_Click(object sender, EventArgs e)
        {
            SearchAccountBooking();
        }

        private void SearchAccountBooking()
        {
            try
            {
                int companyId = ddlCompany.SelectedValue.ToInt();

                if (companyId != 0)
                {

                    frmSearchBooking frm = new frmSearchBooking(companyId);
                    frm.ShowDialog();



                    if (frm.IsSelected)
                    {

                        txtCustomerMobileNo.TextChanged -= new EventHandler(txtCustomerMobileNo_TextChanged);
                        txtCustomerPhoneNo.TextChanged -= new EventHandler(txtCustomerPhoneNo_TextChanged);

                        PickBookingComplete(frm.CustomerName, frm.phoneNo, frm.mobileNo, frm.fromLocTypeId, frm.toLocTypeId, frm.fromLocId, frm.toLocId, frm.from, frm.to, frm.fare, false, frm.bookingTypeId, frm.CustEmail);

                        txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
                        txtCustomerPhoneNo.TextChanged += new EventHandler(txtCustomerPhoneNo_TextChanged);

                        numCustomerFares.Value = frm.customerFare;

                        if (numCompanyFares != null)
                            numCompanyFares.Value = frm.companyFare;


                        if (frm.viaString.ToStr().Trim().Length > 0)
                        {

                            if (grdVia == null)
                            {
                                CreateViaPanel();

                            }

                            string[] viaArr = frm.viaString.ToStr().Trim().Split(new char[] { ',' });

                            grdVia.Rows.Clear();

                            GridViewRowInfo row = null;
                            for (int i = 0; i < viaArr.Count(); i++)
                            {


                                row = grdVia.Rows.AddNew();
                                row.Cells["FROMVIALOCTYPEID"].Value = Enums.LOCATION_TYPES.ADDRESS;
                                row.Cells["FROMTYPELABEL"].Value = "";

                                row.Cells["FROMTYPEVALUE"].Value = "";

                                row.Cells["VIALOCATIONID"].Value = null;
                                row.Cells["VIALOCATIONLABEL"].Value = "Address";
                                row.Cells["VIALOCATIONVALUE"].Value = viaArr[i];
                            }

                            btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                            btnSelectVia.ButtonElement.ButtonFillElement.BackColor = Color.DarkOrange;
                            btnSelectVia.ButtonElement.ButtonFillElement.NumberOfColors = 1;
                        }

                    }


                    frm.Dispose();
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void btnTrackDriver_Click(object sender, EventArgs e)
        {
            try
            {
                if (objMaster.PrimaryKeyValue != null && objMaster.Current != null)
                {

                    if (AppVars.objPolicyConfiguration.TrackDriverType.ToBool() == true)
                    {

                        long jobId = objMaster.Current.Id;

                        if (jobId > 0)
                        {

                            string driverNo = objMaster.Current.Fleet_Driver.DefaultIfEmpty().DriverNo.ToStr().Trim();

                            if (driverNo.Length > 0)
                            {

                                Thread smsThread = new Thread(delegate()
                                {
                                    new BroadcasterData().BroadCastToPort("**track driver=" + driverNo + "=" + Environment.MachineName, 3530);

                                });


                                smsThread.Start();

                                System.Threading.Thread.Sleep(1000);
                            }
                        }

                    }
                    else
                    {

                        rptJobRouthPathGoogle rpt = new rptJobRouthPathGoogle(General.GetObject<Booking>(c => c.Id == objMaster.Current.Id), true);
                        rpt.ShowDialog();

                        rpt.Dispose();
                    }

                }
            }
            catch (Exception ex)
            {


            }
        }





        private void InitializeDirectionCombo()
        {

            this.ddlDirection = new System.Windows.Forms.ComboBox();
            this.lblDirection = new Telerik.WinControls.UI.RadLabel();

            this.pnlMain.Controls.Add(this.ddlDirection);
            this.pnlMain.Controls.Add(this.lblDirection);


            // 
            // ddlDirection
            // 
            this.ddlDirection.BackColor = System.Drawing.Color.White;
            this.ddlDirection.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDirection.FormattingEnabled = true;
            this.ddlDirection.Items.AddRange(new object[] {
            "InBound",
            "OutBound"});
            this.ddlDirection.Location = new System.Drawing.Point(1075, 45);
            this.ddlDirection.Name = "ddlDirection";
            this.ddlDirection.Size = new System.Drawing.Size(97, 24);
            this.ddlDirection.TabIndex = 268;
            // 
            // lblDirection
            // 

            ((System.ComponentModel.ISupportInitialize)(this.lblDirection)).BeginInit();
            this.lblDirection.BackColor = System.Drawing.Color.Transparent;
            this.lblDirection.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDirection.Location = new System.Drawing.Point(1001, 47);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(68, 22);
            this.lblDirection.TabIndex = 267;
            this.lblDirection.Text = "Direction";




            ((System.ComponentModel.ISupportInitialize)(this.lblDirection)).EndInit();
        }

        private void btnPasteBooking_Click(object sender, EventArgs e)
        {

            if (AppVars.objCopyBooking != null)
            {

                try
                {


                    txtVehicleNo.Text = AppVars.objCopyBooking.Fleet_Master.DefaultIfEmpty().Plateno.ToStr();

                    ddlSubCompany.SelectedValue = AppVars.objCopyBooking.SubcompanyId.ToInt();

                    ddlBookingType.SelectedValue = AppVars.objCopyBooking.BookingTypeId;

                    chkQuotation.Location = new Point(chkQuotation.Location.X, chkQuotation.Location.Y + 20);
                    chkQuotation.Checked = AppVars.objCopyBooking.IsQuotation.ToBool();
                    WasQuotiation = chkQuotation.Checked;





                    ddlFromLocType.SelectedValue = AppVars.objCopyBooking.FromLocTypeId;
                    ddlToLocType.SelectedValue = AppVars.objCopyBooking.ToLocTypeId;

                    DetachLocationsSelectionEvent(ddlFromLocation);
                    ddlFromLocation.SelectedValue = AppVars.objCopyBooking.FromLocId;
                    AttachLocationSelectionEvent(ddlFromLocation);

                    DetachLocationsSelectionEvent(ddlToLocation);
                    ddlToLocation.SelectedValue = AppVars.objCopyBooking.ToLocId;
                    AttachLocationSelectionEvent(ddlToLocation);

                    ddlVehicleType.SelectedValue = AppVars.objCopyBooking.VehicleTypeId;


                    ddlCustomerName.Text = AppVars.objCopyBooking.CustomerName;

                    txtCustomerMobileNo.TextChanged -= new EventHandler(txtCustomerMobileNo_TextChanged);
                    txtCustomerPhoneNo.TextChanged -= new EventHandler(txtCustomerPhoneNo_TextChanged);




                    txtCustomerMobileNo.Text = AppVars.objCopyBooking.CustomerMobileNo;
                    txtCustomerPhoneNo.Text = AppVars.objCopyBooking.CustomerPhoneNo;
                    txtEmail.Text = AppVars.objCopyBooking.CustomerEmail.ToStr().Trim();
                    numCustomerFares.Value = AppVars.objCopyBooking.CustomerPrice.ToDecimal();

                    txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
                    txtCustomerPhoneNo.TextChanged += new EventHandler(txtCustomerPhoneNo_TextChanged);



                    txtSpecialRequirements.Text = AppVars.objCopyBooking.SpecialRequirements;


                    int journeyTypeId = AppVars.objCopyBooking.JourneyTypeId.ToInt();

                    opt_JOneWay.ToggleStateChanging -= opt_JOneWay_ToggleStateChanging;


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

                    if (ddlReturnFromAirport != null)
                        ddlReturnFromAirport.SelectedValue = AppVars.objCopyBooking.ReturnFromLocId;




                    opt_JOneWay.ToggleStateChanging += new StateChangingEventHandler(opt_JOneWay_ToggleStateChanging);



                    chkIsCompanyRates.Checked = AppVars.objCopyBooking.IsCompanyWise.ToBool();

                    if (AppVars.objCopyBooking.CompanyId != null && AppVars.objCopyBooking.Gen_Company.IsClosed.ToBool())
                    {

                        var data = (List<Gen_Company>)ddlCompany.DataSource;
                        data.Add(AppVars.objCopyBooking.Gen_Company);

                        ddlCompany.SelectedValueChanged -= new EventHandler(ddlCompany_SelectedValueChanged);
                        ComboFunctions.FillCompanyCombo(ddlCompany, data);
                        ddlCompany.SelectedValueChanged+=new EventHandler(ddlCompany_SelectedValueChanged);
                    }


                    ddlCompany.SelectedValue = AppVars.objCopyBooking.CompanyId;

                    ddlPaymentType.SelectedValue = AppVars.objCopyBooking.PaymentTypeId;



                    if (pnlOrderNo != null)
                    {

                        txtOrderNo.Text = AppVars.objCopyBooking.OrderNo.ToStr();
                        txtPupilNo.Text = AppVars.objCopyBooking.PupilNo.ToStr();

                    }

                    if (txtAccountBookedBy != null)
                    {
                        txtAccountBookedBy.Text = AppVars.objCopyBooking.BookedBy.ToStr().Trim();
                    }


                    if (chkTakenByAgent != null)
                    {

                        chkTakenByAgent.Checked = AppVars.objCopyBooking.JobTakenByCompany.ToBool();
                        numAgentCommissionPercent.Value = AppVars.objCopyBooking.AgentCommissionPercent.ToInt();
                        ddlAgentCommissionType.SelectedIndex = AppVars.objCopyBooking.FromFlightNo.ToStr().Trim() == "Percent" ? 0 : 1;
                        numAgentCommission.Value = AppVars.objCopyBooking.AgentCommission.ToDecimal();
                    }

                    if (ddlDepartment != null)
                        ddlDepartment.SelectedValue = AppVars.objCopyBooking.DepartmentId;



                    dtpPickupDate.Value = AppVars.objCopyBooking.PickupDateTime.ToDate();
                    dtpPickupTime.Value = AppVars.objCopyBooking.PickupDateTime;

                    if (dtpReturnPickupDate != null)
                    {
                        dtpReturnPickupDate.Value = AppVars.objCopyBooking.ReturnPickupDateTime.ToDateorNull();
                        dtpReturnPickupTime.Value = AppVars.objCopyBooking.ReturnPickupDateTime.ToDateTimeorNull();
                        ddlReturnDriver.SelectedValue = AppVars.objCopyBooking.ReturnDriverId;
                    }

                    if (txtReturnSpecialReq != null)
                    {
                        if (AppVars.objCopyBooking.BookingReturns.Count > 0)
                        {
                            txtReturnSpecialReq.Text = AppVars.objCopyBooking.BookingReturns[0].DefaultIfEmpty().SpecialRequirements.ToStr();

                            if (ddlReturnVehicleType != null)
                                ddlReturnVehicleType.SelectedValue = AppVars.objCopyBooking.BookingReturns[0].DefaultIfEmpty().VehicleTypeId;
                        }
                    }





                    num_TotalPassengers.Value = AppVars.objCopyBooking.NoofPassengers.ToDecimal();
                    numTotalLuggages.Value = AppVars.objCopyBooking.NoofLuggages.ToDecimal();

                    numFareRate.Value = AppVars.objCopyBooking.FareRate.ToDecimal();

                    if (numReturnFare != null)
                        numReturnFare.Value = AppVars.objCopyBooking.ReturnFareRate.ToDecimal();

                    if (numCompanyFares != null)
                    {
                        numCompanyFares.Value = AppVars.objCopyBooking.CompanyPrice.ToDecimal();

                        if (journeyTypeId == Enums.JOURNEY_TYPES.RETURN)
                        {
                            numReturnCompanyFares.Value = AppVars.objCopyBooking.WaitingMins.ToDecimal();

                        }


                    }

                    numParkingChrgs.Value = AppVars.objCopyBooking.ParkingCharges.ToDecimal();
                    numWaitingChrgs.Value = AppVars.objCopyBooking.WaitingCharges.ToDecimal();
                    numExtraChrgs.Value = AppVars.objCopyBooking.ExtraDropCharges.ToDecimal();
                    numMeetCharges.Value = AppVars.objCopyBooking.MeetAndGreetCharges.ToDecimal();
                    numCongChrgs.Value = AppVars.objCopyBooking.CongtionCharges.ToDecimal();

                    numTotalChrgs.Value = AppVars.objCopyBooking.TotalCharges.ToDecimal();

                    if (pnlComcab != null)
                    {
                        numComcab_Cash.Value = AppVars.objCopyBooking.CashRate.ToDecimal();
                        numComcab_Account.Value = AppVars.objCopyBooking.AccountRate.ToDecimal();
                        numComcab_ExtraMile.Value = AppVars.objCopyBooking.ExtraMile.ToDecimal();
                        numComcab_WaitingMin.Value = AppVars.objCopyBooking.WaitingMins.ToDecimal();
                    }


                    txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    txtFromAddress.Text = AppVars.objCopyBooking.FromAddress.ToStr();
                    txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

                    txtFromFlightDoorNo.Text = AppVars.objCopyBooking.FromDoorNo.ToStr();
                    txtFromStreetComing.Text = AppVars.objCopyBooking.FromStreet.ToStr();

                    txtFromPostCode.Text = AppVars.objCopyBooking.FromPostCode.ToStr();

                    txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
                    txtToAddress.Text = AppVars.objCopyBooking.ToAddress.ToStr();
                    txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);


                    txtToFlightDoorNo.Text = AppVars.objCopyBooking.ToDoorNo.ToStr();
                    txtToStreetComing.Text = AppVars.objCopyBooking.ToStreet.ToStr();

                    txtToPostCode.Text = AppVars.objCopyBooking.ToPostCode.ToStr();



                    chkIsCommissionWise.Checked = AppVars.objCopyBooking.IsCommissionWise.ToBool();
                    ddlCommissionType.SelectedValue = AppVars.objCopyBooking.DriverCommissionType.ToStr().Trim();
                    numDriverCommission.Value = AppVars.objCopyBooking.DriverCommission.ToDecimal();

                    chkAutoDespatch.Checked = AppVars.objCopyBooking.AutoDespatch.ToBool();
                    DateTime? pickUpDate = AppVars.objCopyBooking.PickupDateTime;


                    if (AppVars.objCopyBooking.AutoDespatchTime != null)
                    {
                        DateTime? autoDespatchDate = AppVars.objCopyBooking.AutoDespatchTime;
                        int mins = pickUpDate.Value.TimeOfDay.Subtract(autoDespatchDate.Value.TimeOfDay).Minutes.ToInt();
                        numBeforeMinutes.Value = mins < 0 ? 10 : mins;
                    }
                    else
                        numBeforeMinutes.Value = 10;

                    //    ShowAutoDespatchLabels(AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool());







                    int fromLocTypeId = AppVars.objCopyBooking.FromLocTypeId.ToInt();

                    if (fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                        txtFromAddress.Focus();
                    else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                        txtFromPostCode.Focus();
                    else
                        ddlFromLocation.Focus();





                    ddlPickupPlot.SelectedValue = AppVars.objCopyBooking.ZoneId.ToInt();
                    ddlDropOffPlot.SelectedValue = AppVars.objCopyBooking.DropOffZoneId.ToInt();
                    // txtPickupPlot.Text = objMaster.Current.Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr();
                    // txtDropOffZone.Text = objMaster.Current.Gen_Zone.DefaultIfEmpty().ZoneName.ToStr();









                    // Shuttle Job Working



                    if (ddlDirection != null)
                        ddlDirection.Text = AppVars.objCopyBooking.BoundType.ToStr().Trim();

                    txtFaresPostedFrom.Text = AppVars.objCopyBooking.FaresPostedFrom.ToStr();

                    chkQuotedPrice.Checked = objMaster.Current.IsQuotedPrice.ToBool();

                    //if (!string.IsNullOrEmpty(txtFaresPostedFrom.Text))
                    //{
                    //    txtFaresPostedFrom.Visible = true;

                    //    chkQuotedPrice.Visible = false;
                    //}
                    //else
                    //{
                    //    chkQuotedPrice.Visible = true;

                    //}

                    if (ddlBabyseat1 != null && ddlbabyseat2 != null)
                    {
                        string babyseats = AppVars.objCopyBooking.BabySeats.ToStr();
                        if (!string.IsNullOrEmpty(babyseats) && babyseats.Contains("<<<"))
                        {

                            string[] arr = babyseats.Split(new string[] { "<<<" }, StringSplitOptions.None);

                            if (arr.Count() == 2)
                            {
                                ddlBabyseat1.SelectedItem = arr[0].ToStr().Trim();
                                ddlbabyseat2.SelectedItem = arr[1].ToStr().Trim();

                            }
                        }
                    }




                    //th = new System.Threading.Thread(new ThreadStart(DisplayBooking_Map));
                    //th.IsBackground = true;
                    //th.Start();

                    lblMap.Text = AppVars.objCopyBooking.DistanceString.ToStr();



                    if (AppVars.objCopyBooking.Booking_ViaLocations.Count > 0)
                    {
                        CreateViaPanel();


                        GridViewRowInfo row = null;
                        foreach (var item in AppVars.objCopyBooking.Booking_ViaLocations)
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

                        ClearViaDetails();


                        btnSelectVia.Text = "Show Via Point(" + grdVia.Rows.Count + ")";
                    }





                }
                catch (Exception ex)
                {
                    ENUtils.ShowMessage(ex.Message);

                }


            }



        }

        private void btnComplaint_Click(object sender, EventArgs e)
        {
            try
            {


                if (objMaster.PrimaryKeyValue == null)
                    return;



                List<Complaint> listofBookingComplaints = General.GetQueryable<Complaint>(c => c.BookingId == objMaster.Current.Id).ToList();


                int lastComplaintId = listofBookingComplaints.OrderByDescending(c => c.ComplainDateTime).FirstOrDefault().DefaultIfEmpty().Id;


                frmComplaint frmComp = new frmComplaint(lastComplaintId, listofBookingComplaints, objMaster.PrimaryKeyValue.ToLong(), objMaster.Current.BookingNo, objMaster.Current.CustomerName, objMaster.Current.CustomerMobileNo.ToStr().Length > 0 ? objMaster.Current.CustomerMobileNo.ToStr() : objMaster.Current.CustomerPhoneNo.ToStr(), objMaster.Current.CustomerId != null ? objMaster.Current.Customer.DefaultIfEmpty().Address1.ToStr() : string.Empty, true);
                frmComp.StartPosition = FormStartPosition.CenterScreen;
                frmComp.FormBorderStyle = FormBorderStyle.FixedSingle;

                frmComp.ShowDialog();

                frmComp.Dispose();
                frmComp = null;
                GC.Collect();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnLostProperty_Click(object sender, EventArgs e)
        {
            try
            {

                if (objMaster.PrimaryKeyValue == null)
                    return;


                frmLostProperty frmComp = new frmLostProperty(objMaster.PrimaryKeyValue.ToLong(), objMaster.Current);
                frmComp.StartPosition = FormStartPosition.CenterScreen;
                frmComp.FormBorderStyle = FormBorderStyle.FixedSingle;

                frmComp.ShowDialog();

                frmComp.Dispose();
                GC.Collect();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnReturnTo_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetReturnTo(args.ToggleState);
        }



        private void SetReturnTo(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                btnReturnTo.Text = "Airport";



                ddlReturnTo.Visible = true;
                txtReturnTo.Text = string.Empty;
                txtReturnTo.Visible = false;

                if (ddlReturnTo.DataSource == null)
                {
                    ComboFunctions.FillLocationsCombo(ddlReturnTo, c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT);
                    ddlReturnTo.SelectedIndex = -1;
                }
            }
            else
            {
                btnReturnTo.Text = "Address";
                ddlReturnTo.Visible = false;
                ddlReturnTo.SelectedValue = null;
                txtReturnTo.Visible = true;

            }


        }

        private void SetReturnFrom(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                btnReturnFrom.Text = "Airport";

                if (ddlReturnFromAirport != null)
                {
                    ddlReturnFromAirport.Visible = true;

                    if (ddlReturnFromAirport.DataSource == null)
                    {
                        ComboFunctions.FillLocationsCombo(ddlReturnFromAirport, c => c.LocationTypeId == Enums.LOCATION_TYPES.AIRPORT);
                        ddlReturnFromAirport.SelectedIndex = -1;
                    }

                }
                txtReturnFrom.Text = string.Empty;
                txtReturnFrom.Visible = false;
            }
            else
            {
                btnReturnFrom.Text = "Address";
                if (ddlReturnFromAirport != null)
                {
                    ddlReturnFromAirport.Visible = false;
                    ddlReturnFromAirport.SelectedValue = null;
                }
                txtReturnFrom.Visible = true;

            }

        }

        private void btnReturnFrom_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            SetReturnFrom(args.ToggleState);
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


                res = res.Where(c => AppVars.zonesList.Count(a => c.Contains(a)) > 0).ToArray<string>();

                ShowAddresses();

            }
            catch
            {


            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveAndReady_Click(object sender, EventArgs e)
        {
            try
            {
                if (objMaster.PrimaryKeyValue != null)
                {

                    objMaster.Edit();
                }


                if (objMaster.Current.MasterJobId != null)
                {

                    if (numReturnFare != null)
                    {
                        objMaster.Current.Booking1.ReturnFareRate = numReturnFare.Value.ToDecimal();

                        if (numCompanyFares != null)
                            objMaster.Current.Booking1.WaitingMins = numCompanyFares.Value.ToDecimal();
                    }
                }


                objMaster.Current.FareRate = numFareRate.Value.ToDecimal();

                if (numCompanyFares != null)
                    objMaster.Current.CompanyPrice = numCompanyFares.Value.ToDecimal();


                objMaster.Current.CustomerPrice = numCustomerFares.Value.ToDecimal();


                objMaster.Current.CongtionCharges = numCongChrgs.Value.ToDecimal();
                objMaster.Current.MeetAndGreetCharges = numMeetCharges.Value.ToDecimal();

                objMaster.Current.ParkingCharges = numParkingChrgs.Value.ToDecimal();
                objMaster.Current.WaitingCharges = numWaitingChrgs.Value.ToDecimal();
                objMaster.Current.ExtraDropCharges = numExtraChrgs.Value.ToDecimal();


                if (numEscortPrice != null)
                    objMaster.Current.EscortPrice = numEscortPrice.Value;

                objMaster.Current.IsProcessed = true;

                objMaster.CheckCustomerValidation = false;
                objMaster.CheckDataValidation = false;

                objMaster.DisableUpdateReturnJob = true;


                // other details

                objMaster.Current.VehicleTypeId = ddlVehicleType.SelectedValue.ToIntorNull();
                objMaster.Current.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();
                objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();

                if (ddlDepartment != null)
                    objMaster.Current.DepartmentId = ddlDepartment.SelectedValue.ToLongorNull();

                objMaster.Current.IsCompanyWise = chkIsCompanyRates.Checked;


                if (pnlOrderNo != null)
                {

                    if (txtOrderNo != null)
                        objMaster.Current.OrderNo = txtOrderNo.Text.ToStr().Trim();

                    if (txtPupilNo != null)
                        objMaster.Current.PupilNo = txtPupilNo.Text.ToStr().Trim();
                }

                // objMaster.Current.IsQuotation = chkQuotation.Checked;



                objMaster.Current.PickupDateTime = string.Format("{0:dd/MM/yyyy HH:mm}", dtpPickupDate.Value.ToDate() + dtpPickupTime.Value.Value.TimeOfDay).ToDateTime();

                if (dtpReturnPickupDate != null)
                {
                    if (dtpReturnPickupDate.Value != null && dtpReturnPickupTime.Value != null)
                    {
                        objMaster.Current.ReturnPickupDateTime = dtpReturnPickupDate.Value.ToDateorNull() + dtpReturnPickupTime.Value.Value.TimeOfDay;
                    }
                    else
                        objMaster.Current.ReturnPickupDateTime = null;


                    if (txtReturnSpecialReq != null)
                    {
                        objMaster.ReturnSpecialRequirement = txtReturnSpecialReq.Text.Trim();
                    }


                    if (ddlReturnVehicleType != null)
                    {
                        objMaster.ReturnVehicleTypeId = ddlReturnVehicleType.SelectedValue.ToIntorNull();
                    }
                }





                objMaster.Current.NoofPassengers = num_TotalPassengers.Value.ToInt();
                objMaster.Current.NoofLuggages = numTotalLuggages.Value.ToInt();


                objMaster.Current.CustomerName = ddlCustomerName.Text.ToStr().Trim();
                objMaster.Current.CustomerPhoneNo = txtCustomerPhoneNo.Text.Trim();
                objMaster.Current.CustomerMobileNo = txtCustomerMobileNo.Text.Trim();
                objMaster.Current.CustomerEmail = txtEmail.Text.Trim();

                objMaster.Current.SpecialRequirements = txtSpecialRequirements.Text.Trim();


                int FromLocTypeId = ddlFromLocType.SelectedValue.ToInt();
                int ToLocTypeId = ddlToLocType.SelectedValue.ToInt();

                objMaster.Current.FromLocTypeId = FromLocTypeId;
                objMaster.Current.ToLocTypeId = ToLocTypeId;
                objMaster.Current.FromLocId = ddlFromLocation.SelectedValue.ToIntorNull();
                objMaster.Current.ToLocId = ddlToLocation.SelectedValue.ToIntorNull();



                if (FromLocTypeId == Enums.LOCATION_TYPES.ADDRESS || FromLocTypeId == Enums.LOCATION_TYPES.BASE)
                    objMaster.Current.FromAddress = txtFromAddress.Text.Trim();

                else if (FromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    objMaster.Current.FromAddress = txtFromPostCode.Text.Trim();
                else
                {
                    objMaster.Current.FromAddress = ddlFromLocation.Text.Trim();
                }



                objMaster.Current.FromDoorNo = txtFromFlightDoorNo.Text.Trim();
                objMaster.Current.FromStreet = txtFromStreetComing.Text.Trim();
                objMaster.Current.FromPostCode = txtFromPostCode.Text.Trim();



                if (ToLocTypeId == Enums.LOCATION_TYPES.ADDRESS || ToLocTypeId == Enums.LOCATION_TYPES.BASE)
                    objMaster.Current.ToAddress = txtToAddress.Text.StripNewLine().Trim();

                else if (ToLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                    objMaster.Current.ToAddress = txtToPostCode.Text.Trim();
                else
                {
                    objMaster.Current.ToAddress = ddlToLocation.Text.Trim();
                }




                objMaster.Current.ToDoorNo = txtToFlightDoorNo.Text.Trim();
                objMaster.Current.ToStreet = txtToStreetComing.Text.Trim();
                objMaster.Current.ToPostCode = txtToPostCode.Text.Trim();


                objMaster.Current.IsCommissionWise = chkIsCommissionWise.Checked;
                objMaster.Current.DriverCommission = numDriverCommission.Value.ToDecimal();
                objMaster.Current.DriverCommissionType = ddlCommissionType.SelectedValue.ToStr().Trim();

                objMaster.Current.BookedBy = txtAccountBookedBy != null ? txtAccountBookedBy.Text.Trim() : "";



                if (grdVia != null)
                {

                    if (grdVia.Rows.Count(c => c.Cells["IsUpdated"].Value.ToInt() == 1) > 0)
                    {
                        objMaster.Current.Booking_ViaLocations.Clear();

                        grdVia.Rows.ToList().ForEach(c => c.Cells["ID"].Value = 0);

                    }
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
                //

                objMaster.Save();


                this.IsSave = true;
                Close();
            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                    ENUtils.ShowMessage(ex.Message);

               
            }
        }

        private void btnPlayRecording_Click(object sender, EventArgs e)
        {
            
            if (objMaster.PrimaryKeyValue != null )
            {
                if (objMaster.Current.CallRefNo.ToStr().Trim().Length > 0)
                {

                    try
                    {

                        XmlDocument d = new XmlDocument();
                        d.Load(Application.StartupPath + "\\Configuration.xml");
                        string callRefNo = objMaster.Current.CallRefNo.ToStr().Trim();
                        DateTime? bookingDate = objMaster.Current.BookingDate;

                        string month = bookingDate.Value.Month < 10 ? "0" + bookingDate.Value.Month.ToStr() : bookingDate.Value.Month.ToStr();
                        string day = bookingDate.Value.Day < 10 ? "0" + bookingDate.Value.Day.ToStr() : bookingDate.Value.Day.ToStr();
                        string dir = d.GetElementsByTagName("RECORDINGDRIVE")[0].InnerText + "\\" + bookingDate.Value.Year.ToStr() + "\\" + month + "\\" + day;
                        if (Directory.Exists(dir))
                        {
                            callRefNo += ".wav";
                            string file = Directory.GetFiles(dir).Where(c => c.EndsWith(callRefNo)).FirstOrDefault();

                            if (file.ToStr().Length > 0)
                            {

                                Process.Start(file);
                            }
                            else
                            {
                                ENUtils.ShowMessage("Recording File not Found '" + callRefNo + "' in the specified Directory " + dir);
                            }
                        }
                        else
                        {

                            ENUtils.ShowMessage("Recording File Directory not Found : " + dir);

                        }

                        GC.Collect();
                    }
                    catch (Exception ex)
                    {

                        ENUtils.ShowMessage(ex.Message);

                    }
                }
                else if (pathwav.ToStr().Length > 0)
                {
                    if (File.Exists(pathwav))
                    {

                        Process.Start(pathwav);
                    }
                    else
                    {
                        MessageBox.Show("Recording File not found : " + pathwav);

                    }
                }
            }
        }



        public void SelectMileageFromRouteSugg(decimal fares, string milesString)
        {

            this.selectedFaresR = fares;
            this.selectedMilesR = milesString;
            UpdateUI();

        }



        private decimal selectedFaresR;
        private string selectedMilesR;

        private void UpdateUI()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new UIDelegate(UpdateUI), null);

            }
            else
            {

                numFareRate.Value = this.selectedFaresR;
                lblMap.Text = this.selectedMilesR.ToStr();
            }

        }

        private void btnSetFares_Click(object sender, EventArgs e)
        {
            try
            {

                btnSetFares.Enabled = false;

                System.Threading.Thread.Sleep(1000);

                int? fromLocTypeId = ddlFromLocType.SelectedValue.ToInt();


                int? toLocTypeId = ddlToLocType.SelectedValue.ToInt();
                int? toLocationId = ddlToLocation.SelectedValue.ToIntorNull();

                string fromLocName = ddlFromLocation.SelectedItem != null ? (ddlFromLocation.SelectedItem as RadComboBoxItem).Text.Trim().ToUpper() : ddlFromLocation.Text.Trim().ToUpper();
                string toLocName = ddlToLocation.SelectedItem != null ? (ddlToLocation.SelectedItem as RadComboBoxItem).Text.Trim().ToUpper() : ddlToLocation.Text.Trim().ToUpper();

                string fromAddress = txtFromAddress.Text.Trim().ToUpper();
                string toAddress = txtToAddress.Text.Trim().ToUpper();
                string fromPostCode = txtFromPostCode.Text.Trim().ToUpper();
                string toPostCode = txtToPostCode.Text.Trim().ToUpper();




                if (fromLocTypeId == Enums.LOCATION_TYPES.BASE || fromLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                {
                    fromAddress = txtFromAddress.Text.Trim().ToUpper();

                }
                else if (fromLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {

                    fromAddress = txtFromPostCode.Text.Trim().ToUpper();
                }
                else
                {
                    fromAddress = fromLocName;

                }


                if (toLocTypeId == Enums.LOCATION_TYPES.BASE || toLocTypeId == Enums.LOCATION_TYPES.ADDRESS)
                {
                    toAddress = txtToAddress.Text.Trim().ToUpper();

                }
                else if (toLocTypeId == Enums.LOCATION_TYPES.POSTCODE)
                {

                    toAddress = txtToPostCode.Text.Trim().ToUpper();
                }
                else
                {
                    toAddress = toLocName;

                }


                string via = string.Empty;


                if (grdVia != null)
                {
                    via = string.Join(",", grdVia.Rows.Select(c => c.Cells["VIALOCATIONVALUE"].Value.ToStr().ToUpper()).ToArray<string>());

                }

                bool IsAddMode = false;
                int? companyId = ddlCompany.SelectedValue.ToIntorNull();

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    Fare_CustomJourney obj = db.Fare_CustomJourneys.FirstOrDefault(c => c.Pickup == fromAddress && c.Destination == toAddress && (via == string.Empty || c.ViaPoints == via)
                    && (companyId == null || c.CompanyId == companyId));

                    if (obj == null)
                    {
                        obj = new Fare_CustomJourney();

                        IsAddMode = true;
                    }


                    obj.DriverFares = numFareRate.Value.ToDecimal();
                    obj.DriverRtnFares = numReturnFare != null ? numReturnFare.Value.ToDecimal() : 0.00m;
                    obj.CustomerFares = numCustomerFares.Value.ToDecimal();
                    obj.CustomerRtnFares = numReturnCustFare != null ? numReturnCustFare.Value.ToDecimal() : 0.00m;

                    obj.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                    obj.ViaPoints = via;

                    obj.CompanyFares = numCompanyFares != null ? numCompanyFares.Value.ToDecimal() : 0.00m;
                    obj.CompanyRtnFares = numReturnCompanyFares != null ? numReturnCompanyFares.Value.ToDecimal() : 0.00m;


                    obj.Pickup = fromAddress;
                    obj.Destination = toAddress;


                    if (IsAddMode)
                    {

                        db.Fare_CustomJourneys.InsertOnSubmit(obj);

                    }
                    db.SubmitChanges();


                }


                btnSetFares.Enabled = true;
            }
            catch (Exception ex)
            {

                btnSetFares.Enabled = true;

            }
        }

        private void btnHotels_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            ShowHotels(args.ToggleState);
        }

        private void ShowHotels(ToggleState toggle)
        {

            InitializeHotelPanel();


            HideAirports();

            HideStations();
            HideHospitals();
            HideTowns();

            pnlHotels.Visible = toggle == ToggleState.On;

            if (toggle == ToggleState.On)
            {
                if (LastFocus == 1)
                {
                    ddlHotelsType.Text = "From";
                }

                if (LastFocus == 2)
                {

                    ddlHotelsType.Text = "To";
                }


                if (ddlHotels.DataSource == null)
                {
                    FillKeyLocations(ddlHotels, General.GetHotelsList());

                }

                ddlHotels.Focus();
            }

            ddlHotels.CloseDropDown();
        }


        private void InitializeHotelPanel()
        {
            if (pnlHotels != null) return;

            this.pnlHotels = new System.Windows.Forms.Panel();
            this.ddlHotels = new UI.MyDropDownList();
            this.btnPickHotels = new Telerik.WinControls.UI.RadButton();
            this.labelHotel = new System.Windows.Forms.Label();
            this.ddlHotelsType = new UI.MyDropDownList();

            this.pnlHotels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlHotels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickHotels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlHotelsType)).BeginInit();




            // 
            // pnlHotels
            // 
            this.pnlHotels.BackColor = System.Drawing.Color.Transparent;
            this.pnlHotels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHotels.Controls.Add(this.ddlHotels);
            this.pnlHotels.Controls.Add(this.btnPickHotels);
            this.pnlHotels.Controls.Add(this.labelHotel);
            this.pnlHotels.Controls.Add(this.ddlHotelsType);
            this.pnlHotels.Location = new System.Drawing.Point(321, 37);
            this.pnlHotels.Name = "pnlHotels";
            this.pnlHotels.Size = new System.Drawing.Size(358, 133);
            this.pnlHotels.TabIndex = 204;
            this.pnlHotels.Visible = false;
            // 
            // ddlHotels
            // 
            this.ddlHotels.Caption = null;
            this.ddlHotels.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlHotels.Location = new System.Drawing.Point(12, 60);
            this.ddlHotels.Name = "ddlHotels";
            this.ddlHotels.Property = null;
            this.ddlHotels.ShowDownArrow = true;
            this.ddlHotels.Size = new System.Drawing.Size(302, 22);
            this.ddlHotels.TabIndex = 2;
            this.ddlHotels.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ddlHotels_KeyDown);
            // 
            // btnPickHotels
            // 
            this.btnPickHotels.Location = new System.Drawing.Point(229, 99);
            this.btnPickHotels.Name = "btnPickHotels";
            this.btnPickHotels.Size = new System.Drawing.Size(122, 21);
            this.btnPickHotels.TabIndex = 3;
            this.btnPickHotels.Text = "Pick";
            this.btnPickHotels.Click += new System.EventHandler(this.btn_pickHotels_Click);
            // 
            // labelHotel
            // 
            this.labelHotel.BackColor = System.Drawing.Color.MintCream;
            this.labelHotel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHotel.ForeColor = System.Drawing.Color.Navy;
            this.labelHotel.Location = new System.Drawing.Point(2, 36);
            this.labelHotel.Name = "labelHotel";
            this.labelHotel.Size = new System.Drawing.Size(353, 18);
            this.labelHotel.TabIndex = 15;
            this.labelHotel.Text = "Hotels";
            // 
            // ddlHotelsType
            // 
            this.ddlHotelsType.Caption = null;
            this.ddlHotelsType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();

            radListDataItem1.Selected = true;
            radListDataItem1.Text = "From";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "To";
            radListDataItem2.TextWrap = true;
            this.ddlHotelsType.Items.Add(radListDataItem1);
            this.ddlHotelsType.Items.Add(radListDataItem2);
            this.ddlHotelsType.Location = new System.Drawing.Point(12, 7);
            this.ddlHotelsType.Name = "ddlHotelsType";
            this.ddlHotelsType.Property = null;
            this.ddlHotelsType.ShowDownArrow = true;
            this.ddlHotelsType.Size = new System.Drawing.Size(104, 22);
            this.ddlHotelsType.TabIndex = 1;
            this.ddlHotelsType.Text = "From";


            this.pnlHotels.ResumeLayout(false);
            this.pnlHotels.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlHotels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickHotels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlHotelsType)).EndInit();


            this.pnlMain.Controls.Add(this.pnlHotels);

            pnlHotels.BringToFront();
        }


        private void ddlHotels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PickHotel();

            }
        }

        private void PickHotel()
        {
            string type = ddlHotelsType.Text.ToLower().Trim();
            if (type == "from")
            {
                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.HOTELS;
                ddlFromLocation.SelectedValue = ddlHotels.SelectedValue;
                txtToAddress.Focus();
            }
            else if (type == "to")
            {
                ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.HOTELS;
                ddlToLocation.SelectedValue = ddlHotels.SelectedValue;
                ddlCustomerName.Focus();
            }

            HideHotels();


            UpdateAutoCalculateFares();

        }

        void btn_pickHotels_Click(object sender, EventArgs e)
        {
            PickHotel();
        }



        #region TOWNS

        private void btntowns_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            Showtowns(args.ToggleState);
        }

        private void Showtowns(ToggleState toggle)
        {

            InitializetownsPanel();


            HideAirports();
            HideStations();
            HideHospitals();
            HideHotels();


            pnltowns.Visible = toggle == ToggleState.On;

            if (toggle == ToggleState.On)
            {
                if (LastFocus == 1)
                {
                    ddltownsType.Text = "From";
                }

                if (LastFocus == 2)
                {

                    ddltownsType.Text = "To";
                }


                if (ddltowns.DataSource == null)
                {
                    FillKeyLocations(ddltowns, General.GetTownsList());

                }

                ddltowns.Focus();
            }

            ddltowns.CloseDropDown();
        }


        private void InitializetownsPanel()
        {
            if (pnltowns != null) return;

            this.pnltowns = new System.Windows.Forms.Panel();
            this.ddltowns = new UI.MyDropDownList();
            this.btnPicktowns = new Telerik.WinControls.UI.RadButton();
            this.labeltowns = new System.Windows.Forms.Label();
            this.ddltownsType = new UI.MyDropDownList();

            this.pnltowns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddltowns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPicktowns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddltownsType)).BeginInit();


            // 
            // pnltowns
            // 
            this.pnltowns.BackColor = System.Drawing.Color.Transparent;
            this.pnltowns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnltowns.Controls.Add(this.ddltowns);
            this.pnltowns.Controls.Add(this.btnPicktowns);
            this.pnltowns.Controls.Add(this.labeltowns);
            this.pnltowns.Controls.Add(this.ddltownsType);
            this.pnltowns.Location = new System.Drawing.Point(321, 37);
            this.pnltowns.Name = "pnltowns";
            this.pnltowns.Size = new System.Drawing.Size(358, 133);
            this.pnltowns.TabIndex = 204;
            this.pnltowns.Visible = false;
            // 
            // ddltowns
            // 
            this.ddltowns.Caption = null;
            this.ddltowns.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddltowns.Location = new System.Drawing.Point(12, 60);
            this.ddltowns.Name = "ddltowns";
            this.ddltowns.Property = null;
            this.ddltowns.ShowDownArrow = true;
            this.ddltowns.Size = new System.Drawing.Size(302, 22);
            this.ddltowns.TabIndex = 2;
            this.ddltowns.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ddltowns_KeyDown);
            // 
            // btnPicktowns
            // 
            this.btnPicktowns.Location = new System.Drawing.Point(229, 99);
            this.btnPicktowns.Name = "btnPicktowns";
            this.btnPicktowns.Size = new System.Drawing.Size(122, 21);
            this.btnPicktowns.TabIndex = 3;
            this.btnPicktowns.Text = "Pick";
            this.btnPicktowns.Click += new System.EventHandler(this.btn_picktowns_Click);
            // 
            // labeltowns
            // 
            this.labeltowns.BackColor = System.Drawing.Color.MintCream;
            this.labeltowns.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labeltowns.ForeColor = System.Drawing.Color.Navy;
            this.labeltowns.Location = new System.Drawing.Point(2, 36);
            this.labeltowns.Name = "labeltowns";
            this.labeltowns.Size = new System.Drawing.Size(353, 18);
            this.labeltowns.TabIndex = 15;
            this.labeltowns.Text = "towns";
            // 
            // ddltownsType
            // 
            this.ddltownsType.Caption = null;
            this.ddltownsType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();

            radListDataItem1.Selected = true;
            radListDataItem1.Text = "From";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "To";
            radListDataItem2.TextWrap = true;
            this.ddltownsType.Items.Add(radListDataItem1);
            this.ddltownsType.Items.Add(radListDataItem2);
            this.ddltownsType.Location = new System.Drawing.Point(12, 7);
            this.ddltownsType.Name = "ddltownsType";
            this.ddltownsType.Property = null;
            this.ddltownsType.ShowDownArrow = true;
            this.ddltownsType.Size = new System.Drawing.Size(104, 22);
            this.ddltownsType.TabIndex = 1;
            this.ddltownsType.Text = "From";


            this.pnltowns.ResumeLayout(false);
            this.pnltowns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddltowns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPicktowns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddltownsType)).EndInit();


            this.pnlMain.Controls.Add(this.pnltowns);

            pnltowns.BringToFront();
        }

        private void HideTowns()
        {
            InitializetownsPanel();

            pnltowns.Visible = false;
            ddltowns.SelectedValue = null;
            ddltowns.CloseDropDown();
            btnTowns.ToggleStateChanged -= new StateChangedEventHandler(btntowns_ToggleStateChanged);
            btnTowns.ToggleState = ToggleState.Off;
            btnTowns.ToggleStateChanged += new StateChangedEventHandler(btntowns_ToggleStateChanged);
        }



        private void ddltowns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Picktowns();

            }
        }

        private void Picktowns()
        {
            string type = ddltownsType.Text.ToLower().Trim();
            if (type == "from")
            {
                ddlFromLocType.SelectedValue = Enums.LOCATION_TYPES.TOWN;
                ddlFromLocation.SelectedValue = ddltowns.SelectedValue;
                txtToAddress.Focus();
            }
            else if (type == "to")
            {
                ddlToLocType.SelectedValue = Enums.LOCATION_TYPES.TOWN;
                ddlToLocation.SelectedValue = ddltowns.SelectedValue;
                ddlCustomerName.Focus();
            }

            HideTowns();


            UpdateAutoCalculateFares();

        }

        void btn_picktowns_Click(object sender, EventArgs e)
        {
            Picktowns();
        }


        #endregion

        private void chkGenerateToken_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (args.NewValue == ToggleState.On)
            {
                if (txtTokenNo.Tag == null)
                {

                    txtTokenNo.Text = new Taxi_BLL.SysPolicy_AutoGeneratedCodesBO().GetSequenceNumber("frmBooking", ddlSubCompany.SelectedValue.ToIntorNull());
                    txtTokenNo.Tag = txtTokenNo.Text;

                }

                 txtTokenNo.Visible = true;
               
            }
            else
            {
                txtTokenNo.Visible = false;

            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {

       
        }



    }
}

