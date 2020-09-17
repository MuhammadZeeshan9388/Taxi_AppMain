namespace Taxi_AppMain
{
    partial class frmSearchDriver
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
            this.ddl_Driver = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDespatchHeading = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpdate = new Telerik.WinControls.UI.RadButton();
            this.chkRentPaid = new Telerik.WinControls.UI.RadCheckBox();
            this.txtPDAVersion = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtInitialBalance = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMakeModel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtVehicle = new System.Windows.Forms.TextBox();
            this.txtTelNo = new System.Windows.Forms.TextBox();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCallDriver = new Telerik.WinControls.UI.RadButton();
            this.txtStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRentPaid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCallDriver)).BeginInit();
            this.SuspendLayout();
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(134, 36);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(282, 26);
            this.ddl_Driver.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select Driver";
            // 
            // lblDespatchHeading
            // 
            this.lblDespatchHeading.BackColor = System.Drawing.Color.CornflowerBlue;
            this.lblDespatchHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDespatchHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDespatchHeading.ForeColor = System.Drawing.Color.White;
            this.lblDespatchHeading.Image = global::Taxi_AppMain.Properties.Resources.pic_Search;
            this.lblDespatchHeading.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDespatchHeading.Location = new System.Drawing.Point(0, 0);
            this.lblDespatchHeading.Name = "lblDespatchHeading";
            this.lblDespatchHeading.Size = new System.Drawing.Size(553, 24);
            this.lblDespatchHeading.TabIndex = 9;
            this.lblDespatchHeading.Text = "Search Driver";
            this.lblDespatchHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Lavender;
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.chkRentPaid);
            this.groupBox1.Controls.Add(this.txtPDAVersion);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtInitialBalance);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtMakeModel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtVehicle);
            this.groupBox1.Controls.Add(this.txtTelNo);
            this.groupBox1.Controls.Add(this.txtMobileNo);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(529, 368);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(325, 336);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(130, 24);
            this.btnUpdate.TabIndex = 25;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnUpdate.GetChildAt(0))).Text = "Update";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnUpdate.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnUpdate.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // chkRentPaid
            // 
            this.chkRentPaid.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRentPaid.Location = new System.Drawing.Point(9, 338);
            this.chkRentPaid.Name = "chkRentPaid";
            this.chkRentPaid.Size = new System.Drawing.Size(97, 22);
            this.chkRentPaid.TabIndex = 24;
            this.chkRentPaid.Text = "Rent Paid";
            // 
            // txtPDAVersion
            // 
            this.txtPDAVersion.BackColor = System.Drawing.Color.GhostWhite;
            this.txtPDAVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPDAVersion.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPDAVersion.Location = new System.Drawing.Point(120, 229);
            this.txtPDAVersion.Name = "txtPDAVersion";
            this.txtPDAVersion.ReadOnly = true;
            this.txtPDAVersion.Size = new System.Drawing.Size(335, 26);
            this.txtPDAVersion.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(8, 231);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 18);
            this.label9.TabIndex = 22;
            this.label9.Text = "PDA Version :";
            // 
            // txtInitialBalance
            // 
            this.txtInitialBalance.BackColor = System.Drawing.Color.GhostWhite;
            this.txtInitialBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInitialBalance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInitialBalance.Location = new System.Drawing.Point(120, 264);
            this.txtInitialBalance.Name = "txtInitialBalance";
            this.txtInitialBalance.ReadOnly = true;
            this.txtInitialBalance.Size = new System.Drawing.Size(335, 26);
            this.txtInitialBalance.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 266);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 45);
            this.label8.TabIndex = 20;
            this.label8.Text = "Initial Balance :";
            // 
            // txtMakeModel
            // 
            this.txtMakeModel.BackColor = System.Drawing.Color.GhostWhite;
            this.txtMakeModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMakeModel.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMakeModel.Location = new System.Drawing.Point(121, 195);
            this.txtMakeModel.Name = "txtMakeModel";
            this.txtMakeModel.ReadOnly = true;
            this.txtMakeModel.Size = new System.Drawing.Size(335, 26);
            this.txtMakeModel.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(9, 197);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 18);
            this.label7.TabIndex = 18;
            this.label7.Text = "Make/Model :";
            // 
            // txtVehicle
            // 
            this.txtVehicle.BackColor = System.Drawing.Color.GhostWhite;
            this.txtVehicle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVehicle.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicle.Location = new System.Drawing.Point(121, 161);
            this.txtVehicle.Name = "txtVehicle";
            this.txtVehicle.ReadOnly = true;
            this.txtVehicle.Size = new System.Drawing.Size(335, 26);
            this.txtVehicle.TabIndex = 17;
            // 
            // txtTelNo
            // 
            this.txtTelNo.BackColor = System.Drawing.Color.GhostWhite;
            this.txtTelNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelNo.Location = new System.Drawing.Point(121, 126);
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.ReadOnly = true;
            this.txtTelNo.Size = new System.Drawing.Size(335, 26);
            this.txtTelNo.TabIndex = 16;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.BackColor = System.Drawing.Color.GhostWhite;
            this.txtMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMobileNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(121, 92);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.ReadOnly = true;
            this.txtMobileNo.Size = new System.Drawing.Size(335, 26);
            this.txtMobileNo.TabIndex = 15;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.GhostWhite;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(121, 57);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(335, 26);
            this.txtEmail.TabIndex = 14;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.GhostWhite;
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(121, 20);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(335, 26);
            this.txtName.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 18);
            this.label6.TabIndex = 11;
            this.label6.Text = "Vehicle :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 18);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tel No :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Mobile No :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "Email :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "Name :";
            // 
            // btnCallDriver
            // 
            this.btnCallDriver.Image = global::Taxi_AppMain.Properties.Resources.callerid5;
            this.btnCallDriver.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnCallDriver.Location = new System.Drawing.Point(446, 27);
            this.btnCallDriver.Name = "btnCallDriver";
            this.btnCallDriver.Size = new System.Drawing.Size(107, 67);
            this.btnCallDriver.TabIndex = 26;
            this.btnCallDriver.Text = "Call Driver";
            this.btnCallDriver.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCallDriver.Visible = false;
            this.btnCallDriver.Click += new System.EventHandler(this.btnCallDriver_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCallDriver.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.callerid5;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCallDriver.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCallDriver.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCallDriver.GetChildAt(0))).Text = "Call Driver";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCallDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCallDriver.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtStatus
            // 
            this.txtStatus.AutoSize = true;
            this.txtStatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.txtStatus.Location = new System.Drawing.Point(25, 67);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(0, 19);
            this.txtStatus.TabIndex = 27;
            // 
            // frmSearchDriver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(553, 468);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnCallDriver);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblDespatchHeading);
            this.Controls.Add(this.ddl_Driver);
            this.Controls.Add(this.label2);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmSearchDriver";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Driver";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTrackDriver_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkRentPaid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCallDriver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList ddl_Driver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDespatchHeading;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVehicle;
        private System.Windows.Forms.TextBox txtTelNo;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInitialBalance;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMakeModel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPDAVersion;
        private System.Windows.Forms.Label label9;
        private Telerik.WinControls.UI.RadButton btnUpdate;
        private Telerik.WinControls.UI.RadCheckBox chkRentPaid;
        private Telerik.WinControls.UI.RadButton btnCallDriver;
        private System.Windows.Forms.Label txtStatus;
    }
}