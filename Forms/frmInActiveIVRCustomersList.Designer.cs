namespace Taxi_AppMain
{
    partial class frmInActiveIVRCustomersList
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
            this.radProgressBar1 = new Telerik.WinControls.UI.RadProgressBar();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnShowAllCustomers = new Telerik.WinControls.UI.RadButton();
            this.btnFind = new Telerik.WinControls.UI.RadButton();
            this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
            this.ddlColumns = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.grdLister = new UI.MyGridView();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.btnLastRecords = new Telerik.WinControls.UI.RadButton();
            this.btnNextRecord = new Telerik.WinControls.UI.RadButton();
            this.btnPreviousRecords = new Telerik.WinControls.UI.RadButton();
            this.btnFirstRecords = new Telerik.WinControls.UI.RadButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowAllCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            this.grdLister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnLastRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNextRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreviousRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFirstRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.radProgressBar1);
            this.radPanel1.Controls.Add(this.btnExport);
            this.radPanel1.Controls.Add(this.lblTotal);
            this.radPanel1.Controls.Add(this.btnShowAllCustomers);
            this.radPanel1.Controls.Add(this.btnFind);
            this.radPanel1.Controls.Add(this.txtSearch);
            this.radPanel1.Controls.Add(this.ddlColumns);
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1010, 34);
            this.radPanel1.TabIndex = 106;
            // 
            // radProgressBar1
            // 
            this.radProgressBar1.Dash = false;
            this.radProgressBar1.Location = new System.Drawing.Point(659, 4);
            this.radProgressBar1.Name = "radProgressBar1";
            this.radProgressBar1.Size = new System.Drawing.Size(150, 26);
            this.radProgressBar1.TabIndex = 1;
            this.radProgressBar1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExport
            // 
            this.btnExport.Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            this.btnExport.Location = new System.Drawing.Point(516, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(138, 24);
            this.btnExport.TabIndex = 6;
            this.btnExport.Tag = "";
            this.btnExport.Text = "Export To Excel";
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.excel__1_;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExport.GetChildAt(0))).Text = "Export To Excel";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExport.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotal.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(776, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(234, 34);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total Customer(s) : 5";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnShowAllCustomers
            // 
            this.btnShowAllCustomers.Image = global::Taxi_AppMain.Properties.Resources.select;
            this.btnShowAllCustomers.Location = new System.Drawing.Point(373, 5);
            this.btnShowAllCustomers.Name = "btnShowAllCustomers";
            this.btnShowAllCustomers.Size = new System.Drawing.Size(122, 24);
            this.btnShowAllCustomers.TabIndex = 4;
            this.btnShowAllCustomers.Tag = "";
            this.btnShowAllCustomers.Text = "Show All Customers";
            this.btnShowAllCustomers.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnShowAllCustomers.Click += new System.EventHandler(this.btnShowAllCustomers_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAllCustomers.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.select;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAllCustomers.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAllCustomers.GetChildAt(0))).Text = "Show All Customers";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowAllCustomers.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowAllCustomers.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnFind
            // 
            this.btnFind.Image = global::Taxi_AppMain.Properties.Resources.pic_Search;
            this.btnFind.Location = new System.Drawing.Point(308, 5);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(59, 24);
            this.btnFind.TabIndex = 3;
            this.btnFind.Tag = "";
            this.btnFind.Text = "Find";
            this.btnFind.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFind.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.pic_Search;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFind.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFind.GetChildAt(0))).Text = "Find";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnFind.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnFind.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(69, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(131, 21);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TabStop = false;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // ddlColumns
            // 
            this.ddlColumns.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlColumns.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlColumns.Location = new System.Drawing.Point(207, 6);
            this.ddlColumns.Name = "ddlColumns";
            this.ddlColumns.Size = new System.Drawing.Size(93, 23);
            this.ddlColumns.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(8, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search";
            // 
            // grdLister
            // 
            this.grdLister.AutoCellFormatting = true;
            this.grdLister.Controls.Add(this.radGridView1);
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.EnableCheckInCheckOut = false;
            this.grdLister.EnableHotTracking = false;
            this.grdLister.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLister.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLister.Location = new System.Drawing.Point(0, 72);
            this.grdLister.Name = "grdLister";
            this.grdLister.PKFieldColumnName = "";
            this.grdLister.ShowImageOnActionButton = true;
            this.grdLister.Size = new System.Drawing.Size(1010, 621);
            this.grdLister.TabIndex = 107;
            this.grdLister.Text = "myGridView1";
            this.grdLister.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdLister_MouseDown);
            // 
            // radGridView1
            // 
            this.radGridView1.Location = new System.Drawing.Point(570, 38);
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(240, 150);
            this.radGridView1.TabIndex = 0;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.Visible = false;
            // 
            // radPanel2
            // 
            this.radPanel2.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel2.Controls.Add(this.btnLastRecords);
            this.radPanel2.Controls.Add(this.btnNextRecord);
            this.radPanel2.Controls.Add(this.btnPreviousRecords);
            this.radPanel2.Controls.Add(this.btnFirstRecords);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 693);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(1010, 47);
            this.radPanel2.TabIndex = 112;
            // 
            // btnLastRecords
            // 
            this.btnLastRecords.Image = global::Taxi_AppMain.Properties.Resources.last_resultset1;
            this.btnLastRecords.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLastRecords.Location = new System.Drawing.Point(668, 4);
            this.btnLastRecords.Name = "btnLastRecords";
            this.btnLastRecords.Size = new System.Drawing.Size(110, 40);
            this.btnLastRecords.TabIndex = 3;
            this.btnLastRecords.Text = "Move Last";
            this.btnLastRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLastRecords.Click += new System.EventHandler(this.btnLastRecords_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnLastRecords.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.last_resultset1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnLastRecords.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnLastRecords.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnLastRecords.GetChildAt(0))).Text = "Move Last";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnLastRecords.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnLastRecords.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnNextRecord
            // 
            this.btnNextRecord.Image = global::Taxi_AppMain.Properties.Resources.resultset_next1;
            this.btnNextRecord.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnNextRecord.Location = new System.Drawing.Point(522, 4);
            this.btnNextRecord.Name = "btnNextRecord";
            this.btnNextRecord.Size = new System.Drawing.Size(110, 40);
            this.btnNextRecord.TabIndex = 2;
            this.btnNextRecord.Text = "Move Next";
            this.btnNextRecord.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNextRecord.Click += new System.EventHandler(this.btnNextRecord_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNextRecord.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.resultset_next1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNextRecord.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNextRecord.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNextRecord.GetChildAt(0))).Text = "Move Next";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNextRecord.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNextRecord.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPreviousRecords
            // 
            this.btnPreviousRecords.Image = global::Taxi_AppMain.Properties.Resources.previous_resultset1;
            this.btnPreviousRecords.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnPreviousRecords.Location = new System.Drawing.Point(376, 4);
            this.btnPreviousRecords.Name = "btnPreviousRecords";
            this.btnPreviousRecords.Size = new System.Drawing.Size(110, 40);
            this.btnPreviousRecords.TabIndex = 1;
            this.btnPreviousRecords.Text = "Move Previous";
            this.btnPreviousRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPreviousRecords.Click += new System.EventHandler(this.btnPreviousRecords_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPreviousRecords.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.previous_resultset1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPreviousRecords.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPreviousRecords.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPreviousRecords.GetChildAt(0))).Text = "Move Previous";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPreviousRecords.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPreviousRecords.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnFirstRecords
            // 
            this.btnFirstRecords.Image = global::Taxi_AppMain.Properties.Resources.first_resultset1;
            this.btnFirstRecords.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnFirstRecords.Location = new System.Drawing.Point(228, 4);
            this.btnFirstRecords.Name = "btnFirstRecords";
            this.btnFirstRecords.Size = new System.Drawing.Size(110, 40);
            this.btnFirstRecords.TabIndex = 0;
            this.btnFirstRecords.Text = "Move First";
            this.btnFirstRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnFirstRecords.Click += new System.EventHandler(this.btnFirstRecords_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFirstRecords.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.first_resultset1;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFirstRecords.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFirstRecords.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnFirstRecords.GetChildAt(0))).Text = "Move First";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnFirstRecords.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnFirstRecords.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Excel Files (.xls)|*.xls|Advanced Excel Files (.xlsx)|*.xlsx";
            this.saveFileDialog1.FilterIndex = 0;
            // 
            // frmInActiveIVRCustomersList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 740);
            this.ControlBox = true;
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "InActive IVR Customers";
            this.Name = "frmInActiveIVRCustomersList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "INACTIVE IVR Customers";
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.radPanel2, 0);
            this.Controls.SetChildIndex(this.grdLister, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowAllCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            this.grdLister.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnLastRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNextRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreviousRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFirstRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private UI.MyGridView grdLister;
        private Telerik.WinControls.UI.RadTextBox txtSearch;
        private Telerik.WinControls.UI.RadDropDownList ddlColumns;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton btnFind;
        private Telerik.WinControls.UI.RadButton btnShowAllCustomers;
        private System.Windows.Forms.Label lblTotal;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadButton btnLastRecords;
        private Telerik.WinControls.UI.RadButton btnNextRecord;
        private Telerik.WinControls.UI.RadButton btnPreviousRecords;
        private Telerik.WinControls.UI.RadButton btnFirstRecords;
        private Telerik.WinControls.UI.RadButton btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Telerik.WinControls.UI.RadProgressBar radProgressBar1;
        private Telerik.WinControls.UI.RadGridView radGridView1;
    }
}