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
    public partial class frmDriverRent : UI.SetupBase
    {

        DriverRentBO objMaster = null;

        public struct COLS
        {
            public static string ID = "ID";
            public static string TransId = "TransId";
            public static string BookingId = "BookingId";
            public static string AccountTypeId = "AccountTypeId";
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
            public static string Account = "Account";
            public static string CompanyId = "CompanyId";

            public static string IsCommissionWise = "IsCommissionWise";
            public static string DriverCommissionType = "DriverCommissionType";
            public static string DriverCommission = "DriverCommission";

        }
        public frmDriverRent()
        {
            InitializeComponent();
            InitializeConstructor();


        }
        public frmDriverRent(int Id)
        {
            InitializeComponent();
            InitializeConstructor();
            ddlDriver.SelectedValue = Id;

        }

        public frmDriverRent(long Id, bool IsHide)
        {
            InitializeComponent();
            InitializeConstructor();
            ddlDriver.SelectedValue = Id;
           
            OnDisplayRecord(Id);

            this.btnPrint2.Visible = !IsHide;
            this.btnSaveInvoice.Visible = !IsHide;
            // DisplayRecord();
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
            objMaster = new DriverRentBO();
            ComboFunctions.FillDriverNoCombo(ddlDriver, c => c.DriverTypeId == 1 && c.IsActive==true);
            ComboFunctions.FillRentPayReasonCombo(ddlReason);

            txtOldBalance.ReadOnly = AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "SPECIAL") == 0;



            dtpTransactionDate.Value = DateTime.Now.ToDateTime();
            FormatChargesGrid();
            txtDriverRent.Text = string.Empty;
            grdLister.ShowGroupPanel = false;
            grdLister.AutoCellFormatting = true;
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            
            this.SetProperties((INavigation)objMaster);

            grdLister.AllowAddNewRow = false;


            //dtpFromDate.Value = DateTime.Now.ToDate().AddDays(-30);
            //dtpTillDate.Value = DateTime.Now.ToDate();

            dtpFromDate.Value = DateTime.Now.ToDate().AddDays(-6);
            dtpTillDate.Value = DateTime.Now.ToDate();

            txtDriverRent.Text = "0.00";
            txtBalance.Text = "0.00";
            txtRentPayment.Text = "0.00";
            txtExtra.Text = "0.00";
            txtFuel.Text = "0.00";
            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
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
                       
                        string fromAddress = row.Cells[COLS.PickupPoint].Value.ToStr().Trim();

                        string toAddress = row.Cells[COLS.Destination].Value.ToStr().Trim();



                        if (string.IsNullOrEmpty(fromAddress))
                        {
                            ENUtils.ShowMessage("Pickup Address cannot be empty");
                            return;
                        }


                        if (string.IsNullOrEmpty(toAddress))
                        {
                            ENUtils.ShowMessage("Destination Address cannot be empty");
                            return;

                        }


                        long id = row.Cells[COLS.BookingId].Value.ToLong();
                        decimal fare = row.Cells[COLS.Fares].Value.ToDecimal();
                        decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                        decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                        decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                        decimal meetAndGreet = row.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                        decimal CongtionCharge = row.Cells[COLS.CongtionCharge].Value.ToDecimal();
                        decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();                        

                        BookingBO objBookngBo = new BookingBO();
                        try
                        {


                            objBookngBo.GetByPrimaryKey(id);

                            if (objBookngBo.Current != null)
                            {
                                objBookngBo.Current.FareRate = fare;
                                objBookngBo.Current.ParkingCharges = parking;
                                objBookngBo.Current.WaitingCharges = waiting;
                                objBookngBo.Current.ExtraDropCharges = extraDrop;
                                objBookngBo.Current.MeetAndGreetCharges = meetAndGreet;
                                objBookngBo.Current.CongtionCharges = CongtionCharge;
                                objBookngBo.Current.TotalCharges = TotalCharges;

                                objBookngBo.Current.FromAddress = fromAddress;
                                objBookngBo.Current.ToAddress = toAddress;


                                objBookngBo.Save();


                                CalculateTotal();
                                CalculateBalance();
                            }
                        }
                        catch (Exception ex)
                        {
                            if (objMaster.Errors.Count > 0)
                                ENUtils.ShowMessage(objMaster.ShowErrors());
                            else
                                ENUtils.ShowMessage(ex.Message);

                        }
                    }
                }
                else if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        RadGridView grid = gridCell.GridControl;
                        grid.CurrentRow.Delete();
                        CalculateTotal();
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
            col.Name = COLS.IsCommissionWise;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name =COLS.DriverCommissionType;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.DriverCommission;
            grdLister.Columns.Add(col);





            col = new GridViewTextBoxColumn();
            col.Name = COLS.CompanyId;
            col.HeaderText = "ComapanyID";
            col.IsVisible = false;
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
            colDt.HeaderText = "Pickup Date-Time";
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
            colCombo.HeaderText = "Vehicle";
            colCombo.DataSource = General.GetQueryable<Fleet_VehicleType>(null).OrderBy(c => c.OrderNo).ToList();
            colCombo.DisplayMember = "VehicleType";
            colCombo.ValueMember = "Id";
            colCombo.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            colCombo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            colCombo.DataType = typeof(int);
            grdLister.Columns.Add(colCombo);
            colCombo.IsVisible = false;
            colCombo.ReadOnly = false;



            col = new GridViewTextBoxColumn();
            col.Name = COLS.Passenger;
            col.HeaderText = "Passenger";
            col.IsVisible = false;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Width = 900;
            col.IsVisible = false;
            col.ReadOnly = true;
            col.Name = COLS.RemovalDescription;
            col.HeaderText = "Description";
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = COLS.Account;
            col.HeaderText = "Account";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.AccountTypeId;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup Point";
            col.Name = "PickupPoint";
          //  col.ReadOnly = true;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = "Destination";
          //  col.ReadOnly = true;
            grdLister.Columns.Add(col);


            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Charges";
            colD.Name = "Charges";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Fares";
            colD.Name = "Fares";
            colD.Maximum = 9999999;
           // colD.ReadOnly = true;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Parking";
            colD.Name = "Parking";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Waiting";
            colD.Name = "Waiting";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Extra Drop";
            colD.Name = "ExtraDrop";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Meet and Greet";
            colD.Name = "MeetAndGreet";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Congestion";
            colD.Name = "CongtionCharge";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.IsVisible = true;
            colD.HeaderText = "Total";
            colD.Name = "Total";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            //colD.Expression = "Charges+Fares+Parking+Waiting+ExtraDrop+MeetAndGreet+CongtionCharge";
        //    colD.Expression = "Fares";
            grdLister.Columns.Add(colD);


            GridViewComboBoxColumn colPayment = new GridViewComboBoxColumn();
            colPayment.Name = COLS.Payment_ID;
            colPayment.HeaderText = "Status";
            colPayment.DataSource = General.GetQueryable<Invoice_PaymentType>(null).Where(c => c.Id == 1 || c.Id == 3).OrderBy(c => c.Id).ToList();
            colPayment.DisplayMember = "PaymentType";
            colPayment.ValueMember = "Id";
            colPayment.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            colPayment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            colPayment.DataType = typeof(int);
            colPayment.IsVisible = false;
            grdLister.Columns.Add(colPayment);
            colPayment.ReadOnly = false;


            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdLister.Columns["PickUpDate"].Width = 60;

            grdLister.Columns["Account"].Width = 75;

            grdLister.Columns["RefNumber"].Width = 40;
            grdLister.Columns["Vehicle"].Width = 60;
            grdLister.Columns[COLS.Passenger].Width = 60;
            grdLister.Columns["PickUpPoint"].Width = 80;
            grdLister.Columns["Destination"].Width = 80;

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





            GridViewCommandColumn colCmd = new GridViewCommandColumn();
            colCmd.Width = 50;
            colCmd.Name = "btnUpdate";
            colCmd.UseDefaultText = true;
            colCmd.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            colCmd.DefaultText = "Update";
            colCmd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(colCmd);
          
            grdLister.AddDeleteColumn();


