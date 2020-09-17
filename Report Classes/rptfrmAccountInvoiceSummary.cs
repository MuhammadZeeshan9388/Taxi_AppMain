using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class rptfrmAccountInvoiceSummary : UI.SetupBase
    {
        bool IsLoaded = false;
        public rptfrmAccountInvoiceSummary()
        {
            InitializeComponent();
           
        }

        private void rptfrmAccountInvoiceSummary_Load(object sender, System.EventArgs e)
        {
            this.FormTitle = "Account Invoice Summary";
            dtpTillDate.Value = DateTime.Now.ToDate();
            dtpFromDate.Value = DateTime.Now.ToDate().AddMonths(-1);
            ComboFunctions.FillCompanyCombo(ddlCompany);
            ddlCompany.Enabled = false;
            chkAll.Checked = true;


        }

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                DateTime? StartDate = dtpFromDate.Value;
                DateTime? TillDate = dtpTillDate.Value;
                if (TillDate != null)
                    TillDate = TillDate + TimeSpan.Parse("23:59:59");


                UM_Form_Template objTemplate = null;
                objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);

                //if (objTemplate == null)
                //{
                //    ENUtils.ShowMessage("Report Template is not defined in Settings");
                //    return;
                //}


              

                if (objTemplate != null  && objTemplate.TemplateName.ToStr() == "Template2")
                {
                    string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";
                   
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptAccountInvoiceSummary.rdlc";

                    if (chkAll.Checked)
                    {
                        using (TaxiDataContext context = new TaxiDataContext())
                        {
                            var invoiceTemp = context.Invoices.Where(w => (w.InvoiceDate >= StartDate && w.InvoiceDate <= TillDate) && w.Gen_Company != null)
                                                   .Select(w => new AccountInvoiceSummaryModel
                                                   {
                                                       InviceNo = w.InvoiceNo,
                                                       InvoiceDate = w.InvoiceDate,
                                                       Id = w.Id,
                                                       AccountName = w.Gen_Company.CompanyName,
                                                       TotalInvoice = w.InvoiceTotal,
                                                       TotalJobs = w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Count(),
                                                       VAT = w.Gen_Company.HasVat == true ? (w.InvoiceTotal * 20) / 100 : 0.00m,
                                                       TotalDriverCharges = w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.FareRate + x.Booking.CongtionCharges + x.Booking.MeetAndGreetCharges)),
                                                       Margin = (w.InvoiceTotal) - (w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.FareRate + x.Booking.CongtionCharges + x.Booking.MeetAndGreetCharges)))

                                                   }).OrderByDescending(w => w.InvoiceDate).ToList();
                            this.AccountInvoiceSummaryModelBindingSource.DataSource = invoiceTemp;
                         
                        }
                    }
                    else
                    {
                        using (TaxiDataContext context = new TaxiDataContext())
                        {
                            var invoiceTemp = context.Invoices.Where(w => (w.InvoiceDate >= StartDate && w.InvoiceDate <= TillDate) && w.Gen_Company != null && w.CompanyId == ddlCompany.SelectedValue.ToInt())
                                             .Select(w => new AccountInvoiceSummaryModel
                                             {
                                                 InviceNo = w.InvoiceNo,
                                                 InvoiceDate = w.InvoiceDate,
                                                 Id = w.Id,
                                                 AccountName = w.Gen_Company.CompanyName,
                                                 TotalInvoice = w.InvoiceTotal,
                                                 TotalJobs = w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Count(),
                                                 VAT = w.Gen_Company.HasVat == true ? (w.InvoiceTotal  * 20) / 100 : 0.00m,//.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.CompanyPrice + x.Booking.ParkingCharges + x.Booking.WaitingCharges)) * 20) / 100 : 0.00m,
                                                 TotalDriverCharges = w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.FareRate + x.Booking.CongtionCharges + x.Booking.MeetAndGreetCharges)),
                                                 Margin = (w.InvoiceTotal) - (w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.FareRate + x.Booking.CongtionCharges + x.Booking.MeetAndGreetCharges)))

                                             }).OrderByDescending(w => w.InvoiceDate).ToList();
                            this.AccountInvoiceSummaryModelBindingSource.DataSource = invoiceTemp;
                          
                        }
                        IsLoaded = true;
                    }
                }
                else
                {
                    if (chkAll.Checked)
                    {
                        using (TaxiDataContext context = new TaxiDataContext())
                        {
                            var invoiceTemp = context.Invoices.Where(w => (w.InvoiceDate >= StartDate && w.InvoiceDate <= TillDate) && w.Gen_Company != null)
                                                   .Select(w => new AccountInvoiceSummaryModel
                                                   {
                                                       InviceNo = w.InvoiceNo,
                                                       InvoiceDate = w.InvoiceDate,
                                                       Id = w.Id,
                                                       AccountCode = w.Gen_Company.CompanyCode,
                                                       AccountName = w.Gen_Company.CompanyName,
                                                       TotalJobs = w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Count(),
                                                       TotalAmount = w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.CompanyPrice + x.Booking.ParkingCharges + x.Booking.WaitingCharges)),
                                                       VAT = w.Gen_Company.HasVat == true ? (w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.CompanyPrice + x.Booking.ParkingCharges + x.Booking.WaitingCharges)) * 20) / 100 : 0.00m,


                                                   }).OrderByDescending(w => w.InvoiceDate).ToList();
                            this.AccountInvoiceSummaryModelBindingSource.DataSource = invoiceTemp;
                          
                        }
                    }
                    else
                    {
                        using (TaxiDataContext context = new TaxiDataContext())
                        {
                            var invoiceTemp = context.Invoices.Where(w => (w.InvoiceDate >= StartDate && w.InvoiceDate <= TillDate) && w.Gen_Company != null && w.CompanyId == ddlCompany.SelectedValue.ToInt())
                                             .Select(w => new AccountInvoiceSummaryModel
                                             {
                                                 InviceNo = w.InvoiceNo,
                                                 InvoiceDate = w.InvoiceDate,
                                                 Id = w.Id,
                                                 AccountCode = w.Gen_Company.CompanyCode,
                                                 AccountName = w.Gen_Company.CompanyName,
                                                 TotalJobs = w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Count(),
                                                 TotalAmount = w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.CompanyPrice + x.Booking.ParkingCharges + x.Booking.WaitingCharges)),
                                                 VAT = w.Gen_Company.HasVat == true ? (w.Invoice_Charges.Where(x => x.InvoiceId == w.Id).Sum(x => (x.Booking.CompanyPrice + x.Booking.ParkingCharges + x.Booking.WaitingCharges)) * 20) / 100 : 0.00m,

                                             }).OrderByDescending(w => w.InvoiceDate).ToList();                 

                            this.AccountInvoiceSummaryModelBindingSource.DataSource = invoiceTemp;
                        }
                        IsLoaded = true;
                    }
                }

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[3];
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Period", "Period: " + StartDate.Value.ToString("dd/MM/yyyy") + " - " + TillDate.Value.ToString("dd/MM/yyyy"));
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyName", AppVars.objSubCompany.CompanyName);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", AppVars.objSubCompany.Address);
                reportViewer1.LocalReport.SetParameters(param);
              
                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("DSLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);
                this.reportViewer1.SetDisplayMode(DisplayMode.Normal);
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                reportViewer1.RefreshReport();

               
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void reportViewer1_Load(object sender, System.EventArgs e)
        {

        }

        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkAll.Checked)
            {
                ddlCompany.SelectedValue = null;
                ddlCompany.Enabled = false;

            }
            else
                ddlCompany.Enabled = true;
        }

        private void btnExportPDF_Click(object sender, System.EventArgs e)
        {
            if (IsLoaded)
            {
                ExportReport("pdf");
            }
            else
            {
                LoadReport();
                ExportReport("pdf");
            }
        }

        public void ExportReport( string exportTo)
        {



            SaveFileDialog saveFileDlg = new SaveFileDialog();

            if (exportTo.ToLower() == "pdf")
                saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            else
                saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Account Invoice Summary";


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

        private void btnExit1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void radLabel2_Click(object sender, System.EventArgs e)
        {

        }
    }
}
