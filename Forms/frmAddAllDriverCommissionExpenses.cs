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
using UI;

namespace Taxi_AppMain
{
    public partial class frmAddAllDriverCommissionExpenses : UI.SetupBase
    {
        DriverCommisionBO objMaster = null;
        ConditionalFormattingObject objCondition = new ConditionalFormattingObject();
        ConditionalFormattingObject objConditionGeneratedTrans = new ConditionalFormattingObject();
        public struct COLS
        {
            public static string Email = "Email";
            public static string Id = "Id";
            public static string DriverNo = "DriverNo";

            public static string DriverCommission = "DriverCommission";
            public static string DriverCommissionPerBooking = "DriverCommissionPerBooking";
            public static string DriverPDARent = "DriverPDARent";

            public static string CommissionPay = "CommissionPay";
        
            public static string OldBalance = "OldBalance";
            public static string InitialBalance = "InitialBalance";

            public static string CurrBalance = "CurrBalance";
            public static string TotalPDARent = "TotalPDARent";
            public static string BookingFees = "BookingFees";
            public static string AccountsTotal = "AccountsTotal";
            public static string AccountExpense = "AccountExpense";
            public static string Owed = "Owed";
            public static string CommissionId = "CommissionId";
            public static string CashTotal = "CashTotal";
            public static string JobsTotal = "JobsTotal";
            public static string AgentFees = "AgentFees";
            public static string DriverEmail = "DriverEmail";
            public static string Hokiday = "Hokiday";
        }



        public frmAddAllDriverCommissionExpenses()
        {

            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(frmAddMultipleCompanyInvoice_FormClosed);
            this.KeyDown += new KeyEventHandler(frmAddAllDriverRent_KeyDown);

            grdDriverCommission.CellValueChanged += new GridViewCellEventHandler(grdDriverCommission_CellValueChanged);
        }

