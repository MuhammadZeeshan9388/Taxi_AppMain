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
    public partial class frmDriverRentTransactionExpensesReport2 : UI.SetupBase
    {



        private List<vu_DriverRentExpense> _DataSource;

        public List<vu_DriverRentExpense> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        private List<vu_FleetDriverRentExpense> _DataSource2;

        public List<vu_FleetDriverRentExpense> DataSource2
        {
            get { return _DataSource2; }
            set { _DataSource2 = value; }
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

                if (objSubcompany == null)
                    objSubcompany = AppVars.objSubCompany;
                
                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[27];

                string address = objSubcompany.Address;
                string telNo = string.Empty;

                string sortCode = objSubcompany.SortCode.ToStr();
                string accountNo = objSubcompany.AccountNo.ToStr();
                string accountTitle = objSubcompany.AccountTitle.ToStr();
                string bank = objSubcompany.BankName.ToStr();

                string hasBankDetails = "1";
                if (string.IsNullOrEmpty(sortCode) && string.IsNullOrEmpty(accountNo) && string.IsNullOrEmpty(accountTitle)
                    && string.IsNullOrEmpty(bank))
                {
                    hasBankDetails = "0";
                }

                if (!string.IsNullOrEmpty(sortCode))
                    sortCode = "Sort Code : " + sortCode;


                if (!string.IsNullOrEmpty(accountTitle))
                    accountTitle = "Account Title : " + accountTitle;


                string website = objSubcompany.WebsiteUrl.ToStr();
                if (!string.IsNullOrEmpty(website))
                {
                    website += " , ";
                }

                website += "Email:" + objSubcompany.EmailAddress.ToStr();


                string companyNumber = objSubcompany.CompanyNumber.ToStr();
                if (!string.IsNullOrEmpty(companyNumber))
                {
                    companyNumber = "Company Number: " + companyNumber;
                }

                string vatNumber = objSubcompany.CompanyVatNumber.ToStr();
           

                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);

                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", objSubcompany.WebsiteUrl.ToStr());

                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MobileNo", "Mobile: " + objSubcompany.EmergencyNo.ToStr());
                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);
                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", "Email: " + objSubcompany.EmailAddress.ToStr());

                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyNumber", companyNumber);
                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATNumber", vatNumber);


                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle", accountTitle);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = objSubcompany.CompanyLogo != null ? objSubcompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", objSubcompany.CompanyName.ToStr());

                param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeading", objSubcompany.CompanyName.ToStr());






                int? driverId = this.DataSource.FirstOrDefault().DefaultIfEmpty().DriverId;

                var data = this.DataSource.FirstOrDefault().DefaultIfEmpty();








                telNo = "Tel No. " + objSubcompany.TelephoneNo;

                if (!string.IsNullOrEmpty(accountNo))
                    accountNo = "Account No : " + accountNo;



                //var objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);

                //string className = "Taxi_AppMain.ReportDesigns.";
                //if (objTemplate!=null)
                //{

                //    if (objTemplate.TemplateName == "Template1")
                //    {

                //        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverRentTrasaction.rdlc";
                //    }
                //    else if (objTemplate.TemplateName == "Template2")
                //    {
                //        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverRentTrasaction2.rdlc";


                //    }

                //    else if (objTemplate.TemplateName == "Template3")
                //    {
                //        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverRentTrasaction3.rdlc";
                //    }

                //    else if (objTemplate.TemplateName == "Template4")
                //    {
                //        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverRentTrasaction4.rdlc";
                //    }

                //}


                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);


                param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountNo", accountNo);




                string vat = "0";
                decimal discountAmount = 0.00m;
                decimal valueAddedTax = 0.0m;



                string discount = string.Format("{0:c}", discountAmount);
                discount = discount.Substring(1);

                string grandTotal = "";

                param[17] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Discount", discount);


                param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InvoiceTotal", grandTotal);

                param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasVat", vat);

                param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VAT", valueAddedTax.ToStr());
                param[11] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasDepartment", "0");

                param[12] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Net", "0");

                param[13] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasCostCenter", "0");

                param[19] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasBankDetails", hasBankDetails);





                int cnt = this.DataSource.Count;
                //NC
                //decimal JobTotal = 0; 
                //   string AccountBooking = string.Empty; 
                //  string CashBooking = string.Empty;

                //    AccountBooking = string.Format("{0:£ #.##}", this.DataSource.Where(c => c.CompanyId != null).Sum(c => c.FareRate.ToDecimal()));
                //    CashBooking = string.Format("{0:£ #.##}", this.DataSource.Where(c => c.CompanyId == null).Sum(c => c.FareRate.ToDecimal()));
                //    JobTotal = this.DataSource.Sum(c => c.FareRate.Value.ToDecimal()+ c.WaitingCharges.ToDecimal());



                //param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountJobTotal", AccountBooking);
                //param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CashJobTotal", CashBooking);







                //comment


                //string BalanceType = string.Empty;
                //int DriverId = this.DataSource.FirstOrDefault().DefaultIfEmpty().DriverId.ToInt();
                //int Id = this.DataSource.FirstOrDefault().DefaultIfEmpty().Id.ToInt(); ;
                //var query=General.GetObject<DriverRent>(c=>c.DriverId==DriverId && c.Id<Id);

                //string StatementDate = string.Empty;
                //if (query == null)
                //{
                //    BalanceType = "Initial Balance";
                //    StatementDate = string.Format("{0:dd/MM}", this.DataSource.FirstOrDefault().DefaultIfEmpty().TransDate);
                //}
                //else
                //{
                //    BalanceType = "Balance from statement "+query.TransNo;
                //    StatementDate = string.Format("{0:dd/MM}", query.TransDate);
                //}

                //param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_jobCount", cnt.ToStr());
                //param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceType", BalanceType);
                //param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementDate", StatementDate);


                // string balance=string.Empty;

                //decimal bal=this.DataSource.FirstOrDefault().DefaultIfEmpty().Balance.ToDecimal();


                //if(bal>=0)
                //{

                //    balance = "You are due to Pay £" + string.Format("{0:f2}", bal);
                //}
                //else
                //{
                //    balance = "You are due to receive £" + string.Format("{0:f2}", bal);

                //    balance = balance.Replace("-", "").Trim();

                //}


                //param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Balance", balance);
                //reportViewer1.LocalReport.SetParameters(param);
                ////this.vu_DriverRentBindingSource.DataSource = this.DataSource;
                //this.vu_DriverRentExpenseBindingSource.DataSource = this.DataSource;
                //this.vu_FleetDriverRentExpenseBindingSource.DataSource = this.DataSource2;
                //this.reportViewer1.ZoomPercent = 100;
                //this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                //this.reportViewer1.RefreshReport();


                string BalanceType = string.Empty;
                int DriverId = 0;
                if (this.DataSource.Count > 0)
                {
                    DriverId = this.DataSource.FirstOrDefault().DriverId.ToInt();
                    int Id = this.DataSource.FirstOrDefault().Id.ToInt(); ;
                    var query = General.GetObject<DriverRent>(c => c.DriverId == DriverId && c.Id < Id);

                    string StatementDate = string.Empty;
                    if (query == null)
                    {
                        BalanceType = "Initial Balance";
                        StatementDate = string.Format("{0:dd/MM}", this.DataSource.FirstOrDefault().TransDate);
                    }
                    else
                    {
                        BalanceType = "Balance from statement " + query.TransNo;
                        StatementDate = string.Format("{0:dd/MM}", query.TransDate);
                    }
                    //  string Commision = (JobTotal * DriverCommision / 100).ToStr();
                    param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_jobCount", cnt.ToStr());
                    param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceType", BalanceType);
                    // param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountExpenses", AccountExpenses.ToStr());
                    param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementDate", StatementDate);

                    //decimal AccountTotal = (this.DataSource.Sum(c => c.AccountJobsTotal)).ToDecimal();
                    //decimal AccountCommision=(25*AccountTotal/100);
                    //decimal CashTotal = (this.DataSource.Sum(c => c.CashJobsTotal)).ToDecimal();
                    //decimal CashCommision=(25*CashTotal/100);
                    //decimal TotalDebit = this.DataSource2.Sum(c => c.Debit).ToDecimal();
                    //decimal TotalCredit = this.DataSource2.Sum(c=>c.Credit).ToDecimal();

                  

                    decimal bal = this.DataSource.FirstOrDefault().DefaultIfEmpty().Balance.ToDecimal();

                    string balance = "Balance Due £" + string.Format("{0:f2}", bal);

                    //if (bal >= 0)
                    //{

                    //    balance = "You are due to Pay £" + string.Format("{0:f2}", bal);
                    //}
                    //else
                    //{
                    //    balance = "You are due to receive £" + string.Format("{0:f2}", bal);

                    //    balance = balance.Replace("-", "").Trim();

                    //}


                    param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Balance", balance);
                }
                else
                {
                    param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_jobCount", cnt.ToStr());
                    param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceType", BalanceType);
                    // param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountExpenses", AccountExpenses.ToStr());
                    param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementDate", "");
                    param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Balance", "");
                }
                reportViewer1.LocalReport.SetParameters(param);
                //this.vu_DriverRentBindingSource.DataSource = this.DataSource;
                this.vu_DriverRentExpenseBindingSource.DataSource = this.DataSource;
                this.vu_FleetDriverRentExpenseBindingSource.DataSource = this.DataSource2;
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }
        int IsCheck = 0;

        private string _CompanyHeader;

        public string CompanyHeader
        {
            get { return _CompanyHeader; }
            set { _CompanyHeader = value; }
        }


        public frmDriverRentTransactionExpensesReport2(int val)
        {
            InitializeComponent();

            IsCheck = val;
         //   this.Load +=new EventHandler(frmInvoiceReport_Load);

          //  ComboFunctions.FillDriverNoCombo(ddlCompany);
        
                pnlCriteria.Visible = false;

         //   dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
         //   dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
        }


        public frmDriverRentTransactionExpensesReport2(IList LIST, DateTime from, DateTime till)
        {
            InitializeComponent();

           // IsCheck = val;
          //  this.Load += new EventHandler(frmInvoiceReport_Load);


            GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
            col.Width = 40;
            col.AutoSizeMode = BestFitColumnMode.None;
            col.HeaderText = "";
            col.Name = "Check";
            grdLister.Columns.Add(col);

            grdLister.DataSource = LIST;


            this.Shown += new EventHandler(frmDriverRentTransactionExpensesReport_Shown);
          
          btnPrev.Click+=new EventHandler(btnPrev_Click);
            btnNext.Click+=new EventHandler(btnNext_Click);
            btnEmailCurrent.Click+=new EventHandler(btnEmailCurrent_Click);
            btnPrintCurrent.Click+=new EventHandler(btnPrintCurrent_Click);
            btnPrintAll.Click+=new EventHandler(btnPrintAll_Click);
           
            btnEmailAll.Click+=new EventHandler(btnEmailAll_Click);
           grdLister.CellDoubleClick+=new GridViewCellEventHandler(grdLister_CellDoubleClick);

           cbAllDrivers.CheckedChanged+=new EventHandler(cbAllDrivers_CheckedChanged);
           
            
           if(from!=till) 
             lblCriteria.Text = "From : " + string.Format("{0:dd/MM/yyyy}", from) + " to " + string.Format("{0:dd/MM/yyyy}", till);


           

            //ComboFunctions.FillSubCompanyCombo(ddlSubCompany);

            //if (ddlSubCompany.Items.Count > 1)
            //    ddlSubCompany.SelectedIndex = 1;
            //else
            //    ddlSubCompany.SelectedIndex = 0;


        }

       

        void frmDriverRentTransactionExpensesReport_Shown(object sender, EventArgs e)
          {
              try
              {
                  this.FormBorderStyle = FormBorderStyle.FixedSingle;
                  this.ControlBox = true;
                  if (grdLister.Rows.Count > 0)
                  {



                      grdLister.Columns["RentId"].IsVisible = false;
                      grdLister.Columns["DriverId"].IsVisible = false;

                      grdLister.AllowAutoSizeColumns = true;
                      grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


                      grdLister.Columns["Check"].Width = 60;
                      grdLister.Columns["Driver"].Width = 260;


                      grdLister.Rows.ToList().ForEach(c => c.Cells["Check"].Value = true);
                      grdLister.ShowFilteringRow = true;
                      grdLister.EnableFiltering = true;

                      txtPreviewlabel.Text = "Preview " + "1" + " of " + grdLister.Rows.Count;




                      var row = grdLister.Rows.FirstOrDefault();

                      if (row != null)
                      {
                          
                          SetDataSourceAndGenerateReport(row.Cells["RentId"].Value.ToInt());
                      }

                  }
              }
              catch (Exception ex)
              {


              }
          }


          private void SetDataSourceAndGenerateReport(int Id)
          {
              if (Id > 0)
              {
                

                

                  var list = General.GetQueryable<vu_DriverRentExpense>(a => a.Id == Id).OrderBy(c => c.PickupDate).ToList();
                  int count = list.Count;

                  this.DataSource = list;
                  var list2 = General.GetQueryable<vu_FleetDriverRentExpense>(c => c.RentId == Id).OrderBy(c => c.Date).ToList();
                  this.DataSource2 = list2;

                  objSubcompany = General.GetObject<Fleet_Driver>(c => c.Id == list.FirstOrDefault().DefaultIfEmpty().DriverId.ToInt()).Gen_SubCompany.DefaultIfEmpty();

               
                  GenerateReport();
              }

          }


          public bool SendEmailInternally(frmEmail frmE, string subject, string invoiceNo, string email)
          {


              frmE.ReportViewer1 = this.reportViewer1;
              frmE.FileTitle = "Rent for " + invoiceNo.ToStr();
              frmE.EmailSubject = subject;
              frmE.ToEmail = email;
              frmE.txtTo.Text = email;
              frmE.txtSubject.Text = subject;
              frmE.txtAttachment.Text = "Rent for " + invoiceNo;

              frmE.SendEmail(true);

              return frmE.IsEmailSent;
              //         General.ShowEmailForm(reportViewer1, "Account Invoice # " + invoiceNo, email);

          }


       

        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }

        string cellValue = string.Empty;
        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {

            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderColor = _HeaderRowBorderColor;
                e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                e.CellElement.BorderColor4 = _HeaderRowBorderColor;

          
                // e.CellElement.DrawBorder = false;
                e.CellElement.BackColor = _HeaderRowBackColor;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                e.CellElement.ForeColor = Color.White;
                e.CellElement.DrawFill = true;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }

            else if (e.CellElement is GridFilterCellElement)
            {
                e.CellElement.Font = oldFont;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.BackColor = Color.White;
                e.CellElement.RowElement.BackColor = Color.White;
                e.CellElement.RowElement.NumberOfColors = 1;

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
            }

            else if (e.CellElement is GridDataCellElement)
            {



                e.CellElement.ToolTipText = e.CellElement.Text;
                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;


            }




        }



        public void ExportReport(string invoiceNo)
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
            saveFileDlg.FileName = "DriverRentTransaction-" + invoiceNo;
          
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

       

        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Driver Rent Report");
        }

        //private void btnViewReport_Click(object sender, EventArgs e)
        //{

        //    ViewReport();
        //}

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        public void SendEmail(string invoiceNo)
        {

            General.ShowEmailForm(reportViewer1, "Driver Rent Transaction # "+invoiceNo);

        }





        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = 0;


                if (grdLister.CurrentRow == null || (grdLister.CurrentRow is GridViewDataRowInfo) == false)
                    grdLister.CurrentRow = grdLister.Rows[rowIndex];

                if (grdLister.Columns["Driver"].FilterDescriptor != null)
                {

                    grdLister.Columns["Driver"].FilterDescriptor.Value = string.Empty;
                }


                if (grdLister.CurrentRow != null)
                {
                    if (grdLister.CurrentRow is GridViewDataRowInfo && (grdLister.CurrentRow.Index + 1) < grdLister.Rows.Count)
                    {
                        rowIndex = grdLister.CurrentRow.Index + 1;
                        grdLister.CurrentRow = grdLister.Rows[rowIndex];
                    }
                    else
                        rowIndex = grdLister.Rows.Count - 1;
                }


                if (rowIndex >= 0)
                {

                    var row = grdLister.Rows.FirstOrDefault(c => c.Index == rowIndex);

                    if (row != null)
                    {

                        int TransId = row.Cells["RentId"].Value.ToInt();

                        SetDataSourceAndGenerateReport(TransId);
                    }


                    txtPreviewlabel.Text = "Preview " + (rowIndex + 1).ToInt() + " of " + grdLister.Rows.Count;

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



                if (grdLister.CurrentRow.Index==-1 && grdLister.Columns["Driver"].FilterDescriptor != null)
                {

                    grdLister.Columns["Driver"].FilterDescriptor.Value = string.Empty;
                }


                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo && (grdLister.CurrentRow.Index + 1) < grdLister.Rows.Count)
                {
                    rowIndex = grdLister.CurrentRow.Index - 1;

                    if (rowIndex == -1)
                        rowIndex = 0;


                    grdLister.CurrentRow = grdLister.Rows[rowIndex];


                }


                if (rowIndex >= 0)
                {

                    var row = grdLister.Rows.FirstOrDefault(c => c.Index == rowIndex);

                    if (row != null)
                    {

                        int TransId = row.Cells["RentId"].Value.ToInt();

                        SetDataSourceAndGenerateReport(TransId);
                    }


                    txtPreviewlabel.Text = "Preview " + (rowIndex + 1).ToInt() + " of " + grdLister.Rows.Count;



                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);


            }
        }

        private void cbAllDrivers_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllDrivers.Checked == true)
            {
                if (grdLister.Rows.Count > 0)
                {
                    for (int i = 0; i < grdLister.Rows.Count; i++)
                    {
                        grdLister.Rows[i].Cells["Check"].Value = true;//..CurrentCell.Value;
                    }
                }
            }
            else if (cbAllDrivers.Checked == false)
            {
                if (grdLister.Rows.Count > 0)
                {
                    for (int i = 0; i < grdLister.Rows.Count; i++)
                    {
                        grdLister.Rows[i].Cells["Check"].Value = false;//..CurrentCell.Value;

                    }
                }
            }
        }

        Gen_SubCompany objSubcompany = null;

       

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
           // SetSubCompany();
            PrintDocument(0);
        }

        private void btnEmailAll_Click(object sender, EventArgs e)
        {
           // SetSubCompany();
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


                    rows = grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).ToList();
                }
                else
                    rows = grdLister.Rows.Where(c => c.Cells["RentId"].Value.ToLong() == TransId).ToList();



                //List<long> invoiceIds = rows.Select(c => c.Cells["RentId"].Value.ToLong()).ToList<long>();


                List<long> invoiceIds = new List<long>();

                if (TransId == 0)
                {

                    invoiceIds = rows.Select(c => c.Cells["RentId"].Value.ToLong()).ToList<long>();
                }
                else
                {

                    invoiceIds = new List<long>();
                    invoiceIds.Add(TransId);

                }


                if (invoiceIds.Count > 0)
                {

                    frmDriverRentTransactionExpensesReport2 frm = new frmDriverRentTransactionExpensesReport2(1);

                    var list = General.GetQueryable<vu_DriverRentExpense>(a => invoiceIds.Contains(a.Id)).ToList();
                    var list2 = General.GetQueryable<vu_FleetDriverRentExpense>(a => invoiceIds.Contains(a.Id)).ToList();

                    List<Fleet_Driver> driversList = General.GetQueryable<Fleet_Driver>(c => c.DriverTypeId == 2).ToList();

                    frmEmail frmEmail = new frmEmail(null, "", "");

                    Fleet_Driver objDriver = null;
                    foreach (var item in rows.Where(c => c.Cells["Check"].Value.ToBool()))
                    {
                        frm.DataSource = list.Where(c => c.Id == item.Cells["RentId"].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                        frm.DataSource2 = list2.Where(c => c.RentId == item.Cells["RentId"].Value.ToLong()).OrderBy(c => c.Date).ToList();
                    //    frm.objSubcompany = objSubcompany;
                        frm.objSubcompany = driversList.FirstOrDefault(C => C.Id == item.Cells["DriverId"].Value.ToInt()).DefaultIfEmpty().Gen_SubCompany.DefaultIfEmpty();

                        frm.GenerateReport();

                        objDriver = driversList.FirstOrDefault(c => c.Id == item.Cells["DriverId"].Value.ToInt());
                        //string email = driversList.FirstOrDefault(c => c.Id == item.Cells[COLS.Id].Value.ToInt()).DefaultIfEmpty().Email.ToStr().Trim();
                        string email = objDriver.Email.ToStr().Trim();

                        if (!string.IsNullOrEmpty(email))
                        {
                            IsSuccess = frm.SendEmailInternally(frmEmail, subject, objDriver.DriverNo.ToStr().Trim(), email);
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





        private void PrintDocument(long TransId)
        {

            try
            {
                try
                {

                    List<GridViewRowInfo> rows = null;


                    if (TransId == 0)
                    {


                        rows = grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).ToList();
                    }
                    else
                        rows = grdLister.Rows.Where(c => c.Cells["RentId"].Value.ToLong() == TransId).ToList();


                    List<long> invoiceIds = new List<long>();

                    if (TransId == 0)
                    {

                        invoiceIds = rows.Select(c => c.Cells["RentId"].Value.ToLong()).ToList<long>();
                    }
                    else
                    {

                        invoiceIds = new List<long>();
                        invoiceIds.Add(TransId);

                    }


                    if (invoiceIds.Count > 0)
                    {

                     
                       

                        var list = General.GetQueryable<vu_DriverRentExpense>(a => invoiceIds.Contains(a.Id)).ToList();
                        var list2 = General.GetQueryable<vu_FleetDriverRentExpense>(a => invoiceIds.Contains(a.Id)).ToList();


                        List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 1);
                   //     frmEmail frmEmail = new frmEmail(null, "", "");

                        frmDriverTransactionExpensesReport2 frm = new frmDriverTransactionExpensesReport2(1);

                        foreach (var item in rows)
                        {
                          

                            frm.DataSource = list.Where(c => c.Id == item.Cells["RentId"].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                            frm.DataSource2 = list2.Where(c => c.RentId == item.Cells["RentId"].Value.ToLong()).OrderBy(c => c.Date).ToList();
                            frm.ObjSubCompany = driversList.FirstOrDefault(C => C.Id == item.Cells["DriverId"].Value.ToInt()).DefaultIfEmpty().Gen_SubCompany.DefaultIfEmpty();
                            frm.CompanyHeader = frm.ObjSubCompany.CompanyName.ToStr().Trim();
                            
                            frm.GenerateReport();

                            ReportPrintDocument rpt = new ReportPrintDocument(frm.reportViewer1.LocalReport);
                            rpt.Print();
                            rpt.Dispose();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ENUtils.ShowMessage(ex.Message);

                }




            }
            catch (Exception ex)
            {


            }


        }

        private void btnPrintCurrent_Click(object sender, EventArgs e)
        {

            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {

               

                PrintDocument(grdLister.CurrentRow.Cells["RentId"].Value.ToLong());

            }
        }

        private void btnEmailCurrent_Click(object sender, EventArgs e)
        {
            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {
              
                EmailInvoices(grdLister.CurrentRow.Cells["RentId"].Value.ToLong());

            }
        }

        private void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row != null && e.Row is GridViewDataRowInfo)
            {
                int TransId = e.Row.Cells["RentId"].Value.ToInt();

                SetDataSourceAndGenerateReport(TransId);


                var row = grdLister.Rows.FirstOrDefault(c => c.Cells["RentId"].Value.ToLong() == TransId);

                if (row != null)
                {

                    txtPreviewlabel.Text = "Preview " + (row.Index + 1).ToInt() + " of " + grdLister.Rows.Count;
                }
            }
        }

     
     
    }
}
