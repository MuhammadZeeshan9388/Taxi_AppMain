namespace Taxi_AppMain
{
    partial class frmShifts
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
            this.grdShifts = new UI.MyGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpTOTime = new UI.MyDatePicker();
            this.dtpFromTime = new UI.MyDatePicker();
            this.btnClearAvail = new System.Windows.Forms.Button();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.radLabel13 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
            this.ddlDriver = new UI.MyDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.ddlShifts = new UI.MyDropDownList();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTOTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlShifts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(696, 187);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(696, 116);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(696, 266);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(696, 340);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdShifts);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.radLabel11);
            this.panel1.Location = new System.Drawing.Point(12, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 197);
            this.panel1.TabIndex = 106;
            // 
            // grdShifts
            // 
            this.grdShifts.AutoCellFormatting = false;
            this.grdShifts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShifts.EnableCheckInCheckOut = false;
            this.grdShifts.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdShifts.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdShifts.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdShifts.Location = new System.Drawing.Point(0, 73);
            // 
            // grdShifts
            // 
            this.grdShifts.MasterTemplate.AllowAddNewRow = false;
            this.grdShifts.MasterTemplate.AllowEditRow = false;
            this.grdShifts.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdShifts.Name = "grdShifts";
            this.grdShifts.PKFieldColumnName = "";
            this.grdShifts.ShowGroupPanel = false;
            this.grdShifts.ShowImageOnActionButton = true;
            this.grdShifts.Size = new System.Drawing.Size(609, 124);
            this.grdShifts.TabIndex = 97;
            this.grdShifts.Text = "myGridView1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpTOTime);
            this.panel2.Controls.Add(this.dtpFromTime);
            this.panel2.Controls.Add(this.btnClearAvail);
            this.panel2.Controls.Add(this.radLabel15);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.radLabel13);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(609, 55);
            this.panel2.TabIndex = 96;
            // 
            // dtpTOTime
            // 
            this.dtpTOTime.AllowDrop = true;
            this.dtpTOTime.CustomFormat = "HH:mm";
            this.dtpTOTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTOTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpTOTime.Location = new System.Drawing.Point(262, 22);
            this.dtpTOTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpTOTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTOTime.Name = "dtpTOTime";
            this.dtpTOTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTOTime.Size = new System.Drawing.Size(83, 24);
            this.dtpTOTime.TabIndex = 68;
            this.dtpTOTime.TabStop = false;
            this.dtpTOTime.Text = "myDatePicker1";
            this.dtpTOTime.Value = null;
            this.dtpTOTime.Opening += new System.ComponentModel.CancelEventHandler(this.dtpTOTime_Opening);
            // 
            // dtpFromTime
            // 
            this.dtpFromTime.AllowDrop = true;
            this.dtpFromTime.CustomFormat = "HH:mm";
            this.dtpFromTime.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromTime.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtpFromTime.Location = new System.Drawing.Point(101, 21);
            this.dtpFromTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpFromTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromTime.Name = "dtpFromTime";
            this.dtpFromTime.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpFromTime.Size = new System.Drawing.Size(83, 24);
            this.dtpFromTime.TabIndex = 67;
            this.dtpFromTime.TabStop = false;
            this.dtpFromTime.Text = "myDatePicker1";
            this.dtpFromTime.Value = null;
            this.dtpFromTime.Opening += new System.ComponentModel.CancelEventHandler(this.dtpFromTime_Opening);
            // 
            // btnClearAvail
            // 
            this.btnClearAvail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearAvail.Location = new System.Drawing.Point(523, 23);
            this.btnClearAvail.Name = "btnClearAvail";
            this.btnClearAvail.Size = new System.Drawing.Size(53, 23);
            this.btnClearAvail.TabIndex = 4;
            this.btnClearAvail.Text = "New";
            this.btnClearAvail.UseVisualStyleBackColor = true;
            this.btnClearAvail.Click += new System.EventHandler(this.btnClearAvail_Click);
            // 
            // radLabel15
            // 
            this.radLabel15.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(200, 27);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(51, 18);
            this.radLabel15.TabIndex = 65;
            this.radLabel15.Text = "To Time";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Taxi_AppMain.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(434, 23);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(72, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // radLabel13
            // 
            this.radLabel13.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel13.Location = new System.Drawing.Point(31, 23);
            this.radLabel13.Name = "radLabel13";
            this.radLabel13.Size = new System.Drawing.Size(65, 18);
            this.radLabel13.TabIndex = 63;
            this.radLabel13.Text = "From Time";
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
            this.radLabel11.Size = new System.Drawing.Size(609, 18);
            this.radLabel11.TabIndex = 95;
            this.radLabel11.Text = "Driver Shifts";
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel11.GetChildAt(0))).BorderVisible = true;
            ((Telerik.WinControls.UI.RadLabelElement)(this.radLabel11.GetChildAt(0))).Text = "Driver Shifts";
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).FitToSizeMode = Telerik.WinControls.RadFitToSizeMode.FitToParentContent;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).BoxStyle = Telerik.WinControls.BorderBoxStyle.FourBorders;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radLabel11.GetChildAt(0).GetChildAt(1))).BottomWidth = 0F;
            // 
            // ddlDriver
            // 
            this.ddlDriver.Caption = null;
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(88, 44);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Property = null;
            this.ddlDriver.ShowDownArrow = true;
            this.ddlDriver.Size = new System.Drawing.Size(231, 26);
            this.ddlDriver.TabIndex = 111;
            this.ddlDriver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlDriver_SelectedIndexChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(19, 48);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(59, 23);
            this.radLabel1.TabIndex = 110;
            this.radLabel1.Text = "Drivers";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnSave.Location = new System.Drawing.Point(386, 293);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(109, 39);
            this.btnSave.TabIndex = 213;
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
            this.btnExitForm.Location = new System.Drawing.Point(501, 293);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(104, 39);
            this.btnExitForm.TabIndex = 212;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.Click += new System.EventHandler(this.btnExitForm_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // ddlShifts
            // 
            this.ddlShifts.Caption = null;
            this.ddlShifts.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlShifts.Location = new System.Drawing.Point(405, 45);
            this.ddlShifts.Name = "ddlShifts";
            this.ddlShifts.Property = null;
            this.ddlShifts.ShowDownArrow = true;
            this.ddlShifts.Size = new System.Drawing.Size(179, 26);
            this.ddlShifts.TabIndex = 214;
            this.ddlShifts.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlShifts_SelectedIndexChanged);
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(348, 47);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(48, 23);
            this.radLabel2.TabIndex = 111;
            this.radLabel2.Text = "Shifts";
            // 
            // frmShifts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 340);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.ddlShifts);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExitForm);
            this.Controls.Add(this.ddlDriver);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.panel1);
            this.FormTitle = "Driver Shift";
            this.Name = "frmShifts";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "frmShifts";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmShifts_Load);
            this.Shown += new System.EventHandler(this.frmShifts_Shown);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.radLabel1, 0);
            this.Controls.SetChildIndex(this.ddlDriver, 0);
            this.Controls.SetChildIndex(this.btnExitForm, 0);
            this.Controls.SetChildIndex(this.btnSave, 0);
            this.Controls.SetChildIndex(this.ddlShifts, 0);
            this.Controls.SetChildIndex(this.radLabel2, 0);
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
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShifts)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTOTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlShifts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClearAvail;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        private System.Windows.Forms.Button btnAdd;
        private Telerik.WinControls.UI.RadLabel radLabel13;
        private Telerik.WinControls.UI.RadLabel radLabel11;
        private UI.MyDatePicker dtpTOTime;
        private UI.MyDatePicker dtpFromTime;
        private UI.MyDropDownList ddlDriver;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private UI.MyDropDownList ddlShifts;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private UI.MyGridView grdShifts;
    }
}
