namespace Taxi_AppMain
{
    partial class frmSinBin
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
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddl_Driver = new Telerik.WinControls.UI.RadDropDownList();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.optBlocked = new System.Windows.Forms.RadioButton();
            this.OptUnBlocked = new System.Windows.Forms.RadioButton();
            this.numBlocked = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblMin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlocked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.Black;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(0, 0);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(380, 31);
            this.radLabel1.TabIndex = 11;
            this.radLabel1.Text = "SIN BIN";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ddl_Driver
            // 
            this.ddl_Driver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddl_Driver.Location = new System.Drawing.Point(110, 56);
            this.ddl_Driver.Name = "ddl_Driver";
            this.ddl_Driver.Size = new System.Drawing.Size(259, 23);
            this.ddl_Driver.TabIndex = 1;
            this.ddl_Driver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddl_Driver_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Select Driver";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            this.btnSave.Location = new System.Drawing.Point(81, 185);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 39);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExitForm
            // 
            this.btnExitForm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitForm.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitForm.Location = new System.Drawing.Point(221, 185);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(104, 39);
            this.btnExitForm.TabIndex = 3;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.Click += new System.EventHandler(this.btnExitForm_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // optBlocked
            // 
            this.optBlocked.AutoSize = true;
            this.optBlocked.Checked = true;
            this.optBlocked.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optBlocked.Location = new System.Drawing.Point(23, 100);
            this.optBlocked.Name = "optBlocked";
            this.optBlocked.Size = new System.Drawing.Size(72, 18);
            this.optBlocked.TabIndex = 31;
            this.optBlocked.TabStop = true;
            this.optBlocked.Tag = "0";
            this.optBlocked.Text = "Blocked";
            this.optBlocked.UseVisualStyleBackColor = true;
            this.optBlocked.CheckedChanged += new System.EventHandler(this.optBlocked_CheckedChanged);
            // 
            // OptUnBlocked
            // 
            this.OptUnBlocked.AutoSize = true;
            this.OptUnBlocked.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OptUnBlocked.Location = new System.Drawing.Point(23, 135);
            this.OptUnBlocked.Name = "OptUnBlocked";
            this.OptUnBlocked.Size = new System.Drawing.Size(88, 18);
            this.OptUnBlocked.TabIndex = 30;
            this.OptUnBlocked.TabStop = true;
            this.OptUnBlocked.Tag = "1";
            this.OptUnBlocked.Text = "UnBlocked";
            this.OptUnBlocked.UseVisualStyleBackColor = true;
            // 
            // numBlocked
            // 
            this.numBlocked.EnableKeyMap = true;
            this.numBlocked.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numBlocked.InterceptArrowKeys = false;
            this.numBlocked.Location = new System.Drawing.Point(110, 98);
            this.numBlocked.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numBlocked.Name = "numBlocked";
            // 
            // 
            // 
            this.numBlocked.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numBlocked.ShowBorder = true;
            this.numBlocked.ShowUpDownButtons = false;
            this.numBlocked.Size = new System.Drawing.Size(112, 24);
            this.numBlocked.TabIndex = 252;
            this.numBlocked.TabStop = false;
            this.numBlocked.Visible = false;
         
            ((Telerik.WinControls.UI.RadSpinElement)(this.numBlocked.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numBlocked.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numBlocked.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numBlocked.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.Location = new System.Drawing.Point(225, 101);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(50, 18);
            this.lblMin.TabIndex = 253;
            this.lblMin.Text = "(mins)";
            this.lblMin.Visible = false;
            // 
            // frmSinBin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(380, 228);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.numBlocked);
            this.Controls.Add(this.optBlocked);
            this.Controls.Add(this.OptUnBlocked);
            this.Controls.Add(this.ddl_Driver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExitForm);
            this.Controls.Add(this.radLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSinBin";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "SIN BIN";
            this.Controls.SetChildIndex(this.radLabel1, 0);
            this.Controls.SetChildIndex(this.btnExitForm, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ddl_Driver, 0);
            this.Controls.SetChildIndex(this.OptUnBlocked, 0);
            this.Controls.SetChildIndex(this.optBlocked, 0);
            this.Controls.SetChildIndex(this.numBlocked, 0);
            this.Controls.SetChildIndex(this.lblMin, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddl_Driver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlocked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList ddl_Driver;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private System.Windows.Forms.RadioButton optBlocked;
        private System.Windows.Forms.RadioButton OptUnBlocked;
        private Telerik.WinControls.UI.RadSpinEditor numBlocked;
        private System.Windows.Forms.Label lblMin;

    }
}
