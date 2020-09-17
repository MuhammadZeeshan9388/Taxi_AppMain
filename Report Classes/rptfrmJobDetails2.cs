using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Utils;
using Taxi_Model;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class rptfrmJobDetails2 :UI.SetupBase
    {

        private List<Vu_BookingDetail> _DataSource;

        public List<Vu_BookingDetail> DataSource
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


        public void SendEmail(string bookingNo, string email)
        {

            General.ShowEmailForm(reportViewer1, "Job Print " + bookingNo, email);
        }
     



        public void GenerateReport()
        {


            try
            {
                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[6];



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyLogo", path);


                string vehicleInformation = "Thankyou for agreeing to cover this booking on our behalf. Please check the details and let us know if any part is" +
                          " unclear. This job requires a " + this.DataSource.FirstOrDefault().DefaultIfEmpty().VehicleType.ToStr() + " CAR " +
                          "or similar vehicle. Our Booking Number must be quoted on your Invoice";



                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VehicleInformation", vehicleInformation);

                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyEmail", AppVars.objSubCompany.EmailAddress.ToStr());
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyFax", AppVars.objSubCompany.Fax.ToStr());
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyTel", AppVars.objSubCompany.TelephoneNo.ToStr());
                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyAddress", AppVars.objSubCompany.Address.ToStr());


                reportViewer1.LocalReport.SetParameters(param);
           

                this.Vu_BookingDetailBindingSource.DataSource = this.DataSource;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
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
            saveFileDlg.FileName = "BookingReport";

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


        public rptfrmJobDetails2()
        {
            InitializeComponent();
        }

        private void rptfrmJobDetails2_Load(object sender, EventArgs e)
        {

        }
    }
}
