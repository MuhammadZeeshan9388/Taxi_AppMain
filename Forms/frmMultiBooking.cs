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
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls;
using System.Net;
using UI;
using System.Xml.Linq;
using Telerik.WinControls.Enumerations;
using System.Data.Linq;
using System.Xml;

namespace Taxi_AppMain
{

    public partial class frmMultiBooking : RadForm
    {

        private string _OldCustomerName;

        public string OldCustomerName
        {
            get { return _OldCustomerName; }
            set { _OldCustomerName = value; }
        }


        private string _OldMobileNo;

        public string OldMobileNo
        {
            get { return _OldMobileNo; }
            set { _OldMobileNo = value; }
        }
        private string _OldPhoneNo;

        public string OldPhoneNo
        {
            get { return _OldPhoneNo; }
            set { _OldPhoneNo = value; }
        }


        private string _OldEmail;

        public string OldEmail
        {
            get { return _OldEmail; }
            set { _OldEmail = value; }
        }

        private bool _IsExistingCustomer;

        public bool IsExistingCustomer
        {
            get { return _IsExistingCustomer; }
            set { _IsExistingCustomer = value; }
        }

        private bool _DisableUpdateReturnJob;

        public bool DisableUpdateReturnJob
        {
            get { return _DisableUpdateReturnJob; }
            set { _DisableUpdateReturnJob = value; }
        }

        private string _AddedBy;

        public string AddedBy
        {
            get { return _AddedBy; }
            set { _AddedBy = value; }
        }

        private int? _ReturnVehicleTypeId;

        public int? ReturnVehicleTypeId
        {
            get { return _ReturnVehicleTypeId; }
            set { _ReturnVehicleTypeId = value; }
        }


        private decimal _ReturnCustomerPrice;

        public decimal ReturnCustomerPrice
        {
            get { return _ReturnCustomerPrice; }
            set { _ReturnCustomerPrice = value; }
        }


        private string _ReturnSpecialRequirement;

        public string ReturnSpecialRequirement
        {
            get { return _ReturnSpecialRequirement; }
            set { _ReturnSpecialRequirement = value; }
        }



        private string _ReturnFromAddress;

        public string ReturnFromAddress
        {
            get { return _ReturnFromAddress; }
            set { _ReturnFromAddress = value; }
        }
        private string _ReturnToAddress;

        public string ReturnToAddress
        {
            get { return _ReturnToAddress; }
            set { _ReturnToAddress = value; }
        }

        private int? _ReturnToLocIdv;

        public int? ReturnToLocIdv
        {
            get { return _ReturnToLocIdv; }
            set { _ReturnToLocIdv = value; }
        }


        BackgroundWorker worker = null;
        public struct COLS
        {
            public static string Exclude = "Exclude";
            public static string DAY = "Day";
            public static string FromLocTypeId = "FromLocTypeId";
            public static string FromLocId = "FromLocId";
            public static string FromAddress = "From";
            public static string ToLocTypeId = "ToLocTypeId";
            public static string ToLocId = "ToLocId";
            public static string ToAddress = "To";
            public static string Account = "A/C";
            public static string AccountName = "AccountName";

            public static string Fare = "Fare";
            public static string ReturnFare = "Return Fare";


            public static string MASTERBOOKING = "Master";
            public static string PickupDate = "PickupDate";
            public static string ReturnPickupDate = "ReturnPickupDate";
            public static string JourneyTypeId = "JourneyTypeId";
            public static string AutoDespatch = "AutoDespatch";
            public static string AutoDespatchDateTime = "AutoDespatchDateTime";
            public static string DriverId = "DriverId";


            public static string FromDoorNo = "FromDoorNo";
            public static string FromStreet = "FromStreet";
            public static string FromPostCode = "FromPostCode";
            public static string ToDoorNo = "ToDoorNo";
            public static string ToStreet = "ToStreet";
            public static string ToPostCode = "ToPostCode";


            public static string ChangeBooking = "ChangeBooking";
        }

        public struct COLS_PICKUPS
        {
            public static string DAY = "Day";

            public static string PickupDate = "PickupDate";
            public static string PickupTime = "PickupTime";
            public static string ReturnPickupDate = "ReturnPickupDate";
            public static string ReturnPickupTime = "ReturnPickupTime";
            public static string Fare = "Fare";
            public static string RetFare = "RetFare";

        }


        private decimal _ReturnCustomerFares;

        public decimal ReturnCustomerFares
        {
            get { return _ReturnCustomerFares; }
            set { _ReturnCustomerFares = value; }
        }



        private Booking _objBookiing;

        public Booking ObjBookiing
        {
            get { return _objBookiing; }
            set { _objBookiing = value; }
        }

