namespace Taxi_AppMain
{
    partial class xfrmAdvanceBookingCustomization
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.grdBookings = new UI.MyGridView();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.btnRevertChanges = new Telerik.WinControls.UI.RadButton();
            this.radPanel5 = new Telerik.WinControls.UI.RadPanel();
            this.ddlVehicleType = new UI.MyDropDownList();
            this.ddlCompany = new UI.MyDropDownList();
            this.ddlPickupPlot = new System.Windows.Forms.ComboBox();
            this.ddlDropOffPlot = new System.Windows.Forms.ComboBox();
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
            this.txtCustomerName = new Telerik.WinControls.UI.RadLabel();
            this.txtContactDetails = new Telerik.WinControls.UI.RadLabel();
            this.chkEndingAt = new Telerik.WinControls.UI.RadCheckBox();
            this.dtpReturnPickupTime = new UI.MyDatePicker();
            this.dtpEndingAt = new UI.MyDatePicker();
            this.chkStartingAt = new Telerik.WinControls.UI.RadCheckBox();
            this.dtpPickupTime = new UI.MyDatePicker();
            this.dtpStartingAt = new UI.MyDatePicker();
            this.numReturnFares = new Telerik.WinControls.UI.RadSpinEditor();
            this.numPickupFares = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnApplyChanges = new Telerik.WinControls.UI.RadButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRevertChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).BeginInit();
            this.radPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicleType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(1165, 32);
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
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.grdBookings);
            this.radPanel1.Controls.Add(this.radPanel2);
            this.radPanel1.Location = new System.Drawing.Point(0, 32);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1165, 805);
            this.radPanel1.TabIndex = 106;
            this.radPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.radPanel1_Paint);
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
            this.grdBookings.Size = new System.Drawing.Size(1165, 197);
            this.grdBookings.TabIndex = 218;
            this.grdBookings.Text = "myGridView1";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.btnRevertChanges);
            this.radPanel2.Controls.Add(this.radPanel5);
            this.radPanel2.Controls.Add(this.btnApplyChanges);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel2.Location = new System.Drawing.Point(0, 0);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(1165, 608);
            this.radPanel2.TabIndex = 219;
            // 
            // btnRevertChanges
            // 
            this.btnRevertChanges.Image = global::Taxi_AppMain.Properties.Resources.refresh;
            this.btnRevertChanges.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnRevertChanges.Location = new System.Drawing.Point(1035, 11);
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
            this.radPanel5.Controls.Add(this.ddlVehicleType);
            this.radPanel5.Controls.Add(this.ddlCompany);
            this.radPanel5.Controls.Add(this.ddlPickupPlot);
            this.radPanel5.Controls.Add(this.ddlDropOffPlot);
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
            this.radPanel5.Size = new System.Drawing.Size(1024, 604);
            this.radPanel5.TabIndex = 248;
            // 
            // ddlVehicleType
            // 
            this.ddlVehicleType.Caption = null;
            this.ddlVehicleType.Location = new System.Drawing.Point(699, 74);
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
            this.ddlCompany.Location = new System.Drawing.Point(794, 38);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Property = null;
            this.ddlCompany.ShowDownArrow = true;
            this.ddlCompany.Size = new System.Drawing.Size(106, 22);
            this.ddlCompany.TabIndex = 257;
            this.ddlCompany.Visible = false;
            // 
            // ddlPickupPlot
            // 
            this.ddlPickupPlot.Location = new System.Drawing.Point(794, 8);
            this.ddlPickupPlot.Name = "ddlPickupPlot";
            this.ddlPickupPlot.Size = new System.Drawing.Size(121, 21);
            this.ddlPickupPlot.TabIndex = 1;
            this.ddlPickupPlot.Visible = false;
            // 
            // ddlDropOffPlot
            // 
            this.ddlDropOffPlot.Location = new System.Drawing.Point(753, 30);
            this.ddlDropOffPlot.Name = "ddlDropOffPlot";
            this.ddlDropOffPlot.Size = new System.Drawing.Size(121, 21);
            this.ddlDropOffPlot.TabIndex = 0;
            this.ddlDropOffPlot.Visible = false;
            // 
            // btnSelectVia
            // 
            this.btnSelectVia.Location = new System.Drawing.Point(794, 166);
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
            this.txtFromAddress.Location = new System.Drawing.Point(116, 280);
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
            this.txtFromPostCode.Location = new System.Drawing.Point(116, 281);
            this.txtFromPostCode.MaxLength = 100;
            this.txtFromPostCode.Name = "txtFromPostCode";
            this.txtFromPostCode.Size = new System.Drawing.Size(206, 24);
            this.txtFromPostCode.TabIndex = 261;
            this.txtFromPostCode.TabStop = false;
            // 
            // txtFromStreetComing
            // 
            this.txtFromStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromStreetComing.Location = new System.Drawing.Point(117, 339);
            this.txtFromStreetComing.MaxLength = 100;
            this.txtFromStreetComing.Name = "txtFromStreetComing";
            this.txtFromStreetComing.Size = new System.Drawing.Size(205, 24);
            this.txtFromStreetComing.TabIndex = 265;
            this.txtFromStreetComing.TabStop = false;
            // 
            // txtFromFlightDoorNo
            // 
            this.txtFromFlightDoorNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromFlightDoorNo.Location = new System.Drawing.Point(116, 254);
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
            this.lblFromStreetComing.Location = new System.Drawing.Point(9, 340);
            this.lblFromStreetComing.Name = "lblFromStreetComing";
            this.lblFromStreetComing.Size = new System.Drawing.Size(88, 22);
            this.lblFromStreetComing.TabIndex = 276;
            this.lblFromStreetComing.Text = "From Street";
            // 
            // lblFromDoorFlightNo
            // 
            this.lblFromDoorFlightNo.BackColor = System.Drawing.Color.Transparent;
            this.lblFromDoorFlightNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDoorFlightNo.Location = new System.Drawing.Point(9, 255);
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
            this.txtToAddress.Location = new System.Drawing.Point(459, 280);
            this.txtToAddress.Multiline = true;
            this.txtToAddress.Name = "txtToAddress";
            // 
            // 
            // 
            this.txtToAddress.RootElement.StretchVertically = true;
            this.txtToAddress.SelectedItem = null;
            this.txtToAddress.Size = new System.Drawing.Size(286, 80);
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
            this.lblFromLoc.Location = new System.Drawing.Point(9, 282);
            this.lblFromLoc.Name = "lblFromLoc";
            this.lblFromLoc.Size = new System.Drawing.Size(104, 22);
            this.lblFromLoc.TabIndex = 272;
            this.lblFromLoc.Text = "From Location";
            // 
            // radLabel13
            // 
            this.radLabel13.BackColor = System.Drawing.Color.Transparent;
            this.radLabel13.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel13.Location = new System.Drawing.Point(9, 231);
            this.radLabel13.Name = "radLabel13";
            this.radLabel13.Size = new System.Drawing.Size(42, 22);
            this.radLabel13.TabIndex = 271;
            this.radLabel13.Text = "From";
            // 
            // ddlFromLocType
            // 
            this.ddlFromLocType.BackColor = System.Drawing.Color.White;
            this.ddlFromLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlFromLocType.Location = new System.Drawing.Point(116, 229);
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
            this.ddlFromLocation.Location = new System.Drawing.Point(116, 280);
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
            this.radLabel9.Location = new System.Drawing.Point(358, 231);
            this.radLabel9.Name = "radLabel9";
            this.radLabel9.Size = new System.Drawing.Size(25, 22);
            this.radLabel9.TabIndex = 273;
            this.radLabel9.Text = "To";
            // 
            // ddlToLocType
            // 
            this.ddlToLocType.BackColor = System.Drawing.Color.White;
            this.ddlToLocType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlToLocType.Location = new System.Drawing.Point(459, 229);
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
            this.lblToLoc.Location = new System.Drawing.Point(357, 282);
            this.lblToLoc.Name = "lblToLoc";
            this.lblToLoc.Size = new System.Drawing.Size(87, 22);
            this.lblToLoc.TabIndex = 274;
            this.lblToLoc.Text = "To Location";
            // 
            // txtToStreetComing
            // 
            this.txtToStreetComing.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToStreetComing.Location = new System.Drawing.Point(459, 339);
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
            this.ddlToLocation.Location = new System.Drawing.Point(459, 281);
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
            this.txtToPostCode.Location = new System.Drawing.Point(459, 281);
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
            this.lblToStreetComing.Location = new System.Drawing.Point(357, 340);
            this.lblToStreetComing.Name = "lblToStreetComing";
            this.lblToStreetComing.Size = new System.Drawing.Size(71, 22);
            this.lblToStreetComing.TabIndex = 278;
            this.lblToStreetComing.Text = "To Street";
            // 
            // txtToFlightDoorNo
            // 
            this.txtToFlightDoorNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToFlightDoorNo.Location = new System.Drawing.Point(459, 255);
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
            this.lblToDoorFlightNo.Location = new System.Drawing.Point(357, 257);
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
            this.ddlJourneyType.Location = new System.Drawing.Point(14, 191);
            this.ddlJourneyType.Name = "ddlJourneyType";
            this.ddlJourneyType.Size = new System.Drawing.Size(154, 22);
            this.ddlJourneyType.TabIndex = 258;
            this.ddlJourneyType.Text = "Booking 1 (O/W)";
            // 
            // chkDestination
            // 
            this.chkDestination.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDestination.Location = new System.Drawing.Point(402, 194);
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
            this.chkReturnFares.Location = new System.Drawing.Point(317, 152);
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
            this.chkPickup.Location = new System.Drawing.Point(175, 191);
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
            this.chkPickupFares.Location = new System.Drawing.Point(105, 152);
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
            this.chkReturnPickupTime.Location = new System.Drawing.Point(317, 110);
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
            this.chkPickupTime.Location = new System.Drawing.Point(104, 108);
            this.chkPickupTime.Name = "chkPickupTime";
            // 
            // 
            // 
            this.chkPickupTime.RootElement.StretchHorizontally = true;
            this.chkPickupTime.RootElement.StretchVertically = true;
            this.chkPickupTime.Size = new System.Drawing.Size(119, 18);
            this.chkPickupTime.TabIndex = 254;
            this.chkPickupTime.Text = "Pickup Time :";
            this.chkPickupTime.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
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
            this.radGroupBox2.Location = new System.Drawing.Point(9, 389);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox2.Size = new System.Drawing.Size(694, 196);
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
            this.radGroupBox1.Controls.Add(this.txtCustomerName);
            this.radGroupBox1.Controls.Add(this.txtContactDetails);
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
            // txtCustomerName
            // 
            this.txtCustomerName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerName.ForeColor = System.Drawing.Color.Blue;
            this.txtCustomerName.Location = new System.Drawing.Point(15, 26);
            this.txtCustomerName.Name = "txtCustomerName";
            // 
            // 
            // 
            this.txtCustomerName.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.txtCustomerName.Size = new System.Drawing.Size(145, 23);
            this.txtCustomerName.TabIndex = 250;
            this.txtCustomerName.Text = "Contact Details :";
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
            // chkEndingAt
            // 
            this.chkEndingAt.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.chkEndingAt.Location = new System.Drawing.Point(408, 72);
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
            this.dtpReturnPickupTime.Location = new System.Drawing.Point(491, 107);
            this.dtpReturnPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpReturnPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpReturnPickupTime.Name = "dtpReturnPickupTime";
            this.dtpReturnPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpReturnPickupTime.ShowUpDown = true;
            this.dtpReturnPickupTime.Size = new System.Drawing.Size(76, 24);
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
            this.dtpEndingAt.Location = new System.Drawing.Point(526, 70);
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
            this.chkStartingAt.Location = new System.Drawing.Point(71, 66);
            this.chkStartingAt.Name = "chkStartingAt";
            // 
            // 
            // 
            this.chkStartingAt.RootElement.StretchHorizontally = true;
            this.chkStartingAt.RootElement.StretchVertically = true;
            this.chkStartingAt.Size = new System.Drawing.Size(169, 30);
            this.chkStartingAt.TabIndex = 249;
            this.chkStartingAt.Text = "From Beginning";
            this.chkStartingAt.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radCheckBox1_ToggleStateChanged);
            // 
            // dtpPickupTime
            // 
            this.dtpPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpPickupTime.CustomFormat = "HH:mm";
            this.dtpPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpPickupTime.Location = new System.Drawing.Point(228, 107);
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
            this.dtpStartingAt.Location = new System.Drawing.Point(245, 70);
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
            this.numReturnFares.Location = new System.Drawing.Point(477, 149);
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
            this.numReturnFares.Size = new System.Drawing.Size(69, 24);
            this.numReturnFares.TabIndex = 129;
            this.numReturnFares.TabStop = false;
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
            this.numPickupFares.Location = new System.Drawing.Point(229, 149);
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
            this.numPickupFares.Size = new System.Drawing.Size(69, 24);
            this.numPickupFares.TabIndex = 24;
            this.numPickupFares.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numPickupFares.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numPickupFares.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numPickupFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numPickupFares.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.Image = global::Taxi_AppMain.Properties.Resources.Book;
            this.btnApplyChanges.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnApplyChanges.Location = new System.Drawing.Point(1035, 227);
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
            this.radPanel4.Location = new System.Drawing.Point(0, 827);
            this.radPanel4.Name = "radPanel4";
            // 
            // 
            // 
            this.radPanel4.RootElement.ForeColor = System.Drawing.Color.SteelBlue;
            this.radPanel4.Size = new System.Drawing.Size(1165, 51);
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
            this.ClientSize = new System.Drawing.Size(1165, 878);
            this.Controls.Add(this.txtBookedBy);
            this.Controls.Add(this.radPanel1);
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
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnRevertChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).EndInit();
            this.radPanel5.ResumeLayout(false);
            this.radPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlVehicleType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.txtCustomerName)).EndInit();
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
        private Telerik.WinControls.UI.RadLabel txtCustomerName;
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
    }
}