        void grdDriverCommission_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Column != null && e.Column is GridViewCheckBoxColumn && e.Column.Name == COLS.Hokiday)
            {
                if (e.Value.ToBool())
                {
                    e.Row.Cells[COLS.DriverPDARent].Tag = e.Row.Cells[COLS.DriverPDARent].Value;
                    e.Row.Cells[COLS.DriverPDARent].Value = 0.00m;

                    e.Row.Cells[COLS.TotalPDARent].Tag = e.Row.Cells[COLS.TotalPDARent].Value;
                    e.Row.Cells[COLS.TotalPDARent].Value = 0.00m;

                    //e.Row.Cells[COLS.CollectionAndDelivery].Tag = e.Row.Cells[COLS.CollectionAndDelivery].Value;
                    //e.Row.Cells[COLS.CollectionAndDelivery].Value = 0.00m;

                    //e.Row.Cells[COLS.TotalCollectionAndDelivery].Tag = e.Row.Cells[COLS.TotalCollectionAndDelivery].Value;
                    //e.Row.Cells[COLS.TotalCollectionAndDelivery].Value = 0.00m;
                }
                else
                {
                    e.Row.Cells[COLS.DriverPDARent].Value = e.Row.Cells[COLS.DriverPDARent].Tag.ToDecimal();
                    e.Row.Cells[COLS.TotalPDARent].Value = e.Row.Cells[COLS.TotalPDARent].Tag.ToDecimal();

                    //e.Row.Cells[COLS.CollectionAndDelivery].Value = e.Row.Cells[COLS.CollectionAndDelivery].Tag.ToDecimal();
                    //e.Row.Cells[COLS.TotalCollectionAndDelivery].Value = e.Row.Cells[COLS.TotalCollectionAndDelivery].Tag.ToDecimal();


                }

            }

        }

        


        void frmAddAllDriverRent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        

        void frmAddMultipleCompanyInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
            GC.Collect();
        }

        private void frmCompanyInvoice_Load(object sender, EventArgs e)
        {
            DateTime? fromDate = AppVars.objPolicyConfiguration.RentFromDateTime.ToDateTimeorNull();
            DateTime? toDate = AppVars.objPolicyConfiguration.RentToDateTime.ToDateTimeorNull();


            if (fromDate == null)
                fromDate = DateTime.Now.AddDays(-7);

            if (toDate == null)
                toDate = DateTime.Now.AddDays(6);

            int subtracted = 7;

            if (fromDate != null)
                subtracted = 7 - (int)fromDate.Value.DayOfWeek;

            int DaysToSubtract = (int)DateTime.Now.DayOfWeek;
            DateTime dtFrom = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
            if (subtracted == 7)
            { 
                subtracted=6;
            }

            fromDate = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.AddDays( -subtracted).Day, fromDate.Value.Hour, fromDate.Value.Minute, 0, 0);

            DateTime dtTo = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
            toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dtTo.AddDays(toDate.Value.DayOfWeek.ToInt()).Day, toDate.Value.Hour, toDate.Value.Minute, 59, 999);

            dtpFromDate.Value = fromDate;
            dtpTillDate.Value = toDate;

            GetDrivers();

            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);

            if (ddlSubCompany.Items.Count > 1)
                ddlSubCompany.SelectedIndex = 1;
            else
                ddlSubCompany.SelectedIndex = 0;
        }
        public void GetDrivers()
        {
            try
            {
                objMaster = new DriverCommisionBO();
                //DateTime OneMonthBefore = DateTime.Now;
                //OneMonthBefore = OneMonthBefore.A(-1);
               
                


                GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                col.Width = 40;
                col.AutoSizeMode = BestFitColumnMode.None;
                col.HeaderText = "";
                col.Name = "Check";
               
              //  col.ReadOnly = true;
                grdDriverCommission.Columns.Add(col);
                GridViewTextBoxColumn tbcol = new GridViewTextBoxColumn();
                tbcol.Name = COLS.Id;
                tbcol.IsVisible = false;
                grdDriverCommission.Columns.Add(tbcol);


                tbcol = new GridViewTextBoxColumn();
                tbcol.Name = COLS.CommissionId;
                tbcol.IsVisible = false;
                grdDriverCommission.Columns.Add(tbcol);


                tbcol = new GridViewTextBoxColumn();
                tbcol.Name = COLS.DriverNo;
                tbcol.HeaderText = "Driver";
                tbcol.Width = 160;
                tbcol.ReadOnly = true;
                grdDriverCommission.Columns.Add(tbcol);

                tbcol = new GridViewTextBoxColumn();
                tbcol.Name = COLS.DriverEmail;
                tbcol.IsVisible = false;
                grdDriverCommission.Columns.Add(tbcol);


                col = new GridViewCheckBoxColumn();
                col.Width = 60;
                col.AutoSizeMode = BestFitColumnMode.None;
                col.HeaderText = "Holiday";
                col.Name = COLS.Hokiday;
                grdDriverCommission.Columns.Add(col);

                //tbcol = new GridViewTextBoxColumn();
                //tbcol.Name = COLS.TransNo;
                //tbcol.HeaderText = "Transaction No";
                //tbcol.Width = 110;
                //tbcol.ReadOnly = true;
                //grdDriverRent.Columns.Add(tbcol);
                //tbcol = new GridViewTextBoxColumn();
                GridViewDecimalColumn dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.DriverCommission;
                dcol.Width = 70;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                dcol.HeaderText = "Comm:";//ission";
                grdDriverCommission.Columns.Add(dcol);


                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.DriverPDARent;
                dcol.Width = 70;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                dcol.HeaderText = "PDA Rent";//ission";
                
                grdDriverCommission.Columns.Add(dcol);
                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.TotalPDARent;
                dcol.HeaderText = "Total PDA Rent";
                dcol.Width = 110;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverCommission.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.BookingFees;
                dcol.HeaderText = "Booking Fees";
                dcol.Width = 110;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverCommission.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.OldBalance;
                dcol.Width = 100;
                dcol.DecimalPlaces = 2;
                dcol.HeaderText = "Old Balance";
                dcol.ReadOnly = true;
                grdDriverCommission.Columns.Add(dcol);

                //tbcol = new GridViewTextBoxColumn();
                //GridViewDecimalColumn 

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.InitialBalance;
                dcol.HeaderText = "Initial Balance";
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = false;
                grdDriverCommission.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.DriverCommissionPerBooking;
               // dcol.HeaderText = "Initial Balance";
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = false;
                grdDriverCommission.Columns.Add(dcol);
                

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.AccountsTotal;
                dcol.HeaderText = "A/C Total";
                dcol.Width = 90;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverCommission.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.CashTotal;
                dcol.HeaderText = "Cash Total";
                dcol.Width = 90;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverCommission.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.JobsTotal;
                dcol.HeaderText = "Jobs Total";
                dcol.Width = 90;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverCommission.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.AgentFees;
                dcol.HeaderText = "Agent Fees";
                dcol.Width = 90;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                
                grdDriverCommission.Columns.Add(dcol);
                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.AccountExpense;
                dcol.HeaderText = "Account Expense";
                dcol.Width = 120;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverCommission.Columns.Add(dcol);
 
                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.CommissionPay;
                dcol.HeaderText = "Comm: Pay";
                dcol.Width = 90;
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = false;
                dcol.ReadOnly = false;
                grdDriverCommission.Columns.Add(dcol);

               
                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.Owed;
                dcol.HeaderText = "Owed";
                dcol.Width = 90;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverCommission.Columns.Add(dcol);



                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.CurrBalance;
                dcol.HeaderText = "Current Balance";
                dcol.Width = 110;
                dcol.DecimalPlaces = 2;
                dcol.ReadOnly = true;
                grdDriverCommission.Columns.Add(dcol);



                
                grdDriverCommission.ShowRowHeaderColumn = false;


                objCondition.CellForeColor = Color.Red;
                objCondition.TValue1 = "x";
                objCondition.ConditionType = ConditionTypes.NotEqual;
                grdDriverCommission.Columns[COLS.CurrBalance].ConditionalFormattingObjectList.Add(objCondition);



                objConditionGeneratedTrans.RowBackColor = Color.LightGreen;
                objConditionGeneratedTrans.TValue1 = "0";
                objConditionGeneratedTrans.ApplyToRow = true;
                objConditionGeneratedTrans.ConditionType = ConditionTypes.Greater;
                grdDriverCommission.Columns[COLS.CommissionId].ConditionalFormattingObjectList.Add(objConditionGeneratedTrans);


                RefreshCommissionView();

            

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);  
            }
          
        }


        private void RefreshCommissionView()
        {
            try
            {

                DateTime? dtFrom = dtpFromDate.Value.ToDate();
                DateTime? dtTill = dtpTillDate.Value.ToDateTimeorNull();


                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_AddAllDriverCommission(dtFrom, dtTill).ToList();


                    int cnt = list.Count;
                    grdDriverCommission.RowCount = cnt;




                    for (int i = 0; i < cnt; i++)
                    {


                        grdDriverCommission.Rows[i].Cells["Check"].Value = true;
                        grdDriverCommission.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdDriverCommission.Rows[i].Cells[COLS.DriverNo].Value = list[i].Driver;
                        grdDriverCommission.Rows[i].Cells[COLS.DriverEmail].Value = list[i].Email;

                       // grdDriverCommission.Rows[i].Cells[COLS.DriverCommission].Value = list[i].DriverMonthlyRent.ToDecimal();


                        // ADD PDA RENT
                        grdDriverCommission.Rows[i].Cells[COLS.DriverPDARent].Value = list[i].PDARent.ToDecimal();

                        if (list[i].CommissionId == 0)
                        {
                            grdDriverCommission.Rows[i].Cells[COLS.OldBalance].Value = list[i].InitialBalance;
                        }
                        else
                        {
                            grdDriverCommission.Rows[i].Cells[COLS.OldBalance].Value = list[i].OldBalance;
                        }
                        grdDriverCommission.Rows[i].Cells[COLS.InitialBalance].Value = list[i].InitialBalance;
                        grdDriverCommission.Rows[i].Cells[COLS.DriverCommissionPerBooking].Value = list[i].DriverCommissionPerBooking;
                        grdDriverCommission.Rows[i].Cells[COLS.CashTotal].Value = null;
                        grdDriverCommission.Rows[i].Cells[COLS.JobsTotal].Value = null;
                        grdDriverCommission.Rows[i].Cells[COLS.AgentFees].Value = null;
                        grdDriverCommission.Rows[i].Cells[COLS.CommissionId].Value = null;
                        grdDriverCommission.Rows[i].Cells[COLS.CommissionPay].Value = null;
                        grdDriverCommission.Rows[i].Cells[COLS.AccountExpense].Value = null;
                        grdDriverCommission.Rows[i].Cells[COLS.Owed].Value = null;
                        grdDriverCommission.Rows[i].Cells[COLS.CurrBalance].Value = (list[i].PDARent.ToDecimal() + grdDriverCommission.Rows[i].Cells[COLS.OldBalance].Value.ToDecimal());
                        grdDriverCommission.Rows[i].Cells[COLS.DriverCommission].Value = null;
                        grdDriverCommission.Rows[i].Cells[COLS.AccountsTotal].Value = null;


                    }
                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
              

        }

       



        private void btnGenerate_Click(object sender, EventArgs e)
        {
            DisplayCommission();
            Generate();
        }
        private void Generate()
        {
            try
            {

                listofDrvDebitCreditNotes = General.GetQueryable<Fleet_Driver_DebitCreditNote>(c => c.DriverId != null).ToList();

                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                List<Fleet_DriverCommision> objCommissionList = null;






                long transId = 0;
                decimal accJobsTotal = 0;
                decimal rentDue = 0;
                decimal currBalance = 0;

                bool IsSavedTrans = false;

                foreach (var row in grdDriverCommission.Rows.Where(c=>c.Cells["Check"].Value.ToBool()))
	            {
                    transId = 0;
                    accJobsTotal = 0;
                    rentDue = 0;
                    currBalance = 0;

                    if (objCommissionList == null)
                    {
                        objCommissionList = General.GetQueryable<Fleet_DriverCommision>(c => c.FromDate == fromDate && c.ToDate == tillDate).ToList();

                    }

                    transId = objCommissionList.FirstOrDefault(c => c.DriverId == row.Cells["Id"].Value.ToInt()).DefaultIfEmpty().Id;

                   

                    if (OnSave(row.Cells["Id"].Value.ToInt(), row.Cells[COLS.OldBalance].Value.ToDecimal(), row.Cells[COLS.DriverCommissionPerBooking].Value.ToDecimal(), row.Cells[COLS.DriverPDARent].Value.ToDecimal(), row.Cells["InitialBalance"].Value.ToDecimal(), row.Cells[COLS.CommissionPay].Value.ToDecimal(), ref accJobsTotal, ref transId, ref rentDue, ref currBalance, row.Cells[COLS.Hokiday].Value.ToBool()))
                    {

                        IsSavedTrans = true;

                        row.Cells[COLS.CommissionId].Value = transId;
                        //row.IsCurrent = true;
                        
                        row.Cells[COLS.AccountsTotal].Value = accJobsTotal;
                        row.Cells[COLS.Owed].Value = rentDue;
                        row.Cells[COLS.CurrBalance].Value = currBalance;

                    }             
                  
	            }


                if (IsSavedTrans)
                {

                    grdDriverCommission.Columns[COLS.CommissionPay].ReadOnly = true;
                    btnPrintAll.Enabled = true;
                    btnViewPrint.Enabled = true;
                }
                //RefreshCommissionView();

            }
            catch (Exception ex)
            {



            }

        }




        private void cbAllCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllDrivers.Checked == true)
            {
                if (grdDriverCommission.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDriverCommission.Rows.Count; i++)
                    {
                        grdDriverCommission.Rows[i].Cells["Check"].Value = true;//..CurrentCell.Value;
                    }
                }
            }
            else if (cbAllDrivers.Checked == false)
            {
                if (grdDriverCommission.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDriverCommission.Rows.Count; i++)
                    {
                        grdDriverCommission.Rows[i].Cells["Check"].Value = false;//..CurrentCell.Value;

                    }
                }
            }
        }


        List<Fleet_Driver_DebitCreditNote> listofDrvDebitCreditNotes = null;

        private bool OnSave(int DriverId, decimal oldBalance, decimal DriverCommissionPerBooking, decimal pdaCommission, decimal InitialBalance, decimal CommissionPayValue, ref decimal accJobs, ref long transId, ref decimal rentDue, ref decimal currBalance, bool IsHoliday)
        {

            bool IsSaved = false;


            try
            {

                //long savedTransId = transId;

                //if (savedTransId > 0)
                //{


                //    var query = General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

                //    if (query != null)
                //    {
                //        if (savedTransId == query.Id)
                //            oldBalance = query.OldBalance.ToDecimal();
                //    }


                //}

                long savedTransId = transId;


                if (savedTransId > 0)
                {
                    var query = General.GetObject<Fleet_DriverCommision>(c => c.Id == savedTransId);

                    if (query != null)
                    {
                     
                        oldBalance = query.OldBalance.ToDecimal();
                    }                  


                }


                bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();
               

                var CommissionList = General.GetGeneralList<Fleet_DriverCommision_Charge>(c => c.BookingId != null && c.TransId != null);
                var list3 = General.GetQueryable<Gen_SysPolicy_AirportDropOffCharge>(null).ToList();
                var list4 = General.GetQueryable<Gen_SysPolicy_AirportPickupCharge>(null).ToList();
                var list2 = (from a in General.GetGeneralList<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                   
                    && (c.FareRate != null)
                    && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                                    && (c.DriverId == DriverId) &&
                                 (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date))
                             join b in CommissionList on a.Id equals b.BookingId into table2
                              from b in table2.DefaultIfEmpty()
                             join d1 in list3 on a.ToLocId equals d1.AirportId into table3
                             from d1 in table3.DefaultIfEmpty()
                             join p1 in list4 on a.FromLocId equals p1.AirportId into table4
                             from p1 in table4.DefaultIfEmpty()
                              where (b==null)
                             select new
                             {
                                 Id = a.Id,
                                 TotalFare = a.FareRate.ToDecimal(),
                                 CompanyId=a.CompanyId,
                               //  AccountTypeId = a.Gen_Company != null && a.Gen_Company.AccountTypeId.ToInt()==Enums.ACCOUNT_TYPE.ACCOUNT ? a.Gen_Company.AccountTypeId : Enums.ACCOUNT_TYPE.CASH,
                                 PaymentTypeId = a.PaymentTypeId, //1,//a.Gen_Company.AccountTypeId,
 
                                 DriverCommissionAmount=a.DriverCommission,
                                 DriverCommissionType=a.DriverCommissionType,
                                 IsCommissionWise=a.IsCommissionWise,
                                 AgentCommission = a.AgentCommission,
                                 DropOffCharge = d1 != null ? d1.Charges.ToDecimal() : 0,
                                 PickupCharge = p1 != null ? p1.Charges.ToDecimal() : 0,
                                 ServiceCharges=a.ServiceCharges
                             }).ToList();



                objMaster = new DriverCommisionBO();

                if (transId == 0)
                {

                    objMaster.New();

                }
                else
                {
                    objMaster.GetByPrimaryKey(transId);

                    if (objMaster.Current == null)
                        objMaster.New();

                }

                    List<Fleet_DriverCommision_Charge> ListDetail = (from a in list2
                                                                     select new Fleet_DriverCommision_Charge
                                                          {
                                                              //Id = a.Id,
                                                              TransId = objMaster.Current.Id,
                                                              BookingId = a.Id,
                                                              CommissionPerBooking = (a.TotalFare * DriverCommissionPerBooking) / 100
                                                              //CommissionPerBooking=a.DriverCommissionAmount
                                                          }).ToList();


                    decimal Total = list2.Sum(c => c.TotalFare).ToDecimal();
                    decimal ACCJobsTotal = list2.Where(c => c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH)
                                           .Sum(c => c.TotalFare).ToDecimal();

                    double totalWeeks = (dtpTillDate.Value.ToDate().Subtract(dtpFromDate.Value.ToDate()).TotalDays) / 7;

                    if (totalWeeks <= 0)
                        totalWeeks = 1;
                    decimal TotalPDARent = (pdaCommission * totalWeeks.ToInt());

                    decimal AgentCommission = list2.Sum(c => c.AgentCommission).ToDecimal();
                    decimal DriverCommissionTotal = 0;

                    if (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt() == 1)
                    {
                        DriverCommissionTotal = ((Total - AgentCommission) * DriverCommissionPerBooking / 100);
                    }
                    else if (AppVars.objPolicyConfiguration.DrvCommissionCalculationType.ToInt() == 4)
                    {
                        DriverCommissionTotal = (Total * DriverCommissionPerBooking / 100);
                    }
                    else
                    {
                        DriverCommissionTotal = ((Total + AgentCommission) * DriverCommissionPerBooking / 100);

                    }


                    decimal owed = ((AgentCommission + TotalPDARent + DriverCommissionTotal) - ACCJobsTotal);



                    rentDue = owed;
                    
                    //NC
                    decimal TotalDropOffCharge = list2.Where(c => c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).Sum(c => c.DropOffCharge).ToDecimal();
                    decimal TotalPickupCharge = list2.Where(c => c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).Sum(c => c.PickupCharge).ToDecimal();
                    decimal AccountExpenses = (TotalDropOffCharge + TotalPickupCharge);


                    decimal TotalServiceCharges = list2.Where(c => c.PaymentTypeId == Enums.PAYMENT_TYPES.CASH).Sum(c => c.ServiceCharges).ToDecimal();


                    //
                    //NC
                    //decimal  DriverCommission = ((Total - AgentCommission) * DriverCommissionPerBooking / 100);
                      
                    decimal totalCredit = 0.00m;
                    decimal totalDebit = 0.00m;


                    decimal Debit = 0.00m;
                    decimal Credit = 0.00m;
                    if (savedTransId > 0 && objMaster.Current.Fleet_DriverCommissionExpenses.Count > 0)
                    {
                        Debit = objMaster.Current.Fleet_DriverCommissionExpenses.Where(c => c.Debit.ToDecimal() > 0).Select(c => c.Debit).Sum().ToDecimal();
                        Credit = objMaster.Current.Fleet_DriverCommissionExpenses.Where(c => c.Credit.ToDecimal() > 0).Select(c => c.Credit).Sum().ToDecimal();
                    }



                    totalCredit = (ACCJobsTotal + AccountExpenses);
                    totalDebit = (TotalPDARent + DriverCommissionTotal +TotalServiceCharges);
                    owed = (totalCredit - totalDebit);
                    //row.Cells[COLS.CurrBalance].Value = (owed + oldBalance);
                   // row.Cells[COLS.Owed].Value = owed;
                    objMaster.Current.Balance = (owed+oldBalance);
                    objMaster.Current.DriverOwed = owed;
                    objMaster.Current.AccountExpenses = AccountExpenses;
                    //

                    objMaster.Current.AccJobsTotal = ACCJobsTotal;
                    objMaster.Current.TotalServiceCharges = TotalServiceCharges;
                    //OC
                    //objMaster.Current.DriverOwed = rentDue;
                    //objMaster.Current.Balance = (rentDue + oldBalance);//RentPay - DriverRent;
                    //NC
                    objMaster.Current.CommissionTotal = DriverCommissionTotal;
                    objMaster.Current.JobsTotal = Total;
                    objMaster.Current.AgentFeesTotal = AgentCommission;


                    objMaster.Current.CommisionPay = CommissionPayValue.ToDecimal();

                  

                    objMaster.Current.DriverCommision = DriverCommissionPerBooking;

                    // add pda rent
                    objMaster.Current.PDARent = pdaCommission;

                    objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();
                    objMaster.Current.AddLog = AppVars.LoginObj.LoginName.ToStr();
                    objMaster.Current.AddOn = DateTime.Now;
                    objMaster.Current.TransDate = DateTime.Now; //dtpTransactionDate.Value.ToDateTime();
                    objMaster.Current.DriverId = DriverId;//ddlDriver.SelectedValue.ToIntorNull();
                    objMaster.Current.OldBalance = oldBalance;// (CurrentBalance - RentPay);
                    objMaster.Current.FromDate = dtpFromDate.Value.ToDate();
                    objMaster.Current.ToDate = dtpTillDate.Value.ToDate();
                    objMaster.Current.TransFor = "Weekly";//ddlDayWise.SelectedText.ToStr();


                    objMaster.Current.Fuel = 0;//Fuel.ToDecimal();
                    objMaster.Current.Extra = pdaCommission;// Extra.ToDecimal();


                    string[] skipProperties = { "Fleet_DriverCommision", "Booking" };
                    IList<Fleet_DriverCommision_Charge> savedList = objMaster.Current.Fleet_DriverCommision_Charges;

                    var list = objMaster.Current.Fleet_DriverCommision_Charges.ToList();


                    Utils.General.SyncChildCollection(ref savedList, ref ListDetail, "Id", skipProperties);

                    
                    //if (chkApplyBookingFees.Checked)
                    //{
                    //    int TotalJobs = list2.Count;
                    //    decimal BookingFees = 0.5m;

                    //    Fleet_DriverCommissionExpense objDriverCommissionExpense = new Fleet_DriverCommissionExpense();
                    //    objDriverCommissionExpense.Debit = (TotalJobs * BookingFees);
                    //    objDriverCommissionExpense.Date = DateTime.Now;
                    //    objDriverCommissionExpense.Description = "Booking Fees";
                     
                    //    if (objMaster.Current.Fleet_DriverCommissionExpenses.Count == 0)
                    //    {
                    //        objMaster.Current.Fleet_DriverCommissionExpenses.Add(objDriverCommissionExpense);
                    //    }

                    //    objMaster.Current.Balance -= objDriverCommissionExpense.Debit;                      

                    //}


                    if (savedTransId == 0)
                    {


                        Fleet_DriverCommissionExpense objDriverCommissionExpense2 = null;
                        foreach (var item in listofDrvDebitCreditNotes.Where(c => c.DriverId == DriverId))
                        {
                            objDriverCommissionExpense2 = new Fleet_DriverCommissionExpense();


                            if ((item.Gen_DebitCreditNote.ExpenseType.ToStr() == "Debit"))
                            {
                                if (item.IsWeekly.ToBool())
                                {
                                    objDriverCommissionExpense2.Debit = (totalWeeks.ToInt() * item.Charges.ToDecimal());
                                }
                                else
                                {
                                    objDriverCommissionExpense2.Debit = item.Charges.ToDecimal();
                                }

                                objMaster.Current.Balance -= objDriverCommissionExpense2.Debit;
                            }
                            else
                            {
                                if (item.IsWeekly.ToBool())
                                {
                                    objDriverCommissionExpense2.Credit = (totalWeeks.ToInt() * item.Charges.ToDecimal());

                                }
                                else
                                {
                                    objDriverCommissionExpense2.Credit = item.Charges.ToDecimal();
                                }

                                objMaster.Current.Balance += objDriverCommissionExpense2.Debit;
                            }

                            objDriverCommissionExpense2.Date = DateTime.Now;
                            objDriverCommissionExpense2.Description = item.Gen_DebitCreditNote.NoteName.ToStr();

                            objMaster.Current.Fleet_DriverCommissionExpenses.Add(objDriverCommissionExpense2);


                        }
                    }


                    if (IsHoliday)
                    {
                        objMaster.Current.Extra = 0.00m;
                        objMaster.Current.PDARent = 0.00m;
                        objMaster.Current.CollectionDeliveryCharges = 0.00m;
                        
                    }

                    objMaster.Current.WeekOff = IsHoliday;

                    objMaster.DisableCheckTotalBookings = true;

                    objMaster.Current.TransactionType = Enums.TRANSACTIONTYPE.DRIVER_COMMISSION_EXPENSE1;


                    objMaster.Save();

                    currBalance = objMaster.Current.Balance.ToDecimal();
                    IsSaved = true;
                    transId = objMaster.Current.Id;

               
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

            return IsSaved;

        }
         
        private void btnExits_Click_1(object sender, EventArgs e)
        {
            this.Close();
            //this.Hide();

        }
        public void ShowDriverCommission(int Id)
        {
            frmDriverCommissionDebitCredit frm = new frmDriverCommissionDebitCredit();
           
            frm.OnDisplayRecord(Id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommissionDebitCredit1");

            if (doc != null)
            {
                doc.Close();
            }

            MainMenuForm.MainMenuFrm.ShowForm(frm);
        }

        //public  void ShowCompanyInvoiceForm(int id)
        //{


        //    frmDriverRent frm = new frmDriverRent(id);
        //    frm.FormBorderStyle = FormBorderStyle.FixedSingle;
        //    frm.MaximizeBox = false;
        //    frm.MinimizeBox = false;
        //    frm.ControlBox = true;
        //    //frm.OnDisplayRecord(id);
        //    frm.ShowDialog();
        //    frm.Dispose();
        //}

        private void grdCompany_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (grdDriverCommission.CurrentRow != null && grdDriverCommission.CurrentRow is GridViewDataRowInfo)
            {
                int CommissionId = grdDriverCommission.CurrentRow.Cells[COLS.CommissionId].Value.ToInt();
                if (CommissionId > 0)
                {
                    ShowDriverCommission(CommissionId);
                 //  ShowCompanyInvoiceForm(id);  
                }
              
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }

        private void btnEmailInvoices_Click(object sender, EventArgs e)
        {


            EmailInvoices();
           
        }


        private void EmailInvoices()
        {
            try
            {
                string subject = txtSubject.Text.Trim();

                if (string.IsNullOrEmpty(subject))
                {
                    ENUtils.ShowMessage("Required : Email Subject");
                    return;
                }

                var rows = grdDriverCommission.Rows.Where(c => c.Cells[COLS.CommissionId].Value.ToLong() > 0).ToList();



                List<long> invoiceIds = rows.Select(c => c.Cells[COLS.CommissionId].Value.ToLong()).ToList<long>();

                if (invoiceIds.Count > 0)
                {
                    //frmDriverTransactionReport frm = new frmDriverTransactionReport(1);
                    //frmDriverCommisionTransactionReport frm = new frmDriverCommisionTransactionReport(1);
                    frmDriverCommisionTransactionExpensesReport frm = new frmDriverCommisionTransactionExpensesReport(1);
                    //var list = General.GetQueryable<vu_DriverCommision>(a => invoiceIds.Contains(a.Id)).ToList();
                    var list = General.GetQueryable<vu_DriverCommisionExpense>(a => invoiceIds.Contains(a.Id)).ToList();
                    var list2 = General.GetQueryable<vu_FleetDriverCommissionExpense>(a => invoiceIds.Contains(a.Id)).ToList();
                    // frm.CompanyHeader = ddlSubCompany.Text.Trim();

                    List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 2);

                    frmEmail frmEmail = new frmEmail(null, "", "");


                    foreach (var item in rows.Where(c => c.Cells["Check"].Value.ToBool()))
                    {
                        frm.DataSource = list.Where(c => c.Id == item.Cells[COLS.CommissionId].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                        frm.DataSource2 = list2.Where(c => c.CommissionId == item.Cells[COLS.CommissionId].Value.ToLong()).OrderBy(c => c.Date).ToList();

                        frm.GenerateReport();



                        //string email = driversList.FirstOrDefault(c => c.Id == item.Cells[COLS.Id].Value.ToInt()).DefaultIfEmpty().Email.ToStr().Trim();
                        string email = item.Cells[COLS.DriverEmail].Value.ToStr();


                        if (!string.IsNullOrEmpty(email))
                        {


                            frm.SendEmailInternally(frmEmail, subject, item.Cells[COLS.DriverNo].Value.ToStr().Trim(), email);
                        }
                    }


                    if (frmEmail != null && frmEmail.IsDisposed == false)
                    {
                        frmEmail.Close();
                        GC.Collect();

                    }



                    ENUtils.ShowMessage("Email has been sent successfully");

                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }





        }

        private void btnDisplayRent_Click(object sender, EventArgs e)
        {
            DisplayCommission();
        }


        private void DisplayCommission()
        {

            try
            {
                decimal CommissionDue = 0;
                int DriverId = 0;
                decimal DriverCommissionPerBooking = 0.00m;
                decimal pdaRent = 0.00m;
                decimal oldBalance = 0.00m;
                decimal AgentCommission = 0;
                foreach (var row in grdDriverCommission.Rows.Where(c => c.Cells["Check"].Value.ToBool()))
                {

                    double totalWeeks = (dtpTillDate.Value.ToDate().Subtract(dtpFromDate.Value.ToDate()).TotalDays) / 7;

                    if (totalWeeks <= 0)
                        totalWeeks = 1;

                    DriverId = row.Cells["Id"].Value.ToInt();
                    //DriverCommission = row.Cells["DriverCommission"].Value.ToDecimal();
                    DriverCommissionPerBooking = row.Cells[COLS.DriverCommissionPerBooking].Value.ToDecimal();
                    // add pda rent
                    pdaRent = row.Cells[COLS.DriverPDARent].Value.ToDecimal();

                    // numpdaRent.Value = numPDARentPerWeek.Value * totalWeeks.ToInt();
                    decimal TotalPDARent = (pdaRent * totalWeeks.ToInt());
                    oldBalance = row.Cells[COLS.OldBalance].Value.ToDecimal();



                    CommissionDue = 0;


                    try
                    {


                        bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();

                        var commissionList = General.GetGeneralList<Fleet_DriverCommision_Charge>(c => c.BookingId != null && c.TransId != null);
                        //c.CompanyId != null  &&
                        var list3 = General.GetQueryable<Gen_SysPolicy_AirportDropOffCharge>(null).ToList();
                        var list4 = General.GetQueryable<Gen_SysPolicy_AirportPickupCharge>(null).ToList();
                        var list2 = (from a in General.GetGeneralList<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED

                            && (c.FareRate != null)
                            && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                                            && (c.DriverId == DriverId)
                                        && (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date))
                                     join b in commissionList on a.Id equals b.BookingId into table2
                                     from b in table2.DefaultIfEmpty()
                                     join d1 in list3 on a.ToLocId equals d1.AirportId into table3
                                     from d1 in table3.DefaultIfEmpty()
                                     join p1 in list4 on a.FromLocId equals p1.AirportId into table4
                                     from p1 in table4.DefaultIfEmpty()

                                     where (b == null)
                                     select new
                                     {
                                         Id = a.Id,
                                         TotalFare = a.FareRate.ToDecimal(),
                                         //  TotalCharges = a.TotalCharges,

                                         CompanyId = a.CompanyId,
                                         AccountTypeId =a.PaymentTypeId, //1,//a.Gen_Company.AccountTypeId,
                                         DriverCommissionAmount = a.DriverCommission,
                                         DriverCommissionType = a.DriverCommissionType,
                                         IsCommissionWise = a.IsCommissionWise,
                                         AgentCommission = a.AgentCommission,
                                         DropOffCharge = d1 != null ? d1.Charges.ToDecimal() : 0,
                                         PickupCharge = p1 != null ? p1.Charges.ToDecimal() : 0
                                     }).ToList();



                        oldBalance = row.Cells[COLS.OldBalance].Value.ToDecimal();


                        decimal Total = list2.Sum(c => c.TotalFare).ToDecimal();

                        decimal ACCJobsTotal = list2.Where(c =>c.AccountTypeId!=Enums.PAYMENT_TYPES.CASH).Sum(c => c.TotalFare).ToDecimal();

                        decimal CashJobsTotal = list2.Where(c => c.AccountTypeId==Enums.PAYMENT_TYPES.CASH).Sum(c => c.TotalFare).ToDecimal();

                        AgentCommission = list2.Sum(c => c.AgentCommission).ToDecimal();

                        decimal JobsTotal = (ACCJobsTotal + CashJobsTotal);
                        decimal TotalDropOffCharge = list2.Where(c => c.AccountTypeId!=Enums.PAYMENT_TYPES.CASH).Sum(c => c.DropOffCharge).ToDecimal();
                        decimal TotalPickupCharge = list2.Where(c => c.AccountTypeId != Enums.PAYMENT_TYPES.CASH).Sum(c => c.PickupCharge).ToDecimal();
                        decimal AccountExpenses = (TotalDropOffCharge + TotalPickupCharge);
                        DriverCommissionPerBooking = ((JobsTotal - AgentCommission) * row.Cells[COLS.DriverCommissionPerBooking].Value.ToDecimal() / 100);

                        decimal owed = ((AgentCommission + TotalPDARent + DriverCommissionPerBooking) - ACCJobsTotal);
                        decimal PDARent = row.Cells[COLS.DriverPDARent].Value.ToDecimal();

                        CommissionDue = owed;
                        //NC
                        decimal totalCredit = 0.00m;
                        decimal totalDebit = 0.00m;
                        int TotalJobs = list2.Count;
                        decimal BookingFees = 0.5m;
                        totalCredit = (ACCJobsTotal + AccountExpenses);
                        totalDebit = (TotalPDARent + DriverCommissionPerBooking);
                        owed = (totalCredit - totalDebit);
                        row.Cells[COLS.CurrBalance].Value = (owed + oldBalance);
                        row.Cells[COLS.Owed].Value = owed;
                        row.Cells[COLS.AccountExpense].Value = AccountExpenses;
                        row.Cells[COLS.TotalPDARent].Value = TotalPDARent;

                        if (chkApplyBookingFees.Checked)
                        {
                            row.Cells[COLS.BookingFees].Value = (TotalJobs * BookingFees);
                        }
                        //
                        //NC
                        //totalCredit = (txtTotalAccountBooking.Text.TrimStart('£', ' ').ToDecimal() + Credit + (spnAccountExpenses.Value.ToDecimal()));
                        //totalDebit = (numpdaRent.Value + txtCommsionTotal.Text.ToDecimal() + Debit);

                        //owedBalance = (totalCredit - totalDebit);
                        //txtDriverOwed.Text = owedBalance.ToStr();
                        //Currentbalance = owedBalance + txtOldBalance.Text.TrimStart('£', ' ').ToDecimal();
                        //txtBalance.Text = Currentbalance.ToStr(); 
                        //

                        row.Cells[COLS.CashTotal].Value = CashJobsTotal;
                        row.Cells[COLS.JobsTotal].Value = (JobsTotal);
                        row.Cells[COLS.AccountsTotal].Value = ACCJobsTotal;

                        //row.Cells[COLS.Owed].Value = CommissionDue;
                        //row.Cells[COLS.CurrBalance].Value =(CommissionDue+oldBalance);

                        row.Cells[COLS.CommissionPay].Value = 0.00m;

                        row.Cells[COLS.CommissionId].Value = null;
                        row.Cells[COLS.AgentFees].Value = AgentCommission;
                        row.Cells[COLS.DriverCommission].Value = DriverCommissionPerBooking;

                        dtpFromDate.Enabled = false;
                        dtpTillDate.Enabled = false;

                    }
                    catch (Exception ex)
                    {
                        ENUtils.ShowMessage(ex.Message);
                        //if (objMaster.Errors.Count > 0)
                        //    ENUtils.ShowMessage(objMaster.ShowErrors());
                        //else
                        //{
                        //    ENUtils.ShowMessage(ex.Message);

                        //}
                    }

                    //
                }
            }
            catch (Exception ex)
            {



            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbAllDrivers.Checked = true;
            dtpFromDate.Enabled = true;
            dtpTillDate.Enabled = true;


            grdDriverCommission.Columns[COLS.CommissionPay].ReadOnly = false;

            RefreshCommissionView();
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            PrintDocument();
        }


        private void PrintDocument()
        {

            try
            {
                try
                {



                    var rows = grdDriverCommission.Rows.Where(c => c.Cells[COLS.CommissionId].Value.ToLong() > 0).ToList();



                    List<long> invoiceIds = rows.Select(c => c.Cells[COLS.CommissionId].Value.ToLong()).ToList<long>();

                    if (invoiceIds.Count > 0)
                    {
                        //frmDriverCommisionTransactionReport frm = new frmDriverCommisionTransactionReport(1);
                        frmDriverCommisionTransactionExpensesReport frm = new frmDriverCommisionTransactionExpensesReport(1);
                        frm.CompanyHeader = ddlSubCompany.Text.Trim();

                        var list = General.GetQueryable<vu_DriverCommisionExpense>(a => invoiceIds.Contains(a.Id)).ToList();
                        var list2 = General.GetQueryable<vu_FleetDriverCommissionExpense>(a => invoiceIds.Contains(a.Id)).ToList();


                        List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 2);

                        frmEmail frmEmail = new frmEmail(null, "", "");


                        foreach (var item in rows)
                        {
                            frm.DataSource = list.Where(c => c.Id == item.Cells[COLS.CommissionId].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                            frm.DataSource2 = list2.Where(c => c.CommissionId == item.Cells[COLS.CommissionId].Value.ToLong()).OrderBy(c=>c.Date).ToList();

                            frm.GenerateReport();

                            ReportPrintDocument rpt = new ReportPrintDocument(frm.reportViewer1.LocalReport);
                            rpt.Print();
                            rpt.Dispose();



                        }



                      

                    }
                }
                catch (Exception ex)
                {
                    ENUtils.ShowMessage(ex.Message);

                }



               
            }
            catch (Exception ex)
            {


            }


        }

        private void radGroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnViewPrint_Click(object sender, EventArgs e)
        {
            try
            {


                var rows = grdDriverCommission.Rows.Where(c => c.Cells[COLS.CommissionId].Value.ToLong() > 0).ToList();

                var list = (from a in grdDriverCommission.Rows.Where(c => c.Cells[COLS.CommissionId].Value.ToInt() > 0)
                            select new
                            {
                                CommissionId = a.Cells[COLS.CommissionId].Value.ToInt(),
                                DriverId = a.Cells[COLS.Id].Value.ToInt(),
                                Driver = a.Cells[COLS.DriverNo].Value.ToStr()
                            }).ToList();


                //  List<long> invoiceIds = rows.Select(c => c.Cells[COLS.CommissionId].Value.ToLong()).ToList<long>();

                if (list.Count > 0)
                {
                    frmDriverCommisionTransactionExpensesReport frm = new frmDriverCommisionTransactionExpensesReport(list, dtpFromDate.Value.ToDate(), dtpTillDate.Value.ToDate());
                    frm.ShowDialog();
                    frm.Dispose();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }

        private void btnDeleteGenerated_Click(object sender, EventArgs e)
        {
            try
            {

                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Delete All ? ", "", MessageBoxButtons.YesNo))
                {

                    string[] list = grdDriverCommission.Rows.Where(c => c.Cells[COLS.CommissionId].Value.ToLong() > 0).Select(c => c.Cells[COLS.CommissionId].Value.ToStr())
                                            .ToArray<string>();

                    if (list.Count() > 0)
                    {
                        string arr = string.Join(",", list);

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_RunProcedure("delete from fleet_drivercommision where Id in (" + arr + ")");

                        }

                        btnDeleteGenerated.Visible = false;

                        grdDriverCommission.Rows.ToList().ForEach(c => c.Cells[COLS.CommissionId].Value = 0);
                    }
                }


            }
            catch (Exception ex)
            {


            }
        }
    }
}
