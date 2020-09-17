namespace Taxi_AppMain
{
    partial class rptfrmDriverAccountBookings
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
            this.stp_DriverAccountBookingsResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdPaymentTypes = new UI.MyGridView();
            this.optIsNonVat = new Telerik.WinControls.UI.RadRadioButton();
            this.btnSelectPaymentType = new Telerik.WinControls.UI.RadToggleButton();
            this.txtPaymentTypes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ddlAllDriver = new Telerik.WinControls.UI.RadDropDownList();
            this.chkAllDriver = new Telerik.WinControls.UI.RadCheckBox();
            this.optBoth = new Telerik.WinControls.UI.RadRadioButton();
            this.optIsVat = new Telerik.WinControls.UI.RadRadioButton();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnEmail = new Telerik.WinControls.UI.RadButton();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.dtpTill = new UI.MyDatePicker();
            this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
            this.dtpFrom = new UI.MyDatePicker();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stp_DriverAccountBookingsResultBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentTypes.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optIsNonVat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAllDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optBoth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optIsVat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(741, 307);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(767, 245);
            // 
            // stp_DriverAccountBookingsResultBindingSource
            // 
            this.stp_DriverAccountBookingsResultBindingSource.DataSource = typeof(Taxi_Model.stp_DriverAccountBookingsResult);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optIsNonVat);
            this.panel1.Controls.Add(this.btnSelectPaymentType);
            this.panel1.Controls.Add(this.txtPaymentTypes);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ddlAllDriver);
            this.panel1.Controls.Add(this.chkAllDriver);
            this.panel1.Controls.Add(this.optBoth);
            this.panel1.Controls.Add(this.optIsVat);
            this.panel1.Controls.Add(this.btnExit1);
            this.panel1.Controls.Add(this.btnEmail);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.radLabel1);
            this.panel1.Controls.Add(this.dtpTill);
            this.panel1.Controls.Add(this.radLabel7);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1050, 94);
            this.panel1.TabIndex = 106;
            // 
            // grdPaymentTypes
            // 
            this.grdPaymentTypes.AutoCellFormatting = false;
            this.grdPaymentTypes.EnableCheckInCheckOut = false;
            this.grdPaymentTypes.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdPaymentTypes.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdPaymentTypes.Location = new System.Drawing.Point(375, 132);
            // 
            // grdPaymentTypes
            // 
            this.grdPaymentTypes.MasterTemplate.AllowAddNewRow = false;
            this.grdPaymentTypes.MasterTemplate.AllowEditRow = false;
            this.grdPaymentTypes.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdPaymentTypes.Name = "grdPaymentTypes";
            this.grdPaymentTypes.PKFieldColumnName = "";
            this.grdPaymentTypes.ShowGroupPanel = false;
            this.grdPaymentTypes.ShowImageOnActionButton = true;
            this.grdPaymentTypes.Size = new System.Drawing.Size(177, 238);
            this.grdPaymentTypes.TabIndex = 129;
            this.grdPaymentTypes.Text = "myGridView1";
            this.grdPaymentTypes.Visible = false;
            // 
            // optIsNonVat
            // 
            this.optIsNonVat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optIsNonVat.Location = new System.Drawing.Point(375, 36);
            this.optIsNonVat.Name = "optIsNonVat";
            this.optIsNonVat.Size = new System.Drawing.Size(133, 18);
            this.optIsNonVat.TabIndex = 71;
            this.optIsNonVat.Text = "Non Vat Accounts";
            // 
            // btnSelectPaymentType
            // 
            this.btnSelectPaymentType.Location = new System.Drawing.Point(553, 68);
            this.btnSelectPaymentType.Name = "btnSelectPaymentType";
            this.btnSelectPaymentType.Size = new System.Drawing.Size(25, 23);
            this.btnSelectPaymentType.TabIndex = 130;
            this.btnSelectPaymentType.Text = "...";
            this.btnSelectPaymentType.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.btnSelectPaymentType_ToggleStateChanged);
            ((Telerik.WinControls.UI.RadToggleButtonElement)(this.btnSelectPaymentType.GetChildAt(0))).Text = "...";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelectPaymentType.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelectPaymentType.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtPaymentTypes
            // 
            this.txtPaymentTypes.Font = new System.Drawing.Font("Tahoma", 9F);
            this.txtPaymentTypes.Location = new System.Drawing.Point(375, 68);
            this.txtPaymentTypes.Name = "txtPaymentTypes";
            this.txtPaymentTypes.Size = new System.Drawing.Size(177, 22);
            this.txtPaymentTypes.TabIndex = 128;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(286, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 14);
            this.label6.TabIndex = 127;
            this.label6.Text = "Payment Type";
            // 
            // ddlAllDriver
            // 
            this.ddlAllDriver.Enabled = false;
            this.ddlAllDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlAllDriver.Location = new System.Drawing.Point(97, 64);
            this.ddlAllDriver.Name = "ddlAllDriver";
            this.ddlAllDriver.Size = new System.Drawing.Size(168, 26);
            this.ddlAllDriver.TabIndex = 117;
            // 
            // chkAllDriver
            // 
            this.chkAllDriver.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllDriver.Location = new System.Drawing.Point(12, 68);
            this.chkAllDriver.Name = "chkAllDriver";
            this.chkAllDriver.Size = new System.Drawing.Size(70, 18);
            this.chkAllDriver.TabIndex = 116;
            this.chkAllDriver.Text = "All Driver";
            this.chkAllDriver.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.chkAllDriver.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkAllDriver_ToggleStateChanged_1);
            // 
            // optBoth
            // 
            this.optBoth.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBoth.Location = new System.Drawing.Point(275, 27);
            this.optBoth.Name = "optBoth";
            this.optBoth.Size = new System.Drawing.Size(56, 18);
            this.optBoth.TabIndex = 72;
            this.optBoth.Text = "Both";
            this.optBoth.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // optIsVat
            // 
            this.optIsVat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optIsVat.Location = new System.Drawing.Point(375, 6);
            this.optIsVat.Name = "optIsVat";
            this.optIsVat.Size = new System.Drawing.Size(110, 18);
            this.optIsVat.TabIndex = 70;
            this.optIsVat.Text = "Vat Accounts";
            // 
            // btnExit1
            // 
            this.btnExit1.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit1.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit1.Location = new System.Drawing.Point(955, 27);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(106, 48);
            this.btnExit1.TabIndex = 69;
            this.btnExit1.Text = "Exit";
            this.btnExit1.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnEmail
            // 
            this.btnEmail.Image = global::Taxi_AppMain.Properties.Resources.email;
            this.btnEmail.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnEmail.Location = new System.Drawing.Point(843, 27);
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(106, 48);
            this.btnEmail.TabIndex = 68;
            this.btnEmail.Text = "Email";
            this.btnEmail.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.email;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmail.GetChildAt(0))).Text = "Email";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExport
            // 
            this.btnExport.Image = global::Taxi_AppMain.Properties.Resources.pdf1;
            this.btnExport.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExport.Location = new System.Drawing.Point(731, 27);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(106, 48);
            this.btnExport.TabIndex = 67;
            this.btnExport.Text = "Export";
            this.btnExport.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.pdf1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).Text = "Export";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPrint
            // 
            this.btnPrint.Image = global::Taxi_AppMain.Properties.Resources.Print1;
            this.btnPrint.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnPrint.Location = new System.Drawing.Point(623, 27);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(106, 48);
            this.btnPrint.TabIndex = 66;
            this.btnPrint.Text = "Print";
            this.btnPrint.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Print1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrint.GetChildAt(0))).Text = "Print";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(12, 36);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(54, 19);
            this.radLabel1.TabIndex = 65;
            this.radLabel1.Text = "To Date";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel1.GetChildAt(0))).Text = "To Date";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel1.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel1.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpTill
            // 
            this.dtpTill.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpTill.CustomFormat = "dd/MM/yyyy";
            this.dtpTill.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTill.Location = new System.Drawing.Point(97, 37);
            this.dtpTill.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTill.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTill.Name = "dtpTill";
            this.dtpTill.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTill.Size = new System.Drawing.Size(126, 21);
            this.dtpTill.TabIndex = 64;
            this.dtpTill.TabStop = false;
            this.dtpTill.Text = "myDatePicker1";
            this.dtpTill.Value = null;
            // 
            // radLabel7
            // 
            this.radLabel7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel7.Location = new System.Drawing.Point(12, 9);
            this.radLabel7.Name = "radLabel7";
            this.radLabel7.Size = new System.Drawing.Size(69, 19);
            this.radLabel7.TabIndex = 63;
            this.radLabel7.Text = "From Date";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel7.GetChildAt(0))).Text = "From Date";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel7.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel7.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // dtpFrom
            // 
            this.dtpFrom.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFrom.CustomFormat = "dd/MM/yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFrom.Location = new System.Drawing.Point(97, 10);
            this.dtpFrom.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFrom.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFrom.Size = new System.Drawing.Size(126, 21);
            this.dtpFrom.TabIndex = 62;
            this.dtpFrom.TabStop = false;
            this.dtpFrom.Text = "myDatePicker1";
            this.dtpFrom.Value = null;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Taxi_Model_stp_DriverAccountBookingsResult";
            reportDataSource1.Value = this.stp_DriverAccountBookingsResultBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptDriverAccountBookings.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 132);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1050, 780);
            this.reportViewer1.TabIndex = 107;
            // 
            // rptfrmDriverAccountBookings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 912);
            this.Controls.Add(this.grdPaymentTypes);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.panel1);
            this.FormTitle = "Driver Account Report";
            this.Name = "rptfrmDriverAccountBookings";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver Account Report";
            this.Load += new System.EventHandler(this.rptfrmDriverAccountBookings_Load);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.reportViewer1, 0);
            this.Controls.SetChildIndex(this.grdPaymentTypes, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stp_DriverAccountBookingsResultBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentTypes.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optIsNonVat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAllDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optBoth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optIsVat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private UI.MyDatePicker dtpTill;
        private Telerik.WinControls.UI.RadLabel radLabel7;
        private UI.MyDatePicker dtpFrom;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource stp_DriverAccountBookingsResultBindingSource;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnEmail;
        private Telerik.WinControls.UI.RadButton btnExport;
        private Telerik.WinControls.UI.RadRadioButton optIsNonVat;
        private Telerik.WinControls.UI.RadRadioButton optIsVat;
        private Telerik.WinControls.UI.RadRadioButton optBoth;
        private Telerik.WinControls.UI.RadDropDownList ddlAllDriver;
        private Telerik.WinControls.UI.RadCheckBox chkAllDriver;
        private System.Windows.Forms.TextBox txtPaymentTypes;
        private System.Windows.Forms.Label label6;
        private UI.MyGridView grdPaymentTypes;
        private Telerik.WinControls.UI.RadToggleButton btnSelectPaymentType;
    }
}