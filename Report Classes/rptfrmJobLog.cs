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
//using Telerik.Charting.Styles;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;
using Newtonsoft.Json;

namespace Taxi_AppMain
{
    public partial class rptfrmJobLog : UI.SetupBase
    {

        string bookingNo = "";
        string Ref = "";
        private List<Vu_BookingLog> _DataSource;
        private List<vw_BookingUpdate> _DataSource1;
        private List<vw_BookingUpdate> _DataSource2;

        public List<Vu_BookingLog> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        public List<vw_BookingUpdate> DataSource1
        {
            get { return _DataSource1; }
            set { _DataSource1 = value; }
        }


        public List<vw_BookingUpdate> DataSource2
        {
            get { return _DataSource2; }
            set { _DataSource2 = value; }
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
                
             

                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[18];


                Gen_SubCompany objSubCompany = AppVars.objSubCompany;


                if(this.DataSource.Count > 0)
                {
                    try
                    {

                        if (DataSource[0].SubCompanyId != AppVars.objSubCompany.Id)
                        {
                            objSubCompany = General.GetObject<Gen_SubCompany>(c => c.Id == DataSource[0].SubCompanyId);



                            if (objSubCompany == null)
                                objSubCompany = AppVars.objSubCompany;
                        }
                    }
                    catch
                    {
                        if (objSubCompany == null)
                            objSubCompany = AppVars.objSubCompany;
                    }

                }
                


                string address = objSubCompany.Address;
                string telNo = string.Empty;



                string sortCode = objSubCompany.SortCode.ToStr();
                string accountNo = objSubCompany.AccountNo.ToStr();
                string accountTitle = objSubCompany.AccountTitle.ToStr();
                string bank = objSubCompany.BankName.ToStr();

                string hasBankDetails = "1";
                if (string.IsNullOrEmpty(sortCode) && string.IsNullOrEmpty(accountNo) && string.IsNullOrEmpty(accountTitle)
                    && string.IsNullOrEmpty(bank))
                {
                    hasBankDetails = "0";
                }

                if (!string.IsNullOrEmpty(sortCode))
                    sortCode = "Sort Code : " + sortCode;

        

                if(!string.IsNullOrEmpty(accountTitle))
                    accountTitle = "Account Title : " + accountTitle;

             //   if (!string.IsNullOrEmpty(bank))
             //       bank = "Bank : " + bank;



                string website = objSubCompany.WebsiteUrl.ToStr();
                if (!string.IsNullOrEmpty(website))
                {
                    website += " , ";
                }

                website += "Email:" + objSubCompany.EmailAddress.ToStr();


                string companyNumber = objSubCompany.CompanyNumber.ToStr();
                if (!string.IsNullOrEmpty(companyNumber))
                {
                    companyNumber = "Company Number: " + companyNumber;
                }

                string vatNumber = objSubCompany.CompanyVatNumber.ToStr();
                if (!string.IsNullOrEmpty(vatNumber))
                {
                    vatNumber = "VAT Number: " + vatNumber;
                }



                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
               
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", objSubCompany.WebsiteUrl.ToStr());

                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MobileNo","Mobile: "+ objSubCompany.EmergencyNo.ToStr());
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website",website);
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", "Email: "+objSubCompany.EmailAddress.ToStr());

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyNumber",companyNumber);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATNumber", vatNumber);


                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle", accountTitle);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyLogo != null ? objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", objSubCompany.CompanyName.ToStr());

                //int? companyId = this.DataSource.FirstOrDefault().DefaultIfEmpty().CompanyId;

                //decimal invoiceGrandTotal = this.DataSource.FirstOrDefault().DefaultIfEmpty().InvoiceTotal.ToDecimal() + this.DataSource.FirstOrDefault().DefaultIfEmpty().AdminFees.ToDecimal();
                //var data = this.DataSource.FirstOrDefault().DefaultIfEmpty();

                telNo = "Tel No. " + objSubCompany.TelephoneNo;

                    if (!string.IsNullOrEmpty(accountNo))
                        accountNo = "Account No : " + accountNo;                                   
              
                   
                           
                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone",  telNo);

                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountNo", accountNo);                


                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasBankDetails", hasBankDetails);


                int driverId = this.DataSource.FirstOrDefault().DefaultIfEmpty().DriverId.ToInt();


