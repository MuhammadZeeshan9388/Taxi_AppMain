namespace Taxi_AppMain
{
    partial class frmAddMultipleCompanyInvoice 
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
            this.chkAllGroup = new Telerik.WinControls.UI.RadCheckBox();
            this.grdCompany = new UI.MyGridView();
            this.ddlCompanyGroup = new UI.MyDropDownList();
            this.cbAllCompany = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.btnViewPrint = new Telerik.WinControls.UI.RadButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnEmailInvoices = new Telerik.WinControls.UI.RadButton();
            this.ddlSubCompany = new Telerik.WinControls.UI.RadComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnExits = new Telerik.WinControls.UI.RadButton();
            this.lblInvoiceDate = new Telerik.WinControls.UI.RadLabel();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.btnGenerate = new Telerik.WinControls.UI.RadButton();
            this.dtpInvoiceDate = new UI.MyDatePicker();
            this.dtpDueDate = new UI.MyDatePicker();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.lblCountCompany = new System.Windows.Forms.Label();
            this.btnGetBooking = new Telerik.WinControls.UI.RadButton();
            this.lblTillDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpTillDate = new UI.MyDatePicker();
            this.dtpFromDate = new UI.MyDatePicker();
            this.rbtnCustom = new System.Windows.Forms.RadioButton();
            this.rbtnAll = new System.Windows.Forms.RadioButton();
            this.rbtnMonthly = new System.Windows.Forms.RadioButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompany.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompanyGroup)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSubCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblInvoiceDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpInvoiceDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDueDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGetBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
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
            this.panel1.Controls.Add(this.chkAllGroup);
            this.panel1.Controls.Add(this.grdCompany);
            this.panel1.Controls.Add(this.ddlCompanyGroup);
            this.panel1.Controls.Add(this.cbAllCompany);
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 563);
            this.panel1.TabIndex = 0;
            // 
            // chkAllGroup
            // 
            this.chkAllGroup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllGroup.Location = new System.Drawing.Point(125, 1);
            this.chkAllGroup.Name = "chkAllGroup";
            this.chkAllGroup.Size = new System.Drawing.Size(92, 20);
            this.chkAllGroup.TabIndex = 278;
            this.chkAllGroup.Text = "All Groups";
            this.chkAllGroup.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.chkAllGroup.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkAllGroup_ToggleStateChanged);
            // 
            // grdCompany
            // 
            this.grdCompany.AutoCellFormatting = false;
            this.grdCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCompany.EnableCheckInCheckOut = false;
            this.grdCompany.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdCompany.HeaderRowBorderColor = System.Drawing.Color.MidnightBlue;
            this.grdCompany.Location = new System.Drawing.Point(0, 22);
            // 
            // grdCompany
            // 
            this.grdCompany.MasterTemplate.AllowAddNewRow = false;
            this.grdCompany.Name = "grdCompany";
            this.grdCompany.PKFieldColumnName = "";
            this.grdCompany.ShowGroupPanel = false;
            this.grdCompany.ShowImageOnActionButton = true;
            this.grdCompany.Size = new System.Drawing.Size(347, 541);
            this.grdCompany.TabIndex = 115;
            this.grdCompany.Text = "myGridView1";
            this.grdCompany.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdCompany_CellDoubleClick);
            // 
            // ddlCompanyGroup
            // 
            this.ddlCompanyGroup.Caption = null;
            this.ddlCompanyGroup.Enabled = false;
            this.ddlCompanyGroup.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompanyGroup.ForeColor = System.Drawing.Color.Black;
            this.ddlCompanyGroup.Location = new System.Drawing.Point(218, 0);
            this.ddlCompanyGroup.Name = "ddlCompanyGroup";
            this.ddlCompanyGroup.Property = null;
            // 
            // 
            // 
            this.ddlCompanyGroup.RootElement.ForeColor = System.Drawing.Color.Black;
            this.ddlCompanyGroup.ShowDownArrow = true;
            this.ddlCompanyGroup.Size = new System.Drawing.Size(129, 22);
            this.ddlCompanyGroup.TabIndex = 279;
            this.ddlCompanyGroup.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlCompanyGroup_SelectedIndexChanged);
            // 
            // cbAllCompany
            // 
            this.cbAllCompany.AutoSize = true;
            this.cbAllCompany.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbAllCompany.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAllCompany.Location = new System.Drawing.Point(0, 0);
            this.cbAllCompany.Name = "cbAllCompany";
            this.cbAllCompany.Size = new System.Drawing.Size(347, 22);
            this.cbAllCompany.TabIndex = 0;
            this.cbAllCompany.Text = "Select All";
            this.cbAllCompany.UseVisualStyleBackColor = true;
            this.cbAllCompany.CheckedChanged += new System.EventHandler(this.cbAllCompany_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.radGroupBox2);
            this.panel2.Controls.Add(this.radGroupBox1);
            this.panel2.Location = new System.Drawing.Point(347, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(401, 560);
            this.panel2.TabIndex = 106;
            // 
            // radGroupBox2
            // 
            this.radGroupBox2.Controls.Add(this.btnViewPrint);
            this.radGroupBox2.Controls.Add(this.label2);
            this.radGroupBox2.Controls.Add(this.txtNotes);
            this.radGroupBox2.Controls.Add(this.btnExport);
            this.radGroupBox2.Controls.Add(this.label1);
            this.radGroupBox2.Controls.Add(this.txtSubject);
            this.radGroupBox2.Controls.Add(this.btnEmailInvoices);
            this.radGroupBox2.Controls.Add(this.ddlSubCompany);
            this.radGroupBox2.Controls.Add(this.label6);
            this.radGroupBox2.Controls.Add(this.btnExits);
            this.radGroupBox2.Controls.Add(this.lblInvoiceDate);
            this.radGroupBox2.Controls.Add(this.lblDueDate);
            this.radGroupBox2.Controls.Add(this.btnGenerate);
            this.radGroupBox2.Controls.Add(this.dtpInvoiceDate);
            this.radGroupBox2.Controls.Add(this.dtpDueDate);
            this.radGroupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGroupBox2.FooterImageIndex = -1;
            this.radGroupBox2.FooterImageKey = "";
            this.radGroupBox2.ForeColor = System.Drawing.Color.Black;
            this.radGroupBox2.HeaderImageIndex = -1;
            this.radGroupBox2.HeaderImageKey = "";
            this.radGroupBox2.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox2.HeaderText = "Info";
            this.radGroupBox2.Location = new System.Drawing.Point(12, 189);
            this.radGroupBox2.Name = "radGroupBox2";
            this.radGroupBox2.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox2.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radGroupBox2.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox2.Size = new System.Drawing.Size(382, 360);
            this.radGroupBox2.TabIndex = 107;
            this.radGroupBox2.Text = "Info";
            // 
            // btnViewPrint
            // 
            this.btnViewPrint.Enabled = false;
            this.btnViewPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewPrint.Image = global::Taxi_AppMain.Properties.Resources.Print1;
            this.btnViewPrint.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnViewPrint.Location = new System.Drawing.Point(165, 269);
            this.btnViewPrint.Name = "btnViewPrint";
            this.btnViewPrint.Size = new System.Drawing.Size(90, 49);
            this.btnViewPrint.TabIndex = 117;
            this.btnViewPrint.Text = "View Print";
            this.btnViewPrint.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnViewPrint.Click += new System.EventHandler(this.btnViewPrint_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewPrint.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Print1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewPrint.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewPrint.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewPrint.GetChildAt(0))).Text = "View Print";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 152);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 115;
            this.label2.Text = "Notes";
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotes.Location = new System.Drawing.Point(132, 148);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(238, 77);
            this.txtNotes.TabIndex = 114;
            // 
            // btnExport
            // 
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(27, 300);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(118, 46);
            this.btnExport.TabIndex = 113;
            this.btnExport.Text = "Export Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 112;
            this.label1.Text = "Email Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(131, 118);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(238, 26);
            this.txtSubject.TabIndex = 111;
            // 
            // btnEmailInvoices
            // 
            this.btnEmailInvoices.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmailInvoices.Location = new System.Drawing.Point(265, 241);
            this.btnEmailInvoices.Name = "btnEmailInvoices";
            this.btnEmailInvoices.Size = new System.Drawing.Size(118, 46);
            this.btnEmailInvoices.TabIndex = 110;
            this.btnEmailInvoices.Text = "Email Invoices";
            this.btnEmailInvoices.Click += new System.EventHandler(this.btnEmailInvoices_Click);
            // 
            // ddlSubCompany
            // 
            this.ddlSubCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSubCompany.Location = new System.Drawing.Point(131, 87);
            this.ddlSubCompany.Name = "ddlSubCompany";
            // 
            // 
            // 
            this.ddlSubCompany.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlSubCompany.Size = new System.Drawing.Size(240, 24);
            this.ddlSubCompany.TabIndex = 109;
            this.ddlSubCompany.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(19, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 18);
            this.label6.TabIndex = 108;
            this.label6.Text = "SubCompany";
            // 
            // btnExits
            // 
            this.btnExits.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExits.Location = new System.Drawing.Point(270, 300);
            this.btnExits.Name = "btnExits";
            this.btnExits.Size = new System.Drawing.Size(107, 46);
            this.btnExits.TabIndex = 106;
            this.btnExits.Text = "Exit";
            this.btnExits.Click += new System.EventHandler(this.btnExits_Click_1);
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceDate.ForeColor = System.Drawing.Color.Black;
            this.lblInvoiceDate.Location = new System.Drawing.Point(19, 24);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            // 
            // 
            // 
            this.lblInvoiceDate.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblInvoiceDate.Size = new System.Drawing.Size(104, 22);
            this.lblInvoiceDate.TabIndex = 82;
            this.lblInvoiceDate.Text = "Invoice Date";
            // 
            // lblDueDate
            // 
            this.lblDueDate.AutoSize = true;
            this.lblDueDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblDueDate.ForeColor = System.Drawing.Color.Black;
            this.lblDueDate.Location = new System.Drawing.Point(20, 58);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(76, 18);
            this.lblDueDate.TabIndex = 103;
            this.lblDueDate.Text = "Due Date";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(28, 241);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(118, 46);
            this.btnGenerate.TabIndex = 105;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dtpInvoiceDate
            // 
            this.dtpInvoiceDate.Culture = new System.Globalization.CultureInfo("en-AU");
            this.dtpInvoiceDate.CustomFormat = "dd/MM/yyyy";
            this.dtpInvoiceDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpInvoiceDate.Location = new System.Drawing.Point(130, 23);
            this.dtpInvoiceDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpInvoiceDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpInvoiceDate.Name = "dtpInvoiceDate";
            this.dtpInvoiceDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpInvoiceDate.Size = new System.Drawing.Size(128, 24);
            this.dtpInvoiceDate.TabIndex = 83;
            this.dtpInvoiceDate.TabStop = false;
            this.dtpInvoiceDate.Text = "radDateTimePicker1";
            this.dtpInvoiceDate.Value = null;
            // 
            // dtpDueDate
            // 
            this.dtpDueDate.Culture = new System.Globalization.CultureInfo("en-AU");
            this.dtpDueDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDueDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpDueDate.Location = new System.Drawing.Point(131, 55);
            this.dtpDueDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpDueDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDueDate.Name = "dtpDueDate";
            this.dtpDueDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDueDate.Size = new System.Drawing.Size(128, 24);
            this.dtpDueDate.TabIndex = 104;
            this.dtpDueDate.TabStop = false;
            this.dtpDueDate.Text = "radDateTimePicker1";
            this.dtpDueDate.Value = null;
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.lblCountCompany);
            this.radGroupBox1.Controls.Add(this.btnGetBooking);
            this.radGroupBox1.Controls.Add(this.lblTillDate);
            this.radGroupBox1.Controls.Add(this.lblFromDate);
            this.radGroupBox1.Controls.Add(this.dtpTillDate);
            this.radGroupBox1.Controls.Add(this.dtpFromDate);
            this.radGroupBox1.Controls.Add(this.rbtnCustom);
            this.radGroupBox1.Controls.Add(this.rbtnAll);
            this.radGroupBox1.Controls.Add(this.rbtnMonthly);
            this.radGroupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.ForeColor = System.Drawing.Color.Black;
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Period";
            this.radGroupBox1.Location = new System.Drawing.Point(12, 6);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(382, 176);
            this.radGroupBox1.TabIndex = 106;
            this.radGroupBox1.Text = "Period";
            // 
            // lblCountCompany
            // 
            this.lblCountCompany.AutoSize = true;
            this.lblCountCompany.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.lblCountCompany.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblCountCompany.Location = new System.Drawing.Point(179, 68);
            this.lblCountCompany.Name = "lblCountCompany";
            this.lblCountCompany.Size = new System.Drawing.Size(137, 19);
            this.lblCountCompany.TabIndex = 107;
            this.lblCountCompany.Text = "Count Company";
            this.lblCountCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCountCompany.Visible = false;
            // 
            // btnGetBooking
            // 
            this.btnGetBooking.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetBooking.Location = new System.Drawing.Point(288, 115);
            this.btnGetBooking.Name = "btnGetBooking";
            this.btnGetBooking.Size = new System.Drawing.Size(81, 53);
            this.btnGetBooking.TabIndex = 106;
            this.btnGetBooking.Text = "Get Bookings";
            this.btnGetBooking.Click += new System.EventHandler(this.btnGetBooking_Click);
            // 
            // lblTillDate
            // 
            this.lblTillDate.AutoSize = true;
            this.lblTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTillDate.ForeColor = System.Drawing.Color.Black;
            this.lblTillDate.Location = new System.Drawing.Point(16, 145);
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
            this.lblFromDate.Location = new System.Drawing.Point(17, 117);
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
            this.dtpTillDate.Location = new System.Drawing.Point(130, 144);
            this.dtpTillDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTillDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Name = "dtpTillDate";
            this.dtpTillDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Size = new System.Drawing.Size(120, 24);
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
            this.dtpFromDate.Location = new System.Drawing.Point(130, 115);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(120, 24);
            this.dtpFromDate.TabIndex = 84;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "radDateTimePicker1";
            this.dtpFromDate.Value = null;
            // 
            // rbtnCustom
            // 
            this.rbtnCustom.AutoSize = true;
            this.rbtnCustom.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnCustom.ForeColor = System.Drawing.Color.Black;
            this.rbtnCustom.Location = new System.Drawing.Point(20, 78);
            this.rbtnCustom.Name = "rbtnCustom";
            this.rbtnCustom.Size = new System.Drawing.Size(81, 22);
            this.rbtnCustom.TabIndex = 3;
            this.rbtnCustom.Text = "Custom";
            this.rbtnCustom.UseVisualStyleBackColor = true;
            this.rbtnCustom.CheckedChanged += new System.EventHandler(this.rbtnCustom_CheckedChanged);
            // 
            // rbtnAll
            // 
            this.rbtnAll.AutoSize = true;
            this.rbtnAll.Checked = true;
            this.rbtnAll.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnAll.ForeColor = System.Drawing.Color.Black;
            this.rbtnAll.Location = new System.Drawing.Point(20, 23);
            this.rbtnAll.Name = "rbtnAll";
            this.rbtnAll.Size = new System.Drawing.Size(46, 22);
            this.rbtnAll.TabIndex = 1;
            this.rbtnAll.TabStop = true;
            this.rbtnAll.Text = "All";
            this.rbtnAll.UseVisualStyleBackColor = true;
            this.rbtnAll.CheckedChanged += new System.EventHandler(this.rbtnAll_CheckedChanged);
            // 
            // rbtnMonthly
            // 
            this.rbtnMonthly.AutoSize = true;
            this.rbtnMonthly.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnMonthly.ForeColor = System.Drawing.Color.Black;
            this.rbtnMonthly.Location = new System.Drawing.Point(20, 51);
            this.rbtnMonthly.Name = "rbtnMonthly";
            this.rbtnMonthly.Size = new System.Drawing.Size(86, 22);
            this.rbtnMonthly.TabIndex = 2;
            this.rbtnMonthly.Text = "Monthly";
            this.rbtnMonthly.UseVisualStyleBackColor = true;
            this.rbtnMonthly.CheckedChanged += new System.EventHandler(this.rbtnMonthly_CheckedChanged);
            // 
            // frmAddMultipleCompanyInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 599);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormTitle = "Multiple Account Invoice";
            this.Name = "frmAddMultipleCompanyInvoice";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Multiple Account Invoice";
            this.Load += new System.EventHandler(this.frmCompanyInvoice_Load);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
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
            ((System.ComponentModel.ISupportInitialize)(this.chkAllGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompany.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompanyGroup)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSubCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblInvoiceDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpInvoiceDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDueDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGetBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnCustom;
        private System.Windows.Forms.RadioButton rbtnMonthly;
        private System.Windows.Forms.RadioButton rbtnAll;
        private Telerik.WinControls.UI.RadLabel lblInvoiceDate;
        private UI.MyDatePicker dtpInvoiceDate;
        private System.Windows.Forms.Label lblDueDate;
        private UI.MyDatePicker dtpDueDate;
        private Telerik.WinControls.UI.RadButton btnGenerate;
        private System.Windows.Forms.CheckBox cbAllCompany;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private UI.MyDatePicker dtpFromDate;
        private UI.MyDatePicker dtpTillDate;
        private System.Windows.Forms.Label lblTillDate;
        private System.Windows.Forms.Label lblFromDate;
        private Telerik.WinControls.UI.RadButton btnExits;
        private UI.MyGridView grdCompany;
        private Telerik.WinControls.UI.RadComboBox ddlSubCompany;
        private System.Windows.Forms.Label label6;
        private Telerik.WinControls.UI.RadButton btnEmailInvoices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private Telerik.WinControls.UI.RadButton btnExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNotes;
        private Telerik.WinControls.UI.RadCheckBox chkAllGroup;
        private UI.MyDropDownList ddlCompanyGroup;
        private Telerik.WinControls.UI.RadButton btnGetBooking;
        private System.Windows.Forms.Label lblCountCompany;
        private Telerik.WinControls.UI.RadButton btnViewPrint;
    }
}