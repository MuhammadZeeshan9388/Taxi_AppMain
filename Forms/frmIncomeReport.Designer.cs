namespace Taxi_AppMain
{
    partial class frmIncomeReport
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
            this.grdLister = new UI.MyGridView();
            this.lblTotalEarning = new System.Windows.Forms.Label();
            this.lblCriteria = new System.Windows.Forms.Label();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnExportExcel = new Telerik.WinControls.UI.RadButton();
            this.chkAllVehicle = new Telerik.WinControls.UI.RadCheckBox();
            this.ddlCompanyVehicle = new Telerik.WinControls.UI.RadDropDownList();
            this.dtptilltime = new UI.MyDatePicker();
            this.dtpFromTime = new UI.MyDatePicker();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.ddlSubCompany = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlDriver = new Telerik.WinControls.UI.RadDropDownList();
            this.ddlCompany = new Telerik.WinControls.UI.RadDropDownList();
            this.optBoth = new Telerik.WinControls.UI.RadRadioButton();
            this.optCash = new Telerik.WinControls.UI.RadRadioButton();
            this.optAccount = new Telerik.WinControls.UI.RadRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSendEmail = new Telerik.WinControls.UI.RadButton();
            this.btnExportPDF = new Telerik.WinControls.UI.RadButton();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.btnViewReport = new Telerik.WinControls.UI.RadButton();
            this.dtpTillDate = new UI.MyDatePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtpFromDate = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.lblTotalJobs = new System.Windows.Forms.Label();
            this.lblTotal50PerEarning = new System.Windows.Forms.Label();
            this.lblTotal15PerEarning = new System.Windows.Forms.Label();
            this.lblTotal100Earning = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompanyVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtptilltime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSubCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optBoth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = false;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 224);
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(1254, 592);
            this.grdLister.TabIndex = 114;
            this.grdLister.Text = "myGridView1";
            // 
            // lblTotalEarning
            // 
            this.lblTotalEarning.BackColor = System.Drawing.Color.NavajoWhite;
            this.lblTotalEarning.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalEarning.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalEarning.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotalEarning.ForeColor = System.Drawing.Color.Red;
            this.lblTotalEarning.Location = new System.Drawing.Point(0, 189);
            this.lblTotalEarning.Name = "lblTotalEarning";
            this.lblTotalEarning.Size = new System.Drawing.Size(1254, 35);
            this.lblTotalEarning.TabIndex = 113;
            this.lblTotalEarning.Text = "Total Earning £ 0.00 ";
            this.lblTotalEarning.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCriteria
            // 
            this.lblCriteria.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCriteria.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria.Location = new System.Drawing.Point(0, 157);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(1254, 32);
            this.lblCriteria.TabIndex = 112;
            this.lblCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.GhostWhite;
            this.radPanel1.Controls.Add(this.btnExportExcel);
            this.radPanel1.Controls.Add(this.chkAllVehicle);
            this.radPanel1.Controls.Add(this.ddlCompanyVehicle);
            this.radPanel1.Controls.Add(this.dtptilltime);
            this.radPanel1.Controls.Add(this.dtpFromTime);
            this.radPanel1.Controls.Add(this.radLabel4);
            this.radPanel1.Controls.Add(this.ddlSubCompany);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Controls.Add(this.ddlDriver);
            this.radPanel1.Controls.Add(this.ddlCompany);
            this.radPanel1.Controls.Add(this.optBoth);
            this.radPanel1.Controls.Add(this.optCash);
            this.radPanel1.Controls.Add(this.optAccount);
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Controls.Add(this.btnSendEmail);
            this.radPanel1.Controls.Add(this.btnExportPDF);
            this.radPanel1.Controls.Add(this.btnPrint);
            this.radPanel1.Controls.Add(this.btnViewReport);
            this.radPanel1.Controls.Add(this.dtpTillDate);
            this.radPanel1.Controls.Add(this.radLabel3);
            this.radPanel1.Controls.Add(this.dtpFromDate);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1254, 119);
            this.radPanel1.TabIndex = 111;
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.Image = global::Taxi_AppMain.Properties.Resources.excel;
            this.btnExportExcel.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExportExcel.Location = new System.Drawing.Point(938, 64);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(119, 52);
            this.btnExportExcel.TabIndex = 119;
            this.btnExportExcel.Text = "Export To EXCEL";
            this.btnExportExcel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportExcel.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.excel;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportExcel.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportExcel.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportExcel.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportExcel.GetChildAt(0))).Text = "Export To EXCEL";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExportExcel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExportExcel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkAllVehicle
            // 
            this.chkAllVehicle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllVehicle.Location = new System.Drawing.Point(374, 61);
            this.chkAllVehicle.Name = "chkAllVehicle";
            this.chkAllVehicle.Size = new System.Drawing.Size(96, 23);
            this.chkAllVehicle.TabIndex = 118;
            this.chkAllVehicle.Text = "All Vehicle";
            this.chkAllVehicle.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // ddlCompanyVehicle
            // 
            this.ddlCompanyVehicle.Enabled = false;
            this.ddlCompanyVehicle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompanyVehicle.Location = new System.Drawing.Point(475, 58);
            this.ddlCompanyVehicle.Name = "ddlCompanyVehicle";
            this.ddlCompanyVehicle.Size = new System.Drawing.Size(176, 26);
            this.ddlCompanyVehicle.TabIndex = 117;
            // 
            // dtptilltime
            // 
            this.dtptilltime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtptilltime.CustomFormat = "HH:mm";
            this.dtptilltime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptilltime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtptilltime.Location = new System.Drawing.Point(536, 32);
            this.dtptilltime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtptilltime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtptilltime.Name = "dtptilltime";
            this.dtptilltime.NullDate = new System.DateTime(((long)(0)));
            this.dtptilltime.ShowUpDown = true;
            this.dtptilltime.Size = new System.Drawing.Size(73, 24);
            this.dtptilltime.TabIndex = 116;
            this.dtptilltime.TabStop = false;
            this.dtptilltime.Text = "myDatePicker1";
            this.dtptilltime.Value = new System.DateTime(2014, 10, 21, 0, 0, 0, 0);
            // 
            // dtpFromTime
            // 
            this.dtpFromTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFromTime.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.dtpFromTime.CustomFormat = "HH:mm";
            this.dtpFromTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromTime.Location = new System.Drawing.Point(238, 32);
            this.dtpFromTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromTime.Name = "dtpFromTime";
            this.dtpFromTime.NullDate = new System.DateTime(((long)(0)));
            this.dtpFromTime.ShowUpDown = true;
            this.dtpFromTime.Size = new System.Drawing.Size(73, 24);
            this.dtpFromTime.TabIndex = 115;
            this.dtpFromTime.TabStop = false;
            this.dtpFromTime.Text = "myDatePicker1";
            this.dtpFromTime.Value = new System.DateTime(2014, 10, 21, 0, 0, 0, 0);
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(11, 92);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(102, 22);
            this.radLabel4.TabIndex = 114;
            this.radLabel4.Text = "Sub Company";
            // 
            // ddlSubCompany
            // 
            this.ddlSubCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSubCompany.Location = new System.Drawing.Point(117, 90);
            this.ddlSubCompany.Name = "ddlSubCompany";
            this.ddlSubCompany.Size = new System.Drawing.Size(253, 26);
            this.ddlSubCompany.TabIndex = 113;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(11, 62);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(48, 22);
            this.radLabel1.TabIndex = 112;
            this.radLabel1.Text = "Driver";
            // 
            // ddlDriver
            // 
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(117, 60);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Size = new System.Drawing.Size(253, 26);
            this.ddlDriver.TabIndex = 111;
            // 
            // ddlCompany
            // 
            this.ddlCompany.Enabled = false;
            this.ddlCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompany.Location = new System.Drawing.Point(405, 3);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(253, 26);
            this.ddlCompany.TabIndex = 110;
            // 
            // optBoth
            // 
            this.optBoth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBoth.Location = new System.Drawing.Point(117, 7);
            this.optBoth.Name = "optBoth";
            this.optBoth.Size = new System.Drawing.Size(62, 18);
            this.optBoth.TabIndex = 101;
            this.optBoth.Text = "Both";
            this.optBoth.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // optCash
            // 
            this.optCash.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCash.Location = new System.Drawing.Point(184, 7);
            this.optCash.Name = "optCash";
            this.optCash.Size = new System.Drawing.Size(97, 18);
            this.optCash.TabIndex = 100;
            this.optCash.Text = "Cash Jobs";
            // 
            // optAccount
            // 
            this.optAccount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optAccount.Location = new System.Drawing.Point(285, 7);
            this.optAccount.Name = "optAccount";
            this.optAccount.Size = new System.Drawing.Size(117, 18);
            this.optAccount.TabIndex = 99;
            this.optAccount.Text = "Account Jobs";
            this.optAccount.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.optAccount_ToggleStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 18);
            this.label1.TabIndex = 98;
            this.label1.Text = "Report Type";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Image = global::Taxi_AppMain.Properties.Resources.email;
            this.btnSendEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSendEmail.Location = new System.Drawing.Point(935, 4);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(122, 52);
            this.btnSendEmail.TabIndex = 97;
            this.btnSendEmail.Text = "Email";
            this.btnSendEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.email;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).Text = "Email";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportPDF.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportPDF.Image = global::Taxi_AppMain.Properties.Resources.pdf1;
            this.btnExportPDF.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExportPDF.Location = new System.Drawing.Point(801, 64);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(103, 52);
            this.btnExportPDF.TabIndex = 88;
            this.btnExportPDF.Text = "Export To PDF";
            this.btnExportPDF.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportPDF.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.pdf1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportPDF.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportPDF.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportPDF.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportPDF.GetChildAt(0))).Text = "Export To PDF";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExportPDF.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExportPDF.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::Taxi_AppMain.Properties.Resources.Print1;
            this.btnPrint.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPrint.Location = new System.Drawing.Point(797, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(108, 52);
            this.btnPrint.TabIndex = 87;
            this.btnPrint.Text = "PRINT";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Print1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).Text = "PRINT";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(657, 55);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(113, 53);
            this.btnViewReport.TabIndex = 8;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.TextWrap = true;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewReport.GetChildAt(0))).Text = "View Report";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpTillDate
            // 
            this.dtpTillDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTillDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTillDate.Location = new System.Drawing.Point(416, 32);
            this.dtpTillDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTillDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Name = "dtpTillDate";
            this.dtpTillDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Size = new System.Drawing.Size(114, 24);
            this.dtpTillDate.TabIndex = 3;
            this.dtpTillDate.TabStop = false;
            this.dtpTillDate.Text = "myDatePicker2";
            this.dtpTillDate.Value = null;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(345, 34);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(62, 22);
            this.radLabel3.TabIndex = 2;
            this.radLabel3.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate.Location = new System.Drawing.Point(118, 32);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(114, 24);
            this.dtpFromDate.TabIndex = 1;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "myDatePicker1";
            this.dtpFromDate.Value = null;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(12, 34);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(79, 22);
            this.radLabel2.TabIndex = 0;
            this.radLabel2.Text = "From Date";
            // 
            // lblTotalJobs
            // 
            this.lblTotalJobs.AutoSize = true;
            this.lblTotalJobs.BackColor = System.Drawing.Color.NavajoWhite;
            this.lblTotalJobs.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotalJobs.Location = new System.Drawing.Point(6, 195);
            this.lblTotalJobs.Name = "lblTotalJobs";
            this.lblTotalJobs.Size = new System.Drawing.Size(128, 22);
            this.lblTotalJobs.TabIndex = 115;
            this.lblTotalJobs.Text = "Total Jobs : 0";
            // 
            // lblTotal50PerEarning
            // 
            this.lblTotal50PerEarning.AutoSize = true;
            this.lblTotal50PerEarning.BackColor = System.Drawing.Color.NavajoWhite;
            this.lblTotal50PerEarning.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotal50PerEarning.ForeColor = System.Drawing.Color.Red;
            this.lblTotal50PerEarning.Location = new System.Drawing.Point(386, 195);
            this.lblTotal50PerEarning.Name = "lblTotal50PerEarning";
            this.lblTotal50PerEarning.Size = new System.Drawing.Size(164, 22);
            this.lblTotal50PerEarning.TabIndex = 116;
            this.lblTotal50PerEarning.Text = "Total 50% £ 0.00";
            // 
            // lblTotal15PerEarning
            // 
            this.lblTotal15PerEarning.AutoSize = true;
            this.lblTotal15PerEarning.BackColor = System.Drawing.Color.NavajoWhite;
            this.lblTotal15PerEarning.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotal15PerEarning.ForeColor = System.Drawing.Color.Red;
            this.lblTotal15PerEarning.Location = new System.Drawing.Point(162, 195);
            this.lblTotal15PerEarning.Name = "lblTotal15PerEarning";
            this.lblTotal15PerEarning.Size = new System.Drawing.Size(164, 22);
            this.lblTotal15PerEarning.TabIndex = 116;
            this.lblTotal15PerEarning.Text = "Total 15% £ 0.00";
            // 
            // lblTotal100Earning
            // 
            this.lblTotal100Earning.AutoSize = true;
            this.lblTotal100Earning.BackColor = System.Drawing.Color.NavajoWhite;
            this.lblTotal100Earning.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.lblTotal100Earning.ForeColor = System.Drawing.Color.Red;
            this.lblTotal100Earning.Location = new System.Drawing.Point(623, 195);
            this.lblTotal100Earning.Name = "lblTotal100Earning";
            this.lblTotal100Earning.Size = new System.Drawing.Size(175, 22);
            this.lblTotal100Earning.TabIndex = 117;
            this.lblTotal100Earning.Text = "Total 100% £ 0.00";
            // 
            // frmIncomeReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 816);
            this.Controls.Add(this.lblTotal100Earning);
            this.Controls.Add(this.lblTotal15PerEarning);
            this.Controls.Add(this.lblTotal50PerEarning);
            this.Controls.Add(this.lblTotalJobs);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.lblTotalEarning);
            this.Controls.Add(this.lblCriteria);
            this.Controls.Add(this.radPanel1);
            this.FormTitle = "Income Report";
            this.Name = "frmIncomeReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Income Report";
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.lblCriteria, 0);
            this.Controls.SetChildIndex(this.lblTotalEarning, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            this.Controls.SetChildIndex(this.lblTotalJobs, 0);
            this.Controls.SetChildIndex(this.lblTotal50PerEarning, 0);
            this.Controls.SetChildIndex(this.lblTotal15PerEarning, 0);
            this.Controls.SetChildIndex(this.lblTotal100Earning, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompanyVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtptilltime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSubCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optBoth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.MyGridView grdLister;
        private System.Windows.Forms.Label lblTotalEarning;
        private System.Windows.Forms.Label lblCriteria;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnViewReport;
        private UI.MyDatePicker dtpTillDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private UI.MyDatePicker dtpFromDate;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton btnExportPDF;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private System.Windows.Forms.Label lblTotalJobs;
        private Telerik.WinControls.UI.RadButton btnSendEmail;
        private Telerik.WinControls.UI.RadRadioButton optBoth;
        private Telerik.WinControls.UI.RadRadioButton optCash;
        private Telerik.WinControls.UI.RadRadioButton optAccount;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList ddlDriver;
        private Telerik.WinControls.UI.RadDropDownList ddlCompany;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadDropDownList ddlSubCompany;
        private UI.MyDatePicker dtpFromTime;
        private UI.MyDatePicker dtptilltime;
        private Telerik.WinControls.UI.RadDropDownList ddlCompanyVehicle;
        private Telerik.WinControls.UI.RadCheckBox chkAllVehicle;
		private Telerik.WinControls.UI.RadButton btnExportExcel;
		private System.Windows.Forms.Label lblTotal50PerEarning;
		private System.Windows.Forms.Label lblTotal15PerEarning;
        private System.Windows.Forms.Label lblTotal100Earning;
    }
}