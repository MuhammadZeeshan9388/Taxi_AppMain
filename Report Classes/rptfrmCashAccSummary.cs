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
    public partial class rptfrmCashAccSummary :UI.SetupBase
    {
     
        public rptfrmCashAccSummary()
        {
           
            InitializeComponent();
         
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GenerateReport();
        
        }

        private void GenerateReport()
        {
            try
            {



            
                int companyId=ddl_Company.SelectedValue.ToInt();


                if (companyId == 0)
                {
                    ENUtils.ShowMessage("Please select Account");
                    return;
                }


                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateorNull();


                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);



                this.ClsCashAccSummaryBindingSource.DataSource = GetDataSource(companyId, fromDate, tillDate);
                 Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[7];

                string address = AppVars.objSubCompany.Address.ToStr().Replace("\r\n"," ").Trim();
                string landline = AppVars.objSubCompany.TelephoneNo.ToStr();
                string mobile = AppVars.objSubCompany.EmergencyNo.ToStr();

                string account = ddl_Company.Text.Trim();
                decimal totalAmount = (ClsCashAccSummaryBindingSource.DataSource as List<ClsCashAccSummary>).Sum(c => c.Amount).ToDecimal();
          
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Account", account);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAmount", totalAmount.ToStr());

                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Landline", landline);
                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Mobile", mobile);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Date",String.Format("{0:dd-MMM-yy}",DateTime.Now));
         

                reportViewer1.LocalReport.SetParameters(param);

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {




            }
        }


        private List<ClsCashAccSummary> GetDataSource(int companyId, DateTime? fromDate, DateTime? tillDate)
        {

            var list = (from a in General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.CompanyId == companyId
                                                                  && ((fromDate == null || c.PickupDateTime >= fromDate) && (tillDate == null || c.PickupDateTime <= tillDate)))
                        select new ClsCashAccSummary
                        {
                            CollectionDate=a.PickupDateTime,
                            CollectionTime=a.PickupDateTime,
                            Passenger=a.CustomerName,
                            Pickup=a.FromAddress,
                            Destination=a.ToAddress,
                            Price=a.FareRate,
                            CommissionRate =(a.FareRate >0) ?((a.DriverCommission*100)/a.FareRate):0,
                            Amount=a.DriverCommission
                        }).ToList();


            return list;
        }

        private void rptfrmDriverLoginHistory_Load(object sender, EventArgs e)
        {
            try
            {

                ComboFunctions.FillCompanyCombo(ddl_Company,c=>c.AccountTypeId==Enums.ACCOUNT_TYPE.CASH);

                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());


            }
            catch (Exception ex)
            {


            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            GenerateReport();
            ExportReport();
        }

        public void ExportReport()
        {
            try
            {



                if (this.ClsCashAccSummaryBindingSource.Count > 0)
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
                    saveFileDlg.FileName = "CashAccount";

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
               

            }
            catch (Exception ex)
            {


            }


        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                GenerateReport();
            
                if (this.ClsCashAccSummaryBindingSource.Count > 0)
                {
                    General.ShowEmailForm(reportViewer1, "Cash Account Report");
                }
               
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

    }
}
