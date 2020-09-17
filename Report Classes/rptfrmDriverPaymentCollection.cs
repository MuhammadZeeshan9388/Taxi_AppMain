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
    public partial class rptfrmDriverPaymentCollection : UI.SetupBase
    {
        public rptfrmDriverPaymentCollection()
        {
            InitializeComponent();
            
        }

        private void rptfrmPaymentCollection_Load(object sender, EventArgs e)
        {
            LoadReport();
        }
        public void LoadReport()
        {
            try
            {
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                var list=(new Taxi_Model.TaxiDataContext().stp_PaymentCollection()).ToList();
                var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                this.stp_PaymentCollectionResultBindingSource.DataSource = list2;
                DateTime? dtCurrent = list.Max(c => c.FromDate).ToDateorNull();
                DateTime? dtEnd = list.Max(c => c.ToDate).ToDateorNull();
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];
                string heading = string.Empty;
                heading = "from " + string.Format("{0:dd/MM/yyyy}", dtCurrent) + " to " + string.Format("{0:dd/MM/yyyy}", dtEnd);
                //heading = "From: "+string.Format("{0:dd/MM/yyyy}", dtFrom) + " To: " + string.Format("{0:dd/MM/yyyy}", dtTill);
               // string EndDate = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                //string To = string.Format("{0:dd/MM/yyyy}", dtTill);
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_EndDate", heading);
                //List<ClsLogo> objLogo = new List<ClsLogo>();
                //objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                //ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                //this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
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
        public void ExportReportToExcel(string exportTo)
        {



            SaveFileDialog saveFileDlg = new SaveFileDialog();

            //if (exportTo.ToLower() == "pdf")
            //    saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            //else
            saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Driver Payment Collection Report";


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
    }
}
