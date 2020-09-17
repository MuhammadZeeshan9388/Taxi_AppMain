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
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmBiddingNotifications : Form
    {

        private Booking _objBooking = null;


        //  DriverBO objMaster;
        BookingBO objBooking;
        bool IsAllDriverSelected = false;
        public frmBiddingNotifications(Booking obj)
        {
            InitializeComponent();
            this._objBooking = obj;

            this.KeyPreview = true;
            this.Shown += new EventHandler(frmDriverLogin_Shown);
            this.KeyDown += new KeyEventHandler(frmBiddingNotifications_KeyDown);
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

                

                var list = (from a in General.GetQueryable<Fleet_Driver>(c => c.IsActive == true && c.HasPDA==true)
                            orderby a.DriverNo
                            select new
                            {
                                Id = a.Id,
                                DriverName = a.DriverNo + " - " + a.DriverName

                            }).ToList();  // General.GetQueryable<Fleet_Driver>(null)

                grdDrivers.RowCount = list.Count;

                for (int i = 0; i < list.Count; i++)
                {
                    grdDrivers.Rows[i].Cells["colId"].Value = list[i].Id;
                    grdDrivers.Rows[i].Cells["colDrv"].Value = list[i].DriverName;
                }

               

                lblPrice.Text = "£" + _objBooking.FareRate.ToStr();
                objBooking = new BookingBO();

                objBooking.GetByPrimaryKey(_objBooking.Id);

                if (objBooking.Current != null)
                {

                    GridViewRowInfo row = null;
                    foreach (var item in objBooking.Current.Booking_Biddings)
	                {
                    		 row = grdDrivers.Rows.FirstOrDefault(c => c.Cells["colId"].Value.ToIntorNull() ==item.DriverId);

                             if (row != null)
                             {
                                 row.Cells["Id"].Value = item.Id;
                                 row.Cells["colChk"].Value = true;
                                 row.Tag = "lock";
                             }
	                }   

                }


                DateTime? biddingExpiry = objBooking.Current.PriceBiddingExpiryDate;

                if (biddingExpiry == null)
                {
                    if (objBooking.Current.PickupDateTime.Value.Date > DateTime.Now.Date)
                    {
                        biddingExpiry = objBooking.Current.PickupDateTime.Value.AddHours(-2);

                    }
                    else
                    {
                        biddingExpiry = objBooking.Current.PickupDateTime.Value.AddMinutes(-15);

                    }

                }

                txtBiddingExpiryDate.Text = string.Format("{0:dd/MM/yyyy HH:mm}",biddingExpiry);

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
                        if (grdDrivers.Rows[i].Tag == null)
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



        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {

                int?[] drvIds = grdDrivers.Rows.Where(c => c.Cells["colChk"].Value.ToBool()).Select(a => a.Cells["colId"].Value.ToIntorNull()).ToArray<int?>();


                if (drvIds.Count() == 0)
                {
                    ENUtils.ShowMessage("Please select atleast one driver");
                    return;
                }


                string msg=string.Empty;
                string driverIds = string.Empty;
              
               // lblPrice.Text = objBooking.Current.FareRate.ToStr();
                long JobId = _objBooking.Id.ToLong();


                DateTime currentDateTime=DateTime.Now;

                DateTime? currentJobPickupDateTime=_objBooking.PickupDateTime;



                var existingJobs = General.GetQueryable<Booking>(c => c.Id != JobId && c.DriverId != null &&
                   (c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING_START)
                   && c.PickupDateTime > currentDateTime
                   ).AsEnumerable().Where(c=> drvIds.Count(a => a == c.DriverId) > 0).ToList();


                string errorMsg = string.Empty;

                if (existingJobs.Count() > 0)
               {
                 

                   foreach (var job in existingJobs)
                   {

                       if (job.PickupDateTime < currentJobPickupDateTime && currentJobPickupDateTime.Value.Subtract(job.PickupDateTime.ToDateTime()).TotalHours<1 )
                       {
                           errorMsg += job.Fleet_Driver.DefaultIfEmpty().DriverNo + " already have a job  at " + string.Format("{0:dd/MM - HH:mm}", job.PickupDateTime) + Environment.NewLine;
                       }
                       else if (job.PickupDateTime > currentJobPickupDateTime && job.PickupDateTime.ToDateTime().Subtract(currentJobPickupDateTime.ToDateTime()).TotalHours < 1)
                       {
                           errorMsg += job.Fleet_Driver.DefaultIfEmpty().DriverNo + " already have a job  at " + string.Format("{0:dd/MM - HH:mm}", job.PickupDateTime) + Environment.NewLine;
                       }                                        
                       
                   }

               }


                if (!string.IsNullOrEmpty(errorMsg))
                {
                    ENUtils.ShowMessage(errorMsg);
                    return;
                }


                objBooking.Edit();

                string[] skipProperties = new string[] {"Booking", "Fleet_Driver", "BiddingStatus" };
                if (grdDrivers.Columns.Count > 0)
                {

                    msg = "Bid Price Alert>>Price Bidding Alert " + lblPrice.Text + "=13";
                    //  List<obj.Current.Booking_Biddings> listofAirportPickup = (from a in grdDrivers.Rows

                    List<Booking_Bidding> listofBidding = (from a in grdDrivers.Rows.Where(c=>c.Cells["colChk"].Value.ToBool())
                                                           select new Booking_Bidding

                                                                 {
                                                                     Id = a.Cells["Id"].Value.ToInt(),
                                                                     JobId = JobId,
                                                                     DriverId = a.Cells["colId"].Value.ToIntorNull(),
                                                                   
                                                                   
                                                                  
                                                                 }).ToList();
                    IList<Booking_Bidding> savedList = objBooking.Current.Booking_Biddings;
                    Utils.General.SyncChildCollection(ref savedList, ref listofBidding, "Id", skipProperties);



                  //   int?[] loginDrvs=     General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true).Select(args=>args.DriverId).ToArray<int?>();

                     int[] loginDrvs = General.GetQueryable<Fleet_Driver_Location>(c => c.UpdateDate > (currentDateTime.AddSeconds(-30)))
                                          .Select(args => args.DriverId).ToArray<int>();

                    
                      



                    IList<Fleet_Driver_OfflineJob> savedList2 = objBooking.Current.Fleet_Driver_OfflineJobs;

                    List<Fleet_Driver_OfflineJob> listofOfflineMsgs = (from a in grdDrivers.Rows.Where(c => c.Cells["colChk"].Value.ToBool())
                                                                       select new Fleet_Driver_OfflineJob

                                                                       {
                                                                       
                                                                            BookingId = JobId,
                                                                            DriverId = a.Cells["colId"].Value.ToIntorNull(),
                                                                            UpdatedOn=currentDateTime,
                                                                            OfflineMessage=msg,
                                                                           


                                                                       }).ToList();

                    listofOfflineMsgs.RemoveAll(c => loginDrvs.Count(a => a == c.DriverId) > 0);  

                    Utils.General.SyncChildCollection(ref savedList2, ref listofOfflineMsgs, "Id", skipProperties, c => c.OfflineMessage==msg);
                   

                    string[] drvIdArr=listofBidding.Select(c => c.DriverId.ToStr()).ToArray<string>();
                  
                    driverIds =string.Join(",",drvIdArr );




                    int loopCnt = 1;

                    new Thread(delegate()
                    {
                        while (loopCnt < 3)
                        {
                            bool success = General.SendMessageToPDA("request pda=" + driverIds + "=" + _objBooking.Id + "=" + msg).Result.ToBool();
                                
                              
                            if (success)
                            {
                                break;
                            }
                            else
                                loopCnt++;



                        }
                    }).Start();

                   
                }





                objBooking.CheckCustomerValidation = false;
                objBooking.CheckDataValidation = false;

                DateTime? biddingExpiryDateTime = objBooking.Current.PickupDateTime.Value.AddHours(-2);


                if (objBooking.Current.PickupDateTime.Value.Date <= DateTime.Now.Date)
                    biddingExpiryDateTime = objBooking.Current.PickupDateTime.Value.AddMinutes(-15);

                objBooking.Current.PriceBiddingExpiryDate = biddingExpiryDateTime;
              
                objBooking.Save();


              //  using(TaxiDataContext db=
           

                //string queryDeleteOfflineMsgs="delete from fleet_driver_offlinejobs where offlinemessage='"+msg+"' and  driverid in("+driverIds+")";

               

                //using (TaxiDataContext db = new TaxiDataContext())
                //{
                //      // db.stp_RunProcedure(.Where(c => c.Status == true).Select(c=>c.DriverId.ToList();

                //}

                Close();

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


       


    }
}
