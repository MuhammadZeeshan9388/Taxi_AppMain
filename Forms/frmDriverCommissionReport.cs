

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
    public partial class frmDriverCommissionReport : UI.SetupBase
    {
        RadDropDownMenu menu_Job = null;
        bool IsLoaded;

        private bool NoACCommission;



        List<Fleet_Driver_CommissionRange> listOfCommRange = null;


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
            public static string AgentFees = "AgentFees";
            public static string Total = "Total";
            public static string Commission = "Commission";
            public static string DriverCommission = "DriverCommission";
        }


        public frmDriverCommissionReport()
        {
            try
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


                grdLister.EnableAlternatingRowColor = true;
                grdLister.TableElement.AlternatingRowColor = Color.AliceBlue;



                if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool())
                {

                    txtCommission.Visible = false;
                    ChkCommission.Visible = false;



                    listOfCommRange = GetSystemCommissionRange();


                }
                else
                {


                    txtCommission.Enabled = false;

                    txtCommission.Value = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToInt();
                }


                this.NoACCommission = AppVars.objPolicyConfiguration.NoCommissionFromAccount.ToBool();

                grdLister.ContextMenuOpening += new ContextMenuOpeningEventHandler(grdLister_ContextMenuOpening);
                ddl_Driver.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddl_Driver_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }


        private List<Fleet_Driver_CommissionRange> GetSystemCommissionRange()
        {

            return (from a in General.GetQueryable<Gen_SysPolicy_CommissionPriceRange>(null).ToList()
                    select new Fleet_Driver_CommissionRange
                    {
                        DriverId = 0,
                        FromPrice = a.FromPrice,
                        ToPrice = a.ToPrice,
                        CommissionValue = a.CommissionValue


                    }).ToList();


        }


        private decimal PDARent;

        void ddl_Driver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (!IsLoaded)
                    return;


                if (ddl_Driver.SelectedValue != null)
                {

                   var objDrv = General.GetObject<Fleet_Driver>(c=>c.Id== ddl_Driver.SelectedValue.ToInt());

                   if (objDrv != null )
                   {
                       if (listOfCommRange == null)
                       {
                           if (ChkCommission.Visible && objDrv.DriverCommissionPerBooking.ToDecimal() > 0)
                           {
                               ChkCommission.Checked = true;
                               txtCommission.Value = objDrv.DriverCommissionPerBooking.ToDecimal();
                           }
                       }
                       else
                       {
                           if (objDrv.Fleet_Driver_CommissionRanges.Count > 0)
                           {
                               listOfCommRange = objDrv.Fleet_Driver_CommissionRanges.ToList();
                           }
                           else
                           {
                               listOfCommRange = GetSystemCommissionRange();

                           }


                       }


                       this.PDARent = objDrv.PDARent.ToDecimal();

                   }
                }
            }
            catch
            {


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
                        decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                        decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                        decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();

                        decimal agentFees = row.Cells[COLS.AgentFees].Value.ToDecimal();
                   //     decimal meetAndGreet = row.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                   //     decimal CongtionCharge = row.Cells[COLS.CongtionCharge].Value.ToDecimal();
                        decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();

                        BookingBO objMaster = new BookingBO();
                        try
                        {

                          
                            objMaster.GetByPrimaryKey(id);

                            if (objMaster.Current != null)
                            {
                                objMaster.Current.FareRate = fare;
                                objMaster.Current.CongtionCharges = parking;
                                objMaster.Current.MeetAndGreetCharges = waiting;
                                objMaster.Current.ExtraDropCharges = extraDrop;
                                objMaster.Current.AgentCommission = agentFees;
                              //  objMaster.Current.MeetAndGreetCharges = meetAndGreet;
                             //   objMaster.Current.CongtionCharges = CongtionCharge;
                                objMaster.Current.TotalCharges = TotalCharges;

                                objMaster.DisableUpdateReturnJob = true;
                                objMaster.Save();




                                LoadGridReport();
                            }
                        }
                        catch (Exception ex)
                        {
                           if(objMaster.Errors.Count > 0)
                               ENUtils.ShowMessage(objMaster.ShowErrors());
                           else
                               ENUtils.ShowMessage(ex.Message);

                        }
                    }
                }
          


        }

        void frmDriverReport_Load(object sender, EventArgs e)
        {
            if (AppVars.objPolicyConfiguration.PriceRangeWiseCommission.ToBool() == false)
            {

                if (AppVars.listUserRights.Count(c => c.formName == this.Name && c.functionId == "SPECIAL") > 0)
                {
                    ChkCommission.Visible = true;
                    txtCommission.Visible = true;
                }
            }


            
            //  ViewReport();
            lblJobsTotal.Text = "";
            lblTotalEarning.Visible = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool();

            ComboFunctions.FillDriverNoCombo(ddl_Driver, c => c.DriverTypeId == 2 &&  (c.SubcompanyId==AppVars.DefaultDriverSubCompanyId || AppVars.DefaultDriverSubCompanyId==0));
            
            
            
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());

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
            col.Name = "Via";
            col.HeaderText = "Via";
            col.Width = 40;
            col.ReadOnly = true;
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
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "Congtion";
            colD.Name = "CongtionCharge";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = false;
            grdLister.Columns.Add(colD);




            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.HeaderText = "DriverCommission";
            colD.Name = "DriverCommission";
            colD.Maximum = 9999999;
            colD.IsVisible = false;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);




            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.Width = 80;
            colD.HeaderText = "Agent Fees";
            colD.Name = "AgentFees";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.IsVisible = AppVars.objPolicyConfiguration.BookingPaymentDetailsType.ToInt()==3;
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Total";
            colD.Name = "Total";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.Expression = "(Charges+Parking+Waiting+ExtraDrop)-AgentFees";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Commission";
            colD.Name = "Commission";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            //   colD.Expression = "(Total*DriverCommission)/100";

            grdLister.Columns.Add(colD);



            //     grdLister.Columns["Id"].IsVisible = false;

            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdLister.Columns["PickUpDate"].Width = 90;
            grdLister.Columns["RefNumber"].Width = 40;
            grdLister.Columns["Vehicle"].Width = 50;
            grdLister.Columns["Account"].Width = 60;
            grdLister.Columns["PickUpPoint"].Width = 90;
            grdLister.Columns["Destination"].Width = 90;

            grdLister.Columns["Charges"].Width = 45;
            grdLister.Columns["Parking"].Width = 45;
            grdLister.Columns["Waiting"].Width = 50;
            grdLister.Columns["ExtraDrop"].Width = 55;
            grdLister.Columns["MeetAndGreet"].Width = 50;
            grdLister.Columns["CongtionCharge"].Width = 55;
            grdLister.Columns["Total"].Width = 40;
            grdLister.Columns["Commission"].Width = 55;

            grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            grdLister.Columns["RefNumber"].HeaderText = "Ref #";
            grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
            grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";

         //   grdLister.Columns["MeetAndGreet"].HeaderText = "M & G";
         //   grdLister.Columns["CongtionCharge"].HeaderText = "Congtion";

            //grdLister.Columns["MeetAndGreet"].ReadOnly = false;
            //grdLister.Columns["CongtionCharge"].ReadOnly = false;
            //grdLister.Columns["Charge"].ReadOnly = false;
            //grdLister.Columns["Parking"].ReadOnly = false;
            //grdLister.Columns["Waiting"].ReadOnly = false;
            //grdLister.Columns["ExtraDrop"].ReadOnly = false;

            AddUpdateColumn(grdLister);

            grdLister.AllowEditRow = true;
            IsLoaded = true;
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
            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();
                int companyId = ddlCompany.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();
                bool PickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool();


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


                lblCriteria.Text = "Driver Report Related to '" + ddl_Driver.Text.ToStr()
                    + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);


                int statementType = 0;
                if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On || optCreditCard.ToggleState== ToggleState.On)
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
                else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.CashAccountStatement;
                }


              
               
                //var list = (from a in query
                //            where a.DriverId == driverId && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (a.CompanyId==companyId || companyId==0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                //                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                //                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                //                                                 || (statementType == eStatementType.Both))
                //                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                //            orderby a.PickupDateTime descending
                //            select new
                //            {
                //                Id = a.Id,
                //                PickUpDate = a.PickupDateTime,
                //                RefNumber = a.BookingNo,
                //                Vehicle = a.Fleet_VehicleType.VehicleType,
                //                Account = a.Gen_Company.CompanyName,
                //                PickupPoint = a.FromAddress,
                //                Destination = a.ToAddress,
                //                Charge = a.FareRate,
                //                Parking = a.ParkingCharges,
                //                Waiting = a.WaitingCharges,
                //                ExtraDrop = a.ExtraDropCharges,
                //                MeetAndGreet = a.MeetAndGreetCharges,
                //                CongtionCharge = a.CongtionCharges,
                //                Total = a.TotalCharges,
                //                //   DriverCommission = a.IsCommissionWise == false ? appCommission : a.DriverCommission,
                //                DriverCommission =NoACCommission && a.CompanyId!=null?0:( a.IsCommissionWise == false ? a.Fleet_Driver.DriverCommissionPerBooking : a.DriverCommission),
                //                DriverCommissionType = a.DriverCommissionType
                //            }).ToList();



                TaxiDataContext db = new TaxiDataContext();
             


                var listA = (from a in db.Bookings
                             where a.DriverId == driverId && a.BookingStatusId==Enums.BOOKINGSTATUS.DISPATCHED && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (a.CompanyId==companyId || companyId==0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                                                                 || (statementType == eStatementType.Both))
                                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                            orderby a.PickupDateTime descending
                             select new
                             {
                                 Id = a.Id,
                                 PickUpDate = a.PickupDateTime,
                                 RefNumber = a.BookingNo,
                                 Vehicle = a.Fleet_VehicleType.VehicleType,
                                 Account = a.Gen_Company.CompanyName,
                                 PickupPoint = a.FromAddress,
                                 Via = a.ViaString,
                                 Destination = a.ToAddress,
                                 Charge = a.FareRate,
                                 Parking = a.CongtionCharges,
                                 Waiting = a.MeetAndGreetCharges,
                                 ExtraDrop = a.ExtraDropCharges,
                                 MeetAndGreet = a.MeetAndGreetCharges,
                                 CongtionCharge = a.CongtionCharges,
                                 Total = (a.FareRate + a.CongtionCharges+a.MeetAndGreetCharges+a.ExtraDropCharges),
                                 AgentFees = a.AgentCommissionPercent != null && a.AgentCommissionPercent > 0 ? (a.CashFares * a.AgentCommissionPercent) / 100 : a.AgentCommission,
    
                                 DriverCommission = NoACCommission && a.CompanyId != null ? 0 : (a.IsCommissionWise == false ? a.Fleet_Driver.DriverCommissionPerBooking : a.DriverCommission),
                                 DriverCommissionType = a.DriverCommissionType
                             }).ToList();



                var listB = (from a in db.Bookings.Where(c => c.SecondaryPaymentTypeId != null)
                             where a.DriverId == driverId && a.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && a.SecondaryPaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                                                                 || (statementType == eStatementType.Both))
                                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                            orderby a.PickupDateTime descending
                             select new
                             {
                                 Id = a.Id,
                                 PickUpDate = a.PickupDateTime,
                                 RefNumber = a.BookingNo,
                                 Vehicle = a.Fleet_VehicleType.VehicleType,
                                 Account = a.Gen_Company.CompanyName + "(CASH)",
                                 PickupPoint = a.FromAddress,
                                 Via = a.ViaString,
                                 Destination = a.ToAddress,
                                 Charge = a.CashFares,
                                 Parking = default(decimal?),
                                 Waiting = default(decimal?),
                                 ExtraDrop = default(decimal?),
                                 MeetAndGreet = default(decimal?),
                                 CongtionCharge = default(decimal?),
                                 Total = a.CashFares,
                                 AgentFees=a.AgentCommissionPercent!=null && a.AgentCommissionPercent > 0 ? (a.CashFares*a.AgentCommissionPercent)/100:a.AgentCommission,


                                 DriverCommission = NoACCommission && a.CompanyId != null ? 0 : (a.IsCommissionWise == false ? a.Fleet_Driver.DriverCommissionPerBooking : a.DriverCommission),
                                 DriverCommissionType = a.DriverCommissionType
                             }).ToList();

                var list = listB.Union(listA).ToList();


              
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
                  //  grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = list[i].MeetAndGreet.ToDecimal();
                //    grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = list[i].CongtionCharge.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.AgentFees].Value = list[i].AgentFees.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Total].Value = list[i].Total.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.DriverCommission].Value = list[i].DriverCommission.ToDecimal();


                    if (PickCommissionFromCharges)
                    {
                        grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Charge.ToDecimal() * list[i].DriverCommission.ToDecimal()) / 100
                                                                           : list[i].DriverCommission.ToDecimal();

                    }
                    else
                    {
                        grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Total.ToDecimal() * list[i].DriverCommission.ToDecimal()) / 100
                                                                            : list[i].DriverCommission.ToDecimal();
                    }
                }

                decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Commission].Value.ToDecimal());
                string total = totalEarning.ToStr();
                lblTotalEarning.Text = "Total Earning £ " +string.Format("{0:f2}",total);

                lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();

                lblTotalExtra.Text = "Total Extra Drop £ " + grdLister.Rows.Sum(c => c.Cells[COLS.ExtraDrop].Value.ToDecimal());


                decimal jobsTotal = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());
                lblJobsTotal.Text = "Jobs Total: £" + string.Format("{0:f2}", jobsTotal);

                db.Dispose();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            LoadGridReport();
        }


        private void LoadGridReport()
        {

            try
            {

                if (listOfCommRange == null)
                {


                    if (ChkCommission.Checked == false)
                    {
                        ViewReport();
                    }
                    else
                    {
                        ViewReportCommissionWise();
                    }
                }
                else
                {
                    ViewReportRangeWiseCommission();


                }
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
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            int? companyId = ddlCompany.SelectedValue.ToIntorNull();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

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



          //  {

                 UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "frmDriverCommissionReport" && c.IsDefault == true);

              //  Microsoft.Reporting.WinForms.ReportParameter[] param = null;
              //  string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";


                 if (objTemplate.TemplateName.ToStr() == "Template1" || objTemplate.TemplateName.ToStr() == "Template3" || objTemplate.TemplateName.ToStr() == "Template5")
                {

                    rptfrmDriverCommissionStatement frm = new rptfrmDriverCommissionStatement();
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
                    else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        statementType = eStatementType.CashAccountStatement;
                    }



                    frm.objSubCompany = General.GetObject<Gen_SubCompany>(c => c.Id == ddlSubCompany.SelectedValue.ToInt());

                     frm.TemplatePath="Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";
                    frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);

                    frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;

                    frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                    frm.StatementType = statementType;
                    frm.GenerateReport();
                    //frm.LoadDriverStatementReport(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull());






                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverCommissionStatement1");

                if (doc != null)
                {
                    doc.Close();
                }
                UI.MainMenuForm.MainMenuFrm.ShowForm(frm);

            }

                 else if (objTemplate.TemplateName.ToStr() == "Template2" )
                {
                    //   }
                    rptfrmDriverCommissionStatement2 frm = new rptfrmDriverCommissionStatement2();
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
                    else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        statementType = eStatementType.CashAccountStatement;
                    }
                    else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                    {
                        statementType = 5;
                    }

            


                 //   frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);
                    frm.PDARent = this.PDARent.ToDecimal();
                 
                    frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                    frm.StatementType = statementType;

                    frm.TempPath = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";
                    if (listOfCommRange == null)
                    {
                        frm.DataSource = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);

                        frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;
                        frm.LoadDriverStatementReport(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);
                    }
                    else
                    {
                        frm.DataSource = GetDataSource2ByCommRange(fromDate, tillDate, driverId, companyId, statementType);

                        frm.listofCommRange = this.listOfCommRange;
                        frm.LoadDriverStatementReportRangeWiseComm(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);

                    }


                    DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverCommissionStatement21");

                    if (doc != null)
                    {
                        doc.Close();
                    }
                    UI.MainMenuForm.MainMenuFrm.ShowForm(frm);

                }
                 else if (objTemplate.TemplateName.ToStr() == "Template4")
                 {
                    
                     rptfrmDriverCommissionStatement2 frm = new rptfrmDriverCommissionStatement2();
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
                     else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                     {
                         statementType = eStatementType.CashAccountStatement;
                     }
                     else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                     {
                         statementType = 5;
                     }



                     decimal pdaRent=General.GetObject<Fleet_Driver>(c=>c.Id==driverId).DefaultIfEmpty().PDARent.ToDecimal();
                     double totalWeeks = (dtpTillDate.Value.ToDate().Subtract(dtpFromDate.Value.ToDate()).TotalDays) / 7;
                


                     //   frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);
                     frm.PDARent = (pdaRent * totalWeeks.ToInt());

                     frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                     frm.StatementType = statementType;

                     frm.TempPath = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";
                     
                     frm.DataSource = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);

                     frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;
                     frm.LoadDriverStatementReportByTemplate4(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);
                    
               


                     DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverCommissionStatement21");

                     if (doc != null)
                     {
                         doc.Close();
                     }
                     UI.MainMenuForm.MainMenuFrm.ShowForm(frm);

                 }
        }


        private List<Vu_BookingBase> GetDataSource(int? driverId, int statementType, DateTime? fromDate, DateTime? tillDate,int? companyId)
        {
            return General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId
                                                && ((statementType == eStatementType.AccountStatement && c.CompanyId != null && (c.CompanyId == companyId || companyId == null) && c.PaymentTypeId != Enums.ACCOUNT_TYPE.CASH)
                                               || (statementType == eStatementType.CashAccountStatement && c.CompanyId != null && (c.CompanyId == companyId || companyId == null) && c.PaymentTypeId == Enums.ACCOUNT_TYPE.CASH)
                                               || (statementType == eStatementType.CashStatement && c.CompanyId == null)
                                               || (statementType == eStatementType.Both))

                                                        )
                    .Where(b => (b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate))
                        .OrderByDescending(c => c.PickupDateTime).ToList();

        }


        private List<stp_DriverCommisionResult> GetDataSource2(DateTime? fromDate, DateTime? tillDate, int? driverId, int? companyId, int? statementType)
        {
            return new TaxiDataContext().stp_DriverCommision(dtpFromDate.Value, dtpTillDate.Value, driverId, companyId, statementType,0).ToList();
            
            
            
        }


        private List<stp_DriverCommisionResult> GetDataSource2ByCommRange(DateTime? fromDate, DateTime? tillDate, int? driverId, int? companyId, int? statementType)
        {

            List<stp_DriverCommisionResult> list = (from a in new TaxiDataContext().stp_DriverCommision(dtpFromDate.Value, dtpTillDate.Value, driverId, companyId, statementType,0)
                                                    select new stp_DriverCommisionResult
                                                    {
                                                      
                                                            AccountTypeId=a.AccountTypeId,
                                                            AgentCommission = a.IsCommissionWise.ToBool() ? (a.DriverCommissionType == "Percent" ? ((a.TotalCharges.ToDecimal() * a.DriverCommission) / 100) : a.DriverCommission) : (((a.TotalCharges.ToDecimal() * listOfCommRange.FirstOrDefault(b => a.TotalCharges.ToDecimal() >= b.FromPrice && a.TotalCharges.ToDecimal() <= b.ToPrice).DefaultIfEmpty().CommissionValue.ToDecimal()) / 100)),
                                                            CompanyPrice = a.IsCommissionWise.ToBool() ? (a.DriverCommissionType == "Percent" ? ((a.FareRate.ToDecimal() * a.DriverCommission) / 100) : a.DriverCommission) : (((a.FareRate.ToDecimal() * listOfCommRange.FirstOrDefault(b => a.FareRate.ToDecimal() >= b.FromPrice && a.FareRate.ToDecimal() <= b.ToPrice).DefaultIfEmpty().CommissionValue.ToDecimal()) / 100)),
                        
                                                            AgentCommissionPercent=a.AgentCommissionPercent,
                                                            BookingDate=a.BookingDate,
                                                            BookingNo=a.BookingNo,
                                                            BookingStatusId=a.BookingStatusId,
                                                            BookingTypeId=a.BookingTypeId,
                                                 
                


                                                                  CompanyCode=a.CompanyCode,
                                                                   CompanyId=a.CompanyId,
                                                                    CompanyName=a.CompanyName,
                                                                     
                                                                      CongtionCharges=a.CongtionCharges,
                                                                     
                                                                         CustomerMobileNo=a.CustomerMobileNo,
                                                                          CustomerName=a.CustomerName,
                                                                           CustomerPhoneNo=a.CustomerPhoneNo,
                                                                            CustomerPrice=a.CustomerPrice,
                                                                            
                                                                               Despatchby=a.Despatchby,
                                                                                DriverAddress=a.DriverAddress,
                                                                                  
                                                                                DriverCommission=a.DriverCommission,
                                                                                  DriverCommissionType=a.DriverCommissionType,
                                                                                   DriverFullName=a.DriverFullName,
                                                                                    DriverId=a.DriverId,
                                                                                     DriverName=a.DriverName,
                                                                                      DriverNo=a.DriverNo,
                                                                                       ExtraDropCharges=a.ExtraDropCharges,
                                                                                        FareRate=a.FareRate,
                                                                                         FromAddress=a.FromAddress,
                                                                                          FromDoorNo=a.FromDoorNo,
                                                                                           FromLocType=a.FromLocType,
                                                                                            FromStreet=a.FromStreet,
                                                                                             Id=a.Id,
                                                                                              IsCommissionWise=a.IsCommissionWise,
                                                                                               JobTakenByCompany=a.JobTakenByCompany,
                                                                                                MeetAndGreetCharges=a.MeetAndGreetCharges,
                                                                                                 ParkingCharges=a.ParkingCharges,
                                                                                                  PaymentType=a.PaymentType,
                                                                                                   PaymentTypeId=a.PaymentTypeId,
                                                                                                    PickupDateTime=a.PickupDateTime,
                                                                                                     ReturnDriverFullName=a.ReturnDriverFullName,
                                                                                                      ReturnDriverId=a.ReturnDriverId,
                                                                                                       ReturnFareRate=a.ReturnFareRate,
                                                                                                        ReturnPickupDateTime=a.ReturnPickupDateTime,
                                                                                                         SpecialRequirements=a.SpecialRequirements,
                                                                                                          StatusName=a.StatusName,
                                                                                                           SubCompanyId=a.SubCompanyId,
                                                                                                            ToAddress=a.ToAddress,
                                                                                                             ToDoorNo=a.ToDoorNo,
                                                                                                              ToStreet=a.ToStreet,
                                                                                                               TotalCharges=a.TotalCharges,
                                                                                                                VehicleType=a.VehicleType,
                                                                                                                 VehicleTypeId=a.VehicleTypeId,
                                                                                                                  WaitingCharges=a.WaitingCharges,
                                                                                                                  Via1=a.Via1



                                                    }).ToList();

            return  list;


        }
          

        public struct eStatementType
        {
            public static int AccountStatement = 1;
            public static int CashStatement = 2;
            public static int Both = 3;
            public static int CashAccountStatement = 4;


        } ;

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            int? companyId = ddlCompany.SelectedValue.ToIntorNull();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

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
            
          
            
                 UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "frmDriverCommissionReport" && c.IsDefault == true);

              //  Microsoft.Reporting.WinForms.ReportParameter[] param = null;
              //  string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";
                 if (objTemplate.TemplateName.ToStr() == "Template1" || objTemplate.TemplateName.ToStr() == "Template3" || objTemplate.TemplateName.ToStr() == "Template5")
                 {
                     rptfrmDriverCommissionStatement frm = new rptfrmDriverCommissionStatement();
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
                     else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                     {
                         statementType = eStatementType.CashAccountStatement;
                     }
                     else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                     {
                         statementType = 5;
                     }


                     //var list = General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId).AsEnumerable()
                     //                .Where(b => (b.PickupDateTime.ToDate() >= fromDate && b.PickupDateTime.ToDate() <= tillDate))
                     //                    .OrderByDescending(c => c.PickupDateTime).ToList();

                     frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;

                     frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                     frm.TemplatePath = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";

                     frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);
                     frm.StatementType = statementType;
                     frm.GenerateReport();
                     //  frm.LoadDriverStatementReport(dtpFromDate.Value.ToDateTime(), dtpTillDate.Value.ToDateTime(), ddl_Driver.SelectedValue.ToInt(), ddlCompany.SelectedValue.ToInt());
                     frm.ExportReport();
                 }
                 else if (objTemplate.TemplateName.ToStr() == "Template2")
                 {
                     rptfrmDriverCommissionStatement2 frm = new rptfrmDriverCommissionStatement2();
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
                     else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                     {
                         statementType = eStatementType.CashAccountStatement;
                     }



                     frm.TempPath = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";

                     frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                     frm.StatementType = statementType;


                     if (listOfCommRange == null)
                     {
                         frm.DataSource = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);

                         frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;
                         frm.LoadDriverStatementReport(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);
                     }
                     else
                     {
                         frm.DataSource = GetDataSource2ByCommRange(fromDate, tillDate, driverId, companyId, statementType);

                         frm.listofCommRange = this.listOfCommRange;
                         frm.LoadDriverStatementReportRangeWiseComm(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);

                     }



                     // frm.GenerateReport();
                    // frm.LoadDriverStatementReport(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);
                     frm.ExportReport();
                 }
                 else if (objTemplate.TemplateName.ToStr() == "Template4")
                 {

                     rptfrmDriverCommissionStatement2 frm = new rptfrmDriverCommissionStatement2();
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
                     else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                     {
                         statementType = eStatementType.CashAccountStatement;
                     }
                     else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                     {
                         statementType = 5;
                     }




                     //   frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);
                     frm.PDARent = this.PDARent.ToDecimal();

                     frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                     frm.StatementType = statementType;

                     frm.TempPath = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";

                     frm.DataSource = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);

                     frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;
                     frm.LoadDriverStatementReportByTemplate4(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);



                     frm.ExportReport();

                 }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
             int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            int? companyId = ddlCompany.SelectedValue.ToIntorNull();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? tillDate = dtpTillDate.Value.ToDate();

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

                             UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "frmDriverCommissionReport" && c.IsDefault == true);

              //  Microsoft.Reporting.WinForms.ReportParameter[] param = null;
              //  string className = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_";
                             if (objTemplate.TemplateName.ToStr() == "Template1" || objTemplate.TemplateName.ToStr() == "Template3" || objTemplate.TemplateName.ToStr() == "Template5")
                             {
                                 rptfrmDriverCommissionStatement frm = new rptfrmDriverCommissionStatement();
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
                                 else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                                 {
                                     statementType = eStatementType.CashAccountStatement;
                                 }
                                 else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                                 {
                                     statementType = 5;
                                 }


                                 //var list = General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.DriverId == driverId).AsEnumerable()
                                 //                .Where(b => (b.PickupDateTime.ToDate() >= fromDate && b.PickupDateTime.ToDate() <= tillDate))
                                 //                    .OrderByDescending(c => c.PickupDateTime).ToList();
                                 frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;

                                 frm.TemplatePath = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";

                                 frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                                 frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId); ;
                                 frm.StatementType = statementType;
                                 frm.GenerateReport();
                                 frm.SendEmail();
                             }
                             else if (objTemplate.TemplateName.ToStr() == "Template2")
                             {
                                 rptfrmDriverCommissionStatement2 frm = new rptfrmDriverCommissionStatement2();
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
                                 else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                                 {
                                     statementType = eStatementType.CashAccountStatement;
                                 }


                                 frm.TempPath = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";


                                 frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                                 frm.StatementType = statementType;


                                 if (listOfCommRange == null)
                                 {
                                     frm.DataSource = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);

                                     frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;
                                     frm.LoadDriverStatementReport(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);
                                 }
                                 else
                                 {
                                     frm.DataSource = GetDataSource2ByCommRange(fromDate, tillDate, driverId, companyId, statementType);

                                     frm.listofCommRange = this.listOfCommRange;
                                     frm.LoadDriverStatementReportRangeWiseComm(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);

                                 }


                                 
                                 // frm.GenerateReport();
                                 //frm.LoadDriverStatementReport(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);
                                // frm.ExportReport();
                                 frm.SendEmail();
                             }
                             else if (objTemplate.TemplateName.ToStr() == "Template4")
                             {

                                 rptfrmDriverCommissionStatement2 frm = new rptfrmDriverCommissionStatement2();
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
                                 else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                                 {
                                     statementType = eStatementType.CashAccountStatement;
                                 }
                                 else if (optCreditCard.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                                 {
                                     statementType = 5;
                                 }

                                 //   frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);
                                 frm.PDARent = this.PDARent.ToDecimal();

                                 frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                                 frm.StatementType = statementType;

                                 frm.TempPath = "Taxi_AppMain.ReportDesigns." + objTemplate.TemplateName.ToStr() + "_rptDriverCommissionStatement.rdlc";

                                 frm.DataSource = GetDataSource2(fromDate, tillDate, driverId, companyId, statementType);

                                 frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;
                                 frm.LoadDriverStatementReportByTemplate4(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull(), statementType);


                                 frm.SendEmail();

                             }

        }

        private void frmDriverCommissionReport_Load(object sender, EventArgs e)
        {

        }

        private void ChkCommission_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            try
            {
                IsCommissionWise(args.ToggleState);
            }
            catch (Exception ex)
            {
            }
        }
        private void IsCommissionWise(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                txtCommission.Enabled = true;
            }
            else
            {
                txtCommission.Enabled = false;
            }
        }
        private void ViewReportCommissionWise()
        {
            try
            {
                UM_Form_Template objTemplate = General.GetObject<UM_Form_Template>(c => c.UM_Form.FormName == "frmDriverCommissionReport" && c.IsDefault == true);


                string templateValue = objTemplate.DefaultIfEmpty().TemplateName.ToStr().ToLower();


                int driverId = ddl_Driver.SelectedValue.ToInt();
                int companyId = ddlCompany.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();
                bool PickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool();


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

                lblCriteria.Text = "Driver Report Related to '" + ddl_Driver.Text.ToStr()
                    + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);


                int statementType = 0;
                if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On || optCreditCard.ToggleState == ToggleState.On)
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
                else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.CashAccountStatement;
                }


              //  var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);
                //var list = (from a in query
                //            where a.DriverId == driverId && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                //                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                //                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                //                                                 || (statementType == eStatementType.Both))
                //                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                //            orderby a.PickupDateTime descending
                //            select new
                //            {
                //                Id = a.Id,
                //                PickUpDate = a.PickupDateTime,
                //                RefNumber = a.BookingNo,
                //                Vehicle = a.Fleet_VehicleType.VehicleType,
                //                Account = a.Gen_Company.CompanyName,
                //                PickupPoint = a.FromAddress,
                //                Destination = a.ToAddress,
                //                Charge = a.FareRate,
                //                Parking = a.ParkingCharges,
                //                Waiting = a.WaitingCharges,
                //                ExtraDrop = a.ExtraDropCharges,
                //                MeetAndGreet = a.MeetAndGreetCharges,
                //                CongtionCharge = a.CongtionCharges,
                //                Total = a.TotalCharges,

                //                DriverCommission = NoACCommission && a.CompanyId != null ? 0 : (a.IsCommissionWise == false ? a.Fleet_Driver.DriverCommissionPerBooking : a.DriverCommission),
                //                DriverCommissionType = a.DriverCommissionType
                //            }).ToList();


               

                TaxiDataContext db = new TaxiDataContext();
             //   var list1 = db.Bookings.Where(c =>c.DriverId==driverId && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);
          //      var list2 = db.Bookings.Where(c =>c.DriverId==driverId && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.SecondaryPaymentTypeId != null);
              //  var   query = list1.Concat(list2);


                var listA = (from a in db.Bookings
                            where a.DriverId == driverId && a.BookingStatusId==Enums.BOOKINGSTATUS.DISPATCHED && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                                                                 || (statementType == eStatementType.Both))
                                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                            orderby a.PickupDateTime descending
                            select new
                            {
                                Id = a.Id,
                                PickUpDate = a.PickupDateTime,
                                RefNumber = a.BookingNo,
                                Vehicle = a.Fleet_VehicleType.VehicleType,
                                Account = a.Gen_Company.CompanyName,
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
                                AgentFees = a.AgentCommissionPercent != null && a.AgentCommissionPercent > 0 ? (a.CashFares * a.AgentCommissionPercent) / 100 : a.AgentCommission,

                                DriverCommission = NoACCommission && a.CompanyId != null ? 0 : ( a.IsCommissionWise == false ? a.Fleet_Driver.DriverCommissionPerBooking : a.DriverCommission),
                                DriverCommissionType = a.DriverCommissionType
                            }).ToList();



                var listB = (from a in db.Bookings.Where(c=>c.SecondaryPaymentTypeId!=null)
                             where a.DriverId == driverId && a.BookingStatusId==Enums.BOOKINGSTATUS.DISPATCHED && ((statementType == eStatementType.AccountStatement && a.SecondaryPaymentTypeId == Enums.PAYMENT_TYPES.BANK_ACCOUNT && a.CompanyId != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                                                               || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                                                           || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                                                                  || (statementType == eStatementType.Both))
                                 && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                             orderby a.PickupDateTime descending
                             select new
                             {
                                 Id = a.Id,
                                 PickUpDate = a.PickupDateTime,
                                 RefNumber = a.BookingNo,
                                 Vehicle = a.Fleet_VehicleType.VehicleType,
                                 Account = a.Gen_Company.CompanyName + "(CASH)",
                                 PickupPoint = a.FromAddress,
                                 Via = a.ViaString,
                                 Destination = a.ToAddress,
                                 Charge = a.CashFares,
                                 Parking = default(decimal?),
                                 Waiting = default(decimal?),
                                 ExtraDrop = default(decimal?),
                                 MeetAndGreet = default(decimal?),
                                 CongtionCharge = default(decimal?),
                                 Total = a.CashFares,
                                 AgentFees = a.AgentCommissionPercent != null && a.AgentCommissionPercent > 0 ? (a.CashFares * a.AgentCommissionPercent) / 100 : a.AgentCommission,

                                 DriverCommission = NoACCommission && a.CompanyId != null ? 0 : (a.IsCommissionWise == false ? a.Fleet_Driver.DriverCommissionPerBooking : a.DriverCommission),
                                 DriverCommissionType = a.DriverCommissionType
                             }).ToList();

                var list = listB.Union(listA).ToList();
                          
                
                
                
                
                            

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
                   // grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = list[i].MeetAndGreet.ToDecimal();
                  //  grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = list[i].CongtionCharge.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.AgentFees].Value = list[i].AgentFees.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Total].Value = list[i].Total.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.DriverCommission].Value = list[i].DriverCommission.ToDecimal();


                    decimal totalCharges = grdLister.Rows[i].Cells[COLS.Total].Value.ToDecimal();


                    if (NoACCommission==false || (NoACCommission==true && string.IsNullOrEmpty( grdLister.Rows[i].Cells[COLS.Account].Value.ToStr().Trim())) )
                    {


                        int Comm = txtCommission.Value.ToInt();
                        //  grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Total.ToDecimal() * Comm) / 100 : list[i].DriverCommission.ToDecimal();




                        if (PickCommissionFromCharges)
                        {
                            grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Charge.ToDecimal() * Comm) / 100 : list[i].DriverCommission.ToDecimal();

                            //   grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Charge.ToDecimal() * list[i].DriverCommission.ToDecimal()) / 100
                            //                                                    : list[i].DriverCommission.ToDecimal();

                        }
                        else
                        {
                             if(templateValue.Contains("template4"))
                             {
                                 grdLister.Rows[i].Cells[COLS.Commission].Value =  (totalCharges * Comm) / 100 ;

                             }
                             else
                             {


                            grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (totalCharges * Comm) / 100 : list[i].DriverCommission.ToDecimal();
                             }

                            //grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommissionType == "Percent" ? (list[i].Total.ToDecimal() * list[i].DriverCommission.ToDecimal()) / 100
                            //                                                    : list[i].DriverCommission.ToDecimal();
                        }

                    }
                }

                decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Commission].Value.ToDecimal());
                string total = totalEarning.ToStr();
                lblTotalEarning.Text = "Total Earning £ " +string.Format("{0:f2}",total);

                lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();

                lblTotalExtra.Text = "Total Extra Drop £ " + grdLister.Rows.Sum(c => c.Cells[COLS.ExtraDrop].Value.ToDecimal());


                decimal jobsTotal = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());
                lblJobsTotal.Text = "Jobs Total: £" + string.Format("{0:f2}", jobsTotal);

                db.Dispose();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }

        private void frmDriverCommissionReport_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && ddl_Driver.SelectedValue.ToInt() != 0)
                {
                    if (ChkCommission.Checked == false)
                    {
                        ViewReport();
                    }
                    else
                    {
                        ViewReportCommissionWise();
                    }


                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowErrorMessage(ex.Message);
            }
        }

        private void optAccount_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (optAccount.ToggleState == ToggleState.On || optCashACStatement.ToggleState == ToggleState.On || optCreditCard.ToggleState== ToggleState.On)
                SetCustomAccount(ToggleState.On);
            else
                SetCustomAccount(ToggleState.Off);

        }


        private void SetCustomAccount(ToggleState toggle)
        {
            try
            {
                if (toggle == ToggleState.On)
                {

                    ddlCompany.Enabled = true;
                    chkAll.Visible = true;
                    if (ddlCompany.DataSource == null)
                    {
                        ComboFunctions.FillCompanyCombo(ddlCompany);

                    }

                    ddlCompany.SelectedValue = null;
                }
                else
                {
                    ddlCompany.Enabled = false;
                    ddlCompany.SelectedValue = null;
                    chkAll.Visible = false;
                }
            }
            catch (Exception ex)
            {


            }


        }

        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
                ddlCompany.SelectedValue = null;
        }

        private void optCreditCard_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {



            if (args.ToggleState == ToggleState.On)
            {
                chkAll.Checked = false;


                if (ddlCompany.DataSource == null)
                {
                    ComboFunctions.FillCompanyCombo(ddlCompany);

                }

                RadListDataItem item = ddlCompany.Items.FirstOrDefault(c => c.Text.ToLower() == "credit card" || c.Text.ToLower() == "creditcard");

                if (item != null)
                {
                    ddlCompany.SelectedValue = item.Value;

                }
                else
                {

                    ddlCompany.SelectedValue = null;
                }
            }
            else
            {
                ddlCompany.SelectedValue = null;

            }

        }

        //private void btnReport1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //         int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
        //    int? companyId = ddlCompany.SelectedValue.ToIntorNull();
        //    DateTime? fromDate = dtpFromDate.Value.ToDate();
        //    DateTime? tillDate = dtpTillDate.Value.ToDate();

        //    string error = string.Empty;

        //    if (driverId == null)
        //    {

        //        error += "Required : Driver";
        //    }

        //    if (fromDate == null)
        //    {
        //        if (string.IsNullOrEmpty(error))
        //            error += Environment.NewLine;

        //        error += "Required : From Date";
        //    }

        //    if (tillDate == null)
        //    {
        //        if (string.IsNullOrEmpty(error))
        //            error += Environment.NewLine;

        //        error += "Required : To Date";


        //    }

        //    if (!string.IsNullOrEmpty(error))
        //    {
        //        ENUtils.ShowMessage(error);
        //        return;

        //    }





        //        rptfrmDriverCommissionStatement frm = new rptfrmDriverCommissionStatement();
        //        int statementType = 0;
        //        if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        //        {
        //            statementType = eStatementType.AccountStatement;

        //        }
        //        else if (optCash.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        //        {
        //            statementType = eStatementType.CashStatement;

        //        }
        //        else if (optBoth.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        //        {
        //            statementType = eStatementType.Both;
        //        }
        //        else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
        //        {
        //            statementType = eStatementType.CashAccountStatement;
        //        }



        //        frm.DataSource = GetDataSource(driverId, statementType, fromDate, tillDate, companyId);

        //        frm.Commision = ChkCommission.Checked == true ? txtCommission.Value.ToInt() : 0;

        //        frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
        //        frm.StatementType = statementType;
        //        frm.GenerateReport();
        //        //frm.LoadDriverStatementReport(fromDate.Value.ToDate(), tillDate.Value.ToDate(), driverId.ToIntorNull(), companyId.ToIntorNull());






        //        DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverCommissionStatement1");

        //        if (doc != null)
        //        {
        //            doc.Close();
        //        }
        //        UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
        //    }
        //    catch (Exception ex)
        //    { 
            
        //    }
        //}



        private void ViewReportRangeWiseCommission()
        {
            try
            {
                int driverId = ddl_Driver.SelectedValue.ToInt();
                int companyId = ddlCompany.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();
                bool PickCommissionFromCharges = AppVars.objPolicyConfiguration.PickCommissionFromCharges.ToBool();


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

                lblCriteria.Text = "Driver Report Related to '" + ddl_Driver.Text.ToStr()
                    + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);


                int statementType = 0;
                if (optAccount.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On || optCreditCard.ToggleState == ToggleState.On)
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
                else if (optCashACStatement.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {
                    statementType = eStatementType.CashAccountStatement;
                }

                  //old
                // var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);
                  //

               

               
                //var list = (from a in query
                //            where a.DriverId == driverId && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                //                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                //                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                //                                                 || (statementType == eStatementType.Both))
                //                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                //            orderby a.PickupDateTime descending
                //            select new
                //            {
                //                Id = a.Id,
                //                PickUpDate = a.PickupDateTime,
                //                RefNumber = a.BookingNo,
                //                Vehicle = a.Fleet_VehicleType.VehicleType,
                //                Account = a.Gen_Company.CompanyName,
                //                PickupPoint = a.FromAddress,
                //                Destination = a.ToAddress,
                //                Charge = a.FareRate,
                //                Parking = a.ParkingCharges,
                //                Waiting = a.WaitingCharges,
                //                ExtraDrop = a.ExtraDropCharges,
                //                MeetAndGreet = a.MeetAndGreetCharges,
                //                CongtionCharge = a.CongtionCharges,
                //                Total = a.TotalCharges,
                //                CompanyId=a.CompanyId,
                //                DriverBookingCommission = a.DriverCommission,
                //                DriverCommissionType = a.DriverCommissionType,
                //                IsCommissionWise=   a.IsCommissionWise
                //            }).ToList();


                 TaxiDataContext db = new TaxiDataContext();

                 var listA = (from a in db.Bookings
                             where a.DriverId == driverId && a.BookingStatusId==Enums.BOOKINGSTATUS.DISPATCHED && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                                                                 || (statementType == eStatementType.Both))
                                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                              orderby a.PickupDateTime descending
                              select new
                              {
                                  Id = a.Id,
                                  PickUpDate = a.PickupDateTime,
                                  RefNumber = a.BookingNo,
                                  Vehicle = a.Fleet_VehicleType.VehicleType,
                                  Account = a.Gen_Company.CompanyName,
                                  PickupPoint = a.FromAddress,
                                  Via=a.ViaString,
                                  Destination = a.ToAddress,
                                  Charge = a.FareRate,
                                  Parking = a.CongtionCharges,
                                  Waiting = a.MeetAndGreetCharges,
                                  ExtraDrop = a.ExtraDropCharges,
                                  MeetAndGreet = a.MeetAndGreetCharges,
                                  CongtionCharge = a.CongtionCharges,
                                  Total = a.TotalCharges,
                                  CompanyId = a.CompanyId,
                                  DriverBookingCommission = a.DriverCommission,
                                  DriverCommissionType = a.DriverCommissionType,
                                  IsCommissionWise = a.IsCommissionWise
                              }).ToList();



                 var listB = (from a in db.Bookings.Where(c => c.SecondaryPaymentTypeId != null)
                              where a.DriverId == driverId && a.BookingStatusId==Enums.BOOKINGSTATUS.DISPATCHED && a.SecondaryPaymentTypeId==Enums.PAYMENT_TYPES.BANK_ACCOUNT && ((statementType == eStatementType.AccountStatement && a.CompanyId != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.ACCOUNT)
                                                              || (statementType == eStatementType.CashAccountStatement && a.Gen_Company != null && (a.CompanyId == companyId || companyId == 0) && a.Gen_Company.AccountTypeId == Enums.ACCOUNT_TYPE.CASH)
                                                          || (statementType == eStatementType.CashStatement && a.Gen_Company == null)
                                                                 || (statementType == eStatementType.Both))
                                && (a.PickupDateTime.Value.Date >= fromDate && a.PickupDateTime.Value.Date <= tillDate)
                              orderby a.PickupDateTime descending
                              select new
                              {
                                  Id = a.Id,
                                  PickUpDate = a.PickupDateTime,
                                  RefNumber = a.BookingNo,
                                  Vehicle = a.Fleet_VehicleType.VehicleType,
                                  Account = a.Gen_Company.CompanyName,
                                  PickupPoint = a.FromAddress,
                                  Via = a.ViaString,
                                  Destination = a.ToAddress,
                                  Charge = a.CashFares,
                                  Parking =default(decimal?),
                                  Waiting = default(decimal?),
                                  ExtraDrop = default(decimal?),
                                  MeetAndGreet = default(decimal?),
                                  CongtionCharge = default(decimal?),
                                  Total = a.CashFares,
                                  CompanyId = a.CompanyId,
                                  DriverBookingCommission = a.DriverCommission,
                                  DriverCommissionType = a.DriverCommissionType,
                                  IsCommissionWise = a.IsCommissionWise
                              }).ToList();

                 var list = listB.Union(listA).ToList();

                // grdLister.DataSource = list;
                grdLister.RowCount = list.Count;

                Fleet_Driver_CommissionRange range = null;
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


                    grdLister.Rows[i].Cells[COLS.AgentFees].Value = 0.00m;


                //    grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = list[i].MeetAndGreet.ToDecimal();
                //    grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = list[i].CongtionCharge.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Total].Value = list[i].Total.ToDecimal();


                    if (NoACCommission && list[i].CompanyId != null)
                    {

                        grdLister.Rows[i].Cells[COLS.DriverCommission].Value = 0;
                    }
                    else
                    {
                       range=  listOfCommRange.FirstOrDefault(c => list[i].Total.ToDecimal() >= c.FromPrice && list[i].Total.ToDecimal() <= c.ToPrice);


                       if (range != null)
                       {
                           grdLister.Rows[i].Cells[COLS.DriverCommission].Value = range.CommissionValue.ToDecimal();
                       }
                    }




                    if (NoACCommission == false || (NoACCommission == true && string.IsNullOrEmpty(grdLister.Rows[i].Cells[COLS.Account].Value.ToStr().Trim())))
                    {
                        decimal Comm = range != null ? range.CommissionValue.ToDecimal() : 0;

                        if (list[i].IsCommissionWise.ToBool())
                        {
                            Comm = list[i].DriverBookingCommission.ToInt();
                        }
                      
                        if (PickCommissionFromCharges)
                        {

                            if (list[i].DriverCommissionType == "Percent")
                            {
                                grdLister.Rows[i].Cells[COLS.Commission].Value = (list[i].Charge.ToDecimal() * Comm) / 100;
                            }
                            else
                            {
                                grdLister.Rows[i].Cells[COLS.Commission].Value = Comm;                          

                            }                       
                        }
                        else
                        {

                            if (list[i].DriverCommissionType == "Percent")
                            {
                                grdLister.Rows[i].Cells[COLS.Commission].Value = (list[i].Total.ToDecimal() * Comm) / 100;
                            }
                            else
                            {
                                grdLister.Rows[i].Cells[COLS.Commission].Value = Comm;

                            }                        

                        }

                    }
                }

                decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Commission].Value.ToDecimal());
                string total = totalEarning.ToStr();
                lblTotalEarning.Text = "Total Earning £ " +string.Format("{0:f2}",total);

                lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();

                lblTotalExtra.Text = "Total Extra Drop £ " + grdLister.Rows.Sum(c => c.Cells[COLS.ExtraDrop].Value.ToDecimal());


                decimal jobsTotal = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());
                lblJobsTotal.Text = "Jobs Total: £" + string.Format("{0:f2}", jobsTotal);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }
    }


}
