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
    public partial class frmAddAllDriverRentExpenses2 : UI.SetupBase
    {
        DriverRentBO objMaster = null;

        ConditionalFormattingObject objCondition = new ConditionalFormattingObject();
        ConditionalFormattingObject objConditionGeneratedTrans = new ConditionalFormattingObject();
        public struct COLS
        {
            public static string Email = "Email";
            public static string Id = "Id";
            public static string DriverNo = "DriverNo";

            public static string DriverRent = "DriverRent";
            public static string DriverPDARent = "DriverPDARent";

            public static string RentPay = "RentPay";
            public static string AccountExpenses = "AccountExpenses";
            public static string OldBalance = "OldBalance";
            public static string InitialBalance = "InitialBalance";

            public static string CurrBalance = "CurrBalance";

            public static string AccountsTotal = "AccountsTotal";
            public static string RentDue = "RentDue";
            public static string RentId = "RentId";
            public static string CarRent = "CarRent";
            public static string CarInsuranceRent = "CarInsuranceRent";
            public static string PrimeCompanyRent = "PrimeCompanyRent";
            public static string UseCompanyVehicle = "UseCompanyVehicle";
            public static string TotalDriverPDARent = "TotalPDARent";
            public static string TotalCarRent = "TotalCarRent";
            public static string TotalPrimeCompanyRent = "TotalPrimeCompanyRent";
            public static string TotalCarInsuranceRent = "TotalCarInsuranceRent";
            public static string IsPrimeCompanyDriver = "IsPrimeCompanyDriver";

            public static string IsHoliday = "IsHoliday";

        }



        public frmAddAllDriverRentExpenses2()
        {

            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(frmAddMultipleCompanyInvoice_FormClosed);
            this.grdDriverRent.CellEndEdit += new GridViewCellEventHandler(grdDriverRent_CellEndEdit);
            this.KeyDown += new KeyEventHandler(frmAddAllDriverRent_KeyDown);

            btnDisplayRent.Visible = false;

            grdDriverRent.CellValueChanged+=new GridViewCellEventHandler(grdDriverRent_CellValueChanged); 
        }

        void frmAddAllDriverRent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        void grdDriverRent_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            //try
            //{
            //    decimal RentPay = grdDriverRent.CurrentRow.Cells["RentPay"].Value.ToDecimal();
            //    decimal DriverRent = grdDriverRent.CurrentRow.Cells["DriverRent"].Value.ToDecimal();
            //    decimal InitialBalance = grdDriverRent.CurrentRow.Cells["InitialBalance"].Value.ToDecimal();
            //    decimal Balance = -(DriverRent - InitialBalance);
            //    decimal DriverBalance = grdDriverRent.CurrentRow.Cells["Balance"].Value.ToDecimal();
            //    decimal Current = 0;
            //    if (RentPay > 0)
            //    {
            //        if (DriverBalance != 0)
            //        {
            //            Current = (RentPay + (-DriverRent) + (DriverBalance));
            //            e.Row.Cells[COLS.CurrentBalance].Value = Current;
            //        }
            //        else
            //        {
            //            Current = (RentPay + (-DriverRent) + (InitialBalance));
            //            e.Row.Cells[COLS.CurrentBalance].Value = Current;

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{ 

            //}
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


            try
            {


                int subtracted = 7 - (int)fromDate.Value.DayOfWeek;

                int DaysToSubtract = (int)DateTime.Now.DayOfWeek;
                DateTime dtFrom = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));

                if (subtracted == 7)
                    subtracted = 6;

                fromDate = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.AddDays(-subtracted).Day, fromDate.Value.Hour, fromDate.Value.Minute, 0, 0);


                DateTime dtTo = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
                toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dtTo.AddDays(toDate.Value.DayOfWeek.ToInt()).Day, toDate.Value.Hour, toDate.Value.Minute, 59, 999);


                dtpFromDate.Value = fromDate;
                dtpTillDate.Value = toDate;
            }
            catch
            {
                dtpFromDate.Value = DateTime.Now.AddDays(-6).ToDate();
                dtpTillDate.Value = DateTime.Now.ToDate();


            }


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
                objMaster = new DriverRentBO();
                //DateTime OneMonthBefore = DateTime.Now;
                //OneMonthBefore = OneMonthBefore.A(-1);


                GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
                col.Width = 40;
                col.AutoSizeMode = BestFitColumnMode.None;
                col.HeaderText = "";
                col.Name = "Check";

                //  col.ReadOnly = true;
                grdDriverRent.Columns.Add(col);
                col = new GridViewCheckBoxColumn();
                col.Name = COLS.UseCompanyVehicle;
                col.IsVisible = false;


                grdDriverRent.Columns.Add(col);
                col = new GridViewCheckBoxColumn();
                col.Name = COLS.IsPrimeCompanyDriver;
                col.IsVisible = false;
                grdDriverRent.Columns.Add(col);

                GridViewTextBoxColumn tbcol = new GridViewTextBoxColumn();
                tbcol.Name = COLS.Id;
                tbcol.IsVisible = false;
                grdDriverRent.Columns.Add(tbcol);


                tbcol = new GridViewTextBoxColumn();
                tbcol.Name = COLS.RentId;
                tbcol.IsVisible = false;
                grdDriverRent.Columns.Add(tbcol);


                tbcol = new GridViewTextBoxColumn();
                tbcol.Name = COLS.DriverNo;
                tbcol.HeaderText = "Driver";
                tbcol.Width = 100;
                tbcol.ReadOnly = true;
                grdDriverRent.Columns.Add(tbcol);


                col = new GridViewCheckBoxColumn();
                col.Width = 60;
                col.AutoSizeMode = BestFitColumnMode.None;
                col.HeaderText = "Holiday";
                col.IsVisible = true;
                col.Name = COLS.IsHoliday;
                grdDriverRent.Columns.Add(col);


                GridViewDecimalColumn dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.OldBalance;
                dcol.Width = 100;
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = false;
                dcol.HeaderText = "Old Balance";
                dcol.ReadOnly = true;
                grdDriverRent.Columns.Add(dcol);



                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.InitialBalance;
                dcol.HeaderText = "Initial Balance";
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = false;
                grdDriverRent.Columns.Add(dcol);


                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.DriverRent;
                dcol.Width = 70;
                dcol.ReadOnly = false;
                dcol.DecimalPlaces = 2;
                dcol.HeaderText = "Rent";
                grdDriverRent.Columns.Add(dcol);


                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.DriverPDARent;
                dcol.Width = 70;
                dcol.ReadOnly = false;
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = true;
                dcol.HeaderText = "PDA Rent";
                grdDriverRent.Columns.Add(dcol);
                           
                


                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.CarRent;
                dcol.HeaderText = "Car Rent";
                dcol.Width = 80;
                dcol.ReadOnly = false;
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = true;
                grdDriverRent.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.CarInsuranceRent;
                dcol.HeaderText = "Car Insurance Rent";
                dcol.Width = 130;
                dcol.ReadOnly = false;
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = true;
                grdDriverRent.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.PrimeCompanyRent;
                dcol.HeaderText = "Prime Company Rent";
                dcol.Width = 150;
                dcol.ReadOnly = false;
                dcol.IsVisible = true;
                dcol.DecimalPlaces = 2;
                grdDriverRent.Columns.Add(dcol);



            // To Show Rent
                //dcol = new GridViewDecimalColumn();
                //dcol.Name = COLS.TotalDriverPDARent;
                //dcol.Width = 120;
                //dcol.ReadOnly = true;
                //dcol.DecimalPlaces = 2;
                //dcol.HeaderText = "Total PDA Rent";
                //grdDriverRent.Columns.Add(dcol);

                //dcol = new GridViewDecimalColumn();
                //dcol.Name = COLS.TotalCarRent;
                //dcol.HeaderText = "Total Car Rent";
                //dcol.Width = 120;
                //dcol.ReadOnly = true;
                //dcol.DecimalPlaces = 2;
                //grdDriverRent.Columns.Add(dcol);

                //dcol = new GridViewDecimalColumn();
                //dcol.Name = COLS.TotalCarInsuranceRent;
                //dcol.HeaderText = "Total Car Insurance Rent";
                //dcol.Width = 150;
                //dcol.ReadOnly = true;
                //dcol.DecimalPlaces = 2;
                //grdDriverRent.Columns.Add(dcol);

                //dcol = new GridViewDecimalColumn();
                //dcol.Name = COLS.TotalPrimeCompanyRent;
                //dcol.HeaderText = "Total Prime Company Rent";
                //dcol.Width = 160;
                //dcol.ReadOnly = true;
                //dcol.DecimalPlaces = 2;
                //grdDriverRent.Columns.Add(dcol);
                //

                //dcol = new GridViewDecimalColumn();
                //dcol.Name = COLS.RentDue;
                //dcol.HeaderText = "Rent Due";
                //dcol.Width = 90;
                //dcol.ReadOnly = true;
                //dcol.DecimalPlaces = 2;
                //grdDriverRent.Columns.Add(dcol);



                //dcol = new GridViewDecimalColumn();
                //dcol.Name = COLS.CurrBalance;
                //dcol.HeaderText = "Current Balance";
                //dcol.Width = 120;
                //dcol.DecimalPlaces = 2;
                //dcol.ReadOnly = true;
                //grdDriverRent.Columns.Add(dcol);




                grdDriverRent.ShowRowHeaderColumn = false;


                objCondition.CellForeColor = Color.Red;
                objCondition.TValue1 = "x";
                objCondition.ConditionType = ConditionTypes.NotEqual;
            //    grdDriverRent.Columns[COLS.CurrBalance].ConditionalFormattingObjectList.Add(objCondition);



                objConditionGeneratedTrans.RowBackColor = Color.LightGreen;
                objConditionGeneratedTrans.TValue1 = "0";
                objConditionGeneratedTrans.ApplyToRow = true;
                objConditionGeneratedTrans.ConditionType = ConditionTypes.Greater;
                grdDriverRent.Columns[COLS.RentId].ConditionalFormattingObjectList.Add(objConditionGeneratedTrans);

                grdDriverRent.CellBeginEdit += new GridViewCellCancelEventHandler(grdDriverRent_CellBeginEdit);

                RefreshRentView();



            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }

        }


        void grdDriverRent_CellValueChanged(object sender, GridViewCellEventArgs e)
        {
            if (e.Column != null && e.Column is GridViewCheckBoxColumn && e.Column.Name == COLS.IsHoliday)
            {
                if (e.Value.ToBool())
                {
                    e.Row.Cells[COLS.DriverPDARent].Tag = e.Row.Cells[COLS.DriverPDARent].Value;
                    e.Row.Cells[COLS.CarRent].Tag = e.Row.Cells[COLS.CarRent].Value;


                    e.Row.Cells[COLS.CarRent].Value = 0.00m;
                    e.Row.Cells[COLS.DriverPDARent].Value = 0.00m;
                
                }
                else
                {

                    e.Row.Cells[COLS.CarRent].Value = e.Row.Cells[COLS.CarRent].Tag.ToDecimal();
                    e.Row.Cells[COLS.DriverPDARent].Value = e.Row.Cells[COLS.DriverPDARent].Tag.ToDecimal();                

                }

            }

        }

        void grdDriverRent_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            if(e.Column!=null)
            {
                if ((e.Column.Name == COLS.CarInsuranceRent || e.Column.Name == COLS.CarRent) && e.Row.Cells[COLS.UseCompanyVehicle].Value.ToBool() == false)
                {

                    e.Cancel = true;
                }


                if (e.Column.Name == COLS.PrimeCompanyRent  && e.Row.Cells[COLS.IsPrimeCompanyDriver].Value.ToBool() == false)
                {

                    e.Cancel = true;
                }


            }
               
         
        }


        private void RefreshRentView()
        {
            try
            {

                DateTime? dtFrom = dtpFromDate.Value.ToDate();
                DateTime? dtTill = dtpTillDate.Value.ToDateTimeorNull();


                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_AddAllDriverRent(dtFrom, dtTill).AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>()).ToList();


                    int cnt = list.Count;
                    grdDriverRent.RowCount = cnt;

                    for (int i = 0; i < cnt; i++)
                    {


                        grdDriverRent.Rows[i].Cells["Check"].Value = true;
                        grdDriverRent.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdDriverRent.Rows[i].Cells[COLS.DriverNo].Value = list[i].DriverNo;
                       
                        if (list[i].RentId == 0)
                            grdDriverRent.Rows[i].Cells[COLS.OldBalance].Value = list[i].InitialBalance;
                        else
                            grdDriverRent.Rows[i].Cells[COLS.OldBalance].Value = list[i].OldBalance;


                        grdDriverRent.Rows[i].Cells[COLS.InitialBalance].Value = list[i].InitialBalance;

                        grdDriverRent.Rows[i].Cells[COLS.IsHoliday].Value = false;

                  
                        grdDriverRent.Rows[i].Cells[COLS.RentId].Value = null;


                        grdDriverRent.Rows[i].Cells[COLS.UseCompanyVehicle].Value = list[i].UseCompanyVehicle;
                        grdDriverRent.Rows[i].Cells[COLS.CarInsuranceRent].Value = list[i].CarInsuranceRent;
                        grdDriverRent.Rows[i].Cells[COLS.CarRent].Value = list[i].CarRent;
                        grdDriverRent.Rows[i].Cells[COLS.IsPrimeCompanyDriver].Value = list[i].IsPrimeCompanyDriver;
                     
                        grdDriverRent.Rows[i].Cells[COLS.PrimeCompanyRent].Value = list[i].PrimeCompanyRent;
                     
                        
                        grdDriverRent.Rows[i].Cells[COLS.DriverRent].Value = list[i].DriverMonthlyRent.ToDecimal();


                        // ADD PDA RENT
                        grdDriverRent.Rows[i].Cells[COLS.DriverPDARent].Value = list[i].PDARent.ToDecimal();


                        //grdDriverRent.Rows[i].Cells[COLS.TotalDriverPDARent].Value = grdDriverRent.Rows[i].Cells[COLS.DriverPDARent].Value;
                        //grdDriverRent.Rows[i].Cells[COLS.TotalCarRent].Value = grdDriverRent.Rows[i].Cells[COLS.CarRent].Value;
                        //grdDriverRent.Rows[i].Cells[COLS.TotalCarInsuranceRent].Value = grdDriverRent.Rows[i].Cells[COLS.CarInsuranceRent].Value;
                        //grdDriverRent.Rows[i].Cells[COLS.TotalPrimeCompanyRent].Value = grdDriverRent.Rows[i].Cells[COLS.PrimeCompanyRent].Value;

                    //    grdDriverRent.Rows[i].Cells[COLS.AccountsTotal].Value = null;

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
            Generate();
        }
        private void Generate()
        {
            try
            {
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                List<DriverRent> listofRent = null;

                long transId = 0;
                decimal accJobsTotal = 0;
                decimal rentDue = 0;
                decimal currBalance = 0;

                bool IsSavedTrans = false;
                double totalWeeks = (dtpTillDate.Value.ToDate().Subtract(dtpFromDate.Value.ToDate()).TotalDays) / 7;

                if (totalWeeks <= 0)
                    totalWeeks = 1;
                
                foreach (var row in grdDriverRent.Rows.Where(c => c.Cells["Check"].Value.ToBool()))
                {

                    if (listofRent == null)
                    {
                        listofRent = General.GetQueryable<DriverRent>(c => c.FromDate == fromDate && c.ToDate == tillDate).ToList();

                    }


                    transId = 0;
                    accJobsTotal = 0;
                    rentDue = 0;


                    transId = listofRent.FirstOrDefault(c => c.DriverId == row.Cells["Id"].Value.ToInt()).DefaultIfEmpty().Id;


                    if (OnSave(row.Cells["Id"].Value.ToInt(), row.Cells[COLS.OldBalance].Value.ToDecimal(), row.Cells["DriverRent"].Value.ToDecimal(), row.Cells[COLS.DriverPDARent].Value.ToDecimal(), row.Cells["InitialBalance"].Value.ToDecimal(), 0.00m, ref accJobsTotal, ref transId, ref rentDue, ref currBalance,row.Cells[COLS.CarRent].Value.ToDecimal(),row.Cells[COLS.CarInsuranceRent].Value.ToDecimal(),row.Cells[COLS.PrimeCompanyRent].Value.ToDecimal(),totalWeeks,row.Cells[COLS.IsHoliday].Value.ToBool()))
                    {

                        IsSavedTrans = true;

                        row.Cells[COLS.RentId].Value = transId;

                   //     row.Cells[COLS.AccountsTotal].Value = accJobsTotal;
                    //    row.Cells[COLS.RentDue].Value = rentDue;
                   //     row.Cells[COLS.CurrBalance].Value = currBalance;
                    }

                }


                if (IsSavedTrans)
                {

                //    grdDriverRent.Columns[COLS.RentPay].ReadOnly = true;
                    btnPrintAll.Enabled = true;

                    btnViewPrint.Enabled = true;
                    btnDeleteGenerated.Visible = true;
                }


            }
            catch (Exception ex)
            {



            }

        }




        private void cbAllCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllCompany.Checked == true)
            {
                if (grdDriverRent.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDriverRent.Rows.Count; i++)
                    {
                        grdDriverRent.Rows[i].Cells["Check"].Value = true;//..CurrentCell.Value;
                    }
                }
            }
            else if (cbAllCompany.Checked == false)
            {
                if (grdDriverRent.Rows.Count > 0)
                {
                    for (int i = 0; i < grdDriverRent.Rows.Count; i++)
                    {
                        grdDriverRent.Rows[i].Cells["Check"].Value = false;//..CurrentCell.Value;

                    }
                }
            }
        }


       

        private bool OnSave(int DriverId, decimal oldBalance, decimal DriverRent, decimal pdaRent, decimal InitialBalance, decimal rentPayValue, ref decimal accJobs, ref long transId, ref decimal rentDue, ref decimal currBalance, decimal CarRent, decimal CarInsuranceRent, decimal PrimeCompanyRent,double TotalWeeks,bool IsHoliday)
        {

            bool IsSaved = false;

            // NC
          
          //  decimal Debit = 0.00m;
            decimal totalCredit = 0.00m;
            decimal totalDebit = 0.00m;
            decimal owedBalance = 0.00m;
            decimal Currentbalance = 0.00m;
            //
            try
            {

                long savedTransId = transId;

                if (savedTransId > 0)
                {
                    var query = General.GetObject<DriverRent>(c => c.Id == savedTransId);

                    if (query != null)
                    {
                        oldBalance = query.OldBalance.ToDecimal();
                    }
                }


                //long savedTransId = transId;


                //if (savedTransId > 0)
                //{


                //    var query = General.GetQueryable<DriverRent>(c => c.DriverId == DriverId).OrderByDescending(c => c.Id).FirstOrDefault();

                //    if (query != null)
                //    {
                //        if (savedTransId == query.Id)
                //            oldBalance = query.OldBalance.ToDecimal();
                //    }


                //}
               

              bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();
           


                var rentList = General.GetGeneralList<DriverRent_Charge>(c => c.BookingId != null && c.TransId != null);
               
                //var list2 = (from a in General.GetGeneralList<Booking>(c => c.CompanyId != null && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                var list2 = (from a in General.GetGeneralList<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED 
                    && (c.FareRate != null)
                    && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                                    && (c.DriverId == DriverId) &&
                                 (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date))
                             join b in rentList on a.Id equals b.BookingId into table2
                             from b in table2.DefaultIfEmpty()


                             where (savedTransId != 0 || b == null)
                             select new
                             {
                                 Id = a.Id,
                                 TotalFare = a.FareRate.ToDecimal()  + a.MeetAndGreetCharges.ToDecimal(),
                                 CompanyId = a.CompanyId != null ? a.CompanyId : 0,
                                 AccountTypeId = a.CompanyId != null ? a.Gen_Company.AccountTypeId : 0,
                                 DriverCommissionAmount = a.DriverCommission,
                                 DriverCommissionType = a.DriverCommissionType,
                                 IsCommissionWise = a.IsCommissionWise,
                                 AgentCommission = a.AgentCommission,
                                 DropOffCharge =0.00m,
                                 PickupCharge = 0.00m,
                                 PaymentTypeId = a.PaymentTypeId
                                
                             }).ToList();

                objMaster = new DriverRentBO();

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

                 

                    List<DriverRent_Charge> ListDetail = (from a in list2
                                                          select new DriverRent_Charge
                                                          {
                                                              //Id = a.Id,
                                                              TransId = objMaster.Current.Id,
                                                              BookingId = a.Id

                                                          }).ToList();

                    string[] skipProperties = { "DriverRent", "Booking" };
                    IList<DriverRent_Charge> savedList = objMaster.Current.DriverRent_Charges;

                    var list = objMaster.Current.DriverRent_Charges.ToList();

                    Utils.General.SyncChildCollection(ref savedList, ref ListDetail, "Id", skipProperties);



                    decimal Total = list2.Sum(c => c.TotalFare).ToDecimal();


                    decimal CashJobsTotal = list2.Where(c => c.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CASH)
                                           .Sum(c => c.TotalFare).ToDecimal();


                    decimal ACCJobsTotal = list2.Where(c => c.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.CASH)
                                           .Sum(c => c.TotalFare).ToDecimal();


                    decimal AgentCommission = list2.Where(c => c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH).Sum(c => c.AgentCommission).ToDecimal();

                    objMaster.Current.AgentTotal = AgentCommission.ToDecimal();

                    objMaster.Current.CashJobsTotal = CashJobsTotal;
                    objMaster.Current.AccountJobsTotal = ACCJobsTotal;

                    objMaster.Current.JobsTotal = Total;


                    decimal TotalpdaRent = (pdaRent);
                    decimal TotalCarRent = (CarRent);
                    decimal TotalCarInsuranceRent = (CarInsuranceRent);
                    decimal TotalPrimeCompanyRent = (PrimeCompanyRent);


                    //decimal TotalpdaRent = (pdaRent * TotalWeeks.ToInt());
                    //decimal TotalCarRent = (CarRent * TotalWeeks.ToInt());
                    //decimal TotalCarInsuranceRent = (CarInsuranceRent * TotalWeeks.ToInt());
                    //decimal TotalPrimeCompanyRent = (PrimeCompanyRent * TotalWeeks.ToInt());

                    objMaster.Current.PDARent = TotalpdaRent;
                    objMaster.Current.CarRent = TotalCarRent;
                    objMaster.Current.CarInsuranceRent = TotalCarInsuranceRent;
                    objMaster.Current.PrimeCompanyRent = TotalPrimeCompanyRent;
                    objMaster.Current.DriverRent1 = DriverRent;


                    decimal Debit = 0.00m;
                    decimal Credit = 0.00m;
                    if (savedTransId > 0 && objMaster.Current.Fleet_DriverRentExpenses.Count > 0)
                    {
                        Debit = objMaster.Current.Fleet_DriverRentExpenses.Where(c => c.Debit.ToDecimal() > 0).Select(c => c.Debit).Sum().ToDecimal();
                        Credit = objMaster.Current.Fleet_DriverRentExpenses.Where(c => c.Credit.ToDecimal() > 0).Select(c=>c.Credit).Sum().ToDecimal();
                    }


                    totalCredit = (ACCJobsTotal + Credit);

                    totalDebit = (DriverRent + TotalpdaRent + TotalCarRent + TotalCarInsuranceRent + TotalPrimeCompanyRent + AgentCommission+ Debit);
                    owedBalance = (totalDebit-totalCredit);


                    objMaster.Current.OldBalance = oldBalance;// (CurrentBalance - RentPay);
                    Currentbalance = owedBalance;
                    objMaster.Current.Balance = Currentbalance + oldBalance;

                    objMaster.Current.AccountExpenses = 0.00m;
                    objMaster.Current.RentPay = 0.00m;


                    objMaster.Current.FromDate = dtpFromDate.Value.ToDate();
                    objMaster.Current.ToDate = dtpTillDate.Value.ToDate();
                    objMaster.Current.TransFor = "Weekly";//ddlDayWise.SelectedText.ToStr();


                    objMaster.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();
                    objMaster.Current.AddLog = AppVars.LoginObj.LoginName.ToStr();
                    objMaster.Current.AddOn = DateTime.Now;
                    objMaster.Current.TransDate = DateTime.Now; //dtpTransactionDate.Value.ToDateTime();
                    objMaster.Current.DriverId = DriverId;//ddlDriver.SelectedValue.ToIntorNull();


                    objMaster.Current.Fuel = 0;//Fuel.ToDecimal();
                    objMaster.Current.Extra = 0;// Extra.ToDecimal();
                    objMaster.Current.IsHoliday = IsHoliday;

                    objMaster.Current.TotalJobs = list2.Count;

                    objMaster.Current.TransactionType = Enums.TRANSACTIONTYPE.DRIVER_RENT_EXPENSE2;
                    objMaster.Save();


                    IsSaved = true;
                    transId = objMaster.Current.Id;

             //   }
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
        public void ShowDriverRent(int Id)
        {
            frmDriverRentDebitCredit frm = new frmDriverRentDebitCredit();
            frm.OnDisplayRecord(Id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmfrmDriverRentDebitCredit21");

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
            if (grdDriverRent.CurrentRow != null && grdDriverRent.CurrentRow is GridViewDataRowInfo)
            {
                int RentId = grdDriverRent.CurrentRow.Cells["RentId"].Value.ToInt();
                if (RentId > 0)
                {
                    ShowDriverRent(RentId);
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

                var rows = grdDriverRent.Rows.Where(c => c.Cells[COLS.RentId].Value.ToLong() > 0).ToList();



                List<long> invoiceIds = rows.Select(c => c.Cells[COLS.RentId].Value.ToLong()).ToList<long>();

                if (invoiceIds.Count > 0)
                {

                    frmDriverTransactionExpensesReport frm = new frmDriverTransactionExpensesReport(2);


                    //frm.GenerateReport();

                    //frmDriverTransactionReport frm = new frmDriverTransactionReport(1);

                    //var list = General.GetQueryable<vu_DriverRent>(a => invoiceIds.Contains(a.Id)).ToList();

                    frm.CompanyHeader = ddlSubCompany.Text.Trim();

                    List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 1);

                    frmEmail frmEmail = new frmEmail(null, "", "");


                    foreach (var item in rows)
                    {
                        //frm.DataSource = list.Where(c => c.Id == item.Cells[COLS.RentId].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                        var list = General.GetQueryable<vu_DriverRentExpense>(a => a.Id == item.Cells[COLS.RentId].Value.ToLong()).ToList();
                        int count = list.Count;

                        frm.DataSource = list;
                        var list2 = General.GetQueryable<vu_FleetDriverRentExpense>(a => a.RentId == item.Cells[COLS.RentId].Value.ToLong()).ToList();
                        frm.DataSource2 = list2;


                        frm.GenerateReport();



                        string email = driversList.FirstOrDefault(c => c.Id == item.Cells[COLS.Id].Value.ToInt()).DefaultIfEmpty().Email.ToStr().Trim();



                        if (!string.IsNullOrEmpty(email))
                        {


                            frm.SendEmailInternally(frmEmail, subject, item.Cells[COLS.DriverNo].Value.ToStr().Trim(), email);
                        }
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
            try
            {


           

                decimal rentDue = 0;
                int DriverId = 0;
                decimal DriverRent = 0.00m;
                decimal pdaRent = 0.00m;

                decimal oldBalance = 0.00m;

                // NC
                decimal Credit = 0.00m;
                decimal Debit = 0.00m;
                decimal totalCredit = 0.00m;
                decimal totalDebit = 0.00m;
                decimal owedBalance = 0.00m;
                decimal Currentbalance = 0.00m;
                decimal CarRent = 0.00m;
                decimal CarInsuranceRent = 0.00m;
                decimal PrimeCompanyRent = 0.00m;
                //27/July/16

                double totalWeeks = (dtpTillDate.Value.ToDate().Subtract(dtpFromDate.Value.ToDate()).TotalDays) / 7;

                if (totalWeeks <= 0)
                    totalWeeks = 1;


                //
                //var list3 = General.GetQueryable<Gen_SysPolicy_AirportDropOffCharge>(null).ToList();
                //var list4 = General.GetQueryable<Gen_SysPolicy_AirportPickupCharge>(null).ToList();
                foreach (var row in grdDriverRent.Rows.Where(c => c.Cells["Check"].Value.ToBool()))
                {


                    DriverId = row.Cells["Id"].Value.ToInt();
                    DriverRent = row.Cells["DriverRent"].Value.ToDecimal();

                    // add pda rent
                    pdaRent = row.Cells[COLS.DriverPDARent].Value.ToDecimal();


                    oldBalance = row.Cells[COLS.OldBalance].Value.ToDecimal();
                    //27/July/16
                    CarRent = row.Cells[COLS.CarRent].Value.ToDecimal();
                    CarInsuranceRent = row.Cells[COLS.CarInsuranceRent].Value.ToDecimal();
                    PrimeCompanyRent = row.Cells[COLS.PrimeCompanyRent].Value.ToDecimal();
                    decimal TotalPDARent = (pdaRent );
                    decimal TotalCarRent = (CarRent);
                    decimal TotalCarInsuranceRent = (CarInsuranceRent );
                    decimal TotalPrimeCompanyRent = (PrimeCompanyRent );
                    //



                    rentDue = 0;


                    try
                    {


                        bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();

                        var rentList = General.GetGeneralList<DriverRent_Charge>(c => c.BookingId != null && c.TransId != null);
                        //var list2 = (from a in General.GetGeneralList<Booking>(c => c.CompanyId != null && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                        var list2 = (from a in General.GetGeneralList<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED

                            && (c.FareRate != null)
                            && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                                            && (c.DriverId == DriverId) &&
                                         (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date))
                                     join b in rentList on a.Id equals b.BookingId into table2
                                     from b in table2.DefaultIfEmpty()
                                   
                                     where (b == null)
                                     select new
                                     {
                                         Id = a.Id,
                                         TotalFare = a.FareRate.ToDecimal() + a.CongtionCharges.ToDecimal() + a.MeetAndGreetCharges.ToDecimal(),
                                         //  TotalCharges = a.TotalCharges,
                                         CompanyId = a.CompanyId !=null? a.CompanyId:0,
                                         AccountTypeId =a.CompanyId !=null? a.Gen_Company.AccountTypeId:0,
                                         DriverCommissionAmount = a.DriverCommission,
                                         DriverCommissionType = a.DriverCommissionType,
                                         IsCommissionWise = a.IsCommissionWise,
                                         DropOffCharge =0.00m,
                                         PickupCharge = 0.00m,
                                         PaymentTypeId = a.PaymentTypeId
                                     }).ToList();





                        //if (list2.Count == 0)
                        //    continue;





                        decimal Total = list2.Sum(c => c.TotalFare).ToDecimal();

                        //decimal ACCJobsTotal = list2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.ACCOUNT)
                        //                       .Sum(c => c.TotalFare).ToDecimal();

                        //decimal ACCCashJobsAmountTotal = list2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                        //                                && c.IsCommissionWise.ToBool() && c.DriverCommissionType.ToStr().Trim() == "Amount")
                        //                       .Sum(c => c.DriverCommissionAmount.ToDecimal()).ToDecimal();

                        //decimal ACCCashJobsPercentTotal = list2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                        //                            && c.IsCommissionWise.ToBool() && c.DriverCommissionType.ToStr().Trim() == "Percent")
                        //                   .Sum(c => (c.DriverCommissionAmount.ToDecimal() * 100) / c.TotalFare).ToDecimal();


                        decimal ACCJobsTotal = list2.Where(c =>c.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.CASH)
                                               .Sum(c => c.TotalFare).ToDecimal();

                        decimal ACCCashJobsAmountTotal = list2.Where(c => c.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CASH
                                                        && c.IsCommissionWise.ToBool() && c.DriverCommissionType.ToStr().Trim() == "Amount")
                                               .Sum(c => c.DriverCommissionAmount.ToDecimal()).ToDecimal();

                        decimal ACCCashJobsPercentTotal = list2.Where(c => c.PaymentTypeId.ToInt() != Enums.PAYMENT_TYPES.CASH
                                                    && c.IsCommissionWise.ToBool() && c.DriverCommissionType.ToStr().Trim() == "Percent")
                                           .Sum(c => (c.DriverCommissionAmount.ToDecimal() * 100) / c.TotalFare).ToDecimal();

                        decimal owed = ACCJobsTotal - (DriverRent + TotalPDARent + (ACCCashJobsAmountTotal + ACCCashJobsPercentTotal));

                        owed = owed + oldBalance;
                        rentDue = owed;

                        //NC

                        //txtBalance.Text = owed.ToStr();
                        //decimal owed = (DriverRent+pdaRent);
                        owed = (DriverRent + TotalPDARent + TotalCarRent + TotalCarInsuranceRent + TotalPrimeCompanyRent);
                        decimal AccountExpenses = list2.Sum(c => c.PickupCharge + c.DropOffCharge);
                        //owed = owed + OldBalance;
                        //owed = owed + OldBalance;
                        // decimal balance = owed + payment;
                        // txtBalance.Text = balance.ToStr();
                        // lblPay.Text = balance.ToStr();


                        totalCredit = (ACCJobsTotal + Credit.ToDecimal() + (AccountExpenses));
                        //totalCredit = (AccountjobTotal + Credit.ToDecimal() + (numAccountExpenses.Value.ToDecimal()));
                        // totalDebit = (numpdaRent.Value + txtCommsionTotal.Text.ToDecimal() + Debit);
                        totalDebit = (owed + Debit);
                        owedBalance = (totalCredit - totalDebit);
                        //txtDriverOwed.Text = owedBalance.ToStr();
                        Currentbalance = owedBalance + oldBalance;
                        //lblPay.Text = Currentbalance.ToStr();
                        //txtBalance.Text = Currentbalance.ToStr();

                        row.Cells[COLS.RentDue].Value = Currentbalance;
                        row.Cells[COLS.CurrBalance].Value = Currentbalance;
                        row.Cells[COLS.AccountExpenses].Value = AccountExpenses;
                        row.Cells[COLS.AccountsTotal].Value = ACCJobsTotal;
                        //


                        //Oc
                        //row.Cells[COLS.AccountsTotal].Value = ACCJobsTotal;
                        //row.Cells[COLS.RentDue].Value = rentDue;
                        //row.Cells[COLS.CurrBalance].Value = rentDue;
                        //
                        row.Cells[COLS.RentPay].Value = 0.00m;

                        row.Cells[COLS.RentId].Value = null;
                        //
                        row.Cells[COLS.TotalDriverPDARent].Value = TotalPDARent;
                        row.Cells[COLS.TotalCarRent].Value = TotalCarRent;
                        row.Cells[COLS.TotalCarInsuranceRent].Value = TotalCarInsuranceRent;
                        row.Cells[COLS.TotalPrimeCompanyRent].Value = TotalPrimeCompanyRent;
                        //


                        dtpFromDate.Enabled = false;
                        dtpTillDate.Enabled = false;

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

                    //
                }
            }
            catch (Exception ex)
            {



            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = true;
            dtpTillDate.Enabled = true;


        //    grdDriverRent.Columns[COLS.RentPay].ReadOnly = false;
            ClearData();
            btnViewPrint.Enabled = false;
            RefreshRentView();
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



                    var rows = grdDriverRent.Rows.Where(c => c.Cells[COLS.RentId].Value.ToLong() > 0).ToList();



                    List<long> invoiceIds = rows.Select(c => c.Cells[COLS.RentId].Value.ToLong()).ToList<long>();

                    if (invoiceIds.Count > 0)
                    {
                        //  frmDriverTransactionReport frm = new frmDriverTransactionReport(1);
                        frmDriverTransactionExpensesReport2 frm = new frmDriverTransactionExpensesReport2(2);
                        frm.CompanyHeader = ddlSubCompany.Text.Trim();

                        //var list = General.GetQueryable<vu_DriverRent>(a => invoiceIds.Contains(a.Id)).ToList();
                        var list = General.GetQueryable<vu_DriverRentExpense>(a => invoiceIds.Contains(a.Id)).ToList();
                        int count = list.Count;

                        frm.DataSource = list;
                        var list2 = General.GetQueryable<vu_FleetDriverRentExpense>(a => invoiceIds.Contains(a.Id)).ToList();
                        frm.DataSource2 = list2;

                        //List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 1);

                        frmEmail frmEmail = new frmEmail(null, "", "");


                        foreach (var item in rows)
                        {
                            frm.DataSource = list.Where(c => c.Id == item.Cells[COLS.RentId].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();
                            frm.DataSource2 = list2.Where(c => c.RentId == item.Cells[COLS.RentId].Value.ToLong()).ToList();

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

        private void btnViewPrint_Click(object sender, EventArgs e)
        {
            try
            {


                var rows = grdDriverRent.Rows.Where(c => c.Cells[COLS.RentId].Value.ToLong() > 0).ToList();

                var list = (from a in grdDriverRent.Rows.Where(c => c.Cells[COLS.RentId].Value.ToInt() > 0)
                            select new
                            {
                                RentId = a.Cells[COLS.RentId].Value.ToInt(),
                                DriverId = a.Cells[COLS.Id].Value.ToInt(),
                                Driver = a.Cells[COLS.DriverNo].Value.ToStr()
                            }).ToList();


                //  List<long> invoiceIds = rows.Select(c => c.Cells[COLS.CommissionId].Value.ToLong()).ToList<long>();

                if (list.Count > 0)
                {

                    frmDriverRentTransactionExpensesReport2 frm = new frmDriverRentTransactionExpensesReport2(list, dtpFromDate.Value.ToDate(), dtpTillDate.Value.ToDate());
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

                    string[] list = grdDriverRent.Rows.Where(c => c.Cells[COLS.RentId].Value.ToLong() > 0).Select(c => c.Cells[COLS.RentId].Value.ToStr())
                                            .ToArray<string>();

                    if (list.Count() > 0)
                    {
                        string arr = string.Join(",", list);

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_RunProcedure("delete from driverrent where Id in (" + arr + ")");

                        }

                    
                        ClearData();
                     
                    }
                }


            }
            catch (Exception ex)
            {


            }

        }

        private void ClearData()
        {
            grdDriverRent.Rows.ToList().ForEach(c => c.Cells[COLS.RentId].Value = 0);
            btnDeleteGenerated.Visible = false;
        }
    
    
    }


   


   
}
