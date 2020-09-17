namespace Taxi_AppMain
{
    partial class frmSendEmailOption
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
            this.rdoAccount = new Telerik.WinControls.UI.RadRadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoCustomer = new Telerik.WinControls.UI.RadRadioButton();
            this.btnSendInvoice = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.object_c417983d_ea15_4a1a_b9d7_64e3355e67c6 = new Telerik.WinControls.RootRadElement();
            ((System.ComponentModel.ISupportInitialize)(this.rdoAccount)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // rdoAccount
            // 
            this.rdoAccount.Location = new System.Drawing.Point(5, 17);
            this.rdoAccount.Name = "rdoAccount";
            this.rdoAccount.Size = new System.Drawing.Size(81, 18);
            this.rdoAccount.TabIndex = 0;
            this.rdoAccount.Text = "Account";
            this.rdoAccount.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoAccount.GetChildAt(0))).ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoAccount.GetChildAt(0))).Text = "Account";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoAccount.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoAccount.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoCustomer);
            this.panel1.Controls.Add(this.rdoAccount);
            this.panel1.Location = new System.Drawing.Point(119, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(190, 48);
            this.panel1.TabIndex = 1;
            // 
            // rdoCustomer
            // 
            this.rdoCustomer.Location = new System.Drawing.Point(92, 16);
            this.rdoCustomer.Name = "rdoCustomer";
            this.rdoCustomer.Size = new System.Drawing.Size(90, 18);
            this.rdoCustomer.TabIndex = 1;
            this.rdoCustomer.Text = "Customer";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoCustomer.GetChildAt(0))).Text = "Customer";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoCustomer.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoCustomer.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSendInvoice
            // 
            this.btnSendInvoice.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSendInvoice.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnSendInvoice.Location = new System.Drawing.Point(30, 129);
            this.btnSendInvoice.Name = "btnSendInvoice";
            this.btnSendInvoice.Size = new System.Drawing.Size(108, 43);
            this.btnSendInvoice.TabIndex = 270;
            this.btnSendInvoice.Text = "Send Invoice";
            this.btnSendInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSendInvoice.TextWrap = true;
            this.btnSendInvoice.Click += new System.EventHandler(this.btnSendInvoice_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendInvoice.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendInvoice.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendInvoice.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendInvoice.GetChildAt(0))).Text = "Send Invoice";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendInvoice.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendInvoice.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendInvoice.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnCancel
            // 
            this.btnCancel.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCancel.ImageScalingSize = new System.Drawing.Size(16, 12);
            this.btnCancel.Location = new System.Drawing.Point(200, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 43);
            this.btnCancel.TabIndex = 271;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.TextWrap = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).Text = "Cancel";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 272;
            this.label1.Text = "Invoice Type";
            // 
            // object_c417983d_ea15_4a1a_b9d7_64e3355e67c6
            // 
            this.object_c417983d_ea15_4a1a_b9d7_64e3355e67c6.Name = "object_c417983d_ea15_4a1a_b9d7_64e3355e67c6";
            this.object_c417983d_ea15_4a1a_b9d7_64e3355e67c6.StretchHorizontally = true;
            this.object_c417983d_ea15_4a1a_b9d7_64e3355e67c6.StretchVertically = true;
            // 
            // frmSendEmailOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 228);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSendInvoice);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSendEmailOption";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Email Option";
            ((System.ComponentModel.ISupportInitialize)(this.rdoAccount)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdoCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadRadioButton rdoAccount;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadRadioButton rdoCustomer;
        private Telerik.WinControls.UI.RadButton btnSendInvoice;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.RootRadElement object_c417983d_ea15_4a1a_b9d7_64e3355e67c6;
    }
}