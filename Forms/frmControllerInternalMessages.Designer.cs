namespace Taxi_AppMain
{
    partial class frmControllerInternalMessages
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlGroup = new System.Windows.Forms.ComboBox();
            this.ddlController = new System.Windows.Forms.ComboBox();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.chkWelcome = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.pgInbox = new System.Windows.Forms.TabPage();
            this.grdInbox = new System.Windows.Forms.DataGridView();
            this.pgSent = new System.Windows.Forms.TabPage();
            this.grdSent = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.pgInbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdInbox)).BeginInit();
            this.pgSent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSent)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.SteelBlue;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(0, 0);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(878, 31);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Message to Controller";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.GhostWhite;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ddlGroup);
            this.panel1.Controls.Add(this.ddlController);
            this.panel1.Controls.Add(this.chkAll);
            this.panel1.Controls.Add(this.chkWelcome);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSend);
            this.panel1.Controls.Add(this.txtMessage);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 559);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Group";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(81, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Controller";
            // 
            // ddlGroup
            // 
            this.ddlGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlGroup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlGroup.FormattingEnabled = true;
            this.ddlGroup.Location = new System.Drawing.Point(168, 307);
            this.ddlGroup.Name = "ddlGroup";
            this.ddlGroup.Size = new System.Drawing.Size(205, 24);
            this.ddlGroup.TabIndex = 19;
            // 
            // ddlController
            // 
            this.ddlController.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlController.FormattingEnabled = true;
            this.ddlController.Location = new System.Drawing.Point(167, 343);
            this.ddlController.Name = "ddlController";
            this.ddlController.Size = new System.Drawing.Size(205, 24);
            this.ddlController.TabIndex = 12;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAll.Location = new System.Drawing.Point(400, 344);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(97, 20);
            this.chkAll.TabIndex = 13;
            this.chkAll.Text = "Send to All";
            this.chkAll.UseVisualStyleBackColor = true;
            // 
            // chkWelcome
            // 
            this.chkWelcome.AutoSize = true;
            this.chkWelcome.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWelcome.Location = new System.Drawing.Point(579, 380);
            this.chkWelcome.Name = "chkWelcome";
            this.chkWelcome.Size = new System.Drawing.Size(137, 20);
            this.chkWelcome.TabIndex = 17;
            this.chkWelcome.Text = "send as welcome";
            this.chkWelcome.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(80, 381);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 14;
            this.label2.Text = "Message";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.AliceBlue;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(616, 435);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(92, 93);
            this.btnSend.TabIndex = 16;
            this.btnSend.Text = "SEND";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(167, 381);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(406, 158);
            this.txtMessage.TabIndex = 15;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pgInbox);
            this.tabControl1.Controls.Add(this.pgSent);
            this.tabControl1.Location = new System.Drawing.Point(12, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(854, 280);
            this.tabControl1.TabIndex = 10;
            // 
            // pgInbox
            // 
            this.pgInbox.Controls.Add(this.grdInbox);
            this.pgInbox.Location = new System.Drawing.Point(4, 22);
            this.pgInbox.Name = "pgInbox";
            this.pgInbox.Padding = new System.Windows.Forms.Padding(3);
            this.pgInbox.Size = new System.Drawing.Size(846, 254);
            this.pgInbox.TabIndex = 1;
            this.pgInbox.Text = "Inbox";
            this.pgInbox.UseVisualStyleBackColor = true;
            // 
            // grdInbox
            // 
            this.grdInbox.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.grdInbox.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdInbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdInbox.Location = new System.Drawing.Point(3, 3);
            this.grdInbox.Name = "grdInbox";
            this.grdInbox.RowHeadersVisible = false;
            this.grdInbox.RowTemplate.Height = 44;
            this.grdInbox.Size = new System.Drawing.Size(840, 248);
            this.grdInbox.TabIndex = 11;
            // 
            // pgSent
            // 
            this.pgSent.Controls.Add(this.grdSent);
            this.pgSent.Location = new System.Drawing.Point(4, 22);
            this.pgSent.Name = "pgSent";
            this.pgSent.Size = new System.Drawing.Size(846, 254);
            this.pgSent.TabIndex = 2;
            this.pgSent.Text = "Sent";
            this.pgSent.UseVisualStyleBackColor = true;
            // 
            // grdSent
            // 
            this.grdSent.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.grdSent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSent.Location = new System.Drawing.Point(0, 0);
            this.grdSent.Name = "grdSent";
            this.grdSent.RowHeadersVisible = false;
            this.grdSent.RowTemplate.Height = 44;
            this.grdSent.Size = new System.Drawing.Size(846, 254);
            this.grdSent.TabIndex = 11;
            // 
            // frmControllerInternalMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 590);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radLabel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmControllerInternalMessages";
            this.ShowIcon = false;
            this.Text = "Message to Controller";
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.pgInbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdInbox)).EndInit();
            this.pgSent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage pgInbox;
        private System.Windows.Forms.DataGridView grdInbox;
        private System.Windows.Forms.TabPage pgSent;
        private System.Windows.Forms.DataGridView grdSent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ddlGroup;
        private System.Windows.Forms.ComboBox ddlController;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.CheckBox chkWelcome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtMessage;
    }
}