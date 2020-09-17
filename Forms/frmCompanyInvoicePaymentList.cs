using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using Telerik.WinControls.UI.Docking;
using Utils;
using Telerik.WinControls;
using Taxi_AppMain.Forms;

namespace Taxi_AppMain
{
    public partial class frmCompanyInvoicePaymentList : UI.SetupBase
    {
        InvoiceBO objMaster = null;
        int CompanyId = 0;
        public struct COLS
        {
            public static string Payment_ID = "Payment_ID";
            public static string Balnace = "Balnace";

            // for gride
            public static string InvoiceId = "InvoiceId";
            public static string InvoiceNo = "InvoiceNo";
            public static string InvoiceDate = "InvoiceDate";
            public static string Company = "Company";
            public static string CompanyId = "CompanyId";

            public static string Telephone = "Telephone";

            public static string InvoiceTotal = "InvoiceTotal";
            public static string Balance = "Balance";
            public static string Status = "Status";

        }
        public frmCompanyInvoicePaymentList()
        {
            InitializeComponent();
         //   grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);

            objMaster = new InvoiceBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyPendingInvoice_Shown);

            grdLister.EnableHotTracking = false;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            //grdLister.ShowRowHeaderColumn = false;

            grdLister.ShowGroupPanel = false;

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdLister.RowsChanged += new GridViewCollectionChangedEventHandler(GridJobs_RowsChanged);
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

           // grdLister.ScreenTipNeeded+=new ScreenTipNeededEventHandler(grdLister_ScreenTipNeeded);

            this.grdLister.FilterChanged += new GridViewCollectionChangedEventHandler(grdLister_FilterChanged);
        }
        public frmCompanyInvoicePaymentList(int Id)
        {
            InitializeComponent();
           // grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);

            objMaster = new InvoiceBO();
            CompanyId = Id;

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            this.Shown += new EventHandler(frmCompanyPendingInvoice_Shown);

            grdLister.EnableHotTracking = false;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            //grdLister.ShowRowHeaderColumn = false;

            grdLister.ShowGroupPanel = false;

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdLister.RowsChanged += new GridViewCollectionChangedEventHandler(GridJobs_RowsChanged);
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;

           // grdLister.ScreenTipNeeded += new ScreenTipNeededEventHandler(grdLister_ScreenTipNeeded);

            this.grdLister.FilterChanged += new GridViewCollectionChangedEventHandler(grdLister_FilterChanged);
        }

