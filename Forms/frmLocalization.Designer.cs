namespace Taxi_AppMain
{
    partial class frmLocalization
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
            this.btnSaveClose = new Telerik.WinControls.UI.RadButton();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.btnMoveDownZone = new Telerik.WinControls.UI.RadButton();
            this.btnMoveUp = new Telerik.WinControls.UI.RadButton();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.btnAddPostCode = new Telerik.WinControls.UI.RadButton();
            this.txtPostCode = new Telerik.WinControls.UI.RadTextBox();
            this.lblPostCode = new Telerik.WinControls.UI.RadLabel();
            this.grdPostCodes = new UI.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(508, 117);
            this.btnOnNew.TabIndex = 5;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(505, 265);
            this.btnSaveAndClose.TabIndex = 4;
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(595, 110);
            this.btnSaveAndNew.TabIndex = 3;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.btnSaveClose);
            this.radPanel1.Controls.Add(this.btnExit1);
            this.radPanel1.Controls.Add(this.radPanel2);
            this.radPanel1.Location = new System.Drawing.Point(12, 47);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(688, 614);
            this.radPanel1.TabIndex = 106;
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnSaveClose.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveClose.Location = new System.Drawing.Point(484, 219);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(79, 56);
            this.btnSaveClose.TabIndex = 23;
            this.btnSaveClose.Text = "Save && Close";
            this.btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).Text = "Save && Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExit1
            // 
            this.btnExit1.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit1.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit1.Location = new System.Drawing.Point(586, 218);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(79, 56);
            this.btnExit1.TabIndex = 22;
            this.btnExit1.Text = "Exit";
            this.btnExit1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.radPanel2.Location = new System.Drawing.Point(11, 3);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(429, 604);
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
            this.btnClear.Location = new System.Drawing.Point(342, 14);
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
            this.btnAddPostCode.Location = new System.Drawing.Point(261, 14);
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
            this.txtPostCode.Location = new System.Drawing.Point(90, 14);
            this.txtPostCode.MaxLength = 6;
            this.txtPostCode.Name = "txtPostCode";
            this.txtPostCode.Size = new System.Drawing.Size(153, 22);
            this.txtPostCode.TabIndex = 20;
            this.txtPostCode.TabStop = false;
            // 
            // lblPostCode
            // 
            this.lblPostCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostCode.Location = new System.Drawing.Point(10, 16);
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
            this.grdPostCodes.Location = new System.Drawing.Point(3, 55);
            this.grdPostCodes.Name = "grdPostCodes";
            this.grdPostCodes.PKFieldColumnName = "";
            this.grdPostCodes.ShowImageOnActionButton = true;
            this.grdPostCodes.Size = new System.Drawing.Size(240, 534);
            this.grdPostCodes.TabIndex = 18;
            this.grdPostCodes.Text = "myGridView1";
            this.grdPostCodes.DoubleClick += new System.EventHandler(this.grdPostCodes_DoubleClick);
            // 
            // frmLocalization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 666);
            this.ControlBox = true;
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Localization";
            this.Name = "frmLocalization";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Localization";
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
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private UI.MyGridView grdPostCodes;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnAddPostCode;
        private Telerik.WinControls.UI.RadTextBox txtPostCode;
        private Telerik.WinControls.UI.RadLabel lblPostCode;
        private Telerik.WinControls.UI.RadButton btnMoveDownZone;
        private Telerik.WinControls.UI.RadButton btnMoveUp;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnSaveClose;
    }
}