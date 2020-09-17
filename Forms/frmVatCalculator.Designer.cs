namespace Taxi_AppMain
{
    partial class frmVatCalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVatCalculator));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radFourth = new Telerik.WinControls.UI.RadRadioButton();
            this.radThird = new Telerik.WinControls.UI.RadRadioButton();
            this.radSecond = new Telerik.WinControls.UI.RadRadioButton();
            this.radFirst = new Telerik.WinControls.UI.RadRadioButton();
            this.btnApply = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radFourth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radThird)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radFirst)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnApply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbYear);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 86);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Year";
            // 
            // cmbYear
            // 
            this.cmbYear.BackColor = System.Drawing.SystemColors.Control;
            this.cmbYear.FormattingEnabled = true;
            this.cmbYear.Location = new System.Drawing.Point(94, 30);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(121, 22);
            this.cmbYear.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Year :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radFourth);
            this.groupBox2.Controls.Add(this.radThird);
            this.groupBox2.Controls.Add(this.radSecond);
            this.groupBox2.Controls.Add(this.radFirst);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 130);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Calculate Vat";
            // 
            // radFourth
            // 
            this.radFourth.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radFourth.Location = new System.Drawing.Point(7, 95);
            this.radFourth.Name = "radFourth";
            this.radFourth.Size = new System.Drawing.Size(135, 18);
            this.radFourth.TabIndex = 3;
            this.radFourth.Text = "Fourth Quarter Report";
            // 
            // radThird
            // 
            this.radThird.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radThird.Location = new System.Drawing.Point(7, 71);
            this.radThird.Name = "radThird";
            this.radThird.Size = new System.Drawing.Size(135, 18);
            this.radThird.TabIndex = 2;
            this.radThird.Text = "Third Quarter Report";
            // 
            // radSecond
            // 
            this.radSecond.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radSecond.Location = new System.Drawing.Point(6, 47);
            this.radSecond.Name = "radSecond";
            this.radSecond.Size = new System.Drawing.Size(135, 18);
            this.radSecond.TabIndex = 1;
            this.radSecond.Text = "Second Quarter Report";
            // 
            // radFirst
            // 
            this.radFirst.Location = new System.Drawing.Point(7, 23);
            this.radFirst.Name = "radFirst";
            this.radFirst.Size = new System.Drawing.Size(135, 18);
            this.radFirst.TabIndex = 0;
            this.radFirst.Text = "First Quarter Report";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radFirst.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.radFirst.GetChildAt(0).GetChildAt(1).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.SystemColors.Control;
            this.btnApply.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(12, 270);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(92, 31);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnApply.GetChildAt(0))).Text = "Apply";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApply.GetChildAt(0).GetChildAt(1).GetChildAt(1))).MeasureTrailingSpaces = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApply.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnApply.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(183, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 31);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).Text = "Cancel";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmVatCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(288, 316);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVatCalculator";
            this.Text = "Vat Calculator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radFourth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radThird)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radFirst)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnApply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Telerik.WinControls.UI.RadRadioButton radFourth;
        private Telerik.WinControls.UI.RadRadioButton radThird;
        private Telerik.WinControls.UI.RadRadioButton radSecond;
        private Telerik.WinControls.UI.RadRadioButton radFirst;
        private Telerik.WinControls.UI.RadButton btnApply;
        private Telerik.WinControls.UI.RadButton btnCancel;
    }
}