namespace Taxi_AppMain
{
    partial class frmAddAllDriverRentExpenses 
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
            this.grdDriverRent = new UI.MyGridView();
            this.cbAllCompany = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ddlSubCompany = new Telerik.WinControls.UI.RadComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.btnViewPrint = new Telerik.WinControls.UI.RadButton();
            this.btnPrintAll = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnSendEmail = new Telerik.WinControls.UI.RadButton();
            this.btnExits = new Telerik.WinControls.UI.RadButton();
            this.btnGenerate = new Telerik.WinControls.UI.RadButton();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.btnDisplayRent = new Telerik.WinControls.UI.RadButton();
            this.lblTillDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpTillDate = new UI.MyDatePicker();
            this.dtpFromDate = new UI.MyDatePicker();
            this.btnDeleteGenerated = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverRent.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSubCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDisplayRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteGenerated)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(686, 500);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(814, 516);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(561, 489);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(439, 516);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(598, 410);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdDriverRent);
            this.panel1.Controls.Add(this.cbAllCompany);
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 563);
            this.panel1.TabIndex = 0;
            // 
            // grdDriverRent
            // 
            this.grdDriverRent.AutoCellFormatting = false;
            this.grdDriverRent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDriverRent.EnableCheckInCheckOut = false;
            this.grdDriverRent.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDriverRent.HeaderRowBorderColor = System.Drawing.Color.MidnightBlue;
            this.grdDriverRent.Location = new System.Drawing.Point(0, 20);
            // 
            // grdDriverRent
            // 
            this.grdDriverRent.MasterTemplate.AllowAddNewRow = false;
            this.grdDriverRent.Name = "grdDriverRent";
            this.grdDriverRent.PKFieldColumnName = "";
            this.grdDriverRent.ShowGroupPanel = false;
            this.grdDriverRent.ShowImageOnActionButton = true;
            this.grdDriverRent.Size = new System.Drawing.Size(798, 543);
            this.grdDriverRent.TabIndex = 115;
            this.grdDriverRent.Text = "myGridView1";
            this.grdDriverRent.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdCompany_CellDoubleClick);
            // 
            // cbAllCompany
            // 
            this.cbAllCompany.AutoSize = true;
            this.cbAllCompany.Checked = true;
            this.cbAllCompany.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllCompany.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbAllCompany.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAllCompany.Location = new System.Drawing.Point(0, 0);
            this.cbAllCompany.Name = "cbAllCompany";
            this.cbAllCompany.Size = new System.Drawing.Size(798, 20);
            this.cbAllCompany.TabIndex = 0;
            this.cbAllCompany.Text = "Select All";
            this.cbAllCompany.UseVisualStyleBackColor = true;
            this.cbAllCompany.CheckedChanged += new System.EventHandler(this.cbAllCompany_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.ddlSubCompany);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.radGroupBox2);
            this.panel2.Controls.Add(this.radGroupBox1);
            this.panel2.Location = new System.Drawing.Point(803, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(365, 560);
            this.panel2.TabIndex = 106;
            // 
            // ddlSubCompany
            // 
            this.ddlSubCompany.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlSubCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSubCompany.Location = new System.Drawing.Point(155, 247);
            this.ddlSubCompany.Name = "ddlSubCompany";
            // 
            // 
            // 
            this.ddlSubCompany.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlSubCompany.Size = new System.Drawing.Size(198, 24);
            this.ddlSubCompany.TabIndex = 109;
            this.ddlSubCompany.TabStop = false;
            this.ddlSubCompany.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 18);
            this.label6.TabIndex = 108;
            this.label6.Text = "Company Header";
            this.label6.Visible = false;
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.Controls.Add(this.btnDeleteGenerated);
            this.radGroupBox2.Controls.Add(this.btnViewPrint);
            this.radGroupBox2.Controls.Add(this.btnPrintAll);
            this.radGroupBox2.Controls.Add(this.label1);
            this.radGroupBox2.Controls.Add(this.txtSubject);
            this.radGroupBox2.Controls.Add(this.btnSendEmail);
            this.radGroupBox2.Controls.Add(this.btnExits);
            this.radGroupBox2.Controls.Add(this.btnGenerate);
            this.radGroupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGroupBox2.FooterImageIndex = -1;
            this.radGroupBox2.FooterImageKey = "";
            this.radGroupBox2.ForeColor = System.Drawing.Color.Black;
            this.radGroupBox2.HeaderImageIndex = -1;
            this.radGroupBox2.HeaderImageKey = "";
            this.radGroupBox2.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox2.HeaderText = "Info";
            this.radGroupBox2.Location = new System.Drawing.Point(12, 274);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox2.Size = new System.Drawing.Size(347, 283);
            this.radGroupBox2.TabIndex = 107;
            this.radGroupBox2.Text = "Info";
            // 
            // btnViewPrint
            // 
            this.btnViewPrint.Enabled = false;
            this.btnViewPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewPrint.Image = global::Taxi_AppMain.Properties.Resources.Print1;
            this.btnViewPrint.Location = new System.Drawing.Point(16, 168);
            this.btnViewPrint.Name = "btnViewPrint";
            this.btnViewPrint.Size = new System.Drawing.Size(149, 46);
            this.btnViewPrint.TabIndex = 118;
            this.btnViewPrint.Text = "View Print";
            this.btnViewPrint.Click += new System.EventHandler(this.btnViewPrint_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewPrint.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Print1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewPrint.GetChildAt(0))).Text = "View Print";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Enabled = false;
            this.btnPrintAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAll.Location = new System.Drawing.Point(216, 276);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(118, 46);
            this.btnPrintAll.TabIndex = 115;
            this.btnPrintAll.Text = "Print All";
            this.btnPrintAll.Visible = false;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrintAll.GetChildAt(0))).Text = "Print All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 114;
            this.label1.Text = "Email Subject";
            this.label1.Visible = false;
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(112, 104);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(221, 26);
            this.txtSubject.TabIndex = 113;
            this.txtSubject.Visible = false;
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Location = new System.Drawing.Point(45, 276);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(118, 46);
            this.btnSendEmail.TabIndex = 110;
            this.btnSendEmail.Text = "Send Email ";
            this.btnSendEmail.Visible = false;
            this.btnSendEmail.Click += new System.EventHandler(this.btnEmailInvoices_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).Text = "Send Email ";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExits
            // 
            this.btnExits.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExits.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExits.Location = new System.Drawing.Point(227, 168);
            this.btnExits.Name = "btnExits";
            this.btnExits.Size = new System.Drawing.Size(107, 46);
            this.btnExits.TabIndex = 106;
            this.btnExits.Text = "Exit";
            this.btnExits.Click += new System.EventHandler(this.btnExits_Click_1);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExits.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExits.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExits.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExits.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(29, 23);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(134, 63);
            this.btnGenerate.TabIndex = 105;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnGenerate.GetChildAt(0))).Text = "Generate";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnGenerate.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnGenerate.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.btnClear);
            this.radGroupBox1.Controls.Add(this.btnDisplayRent);
            this.radGroupBox1.Controls.Add(this.lblTillDate);
            this.radGroupBox1.Controls.Add(this.lblFromDate);
            this.radGroupBox1.Controls.Add(this.dtpTillDate);
            this.radGroupBox1.Controls.Add(this.dtpFromDate);
            this.radGroupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.ForeColor = System.Drawing.Color.Black;
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Period";
            this.radGroupBox1.Location = new System.Drawing.Point(12, 39);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(347, 199);
            this.radGroupBox1.TabIndex = 106;
            this.radGroupBox1.Text = "Period";
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(199, 131);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(134, 46);
            this.btnClear.TabIndex = 107;
            this.btnClear.Text = "Clear/Reset";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClear.GetChildAt(0))).Text = "Clear/Reset";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClear.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClear.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDisplayRent
            // 
            this.btnDisplayRent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplayRent.Location = new System.Drawing.Point(29, 131);
            this.btnDisplayRent.Name = "btnDisplayRent";
            this.btnDisplayRent.Size = new System.Drawing.Size(134, 46);
            this.btnDisplayRent.TabIndex = 106;
            this.btnDisplayRent.Text = "Display Rent";
            this.btnDisplayRent.Click += new System.EventHandler(this.btnDisplayRent_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDisplayRent.GetChildAt(0))).Text = "Display Rent";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDisplayRent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDisplayRent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblTillDate
            // 
            this.lblTillDate.AutoSize = true;
            this.lblTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTillDate.ForeColor = System.Drawing.Color.Black;
            this.lblTillDate.Location = new System.Drawing.Point(44, 83);
            this.lblTillDate.Name = "lblTillDate";
            this.lblTillDate.Size = new System.Drawing.Size(71, 18);
            this.lblTillDate.TabIndex = 87;
            this.lblTillDate.Text = "Till Date";
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDate.ForeColor = System.Drawing.Color.Black;
            this.lblFromDate.Location = new System.Drawing.Point(44, 46);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(85, 18);
            this.lblFromDate.TabIndex = 86;
            this.lblFromDate.Text = "From Date";
            // 
            // dtpTillDate
            // 
            this.dtpTillDate.Culture = new System.Globalization.CultureInfo("en-AU");
            this.dtpTillDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTillDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTillDate.Location = new System.Drawing.Point(158, 83);
            this.dtpTillDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTillDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Name = "dtpTillDate";
            this.dtpTillDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Size = new System.Drawing.Size(140, 24);
            this.dtpTillDate.TabIndex = 85;
            this.dtpTillDate.TabStop = false;
            this.dtpTillDate.Text = "radDateTimePicker1";
            this.dtpTillDate.Value = null;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Culture = new System.Globalization.CultureInfo("en-AU");
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate.Location = new System.Drawing.Point(158, 39);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(140, 24);
            this.dtpFromDate.TabIndex = 84;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "radDateTimePicker1";
            this.dtpFromDate.Value = null;
            // 
            // btnDeleteGenerated
            // 
            this.btnDeleteGenerated.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteGenerated.Image = global::Taxi_AppMain.Properties.Resources.delete;
            this.btnDeleteGenerated.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnDeleteGenerated.Location = new System.Drawing.Point(231, 20);
            this.btnDeleteGenerated.Name = "btnDeleteGenerated";
            this.btnDeleteGenerated.Size = new System.Drawing.Size(110, 78);
            this.btnDeleteGenerated.TabIndex = 119;
            this.btnDeleteGenerated.Text = "Delete Generated Rent";
            this.btnDeleteGenerated.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeleteGenerated.TextWrap = true;
            this.btnDeleteGenerated.Visible = false;
            this.btnDeleteGenerated.Click += new System.EventHandler(this.btnDeleteGenerated_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteGenerated.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.delete;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteGenerated.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteGenerated.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteGenerated.GetChildAt(0))).Text = "Delete Generated Rent";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteGenerated.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteGenerated.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteGenerated.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmAddAllDriverRentExpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 599);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormTitle = "Add All Driver Rent Expenses";
            this.KeyPreview = true;
            this.Name = "frmAddAllDriverRentExpenses";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Add All Driver Rent Expenses";
            this.Load += new System.EventHandler(this.frmCompanyInvoice_Load);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverRent.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverRent)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSubCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDisplayRent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteGenerated)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadButton btnGenerate;
        private System.Windows.Forms.CheckBox cbAllCompany;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private UI.MyDatePicker dtpFromDate;
        private UI.MyDatePicker dtpTillDate;
        private System.Windows.Forms.Label lblTillDate;
        private System.Windows.Forms.Label lblFromDate;
        private Telerik.WinControls.UI.RadButton btnExits;
        private UI.MyGridView grdDriverRent;
        private Telerik.WinControls.UI.RadButton btnSendEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private Telerik.WinControls.UI.RadButton btnDisplayRent;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnPrintAll;
        private Telerik.WinControls.UI.RadComboBox ddlSubCompany;
        private System.Windows.Forms.Label label6;
        private Telerik.WinControls.UI.RadButton btnViewPrint;
        private Telerik.WinControls.UI.RadButton btnDeleteGenerated;
    }
}