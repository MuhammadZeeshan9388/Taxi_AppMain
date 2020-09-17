using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Telerik.WinControls.UI;
using Taxi_BLL;
using Taxi_Model;
using DAL;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Linq.Expressions;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmAddMultipleCompanyInvoice : UI.SetupBase
    {
        InvoiceBO objMaster = null;
        public struct COLS_DETAILS
        {
            public static string ID = "Id";
            public static string Company = "Company";
            public static string InvoiceId = "InvoiceId";
            public static string InvoiceNo = "InvoiceNo";

            public static string Email = "Email";
            
                  public static string SubCompanyId = "SubCompanyId";
            public static string TotalBooking = "TotalBooking";
        }





        public frmAddMultipleCompanyInvoice()
        {

            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(frmAddMultipleCompanyInvoice_FormClosed);

            grdCompany.AllowEditRow = false;

            ComboFunctions.FillCompanyGroupCombo(ddlCompanyGroup);
            this.grdCompany.KeyDown += new KeyEventHandler(grdCompany_KeyDown);
            this.grdCompany.MouseDown += new MouseEventHandler(grdCompany_MouseDown);
            this.Shown += new EventHandler(frmAddMultipleCompanyInvoice_Shown);
        }

        int maxScrollValue = 0;
        void frmAddMultipleCompanyInvoice_Shown(object sender, EventArgs e)
        {
            maxScrollValue = grdCompany.TableElement.VScrollBar.Value;
        }


        void grdCompany_KeyDown(object sender, KeyEventArgs e)
        {

            if (grdCompany.CurrentColumn == null || grdCompany.CurrentRow == null)
                return;

            try
            {
                if (grdCompany.CurrentRow.Cells[0].Value.ToBool())
                    grdCompany.CurrentRow.Cells[0].Value = false;
                else
                    grdCompany.CurrentRow.Cells[0].Value = true;



                if (grdCompany.CurrentRow != null && grdCompany.CurrentRow.Index >= (grdCompany.Rows.Count - 2))
                {
                    grdCompany.TableElement.VScrollBar.Value = maxScrollValue + 20;

                }
            }
            catch (Exception ex)
            {
            }
        }

        void grdCompany_MouseDown(object sender, MouseEventArgs e)
        {
            if (grdCompany.CurrentColumn == null || grdCompany.CurrentColumn is GridViewCheckBoxColumn == false)
                return;

            try
            {
                if (grdCompany.CurrentRow.Cells[0].Value.ToBool())
                    grdCompany.CurrentRow.Cells[0].Value = false;
                else
                    grdCompany.CurrentRow.Cells[0].Value = true;
            }
            catch (Exception ex)
            {
            }
        }

        void frmAddMultipleCompanyInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
            GC.Collect();
        }

        private void frmCompanyInvoice_Load(object sender, EventArgs e)
        {
            GetCompany();
        }
        public void GetCompany()
        {
            try
            {
                dtpFromDate.Enabled = false;
                dtpTillDate.Enabled = false;

                //dtpTillDate.Value = DateTime.Now;
                dtpInvoiceDate.Value = DateTime.Now;
                DateTime OneMonthAfter = DateTime.Now;
                OneMonthAfter = OneMonthAfter.AddMonths(+1);
                dtpDueDate.Value = OneMonthAfter.ToDate();


                GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                col.Width = 40;
                col.AutoSizeMode = BestFitColumnMode.None;
                col.HeaderText = "";
                col.Name = "Check";
                //  col.ReadOnly = true;
                grdCompany.Columns.Add(col);

                GridViewTextBoxColumn colCompany = new GridViewTextBoxColumn();
                colCompany.Width = 150;
                colCompany.AutoSizeMode = BestFitColumnMode.None;
                colCompany.HeaderText = "Account";
                colCompany.Name = "Company";
                colCompany.ReadOnly = true;
                grdCompany.Columns.Add(colCompany);

                GridViewTextBoxColumn colId = new GridViewTextBoxColumn();
                colId.Width = 40;
                colId.AutoSizeMode = BestFitColumnMode.None;
                colId.HeaderText = "Id";
                colId.Name = "Id";
                colId.ReadOnly = true;
                grdCompany.Columns.Add(colId);
                grdCompany.Columns["Id"].IsVisible = false;

                colId = new GridViewTextBoxColumn();
                colId.Width = 40;
                colId.AutoSizeMode = BestFitColumnMode.None;
                colId.HeaderText = "Email";
                colId.Name = "Email";
                colId.ReadOnly = true;
                grdCompany.Columns.Add(colId);
                grdCompany.Columns["Email"].IsVisible = false;


                colId = new GridViewTextBoxColumn();
                colId.Width = 40;
                colId.AutoSizeMode = BestFitColumnMode.None;
                colId.HeaderText = "InvoiceNo";
                colId.Name = "InvoiceNo";
                colId.IsVisible = false;
                colId.ReadOnly = true;
                grdCompany.Columns.Add(colId);




                GridViewTextBoxColumn colInvoiceId = new GridViewTextBoxColumn();
                colInvoiceId.Width = 40;
                colInvoiceId.AutoSizeMode = BestFitColumnMode.None;
                colInvoiceId.HeaderText = "InvoiceId";
                colInvoiceId.Name = "InvoiceId";
                colInvoiceId.ReadOnly = true;
                grdCompany.Columns.Add(colInvoiceId);
                grdCompany.Columns["InvoiceId"].IsVisible = false;

                GridViewTextBoxColumn colBooking = new GridViewTextBoxColumn();
                colBooking.Width = 130;
                colBooking.AutoSizeMode = BestFitColumnMode.None;
                colBooking.HeaderText = "Total Booking";
                colBooking.Name = "TotalBooking";
                colBooking.ReadOnly = true;
                grdCompany.Columns.Add(colBooking);



                GridViewTextBoxColumn colsubcompanyId = new GridViewTextBoxColumn();
                colsubcompanyId.Width = 40;
                colsubcompanyId.AutoSizeMode = BestFitColumnMode.None;
                colsubcompanyId.HeaderText = "subcompanyId";
                colsubcompanyId.Name =COLS_DETAILS.SubCompanyId;
                colsubcompanyId.ReadOnly = true;
                grdCompany.Columns.Add(colsubcompanyId);
                grdCompany.Columns[COLS_DETAILS.SubCompanyId].IsVisible = false;


                grdCompany.ShowRowHeaderColumn = false;







                ComboFunctions.FillSubCompanyCombo(ddlSubCompany);


                if (ddlSubCompany.Items.Count == 1)
                {
                    ddlSubCompany.SelectedIndex = 0;
                    ddlSubCompany.Enabled = false;

                }
                else
                {

                    ddlSubCompany.SelectedValue = AppVars.objSubCompany.Id;
                }

               

                GetCompanyList();

                ddlSubCompany.SelectedValueChanged += DdlSubCompany_SelectedValueChanged;


                //var query = General.GetQueryable<Gen_Company>(null).Where(c => c.IsClosed == false &&
                //     (c.Gen_Company_PaymentTypes.Count == 0 || c.Gen_Company_PaymentTypes.Any(a => a.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT)))
                //    .OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName }).ToList();





                //grdCompany.RowCount = query.Count;


                //for (int i = 0; i < query.Count; i++)
                //{


                //    grdCompany.Rows[i].Cells[COLS_DETAILS.ID].Value = query[i].Id;
                //    grdCompany.Rows[i].Cells[COLS_DETAILS.Company].Value = query[i].CompanyName;


                //}







            }
            catch (Exception ex)
            {


            }

        }

        private void DdlSubCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            //
            GetCompanyList();
        }

        private void GetCompanyList()
        {

            try
            {
                int subCompanyId = ddlSubCompany.SelectedValue.ToInt();
                int GroupId = ddlCompanyGroup.SelectedValue.ToInt();

                var query = General.GetQueryable<Gen_Company>(null).Where(c => c.IsClosed == false &&
                         (c.Gen_Company_PaymentTypes.Count == 0 || c.Gen_Company_PaymentTypes.Any(a => a.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT))
                         && (subCompanyId == 0 || c.SubCompanyId == subCompanyId)
                         && (GroupId == 0 || c.GroupId == GroupId))

                        .OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName,args.SubCompanyId }).ToList();


                // grdCompany.DataSource = query;

                //grdCompany.BeginUpdate();

                // grdCompany.BeginUpdate();


                grdCompany.RowCount = query.Count;


                for (int i = 0; i < query.Count; i++)
                {


                    grdCompany.Rows[i].Cells[COLS_DETAILS.ID].Value = query[i].Id;
                    grdCompany.Rows[i].Cells[COLS_DETAILS.Company].Value = query[i].CompanyName;
                    grdCompany.Rows[i].Cells[COLS_DETAILS.SubCompanyId].Value = query[i].SubCompanyId;
                    //detailRow.Cells[COLS_DETAILS.TotalBooking].Value = 0;

                }
            }
            catch
            {


            }

        }



        private void rbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = false;
            dtpTillDate.Enabled = false;



        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Generate();
        }
        private void Generate()
        {
            try
            {

                if (rbtnMonthly.Checked || rbtnCustom.Checked)
                {
                    if (dtpFromDate.Value == null || dtpTillDate.Value == null)
                    {

                        ENUtils.ShowMessage("Please enter From and Till Date");
                        return;
                    }


                }


                bool IsGenerated = false;

                foreach (var row in grdCompany.Rows.Where(c => c.Cells["Check"].Value.ToBool()))
                {
                    if (OnSave(row.Cells["Id"].Value.ToInt(), ref IsGenerated) == false)
                    {
                        break;
                    }

                }


                if (IsGenerated)
                {
                    ENUtils.ShowMessage("Invoice(s) generated successfully");
                    btnViewPrint.Enabled = true;

                }



            }
            catch (Exception ex)
            {



            }

        }


        private void rbtnMonthly_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = false;
            dtpTillDate.Enabled = false;


            if (rbtnMonthly.Checked)
            {
                SetDefaultCustomDates();

            }

        }

        private void SetDefaultCustomDates()
        {

            if (dtpFromDate.Value == null)
            {
                DateTime OneMonthBefore = DateTime.Now;
                OneMonthBefore = OneMonthBefore.AddMonths(-1);

                dtpFromDate.Value = OneMonthBefore.ToDate();
                dtpTillDate.Value = DateTime.Now.ToDate();
            }
        }

        private void rbtnCustom_CheckedChanged(object sender, EventArgs e)
        {

            if (rbtnCustom.Checked == true)
            {
                dtpFromDate.Enabled = true;
                dtpTillDate.Enabled = true;

                SetDefaultCustomDates();
            }


        }

        private void cbAllCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllCompany.Checked == true)
            {
                if (grdCompany.Rows.Count > 0)
                {
                    for (int i = 0; i < grdCompany.Rows.Count; i++)
                    {
                        grdCompany.Rows[i].Cells["Check"].Value = true;//..CurrentCell.Value;
                    }
                }
            }
            else if (cbAllCompany.Checked == false)
            {
                if (grdCompany.Rows.Count > 0)
                {
                    for (int i = 0; i < grdCompany.Rows.Count; i++)
                    {
                        grdCompany.Rows[i].Cells["Check"].Value = false;//..CurrentCell.Value;

                    }
                }
            }
        }
        private bool OnSave(int CompanyId, ref bool IsGenerated)
        {

            bool IsSaved = false;


            try
            {


                DateTime? fromDateValue = null;
                DateTime? tillDateValue = null;

               // List<Booking> list1 = null;

                Expression<Func<Booking, bool>> expPickBooking = null;




                if (rbtnAll.Checked)
                {

                    if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 0 || AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 1)
                    {
                        expPickBooking = c => c.CompanyId == CompanyId &&
                                     (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED );

                        //list1 = General.GetGeneralList<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                        //                                         && c.CompanyId == CompanyId


                        //                                         );
                    }
                    else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2)
                    {
                        expPickBooking = c => c.CompanyId == CompanyId &&
                                      (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP);
                                    
                        //list1 = General.GetGeneralList<Booking>(c => (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || (c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH && c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP))
                        //                                        && c.CompanyId == CompanyId


                        //                                        );

                    }
                    else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 3)
                    {

                        expPickBooking = c => c.CompanyId == CompanyId && c.PaymentTypeId==Enums.PAYMENT_TYPES.BANK_ACCOUNT
                                   &&  (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP);

                        //list1 = General.GetGeneralList<Booking>(c => (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                        //                                  && c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT




                        //                                  );


                    }
                }
                else if (rbtnMonthly.Checked)
                {
                    DateTime fromDate = DateTime.Now.AddDays(-30).ToDate();
                    DateTime tillDate = DateTime.Now.ToDate();

                    fromDateValue = fromDate;
                    tillDateValue = tillDate;

                    if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 0 || AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 1)
                    {
                        expPickBooking = c => c.CompanyId == CompanyId &&
                                       (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED )
                                      && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate);


                        //list1 = General.GetGeneralList<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                        //                                       && c.CompanyId == CompanyId
                        //                                       && (c.PickupDateTime.Value.Date >= fromDate && c.PickupDateTime.Value.Date <= tillDate)



                        //                                       );
                    }
                    else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2)
                    {
                        expPickBooking = c => c.CompanyId == CompanyId && 
                                        (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                       && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate);

                        //list1 = General.GetGeneralList<Booking>(c => (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                        //                                     && c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                        //                                     && (c.PickupDateTime.Value.Date >= fromDate && c.PickupDateTime.Value.Date <= tillDate)


                                                    //         );
                    }
                    else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 3)
                    {

                        expPickBooking = c => c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                                      && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                     && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate);

                        //list1 = General.GetGeneralList<Booking>(c => (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                        //                                  && c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                        //                                  && (c.PickupDateTime.Value.Date >= fromDate && c.PickupDateTime.Value.Date <= tillDate)



                        //                                  );


                    }


                }
                else if (rbtnCustom.Checked)
                {
                    dtpFromDate.Enabled = true;
                    dtpTillDate.Enabled = true;


                    DateTime? fromDates = dtpFromDate.Value.ToDate();
                    DateTime? tillDates = dtpTillDate.Value.ToDate() + new TimeSpan(23, 59, 59);


                    fromDateValue = fromDates;
                    tillDateValue = tillDates;

                    if (fromDates < tillDates)
                    {

                        if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 0 || AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 1)
                        {
                            expPickBooking = c => c.CompanyId == CompanyId &&
                                         (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED )
                                        && (c.PickupDateTime >= fromDates && c.PickupDateTime <= tillDates);


                            //list1 = General.GetGeneralList<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                            //                                       && c.CompanyId == CompanyId
                            //                                       && (c.PickupDateTime.Value.Date >= fromDates && c.PickupDateTime.Value.Date <= tillDates)



                            //                                       );

                        }
                        else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2)
                        {
                            expPickBooking = c => c.CompanyId == CompanyId &&
                                          (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                         && (c.PickupDateTime >= fromDates && c.PickupDateTime <= tillDates);
                                         
                           

                            //list1 = General.GetGeneralList<Booking>(c => (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                            //                                     && c.CompanyId == CompanyId
                            //                                     && (c.PickupDateTime.Value.Date >= fromDates && c.PickupDateTime.Value.Date <= tillDates)



                            //                                     );
                        }
                        else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 3)
                        {


                            expPickBooking = c => c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                                         &&   (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                        && (c.PickupDateTime >= fromDates && c.PickupDateTime <= tillDates);


                            //list1 = General.GetGeneralList<Booking>(c => (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                            //                                  && c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                            //                                  && (c.PickupDateTime.Value.Date >= fromDates && c.PickupDateTime.Value.Date <= tillDates)

                            //                                  );

                        }

                    }
                    else
                    {
                        MessageBox.Show("Till Date must be greater than From Date.");
                        return false;
                    }

                }


                //UM_Form_Template template = General.GetObject<UM_Form_Template>(c => c.FormId != null && c.UM_Form.FormName == "frmInvoiceReport" && c.IsDefault == true);


                //string templateName = "Template13";
                //if (template != null)
                //{

                //    templateName = template.TemplateName.ToStr().Trim();

                //}


            
             
               

                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        var listA = db.Bookings.Where(expPickBooking);
                        var list2 = db.Invoice_Charges;



                        var list = (from b in listA
                                    join c in list2 on b.Id equals c.BookingId into table2
                                    from c in table2.DefaultIfEmpty()
                                    where (c == null)
                                    select new
                                    {
                                        BookingId = b.Id,
                                        Charges = b.CompanyPrice,
                                        b.ParkingCharges,
                                        b.WaitingCharges,
                                        b.ExtraDropCharges,
                                        b.DepartmentId
                                    }).ToList();

                        //var list2 = General.GetGeneralList<Invoice_Charge>(c => c.InvoiceId != null);

                        //var list = (from b in list1
                        //            join c in list2 on b.Id equals c.BookingId into table2
                        //            from c in table2.DefaultIfEmpty()
                        //            where (c == null)
                        //            select new
                        //            {
                        //                BookingId = b.Id,
                        //                Charges = b.CompanyPrice.ToDecimal() + b.ParkingCharges.ToDecimal() + b.WaitingCharges.ToDecimal() + b.ExtraDropCharges.ToDecimal(),
                        //                b.DepartmentId
                        //            }).ToList();

                        if (list.Count > 0)
                        {



                            var deptList = list.Select(c => c.DepartmentId).Distinct().ToList<long?>();


                            GridViewRowInfo detailRow = grdCompany.Rows.FirstOrDefault(c => c.Cells[COLS_DETAILS.ID].Value.ToInt() == CompanyId);
                            foreach (var item in deptList)
                            {


                                var listDept = list.Where(c => c.DepartmentId == item).ToList();



                                objMaster = new InvoiceBO();
                                objMaster.New();

                                objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;

                                objMaster.Current.InvoiceDate = dtpInvoiceDate.Value.ToDate();
                                objMaster.Current.CompanyId = CompanyId;  //ddlCompany.SelectedValue.ToIntorNull();

                                objMaster.Current.DueDate = dtpDueDate.Value.ToDate();
                            objMaster.Current.Remarks = txtNotes.Text.Trim();

                                if (item != null)
                                {
                                    objMaster.Current.DepartmentId = item;
                                    objMaster.Current.DepartmentWise = true;

                                }
                                objMaster.Current.InvoiceTypeId = Enums.INVOICE_TYPE.ACCOUNT;
                                //New Columns

                                objMaster.Current.SubCompanyId = ddlSubCompany.SelectedValue.ToIntorNull();

                                if(objMaster.Current.SubCompanyId==null)
                                {
                                objMaster.Current.SubCompanyId = detailRow.Cells[COLS_DETAILS.SubCompanyId].Value.ToInt();
                                }

                                objMaster.Current.InvoiceTotal = listDept.Sum(callnotification => callnotification.Charges.ToDecimal() + callnotification.ParkingCharges.ToDecimal() + callnotification.WaitingCharges.ToDecimal() + callnotification.ExtraDropCharges.ToDecimal());

                                objMaster.Current.FromDate = fromDateValue;
                                objMaster.Current.TillDate = tillDateValue;



                                string[] skipProperties = { "Invoice", "Booking" };
                                IList<Invoice_Charge> savedList = objMaster.Current.Invoice_Charges;
                                List<Invoice_Charge> listofDetail = (from r in listDept

                                                                     select new Invoice_Charge
                                                                     {


                                                                         BookingId = r.BookingId

                                                                     }).ToList();


                                Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

                                objMaster.Save();




                                detailRow.Cells[COLS_DETAILS.InvoiceId].Value += objMaster.Current.Id + ",";
                                detailRow.Cells[COLS_DETAILS.InvoiceNo].Value = objMaster.Current.InvoiceNo;


                            }



                            detailRow.Cells[COLS_DETAILS.Email].Value = objMaster.Current.Gen_Company.DefaultIfEmpty().Email.ToStr().Trim();
                            detailRow.Cells[COLS_DETAILS.TotalBooking].Value = list.Count;


                            IsGenerated = true;

                        }
                    }
                
              
            




                IsSaved = true;


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

            return IsSaved;

        }

        private void btnExits_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }


        public void ShowCompanyInvoiceForm(long id)
        {


            frmInvoice frm = new frmInvoice();
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ControlBox = true;
            frm.OnDisplayRecord(id);
            frm.ShowDialog();
            frm.Dispose();


        }

        private void grdCompany_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdCompany.CurrentRow != null && grdCompany.CurrentRow is GridViewDataRowInfo)
            {
                long id = grdCompany.CurrentRow.Cells["InvoiceId"].Value.ToLong();
                if (id > 0)
                {
                    ShowCompanyInvoiceForm(id);
                }

            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }

        private void btnEmailInvoices_Click(object sender, EventArgs e)
        {
            EmailInvoices();
        }


        private void EmailInvoices()
        {
            try
            {
                string subject = txtSubject.Text.Trim();

                if (string.IsNullOrEmpty(subject))
                {
                    ENUtils.ShowMessage("Required : Email Subject");
                    return;
                }

                var rows = grdCompany.Rows.Where(c =>c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Length>0).ToList();



                List<long> invoiceIds = rows.Where(c=>c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Contains(",") == false).Select(c =>  c.Cells[COLS_DETAILS.InvoiceId].Value.ToLong()).ToList<long>();


                List<GridViewRowInfo> rowsDept = new List<GridViewRowInfo>();
                foreach (var item in rows.Where(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Contains(",")))
                {
                    foreach (var item2 in item.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Split(','))
                    {

                        if(item2.ToStr().IsNumeric())
                            invoiceIds.Add(item2.ToLong());

                    }
                }

                if (invoiceIds.Count > 0)
                {
                    frmInvoiceReport frm = new frmInvoiceReport();
                    frm.HasSplitByDept = false;

                    frm.ObjInvoice = objMaster.Current;

                    var list = General.GetQueryable<vu_Invoice>(a => invoiceIds.Contains(a.Id)).ToList();

                    frmEmail frmEmail = new frmEmail(null, "", "");

                    foreach (var item in rows)
                    {

                        string invoiceIdsX = item.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Trim();

                        foreach (var inv in invoiceIdsX.Split(','))
                        {

                            if (inv.ToStr().Trim().Length > 0 && inv.ToStr().Trim().IsNumeric())
                            {

                                frm.DataSource = list.Where(c => c.Id == inv.ToLong()).OrderBy(c => c.PickupDate).ToList();


                                frm.GenerateReport();


                                string invoiceNo = list.FirstOrDefault(c => c.Id == inv.ToLong()).DefaultIfEmpty().InvoiceNo.ToStr().Trim();

                              

                                frm.reportViewer1.Tag = "invoice";
                                frm.SendEmailInternally(frmEmail, subject, invoiceNo, item.Cells[COLS_DETAILS.Email].Value.ToStr().Trim());
                            }
                        }

                    }



                    ENUtils.ShowMessage("Email has been sent successfully");

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }





        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportExcel();
        }


        private void ExportExcel()
        {
            try
            {


                var rows = grdCompany.Rows.Where(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Length > 0).ToList();



                List<long> invoiceIds = rows.Where(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Contains(",") == false).Select(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToLong()).ToList<long>();



                foreach (var item in rows.Where(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Contains(",")))
                {
                    foreach (var item2 in item.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Split(','))
                    {

                        if (item2.ToStr().IsNumeric())
                            invoiceIds.Add(item2.ToLong());

                    }
                }




                List<GridViewRowInfo> rowsDept = new List<GridViewRowInfo>();
            



                if (invoiceIds.Count > 0)
                {
                    frmInvoiceReport frm = new frmInvoiceReport();
                    frm.HasSplitByDept = false;

                    frm.ObjInvoice = objMaster.Current;

                    var list = General.GetQueryable<vu_Invoice>(a => invoiceIds.Contains(a.Id)).ToList();



                    if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
                    {



                        foreach (var item in rows)
                        {

                            string invoiceIdsX = item.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Trim();

                            foreach (var inv in invoiceIdsX.Split(','))
                            {

                                if (inv.ToStr().Trim().Length > 0 && inv.ToStr().Trim().IsNumeric())
                                {

                                    frm.DataSource = list.Where(c => c.Id == inv.ToLong()).OrderBy(c => c.PickupDate).ToList();


                                    frm.GenerateReport();

                                    string invoiceNo = list.FirstOrDefault(c => c.Id == inv.ToLong()).DefaultIfEmpty().InvoiceNo.ToStr().Trim();

                                    Warning[] warnings;
                                    string[] streamids;
                                    string mimeType;
                                    string encoding;
                                    string extension;

                                    byte[] bytes = frm.reportViewer1.LocalReport.Render(
                                     "Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);



                                    string path = folderBrowserDialog1.SelectedPath.ToStr().Trim();
                                    if (path == string.Empty)
                                    {
                                        ENUtils.ShowMessage("Please check your shared network path before sending email.");
                                        return;
                                    }


                                    path += "\\" + invoiceNo + "." + "xls";

                                    FileInfo file = new FileInfo(path);

                                    using (FileStream fs = file.Create())
                                    {

                                        fs.Write(bytes, 0, bytes.Length);
                                        fs.Flush();
                                        fs.Close();
                                        fs.Dispose();

                                        //List<Attachment> myAttach = new List<Attachment>();
                                        //myAttach.Add(new System.Net.Mail.Attachment(file.FullName));

                                        //Taxi_AppMain.Email.Send(subject, messageBody, fromEmail, toEmail, myAttach);

                                        //      ENUtils.ShowMessage("Email has been Sent Successfully");

                                        //  myAttach[0].Dispose();

                                        //  File.Delete(path);
                                        //  this.Close();
                                    }
                                }
                            }


                        }



                        ENUtils.ShowMessage("Export successfully");
                    }

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void chkAllGroup_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                ddlCompanyGroup.SelectedValue = null;
                ddlCompanyGroup.Enabled = false;
            }
            else
            {
                ddlCompanyGroup.Enabled = true;
            }
        }

        private void ddlCompanyGroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {


                GetCompanyList();


                //int subCompanyId = ddlSubCompany.SelectedValue.ToInt();
                //int GroupId = ddlCompanyGroup.SelectedValue.ToInt();

                //var query = General.GetQueryable<Gen_Company>(null).Where(c => c.IsClosed == false &&
                //         (c.Gen_Company_PaymentTypes.Count == 0 || c.Gen_Company_PaymentTypes.Any(a => a.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT))
                //         && (subCompanyId == 0 || c.SubCompanyId == subCompanyId)
                //         && (GroupId == 0 || c.GroupId == GroupId))
                //        .OrderBy(c => c.CompanyName).Select(args => new { args.Id, args.CompanyName, args.SubCompanyId }).ToList();


                //grdCompany.RowCount = query.Count;


                //for (int i = 0; i < query.Count; i++)
                //{


                //    grdCompany.Rows[i].Cells[COLS_DETAILS.ID].Value = query[i].Id;
                //    grdCompany.Rows[i].Cells[COLS_DETAILS.Company].Value = query[i].CompanyName;
                //    grdCompany.Rows[i].Cells[COLS_DETAILS.SubCompanyId].Value = query[i].SubCompanyId;
                //    grdCompany.Rows[i].Cells[COLS_DETAILS.TotalBooking].Value = string.Empty;

                //}
            }
            catch
            {


            }
        }

        BackgroundWorker worker = null;

        BackgroundWorker UpdateCurrentworker = null;

        private void InitializeWorker()
        {
            if (worker == null)
            {
                worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                worker.WorkerReportsProgress = true;
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            }



        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DateTime? fromDateValue = null;
                DateTime? tillDateValue = null;
                Expression<Func<Booking, bool>> expPickBooking = null;

            

                if (rbtnMonthly.Checked || rbtnCustom.Checked)
                {
                    if (dtpFromDate.Value == null || dtpTillDate.Value == null)
                    {

                        ENUtils.ShowMessage("Please enter From and Till Date");
                        return;
                    }


                }
                
                int Row = 0;
                int count = grdCompany.Rows.Count;
             

                bool IsGenerated = false;
                int CompanyId = 0;
                foreach (var row in grdCompany.Rows)
                {
                   
                   CompanyId = row.Cells["Id"].Value.ToInt();

                if (rbtnAll.Checked)
                {

                    if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 0 || AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 1)
                    {
                        expPickBooking = c => c.CompanyId == CompanyId &&
                                     (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);

                        
                    }
                    else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2)
                    {
                        expPickBooking = c => c.CompanyId == CompanyId &&
                                      (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP);

                        

                    }
                    else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 3)
                    {

                        expPickBooking = c => c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                                   && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP);                     

                    }
                }
                else if (rbtnMonthly.Checked)
                {
                    DateTime fromDate = DateTime.Now.AddDays(-30).ToDate();
                    DateTime tillDate = DateTime.Now.ToDate();

                    fromDateValue = fromDate;
                    tillDateValue = tillDate;

                    if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 0 || AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 1)
                    {
                        expPickBooking = c => c.CompanyId == CompanyId &&
                                       (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
                                      && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate);


                      
                    }
                    else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2)
                    {
                        expPickBooking = c => c.CompanyId == CompanyId &&
                                        (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                       && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate);

                      
                    }
                    else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 3)
                    {

                        expPickBooking = c => c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                                      && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                     && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate);

                      
                    }
                    
                }
                else if (rbtnCustom.Checked)
                {
                    dtpFromDate.Enabled = true;
                    dtpTillDate.Enabled = true;


                    DateTime? fromDates = dtpFromDate.Value.ToDate();
                    DateTime? tillDates = dtpTillDate.Value.ToDate() + new TimeSpan(23, 59, 59);


                    fromDateValue = fromDates;
                    tillDateValue = tillDates;

                    if (fromDates < tillDates)
                    {

                        if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 0 || AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 1)
                        {
                            expPickBooking = c => c.CompanyId == CompanyId &&
                                         (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
                                        && (c.PickupDateTime >= fromDates && c.PickupDateTime <= tillDates);


                          
                        }
                        else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2)
                        {
                            expPickBooking = c => c.CompanyId == CompanyId &&
                                          (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                         && (c.PickupDateTime >= fromDates && c.PickupDateTime <= tillDates);
                           
                        }
                        else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 3)
                        {


                            expPickBooking = c => c.CompanyId == CompanyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT
                                         && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                        && (c.PickupDateTime >= fromDates && c.PickupDateTime <= tillDates);

                           
                        }

                    }
                    else
                    {
                        MessageBox.Show("Till Date must be greater than From Date.");
                        return ;
                    }

                }


                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        var listA = db.Bookings.Where(expPickBooking);
                        var list2 = db.Invoice_Charges;


                        var list = (from b in listA
                                    join c in list2 on b.Id equals c.BookingId into table2
                                    from c in table2.DefaultIfEmpty()
                                    where (c == null)
                                    select new
                                    {
                                        BookingId = b.Id,
                                        Charges = b.CompanyPrice,
                                        b.ParkingCharges,
                                        b.WaitingCharges,
                                        b.ExtraDropCharges,
                                        b.DepartmentId
                                    }).ToList();

                                          

                            GridViewRowInfo detailRow = grdCompany.Rows.FirstOrDefault(c => c.Cells[COLS_DETAILS.ID].Value.ToInt() == CompanyId);

                            detailRow.Cells[COLS_DETAILS.TotalBooking].Value = list.Count;
                           
                            IsGenerated = true;
                        
                        if (Row == 0)
                        {
                            if (lblCountCompany.InvokeRequired)
                            {
                                lblCountCompany.Invoke(new MethodInvoker(delegate { lblCountCompany.Text =  "0 out of " + count.ToString() + " Accounts"; }));
                            }
                        }
                        else
                        {
                            if (lblCountCompany.InvokeRequired)
                            {
                                lblCountCompany.Invoke(new MethodInvoker(delegate { lblCountCompany.Text = Row + " out of " + count.ToString() + " Accounts";  }));
                            }
                        }
                        Row += 1;

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //CurrentRow cr = e.UserState as CurrentRow;
            //if (cr != null)
            //{
            //    lblUpdate.Text = "Updating (" + cr.UpdateValue + ") " + (cr.index) + " out of " + cr.Total + "";
            //}
        }


        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grdCompany.Enabled = true;
            btnEmailInvoices.Enabled = true;
            btnExits.Enabled = true;
            btnGetBooking.Enabled = true;
            btnExport.Enabled = true;
            btnGenerate.Enabled = true;
            lblCountCompany.Visible = false;
        
        }

        private void btnGetBooking_Click(object sender, EventArgs e)
        {
            try
            {
                lblCountCompany.Visible = true;
                InitializeWorker();

                grdCompany.Enabled = false;
                btnEmailInvoices.Enabled = false;
                btnExits.Enabled = false;
                btnGetBooking.Enabled = false;
                btnExport.Enabled = false;
                btnGenerate.Enabled = false;
                worker.RunWorkerAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnViewPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string subject = txtSubject.Text.Trim();

                if (string.IsNullOrEmpty(subject))
                {
                    ENUtils.ShowMessage("Required : Email Subject");
                    return;
                }

                var rows = grdCompany.Rows.Where(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Length > 0).ToList();



                List<long> invoiceIds = rows.Where(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Contains(",") == false).Select(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToLong()).ToList<long>();


                List<GridViewRowInfo> rowsDept = new List<GridViewRowInfo>();
                foreach (var item in rows.Where(c => c.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Contains(",")))
                {
                    foreach (var item2 in item.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Split(','))
                    {

                        if (item2.ToStr().IsNumeric())
                            invoiceIds.Add(item2.ToLong());

                    }
                }

                if (invoiceIds.Count > 0)
                {
                    //frmInvoiceReport frm = new frmInvoiceReport();
                    //frm.HasSplitByDept = false;

                    //frm.ObjInvoice = objMaster.Current;

                   var list = General.GetQueryable<vu_Invoice>(a => invoiceIds.Contains(a.Id)).ToList();

                  //  frmEmail frmEmail = new frmEmail(null, "", "");

                    List<ClsInvoicePrintView> listofInvoices = new List<ClsInvoicePrintView>();

                    foreach (var item in rows)
                    {

                        string invoiceIdsX = item.Cells[COLS_DETAILS.InvoiceId].Value.ToStr().Trim();

                        foreach (var inv in invoiceIdsX.Split(','))
                        {

                            if (inv.ToStr().Trim().Length > 0 && inv.ToStr().Trim().IsNumeric())
                            {


                                string invoiceNo = list.FirstOrDefault(c => c.Id == inv.ToLong()).DefaultIfEmpty().InvoiceNo.ToStr().Trim();
                                string accountName = list.FirstOrDefault(c => c.Id == inv.ToLong()).DefaultIfEmpty().CompanyName.ToStr().Trim();
                             

                                listofInvoices.Add(new ClsInvoicePrintView { Id=inv.ToLong(), InvoiceNo= invoiceNo, Account= accountName, Email = item.Cells[COLS_DETAILS.Email].Value.ToStr().Trim() });

                                //frm.DataSource = list.Where(c => c.Id == inv.ToLong()).OrderBy(c => c.PickupDate).ToList();


                                //frm.GenerateReport();


                                //string invoiceNo = list.FirstOrDefault(c => c.Id == inv.ToLong()).DefaultIfEmpty().InvoiceNo.ToStr().Trim();



                                //frm.reportViewer1.Tag = "invoice";
                                //frm.SendEmailInternally(frmEmail, subject, invoiceNo, item.Cells[COLS_DETAILS.Email].Value.ToStr().Trim());
                            }
                        }

                    }



                    if (listofInvoices.Count > 0)
                    {
                        frmInvoiceReport frmInvoice = new frmInvoiceReport(listofInvoices, dtpFromDate.Value.ToDate(), dtpTillDate.Value.ToDate());
                        frmInvoice.ShowDialog();
                        frmInvoice.Dispose();
                    }



                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }



        }
    }





    public class ClsInvoicePrintView
    {

        public long Id;
        public string InvoiceNo;
        public string Account;
        public string Email;


    }
}
