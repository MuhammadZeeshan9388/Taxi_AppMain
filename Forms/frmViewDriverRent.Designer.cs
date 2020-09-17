namespace Taxi_AppMain
{
    partial class frmViewDriverRent
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
            this.Vu_BookingBaseBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stp_DriverCommisionResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnAddNewRent = new Telerik.WinControls.UI.RadButton();
            this.chkExcludeCC = new Telerik.WinControls.UI.RadCheckBox();
            this.optCreditCard = new Telerik.WinControls.UI.RadRadioButton();
            this.ChkRent = new Telerik.WinControls.UI.RadCheckBox();
            this.txtRent = new Telerik.WinControls.UI.RadSpinEditor();
            this.optBoth = new Telerik.WinControls.UI.RadRadioButton();
            this.optCash = new Telerik.WinControls.UI.RadRadioButton();
            this.optAccount = new Telerik.WinControls.UI.RadRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnView = new Telerik.WinControls.UI.RadButton();
            this.ddl_Driver = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTillDate = new UI.MyDatePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtpFromDate = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.radPageViewPage2 = new Telerik.WinControls.UI.RadPageViewPage();
            this.grdLister = new UI.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vu_BookingBaseBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stp_DriverCommisionResultBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExcludeCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optCreditCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optBoth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
            this.radPageView1.SuspendLayout();
            this.radPageViewPage1.SuspendLayout();
            this.radPageViewPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // Vu_BookingBaseBindingSource
            // 
            this.Vu_BookingBaseBindingSource.DataSource = typeof(Taxi_Model.Vu_BookingBase);
            // 
            // stp_DriverCommisionResultBindingSource
            // 
            this.stp_DriverCommisionResultBindingSource.DataSource = typeof(Taxi_Model.stp_DriverCommisionResult);
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.GhostWhite;
            this.radPanel1.Controls.Add(this.btnAddNewRent);
            this.radPanel1.Controls.Add(this.chkExcludeCC);
            this.radPanel1.Controls.Add(this.optCreditCard);
            this.radPanel1.Controls.Add(this.ChkRent);
            this.radPanel1.Controls.Add(this.txtRent);
            this.radPanel1.Controls.Add(this.optBoth);
            this.radPanel1.Controls.Add(this.optCash);
            this.radPanel1.Controls.Add(this.optAccount);
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Controls.Add(this.btnView);
            this.radPanel1.Controls.Add(this.ddl_Driver);
            this.radPanel1.Controls.Add(this.label2);
            this.radPanel1.Controls.Add(this.dtpTillDate);
            this.radPanel1.Controls.Add(this.radLabel3);
            this.radPanel1.Controls.Add(this.dtpFromDate);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1073, 110);
            this.radPanel1.TabIndex = 108;
            // 
            // btnAddNewRent
            // 
            this.btnAddNewRent.Image = global::Taxi_AppMain.Properties.Resources.AddBig;
            this.btnAddNewRent.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNewRent.Location = new System.Drawing.Point(925, 10);
            this.btnAddNewRent.Name = "btnAddNewRent";
            this.btnAddNewRent.Size = new System.Drawing.Size(140, 53);
            this.btnAddNewRent.TabIndex = 104;
            this.btnAddNewRent.Text = "Add New Rent";
            this.btnAddNewRent.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewRent.Click += new System.EventHandler(this.btnAddNewRent_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewRent.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.AddBig;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewRent.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewRent.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewRent.GetChildAt(0))).Text = "Add New Rent";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddNewRent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddNewRent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkExcludeCC
            // 
            this.chkExcludeCC.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkExcludeCC.Location = new System.Drawing.Point(613, 13);
            this.chkExcludeCC.Name = "chkExcludeCC";
            this.chkExcludeCC.Size = new System.Drawing.Size(131, 18);
            this.chkExcludeCC.TabIndex = 103;
            this.chkExcludeCC.Text = "Exclude Credit Card ";
            // 
            // optCreditCard
            // 
            this.optCreditCard.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCreditCard.Location = new System.Drawing.Point(401, 12);
            this.optCreditCard.Name = "optCreditCard";
            this.optCreditCard.Size = new System.Drawing.Size(124, 18);
            this.optCreditCard.TabIndex = 102;
            this.optCreditCard.Text = "Credit Card";
            // 
            // ChkRent
            // 
            this.ChkRent.Font = new System.Drawing.Font("Tahoma", 10.5F);
            this.ChkRent.Location = new System.Drawing.Point(411, 48);
            this.ChkRent.Name = "ChkRent";
            this.ChkRent.Size = new System.Drawing.Size(51, 21);
            this.ChkRent.TabIndex = 100;
            this.ChkRent.Text = "Rent";
            this.ChkRent.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.ChkRent_ToggleStateChanged);
            // 
            // txtRent
            // 
            this.txtRent.Enabled = false;
            this.txtRent.EnableKeyMap = true;
            this.txtRent.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRent.InterceptArrowKeys = false;
            this.txtRent.Location = new System.Drawing.Point(359, 45);
            this.txtRent.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txtRent.Name = "txtRent";
            // 
            // 
            // 
            this.txtRent.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.txtRent.ShowBorder = true;
            this.txtRent.ShowUpDownButtons = false;
            this.txtRent.Size = new System.Drawing.Size(41, 24);
            this.txtRent.TabIndex = 99;
            this.txtRent.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.txtRent.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.txtRent.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtRent.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.txtRent.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // optBoth
            // 
            this.optBoth.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBoth.Location = new System.Drawing.Point(531, 13);
            this.optBoth.Name = "optBoth";
            this.optBoth.Size = new System.Drawing.Size(73, 18);
            this.optBoth.TabIndex = 94;
            this.optBoth.Text = "All";
            this.optBoth.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // optCash
            // 
            this.optCash.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCash.Location = new System.Drawing.Point(269, 12);
            this.optCash.Name = "optCash";
            this.optCash.Size = new System.Drawing.Size(126, 18);
            this.optCash.TabIndex = 93;
            this.optCash.Text = "Cash Statement";
            // 
            // optAccount
            // 
            this.optAccount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optAccount.Location = new System.Drawing.Point(121, 12);
            this.optAccount.Name = "optAccount";
            this.optAccount.Size = new System.Drawing.Size(142, 18);
            this.optAccount.TabIndex = 92;
            this.optAccount.Text = "Account Statement";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 18);
            this.label1.TabIndex = 91;
            this.label1.Text = "Report Type";
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnView.Location = new System.Drawing.Point(514, 48);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(114, 52);
            this.btnView.TabIndex = 89;
            this.btnView.Text = "View";
            this.btnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnView.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnView.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnView.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnView.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnView.GetChildAt(0))).Text = "View";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnView.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnView.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(121, 43);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(226, 26);
            this.ddl_Driver.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Driver";
            // 
            // dtpTillDate
            // 
            this.dtpTillDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpTillDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTillDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTillDate.Location = new System.Drawing.Point(359, 76);
            this.dtpTillDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTillDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Name = "dtpTillDate";
            this.dtpTillDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Size = new System.Drawing.Size(147, 24);
            this.dtpTillDate.TabIndex = 3;
            this.dtpTillDate.TabStop = false;
            this.dtpTillDate.Text = "myDatePicker2";
            this.dtpTillDate.Value = null;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(284, 78);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(62, 22);
            this.radLabel3.TabIndex = 2;
            this.radLabel3.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate.Location = new System.Drawing.Point(121, 76);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(148, 24);
            this.dtpFromDate.TabIndex = 1;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "myDatePicker1";
            this.dtpFromDate.Value = null;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(11, 78);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(79, 22);
            this.radLabel2.TabIndex = 0;
            this.radLabel2.Text = "From Date";
            // 
            // radPageView1
            // 
            this.radPageView1.Controls.Add(this.radPageViewPage1);
            this.radPageView1.Controls.Add(this.radPageViewPage2);
            this.radPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPageView1.Location = new System.Drawing.Point(0, 148);
            this.radPageView1.Name = "radPageView1";
            this.radPageView1.SelectedPage = this.radPageViewPage1;
            this.radPageView1.Size = new System.Drawing.Size(1073, 643);
            this.radPageView1.TabIndex = 109;
            this.radPageView1.Text = "radPageView1";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            // 
            // radPageViewPage1
            // 
            this.radPageViewPage1.Controls.Add(this.reportViewer1);
            this.radPageViewPage1.Location = new System.Drawing.Point(10, 37);
            this.radPageViewPage1.Name = "radPageViewPage1";
            this.radPageViewPage1.Size = new System.Drawing.Size(1052, 595);
            this.radPageViewPage1.Text = "View Rent";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Taxi_Model_Vu_BookingBase";
            reportDataSource1.Value = this.Vu_BookingBaseBindingSource;
            reportDataSource2.Name = "Taxi_Model_stp_DriverCommisionResult";
            reportDataSource2.Value = this.stp_DriverCommisionResultBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.Template3_rptDriverStatement.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1052, 595);
            this.reportViewer1.TabIndex = 115;
            // 
            // radPageViewPage2
            // 
            this.radPageViewPage2.Controls.Add(this.grdLister);
            this.radPageViewPage2.Location = new System.Drawing.Point(10, 37);
            this.radPageViewPage2.Name = "radPageViewPage2";
            this.radPageViewPage2.Size = new System.Drawing.Size(1058, 420);
            this.radPageViewPage2.Text = "Rent History";
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = false;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.grdLister.MasterTemplate.AllowEditRow = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(1058, 420);
            this.grdLister.TabIndex = 111;
            this.grdLister.Text = "myGridView1";
            // 
            // frmViewDriverRent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 791);
            this.ControlBox = true;
            this.Controls.Add(this.radPageView1);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Driver Rent";
            this.KeyPreview = true;
            this.Name = "frmViewDriverRent";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver Rent";
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.radPageView1, 0);
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
            ((System.ComponentModel.ISupportInitialize)(this.Vu_BookingBaseBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stp_DriverCommisionResultBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewRent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkExcludeCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optCreditCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChkRent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optBoth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
            this.radPageView1.ResumeLayout(false);
            this.radPageViewPage1.ResumeLayout(false);
            this.radPageViewPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadCheckBox chkExcludeCC;
        private Telerik.WinControls.UI.RadRadioButton optCreditCard;
        private Telerik.WinControls.UI.RadCheckBox ChkRent;
        private Telerik.WinControls.UI.RadSpinEditor txtRent;
        private Telerik.WinControls.UI.RadRadioButton optBoth;
        private Telerik.WinControls.UI.RadRadioButton optCash;
        private Telerik.WinControls.UI.RadRadioButton optAccount;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton btnView;
        private Telerik.WinControls.UI.RadDropDownList ddl_Driver;
        private System.Windows.Forms.Label label2;
        private UI.MyDatePicker dtpTillDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private UI.MyDatePicker dtpFromDate;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton btnAddNewRent;
        private Telerik.WinControls.UI.RadPageView radPageView1;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage1;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage2;
        private UI.MyGridView grdLister;
        private System.Windows.Forms.BindingSource stp_DriverCommisionResultBindingSource;
        private System.Windows.Forms.BindingSource Vu_BookingBaseBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}