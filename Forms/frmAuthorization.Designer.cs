namespace Taxi_AppMain
{
    partial class frmAuthorization
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
            this.txtAction = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.txtDriver = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPickupPoint = new System.Windows.Forms.Label();
            this.txtDestination = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtTimer = new Telerik.WinControls.UI.RadLabel();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnTrackDriver = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAction
            // 
            this.txtAction.BackColor = System.Drawing.Color.Crimson;
            this.txtAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtAction.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.txtAction.ForeColor = System.Drawing.Color.White;
            this.txtAction.Location = new System.Drawing.Point(0, 0);
            this.txtAction.Name = "txtAction";
            this.txtAction.Size = new System.Drawing.Size(458, 40);
            this.txtAction.TabIndex = 14;
            this.txtAction.Text = "Job No Pickup Authorization";
            this.txtAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.Green;
            this.btnYes.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(48, 250);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(120, 52);
            this.btnYes.TabIndex = 15;
            this.btnYes.Text = "&ALLOW";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.Red;
            this.btnNo.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.Location = new System.Drawing.Point(282, 250);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(120, 52);
            this.btnNo.TabIndex = 16;
            this.btnNo.Text = "&DENY";
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // txtDriver
            // 
            this.txtDriver.BackColor = System.Drawing.SystemColors.MenuBar;
            this.txtDriver.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtDriver.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDriver.Location = new System.Drawing.Point(0, 40);
            this.txtDriver.Name = "txtDriver";
            this.txtDriver.Size = new System.Drawing.Size(458, 22);
            this.txtDriver.TabIndex = 17;
            this.txtDriver.Text = "Driver : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(13, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 18);
            this.label2.TabIndex = 18;
            this.label2.Text = "Pickup :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(14, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Destination :";
            // 
            // txtPickupPoint
            // 
            this.txtPickupPoint.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtPickupPoint.Location = new System.Drawing.Point(14, 90);
            this.txtPickupPoint.Name = "txtPickupPoint";
            this.txtPickupPoint.Size = new System.Drawing.Size(444, 48);
            this.txtPickupPoint.TabIndex = 20;
            // 
            // txtDestination
            // 
            this.txtDestination.Font = new System.Drawing.Font("Tahoma", 11F);
            this.txtDestination.Location = new System.Drawing.Point(15, 170);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(443, 63);
            this.txtDestination.TabIndex = 21;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtTimer
            // 
            this.txtTimer.BackColor = System.Drawing.Color.Crimson;
            this.txtTimer.Font = new System.Drawing.Font("Tahoma", 13F, System.Drawing.FontStyle.Bold);
            this.txtTimer.ForeColor = System.Drawing.Color.White;
            this.txtTimer.Location = new System.Drawing.Point(428, 9);
            this.txtTimer.Name = "txtTimer";
            // 
            // 
            // 
            this.txtTimer.RootElement.ForeColor = System.Drawing.Color.White;
            this.txtTimer.Size = new System.Drawing.Size(31, 25);
            this.txtTimer.TabIndex = 136;
            this.txtTimer.Text = "60";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnTrackDriver
            // 
            this.btnTrackDriver.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnTrackDriver.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.btnTrackDriver.ForeColor = System.Drawing.Color.Black;
            this.btnTrackDriver.Location = new System.Drawing.Point(342, 40);
            this.btnTrackDriver.Name = "btnTrackDriver";
            this.btnTrackDriver.Size = new System.Drawing.Size(116, 22);
            this.btnTrackDriver.TabIndex = 137;
            this.btnTrackDriver.Text = "&TRACK DRIVER";
            this.btnTrackDriver.UseVisualStyleBackColor = false;
            this.btnTrackDriver.Click += new System.EventHandler(this.btnTrackDriver_Click);
            // 
            // frmAuthorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(458, 301);
            this.ControlBox = false;
            this.Controls.Add(this.btnTrackDriver);
            this.Controls.Add(this.txtTimer);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtPickupPoint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDriver);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.txtAction);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAuthorization";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.txtTimer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtAction;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label txtDriver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtPickupPoint;
        private System.Windows.Forms.Label txtDestination;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadLabel txtTimer;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnTrackDriver;
    }
}