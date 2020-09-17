namespace Taxi_AppMain
{
    partial class frmCustomerBunch
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
            this.grdCustomerBunch = new UI.MyGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerBunch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerBunch.MasterTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // grdCustomerBunch
            // 
            this.grdCustomerBunch.AutoCellFormatting = true;
            this.grdCustomerBunch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCustomerBunch.EnableCheckInCheckOut = false;
            this.grdCustomerBunch.EnableHotTracking = false;
            this.grdCustomerBunch.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdCustomerBunch.HeaderRowBorderColor = System.Drawing.Color.DarkSlateBlue;
            this.grdCustomerBunch.Location = new System.Drawing.Point(0, 0);
            // 
            // grdCustomerBunch
            // 
            this.grdCustomerBunch.MasterTemplate.AllowAddNewRow = false;
            this.grdCustomerBunch.Name = "grdCustomerBunch";
            this.grdCustomerBunch.PKFieldColumnName = "";
            this.grdCustomerBunch.ShowGroupPanel = false;
            this.grdCustomerBunch.ShowImageOnActionButton = true;
            this.grdCustomerBunch.Size = new System.Drawing.Size(617, 566);
            this.grdCustomerBunch.TabIndex = 114;
            this.grdCustomerBunch.Text = "myGridView1";
            // 
            // frmCustomerBunch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 566);
            this.Controls.Add(this.grdCustomerBunch);
            this.Name = "frmCustomerBunch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Bunch";
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerBunch.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCustomerBunch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.MyGridView grdCustomerBunch;
    }
}