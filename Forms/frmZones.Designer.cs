namespace Taxi_AppMain
{
    partial class frmZones
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.ddlKind = new UI.MyDropDownList();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.ddlType = new UI.MyDropDownList();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.lblShort = new Telerik.WinControls.UI.RadLabel();
            this.txtShortName = new Telerik.WinControls.UI.RadTextBox();
            this.chkBase = new Telerik.WinControls.UI.RadCheckBox();
            this.txtArea = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
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
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlKind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(508, 265);
            this.btnOnNew.TabIndex = 5;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(690, 265);
            this.btnSaveAndClose.TabIndex = 4;
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(595, 265);
            this.btnSaveAndNew.TabIndex = 3;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.ddlKind);
            this.radPanel1.Controls.Add(this.radLabel5);
            this.radPanel1.Controls.Add(this.ddlType);
            this.radPanel1.Controls.Add(this.radLabel3);
            this.radPanel1.Controls.Add(this.lblShort);
            this.radPanel1.Controls.Add(this.txtShortName);
            this.radPanel1.Controls.Add(this.chkBase);
            this.radPanel1.Controls.Add(this.txtArea);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Controls.Add(this.radLabel4);
            this.radPanel1.Controls.Add(this.radPanel2);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Controls.Add(this.txtZoneName);
            this.radPanel1.Location = new System.Drawing.Point(12, 47);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(772, 296);
            this.radPanel1.TabIndex = 106;
            // 
            // ddlKind
            // 
            this.ddlKind.Caption = null;
            this.ddlKind.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlKind.Location = new System.Drawing.Point(594, 86);
            this.ddlKind.Name = "ddlKind";
            this.ddlKind.Property = null;
            this.ddlKind.ShowDownArrow = true;
            this.ddlKind.Size = new System.Drawing.Size(164, 26);
            this.ddlKind.TabIndex = 243;
            // 
            // radLabel5
            // 
            this.radLabel5.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(493, 88);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(78, 22);
            this.radLabel5.TabIndex = 242;
            this.radLabel5.Text = "Category";
            // 
            // ddlType
            // 
            this.ddlType.Caption = null;
            this.ddlType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlType.Location = new System.Drawing.Point(594, 49);
            this.ddlType.Name = "ddlType";
            this.ddlType.Property = null;
            this.ddlType.ShowDownArrow = true;
            this.ddlType.Size = new System.Drawing.Size(164, 26);
            this.ddlType.TabIndex = 241;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(493, 51);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(45, 22);
            this.radLabel3.TabIndex = 31;
            this.radLabel3.Text = "Type";
            // 
            // lblShort
            // 
            this.lblShort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShort.Location = new System.Drawing.Point(493, 16);
            this.lblShort.Name = "lblShort";
            this.lblShort.Size = new System.Drawing.Size(99, 22);
            this.lblShort.TabIndex = 30;
            this.lblShort.Text = "Short Name";
            // 
            // txtShortName
            // 
            this.txtShortName.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtShortName.Location = new System.Drawing.Point(594, 17);
            this.txtShortName.MaxLength = 50;
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(164, 22);
            this.txtShortName.TabIndex = 29;
            this.txtShortName.TabStop = false;
            // 
            // chkBase
            // 
            this.chkBase.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBase.Location = new System.Drawing.Point(282, 15);
            this.chkBase.Name = "chkBase";
            this.chkBase.Size = new System.Drawing.Size(58, 22);
            this.chkBase.TabIndex = 28;
            this.chkBase.Text = "Base";
            // 
            // txtArea
            // 
            this.txtArea.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtArea.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtArea.Location = new System.Drawing.Point(521, 177);
            this.txtArea.MaxLength = 50;
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(102, 22);
            this.txtArea.TabIndex = 26;
            this.txtArea.TabStop = false;
            this.txtArea.Visible = false;
            // 
            // radLabel2
            // 
            this.radLabel2.AutoSize = false;
            this.radLabel2.BackColor = System.Drawing.Color.NavajoWhite;
            this.radLabel2.BorderVisible = true;
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(10, 51);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(430, 18);
            this.radLabel2.TabIndex = 20;
            this.radLabel2.Text = "Associated Post Codes";
            // 
            // radLabel4
            // 
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(482, 177);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(34, 19);
            this.radLabel4.TabIndex = 25;
            this.radLabel4.Text = "Area";
            this.radLabel4.Visible = false;
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
            this.radPanel2.Size = new System.Drawing.Size(429, 237);
            this.radPanel2.TabIndex = 19;
            // 
            // btnMoveDownZone
            // 
            this.btnMoveDownZone.Image = global::Taxi_AppMain.Properties.Resources.lc_movedown;
            this.btnMoveDownZone.Location = new System.Drawing.Point(291, 152);
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
            this.btnMoveUp.Location = new System.Drawing.Point(291, 105);
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
            this.btnClear.Location = new System.Drawing.Point(342, 35);
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
            this.btnAddPostCode.Location = new System.Drawing.Point(261, 35);
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
            this.txtPostCode.MaxLength = 8;
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(97, 22);
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
            // frmZones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 354);
            this.Controls.Add(this.radPanel1);
            this.FormTitle = "Zones";
            this.Name = "frmZones";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowAddNewButton = true;
            this.ShowHeader = true;
            this.ShowSaveAndCloseButton = true;
            this.ShowSaveAndNewButton = true;
            this.Text = "Zones";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmZones_FormClosed);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ddlKind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
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
        private Telerik.WinControls.UI.RadTextBox txtArea;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadCheckBox chkBase;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel lblShort;
        private Telerik.WinControls.UI.RadTextBox txtShortName;
        private UI.MyDropDownList ddlType;
        private UI.MyDropDownList ddlKind;
        private Telerik.WinControls.UI.RadLabel radLabel5;
    }
}