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
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;
using System.Collections;

namespace Taxi_AppMain
{
    public partial class frmInvoiceReport : UI.SetupBase
    {

        bool IsSplitByOrder = false;

        private List<vu_Invoice> _DataSource;

        public List<vu_Invoice> DataSource
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

        public void SetTemplate1()
        {


        }

        Gen_SubCompany objSubCompany = null;
        public void GenerateReport()
        {
            try
            {
       
               

                if (ObjInvoice.SubCompanyId == null)
                    objSubCompany = AppVars.objSubCompany;
                else
                    objSubCompany = ObjInvoice.Gen_SubCompany;


                if (objSubCompany == null)
                    return;



                Microsoft.Reporting.WinForms.ReportParameter[] param = null;

            //    if (ddlCompany.SelectedValue == null)
           //         pnlCriteria.Visible = false;

                reportViewer1.LocalReport.EnableExternalImages = true;

                int minRows = 8;

                string address = objSubCompany.Address;
                string telNo = string.Empty;

                string sortCode = objSubCompany.SortCode.ToStr();
                string accountNo = objSubCompany.AccountNo.ToStr();
                string accountTitle = objSubCompany.AccountTitle.ToStr();
                string bank = objSubCompany.BankName.ToStr();
                string Fax = string.Empty;
                string email = objSubCompany.EmailAddress.ToStr().Trim();
                decimal DiscountPercent = 0.00m;
                string hasBankDetails = "1";
                if (string.IsNullOrEmpty(sortCode) && string.IsNullOrEmpty(accountNo) && string.IsNullOrEmpty(accountTitle)
                    && string.IsNullOrEmpty(bank))
                {
                    hasBankDetails = "0";
                }

                if (hasBankDetails == "0" && objSubCompany.CompanyVatNumber.ToStr().Trim().Length > 0)
                {
                    hasBankDetails = "1";

                }

                if (!string.IsNullOrEmpty(sortCode))
                    sortCode = "Sort Code : " + sortCode;


                if (!string.IsNullOrEmpty(bank))
                    bank = "Bank : " + bank;


                if (!string.IsNullOrEmpty(accountTitle))
                    accountTitle = "Account Title : " + accountTitle;


                string website = objSubCompany.WebsiteUrl.ToStr();
                if (!string.IsNullOrEmpty(website))
                {
                    website += " , ";
                }

                website += "Email:" + objSubCompany.EmailAddress.ToStr();


                string companyNumber = objSubCompany.CompanyNumber.ToStr();
                if (!string.IsNullOrEmpty(companyNumber))
                {
                    companyNumber = "Company Number: " + companyNumber;
                }

                string vatNumber = objSubCompany.CompanyVatNumber.ToStr();
                if (!string.IsNullOrEmpty(vatNumber))
                {
                    vatNumber = "VAT Number: " + vatNumber;
                }



               

                int? companyId = this.DataSource.FirstOrDefault().DefaultIfEmpty().CompanyId;

                var data = this.DataSource.FirstOrDefault().DefaultIfEmpty();
                string adminFeeLabel = data.AdminFeesLabel.ToStr();

                if (!string.IsNullOrEmpty(accountNo))
                    accountNo = "Account No : " + accountNo;

                UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);


            
                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "!ndex_@ut0_$erv!ce$_Ltd")
                {
                    objTemplate.TemplateName = "Template48";

                }


                if (objTemplate.TemplateName.ToStr() == "Template13" || objTemplate.TemplateName.ToStr() == "Template14")
                {
                    telNo = "Tel No. " + objSubCompany.TelephoneNo + " Fax. " + objSubCompany.Fax;
                    email += " , " + objSubCompany.WebsiteUrl.ToStr();
                    address = objSubCompany.CompanyName.ToStr() + " , " + address;
                }
                else
                {
                    telNo = "Tel No. " + objSubCompany.TelephoneNo;
                }


                if (objTemplate.TemplateName.ToStr() == "Template16")
                {
                    website = objSubCompany.WebsiteUrl;
                }
                else if (objTemplate.TemplateName.ToStr() == "Template18")
                {
                    vatNumber = "VAT No: " + objSubCompany.CompanyVatNumber.ToStr();
                    telNo = "Telephone:" + objSubCompany.TelephoneNo + " - " + objSubCompany.MobileNo;
                    website = "Fax: " + objSubCompany.Fax + " - " + objSubCompany.WebsiteUrl.ToStr();
                }
                else if (objTemplate.TemplateName.ToStr() == "Template19")
                {
                    companyNumber = "Company Number: " + objSubCompany.CompanyNumber + " , VAT No: " + objSubCompany.CompanyVatNumber;
                    telNo = "Tel No. " + objSubCompany.TelephoneNo + " Fax. " + objSubCompany.Fax;
                   
                    email = objSubCompany.EmailAddress + " , " + objSubCompany.WebsiteUrl.ToStr();
                }








                if (objTemplate.TemplateName.ToStr() == "Template6")
                    param = new Microsoft.Reporting.WinForms.ReportParameter[24];

                else if (objTemplate.TemplateName.ToStr() == "Template7" || objTemplate.TemplateName.ToStr() == "Template8"
                    || objTemplate.TemplateName.ToStr() == "Template9" || objTemplate.TemplateName.ToStr() == "Template10"
                    || objTemplate.TemplateName.ToStr() == "Template11" || objTemplate.TemplateName.ToStr() == "Template15"
                    || objTemplate.TemplateName.ToStr() == "Template17" || objTemplate.TemplateName.ToStr() == "Template21"
                    || objTemplate.TemplateName.ToStr() == "Template23" || objTemplate.TemplateName.ToStr() == "Template25"
                   || objTemplate.TemplateName.ToStr() == "Template27"
                    || objTemplate.TemplateName.ToStr() == "Template30"
                   || objTemplate.TemplateName.ToStr() == "Template31" || objTemplate.TemplateName.ToStr() == "Template32"
                   || objTemplate.TemplateName.ToStr() == "Template48")
                {
                    param = new Microsoft.Reporting.WinForms.ReportParameter[26];


                  

                }
                else if (objTemplate.TemplateName.ToStr() == "Template16")
                    param = new Microsoft.Reporting.WinForms.ReportParameter[27];

                else if (objTemplate.TemplateName.ToStr() == "Template22")
                    param = new Microsoft.Reporting.WinForms.ReportParameter[28];

                else if (objTemplate.TemplateName.ToStr() == "Template24" || objTemplate.TemplateName.ToStr() == "Template26")
                    param = new Microsoft.Reporting.WinForms.ReportParameter[32];

                else if (objTemplate.TemplateName.ToStr() == "Template18")
                    param = new Microsoft.Reporting.WinForms.ReportParameter[31];

                else if (objTemplate.TemplateName.ToStr() == "Template45")
                    param = new Microsoft.Reporting.WinForms.ReportParameter[27];

                else
                    param = new Microsoft.Reporting.WinForms.ReportParameter[22];



                string footer = objSubCompany.WebsiteUrl.ToStr();
                if (objTemplate.TemplateName.ToStr() == "Template10" || objTemplate.TemplateName.ToStr() == "Template21"
                    || objTemplate.TemplateName.ToStr() == "Template25" || objTemplate.TemplateName.ToStr() == "Template27"
                    || objTemplate.TemplateName.ToStr() == "Template30" || objTemplate.TemplateName.ToStr() == "Template45"
                   || objTemplate.TemplateName.ToStr() == "Template31" || objTemplate.TemplateName.ToStr() == "Template32"
                   || objTemplate.TemplateName.ToStr() == "Template48")//|| objTemplate.TemplateName.ToStr() == "Template16")
                {

                    if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr().ToLower() == "jaquarexpresscars")
                    {

                        footer = "Jaguar Express is a Trading name for Kamran Cars & Airport Transfers For London" + Environment.NewLine;
                        footer += "Cheques may be made payable to : Airport Transfers For London" + Environment.NewLine;
                        footer += "BACS PAYMENT INSTRUCTION :" + Environment.NewLine;
                        footer += "VAT : " + objSubCompany.CompanyVatNumber.ToStr();
                    }