//            grdLister.CommandCellClick+=new CommandCellClickEventHandler(grdLister_CommandCellClick);


        }

      


        protected override void OnClosed(EventArgs e)
        {

            General.RefreshListWithoutSelected<frmDriverRentList>("frmDriverRentList1");

        }


        public override void Save()
        {
            OnSave();
        }
        private void OnSave()
        {
            try
            {

                int DriverId = ddlDriver.SelectedValue.ToInt();

                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    var query = General.GetQueryable<DriverRent>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

                    if (query != null)
                    {
                        string Transno = query.TransNo.ToStr();

                        if (Transno == txtTransNo.Text)
                        {
                            objMaster.Edit();
                        }
                        else
                        {
                            ENUtils.ShowMessage("Can not Save Record. Another Record Exits..");
                        }
                    }
                    else
                    {
                        objMaster.Edit();
                    }
                }
                decimal Fuel = txtFuel.Text == "" ? 0 : txtFuel.Text.ToDecimal();
                decimal Extra = txtExtra.Text == "" ? 0 : txtExtra.Text.ToDecimal();



                objMaster.Current.TransDate = dtpTransactionDate.Value.ToDateTime();
                objMaster.Current.DriverId = ddlDriver.SelectedValue.ToIntorNull();
                objMaster.Current.DriverRent1 = txtRent.Text.ToDecimal();
                objMaster.Current.Balance = txtBalance.Text.ToDecimal();
                objMaster.Current.OldBalance = txtOldBalance.Text.ToDecimal();

                objMaster.Current.PDARent = numpdaRent.Value.ToDecimal();

                objMaster.Current.FromDate = dtpFromDate.Value.ToDate();
                objMaster.Current.ToDate = dtpTillDate.Value.ToDate();
                objMaster.Current.TransFor = ddlDayWise.SelectedText.ToStr();

                objMaster.Current.JobsTotal = grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value.ToStr() != "" && (c.Cells[COLS.AccountTypeId].Value.ToStr()!="" &&  c.Cells[COLS.AccountTypeId].Value.ToInt() == Enums.ACCOUNT_TYPE.ACCOUNT) && c.Cells[COLS.Total].Value.ToStr()!="")
                                    .Sum(c =>  c.Cells[COLS.Total].Value.ToDecimal());







                objMaster.Current.Fuel = Fuel.ToDecimal();
                objMaster.Current.Extra = Extra.ToDecimal();

                objMaster.Current.RentPayReasonId = ddlReason.SelectedValue.ToIntorNull();

                if (objMaster.PrimaryKeyValue != null)
                {
                    decimal val1 = txtRentPayment.Text.ToDecimal();
                    decimal val2 = txtRentPaid.Text.ToDecimal();

                    decimal total = val1 + val2;

                    objMaster.Current.RentPay = total;
                }
                else
                {
                    objMaster.Current.RentPay = txtRentPayment.Text.ToDecimal();
                }
                

                string[] skipProperties = { "DriverRent", "Booking" };
                IList<DriverRent_Charge> savedList = objMaster.Current.DriverRent_Charges;
                List<DriverRent_Charge> listofDetail = (from r in grdLister.Rows

                                                        select new DriverRent_Charge
                                                            {
                                                                Id = r.Cells[COLS.ID].Value.ToLong(),
                                                                TransId = r.Cells[COLS.TransId].Value.ToLong(),
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


            try
            {

                btnPaymentHistory.Visible = true;
                pnlRentPaid.Visible = true;
                lblRentPaid.Visible = true;
                lblRent.Visible = true;
                pnlRent.Visible = true;

                lblRentPound.Visible = true;
                lblRentPaidPound.Visible = true;

                ddlReason.SelectedValue = objMaster.Current.RentPayReasonId;

                btnExportPDF.Enabled = true;
                btnPrint2.Enabled = true;
                btnSendEmail.Enabled = true;
                pnlPaid.Visible = true;

                txtTransNo.Text = objMaster.Current.TransNo.ToStr();
                dtpTransactionDate.Value = objMaster.Current.TransDate.ToDate();
                ddlDriver.SelectedValue = objMaster.Current.DriverId;

                dtpFromDate.Value = objMaster.Current.FromDate.ToDate();
                dtpTillDate.Value = objMaster.Current.ToDate.ToDate();


                ddlDayWise.SelectedText = "";
                ddlDayWise.SelectedText = objMaster.Current.TransFor.ToStr();



                int cnt = objMaster.Current.DriverRent_Charges.Count;
                var list = objMaster.Current.DriverRent_Charges;
                grdLister.RowCount = cnt;
                Booking objBooking = null;
                for (int i = 0; i < cnt; i++)
                {
                    grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                    grdLister.Rows[i].Cells[COLS.TransId].Value = list[i].TransId;
                    grdLister.Rows[i].Cells[COLS.BookingId].Value = list[i].BookingId;

                    objBooking = list[i].Booking;

                    if (objBooking != null)
                    {

                        grdLister.Rows[i].Cells[COLS.Account].Value = objBooking.CompanyId != null ? objBooking.Gen_Company.CompanyName : "";
                        grdLister.Rows[i].Cells[COLS.CompanyId].Value = objBooking.CompanyId.ToStr();

                        grdLister.Rows[i].Cells[COLS.AccountTypeId].Value = objBooking.CompanyId != null ? objBooking.Gen_Company.AccountTypeId.ToStr().Trim() : null;

                        grdLister.Rows[i].Cells[COLS.PickupDate].Value = objBooking.PickupDateTime;
                        grdLister.Rows[i].Cells[COLS.OrderNo].Value = objBooking.OrderNo;
                        grdLister.Rows[i].Cells[COLS.PupilNo].Value = objBooking.PupilNo;

                        grdLister.Rows[i].Cells[COLS.BookedBy].Value = objBooking.Gen_Company_Department.DefaultIfEmpty().DepartmentName.ToStr();

                        grdLister.Rows[i].Cells[COLS.Vehicle].Value = objBooking.Fleet_VehicleType.VehicleType;

                        grdLister.Rows[i].Cells[COLS.VehicleID].Value = objBooking.VehicleTypeId;
                        grdLister.Rows[i].Cells[COLS.RefNumber].Value = objBooking.BookingNo;
                        //grdLister.Rows[i].Cells[COLS.Charges].Value = objBooking.FareRate.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.Charges].Value = objBooking.CompanyPrice.ToDecimal();

                        grdLister.Rows[i].Cells[COLS.Fares].Value = objBooking.FareRate.ToDecimal();

                        grdLister.Rows[i].Cells[COLS.Parking].Value = objBooking.CongtionCharges.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.PickupPoint].Value = !string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + " - " + objBooking.FromAddress.ToStr() : objBooking.FromAddress.ToStr();
                        grdLister.Rows[i].Cells[COLS.Destination].Value = !string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + " - " + objBooking.ToAddress.ToStr() : objBooking.ToAddress.ToStr();
                        grdLister.Rows[i].Cells[COLS.Waiting].Value = objBooking.MeetAndGreetCharges.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = objBooking.ExtraDropCharges.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = objBooking.MeetAndGreetCharges.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = objBooking.CongtionCharges.ToDecimal();
                        grdLister.Rows[i].Cells[COLS.Total].Value = objBooking.FareRate.ToDecimal() + objBooking.MeetAndGreetCharges.ToDecimal() + objBooking.CongtionCharges.ToDecimal();


                        grdLister.Rows[i].Cells[COLS.Passenger].Value = objBooking.CustomerName.ToStr().Trim();

                        grdLister.Rows[i].Cells[COLS.Payment_ID].Value = objBooking.InvoicePaymentTypeId.ToInt();


                        grdLister.Rows[i].Cells[COLS.IsCommissionWise].Value = objBooking.IsCommissionWise.ToBool();
                        grdLister.Rows[i].Cells[COLS.DriverCommissionType].Value = objBooking.DriverCommissionType.ToStr().Trim();
                        grdLister.Rows[i].Cells[COLS.DriverCommission].Value = objBooking.DriverCommission.ToDecimal();

                    }

                }

                grdLister.CurrentRow = null;

                txtAccountTotal.Text = objMaster.Current.JobsTotal.ToDecimal().ToStr();
                //txtRentPayment.Text = objMaster.Current.RentPay.ToDecimal().ToStr();


                txtDriverRent.Text = objMaster.Current.Balance.ToDecimal().ToStr();
                lblDriverRent.Text = "Driver Rent (Rem)";

                txtOldBalance.Text = objMaster.Current.OldBalance.ToDecimal().ToStr();
                txtBalance.Text = objMaster.Current.Balance.ToDecimal().ToStr();


                txtRent.Text = objMaster.Current.DriverRent1.ToDecimal().ToStr();
                txtRentPaid.Text = objMaster.Current.RentPay.ToDecimal().ToStr();

                txtFuel.Text = objMaster.Current.Fuel.ToDecimal().ToStr();
                txtExtra.Text = objMaster.Current.Extra.ToDecimal().ToStr();
                txtRentPayment.Text = "0.00";



                numpdaRent.Value = objMaster.Current.PDARent.ToDecimal();



                // Adding Cash A/c Amount Total


                decimal cashACCAmountTotal = grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value.ToStr().Trim() != ""
                    && c.Cells[COLS.AccountTypeId].Value.ToInt() == Enums.ACCOUNT_TYPE.CASH
                     && c.Cells[COLS.DriverCommissionType].Value.ToStr().Trim() == "Amount"
                     && c.Cells[COLS.IsCommissionWise].Value.ToBool()
                    )

                                        .Sum(c => c.Cells[COLS.DriverCommission].Value.ToDecimal());




                decimal cashACCPercentTotal = grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value.ToStr().Trim() != ""
                  && c.Cells[COLS.AccountTypeId].Value.ToInt() == Enums.ACCOUNT_TYPE.CASH
                  && c.Cells[COLS.DriverCommissionType].Value.ToStr().Trim() == "Percent"
                && c.Cells[COLS.IsCommissionWise].Value.ToBool())
                                      .Sum(c => (c.Cells[COLS.DriverCommission].Value.ToDecimal() * 100) / c.Cells[COLS.Total].Value.ToDecimal());



                txtCashAccJobsTotal.Text = string.Format("{0: #.##}", (cashACCAmountTotal + cashACCPercentTotal));



                ddlSubCompany.Enabled = true;
                if (ddlSubCompany.DataSource == null)
                {
                    ComboFunctions.FillSubCompanyCombo(ddlSubCompany);

                    if (ddlSubCompany.Items.Count > 1)
                        ddlSubCompany.SelectedIndex = 1;
                    else
                        ddlSubCompany.SelectedIndex = 0;


                }
            }
            catch (Exception ex)
            {



            }

        }


        private void btnPickBooking_Click(object sender, EventArgs e)
        {
            try
            {
                int DriverId = ddlDriver.SelectedValue.ToInt();

                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();


                string error = string.Empty;
                if (DriverId == 0)
                {
                    error += "Required : Driver";
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



                bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();

                string[] hiddenColumns = null;


                hiddenColumns = new string[] {  "Id", "Parking","Destination","Waiting","ExtraDrop","MeetAndGreet","Congtion",
                                                "Total","OrderNo","PupilNo","BookingDate","Description","Charges","AccountType","CompanyId",   "IsCommissionWise","DriverCommissionType","DriverCommission"};



                Func<Booking, bool> _conditionDate = null;
                if (ddlPickType.SelectedIndex == 0)
                    _conditionDate = b => b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate;
                else
                    _conditionDate = b => b.BookingDate.Value.Date >= fromDate && b.BookingDate.Value.Date <= tillDate;


                List<object[]> list = General.ShowDriverTransactionBookingMultiLister(c => c.DriverId == DriverId && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED) 
                    && ((RentForProcessedJobs == true 
                                &&  (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs==false && ( c.IsProcessed == null || c.IsProcessed == false)))
                                    , hiddenColumns, _conditionDate);
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

                    row.Cells[COLS.Account].Value = list[i][12].ToStr();

                    row.Cells[COLS.CompanyId].Value = list[i][11].ToStr();
                    row.Cells[COLS.AccountTypeId].Value = list[i][22].ToStr();

                    row.Cells[COLS.Charges].Value = list[i][10].ToDecimal();
                    row.Cells[COLS.Fares].Value = list[i][21].ToDecimal();
                    row.Cells[COLS.Parking].Value = list[i][13].ToDecimal();
                    row.Cells[COLS.Waiting].Value = list[i][14].ToDecimal();
                    row.Cells[COLS.ExtraDrop].Value = list[i][15].ToDecimal();
                    row.Cells[COLS.MeetAndGreet].Value = list[i][16].ToDecimal();
                    row.Cells[COLS.CongtionCharge].Value = list[i][17].ToDecimal();

                // old : 7 aug 15
                    //row.Cells[COLS.Total].Value = list[i][21].ToDecimal();
                    //new : 7 aug 15
                    row.Cells[COLS.Total].Value = list[i][19].ToDecimal();

                    row.Cells[COLS.RemovalDescription].Value = list[i][18].ToStr();

                    row.Cells[COLS.BookedBy].Value = list[i][20].ToStr();



                    row.Cells[COLS.IsCommissionWise].Value = list[i][23].ToStr().Trim();
                    row.Cells[COLS.DriverCommissionType].Value = list[i][24].ToStr().Trim();
                    row.Cells[COLS.DriverCommission].Value = list[i][25].ToDecimal();

                }

                CalculateTotal();
                CalculateBalance();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

      

        private void ddlDriver_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int? DriverId = ddlDriver.SelectedValue.ToInt();

                Fleet_Driver obj = General.GetObject<Fleet_Driver>(c => c.Id == DriverId);
                if (obj != null)
                {
                    txtDriverRent.Text = (obj.DriverMonthlyRent.ToDecimal()).ToStr();
                    txtRent.Text = (obj.DriverMonthlyRent.ToDecimal()).ToStr();
                    numpdaRent.Value = obj.PDARent.ToDecimal();
                    CalculateTotal();
                    CalculateBalance();
                }


                if (obj.DriverRents.Count == 0)
                {

                    decimal driverBalance =obj.InitialBalance.ToDecimal();
                    txtOldBalance.Text = (driverBalance).ToStr();

                }
                else
                {



                    var query = General.GetQueryable<DriverRent>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

                    if (query != null)
                    {
                        decimal driverBalance = query.Balance.ToDecimal();
                        txtOldBalance.Text = (driverBalance).ToStr();
                    }
                    else
                    {
                        txtOldBalance.Text = "0.00";
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


        private void CalculateTotal()
        {

            try
            {

                txtAccountTotal.Text = string.Format("{0: #.##}", grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value.ToStr().Trim() != "" && c.Cells[COLS.AccountTypeId].Value.ToInt() == Enums.ACCOUNT_TYPE.ACCOUNT)
                                        .Sum(c => c.Cells[COLS.Total].Value.ToDecimal()));

                if (txtAccountTotal.Text == " ")
                {
                    txtAccountTotal.Text = "0.00";
                }


                decimal cashACCAmountTotal = grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value.ToStr().Trim() != ""
                    && c.Cells[COLS.AccountTypeId].Value.ToInt() == Enums.ACCOUNT_TYPE.CASH
                     && c.Cells[COLS.DriverCommissionType].Value.ToStr().Trim()=="Amount"
                     && c.Cells[COLS.IsCommissionWise].Value.ToBool()
                    ) 
                   
                                        .Sum(c => c.Cells[COLS.DriverCommission].Value.ToDecimal());




                decimal cashACCPercentTotal = grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value.ToStr().Trim() != ""
                  && c.Cells[COLS.AccountTypeId].Value.ToInt() == Enums.ACCOUNT_TYPE.CASH
                  &&   c.Cells[COLS.DriverCommissionType].Value.ToStr().Trim()=="Percent"
                && c.Cells[COLS.IsCommissionWise].Value.ToBool())
                                      .Sum(c => (c.Cells[COLS.DriverCommission].Value.ToDecimal() * 100) / c.Cells[COLS.Total].Value.ToDecimal());



                txtCashAccJobsTotal.Text = string.Format("{0: #.##}", (cashACCAmountTotal + cashACCPercentTotal));


            }
            catch (Exception ex)
            {
            }
        }



        void CalculateBalance()
        {
            try
            {
                if (objMaster.PrimaryKeyValue == null)
                {
                    // for new record
                    Double dblValue;
                    if (txtRentPayment.Text != "" && txtAccountTotal.Text != "" && Double.TryParse(txtRentPayment.Text, out dblValue))
                    {

                        decimal AccountjobTotal = txtAccountTotal.Text.ToDecimal();

                        //decimal OldBalance = txtOldBalance.Text.ToDecimal();
                        decimal OldBalance = txtOldBalance.Text == "" ? 0 : txtOldBalance.Text.ToDecimal();
                        decimal DriverRent = txtDriverRent.Text.ToDecimal();



                        // Add pda rent
                        DriverRent = DriverRent + numpdaRent.Value;


                        // New lines added on 9-6-15 for adding cash A/C Job Commission Amount
                        if (txtCashAccJobsTotal.Text.ToStr().Trim() != string.Empty && txtCashAccJobsTotal.Text.ToStr().IsNumeric())
                            DriverRent = DriverRent + txtCashAccJobsTotal.Text.Trim().ToDecimal();


                        decimal payment = txtRentPayment.Text == "" ? 0 : txtRentPayment.Text.ToDecimal();

                        decimal Extra = txtExtra.Text == "" ? 0 : txtExtra.Text.ToDecimal();
                        decimal Fuel = txtFuel.Text == "" ? 0 : txtFuel.Text.ToDecimal();



                        // For Add Job Account Total
                        //Total = Total + OldBalance + Extra + Fuel + AccountjobTotal;


                        //Total = Total + OldBalance + Extra + Fuel;


                        decimal owed = AccountjobTotal  + Extra + Fuel - DriverRent;
                        owed = owed + OldBalance;

                        decimal balance = owed + payment;
                        txtBalance.Text = balance.ToStr();
                        lblPay.Text = balance.ToStr();

                        //DriverRent = DriverRent + OldBalance - Extra - Fuel - AccountjobTotal;
                        //if (payment <= DriverRent)
                        //{

                        //    decimal chk = (DriverRent - payment);

                        //    txtBalance.Text = chk.ToStr();
                        //    lblPay.Text = chk.ToStr();
                        //}
                        //else
                        //{
                        //    txtBalance.Text = "0.00";
                        //    txtRentPayment.Text = (DriverRent).ToStr();
                        //}
                    }
                    else
                    {
                        txtRentPayment.Text = "0.00";
                    }
                }
                else
                {
                    // for update record
                    decimal OldFuel = objMaster.Current.Fuel.ToDecimal();
                    decimal OldExtra = objMaster.Current.Extra.ToDecimal();
                    Double dblValue;
                    if (txtRentPayment.Text != "" && txtAccountTotal.Text != "" && Double.TryParse(txtRentPayment.Text, out dblValue))
                    {
                        decimal AccountjobTotal = txtAccountTotal.Text.ToDecimal();
                        
                        decimal OldBalance = txtOldBalance.Text == "" ? 0 : txtOldBalance.Text.ToDecimal();
                        decimal driverRent = txtRent.Text.ToDecimal();

                       
                        // add pda rent
                        driverRent = driverRent + numpdaRent.Value.ToDecimal();

                        // New lines added on 9-6-15 for adding cash A/C Job Commission Amount
                        if (txtCashAccJobsTotal.Text.ToStr().Trim() != string.Empty && txtCashAccJobsTotal.Text.ToStr().IsNumeric())
                            driverRent = driverRent + txtCashAccJobsTotal.Text.Trim().ToDecimal();


                        decimal payment = txtRentPayment.Text == "" ? 0 : txtRentPayment.Text.ToDecimal();

                        decimal Extra = txtExtra.Text == "" ? 0 : txtExtra.Text.ToDecimal();
                        decimal Fuel = txtFuel.Text == "" ? 0 : txtFuel.Text.ToDecimal();


                        decimal newfuel = Fuel - OldFuel;
                        decimal newExtra = Extra - OldExtra;

                        if (Fuel > OldFuel && Extra > OldExtra)
                        {
                            driverRent = driverRent + newExtra + newfuel;
                        }
                        else if (Fuel > OldFuel)
                        {
                            driverRent = driverRent + newfuel;
                        }
                        else if (Extra > OldExtra)
                        {
                            driverRent = driverRent + newExtra;
                        }
                        //else
                        //{
                        //    driverRent = driverRent;
                        //}


                        decimal owed = (AccountjobTotal + Extra + Fuel - driverRent) + objMaster.Current.RentPay.ToDecimal();
                        owed = owed  + OldBalance;

                        decimal balance = owed + payment;
                        txtBalance.Text = balance.ToStr();
                        lblPay.Text = balance.ToStr();

                        //txtBalance.Text = objMaster.Current.Balance.ToStr();
                        //txtOldBalance.Text = objMaster.Current.OldBalance.ToStr();
                        //txtRentPaid.Text = objMaster.Current.RentPay.ToStr();



                        //if (payment <= driverRent)
                        //{

                        //    decimal chk = (driverRent - payment);

                        //    txtBalance.Text = chk.ToStr();
                        //}
                        //else
                        //{
                        //    txtBalance.Text = "0.00";
                        //    txtRentPayment.Text = (driverRent).ToStr();
                        //}
                    }
                    else
                    {
                        txtRentPayment.Text = "0.00";
                    }
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
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

            var list = General.GetQueryable<vu_DriverRent>(a => a.Id == id).OrderBy(c=>c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;
            frm.CompanyHeader = ddlSubCompany.Text.Trim();
            frm.GenerateReport();

            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverTransactionReport1");



            if (doc != null)
            {
                doc.Close();
            }

            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
        }




        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            frmDriverTransactionReport frm = new frmDriverTransactionReport(1);


            var list = General.GetQueryable<vu_DriverRent>(a => a.Id == id).OrderBy(c => c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;
            frm.CompanyHeader = ddlSubCompany.Text.Trim();

            frm.GenerateReport();

            frm.ExportReport(objMaster.Current.TransNo);
        }


        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            OnSave();

        }
        public override void OnNew()
        {
            txtTransNo.Text = string.Empty;
            ComboFunctions.FillDriverNoCombo(ddlDriver, c => c.DriverTypeId == 1 && c.IsActive == true);
            ComboFunctions.FillRentPayReasonCombo(ddlReason);
            grdLister.Rows.Clear();
            txtAccountTotal.Text = string.Empty;
            ddlReason.SelectedIndex = 0;
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            frmDriverTransactionReport frm = new frmDriverTransactionReport(1);


            var list = General.GetQueryable<vu_DriverRent>(a => a.Id == id).OrderBy(c => c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;
            frm.CompanyHeader = ddlSubCompany.Text.Trim();

            frm.GenerateReport();

            frm.SendEmail(objMaster.Current.TransNo);
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

        private void txtRentPayment_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }
    
        private void ddlDayWise_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                string val = ddlDayWise.SelectedItem.Text.ToStr();

                if (val == "Weekly")
                {
                    dtpFromDate.Value = DateTime.Now.ToDate().AddDays(-6);
                    dtpTillDate.Value = DateTime.Now.ToDate();
                }
                else if (val == "Monthly")
                {
                    dtpFromDate.Value = DateTime.Now.ToDate().AddDays(-30);
                    dtpTillDate.Value = DateTime.Now.ToDate();
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void btnBookingReport_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void btnSingle_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;


            frmDriverTransactionReport frm = new frmDriverTransactionReport(2);

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

        private void txtFuel_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        private void txtExtra_TextChanged(object sender, EventArgs e)
        {
            CalculateBalance();
        }

        private void txtFuel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;



        }

        private void txtExtra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }

        private void txtOldBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ( e.KeyChar!='-' && (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back))))
                e.Handled = true;

            if (grdLister.Rows.Count > 0)
                e.Handled = true;

            if (txtOldBalance.Text.Length > 0 && e.KeyChar == '-')
                e.Handled = true;
       
        }

        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {
               
                if(objMaster.Current!=null && objMaster.PrimaryKeyValue!=null)
                {

                    frmSearchDriverRentPaymentHistory frm = new frmSearchDriverRentPaymentHistory("",objMaster.Current.DriverRent_PaymentHistories.OrderBy(c => c.PaymentDate).ToList());

                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();


                    frm.Dispose();
                    frm = null;

                    GC.Collect();



                }
            }
            catch (Exception ex)
            {

            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            frmDriverRentDebitCredit frm = new frmDriverRentDebitCredit();
            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRentDebitCredit1");
            if (doc != null)
            {
                doc.Close();
            }

            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            frmAddAllDriverRentExpenses frm = new frmAddAllDriverRentExpenses();
            frm.ShowDialog();
        }


    }
}
