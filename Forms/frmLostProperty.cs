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
using Telerik.Data;

namespace Taxi_AppMain
{
    public partial class frmLostProperty : UI.SetupBase
    {

        LostPropertyBO objMaster = null;

        public struct COLS
        {
            public static string ID = "ID";
            public static string TransId = "TransId";
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

            public static string Fares = "Fares";

        }
        public frmLostProperty()
        {
            InitializeComponent();
            InitializeConstructor();


        }

        public frmLostProperty(long jobId, Booking objB)
        {
            InitializeComponent();
            InitializeConstructor();



            DisplayJobLostProperty(jobId, objB);

        }

        private void DisplayJobLostProperty(long jobId, Booking objBook)
        {
            try
            {
                btnNew.Enabled = false;
                btnSaveAndNew.Enabled = false;

                grdLister.Enabled = false;
                txtCustomerName.ReadOnly = true;
                txtCustomerMobileNo.ReadOnly = false;


                LostProperty objLost = General.GetQueryable<LostProperty>(c => c.JobId == jobId).OrderByDescending(c => c.LostDate).FirstOrDefault();

                if (objLost != null)
                {

                    objMaster.GetByPrimaryKey(objLost.Id);
                    DisplayRecord();
                }
                else
                {
                    grdLister.RowCount = 1;

                    if (objBook != null)
                    {



                        txtCustomerName.Text = objBook.CustomerName.ToStr();
                        txtCustomerMobileNo.Text = objBook.CustomerMobileNo != string.Empty ? objBook.CustomerMobileNo.ToStr() : objBook.CustomerPhoneNo.ToStr();

                        txtAddressDetail.Text = objBook.Customer.DefaultIfEmpty().Address1.ToStr();

                        grdLister.Rows[0].Cells[COLS.BookingId].Value = jobId;
                        grdLister.Rows[0].Cells[COLS.PickupDate].Value = objBook.PickupDateTime;
                        grdLister.Rows[0].Cells[COLS.OrderNo].Value = objBook.OrderNo;
                        grdLister.Rows[0].Cells[COLS.PupilNo].Value = objBook.PupilNo;

                        grdLister.Rows[0].Cells[COLS.BookedBy].Value = objBook.Gen_Company_Department.DefaultIfEmpty().DepartmentName.ToStr();

                        grdLister.Rows[0].Cells[COLS.Vehicle].Value = objBook.Fleet_VehicleType.VehicleType;

                        grdLister.Rows[0].Cells[COLS.VehicleID].Value = objBook.VehicleTypeId;
                        grdLister.Rows[0].Cells[COLS.RefNumber].Value = objBook.BookingNo;
                        grdLister.Rows[0].Cells[COLS.Charges].Value = objBook.CompanyPrice.ToDecimal();

                        grdLister.Rows[0].Cells[COLS.Fares].Value = objBook.FareRate.ToDecimal();

                        grdLister.Rows[0].Cells[COLS.Parking].Value = objBook.ParkingCharges.ToDecimal();
                        grdLister.Rows[0].Cells[COLS.PickupPoint].Value = !string.IsNullOrEmpty(objBook.FromDoorNo) ? objBook.FromDoorNo + " - " + objBook.FromAddress.ToStr() : objBook.FromAddress.ToStr();
                        grdLister.Rows[0].Cells[COLS.Destination].Value = !string.IsNullOrEmpty(objBook.ToDoorNo) ? objBook.ToDoorNo + " - " + objBook.ToAddress.ToStr() : objBook.ToAddress.ToStr();
                        grdLister.Rows[0].Cells[COLS.Waiting].Value = objBook.WaitingCharges.ToDecimal();
                        grdLister.Rows[0].Cells[COLS.ExtraDrop].Value = objBook.ExtraDropCharges.ToDecimal();
                        grdLister.Rows[0].Cells[COLS.MeetAndGreet].Value = objBook.MeetAndGreetCharges.ToDecimal();
                        grdLister.Rows[0].Cells[COLS.CongtionCharge].Value = objBook.CongtionCharges.ToDecimal();
                        grdLister.Rows[0].Cells[COLS.Total].Value = objBook.TotalCharges.ToDecimal();


                        grdLister.Rows[0].Cells[COLS.Passenger].Value = objBook.CustomerName.ToStr().Trim();
                        grdLister.Rows[0].Cells[COLS.Payment_ID].Value = objBook.InvoicePaymentTypeId.ToIntorNull();
                    }

                }
            }
            catch (Exception ex)
            {


            }



        }


