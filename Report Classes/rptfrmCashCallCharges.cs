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
    public partial class rptfrmCashCallCharges : UI.SetupBase
    {
        private List<stp_CashCallChargesResult> _DataSource;

        public List<stp_CashCallChargesResult> DataSource
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

        public rptfrmCashCallCharges()
        {
            InitializeComponent();
        }

        private void rptfrmCashCallCharges_Load(object sender, EventArgs e)
        {
            loadDates();
        }
        private void loadDates()
        {
            dtpFrom.Value = DateTime.Now.GetStartOfCurrentWeek();
            dtpTill.Value = DateTime.Now.GetEndOfCurrentWeek();
        
        }

        public void LoadReport()
        {
            try
            {

                if (dtpFrom.Value != null && dtpFrom.Value.Value.Year == 1753)
                    dtpFrom.Value = null;

                if (dtpTill.Value != null && dtpTill.Value.Value.Year == 1753)
                    dtpTill.Value = null;



                DateTime? fromDate = dtpFrom.Value;
                DateTime? tillDate = dtpTill.Value;

                DateTime startTime = Convert.ToDateTime(fromDate.Value);
                DateTime endtime = Convert.ToDateTime(tillDate.Value);

                decimal Charges = 0;
                //var query = General.GetQueryable<Fleet_Driver_Availability>(c => c.DriverId == DriverId[0]).FirstOrDefault();
                var query = General.GetQueryable<Gen_SysPolicy_Configuration>(c => c.Id == 1).FirstOrDefault();
                if (query != null)
                {
                    Charges = query.CashCallCharges.ToDecimal();
                }

                this.reportViewer1.LocalReport.EnableExternalImages = true;
                var list = (from a in new Taxi_Model.TaxiDataContext().stp_CashCallCharges(fromDate, tillDate)
                            select new stp_CashCallChargesResult
                            {
                                DriverName=a.DriverName,
                                DriverNo=a.DriverNo,
                                TotalJobs=a.TotalJobs,
                                TotalAmount=a.TotalAmount*Charges

                            }).ToList();


                this.stp_CashCallChargesResultBindingSource.DataSource = list;
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];

                string address = AppVars.objSubCompany.Address;
                string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                string heading = string.Empty;
                if (fromDate != null && tillDate != null)
                {
                    heading = string.Format("{0:dd/MM/yy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yy HH:mm}", tillDate);
                }


                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", heading);

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

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            LoadReport();
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
            saveFileDlg.FileName = "Cash Call Report";

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
        private List<stp_CashCallChargesResult> GetData(DateTime? fromDate, DateTime? tillDate)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_CashCallCharges(fromDate, tillDate)
                    select new stp_CashCallChargesResult
                    {
                        DriverName = a.DriverName,
                        DriverNo = a.DriverNo,
                        TotalJobs = a.TotalJobs,
                        TotalAmount = a.TotalAmount

                    }).ToList();
        }
      
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportReport();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {


            DateTime? fromDate = dtpFrom.Value.ToDate();
            DateTime? tillDate = dtpTill.Value.ToDate();

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

          

            ReportHeading = "Cash Call Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            DataSource = GetData(fromDate, tillDate);

            LoadReport();
            
            SendEmail();
        }
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Cash Call Report");
        }



    }
}
