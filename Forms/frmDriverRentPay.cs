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
    public partial class frmDriverRentPay : UI.SetupBase
    {
        bool IsDriverLoaded = false;
        long RentId = 0;
        public frmDriverRentPay()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmAddDriverRent_Load);
            this.ddlDrivers.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlDrivers_SelectedIndexChanged);
            this.FormClosed += new FormClosedEventHandler(frmDriverRentPay_FormClosed);
            this.KeyDown += new KeyEventHandler(frmDriverRentPay_KeyDown);
            this.KeyPreview = true;

            spnRentPay.SpinElement.TextChanging += new TextChangingEventHandler(SpinElement_TextChanging);

        }

        void SpinElement_TextChanging(object sender, TextChangingEventArgs e)
        {
            if (e.NewValue != "" && e.NewValue!="-")
            {
                numCurrBalance.Value = lblRentDue.Text.ToDecimal() + e.NewValue.ToDecimal();
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
                RentId = 0;

             

                lblTitle.Visible = false;
                lblTransactionNo.Visible = false;
                txtRentPaid.Visible = false;
                txtRentPaidSign.Visible = false;
                txtIsPaid.Visible = false;
                ddlRentPayReason.SelectedIndex = 0;


                DriverId = ddlDrivers.SelectedValue.ToInt();

                var item = (from a in General.GetQueryable<DriverRent>(c => c.DriverId == DriverId)
                            orderby a.Id descending
                            select new
                            {
                                Id = a.Id,
                                BalanceDue = a.Balance,
                                TransactionNo = a.TransNo,
                                PeriodFrom = a.FromDate,
                                PeriodTo = a.ToDate,
                                RentPaid = a.RentPay,
                                PayHistory = a.DriverRent_PaymentHistories
                            }).FirstOrDefault();


                if (item != null)
                {

                    lblTitle.Visible = true;
                    lblTransactionNo.Visible = true;


                    lblRentDue.Text = item.BalanceDue.ToStr();

                    numCurrBalance.Value = item.BalanceDue.ToDecimal();

                    RentId = item.Id;

                    lblTitle.Text = "Statment No:";
                    lblTransactionNo.Text = item.TransactionNo;

                    if (item.PeriodFrom != null)
                        lblTransactionNo.Text += " , Period : " + string.Format("{0:dd/MM/yyyy}", item.PeriodFrom);


                    if (item.PeriodTo != null)
                        lblTransactionNo.Text += " to : " + string.Format("{0:dd/MM/yyyy}", item.PeriodTo);

                    if (item.RentPaid.ToDecimal() > 0)
                    {

                        txtRentPaid.Visible = true;
                        txtRentPaidSign.Visible = true;
                        txtIsPaid.Visible = true;
                        txtRentPaid.Text = item.RentPaid.ToStr();

                    }

                    btnSave.Enabled = true;


                    ShowPaymentHistory(item.PayHistory.ToList());

                }
                else
                {



                    lblRentDue.Text = "0";
                    numCurrBalance.Value = 0.00m;

                    spnRentPay.Value = 0;

                    lblerror.ForeColor = Color.Red;
                    lblerror.Text = "Driver Rent is not generated";
                    btnSave.Enabled = false;
                    grdPaymentHistory.Rows.Clear();

                }


                chkOther.Checked = false;
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        private void ShowPaymentHistory(List<DriverRent_PaymentHistory> list)
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
                     row.Cells["RentPay"].Value=item.RentPay.ToDecimal();
                     row.Cells["BalanceDue"].Value=item.BalanceDue.ToDecimal();
                     row.Cells["Reason"].Value=item.DriverRentPayReason.DefaultIfEmpty().RentReason.ToStr();


                }
            }
            catch(Exception ex)
            {

            }

        }

        void frmAddDriverRent_Load(object sender, EventArgs e)
        {
            FillCombo();
            IsDriverLoaded = true;
        }
        private void FillCombo()
        {
            ComboFunctions.FillDriverNoCombo(ddlDrivers, c => c.DriverTypeId == 1);
            ComboFunctions.FillRentPayReasonCombo(ddlRentPayReason);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateDriverRent();
        }
        private void UpdateDriverRent()
        {
            try
            {
                if (RentId == 0)
                {
                    ENUtils.ShowMessage("Driver Rent is not Generated");
                    return;
                }



                if (chkOther.Checked)
                    ddlRentPayReason.SelectedValue = null;

                decimal RentDue=lblRentDue.Text.ToDecimal();
                decimal RentPay= spnRentPay.Value.ToDecimal();
                decimal RentPaid = (RentPay + RentDue);
                decimal CurrentBalance = numCurrBalance.Value.ToDecimal();

                int? rentPayReasonId = ddlRentPayReason.SelectedValue.ToIntorNull();
                string rentPayReasonText = txtReason.Text.Trim();
                bool IsNewReason = false;
                 
              


               
                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        if (rentPayReasonId == null && !string.IsNullOrEmpty(rentPayReasonText))
                        {
                            try
                            {

                                DriverRentPayReason objRentPayReason = new DriverRentPayReason();
                                rentPayReasonId = db.DriverRentPayReasons.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
                                objRentPayReason.Id = rentPayReasonId.ToInt();
                                objRentPayReason.RentReason = rentPayReasonText;


                                db.DriverRentPayReasons.InsertOnSubmit(objRentPayReason);
                                db.SubmitChanges();

                                IsNewReason = true;
                            }
                            catch
                            {


                            }
                                
                        }



                        db.stp_UpdateDriverRentWithReason(RentId, CurrentBalance, RentPay,rentPayReasonId);


                    }



                    if (IsNewReason)
                    {

                        ComboFunctions.FillRentPayReasonCombo(ddlRentPayReason);
                    }
                
                //else
                //{

                //    DriverRentBO rentBo = new DriverRentBO();
                //    rentBo.GetByPrimaryKey(RentId);

                //    if (rentBo.Current != null)
                //    {
                //        rentBo.Current.RentPayReasonId = ddlRentPayReason.SelectedValue.ToIntorNull();
                //        rentBo.Current.Balance

                //    }

                //}

                    DisplayRecord();
             //   this.Close();

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

                    frmSearchDriverRentPaymentHistory frm=new frmSearchDriverRentPaymentHistory("",General.GetQueryable<DriverRent_PaymentHistory>(c=>c.DriverRent.DriverId== ddlDrivers.SelectedValue.ToInt()).OrderBy(c=>c.PaymentDate).ToList());

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
                ddlRentPayReason.Visible = false;
                ddlRentPayReason.SelectedValue = null;
            }
            else
            {
                txtReason.Visible = false;
                ddlRentPayReason.Visible = true;

            }
        }
    }
}
