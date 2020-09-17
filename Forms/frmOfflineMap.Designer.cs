using System.Windows.Forms;
namespace Taxi_AppMain
{
    partial class frmOfflineMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOfflineMap));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.axMappointControl1 = new AxMapPoint.AxMappointControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grdLister = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.axMappointControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).BeginInit();
            this.SuspendLayout();
            // 
            // axMappointControl1
            // 
            this.axMappointControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMappointControl1.Enabled = true;
            this.axMappointControl1.Location = new System.Drawing.Point(0, 0);
            this.axMappointControl1.Name = "axMappointControl1";
            this.axMappointControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMappointControl1.OcxState")));
            this.axMappointControl1.Size = new System.Drawing.Size(977, 832);
            this.axMappointControl1.TabIndex = 117;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
         //   this.pictureBox1.Image = global::TreasureRouteMap.Properties.Resources.tg_world_map_loading;
            this.pictureBox1.Location = new System.Drawing.Point(0, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(977, 739);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 118;
            this.pictureBox1.TabStop = false;
            // 
            // grdLister
            // 
            this.grdLister.AllowUserToAddRows = false;
            this.grdLister.AllowUserToDeleteRows = false;
            this.grdLister.AllowUserToOrderColumns = true;
            this.grdLister.AllowUserToResizeColumns = false;
            this.grdLister.AllowUserToResizeRows = false;
            this.grdLister.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdLister.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdLister.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdLister.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.grdLister.Location = new System.Drawing.Point(0, 0);
            this.grdLister.Name = "grdLister";
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdLister.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grdLister.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.grdLister.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.grdLister.RowTemplate.Height = 50;
            this.grdLister.Size = new System.Drawing.Size(977, 93);
            this.grdLister.TabIndex = 119;
            this.grdLister.Text = "myGridView1";
            this.grdLister.Visible = false;
            // 
            // frmOfflineMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 832);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.grdLister);
            this.Controls.Add(this.axMappointControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmOfflineMap";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Offline Map";
            ((System.ComponentModel.ISupportInitialize)(this.axMappointControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLister)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxMapPoint.AxMappointControl axMappointControl1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DataGridView grdLister;



    }
}