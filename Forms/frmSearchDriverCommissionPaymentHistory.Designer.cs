namespace Taxi_AppMain
{
    partial class frmSearchDriverCommissionPaymentHistory
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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
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
            gridViewTextBoxColumn7.FormatString = "";
            gridViewTextBoxColumn7.HeaderText = "Payment Date";
            gridViewTextBoxColumn7.Name = "PaymentDate";
            gridViewTextBoxColumn7.Width = 140;
            gridViewTextBoxColumn8.FormatString = "";
            gridViewTextBoxColumn8.HeaderText = "Old Balance";
            gridViewTextBoxColumn8.Name = "Balance";
            gridViewTextBoxColumn8.Width = 100;
            gridViewTextBoxColumn9.FormatString = "";
            gridViewTextBoxColumn9.HeaderText = "Commission Pay";
            gridViewTextBoxColumn9.Name = "CommissionPay";
            gridViewTextBoxColumn9.Width = 100;
            gridViewTextBoxColumn10.FormatString = "";
            gridViewTextBoxColumn10.HeaderText = "Balance Due";
            gridViewTextBoxColumn10.Name = "BalanceDue";
            gridViewTextBoxColumn10.Width = 100;
            gridViewTextBoxColumn11.FormatString = "";
            gridViewTextBoxColumn11.HeaderText = "Reason";
            gridViewTextBoxColumn11.Name = "Reason";
            gridViewTextBoxColumn11.Width = 240;
            gridViewTextBoxColumn12.HeaderText = "Week";
            gridViewTextBoxColumn12.Name = "Week";
            this.grdPaymentHistory.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11,
            gridViewTextBoxColumn12});
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
            // frmSearchDriverCommissionPaymentHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 465);
            this.Controls.Add(this.grdPaymentHistory);
            this.Controls.Add(this.lblHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSearchDriverCommissionPaymentHistory";
            this.ShowIcon = false;
            this.Text = "Driver Commission Payment History";
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