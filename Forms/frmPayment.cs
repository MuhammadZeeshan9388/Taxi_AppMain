using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_BLL;
using Utils;
using Taxi_Model;
using DAL;
using UI;
using Telerik.WinControls.UI;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Taxi_AppMain.Classes;
using Telerik.WinControls.Enumerations;
using Telerik.WinControls;
using System.Collections;

namespace Taxi_AppMain
{
    public partial class frmPayment : UI.SetupBase
    {
        int InvoiceId = 0;
        bool IsCompanyLoaded = false;
        
        public frmPayment()
        {
            InitializeComponent();
            dtpPaymentDate.Value = DateTime.Now;
            FormatePaymenttGride();
            FillCombo();
            
            grdPayment.CellDoubleClick += new GridViewCellEventHandler(grdPayment_CellDoubleClick);
            grdPayment.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            ddlCompany.SelectedValueChanged += DdlCompany_SelectedValueChanged;
            numPayment.SpinElement.TextChanging += SpinElement_TextChanging; ;
            this.Load += FrmPayment_Load;
        }

        private void SpinElement_TextChanging(object sender, TextChangingEventArgs e)
        {
            if (e.NewValue != "" && e.NewValue != "-")
            {

                if (e.NewValue.ToStr().Trim().Length > 0)
                {

                    txtBalance.Value =( txtInvoiceTotal.Text.ToDecimal()- txtCreditNote.Text.ToDecimal()) - (e.NewValue.ToDecimal()+ grdPayment.Rows.Sum(c => c.Cells[COL_PAYMENT.PAYMENT].Value.ToDecimal()));
                    
                }

            }
            else
            {
                txtBalance.Value = (txtInvoiceTotal.Text.ToDecimal() - txtCreditNote.Text.ToDecimal()) - grdPayment.Rows.Sum(c => c.Cells[COL_PAYMENT.PAYMENT].Value.ToDecimal());
                
            }
        }

        public frmPayment(int CompanyId, int InvoiceId)
        {
            InitializeComponent();
            dtpPaymentDate.Value = DateTime.Now;
            FormatePaymenttGride();
            FillCombo();

            grdPayment.CellDoubleClick += new GridViewCellEventHandler(grdPayment_CellDoubleClick);
            grdPayment.CommandCellClick += new CommandCellClickEventHandler(grid_CommandCellClick);
            ddlCompany.SelectedValueChanged += DdlCompany_SelectedValueChanged;
            numPayment.SpinElement.TextChanging += SpinElement_TextChanging; ;
            this.Load += FrmPayment_Load;
            ddlCompany.SelectedValue = CompanyId;
            ddlCompany.Enabled = false;
            this.InvoiceId = InvoiceId;
        }


        private void FrmPayment_Load(object sender, EventArgs e)
        {
            IsCompanyLoaded = true;
            if (InvoiceId > 0)
            {
                Display();
            }
        }

        private void DdlCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (IsCompanyLoaded == false) return;

            Display();
        }

