namespace Taxi_AppMain
{
    partial class frmDriverShiftsList
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
            this.radLabel24 = new Telerik.WinControls.UI.RadLabel();
            this.btnAddShift = new Telerik.WinControls.UI.RadButton();
            this.btnAddNewWebLogin = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel24)).BeginInit();
            this.radLabel24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewWebLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = false;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 75);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.EnableFiltering = true;
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(768, 440);
            this.grdLister.TabIndex = 123;
            this.grdLister.Text = "myGridView1";
            // 
            // radLabel24
            // 
            this.radLabel24.AutoSize = false;
            this.radLabel24.BackColor = System.Drawing.Color.SteelBlue;
            this.radLabel24.Controls.Add(this.btnAddShift);
            this.radLabel24.Controls.Add(this.btnAddNewWebLogin);
            this.radLabel24.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel24.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold);
            this.radLabel24.ForeColor = System.Drawing.Color.Gainsboro;
            this.radLabel24.Location = new System.Drawing.Point(0, 38);
            this.radLabel24.Name = "radLabel24";
            // 
            // 
            // 
            this.radLabel24.RootElement.ForeColor = System.Drawing.Color.Gainsboro;
            this.radLabel24.Size = new System.Drawing.Size(768, 37);
            this.radLabel24.TabIndex = 124;
            this.radLabel24.Text = "Shifts List";
            this.radLabel24.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAddShift
            // 
            this.btnAddShift.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAddShift.Location = new System.Drawing.Point(615, 7);
            this.btnAddShift.Name = "btnAddShift";
            this.btnAddShift.Size = new System.Drawing.Size(141, 24);
            this.btnAddShift.TabIndex = 65;
            this.btnAddShift.Text = "Add Shift";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddShift.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.add;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddShift.GetChildAt(0))).Text = "Add Shift";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddShift.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnAddNewWebLogin
            // 
            this.btnAddNewWebLogin.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAddNewWebLogin.Location = new System.Drawing.Point(773, 0);
            this.btnAddNewWebLogin.Name = "btnAddNewWebLogin";
            this.btnAddNewWebLogin.Size = new System.Drawing.Size(141, 24);
            this.btnAddNewWebLogin.TabIndex = 64;
            this.btnAddNewWebLogin.Text = "Add New";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewWebLogin.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.add;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewWebLogin.GetChildAt(0))).Text = "Add New";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddNewWebLogin.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddNewWebLogin.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmDriverShiftsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 515);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.radLabel24);
            this.FormTitle = "Shifts List";
            this.Name = "frmDriverShiftsList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Shifts List";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radLabel24, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel24)).EndInit();
            this.radLabel24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAddShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewWebLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private UI.MyGridView grdLister;
        private Telerik.WinControls.UI.RadLabel radLabel24;
        private Telerik.WinControls.UI.RadButton btnAddNewWebLogin;
        private Telerik.WinControls.UI.RadButton btnAddShift;
    }
}