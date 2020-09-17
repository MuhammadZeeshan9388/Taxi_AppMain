using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Docking;


namespace Taxi_AppMain
{
    public partial class frmCashAccountStatementReport : UI.SetupBase
    {

        string reportType = string.Empty;

        public struct COLS
        {
            public static string ID = "ID";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string RefNumber = "RefNumber";

            public static string Driver = "Driver";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";

            public static string Charges = "Charges";

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string Total = "Total";
            public static string Commission = "Commission";
            public static string DriverCommission = "DriverCommission";
        }


        public frmCashAccountStatementReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDriverReport_Load);

            grdLister.EnableHotTracking = false;
            //grdLister.AutoCellFormatting = true;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);

            grdLister.ShowGroupPanel = false;

            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);


            grdLister.EnableAlternatingRowColor = true;
            grdLister.TableElement.AlternatingRowColor = Color.AliceBlue;
           
        }


        Font oldFont = new Font("Tahoma", 10, FontStyle.Regular);

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

        string cellValue = string.Empty;
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

            else if (e.CellElement is GridFilterCellElement)
            {
                e.CellElement.Font = oldFont;
                e.CellElement.NumberOfColors = 1;
                e.CellElement.BackColor = Color.White;
                e.CellElement.RowElement.BackColor = Color.White;
                e.CellElement.RowElement.NumberOfColors = 1;

                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;
            }

            else if (e.CellElement is GridDataCellElement)
            {


          
                e.CellElement.ToolTipText = e.CellElement.Text;
                e.CellElement.BorderColor = Color.DarkSlateBlue;
                e.CellElement.BorderColor2 = Color.DarkSlateBlue;
                e.CellElement.BorderColor3 = Color.DarkSlateBlue;
                e.CellElement.BorderColor4 = Color.DarkSlateBlue;

                e.CellElement.BorderBoxStyle = BorderBoxStyle.SingleBorder;

                e.CellElement.ForeColor = Color.Black;

                e.CellElement.Font = oldFont;


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
                    long id = row.Cells[COLS.ID].Value.ToLong();
                    decimal fare = row.Cells[COLS.Charges].Value.ToDecimal();
                    decimal parking = row.Cells[COLS.Parking].Value.ToDecimal();
                    decimal waiting = row.Cells[COLS.Waiting].Value.ToDecimal();
                    decimal extraDrop = row.Cells[COLS.ExtraDrop].Value.ToDecimal();
                    decimal meetAndGreet = row.Cells[COLS.MeetAndGreet].Value.ToDecimal();
                    decimal CongtionCharge = row.Cells[COLS.CongtionCharge].Value.ToDecimal();
                    decimal TotalCharges = row.Cells[COLS.Total].Value.ToDecimal();


                    BookingBO objMaster = new BookingBO();
                    objMaster.GetByPrimaryKey(id);

                    if (objMaster.Current != null)
                    {
                        objMaster.Current.FareRate = fare;
                        objMaster.Current.ParkingCharges = parking;
                        objMaster.Current.WaitingCharges = waiting;
                        objMaster.Current.ExtraDropCharges = extraDrop;
                        objMaster.Current.MeetAndGreetCharges = meetAndGreet;
                        objMaster.Current.CongtionCharges = CongtionCharge;
                        objMaster.Current.TotalCharges = TotalCharges;


                        objMaster.Save();

                        ViewReport();
                    }


                }


            }
           
        }

        void frmDriverReport_Load(object sender, EventArgs e)
        {

        }

        private void AddUpdateColumn(RadGridView grid)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Width = 50;

            col.Name = "btnUpdate";
            col.UseDefaultText = true;
            col.ImageLayout = System.Windows.Forms.ImageLayout.Center;
            col.DefaultText = "Update";
            col.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;

            grid.Columns.Add(col);

        }


        private void ViewReport()
        {
            try
            {
                reportType = optDetail.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On ? "Detailed" : "Summary";
                bool show = true;
                if (reportType == "Summary")
                {
                    show = false;
                }

                int companyId = ddlCompany.SelectedValue.ToInt();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;
                if (companyId == 0)
                {
                    error += "Required : Company";
                }

                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";


                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }

                lblCriteria.Text = "Account Report Related to '" + ddlCompany.Text.ToStr()
                    + "'                   Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);






                var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED);


                decimal appCommission = AppVars.objPolicyConfiguration.DriverCommissionPerBooking.ToDecimal();

                var list = (from a in query.AsEnumerable()
                            where a.CompanyId == companyId

                                && (a.PickupDateTime.ToDate() >= fromDate && a.PickupDateTime.ToDate() <= tillDate)
                            orderby a.PickupDateTime descending
                            select new
                            {
                                Id = a.Id,
                                PickUpDate = a.PickupDateTime,
                                RefNumber = a.BookingNo,
                                Vehicle = a.Fleet_VehicleType.VehicleType,
                                Drv = a.DriverId != null ? a.Fleet_Driver.DriverNo : "",
                                DriverName=a.DriverId!=null?a.Fleet_Driver.DriverNo+"-"+a.Fleet_Driver.DriverName:"",
                                PickupPoint = a.FromAddress,
                                Destination = a.ToAddress,
                                Charge = a.FareRate,
                                Parking = a.ParkingCharges.ToDecimal(),
                                Waiting = a.WaitingCharges.ToDecimal(),
                                ExtraDrop = a.ExtraDropCharges.ToDecimal(),
                                MeetAndGreet = a.MeetAndGreetCharges.ToDecimal(),
                                CongtionCharge = a.CongtionCharges.ToDecimal(),
                                Total = a.TotalCharges.ToDecimal(),
                                DriverCommission = a.IsCommissionWise == false ? a.Fleet_Driver.DefaultIfEmpty().DriverCommissionPerBooking.ToDecimal() : a.DriverCommission,
                                DriverCommissionType = a.DriverCommissionType
                            }).ToList();

                // grdLister.DataSource = list;
                grdLister.RowCount = list.Count;

                for (int i = 0; i < list.Count; i++)
                {
                    grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                    grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i].PickUpDate;
                    grdLister.Rows[i].Cells[COLS.RefNumber].Value = list[i].RefNumber.ToStr();
                    grdLister.Rows[i].Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();
                  

                    if (reportType == "Summary")
                    {
                        grdLister.Rows[i].Cells[COLS.Driver].Value = list[i].DriverName.ToStr();

                    }
                    else
                    {
                        grdLister.Rows[i].Cells[COLS.Driver].Value = list[i].Drv.ToStr();

                    }

                    grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].PickupPoint.ToStr();
                    grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].Destination.ToStr();
                    grdLister.Rows[i].Cells[COLS.Charges].Value = list[i].Charge.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Parking].Value = list[i].Parking.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Waiting].Value = list[i].Waiting.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.ExtraDrop].Value = list[i].ExtraDrop.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.MeetAndGreet].Value = list[i].MeetAndGreet.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.CongtionCharge].Value = list[i].CongtionCharge.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Total].Value = list[i].Total.ToDecimal();

                    grdLister.Rows[i].Cells[COLS.DriverCommission].Value = list[i].DriverCommission.ToDecimal();
                    grdLister.Rows[i].Cells[COLS.Commission].Value = list[i].DriverCommission.ToDecimal();

                }

                decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Commission].Value.ToDecimal());
                string total = totalEarning.ToStr();
                lblTotalEarning.Text = "Total Earning £ " + total;

                lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();





              
                   

                    grdLister.Columns[COLS.RefNumber].IsVisible = show;
                    grdLister.Columns[COLS.PickupPoint].IsVisible = show;
                    grdLister.Columns[COLS.Destination].IsVisible = show;
                    grdLister.Columns[COLS.CongtionCharge].IsVisible = show;
                    grdLister.Columns[COLS.MeetAndGreet].IsVisible = show;
                    grdLister.Columns[COLS.ExtraDrop].IsVisible = show;
                    grdLister.Columns[COLS.Parking].IsVisible = show;
                    grdLister.Columns[COLS.Waiting].IsVisible = show;
                    grdLister.Columns[COLS.Vehicle].IsVisible = show;

                  

                
                 

            }
            catch (Exception ex)
            {


            }
        }


     


        private void btnViewReport_Click(object sender, EventArgs e)
        {
            ViewReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Print();
        }

        public override void Print()
        {
            try
            {
                int? companyId = ddlCompany.SelectedValue.ToIntorNull();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;

                if (companyId == null)
                {
                    error += "Required : Account";
                }

                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";


                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }







                rptfrmCashAccStatement frm = new rptfrmCashAccStatement();

                frm.ReportType = this.reportType;
                frm.DataSource = GetDataSource(companyId, fromDate, tillDate);



                frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                //   frm.StatementType = statementType;
                frm.GenerateReport();

                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmCashAccStatement1");

                if (doc != null)
                {
                    doc.Close();
                }
                UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {


            }
        }


        private List<Vu_BookingBase> GetDataSource(int? companyId, DateTime? fromDate, DateTime? tillDate)
        {
            return General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED && c.CompanyId == companyId)
                .Where(b => (b.PickupDateTime.Value.Date >= fromDate && b.PickupDateTime.Value.Date <= tillDate))
                        .OrderByDescending(c => c.PickupDateTime).ToList();

        }

        public  struct eStatementType
        {
            public static int AccountStatement=1;
            public static int CashStatement=2;
            public static int Both = 3;
            public static int CashAccountStatement = 4;
         
            
        } ;

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                int? companyId = ddlCompany.SelectedValue.ToIntorNull();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;

                if (companyId == null)
                {
                    error += "Required : Account";
                }

                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";


                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }







                rptfrmCashAccStatement frm = new rptfrmCashAccStatement();

                frm.ReportType = this.reportType;
                frm.DataSource = GetDataSource(companyId, fromDate, tillDate);



                frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                //   frm.StatementType = statementType;
                frm.GenerateReport();

                frm.ExportReport();

            }
            catch (Exception ex)
            {


            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            try
            {
                int? companyId = ddlCompany.SelectedValue.ToIntorNull();
                DateTime? fromDate = dtpFromDate.Value.ToDate();
                DateTime? tillDate = dtpTillDate.Value.ToDate();

                string error = string.Empty;

                if (companyId == null)
                {
                    error += "Required : Account";
                }

                if (fromDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : From Date";
                }

                if (tillDate == null)
                {
                    if (string.IsNullOrEmpty(error))
                        error += Environment.NewLine;

                    error += "Required : To Date";


                }

                if (!string.IsNullOrEmpty(error))
                {
                    ENUtils.ShowMessage(error);
                    return;

                }







                rptfrmCashAccStatement frm = new rptfrmCashAccStatement();

                frm.ReportType = this.reportType;
                frm.DataSource = GetDataSource(companyId, fromDate, tillDate);



                frm.DatePeriod = "For the Period : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", tillDate);
                //   frm.StatementType = statementType;
                frm.GenerateReport();

                frm.SendEmail();

            }
            catch (Exception ex)
            {


            }
        }

        private void frmCashAccountStatementReport_Load(object sender, EventArgs e)
        {
            try
            {
                ComboFunctions.FillCompanyCombo(ddlCompany, c => c.AccountTypeId == Enums.ACCOUNT_TYPE.CASH);
                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpTillDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());


                GridViewTextBoxColumn col = new GridViewTextBoxColumn();
                col.IsVisible = false;
                col.Name = "Id";
                grdLister.Columns.Add(col);

                GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
                colDt.Name = "PickupDate";
                colDt.ReadOnly = true;
                colDt.HeaderText = "Pickup Date-Time";
                grdLister.Columns.Add(colDt);



                col = new GridViewTextBoxColumn();
                // col.IsVisible = false;
                col.ReadOnly = true;
                col.HeaderText = "Ref #";
                col.Name = "RefNumber";
                grdLister.Columns.Add(col);

                col = new GridViewTextBoxColumn();
                // col.IsVisible = false;
                col.HeaderText = "Vehicle";
                col.Name = "Vehicle";
                col.ReadOnly = true;
                grdLister.Columns.Add(col);


                col = new GridViewTextBoxColumn();
                //  col.IsVisible = false;
                col.ReadOnly = true;
                col.Name = "Driver";
                col.HeaderText = "Driver";
                grdLister.Columns.Add(col);



                col = new GridViewTextBoxColumn();
                // col.IsVisible = false;
                col.ReadOnly = true;
                col.HeaderText = "Pickup Point";
                col.Name = "PickupPoint";
                grdLister.Columns.Add(col);



                col = new GridViewTextBoxColumn();
                //     col.IsVisible = false;
                col.ReadOnly = true;
                col.HeaderText = "Destination";
                col.Name = "Destination";
                grdLister.Columns.Add(col);





                GridViewDecimalColumn colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Charges";
                colD.Name = "Charges";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);

                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Parking";
                colD.Name = "Parking";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Waiting";
                colD.Name = "Waiting";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Extra Drop";
                colD.Name = "ExtraDrop";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "M & G";
                colD.Name = "MeetAndGreet";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "Congtion";
                colD.Name = "CongtionCharge";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);




                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.HeaderText = "DriverCommission";
                colD.Name = "DriverCommission";
                colD.Maximum = 9999999;
                colD.IsVisible = false;
                colD.FormatString = "{0:#,###0.00}";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.ReadOnly = true;
                colD.HeaderText = "Total";
                colD.Name = "Total";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                colD.Expression = "Charges+Parking+Waiting+ExtraDrop+MeetAndGreet+CongtionCharge";
                grdLister.Columns.Add(colD);


                colD = new GridViewDecimalColumn();
                colD.DecimalPlaces = 2;
                colD.Minimum = 0;
                colD.ReadOnly = true;
                colD.HeaderText = "Commission";
                colD.Name = "Commission";
                colD.Maximum = 9999999;
                colD.FormatString = "{0:#,###0.00}";
                //   colD.Expression = "(Total*DriverCommission)/100";

                grdLister.Columns.Add(colD);



                //     grdLister.Columns["Id"].IsVisible = false;

                (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
                (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


                grdLister.Columns["PickUpDate"].Width = 90;
                grdLister.Columns["RefNumber"].Width = 40;
                grdLister.Columns["Vehicle"].Width = 50;
                grdLister.Columns["Driver"].Width = 60;
                grdLister.Columns["PickUpPoint"].Width = 90;
                grdLister.Columns["Destination"].Width = 90;

                grdLister.Columns["Charges"].Width = 45;
                grdLister.Columns["Parking"].Width = 45;
                grdLister.Columns["Waiting"].Width = 50;
                grdLister.Columns["ExtraDrop"].Width = 55;
                grdLister.Columns["MeetAndGreet"].Width = 50;
                grdLister.Columns["CongtionCharge"].Width = 55;
                grdLister.Columns["Total"].Width = 40;
                grdLister.Columns["Commission"].Width = 55;

                grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
                grdLister.Columns["RefNumber"].HeaderText = "Ref #";
                grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
                grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";

                grdLister.Columns["MeetAndGreet"].HeaderText = "M & G";
                grdLister.Columns["CongtionCharge"].HeaderText = "Congtion";

                //grdLister.Columns["MeetAndGreet"].ReadOnly = false;
                //grdLister.Columns["CongtionCharge"].ReadOnly = false;
                //grdLister.Columns["Charge"].ReadOnly = false;
                //grdLister.Columns["Parking"].ReadOnly = false;
                //grdLister.Columns["Waiting"].ReadOnly = false;
                //grdLister.Columns["ExtraDrop"].ReadOnly = false;

                AddUpdateColumn(grdLister);

                grdLister.AllowEditRow = true;
            }
            catch (Exception ex)
            {


            }
        }
    }
}
