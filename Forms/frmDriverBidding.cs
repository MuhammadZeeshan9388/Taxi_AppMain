using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Utils;
using Taxi_Model;
using Taxi_BLL;

namespace Taxi_AppMain
{
    public partial class frmDriverBidding : Form
    {

        private Booking _objBooking = null;


        //  DriverBO objMaster;
        BookingBO objBooking;
        bool IsAllDriverSelected = false;
        public frmDriverBidding(Booking obj)
        {
            InitializeComponent();
            this._objBooking = obj;

            this.KeyPreview = true;
            this.Shown += new EventHandler(frmDriverLogin_Shown);
            this.KeyDown += new KeyEventHandler(frmBiddingNotifications_KeyDown);

            grdDrivers.CommandCellClick += new CommandCellClickEventHandler(grdDrivers_CommandCellClick);
        }


      

        void grdDrivers_CommandCellClick(object sender, EventArgs e)
        {
            try
            {
                if (grdDrivers.CurrentRow != null && grdDrivers.CurrentRow is GridViewDataRowInfo)
                {

                    decimal drvfares = grdDrivers.CurrentRow.Cells["Bid"].Value.ToDecimal();
                    int? driverId = grdDrivers.CurrentRow.Cells["DriverId"].Value.ToIntorNull();

                    bool IsDespatched = false;
                    long jobId = _objBooking.Id;


                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        Booking obj = db.Bookings.FirstOrDefault(c => c.Id == jobId);

                        if (obj != null)
                        {


                            obj.Booking_Logs.Add(new Booking_Log
                            {
                                BookingId = obj.Id,
                                UpdateDate = DateTime.Now,
                                BeforeUpdate = "Old Fares : £" + Math.Round(obj.FareRate.ToDecimal(), 2),
                                AfterUpdate = "Fares changed to Lowest Bid Driver Rate £" + drvfares
                            });

                            obj.FareRate = drvfares;
                            db.SubmitChanges();
                        }
                    }




                    if (_objBooking.PickupDateTime.Value.Date <= DateTime.Now.Date && General.GetQueryable<Fleet_DriverQueueList>(c=>c.DriverId==driverId && c.Status==true).Count() > 0)
                    {
                        frmDespatchJob frm = new frmDespatchJob((General.GetObject<Booking>(c => c.Id == _objBooking.Id)), driverId, true);
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        IsDespatched = frm.IsDespatched;

                        frm.Dispose();
                    }
                    else
                    {
                        frmDespatchPreBooking frmPreDespatch = new frmDespatchPreBooking(General.GetObject<Booking>(c => c.Id == _objBooking.Id), driverId, true);
                        frmPreDespatch.StartPosition = FormStartPosition.CenterScreen;
                        frmPreDespatch.ShowDialog();
                        IsDespatched = frmPreDespatch.SuccessDespatched;

                        frmPreDespatch.Dispose();

                    }


                  

                }
            }
            catch (Exception ex)
            {


            }
           


        }

        void frmBiddingNotifications_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();

            }
        }



        void frmDriverLogin_Shown(object sender, EventArgs e)
        {
            try
            {

              
                objBooking = new BookingBO();

                objBooking.GetByPrimaryKey(_objBooking.Id);                        

                if (objBooking.Current != null)
                {

                    GridViewRowInfo row = null;
                    foreach (var item in objBooking.Current.Booking_Biddings.Where(c=>c.BidRate!=null).OrderBy(c=>c.BidRate))
	                {

                        row = grdDrivers.Rows.AddNew();

                            
                        row.Cells["Id"].Value = item.JobId;
                        row.Cells["DriverNo"].Value = item.Fleet_Driver.DefaultIfEmpty().DriverNo + " - " +item.Fleet_Driver.DefaultIfEmpty().DriverName.ToStr();
                        row.Cells["Bid"].Value = item.BidRate.ToDecimal();
                        row.Cells["DriverId"].Value =item.DriverId.ToIntorNull();
                             
	                }


                    lblTotal.Text = "Total Bids : " + grdDrivers.Rows.Count;
                }


                if (objBooking.Current.DriverId != null &&
                    (objBooking.Current.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || objBooking.Current.BookingStatusId == Enums.BOOKINGSTATUS.PENDING_START))
                {

                    grdDrivers.Enabled = false;

                    txtJobOfferedTo.Text = "Job Offered to Driver (" + objBooking.Current.Fleet_Driver.DefaultIfEmpty().DriverNo + ")";

                }

            }
            catch (Exception ex)
            {

                ENUtils.ShowMessage(ex.Message);
            }

        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void btnLogin_Click(object sender, EventArgs e)
        //{
        //     LoginDriver();
        //}


    
        private void grdDrivers_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
        {
            try
            {
                if (e.Column != null && e.Column.Name == "colDrv")
                    e.Cancel = true;


                if (e.Column != null && e.Column.Name == "colChk" && e.Row is GridViewDataRowInfo && e.Row.Tag.ToStr()=="lock")
                {
                    e.Cancel = true;

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void chkAllDrivers_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            try
            {
                if (IsAllDriverSelected == false)
                {
                    IsAllDriverSelected = true;
                    for (int i = 0; i < grdDrivers.RowCount; i++)
                    {
                        grdDrivers.Rows[i].Cells["colChk"].Value = true;
                    }
                }
                else
                {
                    IsAllDriverSelected = false;
                    for (int i = 0; i < grdDrivers.RowCount; i++)
                    {
                        if (grdDrivers.Rows[i].Cells["colChk"].Tag == null)
                        {

                            grdDrivers.Rows[i].Cells["colChk"].Value = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }



    

       


    }
}
