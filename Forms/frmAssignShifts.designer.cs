namespace Taxi_AppMain
{
    partial class frmAssignShifts
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
            this.radLabel24 = new Telerik.WinControls.UI.RadLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.grdDriver = new UI.MyGridView();
            this.grdShift = new UI.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel24)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriver.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShift.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdShift);
            this.panel1.Controls.Add(this.radLabel24);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.grdDriver);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 574);
            this.panel1.TabIndex = 106;
            // 
            // radLabel24
            // 
            this.radLabel24.AutoSize = false;
            this.radLabel24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.radLabel24.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel24.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel24.ForeColor = System.Drawing.Color.Black;
            this.radLabel24.Location = new System.Drawing.Point(270, 0);
            this.radLabel24.Name = "radLabel24";
            // 
            // 
            // 
            this.radLabel24.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel24.Size = new System.Drawing.Size(591, 23);
            this.radLabel24.TabIndex = 121;
            this.radLabel24.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExit1);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(270, 518);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(591, 56);
            this.panel2.TabIndex = 0;
            // 
            // btnExit1
            // 
            this.btnExit1.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit1.Location = new System.Drawing.Point(487, 6);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(87, 44);
            this.btnExit1.TabIndex = 265;
            this.btnExit1.Text = "Exit";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSave.Location = new System.Drawing.Point(369, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 44);
            this.btnSave.TabIndex = 264;
            this.btnSave.Text = "Save";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdDriver
            // 
            this.grdDriver.AutoCellFormatting = false;
            this.grdDriver.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdDriver.EnableCheckInCheckOut = false;
            this.grdDriver.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDriver.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdDriver.Location = new System.Drawing.Point(0, 0);
            this.grdDriver.Name = "grdDriver";
            this.grdDriver.PKFieldColumnName = "";
            this.grdDriver.ShowImageOnActionButton = true;
            this.grdDriver.Size = new System.Drawing.Size(270, 574);
            this.grdDriver.TabIndex = 117;
            this.grdDriver.Text = "myGridView1";
            // 
            // grdShift
            // 
            this.grdShift.AutoCellFormatting = false;
            this.grdShift.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShift.EnableCheckInCheckOut = false;
            this.grdShift.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdShift.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdShift.Location = new System.Drawing.Point(270, 23);
            this.grdShift.Name = "grdShift";
            this.grdShift.PKFieldColumnName = "";
            this.grdShift.ShowImageOnActionButton = true;
            this.grdShift.Size = new System.Drawing.Size(591, 495);
            this.grdShift.TabIndex = 120;
            this.grdShift.Text = "myGridView1";
            // 
            // frmAssignShifts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 612);
            this.ControlBox = true;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Assign Shifts";
            this.KeyPreview = true;
            this.Name = "frmAssignShifts";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Assign Shifts";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel24)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriver.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShift.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UI.MyGridView grdDriver;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private UI.MyGridView grdShift;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadLabel radLabel24;
    }
}