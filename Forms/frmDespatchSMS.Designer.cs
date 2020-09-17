namespace Taxi_AppMain
{
    partial class frmDespatchSMS
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
            this.object_a8ad72cf_04e1_45ee_9f0c_ca3771b01428 = new Telerik.WinControls.RootRadElement();
            this.grdNearestDrv = new Telerik.WinControls.UI.RadGridView();
            this.chkCompleteJob = new System.Windows.Forms.CheckBox();
            this.chkReturnDetails = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDespatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNearestDrv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNearestDrv.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDespatchHeading
            // 
            this.lblDespatchHeading.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lblDespatchHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDespatchHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDespatchHeading.ForeColor = System.Drawing.Color.Black;
            this.lblDespatchHeading.Location = new System.Drawing.Point(0, 0);
            this.lblDespatchHeading.Name = "lblDespatchHeading";
            this.lblDespatchHeading.Size = new System.Drawing.Size(379, 28);
            this.lblDespatchHeading.TabIndex = 0;
            this.lblDespatchHeading.Text = "Dispatch SMS";
            this.lblDespatchHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Driver";
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(103, 92);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(259, 23);
            this.ddl_Driver.TabIndex = 2;
            this.ddl_Driver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddl_Driver_SelectedIndexChanged);
            // 
            // lblNocMessage
            // 
            this.lblNocMessage.AutoSize = true;
            this.lblNocMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNocMessage.Location = new System.Drawing.Point(16, 31);
            this.lblNocMessage.Name = "lblNocMessage";
            this.lblNocMessage.Size = new System.Drawing.Size(0, 14);
            this.lblNocMessage.TabIndex = 4;
            this.lblNocMessage.Visible = false;
            // 
            // btnDespatch
            // 
            this.btnDespatch.Location = new System.Drawing.Point(157, 219);
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
            this.lblCustomerMobNo.Location = new System.Drawing.Point(100, 123);
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
            this.lblDriverMobNo.Location = new System.Drawing.Point(100, 147);
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
            this.txtCustomerMobNo.Location = new System.Drawing.Point(229, 123);
            this.txtCustomerMobNo.Name = "txtCustomerMobNo";
            this.txtCustomerMobNo.Size = new System.Drawing.Size(0, 14);
            this.txtCustomerMobNo.TabIndex = 9;
            this.txtCustomerMobNo.Visible = false;
            // 
            // txtDriverMobNo
            // 
            this.txtDriverMobNo.AutoSize = true;
            this.txtDriverMobNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDriverMobNo.Location = new System.Drawing.Point(229, 147);
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
            // object_a8ad72cf_04e1_45ee_9f0c_ca3771b01428
            // 
            this.object_a8ad72cf_04e1_45ee_9f0c_ca3771b01428.Name = "object_a8ad72cf_04e1_45ee_9f0c_ca3771b01428";
            this.object_a8ad72cf_04e1_45ee_9f0c_ca3771b01428.StretchHorizontally = true;
            this.object_a8ad72cf_04e1_45ee_9f0c_ca3771b01428.StretchVertically = true;
            // 
            // grdNearestDrv
            // 
            this.grdNearestDrv.BackColor = System.Drawing.Color.LightYellow;
            this.grdNearestDrv.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNearestDrv.Location = new System.Drawing.Point(4, 297);
            // 
            // grdNearestDrv
            // 
            this.grdNearestDrv.MasterTemplate.AllowAddNewRow = false;
            this.grdNearestDrv.MasterTemplate.AllowDeleteRow = false;
            this.grdNearestDrv.MasterTemplate.AllowEditRow = false;
            this.grdNearestDrv.MasterTemplate.EnableFiltering = true;
            this.grdNearestDrv.MasterTemplate.EnableGrouping = false;
            this.grdNearestDrv.MasterTemplate.ShowColumnHeaders = false;
            this.grdNearestDrv.MasterTemplate.ShowFilteringRow = false;
            this.grdNearestDrv.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdNearestDrv.Name = "grdNearestDrv";
            // 
            // 
            // 
            this.grdNearestDrv.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.grdNearestDrv.ShowGroupPanel = false;
            this.grdNearestDrv.Size = new System.Drawing.Size(385, 120);
            this.grdNearestDrv.TabIndex = 16;
            this.grdNearestDrv.Text = "radGridView1";
            // 
            // chkCompleteJob
            // 
            this.chkCompleteJob.AutoSize = true;
            this.chkCompleteJob.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompleteJob.Location = new System.Drawing.Point(14, 231);
            this.chkCompleteJob.Name = "chkCompleteJob";
            this.chkCompleteJob.Size = new System.Drawing.Size(110, 18);
            this.chkCompleteJob.TabIndex = 18;
            this.chkCompleteJob.Text = "Complete Job";
            this.chkCompleteJob.UseVisualStyleBackColor = true;
            this.chkCompleteJob.CheckedChanged += new System.EventHandler(this.chkCompleteJob_CheckedChanged);
            // 
            // chkReturnDetails
            // 
            this.chkReturnDetails.AutoSize = true;
            this.chkReturnDetails.Checked = true;
            this.chkReturnDetails.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReturnDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReturnDetails.Location = new System.Drawing.Point(14, 207);
            this.chkReturnDetails.Name = "chkReturnDetails";
            this.chkReturnDetails.Size = new System.Drawing.Size(114, 18);
            this.chkReturnDetails.TabIndex = 19;
            this.chkReturnDetails.Text = "Return Details";
            this.chkReturnDetails.UseVisualStyleBackColor = true;
            // 
            // frmDespatchSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(379, 271);
            this.Controls.Add(this.chkReturnDetails);
            this.Controls.Add(this.chkCompleteJob);
            this.Controls.Add(this.grdNearestDrv);
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
            this.Name = "frmDespatchSMS";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dispatch SMS / Complete Job";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDespatchJob_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDespatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNearestDrv.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNearestDrv)).EndInit();
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
        private Telerik.WinControls.RootRadElement object_a8ad72cf_04e1_45ee_9f0c_ca3771b01428;
        private Telerik.WinControls.UI.RadGridView grdNearestDrv;
        private System.Windows.Forms.CheckBox chkCompleteJob;
        private System.Windows.Forms.CheckBox chkReturnDetails;
    }
}