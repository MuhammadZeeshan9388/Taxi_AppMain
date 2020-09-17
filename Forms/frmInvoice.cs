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
    public partial class frmInvoice : UI.SetupBase
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
        public frmInvoice()
        {
            InitializeComponent();
            InitializeConstructor();
           
           
        }

        public frmInvoice(int Id)
        {
            InitializeComponent();
            InitializeConstructor();
         
            ddlCompany.SelectedValue = Id;
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
          

            dtpInvoiceDate.Value = DateTime.Now.ToDate();
            dtpDueDate.Value = DateTime.Now.ToDate().AddMonths(1);
            FormatChargesGrid();

            grdLister.ShowGroupPanel = false;
           // grdLister.AutoCellFormatting = true;
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            objMaster = new InvoiceBO();
            this.SetProperties((INavigation)objMaster);

            grdLister.AllowAddNewRow = false;


            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
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

                        long id = row.Cells[COLS.BookingId].Value.ToLong();
                        int vehicleTypeId = row.Cells[COLS.VehicleID].Value.ToInt();
                        decimal fare = row.Cells[COLS.Charges].Value.ToDecimal();
                        decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                        decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                        decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                        decimal tip = row.Cells[COLS.Tip].Value.ToDecimal();

                   
                        decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();
                        string Destination = row.Cells[COLS.Destination].Value.ToStr();
                        string PickupPoint = row.Cells[COLS.PickupPoint].Value.ToStr();
                        string Passenger = row.Cells[COLS.Passenger].Value.ToStr();
               
                        int? invoicepaymentId = row.Cells[COLS.Payment_ID].Value.ToIntorNull();

                        if (Destination == "")
                         {
                                RadMessageBox.Show("Required: Destination");
                                return;
                         }
                         if (PickupPoint == "")
                         {
                             RadMessageBox.Show("Required: PickupPoint");
                             return;
                         }                              

                        string orderNo = row.Cells[COLS.OrderNo].Value.ToStr();

                        BookingBO objMaster = new BookingBO();
                        objMaster.GetByPrimaryKey(id);

                        if (objMaster.Current != null)
                        {
                            objMaster.Current.CompanyPrice = fare;
                            objMaster.Current.ParkingCharges = parking;
                            objMaster.Current.WaitingCharges = waiting;
                            objMaster.Current.ExtraDropCharges = extraDrop;
                            objMaster.Current.TipAmount = tip;

                            objMaster.Current.TotalCharges = TotalCharges;
                            objMaster.Current.InvoicePaymentTypeId = invoicepaymentId;

                            objMaster.Current.OrderNo = orderNo;

                            objMaster.Current.ToAddress = Destination;
                            objMaster.Current.FromAddress = PickupPoint;
                            objMaster.Current.CustomerName = Passenger;

                             if (vehicleTypeId != 0)
                             {
                                 objMaster.Current.VehicleTypeId = vehicleTypeId;
                             }

                              

                             objMaster.CheckCustomerValidation = false;
                             objMaster.CheckDataValidation = false;
                             objMaster.DisableUpdateReturnJob = true;
                             objMaster.Save();
                           

                            CalculateTotal();


                            long index = grdLister.CurrentRow != null ? grdLister.CurrentRow.Cells["Id"].Value.ToLong() : -1;
                            int val = grdLister.TableElement.VScrollBar.Value;


                            Save();


                            if (index > 0)
                                grdLister.CurrentRow = grdLister.Rows.FirstOrDefault(c => c.Cells["Id"].Value.ToLong() == index);

                            grdLister.TableElement.VScrollBar.Value = val;
                        }
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
                col.Name = "OrderNo";
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = "Booked By";
                col.Name = COLS.BookedBy;
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = "Pupil No";
                col.Name = "PupilNo";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = "Vehicle";
                col.Name = "Vehicle";
                grdLister.Columns.Add(col);




                GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
                colCombo.Name = COLS.VehicleID;
              //  colCombo.IsVisible = false;
                colCombo.HeaderText = "Vehicle";
                colCombo.DataSource = General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo).Select(args => new { Id = args.Id, VehicleType = args.VehicleType }).ToList();
                colCombo.DisplayMember = "VehicleType";
                colCombo.ValueMember = "Id";
                colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
                colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                colCombo.ReadOnly = false;
               
                grdLister.Columns.Add(colCombo);




                col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.HeaderText = COLS.BookingStatusId;
                col.Name =COLS.BookingStatusId;
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                col.Name = COLS.Passenger;
                col.HeaderText = "Passenger";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.Width = 900;
                col.IsVisible = false;
                col.ReadOnly = true;
                col.Name = COLS.RemovalDescription;
                col.HeaderText = "Description";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                col.HeaderText = "Pickup Point";
                col.Name = "PickupPoint";
                grdLister.Columns.Add(col);



                col = new GridViewTextBoxColumn();
                col.HeaderText = "Destination";
                col.Name = "Destination";
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
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);

                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Parking";
                colD.Name = "Parking";
                colD.IsVisible = TemplateName != "Template13" && TemplateName != "Template24";
   
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);

                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = COLS.Tip;
                colD.Name = COLS.Tip;
                colD.Maximum = 1000;
                colD.IsVisible = TemplateName.ToLower() == "template27";
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Waiting";
                colD.Name = "Waiting";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Extra Charges";
                colD.Name = "ExtraDrop";
                colD.Maximum = 9999999;
                colD.IsVisible = true;
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
                colD.Expression = "Charges+Parking+Waiting+ExtraDrop";
                colD.FormatString = "{0:#,###0.00}";

                if (TemplateName == "Template10" || TemplateName == "Template25" || TemplateName == "Template26" || TemplateName == "Template32" || TemplateName == "Template45" || TemplateName == "Template48")
                {
                    colD.Expression = "Charges+Parking+Waiting+ExtraDrop";
                }
                else  if ( TemplateName == "Template27")
                {
                    colD.Expression = "Charges+Parking+Waiting+Tip";
                }

                else if (TemplateName == "Template13" || (TemplateName == "Template24"  ))
                {
                    colD.Expression = "Charges+Waiting";

                    
                }
                else
                {
                    colD.Expression = "Charges+Parking+Waiting+ExtraDrop";
                    grdLister.Columns["ExtraDrop"].IsVisible = true;
                }


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
                grdLister.Columns["ExtraDrop"].HeaderText = "Extra Charges";

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


                if (TemplateName.ToStr().ToLower().Trim() == "template15" || TemplateName.ToStr().ToLower().Trim() == "template10" || TemplateName.ToStr().ToLower().Trim() == "template32" || TemplateName == "Template45" || TemplateName == "Template48")
                {
                    ddlSplitBy.Visible = true;
                    lblSplitBy.Visible = true;
                    if (TemplateName.ToStr().ToLower().Trim() == "template10" || TemplateName.ToStr().ToLower().Trim() == "template32" || TemplateName == "Template45" || TemplateName == "Template48")
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

                if (this.AutoInc == false && txtInvoiceNo.Text.Trim() == string.Empty)
                {
                    ENUtils.ShowMessage("Required : Invoice No");
                    return;

                }



                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                  //  objMaster.Current.InvoicePaymentTypeID = 1;
                    objMaster.Current.AddOn = DateTime.Now;
                    objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToIntorNull();
                    objMaster.Current.AddLog = AppVars.LoginObj.UserName.ToStr();
                }
                else
                {
                    objMaster.Edit();

                    objMaster.Current.EditOn = DateTime.Now;
                    objMaster.Current.EditBy = AppVars.LoginObj.LuserId.ToIntorNull();
                    objMaster.Current.EditLog = AppVars.LoginObj.UserName.ToStr();
                }

                objMaster.Current.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;
                objMaster.Current.InvoiceDate = dtpInvoiceDate.Value.ToDate();
                objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                objMaster.Current.DepartmentId = ddlDepartment.SelectedValue.ToLongorNull();
                objMaster.Current.DepartmentWise = chkDepartmentWise.Checked;

                objMaster.Current.CostCenterId=ddlCostCenter.SelectedValue.ToIntorNull();
                objMaster.Current.CostCenterWise=chkCostCenterWise.Checked;
                objMaster.Current.DueDate = dtpDueDate.Value.ToDate();


                objMaster.Current.Remarks = txtNotes.Text.Trim();

                if (chkAllFromDate.Checked)
                {
                    objMaster.Current.FromDate = grdLister.Rows.Select(c => c.Cells[COLS.PickupDate].Value.ToDate()).OrderBy(c => c.Date).FirstOrDefault().ToDate();

                }
                else
                {

                    objMaster.Current.FromDate = dtpFromDate.Value.ToDate();

                }
               
                objMaster.Current.TillDate = dtpTillDate.Value.ToDate();

                objMaster.Current.InvoiceNo = txtInvoiceNo.Text.Trim();
                objMaster.Current.InvoiceTypeId = Enums.INVOICE_TYPE.ACCOUNT;

                objMaster.Current.InvoiceTotal = grdLister.Rows.Where(c=>c.Cells[COLS.PaymentTypeId].Value.ToInt()!=6)
                                    .Sum(c => c.Cells[COLS.Total].Value.ToDecimal());


                objMaster.Current.SubCompanyId = ddlSubCompany.SelectedValue.ToIntorNull();



            //   var distinctRows= grdLister.Rows.Select(c => c.Cells[COLS.BookingId].ToLong()).Distinct();

                objMaster.Current.OrderNo = txtOrderNo.Text.Trim();
               
             


                string[] skipProperties = { "Invoice", "Booking" };
                IList<Invoice_Charge> savedList = objMaster.Current.Invoice_Charges;
                List<Invoice_Charge> listofDetail = (from r in grdLister.Rows

                                                     select new Invoice_Charge
                                                            {
                                                                Id = r.Cells[COLS.ID].Value.ToLong(),
                                                                InvoiceId = r.Cells[COLS.InvoiceId].Value.ToLong(),
                                                                BookingId = r.Cells[COLS.BookingId].Value.ToLongorNull(),

                                                            }).ToList();


                Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

                objMaster.Save();

                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
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


        public override void DisplayRecord()
        {
            try
            {


                if (objMaster.Current == null) return;

                btnExportExcel.Enabled = true;
                btnExportPDF.Enabled = true;
                btnPrint.Enabled = true;
                btnSendEmail.Enabled = true;

                ddlBookingType.SelectedValue = objMaster.Current.BookingTypeId;

                txtInvoiceNo.Text = objMaster.Current.InvoiceNo.ToStr();
                dtpInvoiceDate.Value = objMaster.Current.InvoiceDate.ToDate();


                txtNotes.Text = objMaster.Current.Remarks.ToStr();


                if (objMaster.Current.CompanyId != null)
                {

                    //var data = (List<Gen_Company>)ddlCompany.DataSource;
                    //data.Add(objMaster.Current.Gen_Company);
                    if (objMaster.Current.Gen_Company.IsClosed.ToBool() || ddlCompany.Items.Count(c => c.Value.ToInt() == objMaster.Current.CompanyId.ToInt()) == 0)
                    {
                        ComboFunctions.FillCompanyForInvoiceCombo(ddlCompany, objMaster.Current.CompanyId.ToInt());
                    }

                }

                ddlCompany.SelectedValue = objMaster.Current.CompanyId;
                ddlCompany.Enabled = false;
                dtpDueDate.Value = objMaster.Current.DueDate.ToDate();
                ddlDepartment.SelectedValue = objMaster.Current.DepartmentId;
                chkDepartmentWise.Checked = objMaster.Current.DepartmentWise.ToBool();

                ddlCostCenter.SelectedValue = objMaster.Current.CostCenterId;
                chkCostCenterWise.Checked = objMaster.Current.CostCenterWise.ToBool();


                ddlSubCompany.SelectedValue = objMaster.Current.SubCompanyId;


                dtpFromDate.Value = objMaster.Current.FromDate.ToDateorNull();
                dtpTillDate.Value = objMaster.Current.TillDate.ToDateorNull();

                txtOrderNo.Text = objMaster.Current.OrderNo.ToStr().Trim();

                List<stp_GetInvoiceBookingsResult> list = null;

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    list = db.stp_GetInvoiceBookings(objMaster.Current.Id).ToList();
                }


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


                    grdLister.Rows[i].Cells[COLS.Parking].Value = list[i].ParkingCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].FromAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].ToAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Waiting].Value = list[i].WaitingCharges.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = list[i].ExtraDropCharges.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.Tip].Value = list[i].TipAmount.ToDecimal();

                    if (TemplateName == "Template13" || TemplateName == "Template24")
                    {
                        grdLister.Rows[i].Cells[COLS.Total].Value = list[i].CompanyPrice.ToDecimal() + list[i].WaitingCharges.ToDecimal();

                    }
                    else if (TemplateName == "Template14")
                    {
                        grdLister.Rows[i].Cells[COLS.Total].Value = list[i].CompanyPrice.ToDecimal() + list[i].WaitingCharges.ToDecimal() + list[i].ParkingCharges.ToDecimal() + list[i].ExtraDropCharges.ToDecimal();
                    }
                    else
                    {
                        grdLister.Rows[i].Cells[COLS.Total].Value = list[i].TotalCharges.ToDecimal();
                    }

                    grdLister.Rows[i].Cells[COLS.Passenger].Value = list[i].CustomerName.ToStr().Trim();
                    grdLister.Rows[i].Cells[COLS.PaymentTypeId].Value = list[i].PaymentTypeId.ToInt();

                    grdLister.Rows[i].Cells[COLS.BookingStatusId].Value = list[i].BookingStatusId.ToInt();



                }


                grdLister.CurrentRow = null;

                grdLister.EndUpdate();

                grdLister.ReadOnly = false;
                grdLister.AllowEditRow = true;

                txtInvoiceAmount.Text = objMaster.Current.InvoiceTotal.ToDecimal().ToStr();

                ShowAutoContinue();
                btnCreditNote.Visible = true;


                using (TaxiDataContext db = new TaxiDataContext())
                {

                    bool creditNotExist = db.InvoiceCreditNotes.Count(c => c.InvoiceId == objMaster.Current.Id) > 0;


                    if (creditNotExist)
                    {

                        btnCreditNote.BackColor = Color.LightGreen;
                        this.btnCreditNote.ContextMenuStrip = this.contextMenuCreditNote;

                    }

                }
            }
            catch
            {



            }

        }


        private void ShowAutoContinue()
        {

            if (ddlDepartment.SelectedValue != null)
            {
                chkAutoContinue.Visible = true;
                chkAutoContinue.Checked = true;
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

        private void AutoPickUp()
        {
            try
            {
                int companyId = ddlCompany.SelectedValue.ToInt();

                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                long departmentId = ddlDepartment.SelectedValue.ToLong();
                int costcenterId = ddlCostCenter.SelectedValue.ToInt();


                string error = string.Empty;
                if (companyId == 0)
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



                string[] hiddenColumns = null;


                hiddenColumns = new string[] {  "Id", "CompanyId","CompanyName","Parking","Destination","Waiting","ExtraDrop","MeetAndGreet","Congtion",
                                                "Total","OrderNo","PupilNo","BookingDate","Description","Fare","AccountType","PaymentTypeId"};



                bool IsDepartmentWise = chkDepartmentWise.Checked;
                bool IsCostCenterWise = chkCostCenterWise.Checked;

                string orderNo = ddlOrderNo.SelectedValue.ToStr().Trim();


                Func<Booking, bool> _conditionDate = null;
                if (ddlPickType.SelectedIndex == 0)
                    _conditionDate = b => b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate;
                else
                    _conditionDate = b => b.BookingDate.Value.Date >= fromDate && b.BookingDate.Value.Date <= tillDate;


                bool ForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();





                Expression<Func<Booking,bool>> expPickBooking=null;
                Expression<Func<Invoice_Charge, bool>> _invoiceCondition = null;

                if(AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt()==0 || AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt()==1)
                {
                    expPickBooking = c => c.CompanyId == companyId && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED) && (orderNo == "" || c.OrderNo == orderNo)
                                                                           && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
                                                                             && ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false));
                }
                else if(AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt()==2) // Pick DISPATCHED AND NOPICKUP BOOKINGS
                {
                    expPickBooking = c => c.CompanyId == companyId &&
                                       (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || (c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH && c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP))
                                     && (orderNo == "" || c.OrderNo == orderNo)
                                     && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
                                     && ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false));

                                                                                                             



                }
                else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 3)
                {
                    expPickBooking = c => c.CompanyId == companyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED) && (orderNo == "" || c.OrderNo == orderNo)
                                                                           && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
                                                                             && ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false));

                                                                                                             
                }

                string templateName = "Template13";
                UM_Form_Template template = General.GetObject<UM_Form_Template>(c => c.FormId != null && c.UM_Form.FormName == "frmInvoiceReport" && c.IsDefault == true);



                if (template != null)
                {

                    TemplateName = template.TemplateName.ToStr().Trim();

                }
                GridViewRowInfo row;
                if (templateName == "Template13")
                {

                    var list1 = General.GetGeneralList<Booking>(expPickBooking).Where(_conditionDate);
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
                                    BookingStatusId = b.BookingStatusId.ToIntorNull()
                                }).ToList();


                    int cnt = list.Count;
                    grdLister.Rows.Clear();
                    
                    for (int i = 0; i < cnt; i++)
                    {

                        row = grdLister.Rows.AddNew();
                        row.Cells[COLS.BookingId].Value = list[i].Id.ToLongorNull();
                        row.Cells[COLS.RefNumber].Value = list[i].RefNo.ToStr();
                        row.Cells[COLS.PickupDate].Value = list[i].PickupDate.ToDateTime();
                        row.Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();

                        row.Cells[COLS.OrderNo].Value = list[i].OrderNo.ToStr();
                        row.Cells[COLS.PupilNo].Value = list[i].PupilNo.ToStr();

                        row.Cells[COLS.Passenger].Value = list[i].Passenger.ToStr();
                        row.Cells[COLS.PickupPoint].Value = list[i].PickupPoint.ToStr();
                        row.Cells[COLS.Destination].Value = list[i].Destination.ToStr();
                        row.Cells[COLS.Charges].Value = list[i].Charges.ToDecimal();
                        row.Cells[COLS.Parking].Value = list[i].Parking.ToDecimal();
                        row.Cells[COLS.Waiting].Value = list[i].Waiting.ToDecimal();
                        row.Cells[COLS.ExtraDrop].Value = list[i].ExtraDrop.ToDecimal();

                        row.Cells[COLS.Total].Value = list[i].Total.ToDecimal();

                        row.Cells[COLS.RemovalDescription].Value = list[i].Destination.ToStr();

                        row.Cells[COLS.BookedBy].Value = list[i].BookedBy.ToStr();

                        row.Cells[COLS.PaymentTypeId].Value = list[i].PaymentTypeId.ToInt();

                        row.Cells[COLS.BookingStatusId].Value = list[i].BookingStatusId.ToInt();
                    }
                }
                else if (templateName == "Template14" || templateName == "Template23")
                {
                    var list1 = General.GetGeneralList<Booking>(expPickBooking).Where(_conditionDate);
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
                                    BookingStatusId = b.BookingStatusId.ToIntorNull()
                                }).ToList();
                    int cnt = list.Count;
                    grdLister.Rows.Clear();

                    for (int i = 0; i < cnt; i++)
                    {

                        row = grdLister.Rows.AddNew();
                        row.Cells[COLS.BookingId].Value = list[i].Id.ToLongorNull();
                        row.Cells[COLS.RefNumber].Value = list[i].RefNo.ToStr();
                        row.Cells[COLS.PickupDate].Value = list[i].PickupDate.ToDateTime();
                        row.Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();

                        row.Cells[COLS.OrderNo].Value = list[i].OrderNo.ToStr();
                        row.Cells[COLS.PupilNo].Value = list[i].PupilNo.ToStr();

                        row.Cells[COLS.Passenger].Value = list[i].Passenger.ToStr();
                        row.Cells[COLS.PickupPoint].Value = list[i].PickupPoint.ToStr();
                        row.Cells[COLS.Destination].Value = list[i].Destination.ToStr();
                        row.Cells[COLS.Charges].Value = list[i].Charges.ToDecimal();
                        row.Cells[COLS.Parking].Value = list[i].Parking.ToDecimal();
                        row.Cells[COLS.Waiting].Value = list[i].Waiting.ToDecimal();
                        row.Cells[COLS.ExtraDrop].Value = list[i].ExtraDrop.ToDecimal();

                        row.Cells[COLS.Total].Value = list[i].Total.ToDecimal();

                        row.Cells[COLS.RemovalDescription].Value = list[i].Destination.ToStr();

                        row.Cells[COLS.BookedBy].Value = list[i].BookedBy.ToStr();

                        row.Cells[COLS.PaymentTypeId].Value = list[i].PaymentTypeId.ToInt();

                        row.Cells[COLS.BookingStatusId].Value = list[i].BookingStatusId.ToInt();
                    }
                }
                else
                {
                    var list1 = General.GetGeneralList<Booking>(expPickBooking).Where(_conditionDate);
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
                                    Total = (b.CompanyPrice.ToDecimal() + b.WaitingCharges.ToDecimal() + b.ParkingCharges.ToDecimal()),
                                    //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                                    BookedBy = b.BookedBy.ToStr(),
                                    Fare = b.FareRate.ToDecimal(),

                                    AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                                    PaymentTypeId = b.PaymentTypeId,
                                    BookingStatusId = b.BookingStatusId.ToIntorNull()
                                }).ToList();
                    int cnt = list.Count;
                    grdLister.Rows.Clear();

                    for (int i = 0; i < cnt; i++)
                    {

                        row = grdLister.Rows.AddNew();
                        row.Cells[COLS.BookingId].Value = list[i].Id.ToLongorNull();
                        row.Cells[COLS.RefNumber].Value = list[i].RefNo.ToStr();
                        row.Cells[COLS.PickupDate].Value = list[i].PickupDate.ToDateTime();
                        row.Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();

                        row.Cells[COLS.OrderNo].Value = list[i].OrderNo.ToStr();
                        row.Cells[COLS.PupilNo].Value = list[i].PupilNo.ToStr();

                        row.Cells[COLS.Passenger].Value = list[i].Passenger.ToStr();
                        row.Cells[COLS.PickupPoint].Value = list[i].PickupPoint.ToStr();
                        row.Cells[COLS.Destination].Value = list[i].Destination.ToStr();
                        row.Cells[COLS.Charges].Value = list[i].Charges.ToDecimal();
                        row.Cells[COLS.Parking].Value = list[i].Parking.ToDecimal();
                        row.Cells[COLS.Waiting].Value = list[i].Waiting.ToDecimal();
                        row.Cells[COLS.ExtraDrop].Value = list[i].ExtraDrop.ToDecimal();

                        row.Cells[COLS.Total].Value = list[i].Total.ToDecimal();

                        row.Cells[COLS.RemovalDescription].Value = list[i].Destination.ToStr();

                        row.Cells[COLS.BookedBy].Value = list[i].BookedBy.ToStr();

                        row.Cells[COLS.PaymentTypeId].Value = list[i].PaymentTypeId.ToInt();

                        row.Cells[COLS.BookingStatusId].Value = list[i].BookingStatusId.ToInt();
                    }
                }
                CalculateTotal();              

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        
        }
        private void btnPickBooking_Click(object sender, EventArgs e)
        {
            try
            {
                //   int bookingTypeId = ddlBookingType.SelectedValue.ToInt();
                int companyId = ddlCompany.SelectedValue.ToInt();

                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate() + new TimeSpan(23,59,59);

                if (chkAllFromDate.Checked)
                    fromDate = new DateTime(DateTime.Now.Year - 1, 1, 1);

                long departmentId = ddlDepartment.SelectedValue.ToLong();
                int costcenterId = ddlCostCenter.SelectedValue.ToInt();


                string error = string.Empty;
                if (companyId == 0)
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



                string[] hiddenColumns = null;


                hiddenColumns = new string[] {  "Id", "CompanyId","CompanyName","Parking","Destination","Waiting","ExtraDrop","MeetAndGreet","Congtion",
                                                "Total","OrderNo","PupilNo","BookingDate","Description","Fare","AccountType","PaymentTypeId","BookingStatusId","Tip"};



                bool IsDepartmentWise = chkDepartmentWise.Checked;
                bool IsCostCenterWise = chkCostCenterWise.Checked;

                string orderNo = ddlOrderNo.SelectedValue.ToStr().Trim();


                if (ddlOrderNo.Visible == false && txtOrderNo.Visible == true && orderNo.Length == 0 && txtOrderNo.Text.Length > 0)
                {
                    orderNo = txtOrderNo.Text.Trim();
                }

                Func<Booking, bool> _conditionDate = null;
                if (ddlPickType.SelectedIndex == 0)
                    _conditionDate = b => b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate;
                else
                    _conditionDate = b => b.BookingDate.Value.Date >= fromDate && b.BookingDate.Value.Date <= tillDate;


                bool ForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();
                
                Expression<Func<Booking,bool>> expPickBooking=null;


                if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 0 || AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 1)
                {
                    expPickBooking = c => c.CompanyId == companyId && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED) && (orderNo == "" || c.OrderNo == orderNo)
                                                                           && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
                                                                             && ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false));


                }
                else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2) // Pick DISPATCHED AND NOPICKUP BOOKINGS
                {

               
                    //if (orderNo.ToStr().Trim().Length == 0 && IsDepartmentWise == false && IsCostCenterWise == false)
                    //{
                        expPickBooking = c => c.CompanyId == companyId &&
                                           (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                                          && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate)
                                           && (orderNo == "" || c.OrderNo == orderNo)
                                           && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false));

                    //}
                    //if (orderNo.ToStr().Trim().Length > 0 && IsDepartmentWise == false && IsCostCenterWise == false)
                    //{
                    //    expPickBooking = c => c.CompanyId == companyId &&
                    //                       (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                    //                      && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate);

                    //}
                    //if (orderNo.ToStr().Trim().Length == 0 && IsDepartmentWise == false && IsCostCenterWise == false)
                    //{
                    //    expPickBooking = c => c.CompanyId == companyId &&
                    //                       (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                    //                      && (c.PickupDateTime >= fromDate && c.PickupDateTime <= tillDate);

                    //}

                    //&& (orderNo == "" || c.OrderNo == orderNo)
                    //&& ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
                    //&& ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false));
                    //expPickBooking = c => c.CompanyId == companyId &&
                    //                   (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED || c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP)
                    //                 && (orderNo == "" || c.OrderNo == orderNo)
                    //                 && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
                    //                 && ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false));

                }
                else if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 3)
                {
                    expPickBooking = c => c.CompanyId == companyId && c.PaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED) && (orderNo == "" || c.OrderNo == orderNo)
                                                                           && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
                                                                             && ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false));


                }





                List<object[]> list = ShowBookingMultiLister(expPickBooking,a => a.InvoiceId != null , hiddenColumns, _conditionDate, TemplateName.ToStr());
                GridViewRowInfo row;

                int cnt = list.Count;


             

                var existBookingId = grdLister.Rows.Select(c => c.Cells[COLS.BookingId].Value.ToLong()).ToList<long>();
            
               list.RemoveAll(c => existBookingId.Contains(c[0].ToLong()));

                cnt = list.Count;

                for (int i = 0; i < cnt; i++)
                {
                  

                    row = grdLister.Rows.AddNew();


                    row.Cells[COLS.BookingId].Value = list[i][0].ToLongorNull();
                    row.Cells[COLS.RefNumber].Value = list[i][3].ToStr();
                    row.Cells[COLS.PickupDate].Value = list[i][2].ToDateTime();


                    row.Cells[COLS.Vehicle].Value = list[i][4].ToStr();




                    row.Cells[COLS.OrderNo].Value = list[i][5].ToStr();
                    row.Cells[COLS.PupilNo].Value = list[i][6].ToStr();

                    row.Cells[COLS.Passenger].Value = list[i][7].ToStr();


                    row.Cells[COLS.PickupPoint].Value = list[i][8].ToStr();
                    row.Cells[COLS.Destination].Value = list[i][9].ToStr();
                    row.Cells[COLS.Charges].Value = list[i][10].ToDecimal();
                    row.Cells[COLS.Parking].Value = list[i][13].ToDecimal();
                    row.Cells[COLS.Waiting].Value = list[i][14].ToDecimal();
                    row.Cells[COLS.ExtraDrop].Value = list[i][15].ToDecimal();
             

                    row.Cells[COLS.Tip].Value = list[i][26].ToInt();


                    row.Cells[COLS.Total].Value = list[i][19].ToDecimal();

                    row.Cells[COLS.RemovalDescription].Value = list[i][18].ToStr();

                    row.Cells[COLS.BookedBy].Value = list[i][20].ToStr();
                    
                    row.Cells[COLS.PaymentTypeId].Value = list[i][23].ToInt();                   
                   
                    row.Cells[COLS.BookingStatusId].Value = list[i][24].ToInt();
                }

                CalculateTotal();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

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
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list1 = db.Bookings.Where(_condition).Where(_condition2);
                    var list2 = db.Invoice_Charges;

                    var list = (from b in list1
                                join c in list2 on b.Id equals c.BookingId into table2
                                join v in db.Fleet_VehicleTypes on b.VehicleTypeId equals v.Id
                                join p in db.Gen_PaymentTypes on b.PaymentTypeId equals p.Id
                                from c in table2.DefaultIfEmpty()
                                where (c == null)
                                select new
                                {
                                    Id = b.Id,


                                    BookingDate = b.BookingDate,
                                    PickupDate = b.PickupDateTime,

                                    RefNo = b.BookingNo,
                                    Vehicle = v.VehicleType,
                                    OrderNo = b.OrderNo,
                                    PupilNo = b.PupilNo,
                                    Passenger = b.CustomerName,
                                    PickupPoint = b.FromAddress,
                                    Destination = b.ToAddress,
                                    //  Charges = b.FareRate.ToDecimal(),
                                    Charges = b.CompanyPrice,
                                    CompanyId = b.CompanyId,
                                    CompanyName = "",
                                    Parking = b.ParkingCharges,
                                    Waiting = b.WaitingCharges,
                                    ExtraDrop = b.ExtraDropCharges,
                                    MeetAndGreet = b.MeetAndGreetCharges,
                                    Congtion = b.CongtionCharges,
                                    Description = "",
                                    Total = b.TotalCharges,
                                    //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                                    BookedBy = b.BookedBy,
                                    Fare = b.FareRate,

                                    AccountType = 0,
                                    PaymentTypeId = b.PaymentTypeId,
                                    BookingStatusId = b.BookingStatusId,
                                    PaymentType = p.PaymentType,
                                    Tip = b.TipAmount,
                                }).ToList();


                    //var list1 = General.GetGeneralList<Booking>(_condition).Where(_condition2);
                    //var list2 = General.GetGeneralList<Invoice_Charge>(_invoiceCondition);

                    //var list = (from b in list1
                    //            join c in list2 on b.Id equals c.BookingId into table2
                    //            from c in table2.DefaultIfEmpty()
                    //            where (c == null)
                    //            select new
                    //            {
                    //                Id = b.Id,


                    //                BookingDate = b.BookingDate,
                    //                PickupDate = b.PickupDateTime,

                    //                RefNo = b.BookingNo,
                    //                Vehicle = b.Fleet_VehicleType.VehicleType,
                    //                OrderNo = b.OrderNo,
                    //                PupilNo = b.PupilNo,
                    //                Passenger = b.CustomerName,
                    //                PickupPoint = !string.IsNullOrEmpty(b.FromDoorNo) ? b.FromDoorNo + " - " + b.FromAddress : b.FromAddress,
                    //                Destination = !string.IsNullOrEmpty(b.ToDoorNo) ? b.ToDoorNo + " - " + b.ToAddress : b.ToAddress,
                    //                //  Charges = b.FareRate.ToDecimal(),
                    //                Charges = b.CompanyPrice.ToDecimal(),
                    //                CompanyId = b.CompanyId,
                    //                CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                    //                Parking = b.ParkingCharges.ToDecimal(),
                    //                Waiting = b.WaitingCharges.ToDecimal(),
                    //                ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                    //                MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                    //                Congtion = b.CongtionCharges.ToDecimal(),
                    //                Description = "",
                    //                Total = (b.CompanyPrice.ToDecimal() + b.WaitingCharges.ToDecimal() + b.ParkingCharges.ToDecimal() + b.ExtraDropCharges.ToDecimal()),
                    //                //  BookedBy = b.DepartmentId != null ? b.Gen_Company_Department.DepartmentName.ToStr() : "",
                    //                BookedBy = b.BookedBy.ToStr(),
                    //                Fare = b.FareRate.ToDecimal(),

                    //                AccountType = b.CompanyId != null ? b.Gen_Company.AccountTypeId.ToInt() : 0,
                    //                PaymentTypeId = b.PaymentTypeId,
                    //                BookingStatusId = b.BookingStatusId.ToIntorNull(),
                    //                PaymentType = b.Gen_PaymentType.PaymentType,
                    //                Tip = b.TipAmount.ToDecimal(),
                    //            }).ToList();


                    frm = new Taxi_AppMain.frmLister(list, "Id", true, hiddenColumns);


                    frm.HiddenColumns = hiddenColumns;
                    frm.ShowDialog();
                }

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

            txtInvoiceAmount.Text =string.Format("{0:£ #.##}",grdLister.Rows.Where(c=>c.Cells[COLS.PaymentTypeId].Value.ToInt()!=6)
                                                    .Sum(c => c.Cells[COLS.Total].Value.ToDecimal()));

        }

        private void ClearOrderNo()
        {
            ddlOrderNo.DataSource = null;

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



                    if (orderNo && obj.HasSingleOrderNo.ToBool()==false)
                    {

                        var list = General.GetQueryable<Booking>(c => c.CompanyId == obj.Id && (c.OrderNo != null && c.OrderNo != ""))
                                       .Select(args => new { Id = args.OrderNo, OrderNo = args.OrderNo }).Distinct().ToList();

                        ComboFunctions.FillCombo(list, ddlOrderNo, "OrderNo", "Id");

                        ddlOrderNo.Visible = true;
                        label5.Visible = true;
                    }
                    else
                    {
                        ddlOrderNo.Visible = false;
                        label5.Visible = false;
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

            frmInvoiceReport frm = new frmInvoiceReport();
           // frm.HasSplitByDept = chkSplitByDept.Checked;
            frm.HasSplitByField = ddlSplitBy.Text;
            frm.ObjInvoice = objMaster.Current;
            var list = General.GetQueryable<vu_Invoice>(a => a.Id == id).OrderBy(c=>c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;

           

            frm.GenerateReport();


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmInvoiceReport1");

            if (doc != null)
            {
                doc.Close();
            }

            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
          //  MainMenuForm.MainMenuFrm.ShowForm(frm);
        }

        private void ExportReport(string exportTo)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            frmInvoiceReport frm = new frmInvoiceReport();
           // frm.HasSplitByDept = chkSplitByDept.Checked;
            frm.HasSplitByField = ddlSplitBy.Text;
            frm.ObjInvoice = objMaster.Current;

            frm.ExportFileType = exportTo;
            var list = General.GetQueryable<vu_Invoice>(a => a.Id == id).OrderBy(c => c.PickupDate).ToList();
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
            txtInvoiceAmount.Text = string.Empty;
            ddlCompany.Enabled = true;
            btnCreditNote.BackColor = Color.AliceBlue;

        }

        private void chkDepartmentWise_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (args.NewValue == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlDepartment.Enabled = true;
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

            frmInvoiceReport frm = new frmInvoiceReport();
          
            frm.HasSplitByField = ddlSplitBy.Text;
            frm.ObjInvoice = objMaster.Current;

            var list = General.GetQueryable<vu_Invoice>(a => a.Id == id).OrderBy(c=>c.PickupDate).ToList();
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

            btnAddNewInvoice.Location = new Point(this.Width - 185, 0);
            btnAddNewInvoice.BringToFront();
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
            txtInvoiceAmount.Text = string.Empty;
            ddlCompany.Enabled = true;
            if (chkAutoContinue.Visible == true && chkAutoContinue.Checked)
            {
                int Index=ddlDepartment.SelectedIndex.ToInt();
                Index=(Index+1);
                ddlDepartment.SelectedIndex = Index;
                AutoPickUp();
            }
            else
            {
                chkDepartmentWise.Checked = false; 
            }

            btnCreditNote.BackColor = Color.AliceBlue;

        }

        private void chkAutoContinue_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkAutoContinue.Checked)
            {
                ddlDepartment.Enabled = true;
            }
            else
            {
                ddlDepartment.SelectedValue = null;
            }
        }

        private void chkAllFromDate_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                dtpFromDate.Enabled = false;

            }
            else
            {

                dtpFromDate.Enabled = true;
            }
        }

        private void btnCreditNote_Click(object sender, EventArgs e)
        {
            try
            {
                frmInvoiceCreditNotes frm = new frmInvoiceCreditNotes();
                frm.OnDisplayRecord(objMaster.Current.Id);
                frm.ShowDialog();


                frm.Dispose();
            }
            catch (Exception ex)
            {


            }
        }

        private void deleteCreditNote_Click(object sender, EventArgs e)
        {
            using (TaxiDataContext db = new TaxiDataContext())
            {

                var objNote = db.InvoiceCreditNotes.FirstOrDefault(c => c.InvoiceId == objMaster.Current.Id);


                if (objNote!=null)
                {

                    db.InvoiceCreditNotes.DeleteOnSubmit(objNote);
                    db.SubmitChanges();
                    btnCreditNote.BackColor = Color.AliceBlue;

                }

            }

        }
    }
}
