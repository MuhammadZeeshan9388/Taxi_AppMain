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

namespace Taxi_AppMain
{
    public partial class frmInvoiceCourier : UI.SetupBase
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

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string RemovalDescription = "RemovalDescription";
            public static string Total = "Total";

            public static string Payment_ID = "Payment_ID";

            public static string PaymentTypeId = "PaymentTypeId";

        }
        public frmInvoiceCourier()
        {
            InitializeComponent();
            InitializeConstructor();
           
           
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
            catch (Exception ex)
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


            ComboFunctions.FillCompanyCombo(ddlCompany);
            ComboFunctions.FillBookingTypeCombo(ddlBookingType);
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
                        decimal fare = row.Cells[COLS.Charges].Value.ToDecimal();
                        decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                        decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                        decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                        decimal meetAndGreet = row.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                        decimal CongtionCharge = row.Cells[COLS.CongtionCharge].Value.ToDecimal();
                        decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();
                        string Destination = row.Cells[COLS.Destination].Value.ToStr();
                        string PickupPoint = row.Cells[COLS.PickupPoint].Value.ToStr();
                        string Passenger = row.Cells[COLS.Passenger].Value.ToStr();
                   //     int? VehicleID = row.Cells[COLS.VehicleID].Value.ToInt();
                        int? invoicepaymentId = row.Cells[COLS.Payment_ID].Value.ToIntorNull();

                        BookingBO objMaster = new BookingBO();
                        objMaster.GetByPrimaryKey(id);

                        if (objMaster.Current != null)
                        {
                            objMaster.Current.CompanyPrice = fare;
                            objMaster.Current.ParkingCharges = parking;
                            objMaster.Current.WaitingCharges = waiting;
                            objMaster.Current.ExtraDropCharges = extraDrop;
                            objMaster.Current.MeetAndGreetCharges = meetAndGreet;
                            objMaster.Current.CongtionCharges = CongtionCharge;
                            objMaster.Current.TotalCharges = TotalCharges;
                            objMaster.Current.InvoicePaymentTypeId = invoicepaymentId;
                            if (Destination == "")
                            {
                                RadMessageBox.Show("Requried: Destination");
                            }
                            else if (PickupPoint == "")
                            {
                                RadMessageBox.Show("Requried: PickupPoint");
                            }
                            else
                            {
                                objMaster.Current.ToAddress = Destination;
                                objMaster.Current.FromAddress = PickupPoint;
                                objMaster.Current.CustomerName = Passenger;
                             //   objMaster.Current.VehicleTypeId = VehicleID;
                                objMaster.Save();
                            }

                            CalculateTotal();
                        }
                    }
                }
            }
            catch (Exception ex)
            {



            }
           
        }
        


        private void FormatChargesGrid()
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
            col.Name =COLS.BookingId;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
            colDt.Name = "PickupDate";
            colDt.ReadOnly = true;
            colDt.HeaderText = "Pickup Date-Time";
            grdLister.Columns.Add(colDt);



            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.ReadOnly = true;
            col.Width = 100;
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
            // col.IsVisible = false;
            col.HeaderText = "Vehicle";
            col.Name = "Vehicle";            
            grdLister.Columns.Add(col);


            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            colCombo.Name = COLS.VehicleID;
            colCombo.IsVisible = false;
            colCombo.HeaderText = "Vehicle";
            colCombo.DataSource = General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo).ToList();
            colCombo.DisplayMember = "VehicleType";
            colCombo.ValueMember = "Id";
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            grdLister.Columns.Add(colCombo);
            colCombo.ReadOnly = false;
            


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


            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.IsVisible = true;
            colD.HeaderText = "Charges";
            colD.Name = "Charges";
            colD.IsVisible = false;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);

           


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.IsVisible = false;
            colD.HeaderText = "Waiting";
            colD.Name = "Waiting";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.IsVisible = false;
            colD.HeaderText = "Extra Drop";
            colD.Name = "ExtraDrop";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Meet and Greet";
            colD.Name = "MeetAndGreet";
            colD.IsVisible = false;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Congestion";
            colD.Name = "CongtionCharge";
            colD.IsVisible = false;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.Width = 130;
            colD.ReadOnly = true;
            colD.HeaderText = "Total";
            colD.Name = "Total";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.Expression = "Charges+Waiting";
            grdLister.Columns.Add(colD);


            GridViewComboBoxColumn colPayment = new GridViewComboBoxColumn();
            colPayment.IsVisible = false;
            colPayment.Name = COLS.Payment_ID;
            colPayment.HeaderText = "Status";
            colPayment.DataSource = General.GetQueryable<Invoice_PaymentType>(null).Where(c=>c.Id == 1 || c.Id == 3).OrderBy(c => c.Id).ToList();
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
            grdLister.Columns[COLS.Passenger].Width = 60;
            grdLister.Columns["PickUpPoint"].Width = 200;
            grdLister.Columns["Destination"].Width = 200;

            grdLister.Columns["Charges"].Width = 50;
         //   grdLister.Columns["Parking"].Width = 45;
            grdLister.Columns["Waiting"].Width = 50;
            grdLister.Columns["ExtraDrop"].Width = 60;
         //   grdLister.Columns["MeetAndGreet"].Width = 50;
        //    grdLister.Columns["CongtionCharge"].Width = 60;
            grdLister.Columns["Total"].Width = 50;
            grdLister.Columns["OrderNo"].Width = 55;

            grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            grdLister.Columns["RefNumber"].HeaderText = "Ref #";
            grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
         //   grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";

          //  grdLister.Columns["MeetAndGreet"].HeaderText = "M & G";
         //   grdLister.Columns["CongtionCharge"].HeaderText = "Congestion";
            grdLister.Columns["Payment_ID"].Width = 70;
            


            AddUpdateColumn(grdLister);


            ConditionalFormattingObject objPaid = new ConditionalFormattingObject();
            objPaid.ApplyToRow = true;
            objPaid.RowBackColor = Color.LightGreen;
            objPaid.ConditionType = ConditionTypes.Equal;
            objPaid.TValue1 = "6";
            objPaid.TValue2 = "6";
            grdLister.Columns["PaymentTypeId"].ConditionalFormattingObjectList.Add(objPaid);
           






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
                }
                else
                {
                    objMaster.Edit();
                }

                objMaster.Current.BookingTypeId = ddlBookingType.SelectedValue.ToIntorNull();
                objMaster.Current.InvoiceDate = dtpInvoiceDate.Value.ToDate();
                objMaster.Current.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                objMaster.Current.DepartmentId = ddlDepartment.SelectedValue.ToLongorNull();
                objMaster.Current.DepartmentWise = chkDepartmentWise.Checked;

                objMaster.Current.CostCenterId=ddlCostCenter.SelectedValue.ToIntorNull();
                objMaster.Current.CostCenterWise=chkCostCenterWise.Checked;
                objMaster.Current.DueDate = dtpDueDate.Value.ToDate();

                objMaster.Current.InvoiceNo = txtInvoiceNo.Text.Trim();
                objMaster.Current.InvoiceTypeId = Enums.INVOICE_TYPE.ACCOUNT;

                objMaster.Current.InvoiceTotal = grdLister.Rows.Where(c=>c.Cells[COLS.PaymentTypeId].Value.ToInt()!=6)
                                    .Sum(c => c.Cells[COLS.Total].Value.ToDecimal());



            //   var distinctRows= grdLister.Rows.Select(c => c.Cells[COLS.BookingId].ToLong()).Distinct();


               
             


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

            
            if (objMaster.Current == null) return;

            btnExportExcel.Enabled = true;
            btnExportPDF.Enabled = true;
            btnPrint.Enabled = true;
            btnSendEmail.Enabled = true;

            ddlBookingType.SelectedValue = objMaster.Current.BookingTypeId;

            txtInvoiceNo.Text = objMaster.Current.InvoiceNo.ToStr();
            dtpInvoiceDate.Value = objMaster.Current.InvoiceDate.ToDate();



            if (objMaster.Current.CompanyId != null && objMaster.Current.Gen_Company.IsClosed.ToBool())
            {

                var data = (List<Gen_Company>)ddlCompany.DataSource;
                data.Add(objMaster.Current.Gen_Company);
                ComboFunctions.FillCompanyCombo(ddlCompany, data);
            }

            ddlCompany.SelectedValue = objMaster.Current.CompanyId;
            dtpDueDate.Value = objMaster.Current.DueDate.ToDate();
            ddlDepartment.SelectedValue = objMaster.Current.DepartmentId;
            chkDepartmentWise.Checked = objMaster.Current.DepartmentWise.ToBool();

            ddlCostCenter.SelectedValue=objMaster.Current.CostCenterId;
            chkCostCenterWise.Checked=objMaster.Current.CostCenterWise.ToBool();

            int cnt = objMaster.Current.Invoice_Charges.Count;
            var list = objMaster.Current.Invoice_Charges;
            grdLister.RowCount = cnt;
            Booking objBooking = null;
            for (int i = 0; i < cnt; i++)
            {
                grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                grdLister.Rows[i].Cells[COLS.InvoiceId].Value = list[i].InvoiceId;
                grdLister.Rows[i].Cells[COLS.BookingId].Value = list[i].BookingId;

                objBooking = list[i].Booking;

                if (objBooking != null)
                {

                    grdLister.Rows[i].Cells[COLS.Payment_ID].Value = objBooking.InvoicePaymentTypeId;
                    grdLister.Rows[i].Cells[COLS.PickupDate].Value = objBooking.PickupDateTime;
                    grdLister.Rows[i].Cells[COLS.OrderNo].Value = objBooking.OrderNo;
                    grdLister.Rows[i].Cells[COLS.PupilNo].Value = objBooking.PupilNo;

                    grdLister.Rows[i].Cells[COLS.BookedBy].Value = objBooking.Gen_Company_Department.DefaultIfEmpty().DepartmentName.ToStr();


                    grdLister.Rows[i].Cells[COLS.Vehicle].Value = objBooking.Fleet_VehicleType.VehicleType;

                    grdLister.Rows[i].Cells[COLS.VehicleID].Value = objBooking.VehicleTypeId;
                    grdLister.Rows[i].Cells[COLS.RefNumber].Value = objBooking.BookingNo;
                    //grdLister.Rows[i].Cells[COLS.Charges].Value = objBooking.FareRate.ToDecimal();
                 //  grdLister.Rows[i].Cells[COLS.Charges].Value = objBooking.Booking_CourierItems.Sum(c=>c.Amount.ToDecimal()).ToDecimal();
           
                    
              //      grdLister.Rows[i].Cells[COLS.Parking].Value = objBooking.ParkingCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.PickupPoint].Value =!string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + " - " + objBooking.FromAddress.ToStr():objBooking.FromAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Destination].Value = !string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + " - " + objBooking.ToAddress.ToStr() : objBooking.ToAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Waiting].Value = objBooking.WaitingCharges.ToDecimal();
               //     grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = objBooking.ExtraDropCharges.ToDecimal();
              //      grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = objBooking.MeetAndGreetCharges.ToDecimal();
             //       grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = objBooking.CongtionCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Total].Value = objBooking.TotalCharges.ToDecimal();


                    grdLister.Rows[i].Cells[COLS.Passenger].Value = objBooking.CustomerName.ToStr().Trim();
                    grdLister.Rows[i].Cells[COLS.PaymentTypeId].Value = objBooking.PaymentTypeId.ToInt();



                }
          
            }


            grdLister.CurrentRow = null;

            txtInvoiceAmount.Text = objMaster.Current.InvoiceTotal.ToDecimal().ToStr();

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
            col.IsVisible = false;
            grid.Columns.Add(col);

        }


        private void btnPickBooking_Click(object sender, EventArgs e)
        {
            try
            {
                int bookingTypeId = ddlBookingType.SelectedValue.ToInt();
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
                                                "Total","OrderNo","PupilNo","BookingDate","Description","Fare","AccountType"};

                

                bool IsDepartmentWise = chkDepartmentWise.Checked;
                bool IsCostCenterWise = chkCostCenterWise.Checked;


                Func<Booking, bool> _conditionDate = null;
                if (ddlPickType.SelectedIndex == 0)
                    _conditionDate = b => b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate;
                else
                    _conditionDate = b => b.BookingDate.Value.Date >= fromDate && b.BookingDate.Value.Date <= tillDate;


                List<object[]> list = General.ShowCourierBookingMultiLister(c => c.CompanyId == companyId && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
                                                                            && ((IsDepartmentWise && c.DepartmentId == departmentId) || (IsDepartmentWise == false))
                                                                              && ((IsCostCenterWise && c.CostCenterId == costcenterId) || (IsCostCenterWise == false))
                                                                       ,
                                                                         a => a.InvoiceId != null,
                                                                        hiddenColumns, _conditionDate);
                GridViewRowInfo row;

                int cnt=list.Count;
                

           

                var  existBookingId=grdLister.Rows.Select(c=>c.Cells[COLS.BookingId].Value.ToLong()).ToList<long>();
           
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
                    row.Cells[COLS.Waiting].Value = list[i][14].ToDecimal();
                    row.Cells[COLS.Total].Value = list[i][19].ToDecimal();


                    row.Cells[COLS.BookedBy].Value = list[i][20].ToStr();


                    row.Cells[COLS.PaymentTypeId].Value = list[i][23].ToInt();

                }

                CalculateTotal();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void CalculateTotal()
        {

            txtInvoiceAmount.Text =string.Format("{0:£ #.##}",grdLister.Rows.Where(c=>c.Cells[COLS.PaymentTypeId].Value.ToInt()!=6)
                                                    .Sum(c => c.Cells[COLS.Total].Value.ToDecimal()));

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
            }
            else
            {
                Gen_Company obj = General.GetObject<Gen_Company>(c => c.Id == companyId);
                if (obj != null)
                {
                    this.companyEmail = obj.Email.ToStr().Trim();
                    FillDepartmentCombo(obj.Id);
                    FillCostCenterCombo(obj.Id);
                    bool orderNo = obj.HasOrderNo.ToBool();
                    bool pupilNo = obj.HasPupilNo.ToBool();

                    bool HasBookedBy = obj.HasBookedBy.ToBool();
                    SetOrderNoColumn(orderNo);
                    SetPupilNoColumn(pupilNo);
                    SetBookedByColumn(HasBookedBy);
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
            ComboFunctions.FillCompanyCombo(ddlCompany);
            chkDepartmentWise.Checked = false;
            chkCostCenterWise.Checked = false;
            grdLister.Rows.Clear();
            txtInvoiceAmount.Text = string.Empty;

        }

        private void chkDepartmentWise_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            if (args.NewValue == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                ddlDepartment.Enabled = true;

            }
            else
            {

                ddlDepartment.Enabled = false;
                ddlDepartment.SelectedValue = null; 
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            frmInvoiceReport frm = new frmInvoiceReport();


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
      //      grdLister.Columns[COLS.CongtionCharge].IsVisible = show;
            grdLister.Columns[COLS.Destination].IsVisible = show;
        //    grdLister.Columns[COLS.ExtraDrop].IsVisible = show;
         //   grdLister.Columns[COLS.MeetAndGreet].IsVisible = show;
            grdLister.Columns[COLS.OrderNo].IsVisible = show;
       //     grdLister.Columns[COLS.Parking].IsVisible = show;
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

       

    }
}
