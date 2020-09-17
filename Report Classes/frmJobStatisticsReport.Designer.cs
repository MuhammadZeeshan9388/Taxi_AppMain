namespace Taxi_AppMain
{
    partial class frmJobStatisticsReport
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.txtPostCode = new Telerik.WinControls.UI.RadTextBox();
            this.ddlLocation = new Telerik.WinControls.UI.RadDropDownList();
            this.PnlArea = new Telerik.WinControls.UI.RadPanel();
            this.rbtnDestination = new Telerik.WinControls.UI.RadRadioButton();
            this.rbtnPickup = new Telerik.WinControls.UI.RadRadioButton();
            this.rbtnBoth = new Telerik.WinControls.UI.RadRadioButton();
            this.ddlArea = new Telerik.WinControls.UI.RadDropDownList();
            this.btnEmail = new Telerik.WinControls.UI.RadButton();
            this.lblSearchType = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.rbtnPostCode = new Telerik.WinControls.UI.RadRadioButton();
            this.rbtnArea = new Telerik.WinControls.UI.RadRadioButton();
            this.rbtnLocation = new Telerik.WinControls.UI.RadRadioButton();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.btnViewReport = new Telerik.WinControls.UI.RadButton();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.fromDate = new UI.MyDatePicker();
            this.tillDate = new UI.MyDatePicker();
            this.grdJobsStatistics = new Telerik.WinControls.UI.RadGridView();
            this.lblTotalJobs = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PnlArea)).BeginInit();
            this.PnlArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDestination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPickup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBoth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSearchType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobsStatistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobsStatistics.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.txtPostCode);
            this.radPanel1.Controls.Add(this.ddlLocation);
            this.radPanel1.Controls.Add(this.PnlArea);
            this.radPanel1.Controls.Add(this.ddlArea);
            this.radPanel1.Controls.Add(this.btnEmail);
            this.radPanel1.Controls.Add(this.lblSearchType);
            this.radPanel1.Controls.Add(this.radPanel2);
            this.radPanel1.Controls.Add(this.btnExit1);
            this.radPanel1.Controls.Add(this.btnExport);
            this.radPanel1.Controls.Add(this.btnPrint);
            this.radPanel1.Controls.Add(this.btnViewReport);
            this.radPanel1.Controls.Add(this.radLabel3);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Controls.Add(this.fromDate);
            this.radPanel1.Controls.Add(this.tillDate);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1243, 161);
            this.radPanel1.TabIndex = 106;
            // 
            // txtPostCode
            // 
            this.txtPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPostCode.Location = new System.Drawing.Point(96, 62);
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(212, 24);
            this.txtPostCode.TabIndex = 107;
            this.txtPostCode.TabStop = false;
            // 
            // ddlLocation
            // 
            this.ddlLocation.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlLocation.Location = new System.Drawing.Point(95, 61);
            this.ddlLocation.Name = "ddlLocation";
            this.ddlLocation.Size = new System.Drawing.Size(213, 26);
            this.ddlLocation.TabIndex = 108;
            this.ddlLocation.Text = "radDropDownList1";
            // 
            // PnlArea
            // 
            this.PnlArea.Controls.Add(this.rbtnDestination);
            this.PnlArea.Controls.Add(this.rbtnPickup);
            this.PnlArea.Controls.Add(this.rbtnBoth);
            this.PnlArea.Location = new System.Drawing.Point(389, 18);
            this.PnlArea.Name = "PnlArea";
            this.PnlArea.Size = new System.Drawing.Size(363, 36);
            this.PnlArea.TabIndex = 111;
            // 
            // rbtnDestination
            // 
            this.rbtnDestination.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDestination.Location = new System.Drawing.Point(227, 10);
            this.rbtnDestination.Name = "rbtnDestination";
            this.rbtnDestination.Size = new System.Drawing.Size(123, 18);
            this.rbtnDestination.TabIndex = 109;
            this.rbtnDestination.Text = "Destination";
            // 
            // rbtnPickup
            // 
            this.rbtnPickup.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnPickup.Location = new System.Drawing.Point(104, 10);
            this.rbtnPickup.Name = "rbtnPickup";
            this.rbtnPickup.Size = new System.Drawing.Size(105, 18);
            this.rbtnPickup.TabIndex = 109;
            this.rbtnPickup.Text = "Pickup";
            this.rbtnPickup.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // rbtnBoth
            // 
            this.rbtnBoth.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnBoth.Location = new System.Drawing.Point(4, 10);
            this.rbtnBoth.Name = "rbtnBoth";
            this.rbtnBoth.Size = new System.Drawing.Size(81, 18);
            this.rbtnBoth.TabIndex = 108;
            this.rbtnBoth.Text = "Both";
            this.rbtnBoth.Visible = false;
            // 
            // ddlArea
            // 
            this.ddlArea.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlArea.Location = new System.Drawing.Point(95, 61);
            this.ddlArea.Name = "ddlArea";
            this.ddlArea.Size = new System.Drawing.Size(213, 26);
            this.ddlArea.TabIndex = 109;
            this.ddlArea.Text = "radDropDownList2";
            // 
            // btnEmail
            // 
            this.btnEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmail.Image = global::Taxi_AppMain.Properties.Resources.email;
            this.btnEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnEmail.Location = new System.Drawing.Point(844, 92);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(113, 52);
            this.btnEmail.TabIndex = 107;
            this.btnEmail.Text = "Email";
            this.btnEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEmail.Click += new System.EventHandler(this.btnEmail_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.email;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).Text = "Email";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblSearchType
            // 
            this.lblSearchType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchType.Location = new System.Drawing.Point(8, 64);
            this.lblSearchType.Name = "lblSearchType";
            this.lblSearchType.Size = new System.Drawing.Size(76, 22);
            this.lblSearchType.TabIndex = 7;
            this.lblSearchType.Text = "Post Code";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.rbtnPostCode);
            this.radPanel2.Controls.Add(this.rbtnArea);
            this.radPanel2.Controls.Add(this.rbtnLocation);
            this.radPanel2.Location = new System.Drawing.Point(9, 18);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(359, 36);
            this.radPanel2.TabIndex = 107;
            // 
            // rbtnPostCode
            // 
            this.rbtnPostCode.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnPostCode.Location = new System.Drawing.Point(5, 9);
            this.rbtnPostCode.Name = "rbtnPostCode";
            this.rbtnPostCode.Size = new System.Drawing.Size(110, 18);
            this.rbtnPostCode.TabIndex = 97;
            this.rbtnPostCode.Text = "Post Code";
            this.rbtnPostCode.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.rbtnPostCode.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rbtnPostCode_ToggleStateChanged);
            // 
            // rbtnArea
            // 
            this.rbtnArea.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnArea.Location = new System.Drawing.Point(246, 9);
            this.rbtnArea.Name = "rbtnArea";
            this.rbtnArea.Size = new System.Drawing.Size(92, 18);
            this.rbtnArea.TabIndex = 99;
            this.rbtnArea.Text = "Area";
            this.rbtnArea.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rbtnArea_ToggleStateChanged);
            // 
            // rbtnLocation
            // 
            this.rbtnLocation.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnLocation.Location = new System.Drawing.Point(130, 9);
            this.rbtnLocation.Name = "rbtnLocation";
            this.rbtnLocation.Size = new System.Drawing.Size(110, 18);
            this.rbtnLocation.TabIndex = 98;
            this.rbtnLocation.Text = "Location";
            this.rbtnLocation.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rbtnLocation_ToggleStateChanged);
            // 
            // btnExit1
            // 
            this.btnExit1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit1.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit1.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExit1.Location = new System.Drawing.Point(994, 92);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(113, 52);
            this.btnExit1.TabIndex = 96;
            this.btnExit1.Text = "Exit";
            this.btnExit1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit1.Click += new System.EventHandler(this.btnExit1_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Image = global::Taxi_AppMain.Properties.Resources.pdf1;
            this.btnExport.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExport.Location = new System.Drawing.Point(691, 92);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(113, 52);
            this.btnExport.TabIndex = 95;
            this.btnExport.Text = "Export Report";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.pdf1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).Text = "Export Report";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Image = global::Taxi_AppMain.Properties.Resources.Print1;
            this.btnPrint.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPrint.Location = new System.Drawing.Point(538, 92);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(113, 52);
            this.btnPrint.TabIndex = 94;
            this.btnPrint.Text = "Print Report";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Print1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).Text = "Print Report";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnViewReport
            // 
            this.btnViewReport.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewReport.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnViewReport.Location = new System.Drawing.Point(388, 92);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(113, 52);
            this.btnViewReport.TabIndex = 93;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewReport.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewReport.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewReport.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewReport.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewReport.GetChildAt(0))).Text = "View Report";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(8, 122);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(64, 22);
            this.radLabel3.TabIndex = 9;
            this.radLabel3.Text = "Till Date";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(8, 92);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(79, 22);
            this.radLabel2.TabIndex = 6;
            this.radLabel2.Text = "From Date";
            // 
            // fromDate
            // 
            this.fromDate.CustomFormat = "dd/MM/yyyy";
            this.fromDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.fromDate.Location = new System.Drawing.Point(95, 92);
            this.fromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.fromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.fromDate.Name = "fromDate";
            this.fromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.fromDate.Size = new System.Drawing.Size(213, 24);
            this.fromDate.TabIndex = 7;
            this.fromDate.TabStop = false;
            this.fromDate.Text = "myDatePicker1";
            this.fromDate.Value = null;
            // 
            // tillDate
            // 
            this.tillDate.CustomFormat = "dd/MM/yyyy";
            this.tillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tillDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.tillDate.Location = new System.Drawing.Point(95, 120);
            this.tillDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.tillDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tillDate.Name = "tillDate";
            this.tillDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tillDate.Size = new System.Drawing.Size(213, 24);
            this.tillDate.TabIndex = 8;
            this.tillDate.TabStop = false;
            this.tillDate.Text = "myDatePicker1";
            this.tillDate.Value = null;
            // 
            // grdJobsStatistics
            // 
            this.grdJobsStatistics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdJobsStatistics.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdJobsStatistics.Location = new System.Drawing.Point(0, 234);
            // 
            // grdJobsStatistics
            // 
            this.grdJobsStatistics.MasterTemplate.AllowAddNewRow = false;
            this.grdJobsStatistics.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.grdJobsStatistics.MasterTemplate.AllowDeleteRow = false;
            this.grdJobsStatistics.MasterTemplate.AllowEditRow = false;
            this.grdJobsStatistics.MasterTemplate.EnableFiltering = true;
            this.grdJobsStatistics.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdJobsStatistics.Name = "grdJobsStatistics";
            this.grdJobsStatistics.ShowGroupPanel = false;
            this.grdJobsStatistics.Size = new System.Drawing.Size(1243, 738);
            this.grdJobsStatistics.TabIndex = 107;
            this.grdJobsStatistics.Text = "radGridView1";
            // 
            // lblTotalJobs
            // 
            this.lblTotalJobs.BackColor = System.Drawing.Color.NavajoWhite;
            this.lblTotalJobs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalJobs.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalJobs.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalJobs.Location = new System.Drawing.Point(0, 199);
            this.lblTotalJobs.Name = "lblTotalJobs";
            this.lblTotalJobs.Size = new System.Drawing.Size(1243, 35);
            this.lblTotalJobs.TabIndex = 116;
            this.lblTotalJobs.Text = "Total Jobs ";
            this.lblTotalJobs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmJobStatisticsReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 972);
            this.Controls.Add(this.grdJobsStatistics);
            this.Controls.Add(this.lblTotalJobs);
            this.Controls.Add(this.radPanel1);
            this.FormTitle = "Job Statistics Report";
            this.Name = "frmJobStatisticsReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Job Statistics Report";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.lblTotalJobs, 0);
            this.Controls.SetChildIndex(this.grdJobsStatistics, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PnlArea)).EndInit();
            this.PnlArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDestination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPickup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnBoth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSearchType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobsStatistics.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobsStatistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private UI.MyDatePicker fromDate;
        private UI.MyDatePicker tillDate;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnExport;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadButton btnViewReport;
        private Telerik.WinControls.UI.RadRadioButton rbtnPostCode;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadRadioButton rbtnLocation;
        private Telerik.WinControls.UI.RadRadioButton rbtnArea;
        private Telerik.WinControls.UI.RadTextBox txtPostCode;
        private Telerik.WinControls.UI.RadButton btnEmail;
        private Telerik.WinControls.UI.RadGridView grdJobsStatistics;
        private Telerik.WinControls.UI.RadDropDownList ddlLocation;
        private Telerik.WinControls.UI.RadDropDownList ddlArea;
        private Telerik.WinControls.UI.RadRadioButton rbtnPickup;
        private Telerik.WinControls.UI.RadRadioButton rbtnBoth;
        private Telerik.WinControls.UI.RadLabel lblSearchType;
        private Telerik.WinControls.UI.RadRadioButton rbtnDestination;
        private Telerik.WinControls.UI.RadPanel PnlArea;
        private System.Windows.Forms.Label lblTotalJobs;
    }
}