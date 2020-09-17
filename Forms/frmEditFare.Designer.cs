namespace Taxi_AppMain
{
    partial class frmEditFare
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.txtCompFare = new System.Windows.Forms.NumericUpDown();
            this.txtCustFare = new System.Windows.Forms.NumericUpDown();
            this.txtFare = new System.Windows.Forms.NumericUpDown();
            this.lblCompFare = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.lblFare = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompFare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFare)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.SteelBlue;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(0, 0);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(388, 31);
            this.radLabel1.TabIndex = 231;
            this.radLabel1.Text = "Edit Fares";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.txtCompFare);
            this.radPanel1.Controls.Add(this.txtCustFare);
            this.radPanel1.Controls.Add(this.txtFare);
            this.radPanel1.Controls.Add(this.lblCompFare);
            this.radPanel1.Controls.Add(this.radLabel2);
            this.radPanel1.Controls.Add(this.btnSave);
            this.radPanel1.Controls.Add(this.btnExitForm);
            this.radPanel1.Controls.Add(this.lblFare);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 31);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(388, 262);
            this.radPanel1.TabIndex = 232;
            // 
            // txtCompFare
            // 
            this.txtCompFare.DecimalPlaces = 2;
            this.txtCompFare.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompFare.Location = new System.Drawing.Point(160, 133);
            this.txtCompFare.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCompFare.Name = "txtCompFare";
            this.txtCompFare.Size = new System.Drawing.Size(93, 27);
            this.txtCompFare.TabIndex = 233;
            this.txtCompFare.Visible = false;
            // 
            // txtCustFare
            // 
            this.txtCustFare.DecimalPlaces = 2;
            this.txtCustFare.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCustFare.Location = new System.Drawing.Point(160, 78);
            this.txtCustFare.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtCustFare.Name = "txtCustFare";
            this.txtCustFare.Size = new System.Drawing.Size(93, 27);
            this.txtCustFare.TabIndex = 232;
            // 
            // txtFare
            // 
            this.txtFare.DecimalPlaces = 2;
            this.txtFare.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFare.ForeColor = System.Drawing.Color.Red;
            this.txtFare.Location = new System.Drawing.Point(160, 20);
            this.txtFare.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtFare.Name = "txtFare";
            this.txtFare.Size = new System.Drawing.Size(93, 27);
            this.txtFare.TabIndex = 231;
            // 
            // lblCompFare
            // 
            this.lblCompFare.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompFare.Location = new System.Drawing.Point(18, 135);
            this.lblCompFare.Name = "lblCompFare";
            this.lblCompFare.Size = new System.Drawing.Size(130, 23);
            this.lblCompFare.TabIndex = 229;
            this.lblCompFare.Text = "Company Price £";
            this.lblCompFare.Visible = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(18, 80);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(136, 23);
            this.radLabel2.TabIndex = 227;
            this.radLabel2.Text = "Customer Fares £";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.Tick3;
            this.btnSave.Location = new System.Drawing.Point(76, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 39);
            this.btnSave.TabIndex = 220;
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
            this.btnExitForm.Location = new System.Drawing.Point(216, 210);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(104, 39);
            this.btnExitForm.TabIndex = 219;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.Click += new System.EventHandler(this.btnExitForm_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblFare
            // 
            this.lblFare.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFare.Location = new System.Drawing.Point(92, 22);
            this.lblFare.Name = "lblFare";
            this.lblFare.Size = new System.Drawing.Size(61, 23);
            this.lblFare.TabIndex = 218;
            this.lblFare.Text = "Fares £";
            // 
            // frmEditFare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(388, 293);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmEditFare";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Fare";
            this.Shown += new System.EventHandler(this.frmEditFare_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEditFare_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCompFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCustFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCompFare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFare)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private Telerik.WinControls.UI.RadLabel lblFare;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel lblCompFare;
        private System.Windows.Forms.NumericUpDown txtCompFare;
        private System.Windows.Forms.NumericUpDown txtCustFare;
        private System.Windows.Forms.NumericUpDown txtFare;

    }
}
