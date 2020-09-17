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
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.Enumerations;
using System.IO;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class rptfrmIncomeSummaryReport : UI.SetupBase
    {
        public List<stp_GetIncomeSummaryResult> DataSource { get; set; }
        public string Period { get; set; }
        public rptfrmIncomeSummaryReport()
        {
            InitializeComponent();
        }

        private void rptfrmIncomeSummaryReport_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadReport()
        {
            try
            {
                List<TempIncomeSummary> objTempIncomeSummary = new List<TempIncomeSummary>();
                foreach (var item in this.DataSource)
                {
                    objTempIncomeSummary.Add(new TempIncomeSummary { Id = item.Id, InvId = item.InvId.ToLong(), Description = item.Description, InvoiceDate = item.InvoiceDate.ToDateTime(), Income = item.Income.ToDecimal(), Expense = item.Expense.ToDecimal(), Header = AppVars.objSubCompany.CompanyName, Address = AppVars.objSubCompany.Address , Period = this.Period });

                }
                
                this.TempIncomeSummaryBindingSource.DataSource = objTempIncomeSummary;

                //Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                //string heading = string.Empty;
                //// heading = "For the Period " + string.Format("{0:dd/MM/yyyy}", dtFrom) + " to " + string.Format("{0:dd/MM/yyyy}", dtTill);
                ////heading = "From: "+string.Format("{0:dd/MM/yyyy}", dtFrom) + " To: " + string.Format("{0:dd/MM/yyyy}", dtTill);
                //// string EndDate = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                ////string To = string.Format("{0:dd/MM/yyyy}", dtTill);
                //param[0] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterHeader", AppVars.objSubCompany.CompanyName);
                //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterCompanyAddress", AppVars.objSubCompany.Address);
                //param[2] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterPeriod", Period);
                //// reportViewer1.LocalReport.ReportPath = "Taxi_AppMain.ReportDesigns.rptIncomeSummaryReport.rdlc";
                //////List<ClsLogo> objLogo = new List<ClsLogo>();
                //////objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                //////ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                //////this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                //reportViewer1.LocalReport.SetParameters(param);
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                // IsReportLoaded = true;
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

            saveFileDlg.FileName = "Income Summary Report";

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
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Income Summary Report");
        }
    }
}
