namespace Taxi_AppMain
{
    partial class frmPeakOffPeakTimeSettings
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
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn2 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn3 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn2 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
            Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.grdPeak = new Telerik.WinControls.UI.RadGridView();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel17 = new Telerik.WinControls.UI.RadLabel();
            this.numIncrementRate = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPeak)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPeak.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrementRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.grdPeak);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Controls.Add(this.radLabel17);
            this.radPanel1.Controls.Add(this.numIncrementRate);
            this.radPanel1.Controls.Add(this.btnExitForm);
            this.radPanel1.Controls.Add(this.btnSave);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 75);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(967, 497);
            this.radPanel1.TabIndex = 104;
            // 
            // grdPeak
            // 
            this.grdPeak.Location = new System.Drawing.Point(19, 46);
            // 
            // grdPeak
            // 
            this.grdPeak.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
            gridViewComboBoxColumn1.DisplayMember = null;
            gridViewComboBoxColumn1.FormatString = "";
            gridViewComboBoxColumn1.HeaderText = "Plot";
            gridViewComboBoxColumn1.Name = "Plot";
            gridViewComboBoxColumn1.ValueMember = null;
            gridViewComboBoxColumn1.Width = 150;
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Id";
            gridViewTextBoxColumn1.IsVisible = false;
            gridViewTextBoxColumn1.Name = "Id";
            gridViewComboBoxColumn2.DisplayMember = null;
            gridViewComboBoxColumn2.FormatString = "";
            gridViewComboBoxColumn2.HeaderText = "From Day";
            gridViewComboBoxColumn2.Name = "FromDay";
            gridViewComboBoxColumn2.ValueMember = null;
            gridViewComboBoxColumn2.Width = 120;
            gridViewComboBoxColumn3.DisplayMember = null;
            gridViewComboBoxColumn3.FormatString = "";
            gridViewComboBoxColumn3.HeaderText = "Till Day";
            gridViewComboBoxColumn3.Name = "TillDay";
            gridViewComboBoxColumn3.ValueMember = null;
            gridViewComboBoxColumn3.Width = 120;
            gridViewDateTimeColumn1.FormatString = "";
            gridViewDateTimeColumn1.HeaderText = "From Time";
            gridViewDateTimeColumn1.Name = "FromTime";
            gridViewDateTimeColumn1.Width = 100;
            gridViewDateTimeColumn2.FormatString = "";
            gridViewDateTimeColumn2.HeaderText = "Till TIme";
            gridViewDateTimeColumn2.Name = "TillTime";
            gridViewDateTimeColumn2.Width = 100;
            gridViewDecimalColumn1.HeaderText = "Increment Percentage";
            gridViewDecimalColumn1.Name = "IncrementPercentage";
            gridViewDecimalColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            gridViewDecimalColumn1.Width = 160;
            gridViewCheckBoxColumn1.HeaderText = "Amount Wise";
            gridViewCheckBoxColumn1.Name = "AmountWise";
            gridViewCheckBoxColumn1.Width = 80;
            gridViewDecimalColumn2.HeaderText = "Amount";
            gridViewDecimalColumn2.Name = "Amount";
            gridViewDecimalColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight;
            gridViewDecimalColumn2.Width = 70;
            this.grdPeak.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewComboBoxColumn1,
            gridViewTextBoxColumn1,
            gridViewComboBoxColumn2,
            gridViewComboBoxColumn3,
            gridViewDateTimeColumn1,
            gridViewDateTimeColumn2,
            gridViewDecimalColumn1,
            gridViewCheckBoxColumn1,
            gridViewDecimalColumn2});
            this.grdPeak.Name = "grdPeak";
            this.grdPeak.ShowGroupPanel = false;
            this.grdPeak.Size = new System.Drawing.Size(935, 300);
            this.grdPeak.TabIndex = 207;
            this.grdPeak.Text = "radGridView1";
            this.grdPeak.Click += new System.EventHandler(this.grdPeak_Click);
            // 
            // radLabel2
            // 
            this.radLabel2.BackColor = System.Drawing.Color.Transparent;
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.ForeColor = System.Drawing.Color.Black;
            this.radLabel2.Location = new System.Drawing.Point(310, 9);
            this.radLabel2.Name = "radLabel2";
            // 
            // 
            // 
            this.radLabel2.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel2.Size = new System.Drawing.Size(59, 22);
            this.radLabel2.TabIndex = 206;
            this.radLabel2.Text = "Percent";
            this.radLabel2.Visible = false;
            // 
            // radLabel17
            // 
            this.radLabel17.BackColor = System.Drawing.Color.Transparent;
            this.radLabel17.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel17.ForeColor = System.Drawing.Color.Black;
            this.radLabel17.Location = new System.Drawing.Point(19, 9);
            this.radLabel17.Name = "radLabel17";
            // 
            // 
            // 
            this.radLabel17.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel17.Size = new System.Drawing.Size(189, 22);
            this.radLabel17.TabIndex = 205;
            this.radLabel17.Text = "Peak Time Increment Rate";
            this.radLabel17.Visible = false;
            // 
            // numIncrementRate
            // 
            this.numIncrementRate.EnableKeyMap = true;
            this.numIncrementRate.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.numIncrementRate.ForeColor = System.Drawing.Color.Red;
            this.numIncrementRate.InterceptArrowKeys = false;
            this.numIncrementRate.Location = new System.Drawing.Point(224, 8);
            this.numIncrementRate.Name = "numIncrementRate";
            // 
            // 
            // 
            this.numIncrementRate.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numIncrementRate.RootElement.ForeColor = System.Drawing.Color.Red;
            this.numIncrementRate.ShowBorder = true;
            this.numIncrementRate.ShowUpDownButtons = false;
            this.numIncrementRate.Size = new System.Drawing.Size(72, 24);
            this.numIncrementRate.TabIndex = 204;
            this.numIncrementRate.TabStop = false;
            this.numIncrementRate.Visible = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numIncrementRate.GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadSpinElement)(this.numIncrementRate.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numIncrementRate.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numIncrementRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numIncrementRate.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExitForm
            // 
            this.btnExitForm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitForm.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitForm.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExitForm.Location = new System.Drawing.Point(486, 420);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(112, 56);
            this.btnExitForm.TabIndex = 202;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExitForm.Click += new System.EventHandler(this.btnExitForm_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(338, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 56);
            this.btnSave.TabIndex = 201;
            this.btnSave.Text = "Save Settings";
            this.btnSave.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save Settings";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel6
            // 
            this.radLabel6.AutoSize = false;
            this.radLabel6.BackColor = System.Drawing.Color.SteelBlue;
            this.radLabel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel6.ForeColor = System.Drawing.Color.White;
            this.radLabel6.Location = new System.Drawing.Point(0, 38);
            this.radLabel6.Name = "radLabel6";
            // 
            // 
            // 
            this.radLabel6.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel6.Size = new System.Drawing.Size(967, 37);
            this.radLabel6.TabIndex = 105;
            this.radLabel6.Text = "Peak Time Settings";
            this.radLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmPeakOffPeakTimeSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 572);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radLabel6);
            this.KeyPreview = true;
            this.Name = "frmPeakOffPeakTimeSettings";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Fare Increment";
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.radLabel6, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPeak.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPeak)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrementRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel6;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadGridView grdPeak;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel17;
        private Telerik.WinControls.UI.RadSpinEditor numIncrementRate;
    }
}