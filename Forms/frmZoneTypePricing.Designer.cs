namespace Taxi_AppMain
{
    partial class frmZoneTypePricing
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
            this.ddlFromZone = new UI.MyDropDownList();
            this.ddlToZone = new UI.MyDropDownList();
            this.grdZone = new UI.MyGridView();
            this.btnAddPostCode = new Telerik.WinControls.UI.RadButton();
            this.lblPostCode = new Telerik.WinControls.UI.RadLabel();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.spnPrice = new Telerik.WinControls.UI.RadSpinEditor();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZone.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlFromZone
            // 
            this.ddlFromZone.Caption = null;
            this.ddlFromZone.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlFromZone.Location = new System.Drawing.Point(208, 55);
            this.ddlFromZone.Name = "ddlFromZone";
            this.ddlFromZone.Property = null;
            this.ddlFromZone.ShowDownArrow = true;
            this.ddlFromZone.Size = new System.Drawing.Size(231, 26);
            this.ddlFromZone.TabIndex = 0;
            // 
            // ddlToZone
            // 
            this.ddlToZone.Caption = null;
            this.ddlToZone.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlToZone.Location = new System.Drawing.Point(208, 96);
            this.ddlToZone.Name = "ddlToZone";
            this.ddlToZone.Property = null;
            this.ddlToZone.ShowDownArrow = true;
            this.ddlToZone.Size = new System.Drawing.Size(231, 26);
            this.ddlToZone.TabIndex = 1;
            // 
            // grdZone
            // 
            this.grdZone.AutoCellFormatting = false;
            this.grdZone.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdZone.EnableCheckInCheckOut = false;
            this.grdZone.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdZone.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdZone.Location = new System.Drawing.Point(0, 227);
            // 
            // grdZone
            // 
            this.grdZone.MasterTemplate.AllowColumnHeaderContextMenu = false;
            this.grdZone.Name = "grdZone";
            this.grdZone.PKFieldColumnName = "";
            this.grdZone.ShowImageOnActionButton = true;
            this.grdZone.Size = new System.Drawing.Size(703, 364);
            this.grdZone.TabIndex = 5;
            this.grdZone.Text = "myGridView1";
            // 
            // btnAddPostCode
            // 
            this.btnAddPostCode.Location = new System.Drawing.Point(208, 177);
            this.btnAddPostCode.Name = "btnAddPostCode";
            this.btnAddPostCode.Size = new System.Drawing.Size(69, 33);
            this.btnAddPostCode.TabIndex = 3;
            this.btnAddPostCode.Text = "Add";
            this.btnAddPostCode.Click += new System.EventHandler(this.btnAddPostCode_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddPostCode.GetChildAt(0))).Text = "Add";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddPostCode.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddPostCode.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblPostCode
            // 
            this.lblPostCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostCode.Location = new System.Drawing.Point(63, 62);
            this.lblPostCode.Name = "lblPostCode";
            this.lblPostCode.Size = new System.Drawing.Size(108, 19);
            this.lblPostCode.TabIndex = 115;
            this.lblPostCode.Text = "From Plot Type";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(313, 177);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 33);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClear.GetChildAt(0))).Text = "Clear";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClear.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClear.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(63, 100);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(90, 19);
            this.radLabel1.TabIndex = 118;
            this.radLabel1.Text = "To Plot Type";
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(63, 140);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(40, 19);
            this.radLabel2.TabIndex = 119;
            this.radLabel2.Text = "Price";
            // 
            // spnPrice
            // 
            this.spnPrice.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spnPrice.Location = new System.Drawing.Point(208, 139);
            this.spnPrice.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.spnPrice.Name = "spnPrice";
            // 
            // 
            // 
            this.spnPrice.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.spnPrice.ShowBorder = true;
            this.spnPrice.Size = new System.Drawing.Size(132, 21);
            this.spnPrice.TabIndex = 2;
            this.spnPrice.TabStop = false;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.spnPrice.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.spnPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.spnPrice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmZoneTypePricing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 591);
            this.Controls.Add(this.grdZone);
            this.Controls.Add(this.spnPrice);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddPostCode);
            this.Controls.Add(this.lblPostCode);
            this.Controls.Add(this.ddlToZone);
            this.Controls.Add(this.ddlFromZone);
            this.FormTitle = "Plot Type Pricing";
            this.Name = "frmZoneTypePricing";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Plot Type Pricing";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.ddlFromZone, 0);
            this.Controls.SetChildIndex(this.ddlToZone, 0);
            this.Controls.SetChildIndex(this.lblPostCode, 0);
            this.Controls.SetChildIndex(this.btnAddPostCode, 0);
            this.Controls.SetChildIndex(this.btnClear, 0);
            this.Controls.SetChildIndex(this.radLabel1, 0);
            this.Controls.SetChildIndex(this.radLabel2, 0);
            this.Controls.SetChildIndex(this.spnPrice, 0);
            this.Controls.SetChildIndex(this.grdZone, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlFromZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlToZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZone.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.MyDropDownList ddlFromZone;
        private UI.MyDropDownList ddlToZone;
        private UI.MyGridView grdZone;
        private Telerik.WinControls.UI.RadButton btnAddPostCode;
        private Telerik.WinControls.UI.RadLabel lblPostCode;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadSpinEditor spnPrice;
    }
}