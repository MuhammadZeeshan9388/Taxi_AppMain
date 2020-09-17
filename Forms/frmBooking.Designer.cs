using System;

using System.Windows.Forms;
namespace Taxi_AppMain
{
    partial class frmBooking
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnInfo = new System.Windows.Forms.Button();
            this.optSMSThirdParty = new System.Windows.Forms.RadioButton();
            this.btnExcludeDrivers = new System.Windows.Forms.Button();
            this.optSMSGsm = new System.Windows.Forms.RadioButton();
            this.chkAllocateDriver = new System.Windows.Forms.CheckBox();
            this.chkSecondaryPaymentType = new System.Windows.Forms.CheckBox();
            this.btnAttributes = new System.Windows.Forms.Button();
            this.pnljourney = new System.Windows.Forms.GroupBox();
            this.opt_one = new System.Windows.Forms.RadioButton();
            this.opt_waitreturn = new System.Windows.Forms.RadioButton();
            this.opt_return = new System.Windows.Forms.RadioButton();
            this.chkPermanentCustNotes = new System.Windows.Forms.CheckBox();
            this.txtReturnFrom = new UIX.AutoCompleteTextBox();
            this.txtReturnTo = new UIX.AutoCompleteTextBox();
            this.btnPickAccountBooking = new System.Windows.Forms.Button();
            this.ddlDropOffPlot = new System.Windows.Forms.ComboBox();
            this.ddlPickupPlot = new System.Windows.Forms.ComboBox();
            this.radPanel1 = new System.Windows.Forms.Panel();
            this.chkQuotedPrice = new System.Windows.Forms.CheckBox();
            this.txtPaymentReference = new System.Windows.Forms.TextBox();
            this.lblPaymentRef = new System.Windows.Forms.Label();
            this.txtFaresPostedFrom = new System.Windows.Forms.Label();
            this.radLabel5 = new System.Windows.Forms.Label();
            this.numFareRate = new System.Windows.Forms.NumericUpDown();
            this.btnDetailMap = new System.Windows.Forms.Button();
            this.pnlOtherCharges = new System.Windows.Forms.Panel();
            this.lblTip = new System.Windows.Forms.Label();
            this.numTipAmount = new System.Windows.Forms.NumericUpDown();
            this.radLabel4 = new System.Windows.Forms.Label();
            this.numDrvWaitingMins = new System.Windows.Forms.NumericUpDown();
            this.radLabel18 = new System.Windows.Forms.Label();
            this.numTotalChrgs = new System.Windows.Forms.NumericUpDown();
            this.radLabel20 = new System.Windows.Forms.Label();
            this.numCongChrgs = new System.Windows.Forms.NumericUpDown();
            this.radLabel16 = new System.Windows.Forms.Label();
            this.numMeetCharges = new System.Windows.Forms.NumericUpDown();
            this.radLabel17 = new System.Windows.Forms.Label();
            this.numExtraChrgs = new System.Windows.Forms.NumericUpDown();
            this.lblAccWaitingCharges = new System.Windows.Forms.Label();
            this.numWaitingChrgs = new System.Windows.Forms.NumericUpDown();
            this.lblAccParkingCharges = new System.Windows.Forms.Label();
            this.numParkingChrgs = new System.Windows.Forms.NumericUpDown();
            this.pnlPaymentMode = new System.Windows.Forms.Label();
            this.numCashPaymentFares = new System.Windows.Forms.NumericUpDown();
            this.btnPayment = new System.Windows.Forms.Button();
            this.chkIsCommissionWise = new System.Windows.Forms.CheckBox();
            this.ddlCommissionType = new UIX.MyDropDownList();
            this.numDriverCommission = new System.Windows.Forms.NumericUpDown();
            this.radLabel7 = new System.Windows.Forms.Label();
            this.ddlPaymentType = new UIX.MyDropDownList();
            this.btnPickFares = new System.Windows.Forms.Button();
            this.lblMap = new System.Windows.Forms.Label();
            this.lblPaymentHeading = new System.Windows.Forms.Label();
            this.pnlFares = new System.Windows.Forms.Label();
            this.pnlBookingFees = new System.Windows.Forms.Label();
            this.numReturnBookingFee = new System.Windows.Forms.NumericUpDown();
            this.lblReturnCustFare = new System.Windows.Forms.Label();
            this.numBookingFee = new System.Windows.Forms.NumericUpDown();
            this.lblDropOffPlot = new System.Windows.Forms.Label();
            this.lblPickupPlot = new System.Windows.Forms.Label();
            this.txtVehicleNo = new System.Windows.Forms.Label();
            this.pnlAutoDespatch = new System.Windows.Forms.Panel();
            this.chkLead = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numLead = new System.Windows.Forms.NumericUpDown();
            this.btnSms = new System.Windows.Forms.Button();
            this.chkBidding = new System.Windows.Forms.CheckBox();
            this.chkDisablePassengerSMS = new System.Windows.Forms.CheckBox();
            this.chkDisableDriverSMS = new System.Windows.Forms.CheckBox();
            this.chkAutoDespatch = new System.Windows.Forms.CheckBox();
            this.numBeforeMinutes = new System.Windows.Forms.NumericUpDown();
            this.pnlCustomer = new System.Windows.Forms.Label();
            this.radLabel2 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnCustomerLister = new System.Windows.Forms.Button();
            this.radLabel6 = new System.Windows.Forms.Label();
            this.radLabel21 = new System.Windows.Forms.Label();
            this.radLabel19 = new System.Windows.Forms.Label();
            this.txtCustomerPhoneNo = new System.Windows.Forms.TextBox();
            this.txtCustomerMobileNo = new System.Windows.Forms.TextBox();
            this.ddlCustomerName = new System.Windows.Forms.TextBox();
            this.btn_notes = new System.Windows.Forms.Button();
            this.radPanel3 = new System.Windows.Forms.Panel();
            this.btnJobInformation = new System.Windows.Forms.Button();
            this.btnCancelBooking = new System.Windows.Forms.Button();
            this.btnExitForm = new System.Windows.Forms.Button();
            this.radLabel27 = new System.Windows.Forms.Label();
            this.btnSaveNew = new System.Windows.Forms.Button();
            this.numTotalLuggages = new System.Windows.Forms.NumericUpDown();
            this.lblLuggages = new System.Windows.Forms.Label();
            this.lblPassengers = new System.Windows.Forms.Label();
            this.num_TotalPassengers = new System.Windows.Forms.NumericUpDown();
            this.btnMultiVehicle = new System.Windows.Forms.Button();
            this.radLabel11 = new System.Windows.Forms.Label();
            this.ddlVehicleType = new UIX.MyDropDownList();
            this.chkIsCompanyRates = new System.Windows.Forms.CheckBox();
            this.radLabel14 = new System.Windows.Forms.Label();
            this.ddlCompany = new System.Windows.Forms.ComboBox();
            this.dtpPickupDate = new UIX.MyDatePicker();
            this.radLabel3 = new System.Windows.Forms.Label();
            this.dtpPickupTime = new UIX.MyDatePicker();
            this.lblPickupTime = new System.Windows.Forms.Label();
            this.ddlDriver = new UIX.MyDropDownList();
            this.radLabel25 = new System.Windows.Forms.Label();
            this.txtSpecialRequirements = new System.Windows.Forms.TextBox();
            this.pnlReturnJobNo = new System.Windows.Forms.Label();
            this.chkQuotation = new System.Windows.Forms.CheckBox();
            this.chkReverse = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPasteBooking = new System.Windows.Forms.Button();
            this.btnDespatchView = new System.Windows.Forms.Button();
            this.btnAccountCode = new System.Windows.Forms.Button();
            this.btnViewMapReport = new System.Windows.Forms.Button();
            this.btnNearestDrv = new System.Windows.Forms.Button();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.btnBase = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnMultiBooking = new System.Windows.Forms.Button();
            this.btnTrackDriver = new System.Windows.Forms.Button();
            this.txtFromAddress = new UIX.AutoCompleteTextBox();
            this.txtFromPostCode = new System.Windows.Forms.TextBox();
            this.txtFromStreetComing = new System.Windows.Forms.TextBox();
            this.txtFromFlightDoorNo = new System.Windows.Forms.TextBox();
            this.lblFromStreetComing = new System.Windows.Forms.Label();
            this.lblFromDoorFlightNo = new System.Windows.Forms.Label();
            this.txtToAddress = new UIX.AutoCompleteTextBox();
            this.lblFromLoc = new System.Windows.Forms.Label();
            this.ddlFromLocType = new System.Windows.Forms.ComboBox();
            this.ddlToLocType = new System.Windows.Forms.ComboBox();
            this.lblToLoc = new System.Windows.Forms.Label();
            this.txtToStreetComing = new System.Windows.Forms.TextBox();
            this.txtToPostCode = new System.Windows.Forms.TextBox();
            this.lblToStreetComing = new System.Windows.Forms.Label();
            this.txtToFlightDoorNo = new System.Windows.Forms.TextBox();
            this.lblToDoorFlightNo = new System.Windows.Forms.Label();
            this.btnSelectVia = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBookingNo = new System.Windows.Forms.Label();
            this.ddlBookingType = new System.Windows.Forms.ComboBox();
            this.lblBookingType = new System.Windows.Forms.Label();
            this.lblBookedBy = new System.Windows.Forms.Label();
            this.ddlSubCompany = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnljourney.SuspendLayout();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRate)).BeginInit();
            this.pnlOtherCharges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTipAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDrvWaitingMins)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCongChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeetCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitingChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParkingChrgs)).BeginInit();
            this.pnlPaymentMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCashPaymentFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDriverCommission)).BeginInit();
            this.pnlBookingFees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnBookingFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBookingFee)).BeginInit();
            this.pnlAutoDespatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeforeMinutes)).BeginInit();
            this.pnlCustomer.SuspendLayout();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalLuggages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPassengers)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.pnlMain.Controls.Add(this.btnInfo);
            this.pnlMain.Controls.Add(this.optSMSThirdParty);
            this.pnlMain.Controls.Add(this.btnExcludeDrivers);
            this.pnlMain.Controls.Add(this.optSMSGsm);
            this.pnlMain.Controls.Add(this.chkAllocateDriver);
            this.pnlMain.Controls.Add(this.chkSecondaryPaymentType);
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
            this.pnlMain.Controls.Add(this.ddlToLocType);
            this.pnlMain.Controls.Add(this.lblToLoc);
            this.pnlMain.Controls.Add(this.txtToStreetComing);
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
            // btnInfo
            // 
            this.btnInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnInfo.Font = new System.Drawing.Font("Arial Narrow", 11F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnInfo.ForeColor = System.Drawing.Color.Blue;
            this.btnInfo.Location = new System.Drawing.Point(728, 220);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(31, 23);
            this.btnInfo.TabIndex = 509;
            this.btnInfo.Text = "i";
            this.btnInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInfo.UseVisualStyleBackColor = false;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // optSMSThirdParty
            // 
            this.optSMSThirdParty.BackColor = System.Drawing.Color.Transparent;
            this.optSMSThirdParty.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSMSThirdParty.ForeColor = System.Drawing.Color.Purple;
            this.optSMSThirdParty.Location = new System.Drawing.Point(639, 415);
            this.optSMSThirdParty.Name = "optSMSThirdParty";
            this.optSMSThirdParty.Size = new System.Drawing.Size(91, 18);
            this.optSMSThirdParty.TabIndex = 237;
            this.optSMSThirdParty.Text = "By Provider";
            this.optSMSThirdParty.UseVisualStyleBackColor = false;
            this.optSMSThirdParty.Visible = false;
            // 
            // btnExcludeDrivers
            // 
            this.btnExcludeDrivers.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExcludeDrivers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExcludeDrivers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnExcludeDrivers.Location = new System.Drawing.Point(762, 314);
            this.btnExcludeDrivers.Name = "btnExcludeDrivers";
            this.btnExcludeDrivers.Size = new System.Drawing.Size(161, 45);
            this.btnExcludeDrivers.TabIndex = 504;
            this.btnExcludeDrivers.Text = "Exclude Driver(s)";
            this.btnExcludeDrivers.UseVisualStyleBackColor = false;
            this.btnExcludeDrivers.Click += new System.EventHandler(this.btnExcludeDrivers_Click);
            // 
            // optSMSGsm
            // 
            this.optSMSGsm.BackColor = System.Drawing.Color.Transparent;
            this.optSMSGsm.Checked = true;
            this.optSMSGsm.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSMSGsm.ForeColor = System.Drawing.Color.Purple;
            this.optSMSGsm.Location = new System.Drawing.Point(639, 395);
            this.optSMSGsm.Name = "optSMSGsm";
            this.optSMSGsm.Size = new System.Drawing.Size(83, 18);
            this.optSMSGsm.TabIndex = 236;
            this.optSMSGsm.TabStop = true;
            this.optSMSGsm.Text = "By GSM ";
            this.optSMSGsm.UseVisualStyleBackColor = false;
            this.optSMSGsm.Visible = false;
            // 
            // chkAllocateDriver
            // 
            this.chkAllocateDriver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.chkAllocateDriver.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.chkAllocateDriver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.chkAllocateDriver.Location = new System.Drawing.Point(360, 367);
            this.chkAllocateDriver.Name = "chkAllocateDriver";
            this.chkAllocateDriver.Size = new System.Drawing.Size(134, 22);
            this.chkAllocateDriver.TabIndex = 503;
            this.chkAllocateDriver.Text = "Allocate Driver";
            this.chkAllocateDriver.UseVisualStyleBackColor = false;
            // 
            // chkSecondaryPaymentType
            // 
            this.chkSecondaryPaymentType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSecondaryPaymentType.ForeColor = System.Drawing.Color.Black;
            this.chkSecondaryPaymentType.Location = new System.Drawing.Point(331, 521);
            this.chkSecondaryPaymentType.Name = "chkSecondaryPaymentType";
            this.chkSecondaryPaymentType.Size = new System.Drawing.Size(233, 18);
            this.chkSecondaryPaymentType.TabIndex = 0;
            this.chkSecondaryPaymentType.Text = "Additional Cash Payment Type";
            this.chkSecondaryPaymentType.Visible = false;
            // 
            // btnAttributes
            // 
            this.btnAttributes.BackColor = System.Drawing.Color.AliceBlue;
            this.btnAttributes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAttributes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnAttributes.Location = new System.Drawing.Point(762, 266);
            this.btnAttributes.Name = "btnAttributes";
            this.btnAttributes.Size = new System.Drawing.Size(161, 45);
            this.btnAttributes.TabIndex = 502;
            this.btnAttributes.Text = "Attributes";
            this.btnAttributes.UseVisualStyleBackColor = false;
            this.btnAttributes.Click += new System.EventHandler(this.btnAttributes_Click);
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
            this.pnljourney.TabStop = false;
            // 
            // opt_one
            // 
            this.opt_one.AutoSize = true;
            this.opt_one.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_one.Location = new System.Drawing.Point(13, 5);
            this.opt_one.Name = "opt_one";
            this.opt_one.Size = new System.Drawing.Size(88, 22);
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
            this.opt_return.Location = new System.Drawing.Point(102, 5);
            this.opt_return.Name = "opt_return";
            this.opt_return.Size = new System.Drawing.Size(69, 22);
            this.opt_return.TabIndex = 8;
            this.opt_return.Text = "Return";
            this.opt_return.UseVisualStyleBackColor = true;
            this.opt_return.CheckedChanged += new System.EventHandler(this.opt_return_CheckedChanged);
            this.opt_return.KeyDown += new System.Windows.Forms.KeyEventHandler(this.opt_one_KeyDown);
            this.opt_return.Validating += new System.ComponentModel.CancelEventHandler(this.opt_return_Validating);
            // 
            // chkPermanentCustNotes
            // 
            this.chkPermanentCustNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.chkPermanentCustNotes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkPermanentCustNotes.Location = new System.Drawing.Point(5, 508);
            this.chkPermanentCustNotes.Name = "chkPermanentCustNotes";
            this.chkPermanentCustNotes.Size = new System.Drawing.Size(100, 18);
            this.chkPermanentCustNotes.TabIndex = 0;
            this.chkPermanentCustNotes.Text = "Permanent";
            this.chkPermanentCustNotes.UseVisualStyleBackColor = false;
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
            this.txtReturnFrom.SelectedItem = null;
            this.txtReturnFrom.Size = new System.Drawing.Size(237, 35);
            this.txtReturnFrom.TabIndex = 271;
            this.txtReturnFrom.TabStop = false;
            this.txtReturnFrom.Values = null;
            this.txtReturnFrom.Visible = false;
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
            this.txtReturnTo.SelectedItem = null;
            this.txtReturnTo.Size = new System.Drawing.Size(225, 35);
            this.txtReturnTo.TabIndex = 270;
            this.txtReturnTo.TabStop = false;
            this.txtReturnTo.Values = null;
            this.txtReturnTo.Visible = false;
            // 
            // btnPickAccountBooking
            // 
            this.btnPickAccountBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnPickAccountBooking.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnPickAccountBooking.Location = new System.Drawing.Point(328, 324);
            this.btnPickAccountBooking.Name = "btnPickAccountBooking";
            this.btnPickAccountBooking.Size = new System.Drawing.Size(30, 23);
            this.btnPickAccountBooking.TabIndex = 0;
            this.btnPickAccountBooking.Text = "...";
            this.btnPickAccountBooking.UseVisualStyleBackColor = false;
            this.btnPickAccountBooking.Click += new System.EventHandler(this.btnPickAccountBooking_Click);
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
            this.chkQuotedPrice.Size = new System.Drawing.Size(64, 16);
            this.chkQuotedPrice.TabIndex = 0;
            this.chkQuotedPrice.Text = "Quoted";
            this.chkQuotedPrice.UseVisualStyleBackColor = false;
            // 
            // txtPaymentReference
            // 
            this.txtPaymentReference.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentReference.Location = new System.Drawing.Point(930, 78);
            this.txtPaymentReference.MaxLength = 500;
            this.txtPaymentReference.Multiline = true;
            this.txtPaymentReference.Name = "txtPaymentReference";
            this.txtPaymentReference.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPaymentReference.Size = new System.Drawing.Size(280, 61);
            this.txtPaymentReference.TabIndex = 0;
            this.txtPaymentReference.TabStop = false;
            // 
            // lblPaymentRef
            // 
            this.lblPaymentRef.BackColor = System.Drawing.Color.Linen;
            this.lblPaymentRef.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentRef.ForeColor = System.Drawing.Color.Black;
            this.lblPaymentRef.Location = new System.Drawing.Point(945, 59);
            this.lblPaymentRef.Name = "lblPaymentRef";
            this.lblPaymentRef.Size = new System.Drawing.Size(192, 20);
            this.lblPaymentRef.TabIndex = 244;
            this.lblPaymentRef.Text = "Payment References";
            // 
            // txtFaresPostedFrom
            // 
            this.txtFaresPostedFrom.BackColor = System.Drawing.Color.Orange;
            this.txtFaresPostedFrom.Font = new System.Drawing.Font("Tahoma", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFaresPostedFrom.ForeColor = System.Drawing.Color.Maroon;
            this.txtFaresPostedFrom.Location = new System.Drawing.Point(660, 61);
            this.txtFaresPostedFrom.Name = "txtFaresPostedFrom";
            this.txtFaresPostedFrom.Size = new System.Drawing.Size(62, 30);
            this.txtFaresPostedFrom.TabIndex = 243;
            this.txtFaresPostedFrom.Text = "Meter";
            this.txtFaresPostedFrom.Visible = false;
            // 
            // radLabel5
            // 
            this.radLabel5.BackColor = System.Drawing.Color.Orange;
            this.radLabel5.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.radLabel5.ForeColor = System.Drawing.Color.Black;
            this.radLabel5.Location = new System.Drawing.Point(525, 64);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(65, 22);
            this.radLabel5.TabIndex = 237;
            this.radLabel5.Text = "Fares  £";
            // 
            // numFareRate
            // 
            this.numFareRate.DecimalPlaces = 2;
            this.numFareRate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFareRate.ForeColor = System.Drawing.Color.Red;
            this.numFareRate.InterceptArrowKeys = false;
            this.numFareRate.Location = new System.Drawing.Point(590, 62);
            this.numFareRate.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFareRate.Name = "numFareRate";
            this.numFareRate.Size = new System.Drawing.Size(65, 26);
            this.numFareRate.TabIndex = 2;
            this.numFareRate.TabStop = false;
            // 
            // btnDetailMap
            // 
            this.btnDetailMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnDetailMap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDetailMap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnDetailMap.Location = new System.Drawing.Point(808, 113);
            this.btnDetailMap.Name = "btnDetailMap";
            this.btnDetailMap.Size = new System.Drawing.Size(113, 27);
            this.btnDetailMap.TabIndex = 0;
            this.btnDetailMap.TabStop = false;
            this.btnDetailMap.Text = "Show Map (F3)";
            this.btnDetailMap.UseVisualStyleBackColor = false;
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
            this.lblTip.Size = new System.Drawing.Size(28, 20);
            this.lblTip.TabIndex = 155;
            this.lblTip.Text = "Tip";
            this.lblTip.Visible = false;
            // 
            // numTipAmount
            // 
            this.numTipAmount.Font = new System.Drawing.Font("Tahoma", 9F);
            this.numTipAmount.InterceptArrowKeys = false;
            this.numTipAmount.Location = new System.Drawing.Point(260, 5);
            this.numTipAmount.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numTipAmount.Name = "numTipAmount";
            this.numTipAmount.Size = new System.Drawing.Size(31, 22);
            this.numTipAmount.TabIndex = 0;
            this.numTipAmount.TabStop = false;
            this.numTipAmount.Visible = false;
            // 
            // radLabel4
            // 
            this.radLabel4.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel4.ForeColor = System.Drawing.Color.Black;
            this.radLabel4.Location = new System.Drawing.Point(233, 61);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(39, 20);
            this.radLabel4.TabIndex = 153;
            this.radLabel4.Text = "W/T";
            // 
            // numDrvWaitingMins
            // 
            this.numDrvWaitingMins.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numDrvWaitingMins.InterceptArrowKeys = false;
            this.numDrvWaitingMins.Location = new System.Drawing.Point(277, 58);
            this.numDrvWaitingMins.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numDrvWaitingMins.Name = "numDrvWaitingMins";
            this.numDrvWaitingMins.Size = new System.Drawing.Size(48, 24);
            this.numDrvWaitingMins.TabIndex = 0;
            this.numDrvWaitingMins.TabStop = false;
            // 
            // radLabel18
            // 
            this.radLabel18.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel18.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel18.ForeColor = System.Drawing.Color.Red;
            this.radLabel18.Location = new System.Drawing.Point(327, 61);
            this.radLabel18.Name = "radLabel18";
            this.radLabel18.Size = new System.Drawing.Size(118, 20);
            this.radLabel18.TabIndex = 150;
            this.radLabel18.Text = "Total Charges £";
            // 
            // numTotalChrgs
            // 
            this.numTotalChrgs.BackColor = System.Drawing.SystemColors.Control;
            this.numTotalChrgs.DecimalPlaces = 2;
            this.numTotalChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numTotalChrgs.Location = new System.Drawing.Point(448, 59);
            this.numTotalChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numTotalChrgs.Name = "numTotalChrgs";
            this.numTotalChrgs.ReadOnly = true;
            this.numTotalChrgs.Size = new System.Drawing.Size(69, 24);
            this.numTotalChrgs.TabIndex = 0;
            this.numTotalChrgs.TabStop = false;
            // 
            // radLabel20
            // 
            this.radLabel20.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel20.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel20.ForeColor = System.Drawing.Color.Black;
            this.radLabel20.Location = new System.Drawing.Point(1, 33);
            this.radLabel20.Name = "radLabel20";
            this.radLabel20.Size = new System.Drawing.Size(165, 20);
            this.radLabel20.TabIndex = 148;
            this.radLabel20.Text = "Drv Parking Charges £ ";
            // 
            // numCongChrgs
            // 
            this.numCongChrgs.DecimalPlaces = 2;
            this.numCongChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numCongChrgs.InterceptArrowKeys = false;
            this.numCongChrgs.Location = new System.Drawing.Point(170, 31);
            this.numCongChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCongChrgs.Name = "numCongChrgs";
            this.numCongChrgs.Size = new System.Drawing.Size(59, 24);
            this.numCongChrgs.TabIndex = 0;
            this.numCongChrgs.TabStop = false;
            // 
            // radLabel16
            // 
            this.radLabel16.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel16.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel16.ForeColor = System.Drawing.Color.Black;
            this.radLabel16.Location = new System.Drawing.Point(288, 33);
            this.radLabel16.Name = "radLabel16";
            this.radLabel16.Size = new System.Drawing.Size(166, 20);
            this.radLabel16.TabIndex = 146;
            this.radLabel16.Text = "Drv Waiting Charges  £";
            // 
            // numMeetCharges
            // 
            this.numMeetCharges.DecimalPlaces = 2;
            this.numMeetCharges.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numMeetCharges.InterceptArrowKeys = false;
            this.numMeetCharges.Location = new System.Drawing.Point(457, 31);
            this.numMeetCharges.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numMeetCharges.Name = "numMeetCharges";
            this.numMeetCharges.Size = new System.Drawing.Size(61, 24);
            this.numMeetCharges.TabIndex = 0;
            this.numMeetCharges.TabStop = false;
            // 
            // radLabel17
            // 
            this.radLabel17.BackColor = this.pnlOtherCharges.BackColor;
            this.radLabel17.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel17.ForeColor = System.Drawing.Color.Black;
            this.radLabel17.Location = new System.Drawing.Point(3, 61);
            this.radLabel17.Name = "radLabel17";
            this.radLabel17.Size = new System.Drawing.Size(158, 20);
            this.radLabel17.TabIndex = 144;
            this.radLabel17.Text = "Extra Drop/Charges £";
            // 
            // numExtraChrgs
            // 
            this.numExtraChrgs.DecimalPlaces = 2;
            this.numExtraChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numExtraChrgs.InterceptArrowKeys = false;
            this.numExtraChrgs.Location = new System.Drawing.Point(170, 59);
            this.numExtraChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numExtraChrgs.Name = "numExtraChrgs";
            this.numExtraChrgs.Size = new System.Drawing.Size(60, 24);
            this.numExtraChrgs.TabIndex = 0;
            this.numExtraChrgs.TabStop = false;
            // 
            // lblAccWaitingCharges
            // 
            this.lblAccWaitingCharges.BackColor = this.pnlOtherCharges.BackColor;
            this.lblAccWaitingCharges.Enabled = false;
            this.lblAccWaitingCharges.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblAccWaitingCharges.ForeColor = System.Drawing.Color.Black;
            this.lblAccWaitingCharges.Location = new System.Drawing.Point(290, 7);
            this.lblAccWaitingCharges.Name = "lblAccWaitingCharges";
            this.lblAccWaitingCharges.Size = new System.Drawing.Size(164, 20);
            this.lblAccWaitingCharges.TabIndex = 142;
            this.lblAccWaitingCharges.Text = "A/C Waiting Charges £";
            // 
            // numWaitingChrgs
            // 
            this.numWaitingChrgs.DecimalPlaces = 2;
            this.numWaitingChrgs.Enabled = false;
            this.numWaitingChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numWaitingChrgs.InterceptArrowKeys = false;
            this.numWaitingChrgs.Location = new System.Drawing.Point(457, 5);
            this.numWaitingChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numWaitingChrgs.Name = "numWaitingChrgs";
            this.numWaitingChrgs.Size = new System.Drawing.Size(60, 24);
            this.numWaitingChrgs.TabIndex = 0;
            this.numWaitingChrgs.TabStop = false;
            // 
            // lblAccParkingCharges
            // 
            this.lblAccParkingCharges.BackColor = this.pnlOtherCharges.BackColor;
            this.lblAccParkingCharges.Enabled = false;
            this.lblAccParkingCharges.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblAccParkingCharges.ForeColor = System.Drawing.Color.Black;
            this.lblAccParkingCharges.Location = new System.Drawing.Point(2, 6);
            this.lblAccParkingCharges.Name = "lblAccParkingCharges";
            this.lblAccParkingCharges.Size = new System.Drawing.Size(163, 20);
            this.lblAccParkingCharges.TabIndex = 140;
            this.lblAccParkingCharges.Text = "A/C Parking Charges £";
            // 
            // numParkingChrgs
            // 
            this.numParkingChrgs.DecimalPlaces = 2;
            this.numParkingChrgs.Enabled = false;
            this.numParkingChrgs.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numParkingChrgs.InterceptArrowKeys = false;
            this.numParkingChrgs.Location = new System.Drawing.Point(169, 4);
            this.numParkingChrgs.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numParkingChrgs.Name = "numParkingChrgs";
            this.numParkingChrgs.Size = new System.Drawing.Size(60, 24);
            this.numParkingChrgs.TabIndex = 0;
            this.numParkingChrgs.TabStop = false;
            // 
            // pnlPaymentMode
            // 
            this.pnlPaymentMode.BackColor = this.pnlOtherCharges.BackColor;
            this.pnlPaymentMode.Controls.Add(this.numCashPaymentFares);
            this.pnlPaymentMode.Controls.Add(this.btnPayment);
            this.pnlPaymentMode.Controls.Add(this.chkIsCommissionWise);
            this.pnlPaymentMode.Controls.Add(this.ddlCommissionType);
            this.pnlPaymentMode.Controls.Add(this.numDriverCommission);
            this.pnlPaymentMode.Controls.Add(this.radLabel7);
            this.pnlPaymentMode.Controls.Add(this.ddlPaymentType);
            this.pnlPaymentMode.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.pnlPaymentMode.ForeColor = System.Drawing.Color.White;
            this.pnlPaymentMode.Location = new System.Drawing.Point(2, 22);
            this.pnlPaymentMode.Name = "pnlPaymentMode";
            this.pnlPaymentMode.Size = new System.Drawing.Size(638, 35);
            this.pnlPaymentMode.TabIndex = 0;
            this.pnlPaymentMode.TabStop = true;
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
            this.numCashPaymentFares.Size = new System.Drawing.Size(61, 26);
            this.numCashPaymentFares.TabIndex = 0;
            this.numCashPaymentFares.TabStop = false;
            this.numCashPaymentFares.Visible = false;
            // 
            // btnPayment
            // 
            this.btnPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnPayment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnPayment.Location = new System.Drawing.Point(236, 1);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(105, 30);
            this.btnPayment.TabIndex = 0;
            this.btnPayment.Text = "Payment";
            this.btnPayment.UseVisualStyleBackColor = false;
            this.btnPayment.Visible = false;
            // 
            // chkIsCommissionWise
            // 
            this.chkIsCommissionWise.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.chkIsCommissionWise.ForeColor = System.Drawing.Color.Black;
            this.chkIsCommissionWise.Location = new System.Drawing.Point(347, 7);
            this.chkIsCommissionWise.Name = "chkIsCommissionWise";
            this.chkIsCommissionWise.Size = new System.Drawing.Size(112, 18);
            this.chkIsCommissionWise.TabIndex = 205;
            this.chkIsCommissionWise.Text = "Commission";
            // 
            // ddlCommissionType
            // 
            this.ddlCommissionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlCommissionType.Enabled = false;
            this.ddlCommissionType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCommissionType.ForeColor = System.Drawing.Color.Black;
            this.ddlCommissionType.Location = new System.Drawing.Point(530, 4);
            this.ddlCommissionType.Name = "ddlCommissionType";
            this.ddlCommissionType.ShowDownArrow = true;
            this.ddlCommissionType.Size = new System.Drawing.Size(108, 26);
            this.ddlCommissionType.TabIndex = 207;
            // 
            // numDriverCommission
            // 
            this.numDriverCommission.Enabled = false;
            this.numDriverCommission.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDriverCommission.Location = new System.Drawing.Point(461, 6);
            this.numDriverCommission.Name = "numDriverCommission";
            this.numDriverCommission.Size = new System.Drawing.Size(63, 23);
            this.numDriverCommission.TabIndex = 206;
            this.numDriverCommission.TabStop = false;
            // 
            // radLabel7
            // 
            this.radLabel7.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel7.ForeColor = System.Drawing.Color.Black;
            this.radLabel7.Location = new System.Drawing.Point(2, 6);
            this.radLabel7.Name = "radLabel7";
            this.radLabel7.Size = new System.Drawing.Size(109, 20);
            this.radLabel7.TabIndex = 132;
            this.radLabel7.Text = "Payment Mode";
            // 
            // ddlPaymentType
            // 
            this.ddlPaymentType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlPaymentType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlPaymentType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPaymentType.ForeColor = System.Drawing.Color.Black;
            this.ddlPaymentType.FormattingEnabled = true;
            this.ddlPaymentType.Location = new System.Drawing.Point(114, 3);
            this.ddlPaymentType.Name = "ddlPaymentType";
            this.ddlPaymentType.ShowDownArrow = true;
            this.ddlPaymentType.Size = new System.Drawing.Size(115, 26);
            this.ddlPaymentType.TabIndex = 0;
            // 
            // btnPickFares
            // 
            this.btnPickFares.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnPickFares.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPickFares.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnPickFares.Location = new System.Drawing.Point(644, 22);
            this.btnPickFares.Name = "btnPickFares";
            this.btnPickFares.Size = new System.Drawing.Size(210, 36);
            this.btnPickFares.TabIndex = 0;
            this.btnPickFares.Text = "Calculate Fares (F4)";
            this.btnPickFares.UseVisualStyleBackColor = false;
            this.btnPickFares.MouseLeave += new System.EventHandler(this.btnSaveNew_MouseLeave);
            this.btnPickFares.MouseHover += new System.EventHandler(this.btnSaveNew_MouseHover);
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
            this.lblPaymentHeading.BackColor = System.Drawing.Color.Maroon;
            this.lblPaymentHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPaymentHeading.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblPaymentHeading.ForeColor = System.Drawing.Color.White;
            this.lblPaymentHeading.Location = new System.Drawing.Point(0, 0);
            this.lblPaymentHeading.Name = "lblPaymentHeading";
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
            this.pnlBookingFees.BackColor = System.Drawing.Color.Orange;
            this.pnlBookingFees.Controls.Add(this.numReturnBookingFee);
            this.pnlBookingFees.Controls.Add(this.lblReturnCustFare);
            this.pnlBookingFees.Controls.Add(this.numBookingFee);
            this.pnlBookingFees.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.pnlBookingFees.ForeColor = System.Drawing.Color.Black;
            this.pnlBookingFees.Location = new System.Drawing.Point(920, 23);
            this.pnlBookingFees.Name = "pnlBookingFees";
            this.pnlBookingFees.Size = new System.Drawing.Size(289, 35);
            this.pnlBookingFees.TabIndex = 0;
            this.pnlBookingFees.Text = "Booking Fee £";
            this.pnlBookingFees.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pnlBookingFees.Visible = false;
            // 
            // numReturnBookingFee
            // 
            this.numReturnBookingFee.DecimalPlaces = 2;
            this.numReturnBookingFee.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReturnBookingFee.InterceptArrowKeys = false;
            this.numReturnBookingFee.Location = new System.Drawing.Point(225, 5);
            this.numReturnBookingFee.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numReturnBookingFee.Name = "numReturnBookingFee";
            this.numReturnBookingFee.Size = new System.Drawing.Size(58, 26);
            this.numReturnBookingFee.TabIndex = 0;
            this.numReturnBookingFee.TabStop = false;
            this.numReturnBookingFee.Visible = false;
            // 
            // lblReturnCustFare
            // 
            this.lblReturnCustFare.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnCustFare.Location = new System.Drawing.Point(172, 9);
            this.lblReturnCustFare.Name = "lblReturnCustFare";
            this.lblReturnCustFare.Size = new System.Drawing.Size(49, 19);
            this.lblReturnCustFare.TabIndex = 248;
            this.lblReturnCustFare.Text = "R/T  £";
            this.lblReturnCustFare.Visible = false;
            // 
            // numBookingFee
            // 
            this.numBookingFee.DecimalPlaces = 2;
            this.numBookingFee.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBookingFee.InterceptArrowKeys = false;
            this.numBookingFee.Location = new System.Drawing.Point(107, 5);
            this.numBookingFee.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numBookingFee.Name = "numBookingFee";
            this.numBookingFee.Size = new System.Drawing.Size(59, 26);
            this.numBookingFee.TabIndex = 0;
            this.numBookingFee.TabStop = false;
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
            this.txtVehicleNo.Size = new System.Drawing.Size(2, 2);
            this.txtVehicleNo.TabIndex = 258;
            // 
            // pnlAutoDespatch
            // 
            this.pnlAutoDespatch.BackColor = System.Drawing.Color.FloralWhite;
            this.pnlAutoDespatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAutoDespatch.Controls.Add(this.chkLead);
            this.pnlAutoDespatch.Controls.Add(this.label6);
            this.pnlAutoDespatch.Controls.Add(this.numLead);
            this.pnlAutoDespatch.Controls.Add(this.btnSms);
            this.pnlAutoDespatch.Controls.Add(this.chkBidding);
            this.pnlAutoDespatch.Controls.Add(this.chkDisablePassengerSMS);
            this.pnlAutoDespatch.Controls.Add(this.chkDisableDriverSMS);
            this.pnlAutoDespatch.Controls.Add(this.chkAutoDespatch);
            this.pnlAutoDespatch.Controls.Add(this.numBeforeMinutes);
            this.pnlAutoDespatch.Location = new System.Drawing.Point(760, 135);
            this.pnlAutoDespatch.Name = "pnlAutoDespatch";
            this.pnlAutoDespatch.Size = new System.Drawing.Size(165, 228);
            this.pnlAutoDespatch.TabIndex = 0;
            // 
            // chkLead
            // 
            this.chkLead.AutoSize = true;
            this.chkLead.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkLead.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.chkLead.Location = new System.Drawing.Point(8, 3);
            this.chkLead.Name = "chkLead";
            this.chkLead.Size = new System.Drawing.Size(55, 18);
            this.chkLead.TabIndex = 522;
            this.chkLead.Text = "Lead";
            this.chkLead.CheckedChanged += new System.EventHandler(this.chkLead_CheckedChanged);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FloralWhite;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.label6.Location = new System.Drawing.Point(115, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 521;
            this.label6.Text = "mins";
            // 
            // numLead
            // 
            this.numLead.Enabled = false;
            this.numLead.Font = new System.Drawing.Font("Tahoma", 9F);
            this.numLead.InterceptArrowKeys = false;
            this.numLead.Location = new System.Drawing.Point(63, 2);
            this.numLead.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numLead.Name = "numLead";
            this.numLead.Size = new System.Drawing.Size(50, 22);
            this.numLead.TabIndex = 520;
            this.numLead.TabStop = false;
            // 
            // btnSms
            // 
            this.btnSms.BackColor = System.Drawing.Color.Purple;
            this.btnSms.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSms.ForeColor = System.Drawing.Color.White;
            this.btnSms.Location = new System.Drawing.Point(36, 60);
            this.btnSms.Name = "btnSms";
            this.btnSms.Size = new System.Drawing.Size(86, 26);
            this.btnSms.TabIndex = 222;
            this.btnSms.Text = "SMS";
            this.btnSms.UseVisualStyleBackColor = false;
            this.btnSms.Visible = false;
            // 
            // chkBidding
            // 
            this.chkBidding.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkBidding.Location = new System.Drawing.Point(1, 109);
            this.chkBidding.Name = "chkBidding";
            this.chkBidding.Size = new System.Drawing.Size(73, 20);
            this.chkBidding.TabIndex = 223;
            this.chkBidding.Text = "Bidding";
            // 
            // chkDisablePassengerSMS
            // 
            this.chkDisablePassengerSMS.AutoSize = true;
            this.chkDisablePassengerSMS.BackColor = System.Drawing.Color.Transparent;
            this.chkDisablePassengerSMS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.chkDisablePassengerSMS.ForeColor = System.Drawing.Color.Black;
            this.chkDisablePassengerSMS.Location = new System.Drawing.Point(1, 43);
            this.chkDisablePassengerSMS.Name = "chkDisablePassengerSMS";
            this.chkDisablePassengerSMS.Size = new System.Drawing.Size(158, 17);
            this.chkDisablePassengerSMS.TabIndex = 204;
            this.chkDisablePassengerSMS.Text = "Disable Passenger Text";
            this.chkDisablePassengerSMS.UseVisualStyleBackColor = false;
            // 
            // chkDisableDriverSMS
            // 
            this.chkDisableDriverSMS.AutoSize = true;
            this.chkDisableDriverSMS.BackColor = System.Drawing.Color.Transparent;
            this.chkDisableDriverSMS.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.chkDisableDriverSMS.ForeColor = System.Drawing.Color.Black;
            this.chkDisableDriverSMS.Location = new System.Drawing.Point(1, 25);
            this.chkDisableDriverSMS.Name = "chkDisableDriverSMS";
            this.chkDisableDriverSMS.Size = new System.Drawing.Size(134, 17);
            this.chkDisableDriverSMS.TabIndex = 203;
            this.chkDisableDriverSMS.Text = "Disable Driver Text";
            this.chkDisableDriverSMS.UseVisualStyleBackColor = false;
            // 
            // chkAutoDespatch
            // 
            this.chkAutoDespatch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
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
            this.numBeforeMinutes.Size = new System.Drawing.Size(24, 26);
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
            this.pnlCustomer.BackColor = System.Drawing.Color.AliceBlue;
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
            this.pnlCustomer.Size = new System.Drawing.Size(395, 115);
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
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(134, 89);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(201, 26);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.TabStop = false;
            // 
            // btnCustomerLister
            // 
            this.btnCustomerLister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnCustomerLister.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnCustomerLister.Location = new System.Drawing.Point(336, 5);
            this.btnCustomerLister.Name = "btnCustomerLister";
            this.btnCustomerLister.Size = new System.Drawing.Size(31, 23);
            this.btnCustomerLister.TabIndex = 0;
            this.btnCustomerLister.Text = "...";
            this.btnCustomerLister.UseVisualStyleBackColor = false;
            // 
            // radLabel6
            // 
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel6.Location = new System.Drawing.Point(0, 5);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(133, 22);
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
            this.txtCustomerPhoneNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerPhoneNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerPhoneNo.Location = new System.Drawing.Point(134, 32);
            this.txtCustomerPhoneNo.MaxLength = 30;
            this.txtCustomerPhoneNo.Name = "txtCustomerPhoneNo";
            this.txtCustomerPhoneNo.Size = new System.Drawing.Size(201, 26);
            this.txtCustomerPhoneNo.TabIndex = 0;
            this.txtCustomerPhoneNo.TabStop = false;
            // 
            // txtCustomerMobileNo
            // 
            this.txtCustomerMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomerMobileNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerMobileNo.Location = new System.Drawing.Point(134, 61);
            this.txtCustomerMobileNo.MaxLength = 30;
            this.txtCustomerMobileNo.Name = "txtCustomerMobileNo";
            this.txtCustomerMobileNo.Size = new System.Drawing.Size(201, 26);
            this.txtCustomerMobileNo.TabIndex = 0;
            this.txtCustomerMobileNo.TabStop = false;
            // 
            // ddlCustomerName
            // 
            this.ddlCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ddlCustomerName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCustomerName.Location = new System.Drawing.Point(134, 3);
            this.ddlCustomerName.MaxLength = 100;
            this.ddlCustomerName.Name = "ddlCustomerName";
            this.ddlCustomerName.Size = new System.Drawing.Size(201, 26);
            this.ddlCustomerName.TabIndex = 6;
            this.ddlCustomerName.TabStop = false;
            // 
            // btn_notes
            // 
            this.btn_notes.BackColor = System.Drawing.Color.GhostWhite;
            this.btn_notes.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.btn_notes.Location = new System.Drawing.Point(336, 48);
            this.btn_notes.Name = "btn_notes";
            this.btn_notes.Size = new System.Drawing.Size(59, 63);
            this.btn_notes.TabIndex = 0;
            this.btn_notes.Text = "Notes(0) [F5]";
            this.btn_notes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_notes.UseVisualStyleBackColor = false;
            // 
            // radPanel3
            // 
            this.radPanel3.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel3.Controls.Add(this.btnJobInformation);
            this.radPanel3.Controls.Add(this.btnCancelBooking);
            this.radPanel3.Controls.Add(this.btnExitForm);
            this.radPanel3.Controls.Add(this.radLabel27);
            this.radPanel3.Controls.Add(this.btnSaveNew);
            this.radPanel3.Location = new System.Drawing.Point(663, 448);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(545, 90);
            this.radPanel3.TabIndex = 234;
            // 
            // btnJobInformation
            // 
            this.btnJobInformation.Enabled = false;
            this.btnJobInformation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnJobInformation.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnJobInformation.Image = global::Taxi_AppMain.Resources.Resource1.Print1;
            this.btnJobInformation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnJobInformation.Location = new System.Drawing.Point(164, 21);
            this.btnJobInformation.Name = "btnJobInformation";
            this.btnJobInformation.Size = new System.Drawing.Size(112, 65);
            this.btnJobInformation.TabIndex = 201;
            this.btnJobInformation.Text = "Job Information";
            this.btnJobInformation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnCancelBooking
            // 
            this.btnCancelBooking.Enabled = false;
            this.btnCancelBooking.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelBooking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnCancelBooking.Image = global::Taxi_AppMain.Resources.Resource1.remove;
            this.btnCancelBooking.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelBooking.Location = new System.Drawing.Point(292, 21);
            this.btnCancelBooking.Name = "btnCancelBooking";
            this.btnCancelBooking.Size = new System.Drawing.Size(112, 65);
            this.btnCancelBooking.TabIndex = 199;
            this.btnCancelBooking.Text = "Cancel Booking";
            this.btnCancelBooking.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnExitForm
            // 
            this.btnExitForm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnExitForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnExitForm.Image = global::Taxi_AppMain.Resources.Resource1.exit;
            this.btnExitForm.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnExitForm.Location = new System.Drawing.Point(422, 21);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(117, 65);
            this.btnExitForm.TabIndex = 200;
            this.btnExitForm.TabStop = false;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExitForm.MouseLeave += new System.EventHandler(this.btnSaveNew_MouseLeave);
            this.btnExitForm.MouseHover += new System.EventHandler(this.btnSaveNew_MouseHover);
            // 
            // radLabel27
            // 
            this.radLabel27.BackColor = System.Drawing.Color.Navy;
            this.radLabel27.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel27.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold);
            this.radLabel27.ForeColor = System.Drawing.Color.White;
            this.radLabel27.Location = new System.Drawing.Point(0, 0);
            this.radLabel27.Name = "radLabel27";
            this.radLabel27.Size = new System.Drawing.Size(545, 19);
            this.radLabel27.TabIndex = 26;
            this.radLabel27.Text = "Actions";
            // 
            // btnSaveNew
            // 
            this.btnSaveNew.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSaveNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveNew.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSaveNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnSaveNew.Image = global::Taxi_AppMain.Resources.Resource1.save_Tick;
            this.btnSaveNew.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveNew.Location = new System.Drawing.Point(7, 22);
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(136, 65);
            this.btnSaveNew.TabIndex = 25;
            this.btnSaveNew.Text = "Save Booking    (HOME)";
            this.btnSaveNew.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveNew.UseVisualStyleBackColor = false;
            this.btnSaveNew.MouseLeave += new System.EventHandler(this.btnSaveNew_MouseLeave);
            this.btnSaveNew.MouseHover += new System.EventHandler(this.btnSaveNew_MouseHover);
            // 
            // numTotalLuggages
            // 
            this.numTotalLuggages.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotalLuggages.InterceptArrowKeys = false;
            this.numTotalLuggages.Location = new System.Drawing.Point(263, 293);
            this.numTotalLuggages.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numTotalLuggages.Name = "numTotalLuggages";
            this.numTotalLuggages.Size = new System.Drawing.Size(57, 26);
            this.numTotalLuggages.TabIndex = 0;
            this.numTotalLuggages.TabStop = false;
            // 
            // lblLuggages
            // 
            this.lblLuggages.BackColor = System.Drawing.Color.Transparent;
            this.lblLuggages.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLuggages.ForeColor = System.Drawing.Color.Black;
            this.lblLuggages.Location = new System.Drawing.Point(174, 294);
            this.lblLuggages.Name = "lblLuggages";
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
            this.lblPassengers.Size = new System.Drawing.Size(84, 22);
            this.lblPassengers.TabIndex = 252;
            this.lblPassengers.Text = "Passengers";
            // 
            // num_TotalPassengers
            // 
            this.num_TotalPassengers.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_TotalPassengers.InterceptArrowKeys = false;
            this.num_TotalPassengers.Location = new System.Drawing.Point(110, 293);
            this.num_TotalPassengers.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.num_TotalPassengers.Name = "num_TotalPassengers";
            this.num_TotalPassengers.Size = new System.Drawing.Size(56, 26);
            this.num_TotalPassengers.TabIndex = 0;
            this.num_TotalPassengers.TabStop = false;
            this.num_TotalPassengers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnMultiVehicle
            // 
            this.btnMultiVehicle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnMultiVehicle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnMultiVehicle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnMultiVehicle.Location = new System.Drawing.Point(244, 215);
            this.btnMultiVehicle.Name = "btnMultiVehicle";
            this.btnMultiVehicle.Size = new System.Drawing.Size(99, 25);
            this.btnMultiVehicle.TabIndex = 0;
            this.btnMultiVehicle.Text = "Multi Vehicle";
            this.btnMultiVehicle.UseVisualStyleBackColor = false;
            // 
            // radLabel11
            // 
            this.radLabel11.BackColor = this.pnlMain.BackColor;
            this.radLabel11.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel11.Location = new System.Drawing.Point(6, 216);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(94, 20);
            this.radLabel11.TabIndex = 244;
            this.radLabel11.Text = "Vehicle Type";
            // 
            // ddlVehicleType
            // 
            this.ddlVehicleType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlVehicleType.FormattingEnabled = true;
            this.ddlVehicleType.Location = new System.Drawing.Point(113, 214);
            this.ddlVehicleType.Name = "ddlVehicleType";
            this.ddlVehicleType.ShowDownArrow = true;
            this.ddlVehicleType.Size = new System.Drawing.Size(124, 26);
            this.ddlVehicleType.TabIndex = 8;
            // 
            // chkIsCompanyRates
            // 
            this.chkIsCompanyRates.BackColor = System.Drawing.Color.Transparent;
            this.chkIsCompanyRates.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.chkIsCompanyRates.ForeColor = System.Drawing.Color.Black;
            this.chkIsCompanyRates.Location = new System.Drawing.Point(8, 326);
            this.chkIsCompanyRates.Name = "chkIsCompanyRates";
            this.chkIsCompanyRates.Size = new System.Drawing.Size(84, 22);
            this.chkIsCompanyRates.TabIndex = 238;
            this.chkIsCompanyRates.Text = "Account";
            this.chkIsCompanyRates.UseVisualStyleBackColor = false;
            // 
            // radLabel14
            // 
            this.radLabel14.BackColor = System.Drawing.Color.Transparent;
            this.radLabel14.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel14.Location = new System.Drawing.Point(6, 269);
            this.radLabel14.Name = "radLabel14";
            this.radLabel14.Size = new System.Drawing.Size(104, 21);
            this.radLabel14.TabIndex = 245;
            this.radLabel14.Text = "Journey Type";
            // 
            // ddlCompany
            // 
            this.ddlCompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlCompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompany.FormattingEnabled = true;
            this.ddlCompany.Location = new System.Drawing.Point(111, 323);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(208, 26);
            this.ddlCompany.TabIndex = 240;
            this.ddlCompany.TabStop = false;
            // 
            // dtpPickupDate
            // 
            this.dtpPickupDate.CustomFormat = "dd/MM/yyyy";
            this.dtpPickupDate.Font = new System.Drawing.Font("Tahoma", 11F);
            this.dtpPickupDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPickupDate.Location = new System.Drawing.Point(495, 334);
            this.dtpPickupDate.Name = "dtpPickupDate";
            this.dtpPickupDate.Size = new System.Drawing.Size(102, 25);
            this.dtpPickupDate.TabIndex = 5;
            this.dtpPickupDate.TabStop = false;
            this.dtpPickupDate.Value = null;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel3.Location = new System.Drawing.Point(368, 337);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(94, 21);
            this.radLabel3.TabIndex = 246;
            this.radLabel3.Text = "Pickup Date";
            // 
            // dtpPickupTime
            // 
            this.dtpPickupTime.CustomFormat = "HH:mm";
            this.dtpPickupTime.Font = new System.Drawing.Font("Tahoma", 11F);
            this.dtpPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPickupTime.Location = new System.Drawing.Point(658, 334);
            this.dtpPickupTime.Name = "dtpPickupTime";
            this.dtpPickupTime.ShowUpDown = true;
            this.dtpPickupTime.Size = new System.Drawing.Size(91, 25);
            this.dtpPickupTime.TabIndex = 4;
            this.dtpPickupTime.TabStop = false;
            this.dtpPickupTime.Value = null;
            // 
            // lblPickupTime
            // 
            this.lblPickupTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.lblPickupTime.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.lblPickupTime.Location = new System.Drawing.Point(603, 337);
            this.lblPickupTime.Name = "lblPickupTime";
            this.lblPickupTime.Size = new System.Drawing.Size(50, 20);
            this.lblPickupTime.TabIndex = 247;
            this.lblPickupTime.Text = "Time";
            // 
            // ddlDriver
            // 
            this.ddlDriver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlDriver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.FormattingEnabled = true;
            this.ddlDriver.Location = new System.Drawing.Point(495, 363);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.ShowDownArrow = true;
            this.ddlDriver.Size = new System.Drawing.Size(254, 26);
            this.ddlDriver.TabIndex = 0;
            this.ddlDriver.Validating += new System.ComponentModel.CancelEventHandler(this.ddlDriver_Validating);
            // 
            // radLabel25
            // 
            this.radLabel25.BackColor = System.Drawing.Color.Transparent;
            this.radLabel25.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel25.Location = new System.Drawing.Point(3, 470);
            this.radLabel25.Name = "radLabel25";
            this.radLabel25.Size = new System.Drawing.Size(106, 38);
            this.radLabel25.TabIndex = 243;
            this.radLabel25.Text = "Special Requirements";
            // 
            // txtSpecialRequirements
            // 
            this.txtSpecialRequirements.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.txtSpecialRequirements.Location = new System.Drawing.Point(111, 462);
            this.txtSpecialRequirements.MaxLength = 500;
            this.txtSpecialRequirements.Multiline = true;
            this.txtSpecialRequirements.Name = "txtSpecialRequirements";
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
            this.chkQuotation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.chkQuotation.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.chkQuotation.ForeColor = System.Drawing.Color.Fuchsia;
            this.chkQuotation.Location = new System.Drawing.Point(752, 64);
            this.chkQuotation.Name = "chkQuotation";
            this.chkQuotation.Size = new System.Drawing.Size(134, 22);
            this.chkQuotation.TabIndex = 206;
            this.chkQuotation.Text = "Quotation (F6)";
            this.chkQuotation.UseVisualStyleBackColor = false;
            // 
            // chkReverse
            // 
            this.chkReverse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.chkReverse.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.chkReverse.ForeColor = System.Drawing.Color.Black;
            this.chkReverse.Location = new System.Drawing.Point(762, 98);
            this.chkReverse.Name = "chkReverse";
            this.chkReverse.Size = new System.Drawing.Size(107, 28);
            this.chkReverse.TabIndex = 0;
            this.chkReverse.Text = "Reverse (F9)";
            this.chkReverse.UseVisualStyleBackColor = false;
            this.chkReverse.CheckStateChanged += new System.EventHandler(this.chkReverse_ToggleStateChanging);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.panel1.Controls.Add(this.btnPasteBooking);
            this.panel1.Controls.Add(this.btnDespatchView);
            this.panel1.Controls.Add(this.btnAccountCode);
            this.panel1.Controls.Add(this.btnViewMapReport);
            this.panel1.Controls.Add(this.btnNearestDrv);
            this.panel1.Controls.Add(this.btnSendEmail);
            this.panel1.Controls.Add(this.btnBase);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnMultiBooking);
            this.panel1.Controls.Add(this.btnTrackDriver);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1215, 37);
            this.panel1.TabIndex = 0;
            // 
            // btnPasteBooking
            // 
            this.btnPasteBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnPasteBooking.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnPasteBooking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnPasteBooking.Location = new System.Drawing.Point(698, -1);
            this.btnPasteBooking.Name = "btnPasteBooking";
            this.btnPasteBooking.Size = new System.Drawing.Size(105, 38);
            this.btnPasteBooking.TabIndex = 274;
            this.btnPasteBooking.Text = "Paste Booking";
            this.btnPasteBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPasteBooking.UseVisualStyleBackColor = false;
            this.btnPasteBooking.Click += new System.EventHandler(this.btnPasteBooking_Click);
            // 
            // btnDespatchView
            // 
            this.btnDespatchView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnDespatchView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDespatchView.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnDespatchView.Location = new System.Drawing.Point(823, -1);
            this.btnDespatchView.Name = "btnDespatchView";
            this.btnDespatchView.Size = new System.Drawing.Size(127, 38);
            this.btnDespatchView.TabIndex = 0;
            this.btnDespatchView.Text = "Route Suggestion";
            this.btnDespatchView.UseVisualStyleBackColor = false;
            this.btnDespatchView.Click += new System.EventHandler(this.btnDespatchView_Click);
            // 
            // btnAccountCode
            // 
            this.btnAccountCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnAccountCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAccountCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnAccountCode.Location = new System.Drawing.Point(315, -1);
            this.btnAccountCode.Name = "btnAccountCode";
            this.btnAccountCode.Size = new System.Drawing.Size(98, 38);
            this.btnAccountCode.TabIndex = 210;
            this.btnAccountCode.Text = "Account Code (F10)";
            this.btnAccountCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAccountCode.UseVisualStyleBackColor = false;
            this.btnAccountCode.Click += new System.EventHandler(this.btnAccountCode_Click);
            // 
            // btnViewMapReport
            // 
            this.btnViewMapReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnViewMapReport.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnViewMapReport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnViewMapReport.Location = new System.Drawing.Point(1104, 1);
            this.btnViewMapReport.Name = "btnViewMapReport";
            this.btnViewMapReport.Size = new System.Drawing.Size(105, 35);
            this.btnViewMapReport.TabIndex = 0;
            this.btnViewMapReport.Text = "Map Report (CTRL+M)";
            this.btnViewMapReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnViewMapReport.UseVisualStyleBackColor = false;
            this.btnViewMapReport.Visible = false;
            // 
            // btnNearestDrv
            // 
            this.btnNearestDrv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnNearestDrv.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnNearestDrv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnNearestDrv.Location = new System.Drawing.Point(967, -1);
            this.btnNearestDrv.Name = "btnNearestDrv";
            this.btnNearestDrv.Size = new System.Drawing.Size(113, 38);
            this.btnNearestDrv.TabIndex = 209;
            this.btnNearestDrv.Text = "Nearest Drivers (F12)";
            this.btnNearestDrv.UseVisualStyleBackColor = false;
            this.btnNearestDrv.Click += new System.EventHandler(this.btnNearestDrv_Click);
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnSendEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSendEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnSendEmail.Location = new System.Drawing.Point(427, -1);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(90, 38);
            this.btnSendEmail.TabIndex = 0;
            this.btnSendEmail.Text = "Send Email     (F11)";
            this.btnSendEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSendEmail.UseVisualStyleBackColor = false;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // btnBase
            // 
            this.btnBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnBase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnBase.Location = new System.Drawing.Point(5, -1);
            this.btnBase.Name = "btnBase";
            this.btnBase.Size = new System.Drawing.Size(82, 38);
            this.btnBase.TabIndex = 0;
            this.btnBase.Text = "     Base    (F1)";
            this.btnBase.UseVisualStyleBackColor = false;
            this.btnBase.Click += new System.EventHandler(this.btnBase_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnSearch.Location = new System.Drawing.Point(101, -1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 38);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Job History (F7)";
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnMultiBooking
            // 
            this.btnMultiBooking.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnMultiBooking.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnMultiBooking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnMultiBooking.Location = new System.Drawing.Point(205, -1);
            this.btnMultiBooking.Name = "btnMultiBooking";
            this.btnMultiBooking.Size = new System.Drawing.Size(98, 38);
            this.btnMultiBooking.TabIndex = 0;
            this.btnMultiBooking.Text = "Multi Booking (F8)";
            this.btnMultiBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMultiBooking.UseVisualStyleBackColor = false;
            this.btnMultiBooking.Click += new System.EventHandler(this.btnMultiBooking_Click);
            // 
            // btnTrackDriver
            // 
            this.btnTrackDriver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnTrackDriver.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnTrackDriver.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnTrackDriver.Location = new System.Drawing.Point(699, -1);
            this.btnTrackDriver.Name = "btnTrackDriver";
            this.btnTrackDriver.Size = new System.Drawing.Size(105, 38);
            this.btnTrackDriver.TabIndex = 275;
            this.btnTrackDriver.Text = "Track Driver";
            this.btnTrackDriver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTrackDriver.UseVisualStyleBackColor = false;
            this.btnTrackDriver.Visible = false;
            // 
            // txtFromAddress
            // 
            this.txtFromAddress.BackColor = System.Drawing.Color.White;
            this.txtFromAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFromAddress.DefaultHeight = 0;
            this.txtFromAddress.DefaultWidth = 0;
            this.txtFromAddress.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtFromAddress.ForceListBoxToUpdate = false;
            this.txtFromAddress.FormerValue = "";
            this.txtFromAddress.Location = new System.Drawing.Point(114, 64);
            this.txtFromAddress.Multiline = true;
            this.txtFromAddress.Name = "txtFromAddress";
            this.txtFromAddress.SelectedItem = null;
            this.txtFromAddress.Size = new System.Drawing.Size(200, 80);
            this.txtFromAddress.TabIndex = 1;
            this.txtFromAddress.TabStop = false;
            this.txtFromAddress.Values = null;
            this.txtFromAddress.Enter += new System.EventHandler(this.txtFromAddress_Enter);
            this.txtFromAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromAddress_KeyDown);
            this.txtFromAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromAddress_KeyPress);
            // 
            // txtFromPostCode
            // 
            this.txtFromPostCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFromPostCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFromPostCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromPostCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFromPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromPostCode.Location = new System.Drawing.Point(114, 95);
            this.txtFromPostCode.MaxLength = 100;
            this.txtFromPostCode.Name = "txtFromPostCode";
            this.txtFromPostCode.Size = new System.Drawing.Size(206, 26);
            this.txtFromPostCode.TabIndex = 1;
            this.txtFromPostCode.TabStop = false;
            this.txtFromPostCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromPostCode_KeyDown);
            this.txtFromPostCode.Validated += new System.EventHandler(this.txtFromPostCode_Validated);
            // 
            // txtFromStreetComing
            // 
            this.txtFromStreetComing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromStreetComing.Location = new System.Drawing.Point(114, 153);
            this.txtFromStreetComing.MaxLength = 100;
            this.txtFromStreetComing.Name = "txtFromStreetComing";
            this.txtFromStreetComing.Size = new System.Drawing.Size(205, 26);
            this.txtFromStreetComing.TabIndex = 300;
            this.txtFromStreetComing.TabStop = false;
            this.txtFromStreetComing.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFromStreetComing_KeyDown);
            // 
            // txtFromFlightDoorNo
            // 
            this.txtFromFlightDoorNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFromFlightDoorNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromFlightDoorNo.Location = new System.Drawing.Point(114, 124);
            this.txtFromFlightDoorNo.MaxLength = 100;
            this.txtFromFlightDoorNo.Name = "txtFromFlightDoorNo";
            this.txtFromFlightDoorNo.Size = new System.Drawing.Size(205, 26);
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
            this.txtToAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToAddress.DefaultHeight = 0;
            this.txtToAddress.DefaultWidth = 0;
            this.txtToAddress.Font = new System.Drawing.Font("Tahoma", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtToAddress.ForceListBoxToUpdate = false;
            this.txtToAddress.FormerValue = "";
            this.txtToAddress.Location = new System.Drawing.Point(496, 64);
            this.txtToAddress.Multiline = true;
            this.txtToAddress.Name = "txtToAddress";
            this.txtToAddress.SelectedItem = null;
            this.txtToAddress.Size = new System.Drawing.Size(200, 80);
            this.txtToAddress.TabIndex = 3;
            this.txtToAddress.TabStop = false;
            this.txtToAddress.Values = null;
            this.txtToAddress.Enter += new System.EventHandler(this.txtToAddress_Enter);
            this.txtToAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToAddress_KeyDown);
            this.txtToAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFromAddress_KeyPress);
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
            this.ddlFromLocType.Size = new System.Drawing.Size(136, 24);
            this.ddlFromLocType.TabIndex = 500;
            this.ddlFromLocType.TabStop = false;
            this.ddlFromLocType.Visible = false;
            this.ddlFromLocType.SelectedIndexChanged += new System.EventHandler(this.ddlFromLocType_SelectedIndexChanged);
            // 
            // ddlToLocType
            // 
            this.ddlToLocType.BackColor = System.Drawing.Color.White;
            this.ddlToLocType.Location = new System.Drawing.Point(386, 497);
            this.ddlToLocType.Name = "ddlToLocType";
            this.ddlToLocType.Size = new System.Drawing.Size(131, 26);
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
            this.txtToStreetComing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToStreetComing.Location = new System.Drawing.Point(496, 152);
            this.txtToStreetComing.MaxLength = 100;
            this.txtToStreetComing.Name = "txtToStreetComing";
            this.txtToStreetComing.Size = new System.Drawing.Size(206, 26);
            this.txtToStreetComing.TabIndex = 301;
            this.txtToStreetComing.TabStop = false;
            this.txtToStreetComing.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtToStreetComing_KeyDown);
            // 
            // txtToPostCode
            // 
            this.txtToPostCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtToPostCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtToPostCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToPostCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPostCode.Location = new System.Drawing.Point(496, 95);
            this.txtToPostCode.MaxLength = 100;
            this.txtToPostCode.Name = "txtToPostCode";
            this.txtToPostCode.Size = new System.Drawing.Size(206, 26);
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
            this.txtToFlightDoorNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtToFlightDoorNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToFlightDoorNo.Location = new System.Drawing.Point(496, 124);
            this.txtToFlightDoorNo.MaxLength = 100;
            this.txtToFlightDoorNo.Name = "txtToFlightDoorNo";
            this.txtToFlightDoorNo.Size = new System.Drawing.Size(206, 26);
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
            this.btnSelectVia.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.btnSelectVia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnSelectVia.Location = new System.Drawing.Point(324, 74);
            this.btnSelectVia.Name = "btnSelectVia";
            this.btnSelectVia.Size = new System.Drawing.Size(62, 65);
            this.btnSelectVia.TabIndex = 0;
            this.btnSelectVia.Text = "+Via (0) [ F2 ]";
            this.btnSelectVia.UseVisualStyleBackColor = false;
            this.btnSelectVia.Click += new System.EventHandler(this.btnSelectVia_Click);
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
            this.ddlBookingType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ddlBookingType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlBookingType.Location = new System.Drawing.Point(993, 7);
            this.ddlBookingType.Name = "ddlBookingType";
            this.ddlBookingType.Size = new System.Drawing.Size(106, 26);
            this.ddlBookingType.TabIndex = 218;
            // 
            // lblBookingType
            // 
            this.lblBookingType.BackColor = System.Drawing.Color.SteelBlue;
            this.lblBookingType.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblBookingType.ForeColor = System.Drawing.Color.White;
            this.lblBookingType.Location = new System.Drawing.Point(929, 9);
            this.lblBookingType.Name = "lblBookingType";
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
            this.ddlSubCompany.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ddlSubCompany.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSubCompany.FormattingEnabled = true;
            this.ddlSubCompany.Location = new System.Drawing.Point(7, 6);
            this.ddlSubCompany.Name = "ddlSubCompany";
            this.ddlSubCompany.Size = new System.Drawing.Size(246, 27);
            this.ddlSubCompany.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.btnExit.Image = global::Taxi_AppMain.Resources.Resource1.exit;
            this.btnExit.Location = new System.Drawing.Point(1139, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 40);
            this.btnExit.TabIndex = 273;
            this.btnExit.Text = "Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 742);
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
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnljourney.ResumeLayout(false);
            this.pnljourney.PerformLayout();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRate)).EndInit();
            this.pnlOtherCharges.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTipAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDrvWaitingMins)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCongChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeetCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitingChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParkingChrgs)).EndInit();
            this.pnlPaymentMode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCashPaymentFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDriverCommission)).EndInit();
            this.pnlBookingFees.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numReturnBookingFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBookingFee)).EndInit();
            this.pnlAutoDespatch.ResumeLayout(false);
            this.pnlAutoDespatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBeforeMinutes)).EndInit();
            this.pnlCustomer.ResumeLayout(false);
            this.pnlCustomer.PerformLayout();
            this.radPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTotalLuggages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPassengers)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private Label lblToLoc;
        private Label lblFromLoc;
        private Label lblReturnPickupTime;
        private Label lblReturnPickupDate;
        private UIX.MyDatePicker dtpReturnPickupTime;
        private UIX.MyDatePicker dtpReturnPickupDate;
        private CheckBox lblReturnDriver;
        private UIX.MyDropDownList ddlReturnDriver;
        private Label lblFromDoorFlightNo;
        private System.Windows.Forms.TextBox txtFromPostCode;
        private System.Windows.Forms.TextBox txtFromStreetComing;
        private System.Windows.Forms.TextBox txtFromFlightDoorNo;
        private Label lblFromStreetComing;
        private System.Windows.Forms.TextBox txtToStreetComing;
        private System.Windows.Forms.TextBox txtToFlightDoorNo;
        private Label lblToStreetComing;
        private Label lblToDoorFlightNo;
        private System.Windows.Forms.TextBox txtToPostCode;
        private Button btnSelectVia;
        private UIX.AutoCompleteTextBox txtFromAddress;
        private UIX.AutoCompleteTextBox txtToAddress;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnMultiBooking;
        private System.Windows.Forms.Label label1;
        private Button btnBase;
        private System.Windows.Forms.Panel panel1;
      



        private System.Windows.Forms.CheckBox chkReverse;
        private System.Windows.Forms.Panel pnlOrderNo;
        private Label lblOrderNo;
        private System.Windows.Forms.TextBox txtPupilNo;
        private System.Windows.Forms.TextBox txtOrderNo;
        private Label lblPupilNo;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnDespatchView;
        private Label txtBookingNo;
        private System.Windows.Forms.CheckBox chkQuotation;
        private System.Windows.Forms.Button btnSendEmail;


        private System.Windows.Forms.ComboBox ddlBookingType;
        private Label lblBookingType;

        private System.Windows.Forms.Panel pnlComcab;
        private Label radLabel29;
        private System.Windows.Forms.NumericUpDown numComcab_WaitingMin;
        private Label radLabel31;
        private System.Windows.Forms.NumericUpDown numComcab_ExtraMile;
        private Label radLabel28;
        private System.Windows.Forms.NumericUpDown numComcab_Account;
        private Label radLabel8;
        private System.Windows.Forms.NumericUpDown numComcab_Cash;
        private System.Windows.Forms.Panel pnlAccpassword;
        private System.Windows.Forms.TextBox txtAccPassword;
        private Label radLabel33;
        private System.Windows.Forms.Label pnlReturnJobNo;
        private System.Windows.Forms.Panel pnlVia;
        private UIX.AutoCompleteTextBox txtViaAddress;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnAddVia;
        private UIX.MyDropDownList ddlViaLocation;
        private Telerik.WinControls.UI.RadGridView grdVia;
        private UIX.AutoCompleteTextBox txtviaPostCode;
        private Label lblViaLoc;
        private Label lblFromViaPoint;
        private UIX.MyDropDownList ddlViaFromLocType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBookedBy;
        private ComboBox ddlReturnFromAirport;
        private Label lblReturnFromAirport;
        private System.Windows.Forms.ComboBox ddlSubCompany;
       
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button btnNearestDrv;
        private System.Windows.Forms.ComboBox ddlPickupPlot;
        private System.Windows.Forms.Panel radPanel1;

        private Label lblCompanyPrice;
        private System.Windows.Forms.NumericUpDown numCompanyFares;

        private Label lblReturnCompanyPrice;
        private System.Windows.Forms.NumericUpDown numReturnCompanyFares;

        private Label lblRetFares;
        private System.Windows.Forms.NumericUpDown numReturnFare;
        private Label radLabel5;
        private System.Windows.Forms.NumericUpDown numFareRate;
        private System.Windows.Forms.Button btnDetailMap;
        private System.Windows.Forms.Panel pnlOtherCharges;
        private Label radLabel18;
        private System.Windows.Forms.NumericUpDown numTotalChrgs;
        private Label radLabel20;
        private System.Windows.Forms.NumericUpDown numCongChrgs;
        private Label radLabel16;
        private System.Windows.Forms.NumericUpDown numMeetCharges;
        private Label radLabel17;
        private System.Windows.Forms.NumericUpDown numExtraChrgs;
        private Label lblAccWaitingCharges;
        private System.Windows.Forms.NumericUpDown numWaitingChrgs;
        private Label lblAccParkingCharges;
        private System.Windows.Forms.NumericUpDown numParkingChrgs;
        private Label pnlPaymentMode;
        private System.Windows.Forms.Button btnPayment;
        private UIX.MyDropDownList ddlCommissionType;
        private System.Windows.Forms.NumericUpDown numDriverCommission;
        private System.Windows.Forms.CheckBox chkIsCommissionWise;
        private Label radLabel7;
        private UIX.MyDropDownList ddlPaymentType;
        private System.Windows.Forms.Button btnPickFares;
        private System.Windows.Forms.Label lblMap;
        private Label lblPaymentHeading;
        private System.Windows.Forms.Label pnlFares;
        private System.Windows.Forms.Label lblDropOffPlot;
        private System.Windows.Forms.Label lblPickupPlot;
        private System.Windows.Forms.Button btn_notes;
        private Label txtVehicleNo;
        private System.Windows.Forms.Panel pnlAutoDespatch;
        private System.Windows.Forms.Button btnSms;
        private System.Windows.Forms.CheckBox chkDisablePassengerSMS;
        private System.Windows.Forms.CheckBox chkDisableDriverSMS;
        private System.Windows.Forms.CheckBox chkAutoDespatch;
        private System.Windows.Forms.NumericUpDown numBeforeMinutes;
        private Label pnlCustomer;
        private System.Windows.Forms.Button btnCustomerLister;
        private Label radLabel6;
        private Label radLabel21;
        private Label radLabel19;
        private System.Windows.Forms.TextBox txtCustomerPhoneNo;
        private System.Windows.Forms.TextBox txtCustomerMobileNo;
        private System.Windows.Forms.TextBox ddlCustomerName;
        private System.Windows.Forms.Panel radPanel3;
       // private Telerik.WinControls.UI.RadSplitButton btnPrintJob;
        private System.Windows.Forms.Button btnCancelBooking;
        private System.Windows.Forms.Button btnExitForm;
      
        private Label radLabel27;
        private System.Windows.Forms.Button btnSaveNew;
        private System.Windows.Forms.NumericUpDown numTotalLuggages;
        private Label lblLuggages;
        private Label lblPassengers;
        private System.Windows.Forms.NumericUpDown num_TotalPassengers;
        private Label lblDepartment;
        private UIX.MyDropDownList ddlDepartment;
        private System.Windows.Forms.Button btnMultiVehicle;
        private Label radLabel11;
        private UIX.MyDropDownList ddlVehicleType;
        private System.Windows.Forms.CheckBox chkIsCompanyRates;
        private Label radLabel14;
        private ComboBox ddlCompany;
        private UIX.MyDatePicker dtpPickupDate;
        private Label radLabel3;
        private UIX.MyDatePicker dtpPickupTime;
        private Label lblPickupTime;
        private UIX.MyDropDownList ddlDriver;
        private Label radLabel25;
        private System.Windows.Forms.TextBox txtSpecialRequirements;


   

        private Label radLabel2;
        private System.Windows.Forms.TextBox txtEmail;
      
        private Label pnlBookingFees;
        private System.Windows.Forms.NumericUpDown numBookingFee;
        private Label radLabel32;
        private System.Windows.Forms.NumericUpDown numAgentCommission;
        private System.Windows.Forms.CheckBox chkTakenByAgent;
        private Label radLabel34;
        private System.Windows.Forms.NumericUpDown numAgentCommissionPercent;
        private UIX.MyDropDownList ddlAgentCommissionType;
        private System.Windows.Forms.PictureBox pic_Signature;
        private System.Windows.Forms.Label txtCourierSignedOn;
        private System.Windows.Forms.Label lblCourierHeader;
 
        private Label txtFaresPostedFrom;


        private Label lblAccountBookedBy;
        private System.Windows.Forms.TextBox txtAccountBookedBy;
        private Label lblReturnSpecialReq;
        private System.Windows.Forms.TextBox txtReturnSpecialReq;
        private Label lblDirection;
        private System.Windows.Forms.ComboBox ddlDirection;
        private System.Windows.Forms.Button btnViewMapReport;
     
        private System.Windows.Forms.Button btnPickAccountBooking;
        private System.Windows.Forms.ComboBox ddlbabyseat2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox ddlBabyseat1;
        private System.Windows.Forms.Label label8;
        private Label lblReturnVehicle;
        private UIX.MyDropDownList ddlReturnVehicleType;
      
        private Label lblEscort;

        private System.Windows.Forms.NumericUpDown numEscortPrice;
        private Label lblEscortPrice;
        private System.Windows.Forms.TextBox txtPaymentReference;
        private Label lblPaymentRef;
        private UIX.AutoCompleteTextBox txtReturnTo;
        private UIX.AutoCompleteTextBox txtReturnFrom;
        private System.Windows.Forms.NumericUpDown numCashPaymentFares;
        private System.Windows.Forms.CheckBox chkSecondaryPaymentType;
        private System.Windows.Forms.CheckBox chkBidding;
        private System.Windows.Forms.NumericUpDown numReturnBookingFee;
        private Label lblReturnCustFare;
        private System.Windows.Forms.Button btnExit;
      
        System.Windows.Forms.NumericUpDown numDrvWaitingMins;
        private Label radLabel4;
     
        private Label lblTip;
        System.Windows.Forms.NumericUpDown numTipAmount;
        private Label lblJourneyTime;
        System.Windows.Forms.NumericUpDown numJourneyTime;
        System.Windows.Forms.CheckBox chkQuotedPrice;
        System.Windows.Forms.CheckBox chkPermanentCustNotes;
        //private RadGridView grdPickupDateTime;
        private System.Windows.Forms.RadioButton optSMSThirdParty;
        private System.Windows.Forms.RadioButton optSMSGsm;
        private System.Windows.Forms.ComboBox ddlDropOffPlot;
        private Button btnAccountCode;
        //NC
        private System.Windows.Forms.DataGridView grdPickupDateTime;
        private System.Windows.Forms.TabControl radPageView1;
        private System.Windows.Forms.TabPage tabCurrentBooking;
        private System.Windows.Forms.TabPage tabNearestDrivers;

        private Button btnRefreshNearestDrivers;
        private System.Windows.Forms.GroupBox pnljourney;
        private System.Windows.Forms.RadioButton opt_one;
        private System.Windows.Forms.RadioButton opt_waitreturn;
        private System.Windows.Forms.RadioButton opt_return;
        private Button btnAttributes;
        private ComboBox ddlFromLocType;
        private ComboBox ddlToLocType;
        private Button btnPasteBooking;
        private Button btnJobInformation;
        private Button btnTrackDriver;
        private CheckBox chkAllocateDriver;
        private CheckBox chkSameAllocationtoReturnDrv;
        public Button btnExcludeDrivers;
        private ComboBox ddlCompletedSubCompany;
        private Label lblCompletedSubCompany;
        private Button btnInfo;
        private CheckBox chkLead;
        private Label label6;
        private NumericUpDown numLead;
        //

    }
}