                string phcdriver = " ";
                string phcVehicle = " ";
                string vehicleDetails = " ";
                     

                if (driverId > 0)
                {
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.DeferredLoadingEnabled = false;

                        var list = db.Fleet_Driver_Documents.Where(c => c.DriverId == driverId && (c.DocumentId == Enums.DRIVER_DOCUMENTS.PCODriver || c.DocumentId == Enums.DRIVER_DOCUMENTS.PCOVehicle))
                            .Select(c => new { c.BadgeNumber, c.DocumentId }).ToList();

                        foreach (var item in list)
                        {
                            if (item.DocumentId.ToInt() == Enums.DRIVER_DOCUMENTS.PCODriver)
                            {
                                phcdriver = item.BadgeNumber.ToStr();

                                if (phcdriver.ToStr().Trim().Length == 0)
                                    phcdriver = " ";
                            }

                            if (item.DocumentId.ToInt() == Enums.DRIVER_DOCUMENTS.PCOVehicle)
                            {
                                phcVehicle = item.BadgeNumber.ToStr();


                                if (phcVehicle.ToStr().Trim().Length == 0)
                                    phcVehicle = " ";
                            }
                        }

                        var vehicleDAta = db.Fleet_Drivers.Where(c => c.Id == driverId).Select(args => new { args.VehicleMake, args.VehicleModel, args.VehicleNo }).FirstOrDefault();
                        if (vehicleDAta != null)
                        {
                            vehicleDetails ="Vehicle Details : "+ vehicleDAta.VehicleMake.ToStr() + " - " + vehicleDAta.VehicleModel.ToStr() + " - " + vehicleDAta.VehicleNo.ToStr();

                        }
                      

                    }


                }

                if(phcdriver.ToStr().Trim().Length > 0)
                {
                    phcdriver = "PHC Driver : " + phcdriver;

                }
             

                if (phcVehicle.ToStr().Trim().Length > 0)
                {
                    phcVehicle = "PHC Vehicle : " + phcVehicle;

                }
               

                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter_PHCDriver", phcdriver);


                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter_PHCVehicle", phcVehicle);
                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter_VehicleDetails", vehicleDetails);



                reportViewer1.LocalReport.SetParameters(param);


                int cnt = this.DataSource.Count;

                int minRows = 8;

                if (cnt < minRows)
                {
                    for (int i = 0; i < minRows - cnt; i++)
                    {
                     //   this.DataSource.Add(new Vu_BookingLog { Id = data.Id, CompanyId = data.CompanyId });


                    }

                }




                this.vw_BookingUpdateBindingSource.DataSource = this.DataSource1;
                this.Vu_BookingLogBindingSource.DataSource = this.DataSource;
            //    this.Vu_BookingBaseBindingSource.DataSource = this.DataSource1;

              //  this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.SetDisplayMode(DisplayMode.Normal);
               // this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
      
        }

        public rptfrmJobLog(string BookingRef)
        {
            InitializeComponent();
            chkRefNo.Checked = true;
            bookingNo = BookingRef;
            Ref = BookingRef;


            pnlCriteria.Visible = false;

            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
            if (bookingNo != null)
            {
              txtBookingNo.Text = BookingRef;
                send(0);
               //ViewReport();
                Ref = null;
            }
            
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

        public bool chck(CheckBox chck)
        {
            bool f = false;
            if (chkRefNo.Checked)
                f = true;
            else
                f = false;
            return f;
        }

        private void ViewReport()
        {
            if (Ref == null)
            {
                bookingNo = txtBookingNo.Text.ToString();
            }


            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();


            string error = string.Empty;


            if (chkRefNo.Checked == true)
            {
                this.DataSource = GetDataSource2(bookingNo);
                this.DataSource1 = GetDataSourceupdates(bookingNo,false);

             //   this.DataSource2 = GetDataSourceupdates(bookingNo,true);
            }
            else
            {
                this.DataSource = GetDataSource(fromDate, tillDate);
            }



            if (this.DataSource.Count == 0)
            {
                ENUtils.ShowMessage("No Record(s) Found..");
                return;

            }

            GenerateReport();

        }
        private List<Vu_BookingLog> GetDataSource(DateTime? fromDate, DateTime? tillDate)
        {
            return General.GetQueryable<Vu_BookingLog>(c => c.PickupDateTime.Value.Date >= fromDate.Value.Date && c.PickupDateTime.Value.Date <= tillDate.Value.Date
                                                        && c.DriverId != null
                                                        && (c.SubCompanyId==AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId==0)
                                                        && (c.BookingStatusId != Enums.BOOKINGSTATUS.WAITING && c.BookingStatusId != Enums.BOOKINGSTATUS.NOSHOW
                                                        && c.BookingStatusId != Enums.BOOKINGSTATUS.PENDING && c.BookingStatusId != Enums.BOOKINGSTATUS.REJECTED
                                                        && c.BookingStatusId != Enums.BOOKINGSTATUS.CANCELLED))
                                                       .OrderByDescending(b => b.PickupDateTime).ToList();
           
        }

        private List<Vu_BookingLog> GetDataSource2(string bookingNo)
        {
            return General.GetQueryable<Vu_BookingLog>(c => c.BookingNo == bookingNo)                                                       
                                                    
                                                       .OrderByDescending(b => b.PickupDateTime).ToList();

        }
        private List<vw_BookingUpdate> GetDataSourceupdates(string bookingNo,bool IsSingleUpdate)
        {

            //if (IsSingleUpdate == false)
            //{

                return General.GetQueryable<vw_BookingUpdate>(c => c.BookingNo == bookingNo )
                                                           .OrderByDescending(b => b.BookingId).ToList();
            //}
            //else
            //{
            //    return General.GetQueryable<vw_BookingUpdate>(c => c.BookingNo == bookingNo && (c.IsSingleLog != null && c.IsSingleLog == true))
            //                                             .OrderByDescending(b => b.BookingId).ToList();

            //}
        }
        public void send(int a)
        {
            if (Ref == null)
            {
                bookingNo = txtBookingNo.Text.ToString();
            }

            string viaStr = "**";



            Gen_SubCompany objSubCompany = AppVars.objSubCompany;
            string connString = Application.StartupPath + @"\Reports\Report.exe";
           // string connString =@"D:\Zeeshan\Cloud Despatch\Taxi_AppMain\bin\x86\Debug\Reports\Report.exe";
            //D:\Zeeshan\Cloud Despatch\Taxi_AppMain\bin\x86\Debug
            // string connString = @"C:\Users\Muhammad Zeeshan\Documents\Visual Studio 2010\Projects\Report\Report\bin\Debug\Report.exe";

            //C:\Users\Muhammad Zeeshan\Documents\Visual Studio 2010\Projects\Report\Report\bin\Debug





            //System.Diagnostics.Process proc = System.Diagnostics.Process.GetProcesses().FirstOrDefault(c => c.ProcessName.Contains("Report"));

            //if (proc != null)
            //{
            //    proc.Kill();
            //    proc.CloseMainWindow();
            //    proc.Close();
            //}

            DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
            DateTime? toDate = dtpTillDate.Value.ToDateorNull();
            string conn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr();

           

            Classes.JArguments j = new Classes.JArguments()
            {//Connectionstring =
               
                ConnectionString = conn,
                bookingid = bookingNo.ToString(),
                fromDate = fromDate.Value,
                toDate=toDate.Value,
              _Checkbox =chkRefNo.Checked
             ,reportname= "rptfrmJobDetails"

            };

            // Convert BlogSites object to JOSN string format  
            string jsonData = JsonConvert.SerializeObject(j);
            jsonData = Cryptography.Encrypt(jsonData, "report", true);


            System.Diagnostics.Process.Start(connString, jsonData);
        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
           send(0);
          ///  ViewReport();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        public void SendEmail(string invoiceNo)
        {

            General.ShowEmailForm(reportViewer1, "Account Invoice # "+invoiceNo);

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

        private void chkRefNo_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            DisableChange(args.ToggleState);
        }
        private void DisableChange(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                txtBookingNo.Enabled = true;
                dtpFromDate.Enabled = false;
                dtpTillDate.Enabled = false;

            }
            else
            {
                txtBookingNo.Enabled = false;
                dtpFromDate.Enabled = true;
                dtpTillDate.Enabled = true;

            }            
        }

        private void rptfrmJobLog_Load(object sender, EventArgs e)
        {

        }

        private void pnlCriteria_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
