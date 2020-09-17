namespace Taxi_AppMain
{
    partial class frmCheckDriverDistance
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
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.radLabel27 = new Telerik.WinControls.UI.RadLabel();
            this.ddlDriver = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtDistance = new Telerik.WinControls.UI.RadLabel();
            this.btnCheckDistance = new Telerik.WinControls.UI.RadButton();
            this.txtError = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExitForm
            // 
            this.btnExitForm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitForm.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExitForm.Location = new System.Drawing.Point(268, 290);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(159, 64);
            this.btnExitForm.TabIndex = 280;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.Click += new System.EventHandler(this.btnExitForm_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel27
            // 
            this.radLabel27.AutoSize = false;
            this.radLabel27.BackColor = System.Drawing.Color.Navy;
            this.radLabel27.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel27.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.radLabel27.ForeColor = System.Drawing.Color.White;
            this.radLabel27.Location = new System.Drawing.Point(0, 0);
            this.radLabel27.Name = "radLabel27";
            // 
            // 
            // 
            this.radLabel27.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel27.Size = new System.Drawing.Size(487, 28);
            this.radLabel27.TabIndex = 276;
            this.radLabel27.Text = "Driver Distance View";
            this.radLabel27.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ddlDriver
            // 
            this.ddlDriver.BackColor = System.Drawing.Color.White;
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(135, 78);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Size = new System.Drawing.Size(286, 24);
            this.ddlDriver.TabIndex = 282;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 18);
            this.label1.TabIndex = 281;
            this.label1.Text = "Select Driver";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.radLabel1.ForeColor = System.Drawing.Color.Black;
            this.radLabel1.Location = new System.Drawing.Point(52, 169);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel1.Size = new System.Drawing.Size(105, 27);
            this.radLabel1.TabIndex = 283;
            this.radLabel1.Text = "Distance :";
            // 
            // txtDistance
            // 
            this.txtDistance.AutoSize = false;
            this.txtDistance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.txtDistance.ForeColor = System.Drawing.Color.Blue;
            this.txtDistance.Location = new System.Drawing.Point(170, 169);
            this.txtDistance.Name = "txtDistance";
            // 
            // 
            // 
            this.txtDistance.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.txtDistance.Size = new System.Drawing.Size(292, 67);
            this.txtDistance.TabIndex = 284;
            // 
            // btnCheckDistance
            // 
            this.btnCheckDistance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckDistance.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnCheckDistance.Location = new System.Drawing.Point(49, 290);
            this.btnCheckDistance.Name = "btnCheckDistance";
            this.btnCheckDistance.Size = new System.Drawing.Size(160, 64);
            this.btnCheckDistance.TabIndex = 285;
            this.btnCheckDistance.Text = "Calculate Distance";
            this.btnCheckDistance.Click += new System.EventHandler(this.btnCheckDistance_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCheckDistance.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCheckDistance.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCheckDistance.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCheckDistance.GetChildAt(0))).Text = "Calculate Distance";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCheckDistance.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCheckDistance.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtError
            // 
            this.txtError.AutoSize = false;
            this.txtError.BackColor = System.Drawing.Color.GhostWhite;
            this.txtError.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtError.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.txtError.ForeColor = System.Drawing.Color.Red;
            this.txtError.Location = new System.Drawing.Point(0, 382);
            this.txtError.Name = "txtError";
            // 
            // 
            // 
            this.txtError.RootElement.ForeColor = System.Drawing.Color.Red;
            this.txtError.Size = new System.Drawing.Size(487, 18);
            this.txtError.TabIndex = 286;
            // 
            // frmCheckDriverDistance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 400);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.btnCheckDistance);
            this.Controls.Add(this.txtDistance);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.ddlDriver);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnExitForm);
            this.Controls.Add(this.radLabel27);
            this.Name = "frmCheckDriverDistance";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.ShowIcon = false;
            this.Text = "Driver Distance View";
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCheckDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnExitForm;
        private Telerik.WinControls.UI.RadLabel radLabel27;
        private System.Windows.Forms.ComboBox ddlDriver;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel txtDistance;
        private Telerik.WinControls.UI.RadButton btnCheckDistance;
        private Telerik.WinControls.UI.RadLabel txtError;
    }
}