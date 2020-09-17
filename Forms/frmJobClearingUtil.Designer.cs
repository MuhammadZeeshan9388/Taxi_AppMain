namespace Taxi_AppMain
{
    partial class frmJobClearingUtil
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
            this.grdLister = new UI.MyGridView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnClearSelected = new Telerik.WinControls.UI.RadButton();
            this.btnClearAll = new Telerik.WinControls.UI.RadButton();
            this.lblHeading = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(645, 522);
            this.btnExit.Size = new System.Drawing.Size(130, 56);
            this.btnExit.Visible = true;
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = true;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.EnableHotTracking = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 58);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(1000, 438);
            this.grdLister.TabIndex = 117;
            this.grdLister.Text = "myGridView1";
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.btnClearSelected);
            this.radPanel1.Controls.Add(this.btnClearAll);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel1.Location = new System.Drawing.Point(0, 496);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1000, 104);
            this.radPanel1.TabIndex = 116;
            // 
            // btnClearSelected
            // 
            this.btnClearSelected.Image = global::Taxi_AppMain.Properties.Resources.lc_movedown1;
            this.btnClearSelected.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClearSelected.Location = new System.Drawing.Point(194, 26);
            this.btnClearSelected.Name = "btnClearSelected";
            this.btnClearSelected.Size = new System.Drawing.Size(130, 56);
            this.btnClearSelected.TabIndex = 3;
            this.btnClearSelected.Text = "Clear Selected Jobs";
            this.btnClearSelected.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClearSelected.Click += new System.EventHandler(this.btnClearSelected_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearSelected.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.lc_movedown1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearSelected.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearSelected.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearSelected.GetChildAt(0))).Text = "Clear Selected Jobs";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearSelected.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearSelected.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnClearAll
            // 
            this.btnClearAll.Image = global::Taxi_AppMain.Properties.Resources.paste;
            this.btnClearAll.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClearAll.Location = new System.Drawing.Point(413, 26);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(130, 56);
            this.btnClearAll.TabIndex = 2;
            this.btnClearAll.Text = "Clear All Jobs";
            this.btnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearAll.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.paste;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearAll.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearAll.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearAll.GetChildAt(0))).Text = "Clear All Jobs";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblHeading
            // 
            this.lblHeading.AutoSize = false;
            this.lblHeading.BackColor = System.Drawing.Color.Tomato;
            this.lblHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeading.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.Location = new System.Drawing.Point(0, 38);
            this.lblHeading.Name = "lblHeading";
            // 
            // 
            // 
            this.lblHeading.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblHeading.Size = new System.Drawing.Size(1000, 20);
            this.lblHeading.TabIndex = 118;
            // 
            // frmJobClearingUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.radPanel1);
            this.FormTitle = "Job Clearing Utility";
            this.Name = "frmJobClearingUtil";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Job Clearing Utility";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.lblHeading, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClearSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MyGridView grdLister;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnClearSelected;
        private Telerik.WinControls.UI.RadButton btnClearAll;
        private Telerik.WinControls.UI.RadLabel lblHeading;
    }
}