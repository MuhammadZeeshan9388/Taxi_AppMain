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
    public partial class rptfrmDriverCommissionStatement: UI.SetupBase
    {


        public  Gen_SubCompany objSubCompany = null;

        private bool NoACCommission;

        public rptfrmDriverCommissionStatement()
        {
            InitializeComponent();


            NoACCommission = AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool();
        }

        private string _DatePeriod;

        public string DatePeriod
        {
            get { return _DatePeriod; }
            set { _DatePeriod = value; }
        }
        private int _Commision;

        public int Commision
        {
            get { return _Commision; }
            set { _Commision = value; }
        }

       
        private List<Vu_BookingBase> _DataSource;

        public List<Vu_BookingBase> DataSource
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


        private int _StatementType;

        public int StatementType
        {
            get { return _StatementType; }
            set { _StatementType = value; }
        }



        private string _TemplatePath;

        public string TemplatePath
        {
            get { return _TemplatePath; }
            set { _TemplatePath = value; }
        }




        public void GenerateReport()
        {

            string pickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool() == true ? "1" : "0";
            reportViewer1.LocalReport.EnableExternalImages = true;

            if (objSubCompany == null)
                objSubCompany = AppVars.objSubCompany;

            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[23];

            string address = objSubCompany.Address;
            string telNo = "Tel No. " + objSubCompany.TelephoneNo;

            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);

            if (!string.IsNullOrEmpty(this.TemplatePath.ToStr().Trim()))
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = this.TemplatePath.ToStr().Replace("Template1_","").ToStr().Trim();

                
            }
          
            List<ClsLogo> objLogo = new List<ClsLogo>();
            objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyLogo != null ? objSubCompany.CompanyLogo.ToArray() : null });
            ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
            this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
            

            string path = @"File:";
            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


            // Summary Calculations

            decimal ExtraSum = this.DataSource.Sum(c => c.ExtraDropCharges.ToDecimal());
            string TotalExtraSum = string.Format("{0:c}", ExtraSum);
            TotalExtraSum = TotalExtraSum.Substring(1);
            TotalExtraSum = TotalExtraSum.Insert(0, "£ ");

            decimal JobsSum= this.DataSource.Sum(c => c.TotalCharges.ToDecimal());
            int jobsCnt = this.DataSource.Count;
            string jobsTotal = string.Format("{0:c}", JobsSum);
            jobsTotal = jobsTotal.Substring(1);
            jobsTotal = jobsTotal.Insert(0, "£ ");
            decimal zeroValue = 0.00m;
            string zeroStr = string.Format("{0:c}", zeroValue);
            zeroStr = zeroStr.Substring(1);
            zeroStr = zeroStr.Insert(0, "£ ");


    
            // Old Calculation 27 Nov 14

            string totalAccountBookings = this.DataSource.Count(c => c.CompanyId != null && c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).ToStr();
            decimal totalAccountCharges = this.DataSource.Where(c => c.CompanyId != null && c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).Sum(c => c.TotalCharges.ToDecimal());
            string totalAccountBookingsCharges = string.Format("{0:c}", totalAccountCharges).Substring(1);
            totalAccountBookingsCharges = totalAccountBookingsCharges.Insert(0, "£ ");

            decimal totalCashBooking = this.DataSource.Count(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).ToDecimal();
            decimal SumofTotalCashBookingChrgs = this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).Sum(c => c.TotalCharges.ToDecimal());
            string totalCashBookingCharges = string.Format("{0:c}", this.DataSource.Where(c => c.CompanyId == null || (c.CompanyId != null && c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH)).Sum(c => c.TotalCharges.ToDecimal()));
            totalCashBookingCharges = totalCashBookingCharges.Substring(1);
            totalCashBookingCharges = totalCashBookingCharges.Insert(0, "£ ");



           


            decimal SumOfAccountandCashJobTotal = totalAccountCharges + SumofTotalCashBookingChrgs;
            string TotalSumOfAccountandCashJobTotal = string.Format("{0:c}", SumOfAccountandCashJobTotal);
            TotalSumOfAccountandCashJobTotal = TotalSumOfAccountandCashJobTotal.Substring(1);
            TotalSumOfAccountandCashJobTotal = TotalSumOfAccountandCashJobTotal.Insert(0, "£ ");



            decimal totalCommission = 0.00m;

            if (AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool())
            {
                if (NoACCommission == false)
                {
                    totalCommission = this.DataSource.Sum(c =>c.IsCommissionWise==true ?  (c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission):((c.FareRate * Commision) / 100) ).ToDecimal();
                }
                else
                {


                    totalCommission = this.DataSource.Where(c=>c.CompanyId==null)
                                .Sum(c => c.DriverCommissionType == "Percent" ? ((c.FareRate * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();


                }

            }
            else
            {
                if (Commision > 0)
                {
                    if (NoACCommission == false)
                    {

                        totalCommission = this.DataSource.Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * Commision) / 100) : c.DriverCommission).ToDecimal();
                    }
                    else
                    {
                        totalCommission = this.DataSource.Where(c => c.CompanyId == null).Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * Commision) / 100) : c.DriverCommission).ToDecimal();


                    }
                }
                else
                {
                    if (NoACCommission == false)
                    {
                        totalCommission = this.DataSource.Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();
                    }
                    else
                    {
                        totalCommission = this.DataSource.Where(c => c.CompanyId == null).Sum(c => c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommission) / 100) : c.DriverCommission).ToDecimal();


                    }
                    
                }
            }
          


          
            
            string driverTotalCommission = string.Format("{0:c}", totalCommission);

            driverTotalCommission = driverTotalCommission.Substring(1);
            driverTotalCommission = driverTotalCommission.Insert(0, "£ ");


            string totalSettingsCommission = driverTotalCommission;
       
         
            string driverOwedRate = "£ " + string.Format("{0:c}", totalCommission).Substring(1);

            if (StatementType == Taxi_AppMain.frmDriverReport.eStatementType.CashStatement)
            {
                driverTotalCommission = "£ 0.00";           
                driverOwedRate = jobsTotal;
            }
            else if (StatementType == Taxi_AppMain.frmDriverReport.eStatementType.Both
                || StatementType == Taxi_AppMain.frmDriverReport.eStatementType.AccountStatement)
            {
             

                driverOwedRate = "£ " + string.Format("{0:#.##}", totalCommission - totalAccountCharges); 
            }

            
            param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

            param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsQuantity", totalAccountBookings.ToStr() + " account booking");
            param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGross", totalAccountBookingsCharges);
            param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdditionsAccountsGrossTotal", totalAccountBookingsCharges);        


            param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashQuantity", totalCashBooking + " cash booking");
            param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGross", totalCashBookingCharges);
            param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DeductionsCashGrossTotal", totalCashBookingCharges);

            param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAdditions", TotalSumOfAccountandCashJobTotal);
            param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDeductions",driverTotalCommission);
            param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", TotalSumOfAccountandCashJobTotal);
            param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceBFD", zeroStr);

            param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverOwed", driverOwedRate);            

            param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
            param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementType", this.StatementType.ToStr());
            param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionTotal", totalSettingsCommission);
            param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PickCommissionFromCharges", pickCommissionFromCharges);
            param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalExtraDrop", TotalExtraSum);

            param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_CommissionWise", (Commision).ToStr());


            param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_NoACComm", AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool() ? "1" : "0");


            reportViewer1.LocalReport.SetParameters(param);
            reportViewer1.LocalReport.SetParameters(param);


            this.Vu_BookingBaseBindingSource.DataSource = this.DataSource;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.ZoomMode= Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
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

        private void rptfrmDriverCommissionStatement_Load(object sender, EventArgs e)
        {

        }


        public void SendEmail()
        {

            General.ShowEmailForm(reportViewer1,"Driver Commission Report");

        }
     

     
    }
}
