namespace Taxi_AppMain
{
    partial class frmUnProcessedJobsList
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
            this.grdLister = new Telerik.WinControls.UI.RadGridView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.ddlPaymentType = new Telerik.WinControls.UI.RadDropDownList();
            this.btnProcessAllJobs = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlDriver = new Telerik.WinControls.UI.RadDropDownList();
            this.dtpTillDate = new UI.MyDatePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dtpFromDate = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.btnRefresh = new Telerik.WinControls.UI.RadButton();
            this.chkAllAcc = new Telerik.WinControls.UI.RadCheckBox();
            this.ddlCompany = new Telerik.WinControls.UI.RadDropDownList();
            this.optAccount = new Telerik.WinControls.UI.RadRadioButton();
            this.optBoth = new Telerik.WinControls.UI.RadRadioButton();
            this.optCash = new Telerik.WinControls.UI.RadRadioButton();
            this.btnSelected = new Telerik.WinControls.UI.RadButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPaymentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProcessAllJobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllAcc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optBoth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optCash)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdLister
            // 
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLister.Location = new System.Drawing.Point(0, 110);
            this.grdLister.Name = "grdLister";
            this.grdLister.Size = new System.Drawing.Size(1183, 670);
            this.grdLister.TabIndex = 109;
            this.grdLister.Text = "myGridView1";
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.radLabel4);
            this.radPanel1.Controls.Add(this.ddlPaymentType);
            this.radPanel1.Controls.Add(this.btnProcessAllJobs);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Controls.Add(this.ddlDriver);
            this.radPanel1.Controls.Add(this.dtpTillDate);
            this.radPanel1.Controls.Add(this.radLabel3);
            this.radPanel1.Controls.Add(this.dtpFromDate);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Controls.Add(this.btnRefresh);
            this.radPanel1.Controls.Add(this.chkAllAcc);
            this.radPanel1.Controls.Add(this.ddlCompany);
            this.radPanel1.Controls.Add(this.optAccount);
            this.radPanel1.Controls.Add(this.optBoth);
            this.radPanel1.Controls.Add(this.optCash);
            this.radPanel1.Controls.Add(this.btnSelected);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1183, 72);
            this.radPanel1.TabIndex = 108;
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(697, 6);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(105, 22);
            this.radLabel4.TabIndex = 119;
            this.radLabel4.Text = "Payment Type";
            // 
            // ddlPaymentType
            // 
            this.ddlPaymentType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlPaymentType.Location = new System.Drawing.Point(806, 4);
            this.ddlPaymentType.Name = "ddlPaymentType";
            this.ddlPaymentType.Size = new System.Drawing.Size(161, 26);
            this.ddlPaymentType.TabIndex = 118;

            // 
            // btnProcessAllJobs
            // 
            this.btnProcessAllJobs.Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            this.btnProcessAllJobs.Location = new System.Drawing.Point(806, 33);
            this.btnProcessAllJobs.Margin = new System.Windows.Forms.Padding(10, 3, 3, 10);
            this.btnProcessAllJobs.Name = "btnProcessAllJobs";
            this.btnProcessAllJobs.Size = new System.Drawing.Size(161, 38);
            this.btnProcessAllJobs.TabIndex = 117;
            this.btnProcessAllJobs.Text = "Process All Jobs";
            this.btnProcessAllJobs.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnProcessAllJobs.TextWrap = true;
            this.btnProcessAllJobs.Click += new System.EventHandler(this.btnProcessAllJobs_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnProcessAllJobs.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnProcessAllJobs.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnProcessAllJobs.GetChildAt(0))).Text = "Process All Jobs";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnProcessAllJobs.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnProcessAllJobs.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnProcessAllJobs.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(411, 8);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(48, 22);
            this.radLabel1.TabIndex = 116;
            this.radLabel1.Text = "Driver";
            // 
            // ddlDriver
            // 
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(459, 6);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Size = new System.Drawing.Size(235, 26);
            this.ddlDriver.TabIndex = 115;
            this.ddlDriver.Enter += new System.EventHandler(this.ddlDriver_Enter);
            // 
            // dtpTillDate
            // 
            this.dtpTillDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpTillDate.CustomFormat = "dd/MM/yyyy";
            this.dtpTillDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTillDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTillDate.Location = new System.Drawing.Point(291, 7);
            this.dtpTillDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTillDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Name = "dtpTillDate";
            this.dtpTillDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTillDate.Size = new System.Drawing.Size(114, 24);
            this.dtpTillDate.TabIndex = 113;
            this.dtpTillDate.TabStop = false;
            this.dtpTillDate.Text = "myDatePicker2";
            this.dtpTillDate.Value = null;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(220, 9);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(62, 22);
            this.radLabel3.TabIndex = 112;
            this.radLabel3.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Culture = new System.Globalization.CultureInfo("en-GB");
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromDate.Location = new System.Drawing.Point(101, 7);
            this.dtpFromDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromDate.Size = new System.Drawing.Size(114, 24);
            this.dtpFromDate.TabIndex = 111;
            this.dtpFromDate.TabStop = false;
            this.dtpFromDate.Text = "myDatePicker1";
            this.dtpFromDate.Value = null;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(12, 9);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(79, 22);
            this.radLabel2.TabIndex = 110;
            this.radLabel2.Text = "From Date";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Taxi_AppMain.Properties.Resources.glossary_toolbar_find;
            this.btnRefresh.Location = new System.Drawing.Point(639, 33);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(10, 3, 3, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(121, 38);
            this.btnRefresh.TabIndex = 105;
            this.btnRefresh.Text = "Search Jobs";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRefresh.TextWrap = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRefresh.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.glossary_toolbar_find;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRefresh.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnRefresh.GetChildAt(0))).Text = "Search Jobs";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnRefresh.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnRefresh.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnRefresh.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkAllAcc
            // 
            this.chkAllAcc.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkAllAcc.Location = new System.Drawing.Point(539, 41);
            this.chkAllAcc.Name = "chkAllAcc";
            this.chkAllAcc.Size = new System.Drawing.Size(86, 18);
            this.chkAllAcc.TabIndex = 104;
            this.chkAllAcc.Text = "All Accounts";
            this.chkAllAcc.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkAllAcc_ToggleStateChanged);
            // 
            // ddlCompany
            // 
            this.ddlCompany.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlCompany.Location = new System.Drawing.Point(278, 38);
            this.ddlCompany.Name = "ddlCompany";
            this.ddlCompany.Size = new System.Drawing.Size(250, 26);
            this.ddlCompany.TabIndex = 100;
            // 
            // optAccount
            // 
            this.optAccount.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optAccount.Location = new System.Drawing.Point(171, 41);
            this.optAccount.Name = "optAccount";
            this.optAccount.Size = new System.Drawing.Size(99, 18);
            this.optAccount.TabIndex = 96;
            this.optAccount.Text = "Account";
            // 
            // optBoth
            // 
            this.optBoth.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBoth.Location = new System.Drawing.Point(9, 41);
            this.optBoth.Name = "optBoth";
            this.optBoth.Size = new System.Drawing.Size(60, 18);
            this.optBoth.TabIndex = 98;
            this.optBoth.Text = "All";
            this.optBoth.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // optCash
            // 
            this.optCash.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCash.Location = new System.Drawing.Point(82, 41);
            this.optCash.Name = "optCash";
            this.optCash.Size = new System.Drawing.Size(92, 18);
            this.optCash.TabIndex = 97;
            this.optCash.Text = "Cash";
            // 
            // btnSelected
            // 
            this.btnSelected.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelected.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSelected.Location = new System.Drawing.Point(1040, 0);
            this.btnSelected.Margin = new System.Windows.Forms.Padding(10, 3, 3, 10);
            this.btnSelected.Name = "btnSelected";
            this.btnSelected.Size = new System.Drawing.Size(143, 72);
            this.btnSelected.TabIndex = 0;
            this.btnSelected.Text = "Save And Ready Selected Jobs";
            this.btnSelected.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSelected.TextWrap = true;
            this.btnSelected.Click += new System.EventHandler(this.btnDeleteSelected_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSelected.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSelected.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSelected.GetChildAt(0))).Text = "Save And Ready Selected Jobs";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelected.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelected.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSelected.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel Files (.xls)|*.xls|Advanced Excel Files (.xlsx)|*.xlsx";
            this.saveFileDialog1.FilterIndex = 0;
            // 
            // frmUnProcessedJobsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 780);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.radPanel1);
            this.FormTitle = "UnProcessed Bookings";
            this.Name = "frmUnProcessedJobsList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "UnProcessed Bookings";
            this.Load += new System.EventHandler(this.frmUnProcessedJobsList_Load);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlPaymentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnProcessAllJobs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTillDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllAcc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optBoth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optCash)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdLister;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnSelected;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private Telerik.WinControls.UI.RadRadioButton optAccount;
        private Telerik.WinControls.UI.RadRadioButton optBoth;
        private Telerik.WinControls.UI.RadRadioButton optCash;
        private Telerik.WinControls.UI.RadDropDownList ddlCompany;
        private Telerik.WinControls.UI.RadCheckBox chkAllAcc;
        private Telerik.WinControls.UI.RadButton btnRefresh;
        private UI.MyDatePicker dtpTillDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private UI.MyDatePicker dtpFromDate;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList ddlDriver;
        private Telerik.WinControls.UI.RadButton btnProcessAllJobs;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadDropDownList ddlPaymentType;
    }
}