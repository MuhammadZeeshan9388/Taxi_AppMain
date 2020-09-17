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
    public partial class frmDriverCommissionPayExpenses2 : UI.SetupBase
    {
        bool IsDriverLoaded = false;
        long CommissionId = 0;
        DriverCommisionBO objDriverCommision = new DriverCommisionBO();
        public frmDriverCommissionPayExpenses2()
        {
            InitializeComponent();
            FormatGrid();
            this.Load += new EventHandler(frmAddDriverRent_Load);
            this.ddlDrivers.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlDrivers_SelectedIndexChanged);
            this.FormClosed += new FormClosedEventHandler(frmDriverRentPay_FormClosed);
            this.KeyDown += new KeyEventHandler(frmDriverRentPay_KeyDown);
            this.KeyPreview = true;

            spnCommissionPay.SpinElement.TextChanging += new TextChangingEventHandler(SpinElement_TextChanging);
            //AddDeleteColumn(grdPaymentHistory);
            grdPaymentHistory.CommandCellClick += new CommandCellClickEventHandler(grdPaymentHistory_CommandCellClick);


            rdoCredit.ToggleStateChanged += new StateChangedEventHandler(rdoCredit_ToggleStateChanged);
            rdoDebit.ToggleStateChanged += new StateChangedEventHandler(rdoCredit_ToggleStateChanged);

        }



        void grdPaymentHistory_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                //if (grdPaymentHistory.CurrentRow != null && grdPaymentHistory.CurrentRow is GridViewDataRowInfo)
                //{
                //    long paymentId = grdPaymentHistory.CurrentRow.Cells["Id"].Value.ToLong();
                //    if (grdPaymentHistory.Rows.Count > 1 && paymentId < grdPaymentHistory.Rows.Where(c => c.Index != grdPaymentHistory.CurrentRow.Index)
                //                                              .OrderByDescending((c => c.Cells["Id"].Value.ToLong())).FirstOrDefault().Cells["Id"].Value.ToLong())
                //    {
                //        ENUtils.ShowMessage("Payment History will be Delete in Reverse Order");
                //        return;
                //    }
                //    else
                //    {
                //        decimal commissionPay=grdPaymentHistory.CurrentRow.Cells["CommissionPay"].Value.ToDecimal();
                //        using (TaxiDataContext db = new TaxiDataContext())
                //        {
                //            db.stp_UpdateDriverCommissionPayment(CommissionId, paymentId, commissionPay);
                //        }
                //        grdPaymentHistory.CurrentRow.Delete();
                //        DisplayRecord();
                //    }
                //}
                 GridCommandCellElement gridCell = (GridCommandCellElement)sender;
                 if (gridCell.ColumnInfo.Name.ToLower() == "btndelete")
                 {
                     if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete a Record ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                     {
                         long Id = grdPaymentHistory.CurrentRow.Cells["Id"].Value.ToLong();
                         if (grdPaymentHistory.CurrentRow != null && grdPaymentHistory.CurrentRow is GridViewDataRowInfo)
                         {

                             if (grdPaymentHistory.Rows.Count > 1 && Id < grdPaymentHistory.Rows.Where(c => c.Index != grdPaymentHistory.CurrentRow.Index)
                                                                       .OrderByDescending((c => c.Cells["Id"].Value.ToLong())).FirstOrDefault().Cells["Id"].Value.ToLong())
                             {
                                 ENUtils.ShowMessage("Expenses History will be Delete in Reverse Order");
                                 return;
                             }
                         }

                         string Type = grdPaymentHistory.CurrentRow.Cells[COLS.Type].Value.ToStr().ToLower();
                         decimal Amount = grdPaymentHistory.CurrentRow.Cells[COLS.Amount].Value.ToDecimal();
                         decimal CurrentBalance = numCurrBalance.Value.ToDecimal();
                         grdPaymentHistory.CurrentRow.Delete();
                         if (Type == "credit")
                         {
                             //if (CurrentBalance > 0)
                             //{
                                 numCurrBalance.Value = (CurrentBalance - Amount);
                          
                          
                         }
                         else
                         {
                             //if (CurrentBalance > 0)
                             //{
                                 numCurrBalance.Value = (CurrentBalance + Amount);
                             
                         }
                         objDriverCommision.GetByPrimaryKey(CommissionId);
                         objDriverCommision.Edit();
                         objDriverCommision.Current.Balance = numCurrBalance.Value.ToDecimal();
                        
                         objDriverCommision.Save();
                         using (TaxiDataContext db = new TaxiDataContext())
                         {
                             var query = db.Fleet_DriverCommissionExpenses.Single(c => c.Id == Id);
                             db.Fleet_DriverCommissionExpenses.DeleteOnSubmit(query);
                             db.SubmitChanges();
                         }
                         DisplayRecord();
                     }
                 }
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void AddDeleteColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.BestFit();

            col.Name = "btnDelete";
            col.Width = 60;
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Delete";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }

        void rdoCredit_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            try
            {
                if (rdoCredit.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
                {

                    numCurrBalance.Value = lblCommissionDue.Text.ToDecimal() + spnCommissionPay.Value;
                }
                else
                {
                    numCurrBalance.Value = lblCommissionDue.Text.ToDecimal() - spnCommissionPay.Value;
                }
            }
            catch
            {


            }
        }

        void SpinElement_TextChanging(object sender, TextChangingEventArgs e)
        {
            if (e.NewValue != "" && e.NewValue != "-")
            {

                if (e.NewValue.ToStr().Trim().Length > 0)
                {
                    if (rdoCredit.IsChecked)
                    {
                       
                        numCurrBalance.Value = lblCommissionDue.Text.ToDecimal() + e.NewValue.ToDecimal();
                    }
                    else
                    {
                        numCurrBalance.Value = lblCommissionDue.Text.ToDecimal() - e.NewValue.ToDecimal();
                    }

                }

            }
            else
            {
                numCurrBalance.Value = lblCommissionDue.Text.ToDecimal() - 0.00m;

            }

        }

        //void SpinElement_ValueChanged(object sender, EventArgs e)
        //{
        //    numCurrBalance.Value = numCurrBalance.Value+  spnRentPay.Value;
        //}

        void frmDriverRentPay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        void frmDriverRentPay_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);
            GC.Collect();
        }

        void ddlDrivers_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

            if (IsDriverLoaded == false) return;

            DisplayRecord();

        }
        private void Save()
        {
            try
            {
                if (objDriverCommision.Current == null) return;
                string Description = txtReason.Text.ToStr().Trim();
                if (string.IsNullOrEmpty(Description))
                {
                    ENUtils.ShowMessage("Required : Description");
                    return;
                }
                objDriverCommision.Edit();
                objDriverCommision.Current.Balance = numCurrBalance.Value.ToDecimal();
                Fleet_DriverCommissionExpense objDriverCommissionExpense = new Fleet_DriverCommissionExpense();
                if (rdoCredit.IsChecked)
                {
                    objDriverCommissionExpense.Credit = spnCommissionPay.Value.ToDecimal();
                    objDriverCommissionExpense.Amount = spnCommissionPay.Value.ToDecimal();
                }
                else
                {
                    objDriverCommissionExpense.Debit = spnCommissionPay.Value.ToDecimal();
                    objDriverCommissionExpense.Amount = spnCommissionPay.Value.ToDecimal();
                }
                objDriverCommissionExpense.Date = DateTime.Now;
                objDriverCommissionExpense.Description = Description;
                objDriverCommissionExpense.IsPaid = true;
                if (objDriverCommision.Current.Fleet_DriverCommissionExpenses.Count == 0)
                {
                    objDriverCommision.Current.Fleet_DriverCommissionExpenses.Add(objDriverCommissionExpense);
                }
                else
                {
                    objDriverCommision.Current.Fleet_DriverCommissionExpenses.Add(objDriverCommissionExpense);
                }

                objDriverCommision.Current.EditOn = DateTime.Now;
                objDriverCommision.Current.EditLog = AppVars.LoginObj.UserName.ToStr();
                objDriverCommision.Current.EditBy = AppVars.LoginObj.LuserId.ToInt();

                objDriverCommision.Save();
                DisplayRecord();
                //this.Close(); 

            }
            catch (Exception ex)
            {
                if (objDriverCommision.Errors.Count > 0)
                {
                    ENUtils.ShowMessage(objDriverCommision.ShowErrors());
                }
                else
                {
                    ENUtils.ShowMessage(ex.Message);
                }
            }
        }
        private void DisplayRecord()
        {

            try
            {
                int DriverId = 0;
                CommissionId = 0;



                lblTitle.Visible = false;
                lblTransactionNo.Visible = false;
                //txtRentPaid.Visible = false;
                //txtRentPaidSign.Visible = false;
                //txtIsPaid.Visible = false;
                rdoCredit.IsChecked = true;
                spnCommissionPay.Value = 0;
                txtReason.Text = "";
                //  ddlCommissionPayReason.SelectedIndex = 0;


                DriverId = ddlDrivers.SelectedValue.ToInt();

                var item = (from a in General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId)
                            orderby a.Id descending
                            select new
                            {
                                Id = a.Id,
                                BalanceDue = a.Balance,
                                DriverCommission = a.DriverOwed,
                                TransactionNo = a.TransNo,
                                PeriodFrom = a.FromDate,
                                PeriodTo = a.ToDate,
                                CommisionPaid = a.CommisionPay,
                                PayHistory = a.DriverCommission_PaymentHistories //a.DriverRent_PaymentHistories
                            }).FirstOrDefault();


                if (item != null)
                {
                    lblerror.Visible = false;
                    lblTitle.Visible = true;
                    lblTransactionNo.Visible = true;

                    objDriverCommision.GetByPrimaryKey(item.Id);
                    //lblRentDue.Text = item.BalanceDue.ToStr();

                    //numCurrBalance.Value = item.BalanceDue.ToDecimal();
                    //lblCommissionDue.Text = item.DriverCommission.ToStr();
                    lblCommissionDue.Text = item.BalanceDue.ToStr();
                    lblerror.Text = item.DriverCommission.ToStr();
                    //numCurrBalance.Value = item.DriverCommission.ToDecimal();
                    numCurrBalance.Value = item.BalanceDue.ToDecimal();

                    CommissionId = item.Id;

                    lblTitle.Text = "Statement No:";
                    lblTransactionNo.Text = item.TransactionNo;

                    if (item.PeriodFrom != null)
                        lblTransactionNo.Text += " , Period : " + string.Format("{0:dd/MM/yyyy}", item.PeriodFrom);


                    if (item.PeriodTo != null)
                        lblTransactionNo.Text += " to : " + string.Format("{0:dd/MM/yyyy}", item.PeriodTo);

                    //if (item.CommisionPaid.ToDecimal() > 0)
                    //{

                    //    //txtRentPaid.Visible = true;
                    //    //txtRentPaidSign.Visible = true;
                    //    //txtIsPaid.Visible = true;
                    //    //txtRentPaid.Text = item.CommisionPaid.ToStr();

                    //}

                    btnSave.Enabled = true;


                    ShowPaymentHistory(item.PayHistory.ToList());


                    var list = (from a in General.GetQueryable<Fleet_DriverCommissionExpense>(c => c.CommissionId == CommissionId && (c.IsPaid == true))
                                //where a.IsPaid == true
                                select new
                                {
                                    Id = a.Id,
                                    CommissionId = a.CommissionId,
                                    Credit = a.Credit,
                                    Debit = a.Debit,
                                    Date = a.Date,
                                    Type = a.Credit > 0.00m ? "Credit" : "Debit",
                                    Amount = a.Amount,
                                    Description = a.Description,
                                    Balance = a.Fleet_DriverCommision.Balance
                                }).ToList();

                    grdPaymentHistory.Rows.Clear();

                    grdPaymentHistory.RowCount = list.Count;
                    for (int i = 0; i < list.Count; i++)
                    {
                        grdPaymentHistory.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdPaymentHistory.Rows[i].Cells[COLS.Credit].Value = list[i].Credit;
                        grdPaymentHistory.Rows[i].Cells[COLS.Debit].Value = list[i].Debit;
                        grdPaymentHistory.Rows[i].Cells[COLS.Type].Value = list[i].Type;
                        grdPaymentHistory.Rows[i].Cells[COLS.Date].Value = list[i].Date;
                        grdPaymentHistory.Rows[i].Cells[COLS.Description].Value = list[i].Description;
                        grdPaymentHistory.Rows[i].Cells[COLS.Balance].Value = list[i].Balance;
                        grdPaymentHistory.Rows[i].Cells[COLS.CommissionId].Value = list[i].CommissionId;
                        grdPaymentHistory.Rows[i].Cells[COLS.Amount].Value = list[i].Amount;
                    }
                }
                else
                {
                    lblCommissionDue.Text = "0";
                    numCurrBalance.Value = 0.00m;

                    spnCommissionPay.Value = 0;
                    lblerror.Visible = true;
                    lblerror.ForeColor = Color.Red;
                    lblerror.Text = "Driver Commission is not generated";
                    btnSave.Enabled = false;
                    grdPaymentHistory.Rows.Clear();

                    //txtRentPaid.Visible = false;
                    //txtRentPaidSign.Visible = false;
                    //txtIsPaid.Visible = false;
                    //txtRentPaid.Text = string.Empty;
                }


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        public struct COLS
        {
            public static string Id = "Id";
            public static string Credit = "Credit";
            public static string Debit = "Debit";
            public static string Date = "Date";
            public static string Description = "Description";
            public static string Type = "Type";
            public static string Balance = "Balance";
            public static string Amount = "Amount";
            public static string CommissionId = "CommissionId";
            //Type
        }
        private void FormatGrid()
        {
            GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();
            dtcol.Name = COLS.Date;
            dtcol.HeaderText = COLS.Date;
            //dtcol.CustomFormat = "{0:dd/MM/yyyy}";
            dtcol.FormatString = "{0:dd/MM/yyyy}";
            dtcol.Width = 110;
            dtcol.ReadOnly = true;
            grdPaymentHistory.Columns.Add(dtcol);
            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.Name = COLS.Id;
            col.IsVisible = false;
            grdPaymentHistory.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            col.Name = COLS.CommissionId;
            col.IsVisible = false;
            grdPaymentHistory.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            col.Name = COLS.Type;
            col.HeaderText = COLS.Type;
            col.Width = 100;
            col.ReadOnly = true;
            grdPaymentHistory.Columns.Add(col);
            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.Credit;
            dcol.IsVisible = false;
            grdPaymentHistory.Columns.Add(dcol);
            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.Debit;
            dcol.IsVisible = false;
            grdPaymentHistory.Columns.Add(dcol);
            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.Amount;
            dcol.HeaderText = COLS.Amount;
            dcol.ReadOnly = true;
            dcol.Width = 110;
            grdPaymentHistory.Columns.Add(dcol);


            col = new GridViewTextBoxColumn();
            col.Name = COLS.Description;
            col.HeaderText = COLS.Description;
            col.Width = 140;
            col.ReadOnly = true;
            grdPaymentHistory.Columns.Add(col);
            dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.Balance;
            dcol.HeaderText = COLS.Balance;
            dcol.Width = 100;
            dcol.ReadOnly = true;
            grdPaymentHistory.Columns.Add(dcol);
            GridViewCommandColumn cmd = new GridViewCommandColumn();
            cmd.Width = 100;

            cmd.Name = "btnDelete";
            cmd.UseDefaultText = true;
            cmd.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmd.DefaultText = "Delete";// caption;
            cmd.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grdPaymentHistory.Columns.Add(cmd);
        }
        private void ShowPaymentHistory(List<DriverCommission_PaymentHistory> list)
        {

            try
            {
                grdPaymentHistory.Rows.Clear();

                GridViewRowInfo row = null;
                foreach (var item in list)
                {
                    row = grdPaymentHistory.Rows.AddNew();

                    row.Cells["PaymentDate"].Value = string.Format("{0:dd/MM/yyyy HH:mm}", item.PaymentDate.ToDateTime());

                    row.Cells["Balance"].Value = item.OldBalance.ToDecimal();
                    row.Cells["CommissionPay"].Value = item.CommissionPay.ToDecimal();
                    row.Cells["BalanceDue"].Value = item.BalanceDue.ToDecimal();
                    row.Cells["Reason"].Value = item.DriverCommissionPayReason.DefaultIfEmpty().CommissionReason.ToStr();

                    row.Cells["Id"].Value = item.Id;
                }


                //txtRentPaid.Visible = true;
                //txtRentPaidSign.Visible = true;
                //txtIsPaid.Visible = true;
                //txtRentPaid.Text = list.Sum(c => c.CommissionPay.ToDecimal().ToDecimal()).ToStr();
            }
            catch (Exception ex)
            {

            }

        }

        void frmAddDriverRent_Load(object sender, EventArgs e)
        {
            FillCombo();
            IsDriverLoaded = true;


            this.Size = new Size(700, 788);
        }
        private void FillCombo()
        {
            ComboFunctions.FillDriverNoCombo(ddlDrivers, c => c.DriverTypeId == 2);
            //   ComboFunctions.FillCommissionReasonCombo(ddlCommissionPayReason);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save(); //UpdateDriverCommission();
        }
            
            
            
        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
