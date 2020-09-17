namespace Taxi_AppMain
{
    partial class frmCustomMessageBox
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
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.Label();
            this.txtAccept = new System.Windows.Forms.Label();
            this.txtAcceptLabel = new System.Windows.Forms.Label();
            this.txtPendingAccept = new System.Windows.Forms.Label();
            this.txtPendingAcceptLabel = new System.Windows.Forms.Label();
            this.txtDeclineLabel = new System.Windows.Forms.Label();
            this.txtDecline = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.White;
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Location = new System.Drawing.Point(108, 127);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 42);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNo.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.Location = new System.Drawing.Point(246, 127);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 42);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.Silver;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTitle.Font = new System.Drawing.Font("Arial", 14F);
            this.txtTitle.Location = new System.Drawing.Point(0, 0);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(423, 30);
            this.txtTitle.TabIndex = 2;
            this.txtTitle.Text = "Booking Ref: ";
            this.txtTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAccept
            // 
            this.txtAccept.Font = new System.Drawing.Font("Arial", 12F);
            this.txtAccept.Location = new System.Drawing.Point(54, 58);
            this.txtAccept.Name = "txtAccept";
            this.txtAccept.Size = new System.Drawing.Size(334, 35);
            this.txtAccept.TabIndex = 3;
            this.txtAccept.Text = "Are you sure wanted to  Accept  this booking ?";
            this.txtAccept.Visible = false;
            // 
            // txtAcceptLabel
            // 
            this.txtAcceptLabel.AutoSize = true;
            this.txtAcceptLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcceptLabel.ForeColor = System.Drawing.Color.Green;
            this.txtAcceptLabel.Location = new System.Drawing.Point(215, 58);
            this.txtAcceptLabel.Name = "txtAcceptLabel";
            this.txtAcceptLabel.Size = new System.Drawing.Size(62, 19);
            this.txtAcceptLabel.TabIndex = 4;
            this.txtAcceptLabel.Text = "Accept";
            this.txtAcceptLabel.Visible = false;
            // 
            // txtPendingAccept
            // 
            this.txtPendingAccept.Font = new System.Drawing.Font("Arial", 12F);
            this.txtPendingAccept.Location = new System.Drawing.Point(16, 60);
            this.txtPendingAccept.Name = "txtPendingAccept";
            this.txtPendingAccept.Size = new System.Drawing.Size(395, 32);
            this.txtPendingAccept.TabIndex = 5;
            this.txtPendingAccept.Text = "Are you sure wanted to Pending Accept    this booking ?";
            this.txtPendingAccept.Visible = false;
            // 
            // txtPendingAcceptLabel
            // 
            this.txtPendingAcceptLabel.AutoSize = true;
            this.txtPendingAcceptLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPendingAcceptLabel.ForeColor = System.Drawing.Color.HotPink;
            this.txtPendingAcceptLabel.Location = new System.Drawing.Point(178, 60);
            this.txtPendingAcceptLabel.Name = "txtPendingAcceptLabel";
            this.txtPendingAcceptLabel.Size = new System.Drawing.Size(129, 19);
            this.txtPendingAcceptLabel.TabIndex = 6;
            this.txtPendingAcceptLabel.Text = "Pending Accept";
            this.txtPendingAcceptLabel.Visible = false;
            // 
            // txtDeclineLabel
            // 
            this.txtDeclineLabel.AutoSize = true;
            this.txtDeclineLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeclineLabel.ForeColor = System.Drawing.Color.Red;
            this.txtDeclineLabel.Location = new System.Drawing.Point(215, 60);
            this.txtDeclineLabel.Name = "txtDeclineLabel";
            this.txtDeclineLabel.Size = new System.Drawing.Size(66, 19);
            this.txtDeclineLabel.TabIndex = 8;
            this.txtDeclineLabel.Text = "Decline";
            this.txtDeclineLabel.Visible = false;
            // 
            // txtDecline
            // 
            this.txtDecline.Font = new System.Drawing.Font("Arial", 12F);
            this.txtDecline.Location = new System.Drawing.Point(54, 60);
            this.txtDecline.Name = "txtDecline";
            this.txtDecline.Size = new System.Drawing.Size(334, 31);
            this.txtDecline.TabIndex = 7;
            this.txtDecline.Text = "Are you sure wanted to  Decline  this booking ?";
            this.txtDecline.Visible = false;
            // 
            // frmCustomMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(423, 181);
            this.ControlBox = false;
            this.Controls.Add(this.txtDeclineLabel);
            this.Controls.Add(this.txtDecline);
            this.Controls.Add(this.txtPendingAcceptLabel);
            this.Controls.Add(this.txtPendingAccept);
            this.Controls.Add(this.txtAcceptLabel);
            this.Controls.Add(this.txtAccept);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCustomMessageBox";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Label txtTitle;
        private System.Windows.Forms.Label txtAccept;
        private System.Windows.Forms.Label txtAcceptLabel;
        private System.Windows.Forms.Label txtPendingAccept;
        private System.Windows.Forms.Label txtPendingAcceptLabel;
        private System.Windows.Forms.Label txtDeclineLabel;
        private System.Windows.Forms.Label txtDecline;
    }
}