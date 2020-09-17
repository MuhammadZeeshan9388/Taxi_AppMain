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
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;

namespace Taxi_AppMain
{
    public partial class rptfrmDriverRentSummaryReport : UI.SetupBase
    {
        bool IsReportLoaded = false;
        private List<stp_DriverRentSummaryResult> _DataSource;

        public List<stp_DriverRentSummaryResult> DataSource
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
        public rptfrmDriverRentSummaryReport()
        {
            InitializeComponent();
        }

        private void rptfrmDriverRentSummaryReport_Load(object sender, EventArgs e)
        {
            DefaultDate();
        }
        private void DefaultDate()
        {
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
        }
        private void GenerateReport()
        {
            try
            {
                //if (dtpFromDate.Value != null && dtpFromDate.Value.Year == 1753)
                //    dtpFromDate.Value = null;

                //if (dtpTillDate.Value != null && dtpTillDate.Value.Year == 1753)
                //    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateorNull();



                var list = (from a in new TaxiDataContext().stp_DriverRentSummary(fromDate, tillDate)
                            select new stp_DriverRentSummaryResult
                            {
                                Id = a.Id,
                                DriverNo = a.DriverNo,
                                Account = a.Account,
                                Cash = a.Cash,
                                Rent=a.Rent,
                                Balance = a.Balance
                            }).ToList();
                this.stp_DriverRentSummaryResultBindingSource.DataSource = list;
                //this.stp_DriverCommissionSummaryResultBindingSource.DataSource = list;
                //Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];

                //string address = AppVars.objSubCompany.Address;
                //string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                //param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
                //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                //param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);

                //string heading = string.Empty;
                //if (fromDate != null && tillDate != null)
                //{
                //    heading = string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                //}
                //param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", heading);


                //reportViewer1.LocalReport.SetParameters(param);



                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                IsReportLoaded = true;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
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

            saveFileDlg.FileName = "Driver Rent Summary Report";

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
        public void ExportReportToExcel(string exportTo)
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            //if (exportTo.ToLower() == "pdf")
            //    saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            //else
            saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Driver Rent Summary Report";


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
                    // MessageBox.Show(ex.Message);
                }
            }
        }
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Driver Rent Summary Report");
        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void btnExportToPDF_Click(object sender, EventArgs e)
        {
            if (IsReportLoaded == true)
            {
                ExportReport();
            }
            else
            {
                GenerateReport();
                ExportReport();
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (IsReportLoaded == true)
            {
                ExportReportToExcel("excel");
            }
            else
            {
                GenerateReport();
                ExportReportToExcel("excel");
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;


                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";
                }
                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }
                ReportHeading = "Driver Rent Summary Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);

                DataSource = GetData(fromDate, tillDate);
                GenerateReport();
                SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private List<stp_DriverRentSummaryResult> GetData(DateTime? fromDate, DateTime? tillDate)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_DriverRentSummary(fromDate, tillDate)
                    select new stp_DriverRentSummaryResult
                    {
                        Id = a.Id,
                        DriverNo = a.DriverNo,
                        Account = a.Account,
                        Cash = a.Cash,
                        Rent=a.Rent,
                        Balance = a.Balance
                    }).ToList();
        }
    }
}
