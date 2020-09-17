using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Taxi_BLL;
using Utils;
using System.IO;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls;
using Telerik.WinControls.Enumerations;
using Newtonsoft.Json;

namespace Taxi_AppMain
{
    public partial class rptfrmJobsListReceipts : UI.SetupBase
    {
        public rptfrmJobsListReceipts()
        {
            InitializeComponent();


            grdLister.ViewCellFormatting += new CellFormattingEventHandler(grdLister_ViewCellFormatting);
            grdLister.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
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

        private void rptfrmJobsList_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());



            GridViewTextBoxColumn col = new GridViewTextBoxColumn();
            col.IsVisible = false;
            col.Name = "Id";
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Ref #";
            col.Name = COLS.RefNumber;
            grdLister.Columns.Add(col);

            GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
            colDt.Name = COLS.BookingDate;
            colDt.ReadOnly = true;
            colDt.HeaderText = "Booking Date";
            grdLister.Columns.Add(colDt);

            colDt = new GridViewDateTimeColumn();
            colDt.Name = COLS.PickupDate;
            colDt.ReadOnly = true;
            colDt.HeaderText = "Pickup Date-Time";
            grdLister.Columns.Add(colDt);


            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Passenger";
            col.Name = COLS.Passenger;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Pickup Point";
            col.Name = COLS.PickupPoint;
            grdLister.Columns.Add(col);



            col = new GridViewTextBoxColumn();
            //     col.IsVisible = false;
            col.ReadOnly = true;
            col.HeaderText = "Destination";
            col.Name = COLS.Destination;
            grdLister.Columns.Add(col);





            GridViewDecimalColumn colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = COLS.Fare;
            colD.Name = COLS.Fare;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);