        private void frmDriverRent_Load(object sender, EventArgs e)
        {

        }
        private void frmDriverRent_Shown(object sender, EventArgs e)
        {
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        }
        private void InitializeConstructor()
        {
            //ComboFunctions.FillDriverNoCombo(ddlDriver);
            dtpLPDate.Value = DateTime.Now.ToDate();
            dtpLostDate.Value = DateTime.Now.ToDate();
            FormatChargesGrid();
            grdLister.ShowGroupPanel = false;
            grdLister.AutoCellFormatting = true;
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;


            //  grdLister.Size = new Size(grdLister.Width, 70);
            grdLister.TableElement.RowHeight = 90;

            objMaster = new LostPropertyBO();
            this.SetProperties((INavigation)objMaster);

            grdLister.AllowAddNewRow = false;


            dtpFromDate.Value = DateTime.Now.ToDate().AddDays(-30);
            dtpTillDate.Value = DateTime.Now.ToDate();

            //  grdLister.TableElement.ROWE
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            //txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
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
                    int? VehicleID = row.Cells[COLS.VehicleID].Value.ToInt();
                    int? invoicepaymentId = row.Cells[COLS.Payment_ID].Value.ToInt();

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
                            objMaster.Current.VehicleTypeId = VehicleID;
                            objMaster.Save();
                        }

