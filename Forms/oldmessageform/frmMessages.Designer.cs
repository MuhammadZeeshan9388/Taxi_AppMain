namespace Taxi_AppMain.Forms
{
    partial class frmMessages
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
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.txtMessage = new Telerik.WinControls.UI.RadTextBox();
            this.btnSend = new Telerik.WinControls.UI.RadButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.txtTitle = new Telerik.WinControls.UI.RadLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlShow = new Telerik.WinControls.UI.RadDropDownList();
            this.txtConversation = new HtmlRichText.HtmlRichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.templatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTemplatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddTemplate = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlShow)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radPanel2
            // 
            this.radPanel2.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel2.Controls.Add(this.txtMessage);
            this.radPanel2.Controls.Add(this.btnSend);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 302);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(506, 63);
            this.radPanel2.TabIndex = 5;
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(4, 3);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            // 
            // 
            // 
            this.txtMessage.RootElement.StretchVertically = true;
            this.txtMessage.Size = new System.Drawing.Size(385, 57);
            this.txtMessage.TabIndex = 4;
            this.txtMessage.TabStop = false;
            this.txtMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMessage_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(395, 9);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(107, 47);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSend.GetChildAt(0))).Text = "Send";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSend.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSend.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTestConn);
            this.panel1.Controls.Add(this.txtTitle);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 43);
            this.panel1.TabIndex = 7;
            // 
            // btnTestConn
            // 
            this.btnTestConn.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnTestConn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTestConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestConn.ForeColor = System.Drawing.Color.White;
            this.btnTestConn.Location = new System.Drawing.Point(118, 0);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(111, 43);
            this.btnTestConn.TabIndex = 6;
            this.btnTestConn.Text = "Test Connection";
            this.btnTestConn.UseVisualStyleBackColor = false;
            this.btnTestConn.Click += new System.EventHandler(this.btnTestConn_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.AutoSize = false;
            this.txtTitle.BackColor = System.Drawing.Color.RoyalBlue;
            this.txtTitle.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtTitle.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTitle.ForeColor = System.Drawing.Color.White;
            this.txtTitle.Location = new System.Drawing.Point(229, 0);
            this.txtTitle.Name = "txtTitle";
            // 
            // 
            // 
            this.txtTitle.RootElement.ForeColor = System.Drawing.Color.White;
            this.txtTitle.Size = new System.Drawing.Size(277, 43);
            this.txtTitle.TabIndex = 4;
            this.txtTitle.Text = "danish jaffery";
            this.txtTitle.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.radLabel1);
            this.panel2.Controls.Add(this.ddlShow);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(118, 43);
            this.panel2.TabIndex = 5;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(1, 0);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(113, 16);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "Show Conversation";
            // 
            // ddlShow
            // 
            this.ddlShow.Location = new System.Drawing.Point(10, 16);
            this.ddlShow.Name = "ddlShow";
            this.ddlShow.Size = new System.Drawing.Size(92, 22);
            this.ddlShow.TabIndex = 0;
            // 
            // txtConversation
            // 
            this.txtConversation.BackColor = System.Drawing.Color.White;
            this.txtConversation.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConversation.Location = new System.Drawing.Point(0, 66);
            this.txtConversation.Name = "txtConversation";
            this.txtConversation.ReadOnly = true;
            this.txtConversation.Size = new System.Drawing.Size(506, 236);
            this.txtConversation.TabIndex = 6;
            this.txtConversation.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.templatesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(506, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // templatesToolStripMenuItem
            // 
            this.templatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTemplatToolStripMenuItem,
            this.btnAddTemplate});
            this.templatesToolStripMenuItem.Name = "templatesToolStripMenuItem";
            this.templatesToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.templatesToolStripMenuItem.Text = "Templates";
            // 
            // addTemplatToolStripMenuItem
            // 
            this.addTemplatToolStripMenuItem.Name = "addTemplatToolStripMenuItem";
            this.addTemplatToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.addTemplatToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.addTemplatToolStripMenuItem.Text = "Add Template";
            this.addTemplatToolStripMenuItem.Click += new System.EventHandler(this.addTemplatToolStripMenuItem_Click);
            // 
            // btnAddTemplate
            // 
            this.btnAddTemplate.Name = "btnAddTemplate";
            this.btnAddTemplate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.btnAddTemplate.Size = new System.Drawing.Size(191, 22);
            this.btnAddTemplate.Text = "Template List";
            this.btnAddTemplate.Click += new System.EventHandler(this.btnAddTemplate_Click);
            // 
            // frmMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(506, 365);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.txtConversation);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmMessages";
            this.ShowIcon = false;
            this.Text = "Conversation";
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTitle)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlShow)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadButton btnSend;
        private HtmlRichText.HtmlRichTextBox txtConversation;
        private Telerik.WinControls.UI.RadTextBox txtMessage;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadLabel txtTitle;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList ddlShow;
        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem templatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnAddTemplate;
        private System.Windows.Forms.ToolStripMenuItem addTemplatToolStripMenuItem;
    }
}