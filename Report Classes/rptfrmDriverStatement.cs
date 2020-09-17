using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Utils;
using System.IO;
using Taxi_BLL;
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;

namespace Taxi_AppMain
{
    public partial class rptfrmDriverStatement : UI.SetupBase
    {
        public rptfrmDriverStatement()
        {
            InitializeComponent();
        }


        public Gen_SubCompany objSubCompany = null;


        private Fleet_Driver _ObjDriver;

        public Fleet_Driver ObjDriver
        {
            get { return _ObjDriver; }
            set { _ObjDriver = value; }
        }


        private string _DatePeriod;

        public string DatePeriod
        {
            get { return _DatePeriod; }
            set { _DatePeriod = value; }
        }


       
        private List<Vu_BookingBase> _DataSource;

        public List<Vu_BookingBase> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }


        private List<stp_DriverCommisionResult> _DataSource2;

        public List<stp_DriverCommisionResult> DataSource2
        {
            get { return _DataSource2; }
            set { _DataSource2 = value; }
        }
      
      

        private string _ReportHeading;

        public string ReportHeading
        {
            get { return _ReportHeading; }
            set { _ReportHeading = value; }
        }


        private int _StatementType;

        public int StatementType
        {
            get { return _StatementType; }
            set { _StatementType = value; }
        }

        private int _Rent;

        public int Rent
        {
            get { return _Rent; }
            set { _Rent = value; }
        }

