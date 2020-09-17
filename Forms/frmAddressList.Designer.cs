namespace Taxi_AppMain
{
    partial class frmAddressList
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();


            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnAddNewAddress = new Telerik.WinControls.UI.RadButton();
            this.btnShowAll = new Telerik.WinControls.UI.RadButton();
            this.btnFind = new Telerik.WinControls.UI.RadButton();
            this.txtSearch = new Telerik.WinControls.UI.RadTextBox();
            this.ddlColumns = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.grdLister = new Telerik.WinControls.UI.RadGridView();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.btnLastRecords = new Telerik.WinControls.UI.RadButton();
            this.btnNextRecord = new Telerik.WinControls.UI.RadButton();
            this.btnPreviousRecords = new Telerik.WinControls.UI.RadButton();
            this.btnFirstRecords = new Telerik.WinControls.UI.RadButton();
            this.lblLoading = new Telerik.WinControls.UI.RadLabel();
            this.btnCloseError = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFind)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnLastRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNextRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreviousRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFirstRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLoading)).BeginInit();
            this.lblLoading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.lblLoading);
            this.radPanel1.Controls.Add(this.btnAddNewAddress);
            this.radPanel1.Controls.Add(this.btnShowAll);
            this.radPanel1.Controls.Add(this.btnFind);
            this.radPanel1.Controls.Add(this.txtSearch);
            this.radPanel1.Controls.Add(this.ddlColumns);
            this.radPanel1.Controls.Add(this.label1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(1050, 34);
            this.radPanel1.TabIndex = 109;
            // 
            // btnAddNewAddress
            // 
            this.btnAddNewAddress.Location = new System.Drawing.Point(866, 5);
            this.btnAddNewAddress.Name = "btnAddNewAddress";
            this.btnAddNewAddress.Size = new System.Drawing.Size(129, 24);
            this.btnAddNewAddress.TabIndex = 16;
            this.btnAddNewAddress.Tag = "";
            this.btnAddNewAddress.Text = "Add New Address";
            this.btnAddNewAddress.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddNewAddress.Click += new System.EventHandler(this.btnAddNewAddress_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewAddress.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewAddress.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnAddNewAddress.GetChildAt(0))).Text = "Add New Address";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddNewAddress.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnAddNewAddress.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnShowAll
            // 
            this.btnShowAll.Location = new System.Drawing.Point(632, 5);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(77, 24);
            this.btnShowAll.TabIndex = 15;
            this.btnShowAll.Tag = "";
            this.btnShowAll.Text = "Show All ";
            this.btnShowAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAll.GetChildAt(0))).Image = null;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAll.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnShowAll.GetChildAt(0))).Text = "Show All ";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnShowAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnFind
            // 
            this.btnFind.Image = global::Taxi_AppMain.Properties.Resources.pic_Search;
            this.btnFind.Location = new System.Drawing.Point(532, 5);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(59, 24);
            this.btnFind.TabIndex = 7;
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
            this.txtSearch.Location = new System.Drawing.Point(203, 7);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(133, 21);
            this.txtSearch.TabIndex = 5;
            this.txtSearch.TabStop = false;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // ddlColumns
            // 
            this.ddlColumns.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ddlColumns.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            radListDataItem3.Selected = true;
            radListDataItem3.Text = "Address";
            radListDataItem3.TextWrap = true;
            radListDataItem4.Text = "Post Code";
            radListDataItem4.TextWrap = true;

            radListDataItem5.Text = "Zone";
            radListDataItem5.TextWrap = true;


            this.ddlColumns.Items.Add(radListDataItem3);
            this.ddlColumns.Items.Add(radListDataItem4);
            this.ddlColumns.Items.Add(radListDataItem5);
            this.ddlColumns.Location = new System.Drawing.Point(378, 6);
            this.ddlColumns.Name = "ddlColumns";
            this.ddlColumns.Size = new System.Drawing.Size(111, 23);
            this.ddlColumns.TabIndex = 6;
            this.ddlColumns.Text = "Address";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(113, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search";
            // 
            // grdLister
            // 
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLister.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLister.Location = new System.Drawing.Point(0, 72);
            // 
            // grdLister
            // 
            this.grdLister.MasterTemplate.AllowAddNewRow = false;
            this.grdLister.MasterTemplate.EnableFiltering = true;
            this.grdLister.Name = "grdLister";
            this.grdLister.ShowGroupPanel = false;
            this.grdLister.Size = new System.Drawing.Size(1050, 861);
            this.grdLister.TabIndex = 110;
            this.grdLister.Text = "myGridView1";
            // 
            // radPanel2
            // 
            this.radPanel2.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel2.Controls.Add(this.btnLastRecords);
            this.radPanel2.Controls.Add(this.btnNextRecord);
            this.radPanel2.Controls.Add(this.btnPreviousRecords);
            this.radPanel2.Controls.Add(this.btnFirstRecords);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 933);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(1050, 47);
            this.radPanel2.TabIndex = 114;
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
            // lblLoading
            // 
            this.lblLoading.AutoSize = false;
            this.lblLoading.Controls.Add(this.btnCloseError);
            this.lblLoading.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLoading.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoading.ForeColor = System.Drawing.Color.Blue;
            this.lblLoading.Image = global::Taxi_AppMain.Properties.Resources.pleasewait2;
            this.lblLoading.Location = new System.Drawing.Point(0, 0);
            this.lblLoading.Name = "lblLoading";
            // 
            // 
            // 
            this.lblLoading.RootElement.ForeColor = System.Drawing.Color.Blue;
            this.lblLoading.Size = new System.Drawing.Size(1050, 31);
            this.lblLoading.TabIndex = 164;
            this.lblLoading.Text = "Loading Data Please Wait";
            this.lblLoading.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLoading.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // btnCloseError
            // 
            this.btnCloseError.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseError.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseError.Image = global::Taxi_AppMain.Properties.Resources.remove;
            this.btnCloseError.Location = new System.Drawing.Point(1015, 2);
            this.btnCloseError.Name = "btnCloseError";
            this.btnCloseError.Size = new System.Drawing.Size(30, 28);
            this.btnCloseError.TabIndex = 0;
            this.btnCloseError.UseVisualStyleBackColor = true;
            this.btnCloseError.Visible = false;
            // 
            // frmAddressList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 980);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radPanel2);
            this.FormTitle = "Address List";
            this.Name = "frmAddressList";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Address List";
            this.Load += new System.EventHandler(this.frmAddressList_Load);
            this.Controls.SetChildIndex(this.radPanel2, 0);
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
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.radPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddNewAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFind)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnLastRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNextRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPreviousRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFirstRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLoading)).EndInit();
            this.lblLoading.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnShowAll;
        private Telerik.WinControls.UI.RadButton btnFind;
        private Telerik.WinControls.UI.RadTextBox txtSearch;
        private Telerik.WinControls.UI.RadDropDownList ddlColumns;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadGridView grdLister;
        private Telerik.WinControls.UI.RadButton btnAddNewAddress;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadButton btnLastRecords;
        private Telerik.WinControls.UI.RadButton btnNextRecord;
        private Telerik.WinControls.UI.RadButton btnPreviousRecords;
        private Telerik.WinControls.UI.RadButton btnFirstRecords;
        private Telerik.WinControls.UI.RadLabel lblLoading;
        private System.Windows.Forms.Button btnCloseError;
    }
}