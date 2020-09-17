namespace Taxi_AppMain
{
    partial class frmFetchedOnlineBookingsPopup
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
            this.lblHeading = new Telerik.WinControls.UI.RadLabel();
            this.btnAcceptAll = new Telerik.WinControls.UI.RadButton();
            this.btnDeclineAll = new Telerik.WinControls.UI.RadButton();
            this.lblAccountName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeading)).BeginInit();
            this.lblHeading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAcceptAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeclineAll)).BeginInit();
            this.SuspendLayout();
            // 
            // grdLister
            // 
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableHotTracking = false;
            this.grdLister.Location = new System.Drawing.Point(0, 62);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.Size = new System.Drawing.Size(1242, 381);
            this.grdLister.TabIndex = 117;
            this.grdLister.Text = "myGridView1";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = false;
            this.lblHeading.BackColor = System.Drawing.Color.Lavender;
            this.lblHeading.Controls.Add(this.lblAccountName);
            this.lblHeading.Controls.Add(this.btnAcceptAll);
            this.lblHeading.Controls.Add(this.btnDeclineAll);
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.Image = global::Taxi_AppMain.Properties.Resources.gnome_unknown;
            this.lblHeading.Location = new System.Drawing.Point(0, 0);
            this.lblHeading.Name = "lblHeading";
            // 
            // 
            // 
            this.lblHeading.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.Size = new System.Drawing.Size(1242, 62);
            this.lblHeading.TabIndex = 118;
            this.lblHeading.Text = "                      Online Bookings Authorization";
            this.lblHeading.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAcceptAll
            // 
            this.btnAcceptAll.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnAcceptAll.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAcceptAll.Location = new System.Drawing.Point(959, 6);
            this.btnAcceptAll.Name = "btnAcceptAll";
            this.btnAcceptAll.Size = new System.Drawing.Size(110, 50);
            this.btnAcceptAll.TabIndex = 5;
            this.btnAcceptAll.Text = "Accept All";
            this.btnAcceptAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAcceptAll.Click += new System.EventHandler(this.btnAcceptAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAcceptAll.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAcceptAll.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAcceptAll.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAcceptAll.GetChildAt(0))).Text = "Accept All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAcceptAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAcceptAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDeclineAll
            // 
            this.btnDeclineAll.Image = global::Taxi_AppMain.Properties.Resources.remove;
            this.btnDeclineAll.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDeclineAll.Location = new System.Drawing.Point(1125, 6);
            this.btnDeclineAll.Name = "btnDeclineAll";
            this.btnDeclineAll.Size = new System.Drawing.Size(110, 50);
            this.btnDeclineAll.TabIndex = 4;
            this.btnDeclineAll.Text = "Decline All";
            this.btnDeclineAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDeclineAll.Click += new System.EventHandler(this.btnDeclineAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeclineAll.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.remove;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeclineAll.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeclineAll.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeclineAll.GetChildAt(0))).Text = "Decline All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeclineAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeclineAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.BackColor = System.Drawing.Color.Red;
            this.lblAccountName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblAccountName.ForeColor = System.Drawing.Color.White;
            this.lblAccountName.Location = new System.Drawing.Point(144, 40);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(162, 19);
            this.lblAccountName.TabIndex = 6;
            this.lblAccountName.Text = "Account - KARHOO";
            this.lblAccountName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAccountName.Visible = false;
            // 
            // frmFetchedOnlineBookingsPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 443);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.lblHeading);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MaximizeBox = false;
            this.Name = "frmFetchedOnlineBookingsPopup";
            this.ShowIcon = false;
            this.Text = "Online Bookings Authorization";
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeading)).EndInit();
            this.lblHeading.ResumeLayout(false);
            this.lblHeading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAcceptAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeclineAll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdLister;
        private Telerik.WinControls.UI.RadLabel lblHeading;
        private Telerik.WinControls.UI.RadButton btnAcceptAll;
        private Telerik.WinControls.UI.RadButton btnDeclineAll;
        private System.Windows.Forms.Label lblAccountName;
    }
}