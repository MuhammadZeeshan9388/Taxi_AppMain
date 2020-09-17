namespace Taxi_AppMain
{
    partial class frmAddDayWiseDriverShift
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtpToTime = new UI.MyDatePicker();
            this.dtpStartTime = new UI.MyDatePicker();
            this.ddlDays = new UI.MyDropDownList();
            this.ddlShifts = new UI.MyDropDownList();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnSaveClose = new Telerik.WinControls.UI.RadButton();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.lstDayWiseDriver = new Telerik.WinControls.UI.RadListControl();
            this.lstAllDrivers = new Telerik.WinControls.UI.RadListControl();
            this.btnAddSelectedToAllDriver = new Telerik.WinControls.UI.RadButton();
            this.btnAddSelectedToDayDriverWiseShift = new Telerik.WinControls.UI.RadButton();
            this.btnAddAllToAllDriver = new Telerik.WinControls.UI.RadButton();
            this.btnAddAllToDayDriverWiseShift = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDayWiseDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstAllDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddSelectedToAllDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddSelectedToDayDriverWiseShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddAllToAllDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddAllToDayDriverWiseShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radLabel6);
            this.panel1.Controls.Add(this.radLabel5);
            this.panel1.Controls.Add(this.radLabel4);
            this.panel1.Controls.Add(this.radLabel3);
            this.panel1.Controls.Add(this.dtpToTime);
            this.panel1.Controls.Add(this.dtpStartTime);
            this.panel1.Controls.Add(this.ddlDays);
            this.panel1.Controls.Add(this.ddlShifts);
            this.panel1.Controls.Add(this.btnExit1);
            this.panel1.Controls.Add(this.btnSaveClose);
            this.panel1.Controls.Add(this.radLabel2);
            this.panel1.Controls.Add(this.radLabel1);
            this.panel1.Controls.Add(this.lstDayWiseDriver);
            this.panel1.Controls.Add(this.lstAllDrivers);
            this.panel1.Controls.Add(this.btnAddSelectedToAllDriver);
            this.panel1.Controls.Add(this.btnAddSelectedToDayDriverWiseShift);
            this.panel1.Controls.Add(this.btnAddAllToAllDriver);
            this.panel1.Controls.Add(this.btnAddAllToDayDriverWiseShift);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(775, 407);
            this.panel1.TabIndex = 106;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // radLabel6
            // 
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel6.Location = new System.Drawing.Point(106, 79);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(36, 23);
            this.radLabel6.TabIndex = 116;
            this.radLabel6.Text = "Day";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel6.GetChildAt(0))).Text = "Day";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel6.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel6.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel5
            // 
            this.radLabel5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(106, 42);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(97, 23);
            this.radLabel5.TabIndex = 115;
            this.radLabel5.Text = "Drivers Shift";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel5.GetChildAt(0))).Text = "Drivers Shift";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel5.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel5.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(444, 80);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(76, 23);
            this.radLabel4.TabIndex = 114;
            this.radLabel4.Text = "End Time";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel4.GetChildAt(0))).Text = "End Time";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel4.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel4.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(444, 43);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(84, 23);
            this.radLabel3.TabIndex = 113;
            this.radLabel3.Text = "Start Time";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel3.GetChildAt(0))).Text = "Start Time";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel3.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel3.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpToTime
            // 
            this.dtpToTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpToTime.CustomFormat = "HH:mm";
            this.dtpToTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpToTime.Location = new System.Drawing.Point(574, 81);
            this.dtpToTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpToTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpToTime.Name = "dtpToTime";
            this.dtpToTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpToTime.Size = new System.Drawing.Size(126, 21);
            this.dtpToTime.TabIndex = 112;
            this.dtpToTime.TabStop = false;
            this.dtpToTime.Text = "myDatePicker1";
            this.dtpToTime.Value = null;
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpStartTime.CustomFormat = "HH:mm";
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpStartTime.Location = new System.Drawing.Point(574, 43);
            this.dtpStartTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartTime.Size = new System.Drawing.Size(126, 21);
            this.dtpStartTime.TabIndex = 111;
            this.dtpStartTime.TabStop = false;
            this.dtpStartTime.Text = "myDatePicker1";
            this.dtpStartTime.Value = null;
            // 
            // ddlDays
            // 
            this.ddlDays.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlDays.Caption = null;
            this.ddlDays.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDays.Location = new System.Drawing.Point(212, 80);
            this.ddlDays.Name = "ddlDays";
            this.ddlDays.Property = null;
            this.ddlDays.ShowDownArrow = true;
            this.ddlDays.Size = new System.Drawing.Size(157, 22);
            this.ddlDays.TabIndex = 110;
            // 
            // ddlShifts
            // 
            this.ddlShifts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlShifts.Caption = null;
            this.ddlShifts.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlShifts.Location = new System.Drawing.Point(212, 42);
            this.ddlShifts.Name = "ddlShifts";
            this.ddlShifts.Property = null;
            this.ddlShifts.ShowDownArrow = true;
            this.ddlShifts.Size = new System.Drawing.Size(157, 22);
            this.ddlShifts.TabIndex = 109;
            this.ddlShifts.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlShifts_SelectedIndexChanged);
            // 
            // btnExit1
            // 
            this.btnExit1.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit1.Location = new System.Drawing.Point(601, 345);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(99, 54);
            this.btnExit1.TabIndex = 108;
            this.btnExit1.Text = "Exit";
            this.btnExit1.Click += new System.EventHandler(this.btnExit1_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSaveClose.Location = new System.Drawing.Point(446, 345);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(103, 54);
            this.btnSaveClose.TabIndex = 107;
            this.btnSaveClose.Text = "Save Close";
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).Text = "Save Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(444, 113);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(143, 23);
            this.radLabel2.TabIndex = 105;
            this.radLabel2.Text = "Selected Drivers";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel2.GetChildAt(0))).Text = "Selected Drivers";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel2.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel2.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(106, 113);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(67, 23);
            this.radLabel1.TabIndex = 104;
            this.radLabel1.Text = "Drivers";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel1.GetChildAt(0))).Text = "Drivers";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel1.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel1.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lstDayWiseDriver
            // 
            this.lstDayWiseDriver.CaseSensitiveSort = true;
            this.lstDayWiseDriver.Location = new System.Drawing.Point(445, 151);
            this.lstDayWiseDriver.Name = "lstDayWiseDriver";
            this.lstDayWiseDriver.Size = new System.Drawing.Size(256, 172);
            this.lstDayWiseDriver.TabIndex = 101;
            this.lstDayWiseDriver.Text = "radListControl1";
            // 
            // lstAllDrivers
            // 
            this.lstAllDrivers.CaseSensitiveSort = true;
            this.lstAllDrivers.Location = new System.Drawing.Point(106, 151);
            this.lstAllDrivers.Name = "lstAllDrivers";
            this.lstAllDrivers.Size = new System.Drawing.Size(263, 172);
            this.lstAllDrivers.TabIndex = 100;
            this.lstAllDrivers.Text = "radListControl1";
            // 
            // btnAddSelectedToAllDriver
            // 
            this.btnAddSelectedToAllDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSelectedToAllDriver.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddSelectedToAllDriver.Location = new System.Drawing.Point(389, 252);
            this.btnAddSelectedToAllDriver.Name = "btnAddSelectedToAllDriver";
            this.btnAddSelectedToAllDriver.Size = new System.Drawing.Size(35, 22);
            this.btnAddSelectedToAllDriver.TabIndex = 99;
            this.btnAddSelectedToAllDriver.Text = "<";
            this.btnAddSelectedToAllDriver.Click += new System.EventHandler(this.btnAddSelectedToAllDriver_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddSelectedToAllDriver.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddSelectedToAllDriver.GetChildAt(0))).Text = "<";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddSelectedToAllDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddSelectedToAllDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnAddSelectedToDayDriverWiseShift
            // 
            this.btnAddSelectedToDayDriverWiseShift.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSelectedToDayDriverWiseShift.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddSelectedToDayDriverWiseShift.Location = new System.Drawing.Point(389, 183);
            this.btnAddSelectedToDayDriverWiseShift.Name = "btnAddSelectedToDayDriverWiseShift";
            this.btnAddSelectedToDayDriverWiseShift.Size = new System.Drawing.Size(35, 22);
            this.btnAddSelectedToDayDriverWiseShift.TabIndex = 98;
            this.btnAddSelectedToDayDriverWiseShift.Text = ">";
            this.btnAddSelectedToDayDriverWiseShift.Click += new System.EventHandler(this.btnAddSelectedToDayDriverWiseShift_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddSelectedToDayDriverWiseShift.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddSelectedToDayDriverWiseShift.GetChildAt(0))).Text = ">";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddSelectedToDayDriverWiseShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddSelectedToDayDriverWiseShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnAddAllToAllDriver
            // 
            this.btnAddAllToAllDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAllToAllDriver.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddAllToAllDriver.Location = new System.Drawing.Point(389, 286);
            this.btnAddAllToAllDriver.Name = "btnAddAllToAllDriver";
            this.btnAddAllToAllDriver.Size = new System.Drawing.Size(35, 22);
            this.btnAddAllToAllDriver.TabIndex = 97;
            this.btnAddAllToAllDriver.Text = "<<";
            this.btnAddAllToAllDriver.Click += new System.EventHandler(this.btnAddAllToAllDriver_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddAllToAllDriver.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddAllToAllDriver.GetChildAt(0))).Text = "<<";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddAllToAllDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddAllToAllDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnAddAllToDayDriverWiseShift
            // 
            this.btnAddAllToDayDriverWiseShift.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddAllToDayDriverWiseShift.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAddAllToDayDriverWiseShift.Location = new System.Drawing.Point(389, 218);
            this.btnAddAllToDayDriverWiseShift.Name = "btnAddAllToDayDriverWiseShift";
            this.btnAddAllToDayDriverWiseShift.Size = new System.Drawing.Size(35, 22);
            this.btnAddAllToDayDriverWiseShift.TabIndex = 96;
            this.btnAddAllToDayDriverWiseShift.Text = ">>";
            this.btnAddAllToDayDriverWiseShift.Click += new System.EventHandler(this.btnAddAllToDayDriverWiseShift_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddAllToDayDriverWiseShift.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddAllToDayDriverWiseShift.GetChildAt(0))).Text = ">>";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddAllToDayDriverWiseShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddAllToDayDriverWiseShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmAddDayWiseDriverShift
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 445);
            this.ControlBox = true;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Add Driver Shift";
            this.KeyPreview = true;
            this.Name = "frmAddDayWiseDriverShift";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Add Driver Shift";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpToTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstDayWiseDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstAllDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddSelectedToAllDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddSelectedToDayDriverWiseShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddAllToAllDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddAllToDayDriverWiseShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadListControl lstDayWiseDriver;
        private Telerik.WinControls.UI.RadListControl lstAllDrivers;
        private Telerik.WinControls.UI.RadButton btnAddSelectedToAllDriver;
        private Telerik.WinControls.UI.RadButton btnAddSelectedToDayDriverWiseShift;
        private Telerik.WinControls.UI.RadButton btnAddAllToAllDriver;
        private Telerik.WinControls.UI.RadButton btnAddAllToDayDriverWiseShift;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton btnSaveClose;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private UI.MyDropDownList ddlDays;
        private UI.MyDropDownList ddlShifts;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private UI.MyDatePicker dtpToTime;
        private UI.MyDatePicker dtpStartTime;
        private Telerik.WinControls.UI.RadLabel radLabel6;
    }
}