        private void Display()
        {
            try
            {
                int Id = ddlCompany.SelectedValue.ToInt();
                grdPayment.RowCount = 0;
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var item = (from a in db.Invoices.Where(c => (c.CompanyId == Id) && (InvoiceId == 0 || c.Id == InvoiceId))
                                orderby a.Id descending
                                select new
                                {
                                    Id = a.Id,
                                    a.InvoiceTotal,
                                    a.TotalInvoiceAmount,
                                    a.InvoiceNo,
                                    a.Gen_Company.HasVat,
                                    a.CurrentBalance,
                                    a.Gen_Company.AdminFees,
                                    a.Gen_Company.DiscountPercentage
                                }).FirstOrDefault();

                    var objCreditNote=  db.InvoiceCreditNotes.Where(c => c.InvoiceId == InvoiceId).Select(c=>new { c.Id, c.CreditNoteTotal }).FirstOrDefault();


                    if (item == null)
                    {
                        lblMessage.Visible = true;
                        grdPayment.Rows.Clear();
                        txtInvoiceno.Text = "";
                        txtBalance.Value = 0;
                        txtInvoiceTotal.Text = "0.00";
                        return;
                    }


                    lblMessage.Visible = false;
                    InvoiceId = item.Id.ToInt(); ;
                    txtInvoiceno.Text = item.InvoiceNo;

                    decimal creditNoteTotal = 0.00m;

                    decimal Total = item.InvoiceTotal.ToDecimal();
                    decimal adminFees = item.AdminFees.ToInt();
                    decimal discountAmount = 0.00m;

                    if(objCreditNote!=null)
                    {

                        creditNoteTotal= objCreditNote.CreditNoteTotal.ToDecimal();

                    }

                    if (item.DiscountPercentage.ToDecimal() > 0)
                    {
                        lblDiscount.Text = "Discount " + string.Format("{0:##0.##}", item.DiscountPercentage.ToDecimal()) + "%";
                        discountAmount = ((Total * item.DiscountPercentage) / 100).ToDecimal();

                        Total = Total - discountAmount;



                        if (creditNoteTotal > 0)
                        {
                            discountAmount = ((creditNoteTotal * item.DiscountPercentage) / 100).ToDecimal();
                            creditNoteTotal = creditNoteTotal - discountAmount;

                        }
                    }

                    if (item.HasVat.ToBool())
                    {
                        Total = (Total + ((Total * 20) / 100));

                        if (creditNoteTotal > 0)
                        {
                            creditNoteTotal = (creditNoteTotal + ((creditNoteTotal * 20) / 100));
                        }
                    }
                    else
                    {
                        Total = (Total + ((Total * adminFees) / 100));


                        if (creditNoteTotal > 0)
                        {
                            creditNoteTotal = (creditNoteTotal + ((creditNoteTotal * adminFees) / 100));
                        }


                    }

                  
               

                    txtInvoiceTotal.Text = Total.ToStr();

                    if (objCreditNote != null)
                    {
                        lblCreditNote.Visible = true;
                        txtCreditNote.Visible = true;
                        txtCreditNote.Text = creditNoteTotal.ToStr();



                    }
                    else
                        txtCreditNote.Text = creditNoteTotal.ToStr();

                    var data1 = db.invoice_Payments.Where(c => c.invoiceId == item.Id).OrderBy(c => c.id);



                    var query = (from a in data1
                                 select new
                                 {
                                     Id = a.id,
                                     invoiceID = a.invoiceId,
                                     Payment = a.InvoicePayment,
                                     Balance = a.Balance,
                                     Date = a.PaymentDate,
                                     a.AddBy,
                                     a.AddOn
                                 }).AsQueryable();


                    if (query.Count() > 0)
                    {
                        DataTable dt = query.ToDataTable();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            GridViewRowInfo row = null;

                            if (grdPayment.CurrentRow != null)
                            {
                                row = grdPayment.CurrentRow;
                            }

                            else
                            {
                                row = grdPayment.Rows.AddNew();

                            }
                            row.Cells[COL_PAYMENT.PAYMENT].Value = dt.Rows[i]["Payment"].ToDecimal();
                            row.Cells[COL_PAYMENT.BALANCE].Value = dt.Rows[i]["Balance"].ToDecimal();
                            row.Cells[COL_PAYMENT.DATE].Value = dt.Rows[i]["Date"].ToDateTime();
                            row.Cells[COL_PAYMENT.AddOn].Value = dt.Rows[i]["AddOn"].ToDateTime();
                            row.Cells[COL_PAYMENT.AddBy].Value = dt.Rows[i]["AddBy"];
                            row.Cells[COL_PAYMENT.ID].Value = dt.Rows[i]["Id"].ToInt();
                            row.Cells[COL_PAYMENT.MASTERID].Value = InvoiceId;
                            grdPayment.CurrentRow = null;


                        }

                    }



                    if (Total > 0 && grdPayment.Rows.Count == 0)
                    {



                        txtBalance.Value = Total-creditNoteTotal;
                    }
                    else
                    {
                        txtBalance.Value = item.CurrentBalance.ToDecimal();
                    }
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void FillCombo()
        {
            ComboFunctions.FillCompanyCombo(ddlCompany);
        }

        InvoiceBO objMaster = new InvoiceBO();
        void DisplayRecords()
        {
            objMaster.GetByPrimaryKey(InvoiceId);


            var data1 = objMaster.Current.invoice_Payments.OrderBy(c => c.id);
            var query = (from a in data1
                         select new
                         {
                             Id = a.id,
                             invoiceID = a.invoiceId,
                             Payment = a.InvoicePayment,
                             Balance = a.Balance,
                             Date = a.PaymentDate,
                             a.AddBy,
                             a.AddOn
                         }).AsQueryable();
            if (query.Count() > 0)
            {
                DataTable dt = query.ToDataTable();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    GridViewRowInfo row = null;

                    if (grdPayment.CurrentRow != null)
                    {
                        row = grdPayment.CurrentRow;
                    }

                    else
                    {
                        row = grdPayment.Rows.AddNew();

                    }
                    row.Cells[COL_PAYMENT.PAYMENT].Value = dt.Rows[i]["Payment"].ToDecimal();
                    row.Cells[COL_PAYMENT.BALANCE].Value = dt.Rows[i]["Balance"].ToDecimal();
                    row.Cells[COL_PAYMENT.DATE].Value = dt.Rows[i]["Date"].ToDateTime();
                    row.Cells[COL_PAYMENT.AddOn].Value = dt.Rows[i]["AddOn"].ToDateTime();
                    row.Cells[COL_PAYMENT.AddBy].Value = dt.Rows[i]["AddBy"];
                    row.Cells[COL_PAYMENT.ID].Value = dt.Rows[i]["Id"].ToInt();
                    row.Cells[COL_PAYMENT.MASTERID].Value = InvoiceId;
                    grdPayment.CurrentRow = null;

                    decimal Total = dt.Rows[i]["Balance"].ToDecimal();
                    txtInvoiceTotal.Text = Total.ToStr();
                    txtBalance.Text = Total.ToStr();
                }

            }
        }
        public struct COL_PAYMENT
        {
            public static string ID = "ID";
            public static string MASTERID = "MASTERID";