                    else
                    {

                        if (objTemplate.TemplateName.ToStr() == "Template25")
                        {
                            footer = "If you have any queries regarding invoice, please dont hesitate to contact Accounts Team on " + objSubCompany.EmailAddress.ToStr() + " or " + objSubCompany.TelephoneNo.ToStr();
                                                   

                        }
                        else
                        {

                            footer = "If you have any queries regarding invoice, please dont hesitate to contact Accounts Team on " + objSubCompany.EmailAddress.ToStr() + " or " + objSubCompany.TelephoneNo.ToStr();
                            footer += "\r\n" + objSubCompany.WebsiteUrl.ToStr() + " are part of " + objSubCompany.AccountTitle;


                            if (objTemplate.ShowSpecialColumns.ToBool())
                            {
                                footer += "\r\n" + "Registered in England and Wales No. GB " + objSubCompany.CompanyNumber;
                            }
                        }
                    }



                    //    3 websites are part of account title.
                    //  Registered in England and Wales No. GB account number, Vat Reg No. GB VatNo

                    minRows = 4;
                }

                if (objTemplate.TemplateName.ToStr() == "Template16")
                {
                    footer = "All querries should be made as soon as possible either by email ( " + objSubCompany.EmailAddress + " ) , Customer Services  number : " + objSubCompany.TelephoneNo + " or call Kamran direct  on " + objSubCompany.MobileNo + "  between MON to FRI 9am to 6pm";
                }

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);


                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MobileNo", "Mobile: " + objSubCompany.EmergencyNo.ToStr());
                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);
                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", "Email: " + email);

                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyNumber", companyNumber);


                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle", accountTitle);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);


                //


                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyLogo != null ? objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);


                string path = AppVars.objPolicyConfiguration.ControllerWelcomeMessage.ToStr();


                //  string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);

                //


                if (objTemplate.TemplateName.ToStr() == "Template15")
                {
                    //  param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeLabel", data.AdminFeesLabel.ToStr());

                    footer = objSubCompany.CompanyName.ToStr();
                    website = objSubCompany.WebsiteUrl.ToStr();
                    param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);
                    param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", objSubCompany.CompanyName.ToStr() + " " + objSubCompany.Address + " VAT No " + objSubCompany.CompanyVatNumber.ToStr());

                    param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", footer);


                }
                else
                {
                    if (objTemplate.TemplateName.ToStr() == "Template24")
                    {
                        var dateRange=this.DataSource.FirstOrDefault(c=>c.Id!=0);

                        string accTitle = string.Empty;

                        if (!string.IsNullOrEmpty(accountTitle))
                            accTitle = accountTitle;


                        param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", objSubCompany.CompanyName.ToStr());
                        param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle",  objSubCompany.AccountTitle.ToStr());


                        string dateCriteria=" ";

                        if(dateRange!=null && dateRange.FromDate!=null && dateRange.TillDate!=null)
                        dateCriteria="From "+string.Format("{0:dd-MMM-yy}",dateRange.FromDate.ToDate()) + " till " +string.Format("{0:dd-MMM-yy}",dateRange.TillDate.ToDate()) ;

                        param[31] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DateRange", dateCriteria);

                    }
                    else
                        param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", objSubCompany.CompanyName.ToStr());

                }



                if (objTemplate.TemplateName.ToStr() == "Template3")
                {
                    minRows = 4;

                }



                string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";

                if (objTemplate.TemplateName.ToStr() == "Template4")
                {


                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice3.rdlc";

                }
                else if (objTemplate.TemplateName.ToStr() == "Template5")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";

                }
                else if (objTemplate.TemplateName.ToStr() == "Template6" || objTemplate.TemplateName.ToStr() == "Template7"
                    || objTemplate.TemplateName.ToStr() == "Template8" || objTemplate.TemplateName.ToStr() == "Template9"
                    || objTemplate.TemplateName.ToStr() == "Template10" || objTemplate.TemplateName.ToStr() == "Template11"
                    || objTemplate.TemplateName.ToStr() == "Template15" || objTemplate.TemplateName.ToStr() == "Template16"
                    || objTemplate.TemplateName.ToStr() == "Template17" || objTemplate.TemplateName.ToStr() == "Template18"
                    || objTemplate.TemplateName.ToStr() == "Template21" || objTemplate.TemplateName.ToStr() == "Template22"
                    || objTemplate.TemplateName.ToStr() == "Template23" || objTemplate.TemplateName.ToStr() == "Template24"
                    || objTemplate.TemplateName.ToStr() == "Template25" || objTemplate.TemplateName.ToStr() == "Template26"
                    || objTemplate.TemplateName.ToStr() == "Template27" || objTemplate.TemplateName.ToStr() == "Template30"
                   || objTemplate.TemplateName.ToStr() == "Template31" || objTemplate.TemplateName.ToStr() == "Template32"
                    || objTemplate.TemplateName.ToStr() == "Template45" || objTemplate.TemplateName.ToStr() == "Template48")
                {

                    minRows = 6;

                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";


                    if (objTemplate.TemplateName.ToStr() == "Template10" || objTemplate.TemplateName.ToStr() == "Template16"
                        || objTemplate.TemplateName.ToStr() == "Template17" || objTemplate.TemplateName.ToStr() == "Template18"
                        || objTemplate.TemplateName.ToStr() == "Template25" || objTemplate.TemplateName.ToStr() == "Template45"
                        || objTemplate.TemplateName.ToStr() == "Template27" || objTemplate.TemplateName.ToStr() == "Template30"
                   || objTemplate.TemplateName.ToStr() == "Template31" || objTemplate.TemplateName.ToStr() == "Template32"
                   || objTemplate.TemplateName.ToStr() == "Template48")
                        minRows = 4;


                    if (objTemplate.TemplateName.ToStr() == "Template9")
                        telNo += Environment.NewLine + "Fax No. " + objSubCompany.Fax;

                    else if (objTemplate.TemplateName.ToStr() == "Template15")
                    {
                        if (HasSplitByField == "Split By Department")
                        //HasSplitByField=="Split By Order No"
                        // if (HasSplitByDept)
                        {

                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "SplitByDept_rptCompanyInvoice.rdlc";
                        }
                        else if (HasSplitByField == "Split By Order No")
                        {
                            IsSplitByOrder = true;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "SplitByOrder_rptCompanyInvoice.rdlc";
                        }
                        else
                        {

                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";

                        }

                        telNo = "T: " + objSubCompany.TelephoneNo + " F: " + objSubCompany.Fax + " E: " + objSubCompany.EmailAddress;



                    }
                    else if (objTemplate.TemplateName.ToStr() == "Template16")
                    {
                        telNo = "Tel No. " + objSubCompany.TelephoneNo;
                        email = "Email. " + objSubCompany.EmailAddress;

                        website = objSubCompany.WebsiteUrl.ToStr();
                        Fax = "Fax. " + objSubCompany.Fax.ToStr();

                        accountNo = "Your Account No : " + objSubCompany.AccountNo;

                        if (objTemplate.TemplateName.ToStr() == "Template16" && this.ExportFileType.ToStr().ToLower() == "excel")
                        {
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "excel_rptCompanyInvoice.rdlc";
                        }

                    }
                    else if (objTemplate.TemplateName.ToStr() == "Template18")
                    {
                        telNo = "Telephone:" + objSubCompany.TelephoneNo + " - " + objSubCompany.MobileNo;
                        if (objTemplate.TemplateName.ToStr() == "Template18" && this.ExportFileType.ToStr().ToLower() == "excel")
                        {
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "excel_rptCompanyInvoice.rdlc";
                        }
                    }
                    else if (objTemplate.TemplateName.ToStr() == "Template45")
                    {
                        vatNumber = objSubCompany.CompanyVatNumber;
                    }
                    else if (objTemplate.TemplateName.ToStr() == "Template48")
                    {
                        if (objSubCompany.Fax.ToStr().Trim().Length > 0)
                            telNo += Environment.NewLine + "Fax No. " + objSubCompany.Fax;
                    }
                    else
                    {
                        if (objTemplate.TemplateName.ToStr() == "Template25")
                        {

                            telNo = "Vat Number. " + objSubCompany.CompanyVatNumber;

                        }
                        else
                        {

                            if (objSubCompany.Fax.ToStr().Trim().Length > 0)
                                telNo += Environment.NewLine + "Fax No. " + objSubCompany.Fax;


                            telNo += Environment.NewLine + "Email. " + objSubCompany.EmailAddress;


                        }
                    }


                    param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeLabel", data.AdminFeesLabel.ToStr());
                    param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeValue", data.AdminFees.ToStr());

                    if (objTemplate.TemplateName.ToStr() == "Template7" || objTemplate.TemplateName.ToStr() == "Template8" || objTemplate.TemplateName.ToStr() == "Template9")
                    {

                        param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_IBAN", "IBAN : " + objSubCompany.IbanNumber.ToStr());
                        param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BLC", "BLC : " + objSubCompany.BlcNumber.ToStr());

                        if (objTemplate.TemplateName.ToStr() == "Template8")
                        {
                            bank = bank.Replace("Bank : ", "").Trim();

                            vatNumber = vatNumber.Replace("VAT Number: ", "VAT NO.").Trim();
                        }
                    }
                    else if (objTemplate.TemplateName.ToStr() == "Template10" || objTemplate.TemplateName.ToStr() == "Template11" || 
                        objTemplate.TemplateName.ToStr() == "Template15" || objTemplate.TemplateName.ToStr() == "Template17" || 
                        objTemplate.TemplateName.ToStr() == "Template21" || objTemplate.TemplateName.ToStr() == "Template22" ||
                        objTemplate.TemplateName.ToStr() == "Template23" || objTemplate.TemplateName.ToStr() == "Template24"
                        || objTemplate.TemplateName.ToStr() == "Template25" || objTemplate.TemplateName.ToStr() == "Template26"
                        || objTemplate.TemplateName.ToStr() == "Template27" || objTemplate.TemplateName.ToStr() == "Template30"
                        || objTemplate.TemplateName.ToStr() == "Template31" || objTemplate.TemplateName.ToStr() == "Template32"
                        || objTemplate.TemplateName.ToStr() == "Template45" || objTemplate.TemplateName.ToStr() == "Template48")
                    {
                        param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_IBAN", "IBAN No : " + objSubCompany.IbanNumber.ToStr());
                        param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BLC", "SWIFT Code : " + objSubCompany.BlcNumber.ToStr());



                        if ((objTemplate.TemplateName.ToStr() == "Template10" || objTemplate.TemplateName.ToStr() == "Template32" || objTemplate.TemplateName.ToStr() == "Template45") && this.ExportFileType.ToStr().ToLower() == "excel")
                        {
                            if (HasSplitByField == "Split By Department")
                            {
                                //this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "SplitByDept_rptCompanyInvoice.rdlc";
                                if (this.ExportFileType.ToStr().ToLower() == "excel")
                                {
                                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "excel_SplitByDept_rptCompanyInvoice.rdlc";
                                }
                            }
                            else
                            {
                                this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "excel_rptCompanyInvoice.rdlc";
                                //excel_SplitByDept_rptCompanyInvoice.rdlc
                            }
                        }
                        else if (objTemplate.TemplateName.ToStr() == "Template17" && this.ExportFileType.ToStr().ToLower() == "excel")
                        {

                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "excel_rptCompanyInvoice.rdlc";
                        }
                        else  if (objTemplate.TemplateName.ToStr() == "Template25" && this.ExportFileType.ToStr().ToLower() == "excel")
                        {

                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "excel_rptCompanyInvoice.rdlc";
                        }
                        else if (objTemplate.TemplateName.ToStr() == "Template21" && this.ExportFileType.ToStr().ToLower() == "excel")
                        {
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "excel_rptCompanyInvoice.rdlc";
                        }
                        else if (objTemplate.TemplateName.ToStr() == "Template22" || objTemplate.TemplateName.ToStr() == "Template26")
                        {
                            telNo = objSubCompany.TelephoneNo;
                            email = objSubCompany.EmailAddress;
                            Fax = objSubCompany.Fax;
                            website = objSubCompany.WebsiteUrl;
                            vatNumber = objSubCompany.CompanyVatNumber;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";
                            param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);
                            param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", email);
                            param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Fax", Fax);
                            param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_EmergencyNo", objSubCompany.EmergencyNo);
                            //Report_Parameter_EmergencyNo
                            //Report_Parameter_Fax
                        
                        }
                        else if (objTemplate.TemplateName.ToStr() == "Template23")
                        {
                            companyNumber = objSubCompany.CompanyNumber;
                            vatNumber = objSubCompany.CompanyVatNumber;
                            bank = objSubCompany.BankName;
                            sortCode = objSubCompany.SortCode;
                            accountNo = objSubCompany.AccountNo;
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";
                            param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyNumber", companyNumber);


                            param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
                         
                            param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);
                        }
                       
                        else if ((objTemplate.TemplateName.ToStr() == "Template10" || objTemplate.TemplateName.ToStr() == "Template32" || objTemplate.TemplateName.ToStr() == "Template45" || objTemplate.TemplateName.ToStr() == "Template48") && HasSplitByField == "Split By Department")
                        {
                            this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "SplitByDept_rptCompanyInvoice.rdlc";
                        }

                    }


                }
                else if (objTemplate.TemplateName.ToStr() == "Template12" || objTemplate.TemplateName.ToStr() == "Template13" || objTemplate.TemplateName.ToStr() == "Template14" || objTemplate.TemplateName.ToStr() == "Template19")
                {
                    param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address.ToProperCase());

                    minRows = 10;

                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";

                }
              
                else if (objTemplate.TemplateName.ToStr() == "Template20")
                {
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";

                    objLogo.Add(new ClsLogo { ImageInBytes = objSubCompany.CompanyFooterLogo != null ? objSubCompany.CompanyFooterLogo.ToArray() : null });                  
                }
               
                else
                {

                    if (data.HasOrderNo.ToBool() && data.HasBookedBy.ToBool())
                    {
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";
                    }
                    else if (data.HasOrderNo.ToBool() == false && data.HasBookedBy.ToBool())
                    {
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice2.rdlc";
                    }
                    else if (data.HasOrderNo.ToBool() == false && data.HasBookedBy.ToBool() == false)
                    {
                        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice3.rdlc";
                    }
                    else if (data.HasOrderNo.ToBool() == true && data.HasBookedBy.ToBool() == false)
                    {

                        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice4.rdlc";

                    }

                }

                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);


                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountNo", accountNo);

                decimal netAmount = 0.00m;
                decimal totalExtra = 0.00m;

                decimal invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                //NC
                //Changes Area for VAT start here.
                //decimal invoiceGrandTotal2 = 0.00m;//= (netAmount + data.AdminFees.ToDecimal());
                //decimal NetCharges = 0.0m;



                decimal DriverCostNonVAT = 0.0m;
                decimal BusinessCharge = 0.0m;
                decimal VatOnBusinessCharge = 0.0m;
                decimal TotalGPB = 0.0m;
                //

                if (objTemplate.TemplateName.ToStr() == "Template13" || objTemplate.TemplateName.ToStr() == "Template14" || objTemplate.TemplateName.ToStr() == "Template24")
                {

                    if (objTemplate.TemplateName.ToStr() == "Template13" || objTemplate.TemplateName.ToStr() == "Template24")
                    {
                        netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                           .Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal());
                        //  NetCharges = this.DataSource.Where(c => c.VehicleType != "Saloon" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal());

                    }
                    else if (objTemplate.TemplateName.ToStr() == "Template14")
                    {
                        netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                           .Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());

                        //  NetCharges = this.DataSource.Where(c => c.VehicleType != "Saloon" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());
                    }

                    invoiceGrandTotal = netAmount;
                    minRows = 5;
                }
                else if (objTemplate.TemplateName.ToStr() == "Template17")
                {
                //    NetCharges = this.DataSource.Where(c => c.VehicleType != "Coach" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());
                //    netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                //.Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal() );

                 
                //    invoiceGrandTotal = NetCharges + data.AdminFees.ToDecimal();
                 
                //    invoiceGrandTotal2 = netAmount + data.AdminFees.ToDecimal();

                    netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                  .Sum(c => c.Charges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());


                    invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();



                }
                //else if(&& this.ExportFileType.ToStr().ToLower() == "excel")
                else if (objTemplate.TemplateName.ToStr() == "Template18")
                {

                    netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                    .Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal());
                    invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();
                    //NC
                    //valueAddedTax = (invoiceGrandTotal * 20) / 100;
                    DriverCostNonVAT = ((invoiceGrandTotal * 80) / 100);
                    BusinessCharge = ((invoiceGrandTotal * 20) / 100);

                    VatOnBusinessCharge = ((BusinessCharge) * 20 / 100);
                    TotalGPB = (DriverCostNonVAT + BusinessCharge + VatOnBusinessCharge);
                    // valueAddedTax
                }
                else if (objTemplate.TemplateName.ToStr() == "Template10" )
                {


                    //if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "!ndex_@ut0_$erv!ce$_Ltd")
                    //{


                    //    netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                    //              .Sum(c => c.Charges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());


                    //    invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                    //}

                    //else
                    //{

                        netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                       .Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal());

                        totalExtra = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                       .Sum(c => c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());

                        invoiceGrandTotal = netAmount;
                 //   }
                   
                }
                else if (objTemplate.TemplateName.ToStr() == "Template25"
                    || objTemplate.TemplateName.ToStr() == "Template27" || objTemplate.TemplateName.ToStr() == "Template32"
                    || objTemplate.TemplateName.ToStr() == "Template45" || objTemplate.TemplateName.ToStr() == "Template48")
                {


                    netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                   .Sum(c => c.Charges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());


                    invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                }
                else if (objTemplate.TemplateName.ToStr() == "Template21" || objTemplate.TemplateName.ToStr() == "Template30")
                {

                    netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                   .Sum(c => c.Charges.ToDecimal());


                    invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                }

                
                else
                {
                    netAmount = this.DataSource.Where(c => c.PaymentTypeId.ToInt() != 6)
                                    .Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal());


                    invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                }


                string vat = "0";
                decimal discountAmount = 0.00m;
                decimal valueAddedTax = 0.0m;
                decimal AdminFeesPercent = 0.00m;
                decimal AdminFees = 0.00m;
                string HasAdminFees = string.Empty;
                string HasDiscount = "0";
                string CompanyAccountNo = string.Empty;
                string VATType = "VAT:";
                string vatOnServiceChargeString = "VAT";

                if (companyId != null)
                {
                    Gen_Company objCompany = General.GetObject<Gen_Company>(c => c.Id == companyId);

                    if (objCompany != null)
                    {
                      

                        if (objCompany.DiscountPercentage.ToDecimal() > 0)
                        {
                            discountAmount = (netAmount * objCompany.DiscountPercentage.ToDecimal()) / 100;
                            DiscountPercent = objCompany.DiscountPercentage.ToDecimal();
                            HasDiscount = "1";
                        }

                        if (objCompany.AdminFees > 0) 
                        {
                           // invoiceGrandTotal = invoiceGrandTotal + totalExtra;

                            
                            AdminFees = (netAmount * objCompany.AdminFees.ToDecimal()) / 100;
                            AdminFeesPercent = objCompany.AdminFees.ToDecimal();
                            HasAdminFees = "1";


                            if(param.Count()>23)
                               param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeValue", string.Format("{0:c}", AdminFees).Substring(1) );


                        }


                        if (objCompany.HasVat.ToBool())
                        {


                            valueAddedTax = ((netAmount+AdminFees) * 20) / 100;
                            vat = "1";
                            if (objCompany.VatOnlyOnAdminFees.ToBool())
                            {
                                valueAddedTax = (AdminFees * 20) / 100;
                                vat = "1";
                                vatOnServiceChargeString = "VAT on Service Charge";
                                VATType = "VAT on Admin Fees:";
                            }

                        }

                        invoiceGrandTotal = netAmount + totalExtra+ AdminFees;


                        if (objTemplate.TemplateName.ToStr() == "Template15")
                        {
                            param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeLabel", objCompany.AdminFeeType.ToStr() == "Percent" ? objCompany.AdminFees.ToStr() + "%" : objCompany.AdminFees.ToStr());


                        }
                        else if (objTemplate.TemplateName.ToStr() == "Template26")
                        {
                            if (vat == "1")
                            {
                                VATType = "VAT:";
                                vatOnServiceChargeString = "VAT";

                                decimal subTotal = (invoiceGrandTotal - discountAmount);
                                valueAddedTax = (subTotal * 20) / 100;


                                invoiceGrandTotal = ((invoiceGrandTotal - discountAmount) + valueAddedTax);
                            }
                            else
                                invoiceGrandTotal = ((invoiceGrandTotal - discountAmount) + valueAddedTax);

                        }
                        else if (objTemplate.TemplateName.ToStr() == "Template45")
                        {
                            if (vat == "1")
                            {
                                VATType = "VAT:";
                                vatOnServiceChargeString = "VAT";

                                decimal subTotal = (invoiceGrandTotal - discountAmount);
                                valueAddedTax = (subTotal * 20) / 100;


                                invoiceGrandTotal = ((invoiceGrandTotal - discountAmount) + valueAddedTax);
                            }
                            else
                                invoiceGrandTotal = ((invoiceGrandTotal - discountAmount) + valueAddedTax);

                        }
                        else
                        {
                            invoiceGrandTotal = (invoiceGrandTotal + valueAddedTax) - discountAmount;
                        }

                        CompanyAccountNo = objCompany.AccountNo.ToStr().Trim();
                    }

                }
               
           
                //OC
                // invoiceGrandTotal = (invoiceGrandTotal + valueAddedTax) -discountAmount;
                //
                //NC
                // invoiceGrandTotal=(invoiceGrandTotal2+valueAddedTax)-discountAmount;
                //
                //Changes Area for VAT End here.

                bool departmentwise = this.DataSource.FirstOrDefault().DefaultIfEmpty().DepartmentWise.ToBool();
                bool costCenterWise = this.DataSource.FirstOrDefault().DefaultIfEmpty().HasOrderNo.ToBool();

                string grandTotal = string.Format("{0:c}", invoiceGrandTotal);
                grandTotal = grandTotal.Substring(1);

                string net = string.Format("{0:c}", netAmount);
                // string net = string.Format("{0:c}", NetCharges);
                net = net.Substring(1);


                string discount = string.Format("{0:c}", discountAmount);
                discount = discount.Substring(1);

                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", footer);



                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Discount", discount);

                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATNumber", vatNumber);

                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InvoiceTotal", grandTotal);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasVat", vat);

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VAT", valueAddedTax.ToStr());
                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasDepartment", departmentwise ? "1" : "0");

                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Net", net);

                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasCostCenter", costCenterWise ? "1" : "0");

                param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasBankDetails", hasBankDetails);
                if (objTemplate.TemplateName.ToStr() == "Template16")
                {
                    param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_IBAN", "IBAN No : " + objSubCompany.IbanNumber.ToStr());
                    param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BLC", "SWIFT Code : " + objSubCompany.BlcNumber.ToStr());
                    param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Fax", Fax);
                }
                if (objTemplate.TemplateName.ToStr() == "Template18")
                {
                    param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_IBAN", "IBAN No : " + objSubCompany.IbanNumber.ToStr());
                    param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BLC", "SWIFT Code : " + objSubCompany.BlcNumber.ToStr());
                    param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_NetAmount", TotalGPB.ToStr());
                    param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DriverCost", DriverCostNonVAT.ToStr());
                    param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BusinessCharge", BusinessCharge.ToStr());
                    param[29] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VatOnBusinessCharge", VatOnBusinessCharge.ToStr());
                    param[30] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyAccountNo", CompanyAccountNo);
                    //Parameters!Report_Parameter_DriverCost.Value
                }

                else if (objTemplate.TemplateName.ToStr() == "Template26")
                {
                    param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeLabel", "Admin Fees " + AdminFeesPercent + " %");
                    // param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_EmergencyNo", VATType);
                    param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasAdminFees", HasAdminFees);
                    param[29] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATLabel", VATType);
                    param[30] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasDiscount", HasDiscount);

                    param[31] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DiscountLabel", "Discount:");

                    minRows = 2;
                }
                else if (objTemplate.TemplateName.ToStr() == "Template30")
                {
                    param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", objSubCompany.TelephoneNo);
                }
                else if (objTemplate.TemplateName.ToStr() == "Template24")
                {
                    accountNo = objSubCompany.AccountNo.ToStr();
                    param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", objSubCompany.SortCode.ToStr());
                    param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountNo", accountNo);
                    param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_IBAN", objSubCompany.IbanNumber.ToStr());
                    param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BLC", objSubCompany.BlcNumber.ToStr());
                    vatNumber = objSubCompany.CompanyVatNumber;
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCompanyInvoice.rdlc";
                    param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);
                    param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", email);
                    param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Fax", Fax);
                    param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_EmergencyNo", vatOnServiceChargeString);
                    param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DiscountPercent", "Discount ("+DiscountPercent.ToStr()+"%)");
                    param[29] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasAdminFees", HasAdminFees);
                    param[30] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasDisCount", HasDiscount);
                    //Report_Parameter_HasAdminFees
                    param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeLabel", "Admin Fees %"+AdminFeesPercent);
                    param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeValue", AdminFees.ToStr());
                    minRows = 2;

                    //param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeLabel", objCompany.AdminFeeType.ToStr() == "Percent" ? objCompany.AdminFees.ToStr() + "%" : objCompany.AdminFees.ToStr());
                    //param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);
                }

                else if (objTemplate.TemplateName.ToStr() == "Template45")
                {
                    //param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AdminFeeLabel", "Admin Fees " + AdminFeesPercent + " %");
                    //// param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_EmergencyNo", VATType);
                    //param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasAdminFees", HasAdminFees);
                    //param[29] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATLabel", VATType);
                    //param[30] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasDiscount", HasDiscount);



                    param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_DiscountLabel", "Discount " +string.Format("{0:##0.##}", DiscountPercent)+"%");

                   // minRows = 2;
                }

                if (data != null && IsSplitByOrder == false)// && HasSplitByField != "Split By Order No")
                {


                    int cnt = this.DataSource.Count;

                    //  int minRows = 8;

                    if (cnt < minRows)
                    {
                        for (int i = 0; i < minRows - cnt; i++)
                        {
                            this.DataSource.Add(new vu_Invoice { Id = data.Id, BookingId = data.BookingId, HasBookedBy = data.HasBookedBy, HasOrderNo = data.HasOrderNo, HasPupilNo = data.HasPupilNo });

                        }

                    }

                }



               
                   reportViewer1.LocalReport.SetParameters(param);

                   this.vuInvoiceBindingSource.DataSource = this.DataSource;
          
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }


        private Invoice _ObjInvoice;

        public Invoice ObjInvoice
        {
            get { return _ObjInvoice; }
            set { _ObjInvoice = value; }
        }


        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(this.reportViewer1.LocalReport.DataSources[2]);
        }

        public bool HasSplitByDept;
        public string HasSplitByField;

        private string _ExportFileType;

        public string ExportFileType
        {
            get { return _ExportFileType; }
            set { _ExportFileType = value; }
        }


        public frmInvoiceReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmInvoiceReport_Load);

            ComboFunctions.FillCompanyCombo(ddlCompany);


            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());

        }


        private void InitialializeAllReport()
        {

            this.pnlCriteria = new Telerik.WinControls.UI.RadPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.btnEmailCurrent = new Telerik.WinControls.UI.RadButton();
            this.btnEmailAll = new Telerik.WinControls.UI.RadButton();
            this.btnPrintAll = new Telerik.WinControls.UI.RadButton();
            this.cbAllDrivers = new System.Windows.Forms.CheckBox();
            this.grdDriverCommission = new UI.MyGridView();
            this.txtPreviewlabel = new System.Windows.Forms.Label();
            this.btnPrev = new Telerik.WinControls.UI.RadButton();
            this.radDropDownList1 = new Telerik.WinControls.UI.RadDropDownList();
            this.ddlDriver = new System.Windows.Forms.Label();
            this.btnNext = new Telerik.WinControls.UI.RadButton();
            this.myDatePicker1 = new UI.MyDatePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.myDatePicker2 = new UI.MyDatePicker();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();



            this.Controls.Add(this.pnlCriteria);

            ((System.ComponentModel.ISupportInitialize)(this.pnlCriteria)).BeginInit();
            this.pnlCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission.MasterTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();







            // 
            // pnlCriteria
            // 
            this.pnlCriteria.BackColor = System.Drawing.Color.GhostWhite;
            this.pnlCriteria.Controls.Add(this.label1);
            this.pnlCriteria.Controls.Add(this.txtSubject);
            this.pnlCriteria.Controls.Add(this.btnEmailCurrent);
            this.pnlCriteria.Controls.Add(this.btnEmailAll);
            this.pnlCriteria.Controls.Add(this.btnPrintAll);
            this.pnlCriteria.Controls.Add(this.cbAllDrivers);
            this.pnlCriteria.Controls.Add(this.grdDriverCommission);
            this.pnlCriteria.Controls.Add(this.txtPreviewlabel);
            this.pnlCriteria.Controls.Add(this.btnPrev);
            this.pnlCriteria.Controls.Add(this.radDropDownList1);
            this.pnlCriteria.Controls.Add(this.ddlDriver);
            this.pnlCriteria.Controls.Add(this.btnNext);
            this.pnlCriteria.Controls.Add(this.myDatePicker1);
            this.pnlCriteria.Controls.Add(this.radLabel3);
            this.pnlCriteria.Controls.Add(this.myDatePicker2);
            this.pnlCriteria.Controls.Add(this.radLabel2);
            this.pnlCriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCriteria.Location = new System.Drawing.Point(0, 38);
            this.pnlCriteria.Name = "pnlCriteria";
            this.pnlCriteria.Size = new System.Drawing.Size(1308, 143);
            this.pnlCriteria.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(702, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 18);
            this.label1.TabIndex = 123;
            this.label1.Text = "Email Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(810, 116);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(221, 26);
            this.txtSubject.TabIndex = 122;
            // 
            // btnEmailCurrent
            // 
            this.btnEmailCurrent.Location = new System.Drawing.Point(768, 36);
            this.btnEmailCurrent.Name = "btnEmailCurrent";
            this.btnEmailCurrent.Size = new System.Drawing.Size(113, 52);
            this.btnEmailCurrent.TabIndex = 120;
            this.btnEmailCurrent.Text = "Email Current Record";
            this.btnEmailCurrent.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailCurrent.GetChildAt(0))).Text = "Email Current Record";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailCurrent.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnEmailAll
            // 
            this.btnEmailAll.Location = new System.Drawing.Point(918, 36);
            this.btnEmailAll.Name = "btnEmailAll";
            this.btnEmailAll.Size = new System.Drawing.Size(113, 52);
            this.btnEmailAll.TabIndex = 119;
            this.btnEmailAll.Text = "Email All";
            this.btnEmailAll.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnEmailAll.GetChildAt(0))).Text = "Email All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnEmailAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.Location = new System.Drawing.Point(1117, 71);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(113, 43);
            this.btnPrintAll.TabIndex = 118;
            this.btnPrintAll.Text = "Print All";
            this.btnPrintAll.TextWrap = true;
            this.btnPrintAll.Visible = false;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrintAll.GetChildAt(0))).Text = "Print All";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrintAll.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // cbAllDrivers
            // 
            this.cbAllDrivers.AutoSize = true;
            this.cbAllDrivers.Checked = true;
            this.cbAllDrivers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAllDrivers.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbAllDrivers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAllDrivers.Location = new System.Drawing.Point(0, 0);
            this.cbAllDrivers.Name = "cbAllDrivers";
            this.cbAllDrivers.Size = new System.Drawing.Size(1308, 20);
            this.cbAllDrivers.TabIndex = 117;
            this.cbAllDrivers.Text = "Select All";
            this.cbAllDrivers.UseVisualStyleBackColor = true;
            // 
            // grdDriverCommission
            // 
            this.grdDriverCommission.AutoCellFormatting = false;
            this.grdDriverCommission.EnableCheckInCheckOut = false;
            this.grdDriverCommission.HeaderRowBackColor = System.Drawing.Color.SteelBlue;
            this.grdDriverCommission.HeaderRowBorderColor = System.Drawing.Color.MidnightBlue;
            this.grdDriverCommission.Location = new System.Drawing.Point(8, 21);
            // 
            // grdDriverCommission
            // 
            this.grdDriverCommission.MasterTemplate.AllowAddNewRow = false;
            this.grdDriverCommission.MasterTemplate.AllowEditRow = false;

            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();

            gridViewTextBoxColumn1.HeaderText = "Id";
            gridViewTextBoxColumn1.Name = "Id";
            gridViewTextBoxColumn2.HeaderText = "Invoice #";
            gridViewTextBoxColumn2.Name = "InvoiceNo";
            gridViewTextBoxColumn2.Width = 110;
            gridViewTextBoxColumn3.HeaderText = "Account";
            gridViewTextBoxColumn3.Name = "Account";
            gridViewTextBoxColumn3.Width = 160;
            gridViewTextBoxColumn4.HeaderText = "Email";
            gridViewTextBoxColumn4.Name = "Email";
            gridViewTextBoxColumn4.Width = 80;
            this.grdDriverCommission.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.grdDriverCommission.Name = "grdDriverCommission";
            this.grdDriverCommission.PKFieldColumnName = "";
            this.grdDriverCommission.ShowGroupPanel = false;
            this.grdDriverCommission.ShowImageOnActionButton = true;
            this.grdDriverCommission.Size = new System.Drawing.Size(461, 120);
            this.grdDriverCommission.TabIndex = 116;
            this.grdDriverCommission.Text = "myGridView1";
            // 
            // txtPreviewlabel
            // 
            this.txtPreviewlabel.AutoSize = true;
            this.txtPreviewlabel.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreviewlabel.Location = new System.Drawing.Point(517, 101);
            this.txtPreviewlabel.Name = "txtPreviewlabel";
            this.txtPreviewlabel.Size = new System.Drawing.Size(159, 23);
            this.txtPreviewlabel.TabIndex = 20;
            this.txtPreviewlabel.Text = "Preview 1 of 10";
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(475, 45);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(129, 43);
            this.btnPrev.TabIndex = 19;
            this.btnPrev.Text = "<< Previous  ";
            this.btnPrev.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnPrev.GetChildAt(0))).Text = "<< Previous  ";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnPrev.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // radDropDownList1
            // 
            this.radDropDownList1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radDropDownList1.Location = new System.Drawing.Point(140, 15);
            this.radDropDownList1.Name = "radDropDownList1";
            this.radDropDownList1.Size = new System.Drawing.Size(226, 26);
            this.radDropDownList1.TabIndex = 18;
            // 
            // ddlDriver
            // 
            this.ddlDriver.AutoSize = true;
            this.ddlDriver.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlDriver.Location = new System.Drawing.Point(59, 18);
            this.ddlDriver.Name = "ddlDriver";
            this.ddlDriver.Size = new System.Drawing.Size(46, 18);
            this.ddlDriver.TabIndex = 17;
            this.ddlDriver.Text = "Driver";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(618, 45);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(113, 43);
            this.btnNext.TabIndex = 8;
            this.btnNext.Text = "Next >>";
            this.btnNext.TextWrap = true;
            ((Telerik.WinControls.UI.RadButtonElement)(this.btnNext.GetChildAt(0))).Text = "Next >>";
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextWrap = true;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.Primitives.TextPrimitive)(this.btnNext.GetChildAt(0).GetChildAt(1).GetChildAt(1))).Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // myDatePicker1
            // 
            this.myDatePicker1.CustomFormat = "dd/MM/yyyy";
            this.myDatePicker1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myDatePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.myDatePicker1.Location = new System.Drawing.Point(133, 43);
            this.myDatePicker1.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.myDatePicker1.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.myDatePicker1.Name = "myDatePicker1";
            this.myDatePicker1.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.myDatePicker1.Size = new System.Drawing.Size(114, 24);
            this.myDatePicker1.TabIndex = 3;
            this.myDatePicker1.TabStop = false;
            this.myDatePicker1.Text = "myDatePicker2";
            this.myDatePicker1.Value = null;
            this.myDatePicker1.Visible = false;
            // 
            // radLabel3
            // 
            this.radLabel3.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(12, 45);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(116, 22);
            this.radLabel3.TabIndex = 2;
            this.radLabel3.Text = "To Invoice Date";
            this.radLabel3.Visible = false;
            // 
            // myDatePicker2
            // 
            this.myDatePicker2.CustomFormat = "dd/MM/yyyy";
            this.myDatePicker2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myDatePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.myDatePicker2.Location = new System.Drawing.Point(187, 40);
            this.myDatePicker2.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.myDatePicker2.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.myDatePicker2.Name = "myDatePicker2";
            this.myDatePicker2.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.myDatePicker2.Size = new System.Drawing.Size(114, 24);
            this.myDatePicker2.TabIndex = 1;
            this.myDatePicker2.TabStop = false;
            this.myDatePicker2.Text = "myDatePicker1";
            this.myDatePicker2.Value = null;
            this.myDatePicker2.Visible = false;
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(45, 42);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(133, 22);
            this.radLabel2.TabIndex = 0;
            this.radLabel2.Text = "From Invoice Date";
            this.radLabel2.Visible = false;


            ((System.ComponentModel.ISupportInitialize)(this.pnlCriteria)).EndInit();
            this.pnlCriteria.ResumeLayout(false);
            this.pnlCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnEmailAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrintAll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverCommission)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDropDownList1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myDatePicker2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();

        }

        public frmInvoiceReport(List<ClsInvoicePrintView> lst, DateTime from, DateTime till)
        {
            InitializeComponent();

            InitialializeAllReport();


            GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
            col.Width = 40;
            col.AutoSizeMode = BestFitColumnMode.None;
            col.HeaderText = "";
            col.Name = "Check";
            grdDriverCommission.Columns.Add(col);


            this.LIST = lst;
            this.Shown += FrmInvoiceReport_Shown;
          //  this.Shown += new EventHandler(frmDriverCommisionTransactionExpensesReport2_Shown);




        //    lblCriteria.Text = "From : " + string.Format("{0:dd/MM/yyyy}", from) + " to " + string.Format("{0:dd/MM/yyyy}", till);

        }

        private List<ClsInvoicePrintView> LIST;

        private void FrmInvoiceReport_Shown(object sender, EventArgs e)
        {
            try
            {

                grdDriverCommission.RowCount = LIST.Count;
                for(int i=0; i<LIST.Count; i++)
                {
                    grdDriverCommission.Rows[i].Cells["Id"].Value = LIST[i].Id;
                    grdDriverCommission.Rows[i].Cells["InvoiceNo"].Value = LIST[i].InvoiceNo;
                    grdDriverCommission.Rows[i].Cells["Account"].Value = LIST[i].Account;
                    grdDriverCommission.Rows[i].Cells["Email"].Value = LIST[i].Email;

                }

             
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.ControlBox = true;
                if (grdDriverCommission.Rows.Count > 0)
                {



                    grdDriverCommission.Columns["Id"].IsVisible = false;
             //       grdDriverCommission.Columns["DriverId"].IsVisible = false;

               //     grdDriverCommission.Columns["VAT"].IsVisible = false;

                    grdDriverCommission.AllowAutoSizeColumns = true;
                    grdDriverCommission.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


                    grdDriverCommission.Columns["Check"].Width = 60;
                 


                    grdDriverCommission.Rows.ToList().ForEach(c => c.Cells["Check"].Value = true);
                    grdDriverCommission.ShowFilteringRow = true;
                    grdDriverCommission.EnableFiltering = true;

                    txtPreviewlabel.Text = "Preview " + "1" + " of " + grdDriverCommission.Rows.Count;


                    grdDriverCommission.CurrentRow = grdDriverCommission.Rows.FirstOrDefault();


                    var row = grdDriverCommission.Rows.FirstOrDefault();

                    if (row != null)
                    {
                        SetDataSourceAndGenerateReport(row.Cells["Id"].Value.ToInt(),0);
                    }


                    btnNext.Click += btnNext_Click;
                    btnPrev.Click += btnPrev_Click;
                    btnEmailAll.Click += btnEmailAll_Click;
                    btnEmailCurrent.Click += btnEmailCurrent_Click;


                    grdDriverCommission.CellClick += grdDriverCommission_CellDoubleClick;
                    cbAllDrivers.CheckedChanged += cbAllDrivers_CheckedChanged;
                }
            }
            catch (Exception ex)
            {


            }


        }

      

        private void cbAllDrivers_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllDrivers.Checked == true)
            {
                if (grdDriverCommission.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDriverCommission.Rows.Count; i++)
                    {
                        grdDriverCommission.Rows[i].Cells["Check"].Value = true;//..CurrentCell.Value;
                    }
                }
            }
            else if (cbAllDrivers.Checked == false)
            {
                if (grdDriverCommission.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDriverCommission.Rows.Count; i++)
                    {
                        grdDriverCommission.Rows[i].Cells["Check"].Value = false;//..CurrentCell.Value;

                    }
                }
            }
        }



        private void SetDataSourceAndGenerateReport(int Id, decimal VAT)
        {

            if (Id > 0)
            {
            
                var list = General.GetQueryable<vu_Invoice>(a => a.Id == Id).OrderBy(c => c.PickupDate).ToList();
                int count = list.Count;
                this.ObjInvoice = General.GetObject<Invoice>(c => c.Id == Id);
                this.DataSource = list;
           

                GenerateReport();







            }

        }

        private void grdDriverCommission_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
                if (e.Row != null && e.Row is GridViewDataRowInfo)
                {
                    int TransId = e.Row.Cells["Id"].Value.ToInt();

                    SetDataSourceAndGenerateReport(TransId, 0);


                    var row = grdDriverCommission.Rows.FirstOrDefault(c => c.Cells["Id"].Value.ToLong() == TransId);

                    if (row != null)
                    {

                        txtPreviewlabel.Text = "Preview " + (row.Index + 1).ToInt() + " of " + grdDriverCommission.Rows.Count;
                    }
                }
            }
            catch
            {

            }
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = 0;


                if (grdDriverCommission.CurrentRow == null || (grdDriverCommission.CurrentRow is GridViewDataRowInfo) == false)
                    grdDriverCommission.CurrentRow = grdDriverCommission.Rows[rowIndex];


                if (grdDriverCommission.CurrentRow != null)
                {
                    if (grdDriverCommission.CurrentRow is GridViewDataRowInfo && (grdDriverCommission.CurrentRow.Index + 1) < grdDriverCommission.Rows.Count)
                    {
                        rowIndex = grdDriverCommission.CurrentRow.Index + 1;
                        grdDriverCommission.CurrentRow = grdDriverCommission.Rows[rowIndex];
                    }
                    else
                        rowIndex = grdDriverCommission.Rows.Count - 1;
                }


                if (rowIndex >= 0)
                {

                    var row = grdDriverCommission.Rows.FirstOrDefault(c => c.Index == rowIndex);

                    if (row != null)
                    {

                        int TransId = row.Cells["Id"].Value.ToInt();

                        SetDataSourceAndGenerateReport(TransId, 0);
                    }


                    txtPreviewlabel.Text = "Preview " + (rowIndex + 1).ToInt() + " of " + grdDriverCommission.Rows.Count;

                }




            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);


            }

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {

                int rowIndex = 0;




                if (grdDriverCommission.CurrentRow != null && grdDriverCommission.CurrentRow is GridViewDataRowInfo)// && (grdDriverCommission.CurrentRow.Index + 1) < grdDriverCommission.Rows.Count)
                {
                    rowIndex = grdDriverCommission.CurrentRow.Index - 1;

                    if (rowIndex == -1)
                        rowIndex = 0;


                    grdDriverCommission.CurrentRow = grdDriverCommission.Rows[rowIndex];


                }


                if (rowIndex >= 0)
                {

                    var row = grdDriverCommission.Rows.FirstOrDefault(c => c.Index == rowIndex);

                    if (row != null)
                    {

                        int TransId = row.Cells["Id"].Value.ToInt();

                        SetDataSourceAndGenerateReport(TransId,0);
                    }


                    txtPreviewlabel.Text = "Preview " + (rowIndex + 1).ToInt() + " of " + grdDriverCommission.Rows.Count;



                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);


            }
        }



        //private void btnPrintCurrent_Click(object sender, EventArgs e)
        //{

        //    if (grdDriverCommission.CurrentRow != null && grdDriverCommission.CurrentRow is GridViewDataRowInfo)
        //    {
        //        PrintDocument(grdDriverCommission.CurrentRow.Cells["Id"].Value.ToLong());

        //    }
        //}

        private void btnEmailCurrent_Click(object sender, EventArgs e)
        {
            if (grdDriverCommission.CurrentRow != null && grdDriverCommission.CurrentRow is GridViewDataRowInfo)
            {
                EmailInvoices(grdDriverCommission.CurrentRow.Cells["Id"].Value.ToLong());

            }
        }


        private void btnEmailAll_Click(object sender, EventArgs e)
        {
            EmailInvoices(0);
        }

        private void EmailInvoices(long TransId)
        {

            bool IsSuccess = false;
            try
            {
                string subject = txtSubject.Text.Trim();

                if (string.IsNullOrEmpty(subject))
                {
                    ENUtils.ShowMessage("Required : Email Subject");
                    return;
                }

                List<GridViewRowInfo> rows = null;


                if (TransId == 0)
                {


                    rows = grdDriverCommission.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).ToList();
                }
                else
                    rows = grdDriverCommission.Rows.Where(c => c.Cells["Id"].Value.ToLong() == TransId).ToList();


                List<long> invoiceIds = new List<long>();

                if (TransId == 0)
                {

                    invoiceIds = rows.Select(c => c.Cells["Id"].Value.ToLong()).ToList<long>();
                }
                else
                {

                    invoiceIds = new List<long>();
                    invoiceIds.Add(TransId);

                }



                if (invoiceIds.Count > 0)
                {
                    frmInvoiceReport frm = new frmInvoiceReport();
                    frm.reportViewer1.Tag = "invoice";
                   


                    frmEmail frmEmail = new frmEmail(null, "", "");

                  //  Fleet_Driver objDriver = null;
                    foreach (var item in rows)
                    {
                    




                        //   frm.HasSplitByField = ddlSplitBy.Text;
                        frm.ObjInvoice = this.ObjInvoice;

                        var list = General.GetQueryable<vu_Invoice>(a => a.Id == item.Cells["Id"].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                        int count = list.Count;

                        frm.DataSource = list;



                        frm.GenerateReport();

                        string email = item.Cells["Email"].Value.ToStr();
                        if (!string.IsNullOrEmpty(email))
                        {
                            frm.SendEmailInternally(frmEmail,txtSubject.Text, item.Cells["InvoiceNo"].Value.ToStr(), item.Cells["Email"].Value.ToStr());

                            IsSuccess = true;
                           // IsSuccess = frm.SendEmailInternally(frmEmail, subject, objDriver.DriverNo.ToStr().Trim(), email);
                        }

                      



                    }


                    if (frmEmail != null && frmEmail.IsDisposed == false)
                    {
                        frmEmail.Close();
                        GC.Collect();

                    }


                    if (IsSuccess)
                    {

                        RadDesktopAlert alert = new RadDesktopAlert();
                        alert.ContentText = "Email has been sent successfully";
                        alert.Show();
                    }
                    // ENUtils.ShowMessage("Email has been sent successfully");

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }





        }

        public void ExportReport(string invoiceNo, string exportTo)
        {



            SaveFileDialog saveFileDlg = new SaveFileDialog();

            if (exportTo.ToLower() == "pdf")
                saveFileDlg.Filter = "PDF File (*.pdf)|*.pdf";
            else
                saveFileDlg.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

            saveFileDlg.Title = "Save File";
            saveFileDlg.FileName = "Invoice-" + invoiceNo;


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

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name == "btnUpdate")
            {

                GridViewRowInfo row = gridCell.RowInfo;

                if (row is GridViewDataRowInfo)
                {

                    long id = row.Cells[COLS.ID].Value.ToLong();
                    decimal fare = row.Cells[COLS.Charges].Value.ToDecimal();
                    decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                    decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                    decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                    decimal meetAndGreet = row.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                    decimal CongtionCharge = row.Cells[COLS.CongtionCharge].Value.ToDecimal();
                    decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();


                    BookingBO objMaster = new BookingBO();
                    objMaster.GetByPrimaryKey(id);

                    if (objMaster.Current != null)
                    {
                        objMaster.Current.FareRate = fare;
                        objMaster.Current.ParkingCharges = parking;
                        objMaster.Current.WaitingCharges = waiting;
                        objMaster.Current.ExtraDropCharges = extraDrop;
                        objMaster.Current.MeetAndGreetCharges = meetAndGreet;
                        objMaster.Current.CongtionCharges = CongtionCharge;
                        objMaster.Current.TotalCharges = TotalCharges;


                        objMaster.Save();

                        ViewReport();
                    }


                }


            }

        }

        void frmInvoiceReport_Load(object sender, EventArgs e)
        {


        }

        private void AddUpdateColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 50;

            col.Name = "btnUpdate";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Update";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }


        private void ViewReport()
        {
            int companyId = ddlCompany.SelectedValue.ToInt();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

            string error = string.Empty;
            if (companyId == 0)
            {
                error += "Required : Company";
            }

            if (fromDate == null)
            {
                if (string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : From Date";
            }

            if (tillDate == null)
            {
                if (string.IsNullOrEmpty(error))
                    error += Environment.NewLine;

                error += "Required : To Date";


            }

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;

            }

        //    lblCriteria.Text = "Account Invoice Report Related to '" + ddlCompany.Text.ToStr() + "', Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);




            var list = General.GetQueryable<vu_Invoice>(a => a.CompanyId == companyId && a.InvoiceDate >= fromDate && a.InvoiceDate <= tillDate).ToList();
            int count = list.Count;

            this.DataSource = list;


            GenerateReport();




        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {

            ViewReport();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        public void SendEmail(string invoiceNo)
        {
            General.ShowEmailForm(reportViewer1, "Account Invoice # " + invoiceNo);
        }


        public void SendEmail(string invoiceNo, string email)
        {
            reportViewer1.Tag = "invoice";

            General.ShowEmailForm(reportViewer1, "Account Invoice # " + invoiceNo, email, objSubCompany,false);

        }

        public void SendEmailInternally(frmEmail frmE, string subject, string invoiceNo, string email)
        {


            frmE.ReportViewer1 = this.reportViewer1;
            frmE.FileTitle = "Account Invoice # " + invoiceNo.ToStr();
            frmE.EmailSubject = subject;
            frmE.ToEmail = email;
            frmE.txtTo.Text = email;
            frmE.txtSubject.Text = subject;
            frmE.txtAttachment.Text = "Account Invoice # " + invoiceNo;

       
            frmE.SendEmail(true);

            //         General.ShowEmailForm(reportViewer1, "Account Invoice # " + invoiceNo, email);

        }



        private void ExportExportDirect()
        {


        }



    }
}
