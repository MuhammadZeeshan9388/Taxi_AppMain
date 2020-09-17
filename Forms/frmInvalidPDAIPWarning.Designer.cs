namespace Taxi_AppMain
{
    partial class frmInvalidPDAIPWarning
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
            this.txtHeader = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtListenerIP = new System.Windows.Forms.Label();
            this.txtOfficeIP = new System.Windows.Forms.Label();
            this.txtMismatch = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtHeader
            // 
            this.txtHeader.BackColor = System.Drawing.Color.Yellow;
            this.txtHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtHeader.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHeader.ForeColor = System.Drawing.Color.Black;
            this.txtHeader.Image = global::Taxi_AppMain.Properties.Resources.alertjob;
            this.txtHeader.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.txtHeader.Location = new System.Drawing.Point(0, 0);
            this.txtHeader.Name = "txtHeader";
            this.txtHeader.Size = new System.Drawing.Size(714, 51);
            this.txtHeader.TabIndex = 0;
            this.txtHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Black;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(289, 204);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 52);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(0, 292);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(714, 24);
            this.label5.TabIndex = 30;
            this.label5.Text = "Note: After putting Listener IP ,please re-login Dispatch System from all the sys" +
    "tems.";
            // 
            // txtListenerIP
            // 
            this.txtListenerIP.AutoSize = true;
            this.txtListenerIP.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtListenerIP.ForeColor = System.Drawing.Color.Green;
            this.txtListenerIP.Location = new System.Drawing.Point(37, 96);
            this.txtListenerIP.Name = "txtListenerIP";
            this.txtListenerIP.Size = new System.Drawing.Size(253, 19);
            this.txtListenerIP.TabIndex = 31;
            this.txtListenerIP.Text = "Dispatch System Listener IP : ";
            this.txtListenerIP.Visible = false;
            // 
            // txtOfficeIP
            // 
            this.txtOfficeIP.AutoSize = true;
            this.txtOfficeIP.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOfficeIP.ForeColor = System.Drawing.Color.Green;
            this.txtOfficeIP.Location = new System.Drawing.Point(120, 138);
            this.txtOfficeIP.Name = "txtOfficeIP";
            this.txtOfficeIP.Size = new System.Drawing.Size(168, 19);
            this.txtOfficeIP.TabIndex = 32;
            this.txtOfficeIP.Text = "Internet Static IP : ";
            this.txtOfficeIP.Visible = false;
            // 
            // txtMismatch
            // 
            this.txtMismatch.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.txtMismatch.ForeColor = System.Drawing.Color.Red;
            this.txtMismatch.Image = global::Taxi_AppMain.Properties.Resources.rejectJob;
            this.txtMismatch.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.txtMismatch.Location = new System.Drawing.Point(513, 96);
            this.txtMismatch.Name = "txtMismatch";
            this.txtMismatch.Size = new System.Drawing.Size(168, 73);
            this.txtMismatch.TabIndex = 33;
            this.txtMismatch.Text = "IP Mismatch";
            this.txtMismatch.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.txtMismatch.Visible = false;
            // 
            // frmInvalidPDAIPWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(714, 316);
            this.ControlBox = false;
            this.Controls.Add(this.txtMismatch);
            this.Controls.Add(this.txtOfficeIP);
            this.Controls.Add(this.txtListenerIP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmInvalidPDAIPWarning";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAccJobsReminder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label txtListenerIP;
        private System.Windows.Forms.Label txtOfficeIP;
        private System.Windows.Forms.Label txtMismatch;
    }
}