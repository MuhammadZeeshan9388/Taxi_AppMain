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
    public partial class rptfrmDriverLoginHour : UI.SetupBase
    {
        bool IsReportLoaded = false;
        public rptfrmDriverLoginHour()
        {
            InitializeComponent();
            this.btnViewReport.Click += new EventHandler(btnViewReport_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnExport.Click += new EventHandler(btnExport_Click);
            this.btnEmail.Click += new EventHandler(btnEmail_Click);
        
        }

        void btnEmail_Click(object sender, EventArgs e)
        {
            DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
            DateTime? dtTill = dtpTillDate.Value.ToDateorNull();
            string Error = string.Empty;
            if (dtFrom == null)
            {
                Error = "Required: From Date";
            }
            if (dtTill == null)
            {
                if (string.IsNullOrEmpty(Error))
                {
                    Error = "Required: To Date";
                }
                else
                {
                    Error += Environment.NewLine + "Required: To Date";
                }
            }
            if (!string.IsNullOrEmpty(Error))
            {
                ENUtils.ShowMessage(Error);
                return;
            }
            LoadReport();
            SendEmail();
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            if (IsReportLoaded)
            {
                ExportReport();
            }
            else
            {
                LoadReport();
                ExportReport();
            }
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnViewReport_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        private void DefaultDate()
        {
            dtpFromDate.Value = DateTime.Now.AddDays(-7);
            dtpTillDate.Value = DateTime.Now;
        }
        private void rptfrmPaymentCollection_Load(object sender, EventArgs e)
        {
            DefaultDate();
        }
        public void LoadReport()
        {
            try
            {
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTill = dtpTillDate.Value.ToDateorNull();
                string Error = string.Empty;
                if (dtFrom == null)
                {
                    Error = "Required: From Date";
                }
                if (dtTill == null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required: To Date";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required: To Date";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                this.reportViewer1.LocalReport.EnableExternalImages = true;
                var list=(new Taxi_Model.TaxiDataContext().stp_GetDriverLoginHour(dtFrom,dtTill+ TimeSpan.Parse("23:59:59"))).ToList();
                var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                this.stp_GetDriverLoginHourResultBindingSource.DataSource = list2;
                
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];
                string heading = string.Empty;
                heading = "For the Period " + string.Format("{0:dd/MM/yyyy}", dtFrom) + " to " + string.Format("{0:dd/MM/yyyy}", dtTill);
                //heading = "From: "+string.Format("{0:dd/MM/yyyy}", dtFrom) + " To: " + string.Format("{0:dd/MM/yyyy}", dtTill);
               // string EndDate = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                //string To = string.Format("{0:dd/MM/yyyy}", dtTill);
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_LoginPeriod", heading);
                //List<ClsLogo> objLogo = new List<ClsLogo>();
                //objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                //ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                //this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                reportViewer1.LocalReport.SetParameters(param);
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                IsReportLoaded = true;
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

            saveFileDlg.FileName = "Driver Login Hours Report";

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
            General.ShowEmailForm(reportViewer1, "Driver Login Hours Report");
        }
        public void ExportReportToExcel(string exportTo)
        {



            SaveFileDialog saveFileDlg = new SaveFileDialog();

            //if (exportTo.ToLower() == "pdf")
            //    saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            //else
            saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Driver Payment Collection Report";


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
    }
}
