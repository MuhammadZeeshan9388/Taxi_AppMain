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
    public partial class frmDriverCommissionPay : UI.SetupBase
    {
        bool IsDriverLoaded = false;
        long CommissionId = 0;
        public frmDriverCommissionPay()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmAddDriverRent_Load);
            this.ddlDrivers.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlDrivers_SelectedIndexChanged);
            this.FormClosed += new FormClosedEventHandler(frmDriverRentPay_FormClosed);
            this.KeyDown += new KeyEventHandler(frmDriverRentPay_KeyDown);
            this.KeyPreview = true;

            spnCommissionPay.SpinElement.TextChanging += new TextChangingEventHandler(SpinElement_TextChanging);
            AddDeleteColumn(grdPaymentHistory);
            grdPaymentHistory.CommandCellClick += new CommandCellClickEventHandler(grdPaymentHistory_CommandCellClick);

         
        }

      

        void grdPaymentHistory_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                if (grdPaymentHistory.CurrentRow != null && grdPaymentHistory.CurrentRow is GridViewDataRowInfo)
                {
                    long paymentId = grdPaymentHistory.CurrentRow.Cells["Id"].Value.ToLong();


                    if (grdPaymentHistory.Rows.Count > 1 && paymentId < grdPaymentHistory.Rows.Where(c => c.Index != grdPaymentHistory.CurrentRow.Index)
                                                              .OrderByDescending((c => c.Cells["Id"].Value.ToLong())).FirstOrDefault().Cells["Id"].Value.ToLong())
                    {
                        ENUtils.ShowMessage("Payment History will be Delete in Reverse Order");
                        return;

                    }

                    else
                    {

                        decimal commissionPay=grdPaymentHistory.CurrentRow.Cells["CommissionPay"].Value.ToDecimal();

                        using (TaxiDataContext db = new TaxiDataContext())
                        {

                            db.stp_UpdateDriverCommissionPayment(CommissionId, paymentId, commissionPay);

                        }

                        grdPaymentHistory.CurrentRow.Delete();

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

        void SpinElement_TextChanging(object sender, TextChangingEventArgs e)
        {
            if (e.NewValue != "" && e.NewValue != "-")
            {

                if (e.NewValue.ToStr().Trim().Length > 0)
                {
                    if (numCurrBalance.Value < 0)
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

        private void DisplayRecord()
        {

            try
            {
                int DriverId = 0;
                CommissionId = 0;

             

                lblTitle.Visible = false;
                lblTransactionNo.Visible = false;
                txtRentPaid.Visible = false;
                txtRentPaidSign.Visible = false;
                txtIsPaid.Visible = false;
                ddlCommissionPayReason.SelectedIndex = 0;


                DriverId = ddlDrivers.SelectedValue.ToInt();

                var item = (from a in General.GetQueryable<Fleet_DriverCommision>(c => c.DriverId == DriverId)
                            orderby a.Id descending
                            select new
                            {
                                Id = a.Id,
                                BalanceDue = a.Balance,
                                DriverCommission=a.DriverOwed,
                                TransactionNo = a.TransNo,
                                PeriodFrom = a.FromDate,
                                PeriodTo = a.ToDate,
                                CommisionPaid = a.CommisionPay,
                                PayHistory =a.DriverCommission_PaymentHistories //a.DriverRent_PaymentHistories
                            }).FirstOrDefault();


                if (item != null)
                {
                    lblerror.Visible = false;
                    lblTitle.Visible = true;
                    lblTransactionNo.Visible = true;


                    //lblRentDue.Text = item.BalanceDue.ToStr();

                    //numCurrBalance.Value = item.BalanceDue.ToDecimal();
                    //lblCommissionDue.Text = item.DriverCommission.ToStr();
                    lblCommissionDue.Text = item.BalanceDue.ToStr();
                    lblerror.Text=item.DriverCommission.ToStr();
                    //numCurrBalance.Value = item.DriverCommission.ToDecimal();
                    numCurrBalance.Value=item.BalanceDue.ToDecimal();

                    CommissionId = item.Id;

                    lblTitle.Text = "Statment No:";
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

                    txtRentPaid.Visible = false;
                    txtRentPaidSign.Visible = false;
                    txtIsPaid.Visible = false;
                    txtRentPaid.Text = string.Empty;
                }


                chkOther.Checked = false;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void ShowPaymentHistory(List<DriverCommission_PaymentHistory> list)
        {

            try
            {
                grdPaymentHistory.Rows.Clear();

                GridViewRowInfo row=null;
                foreach (var item in list)
                {
                   row=  grdPaymentHistory.Rows.AddNew();

                     row.Cells["PaymentDate"].Value=string.Format("{0:dd/MM/yyyy HH:mm}", item.PaymentDate.ToDateTime());

                     row.Cells["Balance"].Value=item.OldBalance.ToDecimal();
                     row.Cells["CommissionPay"].Value = item.CommissionPay.ToDecimal();
                     row.Cells["BalanceDue"].Value=item.BalanceDue.ToDecimal();
                     row.Cells["Reason"].Value = item.DriverCommissionPayReason.DefaultIfEmpty().CommissionReason.ToStr();

                     row.Cells["Id"].Value = item.Id;
                }


                txtRentPaid.Visible = true;
                txtRentPaidSign.Visible = true;
                txtIsPaid.Visible = true;
                txtRentPaid.Text = list.Sum(c=>c.CommissionPay.ToDecimal().ToDecimal()).ToStr();
            }
            catch(Exception ex)
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
            ComboFunctions.FillDriverNoCombo(ddlDrivers,c=>c.DriverTypeId==2);
            ComboFunctions.FillCommissionReasonCombo(ddlCommissionPayReason);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDriverCommission();
        }
        private void UpdateDriverCommission()
        {
            try
            {
                if (CommissionId == 0)
                {
                    ENUtils.ShowMessage("Driver Commission is not Generated");
                    return;
                }



                if (chkOther.Checked)
                    ddlCommissionPayReason.SelectedValue = null;

                decimal CommissionDue = lblCommissionDue.Text.ToDecimal();
                decimal CommissionPay = spnCommissionPay.Value.ToDecimal();
                decimal CommissionPaid = (CommissionPay + CommissionDue);
                decimal CurrentBalance = numCurrBalance.Value.ToDecimal();

                int? CommissionPayReasonId = ddlCommissionPayReason.SelectedValue.ToIntorNull();
                string CommissionPayReasonText = txtReason.Text.Trim();
                bool IsNewReason = false;
                 
              


               
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        if (CommissionPayReasonId == null && !string.IsNullOrEmpty(CommissionPayReasonText))
                        {
                            try
                            {

                                //DriverRentPayReason objRentPayReason = new DriverRentPayReason();
                                //rentPayReasonId = db.DriverRentPayReasons.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
                                //objRentPayReason.Id = rentPayReasonId.ToInt();
                                //objRentPayReason.RentReason = rentPayReasonText;


                                //db.DriverRentPayReasons.InsertOnSubmit(objRentPayReason);
                                //db.SubmitChanges();

                                DriverCommissionPayReason objCommissionPayReason = new DriverCommissionPayReason();
                                CommissionPayReasonId = db.DriverCommissionPayReasons.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
                                objCommissionPayReason.Id=CommissionPayReasonId.ToInt();
                                objCommissionPayReason.CommissionReason = CommissionPayReasonText;
                                db.DriverCommissionPayReasons.InsertOnSubmit(objCommissionPayReason);
                                db.SubmitChanges();
                                IsNewReason = true;
                            }
                            catch
                            {


                            }
                                
                        }
                

                        db.stp_UpdateDriverCommissionWithReason(CommissionId, CurrentBalance, CommissionPay, CommissionPayReasonId);
                    }

                    if (IsNewReason)
                    {
                        ComboFunctions.FillCommissionReasonCombo(ddlCommissionPayReason);
                    }
                
              

                    spnCommissionPay.Value = 0.00m;

                    DisplayRecord();
        

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPaymentHistory_Click(object sender, EventArgs e)
        {
            try
            {
                if(ddlDrivers.SelectedValue==null)
                {

                    ENUtils.ShowMessage("Please select a driver");
                }
                else
                {

                    frmSearchDriverCommissionPaymentHistory frm=new frmSearchDriverCommissionPaymentHistory("",General.GetQueryable<DriverCommission_PaymentHistory>(c=>c.Fleet_DriverCommision.DriverId== ddlDrivers.SelectedValue.ToInt()).OrderBy(c=>c.PaymentDate).ToList());

                    frm.StartPosition= FormStartPosition.CenterScreen;
                    frm.ShowDialog();


                    frm.Dispose();
                    frm=null;

                    GC.Collect();



                }
            }
            catch(Exception ex)
            {

            }
        }

        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOther.Checked)
            {
                txtReason.Visible = true;
                ddlCommissionPayReason.Visible = false;
                ddlCommissionPayReason.SelectedValue = null;
            }
            else
            {
                txtReason.Visible = false;
                ddlCommissionPayReason.Visible = true;

            }
        }
    }
}