        public void GenerateReport()
        {
            try
            {
                Microsoft.Reporting.WinForms.ReportParameter[] param = null;
             
                UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);


                string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";

                if (objTemplate.TemplateName.ToStr() == "Template1")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                    param=  new Microsoft.Reporting.WinForms.ReportParameter[19];
                }
                else if (objTemplate.TemplateName.ToStr() == "Template2")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                    param = new Microsoft.Reporting.WinForms.ReportParameter[19];
                }
                else if (objTemplate.TemplateName.ToStr() == "Template3" || objTemplate.TemplateName.ToStr() == "Template4")
                {
                    GenerateReport3(objTemplate.TemplateName.ToStr().Trim());
                    return;
                  //  this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                  //  param = new Microsoft.Reporting.WinForms.ReportParameter[26];
                }

                reportViewer1.LocalReport.EnableExternalImages = true;

                  

                if(objSubCompany==null)
                   objSubCompany=AppVars.objSubCompany;


                string address = objSubCompany.Address;
                string telNo = "Tel No. " + objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyLogo != null ? objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


                // Summary Calculations
                decimal JobsSum = this.DataSource.Sum(c => c.TotalCharges.ToDecimal());
                int jobsCnt = this.DataSource.Count;
                string jobsTotal = string.Format("{0:c}", JobsSum);
                jobsTotal = jobsTotal.Substring(1);
                jobsTotal = jobsTotal.Insert(0, "£ ");
                decimal zeroValue = 0.00m;
                string zeroStr = string.Format("{0:c}", zeroValue);
                zeroStr = zeroStr.Substring(1);
                zeroStr = zeroStr.Insert(0, "£ ");




                decimal totalCashAccountAmount = this.DataSource.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                                         && c.DriverCommissionType.ToStr() == "Amount").Sum(c => c.DriverCommission.ToDecimal());

                string totalCashAccountAmountStr = string.Format("{0:c}", totalCashAccountAmount);
                totalCashAccountAmountStr = totalCashAccountAmountStr.Substring(1);
                totalCashAccountAmountStr = totalCashAccountAmountStr.Insert(0, "£ ");
                
                string totalAccountBookings = this.DataSource.Count(c => c.CompanyId != null && c.AccountTypeId==Enums.ACCOUNT_TYPE.ACCOUNT).ToStr();
                decimal totalAccountCharges = this.DataSource.Where(c => c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT).Sum(c => c.TotalCharges.ToDecimal());
                string totalAccountBookingsCharges = string.Format("{0:c}", totalAccountCharges).Substring(1);
                totalAccountBookingsCharges = totalAccountBookingsCharges.Insert(0, "£ ");

                string totalCashBooking = this.DataSource.Count(c => c.CompanyId == null || (c.CompanyId!=null && c.AccountTypeId==Enums.ACCOUNT_TYPE.CASH)).ToStr();
                string totalCashBookingCharges = string.Format("{0:c}", this.DataSource.Where(c => c.CompanyId == null ||(c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH))
                                                                                            .Sum(c => c.TotalCharges.ToDecimal()));
                totalCashBookingCharges = totalCashBookingCharges.Substring(1);
                totalCashBookingCharges = totalCashBookingCharges.Insert(0, "£ ");

                decimal driverMonthlyRent = 0;

                 if (Rent > 0)
                 {
                     driverMonthlyRent = (Rent).ToDecimal() + totalCashAccountAmount;
                 }
                 else
                 {
                     driverMonthlyRent = ObjDriver.DriverMonthlyRent.ToDecimal() + totalCashAccountAmount;
                 }

               
                string driverMonthlyRentRate = string.Format("{0:c}", driverMonthlyRent);
                driverMonthlyRentRate = driverMonthlyRentRate.Substring(1);
                driverMonthlyRentRate = driverMonthlyRentRate.Insert(0, "£ ");


                decimal driverOwed = totalAccountCharges - driverMonthlyRent;
                string driverOwedRate = "£ " + string.Format("{0:#.00}", driverOwed);


                if ( StatementType == 2)
                {
                    driverMonthlyRentRate = "£ 0.00";

                    driverOwedRate = jobsTotal;
                }

                //UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);


                //string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";

                //if (objTemplate.TemplateName.ToStr() == "Template1")
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";

                //}
                //else if (objTemplate.TemplateName.ToStr() == "Template2")
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                //}
                //else if (objTemplate.TemplateName.ToStr() == "Template3")
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";
                //}



                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsQuantity", totalAccountBookings.ToStr() + " account booking");
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGross", totalAccountBookingsCharges);
                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGrossTotal", totalAccountBookingsCharges);



                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashQuantity", totalCashBooking + " cash booking");
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGross", totalCashBookingCharges);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGrossTotal", totalCashBookingCharges);

                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAdditions", totalAccountBookingsCharges);
                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDeductions", driverMonthlyRentRate);
                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", jobsTotal);
                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceBFD", zeroStr);



                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", driverOwedRate.ToStr());


                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementType", this.StatementType.ToStr());

                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalCommissionAmount", totalCashAccountAmountStr.ToStr());

                //param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_RentWise", (Rent).ToStr());
                //


                reportViewer1.LocalReport.SetParameters(param);
             

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


        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(this.reportViewer1.LocalReport.DataSources[3]);
        }




        public void GenerateReport3(string templateNo)
        {
            try
            {
                Microsoft.Reporting.WinForms.ReportParameter[] param = null;

                string className = "Taxi_AppMain.ReportDesigns." + templateNo + "_";

               

                this.reportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverStatement.rdlc";



                    decimal PDARent = ObjDriver.PDARent.ToDecimal();
                
                if(templateNo=="Template3")
                    param = new Microsoft.Reporting.WinForms.ReportParameter[22];
                else if (templateNo == "Template4")
                {
                    param = new Microsoft.Reporting.WinForms.ReportParameter[23];

                    param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverPDARent", PDARent.ToStr());

                }


                reportViewer1.LocalReport.EnableExternalImages = true;


                if (objSubCompany == null)
                    objSubCompany = AppVars.objSubCompany;


                string address = objSubCompany.Address;
                string telNo = "Tel No. " + objSubCompany.TelephoneNo;

              
                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyLogo != null ? objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
              //  param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


                // Summary Calculations
                decimal JobsSum = this.DataSource2.Sum(c => c.FareRate.ToDecimal() + c.MeetAndGreetCharges.ToDecimal());
                int jobsCnt = this.DataSource2.Count;
                string jobsTotal = string.Format("{0:c}", JobsSum);
                jobsTotal = jobsTotal.Substring(1);
                jobsTotal = jobsTotal.Insert(0, "£ ");
                decimal zeroValue = 0.00m;
                string zeroStr = string.Format("{0:c}", zeroValue);
                zeroStr = zeroStr.Substring(1);
                zeroStr = zeroStr.Insert(0, "£ ");




                decimal totalCashAccountAmount = this.DataSource2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                                         && c.DriverCommissionType.ToStr() == "Amount").Sum(c => c.DriverCommission.ToDecimal());

                string totalCashAccountAmountStr = string.Format("{0:c}", totalCashAccountAmount);
                totalCashAccountAmountStr = totalCashAccountAmountStr.Substring(1);
                totalCashAccountAmountStr = totalCashAccountAmountStr.Insert(0, "£ ");

                string totalAccountBookings = this.DataSource2.Count(c => c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT).ToStr();
                decimal totalAccountCharges = this.DataSource2.Where(c => c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT).Sum(c => c.FareRate.ToDecimal() + c.MeetAndGreetCharges.ToDecimal());
                string totalAccountBookingsCharges = string.Format("{0:c}", totalAccountCharges).Substring(1);
                totalAccountBookingsCharges = totalAccountBookingsCharges.Insert(0, "£ ");

                string totalCashBooking = this.DataSource2.Count(c => c.CompanyId == null || (c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)).ToStr();
                string totalCashBookingCharges = string.Format("{0:c}", this.DataSource2.Where(c => c.CompanyId == null || (c.CompanyId != null && c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH))
                                                                                            .Sum(c => c.FareRate.ToDecimal() + c.MeetAndGreetCharges.ToDecimal()));
                totalCashBookingCharges = totalCashBookingCharges.Substring(1);
                totalCashBookingCharges = totalCashBookingCharges.Insert(0, "£ ");

                decimal driverMonthlyRent = 0;

                if (Rent > 0)
                {
                    driverMonthlyRent = (Rent).ToDecimal() + totalCashAccountAmount;
                }
                else
                {
                    driverMonthlyRent = ObjDriver.DriverMonthlyRent.ToDecimal() + totalCashAccountAmount;
                }


                string driverMonthlyRentRate = string.Format("{0:c}", driverMonthlyRent);
                driverMonthlyRentRate = driverMonthlyRentRate.Substring(1);
                driverMonthlyRentRate = driverMonthlyRentRate.Insert(0, "£ ");


                // Add pda rent

                driverMonthlyRent = driverMonthlyRent + PDARent;


                decimal driverOwed = totalAccountCharges - driverMonthlyRent;
                string driverOwedRate = "£ " + string.Format("{0:#.00}", driverOwed);


                if (StatementType == 2)
                {
                    driverMonthlyRentRate = "£ 0.00";

                    driverOwedRate = jobsTotal;
                }

               


                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsQuantity", totalAccountBookings.ToStr() + " account booking");
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGross", totalAccountBookingsCharges);
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGrossTotal", totalAccountBookingsCharges);



                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashQuantity", totalCashBooking + " cash booking");
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGross", totalCashBookingCharges);
                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGrossTotal", totalCashBookingCharges);

                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAdditions", totalAccountBookingsCharges);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDeductions", driverMonthlyRentRate);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", jobsTotal);
                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceBFD", zeroStr);



                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", driverOwedRate.ToStr());


                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementType", this.StatementType.ToStr());

              //  param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalCommissionAmount", totalCashAccountAmountStr.ToStr());

                //param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_RentWise", (Rent).ToStr());
                //


               // var query =ObjDriver General.GetObject<Fleet_Driver>(c => c.Id == driverid);
                string PCODRiverExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.PCODriverExpiryDate.ToDateorNull());
                string PCOVehicleExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.PCOVehicleExpiryDate.ToDateorNull());
                string MOT2ExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.MOT2ExpiryDate.ToDateorNull());
                string MOTExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.MOTExpiryDate.ToDateorNull());
                string InsuranceExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.InsuranceExpiryDate.ToDateorNull());
                string LicenseExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.DrivingLicenseExpiryDate.ToDateorNull());
                string RoadTaxiExpiryDate = string.Format("{0:dd/MM/yyyy}", ObjDriver.RoadTaxiExpiryDate.ToDateorNull());

                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCODRiverExpiryDate", PCODRiverExpiryDate);
                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PCOVehicleExpiryDate", PCOVehicleExpiryDate);
                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOT2ExpiryDate", MOT2ExpiryDate);
                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MOTExpiryDate", MOTExpiryDate);
                param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InsuranceExpiryDate", InsuranceExpiryDate);
                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_LicenseExpiryDate", LicenseExpiryDate);
                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_RoadTaxExpiryDate", RoadTaxiExpiryDate);

    

              

                var list2 = (from a in new Taxi_Model.TaxiDataContext().stp_VehicleUsage(fromDate, tillDate, driverId, companyId, statementType)
                             select new stp_VehicleUsageResult
                             {
                                 VehicleType = a.VehicleType,
                                 VehicleNo = a.VehicleNo,
                                 Accbkgs = a.Accbkgs,
                                 AccTotat = a.AccTotat,
                                 CashBkgs = a.CashBkgs,
                                 CashTotal = a.CashTotal,
                                 Booking = a.Booking,
                                 Total = a.Total
                             }).ToList();
              
                
                ReportDataSource courierDataSource = new ReportDataSource("Taxi_Model_stp_VehicleUsageResult", list2);
                this.reportViewer1.LocalReport.DataSources.Add(courierDataSource);


                reportViewer1.LocalReport.SetParameters(param);


                this.stp_DriverCommisionResultBindingSource.DataSource = this.DataSource2;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        public DateTime? fromDate;
        public DateTime? tillDate;
        public int? driverId;
        public int? companyId;
        public int statementType;


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
            saveFileDlg.FileName = "DriverStatement";
          
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



       

        private void btnViewReport_Click(object sender, EventArgs e)
        {

          
        }

        private void rptfrmDriverStatement_Load(object sender, EventArgs e)
        {

        }

        public void SendEmail()
        {

            General.ShowEmailForm(reportViewer1, "Driver Rent Report");

        }
     

     

     
    }
}
