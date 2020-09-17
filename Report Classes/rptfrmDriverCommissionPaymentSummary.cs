using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using Telerik.WinControls.UI.Export;
using Telerik.Data;


namespace Taxi_AppMain
{
    public partial class rptfrmDriverCommissionPaymentSummary : UI.SetupBase
    {
        RadGridViewExcelExporter exporter = null;

        public rptfrmDriverCommissionPaymentSummary()
        {
            InitializeComponent();
            this.Load += new EventHandler(rptfrmDriverCommissionPaymentSummary_Load);
            ExportToExcelML export = new ExportToExcelML(this.radGridView1);
            export.ExportVisualSettings = true;
            export.HiddenColumnOption = HiddenOption.ExportAsHidden;
            export.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
            export.ExcelCellFormatting += new Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventHandler(export_ExcelCellFormatting);
            //

            //
            //radGridView1.Columns["PickupDate"].HeaderText = "Pickup Date-Time";

            //radGridView1.Columns["From"].HeaderText = "Pick-up Address";
            //radGridView1.Columns["To"].HeaderText = "Drop-off Address";
            //radGridView1.Columns["CompanyName"].HeaderText = "Account";

            //Me.RadGridView1.Columns(0).ExcelExportType = Export.DisplayFormatType.Text

            //CompanyName
            //exporter = new RadGridViewExcelExporter();

            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            //worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            //worker.RunWorkerAsync(saveFileDialog1.FileName);
            //exporter.Progress += new ProgressHandler(exportProgress);

            //this.btnExport.Enabled = false;
        }
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {



            this.btnExport.Enabled = true;
            this.radProgressBar1.Value1 = 0;

            ENUtils.ShowMessage("Export successfully.");

        }

        void rptfrmDriverCommissionPaymentSummary_Load(object sender, EventArgs e)
        {
            PopulateData();
        }
        void export_ExcelCellFormatting(object sender, Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventArgs e)
        {

            e.ExcelStyleElement.AlignmentElement.WrapText = false;
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.IsDisposed)
            {
                e.Cancel = true;
                return;

            }



            exporter.Export(this.radGridView1, (String)e.Argument, "Driver Commission");


        }

