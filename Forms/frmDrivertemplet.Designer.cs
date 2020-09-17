namespace Taxi_AppMain
{
    partial class frmDrivertemplet
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel21 = new Telerik.WinControls.UI.RadLabel();
            this.grdSMSTemplets = new UI.MyGridView();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSMSTemplets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSMSTemplets.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(874, 223);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(874, 152);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(874, 302);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(874, 376);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(598, 615);
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.radPanel2);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(726, 571);
            this.radPanel1.TabIndex = 106;
            // 
            // radLabel21
            // 
            this.radLabel21.AutoSize = false;
            this.radLabel21.BackColor = System.Drawing.Color.DarkSlateGray;
            this.radLabel21.BorderVisible = true;
            this.radLabel21.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel21.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel21.ForeColor = System.Drawing.Color.White;
            this.radLabel21.Location = new System.Drawing.Point(0, 0);
            this.radLabel21.Name = "radLabel21";
            // 
            // 
            // 
            this.radLabel21.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel21.Size = new System.Drawing.Size(726, 18);
            this.radLabel21.TabIndex = 119;
            this.radLabel21.Text = "Driver Templates";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel21.GetChildAt(0))).BorderVisible = true;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel21.GetChildAt(0))).Text = "Driver Templates";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel21.GetChildAt(0).GetChildAt(1))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel21.GetChildAt(0).GetChildAt(1))).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel21.GetChildAt(0).GetChildAt(1))).BottomWidth = 0F;
            // 
            // grdSMSTemplets
            // 
            this.grdSMSTemplets.AutoCellFormatting = false;
            this.grdSMSTemplets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSMSTemplets.EnableCheckInCheckOut = false;
            this.grdSMSTemplets.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grdSMSTemplets.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdSMSTemplets.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdSMSTemplets.Location = new System.Drawing.Point(0, 18);
            this.grdSMSTemplets.Name = "grdSMSTemplets";
            this.grdSMSTemplets.PKFieldColumnName = "";
            this.grdSMSTemplets.ShowImageOnActionButton = true;
            this.grdSMSTemplets.Size = new System.Drawing.Size(726, 553);
            this.grdSMSTemplets.TabIndex = 116;
            this.grdSMSTemplets.Text = "myGridView1";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.grdSMSTemplets);
            this.radPanel2.Controls.Add(this.radLabel21);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel2.Location = new System.Drawing.Point(0, 0);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(726, 571);
            this.radPanel2.TabIndex = 120;
            // 
            // frmDrivertemplet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 609);
            this.ControlBox = true;
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.FormTitle = "Driver Templates";
            this.KeyPreview = true;
            this.Name = "frmDrivertemplet";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Driver Templates";
            this.Load += new System.EventHandler(this.frmDrivertemplet_Load);
            this.Shown += new System.EventHandler(this.frmDrivertemplet_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDrivertemplet_KeyDown);
            this.Controls.SetChildIndex(this.radPanel1, 0);
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
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSMSTemplets.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSMSTemplets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private UI.MyGridView grdSMSTemplets;
        private Telerik.WinControls.UI.RadLabel radLabel21;
        private Telerik.WinControls.UI.RadPanel radPanel2;
    }
}