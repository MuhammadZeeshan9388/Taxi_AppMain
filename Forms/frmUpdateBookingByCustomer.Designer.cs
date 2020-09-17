namespace Taxi_AppMain.Forms
{
    partial class frmUpdateBookingByCustomer
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
            this.txtDestination = new System.Windows.Forms.Label();
            this.txtPickupPoint = new System.Windows.Forms.Label();
            this.txtUpdateString = new System.Windows.Forms.Label();
            this.txtCustomer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnYes = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.btnViewJob = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDestination
            // 
            this.txtDestination.AutoSize = true;
            this.txtDestination.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDestination.Location = new System.Drawing.Point(15, 184);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(0, 19);
            this.txtDestination.TabIndex = 29;
            // 
            // txtPickupPoint
            // 
            this.txtPickupPoint.AutoSize = true;
            this.txtPickupPoint.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPickupPoint.Location = new System.Drawing.Point(14, 104);
            this.txtPickupPoint.Name = "txtPickupPoint";
            this.txtPickupPoint.Size = new System.Drawing.Size(0, 19);
            this.txtPickupPoint.TabIndex = 28;
            // 
            // txtUpdateString
            // 
            this.txtUpdateString.AutoSize = true;
            this.txtUpdateString.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUpdateString.Location = new System.Drawing.Point(13, 81);
            this.txtUpdateString.Name = "txtUpdateString";
            this.txtUpdateString.Size = new System.Drawing.Size(0, 19);
            this.txtUpdateString.TabIndex = 26;
            // 
            // txtCustomer
            // 
            this.txtCustomer.BackColor = System.Drawing.Color.SeaShell;
            this.txtCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCustomer.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCustomer.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomer.Location = new System.Drawing.Point(0, 40);
            this.txtCustomer.Name = "txtCustomer";
            this.txtCustomer.Size = new System.Drawing.Size(654, 36);
            this.txtCustomer.TabIndex = 25;
            this.txtCustomer.Text = "Name :";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.Green;
            this.btnYes.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(254, 266);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(120, 52);
            this.btnYes.TabIndex = 23;
            this.btnYes.Text = "OK";
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.Green;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.txtTitle.ForeColor = System.Drawing.Color.White;
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(654, 40);
            this.txtTitle.TabIndex = 22;
            this.txtTitle.Text = "Job Updated By Customer";
            this.txtTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFooter
            // 
            this.lblFooter.BackColor = System.Drawing.Color.SeaShell;
            this.lblFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFooter.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblFooter.Location = new System.Drawing.Point(0, 321);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(654, 18);
            this.lblFooter.TabIndex = 30;
            // 
            // btnViewJob
            // 
            this.btnViewJob.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnViewJob.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnViewJob.ForeColor = System.Drawing.Color.White;
            this.btnViewJob.Location = new System.Drawing.Point(535, 40);
            this.btnViewJob.Name = "btnViewJob";
            this.btnViewJob.Size = new System.Drawing.Size(120, 36);
            this.btnViewJob.TabIndex = 31;
            this.btnViewJob.Text = "View Job";
            this.btnViewJob.UseVisualStyleBackColor = false;
            this.btnViewJob.Click += new System.EventHandler(this.btnViewJob_Click);
            // 
            // frmUpdateBookingByCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(654, 339);
            this.ControlBox = false;
            this.Controls.Add(this.btnViewJob);
            this.Controls.Add(this.lblFooter);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtPickupPoint);
            this.Controls.Add(this.txtUpdateString);
            this.Controls.Add(this.txtCustomer);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.txtTitle);
            this.MaximizeBox = false;
            this.Name = "frmUpdateBookingByCustomer";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtDestination;
        private System.Windows.Forms.Label txtPickupPoint;
        private System.Windows.Forms.Label txtUpdateString;
        private System.Windows.Forms.Label txtCustomer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Label txtTitle;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.Button btnViewJob;
    }
}