        //Update the progress bar with the export progress    
        private void exportProgress(object sender, ProgressEventArgs e)
        {

            if (this.IsDisposed)
                return;
            // Call InvokeRequired to check if thread needs marshalling, to access properly the UI thread.
            try
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new EventHandler(
                    delegate
                    {
                        if (e.ProgressValue <= 100)
                        {
                            radProgressBar1.Value1 = e.ProgressValue;
                        }
                        else
                        {
                            radProgressBar1.Value1 = 100;
                        }
                    }));
                }
                else
                {
                    if (e.ProgressValue <= 100)
                    {
                        radProgressBar1.Value1 = e.ProgressValue;
                    }
                    else
                    {
                        radProgressBar1.Value1 = 100;
                    }
                }
            }
            catch (Exception ex)
            { }
        }
        public override void PopulateData()
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    grdLister.DataSource = db.stp_DriverCommissionPaymentSummary();
                    grdLister.Columns["Id"].IsVisible = false;
                    grdLister.Columns["LastStatementDate"].IsVisible = false;
                     
                    grdLister.Columns["DriverNo"].HeaderText = "Driver No";
                    grdLister.Columns["DriverName"].HeaderText = "Name";
                    grdLister.Columns["VehicleType"].HeaderText = "Vehicle Type";
                    grdLister.Columns["LastStatementNo"].IsVisible = false;
                    //grdLister.Columns["LastStatementNo"].HeaderText = "Last Statement No";
                    grdLister.Columns["DriverName"].Width = 140;
                    grdLister.Columns["CurrentBalance"].HeaderText = "C Balance";
                    grdLister.Columns["DriverNo"].Width = 140;
                    grdLister.Columns["VehicleType"].Width = 140;
                    grdLister.Columns["Owed"].Width = 100;
                    grdLister.Columns["Paid"].Width = 100;
                    grdLister.Columns["CurrentBalance"].Width = 150;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void InitializeExportGrid()
        {
            this.radGridView1 = new Telerik.WinControls.UI.RadGridView();

            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).BeginInit();


            this.radGridView1.Location = new System.Drawing.Point(405, 60);
            this.radGridView1.Name = "radGridView1";
            this.radGridView1.Size = new System.Drawing.Size(240, 150);
            this.radGridView1.TabIndex = 18;
            this.radGridView1.Text = "radGridView1";
            this.radGridView1.Visible = false;

            ((System.ComponentModel.ISupportInitialize)(this.radGridView1.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radGridView1)).EndInit();

            //this.radPanel1.Controls.Add(this.radGridView1);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.OK == saveFileDialog1.ShowDialog())
                {
                    if (radGridView1 == null)
                        InitializeExportGrid();


                    radGridView1.Columns.Clear();
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("DriverNo", "DriverNo"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("DriverName", "Name"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("VehicleType", "Vehicle"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Owed", "Owed"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Paid", "Paid"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("CurrentBalance", "C Balance"));
                    //radGridView1.Columns.Add(new GridViewTextBoxColumn("Driver", "Driver"));
                    //radGridView1.Columns.Add(new GridViewTextBoxColumn("Vehicle", "Vehicle"));
                    //radGridView1.Columns.Add(new GridViewTextBoxColumn("Price", "Price"));

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        var query = db.stp_DriverCommissionPaymentSummary().ToList();


                        

                        radGridView1.RowCount = query.Count;
                        for (int i = 0; i < query.Count; i++)
                        {
                           // radGridView1.Rows[i].Cells["PickupDate"].Value = " " + string.Format(" {0:dd/MM/yyyy HH:mm} ", query[i].PickupDate) + "  ";
                            radGridView1.Rows[i].Cells["DriverNo"].Value = query[i].DriverNo.Replace("=", "").Trim();
                            radGridView1.Rows[i].Cells["DriverName"].Value = query[i].DriverName.Replace("=", "").Trim();
                            radGridView1.Rows[i].Cells["VehicleType"].Value = query[i].VehicleType.Replace("=", "").Trim();
                            //radGridView1.Rows[i].Cells["Customer"].Value = query[i].Customer.Replace("=", "").Trim();
                            //radGridView1.Rows[i].Cells["Telephone"].Value = string.Format(" {0:F0} ", query[i].Telephone) + "."; //" " + string.Format(" {0:dd/MM/yyyy HH:mm} ", query[i].Telephone.Replace("=", "").Trim()) + " ";
                            radGridView1.Rows[i].Cells["Owed"].Value = query[i].Owed;
                            radGridView1.Rows[i].Cells["Paid"].Value = query[i].Paid;
                            radGridView1.Rows[i].Cells["CurrentBalance"].Value = query[i].CurrentBalance;
                            //  radGridView1.Rows[i].Cells["Telephone"].ViewInfo.;
                            //radGridView1.Rows[i].Cells["Telephone"].Value = (radGridView1.Rows[i].Cells["Telephone"].Value.ToString().Replace(".", ""));
                        }
                    }
                    ExportToExcelML export = new ExportToExcelML(this.radGridView1);
                    export.ExportVisualSettings = true;
                    export.HiddenColumnOption = HiddenOption.ExportAsHidden;
                    export.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                    export.ExcelCellFormatting += new Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventHandler(export_ExcelCellFormatting);
                    //

                    radGridView1.Columns["DriverNo"].HeaderText = "Driver No";
                    radGridView1.Columns["DriverName"].HeaderText = "Name";
                    radGridView1.Columns["VehicleType"].HeaderText = "Vehicle";
                   
                    //grdLister.Columns["LastStatementNo"].HeaderText = "Last Statement No";

                    radGridView1.Columns["CurrentBalance"].HeaderText = "C Balance";
                    //
                    //grdLister.Columns["PickupDate"].HeaderText = "Pickup Date-Time";

                    //radGridView1.Columns["From"].HeaderText = "Pick-up Address";
                    //radGridView1.Columns["To"].HeaderText = "Drop-off Address";
                    //radGridView1.Columns["CompanyName"].HeaderText = "Account";

                    //Me.RadGridView1.Columns(0).ExcelExportType = Export.DisplayFormatType.Text

                    //CompanyName
                    exporter = new RadGridViewExcelExporter();

                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
                    worker.RunWorkerAsync(saveFileDialog1.FileName);
                    exporter.Progress += new ProgressHandler(exportProgress);

                    this.btnExport.Enabled = false;
                }
            }

            catch (Exception ex)
            {

            }
        }
    }
}
