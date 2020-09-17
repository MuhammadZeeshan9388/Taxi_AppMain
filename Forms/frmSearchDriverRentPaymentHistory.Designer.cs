namespace Taxi_AppMain
{
    partial class frmSearchDriverRentPaymentHistory
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.grdPaymentHistory = new Telerik.WinControls.UI.RadGridView();
            this.lblHeader = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // grdPaymentHistory
            // 
            this.grdPaymentHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPaymentHistory.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdPaymentHistory.Location = new System.Drawing.Point(0, 27);
            // 
            // grdPaymentHistory
            // 
            this.grdPaymentHistory.MasterTemplate.AllowAddNewRow = false;
            this.grdPaymentHistory.MasterTemplate.AllowDeleteRow = false;
            this.grdPaymentHistory.MasterTemplate.AllowEditRow = false;
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Payment Date";
            gridViewTextBoxColumn1.Name = "PaymentDate";
            gridViewTextBoxColumn1.Width = 140;
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Old Balance";
            gridViewTextBoxColumn2.Name = "Balance";
            gridViewTextBoxColumn2.Width = 100;
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "Rent Pay";
            gridViewTextBoxColumn3.Name = "RentPay";
            gridViewTextBoxColumn3.Width = 100;
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "Balance Due";
            gridViewTextBoxColumn4.Name = "BalanceDue";
            gridViewTextBoxColumn4.Width = 100;
            gridViewTextBoxColumn5.FormatString = "";
            gridViewTextBoxColumn5.HeaderText = "Reason";
            gridViewTextBoxColumn5.Name = "Reason";
            gridViewTextBoxColumn5.Width = 240;
            gridViewTextBoxColumn6.HeaderText = "Week";
            gridViewTextBoxColumn6.Name = "Week";
            this.grdPaymentHistory.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
            this.grdPaymentHistory.MasterTemplate.ShowRowHeaderColumn = false;
            this.grdPaymentHistory.Name = "grdPaymentHistory";
            this.grdPaymentHistory.ShowGroupPanel = false;
            this.grdPaymentHistory.Size = new System.Drawing.Size(731, 438);
            this.grdPaymentHistory.TabIndex = 110;
            this.grdPaymentHistory.Text = "radGridView1";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = false;
            this.lblHeader.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            // 
            // 
            // 
            this.lblHeader.RootElement.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Size = new System.Drawing.Size(731, 27);
            this.lblHeader.TabIndex = 109;
            this.lblHeader.Text = "Payment History";
            this.lblHeader.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSearchDriverRentPaymentHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 465);
            this.Controls.Add(this.grdPaymentHistory);
            this.Controls.Add(this.lblHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchDriverRentPaymentHistory";
            this.ShowIcon = false;
            this.Text = "Driver Rent Payment History";
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPaymentHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdPaymentHistory;
        private Telerik.WinControls.UI.RadLabel lblHeader;
    }
}