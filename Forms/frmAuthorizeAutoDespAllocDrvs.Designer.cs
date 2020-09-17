namespace Taxi_AppMain
{
    partial class frmAuthorizeAutoDespAllocDrvs
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
            this.grdLister = new Telerik.WinControls.UI.RadGridView();
            this.lblHeading = new Telerik.WinControls.UI.RadLabel();
            this.BtnClose = new Telerik.WinControls.UI.RadButton();
            this.txtTimer = new Telerik.WinControls.UI.RadLabel();
            this.btnAcceptAll = new Telerik.WinControls.UI.RadButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeading)).BeginInit();
            this.lblHeading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAcceptAll)).BeginInit();
            this.SuspendLayout();
            // 
            // grdLister
            // 
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableHotTracking = false;
            this.grdLister.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.grdLister.Location = new System.Drawing.Point(0, 62);
            this.grdLister.Name = "grdLister";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.Size = new System.Drawing.Size(884, 381);
            this.grdLister.TabIndex = 117;
            this.grdLister.Text = "myGridView1";
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = false;
            this.lblHeading.BackColor = System.Drawing.Color.Yellow;
            this.lblHeading.Controls.Add(this.BtnClose);
            this.lblHeading.Controls.Add(this.txtTimer);
            this.lblHeading.Controls.Add(this.btnAcceptAll);
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.Red;
            this.lblHeading.Image = global::Taxi_AppMain.Properties.Resources.alertjob;
            this.lblHeading.Location = new System.Drawing.Point(0, 0);
            this.lblHeading.Name = "lblHeading";
            // 
            // 
            // 
            this.lblHeading.RootElement.ForeColor = System.Drawing.Color.Red;
            this.lblHeading.Size = new System.Drawing.Size(884, 62);
            this.lblHeading.TabIndex = 118;
            this.lblHeading.Text = "          Authorization for Unavailability of the Allocated driver";
            this.lblHeading.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnClose
            // 
            this.BtnClose.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.BtnClose.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnClose.Location = new System.Drawing.Point(779, 6);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(102, 50);
            this.BtnClose.TabIndex = 138;
            this.BtnClose.Text = "DENY ALL";
            this.BtnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.BtnClose.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.BtnClose.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.BtnClose.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.BtnClose.GetChildAt(0))).Text = "DENY ALL";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.BtnClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.BtnClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtTimer
            // 
            this.txtTimer.BackColor = System.Drawing.Color.Crimson;
            this.txtTimer.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.txtTimer.ForeColor = System.Drawing.Color.White;
            this.txtTimer.Location = new System.Drawing.Point(9, 23);
            this.txtTimer.Name = "txtTimer";
            // 
            // 
            // 
            this.txtTimer.RootElement.ForeColor = System.Drawing.Color.White;
            this.txtTimer.Size = new System.Drawing.Size(42, 25);
            this.txtTimer.TabIndex = 137;
            this.txtTimer.Text = "300";
            // 
            // btnAcceptAll
            // 
            this.btnAcceptAll.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnAcceptAll.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAcceptAll.Location = new System.Drawing.Point(663, 6);
            this.btnAcceptAll.Name = "btnAcceptAll";
            this.btnAcceptAll.Size = new System.Drawing.Size(110, 50);
            this.btnAcceptAll.TabIndex = 5;
            this.btnAcceptAll.Text = "ALLOW ALL";
            this.btnAcceptAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAcceptAll.Click += new System.EventHandler(this.btnAcceptAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAcceptAll.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAcceptAll.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAcceptAll.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAcceptAll.GetChildAt(0))).Text = "ALLOW ALL";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAcceptAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAcceptAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmAuthorizeAutoDespAllocDrvs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 443);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.lblHeading);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmAuthorizeAutoDespAllocDrvs";
            this.ShowIcon = false;
            this.Text = "Authorization";
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeading)).EndInit();
            this.lblHeading.ResumeLayout(false);
            this.lblHeading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAcceptAll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdLister;
        private Telerik.WinControls.UI.RadLabel lblHeading;
        private Telerik.WinControls.UI.RadButton btnAcceptAll;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadLabel txtTimer;
        private Telerik.WinControls.UI.RadButton BtnClose;
    }
}