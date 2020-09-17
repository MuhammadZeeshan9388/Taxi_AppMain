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
    public partial class rptfrmJobActivity : UI.SetupBase
    {
       


        private List<Vu_BookingBase> _DataSource;

        public List<Vu_BookingBase> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        public struct COLS
        {
            public static string ID = "ID";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string OrderNo = "OrderNo";
            public static string PupilNo = "PupilNo";

            public static string RefNumber = "RefNumber";

            public static string Passenger = "Passenger";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

            public static string Charges = "Charges";

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string Total = "Total";

        }


        public void GenerateReport()
        {
            try
            {
                //if (ddlCompany.SelectedValue == null)
                //    pnlCriteria.Visible = false;

                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[16];

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

             //   if (!string.IsNullOrEmpty(bank))
             //       bank = "Bank : " + bank;



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
               
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", AppVars.objSubCompany.WebsiteUrl.ToStr());

                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MobileNo","Mobile: "+ AppVars.objSubCompany.EmergencyNo.ToStr());
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website",website);
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", "Email: "+AppVars.objSubCompany.EmailAddress.ToStr());

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyNumber",companyNumber);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATNumber", vatNumber);


                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle", accountTitle);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);

                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());

                int? companyId = this.DataSource.FirstOrDefault().DefaultIfEmpty().CompanyId;

                //decimal invoiceGrandTotal = this.DataSource.FirstOrDefault().DefaultIfEmpty().InvoiceTotal.ToDecimal() + this.DataSource.FirstOrDefault().DefaultIfEmpty().AdminFees.ToDecimal();
                var data = this.DataSource.FirstOrDefault().DefaultIfEmpty();

                telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                    if (!string.IsNullOrEmpty(accountNo))
                        accountNo = "Account No : " + accountNo;                                   
              
                   
                           
                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone",  telNo);

                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountNo", accountNo);                


                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasBankDetails", hasBankDetails);
                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyWise", ddlCompany.SelectedValue != null ? "1" : "0");


                reportViewer1.LocalReport.SetParameters(param);


                int cnt = this.DataSource.Count;

                int minRows = 8;

                if (cnt < minRows)
                {
                    for (int i = 0; i < minRows - cnt; i++)
                    {
                        this.DataSource.Add(new  Vu_BookingBase {  Id=data.Id,   CompanyId=data.CompanyId });


                    }

                }
                

                this.Vu_BookingBaseBindingSource.DataSource = this.DataSource;

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
      
        }

        public rptfrmJobActivity()
        {
            InitializeComponent();
        
            ComboFunctions.FillCompanyCombo(ddlCompany);

            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
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
            saveFileDlg.FileName = "Invoice-" + invoiceNo;
          
         //   saveFileDlg.RestoreDirectory = false;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {
              
            
                try
                {
                    FileStream fs = new FileStream(saveFileDlg.FileName,FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

                          

        }

  

        private void AddUpdateColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 50;

            col.Name = "btnUpdate";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Update";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }


        private void ViewReport()
        {
            int companyId = ddlCompany.SelectedValue.ToInt();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();
            bool IsCompanyWise = chkCompanyWise.Checked;

            string error = string.Empty;
            if (companyId == 0 && IsCompanyWise)
            {
                error += "Required : Company";
            }

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

            if (!string.IsNullOrEmpty(ddlCompany.Text.ToStr()))
            {
                lblCriteria.Text = "Job Activity Report Related to '" + ddlCompany.Text.ToStr() + "'";
            }
            else
                lblCriteria.Text = "Job Activity Report";

            lblCriteria.Text +=", Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);

          


        

            this.DataSource = GetDataSource(companyId,fromDate,tillDate);


            if (this.DataSource.Count == 0)
            {
                ENUtils.ShowMessage("No Record(s) Found..");
                return;

            }

            GenerateReport();

        }

        private List<Vu_BookingBase> GetDataSource(int? companyId, DateTime? fromDate, DateTime? tillDate)
        {
            return General.GetQueryable<Vu_BookingBase>(c =>c.PickupDateTime.Value.Date >= fromDate.Value.Date && c.PickupDateTime.Value.Date <= tillDate.Value.Date
                                           && (c.CompanyId==companyId || companyId==0) && c.DriverId!=null && (c.AcceptedDateTime!=null || c.ArrivalDateTime!=null) 
                                           && (c.SubCompanyId==AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId==0)
                                           && (c.BookingStatusId!=Enums.BOOKINGSTATUS.WAITING && c.BookingStatusId!=Enums.BOOKINGSTATUS.NOSHOW
                                           && c.BookingStatusId!=Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId!=Enums.BOOKINGSTATUS.REJECTED
                                           && c.BookingStatusId!=Enums.BOOKINGSTATUS.CANCELLED))
                                          .OrderByDescending(b => b.PickupDateTime).ToList();

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

            General.ShowEmailForm(reportViewer1, "Account Invoice # "+invoiceNo);

        }

        private void rptfrmJobActivity_Load(object sender, EventArgs e)
        {

        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            General.ShowEmailForm(reportViewer1, "Job Activity Report");
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
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
                saveFileDlg.FileName = "JobActivity";

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
            catch (Exception ex)
            {


            }
        }

        private void chkCompanyWise_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlCompany.Enabled = true;
            }
            else
            {
                ddlCompany.SelectedValue = null;
                ddlCompany.Enabled = false;

            }
        }
     
     
    }
}
