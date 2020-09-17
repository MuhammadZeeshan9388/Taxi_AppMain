namespace Taxi_AppMain.Forms
{
    partial class frmMessageReply
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
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtReply = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.txtFromMsg = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(0, 0);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(426, 23);
            this.radLabel1.TabIndex = 232;
            this.radLabel1.Text = "Reply";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtReply
            // 
            this.txtReply.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReply.Location = new System.Drawing.Point(0, 23);
            this.txtReply.Name = "txtReply";
            this.txtReply.Size = new System.Drawing.Size(344, 138);
            this.txtReply.TabIndex = 233;
            this.txtReply.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblFrom);
            this.panel1.Controls.Add(this.txtFromMsg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 161);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 112);
            this.panel1.TabIndex = 235;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFrom.Location = new System.Drawing.Point(0, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(0, 18);
            this.lblFrom.TabIndex = 235;
            // 
            // txtFromMsg
            // 
            this.txtFromMsg.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtFromMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtFromMsg.Location = new System.Drawing.Point(0, 20);
            this.txtFromMsg.Name = "txtFromMsg";
            this.txtFromMsg.Size = new System.Drawing.Size(426, 92);
            this.txtFromMsg.TabIndex = 234;
            this.txtFromMsg.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(344, 23);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(82, 138);
            this.btnSend.TabIndex = 236;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // frmMessageReply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 273);
            this.Controls.Add(this.txtReply);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radLabel1);
            this.MaximizeBox = false;
            this.Name = "frmMessageReply";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reply";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMessageReply_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.RichTextBox txtReply;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.RichTextBox txtFromMsg;
        private System.Windows.Forms.Button btnSend;

    }
}