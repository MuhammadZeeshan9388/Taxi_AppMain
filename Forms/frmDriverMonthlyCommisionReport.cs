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
using Microsoft.Reporting.WinForms;
using Taxi_AppMain.Classes;

namespace Taxi_AppMain
{
    public partial class frmDriverMonthlyCommisionReport : UI.SetupBase
    {



        private List<vu_DriverCommision> _DataSource;

        public List<vu_DriverCommision> DataSource
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

          


                Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[26];

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



                string website = AppVars.objSubCompany.WebsiteUrl.ToStr();
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

                string vatNumber = AppVars.objSubCompany.CompanyVatNumber.ToStr();
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

                string path = @"File:";
                param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);
                param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());







                int? driverId = this.DataSource.FirstOrDefault().DefaultIfEmpty().DriverId;

                var data = this.DataSource.FirstOrDefault().DefaultIfEmpty();


                telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;

                if (!string.IsNullOrEmpty(accountNo))
                    accountNo = "Account No : " + accountNo;

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

                string AccountBooking = string.Format("{0:£ #.##}", this.DataSource.Where(c => c.CompanyId != null).Sum(c => c.FareRate.Value.ToDecimal()));

                string CashBooking = string.Format("{0:£ #.##}", this.DataSource.Where(c => c.CompanyId == null).Sum(c => c.FareRate.Value.ToDecimal()));

                param[22] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountJobTotal", AccountBooking);
                param[23] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CashJobTotal", CashBooking);


                Fleet_Driver obj = General.GetObject<Fleet_Driver>(c => c.Id == driverId);
                decimal DriverCommision = obj.DriverCommissionPerBooking.ToDecimal();

                decimal JobTotal = this.DataSource.Sum(c => c.FareRate.Value.ToDecimal());
                string Commision = (JobTotal * DriverCommision / 100).ToStr();


                param[24] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Commision", Commision);

                string DriverGrandTotal = "";
                if (this.DataSource != null)
                {
                    DriverGrandTotal = (this.DataSource[0].DriverCommision + this.DataSource[0].Extra + this.DataSource[0].fuel + this.DataSource[0].OldBalance).ToStr();
                }

                param[25] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_GrandTotal", DriverGrandTotal);

                reportViewer1.LocalReport.SetParameters(param);


                int cnt = this.DataSource.Count;



                this.vu_DriverCommisionBindingSource.DataSource = this.DataSource;

                this.reportViewer1.ZoomPercent = 100;
                this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        public frmDriverMonthlyCommisionReport()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmInvoiceReport_Load);

            ComboFunctions.FillDriverNoCombo(ddlDriver, c => c.DriverTypeId == 2);
            rdoMonyhly.IsChecked = true;
            int Month = DateTime.Now.Month.ToInt();
            string Year = DateTime.Now.Year.ToStr();

            ddlWeekMonths.SelectedIndex = Month.ToInt();
            ddlMonth.SelectedIndex = Month.ToInt();
            ddlYear.SelectedText = Year.ToStr();
            ddlYear.Select();
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
                //e.CellElement
                e.CellElement.BorderColor = _HeaderRowBorderColor;
                e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                //e.CellElement.DrawBorder = false;
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
            saveFileDlg.FileName = "DriverMonthlyCommisionReport-" + invoiceNo;


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
            int DriverId = ddlDriver.SelectedValue.ToInt();
            int Month = ddlMonth.SelectedIndex.ToInt();
            string Year = ddlYear.SelectedText.ToStr();


            string error = string.Empty;
            if (DriverId == 0)
            {
                error += "Required : Driver";
            }
            //if (Month == 0)
            //{
            //    error += "Required : Month";
            //}
            //if (Year == 0)
            //{
            //    error += "Required : Year";
            //}

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;

            }

            if (rdoMonyhly.IsChecked == true)
            {
                var query = General.GetQueryable<vu_DriverCommision>(a => a.DriverId == DriverId && a.TransDate.Value.Month == Month && a.TransDate.Value.Year == (Year).ToInt()).OrderByDescending(c => c.TransDate).FirstOrDefault();
                if (query != null)
                {
                    DateTime? TransDate = query.TransDate.ToDateTime();



                    var list = General.GetQueryable<vu_DriverCommision>(a => a.DriverId == DriverId && a.TransDate.Value.Month == Month && a.TransDate.Value.Year == (Year).ToInt()).Where(c => c.TransDate == TransDate).ToList();


                    this.DataSource = list;
                    GenerateReport();
                }
                else
                {
                    ENUtils.ShowMessage("No Data Found..");
                }
            }
            else
            {
                string Dates = ddlDates.SelectedText.ToStr();

                DateTime FromDate = Dates.Substring(0, 11).ToDate();
                DateTime ToDate = Dates.Substring(15, 11).ToDate();

                var list = General.GetQueryable<vu_DriverCommision>(a => a.DriverId == DriverId && a.FromDate == FromDate && a.ToDate == ToDate).ToList();


                this.DataSource = list;
                GenerateReport();
            }

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

            General.ShowEmailForm(reportViewer1, "Driver Monthly Commision # " + invoiceNo);

        }

        private void rdoMonyhly_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            RadioSate(args.ToggleState);
        }
        private void RadioSate(ToggleState toggle)
        {
            int Month = DateTime.Now.Month.ToInt();
            if (toggle == ToggleState.On)
            {
                pnlMonth.Visible = true;
                pnlDate.Visible = false;
                ddlMonth.SelectedIndex = Month.ToInt();
            }
            else
            {
                pnlMonth.Visible = false;
                pnlDate.Visible = true;


                ddlWeekMonths.SelectedIndex = Month.ToInt();
                ddlWeekMonths.SelectedIndex = Month.ToInt();
            }
        }

        private void ddlWeekMonths_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();
                int Month = ddlWeekMonths.SelectedIndex.ToInt();

                ComboFunctions.FillCommisionDriverDates(ddlDates, c => c.DriverId == DriverId && c.FromDate.Value.Month == Month);


            }
            catch (Exception ex)
            {
            }
        }
        private void ddlDriver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();

                int Month = ddlWeekMonths.SelectedIndex.ToInt();

                ComboFunctions.FillCommisionDriverDates(ddlDates, c => c.DriverId == DriverId && c.FromDate.Value.Month == Month);
            }
            catch (Exception ex)
            {

            }
        }


    }
}
