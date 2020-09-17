namespace Taxi_AppMain
{
    partial class frmRoutSuggestions
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.grdLister = new Telerik.WinControls.UI.RadGridView();
            this.pnlMain = new Telerik.WinControls.UI.RadPanel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.pnlTop = new Telerik.WinControls.UI.RadPanel();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.pnlLeft = new Telerik.WinControls.UI.RadPanel();
            this.grdAddress = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).BeginInit();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddress.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(966, 626);
            this.webBrowser1.TabIndex = 119;
        //   this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted_1);
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            // 
            // grdLister
            // 
            this.grdLister.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.grdLister.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLister.ForeColor = System.Drawing.Color.DarkBlue;
            this.grdLister.Location = new System.Drawing.Point(-5, 443);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.MasterTemplate.AllowEditRow = false;
            this.grdLister.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdLister.Name = "grdLister";
            // 
            // 
            // 
            this.grdLister.RootElement.ForeColor = System.Drawing.Color.DarkBlue;
            this.grdLister.Size = new System.Drawing.Size(390, 183);
            this.grdLister.TabIndex = 121;
            this.grdLister.Text = "radGridView1";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.radPanel2);
            this.pnlMain.Controls.Add(this.pnlTop);
            this.pnlMain.Controls.Add(this.radPanel1);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Controls.Add(this.grdLister);
            this.pnlMain.Controls.Add(this.webBrowser1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(966, 626);
            this.pnlMain.TabIndex = 122;
            // 
            // radPanel2
            // 
            this.radPanel2.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel2.Location = new System.Drawing.Point(383, 85);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(32, 541);
            this.radPanel2.TabIndex = 125;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanel2.GetChildAt(0).GetChildAt(1))).Width = 0F;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlTop.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlTop.ForeColor = System.Drawing.Color.Blue;
            this.pnlTop.Location = new System.Drawing.Point(-1, 1);
            this.pnlTop.Name = "pnlTop";
            // 
            // 
            // 
            this.pnlTop.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.pnlTop.Size = new System.Drawing.Size(1029, 86);
            this.pnlTop.TabIndex = 122;
            this.pnlTop.Text = "Route Suggesstion";
            this.pnlTop.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.pnlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTop_Paint);
            ((Telerik.WinControls.UI.RadPanelElement)(this.pnlTop.GetChildAt(0))).Text = "Route Suggesstion";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.pnlTop.GetChildAt(0).GetChildAt(1))).Width = 0F;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.White;
            this.radPanel1.Location = new System.Drawing.Point(0, 343);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(392, 100);
            this.radPanel1.TabIndex = 124;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanel1.GetChildAt(0).GetChildAt(1))).Width = 0F;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.grdAddress);
            this.pnlLeft.Location = new System.Drawing.Point(-1, 89);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(380, 260);
            this.pnlLeft.TabIndex = 123;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.pnlLeft.GetChildAt(0).GetChildAt(1))).Width = 0F;
            // 
            // grdAddress
            // 
            this.grdAddress.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAddress.ForeColor = System.Drawing.Color.DarkBlue;
            this.grdAddress.Location = new System.Drawing.Point(0, -8);
            // 
            // grdAddress
            // 
            this.grdAddress.MasterTemplate.AllowAddNewRow = false;
            this.grdAddress.MasterTemplate.AllowEditRow = false;
            this.grdAddress.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdAddress.Name = "grdAddress";
            // 
            // 
            // 
            this.grdAddress.RootElement.ForeColor = System.Drawing.Color.DarkBlue;
            this.grdAddress.Size = new System.Drawing.Size(390, 269);
            this.grdAddress.TabIndex = 122;
            this.grdAddress.Text = "radGridView1";
            this.grdAddress.Click += new System.EventHandler(this.grdAddress_Click);
            // 
            // frmRoutSuggestions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(966, 626);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmRoutSuggestions";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rout Suggestions";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRoutSuggestions_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlLeft)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAddress.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private Telerik.WinControls.UI.RadGridView grdLister;
        private Telerik.WinControls.UI.RadPanel pnlMain;
        private Telerik.WinControls.UI.RadPanel pnlTop;
        private Telerik.WinControls.UI.RadPanel pnlLeft;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadGridView grdAddress;
        private Telerik.WinControls.UI.RadPanel radPanel2;
    }
}