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
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;
namespace Taxi_AppMain
{
    public partial class rptfrmCashAccStatement : UI.SetupBase
    {

        private string reportType = string.Empty;

        public string ReportType
        {
            get { return reportType; }
            set { reportType = value; }
        }

        public rptfrmCashAccStatement()
        {
            InitializeComponent();
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



        public void GenerateReport()
        {


            reportViewer1.LocalReport.EnableExternalImages = true;

            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[8];

            string address = AppVars.objSubCompany.Address;
            string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);


            List<ClsLogo> objLogo = new List<ClsLogo>();
            objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
            ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
            this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

            string path = @"File:";
            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);


            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", this.DatePeriod);


            // Summary Calculations
            decimal JobsSum= this.DataSource.Sum(c => c.TotalCharges.ToDecimal());
            int jobsCnt = this.DataSource.Count;
            string jobsTotal = string.Format("{0:c}", JobsSum);
            jobsTotal = jobsTotal.Substring(1);
            jobsTotal = jobsTotal.Insert(0, "£ ");
            decimal zeroValue = 0.00m;
            string zeroStr = string.Format("{0:c}", zeroValue);
            zeroStr = zeroStr.Substring(1);
            zeroStr = zeroStr.Insert(0, "£ ");
           
         


            decimal totalCommission = this.DataSource.Sum(c =>c.DriverCommission).ToDecimal();
            string driverTotalCommission = string.Format("{0:c}", totalCommission);

            driverTotalCommission = driverTotalCommission.Substring(1);
            driverTotalCommission = driverTotalCommission.Insert(0, "£ ");


            string totalSettingsCommission = driverTotalCommission;


            if (reportType == "Detailed")
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptCashAccStatement.rdlc";
            }
            else
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptCashAccStatementSummary.rdlc";

               

            }

            
            param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsTotal", jobsTotal);

          
            param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobsCnt", jobsCnt.ToStr());
            param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionPercent", AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal().ToStr());
            param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionTotal", totalSettingsCommission);

            //

            
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
            saveFileDlg.FileName = "CashAccStatement";
          
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

            General.ShowEmailForm(reportViewer1,"Cash Account Statement Report");

        }

        private void rptfrmCashAccStatement_Load(object sender, EventArgs e)
        {

        }
     

     
    }
}
