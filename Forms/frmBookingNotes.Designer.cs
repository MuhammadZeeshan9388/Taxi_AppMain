namespace Taxi_AppMain
{
    partial class frmBookingNotes
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
            this.grdNotes = new UI.MyGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtNotes = new Telerik.WinControls.UI.RadTextBox();
            this.btnClearAvail = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNotes.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(655, 187);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(655, 116);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(655, 266);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(655, 340);
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(655, 415);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdNotes);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.radLabel11);
            this.panel1.Location = new System.Drawing.Point(12, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(713, 507);
            this.panel1.TabIndex = 107;
            // 
            // grdNotes
            // 
            this.grdNotes.AutoCellFormatting = false;
            this.grdNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdNotes.EnableCheckInCheckOut = false;
            this.grdNotes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdNotes.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdNotes.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdNotes.Location = new System.Drawing.Point(0, 146);
            // 
            // grdNotes
            // 
            this.grdNotes.MasterTemplate.AllowAddNewRow = false;
            this.grdNotes.MasterTemplate.AllowEditRow = false;
            this.grdNotes.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdNotes.Name = "grdNotes";
            this.grdNotes.PKFieldColumnName = "";
            this.grdNotes.ShowGroupPanel = false;
            this.grdNotes.ShowImageOnActionButton = true;
            this.grdNotes.Size = new System.Drawing.Size(713, 361);
            this.grdNotes.TabIndex = 97;
            this.grdNotes.Text = "myGridView1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radLabel1);
            this.panel2.Controls.Add(this.txtNotes);
            this.panel2.Controls.Add(this.btnClearAvail);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(713, 128);
            this.panel2.TabIndex = 96;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(13, 27);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(42, 23);
            this.radLabel1.TabIndex = 5;
            this.radLabel1.Text = "Note";
            // 
            // txtNotes
            // 
            this.txtNotes.AutoScroll = true;
            this.txtNotes.Location = new System.Drawing.Point(61, 6);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            // 
            // 
            // 
            this.txtNotes.RootElement.StretchVertically = true;
            this.txtNotes.Size = new System.Drawing.Size(460, 112);
            this.txtNotes.TabIndex = 1;
            this.txtNotes.TabStop = false;
            // 
            // btnClearAvail
            // 
            this.btnClearAvail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearAvail.Location = new System.Drawing.Point(636, 27);
            this.btnClearAvail.Name = "btnClearAvail";
            this.btnClearAvail.Size = new System.Drawing.Size(53, 23);
            this.btnClearAvail.TabIndex = 4;
            this.btnClearAvail.Text = "New";
            this.btnClearAvail.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(547, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // radLabel11
            // 
            this.radLabel11.AutoSize = false;
            this.radLabel11.BackColor = System.Drawing.Color.Purple;
            this.radLabel11.BorderVisible = true;
            this.radLabel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel11.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel11.ForeColor = System.Drawing.Color.White;
            this.radLabel11.Location = new System.Drawing.Point(0, 0);
            this.radLabel11.Name = "radLabel11";
            // 
            // 
            // 
            this.radLabel11.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel11.Size = new System.Drawing.Size(713, 18);
            this.radLabel11.TabIndex = 95;
            this.radLabel11.Text = "Booking Notes";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel11.GetChildAt(0))).BorderVisible = true;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel11.GetChildAt(0))).Text = "Booking Notes";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).BottomWidth = 0F;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnSave.Location = new System.Drawing.Point(454, 583);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 53);
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
            this.btnExitForm.Location = new System.Drawing.Point(615, 583);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(104, 53);
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
            // frmBookingNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 638);
            this.ControlBox = true;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExitForm);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.FormTitle = "Booking Notes";
            this.KeyPreview = true;
            this.Name = "frmBookingNotes";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Notes";
            this.ThemeName = "ControlDefault";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBookingNotes_KeyDown);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.btnExitForm, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdNotes.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNotes)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private UI.MyGridView grdNotes;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadTextBox txtNotes;
        private System.Windows.Forms.Button btnClearAvail;
        private System.Windows.Forms.Button btnAdd;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnExitForm;
    }
}