        List<Booking> objMasterBookingList = null;
        public frmMultiBooking(Booking objMasterBooking)
        {
            InitializeComponent();
            this.ObjBookiing = objMasterBooking;

            InitializeFormSettings();
            this.FormClosing += FrmMultiBooking_FormClosing;


        }
        private bool IsFormClosed = false;
        private void FrmMultiBooking_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (worker != null)
                {
                    if (worker.IsBusy)
                    {
                        if (DialogResult.No == MessageBox.Show("Bookings are not fully saved! " + Environment.NewLine + "Do you still want to close this form ?", "Save Bookings", MessageBoxButtons.YesNo))
                        {
                            e.Cancel = true;
                            return;
                        }

                    }

                    IsFormClosed = true;
                    worker.CancelAsync();
                    worker.Dispose();
                    worker = null;

                }


            }
            catch
            {


            }
        }

        private void SetCashAccount(int accTypeId)
        {
            if (accTypeId == Enums.ACCOUNT_TYPE.CASH)
            {

                SetCashPaymentType();
            }
            else if (accTypeId == 3)
            {
                SetCreditCardPaymentType();
            }

            else
            {

                SetAccountPaymentType();
            }

        }

        private void SetCashPaymentType()
        {

            ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;
        }


        private void SetCreditCardPaymentType()
        {

            ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CREDIT_CARD;
        }


        private void SetAccountPaymentType()
        {
            ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.BANK_ACCOUNT;

        }



        private void InitializeFormSettings()
        {
            try
            {

                optDaily.ToggleState = ToggleState.On;
                FormatBookingGrid();

                FormatDailyPickupsGrid();
                FormatWeeklyPickupsGrid();

                //  FormatPickupsGrid();
                grdBookings.ViewCellFormatting += new CellFormattingEventHandler(grdBookings_ViewCellFormatting);
                grdBookings.KeyDown += new KeyEventHandler(grdBookings_KeyDown);
                this.Shown += new EventHandler(frmMultiBooking_Shown);

                MapType = AppVars.objPolicyConfiguration.MapType.ToInt();
                //   chkAutoDespatch.Enabled = AppVars.objPolicyConfiguration.EnablePDA.ToBool() ? true : false;

                ComboFunctions.FillCompanyCombo(ddlCompany);
                ComboFunctions.FillPaymentTypeCombo(ddlPaymentType);

                chkIsCompanyRates.Checked = ObjBookiing.IsCompanyWise.ToBool();
                ddlCompany.SelectedValue = ObjBookiing.CompanyId;
                ddlDepartment.SelectedValue = ObjBookiing.DepartmentId;
                ddlPaymentType.SelectedValue = ObjBookiing.PaymentTypeId;


                numFareRate.Value = ObjBookiing.FareRate.ToDecimal();
                numReturnFare.Value = ObjBookiing.ReturnFareRate.ToDecimal();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        void grdBookings_KeyDown(object sender, KeyEventArgs e)
        {
            if (grdBookings.CurrentColumn == null || grdBookings.CurrentRow == null)
                return;

            try
            {
                if (grdBookings.CurrentRow.Cells[0].Value.ToBool())
                    grdBookings.CurrentRow.Cells[0].Value = false;
                else
                    grdBookings.CurrentRow.Cells[0].Value = true;
            }
            catch (Exception ex)
            {
            }
        }



        private int _MapType;

        public int MapType
        {
            get { return _MapType; }
            set { _MapType = value; }
        }


        private void FormatDailyPickupsGrid()
        {
            FormatPickupsGrid(grdPickupDates);


        }
        private void FormatWeeklyPickupsGrid()
        {
            //  FormatPickupsGrid(grdWeeklyPickupGrid);


        }

        private void FormatPickupsGrid(RadGridView grid)
        {
            grid.AllowAddNewRow = false;
            grid.ShowGroupPanel = false;
            grid.ShowRowHeaderColumn = false;
            grid.AllowEditRow = false;
            grid.EnableHotTracking = false;

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = COLS_PICKUPS.DAY;
            col.Name = COLS_PICKUPS.DAY;
            col.Width = 30;
            grid.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup Date";
            col.Width = 90;
            col.Name = COLS_PICKUPS.PickupDate;
            grid.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Time";
            col.Width = 45;
            col.Name = COLS_PICKUPS.PickupTime;
            grid.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Ret. Pickup Date";
            col.Width = 100;
            col.Name = COLS_PICKUPS.ReturnPickupDate;
            grid.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "Time";
            col.Width = 45;
            col.Name = COLS_PICKUPS.ReturnPickupTime;
            grid.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS_PICKUPS.Fare;
            col.Name = COLS_PICKUPS.Fare;
            col.Width = 60;
            grid.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Ret. Fare";
            col.Name = COLS_PICKUPS.RetFare;
            col.Width = 60;
            grid.Columns.Add(col);


        }


        void frmMultiBooking_Shown(object sender, EventArgs e)
        {
            try
            {
                numDays.Value = 1;


                grdBookings.RowCount = 1;

                if (dtpEndingDate != null)
                {
                    dtpEndingDate.Validated += new EventHandler(dtpEndingDate_Validated);


                }

                // Add Default Master Booking
                AddBooking(grdBookings.Rows[0], "1", ObjBookiing.PickupDateTime.ToDateTime(), ObjBookiing.ReturnPickupDateTime.ToDateTimeorNull(), ObjBookiing.FromLocTypeId, ObjBookiing.FromLocId, ObjBookiing.FromAddress,
                                         ObjBookiing.ToLocTypeId, ObjBookiing.ToLocId, ObjBookiing.ToAddress, ObjBookiing.FareRate.ToDecimal(), ObjBookiing.ReturnFareRate.ToDecimal());


                numDays.Focus();

                dtp_StartPickupDate.Value = ObjBookiing.PickupDateTime.ToDate();
                dtp_StartPickupTime.Value = ObjBookiing.PickupDateTime;

                dtpWeekPickupTime.Value = ObjBookiing.PickupDateTime;
                numFareRateWeek.Value = ObjBookiing.FareRate.ToDecimal();
                numCustFareRateWeek.Value = ObjBookiing.CustomerPrice.ToDecimal();
                numCompanyFareRateWeek.Value = ObjBookiing.CompanyPrice.ToDecimal();


                if (ObjBookiing.ReturnPickupDateTime == null)
                {
                    SetReturnPickupDate(ToggleState.Off);

                }
                else
                {
                    chkIsReturn.Checked = true;
                    dtp_ReturnStartPickupDate.Value = ObjBookiing.ReturnPickupDateTime.ToDateorNull();
                    dtp_ReturnStartPickupTime.Value = ObjBookiing.ReturnPickupDateTime;



                    chkReturnWeekJourney.Checked = true;
                    numWeekFareRateReturn.Value = ObjBookiing.ReturnFareRate.Value.ToDecimal();
                    numWeekCustFareRateReturn.Value = this.ReturnCustomerFares.ToDecimal();
                    numWeekCompanyFareRateReturn.Value = ObjBookiing.WaitingMins.ToDecimal();


                    dtpWeekReturnPickupTime.Value = ObjBookiing.ReturnPickupDateTime;

                }

                FillDaysCombo();
                AddPickupDate();

                ddlPickupDay.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlPickupDay_SelectedIndexChanged);


                numWeeks.Validated += new EventHandler(numWeeks_Validated);


                dtpStartingAt.Value = DateTime.Now.ToDate();

                dtpEndingDate.Value = dtpStartingAt.Value.Value.AddDays((numWeeks.Value.ToInt() * 7)).ToDate();
            }

            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        void numWeeks_Validated(object sender, EventArgs e)
        {
            try
            {

                if (dtpStartingAt != null)
                {



                    dtpEndingDate.Value = dtpStartingAt.Value.Value.AddDays((numWeeks.Value.ToInt() * 7)).ToDate();

                }
            }
            catch (Exception ex)
            {


            }

        }

        void dtpEndingDate_Validated(object sender, EventArgs e)
        {
            //try
            //{

            //    if (dtpStartingAt != null && dtpEndingDate != null)
            //    {

            //        TimeSpan ts = (dtpEndingDate.Value.ToDate() - dtpStartingAt.Value.ToDate());
            //        // Difference in days.
            //        int differenceInDays = ts.Days;
            //        // int Days = (differenceInDays * 7);
            //        decimal Weeks = Math.Ceiling((differenceInDays.ToDecimal() / 7));
            //        if (Weeks == 0)
            //        {
            //            Weeks = 1;
            //        }
            //        numWeeks.Value = Weeks; //differenceInDays;



            //        //  IsEndDateChanged = true;
            //        //dtpEndingAt.Value = dtTo;
            //        // dtpEndingAt.Value = (DateTime.Now.AddDays(Weeks * 7));
            //    }
            //}
            //catch (Exception ex)
            //{


            //}


            try
            {

                if (dtpStartingAt != null && dtpEndingDate != null)
                {

                    TimeSpan ts = (dtpEndingDate.Value.ToDate() - dtpStartingAt.Value.ToDate());
                    // Difference in days.
                    int differenceInDays = ts.Days;
                    // int Days = (differenceInDays * 7);
                    decimal Weeks = Math.Ceiling((differenceInDays.ToDecimal() / 7));
                    if (Weeks == 0)
                    {
                        Weeks = 1;
                    }
                    numWeeks.Value = Weeks; //differenceInDays;



                    //  IsEndDateChanged = true;
                    //dtpEndingAt.Value = dtTo;
                    // dtpEndingAt.Value = (DateTime.Now.AddDays(Weeks * 7));
                }
            }
            catch (Exception ex)
            {


            }

        }







        Font oldFont = new Font("Tahoma", 9, FontStyle.Regular);
        Font newFont = new Font("Tahoma", 9, FontStyle.Bold);


        private Color _HeaderRowBackColor = Color.SteelBlue;

        public Color HeaderRowBackColor
        {
            get { return _HeaderRowBackColor; }
            set { _HeaderRowBackColor = value; }
        }


        private Color _HeaderRowBorderColor = Color.DarkSlateBlue;

        public Color HeaderRowBorderColor
        {
            get { return _HeaderRowBorderColor; }
            set { _HeaderRowBorderColor = value; }
        }


        string cellValue = string.Empty;
        void grdBookings_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {

            if (e.CellElement is GridHeaderCellElement)
            {
                //    e.CellElement
                e.CellElement.BorderColor = _HeaderRowBorderColor;
                e.CellElement.BorderColor2 = _HeaderRowBorderColor;
                e.CellElement.BorderColor3 = _HeaderRowBorderColor;
                e.CellElement.BorderColor4 = _HeaderRowBorderColor;


                // e.CellElement.DrawBorder = false;
                e.CellElement.BackColor = _HeaderRowBackColor;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.Font = newFont;
                e.CellElement.ForeColor = Color.White;
                e.CellElement.DrawFill = true;
                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

            }

            else if (e.CellElement is GridDataCellElement)
            {

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;
                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
                e.CellElement.ForeColor = Color.Black;
                e.CellElement.Font = oldFont;


                cellValue = e.CellElement.RowInfo.Cells[COLS.MASTERBOOKING].Value.ToStr();

                if (e.CellElement.RowElement.IsSelected == true && cellValue != COLS.MASTERBOOKING)
                {

                    e.CellElement.RowElement.NumberOfColors = 1;
                    e.CellElement.RowElement.BackColor = Color.DeepSkyBlue;

                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.DeepSkyBlue;
                    e.CellElement.ForeColor = Color.White;
                    e.CellElement.Font = newFont;

                }


                else
                {
                    if (cellValue != COLS.MASTERBOOKING)
                        e.CellElement.RowElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.All);

                }

                if (cellValue == COLS.MASTERBOOKING)
                {

                    e.CellElement.RowElement.NumberOfColors = 1;
                    e.CellElement.RowElement.BackColor = Color.LightPink;

                    e.CellElement.NumberOfColors = 1;
                    e.CellElement.BackColor = Color.LightPink;
                    e.CellElement.ForeColor = Color.Black;
                    e.CellElement.Font = newFont;

                }
            }
        }

        private void FormatBookingGrid()
        {
            grdBookings.AllowAddNewRow = false;
            grdBookings.ShowGroupPanel = false;
            grdBookings.ShowRowHeaderColumn = false;
            grdBookings.AllowEditRow = true;
            grdBookings.EnableHotTracking = false;

            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = COLS.Exclude;
            cbcol.HeaderText = "Exclude";
            cbcol.Width = 55;
            grdBookings.Columns.Add(cbcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.DAY;
            col.Name = COLS.DAY;
            col.Width = 35;
            col.ReadOnly = true;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Pickup Date Time";
            col.ReadOnly = true;
            col.Width = 130;
            //    col.FormatString = "dd/MM/yyyy HH:mm";
            col.Name = COLS.PickupDate;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Return Pickup Date Time";
            col.Width = 165;
            col.ReadOnly = true;
            // col.FormatString = "dd/MM/yyyy HH:mm";
            col.Name = COLS.ReturnPickupDate;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromLocTypeId;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromLocId;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Master";
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromDoorNo;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromStreet;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.FromPostCode;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToDoorNo;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToStreet;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToPostCode;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.JourneyTypeId;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToLocTypeId;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = COLS.ToLocId;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.FromAddress;
            col.Name = COLS.FromAddress;
            col.ReadOnly = true;
            col.Width = 130;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.ToAddress;
            col.Name = COLS.ToAddress;
            col.ReadOnly = true;
            col.Width = 130;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.Account;
            col.Name = COLS.Account;
            col.IsVisible = false;
            grdBookings.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.HeaderText = "A/C";
            col.Name = COLS.AccountName;
            col.ReadOnly = true;
            col.Width = 70;
            grdBookings.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.HeaderText = COLS.Fare;
            col.Name = COLS.Fare;
            col.ReadOnly = true;
            col.Width = 55;
            grdBookings.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.HeaderText = "Ret. Fare";
            col.Name = COLS.ReturnFare;
            col.ReadOnly = true;
            col.Width = 75;
            grdBookings.Columns.Add(col);





            GridViewCommandColumn cmdCol = new GridViewCommandColumn();
            cmdCol.Width = 50;
            grdBookings.CommandCellClick += new CommandCellClickEventHandler(grdBookings_CommandCellClick);
            cmdCol.Name = COLS.ChangeBooking;
            cmdCol.UseDefaultText = true;
            cmdCol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdCol.DefaultText = "Edit";
            cmdCol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdBookings.Columns.Add(cmdCol);



        }

        void grdBookings_CommandCellClick(object sender, EventArgs e)
        {

            try
            {

                GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                if (gridCell.ColumnInfo.Name == COLS.ChangeBooking)
                {
                    GridViewRowInfo row = gridCell.RowInfo;

                    Booking obj = new Booking();
                    obj.VehicleTypeId = ObjBookiing.VehicleTypeId;

                    obj.FareRate = row.Cells[COLS.Fare].Value.ToDecimal();
                    obj.FromDoorNo = row.Cells[COLS.FromDoorNo].Value.ToStr();
                    obj.FromAddress = row.Cells[COLS.FromAddress].Value.ToStr();
                    obj.FromLocId = row.Cells[COLS.FromLocId].Value.ToIntorNull();
                    obj.FromLocTypeId = row.Cells[COLS.FromLocTypeId].Value.ToIntorNull();
                    obj.JourneyTypeId = row.Cells[COLS.JourneyTypeId].Value.ToIntorNull();
                    obj.PickupDateTime = row.Cells[COLS.PickupDate].Value.ToDateTimeorNull();
                    obj.ReturnFareRate = row.Cells[COLS.ReturnFare].Value.ToDecimal();
                    obj.ReturnPickupDateTime = row.Cells[COLS.ReturnPickupDate].Value.ToDateTimeorNull();

                    obj.ToDoorNo = row.Cells[COLS.ToDoorNo].Value.ToStr();
                    obj.ToAddress = row.Cells[COLS.ToAddress].Value.ToStr();
                    obj.ToLocId = row.Cells[COLS.ToLocId].Value.ToIntorNull();
                    obj.ToLocTypeId = row.Cells[COLS.ToLocTypeId].Value.ToIntorNull();


                    frmEditAdvBooking frm = new frmEditAdvBooking(obj, row.Cells[COLS.DAY].Value.ToStr());
                    frm.StartPosition = FormStartPosition.CenterParent;
                    frm.ShowDialog();

                    row.Cells[COLS.FromAddress].Value = frm.ObjBooking.FromAddress;
                    row.Cells[COLS.FromDoorNo].Value = frm.ObjBooking.FromDoorNo;
                    // row.Cells[COLS.FromAddress].Value = frm.ObjBooking.FromAddress;


                    row.Cells[COLS.FromLocId].Value = frm.ObjBooking.FromLocId;
                    row.Cells[COLS.FromLocTypeId].Value = frm.ObjBooking.FromLocTypeId;

                    row.Cells[COLS.JourneyTypeId].Value = frm.ObjBooking.JourneyTypeId;

                    row.Cells[COLS.PickupDate].Value = frm.ObjBooking.PickupDateTime;
                    row.Cells[COLS.ReturnPickupDate].Value = frm.ObjBooking.ReturnPickupDateTime;

                    row.Cells[COLS.Fare].Value = frm.ObjBooking.FareRate;
                    row.Cells[COLS.ReturnFare].Value = frm.ObjBooking.ReturnFareRate;

                    row.Cells[COLS.ToAddress].Value = frm.ObjBooking.ToAddress;
                    row.Cells[COLS.ToDoorNo].Value = frm.ObjBooking.ToDoorNo.ToStr();

                    row.Cells[COLS.ToLocId].Value = frm.ObjBooking.ToLocId;
                    row.Cells[COLS.ToLocTypeId].Value = frm.ObjBooking.ToLocTypeId;



                    frm.Dispose();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void AddBooking(GridViewRowInfo row, string day, DateTime pickupDate, DateTime? returnPickupDate, int? fromLocTypeId, int? fromLocId,
                                string fromAddress, int? toLocTypeId, int? toLocId, string toAddress, decimal fareRate, decimal returnFareRate)
        {

            row.Cells[COLS.DAY].Value = day;
            row.Cells[COLS.PickupDate].Value = string.Format("{0:dd/MM/yyyy HH:mm}", pickupDate);
            row.Cells[COLS.FromAddress].Value = fromAddress;
            row.Cells[COLS.ToAddress].Value = toAddress;
            row.Cells[COLS.Fare].Value = fareRate;

            row.Cells[COLS.FromLocTypeId].Value = fromLocTypeId;
            row.Cells[COLS.FromLocId].Value = fromLocId;

            row.Cells[COLS.ToLocTypeId].Value = toLocTypeId;
            row.Cells[COLS.ToLocId].Value = toLocId;

            row.Cells[COLS.FromDoorNo].Value = ObjBookiing.FromDoorNo.ToStr();
            row.Cells[COLS.FromStreet].Value = ObjBookiing.FromStreet.ToStr();
            row.Cells[COLS.FromPostCode].Value = ObjBookiing.FromPostCode.ToStr();

            row.Cells[COLS.ToDoorNo].Value = ObjBookiing.ToDoorNo.ToStr();
            row.Cells[COLS.ToStreet].Value = ObjBookiing.ToStreet.ToStr();
            row.Cells[COLS.ToPostCode].Value = ObjBookiing.ToPostCode.ToStr();





            row.Cells[COLS.Account].Value = ObjBookiing.CompanyId;
            row.Cells[COLS.AccountName].Value = ddlCompany.SelectedItem.DefaultIfEmpty().Text.ToStr().Trim();


            if (returnPickupDate != null)
            {
                row.Cells[COLS.JourneyTypeId].Value = Enums.JOURNEY_TYPES.RETURN;
                row.Cells[COLS.ReturnPickupDate].Value = string.Format("{0:dd/MM/yyyy HH:mm}", returnPickupDate);
                row.Cells[COLS.ReturnFare].Value = returnFareRate;
            }
            else
            {

                if (ObjBookiing.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.WAITANDRETURN)
                {
                    row.Cells[COLS.JourneyTypeId].Value = Enums.JOURNEY_TYPES.WAITANDRETURN;

                }
                else
                {

                    row.Cells[COLS.JourneyTypeId].Value = Enums.JOURNEY_TYPES.ONEWAY;
                }
            }

            if (day == "1")
            {
                row.Cells[COLS.MASTERBOOKING].Value = COLS.MASTERBOOKING;

            }

        }

        private void btnCreateBooking_Click(object sender, EventArgs e)
        {
            try
            {
                grdBookings.Rows.Clear();


                int days = numDays.Value.ToInt();

                ObjBookiing.IsCompanyWise = chkIsCompanyRates.Checked;
                ObjBookiing.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                ObjBookiing.DepartmentId = ddlDepartment.SelectedValue.ToIntorNull();
                ObjBookiing.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();


                CreateBooking(days, chkSame.Checked, chkOrigin.Checked, chkDestination.Checked);



                grdBookings.CurrentRow = grdBookings.RowCount == 1 ? grdBookings.Rows[0] : grdBookings.Rows[1];
                SelectRowDetail(grdBookings.CurrentRow);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void CreateBooking(int days, bool sameAsMasterBooking, bool sameOrigin, bool sameDestination)
        {


            string fromAddress = ObjBookiing.FromAddress;
            string toAddress = ObjBookiing.ToAddress;
            decimal fareRate = ObjBookiing.FareRate.ToDecimal();
            decimal retFareRate = ObjBookiing.ReturnFareRate.ToDecimal();

            int? fromLocTypeId = ObjBookiing.FromLocTypeId;
            int? fromLocId = ObjBookiing.FromLocId;

            int? toLocTypeId = ObjBookiing.ToLocTypeId;
            int? toLocId = ObjBookiing.ToLocId;





            if (sameAsMasterBooking == false)
            {

                if (sameOrigin == false && sameDestination == false)
                {
                    fromLocId = null;
                    fromAddress = string.Empty;

                    toLocId = null;
                    toAddress = string.Empty;
                }


                if (sameOrigin == false)
                {
                    fromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
                    fromLocId = null;
                    fromAddress = string.Empty;
                }

                if (sameDestination == false)
                {

                    toLocTypeId = Enums.LOCATION_TYPES.ADDRESS;
                    toLocId = null;
                    toAddress = string.Empty;
                }


                if (sameDestination == false || sameOrigin == false)
                {
                    fareRate = 0;

                }
            }

            grdBookings.RowCount = days;

            DateTime? pickupDate = null;
            TimeSpan pickupTime = TimeSpan.Zero;


            DateTime? returnpickupDate = null;
            TimeSpan returnpickupTime = TimeSpan.Zero;

            string day = string.Empty;
            GridViewRowInfo row = null;
            string returnPickupTimeStr = string.Empty;

            bool skipWeekend = chkSkipWeekEnd.Checked;
            int cnter = 0;

            for (int i = 1; i <= days; i++)
            {
                day = i.ToStr();
                returnpickupDate = null;
                retFareRate = 0;
                row = grdPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS_PICKUPS.DAY].Value.ToStr() == day);

                if (row != null)
                {
                    TimeSpan.TryParse(row.Cells[COLS_PICKUPS.PickupTime].Value.ToStr(), out pickupTime);
                    pickupDate = row.Cells[COLS_PICKUPS.PickupDate].Value.ToDate() + pickupTime;

                    returnPickupTimeStr = row.Cells[COLS_PICKUPS.ReturnPickupTime].Value.ToStr().Trim();
                    if (!string.IsNullOrEmpty(returnPickupTimeStr))
                    {

                        TimeSpan.TryParse(returnPickupTimeStr, out returnpickupTime);
                        returnpickupDate = row.Cells[COLS_PICKUPS.ReturnPickupDate].Value.ToDate() + returnpickupTime;
                    }

                    fareRate = row.Cells[COLS_PICKUPS.Fare].Value.ToDecimal();
                    retFareRate = row.Cells[COLS_PICKUPS.RetFare].Value.ToDecimal();
                }
                else
                {


                    pickupDate = ObjBookiing.PickupDateTime.Value.AddDays((i + cnter) - 1);


                    if (ObjBookiing.ReturnPickupDateTime != null)
                    {

                        returnpickupDate = pickupDate.Value.Date + ObjBookiing.ReturnPickupDateTime.Value.TimeOfDay;
                        retFareRate = ObjBookiing.ReturnFareRate.ToDecimal();
                    }



                    //if (cnter > 0)
                    //{
                    //    cnter = 0;
                    //}

                    if (skipWeekend)
                    {
                        if (pickupDate.Value.DayOfWeek == DayOfWeek.Saturday)
                        {
                            pickupDate = pickupDate.Value.AddDays(2);
                            cnter += 2;
                        }
                        else if (pickupDate.Value.DayOfWeek == DayOfWeek.Sunday)
                        {
                            pickupDate = pickupDate.Value.AddDays(1);
                            cnter += 1;

                        }



                        if (returnpickupDate != null)
                        {

                            if (returnpickupDate.Value.DayOfWeek == DayOfWeek.Saturday)
                            {
                                returnpickupDate = returnpickupDate.Value.AddDays(2);
                                //   cnter += 2;
                            }
                            else if (returnpickupDate.Value.DayOfWeek == DayOfWeek.Sunday)
                            {
                                returnpickupDate = returnpickupDate.Value.AddDays(1);
                                //   cnter += 1;

                            }
                        }
                    }


                    fareRate = ObjBookiing.FareRate.ToDecimal();
                    retFareRate = ObjBookiing.ReturnFareRate.ToDecimal();
                }

                AddBooking(grdBookings.Rows[i - 1], day, pickupDate.ToDateTime(), returnpickupDate.ToDateTimeorNull(), fromLocTypeId, fromLocId,
                                      fromAddress, toLocTypeId, toLocId, toAddress, fareRate, retFareRate);

            }



        }

        class CurrentRow
        {
            public int index;
            public string UpdateValue;
            public int Total;
        }
        private void InitializeWorker()
        {
            if (worker == null)
            {
                worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged; ;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted; ;

            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string result = (string)e.Result;
            lblUpdate.Text = result;
            btnSaveBooking.Enabled = true;
            btnWeeklyCreateBooking.Enabled = true;
            btnCreateBooking.Enabled = true;
            if (this.Saved)
            {
                this.Close();
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentRow cr = e.UserState as CurrentRow;
            if (cr != null)
            {
                lblUpdate.Text = "Saving (" + cr.UpdateValue + ") " + (cr.index) + " out of " + cr.Total + "";
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Save();
        }

        List<DateTime> listOfPickUpDateTime = new List<DateTime>();
        DateTime nowDate;
        CurrentRow cr = null;
        private bool Save()
        {
            bool IsSuccess = true;
            try
            {
                int? bookingTypeId = ObjBookiing.BookingTypeId != null ? ObjBookiing.BookingTypeId : Enums.BOOKING_TYPES.LOCAL;

                // int? bookingTypeId = Enums.BOOKING_TYPES.LOCAL;

                int? vehicleTypeId = ObjBookiing.VehicleTypeId;
                int? paymentTypeId = ObjBookiing.PaymentTypeId;
                int totalPassengers = ObjBookiing.NoofPassengers.ToInt();
                int totalLuggages = ObjBookiing.NoofLuggages.ToInt();

                string customerName = ObjBookiing.CustomerName.ToStr();
                string customerPhoneNo = ObjBookiing.CustomerPhoneNo.ToStr();
                string customerMobileNo = ObjBookiing.CustomerMobileNo.ToStr().Trim();


                bool IsConfirmedDriver = chkConfirmDriver.Checked;
                int? driverId = ddlDriver.SelectedValue.ToIntorNull();

                bool IsConfirmedReturnDriver = chkReturnConfirm.Checked;
                int? returnDriverId = ddlReturnAllocatedDriver.SelectedValue.ToIntorNull();

                Booking objMaster = null;
                int rowIndex = 0;
                nowDate = DateTime.Now;
                GridViewRowInfo row = null;



                AdvanceBookingBO objAdvBO = new AdvanceBookingBO();
                objAdvBO.New();
                objAdvBO.Current.CustomerName = customerName;
                objAdvBO.Current.CustomerTelephoneNo = customerPhoneNo;
                objAdvBO.Current.CustomerMobileNo = customerMobileNo;
                objAdvBO.Current.CustomerEmail = ObjBookiing.CustomerEmail.ToStr().Trim();
                objAdvBO.Current.FromAddress = ObjBookiing.FromAddress.ToStr().Trim();
                objAdvBO.Current.ToAddress = ObjBookiing.ToAddress.ToStr().Trim();



                objAdvBO.Current.AddOn = DateTime.Now;
                objAdvBO.Current.AddLog = AppVars.LoginObj.UserName.ToStr();
                objAdvBO.Current.AddBy = AppVars.LoginObj.LuserId.ToIntorNull();

                objAdvBO.Save();


                long? advanceBookingId = objAdvBO.Current.Id;

                Booking objBooking = new Booking();
                List<Booking> objBookingList = new List<Booking>();
                // objBookingList.Add(objBooking);
                cr = new CurrentRow();
                cr.Total = grdBookings.Rows.Where(c => (!string.IsNullOrEmpty(c.Cells[COLS.FromAddress].Value.ToStr())) && (c.Cells[COLS.Exclude].Value.ToBool() == false)).Count();
                // cr.UpdateValue = item.PostCode;

                bool IsSavedCustomer = false;
                string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                for (int day = 1; day <= grdBookings.RowCount; day++)
                {
                    try
                    {
                        rowIndex = day - 1;
                        row = grdBookings.Rows[rowIndex];

                        if (row.Cells[COLS.DAY].Value == null || row.Cells[COLS.Exclude].Value.ToBool())
                            continue;


                        if (IsFormClosed)
                            break;



                        objMaster = new Booking();

                        objMaster.BookingTypeId = bookingTypeId;

                        objMaster.BookingDate = nowDate;


                        objMaster.FromAddress = row.Cells[COLS.FromAddress].Value.ToStr();
                        objMaster.ToAddress = row.Cells[COLS.ToAddress].Value.ToStr();

                        objMaster.FareRate = numFareRateWeek.Value.ToDecimal();
                        objMaster.ReturnFareRate = numWeekFareRateReturn.Value.ToDecimal();





                        objMaster.CompanyPrice = numCompanyFareRateWeek.Value.ToDecimal();
                        objMaster.WaitingMins = numWeekCompanyFareRateReturn.Value.ToDecimal();

                        objMaster.CustomerPrice = numFareRateWeek.Value.ToDecimal();
                        //objMaster.ReturnCustomerPrice = numWeekFareRateReturn.Value.ToDecimal();



                        objMaster.PickupDateTime = row.Cells[COLS.PickupDate].Value.ToDateTime();
                        objMaster.ReturnPickupDateTime = row.Cells[COLS.ReturnPickupDate].Value.ToDateTimeorNull();

                        listOfPickUpDateTime.Add(objMaster.PickupDateTime.ToDateTime());



                        try
                        {
                            objMaster.AutoDespatchTime = objMaster.PickupDateTime.Value.AddMinutes(-ObjBookiing.DeadMileage.ToDecimal().ToInt()).ToDateTime();
                            objMaster.DeadMileage = ObjBookiing.DeadMileage.ToDecimal();
                        }
                        catch
                        {

                        }

                        objMaster.FromLocTypeId = row.Cells[COLS.FromLocTypeId].Value.ToIntorNull();
                        objMaster.ToLocTypeId = row.Cells[COLS.ToLocTypeId].Value.ToIntorNull();

                        objMaster.FromLocId = row.Cells[COLS.FromLocId].Value.ToIntorNull();
                        objMaster.ToLocId = row.Cells[COLS.ToLocId].Value.ToIntorNull();

                        objMaster.FromDoorNo = row.Cells[COLS.FromDoorNo].Value.ToStr();
                        objMaster.FromStreet = row.Cells[COLS.FromStreet].Value.ToStr();
                        objMaster.FromPostCode = row.Cells[COLS.FromPostCode].Value.ToStr();
                        objMaster.ToDoorNo = row.Cells[COLS.ToDoorNo].Value.ToStr();
                        objMaster.ToStreet = row.Cells[COLS.ToStreet].Value.ToStr();
                        objMaster.ToPostCode = row.Cells[COLS.ToPostCode].Value.ToStr();


                        objMaster.SubcompanyId = ObjBookiing.SubcompanyId;
                        objMaster.JourneyTypeId = row.Cells[COLS.JourneyTypeId].Value.ToInt();

                        objMaster.IsCompanyWise = chkIsCompanyRates.Checked.ToBool();
                        objMaster.CompanyId = ObjBookiing.CompanyId;
                        objMaster.DepartmentId = ObjBookiing.DepartmentId;
                        objMaster.PaymentTypeId = paymentTypeId;

                        objMaster.IsQuotation = ObjBookiing.IsQuotation.ToBool();

                        objMaster.OrderNo = ObjBookiing.OrderNo.ToStr();
                        objMaster.PupilNo = ObjBookiing.PupilNo.ToStr();


                        objMaster.VehicleTypeId = vehicleTypeId;
                        objMaster.CustomerName = customerName;
                        objMaster.CustomerPhoneNo = customerPhoneNo;
                        objMaster.CustomerMobileNo = customerMobileNo;
                        objMaster.CustomerEmail = ObjBookiing.CustomerEmail.ToStr().Trim();



                        objMaster.AutoDespatch = ObjBookiing.AutoDespatch.ToBool();


                        objMaster.IsCommissionWise = ObjBookiing.IsCommissionWise.ToBool();
                        objMaster.DriverCommission = ObjBookiing.DriverCommission.ToDecimal();
                        objMaster.DriverCommissionType = ObjBookiing.DriverCommissionType.ToStr();

                        objMaster.BookedBy = ObjBookiing.BookedBy.ToStr();


                        objMaster.SpecialRequirements = ObjBookiing.SpecialRequirements.ToStr();

                        objMaster.AutoDespatch = ObjBookiing.AutoDespatch.ToBool();
                        objMaster.IsBidding = ObjBookiing.IsBidding.ToBool();

                        IList<Booking_ViaLocation> savedList = objMaster.Booking_ViaLocations;
                        List<Booking_ViaLocation> listofDetail = (from r in ObjBookiing.Booking_ViaLocations.ToList()
                                                                  select new Booking_ViaLocation
                                                                  {

                                                                      ViaLocTypeId = r.ViaLocTypeId.Value.ToIntorNull(),
                                                                      ViaLocTypeLabel = r.ViaLocTypeLabel.ToStr(),
                                                                      ViaLocTypeValue = r.ViaLocTypeValue.ToStr(),

                                                                      ViaLocId = r.ViaLocId.ToIntorNull(),
                                                                      ViaLocLabel = r.ViaLocLabel.ToStr(),
                                                                      ViaLocValue = r.ViaLocValue.ToStr()

                                                                  }).ToList();


                        Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);

                        objMaster.AddOn = DateTime.Now;
                        objMaster.AddLog = AppVars.LoginObj.UserName.ToStr();
                        objMaster.AddBy = AppVars.LoginObj.LuserId.ToIntorNull();



                        objMaster.AdvanceBookingId = advanceBookingId;



                        objMaster.DriverId = driverId;



                        objMaster.IsConfirmedDriver = IsConfirmedDriver;


                        if (objMaster.ReturnPickupDateTime != null)
                        {
                            //objMaster.ReturnConfirmAllocaedDriver = IsConfirmedReturnDriver;
                            objMaster.ReturnDriverId = returnDriverId;
                        }


                        if (AppVars.objPolicyConfiguration.ShowAreaWithPlots.ToBool() && AppVars.objPolicyConfiguration.EnablePDA.ToBool())
                        {
                            if (objMaster.FromPostCode.ToStr().Trim() == string.Empty)
                                objMaster.FromPostCode = General.GetPostCodeMatch(objMaster.FromAddress.ToStr().Trim());


                            if (objMaster.FromPostCode.ToStr().Trim() != string.Empty)
                            {
                                if ((LastPickupPostCode == string.Empty || LastPickupPostCode != objMaster.FromPostCode.ToStr().Trim()))
                                {

                                    objMaster.ZoneId = General.GetZoneId(objMaster.FromPostCode);
                                    LastPickupPostCode = objMaster.FromPostCode.ToStr().Trim();
                                    lastPickupZoneId = objMaster.ZoneId;


                                  
                                }
                                else
                                {

                                    objMaster.ZoneId = lastPickupZoneId;

                                 //   objMaster.ExtraMile = ObjBookiing.ExtraMile.ToDecimal();
                                 //   objMaster.DeadMileage = ObjBookiing.DeadMileage.ToDecimal();
                                }
                            }


                            if (objMaster.ToPostCode.ToStr().Trim() == string.Empty)
                                objMaster.ToPostCode = General.GetPostCodeMatch(objMaster.ToAddress.ToStr().Trim());


                            if (objMaster.ToPostCode.ToStr().Trim() != string.Empty)
                            {
                                if ((LastDropOffPostCode == string.Empty || LastDropOffPostCode != objMaster.ToPostCode.ToStr().Trim()))
                                {

                                    objMaster.DropOffZoneId = General.GetZoneId(objMaster.ToPostCode);
                                    LastDropOffPostCode = objMaster.ToPostCode.ToStr().Trim();
                                    lastDropOffZoneId = objMaster.DropOffZoneId;
                                }
                                else
                                {
                                    objMaster.DropOffZoneId = lastDropOffZoneId;
                                }
                            }
                        }

                        objMaster.CallRefNo = ObjBookiing.CallRefNo.ToStr().Trim();

                        //if (day > 1)
                        //    objMaster.CheckCustomerValidation = false;




                        objMaster.AgentCommission = ObjBookiing.AgentCommission.ToDecimal();
                        objMaster.AgentCommissionPercent = ObjBookiing.AgentCommissionPercent;
                        objMaster.JobTakenByCompany = ObjBookiing.JobTakenByCompany.ToBool();
                        objMaster.FromFlightNo = ObjBookiing.FromFlightNo.ToStr();

                        // objMaster.CheckServiceCharges = AppVars.objPolicyConfiguration.SendBookingCompletionEmail.ToBool();
                        objMaster.ServiceCharges = ObjBookiing.ServiceCharges.ToDecimal();
                        // objMaster.ReturnCustomerPrice = this.ReturnCustomerFares.ToDecimal();



                       if(ObjBookiing.EscortId!=null)
                        {

                            objMaster.EscortId = ObjBookiing.EscortId;
                        }

                        objMaster.EscortPrice = ObjBookiing.EscortPrice.ToDecimal();


                        //objMaster.Save();
                        objBookingList.Add(objMaster);

                        //if (worker != null)
                        //{
                        //    cr.UpdateValue = objMaster.BookingNo;
                        //    cnter++;
                        //    cr.index = cnter;
                        //    worker.ReportProgress(cnter, cr);
                        //}
                    }
                    catch (Exception ex)
                    {
                        IsSuccess = false;
                        //if (objMaster.Errors.Count > 0)
                        //    ENUtils.ShowMessage(objMaster.ShowErrors());
                        //else
                        //{
                        //    ENUtils.ShowMessage(ex.Message);

                        //}
                        break;
                    }

                }

                LastOveralNumber = 0;
                string prefix = string.Empty;

                int? subCompanyId = objBookingList[0].SubcompanyId;

                // Current.BookingNo = new SysPolicy_DocumentNumberSetupBO().GetSequenceNumber(Enums.GEN_DOCUMENTS.BOOKINGNO, Current.SubcompanyId);
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var obj = db.Gen_SysPolicy_DocumentNumberSetups.FirstOrDefault(c => c.DocumentId == Enums.GEN_DOCUMENTS.BOOKINGNO && c.SubcompanyId ==subCompanyId );

                    LastOveralNumber = obj.LastNumber.ToLong();
                    prefix = obj.Prefix;

                }



                    int BookingGroup = 50;

                    if (objBookingList.Count >= BookingGroup)
                    {
                        int Skip = 0;
                        int Take = BookingGroup;
                        int TotalLoop = (objBookingList.Count / BookingGroup);
                        for (int i = 0; i < TotalLoop; i++)
                        {
                            objMasterBookingList = new List<Booking>();
                            var objBookingList2 = objBookingList.Skip(Skip).Take(Take).ToList();
                            foreach (var item in objBookingList2)
                            {

                                WithInSave(item, false, prefix);



                            }
                            Skip += Take;
                            IsSuccess = SaveAll(objMasterBookingList);

                        }
                        int Remaing = (objBookingList.Count % BookingGroup);

                        var objBookingList3 = objBookingList.Skip(Skip).Take(Remaing).ToList();
                        objMasterBookingList = new List<Booking>();
                        foreach (var item in objBookingList3)
                        {
                            WithInSave(item, false, prefix);
                        }
                        IsSuccess = SaveAll(objMasterBookingList);
                    }
                    else
                    {
                        objMasterBookingList = new List<Booking>();
                        foreach (var item in objBookingList)
                        {
                            WithInSave(item, false, prefix);
                        }
                        IsSuccess = SaveAll(objMasterBookingList);
                    }
                    this.Saved = true;


                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var obj = db.Gen_SysPolicy_DocumentNumberSetups.FirstOrDefault(c => c.DocumentId == Enums.GEN_DOCUMENTS.BOOKINGNO && c.SubcompanyId == subCompanyId);


                    obj.LastNumber = LastOveralNumber;
                    db.SubmitChanges();

                }

                SendAdvanceBookingConfirmationText(customerMobileNo);

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
                IsSuccess = false;
            }

            return IsSuccess;

        }
        private bool SaveAll(List<Booking> objMasterBookingList)
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.Bookings.InsertAllOnSubmit(objMasterBookingList);
                    db.SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        long LastOveralNumber = 0;

        private void WithInSave(Booking Current, bool CheckCustomerValidation,string preFix)
        {
            string mobNo = Current.CustomerMobileNo.ToStr().Trim();
            string telNo = Current.CustomerPhoneNo.ToStr().Trim();

            string customerName = Current.CustomerName.ToStr().Trim();

            string email = Current.CustomerEmail.ToStr().Trim();

            long id = Current.Id;
            if (id == 0)
            {
                LastOveralNumber++;
              

                string bookingNO = preFix + LastOveralNumber.ToStr();


                if (bookingNO.ToStr().Trim().Length > 0)
                    Current.BookingNo = bookingNO.ToStr().Trim();
                else               
                   Current.BookingNo = new SysPolicy_DocumentNumberSetupBO().GetSequenceNumber(Enums.GEN_DOCUMENTS.BOOKINGNO, Current.SubcompanyId);



               

            }

            if (CheckCustomerValidation)
            {

                SaveCustomer(customerName, telNo, mobNo, email, Current);
            }

            if (Current.BookingStatusId == null)
                Current.BookingStatusId = Enums.BOOKINGSTATUS.WAITING;


            Current.CustomerMobileNo = mobNo;
            Current.CustomerPhoneNo = telNo;

            Current.ParkingCharges = Current.ParkingCharges.ToDecimal();
            Current.WaitingCharges = Current.WaitingCharges.ToDecimal();
            Current.ExtraDropCharges = Current.ExtraDropCharges.ToDecimal();
            Current.MeetAndGreetCharges = Current.MeetAndGreetCharges.ToDecimal();
            Current.CongtionCharges = Current.CongtionCharges.ToDecimal();



            if (Current.CompanyId == null)
            {
                Current.TotalCharges = Current.FareRate.ToDecimal()
                             + Current.ExtraDropCharges.ToDecimal() + Current.MeetAndGreetCharges.ToDecimal() + Current.CongtionCharges.ToDecimal() + Current.ServiceCharges.ToDecimal();
            }
            else
            {
                Current.TotalCharges = Current.CompanyPrice.ToDecimal() + Current.ParkingCharges.ToDecimal() + Current.WaitingCharges.ToDecimal() + Current.ExtraDropCharges.ToDecimal() + Current.ServiceCharges.ToDecimal();

            }

            int paymentTypeId = Current.PaymentTypeId.ToInt();
            int? companyId = Current.CompanyId;
            if ((paymentTypeId != Enums.PAYMENT_TYPES.CASH && companyId == null))
            {


                if (Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD || Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)
                {

                    Gen_Company objCompany = GeneralBLL.GetObject<Gen_Company>(c => c.SysGenId == Enums.SYSGEN_COMPANY.CREDITCARD);
                    if (objCompany != null)
                        Current.CompanyId = objCompany.Id;
                }
                else
                {
                    if (Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.PAYPAL)
                    {
                        Gen_Company objCompany = GeneralBLL.GetObject<Gen_Company>(c => c.CompanyName.ToUpper() == "PAYPAL");

                        if (objCompany != null)
                            Current.CompanyId = objCompany.Id;
                    }
                    else if (Current.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.BANK_ACCOUNT)
                    {
                        Gen_Company objCompany = GeneralBLL.GetObject<Gen_Company>(c => c.CompanyName.ToUpper() == "ACCOUNT");

                        if (objCompany != null)
                            Current.CompanyId = objCompany.Id;
                    }
                    else if (Current.PaymentTypeId.ToInt() == 8)
                    {
                        Gen_Company objCompany = GeneralBLL.GetObject<Gen_Company>(c => c.CompanyName.ToUpper() == "Pay by Phone");

                        if (objCompany != null)
                            Current.CompanyId = objCompany.Id;
                    }



                }


                if (Current.CompanyId != null)
                {

                    Current.IsCompanyWise = true;

                }
            }


            if (Current.IsBidding == null)
                Current.IsBidding = false;

            if (Current.InvoicePaymentTypeId == null)
                Current.InvoicePaymentTypeId = Enums.INVOICE_PAYMENTTYPES.UNPAID;


            if (Current.BookingTypeId == null)
            {
                Current.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;
            }


            if (Current.IsQuotation == null)
            {
                Current.IsQuotation = false;
            }


            if (string.IsNullOrEmpty(Current.FromPostCode.ToStr()) || Current.FromAddress.ToStr().Contains(Current.FromPostCode.ToStr()) == false)
            {
                Current.FromPostCode = GeneralBLL.GetPostCodeMatch(Current.FromAddress.ToStr().ToUpper());
            }

            if (string.IsNullOrEmpty(Current.ToPostCode.ToStr()) || Current.ToAddress.ToStr().Contains(Current.ToPostCode.ToStr()) == false)
            {
                Current.ToPostCode = GeneralBLL.GetPostCodeMatch(Current.ToAddress.ToStr());
            }




            Current.FromOther = !string.IsNullOrEmpty(Current.FromPostCode.ToStr()) ? Current.FromAddress.ToStr().Trim().Replace(Current.FromPostCode, "").ToStr().Trim() : Current.FromAddress.ToStr().Trim();
            Current.ToOther = !string.IsNullOrEmpty(Current.ToPostCode.ToStr()) ? Current.ToAddress.ToStr().Trim().Replace(Current.ToPostCode, "").ToStr().Trim() : Current.ToAddress.ToStr().Trim();






            if (Current.Id > 0 && Current.MasterJobId != null && Current.Booking1 != null)
            {
                Current.Booking1.ReturnFareRate = Current.FareRate.ToDecimal();

                Current.Booking1.WaitingMins = Current.CompanyPrice.ToDecimal();


                //  Current.Booking1.ReturnFareRate = Current.ReturnFareRate.ToDecimal();


                //  Current.Booking1.returnv = Current.VehicleTypeId;
            }


            //  Via 1 : PENTAX H SOUTH HILL AVENUE HARROW HA2 0DU , Via 2 : HEATHROW TERMINAL 4, TW6 2GA , Via 3 : GOURMET BURGER 333 PUTNEY BRIDGE RD, SW15 2PG , Via 4 : VICTORIA ROAD RUISLIP HA4 0AA
            if (Current.Booking_ViaLocations.Count > 0)
            {
                int cnter = 0;
                Current.ViaString = string.Join(",", Current.Booking_ViaLocations.Select(args => "Via " + (++cnter) + " : " + args.ViaLocValue.Replace(",", " ")).ToArray<string>());
                
            }

            if (Current.SubcompanyId == null)
            {
                int subCompanyId = GeneralBLL.GetObject<Gen_SubCompany>(c => c.IsSysGen != null && c.IsSysGen == true).DefaultIfEmpty().Id;

                if (subCompanyId > 0)
                {
                    Current.SubcompanyId = subCompanyId;

                }
            }
            if (Current.JourneyTypeId.ToInt() == Enums.JOURNEY_TYPES.RETURN)
            {

                CreateReturnJob(Current,preFix);
            }
            else
            {
                objMasterBookingList.Add(Current);

                //using (TaxiDataContext db = new TaxiDataContext())
                //{
                //    db.Bookings.InsertOnSubmit(Current);
                //    db.SubmitChanges();
                //}
                if (worker != null)
                {
                    cr.UpdateValue = Current.BookingNo;
                    cnter++;
                    cr.index = cnter;
                    worker.ReportProgress(cnter, cr);
                }
            }
        }
        int cnter = 0;
        private void CreateReturnJob(Booking Current,string bookingno)
        {


            if (this.DisableUpdateReturnJob)
                return;


            Booking returnBooking = null;
            DateTime nowDate = DateTime.Now;

            try
            {
                if (Current.BookingReturns.Count == 0)
                {
                    returnBooking = new Booking();
                    Current.BookingReturns.Add(returnBooking);

                    returnBooking.BookingDate = nowDate;

                    if (Current.AddBy != null)
                        returnBooking.AddBy = Current.AddBy;

                    returnBooking.AddOn = nowDate;

                    if (!string.IsNullOrEmpty(Current.AddLog))
                        returnBooking.AddLog = Current.AddLog;
                    else
                        returnBooking.AddLog = AddedBy.ToStr();



                    returnBooking.ParkingCharges = Current.ParkingCharges.ToDecimal();
                    returnBooking.WaitingCharges = Current.WaitingCharges.ToDecimal();
                    returnBooking.MeetAndGreetCharges = Current.MeetAndGreetCharges.ToDecimal();
                    returnBooking.CongtionCharges = Current.CongtionCharges.ToDecimal();
                    returnBooking.ExtraDropCharges = Current.ExtraDropCharges.ToDecimal();
                    returnBooking.DriverWaitingMins = Current.DriverWaitingMins.ToInt();
                }
                else
                    returnBooking = Current.BookingReturns.FirstOrDefault();




                returnBooking.OnlineBookingId = Current.OnlineBookingId;
                returnBooking.BookingTypeId = Current.BookingTypeId;

                returnBooking.MasterJobId = Current.Id;

                if (returnBooking.DriverId == null)
                {
                    returnBooking.DriverId = Current.ReturnDriverId;

                    if (Current.IsConfirmedDriver.ToBool() && Current.DriverId != null)
                        returnBooking.IsConfirmedDriver = true;
                }
                else
                {
                    Current.ReturnDriverId = returnBooking.DriverId;
                }


                //if (this.ReturnConfirmAllocaedDriver != null)
                //{
                //    returnBooking.IsBidding = true;

                //}


                if (this.ReturnVehicleTypeId != null)
                {
                    returnBooking.VehicleTypeId = this.ReturnVehicleTypeId;
                }
                else
                {
                    returnBooking.VehicleTypeId = Current.VehicleTypeId.ToIntorNull();

                }

                // Added on : 17/08/2015 (Danish)
                returnBooking.BabySeats = Current.BabySeats.ToStr().Trim();
                //


                // Added on : 19/08/2015 (danish)

                returnBooking.SecondaryPaymentTypeId = Current.SecondaryPaymentTypeId;
                returnBooking.CashFares = Current.CashFares;

                //

                returnBooking.PaymentTypeId = Current.PaymentTypeId.ToIntorNull();
                returnBooking.CompanyId = Current.CompanyId.ToIntorNull();
                returnBooking.DepartmentId = Current.DepartmentId.ToIntorNull();
                returnBooking.IsCompanyWise = Current.IsCompanyWise.ToBool();
                returnBooking.JourneyTypeId = Enums.JOURNEY_TYPES.ONEWAY;

                returnBooking.NoofHandLuggages = Current.NoofHandLuggages.ToInt();
                returnBooking.NoofLuggages = Current.NoofLuggages.ToInt();
                returnBooking.NoofPassengers = Current.NoofPassengers.ToInt();

                returnBooking.PickupDateTime = Current.ReturnPickupDateTime;

                try
                {
                    returnBooking.AutoDespatchTime = returnBooking.PickupDateTime.Value.AddMinutes(-returnBooking.DeadMileage.ToDecimal().ToInt()).ToDateTime();
                    returnBooking.DeadMileage = returnBooking.DeadMileage.ToDecimal();
                }
                catch
                {

                }

                if (returnBooking.BookingTypeId == Enums.BOOKING_TYPES.WEB)
                {
                    returnBooking.FareRate = Current.ReturnFareRate.ToDecimal();

                    returnBooking.BookingTypeId = Enums.BOOKING_TYPES.WEB;
                }
                else
                {



                    returnBooking.FareRate = Current.ReturnFareRate.ToDecimal();
                    // Return Company Price

                    returnBooking.CompanyPrice = Current.WaitingMins.ToDecimal();
                    //
                    // returnBooking.CustomerPrice = Current.CustomerPrice.ToDecimal();
                    returnBooking.CustomerPrice = Current.ReturnFareRate.ToDecimal();

                    returnBooking.ServiceCharges = this.ReturnCustomerPrice.ToDecimal();

                    returnBooking.EscortPrice = Current.EscortPrice.ToDecimal();
                }






                returnBooking.CustomerId = Current.CustomerId.ToIntorNull();
                returnBooking.CustomerName = Current.CustomerName.ToStr().Trim();
                returnBooking.CustomerEmail = Current.CustomerEmail.ToStr().Trim();

                //   returnBooking.CustomerEmail=txtCustomerEmail.Text.Trim();
                returnBooking.CustomerPhoneNo = Current.CustomerPhoneNo.ToStr().Trim();
                returnBooking.CustomerMobileNo = Current.CustomerMobileNo.ToStr().Trim();
                //    returnBooking.SpecialRequirements = Current.SpecialRequirements.ToStr().Trim();

                returnBooking.SpecialRequirements = this.ReturnSpecialRequirement.ToStr().Trim();


                returnBooking.AdvanceBookingId = Current.AdvanceBookingId;
                returnBooking.SubcompanyId = Current.SubcompanyId;

                int FromLocTypeId = Current.ToLocTypeId.ToInt();
                int ToLocTypeId = Current.FromLocTypeId.ToInt();

                returnBooking.FromLocTypeId = FromLocTypeId;
                returnBooking.ToLocTypeId = ToLocTypeId;



                // new
                //if (this.ReturnFromAddress.ToStr().Trim() != string.Empty)
                //    returnBooking.FromLocTypeId = Enums.LOCATION_TYPES.ADDRESS;


                //if (this.ReturnToAddress.ToStr().Trim() != string.Empty)
                //    returnBooking.ToLocTypeId = Enums.LOCATION_TYPES.ADDRESS;

                //else if (this.ReturnToLocIdv != null)
                //    returnBooking.ToLocTypeId = Enums.LOCATION_TYPES.AIRPORT;


                //

                if (Current.ReturnFromLocId != null)
                {
                    returnBooking.FromLocId = Current.ReturnFromLocId;

                    Gen_Location returnFromLocAirport = GeneralBLL.GetObject<Gen_Location>(c => c.Id == Current.ReturnFromLocId);
                    if (returnFromLocAirport != null)
                    {

                        returnBooking.FromAddress = returnFromLocAirport.PostCode != string.Empty ? returnFromLocAirport.LocationName + ", " + returnFromLocAirport.PostCode : returnFromLocAirport.LocationName;
                    }

                    returnBooking.FromStreet = Current.ToStreet.ToStr().Trim();
                    returnBooking.FromPostCode = GeneralBLL.GetPostCodeMatch(returnBooking.FromAddress.ToStr().Trim());

                    if (!string.IsNullOrEmpty(returnBooking.FromPostCode.ToStr().Trim()))
                    {
                        returnBooking.FromOther = returnBooking.FromAddress.ToStr().Replace(returnBooking.FromPostCode.ToStr(), "").Trim();
                    }
                    else
                    {
                        returnBooking.FromOther = returnBooking.FromAddress.ToStr().Trim();

                    }
                }
                else
                {
                    if (this.ReturnFromAddress.ToStr().Trim() != string.Empty)
                    {

                        returnBooking.FromLocId = null;
                        returnBooking.FromAddress = this.ReturnFromAddress.ToStr().Trim();
                        returnBooking.FromPostCode = GeneralBLL.GetPostCodeMatch(returnBooking.FromAddress);

                        if (returnBooking.FromPostCode.ToStr().Trim() != string.Empty)
                            returnBooking.FromOther = returnBooking.FromAddress.Replace(returnBooking.FromPostCode, "").Trim();
                        else
                            returnBooking.FromOther = returnBooking.FromAddress.ToStr();
                    }
                    else
                    {


                        returnBooking.FromLocId = Current.ToLocId.ToIntorNull();
                        returnBooking.FromAddress = Current.ToAddress;


                        returnBooking.FromStreet = Current.ToStreet.ToStr().Trim();
                        returnBooking.FromPostCode = Current.ToPostCode.ToStr().Trim();
                    }
                }

                //old
                //returnBooking.ToLocId = Current.FromLocId.ToIntorNull();
                //returnBooking.ToAddress = Current.FromAddress;


                // new
                if (ReturnToLocIdv != null)
                {
                    returnBooking.ToLocId = ReturnToLocIdv;

                    Gen_Location returnToLoc = GeneralBLL.GetObject<Gen_Location>(c => c.Id == ReturnToLocIdv);
                    if (returnToLoc != null)
                    {

                        returnBooking.ToAddress = returnToLoc.PostCode != string.Empty ? returnToLoc.LocationName + ", " + returnToLoc.PostCode : returnToLoc.LocationName;
                    }


                    returnBooking.ToPostCode = GeneralBLL.GetPostCodeMatch(returnBooking.ToAddress.ToStr());


                    //  returnBooking.ToStreet = Current.ToStreet.ToStr().Trim();
                }
                else
                {
                    if (this.ReturnToAddress.ToStr().Trim() != string.Empty)
                    {

                        returnBooking.ToLocId = null;
                        returnBooking.ToAddress = this.ReturnToAddress.ToStr().Trim();


                        returnBooking.ToPostCode = GeneralBLL.GetPostCodeMatch(returnBooking.ToAddress);

                        if (returnBooking.ToPostCode.ToStr().Trim() != string.Empty)
                            returnBooking.ToOther = returnBooking.ToAddress.Replace(returnBooking.ToPostCode, "").Trim();
                        else
                            returnBooking.ToOther = returnBooking.ToAddress.ToStr();
                    }
                    else
                    {
                        returnBooking.ToLocId = Current.FromLocId.ToIntorNull();
                        returnBooking.ToAddress = Current.FromAddress;

                    }
                }
                //

                //



                returnBooking.FromDoorNo = Current.ToDoorNo.ToStr().Trim();
                //returnBooking.FromStreet = Current.ToStreet.ToStr().Trim();
                //returnBooking.FromPostCode = Current.ToPostCode.ToStr().Trim();





                if (Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.WEB && returnBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                {

                    if (!string.IsNullOrEmpty(Current.ToStreet.ToStr().Trim()) && Current.ToDoorNo.ToStr().Trim() == string.Empty)
                    {
                        if (Current.ToStreet.ToStr().Length < 100)
                        {
                            returnBooking.ToDoorNo = Current.ToStreet.ToStr().Trim();
                        }

                        returnBooking.ToStreet = Current.ToStreet.ToStr().Trim();
                    }
                    else
                    {
                        returnBooking.ToDoorNo = Current.FromDoorNo.ToStr().Trim();
                        returnBooking.ToStreet = Current.FromStreet.ToStr().Trim();
                    }
                }
                else
                {
                    returnBooking.ToDoorNo = Current.FromDoorNo.ToStr().Trim();
                    returnBooking.ToStreet = Current.FromStreet.ToStr().Trim();

                }




                if (Current.ReturnFromLocId == null && ReturnFromAddress.ToStr().Trim() == string.Empty)
                    returnBooking.FromOther = Current.ToOther.ToStr();

                if (ReturnToLocIdv == null && ReturnToAddress.ToStr().Trim() == string.Empty)
                {
                    returnBooking.ToOther = Current.FromOther.ToStr();
                    returnBooking.ToPostCode = Current.FromPostCode.ToStr().Trim();
                }
                else
                {

                    if (!string.IsNullOrEmpty(returnBooking.ToPostCode.ToStr().Trim()))
                        returnBooking.ToOther = returnBooking.ToAddress.Replace(returnBooking.ToPostCode, "").Trim(); ;

                }

                returnBooking.AutoDespatch = Current.AutoDespatch.ToBool();
                returnBooking.IsBidding = Current.IsBidding.ToBool();
                //if (returnBooking.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.WAITING)
                //{

                returnBooking.IsCommissionWise = Current.IsCommissionWise.ToBool();
                returnBooking.DriverCommission = Current.DriverCommission.ToDecimal();
                returnBooking.DriverCommissionType = Current.DriverCommissionType.ToStr().Trim();

                if (returnBooking.Id == 0)
                    returnBooking.PaymentComments = Current.PaymentComments.ToStr().Trim();
                //   }

                returnBooking.JobTakenByCompany = Current.JobTakenByCompany.ToBool();
                returnBooking.AgentCommissionPercent = Current.AgentCommissionPercent.ToInt();
                returnBooking.AgentCommission = Current.AgentCommission.ToDecimal();

                string[] skipProperties = { "Gen_Location", "Booking", "Gen_LocationType" };
                IList<Booking_ViaLocation> savedList = returnBooking.Booking_ViaLocations;
                List<Booking_ViaLocation> listofDetail = (from r in Current.Booking_ViaLocations.OrderByDescending(c => c.Id)
                                                          select new Booking_ViaLocation
                                                          {
                                                              Id = 0,
                                                              BookingId = r.BookingId,
                                                              ViaLocTypeId = r.ViaLocTypeId.ToIntorNull(),
                                                              ViaLocTypeLabel = r.ViaLocTypeLabel.ToStr(),
                                                              ViaLocTypeValue = r.ViaLocTypeValue.ToStr(),

                                                              ViaLocId = r.ViaLocId.ToIntorNull(),
                                                              ViaLocLabel = r.ViaLocLabel.ToStr(),
                                                              ViaLocValue = r.ViaLocValue.ToStr()

                                                          }).ToList();


                Utils.General.SyncChildCollection(ref savedList, ref listofDetail, "Id", skipProperties);


                if (listofDetail.Count > 0)
                {
                    int cnter = 0;
                    returnBooking.ViaString = string.Join(",", listofDetail.Select(args => "Via " + (++cnter) + " : " + args.ViaLocValue.Replace(",", " ")).ToArray<string>());


                }

                if (returnBooking.BookingStatusId == null)
                    returnBooking.BookingStatusId = Enums.BOOKINGSTATUS.WAITING;


                if (returnBooking.BookingTypeId == null)
                {
                    returnBooking.BookingTypeId = Enums.BOOKING_TYPES.LOCAL;
                }



                returnBooking.IsQuotation = Current.IsQuotation.ToBool();


                if (returnBooking.CompanyId == null)
                {
                    returnBooking.TotalCharges = returnBooking.FareRate.ToDecimal()
                                 + returnBooking.ExtraDropCharges.ToDecimal() + returnBooking.MeetAndGreetCharges.ToDecimal() + returnBooking.CongtionCharges.ToDecimal();
                }
                else
                {
                    returnBooking.TotalCharges = returnBooking.CompanyPrice.ToDecimal() + returnBooking.ParkingCharges.ToDecimal() + returnBooking.WaitingCharges.ToDecimal() + returnBooking.ExtraDropCharges.ToDecimal();

                }

                //   returnBooking.TotalCharges = returnBooking.FareRate.ToDecimal();

                returnBooking.ZoneId = Current.DropOffZoneId;
                returnBooking.DropOffZoneId = Current.ZoneId;

                returnBooking.IsCompanyWise = Current.IsCompanyWise.ToBool();

                returnBooking.CompanyId = Current.CompanyId;


                if (Current.PupilNo.ToStr().Trim().Length > 0)
                {

                    returnBooking.OrderNo = Current.PupilNo.ToStr();
                }
                else
                {

                    returnBooking.OrderNo = Current.OrderNo.ToStr();
                }

                returnBooking.EscortName = Current.EscortName.ToStr();
                returnBooking.BookedBy = Current.BookedBy.ToStr();
                returnBooking.PupilNo = Current.PupilNo.ToStr();




                if (returnBooking.Id == 0)
                {
                    LastOveralNumber++;
                    returnBooking.BookingNo = bookingno+ LastOveralNumber.ToStr();
                   
                   // returnBooking.BookingNo = new SysPolicy_DocumentNumberSetupBO().GetSequenceNumber(Enums.GEN_DOCUMENTS.BOOKINGNO, Current.SubcompanyId, true);
                    if (returnBooking.BookingNo.ToStr().Length == 1)
                    {

                        string temp = returnBooking.BookingNo.ToStr();

                        returnBooking.BookingNo = Current.BookingNo.ToStr() + temp + "2";
                        Current.BookingNo = Current.BookingNo.ToStr() + temp + "1";

                    }





                    if (returnBooking.CompanyId == null)
                    {
                        returnBooking.TotalCharges = returnBooking.FareRate.ToDecimal()
                                     + returnBooking.ExtraDropCharges.ToDecimal() + returnBooking.MeetAndGreetCharges.ToDecimal() + returnBooking.CongtionCharges.ToDecimal();
                    }
                    else
                    {
                        returnBooking.TotalCharges = returnBooking.CompanyPrice.ToDecimal() + returnBooking.ParkingCharges.ToDecimal() + returnBooking.WaitingCharges.ToDecimal() + returnBooking.ExtraDropCharges.ToDecimal();

                    }



                    //if (returnBooking.SpecialRequirements.ToStr().Trim().Length > 0)
                    //    returnBooking.SpecialRequirements = returnBooking.BookingNo.ToStr() + ", " + returnBooking.SpecialRequirements.ToStr().Trim();
                    //else
                    //    returnBooking.SpecialRequirements = returnBooking.BookingNo.ToStr();
                    //  returnBooking.TotalCharges = returnBooking.FareRate.ToDecimal() + returnBooking.ParkingCharges + returnBooking.WaitingCharges + returnBooking.MeetAndGreetCharges + returnBooking.CongtionCharges + returnBooking.ExtraDropCharges;

                }

                if (returnBooking.SubcompanyId == null)
                {
                    int subCompanyId = GeneralBLL.GetObject<Gen_SubCompany>(c => c.IsSysGen != null && c.IsSysGen == true).DefaultIfEmpty().Id;

                    if (subCompanyId > 0)
                    {
                        returnBooking.SubcompanyId = subCompanyId;

                    }
                }


                //

                if (string.IsNullOrEmpty(returnBooking.FromPostCode.ToStr()))
                {
                    returnBooking.FromPostCode = GeneralBLL.GetPostCodeMatch(returnBooking.FromAddress.ToStr());
                }

                if (worker != null)
                {
                    cr.UpdateValue = returnBooking.BookingNo;
                    cnter++;
                    cr.index = cnter;
                    worker.ReportProgress(cnter, cr);
                }
                objMasterBookingList.Add(returnBooking);
                //using (TaxiDataContext db = new TaxiDataContext())
                //{
                //    db.Bookings.InsertOnSubmit(returnBooking);
                //    db.SubmitChanges();
                //}
              

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);


            }

        }
        private void SaveCustomer(string customerName, string telNo, string mobNo, string email, Booking Current)
        {
            CustomerBO objCustomerMaster = null;

            bool customerUpdated = false;
            try
            {

                if (Current.CompanyId != null && Current.CompanyCreditCardDetails.ToStr().Trim() != string.Empty)
                {

                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        Customer cust = db.Customers.Where(c => (c.TelephoneNo == telNo || telNo == string.Empty) && (c.MobileNo == mobNo || mobNo == string.Empty))
                            .OrderByDescending(c => c.Id).FirstOrDefault();



                        if (cust != null)
                        {

                            cust.CreditCardDetails = Current.CustomerCreditCardDetails.ToStr().Trim();
                            db.SubmitChanges();
                        }

                    }
                }


                if ((string.IsNullOrEmpty(telNo) && string.IsNullOrEmpty(mobNo)) || (!string.IsNullOrEmpty(email) && this.OldEmail.ToStr().ToLower().Equals(email.ToStr().ToLower()))) return;


                if (Current.Id == 0 || this.OldCustomerName.ToStr().ToLower().Equals(customerName.ToStr().ToLower()) == false || (this.OldEmail.ToStr().ToLower().Equals(email.ToStr().ToLower()) == false)
                      || (this.OldMobileNo.ToStr().ToLower().Equals(mobNo.ToStr().ToLower()) == false)
                      || (this.OldPhoneNo.ToStr().ToLower().Equals(telNo.ToStr().ToLower()) == false)
                    )
                {

                    if (IsExistingCustomer && this.OldCustomerName.ToStr().ToLower().Equals(customerName.ToStr().ToLower()))
                        return;



                    objCustomerMaster = new CustomerBO();

                    Customer obj = GeneralBLL.GetObject<Customer>(c => (c.TelephoneNo == telNo || telNo == string.Empty) && (c.MobileNo == mobNo || mobNo == string.Empty));

                    if (obj == null)
                    {
                        if (Current.Id == 0)
                        {
                            objCustomerMaster.New();
                            objCustomerMaster.Current.TotalCalls = objCustomerMaster.Current.TotalCalls.ToInt() + 1;
                        }
                        else
                        {
                            try
                            {

                                if ((this.OldMobileNo.ToStr().ToLower().Equals(mobNo.ToStr().ToLower()) == false) || (this.OldPhoneNo.ToStr().ToLower().Equals(telNo.ToStr().ToLower()) == false))
                                {
                                    objCustomerMaster.New();
                                    objCustomerMaster.Current.TotalCalls = objCustomerMaster.Current.TotalCalls.ToInt() + 1;
                                    objCustomerMaster.Current.CompanyName = customerName;
                                    objCustomerMaster.Current.Name = customerName;
                                    objCustomerMaster.Current.TelephoneNo = telNo;
                                    objCustomerMaster.Current.MobileNo = mobNo;

                                    objCustomerMaster.Current.Email = email;

                                    objCustomerMaster.Current.Address1 = Current.FromAddress.ToStr().Trim();
                                    objCustomerMaster.Current.DoorNo = Current.FromDoorNo.ToStr().Trim();

                                    if (Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.WEB || Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.ONLINE
                             || Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.THIRDPARTY || Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.IMPORT)
                                    {
                                        if (string.IsNullOrEmpty(objCustomerMaster.Current.Name.ToStr().Trim()))
                                            objCustomerMaster.Current.Name = "UNKNOWN";

                                        objCustomerMaster.CheckDataValidation = false;
                                    }

                                    objCustomerMaster.Current.CompanyId = Current.CompanyId;
                                    objCustomerMaster.Current.LikesAndDislikes = Current.NoOfChilds.ToInt() == 1 ? Current.SpecialRequirements.ToStr().Trim() : "";

                                    objCustomerMaster.Current.ExcludedDriverIds = Current.NoOfChilds.ToInt() == 1 ? Current.ExcludedDriverIds.ToStr().Trim() : "";

                                    objCustomerMaster.Save();
                                    customerUpdated = true;
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                    }
                    else
                    {

                        int custId = obj.Id;
                        if ((Current.CustomerId != null && Current.CustomerId != obj.Id))
                            return;

                        objCustomerMaster.GetByPrimaryKey(custId);
                    }

                    if (objCustomerMaster.Current == null) return;


                    objCustomerMaster.Current.CompanyName = customerName;
                    objCustomerMaster.Current.Name = customerName;
                    objCustomerMaster.Current.TelephoneNo = telNo;
                    objCustomerMaster.Current.MobileNo = mobNo;

                    objCustomerMaster.Current.Email = email;
                    objCustomerMaster.Current.LikesAndDislikes = Current.NoOfChilds.ToInt() == 1 ? Current.SpecialRequirements.ToStr().Trim() : "";
                    objCustomerMaster.Current.ExcludedDriverIds = Current.NoOfChilds.ToInt() == 1 ? Current.ExcludedDriverIds.ToStr().Trim() : "";

                    objCustomerMaster.Current.Address1 = Current.FromAddress.ToStr().Trim();
                    objCustomerMaster.Current.DoorNo = Current.FromDoorNo.ToStr().Trim();


                    if (Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.WEB || Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.ONLINE
                        || Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.THIRDPARTY
                         || Current.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.IMPORT)
                    {

                        if (string.IsNullOrEmpty(objCustomerMaster.Current.Name.ToStr().Trim()))
                            objCustomerMaster.Current.Name = "UNKNOWN";

                        objCustomerMaster.CheckDataValidation = false;

                    }

                    objCustomerMaster.Current.CompanyId = Current.CompanyId;


                    objCustomerMaster.Save();
                    customerUpdated = true;
                    Current.CustomerId = objCustomerMaster.Current.Id == 0 ? null : objCustomerMaster.Current.Id.ToIntorNull();
                }



                // if (Current.NoOfChilds.ToInt() > 0 && customerUpdated == false && (this.PermanentNotes.ToStr().Trim() != Current.SpecialRequirements.ToStr().Trim() || this.ExcludedDriverIds.ToStr().Trim() != Current.ExcludedDriverIds.ToStr().Trim()))
                {
                    try
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            Customer cust = db.Customers.Where(c => (c.TelephoneNo == telNo || telNo == string.Empty) && (c.MobileNo == mobNo || mobNo == string.Empty))
                                .OrderByDescending(c => c.Id).FirstOrDefault();



                            if (cust != null)
                            {

                                cust.LikesAndDislikes = Current.SpecialRequirements.ToStr().Trim();
                                cust.ExcludedDriverIds = Current.ExcludedDriverIds.ToStr().Trim();
                                db.SubmitChanges();
                            }

                        }
                    }
                    catch
                    {


                    }


                }
            }
            catch (Exception ex)
            {
                if (objCustomerMaster != null && objCustomerMaster.Errors.Count > 0)
                {
                    throw new Exception(objCustomerMaster.ShowErrors());
                }
                else
                    throw new Exception(ex.Message);
            }
        }

        private string LastPickupPostCode = string.Empty;
        private string LastDropOffPostCode = string.Empty;
        private int? lastPickupZoneId = null;
        private int? lastDropOffZoneId = null;

        private bool SendAdvanceBookingConfirmationText(string mobileNo)
        {
            if (string.IsNullOrEmpty(mobileNo)) return false;

            bool rtn = true;

            // Advance Booking Confirmation Text
            bool enableAdvBookingText = AppVars.objPolicyConfiguration.EnableAdvanceBookingSMSConfirmation.ToBool();

            if (enableAdvBookingText && listOfPickUpDateTime.Count > 0 && this.ObjBookiing != null && this.ObjBookiing.IsQuotation.ToBool() == false)
            {

                int afterMins = AppVars.objPolicyConfiguration.AdvanceBookingSMSConfirmationMins.ToInt();

                int minDifference = 0;
                bool foundAny = false;
                int dayDiff = 0;
                foreach (var pickupTime in listOfPickUpDateTime)
                {
                    dayDiff = pickupTime.Date.Subtract(DateTime.Now.Date).Days;
                    minDifference = pickupTime.TimeOfDay.Subtract(nowDate.TimeOfDay).Minutes;

                    if (dayDiff > 0 || afterMins > 0 && minDifference >= afterMins)
                    {
                        foundAny = true;
                        break;
                    }
                }



                if (foundAny)
                {
                    string msg = AppVars.objPolicyConfiguration.AdvanceBookingSMSText.ToStr().Trim();
                    object propertyValue = string.Empty;

                    foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                    {
                        switch (tag.TagObjectName)
                        {
                            case "booking":

                                if (tag.TagPropertyValue.Contains('.'))
                                {

                                    string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                    object parentObj = ObjBookiing.GetType().GetProperty(val[0]).GetValue(ObjBookiing, null);

                                    if (parentObj != null)
                                    {
                                        propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                    }
                                    else
                                        propertyValue = string.Empty;


                                    break;
                                }
                                else
                                {
                                    propertyValue = ObjBookiing.GetType().GetProperty(tag.TagPropertyValue).GetValue(ObjBookiing, null);
                                }


                                if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                                {
                                    propertyValue = ObjBookiing.GetType().GetProperty(tag.TagPropertyValue2).GetValue(ObjBookiing, null);
                                }
                                break;


                            default:
                                propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                                break;

                        }


                        msg = msg.Replace(tag.TagMemberValue,
                            tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                    }


                    msg.Replace("\n\n", "\n");

                    string refMsg = "";
                    rtn = General.SendAdvanceBookingSMS(mobileNo, ref refMsg, msg, ObjBookiing.SMSType.ToInt());

                }
            }

            return rtn;

            //

        }

        private void grdBookings_Click(object sender, EventArgs e)
        {

        }

        private void radPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private bool _Saved;

        public bool Saved
        {
            get { return _Saved; }
            set { _Saved = value; }
        }


        private void btnSaveBooking_Click(object sender, EventArgs e)
        {
            //Saved = Save();
            btnSaveBooking.Enabled = false;
            btnWeeklyCreateBooking.Enabled = false;
            btnCreateBooking.Enabled = false;
            if (worker == null)
            {
                InitializeWorker();
            }
            worker.RunWorkerAsync();

        }

        private void radCheckBox1_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {
            // btnCreateBooking.Enabled= args.NewValue== Telerik.WinControls.Enumerations.ToggleState.On;

            if (args.NewValue == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                chkOrigin.Visible = false;
                chkDestination.Visible = false;

                chkOrigin.Checked = true;
                chkDestination.Checked = true;
            }
            else
            {
                chkOrigin.Visible = true;
                chkDestination.Visible = true;

            }
        }




        private void grdBookings_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            SelectRowDetail(e.Row);
        }


        private void SelectRowDetail(GridViewRowInfo row)
        {
            //if (row != null && row is GridViewDataRowInfo)
            //{
            //    lblBookingDay.Text = "Day " + row.Cells[COLS.DAY].Value.ToStr();
            //    lblBookingDay.Tag = row.Cells[COLS.DAY].Value.ToInt();

            //    ddlFromLocType.SelectedValue = row.Cells[COLS.FromLocTypeId].Value;
            //    ddlToLocType.SelectedValue = row.Cells[COLS.ToLocTypeId].Value;
            //    ddlFromLocation.SelectedValue = row.Cells[COLS.FromLocId].Value;
            //    ddlToLocation.SelectedValue = row.Cells[COLS.ToLocId].Value;


            //    txtFromAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            //    txtFromAddress.Text = row.Cells[COLS.FromAddress].Value.ToStr();
            //    txtFromAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            //    txtToAddress.TextChanged -= new EventHandler(TextBoxElement_TextChanged);
            //    txtToAddress.Text = row.Cells[COLS.ToAddress].Value.ToStr();
            //    txtToAddress.TextChanged += new EventHandler(TextBoxElement_TextChanged);

            //    dtpPickupDate.Value = row.Cells[COLS.PickupDate].Value.ToDateorNull();
            //    dtpPickupTime.Value = row.Cells[COLS.PickupDate].Value.ToDateTimeorNull();


            //    if (row.Cells[COLS.ReturnPickupDate].Value != numReturnFareRate)
            //    {
            //        dtpReturnPickupDate.Value = row.Cells[COLS.ReturnPickupDate].Value.ToDate();
            //        dtpReturnPickupTime.Value = row.Cells[COLS.ReturnPickupDate].Value.ToDateTime();
            //        numReturnFareRate.Value = row.Cells[COLS.ReturnFare].Value.ToDecimal();
            //    }              

            //}
        }

        private void chkAutoDespatch_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //numBeforeMinutes.Enabled = args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
        }

        private void btnAddPickupDate_Click(object sender, EventArgs e)
        {
            AddPickupDate();
        }

        private void AddPickupDate()
        {
            string day = ddlPickupDay.SelectedValue.ToStr().Trim();
            DateTime? pickupdate = dtp_StartPickupDate.Value;
            DateTime? pickuptime = dtp_StartPickupTime.Value;
            DateTime? returnpickupdate = dtp_ReturnStartPickupDate.Value;
            DateTime? returnpickuptime = dtp_ReturnStartPickupTime.Value;
            decimal fare = numFareRate.Value.ToDecimal();
            decimal retFare = numReturnFare.Value.ToDecimal();

            bool IsReturn = chkIsReturn.Checked;

            string error = string.Empty;



            if (string.IsNullOrEmpty(day))
            {
                error += "Required : Day" + Environment.NewLine;
            }
            if (pickupdate == null)
            {

                error += "Required : Pickup Date" + Environment.NewLine;

            }

            if (pickuptime == null)
            {

                error += "Required : Pickup Time";

            }

            if (IsReturn)
            {
                if (returnpickupdate == null)
                {

                    error += "Required : Return Pickup Date" + Environment.NewLine;

                }

                if (returnpickuptime == null)
                {

                    error += "Required : Return Pickup Time";

                }
            }

            if (!string.IsNullOrEmpty(error))
            {
                ENUtils.ShowMessage(error);
                return;
            }


            pickupdate = pickupdate.ToDate();
            returnpickupdate = returnpickupdate.ToDateorNull();



            if (IsReturn)
            {
                if (pickupdate > returnpickupdate || (pickupdate >= returnpickupdate && pickuptime.Value.TimeOfDay > returnpickuptime.Value.TimeOfDay))
                {
                    ENUtils.ShowMessage("Day " + day + " :" + " Pickup Date Time must be less than Return Pickup Date Time");
                    return;
                }
            }


            GridViewRowInfo row = null;
            if (grdPickupDates.CurrentRow != null)
            {
                if (grdPickupDates.CurrentRow is GridViewNewRowInfo)
                    grdPickupDates.CurrentRow = null;

                if (grdPickupDates.CurrentRow is GridViewDataRowInfo)
                    row = grdPickupDates.CurrentRow;

            }






            if (grdPickupDates.Rows.Count(c => c.Cells[COLS.DAY].Value.ToStr().Equals(day)) > 0)
                row = grdPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS.DAY].Value.ToStr() == day);
            else
            {

                if (grdPickupDates.Rows.Count >= day.ToInt())
                {
                    ENUtils.ShowMessage("Total Days Limit exceed." + Environment.NewLine + "You have to Enter more Days before Adding it into a Grid");
                    return;
                }

                row = grdPickupDates.Rows.AddNew();

            }


            row.Cells[COLS_PICKUPS.DAY].Value = day;
            row.Cells[COLS_PICKUPS.PickupDate].Value = string.Format("{0:dd/MM/yyyy}", pickupdate);
            row.Cells[COLS_PICKUPS.PickupTime].Value = string.Format("{0:HH:mm}", pickuptime);

            row.Cells[COLS_PICKUPS.ReturnPickupDate].Value = string.Format("{0:dd/MM/yyyy}", returnpickupdate);
            row.Cells[COLS_PICKUPS.ReturnPickupTime].Value = string.Format("{0:HH:mm}", returnpickuptime);

            row.Cells[COLS_PICKUPS.Fare].Value = fare;
            row.Cells[COLS_PICKUPS.RetFare].Value = retFare;


            ddlPickupDay.SelectedIndex = ++ddlPickupDay.SelectedIndex;


            SetPickupDateGridNull();

        }

        private void SetPickupDateGridNull()
        {

            grdPickupDates.CurrentRow = null;
            dtp_StartPickupDate.Focus();
        }








        private void numDays_Validated(object sender, EventArgs e)
        {


            FillDaysCombo();

        }

        private void FillDaysCombo()
        {


            int days = numDays.Value.ToInt();

            ddlPickupDay.Items.Clear();


            RadListDataItem item = null;

            for (int i = 1; i <= days; i++)
            {

                item = new RadListDataItem();
                item.Text = i.ToStr();
                item.Value = item.Text;

                ddlPickupDay.Items.Add(item);

            }


            ddlPickupDay.SelectedValue = "1";

        }

        private void chkIsReturn_ToggleStateChanging(object sender, StateChangingEventArgs args)
        {

            SetReturnPickupDate(args.NewValue);
        }

        private void SetReturnPickupDate(ToggleState toggle)
        {

            if (toggle == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                pnlReturn.Enabled = true;


                dtp_ReturnStartPickupDate.Value = dtp_StartPickupDate.Value;
                dtp_ReturnStartPickupTime.Value = dtp_StartPickupTime.Value;
                numReturnFare.Enabled = true;
            }
            else
            {


                pnlReturn.Enabled = false;
                dtp_ReturnStartPickupDate.Value = null;
                dtp_ReturnStartPickupTime.Value = null;

                numReturnFare.Enabled = false;
                numReturnFare.Value = 0;

            }

        }

        private void ddlPickupDay_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            string day = ddlPickupDay.SelectedValue.ToStr().Trim();



            if (string.IsNullOrEmpty(day)) return;


            GridViewRowInfo row = grdPickupDates.Rows.FirstOrDefault(c => c.Cells[COLS_PICKUPS.DAY].Value.ToStr() == day);

            if (row != null)

            {
                DateTime? pickupDate = row.Cells[COLS_PICKUPS.PickupDate].Value.ToDate();
                DateTime pickupTime = row.Cells[COLS_PICKUPS.PickupTime].Value.ToDateTime();

                dtp_StartPickupDate.Value = pickupDate;
                dtp_StartPickupTime.Value = pickupTime;

                if (row.Cells[COLS_PICKUPS.ReturnPickupDate].Value.ToStr() != string.Empty)
                {
                    pickupDate = row.Cells[COLS_PICKUPS.ReturnPickupDate].Value.ToDate();

                    pickupTime = row.Cells[COLS_PICKUPS.ReturnPickupTime].Value.ToDateTime();

                    dtp_ReturnStartPickupDate.Value = pickupDate;
                    dtp_ReturnStartPickupTime.Value = pickupTime;



                }



            }

        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            try
            {




                CalculateFares();





            }
            catch (Exception ex)
            {



            }
        }



        List<decimal> milesList = new List<decimal>();

        private bool CalculateFares()
        {
            milesList.Clear();
            int? vehicleTypeId = ObjBookiing.VehicleTypeId;
            int companyId = ObjBookiing.CompanyId.ToInt();
            int? fromLocationId = ObjBookiing.FromLocId.ToIntorNull();
            int? fromLocTypeId = ObjBookiing.FromLocTypeId.ToInt();
            DateTime bookingDate = DateTime.Now.ToDate();

            int? toLocTypeId = ObjBookiing.ToLocTypeId.ToInt();
            int? toLocationId = ObjBookiing.ToLocId.ToIntorNull();

            string fromLocName = ObjBookiing.FromAddress.Trim();
            string toLocName = ObjBookiing.ToAddress.Trim();

            string fromAddress = ObjBookiing.FromAddress.Trim();
            string toAddress = ObjBookiing.ToAddress.Trim();
            string fromPostCode = ObjBookiing.FromPostCode.Trim();
            string toPostCode = ObjBookiing.ToPostCode.Trim();
            DateTime? pickupTime = ObjBookiing.PickupDateTime;
            List<string> errors = new List<string>();


            if (vehicleTypeId == null)
            {
                errors.Add("Required : Vehicle Type");

            }

            if (fromLocationId == null && string.IsNullOrEmpty(fromPostCode) && string.IsNullOrEmpty(fromAddress))
            {

                errors.Add("Required : From Address");

            }


            if (toLocationId == null && string.IsNullOrEmpty(toPostCode) && string.IsNullOrEmpty(toAddress))
            {
                errors.Add("Required : To Address");

            }


            if (errors.Count > 0)
            {
                ENUtils.ShowMessage(string.Join(Environment.NewLine, errors.Select(c => c).ToArray<string>()));
                return false;
            }






            // Calculating Fares


            //    lblMsgCalculateFares.Visible = true;

            int tempFromLocId = 0;
            int tempToLocId = 0;
            string tempFromPostCode = "";
            string tempToPostCode = "";
            string errorMsg = string.Empty;

            decimal fareVal = 0.00m;
            decimal deadMileage = AppVars.objPolicyConfiguration.DeadMileage.ToDecimal();



            if (errorMsg == "Error")
            {

                // numFareRate.Value = 0;
                return false;
            }

            if (tempToLocId == 0)
            {
                tempFromLocId = fromLocationId.ToInt();
                if (tempFromLocId != 0)
                {
                    tempFromPostCode = fromLocName;
                }
                else
                    tempFromPostCode = fromAddress != string.Empty ? fromAddress : fromPostCode;

            }
            else
            {
                tempFromLocId = tempToLocId;
                tempFromPostCode = tempToPostCode;

            }

            tempToLocId = toLocationId.ToInt();
            if (tempToLocId != 0)
                tempToPostCode = toLocName;
            else
                tempToPostCode = toAddress != string.Empty ? toAddress : toPostCode;


            bool IsCompanyFareExist = false;
            string estimatedTime = string.Empty;

            fareVal += General.GetFareRate(ObjBookiing.SubcompanyId.ToInt(), companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, false, pickupTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime);


            //    fareVal += General.GetFareRate(companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, false, pickupTime,ref deadMileage);

            if (errorMsg == "Error")
            {


                numFareRate.Value = 0;
                return false;
            }




            string ff = string.Format("{0:#}", fareVal);
            if (ff == string.Empty)
                ff = "0";
            decimal dd = ff.ToDecimal();

            // Add Airport Pickup Charges If Pickup Point is From Airport...
            if (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT && errorMsg == "Reverse found")
                dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();

            var objInc = General.GetObject<Fare_IncrementSetting>(c => c.Id != 0 && c.EnableIncrement != null && c.EnableIncrement == true);

            if (objInc != null && DateTime.Now.ToDate() >= objInc.FromDate.ToDate() && DateTime.Now.ToDate() <= objInc.TillDate.ToDate())
            {
                if (objInc.IncrementType.ToStr() == "percent")
                {
                    dd = dd + ((dd * objInc.IncrementRate.ToDecimal()) / 100);
                    if (AppVars.objPolicyConfiguration.RoundMileageFares.ToBool())
                    {

                        dd = Math.Ceiling(dd);
                    }
                }
                else
                {
                    dd += objInc.IncrementRate.ToDecimal();

                }

            }


            numFareRate.Value = dd;




            if (chkIsReturn.Checked)
            {

                numReturnFare.Value = numFareRate.Value - ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForReturnedJourneyPercent.ToInt()) / 100);

                if (toLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                    numReturnFare.Value += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();

            }
            else
                numReturnFare.Value = 0;


            int journeyTypeId = ObjBookiing.JourneyTypeId.ToInt();

            if (journeyTypeId == Enums.JOURNEY_TYPES.WAITANDRETURN)
            {
                numFareRate.Value = numFareRate.Value + ((numFareRate.Value * AppVars.objPolicyConfiguration.DiscountForWRJourneyPercent.ToInt()) / 100);
            }

            return true;

        }

        private void CalculateWeekFares()
        {
            try
            {
                milesList.Clear();
                int? vehicleTypeId = ObjBookiing.VehicleTypeId;
                int companyId = ObjBookiing.CompanyId.ToInt();
                int? fromLocationId = ObjBookiing.FromLocId.ToIntorNull();
                int? fromLocTypeId = ObjBookiing.FromLocTypeId.ToInt();
                DateTime bookingDate = DateTime.Now.ToDate();

                int? toLocTypeId = ObjBookiing.ToLocTypeId.ToInt();
                int? toLocationId = ObjBookiing.ToLocId.ToIntorNull();

                string fromLocName = ObjBookiing.FromAddress.Trim();
                string toLocName = ObjBookiing.ToAddress.Trim();

                string fromAddress = ObjBookiing.FromAddress.Trim();
                string toAddress = ObjBookiing.ToAddress.Trim();
                string fromPostCode = ObjBookiing.FromPostCode.Trim();
                string toPostCode = ObjBookiing.ToPostCode.Trim();

                DateTime? pickupTime = ObjBookiing.PickupDateTime;

                List<string> errors = new List<string>();


                if (vehicleTypeId == null)
                {
                    errors.Add("Required : Vehicle Type");

                }

                if (fromLocationId == null && string.IsNullOrEmpty(fromPostCode) && string.IsNullOrEmpty(fromAddress))
                {

                    errors.Add("Required : From Address");

                }


                if (toLocationId == null && string.IsNullOrEmpty(toPostCode) && string.IsNullOrEmpty(toAddress))
                {
                    errors.Add("Required : To Address");

                }


                if (errors.Count > 0)
                {
                    ENUtils.ShowMessage(string.Join(Environment.NewLine, errors.Select(c => c).ToArray<string>()));
                    //return false;
                }






                // Calculating Fares


                //    lblMsgCalculateFares.Visible = true;

                int tempFromLocId = 0;
                int tempToLocId = 0;
                string tempFromPostCode = "";
                string tempToPostCode = "";
                string errorMsg = string.Empty;

                decimal fareVal = 0.00m;
                decimal deadMileage = AppVars.objPolicyConfiguration.DeadMileage.ToDecimal();



                if (errorMsg == "Error")
                {

                    // numFareRate.Value = 0;
                    //  return false;
                }

                if (tempToLocId == 0)
                {
                    tempFromLocId = fromLocationId.ToInt();
                    if (tempFromLocId != 0)
                    {
                        tempFromPostCode = fromLocName;
                    }
                    else
                        tempFromPostCode = fromAddress != string.Empty ? fromAddress : fromPostCode;

                }
                else
                {
                    tempFromLocId = tempToLocId;
                    tempFromPostCode = tempToPostCode;

                }

                tempToLocId = toLocationId.ToInt();
                if (tempToLocId != 0)
                    tempToPostCode = toLocName;
                else
                    tempToPostCode = toAddress != string.Empty ? toAddress : toPostCode;




                bool IsCompanyFareExist = false;
                string estimatedTime = string.Empty;

                fareVal += General.GetFareRate(ObjBookiing.SubcompanyId.ToInt(), companyId, vehicleTypeId.ToInt(), tempFromLocId, tempToLocId, tempFromPostCode, tempToPostCode, ref errorMsg, ref milesList, false, false, pickupTime, ref deadMileage, fromLocTypeId.ToInt(), toLocTypeId.ToInt(), ref IsCompanyFareExist, ref estimatedTime);

                if (errorMsg == "Error")
                {


                    numFareRateWeek.Value = 0;
                    //   return false;
                }




                string ff = string.Format("{0:#}", fareVal);
                if (ff == string.Empty)
                    ff = "0";
                decimal dd = ff.ToDecimal();

                // Add Airport Pickup Charges If Pickup Point is From Airport...
                if (fromLocTypeId == Enums.LOCATION_TYPES.AIRPORT && errorMsg == "Reverse found")
                    dd += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();

                numFareRateWeek.Value = dd;
                numCustFareRateWeek.Value = dd;
                numCompanyFareRateWeek.Value = dd;



                if (chkReturnWeekJourney.Checked)
                {

                    numWeekFareRateReturn.Value = numFareRateWeek.Value - ((numFareRateWeek.Value * AppVars.objPolicyConfiguration.DiscountForReturnedJourneyPercent.ToInt()) / 100);

                    if (toLocTypeId == Enums.LOCATION_TYPES.AIRPORT)
                        numWeekFareRateReturn.Value += AppVars.objPolicyConfiguration.AirportPickupCharges.ToDecimal();


                    numWeekCustFareRateReturn.Value = numWeekFareRateReturn.Value;
                    numWeekCompanyFareRateReturn.Value = numWeekFareRateReturn.Value;

                }
                else
                {
                    numWeekFareRateReturn.Value = 0;
                    numWeekCustFareRateReturn.Value = 0;
                    numWeekCompanyFareRateReturn.Value = 0;

                }

                int journeyTypeId = ObjBookiing.JourneyTypeId.ToInt();

                if (journeyTypeId == Enums.JOURNEY_TYPES.WAITANDRETURN)
                {
                    numFareRateWeek.Value = numFareRateWeek.Value + ((numFareRateWeek.Value * AppVars.objPolicyConfiguration.DiscountForWRJourneyPercent.ToInt()) / 100);


                    numWeekCustFareRateReturn.Value = numFareRateWeek.Value;
                    numWeekCompanyFareRateReturn.Value = numFareRateWeek.Value;

                }


            }
            catch (Exception ex)
            {


            }

        }



        char[] separatorArr = new char[] { ' ' };
        private decimal GetSurchargeRate(string postCode)
        {
            decimal percentage = 0.00m;
            string[] splitPostCode = postCode.Split(separatorArr);
            if (splitPostCode.Count() > 1)
            {

                Gen_SysPolicy_SurchargeRate obj = General.GetObject<Gen_SysPolicy_SurchargeRate>(c => c.SysPolicyId != null && c.PostCode.Trim().ToLower() == splitPostCode[0].Trim().ToLower());

                if (obj != null)
                    percentage = obj.Percentage.ToDecimal();
            }

            return percentage;

        }

        private decimal CalculateDistance(string origin, string destination)
        {
            decimal miles = 0.00m;



            string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + "&sensor=false";



            XmlTextReader reader = new XmlTextReader(url2);
            reader.WhitespaceHandling = WhitespaceHandling.Significant;
            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(reader);
            DataTable dt = ds.Tables["distance"];
            if (dt != null)
            {
                var rows = dt.Rows.OfType<DataRow>().Where(c => c[0].ToStr().Trim() == c[1].ToStr().Strip("m").Trim()).ToList();

                decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                decimal milKM = 0.621m;
                decimal milMeter = 0.00062137119m;

                miles = (milKM * distanceKm) + (milMeter * distanceMeter);

            }


            return miles;
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (grdPickupDates.CurrentRow == null || grdPickupDates.CurrentRow is GridViewDataRowInfo == false) return;


            grdPickupDates.CurrentRow.Delete();

        }

        private void radPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCalculateFares_Click(object sender, EventArgs e)
        {
            CalculateFares();
        }

        private void chkIsCompanyRates_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            UseCompanyRates(args.ToggleState);
        }

        private void UseCompanyRates(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.BANK_ACCOUNT;
                // pnlOrderNo.Visible = true;

            }
            else
            {
                ddlPaymentType.SelectedValue = Enums.PAYMENT_TYPES.CASH;


            }
            ddlCompany.Enabled = toggle == ToggleState.On;
            ddlCompany.SelectedValue = toggle == ToggleState.Off ? null : ddlCompany.SelectedValue;
        }

        private void ddlCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (chkIsCompanyRates.Checked)
            {
                int? companyId = ddlCompany.SelectedValue.ToIntorNull();


                if (companyId == null)
                {

                    ClearDepartment();
                    SetCashPaymentType();

                }
                else
                {
                    Gen_Company obj = General.GetObject<Gen_Company>(c => c.Id == companyId);
                    if (obj != null)
                    {
                        FillDepartmentsCombo(obj.Id);



                        SetCashAccount(obj.AccountTypeId.ToInt());


                    }
                    else
                    {

                        SetCashPaymentType();
                    }

                }
            }
            else
            {

                ClearDepartment();
                SetCashPaymentType();
            }
        }

        private void ClearDepartment()
        {
            ddlDepartment.DataSource = null;


        }

        private void FillDepartmentsCombo(int companyId)
        {
            ComboFunctions.FillCompanyDepartmentCombo(ddlDepartment, c => c.CompanyId == companyId);


        }

        private void frmMultiBooking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void radRadioButton1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                radPageView1.Pages[1].Item.Visibility = ElementVisibility.Collapsed;
                radPageView1.SelectedPage = pg_Daily;
            }
            else
            {
                radPageView1.Pages[1].Item.Visibility = ElementVisibility.Visible;


            }

        }

        private void optWeekly_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {
                radPageView1.Pages[0].Item.Visibility = ElementVisibility.Collapsed;
                radPageView1.SelectedPage = pg_weekly;
            }
            else
            {
                radPageView1.Pages[0].Item.Visibility = ElementVisibility.Visible;

            }
        }

        private void btnWeeklyCreateBooking_Click(object sender, EventArgs e)
        {
            try
            {

                ObjBookiing.IsCompanyWise = chkIsCompanyRates.Checked;
                ObjBookiing.CompanyId = ddlCompany.SelectedValue.ToIntorNull();
                ObjBookiing.DepartmentId = ddlDepartment.SelectedValue.ToIntorNull();
                ObjBookiing.PaymentTypeId = ddlPaymentType.SelectedValue.ToIntorNull();


                if (dtpWeekPickupTime.Value == null)
                {
                    ENUtils.ShowMessage("Required Pickup Time");
                    return;

                }




                CreateWeeklyBooking();

                // grdBookings.CurrentRow = grdBookings.RowCount == 1 ? grdBookings.Rows[0] : grdBookings.Rows[1];
                // SelectRowDetail(grdBookings.CurrentRow);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void CreateWeeklyBooking()
        {
            try
            {


                string fromAddress = ObjBookiing.FromAddress;

                string toAddress = ObjBookiing.ToAddress;
                decimal fareRate = numFareRateWeek.Value;
                decimal retFareRate = numWeekFareRateReturn.Value;

                int? fromLocTypeId = ObjBookiing.FromLocTypeId;
                int? fromLocId = ObjBookiing.FromLocId;

                int? toLocTypeId = ObjBookiing.ToLocTypeId;
                int? toLocId = ObjBookiing.ToLocId;


                int bookingsToCreate = 0;
                int weeksCnt = numWeeks.Value.ToInt();

                int weekDaysCnt = 0;

                DateTime? startingAt = dtpStartingAt.Value.Value.Date;


                List<string> daysList = new List<string>();


                weeksCnt += 2;

                if (chkAutoRecurred.Checked == false)
                {

                    if (chkMon.Checked)
                    {
                        weekDaysCnt++;

                        daysList.Add("monday");

                    }
                    if (chkTue.Checked)
                    {
                        weekDaysCnt++;
                        daysList.Add("tuesday");
                    }

                    if (chkWed.Checked)
                    {
                        weekDaysCnt++;
                        daysList.Add("wednesday");
                    }

                    if (chkThurs.Checked)
                    {
                        weekDaysCnt++;
                        daysList.Add("thursday");
                    }


                    if (chkFri.Checked)
                    {
                        weekDaysCnt++;
                        daysList.Add("friday");
                    }

                    if (chkSat.Checked)
                    {
                        weekDaysCnt++;
                        daysList.Add("saturday");
                    }


                    if (chkSun.Checked)
                    {
                        weekDaysCnt++;
                        daysList.Add("sunday");
                    }


                    if (numWeeks.Value == 1)
                        weeksCnt = 7;

                    //  bookingsToCreate = weeksCnt * weekDaysCnt;



                    //OC 14/7/16
                    // bookingsToCreate = weeksCnt * weekDaysCnt;
                    //
                    //NC 15/7/16
                    weeksCnt = (numWeeks.Value.ToInt() * 7);
                    bookingsToCreate = weeksCnt * weekDaysCnt;
                    //

                }


                if (bookingsToCreate == 0)
                {
                    ENUtils.ShowMessage("No Bookings To Create");
                    return;
                }

                if (startingAt == null)
                {
                    ENUtils.ShowMessage("Please specify starting at");
                    return;

                }
                else
                {

                    if (dtpEndingDate.Value == null)
                    {
                        ENUtils.ShowMessage("Please specify Ending Date");
                        return;

                    }


                    if (dtpEndingDate.Value != null && startingAt.ToDate() > dtpEndingDate.Value.ToDate())
                    {
                        ENUtils.ShowMessage("Starting Date must be less than Ending Date");
                        return;

                    }


                }

                // DANISH: Removed by Request of our Client=>Chepstrow Cars
                //else
                //{
                //    DateTime? matchStartDate = GetStartingPoint();

                //    if (startingAt < matchStartDate)
                //    {
                //        ENUtils.ShowMessage("Starting Date must be Start from " + string.Format("{0:dd/MM/yyyy}", matchStartDate));
                //        return;
                //    }

                //}


                DateTime? pickupDate = startingAt;

                DateTime? returnpickupDate = null;

                string day = string.Empty;

                string returnPickupTimeStr = string.Empty;

                bool skipWeekend = chkSkipWeekEnd.Checked;

                string dayOfWeek = string.Empty;
                grdBookings.Rows.Clear();

                grdBookings.RowCount = bookingsToCreate;

                bool IsStart = true;

                int rowCnt = 0;

                List<string> excludeDaysList = null;
                for (int i = 1; i <= weeksCnt; i++)
                {

                    for (int j = 0; j < daysList.Count; j++)
                    {

                        if (IsStart)
                        {

                            if (pickupDate.Value.DayOfWeek.ToStr().ToLower() == daysList[j])
                                IsStart = false;
                            else
                            {

                                if (excludeDaysList == null)
                                    excludeDaysList = new List<string>();

                                excludeDaysList.Add(daysList[j]);
                                continue;

                            }


                        }
                        else
                        {

                            while (pickupDate.Value.DayOfWeek.ToStr().ToLower() != daysList[j])
                                pickupDate = pickupDate.Value.AddDays(1);
                        }


                        pickupDate = pickupDate.Value.Date + ((TimeSpan)dtpWeekPickupTime.Value.Value.TimeOfDay);


                        if (dtpEndingDate.Value != null && pickupDate.Value.ToDate() > dtpEndingDate.Value.ToDate())
                            break;

                        if (chkReturnWeekJourney.Checked && dtpWeekReturnPickupTime.Value != null)
                        {
                            returnpickupDate = pickupDate.Value.Date;
                            returnpickupDate = returnpickupDate.Value.ToDate() + ((TimeSpan)dtpWeekReturnPickupTime.Value.Value.TimeOfDay);


                        }



                        AddBooking(grdBookings.Rows[rowCnt], string.Format("{0:ddd}", pickupDate), pickupDate.ToDateTime(), returnpickupDate.ToDateTimeorNull(), fromLocTypeId, fromLocId,
                                             fromAddress, toLocTypeId, toLocId, toAddress, fareRate, retFareRate);


                        rowCnt++;


                    }




                    //if (excludeDaysList != null)
                    //{

                    //    for (int j = 0; j < excludeDaysList.Count; j++)
                    //    {



                    //        while (pickupDate.Value.DayOfWeek.ToStr().ToLower() != excludeDaysList[j])
                    //                pickupDate = pickupDate.Value.AddDays(1);





                    //        pickupDate = pickupDate.Value.Date + ((TimeSpan)dtpWeekPickupTime.Value.Value.TimeOfDay);



                    //        if (dtpEndingDate.Value != null && pickupDate.Value.ToDate() > dtpEndingDate.Value.ToDate())
                    //            break;


                    //        if (chkReturnWeekJourney.Checked && dtpWeekReturnPickupTime.Value != null)
                    //        {
                    //            returnpickupDate = pickupDate.Value.Date;
                    //            returnpickupDate = returnpickupDate.Value.ToDate() + ((TimeSpan)dtpWeekReturnPickupTime.Value.Value.TimeOfDay);


                    //        }


                    //        if (rowCnt >= grdBookings.Rows.Count)
                    //            break;

                    //        AddBooking(grdBookings.Rows[rowCnt], string.Format("{0:ddd}", pickupDate), pickupDate.ToDateTime(), returnpickupDate.ToDateTimeorNull(), fromLocTypeId, fromLocId,
                    //                             fromAddress, toLocTypeId, toLocId, toAddress, fareRate, retFareRate);


                    //        rowCnt++;


                    //    }

                    //}

                    pickupDate = pickupDate.Value.AddDays(1);


                    if (dtpEndingDate.Value != null && pickupDate.Value.ToDate() > dtpEndingDate.Value.ToDate())
                        break;
                }


            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }


        }

        private void chkMon_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            DateTime currentDate = DateTime.Now;

            if (chkMon.Checked)
                SetStartingPoint(ref currentDate, "monday");


            else if (chkTue.Checked)
                SetStartingPoint(ref currentDate, "tuesday");

            else if (chkWed.Checked)
                SetStartingPoint(ref currentDate, "wednesday");


            else if (chkThurs.Checked)
                SetStartingPoint(ref currentDate, "thursday");


            else if (chkFri.Checked)
                SetStartingPoint(ref currentDate, "friday");


            else if (chkSat.Checked)
                SetStartingPoint(ref currentDate, "saturday");


            else if (chkSun.Checked)
                SetStartingPoint(ref currentDate, "sunday");


            dtpStartingAt.Value = currentDate;

        }


        private void SetStartingPoint(ref DateTime currentDate, string day)
        {

            if (dtpStartingAt.Value != null)
                return;

            while (currentDate.DayOfWeek.ToStr().ToLower() != day)
                currentDate = currentDate.AddDays(1);


        }


        private DateTime GetStartingPoint()
        {

            DateTime currentDate = DateTime.Now;
            string day = string.Empty;

            if (chkMon.Checked)
                day = "monday";



            else if (chkTue.Checked)
                day = "tuesday";

            else if (chkWed.Checked)
                day = "wednesday";



            else if (chkThurs.Checked)
                day = "thursday";


            else if (chkFri.Checked)
                day = "friday";


            else if (chkSat.Checked)
                day = "saturday";


            else if (chkSun.Checked)
                day = "sunday";





            while (currentDate.DayOfWeek.ToStr().ToLower() != day)
                currentDate = currentDate.AddDays(1);



            return currentDate.Date;

        }

        private void btnCalculateWeekFare_Click(object sender, EventArgs e)
        {
            CalculateWeekFares();
        }

        private void chkReturnWeekJourney_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            if (chkReturnWeekJourney.ToggleState == ToggleState.On)
            {

                numWeekFareRateReturn.Enabled = true;
                numWeekCustFareRateReturn.Enabled = true;
                numWeekCompanyFareRateReturn.Enabled = true;

                dtpWeekReturnPickupTime.Enabled = true;
                dtpWeekReturnPickupTime.Value = DateTime.Now;
            }
            else
            {
                numWeekFareRateReturn.Value = 0;
                dtpWeekReturnPickupTime.Value = null;

                numWeekFareRateReturn.Value = 0.00m;
                numWeekCustFareRateReturn.Value = 0.00m;
                numWeekCompanyFareRateReturn.Value = 0.00m;


                numWeekFareRateReturn.Enabled = false;
                numWeekCustFareRateReturn.Enabled = false;
                numWeekCompanyFareRateReturn.Enabled = false;


            }
        }

        private void ddlDriver_Enter(object sender, EventArgs e)
        {
            if (ddlDriver.DataSource == null)
            {
                ComboFunctions.FillDriverNoCombo(ddlDriver);
            }
        }

        private void ddlReturnAllocatedDriver_Enter(object sender, EventArgs e)
        {
            if (ddlReturnAllocatedDriver.DataSource == null)
            {
                ComboFunctions.FillDriverNoCombo(ddlReturnAllocatedDriver);
            }
        }



    }
}
