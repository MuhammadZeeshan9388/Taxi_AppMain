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
    public partial class rptfrmPreCustomerInvoice : UI.SetupBase
    {
       


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


        public rptfrmPreCustomerInvoice()
        {
            InitializeComponent();

            ddlCustomer.SelectedIndexChanged += new EventHandler(ddlCustomer_SelectedIndexChanged);

            ComboFunctions.FillMultiColumnCustomerCombo(ddlCustomer);
            ddlCustomer.MultiColumnComboBoxElement.DropDownWidth = 500;
            ddlCustomer.EditorControl.AutoSizeRows = false;
            ddlCustomer.EditorControl.BestFitColumns();
            ddlCustomer.EditorControl.ColumnWidthChanged += new ColumnWidthChangedEventHandler(EditorControl_ColumnWidthChanged);



            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());


        }


        public void GenerateReport()
        {
            if (ddlCustomer.SelectedValue == null)
                pnlCriteria.Visible = false;

            reportViewer1.LocalReport.EnableExternalImages = true;

            Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[10];

            string address = AppVars.objSubCompany.Address;
            string telNo = "Tel No. " + AppVars.objSubCompany.TelephoneNo;


            string sortCode = AppVars.objSubCompany.SortCode.ToStr();
            string accountNo = AppVars.objSubCompany.AccountNo.ToStr();
            string accountTitle = AppVars.objSubCompany.AccountTitle.ToStr();
            string bank = AppVars.objSubCompany.BankName.ToStr();

            if (!string.IsNullOrEmpty(sortCode))
                sortCode = "Sort Code : " + sortCode;

            if (!string.IsNullOrEmpty(accountNo))
                accountNo = "Account No : " + accountNo;

            if (!string.IsNullOrEmpty(accountTitle))
                accountTitle = "Account Title : " + accountTitle;

            if (!string.IsNullOrEmpty(bank))
                bank = "Bank : " + bank;

            UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == this.Name && c.IsDefault == true);


            string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";

            if (objTemplate.TemplateName.ToStr() == "Template1")
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCustomerInvoice.rdlc";

            }
            else if (objTemplate.TemplateName.ToStr() == "Template2")
            {
                this.reportViewer1.LocalReport.ReportEmbeddedResource = className + "rptCustomerInvoice.rdlc";

            }

            param[0] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Address", address);
            param[1] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Telephone", telNo);



            List<ClsLogo> objLogo = new List<ClsLogo>();
            objLogo.Add(new ClsLogo { ImageInBytes = AppVars.objSubCompany.CompanyLogo != null ? AppVars.objSubCompany.CompanyLogo.ToArray() : null });
            ReportDataSource imageDataSource = new ReportDataSource("Taxi_AppMain_Classes_ClsLogo", objLogo);
            this.reportViewer1.LocalReport.DataSources.Add(imageDataSource);

            string path = @"File:";
            param[2] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Path", path);


            //   int? companyId = this.DataSource.FirstOrDefault().DefaultIfEmpty().CompanyId;





            decimal invoiceGrandTotal = this.DataSource.FirstOrDefault().DefaultIfEmpty().InvoiceTotal.ToDecimal() + this.DataSource.FirstOrDefault().DefaultIfEmpty().AdminFees.ToDecimal();
            string grandTotal = string.Format("{0:c}", invoiceGrandTotal);
            grandTotal = grandTotal.Substring(1);
            param[3] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_InvoiceTotal", grandTotal);


            param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_CompanyHeader", AppVars.objSubCompany.CompanyName.ToStr());

            param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_SortCode", sortCode);
            param[6] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountNo", accountNo);
            param[7] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_AccountTitle", accountTitle);
            param[8] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Bank", bank);
            param[9] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_Email", "Email: " + AppVars.objSubCompany.EmailAddress.ToStr());

            //string vat = "0";
            //decimal valueAddedTax = 0.0m;
            //if (companyId != null)
            //{
            //    Gen_Company objCompany = General.GetObject<Gen_Company>(c => c.Id == companyId);

            //    if (objCompany != null)
            //    {
            //        if (objCompany.HasVat.ToBool())
            //        {
            //            valueAddedTax = (invoiceGrandTotal * 20) / 100;
            //            vat = "1";

            //        }

            //    }

            //}

            // invoiceGrandTotal = invoiceGrandTotal + valueAddedTax;


            //      param[4] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_HasVat", vat);

            //           param[5] = new Microsoft.Reporting.WinForms.ReportParameter("Report_Parameter_VAT", valueAddedTax.ToStr());


            reportViewer1.LocalReport.SetParameters(param);



            this.vuInvoiceBindingSource.DataSource = this.DataSource;

            this.reportViewer1.ZoomPercent = 100;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
            this.reportViewer1.RefreshReport();

        }
     


        void EditorControl_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            try
            {
                MasterGridViewTemplate template = (MasterGridViewTemplate)sender;
                template.Columns["Id"].IsVisible = false;
                template.Columns["Name"].Width = 80;
            }
            catch (Exception ex)
            {

            }
        }

        void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsFormLoaded == false)
            {
                ddlCustomer.SelectedIndex = -1;
                ddlCustomer.SelectedValue = null;
                ddlCustomer.Text = string.Empty;

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
            saveFileDlg.FileName = "Pre-Invoice-" + invoiceNo;
          
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
            int customerId = ddlCustomer.SelectedValue.ToInt();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

            string error = string.Empty;
            if (customerId == 0)
            {
                error += "Required : Customer";
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

            lblCriteria.Text = "Customer Invoice Report Related to '" + ddlCustomer.Text.ToStr() + "', Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);




            var list = General.GetQueryable<vu_Invoice>(a => a.CustomerId == customerId && a.InvoiceDate >= fromDate && a.InvoiceDate <= tillDate).ToList();
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

            General.ShowEmailForm(reportViewer1, "Account Pre-Invoice # "+invoiceNo);

        }

        private bool IsFormLoaded = false;

        private void rptfrmCustomerInvoice_Load(object sender, EventArgs e)
        {
            IsFormLoaded = true;
        
        }
     
     
    }
}
