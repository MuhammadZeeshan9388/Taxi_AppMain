namespace Taxi_AppMain
{
    partial class frmManageLocations
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AddLocations = new System.Windows.Forms.TabPage();
            this.grdPostCodes = new UI.MyGridView();
            this.chkIncludeAll = new System.Windows.Forms.CheckBox();
            this.chkExcludeAll = new System.Windows.Forms.CheckBox();
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.btnGetAll = new Telerik.WinControls.UI.RadButton();
            this.lblPostCode = new Telerik.WinControls.UI.RadLabel();
            this.ddlLocations = new UI.MyDropDownList();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnExit1 = new Telerik.WinControls.UI.RadButton();
            this.btnSaveClose = new Telerik.WinControls.UI.RadButton();
            this.DeleteLocations = new System.Windows.Forms.TabPage();
            this.btnExit2 = new Telerik.WinControls.UI.RadButton();
            this.btnDelete = new Telerik.WinControls.UI.RadButton();
            this.grdDeleteLocations = new UI.MyGridView();
            this.chkAllPostCodes = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.grdLocations = new UI.MyGridView();
            this.chkAllLocations = new System.Windows.Forms.CheckBox();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlLocationTypes2 = new UI.MyDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.AddLocations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPostCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPostCodes.MasterTemplate)).BeginInit();
            this.grdPostCodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGetAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPostCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLocations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveClose)).BeginInit();
            this.DeleteLocations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDeleteLocations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDeleteLocations.MasterTemplate)).BeginInit();
            this.grdDeleteLocations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocations.MasterTemplate)).BeginInit();
            this.grdLocations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLocationTypes2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(508, 117);
            this.btnOnNew.TabIndex = 5;
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.Location = new System.Drawing.Point(505, 168);
            this.btnSaveAndClose.TabIndex = 4;
            // 
            // btnSaveAndNew
            // 
            this.btnSaveAndNew.Location = new System.Drawing.Point(595, 110);
            this.btnSaveAndNew.TabIndex = 3;
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(233)))), ((int)(((byte)(240)))), ((int)(((byte)(249)))));
            this.radPanel1.Controls.Add(this.tabControl1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 38);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(842, 567);
            this.radPanel1.TabIndex = 106;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AddLocations);
            this.tabControl1.Controls.Add(this.DeleteLocations);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(842, 567);
            this.tabControl1.TabIndex = 30;
            // 
            // AddLocations
            // 
            this.AddLocations.Controls.Add(this.grdPostCodes);
            this.AddLocations.Controls.Add(this.radPanel3);
            this.AddLocations.Controls.Add(this.btnSave);
            this.AddLocations.Controls.Add(this.btnExit1);
            this.AddLocations.Controls.Add(this.btnSaveClose);
            this.AddLocations.Location = new System.Drawing.Point(4, 22);
            this.AddLocations.Name = "AddLocations";
            this.AddLocations.Padding = new System.Windows.Forms.Padding(3);
            this.AddLocations.Size = new System.Drawing.Size(834, 417);
            this.AddLocations.TabIndex = 0;
            this.AddLocations.Text = "Add Locations";
            this.AddLocations.UseVisualStyleBackColor = true;
            // 
            // grdPostCodes
            // 
            this.grdPostCodes.AutoCellFormatting = false;
            this.grdPostCodes.Controls.Add(this.chkIncludeAll);
            this.grdPostCodes.Controls.Add(this.chkExcludeAll);
            this.grdPostCodes.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdPostCodes.EnableCheckInCheckOut = false;
            this.grdPostCodes.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdPostCodes.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdPostCodes.Location = new System.Drawing.Point(3, 35);
            this.grdPostCodes.Name = "grdPostCodes";
            this.grdPostCodes.PKFieldColumnName = "";
            this.grdPostCodes.ShowImageOnActionButton = true;
            this.grdPostCodes.Size = new System.Drawing.Size(533, 379);
            this.grdPostCodes.TabIndex = 30;
            this.grdPostCodes.Text = "myGridView1";
            // 
            // chkIncludeAll
            // 
            this.chkIncludeAll.AutoSize = true;
            this.chkIncludeAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncludeAll.Location = new System.Drawing.Point(5, 3);
            this.chkIncludeAll.Name = "chkIncludeAll";
            this.chkIncludeAll.Size = new System.Drawing.Size(41, 20);
            this.chkIncludeAll.TabIndex = 3;
            this.chkIncludeAll.Text = "All";
            this.chkIncludeAll.UseVisualStyleBackColor = true;
            // 
            // chkExcludeAll
            // 
            this.chkExcludeAll.AutoSize = true;
            this.chkExcludeAll.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExcludeAll.Location = new System.Drawing.Point(471, 3);
            this.chkExcludeAll.Name = "chkExcludeAll";
            this.chkExcludeAll.Size = new System.Drawing.Size(46, 20);
            this.chkExcludeAll.TabIndex = 2;
            this.chkExcludeAll.Text = "Exc";
            this.chkExcludeAll.UseVisualStyleBackColor = true;
            // 
            // radPanel3
            // 
            this.radPanel3.Controls.Add(this.btnGetAll);
            this.radPanel3.Controls.Add(this.lblPostCode);
            this.radPanel3.Controls.Add(this.ddlLocations);
            this.radPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel3.Location = new System.Drawing.Point(3, 3);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(828, 32);
            this.radPanel3.TabIndex = 31;
            // 
            // btnGetAll
            // 
            this.btnGetAll.Location = new System.Drawing.Point(368, 5);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(69, 22);
            this.btnGetAll.TabIndex = 26;
            this.btnGetAll.Text = "Get All";
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnGetAll.GetChildAt(0))).Text = "Get All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnGetAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnGetAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // lblPostCode
            // 
            this.lblPostCode.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostCode.Location = new System.Drawing.Point(6, 6);
            this.lblPostCode.Name = "lblPostCode";
            this.lblPostCode.Size = new System.Drawing.Size(107, 19);
            this.lblPostCode.TabIndex = 19;
            this.lblPostCode.Text = "Location Types";
            // 
            // ddlLocations
            // 
            this.ddlLocations.Caption = null;
            this.ddlLocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlLocations.Location = new System.Drawing.Point(119, 4);
            this.ddlLocations.Name = "ddlLocations";
            this.ddlLocations.Property = null;
            this.ddlLocations.ShowDownArrow = true;
            this.ddlLocations.Size = new System.Drawing.Size(229, 24);
            this.ddlLocations.TabIndex = 25;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnSave.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSave.Location = new System.Drawing.Point(546, 411);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(79, 56);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSave.GetChildAt(0))).Text = "Save";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSave.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnExit1
            // 
            this.btnExit1.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit1.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit1.Location = new System.Drawing.Point(760, 410);
            this.btnExit1.Name = "btnExit1";
            this.btnExit1.Size = new System.Drawing.Size(79, 56);
            this.btnExit1.TabIndex = 22;
            this.btnExit1.Text = "Exit";
            this.btnExit1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit1.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit1.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnSaveClose.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnSaveClose.Location = new System.Drawing.Point(653, 411);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(79, 56);
            this.btnSaveClose.TabIndex = 23;
            this.btnSaveClose.Text = "Save && Close";
            this.btnSaveClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveClose.GetChildAt(0))).Text = "Save && Close";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveClose.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // DeleteLocations
            // 
            this.DeleteLocations.Controls.Add(this.btnExit2);
            this.DeleteLocations.Controls.Add(this.btnDelete);
            this.DeleteLocations.Controls.Add(this.grdDeleteLocations);
            this.DeleteLocations.Controls.Add(this.grdLocations);
            this.DeleteLocations.Controls.Add(this.radPanel2);
            this.DeleteLocations.Location = new System.Drawing.Point(4, 22);
            this.DeleteLocations.Name = "DeleteLocations";
            this.DeleteLocations.Padding = new System.Windows.Forms.Padding(3);
            this.DeleteLocations.Size = new System.Drawing.Size(834, 541);
            this.DeleteLocations.TabIndex = 1;
            this.DeleteLocations.Text = "Delete Locations";
            this.DeleteLocations.UseVisualStyleBackColor = true;
            // 
            // btnExit2
            // 
            this.btnExit2.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit2.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnExit2.Location = new System.Drawing.Point(724, 404);
            this.btnExit2.Name = "btnExit2";
            this.btnExit2.Size = new System.Drawing.Size(79, 56);
            this.btnExit2.TabIndex = 34;
            this.btnExit2.Text = "Exit";
            this.btnExit2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit2.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit2.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit2.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit2.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit2.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit2.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnDelete.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.Location = new System.Drawing.Point(617, 405);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(79, 56);
            this.btnDelete.TabIndex = 35;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDelete.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDelete.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDelete.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnDelete.GetChildAt(0))).Text = "Delete";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDelete.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnDelete.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // grdDeleteLocations
            // 
            this.grdDeleteLocations.AutoCellFormatting = false;
            this.grdDeleteLocations.Controls.Add(this.chkAllPostCodes);
            this.grdDeleteLocations.Controls.Add(this.checkBox2);
            this.grdDeleteLocations.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdDeleteLocations.EnableCheckInCheckOut = false;
            this.grdDeleteLocations.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDeleteLocations.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdDeleteLocations.Location = new System.Drawing.Point(536, 35);
            this.grdDeleteLocations.Name = "grdDeleteLocations";
            this.grdDeleteLocations.PKFieldColumnName = "";
            this.grdDeleteLocations.ShowImageOnActionButton = true;
            this.grdDeleteLocations.Size = new System.Drawing.Size(295, 254);
            this.grdDeleteLocations.TabIndex = 33;
            this.grdDeleteLocations.Text = "myGridView2";
            // 
            // chkAllPostCodes
            // 
            this.chkAllPostCodes.AutoSize = true;
            this.chkAllPostCodes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllPostCodes.Location = new System.Drawing.Point(10, 6);
            this.chkAllPostCodes.Name = "chkAllPostCodes";
            this.chkAllPostCodes.Size = new System.Drawing.Size(41, 20);
            this.chkAllPostCodes.TabIndex = 5;
            this.chkAllPostCodes.Text = "All";
            this.chkAllPostCodes.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox2.Location = new System.Drawing.Point(476, 6);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(46, 20);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Exc";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // grdLocations
            // 
            this.grdLocations.AutoCellFormatting = false;
            this.grdLocations.Controls.Add(this.chkAllLocations);
            this.grdLocations.Dock = System.Windows.Forms.DockStyle.Left;
            this.grdLocations.EnableCheckInCheckOut = false;
            this.grdLocations.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdLocations.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdLocations.Location = new System.Drawing.Point(3, 35);
            this.grdLocations.Name = "grdLocations";
            this.grdLocations.PKFieldColumnName = "";
            this.grdLocations.ShowImageOnActionButton = true;
            this.grdLocations.Size = new System.Drawing.Size(533, 503);
            this.grdLocations.TabIndex = 31;
            this.grdLocations.Text = "myGridView1";
            // 
            // chkAllLocations
            // 
            this.chkAllLocations.AutoSize = true;
            this.chkAllLocations.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAllLocations.Location = new System.Drawing.Point(5, 7);
            this.chkAllLocations.Name = "chkAllLocations";
            this.chkAllLocations.Size = new System.Drawing.Size(41, 20);
            this.chkAllLocations.TabIndex = 5;
            this.chkAllLocations.Text = "All";
            this.chkAllLocations.UseVisualStyleBackColor = true;
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.ddlLocationTypes2);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel2.Location = new System.Drawing.Point(3, 3);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(828, 32);
            this.radPanel2.TabIndex = 32;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(6, 6);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(107, 19);
            this.radLabel1.TabIndex = 19;
            this.radLabel1.Text = "Location Types";
            // 
            // ddlLocationTypes2
            // 
            this.ddlLocationTypes2.Caption = null;
            this.ddlLocationTypes2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlLocationTypes2.Location = new System.Drawing.Point(119, 4);
            this.ddlLocationTypes2.Name = "ddlLocationTypes2";
            this.ddlLocationTypes2.Property = null;
            this.ddlLocationTypes2.ShowDownArrow = true;
            this.ddlLocationTypes2.Size = new System.Drawing.Size(229, 24);
            this.ddlLocationTypes2.TabIndex = 25;
            // 
            // frmManageLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 605);
            this.ControlBox = true;
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.FormTitle = "Manage Locations";
            this.Name = "frmManageLocations";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Manage Locations";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmZones_FormClosed);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
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
            this.tabControl1.ResumeLayout(false);
            this.AddLocations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPostCodes.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPostCodes)).EndInit();
            this.grdPostCodes.ResumeLayout(false);
            this.grdPostCodes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            this.radPanel3.ResumeLayout(false);
            this.radPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGetAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPostCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLocations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveClose)).EndInit();
            this.DeleteLocations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnExit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDeleteLocations.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDeleteLocations)).EndInit();
            this.grdDeleteLocations.ResumeLayout(false);
            this.grdDeleteLocations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocations.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocations)).EndInit();
            this.grdLocations.ResumeLayout(false);
            this.grdLocations.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlLocationTypes2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton btnExit1;
        private Telerik.WinControls.UI.RadButton btnSaveClose;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AddLocations;
        private System.Windows.Forms.TabPage DeleteLocations;
        private UI.MyGridView grdPostCodes;
        private UI.MyGridView grdLocations;
        private System.Windows.Forms.CheckBox chkIncludeAll;
        private System.Windows.Forms.CheckBox chkExcludeAll;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadButton btnGetAll;
        private Telerik.WinControls.UI.RadLabel lblPostCode;
        private UI.MyDropDownList ddlLocations;
        private System.Windows.Forms.CheckBox chkAllPostCodes;
        private System.Windows.Forms.CheckBox checkBox2;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private UI.MyDropDownList ddlLocationTypes2;
        private Telerik.WinControls.UI.RadButton btnExit2;
        private Telerik.WinControls.UI.RadButton btnDelete;
        private UI.MyGridView grdDeleteLocations;
        private System.Windows.Forms.CheckBox chkAllLocations;
    }
}