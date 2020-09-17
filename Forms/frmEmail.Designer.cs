namespace Taxi_AppMain
{
    partial class frmEmail
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
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTo = new Telerik.WinControls.UI.RadTextBox();
            this.txtSubject = new Telerik.WinControls.UI.RadTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ddlExportType = new Telerik.WinControls.UI.RadDropDownList();
            this.txtAttachment = new System.Windows.Forms.Label();
            this.btnSendEmail = new Telerik.WinControls.UI.RadButton();
            this.txtFrom = new Telerik.WinControls.UI.RadTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEditFrom = new Telerik.WinControls.UI.RadButton();
            this.btnPickEmail = new Telerik.WinControls.UI.RadDropDownButton();
            this.txtBody = new Telerik.WinControls.UI.RadTextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlExportType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickEmail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBody)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.Beige;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(0, 0);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(479, 23);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Email";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 92;
            this.label1.Text = "To Email";
            // 
            // txtTo
            // 
            this.txtTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtTo.Location = new System.Drawing.Point(90, 56);
            this.txtTo.MaxLength = 100;
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(233, 22);
            this.txtTo.TabIndex = 202;
            this.txtTo.TabStop = false;
            // 
            // txtSubject
            // 
            this.txtSubject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSubject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSubject.Location = new System.Drawing.Point(90, 86);
            this.txtSubject.MaxLength = 300;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(380, 22);
            this.txtSubject.TabIndex = 204;
            this.txtSubject.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(6, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 17);
            this.label2.TabIndex = 203;
            this.label2.Text = "Subject";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ddlExportType);
            this.groupBox1.Controls.Add(this.txtAttachment);
            this.groupBox1.Location = new System.Drawing.Point(15, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 62);
            this.groupBox1.TabIndex = 205;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Attachment";
            // 
            // ddlExportType
            // 
            this.ddlExportType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlExportType.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem1.Selected = true;
            radListDataItem1.Text = "Pdf";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem2.Text = "Excel";
            radListDataItem2.TextWrap = true;
            this.ddlExportType.Items.Add(radListDataItem1);
            this.ddlExportType.Items.Add(radListDataItem2);
            this.ddlExportType.Location = new System.Drawing.Point(282, 23);
            this.ddlExportType.Name = "ddlExportType";
            this.ddlExportType.Size = new System.Drawing.Size(66, 23);
            this.ddlExportType.TabIndex = 1;
            this.ddlExportType.Text = "Pdf";
            // 
            // txtAttachment
            // 
            this.txtAttachment.AutoSize = true;
            this.txtAttachment.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAttachment.Location = new System.Drawing.Point(7, 27);
            this.txtAttachment.Name = "txtAttachment";
            this.txtAttachment.Size = new System.Drawing.Size(130, 13);
            this.txtAttachment.TabIndex = 0;
            this.txtAttachment.Text = "Driver Commission Report";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendEmail.Image = global::Taxi_AppMain.Properties.Resources.email;
            this.btnSendEmail.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSendEmail.Location = new System.Drawing.Point(388, 259);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(82, 52);
            this.btnSendEmail.TabIndex = 206;
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
            // txtFrom
            // 
            this.txtFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFrom.Enabled = false;
            this.txtFrom.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtFrom.Location = new System.Drawing.Point(90, 26);
            this.txtFrom.MaxLength = 100;
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(233, 22);
            this.txtFrom.TabIndex = 208;
            this.txtFrom.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(6, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 17);
            this.label3.TabIndex = 207;
            this.label3.Text = "From";
            // 
            // btnEditFrom
            // 
            this.btnEditFrom.Location = new System.Drawing.Point(332, 26);
            this.btnEditFrom.Name = "btnEditFrom";
            this.btnEditFrom.Size = new System.Drawing.Size(48, 24);
            this.btnEditFrom.TabIndex = 209;
            this.btnEditFrom.Text = "Edit";
            this.btnEditFrom.Click += new System.EventHandler(this.btnEditFrom_Click);
            // 
            // btnPickEmail
            // 
            this.btnPickEmail.Location = new System.Drawing.Point(332, 56);
            this.btnPickEmail.Name = "btnPickEmail";
            this.btnPickEmail.Size = new System.Drawing.Size(48, 24);
            this.btnPickEmail.TabIndex = 223;
            this.btnPickEmail.Text = "Pick";
            // 
            // txtBody
            // 
            this.txtBody.AcceptsReturn = true;
            this.txtBody.AcceptsTab = true;
            this.txtBody.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBody.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBody.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtBody.Location = new System.Drawing.Point(91, 116);
            this.txtBody.MaxLength = 50000;
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            // 
            // 
            // 
            this.txtBody.RootElement.StretchVertically = true;
            this.txtBody.Size = new System.Drawing.Size(380, 129);
            this.txtBody.TabIndex = 225;
            this.txtBody.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(7, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 224;
            this.label4.Text = "Body";
            // 
            // frmEmail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Ivory;
            this.ClientSize = new System.Drawing.Size(479, 321);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPickEmail);
            this.Controls.Add(this.btnEditFrom);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSendEmail);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmail";
            this.ShowIcon = false;
            this.Text = "Email";
            this.Load += new System.EventHandler(this.frmEmail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSubject)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlExportType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSendEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEditFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPickEmail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBody)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadButton btnSendEmail;
        private Telerik.WinControls.UI.RadTextBox txtFrom;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadButton btnEditFrom;
        private Telerik.WinControls.UI.RadDropDownButton btnPickEmail;
        public Telerik.WinControls.UI.RadTextBox txtTo;
        public Telerik.WinControls.UI.RadTextBox txtSubject;
        public System.Windows.Forms.Label txtAttachment;
        private Telerik.WinControls.UI.RadDropDownList ddlExportType;
        public Telerik.WinControls.UI.RadTextBox txtBody;
        private System.Windows.Forms.Label label4;
    }
}
