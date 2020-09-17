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
    public partial class frmPlotStatistics : UI.SetupBase
    {
        private List<stp_PlotStatisticsResult> _DataSource;

        public List<stp_PlotStatisticsResult> DataSource
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

        public frmPlotStatistics()
        {
            InitializeComponent();
        }

        private void frmPlotStatistics_Load(object sender, EventArgs e)
        {

          //  this.reportViewer1.RefreshReport();
            LoadDates();
        }


        public void LoadReport()
        {
            try
            {

                if (fromDate.Value != null && fromDate.Value.Value.Year == 1753)
                    fromDate.Value = null;

                if (tillDate.Value != null && tillDate.Value.Value.Year == 1753)
                    tillDate.Value = null;

                DateTime? fromDates = fromDate.Value.ToDate();
                DateTime? tillDates = tillDate.Value.ToDate();


                this.reportViewer1.LocalReport.EnableExternalImages = true;

               
                var list = (from a in new TaxiDataContext().stp_PlotStatistics(fromDates, tillDates)
                            select new stp_PlotStatisticsResult
                            {
                                ZoneName = a.ZoneName,
                                TotalPicked = a.TotalPicked,
                                TotalDroped = a.TotalDroped,
                                OrderNo=a.OrderNo
                            }).ToList();
                this.stp_PlotStatisticsResultBindingSource.DataSource = list;
                              
               





                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];

                string heading = string.Empty;
                if (fromDates != null && tillDates != null)
                {
                    heading = string.Format("{0:dd/MM/yy HH:mm}", fromDates) + " to " + string.Format("{0:dd/MM/yy HH:mm}", tillDates);
                }

                //string totalDrivers = list.Count.ToStr();
                //string jobsDone = list.Sum(c => c.JobsDone).ToStr();

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);
                //param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalDriver", totalDrivers);
                //param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalJobs", jobsDone);
                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                reportViewer1.LocalReport.SetParameters(param);




                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();

            }
            catch (Exception ex)
            {

            }
        }
        public void LoadDates()
        {
            fromDate.Value = DateTime.Now.GetStartOfCurrentWeek();
            tillDate.Value = DateTime.Now.GetEndOfCurrentWeek();
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
            saveFileDlg.FileName = "Plot Statistics Report";

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
        //public void LoadReport()
        //{
        //    try
        //    {

        //        if (fromDate.Value != null && fromDate.Value.Value.Year == 1753)
        //            fromDate.Value = null;

        //        if (tillDate.Value != null && tillDate.Value.Value.Year == 1753)
        //            tillDate.Value = null;



        //        DateTime? fromDates = fromDate.Value;
        //        DateTime? tillDates = tillDate.Value;


        //        this.AdminActivity.LocalReport.EnableExternalImages = true;
        //        var list =
        //        this.stp_PlotStatisticsResultBindingSource.DataSource = (from a in new Taxi_Model.TaxiDataContext().stp_PlotStatistics(fromDates,tillDates)
        //                                                                   select new stp_PlotStatisticsResult
        //                                                                   {
        //                                                                       ZoneName=a.ZoneName,
        //                                                                       TotalPicked=a.TotalPicked,
        //                                                                       TotalDroped=a.TotalDroped,
        //                                                                       OrderNo=a.OrderNo
                                                                               

        //                                                                   }).ToList();



        //        Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];

        //        //string address = AppVars.objSubCompany.Address;
        //        //string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
        //        string heading = string.Empty;
        //        if (fromDates != null && tillDates != null)
        //        {
        //            heading = string.Format("{0:dd/MM/yy HH:mm}", fromDates) + " to " + string.Format("{0:dd/MM/yy HH:mm}", tillDates);
        //        }


        //        param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);
             
        //        List<ClsLogo> objLogo = new List<ClsLogo>();
        //        objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
        //        ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
        //        this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
        //        reportViewer1.LocalReport.SetParameters(param);



        //        this.reportViewer1.ZoomPercent = 100;
        //        this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
        //        this.reportViewer1.RefreshReport();
        //        // }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        private List<stp_PlotStatisticsResult> GetData(DateTime? fromDates, DateTime? tillDates)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_PlotStatistics(fromDates, tillDates)
                    select new stp_PlotStatisticsResult
                    {
                        ZoneName=a.ZoneName,
                        TotalPicked=a.TotalPicked,
                        TotalDroped=a.TotalDroped,
                        OrderNo=a.OrderNo
                        
                    }).ToList();
        }
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Plot Statistics Report");
        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            DateTime? fromDates = fromDate.Value.ToDate();
            DateTime? tillDates = tillDate.Value.ToDate();

            string error = string.Empty;


            if (fromDate == null)
            {
                if (string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : From Date";
            }

            if (tillDates == null)
            {
                if (string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : To Date";
            }

            ReportHeading = "Controller Activity Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDates) + " to " + string.Format("{0:dd/MM/yyyy}", tillDates);
            DataSource = GetData(fromDates, tillDates);

            LoadReport();
            
            SendEmail();
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportReport();
        }
        
    }
}
