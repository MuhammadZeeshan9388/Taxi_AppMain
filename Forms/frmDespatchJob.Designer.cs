namespace Taxi_AppMain
{
    partial class frmDespatchJob
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
            this.lblNocMessage = new System.Windows.Forms.Label();
            this.btnDespatch = new Telerik.WinControls.UI.RadButton();
            this.lblCustomerMobNo = new System.Windows.Forms.Label();
            this.lblDriverMobNo = new System.Windows.Forms.Label();
            this.txtCustomerMobNo = new System.Windows.Forms.Label();
            this.txtDriverMobNo = new System.Windows.Forms.Label();
            this.lblSmsError1 = new System.Windows.Forms.Label();
            this.lblSMSError2 = new System.Windows.Forms.Label();
            this.chkSMS = new System.Windows.Forms.CheckBox();
            this.lblLastGPSConn = new System.Windows.Forms.Label();
            this.txtJobDetails = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDespatch)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDespatchHeading
            // 
            this.lblDespatchHeading.BackColor = System.Drawing.Color.LightYellow;
            this.lblDespatchHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDespatchHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDespatchHeading.ForeColor = System.Drawing.Color.Black;
            this.lblDespatchHeading.Location = new System.Drawing.Point(0, 0);
            this.lblDespatchHeading.Name = "lblDespatchHeading";
            this.lblDespatchHeading.Size = new System.Drawing.Size(384, 27);
            this.lblDespatchHeading.TabIndex = 0;
            this.lblDespatchHeading.Text = "Despatch Job";
            this.lblDespatchHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Driver";
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(103, 132);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(259, 23);
            this.ddl_Driver.TabIndex = 2;
            this.ddl_Driver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddl_Driver_SelectedIndexChanged);
            // 
            // lblNocMessage
            // 
            this.lblNocMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNocMessage.Location = new System.Drawing.Point(3, 75);
            this.lblNocMessage.Name = "lblNocMessage";
            this.lblNocMessage.Size = new System.Drawing.Size(380, 53);
            this.lblNocMessage.TabIndex = 4;
            this.lblNocMessage.Visible = false;
            // 
            // btnDespatch
            // 
            this.btnDespatch.Location = new System.Drawing.Point(162, 246);
            this.btnDespatch.Name = "btnDespatch";
            this.btnDespatch.Size = new System.Drawing.Size(130, 41);
            this.btnDespatch.TabIndex = 5;
            this.btnDespatch.Text = "Dispatch";
            this.btnDespatch.Click += new System.EventHandler(this.btnDespatch_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDespatch.GetChildAt(0))).Text = "Dispatch";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDespatch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDespatch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblCustomerMobNo
            // 
            this.lblCustomerMobNo.AutoSize = true;
            this.lblCustomerMobNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerMobNo.Location = new System.Drawing.Point(100, 163);
            this.lblCustomerMobNo.Name = "lblCustomerMobNo";
            this.lblCustomerMobNo.Size = new System.Drawing.Size(124, 14);
            this.lblCustomerMobNo.TabIndex = 6;
            this.lblCustomerMobNo.Text = "Customer Mobile No :";
            this.lblCustomerMobNo.Visible = false;
            // 
            // lblDriverMobNo
            // 
            this.lblDriverMobNo.AutoSize = true;
            this.lblDriverMobNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriverMobNo.Location = new System.Drawing.Point(100, 187);
            this.lblDriverMobNo.Name = "lblDriverMobNo";
            this.lblDriverMobNo.Size = new System.Drawing.Size(103, 14);
            this.lblDriverMobNo.TabIndex = 8;
            this.lblDriverMobNo.Text = "Driver Mobile No :";
            this.lblDriverMobNo.Visible = false;
            // 
            // txtCustomerMobNo
            // 
            this.txtCustomerMobNo.AutoSize = true;
            this.txtCustomerMobNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerMobNo.Location = new System.Drawing.Point(229, 163);
            this.txtCustomerMobNo.Name = "txtCustomerMobNo";
            this.txtCustomerMobNo.Size = new System.Drawing.Size(0, 14);
            this.txtCustomerMobNo.TabIndex = 9;
            this.txtCustomerMobNo.Visible = false;
            // 
            // txtDriverMobNo
            // 
            this.txtDriverMobNo.AutoSize = true;
            this.txtDriverMobNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDriverMobNo.Location = new System.Drawing.Point(229, 187);
            this.txtDriverMobNo.Name = "txtDriverMobNo";
            this.txtDriverMobNo.Size = new System.Drawing.Size(0, 14);
            this.txtDriverMobNo.TabIndex = 10;
            this.txtDriverMobNo.Visible = false;
            // 
            // lblSmsError1
            // 
            this.lblSmsError1.AutoSize = true;
            this.lblSmsError1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSmsError1.ForeColor = System.Drawing.Color.DeepPink;
            this.lblSmsError1.Location = new System.Drawing.Point(16, 49);
            this.lblSmsError1.Name = "lblSmsError1";
            this.lblSmsError1.Size = new System.Drawing.Size(0, 14);
            this.lblSmsError1.TabIndex = 11;
            this.lblSmsError1.Visible = false;
            // 
            // lblSMSError2
            // 
            this.lblSMSError2.AutoSize = true;
            this.lblSMSError2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSMSError2.ForeColor = System.Drawing.Color.DeepPink;
            this.lblSMSError2.Location = new System.Drawing.Point(16, 70);
            this.lblSMSError2.Name = "lblSMSError2";
            this.lblSMSError2.Size = new System.Drawing.Size(0, 14);
            this.lblSMSError2.TabIndex = 12;
            this.lblSMSError2.Visible = false;
            // 
            // chkSMS
            // 
            this.chkSMS.AutoSize = true;
            this.chkSMS.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSMS.Location = new System.Drawing.Point(43, 185);
            this.chkSMS.Name = "chkSMS";
            this.chkSMS.Size = new System.Drawing.Size(53, 18);
            this.chkSMS.TabIndex = 17;
            this.chkSMS.Text = "SMS";
            this.chkSMS.UseVisualStyleBackColor = true;
            this.chkSMS.Visible = false;
            // 
            // lblLastGPSConn
            // 
            this.lblLastGPSConn.AutoSize = true;
            this.lblLastGPSConn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLastGPSConn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastGPSConn.ForeColor = System.Drawing.Color.Red;
            this.lblLastGPSConn.Location = new System.Drawing.Point(0, 300);
            this.lblLastGPSConn.Name = "lblLastGPSConn";
            this.lblLastGPSConn.Size = new System.Drawing.Size(156, 14);
            this.lblLastGPSConn.TabIndex = 18;
            this.lblLastGPSConn.Text = "Last Connection Made : ";
            this.lblLastGPSConn.Visible = false;
            // 
            // txtJobDetails
            // 
            this.txtJobDetails.BackColor = System.Drawing.Color.LightYellow;
            this.txtJobDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJobDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtJobDetails.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.txtJobDetails.ForeColor = System.Drawing.Color.Blue;
            this.txtJobDetails.Location = new System.Drawing.Point(0, 27);
            this.txtJobDetails.Name = "txtJobDetails";
            this.txtJobDetails.Size = new System.Drawing.Size(384, 44);
            this.txtJobDetails.TabIndex = 19;
            this.txtJobDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmDespatchJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(384, 314);
            this.Controls.Add(this.txtJobDetails);
            this.Controls.Add(this.lblLastGPSConn);
            this.Controls.Add(this.chkSMS);
            this.Controls.Add(this.lblSMSError2);
            this.Controls.Add(this.lblSmsError1);
            this.Controls.Add(this.txtDriverMobNo);
            this.Controls.Add(this.txtCustomerMobNo);
            this.Controls.Add(this.lblDriverMobNo);
            this.Controls.Add(this.lblCustomerMobNo);
            this.Controls.Add(this.btnDespatch);
            this.Controls.Add(this.lblNocMessage);
            this.Controls.Add(this.ddl_Driver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDespatchHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmDespatchJob";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dispatch Job";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDespatchJob_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDespatch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDespatchHeading;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadDropDownList ddl_Driver;
        private System.Windows.Forms.Label lblNocMessage;
        private Telerik.WinControls.UI.RadButton btnDespatch;
        private System.Windows.Forms.Label lblCustomerMobNo;
        private System.Windows.Forms.Label lblDriverMobNo;
        private System.Windows.Forms.Label txtCustomerMobNo;
        private System.Windows.Forms.Label txtDriverMobNo;
        private System.Windows.Forms.Label lblSmsError1;
        private System.Windows.Forms.Label lblSMSError2;
        private System.Windows.Forms.CheckBox chkSMS;
       
        private Telerik.WinControls.UI.RadLabel lblNearestDrv;
        private System.Windows.Forms.DataGridView grdNearestDrv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn details;
        private System.Windows.Forms.DataGridViewButtonColumn btnDespatchJob;
        private System.Windows.Forms.Label lblLastGPSConn;
        private System.Windows.Forms.Label txtJobDetails;
    }
}