using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls;
using Telerik.Data;
using Taxi_AppMain.Classes;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI.Docking;
using UI;

namespace Taxi_AppMain
{
    public partial class frmDriverIncomeStatement : UI.SetupBase
    {
        RadGridViewExcelExporter exporter = null;
        public frmDriverIncomeStatement()
        {
            InitializeComponent();
            grdLister.ReadOnly = true;
            grdLister.ShowGroupPanel = false;
            grdLister.EnableHotTracking = false;
            grdLister.EnableFiltering = true;
            btnExport.Visible = false;
            this.Load += new EventHandler(frmPaymentCollection_Load);
            this.grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            btnPrint.Click += new EventHandler(btnPrint_Click);
            this.btnExportExcel.Click += new EventHandler(btnExportExcel_Click);
        }

        void btnExportExcel_Click(object sender, EventArgs e)
        {
            rptfrmDriverIncomeStatement frm = new rptfrmDriverIncomeStatement();
            frm.LoadReport();
            frm.ExportReportToExcel("excel");
        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            rptfrmDriverIncomeStatement frm = new rptfrmDriverIncomeStatement();
            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverIncomeStatement1");
            if (doc != null)
            {
                doc.Close();
            }
            MainMenuForm.MainMenuFrm.ShowForm(frm);   
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }

        string cellValue = string.Empty;
        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderColor = _HeaderRowBorderColor;
                e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                // e.CellElement.DrawBorder = false;
                e.CellElement.BackColor = _HeaderRowBackColor;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                e.CellElement.ForeColor = Color.White;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }
        }


        void frmPaymentCollection_Load(object sender, EventArgs e)
        {
            PopulateData();
        }
        public override void PopulateData()
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_IncomeStatement().ToList();
                    grdLister.DataSource = list;
                }

                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["DriverName"].IsVisible = false;
                grdLister.Columns["DriverNo"].HeaderText = "Driver";
                grdLister.Columns["NetTotal"].HeaderText = "Net Total";
                grdLister.Columns["AccountBookings"].HeaderText = "Account Bookings";
                grdLister.Columns["OfficeToPay"].HeaderText = "Office To Pay";
                grdLister.Columns["DriverToPay"].HeaderText = "Driver To Pay";
                grdLister.Columns["AccountBookings"].HeaderText = "Account Bookings";
                grdLister.Columns["DriverNo"].Width = 160;
                grdLister.Columns["AccountBookings"].Width = 130;
                grdLister.Columns["Balance"].Width = 130;
                grdLister.Columns["DriverToPay"].Width = 130;
                grdLister.Columns["NetTotal"].Width = 130;
                grdLister.Columns["Rent"].Width = 130;
                grdLister.Columns["OfficeToPay"].Width = 130;
                grdLister.Columns["BalanceBF"].HeaderText = "Balance B/F";
                grdLister.Columns["BalanceBF"].Width = 130;
                //grdLister.Columns["DriverNo"].Width = 120;
                //Active
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
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



            exporter.Export(this.radGridView1, (String)e.Argument, "Income Statement");


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
        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {



            this.btnExport.Enabled = true;
            this.radProgressBar1.Value1 = 0;

            ENUtils.ShowMessage("Export successfully.");

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
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("NetTotal", "NetTotal"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Rent", "Rent"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("AccountBookings", "AccountBookings"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("Balance", "Balance"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("OfficeToPay", "OfficeToPay"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("DriverToPay", "DriverToPay"));
                    radGridView1.Columns.Add(new GridViewTextBoxColumn("BalanceBF", "BalanceBF"));
                    //radGridView1.Columns.Add(new GridViewTextBoxColumn("Total", "Total"));
                    //radGridView1.Columns.Add(new GridViewTextBoxColumn("Active", "Active"));


                    //this.radGridView1.Columns["Telephone"].ExcelExportType = Telerik.WinControls.UI.Export.DisplayFormatType.Text;
                    //this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Custom;

                    //   radGridView1.DataSource = query;
                   
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        var list = db.stp_IncomeStatement().ToList();
                       // grdLister.DataSource = list;



                        radGridView1.RowCount = list.Count;
                        for (int i = 0; i < list.Count; i++)
                        {
                            radGridView1.Rows[i].Cells["DriverNo"].Value = list[i].DriverNo;
                            radGridView1.Rows[i].Cells["NetTotal"].Value = list[i].NetTotal;
                            radGridView1.Rows[i].Cells["Rent"].Value = list[i].Rent;
                            radGridView1.Rows[i].Cells["AccountBookings"].Value = list[i].AccountBookings;
                            radGridView1.Rows[i].Cells["Balance"].Value = list[i].Balance;
                            radGridView1.Rows[i].Cells["OfficeToPay"].Value = list[i].OfficeToPay;
                            radGridView1.Rows[i].Cells["DriverToPay"].Value = list[i].DriverToPay;
                            radGridView1.Rows[i].Cells["BalanceBF"].Value = list[i].BalanceBF;
                            //  radGridView1.Rows[i].Cells["Telephone"].ViewInfo.;
                            //radGridView1.Rows[i].Cells["Telephone"].Value = (radGridView1.Rows[i].Cells["Telephone"].Value.ToString().Replace(".", ""));
                        }
                    }

                    // this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Fixed;
                    //this.radGridView1.Columns["Telephone"].ExcelExportType = DisplayFormatType.Custom;
                    ExportToExcelML export = new ExportToExcelML(this.radGridView1);
                    export.ExportVisualSettings = true;
                    export.HiddenColumnOption = HiddenOption.ExportAsHidden;
                    export.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                    export.ExcelCellFormatting += new Telerik.WinControls.UI.Export.ExcelML.ExcelCellFormattingEventHandler(export_ExcelCellFormatting);
                    //

                    //
                    
                    //radGridView1.Columns["DriverNo"].HeaderText = "Driver No";
                    //radGridView1.Columns["AccountBookings"].HeaderText = "Account Bookings";
                    //radGridView1.Columns["OfficeToPay"].HeaderText = "Office To Pay";
                    //radGridView1.Columns["BalanceBF"].HeaderText = "Balance B/F";
                    //radGridView1.Columns["DriverToPay"].HeaderText = "Driver To Pay";

                    radGridView1.Columns["DriverNo"].HeaderText = "Driver";
                    radGridView1.Columns["AccountBookings"].HeaderText = "A/C Bookings";
                    radGridView1.Columns["OfficeToPay"].HeaderText = "Office To Pay";
                    radGridView1.Columns["BalanceBF"].HeaderText = "Blnc B/F";
                    radGridView1.Columns["DriverToPay"].HeaderText = "Drv To Pay";
                    //

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
                ENUtils.ShowMessage(ex.Message);

            }
        }

    }
}
