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
using System.IO;
using Microsoft.Reporting.WinForms;

namespace Taxi_AppMain
{
    public partial class frmCompanyInvoiceLastStatement : UI.SetupBase
    {
        private bool IsFareAndWaitingWiseComm;
        public frmCompanyInvoiceLastStatement()
        {
            InitializeComponent();
            FormatGrid();

            grdDriverRentLastStatement.EnableFiltering = true;
            this.Load += new EventHandler(frmDriverRentLastStatement_Load);
            this.chkAll.CheckedChanged += new EventHandler(chkAll_CheckedChanged);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnPrint.Click += new EventHandler(btnPrint_Click);

        }

        void btnPrint_Click(object sender, EventArgs e)
        {
            PrintStatement();
        }
        private void PrintStatement()
        {
            try
            {
                if (grdDriverRentLastStatement.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).Count() > 0)
                {
                    //foreach (var item in grdDriverRentLastStatement.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true))
                    //{
                    //    long Id = item.Cells[COLS.Id].Value.ToLong();
                    //    ReportPrintDocument rpt = null;
                    //    frmDriverCommisionTransactionExpensesReport3 frm = null;
                    //    if (Id > 0)
                    //    {
                    //        frm = new frmDriverCommisionTransactionExpensesReport3(1);
                    //        var list = General.GetQueryable<vu_DriverCommisionExpenses2>(a => a.Id == Id).OrderBy(c => c.PickupDate).ToList();
                    //        int count = list.Count;

                    //        frm.DataSource = list;
                    //        var list2 = General.GetQueryable<vu_FleetDriverCommissionExpense>(c => c.CommissionId == Id).OrderBy(c => c.Date).ToList();
                    //        frm.DataSource2 = list2;

                    //        frm.IsFareAndWaitingWise = this.IsFareAndWaitingWiseComm;

                    //        frm.GenerateReport();
                    //        rpt = new ReportPrintDocument(frm.reportViewer1.LocalReport);
                    //        rpt.Print();
                    //        rpt.Dispose();
                    //    }
                    //}



                    var rows = grdDriverRentLastStatement.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).ToList();

                    var list = (from a in rows.Where(c => c.Cells[COLS.Id].Value.ToInt() > 0)
                                select new
                                {
                                    Id = a.Cells[COLS.Id].Value.ToInt(),
                                    CompanyId = a.Cells[COLS.DriverId].Value.ToInt(),
                                    Driver = a.Cells[COLS.DriverNo].Value.ToStr()
                                }).ToList();

                    frmInvoiceReport frm = new frmInvoiceReport();
                    ReportPrintDocument rpt = null;
                    foreach (var item in list)
                    {
                        if (item.Id > 0)
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                var listInv = db.vu_Invoices.Where(a => a.Id == item.Id).OrderBy(c => c.PickupDate).ToList();

                                frm.DataSource = listInv;

                                var Invoice = db.Invoices.Where(c => c.Id == item.Id).FirstOrDefault();
                                frm.ObjInvoice = Invoice;

                                //var Inv=
                                //frm.ObjInvoice = listInv.;
                                frm.GenerateReport();
                                rpt = new ReportPrintDocument(frm.reportViewer1.LocalReport);
                                rpt.Print();
                                rpt.Dispose();
                            }
                        }
                    }


