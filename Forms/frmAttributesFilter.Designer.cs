namespace Taxi_AppMain
{
    partial class frmAttributesFilter
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.optAvailableDrivers = new Telerik.WinControls.UI.RadRadioButton();
            this.optLoginDrivers = new Telerik.WinControls.UI.RadRadioButton();
            this.optAllDrivers = new Telerik.WinControls.UI.RadRadioButton();
            this.ddlAttributes = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
            this.grdLister = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optAvailableDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optLoginDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAllDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAttributes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.optAvailableDrivers);
            this.panel1.Controls.Add(this.optLoginDrivers);
            this.panel1.Controls.Add(this.optAllDrivers);
            this.panel1.Controls.Add(this.ddlAttributes);
            this.panel1.Controls.Add(this.radLabel7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 72);
            this.panel1.TabIndex = 106;
            // 
            // optAvailableDrivers
            // 
            this.optAvailableDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optAvailableDrivers.Location = new System.Drawing.Point(44, 42);
            this.optAvailableDrivers.Name = "optAvailableDrivers";
            this.optAvailableDrivers.Size = new System.Drawing.Size(136, 18);
            this.optAvailableDrivers.TabIndex = 200;
            this.optAvailableDrivers.Text = "Available Drivers";
            this.optAvailableDrivers.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            // 
            // optLoginDrivers
            // 
            this.optLoginDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optLoginDrivers.Location = new System.Drawing.Point(181, 42);
            this.optLoginDrivers.Name = "optLoginDrivers";
            this.optLoginDrivers.Size = new System.Drawing.Size(115, 18);
            this.optLoginDrivers.TabIndex = 199;
            this.optLoginDrivers.Text = "Login Drivers";
            // 
            // optAllDrivers
            // 
            this.optAllDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optAllDrivers.Location = new System.Drawing.Point(301, 42);
            this.optAllDrivers.Name = "optAllDrivers";
            this.optAllDrivers.Size = new System.Drawing.Size(106, 18);
            this.optAllDrivers.TabIndex = 198;
            this.optAllDrivers.Text = "All Drivers";
            // 
            // ddlAttributes
            // 
            this.ddlAttributes.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlAttributes.Location = new System.Drawing.Point(182, 10);
            this.ddlAttributes.Name = "ddlAttributes";
            this.ddlAttributes.Size = new System.Drawing.Size(218, 26);
            this.ddlAttributes.TabIndex = 197;
            // 
            // radLabel7
            // 
            this.radLabel7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel7.Location = new System.Drawing.Point(44, 13);
            this.radLabel7.Name = "radLabel7";
            this.radLabel7.Size = new System.Drawing.Size(114, 19);
            this.radLabel7.TabIndex = 196;
            this.radLabel7.Text = "Attributes Filter";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel7.GetChildAt(0))).Text = "Attributes Filter";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel7.GetChildAt(0).GetChildAt(2).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.TopLeft;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radLabel7.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdLister
            // 
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.Location = new System.Drawing.Point(0, 110);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.Size = new System.Drawing.Size(611, 460);
            this.grdLister.TabIndex = 110;
            // 
            // frmAttributesFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 570);
            this.ControlBox = true;
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Attributes Filter";
            this.KeyPreview = true;
            this.Name = "frmAttributesFilter";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Attributes Filter";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optAvailableDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optLoginDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.optAllDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlAttributes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadGridView grdLister;
        private Telerik.WinControls.UI.RadLabel radLabel7;
        private Telerik.WinControls.UI.RadDropDownList ddlAttributes;
        private Telerik.WinControls.UI.RadRadioButton optAllDrivers;
        private Telerik.WinControls.UI.RadRadioButton optAvailableDrivers;
        private Telerik.WinControls.UI.RadRadioButton optLoginDrivers;
    }
}