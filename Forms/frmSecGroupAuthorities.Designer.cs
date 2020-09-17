namespace Taxi_AppMain
{
    partial class frmSecGroupAuthorities
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
            this.components = new System.ComponentModel.Container();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.ddlGroups = new Telerik.WinControls.UI.RadDropDownList();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv_Forms = new Telerik.WinControls.UI.RadTreeView();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radPanel3 = new Telerik.WinControls.UI.RadPanel();
            this.grdFunctions = new Telerik.WinControls.UI.RadGridView();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel4 = new Telerik.WinControls.UI.RadPanel();
            this.grdReportTemplates = new Telerik.WinControls.UI.RadGridView();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.txtId = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tv_Forms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).BeginInit();
            this.radPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFunctions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFunctions.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).BeginInit();
            this.radPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReportTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReportTemplates.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveOn
            // 
            this.btnSaveOn.Location = new System.Drawing.Point(572, 48);
            // 
            // btnOnNew
            // 
            this.btnOnNew.Location = new System.Drawing.Point(492, 48);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(652, 48);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(9, 23);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(48, 19);
            this.radLabel1.TabIndex = 17;
            this.radLabel1.Text = "Group";
            // 
            // ddlGroups
            // 
            this.ddlGroups.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlGroups.Location = new System.Drawing.Point(69, 21);
            this.ddlGroups.Name = "ddlGroups";
            this.ddlGroups.Size = new System.Drawing.Size(263, 23);
            this.ddlGroups.TabIndex = 18;
            this.ddlGroups.SelectedValueChanged += new System.EventHandler(this.ddlGroups_SelectedValueChanged);
            // 
            // radPanel1
            // 
            this.radPanel1.Controls.Add(this.splitContainer1);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 115);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(733, 632);
            this.radPanel1.TabIndex = 19;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv_Forms);
            this.splitContainer1.Panel1.Controls.Add(this.radLabel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(733, 632);
            this.splitContainer1.SplitterDistance = 244;
            this.splitContainer1.TabIndex = 4;
            // 
            // tv_Forms
            // 
            this.tv_Forms.BackColor = System.Drawing.Color.Transparent;
            this.tv_Forms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_Forms.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tv_Forms.Location = new System.Drawing.Point(0, 18);
            this.tv_Forms.Name = "tv_Forms";
            this.tv_Forms.Size = new System.Drawing.Size(244, 614);
            this.tv_Forms.SpacingBetweenNodes = 2;
            this.tv_Forms.TabIndex = 0;
            this.tv_Forms.Text = "radTreeView1";
            // 
            // radLabel2
            // 
            this.radLabel2.AutoSize = false;
            this.radLabel2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.radLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(0, 0);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(244, 18);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Forms List";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.radPanel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.radPanel4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(485, 632);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // radPanel3
            // 
            this.radPanel3.Controls.Add(this.grdFunctions);
            this.radPanel3.Controls.Add(this.radLabel3);
            this.radPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel3.Location = new System.Drawing.Point(3, 3);
            this.radPanel3.Name = "radPanel3";
            this.radPanel3.Size = new System.Drawing.Size(479, 310);
            this.radPanel3.TabIndex = 0;
            // 
            // grdFunctions
            // 
            this.grdFunctions.BackColor = System.Drawing.Color.Transparent;
            this.grdFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFunctions.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdFunctions.Location = new System.Drawing.Point(0, 18);
            this.grdFunctions.Name = "grdFunctions";
            this.grdFunctions.ShowGroupPanel = false;
            this.grdFunctions.Size = new System.Drawing.Size(479, 292);
            this.grdFunctions.TabIndex = 4;
            this.grdFunctions.Text = "radGridView1";
            this.grdFunctions.ValueChanging += new Telerik.WinControls.UI.ValueChangingEventHandler(this.grdFunctions_ValueChanging);
            // 
            // radLabel3
            // 
            this.radLabel3.AutoSize = false;
            this.radLabel3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.radLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(0, 0);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(479, 18);
            this.radLabel3.TabIndex = 5;
            this.radLabel3.Text = "Functions";
            // 
            // radPanel4
            // 
            this.radPanel4.Controls.Add(this.grdReportTemplates);
            this.radPanel4.Controls.Add(this.radLabel4);
            this.radPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel4.Location = new System.Drawing.Point(3, 319);
            this.radPanel4.Name = "radPanel4";
            this.radPanel4.Size = new System.Drawing.Size(479, 310);
            this.radPanel4.TabIndex = 1;
            // 
            // grdReportTemplates
            // 
            this.grdReportTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReportTemplates.Location = new System.Drawing.Point(0, 18);
            // 
            // grdReportTemplates
            // 
            this.grdReportTemplates.MasterTemplate.AllowAddNewRow = false;
            this.grdReportTemplates.Name = "grdReportTemplates";
            this.grdReportTemplates.ShowGroupPanel = false;
            this.grdReportTemplates.Size = new System.Drawing.Size(479, 292);
            this.grdReportTemplates.TabIndex = 8;
            this.grdReportTemplates.Text = "radGridView1";
            this.grdReportTemplates.ValueChanging += new Telerik.WinControls.UI.ValueChangingEventHandler(this.grdReportTemplates_ValueChanging);
            // 
            // radLabel4
            // 
            this.radLabel4.AutoSize = false;
            this.radLabel4.BackColor = System.Drawing.Color.LightSkyBlue;
            this.radLabel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.Location = new System.Drawing.Point(0, 0);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(479, 18);
            this.radLabel4.TabIndex = 7;
            this.radLabel4.Text = "Templates";
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.txtId);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.ddlGroups);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radPanel2.Location = new System.Drawing.Point(0, 38);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(733, 77);
            this.radPanel2.TabIndex = 20;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(373, 23);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(100, 20);
            this.txtId.TabIndex = 19;
            this.txtId.TabStop = false;
            this.txtId.Visible = false;
            // 
            // frmSecGroupAuthorities
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 747);
            this.Controls.Add(this.radPanel1);
            this.Controls.Add(this.radPanel2);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormTitle = "Security Group Authorities";
            this.Name = "frmSecGroupAuthorities";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowAddNewButton = true;
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.ShowSaveButton = true;
            this.Text = "Security Group Authorities";
            this.Load += new System.EventHandler(this.frmSecGroupAuthorities_Load);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.radPanel2, 0);
            this.Controls.SetChildIndex(this.radPanel1, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ddlGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tv_Forms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel3)).EndInit();
            this.radPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFunctions.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFunctions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel4)).EndInit();
            this.radPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReportTemplates.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReportTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList ddlGroups;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadTreeView tv_Forms;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Telerik.WinControls.UI.RadTextBox txtId;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Telerik.WinControls.UI.RadPanel radPanel3;
        private Telerik.WinControls.UI.RadGridView grdFunctions;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadPanel radPanel4;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadGridView grdReportTemplates;
    }
}