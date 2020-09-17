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
using Telerik.WinControls.Enumerations;


namespace Taxi_AppMain
{
    public partial class frmCompanyIncomeReport : UI.SetupBase
    {
        public struct COLS
        {
            public static string ID = "ID";
            public static string PickupDate = "PickupDate";
            public static string Vehicle = "Vehicle";
            public static string RefNumber = "RefNumber";

            public static string Account = "Account";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";
            public static string Driver = "Driver";

            public static string Charges = "Charges";

            public static string Parking = "Parking";
            public static string Waiting = "Waiting";
            public static string ExtraDrop = "ExtraDrop";
            public static string MeetAndGreet = "MeetAndGreet";
            public static string CongtionCharge = "CongtionCharge";
            public static string Total = "Total";

        }
        public frmCompanyIncomeReport()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmCompanyIncomeReport_Load);

            grdLister.EnableHotTracking = false;
            //grdLister.AutoCellFormatting = true;
            grdLister.AllowAddNewRow = false;
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            grdLister.ShowRowHeaderColumn = false;

            grdLister.CommandCellClick += new CommandCellClickEventHandler(grdLister_CommandCellClick);

            grdLister.ShowGroupPanel = false;

            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.CellDoubleClick += new GridViewCellEventHandler(grdLister_CellDoubleClick);

