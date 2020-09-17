namespace Taxi_AppMain
{
    partial class frmPlotMovements
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("UnPlotted Addresses");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Plots");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Manually Plotted Addresses");
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.radPageView1 = new Telerik.WinControls.UI.RadPageView();
            this.pg_addresses = new Telerik.WinControls.UI.RadPageViewPage();
            this.grdAddresses = new Telerik.WinControls.UI.RadGridView();
            this.pg_locations = new Telerik.WinControls.UI.RadPageViewPage();
            this.grdLocations = new Telerik.WinControls.UI.RadGridView();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.txtPlotName = new Telerik.WinControls.UI.RadLabel();
            this.btnSaveChanges = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).BeginInit();
            this.radPageView1.SuspendLayout();
            this.pg_addresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddresses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddresses.MasterTemplate)).BeginInit();
            this.pg_locations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocations.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlotName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveChanges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 65);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Snow;
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.radPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(1050, 847);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 106;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.HotTracking = true;
            this.treeView1.Indent = 22;
            this.treeView1.ItemHeight = 22;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "node_unplotted";
            treeNode1.Text = "UnPlotted Addresses";
            treeNode2.Name = "node_plots";
            treeNode2.Text = "Plots";
            treeNode3.Name = "ManualPlotted";
            treeNode3.Text = "Manually Plotted Addresses";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1.Size = new System.Drawing.Size(200, 847);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.radPageView1);
            this.radPanel1.Controls.Add(this.radPanel2);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(846, 847);
            this.radPanel1.TabIndex = 0;
            // 
            // radPageView1
            // 
            this.radPageView1.Controls.Add(this.pg_addresses);
            this.radPageView1.Controls.Add(this.pg_locations);
            this.radPageView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPageView1.Location = new System.Drawing.Point(0, 41);
            this.radPageView1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.radPageView1.Name = "radPageView1";
            this.radPageView1.SelectedPage = this.pg_addresses;
            this.radPageView1.Size = new System.Drawing.Size(846, 806);
            this.radPageView1.TabIndex = 1;
            this.radPageView1.Text = "radPageView1";
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).StripButtons = Telerik.WinControls.UI.StripViewButtons.None;
            ((Telerik.WinControls.UI.RadPageViewStripElement)(this.radPageView1.GetChildAt(0))).ItemFitMode = Telerik.WinControls.UI.StripViewItemFitMode.None;
            ((Telerik.WinControls.UI.RadPageViewContentAreaElement)(this.radPageView1.GetChildAt(0).GetChildAt(1))).Padding = new System.Windows.Forms.Padding(-4, 4, 4, 4);
            ((Telerik.WinControls.UI.RadPageViewContentAreaElement)(this.radPageView1.GetChildAt(0).GetChildAt(1))).Margin = new System.Windows.Forms.Padding(0);
            ((Telerik.WinControls.UI.RadPageViewLabelElement)(this.radPageView1.GetChildAt(0).GetChildAt(2))).Text = "Addresses";
            ((Telerik.WinControls.UI.RadPageViewLabelElement)(this.radPageView1.GetChildAt(0).GetChildAt(2))).Margin = new System.Windows.Forms.Padding(-10, -10, 0, 0);
            // 
            // pg_addresses
            // 
            this.pg_addresses.Controls.Add(this.grdAddresses);
            this.pg_addresses.Location = new System.Drawing.Point(2, 37);
            this.pg_addresses.Name = "pg_addresses";
            this.pg_addresses.Size = new System.Drawing.Size(833, 758);
            this.pg_addresses.Text = "Addresses";
            // 
            // grdAddresses
            // 
            this.grdAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAddresses.Location = new System.Drawing.Point(0, 0);
            this.grdAddresses.Name = "grdAddresses";
            this.grdAddresses.Size = new System.Drawing.Size(833, 758);
            this.grdAddresses.TabIndex = 0;
            this.grdAddresses.Text = "radGridView1";
            // 
            // pg_locations
            // 
            this.pg_locations.Controls.Add(this.grdLocations);
            this.pg_locations.Location = new System.Drawing.Point(2, 37);
            this.pg_locations.Name = "pg_locations";
            this.pg_locations.Size = new System.Drawing.Size(833, 758);
            this.pg_locations.Text = "Locations";
            // 
            // grdLocations
            // 
            this.grdLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLocations.Location = new System.Drawing.Point(0, 0);
            this.grdLocations.Name = "grdLocations";
            this.grdLocations.Size = new System.Drawing.Size(833, 758);
            this.grdLocations.TabIndex = 1;
            this.grdLocations.Text = "radGridView1";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.txtPlotName);
            this.radPanel2.Controls.Add(this.btnSaveChanges);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel2.Location = new System.Drawing.Point(0, 0);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(846, 41);
            this.radPanel2.TabIndex = 0;
            // 
            // txtPlotName
            // 
            this.txtPlotName.AutoSize = false;
            this.txtPlotName.BackColor = System.Drawing.Color.AliceBlue;
            this.txtPlotName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPlotName.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlotName.Location = new System.Drawing.Point(0, 0);
            this.txtPlotName.Name = "txtPlotName";
            this.txtPlotName.Size = new System.Drawing.Size(680, 41);
            this.txtPlotName.TabIndex = 1;
            this.txtPlotName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSaveChanges.Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            this.btnSaveChanges.Location = new System.Drawing.Point(680, 0);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(166, 41);
            this.btnSaveChanges.TabIndex = 0;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveChanges.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.save_Tick;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnSaveChanges.GetChildAt(0))).Text = "Save Changes";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnSaveChanges.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.Black;
            this.radLabel1.Location = new System.Drawing.Point(0, 38);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.Black;
            this.radLabel1.Size = new System.Drawing.Size(1050, 27);
            this.radLabel1.TabIndex = 107;
            this.radLabel1.Text = "Plot Movements";
            this.radLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.radLabel1.TextWrap = false;
            // 
            // frmPlotMovements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 912);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.radLabel1);
            this.Name = "frmPlotMovements";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Plot Movements";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPlotMovements_Load);
            this.Controls.SetChildIndex(this.radLabel1, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPageView1)).EndInit();
            this.radPageView1.ResumeLayout(false);
            this.pg_addresses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAddresses.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddresses)).EndInit();
            this.pg_locations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLocations.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLocations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtPlotName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveChanges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.TreeView treeView1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadPageView radPageView1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadPageViewPage pg_addresses;
        private Telerik.WinControls.UI.RadPageViewPage pg_locations;
        private Telerik.WinControls.UI.RadGridView grdAddresses;
        private Telerik.WinControls.UI.RadGridView grdLocations;
        private Telerik.WinControls.UI.RadButton btnSaveChanges;
        private Telerik.WinControls.UI.RadLabel txtPlotName;

    }
}