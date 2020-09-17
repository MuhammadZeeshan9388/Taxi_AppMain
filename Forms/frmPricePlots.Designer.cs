namespace Taxi_AppMain
{
    partial class frmPricePlots
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
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.numTillprice = new Telerik.WinControls.UI.RadSpinEditor();
            this.numfromprice = new Telerik.WinControls.UI.RadSpinEditor();
            this.lblShort = new Telerik.WinControls.UI.RadLabel();
            this.btnAddPriceplot = new Telerik.WinControls.UI.RadButton();
            this.grdPriceplot = new UI.MyGridView();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTillprice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numfromprice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPriceplot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPriceplot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPriceplot.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(508, 265);
            this.btnOnNew.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(363, -1);
            this.btnExit.Size = new System.Drawing.Size(77, 39);
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(690, 265);
            this.btnSaveAndClose.TabIndex = 4;
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(595, 265);
            this.btnSaveAndNew.TabIndex = 3;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.grdPriceplot);
            this.radPanel1.Controls.Add(this.btnClear);
            this.radPanel1.Controls.Add(this.numTillprice);
            this.radPanel1.Controls.Add(this.numfromprice);
            this.radPanel1.Controls.Add(this.lblShort);
            this.radPanel1.Controls.Add(this.btnAddPriceplot);
            this.radPanel1.Controls.Add(this.radLabel1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(432, 395);
            this.radPanel1.TabIndex = 106;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(320, 53);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(69, 22);
            this.btnClear.TabIndex = 144;
            this.btnClear.Text = "New";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClear.GetChildAt(0))).Text = "New";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClear.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClear.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numTillprice
            // 
            this.numTillprice.EnableKeyMap = true;
            this.numTillprice.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numTillprice.InterceptArrowKeys = false;
            this.numTillprice.Location = new System.Drawing.Point(121, 51);
            this.numTillprice.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numTillprice.Name = "numTillprice";
            // 
            // 
            // 
            this.numTillprice.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numTillprice.ShowBorder = true;
            this.numTillprice.ShowUpDownButtons = false;
            this.numTillprice.Size = new System.Drawing.Size(94, 24);
            this.numTillprice.TabIndex = 143;
            this.numTillprice.TabStop = false;
            ((Telerik.WinControls.UI.RadSpinElement)(this.numTillprice.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numTillprice.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTillprice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numTillprice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // numfromprice
            // 
            this.numfromprice.EnableKeyMap = true;
            this.numfromprice.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.numfromprice.InterceptArrowKeys = false;
            this.numfromprice.Location = new System.Drawing.Point(121, 15);
            this.numfromprice.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numfromprice.Name = "numfromprice";
            // 
            // 
            // 
            this.numfromprice.RootElement.AutoSizeMode = Telerik.WinControls.RadAutoSizeMode.WrapAroundChildren;
            this.numfromprice.ShowBorder = true;
            this.numfromprice.ShowUpDownButtons = false;
            this.numfromprice.Size = new System.Drawing.Size(94, 24);
            this.numfromprice.TabIndex = 142;
            this.numfromprice.TabStop = false;
            this.numfromprice.Leave += new System.EventHandler(this.numfromprice_Leave);
            ((Telerik.WinControls.UI.RadSpinElement)(this.numfromprice.GetChildAt(0))).Padding = new System.Windows.Forms.Padding(0, 2, 0, 1);
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.numfromprice.GetChildAt(0).GetChildAt(0))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numfromprice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Text = "0";
            ((Telerik.WinControls.UI.RadTextBoxItem)(this.numfromprice.GetChildAt(0).GetChildAt(2).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblShort
            // 
            this.lblShort.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShort.Location = new System.Drawing.Point(8, 53);
            this.lblShort.Name = "lblShort";
            this.lblShort.Size = new System.Drawing.Size(74, 22);
            this.lblShort.TabIndex = 30;
            this.lblShort.Text = "Till Price";
            // 
            // btnAddPriceplot
            // 
            this.btnAddPriceplot.Location = new System.Drawing.Point(235, 53);
            this.btnAddPriceplot.Name = "btnAddPriceplot";
            this.btnAddPriceplot.Size = new System.Drawing.Size(65, 22);
            this.btnAddPriceplot.TabIndex = 21;
            this.btnAddPriceplot.Text = "Add";
            this.btnAddPriceplot.Click += new System.EventHandler(this.btnAddPriceplot_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddPriceplot.GetChildAt(0))).Text = "Add";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddPriceplot.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddPriceplot.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdPriceplot
            // 
            this.grdPriceplot.AutoCellFormatting = false;
            this.grdPriceplot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdPriceplot.EnableCheckInCheckOut = false;
            this.grdPriceplot.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdPriceplot.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdPriceplot.Location = new System.Drawing.Point(0, 85);
            this.grdPriceplot.Name = "grdPriceplot";
            this.grdPriceplot.PKFieldColumnName = "";
            this.grdPriceplot.ShowImageOnActionButton = true;
            this.grdPriceplot.Size = new System.Drawing.Size(432, 310);
            this.grdPriceplot.TabIndex = 18;
            this.grdPriceplot.Text = "myGridView1";
            this.grdPriceplot.CommandCellClick += new Telerik.WinControls.UI.CommandCellClickEventHandler(this.grdPriceplot_CommandCellClick);
            this.grdPriceplot.DoubleClick += new System.EventHandler(this.grdPostCodes_DoubleClick);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(8, 15);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(91, 22);
            this.radLabel1.TabIndex = 17;
            this.radLabel1.Text = "From Price";
            // 
            // frmPricePlots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 433);
            this.ControlBox = true;
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.FormTitle = "Price Plot";
            this.Name = "frmPricePlots";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.Text = "Price Plot";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmZones_FormClosed);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTillprice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numfromprice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblShort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPriceplot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPriceplot.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPriceplot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private UI.MyGridView grdPriceplot;
        private Telerik.WinControls.UI.RadButton btnAddPriceplot;
        private Telerik.WinControls.UI.RadLabel lblShort;
        private Telerik.WinControls.UI.RadSpinEditor numfromprice;
        private Telerik.WinControls.UI.RadSpinEditor numTillprice;
        private Telerik.WinControls.UI.RadButton btnClear;
    }
}