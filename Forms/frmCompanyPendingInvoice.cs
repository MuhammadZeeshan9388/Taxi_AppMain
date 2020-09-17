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
    public partial class frmCompanyPendingInvoice : UI.SetupBase
    {
        InvoiceBO objMaster = null;
        ConditionalFormattingObject objYellow = new ConditionalFormattingObject();
        ConditionalFormattingObject objWhite = new ConditionalFormattingObject();
        RadDropDownMenu AddCommisionItems = null;
        int CompanyId = 0;
        public struct COLS
        {
            public static string Payment_ID = "Payment_ID";
            public static string Balnace = "Balnace";

            // for gride
            public static string InvoiceId = "InvoiceId";
            public static string InvoiceNo = "InvoiceNo";
            public static string InvoiceDate = "InvoiceDate";
            public static string DueDate = "DueDate";
            public static string Company = "Company";
            public static string CompanyId = "CompanyId";


            public static string Telephone = "Telephone";

            public static string InvoiceTotal = "InvoiceTotal";
            public static string CreditNote = "CreditNote";
            public static string Paid = "Paid";
            public static string Balance = "Balance";

        }
        public frmCompanyPendingInvoice()
        {
            InitializeComponent();
            Initialize();
        }

        public frmCompanyPendingInvoice(int Id)
        {
            InitializeComponent();
            Initialize();
            CompanyId = Id;
        }
        private void Initialize()
        {


            grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);
            AddCommisionItems = new RadDropDownMenu();
            AddCommisionItems.BackColor = Color.Orange;


            RadMenuItem AddCommisionItems1 = new RadMenuItem("Pay Invoice");
            AddCommisionItems1.ForeColor = Color.Blue;
            AddCommisionItems1.BackColor = Color.Blue;
            AddCommisionItems1.Font = new Font("Tahoma", 10, FontStyle.Bold);
            AddCommisionItems1.Click += AddCommisionItems1_Click;
            AddCommisionItems.Items.Add(AddCommisionItems1);


            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);

            objMaster = new InvoiceBO();
           

            this.SetProperties((INavigation)objMaster);

            this.Shown += new EventHandler(frmCompanyPendingInvoice_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            grdLister.RowsChanged += new GridViewCollectionChangedEventHandler(GridJobs_RowsChanged);
            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);

            grdLister.ShowRowHeaderColumn = false;
            grdLister.EnableHotTracking = false;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            grdLister.ShowGroupPanel = false;
            grdLister.ShowFilteringRow = true;
            //  grdLister.ScreenTipNeeded += new ScreenTipNeededEventHandler(grdLister_ScreenTipNeeded);
            this.grdLister.FilterChanged += new GridViewCollectionChangedEventHandler(grdLister_FilterChanged);
        }
        void grdLister_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            try
            {
                GridDataCellElement cell = e.ContextMenuProvider as GridDataCellElement;
                if (cell == null)
                    return;

                else if (cell.GridControl.Name == "grdLister")
                {
                    e.ContextMenu = AddCommisionItems;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
        private void AddCommisionItems1_Click(object sender, EventArgs e)
        {
            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewRowInfo)
            {
                PaymetForm(grdLister.CurrentRow);
            }
        }

        void grdLister_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            try
            {
                //  foreach (var item in grdLister.ChildRows)

                decimal InvoiceTotal = 0.00m;

                InvoiceTotal = grdLister.ChildRows.Sum(c => c.Cells[COLS.InvoiceTotal].Value.ToDecimal());
                lblInvoiceTotal.Text = "Invoice Total: " + InvoiceTotal + " | Paid Total : " + grdLister.Rows.Sum(c => c.Cells[COLS.Paid].Value.ToDecimal()) + " | Balance Total : " + grdLister.Rows.Sum(c => c.Cells[COLS.Balance].Value.ToDecimal());



            }
            catch
            {

            }
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

                    int Id = row.Cells[COLS.InvoiceId].Value.ToInt();
                    int companyId = row.Cells["CompanyId"].Value.ToInt();
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

            if (e.CellElement.RowInfo is GridViewSummaryRowInfo)
            {
                e.CellElement.DrawFill = true;
                e.CellElement.GradientStyle = GradientStyles.Solid;
                e.CellElement.BackColor = Color.Gainsboro;
                e.Row.Height = 35;
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                e.CellElement.Font = new Font("Tahoma", 11, FontStyle.Bold);
                e.CellElement.ForeColor = Color.Red;
            }




        }
        void GridJobs_RowsChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {
                LoadInvoiceList();
            }
        }
        private void Addbalance()
        {
            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = COLS.Balnace;
            colD.Name = COLS.Balnace;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.ReadOnly = true;
            grdLister.Columns.Add(colD);
        }
        private void AddDropdown()
        {
            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            colCombo.Name = COLS.Payment_ID;
            colCombo.HeaderText = "Status";
            colCombo.DataSource = General.GetQueryable<Invoice_PaymentType>(null).OrderBy(c => c.Id).ToList();
            colCombo.DisplayMember = "PaymentType";
            colCombo.ValueMember = "Id";
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdLister.Columns.Add(colCombo);
            colCombo.ReadOnly = false;
        }
        private void AddUpdateColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 80;
            col.Name = "btnUpdate";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Pay";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grid.Columns.Add(col);

            GridViewCommandColumn cmdcol = new GridViewCommandColumn();
            cmdcol.Width = 80;
            cmdcol.Name = "btnInfo";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Info";
            cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(cmdcol);
        }
        void frmCompanyPendingInvoice_Shown(object sender, EventArgs e)
        {

            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            this.InitializeForm("frmInvoice");

            //Addbalance();
            //AddDropdown();

            LoadInvoiceList();

            AddUpdateColumn(grdLister);


            // grdLister.Columns["Id"].IsVisible = false;

            grdLister.Columns["InvoiceNo"].HeaderText = "Invoice No";
            grdLister.Columns["InvoiceNo"].Width = 80;

            grdLister.Columns["InvoiceDate"].HeaderText = "Invoice Date";
            grdLister.Columns["InvoiceDate"].Width = 100;

            (grdLister.Columns["InvoiceDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["InvoiceDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";
            //(grdLister.Columns["Balance"] as GridViewDecimalColumn).FormatString = "{0:#,###0.00}";

            grdLister.Columns["DueDate"].HeaderText = "Due Date";
            grdLister.Columns["DueDate"].Width = 100;
            grdLister.Columns["Balance"].Width = 100;
            grdLister.Columns[COLS.CreditNote].Width = 100;

            (grdLister.Columns["DueDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
            (grdLister.Columns["DueDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

            grdLister.Columns["Company"].Width = 150;

            grdLister.Columns["Telephone"].Width = 100;

            grdLister.Columns["InvoiceTotal"].Width = 120;
            grdLister.Columns["InvoiceTotal"].HeaderText = "Invoice Total";
            //grdLister.Columns["btnUpdate"].Width = 120;
            grdLister.Columns["Payment_ID"].Width = 100;
            //grdLister.Columns["Payment"].IsVisible = false;

            //grdLister.Columns["Payment_ID"].PinPosition = PinnedColumnPosition.Right;
            //grdLister.Columns["Balnace"].PinPosition = PinnedColumnPosition.Right;
            //grdLister.Columns["btnUpdate"].PinPosition = PinnedColumnPosition.Right;


            //UI.GridFunctions.SetFilter(grdLister);
            //---------------------------------------------------------------


            DateTime dtNow = DateTime.Now.ToDate();


            objYellow.CellBackColor = Color.LightPink;
            objYellow.ConditionType = ConditionTypes.LessOrEqual;
            objYellow.TValue1 = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
            objYellow.TValue2 = string.Empty;



            objWhite.ApplyToRow = true;
            objWhite.RowBackColor = Color.White;
            objWhite.TValue1 = string.Format("{0:dd/MM/yyyy HH:mm}", DateTime.Now);
            objWhite.ConditionType = ConditionTypes.LessOrEqual;
            objWhite.TValue2 = string.Empty;


            grdLister.Columns["DueDate"].ConditionalFormattingObjectList.Add(objYellow);
            grdLister.Columns["DueDate"].ConditionalFormattingObjectList[0] = objYellow;



            


          


        }


     
        private void LoadInvoiceList()
        {
            try
            {

                //var query = from a in General.GetQueryable<Invoice>(c=>c.InvoiceTypeId==Enums.INVOICE_TYPE.ACCOUNT && c.PaymentID ==1 || c.PaymentID == 2)

                //            select new
                //            {
                //                Id = a.Id,
                //                InvoiceNo = a.InvoiceNo,
                //                InvoiceDate =a.InvoiceDate,
                //                DueDate = a.DueDate,
                //                Company = a.Gen_Company.CompanyName,
                //                Telephone = a.Gen_Company.TelephoneNo,
                //                InvoiceTotal=a.InvoiceTotal,
                //                Payment = a.PaymentID,
                //                //Balance = a.PaymentID == 2 ? a.InvoiceTotal / 2 : a.InvoiceTotal,
                //                Balance = a.invoice_Payments.FirstOrDefault().DefaultIfEmpty().Balance
                //            };




                //grdLister.DataSource = query.ToList();
                //DataTable dt = query.ToDataTable();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    int total = dt.Rows[i]["InvoiceTotal"].ToInt();
                //    int payment = dt.Rows[i]["Payment"].ToInt();

                //    grdLister.Rows[i].Cells[COLS.Payment_ID].Value = dt.Rows[i]["Payment"].ToInt();

                //}
                //--------------------------------------------------------------------
                //var query = General.GetQueryable<Invoice>(c => c.InvoiceTypeId == Enums.INVOICE_TYPE.ACCOUNT
                //    && (c.InvoicePaymentTypeID == null || c.InvoicePaymentTypeID == Enums.INVOICE_PAYMENTTYPES.UNPAID
                //    || c.InvoicePaymentTypeID == Enums.INVOICE_PAYMENTTYPES.HALFPAID
                //     || c.InvoicePaymentTypeID == Enums.INVOICE_PAYMENTTYPES.CUSTOM) && (c.CompanyId == CompanyId || CompanyId == 0));



                //var list = (from a in query.AsEnumerable()
                //            select new
                //            {
                //                Id = a.Id,
                //                InvoiceNo = a.InvoiceNo,
                //                InvoiceDate = a.InvoiceDate,
                //                DueDate = a.DueDate,
                //                CompanyId = a.CompanyId,
                //                Company = a.Gen_Company.CompanyName,
                //                Telephone = a.Gen_Company.TelephoneNo,
                //                InvoiceTotal = (a.Gen_Company.HasVat != null && a.Gen_Company.HasVat == true) ? string.Format("{0:f2}", (a.InvoiceTotal + ((a.InvoiceTotal * 20) / 100))) : string.Format("{0:f2}", a.InvoiceTotal),
                //                Payment = a.InvoicePaymentTypeID,
                //                Paid = a.InvoicePaymentTypeID == Enums.INVOICE_PAYMENTTYPES.HALFPAID ? (a.Gen_Company.HasVat == true ? (a.InvoiceTotal + ((a.InvoiceTotal * 20) / 100)) / 2 : (a.InvoiceTotal / 2)) : (a.Gen_Company.HasVat == true ? ((a.invoice_Payments.Sum(c => c.InvoicePayment))) : (a.invoice_Payments.Sum(c => c.InvoicePayment).ToDecimal())),
                //                //Balance2 = a.InvoicePaymentTypeID == Enums.INVOICE_PAYMENTTYPES.HALFPAID ? (a.Gen_Company.HasVat == true ? (a.InvoiceTotal + ((a.InvoiceTotal * 20) / 100)) / 2 : (a.InvoiceTotal / 2)) : (a.Gen_Company.HasVat == true ? (((a.InvoiceTotal + ((a.InvoiceTotal * 20) / 100)) - a.TotalInvoiceAmount.ToDecimal())) : (a.InvoiceTotal - a.TotalInvoiceAmount.ToDecimal())),
                //                //Balance = a.invoice_Payments.FirstOrDefault().DefaultIfEmpty().Balance
                //                //  Balance = a.invoice_Payments.LastOrDefault().DefaultIfEmpty().Balance
                //                // Balance = a.InvoicePaymentTypeID == Enums.INVOICE_PAYMENTTYPES.HALFPAID ? (a.Gen_Company.HasVat == true ? (a.InvoiceTotal + ((a.InvoiceTotal * 20) / 100)) / 2 : (a.InvoiceTotal / 2)) : (a.Gen_Company.HasVat == true ? (((    a.InvoiceTotal +  ((a.InvoiceTotal * 20) / 100)) - a.TotalInvoiceAmount.ToDecimal())) : (a.InvoiceTotal - a.TotalInvoiceAmount.ToDecimal())),
                //                Balance = a.InvoicePaymentTypeID == Enums.INVOICE_PAYMENTTYPES.HALFPAID ? (a.Gen_Company.HasVat == true ? (a.InvoiceTotal + ((a.InvoiceTotal * 20) / 100)) / 2 : (a.InvoiceTotal / 2)) : (a.Gen_Company.HasVat == true ? (((a.InvoiceTotal + ((a.InvoiceTotal * 20) / 100)) - a.invoice_Payments.Sum(c => c.InvoicePayment))) : (a.InvoiceTotal - a.invoice_Payments.Sum(c => c.InvoicePayment).ToDecimal())),
                //            }).OrderByDescending(c => c.InvoiceDate).ToList();

            //    new stp_GetPendingInvoicesResult().cre

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_GetPendingInvoices(CompanyId).ToList();

                    grdLister.RowCount = list.Count;
                    //  DataTable dt = list.ToDataTable();

                    for (int i = 0; i < list.Count; i++)
                    {
                        grdLister.Rows[i].Cells[COLS.InvoiceId].Value = list[i].Id;
                        grdLister.Rows[i].Cells[COLS.CompanyId].Value = list[i].CompanyId;
                        grdLister.Rows[i].Cells[COLS.InvoiceNo].Value = list[i].InvoiceNo;
                        grdLister.Rows[i].Cells[COLS.InvoiceDate].Value = list[i].InvoiceDate;
                        //grdLister.Rows[i].Cells[COLS.DueDate].Value = list[i].DueDate;
                        grdLister.Rows[i].Cells[COLS.Company].Value = list[i].CompanyName;
                        grdLister.Rows[i].Cells[COLS.Telephone].Value = list[i].TelephoneNo;
                        grdLister.Rows[i].Cells[COLS.InvoiceTotal].Value = list[i].InvoiceTotal;
                        grdLister.Rows[i].Cells[COLS.CreditNote].Value = list[i].CreditNoteTotal;
                        grdLister.Rows[i].Cells[COLS.Paid].Value = list[i].PaidAmount.ToDecimal();

                        string bal = list[i].DueDate.ToStr();

                        if (bal != "")
                        {
                            grdLister.Rows[i].Cells[COLS.DueDate].Value = list[i].DueDate;
                        }



                        //decimal hk = list[i].Balance.ToDecimal();
                        //int pay = list[i].Payment.ToInt();
                        //if (hk == 0 && pay == 2)
                        //{
                        //    grdLister.Rows[i].Cells[COLS.Balance].Value = list[i].Balance2.ToDecimal();
                        //}
                        //else
                        //{
                        //grdLister.Rows[i].Cells[COLS.Balance].Value = Math.Round(list[i].CurrentBalance.ToDecimal(), 2);
                        grdLister.Rows[i].Cells[COLS.Balance].Value =((list[i].InvoiceTotal.ToDecimal()-list[i].CreditNoteTotal.ToDecimal()) - list[i].PaidAmount.ToDecimal());

                        //   }
                        //  grdLister.Rows[i].Cells[COLS.Payment_ID].Value = list[i].InvoicePaymentTypeID;
                        grdLister.Rows[i].Cells[COLS.Payment_ID].Value = Enums.INVOICE_PAYMENTTYPES.CUSTOM;

                    }
                }

                decimal InvoiceTotal = 0.00m;


                InvoiceTotal = grdLister.Rows.Sum(c => c.Cells[COLS.InvoiceTotal].Value.ToDecimal());
                lblInvoiceTotal.Text = "Invoice Total: " + InvoiceTotal + " | Paid Total : " + grdLister.Rows.Sum(c => c.Cells[COLS.Paid].Value.ToDecimal()) + " | Balance Total : " + grdLister.Rows.Sum(c => c.Cells[COLS.Balance].Value.ToDecimal());

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }


        }

        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name.ToLower() == "btnupdate")
                {
                    int? val = gridCell.GridControl.CurrentRow.Cells["Payment_ID"].Value.ToInt();
                    string chk = gridCell.GridControl.CurrentRow.Cells["DueDate"].Value.ToStr();
                    if (val == Enums.INVOICE_PAYMENTTYPES.CUSTOM)
                    {
                        PaymetForm(grdLister.CurrentRow);
                        // ShowPaymetForm(gridCell.GridControl.CurrentRow.Cells["InvoiceId"].Value.ToInt(), gridCell.GridControl.CurrentRow.Cells["InvoiceNo"].Value.ToStr(), gridCell.GridControl.CurrentRow.Cells["InvoiceTotal"].Value.ToStr()); ;
                    }
                    //else if (val == Enums.INVOICE_PAYMENTTYPES.UNPAID)
                    //{
                    //    int Id = gridCell.GridControl.CurrentRow.Cells["InvoiceId"].Value.ToInt();
                    //    using (TaxiDataContext db = new TaxiDataContext())
                    //    {
                    //        var list = db.invoice_Payments.Where(c => c.invoiceId == Id).ToList();
                    //        if (list.Count > 0)
                    //        {
                    //            db.invoice_Payments.DeleteAllOnSubmit(list);
                    //        }
                    //        var query = db.Invoices.FirstOrDefault(c => c.Id == Id);
                    //        query.InvoicePaymentTypeID = Enums.INVOICE_PAYMENTTYPES.UNPAID;
                    //        db.SubmitChanges();
                    //    }
                    //    // var list=
                    //}
                    //else if (val == Enums.INVOICE_PAYMENTTYPES.HALFPAID)
                    //{


                    //    long InvoiceId = gridCell.GridControl.CurrentRow.Cells["InvoiceId"].Value.ToLong();
                    //    long paymentID = gridCell.GridControl.CurrentRow.Cells["Payment_ID"].Value.ToLong();

                    //    InvoiceBO objMaster = new InvoiceBO();
                    //    objMaster.GetByPrimaryKey(InvoiceId);
                    //    if (objMaster.Current != null)
                    //    {

                    //        //if (val == Enums.INVOICE_PAYMENTTYPES.CUSTOM)
                    //        //{
                    //        //    objMaster.Current.TotalInvoiceAmount = gridCell.GridControl.CurrentRow.Cells[COLS.Paid].Value.ToDecimal();

                    //        //}
                    //        //else if (val == Enums.INVOICE_PAYMENTTYPES.UNPAID)
                    //        //{

                    //        //    objMaster.Current.TotalInvoiceAmount = 0.00m;
                    //        //}

                    //        objMaster.CheckDataValidation = false;
                    //        objMaster.Current.InvoicePaymentTypeID = paymentID.ToInt();
                    //        objMaster.Save();
                    //    }
                    //}
                      
                }
                else if (gridCell.ColumnInfo.Name.ToLower() == "btninfo")
                {

                    ViewInfo();
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
            }
        }

        private void ViewInfo()
        {
            try
            {


                GridViewRowInfo row = grdLister.CurrentRow;

                if (row != null && row is GridViewDataRowInfo)
                {

                    int Id = row.Cells[COLS.InvoiceId].Value.ToInt();
                    int companyId = row.Cells["CompanyId"].Value.ToInt();
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





                            string newLine = Environment.NewLine;

                            StringBuilder text = new StringBuilder();

                            text.Append("<html>");


                            text.Append("<b>" + " Invoice # : " + list[0].InvoiceNo.ToStr() + " @ <color=Red>" + string.Format("{0:dd/MM/yy HH:mm}", list[0].InvoiceDate) + "</b>");
                            text.Append("<br><br>");
                            text.Append(newLine + newLine);
                            text.Append("<br><b><color=Black>Company : " + list[0].CompanyName.ToStr() + "</b>");
                            text.Append(newLine + newLine);
                            text.Append("<br><b>Address : </b>" + list[0].CompanyAddress.ToStr());
                            text.Append("<br><br>");
                            text.Append(newLine + newLine);
                            text.Append("<br><b>Gross Total : </b>" + Math.Round(netAmount.ToDecimal(), 2));
                            text.Append(newLine + "Gross Total : " + Math.Round(netAmount.ToDecimal(), 2));

                            text.Append(newLine + newLine);


                            if (HasDiscount.ToStr() == "1")
                            {
                                text.Append("<b>Discount " + string.Format("{0:##0.##}", DiscountPercent) + "%" + " : " + Math.Round(discountAmount, 2));
                                text.Append(newLine + newLine);
                            }


                            if (valueAddedTax.ToDecimal() > 0)
                            {
                                text.Append("<br><b><color=Red>Vat : </b>       " + Math.Round(valueAddedTax.ToDecimal(), 2));
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

        void frmLocationList_Load(object sender, EventArgs e)
        {


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.IsVisible = false;
            col.HeaderText = "invoiceID";
            col.Name = COLS.InvoiceId;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.IsVisible = false;
            col.HeaderText = "CompanyId";
            col.Name = COLS.CompanyId;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.HeaderText = "invoiceNo";
            col.Name = COLS.InvoiceNo;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn date = new GridViewDateTimeColumn();
            date.ReadOnly = true;
            date.HeaderText = "Incoice Date";
            date.Name = COLS.InvoiceDate;
            grdLister.Columns.Add(date);


            date = new GridViewDateTimeColumn();
            date.ReadOnly = true;
            date.HeaderText = "Due Date";
            date.Name = COLS.DueDate;
            grdLister.Columns.Add(date);



            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.HeaderText = "Company";
            col.Name = COLS.Company;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.HeaderText = "Telephone";
            col.Name = COLS.Telephone;
            grdLister.Columns.Add(col);



            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            //  col = new GridViewTextBoxColumn();
            colD.ReadOnly = true;
            colD.HeaderText = "invoice Total";
            colD.Name = COLS.InvoiceTotal;
            colD.DecimalPlaces = 2;
            colD.Maximum = 99999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.Minimum = -99999999;
            grdLister.Columns.Add(colD);




            colD = new GridViewDecimalColumn();
            //  col = new GridViewTextBoxColumn();
            colD.ReadOnly = true;
            colD.HeaderText = "Credit Note";
            colD.Name = COLS.CreditNote;
            colD.DecimalPlaces = 2;
            colD.Maximum = 99999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.Minimum = -99999999;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.HeaderText = "Paid";
            colD.Name = COLS.Paid;
            grdLister.Columns.Add(colD);



            colD = new GridViewDecimalColumn();
            colD.ReadOnly = true;
            colD.Minimum = -99999999;
            colD.HeaderText = "Balance";
            colD.Name = COLS.Balance;
            colD.DecimalPlaces = 2;
            colD.Maximum = 99999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            colCombo.Name = COLS.Payment_ID;
            colCombo.HeaderText = "Status";
            colCombo.DataSource = General.GetQueryable<Invoice_PaymentType>(c => c.Id != 0).OrderBy(c => c.Id).ToList();
            colCombo.DisplayMember = "PaymentType";
            colCombo.ValueMember = "Id";
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdLister.Columns.Add(colCombo);
            colCombo.ReadOnly = false;
            colCombo.IsVisible = false;
            grdLister.CellBeginEdit += new GridViewCellCancelEventHandler(grdLister_CellBeginEdit);

        }

        void grdLister_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            //if (e.Column != null && e.Column.Name == COLS.Paid && e.Row!=null)
            //{
            //    if (e.Row.Cells[COLS.Payment_ID].Value.ToInt() != Enums.INVOICE_PAYMENTTYPES.CUSTOM)
            //    {
            //        e.Cancel = true;
            //    }

            //}
        }


        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {

            // ViewDetailForm(e.Row);
        }
        
        public  void PaymetForm(GridViewRowInfo row)
        {

            try
            {

                //frmInvoicePayment frm = new frmInvoicePayment(id, InvoiceNo, Total);
                frmPayment frm = new frmPayment(row.Cells["CompanyId"].Value.ToInt(), row.Cells["InvoiceId"].Value.ToInt());
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

        void Grid_RowsChanging(object sender, GridViewCollectionChangingEventArgs e)
        {
            if (e.Action == Telerik.WinControls.Data.NotifyCollectionChangedAction.Remove)
            {

                objMaster = new InvoiceBO();

                try
                {

                    objMaster.GetByPrimaryKey(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
                    objMaster.Delete(objMaster.Current);
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
            //if (grdLister.Columns.Count == 0) return;
            //if (grdLister.Columns["DueDate"].ConditionalFormattingObjectList[0] == objWhite)
            //{
            //    grdLister.Columns["DueDate"].ConditionalFormattingObjectList[0] = objYellow;

            //}
            //else
            //{
            //    grdLister.Columns["DueDate"].ConditionalFormattingObjectList[0] = objWhite;
            //}
        }
    }
}

