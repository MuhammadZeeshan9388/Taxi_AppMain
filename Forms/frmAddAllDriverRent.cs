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
    public partial class frmAddAllDriverRent : UI.SetupBase
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
        
            public static string OldBalance = "OldBalance";
            public static string InitialBalance = "InitialBalance";

            public static string CurrBalance = "CurrBalance";
           
            public static string AccountsTotal = "AccountsTotal";
            public static string RentDue = "RentDue";
            public static string RentId = "RentId";
           
        }



        public frmAddAllDriverRent()
        {

            InitializeComponent();

            this.FormClosed += new FormClosedEventHandler(frmAddMultipleCompanyInvoice_FormClosed);
            this.grdDriverRent.CellEndEdit += new GridViewCellEventHandler(grdDriverRent_CellEndEdit);
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


            if (fromDate != null && toDate != null)
            {

                int subtracted = 7 - (int)fromDate.Value.DayOfWeek;

                int DaysToSubtract = (int)DateTime.Now.DayOfWeek;
                DateTime dtFrom = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
                // fromDate = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.Day - fromDate.Value.DayOfWeek.ToInt()+1, fromDate.Value.Hour, fromDate.Value.Minute, 0, 0);


                if (subtracted == 7)
                    subtracted = 6;

                fromDate = new DateTime(dtFrom.Year, dtFrom.Month, dtFrom.AddDays(-subtracted).Day, fromDate.Value.Hour, fromDate.Value.Minute, 0, 0);


                DateTime dtTo = DateTime.Now.Subtract(TimeSpan.FromDays(DaysToSubtract));
                toDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, dtTo.AddDays(toDate.Value.DayOfWeek.ToInt()).Day, toDate.Value.Hour, toDate.Value.Minute, 59, 999);


                dtpFromDate.Value = fromDate;
                dtpTillDate.Value = toDate;

            }
            else
            {

                dtpFromDate.Value = DateTime.Now.AddDays(-7).ToDate();
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
                tbcol.Width = 160;
                tbcol.ReadOnly = true;
                grdDriverRent.Columns.Add(tbcol);
                //tbcol = new GridViewTextBoxColumn();
                //tbcol.Name = COLS.TransNo;
                //tbcol.HeaderText = "Transaction No";
                //tbcol.Width = 110;
                //tbcol.ReadOnly = true;
                //grdDriverRent.Columns.Add(tbcol);
                //tbcol = new GridViewTextBoxColumn();
                GridViewDecimalColumn dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.DriverRent;
                dcol.Width = 70;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                dcol.HeaderText = "Rent";
                grdDriverRent.Columns.Add(dcol);


                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.DriverPDARent;
                dcol.Width = 70;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                dcol.HeaderText = "PDA Rent";
                grdDriverRent.Columns.Add(dcol);

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.OldBalance;
                dcol.Width = 100;
                dcol.DecimalPlaces = 2;
                dcol.HeaderText = "Old Balance";
                dcol.ReadOnly = true;
                grdDriverRent.Columns.Add(dcol);

                //tbcol = new GridViewTextBoxColumn();
                //GridViewDecimalColumn 

                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.InitialBalance;
                dcol.HeaderText = "Initial Balance";
                dcol.DecimalPlaces = 2;
                dcol.IsVisible = false;
                grdDriverRent.Columns.Add(dcol);
                
                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.AccountsTotal;
                dcol.HeaderText = "Accounts Total";
                dcol.Width = 110;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverRent.Columns.Add(dcol);


                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.RentPay;
                dcol.HeaderText = "Rent Pay";
                dcol.Width = 90;
                dcol.DecimalPlaces = 2;
                dcol.ReadOnly = false;
                grdDriverRent.Columns.Add(dcol);

               
                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.RentDue;
                dcol.HeaderText = "Rent Due";
                dcol.Width = 90;
                dcol.ReadOnly = true;
                dcol.DecimalPlaces = 2;
                grdDriverRent.Columns.Add(dcol);



                dcol = new GridViewDecimalColumn();
                dcol.Name = COLS.CurrBalance;
                dcol.HeaderText = "Current Balance";
                dcol.Width = 120;
                dcol.DecimalPlaces = 2;
                dcol.ReadOnly = true;
                grdDriverRent.Columns.Add(dcol);



                
                grdDriverRent.ShowRowHeaderColumn = false;


                objCondition.CellForeColor = Color.Red;
                objCondition.TValue1 = "x";
                objCondition.ConditionType = ConditionTypes.NotEqual;
                grdDriverRent.Columns[COLS.CurrBalance].ConditionalFormattingObjectList.Add(objCondition);



                objConditionGeneratedTrans.RowBackColor = Color.LightGreen;
                objConditionGeneratedTrans.TValue1 = "0";
                objConditionGeneratedTrans.ApplyToRow = true;
                objConditionGeneratedTrans.ConditionType = ConditionTypes.Greater;
                grdDriverRent.Columns[COLS.RentId].ConditionalFormattingObjectList.Add(objConditionGeneratedTrans);


                RefreshRentView();

            

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);  
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
                    var list = db.stp_AddAllDriverRent(dtFrom, dtTill).ToList();


                    int cnt = list.Count;
                    grdDriverRent.RowCount = cnt;




                    for (int i = 0; i < cnt; i++)
                    {


                        grdDriverRent.Rows[i].Cells["Check"].Value = true;
                        grdDriverRent.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdDriverRent.Rows[i].Cells[COLS.DriverNo].Value = list[i].DriverNo;

                        grdDriverRent.Rows[i].Cells[COLS.DriverRent].Value = list[i].DriverMonthlyRent.ToDecimal();


                        // ADD PDA RENT
                        grdDriverRent.Rows[i].Cells[COLS.DriverPDARent].Value = list[i].PDARent.ToDecimal();

                        if (list[i].RentId == 0)
                            grdDriverRent.Rows[i].Cells[COLS.OldBalance].Value = list[i].InitialBalance;
                        else
                            grdDriverRent.Rows[i].Cells[COLS.OldBalance].Value = list[i].OldBalance;


                        grdDriverRent.Rows[i].Cells[COLS.InitialBalance].Value = list[i].InitialBalance;


                        grdDriverRent.Rows[i].Cells[COLS.RentId].Value = null;
                        grdDriverRent.Rows[i].Cells[COLS.RentPay].Value = null;
                        grdDriverRent.Rows[i].Cells[COLS.RentDue].Value = null;
                        grdDriverRent.Rows[i].Cells[COLS.CurrBalance].Value = null;

                        grdDriverRent.Rows[i].Cells[COLS.AccountsTotal].Value = null;


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

                foreach (var row in grdDriverRent.Rows.Where(c=>c.Cells["Check"].Value.ToBool()))
	            {
                    transId = 0;
                    accJobsTotal = 0;
                    rentDue = 0;
                    if (OnSave(row.Cells["Id"].Value.ToInt(), row.Cells[COLS.OldBalance].Value.ToDecimal(), row.Cells["DriverRent"].Value.ToDecimal(),row.Cells[COLS.DriverPDARent].Value.ToDecimal(), row.Cells["InitialBalance"].Value.ToDecimal(),row.Cells[COLS.RentPay].Value.ToDecimal(), ref accJobsTotal, ref transId,ref rentDue ,ref currBalance))
                    {

                        IsSavedTrans = true;

                        row.Cells[COLS.RentId].Value = transId;

                        row.Cells[COLS.AccountsTotal].Value = accJobsTotal;
                        row.Cells[COLS.RentDue].Value = rentDue;
                        row.Cells[COLS.CurrBalance].Value = currBalance;
                    }             
                  
	            }


                if (IsSavedTrans)
                {

                    grdDriverRent.Columns[COLS.RentPay].ReadOnly = true;
                    btnPrintAll.Enabled = true;
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
        private bool OnSave(int DriverId, decimal oldBalance, decimal DriverRent,decimal pdaRent, decimal InitialBalance, decimal rentPayValue, ref decimal accJobs, ref long transId, ref decimal rentDue,ref decimal currBalance)
        {

            bool IsSaved = false;


            try
            {


                bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();

                 //var list = (from b in list1
                 //           join c in list2 on b.Id equals c.BookingId into table2
                 //           from c in table2.DefaultIfEmpty()
                 //           where (c == null)
                 //           select new


                var rentList = General.GetGeneralList<DriverRent_Charge>(c => c.BookingId != null && c.TransId != null);


                Expression<Func<Booking, bool>> _exp = null;
                if (AppVars.objPolicyConfiguration.PickBookingOnInvoicingType.ToInt() == 2)
                {

                    _exp = c => c.CompanyId != null && ((c.PaymentTypeId != Enums.PAYMENT_TYPES.CASH && c.BookingStatusId == Enums.BOOKINGSTATUS.NOPICKUP) || c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED)

                    && (c.FareRate != null)
                    && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                                    && (c.DriverId == DriverId) &&
                                 (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date);
                }
                else
                {
                    _exp = c => c.CompanyId != null && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED

               && (c.FareRate != null)
               && ((RentForProcessedJobs == true && (c.IsProcessed != null && c.IsProcessed == true)) || (RentForProcessedJobs == false && (c.IsProcessed == null || c.IsProcessed == false)))
                               && (c.DriverId == DriverId) &&
                            (c.PickupDateTime.Value.Date >= dtpFromDate.Value.Value.Date && c.PickupDateTime.Value.Date <= dtpTillDate.Value.Value.Date);

                }

                var list2 = (from a in General.GetGeneralList<Booking>(_exp)
                              join b in rentList on a.Id equals b.BookingId into table2
                              from b in table2.DefaultIfEmpty()
                              where (b==null)
                             select new
                             {
                                 Id = a.Id,
                                 TotalFare = a.FareRate.ToDecimal()+a.CongtionCharges.ToDecimal()+a.MeetAndGreetCharges.ToDecimal(),
                               //  TotalCharges = a.TotalCharges,
                                 CompanyId=a.CompanyId,
                                 AccountTypeId=a.Gen_Company.AccountTypeId,
                                 DriverCommissionAmount=a.DriverCommission,
                                 DriverCommissionType=a.DriverCommissionType,
                                 IsCommissionWise=a.IsCommissionWise
                             }).ToList();



                //if (list2.Count == 0)
                //    return false;

    
         



                objMaster=new DriverRentBO();
                objMaster.New();
               
            
             


                List<DriverRent_Charge> ListDetail = (from a in list2
                                                      select new DriverRent_Charge
                                                      {
                                                          //Id = a.Id,
                                                          TransId = objMaster.Current.Id,
                                                          BookingId = a.Id

                                                      }).ToList();



                decimal Total = list2.Sum(c => c.TotalFare).ToDecimal();

                decimal ACCJobsTotal = list2.Where(c=>c.CompanyId!=null && c.AccountTypeId.ToInt()==Enums.ACCOUNT_TYPE.ACCOUNT)
                                       .Sum(c => c.TotalFare).ToDecimal();

                decimal ACCCashJobsAmountTotal = list2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH 
                                                && c.IsCommissionWise.ToBool() && c.DriverCommissionType.ToStr().Trim()=="Amount")
                                       .Sum(c => c.DriverCommissionAmount.ToDecimal()).ToDecimal();

                decimal ACCCashJobsPercentTotal = list2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                                            && c.IsCommissionWise.ToBool() && c.DriverCommissionType.ToStr().Trim() == "Percent")
                                   .Sum(c => (c.DriverCommissionAmount.ToDecimal() * 100) / c.TotalFare).ToDecimal();



                objMaster.Current.JobsTotal = ACCJobsTotal;


                accJobs = ACCJobsTotal;

                decimal owed = ACCJobsTotal - (DriverRent +  pdaRent + (ACCCashJobsAmountTotal + ACCCashJobsPercentTotal));

                owed = owed + oldBalance;
                rentDue = owed;

               // decimal balance = owed;// +payment;

                currBalance = owed + rentPayValue.ToDecimal();


                objMaster.Current.RentPay = rentPayValue.ToDecimal();

                objMaster.Current.Balance = currBalance;//RentPay - DriverRent;
          
                objMaster.Current.DriverRent1 = DriverRent;


                // add pda rent
                objMaster.Current.PDARent = pdaRent;

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
                
         


                string[] skipProperties = { "DriverRent", "Booking" };
                IList<DriverRent_Charge> savedList = objMaster.Current.DriverRent_Charges;
               
                
                var list = objMaster.Current.DriverRent_Charges.ToList();

            



                Utils.General.SyncChildCollection(ref savedList, ref ListDetail, "Id", skipProperties);


                objMaster.Save();              


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
        public void ShowDriverRent(int Id)
        {
            frmDriverRent frm = new frmDriverRent();
            frm.OnDisplayRecord(Id);


            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmDriverRent1");

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
                    frmDriverTransactionReport frm = new frmDriverTransactionReport(1);

                    var list = General.GetQueryable<vu_DriverRent>(a => invoiceIds.Contains(a.Id)).ToList();

                    frm.CompanyHeader = ddlSubCompany.Text.Trim();

                    List<Fleet_Driver> driversList=General.GetGeneralList<Fleet_Driver>(c=>c.DriverTypeId==1);
              
                    frmEmail frmEmail = new frmEmail(null, "", "");
                   
                    
                    foreach (var item in rows)
                    {
                        frm.DataSource = list.Where(c => c.Id == item.Cells[COLS.RentId].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();

                   
                        frm.GenerateReport();



                        string email=driversList.FirstOrDefault(c=>c.Id==item.Cells[COLS.Id].Value.ToInt()).DefaultIfEmpty().Email.ToStr().Trim();

                                              

                        if(!string.IsNullOrEmpty(email))
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


              
            //    decimal accJobsTotal = 0;
                decimal rentDue = 0;
                int DriverId=0;
                decimal DriverRent=0.00m;
                decimal pdaRent = 0.00m;
                
                decimal oldBalance=0.00m;



                foreach (var row in grdDriverRent.Rows.Where(c => c.Cells["Check"].Value.ToBool()))
                {


                    DriverId = row.Cells["Id"].Value.ToInt();
                    DriverRent = row.Cells["DriverRent"].Value.ToDecimal();
                    
                    // add pda rent
                    pdaRent = row.Cells[COLS.DriverPDARent].Value.ToDecimal();


                    oldBalance = row.Cells[COLS.OldBalance].Value.ToDecimal();



                    rentDue = 0;


                    try
                    {


                        bool RentForProcessedJobs = AppVars.objPolicyConfiguration.RentForProcessedJobs.ToBool();

                        var rentList = General.GetGeneralList<DriverRent_Charge>(c => c.BookingId != null && c.TransId != null);

                        var list2 = (from a in General.GetGeneralList<Booking>(c => c.CompanyId != null && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED

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
                                         CompanyId = a.CompanyId,
                                         AccountTypeId = a.Gen_Company.AccountTypeId,
                                         DriverCommissionAmount = a.DriverCommission,
                                         DriverCommissionType = a.DriverCommissionType,
                                         IsCommissionWise = a.IsCommissionWise
                                     }).ToList();





                        //if (list2.Count == 0)
                        //    continue;





                        decimal Total = list2.Sum(c => c.TotalFare).ToDecimal();

                        decimal ACCJobsTotal = list2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.ACCOUNT)
                                               .Sum(c => c.TotalFare).ToDecimal();

                        decimal ACCCashJobsAmountTotal = list2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                                                        && c.IsCommissionWise.ToBool() && c.DriverCommissionType.ToStr().Trim() == "Amount")
                                               .Sum(c => c.DriverCommissionAmount.ToDecimal()).ToDecimal();

                        decimal ACCCashJobsPercentTotal = list2.Where(c => c.CompanyId != null && c.AccountTypeId.ToInt() == Enums.ACCOUNT_TYPE.CASH
                                                    && c.IsCommissionWise.ToBool() && c.DriverCommissionType.ToStr().Trim() == "Percent")
                                           .Sum(c => (c.DriverCommissionAmount.ToDecimal() * 100) / c.TotalFare).ToDecimal();



                        decimal owed = ACCJobsTotal - (DriverRent + pdaRent+ (ACCCashJobsAmountTotal + ACCCashJobsPercentTotal));

                        owed = owed + oldBalance;
                        rentDue = owed;





                        row.Cells[COLS.AccountsTotal].Value = ACCJobsTotal;
                        row.Cells[COLS.RentDue].Value = rentDue;
                        row.Cells[COLS.CurrBalance].Value = rentDue;

                        row.Cells[COLS.RentPay].Value = 0.00m;

                        row.Cells[COLS.RentId].Value = null;



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


            grdDriverRent.Columns[COLS.RentPay].ReadOnly = false;

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
                        frmDriverTransactionReport frm = new frmDriverTransactionReport(1);

                        frm.CompanyHeader = ddlSubCompany.Text.Trim();

                        var list = General.GetQueryable<vu_DriverRent>(a => invoiceIds.Contains(a.Id)).ToList();


                        List<Fleet_Driver> driversList = General.GetGeneralList<Fleet_Driver>(c => c.DriverTypeId == 1);

                        frmEmail frmEmail = new frmEmail(null, "", "");


                        foreach (var item in rows)
                        {
                            frm.DataSource = list.Where(c => c.Id == item.Cells[COLS.RentId].Value.ToLong()).OrderBy(c => c.PickupDate).ToList();


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
    }
}
