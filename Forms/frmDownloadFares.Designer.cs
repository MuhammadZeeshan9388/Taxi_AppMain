namespace Taxi_AppMain
{
    partial class frmDownloadFares
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
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radPageViewPage1 = new Telerik.WinControls.UI.RadPageViewPage();
            this.grdFixFares = new Telerik.WinControls.UI.RadGridView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnClearAll = new Telerik.WinControls.UI.RadButton();
            this.btnPasteFixFares = new Telerik.WinControls.UI.RadButton();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.btnSaveData = new Telerik.WinControls.UI.RadButton();
            this.btnExitForm = new Telerik.WinControls.UI.RadButton();
            this.RadPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.radPageViewPage2 = new Telerik.WinControls.UI.RadPageViewPage();
            this.grdPlotFares = new Telerik.WinControls.UI.RadGridView();
            this.radPanel4 = new Telerik.WinControls.UI.RadPanel();
            this.btnClearPlotFares = new Telerik.WinControls.UI.RadButton();
            this.btnPastePlotFares = new Telerik.WinControls.UI.RadButton();
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.btnSavePlotFares = new Telerik.WinControls.UI.RadButton();
            this.btnExitPlotFares = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.radPageViewPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFixFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFixFares.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPasteFixFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadPageView1)).BeginInit();
            this.RadPageView1.SuspendLayout();
            this.radPageViewPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPlotFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPlotFares.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).BeginInit();
            this.radPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnClearPlotFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPastePlotFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSavePlotFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitPlotFares)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.MediumBlue;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(0, 38);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(655, 24);
            this.radLabel1.TabIndex = 0;
            this.radLabel1.Text = "Fares Import Utility";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            // 
            // radPageViewPage1
            // 
            this.radPageViewPage1.Controls.Add(this.grdFixFares);
            this.radPageViewPage1.Controls.Add(this.radPanel1);
            this.radPageViewPage1.Controls.Add(this.radPanel2);
            this.radPageViewPage1.Location = new System.Drawing.Point(10, 37);
            this.radPageViewPage1.Name = "radPageViewPage1";
            this.radPageViewPage1.Size = new System.Drawing.Size(634, 513);
            this.radPageViewPage1.Text = "Simple Fixed Fares";
            // 
            // grdFixFares
            // 
            this.grdFixFares.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFixFares.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdFixFares.Location = new System.Drawing.Point(0, 43);
            // 
            // grdFixFares
            // 
            this.grdFixFares.MasterTemplate.AllowAddNewRow = false;
            this.grdFixFares.MasterTemplate.EnableGrouping = false;
            this.grdFixFares.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdFixFares.Name = "grdFixFares";
            this.grdFixFares.ShowGroupPanel = false;
            this.grdFixFares.Size = new System.Drawing.Size(634, 421);
            this.grdFixFares.TabIndex = 0;
            this.grdFixFares.Text = "radGridView1";
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.btnClearAll);
            this.radPanel1.Controls.Add(this.btnPasteFixFares);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(634, 43);
            this.radPanel1.TabIndex = 4;
            // 
            // btnClearAll
            // 
            this.btnClearAll.Image = global::Taxi_AppMain.Properties.Resources.delete;
            this.btnClearAll.Location = new System.Drawing.Point(474, 6);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(118, 31);
            this.btnClearAll.TabIndex = 9;
            this.btnClearAll.Tag = "";
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearAll.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.delete;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearAll.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearAll.GetChildAt(0))).Text = "Clear All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPasteFixFares
            // 
            this.btnPasteFixFares.Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            this.btnPasteFixFares.Location = new System.Drawing.Point(78, 6);
            this.btnPasteFixFares.Name = "btnPasteFixFares";
            this.btnPasteFixFares.Size = new System.Drawing.Size(138, 31);
            this.btnPasteFixFares.TabIndex = 7;
            this.btnPasteFixFares.Tag = "";
            this.btnPasteFixFares.Text = "Paste Fares";
            this.btnPasteFixFares.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPasteFixFares.Click += new System.EventHandler(this.btnPasteFixFares_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteFixFares.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteFixFares.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPasteFixFares.GetChildAt(0))).Text = "Paste Fares";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPasteFixFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPasteFixFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.btnSaveData);
            this.radPanel2.Controls.Add(this.btnExitForm);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 464);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(634, 49);
            this.radPanel2.TabIndex = 3;
            // 
            // btnSaveData
            // 
            this.btnSaveData.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSaveData.Location = new System.Drawing.Point(47, 6);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(138, 39);
            this.btnSaveData.TabIndex = 10;
            this.btnSaveData.Tag = "";
            this.btnSaveData.Text = "Save";
            this.btnSaveData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSaveData.Click += new System.EventHandler(this.btnSave_Click_1);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveData.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveData.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveData.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveData.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveData.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExitForm
            // 
            this.btnExitForm.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitForm.Location = new System.Drawing.Point(454, 6);
            this.btnExitForm.Name = "btnExitForm";
            this.btnExitForm.Size = new System.Drawing.Size(138, 39);
            this.btnExitForm.TabIndex = 9;
            this.btnExitForm.Tag = "";
            this.btnExitForm.Text = "Exit";
            this.btnExitForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitForm.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitForm.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // RadPageView1
            // 
            this.RadPageView1.Controls.Add(this.radPageViewPage1);
            this.RadPageView1.Controls.Add(this.radPageViewPage2);
            this.RadPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadPageView1.Location = new System.Drawing.Point(0, 62);
            this.RadPageView1.Name = "RadPageView1";
            this.RadPageView1.SelectedPage = this.radPageViewPage1;
            this.RadPageView1.Size = new System.Drawing.Size(655, 561);
            this.RadPageView1.TabIndex = 3;
            this.RadPageView1.Text = "radPageView1";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.RadPageView1.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            // 
            // radPageViewPage2
            // 
            this.radPageViewPage2.Controls.Add(this.grdPlotFares);
            this.radPageViewPage2.Controls.Add(this.radPanel4);
            this.radPageViewPage2.Controls.Add(this.radPanel3);
            this.radPageViewPage2.Location = new System.Drawing.Point(10, 37);
            this.radPageViewPage2.Name = "radPageViewPage2";
            this.radPageViewPage2.Size = new System.Drawing.Size(634, 513);
            this.radPageViewPage2.Text = "Plot to Plot Fixed Fares";
            // 
            // grdPlotFares
            // 
            this.grdPlotFares.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPlotFares.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPlotFares.Location = new System.Drawing.Point(0, 43);
            // 
            // 
            // 
            this.grdPlotFares.MasterTemplate.AllowAddNewRow = false;
            this.grdPlotFares.MasterTemplate.EnableGrouping = false;
            this.grdPlotFares.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdPlotFares.Name = "grdPlotFares";
            this.grdPlotFares.ShowGroupPanel = false;
            this.grdPlotFares.Size = new System.Drawing.Size(634, 421);
            this.grdPlotFares.TabIndex = 4;
            this.grdPlotFares.Text = "radGridView1";
            // 
            // radPanel4
            // 
            this.radPanel4.Controls.Add(this.btnClearPlotFares);
            this.radPanel4.Controls.Add(this.btnPastePlotFares);
            this.radPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel4.Location = new System.Drawing.Point(0, 0);
            this.radPanel4.Name = "radPanel4";
            this.radPanel4.Size = new System.Drawing.Size(634, 43);
            this.radPanel4.TabIndex = 6;
            // 
            // btnClearPlotFares
            // 
            this.btnClearPlotFares.Image = global::Taxi_AppMain.Properties.Resources.delete;
            this.btnClearPlotFares.Location = new System.Drawing.Point(474, 6);
            this.btnClearPlotFares.Name = "btnClearPlotFares";
            this.btnClearPlotFares.Size = new System.Drawing.Size(118, 31);
            this.btnClearPlotFares.TabIndex = 9;
            this.btnClearPlotFares.Tag = "";
            this.btnClearPlotFares.Text = "Clear All";
            this.btnClearPlotFares.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClearPlotFares.Click += new System.EventHandler(this.btnClearPlotFares_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearPlotFares.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.delete;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearPlotFares.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnClearPlotFares.GetChildAt(0))).Text = "Clear All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearPlotFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnClearPlotFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPastePlotFares
            // 
            this.btnPastePlotFares.Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            this.btnPastePlotFares.Location = new System.Drawing.Point(78, 6);
            this.btnPastePlotFares.Name = "btnPastePlotFares";
            this.btnPastePlotFares.Size = new System.Drawing.Size(138, 31);
            this.btnPastePlotFares.TabIndex = 7;
            this.btnPastePlotFares.Tag = "";
            this.btnPastePlotFares.Text = "Paste Fares";
            this.btnPastePlotFares.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPastePlotFares.Click += new System.EventHandler(this.btnPastePlotFares_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPastePlotFares.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPastePlotFares.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPastePlotFares.GetChildAt(0))).Text = "Paste Fares";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPastePlotFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPastePlotFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radPanel3
            // 
            this.radPanel3.Controls.Add(this.btnSavePlotFares);
            this.radPanel3.Controls.Add(this.btnExitPlotFares);
            this.radPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel3.Location = new System.Drawing.Point(0, 464);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(634, 49);
            this.radPanel3.TabIndex = 5;
            // 
            // btnSavePlotFares
            // 
            this.btnSavePlotFares.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSavePlotFares.Location = new System.Drawing.Point(47, 6);
            this.btnSavePlotFares.Name = "btnSavePlotFares";
            this.btnSavePlotFares.Size = new System.Drawing.Size(138, 39);
            this.btnSavePlotFares.TabIndex = 10;
            this.btnSavePlotFares.Tag = "";
            this.btnSavePlotFares.Text = "Save";
            this.btnSavePlotFares.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSavePlotFares.Click += new System.EventHandler(this.btnSavePlotFares_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSavePlotFares.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSavePlotFares.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSavePlotFares.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSavePlotFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSavePlotFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExitPlotFares
            // 
            this.btnExitPlotFares.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExitPlotFares.Location = new System.Drawing.Point(454, 6);
            this.btnExitPlotFares.Name = "btnExitPlotFares";
            this.btnExitPlotFares.Size = new System.Drawing.Size(138, 39);
            this.btnExitPlotFares.TabIndex = 9;
            this.btnExitPlotFares.Tag = "";
            this.btnExitPlotFares.Text = "Exit";
            this.btnExitPlotFares.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExitPlotFares.Click += new System.EventHandler(this.btnExitPlotFares_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitPlotFares.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitPlotFares.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExitPlotFares.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitPlotFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExitPlotFares.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // frmDownloadFares
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 623);
            this.Controls.Add(this.RadPageView1);
            this.Controls.Add(this.radLabel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmDownloadFares";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Import Fares";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radLabel1, 0);
            this.Controls.SetChildIndex(this.RadPageView1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.radPageViewPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFixFares.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFixFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClearAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPasteFixFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RadPageView1)).EndInit();
            this.RadPageView1.ResumeLayout(false);
            this.radPageViewPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPlotFares.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPlotFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).EndInit();
            this.radPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnClearPlotFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPastePlotFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            this.radPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSavePlotFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExitPlotFares)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage1;
        private Telerik.WinControls.UI.RadGridView grdFixFares;
        private Telerik.WinControls.UI.RadPageView RadPageView1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnClearAll;
        private Telerik.WinControls.UI.RadButton btnPasteFixFares;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadButton btnSaveData;
        private Telerik.WinControls.UI.RadButton btnExitForm;
        private Telerik.WinControls.UI.RadPageViewPage radPageViewPage2;
        private Telerik.WinControls.UI.RadPanel radPanel4;
        private Telerik.WinControls.UI.RadButton btnClearPlotFares;
        private Telerik.WinControls.UI.RadButton btnPastePlotFares;
        private Telerik.WinControls.UI.RadGridView grdPlotFares;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadButton btnSavePlotFares;
        private Telerik.WinControls.UI.RadButton btnExitPlotFares;
    }
}