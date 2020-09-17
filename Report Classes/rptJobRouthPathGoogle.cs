using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using Utils;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using Taxi_App;
using System.Xml;
using Taxi_BLL;
using System.IO;
using System.Drawing.Imaging;


namespace Taxi_AppMain
{
    public partial class rptJobRouthPathGoogle : Form
    {
        private Booking _ObjBooking;

        public Booking ObjBooking
        {
            get { return _ObjBooking; }
            set { _ObjBooking = value; }
        }

        public rptJobRouthPathGoogle()
        {
            InitializeComponent();
        }


        private int _DriverId;

        private bool IsTrackDriver;

      

        public rptJobRouthPathGoogle(Booking obj, bool TrackDriver)
        {
            InitializeComponent();
            this.ObjBooking = obj;
            IsTrackDriver = TrackDriver;

            if (TrackDriver && obj != null)
            {
                this._DriverId = obj.DriverId.ToInt();

            }

            this.FormClosing += new FormClosingEventHandler(rptJobRouthPath_FormClosing);
            this.Load += new EventHandler(rptJobRouthPath_Load);
        }

        public rptJobRouthPathGoogle(Booking obj, bool TrackDriver,int driverId)
        {
            InitializeComponent();
            this.ObjBooking = obj;
            IsTrackDriver = TrackDriver;
            this._DriverId = driverId;
            this.FormClosing += new FormClosingEventHandler(rptJobRouthPath_FormClosing);
            this.Load += new EventHandler(rptJobRouthPath_Load);
            this.Shown += new EventHandler(rptJobRouthPathGoogle_Shown);
        }

        void rptJobRouthPathGoogle_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }


    

