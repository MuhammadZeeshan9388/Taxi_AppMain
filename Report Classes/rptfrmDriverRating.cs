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


namespace Taxi_AppMain
{
    public partial class rptfrmDriverRating :UI.SetupBase
    {
        int DriverId = 0; 
        public rptfrmDriverRating(int ID)
        {
            DriverId = ID;
            InitializeComponent();
            GenerateReportLoad();
            
        
        }

        private void GenerateReportLoad()
        {
            try
            {
                var today = DateTime.Today;
                var month = new DateTime(today.Year, today.Month, 1);
                var first = month.AddMonths(-2);
                var last = month.AddDays(-2);
                               
                
                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateorNull();


                var list = General.GetQueryable<Fleet_Driver>(c => c.Id == DriverId).ToList();

                string Driver = list.FirstOrDefault().DriverNo + " - " + list.FirstOrDefault().DriverName;

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    this.stp_DriverRatingResultBindingSource.DataSource = db.stp_DriverRating(first.ToDateTime(), DateTime.Today, DriverId).ToList().OrderByDescending(c => c.UpdatedOn);
                }

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);


                string heading = string.Empty;
                if (fromDate != null && tillDate != null)
                {
                    heading = string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                }
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", heading);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Driver", Driver);
                reportViewer1.LocalReport.SetParameters(param);



                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {




            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            GenerateReport();
        
        }

        private void GenerateReport()
        {
            try
            {
                             

                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateorNull();


                var list = General.GetQueryable<Fleet_Driver>(c => c.Id == DriverId).ToList();

                string Driver = list.FirstOrDefault().DriverNo + " - " + list.FirstOrDefault().DriverName;

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    this.stp_DriverRatingResultBindingSource.DataSource = db.stp_DriverRating(fromDate, tillDate, DriverId).ToList().OrderByDescending(c => c.UpdatedOn);
                }

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);
                

                string heading = string.Empty;
                if (fromDate != null && tillDate != null)
                {
                    heading =  string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                }
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria", heading);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Driver", Driver);
                reportViewer1.LocalReport.SetParameters(param);



                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {




            }
        }

        private void rptfrmDriverRating_Load(object sender, EventArgs e)
        {
            try
            {

                

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
            saveFileDlg.FileName = "DriverBreakHistory";

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
