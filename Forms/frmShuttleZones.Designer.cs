namespace Taxi_AppMain
{
    partial class frmShuttleZones
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn2 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            this.grdAllowedPickups = new UI.MyGridView();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.btnMoveDownZone = new Telerik.WinControls.UI.RadButton();
            this.btnMoveUp = new Telerik.WinControls.UI.RadButton();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.btnAddPostCode = new Telerik.WinControls.UI.RadButton();
            this.txtPostCode = new Telerik.WinControls.UI.RadTextBox();
            this.lblPostCode = new Telerik.WinControls.UI.RadLabel();
            this.grdPostCodes = new UI.MyGridView();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtZoneName = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel9 = new Telerik.WinControls.UI.RadLabel();
            this.grdLister = new UI.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAllowedPickups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAllowedPickups.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveDownZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPostCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPostCodes.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZoneName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(473, 361);
            this.btnOnNew.TabIndex = 5;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(694, 359);
            this.btnSaveAndClose.TabIndex = 4;
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(580, 360);
            this.btnSaveAndNew.TabIndex = 3;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.radGroupBox1);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Controls.Add(this.radPanel2);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Controls.Add(this.txtZoneName);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(898, 401);
            this.radPanel1.TabIndex = 106;
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.grdAllowedPickups);
            this.radGroupBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.GroupBoxStyle = Telerik.WinControls.UI.RadGroupBoxStyle.Office;
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Allow Pickups";
            this.radGroupBox1.Location = new System.Drawing.Point(414, 51);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(443, 237);
            this.radGroupBox1.TabIndex = 21;
            this.radGroupBox1.Text = "Allow Pickups";
            // 
            // grdAllowedPickups
            // 
            this.grdAllowedPickups.AutoCellFormatting = false;
            this.grdAllowedPickups.EnableCheckInCheckOut = false;
            this.grdAllowedPickups.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdAllowedPickups.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdAllowedPickups.Location = new System.Drawing.Point(5, 23);
            // 
            // grdAllowedPickups
            // 
            this.grdAllowedPickups.MasterTemplate.AllowAddNewRow = false;
            this.grdAllowedPickups.MasterTemplate.AllowColumnReorder = false;
            gridViewTextBoxColumn6.FormatString = "";
            gridViewTextBoxColumn6.HeaderText = "Id";
            gridViewTextBoxColumn6.IsVisible = false;
            gridViewTextBoxColumn6.Name = "Id";
            gridViewTextBoxColumn7.FormatString = "";
            gridViewTextBoxColumn7.HeaderText = "column2";
            gridViewTextBoxColumn7.IsVisible = false;
            gridViewTextBoxColumn7.Name = "MasterId";
            gridViewTextBoxColumn8.FormatString = "";
            gridViewTextBoxColumn8.HeaderText = "column3";
            gridViewTextBoxColumn8.IsVisible = false;
            gridViewTextBoxColumn8.Name = "AllowedZoneId";
            gridViewTextBoxColumn9.FormatString = "";
            gridViewTextBoxColumn9.HeaderText = "Zones";
            gridViewTextBoxColumn9.Name = "AllowedZoneName";
            gridViewTextBoxColumn9.Width = 80;
            gridViewTextBoxColumn10.FormatString = "";
            gridViewTextBoxColumn10.HeaderText = "PostCodes";
            gridViewTextBoxColumn10.Name = "PostCodes";
            gridViewTextBoxColumn10.Width = 260;
            gridViewTextBoxColumn10.WrapText = true;
            gridViewCheckBoxColumn2.HeaderText = "Allowed";
            gridViewCheckBoxColumn2.Name = "IsAllowed";
            gridViewCheckBoxColumn2.Width = 80;
            this.grdAllowedPickups.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewCheckBoxColumn2});
            this.grdAllowedPickups.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdAllowedPickups.Name = "grdAllowedPickups";
            this.grdAllowedPickups.PKFieldColumnName = "";
            this.grdAllowedPickups.ShowGroupPanel = false;
            this.grdAllowedPickups.ShowImageOnActionButton = true;
            this.grdAllowedPickups.Size = new System.Drawing.Size(425, 211);
            this.grdAllowedPickups.TabIndex = 271;
            this.grdAllowedPickups.Text = "myGridView1";
            // 
            // radLabel2
            // 
            this.radLabel2.AutoSize = false;
            this.radLabel2.BackColor = System.Drawing.Color.NavajoWhite;
            this.radLabel2.BorderVisible = true;
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(10, 51);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(390, 18);
            this.radLabel2.TabIndex = 20;
            this.radLabel2.Text = "Associated Post Codes";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.btnMoveDownZone);
            this.radPanel2.Controls.Add(this.btnMoveUp);
            this.radPanel2.Controls.Add(this.btnClear);
            this.radPanel2.Controls.Add(this.btnAddPostCode);
            this.radPanel2.Controls.Add(this.txtPostCode);
            this.radPanel2.Controls.Add(this.lblPostCode);
            this.radPanel2.Controls.Add(this.grdPostCodes);
            this.radPanel2.Location = new System.Drawing.Point(11, 51);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(389, 237);
            this.radPanel2.TabIndex = 19;
            // 
            // btnMoveDownZone
            // 
            this.btnMoveDownZone.Image = global::Taxi_AppMain.Properties.Resources.lc_movedown;
            this.btnMoveDownZone.Location = new System.Drawing.Point(256, 152);
            this.btnMoveDownZone.Name = "btnMoveDownZone";
            this.btnMoveDownZone.Size = new System.Drawing.Size(120, 28);
            this.btnMoveDownZone.TabIndex = 24;
            this.btnMoveDownZone.Text = "Move Down";
            this.btnMoveDownZone.Click += new System.EventHandler(this.btnMoveDownZone_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMoveDownZone.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.lc_movedown;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMoveDownZone.GetChildAt(0))).Text = "Move Down";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveDownZone.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveDownZone.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Image = global::Taxi_AppMain.Properties.Resources.lc_moveup;
            this.btnMoveUp.Location = new System.Drawing.Point(256, 105);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(120, 28);
            this.btnMoveUp.TabIndex = 23;
            this.btnMoveUp.Text = "Move Up";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMoveUp.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.lc_moveup;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnMoveUp.GetChildAt(0))).Text = "Move Up";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveUp.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnMoveUp.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(299, 35);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 22);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "New";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClear.GetChildAt(0))).Text = "New";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClear.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClear.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnAddPostCode
            // 
            this.btnAddPostCode.Location = new System.Drawing.Point(218, 35);
            this.btnAddPostCode.Name = "btnAddPostCode";
            this.btnAddPostCode.Size = new System.Drawing.Size(69, 22);
            this.btnAddPostCode.TabIndex = 21;
            this.btnAddPostCode.Text = "Add";
            this.btnAddPostCode.Click += new System.EventHandler(this.btnAddPostCode_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddPostCode.GetChildAt(0))).Text = "Add";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddPostCode.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddPostCode.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtPostCode
            // 
            this.txtPostCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPostCode.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtPostCode.Location = new System.Drawing.Point(115, 35);
            this.txtPostCode.MaxLength = 6;
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(77, 22);
            this.txtPostCode.TabIndex = 20;
            this.txtPostCode.TabStop = false;
            // 
            // lblPostCode
            // 
            this.lblPostCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostCode.Location = new System.Drawing.Point(10, 37);
            this.lblPostCode.Name = "lblPostCode";
            this.lblPostCode.Size = new System.Drawing.Size(74, 19);
            this.lblPostCode.TabIndex = 19;
            this.lblPostCode.Text = "Post Code";
            // 
            // grdPostCodes
            // 
            this.grdPostCodes.AutoCellFormatting = false;
            this.grdPostCodes.EnableCheckInCheckOut = false;
            this.grdPostCodes.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdPostCodes.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdPostCodes.Location = new System.Drawing.Point(3, 72);
            this.grdPostCodes.Name = "grdPostCodes";
            this.grdPostCodes.PKFieldColumnName = "";
            this.grdPostCodes.ShowImageOnActionButton = true;
            this.grdPostCodes.Size = new System.Drawing.Size(240, 163);
            this.grdPostCodes.TabIndex = 18;
            this.grdPostCodes.Text = "myGridView1";
            this.grdPostCodes.DoubleClick += new System.EventHandler(this.grdPostCodes_DoubleClick);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(8, 15);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(95, 22);
            this.radLabel1.TabIndex = 17;
            this.radLabel1.Text = "Zone Name";
            // 
            // txtZoneName
            // 
            this.txtZoneName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtZoneName.Location = new System.Drawing.Point(106, 16);
            this.txtZoneName.MaxLength = 50;
            this.txtZoneName.Name = "txtZoneName";
            this.txtZoneName.Size = new System.Drawing.Size(164, 22);
            this.txtZoneName.TabIndex = 1;
            this.txtZoneName.TabStop = false;
            this.txtZoneName.Validated += new System.EventHandler(this.txtZoneName_Validated);
            // 
            // radLabel9
            // 
            this.radLabel9.AutoSize = false;
            this.radLabel9.BackColor = System.Drawing.Color.DodgerBlue;
            this.radLabel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel9.ForeColor = System.Drawing.Color.White;
            this.radLabel9.Location = new System.Drawing.Point(0, 439);
            this.radLabel9.Name = "radLabel9";
            // 
            // 
            // 
            this.radLabel9.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel9.Size = new System.Drawing.Size(898, 22);
            this.radLabel9.TabIndex = 107;
            this.radLabel9.Text = "Shuttle Zones List";
            this.radLabel9.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = true;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.EnableHotTracking = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 461);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(898, 325);
            this.grdLister.TabIndex = 114;
            this.grdLister.Text = "myGridView1";
            // 
            // frmShuttleZones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 786);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.radLabel9);
            this.Controls.Add(this.radPanel1);
            this.FormTitle = "Shuttle Zones List";
            this.Name = "frmShuttleZones";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowAddNewButton = true;
            this.ShowHeader = true;
            this.ShowSaveAndCloseButton = true;
            this.ShowSaveAndNewButton = true;
            this.Text = "Shuttle Zones";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmZones_FormClosed);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.radLabel9, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAllowedPickups.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAllowedPickups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveDownZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoveUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPostCodes.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPostCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtZoneName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtZoneName;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private UI.MyGridView grdPostCodes;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnAddPostCode;
        private Telerik.WinControls.UI.RadTextBox txtPostCode;
        private Telerik.WinControls.UI.RadLabel lblPostCode;
        private Telerik.WinControls.UI.RadButton btnMoveDownZone;
        private Telerik.WinControls.UI.RadButton btnMoveUp;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
        private UI.MyGridView grdAllowedPickups;
        private Telerik.WinControls.UI.RadLabel radLabel9;
        private UI.MyGridView grdLister;
    }
}