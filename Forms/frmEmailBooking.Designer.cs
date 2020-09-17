namespace Taxi_AppMain
{
    partial class frmEmailBooking
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnSendEmail = new Telerik.WinControls.UI.RadButton();
            this.txtSubject = new Telerik.WinControls.UI.RadTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTo = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.chkReturnDetails = new System.Windows.Forms.CheckBox();
            this.btnPickEmail = new Telerik.WinControls.UI.RadDropDownButton();
            this.ddlFrom = new System.Windows.Forms.ComboBox();
            this.ddlEmailToType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.radLabel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickEmail)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 18);
            this.label3.TabIndex = 217;
            this.label3.Text = "From :";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Image = global::Taxi_AppMain.Properties.Resources.email;
            this.btnSendEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSendEmail.Location = new System.Drawing.Point(460, 30);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(82, 52);
            this.btnSendEmail.TabIndex = 216;
            this.btnSendEmail.Text = "Send";
            this.btnSendEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.email;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSendEmail.GetChildAt(0))).Text = "Send";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSendEmail.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtSubject
            // 
            this.txtSubject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSubject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(96, 142);
            this.txtSubject.MaxLength = 150;
            this.txtSubject.Multiline = true;
            this.txtSubject.Name = "txtSubject";
            // 
            // 
            // 
            this.txtSubject.RootElement.StretchVertically = true;
            this.txtSubject.Size = new System.Drawing.Size(446, 77);
            this.txtSubject.TabIndex = 214;
            this.txtSubject.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 18);
            this.label2.TabIndex = 213;
            this.label2.Text = "Subject";
            // 
            // txtTo
            // 
            this.txtTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(96, 96);
            this.txtTo.MaxLength = 100;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(276, 24);
            this.txtTo.TabIndex = 212;
            this.txtTo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 211;
            this.label1.Text = "To Email :";
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.Beige;
            this.radLabel1.Controls.Add(this.chkReturnDetails);
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(0, 0);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(550, 28);
            this.radLabel1.TabIndex = 210;
            this.radLabel1.Text = "Booking Confirmation";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkReturnDetails
            // 
            this.chkReturnDetails.AutoSize = true;
            this.chkReturnDetails.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkReturnDetails.Location = new System.Drawing.Point(11, 4);
            this.chkReturnDetails.Name = "chkReturnDetails";
            this.chkReturnDetails.Size = new System.Drawing.Size(114, 18);
            this.chkReturnDetails.TabIndex = 225;
            this.chkReturnDetails.Text = "Return Details";
            this.chkReturnDetails.UseVisualStyleBackColor = true;
            this.chkReturnDetails.Visible = false;
            // 
            // btnPickEmail
            // 
            this.btnPickEmail.Location = new System.Drawing.Point(378, 96);
            this.btnPickEmail.Name = "btnPickEmail";
            this.btnPickEmail.Size = new System.Drawing.Size(48, 24);
            this.btnPickEmail.TabIndex = 222;
            this.btnPickEmail.Text = "Pick";
            this.btnPickEmail.Click += new System.EventHandler(this.btnPickEmail_Click);
            // 
            // ddlFrom
            // 
            this.ddlFrom.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlFrom.FormattingEnabled = true;
            this.ddlFrom.Location = new System.Drawing.Point(96, 38);
            this.ddlFrom.Name = "ddlFrom";
            this.ddlFrom.Size = new System.Drawing.Size(330, 26);
            this.ddlFrom.TabIndex = 223;
            // 
            // ddlEmailToType
            // 
            this.ddlEmailToType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlEmailToType.FormattingEnabled = true;
            this.ddlEmailToType.Items.AddRange(new object[] {
            "Customer",
            "Company",
            "Driver"});
            this.ddlEmailToType.Location = new System.Drawing.Point(439, 95);
            this.ddlEmailToType.Name = "ddlEmailToType";
            this.ddlEmailToType.Size = new System.Drawing.Size(107, 26);
            this.ddlEmailToType.TabIndex = 224;
            this.ddlEmailToType.Visible = false;
            // 
            // frmEmailBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(550, 308);
            this.Controls.Add(this.ddlEmailToType);
            this.Controls.Add(this.ddlFrom);
            this.Controls.Add(this.btnPickEmail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radLabel1);
            this.MaximizeBox = false;
            this.Name = "frmEmailBooking";
            this.ShowIcon = false;
            this.Text = "Booking Confirmation";
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.radLabel1.ResumeLayout(false);
            this.radLabel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickEmail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadButton btnSendEmail;
        private Telerik.WinControls.UI.RadTextBox txtSubject;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox txtTo;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownButton btnPickEmail;
        private System.Windows.Forms.ComboBox ddlFrom;
        private System.Windows.Forms.ComboBox ddlEmailToType;
        private System.Windows.Forms.CheckBox chkReturnDetails;
    }
}