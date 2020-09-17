namespace Taxi_AppMain
{
    partial class frmDriverCommisionTransactionExpensesReport
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource9 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource10 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource11 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource12 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.vu_DriverCommisionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vu_DriverCommisionExpenseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vu_FleetDriverCommissionExpenseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vuInvoiceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblCriteria = new System.Windows.Forms.Label();
            this.pnlCriteria = new Telerik.WinControls.UI.RadPanel();
            this.ddlCompany = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.btnViewReport = new Telerik.WinControls.UI.RadButton();
            this.dtpTillDate = new UI.MyDatePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtpFromDate = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
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
            this.ddlDriver = new System.Windows.Forms.Label();
            this.btnNext = new Telerik.WinControls.UI.RadButton();
            this.myDatePicker1 = new UI.MyDatePicker();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.myDatePicker2 = new UI.MyDatePicker();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverCommisionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverCommisionExpenseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_FleetDriverCommissionExpenseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vuInvoiceBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCriteria)).BeginInit();
            this.pnlCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // vu_DriverCommisionBindingSource
            // 
            this.vu_DriverCommisionBindingSource.DataSource = typeof(Taxi_Model.vu_DriverCommision);
            // 
            // vu_DriverCommisionExpenseBindingSource
            // 
            this.vu_DriverCommisionExpenseBindingSource.DataSource = typeof(Taxi_Model.vu_DriverCommisionExpense);
            // 
            // vu_FleetDriverCommissionExpenseBindingSource
            // 
            this.vu_FleetDriverCommissionExpenseBindingSource.DataSource = typeof(Taxi_Model.vu_FleetDriverCommissionExpense);
            // 
            // vuInvoiceBindingSource
            // 
            this.vuInvoiceBindingSource.DataSource = typeof(Taxi_Model.vu_DriverCommision);
            // 
            // lblCriteria
            // 
            this.lblCriteria.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCriteria.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria.Location = new System.Drawing.Point(0, 220);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(1042, 27);
            this.lblCriteria.TabIndex = 111;
            this.lblCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.pnlCriteria.Controls.Add(this.ddlDriver);
            this.pnlCriteria.Controls.Add(this.btnNext);
            this.pnlCriteria.Controls.Add(this.myDatePicker1);
            this.pnlCriteria.Controls.Add(this.radLabel1);
            this.pnlCriteria.Controls.Add(this.myDatePicker2);
            this.pnlCriteria.Controls.Add(this.radLabel4);
            this.pnlCriteria.Controls.Add(this.ddlCompany);
            this.pnlCriteria.Controls.Add(this.label2);
            this.pnlCriteria.Controls.Add(this.btnViewReport);
            this.pnlCriteria.Controls.Add(this.dtpTillDate);
            this.pnlCriteria.Controls.Add(this.radLabel3);
            this.pnlCriteria.Controls.Add(this.dtpFromDate);
            this.pnlCriteria.Controls.Add(this.radLabel2);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 38);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Size = new System.Drawing.Size(1042, 182);
            this.pnlCriteria.TabIndex = 110;
            // 
            // ddlCompany
            // 
            this.ddlCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompany.Location = new System.Drawing.Point(473, 132);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(226, 26);
            this.ddlCompany.TabIndex = 18;
            this.ddlCompany.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(392, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Company";
            this.label2.Visible = false;
            // 
            // btnViewReport
            // 
            this.btnViewReport.Location = new System.Drawing.Point(924, 143);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(113, 43);
            this.btnViewReport.TabIndex = 8;
            this.btnViewReport.Text = "Preview";
            this.btnViewReport.TextWrap = true;
            this.btnViewReport.Visible = false;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnViewReport.GetChildAt(0))).Text = "Preview";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnViewReport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpTillDate
            // 
            this.dtpTillDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTillDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTillDate.Location = new System.Drawing.Point(721, 171);
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
            this.radLabel3.Location = new System.Drawing.Point(600, 173);
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
            this.dtpFromDate.Location = new System.Drawing.Point(473, 171);
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
            this.radLabel2.Location = new System.Drawing.Point(331, 173);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(133, 22);
            this.radLabel2.TabIndex = 0;
            this.radLabel2.Text = "From Invoice Date";
            this.radLabel2.Visible = false;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource9.Name = "Taxi_Model_vu_DriverCommision";
            reportDataSource9.Value = this.vu_DriverCommisionBindingSource;
            reportDataSource10.Name = "Taxi_AppMain_Classes_ClsLogo";
            reportDataSource10.Value = null;
            reportDataSource11.Name = "Taxi_Model_vu_DriverCommisionExpense";
            reportDataSource11.Value = this.vu_DriverCommisionExpenseBindingSource;
            reportDataSource12.Name = "Taxi_Model_vu_FleetDriverCommissionExpense";
            reportDataSource12.Value = this.vu_FleetDriverCommissionExpenseBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource9);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource10);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource11);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource12);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptDriverCommisionExpenses.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 247);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1042, 533);
            this.reportViewer1.TabIndex = 112;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(702, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 140;
            this.label1.Text = "Email Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(810, 146);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(221, 26);
            this.txtSubject.TabIndex = 139;
            // 
            // btnPrintCurrent
            // 
            this.btnPrintCurrent.Location = new System.Drawing.Point(756, 41);
            this.btnPrintCurrent.Name = "btnPrintCurrent";
            this.btnPrintCurrent.Size = new System.Drawing.Size(113, 43);
            this.btnPrintCurrent.TabIndex = 138;
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
            this.btnEmailCurrent.Location = new System.Drawing.Point(756, 101);
            this.btnEmailCurrent.Name = "btnEmailCurrent";
            this.btnEmailCurrent.Size = new System.Drawing.Size(113, 43);
            this.btnEmailCurrent.TabIndex = 137;
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
            this.btnEmailAll.Location = new System.Drawing.Point(917, 101);
            this.btnEmailAll.Name = "btnEmailAll";
            this.btnEmailAll.Size = new System.Drawing.Size(113, 43);
            this.btnEmailAll.TabIndex = 136;
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
            this.btnPrintAll.Location = new System.Drawing.Point(917, 41);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(113, 43);
            this.btnPrintAll.TabIndex = 135;
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
            this.cbAllDrivers.TabIndex = 134;
            this.cbAllDrivers.Text = "Select All";
            this.cbAllDrivers.UseVisualStyleBackColor = true;
            // 
            // grdDriverCommission
            // 
            this.grdDriverCommission.AutoCellFormatting = false;
            this.grdDriverCommission.EnableCheckInCheckOut = false;
            this.grdDriverCommission.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDriverCommission.HeaderRowBorderColor = System.Drawing.Color.MidnightBlue;
            this.grdDriverCommission.Location = new System.Drawing.Point(8, 23);
            // 
            // grdDriverCommission
            // 
            this.grdDriverCommission.MasterTemplate.AllowAddNewRow = false;
            this.grdDriverCommission.Name = "grdDriverCommission";
            this.grdDriverCommission.PKFieldColumnName = "";
            this.grdDriverCommission.ShowGroupPanel = false;
            this.grdDriverCommission.ShowImageOnActionButton = true;
            this.grdDriverCommission.Size = new System.Drawing.Size(355, 149);
            this.grdDriverCommission.TabIndex = 133;
            this.grdDriverCommission.Text = "myGridView1";
            this.grdDriverCommission.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdDriverCommission_CellDoubleClick);
            // 
            // txtPreviewlabel
            // 
            this.txtPreviewlabel.AutoSize = true;
            this.txtPreviewlabel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviewlabel.Location = new System.Drawing.Point(481, 131);
            this.txtPreviewlabel.Name = "txtPreviewlabel";
            this.txtPreviewlabel.Size = new System.Drawing.Size(159, 23);
            this.txtPreviewlabel.TabIndex = 132;
            this.txtPreviewlabel.Text = "Preview 1 of 10";
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(425, 75);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(129, 43);
            this.btnPrev.TabIndex = 131;
            this.btnPrev.Text = "<< Previous  ";
            this.btnPrev.TextWrap = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrev.GetChildAt(0))).Text = "<< Previous  ";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlDriver
            // 
            this.ddlDriver.AutoSize = true;
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(59, 48);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Size = new System.Drawing.Size(46, 18);
            this.ddlDriver.TabIndex = 129;
            this.ddlDriver.Text = "Driver";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(582, 75);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(113, 43);
            this.btnNext.TabIndex = 128;
            this.btnNext.Text = "Next >>";
            this.btnNext.TextWrap = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNext.GetChildAt(0))).Text = "Next >>";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // myDatePicker1
            // 
            this.myDatePicker1.CustomFormat = "dd/MM/yyyy";
            this.myDatePicker1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myDatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.myDatePicker1.Location = new System.Drawing.Point(133, 73);
            this.myDatePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.myDatePicker1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.myDatePicker1.Name = "myDatePicker1";
            this.myDatePicker1.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.myDatePicker1.Size = new System.Drawing.Size(114, 24);
            this.myDatePicker1.TabIndex = 127;
            this.myDatePicker1.TabStop = false;
            this.myDatePicker1.Text = "myDatePicker2";
            this.myDatePicker1.Value = null;
            this.myDatePicker1.Visible = false;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(12, 75);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(116, 22);
            this.radLabel1.TabIndex = 126;
            this.radLabel1.Text = "To Invoice Date";
            this.radLabel1.Visible = false;
            // 
            // myDatePicker2
            // 
            this.myDatePicker2.CustomFormat = "dd/MM/yyyy";
            this.myDatePicker2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myDatePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.myDatePicker2.Location = new System.Drawing.Point(187, 70);
            this.myDatePicker2.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.myDatePicker2.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.myDatePicker2.Name = "myDatePicker2";
            this.myDatePicker2.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.myDatePicker2.Size = new System.Drawing.Size(114, 24);
            this.myDatePicker2.TabIndex = 125;
            this.myDatePicker2.TabStop = false;
            this.myDatePicker2.Text = "myDatePicker1";
            this.myDatePicker2.Value = null;
            this.myDatePicker2.Visible = false;
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(45, 72);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(133, 22);
            this.radLabel4.TabIndex = 124;
            this.radLabel4.Text = "From Invoice Date";
            this.radLabel4.Visible = false;
            // 
            // frmDriverCommisionTransactionExpensesReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 780);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.lblCriteria);
            this.Controls.Add(this.pnlCriteria);
            this.FormTitle = "Driver Commision Report";
            this.Name = "frmDriverCommisionTransactionExpensesReport";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver Commision Report";
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
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverCommisionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverCommisionExpenseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_FleetDriverCommissionExpenseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vuInvoiceBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCriteria)).EndInit();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblCriteria;
        private Telerik.WinControls.UI.RadPanel pnlCriteria;
        private Telerik.WinControls.UI.RadDropDownList ddlCompany;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadButton btnViewReport;
        private UI.MyDatePicker dtpTillDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private UI.MyDatePicker dtpFromDate;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.BindingSource vuInvoiceBindingSource;
        private System.Windows.Forms.BindingSource vu_DriverCommisionBindingSource;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource vu_DriverCommisionExpenseBindingSource;
        private System.Windows.Forms.BindingSource vu_FleetDriverCommissionExpenseBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private Telerik.WinControls.UI.RadButton btnPrintCurrent;
        private Telerik.WinControls.UI.RadButton btnEmailCurrent;
        private Telerik.WinControls.UI.RadButton btnEmailAll;
        private Telerik.WinControls.UI.RadButton btnPrintAll;
        private System.Windows.Forms.CheckBox cbAllDrivers;
        private UI.MyGridView grdDriverCommission;
        private System.Windows.Forms.Label txtPreviewlabel;
        private Telerik.WinControls.UI.RadButton btnPrev;
        private System.Windows.Forms.Label ddlDriver;
        private Telerik.WinControls.UI.RadButton btnNext;
        private UI.MyDatePicker myDatePicker1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private UI.MyDatePicker myDatePicker2;
        private Telerik.WinControls.UI.RadLabel radLabel4;

    }
}