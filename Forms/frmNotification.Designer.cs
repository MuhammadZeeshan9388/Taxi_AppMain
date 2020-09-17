namespace Taxi_AppMain.Forms
{
    partial class frmNotification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotification));
            this.lblTotalDrivers = new System.Windows.Forms.Label();
            this.btnExpiryDrivers = new Telerik.WinControls.UI.RadButton();
            this.btnDueDrivers = new Telerik.WinControls.UI.RadButton();
            this.grdLister = new UI.MyGridView();
            this.btnSendSMS = new Telerik.WinControls.UI.RadButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExpiryDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDueDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendSMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(84, 858);
            this.btnSaveOn.Size = new System.Drawing.Size(70, 10);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(8, 858);
            this.btnOnNew.Size = new System.Drawing.Size(70, 10);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.Location = new System.Drawing.Point(1102, 0);
            this.btnExit.Size = new System.Drawing.Size(77, 38);
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(236, 858);
            this.btnSaveAndClose.Size = new System.Drawing.Size(79, 10);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(160, 855);
            this.btnSaveAndNew.Size = new System.Drawing.Size(79, 10);
            // 
            // lblTotalDrivers
            // 
            this.lblTotalDrivers.BackColor = System.Drawing.Color.NavajoWhite;
            this.lblTotalDrivers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotalDrivers.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalDrivers.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDrivers.Location = new System.Drawing.Point(0, 38);
            this.lblTotalDrivers.Name = "lblTotalDrivers";
            this.lblTotalDrivers.Size = new System.Drawing.Size(1174, 35);
            this.lblTotalDrivers.TabIndex = 121;
            this.lblTotalDrivers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnExpiryDrivers
            // 
            this.btnExpiryDrivers.Location = new System.Drawing.Point(956, 43);
            this.btnExpiryDrivers.Name = "btnExpiryDrivers";
            this.btnExpiryDrivers.Size = new System.Drawing.Size(130, 24);
            this.btnExpiryDrivers.TabIndex = 1;
            this.btnExpiryDrivers.Text = "Show Expire Drivers";
            this.btnExpiryDrivers.Click += new System.EventHandler(this.btnExpiryDrivers_Click);
            // 
            // btnDueDrivers
            // 
            this.btnDueDrivers.Location = new System.Drawing.Point(792, 44);
            this.btnDueDrivers.Name = "btnDueDrivers";
            this.btnDueDrivers.Size = new System.Drawing.Size(130, 24);
            this.btnDueDrivers.TabIndex = 0;
            this.btnDueDrivers.Text = "Show Due Drivers";
            this.btnDueDrivers.Click += new System.EventHandler(this.btnDueDrivers_Click);
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = true;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.EnableHotTracking = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 73);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(1174, 385);
            this.grdLister.TabIndex = 122;
            this.grdLister.Text = "myGridView1";
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Enabled = false;
            this.btnSendSMS.Image = global::Taxi_AppMain.Properties.Resources.icon_email_png;
            this.btnSendSMS.Location = new System.Drawing.Point(557, 40);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(218, 30);
            this.btnSendSMS.TabIndex = 123;
            this.btnSendSMS.Text = "ReSend Document Expiry SMS";
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10800000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 458);
            this.Controls.Add(this.btnSendSMS);
            this.Controls.Add(this.btnExpiryDrivers);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.btnDueDrivers);
            this.Controls.Add(this.lblTotalDrivers);
            this.FixedExitButtonOnTopRight = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Document Expiry Notification";
            this.Name = "frmNotification";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.Text = "Document Expiry Notification";
            this.ThemeName = "ControlDefault";
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.lblTotalDrivers, 0);
            this.Controls.SetChildIndex(this.btnDueDrivers, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            this.Controls.SetChildIndex(this.btnExpiryDrivers, 0);
            this.Controls.SetChildIndex(this.btnSendSMS, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExpiryDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDueDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendSMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTotalDrivers;
        private Telerik.WinControls.UI.RadButton btnDueDrivers;
        private Telerik.WinControls.UI.RadButton btnExpiryDrivers;
        private UI.MyGridView grdLister;
        private Telerik.WinControls.UI.RadButton btnSendSMS;
        private System.Windows.Forms.Timer timer1;
    }
}
