namespace Taxi_AppMain
{
    partial class frmManageKeys
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewCommandColumn gridViewCommandColumn1 = new Telerik.WinControls.UI.GridViewCommandColumn();
            this.lblAirportPickupChrges = new Telerik.WinControls.UI.RadLabel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnGetKey = new Telerik.WinControls.UI.RadButton();
            this.grdETAKeys = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.lblAirportPickupChrges)).BeginInit();
            this.lblAirportPickupChrges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGetKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdETAKeys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdETAKeys.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAirportPickupChrges
            // 
            this.lblAirportPickupChrges.AutoSize = false;
            this.lblAirportPickupChrges.BackColor = System.Drawing.Color.Wheat;
            this.lblAirportPickupChrges.BorderVisible = true;
            this.lblAirportPickupChrges.Controls.Add(this.btnSave);
            this.lblAirportPickupChrges.Controls.Add(this.btnGetKey);
            this.lblAirportPickupChrges.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAirportPickupChrges.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblAirportPickupChrges.ForeColor = System.Drawing.Color.Black;
            this.lblAirportPickupChrges.Location = new System.Drawing.Point(0, 0);
            this.lblAirportPickupChrges.Name = "lblAirportPickupChrges";
            // 
            // 
            // 
            this.lblAirportPickupChrges.RootElement.ForeColor = System.Drawing.Color.Black;
            this.lblAirportPickupChrges.Size = new System.Drawing.Size(506, 39);
            this.lblAirportPickupChrges.TabIndex = 146;
            this.lblAirportPickupChrges.Text = "Manage keys";
            this.lblAirportPickupChrges.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(385, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(117, 24);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnGetKey
            // 
            this.btnGetKey.Location = new System.Drawing.Point(12, 9);
            this.btnGetKey.Name = "btnGetKey";
            this.btnGetKey.Size = new System.Drawing.Size(117, 24);
            this.btnGetKey.TabIndex = 18;
            this.btnGetKey.Text = "Get key";
            this.btnGetKey.Click += new System.EventHandler(this.btnGetKey_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnGetKey.GetChildAt(0))).Text = "Get key";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnGetKey.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnGetKey.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdETAKeys
            // 
            this.grdETAKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdETAKeys.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdETAKeys.Location = new System.Drawing.Point(0, 39);
            // 
            // grdETAKeys
            // 
            this.grdETAKeys.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Key";
            gridViewTextBoxColumn1.Name = "column1";
            gridViewTextBoxColumn1.Width = 400;
            gridViewCommandColumn1.DefaultText = "Delete";
            gridViewCommandColumn1.FormatString = "";
            gridViewCommandColumn1.HeaderText = "";
            gridViewCommandColumn1.Name = "column2";
            gridViewCommandColumn1.UseDefaultText = true;
            gridViewCommandColumn1.Width = 80;
            this.grdETAKeys.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewCommandColumn1});
            this.grdETAKeys.Name = "grdETAKeys";
            this.grdETAKeys.ShowGroupPanel = false;
            this.grdETAKeys.Size = new System.Drawing.Size(506, 363);
            this.grdETAKeys.TabIndex = 145;
            this.grdETAKeys.Text = "radGridView2";
            // 
            // frmManageKeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 402);
            this.Controls.Add(this.grdETAKeys);
            this.Controls.Add(this.lblAirportPickupChrges);
            this.MaximizeBox = false;
            this.Name = "frmManageKeys";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.lblAirportPickupChrges)).EndInit();
            this.lblAirportPickupChrges.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGetKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdETAKeys.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdETAKeys)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel lblAirportPickupChrges;
        private Telerik.WinControls.UI.RadGridView grdETAKeys;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnGetKey;
    }
}