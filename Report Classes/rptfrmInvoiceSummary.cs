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
    public partial class rptfrmInvoiceSummary : UI.SetupBase
    {
        bool IsReportLoded = false;
        private List<stp_InvoiceSummaryResult> _DataSource;

        public List<stp_InvoiceSummaryResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }


        //private string _ReportHeading;

        //public string ReportHeading
        //{
        //    get { return _ReportHeading; }
        //    set { _ReportHeading = value; }
        //}

        public rptfrmInvoiceSummary()
        {
            InitializeComponent();
        }

        private void rptfrmInvoiceSummary_Load(object sender, EventArgs e)
        {
            DefaultDate();
        }
        private void DefaultDate()
        {
            dtpFromDate.Value = DateTime.Now.AddMonths(-1).ToDate();
            dtpTillDate.Value = DateTime.Now.ToDate();
        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadReport()
        {
            try
            {

                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value;
                DateTime? tillDate = dtpTillDate.Value;

                if (chkAllInvoices.Checked == true)
                {
                    fromDate = null;
                    tillDate = null;
                }



                this.reportViewer1.LocalReport.EnableExternalImages = true;
                var list = (from a in new Taxi_Model.TaxiDataContext().stp_InvoiceSummary(fromDate, tillDate)
                                                                           select new stp_InvoiceSummaryResult
                                                                           {
                                                                               CompanyCode=a.CompanyCode,
                                                                               AccountNo=a.AccountNo,
                                                                               CompanyName=a.CompanyName,
                                                                               InvoiceTotal=a.InvoiceTotal
                                                                           }).ToList();


                this.stp_InvoiceSummaryResultBindingSource.DataSource = list;
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                string heading = string.Empty;
                if (fromDate != null && tillDate != null)
                {
                    heading = string.Format("{0:dd/MM/yy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yy HH:mm}", tillDate);
                }
                string CompanyName = string.Empty;
                string CompanyEmail = string.Empty;
                string CompanyWebsite = string.Empty;
                CompanyName = AppVars.objSubCompany.CompanyName.ToStr() +" "+ AppVars.objSubCompany.Address.ToStr();
                CompanyEmail = "T: " + AppVars.objSubCompany.TelephoneNo.ToStr() + " F: " + AppVars.objSubCompany.Fax.ToStr() + " E: " + AppVars.objSubCompany.EmailAddress.ToStr();
                CompanyWebsite = AppVars.objSubCompany.CompanyName.ToStr() + " " + AppVars.objSubCompany.WebsiteUrl.ToStr();
              //  param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);
                //InvoiceDate
                string InvoiceDate = string.Format("{0:dd MMMM yyyy}", DateTime.Now.ToDate());
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InvoiceDate", InvoiceDate);
                //param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyName", CompanyName);
                //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", CompanyEmail);
                //param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_WebSite",CompanyWebsite);

                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                reportViewer1.LocalReport.SetParameters(param);



                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                IsReportLoded = true;
                // }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
            //var list = new Taxi_Model.TaxiDataContext().stp_ControllerReport("Admin", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), 1);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (IsReportLoded == false)
            {
                LoadReport();
                ExportReport();
            }
            else
            {
                ExportReport();
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
            saveFileDlg.FileName = "Invoice Summary Report";

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
        private List<stp_InvoiceSummaryResult> GetData(DateTime? fromDate, DateTime? tillDate)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_InvoiceSummary(fromDate, tillDate)
                    select new stp_InvoiceSummaryResult
                    {
                        AccountNo=a.AccountNo,
                        CompanyName=a.CompanyName,
                        InvoiceTotal=a.InvoiceTotal

                    }).ToList();
        }
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Invoice Summary Report");
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;
                if (chkAllInvoices.Checked == true)
                {
                    fromDate = null;
                    tillDate = null;
                }

                //if (fromDate == null)
                //{
                //    if (string.IsNullOrEmpty(error))
                //        error += Environment.NewLine;

                //    error += "Required : From Date";
                //}

                //if (tillDate == null)
                //{
                //    if (string.IsNullOrEmpty(error))
                //        error += Environment.NewLine;

                //    error += "Required : To Date";
                //}

               // ReportHeading = "Controller Activity Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                DataSource = GetData(fromDate, tillDate);

                LoadReport();
                // frm.LoadReport();

                SendEmail();
 
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void chkAllInvoices_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkAllInvoices.Checked == true)
            {
                dtpFromDate.Enabled = false;
                dtpTillDate.Enabled = false;
            }
            else
            {
                dtpFromDate.Enabled = true;
                dtpTillDate.Enabled = true;
            }
        }
    }
}
