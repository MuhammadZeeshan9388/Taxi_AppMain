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
using System.Linq.Expressions;

namespace Taxi_AppMain
{
    public partial class frmAddAllDriverCommission : UI.SetupBase
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
           
            public static string AccountsTotal = "AccountsTotal";
            public static string Owed = "Owed";
            public static string CommissionId = "CommissionId";
            public static string CashTotal = "CashTotal";
            public static string JobsTotal = "JobsTotal";
            public static string AgentFees = "AgentFees";
            public static string DriverEmail = "DriverEmail";
        }



        public frmAddAllDriverCommission()
        {

            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(frmAddMultipleCompanyInvoice_FormClosed);
            this.grdDriverCommission.CellEndEdit += new GridViewCellEventHandler(grdDriverRent_CellEndEdit);
            this.KeyDown += new KeyEventHandler(frmAddAllDriverRent_KeyDown);
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


            int subtracted = 7 - (int)fromDate.Value.DayOfWeek;

            int DaysToSubtract = (int)DateTime.Now.DayOfWeek;
            DateTime dtFrom = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
           // fromDate = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day - fromDate.Value.DayOfWeek.ToInt()+1, fromDate.Value.Hour, fromDate.Value.Minute, 0, 0);

           // fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-subtracted).Day, fromDate.Value.Hour, fromDate.Value.Minute, 0, 0);
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
                dcol.Name = COLS.CommissionPay;
                dcol.HeaderText = "Comm: Pay";
                dcol.Width = 90;
                dcol.DecimalPlaces = 2;
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
            Generate();
        }
        private void Generate()
        {
            try
            {


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
                    if (OnSave(row.Cells["Id"].Value.ToInt(), row.Cells[COLS.OldBalance].Value.ToDecimal(), row.Cells[COLS.DriverCommissionPerBooking].Value.ToDecimal(),row.Cells[COLS.DriverPDARent].Value.ToDecimal(), row.Cells["InitialBalance"].Value.ToDecimal(),row.Cells[COLS.CommissionPay].Value.ToDecimal(), ref accJobsTotal, ref transId,ref rentDue ,ref currBalance))
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
        private bool OnSave(int DriverId, decimal oldBalance, decimal DriverCommissionPerBooking, decimal pdaCommission, decimal InitialBalance, decimal CommissionPayValue, ref decimal accJobs, ref long transId, ref decimal rentDue, ref decimal currBalance)
        {

            bool IsSaved = false;


            try
            {


                bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();
               

                var CommissionList = General.GetGeneralList<Fleet_DriverCommision_Charge>(c => c.BookingId != null && c.TransId != null);




                 Expression<Func<Booking, bool>> _exp = null;
                 if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2)
                 {
                    _exp= c => ((c.PaymentTypeId!=Enums.PAYMENT_TYPES.CASH && c.BookingStatusId==Enums.BOOKINGSTATUS.NOPICKUP) || c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)
                   
                    && (c.FareRate != null)
                    && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                                    && (c.DriverId == DriverId) &&
                                 (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date);


                 }
                 else
                 {

                     _exp = c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED

                    && (c.FareRate != null)
                    && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                                    && (c.DriverId == DriverId) &&
                                 (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date);


                 }





                var list2 = (from a in General.GetGeneralList<Booking>(_exp)
                             join b in CommissionList on a.Id equals b.BookingId into table2
                              from b in table2.DefaultIfEmpty()
                              where (b==null)
                             select new
                             {
                                 Id = a.Id,
                                 TotalFare = a.FareRate.ToDecimal(),
                                 CompanyId=a.CompanyId,
                                 AccountTypeId = a.Gen_Company != null ? a.Gen_Company.AccountTypeId : a.PaymentTypeId,
                                 DriverCommissionAmount=a.DriverCommission,
                                 DriverCommissionType=a.DriverCommissionType,
                                 IsCommissionWise=a.IsCommissionWise,
                                 AgentCommission = a.AgentCommission
                             }).ToList();


                if (list2.Count > 0)
                {

                    objMaster = new DriverCommisionBO();
                    objMaster.New();

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
                    decimal ACCJobsTotal = list2.Where(c => c.CompanyId != null).Sum(c => c.TotalFare).ToDecimal();

                    double totalWeeks = (dtpTillDate.Value.ToDate().Subtract(dtpFromDate.Value.ToDate()).TotalDays) / 7;

                    if (totalWeeks <= 0)
                        totalWeeks = 1;

                    decimal TotalPDARent = (pdaCommission * totalWeeks.ToInt());

                    decimal AgentCommission = list2.Sum(c => c.AgentCommission).ToDecimal();
                    decimal DriverCommissionTotal = 0;
                    DriverCommissionTotal = ((Total - AgentCommission) * DriverCommissionPerBooking / 100);
                    decimal owed = ((AgentCommission + TotalPDARent + DriverCommissionTotal) - ACCJobsTotal);

                    rentDue = owed;


                    objMaster.Current.AccJobsTotal = ACCJobsTotal;
                    objMaster.Current.DriverOwed = rentDue;
                    objMaster.Current.CommissionTotal = DriverCommissionTotal;
                    objMaster.Current.JobsTotal = Total;
                    objMaster.Current.AgentFeesTotal = AgentCommission;


                    objMaster.Current.CommisionPay = CommissionPayValue.ToDecimal();

                    objMaster.Current.Balance = (rentDue + oldBalance);//RentPay - DriverRent;

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
                    objMaster.Current.Extra = 0;// Extra.ToDecimal();


                    string[] skipProperties = { "Fleet_DriverCommision", "Booking" };
                    IList<Fleet_DriverCommision_Charge> savedList = objMaster.Current.Fleet_DriverCommision_Charges;

                    var list = objMaster.Current.Fleet_DriverCommision_Charges.ToList();


                    Utils.General.SyncChildCollection(ref savedList, ref ListDetail, "Id", skipProperties);


                    objMaster.Save();

                    currBalance = objMaster.Current.Balance.ToDecimal();
                    IsSaved = true;
                    transId = objMaster.Current.Id;

                }
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
            frmDriverCommision frm = new frmDriverCommision();
           
            frm.OnDisplayRecord(Id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverCommision1");

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
                    frmDriverCommisionTransactionReport frm = new frmDriverCommisionTransactionReport(1);
                    var list = General.GetQueryable<vu_DriverCommision>(a => invoiceIds.Contains(a.Id)).ToList();

                   // frm.CompanyHeader = ddlSubCompany.Text.Trim();

                    List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 2);

                    frmEmail frmEmail = new frmEmail(null, "", "");


                    foreach (var item in rows.Where(c => c.Cells["Check"].Value.ToBool()))
                    {
                        frm.DataSource = list.Where(c => c.Id == item.Cells[COLS.CommissionId].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();


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
            try
            {
                decimal CommissionDue = 0;
                int DriverId=0;
                decimal DriverCommissionPerBooking=0.00m;
                decimal pdaRent = 0.00m;   
                decimal oldBalance=0.00m;
                decimal AgentCommission=0;
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
                        var list2 = (from a in General.GetGeneralList<Booking>(c =>c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED

                            && (c.FareRate != null)
                            && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                                            && (c.DriverId == DriverId)
                                        && (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date))
                                     join b in commissionList on a.Id equals b.BookingId into table2
                                     from b in table2.DefaultIfEmpty()
                                     where (b == null)
                                     select new
                                     {
                                         Id = a.Id,
                                         TotalFare = a.FareRate.ToDecimal() ,
                                         //  TotalCharges = a.TotalCharges,

                                         CompanyId = a.CompanyId,
                                         AccountTypeId =a.Gen_Company != null ?   a.Gen_Company.AccountTypeId : a.PaymentTypeId, //1,//a.Gen_Company.AccountTypeId,
                                         DriverCommissionAmount = a.DriverCommission,
                                         DriverCommissionType = a.DriverCommissionType,
                                         IsCommissionWise = a.IsCommissionWise,
                                         AgentCommission=a.AgentCommission
                                     }).ToList();
                                   


                        oldBalance = row.Cells[COLS.OldBalance].Value.ToDecimal();

                        decimal Total = list2.Sum(c => c.TotalFare).ToDecimal();                     
                        decimal ACCJobsTotal = list2.Where(c => c.CompanyId != null ).Sum(c => c.TotalFare).ToDecimal();                     
                        decimal CashJobsTotal = list2.Where(c => c.CompanyId == null ).Sum(c => c.TotalFare).ToDecimal();                      
                        AgentCommission = list2.Sum(c => c.AgentCommission).ToDecimal();
                       
                        decimal JobsTotal = (ACCJobsTotal + CashJobsTotal);

                        DriverCommissionPerBooking = ((JobsTotal -AgentCommission) * row.Cells[COLS.DriverCommissionPerBooking].Value.ToDecimal() / 100);
                      
                        decimal owed = ((AgentCommission + TotalPDARent + DriverCommissionPerBooking) - ACCJobsTotal);
                
                        CommissionDue = owed;
             
                        row.Cells[COLS.CashTotal].Value = CashJobsTotal;
                        row.Cells[COLS.JobsTotal].Value=(JobsTotal);
                        row.Cells[COLS.AccountsTotal].Value = ACCJobsTotal;
                        row.Cells[COLS.Owed].Value = CommissionDue;
                        row.Cells[COLS.CurrBalance].Value =(CommissionDue+oldBalance);

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
                    }                   
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
                        frmDriverCommisionTransactionReport frm = new frmDriverCommisionTransactionReport(1);

                        frm.CompanyHeader = ddlSubCompany.Text.Trim();

                        var list = General.GetQueryable<vu_DriverCommision>(a => invoiceIds.Contains(a.Id)).ToList();


                        List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 2);

                        frmEmail frmEmail = new frmEmail(null, "", "");


                        foreach (var item in rows)
                        {
                            frm.DataSource = list.Where(c => c.Id == item.Cells[COLS.CommissionId].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();


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

        private void radButton1_Click(object sender, EventArgs e)
        {
            frmAddAllDriverCommissionExpenses frm = new frmAddAllDriverCommissionExpenses();
            this.Hide();
            frm.Show();
        }
    }
}
