namespace Taxi_AppMain
{
    partial class frmDriverCommisionTransactionExpensesReport4
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.vu_FleetDriverCommissionExpenseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vu_DriverCommisionExpenses2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblCriteria = new System.Windows.Forms.Label();
            this.pnlCriteria = new Telerik.WinControls.UI.RadPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnPrintCurrent = new Telerik.WinControls.UI.RadButton();
            this.btnEmailCurrent = new Telerik.WinControls.UI.RadButton();
            this.btnEmailAll = new Telerik.WinControls.UI.RadButton();
            this.btnPrintAll = new Telerik.WinControls.UI.RadButton();
            this.cbAllDrivers = new System.Windows.Forms.CheckBox();
            this.grdDriverCommission = new UI.MyGridView();
            this.txtPreviewlabel = new System.Windows.Forms.Label();
            this.btnPrev = new Telerik.WinControls.UI.RadButton();
            this.ddlCompany = new Telerik.WinControls.UI.RadDropDownList();
            this.ddlDriver = new System.Windows.Forms.Label();
            this.btnNext = new Telerik.WinControls.UI.RadButton();
            this.dtpTillDate = new UI.MyDatePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtpFromDate = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ClsLogoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_FleetDriverCommissionExpenseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverCommisionExpenses2BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCriteria)).BeginInit();
            this.pnlCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClsLogoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // vu_FleetDriverCommissionExpenseBindingSource
            // 
            this.vu_FleetDriverCommissionExpenseBindingSource.DataSource = typeof(Taxi_Model.vu_FleetDriverCommissionExpense);
            // 
            // vu_DriverCommisionExpenses2BindingSource
            // 
            this.vu_DriverCommisionExpenses2BindingSource.DataSource = typeof(Taxi_Model.vu_DriverCommisionExpenses2);
            // 
            // lblCriteria
            // 
            this.lblCriteria.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCriteria.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria.Location = new System.Drawing.Point(0, 181);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(1042, 27);
            this.lblCriteria.TabIndex = 111;
            this.lblCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlCriteria.Controls.Add(this.label1);
            this.pnlCriteria.Controls.Add(this.txtSubject);
            this.pnlCriteria.Controls.Add(this.btnPrintCurrent);
            this.pnlCriteria.Controls.Add(this.btnEmailCurrent);
            this.pnlCriteria.Controls.Add(this.btnEmailAll);
            this.pnlCriteria.Controls.Add(this.btnPrintAll);
            this.pnlCriteria.Controls.Add(this.cbAllDrivers);
            this.pnlCriteria.Controls.Add(this.grdDriverCommission);
            this.pnlCriteria.Controls.Add(this.txtPreviewlabel);
            this.pnlCriteria.Controls.Add(this.btnPrev);
            this.pnlCriteria.Controls.Add(this.ddlCompany);
            this.pnlCriteria.Controls.Add(this.ddlDriver);
            this.pnlCriteria.Controls.Add(this.btnNext);
            this.pnlCriteria.Controls.Add(this.dtpTillDate);
            this.pnlCriteria.Controls.Add(this.radLabel3);
            this.pnlCriteria.Controls.Add(this.dtpFromDate);
            this.pnlCriteria.Controls.Add(this.radLabel2);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 38);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Size = new System.Drawing.Size(1042, 143);
            this.pnlCriteria.TabIndex = 110;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(702, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 123;
            this.label1.Text = "Email Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(810, 116);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(221, 26);
            this.txtSubject.TabIndex = 122;
            // 
            // btnPrintCurrent
            // 
            this.btnPrintCurrent.Location = new System.Drawing.Point(756, 11);
            this.btnPrintCurrent.Name = "btnPrintCurrent";
            this.btnPrintCurrent.Size = new System.Drawing.Size(113, 43);
            this.btnPrintCurrent.TabIndex = 121;
            this.btnPrintCurrent.Text = "Print Current Record";
            this.btnPrintCurrent.TextWrap = true;
            this.btnPrintCurrent.Click += new System.EventHandler(this.btnPrintCurrent_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrintCurrent.GetChildAt(0))).Text = "Print Current Record";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnEmailCurrent
            // 
            this.btnEmailCurrent.Location = new System.Drawing.Point(756, 71);
            this.btnEmailCurrent.Name = "btnEmailCurrent";
            this.btnEmailCurrent.Size = new System.Drawing.Size(113, 43);
            this.btnEmailCurrent.TabIndex = 120;
            this.btnEmailCurrent.Text = "Email Current Record";
            this.btnEmailCurrent.TextWrap = true;
            this.btnEmailCurrent.Click += new System.EventHandler(this.btnEmailCurrent_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailCurrent.GetChildAt(0))).Text = "Email Current Record";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnEmailAll
            // 
            this.btnEmailAll.Location = new System.Drawing.Point(917, 71);
            this.btnEmailAll.Name = "btnEmailAll";
            this.btnEmailAll.Size = new System.Drawing.Size(113, 43);
            this.btnEmailAll.TabIndex = 119;
            this.btnEmailAll.Text = "Email All";
            this.btnEmailAll.TextWrap = true;
            this.btnEmailAll.Click += new System.EventHandler(this.btnEmailAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailAll.GetChildAt(0))).Text = "Email All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(917, 11);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(113, 43);
            this.btnPrintAll.TabIndex = 118;
            this.btnPrintAll.Text = "Print All";
            this.btnPrintAll.TextWrap = true;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrintAll.GetChildAt(0))).Text = "Print All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.cbAllDrivers.Size = new System.Drawing.Size(1042, 20);
            this.cbAllDrivers.TabIndex = 117;
            this.cbAllDrivers.Text = "Select All";
            this.cbAllDrivers.UseVisualStyleBackColor = true;
            this.cbAllDrivers.CheckedChanged += new System.EventHandler(this.cbAllDrivers_CheckedChanged);
            // 
            // grdDriverCommission
            // 
            this.grdDriverCommission.AutoCellFormatting = false;
            this.grdDriverCommission.EnableCheckInCheckOut = false;
            this.grdDriverCommission.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDriverCommission.HeaderRowBorderColor = System.Drawing.Color.MidnightBlue;
            this.grdDriverCommission.Location = new System.Drawing.Point(8, 21);
            // 
            // grdDriverCommission
            // 
            this.grdDriverCommission.MasterTemplate.AllowAddNewRow = false;
            this.grdDriverCommission.Name = "grdDriverCommission";
            this.grdDriverCommission.PKFieldColumnName = "";
            this.grdDriverCommission.ShowGroupPanel = false;
            this.grdDriverCommission.ShowImageOnActionButton = true;
            this.grdDriverCommission.Size = new System.Drawing.Size(355, 120);
            this.grdDriverCommission.TabIndex = 116;
            this.grdDriverCommission.Text = "myGridView1";
            this.grdDriverCommission.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdDriverCommission_CellDoubleClick);
            // 
            // txtPreviewlabel
            // 
            this.txtPreviewlabel.AutoSize = true;
            this.txtPreviewlabel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviewlabel.Location = new System.Drawing.Point(481, 101);
            this.txtPreviewlabel.Name = "txtPreviewlabel";
            this.txtPreviewlabel.Size = new System.Drawing.Size(159, 23);
            this.txtPreviewlabel.TabIndex = 20;
            this.txtPreviewlabel.Text = "Preview 1 of 10";
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(425, 45);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(129, 43);
            this.btnPrev.TabIndex = 19;
            this.btnPrev.Text = "<< Previous  ";
            this.btnPrev.TextWrap = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrev.GetChildAt(0))).Text = "<< Previous  ";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlCompany
            // 
            this.ddlCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompany.Location = new System.Drawing.Point(140, 15);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(226, 26);
            this.ddlCompany.TabIndex = 18;
            // 
            // ddlDriver
            // 
            this.ddlDriver.AutoSize = true;
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(59, 18);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Size = new System.Drawing.Size(46, 18);
            this.ddlDriver.TabIndex = 17;
            this.ddlDriver.Text = "Driver";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(582, 45);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(113, 43);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Next >>";
            this.btnNext.TextWrap = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNext.GetChildAt(0))).Text = "Next >>";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpTillDate
            // 
            this.dtpTillDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTillDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTillDate.Location = new System.Drawing.Point(133, 43);
            this.dtpTillDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTillDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Name = "dtpTillDate";
            this.dtpTillDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Size = new System.Drawing.Size(114, 24);
            this.dtpTillDate.TabIndex = 3;
            this.dtpTillDate.TabStop = false;
            this.dtpTillDate.Text = "myDatePicker2";
            this.dtpTillDate.Value = null;
            this.dtpTillDate.Visible = false;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(12, 45);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(116, 22);
            this.radLabel3.TabIndex = 2;
            this.radLabel3.Text = "To Invoice Date";
            this.radLabel3.Visible = false;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate.Location = new System.Drawing.Point(187, 40);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(114, 24);
            this.dtpFromDate.TabIndex = 1;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "myDatePicker1";
            this.dtpFromDate.Value = null;
            this.dtpFromDate.Visible = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(45, 42);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(133, 22);
            this.radLabel2.TabIndex = 0;
            this.radLabel2.Text = "From Invoice Date";
            this.radLabel2.Visible = false;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Value = null;
            reportDataSource2.Name = "Taxi_AppMain_Classes_ClsLogo";
            reportDataSource2.Value = null;
            reportDataSource3.Name = "Taxi_Model_vu_FleetDriverCommissionExpense";
            reportDataSource3.Value = this.vu_FleetDriverCommissionExpenseBindingSource;
            reportDataSource4.Name = "Taxi_Model_vu_DriverCommisionExpenses2";
            reportDataSource4.Value = this.vu_DriverCommisionExpenses2BindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptDriverCommisionExpenses3.rdlc";
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptDriverCommisionExpenses4.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 208);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1042, 572);
            this.reportViewer1.TabIndex = 112;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // ClsLogoBindingSource
            // 
            this.ClsLogoBindingSource.DataSource = typeof(Taxi_AppMain.Classes.ClsLogo);
            // 
            // frmDriverCommisionTransactionExpensesReport4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 780);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.lblCriteria);
            this.Controls.Add(this.pnlCriteria);
            this.FormTitle = "Driver Commision Report";
            this.Name = "frmDriverCommisionTransactionExpensesReport4";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver Commission Report";
            this.Load += new System.EventHandler(this.frmInvoiceReport_Load);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.pnlCriteria, 0);
            this.Controls.SetChildIndex(this.lblCriteria, 0);
            this.Controls.SetChildIndex(this.reportViewer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_FleetDriverCommissionExpenseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverCommisionExpenses2BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCriteria)).EndInit();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClsLogoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCriteria;
        private Telerik.WinControls.UI.RadPanel pnlCriteria;
        private Telerik.WinControls.UI.RadDropDownList ddlCompany;
        private System.Windows.Forms.Label ddlDriver;
        private Telerik.WinControls.UI.RadButton btnNext;
        private UI.MyDatePicker dtpTillDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private UI.MyDatePicker dtpFromDate;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource vu_FleetDriverCommissionExpenseBindingSource;
        private System.Windows.Forms.BindingSource vu_DriverCommisionExpenses2BindingSource;
        private System.Windows.Forms.Label txtPreviewlabel;
        private Telerik.WinControls.UI.RadButton btnPrev;
        private UI.MyGridView grdDriverCommission;
        private System.Windows.Forms.CheckBox cbAllDrivers;
        private Telerik.WinControls.UI.RadButton btnEmailCurrent;
        private Telerik.WinControls.UI.RadButton btnEmailAll;
        private Telerik.WinControls.UI.RadButton btnPrintAll;
        private Telerik.WinControls.UI.RadButton btnPrintCurrent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.BindingSource ClsLogoBindingSource;

    }
}