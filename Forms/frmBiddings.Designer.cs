namespace Taxi_AppMain
{
    partial class frmBiddings
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
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnSaveBooking = new Telerik.WinControls.UI.RadButton();
            this.grdPreBooking = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
            this.grdPendingJobs = new UI.MyGridView();
            this.radPageViewPage2 = new Telerik.WinControls.UI.RadPageViewPage();
            this.grdPreBookings = new UI.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreBooking)).BeginInit();
            this.grdPreBooking.SuspendLayout();
            this.radPageViewPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingJobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingJobs.MasterTemplate)).BeginInit();
            this.radPageViewPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreBookings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreBookings.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.RoyalBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(992, 32);
            this.label1.TabIndex = 108;
            this.label1.Text = "Biddings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.radPanel1.Controls.Add(this.btnSaveBooking);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel1.Location = new System.Drawing.Point(0, 618);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(992, 48);
            this.radPanel1.TabIndex = 112;
            // 
            // btnSaveBooking
            // 
            this.btnSaveBooking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveBooking.Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            this.btnSaveBooking.Location = new System.Drawing.Point(798, 2);
            this.btnSaveBooking.Name = "btnSaveBooking";
            this.btnSaveBooking.Size = new System.Drawing.Size(183, 44);
            this.btnSaveBooking.TabIndex = 220;
            this.btnSaveBooking.Text = "Save and Close";
            this.btnSaveBooking.Click += new System.EventHandler(this.btnSaveBooking_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveBooking.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveBooking.GetChildAt(0))).Text = "Save and Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdPreBooking
            // 
            this.grdPreBooking.Controls.Add(this.radPageViewPage1);
            this.grdPreBooking.Controls.Add(this.radPageViewPage2);
            this.grdPreBooking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPreBooking.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPreBooking.Location = new System.Drawing.Point(0, 32);
            this.grdPreBooking.Name = "grdPreBooking";
            this.grdPreBooking.SelectedPage = this.radPageViewPage1;
            this.grdPreBooking.Size = new System.Drawing.Size(992, 586);
            this.grdPreBooking.TabIndex = 113;
            this.grdPreBooking.Text = "Today\'s Booking";
            // 
            // radPageViewPage1
            // 
            this.radPageViewPage1.Controls.Add(this.grdPendingJobs);
            this.radPageViewPage1.Location = new System.Drawing.Point(10, 41);
            this.radPageViewPage1.Name = "radPageViewPage1";
            this.radPageViewPage1.Size = new System.Drawing.Size(971, 534);
            this.radPageViewPage1.Text = "Today\'s Booking";
            // 
            // grdPendingJobs
            // 
            this.grdPendingJobs.AutoCellFormatting = false;
            this.grdPendingJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPendingJobs.EnableCheckInCheckOut = false;
            this.grdPendingJobs.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdPendingJobs.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdPendingJobs.Location = new System.Drawing.Point(0, 0);
            // 
            // grdPendingJobs
            // 
            this.grdPendingJobs.MasterTemplate.AllowAddNewRow = false;
            this.grdPendingJobs.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdPendingJobs.Name = "grdPendingJobs";
            this.grdPendingJobs.PKFieldColumnName = "";
            this.grdPendingJobs.ShowGroupPanel = false;
            this.grdPendingJobs.ShowImageOnActionButton = true;
            this.grdPendingJobs.Size = new System.Drawing.Size(971, 534);
            this.grdPendingJobs.TabIndex = 112;
            this.grdPendingJobs.Text = "radGridView1";
            // 
            // radPageViewPage2
            // 
            this.radPageViewPage2.Controls.Add(this.grdPreBookings);
            this.radPageViewPage2.Location = new System.Drawing.Point(10, 41);
            this.radPageViewPage2.Name = "radPageViewPage2";
            this.radPageViewPage2.Size = new System.Drawing.Size(971, 614);
            this.radPageViewPage2.Text = "Pre Bookings";
            // 
            // grdPreBookings
            // 
            this.grdPreBookings.AutoCellFormatting = false;
            this.grdPreBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPreBookings.EnableCheckInCheckOut = false;
            this.grdPreBookings.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdPreBookings.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdPreBookings.Location = new System.Drawing.Point(0, 0);
            // 
            // grdPreBookings
            // 
            this.grdPreBookings.MasterTemplate.AllowAddNewRow = false;
            this.grdPreBookings.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdPreBookings.Name = "grdPreBookings";
            this.grdPreBookings.PKFieldColumnName = "";
            this.grdPreBookings.ShowGroupPanel = false;
            this.grdPreBookings.ShowImageOnActionButton = true;
            this.grdPreBookings.Size = new System.Drawing.Size(971, 614);
            this.grdPreBookings.TabIndex = 113;
            this.grdPreBookings.Text = "radGridView1";
            // 
            // frmBiddings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 666);
            this.Controls.Add(this.grdPreBooking);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.label1);
            this.Name = "frmBiddings";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.Text = "Biddings";
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreBooking)).EndInit();
            this.grdPreBooking.ResumeLayout(false);
            this.radPageViewPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingJobs.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingJobs)).EndInit();
            this.radPageViewPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPreBookings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPreBookings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnSaveBooking;
        private Telerik.WinControls.UI.RadPageView grdPreBooking;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage1;
        private UI.MyGridView grdPendingJobs;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage2;
        private UI.MyGridView grdPreBookings;
    }
}