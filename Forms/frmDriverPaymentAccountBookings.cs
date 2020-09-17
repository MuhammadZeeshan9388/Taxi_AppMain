using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using Taxi_BLL;
using DAL;
using Utils;
using Telerik.WinControls;
using Telerik.Data;
using Taxi_AppMain.Classes;
using Telerik.WinControls.UI.Export;
using Telerik.WinControls.UI.Docking;
using UI;

namespace Taxi_AppMain
{
    public partial class frmDriverPaymentAccountBookings : UI.SetupBase
    {
        DriverWeeklyRentHistoryBO objDriverWeeklyRentHistory;
        public frmDriverPaymentAccountBookings()
        {
            InitializeComponent();
            objDriverWeeklyRentHistory = new DriverWeeklyRentHistoryBO();
            this.SetProperties((INavigation)objDriverWeeklyRentHistory);

            this.Load += new EventHandler(frmPaymentCollection_Load);
            this.grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.btnExportExcel.Click += new EventHandler(btnExportExcel_Click);
            this.grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);


            btnPrint.Visible = false;

            grdLister.AllowAddNewRow = false;
            grdLister.AllowEditRow = true;
            grdLister.ShowGroupPanel = false;
            grdLister.EnableHotTracking = false;
            grdLister.EnableFiltering = true;


            optCommission.CheckedChanged += new EventHandler(optCommission_CheckedChanged);
            this.btnUpdateAll.Click += new EventHandler(btnUpdateAll_Click);

            btnShowRent.Click += new EventHandler(btnShowRent_Click);
         
        }

      

        void btnShowRent_Click(object sender, EventArgs e)
        {
            PopulateData();

        }

        void btnUpdateAll_Click(object sender, EventArgs e)
        {
            UpdatePaymentCollection();
        }