            col = new GridViewTextBoxColumn();
            //  col.IsVisible = false;
            col.ReadOnly = true;
            col.Name = COLS.Account;
            col.HeaderText = COLS.Account;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            //  col.IsVisible = false;
            col.ReadOnly = true;
            col.Name = COLS.Driver;
            col.HeaderText = COLS.Driver;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.HeaderText = COLS.Vehicle;
            col.Name = COLS.Vehicle;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.HeaderText = COLS.Status;
            col.Name = COLS.Status;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);


            col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.HeaderText = COLS.DespatchBy;
            col.Name = COLS.DespatchBy;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);




            grdLister.Columns[COLS.RefNumber].HeaderText = "Ref #";

            grdLister.Columns[COLS.Fare].HeaderText = "Fare £";
            grdLister.Columns[COLS.PickupPoint].HeaderText = "Pickup Point";

            grdLister.Columns[COLS.Destination].HeaderText = "Destination";


            grdLister.Columns[COLS.BookingDate].HeaderText = "Booking Date";

            (grdLister.Columns[COLS.BookingDate] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns[COLS.BookingDate] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            (grdLister.Columns[COLS.PickupDate] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns[COLS.PickupDate] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            grdLister.Columns[COLS.PickupDate].HeaderText = "Pickup Date-Time";
            grdLister.Columns[COLS.Account].HeaderText = "A/C";

            grdLister.Columns[COLS.DespatchBy].HeaderText = "Despatched By";

        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            ViewReport();
        }


        private List<Vu_BookingBase> GetDataSource(int reportType, int companyId, DateTime? fromDate, DateTime? toDate)
        {
            return General.GetQueryable<Vu_BookingBase>(c =>
                                 (



                                    (reportType == eReportType.COMPLETED_JOBS && c.BookingStatusId == Enums.BOOKINGSTATUS.DISPATCHED) ||
                                    (reportType == eReportType.INCOMPLETE_JOBS && (c.BookingStatusId == Enums.BOOKINGSTATUS.WAITING || c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || c.BookingStatusId == Enums.BOOKINGSTATUS.ONHOLD)) ||
                                    (reportType == eReportType.NOTACCEPTED_JOBS && c.BookingStatusId == Enums.BOOKINGSTATUS.NOTACCEPTED) ||
                                    (reportType == eReportType.REJECTED_JOBS && c.BookingStatusId == Enums.BOOKINGSTATUS.REJECTED) ||
                                    (reportType == eReportType.CANCELLED_JOBS && c.BookingStatusId == Enums.BOOKINGSTATUS.CANCELLED)
                                 )

                                 && (companyId == 0 || c.CompanyId == companyId)


                          && ((fromDate == null || c.PickupDateTime.Value.Date >= fromDate) && (toDate == null || c.PickupDateTime.Value.Date <= toDate))
                          && (c.SubCompanyId == AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId == 0))
                                 .OrderByDescending(c => c.PickupDateTime).ToList();

        }


        public override void Print()
        {
            try
            {

                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    long id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                    var list = General.GetQueryable<Vu_BookingDetail>(c => c.Id == id).ToList();
                    send(id);
                  //  rptfrmJobReceiptDetails frm = null;

                  //  frm = new rptfrmJobReceiptDetails();
                  //  frm.DataSource = list;
                  //  frm.GenerateReport();
                  //  frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                  //  frm.Size = new Size(600, 1024);
                  //  frm.ControlBox = true;


                  //  frm.StartPosition = FormStartPosition.CenterScreen;

                  //  frm.MaximizeBox = false;
                  //  frm.MinimizeBox = false;
                  //  frm.ShowDialog();

                  ////  frm.reportViewer1.Dispose();
                  //  frm.Dispose();

                    //DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName(objReport.TemplateValue + "1");

                    //if (doc != null)
                    //{
                    //    doc.Close();
                    //}




                }
            }
            catch (Exception ex)
            {


            }

        }


        private void btnViewReport_Click_1(object sender, EventArgs e)
        {
            Print();

        }


        public struct COLS
        {



            public static string ID = "ID";
            public static string BookingDate = "BookingDate";

            public static string PickupDate = "PickupDate";
            public static string RefNumber = "RefNumber";
            public static string Passenger = "Passenger";

            public static string Account = "Account";

            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";
            public static string Driver = "Driver";
            public static string Vehicle = "Vehicle";
            public static string Fare = "Fare";
            public static string Status = "Status";
            public static string DespatchBy = "DespatchBy";

        }

        public struct eReportType
        {
            public static int COMPLETED_JOBS = 1;
            public static int INCOMPLETE_JOBS = 2;
            public static int NOTACCEPTED_JOBS = 3;
            public static int REJECTED_JOBS = 4;
            public static int CANCELLED_JOBS = 5;



        }


        private int GetReportType()
        {

            int reportType = eReportType.COMPLETED_JOBS;
            if (optInCompleteJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                reportType = eReportType.INCOMPLETE_JOBS;
            }


            //else if (optNotAccepedJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    reportType = eReportType.NOTACCEPTED_JOBS;
            //}
            //else if (optRejectedJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    reportType = eReportType.REJECTED_JOBS;
            //}
            //else if (optCancelledJobs.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            //{
            //    reportType = eReportType.CANCELLED_JOBS;
            //}

            return reportType;

        }


        private void ViewReport()
        {



            var list = GetDataSource(GetReportType(), ddlCompany.SelectedValue.ToInt(), dtpFromDate.Value.ToDateorNull(), dtpToDate.Value.ToDateorNull());



            lblTotalJobs.Text = "Total Job(s) : " + list.Count.ToStr();


            // grdLister.DataSource = list;
            grdLister.RowCount = list.Count;


            GridViewRowInfo row = null;
            for (int i = 0; i < list.Count; i++)
            {

                row = grdLister.Rows[i];

                row.Cells[COLS.ID].Value = list[i].Id;
                row.Cells[COLS.BookingDate].Value = list[i].BookingDate.ToStr();
                row.Cells[COLS.PickupDate].Value = list[i].PickupDateTime;


                row.Cells[COLS.RefNumber].Value = list[i].BookingNo.ToStr();
                row.Cells[COLS.Passenger].Value = list[i].CustomerName.ToStr();

                row.Cells[COLS.Vehicle].Value = list[i].VehicleType.ToStr();
                row.Cells[COLS.Account].Value = list[i].CompanyName.ToStr();
                row.Cells[COLS.PickupPoint].Value = list[i].FromAddress.ToStr();
                row.Cells[COLS.Destination].Value = list[i].ToAddress.ToStr();
                row.Cells[COLS.Fare].Value = list[i].FareRate.ToDecimal();

                row.Cells[COLS.Driver].Value = list[i].DriverNo.ToStr();
                row.Cells[COLS.Status].Value = list[i].StatusName.ToStr();
                row.Cells[COLS.DespatchBy].Value = list[i].Despatchby.ToStr();


            }



            lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();


        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {


            try
            {

                rptfrmJobListViewer frm = new rptfrmJobListViewer();

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? toDate = dtpToDate.Value.ToDateorNull();



                frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy}", toDate);
                frm.DataSource = GetDataSource(GetReportType(), ddlCompany.SelectedValue.ToInt(), fromDate, toDate);

                frm.GenerateReport();

                frm.ExportReport();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }
        }



        void grdLister_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            ViewDetailForm();
        }

        private void ViewDetailForm()
        {

            if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
            {
                ShowBookingForm(grdLister.CurrentRow.Cells["Id"].Value.ToInt());
            }
            else
            {
                ENUtils.ShowMessage("Please select a record");
            }
        }

        private void ShowBookingForm(int id)
        {


            frmBooking frm = new frmBooking();
            frm.OnDisplayRecord(id);

            frm.ControlBox = true;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.MaximizeBox = false;
            frm.ShowDialog();

        }

        private void chkAll_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == ToggleState.On)
            {



                ddlCompany.SelectedValue = null;
                ddlCompany.Enabled = false;


            }
            else
            {
                if (ddlCompany.DataSource == null)
                {
                    ComboFunctions.FillCompanyCombo(ddlCompany);
                }

                ddlCompany.Enabled = true;

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
             PrintDocument();
        }
        public void send(long id)
        {



            string connString = Application.StartupPath + @"\Reports\Report.exe";



            string conn = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr();



            Classes.JArg_Receipts j = new Classes.JArg_Receipts()
            {//Connectionstring =

                ConnectionString = conn,
               id = id,
                ReportName=this.Name

            };

            // Convert BlogSites object to JOSN string format  
            string jsonData = JsonConvert.SerializeObject(j);
            jsonData = Cryptography.Encrypt(jsonData, "report", true);

            TaxiDataContext db = new TaxiDataContext();
          
         // db.Sp_IsActiveReport("Reciept", true);
            System.Diagnostics.Process.Start(connString, jsonData);
        }
        private void PrintDocument()
        {

            try
            {

                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {
                    long id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();
                    var list = General.GetQueryable<Vu_BookingDetail>(c => c.Id == id).ToList();

                    send(id);
                    //rptfrmJobReceiptDetails frm = null;

                    //frm = new rptfrmJobReceiptDetails();
                    //frm.DataSource = list;
                    //frm.GenerateReport();

                    //ReportPrintDocument rpt = new ReportPrintDocument(frm.reportViewer1.LocalReport);
                    //rpt.Print();
                    //rpt.Dispose();

                    //frm.reportViewer1.Dispose();
                    //frm.Dispose();


                }
            }
            catch (Exception ex)
            {


            }


        }

        private void btnEmailReceipt_Click(object sender, EventArgs e)
        {
            SendEmail();
        }


        private void SendEmail()
        {

            try
            {

                if (grdLister.CurrentRow != null && grdLister.CurrentRow is GridViewDataRowInfo)
                {

                    long id = grdLister.CurrentRow.Cells["Id"].Value.ToLong();

                    send(id);

                   // var list = General.GetQueryable<Vu_BookingDetail>(c => c.Id == id).ToList();

                   // rptfrmJobReceiptDetails frm = null;

                   // frm = new rptfrmJobReceiptDetails();
                   // frm.DataSource = list;
                   // frm.GenerateReport();

                   // frm.SendEmail(grdLister.CurrentRow.Cells[COLS.RefNumber].Value.ToStr(), "");

                   //// frm.reportViewer1.Dispose();
                   // frm.Dispose();
                }
            }
            catch (Exception ex)
            {


            }
        }






        private void rptfrmJobsListReceipts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                PrintDocument();
            else if (e.KeyCode == Keys.F2)
                  Print();
              
            else if (e.KeyCode == Keys.F3)
                SendEmail();

        }

    }
}
