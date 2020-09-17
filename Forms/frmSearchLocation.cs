using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using Utils;
using GMap.NET.WindowsForms;
using GMap.NET;
using Taxi_Model;
using Taxi_App;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using Taxi_BLL;
using DotNetCoords;

namespace Taxi_AppMain
{
    public partial class frmSearchLocation : Form
    {

        private Taxi_AppMain.PlaceSearchResponse SearchPlaces;

        public string LocationName { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public bool IsPick { get; set; }



        public double DefaultLat;
        public double DefaultLng;


        public bool OpenFromSearch;

        Cursor EditCursor = null;
        public frmSearchLocation()
        {
        
            InitializeComponent();
            this.Load += new EventHandler(frmLocationLatLong_Load);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnPick.Click += new EventHandler(btnPick_Click);
            btnSelectLocation.Click += new EventHandler(btnSelectLocation_Click);
            this.btnClose.Click += new EventHandler(btnClose_Click);
            this.txtAddress.KeyDown += new KeyEventHandler(txtAddress_KeyDown);
            this.gMapControl1.MouseClick += new MouseEventHandler(gMapControl1_MouseClick);
            this.KeyDown += new KeyEventHandler(frmSearchLocation_KeyDown);
            EditCursor = new Cursor(Resources.Resource1.pushpin_PassengerOnBoard.GetHicon()); ;

            gMapControl1.DoubleClick += new EventHandler(gMapControl1_DoubleClick);


            ContextMenu cm = new ContextMenu();
            MenuItem item = new MenuItem();
            item.Text = "Edit Location";
            item.Click += new EventHandler(item_Click);
            cm.MenuItems.Add(item);
            gMapControl1.ContextMenu = cm;




        }

        void btnSelectLocation_Click(object sender, EventArgs e)
        {

            SelectLocation();


        }

        private void SelectLocation()
        {
            if (Lat == 0 && gMapControl1.Overlays.Count > 0 && gMapControl1.Overlays[0].Markers.Count > 0)
            {
                Lat = (float)gMapControl1.Overlays[0].Markers[0].Position.Lat;
                Lng = (float)gMapControl1.Overlays[0].Markers[0].Position.Lng;

            }


            if (Lat != 0)
            {
                IsPick = true;

                string locationAddress = txtAddress.Text.ToUpper();
                string locationName = locationAddress;

                if (dgvLocations.Visible == true && dgvLocations.Rows.Count > 0 && dgvLocations.SelectedRows.Count > 0)
                {
                    var result = SearchPlaces.Result[dgvLocations.SelectedRows[0].Index];

                    locationAddress = result.Formatted_address.ToStr().ToUpper();
                    locationName = result.Name.ToStr().ToUpper();


                }

                locationName = locationName.Replace("'", "").Trim().Replace("  ", " ").Trim();
                locationName = locationName.Replace(",", "").Trim().Replace("  ", " ").Trim();
                locationName = locationName.Replace("&", "AND").Trim().Replace("  ", " ").Trim();
                locationName = locationName.Replace("/", "-").Trim().Replace("  ", " ").Trim();
                if (locationAddress.ToStr().Trim().Length > 0)
                {
                    locationAddress = locationAddress.Replace("'", "").Trim().Replace("  ", " ").Trim();
                    locationAddress = locationAddress.Replace(",", "").Trim().Replace("  ", " ").Trim();
                    locationAddress = locationAddress.Replace("&", "AND").Trim().Replace("  ", " ").Trim();
                    locationAddress = locationAddress.Replace("/", "-").Trim().Replace("  ", " ").Trim();
                }

                Taxi_BLL.LocationBO objMaster = new Taxi_BLL.LocationBO();
                try
                {




                    int locId = 0;
                    using (TaxiDataContext db = new TaxiDataContext())
                    {

                        locId = db.Gen_Locations.FirstOrDefault(c => c.LocationName == locationName || c.Address == locationAddress).DefaultIfEmpty().Id;

                    }

                    if (locId == 0)
                    {
                        objMaster.New();


                    }
                    else
                    {
                        objMaster.GetByPrimaryKey(locId);
                        objMaster.Edit();

                    }

                    if (locationName.ToStr() != locationAddress.ToStr())
                        SelectedLocation = (locationName + " " + locationAddress).Trim();
                    else
                        SelectedLocation = locationName.ToStr().Trim();



                    objMaster.Current.LocationName = locationName;
                   


                    if (locationAddress.ToStr().Length == 0)
                        locationAddress = locationName;

                    objMaster.Current.Address = locationAddress.ToStr().Trim();
                    objMaster.Current.PostCode = General.GetPostCodeMatch(locationAddress).ToStr().ToUpper().Trim();



                    if (objMaster.Current.Id == 0 || objMaster.Current.LocationTypeId != Enums.LOCATION_TYPES.AIRPORT)
                    {
                        objMaster.Current.LocationTypeId = Enums.LOCATION_TYPES.ADDRESS;
                        objMaster.Current.CustomShortKey = false;
                        objMaster.Current.ShortCutKey = "";

                    }

                    if (objMaster.Current.LocationName.ToStr().Contains(objMaster.Current.PostCode) && objMaster.Current.PostCode.ToStr().Trim().Length>0)
                    {


                        objMaster.Current.LocationName = objMaster.Current.LocationName.Replace(objMaster.Current.PostCode, "").Trim();


                    }
                   
                    objMaster.Current.Latitude = Convert.ToDouble(Lat);
                    objMaster.Current.Longitude = Convert.ToDouble(Lng);


                    if (objMaster.Current.Latitude > 0 && objMaster.Current.ZoneId == null)
                    {
                        try
                        {

                            var plot = (from a in General.GetQueryable<Gen_Zone>(c => (c.ShapeType != null && c.ShapeType == "circle") || (c.MinLatitude != null && (objMaster.Current.Latitude >= c.MinLatitude && objMaster.Current.Latitude <= c.MaxLatitude)
                                                                              && (objMaster.Current.Longitude <= c.MaxLongitude && objMaster.Current.Longitude >= c.MinLongitude)))
                                        orderby a.PlotKind

                                        select a.Id).ToArray<int>();

                            if (plot.Count() > 0)
                            {
                                using (TaxiDataContext DB = new TaxiDataContext())
                                {
                                    foreach (var item in plot)
                                    {

                                        if (FindPoint(Convert.ToDouble(objMaster.Current.Latitude), Convert.ToDouble(objMaster.Current.Longitude), DB.Gen_Zone_PolyVertices.Where(c => c.ZoneId == item).ToList()))
                                        {
                                            objMaster.Current.ZoneId = item;
                                            break;
                                        }
                                    }
                                }





                            }
                        }
                        catch
                        {


                        }


                    }


                    objMaster.IsManualLocation = true;
                    objMaster.Save();


                   // WriteLog("success");
                }
                catch (Exception ex)
                {
                    WriteLog((objMaster.Errors.Count  >0 ? objMaster.ShowErrors():ex.Message));

                }

            }
           

            this.Close();

        }

        private void WriteLog(string msg)
        {
            try
            {
                File.AppendAllText(Application.StartupPath + "\\placesadd.txt", DateTime.Now.ToStr() + " :" + msg + Environment.NewLine);

            }
            catch
            {


            }


        }

        public string SelectedLocation = string.Empty;



        private bool is_in_circle(double circle_x, double circle_y, double r, double x, double y)
        {

            double d = new DotNetCoords.LatLng(Convert.ToDouble(circle_x), Convert.ToDouble(circle_y)).DistanceMiles(new DotNetCoords.LatLng(Convert.ToDouble(x), Convert.ToDouble(y)));

            //double d = Math.Sqrt(((circle_x - x) * (circle_x - x)) + ((circle_y - y) * (circle_y - y)));
            return d <= r;
        }



        private bool FindPoint(double pointLat, double pointLng, List<Gen_Zone_PolyVertice> PontosPolig)
        {//                             X               y               
            int sides = PontosPolig.Count();
            int j = sides - 1;
            bool pointStatus = false;


            if (sides == 1)
            {

                double radius = Convert.ToDouble(PontosPolig[0].Diameter) / 2;
                double lat = Convert.ToDouble(PontosPolig[0].Latitude);
                double lng = Convert.ToDouble(PontosPolig[0].Longitude);


                //double temp = ((lat - pointLat) * (lat - pointLat)) + ((lng - pointLng) * (lng - pointLng));

                //double dist = SqrRoot(temp);

                pointStatus = is_in_circle(pointLat, pointLng, radius, lat, lng);
                //  pointStatus = is_in_circle(lat, lng, radius, pointLat, pointLng);

                //if (dist <= radius)
                //    pointStatus = true;
            }
            else
            {

                for (int i = 0; i < sides; i++)
                {
                    if (PontosPolig[i].Longitude < pointLng && PontosPolig[j].Longitude >= pointLng ||
                        PontosPolig[j].Longitude < pointLng && PontosPolig[i].Longitude >= pointLng)
                    {
                        if (PontosPolig[i].Latitude + (pointLng - PontosPolig[i].Longitude) /
                            (PontosPolig[j].Longitude - PontosPolig[i].Longitude) * (PontosPolig[j].Latitude - PontosPolig[i].Latitude) < pointLat)
                        {
                            pointStatus = !pointStatus;
                        }
                    }
                    j = i;
                }
            }
            return pointStatus;
        }

        public static double SqrRoot(double t)
        {

            double lb = 0, ub = t, temp = 0;
            int count = 50;

            while (count != 0)
            {
                temp = (lb + ub) / 2;

                if (temp * temp == t)
                {

                    return temp;
                }
                else if (temp * temp > t)
                {
                    ub = temp;
                }
                else
                {

                    lb = temp;

                }



                count--;
            }

            return temp;


        }

        void frmSearchLocation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if(e.KeyData== Keys.Insert)
            {
                SelectLocation();

            }
            else if (e.KeyData == Keys.Down)
            {
                if (dgvLocations.Visible && dgvLocations.Rows.Count > 1)
                {
                    BringToFront();
                    dgvLocations.Focus();

                }

            }
        }

