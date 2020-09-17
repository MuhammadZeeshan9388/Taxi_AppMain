namespace Taxi_AppMain
{
    partial class frmWarning
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
            this.txtListenerIP = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
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
            this.txtHeader.Size = new System.Drawing.Size(528, 51);
            this.txtHeader.TabIndex = 0;
            this.txtHeader.Text = "Tracking Map is not running";
            this.txtHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Black;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(204, 165);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(128, 52);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtListenerIP
            // 
            this.txtListenerIP.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtListenerIP.ForeColor = System.Drawing.Color.Green;
            this.txtListenerIP.Location = new System.Drawing.Point(50, 86);
            this.txtListenerIP.Name = "txtListenerIP";
            this.txtListenerIP.Size = new System.Drawing.Size(431, 59);
            this.txtListenerIP.TabIndex = 31;
            this.txtListenerIP.Text = "For Driver Tracking and PDA Synchronization Double Click on the icon on desktop \"" +
    "Tracking Map\" or press \"OK\" to run it.";
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(0, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(528, 17);
            this.label5.TabIndex = 30;
            this.label5.Text = "Note: Tracking Map must be running all the time on this machine.";
            // 
            // frmWarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(528, 248);
            this.ControlBox = false;
            this.Controls.Add(this.txtListenerIP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmWarning";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAccJobsReminder";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label txtHeader;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label txtListenerIP;
        private System.Windows.Forms.Label label5;
    }
}