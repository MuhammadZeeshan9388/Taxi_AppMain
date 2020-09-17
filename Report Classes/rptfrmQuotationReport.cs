using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using Utils;
using System.IO;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using System.Collections;
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;

namespace Taxi_AppMain
{
    public partial class rptfrmQuotationReport : UI.SetupBase
    {
        bool IsReportLoaded = false;
        public rptfrmQuotationReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(rptfrmQuotationReport_Load);
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
        }

        void rptfrmQuotationReport_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;



            TimeSpan tillTime = TimeSpan.Zero;

            TimeSpan.TryParse("23:59:59", out tillTime);

            dtpToDate.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue()).Date);



            dtptilltime.Value = dtpToDate.Value.Value.Date + tillTime;
        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadReport()
        {
            try
            {
            
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTill = dtpToDate.Value.ToDateorNull();


                if (dtFrom != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    dtFrom = (dtFrom.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();

                if (dtTill != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    dtTill = (dtTill.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();
                string Error = string.Empty;
                if (dtFrom == null)
                {
                    Error = "Required: From Date";
                }
                if (dtTill == null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required: To Date";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required: To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                this.reportViewer1.LocalReport.EnableExternalImages = true;

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.ExecuteQuery<stp_QuotationReportResult>("exec stp_QuotationReport {0},{1},{2}",dtFrom, dtTill, ddlDateCriteria.SelectedIndex.ToInt()).ToList();


                this.stp_QuotationReportResultBindingSource.DataSource = list;
                
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];
                string heading = string.Empty;
                heading = "Date Range: " + string.Format("{0:yyyy-MM-dd HH:mm}", dtFrom) + " to " + string.Format("{0:yyyy-MM-dd HH:mm}", dtTill);
                heading = "From: "+string.Format("{0:dd/MM/yyyy}", dtFrom) + " To: " + string.Format("{0:dd/MM/yyyy}", dtTill);
                 string EndDate = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                string To = string.Format("{0:dd/MM/yyyy}", dtTill);

                 
             
                int Cancelled = list.Where(c => c.BookingStatusId == Enums.BOOKINGSTATUS.CANCELLED).Count();
                int Total = list.Count;
                    int pending =  Total-Cancelled;
                    int confirmed = list.Where(c => c.BookingNo.EndsWith("/Q") && c.IsQuotation.ToBool()==false).Count();


                    string TotalPending = "Total Pending: " + pending.ToStr() ;
                string TotalCancelled = "Total Cancelled: " + Cancelled.ToStr();
                string TotalJobs = "Total Quotations: " + Total.ToStr() ;
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterPeriod", heading);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterTotalPending", TotalPending);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterTotalCancelled", TotalCancelled);
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterTotalQuotation", TotalJobs);
                    param[4] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterTotalConfirmed","Total Confirmed: "+ confirmed);

                    List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
               
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                reportViewer1.LocalReport.SetParameters(param);
                this.reportViewer1.SetDisplayMode(DisplayMode.Normal);
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                IsReportLoaded = true;
                }
                
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
                 
                 
        }
        private decimal AvgJob(decimal Total, decimal Commission, int JobsDone)
        { 
            decimal Avg=0.00m;

            if (JobsDone == 0)
                JobsDone = 1;


          
                Avg = ((Total - Commission) / JobsDone);
            
            return Avg;
        }
        public void ExportReport()
        {

            Microsoft.Reporting.WinForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = reportViewer1.LocalReport.Render(
             "Pdf", null, out mimeType, out encoding,
              out extension,
             out streamids, out warnings);


            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            saveFileDlg.Title = "Save File";

            saveFileDlg.FileName = "Quotation Report";

            //   saveFileDlg.RestoreDirectory = false;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Quotation Report");
        }
        public void ExportReportToExcel(string exportTo)
        {



            SaveFileDialog saveFileDlg = new SaveFileDialog();

            //if (exportTo.ToLower() == "pdf")
            //    saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            //else
            saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Quotation Report";


            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {


                try
                {
                    FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);

                    Microsoft.Reporting.WinForms.Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] bytes = reportViewer1.LocalReport.Render(
                     exportTo.ToLower(), null, out mimeType, out encoding,
                      out extension,
                     out streamids, out warnings);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

 

        private void btnExportPDF_Click(object sender, EventArgs e)
        {


            try
            {

                //rptfrmJobListViewer frm = new rptfrmJobListViewer();

                //DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                //DateTime? toDate = dtpToDate.Value.ToDateorNull();



                //frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);

                if (IsReportLoaded)
                {
                    ExportReport();
                }
                else
                {
                    LoadReport();
                    ExportReport();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsReportLoaded)
                {
                    ExportReportToExcel("Excel");
                }
                else
                {
                    LoadReport();
                    ExportReportToExcel("Excel");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

       

    }
}
