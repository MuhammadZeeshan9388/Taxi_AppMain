using System;
using Telerik.WinControls.UI;
namespace Taxi_AppMain
{
    partial class frmBooking_old
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);


        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new Telerik.WinControls.UI.RadPanel();
            this.btnAttributes = new Telerik.WinControls.UI.RadButton();
            this.pnljourney = new System.Windows.Forms.Panel();
            this.opt_one = new System.Windows.Forms.RadioButton();
            this.opt_waitreturn = new System.Windows.Forms.RadioButton();
            this.opt_return = new System.Windows.Forms.RadioButton();
            this.chkPermanentCustNotes = new Telerik.WinControls.UI.RadCheckBox();
            this.txtReturnFrom = new UI.AutoCompleteTextBox();
            this.txtReturnTo = new UI.AutoCompleteTextBox();
            this.btnPickAccountBooking = new Telerik.WinControls.UI.RadButton();
            this.ddlDropOffPlot = new System.Windows.Forms.ComboBox();
            this.ddlPickupPlot = new System.Windows.Forms.ComboBox();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.chkQuotedPrice = new Telerik.WinControls.UI.RadCheckBox();
            this.btnSetFares = new Telerik.WinControls.UI.RadButton();
            this.txtPaymentReference = new Telerik.WinControls.UI.RadTextBox();
            this.lblPaymentRef = new Telerik.WinControls.UI.RadLabel();
            this.txtFaresPostedFrom = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.numFareRate = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnDetailMap = new Telerik.WinControls.UI.RadButton();
            this.pnlOtherCharges = new System.Windows.Forms.Panel();
            this.lblTip = new Telerik.WinControls.UI.RadLabel();
            this.numTipAmount = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.numDrvWaitingMins = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel18 = new Telerik.WinControls.UI.RadLabel();
            this.numTotalChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel20 = new Telerik.WinControls.UI.RadLabel();
            this.numCongChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel16 = new Telerik.WinControls.UI.RadLabel();
            this.numMeetCharges = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel17 = new Telerik.WinControls.UI.RadLabel();
            this.numExtraChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblAccWaitingCharges = new Telerik.WinControls.UI.RadLabel();
            this.numWaitingChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblAccParkingCharges = new Telerik.WinControls.UI.RadLabel();
            this.numParkingChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.pnlPaymentMode = new Telerik.WinControls.UI.RadLabel();
            this.chkSecondaryPaymentType = new Telerik.WinControls.UI.RadCheckBox();
            this.numCashPaymentFares = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnPayment = new Telerik.WinControls.UI.RadButton();
            this.ddlCommissionType = new UI.MyDropDownList();
            this.numDriverCommission = new Telerik.WinControls.UI.RadSpinEditor();
            this.chkIsCommissionWise = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
            this.ddlPaymentType = new UI.MyDropDownList();
            this.btnPickFares = new Telerik.WinControls.UI.RadButton();
            this.lblMap = new System.Windows.Forms.Label();
            this.lblPaymentHeading = new Telerik.WinControls.UI.RadLabel();
            this.pnlFares = new System.Windows.Forms.Label();
            this.pnlBookingFees = new Telerik.WinControls.UI.RadLabel();
            this.numReturnBookingFee = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblReturnCustFare = new Telerik.WinControls.UI.RadLabel();
            this.numBookingFee = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblDropOffPlot = new System.Windows.Forms.Label();
            this.lblPickupPlot = new System.Windows.Forms.Label();
            this.txtVehicleNo = new Telerik.WinControls.UI.RadLabel();
            this.pnlAutoDespatch = new System.Windows.Forms.Panel();
            this.btnSms = new System.Windows.Forms.Button();
            this.optSMSThirdParty = new Telerik.WinControls.UI.RadRadioButton();
            this.optSMSGsm = new Telerik.WinControls.UI.RadRadioButton();
            this.chkBidding = new Telerik.WinControls.UI.RadCheckBox();
            this.chkDisablePassengerSMS = new Telerik.WinControls.UI.RadCheckBox();
            this.chkDisableDriverSMS = new Telerik.WinControls.UI.RadCheckBox();
            this.chkAutoDespatch = new Telerik.WinControls.UI.RadCheckBox();
            this.numBeforeMinutes = new Telerik.WinControls.UI.RadSpinEditor();
            this.pnlCustomer = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtEmail = new Telerik.WinControls.UI.RadTextBox();
            this.btnCustomerLister = new Telerik.WinControls.UI.RadButton();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel21 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel19 = new Telerik.WinControls.UI.RadLabel();
            this.txtCustomerPhoneNo = new Telerik.WinControls.UI.RadTextBox();
            this.txtCustomerMobileNo = new Telerik.WinControls.UI.RadTextBox();
            this.ddlCustomerName = new Telerik.WinControls.UI.RadTextBox();
            this.btn_notes = new Telerik.WinControls.UI.RadButton();
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.btnPrintJob = new Telerik.WinControls.UI.RadSplitButton();
            this.btnJobReport = new Telerik.WinControls.UI.RadMenuItem();
            this.btnLogDetail = new Telerik.WinControls.UI.RadMenuItem();
            this.btnCancelBooking = new Telerik.WinControls.UI.RadButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.radLabel27 = new Telerik.WinControls.UI.RadLabel();
            this.btnSaveNew = new Telerik.WinControls.UI.RadButton();
            this.btnConfirmBooking = new Telerik.WinControls.UI.RadButton();
            this.numTotalLuggages = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblLuggages = new Telerik.WinControls.UI.RadLabel();
            this.lblPassengers = new Telerik.WinControls.UI.RadLabel();
            this.num_TotalPassengers = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnMultiVehicle = new Telerik.WinControls.UI.RadButton();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.ddlVehicleType = new UI.MyDropDownList();
            this.chkIsCompanyRates = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel14 = new Telerik.WinControls.UI.RadLabel();
            this.ddlCompany = new Telerik.WinControls.UI.RadComboBox();
            this.dtpPickupDate = new UI.MyDatePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtpPickupTime = new UI.MyDatePicker();
            this.lblPickupTime = new Telerik.WinControls.UI.RadLabel();
            this.ddlDriver = new UI.MyDropDownList();
            this.radLabel25 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel22 = new Telerik.WinControls.UI.RadLabel();
            this.txtSpecialRequirements = new Telerik.WinControls.UI.RadTextBox();
            this.pnlReturnJobNo = new System.Windows.Forms.Label();
            this.chkQuotation = new Telerik.WinControls.UI.RadCheckBox();
            this.chkReverse = new Telerik.WinControls.UI.RadCheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDespatchView = new System.Windows.Forms.Button();
            this.btnAccountCode = new Telerik.WinControls.UI.RadButton();
            this.btnViewMapReport = new Telerik.WinControls.UI.RadButton();
            this.btnNearestDrv = new System.Windows.Forms.Button();
            this.btnSendEmail = new Telerik.WinControls.UI.RadButton();
            this.btnBase = new Telerik.WinControls.UI.RadToggleButton();
            this.btnSearch = new Telerik.WinControls.UI.RadButton();
            this.btnMultiBooking = new Telerik.WinControls.UI.RadButton();
            this.txtFromAddress = new UI.AutoCompleteTextBox();
            this.txtFromPostCode = new Telerik.WinControls.UI.RadTextBox();
            this.txtFromStreetComing = new Telerik.WinControls.UI.RadTextBox();
            this.txtFromFlightDoorNo = new Telerik.WinControls.UI.RadTextBox();
            this.lblFromStreetComing = new Telerik.WinControls.UI.RadLabel();
            this.lblFromDoorFlightNo = new Telerik.WinControls.UI.RadLabel();
            this.txtToAddress = new UI.AutoCompleteTextBox();
            this.lblFromLoc = new Telerik.WinControls.UI.RadLabel();
            this.ddlFromLocType = new Telerik.WinControls.UI.RadComboBox();
            this.ddlFromLocation = new Telerik.WinControls.UI.RadComboBox();
            this.ddlToLocType = new Telerik.WinControls.UI.RadComboBox();
            this.lblToLoc = new Telerik.WinControls.UI.RadLabel();
            this.txtToStreetComing = new Telerik.WinControls.UI.RadTextBox();
            this.ddlToLocation = new Telerik.WinControls.UI.RadComboBox();
            this.txtToPostCode = new Telerik.WinControls.UI.RadTextBox();
            this.lblToStreetComing = new Telerik.WinControls.UI.RadLabel();
            this.txtToFlightDoorNo = new Telerik.WinControls.UI.RadTextBox();
            this.lblToDoorFlightNo = new Telerik.WinControls.UI.RadLabel();
            this.btnSelectVia = new Telerik.WinControls.UI.RadToggleButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBookingNo = new Telerik.WinControls.UI.RadLabel();
            this.ddlBookingType = new Telerik.WinControls.UI.RadDropDownList();
            this.lblBookingType = new Telerik.WinControls.UI.RadLabel();
            this.lblBookedBy = new System.Windows.Forms.Label();
            this.ddlSubCompany = new System.Windows.Forms.ComboBox();
            this.btnTrackDriver = new Telerik.WinControls.UI.RadButton();
            this.btnPasteBooking = new Telerik.WinControls.UI.RadButton();
            this.btnExit = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAttributes)).BeginInit();
            this.pnljourney.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermanentCustNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickAccountBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkQuotedPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentReference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPaymentRef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFaresPostedFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDetailMap)).BeginInit();
            this.pnlOtherCharges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTipAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDrvWaitingMins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCongChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeetCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccWaitingCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitingChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccParkingCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParkingChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPaymentMode)).BeginInit();
            this.pnlPaymentMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSecondaryPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCashPaymentFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCommissionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDriverCommission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommissionWise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPaymentHeading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBookingFees)).BeginInit();
            this.pnlBookingFees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnBookingFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnCustFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBookingFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVehicleNo)).BeginInit();
            this.pnlAutoDespatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optSMSThirdParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optSMSGsm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBidding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisablePassengerSMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisableDriverSMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoDespatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeforeMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomer)).BeginInit();
            this.pnlCustomer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomerLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerPhoneNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerMobileNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_notes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirmBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalLuggages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLuggages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPassengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMultiVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicleType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCompanyRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickupDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialRequirements)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkQuotation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReverse)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewMapReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMultiBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromStreetComing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromFlightDoorNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromStreetComing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromDoorFlightNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromLoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromLocType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToLocType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToLoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToStreetComing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToStreetComing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToFlightDoorNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDoorFlightNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectVia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlBookingType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBookingType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTrackDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPasteBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.pnlMain.Controls.Add(this.btnAttributes);
            this.pnlMain.Controls.Add(this.pnljourney);
            this.pnlMain.Controls.Add(this.chkPermanentCustNotes);
            this.pnlMain.Controls.Add(this.txtReturnFrom);
            this.pnlMain.Controls.Add(this.txtReturnTo);
            this.pnlMain.Controls.Add(this.btnPickAccountBooking);
            this.pnlMain.Controls.Add(this.ddlDropOffPlot);
            this.pnlMain.Controls.Add(this.ddlPickupPlot);
            this.pnlMain.Controls.Add(this.radPanel1);
            this.pnlMain.Controls.Add(this.lblDropOffPlot);
            this.pnlMain.Controls.Add(this.lblPickupPlot);
            this.pnlMain.Controls.Add(this.txtVehicleNo);
            this.pnlMain.Controls.Add(this.pnlAutoDespatch);
            this.pnlMain.Controls.Add(this.pnlCustomer);
            this.pnlMain.Controls.Add(this.radPanel3);
            this.pnlMain.Controls.Add(this.numTotalLuggages);
            this.pnlMain.Controls.Add(this.lblLuggages);
            this.pnlMain.Controls.Add(this.lblPassengers);
            this.pnlMain.Controls.Add(this.num_TotalPassengers);
            this.pnlMain.Controls.Add(this.btnMultiVehicle);
            this.pnlMain.Controls.Add(this.radLabel11);
            this.pnlMain.Controls.Add(this.ddlVehicleType);
            this.pnlMain.Controls.Add(this.chkIsCompanyRates);
            this.pnlMain.Controls.Add(this.radLabel14);
            this.pnlMain.Controls.Add(this.ddlCompany);
            this.pnlMain.Controls.Add(this.dtpPickupDate);
            this.pnlMain.Controls.Add(this.radLabel3);
            this.pnlMain.Controls.Add(this.dtpPickupTime);
            this.pnlMain.Controls.Add(this.lblPickupTime);
            this.pnlMain.Controls.Add(this.ddlDriver);
            this.pnlMain.Controls.Add(this.radLabel25);
            this.pnlMain.Controls.Add(this.radLabel22);
            this.pnlMain.Controls.Add(this.txtSpecialRequirements);
            this.pnlMain.Controls.Add(this.pnlReturnJobNo);
            this.pnlMain.Controls.Add(this.chkQuotation);
            this.pnlMain.Controls.Add(this.chkReverse);
            this.pnlMain.Controls.Add(this.panel1);
            this.pnlMain.Controls.Add(this.txtFromAddress);
            this.pnlMain.Controls.Add(this.txtFromPostCode);
            this.pnlMain.Controls.Add(this.txtFromStreetComing);
            this.pnlMain.Controls.Add(this.txtFromFlightDoorNo);
            this.pnlMain.Controls.Add(this.lblFromStreetComing);
            this.pnlMain.Controls.Add(this.lblFromDoorFlightNo);
            this.pnlMain.Controls.Add(this.txtToAddress);
            this.pnlMain.Controls.Add(this.lblFromLoc);
            this.pnlMain.Controls.Add(this.ddlFromLocType);
            this.pnlMain.Controls.Add(this.ddlFromLocation);
            this.pnlMain.Controls.Add(this.ddlToLocType);
            this.pnlMain.Controls.Add(this.lblToLoc);
            this.pnlMain.Controls.Add(this.txtToStreetComing);
            this.pnlMain.Controls.Add(this.ddlToLocation);
            this.pnlMain.Controls.Add(this.txtToPostCode);
            this.pnlMain.Controls.Add(this.lblToStreetComing);
            this.pnlMain.Controls.Add(this.txtToFlightDoorNo);
            this.pnlMain.Controls.Add(this.lblToDoorFlightNo);
            this.pnlMain.Controls.Add(this.btnSelectVia);
            this.pnlMain.Location = new System.Drawing.Point(5, 42);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1215, 728);
            this.pnlMain.TabIndex = 3;
            // 
            // btnAttributes
            // 
            this.btnAttributes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttributes.Location = new System.Drawing.Point(752, 168);
            this.btnAttributes.Name = "btnAttributes";
            this.btnAttributes.Size = new System.Drawing.Size(165, 45);
            this.btnAttributes.TabIndex = 502;
            this.btnAttributes.Text = "Attributes";
            this.btnAttributes.Click += new System.EventHandler(this.btnAttributes_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAttributes.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAttributes.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAttributes.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAttributes.GetChildAt(0))).Text = "Attributes";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAttributes.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAttributes.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnljourney
            // 
            this.pnljourney.Controls.Add(this.opt_one);
            this.pnljourney.Controls.Add(this.opt_waitreturn);
            this.pnljourney.Controls.Add(this.opt_return);
            this.pnljourney.Location = new System.Drawing.Point(110, 264);
            this.pnljourney.Name = "pnljourney";
            this.pnljourney.Size = new System.Drawing.Size(245, 29);
            this.pnljourney.TabIndex = 9;
            this.pnljourney.TabStop = true;
            // 
            // opt_one
            // 
            this.opt_one.AutoSize = true;
            this.opt_one.FlatAppearance.BorderSize = 2;
            this.opt_one.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.opt_one.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_one.Location = new System.Drawing.Point(13, 5);
            this.opt_one.Name = "opt_one";
            this.opt_one.Size = new System.Drawing.Size(87, 22);
            this.opt_one.TabIndex = 7;
            this.opt_one.TabStop = true;
            this.opt_one.Text = "One Way";
            this.opt_one.UseVisualStyleBackColor = true;
            this.opt_one.CheckedChanged += new System.EventHandler(this.opt_one_CheckedChanged);
            this.opt_one.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opt_one_KeyDown);
            // 
            // opt_waitreturn
            // 
            this.opt_waitreturn.AutoSize = true;
            this.opt_waitreturn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_waitreturn.Location = new System.Drawing.Point(175, 5);
            this.opt_waitreturn.Name = "opt_waitreturn";
            this.opt_waitreturn.Size = new System.Drawing.Size(55, 22);
            this.opt_waitreturn.TabIndex = 9;
            this.opt_waitreturn.TabStop = true;
            this.opt_waitreturn.Text = "W/R";
            this.opt_waitreturn.UseVisualStyleBackColor = true;
            this.opt_waitreturn.CheckedChanged += new System.EventHandler(this.opt_waitreturn_CheckedChanged);
            this.opt_waitreturn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opt_one_KeyDown);
            // 
            // opt_return
            // 
            this.opt_return.AutoSize = true;
            this.opt_return.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_return.Location = new System.Drawing.Point(103, 5);
            this.opt_return.Name = "opt_return";
            this.opt_return.Size = new System.Drawing.Size(69, 22);
            this.opt_return.TabIndex = 8;
            this.opt_return.TabStop = true;
            this.opt_return.Text = "Return";
            this.opt_return.UseVisualStyleBackColor = true;
            this.opt_return.CheckedChanged += new System.EventHandler(this.opt_return_CheckedChanged);
            this.opt_return.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opt_one_KeyDown);
            this.opt_return.Validating += new System.ComponentModel.CancelEventHandler(this.opt_return_Validating);
            // 
            // chkPermanentCustNotes
            // 
            this.chkPermanentCustNotes.BackColor = System.Drawing.Color.AliceBlue;
            this.chkPermanentCustNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkPermanentCustNotes.Location = new System.Drawing.Point(5, 508);
            this.chkPermanentCustNotes.Name = "chkPermanentCustNotes";
            this.chkPermanentCustNotes.Size = new System.Drawing.Size(89, 18);
            this.chkPermanentCustNotes.TabIndex = 0;
            this.chkPermanentCustNotes.Text = "Permanent";
            // 
            // txtReturnFrom
            // 
            this.txtReturnFrom.BackColor = System.Drawing.Color.White;
            this.txtReturnFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReturnFrom.DefaultHeight = 0;
            this.txtReturnFrom.DefaultWidth = 0;
            this.txtReturnFrom.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnFrom.ForceListBoxToUpdate = false;
            this.txtReturnFrom.FormerValue = "";
            this.txtReturnFrom.Location = new System.Drawing.Point(496, 178);
            this.txtReturnFrom.Multiline = true;
            this.txtReturnFrom.Name = "txtReturnFrom";
            // 
            // 
            // 
            this.txtReturnFrom.RootElement.StretchVertically = true;
            this.txtReturnFrom.SelectedItem = null;
            this.txtReturnFrom.Size = new System.Drawing.Size(237, 35);
            this.txtReturnFrom.TabIndex = 271;
            this.txtReturnFrom.TabStop = false;
            this.txtReturnFrom.Values = null;
            this.txtReturnFrom.Visible = false;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtReturnFrom.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtReturnFrom.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).Width = 1F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).InnerColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).InnerColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).InnerColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnFrom.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // txtReturnTo
            // 
            this.txtReturnTo.BackColor = System.Drawing.Color.White;
            this.txtReturnTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReturnTo.DefaultHeight = 0;
            this.txtReturnTo.DefaultWidth = 0;
            this.txtReturnTo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnTo.ForceListBoxToUpdate = false;
            this.txtReturnTo.FormerValue = "";
            this.txtReturnTo.Location = new System.Drawing.Point(114, 178);
            this.txtReturnTo.Multiline = true;
            this.txtReturnTo.Name = "txtReturnTo";
            // 
            // 
            // 
            this.txtReturnTo.RootElement.StretchVertically = true;
            this.txtReturnTo.SelectedItem = null;
            this.txtReturnTo.Size = new System.Drawing.Size(225, 35);
            this.txtReturnTo.TabIndex = 270;
            this.txtReturnTo.TabStop = false;
            this.txtReturnTo.Values = null;
            this.txtReturnTo.Visible = false;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtReturnTo.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).Width = 1F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).InnerColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).InnerColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).InnerColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtReturnTo.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // btnPickAccountBooking
            // 
            this.btnPickAccountBooking.Location = new System.Drawing.Point(328, 324);
            this.btnPickAccountBooking.Name = "btnPickAccountBooking";
            this.btnPickAccountBooking.Size = new System.Drawing.Size(30, 23);
            this.btnPickAccountBooking.TabIndex = 0;
            this.btnPickAccountBooking.Text = "...";
            this.btnPickAccountBooking.Click += new System.EventHandler(this.btnPickAccountBooking_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPickAccountBooking.GetChildAt(0))).Text = "...";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPickAccountBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPickAccountBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlDropOffPlot
            // 
            this.ddlDropOffPlot.BackColor = System.Drawing.Color.White;
            this.ddlDropOffPlot.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDropOffPlot.FormattingEnabled = true;
            this.ddlDropOffPlot.Location = new System.Drawing.Point(495, 182);
            this.ddlDropOffPlot.Name = "ddlDropOffPlot";
            this.ddlDropOffPlot.Size = new System.Drawing.Size(213, 24);
            this.ddlDropOffPlot.TabIndex = 265;
            // 
            // ddlPickupPlot
            // 
            this.ddlPickupPlot.BackColor = System.Drawing.Color.White;
            this.ddlPickupPlot.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPickupPlot.FormattingEnabled = true;
            this.ddlPickupPlot.Location = new System.Drawing.Point(113, 182);
            this.ddlPickupPlot.Name = "ddlPickupPlot";
            this.ddlPickupPlot.Size = new System.Drawing.Size(213, 24);
            this.ddlPickupPlot.TabIndex = 264;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.Linen;
            this.radPanel1.Controls.Add(this.chkQuotedPrice);
            this.radPanel1.Controls.Add(this.btnSetFares);
            this.radPanel1.Controls.Add(this.txtPaymentReference);
            this.radPanel1.Controls.Add(this.lblPaymentRef);
            this.radPanel1.Controls.Add(this.txtFaresPostedFrom);
            this.radPanel1.Controls.Add(this.radLabel5);
            this.radPanel1.Controls.Add(this.numFareRate);
            this.radPanel1.Controls.Add(this.btnDetailMap);
            this.radPanel1.Controls.Add(this.pnlOtherCharges);
            this.radPanel1.Controls.Add(this.pnlPaymentMode);
            this.radPanel1.Controls.Add(this.btnPickFares);
            this.radPanel1.Controls.Add(this.lblMap);
            this.radPanel1.Controls.Add(this.lblPaymentHeading);
            this.radPanel1.Controls.Add(this.pnlFares);
            this.radPanel1.Controls.Add(this.pnlBookingFees);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel1.Location = new System.Drawing.Point(0, 540);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1215, 188);
            this.radPanel1.TabIndex = 2;
            // 
            // chkQuotedPrice
            // 
            this.chkQuotedPrice.BackColor = System.Drawing.Color.Bisque;
            this.chkQuotedPrice.Font = new System.Drawing.Font("Tahoma", 8F);
            this.chkQuotedPrice.Location = new System.Drawing.Point(658, 67);
            this.chkQuotedPrice.Name = "chkQuotedPrice";
            this.chkQuotedPrice.Size = new System.Drawing.Size(55, 16);
            this.chkQuotedPrice.TabIndex = 0;
            this.chkQuotedPrice.Text = "Quoted";
            // 
            // btnSetFares
            // 
            this.btnSetFares.BackColor = System.Drawing.Color.Coral;
            this.btnSetFares.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetFares.Location = new System.Drawing.Point(863, 25);
            this.btnSetFares.Name = "btnSetFares";
            this.btnSetFares.Size = new System.Drawing.Size(54, 31);
            this.btnSetFares.TabIndex = 0;
            this.btnSetFares.Text = "Set Fare";
            this.btnSetFares.Visible = false;
            this.btnSetFares.Click += new System.EventHandler(this.btnSetFares_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSetFares.GetChildAt(0))).Text = "Set Fare";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnSetFares.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.AliceBlue;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnSetFares.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.AliceBlue;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnSetFares.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.AliceBlue;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnSetFares.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.AliceBlue;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSetFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSetFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).ForeColor = System.Drawing.Color.Black;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSetFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).BackColor = System.Drawing.SystemColors.Info;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSetFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtPaymentReference
            // 
            this.txtPaymentReference.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentReference.Location = new System.Drawing.Point(930, 78);
            this.txtPaymentReference.MaxLength = 500;
            this.txtPaymentReference.Multiline = true;
            this.txtPaymentReference.Name = "txtPaymentReference";
            // 
            // 
            // 
            this.txtPaymentReference.RootElement.StretchVertically = true;
            this.txtPaymentReference.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPaymentReference.Size = new System.Drawing.Size(280, 61);
            this.txtPaymentReference.TabIndex = 0;
            this.txtPaymentReference.TabStop = false;
            // 
            // lblPaymentRef
            // 
            this.lblPaymentRef.AutoSize = false;
            this.lblPaymentRef.BackColor = System.Drawing.Color.Linen;
            this.lblPaymentRef.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentRef.ForeColor = System.Drawing.Color.Black;
            this.lblPaymentRef.Location = new System.Drawing.Point(945, 59);
            this.lblPaymentRef.Name = "lblPaymentRef";
            // 
            // 
            // 
            this.lblPaymentRef.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblPaymentRef.Size = new System.Drawing.Size(192, 20);
            this.lblPaymentRef.TabIndex = 244;
            this.lblPaymentRef.Text = "Payment References";
            // 
            // txtFaresPostedFrom
            // 
            this.txtFaresPostedFrom.AutoSize = false;
            this.txtFaresPostedFrom.BackColor = System.Drawing.Color.Orange;
            this.txtFaresPostedFrom.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFaresPostedFrom.ForeColor = System.Drawing.Color.Maroon;
            this.txtFaresPostedFrom.Location = new System.Drawing.Point(660, 61);
            this.txtFaresPostedFrom.Name = "txtFaresPostedFrom";
            // 
            // 
            // 
            this.txtFaresPostedFrom.RootElement.ForeColor = System.Drawing.Color.Maroon;
            this.txtFaresPostedFrom.Size = new System.Drawing.Size(62, 30);
            this.txtFaresPostedFrom.TabIndex = 243;
            this.txtFaresPostedFrom.Text = "Meter";
            this.txtFaresPostedFrom.Visible = false;
            // 
            // radLabel5
            // 
            this.radLabel5.AutoSize = false;
            this.radLabel5.BackColor = System.Drawing.Color.Orange;
            this.radLabel5.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.radLabel5.ForeColor = System.Drawing.Color.Black;
            this.radLabel5.Location = new System.Drawing.Point(525, 64);
            this.radLabel5.Name = "radLabel5";
            // 
            // 
            // 
            this.radLabel5.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel5.Size = new System.Drawing.Size(65, 22);
            this.radLabel5.TabIndex = 237;
            this.radLabel5.Text = "Fares  £";
            // 
            // numFareRate
            // 
            this.numFareRate.DecimalPlaces = 2;
            this.numFareRate.EnableKeyMap = true;
            this.numFareRate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFareRate.ForeColor = System.Drawing.Color.Red;
            this.numFareRate.InterceptArrowKeys = false;
            this.numFareRate.Location = new System.Drawing.Point(595, 62);
            this.numFareRate.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFareRate.Name = "numFareRate";
            // 
            // 
            // 
            this.numFareRate.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numFareRate.RootElement.ForeColor = System.Drawing.Color.Red;
            this.numFareRate.ShowBorder = true;
            this.numFareRate.ShowUpDownButtons = false;
            this.numFareRate.Size = new System.Drawing.Size(62, 24);
            this.numFareRate.TabIndex = 2;
            this.numFareRate.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numFareRate.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numFareRate.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numFareRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numFareRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numFareRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDetailMap
            // 
            this.btnDetailMap.Image = global::Taxi_AppMain.Properties.Resources.map_icon;
            this.btnDetailMap.Location = new System.Drawing.Point(808, 113);
            this.btnDetailMap.Name = "btnDetailMap";
            this.btnDetailMap.Size = new System.Drawing.Size(113, 27);
            this.btnDetailMap.TabIndex = 0;
            this.btnDetailMap.TabStop = false;
            this.btnDetailMap.Text = "Show Map (F3)";
            this.btnDetailMap.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDetailMap.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.map_icon;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDetailMap.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDetailMap.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDetailMap.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDetailMap.GetChildAt(0))).Text = "Show Map (F3)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDetailMap.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = false;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDetailMap.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDetailMap.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnlOtherCharges
            // 
            this.pnlOtherCharges.Controls.Add(this.lblTip);
            this.pnlOtherCharges.Controls.Add(this.numTipAmount);
            this.pnlOtherCharges.Controls.Add(this.radLabel4);
            this.pnlOtherCharges.Controls.Add(this.numDrvWaitingMins);
            this.pnlOtherCharges.Controls.Add(this.radLabel18);
            this.pnlOtherCharges.Controls.Add(this.numTotalChrgs);
            this.pnlOtherCharges.Controls.Add(this.radLabel20);
            this.pnlOtherCharges.Controls.Add(this.numCongChrgs);
            this.pnlOtherCharges.Controls.Add(this.radLabel16);
            this.pnlOtherCharges.Controls.Add(this.numMeetCharges);
            this.pnlOtherCharges.Controls.Add(this.radLabel17);
            this.pnlOtherCharges.Controls.Add(this.numExtraChrgs);
            this.pnlOtherCharges.Controls.Add(this.lblAccWaitingCharges);
            this.pnlOtherCharges.Controls.Add(this.numWaitingChrgs);
            this.pnlOtherCharges.Controls.Add(this.lblAccParkingCharges);
            this.pnlOtherCharges.Controls.Add(this.numParkingChrgs);
            this.pnlOtherCharges.Location = new System.Drawing.Point(2, 54);
            this.pnlOtherCharges.Name = "pnlOtherCharges";
            this.pnlOtherCharges.Size = new System.Drawing.Size(520, 85);
            this.pnlOtherCharges.TabIndex = 0;
            this.pnlOtherCharges.TabStop = true;
            // 
            // lblTip
            // 
            this.lblTip.BackColor = this.pnlOtherCharges.BackColor;
            this.lblTip.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblTip.ForeColor = System.Drawing.Color.Black;
            this.lblTip.Location = new System.Drawing.Point(229, 8);
            this.lblTip.Name = "lblTip";
            // 
            // 
            // 
            this.lblTip.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblTip.Size = new System.Drawing.Size(28, 20);
            this.lblTip.TabIndex = 155;
            this.lblTip.Text = "Tip";
            this.lblTip.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lblTip.Visible = false;
            // 
            // numTipAmount
            // 
            this.numTipAmount.EnableKeyMap = true;
            this.numTipAmount.Font = new System.Drawing.Font("Tahoma", 9F);
            this.numTipAmount.InterceptArrowKeys = false;
            this.numTipAmount.Location = new System.Drawing.Point(260, 5);
            this.numTipAmount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numTipAmount.Name = "numTipAmount";
            // 
            // 
            // 
            this.numTipAmount.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numTipAmount.ShowBorder = true;
            this.numTipAmount.ShowUpDownButtons = false;
            this.numTipAmount.Size = new System.Drawing.Size(31, 24);
            this.numTipAmount.TabIndex = 0;
            this.numTipAmount.TabStop = false;
            this.numTipAmount.Visible = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numTipAmount.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numTipAmount.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTipAmount.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTipAmount.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel4
            // 
            this.radLabel4.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel4.ForeColor = System.Drawing.Color.Black;
            this.radLabel4.Location = new System.Drawing.Point(233, 61);
            this.radLabel4.Name = "radLabel4";
            // 
            // 
            // 
            this.radLabel4.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel4.Size = new System.Drawing.Size(37, 20);
            this.radLabel4.TabIndex = 153;
            this.radLabel4.Text = "W/T";
            this.radLabel4.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // numDrvWaitingMins
            // 
            this.numDrvWaitingMins.EnableKeyMap = true;
            this.numDrvWaitingMins.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numDrvWaitingMins.InterceptArrowKeys = false;
            this.numDrvWaitingMins.Location = new System.Drawing.Point(279, 58);
            this.numDrvWaitingMins.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numDrvWaitingMins.Name = "numDrvWaitingMins";
            // 
            // 
            // 
            this.numDrvWaitingMins.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numDrvWaitingMins.ShowBorder = true;
            this.numDrvWaitingMins.ShowUpDownButtons = false;
            this.numDrvWaitingMins.Size = new System.Drawing.Size(48, 24);
            this.numDrvWaitingMins.TabIndex = 0;
            this.numDrvWaitingMins.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numDrvWaitingMins.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numDrvWaitingMins.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numDrvWaitingMins.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numDrvWaitingMins.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel18
            // 
            this.radLabel18.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel18.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.radLabel18.ForeColor = System.Drawing.Color.Black;
            this.radLabel18.Location = new System.Drawing.Point(329, 61);
            this.radLabel18.Name = "radLabel18";
            // 
            // 
            // 
            this.radLabel18.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel18.Size = new System.Drawing.Size(120, 21);
            this.radLabel18.TabIndex = 150;
            this.radLabel18.Text = "Total Charges £";
            this.radLabel18.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // numTotalChrgs
            // 
            this.numTotalChrgs.DecimalPlaces = 2;
            this.numTotalChrgs.EnableKeyMap = true;
            this.numTotalChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numTotalChrgs.InterceptArrowKeys = false;
            this.numTotalChrgs.Location = new System.Drawing.Point(458, 59);
            this.numTotalChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numTotalChrgs.Name = "numTotalChrgs";
            this.numTotalChrgs.ReadOnly = true;
            // 
            // 
            // 
            this.numTotalChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numTotalChrgs.ShowBorder = true;
            this.numTotalChrgs.ShowUpDownButtons = false;
            this.numTotalChrgs.Size = new System.Drawing.Size(59, 24);
            this.numTotalChrgs.TabIndex = 0;
            this.numTotalChrgs.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numTotalChrgs.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numTotalChrgs.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTotalChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTotalChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel20
            // 
            this.radLabel20.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel20.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel20.ForeColor = System.Drawing.Color.Black;
            this.radLabel20.Location = new System.Drawing.Point(1, 33);
            this.radLabel20.Name = "radLabel20";
            // 
            // 
            // 
            this.radLabel20.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel20.Size = new System.Drawing.Size(165, 20);
            this.radLabel20.TabIndex = 148;
            this.radLabel20.Text = "Drv Parking Charges £ ";
            // 
            // numCongChrgs
            // 
            this.numCongChrgs.DecimalPlaces = 2;
            this.numCongChrgs.EnableKeyMap = true;
            this.numCongChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numCongChrgs.InterceptArrowKeys = false;
            this.numCongChrgs.Location = new System.Drawing.Point(170, 31);
            this.numCongChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCongChrgs.Name = "numCongChrgs";
            // 
            // 
            // 
            this.numCongChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCongChrgs.ShowBorder = true;
            this.numCongChrgs.ShowUpDownButtons = false;
            this.numCongChrgs.Size = new System.Drawing.Size(59, 24);
            this.numCongChrgs.TabIndex = 0;
            this.numCongChrgs.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCongChrgs.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCongChrgs.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCongChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCongChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel16
            // 
            this.radLabel16.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel16.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel16.ForeColor = System.Drawing.Color.Black;
            this.radLabel16.Location = new System.Drawing.Point(288, 33);
            this.radLabel16.Name = "radLabel16";
            // 
            // 
            // 
            this.radLabel16.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel16.Size = new System.Drawing.Size(166, 20);
            this.radLabel16.TabIndex = 146;
            this.radLabel16.Text = "Drv Waiting Charges  £";
            this.radLabel16.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // numMeetCharges
            // 
            this.numMeetCharges.DecimalPlaces = 2;
            this.numMeetCharges.EnableKeyMap = true;
            this.numMeetCharges.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numMeetCharges.InterceptArrowKeys = false;
            this.numMeetCharges.Location = new System.Drawing.Point(457, 31);
            this.numMeetCharges.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numMeetCharges.Name = "numMeetCharges";
            // 
            // 
            // 
            this.numMeetCharges.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numMeetCharges.ShowBorder = true;
            this.numMeetCharges.ShowUpDownButtons = false;
            this.numMeetCharges.Size = new System.Drawing.Size(61, 24);
            this.numMeetCharges.TabIndex = 0;
            this.numMeetCharges.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numMeetCharges.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numMeetCharges.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numMeetCharges.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numMeetCharges.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel17
            // 
            this.radLabel17.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel17.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel17.ForeColor = System.Drawing.Color.Black;
            this.radLabel17.Location = new System.Drawing.Point(3, 61);
            this.radLabel17.Name = "radLabel17";
            // 
            // 
            // 
            this.radLabel17.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel17.Size = new System.Drawing.Size(158, 20);
            this.radLabel17.TabIndex = 144;
            this.radLabel17.Text = "Extra Drop/Charges £";
            // 
            // numExtraChrgs
            // 
            this.numExtraChrgs.DecimalPlaces = 2;
            this.numExtraChrgs.EnableKeyMap = true;
            this.numExtraChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numExtraChrgs.InterceptArrowKeys = false;
            this.numExtraChrgs.Location = new System.Drawing.Point(170, 59);
            this.numExtraChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numExtraChrgs.Name = "numExtraChrgs";
            // 
            // 
            // 
            this.numExtraChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numExtraChrgs.ShowBorder = true;
            this.numExtraChrgs.ShowUpDownButtons = false;
            this.numExtraChrgs.Size = new System.Drawing.Size(60, 24);
            this.numExtraChrgs.TabIndex = 0;
            this.numExtraChrgs.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numExtraChrgs.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numExtraChrgs.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numExtraChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numExtraChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblAccWaitingCharges
            // 
            this.lblAccWaitingCharges.BackColor = this.pnlOtherCharges.BackColor;
            this.lblAccWaitingCharges.Enabled = false;
            this.lblAccWaitingCharges.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblAccWaitingCharges.ForeColor = System.Drawing.Color.Black;
            this.lblAccWaitingCharges.Location = new System.Drawing.Point(290, 7);
            this.lblAccWaitingCharges.Name = "lblAccWaitingCharges";
            // 
            // 
            // 
            this.lblAccWaitingCharges.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblAccWaitingCharges.Size = new System.Drawing.Size(164, 20);
            this.lblAccWaitingCharges.TabIndex = 142;
            this.lblAccWaitingCharges.Text = "A/C Waiting Charges £";
            this.lblAccWaitingCharges.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // numWaitingChrgs
            // 
            this.numWaitingChrgs.DecimalPlaces = 2;
            this.numWaitingChrgs.Enabled = false;
            this.numWaitingChrgs.EnableKeyMap = true;
            this.numWaitingChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numWaitingChrgs.InterceptArrowKeys = false;
            this.numWaitingChrgs.Location = new System.Drawing.Point(457, 5);
            this.numWaitingChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numWaitingChrgs.Name = "numWaitingChrgs";
            // 
            // 
            // 
            this.numWaitingChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numWaitingChrgs.ShowBorder = true;
            this.numWaitingChrgs.ShowUpDownButtons = false;
            this.numWaitingChrgs.Size = new System.Drawing.Size(60, 24);
            this.numWaitingChrgs.TabIndex = 0;
            this.numWaitingChrgs.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numWaitingChrgs.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numWaitingChrgs.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWaitingChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWaitingChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblAccParkingCharges
            // 
            this.lblAccParkingCharges.BackColor = this.pnlOtherCharges.BackColor;
            this.lblAccParkingCharges.Enabled = false;
            this.lblAccParkingCharges.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblAccParkingCharges.ForeColor = System.Drawing.Color.Black;
            this.lblAccParkingCharges.Location = new System.Drawing.Point(2, 6);
            this.lblAccParkingCharges.Name = "lblAccParkingCharges";
            // 
            // 
            // 
            this.lblAccParkingCharges.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblAccParkingCharges.Size = new System.Drawing.Size(163, 20);
            this.lblAccParkingCharges.TabIndex = 140;
            this.lblAccParkingCharges.Text = "A/C Parking Charges £";
            // 
            // numParkingChrgs
            // 
            this.numParkingChrgs.DecimalPlaces = 2;
            this.numParkingChrgs.Enabled = false;
            this.numParkingChrgs.EnableKeyMap = true;
            this.numParkingChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numParkingChrgs.InterceptArrowKeys = false;
            this.numParkingChrgs.Location = new System.Drawing.Point(169, 4);
            this.numParkingChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numParkingChrgs.Name = "numParkingChrgs";
            // 
            // 
            // 
            this.numParkingChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numParkingChrgs.ShowBorder = true;
            this.numParkingChrgs.ShowUpDownButtons = false;
            this.numParkingChrgs.Size = new System.Drawing.Size(60, 24);
            this.numParkingChrgs.TabIndex = 0;
            this.numParkingChrgs.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numParkingChrgs.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numParkingChrgs.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numParkingChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numParkingChrgs.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnlPaymentMode
            // 
            this.pnlPaymentMode.AutoSize = false;
            this.pnlPaymentMode.BackColor = this.pnlOtherCharges.BackColor;
            this.pnlPaymentMode.BorderVisible = true;
            this.pnlPaymentMode.Controls.Add(this.chkSecondaryPaymentType);
            this.pnlPaymentMode.Controls.Add(this.numCashPaymentFares);
            this.pnlPaymentMode.Controls.Add(this.btnPayment);
            this.pnlPaymentMode.Controls.Add(this.ddlCommissionType);
            this.pnlPaymentMode.Controls.Add(this.numDriverCommission);
            this.pnlPaymentMode.Controls.Add(this.chkIsCommissionWise);
            this.pnlPaymentMode.Controls.Add(this.radLabel7);
            this.pnlPaymentMode.Controls.Add(this.ddlPaymentType);
            this.pnlPaymentMode.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.pnlPaymentMode.ForeColor = System.Drawing.Color.White;
            this.pnlPaymentMode.Location = new System.Drawing.Point(2, 22);
            this.pnlPaymentMode.Name = "pnlPaymentMode";
            // 
            // 
            // 
            this.pnlPaymentMode.RootElement.ForeColor = System.Drawing.Color.White;
            this.pnlPaymentMode.Size = new System.Drawing.Size(638, 35);
            this.pnlPaymentMode.TabIndex = 0;
            this.pnlPaymentMode.TabStop = true;
            // 
            // chkSecondaryPaymentType
            // 
            this.chkSecondaryPaymentType.AutoSize = false;
            this.chkSecondaryPaymentType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSecondaryPaymentType.ForeColor = System.Drawing.Color.Black;
            this.chkSecondaryPaymentType.Location = new System.Drawing.Point(347, 7);
            this.chkSecondaryPaymentType.Name = "chkSecondaryPaymentType";
            // 
            // 
            // 
            this.chkSecondaryPaymentType.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkSecondaryPaymentType.Size = new System.Drawing.Size(224, 18);
            this.chkSecondaryPaymentType.TabIndex = 0;
            this.chkSecondaryPaymentType.Text = "Additional Cash Payment Type";
            this.chkSecondaryPaymentType.Visible = false;
            // 
            // numCashPaymentFares
            // 
            this.numCashPaymentFares.DecimalPlaces = 2;
            this.numCashPaymentFares.Enabled = false;
            this.numCashPaymentFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCashPaymentFares.ForeColor = System.Drawing.Color.Red;
            this.numCashPaymentFares.InterceptArrowKeys = false;
            this.numCashPaymentFares.Location = new System.Drawing.Point(575, 5);
            this.numCashPaymentFares.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCashPaymentFares.Name = "numCashPaymentFares";
            // 
            // 
            // 
            this.numCashPaymentFares.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCashPaymentFares.RootElement.ForeColor = System.Drawing.Color.Red;
            this.numCashPaymentFares.ShowBorder = true;
            this.numCashPaymentFares.ShowUpDownButtons = false;
            this.numCashPaymentFares.Size = new System.Drawing.Size(61, 24);
            this.numCashPaymentFares.TabIndex = 0;
            this.numCashPaymentFares.TabStop = false;
            this.numCashPaymentFares.Visible = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCashPaymentFares.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCashPaymentFares.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCashPaymentFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCashPaymentFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPayment
            // 
            this.btnPayment.Enabled = false;
            this.btnPayment.Image = global::Taxi_AppMain.Properties.Resources.payment;
            this.btnPayment.Location = new System.Drawing.Point(236, 5);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(105, 24);
            this.btnPayment.TabIndex = 0;
            this.btnPayment.Text = "Payment";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPayment.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.payment;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPayment.GetChildAt(0))).Text = "Payment";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPayment.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPayment.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlCommissionType
            // 
            this.ddlCommissionType.Caption = null;
            this.ddlCommissionType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlCommissionType.Enabled = false;
            this.ddlCommissionType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCommissionType.ForeColor = System.Drawing.Color.Black;
            this.ddlCommissionType.Location = new System.Drawing.Point(527, 4);
            this.ddlCommissionType.Name = "ddlCommissionType";
            this.ddlCommissionType.Property = null;
            // 
            // 
            // 
            this.ddlCommissionType.RootElement.ForeColor = System.Drawing.Color.Black;
            this.ddlCommissionType.ShowDownArrow = true;
            this.ddlCommissionType.Size = new System.Drawing.Size(108, 26);
            this.ddlCommissionType.TabIndex = 207;
            // 
            // numDriverCommission
            // 
            this.numDriverCommission.Enabled = false;
            this.numDriverCommission.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDriverCommission.Location = new System.Drawing.Point(458, 5);
            this.numDriverCommission.Name = "numDriverCommission";
            // 
            // 
            // 
            this.numDriverCommission.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numDriverCommission.ShowBorder = true;
            this.numDriverCommission.ShowUpDownButtons = false;
            this.numDriverCommission.Size = new System.Drawing.Size(63, 24);
            this.numDriverCommission.TabIndex = 206;
            this.numDriverCommission.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numDriverCommission.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numDriverCommission.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numDriverCommission.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkIsCommissionWise
            // 
            this.chkIsCommissionWise.AutoSize = false;
            this.chkIsCommissionWise.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.chkIsCommissionWise.ForeColor = System.Drawing.Color.Black;
            this.chkIsCommissionWise.Location = new System.Drawing.Point(354, 7);
            this.chkIsCommissionWise.Name = "chkIsCommissionWise";
            // 
            // 
            // 
            this.chkIsCommissionWise.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkIsCommissionWise.Size = new System.Drawing.Size(105, 18);
            this.chkIsCommissionWise.TabIndex = 205;
            this.chkIsCommissionWise.Text = "Commission";
            // 
            // radLabel7
            // 
            this.radLabel7.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel7.ForeColor = System.Drawing.Color.Black;
            this.radLabel7.Location = new System.Drawing.Point(2, 6);
            this.radLabel7.Name = "radLabel7";
            // 
            // 
            // 
            this.radLabel7.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel7.Size = new System.Drawing.Size(109, 20);
            this.radLabel7.TabIndex = 132;
            this.radLabel7.Text = "Payment Mode";
            // 
            // ddlPaymentType
            // 
            this.ddlPaymentType.Caption = null;
            this.ddlPaymentType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPaymentType.ForeColor = System.Drawing.Color.Black;
            this.ddlPaymentType.Location = new System.Drawing.Point(114, 4);
            this.ddlPaymentType.Name = "ddlPaymentType";
            this.ddlPaymentType.Property = null;
            // 
            // 
            // 
            this.ddlPaymentType.RootElement.ForeColor = System.Drawing.Color.Black;
            this.ddlPaymentType.ShowDownArrow = true;
            this.ddlPaymentType.Size = new System.Drawing.Size(115, 26);
            this.ddlPaymentType.TabIndex = 0;
            // 
            // btnPickFares
            // 
            this.btnPickFares.Image = global::Taxi_AppMain.Properties.Resources.fares28x28;
            this.btnPickFares.Location = new System.Drawing.Point(644, 22);
            this.btnPickFares.Name = "btnPickFares";
            this.btnPickFares.Size = new System.Drawing.Size(210, 36);
            this.btnPickFares.TabIndex = 0;
            this.btnPickFares.Text = "Calculate Fares (F4)";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPickFares.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.fares28x28;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPickFares.GetChildAt(0))).Text = "Calculate Fares (F4)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPickFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPickFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblMap
            // 
            this.lblMap.BackColor = System.Drawing.Color.Linen;
            this.lblMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMap.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMap.Location = new System.Drawing.Point(520, 114);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(402, 26);
            this.lblMap.TabIndex = 0;
            // 
            // lblPaymentHeading
            // 
            this.lblPaymentHeading.AutoSize = false;
            this.lblPaymentHeading.BackColor = System.Drawing.Color.Maroon;
            this.lblPaymentHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPaymentHeading.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblPaymentHeading.ForeColor = System.Drawing.Color.White;
            this.lblPaymentHeading.Location = new System.Drawing.Point(0, 0);
            this.lblPaymentHeading.Name = "lblPaymentHeading";
            // 
            // 
            // 
            this.lblPaymentHeading.RootElement.ForeColor = System.Drawing.Color.White;
            this.lblPaymentHeading.Size = new System.Drawing.Size(1215, 20);
            this.lblPaymentHeading.TabIndex = 25;
            this.lblPaymentHeading.Text = "Payment && Charges Details";
            // 
            // pnlFares
            // 
            this.pnlFares.BackColor = System.Drawing.Color.Orange;
            this.pnlFares.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFares.Location = new System.Drawing.Point(520, 60);
            this.pnlFares.Name = "pnlFares";
            this.pnlFares.Size = new System.Drawing.Size(402, 53);
            this.pnlFares.TabIndex = 2;
            // 
            // pnlBookingFees
            // 
            this.pnlBookingFees.AutoSize = false;
            this.pnlBookingFees.BackColor = System.Drawing.Color.Orange;
            this.pnlBookingFees.Controls.Add(this.numReturnBookingFee);
            this.pnlBookingFees.Controls.Add(this.lblReturnCustFare);
            this.pnlBookingFees.Controls.Add(this.numBookingFee);
            this.pnlBookingFees.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.pnlBookingFees.ForeColor = System.Drawing.Color.Black;
            this.pnlBookingFees.Location = new System.Drawing.Point(920, 23);
            this.pnlBookingFees.Name = "pnlBookingFees";
            // 
            // 
            // 
            this.pnlBookingFees.RootElement.ForeColor = System.Drawing.Color.Black;
            this.pnlBookingFees.Size = new System.Drawing.Size(289, 35);
            this.pnlBookingFees.TabIndex = 0;
            this.pnlBookingFees.Text = "Booking Fee £";
            this.pnlBookingFees.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlBookingFees.Visible = false;
            // 
            // numReturnBookingFee
            // 
            this.numReturnBookingFee.DecimalPlaces = 2;
            this.numReturnBookingFee.EnableKeyMap = true;
            this.numReturnBookingFee.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReturnBookingFee.InterceptArrowKeys = false;
            this.numReturnBookingFee.Location = new System.Drawing.Point(225, 5);
            this.numReturnBookingFee.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numReturnBookingFee.Name = "numReturnBookingFee";
            // 
            // 
            // 
            this.numReturnBookingFee.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numReturnBookingFee.ShowBorder = true;
            this.numReturnBookingFee.ShowUpDownButtons = false;
            this.numReturnBookingFee.Size = new System.Drawing.Size(58, 24);
            this.numReturnBookingFee.TabIndex = 0;
            this.numReturnBookingFee.TabStop = false;
            this.numReturnBookingFee.Visible = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnBookingFee.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnBookingFee.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnBookingFee.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblReturnCustFare
            // 
            this.lblReturnCustFare.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnCustFare.Location = new System.Drawing.Point(172, 8);
            this.lblReturnCustFare.Name = "lblReturnCustFare";
            this.lblReturnCustFare.Size = new System.Drawing.Size(49, 19);
            this.lblReturnCustFare.TabIndex = 248;
            this.lblReturnCustFare.Text = "R/T  £";
            this.lblReturnCustFare.Visible = false;
            // 
            // numBookingFee
            // 
            this.numBookingFee.DecimalPlaces = 2;
            this.numBookingFee.EnableKeyMap = true;
            this.numBookingFee.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBookingFee.InterceptArrowKeys = false;
            this.numBookingFee.Location = new System.Drawing.Point(107, 5);
            this.numBookingFee.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numBookingFee.Name = "numBookingFee";
            // 
            // 
            // 
            this.numBookingFee.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numBookingFee.ShowBorder = true;
            this.numBookingFee.ShowUpDownButtons = false;
            this.numBookingFee.Size = new System.Drawing.Size(59, 24);
            this.numBookingFee.TabIndex = 0;
            this.numBookingFee.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numBookingFee.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numBookingFee.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numBookingFee.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numBookingFee.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblDropOffPlot
            // 
            this.lblDropOffPlot.AutoSize = true;
            this.lblDropOffPlot.BackColor = this.pnlMain.BackColor;
            this.lblDropOffPlot.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDropOffPlot.Location = new System.Drawing.Point(392, 185);
            this.lblDropOffPlot.Name = "lblDropOffPlot";
            this.lblDropOffPlot.Size = new System.Drawing.Size(88, 16);
            this.lblDropOffPlot.TabIndex = 261;
            this.lblDropOffPlot.Text = "Return From";
            this.lblDropOffPlot.Visible = false;
            // 
            // lblPickupPlot
            // 
            this.lblPickupPlot.AutoSize = true;
            this.lblPickupPlot.BackColor = this.pnlMain.BackColor;
            this.lblPickupPlot.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPickupPlot.Location = new System.Drawing.Point(7, 186);
            this.lblPickupPlot.Name = "lblPickupPlot";
            this.lblPickupPlot.Size = new System.Drawing.Size(72, 16);
            this.lblPickupPlot.TabIndex = 259;
            this.lblPickupPlot.Text = "Return To";
            this.lblPickupPlot.Visible = false;
            // 
            // txtVehicleNo
            // 
            this.txtVehicleNo.BackColor = System.Drawing.Color.AliceBlue;
            this.txtVehicleNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicleNo.ForeColor = System.Drawing.Color.Black;
            this.txtVehicleNo.Location = new System.Drawing.Point(244, 211);
            this.txtVehicleNo.Name = "txtVehicleNo";
            // 
            // 
            // 
            this.txtVehicleNo.RootElement.ForeColor = System.Drawing.Color.Black;
            this.txtVehicleNo.Size = new System.Drawing.Size(2, 2);
            this.txtVehicleNo.TabIndex = 258;
            // 
            // pnlAutoDespatch
            // 
            this.pnlAutoDespatch.BackColor = System.Drawing.Color.FloralWhite;
            this.pnlAutoDespatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAutoDespatch.Controls.Add(this.btnSms);
            this.pnlAutoDespatch.Controls.Add(this.optSMSThirdParty);
            this.pnlAutoDespatch.Controls.Add(this.optSMSGsm);
            this.pnlAutoDespatch.Controls.Add(this.chkBidding);
            this.pnlAutoDespatch.Controls.Add(this.chkDisablePassengerSMS);
            this.pnlAutoDespatch.Controls.Add(this.chkDisableDriverSMS);
            this.pnlAutoDespatch.Controls.Add(this.chkAutoDespatch);
            this.pnlAutoDespatch.Controls.Add(this.numBeforeMinutes);
            this.pnlAutoDespatch.Location = new System.Drawing.Point(755, 216);
            this.pnlAutoDespatch.Name = "pnlAutoDespatch";
            this.pnlAutoDespatch.Size = new System.Drawing.Size(165, 134);
            this.pnlAutoDespatch.TabIndex = 0;
            // 
            // btnSms
            // 
            this.btnSms.BackColor = System.Drawing.Color.Purple;
            this.btnSms.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSms.ForeColor = System.Drawing.Color.White;
            this.btnSms.Location = new System.Drawing.Point(18, 46);
            this.btnSms.Name = "btnSms";
            this.btnSms.Size = new System.Drawing.Size(124, 38);
            this.btnSms.TabIndex = 222;
            this.btnSms.Text = "SMS";
            this.btnSms.UseVisualStyleBackColor = false;
            this.btnSms.Visible = false;
            // 
            // optSMSThirdParty
            // 
            this.optSMSThirdParty.BackColor = System.Drawing.Color.Transparent;
            this.optSMSThirdParty.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSMSThirdParty.ForeColor = System.Drawing.Color.Purple;
            this.optSMSThirdParty.Location = new System.Drawing.Point(-1, 66);
            this.optSMSThirdParty.Name = "optSMSThirdParty";
            // 
            // 
            // 
            this.optSMSThirdParty.RootElement.ForeColor = System.Drawing.Color.Purple;
            this.optSMSThirdParty.Size = new System.Drawing.Size(91, 18);
            this.optSMSThirdParty.TabIndex = 237;
            this.optSMSThirdParty.Text = "By Provider";
            this.optSMSThirdParty.Visible = false;
            // 
            // optSMSGsm
            // 
            this.optSMSGsm.BackColor = System.Drawing.Color.Transparent;
            this.optSMSGsm.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSMSGsm.ForeColor = System.Drawing.Color.Purple;
            this.optSMSGsm.Location = new System.Drawing.Point(-1, 46);
            this.optSMSGsm.Name = "optSMSGsm";
            // 
            // 
            // 
            this.optSMSGsm.RootElement.ForeColor = System.Drawing.Color.Purple;
            this.optSMSGsm.Size = new System.Drawing.Size(83, 18);
            this.optSMSGsm.TabIndex = 236;
            this.optSMSGsm.Text = "By GSM ";
            this.optSMSGsm.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.optSMSGsm.Visible = false;
            // 
            // chkBidding
            // 
            this.chkBidding.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.chkBidding.Location = new System.Drawing.Point(1, 109);
            this.chkBidding.Name = "chkBidding";
            this.chkBidding.Size = new System.Drawing.Size(73, 20);
            this.chkBidding.TabIndex = 223;
            this.chkBidding.Text = "Bidding";
            // 
            // chkDisablePassengerSMS
            // 
            this.chkDisablePassengerSMS.BackColor = System.Drawing.Color.Transparent;
            this.chkDisablePassengerSMS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.chkDisablePassengerSMS.ForeColor = System.Drawing.Color.Black;
            this.chkDisablePassengerSMS.Location = new System.Drawing.Point(1, 26);
            this.chkDisablePassengerSMS.Name = "chkDisablePassengerSMS";
            // 
            // 
            // 
            this.chkDisablePassengerSMS.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkDisablePassengerSMS.Size = new System.Drawing.Size(148, 16);
            this.chkDisablePassengerSMS.TabIndex = 204;
            this.chkDisablePassengerSMS.Text = "Disable Passenger Text";
            this.chkDisablePassengerSMS.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            this.chkDisablePassengerSMS.TextWrap = true;
            // 
            // chkDisableDriverSMS
            // 
            this.chkDisableDriverSMS.AutoSize = false;
            this.chkDisableDriverSMS.BackColor = System.Drawing.Color.Transparent;
            this.chkDisableDriverSMS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.chkDisableDriverSMS.ForeColor = System.Drawing.Color.Black;
            this.chkDisableDriverSMS.Location = new System.Drawing.Point(1, 2);
            this.chkDisableDriverSMS.Name = "chkDisableDriverSMS";
            // 
            // 
            // 
            this.chkDisableDriverSMS.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkDisableDriverSMS.Size = new System.Drawing.Size(124, 24);
            this.chkDisableDriverSMS.TabIndex = 203;
            this.chkDisableDriverSMS.Text = "Disable Driver Text";
            this.chkDisableDriverSMS.TextWrap = true;
            // 
            // chkAutoDespatch
            // 
            this.chkAutoDespatch.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.chkAutoDespatch.Location = new System.Drawing.Point(1, 87);
            this.chkAutoDespatch.Name = "chkAutoDespatch";
            this.chkAutoDespatch.Size = new System.Drawing.Size(122, 20);
            this.chkAutoDespatch.TabIndex = 0;
            this.chkAutoDespatch.Text = "Auto Despatch";
            // 
            // numBeforeMinutes
            // 
            this.numBeforeMinutes.Enabled = false;
            this.numBeforeMinutes.InterceptArrowKeys = false;
            this.numBeforeMinutes.Location = new System.Drawing.Point(136, 88);
            this.numBeforeMinutes.Name = "numBeforeMinutes";
            // 
            // 
            // 
            this.numBeforeMinutes.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numBeforeMinutes.ShowBorder = true;
            this.numBeforeMinutes.ShowUpDownButtons = false;
            this.numBeforeMinutes.Size = new System.Drawing.Size(24, 20);
            this.numBeforeMinutes.TabIndex = 10;
            this.numBeforeMinutes.TabStop = false;
            this.numBeforeMinutes.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numBeforeMinutes.Visible = false;
            // 
            // pnlCustomer
            // 
            this.pnlCustomer.AutoSize = false;
            this.pnlCustomer.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlCustomer.Controls.Add(this.radLabel2);
            this.pnlCustomer.Controls.Add(this.txtEmail);
            this.pnlCustomer.Controls.Add(this.btnCustomerLister);
            this.pnlCustomer.Controls.Add(this.radLabel6);
            this.pnlCustomer.Controls.Add(this.radLabel21);
            this.pnlCustomer.Controls.Add(this.radLabel19);
            this.pnlCustomer.Controls.Add(this.txtCustomerPhoneNo);
            this.pnlCustomer.Controls.Add(this.txtCustomerMobileNo);
            this.pnlCustomer.Controls.Add(this.ddlCustomerName);
            this.pnlCustomer.Controls.Add(this.btn_notes);
            this.pnlCustomer.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCustomer.Location = new System.Drawing.Point(361, 215);
            this.pnlCustomer.Name = "pnlCustomer";
            this.pnlCustomer.Size = new System.Drawing.Size(390, 115);
            this.pnlCustomer.TabIndex = 6;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 10.5F);
            this.radLabel2.Location = new System.Drawing.Point(6, 90);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(42, 21);
            this.radLabel2.TabIndex = 165;
            this.radLabel2.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(134, 89);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(201, 24);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.TabStop = false;
            // 
            // btnCustomerLister
            // 
            this.btnCustomerLister.Location = new System.Drawing.Point(340, 4);
            this.btnCustomerLister.Name = "btnCustomerLister";
            this.btnCustomerLister.Size = new System.Drawing.Size(31, 23);
            this.btnCustomerLister.TabIndex = 0;
            this.btnCustomerLister.Text = "...";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCustomerLister.GetChildAt(0))).Text = "...";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCustomerLister.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCustomerLister.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel6
            // 
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.radLabel6.Location = new System.Drawing.Point(2, 5);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(128, 21);
            this.radLabel6.TabIndex = 159;
            this.radLabel6.Text = "Passenger Name";
            // 
            // radLabel21
            // 
            this.radLabel21.Font = new System.Drawing.Font("Tahoma", 10.5F);
            this.radLabel21.Location = new System.Drawing.Point(5, 62);
            this.radLabel21.Name = "radLabel21";
            this.radLabel21.Size = new System.Drawing.Size(70, 21);
            this.radLabel21.TabIndex = 161;
            this.radLabel21.Text = "Mobile No";
            // 
            // radLabel19
            // 
            this.radLabel19.Font = new System.Drawing.Font("Tahoma", 10.5F);
            this.radLabel19.Location = new System.Drawing.Point(5, 33);
            this.radLabel19.Name = "radLabel19";
            this.radLabel19.Size = new System.Drawing.Size(95, 21);
            this.radLabel19.TabIndex = 160;
            this.radLabel19.Text = "Telephone No";
            // 
            // txtCustomerPhoneNo
            // 
            this.txtCustomerPhoneNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerPhoneNo.Location = new System.Drawing.Point(134, 32);
            this.txtCustomerPhoneNo.MaxLength = 30;
            this.txtCustomerPhoneNo.Name = "txtCustomerPhoneNo";
            this.txtCustomerPhoneNo.Size = new System.Drawing.Size(201, 24);
            this.txtCustomerPhoneNo.TabIndex = 0;
            this.txtCustomerPhoneNo.TabStop = false;
            // 
            // txtCustomerMobileNo
            // 
            this.txtCustomerMobileNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerMobileNo.Location = new System.Drawing.Point(134, 61);
            this.txtCustomerMobileNo.MaxLength = 30;
            this.txtCustomerMobileNo.Name = "txtCustomerMobileNo";
            this.txtCustomerMobileNo.Size = new System.Drawing.Size(201, 24);
            this.txtCustomerMobileNo.TabIndex = 0;
            this.txtCustomerMobileNo.TabStop = false;
            // 
            // ddlCustomerName
            // 
            this.ddlCustomerName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCustomerName.Location = new System.Drawing.Point(134, 3);
            this.ddlCustomerName.MaxLength = 100;
            this.ddlCustomerName.Name = "ddlCustomerName";
            this.ddlCustomerName.Size = new System.Drawing.Size(204, 24);
            this.ddlCustomerName.TabIndex = 6;
            this.ddlCustomerName.TabStop = false;
            // 
            // btn_notes
            // 
            this.btn_notes.BackColor = System.Drawing.Color.GhostWhite;
            this.btn_notes.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.btn_notes.Image = global::Taxi_AppMain.Properties.Resources.text;
            this.btn_notes.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_notes.Location = new System.Drawing.Point(336, 48);
            this.btn_notes.Name = "btn_notes";
            this.btn_notes.Size = new System.Drawing.Size(54, 63);
            this.btn_notes.TabIndex = 0;
            this.btn_notes.Text = "Notes(0) [F5]";
            this.btn_notes.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btn_notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_notes.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_notes.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.text;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_notes.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_notes.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_notes.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_notes.GetChildAt(0))).Text = "Notes(0) [F5]";
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btn_notes.GetChildAt(0).GetChildAt(0))).BackColor2 = System.Drawing.Color.LightGoldenrodYellow;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btn_notes.GetChildAt(0).GetChildAt(0))).BackColor3 = System.Drawing.Color.LightGoldenrodYellow;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btn_notes.GetChildAt(0).GetChildAt(0))).BackColor4 = System.Drawing.Color.LightGoldenrodYellow;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btn_notes.GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.LightGoldenrodYellow;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_notes.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_notes.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_notes.GetChildAt(0).GetChildAt(1).GetChildAt(1))).BackColor = System.Drawing.Color.LightGoldenrodYellow;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_notes.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            // 
            // radPanel3
            // 
            this.radPanel3.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel3.Controls.Add(this.btnPrintJob);
            this.radPanel3.Controls.Add(this.btnCancelBooking);
            this.radPanel3.Controls.Add(this.btnExitForm);
            this.radPanel3.Controls.Add(this.radLabel27);
            this.radPanel3.Controls.Add(this.btnSaveNew);
            this.radPanel3.Controls.Add(this.btnConfirmBooking);
            this.radPanel3.Location = new System.Drawing.Point(649, 447);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(558, 84);
            this.radPanel3.TabIndex = 234;
            // 
            // btnPrintJob
            // 
            this.btnPrintJob.DefaultItem = null;
            this.btnPrintJob.Enabled = false;
            this.btnPrintJob.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintJob.Image = global::Taxi_AppMain.Properties.Resources.Print1;
            this.btnPrintJob.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrintJob.Items.AddRange(new Telerik.WinControls.RadItem[] {
            this.btnJobReport,
            this.btnLogDetail});
            this.btnPrintJob.Location = new System.Drawing.Point(145, 23);
            this.btnPrintJob.Name = "btnPrintJob";
            this.btnPrintJob.Size = new System.Drawing.Size(125, 56);
            this.btnPrintJob.TabIndex = 203;
            this.btnPrintJob.Text = "Job Information";
            this.btnPrintJob.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrintJob.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrintJob.DropDownOpening += new System.EventHandler(this.btnPrintJob_DropDownOpening);
            ((Telerik.WinControls.UI.RadSplitButtonElement)(this.btnPrintJob.GetChildAt(0))).Text = "Job Information";
            ((Telerik.WinControls.UI.RadSplitButtonElement)(this.btnPrintJob.GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnPrintJob.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.btnPrintJob.GetChildAt(0).GetChildAt(1).GetChildAt(1).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnJobReport
            // 
            this.btnJobReport.Name = "btnJobReport";
            this.btnJobReport.Text = "Job Report";
            this.btnJobReport.Click += new System.EventHandler(this.btnPrintJob_Click);
            // 
            // btnLogDetail
            // 
            this.btnLogDetail.Name = "btnLogDetail";
            this.btnLogDetail.Text = "Audit Trail Report";
            this.btnLogDetail.Click += new System.EventHandler(this.btnLogDetail_Click);
            // 
            // btnCancelBooking
            // 
            this.btnCancelBooking.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelBooking.Image = global::Taxi_AppMain.Properties.Resources.remove;
            this.btnCancelBooking.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelBooking.Location = new System.Drawing.Point(292, 23);
            this.btnCancelBooking.Name = "btnCancelBooking";
            this.btnCancelBooking.Size = new System.Drawing.Size(112, 56);
            this.btnCancelBooking.TabIndex = 199;
            this.btnCancelBooking.Text = "Cancel Booking";
            this.btnCancelBooking.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancelBooking.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.remove;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancelBooking.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancelBooking.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancelBooking.GetChildAt(0))).Text = "Cancel Booking";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancelBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancelBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExitForm
            // 
            this.btnExitForm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitForm.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitForm.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExitForm.Location = new System.Drawing.Point(433, 23);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(112, 56);
            this.btnExitForm.TabIndex = 200;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel27
            // 
            this.radLabel27.AutoSize = false;
            this.radLabel27.BackColor = System.Drawing.Color.Navy;
            this.radLabel27.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel27.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.radLabel27.ForeColor = System.Drawing.Color.White;
            this.radLabel27.Location = new System.Drawing.Point(0, 0);
            this.radLabel27.Name = "radLabel27";
            // 
            // 
            // 
            this.radLabel27.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel27.Size = new System.Drawing.Size(558, 19);
            this.radLabel27.TabIndex = 26;
            this.radLabel27.Text = "Actions";
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNew.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnSaveNew.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveNew.Location = new System.Drawing.Point(12, 20);
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(112, 62);
            this.btnSaveNew.TabIndex = 25;
            this.btnSaveNew.Text = "Save Booking    (HOME)";
            this.btnSaveNew.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveNew.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveNew.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveNew.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveNew.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveNew.GetChildAt(0))).Text = "Save Booking    (HOME)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveNew.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveNew.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveNew.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnConfirmBooking
            // 
            this.btnConfirmBooking.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmBooking.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnConfirmBooking.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnConfirmBooking.Location = new System.Drawing.Point(294, 23);
            this.btnConfirmBooking.Name = "btnConfirmBooking";
            this.btnConfirmBooking.Size = new System.Drawing.Size(110, 56);
            this.btnConfirmBooking.TabIndex = 202;
            this.btnConfirmBooking.Text = "Confirm Booking";
            this.btnConfirmBooking.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConfirmBooking.Visible = false;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnConfirmBooking.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnConfirmBooking.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnConfirmBooking.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnConfirmBooking.GetChildAt(0))).Text = "Confirm Booking";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnConfirmBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnConfirmBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numTotalLuggages
            // 
            this.numTotalLuggages.EnableKeyMap = true;
            this.numTotalLuggages.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotalLuggages.InterceptArrowKeys = false;
            this.numTotalLuggages.Location = new System.Drawing.Point(263, 293);
            this.numTotalLuggages.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numTotalLuggages.Name = "numTotalLuggages";
            // 
            // 
            // 
            this.numTotalLuggages.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numTotalLuggages.ShowBorder = true;
            this.numTotalLuggages.ShowUpDownButtons = false;
            this.numTotalLuggages.Size = new System.Drawing.Size(57, 24);
            this.numTotalLuggages.TabIndex = 0;
            this.numTotalLuggages.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numTotalLuggages.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numTotalLuggages.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTotalLuggages.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTotalLuggages.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblLuggages
            // 
            this.lblLuggages.BackColor = System.Drawing.Color.Transparent;
            this.lblLuggages.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLuggages.ForeColor = System.Drawing.Color.Black;
            this.lblLuggages.Location = new System.Drawing.Point(174, 294);
            this.lblLuggages.Name = "lblLuggages";
            // 
            // 
            // 
            this.lblLuggages.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblLuggages.Size = new System.Drawing.Size(73, 22);
            this.lblLuggages.TabIndex = 254;
            this.lblLuggages.Text = "Luggages";
            // 
            // lblPassengers
            // 
            this.lblPassengers.BackColor = System.Drawing.Color.Transparent;
            this.lblPassengers.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassengers.ForeColor = System.Drawing.Color.Black;
            this.lblPassengers.Location = new System.Drawing.Point(5, 293);
            this.lblPassengers.Name = "lblPassengers";
            // 
            // 
            // 
            this.lblPassengers.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblPassengers.Size = new System.Drawing.Size(84, 22);
            this.lblPassengers.TabIndex = 252;
            this.lblPassengers.Text = "Passengers";
            // 
            // num_TotalPassengers
            // 
            this.num_TotalPassengers.EnableKeyMap = true;
            this.num_TotalPassengers.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_TotalPassengers.InterceptArrowKeys = false;
            this.num_TotalPassengers.Location = new System.Drawing.Point(110, 293);
            this.num_TotalPassengers.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.num_TotalPassengers.Name = "num_TotalPassengers";
            // 
            // 
            // 
            this.num_TotalPassengers.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.num_TotalPassengers.ShowBorder = true;
            this.num_TotalPassengers.ShowUpDownButtons = false;
            this.num_TotalPassengers.Size = new System.Drawing.Size(56, 24);
            this.num_TotalPassengers.TabIndex = 0;
            this.num_TotalPassengers.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.num_TotalPassengers.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnMultiVehicle
            // 
            this.btnMultiVehicle.Location = new System.Drawing.Point(244, 215);
            this.btnMultiVehicle.Name = "btnMultiVehicle";
            this.btnMultiVehicle.Size = new System.Drawing.Size(82, 24);
            this.btnMultiVehicle.TabIndex = 0;
            this.btnMultiVehicle.Text = "Multi Vehicle";
            // 
            // radLabel11
            // 
            this.radLabel11.BackColor = this.pnlMain.BackColor;
            this.radLabel11.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.radLabel11.Location = new System.Drawing.Point(6, 216);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(99, 21);
            this.radLabel11.TabIndex = 244;
            this.radLabel11.Text = "Vehicle Type";
            // 
            // ddlVehicleType
            // 
            this.ddlVehicleType.Caption = null;
            this.ddlVehicleType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlVehicleType.Location = new System.Drawing.Point(113, 214);
            this.ddlVehicleType.Name = "ddlVehicleType";
            this.ddlVehicleType.Property = null;
            this.ddlVehicleType.ShowDownArrow = true;
            this.ddlVehicleType.Size = new System.Drawing.Size(124, 26);
            this.ddlVehicleType.TabIndex = 8;
            // 
            // chkIsCompanyRates
            // 
            this.chkIsCompanyRates.BackColor = System.Drawing.Color.Transparent;
            this.chkIsCompanyRates.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsCompanyRates.ForeColor = System.Drawing.Color.Black;
            this.chkIsCompanyRates.Location = new System.Drawing.Point(8, 325);
            this.chkIsCompanyRates.Name = "chkIsCompanyRates";
            // 
            // 
            // 
            this.chkIsCompanyRates.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkIsCompanyRates.Size = new System.Drawing.Size(84, 22);
            this.chkIsCompanyRates.TabIndex = 238;
            this.chkIsCompanyRates.Text = "Account";
            this.chkIsCompanyRates.TextWrap = true;
            // 
            // radLabel14
            // 
            this.radLabel14.BackColor = System.Drawing.Color.Transparent;
            this.radLabel14.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.radLabel14.Location = new System.Drawing.Point(6, 269);
            this.radLabel14.Name = "radLabel14";
            this.radLabel14.Size = new System.Drawing.Size(104, 21);
            this.radLabel14.TabIndex = 245;
            this.radLabel14.Text = "Journey Type";
            // 
            // ddlCompany
            // 
            this.ddlCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompany.Location = new System.Drawing.Point(111, 323);
            this.ddlCompany.Name = "ddlCompany";
            // 
            // 
            // 
            this.ddlCompany.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlCompany.Size = new System.Drawing.Size(208, 24);
            this.ddlCompany.TabIndex = 240;
            this.ddlCompany.TabStop = false;
            // 
            // dtpPickupDate
            // 
            this.dtpPickupDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpPickupDate.CustomFormat = "dd/MM/yyyy";
            this.dtpPickupDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPickupDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpPickupDate.Location = new System.Drawing.Point(495, 334);
            this.dtpPickupDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpPickupDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPickupDate.Name = "dtpPickupDate";
            this.dtpPickupDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPickupDate.Size = new System.Drawing.Size(102, 24);
            this.dtpPickupDate.TabIndex = 5;
            this.dtpPickupDate.TabStop = false;
            this.dtpPickupDate.Text = "myDatePicker1";
            this.dtpPickupDate.Value = null;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.radLabel3.Location = new System.Drawing.Point(362, 335);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(94, 21);
            this.radLabel3.TabIndex = 246;
            this.radLabel3.Text = "Pickup Date";
            // 
            // dtpPickupTime
            // 
            this.dtpPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpPickupTime.CustomFormat = "HH:mm";
            this.dtpPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpPickupTime.Location = new System.Drawing.Point(649, 334);
            this.dtpPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPickupTime.Name = "dtpPickupTime";
            this.dtpPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPickupTime.ShowUpDown = true;
            this.dtpPickupTime.Size = new System.Drawing.Size(83, 24);
            this.dtpPickupTime.TabIndex = 4;
            this.dtpPickupTime.TabStop = false;
            this.dtpPickupTime.Text = "myDatePicker1";
            this.dtpPickupTime.Value = null;
            // 
            // lblPickupTime
            // 
            this.lblPickupTime.BackColor = System.Drawing.Color.AliceBlue;
            this.lblPickupTime.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.lblPickupTime.Location = new System.Drawing.Point(602, 335);
            this.lblPickupTime.Name = "lblPickupTime";
            this.lblPickupTime.Size = new System.Drawing.Size(42, 20);
            this.lblPickupTime.TabIndex = 247;
            this.lblPickupTime.Text = "Time";
            // 
            // ddlDriver
            // 
            this.ddlDriver.Caption = null;
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(495, 363);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Property = null;
            this.ddlDriver.ShowDownArrow = true;
            this.ddlDriver.Size = new System.Drawing.Size(238, 26);
            this.ddlDriver.TabIndex = 0;
            // 
            // radLabel25
            // 
            this.radLabel25.AutoSize = false;
            this.radLabel25.BackColor = System.Drawing.Color.Transparent;
            this.radLabel25.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.radLabel25.Location = new System.Drawing.Point(3, 470);
            this.radLabel25.Name = "radLabel25";
            this.radLabel25.Size = new System.Drawing.Size(106, 38);
            this.radLabel25.TabIndex = 243;
            this.radLabel25.Text = "Special Requirements";
            // 
            // radLabel22
            // 
            this.radLabel22.BackColor = System.Drawing.Color.Transparent;
            this.radLabel22.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel22.Location = new System.Drawing.Point(362, 366);
            this.radLabel22.Name = "radLabel22";
            this.radLabel22.Size = new System.Drawing.Size(48, 22);
            this.radLabel22.TabIndex = 248;
            this.radLabel22.Text = "Driver";
            // 
            // txtSpecialRequirements
            // 
            this.txtSpecialRequirements.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.txtSpecialRequirements.Location = new System.Drawing.Point(111, 462);
            this.txtSpecialRequirements.MaxLength = 500;
            this.txtSpecialRequirements.Multiline = true;
            this.txtSpecialRequirements.Name = "txtSpecialRequirements";
            // 
            // 
            // 
            this.txtSpecialRequirements.RootElement.StretchVertically = true;
            this.txtSpecialRequirements.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSpecialRequirements.Size = new System.Drawing.Size(203, 77);
            this.txtSpecialRequirements.TabIndex = 3;
            this.txtSpecialRequirements.TabStop = false;
            // 
            // pnlReturnJobNo
            // 
            this.pnlReturnJobNo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlReturnJobNo.ForeColor = System.Drawing.Color.OrangeRed;
            this.pnlReturnJobNo.Location = new System.Drawing.Point(704, 38);
            this.pnlReturnJobNo.Name = "pnlReturnJobNo";
            this.pnlReturnJobNo.Size = new System.Drawing.Size(210, 28);
            this.pnlReturnJobNo.TabIndex = 202;
            this.pnlReturnJobNo.Text = "return";
            this.pnlReturnJobNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlReturnJobNo.Visible = false;
            // 
            // chkQuotation
            // 
            this.chkQuotation.BackColor = System.Drawing.Color.AliceBlue;
            this.chkQuotation.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.chkQuotation.ForeColor = System.Drawing.Color.Fuchsia;
            this.chkQuotation.Location = new System.Drawing.Point(752, 64);
            this.chkQuotation.Name = "chkQuotation";
            // 
            // 
            // 
            this.chkQuotation.RootElement.ForeColor = System.Drawing.Color.Fuchsia;
            this.chkQuotation.Size = new System.Drawing.Size(134, 22);
            this.chkQuotation.TabIndex = 206;
            this.chkQuotation.Text = "Quotation (F6)";
            this.chkQuotation.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkQuotation_ToggleStateChanged);
            // 
            // chkReverse
            // 
            this.chkReverse.AutoSize = false;
            this.chkReverse.BackColor = System.Drawing.Color.AliceBlue;
            this.chkReverse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkReverse.ForeColor = System.Drawing.Color.Black;
            this.chkReverse.Location = new System.Drawing.Point(756, 119);
            this.chkReverse.Name = "chkReverse";
            // 
            // 
            // 
            this.chkReverse.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkReverse.Size = new System.Drawing.Size(107, 28);
            this.chkReverse.TabIndex = 0;
            this.chkReverse.Text = "Reverse (F9)";
            this.chkReverse.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReverse.TextWrap = true;
            this.chkReverse.ToggleStateChanging += new Telerik.WinControls.UI.StateChangingEventHandler(this.chkReverse_ToggleStateChanging);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.btnDespatchView);
            this.panel1.Controls.Add(this.btnAccountCode);
            this.panel1.Controls.Add(this.btnViewMapReport);
            this.panel1.Controls.Add(this.btnNearestDrv);
            this.panel1.Controls.Add(this.btnSendEmail);
            this.panel1.Controls.Add(this.btnBase);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnMultiBooking);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1215, 37);
            this.panel1.TabIndex = 0;
            // 
            // btnDespatchView
            // 
            this.btnDespatchView.BackColor = System.Drawing.Color.AliceBlue;
            this.btnDespatchView.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnDespatchView.Location = new System.Drawing.Point(823, 0);
            this.btnDespatchView.Name = "btnDespatchView";
            this.btnDespatchView.Size = new System.Drawing.Size(127, 37);
            this.btnDespatchView.TabIndex = 0;
            this.btnDespatchView.Text = "Route Suggestion";
            this.btnDespatchView.UseVisualStyleBackColor = false;
            this.btnDespatchView.Click += new System.EventHandler(this.btnDespatchView_Click);
            // 
            // btnAccountCode
            // 
            this.btnAccountCode.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAccountCode.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnAccountCode.Location = new System.Drawing.Point(315, 2);
            this.btnAccountCode.Name = "btnAccountCode";
            this.btnAccountCode.Size = new System.Drawing.Size(98, 33);
            this.btnAccountCode.TabIndex = 210;
            this.btnAccountCode.Text = "Account Code (F10)";
            this.btnAccountCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccountCode.TextWrap = true;
            this.btnAccountCode.Click += new System.EventHandler(this.btnAccountCode_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAccountCode.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAccountCode.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAccountCode.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAccountCode.GetChildAt(0))).Text = "Account Code (F10)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAccountCode.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAccountCode.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAccountCode.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnViewMapReport
            // 
            this.btnViewMapReport.Enabled = false;
            this.btnViewMapReport.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnViewMapReport.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnViewMapReport.Location = new System.Drawing.Point(1104, 1);
            this.btnViewMapReport.Name = "btnViewMapReport";
            this.btnViewMapReport.Size = new System.Drawing.Size(105, 35);
            this.btnViewMapReport.TabIndex = 0;
            this.btnViewMapReport.Text = "Map Report (CTRL+M)";
            this.btnViewMapReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnViewMapReport.TextWrap = true;
            this.btnViewMapReport.Click += new System.EventHandler(this.btnJobRoutePath_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewMapReport.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewMapReport.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewMapReport.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewMapReport.GetChildAt(0))).Text = "Map Report (CTRL+M)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewMapReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewMapReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewMapReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnNearestDrv
            // 
            this.btnNearestDrv.BackColor = System.Drawing.Color.AliceBlue;
            this.btnNearestDrv.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnNearestDrv.Location = new System.Drawing.Point(967, 0);
            this.btnNearestDrv.Name = "btnNearestDrv";
            this.btnNearestDrv.Size = new System.Drawing.Size(113, 37);
            this.btnNearestDrv.TabIndex = 209;
            this.btnNearestDrv.Text = "Nearest Drivers (F12)";
            this.btnNearestDrv.UseVisualStyleBackColor = false;
            this.btnNearestDrv.Click += new System.EventHandler(this.btnNearestDrv_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSendEmail.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnSendEmail.Location = new System.Drawing.Point(427, 2);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(90, 33);
            this.btnSendEmail.TabIndex = 0;
            this.btnSendEmail.Text = "Send Email  (F11)";
            this.btnSendEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSendEmail.TextWrap = true;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).Text = "Send Email  (F11)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnBase
            // 
            this.btnBase.Location = new System.Drawing.Point(5, 2);
            this.btnBase.Name = "btnBase";
            this.btnBase.Size = new System.Drawing.Size(82, 33);
            this.btnBase.TabIndex = 0;
            this.btnBase.Text = "     Base    (F1)";
            this.btnBase.TextWrap = true;
            this.btnBase.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radToggleButton1_ToggleStateChanged_1);
            ((Telerik.WinControls.UI.RadToggleButtonElement)(this.btnBase.GetChildAt(0))).Text = "     Base    (F1)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnBase.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnBase.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnBase.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSearch
            // 
            this.btnSearch.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSearch.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnSearch.Location = new System.Drawing.Point(101, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 33);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Job History (F7)";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.TextWrap = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSearch.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSearch.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSearch.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSearch.GetChildAt(0))).Text = "Job History (F7)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSearch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSearch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSearch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnMultiBooking
            // 
            this.btnMultiBooking.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMultiBooking.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnMultiBooking.Location = new System.Drawing.Point(205, 2);
            this.btnMultiBooking.Name = "btnMultiBooking";
            this.btnMultiBooking.Size = new System.Drawing.Size(98, 33);
            this.btnMultiBooking.TabIndex = 0;
            this.btnMultiBooking.Text = "Multi Booking (F8)";
            this.btnMultiBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMultiBooking.TextWrap = true;
            this.btnMultiBooking.Click += new System.EventHandler(this.btnMultiBooking_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMultiBooking.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMultiBooking.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMultiBooking.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMultiBooking.GetChildAt(0))).Text = "Multi Booking (F8)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMultiBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMultiBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMultiBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtFromAddress
            // 
            this.txtFromAddress.BackColor = System.Drawing.Color.White;
            this.txtFromAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFromAddress.DefaultHeight = 0;
            this.txtFromAddress.DefaultWidth = 0;
            this.txtFromAddress.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtFromAddress.ForceListBoxToUpdate = false;
            this.txtFromAddress.FormerValue = "";
            this.txtFromAddress.Location = new System.Drawing.Point(114, 64);
            this.txtFromAddress.Multiline = true;
            this.txtFromAddress.Name = "txtFromAddress";
            // 
            // 
            // 
            this.txtFromAddress.RootElement.StretchVertically = true;
            this.txtFromAddress.SelectedItem = null;
            this.txtFromAddress.Size = new System.Drawing.Size(200, 80);
            this.txtFromAddress.TabIndex = 1;
            this.txtFromAddress.TabStop = false;
            this.txtFromAddress.Values = null;
            this.txtFromAddress.Enter += new System.EventHandler(this.txtFromAddress_Enter);
            this.txtFromAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromAddress_KeyDown);
            this.txtFromAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromAddress_KeyPress);
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtFromAddress.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtFromAddress.GetChildAt(0).GetChildAt(2))).Width = 1F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtFromAddress.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtFromAddress.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtFromAddress.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtFromAddress.GetChildAt(0).GetChildAt(2))).InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtFromAddress.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // txtFromPostCode
            // 
            this.txtFromPostCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFromPostCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFromPostCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFromPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromPostCode.Location = new System.Drawing.Point(114, 95);
            this.txtFromPostCode.MaxLength = 100;
            this.txtFromPostCode.Name = "txtFromPostCode";
            this.txtFromPostCode.Size = new System.Drawing.Size(206, 24);
            this.txtFromPostCode.TabIndex = 1;
            this.txtFromPostCode.TabStop = false;
            this.txtFromPostCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromPostCode_KeyDown);
            this.txtFromPostCode.Validated += new System.EventHandler(this.txtFromPostCode_Validated);
            // 
            // txtFromStreetComing
            // 
            this.txtFromStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromStreetComing.Location = new System.Drawing.Point(114, 153);
            this.txtFromStreetComing.MaxLength = 100;
            this.txtFromStreetComing.Name = "txtFromStreetComing";
            this.txtFromStreetComing.Size = new System.Drawing.Size(205, 24);
            this.txtFromStreetComing.TabIndex = 300;
            this.txtFromStreetComing.TabStop = false;
            this.txtFromStreetComing.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromStreetComing_KeyDown);
            // 
            // txtFromFlightDoorNo
            // 
            this.txtFromFlightDoorNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromFlightDoorNo.Location = new System.Drawing.Point(114, 124);
            this.txtFromFlightDoorNo.MaxLength = 100;
            this.txtFromFlightDoorNo.Name = "txtFromFlightDoorNo";
            this.txtFromFlightDoorNo.Size = new System.Drawing.Size(205, 24);
            this.txtFromFlightDoorNo.TabIndex = 2;
            this.txtFromFlightDoorNo.TabStop = false;
            this.txtFromFlightDoorNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromFlightDoorNo_KeyDown);
            // 
            // lblFromStreetComing
            // 
            this.lblFromStreetComing.BackColor = this.pnlMain.BackColor;
            this.lblFromStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromStreetComing.Location = new System.Drawing.Point(7, 154);
            this.lblFromStreetComing.Name = "lblFromStreetComing";
            this.lblFromStreetComing.Size = new System.Drawing.Size(88, 22);
            this.lblFromStreetComing.TabIndex = 182;
            this.lblFromStreetComing.Text = "From Street";
            // 
            // lblFromDoorFlightNo
            // 
            this.lblFromDoorFlightNo.BackColor = this.pnlMain.BackColor;
            this.lblFromDoorFlightNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDoorFlightNo.Location = new System.Drawing.Point(7, 125);
            this.lblFromDoorFlightNo.Name = "lblFromDoorFlightNo";
            this.lblFromDoorFlightNo.Size = new System.Drawing.Size(56, 22);
            this.lblFromDoorFlightNo.TabIndex = 180;
            this.lblFromDoorFlightNo.Text = "Door #";
            // 
            // txtToAddress
            // 
            this.txtToAddress.BackColor = System.Drawing.Color.White;
            this.txtToAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToAddress.DefaultHeight = 0;
            this.txtToAddress.DefaultWidth = 0;
            this.txtToAddress.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtToAddress.ForceListBoxToUpdate = false;
            this.txtToAddress.FormerValue = "";
            this.txtToAddress.Location = new System.Drawing.Point(496, 64);
            this.txtToAddress.Multiline = true;
            this.txtToAddress.Name = "txtToAddress";
            // 
            // 
            // 
            this.txtToAddress.RootElement.StretchVertically = true;
            this.txtToAddress.SelectedItem = null;
            this.txtToAddress.Size = new System.Drawing.Size(200, 80);
            this.txtToAddress.TabIndex = 3;
            this.txtToAddress.TabStop = false;
            this.txtToAddress.Values = null;
            this.txtToAddress.Enter += new System.EventHandler(this.txtToAddress_Enter);
            this.txtToAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToAddress_KeyDown);
            this.txtToAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromAddress_KeyPress);
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtToAddress.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtToAddress.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).Width = 1F;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // lblFromLoc
            // 
            this.lblFromLoc.BackColor = this.pnlMain.BackColor;
            this.lblFromLoc.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblFromLoc.Location = new System.Drawing.Point(6, 66);
            this.lblFromLoc.Name = "lblFromLoc";
            this.lblFromLoc.Size = new System.Drawing.Size(111, 21);
            this.lblFromLoc.TabIndex = 130;
            this.lblFromLoc.Text = "From Location";
            // 
            // ddlFromLocType
            // 
            this.ddlFromLocType.BackColor = System.Drawing.Color.White;
            this.ddlFromLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlFromLocType.Location = new System.Drawing.Point(383, 470);
            this.ddlFromLocType.Name = "ddlFromLocType";
            // 
            // 
            // 
            this.ddlFromLocType.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlFromLocType.Size = new System.Drawing.Size(136, 21);
            this.ddlFromLocType.TabIndex = 500;
            this.ddlFromLocType.TabStop = false;
            this.ddlFromLocType.Visible = false;
            this.ddlFromLocType.SelectedIndexChanged += new System.EventHandler(this.ddlFromLocType_SelectedIndexChanged);
            // 
            // ddlFromLocation
            // 
            this.ddlFromLocation.BackColor = System.Drawing.Color.White;
            this.ddlFromLocation.Location = new System.Drawing.Point(383, 416);
            this.ddlFromLocation.Name = "ddlFromLocation";
            // 
            // 
            // 
            this.ddlFromLocation.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlFromLocation.Size = new System.Drawing.Size(161, 20);
            this.ddlFromLocation.TabIndex = 306;
            this.ddlFromLocation.TabStop = false;
            // 
            // ddlToLocType
            // 
            this.ddlToLocType.BackColor = System.Drawing.Color.White;
            this.ddlToLocType.Location = new System.Drawing.Point(386, 497);
            this.ddlToLocType.Name = "ddlToLocType";
            // 
            // 
            // 
            this.ddlToLocType.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlToLocType.Size = new System.Drawing.Size(131, 20);
            this.ddlToLocType.TabIndex = 501;
            this.ddlToLocType.TabStop = false;
            this.ddlToLocType.Visible = false;
            this.ddlToLocType.SelectedIndexChanged += new System.EventHandler(this.ddlToLocType_SelectedIndexChanged);
            // 
            // lblToLoc
            // 
            this.lblToLoc.BackColor = this.pnlMain.BackColor;
            this.lblToLoc.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblToLoc.Location = new System.Drawing.Point(392, 64);
            this.lblToLoc.Name = "lblToLoc";
            this.lblToLoc.Size = new System.Drawing.Size(91, 21);
            this.lblToLoc.TabIndex = 134;
            this.lblToLoc.Text = "To Location";
            // 
            // txtToStreetComing
            // 
            this.txtToStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToStreetComing.Location = new System.Drawing.Point(496, 152);
            this.txtToStreetComing.MaxLength = 100;
            this.txtToStreetComing.Name = "txtToStreetComing";
            this.txtToStreetComing.Size = new System.Drawing.Size(206, 24);
            this.txtToStreetComing.TabIndex = 301;
            this.txtToStreetComing.TabStop = false;
            this.txtToStreetComing.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToStreetComing_KeyDown);
            // 
            // ddlToLocation
            // 
            this.ddlToLocation.BackColor = System.Drawing.Color.White;
            this.ddlToLocation.Location = new System.Drawing.Point(383, 443);
            this.ddlToLocation.Name = "ddlToLocation";
            // 
            // 
            // 
            this.ddlToLocation.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlToLocation.Size = new System.Drawing.Size(134, 20);
            this.ddlToLocation.TabIndex = 305;
            this.ddlToLocation.TabStop = false;
            // 
            // txtToPostCode
            // 
            this.txtToPostCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtToPostCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtToPostCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPostCode.Location = new System.Drawing.Point(496, 95);
            this.txtToPostCode.MaxLength = 100;
            this.txtToPostCode.Name = "txtToPostCode";
            this.txtToPostCode.Size = new System.Drawing.Size(206, 24);
            this.txtToPostCode.TabIndex = 4;
            this.txtToPostCode.TabStop = false;
            this.txtToPostCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToPostCode_KeyDown);
            this.txtToPostCode.Validated += new System.EventHandler(this.txtToPostCode_Validated);
            // 
            // lblToStreetComing
            // 
            this.lblToStreetComing.BackColor = this.pnlMain.BackColor;
            this.lblToStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToStreetComing.Location = new System.Drawing.Point(392, 154);
            this.lblToStreetComing.Name = "lblToStreetComing";
            this.lblToStreetComing.Size = new System.Drawing.Size(71, 22);
            this.lblToStreetComing.TabIndex = 187;
            this.lblToStreetComing.Text = "To Street";
            // 
            // txtToFlightDoorNo
            // 
            this.txtToFlightDoorNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToFlightDoorNo.Location = new System.Drawing.Point(496, 124);
            this.txtToFlightDoorNo.MaxLength = 100;
            this.txtToFlightDoorNo.Name = "txtToFlightDoorNo";
            this.txtToFlightDoorNo.Size = new System.Drawing.Size(206, 24);
            this.txtToFlightDoorNo.TabIndex = 4;
            this.txtToFlightDoorNo.TabStop = false;
            this.txtToFlightDoorNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToFlightDoorNo_KeyDown);
            // 
            // lblToDoorFlightNo
            // 
            this.lblToDoorFlightNo.BackColor = this.pnlMain.BackColor;
            this.lblToDoorFlightNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDoorFlightNo.Location = new System.Drawing.Point(393, 125);
            this.lblToDoorFlightNo.Name = "lblToDoorFlightNo";
            this.lblToDoorFlightNo.Size = new System.Drawing.Size(56, 22);
            this.lblToDoorFlightNo.TabIndex = 186;
            this.lblToDoorFlightNo.Text = "Door #";
            // 
            // btnSelectVia
            // 
            this.btnSelectVia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnSelectVia.Location = new System.Drawing.Point(324, 74);
            this.btnSelectVia.Name = "btnSelectVia";
            this.btnSelectVia.Size = new System.Drawing.Size(62, 65);
            this.btnSelectVia.TabIndex = 0;
            this.btnSelectVia.Text = "+Via (0) [ F2 ]";
            this.btnSelectVia.TextWrap = true;
            this.btnSelectVia.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radToggleButton1_ToggleStateChanged);
            ((Telerik.WinControls.UI.RadToggleButtonElement)(this.btnSelectVia.GetChildAt(0))).Text = "+Via (0) [ F2 ]";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelectVia.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelectVia.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelectVia.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1214, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = " Booking";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBookingNo
            // 
            this.txtBookingNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookingNo.Location = new System.Drawing.Point(59, 10);
            this.txtBookingNo.Name = "txtBookingNo";
            this.txtBookingNo.Size = new System.Drawing.Size(112, 22);
            this.txtBookingNo.TabIndex = 108;
            this.txtBookingNo.Text = "Not Allocated";
            this.txtBookingNo.Visible = false;
            // 
            // ddlBookingType
            // 
            this.ddlBookingType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlBookingType.Location = new System.Drawing.Point(993, 7);
            this.ddlBookingType.Name = "ddlBookingType";
            this.ddlBookingType.Size = new System.Drawing.Size(106, 26);
            this.ddlBookingType.TabIndex = 218;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.ddlBookingType.GetChildAt(0).GetChildAt(0).GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.ddlBookingType.GetChildAt(0).GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.DodgerBlue;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.ddlBookingType.GetChildAt(0).GetChildAt(0).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblBookingType
            // 
            this.lblBookingType.AutoSize = false;
            this.lblBookingType.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblBookingType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookingType.ForeColor = System.Drawing.Color.White;
            this.lblBookingType.Location = new System.Drawing.Point(940, 9);
            this.lblBookingType.Name = "lblBookingType";
            // 
            // 
            // 
            this.lblBookingType.RootElement.ForeColor = System.Drawing.Color.White;
            this.lblBookingType.Size = new System.Drawing.Size(57, 22);
            this.lblBookingType.TabIndex = 219;
            this.lblBookingType.Text = "Type";
            // 
            // lblBookedBy
            // 
            this.lblBookedBy.BackColor = System.Drawing.Color.Lavender;
            this.lblBookedBy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBookedBy.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lblBookedBy.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblBookedBy.Location = new System.Drawing.Point(0, 722);
            this.lblBookedBy.Name = "lblBookedBy";
            this.lblBookedBy.Size = new System.Drawing.Size(1214, 20);
            this.lblBookedBy.TabIndex = 220;
            // 
            // ddlSubCompany
            // 
            this.ddlSubCompany.BackColor = System.Drawing.Color.White;
            this.ddlSubCompany.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSubCompany.FormattingEnabled = true;
            this.ddlSubCompany.Location = new System.Drawing.Point(7, 6);
            this.ddlSubCompany.Name = "ddlSubCompany";
            this.ddlSubCompany.Size = new System.Drawing.Size(246, 27);
            this.ddlSubCompany.TabIndex = 0;
            // 
            // btnTrackDriver
            // 
            this.btnTrackDriver.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnTrackDriver.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnTrackDriver.Location = new System.Drawing.Point(261, 4);
            this.btnTrackDriver.Name = "btnTrackDriver";
            this.btnTrackDriver.Size = new System.Drawing.Size(99, 33);
            this.btnTrackDriver.TabIndex = 211;
            this.btnTrackDriver.Text = "Track Driver";
            this.btnTrackDriver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTrackDriver.TextWrap = true;
            this.btnTrackDriver.Visible = false;
            this.btnTrackDriver.Click += new System.EventHandler(this.btnTrackDriver_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnTrackDriver.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnTrackDriver.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnTrackDriver.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnTrackDriver.GetChildAt(0))).Text = "Track Driver";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTrackDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTrackDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTrackDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPasteBooking
            // 
            this.btnPasteBooking.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPasteBooking.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnPasteBooking.Location = new System.Drawing.Point(361, 3);
            this.btnPasteBooking.Name = "btnPasteBooking";
            this.btnPasteBooking.Size = new System.Drawing.Size(103, 33);
            this.btnPasteBooking.TabIndex = 0;
            this.btnPasteBooking.Text = "Paste Booking";
            this.btnPasteBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPasteBooking.TextWrap = true;
            this.btnPasteBooking.Click += new System.EventHandler(this.btnPasteBooking_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteBooking.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteBooking.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteBooking.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteBooking.GetChildAt(0))).Text = "Paste Booking";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPasteBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPasteBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPasteBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(1135, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(89, 40);
            this.btnExit.TabIndex = 273;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 742);
            this.Controls.Add(this.btnPasteBooking);
            this.Controls.Add(this.btnTrackDriver);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.ddlSubCompany);
            this.Controls.Add(this.lblBookedBy);
            this.Controls.Add(this.ddlBookingType);
            this.Controls.Add(this.lblBookingType);
            this.Controls.Add(this.txtBookingNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBooking";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBooking_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAttributes)).EndInit();
            this.pnljourney.ResumeLayout(false);
            this.pnljourney.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPermanentCustNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickAccountBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkQuotedPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPaymentReference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPaymentRef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFaresPostedFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDetailMap)).EndInit();
            this.pnlOtherCharges.ResumeLayout(false);
            this.pnlOtherCharges.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblTip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTipAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDrvWaitingMins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCongChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeetCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccWaitingCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitingChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccParkingCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParkingChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPaymentMode)).EndInit();
            this.pnlPaymentMode.ResumeLayout(false);
            this.pnlPaymentMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSecondaryPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCashPaymentFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCommissionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDriverCommission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCommissionWise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPaymentHeading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBookingFees)).EndInit();
            this.pnlBookingFees.ResumeLayout(false);
            this.pnlBookingFees.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnBookingFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnCustFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBookingFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVehicleNo)).EndInit();
            this.pnlAutoDespatch.ResumeLayout(false);
            this.pnlAutoDespatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optSMSThirdParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optSMSGsm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBidding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisablePassengerSMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisableDriverSMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoDespatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeforeMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCustomer)).EndInit();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCustomerLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerPhoneNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerMobileNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_notes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            this.radPanel3.ResumeLayout(false);
            this.radPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintJob)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancelBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnConfirmBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalLuggages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLuggages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPassengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMultiVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicleType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCompanyRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickupDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialRequirements)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkQuotation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReverse)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAccountCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewMapReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMultiBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromStreetComing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromFlightDoorNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromStreetComing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromDoorFlightNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromLoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromLocType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToLocType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToLoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToStreetComing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToStreetComing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToFlightDoorNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDoorFlightNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectVia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookingNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlBookingType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBookingType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTrackDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPasteBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel pnlMain;
        private Telerik.WinControls.UI.RadLabel lblToLoc;
        private RadComboBox ddlToLocation;
        private RadComboBox ddlToLocType;
        private Telerik.WinControls.UI.RadLabel lblFromLoc;
        private RadComboBox ddlFromLocation;
        private RadComboBox ddlFromLocType;
        private Telerik.WinControls.UI.RadLabel lblReturnPickupTime;
        private Telerik.WinControls.UI.RadLabel lblReturnPickupDate;
        private UI.MyDatePicker dtpReturnPickupTime;
        private UI.MyDatePicker dtpReturnPickupDate;
        private Telerik.WinControls.UI.RadLabel lblReturnDriver;
        private UI.MyDropDownList ddlReturnDriver;
        private Telerik.WinControls.UI.RadLabel lblFromDoorFlightNo;
        private Telerik.WinControls.UI.RadTextBox txtFromPostCode;
        private Telerik.WinControls.UI.RadTextBox txtFromStreetComing;
        private Telerik.WinControls.UI.RadTextBox txtFromFlightDoorNo;
        private Telerik.WinControls.UI.RadLabel lblFromStreetComing;
        private Telerik.WinControls.UI.RadTextBox txtToStreetComing;
        private Telerik.WinControls.UI.RadTextBox txtToFlightDoorNo;
        private Telerik.WinControls.UI.RadLabel lblToStreetComing;
        private Telerik.WinControls.UI.RadLabel lblToDoorFlightNo;
        private Telerik.WinControls.UI.RadTextBox txtToPostCode;
        private Telerik.WinControls.UI.RadToggleButton btnSelectVia;
        private UI.AutoCompleteTextBox txtFromAddress;
        private UI.AutoCompleteTextBox txtToAddress;
        private Telerik.WinControls.UI.RadButton btnSearch;
        private Telerik.WinControls.UI.RadButton btnMultiBooking;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadToggleButton btnBase;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlAirport;
        private UI.MyDropDownList ddlAirPorts;
        private Telerik.WinControls.UI.RadButton btn_pickAirport;
        private System.Windows.Forms.Label label4;
        private UI.MyDropDownList ddlAirportType;
        private System.Windows.Forms.Panel pnlStations;
        private UI.MyDropDownList ddlStations;
        private Telerik.WinControls.UI.RadButton btnPickStations;
        private System.Windows.Forms.Label label5;
        private UI.MyDropDownList ddlStationType;
        private System.Windows.Forms.Panel pnlHospital;
        private UI.MyDropDownList ddlHospitals;
        private Telerik.WinControls.UI.RadButton btnPickHospital;
        private System.Windows.Forms.Label label6;
        private UI.MyDropDownList ddlHospitalType;



        private Telerik.WinControls.UI.RadCheckBox chkReverse;
        private System.Windows.Forms.Panel pnlOrderNo;
        private Telerik.WinControls.UI.RadLabel lblOrderNo;
        private Telerik.WinControls.UI.RadTextBox txtPupilNo;
        private Telerik.WinControls.UI.RadTextBox txtOrderNo;
        private Telerik.WinControls.UI.RadLabel lblPupilNo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnDespatchView;
        private Telerik.WinControls.UI.RadLabel txtBookingNo;
        private Telerik.WinControls.UI.RadCheckBox chkQuotation;
        private Telerik.WinControls.UI.RadButton btnSendEmail;


        private Telerik.WinControls.UI.RadDropDownList ddlBookingType;
        private Telerik.WinControls.UI.RadLabel lblBookingType;

        private System.Windows.Forms.Panel pnlComcab;
        private Telerik.WinControls.UI.RadLabel radLabel29;
        private Telerik.WinControls.UI.RadSpinEditor numComcab_WaitingMin;
        private Telerik.WinControls.UI.RadLabel radLabel31;
        private Telerik.WinControls.UI.RadSpinEditor numComcab_ExtraMile;
        private Telerik.WinControls.UI.RadLabel radLabel28;
        private Telerik.WinControls.UI.RadSpinEditor numComcab_Account;
        private Telerik.WinControls.UI.RadLabel radLabel8;
        private Telerik.WinControls.UI.RadSpinEditor numComcab_Cash;
        private System.Windows.Forms.Panel pnlAccpassword;
        private Telerik.WinControls.UI.RadTextBox txtAccPassword;
        private Telerik.WinControls.UI.RadLabel radLabel33;
        private System.Windows.Forms.Label pnlReturnJobNo;
        private Telerik.WinControls.UI.RadPanel pnlVia;
        private UI.AutoCompleteTextBox txtViaAddress;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnAddVia;
        private UI.MyDropDownList ddlViaLocation;
        private Telerik.WinControls.UI.RadGridView grdVia;
        private UI.AutoCompleteTextBox txtviaPostCode;
        private Telerik.WinControls.UI.RadLabel lblViaLoc;
        private Telerik.WinControls.UI.RadLabel lblFromViaPoint;
        private UI.MyDropDownList ddlViaFromLocType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBookedBy;
        private RadComboBox ddlReturnFromAirport;
        private Telerik.WinControls.UI.RadLabel lblReturnFromAirport;
        private System.Windows.Forms.ComboBox ddlSubCompany;
       
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnNearestDrv;
        private System.Windows.Forms.ComboBox ddlPickupPlot;
        private Telerik.WinControls.UI.RadPanel radPanel1;

        private Telerik.WinControls.UI.RadLabel lblCompanyPrice;
        private Telerik.WinControls.UI.RadSpinEditor numCompanyFares;

        private Telerik.WinControls.UI.RadLabel lblReturnCompanyPrice;
        private Telerik.WinControls.UI.RadSpinEditor numReturnCompanyFares;

        private Telerik.WinControls.UI.RadLabel lblRetFares;
        private Telerik.WinControls.UI.RadSpinEditor numReturnFare;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadSpinEditor numFareRate;
        private Telerik.WinControls.UI.RadButton btnDetailMap;
        private System.Windows.Forms.Panel pnlOtherCharges;
        private Telerik.WinControls.UI.RadLabel radLabel18;
        private Telerik.WinControls.UI.RadSpinEditor numTotalChrgs;
        private Telerik.WinControls.UI.RadLabel radLabel20;
        private Telerik.WinControls.UI.RadSpinEditor numCongChrgs;
        private Telerik.WinControls.UI.RadLabel radLabel16;
        private Telerik.WinControls.UI.RadSpinEditor numMeetCharges;
        private Telerik.WinControls.UI.RadLabel radLabel17;
        private Telerik.WinControls.UI.RadSpinEditor numExtraChrgs;
        private Telerik.WinControls.UI.RadLabel lblAccWaitingCharges;
        private Telerik.WinControls.UI.RadSpinEditor numWaitingChrgs;
        private Telerik.WinControls.UI.RadLabel lblAccParkingCharges;
        private Telerik.WinControls.UI.RadSpinEditor numParkingChrgs;
        private Telerik.WinControls.UI.RadLabel pnlPaymentMode;
        private Telerik.WinControls.UI.RadButton btnPayment;
        private UI.MyDropDownList ddlCommissionType;
        private Telerik.WinControls.UI.RadSpinEditor numDriverCommission;
        private Telerik.WinControls.UI.RadCheckBox chkIsCommissionWise;
        private Telerik.WinControls.UI.RadLabel radLabel7;
        private UI.MyDropDownList ddlPaymentType;
        private Telerik.WinControls.UI.RadButton btnPickFares;
        private System.Windows.Forms.Label lblMap;
        private Telerik.WinControls.UI.RadLabel lblPaymentHeading;
        private System.Windows.Forms.Label pnlFares;
        private System.Windows.Forms.Label lblDropOffPlot;
        private System.Windows.Forms.Label lblPickupPlot;
        private Telerik.WinControls.UI.RadButton btn_notes;
        private Telerik.WinControls.UI.RadLabel txtVehicleNo;
        private System.Windows.Forms.Panel pnlAutoDespatch;
        private System.Windows.Forms.Button btnSms;
        private Telerik.WinControls.UI.RadCheckBox chkDisablePassengerSMS;
        private Telerik.WinControls.UI.RadCheckBox chkDisableDriverSMS;
        private Telerik.WinControls.UI.RadCheckBox chkAutoDespatch;
        private Telerik.WinControls.UI.RadSpinEditor numBeforeMinutes;
        private Telerik.WinControls.UI.RadLabel pnlCustomer;
        private Telerik.WinControls.UI.RadButton btnCustomerLister;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadLabel radLabel21;
        private Telerik.WinControls.UI.RadLabel radLabel19;
        private Telerik.WinControls.UI.RadTextBox txtCustomerPhoneNo;
        private Telerik.WinControls.UI.RadTextBox txtCustomerMobileNo;
        private Telerik.WinControls.UI.RadTextBox ddlCustomerName;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadSplitButton btnPrintJob;
        private Telerik.WinControls.UI.RadButton btnCancelBooking;
        private Telerik.WinControls.UI.RadButton btnExitForm;

        private Telerik.WinControls.UI.RadLabel radLabel27;
        private Telerik.WinControls.UI.RadButton btnSaveNew;
        private Telerik.WinControls.UI.RadButton btnConfirmBooking;
        private Telerik.WinControls.UI.RadSpinEditor numTotalLuggages;
        private Telerik.WinControls.UI.RadLabel lblLuggages;
        private Telerik.WinControls.UI.RadLabel lblPassengers;
        private Telerik.WinControls.UI.RadSpinEditor num_TotalPassengers;
        private Telerik.WinControls.UI.RadLabel lblDepartment;
        private UI.MyDropDownList ddlDepartment;
        private Telerik.WinControls.UI.RadButton btnMultiVehicle;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private UI.MyDropDownList ddlVehicleType;
        private Telerik.WinControls.UI.RadCheckBox chkIsCompanyRates;
        private Telerik.WinControls.UI.RadLabel radLabel14;
        private RadComboBox ddlCompany;
        private UI.MyDatePicker dtpPickupDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private UI.MyDatePicker dtpPickupTime;
        private Telerik.WinControls.UI.RadLabel lblPickupTime;
        private UI.MyDropDownList ddlDriver;
        private Telerik.WinControls.UI.RadLabel radLabel25;
        private Telerik.WinControls.UI.RadLabel radLabel22;
        private Telerik.WinControls.UI.RadTextBox txtSpecialRequirements;
        private Telerik.WinControls.UI.RadMenuItem btnJobReport;
        private Telerik.WinControls.UI.RadMenuItem btnLogDetail;
        private Telerik.WinControls.UI.RadMenuItem menu_JobReceipt;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox txtEmail;
        private Telerik.WinControls.UI.RadButton btnConfirmationSMS;
        private Telerik.WinControls.UI.RadLabel pnlBookingFees;
        private Telerik.WinControls.UI.RadSpinEditor numBookingFee;
        private Telerik.WinControls.UI.RadLabel radLabel32;
        private Telerik.WinControls.UI.RadSpinEditor numAgentCommission;
        private Telerik.WinControls.UI.RadCheckBox chkTakenByAgent;
        private Telerik.WinControls.UI.RadLabel radLabel34;
        private Telerik.WinControls.UI.RadSpinEditor numAgentCommissionPercent;
        private UI.MyDropDownList ddlAgentCommissionType;
        private System.Windows.Forms.PictureBox pic_Signature;
        private System.Windows.Forms.Label txtCourierSignedOn;
        private System.Windows.Forms.Label lblCourierHeader;
        private UI.MyDatePicker dtpFlightDepDate;
        private Telerik.WinControls.UI.RadLabel lblFlightDepDateTime;
        private UI.MyDatePicker dtpFlightDepTime;
        private Telerik.WinControls.UI.RadLabel lblFlightDepTime;
        private Telerik.WinControls.UI.RadButton btnAddGroup;
        private Telerik.WinControls.UI.RadButton btnViewGroup;
        private Telerik.WinControls.UI.RadLabel lblPickGroup;
        private Telerik.WinControls.UI.RadTextBox txtGroupJobNo;
        private Telerik.WinControls.UI.RadButton btnClearGroup;
        private Telerik.WinControls.UI.RadLabel lblRoomNo;
        private Telerik.WinControls.UI.RadTextBox txtRoomNo;
        private Telerik.WinControls.UI.RadLabel txtFaresPostedFrom;


        private Telerik.WinControls.UI.RadLabel lblAccountBookedBy;
        private Telerik.WinControls.UI.RadTextBox txtAccountBookedBy;
        private Telerik.WinControls.UI.RadLabel lblReturnSpecialReq;
        private Telerik.WinControls.UI.RadTextBox txtReturnSpecialReq;
        private Telerik.WinControls.UI.RadLabel lblDirection;
        private System.Windows.Forms.ComboBox ddlDirection;
        private Telerik.WinControls.UI.RadButton btnViewMapReport;
        private Telerik.WinControls.UI.RadButton btnSendInvoice;
        private Telerik.WinControls.UI.RadButton btnPickAccountBooking;
        private System.Windows.Forms.ComboBox ddlbabyseat2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ddlBabyseat1;
        private System.Windows.Forms.Label label8;
        private Telerik.WinControls.UI.RadLabel lblReturnVehicle;
        private UI.MyDropDownList ddlReturnVehicleType;
        private Telerik.WinControls.UI.RadButton btnTrackDriver;
        private Telerik.WinControls.UI.RadButton btnPasteBooking;
        private System.Windows.Forms.Button btnComplaint;
        private System.Windows.Forms.Button btnLostProperty;
        private Telerik.WinControls.UI.RadLabel lblEscort;

        private Telerik.WinControls.UI.RadSpinEditor numEscortPrice;
        private Telerik.WinControls.UI.RadLabel lblEscortPrice;
        private Telerik.WinControls.UI.RadTextBox txtPaymentReference;
        private Telerik.WinControls.UI.RadLabel lblPaymentRef;
        private UI.AutoCompleteTextBox txtReturnTo;
        private UI.AutoCompleteTextBox txtReturnFrom;
        private Telerik.WinControls.UI.RadSpinEditor numCashPaymentFares;
        private Telerik.WinControls.UI.RadCheckBox chkSecondaryPaymentType;
        private Telerik.WinControls.UI.RadCheckBox chkBidding;
        private Telerik.WinControls.UI.RadSpinEditor numReturnBookingFee;
        private Telerik.WinControls.UI.RadLabel lblReturnCustFare;
        private Telerik.WinControls.UI.RadButton btnExit;
        private System.Windows.Forms.Button btnPlayRecording;
        private RadButton btnSetFares;
        private RadSpinEditor numDrvWaitingMins;
        private RadLabel radLabel4;
        private System.Windows.Forms.Panel pnlHotels;
        private UI.MyDropDownList ddlHotels;
        private Telerik.WinControls.UI.RadButton btnPickHotels;
        private System.Windows.Forms.Label labelHotel;
        private UI.MyDropDownList ddlHotelsType;
        private System.Windows.Forms.Panel pnltowns;
        private UI.MyDropDownList ddltowns;
        private Telerik.WinControls.UI.RadButton btnPicktowns;
        private System.Windows.Forms.Label labeltowns;
        private UI.MyDropDownList ddltownsType;
        private RadLabel lblTip;
        private RadSpinEditor numTipAmount;
        private RadLabel lblJourneyTime;
        private RadSpinEditor numJourneyTime;
        private RadCheckBox chkQuotedPrice;
        private RadCheckBox chkPermanentCustNotes;
        //private RadGridView grdPickupDateTime;
        private RadRadioButton optSMSThirdParty;
        private RadRadioButton optSMSGsm;
        private System.Windows.Forms.ComboBox ddlDropOffPlot;
        private RadButton btnAccountCode;
        //NC
        private System.Windows.Forms.DataGridView grdPickupDateTime;
        private System.Windows.Forms.TabControl radPageView1;
        private System.Windows.Forms.TabPage tabCurrentBooking;
        private System.Windows.Forms.TabPage tabNearestDrivers;

        private RadButton btnRefreshNearestDrivers;
        private System.Windows.Forms.Panel pnljourney;
        private System.Windows.Forms.RadioButton opt_one;
        private System.Windows.Forms.RadioButton opt_waitreturn;
        private System.Windows.Forms.RadioButton opt_return;
        private RadButton btnAttributes;
        //

    }
}