namespace Taxi_AppMain
{
    partial class frmImportBookingTemplate2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportBookingTemplate2));
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.BtnClearGrid = new Telerik.WinControls.UI.RadButton();
            this.btnPasteBooking = new Telerik.WinControls.UI.RadButton();
            this.grdBookings = new UI.MyGridView();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnClearGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPasteBooking)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveOn.Image")));
            this.btnSaveOn.Location = new System.Drawing.Point(668, 659);
            this.btnSaveOn.Size = new System.Drawing.Size(130, 50);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(825, 658);
            this.btnExit.Size = new System.Drawing.Size(130, 50);
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(834, 352);
            this.btnSaveAndClose.Size = new System.Drawing.Size(130, 49);
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radPanel1.Controls.Add(this.BtnClearGrid);
            this.radPanel1.Controls.Add(this.btnPasteBooking);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1139, 58);
            this.radPanel1.TabIndex = 106;
            // 
            // BtnClearGrid
            // 
            this.BtnClearGrid.Image = global::Taxi_AppMain.Properties.Resources.icon_delete;
            this.BtnClearGrid.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.BtnClearGrid.Location = new System.Drawing.Point(180, 5);
            this.BtnClearGrid.Name = "BtnClearGrid";
            this.BtnClearGrid.Size = new System.Drawing.Size(130, 49);
            this.BtnClearGrid.TabIndex = 1;
            this.BtnClearGrid.Text = "Clear All";
            this.BtnClearGrid.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnClearGrid.Click += new System.EventHandler(this.BtnClearGrid_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.BtnClearGrid.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.icon_delete;
            ((Telerik.WinControls.UI.RadButtonElement)(this.BtnClearGrid.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.BtnClearGrid.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.BtnClearGrid.GetChildAt(0))).Text = "Clear All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.BtnClearGrid.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.BtnClearGrid.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPasteBooking
            // 
            this.btnPasteBooking.Image = global::Taxi_AppMain.Properties.Resources.paste;
            this.btnPasteBooking.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPasteBooking.Location = new System.Drawing.Point(19, 5);
            this.btnPasteBooking.Name = "btnPasteBooking";
            this.btnPasteBooking.Size = new System.Drawing.Size(130, 49);
            this.btnPasteBooking.TabIndex = 0;
            this.btnPasteBooking.Text = "Browse Template";
            this.btnPasteBooking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPasteBooking.Click += new System.EventHandler(this.btnPasteBooking_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteBooking.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.paste;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteBooking.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteBooking.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteBooking.GetChildAt(0))).Text = "Browse Template";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPasteBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPasteBooking.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdBookings
            // 
            this.grdBookings.AutoCellFormatting = false;
            this.grdBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBookings.EnableCheckInCheckOut = false;
            this.grdBookings.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdBookings.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdBookings.Location = new System.Drawing.Point(0, 96);
            // 
            // grdBookings
            // 
            this.grdBookings.MasterTemplate.AllowAddNewRow = false;
            this.grdBookings.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdBookings.Name = "grdBookings";
            this.grdBookings.PKFieldColumnName = "";
            this.grdBookings.ShowGroupPanel = false;
            this.grdBookings.ShowImageOnActionButton = true;
            this.grdBookings.Size = new System.Drawing.Size(1139, 557);
            this.grdBookings.TabIndex = 107;
            this.grdBookings.Text = "myGridView1";
            // 
            // radPanel2
            // 
            this.radPanel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 653);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(1139, 58);
            this.radPanel2.TabIndex = 108;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmImportBookingTemplate2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 711);
            this.ControlBox = true;
            this.Controls.Add(this.grdBookings);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.FormTitle = "Import Booking";
            this.Name = "frmImportBookingTemplate2";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.ShowSaveAndCloseButton = true;
            this.ShowSaveButton = true;
            this.Text = "Import Booking";
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.radPanel2, 0);
            this.Controls.SetChildIndex(this.grdBookings, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BtnClearGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPasteBooking)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private UI.MyGridView grdBookings;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadButton btnPasteBooking;
        private Telerik.WinControls.UI.RadButton BtnClearGrid;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}