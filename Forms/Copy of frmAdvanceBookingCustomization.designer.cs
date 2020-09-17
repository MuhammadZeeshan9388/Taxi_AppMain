namespace Taxi_AppMain
{
    partial class frmAdvanceBookingCustomization
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
            this.components = new System.ComponentModel.Container();
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlOneWay = new Telerik.WinControls.UI.RadPanel();
            this.grdBookings = new UI.MyGridView();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.chkCancelBooking = new Telerik.WinControls.UI.RadCheckBox();
            this.btnRevertChanges = new Telerik.WinControls.UI.RadButton();
            this.radPanel5 = new Telerik.WinControls.UI.RadPanel();
            this.chkDayWise = new Telerik.WinControls.UI.RadCheckBox();
            this.groupDayWise = new System.Windows.Forms.GroupBox();
            this.dtpDayWiseReturnPickUpTime = new UI.MyDatePicker();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.rdoSun = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoSat = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoFri = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoThu = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoWed = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoTue = new Telerik.WinControls.UI.RadRadioButton();
            this.dtpDayWisePickupTime = new UI.MyDatePicker();
            this.rdoMon = new Telerik.WinControls.UI.RadRadioButton();
            this.radLabel19 = new Telerik.WinControls.UI.RadLabel();
            this.chkCompanyPrice = new Telerik.WinControls.UI.RadCheckBox();
            this.chkCustomerPrice = new Telerik.WinControls.UI.RadCheckBox();
            this.numReturnCompanyPrice = new Telerik.WinControls.UI.RadSpinEditor();
            this.chkReturnCompanyPrice = new Telerik.WinControls.UI.RadCheckBox();
            this.numReturnCustomerFare = new Telerik.WinControls.UI.RadSpinEditor();
            this.chkReturnCustomerPrice = new Telerik.WinControls.UI.RadCheckBox();
            this.numCompanyPrice = new Telerik.WinControls.UI.RadSpinEditor();
            this.numCustomerPrice = new Telerik.WinControls.UI.RadSpinEditor();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlPaymentType = new UI.MyDropDownList();
            this.radLabel23 = new Telerik.WinControls.UI.RadLabel();
            this.txtReturnSpecialRequirement = new Telerik.WinControls.UI.RadTextBox();
            this.lblReturnSpecialReq = new Telerik.WinControls.UI.RadLabel();
            this.txtCustomerName = new Telerik.WinControls.UI.RadTextBox();
            this.txtSpecialReq = new Telerik.WinControls.UI.RadTextBox();
            this.txtEmail = new Telerik.WinControls.UI.RadTextBox();
            this.txtTelephoneNo = new Telerik.WinControls.UI.RadTextBox();
            this.txtMobileNo = new Telerik.WinControls.UI.RadTextBox();
            this.txtOrderNo = new Telerik.WinControls.UI.RadTextBox();
            this.ddlVehicle = new UI.MyDropDownList();
            this.ddlAccount = new UI.MyDropDownList();
            this.numTotalLuggages = new Telerik.WinControls.UI.RadSpinEditor();
            this.num_TotalPassengers = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel21 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel22 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel12 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel14 = new Telerik.WinControls.UI.RadLabel();
            this.lblOrderNo = new Telerik.WinControls.UI.RadLabel();
            this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.lblPassengers = new Telerik.WinControls.UI.RadLabel();
            this.btnSelectVia = new Telerik.WinControls.UI.RadToggleButton();
            this.txtFromAddress = new UI.AutoCompleteTextBox();
            this.txtFromPostCode = new Telerik.WinControls.UI.RadTextBox();
            this.txtFromStreetComing = new Telerik.WinControls.UI.RadTextBox();
            this.txtFromFlightDoorNo = new Telerik.WinControls.UI.RadTextBox();
            this.lblFromStreetComing = new Telerik.WinControls.UI.RadLabel();
            this.lblFromDoorFlightNo = new Telerik.WinControls.UI.RadLabel();
            this.txtToAddress = new UI.AutoCompleteTextBox();
            this.lblFromLoc = new Telerik.WinControls.UI.RadLabel();
            this.radLabel13 = new Telerik.WinControls.UI.RadLabel();
            this.ddlFromLocType = new UI.DJComboBox();
            this.ddlFromLocation = new UI.DJComboBox();
            this.radLabel9 = new Telerik.WinControls.UI.RadLabel();
            this.ddlToLocType = new UI.DJComboBox();
            this.lblToLoc = new Telerik.WinControls.UI.RadLabel();
            this.txtToStreetComing = new Telerik.WinControls.UI.RadTextBox();
            this.ddlToLocation = new UI.DJComboBox();
            this.txtToPostCode = new Telerik.WinControls.UI.RadTextBox();
            this.lblToStreetComing = new Telerik.WinControls.UI.RadLabel();
            this.txtToFlightDoorNo = new Telerik.WinControls.UI.RadTextBox();
            this.lblToDoorFlightNo = new Telerik.WinControls.UI.RadLabel();
            this.ddlJourneyType = new Telerik.WinControls.UI.RadDropDownList();
            this.chkDestination = new Telerik.WinControls.UI.RadCheckBox();
            this.chkReturnFares = new Telerik.WinControls.UI.RadCheckBox();
            this.chkPickup = new Telerik.WinControls.UI.RadCheckBox();
            this.chkPickupFares = new Telerik.WinControls.UI.RadCheckBox();
            this.chkReturnPickupTime = new Telerik.WinControls.UI.RadCheckBox();
            this.chkPickupTime = new Telerik.WinControls.UI.RadCheckBox();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnInclude = new Telerik.WinControls.UI.RadButton();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.grdExcludePickupDates = new Telerik.WinControls.UI.RadGridView();
            this.btnExclude = new Telerik.WinControls.UI.RadButton();
            this.grdPickupDates = new Telerik.WinControls.UI.RadGridView();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.CustomerName = new Telerik.WinControls.UI.RadLabel();
            this.txtContactDetails = new Telerik.WinControls.UI.RadLabel();
            this.ddlDropOffPlot = new System.Windows.Forms.ComboBox();
            this.ddlPickupPlot = new System.Windows.Forms.ComboBox();
            this.chkEndingAt = new Telerik.WinControls.UI.RadCheckBox();
            this.dtpReturnPickupTime = new UI.MyDatePicker();
            this.dtpEndingAt = new UI.MyDatePicker();
            this.chkStartingAt = new Telerik.WinControls.UI.RadCheckBox();
            this.dtpPickupTime = new UI.MyDatePicker();
            this.dtpStartingAt = new UI.MyDatePicker();
            this.numReturnFares = new Telerik.WinControls.UI.RadSpinEditor();
            this.numPickupFares = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnApplyChanges = new Telerik.WinControls.UI.RadButton();
            this.ddlVehicleType = new UI.MyDropDownList();
            this.ddlCompany = new UI.MyDropDownList();
            this.gbCancelBooking = new System.Windows.Forms.GroupBox();
            this.btnApplyCancelChanges = new Telerik.WinControls.UI.RadButton();
            this.btnSaveChanges = new Telerik.WinControls.UI.RadButton();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.dtpEndingCancel = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.dtpStartingCancel = new UI.MyDatePicker();
            this.btnSaveBooking = new Telerik.WinControls.UI.RadButton();
            this.radPanel4 = new Telerik.WinControls.UI.RadPanel();
            this.txtBookedBy = new Telerik.WinControls.UI.RadLabel();
            this.opt_JOneWay = new Telerik.WinControls.UI.RadRadioButton();
            this.opt_WaitandReturn = new Telerik.WinControls.UI.RadRadioButton();
            this.radLabel18 = new Telerik.WinControls.UI.RadLabel();
            this.numTotalChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel20 = new Telerik.WinControls.UI.RadLabel();
            this.numCongChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel16 = new Telerik.WinControls.UI.RadLabel();
            this.numMeetCharges = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel17 = new Telerik.WinControls.UI.RadLabel();
            this.numExtraChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel10 = new Telerik.WinControls.UI.RadLabel();
            this.numWaitingChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.numParkingChrgs = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblMap = new System.Windows.Forms.Label();
            this.numFareRate = new Telerik.WinControls.UI.RadSpinEditor();
            this.numCustomerFares = new Telerik.WinControls.UI.RadSpinEditor();
            this.opt_JReturnWay = new Telerik.WinControls.UI.RadRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlOneWay)).BeginInit();
            this.pnlOneWay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCancelBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRevertChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).BeginInit();
            this.radPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDayWise)).BeginInit();
            this.groupDayWise.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDayWiseReturnPickUpTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoFri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoWed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoTue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDayWisePickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoMon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompanyPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCustomerPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnCompanyPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnCompanyPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnCustomerFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnCustomerPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompanyPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerPrice)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnSpecialRequirement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnSpecialReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialReq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobileNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalLuggages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPassengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOrderNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectVia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromStreetComing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromFlightDoorNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromStreetComing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromDoorFlightNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromLoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromLocType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToLocType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToLoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToStreetComing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToStreetComing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToFlightDoorNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDoorFlightNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlJourneyType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDestination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPickup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPickupFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInclude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcludePickupDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcludePickupDates.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExclude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupDates.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEndingAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReturnPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndingAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStartingAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartingAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPickupFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnApplyChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicleType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
            this.gbCancelBooking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnApplyCancelChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndingCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartingCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).BeginInit();
            this.radPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBookedBy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_JOneWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_WaitandReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCongChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeetCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitingChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParkingChrgs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_JReturnWay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1234, 32);
            this.label1.TabIndex = 107;
            this.label1.Text = "Multi/Future Booking";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // radPanel1
            // 
            this.pnlOneWay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.pnlOneWay.Controls.Add(this.grdBookings);
            this.pnlOneWay.Controls.Add(this.radPanel2);
            this.pnlOneWay.Location = new System.Drawing.Point(0, 32);
            this.pnlOneWay.Name = "radPanel1";
            this.pnlOneWay.Size = new System.Drawing.Size(1233, 756);
            this.pnlOneWay.TabIndex = 106;
            this.pnlOneWay.Paint += new System.Windows.Forms.PaintEventHandler(this.radPanel1_Paint);
            // 
            // grdBookings
            // 
            this.grdBookings.AutoCellFormatting = false;
            this.grdBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBookings.EnableCheckInCheckOut = false;
            this.grdBookings.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdBookings.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdBookings.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdBookings.Location = new System.Drawing.Point(0, 608);
            this.grdBookings.Name = "grdBookings";
            this.grdBookings.PKFieldColumnName = "";
            this.grdBookings.ShowImageOnActionButton = true;
            this.grdBookings.Size = new System.Drawing.Size(1233, 148);
            this.grdBookings.TabIndex = 218;
            this.grdBookings.Text = "myGridView1";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.chkCancelBooking);
            this.radPanel2.Controls.Add(this.btnRevertChanges);
            this.radPanel2.Controls.Add(this.radPanel5);
            this.radPanel2.Controls.Add(this.btnApplyChanges);
            this.radPanel2.Controls.Add(this.ddlVehicleType);
            this.radPanel2.Controls.Add(this.ddlCompany);
            this.radPanel2.Controls.Add(this.gbCancelBooking);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel2.Location = new System.Drawing.Point(0, 0);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(1233, 608);
            this.radPanel2.TabIndex = 219;
            // 
            // chkCancelBooking
            // 
            this.chkCancelBooking.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.chkCancelBooking.Location = new System.Drawing.Point(1042, 127);
            this.chkCancelBooking.Name = "chkCancelBooking";
            // 
            // 
            // 
            this.chkCancelBooking.RootElement.StretchHorizontally = true;
            this.chkCancelBooking.RootElement.StretchVertically = true;
            this.chkCancelBooking.Size = new System.Drawing.Size(169, 30);
            this.chkCancelBooking.TabIndex = 280;
            this.chkCancelBooking.Text = "Cancel Booking";
            this.chkCancelBooking.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkCancelBooking_ToggleStateChanged);
            // 
            // btnRevertChanges
            // 
            this.btnRevertChanges.Image = global::Taxi_AppMain.Properties.Resources.refresh;
            this.btnRevertChanges.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnRevertChanges.Location = new System.Drawing.Point(1067, 11);
            this.btnRevertChanges.Name = "btnRevertChanges";
            this.btnRevertChanges.Size = new System.Drawing.Size(124, 88);
            this.btnRevertChanges.TabIndex = 249;
            this.btnRevertChanges.Text = "Revert Changes";
            this.btnRevertChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRevertChanges.TextWrap = true;
            this.btnRevertChanges.Click += new System.EventHandler(this.btnRevertChanges_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRevertChanges.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.refresh;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRevertChanges.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRevertChanges.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRevertChanges.GetChildAt(0))).Text = "Revert Changes";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnRevertChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnRevertChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnRevertChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radPanel5
            // 
            this.radPanel5.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel5.Controls.Add(this.chkDayWise);
            this.radPanel5.Controls.Add(this.groupDayWise);
            this.radPanel5.Controls.Add(this.chkCompanyPrice);
            this.radPanel5.Controls.Add(this.chkCustomerPrice);
            this.radPanel5.Controls.Add(this.numReturnCompanyPrice);
            this.radPanel5.Controls.Add(this.chkReturnCompanyPrice);
            this.radPanel5.Controls.Add(this.numReturnCustomerFare);
            this.radPanel5.Controls.Add(this.chkReturnCustomerPrice);
            this.radPanel5.Controls.Add(this.numCompanyPrice);
            this.radPanel5.Controls.Add(this.numCustomerPrice);
            this.radPanel5.Controls.Add(this.groupBox1);
            this.radPanel5.Controls.Add(this.btnSelectVia);
            this.radPanel5.Controls.Add(this.txtFromAddress);
            this.radPanel5.Controls.Add(this.txtFromPostCode);
            this.radPanel5.Controls.Add(this.txtFromStreetComing);
            this.radPanel5.Controls.Add(this.txtFromFlightDoorNo);
            this.radPanel5.Controls.Add(this.lblFromStreetComing);
            this.radPanel5.Controls.Add(this.lblFromDoorFlightNo);
            this.radPanel5.Controls.Add(this.txtToAddress);
            this.radPanel5.Controls.Add(this.lblFromLoc);
            this.radPanel5.Controls.Add(this.radLabel13);
            this.radPanel5.Controls.Add(this.ddlFromLocType);
            this.radPanel5.Controls.Add(this.ddlFromLocation);
            this.radPanel5.Controls.Add(this.radLabel9);
            this.radPanel5.Controls.Add(this.ddlToLocType);
            this.radPanel5.Controls.Add(this.lblToLoc);
            this.radPanel5.Controls.Add(this.txtToStreetComing);
            this.radPanel5.Controls.Add(this.ddlToLocation);
            this.radPanel5.Controls.Add(this.txtToPostCode);
            this.radPanel5.Controls.Add(this.lblToStreetComing);
            this.radPanel5.Controls.Add(this.txtToFlightDoorNo);
            this.radPanel5.Controls.Add(this.lblToDoorFlightNo);
            this.radPanel5.Controls.Add(this.ddlJourneyType);
            this.radPanel5.Controls.Add(this.chkDestination);
            this.radPanel5.Controls.Add(this.chkReturnFares);
            this.radPanel5.Controls.Add(this.chkPickup);
            this.radPanel5.Controls.Add(this.chkPickupFares);
            this.radPanel5.Controls.Add(this.chkReturnPickupTime);
            this.radPanel5.Controls.Add(this.chkPickupTime);
            this.radPanel5.Controls.Add(this.radGroupBox2);
            this.radPanel5.Controls.Add(this.radGroupBox1);
            this.radPanel5.Controls.Add(this.chkEndingAt);
            this.radPanel5.Controls.Add(this.dtpReturnPickupTime);
            this.radPanel5.Controls.Add(this.dtpEndingAt);
            this.radPanel5.Controls.Add(this.chkStartingAt);
            this.radPanel5.Controls.Add(this.dtpPickupTime);
            this.radPanel5.Controls.Add(this.dtpStartingAt);
            this.radPanel5.Controls.Add(this.numReturnFares);
            this.radPanel5.Controls.Add(this.numPickupFares);
            this.radPanel5.Location = new System.Drawing.Point(5, 3);
            this.radPanel5.Name = "radPanel5";
            this.radPanel5.Size = new System.Drawing.Size(1031, 604);
            this.radPanel5.TabIndex = 248;
            // 
            // chkDayWise
            // 
            this.chkDayWise.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.chkDayWise.Location = new System.Drawing.Point(741, 12);
            this.chkDayWise.Name = "chkDayWise";
            // 
            // 
            // 
            this.chkDayWise.RootElement.StretchHorizontally = true;
            this.chkDayWise.RootElement.StretchVertically = true;
            this.chkDayWise.Size = new System.Drawing.Size(114, 30);
            this.chkDayWise.TabIndex = 300;
            this.chkDayWise.Text = "Day Wise";
            this.chkDayWise.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkDayWise_ToggleStateChanged);
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.chkDayWise.GetChildAt(0))).Text = "Day Wise";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.chkDayWise.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.chkDayWise.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupDayWise
            // 
            this.groupDayWise.Controls.Add(this.dtpDayWiseReturnPickUpTime);
            this.groupDayWise.Controls.Add(this.radLabel5);
            this.groupDayWise.Controls.Add(this.rdoSun);
            this.groupDayWise.Controls.Add(this.rdoSat);
            this.groupDayWise.Controls.Add(this.rdoFri);
            this.groupDayWise.Controls.Add(this.rdoThu);
            this.groupDayWise.Controls.Add(this.rdoWed);
            this.groupDayWise.Controls.Add(this.rdoTue);
            this.groupDayWise.Controls.Add(this.dtpDayWisePickupTime);
            this.groupDayWise.Controls.Add(this.rdoMon);
            this.groupDayWise.Controls.Add(this.radLabel19);
            this.groupDayWise.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupDayWise.Location = new System.Drawing.Point(740, 23);
            this.groupDayWise.Name = "groupDayWise";
            this.groupDayWise.Size = new System.Drawing.Size(232, 134);
            this.groupDayWise.TabIndex = 299;
            this.groupDayWise.TabStop = false;
            this.groupDayWise.Text = "Day Wise";
            this.groupDayWise.Visible = false;
            // 
            // dtpDayWiseReturnPickUpTime
            // 
            this.dtpDayWiseReturnPickUpTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpDayWiseReturnPickUpTime.CustomFormat = "HH:mm";
            this.dtpDayWiseReturnPickUpTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDayWiseReturnPickUpTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDayWiseReturnPickUpTime.Location = new System.Drawing.Point(148, 103);
            this.dtpDayWiseReturnPickUpTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDayWiseReturnPickUpTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDayWiseReturnPickUpTime.Name = "dtpDayWiseReturnPickUpTime";
            this.dtpDayWiseReturnPickUpTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDayWiseReturnPickUpTime.ShowUpDown = true;
            this.dtpDayWiseReturnPickUpTime.Size = new System.Drawing.Size(76, 24);
            this.dtpDayWiseReturnPickUpTime.TabIndex = 309;
            this.dtpDayWiseReturnPickUpTime.TabStop = false;
            this.dtpDayWiseReturnPickUpTime.Text = "myDatePicker1";
            this.dtpDayWiseReturnPickUpTime.Value = null;
            // 
            // radLabel5
            // 
            this.radLabel5.BackColor = System.Drawing.Color.Transparent;
            this.radLabel5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(7, 72);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(90, 22);
            this.radLabel5.TabIndex = 308;
            this.radLabel5.Text = "Pickup Time";
            // 
            // rdoSun
            // 
            this.rdoSun.Location = new System.Drawing.Point(116, 45);
            this.rdoSun.Name = "rdoSun";
            this.rdoSun.Size = new System.Drawing.Size(43, 18);
            this.rdoSun.TabIndex = 307;
            this.rdoSun.Text = "Sun";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoSun.GetChildAt(0))).Text = "Sun";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoSun.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoSun.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoSat
            // 
            this.rdoSat.Location = new System.Drawing.Point(63, 45);
            this.rdoSat.Name = "rdoSat";
            this.rdoSat.Size = new System.Drawing.Size(43, 18);
            this.rdoSat.TabIndex = 306;
            this.rdoSat.Text = "Sat";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoSat.GetChildAt(0))).Text = "Sat";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoSat.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoSat.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoFri
            // 
            this.rdoFri.Location = new System.Drawing.Point(8, 45);
            this.rdoFri.Name = "rdoFri";
            this.rdoFri.Size = new System.Drawing.Size(43, 18);
            this.rdoFri.TabIndex = 305;
            this.rdoFri.Text = "Fri";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoFri.GetChildAt(0))).Text = "Fri";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoFri.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoFri.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoThu
            // 
            this.rdoThu.Location = new System.Drawing.Point(172, 20);
            this.rdoThu.Name = "rdoThu";
            this.rdoThu.Size = new System.Drawing.Size(44, 18);
            this.rdoThu.TabIndex = 304;
            this.rdoThu.Text = "Thu";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoThu.GetChildAt(0))).Text = "Thu";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoThu.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoThu.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoWed
            // 
            this.rdoWed.Location = new System.Drawing.Point(116, 20);
            this.rdoWed.Name = "rdoWed";
            this.rdoWed.Size = new System.Drawing.Size(49, 18);
            this.rdoWed.TabIndex = 303;
            this.rdoWed.Text = "Wed";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoWed.GetChildAt(0))).Text = "Wed";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoWed.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoWed.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoTue
            // 
            this.rdoTue.Location = new System.Drawing.Point(63, 20);
            this.rdoTue.Name = "rdoTue";
            this.rdoTue.Size = new System.Drawing.Size(48, 18);
            this.rdoTue.TabIndex = 302;
            this.rdoTue.Text = "Tue";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoTue.GetChildAt(0))).Text = "Tue";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoTue.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoTue.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpDayWisePickupTime
            // 
            this.dtpDayWisePickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpDayWisePickupTime.CustomFormat = "HH:mm";
            this.dtpDayWisePickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDayWisePickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDayWisePickupTime.Location = new System.Drawing.Point(148, 73);
            this.dtpDayWisePickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDayWisePickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDayWisePickupTime.Name = "dtpDayWisePickupTime";
            this.dtpDayWisePickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDayWisePickupTime.ShowUpDown = true;
            this.dtpDayWisePickupTime.Size = new System.Drawing.Size(76, 24);
            this.dtpDayWisePickupTime.TabIndex = 288;
            this.dtpDayWisePickupTime.TabStop = false;
            this.dtpDayWisePickupTime.Text = "myDatePicker1";
            this.dtpDayWisePickupTime.Value = null;
            // 
            // rdoMon
            // 
            this.rdoMon.Location = new System.Drawing.Point(8, 20);
            this.rdoMon.Name = "rdoMon";
            this.rdoMon.Size = new System.Drawing.Size(48, 18);
            this.rdoMon.TabIndex = 301;
            this.rdoMon.Text = "Mon";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoMon.GetChildAt(0))).Text = "Mon";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoMon.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoMon.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radLabel19
            // 
            this.radLabel19.BackColor = System.Drawing.Color.Transparent;
            this.radLabel19.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel19.Location = new System.Drawing.Point(7, 103);
            this.radLabel19.Name = "radLabel19";
            this.radLabel19.Size = new System.Drawing.Size(141, 22);
            this.radLabel19.TabIndex = 272;
            this.radLabel19.Text = "Return Pickup Time";
            // 
            // chkCompanyPrice
            // 
            this.chkCompanyPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompanyPrice.Location = new System.Drawing.Point(14, 198);
            this.chkCompanyPrice.Name = "chkCompanyPrice";
            // 
            // 
            // 
            this.chkCompanyPrice.RootElement.StretchHorizontally = true;
            this.chkCompanyPrice.RootElement.StretchVertically = true;
            this.chkCompanyPrice.Size = new System.Drawing.Size(151, 18);
            this.chkCompanyPrice.TabIndex = 298;
            this.chkCompanyPrice.Text = "Company Price £ :";
            this.chkCompanyPrice.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkCompanyPrice_ToggleStateChanged);
            // 
            // chkCustomerPrice
            // 
            this.chkCustomerPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomerPrice.Location = new System.Drawing.Point(14, 166);
            this.chkCustomerPrice.Name = "chkCustomerPrice";
            // 
            // 
            // 
            this.chkCustomerPrice.RootElement.StretchHorizontally = true;
            this.chkCustomerPrice.RootElement.StretchVertically = true;
            this.chkCustomerPrice.Size = new System.Drawing.Size(151, 18);
            this.chkCustomerPrice.TabIndex = 297;
            this.chkCustomerPrice.Text = "Customer Price £ :";
            this.chkCustomerPrice.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkCustomerPrice_ToggleStateChanged);
            // 
            // numReturnCompanyPrice
            // 
            this.numReturnCompanyPrice.DecimalPlaces = 2;
            this.numReturnCompanyPrice.Enabled = false;
            this.numReturnCompanyPrice.EnableKeyMap = true;
            this.numReturnCompanyPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReturnCompanyPrice.InterceptArrowKeys = false;
            this.numReturnCompanyPrice.Location = new System.Drawing.Point(468, 196);
            this.numReturnCompanyPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numReturnCompanyPrice.Name = "numReturnCompanyPrice";
            // 
            // 
            // 
            this.numReturnCompanyPrice.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numReturnCompanyPrice.ShowBorder = true;
            this.numReturnCompanyPrice.ShowUpDownButtons = false;
            this.numReturnCompanyPrice.Size = new System.Drawing.Size(77, 24);
            this.numReturnCompanyPrice.TabIndex = 296;
            this.numReturnCompanyPrice.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnCompanyPrice.GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnCompanyPrice.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numReturnCompanyPrice.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnCompanyPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnCompanyPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkReturnCompanyPrice
            // 
            this.chkReturnCompanyPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReturnCompanyPrice.Location = new System.Drawing.Point(263, 200);
            this.chkReturnCompanyPrice.Name = "chkReturnCompanyPrice";
            // 
            // 
            // 
            this.chkReturnCompanyPrice.RootElement.StretchHorizontally = true;
            this.chkReturnCompanyPrice.RootElement.StretchVertically = true;
            this.chkReturnCompanyPrice.Size = new System.Drawing.Size(201, 18);
            this.chkReturnCompanyPrice.TabIndex = 295;
            this.chkReturnCompanyPrice.Text = "Return Company Price £ :";
            this.chkReturnCompanyPrice.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkReturnCompanyPrice_ToggleStateChanged);
            // 
            // numReturnCustomerFare
            // 
            this.numReturnCustomerFare.DecimalPlaces = 2;
            this.numReturnCustomerFare.Enabled = false;
            this.numReturnCustomerFare.EnableKeyMap = true;
            this.numReturnCustomerFare.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReturnCustomerFare.InterceptArrowKeys = false;
            this.numReturnCustomerFare.Location = new System.Drawing.Point(468, 164);
            this.numReturnCustomerFare.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numReturnCustomerFare.Name = "numReturnCustomerFare";
            // 
            // 
            // 
            this.numReturnCustomerFare.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numReturnCustomerFare.ShowBorder = true;
            this.numReturnCustomerFare.ShowUpDownButtons = false;
            this.numReturnCustomerFare.Size = new System.Drawing.Size(77, 24);
            this.numReturnCustomerFare.TabIndex = 294;
            this.numReturnCustomerFare.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnCustomerFare.GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnCustomerFare.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numReturnCustomerFare.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnCustomerFare.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnCustomerFare.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkReturnCustomerPrice
            // 
            this.chkReturnCustomerPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReturnCustomerPrice.Location = new System.Drawing.Point(263, 165);
            this.chkReturnCustomerPrice.Name = "chkReturnCustomerPrice";
            // 
            // 
            // 
            this.chkReturnCustomerPrice.RootElement.StretchHorizontally = true;
            this.chkReturnCustomerPrice.RootElement.StretchVertically = true;
            this.chkReturnCustomerPrice.Size = new System.Drawing.Size(201, 18);
            this.chkReturnCustomerPrice.TabIndex = 293;
            this.chkReturnCustomerPrice.Text = "Return Customer Price £ :";
            this.chkReturnCustomerPrice.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkReturnCustomerPrice_ToggleStateChanged);
            // 
            // numCompanyPrice
            // 
            this.numCompanyPrice.DecimalPlaces = 2;
            this.numCompanyPrice.Enabled = false;
            this.numCompanyPrice.EnableKeyMap = true;
            this.numCompanyPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCompanyPrice.InterceptArrowKeys = false;
            this.numCompanyPrice.Location = new System.Drawing.Point(174, 196);
            this.numCompanyPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCompanyPrice.Name = "numCompanyPrice";
            // 
            // 
            // 
            this.numCompanyPrice.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCompanyPrice.ShowBorder = true;
            this.numCompanyPrice.ShowUpDownButtons = false;
            this.numCompanyPrice.Size = new System.Drawing.Size(77, 24);
            this.numCompanyPrice.TabIndex = 292;
            this.numCompanyPrice.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCompanyPrice.GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCompanyPrice.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCompanyPrice.GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCompanyPrice.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCompanyPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCompanyPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numCustomerPrice
            // 
            this.numCustomerPrice.DecimalPlaces = 2;
            this.numCustomerPrice.Enabled = false;
            this.numCustomerPrice.EnableKeyMap = true;
            this.numCustomerPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCustomerPrice.ForeColor = System.Drawing.Color.Red;
            this.numCustomerPrice.InterceptArrowKeys = false;
            this.numCustomerPrice.Location = new System.Drawing.Point(175, 163);
            this.numCustomerPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCustomerPrice.Name = "numCustomerPrice";
            // 
            // 
            // 
            this.numCustomerPrice.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCustomerPrice.RootElement.ForeColor = System.Drawing.Color.Red;
            this.numCustomerPrice.ShowBorder = true;
            this.numCustomerPrice.ShowUpDownButtons = false;
            this.numCustomerPrice.Size = new System.Drawing.Size(75, 24);
            this.numCustomerPrice.TabIndex = 290;
            this.numCustomerPrice.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCustomerPrice.GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCustomerPrice.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCustomerPrice.GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCustomerPrice.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Layouts.BoxLayout)(this.numCustomerPrice.GetChildAt(0).GetChildAt(2).GetChildAt(0))).Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            ((Telerik.WinControls.UI.RadRepeatArrowElement)(this.numCustomerPrice.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCustomerPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCustomerPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlPaymentType);
            this.groupBox1.Controls.Add(this.radLabel23);
            this.groupBox1.Controls.Add(this.txtReturnSpecialRequirement);
            this.groupBox1.Controls.Add(this.lblReturnSpecialReq);
            this.groupBox1.Controls.Add(this.txtCustomerName);
            this.groupBox1.Controls.Add(this.txtSpecialReq);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtTelephoneNo);
            this.groupBox1.Controls.Add(this.txtMobileNo);
            this.groupBox1.Controls.Add(this.txtOrderNo);
            this.groupBox1.Controls.Add(this.ddlVehicle);
            this.groupBox1.Controls.Add(this.ddlAccount);
            this.groupBox1.Controls.Add(this.numTotalLuggages);
            this.groupBox1.Controls.Add(this.num_TotalPassengers);
            this.groupBox1.Controls.Add(this.radLabel21);
            this.groupBox1.Controls.Add(this.radLabel22);
            this.groupBox1.Controls.Add(this.radLabel11);
            this.groupBox1.Controls.Add(this.radLabel12);
            this.groupBox1.Controls.Add(this.radLabel14);
            this.groupBox1.Controls.Add(this.lblOrderNo);
            this.groupBox1.Controls.Add(this.radLabel8);
            this.groupBox1.Controls.Add(this.radLabel7);
            this.groupBox1.Controls.Add(this.radLabel6);
            this.groupBox1.Controls.Add(this.lblPassengers);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(706, 163);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 428);
            this.groupBox1.TabIndex = 288;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Other Details";
            // 
            // ddlPaymentType
            // 
            this.ddlPaymentType.Caption = null;
            this.ddlPaymentType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPaymentType.Location = new System.Drawing.Point(135, 108);
            this.ddlPaymentType.Name = "ddlPaymentType";
            this.ddlPaymentType.Property = null;
            this.ddlPaymentType.ShowDownArrow = true;
            this.ddlPaymentType.Size = new System.Drawing.Size(186, 26);
            this.ddlPaymentType.TabIndex = 303;
            this.ddlPaymentType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlPaymentType_SelectedIndexChanged);
            // 
            // radLabel23
            // 
            this.radLabel23.BackColor = System.Drawing.Color.Transparent;
            this.radLabel23.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel23.ForeColor = System.Drawing.Color.Black;
            this.radLabel23.Location = new System.Drawing.Point(2, 109);
            this.radLabel23.Name = "radLabel23";
            // 
            // 
            // 
            this.radLabel23.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel23.Size = new System.Drawing.Size(118, 22);
            this.radLabel23.TabIndex = 302;
            this.radLabel23.Text = "Payment Type";
            // 
            // txtReturnSpecialRequirement
            // 
            this.txtReturnSpecialRequirement.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnSpecialRequirement.Location = new System.Drawing.Point(135, 363);
            this.txtReturnSpecialRequirement.MaxLength = 100;
            this.txtReturnSpecialRequirement.Multiline = true;
            this.txtReturnSpecialRequirement.Name = "txtReturnSpecialRequirement";
            // 
            // 
            // 
            this.txtReturnSpecialRequirement.RootElement.StretchVertically = true;
            this.txtReturnSpecialRequirement.Size = new System.Drawing.Size(186, 53);
            this.txtReturnSpecialRequirement.TabIndex = 301;
            this.txtReturnSpecialRequirement.TabStop = false;
            this.txtReturnSpecialRequirement.Visible = false;
            // 
            // lblReturnSpecialReq
            // 
            this.lblReturnSpecialReq.BackColor = System.Drawing.Color.Transparent;
            this.lblReturnSpecialReq.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnSpecialReq.ForeColor = System.Drawing.Color.Black;
            this.lblReturnSpecialReq.Location = new System.Drawing.Point(1, 337);
            this.lblReturnSpecialReq.Name = "lblReturnSpecialReq";
            // 
            // 
            // 
            this.lblReturnSpecialReq.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblReturnSpecialReq.Size = new System.Drawing.Size(142, 22);
            this.lblReturnSpecialReq.TabIndex = 298;
            this.lblReturnSpecialReq.Text = "Return Special Req:";
            this.lblReturnSpecialReq.Visible = false;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.ForeColor = System.Drawing.Color.Blue;
            this.txtCustomerName.Location = new System.Drawing.Point(135, 166);
            this.txtCustomerName.MaxLength = 100;
            this.txtCustomerName.Name = "txtCustomerName";
            // 
            // 
            // 
            this.txtCustomerName.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.txtCustomerName.Size = new System.Drawing.Size(186, 24);
            this.txtCustomerName.TabIndex = 296;
            this.txtCustomerName.TabStop = false;
            // 
            // txtSpecialReq
            // 
            this.txtSpecialReq.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpecialReq.Location = new System.Drawing.Point(135, 278);
            this.txtSpecialReq.MaxLength = 100;
            this.txtSpecialReq.Multiline = true;
            this.txtSpecialReq.Name = "txtSpecialReq";
            // 
            // 
            // 
            this.txtSpecialReq.RootElement.StretchVertically = true;
            this.txtSpecialReq.Size = new System.Drawing.Size(186, 55);
            this.txtSpecialReq.TabIndex = 300;
            this.txtSpecialReq.TabStop = false;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(135, 250);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(186, 24);
            this.txtEmail.TabIndex = 299;
            this.txtEmail.TabStop = false;
            // 
            // txtTelephoneNo
            // 
            this.txtTelephoneNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelephoneNo.Location = new System.Drawing.Point(135, 222);
            this.txtTelephoneNo.MaxLength = 100;
            this.txtTelephoneNo.Name = "txtTelephoneNo";
            this.txtTelephoneNo.Size = new System.Drawing.Size(186, 24);
            this.txtTelephoneNo.TabIndex = 298;
            this.txtTelephoneNo.TabStop = false;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(135, 194);
            this.txtMobileNo.MaxLength = 100;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(186, 24);
            this.txtMobileNo.TabIndex = 297;
            this.txtMobileNo.TabStop = false;
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNo.Location = new System.Drawing.Point(135, 138);
            this.txtOrderNo.MaxLength = 100;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.Size = new System.Drawing.Size(186, 24);
            this.txtOrderNo.TabIndex = 295;
            this.txtOrderNo.TabStop = false;
            // 
            // ddlVehicle
            // 
            this.ddlVehicle.Caption = null;
            this.ddlVehicle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlVehicle.Location = new System.Drawing.Point(135, 20);
            this.ddlVehicle.Name = "ddlVehicle";
            this.ddlVehicle.Property = null;
            this.ddlVehicle.ShowDownArrow = true;
            this.ddlVehicle.Size = new System.Drawing.Size(186, 26);
            this.ddlVehicle.TabIndex = 291;
            // 
            // ddlAccount
            // 
            this.ddlAccount.Caption = null;
            this.ddlAccount.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlAccount.Location = new System.Drawing.Point(135, 78);
            this.ddlAccount.Name = "ddlAccount";
            this.ddlAccount.Property = null;
            this.ddlAccount.ShowDownArrow = true;
            this.ddlAccount.Size = new System.Drawing.Size(186, 26);
            this.ddlAccount.TabIndex = 294;
            this.ddlAccount.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlAccount_SelectedIndexChanged);
            // 
            // numTotalLuggages
            // 
            this.numTotalLuggages.EnableKeyMap = true;
            this.numTotalLuggages.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTotalLuggages.InterceptArrowKeys = false;
            this.numTotalLuggages.Location = new System.Drawing.Point(276, 50);
            this.numTotalLuggages.Name = "numTotalLuggages";
            // 
            // 
            // 
            this.numTotalLuggages.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numTotalLuggages.ShowBorder = true;
            this.numTotalLuggages.ShowUpDownButtons = false;
            this.numTotalLuggages.Size = new System.Drawing.Size(45, 24);
            this.numTotalLuggages.TabIndex = 293;
            this.numTotalLuggages.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numTotalLuggages.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numTotalLuggages.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTotalLuggages.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTotalLuggages.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // num_TotalPassengers
            // 
            this.num_TotalPassengers.EnableKeyMap = true;
            this.num_TotalPassengers.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_TotalPassengers.InterceptArrowKeys = false;
            this.num_TotalPassengers.Location = new System.Drawing.Point(135, 50);
            this.num_TotalPassengers.Name = "num_TotalPassengers";
            // 
            // 
            // 
            this.num_TotalPassengers.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.num_TotalPassengers.ShowBorder = true;
            this.num_TotalPassengers.ShowUpDownButtons = false;
            this.num_TotalPassengers.Size = new System.Drawing.Size(48, 24);
            this.num_TotalPassengers.TabIndex = 292;
            this.num_TotalPassengers.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.num_TotalPassengers.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel21
            // 
            this.radLabel21.BackColor = System.Drawing.Color.Transparent;
            this.radLabel21.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel21.ForeColor = System.Drawing.Color.Black;
            this.radLabel21.Location = new System.Drawing.Point(3, 278);
            this.radLabel21.Name = "radLabel21";
            // 
            // 
            // 
            this.radLabel21.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel21.Size = new System.Drawing.Size(92, 22);
            this.radLabel21.TabIndex = 287;
            this.radLabel21.Text = "Special Req:";
            // 
            // radLabel22
            // 
            this.radLabel22.BackColor = System.Drawing.Color.Transparent;
            this.radLabel22.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel22.ForeColor = System.Drawing.Color.Black;
            this.radLabel22.Location = new System.Drawing.Point(3, 250);
            this.radLabel22.Name = "radLabel22";
            // 
            // 
            // 
            this.radLabel22.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel22.Size = new System.Drawing.Size(44, 22);
            this.radLabel22.TabIndex = 286;
            this.radLabel22.Text = "Email";
            // 
            // radLabel11
            // 
            this.radLabel11.BackColor = System.Drawing.Color.Transparent;
            this.radLabel11.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.ForeColor = System.Drawing.Color.Black;
            this.radLabel11.Location = new System.Drawing.Point(2, 221);
            this.radLabel11.Name = "radLabel11";
            // 
            // 
            // 
            this.radLabel11.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel11.Size = new System.Drawing.Size(102, 22);
            this.radLabel11.TabIndex = 285;
            this.radLabel11.Text = "Telephone No";
            // 
            // radLabel12
            // 
            this.radLabel12.BackColor = System.Drawing.Color.Transparent;
            this.radLabel12.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel12.ForeColor = System.Drawing.Color.Black;
            this.radLabel12.Location = new System.Drawing.Point(3, 194);
            this.radLabel12.Name = "radLabel12";
            // 
            // 
            // 
            this.radLabel12.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel12.Size = new System.Drawing.Size(75, 22);
            this.radLabel12.TabIndex = 284;
            this.radLabel12.Text = "Mobile No";
            // 
            // radLabel14
            // 
            this.radLabel14.BackColor = System.Drawing.Color.Transparent;
            this.radLabel14.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel14.ForeColor = System.Drawing.Color.Black;
            this.radLabel14.Location = new System.Drawing.Point(1, 166);
            this.radLabel14.Name = "radLabel14";
            // 
            // 
            // 
            this.radLabel14.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel14.Size = new System.Drawing.Size(132, 22);
            this.radLabel14.TabIndex = 283;
            this.radLabel14.Text = "Customer Name";
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.BackColor = System.Drawing.Color.Transparent;
            this.lblOrderNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNo.ForeColor = System.Drawing.Color.Black;
            this.lblOrderNo.Location = new System.Drawing.Point(2, 138);
            this.lblOrderNo.Name = "lblOrderNo";
            // 
            // 
            // 
            this.lblOrderNo.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblOrderNo.Size = new System.Drawing.Size(70, 22);
            this.lblOrderNo.TabIndex = 282;
            this.lblOrderNo.Text = "Order No";
            // 
            // radLabel8
            // 
            this.radLabel8.BackColor = System.Drawing.Color.Transparent;
            this.radLabel8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel8.ForeColor = System.Drawing.Color.Black;
            this.radLabel8.Location = new System.Drawing.Point(2, 79);
            this.radLabel8.Name = "radLabel8";
            // 
            // 
            // 
            this.radLabel8.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel8.Size = new System.Drawing.Size(62, 22);
            this.radLabel8.TabIndex = 281;
            this.radLabel8.Text = "Account";
            // 
            // radLabel7
            // 
            this.radLabel7.BackColor = System.Drawing.Color.Transparent;
            this.radLabel7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel7.ForeColor = System.Drawing.Color.Black;
            this.radLabel7.Location = new System.Drawing.Point(2, 50);
            this.radLabel7.Name = "radLabel7";
            // 
            // 
            // 
            this.radLabel7.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel7.Size = new System.Drawing.Size(77, 22);
            this.radLabel7.TabIndex = 280;
            this.radLabel7.Text = "Passenger";
            // 
            // radLabel6
            // 
            this.radLabel6.BackColor = System.Drawing.Color.Transparent;
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel6.ForeColor = System.Drawing.Color.Black;
            this.radLabel6.Location = new System.Drawing.Point(189, 51);
            this.radLabel6.Name = "radLabel6";
            // 
            // 
            // 
            this.radLabel6.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel6.Size = new System.Drawing.Size(73, 22);
            this.radLabel6.TabIndex = 279;
            this.radLabel6.Text = "Luggages";
            // 
            // lblPassengers
            // 
            this.lblPassengers.BackColor = System.Drawing.Color.Transparent;
            this.lblPassengers.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassengers.ForeColor = System.Drawing.Color.Black;
            this.lblPassengers.Location = new System.Drawing.Point(3, 21);
            this.lblPassengers.Name = "lblPassengers";
            // 
            // 
            // 
            this.lblPassengers.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblPassengers.Size = new System.Drawing.Size(64, 22);
            this.lblPassengers.TabIndex = 278;
            this.lblPassengers.Text = "Vehicle";
            // 
            // btnSelectVia
            // 
            this.btnSelectVia.Location = new System.Drawing.Point(565, 173);
            this.btnSelectVia.Name = "btnSelectVia";
            this.btnSelectVia.Size = new System.Drawing.Size(122, 46);
            this.btnSelectVia.TabIndex = 279;
            this.btnSelectVia.Text = "Show Via Point";
            this.btnSelectVia.TextWrap = true;
            this.btnSelectVia.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.btnSelectVia_ToggleStateChanged);
            ((Telerik.WinControls.UI.RadToggleButtonElement)(this.btnSelectVia.GetChildAt(0))).Text = "Show Via Point";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelectVia.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelectVia.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelectVia.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtFromAddress
            // 
            this.txtFromAddress.BackColor = System.Drawing.Color.White;
            this.txtFromAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFromAddress.DefaultHeight = 0;
            this.txtFromAddress.DefaultWidth = 0;
            this.txtFromAddress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromAddress.ForceListBoxToUpdate = false;
            this.txtFromAddress.FormerValue = "";
            this.txtFromAddress.Location = new System.Drawing.Point(116, 311);
            this.txtFromAddress.Multiline = true;
            this.txtFromAddress.Name = "txtFromAddress";
            // 
            // 
            // 
            this.txtFromAddress.RootElement.StretchVertically = true;
            this.txtFromAddress.SelectedItem = null;
            this.txtFromAddress.Size = new System.Drawing.Size(225, 80);
            this.txtFromAddress.TabIndex = 263;
            this.txtFromAddress.TabStop = false;
            this.txtFromAddress.Values = null;
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
            this.txtFromPostCode.Location = new System.Drawing.Point(116, 312);
            this.txtFromPostCode.MaxLength = 100;
            this.txtFromPostCode.Name = "txtFromPostCode";
            this.txtFromPostCode.Size = new System.Drawing.Size(206, 24);
            this.txtFromPostCode.TabIndex = 261;
            this.txtFromPostCode.TabStop = false;
            // 
            // txtFromStreetComing
            // 
            this.txtFromStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromStreetComing.Location = new System.Drawing.Point(117, 370);
            this.txtFromStreetComing.MaxLength = 100;
            this.txtFromStreetComing.Name = "txtFromStreetComing";
            this.txtFromStreetComing.Size = new System.Drawing.Size(205, 24);
            this.txtFromStreetComing.TabIndex = 265;
            this.txtFromStreetComing.TabStop = false;
            // 
            // txtFromFlightDoorNo
            // 
            this.txtFromFlightDoorNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromFlightDoorNo.Location = new System.Drawing.Point(116, 285);
            this.txtFromFlightDoorNo.MaxLength = 100;
            this.txtFromFlightDoorNo.Name = "txtFromFlightDoorNo";
            this.txtFromFlightDoorNo.Size = new System.Drawing.Size(206, 24);
            this.txtFromFlightDoorNo.TabIndex = 262;
            this.txtFromFlightDoorNo.TabStop = false;
            // 
            // lblFromStreetComing
            // 
            this.lblFromStreetComing.BackColor = System.Drawing.Color.Transparent;
            this.lblFromStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromStreetComing.Location = new System.Drawing.Point(9, 371);
            this.lblFromStreetComing.Name = "lblFromStreetComing";
            this.lblFromStreetComing.Size = new System.Drawing.Size(88, 22);
            this.lblFromStreetComing.TabIndex = 276;
            this.lblFromStreetComing.Text = "From Street";
            // 
            // lblFromDoorFlightNo
            // 
            this.lblFromDoorFlightNo.BackColor = System.Drawing.Color.Transparent;
            this.lblFromDoorFlightNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDoorFlightNo.Location = new System.Drawing.Point(9, 286);
            this.lblFromDoorFlightNo.Name = "lblFromDoorFlightNo";
            this.lblFromDoorFlightNo.Size = new System.Drawing.Size(56, 22);
            this.lblFromDoorFlightNo.TabIndex = 275;
            this.lblFromDoorFlightNo.Text = "Door #";
            // 
            // txtToAddress
            // 
            this.txtToAddress.BackColor = System.Drawing.Color.White;
            this.txtToAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToAddress.DefaultHeight = 0;
            this.txtToAddress.DefaultWidth = 0;
            this.txtToAddress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToAddress.ForceListBoxToUpdate = false;
            this.txtToAddress.FormerValue = "";
            this.txtToAddress.Location = new System.Drawing.Point(459, 311);
            this.txtToAddress.Multiline = true;
            this.txtToAddress.Name = "txtToAddress";
            // 
            // 
            // 
            this.txtToAddress.RootElement.StretchVertically = true;
            this.txtToAddress.SelectedItem = null;
            this.txtToAddress.Size = new System.Drawing.Size(244, 80);
            this.txtToAddress.TabIndex = 268;
            this.txtToAddress.TabStop = false;
            this.txtToAddress.Values = null;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtToAddress.GetChildAt(0))).StretchVertically = true;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).ForeColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).ForeColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).ForeColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.txtToAddress.GetChildAt(0).GetChildAt(2))).ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(189)))), ((int)(((byte)(232)))));
            // 
            // lblFromLoc
            // 
            this.lblFromLoc.BackColor = System.Drawing.Color.Transparent;
            this.lblFromLoc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromLoc.Location = new System.Drawing.Point(9, 313);
            this.lblFromLoc.Name = "lblFromLoc";
            this.lblFromLoc.Size = new System.Drawing.Size(104, 22);
            this.lblFromLoc.TabIndex = 272;
            this.lblFromLoc.Text = "From Location";
            // 
            // radLabel13
            // 
            this.radLabel13.BackColor = System.Drawing.Color.Transparent;
            this.radLabel13.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel13.Location = new System.Drawing.Point(9, 262);
            this.radLabel13.Name = "radLabel13";
            this.radLabel13.Size = new System.Drawing.Size(42, 22);
            this.radLabel13.TabIndex = 271;
            this.radLabel13.Text = "From";
            // 
            // ddlFromLocType
            // 
            this.ddlFromLocType.BackColor = System.Drawing.Color.White;
            this.ddlFromLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlFromLocType.Location = new System.Drawing.Point(116, 260);
            this.ddlFromLocType.Name = "ddlFromLocType";
            this.ddlFromLocType.NewValue = null;
            this.ddlFromLocType.OldValue = null;
            // 
            // 
            // 
            this.ddlFromLocType.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlFromLocType.ShowDropDownArrow = Telerik.WinControls.ElementVisibility.Visible;
            this.ddlFromLocType.Size = new System.Drawing.Size(170, 23);
            this.ddlFromLocType.TabIndex = 259;
            this.ddlFromLocType.TabStop = false;
            // 
            // ddlFromLocation
            // 
            this.ddlFromLocation.BackColor = System.Drawing.Color.White;
            this.ddlFromLocation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlFromLocation.Location = new System.Drawing.Point(116, 311);
            this.ddlFromLocation.Name = "ddlFromLocation";
            this.ddlFromLocation.NewValue = null;
            this.ddlFromLocation.OldValue = null;
            // 
            // 
            // 
            this.ddlFromLocation.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlFromLocation.ShowDropDownArrow = Telerik.WinControls.ElementVisibility.Visible;
            this.ddlFromLocation.Size = new System.Drawing.Size(225, 23);
            this.ddlFromLocation.TabIndex = 260;
            this.ddlFromLocation.TabStop = false;
            // 
            // radLabel9
            // 
            this.radLabel9.BackColor = System.Drawing.Color.Transparent;
            this.radLabel9.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel9.Location = new System.Drawing.Point(358, 262);
            this.radLabel9.Name = "radLabel9";
            this.radLabel9.Size = new System.Drawing.Size(25, 22);
            this.radLabel9.TabIndex = 273;
            this.radLabel9.Text = "To";
            // 
            // ddlToLocType
            // 
            this.ddlToLocType.BackColor = System.Drawing.Color.White;
            this.ddlToLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlToLocType.Location = new System.Drawing.Point(459, 260);
            this.ddlToLocType.Name = "ddlToLocType";
            this.ddlToLocType.NewValue = null;
            this.ddlToLocType.OldValue = null;
            // 
            // 
            // 
            this.ddlToLocType.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlToLocType.ShowDropDownArrow = Telerik.WinControls.ElementVisibility.Visible;
            this.ddlToLocType.Size = new System.Drawing.Size(168, 23);
            this.ddlToLocType.TabIndex = 264;
            this.ddlToLocType.TabStop = false;
            // 
            // lblToLoc
            // 
            this.lblToLoc.BackColor = System.Drawing.Color.Transparent;
            this.lblToLoc.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToLoc.Location = new System.Drawing.Point(357, 313);
            this.lblToLoc.Name = "lblToLoc";
            this.lblToLoc.Size = new System.Drawing.Size(87, 22);
            this.lblToLoc.TabIndex = 274;
            this.lblToLoc.Text = "To Location";
            // 
            // txtToStreetComing
            // 
            this.txtToStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToStreetComing.Location = new System.Drawing.Point(459, 370);
            this.txtToStreetComing.MaxLength = 100;
            this.txtToStreetComing.Name = "txtToStreetComing";
            this.txtToStreetComing.Size = new System.Drawing.Size(206, 24);
            this.txtToStreetComing.TabIndex = 270;
            this.txtToStreetComing.TabStop = false;
            // 
            // ddlToLocation
            // 
            this.ddlToLocation.BackColor = System.Drawing.Color.White;
            this.ddlToLocation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlToLocation.Location = new System.Drawing.Point(459, 312);
            this.ddlToLocation.Name = "ddlToLocation";
            this.ddlToLocation.NewValue = null;
            this.ddlToLocation.OldValue = null;
            // 
            // 
            // 
            this.ddlToLocation.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlToLocation.ShowDropDownArrow = Telerik.WinControls.ElementVisibility.Visible;
            this.ddlToLocation.Size = new System.Drawing.Size(215, 23);
            this.ddlToLocation.TabIndex = 267;
            this.ddlToLocation.TabStop = false;
            // 
            // txtToPostCode
            // 
            this.txtToPostCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtToPostCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtToPostCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtToPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToPostCode.Location = new System.Drawing.Point(459, 312);
            this.txtToPostCode.MaxLength = 100;
            this.txtToPostCode.Name = "txtToPostCode";
            this.txtToPostCode.Size = new System.Drawing.Size(206, 24);
            this.txtToPostCode.TabIndex = 266;
            this.txtToPostCode.TabStop = false;
            // 
            // lblToStreetComing
            // 
            this.lblToStreetComing.BackColor = System.Drawing.Color.Transparent;
            this.lblToStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToStreetComing.Location = new System.Drawing.Point(357, 371);
            this.lblToStreetComing.Name = "lblToStreetComing";
            this.lblToStreetComing.Size = new System.Drawing.Size(71, 22);
            this.lblToStreetComing.TabIndex = 278;
            this.lblToStreetComing.Text = "To Street";
            // 
            // txtToFlightDoorNo
            // 
            this.txtToFlightDoorNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToFlightDoorNo.Location = new System.Drawing.Point(459, 286);
            this.txtToFlightDoorNo.MaxLength = 100;
            this.txtToFlightDoorNo.Name = "txtToFlightDoorNo";
            this.txtToFlightDoorNo.Size = new System.Drawing.Size(215, 24);
            this.txtToFlightDoorNo.TabIndex = 269;
            this.txtToFlightDoorNo.TabStop = false;
            // 
            // lblToDoorFlightNo
            // 
            this.lblToDoorFlightNo.BackColor = System.Drawing.Color.Transparent;
            this.lblToDoorFlightNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDoorFlightNo.Location = new System.Drawing.Point(357, 288);
            this.lblToDoorFlightNo.Name = "lblToDoorFlightNo";
            this.lblToDoorFlightNo.Size = new System.Drawing.Size(56, 22);
            this.lblToDoorFlightNo.TabIndex = 277;
            this.lblToDoorFlightNo.Text = "Door #";
            // 
            // ddlJourneyType
            // 
            this.ddlJourneyType.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem1.Selected = true;
            radListDataItem1.Text = "Booking 1 (O/W)";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "Booking 2 (R/T)";
            radListDataItem2.TextWrap = true;
            this.ddlJourneyType.Items.Add(radListDataItem1);
            this.ddlJourneyType.Items.Add(radListDataItem2);
            this.ddlJourneyType.Location = new System.Drawing.Point(14, 227);
            this.ddlJourneyType.Name = "ddlJourneyType";
            this.ddlJourneyType.Size = new System.Drawing.Size(154, 22);
            this.ddlJourneyType.TabIndex = 258;
            this.ddlJourneyType.Text = "Booking 1 (O/W)";
            // 
            // chkDestination
            // 
            this.chkDestination.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDestination.Location = new System.Drawing.Point(458, 230);
            this.chkDestination.Name = "chkDestination";
            // 
            // 
            // 
            this.chkDestination.RootElement.StretchHorizontally = true;
            this.chkDestination.RootElement.StretchVertically = true;
            this.chkDestination.Size = new System.Drawing.Size(156, 18);
            this.chkDestination.TabIndex = 256;
            this.chkDestination.Text = "Change Destination";
            this.chkDestination.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkDestination_ToggleStateChanged);
            // 
            // chkReturnFares
            // 
            this.chkReturnFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReturnFares.Location = new System.Drawing.Point(263, 133);
            this.chkReturnFares.Name = "chkReturnFares";
            // 
            // 
            // 
            this.chkReturnFares.RootElement.StretchHorizontally = true;
            this.chkReturnFares.RootElement.StretchVertically = true;
            this.chkReturnFares.Size = new System.Drawing.Size(154, 18);
            this.chkReturnFares.TabIndex = 257;
            this.chkReturnFares.Text = "Return Fares    £ :";
            this.chkReturnFares.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkReturnFares_ToggleStateChanged);
            // 
            // chkPickup
            // 
            this.chkPickup.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPickup.Location = new System.Drawing.Point(175, 227);
            this.chkPickup.Name = "chkPickup";
            // 
            // 
            // 
            this.chkPickup.RootElement.StretchHorizontally = true;
            this.chkPickup.RootElement.StretchVertically = true;
            this.chkPickup.Size = new System.Drawing.Size(159, 21);
            this.chkPickup.TabIndex = 255;
            this.chkPickup.Text = "Change Pickup Point";
            this.chkPickup.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkPickup_ToggleStateChanged);
            // 
            // chkPickupFares
            // 
            this.chkPickupFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPickupFares.Location = new System.Drawing.Point(14, 134);
            this.chkPickupFares.Name = "chkPickupFares";
            // 
            // 
            // 
            this.chkPickupFares.RootElement.StretchHorizontally = true;
            this.chkPickupFares.RootElement.StretchVertically = true;
            this.chkPickupFares.Size = new System.Drawing.Size(107, 18);
            this.chkPickupFares.TabIndex = 256;
            this.chkPickupFares.Text = "Fares    £ :";
            this.chkPickupFares.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkPickupFares_ToggleStateChanged);
            // 
            // chkReturnPickupTime
            // 
            this.chkReturnPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReturnPickupTime.Location = new System.Drawing.Point(263, 102);
            this.chkReturnPickupTime.Name = "chkReturnPickupTime";
            // 
            // 
            // 
            this.chkReturnPickupTime.RootElement.StretchHorizontally = true;
            this.chkReturnPickupTime.RootElement.StretchVertically = true;
            this.chkReturnPickupTime.Size = new System.Drawing.Size(167, 18);
            this.chkReturnPickupTime.TabIndex = 255;
            this.chkReturnPickupTime.Text = "Return Pickup Time :";
            this.chkReturnPickupTime.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkReturnPickupTime_ToggleStateChanged);
            // 
            // chkPickupTime
            // 
            this.chkPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPickupTime.Location = new System.Drawing.Point(14, 100);
            this.chkPickupTime.Name = "chkPickupTime";
            // 
            // 
            // 
            this.chkPickupTime.RootElement.StretchHorizontally = true;
            this.chkPickupTime.RootElement.StretchVertically = true;
            this.chkPickupTime.Size = new System.Drawing.Size(119, 18);
            this.chkPickupTime.TabIndex = 254;
            this.chkPickupTime.Text = "Pickup Time :";
            this.chkPickupTime.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkPickupTime_ToggleStateChanged);
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radGroupBox2.Controls.Add(this.radLabel1);
            this.radGroupBox2.Controls.Add(this.btnInclude);
            this.radGroupBox2.Controls.Add(this.radLabel3);
            this.radGroupBox2.Controls.Add(this.grdExcludePickupDates);
            this.radGroupBox2.Controls.Add(this.btnExclude);
            this.radGroupBox2.Controls.Add(this.grdPickupDates);
            this.radGroupBox2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGroupBox2.FooterImageIndex = -1;
            this.radGroupBox2.FooterImageKey = "";
            this.radGroupBox2.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox2.HeaderImageIndex = -1;
            this.radGroupBox2.HeaderImageKey = "";
            this.radGroupBox2.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox2.HeaderText = "Bookings To Delete/Exclude";
            this.radGroupBox2.Location = new System.Drawing.Point(9, 404);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox2.Size = new System.Drawing.Size(694, 189);
            this.radGroupBox2.TabIndex = 253;
            this.radGroupBox2.Text = "Bookings To Delete/Exclude";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.Blue;
            this.radLabel1.Location = new System.Drawing.Point(90, 27);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.radLabel1.Size = new System.Drawing.Size(108, 22);
            this.radLabel1.TabIndex = 251;
            this.radLabel1.Text = "Pickup Dates";
            // 
            // btnInclude
            // 
            this.btnInclude.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInclude.Location = new System.Drawing.Point(338, 110);
            this.btnInclude.Name = "btnInclude";
            this.btnInclude.Size = new System.Drawing.Size(38, 29);
            this.btnInclude.TabIndex = 250;
            this.btnInclude.Text = "<";
            this.btnInclude.Click += new System.EventHandler(this.btnInclude_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnInclude.GetChildAt(0))).Text = "<";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnInclude.GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(475, 26);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(173, 22);
            this.radLabel3.TabIndex = 249;
            this.radLabel3.Text = "Exclude Pickup Dates";
            // 
            // grdExcludePickupDates
            // 
            this.grdExcludePickupDates.Location = new System.Drawing.Point(420, 51);
            this.grdExcludePickupDates.Name = "grdExcludePickupDates";
            this.grdExcludePickupDates.Size = new System.Drawing.Size(260, 125);
            this.grdExcludePickupDates.TabIndex = 248;
            this.grdExcludePickupDates.Text = "radGridView2";
            // 
            // btnExclude
            // 
            this.btnExclude.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExclude.Location = new System.Drawing.Point(338, 67);
            this.btnExclude.Name = "btnExclude";
            this.btnExclude.Size = new System.Drawing.Size(38, 29);
            this.btnExclude.TabIndex = 1;
            this.btnExclude.Text = ">";
            this.btnExclude.Click += new System.EventHandler(this.btnExclude_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExclude.GetChildAt(0))).Text = ">";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExclude.GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdPickupDates
            // 
            this.grdPickupDates.Location = new System.Drawing.Point(29, 51);
            this.grdPickupDates.Name = "grdPickupDates";
            this.grdPickupDates.Size = new System.Drawing.Size(260, 125);
            this.grdPickupDates.TabIndex = 0;
            this.grdPickupDates.Text = "radGridView1";
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.CustomerName);
            this.radGroupBox1.Controls.Add(this.txtContactDetails);
            this.radGroupBox1.Controls.Add(this.ddlDropOffPlot);
            this.radGroupBox1.Controls.Add(this.ddlPickupPlot);
            this.radGroupBox1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Customer Details";
            this.radGroupBox1.Location = new System.Drawing.Point(8, 4);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(695, 56);
            this.radGroupBox1.TabIndex = 252;
            this.radGroupBox1.Text = "Customer Details";
            // 
            // CustomerName
            // 
            this.CustomerName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerName.ForeColor = System.Drawing.Color.Blue;
            this.CustomerName.Location = new System.Drawing.Point(6, 24);
            this.CustomerName.Name = "CustomerName";
            // 
            // 
            // 
            this.CustomerName.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.CustomerName.Size = new System.Drawing.Size(145, 23);
            this.CustomerName.TabIndex = 250;
            this.CustomerName.Text = "Contact Details :";
            // 
            // txtContactDetails
            // 
            this.txtContactDetails.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactDetails.ForeColor = System.Drawing.Color.Blue;
            this.txtContactDetails.Location = new System.Drawing.Point(236, 26);
            this.txtContactDetails.Name = "txtContactDetails";
            // 
            // 
            // 
            this.txtContactDetails.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.txtContactDetails.Size = new System.Drawing.Size(145, 23);
            this.txtContactDetails.TabIndex = 249;
            this.txtContactDetails.Text = "Contact Details :";
            // 
            // ddlDropOffPlot
            // 
            this.ddlDropOffPlot.Location = new System.Drawing.Point(-5, -3);
            this.ddlDropOffPlot.Name = "ddlDropOffPlot";
            this.ddlDropOffPlot.Size = new System.Drawing.Size(121, 21);
            this.ddlDropOffPlot.TabIndex = 0;
            this.ddlDropOffPlot.Visible = false;
            // 
            // ddlPickupPlot
            // 
            this.ddlPickupPlot.Location = new System.Drawing.Point(1, -1);
            this.ddlPickupPlot.Name = "ddlPickupPlot";
            this.ddlPickupPlot.Size = new System.Drawing.Size(121, 21);
            this.ddlPickupPlot.TabIndex = 1;
            this.ddlPickupPlot.Visible = false;
            // 
            // chkEndingAt
            // 
            this.chkEndingAt.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.chkEndingAt.Location = new System.Drawing.Point(321, 69);
            this.chkEndingAt.Name = "chkEndingAt";
            // 
            // 
            // 
            this.chkEndingAt.RootElement.StretchHorizontally = true;
            this.chkEndingAt.RootElement.StretchVertically = true;
            this.chkEndingAt.Size = new System.Drawing.Size(101, 18);
            this.chkEndingAt.TabIndex = 251;
            this.chkEndingAt.Text = "Till End";
            this.chkEndingAt.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkEndingAt_ToggleStateChanged);
            // 
            // dtpReturnPickupTime
            // 
            this.dtpReturnPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpReturnPickupTime.CustomFormat = "HH:mm";
            this.dtpReturnPickupTime.Enabled = false;
            this.dtpReturnPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpReturnPickupTime.Location = new System.Drawing.Point(468, 100);
            this.dtpReturnPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpReturnPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpReturnPickupTime.Name = "dtpReturnPickupTime";
            this.dtpReturnPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpReturnPickupTime.ShowUpDown = true;
            this.dtpReturnPickupTime.Size = new System.Drawing.Size(77, 24);
            this.dtpReturnPickupTime.TabIndex = 249;
            this.dtpReturnPickupTime.TabStop = false;
            this.dtpReturnPickupTime.Text = "myDatePicker2";
            this.dtpReturnPickupTime.Value = null;
            // 
            // dtpEndingAt
            // 
            this.dtpEndingAt.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpEndingAt.CustomFormat = "dd/MM/yyyy";
            this.dtpEndingAt.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndingAt.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpEndingAt.Location = new System.Drawing.Point(467, 67);
            this.dtpEndingAt.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEndingAt.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEndingAt.Name = "dtpEndingAt";
            this.dtpEndingAt.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEndingAt.Size = new System.Drawing.Size(102, 24);
            this.dtpEndingAt.TabIndex = 250;
            this.dtpEndingAt.TabStop = false;
            this.dtpEndingAt.Text = "myDatePicker1";
            this.dtpEndingAt.Value = null;
            // 
            // chkStartingAt
            // 
            this.chkStartingAt.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.chkStartingAt.Location = new System.Drawing.Point(14, 63);
            this.chkStartingAt.Name = "chkStartingAt";
            // 
            // 
            // 
            this.chkStartingAt.RootElement.StretchHorizontally = true;
            this.chkStartingAt.RootElement.StretchVertically = true;
            this.chkStartingAt.Size = new System.Drawing.Size(170, 30);
            this.chkStartingAt.TabIndex = 249;
            this.chkStartingAt.Text = "From Beginning";
            this.chkStartingAt.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radCheckBox1_ToggleStateChanged);
            // 
            // dtpPickupTime
            // 
            this.dtpPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpPickupTime.CustomFormat = "HH:mm";
            this.dtpPickupTime.Enabled = false;
            this.dtpPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpPickupTime.Location = new System.Drawing.Point(175, 99);
            this.dtpPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPickupTime.Name = "dtpPickupTime";
            this.dtpPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpPickupTime.ShowUpDown = true;
            this.dtpPickupTime.Size = new System.Drawing.Size(76, 24);
            this.dtpPickupTime.TabIndex = 247;
            this.dtpPickupTime.TabStop = false;
            this.dtpPickupTime.Text = "myDatePicker1";
            this.dtpPickupTime.Value = null;
            // 
            // dtpStartingAt
            // 
            this.dtpStartingAt.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpStartingAt.CustomFormat = "dd/MM/yyyy";
            this.dtpStartingAt.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartingAt.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpStartingAt.Location = new System.Drawing.Point(186, 67);
            this.dtpStartingAt.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartingAt.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartingAt.Name = "dtpStartingAt";
            this.dtpStartingAt.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartingAt.Size = new System.Drawing.Size(102, 24);
            this.dtpStartingAt.TabIndex = 246;
            this.dtpStartingAt.TabStop = false;
            this.dtpStartingAt.Text = "myDatePicker1";
            this.dtpStartingAt.Value = null;
            // 
            // numReturnFares
            // 
            this.numReturnFares.DecimalPlaces = 2;
            this.numReturnFares.Enabled = false;
            this.numReturnFares.EnableKeyMap = true;
            this.numReturnFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReturnFares.InterceptArrowKeys = false;
            this.numReturnFares.Location = new System.Drawing.Point(468, 132);
            this.numReturnFares.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numReturnFares.Name = "numReturnFares";
            // 
            // 
            // 
            this.numReturnFares.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numReturnFares.ShowBorder = true;
            this.numReturnFares.ShowUpDownButtons = false;
            this.numReturnFares.Size = new System.Drawing.Size(77, 24);
            this.numReturnFares.TabIndex = 129;
            this.numReturnFares.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnFares.GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnFares.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numReturnFares.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numPickupFares
            // 
            this.numPickupFares.DecimalPlaces = 2;
            this.numPickupFares.Enabled = false;
            this.numPickupFares.EnableKeyMap = true;
            this.numPickupFares.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numPickupFares.InterceptArrowKeys = false;
            this.numPickupFares.Location = new System.Drawing.Point(175, 131);
            this.numPickupFares.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numPickupFares.Name = "numPickupFares";
            // 
            // 
            // 
            this.numPickupFares.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numPickupFares.ShowBorder = true;
            this.numPickupFares.ShowUpDownButtons = false;
            this.numPickupFares.Size = new System.Drawing.Size(75, 24);
            this.numPickupFares.TabIndex = 24;
            this.numPickupFares.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numPickupFares.GetChildAt(0))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numPickupFares.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numPickupFares.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numPickupFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numPickupFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.Image = global::Taxi_AppMain.Properties.Resources.Book;
            this.btnApplyChanges.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnApplyChanges.Location = new System.Drawing.Point(1065, 276);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(124, 119);
            this.btnApplyChanges.TabIndex = 247;
            this.btnApplyChanges.Text = "Apply Changes";
            this.btnApplyChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApplyChanges.TextWrap = true;
            this.btnApplyChanges.Click += new System.EventHandler(this.btnApplyChanges_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApplyChanges.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Book;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApplyChanges.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApplyChanges.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApplyChanges.GetChildAt(0))).Text = "Apply Changes";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApplyChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApplyChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApplyChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlVehicleType
            // 
            this.ddlVehicleType.Caption = null;
            this.ddlVehicleType.Location = new System.Drawing.Point(4, 135);
            this.ddlVehicleType.Name = "ddlVehicleType";
            this.ddlVehicleType.Property = null;
            this.ddlVehicleType.ShowDownArrow = true;
            this.ddlVehicleType.Size = new System.Drawing.Size(106, 22);
            this.ddlVehicleType.TabIndex = 254;
            this.ddlVehicleType.Visible = false;
            // 
            // ddlCompany
            // 
            this.ddlCompany.Caption = null;
            this.ddlCompany.Location = new System.Drawing.Point(1, 132);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Property = null;
            this.ddlCompany.ShowDownArrow = true;
            this.ddlCompany.Size = new System.Drawing.Size(106, 22);
            this.ddlCompany.TabIndex = 257;
            this.ddlCompany.Visible = false;
            // 
            // gbCancelBooking
            // 
            this.gbCancelBooking.Controls.Add(this.btnApplyCancelChanges);
            this.gbCancelBooking.Controls.Add(this.btnSaveChanges);
            this.gbCancelBooking.Controls.Add(this.radLabel4);
            this.gbCancelBooking.Controls.Add(this.dtpEndingCancel);
            this.gbCancelBooking.Controls.Add(this.radLabel2);
            this.gbCancelBooking.Controls.Add(this.dtpStartingCancel);
            this.gbCancelBooking.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbCancelBooking.Location = new System.Drawing.Point(1037, 132);
            this.gbCancelBooking.Name = "gbCancelBooking";
            this.gbCancelBooking.Size = new System.Drawing.Size(193, 115);
            this.gbCancelBooking.TabIndex = 285;
            this.gbCancelBooking.TabStop = false;
            this.gbCancelBooking.Text = "Cancel Booking";
            this.gbCancelBooking.Visible = false;
            // 
            // btnApplyCancelChanges
            // 
            this.btnApplyCancelChanges.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnApplyCancelChanges.Location = new System.Drawing.Point(3, 85);
            this.btnApplyCancelChanges.Name = "btnApplyCancelChanges";
            this.btnApplyCancelChanges.Size = new System.Drawing.Size(89, 21);
            this.btnApplyCancelChanges.TabIndex = 287;
            this.btnApplyCancelChanges.Text = "Apply";
            this.btnApplyCancelChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnApplyCancelChanges.TextWrap = true;
            this.btnApplyCancelChanges.Click += new System.EventHandler(this.btnApplyCancelChanges_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApplyCancelChanges.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApplyCancelChanges.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApplyCancelChanges.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApplyCancelChanges.GetChildAt(0))).Text = "Apply";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApplyCancelChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApplyCancelChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApplyCancelChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveChanges.Location = new System.Drawing.Point(101, 85);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(89, 21);
            this.btnSaveChanges.TabIndex = 286;
            this.btnSaveChanges.Text = "Save";
            this.btnSaveChanges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveChanges.TextWrap = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveChanges.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveChanges.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveChanges.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveChanges.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel4
            // 
            this.radLabel4.BackColor = System.Drawing.Color.Transparent;
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(5, 56);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(69, 22);
            this.radLabel4.TabIndex = 273;
            this.radLabel4.Text = "Till Date ";
            // 
            // dtpEndingCancel
            // 
            this.dtpEndingCancel.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpEndingCancel.CustomFormat = "dd/MM/yyyy";
            this.dtpEndingCancel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndingCancel.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpEndingCancel.Location = new System.Drawing.Point(93, 54);
            this.dtpEndingCancel.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEndingCancel.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEndingCancel.Name = "dtpEndingCancel";
            this.dtpEndingCancel.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEndingCancel.Size = new System.Drawing.Size(102, 24);
            this.dtpEndingCancel.TabIndex = 284;
            this.dtpEndingCancel.TabStop = false;
            this.dtpEndingCancel.Text = "myDatePicker1";
            this.dtpEndingCancel.Value = null;
            // 
            // radLabel2
            // 
            this.radLabel2.BackColor = System.Drawing.Color.Transparent;
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(5, 25);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(84, 22);
            this.radLabel2.TabIndex = 272;
            this.radLabel2.Text = "From Date ";
            // 
            // dtpStartingCancel
            // 
            this.dtpStartingCancel.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpStartingCancel.CustomFormat = "dd/MM/yyyy";
            this.dtpStartingCancel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartingCancel.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpStartingCancel.Location = new System.Drawing.Point(93, 25);
            this.dtpStartingCancel.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartingCancel.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartingCancel.Name = "dtpStartingCancel";
            this.dtpStartingCancel.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartingCancel.Size = new System.Drawing.Size(102, 24);
            this.dtpStartingCancel.TabIndex = 283;
            this.dtpStartingCancel.TabStop = false;
            this.dtpStartingCancel.Text = "myDatePicker1";
            this.dtpStartingCancel.Value = null;
            // 
            // btnSaveBooking
            // 
            this.btnSaveBooking.Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            this.btnSaveBooking.Location = new System.Drawing.Point(976, 3);
            this.btnSaveBooking.Name = "btnSaveBooking";
            this.btnSaveBooking.Size = new System.Drawing.Size(183, 44);
            this.btnSaveBooking.TabIndex = 219;
            this.btnSaveBooking.Text = "Save and Close";
            this.btnSaveBooking.Click += new System.EventHandler(this.btnSaveBooking_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveBooking.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveBooking.GetChildAt(0))).Text = "Save and Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radPanel4
            // 
            this.radPanel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel4.Controls.Add(this.btnSaveBooking);
            this.radPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel4.ForeColor = System.Drawing.Color.SteelBlue;
            this.radPanel4.Location = new System.Drawing.Point(0, 791);
            this.radPanel4.Name = "radPanel4";
            // 
            // 
            // 
            this.radPanel4.RootElement.ForeColor = System.Drawing.Color.SteelBlue;
            this.radPanel4.Size = new System.Drawing.Size(1234, 51);
            this.radPanel4.TabIndex = 225;
            this.radPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.radPanel4_Paint);
            // 
            // txtBookedBy
            // 
            this.txtBookedBy.AutoSize = false;
            this.txtBookedBy.BackColor = System.Drawing.Color.RoyalBlue;
            this.txtBookedBy.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookedBy.ForeColor = System.Drawing.Color.Yellow;
            this.txtBookedBy.Location = new System.Drawing.Point(495, 5);
            this.txtBookedBy.Name = "txtBookedBy";
            // 
            // 
            // 
            this.txtBookedBy.RootElement.ForeColor = System.Drawing.Color.Yellow;
            this.txtBookedBy.Size = new System.Drawing.Size(353, 22);
            this.txtBookedBy.TabIndex = 247;
            this.txtBookedBy.Text = "Booked By : Danish on 11/11/2014 @ 11:50";
            this.txtBookedBy.TextAlignment = System.Drawing.ContentAlignment.TopRight;
            // 
            // opt_JOneWay
            // 
            this.opt_JOneWay.Location = new System.Drawing.Point(0, 0);
            this.opt_JOneWay.Name = "opt_JOneWay";
            this.opt_JOneWay.Size = new System.Drawing.Size(110, 18);
            this.opt_JOneWay.TabIndex = 256;
            // 
            // opt_WaitandReturn
            // 
            this.opt_WaitandReturn.Location = new System.Drawing.Point(0, 0);
            this.opt_WaitandReturn.Name = "opt_WaitandReturn";
            this.opt_WaitandReturn.Size = new System.Drawing.Size(110, 18);
            this.opt_WaitandReturn.TabIndex = 107;
            // 
            // radLabel18
            // 
            this.radLabel18.Location = new System.Drawing.Point(0, 0);
            this.radLabel18.Name = "radLabel18";
            this.radLabel18.Size = new System.Drawing.Size(2, 2);
            this.radLabel18.TabIndex = 0;
            // 
            // numTotalChrgs
            // 
            this.numTotalChrgs.Location = new System.Drawing.Point(0, 0);
            this.numTotalChrgs.Name = "numTotalChrgs";
            // 
            // 
            // 
            this.numTotalChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numTotalChrgs.ShowBorder = true;
            this.numTotalChrgs.Size = new System.Drawing.Size(100, 21);
            this.numTotalChrgs.TabIndex = 0;
            this.numTotalChrgs.TabStop = false;
            // 
            // radLabel20
            // 
            this.radLabel20.Location = new System.Drawing.Point(0, 0);
            this.radLabel20.Name = "radLabel20";
            this.radLabel20.Size = new System.Drawing.Size(2, 2);
            this.radLabel20.TabIndex = 0;
            // 
            // numCongChrgs
            // 
            this.numCongChrgs.Location = new System.Drawing.Point(0, 0);
            this.numCongChrgs.Name = "numCongChrgs";
            // 
            // 
            // 
            this.numCongChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCongChrgs.ShowBorder = true;
            this.numCongChrgs.Size = new System.Drawing.Size(100, 21);
            this.numCongChrgs.TabIndex = 0;
            this.numCongChrgs.TabStop = false;
            // 
            // radLabel16
            // 
            this.radLabel16.Location = new System.Drawing.Point(0, 0);
            this.radLabel16.Name = "radLabel16";
            this.radLabel16.Size = new System.Drawing.Size(2, 2);
            this.radLabel16.TabIndex = 0;
            // 
            // numMeetCharges
            // 
            this.numMeetCharges.Location = new System.Drawing.Point(0, 0);
            this.numMeetCharges.Name = "numMeetCharges";
            // 
            // 
            // 
            this.numMeetCharges.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numMeetCharges.ShowBorder = true;
            this.numMeetCharges.Size = new System.Drawing.Size(100, 21);
            this.numMeetCharges.TabIndex = 0;
            this.numMeetCharges.TabStop = false;
            // 
            // radLabel17
            // 
            this.radLabel17.Location = new System.Drawing.Point(0, 0);
            this.radLabel17.Name = "radLabel17";
            this.radLabel17.Size = new System.Drawing.Size(2, 2);
            this.radLabel17.TabIndex = 0;
            // 
            // numExtraChrgs
            // 
            this.numExtraChrgs.Location = new System.Drawing.Point(0, 0);
            this.numExtraChrgs.Name = "numExtraChrgs";
            // 
            // 
            // 
            this.numExtraChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numExtraChrgs.ShowBorder = true;
            this.numExtraChrgs.Size = new System.Drawing.Size(100, 21);
            this.numExtraChrgs.TabIndex = 0;
            this.numExtraChrgs.TabStop = false;
            // 
            // radLabel10
            // 
            this.radLabel10.Location = new System.Drawing.Point(0, 0);
            this.radLabel10.Name = "radLabel10";
            this.radLabel10.Size = new System.Drawing.Size(2, 2);
            this.radLabel10.TabIndex = 0;
            // 
            // numWaitingChrgs
            // 
            this.numWaitingChrgs.Location = new System.Drawing.Point(0, 0);
            this.numWaitingChrgs.Name = "numWaitingChrgs";
            // 
            // 
            // 
            this.numWaitingChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numWaitingChrgs.ShowBorder = true;
            this.numWaitingChrgs.Size = new System.Drawing.Size(100, 21);
            this.numWaitingChrgs.TabIndex = 0;
            this.numWaitingChrgs.TabStop = false;
            // 
            // radLabel15
            // 
            this.radLabel15.Location = new System.Drawing.Point(0, 0);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(2, 2);
            this.radLabel15.TabIndex = 0;
            // 
            // numParkingChrgs
            // 
            this.numParkingChrgs.Location = new System.Drawing.Point(0, 0);
            this.numParkingChrgs.Name = "numParkingChrgs";
            // 
            // 
            // 
            this.numParkingChrgs.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numParkingChrgs.ShowBorder = true;
            this.numParkingChrgs.Size = new System.Drawing.Size(100, 21);
            this.numParkingChrgs.TabIndex = 0;
            this.numParkingChrgs.TabStop = false;
            // 
            // lblMap
            // 
            this.lblMap.Location = new System.Drawing.Point(0, 0);
            this.lblMap.Name = "lblMap";
            this.lblMap.Size = new System.Drawing.Size(100, 23);
            this.lblMap.TabIndex = 0;
            // 
            // numFareRate
            // 
            this.numFareRate.Location = new System.Drawing.Point(0, 0);
            this.numFareRate.Name = "numFareRate";
            // 
            // 
            // 
            this.numFareRate.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numFareRate.ShowBorder = true;
            this.numFareRate.Size = new System.Drawing.Size(100, 21);
            this.numFareRate.TabIndex = 0;
            this.numFareRate.TabStop = false;
            // 
            // numCustomerFares
            // 
            this.numCustomerFares.Location = new System.Drawing.Point(0, 0);
            this.numCustomerFares.Name = "numCustomerFares";
            // 
            // 
            // 
            this.numCustomerFares.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCustomerFares.ShowBorder = true;
            this.numCustomerFares.Size = new System.Drawing.Size(100, 21);
            this.numCustomerFares.TabIndex = 0;
            this.numCustomerFares.TabStop = false;
            // 
            // opt_JReturnWay
            // 
            this.opt_JReturnWay.Location = new System.Drawing.Point(0, 0);
            this.opt_JReturnWay.Name = "opt_JReturnWay";
            this.opt_JReturnWay.Size = new System.Drawing.Size(110, 18);
            this.opt_JReturnWay.TabIndex = 255;
            // 
            // frmAdvanceBookingCustomization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 842);
            this.Controls.Add(this.txtBookedBy);
            this.Controls.Add(this.pnlOneWay);
            this.Controls.Add(this.radPanel4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmAdvanceBookingCustomization";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Multi Booking";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMultiBooking_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlOneWay)).EndInit();
            this.pnlOneWay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkCancelBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRevertChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).EndInit();
            this.radPanel5.ResumeLayout(false);
            this.radPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDayWise)).EndInit();
            this.groupDayWise.ResumeLayout(false);
            this.groupDayWise.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDayWiseReturnPickUpTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoSat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoFri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoWed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoTue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDayWisePickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoMon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCompanyPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCustomerPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnCompanyPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnCompanyPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnCustomerFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnCustomerPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompanyPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerPrice)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReturnSpecialRequirement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnSpecialReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpecialReq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelephoneNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMobileNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOrderNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalLuggages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPassengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOrderNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectVia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromStreetComing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromFlightDoorNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromStreetComing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromDoorFlightNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromLoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromLocType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToLocType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToLoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToStreetComing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToStreetComing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtToFlightDoorNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDoorFlightNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlJourneyType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDestination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPickup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPickupFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInclude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcludePickupDates.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdExcludePickupDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExclude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupDates.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtContactDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEndingAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpReturnPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndingAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStartingAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartingAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPickupFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnApplyChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicleType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
            this.gbCancelBooking.ResumeLayout(false);
            this.gbCancelBooking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnApplyCancelChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndingCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartingCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).EndInit();
            this.radPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBookedBy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_JOneWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_WaitandReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCongChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMeetCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWaitingChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numParkingChrgs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustomerFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.opt_JReturnWay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnSaveBooking;
        private Telerik.WinControls.UI.RadPanel radPanel4;
        private UI.MyGridView grdBookings;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadCheckBox chkEndingAt;
        private UI.MyDatePicker dtpEndingAt;
        private Telerik.WinControls.UI.RadCheckBox chkStartingAt;
        private Telerik.WinControls.UI.RadPanel radPanel5;
        private UI.MyDatePicker dtpReturnPickupTime;
        private UI.MyDatePicker dtpPickupTime;
        private Telerik.WinControls.UI.RadSpinEditor numReturnFares;
        private Telerik.WinControls.UI.RadSpinEditor numPickupFares;
        private Telerik.WinControls.UI.RadButton btnApplyChanges;
        private UI.MyDatePicker dtpStartingAt;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadLabel txtContactDetails;
        private Telerik.WinControls.UI.RadLabel txtBookedBy;
        private Telerik.WinControls.UI.RadLabel CustomerName;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private Telerik.WinControls.UI.RadButton btnExclude;
        private Telerik.WinControls.UI.RadGridView grdPickupDates;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadGridView grdExcludePickupDates;
        private Telerik.WinControls.UI.RadButton btnInclude;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadCheckBox chkPickupTime;
        private Telerik.WinControls.UI.RadCheckBox chkReturnFares;
        private Telerik.WinControls.UI.RadCheckBox chkPickupFares;
        private Telerik.WinControls.UI.RadCheckBox chkReturnPickupTime;
        private Telerik.WinControls.UI.RadButton btnRevertChanges;
        private Telerik.WinControls.UI.RadCheckBox chkDestination;
        private Telerik.WinControls.UI.RadCheckBox chkPickup;
        private Telerik.WinControls.UI.RadDropDownList ddlJourneyType;
        private UI.AutoCompleteTextBox txtFromAddress;
        private Telerik.WinControls.UI.RadTextBox txtFromPostCode;
        private Telerik.WinControls.UI.RadTextBox txtFromStreetComing;
        private Telerik.WinControls.UI.RadTextBox txtFromFlightDoorNo;
        private Telerik.WinControls.UI.RadLabel lblFromStreetComing;
        private Telerik.WinControls.UI.RadLabel lblFromDoorFlightNo;
        private UI.AutoCompleteTextBox txtToAddress;
        private Telerik.WinControls.UI.RadLabel lblFromLoc;
        private Telerik.WinControls.UI.RadLabel radLabel13;
        private UI.DJComboBox ddlFromLocType;
        private UI.DJComboBox ddlFromLocation;
        private Telerik.WinControls.UI.RadLabel radLabel9;
        private UI.DJComboBox ddlToLocType;
        private Telerik.WinControls.UI.RadLabel lblToLoc;
        private Telerik.WinControls.UI.RadTextBox txtToStreetComing;
        private UI.DJComboBox ddlToLocation;
        private Telerik.WinControls.UI.RadTextBox txtToPostCode;
        private Telerik.WinControls.UI.RadLabel lblToStreetComing;
        private Telerik.WinControls.UI.RadTextBox txtToFlightDoorNo;
        private Telerik.WinControls.UI.RadLabel lblToDoorFlightNo;
        private Telerik.WinControls.UI.RadToggleButton btnSelectVia;
        /// NC
        private Telerik.WinControls.UI.RadPanel pnlVia;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private UI.AutoCompleteTextBox txtViaAddress;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnAddVia;
        private UI.MyDropDownList ddlViaLocation;
        private Telerik.WinControls.UI.RadGridView grdVia;
        private UI.AutoCompleteTextBox txtviaPostCode;
        private Telerik.WinControls.UI.RadLabel lblViaLoc;
        private Telerik.WinControls.UI.RadLabel lblFromViaPoint;
        private UI.MyDropDownList ddlViaFromLocType;
        private Telerik.WinControls.UI.RadSpinEditor numFareRate;
        private Telerik.WinControls.UI.RadSpinEditor numCustomerFares;
        private Telerik.WinControls.UI.RadSpinEditor numCompanyFares;
        private Telerik.WinControls.UI.RadSpinEditor numReturnFare;
        private Telerik.WinControls.UI.RadRadioButton opt_JReturnWay;
        private Telerik.WinControls.UI.RadRadioButton opt_JOneWay;
        private Telerik.WinControls.UI.RadRadioButton opt_WaitandReturn;
        private System.Windows.Forms.ComboBox ddlDropOffPlot;
        private System.Windows.Forms.ComboBox ddlPickupPlot;
        private UI.MyDropDownList ddlVehicleType;
        private UI.MyDropDownList ddlCompany;
        private Telerik.WinControls.UI.RadLabel radLabel18;
        private Telerik.WinControls.UI.RadSpinEditor numTotalChrgs;
        private Telerik.WinControls.UI.RadLabel radLabel20;
        private Telerik.WinControls.UI.RadSpinEditor numCongChrgs;
        private Telerik.WinControls.UI.RadLabel radLabel16;
        private Telerik.WinControls.UI.RadSpinEditor numMeetCharges;
        private Telerik.WinControls.UI.RadLabel radLabel17;
        private Telerik.WinControls.UI.RadSpinEditor numExtraChrgs;
        private Telerik.WinControls.UI.RadLabel radLabel10;
        private Telerik.WinControls.UI.RadSpinEditor numWaitingChrgs;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        private Telerik.WinControls.UI.RadSpinEditor numParkingChrgs;
        private System.Windows.Forms.Label lblMap;
        private UI.MyDatePicker dtpEndingCancel;
        private UI.MyDatePicker dtpStartingCancel;
        private Telerik.WinControls.UI.RadCheckBox chkCancelBooking;
        private System.Windows.Forms.GroupBox gbCancelBooking;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadButton btnSaveChanges;
        private Telerik.WinControls.UI.RadButton btnApplyCancelChanges;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadTextBox txtReturnSpecialRequirement;
        private Telerik.WinControls.UI.RadLabel lblReturnSpecialReq;
        private Telerik.WinControls.UI.RadTextBox txtCustomerName;
        private Telerik.WinControls.UI.RadTextBox txtSpecialReq;
        private Telerik.WinControls.UI.RadTextBox txtEmail;
        private Telerik.WinControls.UI.RadTextBox txtTelephoneNo;
        private Telerik.WinControls.UI.RadTextBox txtMobileNo;
        private Telerik.WinControls.UI.RadTextBox txtOrderNo;
        private UI.MyDropDownList ddlVehicle;
        private UI.MyDropDownList ddlAccount;
        private Telerik.WinControls.UI.RadSpinEditor numTotalLuggages;
        private Telerik.WinControls.UI.RadSpinEditor num_TotalPassengers;
        private Telerik.WinControls.UI.RadLabel radLabel21;
        private Telerik.WinControls.UI.RadLabel radLabel22;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel12;
        private Telerik.WinControls.UI.RadLabel radLabel14;
        private Telerik.WinControls.UI.RadLabel lblOrderNo;
        private Telerik.WinControls.UI.RadLabel radLabel8;
        private Telerik.WinControls.UI.RadLabel radLabel7;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadLabel lblPassengers;
        private Telerik.WinControls.UI.RadSpinEditor numCustomerPrice;
        private Telerik.WinControls.UI.RadSpinEditor numCompanyPrice;
        private Telerik.WinControls.UI.RadCheckBox chkReturnCustomerPrice;
        private Telerik.WinControls.UI.RadCheckBox chkReturnCompanyPrice;
        private Telerik.WinControls.UI.RadSpinEditor numReturnCustomerFare;
        private Telerik.WinControls.UI.RadSpinEditor numReturnCompanyPrice;
        private Telerik.WinControls.UI.RadCheckBox chkCustomerPrice;
        private Telerik.WinControls.UI.RadCheckBox chkCompanyPrice;
        private System.Windows.Forms.GroupBox groupDayWise;
        private Telerik.WinControls.UI.RadLabel radLabel19;
        private Telerik.WinControls.UI.RadCheckBox chkDayWise;
        private UI.MyDatePicker dtpDayWisePickupTime;
        private Telerik.WinControls.UI.RadRadioButton rdoMon;
        private Telerik.WinControls.UI.RadRadioButton rdoTue;
        private Telerik.WinControls.UI.RadRadioButton rdoWed;
        private Telerik.WinControls.UI.RadRadioButton rdoThu;
        private Telerik.WinControls.UI.RadRadioButton rdoFri;
        private Telerik.WinControls.UI.RadRadioButton rdoSat;
        private Telerik.WinControls.UI.RadRadioButton rdoSun;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private UI.MyDatePicker dtpDayWiseReturnPickUpTime;
        private UI.MyDropDownList ddlPaymentType;
        private Telerik.WinControls.UI.RadLabel radLabel23;
    }
}