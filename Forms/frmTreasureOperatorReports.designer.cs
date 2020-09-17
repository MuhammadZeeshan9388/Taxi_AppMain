namespace Taxi_AppMain
{
    partial class frmTreasureOperatorReports
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdOperatorPrivateHireDriverRecord = new Telerik.WinControls.UI.RadGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpToDate = new UI.MyDatePicker();
            this.dtpFromDate = new UI.MyDatePicker();
            this.radProgressBar1 = new Telerik.WinControls.UI.RadProgressBar();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            this.btnShowOperator = new Telerik.WinControls.UI.RadButton();
            this.rdoCanceled = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoCompleted = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoAll = new Telerik.WinControls.UI.RadRadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlSubCompany = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdOperatorVehicleRecord = new Telerik.WinControls.UI.RadGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radProgressBar2 = new Telerik.WinControls.UI.RadProgressBar();
            this.btnExport2 = new Telerik.WinControls.UI.RadButton();
            this.btnShowVehicle = new Telerik.WinControls.UI.RadButton();
            this.rdoCanceled2 = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoCompleted2 = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoAll2 = new Telerik.WinControls.UI.RadRadioButton();
            this.dtpToDate2 = new Telerik.WinControls.UI.RadDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFromDate2 = new Telerik.WinControls.UI.RadDateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ddlSubCompany2 = new System.Windows.Forms.ComboBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperatorPrivateHireDriverRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperatorPrivateHireDriverRecord.MasterTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowOperator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCanceled)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCompleted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAll)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperatorVehicleRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperatorVehicleRecord.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radProgressBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowVehicle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCanceled2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCompleted2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAll2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Click += new System.EventHandler(this.btnSaveOn_Click);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Click += new System.EventHandler(this.btnOnNew_Click);
            // 
            // btnExit
            // 
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Click += new System.EventHandler(this.btnSaveAndNew_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1214, 654);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdOperatorPrivateHireDriverRecord);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1206, 625);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Operator Private Hire Driver Record";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // grdOperatorPrivateHireDriverRecord
            // 
            this.grdOperatorPrivateHireDriverRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOperatorPrivateHireDriverRecord.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdOperatorPrivateHireDriverRecord.Location = new System.Drawing.Point(3, 34);
            // 
            // grdOperatorPrivateHireDriverRecord
            // 
            this.grdOperatorPrivateHireDriverRecord.MasterTemplate.AllowAddNewRow = false;
            this.grdOperatorPrivateHireDriverRecord.MasterTemplate.AllowEditRow = false;
            this.grdOperatorPrivateHireDriverRecord.MasterTemplate.EnableFiltering = true;
            this.grdOperatorPrivateHireDriverRecord.MasterTemplate.EnableGrouping = false;
            this.grdOperatorPrivateHireDriverRecord.MasterTemplate.ShowGroupedColumns = true;
            this.grdOperatorPrivateHireDriverRecord.Name = "grdOperatorPrivateHireDriverRecord";
            this.grdOperatorPrivateHireDriverRecord.ShowGroupPanel = false;
            this.grdOperatorPrivateHireDriverRecord.Size = new System.Drawing.Size(1200, 588);
            this.grdOperatorPrivateHireDriverRecord.TabIndex = 0;
            this.grdOperatorPrivateHireDriverRecord.Text = "radGridView1";
            this.grdOperatorPrivateHireDriverRecord.Click += new System.EventHandler(this.grdOperatorPrivateHireDriverRecord_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpToDate);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.radProgressBar1);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnShowOperator);
            this.panel1.Controls.Add(this.rdoCanceled);
            this.panel1.Controls.Add(this.rdoCompleted);
            this.panel1.Controls.Add(this.rdoAll);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ddlSubCompany);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 31);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpToDate.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpToDate.Location = new System.Drawing.Point(547, 3);
            this.dtpToDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpToDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpToDate.Size = new System.Drawing.Size(107, 21);
            this.dtpToDate.TabIndex = 6;
            this.dtpToDate.TabStop = false;
            this.dtpToDate.Text = "myDatePicker1";
            this.dtpToDate.Value = null;
            this.dtpToDate.ValueChanged += new System.EventHandler(this.dtpToDate_ValueChanged);
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate.Location = new System.Drawing.Point(385, 3);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(107, 21);
            this.dtpFromDate.TabIndex = 5;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "myDatePicker1";
            this.dtpFromDate.Value = null;
            this.dtpFromDate.ValueChanged += new System.EventHandler(this.dtpFromDate_ValueChanged);
            // 
            // radProgressBar1
            // 
            this.radProgressBar1.Dash = false;
            this.radProgressBar1.Location = new System.Drawing.Point(1066, 4);
            this.radProgressBar1.Name = "radProgressBar1";
            this.radProgressBar1.Size = new System.Drawing.Size(121, 23);
            this.radProgressBar1.TabIndex = 227;
            this.radProgressBar1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radProgressBar1.Visible = false;
            this.radProgressBar1.Click += new System.EventHandler(this.radProgressBar1_Click);
            // 
            // btnExport
            // 
            this.btnExport.Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            this.btnExport.Location = new System.Drawing.Point(964, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(88, 24);
            this.btnExport.TabIndex = 226;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click_1);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).Text = "Export";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnShowOperator
            // 
            this.btnShowOperator.Location = new System.Drawing.Point(881, 3);
            this.btnShowOperator.Name = "btnShowOperator";
            this.btnShowOperator.Size = new System.Drawing.Size(72, 24);
            this.btnShowOperator.TabIndex = 9;
            this.btnShowOperator.Text = "Show";
            this.btnShowOperator.Click += new System.EventHandler(this.btnShowOperator_Click_1);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowOperator.GetChildAt(0))).Text = "Show";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowOperator.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowOperator.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // rdoCanceled
            // 
            this.rdoCanceled.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCanceled.Location = new System.Drawing.Point(798, 4);
            this.rdoCanceled.Name = "rdoCanceled";
            this.rdoCanceled.Size = new System.Drawing.Size(86, 18);
            this.rdoCanceled.TabIndex = 8;
            this.rdoCanceled.Text = "Canceled";
            this.rdoCanceled.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdoCanceled_ToggleStateChanged);
            // 
            // rdoCompleted
            // 
            this.rdoCompleted.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCompleted.Location = new System.Drawing.Point(707, 4);
            this.rdoCompleted.Name = "rdoCompleted";
            this.rdoCompleted.Size = new System.Drawing.Size(86, 18);
            this.rdoCompleted.TabIndex = 7;
            this.rdoCompleted.Text = "Completed";
            this.rdoCompleted.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdoCompleted_ToggleStateChanged);
            // 
            // rdoAll
            // 
            this.rdoAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAll.Location = new System.Drawing.Point(659, 5);
            this.rdoAll.Name = "rdoAll";
            this.rdoAll.Size = new System.Drawing.Size(46, 18);
            this.rdoAll.TabIndex = 6;
            this.rdoAll.Text = "All";
            this.rdoAll.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.rdoAll.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdoAll_ToggleStateChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(495, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "To Date";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "From Date";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sub Company";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ddlSubCompany
            // 
            this.ddlSubCompany.FormattingEnabled = true;
            this.ddlSubCompany.Location = new System.Drawing.Point(151, 3);
            this.ddlSubCompany.Name = "ddlSubCompany";
            this.ddlSubCompany.Size = new System.Drawing.Size(149, 24);
            this.ddlSubCompany.TabIndex = 0;
            this.ddlSubCompany.SelectedIndexChanged += new System.EventHandler(this.ddlSubCompany_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdOperatorVehicleRecord);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1206, 625);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Operator Vehicle Record";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // grdOperatorVehicleRecord
            // 
            this.grdOperatorVehicleRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdOperatorVehicleRecord.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdOperatorVehicleRecord.Location = new System.Drawing.Point(3, 34);
            // 
            // grdOperatorVehicleRecord
            // 
            this.grdOperatorVehicleRecord.MasterTemplate.AllowAddNewRow = false;
            this.grdOperatorVehicleRecord.MasterTemplate.AllowEditRow = false;
            this.grdOperatorVehicleRecord.MasterTemplate.EnableFiltering = true;
            this.grdOperatorVehicleRecord.MasterTemplate.EnableGrouping = false;
            this.grdOperatorVehicleRecord.MasterTemplate.ShowGroupedColumns = true;
            this.grdOperatorVehicleRecord.Name = "grdOperatorVehicleRecord";
            this.grdOperatorVehicleRecord.ShowGroupPanel = false;
            this.grdOperatorVehicleRecord.Size = new System.Drawing.Size(1200, 588);
            this.grdOperatorVehicleRecord.TabIndex = 3;
            this.grdOperatorVehicleRecord.Text = "radGridView1";
            this.grdOperatorVehicleRecord.Click += new System.EventHandler(this.grdOperatorVehicleRecord_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radProgressBar2);
            this.panel2.Controls.Add(this.btnExport2);
            this.panel2.Controls.Add(this.btnShowVehicle);
            this.panel2.Controls.Add(this.rdoCanceled2);
            this.panel2.Controls.Add(this.rdoCompleted2);
            this.panel2.Controls.Add(this.rdoAll2);
            this.panel2.Controls.Add(this.dtpToDate2);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.dtpFromDate2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.ddlSubCompany2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1200, 31);
            this.panel2.TabIndex = 2;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // radProgressBar2
            // 
            this.radProgressBar2.Dash = false;
            this.radProgressBar2.Location = new System.Drawing.Point(1060, 4);
            this.radProgressBar2.Name = "radProgressBar2";
            this.radProgressBar2.Size = new System.Drawing.Size(121, 23);
            this.radProgressBar2.TabIndex = 228;
            this.radProgressBar2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radProgressBar2.Visible = false;
            this.radProgressBar2.Click += new System.EventHandler(this.radProgressBar2_Click);
            // 
            // btnExport2
            // 
            this.btnExport2.Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            this.btnExport2.Location = new System.Drawing.Point(961, 3);
            this.btnExport2.Name = "btnExport2";
            this.btnExport2.Size = new System.Drawing.Size(89, 24);
            this.btnExport2.TabIndex = 226;
            this.btnExport2.Text = "Export";
            this.btnExport2.Click += new System.EventHandler(this.btnExport2_Click_1);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport2.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport2.GetChildAt(0))).Text = "Export";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport2.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport2.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnShowVehicle
            // 
            this.btnShowVehicle.Location = new System.Drawing.Point(875, 3);
            this.btnShowVehicle.Name = "btnShowVehicle";
            this.btnShowVehicle.Size = new System.Drawing.Size(72, 24);
            this.btnShowVehicle.TabIndex = 9;
            this.btnShowVehicle.Text = "Show";
            this.btnShowVehicle.Click += new System.EventHandler(this.btnShowVehicle_Click_1);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowVehicle.GetChildAt(0))).Text = "Show";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowVehicle.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowVehicle.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // rdoCanceled2
            // 
            this.rdoCanceled2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCanceled2.Location = new System.Drawing.Point(794, 4);
            this.rdoCanceled2.Name = "rdoCanceled2";
            this.rdoCanceled2.Size = new System.Drawing.Size(86, 18);
            this.rdoCanceled2.TabIndex = 8;
            this.rdoCanceled2.Text = "Canceled";
            this.rdoCanceled2.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdoCanceled2_ToggleStateChanged);
            // 
            // rdoCompleted2
            // 
            this.rdoCompleted2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCompleted2.Location = new System.Drawing.Point(703, 4);
            this.rdoCompleted2.Name = "rdoCompleted2";
            this.rdoCompleted2.Size = new System.Drawing.Size(86, 18);
            this.rdoCompleted2.TabIndex = 7;
            this.rdoCompleted2.Text = "Completed";
            this.rdoCompleted2.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdoCompleted2_ToggleStateChanged);
            // 
            // rdoAll2
            // 
            this.rdoAll2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoAll2.Location = new System.Drawing.Point(661, 5);
            this.rdoAll2.Name = "rdoAll2";
            this.rdoAll2.Size = new System.Drawing.Size(46, 18);
            this.rdoAll2.TabIndex = 6;
            this.rdoAll2.Text = "All";
            this.rdoAll2.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.rdoAll2.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdoAll2_ToggleStateChanged);
            // 
            // dtpToDate2
            // 
            this.dtpToDate2.Culture = new System.Globalization.CultureInfo("en-PK");
            this.dtpToDate2.CustomFormat = "dd/MM/yyyy";
            this.dtpToDate2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate2.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpToDate2.Location = new System.Drawing.Point(557, 4);
            this.dtpToDate2.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpToDate2.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpToDate2.Name = "dtpToDate2";
            this.dtpToDate2.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpToDate2.Size = new System.Drawing.Size(97, 21);
            this.dtpToDate2.TabIndex = 5;
            this.dtpToDate2.TabStop = false;
            this.dtpToDate2.Text = "radDateTimePicker2";
            this.dtpToDate2.Value = new System.DateTime(2016, 8, 26, 16, 45, 18, 948);
            this.dtpToDate2.ValueChanged += new System.EventHandler(this.dtpToDate2_ValueChanged);
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.dtpToDate2.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.dtpToDate2.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(498, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "To Date";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // dtpFromDate2
            // 
            this.dtpFromDate2.Culture = new System.Globalization.CultureInfo("en-PK");
            this.dtpFromDate2.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate2.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate2.Location = new System.Drawing.Point(393, 4);
            this.dtpFromDate2.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate2.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate2.Name = "dtpFromDate2";
            this.dtpFromDate2.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate2.Size = new System.Drawing.Size(97, 21);
            this.dtpFromDate2.TabIndex = 3;
            this.dtpFromDate2.TabStop = false;
            this.dtpFromDate2.Text = "radDateTimePicker1";
            this.dtpFromDate2.Value = new System.DateTime(2016, 8, 26, 16, 45, 18, 948);
            this.dtpFromDate2.ValueChanged += new System.EventHandler(this.dtpFromDate2_ValueChanged);
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.dtpFromDate2.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.dtpFromDate2.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(313, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 2;
            this.label6.Text = "From Date";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(55, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Sub Company";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // ddlSubCompany2
            // 
            this.ddlSubCompany2.FormattingEnabled = true;
            this.ddlSubCompany2.Location = new System.Drawing.Point(155, 3);
            this.ddlSubCompany2.Name = "ddlSubCompany2";
            this.ddlSubCompany2.Size = new System.Drawing.Size(149, 24);
            this.ddlSubCompany2.TabIndex = 0;
            this.ddlSubCompany2.SelectedIndexChanged += new System.EventHandler(this.ddlSubCompany2_SelectedIndexChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel Files (.xls)|*.xls|Advanced Excel Files (.xlsx)|*.xlsx";
            this.saveFileDialog1.FilterIndex = 0;
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "Excel Files (.xls)|*.xls|Advanced Excel Files (.xlsx)|*.xlsx";
            this.saveFileDialog2.FilterIndex = 0;
            this.saveFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog2_FileOk);
            // 
            // frmTreasureOperatorReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 692);
            this.Controls.Add(this.tabControl1);
            this.FormTitle = "PCO Reports";
            this.Name = "frmTreasureOperatorReports";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Operator Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.tabControl1, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdOperatorPrivateHireDriverRecord.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperatorPrivateHireDriverRecord)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowOperator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCanceled)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCompleted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAll)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdOperatorVehicleRecord.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOperatorVehicleRecord)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radProgressBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowVehicle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCanceled2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCompleted2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAll2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToDate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Telerik.WinControls.UI.RadGridView grdOperatorPrivateHireDriverRecord;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ddlSubCompany;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Telerik.WinControls.UI.RadRadioButton rdoAll;
        private Telerik.WinControls.UI.RadRadioButton rdoCanceled;
        private Telerik.WinControls.UI.RadRadioButton rdoCompleted;
        private Telerik.WinControls.UI.RadButton btnShowOperator;
        private Telerik.WinControls.UI.RadButton btnExport;
    
      
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Telerik.WinControls.UI.RadGridView grdOperatorVehicleRecord;
     
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadButton btnExport2;
        private Telerik.WinControls.UI.RadButton btnShowVehicle;
        private Telerik.WinControls.UI.RadRadioButton rdoCanceled2;
        private Telerik.WinControls.UI.RadRadioButton rdoCompleted2;
        private Telerik.WinControls.UI.RadRadioButton rdoAll2;
        private Telerik.WinControls.UI.RadDateTimePicker dtpToDate2;
        private System.Windows.Forms.Label label5;
        private Telerik.WinControls.UI.RadDateTimePicker dtpFromDate2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ddlSubCompany2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private Telerik.WinControls.UI.RadProgressBar radProgressBar1;
        private Telerik.WinControls.UI.RadProgressBar radProgressBar2;
        private UI.MyDatePicker dtpFromDate;
        private UI.MyDatePicker dtpToDate;
    }
}

