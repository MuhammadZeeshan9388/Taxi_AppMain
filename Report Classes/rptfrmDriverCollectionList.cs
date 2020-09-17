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
using Taxi_AppMain.Classes;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace Taxi_AppMain
{
    public partial class rptfrmDriverCollectionList : UI.SetupBase
    {
        public rptfrmDriverCollectionList()
        {
            InitializeComponent();
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.btnExport.Click += new EventHandler(btnExport_Click);
            this.btnEmail.Click += new EventHandler(btnEmail_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnEmail_Click(object sender, EventArgs e)
        {
            DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
            DateTime? dtTo = dtpToDate.Value.ToDateorNull();
            string error = string.Empty;
            if (dtFrom == null)
            {
                if (string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : From Date";
            }

            if (dtTo == null)
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
            DataSource = GetData(dtFrom,dtTo);
            
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

        void btnPrint_Click(object sender, EventArgs e)
        {
            LoadReport();
        }
        bool IsReportLoaded = false;
        private List<stp_DriverCollectionListResult> _DataSource;

        public List<stp_DriverCollectionListResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        private void rptfrmDriverCollectionList_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Now.AddDays(-7);
            dtpToDate.Value = DateTime.Now;
        }
        public void LoadReport()
        {
            try
            {
                DateTime? dtFrom = dtpFromDate.Value.ToDateorNull();
                DateTime? dtTo = dtpToDate.Value.ToDateorNull();
                string error = string.Empty;
                if (dtFrom == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (dtTo == null)
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
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                //var list =
                this.stp_DriverCollectionListResultBindingSource.DataSource = (from a in new Taxi_Model.TaxiDataContext().stp_DriverCollectionList(dtFrom, dtTo)
                                                                           select new stp_DriverCollectionListResult
                                                                           {
                                                                               Id=a.Id,
                                                                               Account=a.Account,
                                                                               Commission=a.Commission,
                                                                               DriverName=a.DriverName,
                                                                               DriverNo=a.DriverNo,
                                                                               Total=a.Total,
                                                                               Weekly=a.Weekly
                                                                           }).ToList();



                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];

                // string address = AppVars.objSubCompany.Address;
                // string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                string heading = string.Empty;
                //if (dtFrom != null && dtTill != null)
                //{
               // heading = string.Format("{0:dd/MM/yy}", dtFrom) + " to " + string.Format("{0:dd/MM/yy}", dtTo);
                // }


                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_FromDate", string.Format("{0:dd/MM/yy}", dtFrom));
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_ToDate", string.Format("{0:dd/MM/yy}", dtTo));

                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
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

            saveFileDlg.FileName = "Driver Collection List Report";

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

        private List<stp_DriverCollectionListResult> GetData(DateTime? fromDate, DateTime? tillDate)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_DriverCollectionList(fromDate, tillDate)
                    select new stp_DriverCollectionListResult
                    {
                        Id = a.Id,
                        Account = a.Account,
                        Commission = a.Commission,
                        DriverName = a.DriverName,
                        DriverNo = a.DriverNo,
                        Total = a.Total,
                        Weekly = a.Weekly

                    }).ToList();
        }


        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Driver Collection List Report");
        }

    }
}
