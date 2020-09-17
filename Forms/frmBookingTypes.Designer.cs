namespace Taxi_AppMain
{
    partial class frmBookingTypes
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
            this.grdBookingType = new UI.MyGridView();
            this.btnSaveClose = new Telerik.WinControls.UI.RadButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookingType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookingType.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(519, 0);
            this.btnExit.Size = new System.Drawing.Size(77, 38);
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // grdBookingType
            // 
            this.grdBookingType.AutoCellFormatting = false;
            this.grdBookingType.EnableCheckInCheckOut = false;
            this.grdBookingType.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdBookingType.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdBookingType.Location = new System.Drawing.Point(0, 42);
            // 
            // grdBookingType
            // 
            this.grdBookingType.MasterTemplate.AllowAddNewRow = false;
            this.grdBookingType.Name = "grdBookingType";
            this.grdBookingType.PKFieldColumnName = "";
            this.grdBookingType.ShowGroupPanel = false;
            this.grdBookingType.ShowImageOnActionButton = true;
            this.grdBookingType.Size = new System.Drawing.Size(411, 406);
            this.grdBookingType.TabIndex = 110;
            this.grdBookingType.Text = "radGridView1";
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnSaveClose.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSaveClose.Location = new System.Drawing.Point(460, 217);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(101, 80);
            this.btnSaveClose.TabIndex = 112;
            this.btnSaveClose.Text = "Save && Close";
            this.btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).Text = "Save && Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmBookingTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 456);
            this.Controls.Add(this.btnSaveClose);
            this.Controls.Add(this.grdBookingType);
            this.FixedExitButtonOnTopRight = true;
            this.FormTitle = "Booking Types Color Coding";
            this.Name = "frmBookingTypes";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.Text = "Booking Types Color Codings";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.grdBookingType, 0);
            this.Controls.SetChildIndex(this.btnSaveClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookingType.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookingType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MyGridView grdBookingType;
        private Telerik.WinControls.UI.RadButton btnSaveClose;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}