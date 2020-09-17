namespace Taxi_AppMain
{
    partial class frmAddFaresToWeb
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
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.grdFares = new UI.MyGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblshowexport = new System.Windows.Forms.Label();
            this.lblExportingStatus = new System.Windows.Forms.Label();
            this.btnExportToWeb = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFares.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportToWeb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnCancel.Location = new System.Drawing.Point(865, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(113, 59);
            this.btnCancel.TabIndex = 50;
            this.btnCancel.Text = "Exit";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnCancel.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnCancel.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdFares
            // 
            this.grdFares.AutoCellFormatting = false;
            this.grdFares.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFares.EnableCheckInCheckOut = false;
            this.grdFares.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdFares.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdFares.Location = new System.Drawing.Point(0, 38);
            // 
            // grdFares
            // 
            this.grdFares.MasterTemplate.AllowAddNewRow = false;
            this.grdFares.Name = "grdFares";
            this.grdFares.PKFieldColumnName = "";
            this.grdFares.ShowGroupPanel = false;
            this.grdFares.ShowImageOnActionButton = true;
            this.grdFares.Size = new System.Drawing.Size(991, 397);
            this.grdFares.TabIndex = 107;
            this.grdFares.Text = "myGridView1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblshowexport);
            this.panel2.Controls.Add(this.lblExportingStatus);
            this.panel2.Controls.Add(this.btnExportToWeb);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 435);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(991, 70);
            this.panel2.TabIndex = 108;
            // 
            // lblshowexport
            // 
            this.lblshowexport.AutoSize = true;
            this.lblshowexport.Location = new System.Drawing.Point(7, 50);
            this.lblshowexport.Name = "lblshowexport";
            this.lblshowexport.Size = new System.Drawing.Size(0, 16);
            this.lblshowexport.TabIndex = 53;
            // 
            // lblExportingStatus
            // 
            this.lblExportingStatus.AutoSize = true;
            this.lblExportingStatus.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExportingStatus.Location = new System.Drawing.Point(7, 9);
            this.lblExportingStatus.Name = "lblExportingStatus";
            this.lblExportingStatus.Size = new System.Drawing.Size(0, 16);
            this.lblExportingStatus.TabIndex = 52;
            // 
            // btnExportToWeb
            // 
            this.btnExportToWeb.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExportToWeb.Location = new System.Drawing.Point(743, 6);
            this.btnExportToWeb.Name = "btnExportToWeb";
            this.btnExportToWeb.Size = new System.Drawing.Size(114, 58);
            this.btnExportToWeb.TabIndex = 51;
            this.btnExportToWeb.Text = "Export";
            this.btnExportToWeb.Click += new System.EventHandler(this.btnExportToWeb_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportToWeb.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExportToWeb.GetChildAt(0))).Text = "Export";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExportToWeb.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExportToWeb.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmAddFaresToWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 505);
            this.ControlBox = true;
            this.Controls.Add(this.grdFares);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Export Fares To Web";
            this.Name = "frmAddFaresToWeb";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Export Fares To Web All VehicleType";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.grdFares, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFares.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFares)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportToWeb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MyGridView grdFares;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadButton btnExportToWeb;
        private System.Windows.Forms.Label lblshowexport;
        private System.Windows.Forms.Label lblExportingStatus;
    }
}