namespace Taxi_AppMain
{
    partial class frmBookingStats
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
            this.grdPickupPlot = new Telerik.WinControls.UI.RadGridView();
            this.grdDropOffPlot = new Telerik.WinControls.UI.RadGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupPlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupPlot.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDropOffPlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDropOffPlot.MasterTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdPickupPlot
            // 
            this.grdPickupPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPickupPlot.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPickupPlot.Location = new System.Drawing.Point(0, 19);
            // 
            // grdPickupPlot
            // 
            this.grdPickupPlot.MasterTemplate.AllowAddNewRow = false;
            this.grdPickupPlot.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdPickupPlot.Name = "grdPickupPlot";
            this.grdPickupPlot.ShowGroupPanel = false;
            this.grdPickupPlot.Size = new System.Drawing.Size(200, 183);
            this.grdPickupPlot.TabIndex = 2;
            this.grdPickupPlot.Text = "radGridView1";
            this.grdPickupPlot.Click += new System.EventHandler(this.grdPickupPlot_Click);
            // 
            // grdDropOffPlot
            // 
            this.grdDropOffPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDropOffPlot.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDropOffPlot.Location = new System.Drawing.Point(0, 19);
            // 
            // grdDropOffPlot
            // 
            this.grdDropOffPlot.MasterTemplate.AllowAddNewRow = false;
            this.grdDropOffPlot.MasterTemplate.AllowEditRow = false;
            this.grdDropOffPlot.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdDropOffPlot.Name = "grdDropOffPlot";
            this.grdDropOffPlot.ShowGroupPanel = false;
            this.grdDropOffPlot.Size = new System.Drawing.Size(351, 183);
            this.grdDropOffPlot.TabIndex = 3;
            this.grdDropOffPlot.Text = "radGridView2";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.radGridView1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 202);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(551, 246);
            this.panel1.TabIndex = 4;
            // 
            // radGridView1
            // 
            this.radGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radGridView1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radGridView1.Location = new System.Drawing.Point(0, 0);
            // 
            // radGridView1
            // 
            this.radGridView1.MasterTemplate.AllowAddNewRow = false;
            this.radGridView1.MasterTemplate.ShowRowHeaderColumn = false;
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.ShowGroupPanel = false;
            this.radGridView1.Size = new System.Drawing.Size(551, 246);
            this.radGridView1.TabIndex = 3;
            this.radGridView1.Text = "radGridView1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(551, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nearest Zones";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grdPickupPlot);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 202);
            this.panel2.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Job Pickup Zone";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdDropOffPlot);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(200, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(351, 202);
            this.panel3.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(351, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Dropping Off Zones";
            // 
            // frmBookingStats
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(551, 448);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmBookingStats";
            this.ShowIcon = false;
            this.Text = "Statistics";
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupPlot.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickupPlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDropOffPlot.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDropOffPlot)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdPickupPlot;
        private Telerik.WinControls.UI.RadGridView grdDropOffPlot;
        private Telerik.WinControls.UI.RadGridView radGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;

    }
}