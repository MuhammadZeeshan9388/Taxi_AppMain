namespace Taxi_AppMain
{
    partial class rptfrmDriverInfo
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.vwDriverInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.vwDriverInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vw_DriverInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.vuDriverVehicleHistoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwDriverInfoBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwDriverInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vw_DriverInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vuDriverVehicleHistoryBindingSource)).BeginInit();
            //  ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(961, 812);
            // 
            // vwDriverInfoBindingSource1
            // 
            this.vwDriverInfoBindingSource1.DataSource = typeof(Taxi_Model.vw_DriverInfo);
            // 
            // vwDriverInfoBindingSource
            // 
            this.vwDriverInfoBindingSource.DataSource = typeof(Taxi_Model.vw_DriverInfo);
            // 
            // vw_DriverInfoBindingSource
            // 
            this.vw_DriverInfoBindingSource.DataSource = typeof(Taxi_Model.vw_DriverInfo);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Taxi_Model_vw_DriverInfo";
            reportDataSource1.Value = this.vwDriverInfoBindingSource1;
            reportDataSource2.Name = "Taxi_Model_vu_DriverVehicleHistory";
            reportDataSource2.Value = this.vuDriverVehicleHistoryBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptfrmDriverInfo.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 38);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(836, 703);
            this.reportViewer1.TabIndex = 120;
            // 
            // vuDriverVehicleHistoryBindingSource
            // 
            this.vuDriverVehicleHistoryBindingSource.DataSource = typeof(Taxi_Model.vu_DriverVehicleHistory);
            // 
            // rptfrmDriverInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 741);
            this.ControlBox = true;
            this.Controls.Add(this.reportViewer1);
            this.FormTitle = "Driver Agreement";
            this.Name = "rptfrmDriverInfo";
            // 
            // 
            // 
          //  this.RootElement.ApplyShapeToControl = true;
        //   this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.ShowExitButton = true;
            this.ShowHeader = true;
            this.Text = "Job Log Report";
            this.Load += new System.EventHandler(this.rptfrmDriverInfo_Load);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.reportViewer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwDriverInfoBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vwDriverInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vw_DriverInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vuDriverVehicleHistoryBindingSource)).EndInit();
            //  ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource vw_DriverInfoBindingSource;
        private System.Windows.Forms.BindingSource vwDriverInfoBindingSource;
        private System.Windows.Forms.BindingSource vwDriverInfoBindingSource1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource vuDriverVehicleHistoryBindingSource;
    }
}