        void grdLister_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            try
            {
                decimal InvoiceTotal = 0.00m;

                InvoiceTotal = grdLister.ChildRows.Sum(c => c.Cells[COLS.InvoiceTotal].Value.ToDecimal());
                lblInvoiceTotal.Text = "Invoice Total: " + InvoiceTotal;
            }
            catch { }

        }
        void grdLister_ScreenTipNeeded(object sender, ScreenTipNeededEventArgs e)
        {
            ShowScreenTipForCellStats(e.Item as GridDataCellElement);
        }
        string TemplateName = "0";
        private void ShowScreenTipForCellStats(GridDataCellElement cell)
        {
            if (cell == null)
                return;

            try
            {


                GridViewRowInfo row = cell.RowElement.RowInfo;

                if (row != null && row is GridViewDataRowInfo)
                {

                    int Id = row.Cells["Id"].Value.ToInt();
                    int companyId = row.Cells[COLS.CompanyId].Value.ToInt();
                    List<vu_Invoice> list = null;

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        list = db.vu_Invoices.Where(c => c.Id == Id).ToList();


                        if (TemplateName == "0")
                            TemplateName = db.UM_Form_Templates.FirstOrDefault(c => c.UM_Form.FormName == "frmInvoiceReport" && c.IsDefault == true).DefaultIfEmpty().TemplateName.ToStr();

                    }

                    if (list.Count > 0)
                    {

                        var data = list.FirstOrDefault().DefaultIfEmpty();



                        decimal netAmount = 0.00m;

                        decimal invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                        //NC
                        //Changes Area for VAT start here.
                        decimal invoiceGrandTotal2 = 0.00m;//= (netAmount + data.AdminFees.ToDecimal());
                        decimal NetCharges = 0.0m;



                        decimal DriverCostNonVAT = 0.0m;
                        decimal BusinessCharge = 0.0m;
                        decimal VatOnBusinessCharge = 0.0m;
                        decimal TotalGPB = 0.0m;
                        //

                        if (TemplateName.ToStr() == "Template13" || TemplateName.ToStr() == "Template14" || TemplateName.ToStr() == "Template24")
                        {

                            if (TemplateName.ToStr() == "Template13" || TemplateName.ToStr() == "Template24")
                            {
                                netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                                   .Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal());
                                //  NetCharges = list.Where(c => c.VehicleType != "Saloon" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal());

                            }
                            else if (TemplateName.ToStr() == "Template14")
                            {
                                netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                                   .Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());

                                //  NetCharges = list.Where(c => c.VehicleType != "Saloon" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());
                            }

                            invoiceGrandTotal = netAmount;

                        }
                        else if (TemplateName.ToStr() == "Template17")
                        {
                            NetCharges = list.Where(c => c.VehicleType != "Coach" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());
                            netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                        .Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.MeetAndGreetCharges.ToDecimal() + c.CongtionCharges.ToDecimal());

                            // NetCharges = list.Where(c => c.VehicleType != "Coach" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.MeetAndGreetCharges.ToDecimal() + c.CongtionCharges.ToDecimal());

                            invoiceGrandTotal = NetCharges + data.AdminFees.ToDecimal();
                            //  invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();
                            invoiceGrandTotal2 = netAmount + data.AdminFees.ToDecimal();



                        }
                        //else if(&& this.ExportFileType.ToStr().ToLower() == "excel")
                        else if (TemplateName.ToStr() == "Template18")
                        {

                            netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
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
                        else if (TemplateName.ToStr() == "Template10" || TemplateName.ToStr() == "Template25"
                            || TemplateName.ToStr() == "Template27")
                        {


                            netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                           .Sum(c => c.Charges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal());


                            invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                        }
                        else if (TemplateName.ToStr() == "Template21")
                        {

                            netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                           .Sum(c => c.Charges.ToDecimal());


                            invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                        }


                        else
                        {
                            netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                            .Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal());


                            invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                        }


                        string vat = "0";
                        decimal discountAmount = 0.00m;
                        decimal DiscountPercent = 0.00m;
                        decimal valueAddedTax = 0.0m;
                        decimal AdminFeesPercent = 0.00m;
                        decimal AdminFees = 0.00m;
                        string HasAdminFees = string.Empty;
                        string HasDiscount = "0";
                        string CompanyAccountNo = string.Empty;


                        if (companyId != 0)
                        {
                            Gen_Company objCompany = General.GetObject<Gen_Company>(c => c.Id == companyId);

                            if (objCompany != null)
                            {
                                if (objCompany.HasVat.ToBool())
                                {


                                    valueAddedTax = (invoiceGrandTotal * 20) / 100;
                                    vat = "1";
                                    if (objCompany.VatOnlyOnAdminFees.ToBool())
                                    {
                                        valueAddedTax = (data.AdminFees.ToDecimal() * 20) / 100;
                                        vat = "1";

                                    }

                                }

                                if (objCompany.DiscountPercentage.ToDecimal() > 0)
                                {
                                    discountAmount = (invoiceGrandTotal * objCompany.DiscountPercentage.ToDecimal()) / 100;
                                    DiscountPercent = objCompany.DiscountPercentage.ToDecimal();
                                    HasDiscount = "1";
                                }
                                if (objCompany.AdminFees > 0)
                                {
                                    decimal GrandAmount = (invoiceGrandTotal - discountAmount);
                                    AdminFees = (invoiceGrandTotal * objCompany.AdminFees.ToDecimal()) / 100;
                                    AdminFeesPercent = objCompany.AdminFees.ToDecimal();
                                    HasAdminFees = "1";
                                }


                                CompanyAccountNo = objCompany.AccountNo.ToStr().Trim();
                            }

                        }

                        if (TemplateName.ToStr() == "Template17")
                        {
                            invoiceGrandTotal = (invoiceGrandTotal2 + valueAddedTax) - discountAmount;
                        }
                        else
                        {
                            invoiceGrandTotal = (invoiceGrandTotal + valueAddedTax) - discountAmount;
                        }







                        StringBuilder text = new StringBuilder();

                        text.Append("<html>");


                        text.Append("<b>" + " Invoice # : " + list[0].InvoiceNo.ToStr() + " @ <color=Red>" + string.Format("{0:dd/MM/yy HH:mm}", list[0].InvoiceDate) + "</b>");
                        text.Append("<br><br>");
                        text.Append("<br><b><color=Black>Company : " + list[0].CompanyName.ToStr() + "</b>");
                        text.Append("<br>Address : " + list[0].CompanyAddress.ToStr());
                        text.Append("<br><br>");
                        text.Append("<br><b>Gross Total : </b>" + Math.Round(netAmount.ToDecimal(), 2));

                        if (valueAddedTax.ToDecimal() > 0)
                        {
                            text.Append("<br><b>Vat : </b>" + Math.Round(valueAddedTax.ToDecimal(), 2));

                        }


                        if (AdminFees.ToDecimal() > 0)
                        {
                            text.Append("<br><b>Admin Fees : </b>" + Math.Round(AdminFees.ToDecimal(), 2));

                        }

                        text.Append("<br><b>Total Due : </b>" + Math.Round(invoiceGrandTotal + AdminFees, 2));
                        text.Append("<br><br>");


                        RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();
                        screenTip.CaptionLabel.Margin = new Padding(3);

                        screenTip.CaptionLabel.Text = text.ToStr();
                        //   screenTip.CaptionLabel.Text = text.ToStr();
                        screenTip.MainTextLabel.Text = string.Empty;
                        screenTip.EnableCustomSize = false;


                        cell.ScreenTip = screenTip;
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



    


        void GridJobs_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                LoadInvoiceList();
            }
        }



        void frmCompanyPendingInvoice_Shown(object sender, EventArgs e)
        {

            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            this.InitializeForm("frmInvoice");

            LoadInvoiceList();


            GridViewCommandColumn cmdcol = new GridViewCommandColumn();
            cmdcol.Width = 150;
            cmdcol.Name = "btnPay";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Payment History";
            cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(cmdcol);

            cmdcol = new GridViewCommandColumn();
            cmdcol.Width = 80;
            cmdcol.Name = "btnInfo";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Info";
            cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(cmdcol);

            grdLister.AddDeleteColumn();
            grdLister.Columns["btnDelete"].Width = 70;
            grdLister.Columns["btnDelete"].IsVisible = false;

            grdLister.Columns["CompanyId"].IsVisible = false;
            grdLister.Columns["Id"].IsVisible = false;
              grdLister.Columns["Payment"].IsVisible = false;

            grdLister.Columns["InvoiceNo"].HeaderText = "Invoice No";
            grdLister.Columns["InvoiceNo"].Width = 80;

            grdLister.Columns["InvoiceDate"].HeaderText = "Invoice Date";
            grdLister.Columns["InvoiceDate"].Width = 100;

            (grdLister.Columns["InvoiceDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["InvoiceDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            //(grdLister.Columns["Balance"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";

            grdLister.Columns["DueDate"].HeaderText = "Due Date";
            grdLister.Columns["DueDate"].Width = 100;
        //    grdLister.Columns["Balance"].Width = 70;

            (grdLister.Columns["DueDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["DueDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

            grdLister.Columns["Company"].Width = 260;

            grdLister.Columns["Telephone"].Width = 100;

            grdLister.Columns["InvoiceTotal"].Width = 120;
            grdLister.Columns["InvoiceTotal"].HeaderText = "Total Paid";
            //grdLister.Columns["btnUpdate"].Width = 120;
            //  grdLister.Columns["Payment_ID"].Width = 100;

          

        }

        private void LoadInvoiceList()
        {
            try
            {


               
                //var query = General.GetQueryable<Invoice>(c => c.InvoiceTypeId == Enums.INVOICE_TYPE.ACCOUNT && c.PaymentID == 3);

                var data1 = General.GetQueryable<Invoice>(c=>(c.CompanyId==CompanyId || CompanyId==0));
           //     var data2 = General.GetQueryable<invoice_Payment>(null).AsEnumerable().OrderByDescending(c => c.PaymentDate); ; ;
                
              var list = (from a in data1
                          where a.InvoicePaymentTypeID!=null && a.InvoicePaymentTypeID==Enums.INVOICE_PAYMENTTYPES.FULLPAID
                          //join b in data2 on  a.Id equals b.invoiceId
                            select  new
                            {
                                Id = a.Id,
                                InvoiceNo = a.InvoiceNo,
                                InvoiceDate = a.InvoiceDate,
                                DueDate = a.DueDate,
                                CompanyId = a.CompanyId,
                                Company = a.Gen_Company.CompanyName,
                                Telephone = a.Gen_Company.TelephoneNo,
                                InvoiceTotal = a.PaidAmount,
                                Payment = a.InvoicePaymentTypeID,
                             //   Balance2 = a.InvoicePaymentTypeID == 2 ? a.InvoiceTotal / 2 : a.InvoiceTotal,
                                //Balance = a.invoice_Payments.FirstOrDefault().DefaultIfEmpty().Balance
                          //      Balance = a.invoice_Payments.LastOrDefault().DefaultIfEmpty().Balance
                            }).OrderByDescending(c=>c.InvoiceDate).ToList();

              //  grdLister.RowCount = list.Count;

              grdLister.DataSource = list;

              decimal InvoiceTotal = 0.00m;
              decimal BalanceAmount = 0.00m;

              InvoiceTotal = grdLister.Rows.Sum(c => c.Cells[COLS.InvoiceTotal].Value.ToDecimal());
              lblInvoiceTotal.Text = "Total Paid Amount : " + InvoiceTotal;
               // DataTable dt = list.ToDataTable();

                //for (int i = 0; i < list.Count; i++)
                //{
                //    grdLister.Rows[i].Cells[COLS.InvoiceId].Value = list[i].Id;
                //    grdLister.Rows[i].Cells[COLS.InvoiceNo].Value = list[i].InvoiceNo;
                //    grdLister.Rows[i].Cells[COLS.InvoiceDate].Value = list[i].InvoiceDate;

                //    grdLister.Rows[i].Cells[COLS.Company].Value = list[i].Company;
                //    grdLister.Rows[i].Cells[COLS.Telephone].Value = list[i].Telephone;
                //    grdLister.Rows[i].Cells[COLS.InvoiceTotal].Value = list[i].InvoiceTotal;
                //    grdLister.Rows[i].Cells[COLS.Status].Value = list[i].status.ToStr();

                //    string hk = list[i].Balance.ToStr();
                //    if (hk.Contains("-"))
                //    {
                //        grdLister.Rows[i].Cells[COLS.Balance].Value = list[i].Balance.ToDecimal();

                //    }
                //    else
                //    {
                //        grdLister.Rows[i].Cells[COLS.Balance].Value = "Nill";
                //    }
                //}

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }


        }



        void frmCompanyInvoicePaymentList_Load(object sender, EventArgs e)
        {


            //GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            //col.ReadOnly = true;
            //col.IsVisible = false;
            //col.HeaderText = "invoiceID";
            //col.Name = COLS.InvoiceId;
            //grdLister.Columns.Add(col);

            //col = new GridViewTextBoxColumn();
            //col.ReadOnly = true;
            //col.HeaderText = "invoiceNo";
            //col.Name = COLS.InvoiceNo;
            //grdLister.Columns.Add(col);

            //GridViewDateTimeColumn date = new GridViewDateTimeColumn();
            //date.ReadOnly = true;
            //date.HeaderText = "Incoice Date";
            //date.Name = COLS.InvoiceDate;
            //grdLister.Columns.Add(date);



            //col = new GridViewTextBoxColumn();
            //col.ReadOnly = true;
            //col.HeaderText = "Company";
            //col.Name = COLS.Company;
            //grdLister.Columns.Add(col);

            //col = new GridViewTextBoxColumn();
            //col.ReadOnly = true;
            //col.HeaderText = "Telephone";
            //col.Name = COLS.Telephone;
            //grdLister.Columns.Add(col);



            //col = new GridViewTextBoxColumn();
            //col.ReadOnly = true;
            //col.HeaderText = "invoice Total";
            //col.Name = COLS.InvoiceTotal;
            //grdLister.Columns.Add(col);


            //col = new GridViewTextBoxColumn();
            //col.ReadOnly = true;
            //col.HeaderText = "Balance";
            //col.Name = COLS.Balance;
            //grdLister.Columns.Add(col);

            //col = new GridViewTextBoxColumn();
            //col.ReadOnly = true;
            //col.HeaderText = "Status";
            //col.Name = COLS.Status;
            //grdLister.Columns.Add(col);


        }

        //void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        //{

        //    ViewDetailForm(e.Row);
        //}
        //private void ViewDetailForm(GridViewRowInfo row)
        //{
        //    try
        //    {
        //        int? val = row.Cells[COLS.Payment_ID].Value.ToInt();
        //        if (row != null && row is GridViewDataRowInfo && val == 4)
        //        {
        //            ShowPaymetForm(row.Cells["InvoiceId"].Value.ToInt(), row.Cells["InvoiceNo"].Value.ToStr(), row.Cells["InvoiceTotal"].Value.ToStr()); ;
        //        }

        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //}
        //public static void ShowPaymetForm(int id, string InvoiceNo, string Total)
        //{

        //    try
        //    {

        //        //frmInvoicePayment frm = new frmInvoicePayment(id, InvoiceNo, Total);
        //        frmPayment frm = new frmPayment(id, InvoiceNo, Total);
        //        frm.MaximizeBox = false;
        //        frm.ShowDialog();
        //        frm.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        ENUtils.ShowMessage(ex.Message);

        //    }


        //}

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new InvoiceBO();

                try
                {
                    long id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                   
                    objMaster.GetByPrimaryKey(id);

                    if (objMaster.Current != null)
                    {

                        objMaster.Current.InvoicePaymentTypeID = Enums.INVOICE_PAYMENTTYPES.UNPAID;

                        objMaster.Save();
                    }
                }
                catch (Exception ex)
                {
                    if (objMaster.Errors.Count > 0)
                        ENUtils.ShowMessage(objMaster.ShowErrors());
                    else
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }
                    e.Cancel = true;

                }
            }
        }







        public override void RefreshData()
        {
            PopulateData();
        }



        public override void PopulateData()
        {

            LoadInvoiceList();

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            //  txtSearch.Text = string.Empty;
            LoadInvoiceList();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            PopulateData();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PopulateData();

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Invoice ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        

                        //objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["InvoiceId"].Value.ToLong());



                        grdLister.CurrentRow.Delete();

                        //int InvoiceId = grdLister.CurrentRow.Cells["InvoiceId"].Value.ToInt();
                        //(new TaxiDataContext()).stp_DeleteInvoicePayment(InvoiceId);
                        //LoadInvoiceList();
                        
                       
                    }
                }
               else  if (gridCell.ColumnInfo.Name.ToLower() == "btninfo")
                {
                    ViewInfo();
                }
                else if(gridCell.ColumnInfo.Name.ToLower()=="btnpay")
                {


                    try
                    {

                        //frmInvoicePayment frm = new frmInvoicePayment(id, InvoiceNo, Total);
                        frmPayment frm = new frmPayment(gridCell.RowInfo.Cells["CompanyId"].Value.ToInt(), gridCell.RowInfo.Cells["Id"].Value.ToInt());
                        frm.MaximizeBox = false;
                        frm.ShowDialog();
                        frm.Dispose();

                        LoadInvoiceList();
                    }
                    catch (Exception ex)
                    {
                        ENUtils.ShowMessage(ex.Message);

                    }

                }
                }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void ViewInfo()
        {

            try
            {


                GridViewRowInfo row = grdLister.CurrentRow;

                if (row != null && row is GridViewDataRowInfo)
                {

                    int Id = row.Cells["Id"].Value.ToInt();
                    int companyId = row.Cells[COLS.CompanyId].Value.ToInt();
                    List<vu_Invoice> list = null;

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        list = db.vu_Invoices.Where(c => c.Id == Id).ToList();


                        if (TemplateName == "0")
                            TemplateName = db.UM_Form_Templates.FirstOrDefault(c => c.UM_Form.FormName == "frmInvoiceReport" && c.IsDefault == true).DefaultIfEmpty().TemplateName.ToStr();



                        if (list.Count > 0)
                        {

                            var data = list.FirstOrDefault().DefaultIfEmpty();



                            decimal netAmount = 0.00m;

                            decimal invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                            //NC
                            //Changes Area for VAT start here.
                            decimal invoiceGrandTotal2 = 0.00m;//= (netAmount + data.AdminFees.ToDecimal());
                            decimal NetCharges = 0.0m;



                            decimal DriverCostNonVAT = 0.0m;
                            decimal BusinessCharge = 0.0m;
                            decimal VatOnBusinessCharge = 0.0m;
                            decimal TotalGPB = 0.0m;
                            //

                            if (TemplateName.ToStr() == "Template13" || TemplateName.ToStr() == "Template14" || TemplateName.ToStr() == "Template24")
                            {

                                if (TemplateName.ToStr() == "Template13" || TemplateName.ToStr() == "Template24")
                                {
                                    netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                                       .Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal());
                                    //  NetCharges = list.Where(c => c.VehicleType != "Saloon" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal());

                                }
                                else if (TemplateName.ToStr() == "Template14")
                                {
                                    netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                                       .Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());

                                    //  NetCharges = list.Where(c => c.VehicleType != "Saloon" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());
                                }

                                invoiceGrandTotal = netAmount;

                            }
                            else if (TemplateName.ToStr() == "Template17")
                            {
                                NetCharges = list.Where(c => c.VehicleType != "Coach" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.ExtraDropCharges.ToDecimal());
                                netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                            .Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.MeetAndGreetCharges.ToDecimal() + c.CongtionCharges.ToDecimal());

                                // NetCharges = list.Where(c => c.VehicleType != "Coach" && c.PaymentTypeId.ToInt() != 6).Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal() + c.MeetAndGreetCharges.ToDecimal() + c.CongtionCharges.ToDecimal());

                                invoiceGrandTotal = NetCharges + data.AdminFees.ToDecimal();
                                //  invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();
                                invoiceGrandTotal2 = netAmount + data.AdminFees.ToDecimal();



                            }
                            //else if(&& this.ExportFileType.ToStr().ToLower() == "excel")
                            else if (TemplateName.ToStr() == "Template18")
                            {

                                netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
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
                            else if (TemplateName.ToStr() == "Template10" || TemplateName.ToStr() == "Template25"
                                || TemplateName.ToStr() == "Template27")
                            {


                                netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                               .Sum(c => c.Charges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal());


                                invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                            }
                            else if (TemplateName.ToStr() == "Template21")
                            {

                                netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                               .Sum(c => c.Charges.ToDecimal());


                                invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                            }



                            else
                            {
                                netAmount = list.Where(c => c.PaymentTypeId.ToInt() != 6)
                                                .Sum(c => c.Charges.ToDecimal() + c.ExtraDropCharges.ToDecimal() + c.ParkingCharges.ToDecimal() + c.WaitingCharges.ToDecimal());


                                invoiceGrandTotal = netAmount + data.AdminFees.ToDecimal();

                            }


                            string vat = "0";
                            decimal discountAmount = 0.00m;
                            decimal DiscountPercent = 0.00m;
                            decimal valueAddedTax = 0.0m;
                            decimal AdminFeesPercent = 0.00m;
                            decimal AdminFees = 0.00m;
                            string HasAdminFees = string.Empty;
                            string HasDiscount = "0";
                            string CompanyAccountNo = string.Empty;


                            if (companyId != 0)
                            {
                                Gen_Company objCompany = db.Gen_Companies.FirstOrDefault(c => c.Id == companyId);

                                if (objCompany != null)
                                {
                                    if (objCompany.HasVat.ToBool())
                                    {


                                        valueAddedTax = (invoiceGrandTotal * 20) / 100;
                                        vat = "1";
                                        if (objCompany.VatOnlyOnAdminFees.ToBool())
                                        {
                                            valueAddedTax = (data.AdminFees.ToDecimal() * 20) / 100;
                                            vat = "1";

                                        }

                                    }

                                    if (objCompany.DiscountPercentage.ToDecimal() > 0)
                                    {
                                        discountAmount = (invoiceGrandTotal * objCompany.DiscountPercentage.ToDecimal()) / 100;
                                        DiscountPercent = objCompany.DiscountPercentage.ToDecimal();
                                        HasDiscount = "1";
                                    }
                                    if (objCompany.AdminFees > 0)
                                    {
                                        decimal GrandAmount = (invoiceGrandTotal - discountAmount);
                                        AdminFees = (invoiceGrandTotal * objCompany.AdminFees.ToDecimal()) / 100;
                                        AdminFeesPercent = objCompany.AdminFees.ToDecimal();
                                        HasAdminFees = "1";
                                    }


                                    CompanyAccountNo = objCompany.AccountNo.ToStr().Trim();
                                }

                            }

                            if (TemplateName.ToStr() == "Template17")
                            {
                                invoiceGrandTotal = (invoiceGrandTotal2 + valueAddedTax) - discountAmount;
                            }
                            else if (TemplateName.ToStr() == "Template45")
                            {
                                if (vat == "1")
                                {


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







                            StringBuilder text = new StringBuilder();

                            //text.Append("<html>");


                            //text.Append("<b>" + " Invoice # : " + list[0].InvoiceNo.ToStr() + " @ <color=Red>" + string.Format("{0:dd/MM/yy HH:mm}", list[0].InvoiceDate) + "</b>");
                            //text.Append("<br><br>");
                            //text.Append("<br><b><color=Black>Company : " + list[0].CompanyName.ToStr() + "</b>");
                            //text.Append("<br>Address : " + list[0].CompanyAddress.ToStr());
                            //text.Append("<br><br>");
                            //text.Append("<br><b>Gross Total : </b>" + Math.Round(netAmount.ToDecimal(), 2));

                            //if (valueAddedTax.ToDecimal() > 0)
                            //{
                            //    text.Append("<br><b>Vat : </b>" + Math.Round(valueAddedTax.ToDecimal(), 2));

                            //}


                            //if (AdminFees.ToDecimal() > 0)
                            //{
                            //    text.Append("<br><b>Admin Fees : </b>" + Math.Round(AdminFees.ToDecimal(), 2));

                            //}

                            //text.Append("<br><b>Total Due : </b>" + Math.Round(invoiceGrandTotal + AdminFees, 2));
                            //text.Append("<br><br>");
                            string newLine = Environment.NewLine;
                            text.Append("<html>");


                            text.Append("<b>" + " Invoice # : " + list[0].InvoiceNo.ToStr() + " @ <color=Red>" + string.Format("{0:dd/MM/yy HH:mm}", list[0].InvoiceDate) + "</b>");
                            text.Append("<br><br>");
                            text.Append(newLine + newLine);
                            text.Append("<br><b><color=Black>Company : " + list[0].CompanyName.ToStr() + "</b>");
                            text.Append(newLine + newLine);
                            text.Append("<br>Address : " + list[0].CompanyAddress.ToStr());
                            text.Append(newLine + newLine);
                            text.Append("<br><br>");
                            text.Append(newLine + newLine);
                            text.Append("<br><br><b><color=Red>Gross Total : </b>" + Math.Round(netAmount.ToDecimal(), 2));
                            text.Append(newLine + newLine);


                            if (HasDiscount.ToStr() == "1")
                            {
                                text.Append("<b>Discount " + string.Format("{0:##0.##}", DiscountPercent) + "%" + " : " + Math.Round(discountAmount, 2));
                                text.Append(newLine + newLine);
                            }


                            if (valueAddedTax.ToDecimal() > 0)
                            {
                                text.Append("<br><b><color=Red>Vat : </b>" + Math.Round(valueAddedTax.ToDecimal(), 2));
                                text.Append(newLine + newLine);
                            }


                            if (AdminFees.ToDecimal() > 0)
                            {
                                text.Append("<br><b><color=Red>Admin Fees : </b>" + Math.Round(AdminFees.ToDecimal(), 2));
                                text.Append(newLine + newLine);
                            }

                            text.Append("<br><b><color=Red>Total Due : </b>" + Math.Round(invoiceGrandTotal + AdminFees, 2));
                            text.Append("<br><br>");
                            text.Append(newLine + newLine);

                            frmCustomScreenInvoiceTip frmTip = new frmCustomScreenInvoiceTip(text.ToString());
                            frmTip.StartPosition = FormStartPosition.CenterParent;
                            frmTip.ShowInTaskbar = false;
                            frmTip.ShowDialog();
                            KeyEventArgs key = frmTip.LastSendEventArgs;

                            frmTip.Dispose();
                        }
                        //RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();
                        //screenTip.CaptionLabel.Margin = new Padding(3);

                        //screenTip.CaptionLabel.Text = text.ToStr();
                        ////   screenTip.CaptionLabel.Text = text.ToStr();
                        //screenTip.MainTextLabel.Text = string.Empty;
                        //screenTip.EnableCustomSize = false;




                        //cell.ScreenTip = screenTip;
                    }

                }

            }
            catch (Exception ex)
            {


            }

        }
    }
}

