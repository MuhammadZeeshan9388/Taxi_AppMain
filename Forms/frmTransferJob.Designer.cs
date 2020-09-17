namespace Taxi_AppMain
{
    partial class frmTransferJob
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            this.lblTransferHeading = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ddlThirdPartyCompany = new Telerik.WinControls.UI.RadDropDownList();
            this.btnTransfer = new Telerik.WinControls.UI.RadButton();
            this.lblCustomerMobNo = new System.Windows.Forms.Label();
            this.lblDriverMobNo = new System.Windows.Forms.Label();
            this.txtCustomerMobNo = new System.Windows.Forms.Label();
            this.lblSmsError1 = new System.Windows.Forms.Label();
            this.lblSMSError2 = new System.Windows.Forms.Label();
            this.numTransferJobCommission = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblFareRate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlCommissionType = new Telerik.WinControls.UI.RadDropDownList();
            this.numCommissionPercent = new Telerik.WinControls.UI.RadSpinEditor();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ddlThirdPartyCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTransfer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTransferJobCommission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCommissionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCommissionPercent)).BeginInit();
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
            this.lblTransferHeading.Size = new System.Drawing.Size(383, 28);
            this.lblTransferHeading.TabIndex = 0;
            this.lblTransferHeading.Text = "Transfer Job";
            this.lblTransferHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select";
            // 
            // ddlThirdPartyCompany
            // 
            this.ddlThirdPartyCompany.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlThirdPartyCompany.Location = new System.Drawing.Point(137, 50);
            this.ddlThirdPartyCompany.Name = "ddlThirdPartyCompany";
            this.ddlThirdPartyCompany.Size = new System.Drawing.Size(223, 23);
            this.ddlThirdPartyCompany.TabIndex = 0;
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(129, 200);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(130, 41);
            this.btnTransfer.TabIndex = 5;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.Click += new System.EventHandler(this.btnDespatch_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnTransfer.GetChildAt(0))).Text = "Transfer";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTransfer.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnTransfer.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblCustomerMobNo
            // 
            this.lblCustomerMobNo.AutoSize = true;
            this.lblCustomerMobNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerMobNo.Location = new System.Drawing.Point(21, 83);
            this.lblCustomerMobNo.Name = "lblCustomerMobNo";
            this.lblCustomerMobNo.Size = new System.Drawing.Size(64, 16);
            this.lblCustomerMobNo.TabIndex = 6;
            this.lblCustomerMobNo.Text = "Fare Rate";
            // 
            // lblDriverMobNo
            // 
            this.lblDriverMobNo.AutoSize = true;
            this.lblDriverMobNo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriverMobNo.Location = new System.Drawing.Point(21, 139);
            this.lblDriverMobNo.Name = "lblDriverMobNo";
            this.lblDriverMobNo.Size = new System.Drawing.Size(77, 16);
            this.lblDriverMobNo.TabIndex = 8;
            this.lblDriverMobNo.Text = "Commission";
            // 
            // txtCustomerMobNo
            // 
            this.txtCustomerMobNo.AutoSize = true;
            this.txtCustomerMobNo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustomerMobNo.Location = new System.Drawing.Point(205, 80);
            this.txtCustomerMobNo.Name = "txtCustomerMobNo";
            this.txtCustomerMobNo.Size = new System.Drawing.Size(0, 14);
            this.txtCustomerMobNo.TabIndex = 9;
            this.txtCustomerMobNo.Visible = false;
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
            // numTransferJobCommission
            // 
            this.numTransferJobCommission.DecimalPlaces = 2;
            this.numTransferJobCommission.Enabled = false;
            this.numTransferJobCommission.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTransferJobCommission.Location = new System.Drawing.Point(174, 137);
            this.numTransferJobCommission.Name = "numTransferJobCommission";
            // 
            // 
            // 
            this.numTransferJobCommission.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numTransferJobCommission.ShowBorder = true;
            this.numTransferJobCommission.Size = new System.Drawing.Size(100, 21);
            this.numTransferJobCommission.TabIndex = 14;
            this.numTransferJobCommission.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numTransferJobCommission.GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblFareRate
            // 
            this.lblFareRate.AutoSize = true;
            this.lblFareRate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFareRate.ForeColor = System.Drawing.Color.Red;
            this.lblFareRate.Location = new System.Drawing.Point(171, 82);
            this.lblFareRate.Name = "lblFareRate";
            this.lblFareRate.Size = new System.Drawing.Size(16, 16);
            this.lblFareRate.TabIndex = 15;
            this.lblFareRate.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 16;
            this.label1.Text = "Commission Type";
            // 
            // ddlCommissionType
            // 
            this.ddlCommissionType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlCommissionType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem1.Selected = true;
            radListDataItem1.Text = "Percent";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "Amount";
            radListDataItem2.TextWrap = true;
            this.ddlCommissionType.Items.Add(radListDataItem1);
            this.ddlCommissionType.Items.Add(radListDataItem2);
            this.ddlCommissionType.Location = new System.Drawing.Point(174, 107);
            this.ddlCommissionType.Name = "ddlCommissionType";
            this.ddlCommissionType.Size = new System.Drawing.Size(100, 23);
            this.ddlCommissionType.TabIndex = 17;
            this.ddlCommissionType.Text = "Percent";
            // 
            // numCommissionPercent
            // 
            this.numCommissionPercent.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numCommissionPercent.Location = new System.Drawing.Point(280, 107);
            this.numCommissionPercent.Name = "numCommissionPercent";
            // 
            // 
            // 
            this.numCommissionPercent.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numCommissionPercent.ShowBorder = true;
            this.numCommissionPercent.Size = new System.Drawing.Size(54, 21);
            this.numCommissionPercent.TabIndex = 18;
            this.numCommissionPercent.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numCommissionPercent.GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(340, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "%";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmTransferJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(383, 271);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numCommissionPercent);
            this.Controls.Add(this.ddlCommissionType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFareRate);
            this.Controls.Add(this.numTransferJobCommission);
            this.Controls.Add(this.lblSMSError2);
            this.Controls.Add(this.lblSmsError1);
            this.Controls.Add(this.txtCustomerMobNo);
            this.Controls.Add(this.lblDriverMobNo);
            this.Controls.Add(this.lblCustomerMobNo);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.ddlThirdPartyCompany);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTransferHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmTransferJob";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Transfer Job";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDespatchJob_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ddlThirdPartyCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTransfer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTransferJobCommission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlCommissionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCommissionPercent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTransferHeading;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadDropDownList ddlThirdPartyCompany;
        private Telerik.WinControls.UI.RadButton btnTransfer;
        private System.Windows.Forms.Label lblCustomerMobNo;
        private System.Windows.Forms.Label lblDriverMobNo;
        private System.Windows.Forms.Label txtCustomerMobNo;
        private System.Windows.Forms.Label lblSmsError1;
        private System.Windows.Forms.Label lblSMSError2;
       
        private Telerik.WinControls.UI.RadLabel lblNearestDrv;
        private System.Windows.Forms.DataGridView grdNearestDrv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn details;
        private System.Windows.Forms.DataGridViewButtonColumn btnDespatchJob;
        private Telerik.WinControls.UI.RadSpinEditor numTransferJobCommission;
        private System.Windows.Forms.Label lblFareRate;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadDropDownList ddlCommissionType;
        private Telerik.WinControls.UI.RadSpinEditor numCommissionPercent;
        private System.Windows.Forms.Label label3;
    }
}