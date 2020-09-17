using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Taxi_Model;
using Utils;

namespace Taxi_AppMain
{
    public partial class rptfrmJobListViewer : UI.SetupBase
    {

        
        private List<Vu_BookingBase> _DataSource;

        public List<Vu_BookingBase> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
      
        public List<stp_GetBookingBaseResult> DataSourceBySP { get; set; }

        private string _ReportHeading;

        public string ReportHeading
        {
            get { return _ReportHeading; }
            set { _ReportHeading = value; }
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
            saveFileDlg.FileName = "JobsList";

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



        public void ExportReportToExcel()
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
            saveFileDlg.FileName = "JobsList";

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

        private string _TemplateValue;

        public string TemplateValue
        {
            get { return _TemplateValue; }
            set { _TemplateValue = value; }
        }

        public void GenerateReport()
        {
            if (this.TemplateValue.ToStr().Trim().Length == 0)
                return;
           

            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns." + this.TemplateValue;



            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];

            string address = AppVars.objSubCompany.Address;
            string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;
          param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);

            string heading = this.ReportHeading;

            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalJobs", this.DataSourceBySP.Count.ToStr());
            // param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalJobs", this.DataSource.Count.ToStr());
            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Heading", heading);

            param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());

            reportViewer1.LocalReport.SetParameters(param);

            this.Vu_BookingBaseBindingSource.DataSource = this.DataSourceBySP;
           // this.Vu_BookingBaseBindingSource.DataSource = this.DataSource;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }



        public rptfrmJobListViewer()
        {
            InitializeComponent();
        }

        private void rptfrmJobListViewer_Load(object sender, EventArgs e)
        {

        }

        private void btnSaveOn_Click(object sender, EventArgs e)
        {
            try
            {
               // Vu_BookingBaseBindingSource.
              //  invcedt.Receipt.Columns[ddl_dt.Text].SetOrdinal(Convert.ToInt32(num.Value));
            }
            catch (Exception)
            { }
        }

        private void uC_JobListMain1_Load(object sender, EventArgs e)
        {

        }
    }
}
