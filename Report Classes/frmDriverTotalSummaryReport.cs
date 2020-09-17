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
using Telerik.WinControls.Enumerations;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class frmDriverTotalSummaryReport : UI.SetupBase
    {

        string TypeReport = "";
        string ReportFor = "";
        private List<stp_DriverSummaryResult> _DataSource;

        public List<stp_DriverSummaryResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        

        public void GenerateReport()
        {
            try
            {


                reportViewer1.LocalReport.EnableExternalImages = true;



                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[27];

                string address = AppVars.objSubCompany.Address;
                string telNo = string.Empty;



                string sortCode = AppVars.objSubCompany.SortCode.ToStr();
                string accountNo = AppVars.objSubCompany.AccountNo.ToStr();
                string accountTitle = AppVars.objSubCompany.AccountTitle.ToStr();
                string bank = AppVars.objSubCompany.BankName.ToStr();

                string hasBankDetails = "1";
                if (string.IsNullOrEmpty(sortCode) && string.IsNullOrEmpty(accountNo) && string.IsNullOrEmpty(accountTitle)
                    && string.IsNullOrEmpty(bank))
                {
                    hasBankDetails = "0";
                }

                if (!string.IsNullOrEmpty(sortCode))
                    sortCode = "Sort Code : " + sortCode;



                if (!string.IsNullOrEmpty(accountTitle))
                    accountTitle = "Account Title : " + accountTitle;



                string website = AppVars.objSubCompany.WebsiteUrl.ToStr();
                if (!string.IsNullOrEmpty(website))
                {
                    website += " , ";
                }

                website += "Email:" + AppVars.objSubCompany.EmailAddress.ToStr();


                string companyNumber = AppVars.objSubCompany.CompanyNumber.ToStr();
                if (!string.IsNullOrEmpty(companyNumber))
                {
                    companyNumber = "Company Number: " + companyNumber;
                }

                string vatNumber = AppVars.objSubCompany.CompanyVatNumber.ToStr();
                if (!string.IsNullOrEmpty(vatNumber))
                {
                    vatNumber = "VAT Number: " + vatNumber;
                }

                
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);

                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", AppVars.objSubCompany.WebsiteUrl.ToStr());

                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MobileNo", "Mobile: " + AppVars.objSubCompany.EmergencyNo.ToStr());
                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);
                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", "Email: " + AppVars.objSubCompany.EmailAddress.ToStr());

                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyNumber", companyNumber);
                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATNumber", vatNumber);


                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle", accountTitle);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());







                int? driverId = this.DataSource.FirstOrDefault().DefaultIfEmpty().DriverId;

                var data = this.DataSource.FirstOrDefault().DefaultIfEmpty();


                telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                if (!string.IsNullOrEmpty(accountNo))
                    accountNo = "Account No : " + accountNo;

                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);


                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountNo", accountNo);




                string vat = "0";
                decimal discountAmount = 0.00m;
                decimal valueAddedTax = 0.0m;



                string discount = string.Format("{0:c}", discountAmount);
                discount = discount.Substring(1);

                string grandTotal = "";

                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Discount", discount);


                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InvoiceTotal", grandTotal);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasVat", vat);

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VAT", valueAddedTax.ToStr());
                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasDepartment", "0");

                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Net", "0");

                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasCostCenter", "0");

                param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasBankDetails", hasBankDetails);

                

                param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountJobTotal", "");
                param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CashJobTotal", "");


                Fleet_Driver obj = General.GetObject<Fleet_Driver>(c => c.Id == driverId);


                param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Commision", "");

                string DriverGrandTotal = "";
                
                param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_GrandTotal", DriverGrandTotal);

                param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_ReportFor", ReportFor);

                reportViewer1.LocalReport.SetParameters(param);


                int cnt = this.DataSource.Count;



                this.vu_DriverCommisionBindingSource.DataSource = this.DataSource;

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        public frmDriverTotalSummaryReport()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmInvoiceReport_Load);

            string Year = DateTime.Now.Year.ToStr();

            dtpFromDate.Value = DateTime.Now.GetStartOfCurrentWeek();
            dtpTillDate.Value = DateTime.Now.GetEndOfCurrentWeek();
        }

  



        public void ExportReport(string invoiceNo)
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
            saveFileDlg.FileName = "DriverMonthlyCommisionReport-" + invoiceNo;


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

        

        void frmInvoiceReport_Load(object sender, EventArgs e)
        {
            if (optRentDriver.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                TypeReport = "RentDrivers";
            }
            else if (optCommisionDrivers.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                TypeReport = "CommisionDrivers";
            }
            else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {

                TypeReport = "BothDrivers";
            }

        }
        private void ViewReport()
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

            lblCriteria.Text = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + "  to  " + string.Format("{0:dd/MM/yyyy}", tillDate);


            // for sp

            
            bool col_Rent = false;
            bool col_Commision = false;
            bool col_Both = false;

            int DriverType = 0;
            if (optRentDriver.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                DriverType = 1;
                col_Rent = true;
                ReportFor = "For Rent Drivers";
            }
            else if (optCommisionDrivers.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                DriverType = 2;
                col_Commision = true;
                ReportFor = "For Commision Drivers";
            }
            else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                DriverType = 0;
                col_Both = true;
                ReportFor = "Rent/Commision Drivers";
            }

            var list = (from a in (new TaxiDataContext()).stp_DriverSummary(fromDate, tillDate)
                        where
                        (a.subcompanyId==AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId==0)
                        &&
                        (col_Rent && (a.DriverTypeId == DriverType))
                        || (col_Commision && (a.DriverTypeId == DriverType))
                        || (col_Both && (a.DriverTypeId != 0))
                        select new stp_DriverSummaryResult
                        {
                            DriverName = a.DriverName,
                            DriverNo = a.DriverNo,
                            DriverVehicle = a.DriverVehicle,
                            TottalBookings = a.TottalBookings,
                            totalEarning = a.totalEarning

                        }).OrderBy(i => i.DriverNo, new NaturalSortComparer<string>()).ToList();

            this.DataSource = list;

            GenerateReport();
           
            

        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {

            ViewReport();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        public void SendEmail(string invoiceNo)
        {

            General.ShowEmailForm(reportViewer1, "Driver Monthly Commision # " + invoiceNo);

        }
        
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                
               

                ViewReport();
                if (this.DataSource.Count > 0)
                {
                    ExportSummaryReport(TypeReport);
                }
                else
                {
                    ENUtils.ShowMessage("Data Not Found");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }

        }
        void ExportSummaryReport(string type)
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
            saveFileDlg.FileName = "DriverSummaryRepot-" + type;

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

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                ViewReport();
                if(this.DataSource.Count>0)
                {
                General.ShowEmailForm(reportViewer1, "Driver Summary Report# " + TypeReport);
                }
                else
                {
                    ENUtils.ShowMessage("Data Not Found");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }
      
    


    }
}
