namespace Taxi_AppMain
{
    partial class frmCreateGroup
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
            this.ddlZone = new System.Windows.Forms.ComboBox();
            this.lblPickupPlot = new System.Windows.Forms.Label();
            this.radLabel27 = new Telerik.WinControls.UI.RadLabel();
            this.lblPassengers = new Telerik.WinControls.UI.RadLabel();
            this.num_TotalPassengers = new Telerik.WinControls.UI.RadSpinEditor();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.btnSaveNew = new Telerik.WinControls.UI.RadButton();
            this.lblReservedSeats = new Telerik.WinControls.UI.RadLabel();
            this.numReservedSeats = new Telerik.WinControls.UI.RadSpinEditor();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPassengers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReservedSeats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReservedSeats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlZone
            // 
            this.ddlZone.BackColor = System.Drawing.Color.White;
            this.ddlZone.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlZone.Location = new System.Drawing.Point(117, 60);
            this.ddlZone.Name = "ddlZone";
            this.ddlZone.Size = new System.Drawing.Size(286, 24);
            this.ddlZone.TabIndex = 266;
            // 
            // lblPickupPlot
            // 
            this.lblPickupPlot.AutoSize = true;
            this.lblPickupPlot.BackColor = System.Drawing.Color.Transparent;
            this.lblPickupPlot.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPickupPlot.Location = new System.Drawing.Point(16, 60);
            this.lblPickupPlot.Name = "lblPickupPlot";
            this.lblPickupPlot.Size = new System.Drawing.Size(95, 18);
            this.lblPickupPlot.TabIndex = 265;
            this.lblPickupPlot.Text = "Select Zone";
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
            this.radLabel27.Size = new System.Drawing.Size(415, 28);
            this.radLabel27.TabIndex = 267;
            this.radLabel27.Text = "New Group";
            this.radLabel27.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPassengers
            // 
            this.lblPassengers.BackColor = System.Drawing.Color.Transparent;
            this.lblPassengers.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassengers.ForeColor = System.Drawing.Color.Black;
            this.lblPassengers.Location = new System.Drawing.Point(15, 101);
            this.lblPassengers.Name = "lblPassengers";
            // 
            // 
            // 
            this.lblPassengers.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblPassengers.Size = new System.Drawing.Size(96, 22);
            this.lblPassengers.TabIndex = 269;
            this.lblPassengers.Text = "No of Seats";
            // 
            // num_TotalPassengers
            // 
            this.num_TotalPassengers.EnableKeyMap = true;
            this.num_TotalPassengers.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_TotalPassengers.InterceptArrowKeys = false;
            this.num_TotalPassengers.Location = new System.Drawing.Point(117, 100);
            this.num_TotalPassengers.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.num_TotalPassengers.Name = "num_TotalPassengers";
            // 
            // 
            // 
            this.num_TotalPassengers.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.num_TotalPassengers.ShowBorder = true;
            this.num_TotalPassengers.ShowUpDownButtons = false;
            this.num_TotalPassengers.Size = new System.Drawing.Size(56, 24);
            this.num_TotalPassengers.TabIndex = 268;
            this.num_TotalPassengers.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.num_TotalPassengers.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.num_TotalPassengers.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExitForm
            // 
            this.btnExitForm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitForm.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitForm.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExitForm.Location = new System.Drawing.Point(236, 187);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(112, 56);
            this.btnExitForm.TabIndex = 271;
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
            // btnSaveNew
            // 
            this.btnSaveNew.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNew.Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            this.btnSaveNew.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveNew.Location = new System.Drawing.Point(69, 186);
            this.btnSaveNew.Name = "btnSaveNew";
            this.btnSaveNew.Size = new System.Drawing.Size(112, 56);
            this.btnSaveNew.TabIndex = 270;
            this.btnSaveNew.Text = "Save Group";
            this.btnSaveNew.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveNew.Click += new System.EventHandler(this.btnSaveNew_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveNew.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Tick31;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveNew.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveNew.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveNew.GetChildAt(0))).Text = "Save Group";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveNew.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveNew.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblReservedSeats
            // 
            this.lblReservedSeats.BackColor = System.Drawing.Color.Transparent;
            this.lblReservedSeats.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservedSeats.ForeColor = System.Drawing.Color.Red;
            this.lblReservedSeats.Location = new System.Drawing.Point(210, 102);
            this.lblReservedSeats.Name = "lblReservedSeats";
            // 
            // 
            // 
            this.lblReservedSeats.RootElement.ForeColor = System.Drawing.Color.Red;
            this.lblReservedSeats.Size = new System.Drawing.Size(80, 22);
            this.lblReservedSeats.TabIndex = 273;
            this.lblReservedSeats.Text = "Reserved";
            this.lblReservedSeats.Visible = false;
            // 
            // numReservedSeats
            // 
            this.numReservedSeats.EnableKeyMap = true;
            this.numReservedSeats.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReservedSeats.ForeColor = System.Drawing.Color.Red;
            this.numReservedSeats.InterceptArrowKeys = false;
            this.numReservedSeats.Location = new System.Drawing.Point(301, 100);
            this.numReservedSeats.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numReservedSeats.Name = "numReservedSeats";
            // 
            // 
            // 
            this.numReservedSeats.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numReservedSeats.RootElement.ForeColor = System.Drawing.Color.Red;
            this.numReservedSeats.ShowBorder = true;
            this.numReservedSeats.ShowUpDownButtons = false;
            this.numReservedSeats.Size = new System.Drawing.Size(56, 24);
            this.numReservedSeats.TabIndex = 272;
            this.numReservedSeats.TabStop = false;
            this.numReservedSeats.Visible = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numReservedSeats.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numReservedSeats.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReservedSeats.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReservedSeats.GetChildAt(0).GetChildAt(2).GetChildAt(1))).ForeColor = System.Drawing.Color.Red;
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numReservedSeats.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmCreateGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 270);
            this.Controls.Add(this.lblReservedSeats);
            this.Controls.Add(this.numReservedSeats);
            this.Controls.Add(this.btnExitForm);
            this.Controls.Add(this.btnSaveNew);
            this.Controls.Add(this.lblPassengers);
            this.Controls.Add(this.num_TotalPassengers);
            this.Controls.Add(this.radLabel27);
            this.Controls.Add(this.ddlZone);
            this.Controls.Add(this.lblPickupPlot);
            this.Name = "frmCreateGroup";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "New Group";
            this.ThemeName = "ControlDefault";
            this.Controls.SetChildIndex(this.lblPickupPlot, 0);
            this.Controls.SetChildIndex(this.ddlZone, 0);
            this.Controls.SetChildIndex(this.radLabel27, 0);
            this.Controls.SetChildIndex(this.num_TotalPassengers, 0);
            this.Controls.SetChildIndex(this.lblPassengers, 0);
            this.Controls.SetChildIndex(this.btnSaveNew, 0);
            this.Controls.SetChildIndex(this.btnExitForm, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.numReservedSeats, 0);
            this.Controls.SetChildIndex(this.lblReservedSeats, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPassengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_TotalPassengers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblReservedSeats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReservedSeats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlZone;
        private System.Windows.Forms.Label lblPickupPlot;
        private Telerik.WinControls.UI.RadLabel radLabel27;
        private Telerik.WinControls.UI.RadLabel lblPassengers;
        private Telerik.WinControls.UI.RadSpinEditor num_TotalPassengers;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private Telerik.WinControls.UI.RadButton btnSaveNew;
        private Telerik.WinControls.UI.RadLabel lblReservedSeats;
        private Telerik.WinControls.UI.RadSpinEditor numReservedSeats;
    }
}
