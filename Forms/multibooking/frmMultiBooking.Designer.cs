namespace Taxi_AppMain
{
    partial class frmMultiBooking
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
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.chkSkipWeekEnd = new Telerik.WinControls.UI.RadCheckBox();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.numReturnFare = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.numFareRate = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnCalculateFares = new Telerik.WinControls.UI.RadButton();
            this.pnlReturn = new System.Windows.Forms.Panel();
            this.radLabel10 = new Telerik.WinControls.UI.RadLabel();
            this.dtp_ReturnStartPickupTime = new UI.MyDatePicker();
            this.dtp_ReturnStartPickupDate = new UI.MyDatePicker();
            this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
            this.chkIsReturn = new Telerik.WinControls.UI.RadCheckBox();
            this.grdPickupDates = new Telerik.WinControls.UI.RadGridView();
            this.btnDeleteRow = new Telerik.WinControls.UI.RadButton();
            this.ddlPickupDay = new UI.MyDropDownList();
            this.btnAddPickupDate = new Telerik.WinControls.UI.RadButton();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.dtp_StartPickupTime = new UI.MyDatePicker();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.dtp_StartPickupDate = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.chkDestination = new Telerik.WinControls.UI.RadCheckBox();
            this.chkOrigin = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.numDays = new Telerik.WinControls.UI.RadSpinEditor();
            this.chkSame = new Telerik.WinControls.UI.RadCheckBox();
            this.btnCreateBooking = new Telerik.WinControls.UI.RadButton();
            this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
            this.ddlPaymentType = new UI.MyDropDownList();
            this.lblDepartment = new Telerik.WinControls.UI.RadLabel();
            this.ddlDepartment = new UI.MyDropDownList();
            this.chkIsCompanyRates = new Telerik.WinControls.UI.RadCheckBox();
            this.ddlCompany = new UI.MyDropDownList();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.grdBookings = new UI.MyGridView();
            this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.pg_Daily = new Telerik.WinControls.UI.RadPageViewPage();
            this.pg_weekly = new Telerik.WinControls.UI.RadPageViewPage();
            this.dtpEndingDate = new UI.MyDatePicker();
            this.radLabel12 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel5 = new Telerik.WinControls.UI.RadPanel();
            this.lblweekcompanyreturnfares = new Telerik.WinControls.UI.RadLabel();
            this.numWeekCompanyFareRateReturn = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel18 = new Telerik.WinControls.UI.RadLabel();
            this.numCompanyFareRateWeek = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblweekcustreturnfares = new Telerik.WinControls.UI.RadLabel();
            this.numWeekCustFareRateReturn = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel16 = new Telerik.WinControls.UI.RadLabel();
            this.numCustFareRateWeek = new Telerik.WinControls.UI.RadSpinEditor();
            this.dtpWeekReturnPickupTime = new UI.MyDatePicker();
            this.chkReturnWeekJourney = new Telerik.WinControls.UI.RadCheckBox();
            this.dtpWeekPickupTime = new UI.MyDatePicker();
            this.radLabel14 = new Telerik.WinControls.UI.RadLabel();
            this.lblReturnWeekPickupTime = new Telerik.WinControls.UI.RadLabel();
            this.lblWeekReturnFare = new Telerik.WinControls.UI.RadLabel();
            this.numWeekFareRateReturn = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel13 = new Telerik.WinControls.UI.RadLabel();
            this.numFareRateWeek = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnCalculateWeekFare = new Telerik.WinControls.UI.RadButton();
            this.btnWeeklyCreateBooking = new Telerik.WinControls.UI.RadButton();
            this.chkSun = new Telerik.WinControls.UI.RadCheckBox();
            this.chkSat = new Telerik.WinControls.UI.RadCheckBox();
            this.chkFri = new Telerik.WinControls.UI.RadCheckBox();
            this.chkThurs = new Telerik.WinControls.UI.RadCheckBox();
            this.chkWed = new Telerik.WinControls.UI.RadCheckBox();
            this.chkAutoRecurred = new Telerik.WinControls.UI.RadCheckBox();
            this.dtpStartingAt = new UI.MyDatePicker();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel9 = new Telerik.WinControls.UI.RadLabel();
            this.numWeeks = new Telerik.WinControls.UI.RadSpinEditor();
            this.chkTue = new Telerik.WinControls.UI.RadCheckBox();
            this.chkMon = new Telerik.WinControls.UI.RadCheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkReturnConfirm = new Telerik.WinControls.UI.RadCheckBox();
            this.ddlReturnAllocatedDriver = new UI.MyDropDownList();
            this.chkConfirmDriver = new Telerik.WinControls.UI.RadCheckBox();
            this.ddlDriver = new UI.MyDropDownList();
            this.btnSaveBooking = new Telerik.WinControls.UI.RadButton();
            this.radPanel4 = new Telerik.WinControls.UI.RadPanel();
            this.optDaily = new Telerik.WinControls.UI.RadRadioButton();
            this.optWeekly = new Telerik.WinControls.UI.RadRadioButton();
            this.lblUpdate = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSkipWeekEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalculateFares)).BeginInit();
            this.pnlReturn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_ReturnStartPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_ReturnStartPickupDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupDates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupDates.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPickupDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPickupDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_StartPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_StartPickupDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDestination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOrigin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCreateBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCompanyRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
            this.radPageView1.SuspendLayout();
            this.pg_Daily.SuspendLayout();
            this.pg_weekly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndingDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).BeginInit();
            this.radPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblweekcompanyreturnfares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeekCompanyFareRateReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompanyFareRateWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblweekcustreturnfares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeekCustFareRateReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustFareRateWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWeekReturnPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnWeekJourney)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWeekPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnWeekPickupTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWeekReturnFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeekFareRateReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRateWeek)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalculateWeekFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWeeklyCreateBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSun)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThurs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoRecurred)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartingAt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeeks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMon)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnConfirm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnAllocatedDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkConfirmDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).BeginInit();
            this.radPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optDaily)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optWeekly)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUpdate)).BeginInit();
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
            this.label1.Size = new System.Drawing.Size(914, 32);
            this.label1.TabIndex = 107;
            this.label1.Text = "Multi Booking";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            // 
            // radPanel2
            // 
            this.radPanel2.BackColor = System.Drawing.Color.Ivory;
            this.radPanel2.Controls.Add(this.chkSkipWeekEnd);
            this.radPanel2.Controls.Add(this.radGroupBox1);
            this.radPanel2.Controls.Add(this.chkDestination);
            this.radPanel2.Controls.Add(this.chkOrigin);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.numDays);
            this.radPanel2.Controls.Add(this.chkSame);
            this.radPanel2.Controls.Add(this.btnCreateBooking);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel2.Location = new System.Drawing.Point(0, 0);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(893, 315);
            this.radPanel2.TabIndex = 217;
            // 
            // chkSkipWeekEnd
            // 
            this.chkSkipWeekEnd.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSkipWeekEnd.Location = new System.Drawing.Point(716, 7);
            this.chkSkipWeekEnd.Name = "chkSkipWeekEnd";
            // 
            // 
            // 
            this.chkSkipWeekEnd.RootElement.StretchHorizontally = true;
            this.chkSkipWeekEnd.RootElement.StretchVertically = true;
            this.chkSkipWeekEnd.Size = new System.Drawing.Size(114, 18);
            this.chkSkipWeekEnd.TabIndex = 212;
            this.chkSkipWeekEnd.Text = "Skip Weekend";
            this.chkSkipWeekEnd.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.radPanel3);
            this.radGroupBox1.Controls.Add(this.pnlReturn);
            this.radGroupBox1.Controls.Add(this.chkIsReturn);
            this.radGroupBox1.Controls.Add(this.grdPickupDates);
            this.radGroupBox1.Controls.Add(this.btnDeleteRow);
            this.radGroupBox1.Controls.Add(this.ddlPickupDay);
            this.radGroupBox1.Controls.Add(this.btnAddPickupDate);
            this.radGroupBox1.Controls.Add(this.radLabel6);
            this.radGroupBox1.Controls.Add(this.dtp_StartPickupTime);
            this.radGroupBox1.Controls.Add(this.radLabel4);
            this.radGroupBox1.Controls.Add(this.dtp_StartPickupDate);
            this.radGroupBox1.Controls.Add(this.radLabel2);
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Pickup & Return Pickup Date";
            this.radGroupBox1.Location = new System.Drawing.Point(6, 48);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(736, 236);
            this.radGroupBox1.TabIndex = 158;
            this.radGroupBox1.Text = "Pickup & Return Pickup Date";
            ((Telerik.WinControls.UI.GroupBoxHeader)(this.radGroupBox1.GetChildAt(0).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radPanel3
            // 
            this.radPanel3.BackColor = System.Drawing.Color.Orange;
            this.radPanel3.Controls.Add(this.radLabel3);
            this.radPanel3.Controls.Add(this.numReturnFare);
            this.radPanel3.Controls.Add(this.radLabel5);
            this.radPanel3.Controls.Add(this.numFareRate);
            this.radPanel3.Controls.Add(this.btnCalculateFares);
            this.radPanel3.Location = new System.Drawing.Point(8, 180);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(295, 55);
            this.radPanel3.TabIndex = 238;
            // 
            // radLabel3
            // 
            this.radLabel3.BackColor = System.Drawing.Color.Transparent;
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.ForeColor = System.Drawing.Color.Black;
            this.radLabel3.Location = new System.Drawing.Point(11, 31);
            this.radLabel3.Name = "radLabel3";
            // 
            // 
            // 
            this.radLabel3.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel3.Size = new System.Drawing.Size(105, 18);
            this.radLabel3.TabIndex = 130;
            this.radLabel3.Text = "Return Fares   £";
            // 
            // numReturnFare
            // 
            this.numReturnFare.DecimalPlaces = 2;
            this.numReturnFare.Enabled = false;
            this.numReturnFare.EnableKeyMap = true;
            this.numReturnFare.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReturnFare.InterceptArrowKeys = false;
            this.numReturnFare.Location = new System.Drawing.Point(123, 29);
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
            this.numReturnFare.Size = new System.Drawing.Size(69, 20);
            this.numReturnFare.TabIndex = 129;
            this.numReturnFare.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReturnFare.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numReturnFare.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnFare.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReturnFare.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel5
            // 
            this.radLabel5.BackColor = System.Drawing.Color.Transparent;
            this.radLabel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.ForeColor = System.Drawing.Color.Black;
            this.radLabel5.Location = new System.Drawing.Point(59, 4);
            this.radLabel5.Name = "radLabel5";
            // 
            // 
            // 
            this.radLabel5.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel5.Size = new System.Drawing.Size(59, 18);
            this.radLabel5.TabIndex = 128;
            this.radLabel5.Text = "Fares   £";
            // 
            // numFareRate
            // 
            this.numFareRate.DecimalPlaces = 2;
            this.numFareRate.EnableKeyMap = true;
            this.numFareRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFareRate.InterceptArrowKeys = false;
            this.numFareRate.Location = new System.Drawing.Point(123, 2);
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
            this.numFareRate.ShowBorder = true;
            this.numFareRate.ShowUpDownButtons = false;
            this.numFareRate.Size = new System.Drawing.Size(69, 20);
            this.numFareRate.TabIndex = 24;
            this.numFareRate.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numFareRate.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numFareRate.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numFareRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numFareRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnCalculateFares
            // 
            this.btnCalculateFares.Image = global::Taxi_AppMain.Properties.Resources.fares;
            this.btnCalculateFares.Location = new System.Drawing.Point(200, 2);
            this.btnCalculateFares.Name = "btnCalculateFares";
            this.btnCalculateFares.Size = new System.Drawing.Size(92, 51);
            this.btnCalculateFares.TabIndex = 141;
            this.btnCalculateFares.TabStop = false;
            this.btnCalculateFares.Text = "Calculate";
            this.btnCalculateFares.Click += new System.EventHandler(this.btnCalculateFares_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCalculateFares.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.fares;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCalculateFares.GetChildAt(0))).Text = "Calculate";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCalculateFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCalculateFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // pnlReturn
            // 
            this.pnlReturn.Controls.Add(this.radLabel10);
            this.pnlReturn.Controls.Add(this.dtp_ReturnStartPickupTime);
            this.pnlReturn.Controls.Add(this.dtp_ReturnStartPickupDate);
            this.pnlReturn.Controls.Add(this.radLabel8);
            this.pnlReturn.Location = new System.Drawing.Point(102, 57);
            this.pnlReturn.Name = "pnlReturn";
            this.pnlReturn.Size = new System.Drawing.Size(148, 120);
            this.pnlReturn.TabIndex = 225;
            // 
            // radLabel10
            // 
            this.radLabel10.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel10.Location = new System.Drawing.Point(6, 19);
            this.radLabel10.Name = "radLabel10";
            this.radLabel10.Size = new System.Drawing.Size(138, 19);
            this.radLabel10.TabIndex = 234;
            this.radLabel10.Text = "Return Pickup Date";
            // 
            // dtp_ReturnStartPickupTime
            // 
            this.dtp_ReturnStartPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtp_ReturnStartPickupTime.CustomFormat = "HH:mm";
            this.dtp_ReturnStartPickupTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_ReturnStartPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_ReturnStartPickupTime.Location = new System.Drawing.Point(7, 95);
            this.dtp_ReturnStartPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_ReturnStartPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_ReturnStartPickupTime.Name = "dtp_ReturnStartPickupTime";
            this.dtp_ReturnStartPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_ReturnStartPickupTime.ShowUpDown = true;
            this.dtp_ReturnStartPickupTime.Size = new System.Drawing.Size(76, 21);
            this.dtp_ReturnStartPickupTime.TabIndex = 235;
            this.dtp_ReturnStartPickupTime.TabStop = false;
            this.dtp_ReturnStartPickupTime.Text = "myDatePicker1";
            this.dtp_ReturnStartPickupTime.Value = null;
            // 
            // dtp_ReturnStartPickupDate
            // 
            this.dtp_ReturnStartPickupDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtp_ReturnStartPickupDate.CustomFormat = "dd/MM/yyyy";
            this.dtp_ReturnStartPickupDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_ReturnStartPickupDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_ReturnStartPickupDate.Location = new System.Drawing.Point(6, 44);
            this.dtp_ReturnStartPickupDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_ReturnStartPickupDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_ReturnStartPickupDate.Name = "dtp_ReturnStartPickupDate";
            this.dtp_ReturnStartPickupDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_ReturnStartPickupDate.Size = new System.Drawing.Size(89, 21);
            this.dtp_ReturnStartPickupDate.TabIndex = 233;
            this.dtp_ReturnStartPickupDate.TabStop = false;
            this.dtp_ReturnStartPickupDate.Text = "myDatePicker4";
            this.dtp_ReturnStartPickupDate.Value = null;
            // 
            // radLabel8
            // 
            this.radLabel8.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel8.Location = new System.Drawing.Point(6, 71);
            this.radLabel8.Name = "radLabel8";
            this.radLabel8.Size = new System.Drawing.Size(37, 18);
            this.radLabel8.TabIndex = 236;
            this.radLabel8.Text = "Time";
            // 
            // chkIsReturn
            // 
            this.chkIsReturn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsReturn.Location = new System.Drawing.Point(114, 20);
            this.chkIsReturn.Name = "chkIsReturn";
            // 
            // 
            // 
            this.chkIsReturn.RootElement.StretchHorizontally = true;
            this.chkIsReturn.RootElement.StretchVertically = true;
            this.chkIsReturn.Size = new System.Drawing.Size(116, 18);
            this.chkIsReturn.TabIndex = 237;
            this.chkIsReturn.Text = "Return Journey";
            this.chkIsReturn.ToggleStateChanging += new Telerik.WinControls.UI.StateChangingEventHandler(this.chkIsReturn_ToggleStateChanging);
            // 
            // grdPickupDates
            // 
            this.grdPickupDates.Location = new System.Drawing.Point(308, 14);
            this.grdPickupDates.Name = "grdPickupDates";
            this.grdPickupDates.Size = new System.Drawing.Size(425, 217);
            this.grdPickupDates.TabIndex = 232;
            this.grdPickupDates.Text = "radGridView1";
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Image = global::Taxi_AppMain.Properties.Resources.delete;
            this.btnDeleteRow.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeleteRow.Location = new System.Drawing.Point(254, 113);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(47, 40);
            this.btnDeleteRow.TabIndex = 231;
            this.btnDeleteRow.Text = "Delete";
            this.btnDeleteRow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteRow.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.delete;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteRow.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteRow.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteRow.GetChildAt(0))).Text = "Delete";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteRow.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteRow.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlPickupDay
            // 
            this.ddlPickupDay.Caption = null;
            this.ddlPickupDay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPickupDay.Location = new System.Drawing.Point(8, 43);
            this.ddlPickupDay.Name = "ddlPickupDay";
            this.ddlPickupDay.Property = null;
            this.ddlPickupDay.ShowDownArrow = true;
            this.ddlPickupDay.Size = new System.Drawing.Size(88, 23);
            this.ddlPickupDay.TabIndex = 229;
            // 
            // btnAddPickupDate
            // 
            this.btnAddPickupDate.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAddPickupDate.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddPickupDate.Location = new System.Drawing.Point(254, 55);
            this.btnAddPickupDate.Name = "btnAddPickupDate";
            this.btnAddPickupDate.Size = new System.Drawing.Size(47, 40);
            this.btnAddPickupDate.TabIndex = 230;
            this.btnAddPickupDate.Text = "Add";
            this.btnAddPickupDate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddPickupDate.Click += new System.EventHandler(this.btnAddPickupDate_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddPickupDate.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.add;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddPickupDate.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddPickupDate.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddPickupDate.GetChildAt(0))).Text = "Add";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddPickupDate.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddPickupDate.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel6
            // 
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel6.Location = new System.Drawing.Point(7, 18);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(78, 19);
            this.radLabel6.TabIndex = 228;
            this.radLabel6.Text = "Select Day";
            // 
            // dtp_StartPickupTime
            // 
            this.dtp_StartPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtp_StartPickupTime.CustomFormat = "HH:mm";
            this.dtp_StartPickupTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_StartPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_StartPickupTime.Location = new System.Drawing.Point(8, 152);
            this.dtp_StartPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_StartPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_StartPickupTime.Name = "dtp_StartPickupTime";
            this.dtp_StartPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_StartPickupTime.ShowUpDown = true;
            this.dtp_StartPickupTime.Size = new System.Drawing.Size(76, 21);
            this.dtp_StartPickupTime.TabIndex = 226;
            this.dtp_StartPickupTime.TabStop = false;
            this.dtp_StartPickupTime.Text = "myDatePicker1";
            this.dtp_StartPickupTime.Value = null;
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(7, 128);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(37, 18);
            this.radLabel4.TabIndex = 227;
            this.radLabel4.Text = "Time";
            // 
            // dtp_StartPickupDate
            // 
            this.dtp_StartPickupDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtp_StartPickupDate.CustomFormat = "dd/MM/yyyy";
            this.dtp_StartPickupDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_StartPickupDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_StartPickupDate.Location = new System.Drawing.Point(7, 101);
            this.dtp_StartPickupDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_StartPickupDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_StartPickupDate.Name = "dtp_StartPickupDate";
            this.dtp_StartPickupDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_StartPickupDate.Size = new System.Drawing.Size(89, 21);
            this.dtp_StartPickupDate.TabIndex = 224;
            this.dtp_StartPickupDate.TabStop = false;
            this.dtp_StartPickupDate.Text = "myDatePicker1";
            this.dtp_StartPickupDate.Value = null;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(7, 76);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(87, 19);
            this.radLabel2.TabIndex = 225;
            this.radLabel2.Text = "Pickup Date";
            // 
            // chkDestination
            // 
            this.chkDestination.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDestination.Location = new System.Drawing.Point(550, 6);
            this.chkDestination.Name = "chkDestination";
            // 
            // 
            // 
            this.chkDestination.RootElement.StretchHorizontally = true;
            this.chkDestination.RootElement.StretchVertically = true;
            this.chkDestination.Size = new System.Drawing.Size(114, 18);
            this.chkDestination.TabIndex = 117;
            this.chkDestination.Text = "Same destination";
            this.chkDestination.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.chkDestination.Visible = false;
            // 
            // chkOrigin
            // 
            this.chkOrigin.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOrigin.Location = new System.Drawing.Point(410, 6);
            this.chkOrigin.Name = "chkOrigin";
            // 
            // 
            // 
            this.chkOrigin.RootElement.StretchHorizontally = true;
            this.chkOrigin.RootElement.StretchVertically = true;
            this.chkOrigin.Size = new System.Drawing.Size(121, 18);
            this.chkOrigin.TabIndex = 116;
            this.chkOrigin.Text = "Same Pickup Point";
            this.chkOrigin.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.chkOrigin.Visible = false;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(9, 5);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(108, 23);
            this.radLabel1.TabIndex = 112;
            this.radLabel1.Text = "Enter Days :";
            // 
            // numDays
            // 
            this.numDays.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numDays.Location = new System.Drawing.Point(124, 3);
            this.numDays.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDays.Name = "numDays";
            // 
            // 
            // 
            this.numDays.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numDays.RootElement.StretchVertically = true;
            this.numDays.ShowBorder = true;
            this.numDays.ShowUpDownButtons = false;
            this.numDays.Size = new System.Drawing.Size(61, 25);
            this.numDays.TabIndex = 113;
            this.numDays.TabStop = false;
            this.numDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDays.Validated += new System.EventHandler(this.numDays_Validated);
            ((Telerik.WinControls.UI.RadSpinElement)(this.numDays.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numDays.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "1";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numDays.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkSame
            // 
            this.chkSame.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSame.Location = new System.Drawing.Point(229, 6);
            this.chkSame.Name = "chkSame";
            // 
            // 
            // 
            this.chkSame.RootElement.StretchHorizontally = true;
            this.chkSame.RootElement.StretchVertically = true;
            this.chkSame.Size = new System.Drawing.Size(167, 18);
            this.chkSame.TabIndex = 114;
            this.chkSame.Text = "Same As Day 1 Booking";
            this.chkSame.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.chkSame.ToggleStateChanging += new Telerik.WinControls.UI.StateChangingEventHandler(this.radCheckBox1_ToggleStateChanging);
            // 
            // btnCreateBooking
            // 
            this.btnCreateBooking.Image = global::Taxi_AppMain.Properties.Resources.Book;
            this.btnCreateBooking.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnCreateBooking.Location = new System.Drawing.Point(779, 107);
            this.btnCreateBooking.Name = "btnCreateBooking";
            this.btnCreateBooking.Size = new System.Drawing.Size(82, 109);
            this.btnCreateBooking.TabIndex = 115;
            this.btnCreateBooking.Text = "Create Bookings";
            this.btnCreateBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCreateBooking.TextWrap = true;
            this.btnCreateBooking.Click += new System.EventHandler(this.btnCreateBooking_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCreateBooking.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Book;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCreateBooking.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCreateBooking.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCreateBooking.GetChildAt(0))).Text = "Create Bookings";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCreateBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCreateBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCreateBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel7
            // 
            this.radLabel7.BackColor = System.Drawing.Color.Transparent;
            this.radLabel7.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold);
            this.radLabel7.ForeColor = System.Drawing.Color.Black;
            this.radLabel7.Location = new System.Drawing.Point(547, 10);
            this.radLabel7.Name = "radLabel7";
            // 
            // 
            // 
            this.radLabel7.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel7.Size = new System.Drawing.Size(112, 20);
            this.radLabel7.TabIndex = 211;
            this.radLabel7.Text = "Payment Mode";
            // 
            // ddlPaymentType
            // 
            this.ddlPaymentType.Caption = null;
            this.ddlPaymentType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPaymentType.ForeColor = System.Drawing.Color.Black;
            this.ddlPaymentType.Location = new System.Drawing.Point(659, 8);
            this.ddlPaymentType.Name = "ddlPaymentType";
            this.ddlPaymentType.Property = null;
            // 
            // 
            // 
            this.ddlPaymentType.RootElement.ForeColor = System.Drawing.Color.Black;
            this.ddlPaymentType.ShowDownArrow = true;
            this.ddlPaymentType.Size = new System.Drawing.Size(115, 26);
            this.ddlPaymentType.TabIndex = 210;
            // 
            // lblDepartment
            // 
            this.lblDepartment.BackColor = System.Drawing.Color.Transparent;
            this.lblDepartment.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.ForeColor = System.Drawing.Color.Black;
            this.lblDepartment.Location = new System.Drawing.Point(298, 8);
            this.lblDepartment.Name = "lblDepartment";
            // 
            // 
            // 
            this.lblDepartment.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblDepartment.Size = new System.Drawing.Size(88, 22);
            this.lblDepartment.TabIndex = 209;
            this.lblDepartment.Text = "Department";
            // 
            // ddlDepartment
            // 
            this.ddlDepartment.Caption = null;
            this.ddlDepartment.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDepartment.Location = new System.Drawing.Point(390, 8);
            this.ddlDepartment.Name = "ddlDepartment";
            this.ddlDepartment.Property = null;
            this.ddlDepartment.ShowDownArrow = true;
            this.ddlDepartment.Size = new System.Drawing.Size(150, 26);
            this.ddlDepartment.TabIndex = 208;
            // 
            // chkIsCompanyRates
            // 
            this.chkIsCompanyRates.BackColor = System.Drawing.Color.Transparent;
            this.chkIsCompanyRates.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsCompanyRates.ForeColor = System.Drawing.Color.Black;
            this.chkIsCompanyRates.Location = new System.Drawing.Point(49, 10);
            this.chkIsCompanyRates.Name = "chkIsCompanyRates";
            // 
            // 
            // 
            this.chkIsCompanyRates.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkIsCompanyRates.Size = new System.Drawing.Size(76, 22);
            this.chkIsCompanyRates.TabIndex = 159;
            this.chkIsCompanyRates.Text = "Account";
            this.chkIsCompanyRates.TextWrap = true;
            this.chkIsCompanyRates.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkIsCompanyRates_ToggleStateChanged);
            // 
            // ddlCompany
            // 
            this.ddlCompany.Caption = null;
            this.ddlCompany.Enabled = false;
            this.ddlCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompany.Location = new System.Drawing.Point(135, 7);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Property = null;
            this.ddlCompany.ShowDownArrow = true;
            this.ddlCompany.Size = new System.Drawing.Size(153, 26);
            this.ddlCompany.TabIndex = 160;
            this.ddlCompany.SelectedValueChanged += new System.EventHandler(this.ddlCompany_SelectedValueChanged);
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.grdBookings);
            this.radPanel1.Controls.Add(this.radPageView1);
            this.radPanel1.Controls.Add(this.panel1);
            this.radPanel1.Location = new System.Drawing.Point(0, 32);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(914, 704);
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
            this.grdBookings.Location = new System.Drawing.Point(0, 435);
            this.grdBookings.Name = "grdBookings";
            this.grdBookings.PKFieldColumnName = "";
            this.grdBookings.ShowImageOnActionButton = true;
            this.grdBookings.Size = new System.Drawing.Size(914, 269);
            this.grdBookings.TabIndex = 218;
            this.grdBookings.Text = "myGridView1";
            // 
            // radPageView1
            // 
            this.radPageView1.Controls.Add(this.pg_Daily);
            this.radPageView1.Controls.Add(this.pg_weekly);
            this.radPageView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPageView1.Location = new System.Drawing.Point(0, 72);
            this.radPageView1.Name = "radPageView1";
            this.radPageView1.SelectedPage = this.pg_Daily;
            this.radPageView1.Size = new System.Drawing.Size(914, 363);
            this.radPageView1.TabIndex = 219;
            this.radPageView1.Text = "radPageView1";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            // 
            // pg_Daily
            // 
            this.pg_Daily.Controls.Add(this.radPanel2);
            this.pg_Daily.Location = new System.Drawing.Point(10, 37);
            this.pg_Daily.Name = "pg_Daily";
            this.pg_Daily.Size = new System.Drawing.Size(893, 315);
            this.pg_Daily.Text = "Daily";
            // 
            // pg_weekly
            // 
            this.pg_weekly.Controls.Add(this.dtpEndingDate);
            this.pg_weekly.Controls.Add(this.radLabel12);
            this.pg_weekly.Controls.Add(this.radPanel5);
            this.pg_weekly.Controls.Add(this.btnWeeklyCreateBooking);
            this.pg_weekly.Controls.Add(this.chkSun);
            this.pg_weekly.Controls.Add(this.chkSat);
            this.pg_weekly.Controls.Add(this.chkFri);
            this.pg_weekly.Controls.Add(this.chkThurs);
            this.pg_weekly.Controls.Add(this.chkWed);
            this.pg_weekly.Controls.Add(this.chkAutoRecurred);
            this.pg_weekly.Controls.Add(this.dtpStartingAt);
            this.pg_weekly.Controls.Add(this.radLabel11);
            this.pg_weekly.Controls.Add(this.radLabel9);
            this.pg_weekly.Controls.Add(this.numWeeks);
            this.pg_weekly.Controls.Add(this.chkTue);
            this.pg_weekly.Controls.Add(this.chkMon);
            this.pg_weekly.Location = new System.Drawing.Point(10, 37);
            this.pg_weekly.Name = "pg_weekly";
            this.pg_weekly.Size = new System.Drawing.Size(893, 315);
            this.pg_weekly.Text = "Weekly";
            // 
            // dtpEndingDate
            // 
            this.dtpEndingDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpEndingDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEndingDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEndingDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpEndingDate.Location = new System.Drawing.Point(782, 72);
            this.dtpEndingDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEndingDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEndingDate.Name = "dtpEndingDate";
            this.dtpEndingDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEndingDate.Size = new System.Drawing.Size(102, 24);
            this.dtpEndingDate.TabIndex = 241;
            this.dtpEndingDate.TabStop = false;
            this.dtpEndingDate.Text = "myDatePicker1";
            this.dtpEndingDate.Value = null;
            // 
            // radLabel12
            // 
            this.radLabel12.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.radLabel12.Location = new System.Drawing.Point(659, 72);
            this.radLabel12.Name = "radLabel12";
            this.radLabel12.Size = new System.Drawing.Size(119, 23);
            this.radLabel12.TabIndex = 242;
            this.radLabel12.Text = "Ending Date :";
            // 
            // radPanel5
            // 
            this.radPanel5.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel5.Controls.Add(this.lblweekcompanyreturnfares);
            this.radPanel5.Controls.Add(this.numWeekCompanyFareRateReturn);
            this.radPanel5.Controls.Add(this.radLabel18);
            this.radPanel5.Controls.Add(this.numCompanyFareRateWeek);
            this.radPanel5.Controls.Add(this.lblweekcustreturnfares);
            this.radPanel5.Controls.Add(this.numWeekCustFareRateReturn);
            this.radPanel5.Controls.Add(this.radLabel16);
            this.radPanel5.Controls.Add(this.numCustFareRateWeek);
            this.radPanel5.Controls.Add(this.dtpWeekReturnPickupTime);
            this.radPanel5.Controls.Add(this.chkReturnWeekJourney);
            this.radPanel5.Controls.Add(this.dtpWeekPickupTime);
            this.radPanel5.Controls.Add(this.radLabel14);
            this.radPanel5.Controls.Add(this.lblReturnWeekPickupTime);
            this.radPanel5.Controls.Add(this.lblWeekReturnFare);
            this.radPanel5.Controls.Add(this.numWeekFareRateReturn);
            this.radPanel5.Controls.Add(this.radLabel13);
            this.radPanel5.Controls.Add(this.numFareRateWeek);
            this.radPanel5.Controls.Add(this.btnCalculateWeekFare);
            this.radPanel5.Location = new System.Drawing.Point(16, 116);
            this.radPanel5.Name = "radPanel5";
            this.radPanel5.Size = new System.Drawing.Size(685, 196);
            this.radPanel5.TabIndex = 240;
            // 
            // lblweekcompanyreturnfares
            // 
            this.lblweekcompanyreturnfares.BackColor = System.Drawing.Color.Transparent;
            this.lblweekcompanyreturnfares.Enabled = false;
            this.lblweekcompanyreturnfares.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblweekcompanyreturnfares.ForeColor = System.Drawing.Color.Black;
            this.lblweekcompanyreturnfares.Location = new System.Drawing.Point(208, 160);
            this.lblweekcompanyreturnfares.Name = "lblweekcompanyreturnfares";
            // 
            // 
            // 
            this.lblweekcompanyreturnfares.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblweekcompanyreturnfares.Size = new System.Drawing.Size(157, 23);
            this.lblweekcompanyreturnfares.TabIndex = 257;
            this.lblweekcompanyreturnfares.Text = "Return A/C Fares   £";
            // 
            // numWeekCompanyFareRateReturn
            // 
            this.numWeekCompanyFareRateReturn.DecimalPlaces = 2;
            this.numWeekCompanyFareRateReturn.Enabled = false;
            this.numWeekCompanyFareRateReturn.EnableKeyMap = true;
            this.numWeekCompanyFareRateReturn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWeekCompanyFareRateReturn.InterceptArrowKeys = false;
            this.numWeekCompanyFareRateReturn.Location = new System.Drawing.Point(376, 158);
            this.numWeekCompanyFareRateReturn.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numWeekCompanyFareRateReturn.Name = "numWeekCompanyFareRateReturn";
            // 
            // 
            // 
            this.numWeekCompanyFareRateReturn.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numWeekCompanyFareRateReturn.ShowBorder = true;
            this.numWeekCompanyFareRateReturn.ShowUpDownButtons = false;
            this.numWeekCompanyFareRateReturn.Size = new System.Drawing.Size(69, 24);
            this.numWeekCompanyFareRateReturn.TabIndex = 256;
            this.numWeekCompanyFareRateReturn.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numWeekCompanyFareRateReturn.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numWeekCompanyFareRateReturn.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWeekCompanyFareRateReturn.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWeekCompanyFareRateReturn.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel18
            // 
            this.radLabel18.BackColor = System.Drawing.Color.Transparent;
            this.radLabel18.Font = new System.Drawing.Font("Tahoma", 12F);
            this.radLabel18.ForeColor = System.Drawing.Color.Black;
            this.radLabel18.Location = new System.Drawing.Point(8, 160);
            this.radLabel18.Name = "radLabel18";
            // 
            // 
            // 
            this.radLabel18.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel18.Size = new System.Drawing.Size(99, 23);
            this.radLabel18.TabIndex = 255;
            this.radLabel18.Text = "A/C Price   £";
            // 
            // numCompanyFareRateWeek
            // 
            this.numCompanyFareRateWeek.DecimalPlaces = 2;
            this.numCompanyFareRateWeek.EnableKeyMap = true;
            this.numCompanyFareRateWeek.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCompanyFareRateWeek.InterceptArrowKeys = false;
            this.numCompanyFareRateWeek.Location = new System.Drawing.Point(131, 158);
            this.numCompanyFareRateWeek.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCompanyFareRateWeek.Name = "numCompanyFareRateWeek";
            // 
            // 
            // 
            this.numCompanyFareRateWeek.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCompanyFareRateWeek.ShowBorder = true;
            this.numCompanyFareRateWeek.ShowUpDownButtons = false;
            this.numCompanyFareRateWeek.Size = new System.Drawing.Size(69, 24);
            this.numCompanyFareRateWeek.TabIndex = 254;
            this.numCompanyFareRateWeek.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCompanyFareRateWeek.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCompanyFareRateWeek.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCompanyFareRateWeek.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCompanyFareRateWeek.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblweekcustreturnfares
            // 
            this.lblweekcustreturnfares.BackColor = System.Drawing.Color.Transparent;
            this.lblweekcustreturnfares.Enabled = false;
            this.lblweekcustreturnfares.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblweekcustreturnfares.ForeColor = System.Drawing.Color.Black;
            this.lblweekcustreturnfares.Location = new System.Drawing.Point(208, 126);
            this.lblweekcustreturnfares.Name = "lblweekcustreturnfares";
            // 
            // 
            // 
            this.lblweekcustreturnfares.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblweekcustreturnfares.Size = new System.Drawing.Size(167, 23);
            this.lblweekcustreturnfares.TabIndex = 253;
            this.lblweekcustreturnfares.Text = "Return Cust. Fares   £";
            // 
            // numWeekCustFareRateReturn
            // 
            this.numWeekCustFareRateReturn.DecimalPlaces = 2;
            this.numWeekCustFareRateReturn.Enabled = false;
            this.numWeekCustFareRateReturn.EnableKeyMap = true;
            this.numWeekCustFareRateReturn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWeekCustFareRateReturn.InterceptArrowKeys = false;
            this.numWeekCustFareRateReturn.Location = new System.Drawing.Point(376, 124);
            this.numWeekCustFareRateReturn.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numWeekCustFareRateReturn.Name = "numWeekCustFareRateReturn";
            // 
            // 
            // 
            this.numWeekCustFareRateReturn.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numWeekCustFareRateReturn.ShowBorder = true;
            this.numWeekCustFareRateReturn.ShowUpDownButtons = false;
            this.numWeekCustFareRateReturn.Size = new System.Drawing.Size(69, 24);
            this.numWeekCustFareRateReturn.TabIndex = 252;
            this.numWeekCustFareRateReturn.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numWeekCustFareRateReturn.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numWeekCustFareRateReturn.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWeekCustFareRateReturn.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWeekCustFareRateReturn.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel16
            // 
            this.radLabel16.BackColor = System.Drawing.Color.Transparent;
            this.radLabel16.Font = new System.Drawing.Font("Tahoma", 12F);
            this.radLabel16.ForeColor = System.Drawing.Color.Black;
            this.radLabel16.Location = new System.Drawing.Point(8, 126);
            this.radLabel16.Name = "radLabel16";
            // 
            // 
            // 
            this.radLabel16.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel16.Size = new System.Drawing.Size(114, 23);
            this.radLabel16.TabIndex = 251;
            this.radLabel16.Text = "Cust. Fares   £";
            // 
            // numCustFareRateWeek
            // 
            this.numCustFareRateWeek.DecimalPlaces = 2;
            this.numCustFareRateWeek.EnableKeyMap = true;
            this.numCustFareRateWeek.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCustFareRateWeek.InterceptArrowKeys = false;
            this.numCustFareRateWeek.Location = new System.Drawing.Point(131, 124);
            this.numCustFareRateWeek.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numCustFareRateWeek.Name = "numCustFareRateWeek";
            // 
            // 
            // 
            this.numCustFareRateWeek.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCustFareRateWeek.ShowBorder = true;
            this.numCustFareRateWeek.ShowUpDownButtons = false;
            this.numCustFareRateWeek.Size = new System.Drawing.Size(69, 24);
            this.numCustFareRateWeek.TabIndex = 250;
            this.numCustFareRateWeek.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCustFareRateWeek.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numCustFareRateWeek.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCustFareRateWeek.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numCustFareRateWeek.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpWeekReturnPickupTime
            // 
            this.dtpWeekReturnPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpWeekReturnPickupTime.CustomFormat = "HH:mm";
            this.dtpWeekReturnPickupTime.Enabled = false;
            this.dtpWeekReturnPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWeekReturnPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpWeekReturnPickupTime.Location = new System.Drawing.Point(370, 49);
            this.dtpWeekReturnPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpWeekReturnPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpWeekReturnPickupTime.Name = "dtpWeekReturnPickupTime";
            this.dtpWeekReturnPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpWeekReturnPickupTime.ShowUpDown = true;
            this.dtpWeekReturnPickupTime.Size = new System.Drawing.Size(76, 24);
            this.dtpWeekReturnPickupTime.TabIndex = 249;
            this.dtpWeekReturnPickupTime.TabStop = false;
            this.dtpWeekReturnPickupTime.Text = "myDatePicker2";
            this.dtpWeekReturnPickupTime.Value = null;
            // 
            // chkReturnWeekJourney
            // 
            this.chkReturnWeekJourney.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReturnWeekJourney.Location = new System.Drawing.Point(11, 6);
            this.chkReturnWeekJourney.Name = "chkReturnWeekJourney";
            // 
            // 
            // 
            this.chkReturnWeekJourney.RootElement.StretchHorizontally = true;
            this.chkReturnWeekJourney.RootElement.StretchVertically = true;
            this.chkReturnWeekJourney.Size = new System.Drawing.Size(165, 18);
            this.chkReturnWeekJourney.TabIndex = 239;
            this.chkReturnWeekJourney.Text = "Return Journey";
            this.chkReturnWeekJourney.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkReturnWeekJourney_ToggleStateChanged);
            // 
            // dtpWeekPickupTime
            // 
            this.dtpWeekPickupTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpWeekPickupTime.CustomFormat = "HH:mm";
            this.dtpWeekPickupTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWeekPickupTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpWeekPickupTime.Location = new System.Drawing.Point(124, 49);
            this.dtpWeekPickupTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpWeekPickupTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpWeekPickupTime.Name = "dtpWeekPickupTime";
            this.dtpWeekPickupTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpWeekPickupTime.ShowUpDown = true;
            this.dtpWeekPickupTime.Size = new System.Drawing.Size(76, 24);
            this.dtpWeekPickupTime.TabIndex = 247;
            this.dtpWeekPickupTime.TabStop = false;
            this.dtpWeekPickupTime.Text = "myDatePicker1";
            this.dtpWeekPickupTime.Value = null;
            // 
            // radLabel14
            // 
            this.radLabel14.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel14.Location = new System.Drawing.Point(8, 50);
            this.radLabel14.Name = "radLabel14";
            this.radLabel14.Size = new System.Drawing.Size(107, 23);
            this.radLabel14.TabIndex = 246;
            this.radLabel14.Text = "Pickup Time :";
            // 
            // lblReturnWeekPickupTime
            // 
            this.lblReturnWeekPickupTime.Enabled = false;
            this.lblReturnWeekPickupTime.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnWeekPickupTime.Location = new System.Drawing.Point(209, 50);
            this.lblReturnWeekPickupTime.Name = "lblReturnWeekPickupTime";
            this.lblReturnWeekPickupTime.Size = new System.Drawing.Size(161, 23);
            this.lblReturnWeekPickupTime.TabIndex = 248;
            this.lblReturnWeekPickupTime.Text = "Return Pickup Time :";
            // 
            // lblWeekReturnFare
            // 
            this.lblWeekReturnFare.BackColor = System.Drawing.Color.Transparent;
            this.lblWeekReturnFare.Enabled = false;
            this.lblWeekReturnFare.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblWeekReturnFare.ForeColor = System.Drawing.Color.Black;
            this.lblWeekReturnFare.Location = new System.Drawing.Point(208, 93);
            this.lblWeekReturnFare.Name = "lblWeekReturnFare";
            // 
            // 
            // 
            this.lblWeekReturnFare.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblWeekReturnFare.Size = new System.Drawing.Size(125, 23);
            this.lblWeekReturnFare.TabIndex = 130;
            this.lblWeekReturnFare.Text = "Return Fares   £";
            // 
            // numWeekFareRateReturn
            // 
            this.numWeekFareRateReturn.DecimalPlaces = 2;
            this.numWeekFareRateReturn.Enabled = false;
            this.numWeekFareRateReturn.EnableKeyMap = true;
            this.numWeekFareRateReturn.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWeekFareRateReturn.InterceptArrowKeys = false;
            this.numWeekFareRateReturn.Location = new System.Drawing.Point(376, 91);
            this.numWeekFareRateReturn.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numWeekFareRateReturn.Name = "numWeekFareRateReturn";
            // 
            // 
            // 
            this.numWeekFareRateReturn.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numWeekFareRateReturn.ShowBorder = true;
            this.numWeekFareRateReturn.ShowUpDownButtons = false;
            this.numWeekFareRateReturn.Size = new System.Drawing.Size(69, 24);
            this.numWeekFareRateReturn.TabIndex = 129;
            this.numWeekFareRateReturn.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numWeekFareRateReturn.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numWeekFareRateReturn.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWeekFareRateReturn.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWeekFareRateReturn.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel13
            // 
            this.radLabel13.BackColor = System.Drawing.Color.Transparent;
            this.radLabel13.Font = new System.Drawing.Font("Tahoma", 12F);
            this.radLabel13.ForeColor = System.Drawing.Color.Black;
            this.radLabel13.Location = new System.Drawing.Point(8, 93);
            this.radLabel13.Name = "radLabel13";
            // 
            // 
            // 
            this.radLabel13.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel13.Size = new System.Drawing.Size(71, 23);
            this.radLabel13.TabIndex = 128;
            this.radLabel13.Text = "Fares   £";
            // 
            // numFareRateWeek
            // 
            this.numFareRateWeek.DecimalPlaces = 2;
            this.numFareRateWeek.EnableKeyMap = true;
            this.numFareRateWeek.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFareRateWeek.InterceptArrowKeys = false;
            this.numFareRateWeek.Location = new System.Drawing.Point(131, 91);
            this.numFareRateWeek.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numFareRateWeek.Name = "numFareRateWeek";
            // 
            // 
            // 
            this.numFareRateWeek.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numFareRateWeek.ShowBorder = true;
            this.numFareRateWeek.ShowUpDownButtons = false;
            this.numFareRateWeek.Size = new System.Drawing.Size(69, 24);
            this.numFareRateWeek.TabIndex = 24;
            this.numFareRateWeek.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numFareRateWeek.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numFareRateWeek.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numFareRateWeek.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.00";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numFareRateWeek.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnCalculateWeekFare
            // 
            this.btnCalculateWeekFare.Image = global::Taxi_AppMain.Properties.Resources.fares;
            this.btnCalculateWeekFare.Location = new System.Drawing.Point(486, 65);
            this.btnCalculateWeekFare.Name = "btnCalculateWeekFare";
            this.btnCalculateWeekFare.Size = new System.Drawing.Size(178, 51);
            this.btnCalculateWeekFare.TabIndex = 141;
            this.btnCalculateWeekFare.TabStop = false;
            this.btnCalculateWeekFare.Text = "Calculate";
            this.btnCalculateWeekFare.Click += new System.EventHandler(this.btnCalculateWeekFare_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCalculateWeekFare.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.fares;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCalculateWeekFare.GetChildAt(0))).Text = "Calculate";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCalculateWeekFare.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCalculateWeekFare.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnWeeklyCreateBooking
            // 
            this.btnWeeklyCreateBooking.Image = global::Taxi_AppMain.Properties.Resources.Book;
            this.btnWeeklyCreateBooking.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnWeeklyCreateBooking.Location = new System.Drawing.Point(731, 151);
            this.btnWeeklyCreateBooking.Name = "btnWeeklyCreateBooking";
            this.btnWeeklyCreateBooking.Size = new System.Drawing.Size(147, 109);
            this.btnWeeklyCreateBooking.TabIndex = 234;
            this.btnWeeklyCreateBooking.Text = "Create Bookings";
            this.btnWeeklyCreateBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnWeeklyCreateBooking.TextWrap = true;
            this.btnWeeklyCreateBooking.Click += new System.EventHandler(this.btnWeeklyCreateBooking_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnWeeklyCreateBooking.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Book;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnWeeklyCreateBooking.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnWeeklyCreateBooking.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnWeeklyCreateBooking.GetChildAt(0))).Text = "Create Bookings";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnWeeklyCreateBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnWeeklyCreateBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnWeeklyCreateBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkSun
            // 
            this.chkSun.AutoSize = false;
            this.chkSun.BackColor = System.Drawing.Color.RoyalBlue;
            this.chkSun.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
            this.chkSun.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSun.ForeColor = System.Drawing.Color.White;
            this.chkSun.Location = new System.Drawing.Point(707, 16);
            this.chkSun.Name = "chkSun";
            // 
            // 
            // 
            this.chkSun.RootElement.ForeColor = System.Drawing.Color.White;
            this.chkSun.Size = new System.Drawing.Size(107, 32);
            this.chkSun.TabIndex = 233;
            this.chkSun.Text = "SUN";
            this.chkSun.TextWrap = true;
            this.chkSun.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkMon_ToggleStateChanged);
            // 
            // chkSat
            // 
            this.chkSat.AutoSize = false;
            this.chkSat.BackColor = System.Drawing.Color.RoyalBlue;
            this.chkSat.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSat.ForeColor = System.Drawing.Color.White;
            this.chkSat.Location = new System.Drawing.Point(594, 16);
            this.chkSat.Name = "chkSat";
            // 
            // 
            // 
            this.chkSat.RootElement.ForeColor = System.Drawing.Color.White;
            this.chkSat.Size = new System.Drawing.Size(107, 32);
            this.chkSat.TabIndex = 232;
            this.chkSat.Text = "SAT";
            this.chkSat.TextWrap = true;
            this.chkSat.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkMon_ToggleStateChanged);
            // 
            // chkFri
            // 
            this.chkFri.AutoSize = false;
            this.chkFri.BackColor = System.Drawing.Color.RoyalBlue;
            this.chkFri.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
            this.chkFri.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFri.ForeColor = System.Drawing.Color.White;
            this.chkFri.Location = new System.Drawing.Point(481, 16);
            this.chkFri.Name = "chkFri";
            // 
            // 
            // 
            this.chkFri.RootElement.ForeColor = System.Drawing.Color.White;
            this.chkFri.Size = new System.Drawing.Size(107, 32);
            this.chkFri.TabIndex = 231;
            this.chkFri.Text = "FRI";
            this.chkFri.TextWrap = true;
            this.chkFri.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkMon_ToggleStateChanged);
            // 
            // chkThurs
            // 
            this.chkThurs.AutoSize = false;
            this.chkThurs.BackColor = System.Drawing.Color.RoyalBlue;
            this.chkThurs.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkThurs.ForeColor = System.Drawing.Color.White;
            this.chkThurs.Location = new System.Drawing.Point(368, 16);
            this.chkThurs.Name = "chkThurs";
            // 
            // 
            // 
            this.chkThurs.RootElement.ForeColor = System.Drawing.Color.White;
            this.chkThurs.Size = new System.Drawing.Size(107, 32);
            this.chkThurs.TabIndex = 230;
            this.chkThurs.Text = "THURS";
            this.chkThurs.TextWrap = true;
            this.chkThurs.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkMon_ToggleStateChanged);
            // 
            // chkWed
            // 
            this.chkWed.AutoSize = false;
            this.chkWed.BackColor = System.Drawing.Color.RoyalBlue;
            this.chkWed.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
            this.chkWed.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWed.ForeColor = System.Drawing.Color.White;
            this.chkWed.Location = new System.Drawing.Point(255, 16);
            this.chkWed.Name = "chkWed";
            // 
            // 
            // 
            this.chkWed.RootElement.ForeColor = System.Drawing.Color.White;
            this.chkWed.Size = new System.Drawing.Size(107, 32);
            this.chkWed.TabIndex = 229;
            this.chkWed.Text = "WED";
            this.chkWed.TextWrap = true;
            this.chkWed.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkMon_ToggleStateChanged);
            // 
            // chkAutoRecurred
            // 
            this.chkAutoRecurred.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoRecurred.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.chkAutoRecurred.ForeColor = System.Drawing.Color.Red;
            this.chkAutoRecurred.Location = new System.Drawing.Point(112, 70);
            this.chkAutoRecurred.Name = "chkAutoRecurred";
            // 
            // 
            // 
            this.chkAutoRecurred.RootElement.ForeColor = System.Drawing.Color.Red;
            this.chkAutoRecurred.Size = new System.Drawing.Size(147, 23);
            this.chkAutoRecurred.TabIndex = 228;
            this.chkAutoRecurred.Text = "Auto Recurring";
            this.chkAutoRecurred.TextWrap = true;
            this.chkAutoRecurred.Visible = false;
            // 
            // dtpStartingAt
            // 
            this.dtpStartingAt.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpStartingAt.CustomFormat = "dd/MM/yyyy";
            this.dtpStartingAt.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartingAt.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpStartingAt.Location = new System.Drawing.Point(547, 71);
            this.dtpStartingAt.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartingAt.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartingAt.Name = "dtpStartingAt";
            this.dtpStartingAt.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartingAt.Size = new System.Drawing.Size(102, 24);
            this.dtpStartingAt.TabIndex = 226;
            this.dtpStartingAt.TabStop = false;
            this.dtpStartingAt.Text = "myDatePicker1";
            this.dtpStartingAt.Value = null;
            // 
            // radLabel11
            // 
            this.radLabel11.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.radLabel11.Location = new System.Drawing.Point(432, 71);
            this.radLabel11.Name = "radLabel11";
            this.radLabel11.Size = new System.Drawing.Size(107, 23);
            this.radLabel11.TabIndex = 227;
            this.radLabel11.Text = "Starting at :";
            // 
            // radLabel9
            // 
            this.radLabel9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel9.Location = new System.Drawing.Point(279, 71);
            this.radLabel9.Name = "radLabel9";
            this.radLabel9.Size = new System.Drawing.Size(74, 23);
            this.radLabel9.TabIndex = 167;
            this.radLabel9.Text = "Weeks :";
            // 
            // numWeeks
            // 
            this.numWeeks.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numWeeks.Location = new System.Drawing.Point(359, 69);
            this.numWeeks.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numWeeks.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWeeks.Name = "numWeeks";
            // 
            // 
            // 
            this.numWeeks.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numWeeks.RootElement.StretchVertically = true;
            this.numWeeks.ShowBorder = true;
            this.numWeeks.ShowUpDownButtons = false;
            this.numWeeks.Size = new System.Drawing.Size(61, 25);
            this.numWeeks.TabIndex = 168;
            this.numWeeks.TabStop = false;
            this.numWeeks.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            ((Telerik.WinControls.UI.RadSpinElement)(this.numWeeks.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWeeks.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "1";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numWeeks.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkTue
            // 
            this.chkTue.AutoSize = false;
            this.chkTue.BackColor = System.Drawing.Color.RoyalBlue;
            this.chkTue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTue.ForeColor = System.Drawing.Color.White;
            this.chkTue.Location = new System.Drawing.Point(142, 16);
            this.chkTue.Name = "chkTue";
            // 
            // 
            // 
            this.chkTue.RootElement.ForeColor = System.Drawing.Color.White;
            this.chkTue.Size = new System.Drawing.Size(107, 32);
            this.chkTue.TabIndex = 161;
            this.chkTue.Text = "TUE";
            this.chkTue.TextWrap = true;
            this.chkTue.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkMon_ToggleStateChanged);
            // 
            // chkMon
            // 
            this.chkMon.AutoSize = false;
            this.chkMon.BackColor = System.Drawing.Color.RoyalBlue;
            this.chkMon.DisplayStyle = Telerik.WinControls.DisplayStyle.Text;
            this.chkMon.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMon.ForeColor = System.Drawing.Color.White;
            this.chkMon.Location = new System.Drawing.Point(29, 16);
            this.chkMon.Name = "chkMon";
            // 
            // 
            // 
            this.chkMon.RootElement.ForeColor = System.Drawing.Color.White;
            this.chkMon.Size = new System.Drawing.Size(107, 32);
            this.chkMon.TabIndex = 160;
            this.chkMon.Text = "MON";
            this.chkMon.TextWrap = true;
            this.chkMon.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkMon_ToggleStateChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkReturnConfirm);
            this.panel1.Controls.Add(this.ddlReturnAllocatedDriver);
            this.panel1.Controls.Add(this.chkConfirmDriver);
            this.panel1.Controls.Add(this.ddlDriver);
            this.panel1.Controls.Add(this.chkIsCompanyRates);
            this.panel1.Controls.Add(this.ddlDepartment);
            this.panel1.Controls.Add(this.radLabel7);
            this.panel1.Controls.Add(this.ddlCompany);
            this.panel1.Controls.Add(this.lblDepartment);
            this.panel1.Controls.Add(this.ddlPaymentType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 72);
            this.panel1.TabIndex = 220;
            // 
            // chkReturnConfirm
            // 
            this.chkReturnConfirm.BackColor = System.Drawing.Color.Transparent;
            this.chkReturnConfirm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReturnConfirm.ForeColor = System.Drawing.Color.Black;
            this.chkReturnConfirm.Location = new System.Drawing.Point(423, 44);
            this.chkReturnConfirm.Name = "chkReturnConfirm";
            // 
            // 
            // 
            this.chkReturnConfirm.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkReturnConfirm.Size = new System.Drawing.Size(171, 22);
            this.chkReturnConfirm.TabIndex = 216;
            this.chkReturnConfirm.Text = "Confirm Return Driver";
            this.chkReturnConfirm.TextWrap = true;
            // 
            // ddlReturnAllocatedDriver
            // 
            this.ddlReturnAllocatedDriver.Caption = null;
            this.ddlReturnAllocatedDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlReturnAllocatedDriver.Location = new System.Drawing.Point(596, 41);
            this.ddlReturnAllocatedDriver.Name = "ddlReturnAllocatedDriver";
            this.ddlReturnAllocatedDriver.Property = null;
            this.ddlReturnAllocatedDriver.ShowDownArrow = true;
            this.ddlReturnAllocatedDriver.Size = new System.Drawing.Size(247, 26);
            this.ddlReturnAllocatedDriver.TabIndex = 215;
            this.ddlReturnAllocatedDriver.Enter += new System.EventHandler(this.ddlReturnAllocatedDriver_Enter);
            // 
            // chkConfirmDriver
            // 
            this.chkConfirmDriver.BackColor = System.Drawing.Color.Transparent;
            this.chkConfirmDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkConfirmDriver.ForeColor = System.Drawing.Color.Black;
            this.chkConfirmDriver.Location = new System.Drawing.Point(26, 44);
            this.chkConfirmDriver.Name = "chkConfirmDriver";
            // 
            // 
            // 
            this.chkConfirmDriver.RootElement.ForeColor = System.Drawing.Color.Black;
            this.chkConfirmDriver.Size = new System.Drawing.Size(121, 22);
            this.chkConfirmDriver.TabIndex = 212;
            this.chkConfirmDriver.Text = "Confirm Driver";
            this.chkConfirmDriver.TextWrap = true;
            // 
            // ddlDriver
            // 
            this.ddlDriver.Caption = null;
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(155, 41);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Property = null;
            this.ddlDriver.ShowDownArrow = true;
            this.ddlDriver.Size = new System.Drawing.Size(247, 26);
            this.ddlDriver.TabIndex = 213;
            this.ddlDriver.Enter += new System.EventHandler(this.ddlDriver_Enter);
            // 
            // btnSaveBooking
            // 
            this.btnSaveBooking.Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            this.btnSaveBooking.Location = new System.Drawing.Point(658, 2);
            this.btnSaveBooking.Name = "btnSaveBooking";
            this.btnSaveBooking.Size = new System.Drawing.Size(183, 40);
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
            this.radPanel4.BackColor = System.Drawing.Color.SkyBlue;
            this.radPanel4.Controls.Add(this.lblUpdate);
            this.radPanel4.Controls.Add(this.btnSaveBooking);
            this.radPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel4.ForeColor = System.Drawing.Color.SteelBlue;
            this.radPanel4.Location = new System.Drawing.Point(0, 742);
            this.radPanel4.Name = "radPanel4";
            // 
            // 
            // 
            this.radPanel4.RootElement.ForeColor = System.Drawing.Color.SteelBlue;
            this.radPanel4.Size = new System.Drawing.Size(914, 42);
            this.radPanel4.TabIndex = 225;
            this.radPanel4.Paint += new System.Windows.Forms.PaintEventHandler(this.radPanel4_Paint);
            // 
            // optDaily
            // 
            this.optDaily.BackColor = System.Drawing.Color.RoyalBlue;
            this.optDaily.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDaily.ForeColor = System.Drawing.Color.White;
            this.optDaily.Location = new System.Drawing.Point(16, 8);
            this.optDaily.Name = "optDaily";
            // 
            // 
            // 
            this.optDaily.RootElement.ForeColor = System.Drawing.Color.White;
            this.optDaily.Size = new System.Drawing.Size(85, 18);
            this.optDaily.TabIndex = 226;
            this.optDaily.Text = "Daily";
            this.optDaily.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radRadioButton1_ToggleStateChanged);
            // 
            // optWeekly
            // 
            this.optWeekly.BackColor = System.Drawing.Color.RoyalBlue;
            this.optWeekly.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optWeekly.ForeColor = System.Drawing.Color.White;
            this.optWeekly.Location = new System.Drawing.Point(118, 8);
            this.optWeekly.Name = "optWeekly";
            // 
            // 
            // 
            this.optWeekly.RootElement.ForeColor = System.Drawing.Color.White;
            this.optWeekly.Size = new System.Drawing.Size(98, 18);
            this.optWeekly.TabIndex = 227;
            this.optWeekly.Text = "Weekly";
            this.optWeekly.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.optWeekly_ToggleStateChanged);
            // 
            // lblUpdate
            // 
            this.lblUpdate.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdate.ForeColor = System.Drawing.Color.Red;
            this.lblUpdate.Location = new System.Drawing.Point(371, 12);
            this.lblUpdate.Name = "lblUpdate";
            // 
            // 
            // 
            this.lblUpdate.RootElement.ForeColor = System.Drawing.Color.Red;
            this.lblUpdate.Size = new System.Drawing.Size(2, 2);
            this.lblUpdate.TabIndex = 243;
            // 
            // frmMultiBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 784);
            this.Controls.Add(this.radPanel4);
            this.Controls.Add(this.optWeekly);
            this.Controls.Add(this.optDaily);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmMultiBooking";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   ";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMultiBooking_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkSkipWeekEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            this.radPanel3.ResumeLayout(false);
            this.radPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReturnFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalculateFares)).EndInit();
            this.pnlReturn.ResumeLayout(false);
            this.pnlReturn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_ReturnStartPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_ReturnStartPickupDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupDates.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupDates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPickupDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPickupDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_StartPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_StartPickupDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDestination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOrigin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCreateBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsCompanyRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
            this.radPageView1.ResumeLayout(false);
            this.pg_Daily.ResumeLayout(false);
            this.pg_weekly.ResumeLayout(false);
            this.pg_weekly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndingDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).EndInit();
            this.radPanel5.ResumeLayout(false);
            this.radPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lblweekcompanyreturnfares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeekCompanyFareRateReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCompanyFareRateWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblweekcustreturnfares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeekCustFareRateReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCustFareRateWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWeekReturnPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnWeekJourney)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpWeekPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReturnWeekPickupTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWeekReturnFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeekFareRateReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFareRateWeek)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalculateWeekFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnWeeklyCreateBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSun)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkSat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkThurs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkWed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAutoRecurred)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartingAt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWeeks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkMon)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkReturnConfirm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlReturnAllocatedDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkConfirmDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).EndInit();
            this.radPanel4.ResumeLayout(false);
            this.radPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optDaily)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optWeekly)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private System.Windows.Forms.Panel pnlReturn;
        private Telerik.WinControls.UI.RadLabel radLabel10;
        private UI.MyDatePicker dtp_ReturnStartPickupTime;
        private UI.MyDatePicker dtp_ReturnStartPickupDate;
        private Telerik.WinControls.UI.RadLabel radLabel8;
        private Telerik.WinControls.UI.RadCheckBox chkIsReturn;
        private Telerik.WinControls.UI.RadGridView grdPickupDates;
        private Telerik.WinControls.UI.RadButton btnDeleteRow;
        private UI.MyDropDownList ddlPickupDay;
        private Telerik.WinControls.UI.RadButton btnAddPickupDate;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private UI.MyDatePicker dtp_StartPickupTime;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private UI.MyDatePicker dtp_StartPickupDate;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadCheckBox chkDestination;
        private Telerik.WinControls.UI.RadCheckBox chkOrigin;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadSpinEditor numDays;
        private Telerik.WinControls.UI.RadCheckBox chkSame;
        private Telerik.WinControls.UI.RadButton btnCreateBooking;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnSaveBooking;
        private Telerik.WinControls.UI.RadPanel radPanel4;
        private UI.MyGridView grdBookings;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadSpinEditor numReturnFare;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadSpinEditor numFareRate;
        private Telerik.WinControls.UI.RadButton btnCalculateFares;
        private Telerik.WinControls.UI.RadCheckBox chkIsCompanyRates;
        private UI.MyDropDownList ddlCompany;
        private Telerik.WinControls.UI.RadLabel lblDepartment;
        private UI.MyDropDownList ddlDepartment;
        private Telerik.WinControls.UI.RadLabel radLabel7;
        private UI.MyDropDownList ddlPaymentType;
        private Telerik.WinControls.UI.RadCheckBox chkSkipWeekEnd;
        private Telerik.WinControls.UI.RadPageView radPageView1;
        private Telerik.WinControls.UI.RadPageViewPage pg_Daily;
        private Telerik.WinControls.UI.RadPageViewPage pg_weekly;
        private Telerik.WinControls.UI.RadRadioButton optDaily;
        private Telerik.WinControls.UI.RadRadioButton optWeekly;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadCheckBox chkTue;
        private Telerik.WinControls.UI.RadCheckBox chkMon;
        private Telerik.WinControls.UI.RadCheckBox chkAutoRecurred;
        private UI.MyDatePicker dtpStartingAt;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel9;
        private Telerik.WinControls.UI.RadSpinEditor numWeeks;
        private Telerik.WinControls.UI.RadCheckBox chkThurs;
        private Telerik.WinControls.UI.RadCheckBox chkWed;
        private Telerik.WinControls.UI.RadCheckBox chkSun;
        private Telerik.WinControls.UI.RadCheckBox chkSat;
        private Telerik.WinControls.UI.RadCheckBox chkFri;
        private Telerik.WinControls.UI.RadButton btnWeeklyCreateBooking;
        private Telerik.WinControls.UI.RadPanel radPanel5;
        private Telerik.WinControls.UI.RadLabel lblWeekReturnFare;
        private Telerik.WinControls.UI.RadSpinEditor numWeekFareRateReturn;
        private Telerik.WinControls.UI.RadLabel radLabel13;
        private Telerik.WinControls.UI.RadSpinEditor numFareRateWeek;
        private Telerik.WinControls.UI.RadButton btnCalculateWeekFare;
        private Telerik.WinControls.UI.RadCheckBox chkReturnWeekJourney;
        private UI.MyDatePicker dtpWeekReturnPickupTime;
        private UI.MyDatePicker dtpWeekPickupTime;
        private Telerik.WinControls.UI.RadLabel radLabel14;
        private Telerik.WinControls.UI.RadLabel lblReturnWeekPickupTime;
        private Telerik.WinControls.UI.RadCheckBox chkConfirmDriver;
        private UI.MyDropDownList ddlDriver;
        private UI.MyDropDownList ddlReturnAllocatedDriver;
        private Telerik.WinControls.UI.RadCheckBox chkReturnConfirm;
        private UI.MyDatePicker dtpEndingDate;
        private Telerik.WinControls.UI.RadLabel radLabel12;
        private Telerik.WinControls.UI.RadLabel lblweekcompanyreturnfares;
        private Telerik.WinControls.UI.RadSpinEditor numWeekCompanyFareRateReturn;
        private Telerik.WinControls.UI.RadLabel radLabel18;
        private Telerik.WinControls.UI.RadSpinEditor numCompanyFareRateWeek;
        private Telerik.WinControls.UI.RadLabel lblweekcustreturnfares;
        private Telerik.WinControls.UI.RadSpinEditor numWeekCustFareRateReturn;
        private Telerik.WinControls.UI.RadLabel radLabel16;
        private Telerik.WinControls.UI.RadSpinEditor numCustFareRateWeek;
        private Telerik.WinControls.UI.RadLabel lblUpdate;
    }
}