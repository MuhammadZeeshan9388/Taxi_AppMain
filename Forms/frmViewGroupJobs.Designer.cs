namespace Taxi_AppMain
{
    partial class frmViewGroupJobs
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
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.grdJobDetails = new UI.MyGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.grdGroupDetails = new UI.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobDetails.MasterTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGroupDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGroupDetails.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.AliceBlue;
            this.radPanel1.Controls.Add(this.radPanel2);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(941, 649);
            this.radPanel1.TabIndex = 106;
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.grdJobDetails);
            this.radPanel2.Controls.Add(this.panel1);
            this.radPanel2.Controls.Add(this.radLabel1);
            this.radPanel2.Controls.Add(this.grdGroupDetails);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radPanel2.Location = new System.Drawing.Point(0, 0);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(941, 649);
            this.radPanel2.TabIndex = 120;
            // 
            // grdJobDetails
            // 
            this.grdJobDetails.AutoCellFormatting = false;
            this.grdJobDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdJobDetails.EnableCheckInCheckOut = false;
            this.grdJobDetails.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdJobDetails.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdJobDetails.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdJobDetails.Location = new System.Drawing.Point(0, 76);
            // 
            // grdJobDetails
            // 
            this.grdJobDetails.MasterTemplate.AllowAddNewRow = false;
            this.grdJobDetails.MasterTemplate.AllowDeleteRow = false;
            this.grdJobDetails.MasterTemplate.AllowEditRow = false;
            this.grdJobDetails.Name = "grdJobDetails";
            this.grdJobDetails.PKFieldColumnName = "";
            this.grdJobDetails.ShowGroupPanel = false;
            this.grdJobDetails.ShowImageOnActionButton = true;
            this.grdJobDetails.Size = new System.Drawing.Size(941, 517);
            this.grdJobDetails.TabIndex = 118;
            this.grdJobDetails.Text = "myGridView1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 593);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(941, 56);
            this.panel1.TabIndex = 119;
            // 
            // btnExit
            // 
            this.btnExit.Image = global::Taxi_AppMain.Properties.Resources.exit;
            this.btnExit.Location = new System.Drawing.Point(811, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(119, 46);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.exit;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnExit.GetChildAt(0))).Text = "Exit";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnExit.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radLabel1
            // 
            this.radLabel1.AutoSize = false;
            this.radLabel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.radLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Location = new System.Drawing.Point(0, 58);
            this.radLabel1.Name = "radLabel1";
            // 
            // 
            // 
            this.radLabel1.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel1.Size = new System.Drawing.Size(941, 18);
            this.radLabel1.TabIndex = 117;
            this.radLabel1.Text = "Group Jobs";
            // 
            // grdGroupDetails
            // 
            this.grdGroupDetails.AutoCellFormatting = false;
            this.grdGroupDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdGroupDetails.EnableCheckInCheckOut = false;
            this.grdGroupDetails.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdGroupDetails.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdGroupDetails.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdGroupDetails.Location = new System.Drawing.Point(0, 0);
            // 
            // grdGroupDetails
            // 
            this.grdGroupDetails.MasterTemplate.AllowAddNewRow = false;
            this.grdGroupDetails.MasterTemplate.AllowDeleteRow = false;
            this.grdGroupDetails.MasterTemplate.AllowEditRow = false;
            this.grdGroupDetails.Name = "grdGroupDetails";
            this.grdGroupDetails.PKFieldColumnName = "";
            this.grdGroupDetails.ShowGroupPanel = false;
            this.grdGroupDetails.ShowImageOnActionButton = true;
            this.grdGroupDetails.Size = new System.Drawing.Size(941, 58);
            this.grdGroupDetails.TabIndex = 116;
            this.grdGroupDetails.Text = "myGridView1";
            // 
            // frmViewGroupJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 649);
            this.Controls.Add(this.radPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmViewGroupJobs";
            this.ShowIcon = false;
            this.Text = "Group Details";
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdJobDetails.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdJobDetails)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGroupDetails.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdGroupDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadPanel radPanel1;
        private UI.MyGridView grdGroupDetails;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private UI.MyGridView grdJobDetails;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton btnExit;
    }
}