            public static string PAYMENT = "PAYMENT";
            public static string BALANCE = "BALANCE";
            public static string DATE = "DATE";
            public static string AddOn = "AddOn";
            public static string AddBy = "AddBy";

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddPayment();
            //if (grdPayment.Rows.Count > 0)
            //{
            //    //Save();
            //}
        }

        private void AddPayment()
        {

            try
            {
                decimal Payment = numPayment.Value.ToDecimal();
                decimal Balance = txtBalance.Value.ToDecimal();
                DateTime? dtPayment = dtpPaymentDate.Value.ToDateorNull();

                string error = string.Empty;

                if (Payment == 0)
                {
                    error += "Required : Payment";
                }
                if (dtPayment == null)
                {
                    if (string.IsNullOrEmpty(error))
                    {
                        error += "Required : Payment Date";
                    }
                    else
                    {
                        error += Environment.NewLine + "Required : Payment Date";
                    }
                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;
                }


                GridViewRowInfo row = null;
                row = grdPayment.Rows.AddNew();
              
                row.Cells[COL_PAYMENT.PAYMENT].Value = Payment;
                row.Cells[COL_PAYMENT.BALANCE].Value = Balance;
                row.Cells[COL_PAYMENT.DATE].Value = dtPayment;
                row.Cells[COL_PAYMENT.AddBy].Value = AppVars.LoginObj.UserName;

                row.Cells[COL_PAYMENT.AddOn].Value = DateTime.Now.ToDateTime();

              
                grdPayment.CurrentRow = null;

                Save();

                numPayment.Value = 0;
            }
            catch (Exception ex)
            {

            }
        }
        private void FormatePaymenttGride()
        {
            grdPayment.AllowAutoSizeColumns = true;
            grdPayment.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdPayment.AllowAddNewRow = false;
            grdPayment.ShowGroupPanel = false;
            grdPayment.AutoCellFormatting = true;
            grdPayment.ShowRowHeaderColumn = false;


            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_PAYMENT.ID;
            grdPayment.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COL_PAYMENT.MASTERID;
            grdPayment.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Add By";
            col.IsVisible = true;
            col.Name = COL_PAYMENT.AddBy;
            col.Width = 150;
            grdPayment.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Payment";
            col.IsVisible = true;
            col.Name = COL_PAYMENT.PAYMENT;
            col.Width = 150;
            grdPayment.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Balance";
            col.IsVisible = true;
            col.Name = COL_PAYMENT.BALANCE;
            col.Width = 150;
            grdPayment.Columns.Add(col);

            GridViewDateTimeColumn dcol = new GridViewDateTimeColumn();

            dcol.HeaderText = "Payment Date";
            dcol.IsVisible = true;
            dcol.Name = COL_PAYMENT.DATE;
            dcol.CustomFormat = "{0:dd/MM/yyyy}";
            dcol.FormatString = "{0:dd/MM/yyyy}";
            dcol.Width = 150;
            grdPayment.Columns.Add(dcol);

            dcol = new GridViewDateTimeColumn();
            dcol.HeaderText = "Date";
            dcol.IsVisible = false;
            dcol.Name = COL_PAYMENT.AddOn;
            dcol.Width = 150;
            grdPayment.Columns.Add(dcol);

            grdPayment.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


            grdPayment.AddDeleteColumn();
            grdPayment.Columns["btnDelete"].Width = 70;

            if (AppVars.listUserRights.Count(c => c.formName == "frmPayment" && c.functionId.ToLower() == "delete") == 0)
                grdPayment.Columns["btnDelete"].IsVisible = false;


            UI.GridFunctions.SetFilter(grdPayment);


        }

