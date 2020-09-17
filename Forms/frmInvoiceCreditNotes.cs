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
using System.Linq.Expressions;

namespace Taxi_AppMain
{
    public partial class frmInvoiceCreditNotes : UI.SetupBase
    {

        RadDropDownMenu menu_Job = null;

        private string companyEmail;


        private bool AutoInc = true;

        InvoiceBO objMaster = null;
        public struct COLS
        {
            public static string ID = "ID";
            public static string InvoiceId = "InvoiceId";
            public static string BookingId = "BookingId";

            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string VehicleID = "VehicleID";
            public static string OrderNo = "OrderNo";
            public static string PupilNo = "PupilNo";
            public static string BookedBy = "BookedBy";

            public static string RefNumber = "RefNumber";

            public static string Passenger = "Passenger";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

            public static string Charges = "Charges";
            public static string Tip = "Tip";
            public static string CreditNoteId = "CreditNoteId";
            public static string CreditAmount = "CreditAmount";
            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string RemovalDescription = "RemovalDescription";
            public static string Total = "Total";

            public static string Payment_ID = "Payment_ID";

            public static string PaymentTypeId = "PaymentTypeId";
            public static string BookingStatusId = "BookingStatusId";

        }
        public frmInvoiceCreditNotes()
        {
            InitializeComponent();
            InitializeConstructor();
            this.Load += new EventHandler(frmInvoiceCreditNotes_Load);
           
        }

