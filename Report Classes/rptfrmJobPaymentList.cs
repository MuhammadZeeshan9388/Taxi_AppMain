using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI.Docking;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Utils;
using Taxi_Model;
using Taxi_BLL;

namespace Taxi_AppMain
{
    public partial class rptfrmJobPaymentList : UI.SetupBase
    {
        public rptfrmJobPaymentList()
        {
            InitializeComponent();

          //  grdLister.CellDoubleClick+=new GridViewCellEventHandler(grdLister_CellDoubleClick);

            grdLister.ViewCellFormatting+=new CellFormattingEventHandler(grdLister_ViewCellFormatting);

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
        public void FillCombo()
        {
            ComboFunctions.FillDriverNoCombo(ddlDrivers);
        }
        private void rptfrmJobsPaymentList_Load(object sender, EventArgs e)
        {
            FillCombo();

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
            col.Name =COLS.RefNumber;
            grdLister.Columns.Add(col);

             GridViewDateTimeColumn colDt = new GridViewDateTimeColumn();
            colDt.Name = COLS.BookingDate;
            colDt.ReadOnly = true;
            colDt.HeaderText = "Booking Date";
            colDt.IsVisible = false;
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
            col.HeaderText = "Vehicle";
            col.Width = 80;
            col.Name = COLS.VehicleType;
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
            col.Name =COLS.Destination;
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

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "A/C Price";
            colD.Name = COLS.AccFare;
            //   colD.Width = 70;
            colD.Maximum = 9999999;
            colD.FormatString = "{0:#,###0.00}";
            grdLister.Columns.Add(colD);

            colD = new GridViewDecimalColumn();
            colD.DecimalPlaces = 2;
            colD.Minimum = 0;
            colD.ReadOnly = true;
            colD.HeaderText = "Cust. Price";
            colD.Name = COLS.CustFare;
            // colD.Width = 70;
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
            col.HeaderText = COLS.AuthCode;
            col.Name = COLS.AuthCode;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

               col = new GridViewTextBoxColumn();
            //  col.IsVisible = false;
            col.ReadOnly = true;
            col.Name = COLS.Driver;
            col.HeaderText = COLS.Driver;
            grdLister.Columns.Add(col);

             

                col = new GridViewTextBoxColumn();
            // col.IsVisible = false;
            col.HeaderText = COLS.Status;
            col.Name = COLS.Status;
            col.ReadOnly = true;
            grdLister.Columns.Add(col);

  


            grdLister.Columns[COLS.RefNumber].HeaderText = "Ref #";

            grdLister.Columns[COLS.Fare].HeaderText = "Fare £";
            grdLister.Columns[COLS.AuthCode].HeaderText = "Auth Code";

            grdLister.Columns[COLS.PickupPoint].HeaderText = "Pickup Point";

            grdLister.Columns[COLS.Destination].HeaderText = "Destination";


            grdLister.Columns[COLS.BookingDate].HeaderText = "Booking Date";

            (grdLister.Columns[COLS.BookingDate] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns[COLS.BookingDate] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";


            (grdLister.Columns[COLS.PickupDate] as GridViewDateTimeColumn).CustomFormat = "dd/MM/yyyy HH:mm";
            (grdLister.Columns[COLS.PickupDate] as GridViewDateTimeColumn).FormatString = "{0:dd/MM/yyyy HH:mm}";

            grdLister.Columns[COLS.PickupDate].HeaderText = "Pickup Date-Time";
            grdLister.Columns[COLS.PickupDate].Width = 80;

          //  grdLister.Columns[COLS.Account].Width = 80;
            grdLister.Columns[COLS.Account].HeaderText = "A/C";
            grdLister.Columns[COLS.Driver].Width = 40;
            grdLister.Columns[COLS.Driver].HeaderText = "Drv";
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            Print();
        }


        public override void Print()
        {
            try
            {

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? toDate = dtpToDate.Value.ToDateorNull();

                if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



                if (toDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    toDate = (toDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

                //var data1 = General.GetQueryable<Vu_BookingBase>(c => c.PaymentTypeId == Enums.PAYMENT_TYPES.CREDIT_CARD)
                //               .OrderByDescending(c => c.PickupDateTime );

                int DriverId = ddlDrivers.SelectedValue.ToInt();
                bool AuthCode = chkAuthCode.Checked;
                string Auth = "";

                if (AuthCode)
                {
                    Auth = "auth";
                }
                //var data1 = General.GetQueryable<Vu_BookingBase>(c => c.PaymentTypeId == Enums.PAYMENT_TYPES.CREDIT_CARD )
                //                 .OrderByDescending(c => c.PickupDateTime);

                var data1 = General.GetQueryable<Vu_BookingBase>(c => (c.PaymentTypeId == Enums.PAYMENT_TYPES.CREDIT_CARD)
                    && (DriverId == 0 || c.DriverId == DriverId) && (Auth != "" ? (c.AuthCode != null && c.AuthCode != "") : (c.AuthCode == "" || c.AuthCode == null || c.AuthCode != "")))
                     .OrderByDescending(c => c.PickupDateTime);



                var list = (from a in data1

                            where

                                   
                                 ((fromDate == null || a.PickupDateTime.Value.Date >= fromDate) && (toDate == null || a.PickupDateTime.Value.Date <= toDate))
                            select a).ToList();



                rptfrmJobPaymentViewer frm = new rptfrmJobPaymentViewer();


                frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", toDate);
                frm.DataSource = list;

                frm.GenerateReport();

                DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("rptfrmJobPaymentViewer1");

                if (doc != null)
                {
                    doc.Close();
                }
                UI.MainMenuForm.MainMenuFrm.ShowForm(frm);
            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);

            }

        }

   
        private void btnViewReport_Click_1(object sender, EventArgs e)
        {
            ViewReport();
        }


        public struct COLS
        {
     


            public static string ID = "ID";
            public static string BookingDate = "BookingDate";

            public static string PickupDate = "PickupDate";
            public static string RefNumber = "RefNumber";
            public static string Passenger = "Passenger";

            public static string Account = "Account";
            public static string VehicleType = "VehicleType";
            public static string PickupPoint = "PickupPoint";
            public static string Destination = "Destination";
            public static string Driver = "Driver";
            public static string AuthCode = "AuthCode";
            public static string Fare = "Fare";
            public static string AccFare = "AccFare";
            public static string CustFare = "CustFare";

           public static string Status = "Status";

        }


        private void ViewReport()
        {

            DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
            DateTime? toDate = dtpToDate.Value.ToDateorNull();


            if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



            if (toDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                toDate = (toDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();


            int DriverId = ddlDrivers.SelectedValue.ToInt();
            bool AuthCode=chkAuthCode.Checked;
            string Auth = "";

            if (AuthCode)
            {
                Auth = "auth";
            }
            //var data1 = General.GetQueryable<Vu_BookingBase>(c => c.PaymentTypeId == Enums.PAYMENT_TYPES.CREDIT_CARD )
            //                 .OrderByDescending(c => c.PickupDateTime);

            var data1 = General.GetQueryable<Vu_BookingBase>(c => (c.PaymentTypeId == Enums.PAYMENT_TYPES.CREDIT_CARD)
                && (DriverId == 0 || c.DriverId == DriverId) && (Auth !="" ? (c.AuthCode!=null && c.AuthCode!="") : (c.AuthCode=="" || c.AuthCode==null || c.AuthCode!="")))
                 .OrderByDescending(c => c.PickupDateTime);



            var list = (from a in data1

                        where


                             ((fromDate == null || a.PickupDateTime.Value.Date >= fromDate) && (toDate == null || a.PickupDateTime.Value.Date <= toDate))
                        select a).ToList();



            lblTotalJobs.Text = "Total Job(s) : "+list.Count.ToStr();




            // grdLister.DataSource = list;
            grdLister.RowCount = list.Count;

            for (int i = 0; i < list.Count; i++)
            {
                grdLister.Rows[i].Cells[COLS.ID].Value = list[i].Id;
                grdLister.Rows[i].Cells[COLS.BookingDate].Value = list[i].BookingDate.ToStr();
                grdLister.Rows[i].Cells[COLS.PickupDate].Value = list[i].PickupDateTime;


                grdLister.Rows[i].Cells[COLS.RefNumber].Value = list[i].BookingNo.ToStr();
                grdLister.Rows[i].Cells[COLS.Passenger].Value = list[i].CustomerName.ToStr();

                grdLister.Rows[i].Cells[COLS.AuthCode].Value = list[i].AuthCode.ToStr();
                grdLister.Rows[i].Cells[COLS.Account].Value = list[i].CompanyName.ToStr();
                grdLister.Rows[i].Cells[COLS.VehicleType].Value = list[i].VehicleType.ToStr();
                grdLister.Rows[i].Cells[COLS.PickupPoint].Value = list[i].FromAddress.ToStr();
                grdLister.Rows[i].Cells[COLS.Destination].Value = list[i].ToAddress.ToStr();
                grdLister.Rows[i].Cells[COLS.Fare].Value = list[i].FareRate.ToDecimal();
                grdLister.Rows[i].Cells[COLS.AccFare].Value = list[i].CompanyPrice.ToDecimal();
                grdLister.Rows[i].Cells[COLS.CustFare].Value = list[i].CustomerPrice.ToDecimal();

                grdLister.Rows[i].Cells[COLS.Driver].Value = list[i].DriverNo.ToStr();
                grdLister.Rows[i].Cells[COLS.Status].Value = list[i].StatusName.ToStr();
             

            }

          

            lblTotalJobs.Text = "Total Jobs : " + grdLister.Rows.Count.ToStr();


        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {

                rptfrmJobPaymentViewer frm = new rptfrmJobPaymentViewer();

                DateTime? fromDate = dtpFromDate.Value.ToDateorNull();
                DateTime? toDate = dtpToDate.Value.ToDateorNull();

                if (fromDate != null && dtpFromTime.Value != null && dtpFromTime.Value.Value != null)
                    fromDate = (fromDate.Value.ToDate() + dtpFromTime.Value.Value.TimeOfDay).ToDateTime();



                if (toDate != null && dtptilltime.Value != null && dtptilltime.Value.Value != null)
                    toDate = (toDate.Value.ToDate() + dtptilltime.Value.Value.TimeOfDay).ToDateTime();

                //var data1 = General.GetQueryable<Vu_BookingBase>(c => c.PaymentTypeId == Enums.PAYMENT_TYPES.CREDIT_CARD)
                //                 .OrderByDescending(c => c.PickupDateTime);

                int DriverId = ddlDrivers.SelectedValue.ToInt();
                bool AuthCode = chkAuthCode.Checked;
                string Auth = "";

                if (AuthCode)
                {
                    Auth = "auth";
                }
                //var data1 = General.GetQueryable<Vu_BookingBase>(c => c.PaymentTypeId == Enums.PAYMENT_TYPES.CREDIT_CARD )
                //                 .OrderByDescending(c => c.PickupDateTime);

                var data1 = General.GetQueryable<Vu_BookingBase>(c => (c.PaymentTypeId == Enums.PAYMENT_TYPES.CREDIT_CARD)
                    && (DriverId == 0 || c.DriverId == DriverId) && (Auth != "" ? (c.AuthCode != null && c.AuthCode != "") : (c.AuthCode == "" || c.AuthCode == null || c.AuthCode != "")))
                     .OrderByDescending(c => c.PickupDateTime);



                var list = (from a in data1

                            where


                                 ((fromDate == null || a.PickupDateTime.Value.Date >= fromDate) && (toDate == null || a.PickupDateTime.Value.Date <= toDate))
                            select a).ToList();

                frm.ReportHeading = "Date Range : " + string.Format("{0:dd/MM/yyyy HH:mm}", fromDate) + " to " + string.Format("{0:dd/MM/yyyy HH:mm}", toDate);
                frm.DataSource = list;

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




    }
}