            grdLister.EnableAlternatingRowColor = true;
            grdLister.TableElement.AlternatingRowColor = Color.AliceBlue;

        }

        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            try
            {
             int Id = e.Row.Cells["Id"].Value.ToInt();
             frmBooking frm = new frmBooking();
             frm.OnDisplayRecord(Id);
             frm.Show();
            }
            catch (Exception ex)
            { }
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

        void frmCompanyIncomeReport_Load(object sender, EventArgs e)
        {
            //  ViewReport();
           // ComboFunctions.FillDriverNoCombo(ddlDriver);           


            TimeSpan tillTime = TimeSpan.Zero;

            TimeSpan.TryParse("23:59:59", out tillTime);

            dtpTillDate.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue()).Date);

            dtptilltime.Value = dtpTillDate.Value.Value.Date + tillTime;

            ComboFunctions.FillCompanyCombo(ddlCompany);
            ComboFunctions.FillSubCompanyCombo(ddlSubCompany);

            ddlCompany.Enabled = true;

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


            //col = new GridViewTextBoxColumn();
            ////  col.IsVisible = false;
            //col.ReadOnly = true;
            //col.Name = "Account";
            //col.HeaderText = "Account";
            //grdLister.Columns.Add(col);
            col = new GridViewTextBoxColumn();
            //     col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = COLS.Driver;
            col.Name = COLS.Driver;
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




            col = new GridViewTextBoxColumn();
            //  col.IsVisible = false;
            col.ReadOnly = true;
            col.Name = "Account";
            col.HeaderText = "Account";
            grdLister.Columns.Add(col);
            //col = new GridViewTextBoxColumn();
            ////     col.IsVisible = false;
            //col.ReadOnly = true;
            //col.HeaderText = COLS.Driver;
            //col.Name = COLS.Driver;
            //grdLister.Columns.Add(col);



            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Charges";
            colD.Name = "Charges";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Parking";
            colD.Name = "Parking";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Waiting";
            colD.Name = "Waiting";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Extra Drop";
            colD.Name = "ExtraDrop";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);


            //colD = new GridViewDecimalColumn();
            //colD.DecimalPlaces = 2;
            //colD.Minimum = 0;
            //colD.HeaderText = "M & G";
            //colD.ReadOnly = true;
            //colD.Name = "MeetAndGreet";
            //colD.Maximum = 9999999;
            //colD.FormatString = "{0:#,###0.00}";
            //grdLister.Columns.Add(colD);


            //colD = new GridViewDecimalColumn();
            //colD.DecimalPlaces = 2;
            //colD.Minimum = 0;
            //colD.ReadOnly = true;
            //colD.HeaderText = "Congestion";
            //colD.Name = "CongtionCharge";
            //colD.Maximum = 9999999;
            //colD.FormatString = "{0:#,###0.00}";
            //grdLister.Columns.Add(colD);


            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.ReadOnly = true;
            colD.HeaderText = "Total";
            colD.Name = "Total";
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            colD.Expression = "Charges+Parking+Waiting+ExtraDrop";
            grdLister.Columns.Add(colD);

            //     grdLister.Columns["Id"].IsVisible = false;

            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns["PickUpDate"] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdLister.Columns["PickUpDate"].Width = 90;
            grdLister.Columns["RefNumber"].Width = 40;
            grdLister.Columns["Vehicle"].Width = 50;
            //grdLister.Columns["Account"].Width = 60;
            grdLister.Columns[COLS.Driver].Width = 40;
            grdLister.Columns["PickUpPoint"].Width = 110;
            grdLister.Columns["Destination"].Width = 110;

           // grdLister.Columns[COLS.Driver].Width = 40;
            grdLister.Columns["Account"].Width = 60;
            grdLister.Columns["Charges"].Width = 45;
            grdLister.Columns["Parking"].Width = 45;
            grdLister.Columns["Waiting"].Width = 50;
            grdLister.Columns["ExtraDrop"].Width = 60;
            //grdLister.Columns["MeetAndGreet"].Width = 60;
            //grdLister.Columns["CongtionCharge"].Width = 70;
            grdLister.Columns["Total"].Width = 40;

            grdLister.Columns["PickUpDate"].HeaderText = "Pickup Date-Time";
            grdLister.Columns["RefNumber"].HeaderText = "Ref #";
            grdLister.Columns["PickUpPoint"].HeaderText = "Pickup Point";
            grdLister.Columns["ExtraDrop"].HeaderText = "Extra Drop";
       

        }



        private void ViewReport()
        {
            try
            {

                DateTime? fromDate = (dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
                DateTime? tillDate = (dtpTillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

                string error = string.Empty;


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


                int companyId = ddlCompany.SelectedValue.ToInt();
               // int driverId = ddlDriver.SelectedValue.ToInt();

                int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();



                lblCriteria.Text = "Date Range : " + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", tillDate);




                var query = General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED
                               // && (c.SubcompanyId==AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId==0)
                                
                                && ( (optAccount.ToggleState== Telerik.WinControls.Enumerations.ToggleState.On && c.CompanyId!=null)
                                //||  (optCash.ToggleState== Telerik.WinControls.Enumerations.ToggleState.On && c.CompanyId==null)
                                ||  (optBoth.ToggleState== Telerik.WinControls.Enumerations.ToggleState.On))

                                &&(c.CompanyId==companyId || companyId==0)
                                &&(c.SubcompanyId==SubCompanyId|| SubCompanyId==0)
                                    );

                var list = (from a in query
                            where a.PickupDateTime.Value >= fromDate && a.PickupDateTime.Value <= tillDate
                           
                            select new
                            {
                                Id = a.Id,
                                PickUpDate = a.PickupDateTime,
                                RefNumber = a.BookingNo,
                                Vehicle = a.VehicleTypeId != null ? a.Fleet_VehicleType.VehicleType : "",
                                Account = a.CompanyId != null ? a.Gen_Company.CompanyName : "",
                            //    PickupPoint = !string.IsNullOrEmpty(a.FromDoorNo) ? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                            //    Destination = !string.IsNullOrEmpty(a.ToDoorNo) ? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,
                                  PickupPoint = a.FromDoorNo!=null && a.FromDoorNo!=string.Empty? a.FromDoorNo + " - " + a.FromAddress : a.FromAddress,
                                     Destination =  a.ToDoorNo!=null && a.ToDoorNo!=string.Empty? a.ToDoorNo + " - " + a.ToAddress : a.ToAddress,

                                Driver = a.DriverId != null ? a.Fleet_Driver.DriverNo : "",
                                Charge = a.CompanyId != null ? a.CompanyPrice : a.FareRate ,
                                Parking = a.ParkingCharges,
                                Waiting = a.WaitingCharges,
                                ExtraDrop = a.ExtraDropCharges,
                                //MeetAndGreet = a.MeetAndGreetCharges,
                                //CongtionCharge = a.CongtionCharges,
                               // Total = a.TotalCharges

                            }).OrderByDescending(c=>c.PickUpDate).ToList();


                int cnt = list.Count;

      
              grdLister.RowCount = cnt;


              grdLister.MasterTemplate.BeginUpdate();

                 GridViewRowInfo row = null;
                for (int i = 0; i < cnt; i++)
                {
                    row = grdLister.Rows[i];

                    row.Cells[COLS.ID].Value = list[i].Id;
                    row.Cells[COLS.PickupDate].Value = list[i].PickUpDate;
                    row.Cells[COLS.RefNumber].Value = list[i].RefNumber.ToStr();
                    row.Cells[COLS.Vehicle].Value = list[i].Vehicle.ToStr();
                    //row.Cells[COLS.Account].Value = list[i].Account.ToStr();
                    row.Cells[COLS.Driver].Value = list[i].Driver.ToStr();
                    row.Cells[COLS.PickupPoint].Value = list[i].PickupPoint;
                    row.Cells[COLS.Destination].Value = list[i].Destination;
                    //row.Cells[COLS.Driver].Value = list[i].Driver.ToStr();
                    row.Cells[COLS.Account].Value = list[i].Account.ToStr();


                    row.Cells[COLS.Charges].Value = list[i].Charge.ToDecimal();
                    row.Cells[COLS.Parking].Value = list[i].Parking;
                    row.Cells[COLS.Waiting].Value = list[i].Waiting;
                    row.Cells[COLS.ExtraDrop].Value = list[i].ExtraDrop;
                    //row.Cells[COLS.MeetAndGreet].Value = list[i].MeetAndGreet;
                    //row.Cells[COLS.CongtionCharge].Value = list[i].CongtionCharge;
                    //row.Cells[COLS.Total].Value = list[i].Total.ToDecimal();


                }
                grdLister.MasterTemplate.EndUpdate();



                decimal totalEarning = grdLister.Rows.Sum(c => c.Cells[COLS.Total].Value.ToDecimal());
                string total = totalEarning.ToStr();
                lblTotalEarning.Text = "Total Earning £ " + total;

                lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

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
            DateTime? fromDate = (dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
            DateTime? tillDate = (dtpTillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

            string error = string.Empty;
         

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


            int companyId = ddlCompany.SelectedValue.ToInt();
           // int driverId = ddlDriver.SelectedValue.ToInt();
            int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();


            rptfrmCompanyIncome frm = new rptfrmCompanyIncome();

           
         
            frm.ReportHeading = "Company Income Report for Date Range :" + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", tillDate);
            frm.DataSource = GetDataSource(fromDate,tillDate,companyId,SubCompanyId);

            frm.GenerateReport();

             DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmCompanyIncome1");

             if (doc != null)
             {
                 doc.Close();
             }
            UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
       
        }


        private List<Vu_BookingBase> GetDataSource(DateTime? fromDate,DateTime? tillDate,int companyId,int SubCompanyId)
        {
            return  General.GetQueryable<Vu_BookingBase>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED 
                //&& (c.SubCompanyId==AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId==0)
                && ( (optAccount.ToggleState== Telerik.WinControls.Enumerations.ToggleState.On && c.CompanyId!=null)
                                //||  (optCash.ToggleState== Telerik.WinControls.Enumerations.ToggleState.On && c.CompanyId==null)
                                ||  (optBoth.ToggleState== Telerik.WinControls.Enumerations.ToggleState.On))


                                  && (c.CompanyId == companyId || companyId == 0)
                                    && (c.SubCompanyId == SubCompanyId || SubCompanyId==0)
                )
                            .Where(b => (b.PickupDateTime.Value >= fromDate && b.PickupDateTime.Value <= tillDate))
                                .OrderByDescending(c => c.PickupDateTime).ToList();


        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            DateTime? fromDate = (dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
            DateTime? tillDate = (dtpTillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

            string error = string.Empty;


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


            int companyId = ddlCompany.SelectedValue.ToInt();
           // int driverId = ddlDriver.SelectedValue.ToInt();
            int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();

            rptfrmCompanyIncome frm = new rptfrmCompanyIncome();

       
            frm.ReportHeading = "Company Income Report for Date Range :" + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", tillDate);
            frm.DataSource = GetDataSource(fromDate, tillDate,companyId,SubCompanyId);
            

            frm.GenerateReport();

            frm.ExportReport();
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            DateTime? fromDate = (dtpFromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();
            DateTime? tillDate = (dtpTillDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

            string error = string.Empty;


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


            int companyId = ddlCompany.SelectedValue.ToInt();
          //  int driverId = ddlDriver.SelectedValue.ToInt();
            int SubCompanyId = ddlSubCompany.SelectedValue.ToInt();

            rptfrmCompanyIncome frm = new rptfrmCompanyIncome();


            frm.ReportHeading = "Company Income Report for Date Range :" + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", tillDate);
            frm.DataSource = GetDataSource(fromDate, tillDate,companyId,SubCompanyId);
            

            frm.GenerateReport();

            frm.SendEmail();

        }

        private void optAccount_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            //if (args.ToggleState == ToggleState.On)
            //{
            //    if (ddlCompany.DataSource == null)
            //    {
            //        ComboFunctions.FillCompanyCombo(ddlCompany);
            //    }

            //    ddlCompany.Enabled = true;

            //}
            //else
            //{
            //    ddlCompany.SelectedValue = null;
            //    ddlCompany.Enabled = false;

            //}
        }


    }
}
