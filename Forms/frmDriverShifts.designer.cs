namespace Taxi_AppMain
{
    partial class frmDriverShifts
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
            this.grdDriverShift = new UI.MyGridView();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDayWise = new System.Windows.Forms.CheckBox();
            this.dtpLogOnWindows = new UI.MyDatePicker();
            this.dtpTillTime = new UI.MyDatePicker();
            this.dtpFromTime = new UI.MyDatePicker();
            this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
            this.btnNew = new Telerik.WinControls.UI.RadButton();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel17 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.txtShiftName = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlStartDay = new UI.MyDropDownList();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.ddlEndDay = new UI.MyDropDownList();
            this.lblDriverShift = new Telerik.WinControls.UI.RadLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShift.MasterTemplate)).BeginInit();
            this.grdDriverShift.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpLogOnWindows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStartDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlEndDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDriverShift)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdDriverShift);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.lblDriverShift);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(615, 471);
            this.panel1.TabIndex = 106;
            // 
            // grdDriverShift
            // 
            this.grdDriverShift.AutoCellFormatting = false;
            this.grdDriverShift.Controls.Add(this.chkAll);
            this.grdDriverShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDriverShift.EnableCheckInCheckOut = false;
            this.grdDriverShift.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDriverShift.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdDriverShift.Location = new System.Drawing.Point(0, 290);
            this.grdDriverShift.Name = "grdDriverShift";
            this.grdDriverShift.PKFieldColumnName = "";
            this.grdDriverShift.ShowImageOnActionButton = true;
            this.grdDriverShift.Size = new System.Drawing.Size(615, 181);
            this.grdDriverShift.TabIndex = 119;
            this.grdDriverShift.Text = "myGridView1";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(21, 6);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(41, 20);
            this.chkAll.TabIndex = 248;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.dtpLogOnWindows);
            this.groupBox1.Controls.Add(this.dtpTillTime);
            this.groupBox1.Controls.Add(this.dtpFromTime);
            this.groupBox1.Controls.Add(this.radLabel8);
            this.groupBox1.Controls.Add(this.btnNew);
            this.groupBox1.Controls.Add(this.radLabel5);
            this.groupBox1.Controls.Add(this.radLabel17);
            this.groupBox1.Controls.Add(this.radLabel3);
            this.groupBox1.Controls.Add(this.btnExit1);
            this.groupBox1.Controls.Add(this.radLabel1);
            this.groupBox1.Controls.Add(this.ddlStartDay);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.radLabel6);
            this.groupBox1.Controls.Add(this.ddlEndDay);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(615, 290);
            this.groupBox1.TabIndex = 247;
            this.groupBox1.TabStop = false;
            // 
            // chkDayWise
            // 
            this.chkDayWise.AutoSize = true;
            this.chkDayWise.Checked = true;
            this.chkDayWise.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDayWise.Location = new System.Drawing.Point(371, 21);
            this.chkDayWise.Name = "chkDayWise";
            this.chkDayWise.Size = new System.Drawing.Size(80, 20);
            this.chkDayWise.TabIndex = 298;
            this.chkDayWise.Text = "Day Wise";
            this.chkDayWise.UseVisualStyleBackColor = true;
            this.chkDayWise.Visible = false;
            // 
            // dtpLogOnWindows
            // 
            this.dtpLogOnWindows.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpLogOnWindows.CustomFormat = "HH:mm";
            this.dtpLogOnWindows.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpLogOnWindows.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpLogOnWindows.Location = new System.Drawing.Point(111, 119);
            this.dtpLogOnWindows.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpLogOnWindows.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpLogOnWindows.Name = "dtpLogOnWindows";
            this.dtpLogOnWindows.NullDate = new System.DateTime(((long)(0)));
            this.dtpLogOnWindows.ShowUpDown = true;
            this.dtpLogOnWindows.Size = new System.Drawing.Size(106, 24);
            this.dtpLogOnWindows.TabIndex = 293;
            this.dtpLogOnWindows.TabStop = false;
            this.dtpLogOnWindows.Text = "myDatePicker1";
            this.dtpLogOnWindows.Value = new System.DateTime(2014, 10, 21, 0, 0, 0, 0);
            // 
            // dtpTillTime
            // 
            this.dtpTillTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpTillTime.CustomFormat = "HH:mm";
            this.dtpTillTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTillTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTillTime.Location = new System.Drawing.Point(220, 153);
            this.dtpTillTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTillTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillTime.Name = "dtpTillTime";
            this.dtpTillTime.NullDate = new System.DateTime(((long)(0)));
            this.dtpTillTime.ShowUpDown = true;
            this.dtpTillTime.Size = new System.Drawing.Size(98, 24);
            this.dtpTillTime.TabIndex = 292;
            this.dtpTillTime.TabStop = false;
            this.dtpTillTime.Text = "myDatePicker1";
            this.dtpTillTime.Value = new System.DateTime(2014, 10, 21, 0, 0, 0, 0);
            // 
            // dtpFromTime
            // 
            this.dtpFromTime.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFromTime.CustomFormat = "HH:mm";
            this.dtpFromTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromTime.Location = new System.Drawing.Point(221, 84);
            this.dtpFromTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromTime.Name = "dtpFromTime";
            this.dtpFromTime.NullDate = new System.DateTime(((long)(0)));
            this.dtpFromTime.ShowUpDown = true;
            this.dtpFromTime.Size = new System.Drawing.Size(97, 24);
            this.dtpFromTime.TabIndex = 291;
            this.dtpFromTime.TabStop = false;
            this.dtpFromTime.Text = "myDatePicker1";
            this.dtpFromTime.Value = new System.DateTime(2014, 10, 21, 0, 0, 0, 0);
            // 
            // radLabel8
            // 
            this.radLabel8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel8.Location = new System.Drawing.Point(324, 156);
            this.radLabel8.Name = "radLabel8";
            this.radLabel8.Size = new System.Drawing.Size(58, 19);
            this.radLabel8.TabIndex = 288;
            this.radLabel8.Text = "(Hr/min)";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel8.GetChildAt(0))).Text = "(Hr/min)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel8.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel8.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnNew
            // 
            this.btnNew.Image = global::Taxi_AppMain.Properties.Resources.AddBig;
            this.btnNew.Location = new System.Drawing.Point(74, 226);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(119, 57);
            this.btnNew.TabIndex = 262;
            this.btnNew.Text = "New";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNew.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.AddBig;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNew.GetChildAt(0))).Text = "New";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNew.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNew.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel5
            // 
            this.radLabel5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(221, 122);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(101, 19);
            this.radLabel5.TabIndex = 272;
            this.radLabel5.Text = "after shift starts";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel5.GetChildAt(0))).Text = "after shift starts";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel5.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel5.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel17
            // 
            this.radLabel17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel17.Location = new System.Drawing.Point(324, 86);
            this.radLabel17.Name = "radLabel17";
            this.radLabel17.Size = new System.Drawing.Size(58, 19);
            this.radLabel17.TabIndex = 280;
            this.radLabel17.Text = "(Hr/min)";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel17.GetChildAt(0))).Text = "(Hr/min)";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel17.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel17.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(21, 122);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(75, 19);
            this.radLabel3.TabIndex = 265;
            this.radLabel3.Text = "Login Up to";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel3.GetChildAt(0))).Text = "Login Up to";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel3.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel3.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExit1
            // 
            this.btnExit1.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit1.Location = new System.Drawing.Point(389, 226);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(119, 57);
            this.btnExit1.TabIndex = 263;
            this.btnExit1.Text = "Exit";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtShiftName
            // 
            this.txtShiftName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtShiftName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtShiftName.Location = new System.Drawing.Point(124, 20);
            this.txtShiftName.MaxLength = 100;
            this.txtShiftName.Name = "txtShiftName";
            this.txtShiftName.Size = new System.Drawing.Size(240, 24);
            this.txtShiftName.TabIndex = 248;
            this.txtShiftName.TabStop = false;
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtShiftName.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.UI.RadTextBoxElement)(this.txtShiftName.GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtShiftName.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.radLabel2.ForeColor = System.Drawing.Color.Blue;
            this.radLabel2.Location = new System.Drawing.Point(28, 21);
            this.radLabel2.Name = "radLabel2";
            // 
            // 
            // 
            this.radLabel2.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.radLabel2.Size = new System.Drawing.Size(92, 22);
            this.radLabel2.TabIndex = 261;
            this.radLabel2.Text = "Shift Name";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel2.GetChildAt(0))).Text = "Shift Name";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel2.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel2.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(21, 87);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(76, 19);
            this.radLabel1.TabIndex = 255;
            this.radLabel1.Text = "Shift Start";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel1.GetChildAt(0))).Text = "Shift Start";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel1.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel1.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlStartDay
            // 
            this.ddlStartDay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ddlStartDay.Caption = null;
            this.ddlStartDay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStartDay.Location = new System.Drawing.Point(112, 84);
            this.ddlStartDay.Name = "ddlStartDay";
            this.ddlStartDay.Property = null;
            this.ddlStartDay.ShowDownArrow = true;
            this.ddlStartDay.Size = new System.Drawing.Size(105, 23);
            this.ddlStartDay.TabIndex = 254;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSave.Location = new System.Drawing.Point(233, 226);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(119, 57);
            this.btnSave.TabIndex = 242;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.BtnSaveShift_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel6
            // 
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel6.Location = new System.Drawing.Point(21, 156);
            this.radLabel6.Name = "radLabel6";
            this.radLabel6.Size = new System.Drawing.Size(68, 19);
            this.radLabel6.TabIndex = 253;
            this.radLabel6.Text = "Shift End";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel6.GetChildAt(0))).Text = "Shift End";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel6.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel6.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlEndDay
            // 
            this.ddlEndDay.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.ddlEndDay.Caption = null;
            this.ddlEndDay.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlEndDay.Location = new System.Drawing.Point(112, 154);
            this.ddlEndDay.Name = "ddlEndDay";
            this.ddlEndDay.Property = null;
            this.ddlEndDay.ShowDownArrow = true;
            this.ddlEndDay.Size = new System.Drawing.Size(105, 23);
            this.ddlEndDay.TabIndex = 247;
            // 
            // lblDriverShift
            // 
            this.lblDriverShift.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriverShift.Location = new System.Drawing.Point(299, 30);
            this.lblDriverShift.Name = "lblDriverShift";
            this.lblDriverShift.Size = new System.Drawing.Size(2, 2);
            this.lblDriverShift.TabIndex = 246;
            ((Telerik.WinControls.UI.RadLabelElement)(this.lblDriverShift.GetChildAt(0))).Text = "";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.lblDriverShift.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.lblDriverShift.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox2.Controls.Add(this.radLabel2);
            this.groupBox2.Controls.Add(this.chkDayWise);
            this.groupBox2.Controls.Add(this.txtShiftName);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox2.Location = new System.Drawing.Point(-11, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(635, 56);
            this.groupBox2.TabIndex = 299;
            this.groupBox2.TabStop = false;
            // 
            // frmDriverShifts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 509);
            this.ControlBox = true;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Shifts";
            this.KeyPreview = true;
            this.Name = "frmDriverShifts";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Shifts";
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
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShift.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShift)).EndInit();
            this.grdDriverShift.ResumeLayout(false);
            this.grdDriverShift.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpLogOnWindows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShiftName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlStartDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlEndDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDriverShift)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UI.MyGridView grdDriverShift;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadLabel lblDriverShift;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private UI.MyDropDownList ddlStartDay;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private UI.MyDropDownList ddlEndDay;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTextBox txtShiftName;
        private System.Windows.Forms.CheckBox chkAll;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnNew;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel5;
		private Telerik.WinControls.UI.RadLabel radLabel17;
		private Telerik.WinControls.UI.RadLabel radLabel8;
		private UI.MyDatePicker dtpLogOnWindows;
		private UI.MyDatePicker dtpTillTime;
		private UI.MyDatePicker dtpFromTime;
        private System.Windows.Forms.CheckBox chkDayWise;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}