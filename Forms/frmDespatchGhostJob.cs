using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Utils;
using Taxi_Model;
using Telerik.WinControls;
using Taxi_BLL;
using System.Threading;
using System.IO.Ports;
using Taxi_AppMain.Classes;
using System.Reflection;
using System.Diagnostics;

using System.IO;
using System.Net.Sockets;
using System.Net;
using DotNetCoords;

namespace Taxi_AppMain
{
    public partial class frmDespatchGhostJob : Form
    {
        private bool IsFojJob;

       
        private long _JobId;

        public long JobId
        {
            get { return _JobId; }
            set { _JobId = value; }
        }

        public bool ReDespatchJob = false;

        public frmDespatchGhostJob(Booking booking)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            this.SelectedDriverId = booking.DriverId;


            ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown += new KeyEventHandler(ddl_Driver_KeyDown);
        }

        public frmDespatchGhostJob(Booking booking,int? driverId, bool reSendJob)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            this.SelectedDriverId = driverId;

            ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown += new KeyEventHandler(ddl_Driver_KeyDown);
            ddl_Driver.Enabled = !reSendJob;
        }


        public frmDespatchGhostJob(Booking booking,bool fojJob)
        {
            InitializeComponent();
            this.IsFojJob = true;

            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            this.SelectedDriverId = booking.DriverId;
           

            ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown += new KeyEventHandler(ddl_Driver_KeyDown);
        }




        public frmDespatchGhostJob(long bookingId, Booking booking, int? driverId, Fleet_Driver driver, bool IsAutoDespatch)
        {
            InitializeComponent();
            this.JobId = bookingId;
            this.SelectedDriverId = driverId;

            LoadDespatchSettings(booking);
            LoadDriverSettings(driver);
            this.IsAutoDespatchActivity = IsAutoDespatch;
            if (!IsAutoDespatch)
            {

                LoadAndSelectDriver();
            }
        }

        private string OtherReason = string.Empty;

        public frmDespatchGhostJob(long bookingId, Booking booking, int? driverId, Fleet_Driver driver, bool IsAutoDespatch,string despatchReason)
        {
            InitializeComponent();
            this.JobId = bookingId;
            this.SelectedDriverId = driverId;
            this.OtherReason = despatchReason;
            LoadDespatchSettings(booking);
            LoadDriverSettings(driver);
            this.IsAutoDespatchActivity = IsAutoDespatch;
            if (!IsAutoDespatch)
            {

                LoadAndSelectDriver();
            }
        }



        //public frmDespatchJob(string custMobileNo, int? driverId, string fromAddress, string toAddress
        //                      , DateTime? pickUpdate, decimal fare, string customerName)
        //{
        //    InitializeComponent();
        //    this.Load += new EventHandler(frmDespatchJob_Load);

        //    if (string.IsNullOrEmpty(custMobileNo.Trim()))
        //    {
        //        lblCustomerMobNo.Visible = false;
        //        txtCustomerMobNo.Visible = false;

        //    }

        //    this.SelectedDriverId = driverId;



        //}

        void ddl_Driver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                OnKeyDespatch();
            }
        }

        private void OnKeyDespatch()
        {
              string text = ddl_Driver.Text;

                if (!string.IsNullOrEmpty(text.Trim()))
                {
                    RadListDataItem item = ddl_Driver.Items.FirstOrDefault(c => c.Text == text);

                    if (item != null)
                    {

                        ddl_Driver.SelectedItem = item;
                    }
                }

                Despatch();

        }

        void ddl_Driver_KeyUp(object sender, KeyEventArgs e)
        {
             if ( e.KeyCode == Keys.Enter)
            {
               OnKeyDespatch();
            }
        }

      


        private Fleet_Driver _objDriver;

        public Fleet_Driver ObjDriver
        {
            get { return _objDriver; }
            set { _objDriver = value; }
        }

        private bool _IsAutoDespatchActivity;

        public bool IsAutoDespatchActivity
        {
            get { return _IsAutoDespatchActivity; }
            set { _IsAutoDespatchActivity = value; }
        }

       





        private int? _SelectedDriverId;

        public int? SelectedDriverId
        {
            get { return _SelectedDriverId; }
            set { _SelectedDriverId = value; }
        }


 


        Booking objBooking = null;
        delegate void UIDel();

        void frmDespatchJob_Load(object sender, EventArgs e)
        {

            try
            {

                LoadDespatchSettings(objBooking);

                LoadAndSelectDriver();


               
                //if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() && (objBooking.ZoneId != null || objBooking.FromPostCode.ToStr().Length>0))
                //{
                //    new Thread(new ThreadStart(LoadNearestJobDrivers)).Start();
                   
                //}


                if (objBooking.JobCode.ToStr().Trim().Length == 0)
                {
                    txtTokenRequired.Visible = true;
                    btnDespatch.Enabled = false;
                    btnGenerateToken.Visible = true;



                }



              //  btnPrintToken.Visible = false;
            }
            catch (Exception ex)
            {


            }
        }


      

        void grdNearestDrv_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            if (e.Row != null && e.Row is GridViewDataRowInfo)
            {

                ddl_Driver.SelectedValue = e.Row.Cells["Id"].Value.ToInt();

            }

        }


          delegate void UIDelegate();
        private void LoadNearestJobDrivers()
        {

            try
            {

                if (this.InvokeRequired)
                {

                    UIDelegate d = new UIDelegate(LoadNearest);
                    this.BeginInvoke(d);
                }
                else
                {
                    LoadNearest();

                }




            }
            catch (Exception ex)
            {


            }

        }


        private void InitializeNearestDriverGrid()
        {
          
            this.grdNearestDrv = new System.Windows.Forms.DataGridView();
            // ((System.ComponentModel.ISupportInitialize)(this.grdDrivers)).BeginInit();



            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();



            this.DriverId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.details = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDespatchJob = new System.Windows.Forms.DataGridViewButtonColumn();

            this.DriverId.HeaderText = "DriverId";
            this.DriverId.Name = "DriverId";
            this.DriverId.ReadOnly = true;
            this.DriverId.Visible = false;
            // 
            // details
            // 
            this.details.HeaderText = "details";
            this.details.Name = "details";
            this.details.ReadOnly = true;
            this.details.Width = 195;
            // 
            // btnDespatchJob
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.btnDespatchJob.DefaultCellStyle = dataGridViewCellStyle2;
            this.btnDespatchJob.HeaderText = "btnDespatchJob";
            this.btnDespatchJob.Name = "btnDespatchJob";
            this.btnDespatchJob.ReadOnly = true;
            this.btnDespatchJob.Text = "Dispatch";
            this.btnDespatchJob.UseColumnTextForButtonValue = true;
            this.btnDespatchJob.Width = 80;


            this.grdNearestDrv.AllowUserToAddRows = false;
            this.grdNearestDrv.AllowUserToDeleteRows = false;
            this.grdNearestDrv.BackgroundColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNearestDrv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdNearestDrv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNearestDrv.ColumnHeadersVisible = false;
            this.grdNearestDrv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                    this.DriverId,
                    this.details,
                    this.btnDespatchJob});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FloralWhite;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdNearestDrv.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdNearestDrv.Location = new System.Drawing.Point(380, 80);
            this.grdNearestDrv.Name = "grdDrivers";
            this.grdNearestDrv.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNearestDrv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdNearestDrv.RowHeadersVisible = false;
            this.grdNearestDrv.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FloralWhite;
            this.grdNearestDrv.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.grdNearestDrv.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FloralWhite;
            this.grdNearestDrv.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.grdNearestDrv.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNearestDrv.RowTemplate.Height = 37;
            this.grdNearestDrv.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.grdNearestDrv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdNearestDrv.Size = new System.Drawing.Size(280, 190);
            this.grdNearestDrv.TabIndex = 226;
            this.grdNearestDrv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDrivers_CellClick);






            this.Controls.Add(this.grdNearestDrv);

         

            this.lblNearestDrv = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lblNearestDrv)).BeginInit();

            // 
            // lblNearestDrv
            // 
            this.lblNearestDrv.AutoSize = false;
            this.lblNearestDrv.BackColor = System.Drawing.Color.SteelBlue;
            this.lblNearestDrv.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNearestDrv.ForeColor = System.Drawing.Color.White;
            this.lblNearestDrv.Location = new System.Drawing.Point(380, 55);
            this.lblNearestDrv.Name = "lblNearestDrv";
            // 
            // 
            // 
            this.lblNearestDrv.RootElement.ForeColor = System.Drawing.Color.White;
            this.lblNearestDrv.Size = new System.Drawing.Size(280, 24);
            this.lblNearestDrv.TabIndex = 19;
            this.lblNearestDrv.Text = "Nearest Drivers";

            this.Controls.Add(this.lblNearestDrv);

            ((System.ComponentModel.ISupportInitialize)(this.lblNearestDrv)).EndInit();
        }

        private void grdDrivers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdNearestDrv.Columns[e.ColumnIndex].Name == "btnDespatchJob" && grdNearestDrv.CurrentCell is DataGridViewButtonCell)
            {
                    ddl_Driver.SelectedValue = grdNearestDrv.CurrentRow.Cells["DriverId"].Value.ToInt();
                    Despatch();
                


            }
        }


        private void LoadNearest()
        {
            try
            {

                string jobAddress = objBooking.FromPostCode.ToStr().Trim();

             Gen_Coordinate objJobCoord=   General.GetObject<Gen_Coordinate>(c => c.PostCode == jobAddress);


             if (objJobCoord != null)
             {

                 if (this.IsFojJob == false)
                 {

                     var ListofPDAAvailDrvs = (from a in AppVars.BLData.GetAll<Fleet_DriverQueueList>(c => c.Status == true && c.Fleet_Driver.HasPDA == true &&
                                     (c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)).AsEnumerable()
                                               join b in AppVars.BLData.GetAll<Fleet_Driver_Location>(c => c.Latitude != 0).AsEnumerable()
                                               on a.DriverId equals b.DriverId
                                               select new
                                               {
                                                   DriverId = a.DriverId,
                                                   DriverNo = a.Fleet_Driver.DriverNo,
                                                   DriverLocation = b.LocationName,
                                                   Latitude = b.Latitude,
                                                   Longitude = b.Longitude,
                                                   WaitSince=a.WaitSinceOn
                                                //   PlottedOn=  b.PlotDate
                                               }).ToList();

                     var nearestDrivers = ListofPDAAvailDrvs.Select(args => new
                     {
                         args.DriverId,
                         MilesAwayFromPickup = new LatLng(args.Latitude, args.Longitude).DistanceMiles(new LatLng(Convert.ToDouble(objJobCoord.Latitude), Convert.ToDouble(objJobCoord.Longitude))),
                         args.DriverNo,
                         Latitude = args.Latitude,
                         Longitude = args.Longitude,
                         Location = args.DriverLocation,
                         args.WaitSince

                     }).OrderBy(args => args.MilesAwayFromPickup).Take(5).ToList();

                     if (nearestDrivers.Count > 0)
                     {
                         this.Size = new Size(670, this.Size.Height);
                         this.StartPosition = FormStartPosition.CenterScreen;

                         InitializeNearestDriverGrid();

                         string waitSince = string.Empty;
                         for (int i = 0; i < nearestDrivers.Count; i++)
                         {
                             waitSince = string.Empty;

                             if (nearestDrivers[i].WaitSince != null)
                             {

                                 waitSince = string.Format("{0:HH:mm:ss}", DateTime.Now.Subtract(nearestDrivers[i].WaitSince.ToDateTime()));
                                 waitSince = waitSince.Remove(waitSince.LastIndexOf(":")).Trim() + " min(s)";


                                 if (waitSince.StartsWith("00"))
                                     waitSince = waitSince.Remove(0, waitSince.IndexOf(":") + 1).Trim();


                                 else if (waitSince.StartsWith("0"))
                                     waitSince = waitSince.Remove(0, 1);

                                 if (waitSince.Contains(":"))
                                     waitSince = waitSince.Replace(":", " hour(s) ").Trim();


                                 waitSince ="W/S :" + waitSince;

                             }

                             grdNearestDrv.Rows.Add(nearestDrivers[i].DriverId, "Drv " + nearestDrivers[i].DriverNo + " - " + Math.Round(nearestDrivers[i].MilesAwayFromPickup, 2) + " mi," + waitSince);

                         }

                     }
                 }
                 else
                 {
                     var ListofPDAAvailDrvs = (from a in AppVars.BLData.GetAll<Fleet_DriverQueueList>(c => c.Status == true && c.Fleet_Driver.HasPDA == true &&
                                  (c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.NOTAVAILABLE || c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONROUTE
                                  || c.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.ARRIVED || c.DriverWorkStatusId==Enums.Driver_WORKINGSTATUS.SOONTOCLEAR)).AsEnumerable()
                                               join b in AppVars.BLData.GetAll<Fleet_Driver_Location>(c => c.Latitude != 0).AsEnumerable()
                                               on a.DriverId equals b.DriverId
                                               select new
                                               {
                                                   DriverId = a.DriverId,
                                                   DriverNo = a.Fleet_Driver.DriverNo ,
                                                   DriverLocation = b.LocationName,
                                                   Latitude = b.Latitude,
                                                   Longitude = b.Longitude
                                               }).ToList();

                     var nearestDrivers = ListofPDAAvailDrvs.Select(args => new
                     {
                         args.DriverId,
                         MilesAwayFromPickup = new LatLng(args.Latitude, args.Longitude).DistanceMiles(new LatLng(Convert.ToDouble(objJobCoord.Latitude), Convert.ToDouble(objJobCoord.Longitude))),
                         args.DriverNo,
                         Latitude = args.Latitude,
                         Longitude = args.Longitude,
                         Location = args.DriverLocation

                     }).OrderBy(args => args.MilesAwayFromPickup).Take(5).ToList();

                     if (nearestDrivers.Count > 0)
                     {
                         this.Size = new Size(650, this.Size.Height);
                         this.StartPosition = FormStartPosition.CenterScreen;

                         InitializeNearestDriverGrid();


                         for (int i = 0; i < nearestDrivers.Count; i++)
                         {

                             grdNearestDrv.Rows.Add(nearestDrivers[i].DriverId, "Drv " + nearestDrivers[i].DriverNo +" - "+ Math.Round(nearestDrivers[i].MilesAwayFromPickup, 2) + " mi");

                         //    grdNearestDrv.Rows.Add(nearestDrivers[i].DriverId, nearestDrivers[i].DriverNo);

                         }

                     }


                 }
             }

            }
            catch (Exception ex)
            {


            }
        }


        private void LoadAndSelectDriver()
        {
            try
            {

                if (this.IsFojJob)
                    ComboFunctions.FillDriverNoFOJQueueCombo(ddl_Driver);

                else
                   ComboFunctions.FillDriverNoQueueCombo(ddl_Driver);
            
                IsLoaded = true;

                if (this.SelectedDriverId != null)
                {
                    ddl_Driver.SelectedValue = this.SelectedDriverId;

                }

            }
            catch (Exception ex)
            {


            }


        }

        public void LoadDespatchSettings(Booking booking)
        {
            try
            {

                if (booking != null)
                {
                    this.objBooking = booking;


                    lblDespatchHeading.Text = "Dispatch Ghost Job -  Ref No : " + objBooking.BookingNo.ToStr();

                    if (objBooking.JobCode.ToStr().Trim().Length > 0)
                    {
                        txtTokenNo.Text = objBooking.JobCode.ToStr().Trim();
                        txtTokenNo.Visible = true;
                        btnPrintToken.Visible = true;
                        //lblDespatchHeading .Text+= " Token # " + objBooking.JobCode.ToStr().Trim();

                    }
                    

                    if (booking.CustomerMobileNo.ToStr().Trim() == string.Empty)
                    {
                      //  lblCustomerMobNo.Visible = false;
                      //  txtCustomerMobNo.Visible = false;
                    }
                    else
                    {
                     //   lblCustomerMobNo.Visible = true;
                      //  txtCustomerMobNo.Visible = true;



                        string mobNo = booking.CustomerMobileNo.ToStr().Trim();
                        if (!Debugger.IsAttached)
                        {
                            if (mobNo.ToStr().StartsWith("00") == false)
                            {
                                int idx = -1;
                                if (mobNo.StartsWith("044") == true)
                                {
                                    idx = mobNo.IndexOf("044");
                                    mobNo = mobNo.Substring(idx + 3);
                                    mobNo = mobNo.Insert(0, "+44");


                                }

                                if (mobNo.StartsWith("07"))
                                {
                                    mobNo = mobNo.Substring(1);
                                }

                                if (mobNo.StartsWith("0440") == false || mobNo.StartsWith("+440") == false)
                                    mobNo = mobNo.Insert(0, "+44");
                            }
                        }



//txtCustomerMobNo.Text = mobNo;


                       

                    }

                }
            }
            catch (Exception ex)
            {


            }

        }



        private void btnDespatch_Click(object sender, EventArgs e)
        {
           
            Despatch();
           
        }

        private void Despatch()
        {
             
            int? driverId = ddl_Driver.SelectedValue.ToIntorNull();
            if (driverId == null)
            {
                ENUtils.ShowMessage("Please select a Driver");
                return;
            }
            else
            {

                try
                {

                    DateTime? serverDateTime = null;

                    if (ObjDriver != null && ObjDriver.HasPDA.ToBool() && ObjDriver.Fleet_Driver_PDASettings.Count > 0
                        && ObjDriver.Fleet_Driver_PDASettings[0].ShowJobAsAlert.ToBool()==false)
                        
                    {
                      


                        Booking objPendingBooking = General.GetQueryable<Booking>(null).Where(c => c.DriverId == ObjDriver.Id && c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING && c.Id != objBooking.Id && c.DespatchDateTime!=null)
                            .OrderByDescending(c => c.DespatchDateTime).FirstOrDefault();


                        if (objPendingBooking != null && objPendingBooking.DespatchDateTime != null)
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                serverDateTime = db.ExecuteQuery<DateTime>("select getdate()").FirstOrDefault();
                            }


                            //if (serverDateTime == null)
                            //    serverDateTime = DateTime.Now;
                        }
                     

                        if (objPendingBooking != null && objPendingBooking.DespatchDateTime != null && serverDateTime!=null && serverDateTime.Value.Subtract(objPendingBooking.DespatchDateTime.Value.AddSeconds(10)).TotalMinutes < (AppVars.objPolicyConfiguration.PDAJobOfferRequestTimeout.ToInt()))
                        {
                            ENUtils.ShowMessage("This Driver already have a Job Offer in Queue" + Environment.NewLine
                                               + "You cannot dispatch another job to this Driver until " +
                                              string.Format("{0:HH:mm:ss}", objPendingBooking.DespatchDateTime.Value.AddSeconds(10).AddMinutes(AppVars.objPolicyConfiguration.PDAJobOfferRequestTimeout.ToInt())));

                            return;
                        }
                        else
                        {
                           // int? jobStatusId = Enums.BOOKINGSTATUS.PENDING;

                            

                            // Restrict Controller to despatch pending jobs
                            //if(objBooking.DriverId!=null && objBooking.BookingStatusId.ToInt()==Enums.BOOKINGSTATUS.PENDING 
                            //    && objBooking.DriverId!=ObjDriver.Id &&  objBooking.DespatchDateTime != null )
                            //{
                            //    DateTime? newAvailableTime = objBooking.DespatchDateTime.Value.AddMinutes(1).AddSeconds(5);


                            //    if(DateTime.Now<newAvailableTime)
                            //    {
                            //        ENUtils.ShowMessage("You cannot despatch this job to other Driver before "+string.Format("{0:HH:mm:ss}",newAvailableTime));
                            //        return;

                            //    }

                            //}

                            string validateMsg = string.Empty;
                         //   DateTime? serverDateTime=null;
                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                if (db.Bookings.Count(c => c.Id == objBooking.Id && c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING
                                                          && c.DriverId != ObjDriver.Id && c.DespatchDateTime != null) > 0)
                                {

                                    if(serverDateTime==null)
                                       serverDateTime = db.ExecuteQuery<DateTime>("select getdate()").FirstOrDefault();

                                    if (serverDateTime != null)
                                    {
                                        Booking objBook = db.Bookings.FirstOrDefault(c => c.Id == objBooking.Id);


                                        if (objBook != null && serverDateTime!=null && objBook.DespatchDateTime!=null)
                                        {
                                            DateTime? newAvailableTime = objBook.DespatchDateTime.Value.AddMinutes(1).AddSeconds(5);


                                            if (serverDateTime < newAvailableTime)
                                             {

                                                 if (objBook.Booking_Logs.OrderByDescending(c => c.Id).FirstOrDefault(c => c.AfterUpdate.Contains("Auto Despatched")) != null)
                                                 {
                                                     validateMsg = "Job is already AutoDespatched to another driver" + Environment.NewLine +
                                                                "You cannot dispatch this job to other Driver before " + string.Format("{0:HH:mm:ss}", newAvailableTime);


                                                 }
                                                 else
                                                 {
                                                     validateMsg = "Job is already Despatched to another driver" + Environment.NewLine +
                                                              "You cannot dispatch this job to other Driver before " + string.Format("{0:HH:mm:ss}", newAvailableTime);


                                                 }                                               

                                             }

                                        }
                                    }
                                }



                            }


                            if (validateMsg.Length > 0)
                            {
                                ENUtils.ShowMessage(validateMsg);
                                        return;
                            }


                



                            if (ReDespatchJob==false && General.GetQueryable<Booking>(null).Count(c => c.Id == JobId && c.DriverId != ObjDriver.Id
                                && (c.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || c.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                                || c.BookingStatusId == Enums.BOOKINGSTATUS.POB)) > 0)
                            {

                                ENUtils.ShowMessage("This job is already despatched to another driver. " + Environment.NewLine + "Press Refresh Button to Update View");
                                return;


                            }
                             
                                 

                            this.IsFOJ = (General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == ObjDriver.Id.ToIntorNull() &&
                                                                  (c.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.AVAILABLE
                                                                   && c.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.ONBREAK) && c.CurrentJobId != null
                                                                   ).Count() > 0);

                            //this.IsFOJ = (General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == ObjDriver.Id.ToIntorNull() &&
                            //                                      (c.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.AVAILABLE
                            //                                       && c.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.ONBREAK) 
                            //                                       ).Count() > 0);

                        }


                    }
                }
                catch (Exception ex)
                {


                }

            }


            string bookingVehType = objBooking.Fleet_VehicleType.VehicleType.ToLower().Trim();


            if (ObjDriver.VehicleTypeId != null)
            {

                if (AppVars.listUserRights.Count(c => c.functionId == "RESTRICT ON DESPATCH JOB TO INVALID VEHICLE DRIVER") > 0)
                {

                    if (ObjDriver.Fleet_VehicleType.NoofPassengers.ToInt() < objBooking.Fleet_VehicleType.NoofPassengers.ToInt())
                    {
                        MessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                  "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + ".");
                                                              
                       
                            return;
                    }

                }
                else
                {


                    if (ObjDriver.Fleet_VehicleType.NoofPassengers.ToInt() < objBooking.Fleet_VehicleType.NoofPassengers.ToInt())
                    {
                        if (DialogResult.No == MessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                  "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine
                                                              + "Do you still want to Dispatch this Job to that Driver " + ObjDriver.DriverNo + " ?", "Dispatch", MessageBoxButtons.YesNo))
                        {
                            return;

                        }



                    }

                }

             
                //else
                //{



                    //if ((bookingVehType.StartsWith("mpv") || bookingVehType.StartsWith("mpv plus"))
                    //   && objBooking.VehicleTypeId != ObjDriver.VehicleTypeId
                    //   && (ObjDriver.Fleet_VehicleType.VehicleType.ToLower().Contains("saloon") || ObjDriver.Fleet_VehicleType.VehicleType.ToLower().Contains("low car")))
                    //{
                    //    RadMessageBox.Show("This Job is for " + bookingVehType + " Vehicle" + Environment.NewLine +
                    //                                               "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine);



                    //    return;
                    //}
                    //else if (bookingVehType.StartsWith("mpv plus") && ObjDriver.Fleet_VehicleType.VehicleType.ToLower().Contains("mpv") && ObjDriver.Fleet_VehicleType.VehicleType.ToLower().ToStr().Contains("mpv plus") == false)
                    //{

                    //    RadMessageBox.Show("This Job is for " + bookingVehType + " Vehicle" + Environment.NewLine +
                    //                                               "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + "." + Environment.NewLine);



                    //    return;
                    //}
            //    }
             
            }


            if (AppVars.objSubCompany.CompanyName.ToStr().Trim() == string.Empty)
            {
                ENUtils.ShowMessage("InComplete Company Information.." + Environment.NewLine +
                                    "Please Enter Company Information");


                frmSysPolicy frm = new frmSysPolicy(1);
                frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                frm.ControlBox = true;
                frm.MaximizeBox = false;
                frm.Size = new Size(750, 600);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }


            bool IsContinue = true;

            if (lblNocMessage.Text.Contains("Expired"))
            {
                if (HasDespatchExpiredDriversRights())
                {

                    if (DialogResult.No == RadMessageBox.Show(lblNocMessage.Text.Replace(" | ", Environment.NewLine) +Environment.NewLine+ Environment.NewLine
                                                            + "Are you sure you want to Dispatch the Job ?", "Dispatch", MessageBoxButtons.YesNo))
                    {
                        IsContinue = false;

                    }
                }
                else
                {

                    if (DialogResult.OK == RadMessageBox.Show("You cannot Dispatch the Job to this Driver!" + Environment.NewLine + lblNocMessage.Text.Replace(" | ", Environment.NewLine), "Dispatch", MessageBoxButtons.OK))
                    {
                        IsContinue = false;

                    }
                }
            }
          



            else if (ObjDriver != null)
            {
             
                
               if (AppVars.objPolicyConfiguration.DisableJobOfferToOnBreakDrv.ToBool()==false &&
                       General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.ONBREAK
                                                                            && c.DriverId == ObjDriver.Id).Count() > 0)
                    {
                        if (DialogResult.No == RadMessageBox.Show("This Driver is on Break." + Environment.NewLine
                                                           + "Do you still want to dispatch the job ?", "Dispatch", MessageBoxButtons.YesNo))
                        {
                            IsContinue = false;

                        }
                    }

               

                // New :- Check to Restrict Controller to Despatch Job for UnAvailble Driver on Shift
                if (IsContinue)
                {
                    if (AppVars.listUserRights.Count(c => c.functionId == "DESPATCH JOBS ON OVERSHIFT") == 0 && ObjDriver.Fleet_Driver_Shifts.Count > 0 
                        && ObjDriver.Fleet_Driver_Shifts.Count(c => c.Driver_Shift_ID != null && c.Driver_Shift_ID != 7) > 0)
                    {
                        string msg = string.Empty;
                        bool IsExist = false;
                        foreach (var item in ObjDriver.Fleet_Driver_Shifts)
                        {
                            if (item.Driver_Shift_ID != null && item.FromTime != null && item.ToTime != null)
                            {

                                if (item.Driver_Shift_ID.ToInt() == 7)
                                    IsExist = true;

                                if (item.Driver_Shift_ID.ToInt() != 7 && IsExist == false)
                                {
                                    string str = DateTime.Now.TimeOfDay.ToStr();

                                    str = str.Substring(0, str.LastIndexOf(':'));
                                    str = str.Replace(":", "").Trim();

                                    int time = str.ToInt();


                                    str = item.FromTime.Value.TimeOfDay.ToStr();
                                    str = str.Substring(0, str.LastIndexOf(':'));
                                    str = str.Replace(":", "").Trim();
                                    int fromTime = str.ToInt();


                                    str = item.ToTime.Value.TimeOfDay.ToStr();
                                    str = str.Substring(0, str.LastIndexOf(':'));
                                    str = str.Replace(":", "").Trim();
                                    int toTime = str.ToInt();

                                    if (time < 1000)
                                    {

                                        // PEAK FARES

                                        if (fromTime < 1000 && toTime < 1000)
                                        {
                                            if (time >= fromTime && time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }
                                        // 6 AM (600) TO 15 PM (1500)
                                        else if (fromTime < 1000 && toTime > 1000)
                                        {
                                            if (time >= fromTime && time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }

                                        // 6 PM (1800) TO 6 AM (600)
                                        else if (fromTime > 1000 && toTime < 1000)
                                        {

                                            if (time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }

                                        // OFF PEAK FARES

                                        if (fromTime < 1000 && toTime < 1000)
                                        {
                                            if (time >= fromTime
                                                    && time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }
                                        // 6 AM (600) TO 15 PM (1500)
                                        else if (fromTime < 1000 && toTime > 1000)
                                        {
                                            if (time >= fromTime
                                                    && time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }

                                        // 6 PM (1800) TO 6 AM (600)
                                        else if (fromTime > 1000 && toTime < 1000)
                                        {

                                            if (time <= toTime)
                                            {
                                                IsExist = true;
                                            }
                                        }

                                    }

                                    else if (time >= 1000)
                                    {
                                        if ((fromTime < 1000 && toTime >= 1000)
                                                || (fromTime >= 1000 && toTime >= 1000))
                                        {

                                            // 6 AM (600) TO 6PM (1700)
                                            if (time >= fromTime && time <= toTime)
                                            {
                                                IsExist = true;
                                            }

                                            else if ((fromTime >= 1000 && toTime < 1000))
                                            {

                                                if (time >= fromTime)
                                                {
                                                    IsExist = true;
                                                }
                                            }
                                            else if ((toTime > fromTime && time < (toTime - fromTime))
                                                || (fromTime > toTime && time > (fromTime - toTime)))
                                            {
                                                IsExist = true;

                                            }

                                        }

                                        else if ((fromTime < 1000 && toTime >= 1000)
                                                || (fromTime >= 1000 && toTime >= 1000))
                                        {

                                            // 6 AM (600) TO 6PM (1700)
                                            if (time >= fromTime
                                                    && time <= toTime)
                                            {
                                                IsExist = true;
                                            }

                                        }

                                        else if ((fromTime >= 1000 && toTime < 1000))
                                        {

                                            // 6 AM (600) TO 6PM (1700)
                                            if (time >= fromTime)
                                            {
                                                IsExist = true;
                                            }

                                        }

                                    }
                                }






                                if (item.Driver_Shift_ID.ToInt() != 7)
                                {
                                    msg += "Shift : " + item.Driver_Shift.ShiftName.ToStr().Trim()
                                   + " [" + string.Format("{0:HH:mm}", item.FromTime) + "," + string.Format("{0:HH:mm}", item.ToTime)+"]";
                                }



                            }
                        }


                        if (IsExist == false)
                        {
                            ENUtils.ShowMessage("Cannot Dispatch Job! Driver Shift is Over" + Environment.NewLine + msg);

                            return;
                        }
                    }

                }
            }


            if (IsContinue == false) return;

            DespatchJob();

        }


        private bool HasDespatchExpiredDriversRights()
        {

            return AppVars.listUserRights.Count(c => c.formName == "frmDriver" && c.functionId == "DESPATCH JOB TO EXPIRED DRIVERS") > 0;
            


            

        }



   
        bool enablePDA = false;
        public bool OnDespatching(ref List<string> listofErrors)
        {
            bool rtn = false;

            bool IsSuccess1 = true;
            bool IsSuccess2 = false;
            string smsError1 = string.Empty;
            string smsError2 = string.Empty;


         try
            {

                if (ObjDriver != null && objBooking != null)
                {

                    decimal drvPdaVersion = ObjDriver.Fleet_Driver_PDASettings.Count > 0 ? ObjDriver.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() : 9.40m;

                    if (drvPdaVersion < 11.7m && drvPdaVersion!=11.10m)
                    {
                        MessageBox.Show("This feature is available on Cab Treasure v11.7" + Environment.NewLine +
                                           "and driver " + ObjDriver.DriverNo + " is using version " + drvPdaVersion);

                        return false;
                    }



                    string customerMobileNo = string.Empty;
                    // For testing Purpose
                  //  customerMobileNo = "03323755646"; 
                    //
                    string customerName = objBooking.CustomerName;

                    string via = string.Join(",", objBooking.Booking_ViaLocations.Select(c => c.ViaLocValue.ToStr()).ToArray<string>());
                    
                    if (!string.IsNullOrEmpty(via.Trim()))
                        via = "Via: " + via;

                //    string specialReq = objBooking.SpecialRequirements.ToStr().Trim();
                    //if (!string.IsNullOrEmpty(specialReq))
                    //    specialReq = "Special Req: " + specialReq;


                  
                    enablePDA = AppVars.objPolicyConfiguration.EnablePDA.ToBool();

                    string custNo = !string.IsNullOrEmpty(objBooking.CustomerMobileNo) ? objBooking.CustomerMobileNo : objBooking.CustomerPhoneNo;
              

                    
                    // Send To Driver

                   
                   
                     IsSuccess2 = true;





                        string paymentType = objBooking.Gen_PaymentType.PaymentCategoryId==null? objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr()
                                :objBooking.Gen_PaymentType.Gen_PaymentCategory.CategoryName.ToStr();

                                string strDeviceRegistrationId = ObjDriver.DeviceId.ToStr();                           
                                string journey = "O/W";


                                if (objBooking.JourneyTypeId.ToInt() == 3)
                                {
                                    journey = "W/R";
                                }


                                string IsExtra = (objBooking.CompanyId != null || objBooking.FromLocTypeId == Enums.LOCATION_TYPES.AIRPORT || objBooking.ToLocTypeId == Enums.LOCATION_TYPES.AIRPORT) ? "1" : "0";
                                int i = 1;
                                string viaP = "";

                               

                                if (objBooking.Booking_ViaLocations.Count > 0)
                                {
                               
                                    viaP =  string.Join(" * ", objBooking.Booking_ViaLocations.Select(c =>"("+i++.ToStr() + ")"+ c.ViaLocValue.ToStr()).ToArray<string>());
                                }


                                string mobileNo = objBooking.CustomerMobileNo.ToStr();
                                string telNo=objBooking.CustomerPhoneNo.ToStr();

                              

                                if (string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(telNo))
                                {
                                    mobileNo = telNo;
                                }
                                else if (!string.IsNullOrEmpty(mobileNo) && !string.IsNullOrEmpty(telNo))
                                {
                                    mobileNo += "/" + telNo;
                                }


                                string pickUpPlot = "";                         
                                string dropOffPlot = "";
                                string companyName = string.Empty;

                                if (drvPdaVersion < 11 && objBooking.CompanyId != null && objBooking.Gen_Company.DefaultIfEmpty().AccountTypeId.ToInt() != Enums.ACCOUNT_TYPE.CASH)
                                    companyName = objBooking.Gen_Company.DefaultIfEmpty().CompanyName;
                                else
                                    companyName = objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();

                                if (drvPdaVersion > 9 && drvPdaVersion != 13.4m)
                                {
                                    pickUpPlot = objBooking.ZoneId != null ? "<<<" + objBooking.Gen_Zone1.ZoneName.ToStr() : "";
                                    dropOffPlot = objBooking.DropOffZoneId != null ? "<<<" + objBooking.Gen_Zone.ZoneName.ToStr() : "";
                                }

                               
                               string FOJJob = string.Empty;

                               

                                if (this.IsFOJ)
                                    FOJJob = "foj";




                                string startJobPrefix = "JobId:";
                              

                                string fromAddress = objBooking.FromAddress.ToStr().Trim();
                                string toAddress = objBooking.ToAddress.ToStr().Trim();

                                if (objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE || objBooking.FromLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                                {
                                    fromAddress = objBooking.FromStreet.ToStr() + " " + objBooking.FromAddress.ToStr();

                                }

                                if (objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.POSTCODE || objBooking.ToLocTypeId.ToInt() == Enums.LOCATION_TYPES.AIRPORT)
                                {
                                    toAddress = objBooking.ToStreet.ToStr() + " " + objBooking.ToAddress.ToStr();
                                }

                                 //half card and cash
                                string specialRequirements = objBooking.SpecialRequirements.ToStr();
                               

                                decimal pdafares =  objBooking.GetType().GetProperty(AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().Trim()).GetValue(objBooking, null).ToDecimal();

                              //  pdafares = objBooking.TotalCharges.ToDecimal();
                                
                                 string msg =string.Empty;

                                 if(drvPdaVersion>=11)
                                 {
                                     string showFares = ",\"ShowFares\":\""+ objBooking.Gen_PaymentType.ShowFaresOnPDA.ToStr().Trim()+"\"";

                                     startJobPrefix = "JobId:";
                                 
                                     msg = startJobPrefix+ "{ \"JobId\" :\"" +JobId.ToStr()+
                                           "\", \"Pickup\":\"" +  (!string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot)+
                                           "\", \"Destination\":\""+ "AS DIRECTED" +"\","+
                                           "\"PickupDateTime\":\""+ string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.PickupDateTime) +"\"" +
                                           ",\"Cust\":\"" + " " + "\",\"Mob\":\"" +  " " + "\",\"Fare\":\"" + 0.00 + "\",\"Vehicle\":\"" + " " + "\",\"Account\":\""+ "" + " " +"\"" +
                                             ",\"Lug\":\"" + "0" + "\",\"Passengers\":\"" + "0" +  "\",\"Journey\":\"" + " " + "\",\"Payment\":\"" + " " + "\",\"Special\":\"" + " " + " " + "\",\"Extra\":\"" + IsExtra + "\",\"Via\":\"" +  " " + " \",\"" +
                                      //        "Did\":\""+ ObjDriver.Id+"\",\"BabySeats\":\"" + objBooking.BabySeats.ToStr() + "\""   +showFares+" }";
                                                             "Did\":\"" + ObjDriver.Id + "\",\"BabySeats\":\"" + " " + "\"" + showFares + ",\"JobCode\":\"" + txtTokenNo.Text + "\"" + "}";
                                     

                                     //msg=  FOJJob + startJobPrefix + objBooking.Id +
                                     //   ":Pickup:" + (!string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot) +

                                     //   ":Destination:" + (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + "-" + toAddress + dropOffPlot : toAddress + dropOffPlot) +
                                     //     ":PickupDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.PickupDateTime) +
                                     //          ":Cust:" + objBooking.CustomerName + ":Mob:" + mobileNo + " " + ":Fare:" + pdafares
                                     //         + ":Vehicle:" + objBooking.Fleet_VehicleType.VehicleType + ":Account:" + companyName + " " +
                                     //         ":Lug:" + objBooking.NoofLuggages.ToInt() + ":Passengers:" + objBooking.NoofPassengers.ToInt() + ":Journey:" + journey +
                                     //         ":Payment:" + paymentType + ":Special:" + specialRequirements + " "
                                     //         + ":Extra:" + IsExtra + ":Via:" + viaP + " " + ":Did:" + ObjDriver.Id;


                                 }
                                 //else
                                 //{
                                    
                                 //     msg=  FOJJob + startJobPrefix + objBooking.Id +
                                 //       ":Pickup:" + (!string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot) +

                                 //       ":Destination:" + (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + "-" + toAddress + dropOffPlot : toAddress + dropOffPlot) +
                                 //         ":PickupDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.PickupDateTime) +
                                 //              ":Cust:" + objBooking.CustomerName + ":Mob:" + mobileNo + " " + ":Fare:" + pdafares
                                 //             + ":Vehicle:" + objBooking.Fleet_VehicleType.VehicleType + ":Account:" + companyName + " " +
                                 //             ":Lug:" + objBooking.NoofLuggages.ToInt() + ":Passengers:" + objBooking.NoofPassengers.ToInt() + ":Journey:" + journey +
                                 //             ":Payment:" + paymentType + ":Special:" + specialRequirements + " "
                                 //             + ":Extra:" + IsExtra + ":Via:" + viaP + " " + ":Did:" + ObjDriver.Id;




                                 //           if (AppVars.objPolicyConfiguration.EnableBabySeats.ToBool())
                                 //           {
                                 //               msg += ":BabySeats:" + objBooking.BabySeats.ToStr() + " ";
                                 //           }

                                 //}
                            

                               // if (objBooking.BookingTypeId.ToInt() == Enums.BOOKING_TYPES.SHUTTLE && objBooking.GroupJobId!=null && this.IsFOJ==false)
                                    
                               //{

                               //     string start = FOJJob + startJobPrefix;
                               //     string end = FOJJob + "GroupJobId:";

                               //     msg = msg.Replace(start, end);
                               //     msg += ":NoOfChilds:" + objBooking.NoOfChilds.ToInt() + ":DepartureDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.FlightDepartureDate)
                               //            + ":GroupNo:" + objBooking.GroupJobId;
                                    

                               //     StringBuilder groupMsgs = new StringBuilder();
                               //     foreach (var groupBooking in General.GetQueryable<Booking>(c=>c.GroupJobId==objBooking.GroupJobId && c.Id!=objBooking.Id && 
                               //                     (c.BookingStatusId==Enums.BOOKINGSTATUS.WAITING || c.BookingStatusId==Enums.BOOKINGSTATUS.NOTACCEPTED ||
                               //                      c.BookingStatusId == Enums.BOOKINGSTATUS.ONHOLD || c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING || c.BookingStatusId == Enums.BOOKINGSTATUS.REJECTED))
                               //                    )
                               //     {
                               //          groupMsgs.Append(

                               //                "<<<"+FOJJob+"GroupJobId:" + groupBooking.Id +
                               //               ":Pickup:" + (!string.IsNullOrEmpty(groupBooking.FromDoorNo) ? groupBooking.FromDoorNo + "-" + groupBooking.FromAddress + pickUpPlot : groupBooking.FromAddress + pickUpPlot) +

                               //                ":Destination:" + (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? groupBooking.ToDoorNo + "-" + groupBooking.ToAddress + dropOffPlot : groupBooking.ToAddress + dropOffPlot) +
                               //                ":PickupDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", groupBooking.PickupDateTime) +
                               //                ":Cust:" + groupBooking.CustomerName + ":Mob:" + mobileNo + " " + ":Fare:" + groupBooking.FareRate
                               //                  + ":Vehicle:" + groupBooking.Fleet_VehicleType.VehicleType + ":Account:" + companyName + " " +
                               //                ":Lug:" + groupBooking.NoofLuggages.ToInt() + ":Passengers:" + groupBooking.NoofPassengers.ToInt() + ":Journey:" + journey +
                               //               ":Payment:" + groupBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr() + ":Special:" + groupBooking.SpecialRequirements.ToStr() + " "
                               //              + ":Extra:" + IsExtra + ":Via:" + viaP + " " + ":Did:" + ObjDriver.Id    + ":NoOfChilds:" + objBooking.NoOfChilds.ToInt() + ":DepartureDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.FlightDepartureDate)
                               //            + ":GroupNo:" + objBooking.GroupJobId


                               //           );
                                         
                               //     }


                               //     msg = msg + groupMsgs.ToString();


                               // }


                                if (msg.Contains("\r\n"))
                                {
                                    msg = msg.Replace("\r\n", " ").Trim();
                                }
                                else
                                {
                                    if (msg.Contains("\n"))
                                    {
                                        msg = msg.Replace("\n", " ").Trim();

                                    }

                                }

                                if (msg.Contains("&"))
                                {
                                    msg = msg.Replace("&", "And");
                                }

                                if (msg.Contains(">"))
                                    msg = msg.Replace(">", " ");


                                if (msg.Contains("="))
                                    msg = msg.Replace("=", " ");

                               
                                  
                                        int loopCnt = 1;
                                    
                                        new Thread(delegate()
                                        {
                                            while (loopCnt < 3)
                                            {
                                                bool success = General.SendMessageToPDA("request pda=" + JobId + "=" + ObjDriver.Id + "=" + msg + "=1=" + ObjDriver.DriverNo).Result.ToBool();
                                                if (success)
                                                {
                                                    break;                                                  

                                                }
                                                else
                                                    loopCnt++;

                                            }
                                        }).Start();
                                 


                              
                               
                           

                           


                                int drvId = objBooking.DriverId.ToInt();
                                int newDriverid = ObjDriver.Id;
                                if ((objBooking.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING_START || objBooking.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.PENDING)
                                     && objBooking.DriverId != null && objBooking.DriverId.ToInt() != newDriverid)
                                {

                                    new Thread(delegate()
                                    {
                                        General.SendMessageToPDA("request pda=" + drvId + "=" + JobId + "=Cancelled Pre Job>>" + JobId + "=2");
                                    }).Start();


                                }
                                else
                                {

                                    if (ReDespatchJob && drvId > 0 && drvId != newDriverid)
                                    {


                                        if (objBooking.BookingStatusId.ToInt() == Enums.BOOKINGSTATUS.FOJ)
                                        {
                                            new Thread(delegate()
                                            {
                                                ReCallFOJBookingFromReDespatch(JobId, drvId);
                                            }).Start();

                                        }
                                        else
                                        {

                                            new Thread(delegate()
                                            {
                                                General.ReCallDespatchBooking(JobId, drvId);
                                            }).Start();


                                        }


                                    }

                                }




                                IsSuccess1 = true;

                    // Send To Customer
                   // if (AppVars.objPolicyConfiguration.EnablePassengerText.ToBool() && objBooking.DisablePassengerSMS.ToBool() == false)
               


                   
                    rtn = true;               

                   


                    //     ClosePort(objSMS.port);

                }
            }
            catch (Exception ex)
            {
                IsSuccess1 = false;

                listofErrors.Add(ex.Message);
                rtn = false;
            }


            return rtn;

        }





        private bool ReCallFOJBookingFromReDespatch(long jobId, int driverId)
        {

            bool rtn = true;

            try
            {
               
                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                {
                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Foj Job>>" + jobId + "=2").Result.ToBool();
                    }

                }
                else
                {

                    //For TCP Connection
                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                    {

                        rtn = General.SendMessageToPDA("request pda=" + driverId + "=" + jobId + "=Cancelled Foj Job>>" + jobId + "=2").Result.ToBool();
                    }


                }

            }
            catch (Exception ex)
            {

                //  ENUtils.ShowMessage(ex.Message);


            }


            return rtn;

        }
    




        private string GetMessage(string message)
        {
            try
            {


                string msg = message;

                object propertyValue = string.Empty;
                foreach (var tag in AppVars.listofSMSTags.Where(c => msg.Contains(c.TagMemberValue)))
                {

                    


                    switch (tag.TagObjectName)
                    {
                        case "booking":

                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = objBooking.GetType().GetProperty(val[0]).GetValue(objBooking, null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }

                            else
                            {
                                if (!string.IsNullOrEmpty(tag.ConditionNotNull) && objBooking.GetType().GetProperty(tag.ConditionNotNull) != null)
                                {

                                    if (tag.ConditionNotNull.ToStr() == "BabySeats" && tag.TagPropertyValue.ToStr() == "BabySeats")
                                    {
                                        propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);

                                        if(!string.IsNullOrEmpty(propertyValue.ToStr().Trim()) && propertyValue.ToStr().Contains("<<<"))
                                        {
                                            string[] arr = propertyValue.ToStr().Split(new string[] { "<<<" }, StringSplitOptions.None);

                                            propertyValue = "B Seat 1 : " + arr[0].ToStr() + Environment.NewLine + "B Seat 2 : " + arr[1].ToStr();

                                        }
                                        
                                    }
                                    else if (objBooking.GetType().GetProperty(tag.ConditionNotNull).GetValue(objBooking, null) != null)
                                    {
                                        propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                    }


                                    //else
                                    //{
                                    //    propertyValue = tag.ConditionNotNullReplacedValue.ToStr();
                                    //}

                                    
                                }
                                else
                                {

                                    propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                }
                            }


                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking, null);
                            }
                            break;


                        case "Booking_ViaLocations":
                            if (tag.TagPropertyValue == "ViaLocValue")
                            {


                                string[] VilLocs = null;
                                int cnt = 1;
                                VilLocs = objBooking.Booking_ViaLocations.Select(c =>cnt++.ToStr()+". "+ c.ViaLocValue).ToArray();
                                if (VilLocs.Count()>0)
                                {
                                  
                                    string Locations ="VIA POINT(s) : \n"+ string.Join("\n", VilLocs);
                                    propertyValue = Locations;
                                }
                                else
                                    propertyValue = string.Empty;

                            }
                            break;


                        case "driver":


                            if (tag.TagPropertyValue.Contains('.'))
                            {

                                string[] val = tag.TagPropertyValue.Split(new char[] { '.' });

                                object parentObj = ObjDriver.GetType().GetProperty(val[0]).GetValue(ObjDriver, null);

                                if (parentObj != null)
                                {
                                    propertyValue = parentObj.GetType().GetProperty(val[1]).GetValue(parentObj, null);
                                }
                                else
                                    propertyValue = string.Empty;


                                break;
                            }

                            else
                            {
                                propertyValue = ObjDriver.GetType().GetProperty(tag.TagPropertyValue).GetValue(ObjDriver, null);
                            }

                            if (string.IsNullOrEmpty(propertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                propertyValue = ObjDriver.GetType().GetProperty(tag.TagPropertyValue2).GetValue(ObjDriver, null);
                            }
                            break;


                        default:
                            propertyValue = AppVars.objSubCompany.GetType().GetProperty(tag.TagPropertyValue).GetValue(AppVars.objSubCompany, null);
                            break;

                    }




                    msg = msg.Replace(tag.TagMemberValue,
                        tag.TagPropertyValuePrefix.ToStr() + string.Format(tag.TagDataFormat, propertyValue) + tag.TagPropertyValueSuffix.ToStr());

                }


                return msg.Replace("\n\n", "\n");
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);
                return "";
            }
        }



        private bool SendDespatchSMS(EuroSMS objSMS, string msg, string mobileNo)
        {

            bool rtn = true;
            try
            {
            

                string smsError1 = "";

                objSMS.ToNumber = mobileNo;
                objSMS.Message = msg;
                System.Threading.Thread.Sleep(7000);
                rtn = objSMS.Send(ref smsError1);
            }
            catch (Exception ex)
            {
                // ENUtils.ShowMessage(ex.Message);


            }

            return rtn;

        }

        private void ClosePort(SerialPort port)
        {
            if (port != null)
                port.Close();


        }

        public bool SuccessDespatched = false;

        private bool _IsFOJ;

        public bool IsFOJ
        {
            get { return _IsFOJ; }
            set { _IsFOJ = value; }
        }

        public bool IsPDADriver = false;
        public  bool IsDespatched = false;

        public void DespatchJob()
        {

            List<string> listofErrors = new List<string>();

            IsDespatched = OnDespatching(ref listofErrors);

            if (IsDespatched)
            {
            
                try
                {

                  //  if (this.JobStatusId == null)
                     //   this.JobStatusId = Enums.BOOKINGSTATUS.PENDING;

                    if (IsAutoDespatchActivity == false && ReDespatchJob == true)
                    {
                        OtherReason = "Job Re-Despatched to Drv ("+ ObjDriver.DriverNo.ToStr()+") from BookingList";
                    }


                    (new TaxiDataContext()).stp_DespatchedGhostJob(this.JobId, ObjDriver.Id, ObjDriver.DriverNo.ToStr(), ObjDriver.HasPDA.ToBool(), enablePDA, false, IsAutoDespatchActivity, AppVars.LoginObj.LoginName.ToStr(), Enums.BOOKINGSTATUS.PENDING, false, OtherReason, objBooking.JobCode.ToStr().Trim());

             

                    bool autoDespatch = objBooking.AutoDespatch.ToBool();
                    if ((!this.IsAutoDespatchActivity || !autoDespatch))
                    {

                        if (enablePDA == false || ObjDriver.HasPDA==false)
                        {
                            int? driverId = ObjDriver.Id.ToIntorNull();

                            Fleet_DriverQueueList driverCurrent = General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == driverId)
                                                                               .OrderByDescending(c => c.Id).FirstOrDefault();

                            if (driverCurrent != null)
                            {
                                if ((enablePDA == false && AppVars.objPolicyConfiguration.EnableOnBoardDrivers.ToBool() == true) || (ObjDriver.HasPDA.ToBool() == false && enablePDA))
                                {
                                    General.OnDespatchUpdateDriverQueue(driverCurrent.Id, objBooking.Id, General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim()));

                                    RefreshBookingList();
                                 

                                }
                                else if (enablePDA == false)
                                {
                                    General.UpdateDriverQueue(driverCurrent.Id, objBooking.Id, General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim()));
                                    RefreshBookingList();
                                }
                            }
                        }

                        SuccessDespatched = true;


                        if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                        {

                            RadDesktopAlert alert = new RadDesktopAlert();
                            alert.AutoCloseDelay = 5;
                            alert.FadeAnimationType = FadeAnimationType.None;
                            alert.FadeAnimationSpeed = 1;
                            //  desktopAlert.FadeAnimationType = FadeAnimationType.None;
                            //     desktopAlert.Popup.Opacity = 10;
                            alert.Popup.AlertElement.Opacity = 100;


                            alert.CaptionText = "Ghost Job Dispatch Successfully";
                            alert.ContentText = "Driver : " + ObjDriver.DriverNo + " - "+ ObjDriver.DriverName;

                       
                            alert.Show();
                        }
                        
                    }

                    if (!this.IsAutoDespatchActivity)
                    {
                        new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_ACTIVE_DASHBOARD);                   
                    }

                    this.Close();
                }
                catch (Exception ex)
                {
                   
                    ENUtils.ShowMessage(ex.Message);
               
                }
            }
            else
            {

               // lblNocMessage.Text = "Job Dispatch Failed..";
               // lblNocMessage.ForeColor = Color.Red;
               // lblNocMessage.Visible = true;

                int cnt = listofErrors.Count;
                if (cnt == 1)
                {
                    lblSmsError1.Visible = true;
                    lblSmsError1.Text = listofErrors[0].ToStr();

                }

                if (cnt == 2)
                {
                    lblSmsError1.Visible = true;
                    lblSmsError1.Text = listofErrors[0].ToStr();

                    lblSMSError2.Visible = true;
                    lblSMSError2.Text = listofErrors[1].ToStr();

                }


            }


        }


        private void RefreshBookingList()
        {

            if (Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBookingsList") > 0)
            {
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingsList") as frmBookingsList).SetRefreshWhenActive("");
            }

           // General.RefreshListWithoutSelected<frmBookingsList>("frmBookingsList1");


        }
     





        bool IsLoaded = false;
        private void ddl_Driver_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (IsLoaded)
                {
                    int id = ddl_Driver.SelectedValue.ToInt();
                    Fleet_Driver obj = General.GetObject<Fleet_Driver>(c => c.Id == id);
                    LoadDriverSettings(obj);
                }

            }
            catch (Exception ex)
            {
               

            }


        }


        private void LoadDriverSettings(Fleet_Driver driver)
        {
             try
            {
                if (driver != null)
                {
                    this.ObjDriver = driver;
                   // lblDriverMobNo.Visible = true;
                  //  txtDriverMobNo.Visible = true;
                    if (string.IsNullOrEmpty(driver.MobileNo.ToStr().Trim()))
                    {
                        //txtDriverMobNo.Text = "Not found";
                      //  txtDriverMobNo.ForeColor = Color.Red;
                    }
                    else
                    {


                        string mobNo = driver.MobileNo.ToStr().Trim();
                        int idx = -1;
                        if (!Debugger.IsAttached)
                        {
                            if (mobNo.ToStr().StartsWith("00") == false)
                            {

                                if (mobNo.StartsWith("044") == true)
                                {
                                    idx = mobNo.IndexOf("044");
                                    mobNo = mobNo.Substring(idx + 3);
                                    mobNo = mobNo.Insert(0, "+44");


                                }

                                if (mobNo.StartsWith("07"))
                                {
                                    mobNo = mobNo.Substring(1);
                                }

                                if (mobNo.StartsWith("044") == false || mobNo.StartsWith("+44") == false)
                                    mobNo = mobNo.Insert(0, "+44");
                            }
                        }

                      
                        DateTime nowDate = DateTime.Now.ToDate();

                      
                        //


                        lblNocMessage.Visible = false;
                        lblNocMessage.Text = string.Empty;

                        if (driver.MOTExpiryDate != null && driver.MOTExpiryDate.ToDate() < nowDate)
                        {
                            lblNocMessage.Visible = true;
                            lblNocMessage.Text = "MOT Expired : " + string.Format("{0:dd/MM/yyyy}", driver.MOTExpiryDate) + " | ";
                            lblNocMessage.ForeColor = Color.Red;


                        }
                        if (driver.MOT2ExpiryDate != null && driver.MOT2ExpiryDate.ToDate() < nowDate)
                        {
                            lblNocMessage.Visible = true;
                            lblNocMessage.Text += "MOT 2 Expired : " + string.Format("{0:dd/MM/yyyy}", driver.MOT2ExpiryDate) + " | ";
                            lblNocMessage.ForeColor = Color.Red;

                        }

                        if (driver.InsuranceExpiryDate != null && (driver.InsuranceExpiryDate.ToDateTime() < DateTime.Now || ObjDriver.InsuranceExpiryDate < objBooking.PickupDateTime))
                        {
                            lblNocMessage.Visible = true;
                            lblNocMessage.Text += "Insurance Expired : " + string.Format("{0:dd/MM/yyyy HH:mm}", driver.InsuranceExpiryDate) + " | ";
                            lblNocMessage.ForeColor = Color.Red;

                        }

                        if (driver.PCODriverExpiryDate != null && driver.PCODriverExpiryDate.ToDate() < nowDate)
                        {
                            lblNocMessage.Visible = true;
                            lblNocMessage.Text += "PCO Driver Expired : " + string.Format("{0:dd/MM/yyyy}", driver.PCODriverExpiryDate) + " | ";
                            lblNocMessage.ForeColor = Color.Red;

                        }

                        if (driver.PCOVehicleExpiryDate != null && driver.PCOVehicleExpiryDate.ToDate() < nowDate)
                        {
                            lblNocMessage.Visible = true;
                            lblNocMessage.Text += "PCO Vehicle Expired : " + string.Format("{0:dd/MM/yyyy}", driver.PCOVehicleExpiryDate) + " | ";
                            lblNocMessage.ForeColor = Color.Red;
                        }

                        if (driver.DrivingLicenseExpiryDate != null && driver.DrivingLicenseExpiryDate.ToDate() < nowDate)
                        {
                            lblNocMessage.Visible = true;
                            lblNocMessage.Text += "Driving License Expired : " + string.Format("{0:dd/MM/yyyy}", driver.DrivingLicenseExpiryDate) + " | ";
                            lblNocMessage.ForeColor = Color.Red;
                        }


                        if (ObjDriver.RoadTaxiExpiryDate != null && ObjDriver.RoadTaxiExpiryDate.ToDate() < nowDate)
                        {

                            lblNocMessage.Visible = true;
                            lblNocMessage.Text += "Road Tax Expired : " + string.Format("{0:dd/MM/yyyy}", ObjDriver.RoadTaxiExpiryDate);
                            lblNocMessage.ForeColor = Color.Red;
                            //  msg += "Road Tax Expired : " + string.Format("{0:dd/MM/yyyy}", ObjDriver.RoadTaxiExpiryDate);

                        }






                    }

                    IsPDADriver = driver.HasPDA.ToBool();

                    if (IsPDADriver)
                    {

                        if (ObjDriver.Fleet_Driver_Locations.Count > 0)
                        {
                            DateTime updateDate = ObjDriver.Fleet_Driver_Locations[0].UpdateDate;

                            lblLastGPSConn.Text = "Last Connection made on " + string.Format("{0:dd-MMM}", updateDate) + " at " + string.Format("{0:HH:mm}", updateDate);
                        }
                        else
                        {
                            lblLastGPSConn.Visible = false;
                        }

                    }

                    lblLastGPSConn.Visible = IsPDADriver;
                }
                else
                    lblLastGPSConn.Visible = false;

            }
             catch (Exception ex)
             {
                 lblLastGPSConn.Visible = false;

             }

        }

      
        private void frmDespatchJob_KeyDown(object sender, KeyEventArgs e)
        {
            
             if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        private void btnGenerateToken_Click(object sender, EventArgs e)
        {
            if (txtTokenNo.Tag == null)
            {

                txtTokenNo.Text = new Taxi_BLL.SysPolicy_AutoGeneratedCodesBO().GetSequenceNumber("frmBooking", objBooking.SubcompanyId);
                txtTokenNo.Tag = txtTokenNo.Text;


                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.stp_RunProcedure("update booking set jobcode='" + txtTokenNo.Text.Trim() + "' where Id=" + objBooking.Id);

                }

            }


            txtTokenNo.Visible = true;
            btnDespatch.Enabled = true;
            btnPrintToken.Visible = true;
        }

        private void btnPrintToken_Click(object sender, EventArgs e)
        {
            if (objBooking!=null && objBooking.JobCode.ToStr().Trim().Length > 0)
                PrintBookingNo(objBooking.JobCode);
        }


        private void PrintBookingNo(string JobNo)
        {
            try
            {
                rptfrmJobNo frm = null;

                if (!string.IsNullOrEmpty(JobNo))
                {

                    ReportPrintDocument rpt = null;

                    frm = new rptfrmJobNo(JobNo);

                    frm.LaodReport();
                    rpt = new ReportPrintDocument(frm.reportViewer1.LocalReport);
                    rpt.Print();
                    rpt.Dispose();
                }


            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


       


    
      
       
       


    }
}
