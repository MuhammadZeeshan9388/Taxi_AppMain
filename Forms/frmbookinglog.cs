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
using DAL;
using Utils;
using Telerik.WinControls.UI;
using Telerik.WinControls;


namespace Taxi_AppMain
{
    public partial class frmbookinglog : UI.SetupBase
    {
        public frmbookinglog()
        {
            InitializeComponent();
            InitializeConstructor();
        }
        private void InitializeConstructor()
        {
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
            

        }

        private void frmbookinglog_Load(object sender, EventArgs e)
        {
            ddlColumns.Items.Add("booking no");
            ddlColumns.Items.Add("Passenger");
            ddlColumns.Items.Add("Phone");



            ddlColumns.SelectedIndex = 0;
            PopulateData1();
        }

        private void PopulateData1()
        {
            string searchTxt = txtSearch.Text.ToLower().Trim();
            string col = ddlColumns.Text.Trim().ToLower();

            if (searchTxt.Length < 3)
                searchTxt = string.Empty;


            //DateTime? fromDate = dtpFromDate.Value.ToDateTimeorNull();
            //DateTime? toDate = dtpToDate.Value.ToDateTimeorNull();
            DateTime? fromDate = dtpFromDate.Value.ToDate();
            DateTime? toDate = dtpToDate.Value.ToDate();

            bool col_name = false;
            bool col_refNo = false;
            
            bool col_mobileno = false;

            if (col == "passenger")
            {
                col_name = true;
            }
            else if (col == "booking no")
            {
                col_refNo = true;
            }
            else if (col == "phone")
            {
                col_mobileno = true;
            }


         
            var data1 = General.GetQueryable<Vu_BookingLog>(c => (c.PickupDateTime.Value >= fromDate && c.PickupDateTime.Value <= toDate) && (c.SubCompanyId==AppVars.DefaultBookingSubCompanyId || AppVars.DefaultBookingSubCompanyId==0))
                            .OrderBy(c => c.BookingNo).AsEnumerable();


            var query = from a in data1
                        where

                       (col_refNo && (a.BookingNo.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                       || (col_name && (a.CustomerName.ToLower().Contains(searchTxt) || searchTxt == string.Empty))
                       || (col_mobileno && (a.CustomerPhoneNo.Contains(searchTxt) || searchTxt == string.Empty))
                            

                        select new
                        {
                            ID = a.Id,
                            BookingNo = a.BookingNo,
                            DateTime = a.PickupDateTime,
                            Customer= a.CustomerName,
                            PickUpPoint = a.FromAddress,
                            Destination = a.ToAddress,
                            jobTime = string.Format("{0: hh:mm:tt}", a.PickupDateTime),
                            DriverNo = a.DriverNo,
                            jobAccept = string.Format("{0: hh:mm:tt}", a.AcceptedDateTime),
                            Arrived = string.Format("{0: hh:mm:tt}", a.AcceptedDateTime),
                            POB = string.Format("{0: hh:mm:tt}", a.POBDateTime),
                            Cleared = string.Format("{0: hh:mm:tt}", a.ClearedDateTime),
                            FareRate = a.FareRate,
                            PaymentType = a.PaymentType,
                            Despatch = string.Format("{0: hh:mm:tt}", a.DespatchDateTime),
                            Status = a.StatusName,
    //JobLate = string.Format("{0: hh:mm:tt}", a.DespatchDateTime) > string.Format("{0: hh:mm:tt}", a.PickupDateTime) ? "" : "",
    
                            JobCreate = a.BookingCreateDate,
                            JobUpdate = a.BookingUpdateDate,
                        };

            grdLister.DataSource = query.ToList();
            grdLister.CurrentRow = null;
            grdLister.Columns["ID"].IsVisible = false;
            grdLister.Columns["PickUpPoint"].IsVisible = false;
            grdLister.Columns["Destination"].IsVisible = false;


            grdLister.Columns["BookingNo"].Width = 90;
            grdLister.Columns["DateTime"].Width = 150;
            grdLister.Columns["Customer"].Width = 90;
            grdLister.Columns["PickUpPoint"].Width = 220;
            grdLister.Columns["Destination"].Width = 220;
            grdLister.Columns["jobTime"].Width = 90;
            grdLister.Columns["DriverNo"].Width = 80;
            grdLister.Columns["jobAccept"].Width = 90;
            grdLister.Columns["Arrived"].Width = 90;
            grdLister.Columns["POB"].Width = 90;
            grdLister.Columns["Cleared"].Width = 90;
            grdLister.Columns["FareRate"].Width = 90;
            grdLister.Columns["PaymentType"].Width = 110;
            grdLister.Columns["Despatch"].Width = 90;
            grdLister.Columns["Status"].Width = 90;
            grdLister.Columns["JobCreate"].Width = 150;
            grdLister.Columns["JobUpdate"].Width = 150;

            


        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            PopulateData1();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.LastDayOfMonthValue());
            PopulateData1();
        }
    }
}
