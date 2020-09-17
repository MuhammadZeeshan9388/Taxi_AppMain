using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Telerik.WinControls.UI;
using Taxi_BLL;
using System.Collections;
using Taxi_Model;
using Utils;
using Taxi_AppMain.Classes;
using System.IO;

namespace Taxi_AppMain
{
    public partial class rptfrmCallHistoryReport : UI.SetupBase
    {
        public List<TempCallHistory> DataSource { get; set; }
        public string ReportHeading { get; set; }
        public rptfrmCallHistoryReport()
        {
            InitializeComponent();
        }

        private void rptfrmCallHistoryReport_Load(object sender, EventArgs e)
        {

           //LoadReport();
        }

        public void LoadReport()
        {
            try
            {
                  this.TempCallHistoryBindingSource.DataSource = this.DataSource;


                
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[7];

             

                
             
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", AppVars.objSubCompany.Address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TelephoneNo", AppVars.objSubCompany.TelephoneNo);

                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalAnsweredCalls", "Total Answered Calls : " + DataSource.Where(c => c.STN.Length > 0).Count());
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalMissedCalls", "Total Missed Calls : " + DataSource.Where(c => (c.STN == null || c.STN == "")).Count());
                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalBookings", "Total Bookings : " + 0);

                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", ReportHeading);

                //Report_Parameter_Telephone
                //param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalJobs", jobsDone);
                
                
                //List<ClsLogo> objLogo = new List<ClsLogo>();
                //objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                //ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                //this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                
                reportViewer1.LocalReport.SetParameters(param);




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
            saveFileDlg.FileName = "Call History Report";

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
}
