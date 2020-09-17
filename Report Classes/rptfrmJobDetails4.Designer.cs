namespace Taxi_AppMain
{
    partial class rptfrmJobDetails4
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.radLabel24 = new Telerik.WinControls.UI.RadLabel();
            this.btnEmailPrint = new Telerik.WinControls.UI.RadButton();
            this.Vu_BookingDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ClsLogoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel24)).BeginInit();
            this.radLabel24.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vu_BookingDetailBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClsLogoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(962, 0);
            this.btnExit.Size = new System.Drawing.Size(77, 38);
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Taxi_Model_Vu_BookingDetail";
            reportDataSource1.Value = this.Vu_BookingDetailBindingSource;
            reportDataSource2.Name = "Taxi_AppMain_Classes_ClsLogo";
            reportDataSource2.Value = this.ClsLogoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptJobDetails4.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 88);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1085, 756);
            this.reportViewer1.TabIndex = 114;
            // 
            // radLabel24
            // 
            this.radLabel24.AutoSize = false;
            this.radLabel24.BackColor = System.Drawing.Color.DodgerBlue;
            this.radLabel24.Controls.Add(this.btnEmailPrint);
            this.radLabel24.Dock = System.Windows.Forms.DockStyle.Top;
            this.radLabel24.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel24.ForeColor = System.Drawing.Color.White;
            this.radLabel24.Location = new System.Drawing.Point(0, 38);
            this.radLabel24.Name = "radLabel24";
            // 
            // 
            // 
            this.radLabel24.RootElement.ForeColor = System.Drawing.Color.White;
            this.radLabel24.Size = new System.Drawing.Size(1085, 50);
            this.radLabel24.TabIndex = 116;
            this.radLabel24.Text = "Booking Report";
            this.radLabel24.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnEmailPrint
            // 
            this.btnEmailPrint.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnEmailPrint.Image = global::Taxi_AppMain.Properties.Resources.email;
            this.btnEmailPrint.ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            this.btnEmailPrint.Location = new System.Drawing.Point(988, 0);
            this.btnEmailPrint.Name = "btnEmailPrint";
            this.btnEmailPrint.Size = new System.Drawing.Size(97, 50);
            this.btnEmailPrint.TabIndex = 64;
            this.btnEmailPrint.Text = "Email Print";
            this.btnEmailPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailPrint.GetChildAt(0))).Image = global::Taxi_AppMain.Properties.Resources.email;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailPrint.GetChildAt(0))).TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailPrint.GetChildAt(0))).ImageAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailPrint.GetChildAt(0))).Text = "Email Print";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailPrint.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // Vu_BookingDetailBindingSource
            // 
            this.Vu_BookingDetailBindingSource.DataSource = typeof(Taxi_Model.Vu_BookingDetail);
            // 
            // ClsLogoBindingSource
            // 
            this.ClsLogoBindingSource.DataMember = "ClsLogo";
            // 
            // rptfrmJobDetails4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 844);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.radLabel24);
            this.FormTitle = "Booking Report";
            this.HeaderColor = System.Drawing.Color.DodgerBlue;
            this.HeaderForeColor = System.Drawing.Color.White;
            this.Name = "rptfrmJobDetails4";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.RootElement.MinSize = new System.Drawing.Size(150, 36);
            this.Text = "Booking Report";
            this.Load += new System.EventHandler(this.rptfrmJobDetails4_Load);
            this.Controls.SetChildIndex(this.btnExit, 0);
            this.Controls.SetChildIndex(this.btnSaveAndNew, 0);
            this.Controls.SetChildIndex(this.btnSaveAndClose, 0);
            this.Controls.SetChildIndex(this.btnSaveOn, 0);
            this.Controls.SetChildIndex(this.btnOnNew, 0);
            this.Controls.SetChildIndex(this.radLabel24, 0);
            this.Controls.SetChildIndex(this.reportViewer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOnNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSaveAndNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel24)).EndInit();
            this.radLabel24.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Vu_BookingDetailBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClsLogoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Telerik.WinControls.UI.RadLabel radLabel24;
        private Telerik.WinControls.UI.RadButton btnEmailPrint;
        private System.Windows.Forms.BindingSource Vu_BookingDetailBindingSource;
        private System.Windows.Forms.BindingSource ClsLogoBindingSource;
    }
}