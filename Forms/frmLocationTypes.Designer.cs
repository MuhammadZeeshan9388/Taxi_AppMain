namespace Taxi_AppMain
{
    partial class frmLocationTypes
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
            this.grdLocationTypes = new UI.MyGridView();
            this.btnSaveCloseLocationType = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocationTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocationTypes.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveCloseLocationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(519, 0);
            this.btnExit.Size = new System.Drawing.Size(77, 38);
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // grdLocationTypes
            // 
            this.grdLocationTypes.AutoCellFormatting = false;
            this.grdLocationTypes.EnableCheckInCheckOut = false;
            this.grdLocationTypes.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLocationTypes.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLocationTypes.Location = new System.Drawing.Point(0, 38);
            // 
            // grdLocationTypes
            // 
            this.grdLocationTypes.MasterTemplate.AllowAddNewRow = false;
            this.grdLocationTypes.Name = "grdLocationTypes";
            this.grdLocationTypes.PKFieldColumnName = "";
            this.grdLocationTypes.ShowGroupPanel = false;
            this.grdLocationTypes.ShowImageOnActionButton = true;
            this.grdLocationTypes.Size = new System.Drawing.Size(377, 544);
            this.grdLocationTypes.TabIndex = 110;
            this.grdLocationTypes.Text = "radGridView1";
            // 
            // btnSaveCloseLocationType
            // 
            this.btnSaveCloseLocationType.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnSaveCloseLocationType.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnSaveCloseLocationType.Location = new System.Drawing.Point(438, 271);
            this.btnSaveCloseLocationType.Name = "btnSaveCloseLocationType";
            this.btnSaveCloseLocationType.Size = new System.Drawing.Size(101, 87);
            this.btnSaveCloseLocationType.TabIndex = 112;
            this.btnSaveCloseLocationType.Text = "Save && Close";
            this.btnSaveCloseLocationType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveCloseLocationType.Click += new System.EventHandler(this.btnSaveCloseLocationType_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCloseLocationType.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCloseLocationType.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCloseLocationType.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveCloseLocationType.GetChildAt(0))).Text = "Save && Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveCloseLocationType.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveCloseLocationType.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmLocationTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 587);
            this.Controls.Add(this.btnSaveCloseLocationType);
            this.Controls.Add(this.grdLocationTypes);
            this.FixedExitButtonOnTopRight = true;
            this.FormTitle = "Location Types";
            this.Name = "frmLocationTypes";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.Text = "Location Types";
            this.Load += new System.EventHandler(this.frmLocationTypes_Load);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.grdLocationTypes, 0);
            this.Controls.SetChildIndex(this.btnSaveCloseLocationType, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocationTypes.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocationTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveCloseLocationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MyGridView grdLocationTypes;
        private Telerik.WinControls.UI.RadButton btnSaveCloseLocationType;
    }
}