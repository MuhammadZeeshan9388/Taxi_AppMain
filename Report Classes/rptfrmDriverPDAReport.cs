using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.IO;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class rptfrmDriverPDAReport : UI.SetupBase
    {
        private List<stp_DriverJobsSummaryReportResult> _DataSource;

        public List<stp_DriverJobsSummaryReportResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }


        private string _ReportHeading;

        public string ReportHeading
        {
            get { return _ReportHeading; }
            set { _ReportHeading = value; }
        }

        public rptfrmDriverPDAReport()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(rptfrmDriverPDAReport_Load_1);
        }

        public void LoadReport()
        {
            try
            {
                
                if (fromDate.Value != null && fromDate.Value.Value.Year == 1753)
                    fromDate.Value = null;

                if (tillDate.Value != null && tillDate.Value.Value.Year == 1753)
                    tillDate.Value = null;

                DateTime? fromDates = fromDate.Value.ToDate();
                DateTime? tillDates = tillDate.Value.ToDate();

               
                this.reportViewer1.LocalReport.EnableExternalImages = true;
             
                 var list=(from a in new TaxiDataContext().stp_DriverJobsSummaryReport(fromDates, tillDates)
                           select new stp_DriverJobsSummaryReportResult
                                                                           {
                                                                               DriverNo = a.DriverNo,
                                                                               JobsDone = a.JobsDone,
                                                                               LastLoginAt = a.LastLoginAt
                                                                           }).ToList();

                 this.stp_DriverReportResultBindingSource.DataSource = list;
                                                                             
               
              

                
             


                
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];

                string heading = string.Empty;
                if (fromDates != null && tillDates != null)
                {
                     heading = string.Format("{0:dd/MM/yy HH:mm}", fromDates) + " to " + string.Format("{0:dd/MM/yy HH:mm}", tillDates);
                }

                string totalDrivers= list.Count.ToStr();
                string jobsDone=  list.Sum(c=>c.JobsDone).ToStr();
                
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);
                param[1]=new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDriver",totalDrivers);
                param[2]=new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalJobs",jobsDone);
                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                reportViewer1.LocalReport.SetParameters(param);




                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                
            }
            catch (Exception ex)
            {

            }
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
            saveFileDlg.FileName = "Driver Jobs Report";

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

        private void radViewReport_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadDates()
        {
            fromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            tillDate.Value = DateTime.Now.ToDate();
        }
        private void rptfrmDriverPDAReport_Load_1(object sender, EventArgs e)
        {
            LoadDates();
        }
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportReport();
        }

        private List<stp_DriverJobsSummaryReportResult> GetData(DateTime? fromDates, DateTime? tillDates)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_DriverJobsSummaryReport(fromDates, tillDates)
                    select new stp_DriverJobsSummaryReportResult
                    {
                        DriverNo=a.DriverNo,
                        JobsDone=a.JobsDone,
                        LastLoginAt=a.LastLoginAt
                        
                    }).ToList();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            DateTime? fromDates = fromDate.Value.ToDate();
            DateTime? tillDates = tillDate.Value.ToDate();

            string error = string.Empty;


            if (fromDates == null)
            {
                if (string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : From Date";
            }

            if (tillDates == null)
            {
                if (string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : To Date";
            }

            ReportHeading = "Controller Activity Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDates) + " to " + string.Format("{0:dd/MM/yyyy}", tillDates);
            DataSource = GetData(fromDates, tillDates);

            LoadReport();
            // frm.LoadReport();

            SendEmail();
        }
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Driver Jobs Report");
        }
    }
}
