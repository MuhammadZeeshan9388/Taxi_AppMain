namespace Taxi_AppMain
{
    partial class frmDespatchPreBooking
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
            this.lblDespatchHeading = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddl_Driver = new Telerik.WinControls.UI.RadDropDownList();
            this.btnDespatch = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.grdShifts = new Telerik.WinControls.UI.RadGridView();
            this.lblNocMessage = new Telerik.WinControls.UI.RadLabel();
            this.lblJobPickupTime = new System.Windows.Forms.Label();
            this.lblWarning = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDespatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNocMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDespatchHeading
            // 
            this.lblDespatchHeading.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblDespatchHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDespatchHeading.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblDespatchHeading.ForeColor = System.Drawing.Color.White;
            this.lblDespatchHeading.Location = new System.Drawing.Point(0, 0);
            this.lblDespatchHeading.Name = "lblDespatchHeading";
            this.lblDespatchHeading.Size = new System.Drawing.Size(672, 28);
            this.lblDespatchHeading.TabIndex = 0;
            this.lblDespatchHeading.Text = "Dispatch Pre-Booking";
            this.lblDespatchHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Driver";
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(112, 166);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(259, 23);
            this.ddl_Driver.TabIndex = 2;
            this.ddl_Driver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddl_Driver_SelectedIndexChanged);
            // 
            // btnDespatch
            // 
            this.btnDespatch.Location = new System.Drawing.Point(176, 242);
            this.btnDespatch.Name = "btnDespatch";
            this.btnDespatch.Size = new System.Drawing.Size(189, 41);
            this.btnDespatch.TabIndex = 5;
            this.btnDespatch.Text = "Dispatch as Future Job";
            this.btnDespatch.Click += new System.EventHandler(this.btnDespatch_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDespatch.GetChildAt(0))).Text = "Dispatch as Future Job";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDespatch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDespatch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(387, 51);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(285, 24);
            this.radLabel1.TabIndex = 6;
            this.radLabel1.Text = "Driver Availability";
            // 
            // grdShifts
            // 
            this.grdShifts.Location = new System.Drawing.Point(386, 76);
            this.grdShifts.Name = "grdShifts";
            this.grdShifts.Size = new System.Drawing.Size(286, 211);
            this.grdShifts.TabIndex = 7;
            this.grdShifts.Text = "radGridView1";
            // 
            // lblNocMessage
            // 
            this.lblNocMessage.AutoSize = false;
            this.lblNocMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNocMessage.ForeColor = System.Drawing.Color.Red;
            this.lblNocMessage.Location = new System.Drawing.Point(9, 53);
            this.lblNocMessage.Name = "lblNocMessage";
            // 
            // 
            // 
            this.lblNocMessage.RootElement.ForeColor = System.Drawing.Color.Red;
            this.lblNocMessage.Size = new System.Drawing.Size(371, 107);
            this.lblNocMessage.TabIndex = 8;
            // 
            // lblJobPickupTime
            // 
            this.lblJobPickupTime.BackColor = System.Drawing.Color.AntiqueWhite;
            this.lblJobPickupTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblJobPickupTime.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblJobPickupTime.ForeColor = System.Drawing.Color.Red;
            this.lblJobPickupTime.Location = new System.Drawing.Point(0, 28);
            this.lblJobPickupTime.Name = "lblJobPickupTime";
            this.lblJobPickupTime.Size = new System.Drawing.Size(672, 18);
            this.lblJobPickupTime.TabIndex = 9;
            this.lblJobPickupTime.Text = "Job Pickup @ 1/3/2014  11:44";
            this.lblJobPickupTime.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblWarning
            // 
            this.lblWarning.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblWarning.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Location = new System.Drawing.Point(113, 198);
            this.lblWarning.Name = "lblWarning";
            // 
            // 
            // 
            this.lblWarning.RootElement.ForeColor = System.Drawing.Color.Red;
            this.lblWarning.Size = new System.Drawing.Size(246, 16);
            this.lblWarning.TabIndex = 10;
            this.lblWarning.Text = "This driver may not be available for this job";
            this.lblWarning.Visible = false;
            // 
            // frmDespatchPreBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(672, 299);
            this.Controls.Add(this.lblWarning);
            this.Controls.Add(this.lblJobPickupTime);
            this.Controls.Add(this.lblNocMessage);
            this.Controls.Add(this.grdShifts);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.btnDespatch);
            this.Controls.Add(this.ddl_Driver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDespatchHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmDespatchPreBooking";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dispatch Pre-Booking";
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDespatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNocMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDespatchHeading;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadDropDownList ddl_Driver;
        private Telerik.WinControls.UI.RadButton btnDespatch;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadGridView grdShifts;
        private Telerik.WinControls.UI.RadLabel lblNocMessage;
        private System.Windows.Forms.Label lblJobPickupTime;
        private Telerik.WinControls.UI.RadLabel lblWarning;
    }
}