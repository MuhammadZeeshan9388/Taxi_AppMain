﻿namespace Taxi_AppMain
{
    partial class frmAddAllDriverCommission 
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
            this.grdDriverCommission = new UI.MyGridView();
            this.cbAllDrivers = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ddlSubCompany = new Telerik.WinControls.UI.RadComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
            this.btnPrintAll = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnSendEmail = new Telerik.WinControls.UI.RadButton();
            this.btnExits = new Telerik.WinControls.UI.RadButton();
            this.btnGenerate = new Telerik.WinControls.UI.RadButton();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.btnDisplayCommission = new Telerik.WinControls.UI.RadButton();
            this.lblTillDate = new System.Windows.Forms.Label();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dtpTillDate = new UI.MyDatePicker();
            this.dtpFromDate = new UI.MyDatePicker();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSubCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).BeginInit();
            this.radGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDisplayCommission)).BeginInit();
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
            this.panel1.Controls.Add(this.grdDriverCommission);
            this.panel1.Controls.Add(this.cbAllDrivers);
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(856, 563);
            this.panel1.TabIndex = 0;
            // 
            // grdDriverCommission
            // 
            this.grdDriverCommission.AutoCellFormatting = false;
            this.grdDriverCommission.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDriverCommission.EnableCheckInCheckOut = false;
            this.grdDriverCommission.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDriverCommission.HeaderRowBorderColor = System.Drawing.Color.MidnightBlue;
            this.grdDriverCommission.Location = new System.Drawing.Point(0, 20);
            // 
            // grdDriverCommission
            // 
            this.grdDriverCommission.MasterTemplate.AllowAddNewRow = false;
            this.grdDriverCommission.Name = "grdDriverCommission";
            this.grdDriverCommission.PKFieldColumnName = "";
            this.grdDriverCommission.ShowGroupPanel = false;
            this.grdDriverCommission.ShowImageOnActionButton = true;
            this.grdDriverCommission.Size = new System.Drawing.Size(856, 543);
            this.grdDriverCommission.TabIndex = 115;
            this.grdDriverCommission.Text = "myGridView1";
            this.grdDriverCommission.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdCompany_CellDoubleClick);
            // 
            // cbAllDrivers
            // 
            this.cbAllDrivers.AutoSize = true;
            this.cbAllDrivers.Checked = true;
            this.cbAllDrivers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllDrivers.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbAllDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAllDrivers.Location = new System.Drawing.Point(0, 0);
            this.cbAllDrivers.Name = "cbAllDrivers";
            this.cbAllDrivers.Size = new System.Drawing.Size(856, 20);
            this.cbAllDrivers.TabIndex = 0;
            this.cbAllDrivers.Text = "Select All";
            this.cbAllDrivers.UseVisualStyleBackColor = true;
            this.cbAllDrivers.CheckedChanged += new System.EventHandler(this.cbAllCompany_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.AliceBlue;
            this.panel2.Controls.Add(this.ddlSubCompany);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.radGroupBox2);
            this.panel2.Controls.Add(this.radGroupBox1);
            this.panel2.Location = new System.Drawing.Point(856, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 560);
            this.panel2.TabIndex = 106;
            // 
            // ddlSubCompany
            // 
            this.ddlSubCompany.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlSubCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlSubCompany.Location = new System.Drawing.Point(146, 247);
            this.ddlSubCompany.Name = "ddlSubCompany";
            // 
            // 
            // 
            this.ddlSubCompany.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.ddlSubCompany.Size = new System.Drawing.Size(198, 24);
            this.ddlSubCompany.TabIndex = 109;
            this.ddlSubCompany.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 18);
            this.label6.TabIndex = 108;
            this.label6.Text = "Company Header";
            // 
            // radGroupBox2
            // 
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
            this.radGroupBox2.Location = new System.Drawing.Point(3, 274);
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
            // btnPrintAll
            // 
            this.btnPrintAll.Enabled = false;
            this.btnPrintAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAll.Location = new System.Drawing.Point(201, 152);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(118, 46);
            this.btnPrintAll.TabIndex = 115;
            this.btnPrintAll.Text = "Print All";
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
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(112, 104);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(221, 26);
            this.txtSubject.TabIndex = 113;
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Location = new System.Drawing.Point(30, 152);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(118, 46);
            this.btnSendEmail.TabIndex = 110;
            this.btnSendEmail.Text = "Send Email ";
            this.btnSendEmail.Click += new System.EventHandler(this.btnEmailInvoices_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).Text = "Send Email ";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExits
            // 
            this.btnExits.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExits.Location = new System.Drawing.Point(121, 224);
            this.btnExits.Name = "btnExits";
            this.btnExits.Size = new System.Drawing.Size(107, 46);
            this.btnExits.TabIndex = 106;
            this.btnExits.Text = "Exit";
            this.btnExits.Click += new System.EventHandler(this.btnExits_Click_1);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExits.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExits.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExits.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnGenerate
            // 
            this.btnGenerate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerate.Location = new System.Drawing.Point(74, 20);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(186, 63);
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
            this.radGroupBox1.Controls.Add(this.btnDisplayCommission);
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
            this.radGroupBox1.Location = new System.Drawing.Point(3, 39);
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
            // btnDisplayCommission
            // 
            this.btnDisplayCommission.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisplayCommission.Location = new System.Drawing.Point(29, 131);
            this.btnDisplayCommission.Name = "btnDisplayCommission";
            this.btnDisplayCommission.Size = new System.Drawing.Size(134, 46);
            this.btnDisplayCommission.TabIndex = 106;
            this.btnDisplayCommission.Text = "Display Commission";
            this.btnDisplayCommission.Click += new System.EventHandler(this.btnDisplayRent_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDisplayCommission.GetChildAt(0))).Text = "Display Commission";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDisplayCommission.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDisplayCommission.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // frmAddAllDriverCommission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 599);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormTitle = "Add All Driver Commission";
            this.KeyPreview = true;
            this.Name = "frmAddAllDriverCommission";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Add All Driver Commission";
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
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlSubCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox2)).EndInit();
            this.radGroupBox2.ResumeLayout(false);
            this.radGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            this.radGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDisplayCommission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadButton btnGenerate;
        private System.Windows.Forms.CheckBox cbAllDrivers;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox2;
        private UI.MyDatePicker dtpFromDate;
        private UI.MyDatePicker dtpTillDate;
        private System.Windows.Forms.Label lblTillDate;
        private System.Windows.Forms.Label lblFromDate;
        private Telerik.WinControls.UI.RadButton btnExits;
        private UI.MyGridView grdDriverCommission;
        private Telerik.WinControls.UI.RadButton btnSendEmail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private Telerik.WinControls.UI.RadButton btnDisplayCommission;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnPrintAll;
        private Telerik.WinControls.UI.RadComboBox ddlSubCompany;
        private System.Windows.Forms.Label label6;
    }
}