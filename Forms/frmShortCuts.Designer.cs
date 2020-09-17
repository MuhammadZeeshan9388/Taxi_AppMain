namespace Taxi_AppMain
{
    partial class frmShortCuts
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
            this.tv_Forms = new Telerik.WinControls.UI.RadTreeView();
            this.grdFormShortCuts = new UI.MyGridView();
            this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tv_Forms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFormShortCuts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFormShortCuts.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).BeginInit();
            this.radGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tv_Forms
            // 
            this.tv_Forms.BackColor = System.Drawing.Color.AliceBlue;
            this.tv_Forms.Dock = System.Windows.Forms.DockStyle.Left;
            this.tv_Forms.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tv_Forms.Location = new System.Drawing.Point(0, 38);
            this.tv_Forms.Name = "tv_Forms";
            this.tv_Forms.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            // 
            // 
            // 
            this.tv_Forms.RootElement.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tv_Forms.Size = new System.Drawing.Size(209, 617);
            this.tv_Forms.SpacingBetweenNodes = 2;
            this.tv_Forms.TabIndex = 112;
            this.tv_Forms.Text = "radTreeView1";
            // 
            // grdFormShortCuts
            // 
            this.grdFormShortCuts.AutoCellFormatting = false;
            this.grdFormShortCuts.BackColor = System.Drawing.Color.AliceBlue;
            this.grdFormShortCuts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFormShortCuts.EnableCheckInCheckOut = false;
            this.grdFormShortCuts.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdFormShortCuts.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdFormShortCuts.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdFormShortCuts.Location = new System.Drawing.Point(10, 20);
            // 
            // grdFormShortCuts
            // 
            this.grdFormShortCuts.MasterTemplate.AllowAddNewRow = false;
            this.grdFormShortCuts.MasterTemplate.ShowColumnHeaders = false;
            this.grdFormShortCuts.Name = "grdFormShortCuts";
            this.grdFormShortCuts.PKFieldColumnName = "";
            this.grdFormShortCuts.ShowGroupPanel = false;
            this.grdFormShortCuts.ShowImageOnActionButton = true;
            this.grdFormShortCuts.Size = new System.Drawing.Size(601, 587);
            this.grdFormShortCuts.TabIndex = 112;
            this.grdFormShortCuts.Text = "radGridView1";
            // 
            // radGroupBox1
            // 
            this.radGroupBox1.Controls.Add(this.grdFormShortCuts);
            this.radGroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGroupBox1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGroupBox1.FooterImageIndex = -1;
            this.radGroupBox1.FooterImageKey = "";
            this.radGroupBox1.HeaderImageIndex = -1;
            this.radGroupBox1.HeaderImageKey = "";
            this.radGroupBox1.HeaderMargin = new System.Windows.Forms.Padding(0);
            this.radGroupBox1.HeaderText = "Short Keys Description";
            this.radGroupBox1.Location = new System.Drawing.Point(209, 38);
            this.radGroupBox1.Name = "radGroupBox1";
            this.radGroupBox1.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            // 
            // 
            // 
            this.radGroupBox1.RootElement.Padding = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.radGroupBox1.Size = new System.Drawing.Size(621, 617);
            this.radGroupBox1.TabIndex = 113;
            this.radGroupBox1.Text = "Short Keys Description";
            // 
            // frmShortCuts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 655);
            this.Controls.Add(this.radGroupBox1);
            this.Controls.Add(this.tv_Forms);
            this.FormTitle = "Short Keys List";
            this.Name = "frmShortCuts";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowHeader = true;
            this.Text = "Short Keys List";
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.tv_Forms, 0);
            this.Controls.SetChildIndex(this.radGroupBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tv_Forms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFormShortCuts.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdFormShortCuts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGroupBox1)).EndInit();
            this.radGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadTreeView tv_Forms;
        private UI.MyGridView grdFormShortCuts;
        private Telerik.WinControls.UI.RadGroupBox radGroupBox1;
    }
}