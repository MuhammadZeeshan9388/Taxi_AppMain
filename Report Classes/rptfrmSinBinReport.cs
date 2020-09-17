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
using Telerik.WinControls.Enumerations;
//using Telerik.Charting.Styles;
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;
namespace Taxi_AppMain
{
    public partial class rptfrmSinBinReport : UI.SetupBase
    {

        private string _Criteria;

        public string Criteria
        {
            get { return _Criteria; }
            set { _Criteria = value; }
        }


        private List<vu_SinBin> _DataSource;

        public List<vu_SinBin> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        public struct COLS
        {
            public static string ID = "ID";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string OrderNo = "OrderNo";
            public static string PupilNo = "PupilNo";

            public static string RefNumber = "RefNumber";

            public static string Passenger = "Passenger";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

            public static string Charges = "Charges";

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string Total = "Total";

        }


        public void GenerateReport()
        {
            try
            {
                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[4];

               


                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", AppVars.objSubCompany.CompanyName.ToStr());
               
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", AppVars.objSubCompany.Address.ToStr());

                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", AppVars.objSubCompany.TelephoneNo.ToStr());
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Criteria",this.Criteria);
              


                //List<ClsLogo> objLogo = new List<ClsLogo>();
                //objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                //ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                //this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                //string path = @"File:";
                //param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                //param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());

               


                reportViewer1.LocalReport.SetParameters(param);


                //int cnt = this.DataSource.Count;

                //int minRows = 8;

                //if (cnt < minRows)
                //{
                //    for (int i = 0; i < minRows - cnt; i++)
                //    {
                //        this.DataSource.Add(new vw_DriverInfo { Id = data.Id, CompanyId = data.CompanyId });
                //    }
                //}

                var list2 = (this.DataSource.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();
                this.vu_SinBinBindingSource.DataSource = list2;//this.DataSource;

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
      
        }

        public rptfrmSinBinReport()
        {
           
            InitializeComponent();
            //ComboFunctions.FillDriverCombo(ddlDriver);
          
        }


        public void SendEmail()
        {

            General.ShowEmailForm(reportViewer1, "SinBin Report");

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
            saveFileDlg.FileName = "Sin Bin Report";
          
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


            ////FileStream fs = new FileStream(@"d:\output.pdf",
            ////   FileMode.Create);

            //FileInfo file = new FileInfo(invoiceNo + ".pdf");
            //FileStream fs = file.Create();

            //fs.Write(bytes, 0, bytes.Length);
            //fs.Close();

       

        }

        private void rptfrmSinBinReport_Load(object sender, EventArgs e)
        {

        }

     


        
        


     
     
    }
}
