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

using Taxi_AppMain.Classes;
using System.Reflection;
using System.Diagnostics;

using System.IO;
using System.Net.Sockets;
using System.Net;
using DotNetCoords;
using System.Xml;


namespace Taxi_AppMain
{
    public partial class frmDespatchJob : Form
    {
        private bool IsFojJob;

       
        private long _JobId;

        public long JobId
        {
            get { return _JobId; }
            set { _JobId = value; }
        }

        public bool ReDespatchJob = false;

        public frmDespatchJob(Booking booking)
        {
            InitializeComponent();
            this.Load += new EventHandler(frmDespatchJob_Load);
            this.JobId = booking.Id;
            this.objBooking = booking;
            this.SelectedDriverId = booking.DriverId;


            ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown += new KeyEventHandler(ddl_Driver_KeyDown);
        }

        public frmDespatchJob(Booking booking,int? driverId, bool reSendJob)
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


        public frmDespatchJob(Booking booking,bool fojJob)
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




        public frmDespatchJob(long bookingId, Booking booking, int? driverId, Fleet_Driver driver, bool IsAutoDespatch)
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

        public frmDespatchJob(long bookingId, Booking booking, int? driverId, Fleet_Driver driver, bool IsAutoDespatch,string despatchReason)
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




                CallNearestDrivers();



            }
            catch (Exception ex)
            {


            }
        }

        private void CallNearestDrivers()
        {
            try
            {

                if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() && (objBooking.ZoneId != null || objBooking.FromPostCode.ToStr().Length > 0))
                {
                    new Thread(new ThreadStart(LoadNearestJobDrivers)).Start();

                }
            }
            catch
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
            this.grdNearestDrv.Location = new System.Drawing.Point(380, 92);
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
            this.grdNearestDrv.Size = new System.Drawing.Size(280, 220);
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
            this.lblNearestDrv.Location = new System.Drawing.Point(380, 70);
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
                if (jobAddress.Contains(" ") == false)
                {
                    jobAddress = General.GetPostCodeMatch(objBooking.FromAddress.ToStr());

                }

             Gen_Coordinate objJobCoord=   General.GetObject<Gen_Coordinate>(c => c.PostCode == jobAddress);
                

             if (objJobCoord != null)
             {

                         if (this.IsFojJob == false)
                         {

                             using (TaxiDataContext db = new TaxiDataContext())
                             {


                                 var ListofPDAAvailDrvs = (from a in db.Fleet_DriverQueueLists.Where(c => c.Status == true  && c.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)
                                                           join b in db.Fleet_Driver_Locations.Where(c => c.Latitude != 0)                                                         
                                                           on a.DriverId equals b.DriverId
                                                           select new
                                                           {
                                                               DriverId = a.DriverId,
                                                               DriverNo = a.Fleet_Driver.DriverNo,
                                                               DriverLocation = b.LocationName,
                                                               Latitude = b.Latitude,
                                                               Longitude = b.Longitude,
                                                               WaitSince = a.WaitSinceOn,
                                                               VehicleTypeId = a.Fleet_Driver.VehicleTypeId,
                                                               NoofPassengers = a.Fleet_Driver.Fleet_VehicleType.NoofPassengers
                                                            
                                                           }).ToList();


                                 var ListofPDAAvailDrvs2 = ListofPDAAvailDrvs.Where(c => c.DriverId == 0).ToList();
                                 string vehAttributes = objBooking.Fleet_VehicleType.DefaultIfEmpty().AttributeValues.ToStr().Trim();

                                 if (vehAttributes.Length > 0)
                                 {



                                     ListofPDAAvailDrvs2 = ListofPDAAvailDrvs.Where(c => vehAttributes.Contains("," + c.VehicleTypeId.ToStr() + ",")).ToList();


                                 }
                                 else
                                 {
                                     ListofPDAAvailDrvs2 = ListofPDAAvailDrvs.Where(c => c.NoofPassengers >= objBooking.Fleet_VehicleType.NoofPassengers).ToList();

                                 }

                                 var nearestDrivers = ListofPDAAvailDrvs2.Select(args => new
                                 {
                                     args.DriverId,
                                     MilesAwayFromPickup = new LatLng(args.Latitude, args.Longitude).DistanceMiles(new LatLng(Convert.ToDouble(objJobCoord.Latitude), Convert.ToDouble(objJobCoord.Longitude))),
                                     //MilesAwayFromPickup=distance(args.Latitude, args.Longitude,(Convert.ToDouble(objJobCoord.Latitude)), Convert.ToDouble(objJobCoord.Longitude),'N'),
                                     args.DriverNo,
                                     Latitude = args.Latitude,
                                     Longitude = args.Longitude,
                                     Location = args.DriverLocation,
                                     args.WaitSince

                                 }).OrderBy(args => args.MilesAwayFromPickup).Take(3).ToList();

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
                                             try
                                             {
                                                 TimeSpan remainingWaiting = DateTime.Now.Subtract(nearestDrivers[i].WaitSince.ToDateTime());

                                                 waitSince = string.Format("{0:HH:mm}", remainingWaiting);
                                                 waitSince = waitSince.Remove(waitSince.LastIndexOf(":")).Trim() + " min(s)";


                                                 if (waitSince.StartsWith("00"))
                                                     waitSince = waitSince.Remove(0, waitSince.IndexOf(":") + 1).Trim();


                                                 else if (waitSince.StartsWith("0"))
                                                     waitSince = waitSince.Remove(0, 1);

                                                 if (waitSince.Contains(":"))
                                                     waitSince = waitSince.Replace(":", " hour(s) ").Trim();


                                                 waitSince = "W/S :" + waitSince;
                                             }
                                             catch
                                             {


                                             }
                                         }



                                         // GMap.NET.GDirections routedir;       
                                         // var routefromtable = GMap.NET.MapProviders.GMapProviders.OpenStreetMap.GetRouteBetweenPoints( new GMap.NET.PointLatLng(Convert.ToDouble(objJobCoord.Latitude), Convert.ToDouble(objJobCoord.Longitude)), new GMap.NET.PointLatLng(Convert.ToDouble(nearestDrivers[i].Latitude), Convert.ToDouble(nearestDrivers[i].Longitude)), true,false,10);

                                         //  grdNearestDrv.Rows.Add(nearestDrivers[i].DriverId, "Drv " + nearestDrivers[i].DriverNo + " - " + routedir.Distance + " mi, " + "ETA : " + routedir.Duration + " min(s)");

                                         double miles = Convert.ToDouble(nearestDrivers[i].MilesAwayFromPickup);
                                         //if (AppVars.objPolicyConfiguration.AutoDespatchNearestDrvRadius.ToDecimal() > 0.00m &&
                                         //  (nearestDrivers[i].MilesAwayFromPickup.ToDecimal() <= AppVars.objPolicyConfiguration.AutoDespatchNearestDrvRadius.ToDecimal() && nearestDrivers[i].MilesAwayFromPickup.ToDecimal() >= 0.5m))
                                         //{

                                         //    miles = GetNearestDriverRadiusOnline(nearestDrivers[i].Latitude, nearestDrivers[i].Longitude, objJobCoord.Latitude, objJobCoord.Longitude);

                                         //  //  miles = GetNearestDriverRadiusOnlineGoogle(nearestDrivers[i].Latitude, nearestDrivers[i].Longitude, objJobCoord.Latitude, objJobCoord.Longitude);


                                         //    if(miles==0)
                                         //        Convert.ToDouble(nearestDrivers[i].MilesAwayFromPickup);
                                         //}

                                         grdNearestDrv.Rows.Add(nearestDrivers[i].DriverId, "Drv " + nearestDrivers[i].DriverNo + " - " + " ETA : " + Math.Round(miles, 1) + " mi, " + Math.Ceiling((nearestDrivers[i].MilesAwayFromPickup / 0.25)) + " min(s), " + waitSince);

                                     }

                                 }
                             }
                 }
                 else
                 {

                     using (TaxiDataContext db = new TaxiDataContext())
                     {
                            db.CommandTimeout = 4;


                         var nearestDrivers = (from a in db.Fleet_DriverQueueLists


                                                   join b in db.Bookings on a.CurrentJobId equals b.Id
                                                   join d in db.Fleet_Drivers on a.DriverId equals d.Id
                                                   join v in db.Fleet_VehicleTypes on d.VehicleTypeId equals v.Id


                                                   where a.Status == true && ( a.DriverWorkStatusId == Enums.Driver_WORKINGSTATUS.SOONTOCLEAR)



                                                   select new ClsDrivers
                                                   {
                                                        DriverId = a.DriverId,
                                                       DriverNo =d.DriverNo,
                                                        LocationName =General.GetPostCodeMatch(b.ToAddress),
                                                        VehicleTypeId=d.VehicleTypeId,
                                                         NoOfPassenger=v.NoofPassengers
                                                     
                                                     
                                                   }).ToList();


                            if (nearestDrivers.Count > 0)
                            {
                                string vehAttributes = objBooking.Fleet_VehicleType.DefaultIfEmpty().AttributeValues.ToStr().Trim();


                                if (vehAttributes.Length > 0)
                                {



                                    nearestDrivers = nearestDrivers.Where(c => vehAttributes.Contains("," + c.VehicleTypeId.ToStr() + ",")).ToList();


                                }
                                else
                                {
                                    nearestDrivers = nearestDrivers.Where(c => c.NoOfPassenger >= objBooking.Fleet_VehicleType.NoofPassengers).ToList();

                                }
                            }




                            for (int i = 0; i < nearestDrivers.Count; i++)
                            {

                                if (nearestDrivers[i].LocationName.ToStr().Trim().Length > 0)
                                {
                                    var args = db.stp_getCoordinatesByAddress(nearestDrivers[i].LocationName, nearestDrivers[i].LocationName).FirstOrDefault();


                                    if (args != null)
                                    {

                                        nearestDrivers[i].Distance = new LatLng(Convert.ToDouble(args.Latitude), Convert.ToDouble(args.Longtiude))
                                            .DistanceMiles(new LatLng(Convert.ToDouble(objJobCoord.Latitude), Convert.ToDouble(objJobCoord.Longitude)));


                                    }
                                    else
                                        nearestDrivers[i].Distance = -1;


                                }
                               

                            }


                            foreach (var item in nearestDrivers.Where(c => c.Distance > 0).OrderBy(c => c.Distance).Take(5))
                            {
                                if (grdNearestDrv == null)
                                {

                                    this.Size = new Size(670, this.Size.Height);
                                    this.StartPosition = FormStartPosition.CenterScreen;

                                    InitializeNearestDriverGrid();

                                   // if (showSTC)
                                       

                                }

                             //   btnDespatchJob.Text = "Dispatch as FOJ";

                                grdNearestDrv.Rows.Add(item.DriverId, "Drv " + item.DriverNo + " - " + " ETA : " + Math.Round(item.Distance.ToDecimal(), 1) + " mi, " + Math.Ceiling((Convert.ToDouble(item.Distance) / 0.25)) + " min(s)");

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
                    ComboFunctions.FillDriverNoFOJDespatchQueueCombo(ddl_Driver);

                else
                {
                    try
                    {
                        ComboFunctions.FillDriverNoQueueDespatchCombo(ddl_Driver);

                        ddl_Driver.Items.Where(c => c.DataBoundItem != null && c.DataBoundItem.ToStr().Contains("IsManualLogin = True")).ToList().ForEach(c => c.ForeColor = Color.Red);
                    }
                    catch
                    {

                    }

                }
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

                    if (this.IsFojJob)
                    {
                        lblDespatchHeading.Text = "Dispatch FOJ Job - Ref No : " + objBooking.BookingNo.ToStr();

                    }
                    else
                    {
                        lblDespatchHeading.Text = "Dispatch Job - Ref No : " + objBooking.BookingNo.ToStr() ;
                    }

                    txtJobDetails.Text = "Pickup :   " + objBooking.FromAddress.ToStr().Trim()+ " - @" + string.Format("{0:HH:mm}", objBooking.PickupDateTime)+
                                                      Environment.NewLine+"DropOff : "+objBooking.ToAddress.ToStr().Trim();



                    if (objBooking.AttributeValues.ToStr().Length > 0)
                    {

                        txtJobDetails.Text += Environment.NewLine + "Attributes [" + objBooking.AttributeValues.ToStr().Trim() + "]";
                        txtJobDetails.Size = new Size(txtJobDetails.Width, txtJobDetails.Height + 10);
                    }

                    //if (txtJobDetails.Text.Length > 130)
                    //{
                    //    txtJobDetails.Font = new Font("Tahoma", 8, FontStyle.Bold);

                    //}

                    if (booking.CustomerMobileNo.ToStr().Trim() == string.Empty)
                    {
                        lblCustomerMobNo.Visible = false;
                        txtCustomerMobNo.Visible = false;
                    }
                    else
                    {
                        lblCustomerMobNo.Visible = true;
                        txtCustomerMobNo.Visible = true;



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



                        txtCustomerMobNo.Text = mobNo;


                       

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


        public bool IsQuickReDispatch;
        private bool IsManualLogin = false;

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


                    try
                    {
                        if (ddl_Driver.SelectedItem != null && ddl_Driver.SelectedItem.DataBoundItem != null)
                        {

                            string manualLogin = ddl_Driver.SelectedItem.DataBoundItem.ToStr().Substring(ddl_Driver.SelectedItem.DataBoundItem.ToStr().IndexOf("IsManualLogin"));


                            if (manualLogin.ToStr().ToLower().Contains("true"))
                            {

                                IsManualLogin = true;

                            }

                        }
                    }
                    catch
                    {


                    }

                    if (IsFojJob && AppVars.objPolicyConfiguration.FOJLimit.ToInt() > 0 && General.GetQueryable<Booking>(c => c.BookingStatusId == Enums.BOOKINGSTATUS.FOJ && c.DriverId == driverId).Count() >= AppVars.objPolicyConfiguration.FOJLimit.ToInt())
                    {
                        ENUtils.ShowMessage("You cannot dispatch more than "+AppVars.objPolicyConfiguration.FOJLimit.ToInt()+ " FOJ to a Driver");
                        return;

                    }


                    DateTime? serverDateTime = null;

                    if (ObjDriver != null && ObjDriver.HasPDA.ToBool() && IsManualLogin==false)
                        
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.DeferredLoadingEnabled = false;



                            if(IsQuickReDispatch)
                            {

                                db.stp_UpdateJobStatus(objBooking.Id, Enums.BOOKINGSTATUS.ONHOLD);


                            }


                            DateTime? DespatchDateTime = db.Bookings.Where(c => c.DriverId == ObjDriver.Id
                            && c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING && c.Id != objBooking.Id && c.DespatchDateTime != null)
                            .OrderByDescending(c => c.DespatchDateTime).Select(c=>c.DespatchDateTime).FirstOrDefault();

                            //Booking objPendingBooking = General.GetQueryable<Booking>(null).Where(c => c.DriverId == ObjDriver.Id
                            //&& c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING && c.Id != objBooking.Id && c.DespatchDateTime != null)
                            //.OrderByDescending(c => c.DespatchDateTime).FirstOrDefault();


                        if (DespatchDateTime != null )
                        {
                           
                                serverDateTime = db.ExecuteQuery<DateTime>("select getdate()").FirstOrDefault();
                            
                        }


                        if (DespatchDateTime != null  && serverDateTime != null && serverDateTime.Value.Subtract(DespatchDateTime.Value.AddSeconds(10)).TotalMinutes < (1))
                        {
                            ENUtils.ShowMessage("This Driver already have a Job Offer in Queue" + Environment.NewLine
                                               + "You cannot dispatch another job to this Driver until " +
                                              string.Format("{0:HH:mm:ss}", DespatchDateTime.Value.AddSeconds(10).AddMinutes(1)));

                            return;
                        }                                            
                        else
                        {
                          

                            string validateMsg = string.Empty;
                         //   DateTime? serverDateTime=null;
                            //using (TaxiDataContext db = new TaxiDataContext())
                            //{

                                if (db.Bookings.Count(c => c.Id == objBooking.Id && c.BookingStatusId == Enums.BOOKINGSTATUS.PENDING
                                                          && c.DriverId != ObjDriver.Id && c.DespatchDateTime != null) > 0)
                                {

                                    if (serverDateTime == null)
                                        serverDateTime = db.ExecuteQuery<DateTime>("select getdate()").FirstOrDefault();

                                    if (serverDateTime != null)
                                    {
                                        DateTime? DespatchDateTimeX = db.Bookings.Where(c => c.Id == objBooking.Id).Select(c=>c.DespatchDateTime).FirstOrDefault();
                                       // DateTime? DespatchDateTime= db.Bookings.FirstOrDefault(c => c.Id == objBooking.Id)

                                        if (DespatchDateTimeX != null && serverDateTime != null)
                                        {
                                            DateTime? newAvailableTime = DespatchDateTimeX.Value.AddMinutes(1).AddSeconds(5);


                                            if (serverDateTime < newAvailableTime)
                                            {

                                                //if (objBook.Booking_Logs.OrderByDescending(c => c.Id).FirstOrDefault(c => c.AfterUpdate.Contains("Auto Despatched")) != null)
                                                //{
                                                //    validateMsg = "Job is already AutoDespatched to another driver" + Environment.NewLine +
                                                //               "You cannot dispatch this job to other Driver before " + string.Format("{0:HH:mm:ss}", newAvailableTime);


                                                //}
                                                //else
                                                //{
                                                    validateMsg = "Job is already Despatched to another driver" + Environment.NewLine +
                                                             "You cannot dispatch this job to other Driver before " + string.Format("{0:HH:mm:ss}", newAvailableTime);


                                              //  }

                                            }

                                        }
                                    }
                                }
                                else
                                {


                                  
                                     if ((Application.OpenForms.OfType<Form>()
                                        .FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard)
                                        .CheckJobIfAlreadyAccepted(JobId, ObjDriver.Id))

                                    {

                                        validateMsg = "Job is already Accepted by this driver";
                                    }

                                    //if (db.GetTable<Booking>().Count(c => c.Id == JobId && c.DriverId != null && c.DriverId == ObjDriver.Id
                                    //          && (c.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || c.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                                    //          || c.BookingStatusId == Enums.BOOKINGSTATUS.POB || c.BookingStatusId == Enums.BOOKINGSTATUS.STC)) > 0)
                                    //{

                                    //    validateMsg = "Job is already Accepted by this driver";
                                    //}
                                    else
                                    {
                                        if (IsFojJob == false && db.GetTable<Booking>().Count(a => a.BookingStatusId == Enums.BOOKINGSTATUS.FOJ && a.DriverId != null && (a.DriverId == ObjDriver.Id) && (a.PickupDateTime > DateTime.Now.AddDays(-1))) > 0)
                                        {

                                            validateMsg = "Cannot Job Dispatch! "+Environment.NewLine+"This driver already have FOJ";
                                        }

                                    }
                                }



                           


                            if (validateMsg.Length > 0)
                            {
                                ENUtils.ShowMessage(validateMsg);
                                        return;
                            }





                                if (ReDespatchJob == false && (Application.OpenForms.OfType<Form>()
                                        .FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard)
                                        .CheckJobIfAlreadyAcceptedByOtherDriver(JobId, ObjDriver.Id))
                                {

                                    //if (ReDespatchJob==false && General.GetQueryable<Booking>(null).Count(c => c.Id == JobId && c.DriverId != ObjDriver.Id
                                    //    && (c.BookingStatusId == Enums.BOOKINGSTATUS.ONROUTE || c.BookingStatusId == Enums.BOOKINGSTATUS.ARRIVED
                                    //    || c.BookingStatusId == Enums.BOOKINGSTATUS.POB)) > 0)
                                    //{

                                    ENUtils.ShowMessage("This job is already despatched to another driver. " + Environment.NewLine + "Press Refresh Button to Update View");
                                return;


                            }
                             
                                 

                            this.IsFOJ = db.Fleet_DriverQueueLists.Where(c => c.Status == true && c.DriverId == ObjDriver.Id.ToIntorNull() &&
                                                                  (c.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.AVAILABLE
                                                                   && c.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.ONBREAK) && c.CurrentJobId != null
                                                                   ).Count() > 0;

                            //this.IsFOJ = (General.GetQueryable<Fleet_DriverQueueList>(c => c.Status == true && c.DriverId == ObjDriver.Id.ToIntorNull() &&
                            //                                      (c.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.AVAILABLE
                            //                                       && c.DriverWorkStatusId != Enums.Driver_WORKINGSTATUS.ONBREAK) 
                            //                                       ).Count() > 0);

                        }

                        }
                    }
                }
                catch (Exception ex)
                {


                }

            }


         //   string bookingVehType = objBooking.Fleet_VehicleType.VehicleType.ToLower().Trim();


            if (ObjDriver.VehicleTypeId != null)
            {

                if (objBooking.ExcludedDriverIds.ToStr().Trim().Length > 0 && objBooking.ExcludedDriverIds.ToStr().Trim().Contains(","+ObjDriver.Id+","))
                {

                    MessageBox.Show("Driver exist in Excluded List");
                    return;


                }


                if (objBooking.AttributeValues.ToStr().Trim().Length > 0)
                {

                    string[] bookingAttrs = objBooking.AttributeValues.ToStr().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string drvAttributes = ObjDriver.AttributeValues.ToStr();

                    int totalAttr = bookingAttrs.Count();
                    int matchCnt = 0;
                    string unmatchedAttrValue = string.Empty;
                    string[] drvAttrsArr = drvAttributes.ToStr().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var item in bookingAttrs)
                    {


                        if (drvAttrsArr.Count(c => c.ToLower() == item.ToLower()) > 0)
                        {
                            matchCnt++;

                        }
                        else
                        {

                            unmatchedAttrValue += item + ",";
                        }
                    }

                    if (matchCnt != totalAttr)
                    {

                        if (unmatchedAttrValue.EndsWith(","))
                        {
                            unmatchedAttrValue = unmatchedAttrValue.Substring(0, unmatchedAttrValue.LastIndexOf(","));

                        }

                        MessageBox.Show(("Driver : " + ObjDriver.DriverNo + " doesn't have attributes (" + unmatchedAttrValue + ")"), "Warning");
                        return;
                    }
                }

                if (AppVars.listUserRights.Count(c => c.functionId == "RESTRICT ON DESPATCH JOB TO INVALID VEHICLE DRIVER") > 0)
                {

                    string vehAttributes = objBooking.Fleet_VehicleType.DefaultIfEmpty().AttributeValues.ToStr().Trim();

                    if (vehAttributes.Length > 0)
                    {

                        bool MatchedAttr =false;
                        foreach (var item in vehAttributes.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (ObjDriver.VehicleTypeId.ToInt() == item.ToInt())
                            {
                                MatchedAttr=true;
                                break;

                            }                         
                            
                        }



                        if (MatchedAttr == false)
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
                            MessageBox.Show("This Job is for " + objBooking.Fleet_VehicleType.VehicleType.ToStr() + " Vehicle" + Environment.NewLine +
                                                                      "and Driver no " + ObjDriver.DriverNo + " have " + ObjDriver.Fleet_VehicleType.VehicleType + ".");


                            return;
                        }
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


        private System.Threading.Thread smsThread = null;

        public System.Threading.Thread SmsThread
        {
            get { return smsThread; }
            set { smsThread = value; }
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

                  

                    string customerMobileNo = txtCustomerMobNo.Text.Trim();
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


                    EuroSMS objSMS = new EuroSMS();
                    enablePDA = AppVars.objPolicyConfiguration.EnablePDA.ToBool();

                    string custNo = !string.IsNullOrEmpty(objBooking.CustomerMobileNo) ? objBooking.CustomerMobileNo : objBooking.CustomerPhoneNo;
              

                    
                    // Send To Driver

                    if (enablePDA == false || (enablePDA &&  (ObjDriver.HasPDA.ToBool() == false || IsManualLogin)))
                    {

                        if (objBooking.DisableDriverSMS.ToBool() == false )
                        {

                            objSMS.ToNumber = txtDriverMobNo.Text.Trim();
                            objSMS.Message = GetMessage(AppVars.objPolicyConfiguration.DespatchTextForDriver.ToStr());
                            IsSuccess2=true;


                            if (objSMS.Message.ToStr().Length > 0)
                            {


                                new Thread(delegate()
                                {
                                    try
                                    {
                                        objSMS.Send(ref smsError2);
                                    }
                                    catch
                                    {

                                    }
                                }).Start();
                            }
                            // IsSuccess2 = objSMS.Send(ref smsError2);

                        }
                        else
                            IsSuccess2 = true;


                        if (enablePDA)
                        {


                            if (ReDespatchJob && objBooking.DriverId != null && ObjDriver!=null && objBooking.DriverId.ToInt() != ObjDriver.Id)
                            {

                                new Thread(delegate()
                                {
                                    try
                                    {
                                        General.ReCallDespatchBooking(objBooking.Id, objBooking.DriverId.ToInt());
                                    }
                                    catch
                                    {


                                    }
                                }).Start();


                            }
                        }


                    }
                    else
                    {
                        IsSuccess2 = true;





                        string paymentType = objBooking.Gen_PaymentType.PaymentCategoryId==null? objBooking.Gen_PaymentType.DefaultIfEmpty().PaymentType.ToStr()
                                :objBooking.Gen_PaymentType.Gen_PaymentCategory.CategoryName.ToStr();

                                string strDeviceRegistrationId = ObjDriver.DeviceId.ToStr();                           
                                string journey = "O/W";

                                //if (objBooking.JourneyTypeId.ToInt() == 2)
                                //{
                                //    journey = "Return";

                                //}
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

                               // decimal drvPdaVersion = ObjDriver.Fleet_Driver_PDASettings.Count > 0 ? ObjDriver.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() : 9.40m;


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

                                //if (drvPdaVersion < 11 && objBooking.CompanyId != null && objBooking.Gen_Company.DefaultIfEmpty().AccountTypeId.ToInt() != Enums.ACCOUNT_TYPE.CASH)
                                //    companyName = objBooking.Gen_Company.DefaultIfEmpty().CompanyName;
                            //    else
                                    companyName = objBooking.Gen_Company.DefaultIfEmpty().CompanyName.ToStr();
                              

                                  //error in 13.4 => if its a plot job, then pickup point is hiding in pda.
                                //if (drvPdaVersion >9 && drvPdaVersion!=13.4m)
                                //{
                                    pickUpPlot = objBooking.ZoneId != null ? "<<<" + objBooking.Gen_Zone1.DefaultIfEmpty().ZoneName.ToStr() : "";
                                    dropOffPlot = objBooking.DropOffZoneId != null ? "<<<" + objBooking.Gen_Zone.DefaultIfEmpty().ZoneName.ToStr() : "";
                              //  }

                               
                               string FOJJob = string.Empty;

                               

                                if (this.IsFOJ)
                                    FOJJob = "foj";




                                string startJobPrefix = "JobId:";
                                //if (AppVars.objPolicyConfiguration.PDAJobAlertOnly.ToBool() &&  ObjDriver.Fleet_Driver_PDASettings[0].CurrentPdaVersion.ToDecimal() >= 8.3m && ObjDriver.Fleet_Driver_PDASettings[0].ShowJobAsAlert.ToBool())
                                //{
                                //    startJobPrefix = "AlertJobId:";                                   

                                //}

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


                                //try
                                //{
                                //    if (string.IsNullOrEmpty(General.GetPostCodeMatch(fromAddress.ToStr().Trim())) && fromAddress.ToStr().EndsWith(", UK") == false)
                                //    {
                                //        fromAddress = fromAddress.ToStr() + ", UK";

                                //    }

                                //    if (string.IsNullOrEmpty(General.GetPostCodeMatch(toAddress.ToStr().Trim())) && toAddress.ToStr().EndsWith(", UK") == false)
                                //    {
                                //        toAddress = toAddress.ToStr() + ", UK";

                                //    }
                                //}
                                //catch
                                //{

                                //}

                                 //half card and cash
                                string specialRequirements = objBooking.SpecialRequirements.ToStr();
                                if (objBooking.SecondaryPaymentTypeId != null && objBooking.CashFares.ToDecimal() > 0)
                                {

                                    specialRequirements += " , Additional Cash Payment : " + objBooking.CashFares.ToDecimal();
                                }

                                decimal pdafares =  objBooking.GetType().GetProperty(AppVars.objPolicyConfiguration.PDAFaresPropertyName.ToStr().Trim()).GetValue(objBooking, null).ToDecimal();

                              //  pdafares = objBooking.TotalCharges.ToDecimal();
                                
                                 string msg =string.Empty;

                                
                                     string showFaresValue = objBooking.Gen_PaymentType.ShowFaresOnPDA.ToStr().Trim();

                                   


                                     string showFares = ",\"ShowFares\":\"" + showFaresValue + "\"";
                                     string showSummary = ",\"ShowSummary\":\"" + showFaresValue + "\"";
                                  //   string showSummary = string.Empty;




                                     string agentDetails = string.Empty;
                                     string parkingandWaiting = string.Empty;
                                     if (objBooking.CompanyId != null)
                                     {


                                         //if (AppVars.objPolicyConfiguration.PickCommissionDeductionFromJobsTotal.ToBool())
                                         //{
                                             agentDetails = ",\"AgentFees\":\"" + String.Format("{0:0.00}", objBooking.AgentCommission + objBooking.ServiceCharges.ToDecimal()) + "\"";

                                            if (objBooking.PaymentTypeId.ToInt() == Enums.PAYMENT_TYPES.CASH)
                                            {
                                                pdafares = objBooking.FareRate.ToDecimal() + objBooking.ParkingCharges.ToDecimal() + objBooking.WaitingCharges.ToDecimal() + objBooking.AgentCommission.ToDecimal();


                                            }

                            //}
                            //else
                            //{
                            //    agentDetails = ",\"AgentFees\":\"" + String.Format("{0:0.00}", objBooking.AgentCommission) + "\"";

                            //}

                            parkingandWaiting = ",\"Parking\":\"" + string.Format("{0:0.00}", objBooking.ParkingCharges) + "\",\"Waiting\":\"" + String.Format("{0:0.00}", objBooking.WaitingCharges) + "\"";

                                     }
                                     else
                                     {

                                         //if (objBooking.ServiceCharges>0)
                                         //{
                                             agentDetails = ",\"AgentFees\":\"" + String.Format("{0:0.00}", objBooking.ServiceCharges.ToDecimal()) + "\"";

                                       //  }


                                         parkingandWaiting = ",\"Parking\":\"" + string.Format("{0:0.00}", objBooking.CongtionCharges) + "\",\"Waiting\":\"" + String.Format("{0:0.00}", objBooking.MeetAndGreetCharges) + "\"";
                                         //


                                     }





                                     string fromdoorno = objBooking.FromDoorNo.ToStr().Trim();
                                     if (fromdoorno.Length > 0 && fromdoorno.WordCount() > 2 && fromdoorno.Contains(" "))
                                     {
                                       
                                         try
                                         {

                                             fromdoorno = fromdoorno.Replace(" ", "-");
                                         }
                                         catch
                                         {


                                         }
                                     }


                                     //if (drvPdaVersion == 23.50m && fromAddress.ToStr().Trim().Contains("-"))
                                     //{
                                     //    fromAddress = fromAddress.Replace("-", "  ");

                                     //}


                            if (specialRequirements.ToStr().Contains("\""))
                                specialRequirements = specialRequirements.ToStr().Replace("\"", "-").Trim();

                            msg =FOJJob + startJobPrefix+ "{ \"JobId\" :\"" +JobId.ToStr()+
                                           "\", \"Pickup\":\"" + (!string.IsNullOrEmpty(objBooking.FromDoorNo) ? fromdoorno + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot) +
                                           "\", \"Destination\":\""+ (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + "-" + toAddress + dropOffPlot : toAddress + dropOffPlot) +"\","+
                                           "\"PickupDateTime\":\""+ string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.PickupDateTime) +"\"" +
                                           ",\"Cust\":\"" + objBooking.CustomerName + "\",\"Mob\":\"" + mobileNo + " " + "\",\"Fare\":\"" +string.Format("{0:0.00}",pdafares) + "\",\"Vehicle\":\"" + objBooking.Fleet_VehicleType.VehicleType + "\",\"Account\":\""+ companyName + " " +"\"" +
                                             ",\"Lug\":\"" + objBooking.NoofLuggages.ToInt() + "\",\"Passengers\":\"" + objBooking.NoofPassengers.ToInt() +  "\",\"Journey\":\"" + journey + "\",\"Payment\":\"" + paymentType + "\",\"Special\":\"" + specialRequirements + " " + "\",\"Extra\":\"" + IsExtra + "\",\"Via\":\"" + viaP + " " +"\"" +

                                             ",\"CompanyId\":\"" + objBooking.CompanyId.ToInt() + "\",\"SubCompanyId\":\"" + objBooking.SubcompanyId.ToInt() + "\",\"QuotedPrice\":\"" + (objBooking.IsQuotedPrice.ToBool()?"1":"0")  + "\"" +
                                        

                                             parkingandWaiting + ",\"DriverFares\":\"" + String.Format("{0:0.00}", (objBooking.FareRate-objBooking.ServiceCharges.ToDecimal())) + "\"" +
                                          agentDetails+
                                             ",\"Did\":\"" + ObjDriver.Id + "\",\"BabySeats\":\"" + objBooking.BabySeats.ToStr() + "\"" + showFares + showSummary + " }";
                                  
                                     

                                     //msg=  FOJJob + startJobPrefix + objBooking.Id +
                                     //   ":Pickup:" + (!string.IsNullOrEmpty(objBooking.FromDoorNo) ? objBooking.FromDoorNo + "-" + fromAddress + pickUpPlot : fromAddress + pickUpPlot) +

                                     //   ":Destination:" + (!string.IsNullOrEmpty(objBooking.ToDoorNo) ? objBooking.ToDoorNo + "-" + toAddress + dropOffPlot : toAddress + dropOffPlot) +
                                     //     ":PickupDateTime:" + string.Format("{0:dd/MM/yyyy   HH:mm}", objBooking.PickupDateTime) +
                                     //          ":Cust:" + objBooking.CustomerName + ":Mob:" + mobileNo + " " + ":Fare:" + pdafares
                                     //         + ":Vehicle:" + objBooking.Fleet_VehicleType.VehicleType + ":Account:" + companyName + " " +
                                     //         ":Lug:" + objBooking.NoofLuggages.ToInt() + ":Passengers:" + objBooking.NoofPassengers.ToInt() + ":Journey:" + journey +
                                     //         ":Payment:" + paymentType + ":Special:" + specialRequirements + " "
                                     //         + ":Extra:" + IsExtra + ":Via:" + viaP + " " + ":Did:" + ObjDriver.Id;


                                 
                               
                            

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

                                if (AppVars.objPolicyConfiguration.MapType.ToInt() == 1)
                                {
                                    // For TCP Connection
                                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                                    {

                                        int loopCnt = 1;


                                  
                                            new Thread(delegate()
                                            {
                                                try
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
                                                }
                                                catch
                                                {

                                                }
                                            }).Start();
                                     //   }
                                    }


                                }
                                else
                                {
                                    // For TCP Connection
                                    if (AppVars.objPolicyConfiguration.IsListenAll.ToBool())
                                    {
                                        new Thread(delegate()
                                  {
                                      General.SendMessageToPDA("request pda=" + JobId + "=" + ObjDriver.Id + "=" + msg + "=1=" + ObjDriver.DriverNo);
                                  }).Start();
                                    }

                                }
                           

                                if (chkSMS.Visible && chkSMS.Checked)
                                {

                                    if (AppVars.objPolicyConfiguration.SendPdaDespatchSmsOnAcceptJob.ToBool() == false)
                                    {

                                        objSMS.ToNumber = txtDriverMobNo.Text.Trim();
                                        objSMS.Message = GetMessage(AppVars.objPolicyConfiguration.DespatchTextForDriver.ToStr());

                                        if (IsFojJob)
                                        {
                                            objSMS.Message = "Follow On Job : " + Environment.NewLine + objSMS.Message;
                                        }


                                        new Thread(delegate()
                                        {
                                            objSMS.Send(ref smsError2);
                                        }).Start();
                                    }
                                    
                                }


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


                             
                                
                   
                   }             

            


                    // Send To Customer
                   // if (AppVars.objPolicyConfiguration.EnablePassengerText.ToBool() && objBooking.DisablePassengerSMS.ToBool() == false)
                    if (!ObjDriver.HasPDA.ToBool() && AppVars.objPolicyConfiguration.EnablePassengerText.ToBool() && objBooking.DisablePassengerSMS.ToBool() == false)
                    {

                        if (!string.IsNullOrEmpty(customerMobileNo))
                        {

                            if (objBooking.CompanyId == null || (objBooking.Gen_Company.DefaultIfEmpty().DisableCustomerText.ToBool() == false))
                            {

                                smsThread = new Thread(delegate()
                                    {
                                        IsSuccess1 = SendDespatchSMS(objSMS, GetMessage(AppVars.objPolicyConfiguration.DespatchTextForCustomer.ToStr()), customerMobileNo);
                                    });
                            }
                            else
                            {

                                IsSuccess1 = true;
                            }
                        }
                    }


                    if (IsSuccess1 && IsSuccess2)
                    {
                        rtn = true;
                    }

                    if (IsSuccess1 == false)
                    {
                        listofErrors.Add("[Customer] : " + smsError1);

                    }

                    if (IsSuccess2 == false)
                    {
                        listofErrors.Add("[Driver] : " + smsError2);
                    }


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
                                    //if (tag.TagPropertyValue.ToStr().Trim().ToLower()=="farerate"
                                    //    && messageTo.ToStr() == "customer" &&  objBooking.Gen_PaymentType.DefaultIfEmpty().ShowFaresOnSMS.ToBool()==false)
                                    //{

                                    //    propertyValue = "---";


                                    //}
                                    //else
                                    //{

                                        propertyValue = objBooking.GetType().GetProperty(tag.TagPropertyValue).GetValue(objBooking, null);
                                 //   }
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



                        case "Fleet_Driver_Image":

                            if (!string.IsNullOrEmpty(tag.TagPropertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {
                                if (ObjDriver.Fleet_Driver_Images.Count > 0)
                                {
                                    string linkId = ObjDriver.Fleet_Driver_Images[0].PhotoLinkId.ToStr();

                                    if (linkId.ToStr().Length == 0)
                                        propertyValue = " ";
                                    else
                                    {
                                        if (tag.TagMemberValue.ToStr().Trim().ToLower() == "<TrackDrv>")
                                        {
                                            string encrypt =Cryptography.Encrypt(objBooking.BookingNo.ToStr() + ":" + linkId+":"+AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim()+":"+objBooking.Id, "softeuroconnskey", true);
                                            propertyValue = Program.objLic.CabTrackUrl.ToStr() + "/tck.aspx?q=" + encrypt;

                                        }
                                        else
                                        {

                                            propertyValue = Program.objLic.CabTrackUrl.ToStr() + "/drv.aspx?ref=" + objBooking.BookingNo.ToStr() + ":" + linkId;
                                        }
                                    }
                                 
                                }
                                else
                                    propertyValue = " ";


                                //      propertyValue = objBooking.Fleet_Driver.DefaultIfEmpty().GetType().GetProperty(tag.TagPropertyValue2).GetValue(objBooking.Fleet_Driver.DefaultIfEmpty(), null);
                            }
                            break;


                        case "Fleet_Driver_Documents":

                            if (!string.IsNullOrEmpty(tag.TagPropertyValue.ToStr()) && !string.IsNullOrEmpty(tag.TagPropertyValue2))
                            {

                                if (tag.TagPropertyValue.Contains("PHC Vehicle"))
                                {
                                    propertyValue = ObjDriver.Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.PCOVehicle)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();


                                }
                                else if (tag.TagPropertyValue.Contains("PHC Driver"))
                                {
                                    propertyValue = ObjDriver.Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.PCODriver)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();


                                }
                                else if (tag.TagPropertyValue.Contains("License"))
                                {
                                    propertyValue = ObjDriver.Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.LICENSE)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();

                                }
                                else if (tag.TagPropertyValue.Contains("Insurance"))
                                {
                                    propertyValue = ObjDriver.Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.Insurance)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();

                                }
                                else if (tag.TagPropertyValue.Contains("MOT"))
                                {
                                    propertyValue = ObjDriver.Fleet_Driver_Documents.FirstOrDefault(c => c.DocumentId == Enums.DRIVER_DOCUMENTS.MOT)
                                                        .DefaultIfEmpty().BadgeNumber.ToStr();

                                }
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


                    if (this.IsAutoDespatchActivity || ReDespatchJob==true)
                    {
                        (new TaxiDataContext()).stp_DespatchedJobWithLogReason(this.JobId, ObjDriver.Id, ObjDriver.DriverNo.ToStr(), ObjDriver.HasPDA.ToBool(), enablePDA, false, IsAutoDespatchActivity, AppVars.LoginObj.LoginName.ToStr(), Enums.BOOKINGSTATUS.PENDING, false, OtherReason);
                
                    }
                    else
                    {


                        if (IsManualLogin)
                        {
                            (new TaxiDataContext()).stp_DespatchedGhostJob(this.JobId, ObjDriver.Id, ObjDriver.DriverNo.ToStr(), false, enablePDA, false, IsAutoDespatchActivity, AppVars.LoginObj.LoginName.ToStr(), Enums.BOOKINGSTATUS.PENDING, false, OtherReason, "");


                        }
                        else
                        {
                            (new TaxiDataContext()).stp_DespatchedGhostJob(this.JobId, ObjDriver.Id, ObjDriver.DriverNo.ToStr(), ObjDriver.HasPDA.ToBool(), enablePDA, false, IsAutoDespatchActivity, AppVars.LoginObj.LoginName.ToStr(), Enums.BOOKINGSTATUS.PENDING, false, OtherReason, "");


                        }

                    }
             

                    bool autoDespatch = objBooking.AutoDespatch.ToBool();
                    if ((!this.IsAutoDespatchActivity || !autoDespatch))
                    {

                        if (enablePDA == false || ObjDriver.HasPDA==false || IsManualLogin)
                        {
                            int? driverId = ObjDriver.Id.ToIntorNull();

                            long driverCurrent = 0;


                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                db.CommandTimeout = 4;

                                driverCurrent=db.Fleet_DriverQueueLists.Where(c => c.Status == true && c.DriverId == driverId).Select(c=>c.Id)
                                                                                   .OrderByDescending(c => c).FirstOrDefault();

                                if (driverCurrent != 0)
                                {
                                    if ((enablePDA == false && AppVars.objPolicyConfiguration.EnableOnBoardDrivers.ToBool() == true) || ( (ObjDriver.HasPDA.ToBool() == false || IsManualLogin) && enablePDA))
                                    {
                                        General.OnDespatchUpdateDriverQueue(driverCurrent, objBooking.Id, General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim()));

                                        RefreshBookingList();


                                    }
                                    else if (enablePDA == false)
                                    {
                                        General.UpdateDriverQueue(driverCurrent, objBooking.Id, General.GetPostCodeMatch(objBooking.ToAddress.ToStr().Trim()));
                                        RefreshBookingList();
                                    }
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


                            alert.CaptionText = "Job No : " + objBooking.BookingNo.ToStr() + " Dispatch Successfully";
                            alert.ContentText = "Driver : " + ObjDriver.DriverName;

                            alert.ContentText += Environment.NewLine + "Pickup Date-Time : "
                                                                    + string.Format("{0:dd/MM/yyyy hh:mm tt}", objBooking.PickupDateTime);

                            alert.Show();
                        }
                        
                    }

                    if (!this.IsAutoDespatchActivity)
                    {
                        //live
                        // General.SendMessageToPDA("request broadcast=" + RefreshTypes.REFRESH_ACTIVE_DASHBOARD);

                        //test


                        if (IsManualLogin)
                        {

                            General.SendMessageToPDA("request broadcast=" + RefreshTypes.REFRESH_MANUALLOGINDESPATCHJOB + ">>" + JobId + ">>" + ObjDriver.DriverNo.ToStr());
                        }
                        else
                        {
                            General.SendMessageToPDA("request broadcast=" + RefreshTypes.REFRESH_DESPATCHJOB + ">>" + JobId + ">>" + ObjDriver.DriverNo.ToStr() + ">>" + Enums.BOOKINGSTATUS.PENDING + ">>" + (ObjDriver.HasPDA.ToBool() ? "pda" : "nonpda"));
                        }
                       
                        
                    }


                    //if (Debugger.IsAttached == false)
                    //{
                        this.Close();
                  //  }
                }
                catch (Exception ex)
                {
                   
                    ENUtils.ShowMessage(ex.Message);


                    try
                    {
                        File.AppendAllText(Application.StartupPath + "\\exception_despatchbooking.txt", DateTime.Now + " " + ex.Message + Environment.NewLine);

                    }
                    catch (Exception ex2)
                    {

                    }

                }
            }
            else
            {

                lblNocMessage.Text = "Job Dispatch Failed..";
                lblNocMessage.ForeColor = Color.Red;
                lblNocMessage.Visible = true;

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
                    lblDriverMobNo.Visible = true;
                    txtDriverMobNo.Visible = true;
                    if (string.IsNullOrEmpty(driver.MobileNo.ToStr().Trim()))
                    {
                        txtDriverMobNo.Text = "Not found";
                        txtDriverMobNo.ForeColor = Color.Red;
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

                        txtDriverMobNo.Text = mobNo;
                        DateTime nowDate = DateTime.Now.ToDate();

                        // New Code for PDA Driver No
                        if (driver.HasPDA.ToBool() && AppVars.objPolicyConfiguration.EnablePdaDespatchSms.ToBool())
                        {
                            chkSMS.Visible = true;
                            chkSMS.Checked = true;
                        }
                        else
                        {

                            chkSMS.Visible = false;

                        }
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

                    //if (IsPDADriver)
                    //{

                    //    if (ObjDriver.Fleet_Driver_Locations.Count > 0)
                    //    {
                    //        DateTime updateDate = ObjDriver.Fleet_Driver_Locations[0].UpdateDate;

                    //        lblLastGPSConn.Text = "Last Connection made on " + string.Format("{0:dd-MMM}", updateDate) + " at " + string.Format("{0:HH:mm}", updateDate);
                    //    }
                    //    else
                    //    {
                    //        lblLastGPSConn.Visible = false;
                    //    }

                    //}

                   // lblLastGPSConn.Visible = IsPDADriver;
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



        private string DistanceMatrixServerKey = string.Empty;

        private double GetNearestDriverRadiusOnline(double? Latitude, double? Longitude, double? destLatitude, double? destLongitude)
        {
            double miles = 0.00;
            try
            {



                   
                        //if (string.IsNullOrEmpty(DistanceMatrixServerKey))
                        //{
                        //    using (TaxiDataContext db = new TaxiDataContext())
                        //    {

                        //        DistanceMatrixServerKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='distancematrixserverkey'").FirstOrDefault().ToStr().Trim();


                        //        if (DistanceMatrixServerKey.Length == 0)
                        //            DistanceMatrixServerKey = " ";
                        //        else
                        //            DistanceMatrixServerKey = "&key=" + DistanceMatrixServerKey;
                        //    }
                        //}


                        //if (DistanceMatrixServerKey.ToStr().Trim().Length > 0)
                        //{


                        //    using (System.Data.DataSet ds = new System.Data.DataSet())
                        //    {
                        //        ds.ReadXml(new XmlTextReader("https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + Latitude + "," + Longitude + "&destinations=" + destLatitude + "," + destLongitude + DistanceMatrixServerKey + "&units=imperial&mode=driving&sensor=false"));
                        //        DataTable dt = ds.Tables["distance"];

                        //        if (dt != null && dt.Rows.Count > 0)
                        //        {
                        //            miles = Convert.ToDouble(dt.Rows[0]["text"].ToStr().Replace(" mi", "").Trim());


                                   
                        //        }

                        //    }
                        //}



              //  decimal distance = 0.00m;
               // string Time = "";
              //  TimeSpan timeSpan;

              
                
                
              

                string APP_ID = System.Configuration.ConfigurationManager.AppSettings["MAP_APP_ID"] != null ? System.Configuration.ConfigurationManager.AppSettings["MAP_APP_ID"].ToStr() : "3AFVxo9lo4YV4NVnqgz1";
                string APP_CODE = System.Configuration.ConfigurationManager.AppSettings["MAP_APP_CODE"] != null ? System.Configuration.ConfigurationManager.AppSettings["MAP_APP_CODE"].ToStr() : "uCIGBo3LGjk4d02fxXGtvw";
                string IsUseHTTPS = System.Configuration.ConfigurationManager.AppSettings["MAP_IsUseHTTPS"] != null ? System.Configuration.ConfigurationManager.AppSettings["MAP_IsUseHTTPS"].ToStr() : bool.FalseString.ToLower();

                string MapHereURL = @"https://route.api.here.com/routing/7.2/calculateroute.xml?app_id={0}&app_code={1}&mode=shortest;car;&metricSystem=imperial{2}";

                int i = 0;
                string waypointTemp = "&waypoint{0}=geo!{1},{2}";
                string waypoint = string.Format(waypointTemp, i, Latitude, Longitude);


                i++;
                waypoint += string.Format(waypointTemp, i, destLatitude, destLongitude);

                MapHereURL = string.Format(MapHereURL, APP_ID, APP_CODE, waypoint);

                using (XmlTextReader reader = new XmlTextReader(MapHereURL))
                {
                
                    reader.WhitespaceHandling = WhitespaceHandling.Significant;
                    using (System.Data.DataSet ds = new System.Data.DataSet())
                    {
                        ds.ReadXml(reader);
                        if (ds.Tables["Summary"] != null)
                        {
                            miles = Convert.ToDouble(Math.Round(Convert.ToDecimal(ds.Tables["Summary"].Rows[0]["Distance"]) * 0.00062137119m, 1));
                           
                        }
                    }
                }

            //    Distance = "Distance : " + string.Format("{0:#.##}", distance) + " miles. Time :" + Time + "";

                return miles;


                  



               
            }
            catch (Exception ex)
            {
                //try
                //{
                //    File.AppendAllText(Application.StartupPath + "\\distancematrix.txt", DateTime.Now.ToStr() + ":" + ex.Message);


                //}
                //catch
                //{


                //}

            }

            return miles;
        }


      

       



        //private double GetNearestDriverRadiusOnlineGoogle(double? Latitude, double? Longitude, double? destLatitude, double? destLongitude)
        //{
        //    double miles = 0.00;
        //    try
        //    {




        //        if (string.IsNullOrEmpty(DistanceMatrixServerKey))
        //        {
        //            using (TaxiDataContext db = new TaxiDataContext())
        //            {

        //                DistanceMatrixServerKey = db.ExecuteQuery<string>("select APIKey from mapkeys where maptype='distancematrixserverkey'").FirstOrDefault().ToStr().Trim();


        //                if (DistanceMatrixServerKey.Length == 0)
        //                    DistanceMatrixServerKey = " ";
        //                else
        //                    DistanceMatrixServerKey = "&key=" + DistanceMatrixServerKey;
        //            }
        //        }


        //        if (DistanceMatrixServerKey.ToStr().Trim().Length > 0)
        //        {


        //            using (System.Data.DataSet ds = new System.Data.DataSet())
        //            {
        //                ds.ReadXml(new XmlTextReader("https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + Latitude + "," + Longitude + "&destinations=" + destLatitude + "," + destLongitude + DistanceMatrixServerKey + "&units=imperial&mode=driving&sensor=false"));
        //                DataTable dt = ds.Tables["distance"];

        //                if (dt != null && dt.Rows.Count > 0)
        //                {
        //                    miles = Convert.ToDouble(dt.Rows[0]["text"].ToStr().Replace(" mi", "").Trim());



        //                }

        //            }
        //        }




        //        return miles;







        //    }
        //    catch (Exception ex)
        //    {
        //        //try
        //        //{
        //        //    File.AppendAllText(Application.StartupPath + "\\distancematrix.txt", DateTime.Now.ToStr() + ":" + ex.Message);


        //        //}
        //        //catch
        //        //{


        //        //}

        //    }

        //    return miles;
        //}






    }
}
