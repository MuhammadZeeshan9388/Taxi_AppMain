namespace Taxi_AppMain
{
    partial class frmFareMeterSetting
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
            this.ddlFareMeterType = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel113 = new Telerik.WinControls.UI.RadLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkEnablePeakOffPeakWiseFares = new Telerik.WinControls.UI.RadCheckBox();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.grdFareMeterSetting = new Telerik.WinControls.UI.RadGridView();
            this.spnMeterRoundedValue = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblFareValue = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFareMeterType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel113)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnablePeakOffPeakWiseFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFareMeterSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFareMeterSetting.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMeterRoundedValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFareValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlFareMeterType
            // 
            this.ddlFareMeterType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.ddlFareMeterType.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlFareMeterType.Location = new System.Drawing.Point(168, 16);
            this.ddlFareMeterType.Name = "ddlFareMeterType";
            this.ddlFareMeterType.Size = new System.Drawing.Size(221, 26);
            this.ddlFareMeterType.TabIndex = 137;
            // 
            // radLabel113
            // 
            this.radLabel113.BackColor = System.Drawing.Color.Transparent;
            this.radLabel113.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel113.ForeColor = System.Drawing.Color.Black;
            this.radLabel113.Location = new System.Drawing.Point(14, 18);
            this.radLabel113.Name = "radLabel113";
            // 
            // 
            // 
            this.radLabel113.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel113.Size = new System.Drawing.Size(133, 22);
            this.radLabel113.TabIndex = 136;
            this.radLabel113.Text = "Fare Meter Type";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkEnablePeakOffPeakWiseFares);
            this.panel1.Controls.Add(this.btnExit1);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.grdFareMeterSetting);
            this.panel1.Controls.Add(this.spnMeterRoundedValue);
            this.panel1.Controls.Add(this.lblFareValue);
            this.panel1.Controls.Add(this.radLabel113);
            this.panel1.Controls.Add(this.ddlFareMeterType);
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1197, 596);
            this.panel1.TabIndex = 138;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // chkEnablePeakOffPeakWiseFares
            // 
            this.chkEnablePeakOffPeakWiseFares.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnablePeakOffPeakWiseFares.Location = new System.Drawing.Point(415, 18);
            this.chkEnablePeakOffPeakWiseFares.Name = "chkEnablePeakOffPeakWiseFares";
            this.chkEnablePeakOffPeakWiseFares.Size = new System.Drawing.Size(213, 21);
            this.chkEnablePeakOffPeakWiseFares.TabIndex = 147;
            this.chkEnablePeakOffPeakWiseFares.Text = "Enable Peak OffPeak Fares";
            // 
            // btnExit1
            // 
            this.btnExit1.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit1.Location = new System.Drawing.Point(599, 527);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(93, 44);
            this.btnExit1.TabIndex = 146;
            this.btnExit1.Text = "Exit";
            this.btnExit1.Click += new System.EventHandler(this.btnExit1_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSave.Location = new System.Drawing.Point(435, 527);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 44);
            this.btnSave.TabIndex = 145;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdFareMeterSetting
            // 
            this.grdFareMeterSetting.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdFareMeterSetting.Location = new System.Drawing.Point(5, 112);
            // 
            // grdFareMeterSetting
            // 
            this.grdFareMeterSetting.MasterTemplate.AllowAddNewRow = false;
            this.grdFareMeterSetting.Name = "grdFareMeterSetting";
            this.grdFareMeterSetting.ShowGroupPanel = false;
            this.grdFareMeterSetting.Size = new System.Drawing.Size(1189, 382);
            this.grdFareMeterSetting.TabIndex = 144;
            this.grdFareMeterSetting.Text = "radGridView2";
            // 
            // spnMeterRoundedValue
            // 
            this.spnMeterRoundedValue.DecimalPlaces = 1;
            this.spnMeterRoundedValue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spnMeterRoundedValue.Location = new System.Drawing.Point(220, 57);
            this.spnMeterRoundedValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spnMeterRoundedValue.Name = "spnMeterRoundedValue";
            // 
            // 
            // 
            this.spnMeterRoundedValue.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.spnMeterRoundedValue.RootElement.StretchVertically = true;
            this.spnMeterRoundedValue.ShowBorder = true;
            this.spnMeterRoundedValue.Size = new System.Drawing.Size(63, 21);
            this.spnMeterRoundedValue.TabIndex = 139;
            this.spnMeterRoundedValue.TabStop = false;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.spnMeterRoundedValue.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0.0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.spnMeterRoundedValue.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblFareValue
            // 
            this.lblFareValue.BackColor = System.Drawing.Color.Transparent;
            this.lblFareValue.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFareValue.ForeColor = System.Drawing.Color.Black;
            this.lblFareValue.Location = new System.Drawing.Point(13, 55);
            this.lblFareValue.Name = "lblFareValue";
            // 
            // 
            // 
            this.lblFareValue.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblFareValue.Size = new System.Drawing.Size(163, 22);
            this.lblFareValue.TabIndex = 138;
            this.lblFareValue.Text = "Fare Rounded Value";
            this.lblFareValue.Visible = false;
            // 
            // frmFareMeterSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 646);
            this.Controls.Add(this.panel1);
            this.FormTitle = "Fare Meter Settings";
            this.Name = "frmFareMeterSetting";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Fare Meter Settings";
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
            ((System.ComponentModel.ISupportInitialize)(this.ddlFareMeterType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel113)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnablePeakOffPeakWiseFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFareMeterSetting.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFareMeterSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnMeterRoundedValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFareValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList ddlFareMeterType;
        private Telerik.WinControls.UI.RadLabel radLabel113;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadSpinEditor spnMeterRoundedValue;
        private Telerik.WinControls.UI.RadLabel lblFareValue;
        private Telerik.WinControls.UI.RadGridView grdFareMeterSetting;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadCheckBox chkEnablePeakOffPeakWiseFares;
    }
}