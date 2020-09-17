using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using Utils;
using System.IO;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using System.Collections;
using System.Web.UI.WebControls;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class rptfrmCustomerAppUsersReport : UI.SetupBase
    {
        bool IsReportLoaded = false;

        private List<stp_AppUsersResult> _DataSource;

        public List<stp_AppUsersResult> DataSource
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


        public rptfrmCustomerAppUsersReport()
        {
            InitializeComponent();
 
            //this.btnPrint.Click += new EventHandler(btnPrint_Click);
            //btnEmail.Click += new EventHandler(btnEmail_Click);
        }

        public void ExportReportToExcel()
        {
            try
                {
            Microsoft.Reporting.WinForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = reportViewer1.LocalReport.Render(
             "Excel", null, out mimeType, out encoding,
              out extension,
             out streamids, out warnings);


            SaveFileDialog saveFileDlg = new SaveFileDialog();
            saveFileDlg.Filter = "Excel File (*.xls)|*.xls";
            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "App User Report";

            //   saveileDlg.RestoreDirectory = false;
            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {


                
                    FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();


                }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        public void ExportReport()
        {         

            try
            {
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            //if (exportTo.ToLower() == "pdf")
            //    saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            //else
            saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "App Users Report";

            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {

                    FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);

                    Microsoft.Reporting.WinForms.Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] bytes = reportViewer1.LocalReport.Render(
                    "pdf", null, out mimeType, out encoding,
                      out extension,
                     out streamids, out warnings);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
               
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
           


        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "App Users Report");
        }

        public void GenerateReport()
        {
            
           // this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns." + this.TemplateValue;

            this.stp_AppUsersResultBindingSource.DataSource = _DataSource;

            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];

            string address = AppVars.objSubCompany.Address;
            string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

            string heading = this.ReportHeading;

            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeading", AppVars.objSubCompany.CompanyName);
            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", AppVars.objSubCompany.Address);
            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterPeriod", heading);

            reportViewer1.LocalReport.SetParameters(param);
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
            IsReportLoaded = true;
        }

    
        public void ExportReportToExcel(string exportTo)
        {

            SaveFileDialog saveFileDlg = new SaveFileDialog();

            //if (exportTo.ToLower() == "pdf")
            //    saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            //else
            saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "App Users Report";

            if (saveFileDlg.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);

                    Microsoft.Reporting.WinForms.Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    byte[] bytes = reportViewer1.LocalReport.Render(
                     exportTo.ToLower(), null, out mimeType, out encoding,
                      out extension,
                     out streamids, out warnings);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void rptfrmJobsList_Load(object sender, EventArgs e)
        {
            //dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).Date;

            //TimeSpan tillTime = TimeSpan.Zero;

            //TimeSpan.TryParse("23:59:59", out tillTime);

            //dtpToDate.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue()).Date);

            //dtptilltime.Value = dtpToDate.Value.Value.Date + tillTime;

        }

        private List<object[]> _listofData;

        public List<object[]> ListofData
        {
            get { return _listofData; }
            set { _listofData = value; }
        }


   
    }
}
