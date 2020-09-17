namespace Taxi_AppMain
{
    partial class frmDriverRentTransactionExpensesReport3
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
            this.vu_DriverRentExpenseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vu_FleetDriverRentExpenseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblCriteria = new System.Windows.Forms.Label();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.vu_DriverRentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlCriteria = new Telerik.WinControls.UI.RadPanel();
            this.optSummaryDetails = new Telerik.WinControls.UI.RadRadioButton();
            this.optAccountJobs = new Telerik.WinControls.UI.RadRadioButton();
            this.optFullDetail = new Telerik.WinControls.UI.RadRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnPrintCurrent = new Telerik.WinControls.UI.RadButton();
            this.btnEmailCurrent = new Telerik.WinControls.UI.RadButton();
            this.btnEmailAll = new Telerik.WinControls.UI.RadButton();
            this.btnPrintAll = new Telerik.WinControls.UI.RadButton();
            this.cbAllDrivers = new System.Windows.Forms.CheckBox();
            this.grdLister = new UI.MyGridView();
            this.txtPreviewlabel = new System.Windows.Forms.Label();
            this.btnPrev = new Telerik.WinControls.UI.RadButton();
            this.ddlDriver = new System.Windows.Forms.Label();
            this.btnNext = new Telerik.WinControls.UI.RadButton();
            this.myDatePicker1 = new UI.MyDatePicker();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.myDatePicker2 = new UI.MyDatePicker();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBody = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverRentExpenseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_FleetDriverRentExpenseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverRentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCriteria)).BeginInit();
            this.pnlCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optSummaryDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAccountJobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optFullDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // vu_DriverRentExpenseBindingSource
            // 
            this.vu_DriverRentExpenseBindingSource.DataSource = typeof(Taxi_Model.vu_DriverRentExpense);
            // 
            // vu_FleetDriverRentExpenseBindingSource
            // 
            this.vu_FleetDriverRentExpenseBindingSource.DataSource = typeof(Taxi_Model.vu_FleetDriverRentExpense);
            // 
            // lblCriteria
            // 
            this.lblCriteria.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCriteria.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCriteria.Location = new System.Drawing.Point(0, 220);
            this.lblCriteria.Name = "lblCriteria";
            this.lblCriteria.Size = new System.Drawing.Size(1042, 24);
            this.lblCriteria.TabIndex = 111;
            this.lblCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Taxi_Model_vu_DriverRentExpense";
            reportDataSource1.Value = this.vu_DriverRentExpenseBindingSource;
            reportDataSource2.Name = "Taxi_Model_vu_FleetDriverRentExpense";
            reportDataSource2.Value = this.vu_FleetDriverRentExpenseBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptDriverRentExpenses3.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 244);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1042, 536);
            this.reportViewer1.TabIndex = 112;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // vu_DriverRentBindingSource
            // 
            this.vu_DriverRentBindingSource.DataSource = typeof(Taxi_Model.vu_DriverRent);
            // 
            // pnlCriteria
            // 
            this.pnlCriteria.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlCriteria.Controls.Add(this.label2);
            this.pnlCriteria.Controls.Add(this.txtBody);
            this.pnlCriteria.Controls.Add(this.optSummaryDetails);
            this.pnlCriteria.Controls.Add(this.optFullDetail);
            this.pnlCriteria.Controls.Add(this.optAccountJobs);
            this.pnlCriteria.Controls.Add(this.label1);
            this.pnlCriteria.Controls.Add(this.txtSubject);
            this.pnlCriteria.Controls.Add(this.btnPrintCurrent);
            this.pnlCriteria.Controls.Add(this.btnEmailCurrent);
            this.pnlCriteria.Controls.Add(this.btnEmailAll);
            this.pnlCriteria.Controls.Add(this.btnPrintAll);
            this.pnlCriteria.Controls.Add(this.cbAllDrivers);
            this.pnlCriteria.Controls.Add(this.grdLister);
            this.pnlCriteria.Controls.Add(this.txtPreviewlabel);
            this.pnlCriteria.Controls.Add(this.btnPrev);
            this.pnlCriteria.Controls.Add(this.ddlDriver);
            this.pnlCriteria.Controls.Add(this.btnNext);
            this.pnlCriteria.Controls.Add(this.myDatePicker1);
            this.pnlCriteria.Controls.Add(this.radLabel1);
            this.pnlCriteria.Controls.Add(this.myDatePicker2);
            this.pnlCriteria.Controls.Add(this.radLabel4);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 38);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Size = new System.Drawing.Size(1042, 182);
            this.pnlCriteria.TabIndex = 113;
            // 
            // optSummaryDetails
            // 
            this.optSummaryDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.optSummaryDetails.ForeColor = System.Drawing.Color.Black;
            this.optSummaryDetails.Location = new System.Drawing.Point(523, 2);
            this.optSummaryDetails.Name = "optSummaryDetails";
            // 
            // 
            // 
            this.optSummaryDetails.RootElement.ForeColor = System.Drawing.Color.Black;
            this.optSummaryDetails.Size = new System.Drawing.Size(153, 18);
            this.optSummaryDetails.TabIndex = 213;
            this.optSummaryDetails.Text = "Summary Report";
            this.optSummaryDetails.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.optFullDetail_ToggleStateChanged);
            // 
            // optAccountJobs
            // 
            this.optAccountJobs.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.optAccountJobs.ForeColor = System.Drawing.Color.Black;
            this.optAccountJobs.Location = new System.Drawing.Point(365, 2);
            this.optAccountJobs.Name = "optAccountJobs";
            // 
            // 
            // 
            this.optAccountJobs.RootElement.ForeColor = System.Drawing.Color.Black;
            this.optAccountJobs.Size = new System.Drawing.Size(153, 18);
            this.optAccountJobs.TabIndex = 212;
            this.optAccountJobs.Text = "Account Jobs Report";
            this.optAccountJobs.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.optFullDetail_ToggleStateChanged);
            // 
            // optFullDetail
            // 
            this.optFullDetail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.optFullDetail.ForeColor = System.Drawing.Color.Black;
            this.optFullDetail.Location = new System.Drawing.Point(229, 2);
            this.optFullDetail.Name = "optFullDetail";
            // 
            // 
            // 
            this.optFullDetail.RootElement.ForeColor = System.Drawing.Color.Black;
            this.optFullDetail.Size = new System.Drawing.Size(134, 18);
            this.optFullDetail.TabIndex = 211;
            this.optFullDetail.Text = "Full Detail Report";
            this.optFullDetail.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.optFullDetail.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.optFullDetail_ToggleStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(369, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 140;
            this.label1.Text = "Email Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(474, 151);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(221, 26);
            this.txtSubject.TabIndex = 139;
            // 
            // btnPrintCurrent
            // 
            this.btnPrintCurrent.Location = new System.Drawing.Point(756, 1);
            this.btnPrintCurrent.Name = "btnPrintCurrent";
            this.btnPrintCurrent.Size = new System.Drawing.Size(113, 43);
            this.btnPrintCurrent.TabIndex = 138;
            this.btnPrintCurrent.Text = "Print Current Record";
            this.btnPrintCurrent.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrintCurrent.GetChildAt(0))).Text = "Print Current Record";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnEmailCurrent
            // 
            this.btnEmailCurrent.Location = new System.Drawing.Point(756, 61);
            this.btnEmailCurrent.Name = "btnEmailCurrent";
            this.btnEmailCurrent.Size = new System.Drawing.Size(113, 43);
            this.btnEmailCurrent.TabIndex = 137;
            this.btnEmailCurrent.Text = "Email Current Record";
            this.btnEmailCurrent.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailCurrent.GetChildAt(0))).Text = "Email Current Record";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnEmailAll
            // 
            this.btnEmailAll.Location = new System.Drawing.Point(917, 61);
            this.btnEmailAll.Name = "btnEmailAll";
            this.btnEmailAll.Size = new System.Drawing.Size(113, 43);
            this.btnEmailAll.TabIndex = 136;
            this.btnEmailAll.Text = "Email All";
            this.btnEmailAll.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailAll.GetChildAt(0))).Text = "Email All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(917, 1);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(113, 43);
            this.btnPrintAll.TabIndex = 135;
            this.btnPrintAll.Text = "Print All";
            this.btnPrintAll.TextWrap = true;
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
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = false;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.MidnightBlue;
            this.grdLister.Location = new System.Drawing.Point(8, 23);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(355, 149);
            this.grdLister.TabIndex = 133;
            this.grdLister.Text = "myGridView1";
            // 
            // txtPreviewlabel
            // 
            this.txtPreviewlabel.AutoSize = true;
            this.txtPreviewlabel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviewlabel.Location = new System.Drawing.Point(477, 103);
            this.txtPreviewlabel.Name = "txtPreviewlabel";
            this.txtPreviewlabel.Size = new System.Drawing.Size(159, 23);
            this.txtPreviewlabel.TabIndex = 132;
            this.txtPreviewlabel.Text = "Preview 1 of 10";
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(425, 47);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(129, 43);
            this.btnPrev.TabIndex = 131;
            this.btnPrev.Text = "<< Previous  ";
            this.btnPrev.TextWrap = true;
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
            this.btnNext.Location = new System.Drawing.Point(582, 47);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(113, 43);
            this.btnNext.TabIndex = 128;
            this.btnNext.Text = "Next >>";
            this.btnNext.TextWrap = true;
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(707, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 18);
            this.label2.TabIndex = 215;
            this.label2.Text = "Body";
            // 
            // txtBody
            // 
            this.txtBody.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBody.Location = new System.Drawing.Point(757, 116);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(273, 61);
            this.txtBody.TabIndex = 214;
            // 
            // frmDriverRentTransactionExpensesReport3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 780);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.lblCriteria);
            this.Controls.Add(this.pnlCriteria);
            this.FormTitle = "Driver Rent  Report";
            this.Name = "frmDriverRentTransactionExpensesReport3";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver Rent Report";
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
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverRentExpenseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_FleetDriverRentExpenseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vu_DriverRentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCriteria)).EndInit();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optSummaryDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAccountJobs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optFullDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
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
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.BindingSource vu_DriverRentExpenseBindingSource;
        private System.Windows.Forms.BindingSource vu_FleetDriverRentExpenseBindingSource;
        private System.Windows.Forms.BindingSource vu_DriverRentBindingSource;
        private Telerik.WinControls.UI.RadPanel pnlCriteria;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private Telerik.WinControls.UI.RadButton btnPrintCurrent;
        private Telerik.WinControls.UI.RadButton btnEmailCurrent;
        private Telerik.WinControls.UI.RadButton btnEmailAll;
        private Telerik.WinControls.UI.RadButton btnPrintAll;
        private System.Windows.Forms.CheckBox cbAllDrivers;
        private UI.MyGridView grdLister;
        private System.Windows.Forms.Label txtPreviewlabel;
        private Telerik.WinControls.UI.RadButton btnPrev;
        private System.Windows.Forms.Label ddlDriver;
        private Telerik.WinControls.UI.RadButton btnNext;
        private UI.MyDatePicker myDatePicker1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private UI.MyDatePicker myDatePicker2;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadRadioButton optSummaryDetails;
        private Telerik.WinControls.UI.RadRadioButton optAccountJobs;
        private Telerik.WinControls.UI.RadRadioButton optFullDetail;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBody;
    }
}