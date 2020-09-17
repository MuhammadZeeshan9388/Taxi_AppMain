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
using System.Linq.Expressions;
using Telerik.WinControls.Docking;
using System.Collections;
using Taxi_AppMain;

namespace Taxi_AppMain
{
    public partial class frmCustomerInvoice : UI.SetupBase
    {
        InvoiceBO objMaster = null;
        public struct COLS
        {
            public static string ID = "ID";
            public static string InvoiceId = "InvoiceId";
            public static string BookingId = "BookingId";

            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
       //     public static string OrderNo = "OrderNo";
       //     public static string PupilNo = "PupilNo";

            public static string RefNumber = "RefNumber";

       //     public static string Account = "A/C";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

            public static string Charges = "Charges";

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string Total = "Total";
            public static string PaymentTypeId = "PaymentTypeId";

        }
        public frmCustomerInvoice()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmInvoice_Load);
            InitializeConstructor();
        }

        private void InitializeConstructor()
        {
            try
            {
              //  ComboFunctions.FillMultiColumnCustomerCombo(ddlCustomer);
                //ddlCustomer.MultiColumnComboBoxElement.DropDownWidth = 500;
                //ddlCustomer.EditorControl.AutoSizeRows = false;
                //ddlCustomer.EditorControl.BestFitColumns();
                //ddlCustomer.EditorControl.ColumnWidthChanged += new ColumnWidthChangedEventHandler(EditorControl_ColumnWidthChanged);

                //ddlCustomer.SelectedValue = null;
                //dtpInvoiceDate.Value = DateTime.Now.ToDate();
                FormatChargesGrid();

                grdLister.AutoCellFormatting = false;
                grdLister.ShowGroupPanel = false;
             //   grdLister.AutoCellFormatting = true;
                grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                grdLister.ShowRowHeaderColumn = false;

                objMaster = new InvoiceBO();
                this.SetProperties((INavigation)objMaster);

                grdLister.AllowAddNewRow = false;


                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());



                grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        void EditorControl_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            try
            {
                MasterGridViewTemplate template = (MasterGridViewTemplate)sender;
                template.Columns["Id"].IsVisible = false;
                template.Columns["Name"].Width = 80;
                template.Columns["Email"].IsVisible = false;
            }
            catch (Exception ex)
            {

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
                        decimal fare = row.Cells[COLS.Charges].Value.ToDecimal();
                        decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                        decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                        decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                        decimal meetAndGreet = row.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                        decimal CongtionCharge = row.Cells[COLS.CongtionCharge].Value.ToDecimal();
                        decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();
                        string pickup = row.Cells[COLS.PickupPoint].Value.ToStr();
                        string destination = row.Cells[COLS.Destination].Value.ToStr();




                        BookingBO objMaster = new BookingBO();
                        objMaster.GetByPrimaryKey(id);

                        if (objMaster.Current != null)
                        {
                            objMaster.Current.FareRate = fare;
                            objMaster.Current.CustomerPrice = fare;
                            objMaster.Current.ParkingCharges = parking;
                            objMaster.Current.WaitingCharges = waiting;
                            objMaster.Current.ExtraDropCharges = extraDrop;
                            objMaster.Current.MeetAndGreetCharges = meetAndGreet;
                            objMaster.Current.CongtionCharges = CongtionCharge;
                            objMaster.Current.TotalCharges = TotalCharges;
                            objMaster.Current.FromAddress = pickup;
                            objMaster.Current.ToAddress = destination;
                            objMaster.CheckDataValidation = false;
                            objMaster.CheckCustomerValidation = false;
                            objMaster.Save();


                        }


                    }


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

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
            col.HeaderText = "Job #";
            col.Name = "RefNumber";
            grdLister.Columns.Add(col);

            //col = new GridViewTextBoxColumn();
            // col.IsVisible = false;

            //col.HeaderText = "Order No";
            //col.Name = "OrderNo";
           
            //grdLister.Columns.Add(col);


            //col = new GridViewTextBoxColumn();
            //col.IsVisible = false;
            //col.HeaderText = "Pupil No";
            //col.Name = "PupilNo";
            //grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.HeaderText = "Vehicle";
            col.Name = "Vehicle";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            //col = new GridViewTextBoxColumn();
            ////  col.IsVisible = false;
            //col.ReadOnly = true;
            //col.Name = COLS.Account;
            //col.HeaderText = COLS.Account;
            //grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.ReadOnly = false;
            col.HeaderText = "Pickup Point";
            col.Name = "PickupPoint";
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            //     col.IsVisible = false;
            col.ReadOnly = false;
            col.HeaderText = "Destination";
            col.Name = "Destination";
            grdLister.Columns.Add(col);





            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Charges";
            colD.Name = "Charges";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Parking";
            colD.Name = "Parking";
            colD.Maximum = 9999999;
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
            colD.Maximum = 9999999;
            colD.IsVisible = false;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Total";
            colD.Name = "Total";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
           // colD.Expression = "Charges+Parking+Waiting+ExtraDrop+MeetAndGreet+CongtionCharge";
            colD.Expression = "Charges+Parking+Waiting+ExtraDrop";
            grdLister.Columns.Add(colD);

            //     grdLister.Columns["Id"].IsVisible = false;

            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdLister.Columns["PickUpDate"].Width = 105;
            grdLister.Columns["RefNumber"].Width = 40;
            grdLister.Columns["Vehicle"].Width = 50;
       //     grdLister.Columns[COLS.Account].Width = 60;
            grdLister.Columns["PickUpPoint"].Width = 170;
            grdLister.Columns["Destination"].Width = 170;

            grdLister.Columns["Charges"].Width = 50;
            grdLister.Columns["Parking"].Width = 45;
            grdLister.Columns["Waiting"].Width = 50;
            grdLister.Columns["ExtraDrop"].Width = 60;
            grdLister.Columns["MeetAndGreet"].Width = 50;
            grdLister.Columns["CongtionCharge"].Width = 60;
            grdLister.Columns["Total"].Width = 45;
      //      grdLister.Columns["OrderNo"].Width = 55;

            grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            grdLister.Columns["RefNumber"].HeaderText = "Ref #";
            grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
            grdLister.Columns["ExtraDrop"].HeaderText = "Congestion";

            grdLister.Columns["MeetAndGreet"].HeaderText = "M & G";
            grdLister.Columns["CongtionCharge"].HeaderText = "Congestion";

            //grdLister.Columns["MeetAndGreet"].ReadOnly = false;
            //grdLister.Columns["CongtionCharge"].ReadOnly = false;
            //grdLister.Columns["Charge"].ReadOnly = false;
            //grdLister.Columns["Parking"].ReadOnly = false;
            //grdLister.Columns["Waiting"].ReadOnly = false;
            //grdLister.Columns["ExtraDrop"].ReadOnly = false;

            AddUpdateColumn(grdLister);


            ConditionalFormattingObject objPaid = new ConditionalFormattingObject();
            objPaid.ApplyToRow = true;
            objPaid.RowBackColor = Color.LightGreen;
            objPaid.ConditionType = ConditionTypes.Equal;
            objPaid.TValue1 = "6";
            objPaid.TValue2 = "2";
            grdLister.Columns["PaymentTypeId"].ConditionalFormattingObjectList.Add(objPaid);
           


        }

        protected override void OnClosed(EventArgs e)
        {
          //  General.RefreshListWithoutSelected<frmCustomerInvoiceList>("frmCustomerInvoiceList1");

        }


        public override void Save()
        {

            OnSave();
        }


        private int? GetCustomerId()
        {
         
            CustomerBO objCustomer = new CustomerBO();
            int? rtnCustomerId = null;
            try
            {


                if (objMaster.PrimaryKeyValue == null)
                {



                    objCustomer.New();
                    objCustomer.Current.Name = txtCustomerName.Text.Trim();
                    objCustomer.Current.MobileNo = txtMobileNo.Text.Trim();
                    objCustomer.Current.TelephoneNo = txtTelephoneNo.Text.Trim();
                    objCustomer.Current.Address1 = txtAddress1.Text.Trim();
                    objCustomer.Current.Email = txtEmail.Text.Trim();
                    objCustomer.Current.BlackList = false;
                    objCustomer.Current.BlackListResion = string.Empty;
                    objCustomer.Current.DoorNo = txtDoorNo.Text.Trim();
                    objCustomer.Current.TotalCalls = 0;
                    objCustomer.Current.AddOn = DateTime.Now;

                    objCustomer.CheckDataValidation = false;
                    objCustomer.Save();

                }
                else
                {
                    objCustomer.GetByPrimaryKey(objMaster.Current.CustomerId);
                    objCustomer.Edit();
                    objCustomer.Current.Name = txtCustomerName.Text.Trim();
                    objCustomer.Current.MobileNo = txtMobileNo.Text.Trim();
                    objCustomer.Current.TelephoneNo = txtTelephoneNo.Text.Trim();
                    objCustomer.Current.Address1 = txtAddress1.Text.Trim();
                    objCustomer.Current.Email = txtEmail.Text.Trim();
                    objCustomer.Current.BlackList = false;
                    objCustomer.Current.BlackListResion = string.Empty;
                    objCustomer.Current.DoorNo = txtDoorNo.Text.Trim();
                    objCustomer.Current.TotalCalls = 0;
                    objCustomer.Current.AddOn = DateTime.Now;

                    objCustomer.CheckDataValidation = false;
                    objCustomer.Save();


                }

                rtnCustomerId = objMaster.PrimaryKeyValue == null ? objCustomer.PrimaryKeyValue.ToIntorNull() : objMaster.Current.CustomerId;


            }
            catch
            {


            }



                 return rtnCustomerId;
        }

        private void OnSave()
        {

            try
            {
                

                int? customerId=  GetCustomerId();


                if (customerId == null)
                {
                    MessageBox.Show("Problem on Saving Invoice. Please try again");
                    return;


                }
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();



                }
                else
                {
                    objMaster.Edit();


                    if (customerId == null)
                        customerId = objMaster.Current.CustomerId;
                }


                objMaster.Current.InvoiceDate = dtpInvoiceDate.Value.ToDate();



                objMaster.Current.CustomerId = customerId;
              

                objMaster.Current.InvoiceTypeId = Enums.INVOICE_TYPE.CUSTOMER;

            //    objMaster.Current.InvoiceTotal = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());

                objMaster.Current.InvoiceTotal = grdLister.Rows.Where(c => c.Cells[COLS.PaymentTypeId].Value.ToInt() != 6)
                                  .Sum(c => c.Cells[COLS.Total].Value.ToDecimal());



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
                    MessageBox.Show(objMaster.ShowErrors());
                else
                {
                    MessageBox.Show(ex.Message);

                }
            }

        }


        public override void DisplayRecord()
        {
            try
            {
                if (objMaster.Current == null) return;

             //   btnRefresh.Visible = false;
                btnCustomerLister.Visible = false;
                btnExportPDF.Enabled = true;
                btnPrint.Enabled = true;
                btnSendEmail.Enabled = true;

                txtInvoiceNo.Text = objMaster.Current.InvoiceNo.ToStr();
                dtpInvoiceDate.Value = objMaster.Current.InvoiceDate.ToDate();
               // ddlCustomer.SelectedValue = objMaster.Current.CustomerId;

                Customer objCustomer=objMaster.Current.Customer;

                if(objCustomer!=null)
                {

                    txtCustomerName.Text = objCustomer.Name.ToStr().Trim();
                    txtDoorNo.Text = objCustomer.DoorNo.ToStr().Trim();
                    txtAddress1.Text = objCustomer.Address1.ToStr().Trim();
                    txtTelephoneNo.Text = objCustomer.TelephoneNo.ToStr().Trim();
                    txtMobileNo.Text = objCustomer.MobileNo.ToStr().Trim();
                    txtEmail.Text = objCustomer.Email.ToStr().Trim();


                }


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

                        grdLister.Rows[i].Cells[COLS.PickupDate].Value = objBooking.PickupDateTime;
                        //    grdLister.Rows[i].Cells[COLS.OrderNo].Value = objBooking.OrderNo;
                        //  grdLister.Rows[i].Cells[COLS.PupilNo].Value = objBooking.PupilNo;
                        grdLister.Rows[i].Cells[COLS.Vehicle].Value = objBooking.Fleet_VehicleType.VehicleType;
                        grdLister.Rows[i].Cells[COLS.RefNumber].Value = objBooking.BookingNo;
                        grdLister.Rows[i].Cells[COLS.Charges].Value = objBooking.CustomerPrice.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.Parking].Value = objBooking.ParkingCharges.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.PickupPoint].Value = objBooking.FromAddress.ToStr();
                        grdLister.Rows[i].Cells[COLS.Destination].Value = objBooking.ToAddress.ToStr();
                        grdLister.Rows[i].Cells[COLS.Waiting].Value = objBooking.WaitingCharges.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = objBooking.ExtraDropCharges.ToDecimal();
                        //  grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = objBooking.MeetAndGreetCharges.ToDecimal();
                        //   grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = objBooking.CongtionCharges.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.Total].Value = objBooking.TotalCharges.ToDecimal();

                        grdLister.Rows[i].Cells[COLS.PaymentTypeId].Value = objBooking.PaymentTypeId.ToInt();

                        //     grdLister.Rows[i].Cells[COLS.Account].Value = objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr().Trim();
                    }

                }


                grdLister.CurrentRow = null;

                txtInvoiceAmount.Text = objMaster.Current.InvoiceTotal.ToDecimal().ToStr();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

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

        private bool IsFormLoaded = false;
        void frmInvoice_Load(object sender, EventArgs e)
        {
            btnSaveAndClose.Visible = false;

            IsFormLoaded = true;
            //PaymentTypes();

            //if (objMaster.Current != null)
            //{

            //    ddlCustomer.SelectedValue = objMaster.Current.CustomerId;
            //}
            //else
            //{
            //    ddlCustomer.SelectedValue = null;
            //    ddlCustomer.Text = string.Empty;
            //    ddlCustomer.SelectedIndex = -1;

            //}
        }
        private void PaymentTypes()
        {
            ddlPaymentType.Items.Add("Cash");
            ddlPaymentType.Items.Add("Credit Card");
            ddlPaymentType.Items.Add("Both");
        }
        private void btnPickBooking_Click(object sender, EventArgs e)
        {
            try
            {
            
                string telNo = txtTelephoneNo.Text.Trim();
                string mobNo = txtMobileNo.Text.Trim();


                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();
                int PaymentTypeIndex = ddlPaymentType.SelectedIndex;

                string error = string.Empty;
                //if (customerId == 0)
                //{
                //    error += "Required : Customer";
                //}

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
                    MessageBox.Show(error);
                    return;

                }



                string[] hiddenColumns = new string[] {  "Id", "CompanyId","CompanyName","Parking","Waiting","ExtraDrop","MeetAndGreet","Congtion",
                                                "Total","OrderNo","PupilNo","BookingDate","PaymentTypeId"};


                Expression<Func<Booking, bool>> _condition = null;


                int filterBy = ddlFilterBy.SelectedIndex;


                if (filterBy == 0)
                {


                    //NC
                    // select (a.PostCode != string.Empty ? a.LocationName + ", " + a.PostCode : a.LocationName)
                    _condition=(c=>   (telNo == string.Empty || c.CustomerPhoneNo == telNo) && (mobNo == string.Empty || c.CustomerMobileNo == mobNo)
                   // &&(PaymentTypeIndex==0 ? (c.PaymentTypeId==Enums.PAYMENT_TYPES.CASH || c.PaymentTypeId==Enums.PAYMENT_TYPES.CREDIT_CARD): PaymentTypeIndex==1 ? (c.PaymentTypeId==Enums.PAYMENT_TYPES.CASH): PaymentTypeIndex==2 ?(c.PaymentTypeId==Enums.PAYMENT_TYPES.CREDIT_CARD):PaymentTypeIndex==3)  
                    && (((PaymentTypeIndex==-1 || PaymentTypeIndex==2) &&(c.PaymentTypeId==Enums.PAYMENT_TYPES.CASH || c.PaymentTypeId==Enums.PAYMENT_TYPES.CREDIT_CARD))
                    || (PaymentTypeIndex==0 && (c.PaymentTypeId==Enums.PAYMENT_TYPES.CASH))
                    || (PaymentTypeIndex==1 && (c.PaymentTypeId==Enums.PAYMENT_TYPES.CREDIT_CARD)))
                    && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED));
                
                }
                else if (filterBy == 1)
                {


                    _condition = (c => (c.CompanyId == null || (c.Gen_Company.SysGenId != null && c.Gen_Company.SysGenId == 2) || c.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                                                                                       && (telNo == string.Empty || c.CustomerPhoneNo == telNo) && (mobNo == string.Empty || c.CustomerMobileNo == mobNo)
                                               && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED));

                }
                





                List<object[]> list = ShowCustomerInvoiceBookingMultiLister(_condition
                                                                       , c=>c.InvoiceId!=0,
                                                                       hiddenColumns, fromDate, tillDate);
                GridViewRowInfo row;
                foreach (object[] obj in list)
                {
                    long bookingId = obj[0].ToLong();

                    if (grdLister.Rows.Count(c => c.Cells[COLS.BookingId].Value.ToLong() == bookingId) > 0)
                        continue;

                    row = grdLister.Rows.AddNew();


                    row.Cells[COLS.BookingId].Value = obj[0].ToLongorNull();
                    row.Cells[COLS.RefNumber].Value = obj[3].ToStr();
                    row.Cells[COLS.PickupDate].Value = obj[2].ToDateTime();


                    row.Cells[COLS.Vehicle].Value = obj[4].ToStr();


                    //    row.Cells[COLS.OrderNo].Value = obj[5].ToStr();
                    //    row.Cells[COLS.PupilNo].Value = obj[6].ToStr();

                    //    row.Cells[COLS.Account].Value = obj[11].ToStr();


                    row.Cells[COLS.PickupPoint].Value = obj[8].ToStr();
                    row.Cells[COLS.Destination].Value = obj[9].ToStr();
                    row.Cells[COLS.Charges].Value = obj[10].ToDecimal();
                    row.Cells[COLS.Parking].Value = obj[13].ToDecimal();
                    row.Cells[COLS.Waiting].Value = obj[14].ToDecimal();
                    row.Cells[COLS.ExtraDrop].Value = obj[15].ToDecimal();
                    row.Cells[COLS.MeetAndGreet].Value = obj[16].ToDecimal();
                    row.Cells[COLS.CongtionCharge].Value = obj[17].ToDecimal();
                    row.Cells[COLS.Total].Value = obj[18].ToDecimal();


                    row.Cells[COLS.PaymentTypeId].Value = obj[19].ToInt();

                }


               
                CalculateTotal();

                grdLister.CurrentRow = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        private  List<object[]> ShowCustomerInvoiceBookingMultiLister(Expression<Func<Booking, bool>> _condition, Expression<Func<Invoice_Charge, bool>> _invoiceCondition, string[] hiddenColumns, DateTime? fromDate, DateTime? tillDate)
        {
            Taxi_Model.TaxiDataContext db = new TaxiDataContext();

            var list1 = db.GetTable<Booking>().Where(_condition).Where(b => b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate).AsEnumerable();
            var list2 = db.GetTable<Invoice_Charge>().Where(_invoiceCondition);

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
                            PickupPoint = b.FromAddress,
                            Destination = b.ToAddress,
                            Charges = b.CustomerPrice.ToDecimal(),

                            CompanyId = b.CompanyId,
                            CompanyName = b.CompanyId != null ? b.Gen_Company.CompanyName : "",
                            Parking = b.ParkingCharges.ToDecimal(),
                            Waiting = b.WaitingCharges.ToDecimal(),
                            ExtraDrop = b.ExtraDropCharges.ToDecimal(),
                            MeetAndGreet = b.MeetAndGreetCharges.ToDecimal(),
                            Congtion = b.CongtionCharges.ToDecimal(),
                            Total = b.TotalCharges.ToDecimal(),
                            PaymentTypeId = b.PaymentTypeId


                        }).ToList();


            frmLister frm = new frmLister(list, "Id", true, hiddenColumns);


            frm.HiddenColumns = hiddenColumns;
            frm.ShowDialog();

            return frm.ListofData;

        }

        private void CalculateTotal()
        {

       //     txtInvoiceAmount.Text =string.Format("{0:£ #.##}",grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal()));
            txtInvoiceAmount.Text = string.Format("{0:£ #.##}", grdLister.Rows.Where(c => c.Cells[COLS.PaymentTypeId].Value.ToInt() != 6)
                                        .Sum(c => c.Cells[COLS.Total].Value.ToDecimal()));


        }

      



        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }
        public override void OnNew()
        {
            objMaster.PrimaryKeyValue = null;
            objMaster.Clear();

            grdLister.Rows.Clear();
            txtMobileNo.Text = string.Empty;
            txtTelephoneNo.Text = string.Empty;
            txtAddress1.Text = string.Empty;
           
        }

        public override void Print()
        {


            try
            {
                if (objMaster.Current == null || objMaster.Current.Id == 0) return;
                long id = objMaster.Current.Id;

                rptfrmCustomerInvoice frm = new rptfrmCustomerInvoice(id);


                var list = General.GetQueryable<vu_Invoice>(a => a.Id == id).ToList();
                int count = list.Count;

                frm.DataSource = list;


                frm.GenerateReport();


                Telerik.WinControls.UI.Docking.DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmCustomerInvoice1");

                if (doc != null)
                {
                    doc.Close();
                }

                UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
                //  MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


       

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            rptfrmCustomerInvoice frm = new rptfrmCustomerInvoice();


            var list = General.GetQueryable<vu_Invoice>(a => a.Id == id).ToList();
            int count = list.Count;

            frm.DataSource = list;


            frm.GenerateReport();

            frm.ExportReport(objMaster.Current.InvoiceNo);
    
        }

      

        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomerName.Text.Trim()))
            {
                MessageBox.Show("Required : Customer Name");
                return;
            }


            if (string.IsNullOrEmpty(txtMobileNo.Text.Trim()) && string.IsNullOrEmpty(txtTelephoneNo.Text.Trim()))
            {
                MessageBox.Show("Required : Telephone or Mobile No");
                return;

            }

            if (string.IsNullOrEmpty(txtDoorNo.Text.Trim()))
            {
                MessageBox.Show("Required : Door No");
                return;

            }


            if (string.IsNullOrEmpty(txtAddress1.Text.Trim()))
            {
                MessageBox.Show("Required : Address");
                return;

            }

            OnSave();
        }

       

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            rptfrmCustomerInvoice frm = new rptfrmCustomerInvoice();


            var list = General.GetQueryable<vu_Invoice>(a => a.Id == id).ToList();
            int count = list.Count;

            frm.DataSource = list;


            frm.GenerateReport();

            frm.SendEmail(objMaster.Current.InvoiceNo, EmailAddress);
        }

        private void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private string _EmailAddress;

        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        private void btnCustomerLister_Click(object sender, EventArgs e)
        {
            try
            {
                string mobNo = txtMobileNo.Text.Trim();
                string telNo = txtTelephoneNo.Text.Trim();

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    var list =db.GetTable<Customer>().Where(c => c.Name != "" && (c.MobileNo == mobNo || mobNo == "") && (c.TelephoneNo == telNo || telNo == ""))

                          .Select(args => new
                              {
                                  args.Id,
                                  args.Name,
                                  args.DoorNo,
                                  args.Address1,
                                  args.TelephoneNo,
                                  args.MobileNo,
                                  
                               

                              }).OrderBy(c => c.Name).ToList();

                    object[] obj = ShowFormLister(list, "Id");


                    if (obj != null)
                    {

                        txtCustomerName.Text = obj[2].ToStr();
                      
                        txtDoorNo.Text = obj[3].ToStr();
                        txtAddress1.Text = obj[4].ToStr();
                       // ddlCustomer.SelectedValue = obj[1].ToInt();
                        //txtAddress1.Text = obj[2].ToStr();
                        //txtTelephoneNo.Text = obj[3].ToStr().Trim();
                        //txtMobileNo.Text = obj[4].ToStr().Trim();

                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }


        }

        private  object[] ShowFormLister(IList list, string pkColumn)
        {

            frmLister frm = new frmLister(list, pkColumn, false);

            frm.ShowDialog();


            return frm.RowData;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
          //  ComboFunctions.FillMultiColumnCustomerCombo(ddlCustomer);
        }

        private void f_Paint(object sender, PaintEventArgs e)
        {

        }

     

    }
}