        void frmInvoiceCreditNotes_Load(object sender, EventArgs e)
        {
            
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

                    if (menu_Job == null)
                    {
                        menu_Job = new RadDropDownMenu();                      

                        RadMenuItem viewJobItem1 = new RadMenuItem("View Job");
                        viewJobItem1.ForeColor = Color.DarkBlue;
                        viewJobItem1.BackColor = Color.Orange;
                        viewJobItem1.Font = new Font("Tahoma", 10, FontStyle.Bold);

                        viewJobItem1.Click += new EventHandler(viewJobItem1_Click);
                        menu_Job.Items.Add(viewJobItem1);             

                    }

                    e.ContextMenu = menu_Job;
                    return;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        void viewJobItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    General.ShowBookingForm(grdLister.CurrentRow.Cells[COLS.BookingId].Value.ToInt(), true, "", "", Enums.BOOKING_TYPES.LOCAL);

                }
            }
            catch 
            {
               // ENUtils.ShowMessage(ex.Message);

            }
        }

        private void InitializeConstructor()
        {


           var obj= General.GetObject<Gen_SysPolicy_DocumentNumberSetup>(c => c.DocumentId == Enums.GEN_DOCUMENTS.INVOICENO && (c.AutoIncrement == null || c.AutoIncrement == false));

           if (obj != null)
           {
               txtInvoiceNo.ReadOnly = false;
               txtInvoiceNo.Enabled = true;
               this.AutoInc = false;
           }


            ComboFunctions.FillCompanyForInvoiceCombo(ddlCompany);
          //  ComboFunctions.FillBookingTypeCombo(ddlBookingType);

       
            ComboFunctions.FillSubCompanyNameCombo(ddlSubCompany);


            if (ddlSubCompany.Items.Count == 1)
            {
                ddlSubCompany.SelectedIndex = 0;
                ddlSubCompany.Enabled = false;

            }
            else
            {
               

               ddlSubCompany.SelectedValue = AppVars.objSubCompany.Id;
            }

            ddlBookingType.SelectedValue = Enums.BOOKING_TYPES.LOCAL;
          

            dtpCreditDate.Value = DateTime.Now.ToDate();
            dtpDueDate.Value = DateTime.Now.ToDate().AddMonths(1);
            FormatChargesGrid();

            grdLister.ShowGroupPanel = false;
           // grdLister.AutoCellFormatting = true;
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            objMaster = new InvoiceBO();
            this.SetProperties((INavigation)objMaster);

            grdLister.AllowAddNewRow = false;


            //dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());

         //   grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            grdLister.ContextMenuOpening+=new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);

            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);


           

        }

        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Column.Name == COLS.VehicleID && e.CellElement is GridDataCellElement && e.CellElement.Text == "")
            {

                e.CellElement.Text = e.Row.Cells[COLS.Vehicle].Value.ToStr();


            }
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == "btnUpdate")
                {

                    GridViewRowInfo row = gridCell.RowInfo;

                    if (row is GridViewDataRowInfo)
                    {

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            var listnew = db.InvoiceCreditNotes.Where(c => c.InvoiceId == grdLister.Rows[0].Cells[COLS.InvoiceId].Value.ToLong()).FirstOrDefault();

                            int id = grdLister.CurrentRow.Cells["Id"].Value.ToInt();


                            if (grdLister.CurrentRow.Cells["Check"].Value.ToBool() == true)
                                {

                                    InvoiceCreditNotes_Charge objCreditNote = db.InvoiceCreditNotes_Charges.Where(c => c.InvoiceChargesId == id).FirstOrDefault();

                                    if (objCreditNote == null)
                                    {
                                        objCreditNote = new InvoiceCreditNotes_Charge();
                                    }

                                    objCreditNote.InvoiceChargesId = row.Cells[COLS.ID].Value.ToInt();
                                    objCreditNote.CreditAmount = row.Cells[COLS.CreditAmount].Value.ToDecimal();
                                    objCreditNote.CreditNotesId = listnew.Id;


                                    if (objCreditNote.Id == 0)
                                        db.InvoiceCreditNotes_Charges.InsertOnSubmit(objCreditNote);

                                    db.SubmitChanges();
                                }
                                else
                                {

                                    InvoiceCreditNotes_Charge objCreditNote = db.InvoiceCreditNotes_Charges.Where(c => c.InvoiceChargesId == id).FirstOrDefault();
                                    if (objCreditNote != null)
                                    {

                                        db.InvoiceCreditNotes_Charges.DeleteOnSubmit(objCreditNote);
                                        db.SubmitChanges();

                                    }
                                    else
                                    {
                                        ENUtils.ShowMessage("If you want to change credit Amount kindly check the record first!");
                                    }

                                }

                            
                        }


                        CalculateTotal();
                               
                                             

                            long index = grdLister.CurrentRow != null ? grdLister.CurrentRow.Cells["Id"].Value.ToLong() : -1;
                            int val = grdLister.TableElement.VScrollBar.Value;
                      

                            if (index > 0)
                                grdLister.CurrentRow = grdLister.Rows.FirstOrDefault(c => c.Cells["Id"].Value.ToLong() == index);

                            grdLister.TableElement.VScrollBar.Value = val;
                        }
                    }
                }
            
            catch
            {



            }
           
        }


        string TemplateName = "13";

        private void FormatChargesGrid()
        {

            try
            {

                GridViewTextBoxColumn col = new GridViewTextBoxColumn();


                col.IsVisible = false;
                col.Name = "Id";
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = "recordid";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COLS.CreditNoteId;
                grdLister.Columns.Add(col);

                GridViewCheckBoxColumn colc = new GridViewCheckBoxColumn();
                colc.Width = 20;
                colc.AutoSizeMode = BestFitColumnMode.None;
                colc.HeaderText = "";
                colc.Name = "Check";
                //col.IsPinned = true;
                grdLister.Columns.Add(colc);

                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COLS.InvoiceId;
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COLS.PaymentTypeId;
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = COLS.BookingId;
                grdLister.Columns.Add(col);

                GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
                colDt.Name = "PickupDate";
                colDt.ReadOnly = true;
                colDt.HeaderText = "Pickup Date-Time";
                colDt.SortOrder = RadSortOrder.Ascending;
                colDt.Sort(RadSortOrder.Ascending, true);
                grdLister.Columns.Add(colDt);



                col = new GridViewTextBoxColumn();
                // col.IsVisible = false;
                col.ReadOnly = true;
                col.HeaderText = "Job #";
                col.Name = "RefNumber";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = "Order No";
                col.ReadOnly = true;
                col.Name = "OrderNo";
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = "Booked By";
                col.ReadOnly = true;
                col.Name = COLS.BookedBy;
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = "Pupil No";
                col.ReadOnly = true;
                col.Name = "PupilNo";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = "Vehicle";
                col.ReadOnly = true;
                col.Name = "Vehicle";
                grdLister.Columns.Add(col);




                GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
                colCombo.Name = COLS.VehicleID;
              //  colCombo.IsVisible = false;
                colCombo.HeaderText = "Vehicle";
                colCombo.DataSource = General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, VehicleType = args.VehicleType }).ToList();
                colCombo.DisplayMember = "VehicleType";
                colCombo.ReadOnly = true;
                colCombo.ValueMember = "Id";
                colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
                colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;             
                grdLister.Columns.Add(colCombo);




                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = COLS.BookingStatusId;
                col.Name =COLS.BookingStatusId;
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.Name = COLS.Passenger;
                col.HeaderText = "Passenger";
                col.ReadOnly = true;
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Width = 900;
                col.IsVisible = false;
                col.ReadOnly = true;
                col.Name = COLS.RemovalDescription;
                col.ReadOnly = true;
                col.HeaderText = "Description";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Pickup Point";
                col.ReadOnly = true;
                col.Name = "PickupPoint";
                grdLister.Columns.Add(col);



                col = new GridViewTextBoxColumn();
                col.HeaderText = "Destination";
                col.Name = "Destination";
                col.ReadOnly = true;
                grdLister.Columns.Add(col);


                UM_Form_Template template = General.GetObject<UM_Form_Template>(c => c.FormId != null && c.UM_Form.FormName == "frmInvoiceReport" && c.IsDefault == true);

                if (template != null)
                {
                    TemplateName = template.TemplateName.ToStr().Trim();
                }


                GridViewDecimalColumn colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Fare";
                colD.Name = "Charges";
                colD.Maximum = 9999999;
                colD.ReadOnly = true;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);

                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Credit";
                colD.Name = COLS.CreditAmount;
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);

                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Parking";
                colD.Name = "Parking";
                colD.IsVisible = TemplateName != "Template13" && TemplateName != "Template24";
                colD.ReadOnly = true;
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);

                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = COLS.Tip;
                colD.ReadOnly = true;
                colD.Name = COLS.Tip;
                colD.Maximum = 1000;
                colD.IsVisible = TemplateName.ToLower() == "template27";
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Waiting";
                colD.ReadOnly = true;
                colD.Name = "Waiting";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.ReadOnly = true;
                colD.HeaderText = "Extra Drop";
                colD.Name = "ExtraDrop";
                colD.Maximum = 9999999;
                colD.IsVisible = false;
             //   colD.IsVisible = TemplateName != "Template13";


                // colD.IsVisible = !IsTemplate13;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);




               


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.ReadOnly = true;
                colD.HeaderText = "Total";
                colD.Name = "Total";
                colD.Maximum = 9999999;
                colD.Expression = "CreditAmount+Parking+Waiting+ExtraDrop";
                colD.FormatString = "{0:#,###0.00}";

                //if (TemplateName == "Template10" || TemplateName == "Template25" || TemplateName == "Template26" || TemplateName == "Template32")
                //{
                //    colD.Expression = "Charges+Parking+Waiting";
                //}
                //else  if ( TemplateName == "Template27")
                //{
                //    colD.Expression = "Charges+Parking+Waiting+Tip";
                //}

                //else if (TemplateName == "Template13" || (TemplateName == "Template24"  ))
                //{
                //    colD.Expression = "Charges+Waiting";

                    
                //}
                //else
                //{
                 //   colD.Expression = "Charges+Parking+Waiting+ExtraDrop";
                    grdLister.Columns["ExtraDrop"].IsVisible = true;
              //  }


                grdLister.Columns.Add(colD);


                GridViewComboBoxColumn colPayment = new GridViewComboBoxColumn();
                colPayment.IsVisible = false;
                colPayment.Name = COLS.Payment_ID;
                colPayment.HeaderText = "Status";
                colPayment.DataSource = General.GetQueryable<Invoice_PaymentType>(null).Where(c => c.Id == 1 || c.Id == 3).OrderBy(c => c.Id).ToList();
                colPayment.DisplayMember = "PaymentType";
                colPayment.ValueMember = "Id";
                colPayment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
                colPayment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                colPayment.ReadOnly = false;
                grdLister.Columns.Add(colPayment);


                (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
                (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


                grdLister.Columns["PickUpDate"].Width = 105;
                grdLister.Columns["RefNumber"].Width = 40;
                grdLister.Columns["Vehicle"].Width = 50;

                grdLister.Columns[COLS.VehicleID].Width = 60;

                grdLister.Columns[COLS.Passenger].Width = 60;
                grdLister.Columns["PickUpPoint"].Width = 80;
                grdLister.Columns["Destination"].Width = 80;

                grdLister.Columns["Charges"].Width = 50;
                grdLister.Columns["Parking"].Width = 45;
                grdLister.Columns["Waiting"].Width = 50;
                grdLister.Columns["ExtraDrop"].Width = 60;
              //  grdLister.Columns["MeetAndGreet"].Width = 50;
              // // grdLister.Columns["CongtionCharge"].Width = 60;
                grdLister.Columns["Total"].Width = 45;
                grdLister.Columns["OrderNo"].Width = 55;

                grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
                grdLister.Columns["RefNumber"].HeaderText = "Ref #";
                grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
                grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";

              //  grdLister.Columns["MeetAndGreet"].HeaderText = "M & G";
              //  grdLister.Columns["CongtionCharge"].HeaderText = "Congestion";
                grdLister.Columns["Payment_ID"].Width = 70;



                AddUpdateColumn(grdLister);


                ConditionalFormattingObject objPaid = new ConditionalFormattingObject();
                objPaid.ApplyToRow = true;
                objPaid.RowBackColor = Color.LightGreen;
                objPaid.ConditionType = ConditionTypes.Equal;
                objPaid.TValue1 = "6";
                objPaid.TValue2 = "6";
                grdLister.Columns["PaymentTypeId"].ConditionalFormattingObjectList.Add(objPaid);


                // NOPICKUP BOOKING COLOR
                ConditionalFormattingObject objNoPickup = new ConditionalFormattingObject();
                objNoPickup.ApplyToRow = true;
                objNoPickup.RowBackColor = Color.FromArgb(-32640);
                objNoPickup.ConditionType = ConditionTypes.Equal;
                objNoPickup.TValue1 = "13";
                objNoPickup.TValue2 = "13";
                grdLister.Columns[COLS.BookingStatusId].ConditionalFormattingObjectList.Add(objNoPickup);



                grdLister.CellBeginEdit += new GridViewCellCancelEventHandler(grdLister_CellBeginEdit);
                grdLister.MultiSelect = true;


                if (TemplateName.ToStr().ToLower().Trim() == "template15" || TemplateName.ToStr().ToLower().Trim() == "template10" || TemplateName.ToStr().ToLower().Trim() == "template32")
                {
                    ddlSplitBy.Visible = true;
                    lblSplitBy.Visible = true;
                    if (TemplateName.ToStr().ToLower().Trim() == "template10" || TemplateName.ToStr().ToLower().Trim() == "template32")
                    {
                        ddlSplitBy.Items.RemoveAt(2);
                    }

                }

            }
            catch 
            {

            }



        }


        List<Fleet_VehicleType> listofVehicles = null;

        void grdLister_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            try
            {

                if (e.Column is GridViewComboBoxColumn)
                {

                    if (listofVehicles == null)
                    {

                        listofVehicles = (List<Fleet_VehicleType>)(e.Column as GridViewComboBoxColumn).DataSource;
                    }


                    e.Row.Cells[COLS.VehicleID].Value = listofVehicles.FirstOrDefault(c => c.VehicleType.ToStr().ToUpper() == e.Row.Cells[COLS.Vehicle].Value.ToStr().ToUpper()).DefaultIfEmpty().Id;







                }
            }
            catch 
            {


            }
        }

        protected override void OnClosed(EventArgs e)
        {
            General.RefreshListWithoutSelected<frmCompanyInvoiceList>("frmCompanyInvoiceList1");

        }


        public override void Save()
        {

            OnSave();
        }


        private void OnSave()
        {

            try
            {
                int id = 0;
                if (this.AutoInc == false && txtInvoiceNo.Text.Trim() == string.Empty)
                {
                    ENUtils.ShowMessage("Required : Credit No");
                    return;

                }

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    InvoiceCreditNote objInvoiceCreditNote = null;

                 //   objInvoiceCreditNote.InvoiceCreditNotes_Charges

                    var list = db.InvoiceCreditNotes.Where(c => c.InvoiceId == grdLister.Rows[0].Cells[COLS.InvoiceId].Value.ToLong()).FirstOrDefault(); 

                    if (list != null)
	                {
                        objInvoiceCreditNote = db.InvoiceCreditNotes.FirstOrDefault(c => c.InvoiceId == grdLister.Rows[0].Cells[COLS.InvoiceId].Value.ToLong());
                        objInvoiceCreditNote.EditOn = DateTime.Now;
                        objInvoiceCreditNote.EditBy = AppVars.LoginObj.LuserId.ToIntorNull();
                        objInvoiceCreditNote.EditLog = AppVars.LoginObj.UserName.ToStr();
                        objInvoiceCreditNote.CreditNoteDate = dtpCreditDate.Value.ToDate();                      

	                }
                    else
                    {
                        objInvoiceCreditNote = new InvoiceCreditNote();
                        objInvoiceCreditNote.AddOn = DateTime.Now;
                        objInvoiceCreditNote.AddBy = AppVars.LoginObj.LuserId.ToIntorNull();
                        objInvoiceCreditNote.AddLog = AppVars.LoginObj.UserName.ToStr();

                        objInvoiceCreditNote.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;
                       
                        objInvoiceCreditNote.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                        objInvoiceCreditNote.DepartmentId = ddlDepartment.SelectedValue.ToLongorNull();
                        objInvoiceCreditNote.DepartmentWise = chkDepartmentWise.Checked;
                        objInvoiceCreditNote.CostCenterId = ddlCostCenter.SelectedValue.ToIntorNull();
                        objInvoiceCreditNote.CostCenterWise = chkCostCenterWise.Checked;
                        objInvoiceCreditNote.DueDate = dtpDueDate.Value.ToDate();
                        objInvoiceCreditNote.CreditNoteNo = txtInvoiceNo.Text.Trim();
                        objInvoiceCreditNote.CreditNoteTypeId = Enums.INVOICE_TYPE.ACCOUNT;
                    
                        objInvoiceCreditNote.SubCompanyId = ddlSubCompany.SelectedValue.ToIntorNull();
                        objInvoiceCreditNote.OrderNo = txtOrderNo.Text.Trim();
                        objInvoiceCreditNote.InvoiceId = grdLister.Rows[0].Cells[COLS.InvoiceId].Value.ToLong();
                      //  db.InvoiceCreditNotes.InsertOnSubmit(objInvoiceCreditNote);
                       
                    }


                    objInvoiceCreditNote.CreditNoteDate = dtpCreditDate.Value.ToDate();
                    objInvoiceCreditNote.CreditNoteTotal = grdLister.Rows.Where(c => c.Cells[COLS.PaymentTypeId].Value.ToInt() != 6 && c.Cells["Check"].Value.ToBool() == true).Sum(c => c.Cells[COLS.Total].Value.ToDecimal());




                    //  db.SubmitChanges();



                    string[] skipProperties = { "InvoiceCreditNote" };
                    IList<InvoiceCreditNotes_Charge> savedList = objInvoiceCreditNote.InvoiceCreditNotes_Charges;

                    List<InvoiceCreditNotes_Charge> listofDetail = (from r in grdLister.Rows.Where(c=>c.Cells["Check"].Value.ToBool() == true)

                                                         select new InvoiceCreditNotes_Charge
                                                         {
                                                              Id= r.Cells["recordid"].Value.ToLong(),
                                                              InvoiceChargesId = r.Cells[COLS.ID].Value.ToLong(),
                                                             CreditNotesId = objInvoiceCreditNote.Id,
                                                          
                                                             CreditAmount  =r.Cells[COLS.CreditAmount].Value.ToDecimal()
                                                         }).ToList();


                    Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);


                    if (objInvoiceCreditNote.Id == 0)
                        db.InvoiceCreditNotes.InsertOnSubmit(objInvoiceCreditNote);

                    db.SubmitChanges();

                  
                }


                DisplayRecord();


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
        bool Check = false;

        public override void DisplayRecord()
        {
           
            decimal CreditTotal = 0;
            if (objMaster.Current == null) return;

          

            ddlBookingType.SelectedValue = objMaster.Current.BookingTypeId;

            txtInvoiceNo.Text = objMaster.Current.InvoiceNo.ToStr() + "C";
            dtpCreditDate.Value = DateTime.Now.ToDate(); 





            if (objMaster.Current.CompanyId != null )
            {

                //var data = (List<Gen_Company>)ddlCompany.DataSource;
                //data.Add(objMaster.Current.Gen_Company);
                if( objMaster.Current.Gen_Company.IsClosed.ToBool() || ddlCompany.Items.Count(c=>c.Value.ToInt()== objMaster.Current.CompanyId.ToInt())==0)
                {
                   ComboFunctions.FillCompanyForInvoiceCombo(ddlCompany, objMaster.Current.CompanyId.ToInt());
                }
               
            }

            ddlCompany.SelectedValue = objMaster.Current.CompanyId;
            ddlCompany.Enabled = false;
            dtpDueDate.Value = objMaster.Current.DueDate.ToDate();
            ddlDepartment.SelectedValue = objMaster.Current.DepartmentId;
            chkDepartmentWise.Checked = objMaster.Current.DepartmentWise.ToBool();

            ddlCostCenter.SelectedValue=objMaster.Current.CostCenterId;
            chkCostCenterWise.Checked=objMaster.Current.CostCenterWise.ToBool();


            ddlSubCompany.SelectedValue = objMaster.Current.SubCompanyId;

            txtOrderNo.Text = objMaster.Current.OrderNo.ToStr().Trim();

            List<stp_GetInvoiceBookingsResult> list = null;

            using (TaxiDataContext db = new TaxiDataContext())
            {

                list = db.stp_GetInvoiceBookings(objMaster.Current.Id).ToList();



               
         

            grdLister.RowCount = list.Count;
            grdLister.BeginUpdate();

                for (int i = 0; i < list.Count; i++)
                {
                    grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                    grdLister.Rows[i].Cells[COLS.InvoiceId].Value = list[i].InvoiceId;
                    grdLister.Rows[i].Cells[COLS.BookingId].Value = list[i].BookingId;

                    grdLister.Rows[i].Cells[COLS.Payment_ID].Value = list[i].InvoicePaymentTypeId;
                    grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i].PickupDateTime;
                    grdLister.Rows[i].Cells[COLS.OrderNo].Value = list[i].OrderNo;
                    grdLister.Rows[i].Cells[COLS.PupilNo].Value = list[i].PupilNo;

                    grdLister.Rows[i].Cells[COLS.BookedBy].Value = list[i].DepartmentName.ToStr();

                    grdLister.Rows[i].Cells[COLS.VehicleID].Value = list[i].VehicleTypeId.ToInt();

                    grdLister.Rows[i].Cells[COLS.Vehicle].Value = list[i].VehicleType;

                    grdLister.Rows[i].Cells[COLS.RefNumber].Value = list[i].BookingNo;

                    grdLister.Rows[i].Cells[COLS.Charges].Value = list[i].CompanyPrice.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.CreditAmount].Value = list[i].CompanyPrice.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.Parking].Value = list[i].ParkingCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].FromAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].ToAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Waiting].Value = list[i].WaitingCharges.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = list[i].ExtraDropCharges.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.Tip].Value = list[i].TipAmount.ToDecimal();

                    //if (Check == false)
                        grdLister.Rows[i].Cells["Check"].Value = true;

                    //if (TemplateName == "Template13" || TemplateName == "Template24")
                    //{
                    //    grdLister.Rows[i].Cells[COLS.Total].Value = list[i].CompanyPrice.ToDecimal() + list[i].WaitingCharges.ToDecimal();

                    //}
                    //else if (TemplateName == "Template14")
                    //{
                //        grdLister.Rows[i].Cells[COLS.Total].Value = list[i].CompanyPrice.ToDecimal() + list[i].WaitingCharges.ToDecimal() + list[i].ParkingCharges.ToDecimal() + list[i].ExtraDropCharges.ToDecimal();
                    //}
                    //else
                    //{
                    //    grdLister.Rows[i].Cells[COLS.Total].Value = list[i].TotalCharges.ToDecimal();
                    //}

                    grdLister.Rows[i].Cells[COLS.Passenger].Value = list[i].CustomerName.ToStr().Trim();
                    grdLister.Rows[i].Cells[COLS.PaymentTypeId].Value = list[i].PaymentTypeId.ToInt();

                    grdLister.Rows[i].Cells[COLS.BookingStatusId].Value = list[i].BookingStatusId.ToInt();

                  




                } // end for loop





                grdLister.CurrentRow = null;

                grdLister.EndUpdate();
                grdLister.Refresh();
                grdLister.ReadOnly = false;
                grdLister.AllowEditRow = true;




                var listCreditNotes_Invoice = db.InvoiceCreditNotes.Where(x => x.InvoiceId == objMaster.Current.Id).FirstOrDefault();

                if (listCreditNotes_Invoice != null)
                {

                    btnExportExcel.Enabled = true;
                    btnExportPDF.Enabled = true;
                    btnPrint.Enabled = true;
                    btnSendEmail.Enabled = true;

                    dtpCreditDate.Value = listCreditNotes_Invoice.CreditNoteDate;

                    var listCreditNotes = listCreditNotes_Invoice.InvoiceCreditNotes_Charges.ToList();



                    if (listCreditNotes != null && listCreditNotes.Count > 0)
                    {


                        for (int i = 0; i < grdLister.Rows.Count; i++)
                        {
                            try
                            {
                                if (listCreditNotes.Count(c => c.InvoiceChargesId == grdLister.Rows[i].Cells["Id"].Value.ToInt()) > 0)
                                {

                                   var objRecord=     listCreditNotes.FirstOrDefault(c => c.InvoiceChargesId == grdLister.Rows[i].Cells["Id"].Value.ToInt());

                                    if (objRecord != null)
                                    {
                                        grdLister.Rows[i].Cells["recordid"].Value = objRecord.Id;
                                        grdLister.Rows[i].Cells[COLS.CreditAmount].Value = objRecord.CreditAmount.ToDecimal();
                                        grdLister.Rows[i].Cells[COLS.CreditNoteId].Value = objRecord.CreditNotesId;
                                   //     grdLister.Rows[i].Cells[COLS.Total].Value = grdLister.Rows[i].Cells[COLS.CreditAmount].Value.ToDecimal() + grdLister.Rows[i].Cells[COLS.Waiting].Value.ToDecimal() + grdLister.Rows[i].Cells[COLS.Parking].Value.ToDecimal() + grdLister.Rows[i].Cells[COLS.ExtraDrop].Value.ToDecimal();
                                        grdLister.Rows[i].Cells["Check"].Value = true;

                                    }

                                }
                                else
                                {
                                    grdLister.Rows[i].Cells["Check"].Value = false;

                                }


                            }
                            catch(Exception ex)
                            {

                            }

                        }

                        Check = true;
                    }

                }


            }




           // txtCreditAmount.Text = CreditTotal.ToStr();
            CalculateTotal();

            //ShowAutoContinue();
        }


       

        private void AddUpdateColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 50;
            
            col.Name = "btnUpdate";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Update";
            col.IsVisible = false;
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

    
     
      
        private List<object[]> ShowBookingMultiLister(Expression<Func<Booking, bool>> _condition, Expression<Func<Invoice_Charge, bool>> _invoiceCondition, string[] hiddenColumns, Func<Booking, bool> _condition2, string templateName)
        {

            List<object[]> listofObjects = null;

            Taxi_AppMain.frmLister frm = null;
            if (templateName == "Template13" || TemplateName == "Template24")
            {
                var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
                var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

                var list = (from b in list1
                            join c in list2 on b.Id equals c.BookingId into table2
                            from c in table2.DefaultIfEmpty()
                            where (c == null)
                            select new
                            {
                                Id = b.Id,


                                BookingDate = b.BookingDate,
                                PickupDate = b.PickupDateTime,

                                RefNo = b.BookingNo,
                                Vehicle = b.Fleet_VehicleType.VehicleType,
                                OrderNo = b.OrderNo,
                                PupilNo = b.PupilNo,
                                Passenger = b.CustomerName,
                                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                                //  Charges = b.FareRate.ToDecimal(),
                                Charges = b.CompanyPrice.ToDecimal(),
                                CompanyId = b.CompanyId,
                                CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                                Parking = b.ParkingCharges.ToDecimal(),
                                Waiting = b.WaitingCharges.ToDecimal(),
                                ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                                MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                                Congtion = b.CongtionCharges.ToDecimal(),
                                Description = "",
                                Total = (b.CompanyPrice.ToDecimal() + b.WaitingCharges.ToDecimal()),
                                //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                                BookedBy = b.BookedBy.ToStr(),
                                Fare = b.FareRate.ToDecimal(),

                                AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                                PaymentTypeId = b.PaymentTypeId,
                                BookingStatusId = b.BookingStatusId.ToIntorNull(),
                                PaymentType = b.Gen_PaymentType.PaymentType,
                                Tip=b.TipAmount.ToDecimal(),
                            }).ToList();


                frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


                frm.HiddenColumns = hiddenColumns;
                frm.ShowDialog();


            }
            else if (templateName == "Template14" || templateName == "Template23")
            {
                var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
                var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

                var list = (from b in list1
                            join c in list2 on b.Id equals c.BookingId into table2
                            from c in table2.DefaultIfEmpty()
                            where (c == null)
                            select new
                            {
                                Id = b.Id,


                                BookingDate = b.BookingDate,
                                PickupDate = b.PickupDateTime,

                                RefNo = b.BookingNo,
                                Vehicle = b.Fleet_VehicleType.VehicleType,
                                OrderNo = b.OrderNo,
                                PupilNo = b.PupilNo,
                                Passenger = b.CustomerName,
                                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                                //  Charges = b.FareRate.ToDecimal(),
                                Charges = b.CompanyPrice.ToDecimal(),
                                CompanyId = b.CompanyId,
                                CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                                Parking = b.ParkingCharges.ToDecimal(),
                                Waiting = b.WaitingCharges.ToDecimal(),
                                ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                                MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                                Congtion = b.CongtionCharges.ToDecimal(),
                                Description = "",
                                Total = (b.CompanyPrice.ToDecimal() + b.WaitingCharges.ToDecimal() + b.ParkingCharges.ToDecimal() + b.ExtraDropCharges.ToDecimal()),
                                //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                                BookedBy = b.BookedBy.ToStr(),
                                Fare = b.FareRate.ToDecimal(),

                                AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                                PaymentTypeId = b.PaymentTypeId,
                                BookingStatusId=b.BookingStatusId.ToIntorNull(),
                                PaymentType = b.Gen_PaymentType.PaymentType,
                                  Tip=b.TipAmount.ToDecimal(),
                            }).ToList();


                frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


                frm.HiddenColumns = hiddenColumns;
                frm.ShowDialog();


            }
            else
            {
                var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
                var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

                var list = (from b in list1
                            join c in list2 on b.Id equals c.BookingId into table2
                            from c in table2.DefaultIfEmpty()
                            where (c == null)
                            select new
                            {
                                Id = b.Id,


                                BookingDate = b.BookingDate,
                                PickupDate = b.PickupDateTime,

                                RefNo = b.BookingNo,
                                Vehicle = b.Fleet_VehicleType.VehicleType,
                                OrderNo = b.OrderNo,
                                PupilNo = b.PupilNo,
                                Passenger = b.CustomerName,
                                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                                //  Charges = b.FareRate.ToDecimal(),
                                Charges = b.CompanyPrice.ToDecimal(),
                                CompanyId = b.CompanyId,
                                CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                                Parking = b.ParkingCharges.ToDecimal(),
                                Waiting = b.WaitingCharges.ToDecimal(),
                                ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                                MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                                Congtion = b.CongtionCharges.ToDecimal(),
                                Description = "",
                                Total =  (b.CompanyPrice.ToDecimal() + b.WaitingCharges.ToDecimal() + b.ParkingCharges.ToDecimal()),
                                //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                                BookedBy = b.BookedBy.ToStr(),
                                Fare = b.FareRate.ToDecimal(),

                                AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                                PaymentTypeId = b.PaymentTypeId,
                                BookingStatusId = b.BookingStatusId.ToIntorNull(),
                                PaymentType = b.Gen_PaymentType.PaymentType,
                                Tip = b.TipAmount.ToDecimal(),
                            }).ToList();


                frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


                frm.HiddenColumns = hiddenColumns;
                frm.ShowDialog();


            }




            if (frm != null)
            {
                listofObjects = frm.ListofData;


                frm.Dispose();
                GC.Collect();

            }


            return listofObjects;

        }


        //private void btnPickBooking_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //    //   int bookingTypeId = ddlBookingType.SelectedValue.ToInt();
        //        int companyId = ddlCompany.SelectedValue.ToInt();

        //        DateTime? fromDate = dtpFromDate.Value.ToDate();
        //        DateTime? tillDate = dtpTillDate.Value.ToDate();

        //        long departmentId = ddlDepartment.SelectedValue.ToLong();
        //        int costcenterId = ddlCostCenter.SelectedValue.ToInt();


        //        string error = string.Empty;
        //        if (companyId == 0)
        //        {
        //            error += "Required : Company";
        //        }

        //        if (fromDate == null)
        //        {
        //            if (string.IsNullOrEmpty(error))
        //                error += Environment.NewLine;

        //            error += "Required : From Date";
        //        }

        //        if (tillDate == null)
        //        {
        //            if (string.IsNullOrEmpty(error))
        //                error += Environment.NewLine;

        //            error += "Required : To Date";


        //        }

        //        if (!string.IsNullOrEmpty(error))
        //        {
        //            ENUtils.ShowMessage(error);
        //            return;

        //        }



        //        string[] hiddenColumns = null;


        //        hiddenColumns = new string[] {  "Id", "CompanyId","CompanyName","Parking","Destination","Waiting","ExtraDrop","MeetAndGreet","Congtion",
        //                                        "Total","OrderNo","PupilNo","BookingDate","Description","Fare","AccountType","PaymentTypeId"};

                

        //        bool IsDepartmentWise = chkDepartmentWise.Checked;
        //        bool IsCostCenterWise = chkCostCenterWise.Checked;

        //        string orderNo = ddlOrderNo.SelectedValue.ToStr().Trim();


        //        Func<Booking, bool> _conditionDate = null;
        //        if (ddlPickType.SelectedIndex == 0)
        //            _conditionDate = b => b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate;
        //        else
        //            _conditionDate = b => b.BookingDate.Value.Date >= fromDate && b.BookingDate.Value.Date <= tillDate;


        //        List<object[]> list = General.ShowBookingMultiLister(c => c.CompanyId == companyId && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED) && (orderNo=="" || c.OrderNo==orderNo)
        //                                                                    && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
        //                                                                      && ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false))
        //                                                               ,
        //                                                                 a => a.InvoiceId != null,
        //                                                                hiddenColumns, _conditionDate,TemplateName.ToStr());
        //        GridViewRowInfo row;

        //        int cnt=list.Count;
                

        //        //foreach (object[] obj in list)
        //        //{
        //        //    long bookingId = obj[0].ToLong();

        //        //    if (grdLister.Rows.Count(c => c.Cells[COLS.BookingId].Value.ToLong() == bookingId) > 0)
        //        //        continue;

        //        //    row = grdLister.Rows.AddNew();


        //        //    row.Cells[COLS.BookingId].Value = obj[0].ToLongorNull();
        //        //    row.Cells[COLS.RefNumber].Value = obj[3].ToStr();
        //        //    row.Cells[COLS.PickupDate].Value = obj[2].ToDateTime();


        //        //    row.Cells[COLS.Vehicle].Value = obj[4].ToStr();


        //        //    row.Cells[COLS.OrderNo].Value = obj[5].ToStr();
        //        //    row.Cells[COLS.PupilNo].Value = obj[6].ToStr();

        //        //    row.Cells[COLS.Passenger].Value = obj[7].ToStr();


        //        //    row.Cells[COLS.PickupPoint].Value = obj[8].ToStr();
        //        //    row.Cells[COLS.Destination].Value = obj[9].ToStr();
        //        //    row.Cells[COLS.Charges].Value = obj[10].ToDecimal();
        //        //    row.Cells[COLS.Parking].Value = obj[13].ToDecimal();
        //        //    row.Cells[COLS.Waiting].Value = obj[14].ToDecimal();
        //        //    row.Cells[COLS.ExtraDrop].Value = obj[15].ToDecimal();
        //        //    row.Cells[COLS.MeetAndGreet].Value = obj[16].ToDecimal();
        //        //    row.Cells[COLS.CongtionCharge].Value = obj[17].ToDecimal();
        //        //    row.Cells[COLS.Total].Value = obj[19].ToDecimal();

        //        //    row.Cells[COLS.RemovalDescription].Value = obj[18].ToStr();

        //        //    row.Cells[COLS.BookedBy].Value = obj[20].ToStr();

        //        //}

        //        var  existBookingId=grdLister.Rows.Select(c=>c.Cells[COLS.BookingId].Value.ToLong()).ToList<long>();
        //        //int newCnt= list.Select(c=>c[0].ToLong()).Except(existBookingId).Count();
        //        //grdLister.RowCount += newCnt;



        //         list.RemoveAll(c => existBookingId.Contains(c[0].ToLong()));

        //         cnt = list.Count;

        //        for (int i = 0; i < cnt; i++)
        //        {
        //           // long bookingId = list[i][0].ToLong();

        //            //if (grdLister.Rows.Count(c => c.Cells[COLS.BookingId].Value.ToLong() == bookingId) > 0)
        //            //continue;

        //            row = grdLister.Rows.AddNew();


        //            row.Cells[COLS.BookingId].Value = list[i][0].ToLongorNull();
        //            row.Cells[COLS.RefNumber].Value = list[i][3].ToStr();
        //            row.Cells[COLS.PickupDate].Value = list[i][2].ToDateTime();


        //            row.Cells[COLS.Vehicle].Value = list[i][4].ToStr();


                   

        //            row.Cells[COLS.OrderNo].Value = list[i][5].ToStr();
        //            row.Cells[COLS.PupilNo].Value = list[i][6].ToStr();

        //            row.Cells[COLS.Passenger].Value = list[i][7].ToStr();


        //            row.Cells[COLS.PickupPoint].Value = list[i][8].ToStr();
        //            row.Cells[COLS.Destination].Value = list[i][9].ToStr();
        //            row.Cells[COLS.Charges].Value = list[i][10].ToDecimal();
        //            row.Cells[COLS.Parking].Value = list[i][13].ToDecimal();
        //            row.Cells[COLS.Waiting].Value = list[i][14].ToDecimal();
        //            row.Cells[COLS.ExtraDrop].Value = list[i][15].ToDecimal();
        //            row.Cells[COLS.MeetAndGreet].Value = list[i][16].ToDecimal();
        //            row.Cells[COLS.CongtionCharge].Value = list[i][17].ToDecimal();
                   
                    
                    
                   
        //            row.Cells[COLS.Total].Value = list[i][19].ToDecimal();

        //            row.Cells[COLS.RemovalDescription].Value = list[i][18].ToStr();

        //            row.Cells[COLS.BookedBy].Value = list[i][20].ToStr();


        //            row.Cells[COLS.PaymentTypeId].Value = list[i][23].ToInt();

        //        }

        //        CalculateTotal();
        //    }
        //    catch (Exception ex)
        //    {
        //        ENUtils.ShowMessage(ex.Message);

        //    }

        //}

        private void CalculateTotal()
        {

            txtCreditAmount.Text = grdLister.Rows.Where(c=>c.Cells[COLS.PaymentTypeId].Value.ToInt()!=6 && c.Cells["Check"].Value.ToBool() == true)
                                                    .Sum(c => c.Cells[COLS.Total].Value.ToDecimal()).ToString();

        }

        private void ClearOrderNo()
        {
           

        }

        private void ddlCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (grdLister.Columns.Count == 0) return;

            int? companyId = ddlCompany.SelectedValue.ToIntorNull();


            if (companyId == null)
            {
                SetOrderNoColumn(false);
                SetPupilNoColumn(false);
                SetBookedByColumn(false);
                ClearDepartment();
                ClearCostCenter();
                ClearOrderNo();
              
            }
            else
            {
                Gen_Company obj = General.GetObject<Gen_Company>(c => c.Id == companyId);
                if (obj != null)
                {
                    if(ddlSubCompany!=null && obj.SubCompanyId!=null)
                         this.ddlSubCompany.SelectedValue = obj.SubCompanyId;
                    this.companyEmail = obj.Email.ToStr().Trim();
                    FillDepartmentCombo(obj.Id);
                    FillCostCenterCombo(obj.Id);
                    bool orderNo = obj.HasOrderNo.ToBool();
                    bool pupilNo = obj.HasPupilNo.ToBool();

                    bool HasBookedBy = obj.HasBookedBy.ToBool();
                    SetOrderNoColumn(orderNo);
                    SetPupilNoColumn(pupilNo);
                    SetBookedByColumn(HasBookedBy);


                    if (obj.HasSingleOrderNo.ToBool())
                    {
                        lblOrderNo.Visible = true;
                        txtOrderNo.Visible = true;
                        grdLister.Columns[COLS.OrderNo].IsVisible = false;
                    }
                    else
                    {
                        lblOrderNo.Visible = false;
                        txtOrderNo.Visible = false;

                    }

                }
            }
        }

        private void ClearDepartment()
        {
            ddlDepartment.DataSource = null;

        }

        private void FillDepartmentCombo(int companyId)
        {
            ComboFunctions.FillCompanyDepartmentCombo(ddlDepartment, c => c.CompanyId == companyId);
        }


        private void ClearCostCenter()
        {
            ddlCostCenter.DataSource = null;

        }

        private void FillCostCenterCombo(int companyId)
        {
            ComboFunctions.FillCompanyCostCentersCombo(ddlCostCenter, c => c.CompanyId == companyId);
            ShowHideCostCenter();

        }


        private void ShowHideCostCenter()
        {
            bool show = ddlCostCenter.Items.Count > 0;

            chkCostCenterWise.Visible = show;
            ddlCostCenter.Visible = show;


        }



        private void SetOrderNoColumn(bool show)
        {

            grdLister.Columns[COLS.OrderNo].IsVisible = show;


            if (show)
            {
                grdLister.Columns["OrderNo"].Width = 80;

            }
        }

        private void SetBookedByColumn(bool show)
        {

            grdLister.Columns[COLS.BookedBy].IsVisible = show;


            if (show)
            {
                grdLister.Columns[COLS.BookedBy].Width = 100;

            }
        }

        private void SetPupilNoColumn(bool show)
        {

            grdLister.Columns[COLS.PupilNo].IsVisible = show;


            if (show)
            {
                grdLister.Columns[COLS.PupilNo].Width = 80;

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public override void Print()
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            frmCreditNoteReport frm = new frmCreditNoteReport();
           // frm.HasSplitByDept = chkSplitByDept.Checked;
            frm.HasSplitByField = ddlSplitBy.Text;
            frm.ObjInvoice = objMaster.Current;
            var list = General.GetQueryable<vu_InvoiceCreditNoteReport>(a => a.Id == id).OrderBy(c=>c.PickupDate).ToList();

            if (list.Count > 0)
            {
                frm.DataSource = list;

                frm.GenerateReport();

                frm.ShowDialog();
            }
            else
            {
                ENUtils.ShowMessage("Credit Note Booking not found");
            }

          
            //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmInvoiceReport1");

            //if (doc != null)
            //{
            //    doc.Close();
            //}

            //UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
          
            //  MainMenuForm.MainMenuFrm.ShowForm(frm);
        }

        private void ExportReport(string exportTo)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            frmCreditNoteReport frm = new frmCreditNoteReport();
           // frm.HasSplitByDept = chkSplitByDept.Checked;
            frm.HasSplitByField = ddlSplitBy.Text;
            frm.ObjInvoice = objMaster.Current;

            frm.ExportFileType = exportTo;
            var list = General.GetQueryable<vu_InvoiceCreditNoteReport>(a => a.Id == id).OrderBy(c => c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;


            frm.GenerateReport();

            frm.ExportReport(objMaster.Current.InvoiceNo, exportTo);


        }

       

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ExportReport("pdf");
        }

        private void ddlCompany_SizeChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            OnSave();
          
        }
        public override void OnNew()
        {
            txtInvoiceNo.Text = string.Empty;
            ComboFunctions.FillCompanyForInvoiceCombo(ddlCompany);
            chkDepartmentWise.Checked = false;
            chkCostCenterWise.Checked = false;
            grdLister.Rows.Clear();
            txtCreditAmount.Text = string.Empty;
            ddlCompany.Enabled = true;

        }

        private void chkDepartmentWise_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (args.NewValue == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                //ddlDepartment.Enabled = true;
              //  chkAutoContinue.Visible = true;
            }
            else
            {

                ddlDepartment.Enabled = false;
                ddlDepartment.SelectedValue = null;
               // chkAutoContinue.Visible = false;
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            frmCreditNoteReport frm = new frmCreditNoteReport();
          
            frm.HasSplitByField = ddlSplitBy.Text;
            frm.ObjInvoice = objMaster.Current;

            var list = General.GetQueryable<vu_InvoiceCreditNoteReport>(a => a.Id == id).OrderBy(c => c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;

         

            frm.GenerateReport();

            frm.SendEmail(objMaster.Current.InvoiceNo,this.companyEmail);
        }

        private void chkCostCenterWise_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (args.NewValue == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlCostCenter.Enabled = true;

            }
            else
            {

                ddlCostCenter.Enabled = false;
                ddlCostCenter.SelectedValue = null; 
            }
        }





        private void ShowHideColumns(bool show)
        {

            grdLister.Columns[COLS.Charges].IsVisible = show;
            grdLister.Columns[COLS.CongtionCharge].IsVisible = show;
            grdLister.Columns[COLS.Destination].IsVisible = show;
            grdLister.Columns[COLS.ExtraDrop].IsVisible = show;
            grdLister.Columns[COLS.MeetAndGreet].IsVisible = show;
         //   grdLister.Columns[COLS.OrderNo].IsVisible = show;
            grdLister.Columns[COLS.Parking].IsVisible = show;
            grdLister.Columns[COLS.Passenger].IsVisible = show;
            grdLister.Columns[COLS.PickupDate].IsVisible = show;
            grdLister.Columns[COLS.PickupPoint].IsVisible = show;
            grdLister.Columns[COLS.Destination].IsVisible = show;
            grdLister.Columns[COLS.Vehicle].IsVisible = show;
            grdLister.Columns[COLS.RefNumber].IsVisible = show;
            grdLister.Columns[COLS.Waiting].IsVisible = show;
            grdLister.Columns[COLS.VehicleID].IsVisible = show;

            if (!show)
            {
                grdLister.Columns[COLS.Total].HeaderText = "Amount";
                grdLister.Columns[COLS.Total].Width = 70;

            }
            else
            {
                grdLister.Columns[COLS.Total].HeaderText = "Total";
                grdLister.Columns[COLS.Total].Width = 45;


            }
        }

      

        private void frmInvoice_Shown(object sender, EventArgs e)
        {
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

            
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            ExportReport("excel");
        }

        private void btnAddNewInvoice_Click(object sender, EventArgs e)
        {
            objMaster.Clear();
            objMaster.PrimaryKeyValue = null;
            OnCreateNew();
        }


        private  void OnCreateNew()
        {
            txtInvoiceNo.Text = string.Empty;
          //  ComboFunctions.FillCompanyCombo(ddlCompany);
           
            chkCostCenterWise.Checked = false;
            grdLister.Rows.Clear();
            txtCreditAmount.Text = string.Empty;
            ddlCompany.Enabled = true;
            //if (chkAutoContinue.Visible == true && chkAutoContinue.Checked)
            //{
            //    int Index=ddlDepartment.SelectedIndex.ToInt();
            //    Index=(Index+1);
            //    ddlDepartment.SelectedIndex = Index;
                
            //}
            //else
            //{
            //    chkDepartmentWise.Checked = false; 
            //}

        }

       

       

        private void btnCreditNotes_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                OnSave();
                this.Close();
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.ToString());
            }
           
        }
       

    }
}