        void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SearchLocation();
            }
        }


        void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void item_Click(object sender, EventArgs e)
        {
            gMapControl1.Cursor = EditCursor;
        }

        void gMapControl1_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine("Marker hit? " + ((GMapControl)sender).IsMouseOverMarker);

        }

       

        void btnPick_Click(object sender, EventArgs e)
        {

            if (Lat == 0 && gMapControl1.Overlays.Count > 0 && gMapControl1.Overlays[0].Markers.Count > 0)
            {
                Lat = (float)gMapControl1.Overlays[0].Markers[0].Position.Lat;
                Lng = (float)gMapControl1.Overlays[0].Markers[0].Position.Lng;

            }


            if (Lat == 0 && DefaultLat==0)
            {
                ENUtils.ShowMessage("Please select any Location");
                return;
            }
            else
            {
                IsPick = true;

            }

            if (IsPick)
            {
                if (Lat == 0 && DefaultLat != 0)
                {
                    Lat =(float) DefaultLat;
                    Lng =(float) DefaultLng;

                }
            }

            this.Close();
        }

        void gMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
           
            try
            {

                if (gMapControl1.Cursor == EditCursor)
                {



                    PointLatLng mpt = gMapControl1.FromLocalToLatLng(e.X, e.Y);

                    this.Lat = Convert.ToSingle(mpt.Lat);
                    this.Lng = Convert.ToSingle(mpt.Lng);
                    
                    GMapOverlay polyOverlay = new GMapOverlay(gMapControl1, "overlayJob");

                    GMapMarkerCustom marker1 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(mpt.Lat), Convert.ToDouble(mpt.Lng)), Resources.Resource1.pushpin_PassengerOnBoard);
                    //marker1.ToolTipText = "Location : " + Environment.NewLine + Address.ToStr();
                    //marker1.ToolTipMode = MarkerTooltipMode.Always;
                    //marker1.ToolTip.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    marker1.Tag = new PointLatLng(mpt.Lat, mpt.Lng);
                    

                    polyOverlay.Markers.Add(marker1);
                    gMapControl1.Overlays.Clear();
                    gMapControl1.Overlays.Add(polyOverlay);

                    gMapControl1.Cursor = Cursors.Arrow;
                }
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }
        public Point ConvertGpointToPoint(GPoint point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            SearchLocation();
        }



        private void SearchLocation()
        {
            try
            {
               
                string postCode = txtAddress.Text.Trim();
                string radius = txtRadius.Text.Trim();
                double doubleRadius;

                if (string.IsNullOrEmpty(postCode))
                {
                    //MessageBox.Show("Please enter a PostCode");
                    ENUtils.ShowMessage("Please enter a Address");
                    return;
                }
                else if (string.IsNullOrEmpty(radius))
                {
                    ENUtils.ShowMessage("Please enter Radius.");
                    return;
                }
                else if (!double.TryParse(radius, out doubleRadius))
                {
                    ENUtils.ShowMessage("Radius must be in number.");
                    return;
                }


                


                stp_getCoordinatesByAddressResult baseCoordinates = null;

                if(chkWithinRadius.Checked)
                      baseCoordinates = new TaxiDataContext().stp_getCoordinatesByAddress(AppVars.objPolicyConfiguration.BaseAddress.ToStr(), General.GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr())).FirstOrDefault();

                if (baseCoordinates!=null && baseCoordinates.Latitude.HasValue && baseCoordinates.Longtiude.HasValue)
                {
                    SearchPlaces = GetDistance.SearchPlaces(postCode, new GetDistance.Coords() { Latitude = baseCoordinates.Latitude.Value, Longitude = baseCoordinates.Longtiude.Value }, doubleRadius);

                    if (SearchPlaces != null && SearchPlaces.Status == "OK")
                    {
                        if (SearchPlaces.Result.Count > 0)
                        {
                            var result = SearchPlaces.Result[0];
                            var latLng = new PointLatLng(Convert.ToDouble(result.Geometry.Location.Lat), Convert.ToDouble(result.Geometry.Location.Lng));
                            DrawPoint(latLng);

                            dgvLocations.Visible = true;
                            CreateListOfLocations(SearchPlaces);

                            BringToFront();
                            dgvLocations.Focus();
                        }
                        else
                        {
                            dgvLocations.Columns[0].HeaderText = string.Format("{0} Record(s) found", 0);
                            dgvLocations.Rows.Clear();


                            if (gMapControl1.Overlays.Count > 0 && gMapControl1.Overlays[0].Markers.Count > 0)
                                gMapControl1.Overlays[0].Markers.Clear();

                        }

                    }
                    else
                    {
                        dgvLocations.Visible = false;
                        SearchLocation_old();
                        BringToFront();
                        txtAddress.Focus();
                    }
                }
                else
                {
                    dgvLocations.Visible = false;
                    SearchLocation_old();
                    BringToFront();
                    txtAddress.Focus();
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void DrawPoint(PointLatLng pointLatLng)
        {
            if (pointLatLng != null)
            {

                gMapControl1.Position = pointLatLng; //new PointLatLng(pointLatLng.Lat, pointLatLng.Lng);

                GMapOverlay polyOverlay = new GMapOverlay(gMapControl1, "overlayJob");

                GMapMarkerCustom marker1 = new GMapMarkerCustom(pointLatLng, Resources.Resource1.pushpin_PassengerOnBoard);//new GMapMarkerCustom(new PointLatLng(pointLatLng.Lat, pointLatLng.Lng), Resources.Resource1.pushpin_PassengerOnBoard);
                marker1.ToolTipMode = MarkerTooltipMode.Always;
                marker1.Tag = pointLatLng;//new PointLatLng(pointLatLng.Lat, pointLatLng.Lng);

                polyOverlay.Markers.Add(marker1);
                gMapControl1.Overlays.Clear();
                gMapControl1.Overlays.Add(polyOverlay);

                //Console.WriteLine(pointLatLng.Lat + "," + pointLatLng.Lng);

                gMapControl1.Zoom = 16;
                gMapControl1.Refresh();
            }
        }

        private void CreateListOfLocations(Taxi_AppMain.PlaceSearchResponse places)
        {
            dgvLocations.Rows.Clear();

            if (places != null && places.Status == "OK")
            {


                //if (places.Result.Count <= 3)
                //{
                //    string[] arrV = places.Result.Select(c => c.Name + ", " + c.Formatted_address).Distinct().ToList().ToArray<string>();
                //    for (int i = 0; i < arrV.Count(); i++)
                //    {
                       
                //        //Add items in the listview
                //        string[] arr1 = new string[1];
                       

                //        arr1[0] = arrV[i];

                       
                //        arr1[0] = (arr1[0].LastIndexOf(',') > 0 && (arr1[0].ToStr().ToUpper().EndsWith(", UNITED KINGDOM") || arr1[0].ToStr().ToUpper().EndsWith(", UNITED KINGDOM")) ? arr1[0].Substring(0, arr1[0].LastIndexOf(',')) : arr1[0]).ToUpper().Replace(",", "");


                //        dgvLocations.Rows.Add(arr1);


                //    }

                //    dgvLocations.Columns[0].HeaderText = string.Format("{0} Record(s) found", dgvLocations.Rows.Count);
                //}
                //else
                //{

                    dgvLocations.Columns[0].HeaderText = string.Format("{0} Record(s) found", places.Result.Count());

                    bool isvisible = true;
                    for (int i = 0; i < places.Result.Count(); i++)
                    {
                        isvisible = true;
                        string locationName =  places.Result[i].Formatted_address;


                        if (places.Result[i].Name.ToStr().Trim().Length > 0 && (locationName.ToLower().StartsWith(places.Result[i].Name.ToStr().ToLower() + ", ") == false))
                        {
                            if (locationName.ToStr().Trim().ToLower() != places.Result[i].Name.ToStr().Trim().ToLower())
                            {

                                locationName = places.Result[i].Name + ", " + places.Result[i].Formatted_address;
                            }

                        }
                        locationName = (locationName.LastIndexOf(',') > 0 && (LocationName.ToStr().ToUpper().EndsWith(", UNITED KINGDOM") || LocationName.ToStr().ToUpper().EndsWith(", UNITED KINGDOM")) ? locationName.Substring(0, locationName.LastIndexOf(',')) : locationName).ToUpper().Replace(",", "");

                      
                        string[] arr = new string[1];
                        arr[0] = locationName;


                        if (dgvLocations.Rows.Count > 0 && dgvLocations.Rows.Cast<DataGridViewRow>().Count(c => c.Cells[0].Value.ToStr().ToUpper() == locationName) > 0)
                        {
                            isvisible = false;
                        }
                     

                        dgvLocations.Rows.Add(arr);


                        dgvLocations.Rows[i].Visible = isvisible;

                    }
             //   }
            }
            if (dgvLocations.Rows.Count > 0)
            {
                dgvLocations.Rows[0].Selected = true;
                dgvLocations.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            }
        }

        private void SearchLocation_old()
        {
            try
            {
                string postCode = txtAddress.Text.Trim();

                if (string.IsNullOrEmpty(postCode))
                {
                    //MessageBox.Show("Please enter a PostCode");
                    MessageBox.Show("Please enter a Address");
                    return;
                }
                                
                //postCode = General.GetPostCodeMatch(postCode);
                var latlng = GetDistance.PostCodeToLongLat(postCode, "GB");

                if (latlng != null)
                {
                    GMap.NET.PointLatLng point = new GMap.NET.PointLatLng(latlng.Value.Latitude, latlng.Value.Longitude);
                    gMapControl1.Position = point;

                    GMapOverlay polyOverlay = new GMapOverlay(gMapControl1, "overlayJob");

                    GMapMarkerCustom marker1 = new GMapMarkerCustom(new PointLatLng(Convert.ToDouble(latlng.Value.Latitude), Convert.ToDouble(latlng.Value.Longitude)), Resources.Resource1.pushpin_PassengerOnBoard);
                    marker1.ToolTipMode = MarkerTooltipMode.Always;
                    marker1.Tag = new PointLatLng(latlng.Value.Latitude, latlng.Value.Longitude);

                    polyOverlay.Markers.Add(marker1);
                    gMapControl1.Overlays.Clear();
                    gMapControl1.Overlays.Add(polyOverlay);

                    gMapControl1.Zoom = 16;
                }

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }

        private void DisplayLocationByLatLng()
        {
            try
            {
               

             
               

               
                    GMap.NET.PointLatLng point = new GMap.NET.PointLatLng(this.DefaultLat, this.DefaultLng);
                    gMapControl1.Position = point;

                    GMapOverlay polyOverlay = new GMapOverlay(gMapControl1, "overlayJob");

                    GMapMarkerCustom marker1 = new GMapMarkerCustom(point, Resources.Resource1.pushpin_PassengerOnBoard);
                    marker1.ToolTipMode = MarkerTooltipMode.Never;
                    marker1.Tag =point;
                    polyOverlay.Markers.Add(marker1);
                    gMapControl1.Overlays.Clear();
                    gMapControl1.Overlays.Add(polyOverlay);


                    gMapControl1.Zoom = 16;
             

            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }
        }


        void frmLocationLatLong_Load(object sender, EventArgs e)
        {
            if (OpenFromSearch)
            {
                btnSelectLocation.Visible = true;
                btnPick.Visible = false;

            }
            else
                btnSelectLocation.Visible = false;


            txtAddress.Text = Address;



          



            LoadGoogleMap();

            if (this.DefaultLat != 0 && this.DefaultLat != 0)
            {

                txtAddress.Enabled = false;
                btnSearch.Enabled = false;
                DisplayLocationByLatLng();
            }
            else
            {


                if (!string.IsNullOrEmpty(Address))
                {
                    SearchLocation();
                }
            }
        }

        private void LoadGoogleMap()
        {
            try
            {
                gMapControl1.MapProvider = GMapProviders.GoogleMap;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                gMapControl1.Position = new PointLatLng(51.5171717, -0.1481062);
                gMapControl1.MinZoom = 0;
                gMapControl1.MaxZoom = 19;
                gMapControl1.Zoom = 16;
                gMapControl1.Dock = DockStyle.Fill;
                gMapControl1.DragButton = MouseButtons.Left;

                gMapControl1.MarkersEnabled = true;
                
            }
            catch (Exception ex)
            { }

        }

        private void lstvLocations_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView listCtrl = (ListView)sender;
            if (listCtrl.FocusedItem != null)
            {
                int intdex = listCtrl.FocusedItem.Index;
                if (SearchPlaces != null && SearchPlaces.Status == "OK")
                {
                    var result = SearchPlaces.Result[intdex];
                    var latLng = new PointLatLng(Convert.ToDouble(result.Geometry.Location.Lat), Convert.ToDouble(result.Geometry.Location.Lng));
                    DrawPoint(latLng);
                }
            }
        }

        private void dgvLocations_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dvgCtrl = (DataGridView)sender;
            if (dvgCtrl.SelectedRows.Count > 0 )
            {
                int intdex = dvgCtrl.SelectedRows[0].Index;
                if (SearchPlaces != null && SearchPlaces.Status == "OK")
                {
                    var result = SearchPlaces.Result[intdex];
                    var latLng = new PointLatLng(Convert.ToDouble(result.Geometry.Location.Lat), Convert.ToDouble(result.Geometry.Location.Lng));
                    DrawPoint(latLng);
                }
            }
        }

        private void chkWithinRadius_CheckedChanged(object sender, EventArgs e)
        {
            if(chkWithinRadius.Checked)
            {
                txtRadius.Value = 30;
                txtRadius.Enabled = true;
                dgvLocations.Visible = true;
            }
            else
            {
                txtRadius.Value = 0;
                txtRadius.Enabled = false;
                dgvLocations.Visible = false;

            }
        }

        private void btnPreferredLocation_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dgvLocations.Visible = false;

            }
            else
                dgvLocations.Visible = true;
        }
    }
}
