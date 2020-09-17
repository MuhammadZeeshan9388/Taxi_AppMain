namespace Taxi_AppMain
{
    partial class frmFareIncrement
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.grdFareIncreament = new Telerik.WinControls.UI.RadGridView();
            this.btnAdd = new Telerik.WinControls.UI.RadButton();
            this.optTimeWise = new Telerik.WinControls.UI.RadRadioButton();
            this.optDateWise = new Telerik.WinControls.UI.RadRadioButton();
            this.optDateTimeWise = new Telerik.WinControls.UI.RadRadioButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlIncrementType = new Telerik.WinControls.UI.RadDropDownList();
            this.dtpTillDate = new UI.MyDatePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtpFromDate = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.chkEnableIncrement = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel17 = new Telerik.WinControls.UI.RadLabel();
            this.numIncrementRate = new Telerik.WinControls.UI.RadSpinEditor();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFareIncreament)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFareIncreament.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optTimeWise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optDateWise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optDateTimeWise)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlIncrementType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableIncrement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrementRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.groupBox1);
            this.radPanel1.Controls.Add(this.grdFareIncreament);
            this.radPanel1.Controls.Add(this.btnExitForm);
            this.radPanel1.Controls.Add(this.btnSave);
            this.radPanel1.Controls.Add(this.chkEnableIncrement);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(597, 519);
            this.radPanel1.TabIndex = 104;
            // 
            // grdFareIncreament
            // 
            this.grdFareIncreament.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdFareIncreament.Location = new System.Drawing.Point(3, 260);
            // 
            // grdFareIncreament
            // 
            this.grdFareIncreament.MasterTemplate.AllowAddNewRow = false;
            this.grdFareIncreament.Name = "grdFareIncreament";
            this.grdFareIncreament.ShowGroupPanel = false;
            this.grdFareIncreament.Size = new System.Drawing.Size(592, 175);
            this.grdFareIncreament.TabIndex = 208;
            this.grdFareIncreament.Text = "radGridView2";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(413, 123);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(107, 41);
            this.btnAdd.TabIndex = 207;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAdd.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.add;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAdd.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAdd.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAdd.GetChildAt(0))).Text = "Add";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAdd.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAdd.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // optTimeWise
            // 
            this.optTimeWise.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTimeWise.Location = new System.Drawing.Point(384, 20);
            this.optTimeWise.Name = "optTimeWise";
            this.optTimeWise.Size = new System.Drawing.Size(168, 18);
            this.optTimeWise.TabIndex = 205;
            this.optTimeWise.Text = "Time Wise";
            this.optTimeWise.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.optTimeWise_ToggleStateChanged);
            // 
            // optDateWise
            // 
            this.optDateWise.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDateWise.Location = new System.Drawing.Point(235, 21);
            this.optDateWise.Name = "optDateWise";
            this.optDateWise.Size = new System.Drawing.Size(143, 18);
            this.optDateWise.TabIndex = 204;
            this.optDateWise.Text = "Date Wise";
            this.optDateWise.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.optDateWise_ToggleStateChanged);
            // 
            // optDateTimeWise
            // 
            this.optDateTimeWise.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDateTimeWise.Location = new System.Drawing.Point(36, 21);
            this.optDateTimeWise.Name = "optDateTimeWise";
            this.optDateTimeWise.Size = new System.Drawing.Size(183, 18);
            this.optDateTimeWise.TabIndex = 203;
            this.optDateTimeWise.Text = "Date && Time Wise";
            this.optDateTimeWise.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.optDateTimeWise.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.radRadioButton1_ToggleStateChanged);
            // 
            // btnExitForm
            // 
            this.btnExitForm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitForm.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitForm.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExitForm.Location = new System.Drawing.Point(461, 457);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(112, 56);
            this.btnExitForm.TabIndex = 202;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExitForm.Click += new System.EventHandler(this.btnExitForm_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(313, 458);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 56);
            this.btnSave.TabIndex = 201;
            this.btnSave.Text = "Save Settings";
            this.btnSave.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save Settings";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.BackColor = System.Drawing.Color.Transparent;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.Black;
            this.radLabel1.Location = new System.Drawing.Point(132, 94);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel1.Size = new System.Drawing.Size(116, 22);
            this.radLabel1.TabIndex = 153;
            this.radLabel1.Text = "Increment Type";
            // 
            // ddlIncrementType
            // 
            this.ddlIncrementType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlIncrementType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem3.Selected = true;
            radListDataItem3.Text = "Percent";
            radListDataItem3.TextWrap = true;
            radListDataItem4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem4.Text = "Amount";
            radListDataItem4.TextWrap = true;
            this.ddlIncrementType.Items.Add(radListDataItem3);
            this.ddlIncrementType.Items.Add(radListDataItem4);
            this.ddlIncrementType.Location = new System.Drawing.Point(263, 94);
            this.ddlIncrementType.Name = "ddlIncrementType";
            this.ddlIncrementType.Size = new System.Drawing.Size(106, 26);
            this.ddlIncrementType.TabIndex = 152;
            this.ddlIncrementType.Text = "Percent";
            // 
            // dtpTillDate
            // 
            this.dtpTillDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpTillDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTillDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTillDate.Location = new System.Drawing.Point(364, 51);
            this.dtpTillDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTillDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Name = "dtpTillDate";
            this.dtpTillDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Size = new System.Drawing.Size(156, 24);
            this.dtpTillDate.TabIndex = 151;
            this.dtpTillDate.TabStop = false;
            this.dtpTillDate.Text = "myDatePicker2";
            this.dtpTillDate.Value = null;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(316, 53);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(27, 22);
            this.radLabel3.TabIndex = 150;
            this.radLabel3.Text = "Till";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate.Location = new System.Drawing.Point(132, 51);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(158, 24);
            this.dtpFromDate.TabIndex = 149;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "myDatePicker1";
            this.dtpFromDate.Value = null;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(58, 53);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(42, 22);
            this.radLabel2.TabIndex = 148;
            this.radLabel2.Text = "From";
            // 
            // chkEnableIncrement
            // 
            this.chkEnableIncrement.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableIncrement.ForeColor = System.Drawing.Color.Blue;
            this.chkEnableIncrement.Location = new System.Drawing.Point(3, 39);
            this.chkEnableIncrement.Name = "chkEnableIncrement";
            // 
            // 
            // 
            this.chkEnableIncrement.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.chkEnableIncrement.Size = new System.Drawing.Size(211, 23);
            this.chkEnableIncrement.TabIndex = 147;
            this.chkEnableIncrement.Text = "Enable Fare Increment";
            // 
            // radLabel17
            // 
            this.radLabel17.BackColor = System.Drawing.Color.Transparent;
            this.radLabel17.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel17.ForeColor = System.Drawing.Color.Black;
            this.radLabel17.Location = new System.Drawing.Point(132, 138);
            this.radLabel17.Name = "radLabel17";
            // 
            // 
            // 
            this.radLabel17.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel17.Size = new System.Drawing.Size(113, 22);
            this.radLabel17.TabIndex = 146;
            this.radLabel17.Text = "Increment Rate";
            // 
            // numIncrementRate
            // 
            this.numIncrementRate.EnableKeyMap = true;
            this.numIncrementRate.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.numIncrementRate.ForeColor = System.Drawing.Color.Red;
            this.numIncrementRate.InterceptArrowKeys = false;
            this.numIncrementRate.Location = new System.Drawing.Point(264, 137);
            this.numIncrementRate.Name = "numIncrementRate";
            // 
            // 
            // 
            this.numIncrementRate.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numIncrementRate.RootElement.ForeColor = System.Drawing.Color.Red;
            this.numIncrementRate.ShowBorder = true;
            this.numIncrementRate.ShowUpDownButtons = false;
            this.numIncrementRate.Size = new System.Drawing.Size(72, 24);
            this.numIncrementRate.TabIndex = 145;
            this.numIncrementRate.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numIncrementRate.GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadSpinElement)(this.numIncrementRate.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numIncrementRate.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numIncrementRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numIncrementRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel6
            // 
            this.radLabel6.AutoSize = false;
            this.radLabel6.BackColor = System.Drawing.Color.SteelBlue;
            this.radLabel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel6.ForeColor = System.Drawing.Color.White;
            this.radLabel6.Location = new System.Drawing.Point(0, 38);
            this.radLabel6.Name = "radLabel6";
            // 
            // 
            // 
            this.radLabel6.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel6.Size = new System.Drawing.Size(597, 37);
            this.radLabel6.TabIndex = 105;
            this.radLabel6.Text = "Fare Increment Settings";
            this.radLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optDateTimeWise);
            this.groupBox1.Controls.Add(this.numIncrementRate);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.radLabel17);
            this.groupBox1.Controls.Add(this.optTimeWise);
            this.groupBox1.Controls.Add(this.radLabel2);
            this.groupBox1.Controls.Add(this.optDateWise);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.radLabel3);
            this.groupBox1.Controls.Add(this.dtpTillDate);
            this.groupBox1.Controls.Add(this.ddlIncrementType);
            this.groupBox1.Controls.Add(this.radLabel1);
            this.groupBox1.Location = new System.Drawing.Point(10, 79);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(566, 178);
            this.groupBox1.TabIndex = 209;
            this.groupBox1.TabStop = false;
            // 
            // frmFareIncrement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 557);
            this.Controls.Add(this.radLabel6);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmFareIncrement";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Fare Increment";
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.radLabel6, 0);
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
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFareIncreament.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFareIncreament)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optTimeWise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optDateWise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optDateTimeWise)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlIncrementType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableIncrement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrementRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel17;
        private Telerik.WinControls.UI.RadSpinEditor numIncrementRate;
        private Telerik.WinControls.UI.RadCheckBox chkEnableIncrement;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList ddlIncrementType;
        private UI.MyDatePicker dtpTillDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private UI.MyDatePicker dtpFromDate;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadRadioButton optTimeWise;
        private Telerik.WinControls.UI.RadRadioButton optDateWise;
        private Telerik.WinControls.UI.RadRadioButton optDateTimeWise;
		private Telerik.WinControls.UI.RadButton btnAdd;
		private Telerik.WinControls.UI.RadGridView grdFareIncreament;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}