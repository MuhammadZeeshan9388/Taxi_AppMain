namespace Taxi_AppMain
{
    partial class frmDespatchGhostJob
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
            this.lblSmsError1 = new System.Windows.Forms.Label();
            this.lblSMSError2 = new System.Windows.Forms.Label();
            this.lblLastGPSConn = new System.Windows.Forms.Label();
            this.txtTokenRequired = new System.Windows.Forms.Label();
            this.btnGenerateToken = new Telerik.WinControls.UI.RadButton();
            this.txtTokenNo = new Telerik.WinControls.UI.RadLabel();
            this.btnPrintToken = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDespatch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerateToken)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTokenNo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDespatchHeading
            // 
            this.lblDespatchHeading.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblDespatchHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDespatchHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDespatchHeading.ForeColor = System.Drawing.Color.Black;
            this.lblDespatchHeading.Location = new System.Drawing.Point(0, 0);
            this.lblDespatchHeading.Name = "lblDespatchHeading";
            this.lblDespatchHeading.Size = new System.Drawing.Size(469, 28);
            this.lblDespatchHeading.TabIndex = 0;
            this.lblDespatchHeading.Text = "Despatch Job";
            this.lblDespatchHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select Driver";
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(103, 97);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(259, 23);
            this.ddl_Driver.TabIndex = 2;
            this.ddl_Driver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddl_Driver_SelectedIndexChanged);
            // 
            // lblNocMessage
            // 
            this.lblNocMessage.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblNocMessage.Location = new System.Drawing.Point(13, 136);
            this.lblNocMessage.Name = "lblNocMessage";
            this.lblNocMessage.Size = new System.Drawing.Size(444, 53);
            this.lblNocMessage.TabIndex = 4;
            this.lblNocMessage.Visible = false;
            // 
            // btnDespatch
            // 
            this.btnDespatch.Location = new System.Drawing.Point(121, 213);
            this.btnDespatch.Name = "btnDespatch";
            this.btnDespatch.Size = new System.Drawing.Size(209, 41);
            this.btnDespatch.TabIndex = 5;
            this.btnDespatch.Text = "Dispatch Ghost Job";
            this.btnDespatch.Click += new System.EventHandler(this.btnDespatch_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDespatch.GetChildAt(0))).Text = "Dispatch Ghost Job";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDespatch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDespatch.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // lblLastGPSConn
            // 
            this.lblLastGPSConn.AutoSize = true;
            this.lblLastGPSConn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLastGPSConn.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastGPSConn.ForeColor = System.Drawing.Color.Red;
            this.lblLastGPSConn.Location = new System.Drawing.Point(0, 269);
            this.lblLastGPSConn.Name = "lblLastGPSConn";
            this.lblLastGPSConn.Size = new System.Drawing.Size(156, 14);
            this.lblLastGPSConn.TabIndex = 18;
            this.lblLastGPSConn.Text = "Last Connection Made : ";
            this.lblLastGPSConn.Visible = false;
            // 
            // txtTokenRequired
            // 
            this.txtTokenRequired.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.txtTokenRequired.ForeColor = System.Drawing.Color.Red;
            this.txtTokenRequired.Location = new System.Drawing.Point(1, 39);
            this.txtTokenRequired.Name = "txtTokenRequired";
            this.txtTokenRequired.Size = new System.Drawing.Size(226, 31);
            this.txtTokenRequired.TabIndex = 19;
            this.txtTokenRequired.Text = "Token # is not generated for this Job , Press Generate Token to Dispatch it.";
            this.txtTokenRequired.Visible = false;
            // 
            // btnGenerateToken
            // 
            this.btnGenerateToken.Location = new System.Drawing.Point(233, 36);
            this.btnGenerateToken.Name = "btnGenerateToken";
            this.btnGenerateToken.Size = new System.Drawing.Size(109, 41);
            this.btnGenerateToken.TabIndex = 20;
            this.btnGenerateToken.Text = "Generate Token";
            this.btnGenerateToken.Visible = false;
            this.btnGenerateToken.Click += new System.EventHandler(this.btnGenerateToken_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnGenerateToken.GetChildAt(0))).Text = "Generate Token";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnGenerateToken.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnGenerateToken.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtTokenNo
            // 
            this.txtTokenNo.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTokenNo.ForeColor = System.Drawing.Color.Red;
            this.txtTokenNo.Location = new System.Drawing.Point(381, 36);
            this.txtTokenNo.Name = "txtTokenNo";
            // 
            // 
            // 
            this.txtTokenNo.RootElement.ForeColor = System.Drawing.Color.Red;
            this.txtTokenNo.Size = new System.Drawing.Size(65, 38);
            this.txtTokenNo.TabIndex = 279;
            this.txtTokenNo.Text = "001";
            this.txtTokenNo.Visible = false;
            // 
            // btnPrintToken
            // 
            this.btnPrintToken.Image = global::Taxi_AppMain.Properties.Resources.Print1;
            this.btnPrintToken.Location = new System.Drawing.Point(387, 73);
            this.btnPrintToken.Name = "btnPrintToken";
            this.btnPrintToken.Size = new System.Drawing.Size(53, 31);
            this.btnPrintToken.TabIndex = 280;
            this.btnPrintToken.UseVisualStyleBackColor = true;
            this.btnPrintToken.Visible = false;
            this.btnPrintToken.Click += new System.EventHandler(this.btnPrintToken_Click);
            // 
            // frmDespatchGhostJob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(469, 283);
            this.Controls.Add(this.btnPrintToken);
            this.Controls.Add(this.txtTokenNo);
            this.Controls.Add(this.btnGenerateToken);
            this.Controls.Add(this.txtTokenRequired);
            this.Controls.Add(this.lblLastGPSConn);
            this.Controls.Add(this.lblSMSError2);
            this.Controls.Add(this.lblSmsError1);
            this.Controls.Add(this.btnDespatch);
            this.Controls.Add(this.lblNocMessage);
            this.Controls.Add(this.ddl_Driver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDespatchHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmDespatchGhostJob";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dispatch Ghost Job";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDespatchJob_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDespatch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGenerateToken)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTokenNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDespatchHeading;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadDropDownList ddl_Driver;
        private System.Windows.Forms.Label lblNocMessage;
        private Telerik.WinControls.UI.RadButton btnDespatch;
        private System.Windows.Forms.Label lblSmsError1;
        private System.Windows.Forms.Label lblSMSError2;
       
        private Telerik.WinControls.UI.RadLabel lblNearestDrv;
        private System.Windows.Forms.DataGridView grdNearestDrv;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn details;
        private System.Windows.Forms.DataGridViewButtonColumn btnDespatchJob;
        private System.Windows.Forms.Label lblLastGPSConn;
        private System.Windows.Forms.Label txtTokenRequired;
        private Telerik.WinControls.UI.RadButton btnGenerateToken;
        private Telerik.WinControls.UI.RadLabel txtTokenNo;
        private System.Windows.Forms.Button btnPrintToken;
    }
}