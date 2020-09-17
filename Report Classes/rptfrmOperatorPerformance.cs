using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using Telerik.WinControls.UI.Export;
using Telerik.Data;

namespace Taxi_AppMain
{
    public partial class rptfrmOperatorPerformance : UI.SetupBase
    {
        bool IsReportLoaded = false;
        private List<stp_OperatorPerformanceResult> _DataSource;

        public List<stp_OperatorPerformanceResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        public rptfrmOperatorPerformance()
        {
            InitializeComponent();
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.btnExport.Click += new EventHandler(btnExport_Click);
            this.btnEmail.Click += new EventHandler(btnEmail_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            dtpFromDate.Value = DateTime.Now.GetStartOfCurrentWeek();
            dtpTillDate.Value = DateTime.Now.ToDate();
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();
                if (fromDate > tillDate)
                {
                    ENUtils.ShowMessage("From Date can't greater then Till Date");
                    return;
                }
                string error = string.Empty;


                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";
                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }
                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);

                tillDate=tillDate + tillTime;
               
                DataSource = GetData(fromDate, tillDate);

                SendEmail();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
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

        void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void rptfrmOperatorPerformance_Load(object sender, EventArgs e)
        {

        }
        public void LoadReport()
        {
            try
            {
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);
                DateTime? dtFromDate = dtpFromDate.Value.ToDate();
                DateTime? dtTillDate = dtpTillDate.Value.ToDate() + tillTime;
                if (dtFromDate > dtTillDate)
                {
                    ENUtils.ShowMessage("From Date can't greater then Till Date");
                    return;
                }

                this.stp_OperatorPerformanceResultBindingSource.DataSource = GetData(dtFromDate, dtTillDate).ToList();
                                                                              //select new stp_OperatorPerformanceResult
                                                                              //{
                                                                              //    Id = a.Id,
                                                                              //    CLICalls = a.CLICalls,
                                                                              //    CLICleared = a.CLICleared,
                                                                              //    JobsBooked = a.JobsBooked,
                                                                              //    JobsCancelled = a.JobsCancelled,
                                                                              //    JobsCompletetd = a.JobsCompletetd,
                                                                              //    LateDespatch = a.LateDespatch,
                                                                              //    LoggedIn = a.LoggedIn,
                                                                              //    MailSent = a.MailSent,
                                                                              //    UserName = a.UserName
                                                                              //}).ToList();



                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];

                // string address = AppVars.objSubCompany.Address;
                // string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                string heading = string.Empty;
                //if (dtFrom != null && dtTill != null)
                //{
                heading = "Operator Performance for period " + string.Format("{0:dd/MM/yy}", dtpFromDate.Value) + " to " + string.Format("{0:dd/MM/yy}", dtpTillDate.Value);
                // }
                string PrintedOn = string.Empty;
                PrintedOn = "Printed on " + string.Format("{0:dd/MM/yy}", DateTime.Now) + " at " + string.Format("{0:HH:mm}", DateTime.Now);
                //Printed on 22/04/2016 at 16:40

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PrintedOn", PrintedOn);

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

            saveFileDlg.FileName = "Operator Performance Report";

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
        private List<stp_OperatorPerformanceResult> GetData(DateTime? fromDate, DateTime? tillDate)
        {
            return new Taxi_Model.TaxiDataContext().stp_OperatorPerformance(fromDate, tillDate).ToList();
             
        }


        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Operator Performance Report");
        }


    }
}
