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

namespace Taxi_AppMain
{
    public partial class rptfrmIncome : UI.SetupBase
    {
       


        private List<Vu_BookingBase> _DataSource;

        public List<Vu_BookingBase> DataSource
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
         
            

            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[5];

            string address = AppVars.objSubCompany.Address;
            string telNo ="Tel No. "+ AppVars.objSubCompany.TelephoneNo;

            string grandTotal =  this.DataSource.Sum(c => c.TotalCharges.ToDecimal()).ToStr();


            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);


            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_GrandTotal", grandTotal);
            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Heading", this.ReportHeading);

            param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_TotalJobs", this.DataSource.Count.ToStr());


            reportViewer1.LocalReport.SetParameters(param);


            this.Vu_BookingBaseBindingSource.DataSource = this.DataSource;
            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.RefreshReport();
        }

        public rptfrmIncome()
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
            saveFileDlg.FileName = "IncomeReport";
          
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


		private void btnViewReport_Click(object sender, EventArgs e)
        {

          
        }


        public void SendEmail()
        {

            General.ShowEmailForm(reportViewer1, "Income Report");

        }

        private void reportViewer1_Print(object sender, CancelEventArgs e)
        {
          
        
        }
     

     

     
    }
}
