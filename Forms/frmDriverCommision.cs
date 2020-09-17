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
    public partial class frmDriverCommision : UI.SetupBase
    {
        List<Fleet_Driver_CommissionRange> listOfCommRange = null;

        DriverCommisionBO objMaster = null;
        private bool IsFareAndWaitingWiseComm;

      //  decimal Commision = 0;

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
            public static string Commission = "Commission";
            public static string IsCommissionWise = "IsCommissionWise";
            public static string CommissionType = "CommissionType";
            public static string CommissionValue = "CommissionValue";

            public static string AgentFees = "AgentFees";

            public static string Payment_ID = "Payment_ID";

            public static string Fares = "Fares";
            public static string Account = "Account";
            public static string CompanyId = "CompanyId";

        }
        public frmDriverCommision()
        {
            InitializeComponent();
            InitializeConstructor();

        }
        public frmDriverCommision(int Id)
        {
            InitializeComponent();
            InitializeConstructor();
            ddlDriver.SelectedValue = Id;

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
            objMaster = new DriverCommisionBO();
            ComboFunctions.FillDriverNoCombo(ddlDriver, c => c.DriverTypeId == 2);
            dtpTransactionDate.Value = DateTime.Now.ToDateTime();
            FormatChargesGrid();
            txtDriverOwed.Text = string.Empty;
            grdLister.ShowGroupPanel = false;
        //    grdLister.AutoCellFormatting = true;
            grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            grdLister.Font = new Font("Tahoma", 9, FontStyle.Bold);

            this.SetProperties((INavigation)objMaster);

            grdLister.AllowAddNewRow = false;

            grdLister.AllowDeleteRow = false;


            dtpFromDate.Value = DateTime.Now.ToDate().AddDays(-6);
            dtpTillDate.Value = DateTime.Now.ToDate();

            txtDriverOwed.Text = "0.00";
            txtBalance.Text = "0.00";
            txtCommisionPayment.Text = "0.00";
            txtExtra.Text = "0.00";
            txtFuel.Text = "0.00";
         //   grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);


            dtpFromDate.DateTimePickerElement.ValueChanging += new ValueChangingEventHandler(DateTimePickerElement_ValueChanging);
            dtpTillDate.DateTimePickerElement.ValueChanging+=new ValueChangingEventHandler(DateTimePickerElement_ValueChanging);
            
            if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool())
            {

                listOfCommRange = GetSystemCommissionRange(); 
            }
        }

        void DateTimePickerElement_ValueChanging(object sender, ValueChangingEventArgs e)
        {
            if (grdLister.Rows.Count > 0)
            {

                if(DialogResult.Yes==   MessageBox.Show("You cannot Change Date After Selecting the Jobs! "+Environment.NewLine+
                     "To Change Date Filter , You need to Remove All the Jobs"+Environment.NewLine+
                     "Do you want to Remove All the Jobs ? ","Warning", MessageBoxButtons.YesNo))
                {

                    grdLister.Rows.Clear();
                    numCommissionPercent.Enabled = true;
                    radButton1.Enabled = true;
                    CalculateTotal();
                    CalculateBalance();

                }
                else
                {
                    e.Cancel=true;
                    numCommissionPercent.Enabled = false;

                }
                  

            }
        }

        private List<Fleet_Driver_CommissionRange> GetSystemCommissionRange()
        {
            return  (from a in General.GetQueryable<Gen_SysPolicy_CommissionPriceRange>(null).ToList()
                               select new Fleet_Driver_CommissionRange
                               {
                                   DriverId = 0,
                                   FromPrice = a.FromPrice,
                                   ToPrice = a.ToPrice,
                                   CommissionValue = a.CommissionValue


                               }).ToList();

        }

        //void grdLister_CommandCellClick(object sender, EventArgs e)
        //{
        //    GridCommandCellElement gridCell = (GridCommandCellElement)sender;
        //    if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
        //    {
        //        if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Trash Booking ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
        //        {
        //            RadGridView grid = gridCell.GridControl;
        //            grid.CurrentRow.Delete();
        //        }
        //    }

        //}

        private void FormatChargesGrid()
        {

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();

            col.IsVisible = false;
            col.Name = "Id";
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
            col.HeaderText = "Pickup Point";
            col.Name = "PickupPoint";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = "Destination";
            col.Name = "Destination";
            col.ReadOnly = true;
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
            colD.ReadOnly = true;
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
            colD.IsVisible =false;
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



            string CommissionExpression = "";


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Agent Fee";
            colD.Name = "AgentFees";
            colD.Maximum = 9999999;
            colD.ReadOnly = true;
            colD.FormatString = "{0:#,###0.00}";
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


            if (AppVars.objPolicyConfiguration.PickCommissionFromChargesAndWaiting.ToBool())
            {
                grdLister.Columns["Waiting"].IsVisible = true;
                CommissionExpression = "Fares+Waiting";
                IsFareAndWaitingWiseComm = true;
            }
            else
            {
                if (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt() == 1)
                {

                    CommissionExpression = "(Fares+Waiting+Parking)-AgentFees";
                }
                else if (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt() == 2)
                {

                    CommissionExpression = "(Fares+AgentFees)";
                }
            }
            
            
             colD.Expression = CommissionExpression;
             grdLister.Columns.Add(colD);

             colD = new GridViewDecimalColumn();
             colD.DecimalPlaces = 2;
             colD.Minimum = 0;
             colD.HeaderText = COLS.Commission;
             colD.Name = COLS.Commission;
             colD.Maximum = 9999999;
             colD.FormatString = "{0:#,###0.00}";
             colD.IsVisible = true;
             colD.ReadOnly = true;
             grdLister.Columns.Add(colD);


             col = new GridViewTextBoxColumn();
             col.HeaderText = COLS.IsCommissionWise;
             col.Name = COLS.IsCommissionWise;
             col.ReadOnly = true;
             col.IsVisible = false;
             grdLister.Columns.Add(col);



             col = new GridViewTextBoxColumn();
             col.HeaderText = COLS.CommissionType;
             col.Name = COLS.CommissionType;
             col.ReadOnly = true;
             col.IsVisible = false;
             grdLister.Columns.Add(col);


             colD = new GridViewDecimalColumn();
             colD.DecimalPlaces = 2;
             colD.Minimum = 0;
             colD.HeaderText = COLS.CommissionValue;
             colD.Name = COLS.CommissionValue;
             colD.Maximum = 9999999;
             colD.FormatString = "{0:#,###0.00}";
             colD.IsVisible = false;
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

            grdLister.Columns[COLS.Commission].Width = 60;

            grdLister.Columns["Total"].Width = 45;
            grdLister.Columns["OrderNo"].Width = 55;

            grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            grdLister.Columns["RefNumber"].HeaderText = "Ref #";
            grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
            grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";

            grdLister.Columns["MeetAndGreet"].HeaderText = "M & G";
            grdLister.Columns["CongtionCharge"].HeaderText = "Congestion";
            grdLister.Columns["Payment_ID"].Width = 70;



        //    AddCommandColumn(grdLister, "btnDelete", "Delete");
          //  grdLister.AddDeleteColumn();

        }


        //private void AddCommandColumn(RadGridView grid, string colName, string caption)
        //{
        //    GridViewCommandColumn col = new GridViewCommandColumn();
        //    col.Width = 60;

        //    col.Name = colName;
        //    col.UseDefaultText = true;
        //    col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
        //    col.DefaultText = caption;
        //    col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

        //    grid.Columns.Add(col);

        //}

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

                int DriverId = ddlDriver.SelectedValue.ToInt();

                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    var query = General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

                    if (query != null)
                    {
                        string Transno = query.TransNo.ToStr();

                        if (Transno == txtTransNo.Text)
                        {

                            if (General.GetQueryable<DriverCommission_PaymentHistory>(c => c.CommissionId == objMaster.Current.Id).Count() > 0)
                            {
                                ENUtils.ShowMessage("Can not Save Record. Payment already Exist Against this Transaction..");
                                return;
                            }

                            objMaster.Edit();
                        }
                        else
                        {
                            ENUtils.ShowMessage("Can not Save Record. Another Record Exits..");
                            return;
                        }
                    }
                    else
                    {

                        if (General.GetQueryable<DriverCommission_PaymentHistory>(c=>c.CommissionId==objMaster.Current.Id).Count() > 0)
                        {
                            ENUtils.ShowMessage("Can not Save Record. Payment already Exist Against this Transaction..");
                            return;
                        }


                        if (General.GetQueryable<DriverCommission_PaymentHistory>(c => c.CommissionId == objMaster.Current.Id).Count() > 0)
                        {
                            ENUtils.ShowMessage("Can not Save Record. Payment already Exist Against this Transaction..");
                            return;
                        }

                        objMaster.Edit();
                    }
                }
                decimal Fuel = txtFuel.Text == "" ? 0 : txtFuel.Text.ToDecimal();
                decimal Extra = txtExtra.Text == "" ? 0 : txtExtra.Text.ToDecimal();


                DateTime Datetime = dtpTransactionDate.Value.ToDateTime();
                objMaster.Current.TransDate = dtpTransactionDate.Value.ToDateTime();
                objMaster.Current.DriverId = ddlDriver.SelectedValue.ToIntorNull();
              //  objMaster.Current.DriverCommision = txtDriverOwed.Text == "" ? 0 : txtDriverOwed.Text.ToDecimal();
                objMaster.Current.DriverCommision = numCommissionPercent.Value;


                objMaster.Current.Balance = txtBalance.Text.ToDecimal();
                objMaster.Current.OldBalance = txtOldBalance.Text.ToDecimal();

                objMaster.Current.FromDate = dtpFromDate.Value.ToDate();
                objMaster.Current.ToDate = dtpTillDate.Value.ToDate();
                objMaster.Current.TransFor = ddlDayWise.SelectedText.ToStr();

             
                objMaster.Current.JobsTotal = grdLister.Rows.Sum(c => c.Cells[COLS.Fares].Value.ToDecimal());

                objMaster.Current.Remarks = txtRemarks.Text.Trim();

                objMaster.Current.IsCreditOrDebit = optCredit.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? true : false;


                objMaster.Current.PDARent = numpdaRent.Value.ToDecimal();
                objMaster.Current.DriverOwed = txtDriverOwed.Text == "" ? 0 : txtDriverOwed.Text.ToDecimal();


                objMaster.Current.AgentFeesTotal = numAgentFeeTotal.Value;
                 objMaster.Current.Extra = numPDARentPerWeek.Value;
                objMaster.Current.CommissionTotal =((objMaster.Current.JobsTotal-objMaster.Current.AgentFeesTotal)* numCommissionPercent.Value)/100;
                //grdLister.Rows.Sum(c => c.Cells[COLS.Commission].Value.ToDecimal()).ToDecimal();


                objMaster.Current.AccJobsTotal = grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value != null).Sum(c => c.Cells[COLS.Fares].Value.ToDecimal()).ToDecimal();


                objMaster.Current.Fuel = Fuel.ToDecimal();
               // objMaster.Current.Extra = Extra.ToDecimal();
             
           
                string[] skipProperties = { "Fleet_DriverCommision", "Booking" };
                IList<Fleet_DriverCommision_Charge> savedList = objMaster.Current.Fleet_DriverCommision_Charges;
                List<Fleet_DriverCommision_Charge> listofDetail = (from r in grdLister.Rows

                                                                   select new Fleet_DriverCommision_Charge
                                                            {
                                                                Id = r.Cells[COLS.ID].Value.ToLong(),
                                                                TransId = r.Cells[COLS.TransId].Value.ToLong(),
                                                                BookingId = r.Cells[COLS.BookingId].Value.ToLongorNull(),
                                                                CommissionPerBooking=r.Cells[COLS.Commission].Value.ToDecimal()
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


            pnlRentPaid.Visible = true;
            lblRentPaid.Visible = true;
         
            btnExportPDF.Enabled = true;
            btnPrint.Enabled = true;
            btnSendEmail.Enabled = true;
            pnlPaid.Visible = true;
            ddlDriver.Enabled = false;
            radButton1.Enabled = false;


            txtTransNo.Text = objMaster.Current.TransNo.ToStr();
            dtpTransactionDate.Value = objMaster.Current.TransDate.ToDateTime();
            ddlDriver.SelectedValue = objMaster.Current.DriverId;

            dtpFromDate.Value = objMaster.Current.FromDate.ToDate();
            dtpTillDate.Value = objMaster.Current.ToDate.ToDate();




            txtRemarks.Text = objMaster.Current.Remarks.ToStr();

            if (objMaster.Current.IsCreditOrDebit.ToBool())
                optCredit.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
            else
                optCredit.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;


            if (objMaster.Current.TransFor != "")
            {
                ddlDayWise.SelectedText = objMaster.Current.TransFor.ToStr();
            }

            int cnt = objMaster.Current.Fleet_DriverCommision_Charges.Count;
            var list = objMaster.Current.Fleet_DriverCommision_Charges;
            grdLister.RowCount = cnt;
            Booking objBooking = null;
            Fleet_Driver_CommissionRange range = null;
            for (int i = 0; i < cnt; i++)
            {
                grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                grdLister.Rows[i].Cells[COLS.TransId].Value = list[i].TransId;
                grdLister.Rows[i].Cells[COLS.BookingId].Value = list[i].BookingId;

                objBooking = list[i].Booking;

                if (objBooking != null)
                {
                  

                    grdLister.Rows[i].Cells[COLS.Account].Value = objBooking.CompanyId != null ? objBooking.Gen_Company.CompanyName : "";
                    grdLister.Rows[i].Cells[COLS.CompanyId].Value = objBooking.CompanyId.ToIntorNull();
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


                    grdLister.Rows[i].Cells[COLS.PickupPoint].Value = !string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + " - " + objBooking.FromAddress.ToStr() : objBooking.FromAddress.ToStr();
                    grdLister.Rows[i].Cells[COLS.Destination].Value = !string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + " - " + objBooking.ToAddress.ToStr() : objBooking.ToAddress.ToStr();

                    grdLister.Rows[i].Cells[COLS.Parking].Value = objBooking.CongtionCharges.ToDecimal();
                     grdLister.Rows[i].Cells[COLS.Waiting].Value = objBooking.MeetAndGreetCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = objBooking.ExtraDropCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = objBooking.WaitingCharges.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = objBooking.ParkingCharges.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.AgentFees].Value = objBooking.AgentCommission.ToDecimal();


                    grdLister.Rows[i].Cells[COLS.Total].Value = 
                        (objBooking.FareRate.ToDecimal()+objBooking.MeetAndGreetCharges.ToDecimal()+objBooking.CongtionCharges.ToDecimal())-objBooking.AgentCommission.ToDecimal();


                    grdLister.Rows[i].Cells[COLS.Passenger].Value = objBooking.CustomerName.ToStr().Trim();

                    grdLister.Rows[i].Cells[COLS.Payment_ID].Value = objBooking.InvoicePaymentTypeId.ToInt();


                    grdLister.Rows[i].Cells[COLS.IsCommissionWise].Value = objBooking.IsCommissionWise.ToBool();
                  //  grdLister.Rows[i].Cells[COLS.CommissionType].Value = objBooking.DriverCommissionType.ToStr();
                    grdLister.Rows[i].Cells[COLS.CommissionType].Value = "Percent";
                   // grdLister.Rows[i].Cells[COLS.CommissionValue].Value = objBooking.DriverCommission.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.CommissionValue].Value = list[i].CommissionPerBooking.ToDecimal();


                    // Add Commission Column

                    if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool())
                    {

                        range = listOfCommRange.FirstOrDefault(c => grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal() >= c.FromPrice && grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal() <= c.ToPrice);

                        decimal Comm = range != null ? range.CommissionValue.ToDecimal() : 0;

                        if (objBooking.IsCommissionWise.ToBool())
                        {
                            Comm = objBooking.DriverCommission.ToDecimal();


                            if (objBooking.DriverCommissionType.ToStr() == "Percent")
                            {
                                grdLister.Rows[i].Cells[COLS.Commission].Value = (grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal().ToDecimal() * Comm) / 100;
                            }
                            else
                            {
                                grdLister.Rows[i].Cells[COLS.Commission].Value = Comm;

                            }
                        }
                        else
                        {

                            grdLister.Rows[i].Cells[COLS.Commission].Value = (grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal().ToDecimal() * Comm) / 100;

                        }



                    }
                    else
                    {

                        grdLister.Rows[i].Cells[COLS.Commission].Value = (grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal().ToDecimal() * numCommissionPercent.Value) / 100;


                    }

                }

            }


       

            grdLister.CurrentRow = null;

            txtInvoiceAmount.Text = objMaster.Current.JobsTotal.ToDecimal().ToStr();
          
            txtDriverOwed.Text = objMaster.Current.DriverOwed.ToDecimal().ToStr();

            txtOldBalance.Text = objMaster.Current.OldBalance.ToDecimal().ToStr();
            txtBalance.Text = objMaster.Current.Balance.ToDecimal().ToStr();


            txtRent.Text = objMaster.Current.DriverOwed.ToDecimal().ToStr();
            txtCommissionPaid.Text = objMaster.Current.DriverCommission_PaymentHistories.Sum(c=>c.CommissionPay.ToDecimal()).ToStr();


            numPDARentPerWeek.Value = objMaster.Current.Extra.ToDecimal();

            numpdaRent.Value = objMaster.Current.PDARent.ToDecimal();

            txtFuel.Text = objMaster.Current.Fuel.ToDecimal().ToStr();
          //  txtExtra.Text = objMaster.Current.Extra.ToDecimal().ToStr();

            numCommissionPercent.Value = objMaster.Current.DriverCommision.ToDecimal();
            txtCommisionPayment.Text = "0.00";
            numCommissionPercent.Enabled = false;

            CalculateTotal();


            var query = General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == objMaster.Current.DriverId).OrderByDescending(c => c.Id).FirstOrDefault();


            if ((query != null && query.Id != objMaster.Current.Id && query.Id > objMaster.Current.Id))
            {

                btnSaveInvoice.Enabled = false;
                radButton1.Enabled = false;

            }


            if (objMaster.Current.DriverCommission_PaymentHistories.Count > 0)
            {

                btnPaymentHistory.Visible = true;
                btnSaveInvoice.Enabled = false;
                radButton1.Enabled = false;

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

                if (fromDate > tillDate)
                {

                    ENUtils.ShowMessage("From Date must be less than To Date");
                    return;
                }



                string[] hiddenColumns = null;


               
               
         

              

                if (!IsFareAndWaitingWiseComm)
                    hiddenColumns = new string[] {  "Id", "Parking","Destination","ExtraDrop","MeetAndGreet","Congtion",
                                                "Total","OrderNo","PupilNo","BookingDate","Description","Charges","AccountType","CompanyId","Waiting"
                                                ,"IsCommissionWise","DriverCommissionType","DriverCommission"};

                else
                    hiddenColumns = new string[] {  "Id", "Parking","Destination","ExtraDrop","MeetAndGreet","Congtion",
                                                "Total","OrderNo","PupilNo","BookingDate","Description","Charges","AccountType","CompanyId" 
                                                ,"IsCommissionWise","DriverCommissionType","DriverCommission"};




                bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();

                Expression<Func<Booking, bool>> _conditionDate = null;
                if (ddlPickType.SelectedIndex == 0)
                    _conditionDate =   b =>   ((RentForProcessedJobs == true && (b.IsProcessed != null && b.IsProcessed == true)) || (RentForProcessedJobs == false && (b.IsProcessed == null || b.IsProcessed == false))) &&        (b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate) && (b.DriverId == DriverId && b.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);
                else
                    _conditionDate = b => ((RentForProcessedJobs == true && (b.IsProcessed != null && b.IsProcessed == true)) || (RentForProcessedJobs == false && (b.IsProcessed == null || b.IsProcessed == false))) && (b.BookingDate.Value.Date >= fromDate && b.BookingDate.Value.Date <= tillDate) && (b.DriverId == DriverId && b.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);


                   List<object[]> list = General.ShowDriverCommTransactionBookingMultiLister(c => c.DriverId == DriverId && (c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED),  hiddenColumns, _conditionDate);

          
                GridViewRowInfo row;



                int cnt = list.Count;


                var existBookingId = grdLister.Rows.ToList();

                Fleet_Driver_CommissionRange range = null;
                object[] objExisting = null;
                foreach (var item in existBookingId)
                {

                    if (list.Count(c => c[0].ToLong() == item.Cells[COLS.BookingId].Value.ToLong()) > 0)
                        continue;



                    objExisting = new object[item.Cells.Count];

                    objExisting[0] = item.Cells[COLS.BookingId].Value.ToLong();

                    objExisting[3] = item.Cells[COLS.RefNumber].Value.ToStr();
                    objExisting[2] = item.Cells[COLS.PickupDate].Value.ToDateTime();


                    objExisting[4] = item.Cells[COLS.Vehicle].Value.ToStr();



                    objExisting[5] = item.Cells[COLS.OrderNo].Value.ToStr();
                    objExisting[6] = item.Cells[COLS.PupilNo].Value.ToStr();

                    objExisting[7] = item.Cells[COLS.Passenger].Value.ToStr();

                    objExisting[8] = item.Cells[COLS.PickupPoint].Value.ToStr();
                    objExisting[9] = item.Cells[COLS.Destination].Value.ToStr();

                    objExisting[12] = item.Cells[COLS.Account].Value.ToStr();

                    objExisting[11] = item.Cells[COLS.CompanyId].Value.ToIntorNull();

                    objExisting[10] = item.Cells[COLS.Charges].Value.ToDecimal();
                    objExisting[21] = item.Cells[COLS.Fares].Value.ToDecimal();
                    objExisting[13] = item.Cells[COLS.Parking].Value.ToDecimal();
                    objExisting[14] = item.Cells[COLS.Waiting].Value.ToDecimal();
                    objExisting[15] = item.Cells[COLS.ExtraDrop].Value.ToDecimal();
                    objExisting[16] = item.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                    objExisting[17] = item.Cells[COLS.CongtionCharge].Value.ToDecimal();
                    objExisting[19] = item.Cells[COLS.Total].Value.ToDecimal();

                    objExisting[18] = item.Cells[COLS.RemovalDescription].Value.ToStr();

                    objExisting[20] = item.Cells[COLS.BookedBy].Value.ToStr();

                    // Add Commission Column


                    objExisting[23] = item.Cells[COLS.IsCommissionWise].Value.ToBool();
                    objExisting[24] = item.Cells[COLS.CommissionType].Value.ToStr();
                    objExisting[25] = item.Cells[COLS.CommissionValue].Value.ToDecimal();

                    objExisting[26] = item.Cells[COLS.AgentFees].Value.ToDecimal();


                    ///

                    list.Add(objExisting);

                    
                }

               

               // list.RemoveAll(c => existBookingId.Contains(c[0].ToLong()));

                cnt = list.Count;


                int totalRows = cnt;

                grdLister.RowCount = cnt;


                for (int i = 0; i < totalRows; i++)
                {
                        grdLister.Rows[i].Cells[COLS.BookingId].Value = list[i][0].ToLongorNull();
                        grdLister.Rows[i].Cells[COLS.RefNumber].Value = list[i][3].ToStr();
                        grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i][2].ToDateTime();

                        grdLister.Rows[i].Cells[COLS.Vehicle].Value = list[i][4].ToStr();

                        grdLister.Rows[i].Cells[COLS.OrderNo].Value = list[i][5].ToStr();
                        grdLister.Rows[i].Cells[COLS.PupilNo].Value = list[i][6].ToStr();

                        grdLister.Rows[i].Cells[COLS.Passenger].Value = list[i][7].ToStr();

                        grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i][8].ToStr();
                        grdLister.Rows[i].Cells[COLS.Destination].Value = list[i][9].ToStr();

                        grdLister.Rows[i].Cells[COLS.Account].Value = list[i][12].ToStr();

                        grdLister.Rows[i].Cells[COLS.CompanyId].Value = list[i][11].ToIntorNull();

                        grdLister.Rows[i].Cells[COLS.Charges].Value = list[i][10].ToDecimal();
                        grdLister.Rows[i].Cells[COLS.Fares].Value = list[i][21].ToDecimal();
                        grdLister.Rows[i].Cells[COLS.Parking].Value = list[i][13].ToDecimal();
                        grdLister.Rows[i].Cells[COLS.Waiting].Value = list[i][14].ToDecimal();
                        grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = list[i][15].ToDecimal();
                        grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = list[i][16].ToDecimal();
                        grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = list[i][17].ToDecimal();

                        grdLister.Rows[i].Cells[COLS.AgentFees].Value = list[i][26].ToDecimal();

                        grdLister.Rows[i].Cells[COLS.Total].Value = list[i][19].ToDecimal();

                        grdLister.Rows[i].Cells[COLS.RemovalDescription].Value = list[i][18].ToStr();

                        grdLister.Rows[i].Cells[COLS.BookedBy].Value = list[i][20].ToStr();

                        // Add Commission Column

                        if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool())
                        {

                            range = listOfCommRange.FirstOrDefault(c => grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal() >= c.FromPrice && grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal() <= c.ToPrice);

                            decimal Comm = range != null ? range.CommissionValue.ToDecimal() : 0;

                            if (list[i][23].ToBool())
                            {
                                Comm = list[i][25].ToInt();


                                if (list[i][24].ToStr() == "Percent")
                                {
                                    grdLister.Rows[i].Cells[COLS.Commission].Value = (grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal().ToDecimal() * Comm) / 100;
                                }
                                else
                                {
                                    grdLister.Rows[i].Cells[COLS.Commission].Value = Comm;

                                }
                            }
                            else
                            {

                                grdLister.Rows[i].Cells[COLS.Commission].Value = (grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal().ToDecimal() * Comm) / 100;

                            }



                        }
                        else
                        {
                            //if (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt() == 1)
                            //{

                                grdLister.Rows[i].Cells[COLS.Commission].Value = (grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal().ToDecimal() * numCommissionPercent.Value) / 100;
                            //}

                            //else if (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt() == 2)
                            //{
                            //    grdLister.Rows[i].Cells[COLS.Commission].Value = ((grdLister.Rows[i].Cells[COLS.Fares].Value.ToDecimal().ToDecimal() * numCommissionPercent.Value) / 100) + grdLister.Rows[i].Cells[COLS.AgentFees].Value.ToDecimal();


                            //}
                        }

                        grdLister.Rows[i].Cells[COLS.IsCommissionWise].Value = list[i][23].ToBool();

                        grdLister.Rows[i].Cells[COLS.CommissionType].Value = list[i][24].ToStr();

                        grdLister.Rows[i].Cells[COLS.CommissionValue].Value = list[i][25].ToDecimal();


                        ///
                }




                

                CalculateTotal();
                CalculateBalance();


                if (grdLister.Rows.Count > 0)
                    numCommissionPercent.Enabled = false;
                else
                    numCommissionPercent.Enabled = true;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void CalculateTotal()
        {
            try
            {

               

                double totalWeeks = (dtpTillDate.Value.ToDate().Subtract(dtpFromDate.Value.ToDate()).TotalDays) / 7;

                if (totalWeeks <= 0)
                    totalWeeks = 1;

                numpdaRent.Value = numPDARentPerWeek.Value * totalWeeks.ToInt();

                decimal pdaRent = numpdaRent.Value;

                txtTotalCashBooking.Text = string.Format("{0:£ #.##}", grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value.ToInt() == 0).Sum(c => c.Cells[COLS.Fares].Value.ToDecimal()));


                // change from Total to Fare Sum
                txtTotalAccountBooking.Text = string.Format("{0:£ #.##}", grdLister.Rows.Where(c => c.Cells[COLS.CompanyId].Value.ToStr() != "").Sum(c => c.Cells[COLS.Fares].Value.ToDecimal()));



                // change: TOtal to Fares SUM
                decimal jobsFareTotal=grdLister.Rows.Sum(c => c.Cells[COLS.Fares].Value.ToDecimal());
                txtInvoiceAmount.Text = string.Format("{0:£ #.##}",jobsFareTotal );

                if (txtInvoiceAmount.Text == "£ ")
                {
                    txtInvoiceAmount.Text = "£ 0";
                }
                if (txtTotalAccountBooking.Text == "£ ")
                {
                    txtTotalAccountBooking.Text = "£ 0";
                }
                if (txtCommsionTotal.Text == "£ ")
                {
                    txtCommsionTotal.Text = "£ 0";
                }


                numAgentFeeTotal.Value = grdLister.Rows.Sum(c => (c.Cells[COLS.AgentFees].Value.ToDecimal()).ToDecimal());


                decimal JobTotal = txtInvoiceAmount.Text.TrimStart('£', ' ').ToDecimal();
                txtCommsionTotal.Text = "0.00";

                decimal commissionTotal=0.00m;

                if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool())
                {
                    commissionTotal=grdLister.Rows.Sum(c => Math.Round(c.Cells[COLS.Commission].Value.ToDecimal(),2));
                    txtCommsionTotal.Text =commissionTotal.ToStr();
                }
                else
                {


                  
                    //commissionTotal=( (jobsFareTotal-  numAgentFeeTotal.Value)* numCommissionPercent.Value)/100;

                    commissionTotal = GetCommissionTotal(jobsFareTotal, numAgentFeeTotal.Value, numCommissionPercent.Value);

                    txtCommsionTotal.Text=commissionTotal.ToStr();
                }

                // Owned
                
               // decimal Owend = ((txtCommsionTotal.Text).ToDecimal()) - (txtTotalAccountBooking.Text.TrimStart('£', ' ').ToDecimal() + pdaRent);
                decimal owed=(pdaRent+ numAgentFeeTotal.Value + commissionTotal ) - txtTotalAccountBooking.Text.TrimStart('£', ' ').ToDecimal();
                txtDriverOwed.Text = owed.ToStr();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private decimal GetCommissionTotal(decimal jobsFareTotal,decimal agentFeeTotal,decimal commissionPerBookingPercent)
        {
            decimal rtn = 0.00m;


            //if (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt() == 1)
            //{
                rtn = ((jobsFareTotal - agentFeeTotal) * commissionPerBookingPercent) / 100;

           // }
            //else if (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt() == 2)
            //{

            //     rtn=((jobsFareTotal * commissionPerBookingPercent) / 100) + agentFeeTotal;
            //}

            return rtn;



        }

        private void ddlDriver_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int? DriverId = ddlDriver.SelectedValue.ToInt();

                if (DriverId == null)
                    return;

                Fleet_Driver obj = General.GetObject<Fleet_Driver>(c => c.Id == DriverId);
                if (obj != null)
                {


                    if (General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == obj.Id).Count() == 0)
                    {


                        txtOldBalance.Text = (obj.InitialBalance.ToDecimal()).ToStr();

                    }

                    else
                    {

                        var query = General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

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


                 
                    numCommissionPercent.Value =  obj.DriverCommissionPerBooking.ToDecimal();
                    numPDARentPerWeek.Value = obj.PDARent.ToDecimal();                   

                    CalculateTotal();
                    CalculateBalance();

                    if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool())
                    {
                        if (obj.Fleet_Driver_CommissionRanges.Count > 0)
                        {
                            listOfCommRange = obj.Fleet_Driver_CommissionRanges.ToList();
                        }
                        else
                        {
                            listOfCommRange = GetSystemCommissionRange();

                        }
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


            frmDriverCommisionTransactionReport frm = new frmDriverCommisionTransactionReport(1);

            var list = General.GetQueryable<vu_DriverCommision>(a => a.Id == id).OrderBy(c=>c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;


            frm.IsFareAndWaitingWise = this.IsFareAndWaitingWiseComm;

            frm.GenerateReport();

            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommisionTransactionReport1");



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

            frmDriverCommisionTransactionReport frm = new frmDriverCommisionTransactionReport(1);
            frm.IsFareAndWaitingWise = this.IsFareAndWaitingWiseComm;

            var list = General.GetQueryable<vu_DriverCommision>(a => a.Id == id).OrderBy(c => c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;


            frm.GenerateReport();

            frm.ExportReport(objMaster.Current.TransNo);
        }


        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            OnSave();
        }
        public override void OnNew()
        {
            try
            {
                txtTransNo.Text = string.Empty;
                ComboFunctions.FillDriverNoCombo(ddlDriver, c => c.DriverTypeId == 2);
                grdLister.Rows.Clear();
               

                txtDriverOwed.Text = "0.00";
                txtBalance.Text = "0.00";
                txtCommisionPayment.Text = "0.00";
                txtExtra.Text = "0.00";
                txtFuel.Text = "0.00";

                txtInvoiceAmount.Text = string.Empty;
                txtTotalCashBooking.Text = string.Empty;
                txtCommsionTotal.Text = string.Empty;
                txtTotalAccountBooking.Text = string.Empty;
             
                txtOldBalance.Text = string.Empty;
              
                txtRent.Text = string.Empty;
                txtCommissionPaid.Text = string.Empty;

                ddlDriver.SelectedValue = null;
                numCommissionPercent.Value = 0.00m;
                numAgentFeeTotal.Value = 0.00m;
                numPDARentPerWeek.Value = 0.00m;
                numpdaRent.Value = 0.00m;
                numCommissionPercent.Enabled = true;
                btnSaveInvoice.Enabled = true;


                pnlRentPaid.Visible = false;
                lblRentPaid.Visible = false;

                btnExportPDF.Enabled = false;
                btnPrint.Enabled = false;
                btnSendEmail.Enabled = false;
                pnlPaid.Visible = true;

                ddlDriver.Enabled = true;
                radButton1.Enabled = true;
                btnPaymentHistory.Visible = false;

                objMaster.Clear();
                objMaster.New();
                objMaster.PrimaryKeyValue = null;
            }
            catch
            {


            }

        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;

            frmDriverCommisionTransactionReport frm = new frmDriverCommisionTransactionReport(1);
            frm.IsFareAndWaitingWise = this.IsFareAndWaitingWiseComm;

            var list = General.GetQueryable<vu_DriverCommision>(a => a.Id == id).OrderBy(c => c.PickupDate).ToList();
            int count = list.Count;

            frm.DataSource = list;


            frm.GenerateReport();

            frm.SendEmail(objMaster.Current.TransNo,objMaster.Current.Fleet_Driver.DefaultIfEmpty().Email.ToStr().Trim());
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
            try
            {
                CalculateBalance();
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
                    if (txtCommisionPayment.Text != "" && txtInvoiceAmount.Text != "" && Double.TryParse(txtCommisionPayment.Text, out dblValue))
                    {

                     //   decimal AccountjobTotal = txtInvoiceAmount.Text.TrimStart('£', ' ').ToDecimal();
                     
                        decimal OldBalance = txtOldBalance.Text == "" ? 0 : txtOldBalance.Text.ToDecimal();
                        decimal Total = txtDriverOwed.Text.ToDecimal();
                        decimal payment = txtCommisionPayment.Text == "" ? 0 : txtCommisionPayment.Text.ToDecimal();

                        string driverComm = txtDriverOwed.Text;

                        decimal Extra = txtExtra.Text == "" ? 0 : txtExtra.Text.ToDecimal();
                        decimal Fuel = txtFuel.Text == "" ? 0 : txtFuel.Text.ToDecimal();


                       // decimal pdaRent = numpdaRent.Value;


                        if (optDebit.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                            Total = Total + OldBalance  - Extra - Fuel;
                        else
                        {
                           Total = Total + OldBalance  + Extra - Fuel;
                        }
                                            
                        
                            decimal chk = (Total + payment);

                            txtBalance.Text = chk.ToStr();
                          
                      
                    }
                    else
                    {
                        txtCommisionPayment.Text = "0.00";
                    }
                }
                else
                {
                    //for update record
                    decimal OldFuel = objMaster.Current.Fuel.ToDecimal();
                    decimal OldExtra = objMaster.Current.Extra.ToDecimal();
                    Double dblValue;
                    if (txtCommisionPayment.Text != "" && txtInvoiceAmount.Text != "" && Double.TryParse(txtCommisionPayment.Text, out dblValue))
                    {
                        decimal AccountjobTotal = txtInvoiceAmount.Text.TrimStart('£', ' ').ToDecimal();
                      
                        decimal OldBalance = txtOldBalance.Text == "" ? 0 : txtOldBalance.Text.ToDecimal();
                     

                        decimal Total = objMaster.Current.Balance.Value.ToDecimal();



                        decimal payment = txtCommisionPayment.Text == "" ? 0 : txtCommisionPayment.Text.ToDecimal();

                        decimal Extra = txtExtra.Text == "" ? 0 : txtExtra.Text.ToDecimal();
                        decimal Fuel = txtFuel.Text == "" ? 0 : txtFuel.Text.ToDecimal();

                        decimal newfuel = Fuel - OldFuel;
                        decimal newExtra = Extra - OldExtra;

                        if (Fuel > OldFuel && Extra > OldExtra)
                        {
                            Total = Total - newExtra - newfuel;
                        }
                        else if (Fuel > OldFuel)
                        {
                            Total = Total + newfuel;
                        }
                        else if (Extra > OldExtra)
                        {
                            Total = Total + newExtra;
                        }
                      

                         decimal chk = (Total + payment);
                         txtBalance.Text = chk.ToStr();
                    
                    }
                    else
                    {
                        txtCommisionPayment.Text = "0.00";
                    }
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        private void ddlDayWise_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
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

        private void btnBookingReport_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void btnSingle_Click(object sender, EventArgs e)
        {
            if (objMaster.Current == null || objMaster.Current.Id == 0) return;
            long id = objMaster.Current.Id;


            frmDriverCommisionTransactionReport frm = new frmDriverCommisionTransactionReport(2);

            var list = General.GetQueryable<vu_DriverCommision>(a => a.Id == id).ToList();
            int count = list.Count;

            frm.DataSource = list;


            frm.GenerateReport();


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommisionTransactionReport1");

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
            // New

            txtCommisionPayment.Text = "";
            CalculateTotal();
            //

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

        

        private void optCredit_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            txtCommisionPayment.Text = "";
            CalculateTotal();
            CalculateBalance();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            OnNew();
        }

        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {

                if (objMaster.Current != null && objMaster.PrimaryKeyValue != null)
                {

                    frmSearchDriverCommissionPaymentHistory frm = new frmSearchDriverCommissionPaymentHistory("", objMaster.Current.DriverCommission_PaymentHistories.OrderBy(c => c.PaymentDate).ToList());

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


    }
}
