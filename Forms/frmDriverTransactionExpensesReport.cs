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

namespace Taxi_AppMain
{
    public partial class frmDriverTransactionExpensesReport : UI.SetupBase
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


        Gen_SubCompany _ObjSubCompany;

        public Gen_SubCompany ObjSubCompany
        {
            get { return _ObjSubCompany; }
            set { _ObjSubCompany = value; }
        }

        public void GenerateReport()
        {
            try
            {
                if (ddlCompany.SelectedValue == null)
                    pnlCriteria.Visible = false;

                reportViewer1.LocalReport.EnableExternalImages = true;


                if (ObjSubCompany == null)
                    ObjSubCompany = AppVars.objSubCompany;



                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[27];

                string address = ObjSubCompany.Address;
                string telNo = string.Empty;



                string sortCode = ObjSubCompany.SortCode.ToStr();
                string accountNo = ObjSubCompany.AccountNo.ToStr();
                string accountTitle = ObjSubCompany.AccountTitle.ToStr();
                string bank = ObjSubCompany.BankName.ToStr();

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



                string website = ObjSubCompany.WebsiteUrl.ToStr();
                if (!string.IsNullOrEmpty(website))
                {
                    website += " , ";
                }

                website += "Email:" + ObjSubCompany.EmailAddress.ToStr();


                string companyNumber = ObjSubCompany.CompanyNumber.ToStr();
                if (!string.IsNullOrEmpty(companyNumber))
                {
                    companyNumber = "Company Number: " + companyNumber;
                }

                string vatNumber = ObjSubCompany.CompanyVatNumber.ToStr();
                //if (!string.IsNullOrEmpty(vatNumber))
                //{
                //    vatNumber = "VAT Number: " + vatNumber;
                //}



                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);

                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", ObjSubCompany.WebsiteUrl.ToStr());

                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MobileNo", "Mobile: " + ObjSubCompany.EmergencyNo.ToStr());
                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website",website);
                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", "Email: " + ObjSubCompany.EmailAddress.ToStr());

                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyNumber",companyNumber);
                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATNumber", vatNumber);


                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle", accountTitle);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = ObjSubCompany.CompanyLogo != null ? ObjSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

                string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", ObjSubCompany.CompanyName.ToStr());

                param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeading", ObjSubCompany.CompanyName.ToStr());


              


               
                int? driverId = this.DataSource.FirstOrDefault().DefaultIfEmpty().DriverId;

                var data = this.DataSource.FirstOrDefault().DefaultIfEmpty();








                telNo = "Tel No. " + ObjSubCompany.TelephoneNo;

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
                   
                           
                param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone",  telNo);


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

                 string balance=string.Empty;
                
                decimal bal=this.DataSource.FirstOrDefault().DefaultIfEmpty().Balance.ToDecimal();
                

                if(bal>=0)
                {

                    balance = "You are due to Pay £" + string.Format("{0:f2}", bal);
                }
                else
                {
                    balance = "You are due to receive £" + string.Format("{0:f2}", bal);

                    balance = balance.Replace("-", "").Trim();

                }


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


        private int _SubCompanyId;

        public int SubCompanyId
        {
            get { return _SubCompanyId; }
            set { _SubCompanyId = value; }
        }


        public frmDriverTransactionExpensesReport(int val)
        {
            InitializeComponent();

            IsCheck = val;
            this.Load +=new EventHandler(frmInvoiceReport_Load);

            ComboFunctions.FillDriverNoCombo(ddlCompany);


            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
        }


        public void SendEmailInternally(frmEmail frmE, string subject, string invoiceNo, string email)
        {


            frmE.ReportViewer1 = this.reportViewer1;
            frmE.FileTitle = "Rent for " + invoiceNo.ToStr();
            frmE.EmailSubject = subject;
            frmE.ToEmail = email;
            frmE.txtTo.Text = email;
            frmE.txtSubject.Text = subject;
            frmE.txtAttachment.Text = "Rent for " + invoiceNo;

            frmE.SendEmail(true);

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
            int DriverId = ddlCompany.SelectedValue.ToInt();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

            string error = string.Empty;
            if (DriverId == 0)
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

            lblCriteria.Text = "Account Invoice Report Related to '" + ddlCompany.Text.ToStr() + "', Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);




            var list = General.GetQueryable<vu_DriverRentExpense>(a => a.DriverId == DriverId && a.TransDate >= fromDate && a.TransDate <= tillDate).ToList();
            int count = list.Count;

            this.DataSource = list;


            GenerateReport();




        }

        public void SendEmail()
        {
            General.ShowEmailForm(reportViewer1, "Driver Rent Report");
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

            General.ShowEmailForm(reportViewer1, "Driver Rent Transaction # "+invoiceNo);

        }
     
     
    }
}