        private void grid_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Payment. ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    try
                    {

                    
                    RadGridView grid = gridCell.GridControl;

                    GridViewRowInfo row = grdPayment.CurrentRow;
                    int Id = row.Cells[COL_PAYMENT.ID].Value.ToInt();
                    decimal Payment = row.Cells[COL_PAYMENT.PAYMENT].Value.ToDecimal();
                    int InvoiceId = row.Cells[COL_PAYMENT.MASTERID].Value.ToInt();
                    if (Id > 0)
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            var query = db.invoice_Payments.FirstOrDefault(c => c.id == Id);
                            db.invoice_Payments.DeleteOnSubmit(query);
                            var query2 = db.Invoices.FirstOrDefault(c => c.Id == InvoiceId);
                            query2.CurrentBalance = (query2.CurrentBalance + Payment);
                            query2.PaidAmount = (query2.PaidAmount - Payment);


                                if (query2.CurrentBalance > 0)
                                    query2.InvoicePaymentTypeID = Enums.INVOICE_PAYMENTTYPES.CUSTOM;

                            db.SubmitChanges();
                        }


                        grid.CurrentRow.Delete();
                    }
                    }
                    catch (Exception ex)
                    {
                        ENUtils.ShowMessage(ex.Message);
                    }
                }
            }

        }
        private void btnExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void grdPayment_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            //if (e.Row is GridViewDataRowInfo)
            //{
            //    txtNotes.Text = e.Row.Cells[COL_NOTE.NOTES].Value.ToString();

            //}

            //if (e.Row is GridViewDataRowInfo)
            //{
            //    txtPayment.Text = e.Row.Cells[COL_PAYMENT.PAYMENT].Value.ToString();
            //}
        }
        public override void Save()
        {
            try
            {
                if (InvoiceId > 0)
                {
                    objMaster.GetByPrimaryKey(InvoiceId);
                }

                if (objMaster.PrimaryKeyValue != null)
                {
                    objMaster.Edit();

                    objMaster.CheckDataValidation = false;
                    objMaster.Current.CurrentBalance = txtBalance.Value.ToDecimal();
                   // objMaster.Current.OldBalance=
                    objMaster.Current.PaidAmount = grdPayment.Rows.Sum(c => c.Cells[COL_PAYMENT.PAYMENT].Value.ToDecimal());
                    string[] skipProperties = { "Invoice" };

                    IList<invoice_Payment> savedList3 = objMaster.Current.invoice_Payments;
                    List<invoice_Payment> listofDetail3 = (from r in grdPayment.Rows

                                                           select new invoice_Payment
                                                           {
                                                               id = r.Cells[COL_PAYMENT.ID].Value.ToInt(),
                                                               invoiceId = r.Cells[COL_PAYMENT.MASTERID].Value.ToInt(),
                                                               InvoicePayment = r.Cells[COL_PAYMENT.PAYMENT].Value.ToDecimal(),
                                                               Balance = r.Cells[COL_PAYMENT.BALANCE].Value.ToDecimal(),
                                                               PaymentDate = r.Cells[COL_PAYMENT.DATE].Value.ToDateTime(),
                                                               AddBy = r.Cells[COL_PAYMENT.AddBy].Value.ToStr(),
                                                               AddOn = r.Cells[COL_PAYMENT.AddOn].Value.ToDateTimeorNull(),
                                                           }).ToList();

                    Utils.General.SyncChildCollection(ref savedList3, ref listofDetail3, "id", skipProperties);

                    // if (txtBalance.Text.Contains("-") || txtBalance.Text == "0.00")
                    if (txtBalance.Value <= 0)
                    {
                        //objMaster.Current.InvoiceTotal = 0;
                        objMaster.Current.InvoicePaymentTypeID = Enums.INVOICE_PAYMENTTYPES.FULLPAID;
                        // Enums.INVOICE_PAYMENTTYPES.CUSTOM
                    }
                    else
                    {
                        //objMaster.Current.InvoiceTotal = txtBalance.Text.ToDecimal();
                        objMaster.Current.InvoicePaymentTypeID = Enums.INVOICE_PAYMENTTYPES.CUSTOM;
                    }
                    objMaster.Save();
                    Display();

                }
                //this.Close();

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

        
        protected override void OnClosed(EventArgs e)
        {
          
        }


    }
}
