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
    public partial class frmDriverCommisionTransactionExpensesReport2 : UI.SetupBase
    {


        private bool _IsFareAndWaitingWise;

        public bool IsFareAndWaitingWise
        {
            get { return _IsFareAndWaitingWise; }
            set { _IsFareAndWaitingWise = value; }
        }



        //private List<vu_DriverCommision> _DataSource;

        //public List<vu_DriverCommision> DataSource
        private List<vu_DriverCommisionExpenses2> _DataSource;

        public List<vu_DriverCommisionExpenses2> DataSource
        {
            get { return _DataSource; }
            set { _DataSource = value; }
        }
        private List<vu_FleetDriverCommissionExpense> _DataSource2;

        public List<vu_FleetDriverCommissionExpense> DataSource2
        {
            get { return _DataSource2; }
            set { _DataSource2 = value; }
        }
        private string _CompanyHeader;

        public string CompanyHeader
        {
            get { return _CompanyHeader; }
            set { _CompanyHeader = value; }
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

            
                   //UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);

                   //if (objTemplate == null)
                   //{
                   //    ENUtils.ShowMessage("Report Template is not defined in Settings");
                   //    return;
                   //}
                   

                   //string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";

                   //if (objTemplate.TemplateName.ToStr() == "Template1")
                   //{
                   //    this.reportViewer1.LocalReport.ReportEmbeddedResource = "Taxi_AppMain.ReportDesigns." + "rptDriverCommisionExpenses.rdlc";
                   //}
                   //else if (objTemplate.TemplateName.ToStr() == "Template2" || objTemplate.TemplateName.ToStr() == "Template3")
                   //{
                   //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverCommisionExpenses.rdlc";
                   //    //rptDriverCommisionExpenses
                   //}

                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[31];


                string address = AppVars.objSubCompany.Address;
                string telNo = string.Empty;

                     
                string sortCode = AppVars.objSubCompany.SortCode.ToStr();
                string accountNo = AppVars.objSubCompany.AccountNo.ToStr();
                string accountTitle = AppVars.objSubCompany.AccountTitle.ToStr();
                string bank = AppVars.objSubCompany.BankName.ToStr();


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



                string website =AppVars.objSubCompany.WebsiteUrl.ToStr();
                if (!string.IsNullOrEmpty(website))
                {
                    website += " , ";
                }

                website += "Email:" + AppVars.objSubCompany.EmailAddress.ToStr();


                string companyNumber = AppVars.objSubCompany.CompanyNumber.ToStr();
                if (!string.IsNullOrEmpty(companyNumber))
                {
                    companyNumber = "Company Number: " + companyNumber;
                }

                string vatNumber =AppVars.objSubCompany.CompanyVatNumber.ToStr();
                if (!string.IsNullOrEmpty(vatNumber))
                {
                    vatNumber = "VAT Number: " + vatNumber;
                }


                param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);

                param[18] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Footer", AppVars.objSubCompany.WebsiteUrl.ToStr());

                param[14] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_MobileNo", "Mobile: " + AppVars.objSubCompany.EmergencyNo.ToStr());
                param[15] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Website", website);
                param[16] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", "Email: " + AppVars.objSubCompany.EmailAddress.ToStr());

                param[20] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyNumber", companyNumber);
                param[21] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VATNumber", vatNumber);


                param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
                param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle", accountTitle);
                param[10] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);



                List<ClsLogo> objLogo = new List<ClsLogo>();
                objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
                ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
                this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

              //  string path = @"File:";



                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", this.DataSource.FirstOrDefault().DefaultIfEmpty().DriverNo);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());







                int? driverId = this.DataSource.FirstOrDefault().DefaultIfEmpty().DriverId;

                var data = this.DataSource.FirstOrDefault().DefaultIfEmpty();


                telNo = "Telephone: " + AppVars.objSubCompany.TelephoneNo + ", Fax: " + AppVars.objSubCompany.Fax + ", E-mail: " + AppVars.objSubCompany.EmailAddress + ", Website:"+AppVars.objSubCompany.WebsiteUrl;

                if (!string.IsNullOrEmpty(accountNo))
                    accountNo = "Account No : " + accountNo;


               // string className = "Taxi_AppMain.ReportDesigns.";
                //if (IsCheck == 1)
                //{

                //    if (IsFareAndWaitingWise)
                //    {
                //        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverCommisionTrasaction3.rdlc";

                //    }
                //    else
                //    {

                        

                //        this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverCommisionTrasaction4.rdlc";
                //    }
                //}
                //else
                //{
                //    this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptDriverCommisionTrasaction2.rdlc";
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


                string AccountBooking = string.Empty; 
                  string CashBooking = string.Empty;


       

                  Fleet_Driver obj = General.GetObject<Fleet_Driver>(c => c.Id == driverId);
                  decimal DriverCommision = obj.DriverCommissionPerBooking.ToDecimal();

                

                  decimal JobTotal = 0; 
                if(this.IsFareAndWaitingWise)
                {
                    AccountBooking = string.Format("{0:£ #.##}", this.DataSource.Where(c => c.CompanyId != null && c.BookingTypeId.ToInt() == Enums.ACCOUNT_TYPE.ACCOUNT).Sum(c => c.FareRate.ToDecimal() + c.WaitingCharges.ToDecimal()));
                    CashBooking = string.Format("{0:£ #.##}", this.DataSource.Where(c => c.CompanyId == null || c.BookingTypeId.ToInt()==Enums.ACCOUNT_TYPE.CASH).Sum(c => c.FareRate.ToDecimal() + c.WaitingCharges.ToDecimal()));
                    JobTotal = this.DataSource.Sum(c => c.FareRate.Value.ToDecimal()+ c.WaitingCharges.ToDecimal());

                }
                else
                {
                    AccountBooking = string.Format("{0:£ #.##}", this.DataSource.Where(c => c.CompanyId != null && c.BookingTypeId.ToInt()==Enums.ACCOUNT_TYPE.ACCOUNT).Sum(c => c.FareRate.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal()));
                    CashBooking = string.Format("{0:£ #.##}", this.DataSource.Where(c => c.CompanyId == null || c.BookingTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH).Sum(c => c.FareRate.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal()));
                     JobTotal=   this.DataSource.Sum(c => c.FareRate.ToDecimal()+c.ParkingCharges.ToDecimal()+c.WaitingCharges.ToDecimal());
                }


                param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountJobTotal", AccountBooking);
                param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CashJobTotal", CashBooking);
                string BalanceType = string.Empty;
                int DriverId = this.DataSource.FirstOrDefault().DriverId.ToInt();
                int Id = this.DataSource.FirstOrDefault().Id.ToInt(); ;
                var query=General.GetObject<Fleet_DriverCommision>(c=>c.DriverId==DriverId && c.Id<Id);
               
                string StatementDate = string.Empty;
                if (query == null)
                {
                    BalanceType = "Initial Balance";
                    StatementDate = string.Format("{0:dd/MM}", this.DataSource.FirstOrDefault().TransDate);
                }
                else
                {
                    BalanceType = "Balance from statement "+query.TransNo;
                    StatementDate = string.Format("{0:dd/MM}", query.TransDate);
                }
               
                string Commision = (JobTotal * DriverCommision / 100).ToStr();
                decimal AccountTotal = (this.DataSource.Sum(c => c.AccountJobsTotal)).ToDecimal();
             //   decimal AccountCommision=(25*AccountTotal/100);
                decimal CashTotal = (this.DataSource.Sum(c => c.CashJobsTotal)).ToDecimal();
              //  decimal CashCommision=(25*CashTotal/100);
               // decimal TotalDebit = this.DataSource2.Sum(c => c.Debit).ToDecimal();
              //  decimal TotalCredit = this.DataSource2.Sum(c=>c.Credit).ToDecimal();



              //  decimal commissionTotal = this.data


             //   var objRecord=  General.GetObject<Fleet_DriverCommision>(c => c.Id == Id);


                //if (objRecord != null)
                //{

                //    commissionTotal = objRecord.CommissionTotal.ToDecimal() + objRecord.AgentFeesTotal.ToDecimal();
                //}

                if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool())
                {

                    List<Fleet_Driver_CommissionRange> listofRange = obj.Fleet_Driver_CommissionRanges.ToList();

                    if (listofRange.Count == 0)
                    {
                        listofRange = GetSystemCommissionRange();

                    }

                    Commision =Math.Round(this.DataSource
                                  .Sum(c => c.IsCommissionWise.ToBool() ? (c.DriverCommissionType == "Percent" ? ((c.TotalCharges * c.DriverCommissionOnBooking) / 100) : c.DriverCommissionOnBooking) : (((c.FareRate * listofRange.FirstOrDefault(a => c.TotalCharges >= a.FromPrice && c.FareRate <= a.ToPrice).DefaultIfEmpty().CommissionValue.ToDecimal()) / 100))).ToDecimal(),2).ToStr();


                }


                param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Commision", Commision);

                string DriverGrandTotal = "";

                if (this.DataSource != null)
                {
                    DriverGrandTotal = (this.DataSource[0].DriverCommision + this.DataSource[0].Extra + this.DataSource[0].fuel + this.DataSource[0].OldBalance).ToStr();
                }

                param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_GrandTotal", DriverGrandTotal);

                int cnt = this.DataSource.Count;
                decimal AccountExpenses = 0.00m;
            //    decimal DropOfCharges=this.DataSource.Sum(c=>c.ExtraDropOfCharges).ToDecimal();
             //   decimal PickUpCharges=this.DataSource.Sum(c=>c.ExtraPickUpCharges).ToDecimal();
              //  AccountExpenses=(DropOfCharges+PickUpCharges);
                param[26] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_jobCount", cnt.ToStr());
                param[27] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_BalanceType", BalanceType);
                param[28] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountExpenses", AccountExpenses.ToStr());
                param[29] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_StatementDate", StatementDate);
                //Report_Parameter_StatementDate
              
                string balance=string.Empty;
                
                decimal bal=this.DataSource.FirstOrDefault().DefaultIfEmpty().Balance.ToDecimal();
                

                if(bal>=0)
                {

                    balance = "You are due to receive £" + string.Format("{0:f2}", bal);
                }
                else
                {
                    balance = "You are due to Pay £" + string.Format("{0:f2}", bal);

                    balance = balance.Replace("-", "").Trim();

                }


                param[30] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Balance", balance);


             //   string strCommissionTotal = string.Format("{0:f2}", commissionTotal);
            //    param[31] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CommissionTotal", strCommissionTotal);

                
                
                //int minRows = 12;
                //if (cnt < minRows)
                //{
                //    for (int i = 0; i < minRows - cnt; i++)
                //    {
                //        this.DataSource.Add(new vu_DriverCommisionExpenses2 { Id = data.Id, BookingId = data.BookingId, });//, Passenger = data.Passenger, FromAddress = data.FromAddress, ToAddress = data.ToAddress });

                //    }

                //}
                reportViewer1.LocalReport.SetParameters(param);
                this.vu_DriverCommisionExpenses2BindingSource.DataSource = this.DataSource;
                this.vu_FleetDriverCommissionExpenseBindingSource.DataSource = this.DataSource2;
              
               
                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        public bool SendEmailInternally(frmEmail frmE, string subject, string invoiceNo, string email)
        {


            frmE.ReportViewer1 = this.reportViewer1;
            frmE.FileTitle = "Commission for " + invoiceNo.ToStr();
            frmE.EmailSubject = subject;
            frmE.ToEmail = email;
            frmE.txtTo.Text = email;
            frmE.txtSubject.Text = subject;
            frmE.txtAttachment.Text = "Commission for " + invoiceNo;

            frmE.SendEmail(true);

            return frmE.IsEmailSent;
            //         General.ShowEmailForm(reportViewer1, "Account Invoice # " + invoiceNo, email);

        }


        private List<Fleet_Driver_CommissionRange> GetSystemCommissionRange()
        {

            return (from a in General.GetQueryable<Gen_SysPolicy_CommissionPriceRange>(null).ToList()
                    select new Fleet_Driver_CommissionRange
                    {
                        DriverId = 0,
                        FromPrice = a.FromPrice,
                        ToPrice = a.ToPrice,
                        CommissionValue = a.CommissionValue


                    }).ToList();


        }

        int IsCheck = 0;
        public frmDriverCommisionTransactionExpensesReport2(int val)
        {
            InitializeComponent();

            IsCheck = val;
            this.Load += new EventHandler(frmInvoiceReport_Load);

            ComboFunctions.FillDriverNoCombo(ddlCompany);

            if (ddlCompany.SelectedValue == null)
                pnlCriteria.Visible = false;

            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
        }


        public frmDriverCommisionTransactionExpensesReport2(IList LIST,DateTime from, DateTime till)
        {
            InitializeComponent();

           // IsCheck = val;
            this.Load += new EventHandler(frmInvoiceReport_Load);


            GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
            col.Width = 40;
            col.AutoSizeMode = BestFitColumnMode.None;
            col.HeaderText = "";
            col.Name = "Check";
            grdDriverCommission.Columns.Add(col);

            grdDriverCommission.DataSource = LIST;


            this.Shown += new EventHandler(frmDriverCommisionTransactionExpensesReport2_Shown);


         

            lblCriteria.Text = "From : " + string.Format("{0:dd/MM/yyyy}", from) + " to " + string.Format("{0:dd/MM/yyyy}", till);

        }

        void frmDriverCommisionTransactionExpensesReport2_Shown(object sender, EventArgs e)
        {

            try
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.ControlBox = true;
                if (grdDriverCommission.Rows.Count > 0)
                {



                    grdDriverCommission.Columns["CommissionId"].IsVisible = false;
                    grdDriverCommission.Columns["DriverId"].IsVisible = false;

                    grdDriverCommission.AllowAutoSizeColumns = true;
                    grdDriverCommission.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


                    grdDriverCommission.Columns["Check"].Width = 60;
                    grdDriverCommission.Columns["Driver"].Width = 260;


                    grdDriverCommission.Rows.ToList().ForEach(c => c.Cells["Check"].Value = true);
                    grdDriverCommission.ShowFilteringRow = true;
                    grdDriverCommission.EnableFiltering = true;

                    txtPreviewlabel.Text = "Preview " + "1" + " of " + grdDriverCommission.Rows.Count;




                    var row = grdDriverCommission.Rows.FirstOrDefault();

                    if (row != null)
                    {
                        SetDataSourceAndGenerateReport(row.Cells["CommissionId"].Value.ToInt());
                    }

                }
            }
            catch (Exception ex)
            {


            }



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
            saveFileDlg.FileName = "DriverCommisionTransaction-" + invoiceNo;

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

            var list = General.GetQueryable<vu_DriverCommisionExpenses2>(a => a.DriverId == DriverId && a.TransDate >= fromDate && a.TransDate <= tillDate).ToList();
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


        public void SendEmail(string invoiceNo,string email)
        {

            General.ShowEmailForm(reportViewer1, "Driver Commission Transaction # " + invoiceNo, email);

        }

        private void grdDriverCommission_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row != null && e.Row is GridViewDataRowInfo)
            {
                 int TransId=  e.Row.Cells["CommissionId"].Value.ToInt();

                 SetDataSourceAndGenerateReport(TransId);


                 var row= grdDriverCommission.Rows.FirstOrDefault(c => c.Cells["CommissionId"].Value.ToLong() == TransId);

                 if (row != null)
                 {

                     txtPreviewlabel.Text = "Preview " + (row.Index + 1).ToInt() + " of " + grdDriverCommission.Rows.Count;
                 }
            }
        }


        private void SetDataSourceAndGenerateReport(int Id)
        {
            if (Id > 0)
            {

                var list = General.GetQueryable<vu_DriverCommisionExpenses2>(a => a.Id == Id).OrderBy(c => c.PickupDate).ToList();
                int count = list.Count;

                this.DataSource = list;
                var list2 = General.GetQueryable<vu_FleetDriverCommissionExpense>(c => c.CommissionId == Id).OrderBy(c => c.Date).ToList();
                this.DataSource2 = list2;

                //   frm.IsFareAndWaitingWise = this.IsFareAndWaitingWiseComm;

                GenerateReport();
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = 0;


                if (grdDriverCommission.CurrentRow == null || (grdDriverCommission.CurrentRow is GridViewDataRowInfo) == false)
                    grdDriverCommission.CurrentRow = grdDriverCommission.Rows[rowIndex];


                if (grdDriverCommission.CurrentRow != null )
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

                        int TransId = row.Cells["CommissionId"].Value.ToInt();

                        SetDataSourceAndGenerateReport(TransId);
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

              


                if (grdDriverCommission.CurrentRow != null && grdDriverCommission.CurrentRow is GridViewDataRowInfo && (grdDriverCommission.CurrentRow.Index + 1) < grdDriverCommission.Rows.Count)
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

                        int TransId = row.Cells["CommissionId"].Value.ToInt();

                        SetDataSourceAndGenerateReport(TransId);
                    }


                    txtPreviewlabel.Text = "Preview " + (rowIndex + 1).ToInt() + " of " + grdDriverCommission.Rows.Count;



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

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            PrintDocument(0);
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


                    rows = grdDriverCommission.Rows.Where(c => c.Cells["Check"].Value.ToBool()==true ).ToList();
                }
                else
                    rows = grdDriverCommission.Rows.Where(c => c.Cells["CommissionId"].Value.ToLong() == TransId).ToList();



                //List<long> invoiceIds = rows.Select(c => c.Cells["CommissionId"].Value.ToLong()).ToList<long>();


                List<long> invoiceIds = new List<long>();

                if (TransId == 0)
                {

                    invoiceIds = rows.Select(c => c.Cells["CommissionId"].Value.ToLong()).ToList<long>();
                }
                else
                {

                    invoiceIds = new List<long>();
                    invoiceIds.Add(TransId);

                }


                if (invoiceIds.Count > 0)
                {

                    frmDriverCommisionTransactionExpensesReport2 frm = new frmDriverCommisionTransactionExpensesReport2(1);

                    var list = General.GetQueryable<vu_DriverCommisionExpenses2>(a => invoiceIds.Contains(a.Id)).ToList();
                    var list2 = General.GetQueryable<vu_FleetDriverCommissionExpense>(a => invoiceIds.Contains(a.Id)).ToList();

                    List<Fleet_Driver> driversList = General.GetQueryable<Fleet_Driver>(c => c.DriverTypeId == 2).ToList();

                    frmEmail frmEmail = new frmEmail(null, "", "");

                    Fleet_Driver objDriver = null;
                    foreach (var item in rows.Where(c => c.Cells["Check"].Value.ToBool()))
                    {
                        frm.DataSource = list.Where(c => c.Id == item.Cells["CommissionId"].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                        frm.DataSource2 = list2.Where(c => c.CommissionId == item.Cells["CommissionId"].Value.ToLong()).OrderBy(c => c.Date).ToList();

                        frm.GenerateReport();

                        objDriver = driversList.FirstOrDefault(c => c.Id == item.Cells["DriverId"].Value.ToInt());
                        //string email = driversList.FirstOrDefault(c => c.Id == item.Cells[COLS.Id].Value.ToInt()).DefaultIfEmpty().Email.ToStr().Trim();
                        string email = objDriver.Email.ToStr().Trim();

                        if (!string.IsNullOrEmpty(email))
                        {
                             IsSuccess= frm.SendEmailInternally(frmEmail, subject, objDriver.DriverNo.ToStr().Trim(), email);
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


                        rows = grdDriverCommission.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).ToList();
                    }
                    else
                        rows= grdDriverCommission.Rows.Where(c => c.Cells["CommissionId"].Value.ToLong() ==TransId).ToList();


                    List<long> invoiceIds = new List<long>();

                    if (TransId == 0)
                    {

                        invoiceIds = rows.Select(c => c.Cells["CommissionId"].Value.ToLong()).ToList<long>();
                    }
                    else
                    {

                        invoiceIds = new List<long>();
                        invoiceIds.Add(TransId);

                    }


                    if (invoiceIds.Count > 0)
                    {

                        frmDriverCommisionTransactionExpensesReport2 frm = new frmDriverCommisionTransactionExpensesReport2(1);
                        frm.CompanyHeader = AppVars.objSubCompany.CompanyName.ToStr().Trim();

                        var list = General.GetQueryable<vu_DriverCommisionExpenses2>(a => invoiceIds.Contains(a.Id)).ToList();
                        var list2 = General.GetQueryable<vu_FleetDriverCommissionExpense>(a => invoiceIds.Contains(a.Id)).ToList();


                        List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 2);
                        frmEmail frmEmail = new frmEmail(null, "", "");


                        foreach (var item in rows)
                        {
                            frm.DataSource = list.Where(c => c.Id == item.Cells["CommissionId"].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                            frm.DataSource2 = list2.Where(c => c.CommissionId == item.Cells["CommissionId"].Value.ToLong()).OrderBy(c => c.Date).ToList();

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

            if(grdDriverCommission.CurrentRow!=null && grdDriverCommission.CurrentRow is GridViewDataRowInfo)
            {
                PrintDocument(grdDriverCommission.CurrentRow.Cells["CommissionId"].Value.ToLong());

            }
        }

        private void btnEmailCurrent_Click(object sender, EventArgs e)
        {
            if (grdDriverCommission.CurrentRow != null && grdDriverCommission.CurrentRow is GridViewDataRowInfo)
            {
                EmailInvoices(grdDriverCommission.CurrentRow.Cells["CommissionId"].Value.ToLong());

            }
        }



    }
}
