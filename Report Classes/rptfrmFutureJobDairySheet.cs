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
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class rptfrmFutureJobDiarySheet : UI.SetupBase
    {
        private List<stp_FutureJobDiarySheetResult> _DataSource;

        public List<stp_FutureJobDiarySheetResult> DataSource
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

        bool IsReportLoded = false;
        public rptfrmFutureJobDiarySheet()
        {
            InitializeComponent();
            FillCombo();
            this.btnExportToExcel.Click += new EventHandler(btnExportToExcel_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FillCombo()
        {
            ComboFunctions.FillCompanyCombo(ddlCompany);
        }
        
        void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (IsReportLoded)
            {
                ExportReport("excel");
            }
            else
            {
                LoadReport();
                ExportReport("excel");
            }
        }
        public void ExportReport(string exportTo)
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            if (exportTo.ToLower() == "pdf")
                saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            else
                saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Future Job Diary Sheet Report";


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
                    // MessageBox.Show(ex.Message);
                }
            }
        }
        private void rptfrmFutureJobDairySheet_Load(object sender, EventArgs e)
        {
            LoadDates();
           
        }
        public void LoadDates()
        {
            fromDate.Value = DateTime.Now.ToDate();
            tillDate.Value = DateTime.Now.AddDays(1).ToDate();
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
                
                string error = string.Empty;


                if (fromDates == null)
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
                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }

                TimeSpan tillTime = TimeSpan.Zero;
                TimeSpan.TryParse("23:59:59", out tillTime);

                tillDates = tillDates + tillTime;



                this.reportViewer1.LocalReport.EnableExternalImages = true;
                UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);

                if (objTemplate == null)
                {
                    ENUtils.ShowMessage("Report Template is not defined in Settings");
                    return;
                }
                
                string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";
               
                if (objTemplate.TemplateName.ToStr() == "Template1")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns." + "rptFutureJobDiarySheet.rdlc";
                }
                else if (objTemplate.TemplateName.ToStr() == "Template2")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptFutureJobDiarySheet.rdlc";
                    //rptDriverCommisionExpenses
                }
                

                var list =new TaxiDataContext().stp_FutureJobDiarySheet(fromDates, tillDates,ddlCompany.SelectedValue.ToInt()).ToList();
              
                this.stp_FutureJobDiarySheetResultBindingSource.DataSource = list;









                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[0];
                if (objTemplate.TemplateName.ToStr() == "Template2")
                {
                    param = new Microsoft.Reporting.WinForms.ReportParameter[5];

                    string address = AppVars.objSubCompany.Address;
                    string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
                    string heading = string.Empty;
                    if (fromDates != null && tillDates != null)
                    {
                        heading = string.Format("{0:dd/MM/yy HH:mm}", fromDates) + " to " + string.Format("{0:dd/MM/yy HH:mm}", tillDates);
                    }
                    param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
                    param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);

                    this.ReportHeading = heading;

                    param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalJobs", list.Count.ToStr());
                    param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Heading", heading);

                    param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());
                }
                //List<ClsLogo> objLogo = new List<ClsLogo>();
                //objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                //ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                //this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                reportViewer1.LocalReport.SetParameters(param);
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
                IsReportLoded = true;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (IsReportLoded == true)
            {
                ExportReport();
            }
            else
            {
                LoadReport();
                ExportReport();
            }
        }
        private List<stp_FutureJobDiarySheetResult> GetData(DateTime? fromDates, DateTime? tillDates)
        {
            return (from a in new Taxi_Model.TaxiDataContext().stp_FutureJobDiarySheet(fromDates, tillDates,ddlCompany.SelectedValue.ToInt())
                    select new stp_FutureJobDiarySheetResult
                    {
                        DriverNo = a.DriverNo,
                        PickupDateTime=a.PickupDateTime,
                        PickUpDate=a.PickUpDate,
                        CustomerName=a.CustomerName,
                        AcceptedDateTime=a.AcceptedDateTime,
                        FromAddress=a.FromAddress,
                        ToAddress=a.ToAddress,
                        BookingDate=a.BookingDate,
                        CompanyName=a.CompanyName,
                        CompanyPrice=a.CompanyPrice

                    }).ToList();
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
            saveFileDlg.FileName = "Future Jobs Diary Sheet Report";

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

        private void radViewReport_Click(object sender, EventArgs e)
        {
            LoadReport();
        }


        
        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Future Jobs Diary Sheet Report");
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            DateTime? fromDates = fromDate.Value.ToDate();
            DateTime? tillDates = tillDate.Value.ToDate();

            string error = string.Empty;


            if (fromDates == null)
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
            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;
            }
            // ReportHeading = "Controller Activity Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDates) + " to " + string.Format("{0:dd/MM/yyyy}", tillDates);
            DataSource = GetData(fromDates, tillDates);

           //LoadReport();
            // frm.LoadReport();

            SendEmail();
        }

      
    }
}