        void rptJobRouthPath_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {

          

                if (worker != null)
                {
                    if (worker.IsBusy)
                    {
                        worker.CancelAsync();

                    }

                    worker.Dispose();


                }

                if (timer1 != null)
                {
                    timer1.Stop();
                }

                if (gMapControl1 != null)
                {
                    gMapControl1.Overlays.Clear();
                    gMapControl1.Dispose();
                   
                }

                if (IsTrackDriver == false)
                {
                    this.Dispose(true);
                }
            }
            catch (Exception ex)
            {


            }
        }


        delegate void UIParameterizedDelegate(List<PointLatLng> points);

        private void CreateJobRouteDirection()
        {
            if (ObjBooking == null)
                return;
            try
            {
             
            
                string origin =General.GetPostCodeMatch(ObjBooking.FromAddress);
                string destination = General.GetPostCodeMatch(ObjBooking.ToAddress);


                List<PointLatLng> points = new List<PointLatLng>();
              
             


                try
                {

                    if (string.IsNullOrEmpty(origin)  )
                    {

                        string baseAddress =General.GetPostCodeMatch( AppVars.objPolicyConfiguration.BaseAddress.ToStr().ToUpper());

                        if (!string.IsNullOrEmpty(baseAddress))
                        {

                            var objCoordinate = General.GetObject<Gen_Coordinate>(c => c.PostCode == baseAddress);

                            if (objCoordinate != null)
                            {
                                points.Add(new PointLatLng(Convert.ToDouble(objCoordinate.Latitude),Convert.ToDouble(objCoordinate.Longitude)));

                            }
                        
                        
                        }

                    }
                    else if(!string.IsNullOrEmpty(origin) )
                    {

                        string pickup =General.GetPostCodeMatch( ObjBooking.FromAddress.ToStr().ToUpper());

                        if (!string.IsNullOrEmpty(pickup))
                        {

                            var objCoordinate = General.GetObject<Gen_Coordinate>(c => c.PostCode == pickup);

                            if (objCoordinate != null)
                            {
                                points.Add(new PointLatLng(Convert.ToDouble(objCoordinate.Latitude), Convert.ToDouble(objCoordinate.Longitude)));
                            }
                            else
                            {

                                using (TaxiDataContext db = new TaxiDataContext())
                                {

                                    var objX = db.stp_getCoordinatesByAddress(pickup.ToStr().ToUpper(), pickup.ToStr().ToUpper()).FirstOrDefault();


                                    if (objX == null)
                                    {
                                        var obj = GetDistance.PostCodeToLongLat(pickup, "GB");


                                        if (obj != null)
                                        {

                                            points.Add(new PointLatLng(Convert.ToDouble(obj.Value.Latitude), Convert.ToDouble(obj.Value.Longitude)));


                                        }
                                    }
                                    else
                                    {
                                        points.Add(new PointLatLng(Convert.ToDouble(objX.Latitude), Convert.ToDouble(objX.Longtiude)));

                                    }

                                }


                              

                            }
                
                        }

                    }


                    if (string.IsNullOrEmpty(destination))
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {

                            var objCoord=  db.stp_getCoordinatesByAddress(ObjBooking.ToAddress.ToStr(), destination).FirstOrDefault(); ;


                            if (objCoord != null)
                            {
                                if (objCoord != null)
                                {
                                    points.Add(new PointLatLng(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longtiude)));

                                }

                            }
                        }                 

                    }


                    else if (!string.IsNullOrEmpty(destination))
                    {

                        var objCoordinate = General.GetObject<Gen_Coordinate>(c => c.PostCode == destination);

                        if (objCoordinate != null)
                        {
                            points.Add(new PointLatLng(Convert.ToDouble(objCoordinate.Latitude), Convert.ToDouble(objCoordinate.Longitude)));
                        }
                        else
                        {


                            using (TaxiDataContext db = new TaxiDataContext())
                            {

                                var objX = db.stp_getCoordinatesByAddress(destination.ToStr().ToUpper(), destination.ToStr().ToUpper()).FirstOrDefault();


                                if (objX == null)
                                {
                                    var obj = GetDistance.PostCodeToLongLat(destination, "GB");


                                    if (obj != null)
                                    {

                                        points.Add(new PointLatLng(Convert.ToDouble(obj.Value.Latitude), Convert.ToDouble(obj.Value.Longitude)));


                                    }
                                }
                                else
                                {
                                    points.Add(new PointLatLng(Convert.ToDouble(objX.Latitude), Convert.ToDouble(objX.Longtiude)));

                                }

                            }



                          

                        }

                    }
                   

                    
                }
                catch
                {
                  
                }

             


                if (points.Count > 0)
                {

                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke(new UIParameterizedDelegate(OnDisplayMap),points);


                    }
                    else
                    {
                        OnDisplayMap(points);


                    }                 


                   
                }

                
            }
            catch (Exception ex)
            {


            }

        }

        private void OnDisplayMap(List<PointLatLng> points)
        {
            try
            {
                

                if (ObjBooking!=null && points.Count > 0)
                {
                    bool showBookingMarkers = true;
                    GMapOverlay polyOverlay = new GMapOverlay(gMapControl1, "overlayJob");


                 
                    gMapControl1.MinZoom = 0;
                    gMapControl1.MaxZoom = 24;
                    gMapControl1.Zoom = 12;
                    gMapControl1.DragButton = MouseButtons.Left;




                    GMapMarkerCustom marker1 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(points[0].Lat), Convert.ToDouble(points[0].Lng)), Resources.Resource1.arrived);
                    marker1.ToolTipText = "PICKUP : " + Environment.NewLine + ObjBooking.FromAddress.ToStr();
                    marker1.ToolTipMode = MarkerTooltipMode.Always;
                    marker1.ToolTip.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    marker1.Tag = new PointLatLng(points[0].Lat, points[0].Lng);
                    marker1.Tag = "Pickup";


                    if (ObjBooking.DriverId==null || ObjBooking.DriverId != this._DriverId)
                    {
                        showBookingMarkers = false;
                    }

                    polyOverlay.Markers.Add(marker1);


                    if (points.Count > 1)
                    {
                        if (points.Count == 2)
                        {
                            gMapControl1.Position = new PointLatLng(points[1].Lat, points[1].Lng);
                        }
                        else
                            gMapControl1.Position = new PointLatLng(points[points.Count - 1].Lat, points[points.Count - 1].Lng);



                        if (points.Count > 2)
                        {
                            GMapRoute route = new GMapRoute(points, "routeJob");
                            route.Stroke = new Pen(Color.CornflowerBlue, 8);

                            polyOverlay.Routes.Add(route);
                        }


                        GMapMarkerCustom marker2 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(points[points.Count - 1].Lat), Convert.ToDouble(points[points.Count - 1].Lng)), Resources.Resource1.clear);
                        marker2.ToolTipText = "DESTINATION : " + Environment.NewLine + ObjBooking.ToAddress.ToStr();
                        marker2.ToolTipMode = MarkerTooltipMode.Always;
                        marker2.ToolTip.Font = new Font("Tahoma", 10, FontStyle.Bold);
                        marker2.Tag = "Destination";
                        polyOverlay.Markers.Add(marker2);



                        if (showBookingMarkers == false)
                        {

                            marker1.IsVisible = false;
                            marker2.IsVisible = false;
                            HideJobDetailsZoomPanel();
                        }


                    }
                    else
                        gMapControl1.Position = new PointLatLng(points[0].Lat, points[0].Lng);



                    gMapControl1.Overlays.Add(polyOverlay);




                }
                else
                {
                    

                    if (gMapControl1.Overlays[0].Markers.Count > 0)
                    {
                        gMapControl1.Position = new PointLatLng(gMapControl1.Overlays[0].Markers[0].Position.Lat, gMapControl1.Overlays[0].Markers[0].Position.Lng);

                        gMapControl1.MinZoom = 0;
                        gMapControl1.MaxZoom = 24;
                        gMapControl1.Zoom = 14;
                        gMapControl1.DragButton = MouseButtons.Left;

                        optPickup.Visible = false;
                        optDestination.Visible = false;
                        pnlZoom.Size = new Size(pnlZoom.Size.Width, 60);

                    }
                }
            }
            catch (Exception ex)
            {


            }

        }

        private void HideJobDetailsZoomPanel()
        {

            optPickup.Visible = false;
            optDestination.Visible = false;
            pnlZoom.Size = new Size(pnlZoom.Size.Width, 60);

        }


        delegate void UIVoidDelegate();
        GMapMarkerCustom markerDrv = null;
        private void GetDriverLocation()
        {
            try
            {

                if (IsClosed)
                    return;

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new UIVoidDelegate(OnDisplayDriverLocation));


                }
                else
                {
                    OnDisplayDriverLocation();


                }
            }
            catch (Exception ex)
            {


            }

        }

        int oddCnt = 1;
        string lastETA = string.Empty;
        double? LastSpeed = null;
        private void OnDisplayDriverLocation()
        {
            try
            {
                if (IsClosed)
                    return;

               


                if (this._DriverId == 0)
                    return;

             

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    var obj = db.Fleet_Driver_Locations.Where(c => c.DriverId == this._DriverId).Select(c=>new {c.DriverId,c.Speed,c.Latitude,c.Longitude,c.LocationName,c.EstimatedTimeLeft }).FirstOrDefault();


                    if (obj != null)
                    {
                        string locName = obj.LocationName;

                        if (markerDrv == null)
                        {
                            markerDrv = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(obj.Latitude), Convert.ToDouble(obj.Longitude)), null);


                            

                            if (gMapControl1.Overlays.Count == 0)
                            {
                                GMapOverlay polyOverlay = new GMapOverlay(gMapControl1, "overlayJob");
                                gMapControl1.Overlays.Add(polyOverlay);

                            }

                            gMapControl1.Overlays[0].Markers.Add(markerDrv);
                        }
                        else
                            markerDrv.Position = new PointLatLng(Convert.ToDouble(obj.Latitude), Convert.ToDouble(obj.Longitude));



                        markerDrv.Tag = "Driver";

                        string EstimatedTimeLeft = "";
                        if (AppVars.objPolicyConfiguration.ClientType == "new")
                        {

                            if (AppVars.etaKey.ToStr().Length == 0)
                            {
                                using (TaxiDataContext dbX = new TaxiDataContext())
                                {
                                    try
                                    {
                                        dbX.CommandTimeout = 5;
                                        AppVars.etaKey = db.ExecuteQuery<string>("select apikey from mapkeys where maptype='here'").FirstOrDefault();


                                        if (AppVars.etaKey.ToStr().Trim().Length == 0)
                                            AppVars.etaKey = " ";

                                        else
                                        {
                                            chkETA.Visible = true;


                                        }
                                    }
                                    catch
                                    {
                                        AppVars.etaKey = " ";

                                    }

                                }
                            }
                            else if (AppVars.etaKey.ToStr().Trim().Length > 0)
                            {

                                chkETA.Visible = true;
                            }

                            if (obj.Speed > 0 || (string.IsNullOrEmpty(markerDrv.PDALocationName)))
                            {

                                if (AppVars.objPolicyConfiguration.EnablePOI.ToBool() && obj.Latitude>0)
                                {

                                    locName = db.PostCodesNearLatLong(obj.Latitude, obj.Longitude).FirstOrDefault().DefaultIfEmpty().Street.ToStr();


                                }

                                if (locName.ToStr().Trim().Length == 0)
                                {

                                    if (AppVars.googleKey.ToStr().Length == 0)
                                    {

                                        using (TaxiDataContext dbX = new TaxiDataContext())
                                        {
                                            dbX.CommandTimeout = 5;
                                            AppVars.googleKey = "&key=" + db.ExecuteQuery<string>("select apikey from mapkeys where maptype='geocodinggoogle'").FirstOrDefault();


                                        }
                                    }

                                    if (AppVars.googleKey.ToStr().Trim().Length > 8)
                                    {
                                        locName = GetLocationName(obj.Latitude, obj.Longitude);
                                    }

                                }


                               
                                if (chkETA.Checked)
                                {

                                    if ((LastSpeed == null || LastSpeed != obj.Speed))
                                    {

                                        if (oddCnt % 2 == 1)
                                        {

                                            try
                                            {


                                                GetDistance.Coords obj1 = new GetDistance.Coords { Latitude = obj.Latitude, Longitude = obj.Longitude };

                                                if (markerDrv.WorkStatus.ToStr() == "On Route" || markerDrv.WorkStatus.ToStr() == "Arrived")
                                                {
                                                    GetDistance.Coords obj2 = new GetDistance.Coords { Latitude = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == "Pickup").Position.Lat, Longitude = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == "Pickup").Position.Lng };

                                                   EstimatedTimeLeft = GetDistance.GetLocationDetailsByMapHere(obj1, obj2, AppVars.etaKey, null);


                                                    if (EstimatedTimeLeft.ToStr().Trim().Length > 0)
                                                    {
                                                        EstimatedTimeLeft = "Pickup ETA : " + EstimatedTimeLeft;

                                                    }

                                                }
                                                else if (markerDrv.WorkStatus.ToStr() == "Passenger On Board" || markerDrv.WorkStatus.ToStr() == "Soon To Clear")
                                                {
                                                    if (gMapControl1.Overlays[0].Markers.Count > 2)
                                                    {
                                                        GetDistance.Coords obj2 = new GetDistance.Coords { Latitude = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == "Destination").Position.Lat, Longitude = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == "Destination").Position.Lng };
                                                        EstimatedTimeLeft = GetDistance.GetLocationDetailsByMapHere(obj1, obj2, AppVars.etaKey, null);



                                                        if (EstimatedTimeLeft.ToStr().Trim().Length > 0)
                                                        {
                                                            EstimatedTimeLeft = "DropOff ETA : " + EstimatedTimeLeft;

                                                        }
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            chkETA.Visible = false;
                                                        }
                                                        catch
                                                        {


                                                        }
                                                    }
                                                }

                                            }
                                            catch
                                            {


                                            }

                                            lastETA = EstimatedTimeLeft;
                                        }
                                        else
                                            EstimatedTimeLeft = lastETA;


                                        oddCnt++;
                                    }
                                }
                                else
                                {

                                    EstimatedTimeLeft = lastETA;
                                }



                            }
                            else
                            {
                                if (obj.Speed == 0)
                                {

                                    if (chkETA.Checked)
                                    {

                                        if ((LastSpeed == null || lastETA=="" || LastSpeed != obj.Speed))
                                        {

                                            if (oddCnt % 2 == 1)
                                            {

                                                try
                                                {

                                            


                                                    GetDistance.Coords obj1 = new GetDistance.Coords { Latitude = obj.Latitude, Longitude = obj.Longitude };

                                                    if (markerDrv.WorkStatus.ToStr() == "On Route" || markerDrv.WorkStatus.ToStr() == "Arrived")
                                                    {
                                                        GetDistance.Coords obj2 = new GetDistance.Coords { Latitude = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == "Pickup").Position.Lat, Longitude = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == "Pickup").Position.Lng };

                                                        EstimatedTimeLeft = GetDistance.GetLocationDetailsByMapHere(obj1, obj2, AppVars.etaKey, null);


                                                        if (EstimatedTimeLeft.ToStr().Trim().Length > 0)
                                                        {
                                                            EstimatedTimeLeft = "Pickup ETA : " + EstimatedTimeLeft;

                                                        }

                                                    }
                                                    else if (markerDrv.WorkStatus.ToStr() == "Passenger On Board" || markerDrv.WorkStatus.ToStr() == "Soon To Clear")
                                                    {
                                                        if (gMapControl1.Overlays[0].Markers.Count > 2)
                                                        {
                                                            GetDistance.Coords obj2 = new GetDistance.Coords { Latitude = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == "Destination").Position.Lat, Longitude = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == "Destination").Position.Lng };

                                                            EstimatedTimeLeft = GetDistance.GetLocationDetailsByMapHere(obj1, obj2, AppVars.etaKey, null);



                                                            if (EstimatedTimeLeft.ToStr().Trim().Length > 0)
                                                            {
                                                                EstimatedTimeLeft = "DropOff ETA : " + EstimatedTimeLeft;

                                                            }
                                                        }
                                                        else
                                                        {
                                                            try
                                                            {
                                                                chkETA.Visible = false;
                                                            }
                                                            catch
                                                            {


                                                            }
                                                        }
                                                    }

                                                }
                                                catch
                                                {


                                                }

                                                lastETA = EstimatedTimeLeft;
                                            }
                                            else
                                                EstimatedTimeLeft = lastETA;


                                            oddCnt++;
                                        }
                                        else
                                            EstimatedTimeLeft = lastETA;
                                    }

                                }


                            }
                        }

                        if (locName == "")
                        {
                            locName = markerDrv.PDALocationName.ToStr().Trim();


                        }


                    

                        if (locName != "" && (string.IsNullOrEmpty(markerDrv.PDALocationName) || markerDrv.PDALocationName.Equals(locName) == false))
                        {
                            markerDrv.IsVisible = false;
                            markerDrv.PDALocationName = locName;
                            markerDrv.IsVisible = true;
                        }


                        if (DriverNo.ToStr().Trim().Length == 0)
                        {


                            try
                            {
                                DriverNo = db.Fleet_Drivers.Where(c => c.Id == _DriverId).Select(c => c.DriverNo).FirstOrDefault();
                                DriverNo = "Driver : " + DriverNo.ToStr();
                            }
                            catch
                            {

                            }
                        }
                        markerDrv.ToolTipText = DriverNo + Environment.NewLine + locName.ToStr().ToUpper() + Environment.NewLine + "Speed: " + Math.Round(Convert.ToDouble(obj.Speed), 0) + "mph";


                        if (ObjBooking!=null && ObjBooking.DriverId!=null && ObjBooking.DriverId==this._DriverId && !string.IsNullOrEmpty(EstimatedTimeLeft))
                        {
                            markerDrv.ToolTipText += Environment.NewLine +  EstimatedTimeLeft;
                        }


                        LastSpeed = obj.Speed;

                        string imagePath = System.Windows.Forms.Application.StartupPath + "\\VehicleImages\\";
                        var queue = db.Fleet_DriverQueueLists.Where(c =>c.DriverId==this._DriverId && c.Status == true).Select(args=>new {args.DriverWorkStatusId }).FirstOrDefault();
                        // var queue=obj.Fleet_Driver.Fleet_DriverQueueLists.OrderByDescending(c => c.Status == true).FirstOrDefault();

                      

                        if (queue != null)
                        {

                            string status = "";

                            int StatusId = queue.DriverWorkStatusId.ToInt();

                           

                            if (StatusId == Enums.Driver_WORKINGSTATUS.AVAILABLE)
                            {
                                imagePath += "diamond_Available.png";  //+ item.workstatus.Strip(' ') + ".png"
                                status = "Available";
                            }

                            else if (StatusId == Enums.Driver_WORKINGSTATUS.ONROUTE)
                            {
                                imagePath += "diamond_OnRoute.png";  //+ item.workstatus.Strip(' ') + ".png"
                                status = "On Route";
                            }
                            else if (StatusId == Enums.Driver_WORKINGSTATUS.ONBREAK)
                            {
                                imagePath += "diamond_OnBreak.png";  //+ item.workstatus.Strip(' ') + ".png"
                                status = "OnBreak";
                            }


                            else if (StatusId == Enums.Driver_WORKINGSTATUS.ARRIVED)
                            {

                                imagePath += "diamond_Arrived.png";  //+ item.workstatus.Strip(' ') + ".png"
                                status = "Arrived";
                            }

                            else if (StatusId == Enums.Driver_WORKINGSTATUS.NOTAVAILABLE)
                            {

                                imagePath += "diamond_PassengerOnBoard.png";  //+ item.workstatus.Strip(' ') + ".png"
                                status = "Passenger On Board";
                            }
                            else if (StatusId == Enums.Driver_WORKINGSTATUS.SOONTOCLEAR)
                            {

                                imagePath += "diamond_SoonToClear.png";  //+ item.workstatus.Strip(' ') + ".png"
                                status = "Soon To Clear";
                            }


                            if (string.IsNullOrEmpty(markerDrv.WorkStatus) || markerDrv.WorkStatus.Equals(status) == false)
                            {
                                markerDrv.IsVisible = false;
                                markerDrv.WorkStatus = status.ToStr();
                                markerDrv.IsVisible = true;


                            }

                        }
                        
                        
                        if (File.Exists(imagePath))
                        {
                            markerDrv.MarkerImage = Image.FromFile(imagePath);
                        }

                        //   marker1.ToolTipText = "START at : " + string.Format("{0:HH:mm}", acceptDateTime);
                        markerDrv.ToolTipMode = MarkerTooltipMode.Always;
                        markerDrv.ToolTip.Font = new Font("Tahoma", 8, FontStyle.Bold);



                        if (optDriver.Checked && markerDrv.Position.Lat!=0)
                        {

                            gMapControl1.Position = new PointLatLng(markerDrv.Position.Lat, markerDrv.Position.Lng);

                        }

                        if (ObjBooking == null)
                        {
                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke(new UIParameterizedDelegate(OnDisplayMap), null);


                            }
                            else
                            {
                                OnDisplayMap(null);


                            }

                        }
                      


                    }
                }
            }
            catch (Exception ex)
            {



            }

         
        }

        private string DriverNo = "";


        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (IsClosed)
                    return;


                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke(new UIVoidDelegate(RunUIThread));


                        }
                        else
                        {
                            RunUIThread();


                        }


            }
            catch (Exception ex)
            {

                


            }
        }


        private void RunUIThread()
        {

            try
            {
                gMapControl1.MapProvider = GMapProviders.GoogleMap;

                if (IsTrackDriver)
                {


                    CreateJobRouteDirection();

                    GetDriverLocation();


                    if (timer1 == null)
                    {

                        timer1 = new Timer();
                        timer1.Interval = 6000;
                        timer1.Tick += new EventHandler(timer1_Tick);
                        timer1.Start();

                    }
                }
                else
                {


                    try
                    {

                        DrawPolyVertices(7, 1, this.ObjBooking.Booking_RoutePaths);
                    }
                    catch (Exception ex)
                    {

                        if (ex.Message.ToLower().Contains("cross-thread"))
                        {
                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke(new DrawUI(DrawPolyVertices), 7, 1, this.ObjBooking.Booking_RoutePaths);


                            }
                            else
                            {
                                DrawPolyVertices(7, 1, this.ObjBooking.Booking_RoutePaths);


                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {


            }
        }



        private bool IsClosed = false;
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IsClosed)
                    return;




                gMapControl1.Enabled = true;

                this.Size = new Size(this.Size.Width + 10, this.Size.Height + 10);


                if (IsAttached == false && IsTrackDriver)
                {
                    IsAttached = true;
                    optDriver.CheckedChanged += new EventHandler(optDriver_CheckedChanged);
                    optPickup.CheckedChanged += new EventHandler(optPickup_CheckedChanged);
                    optDestination.CheckedChanged += new EventHandler(optDestination_CheckedChanged);
                    ZoomTo("Driver");
                }

                //txtDistance.Text = distance.ToStr();
                //txtError.Text = string.Empty;
                //btnCheckDistance.Text = "Calculate Distance";
                //btnCheckDistance.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }




        Timer timer1 = null;

        BackgroundWorker worker = null;
        private bool IsAttached = false;
        void rptJobRouthPath_Load(object sender, EventArgs e)
        {
           
            try
            {

                if (this.ObjBooking != null)
                {


                    if (IsTrackDriver)
                    {

                        if (worker == null)
                        {
                            worker = new BackgroundWorker();
                            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                            worker.WorkerSupportsCancellation = true;
                        }

                        gMapControl1.Enabled = false;
                        grdLister.Visible = false;
                        worker.RunWorkerAsync();


                        //optDriver.CheckedChanged += new EventHandler(optDriver_CheckedChanged);
                        //optPickup.CheckedChanged += new EventHandler(optPickup_CheckedChanged);
                        //optDestination.CheckedChanged += new EventHandler(optDestination_CheckedChanged);
                    }
                    else
                    {

                        pnlRouteActions.Visible = true;

                        int len = ObjBooking.FromAddress.ToStr().Length > ObjBooking.ToAddress.ToStr().Length ? ObjBooking.FromAddress.ToStr().Length
                                     : ObjBooking.ToAddress.ToStr().Length;

                        grdLister.AllowEditRow = false;
                        grdLister.AllowAddNewRow = false;

                        if (len >= 20 && len <= 30)
                        {
                            grdLister.Size = new Size(grdLister.Width, grdLister.Height - 28);
                        }
                        else if (len < 20)
                        {
                            grdLister.Size = new Size(grdLister.Width, grdLister.Height - 38);
                        }
                        else if (len > 60)
                        {
                            grdLister.Size = new Size(grdLister.Width, grdLister.Height + 25);
                        }

                        var row = grdLister.Rows.AddNew();
                        row.Cells["Drv"].Value = ObjBooking.Fleet_Driver.DefaultIfEmpty().DriverNo;
                        row.Cells["Pickup"].Value = ObjBooking.FromAddress.ToStr();
                        row.Cells["Destination"].Value = ObjBooking.ToAddress.ToStr();
                        row.Cells["Accepted"].Value = string.Format("{0:dd/MM HH:mm}", ObjBooking.AcceptedDateTime);
                        row.Cells["Arrived"].Value = string.Format("{0:dd/MM HH:mm}", ObjBooking.ArrivalDateTime);
                        row.Cells["POB"].Value = string.Format("{0:dd/MM HH:mm}", ObjBooking.POBDateTime);
                        row.Cells["STC"].Value = string.Format("{0:dd/MM HH:mm}", ObjBooking.STCDateTime);
                        row.Cells["Cleared"].Value = string.Format("{0:dd/MM HH:mm}", ObjBooking.ClearedDateTime);
                        row.Cells["Miles"].Value = ObjBooking.TotalTravelledMiles;


                        if (worker == null)
                        {
                            worker = new BackgroundWorker();
                            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                            worker.WorkerSupportsCancellation = true;
                        }



                        worker.RunWorkerAsync();

                    }
                }
                else
                {

                    if (IsTrackDriver)
                    {
                        this.Text = "Track Driver";

                        if (worker == null)
                        {
                            worker = new BackgroundWorker();
                            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                            worker.WorkerSupportsCancellation = true;
                        }

                        gMapControl1.Enabled = false;
                        grdLister.Visible = false;
                        worker.RunWorkerAsync();


                    }
                }

            }
            catch (Exception ex)
            {
               

            }
        }

        void optDestination_CheckedChanged(object sender, EventArgs e)
        {
            if (optDestination.Checked)
            {

                ZoomTo("Destination");
            }
        }

        void optPickup_CheckedChanged(object sender, EventArgs e)
        {
            if (optPickup.Checked)
            {

                ZoomTo("Pickup");
            }

        }

        void optDriver_CheckedChanged(object sender, EventArgs e)
        {
            if (optDriver.Checked)
            {

                ZoomTo("Driver");
            }
        }

        private void ZoomTo(string option)
        {
            try
            {
                var obj = gMapControl1.Overlays[0].Markers.FirstOrDefault(c => c.Tag.ToStr() == option);

                if (obj != null)
                {
                    if (obj.Position != null && obj.Position.Lat != 0)
                    {

                        gMapControl1.Position = obj.Position;
                        gMapControl1.Zoom = 15;
                    }
                }

            }
            catch (Exception ex)
            {


            }

        }

        private void ZoomToPickup()
        {


        }


        private void ZoomToDestination()
        {



        }

        void timer1_Tick(object sender, EventArgs e)
        {
            GetDriverLocation();
        }



        delegate void DrawUI(int lineWeight, int lineForeColor, IList<Booking_RoutePath> list);

        private void DrawPolyVertices(int lineWeight, int lineForeColor, IList<Booking_RoutePath> listOfLocations)
        {
            try
            {
                pnlZoom.Visible = false;

                gMapControl1.MarkersEnabled = true;
              
                List<PointLatLng> points = new List<PointLatLng>();               
                GMapOverlay polyOverlay = new GMapOverlay(gMapControl1, "overlay1");            


                points = listOfLocations
                       .Select(args => new PointLatLng
                       {
                           Lat = Convert.ToDouble(args.Latitude),
                           Lng = Convert.ToDouble(args.Longitude)

                       }).ToList();
                 

                    if (points.Count > 0)
                    {

                        gMapControl1.Position = new PointLatLng(points[0].Lat, points[0].Lng);

                        gMapControl1.MinZoom = 0;
                        gMapControl1.MaxZoom = 24;
                        gMapControl1.Zoom = 13;
                        gMapControl1.DragButton = MouseButtons.Left;
                    }

                GMapRoute route=new GMapRoute(points,"route1");
                  route.Stroke = new Pen(Color.DodgerBlue, 7);
                 

                polyOverlay.Routes.Add(route);
                  
                gMapControl1.Overlays.Add(polyOverlay);               


                DateTime? acceptDateTime=this.ObjBooking.AcceptedDateTime;
                DateTime? arriveDateTime=this.ObjBooking.ArrivalDateTime;
                DateTime? pobDateTime=this.ObjBooking.POBDateTime;
                DateTime? stcDateTime=this.ObjBooking.STCDateTime;
                DateTime? clearedDateTime=this.ObjBooking.ClearedDateTime;

                Booking_RoutePath objRoute = null;

               
                if(acceptDateTime!=null)
                {
                   objRoute= listOfLocations.FirstOrDefault(c=>c.UpdateDate==acceptDateTime || c.UpdateDate.Value.AddSeconds(30)>acceptDateTime);

                   if (objRoute != null)
                    {
                   

                        GMapMarkerCustom marker1 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(objRoute.Latitude), Convert.ToDouble(objRoute.Longitude)), null);
                        marker1.ToolTipText = "START at : " + string.Format("{0:HH:mm}", acceptDateTime);
                        marker1.ToolTipMode = MarkerTooltipMode.Always;
                        marker1.ToolTip.Font = new Font("Tahoma", 6, FontStyle.Bold);
                        polyOverlay.Markers.Add(marker1);
                    }                                    

                }

                if (arriveDateTime != null)
                {
                    objRoute = listOfLocations.FirstOrDefault(c => c.UpdateDate == arriveDateTime || c.UpdateDate.Value.AddSeconds(30) > arriveDateTime);

                    if (objRoute != null)
                    {
                     
                        GMapMarkerCustom marker1 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(objRoute.Latitude), Convert.ToDouble(objRoute.Longitude)),  Resources.Resource1.arrived);
                        marker1.ToolTipText = "ARRIVED at : " + string.Format("{0:HH:mm}", arriveDateTime);
                        marker1.ToolTipMode = MarkerTooltipMode.Always;
                        marker1.ToolTip.Font = new Font("Tahoma", 6, FontStyle.Bold);
                        polyOverlay.Markers.Add(marker1);
                    }

                }


                if (pobDateTime != null)
                {
                    objRoute = listOfLocations.FirstOrDefault(c => c.UpdateDate == pobDateTime || c.UpdateDate.Value.AddSeconds(30) > pobDateTime);

                    if (objRoute != null)
                    {
                      


                        GMapMarkerCustom marker1 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(objRoute.Latitude), Convert.ToDouble(objRoute.Longitude)), Resources.Resource1.pob);
                        marker1.ToolTipText = "POB at : " + string.Format("{0:HH:mm}", pobDateTime);
                        marker1.ToolTipMode = MarkerTooltipMode.Always;
                        marker1.ToolTip.Font = new Font("Tahoma", 6, FontStyle.Bold);
                        polyOverlay.Markers.Add(marker1);
                    }

                }


                if (stcDateTime != null)
                {
                    objRoute = listOfLocations.FirstOrDefault(c => c.UpdateDate == stcDateTime || c.UpdateDate.Value.AddSeconds(30) > stcDateTime);

                    if (objRoute != null)
                    {
                        
                        GMapMarkerCustom marker1 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(objRoute.Latitude), Convert.ToDouble(objRoute.Longitude)), Resources.Resource1.stc);
                        marker1.ToolTipText = "STC at : " + string.Format("{0:HH:mm}", stcDateTime);
                        marker1.ToolTipMode = MarkerTooltipMode.Always;
                        marker1.ToolTip.Font = new Font("Tahoma", 6, FontStyle.Bold);
                        polyOverlay.Markers.Add(marker1);
                    }

                }


       

                if (clearedDateTime != null)
                {
                    objRoute = listOfLocations.LastOrDefault();

                    if (objRoute != null)
                    {
                       

                        GMapMarkerCustom marker1 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(objRoute.Latitude), Convert.ToDouble(objRoute.Longitude)), Resources.Resource1.clear);
                        marker1.ToolTipText = "FINISH at : " + string.Format("{0:HH:mm}", clearedDateTime);
                        marker1.ToolTipMode = MarkerTooltipMode.Always;
                        marker1.ToolTip.Font = new Font("Tahoma", 6, FontStyle.Bold);
                        polyOverlay.Markers.Add(marker1);
                    }

                }              


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //ENUtils.ShowMessage(ex.Message);

            }
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            SendEmail();
        }


        private void SendEmail()
        {
            try
            {
                string bookingNo = ObjBooking.BookingNo.ToStr().Replace("/","").Replace("-","");


                string path = Application.StartupPath + "\\" + "MapReportRef" + bookingNo + ".jpg";

                if (File.Exists(path))
                    File.Delete(path);

                System.Drawing.Point sp = gMapControl1.Location;
                System.Drawing.Size ds = gMapControl1.Size;
                System.Drawing.Rectangle sr = new System.Drawing.Rectangle(sp, ds);
                //Convert the Image to a JPG
                Image tmpImage;
                tmpImage = CaptureImage(sp, System.Drawing.Point.Empty, sr, "");
                tmpImage.Save(path);


                frmEmail frm = new frmEmail(tmpImage, path);
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();


                if (File.Exists(path))
                    File.Delete(path);
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);

            }

            //General.ShowEmailForm(reportViewer1, "Account Invoice # " + invoiceNo, email);

        }

        SaveFileDialog sfd = new SaveFileDialog();


        private void SaveImage()
        {

            

        }

        public Image CaptureImage(System.Drawing.Point SourcePoint, System.Drawing.Point DestinationPoint, System.Drawing.Rectangle SelectionRectangle, string FilePath)
        {
            Image tmpImage;
            using (Bitmap bitmap = new Bitmap(SelectionRectangle.Width, SelectionRectangle.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(SourcePoint, DestinationPoint, SelectionRectangle.Size);
                }
                //Convert the Image to a JPG
                MemoryStream ms = new MemoryStream();
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                tmpImage = Image.FromStream(ms);
                return tmpImage;
            }
        }
    
        public bool stop = false;
        public bool pause = false;
        int startFrom = 0;
        GMapOverlay polyOverlay = null;
        GMapMarkerCustom marker14 = null;



        public void Navigation()
        {
            try
            {

                polyOverlay = gMapControl1.Overlays[0];
                //List<PointLatLng> points = new List<PointLatLng>();
                //polyOverlay = new GMapOverlay(gMapControl1, "overlay1");
                //IList<Booking_RoutePath> listOfLocations = this.ObjBooking.Booking_RoutePaths;

                //points = listOfLocations
                //       .Select(args => new PointLatLng
                //       {
                //           Lat = Convert.ToDouble(args.Latitude),
                //           Lng = Convert.ToDouble(args.Longitude)

                //       }).ToList();

                //GMapRoute route = new GMapRoute(points, "route1");
                //route.Stroke = new Pen(Color.DodgerBlue, 7);


                //polyOverlay.Routes.Add(route);

                //gMapControl1.Overlays.Add(polyOverlay);


                DateTime? acceptDateTime = this.ObjBooking.AcceptedDateTime;
                DateTime? arriveDateTime = this.ObjBooking.ArrivalDateTime;
                DateTime? pobDateTime = this.ObjBooking.POBDateTime;
                DateTime? stcDateTime = this.ObjBooking.STCDateTime;
                DateTime? clearedDateTime = this.ObjBooking.ClearedDateTime;

                var bmp = new Bitmap(Taxi_AppMain.Properties.Resources.diamond_OnRoute);
                //  string imagepath = global::Taxi_AppMain.Properties.Resources.VoiceRequest5.ToStr();
                var query = General.GetQueryable<Booking_RoutePath>(c => c.BookingId == ObjBooking.Id).ToList();
                var driverno =  ObjBooking.Fleet_Driver.DriverNo;
               // var driverno = General.GetObject<Fleet_Driver>(c=>c.booking

                Font f = new Font("Tahoma", 6, FontStyle.Bold);



                bool isTrue = false;
                for (int i = startFrom; i < query.Count(); i++)
                {
                    if (pause)
                    {

                        startFrom = i;


                        if(marker14==null)
                             marker14 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(query[i].Latitude), Convert.ToDouble(query[i].Longitude)), null);
                        else
                            marker14.Position=new PointLatLng(Convert.ToDouble(query[i].Latitude), Convert.ToDouble(query[i].Longitude)); 
                        
                        marker14.MarkerImage = bmp;
                        marker14.ToolTipText = "Driver " + driverno;
                        marker14.ToolTipText += Environment.NewLine + "Time : " + string.Format("{0:HH:mm:ss}", query[i].UpdateDate);

                        marker14.ToolTipMode = MarkerTooltipMode.Always;
                        marker14.ToolTip.Font = f;

                        if (polyOverlay.Markers.Count == 0)
                            polyOverlay.Markers.Add(marker14);
                        
                        break;

                    }
                    else
                    {
                        if (stop == true)
                        {
                     

                         //   polyOverlay.Markers.Remove(marker14);
                          //  marker14 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(query[0].Latitude), Convert.ToDouble(query[0].Longitude)), null);

                            if (marker14 == null)
                                marker14 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(query[i].Latitude), Convert.ToDouble(query[i].Longitude)), null);
                            else
                            {
                                marker14.Position = new PointLatLng(Convert.ToDouble(query[0].Latitude), Convert.ToDouble(query[0].Longitude));

                              //  marker14.Position.Lat = marker14.P(Convert.ToDouble(query[0].Latitude));

                            }
                            
                            
                            marker14.MarkerImage =  Taxi_AppMain.Properties.Resources.diamond_OnRoute;
                            marker14.ToolTipText = "Driver " + driverno;
                            marker14.ToolTipText += Environment.NewLine + "Time : " + string.Format("{0:HH:mm:ss}", query[i].UpdateDate);

                            marker14.ToolTipMode = MarkerTooltipMode.Always;
                            marker14.ToolTip.Font = f;
                           
                            if(polyOverlay.Markers.Count==0)
                              polyOverlay.Markers.Add(marker14);


                            this.gMapControl1.BeginInvoke((MethodInvoker)delegate()
                            {
                                gMapControl1.Position = new PointLatLng(Convert.ToDouble(query[0].Latitude), Convert.ToDouble(query[0].Longitude));
                                if (ObjBooking.TotalTravelledMiles <= 5)
                                {
                                    gMapControl1.MinZoom = 0;
                                    gMapControl1.MaxZoom = 24;
                                    gMapControl1.Zoom = 15;
                                }
                                else
                                {
                                    gMapControl1.MinZoom = 0;
                                    gMapControl1.MaxZoom = 24;
                                    gMapControl1.Zoom = 13;
                                }

                            });
                      


                            break;

                        }
                        if (stop == false)
                        {

                            isTrue = false;

                            //marker14 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(query[i].Latitude), Convert.ToDouble(query[i].Longitude)), null);

                            if (marker14 == null)
                            {
                                marker14 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(query[i].Latitude), Convert.ToDouble(query[i].Longitude)), null);
                                polyOverlay.Markers.Add(marker14);
                            }
                            else
                                marker14.Position = new PointLatLng(Convert.ToDouble(query[i].Latitude), Convert.ToDouble(query[i].Longitude)); 
                       
                            
                            
                            if(marker14.MarkerImage==null)
                              marker14.MarkerImage = bmp;

                            marker14.ToolTipText ="Driver "+ driverno;
                            marker14.ToolTipText += Environment.NewLine + "Time : " + string.Format("{0:HH:mm:ss}", query[i].UpdateDate);

                            marker14.ToolTipMode = MarkerTooltipMode.Always;
                            marker14.ToolTip.Font = f;



                            if (clearedDateTime != null)
                            {
                                if (i == query.Count || query[i].UpdateDate.Value >= clearedDateTime.Value || query[i].UpdateDate.Value.AddSeconds(15) > clearedDateTime.Value)
                                {

                                      isTrue = true;
                                    //  polyOverlay.Markers.Remove(marker14);
                                    //    bmp = new Bitmap();
                                    marker14.MarkerImage = Taxi_AppMain.Properties.Resources.diamond;
                                }
                            }


                            if (isTrue==false && stcDateTime != null)
                            {

                                if (query[i].UpdateDate.Value >= stcDateTime.Value || query[i].UpdateDate.Value.AddSeconds(30) > stcDateTime.Value)
                                {
                                         isTrue = true;
                                    //   polyOverlay.Markers.Remove(marker14);
                                    //  bmp = new Bitmap();
                                    marker14.MarkerImage = Taxi_AppMain.Properties.Resources.diamond_SoonToClear;
                                    //    polyOverlay.Markers.Add(marker14);
                                }

                            }

                            if (isTrue == false && pobDateTime != null)
                            {
                                if (query[i].UpdateDate.Value >= pobDateTime.Value || query[i].UpdateDate.Value.AddSeconds(30) > pobDateTime.Value)
                                {
                                       isTrue = true;
                                    //   polyOverlay.Markers.Remove(marker14);
                                    //  bmp = new Bitmap();
                                    marker14.MarkerImage = Taxi_AppMain.Properties.Resources.diamond_PassengerOnBoard;
                                    //    polyOverlay.Markers.Add(marker14);
                                }

                            }


                            if (isTrue==false && arriveDateTime != null)
                            {
                                if (query[i].UpdateDate.Value >= arriveDateTime.Value || query[i].UpdateDate.Value.AddSeconds(10) > arriveDateTime.Value)
                                {
                                      isTrue = true;
                                    //  polyOverlay.Markers.Remove(marker14);
                                    //   bmp = new Bitmap();
                                    marker14.MarkerImage = Taxi_AppMain.Properties.Resources.diamond_Arrived;
                                    //  polyOverlay.Markers.Add(marker14);
                                }

                            }

                            if (isTrue==false && acceptDateTime != null)
                            {
                                //objRoute = listOfLocations.FirstOrDefault(c => c.UpdateDate == acceptDateTime || c.UpdateDate.Value.AddSeconds(30) > acceptDateTime);

                                if (acceptDateTime!=null)
                                {
                                    if (query[i].UpdateDate.Value >= acceptDateTime.Value || query[i].UpdateDate.Value.AddSeconds(30) > acceptDateTime.Value)
                                    {
                                        isTrue = true;
                                    //    isTrue = true;
                                     //   polyOverlay.Markers.Remove(marker14);
                                        marker14.MarkerImage = bmp;
                                     //   polyOverlay.Markers.Add(marker14);
                                    }
                                }

                            }



                            this.gMapControl1.BeginInvoke((MethodInvoker)delegate() { gMapControl1.Position = new PointLatLng(Convert.ToDouble(query[i].Latitude), Convert.ToDouble(query[i].Longitude));
                            if (ObjBooking.TotalTravelledMiles <= 5)
                            {
                                gMapControl1.MinZoom = 0;
                                gMapControl1.MaxZoom = 24;
                                gMapControl1.Zoom = 15;
                            }
                            else
                            {
                                gMapControl1.MinZoom = 0;
                                gMapControl1.MaxZoom = 24;
                                gMapControl1.Zoom = 13;
                            }
                            
                            });
                      


                          //  polyOverlay.Markers.Remove(marker14);

                            if(polyOverlay.Markers.Count==0)
                               polyOverlay.Markers.Add(marker14);
                            if (ObjBooking.TotalTravelledMiles <= 5)
                            {
                                System.Threading.Thread.Sleep(100);
                            }
                            else
                            {
                                System.Threading.Thread.Sleep(50);
                            
                            }

                         //   polyOverlay.Markers.Remove(marker14);
                          
                        }


                        GC.Collect();
                    }


                }
                
                if (btnPlayNav.Enabled == false && pause == false)
                {
                    this.btnPlayNav.BeginInvoke((MethodInvoker)delegate() { btnPlayNav.Enabled = true; });
                }
                if (btnRecordingPlay.Enabled == false)
                {
                    
                    
                    //this.btnPauseNav.BeginInvoke((MethodInvoker)delegate() { btnPauseNav.Enabled = true; btnPlayNav.Enabled = true; btnStopNav.Enabled = true; btnRecordingPlay.Enabled = true; timer2.Stop();
                    //int port = Int32.Parse(AppVars.objSubCompany.SmtpPort);

                    //string host = AppVars.objSubCompany.SmtpHost.ToStr() + "," + AppVars.objSubCompany.SmtpPort.ToInt() + "," + AppVars.objSubCompany.SmtpUserName.ToStr();// objLic.HostName.ToStr() + " , " + objLic.SmtpPort.ToStr() + " , " + objLic.EnableSSL.ToBool();
                    //string username = AppVars.objSubCompany.SmtpPassword.ToStr();
                    //System.Diagnostics.Process p = new System.Diagnostics.Process();
                    //p.StartInfo.FileName = Application.StartupPath + "\\MapReportRecorder.exe";
                    //p.StartInfo.Arguments = host + "," + username;
                    //p.Start();
                    
                    //this.MaximizeBox = true;
                    //this.MinimizeBox = true;
                    //this.Close();
                    //});



                    if (IsEmailRecording)
                    {


                        this.btnPauseNav.BeginInvoke((MethodInvoker)delegate()
                        {
                            btnPauseNav.Enabled = true; btnPlayNav.Enabled = true; btnStopNav.Enabled = true; btnRecordingPlay.Enabled = true; timer2.Stop();
                            int port = Int32.Parse(AppVars.objSubCompany.SmtpPort);

                            string host = AppVars.objSubCompany.SmtpHost.ToStr() + "," + AppVars.objSubCompany.SmtpPort.ToInt() + "," + AppVars.objSubCompany.SmtpUserName.ToStr();// objLic.HostName.ToStr() + " , " + objLic.SmtpPort.ToStr() + " , " + objLic.EnableSSL.ToBool();
                            string username = AppVars.objSubCompany.SmtpPassword.ToStr();
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            p.StartInfo.FileName = Application.StartupPath + "\\MapReportRecorder.exe";
                            p.StartInfo.Arguments = host + "," + username + "," + AppVars.objSubCompany.SmtpHasSSL.ToBool().ToStr();
                            p.Start();

                            this.MaximizeBox = true;
                            this.MinimizeBox = true;
                            this.Close();
                        });

                    }
                    
                }

            }
            catch
            { 
            
            
            }
            //                   query[i].Latitude

        }

        bool IsEmailRecording;

        private void btnPlayNav_Click(object sender, EventArgs e)
        {
            try
            {
                IsEmailRecording = false;

                btnRecordingPlay.Enabled = false;
                btnPlayNav.Enabled = false;
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Navigation));
                if (th.ThreadState != System.Threading.ThreadState.Running)
                {
                    stop = false;
                    pause = false;
                    startFrom = 0;
                 

                    if (btnPauseNav.Text == "Resume")
                    {
                        btnPauseNav.Text = "Pause";
                        pause = false;

                    }


                    //  th.IsBackground = true;
                    th.Start();
                } //  th.IsBackground = true;
                btnPlayNav.Enabled = false;
            }
            catch (Exception ex)
            { }
                    
        }

        private void btnPauseNav_Click(object sender, EventArgs e)
        {
            try
            {
                if (pause == false)
                {
                    btnPauseNav.Text = "Resume";
                    pause = true;
                }
                else if (pause == true)
                {
                  //  polyOverlay.Markers.Remove(marker14);
                    btnPauseNav.Text = "Pause";
                    pause = false;
                    System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Navigation));

                    th.Start();

                }
            }
            catch (Exception ex)
            { }
        }

        private void btnStopNav_Click(object sender, EventArgs e)
        {
            try
            {
                btnRecordingPlay.Enabled = true;
                stop = true;
            
                    var query = General.GetObject<Booking_RoutePath>(c => c.BookingId == ObjBooking.Id);

                    if (query != null)
                    {

                        if (marker14 == null)
                            marker14 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(query.Latitude), Convert.ToDouble(query.Longitude)), null);
                        else
                            marker14.Position = new PointLatLng(Convert.ToDouble(query.Latitude), Convert.ToDouble(query.Longitude));


                        marker14.MarkerImage = Taxi_AppMain.Properties.Resources.diamond_OnRoute;
                        marker14.ToolTipText = "Driver " + ObjBooking.Fleet_Driver.DriverNo; 
                        marker14.ToolTipText += Environment.NewLine + "Time : " + string.Format("{0:HH:mm:ss}", query.UpdateDate);

                        marker14.ToolTipMode = MarkerTooltipMode.Always;
                        marker14.ToolTip.Font = new Font("Tahoma", 6, FontStyle.Bold);

                        btnPauseNav.Text = "Pause";
                        pause = false;


                        this.gMapControl1.BeginInvoke((MethodInvoker)delegate()
                        {
                            gMapControl1.Position = new PointLatLng(Convert.ToDouble(query.Latitude), Convert.ToDouble(query.Longitude));
                            if (ObjBooking.TotalTravelledMiles <= 5)
                            {
                                gMapControl1.MinZoom = 0;
                                gMapControl1.MaxZoom = 24;
                                gMapControl1.Zoom = 15;
                            }
                            else
                            {
                                gMapControl1.MinZoom = 0;
                                gMapControl1.MaxZoom = 24;
                                gMapControl1.Zoom = 13;
                            }

                        });
                    }         
              
                
                if (btnPlayNav.Enabled == false)
                {
                    btnPlayNav.Enabled = true;
                   
                    GC.Collect();
                    
                }
            }
            catch (Exception ex)
            {
            
            }
        }

        private void btnRecordingPlay_Click(object sender, EventArgs e)
        {
            try
            {
                

                stop = false;
                if (ObjBooking.TotalTravelledMiles <= 5)
                {
                   
                    timer2.Start();
                }
                if (ObjBooking.TotalTravelledMiles > 5 && ObjBooking.TotalTravelledMiles <= 15)
                {
                    timer2.Interval = 330;
                    timer2.Start();
                }
                if(ObjBooking.TotalTravelledMiles >15)
                {
                    timer2.Interval = 530;
                    timer2.Start();
                }
                btnPauseNav.Enabled = false;
                btnPlayNav.Enabled = false;
                btnStopNav.Enabled = false;
            
                btnRecordingPlay.Enabled = false;

                System.IO.DirectoryInfo di = new DirectoryInfo(paths);

                if (Directory.Exists(paths) == false)
                {
                    Directory.CreateDirectory(paths);
                }
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                IsEmailRecording = true;

                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Navigation));
                if (th.IsAlive == false)
                {
                    //  th.IsBackground = true;
                    th.Start();
                }
                this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
               
                this.MaximizeBox = false;
                this.MinimizeBox = false;


               
            }
            catch (Exception ex)
            { }
        }

        int i = 0;
        
        
        Bitmap bmp;
        Graphics gr;     
        public static void SaveJpeg(string path, Image img, int quality, string value)
        {
            try
            {
                if (quality < 0 || quality > 100)
                    throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

                // Encoder parameter for image quality 
                EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                // JPEG image codec 
                ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;

                img.Save(path + value, jpegCodec, encoderParams);
            }
            catch (Exception ex)
            { }
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            try
            {
                // Get image codecs for all image formats 
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

                // Find the correct image codec 
                for (int i = 0; i < codecs.Length; i++)
                    if (codecs[i].MimeType == mimeType)
                        return codecs[i];

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        string paths = Application.StartupPath + "\\ImagesOfVideo\\ImagesArray\\"; // @"E:\saveimage\New folder\"; 
        private void timer2_Tick_1(object sender, EventArgs e)
        {
            try
            {
               
                bmp = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                gr = Graphics.FromImage(bmp);
                gr.CopyFromScreen(0, 0, 0, 0, new Size(bmp.Width, bmp.Height));

                i++;


                SaveJpeg(paths, bmp, 40, "mygif" + i + " .png");
               
                //  bmp.Save("mygif" + i +" .gif", ImageFormat.Jpeg);
            }
            catch (Exception ex)
            { }

        }

        private void rptJobRouthPathGoogle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {

                Close();
            }
        }

        public string GetLocationName(double? latitude, double? longitude)
        {
            string locationName = string.Empty;
            try
            {
                // Starts Google Geocoding Webservice

                string url2 = string.Empty;
                DataTable dt = null;
                XmlTextReader reader = null;
                System.Data.DataSet ds = null;


                if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "4-star")
                {
                    try
                    {

                        url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyA_OU-prdptvQWov4lkbUCXeB5ATuXByzk";

                        reader = new XmlTextReader(url2);
                        reader.WhitespaceHandling = WhitespaceHandling.Significant;
                        ds = new System.Data.DataSet();
                        ds.ReadXml(reader);

                        dt = ds.Tables["result"];

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                            if (row != null)
                            {
                                locationName = row[1].ToStr().Trim();
                            }
                        }

                        ds.Dispose();
                    }
                    catch
                    {

                    }


                    try
                    {
                        if (string.IsNullOrEmpty(locationName.Trim()))
                        {

                            url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyA_otaK2_m8hixryiig5GCWHMohdOERgZ4";

                            reader = new XmlTextReader(url2);
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;
                            ds = new System.Data.DataSet();
                            ds.ReadXml(reader);

                            dt = ds.Tables["result"];

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                                if (row != null)
                                {
                                    locationName = row[1].ToStr().Trim();
                                }
                            }

                            ds.Dispose();
                        }
                    }
                    catch
                    {


                    }


                    try
                    {
                        if (string.IsNullOrEmpty(locationName.Trim()))
                        {

                            url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyCDmuKvTJXS-w-oh2xCc8VAfgvfG86gMXY";

                            reader = new XmlTextReader(url2);
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;
                            ds = new System.Data.DataSet();
                            ds.ReadXml(reader);

                            dt = ds.Tables["result"];

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                                if (row != null)
                                {
                                    locationName = row[1].ToStr().Trim();
                                }
                            }

                            ds.Dispose();
                        }
                    }
                    catch
                    {


                    }



                }
                else if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "cA$tle_A$$oc!Ate$_wm_l!m!ted")
                {
                    try
                    {

                        url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyD3Mr8oT3nCQsv_mlr1SQ28Sjo_cO0Y0eM";

                        reader = new XmlTextReader(url2);
                        reader.WhitespaceHandling = WhitespaceHandling.Significant;
                        ds = new System.Data.DataSet();
                        ds.ReadXml(reader);

                        dt = ds.Tables["result"];

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                            if (row != null)
                            {
                                locationName = row[1].ToStr().Trim();
                            }
                        }

                        ds.Dispose();
                    }
                    catch
                    {

                    }


                    try
                    {
                        if (string.IsNullOrEmpty(locationName.Trim()))
                        {

                            url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyDgzIbskFzGfbCBEgZH3vQFTsku3kvz7hw";

                            reader = new XmlTextReader(url2);
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;
                            ds = new System.Data.DataSet();
                            ds.ReadXml(reader);

                            dt = ds.Tables["result"];

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                                if (row != null)
                                {
                                    locationName = row[1].ToStr().Trim();
                                }
                            }

                            ds.Dispose();
                        }
                    }
                    catch
                    {


                    }


                    try
                    {
                        if (string.IsNullOrEmpty(locationName.Trim()))
                        {

                            url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyCyUbbFmKw6VdyEY5JECheAKO_bbQRXlUg";

                            reader = new XmlTextReader(url2);
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;
                            ds = new System.Data.DataSet();
                            ds.ReadXml(reader);

                            dt = ds.Tables["result"];

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                                if (row != null)
                                {
                                    locationName = row[1].ToStr().Trim();
                                }
                            }

                            ds.Dispose();
                        }
                    }
                    catch
                    {


                    }



                }

                else if (AppVars.objPolicyConfiguration.DefaultClientId.ToStr() == "Prn-cb")
                {
                    try
                    {

                        url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyAJNEi3iOct1LBsFC6m-zFpFwxPapF10bQ";

                        reader = new XmlTextReader(url2);
                        reader.WhitespaceHandling = WhitespaceHandling.Significant;
                        ds = new System.Data.DataSet();
                        ds.ReadXml(reader);

                        dt = ds.Tables["result"];

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                            if (row != null)
                            {
                                locationName = row[1].ToStr().Trim();
                            }
                        }

                        ds.Dispose();
                    }
                    catch
                    {

                    }


                    try
                    {
                        if (string.IsNullOrEmpty(locationName.Trim()))
                        {

                            url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyAn2zwqsWHc0qoHEayw_j57Xa52Osk-cmA";

                            reader = new XmlTextReader(url2);
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;
                            ds = new System.Data.DataSet();
                            ds.ReadXml(reader);

                            dt = ds.Tables["result"];

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                                if (row != null)
                                {
                                    locationName = row[1].ToStr().Trim();
                                }
                            }

                            ds.Dispose();
                        }
                    }
                    catch
                    {


                    }


                    try
                    {
                        if (string.IsNullOrEmpty(locationName.Trim()))
                        {

                            url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=AIzaSyBqCNXb64ZmIDAzE7WFGcFGjIiC9zdKQP0";

                            reader = new XmlTextReader(url2);
                            reader.WhitespaceHandling = WhitespaceHandling.Significant;
                            ds = new System.Data.DataSet();
                            ds.ReadXml(reader);

                            dt = ds.Tables["result"];

                            if (dt != null && dt.Rows.Count > 0)
                            {

                                DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                                if (row != null)
                                {
                                    locationName = row[1].ToStr().Trim();
                                }
                            }

                            ds.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {


                    }



                }
                else
                {

                    try
                    {





                        url2 = "https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + AppVars.googleKey + "&sensor=false";

                        reader = new XmlTextReader(url2);
                        reader.WhitespaceHandling = WhitespaceHandling.Significant;
                        ds = new System.Data.DataSet();
                        ds.ReadXml(reader);

                        dt = ds.Tables["result"];

                        if (dt != null && dt.Rows.Count > 0)
                        {

                            DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                            if (row != null)
                            {
                                locationName = row[1].ToStr().Trim();
                            }
                        }

                        ds.Dispose();



                        //if (locationName.ToStr().Trim().Length == 0)
                        //{
                        //    try
                        //    {

                        //        File.AppendAllText(System.Windows.Forms.Application.StartupPath + "\\" + "exception_google.txt", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":" + url2 + Environment.NewLine);
                        //    }
                        //    catch
                        //    {


                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        try
                        {

                            File.AppendAllText(System.Windows.Forms.Application.StartupPath + "\\" + "exception_google.txt", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ":" + ex.Message + Environment.NewLine);
                        }
                        catch
                        {


                        }
                    }
                }



                // Starts Bing WebService
                //if (string.IsNullOrEmpty(locationName))
                //{
                //    try
                //    {
                //        if (bingkey.Length > 0)
                //        {

                //            url2 = "http://dev.virtualearth.net/REST/v1/Locations/" + latitude + "," + longitude + "?o=xml&key=At9uWeg3Sk_C611VLD0cc6i9oYu7IioNxvjNUN6-blcjKIX_L2n5G2ObOgEVlNZ_";


                //            reader = new XmlTextReader(url2);
                //            reader.WhitespaceHandling = WhitespaceHandling.Significant;
                //            ds = new System.Data.DataSet();
                //            ds.ReadXml(reader);
                //            dt = ds.Tables["Address"];

                //            if (dt != null)
                //            {

                //                DataRow row = dt.Rows.OfType<DataRow>().FirstOrDefault();
                //                if (row != null)
                //                {
                //                    locationName = row["FormattedAddress"].ToString().Trim();
                //                }
                //            }

                //            ds.Dispose();
                //        }
                //    }
                //    catch
                //    {
                //        locationName = string.Empty;
                //    }
                //}






                // Starts openstreetmap Geocoding Webservice

                //if (string.IsNullOrEmpty(locationName.Trim()))
                //{
                //    try
                //    {
                //        if (openstreetkey.Length > 0)
                //        {


                //            url2 = "http://nominatim.openstreetmap.org/reverse?lat=" + latitude + "&lon=" + longitude;

                //            reader = new XmlTextReader(url2);

                //            ds = new System.Data.DataSet();
                //            ds.ReadXml(reader);
                //            DataTable table = ds.Tables["addressparts"];

                //            if (table != null)
                //            {

                //                DataRow row = table.Rows.OfType<DataRow>().FirstOrDefault();
                //                if (row != null)
                //                {
                //                    if (table.Columns.Contains("road") && table.Columns.Contains("village")
                //                            && table.Columns.Contains("county") && table.Columns.Contains("postcode"))
                //                    {
                //                        locationName = row["road"].ToStr() + ", " + row["village"].ToStr() + ", " + row["county"].ToStr() + ", " + row["postcode"].ToStr();
                //                    }
                //                    else if (table.Columns.Contains("road") && table.Columns.Contains("city")
                //                            && table.Columns.Contains("county") && table.Columns.Contains("postcode"))
                //                    {
                //                        locationName = row["road"].ToStr() + ", " + row["city"].ToStr() + ", " + row["county"].ToStr() + ", " + row["postcode"].ToStr();
                //                    }
                //                    else if (table.Columns.Contains("road") && table.Columns.Contains("town")
                //                            && table.Columns.Contains("county") && table.Columns.Contains("postcode"))
                //                    {
                //                        locationName = row["road"].ToStr() + ", " + row["town"].ToStr() + ", " + row["county"].ToStr() + ", " + row["postcode"].ToStr();
                //                    }
                //                    else if (table.Columns.Contains("road") && table.Columns.Contains("city")
                //                            && table.Columns.Contains("county") && table.Columns.Contains("country"))
                //                    {
                //                        locationName = row["road"].ToStr() + ", " + row["city"].ToStr() + ", " + row["county"].ToStr() + ", " + row["country"].ToStr();
                //                    }
                //                    else if (table.Columns.Contains("city") && table.Columns.Contains("county")
                //                            && table.Columns.Contains("postcode") && table.Columns.Contains("country"))
                //                    {
                //                        locationName = row["city"].ToStr() + ", " + row["county"].ToStr() + ", " + row["postcode"].ToStr() + ", " + row["country"].ToStr();
                //                    }
                //                }
                //            }
                //            ds.Dispose();
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        locationName = string.Empty;

                //    }
                //}





            }
            catch (Exception ex)
            {


            }


            return locationName;
        }

        private void chkETA_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {

                GetDriverLocation();
            }
        }



      



        //int port = Int32.Parse(AppVars.objSubCompany.SmtpPort);

        //    string host = AppVars.objSubCompany.SmtpHost.ToStr() + "," + AppVars.objSubCompany.SmtpPort.ToInt()+ "," + AppVars.objSubCompany.SmtpUserName.ToStr();// objLic.HostName.ToStr() + " , " + objLic.SmtpPort.ToStr() + " , " + objLic.EnableSSL.ToBool();
        //    string username =  AppVars.objSubCompany.SmtpPassword.ToStr();
        //    System.Diagnostics.Process p = new System.Diagnostics.Process();
        //    p.StartInfo.FileName = Application.StartupPath + "\\WindowsSendFile.exe";
        //    p.StartInfo.Arguments = host + "," + username;         
        //    p.Start();
      
      

         


       

    }
}
