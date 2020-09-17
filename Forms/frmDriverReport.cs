using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmDriverReport : UI.SetupBase
    {

        RadDropDownMenu menu_Job = null;

        public struct COLS
        {
            public static string ID = "ID";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string RefNumber = "RefNumber";

            public static string Account = "Account";

            public static string PickupPoint = "PickupPoint";
            public static string Via = "Via";
            public static string Destination = "Destination";

            public static string Charges = "Charges";

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string Total = "Total";
            public static string Amount = "Amount";

        }


        public frmDriverReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDriverReport_Load);

            grdLister.EnableHotTracking = false;
            //grdLister.AutoCellFormatting = true;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);

            grdLister.ShowGroupPanel = false;

            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);

            grdLister.EnableAlternatingRowColor = true;
            grdLister.TableElement.AlternatingRowColor = Color.AliceBlue;
            txtRent.Enabled = false;


            this.FormClosed += new FormClosedEventHandler(frmDriverReport_FormClosed);
        }

        void frmDriverReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
            GC.Collect();
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
                    General.ShowBookingForm(grdLister.CurrentRow.Cells[COLS.ID].Value.ToInt(), true, "", "", Enums.BOOKING_TYPES.LOCAL);

                }
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);

            }
        }


        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

        Font newFont = new Font("Tahoma", 10, FontStyle.Bold);


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
        void grdLister_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

            else if (e.CellElement is GridFilterCellElement)
            {
                e.CellElement.Font = oldFont;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.BackColor = Color.White;
                e.CellElement.RowElement.BackColor = Color.White;
                e.CellElement.RowElement.NumberOfColors = 1;

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
            }

            else if (e.CellElement is GridDataCellElement)
            {



                e.CellElement.ToolTipText = e.CellElement.Text;
                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;


            }




        }

        void grdLister_CommandCellClick(object sender, EventArgs e)
        {
            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name == "btnUpdate")
            {

                GridViewRowInfo row = gridCell.RowInfo;

                if (row is GridViewDataRowInfo)
                {
                    long id = row.Cells[COLS.ID].Value.ToLong();
                    decimal fare = row.Cells[COLS.Charges].Value.ToDecimal();
                 //   decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                 //   decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                    decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                    decimal meetAndGreet = row.Cells[COLS.Waiting].Value.ToDecimal();
                    decimal CongtionCharge = row.Cells[COLS.Parking].Value.ToDecimal();
                    decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();


                    BookingBO objMaster = new BookingBO();
                    objMaster.GetByPrimaryKey(id);

                    if (objMaster.Current != null)
                    {
                        objMaster.Current.FareRate = fare;
                     //   objMaster.Current.ParkingCharges = CongtionCharge;
                     //   objMaster.Current.WaitingCharges = meetAndGreet;
                        objMaster.Current.ExtraDropCharges = extraDrop;
                        objMaster.Current.MeetAndGreetCharges = meetAndGreet;
                        objMaster.Current.CongtionCharges = CongtionCharge;
                        objMaster.Current.TotalCharges = TotalCharges;
                        objMaster.CheckCustomerValidation = false;
                        objMaster.DisableUpdateReturnJob = true;

                        objMaster.Save();

                        ViewReport();
                    }


                }


            }

        }

        void frmDriverReport_Load(object sender, EventArgs e)
        {


            if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "SPECIAL") > 0)
            {

                ChkRent.Visible = true;
                txtRent.Visible = true;

            }
            //  ViewReport();

            ComboFunctions.FillDriverNoCombo(ddl_Driver, c => c.DriverTypeId == 1 && (c.SubcompanyId==AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId==0));
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());

            dtpTillDate.Value=dtpTillDate.Value.Value.AddHours(23).AddMinutes(59);

            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);

            if (ddlSubCompany.Items.Count > 1)
                ddlSubCompany.SelectedIndex = 1;
            else
                ddlSubCompany.SelectedIndex = 0;



            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
            colDt.Name = "PickupDate";
            colDt.ReadOnly = true;
            colDt.HeaderText = "Pickup Date-Time";
            grdLister.Columns.Add(colDt);



            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Ref #";
            col.Name = "RefNumber";
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.HeaderText = "Vehicle";
            col.Name = "Vehicle";
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            //  col.IsVisible = false;
            col.ReadOnly = true;
            col.Name = "Account";
            col.HeaderText = "Account";
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Pickup Point";
            col.Name = "PickupPoint";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.ReadOnly = true;
            col.HeaderText = "Via";
            col.Name = "Via";
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            //     col.IsVisible = false;
            col.ReadOnly = true;
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
            colD.HeaderText = "M & G";
            colD.Name = "MeetAndGreet";
            colD.Maximum = 9999999;
            colD.IsVisible = false;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Congtion Charge";
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
            colD.Expression = "Charges+MeetAndGreet+CongtionCharge+ExtraDrop";
            grdLister.Columns.Add(colD);



            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Amount";
            colD.Name = "Amount";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);

            //     grdLister.Columns["Id"].IsVisible = false;

            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdLister.Columns["PickUpDate"].Width = 90;
            grdLister.Columns["RefNumber"].Width = 40;
            grdLister.Columns["Vehicle"].Width = 45;
            grdLister.Columns["Account"].Width = 60;
            grdLister.Columns["PickUpPoint"].Width = 85;
            grdLister.Columns["Via"].Width = 40;
            grdLister.Columns["Destination"].Width = 85;

            grdLister.Columns["Charges"].Width = 45;
            grdLister.Columns["Parking"].Width = 45;
            grdLister.Columns["Waiting"].Width = 50;
            grdLister.Columns["ExtraDrop"].Width = 55;
            grdLister.Columns["MeetAndGreet"].Width = 40;
            grdLister.Columns["CongtionCharge"].Width = 85;
            grdLister.Columns["Total"].Width = 40;
            grdLister.Columns["Amount"].Width = 50;

            grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            grdLister.Columns["RefNumber"].HeaderText = "Ref #";
            grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
            grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";

            grdLister.Columns["MeetAndGreet"].HeaderText = "M & G";
            grdLister.Columns["CongtionCharge"].HeaderText = "Congtion Charge";

         

            AddUpdateColumn(grdLister);

            grdLister.AllowEditRow = true;

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


        private void ViewReport()
        {
            int? companyId = null;
            int driverId = ddl_Driver.SelectedValue.ToInt();
            DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
            DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

            string error = string.Empty;
            if (driverId == 0)
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

            lblCriteria.Text = "Driver Report Related to '" + ddl_Driver.Text.ToStr() + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);



            bool excludeCreditCard = chkExcludeCC.Checked;

            



            int statementType = 0;
            if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On )
            {
                statementType = eStatementType.AccountStatement;
            }
            else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.CashStatement;
            }
            else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.Both;
            }
            else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.CREDITCARD;
            }



            //if (optCreditCard.ToggleState == ToggleState.On)
            //{
            //    companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == "credit card" || c.CompanyName.ToLower() == "creditcard").DefaultIfEmpty().Id;


            //}


          
            var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);

            var list = (from a in query
                        where a.DriverId == driverId && 
                        
                       ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (excludeCreditCard ==false ||  a.Gen_Company.AccountTypeId!=3 ))
                                               || (statementType == eStatementType.CashStatement && a.CompanyId == null && (excludeCreditCard == false || (a.PaymentTypeId != Enums.PAYMENT_TYPES.CREDIT_CARD && a.PaymentTypeId != Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)))
                                                       || (statementType == eStatementType.Both &&  (excludeCreditCard ==false || (a.CompanyId==null ||  a.Gen_Company.AccountTypeId!=3) ))
                                                       || (statementType == eStatementType.CREDITCARD && a.CompanyId!=null && (  a.Gen_Company.AccountTypeId==3 ) ) 
                                                       
                                                       
                                                       )
                                                                 && (companyId==null || a.CompanyId==companyId)

                            && (a.PickupDateTime.Value >= fromDate && a.PickupDateTime.Value <= tillDate)
                        //     orderby a.PickupDateTime descending
                        select new
                        {
                            Id = a.Id,
                            PickUpDate = a.PickupDateTime,
                            RefNumber = a.BookingNo,
                            Vehicle = a.VehicleTypeId != null ? a.Fleet_VehicleType.VehicleType : "",
                            Account = a.CompanyId != null ? a.Gen_Company.CompanyName : "",
                            PickupPoint = a.FromAddress,
                            Via = a.ViaString,
                            Destination = a.ToAddress,
                            Charge = a.FareRate,
                            Parking = a.CongtionCharges,
                            Waiting = a.MeetAndGreetCharges,
                            ExtraDrop = a.ExtraDropCharges,
                            MeetAndGreet = a.MeetAndGreetCharges,
                            CongtionCharge = a.CongtionCharges,
                            Total = a.TotalCharges,
                            DriverCommission = a.DriverCommission,
                            DriverCommissionType = a.DriverCommissionType,
                            AccountType = a.CompanyId != null ? a.Gen_Company.AccountTypeId : null
                        }).OrderByDescending(a => a.PickUpDate).ToList();

            // grdLister.DataSource = list;
            grdLister.RowCount = list.Count;

            for (int i = 0; i < list.Count; i++)
            {
                grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i].PickUpDate;
                grdLister.Rows[i].Cells[COLS.RefNumber].Value = list[i].RefNumber.ToStr();
                grdLister.Rows[i].Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();
                grdLister.Rows[i].Cells[COLS.Account].Value = list[i].Account.ToStr();
                grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].PickupPoint.ToStr();
                grdLister.Rows[i].Cells[COLS.Via].Value = list[i].Via.ToStr();
                grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].Destination.ToStr();
                grdLister.Rows[i].Cells[COLS.Charges].Value = list[i].Charge.ToDecimal();
                grdLister.Rows[i].Cells[COLS.Parking].Value = list[i].Parking.ToDecimal();
                grdLister.Rows[i].Cells[COLS.Waiting].Value = list[i].Waiting.ToDecimal();
                grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = list[i].ExtraDrop.ToDecimal();
                grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = list[i].MeetAndGreet.ToDecimal();
                grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = list[i].CongtionCharge.ToDecimal();
                grdLister.Rows[i].Cells[COLS.Total].Value = list[i].Total.ToDecimal();


                grdLister.Rows[i].Cells[COLS.Amount].Value = list[i].AccountType.ToInt() == Enums.ACCOUNT_TYPE.CASH && list[i].DriverCommissionType == "Amount" ? list[i].DriverCommission.ToDecimal() : 0;


            }

            decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());
            string total = totalEarning.ToStr();
            lblTotalEarning.Text = "Total Earning £ " + total;

            lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();


        }


        private void ViewReportRentWise()
        {
            int? companyId = null;
            int driverId = ddl_Driver.SelectedValue.ToInt();
            DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
            DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

            string error = string.Empty;
            if (driverId == 0)
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

            lblCriteria.Text = "Driver Report Related to '" + ddl_Driver.Text.ToStr() + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);


            int statementType = 0;
            if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On )
            {
                statementType = eStatementType.AccountStatement;
            }
            else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.CashStatement;
            }
            else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.Both;
            }


            //if (optCreditCard.ToggleState == ToggleState.On)
            //{
            //    companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == "credit card" || c.CompanyName.ToLower() == "creditcard").DefaultIfEmpty().Id;


            //}

            int creditCardAccountId = 0;

            if (chkExcludeCC.Checked)
            {
                creditCardAccountId = General.GetObject<Gen_Company>(c => c.CompanyName.ToUpper().Replace(" ", "").Trim() == "CREDITCARD").DefaultIfEmpty().Id;
            }



            var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);

            var list = (from a in query
                        where a.DriverId == driverId && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (creditCardAccountId == 0 || (a.CompanyId != creditCardAccountId)))
                                               || (statementType == eStatementType.CashStatement && a.CompanyId == null)
                                                       || (statementType == eStatementType.Both && (creditCardAccountId == 0 || (a.CompanyId == null || a.CompanyId != creditCardAccountId)))
                                                       || (statementType == eStatementType.CREDITCARD && a.CompanyId != null && (a.Gen_Company.AccountTypeId == 3))
                                                       
                                                       )
                                                       && (companyId == null || a.CompanyId == companyId)
                            && (a.PickupDateTime.Value >= fromDate && a.PickupDateTime.Value <= tillDate)
                        select new
                        {
                            Id = a.Id,
                            PickUpDate = a.PickupDateTime,
                            RefNumber = a.BookingNo,
                            Vehicle = a.VehicleTypeId != null ? a.Fleet_VehicleType.VehicleType : "",
                            Account = a.CompanyId != null ? a.Gen_Company.CompanyName : "",
                            PickupPoint = a.FromAddress,
                            Via = a.ViaString,
                            Destination = a.ToAddress,
                            Charge = a.FareRate,
                            Parking = a.CongtionCharges,
                            Waiting = a.MeetAndGreetCharges,
                            ExtraDrop = a.ExtraDropCharges,
                            MeetAndGreet = a.MeetAndGreetCharges,
                            CongtionCharge = a.CongtionCharges,
                            Total = a.TotalCharges,
                            DriverCommission = a.DriverCommission,
                            DriverCommissionType = a.DriverCommissionType,
                            AccountType = a.CompanyId != null ? a.Gen_Company.AccountTypeId : null
                        }).OrderByDescending(a => a.PickUpDate).ToList();

            // grdLister.DataSource = list;
            grdLister.RowCount = list.Count;

            for (int i = 0; i < list.Count; i++)
            {
                grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i].PickUpDate;
                grdLister.Rows[i].Cells[COLS.RefNumber].Value = list[i].RefNumber.ToStr();
                grdLister.Rows[i].Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();
                grdLister.Rows[i].Cells[COLS.Account].Value = list[i].Account.ToStr();
                grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].PickupPoint.ToStr();
                grdLister.Rows[i].Cells[COLS.Via].Value = list[i].Via.ToStr();
                grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].Destination.ToStr();
                grdLister.Rows[i].Cells[COLS.Charges].Value = list[i].Charge.ToDecimal();
                grdLister.Rows[i].Cells[COLS.Parking].Value = list[i].Parking.ToDecimal();
                grdLister.Rows[i].Cells[COLS.Waiting].Value = list[i].Waiting.ToDecimal();
                grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = list[i].ExtraDrop.ToDecimal();
                grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = list[i].MeetAndGreet.ToDecimal();
                grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = list[i].CongtionCharge.ToDecimal();
                grdLister.Rows[i].Cells[COLS.Total].Value = list[i].Total.ToDecimal();


                grdLister.Rows[i].Cells[COLS.Amount].Value = list[i].AccountType.ToInt() == Enums.ACCOUNT_TYPE.CASH && list[i].DriverCommissionType == "Amount"
                    ? list[i].DriverCommission.ToDecimal() : 0;


            }

            decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());
            string total = totalEarning.ToStr();
            lblTotalEarning.Text = "Total Earning £ " + total;

            lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();


        }
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                ViewReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public override void Print()
        {

            try
            {
                int? companyId = null;

                int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
                DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
                DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

                string error = string.Empty;

                if (driverId == null)
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

                int statementType = 0;

                //int creditCardAccountId = 0;

                //if (chkExcludeCC.Checked)
                //{
                //    creditCardAccountId = General.GetObject<Gen_Company>(c => c.CompanyName.ToUpper().Replace(" ", "").Trim() == "CREDITCARD").DefaultIfEmpty().Id;
                //}



                //if (optCreditCard.ToggleState == ToggleState.On)
                //{
                //    companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == "credit card" || c.CompanyName.ToLower() == "creditcard").DefaultIfEmpty().Id;


                //}



                rptfrmDriverStatement frm = new rptfrmDriverStatement();


                frm.objSubCompany = General.GetObject<Gen_SubCompany>(c => c.Id == ddlSubCompany.SelectedValue.ToInt());

                if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On )
                {
                    statementType = eStatementType.AccountStatement;


                }
                else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.CashStatement;


                }

                else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.Both;
                }
                else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.CREDITCARD;
                }


                UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "rptfrmDriverStatement" && c.IsDefault == true);


                if (objTemplate.TemplateName.ToStr() == "Template1" || objTemplate.TemplateName.ToStr() == "Template2")
                {

                    frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate,companyId);

                }
                else if (objTemplate.TemplateName.ToStr() == "Template3")
                {

                    frm.DataSource2 = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);
                    frm.fromDate = fromDate;
                    frm.tillDate = tillDate;
                    frm.driverId = driverId;
                    frm.companyId = null;
                    frm.statementType = statementType;
                }



                frm.Rent = ChkRent.Checked == true ? txtRent.Value.ToInt() : 0;


                frm.ObjDriver = General.GetObject<Fleet_Driver>(c => c.Id == driverId).DefaultIfEmpty();


                frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                frm.StatementType = statementType;
                frm.GenerateReport();

                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverStatement1");

                if (doc != null)
                {
                    doc.Close();
                }
                UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private List<Vu_BookingBase> GetDataSource(int? driverId, int statementType, DateTime? fromDate, DateTime? tillDate,int? companyId)
        {

                int creditCardAccountId = 0;


                bool excludeCreditCard = chkExcludeCC.Checked;


                if (chkExcludeCC.Checked)
                {
                    creditCardAccountId = General.GetObject<Gen_Company>(c => c.CompanyName.ToUpper().Replace(" ", "").Trim() == "CREDITCARD").DefaultIfEmpty().Id;
                }           


            //return General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId
            //                                           && ((statementType == eStatementType.AccountStatement && c.CompanyId != null)
            //                                           || (statementType == eStatementType.CashStatement && c.CompanyId == null)
            //                                                   || (statementType == eStatementType.Both))



            //                                                     && (companyId == null || c.CompanyId == companyId))
            //               .Where(b => (b.PickupDateTime.Value >= fromDate && b.PickupDateTime.Value <= tillDate))
            //                   .OrderByDescending(c => c.PickupDateTime).ToList();


              return General.GetQueryable<Vu_BookingBase>(c =>c.BookingStatusId==Enums.BOOKINGSTATUS.DISPATCHED &&

                     c.DriverId == driverId && ((statementType == eStatementType.AccountStatement && c.CompanyId != null && (excludeCreditCard ==false || (c.PaymentTypeId !=Enums.PAYMENT_TYPES.CREDIT_CARD && c.PaymentTypeId!=Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)))
                                               || (statementType == eStatementType.CashStatement && c.CompanyId == null && (excludeCreditCard == false || (c.PaymentTypeId != Enums.PAYMENT_TYPES.CREDIT_CARD && c.PaymentTypeId != Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)))
                                                       || (statementType == eStatementType.Both &&  (excludeCreditCard ==false || (c.PaymentTypeId !=Enums.PAYMENT_TYPES.CREDIT_CARD && c.PaymentTypeId!=Enums.PAYMENT_TYPES.CREDIT_CARD_PAID)))
                                                       || (statementType == eStatementType.CREDITCARD && (c.PaymentTypeId != Enums.PAYMENT_TYPES.CREDIT_CARD && c.PaymentTypeId != Enums.PAYMENT_TYPES.CREDIT_CARD_PAID))
                                                       )
                                                                                     && (companyId == null || c.CompanyId == companyId))
                           .Where(b => (b.PickupDateTime.Value >= fromDate && b.PickupDateTime.Value <= tillDate))
                               .OrderByDescending(c => c.PickupDateTime).ToList();



            //                                                     && (companyId == null || c.CompanyId == companyId))
            //               .Where(b => (b.PickupDateTime.Value >= fromDate && b.PickupDateTime.Value <= tillDate))
            //                   .OrderByDescending(c => c.PickupDateTime).ToList();

        }

        private List<stp_DriverCommisionResult> GetDataSource2(DateTime? fromDate, DateTime? tillDate, int? driverId, int? companyId, int? statementType)
        {
            //return General.GetQueryable<stp_DriverCommisionResult>(c => c.DriverId == driverId && c.BookingStatusId == 1
            //    && ((c.BookingStatusId == 2 && c.DriverId == driverId && c.CompanyId!=null &&(c.CompanyId==companyId || companyId==null))
            //    || (c.BookingStatusId == 3 && c.DriverId == driverId)
            //    || (c.BookingStatusId == 4 && c.DriverId == driverId)
            //    || (c.BookingStatusId == 5 && c.DriverId == driverId)

            //    )).Where(b => (b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate)).OrderByDescending(c => c.PickupDateTime).ToList();


            int creditCardAccountId = 0;

            if (chkExcludeCC.Checked)
            {
                creditCardAccountId = 1;
            }

            return new TaxiDataContext().stp_DriverCommision(dtpFromDate.Value, dtpTillDate.Value, driverId, companyId, statementType, creditCardAccountId).ToList();


            //, c => c.DriverId == driverId && c. && c.BookingStatusId == 1 
            // && ((c.BookingStatusId == 2 && c.DriverId == driverId && c.CompanyId != null && (c.CompanyId == companyId || companyId == null))
            // || (c.BookingStatusId == 3 && c.DriverId == driverId)
            // || (c.BookingStatusId == 4 && c.DriverId == driverId)
            // || (c.BookingStatusId == 5 && c.DriverId == driverId)

            //   )).Where(b => (b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate)).OrderByDescending(c => c.PickupDateTime).ToList();
            //return new TaxiDataContext().stp_DriverCommision(fromDate, tillDate, driverId, companyId,statementType).ToList();

        }

        public struct eStatementType
        {
            public static int AccountStatement = 1;
            public static int CashStatement = 2;
            public static int Both = 3;
            public static int CREDITCARD = 5;


        } ;

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            int? companyId = null;
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
            DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

            string error = string.Empty;

            if (driverId == null)
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


            //if (optCreditCard.ToggleState == ToggleState.On)
            //{
            //    companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == "credit card" || c.CompanyName.ToLower() == "creditcard").DefaultIfEmpty().Id;


            //}


            rptfrmDriverStatement frm = new rptfrmDriverStatement();

            int statementType = 0;
            if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.AccountStatement;


            }
            else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.CashStatement;


            }
            else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.Both;
            }

            //var list = General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId).AsEnumerable()
            //                .Where(b => (b.PickupDateTime.ToDate() >= fromDate && b.PickupDateTime.ToDate() <= tillDate))
            //                    .OrderByDescending(c => c.PickupDateTime).ToList();

            // frm.ReportHeading = "Driver Report for Date Range :" + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
            frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);


            UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "rptfrmDriverStatement" && c.IsDefault == true);


            if (objTemplate.TemplateName.ToStr() == "Template1" || objTemplate.TemplateName.ToStr() == "Template2")
            {

                frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate,companyId);

            }
            else if (objTemplate.TemplateName.ToStr() == "Template3")
            {
              

                frm.DataSource2 = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);
                frm.fromDate = fromDate;
                frm.tillDate = tillDate;
                frm.driverId = driverId;
                frm.companyId = null;
                frm.statementType = statementType;



            }
            
            //  frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate);
            frm.ObjDriver = General.GetObject<Fleet_Driver>(c => c.Id == driverId).DefaultIfEmpty();

            frm.GenerateReport();
            frm.ExportReport();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            int? companyId = null;
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
            DateTime? tillDate = dtpTillDate.Value.ToDateTimeorNull();

            string error = string.Empty;

            if (driverId == null)
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

            //if (optCreditCard.ToggleState == ToggleState.On)
            //{
            //    companyId = General.GetObject<Gen_Company>(c => c.CompanyName.ToLower() == "credit card" || c.CompanyName.ToLower() == "creditcard").DefaultIfEmpty().Id;


            //}


            rptfrmDriverStatement frm = new rptfrmDriverStatement();


            int statementType = 0;
            if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On )
            {
                statementType = eStatementType.AccountStatement;


            }
            else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.CashStatement;


            }
            else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.Both;
            }
            else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                statementType = eStatementType.CREDITCARD;
            }
            //var list = General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId).AsEnumerable()
            //                .Where(b => (b.PickupDateTime.ToDate() >= fromDate && b.PickupDateTime.ToDate() <= tillDate))
            //                    .OrderByDescending(c => c.PickupDateTime).ToList();

            frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
          //  frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate);


            UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "rptfrmDriverStatement" && c.IsDefault == true);


            if (objTemplate.TemplateName.ToStr() == "Template1" || objTemplate.TemplateName.ToStr() == "Template2")
            {

                frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate,companyId);

            }

            else if (objTemplate.TemplateName.ToStr() == "Template3")
            {

                frm.DataSource2 = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);
                frm.fromDate = fromDate;
                frm.tillDate = tillDate;
                frm.driverId = driverId;
                frm.companyId = null;
                frm.statementType = statementType;
            }
           

            
            frm.ObjDriver = General.GetObject<Fleet_Driver>(c => c.Id == driverId).DefaultIfEmpty();

            frm.GenerateReport();
            frm.SendEmail();
        }

        private void ChkCommission_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            try
            {
                IsRentWise(args.ToggleState);
            }
            catch (Exception ex)
            {
            }

        }
        private void IsRentWise(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                txtRent.Enabled = true;
            }
            else
            {
                txtRent.Enabled = false;
            }
        }

        private void frmDriverReport_Load_1(object sender, EventArgs e)
        {

        }
    }
}
