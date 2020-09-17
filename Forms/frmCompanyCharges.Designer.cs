namespace Taxi_AppMain
{
    partial class frmCompanyCharges
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompanyCharges));
            this.btnSaveCompanyCharges = new Telerik.WinControls.UI.RadButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new Telerik.WinControls.UI.RadButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkSignature = new System.Windows.Forms.CheckBox();
            this.chkMileage = new System.Windows.Forms.CheckBox();
            this.chkPassenger = new System.Windows.Forms.CheckBox();
            this.chkWaitingTime = new System.Windows.Forms.CheckBox();
            this.chkFares = new System.Windows.Forms.CheckBox();
            this.chkParkingCharges = new System.Windows.Forms.CheckBox();
            this.chkExtraCharges = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.grdCompanyCharges = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveCompanyCharges)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanyCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanyCharges.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveCompanyCharges
            // 
            this.btnSaveCompanyCharges.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveCompanyCharges.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSaveCompanyCharges.Location = new System.Drawing.Point(696, 6);
            this.btnSaveCompanyCharges.Name = "btnSaveCompanyCharges";
            this.btnSaveCompanyCharges.Size = new System.Drawing.Size(98, 45);
            this.btnSaveCompanyCharges.TabIndex = 1;
            this.btnSaveCompanyCharges.Text = "Save ";
            this.btnSaveCompanyCharges.Click += new System.EventHandler(this.btnSaveCompanyCharges_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCompanyCharges.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCompanyCharges.GetChildAt(0))).Text = "Save ";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveCompanyCharges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveCompanyCharges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSaveCompanyCharges);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 518);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(943, 58);
            this.panel2.TabIndex = 107;
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClose.Location = new System.Drawing.Point(817, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 45);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Exit";
            this.btnClose.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(0))).Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClose.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkSignature);
            this.panel1.Controls.Add(this.chkMileage);
            this.panel1.Controls.Add(this.chkPassenger);
            this.panel1.Controls.Add(this.chkWaitingTime);
            this.panel1.Controls.Add(this.chkFares);
            this.panel1.Controls.Add(this.chkParkingCharges);
            this.panel1.Controls.Add(this.chkExtraCharges);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(943, 51);
            this.panel1.TabIndex = 106;
            // 
            // chkSignature
            // 
            this.chkSignature.AutoSize = true;
            this.chkSignature.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSignature.Location = new System.Drawing.Point(309, 27);
            this.chkSignature.Name = "chkSignature";
            this.chkSignature.Size = new System.Drawing.Size(90, 20);
            this.chkSignature.TabIndex = 6;
            this.chkSignature.Text = "Signature";
            this.chkSignature.UseVisualStyleBackColor = true;
            this.chkSignature.CheckedChanged += new System.EventHandler(this.chkSignature_CheckedChanged);
            // 
            // chkMileage
            // 
            this.chkMileage.AutoSize = true;
            this.chkMileage.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMileage.Location = new System.Drawing.Point(148, 2);
            this.chkMileage.Name = "chkMileage";
            this.chkMileage.Size = new System.Drawing.Size(76, 20);
            this.chkMileage.TabIndex = 2;
            this.chkMileage.Text = "Mileage";
            this.chkMileage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkMileage.UseVisualStyleBackColor = true;
            this.chkMileage.CheckedChanged += new System.EventHandler(this.chkMileage_CheckedChanged);
            // 
            // chkPassenger
            // 
            this.chkPassenger.AutoSize = true;
            this.chkPassenger.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPassenger.Location = new System.Drawing.Point(215, 27);
            this.chkPassenger.Name = "chkPassenger";
            this.chkPassenger.Size = new System.Drawing.Size(95, 20);
            this.chkPassenger.TabIndex = 5;
            this.chkPassenger.Text = "Passenger";
            this.chkPassenger.UseVisualStyleBackColor = true;
            this.chkPassenger.CheckedChanged += new System.EventHandler(this.chkPassenger_CheckedChanged);
            // 
            // chkWaitingTime
            // 
            this.chkWaitingTime.AutoSize = true;
            this.chkWaitingTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWaitingTime.Location = new System.Drawing.Point(227, 2);
            this.chkWaitingTime.Name = "chkWaitingTime";
            this.chkWaitingTime.Size = new System.Drawing.Size(109, 20);
            this.chkWaitingTime.TabIndex = 1;
            this.chkWaitingTime.Text = "Waiting Time";
            this.chkWaitingTime.UseVisualStyleBackColor = true;
            this.chkWaitingTime.CheckedChanged += new System.EventHandler(this.chkWaitingTime_CheckedChanged);
            // 
            // chkFares
            // 
            this.chkFares.AutoSize = true;
            this.chkFares.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFares.Location = new System.Drawing.Point(148, 27);
            this.chkFares.Name = "chkFares";
            this.chkFares.Size = new System.Drawing.Size(62, 20);
            this.chkFares.TabIndex = 4;
            this.chkFares.Text = "Fares";
            this.chkFares.UseVisualStyleBackColor = true;
            this.chkFares.CheckedChanged += new System.EventHandler(this.chkFares_CheckedChanged);
            // 
            // chkParkingCharges
            // 
            this.chkParkingCharges.AutoSize = true;
            this.chkParkingCharges.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkParkingCharges.Location = new System.Drawing.Point(337, 2);
            this.chkParkingCharges.Name = "chkParkingCharges";
            this.chkParkingCharges.Size = new System.Drawing.Size(132, 20);
            this.chkParkingCharges.TabIndex = 0;
            this.chkParkingCharges.Text = "Parking Charges";
            this.chkParkingCharges.UseVisualStyleBackColor = true;
            this.chkParkingCharges.CheckedChanged += new System.EventHandler(this.chkParkingCharges_CheckedChanged);
            // 
            // chkExtraCharges
            // 
            this.chkExtraCharges.AutoSize = true;
            this.chkExtraCharges.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExtraCharges.Location = new System.Drawing.Point(469, 2);
            this.chkExtraCharges.Name = "chkExtraCharges";
            this.chkExtraCharges.Size = new System.Drawing.Size(118, 20);
            this.chkExtraCharges.TabIndex = 3;
            this.chkExtraCharges.Text = "Extra Charges";
            this.chkExtraCharges.UseVisualStyleBackColor = true;
            this.chkExtraCharges.CheckedChanged += new System.EventHandler(this.chkExtraCharges_CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(23, 3);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(15, 14);
            this.checkBox7.TabIndex = 3;
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // grdCompanyCharges
            // 
            this.grdCompanyCharges.AutoScroll = true;
            this.grdCompanyCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCompanyCharges.Location = new System.Drawing.Point(0, 89);
            this.grdCompanyCharges.Name = "grdCompanyCharges";
            this.grdCompanyCharges.Size = new System.Drawing.Size(943, 429);
            this.grdCompanyCharges.TabIndex = 0;
            this.grdCompanyCharges.Text = "Campany Charges";
            // 
            // frmCompanyCharges
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 576);
            this.ControlBox = true;
            this.Controls.Add(this.grdCompanyCharges);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Account Additional Charges";
            this.Name = "frmCompanyCharges";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Account Additional Charges";
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.grdCompanyCharges, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveCompanyCharges)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanyCharges.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompanyCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnSaveCompanyCharges;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkPassenger;
        private System.Windows.Forms.CheckBox chkFares;
        private System.Windows.Forms.CheckBox chkExtraCharges;
        private System.Windows.Forms.CheckBox chkParkingCharges;
        private System.Windows.Forms.CheckBox chkSignature;
        private System.Windows.Forms.CheckBox checkBox7;
        private Telerik.WinControls.UI.RadGridView grdCompanyCharges;
        private System.Windows.Forms.CheckBox chkMileage;
        private System.Windows.Forms.CheckBox chkWaitingTime;
        private Telerik.WinControls.UI.RadButton btnClose;
    }
}