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
using Microsoft.Reporting.WinForms;


namespace Taxi_AppMain
{
    public partial class rptfrmCallStatisticsReport : UI.SetupBase
    {
        bool IsReportLoaded = false;
        public rptfrmCallStatisticsReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(rptfrmCallStatisticsReport_Load);
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnExportPDF.Click += new EventHandler(btnExportPDF_Click);
        }

        void btnExportPDF_Click(object sender, EventArgs e)
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

        void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }

        void rptfrmCallStatisticsReport_Load(object sender, EventArgs e)
        {
            LoadDates();
        }

        public void LoadReport()
        {
            try
            {
                
                if (dtpFromDate.Value != null && dtpFromDate.Value.Value.Year == 1753)
                    dtpFromDate.Value = null;

                if (dtpTillDate.Value != null && dtpTillDate.Value.Value.Year == 1753)
                    dtpTillDate.Value = null;

                DateTime? fromDates = dtpFromDate.Value.ToDateorNull();
                DateTime? tillDates = dtpTillDate.Value.ToDateorNull();

                int HourRange = numHourRange.Value.ToInt();
                if (HourRange == 0 || HourRange>24)
                {
                    HourRange = 2;
                    numHourRange.Value = HourRange;
                }


                string Error = "";
                if (fromDates == null)
                {
                    Error = "Required : From Date";
                }
                if (tillDates == null)
                {
                    if (string.IsNullOrEmpty(Error))
                    {
                        Error = "Required : Till Date";
                    }
                    else
                    {
                        Error += Environment.NewLine + "Required : Till Date";
                    }
                }
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                if (fromDates != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    fromDates = (fromDates.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



                if (tillDates != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    tillDates = (tillDates.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();



                this.reportViewer1.LocalReport.EnableExternalImages = true;
               
                var list = (from a in GeneralBLL.GetQueryable<CallHistory>(null)
                            where (a.CallDateTime >= fromDates)
                            && (a.CallDateTime <= tillDates)
                            orderby a.CallDateTime descending
                            //where (a.AnsweredDateTime.Value.Date >= fromDates)
                            //&& (a.AnsweredDateTime.Value.Date <= tillDates)
                            //orderby a.AnsweredDateTime descending
                            select new
                            {
                                CallDateTime = a.CallDateTime,
                            }).ToList();




                List<NewCallHistory> objCallHistory = new List<NewCallHistory>();

                List<NewCallHistory> objNewCallHistory = new List<NewCallHistory>();

                DateTime dtFrom = DateTime.Now.ToDate();
                DateTime dtTo = DateTime.Now.ToDate();

                int GroupHours = 24;
                string HoursDuration = "";
                
                var grouplist = list.GroupBy(c => c.CallDateTime.ToDate()).ToList(); ;

                foreach (var item in grouplist)
                {
                    int i = 0;
                    dtFrom = item.Key.ToDate();
                    dtTo = item.Key.ToDate();

                    for (; i < GroupHours; i += HourRange)
                    {
                        if (i == 0)
                        {
                            dtTo = item.Key.AddHours(HourRange);
                        }
                        else
                        {
                            dtFrom = item.Key.AddHours(i);
                            
                            dtTo = item.Key.AddHours((i + HourRange));
                            if (dtFrom.Date != dtTo.Date)
                            {
                                dtTo = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day, 23, 59, 59);
                            }
                        }
                        objCallHistory.Add(new NewCallHistory { FromTime = dtFrom, ToTime = dtTo });
                    }
                }

                foreach (var item in objCallHistory)
                {
                    dtFrom = item.FromTime.ToDateTime();
                    dtTo = item.ToTime.ToDateTime();
                    var obj = list.Where(c=>c.CallDateTime>=item.FromTime && c.CallDateTime<=item.ToTime).ToList();;
                    if (obj.Count > 1)
                    {
                        objNewCallHistory.RemoveAll(c => c.FromTime >= dtFrom && c.FromTime <= dtTo);  
                    }
                    if (obj.Count > 0)
                    {
                        HoursDuration = string.Format("{0:HH:mm}", dtFrom) + " to " + string.Format("{0:HH:mm}", dtTo);
                        objNewCallHistory.Add(new NewCallHistory { FromTime = dtFrom, ToTime = dtTo, Id = obj.Count, CalledToNumber = HoursDuration,CallDateTime=dtFrom.ToDate() });
                    }
                }


                if (chkIncludeZeroCalls.Checked)
                {
                    foreach (var item in grouplist)
                    {
                        int i = 0;
                        dtFrom = item.Key.ToDateTime();
                        dtTo = item.Key.ToDateTime();

                        for (; i < GroupHours; i += HourRange)
                        {
                            if (i == 0)
                            {
                                dtTo = item.Key.AddHours(HourRange);
                            }
                            else
                            {
                                dtFrom = item.Key.AddHours(i);
                                dtTo = item.Key.AddHours((i + HourRange));
                                if (dtFrom.Date != dtTo.Date)
                                {
                                    dtTo = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day, 23, 59, 59);
                                }
                            }
                            if (objNewCallHistory.Count(c => c.FromTime == dtFrom && c.ToTime == dtTo) == 0)
                            {
                                HoursDuration = string.Format("{0:HH:mm}", dtFrom) + " to " + string.Format("{0:HH:mm}", dtTo);
                                objNewCallHistory.Add(new NewCallHistory { FromTime = dtFrom, ToTime = dtTo, Id = 0, CalledToNumber = HoursDuration, CallDateTime = dtFrom.ToDate() });
                            }
                        }
                    }
                }


                
                objNewCallHistory = objNewCallHistory.OrderBy(c => c.FromTime).ToList();
                this.CallHistoryBindingSource.DataSource = objNewCallHistory;


                
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];

                string heading = string.Empty;
                if (fromDates != null && tillDates != null)
                {
                    heading = string.Format("{0:dd/MM/yy HH:mm}", fromDates) + " to " + string.Format("{0:dd/MM/yy HH:mm}", tillDates);
                }

                
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterHeader", AppVars.objSubCompany.CompanyName);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", AppVars.objSubCompany.Address);
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", AppVars.objSubCompany.TelephoneNo);

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
                IsReportLoaded = true;

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        public void LoadDates()
        {
            dtpFromDate.Value = DateTime.Now.GetStartOfCurrentWeek();
            dtpTillDate.Value = DateTime.Now.GetEndOfCurrentWeek();
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
            saveFileDlg.FileName = "Call Statistics Report";

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    public class NewCallHistory
    {
        public string CalledToNumber { get; set; }
        public DateTime? AnsweredDateTime { get; set; }
        public int Id { get; set; }
        public DateTime? CallDateTime { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }

    }
}
