namespace Taxi_AppMain
{
    partial class frmInActiveDriversList
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
            this.chkAllDrivers = new Telerik.WinControls.UI.RadRadioButton();
            this.chkLeftDrivers = new Telerik.WinControls.UI.RadRadioButton();
            this.chkHolidayDrivers = new Telerik.WinControls.UI.RadRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLeftDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHolidayDrivers)).BeginInit();
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
            this.grdLister.Location = new System.Drawing.Point(0, 80);
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(1026, 736);
            this.grdLister.TabIndex = 109;
            this.grdLister.Text = "myGridView1";
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.chkHolidayDrivers);
            this.radPanel1.Controls.Add(this.chkLeftDrivers);
            this.radPanel1.Controls.Add(this.chkAllDrivers);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1026, 42);
            this.radPanel1.TabIndex = 110;
            // 
            // chkAllDrivers
            // 
            this.chkAllDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllDrivers.Location = new System.Drawing.Point(13, 13);
            this.chkAllDrivers.Name = "chkAllDrivers";
            this.chkAllDrivers.Size = new System.Drawing.Size(110, 18);
            this.chkAllDrivers.TabIndex = 0;
            this.chkAllDrivers.Text = "All Drivers";
            this.chkAllDrivers.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            this.chkAllDrivers.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkAllDrivers_ToggleStateChanged);
            // 
            // chkLeftDrivers
            // 
            this.chkLeftDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLeftDrivers.Location = new System.Drawing.Point(130, 13);
            this.chkLeftDrivers.Name = "chkLeftDrivers";
            this.chkLeftDrivers.Size = new System.Drawing.Size(110, 18);
            this.chkLeftDrivers.TabIndex = 1;
            this.chkLeftDrivers.Text = "Left Drivers";
            this.chkLeftDrivers.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkAllDrivers_ToggleStateChanged);
            // 
            // chkHolidayDrivers
            // 
            this.chkHolidayDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHolidayDrivers.Location = new System.Drawing.Point(261, 13);
            this.chkHolidayDrivers.Name = "chkHolidayDrivers";
            this.chkHolidayDrivers.Size = new System.Drawing.Size(146, 18);
            this.chkHolidayDrivers.TabIndex = 2;
            this.chkHolidayDrivers.Text = "Holiday Drivers";
            this.chkHolidayDrivers.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkAllDrivers_ToggleStateChanged);
            // 
            // frmInActiveDriversList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 816);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.radPanel1);
            this.FormTitle = "InActive Drivers List";
            this.Name = "frmInActiveDriversList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "InActive Drivers List";
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
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
            ((System.ComponentModel.ISupportInitialize)(this.chkAllDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkLeftDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkHolidayDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MyGridView grdLister;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadRadioButton chkHolidayDrivers;
        private Telerik.WinControls.UI.RadRadioButton chkLeftDrivers;
        private Telerik.WinControls.UI.RadRadioButton chkAllDrivers;
    }
}