        void optCommission_CheckedChanged(object sender, EventArgs e)
        {

            if (optRent.Checked)
            {
                numrent.Visible = true;
                numrent.Value = AppVars.objPolicyConfiguration.DriverMonthlyRent.ToDecimal();
                btnShowRent.Visible = true;
            }
            else
            {
                numrent.Value = 0.00m;
                numrent.Visible = false;
                btnShowRent.Visible = false;
                PopulateData();
            }

           


        }
        private void UpdatePaymentCollection()
        {
            try
            {
                string Error = string.Empty;
                if (grdLister.Rows.Count() > 0 && grdLister.Rows.Count(c => c.Cells["IsPaid"].Value.ToBool() == true) == 0)
                {
                    Error = "Please select record to update";
                }

                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                if (grdLister.Rows.Count(c => c.Cells["IsPaid"].Value.ToBool() == true) > 0)
                {
                    foreach (var item in grdLister.Rows.Where(c => c.Cells["IsPaid"].Value.ToBool() == true))
                    {
                        long TranId = item.Cells["TransId"].Value.ToLong();


                        if (TranId > 0)
                        {

                            try
                            {

                                int IsPaid = item.Cells["IsPaid"].Value.ToBool() == true ? 1 : 0;
                                // decimal adjustments = row.Cells[COLS.Adjustment].Value.ToDecimal();

                                string query = "update fleet_drivercommision set IsWeeklyPaid=" + IsPaid + " where Id=" + TranId;

                                using (TaxiDataContext db = new TaxiDataContext())
                                {
                                    db.stp_RunProcedure(query);

                                }


                                UpdateDriverCommission(item.Cells["TotalRentCommission"].Value.ToDecimal());

                                //PopulateData();
                                DateTime? dtFrom = item.Cells["FromDate"].Value.ToDateorNull();
                                DateTime? dtTill = item.Cells["ToDate"].Value.ToDateorNull();

                                string DateRange = string.Format("{0:dd/MMMM/yyyy}", dtFrom) + " to " + string.Format("{0:dd/MMMM/yyyy}", dtTill);
                                int DriverId = item.Cells["Id"].Value.ToInt();
                                int Id = 0;

                                objDriverWeeklyRentHistory = new DriverWeeklyRentHistoryBO();

                                var MasterQuery = General.GetObject<Fleet_DriverWeeklyRentHistory>(c => (c.FromDate == dtFrom && c.TillDate == dtTill));
                                if (MasterQuery != null)
                                {
                                    Id = MasterQuery.Id;
                                    objDriverWeeklyRentHistory.GetByPrimaryKey(Id);
                                    objDriverWeeklyRentHistory.Edit();
                                    objDriverWeeklyRentHistory.Current.EditBy = AppVars.LoginObj.LuserId.ToInt();
                                    objDriverWeeklyRentHistory.Current.EditLog = AppVars.LoginObj.UserName;
                                    objDriverWeeklyRentHistory.Current.EditOn = DateTime.Now;
                                }
                                else
                                {
                                    objDriverWeeklyRentHistory.New();
                                    objDriverWeeklyRentHistory.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();
                                    objDriverWeeklyRentHistory.Current.AddLog = AppVars.LoginObj.UserName;
                                    objDriverWeeklyRentHistory.Current.AddOn = DateTime.Now;

                                }
                                objDriverWeeklyRentHistory.Current.DateRange = DateRange;
                                objDriverWeeklyRentHistory.Current.FromDate = dtFrom;
                                objDriverWeeklyRentHistory.Current.TillDate = dtTill;
                                objDriverWeeklyRentHistory.Current.CreatedOn = DateTime.Now;

                                if (objDriverWeeklyRentHistory.Current.Fleet_DriverWeeklyRentHistory_Details.Count(c => c.DriverId == DriverId) == 0)
                                {
                                    objDriverWeeklyRentHistory.Current.Fleet_DriverWeeklyRentHistory_Details.Add(new Fleet_DriverWeeklyRentHistory_Detail
                                    {
                                        TransId = objDriverWeeklyRentHistory.PrimaryKeyValue.ToInt(),
                                        AccountBookings = item.Cells["AccountBookings"].Value.ToDecimal(),
                                        Active = item.Cells["Active"].Value.ToStr(),
                                        DriverNo = item.Cells["DriverNo"].Value.ToStr(),
                                        DriverId = DriverId,
                                        RentComm = item.Cells["TotalRentCommission"].Value.ToDecimal(),
                                        IsPaid = item.Cells["IsPaid"].Value.ToBool(),
                                        OfficeToPay = item.Cells["OfficeToPay"].Value.ToDecimal(),
                                        PreviousBalance = item.Cells["PreviousBalance"].Value.ToDecimal(),
                                        DriverToPay = item.Cells["DriverToPay"].Value.ToDecimal()
                                    });
                                }
                                else
                                {

                                    Fleet_DriverWeeklyRentHistory_Detail objDriverWeeklyRentDetail = objDriverWeeklyRentDetail = objDriverWeeklyRentHistory.Current.Fleet_DriverWeeklyRentHistory_Details.FirstOrDefault(c => c.DriverId == DriverId);
                                    objDriverWeeklyRentDetail.TransId = objDriverWeeklyRentHistory.PrimaryKeyValue.ToInt();
                                    objDriverWeeklyRentDetail.IsPaid = item.Cells["IsPaid"].Value.ToBool(); ;
                                    objDriverWeeklyRentDetail.AccountBookings = item.Cells["AccountBookings"].Value.ToDecimal();
                                    objDriverWeeklyRentDetail.Active = item.Cells["Active"].Value.ToStr();
                                    objDriverWeeklyRentDetail.DriverNo = item.Cells["DriverNo"].Value.ToStr();
                                    objDriverWeeklyRentDetail.DriverId = DriverId;
                                    objDriverWeeklyRentDetail.RentComm = item.Cells["TotalRentCommission"].Value.ToDecimal();
                                    objDriverWeeklyRentDetail.OfficeToPay = item.Cells["OfficeToPay"].Value.ToDecimal();
                                    objDriverWeeklyRentDetail.PreviousBalance = item.Cells["PreviousBalance"].Value.ToDecimal();
                                    objDriverWeeklyRentDetail.DriverToPay = item.Cells["DriverToPay"].Value.ToDecimal();
                                    
                                }
                                objDriverWeeklyRentHistory.Save();
                                objDriverWeeklyRentHistory.Clear();
                            }
                            catch (Exception ex)
                            {

                                ENUtils.ShowMessage(ex.Message);

                            }
                        }
                    }
                }
                PopulateData();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void UpdateDriverCommission(decimal rentLimit)
        {
            try
            {

                long? updateCommissionId = null;
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    updateCommissionId = db.Fleet_DriverCommision_Charges.OrderByDescending(c=>c.Id).FirstOrDefault().DefaultIfEmpty().TransId;


                    if (updateCommissionId.ToLong() > 0  )
                    {
                        Fleet_DriverCommision objCommission = db.Fleet_DriverCommisions.FirstOrDefault(c => c.Id == updateCommissionId);


                        if (objCommission != null && objCommission.CommissionTotal >rentLimit)
                        {

                            decimal Credit = 0.00m;
                            decimal Debit = 0.00m;
                            decimal totalCredit = 0.00m;
                            decimal totalDebit = 0.00m;
                            decimal owedBalance = 0.00m;
                            decimal Currentbalance = 0.00m;


                            decimal pdaRent = objCommission.PDARent.ToDecimal();
                            decimal collectionAndDelivery = objCommission.CollectionDeliveryCharges.ToDecimal();


                            decimal accountJobsTotal = objCommission.Fleet_DriverCommision_Charges.Where(c => c.Booking.PaymentTypeId != Enums.PAYMENT_TYPES.CASH).Sum(c => (c.Booking.FareRate + c.Booking.MeetAndGreetCharges)).ToDecimal();
                            decimal jobsFareTotal = objCommission.Fleet_DriverCommision_Charges.Sum(c => (c.Booking.FareRate + c.Booking.MeetAndGreetCharges)).ToDecimal();

                            decimal agentTotal = objCommission.Fleet_DriverCommision_Charges.Where(c => c.Booking.PaymentTypeId == Enums.PAYMENT_TYPES.CASH).Sum(c => c.Booking.AgentCommission).ToDecimal();

                          //  decimal commissionTotal = objCommission.Fleet_DriverCommision_Charges.Sum(c => ((c.Booking.FareRate + c.Booking.MeetAndGreetCharges) * objCommission.DriverCommision) / 100).ToDecimal();
                            decimal commissionTotal = rentLimit;

                            decimal owed = Math.Round((pdaRent + agentTotal + commissionTotal) - accountJobsTotal, 2);

                            if (objCommission.Fleet_DriverCommissionExpenses.Count > 0)
                            {
                                Debit = objCommission.Fleet_DriverCommissionExpenses.Sum(c => c.Debit).ToDecimal();
                                Credit = objCommission.Fleet_DriverCommissionExpenses.Sum(c => c.Credit).ToDecimal();
                            }


                            totalCredit = (commissionTotal + Credit);
                            totalDebit = (accountJobsTotal + Debit);

                            owedBalance = (totalCredit - totalDebit);
                            Currentbalance = owedBalance;

                            objCommission.CommissionTotal = commissionTotal;
                            objCommission.AccJobsTotal = accountJobsTotal;
                            objCommission.Balance = Currentbalance;
                            objCommission.AgentFeesTotal = agentTotal;

                            objCommission.JobsTotal = jobsFareTotal;
                            objCommission.DriverOwed = owed;

                            db.SubmitChanges();
                        }

                    }

                }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

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
                    long TranId = row.Cells["TransId"].Value.ToLong();


                    if (TranId > 0)
                    {

                        try
                        {

                            int IsPaid = row.Cells["IsPaid"].Value.ToBool() == true ? 1 : 0;
                            // decimal adjustments = row.Cells[COLS.Adjustment].Value.ToDecimal();

                            string query = "update fleet_drivercommision set IsWeeklyPaid=" + IsPaid + " where Id=" + TranId;

                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                db.stp_RunProcedure(query);

                            }


                            UpdateDriverCommission(grdLister.CurrentRow.Cells["TotalRentCommission"].Value.ToDecimal());


                            PopulateData();
                            DateTime? dtFrom = grdLister.CurrentRow.Cells["FromDate"].Value.ToDateorNull();
                            DateTime? dtTill = grdLister.CurrentRow.Cells["ToDate"].Value.ToDateorNull();
                            
                            string DateRange = string.Format("{0:dd/MMMM/yyyy}", dtFrom) + " to " + string.Format("{0:dd/MMMM/yyyy}", dtTill);
                            int DriverId = grdLister.CurrentRow.Cells["Id"].Value.ToInt();
                            int Id = 0;

                            objDriverWeeklyRentHistory=new DriverWeeklyRentHistoryBO();

                            var MasterQuery = General.GetObject<Fleet_DriverWeeklyRentHistory>(c => (c.FromDate == dtFrom && c.TillDate == dtTill));
                            if (MasterQuery != null)
                            {
                                Id = MasterQuery.Id;
                                objDriverWeeklyRentHistory.GetByPrimaryKey(Id);
                                objDriverWeeklyRentHistory.Edit();
                                objDriverWeeklyRentHistory.Current.EditBy = AppVars.LoginObj.LuserId.ToInt();
                                objDriverWeeklyRentHistory.Current.EditLog = AppVars.LoginObj.UserName;
                                objDriverWeeklyRentHistory.Current.EditOn = DateTime.Now;
                            }
                            else
                            {
                                objDriverWeeklyRentHistory.New();
                                objDriverWeeklyRentHistory.Current.AddBy = AppVars.LoginObj.LuserId.ToInt();
                                objDriverWeeklyRentHistory.Current.AddLog = AppVars.LoginObj.UserName;
                                objDriverWeeklyRentHistory.Current.AddOn = DateTime.Now;

                            }
                            objDriverWeeklyRentHistory.Current.DateRange = DateRange;
                            objDriverWeeklyRentHistory.Current.FromDate = dtFrom;
                            objDriverWeeklyRentHistory.Current.TillDate = dtTill;
                            objDriverWeeklyRentHistory.Current.CreatedOn = DateTime.Now;




                            if (objDriverWeeklyRentHistory.Current.Fleet_DriverWeeklyRentHistory_Details.Count(c => c.DriverId == DriverId) == 0)
                            {
                                objDriverWeeklyRentHistory.Current.Fleet_DriverWeeklyRentHistory_Details.Add(new Fleet_DriverWeeklyRentHistory_Detail
                                {
                                    TransId = objDriverWeeklyRentHistory.PrimaryKeyValue.ToInt(),
                                    AccountBookings = grdLister.CurrentRow.Cells["AccountBookings"].Value.ToDecimal(),
                                    Active = grdLister.CurrentRow.Cells["Active"].Value.ToStr(),
                                    DriverNo = grdLister.CurrentRow.Cells["DriverNo"].Value.ToStr(),
                                    DriverId = DriverId,
                                    RentComm = grdLister.CurrentRow.Cells["TotalRentCommission"].Value.ToDecimal(),
                                    IsPaid = grdLister.CurrentRow.Cells["IsPaid"].Value.ToBool(),
                                    OfficeToPay = grdLister.CurrentRow.Cells["OfficeToPay"].Value.ToDecimal(),
                                    PreviousBalance = grdLister.CurrentRow.Cells["PreviousBalance"].Value.ToDecimal(),
                                    DriverToPay = grdLister.CurrentRow.Cells["DriverToPay"].Value.ToDecimal()
                                 });
                            }
                            else
                            {

                                Fleet_DriverWeeklyRentHistory_Detail objDriverWeeklyRentDetail=  objDriverWeeklyRentDetail=objDriverWeeklyRentHistory.Current.Fleet_DriverWeeklyRentHistory_Details.FirstOrDefault(c => c.DriverId == DriverId) ;





                                //var query2 = General.GetObject<Fleet_DriverWeeklyRentHistory_Detail>(c => (c.TransId == objDriverWeeklyRentHistory.PrimaryKeyValue.ToInt() && c.DriverId == DriverId));
                                //if (query2 != null)
                                //{
                                //    int HistoryId = query2.Id;
                                //    using (TaxiDataContext db = new TaxiDataContext())
                                //    {
                                     //   Fleet_DriverWeeklyRentHistory_Detail objDriverWeeklyRentDetail = db.Fleet_DriverWeeklyRentHistory_Details.FirstOrDefault(c => c.Id == HistoryId);
                                        objDriverWeeklyRentDetail.TransId = objDriverWeeklyRentHistory.PrimaryKeyValue.ToInt();
                                        objDriverWeeklyRentDetail.IsPaid = grdLister.CurrentRow.Cells["IsPaid"].Value.ToBool(); ;
                                        objDriverWeeklyRentDetail.AccountBookings = grdLister.CurrentRow.Cells["AccountBookings"].Value.ToDecimal();
                                        objDriverWeeklyRentDetail.Active = grdLister.CurrentRow.Cells["Active"].Value.ToStr();
                                        objDriverWeeklyRentDetail.DriverNo = grdLister.CurrentRow.Cells["DriverNo"].Value.ToStr();
                                        objDriverWeeklyRentDetail.DriverId = DriverId;
                                        objDriverWeeklyRentDetail.RentComm = grdLister.CurrentRow.Cells["TotalRentCommission"].Value.ToDecimal();
                                        objDriverWeeklyRentDetail.OfficeToPay = grdLister.CurrentRow.Cells["OfficeToPay"].Value.ToDecimal();
                                        objDriverWeeklyRentDetail.PreviousBalance = grdLister.CurrentRow.Cells["PreviousBalance"].Value.ToDecimal();
                                        objDriverWeeklyRentDetail.DriverToPay = grdLister.CurrentRow.Cells["DriverToPay"].Value.ToDecimal();
                                   //     db.SubmitChanges();
                            //        }
                           //     }
                             
                            }
                            objDriverWeeklyRentHistory.Save();
                            objDriverWeeklyRentHistory.Clear();
                        }
                        catch (Exception ex)
                        {

                            ENUtils.ShowMessage(ex.Message);

                        }
                    }
                }
            }

        }

        void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (grdLister.Rows.Count == 0)
                return;

            try
            {


                saveFileDialog1.Filter = "Excel File (*.xls)|*.xls|AdvExcel File (*.xlsx)|*.xlsx";

                saveFileDialog1.Title = "Save File";
                saveFileDialog1.FileName = "Drivers Weekly Rent Sheet";


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    grdLister.Columns["IsPaid"].IsVisible = false;
                    grdLister.Columns["Paid"].IsVisible = true;
                    grdLister.Columns["btnUpdate"].IsVisible = false;

                    grdLister.Columns["Active"].Width = 35;
                    grdLister.Columns["Active"].HeaderText = "Active";

                    grdLister.Columns["DriverNo"].Width = 38;
                    grdLister.Columns["DriverNo"].HeaderText = "Driver";

                    grdLister.Columns["AccountBookings"].Width = 70;
                    grdLister.Columns["AccountBookings"].HeaderText = "A/C Bookings";

                    grdLister.Columns["TotalRentCommission"].Width = 60;

                    grdLister.Columns["OfficeToPay"].Width = 70;
                    grdLister.Columns["PreviousBalance"].Width = 65;

                    //   grdLister.Columns["Total"].Width = 80;
                    grdLister.Columns["DriverToPay"].Width = 70;
                    grdLister.Columns["Paid"].Width = 30;


                    var row = grdLister.Rows.OrderByDescending(c => c.Cells["ToDate"].Value.ToDate()).FirstOrDefault();

                    DateTime? dtCurrent = row.Cells["FromDate"].Value.ToDate();
                    DateTime dtEnd = row.Cells["ToDate"].Value.ToDate();


                    Microsoft.Reporting.WinForms.ReportParameter[] param = new Microsoft.Reporting.WinForms.ReportParameter[1];
                    string heading = string.Empty;
                    heading = "Drivers Weekly Rent Sheet - " + string.Format("from {0:dd/MM/yyyy}", dtCurrent) + " until " + string.Format("{0:dd/MM/yyyy}", dtEnd);

                    ClsExportGridView obj = new ClsExportGridView(grdLister, saveFileDialog1.FileName);
                    obj.ApplyCellFormatting = true;
                    obj.ConditionalFormattingObject = new StyleDataRowConditionalFormattingObject();
                    obj.ConditionalFormattingObject.ConditionFormattingColumnName = "Paid";
                    obj.ConditionalFormattingObject.RowBackColor = Color.LightGreen;
                    obj.ConditionalFormattingObject.RowForeColor = Color.Black;
                    obj.ConditionalFormattingObject.TValue = "Paid";




                    obj.Heading = heading;
                    if (obj.ExportExcel())
                    {
                        RadDesktopAlert alert = new RadDesktopAlert();
                        alert.CaptionText = "Export";
                        alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>Export Successfully</span></b></html>";
                        alert.Show();
                    }


                    SetDefaultColumnSettings();
                }

            }
            catch (Exception ex)
            {
                SetDefaultColumnSettings();
                ENUtils.ShowMessage(ex.Message);

            }

        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            rptfrmDriverPaymentAccountBookings frm = new rptfrmDriverPaymentAccountBookings();
            DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmDriverPaymentAccountBookings1");
            if (doc != null)
            {
                doc.Close();
            }
            MainMenuForm.MainMenuFrm.ShowForm(frm);
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
            if (e.CellElement.RowInfo is GridViewDataRowInfo)
            {
                if (e.Column.Name == "TotalRentCommission" && e.CellElement.Value.ToDecimal() < e.Row.Cells["ActualTotalRentCommission"].Value.ToDecimal() )
                {
                   
                        e.CellElement.DrawFill = true;
                        //e.CellElement.GradientStyle = GradientStyles.Solid;
                        //e.CellElement.BackColor = Color.Gainsboro;
                        //  e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                        //  e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                        e.CellElement.ForeColor = Color.Red;
                   

                }
                else
                {
                    e.CellElement.DrawFill = false;
                    e.CellElement.ForeColor = Color.Black;

                }

            }
            if (e.CellElement.RowInfo is GridViewSummaryRowInfo)
            {
                e.CellElement.DrawFill = true;
                e.CellElement.GradientStyle = GradientStyles.Solid;
                e.CellElement.BackColor = Color.Gainsboro;
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
                e.CellElement.Font = new Font(e.CellElement.Font, FontStyle.Bold);
                e.CellElement.ForeColor = Color.Black;
            }
        }


        private void FormatGrid()
        {

            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = "Id";
            col.IsVisible = false;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "TransId";
            col.IsVisible = false;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "Active";
            col.HeaderText = "Active/Off";
            col.Width = 110;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            col.Name = "DriverNo";
            col.HeaderText = "Driver No";
            col.Width = 120;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

            //col = new GridViewTextBoxColumn();
            //col.Name = COLS.DriverName;
            //col.IsVisible = false;
            //grdLister.Columns.Add(col);


            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = "AccountBookings";
            dcol.HeaderText = "Account Bookings";
            dcol.Width = 160;
            dcol.DecimalPlaces = 2;
            dcol.ReadOnly = true;
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDecimalColumn();
            dcol.Name = "PaidAccountBookings";
            dcol.HeaderText = "Paid Account Bookings";
            dcol.Width = 160;
            dcol.DecimalPlaces = 2;
            dcol.ReadOnly = true;
            dcol.IsVisible = false;
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDecimalColumn();
            dcol.Name = "TotalRentCommission";
            dcol.HeaderText = "Rent/Comm";
            dcol.Width = 130;
            dcol.DecimalPlaces = 2;
            dcol.ReadOnly = true;
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDecimalColumn();
            dcol.Name = "ActualTotalRentCommission";
            dcol.HeaderText = "ActualRent/Comm";
            dcol.Width = 130;
            dcol.DecimalPlaces = 2;
            dcol.IsVisible = false;
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            grdLister.Columns.Add(dcol);



            dcol = new GridViewDecimalColumn();
            dcol.Name = "PaidTotalRentCommission";
            dcol.HeaderText = "Paid Rent/Comm";
            dcol.Width = 130;
            dcol.DecimalPlaces = 2;
            dcol.IsVisible = false;
            dcol.ReadOnly = true;
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDecimalColumn();
            dcol.Name = "OfficeToPay";
            dcol.HeaderText = "Office To Pay";
            dcol.Width = 120;
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            dcol.DecimalPlaces = 2;
            dcol.ReadOnly = true;
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDecimalColumn();
            dcol.Name = "PaidOfficeToPay";
            dcol.HeaderText = "Paid Office To Pay";
            dcol.Width = 120;
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            dcol.DecimalPlaces = 2;
            dcol.IsVisible = false;
            dcol.ReadOnly = true;
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDecimalColumn();
            dcol.Name = "PreviousBalance";
            dcol.HeaderText = "Prev Balance";
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            dcol.Width = 140;
            dcol.DecimalPlaces = 2;
            dcol.ReadOnly = true;
            grdLister.Columns.Add(dcol);



            dcol = new GridViewDecimalColumn();
            dcol.Name = "PaidPreviousBalance";
            dcol.HeaderText = "Paid Prev Balance";
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            dcol.Width = 140;
            dcol.DecimalPlaces = 2;
            dcol.ReadOnly = true;
            dcol.IsVisible = false;
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDecimalColumn();
            dcol.Name = "DriverToPay";
            dcol.HeaderText = "Driver To Pay";
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            dcol.Width = 140;
            dcol.DecimalPlaces = 2;
            dcol.ReadOnly = true;
            grdLister.Columns.Add(dcol);


            dcol = new GridViewDecimalColumn();
            dcol.Name = "PaidDriverToPay";
            dcol.HeaderText = "Paid Driver To Pay";
            dcol.ExcelExportType = DisplayFormatType.Text;
            dcol.ExcelExportFormatString = "{0:d}";
            dcol.Width = 140;
            dcol.DecimalPlaces = 2;
            dcol.ReadOnly = true;
            dcol.IsVisible = false;
            grdLister.Columns.Add(dcol);

            GridViewDateTimeColumn dtCol = new GridViewDateTimeColumn();
            dtCol.Name = "FromDate";
            dtCol.HeaderText = "FromDate";
            dtCol.Width = 120;
            dtCol.ReadOnly = true;
            dtCol.IsVisible = false;
            grdLister.Columns.Add(dtCol);


            dtCol = new GridViewDateTimeColumn();
            dtCol.Name = "ToDate";
            dtCol.HeaderText = "ToDate";
            dtCol.Width = 120;
            dtCol.ReadOnly = true;
            dtCol.IsVisible = false;
            grdLister.Columns.Add(dtCol);



            GridViewCheckBoxColumn cbcol = new GridViewCheckBoxColumn();
            cbcol.Name = "IsPaid";
            cbcol.HeaderText = "Paid";
            cbcol.Width = 100;
            cbcol.ReadOnly = false;
            // cbcol.IsVisible = false;
            grdLister.Columns.Add(cbcol);



            col = new GridViewTextBoxColumn();
            col.Name = "Paid";
            col.HeaderText = "Paid";
            col.Width = 120;
            col.ReadOnly = true;
            col.IsVisible = false;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            col.Name = "PaidValue";
            col.HeaderText = "PaidValue";
            col.Width = 120;
            col.ReadOnly = true;
            col.IsVisible = false;
            grdLister.Columns.Add(col);




            GridViewCommandColumn cmdcol = new GridViewCommandColumn();
            cmdcol.Width = 80;
            cmdcol.Name = "btnUpdate";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Update";
            cmdcol.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            grdLister.Columns.Add(cmdcol);


            ConditionalFormattingObject obj = new ConditionalFormattingObject();
            obj.RowBackColor = Color.LightGreen;
            obj.TValue1 = "Paid";
            obj.ConditionType = ConditionTypes.Equal;
            obj.TValue2 = string.Empty;
            obj.RowForeColor = Color.Black;
            obj.ApplyToRow = true;
         
            grdLister.Columns["Paid"].ConditionalFormattingObjectList.Add(obj);
            


            ConditionalFormattingObject obj2 = new ConditionalFormattingObject();
            obj2.TValue1 = "(C)";
            obj2.ConditionType = ConditionTypes.EndsWith;
            obj2.TValue2 = string.Empty;
            obj2.CellForeColor = Color.White;
            obj2.CellBackColor = Color.Red;
            obj2.ApplyToRow = false;
            grdLister.Columns["DriverNo"].ConditionalFormattingObjectList.Add(obj2);

           // ConditionalFormattingObject obj2 = new ConditionalFormattingObject();
           // obj2.CellForeColor = Color.Red;
           // obj2.TValue1 = "TotalRentCommission";
           // obj2.TValue2 = "ActualTotalRentCommission";
           // obj2.ApplyToRow = false;
           // obj2.ConditionType = ConditionTypes.NotEqual;
           //// obj.RowBackColor = Color.SkyBlue;
           // this.grdLister.Columns["TotalRentCommission"].ConditionalFormattingObjectList.Add(obj2);
        }

        void frmPaymentCollection_Load(object sender, EventArgs e)
        {
            FormatGrid();
            PopulateData();
        }


        private void AddSummaries()
        {


            this.grdLister.MasterGridViewTemplate.SummaryRowsBottom.Clear();

            GridViewSummaryRowItem item2 = new GridViewSummaryRowItem();


            GridViewSummaryItem c = new GridViewSummaryItem();
            c.Aggregate = GridAggregateFunction.None;
            c.AggregateExpression = "SUM(PaidTotalRentCommission)";
            c.Name = "DriverNo";
            c.FormatString = "Gr. Total";
            item2.Add(c);


            c = new GridViewSummaryItem();
            c.Aggregate = GridAggregateFunction.Sum;
            c.AggregateExpression = "SUM(PaidAccountBookings)";
            c.Name = "AccountBookings";
            c.FormatString = "{0}";
            item2.Add(c);

            c = new GridViewSummaryItem();
            c.Aggregate = GridAggregateFunction.Sum;
            c.AggregateExpression = "SUM(PaidTotalRentCommission)";
            c.Name = "TotalRentCommission";
            c.FormatString = "{0}";
            item2.Add(c);


            c = new GridViewSummaryItem();
            c.Aggregate = GridAggregateFunction.Sum;
            c.AggregateExpression = "SUM(PaidOfficeToPay)";
            c.Name = "OfficeToPay";
            c.FormatString = "{0}";
            item2.Add(c);


            c = new GridViewSummaryItem();
            c.Aggregate = GridAggregateFunction.Sum;
            c.AggregateExpression = "SUM(PaidDriverToPay)";
            c.Name = "DriverToPay";
            c.FormatString = "{0}";
            item2.Add(c);


            c = new GridViewSummaryItem();
            c.Aggregate = GridAggregateFunction.Sum;
            c.AggregateExpression = "SUM(PaidPreviousBalance)";
            c.Name = "PreviousBalance";
            c.FormatString = "{0}";
            item2.Add(c);



            this.grdLister.MasterGridViewTemplate.SummaryRowsBottom.Add(item2);
            this.grdLister.MasterView.SummaryRows[0].PinPosition = PinnedRowPosition.Bottom;
        }




        public override void PopulateData()
        {
            try
            {
                int idx = 0;
                int val = 0;


                int driverTypeId = optRent.Checked ? Enums.DRIVERTYPES.RENT : Enums.DRIVERTYPES.COMMISSION;
                   

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = (from a in db.stp_DriverPaymentAccountBookings(driverTypeId, numrent.Value.ToDecimal())
                                select new
                                  {
                                      a.Id,
                                      a.TransId,
                                      a.Active,
                                      a.AccountBookings,
                                      a.TotalRentCommission,
                                      a.ActualTotalRentCommission,
                                      a.DriverName,
                                      a.DriverNo,
                                      a.DriverToPay,
                                      a.OfficeToPay,
                                      a.PreviousBalance,
                                      IsPaid = a.IsWeeklyPaid,
                                      Paid = a.IsWeeklyPaid != null && a.IsWeeklyPaid == true ? "Paid" : "",
                                      a.FromDate,
                                      a.ToDate,
                              
                                      PaidValue = a.IsWeeklyPaid != null && a.IsWeeklyPaid == true ? 1 : 0

                                  }).ToList();

                    var list2 = (list.AsEnumerable().OrderBy(item => item.DriverNo, new NaturalSortComparer<string>())).ToList();

                    idx = grdLister.CurrentRow != null ? grdLister.CurrentRow.Cells["Id"].Value.ToInt() : -1;
                    val = grdLister.TableElement.VScrollBar.Value;

                    grdLister.BeginUpdate();
                    grdLister.RowCount = list2.Count;
                    for (int i = 0; i < list2.Count; i++)
                    {

                        grdLister.Rows[i].Cells["Id"].Value = list2[i].Id;
                        grdLister.Rows[i].Cells["TransId"].Value = list2[i].TransId;
                        grdLister.Rows[i].Cells["Active"].Value = list2[i].Active;
                        grdLister.Rows[i].Cells["DriverNo"].Value = list2[i].DriverNo;

                        grdLister.Rows[i].Cells["AccountBookings"].Value = list2[i].AccountBookings;
                        grdLister.Rows[i].Cells["PaidAccountBookings"].Value = list2[i].IsPaid.ToBool() ? list2[i].AccountBookings : 0.00m;


                        grdLister.Rows[i].Cells["TotalRentCommission"].Value = list2[i].TotalRentCommission;
                        grdLister.Rows[i].Cells["PaidTotalRentCommission"].Value = list2[i].IsPaid.ToBool() ? list2[i].TotalRentCommission : 0.00m;

                        grdLister.Rows[i].Cells["ActualTotalRentCommission"].Value = list2[i].ActualTotalRentCommission;

                        grdLister.Rows[i].Cells["OfficeToPay"].Value = list2[i].OfficeToPay;
                        grdLister.Rows[i].Cells["PaidOfficeToPay"].Value = list2[i].IsPaid.ToBool() ? list2[i].OfficeToPay : 0.00m;


                        grdLister.Rows[i].Cells["DriverToPay"].Value = list2[i].DriverToPay;
                        grdLister.Rows[i].Cells["PaidDriverToPay"].Value = list2[i].IsPaid.ToBool() ? list2[i].DriverToPay : 0.00m;

                        grdLister.Rows[i].Cells["PreviousBalance"].Value = list2[i].PreviousBalance.ToDecimal();
                        grdLister.Rows[i].Cells["PaidPreviousBalance"].Value = list2[i].IsPaid.ToBool() && list2[i].PreviousBalance.ToDecimal() < 0 ? list2[i].PreviousBalance.ToDecimal() : 0.00m;


                        grdLister.Rows[i].Cells["IsPaid"].Value = list2[i].IsPaid.ToBool();

                        grdLister.Rows[i].Cells["Paid"].Value = list2[i].Paid.ToStr();

                        grdLister.Rows[i].Cells["FromDate"].Value = list2[i].FromDate.ToDate();
                        grdLister.Rows[i].Cells["ToDate"].Value = list2[i].ToDate.ToDate();
                        grdLister.Rows[i].Cells["PaidValue"].Value = list2[i].PaidValue.ToInt();

                    }


                    grdLister.EndUpdate();

                }





                SetDefaultColumnSettings();


                AddSummaries();
                UpdatePeriod();


                if (idx > 0)
                    grdLister.CurrentRow = grdLister.Rows.FirstOrDefault(c => c.Cells["Id"].Value.ToInt() == idx);


                if (grdLister.TableElement.VScrollBar.Maximum >= val)
                    grdLister.TableElement.VScrollBar.Value = val;

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void SetDefaultColumnSettings()
        {
            grdLister.Columns["IsPaid"].IsVisible = true;
            grdLister.Columns["Paid"].IsVisible = false;
            grdLister.Columns["btnUpdate"].IsVisible = true;

            grdLister.Columns["Active"].Width = 110;
            grdLister.Columns["DriverNo"].Width = 120;
            grdLister.Columns["AccountBookings"].Width = 160;
            grdLister.Columns["TotalRentCommission"].Width = 130;

            grdLister.Columns["OfficeToPay"].Width = 120;
            grdLister.Columns["PreviousBalance"].Width = 140;
            grdLister.Columns["IsPaid"].Width = 100;
            grdLister.Columns["DriverToPay"].Width = 140;
            grdLister.Columns["Paid"].Width = 120;


            grdLister.Columns["Active"].HeaderText = "Active/Off";

            grdLister.Columns["DriverNo"].HeaderText = "Driver No";

            grdLister.Columns["AccountBookings"].HeaderText = "Account Bookings";


        }

        private void UpdatePeriod()
        {
            try
            {

                var row = grdLister.Rows.OrderByDescending(c => c.Cells["ToDate"].Value.ToDate()).FirstOrDefault();

                DateTime? dtCurrent = row.Cells["FromDate"].Value.ToDate();
                DateTime dtEnd = row.Cells["ToDate"].Value.ToDate();
                this.FormTitle = "Drivers Weekly Rent List - " + string.Format("from {0:dd/MM/yyyy}", dtCurrent) + " until " + string.Format("{0:dd/MM/yyyy}", dtEnd);



            }
            catch
            {



            }
        }

    }
}
