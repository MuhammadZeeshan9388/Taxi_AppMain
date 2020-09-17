namespace Taxi_AppMain
{
    partial class frmTransferJobInOffice
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
            this.lblTransferHeading = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlThirdPartyCompany = new Telerik.WinControls.UI.RadDropDownList();
            this.btnTransfer = new Telerik.WinControls.UI.RadButton();
            this.lblSmsError1 = new System.Windows.Forms.Label();
            this.lblSMSError2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ddlThirdPartyCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTransfer)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTransferHeading
            // 
            this.lblTransferHeading.BackColor = System.Drawing.Color.LightYellow;
            this.lblTransferHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTransferHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransferHeading.ForeColor = System.Drawing.Color.Black;
            this.lblTransferHeading.Location = new System.Drawing.Point(0, 0);
            this.lblTransferHeading.Name = "lblTransferHeading";
            this.lblTransferHeading.Size = new System.Drawing.Size(516, 28);
            this.lblTransferHeading.TabIndex = 0;
            this.lblTransferHeading.Text = "Transfer Booking";
            this.lblTransferHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "SubCompany";
            // 
            // ddlThirdPartyCompany
            // 
            this.ddlThirdPartyCompany.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlThirdPartyCompany.Location = new System.Drawing.Point(137, 50);
            this.ddlThirdPartyCompany.Name = "ddlThirdPartyCompany";
            this.ddlThirdPartyCompany.Size = new System.Drawing.Size(367, 23);
            this.ddlThirdPartyCompany.TabIndex = 0;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(194, 148);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(130, 41);
            this.btnTransfer.TabIndex = 5;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.Click += new System.EventHandler(this.btnDespatch_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnTransfer.GetChildAt(0))).Text = "Transfer";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTransfer.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTransfer.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // frmTransferJobInOffice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(516, 219);
            this.Controls.Add(this.lblSMSError2);
            this.Controls.Add(this.lblSmsError1);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.ddlThirdPartyCompany);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTransferHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmTransferJobInOffice";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transfer Job";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDespatchJob_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ddlThirdPartyCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTransfer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTransferHeading;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadDropDownList ddlThirdPartyCompany;
        private Telerik.WinControls.UI.RadButton btnTransfer;
        private System.Windows.Forms.Label lblSmsError1;
        private System.Windows.Forms.Label lblSMSError2;
       
        private Telerik.WinControls.UI.RadLabel lblNearestDrv;
        private System.Windows.Forms.DataGridView grdNearestDrv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn details;
        private System.Windows.Forms.DataGridViewButtonColumn btnDespatchJob;
    }
}