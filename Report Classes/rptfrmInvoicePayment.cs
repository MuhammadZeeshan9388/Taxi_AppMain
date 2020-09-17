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
using System.Collections;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class rptfrmInvoicePayment : UI.SetupBase
    {
        bool IsReportLoaded = false;

        string PaymentType = string.Empty;



        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);



        private List<stp_InvoicePaymentResult> _DataSource;

        public List<stp_InvoicePaymentResult> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }

        public bool IsExcelReport = false;
        public rptfrmInvoicePayment()
        {
            InitializeComponent();
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            btnExport.Click += new EventHandler(btnExport_Click);
            this.btnEmail.Click += new EventHandler(btnEmail_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnExportToExcel.Click += new EventHandler(btnExportToExcel_Click);
            this.Load += RptfrmInvoicePayment_Load;


           

        }

        private void RptfrmInvoicePayment_Load(object sender, EventArgs e)
        {
            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);

            

            ComboFunctions.FillCompanyCombo(ddlCompany, ddlSubCompany.SelectedValue.ToInt());

            this.ddlSubCompany.SelectedValueChanged += new System.EventHandler(this.ddlSubCompany_SelectedValueChanged);
            this.ddlCompany.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.ddlCompany_SelectedIndexChanged);
        }

        void btnExportToExcel_Click(object sender, EventArgs e)
        {

			if (IsReportLoaded == true && IsExcelReport == false)
			{
				ExportReportToExcel("Excel");
			}
			else if (IsReportLoaded == true && IsExcelReport == true)
			{
				IsExcelReport = false;
				LoadReport();
				ExportReportToExcel("Excel");
			}
			else
			{
				IsExcelReport = false;
				LoadReport();
				ExportReportToExcel("Excel");
			}

			//IsExcelReport = true;
   //         //            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptDriverAccountBookings.rdlc";
   //         LoadReport();
   //         ExportReportToExcel("Excel");
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnEmail_Click(object sender, EventArgs e)
        {
          
            string error = string.Empty;

            if (ddlSubCompany.SelectedValue != null)
            {
                error += "Required : Sub Company";
            }

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;

            }

            int companyId;
            
            if (ddlCompany.SelectedValue == null)
            {
                companyId = 0;
            }
            else
            {
                companyId = ddlCompany.SelectedValue.ToInt();
            }

            

            IsExcelReport = false;
			this.stp_InvoicePaymentResultBindingSource.DataSource = GetData(ddlSubCompany.SelectedValue.ToInt(), companyId);
			LoadReport();
			SendEmail(); 
        }

        void btnExport_Click(object sender, EventArgs e)
        {

            if (IsReportLoaded == true && IsExcelReport == false)
            {
                ExportReport();
            }
            else if (IsReportLoaded == true && IsExcelReport == true)
            {
                IsExcelReport = false;
                LoadReport();
                ExportReport();
            }
            else
            {
                IsExcelReport = false;
                LoadReport();
                ExportReport();
            }

        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            IsExcelReport = false;
            LoadReport();
        }

       
        private List<stp_InvoicePaymentResult> GetData(int SubCompanyId, int companyId)
        {
                


                                       
            var result = (from a in new Taxi_Model.TaxiDataContext().stp_InvoicePayment(SubCompanyId, companyId)
                          
                          select new stp_InvoicePaymentResult
                          {

                             Balance = a.Balance,
                             CompanyName = a.CompanyName,
                             InvoiceDate = a.InvoiceDate,
                             InvoiceNo = a.InvoiceNo,
                             InvoicePayment = a.InvoicePayment,
                             InvoiceTotal = a.InvoiceTotal,
                             PaymentDate = a.PaymentDate

                          }).AsEnumerable().OrderBy(item => item.InvoiceNo, new NaturalSortComparer<string>()).ToList();

        

           

            if(dtpFromDate.Value!=null && dtpToDate.Value!=null)
            {
                result= result.Where(c => (c.InvoiceDate >= dtpFromDate.Value && c.InvoiceDate <= dtpToDate.Value)).ToList();




            }
           else if (dtpFromDate.Value != null && dtpToDate.Value == null)
            {
                result = result.Where(c => (c.InvoiceDate >= dtpFromDate.Value)).ToList();




            }
            else if (dtpFromDate.Value == null && dtpToDate.Value != null)
            {
                result = result.Where(c => (c.InvoiceDate<= dtpToDate.Value)).ToList();


            }

            return result;
        }

        
       


        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Invoice Statement Report");
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

            saveFileDlg.FileName = "Invoice Statement Report";

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
        public void ExportReportToExcel(string exportTo)
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
			saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";
			saveFileDlg.Title = "Save File";

			saveFileDlg.FileName = "Invoice Statement Report";

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
			//////////////////////////////////////////////////////////////

			//SaveFileDialog saveFileDlg = new SaveFileDialog();

   //         //if (exportTo.ToLower() == "pdf")
   //         //    saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
   //         //else
   //         saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

   //         saveFileDlg.Title = "Save File";
   //         saveFileDlg.FileName = "Invoice Statement Report";


   //         if (saveFileDlg.ShowDialog() == DialogResult.OK)
   //         {


   //             try
   //             {
   //                 FileStream fs = new FileStream(saveFileDlg.FileName, FileMode.Create);

   //                 Microsoft.Reporting.WinForms.Warning[] warnings;
   //                 string[] streamids;
   //                 string mimeType;
   //                 string encoding;
   //                 string extension;

   //                 byte[] bytes = reportViewer1.LocalReport.Render(
   //                  exportTo.ToLower(), null, out mimeType, out encoding,
   //                   out extension,
   //                  out streamids, out warnings);

   //                 fs.Write(bytes, 0, bytes.Length);
   //                 fs.Close();


   //             }
   //             catch (Exception ex)
   //             {
   //                 MessageBox.Show(ex.Message);
   //             }
   //         }
        }
        private void LoadReport()
        {
            try
            {
                string error = string.Empty;

                int companyId ;

                if (ddlSubCompany.SelectedValue == null )
                {
                    error += "Required : Sub Company";
                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }

                if (ddlCompany.SelectedValue == null)
                {
                    companyId = 0;
                }
                else
                {
                    companyId = ddlCompany.SelectedValue.ToInt();
                }

                
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                this.stp_InvoicePaymentResultBindingSource.DataSource = GetData(ddlSubCompany.SelectedValue.ToInt(), companyId);
                              

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[2];

                string heading = string.Empty;
                //heading = "From: "+string.Format("{0:dd/MM/yyyy}", dtFrom) + " To: " + string.Format("{0:dd/MM/yyyy}", dtTill);
           
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyName", AppVars.objSubCompany.CompanyName);
              
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_ReportDate", string.Format("{0:dd MMMM yyyy}", DateTime.Now));


                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                reportViewer1.LocalReport.SetParameters(param);

                if (IsExcelReport)
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptInvoicePayment.rdlc";
                }
                else
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptInvoicePayment.rdlc";
                }


                this.reportViewer1.SetDisplayMode(DisplayMode.Normal);
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

    

      

        private void ddlSubCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlSubCompany.Tag.ToStr() == "1")
                return;

            ddlCompany.Tag = "1";
            ComboFunctions.FillCompanyCombo(ddlCompany, ddlSubCompany.SelectedValue.ToInt());

            ddlCompany.Tag = "0";
        }

        private void ddlCompany_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            if (ddlCompany.SelectedValue == null || ddlCompany.Tag.ToStr()=="1")
                return;
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.DeferredLoadingEnabled = false;
                    int subCompanyId = db.Gen_Companies.FirstOrDefault(c => c.Id == ddlCompany.SelectedValue.ToInt()).DefaultIfEmpty().SubCompanyId.ToInt(); ;

                    if (subCompanyId != 0)
                    {

                        ddlSubCompany.Tag = "1";
                        ddlSubCompany.SelectedValue = subCompanyId;
                        ddlSubCompany.Tag = "0";

                    }
                }
            }
            catch
            {


            }
        }
    }
}
