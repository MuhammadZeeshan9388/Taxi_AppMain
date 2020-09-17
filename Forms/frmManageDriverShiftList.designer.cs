namespace Taxi_AppMain
{
    partial class frmManageDriverShiftList
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
            this.lbFormlHeading = new System.Windows.Forms.Label();
            this.btnAddDriverShift = new Telerik.WinControls.UI.RadButton();
            this.btnDeleteShift = new Telerik.WinControls.UI.RadButton();
            this.grdDriverShift = new Taxi_AppMain.AdvancedGridView();
            this.chkDelete = new Telerik.WinControls.UI.RadCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddDriverShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShift.MasterTemplate)).BeginInit();
            this.grdDriverShift.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lbFormlHeading
            // 
            this.lbFormlHeading.BackColor = System.Drawing.Color.SteelBlue;
            this.lbFormlHeading.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbFormlHeading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbFormlHeading.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFormlHeading.ForeColor = System.Drawing.Color.White;
            this.lbFormlHeading.Location = new System.Drawing.Point(0, 38);
            this.lbFormlHeading.Name = "lbFormlHeading";
            this.lbFormlHeading.Size = new System.Drawing.Size(1050, 33);
            this.lbFormlHeading.TabIndex = 107;
            this.lbFormlHeading.Text = "Manage Driver Shifts";
            this.lbFormlHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddDriverShift
            // 
            this.btnAddDriverShift.Location = new System.Drawing.Point(915, 6);
            this.btnAddDriverShift.Name = "btnAddDriverShift";
            this.btnAddDriverShift.Size = new System.Drawing.Size(123, 24);
            this.btnAddDriverShift.TabIndex = 108;
            this.btnAddDriverShift.Text = "Add Driver Shift";
            this.btnAddDriverShift.Click += new System.EventHandler(this.btnAddDriverShift_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddDriverShift.GetChildAt(0))).Text = "Add Driver Shift";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddDriverShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddDriverShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDeleteShift
            // 
            this.btnDeleteShift.Location = new System.Drawing.Point(797, 6);
            this.btnDeleteShift.Name = "btnDeleteShift";
            this.btnDeleteShift.Size = new System.Drawing.Size(110, 24);
            this.btnDeleteShift.TabIndex = 109;
            this.btnDeleteShift.Text = "Delete Shift";
            this.btnDeleteShift.Click += new System.EventHandler(this.btnDeleteShift_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDeleteShift.GetChildAt(0))).Text = "Delete Shift";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDeleteShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdDriverShift
            // 
            this.grdDriverShift.Controls.Add(this.chkDelete);
            this.grdDriverShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDriverShift.EnableHotTracking = false;
            this.grdDriverShift.Location = new System.Drawing.Point(0, 71);
            // 
            // grdDriverShift
            // 
            this.grdDriverShift.MasterTemplate.EnableFiltering = true;
            this.grdDriverShift.Name = "grdDriverShift";
            this.grdDriverShift.Size = new System.Drawing.Size(1050, 805);
            this.grdDriverShift.TabIndex = 106;
            this.grdDriverShift.Text = "myGridView1";
            // 
            // chkDelete
            // 
            this.chkDelete.Location = new System.Drawing.Point(84, 6);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(35, 19);
            this.chkDelete.TabIndex = 0;
            this.chkDelete.Text = "All";
            this.chkDelete.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkDelete_ToggleStateChanged);
            ((Telerik.WinControls.UI.RadCheckBoxElement)(this.chkDelete.GetChildAt(0))).Text = "All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.chkDelete.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.chkDelete.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmManageDriverShiftList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 876);
            this.Controls.Add(this.btnDeleteShift);
            this.Controls.Add(this.grdDriverShift);
            this.Controls.Add(this.btnAddDriverShift);
            this.Controls.Add(this.lbFormlHeading);
            this.FormTitle = "Manage Driver Shifts";
            this.Name = "frmManageDriverShiftList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Manage Driver Shifts";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.lbFormlHeading, 0);
            this.Controls.SetChildIndex(this.btnAddDriverShift, 0);
            this.Controls.SetChildIndex(this.grdDriverShift, 0);
            this.Controls.SetChildIndex(this.btnDeleteShift, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddDriverShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShift.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShift)).EndInit();
            this.grdDriverShift.ResumeLayout(false);
            this.grdDriverShift.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AdvancedGridView grdDriverShift;
        private System.Windows.Forms.Label lbFormlHeading;
        private Telerik.WinControls.UI.RadButton btnAddDriverShift;
        private Telerik.WinControls.UI.RadCheckBox chkDelete;
        private Telerik.WinControls.UI.RadButton btnDeleteShift;
    }
}