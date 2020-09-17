namespace Taxi_AppMain
{
    partial class frmTodaysBooking
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
            this.grdPendingJobs = new UI.MyGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingJobs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingJobs.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdPendingJobs
            // 
            this.grdPendingJobs.AutoCellFormatting = false;
            this.grdPendingJobs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPendingJobs.EnableCheckInCheckOut = false;
            this.grdPendingJobs.EnableHotTracking = false;
            this.grdPendingJobs.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdPendingJobs.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdPendingJobs.Location = new System.Drawing.Point(0, 56);
            this.grdPendingJobs.Name = "grdPendingJobs";
            this.grdPendingJobs.PKFieldColumnName = "";
            this.grdPendingJobs.ShowImageOnActionButton = true;
            this.grdPendingJobs.Size = new System.Drawing.Size(1192, 556);
            this.grdPendingJobs.TabIndex = 5;
            this.grdPendingJobs.Text = "myGridView1";
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.Coral;
            this.radPanel1.Controls.Add(this.radButton1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPanel1.ForeColor = System.Drawing.Color.White;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            // 
            // 
            // 
            this.radPanel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radPanel1.Size = new System.Drawing.Size(1192, 56);
            this.radPanel1.TabIndex = 6;
            this.radPanel1.Text = "Today\'s Booking";
            this.radPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radButton1
            // 
            this.radButton1.Dock = System.Windows.Forms.DockStyle.Right;
            this.radButton1.Image = global::Taxi_AppMain.Properties.Resources.refresh;
            this.radButton1.Location = new System.Drawing.Point(997, 0);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(195, 56);
            this.radButton1.TabIndex = 0;
            this.radButton1.Text = "Refresh Jobs";
            this.radButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.radButton1.Click += new System.EventHandler(this.btnRefreshJobs_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.refresh;
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton1.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.radButton1.GetChildAt(0))).Text = "Refresh Jobs";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radButton1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmTodaysBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 612);
            this.Controls.Add(this.grdPendingJobs);
            this.Controls.Add(this.radPanel1);
            this.Name = "frmTodaysBooking";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.Text = "Today\'s Booking";
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingJobs.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPendingJobs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MyGridView grdPendingJobs;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton radButton1;
    }
}