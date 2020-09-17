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
using System.Diagnostics;
using Taxi_BLL;
using Newtonsoft.Json;

namespace Taxi_AppMain
{
    public partial class rptfrmJobDetails : UI.SetupBase
    {

        string reportname = "";

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


        Gen_SubCompany objSubCompany = null;

        public void GenerateReport()
        {


            try
            {

                if (this.DataSource.Count > 0 && this.DataSource[0].AuthCode.ToStr().Trim().Length > 0)
                {

                    chkPaymentSummary.Visible = true;
                }

                reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[6];


                 objSubCompany = AppVars.objSubCompany;
                if(this.DataSource.Count > 0 &&  this.DataSource[0].SubCompanyId!=null)
                {

                    objSubCompany = General.GetObject<Gen_SubCompany>(c => c.Id == this.DataSource[0].SubCompanyId);

                    if(objSubCompany==null)
                    {
                        objSubCompany = AppVars.objSubCompany;

                    }
                }



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyLogo != null ? objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyLogo", path);



                string header ="     "+ objSubCompany.Address.ToStr() + ",\n" + 
                            "       Telephone No:" + objSubCompany.TelephoneNo;
                header += ". Fax:" + objSubCompany.Fax.ToStr();
                header += "\nEmergency Contact No:" +objSubCompany.EmergencyNo.ToStr() + ". " + objSubCompany.WebsiteUrl;
                header += "\n                Email:" + objSubCompany.EmailAddress.ToStr();


                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Header", header);


                decimal SurchargeAmount = 0.00m;
                string cardNumber = string.Empty;


                if (chkPaymentSummary.Checked && this.DataSource.Count > 0 &&
                    (this.DataSource[0].PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD || this.DataSource[0].PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                    || this.DataSource[0].PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                    &&
                    this.DataSource[0].AuthCode.ToStr().Trim().Length > 0)
                {
                    Booking_Payment objPayment = General.GetObject<Booking_Payment>(c => c.BookingId == this.DataSource.FirstOrDefault().Id).DefaultIfEmpty();


                    if (objPayment != null)
                    {

                        SurchargeAmount = objPayment.SurchargeAmount.ToDecimal();

                        if (objPayment.CardNumber.ToStr().Length >= 16)
                            cardNumber = "**************" + objPayment.CardNumber.Substring(14).ToStr();
                    }                   

                }


                string criteria = ddlCriteria.Text;
                if (this.DataSource.Count > 0 )
                {
                    if (this.DataSource[0].CompanyId.ToInt()==0 && criteria == "Customer")
                    {

                        criteria = "Driver";

                    }                 

                }

                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("CreditCardExtraCharges", SurchargeAmount.ToStr()); //AppVars.objPolicyConfiguration.CreditCardExtraCharges.ToStr());
                //Report_Parameter_CreditCardExtraCharges

                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("CardNumber", cardNumber);
                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("ReportFor", criteria);
                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("PaymentSummary",chkPaymentSummary.Checked?"1":"0");

                reportViewer1.LocalReport.SetParameters(param);

                //ReportDataSource data = new ReportDataSource("vuBookingDetailBindingSource", this.DataSource);
                //this.reportViewer1.LocalReport.DataSources.Add(data);

                this.vuBookingDetailBindingSource.DataSource = this.DataSource;
                this.reportViewer1.ZoomPercent = 100;

                //if (Debugger.IsAttached)
                //{
               //        this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                //}
                ////else
                //    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
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

            General.ShowEmailForm(reportViewer1, "Job Print " + bookingNo, email,objSubCompany,true);

        }
     

        public rptfrmJobDetails()
        {
            InitializeComponent();
          


          
        }


        public void send(int a)
        {



            string connString = Application.StartupPath + @"\Reports\Report.exe";



            string conn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr();



            Classes.JArguments j = new Classes.JArguments()
            {//Connectionstring =

                ConnectionString = conn,
                id = (long)ddlCriteria.SelectedValue,
                AuthCode = this.DataSource[0].AuthCode.ToString(),
                _Checkbox = chkPaymentSummary.Checked,
               reportname = this.Name

               
            };

            // Convert BlogSites object to JOSN string format  
            string jsonData = JsonConvert.SerializeObject(j);
            jsonData = Cryptography.Encrypt(jsonData, "report", true);


            System.Diagnostics.Process.Start(connString, jsonData);
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

        private void btnEmailPrint_Click(object sender, EventArgs e)
        {
            EmailPrint();
        }

        public void EmailPrint()
        {
            try
            {
                if (this.DataSource != null)
                {
                    var list = this.DataSource.ToList();
                    if (list.Count > 0)
                    {
                        //rptfrmJobDetails frm4 = new rptfrmJobDetails();
                        //frm4.DataSource = this.DataSource;
                        //frm4.GenerateReport();
                        SendEmail(list[0].BookingNo, list[0].CustomerEmail.ToStr().Trim());
                    
                    }

                }
            }
            catch (Exception ex)
            {


            }

        }

        private void ddlCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaxiDataContext db = new TaxiDataContext();
           //(lst[0].Reportname.ToString(), false);
          
            send(0);
            //GenerateReport();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (chkPaymentSummary.Checked)
            {
                ddlCriteria.Enabled = false;

            }
            else
            {
                ddlCriteria.Enabled = true;

            }


            if (this.DataSource.Count > 0 && this.DataSource[0].AuthCode.ToStr().Trim().Length > 0)
            {
                if (this.DataSource[0].CompanyId != null)
                {

                    ddlCriteria.Text = "Customer";

                }
                else
                {
                    ddlCriteria.Text = "Driver";


                }

            }

            GenerateReport();
        }


     

     
    }
}
