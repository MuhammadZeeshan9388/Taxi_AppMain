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
    public partial class frmDriverPaymentAccountBookingsHistory : UI.SetupBase
    {

        public frmDriverPaymentAccountBookingsHistory()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmPaymentCollection_Load);
            this.grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            this.btnExit1.Click += new EventHandler(btnExit1_Click);
            this.btnExportExcel.Click += new EventHandler(btnExportExcel_Click);
            this.grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);
            this.btnShow.Click += new EventHandler(btnShow_Click);


            grdLister.AllowAddNewRow = false;
            grdLister.AllowEditRow = true;
            grdLister.ShowGroupPanel = false;
            grdLister.EnableHotTracking = false;
            grdLister.EnableFiltering = true;


            optCommission.CheckedChanged += new EventHandler(optCommission_CheckedChanged);
            this.btnDeleteAll.Click += new EventHandler(btnDeleteAll_Click);
        }

        void btnDeleteAll_Click(object sender, EventArgs e)
        {
            DeletePaymentHistory();
        }
        private void DeletePaymentHistory()
        {
            try
            {
                string Error = string.Empty;
                if (grdLister.Rows.Count() > 0 && grdLister.Rows.Count(c => c.Cells["Check"].Value.ToBool() == true) == 0)
                {
                    Error = "Please select record to delete";
                }
              
                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }

                int Id = 0;
                int TransId = 0;
                long TranId = 0;
                if (grdLister.Rows.Count(c => c.Cells["Check"].Value.ToBool() == true) > 0)
                {
                    if (DialogResult.Yes == RadMessageBox.Show("Are you sure you want to delete this Record ?", "", MessageBoxButtons.YesNo, RadMessageIcon.Question))
                    {
                        foreach (var item in grdLister.Rows.Where(c => c.Cells["Check"].Value.ToBool() == true))
                        {
                            //long TranId = row.Cells["TranId"].Value.ToLong();
                            Id = item.Cells["Id"].Value.ToInt();
                            TransId = item.Cells["TransId"].Value.ToInt();
                            TranId = item.Cells["TransId"].Value.ToLong();
                            if (TranId > 0)
                            {
                                try
                                {
                                    //int Id = row.Cells["Id"].Value.ToInt();
                                    //int TransId = row.Cells["TranId"].Value.ToInt();
                                    using (TaxiDataContext db = new TaxiDataContext())
                                    {
                                        if (db.GetTable<Fleet_DriverWeeklyRentHistory_Detail>().Count(c => c.TransId == TransId) == 1)
                                        {
                                            Fleet_DriverWeeklyRentHistory objHistory = db.GetTable<Fleet_DriverWeeklyRentHistory>().FirstOrDefault(c => (c.Id == TranId));
                                            if (objHistory != null)
                                            {
                                                db.Fleet_DriverWeeklyRentHistories.DeleteOnSubmit(objHistory);
                                                db.SubmitChanges();
                                            }
                                        }
                                        else
                                        {
                                            Fleet_DriverWeeklyRentHistory_Detail objDetail = db.Fleet_DriverWeeklyRentHistory_Details.FirstOrDefault(c => c.Id == Id);
                                            if (objDetail != null)
                                            {
                                                db.Fleet_DriverWeeklyRentHistory_Details.DeleteOnSubmit(objDetail);
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
                        }
                        PopulateData();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        void btnShow_Click(object sender, EventArgs e)
        {
            PopulateData();
        }
        private void FillCombo()
        {
            ComboFunctions.FillDriverWeeklyRentHistoryCombo(ddlDateRange);
            ComboFunctions.FillDriverNoCombo(ddlDriver);

            //ComboFunctions.FillDriverNoCombo(ddlDriver, c => c.DriverTypeId == 1);
        }
        void optCommission_CheckedChanged(object sender, EventArgs e)
        {
            //PopulateData();
        }


        void grdLister_CommandCellClick(object sender, EventArgs e)
        {

            GridCommandCellElement gridCell = (GridCommandCellElement)sender;
            if (gridCell.ColumnInfo.Name == "btnDelete")
            {

                GridViewRowInfo row = gridCell.RowInfo;

                if (row is GridViewDataRowInfo)
                {
                    long TranId = row.Cells["TransId"].Value.ToLong();


                    if (TranId > 0)
                    {


                        try
                        {

                            int Id = row.Cells["Id"].Value.ToInt();
                            int TransId = row.Cells["TransId"].Value.ToInt();

                            using (TaxiDataContext db = new TaxiDataContext())
                            {


                                if (db.GetTable<Fleet_DriverWeeklyRentHistory_Detail>().Count(c => c.TransId == TransId) == 1)
                                {

                                    Fleet_DriverWeeklyRentHistory objHistory = db.GetTable<Fleet_DriverWeeklyRentHistory>().FirstOrDefault(c =>
                                                  (c.Id == TranId));


                                    if (objHistory != null)
                                    {

                                        db.Fleet_DriverWeeklyRentHistories.DeleteOnSubmit(objHistory);
                                        db.SubmitChanges();
                                    }

                                }
                                else
                                {

                                    Fleet_DriverWeeklyRentHistory_Detail objDetail = db.Fleet_DriverWeeklyRentHistory_Details.FirstOrDefault(c => c.Id == Id);


                                    if (objDetail != null)
                                    {

                                        db.Fleet_DriverWeeklyRentHistory_Details.DeleteOnSubmit(objDetail);
                                        db.SubmitChanges();

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
                saveFileDialog1.FileName = "Driver Weekly Rent History";


                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    grdLister.Columns["IsPaid"].IsVisible = false;
                    grdLister.Columns["Paid"].IsVisible = true;
                    //grdLister.Columns["btnUpdate"].IsVisible = false;

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
                    heading = "Driver Weekly Rent History - " + string.Format("from {0:dd/MM/yyyy}", dtCurrent) + " until " + string.Format("{0:dd/MM/yyyy}", dtEnd);

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
            GridViewCheckBoxColumn ccc = new GridViewCheckBoxColumn();
            ccc.Name = "Check";
            ccc.Width = 80;
            ccc.ReadOnly = false;
            grdLister.Columns.Add(ccc);

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
            cbcol.IsVisible = false;
            grdLister.Columns.Add(cbcol);



            col = new GridViewTextBoxColumn();
            col.Name = "Paid";
            col.HeaderText = "Paid";
            col.Width = 120;
            col.ReadOnly = true;
            //col.IsVisible = false;
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
            cmdcol.Name = "btnDelete";
            cmdcol.UseDefaultText = true;
            cmdcol.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            cmdcol.DefaultText = "Delete";
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

        }

        void frmPaymentCollection_Load(object sender, EventArgs e)
        {
            FormatGrid();
            FillCombo();
            //PopulateData();
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

                int RangeId = ddlDateRange.SelectedValue.ToInt();
                int DriverId = ddlDriver.SelectedValue.ToInt();
                string Error = string.Empty;
                if (RangeId == 0)
                {
                    Error = "Required: Date Range";
                }

                if (!string.IsNullOrEmpty(Error))
                {
                    ENUtils.ShowMessage(Error);
                    return;
                }
                int idx = 0;
                int val = 0;


                int driverTypeId = optRent.Checked ? Enums.DRIVERTYPES.RENT : Enums.DRIVERTYPES.COMMISSION;

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var list = (from a in db.stp_GetDriverWeeklyRentHistory(RangeId, DriverId)
                                select new
                                {
                                    a.Id,
                                    a.TransId,
                                    a.Active,
                                    a.AccountBookings,
                                    a.RentComm,
                                    //  a.DriverName,
                                    a.DriverNo,
                                    a.DriverToPay,
                                    a.OfficeToPay,
                                    a.PreviousBalance,
                                    IsPaid = a.IsPaid,
                                    Paid = a.IsPaid != null && a.IsPaid == true ? "Paid" : "",
                                    a.FromDate,
                                    ToDate = a.TillDate,
                                    PaidValue = a.IsPaid != null && a.IsPaid == true ? 1 : 0
                                }).ToList();
                    //var list = (from a in db.stp_DriverPaymentAccountBookings(driverTypeId, 115.00m)
                    //            select new
                    //              {
                    //                  a.Id,
                    //                  a.TransId,
                    //                  a.Active,
                    //                  a.AccountBookings,
                    //                  a.TotalRentCommission,
                    //                  a.DriverName,
                    //                  a.DriverNo,
                    //                  a.DriverToPay,
                    //                  a.OfficeToPay,
                    //                  a.PreviousBalance,
                    //                  IsPaid = a.IsWeeklyPaid,
                    //                  Paid = a.IsWeeklyPaid != null && a.IsWeeklyPaid == true ? "Paid" : "",
                    //                  a.FromDate,
                    //                  a.ToDate,
                    //                  PaidValue=a.IsWeeklyPaid != null && a.IsWeeklyPaid == true ?1:0

                    //              }).ToList();
                    if (list.Count == 0)
                    {
                        grdLister.RowCount = 0;
                        return;
                    }
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


                        grdLister.Rows[i].Cells["TotalRentCommission"].Value = list2[i].RentComm;
                        grdLister.Rows[i].Cells["PaidTotalRentCommission"].Value = list2[i].IsPaid.ToBool() ? list2[i].RentComm : 0.00m;

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
                        grdLister.Rows[i].Cells["Check"].Value = false;
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
            grdLister.Columns["IsPaid"].IsVisible = false;
            grdLister.Columns["Paid"].IsVisible = true;
            //grdLister.Columns["IsPaid"].IsVisible = true;
            //grdLister.Columns["Paid"].IsVisible = false;
            // grdLister.Columns["btnUpdate"].IsVisible = true;

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
                this.FormTitle = "Driver Weekly Rent History - " + string.Format("from {0:dd/MM/yyyy}", dtCurrent) + " until " + string.Format("{0:dd/MM/yyyy}", dtEnd);



            }
            catch
            {



            }
        }

        
        private void cbAllCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAllCompany.Checked == true)
            {
                if (grdLister.Rows.Count > 0)
                {
                    for (int i = 0; i < grdLister.Rows.Count; i++)
                    {
                        grdLister.Rows[i].Cells["Check"].Value = true;//..CurrentCell.Value;
                    }
                }
            }
            else if (cbAllCompany.Checked == false)
            {
                if (grdLister.Rows.Count > 0)
                {
                    for (int i = 0; i < grdLister.Rows.Count; i++)
                    {
                        grdLister.Rows[i].Cells["Check"].Value = false;//..CurrentCell.Value;

                    }
                }
            }
        }

       

        private void btnDeleteHistory_Click(object sender, EventArgs e)
        {
            try
            {
                int RangeId = ddlDateRange.SelectedValue.ToInt();


                if (RangeId == 0)
                {

                    ENUtils.ShowMessage("Please select a DateRange History");
                    return;
                }

                using (TaxiDataContext db = new TaxiDataContext())
                {

                    Fleet_DriverWeeklyRentHistory objHistory = db.GetTable<Fleet_DriverWeeklyRentHistory>().FirstOrDefault(c =>
                                  (c.Id == RangeId));


                    if (objHistory != null)
                    {

                        db.Fleet_DriverWeeklyRentHistories.DeleteOnSubmit(objHistory);
                        db.SubmitChanges();
                    }


                }

                ComboFunctions.FillDriverWeeklyRentHistoryCombo(ddlDateRange);
                //ComboFunctions.FillDriverCommissionCollectionHistoryCombo(ddlDateRange);
                grdLister.Rows.Clear();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

        }
       



    }
}
