namespace Taxi_AppMain
{
    partial class frmCompanyInvoiceList
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
            this.grdLister = new UI.MyGridView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnVatCalculator = new Telerik.WinControls.UI.RadButton();
            this.optDefault = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.OPTDESC = new System.Windows.Forms.RadioButton();
            this.OPTASC = new System.Windows.Forms.RadioButton();
            this.btnSageExport = new Telerik.WinControls.UI.RadButton();
            this.btnPrintSelected = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnVatCalculator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSageExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = true;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.EnableHotTracking = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 76);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(1188, 740);
            this.grdLister.TabIndex = 113;
            this.grdLister.Text = "myGridView1";
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.btnVatCalculator);
            this.radPanel1.Controls.Add(this.optDefault);
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Controls.Add(this.OPTDESC);
            this.radPanel1.Controls.Add(this.OPTASC);
            this.radPanel1.Controls.Add(this.btnSageExport);
            this.radPanel1.Controls.Add(this.btnPrintSelected);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1188, 38);
            this.radPanel1.TabIndex = 112;
            // 
            // btnVatCalculator
            // 
            this.btnVatCalculator.Location = new System.Drawing.Point(507, 3);
            this.btnVatCalculator.Name = "btnVatCalculator";
            this.btnVatCalculator.Size = new System.Drawing.Size(161, 32);
            this.btnVatCalculator.TabIndex = 30;
            this.btnVatCalculator.Tag = "";
            this.btnVatCalculator.Text = "VAT Calculator";
            this.btnVatCalculator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVatCalculator.Visible = false;
            this.btnVatCalculator.Click += new System.EventHandler(this.btnVatCalculator_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnVatCalculator.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnVatCalculator.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnVatCalculator.GetChildAt(0))).Text = "VAT Calculator";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnVatCalculator.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnVatCalculator.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // optDefault
            // 
            this.optDefault.AutoSize = true;
            this.optDefault.Checked = true;
            this.optDefault.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDefault.Location = new System.Drawing.Point(221, 10);
            this.optDefault.Name = "optDefault";
            this.optDefault.Size = new System.Drawing.Size(70, 18);
            this.optDefault.TabIndex = 29;
            this.optDefault.TabStop = true;
            this.optDefault.Tag = "0";
            this.optDefault.Text = "Default";
            this.optDefault.UseVisualStyleBackColor = true;
            this.optDefault.CheckedChanged += new System.EventHandler(this.OPTDESC_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(159, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 28;
            this.label1.Text = "Sort by";
            // 
            // OPTDESC
            // 
            this.OPTDESC.AutoSize = true;
            this.OPTDESC.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPTDESC.Location = new System.Drawing.Point(395, 10);
            this.OPTDESC.Name = "OPTDESC";
            this.OPTDESC.Size = new System.Drawing.Size(95, 18);
            this.OPTDESC.TabIndex = 27;
            this.OPTDESC.TabStop = true;
            this.OPTDESC.Tag = "2";
            this.OPTDESC.Text = "Descending";
            this.OPTDESC.UseVisualStyleBackColor = true;
            this.OPTDESC.CheckedChanged += new System.EventHandler(this.OPTDESC_CheckedChanged);
            // 
            // OPTASC
            // 
            this.OPTASC.AutoSize = true;
            this.OPTASC.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OPTASC.Location = new System.Drawing.Point(300, 10);
            this.OPTASC.Name = "OPTASC";
            this.OPTASC.Size = new System.Drawing.Size(88, 18);
            this.OPTASC.TabIndex = 26;
            this.OPTASC.TabStop = true;
            this.OPTASC.Tag = "1";
            this.OPTASC.Text = "Ascending";
            this.OPTASC.UseVisualStyleBackColor = true;
            this.OPTASC.CheckedChanged += new System.EventHandler(this.OPTDESC_CheckedChanged);
            // 
            // btnSageExport
            // 
            this.btnSageExport.Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            this.btnSageExport.Location = new System.Drawing.Point(24, 3);
            this.btnSageExport.Name = "btnSageExport";
            this.btnSageExport.Size = new System.Drawing.Size(114, 29);
            this.btnSageExport.TabIndex = 25;
            this.btnSageExport.Tag = "";
            this.btnSageExport.Text = "Sage Export";
            this.btnSageExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSageExport.Click += new System.EventHandler(this.btnSageExport_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSageExport.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSageExport.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSageExport.GetChildAt(0))).Text = "Sage Export";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSageExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSageExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPrintSelected
            // 
            this.btnPrintSelected.Image = global::Taxi_AppMain.Properties.Resources.Print1;
            this.btnPrintSelected.Location = new System.Drawing.Point(1070, 2);
            this.btnPrintSelected.Name = "btnPrintSelected";
            this.btnPrintSelected.Size = new System.Drawing.Size(114, 29);
            this.btnPrintSelected.TabIndex = 24;
            this.btnPrintSelected.Tag = "";
            this.btnPrintSelected.Text = "Print Invoices";
            this.btnPrintSelected.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrintSelected.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.Print1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrintSelected.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrintSelected.GetChildAt(0))).Text = "Print Invoices";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintSelected.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintSelected.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmCompanyInvoiceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 816);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.radPanel1);
            this.FormTitle = "Account Invoice List";
            this.Name = "frmCompanyInvoiceList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Account Invoice List";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnVatCalculator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSageExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MyGridView grdLister;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnPrintSelected;
        private Telerik.WinControls.UI.RadButton btnSageExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton OPTDESC;
        private System.Windows.Forms.RadioButton OPTASC;
        private System.Windows.Forms.RadioButton optDefault;
        private Telerik.WinControls.UI.RadButton btnVatCalculator;
    }
}