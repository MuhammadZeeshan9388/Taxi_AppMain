namespace Taxi_AppMain
{
    partial class frmOnlineBookingSettings
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
            this.chkDisableWebBooking = new Telerik.WinControls.UI.RadCheckBox();
            this.chkDisableAppBooking = new Telerik.WinControls.UI.RadCheckBox();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.grdZones = new Telerik.WinControls.UI.RadGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdOnlineBookingSettings = new Telerik.WinControls.UI.RadGridView();
            this.radLabel24 = new Telerik.WinControls.UI.RadLabel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisableWebBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisableAppBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZones.MasterTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdOnlineBookingSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOnlineBookingSettings.MasterTemplate)).BeginInit();
            this.grdOnlineBookingSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // chkDisableWebBooking
            // 
            this.chkDisableWebBooking.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.chkDisableWebBooking.Location = new System.Drawing.Point(12, 53);
            this.chkDisableWebBooking.Name = "chkDisableWebBooking";
            this.chkDisableWebBooking.Size = new System.Drawing.Size(270, 22);
            this.chkDisableWebBooking.TabIndex = 106;
            this.chkDisableWebBooking.Text = "Permanent Block Online Booking";
            // 
            // chkDisableAppBooking
            // 
            this.chkDisableAppBooking.Font = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Bold);
            this.chkDisableAppBooking.Location = new System.Drawing.Point(485, 48);
            this.chkDisableAppBooking.Name = "chkDisableAppBooking";
            this.chkDisableAppBooking.Size = new System.Drawing.Size(146, 20);
            this.chkDisableAppBooking.TabIndex = 107;
            this.chkDisableAppBooking.Text = "Disable App Booking";
            this.chkDisableAppBooking.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(203, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 56);
            this.btnSave.TabIndex = 208;
            this.btnSave.Text = "Save && Close";
            this.btnSave.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save && Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExitForm
            // 
            this.btnExitForm.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitForm.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitForm.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExitForm.Location = new System.Drawing.Point(355, 6);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(112, 56);
            this.btnExitForm.TabIndex = 207;
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdZones
            // 
            this.grdZones.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdZones.Location = new System.Drawing.Point(0, 415);
            // 
            // grdZones
            // 
            this.grdZones.MasterTemplate.AllowDeleteRow = false;
            this.grdZones.Name = "grdZones";
            this.grdZones.Size = new System.Drawing.Size(723, 208);
            this.grdZones.TabIndex = 209;
            this.grdZones.Text = "myGridView1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExitForm);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 629);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 72);
            this.panel1.TabIndex = 210;
            // 
            // grdOnlineBookingSettings
            // 
            this.grdOnlineBookingSettings.Controls.Add(this.chkDisableAppBooking);
            this.grdOnlineBookingSettings.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdOnlineBookingSettings.Location = new System.Drawing.Point(-3, 131);
            // 
            // grdOnlineBookingSettings
            // 
            this.grdOnlineBookingSettings.MasterTemplate.AllowDeleteRow = false;
            this.grdOnlineBookingSettings.Name = "grdOnlineBookingSettings";
            this.grdOnlineBookingSettings.Size = new System.Drawing.Size(726, 233);
            this.grdOnlineBookingSettings.TabIndex = 211;
            this.grdOnlineBookingSettings.Text = "myGridView1";
            // 
            // radLabel24
            // 
            this.radLabel24.AutoSize = false;
            this.radLabel24.BackColor = System.Drawing.Color.DarkBlue;
            this.radLabel24.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel24.ForeColor = System.Drawing.Color.White;
            this.radLabel24.Location = new System.Drawing.Point(0, 95);
            this.radLabel24.Name = "radLabel24";
            // 
            // 
            // 
            this.radLabel24.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel24.Size = new System.Drawing.Size(723, 35);
            this.radLabel24.TabIndex = 213;
            this.radLabel24.Text = "Restrict ASAP Booking by Date/Time";
            this.radLabel24.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescription
            // 
            this.txtDescription.Enabled = false;
            this.txtDescription.Location = new System.Drawing.Point(416, 53);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(206, 36);
            this.txtDescription.TabIndex = 214;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 215;
            this.label1.Text = "Message";
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.DarkBlue;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(6, 380);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(723, 35);
            this.radLabel1.TabIndex = 216;
            this.radLabel1.Text = "Restrict Booking by Plot";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmOnlineBookingSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 701);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.radLabel24);
            this.Controls.Add(this.grdOnlineBookingSettings);
            this.Controls.Add(this.grdZones);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chkDisableWebBooking);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Online Booking Settings";
            this.Name = "frmOnlineBookingSettings";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Online Booking Settings";
            this.ThemeName = "ControlDefault";
            this.Load += new System.EventHandler(this.frmOnlineBookingSettings_Load);
            this.Controls.SetChildIndex(this.chkDisableWebBooking, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.grdZones, 0);
            this.Controls.SetChildIndex(this.grdOnlineBookingSettings, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radLabel24, 0);
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.radLabel1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisableWebBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDisableAppBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZones.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZones)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdOnlineBookingSettings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdOnlineBookingSettings)).EndInit();
            this.grdOnlineBookingSettings.ResumeLayout(false);
            this.grdOnlineBookingSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadCheckBox chkDisableWebBooking;
        private Telerik.WinControls.UI.RadCheckBox chkDisableAppBooking;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private Telerik.WinControls.UI.RadGridView grdZones;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadGridView grdOnlineBookingSettings;
        private Telerik.WinControls.UI.RadLabel radLabel24;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}
