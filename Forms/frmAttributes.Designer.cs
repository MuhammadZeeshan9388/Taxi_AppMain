namespace Taxi_AppMain
{
    partial class frmAttributes
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
            this.txtAtrributeName = new Telerik.WinControls.UI.RadTextBox();
            this.grdAttributes = new Telerik.WinControls.UI.RadGridView();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtShortName = new Telerik.WinControls.UI.RadTextBox();
            this.chkIsDefault = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAtrributeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttributes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttributes.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDefault)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(598, 148);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(420, 119);
            this.btnOnNew.Click += new System.EventHandler(this.btnOnNew_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(623, 0);
            this.btnExit.Size = new System.Drawing.Size(77, 38);
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(620, 119);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(515, 119);
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(17, 79);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(109, 18);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Attribute Name";
            // 
            // txtAtrributeName
            // 
            this.txtAtrributeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.txtAtrributeName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAtrributeName.Location = new System.Drawing.Point(132, 79);
            this.txtAtrributeName.MaxLength = 100;
            this.txtAtrributeName.Name = "txtAtrributeName";
            this.txtAtrributeName.Size = new System.Drawing.Size(334, 20);
            this.txtAtrributeName.TabIndex = 2;
            this.txtAtrributeName.TabStop = false;
            this.txtAtrributeName.TextChanged += new System.EventHandler(this.txtAtrributeName_TextChanged);
            // 
            // grdAttributes
            // 
            this.grdAttributes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.grdAttributes.EnableHotTracking = false;
            this.grdAttributes.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAttributes.Location = new System.Drawing.Point(12, 235);
            // 
            // grdAttributes
            // 
            this.grdAttributes.MasterTemplate.AllowAddNewRow = false;
            this.grdAttributes.Name = "grdAttributes";
            this.grdAttributes.ShowGroupPanel = false;
            this.grdAttributes.Size = new System.Drawing.Size(686, 412);
            this.grdAttributes.TabIndex = 1;
            this.grdAttributes.Text = "Attributes";
            this.grdAttributes.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdAttributes_CellDoubleClick);
            this.grdAttributes.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(this.grdAttributes_RowsChanging);
            // 
            // radLabel2
            // 
            this.radLabel2.AutoSize = false;
            this.radLabel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.ForeColor = System.Drawing.Color.White;
            this.radLabel2.Location = new System.Drawing.Point(12, 210);
            this.radLabel2.Name = "radLabel2";
            // 
            // 
            // 
            this.radLabel2.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel2.Size = new System.Drawing.Size(687, 24);
            this.radLabel2.TabIndex = 107;
            this.radLabel2.Text = "Attributes List";
            this.radLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radLabel2.Click += new System.EventHandler(this.radLabel2_Click);
            // 
            // radLabel3
            // 
            this.radLabel3.AutoSize = false;
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(17, 115);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(109, 18);
            this.radLabel3.TabIndex = 108;
            this.radLabel3.Text = "Short Name";
            // 
            // txtShortName
            // 
            this.txtShortName.Location = new System.Drawing.Point(132, 114);
            this.txtShortName.MaxLength = 20;
            this.txtShortName.Name = "txtShortName";
            this.txtShortName.Size = new System.Drawing.Size(191, 20);
            this.txtShortName.TabIndex = 2;
            this.txtShortName.TabStop = false;
            // 
            // chkIsDefault
            // 
            this.chkIsDefault.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsDefault.Location = new System.Drawing.Point(17, 148);
            this.chkIsDefault.Name = "chkIsDefault";
            // 
            // 
            // 
            this.chkIsDefault.RootElement.StretchHorizontally = true;
            this.chkIsDefault.RootElement.StretchVertically = true;
            this.chkIsDefault.Size = new System.Drawing.Size(109, 18);
            this.chkIsDefault.TabIndex = 109;
            this.chkIsDefault.Text = "Is Default";
            this.chkIsDefault.Visible = false;
            // 
            // frmDriverAttributes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(704, 652);
            this.Controls.Add(this.chkIsDefault);
            this.Controls.Add(this.txtShortName);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.txtAtrributeName);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.grdAttributes);
            this.FixedExitButtonOnTopRight = true;
            this.FormTitle = "Attributes";
            this.Name = "frmAttributes";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowAddNewButton = true;
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.ShowSaveAndCloseButton = true;
            this.ShowSaveAndNewButton = true;
            this.Text = "Attributes";
            this.Load += new System.EventHandler(this.frmAttributes_Load);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.grdAttributes, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.radLabel2, 0);
            this.Controls.SetChildIndex(this.radLabel1, 0);
            this.Controls.SetChildIndex(this.txtAtrributeName, 0);
            this.Controls.SetChildIndex(this.radLabel3, 0);
            this.Controls.SetChildIndex(this.txtShortName, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.chkIsDefault, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAtrributeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttributes.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttributes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtShortName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsDefault)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtAtrributeName;
        private Telerik.WinControls.UI.RadGridView grdAttributes;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadTextBox txtShortName;
        private Telerik.WinControls.UI.RadCheckBox chkIsDefault;
    }
}
