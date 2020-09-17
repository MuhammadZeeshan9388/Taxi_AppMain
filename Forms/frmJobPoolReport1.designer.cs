namespace Taxi_AppMain
{
    partial class frmJobPoolReport1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdTransferredJob = new Telerik.WinControls.UI.RadGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdAcceptedJob = new Telerik.WinControls.UI.RadGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.dtp_TransferredFrom = new UI.MyDatePicker();
            this.dtp_TranferredTo = new UI.MyDatePicker();
            this.btn_export_transferred = new Telerik.WinControls.UI.RadButton();
            this.btn_ShowTransferred_job = new Telerik.WinControls.UI.RadButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_AcceptedFrom = new UI.MyDatePicker();
            this.dtp_AcceptedTo = new UI.MyDatePicker();
            this.btn_export_accepted = new Telerik.WinControls.UI.RadButton();
            this.btn_ShowAccepted_job = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl_status_0 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_status_1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferredJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferredJob.MasterTemplate)).BeginInit();
            this.grdTransferredJob.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAcceptedJob)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAcceptedJob.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_TransferredFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_TranferredTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_export_transferred)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ShowTransferred_job)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_AcceptedFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_AcceptedTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_export_accepted)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ShowAccepted_job)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Click += new System.EventHandler(this.btnSaveOn_Click);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Click += new System.EventHandler(this.btnOnNew_Click);
            // 
            // btnExit
            // 
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Click += new System.EventHandler(this.btnSaveAndNew_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 38);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1214, 654);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Controls.Add(this.grdTransferredJob);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1206, 625);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Transferred Job";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // grdTransferredJob
            // 
            this.grdTransferredJob.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTransferredJob.Controls.Add(this.panel3);
            this.grdTransferredJob.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdTransferredJob.Location = new System.Drawing.Point(3, 34);
            // 
            // grdTransferredJob
            // 
            this.grdTransferredJob.MasterTemplate.AllowAddNewRow = false;
            this.grdTransferredJob.MasterTemplate.AllowEditRow = false;
            this.grdTransferredJob.MasterTemplate.EnableFiltering = true;
            this.grdTransferredJob.MasterTemplate.EnableGrouping = false;
            this.grdTransferredJob.MasterTemplate.ShowGroupedColumns = true;
            this.grdTransferredJob.Name = "grdTransferredJob";
            this.grdTransferredJob.ShowGroupPanel = false;
            this.grdTransferredJob.Size = new System.Drawing.Size(1200, 496);
            this.grdTransferredJob.TabIndex = 0;
            this.grdTransferredJob.Text = "radGridView1";
           // this.grdTransferredJob.Click += new System.EventHandler(this.grdOperatorPrivateHireDriverRecord_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtp_TransferredFrom);
            this.panel1.Controls.Add(this.dtp_TranferredTo);
            this.panel1.Controls.Add(this.btn_export_transferred);
            this.panel1.Controls.Add(this.btn_ShowTransferred_job);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 31);
            this.panel1.TabIndex = 1;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.grdAcceptedJob);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1206, 625);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Accepted Job";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // grdAcceptedJob
            // 
            this.grdAcceptedJob.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAcceptedJob.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAcceptedJob.Location = new System.Drawing.Point(3, 34);
            // 
            // grdAcceptedJob
            // 
            this.grdAcceptedJob.MasterTemplate.AllowAddNewRow = false;
            this.grdAcceptedJob.MasterTemplate.AllowEditRow = false;
            this.grdAcceptedJob.MasterTemplate.EnableFiltering = true;
            this.grdAcceptedJob.MasterTemplate.EnableGrouping = false;
            this.grdAcceptedJob.MasterTemplate.ShowGroupedColumns = true;
            this.grdAcceptedJob.Name = "grdAcceptedJob";
            this.grdAcceptedJob.ShowGroupPanel = false;
            this.grdAcceptedJob.Size = new System.Drawing.Size(1200, 507);
            this.grdAcceptedJob.TabIndex = 3;
            this.grdAcceptedJob.Text = "radGridView1";
            this.grdAcceptedJob.Click += new System.EventHandler(this.grdOperatorVehicleRecord_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtp_AcceptedFrom);
            this.panel2.Controls.Add(this.dtp_AcceptedTo);
            this.panel2.Controls.Add(this.btn_export_accepted);
            this.panel2.Controls.Add(this.btn_ShowAccepted_job);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1200, 31);
            this.panel2.TabIndex = 2;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel Files (.xls)|*.xls|Advanced Excel Files (.xlsx)|*.xlsx";
            this.saveFileDialog1.FilterIndex = 0;
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "Excel Files (.xls)|*.xls|Advanced Excel Files (.xlsx)|*.xlsx";
            this.saveFileDialog2.FilterIndex = 0;
            this.saveFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog2_FileOk);
            // 
            // dtp_TransferredFrom
            // 
            this.dtp_TransferredFrom.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtp_TransferredFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_TransferredFrom.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_TransferredFrom.Location = new System.Drawing.Point(541, 4);
            this.dtp_TransferredFrom.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_TransferredFrom.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_TransferredFrom.Name = "dtp_TransferredFrom";
            this.dtp_TransferredFrom.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_TransferredFrom.Size = new System.Drawing.Size(264, 21);
            this.dtp_TransferredFrom.TabIndex = 239;
            this.dtp_TransferredFrom.TabStop = false;
            this.dtp_TransferredFrom.Text = "myDatePicker1";
            this.dtp_TransferredFrom.Value = null;
            // 
            // dtp_TranferredTo
            // 
            this.dtp_TranferredTo.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtp_TranferredTo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_TranferredTo.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_TranferredTo.Location = new System.Drawing.Point(214, 6);
            this.dtp_TranferredTo.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_TranferredTo.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_TranferredTo.Name = "dtp_TranferredTo";
            this.dtp_TranferredTo.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_TranferredTo.Size = new System.Drawing.Size(230, 21);
            this.dtp_TranferredTo.TabIndex = 240;
            this.dtp_TranferredTo.TabStop = false;
            this.dtp_TranferredTo.Text = "myDatePicker1";
            this.dtp_TranferredTo.Value = null;
            // 
            // btn_export_transferred
            // 
            this.btn_export_transferred.Location = new System.Drawing.Point(958, 3);
            this.btn_export_transferred.Name = "btn_export_transferred";
            this.btn_export_transferred.Size = new System.Drawing.Size(107, 24);
            this.btn_export_transferred.TabIndex = 237;
            this.btn_export_transferred.Text = "Export";
            this.btn_export_transferred.Click += new System.EventHandler(this.btn_export_transferred_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_export_transferred.GetChildAt(0))).Text = "Export";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_export_transferred.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_export_transferred.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btn_ShowTransferred_job
            // 
            this.btn_ShowTransferred_job.Location = new System.Drawing.Point(828, 4);
            this.btn_ShowTransferred_job.Name = "btn_ShowTransferred_job";
            this.btn_ShowTransferred_job.Size = new System.Drawing.Size(107, 24);
            this.btn_ShowTransferred_job.TabIndex = 238;
            this.btn_ShowTransferred_job.Text = "Show";
            this.btn_ShowTransferred_job.Click += new System.EventHandler(this.btn_ShowTransferred_job_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_ShowTransferred_job.GetChildAt(0))).Text = "Show";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_ShowTransferred_job.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_ShowTransferred_job.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 235;
            this.label3.Text = "From Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(135, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 16);
            this.label5.TabIndex = 236;
            this.label5.Text = "To Date";
            // 
            // dtp_AcceptedFrom
            // 
            this.dtp_AcceptedFrom.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtp_AcceptedFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_AcceptedFrom.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_AcceptedFrom.Location = new System.Drawing.Point(468, 4);
            this.dtp_AcceptedFrom.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_AcceptedFrom.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_AcceptedFrom.Name = "dtp_AcceptedFrom";
            this.dtp_AcceptedFrom.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_AcceptedFrom.Size = new System.Drawing.Size(264, 21);
            this.dtp_AcceptedFrom.TabIndex = 239;
            this.dtp_AcceptedFrom.TabStop = false;
            this.dtp_AcceptedFrom.Text = "myDatePicker1";
            this.dtp_AcceptedFrom.Value = null;
            // 
            // dtp_AcceptedTo
            // 
            this.dtp_AcceptedTo.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtp_AcceptedTo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtp_AcceptedTo.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtp_AcceptedTo.Location = new System.Drawing.Point(128, 4);
            this.dtp_AcceptedTo.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtp_AcceptedTo.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_AcceptedTo.Name = "dtp_AcceptedTo";
            this.dtp_AcceptedTo.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_AcceptedTo.Size = new System.Drawing.Size(230, 21);
            this.dtp_AcceptedTo.TabIndex = 240;
            this.dtp_AcceptedTo.TabStop = false;
            this.dtp_AcceptedTo.Text = "myDatePicker1";
            this.dtp_AcceptedTo.Value = null;
            // 
            // btn_export_accepted
            // 
            this.btn_export_accepted.Location = new System.Drawing.Point(885, 2);
            this.btn_export_accepted.Name = "btn_export_accepted";
            this.btn_export_accepted.Size = new System.Drawing.Size(107, 24);
            this.btn_export_accepted.TabIndex = 237;
            this.btn_export_accepted.Text = "Export";
            this.btn_export_accepted.Click += new System.EventHandler(this.btn_export_accepted_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_export_accepted.GetChildAt(0))).Text = "Export";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_export_accepted.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_export_accepted.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btn_ShowAccepted_job
            // 
            this.btn_ShowAccepted_job.Location = new System.Drawing.Point(755, 3);
            this.btn_ShowAccepted_job.Name = "btn_ShowAccepted_job";
            this.btn_ShowAccepted_job.Size = new System.Drawing.Size(107, 24);
            this.btn_ShowAccepted_job.TabIndex = 238;
            this.btn_ShowAccepted_job.Text = "Show";
            this.btn_ShowAccepted_job.Click += new System.EventHandler(this.btn_ShowAccepted_job_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btn_ShowAccepted_job.GetChildAt(0))).Text = "Show";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_ShowAccepted_job.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btn_ShowAccepted_job.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 16);
            this.label1.TabIndex = 235;
            this.label1.Text = "From Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 236;
            this.label2.Text = "To Date";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(5, 525);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(589, 58);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbl_status_0);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 527);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1200, 95);
            this.panel4.TabIndex = 2;
            // 
            // lbl_status_0
            // 
            this.lbl_status_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_status_0.AutoSize = true;
            this.lbl_status_0.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status_0.Location = new System.Drawing.Point(88, 19);
            this.lbl_status_0.Name = "lbl_status_0";
            this.lbl_status_0.Size = new System.Drawing.Size(100, 39);
            this.lbl_status_0.TabIndex = 0;
            this.lbl_status_0.Text = "label4";
            this.lbl_status_0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lbl_status_1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(3, 547);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1200, 75);
            this.panel5.TabIndex = 4;
            // 
            // lbl_status_1
            // 
            this.lbl_status_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_status_1.AutoSize = true;
            this.lbl_status_1.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_status_1.Location = new System.Drawing.Point(79, 18);
            this.lbl_status_1.Name = "lbl_status_1";
            this.lbl_status_1.Size = new System.Drawing.Size(100, 39);
            this.lbl_status_1.TabIndex = 0;
            this.lbl_status_1.Text = "label4";
            this.lbl_status_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmJobPoolReport1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 692);
            this.Controls.Add(this.tabControl1);
            this.FormTitle = "PCO Reports";
            this.Name = "frmJobPoolReport1";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Operator Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.tabControl1, 0);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferredJob.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferredJob)).EndInit();
            this.grdTransferredJob.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAcceptedJob.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAcceptedJob)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_TransferredFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_TranferredTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_export_transferred)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ShowTransferred_job)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_AcceptedFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtp_AcceptedTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_export_accepted)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_ShowAccepted_job)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Telerik.WinControls.UI.RadGridView grdTransferredJob;
        private System.Windows.Forms.Panel panel1;


        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Telerik.WinControls.UI.RadGridView grdAcceptedJob;

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private UI.MyDatePicker dtp_TransferredFrom;
        private UI.MyDatePicker dtp_TranferredTo;
        private Telerik.WinControls.UI.RadButton btn_export_transferred;
        private Telerik.WinControls.UI.RadButton btn_ShowTransferred_job;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private UI.MyDatePicker dtp_AcceptedFrom;
        private UI.MyDatePicker dtp_AcceptedTo;
        private Telerik.WinControls.UI.RadButton btn_export_accepted;
        private Telerik.WinControls.UI.RadButton btn_ShowAccepted_job;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbl_status_0;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbl_status_1;
    }
}