                    //if (list.Count > 0)
                    //{
                    //    frmDriverCommisionTransactionExpensesReport4 frm = new frmDriverCommisionTransactionExpensesReport4(list, DateTime.Now.ToDate(), DateTime.Now.ToDate());
                    //    frm.ShowDialog();
                    //    frm.Dispose();
                    //}


                }
                else
                {
                    ENUtils.ShowMessage("Please select statement to Print");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void btnExit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }
        private void DeleteTransaction()
        {
            try
            {
                if (grdDriverRentLastStatement.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).Count() > 0)
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete Company Invoice ? ", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        var Id = string.Join(",", grdDriverRentLastStatement.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true).Select(c => c.Cells[COLS.Id].Value.ToStr().ToUpper()).ToArray<string>());
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_RunProcedure("delete from Invoice where Id in(" + Id + ")");
                        }
                        PopulateData();
                    }
                }
                else
                {
                    ENUtils.ShowMessage("Please select Invoice to delete");
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                SelectAll(true);
            }
            else
            {
                SelectAll(false);
            }
        }
        public void SelectAll(bool IsChecked)
        {
            try
            {
                foreach (var item in grdDriverRentLastStatement.ChildRows)
                {
                    item.Cells["Check"].Value = IsChecked;
                }
                //grdLister.Rows.ToList().ForEach(c => c.Cells["COLCheckBox"].Value = SelectAll);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public struct COLS
        {
            public static string Id = "Id";
            public static string DriverId = "DriverId";
            public static string DriverNo = "DriverNo";
            public static string DriverName = "DriverName";
            public static string JobsTotal = "JobsTotal";
            public static string Balance = "Balance";
            public static string OldBalance = "OldBalance";
            public static string TransDate = "TransDate";
            public static string TransNo = "TransNo";
            public static string FromDate = "FromDate";
            public static string ToDate = "ToDate";
        }
        public void FormatGrid()
        {

            grdDriverRentLastStatement.ShowRowHeaderColumn = false;
            GridViewCheckBoxColumn col = new GridViewCheckBoxColumn();
            col.Width = 70;
            col.AutoSizeMode = BestFitColumnMode.None;
            col.HeaderText = "";
            col.Name = "Check";
            //  col.ReadOnly = true;
            grdDriverRentLastStatement.Columns.Add(col);
            GridViewTextBoxColumn colId = new GridViewTextBoxColumn();
            colId.Name = COLS.Id;
            colId.IsVisible = false;
            grdDriverRentLastStatement.Columns.Add(colId);
            colId = new GridViewTextBoxColumn();
            colId.Name = COLS.DriverId;
            colId.IsVisible = false;
            grdDriverRentLastStatement.Columns.Add(colId);
            // colId = new GridViewTextBoxColumn();
            //colId.Name = COLS.DriverName;
            //colId.IsVisible=false;
            //grdDriverRentLastStatement.Columns.Add(colId);

            colId = new GridViewTextBoxColumn();
            colId.Width = 120;
            colId.HeaderText = "Code";
            colId.Name = COLS.DriverNo;
            colId.ReadOnly = true;
            grdDriverRentLastStatement.Columns.Add(colId);

            colId = new GridViewTextBoxColumn();
            colId.Width = 120;
            colId.HeaderText = "Company Name";
            colId.Name = COLS.DriverName;
            colId.ReadOnly = true;
            grdDriverRentLastStatement.Columns.Add(colId);
            colId = new GridViewTextBoxColumn();
            colId.Width = 100;
            colId.HeaderText = "Invoice No";
            colId.Name = COLS.TransNo;
            colId.ReadOnly = true;
            grdDriverRentLastStatement.Columns.Add(colId);
            GridViewDateTimeColumn dtcol = new GridViewDateTimeColumn();
            dtcol.Name = COLS.TransDate;
            dtcol.HeaderText = "Invoice Date";
            dtcol.FormatString = "{0:dd/MM/yyyy}";

            dtcol.Width = 120;
            dtcol.ReadOnly = true;
            grdDriverRentLastStatement.Columns.Add(dtcol);
            dtcol = new GridViewDateTimeColumn();
            dtcol.Name = COLS.FromDate;
            dtcol.HeaderText = "From Date";
            dtcol.FormatString = "{0:dd/MM/yyyy}";
            dtcol.ReadOnly = true;
            dtcol.Width = 120;
            grdDriverRentLastStatement.Columns.Add(dtcol);
            dtcol = new GridViewDateTimeColumn();
            dtcol.Name = COLS.ToDate;
            dtcol.HeaderText = "To Date";
            dtcol.FormatString = "{0:dd/MM/yyyy}";

            dtcol.ReadOnly = true;
            dtcol.Width = 120;
            grdDriverRentLastStatement.Columns.Add(dtcol);
            GridViewDecimalColumn dcol = new GridViewDecimalColumn();
            //dcol.Name = COLS.OldBalance;
            //dcol.HeaderText = "Old Balance";
            //dcol.Width = 120;
            //dcol.ReadOnly = true;
            //grdDriverRentLastStatement.Columns.Add(dcol);
            //dcol = new GridViewDecimalColumn();
            //dcol.Name = COLS.Balance;
            //dcol.HeaderText = "Balance";
            //dcol.Width = 110;
            //dcol.ReadOnly = true;
            //grdDriverRentLastStatement.Columns.Add(dcol);
            //dcol = new GridViewDecimalColumn();
            dcol.Name = COLS.Balance;
            dcol.HeaderText = "Invoice Total";
            dcol.Width = 120;
            dcol.ReadOnly = true;
            grdDriverRentLastStatement.Columns.Add(dcol);
            grdDriverRentLastStatement.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        }

        void frmDriverRentLastStatement_Load(object sender, EventArgs e)
        {
            PopulateData();
        }
        public override void PopulateData()
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = db.stp_GetLastCompanyInvoice().OrderBy(item => item.CompanyName, new NaturalSortComparer<string>()).ToList();
                    grdDriverRentLastStatement.BeginUpdate();
                    grdDriverRentLastStatement.RowCount = list.Count;
                    for (int i = 0; i < list.Count; i++)
                    {
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.Id].Value = list[i].Id;
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.DriverId].Value = list[i].CompanyId;
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.DriverNo].Value = list[i].CompanyCode;
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.DriverName].Value = list[i].CompanyName;
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.TransDate].Value = list[i].InvoiceDate;
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.TransNo].Value = list[i].InvoiceNo;
                        //grdDriverRentLastStatement.Rows[i].Cells[COLS.JobsTotal].Value = list[i].JobsTotal;
                        //grdDriverRentLastStatement.Rows[i].Cells[COLS.OldBalance].Value = list[i].OldBalance;
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.FromDate].Value = list[i].FromDate;
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.ToDate].Value = list[i].TillDate;
                        grdDriverRentLastStatement.Rows[i].Cells[COLS.Balance].Value = list[i].InvoiceTotal;
                        //grdDriverRentLastStatement.Rows[i].Cells[COLS.JobsTotal].Value = list[i].Id;
                    }
                    grdDriverRentLastStatement.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
    }
}
