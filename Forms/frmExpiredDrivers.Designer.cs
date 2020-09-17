namespace Taxi_AppMain.Forms
{
    partial class frmExpiredDrivers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExpiredDrivers));
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            this.radPanel5 = new Telerik.WinControls.UI.RadPanel();
            this.btnShowAll2 = new Telerik.WinControls.UI.RadButton();
            this.btnFind2 = new Telerik.WinControls.UI.RadButton();
            this.txtSearch2 = new Telerik.WinControls.UI.RadTextBox();
            this.ddlColumns2 = new Telerik.WinControls.UI.RadDropDownList();
            this.label3 = new System.Windows.Forms.Label();
            this.grdLister = new UI.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).BeginInit();
            this.radPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowAll2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFind2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColumns2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(84, 858);
            this.btnSaveOn.Size = new System.Drawing.Size(70, 10);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(8, 858);
            this.btnOnNew.Size = new System.Drawing.Size(70, 10);
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.Location = new System.Drawing.Point(986, 0);
            this.btnExit.Size = new System.Drawing.Size(77, 38);
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(236, 858);
            this.btnSaveAndClose.Size = new System.Drawing.Size(79, 10);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(160, 855);
            this.btnSaveAndNew.Size = new System.Drawing.Size(79, 10);
            // 
            // radPanel5
            // 
            this.radPanel5.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel5.Controls.Add(this.btnShowAll2);
            this.radPanel5.Controls.Add(this.btnFind2);
            this.radPanel5.Controls.Add(this.txtSearch2);
            this.radPanel5.Controls.Add(this.ddlColumns2);
            this.radPanel5.Controls.Add(this.label3);
            this.radPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel5.Location = new System.Drawing.Point(0, 38);
            this.radPanel5.Name = "radPanel5";
            this.radPanel5.Size = new System.Drawing.Size(1150, 35);
            this.radPanel5.TabIndex = 127;
            // 
            // btnShowAll2
            // 
            this.btnShowAll2.Location = new System.Drawing.Point(475, 5);
            this.btnShowAll2.Name = "btnShowAll2";
            this.btnShowAll2.Size = new System.Drawing.Size(138, 24);
            this.btnShowAll2.TabIndex = 9;
            this.btnShowAll2.Tag = "";
            this.btnShowAll2.Text = "Show All Drivers";
            this.btnShowAll2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnShowAll2.Click += new System.EventHandler(this.btnShowAll2_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAll2.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAll2.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAll2.GetChildAt(0))).Text = "Show All Drivers";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowAll2.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowAll2.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnFind2
            // 
            this.btnFind2.Image = global::Taxi_AppMain.Properties.Resources.pic_Search;
            this.btnFind2.Location = new System.Drawing.Point(389, 5);
            this.btnFind2.Name = "btnFind2";
            this.btnFind2.Size = new System.Drawing.Size(59, 24);
            this.btnFind2.TabIndex = 8;
            this.btnFind2.Tag = "";
            this.btnFind2.Text = "Find";
            this.btnFind2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFind2.Click += new System.EventHandler(this.btnFind2_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFind2.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.pic_Search;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFind2.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFind2.GetChildAt(0))).Text = "Find";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnFind2.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnFind2.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtSearch2
            // 
            this.txtSearch2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch2.Location = new System.Drawing.Point(92, 7);
            this.txtSearch2.Name = "txtSearch2";
            this.txtSearch2.Size = new System.Drawing.Size(166, 21);
            this.txtSearch2.TabIndex = 6;
            this.txtSearch2.TabStop = false;
            // 
            // ddlColumns2
            // 
            this.ddlColumns2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlColumns2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem1.Selected = true;
            radListDataItem1.Text = "Driver Name";
            radListDataItem1.TextWrap = true;
            radListDataItem2.Text = "Driver No";
            radListDataItem2.TextWrap = true;
            radListDataItem3.Text = "Vehicle No";
            radListDataItem3.TextWrap = true;
            this.ddlColumns2.Items.Add(radListDataItem1);
            this.ddlColumns2.Items.Add(radListDataItem2);
            this.ddlColumns2.Items.Add(radListDataItem3);
            this.ddlColumns2.Location = new System.Drawing.Point(264, 6);
            this.ddlColumns2.Name = "ddlColumns2";
            this.ddlColumns2.Size = new System.Drawing.Size(119, 23);
            this.ddlColumns2.TabIndex = 7;
            this.ddlColumns2.Text = "Driver Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Search";
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = true;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.EnableHotTracking = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 73);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(1150, 527);
            this.grdLister.TabIndex = 126;
            this.grdLister.Text = "myGridView1";
            // 
            // frmExpiredDrivers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 600);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.radPanel5);
            this.FixedExitButtonOnTopRight = true;
            this.FormTitle = "Expired Drivers";
            this.Name = "frmExpiredDrivers";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmExpiredDrivers_Load);
            this.Shown += new System.EventHandler(this.frmExpiredDrivers_Shown);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radPanel5, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel5)).EndInit();
            this.radPanel5.ResumeLayout(false);
            this.radPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowAll2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFind2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColumns2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel5;
        private Telerik.WinControls.UI.RadButton btnShowAll2;
        private Telerik.WinControls.UI.RadButton btnFind2;
        private Telerik.WinControls.UI.RadTextBox txtSearch2;
        private Telerik.WinControls.UI.RadDropDownList ddlColumns2;
        private System.Windows.Forms.Label label3;
        private UI.MyGridView grdLister;

    }
}
