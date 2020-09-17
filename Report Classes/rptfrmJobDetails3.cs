using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Utils;
using System.IO;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;


namespace Taxi_AppMain
{
    public partial class rptfrmJobDetails3 : UI.SetupBase
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



        public void GenerateReport()
        {


            try
            {
                string website = AppVars.objSubCompany.WebsiteUrl.ToStr();
                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyLogo", path);



                string header ="     "+ AppVars.objSubCompany.Address.ToStr() + ",\n" + 
                            "       Telephone No:" + AppVars.objSubCompany.TelephoneNo;
                header += ", Fax:" + AppVars.objSubCompany.Fax.ToStr();

                header += "\n Email:" + AppVars.objSubCompany.EmailAddress.ToStr() + ", Website:" + AppVars.objSubCompany.WebsiteUrl;


                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", header);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyPhone",AppVars.objSubCompany.CompanyNumber.ToStr());

                reportViewer1.LocalReport.SetParameters(param);

                //ReportDataSource data = new ReportDataSource("vuBookingDetailBindingSource", this.DataSource);
                //this.reportViewer1.LocalReport.DataSources.Add(data);

                this.vuBookingDetailBindingSource.DataSource = this.DataSource;
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

        public void SendEmail(string bookingNo, string email)
        {

            General.ShowEmailForm(reportViewer1, "Job Print " + bookingNo, email);

        }
     

        public rptfrmJobDetails3()
        {
            InitializeComponent();
          


          
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
                    FileStream fs = new FileStream(saveFileDlg.FileName,FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }                

        }



       

        private void btnViewReport_Click(object sender, EventArgs e)
        {

          
        }

        private void rptfrmJobDetails_Load(object sender, EventArgs e)
        {
        
        }

        private void reportViewer1_LocationChanged(object sender, EventArgs e)
        {
             
        }

        private void reportViewer1_RenderingComplete(object sender, Microsoft.Reporting.WinForms.RenderingCompleteEventArgs e)
        {
        
      
        }

     

     
    }
}
