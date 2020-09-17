namespace Taxi_AppMain
{
    partial class frmDriverShiftEarning
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
            this.grdDriverShiftEarning = new Telerik.WinControls.UI.RadGridView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.rdoToday = new Telerik.WinControls.UI.RadRadioButton();
            this.rdoCurrent = new Telerik.WinControls.UI.RadRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShiftEarning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShiftEarning.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rdoToday)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdDriverShiftEarning
            // 
            this.grdDriverShiftEarning.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDriverShiftEarning.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDriverShiftEarning.Location = new System.Drawing.Point(0, 86);
            // 
            // grdDriverShiftEarning
            // 
            this.grdDriverShiftEarning.MasterTemplate.AllowAddNewRow = false;
            this.grdDriverShiftEarning.MasterTemplate.AllowDeleteRow = false;
            this.grdDriverShiftEarning.MasterTemplate.AllowEditRow = false;
            this.grdDriverShiftEarning.Name = "grdDriverShiftEarning";
            this.grdDriverShiftEarning.ShowGroupPanel = false;
            this.grdDriverShiftEarning.Size = new System.Drawing.Size(948, 500);
            this.grdDriverShiftEarning.TabIndex = 106;
            this.grdDriverShiftEarning.Text = "radGridView1";
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.rdoToday);
            this.radPanel1.Controls.Add(this.rdoCurrent);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(948, 48);
            this.radPanel1.TabIndex = 107;
            // 
            // rdoToday
            // 
            this.rdoToday.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoToday.Location = new System.Drawing.Point(184, 15);
            this.rdoToday.Name = "rdoToday";
            this.rdoToday.Size = new System.Drawing.Size(110, 18);
            this.rdoToday.TabIndex = 1;
            this.rdoToday.Text = "Today";
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoToday.GetChildAt(0))).Text = "Today";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoToday.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoToday.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoCurrent
            // 
            this.rdoCurrent.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoCurrent.Location = new System.Drawing.Point(12, 15);
            this.rdoCurrent.Name = "rdoCurrent";
            this.rdoCurrent.Size = new System.Drawing.Size(134, 18);
            this.rdoCurrent.TabIndex = 0;
            this.rdoCurrent.Text = "Current Shift";
            this.rdoCurrent.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdoCurrent_ToggleStateChanged);
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoCurrent.GetChildAt(0))).ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
            ((Telerik.WinControls.UI.RadRadioButtonElement)(this.rdoCurrent.GetChildAt(0))).Text = "Current Shift";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.rdoCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmDriverShiftEarning
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 586);
            this.ControlBox = true;
            this.Controls.Add(this.grdDriverShiftEarning);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Driver Shift Earning";
            this.Name = "frmDriverShiftEarning";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Driver Shift Earning";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.grdDriverShiftEarning, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShiftEarning.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverShiftEarning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rdoToday)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdoCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdDriverShiftEarning;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadRadioButton rdoToday;
        private Telerik.WinControls.UI.RadRadioButton rdoCurrent;
    }
}