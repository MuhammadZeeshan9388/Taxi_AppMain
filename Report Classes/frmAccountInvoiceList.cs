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
    public partial class frmAccountInvoiceList : UI.SetupBase
    {
        bool IsReportLoaded = false;
        private List<stp_AccountInvoiceListResult> _DataSource;

        public List<stp_AccountInvoiceListResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }


        public frmAccountInvoiceList()
        {
            InitializeComponent();
        }
        
        private void frmAccountInvoiceList_Load(object sender, EventArgs e)
        {
            FillCombo();
            DefaultDate();
        }
        private void DefaultDate()
        {
            dtpFromDate.Value = DateTime.Now.AddMonths(-1).ToDate();
            dtpTillDate.Value = DateTime.Now.ToDate();
        }
        private void FillCombo()
        {
            Taxi_AppMain.ComboFunctions.FillCompanyCombo(ddlCompanyList);
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

                int CompanyId = ddlCompanyList.SelectedValue.ToInt();
                if (CompanyId == 0)
                {
                    ENUtils.ShowMessage("Required : Company Name");
                    return;
                }
                //dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //dtpTillDate.Value = DateTime.Now.ToDate();
                int TotalInvoices = 0;
                string CompanyName = string.Empty;
                string Address = string.Empty;
                string Footer = string.Empty;
                string Header = string.Empty;
                string TelephoneNo = string.Empty;
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                var list = (from a in new Taxi_Model.TaxiDataContext().stp_AccountInvoiceList(fromDate, tillDate,CompanyId)
                                                                           select new stp_AccountInvoiceListResult
                                                                           {
                                                                              AccountNO=a.AccountNO,
                                                                              Address=a.Address,
                                                                              CompanyName=a.CompanyName,
                                                                              InvoiceNo=a.InvoiceNo,
                                                                              InvoiceDate=a.InvoiceDate,
                                                                              InvoiceTotal=a.InvoiceTotal,
                                                                              VAT=a.VAT,
                                                                              TotalAmount=a.TotalAmount
                                                                           }).ToList();
                this.stp_AccountInvoiceListResultBindingSource.DataSource = list;
                TotalInvoices = list.Count;
                foreach (var item in list)
                {
                    CompanyName = item.CompanyName;
                    Address = item.Address+"  ("+item.AccountNO+")";
                    break;
                }
                Footer = AppVars.objSubCompany.WebsiteUrl.ToStr();

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];
                Header = AppVars.objSubCompany.CompanyName+" "+ AppVars.objSubCompany.Address+" VAT "+AppVars.objSubCompany.CompanyVatNumber.ToStr();
                TelephoneNo = "T: " + AppVars.objSubCompany.TelephoneNo + " F: " + AppVars.objSubCompany.Fax+" E: "+AppVars.objSubCompany.EmailAddress.ToStr();
                //string address = AppVars.objSubCompany.Address;
                //string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                //string heading = string.Empty;
                //if (fromDate != null && tillDate != null)
                //{
                //    heading = string.Format("{0:dd/MM/yy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yy HH:mm}", tillDate);
                //}
                //Report_Parameter_TelephoneNo
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", Header);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TelephoneNo", TelephoneNo);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalInvoices", TotalInvoices.ToStr());
                
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyAddress", Address);
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", Footer);
                //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_1", address);
                //param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_2", telNo);
                //param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_3", AppVars.objSubCompany.CompanyName.ToStr());

               // List<ClsLogo> objLogo = new List<ClsLogo>();
               // objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
               // ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
               // this.AdminActivity.LocalReport.DataSources.Add(imageDataSource);
                reportViewer1.LocalReport.SetParameters(param);



                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                IsReportLoaded = true;
                // }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
            //var list = new Taxi_Model.TaxiDataContext().stp_ControllerReport("Admin", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10), 1);
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (IsReportLoaded == true)
            {
                ExportReport();
            }
            else
            {
                LoadReport();
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
            saveFileDlg.FileName = "Account Invoice List";

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
        private List<stp_AccountInvoiceListResult> GetData(DateTime? fromDate, DateTime? tillDate, int companyid)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_AccountInvoiceList(fromDate, tillDate, companyid)
                    select new stp_AccountInvoiceListResult
                    {
                        AccountNO=a.AccountNO,
                        Address=a.Address,
                        CompanyName=a.CompanyName,
                        InvoiceNo=a.InvoiceNo,
                        InvoiceDate=a.InvoiceDate,
                        InvoiceTotal=a.InvoiceTotal,
                        VAT=a.VAT,
                        TotalAmount=a.TotalAmount,
                       
                    }).ToList();
        }

        private void SendingEmail()
        {
            try
            {
                int companyid = ddlCompanyList.SelectedValue.ToInt();
                if (companyid == 0)
                {
                    ENUtils.ShowMessage("Required : Company");
                    return;
                }
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

                //ReportHeading = "Controller Activity Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                DataSource = GetData(fromDate, tillDate, companyid);

                LoadReport();
                // frm.LoadReport();

                SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Account Invoice List");
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            SendingEmail();
        }

    }
}
