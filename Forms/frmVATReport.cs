using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using DAL;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using System.Linq.Expressions;
using Telerik.Data;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class frmVATInvoice : UI.SetupBase
    {
      

        private string companyEmail;

        DateTime fromDate;
        DateTime tillDate;
        string quarter;

        public frmVATInvoice(string FromDate, string TillDate, string Quarter)
        {
            InitializeComponent();
            fromDate = Convert.ToDateTime( FromDate);
            tillDate =  Convert.ToDateTime(TillDate);
            quarter = Quarter;
            GenerateReport();
            //LoadGrid();
            btnEmail.Visible = true;
            
        }

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);
        public void GenerateReport()
        {
            try
            {
                Microsoft.Reporting.WinForms.ReportParameter[] param = null;
                param = new Microsoft.Reporting.WinForms.ReportParameter[7];
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Quarter", quarter);
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Date", (DateTime.Now.ToString("dd/MM/yyyy")));
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", AppVars.objSubCompany.EmailAddress);
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyName", AppVars.objSubCompany.CompanyName);
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Phoneno", AppVars.objSubCompany.MobileNo);
                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", AppVars.objSubCompany.Address);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VAT", AppVars.objSubCompany.CompanyVatNumber);
                                   
                    var list = (from a in AppVars.BLData.GetQueryable<Invoice>(b => (b.InvoiceDate.Value >= fromDate && b.InvoiceDate.Value <= tillDate) && b.InvoiceDate.HasValue == true)
                                where a.Gen_Company.HasVat!=null && a.Gen_Company.HasVat==true
                                orderby a.InvoiceDate
                                select new VatCalculator()

                                {
                                    InvoiceNo = a.InvoiceNo,
                                    InvoiceDate = a.InvoiceDate,
                                    Net = a.InvoiceTotal -  (a.InvoiceTotal   * a.Gen_Company.DiscountPercentage)/100,
                                   
                                }).ToList();
           
              
            reportViewer1.LocalReport.SetParameters(param);
            VatCalculatorBindingSource.DataSource = list;
           
                           
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {

            }

        }

        private void frmVATInvoice_Load(object sender, EventArgs e)
        {
            
        }

     

        private void btnEmail_Click_1(object sender, EventArgs e)
        {
            companyEmail = AppVars.objSubCompany.EmailAddress;

            frmEmail frm = new frmEmail(reportViewer1, companyEmail);
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.ShowDialog();
        }

       
        
     

              

    }

   
}
