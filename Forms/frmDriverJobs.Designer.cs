namespace Taxi_AppMain
{
    partial class frmDriverJobs
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.RefNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PickupDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PickupPoint = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fare = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RefNo,
            this.PickupDateTime,
            this.PickupPoint,
            this.Destination,
            this.Id,
            this.Fare});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(854, 376);
            this.dataGridView1.TabIndex = 0;
            // 
            // RefNo
            // 
            this.RefNo.HeaderText = "Ref #";
            this.RefNo.Name = "RefNo";
            this.RefNo.ReadOnly = true;
            this.RefNo.Width = 80;
            // 
            // PickupDateTime
            // 
            this.PickupDateTime.HeaderText = "Pickup Date/Time";
            this.PickupDateTime.Name = "PickupDateTime";
            this.PickupDateTime.ReadOnly = true;
            this.PickupDateTime.Width = 120;
            // 
            // PickupPoint
            // 
            this.PickupPoint.HeaderText = "PickupPoint";
            this.PickupPoint.Name = "PickupPoint";
            this.PickupPoint.ReadOnly = true;
            this.PickupPoint.Width = 260;
            // 
            // Destination
            // 
            this.Destination.HeaderText = "Destination";
            this.Destination.Name = "Destination";
            this.Destination.ReadOnly = true;
            this.Destination.Width = 260;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Fare
            // 
            this.Fare.HeaderText = "Fares";
            this.Fare.Name = "Fare";
            this.Fare.Width = 70;
            // 
            // frmDriverJobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(854, 376);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDriverJobs";
            this.ShowIcon = false;
            this.Text = "Driver Jobs";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn RefNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PickupDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn PickupPoint;
        private System.Windows.Forms.DataGridViewTextBoxColumn Destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fare;
    }
}