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
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Taxi_AppMain
{
    public partial class rptfrmJobStatisticsReport : UI.SetupBase
    {
        private List<stp_JobStatisticsResult> _DataSource;

        public List<stp_JobStatisticsResult> DataSource
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
        string FromPostCode = null;
        string ToPostCode = null;
        int FromLocId = 0;
        int ToLocId = 0;
        int FromZoneId = 0;
        int ToZoneId = 0;
        DateTime? dtFrom = null;
        DateTime? dtTill = null;
        public rptfrmJobStatisticsReport(string PostCodeFrom, string PostCodeTo, int LocIdFrom, int LocIdTo, int ZoneIdFrom, int ZoneIdTo, DateTime FromDate, DateTime ToDate)
        {
            InitializeComponent();
            FromPostCode = PostCodeFrom;
            ToPostCode = PostCodeTo;
            FromLocId = LocIdFrom;
            ToLocId = LocIdTo;
            FromZoneId = ZoneIdFrom;
            ToZoneId = ZoneIdTo;
            dtFrom = FromDate;
            dtTill = ToDate;
        }

        private void rptfrmJobStatisticsReport_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadReport()
        {
            try
            {
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                int TotalJobs = 0;
                
                var list = (from a in new TaxiDataContext().stp_JobStatistics(FromPostCode, ToPostCode, FromLocId, ToLocId, FromZoneId, ToZoneId, dtFrom, dtTill)
                            select new stp_JobStatisticsResult
                            {
                                BookingNo=a.BookingNo,
                                PickupDateTime=a.PickupDateTime,
                                FromAddress=a.FromAddress,
                                ToAddress=a.ToAddress,
                                DriverName=a.DriverName,
                                VehicleType=a.VehicleType,
                                StatusName=a.StatusName
                                
                            }).ToList();
                this.stp_JobStatisticsResultBindingSource.DataSource = list;
                TotalJobs = list.Count();

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                string heading = string.Empty;
                if (dtFrom != null && dtTill != null)
                {
                    heading = string.Format("{0:dd/MM/yy}", dtFrom) + " to " + string.Format("{0:dd/MM/yy}", dtTill);
                }


                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_JobTillDate", heading);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalJobs", "Total Jobs : "+TotalJobs.ToStr());

                //param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_2", telNo);
                //param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_3", AppVars.objSubCompany.CompanyName.ToStr());

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
            saveFileDlg.FileName = "Job Statistics Report";

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
        private List<stp_JobStatisticsResult> GetData(string PostCodeFrom, string PostCodeTo, int LocIdFrom, int LocIdTo, int ZoneIdFrom, int ZoneIdTo, DateTime FromDate, DateTime ToDate)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_JobStatistics(FromPostCode, ToPostCode, FromLocId, ToLocId, FromZoneId, ToZoneId, dtFrom, dtTill)
                    select new stp_JobStatisticsResult
                    {
                        BookingNo = a.BookingNo,
                        PickupDateTime = a.PickupDateTime,
                        FromAddress = a.FromAddress,
                        ToAddress = a.ToAddress,
                        DriverName = a.DriverName,
                        VehicleType = a.VehicleType,
                        StatusName = a.StatusName
                    }).ToList();
        }
        public void EmailSending()
        {
            ReportHeading = "Controller Activity Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", dtFrom) + " to " + string.Format("{0:dd/MM/yyyy}", dtTill);
            DataSource = GetData(FromPostCode, ToPostCode, FromLocId, ToLocId, FromZoneId, ToZoneId, dtFrom.Value.ToDate(), dtTill.Value.ToDate());

            LoadReport();

            General.ShowEmailForm(reportViewer1, "Job Statistics Report"); ;
        }
    }
}
