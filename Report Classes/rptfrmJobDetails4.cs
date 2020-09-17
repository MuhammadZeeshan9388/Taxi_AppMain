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
using Taxi_BLL;


namespace Taxi_AppMain
{
    public partial class rptfrmJobDetails4 : UI.SetupBase
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

        public void EmailPrint()
        {
            try
            {
            if(this.DataSource!=null)
            {
                var list=this.DataSource.ToList();
                    if (list.Count > 0)
                    {
                        rptfrmJobDetails4 frm4 = new rptfrmJobDetails4();
                        frm4.DataSource = this.DataSource;
                        frm4.GenerateReport();
                        frm4.SendEmail(list[0].BookingNo, list[0].CustomerEmail.ToStr().Trim());
                        //UM_Form_Template objReport = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "rptfrmJobDetails" && c.IsDefault == true);
                        //rptfrmJobDetails frm = null;
                        //rptfrmJobDetails2 frm2 = null;
                        //rptfrmJobDetails3 frm3 = null;
                        //rptfrmJobDetails4 frm4 = null;
                        //if (objReport != null)
                        //{

                        //    switch (objReport.TemplateValue)
                        //    {
                        //        case "rptfrmJobDetails":
                        //            frm = new rptfrmJobDetails();
                        //            frm.DataSource = this.DataSource;
                        //            frm.GenerateReport();

                        //            frm.SendEmail(list[0].BookingNo, list[0].CustomerEmail.ToStr().Trim());
                        //            break;


                        //        case "rptfrmJobDetails2":
                        //            frm2 = new rptfrmJobDetails2();
                        //            frm2.DataSource = this.DataSource;
                        //            frm2.GenerateReport();

                        //            frm2.SendEmail(list[0].BookingNo, list[0].CustomerEmail.ToStr().Trim());
                        //            break;

                        //        case "rptfrmJobDetails3":
                        //            frm3 = new rptfrmJobDetails3();
                        //            frm3.DataSource = this.DataSource;
                        //            frm3.GenerateReport();
                        //            frm3.SendEmail(list[0].BookingNo, list[0].CustomerEmail.ToStr().Trim());

                        //            break;


                        //        case "rptfrmJobDetails4":
                        //            frm4 = new rptfrmJobDetails4();
                        //            frm4.DataSource = this.DataSource;
                        //            frm4.GenerateReport();

                        //            frm4.SendEmail(list[0].BookingNo, list[0].CustomerEmail.ToStr().Trim());
                        //            break;
                        //    }
                        //}
                    }

                }
            }
            catch (Exception ex)
            {


            }

        }


        int retrycnt = 0;
        public void GenerateReport()
        {


            try
            {
                string website = AppVars.objSubCompany.WebsiteUrl.ToStr();
                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[10];


                decimal SurchargeAmount = 0.00m;
                string cardNumber = string.Empty;


                if (this.DataSource.Count > 0 && 
                    (this.DataSource[0].PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD || this.DataSource[0].PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                    || this.DataSource[0].PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                    && 
                    this.DataSource[0].AuthCode.ToStr().Trim().Length>0)
                {
                    Booking_Payment objPayment = General.GetObject<Booking_Payment>(c => c.BookingId == this.DataSource.FirstOrDefault().Id).DefaultIfEmpty();


                    if (objPayment != null)
                    {

                        SurchargeAmount = objPayment.SurchargeAmount.ToDecimal();

                        if (objPayment.CardNumber.ToStr().Length >= 16)
                             cardNumber = "**************" + objPayment.CardNumber.Substring(14).ToStr();
                    }
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptJobDetails4_acc.rdlc";
                    //this.reportViewer1.LocalReport.ReportPath = "Taxi_AppMain.ReportDesigns.rptJobDetails4_acc.rdlc";

                }

                if (this.DataSource.Count(c => c.CompanyId != null) > 0)
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns.rptJobDetails4_acc.rdlc";
                   // this.reportViewer1.LocalReport.ReportPath = "Taxi_AppMain.ReportDesigns.rptJobDetails4_acc.rdlc";
                }
            
                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyLogo", path);



                string header ="     "+ AppVars.objSubCompany.Address.ToStr() + ",\n" + 
                            "       Telephone No:" + AppVars.objSubCompany.TelephoneNo;
              //  header += ", Fax:" + AppVars.objSubCompany.Fax.ToStr();

                header += "\n Email:" + AppVars.objSubCompany.EmailAddress.ToStr() + ", Website:" + AppVars.objSubCompany.WebsiteUrl;
               // DateTime ? dtPrintDate=DateTime.Now.ToDate();
                string PrintDate = "Print date : " + string.Format("{0:dd-MMMM-yy HH:MM}", DateTime.Now.ToDateTime());
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", header);
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());
                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyPhone", AppVars.objSubCompany.TelephoneNo.ToStr());
                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_PrintDate", PrintDate);

                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CreditCardExtraCharges", SurchargeAmount.ToStr()); //AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToStr());
                //Report_Parameter_CreditCardExtraCharges

                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CardNumber", cardNumber);

                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VatNumber", AppVars.objSubCompany.CompanyVatNumber.ToStr());

                decimal fullTotal = Math.Round(SurchargeAmount + this.DataSource.FirstOrDefault().DefaultIfEmpty().TotalCharges.ToDecimal(), 2);


                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_OverallTotal", fullTotal.ToStr());


                reportViewer1.LocalReport.SetParameters(param);
                
                this.Vu_BookingDetailBindingSource.DataSource = this.DataSource;

                this.reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.SetDisplayMode(DisplayMode.Normal);
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();


               

           
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

                //if (retrycnt == 0)
                //{
                //    retrycnt = 1;
                //    GenerateReport();

                //}
                //else
                    General.AddLog(ex.Message + ",Target : " + ex.TargetSite + ",Source : " + ex.Source + ",Stack Trace :" + ex.StackTrace, Environment.MachineName.ToStr());
            }
        }

        public void SendEmail(string bookingNo, string email)
        {

            General.ShowEmailForm(reportViewer1, "Job Print " + bookingNo, email);

        }
     

        public rptfrmJobDetails4()
        {
            InitializeComponent();

            this.btnEmailPrint.Click += new EventHandler(btnEmail_Click);

          
        }

        void btnEmail_Click(object sender, EventArgs e)
        {
            EmailPrint();
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

        private void rptfrmJobDetails4_Load(object sender, EventArgs e)
        {

        } 
    }
}
