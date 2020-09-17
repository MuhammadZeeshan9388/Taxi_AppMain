namespace Taxi_AppMain.Forms
{
    partial class frmPanicAlert
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
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnStop = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMute = new Telerik.WinControls.UI.RadToggleButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.lblDrivers = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMute)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.Image = global::Taxi_AppMain.Properties.Resources.alertjob;
            this.radLabel1.Location = new System.Drawing.Point(36, 75);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(57, 53);
            this.radLabel1.TabIndex = 108;
            // 
            // btnStop
            // 
            this.btnStop.Image = global::Taxi_AppMain.Properties.Resources.resultset_next;
            this.btnStop.Location = new System.Drawing.Point(128, 88);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(97, 36);
            this.btnStop.TabIndex = 107;
            this.btnStop.Text = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click_1);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Crimson;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(278, 35);
            this.label1.TabIndex = 109;
            this.label1.Text = "Panic Alert";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnMute
            // 
            this.btnMute.BackColor = System.Drawing.Color.HotPink;
            this.btnMute.Location = new System.Drawing.Point(226, 0);
            this.btnMute.Name = "btnMute";
            this.btnMute.Size = new System.Drawing.Size(51, 23);
            this.btnMute.TabIndex = 110;
            this.btnMute.Text = "Mute";
            this.btnMute.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.btnMute_ToggleStateChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(2, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(51, 23);
            this.btnCancel.TabIndex = 112;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDrivers
            // 
            this.lblDrivers.AutoSize = true;
            this.lblDrivers.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblDrivers.ForeColor = System.Drawing.Color.Black;
            this.lblDrivers.Location = new System.Drawing.Point(4, 41);
            this.lblDrivers.Name = "lblDrivers";
            this.lblDrivers.Size = new System.Drawing.Size(0, 23);
            this.lblDrivers.TabIndex = 113;
            // 
            // frmPanicAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Pink;
            this.ClientSize = new System.Drawing.Size(278, 134);
            this.ControlBox = false;
            this.Controls.Add(this.lblDrivers);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnMute);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.btnStop);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPanicAlert";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMute)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton btnStop;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadToggleButton btnMute;
        private Telerik.WinControls.UI.RadButton btnCancel;
        public System.Windows.Forms.Label lblDrivers;
    }
}