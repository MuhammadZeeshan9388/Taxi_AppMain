namespace Taxi_AppMain
{
    partial class frmAddDrivertemplet
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
            this.TxtSMSTemplet = new System.Windows.Forms.RichTextBox();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.radLabel21 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.grdSMSTemplets = new UI.MyGridView();
            this.btnClearSMSTemplet = new System.Windows.Forms.Button();
            this.btnAddSMSTemplet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSMSTemplets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSMSTemplets.MasterTemplate)).BeginInit();
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
            this.btnSaveAndNew.Location = new System.Drawing.Point(598, 532);
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.TxtSMSTemplet);
            this.radPanel1.Controls.Add(this.btnSave);
            this.radPanel1.Controls.Add(this.btnExitForm);
            this.radPanel1.Controls.Add(this.radLabel21);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Controls.Add(this.grdSMSTemplets);
            this.radPanel1.Controls.Add(this.btnClearSMSTemplet);
            this.radPanel1.Controls.Add(this.btnAddSMSTemplet);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(722, 437);
            this.radPanel1.TabIndex = 106;
            // 
            // TxtSMSTemplet
            // 
            this.TxtSMSTemplet.Location = new System.Drawing.Point(85, 12);
            this.TxtSMSTemplet.Name = "TxtSMSTemplet";
            this.TxtSMSTemplet.Size = new System.Drawing.Size(542, 105);
            this.TxtSMSTemplet.TabIndex = 216;
            this.TxtSMSTemplet.Text = "";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnSave.Location = new System.Drawing.Point(485, 388);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 39);
            this.btnSave.TabIndex = 215;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick31;
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
            this.btnExitForm.Location = new System.Drawing.Point(605, 388);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(104, 39);
            this.btnExitForm.TabIndex = 214;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.Click += new System.EventHandler(this.btnExitForm_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel21
            // 
            this.radLabel21.AutoSize = false;
            this.radLabel21.BackColor = System.Drawing.Color.DarkSlateGray;
            this.radLabel21.BorderVisible = true;
            this.radLabel21.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel21.ForeColor = System.Drawing.Color.White;
            this.radLabel21.Location = new System.Drawing.Point(12, 139);
            this.radLabel21.Name = "radLabel21";
            // 
            // 
            // 
            this.radLabel21.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel21.Size = new System.Drawing.Size(702, 18);
            this.radLabel21.TabIndex = 119;
            this.radLabel21.Text = "Driver Templates";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel21.GetChildAt(0))).BorderVisible = true;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel21.GetChildAt(0))).Text = "Driver Templates";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel21.GetChildAt(0).GetChildAt(1))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel21.GetChildAt(0).GetChildAt(1))).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel21.GetChildAt(0).GetChildAt(1))).BottomWidth = 0F;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(8, 12);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(75, 19);
            this.radLabel1.TabIndex = 118;
            this.radLabel1.Text = "Template:";
            // 
            // grdSMSTemplets
            // 
            this.grdSMSTemplets.AutoCellFormatting = false;
            this.grdSMSTemplets.EnableCheckInCheckOut = false;
            this.grdSMSTemplets.Font = new System.Drawing.Font("Tahoma", 9F);
            this.grdSMSTemplets.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdSMSTemplets.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdSMSTemplets.Location = new System.Drawing.Point(12, 157);
            this.grdSMSTemplets.Name = "grdSMSTemplets";
            this.grdSMSTemplets.PKFieldColumnName = "";
            this.grdSMSTemplets.ShowImageOnActionButton = true;
            this.grdSMSTemplets.Size = new System.Drawing.Size(702, 216);
            this.grdSMSTemplets.TabIndex = 116;
            this.grdSMSTemplets.Text = "myGridView1";
            // 
            // btnClearSMSTemplet
            // 
            this.btnClearSMSTemplet.Image = global::Taxi_AppMain.Properties.Resources.delete;
            this.btnClearSMSTemplet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearSMSTemplet.Location = new System.Drawing.Point(639, 87);
            this.btnClearSMSTemplet.Name = "btnClearSMSTemplet";
            this.btnClearSMSTemplet.Size = new System.Drawing.Size(78, 30);
            this.btnClearSMSTemplet.TabIndex = 115;
            this.btnClearSMSTemplet.Text = "Clear";
            this.btnClearSMSTemplet.UseVisualStyleBackColor = true;
            this.btnClearSMSTemplet.Click += new System.EventHandler(this.btnClearSMSTemplet_Click);
            // 
            // btnAddSMSTemplet
            // 
            this.btnAddSMSTemplet.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAddSMSTemplet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddSMSTemplet.Location = new System.Drawing.Point(637, 49);
            this.btnAddSMSTemplet.Name = "btnAddSMSTemplet";
            this.btnAddSMSTemplet.Size = new System.Drawing.Size(80, 30);
            this.btnAddSMSTemplet.TabIndex = 114;
            this.btnAddSMSTemplet.Text = "Add";
            this.btnAddSMSTemplet.UseVisualStyleBackColor = true;
            this.btnAddSMSTemplet.Click += new System.EventHandler(this.btnAddSMSTemplet_Click);
            // 
            // frmAddDrivertemplet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 475);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.FormTitle = "Add Driver Templates";
            this.KeyPreview = true;
            this.Name = "frmAddDrivertemplet";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Add Driver Templates";
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
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSMSTemplets.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSMSTemplets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private UI.MyGridView grdSMSTemplets;
        private System.Windows.Forms.Button btnClearSMSTemplet;
        private System.Windows.Forms.Button btnAddSMSTemplet;
        private Telerik.WinControls.UI.RadLabel radLabel21;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private System.Windows.Forms.RichTextBox TxtSMSTemplet;
    }
}