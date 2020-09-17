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
    public partial class frmCompanyInvoiceList : UI.SetupBase
    {
        InvoiceBO objMaster = null;
        int CompanyId = 0;
        public frmCompanyInvoiceList()
        {
            InitializeComponent();
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new InvoiceBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            grdLister.AllowEditRow = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            this.btnPrintSelected.Click += new EventHandler(btnPrintSelected_Click);
            this.grdLister.KeyDown += new KeyEventHandler(grdLister_KeyDown);
            this.grdLister.MouseDown += new MouseEventHandler(grdLister_MouseDown);
         //   grdLister.ScreenTipNeeded += new ScreenTipNeededEventHandler(grdLister_ScreenTipNeeded);
             // grdLister.ToolTipTextNeeded+=new ToolTipTextNeededEventHandler(grdLister_ToolTipTextNeeded);

            



        }


        public frmCompanyInvoiceList(int Id)
        {
            InitializeComponent();
            CompanyId = Id;
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);
            grdLister.RowsChanging += new Telerik.WinControls.UI.GridViewCollectionChangingEventHandler(Grid_RowsChanging);
            objMaster = new InvoiceBO();

            this.SetProperties((INavigation)objMaster);

            grdLister.ShowRowHeaderColumn = false;
            grdLister.AllowEditRow = false;
            this.Shown += new EventHandler(frmCompanyInvoiceList_Shown);

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            this.btnPrintSelected.Click += new EventHandler(btnPrintSelected_Click);
            this.grdLister.KeyDown += new KeyEventHandler(grdLister_KeyDown);
            this.grdLister.MouseDown += new MouseEventHandler(grdLister_MouseDown);
            //   grdLister.ScreenTipNeeded += new ScreenTipNeededEventHandler(grdLister_ScreenTipNeeded);
            //grdLister.ToolTipTextNeeded += new ToolTipTextNeededEventHandler(grdLister_ToolTipTextNeeded);





        }
        void grdLister_ToolTipTextNeeded(object sender, ToolTipTextNeededEventArgs e)
        {

            try
            {

                GridDataCellElement cell = (GridDataCellElement)sender;



                //   GridViewRowInfo row =grdLister.CurrentRow;

                if (cell != null && cell.RowInfo is GridViewDataRowInfo)
                {
                    int Id = cell.RowInfo.Cells["Id"].Value.ToInt();
                    // int Id = row.Cells["Id"].Value.ToInt();
                    int companyId = cell.RowInfo.Cells["CompanyId"].Value.ToInt();
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
                                // Gen_Company objCompany = General.GetObject<Gen_Company>(c => c.Id == companyId);
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
                            else
                            {
                                invoiceGrandTotal = (invoiceGrandTotal + valueAddedTax) - discountAmount;
                            }







                            StringBuilder text = new StringBuilder();




                            string newLine = Environment.NewLine;

                            text.Append(" Invoice # : " + list[0].InvoiceNo.ToStr() + " @ " + string.Format("{0:dd/MM/yy HH:mm}", list[0].InvoiceDate));
                            text.Append(newLine + newLine);
                            text.Append(newLine + "Company : " + list[0].CompanyName.ToStr());
                            text.Append(newLine + "Address : " + list[0].CompanyAddress.ToStr());
                            text.Append(newLine + newLine);
                            text.Append(newLine + "Gross Total :" + Math.Round(netAmount.ToDecimal(), 2));

                            if (valueAddedTax.ToDecimal() > 0)
                            {
                                text.Append(newLine + "Vat : " + Math.Round(valueAddedTax.ToDecimal(), 2));

                            }


                            if (AdminFees.ToDecimal() > 0)
                            {
                                text.Append(newLine + "Admin Fees : " + Math.Round(AdminFees.ToDecimal(), 2));

                            }

                            text.Append(newLine + "Total Due : " + Math.Round(invoiceGrandTotal + AdminFees, 2));
                            text.Append(newLine + newLine);


                            // RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();
                            //  screenTip.CaptionLabel.Margin = new Padding(3);

                            //  screenTip.CaptionLabel.Text = text.ToStr();
                            //   screenTip.CaptionLabel.Text = text.ToStr();
                            //   screenTip.MainTextLabel.Text = string.Empty;
                            //  screenTip.EnableCustomSize = false;


                            e.ToolTipText = text.ToString();
                        }
                    }

                }

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

                    int Id = row.Cells["Id"].Value.ToInt();
                    int companyId = row.Cells["CompanyId"].Value.ToInt();
                    List<vu_Invoice> list = null;

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        list=  db.vu_Invoices.Where(c => c.Id == Id).ToList();


                        if(TemplateName=="0")
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


                            text.Append("<b>" +  " Invoice # : " + list[0].InvoiceNo.ToStr() + " @ <color=Red>" + string.Format("{0:dd/MM/yy HH:mm}", list[0].InvoiceDate) + "</b>");
                            text.Append("<br><b><color=Blue>Total Jobs : " + list.Count.ToStr() + "</b>");
                            text.Append("<br><br>");
                            text.Append("<br><b><color=Black>Company : " + list[0].CompanyName.ToStr()+"</b>");
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

                            text.Append("<br><b>Total Due : </b>" + Math.Round(invoiceGrandTotal+AdminFees, 2));
                            text.Append("<br><br>");                       


                        if(screenTip==null)
                            screenTip = new RadOffice2007ScreenTipElement();
                     
                        screenTip.CaptionLabel.Margin = new Padding(3);

                        screenTip.CaptionLabel.Text = text.ToStr();
                        //   screenTip.CaptionLabel.Text = text.ToStr();
                        screenTip.MainTextLabel.Text = string.Empty;
                        screenTip.EnableCustomSize = false;
                        screenTip.Visibility = ElementVisibility.Visible;

                        cell.ScreenTip = screenTip;
                    }
                    
                }

            }
            catch 
            {


            }



        }

        RadOffice2007ScreenTipElement screenTip = null;


        void grdLister_MouseDown(object sender, MouseEventArgs e)
        {
            if (grdLister.CurrentColumn == null || grdLister.CurrentColumn is GridViewCheckBoxColumn == false)
                return;

            try
            {
                 if (grdLister.CurrentRow.Cells["Check"].Value.ToBool())
                    grdLister.CurrentRow.Cells["Check"].Value = false;
                else
                    grdLister.CurrentRow.Cells["Check"].Value = true;
            }
            catch
            {
            }
        }

        void grdLister_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdLister.CurrentColumn == null || grdLister.CurrentRow == null)
                return;

            try
            {
                if (grdLister.CurrentRow.Cells["Check"].Value.ToBool())
                    grdLister.CurrentRow.Cells["Check"].Value = false;
                else
                    grdLister.CurrentRow.Cells["Check"].Value = true;
            }
            catch
            {
            }
        }

        void btnPrintSelected_Click(object sender, EventArgs e)
        {
            PrintInvoices();
        }
        private void PrintInvoices()
        {
            try
            {
              //  UM_Form_Template objReport = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "frmInvoiceReport" && c.IsDefault == true);
                frmInvoiceReport frm=null;
                if (grdLister.Rows.Count(c => c.Cells["Check"].Value.ToBool()) > 0)
                {
                    foreach (var item in grdLister.Rows)
                    {
                        if (item.Cells["Check"].Value.ToBool() == true)
                        {

                            long Id = item.Cells["Id"].Value.ToLong();
                            if (Id > 0)
                            {
                                objMaster.GetByPrimaryKey(Id);
                                ReportPrintDocument_Landscape rpt = null;

                                frm = new frmInvoiceReport();
                                frm.ObjInvoice = objMaster.Current;
                                frm.HasSplitByField = "None";
                                var list = General.GetQueryable<vu_Invoice>(a => a.Id == Id).OrderBy(c => c.PickupDate).ToList();
                                int count = list.Count;

                                frm.DataSource = list;
                                frm.GenerateReport();
                                rpt = new ReportPrintDocument_Landscape(frm.reportViewer1.LocalReport);
                                rpt.Print();
                                rpt.Dispose();
                            }
                        }
                    }
                }
                else
                {
                    ENUtils.ShowMessage("Please select invoice to print");
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        void frmCompanyInvoiceList_Shown(object sender, EventArgs e)
        {
            try
            {

                grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;



                this.InitializeForm("frmInvoice");
                LoadInvoiceList(0);

                GridViewCommandColumn cmdcol = new GridViewCommandColumn();
                cmdcol.Width = 80;
                cmdcol.Name = "btnInfo";
                cmdcol.UseDefaultText = true;
                cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
                cmdcol.DefaultText = "Info";
                cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
                grdLister.Columns.Add(cmdcol);

                grdLister.AddEditColumn();

                if (this.CanDelete)
                {
                    grdLister.AddDeleteColumn();
                    grdLister.Columns["btnDelete"].Width = 70;
                }


                grdLister.Columns["Id"].IsVisible = false;
                grdLister.Columns["CompanyId"].IsVisible = false;

                grdLister.Columns["InvoiceNo"].HeaderText = "Invoice No";
                grdLister.Columns["InvoiceNo"].Width = 80;

                grdLister.Columns["InvoiceDate"].HeaderText = "Invoice Date";
                grdLister.Columns["InvoiceDate"].Width = 100;

                (grdLister.Columns["InvoiceDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy";
                (grdLister.Columns["InvoiceDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy}";

                grdLister.Columns["Company"].HeaderText = "Account";
                grdLister.Columns["Company"].Width = 150;
                grdLister.Columns["Address"].Width = 240;


                grdLister.Columns["Telephone"].Width = 100;

                grdLister.Columns["InvoiceTotal"].Width = 100;
                grdLister.Columns["InvoiceTotal"].HeaderText = "Invoice Total";

                grdLister.Columns["btnEdit"].Width = 70;


                UI.GridFunctions.SetFilter(grdLister);


                btnVatCalculator.Visible = AppVars.listUserRights.Count(c => c.functionId == "SHOW VAT CALCULATOR") > 0;
                

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }

        }

        private void LoadInvoiceList(int order)
        {



            if (AppVars.listUserRights.Count(c => c.formName == "frmCompanyInvoiceList" && c.functionId == "PRINT INVOICE LIST") > 0)
            {
                if (grdLister.Columns.Contains("Check") == false)
                {
                    GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                    col.Width = 20;
                    col.AutoSizeMode = BestFitColumnMode.None;
                    col.HeaderText = "";
                    col.Name = "Check";
                    //col.IsPinned = true;
                    grdLister.Columns.Add(col);
                }
                btnPrintSelected.Visible = true;
            }
            else
            {
                btnPrintSelected.Visible = false;
            }



            if (order == 0)
            {
               
               

                    var query = from a in General.GetQueryable<Invoice>(c => (c.InvoiceTypeId == Enums.INVOICE_TYPE.ACCOUNT) && (c.CompanyId==CompanyId || CompanyId==0) )

                                orderby a.InvoiceDate descending

                                select new
                                {
                                    Id = a.Id,
                                    InvoiceNo = a.InvoiceNo,
                                    InvoiceDate = a.InvoiceDate,
                                    a.CompanyId,
                                    Company = a.Gen_Company.CompanyName,
                                    Address = a.Gen_Company.Address,
                                    Telephone = a.Gen_Company.TelephoneNo,
                                    BookedBy = a.AddLog,
                                    InvoiceTotal = a.InvoiceTotal
                                };


                    grdLister.DataSource = query.ToList();
               
            }
            else if (order == 1)
            {
                var query = (from a in General.GetQueryable<Invoice>(c => (c.InvoiceTypeId == Enums.INVOICE_TYPE.ACCOUNT) && (c.CompanyId == CompanyId || CompanyId == 0))
                           
                            select new
                            {
                                Id = a.Id,
                                InvoiceNo = a.InvoiceNo,
                                InvoiceDate = a.InvoiceDate,
                                a.CompanyId,
                                Company = a.Gen_Company.CompanyName,
                                Address = a.Gen_Company.Address,
                                Telephone = a.Gen_Company.TelephoneNo,
                                BookedBy = a.AddLog,
                                InvoiceTotal = a.InvoiceTotal
                            }).AsEnumerable().OrderBy(item => item.InvoiceNo, new NaturalSortComparer<string>());


                grdLister.DataSource = query.ToList();

            }
            else if (order == 2)
            {

                var query = (from a in General.GetQueryable<Invoice>(c => (c.InvoiceTypeId == Enums.INVOICE_TYPE.ACCOUNT) && (c.CompanyId == CompanyId || CompanyId == 0))
                         

                            select new
                            {
                                Id = a.Id,
                                InvoiceNo = a.InvoiceNo,
                                InvoiceDate = a.InvoiceDate,
                                a.CompanyId,
                                Company = a.Gen_Company.CompanyName,
                                Address = a.Gen_Company.Address,
                                Telephone = a.Gen_Company.TelephoneNo,
                                BookedBy = a.AddLog,
                                InvoiceTotal = a.InvoiceTotal
                              }).AsEnumerable().OrderByDescending(item => item.InvoiceNo, new NaturalSortComparer<string>());


                grdLister.DataSource = query.ToList();

            }
            
           

         
            grdLister.Columns["BookedBy"].HeaderText = "Booked by";


          

        }




        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {



                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Invoice ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {

                    int InvoiceId = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    invoice_Payment obj = General.GetObject<invoice_Payment>(c => c.invoiceId == InvoiceId);
                    if (obj == null)
                    {
                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();
                    }
                    else
                    {
                        ENUtils.ShowMessage("You Cannot Delete a Record Payment Exits..");
                    }
                    
                }
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "btnedit")
            {
                ViewDetailForm();


            }

            else if (gridCell.ColumnInfo.Name.ToLower() == "btninfo")
            {
                ViewInfo();


            }
        }

        private void ViewInfo()
        {
            try
            {

                GridViewRowInfo row = grdLister.CurrentRow;

                //   GridViewRowInfo row =grdLister.CurrentRow;

                if (row != null && row is GridViewDataRowInfo)
                {
                    int Id = row.Cells["Id"].Value.ToInt();
                    // int Id = row.Cells["Id"].Value.ToInt();
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







                            StringBuilder text = new StringBuilder();




                            string newLine = Environment.NewLine;

                            text.Append(" Invoice # : " + list[0].InvoiceNo.ToStr() + " @ " + string.Format("{0:dd/MM/yy HH:mm}", list[0].InvoiceDate));
                            text.Append(newLine + newLine);
                            text.Append(newLine + "Company : " + list[0].CompanyName.ToStr());
                            text.Append(newLine + "Address : " + list[0].CompanyAddress.ToStr());
                            text.Append(newLine + newLine);
                            text.Append(newLine + "Gross Total : " + Math.Round(netAmount.ToDecimal(), 2));

                            text.Append(newLine + newLine);


                            if (HasDiscount.ToStr() == "1")
                            {
                                text.Append( "<b>Discount " + string.Format("{0:##0.##}", DiscountPercent) + "%" + " : " + Math.Round(discountAmount, 2));
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


                            // RadOffice2007ScreenTipElement screenTip = new RadOffice2007ScreenTipElement();
                            //  screenTip.CaptionLabel.Margin = new Padding(3);

                            //  screenTip.CaptionLabel.Text = text.ToStr();
                            //   screenTip.CaptionLabel.Text = text.ToStr();
                            //   screenTip.MainTextLabel.Text = string.Empty;
                            //  screenTip.EnableCustomSize = false;


                            // e.ToolTipText = text.ToString();

                            frmCustomScreenInvoiceTip frmTip = new frmCustomScreenInvoiceTip(text.ToString());
                            frmTip.StartPosition = FormStartPosition.CenterParent;
                            frmTip.ShowInTaskbar = false;
                            frmTip.ShowDialog();
                            KeyEventArgs key = frmTip.LastSendEventArgs;

                            frmTip.Dispose();
                        }
                    }
                }

            }
            catch
            {


            }

        }

        void frmLocationList_Load(object sender, EventArgs e)
        {
        }

        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ViewDetailForm();
        }

        private void ViewDetailForm()
        {
            try
            {

              

                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    long id=grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                 //   Cursor.Position = new Point(Cursor.Position.X, 100);
                  //  System.Threading.Thread.Sleep(100);

               //     grdLister.ShowItemToolTips = false;
                  // 
                       General.ShowCompanyInvoiceForm(id);

                      

                       //if (screenTip != null)
                       //{

                         
                       //    screenTip.DisposeChildren();
                       //    screenTip.Dispose();
                       //    screenTip = null;
                       //  //  screenTip.Enabled = false;
                       //}
                }
                else
                {
                    ENUtils.ShowMessage("Please select a record");
                }
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

                    int InvoiceId = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                    invoice_Payment obj = General.GetObject<invoice_Payment>(c => c.invoiceId == InvoiceId);
                    if (obj == null)
                    {
                        objMaster.Delete(objMaster.Current);
                    }
                    else
                    {
                        ENUtils.ShowMessage("You Can not delete a record..");
                        return;
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

            LoadInvoiceList(0);

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
          //  txtSearch.Text = string.Empty;
            LoadInvoiceList(0);
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

        private void btnSageExport_Click(object sender, EventArgs e)
        {
            frmSageExport frm = new frmSageExport();
            frm.ShowDialog();


        }

        private void OPTDESC_CheckedChanged(object sender, EventArgs e)
        {
             int val= (sender as RadioButton).Tag.ToInt();
             LoadInvoiceList(val);
           
        }

        private void btnVatCalculator_Click(object sender, EventArgs e)
        {
            try
            {
                frmVatCalculator frm = new frmVatCalculator();
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                frm.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

       






    }
}