                        CalculateTotal();
                    }
                }
            }
            else if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
            {
                if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                {
                    RadGridView grid = gridCell.GridControl;
                    grid.CurrentRow.Delete();
                }
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
            col.Name = COLS.TransId;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.BookingId;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
            colDt.Name = "PickupDate";
            colDt.ReadOnly = true;
            col.ReadOnly = true;
            colDt.HeaderText = "Pickup Date-Time";
            grdLister.Columns.Add(colDt);



            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.HeaderText = "Job #";
            col.Name = "RefNumber";
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Order No";
            col.Name = "OrderNo";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Booked By";
            col.Name = COLS.BookedBy;
            col.WrapText = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.HeaderText = "Pupil No";
            col.Name = "PupilNo";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Vehicle";
            col.Name = "Vehicle";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            GridViewComboBoxColumn colCombo = new GridViewComboBoxColumn();
            colCombo.Name = COLS.VehicleID;
            colCombo.HeaderText = "Vehicle";
         //   colCombo.DataSource = General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo).ToList();
         //   colCombo.DisplayMember = "VehicleType";
        ////    colCombo.ValueMember = "Id";
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            colCombo.DataType = typeof(int);
            grdLister.Columns.Add(colCombo);
            colCombo.IsVisible = false;
            colCombo.ReadOnly = true;



            col = new GridViewTextBoxColumn();
            col.Name = COLS.Passenger;
            col.HeaderText = "Passenger";
            col.IsVisible = false;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Width = 900;
            col.IsVisible = false;
            col.ReadOnly = true;
            col.WrapText = true;
            col.Name = COLS.RemovalDescription;
            col.HeaderText = "Description";
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup Point";
            col.Name = "PickupPoint";
            col.WrapText = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = "Destination";
            col.WrapText = true;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Charges";
            colD.Name = "Charges";
            colD.Maximum = 9999999;
            colD.ReadOnly = true;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Fares";
            colD.Name = "Fares";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.ReadOnly = true;
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Parking";
            colD.Name = "Parking";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            colD.ReadOnly = true;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Waiting";
            colD.Name = "Waiting";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            colD.ReadOnly = true;
            grdLister.Columns.Add(colD);

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Extra Drop";
            colD.Name = "ExtraDrop";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            colD.ReadOnly = true;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Meet and Greet";
            colD.Name = "MeetAndGreet";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            colD.ReadOnly = true;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Congestion";
            colD.Name = "CongtionCharge";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            colD.ReadOnly = true;
            grdLister.Columns.Add(colD);





            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Total";
            colD.Name = "Total";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            //colD.Expression = "Charges+Fares+Parking+Waiting+ExtraDrop+MeetAndGreet+CongtionCharge";
            colD.Expression = "Fares";
            colD.ReadOnly = true;
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);


            GridViewComboBoxColumn colPayment = new GridViewComboBoxColumn();
            colPayment.Name = COLS.Payment_ID;
            colPayment.HeaderText = "Status";
           // colPayment.DataSource = General.GetQueryable<Invoice_PaymentType>(null).Where(c => c.Id == 1 || c.Id == 3).OrderBy(c => c.Id).ToList();
          //  colPayment.DisplayMember = "PaymentType";
          //  colPayment.ValueMember = "Id";
            colPayment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            colPayment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            colPayment.DataType = typeof(int);
            colPayment.IsVisible = false;
            grdLister.Columns.Add(colPayment);
            colPayment.ReadOnly = true;


            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdLister.Columns["PickUpDate"].Width = 105;
            grdLister.Columns["RefNumber"].Width = 40;
            grdLister.Columns["Vehicle"].Width = 60;
            grdLister.Columns[COLS.Passenger].Width = 60;
            grdLister.Columns["PickUpPoint"].Width = 120;
            grdLister.Columns["Destination"].Width = 120;

            grdLister.Columns["Charges"].Width = 50;
            grdLister.Columns["Parking"].Width = 45;
            grdLister.Columns["Waiting"].Width = 50;
            grdLister.Columns["ExtraDrop"].Width = 60;
            grdLister.Columns["MeetAndGreet"].Width = 50;
            grdLister.Columns["CongtionCharge"].Width = 60;
            grdLister.Columns["Total"].Width = 45;
            grdLister.Columns["OrderNo"].Width = 55;

            grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            grdLister.Columns["RefNumber"].HeaderText = "Ref #";
            grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
            grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";

            grdLister.Columns["MeetAndGreet"].HeaderText = "M & G";
            grdLister.Columns["CongtionCharge"].HeaderText = "Congestion";
            grdLister.Columns["Payment_ID"].Width = 70;

            //AddUpdateColumn(grdLister);
            grdLister.AddDeleteColumn();


        }

        protected override void OnClosed(EventArgs e)
        {
            this.Dispose(true);
            General.RefreshListWithoutSelected<frmLostPropertyList>("frmLostPropertyList1");

        }


        public override void Save()
        {
            OnSave();
        }
        private void OnSave()
        {

            try
            {
                long? jobId = null;
                if (grdLister.Rows.Count > 0)
                {
                    jobId = grdLister.Rows[0].Cells["BookingId"].Value.ToLongorNull();
                }

                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }


                objMaster.Current.ReportedDate = dtpLPDate.Value.ToDate();
                objMaster.Current.Remarks = txtRemarks.Text.ToStr();
                objMaster.Current.CustomerName = txtCustomerName.Text.ToStr();
                objMaster.Current.CustomerAddress = txtAddressDetail.Text.ToStr();
                objMaster.Current.CustomerPhoneNo = txtCustomerMobileNo.Text.ToStr();
                objMaster.Current.Complaint = txtComplain.Text.ToStr();
                objMaster.Current.JobId = jobId;
                //objMaster.Current.Status = ddlStatus.SelectedItem.Text;


                objMaster.Current.Disposition = txtDespostion.Text.ToStr();
                objMaster.Current.Result = txtResult.Text.ToStr();
                objMaster.Current.CheckedBy = txtCheckedby.Text.ToStr();
                objMaster.Current.LostDate = dtpLostDate.Value.ToDate();





                objMaster.Save();

                objMaster.GetByPrimaryKey(objMaster.PrimaryKeyValue);
                DisplayRecord();
                General.RefreshListWithoutSelected<frmLostPropertyList>("frmLostPropertyList1");

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

            txtCustomerMobileNo.TextChanged -= new EventHandler(txtCustomerMobileNo_TextChanged);
            if (objMaster.Current == null) return;

            //lblStatus.Visible = true;
            //ddlStatus.Visible = true;

            txtLPNo.Text = objMaster.Current.LPNo.ToStr();
            dtpLPDate.Value = objMaster.Current.ReportedDate.ToDate();
            txtRemarks.Text = objMaster.Current.Remarks;
            txtCustomerName.Text = objMaster.Current.CustomerName;
            txtAddressDetail.Text = objMaster.Current.CustomerAddress;
            txtCustomerMobileNo.Text = objMaster.Current.CustomerPhoneNo;
            txtComplain.Text = objMaster.Current.Complaint;
            //ddlStatus.SelectedItem.Text = objMaster.Current.Status;




            txtDespostion.Text = objMaster.Current.Disposition;
            txtResult.Text = objMaster.Current.Result;
            txtCheckedby.Text = objMaster.Current.CheckedBy;
            dtpLostDate.Value = objMaster.Current.LostDate;




            //   int cnt = 1;
            // var list = General.GetQueryable<LostProperty>(null).FirstOrDefault();
            grdLister.RowCount = 1;
            Booking objBooking = objMaster.Current.Booking;
            int i = 0;
            // for (int i = 0; i < cnt; i++)
            //   {
            grdLister.Rows[i].Cells[COLS.ID].Value = objMaster.Current.Id;
            grdLister.Rows[i].Cells[COLS.TransId].Value = objMaster.Current.LPNo.ToStr();
            grdLister.Rows[i].Cells[COLS.BookingId].Value = objMaster.Current.JobId;

            //  objBooking = list.Booking;

            if (objBooking != null)
            {
                grdLister.Rows[i].Cells[COLS.PickupDate].Value = objBooking.PickupDateTime;
                grdLister.Rows[i].Cells[COLS.OrderNo].Value = objBooking.OrderNo;
                grdLister.Rows[i].Cells[COLS.PupilNo].Value = objBooking.PupilNo;

                grdLister.Rows[i].Cells[COLS.BookedBy].Value = objBooking.Gen_Company_Department.DefaultIfEmpty().DepartmentName.ToStr();


                grdLister.Rows[i].Cells[COLS.Vehicle].Value = objBooking.Fleet_VehicleType.VehicleType;

                grdLister.Rows[i].Cells[COLS.VehicleID].Value = objBooking.VehicleTypeId;
                grdLister.Rows[i].Cells[COLS.RefNumber].Value = objBooking.BookingNo;
                grdLister.Rows[i].Cells[COLS.Charges].Value = objBooking.CompanyPrice.ToDecimal();

                grdLister.Rows[i].Cells[COLS.Fares].Value = objBooking.FareRate.ToDecimal();


                grdLister.Rows[i].Cells[COLS.Parking].Value = objBooking.ParkingCharges.ToDecimal();
                grdLister.Rows[i].Cells[COLS.PickupPoint].Value = !string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + " - " + objBooking.FromAddress.ToStr() : objBooking.FromAddress.ToStr();
                grdLister.Rows[i].Cells[COLS.Destination].Value = !string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + " - " + objBooking.ToAddress.ToStr() : objBooking.ToAddress.ToStr();
                grdLister.Rows[i].Cells[COLS.Waiting].Value = objBooking.WaitingCharges.ToDecimal();
                grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = objBooking.ExtraDropCharges.ToDecimal();
                grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = objBooking.MeetAndGreetCharges.ToDecimal();
                grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = objBooking.CongtionCharges.ToDecimal();
                grdLister.Rows[i].Cells[COLS.Total].Value = objBooking.TotalCharges.ToDecimal();


                grdLister.Rows[i].Cells[COLS.Passenger].Value = objBooking.CustomerName.ToStr().Trim();

                grdLister.Rows[i].Cells[COLS.Payment_ID].Value = objBooking.InvoicePaymentTypeId.ToIntorNull();

            }

            //    }
            txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);

            grdLister.Rows[0].IsSelected = true;

        }
        private void btnPickBooking_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdLister.Rows.Count == 0)
                {

                    string CustomerName = txtCustomerName.Text.ToStr();
                    string Customerphone = txtCustomerMobileNo.Text.ToStr();
                    //int DriverId = ddlDriver.SelectedValue.ToInt();

                    DateTime? fromDate = dtpFromDate.Value.ToDate();
                    DateTime? tillDate = dtpTillDate.Value.ToDate();


                    string error = string.Empty;

                    if (CustomerName == "" && Customerphone == "")
                    {
                        if (string.IsNullOrEmpty(error))
                            error += Environment.NewLine;

                        error += "Required : Customer Name Or PhoneNo";


                    }
                    if (!string.IsNullOrEmpty(error))
                    {
                        ENUtils.ShowMessage(error);
                        return;

                    }



                    string[] hiddenColumns = null;


                    hiddenColumns = new string[] {  "Id", "CompanyId","CompanyName","Parking","Destination","Waiting","ExtraDrop","MeetAndGreet","Congtion",
                                                "Total","OrderNo","PupilNo","BookingDate","Description","Charges","AccountType"};




                    Func<Booking, bool> _conditionDate = null;
                    if (ddlPickType.SelectedIndex == 0)
                        _conditionDate = b => b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate;
                    else
                        _conditionDate = b => b.BookingDate.Value.Date >= fromDate && b.BookingDate.Value.Date <= tillDate;


                    List<object[]> list = General.ShowBookingMultiLister(c => c.CustomerName == CustomerName || c.CustomerMobileNo == Customerphone && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED), a => a.InvoiceId != null, hiddenColumns, _conditionDate);


                    GridViewRowInfo row;

                    int cnt = list.Count;

                    if (cnt > 1)
                    {
                        ENUtils.ShowMessage("You can not select multi booking.");
                        list = General.ShowBookingMultiLister(c => c.CustomerName == CustomerName || c.CustomerMobileNo == Customerphone && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED), a => a.InvoiceId != null, hiddenColumns, _conditionDate);

                    }


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
                        row.Cells[COLS.Fares].Value = list[i][19].ToDecimal();
                        row.Cells[COLS.Parking].Value = list[i][13].ToDecimal();
                        row.Cells[COLS.Waiting].Value = list[i][14].ToDecimal();
                        row.Cells[COLS.ExtraDrop].Value = list[i][15].ToDecimal();
                        row.Cells[COLS.MeetAndGreet].Value = list[i][16].ToDecimal();
                        row.Cells[COLS.CongtionCharge].Value = list[i][17].ToDecimal();
                        row.Cells[COLS.Total].Value = list[i][19].ToDecimal();

                        row.Cells[COLS.RemovalDescription].Value = list[i][18].ToStr();

                        row.Cells[COLS.BookedBy].Value = list[i][20].ToStr();

                    }

                    CalculateTotal();
                }
                else
                {
                    ENUtils.ShowMessage("Booking Already In a List.");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void CalculateTotal()
        {

            //txtInvoiceAmount.Text = string.Format("{0:£ #.##}", grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal()));

        }

        private void ddlDriver_SelectedValueChanged(object sender, EventArgs e)
        {

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



        public override void Print()
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;


            frmDriverTransactionReport frm = new frmDriverTransactionReport(1);

            var list = General.GetQueryable<vu_DriverRent>(a => a.Id == id).ToList();
            int count = list.Count;

            frm.DataSource = list;


            frm.GenerateReport();


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverTransactionReport1");

            if (doc != null)
            {
                doc.Close();
            }

            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
        }







        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            OnSave();

        }
        public override void AddNew()
        {
            OnNew();
        }
        public override void OnNew()
        {
            txtLPNo.Text = string.Empty;
            grdLister.Rows.Clear();
            dtpFromDate.Value = DateTime.Now.ToDate().AddDays(-30);
            dtpTillDate.Value = DateTime.Now.ToDate();

        }




        private void ShowHideColumns(bool show)
        {

            grdLister.Columns[COLS.Charges].IsVisible = show;
            grdLister.Columns[COLS.Fares].IsVisible = show;
            grdLister.Columns[COLS.CongtionCharge].IsVisible = show;
            grdLister.Columns[COLS.Destination].IsVisible = show;
            grdLister.Columns[COLS.ExtraDrop].IsVisible = show;
            grdLister.Columns[COLS.MeetAndGreet].IsVisible = show;
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

        private void btnPickCustomerAddress_Click(object sender, EventArgs e)
        {
            if (grdLister.Rows.Count == 0)
            {
                PickCustomerAddress(txtCustomerMobileNo.Text.Trim());
            }
            else
            {
                ENUtils.ShowMessage("Booking Already In a List.");
            }
        }

        private void PickCustomerAddress(string MobNo)
        {
            try
            {

                if (string.IsNullOrEmpty(MobNo))
                    return;



                SearchBooking();

            }
            catch (Exception ex)
            {


            }

        }
        private void SearchBooking()
        {
            try
            {

                frmSearchBooking frm = new frmSearchBooking(txtCustomerName.Text, "", txtCustomerMobileNo.Text.Trim());

                frm.ShowDialog();

                if (frm.IsSelected)
                {

                    txtCustomerName.Text = frm.CustomerName;
                    string Pick = frm.from;
                    string Destination = frm.to;
                    int? jobId = frm.JobId;
                    string pickupDate = frm.pickUpdate;
                    string Refno = frm.RefNo;
                    string Vechile = frm.Vechile;


                    GridViewRowInfo row;

                    row = grdLister.Rows.AddNew();

                    row.Cells[COLS.BookingId].Value = jobId;
                    row.Cells[COLS.PickupPoint].Value = Pick;
                    row.Cells[COLS.Destination].Value = Destination;
                    row.Cells[COLS.PickupDate].Value = pickupDate;
                    row.Cells[COLS.RefNumber].Value = Refno;
                    row.Cells[COLS.Vehicle].Value = Vechile;

                    frm.Dispose();
                    return;
                }
            }
            catch (Exception ex)
            {

            }

        }



        private void txtCustomerMobileNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerMobileNo.Text.Trim().Length == 11)
                {
                    if (grdLister.Rows.Count == 0)
                    {

                        txtCustomerMobileNo.TextChanged -= new EventHandler(txtCustomerMobileNo_TextChanged);

                        SearchBooking();


                        txtCustomerMobileNo.TextChanged += new EventHandler(txtCustomerMobileNo_TextChanged);
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception ex)
            {


            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtLPNo.Text = string.Empty;
            dtpLPDate.Value = DateTime.Now.ToDate();
            txtRemarks.Text = string.Empty;
            txtCustomerName.Text = string.Empty;
            txtAddressDetail.Text = string.Empty;
            txtCustomerMobileNo.Text = string.Empty;
            txtComplain.Text = string.Empty;
            txtDespostion.Text = string.Empty;
            txtCheckedby.Text = string.Empty;
            txtResult.Text = string.Empty;
            dtpLostDate.Value = DateTime.Now.ToDate();

            grdLister.RowCount = 0;
        }

        private void frmLostProperty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (objMaster.Current != null)
                {
                    int Id = objMaster.Current.Id.ToInt();
                    rptfrmLostProperty frm = new rptfrmLostProperty(Id);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if (objMaster.Current != null)
                {
                    int Id = objMaster.Current.Id.ToInt();
                    rptfrmLostProperty frm = new rptfrmLostProperty(Id);
                    frm.LoadReport();
                    frm.ExportReport();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            try
            {
                if (objMaster.Current != null)
                {
                    int Id = objMaster.Current.Id.ToInt();
                    rptfrmLostProperty frm = new rptfrmLostProperty(Id);
                    frm.SendEmail();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        

    }
}
