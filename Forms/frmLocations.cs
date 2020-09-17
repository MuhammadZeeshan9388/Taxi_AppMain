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
using Telerik.WinControls.Enumerations;

namespace Taxi_AppMain
{
    public partial class frmLocations : UI.SetupBase
    {
       // bool IsLocationTypeLoaded = false;
        LocationBO objMaster;
        public frmLocations()
        {
            InitializeComponent();
            InitializeConstructor();
        }

        private int? _locationId;

        public int? LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }

        public frmLocations(int? locTypeId)
        {
            InitializeComponent();
            InitializeConstructor();

            ddlLocationType.SelectedValue = locTypeId;
            txtLocName.Focus();
        }

        private void InitializeConstructor()
        {
          
            objMaster = new LocationBO();
            this.SetProperties((INavigation)objMaster);
            FillCombos();

            this.Shown += new EventHandler(frmLocations_Shown);
            this.FormClosed += new FormClosedEventHandler(frmLocations_FormClosed);
            txtShortCutKey.Enabled = false;
            txtShortCutKey.Text = "";
            this.KeyDown += new KeyEventHandler(frmLocations_KeyDown);
            this.ddlLocationType.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(ddlLocationType_SelectedIndexChanged);
          
           
        }



        void ddlLocationType_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                if (IsLoaded == false)
                    return;

                if (ddlLocationType.SelectedValue.ToInt() == Enums.LOCATION_TYPES.ADDRESS)
                {
                    txtAddress.CharacterCasing = CharacterCasing.Upper;
                    txtLocName.CharacterCasing = CharacterCasing.Upper;
                 //   txtLocName.Text = txtLocName.Text.ToUpper();
                  //  txtLocName.Enabled = false;
                }
                else
                {
                    txtAddress.CharacterCasing = CharacterCasing.Normal;
                    txtLocName.CharacterCasing = CharacterCasing.Normal;
                   // txtLocName.Text = txtLocName.Text.ToLower();
                  //  txtLocName.Enabled = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
       

      

        void frmLocations_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        
        void frmLocations_Shown(object sender, EventArgs e)
        {
            txtLocName.Focus();
            if (ddlLocationType.SelectedValue.ToInt() == 7)
            {
                ddlLocationType.Enabled = false;
            }
        }

      
     

        private void FillCombos()
        {
            //ComboFunctions.FillLocationTypeCombo(ddlLocationType,c=>c.Id!=Enums.LOCATION_TYPES.ADDRESS);

            ComboFunctions.FillLocationTypeCombo(ddlLocationType);
            ComboFunctions.FillZonesCombo(ddlZone);

        }

        private void LoadLocationsList()
        {


         


            var data1 = AppVars.BLData.GetAll<Gen_Location>().OrderBy(c => c.LocationName);
            var data2 = AppVars.BLData.GetAll<Gen_LocationType>();
            var data3 = AppVars.BLData.GetAll<Gen_Zone>();

            var query = from a in data1
                        join b in data2 on a.LocationTypeId equals b.Id
                        //join c in data3 on a.ZoneId equals c.Id
                        select new
                        {
                            Id = a.Id,
                            LocationName = a.LocationName,
                            LocationType = b.LocationType,
                            Address = a.Address,
                            PostCode = a.PostCode,
                            //Zone =c.ZoneName

                        };



    
         
        }

        public override void PopulateData()
        {
            base.PopulateData();
        }



        #region Overridden Methods


      

        public override void DisplayRecord()
        {
            if (objMaster.Current == null) return;

            txtLocName.Text = objMaster.Current.LocationName;
            ddlLocationType.SelectedValue = objMaster.Current.LocationTypeId;
            txtAddress.Text = objMaster.Current.Address;
            txtPostCode.Text = objMaster.Current.PostCode;
           // txtShortCutKey.Text = objMaster.Current.ShortCutKey;
          //  txtCity.Text = objMaster.Current.City;
            ddlZone.SelectedValue = objMaster.Current.ZoneId;
            numExtraChrgs.Value = objMaster.Current.ExtraCommission.ToDecimal();

            chkShortKey.Checked = objMaster.Current.CustomShortKey.ToBool();

            txtShortCutKey.Text = objMaster.Current.ShortCutKey.ToStr().Trim();
            txtLat.Text =objMaster.Current.Latitude!=null ? objMaster.Current.Latitude.ToStr():"";
            txtLng.Text = objMaster.Current.Longitude != null ? objMaster.Current.Longitude.ToStr() : "";
            
        }



        public override void AddNew()
        {
            OnNew();
        }

        public override void OnNew()
        {
            ddlLocationType.SelectedValue = null;
            ddlZone.SelectedValue = null;

            txtAddress.CharacterCasing = CharacterCasing.Normal;
            txtLocName.CharacterCasing = CharacterCasing.Normal;
        }


        public override void Save()
        {
            try
            {
               
                if (objMaster.PrimaryKeyValue == null)
                {
                    objMaster.New();
                }
                else
                {
                    objMaster.Edit();
                }

                objMaster.Current.LocationName = txtLocName.Text.Trim();
                objMaster.Current.LocationTypeId = ddlLocationType.SelectedValue.ToIntorNull();
                objMaster.Current.Address = txtAddress.Text.Trim();
                objMaster.Current.PostCode = txtPostCode.Text.Trim();
                objMaster.Current.ZoneId = ddlZone.SelectedValue.ToIntorNull();



                objMaster.Current.ExtraCommission = numExtraChrgs.Value.ToDecimal();
                objMaster.Current.CustomShortKey = chkShortKey.Checked;
                objMaster.Current.ShortCutKey = txtShortCutKey.Text.Trim().ToLower();
                objMaster.Current.Latitude =txtLat.Text.Trim().Length>0 ? Convert.ToDouble(txtLat.Text.Trim()):0;
                objMaster.Current.Longitude =txtLng.Text.Trim().Length>0 ? Convert.ToDouble(txtLng.Text.Trim()):0;


                if (objMaster.Current.Latitude > 0 && objMaster.Current.ZoneId==null)
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


               // objMaster.Current.FullLocationName=  (REPLACE((  objMaster.Current.LocationName + ' '+REPLACE(((objMaster.Current.Address,objMaster.Current.PostCode,"")),objMaster.Current.LocationName,"") + " "+objMaster.Current.PostCode),"  "," ");
                objMaster.IsManualLocation = true;
                objMaster.Save();



                AppVars.keyLocations = (from a in AppVars.BLData.GetAll<Gen_Location>(c => c.ShortCutKey != string.Empty)
                                        select a.ShortCutKey).Distinct().ToList();


                LocationId = objMaster.Current.Id.ToIntorNull();


                using (TaxiDataContext db = new TaxiDataContext())
                {
                   long locationAddressId=   db.ExecuteQuery<long>("select  AddressId from Gen_Locations_Address where locationId=" + LocationId).FirstOrDefault();

                   Gen_Address objAddress = db.Gen_Addresses.FirstOrDefault(c => c.EntityID == locationAddressId);

                        if(objAddress!=null)
                        {

                            if(AppVars.listOfAddress.Count(c=>c.AddressLine1==objAddress.AddressLine1)==0)
                            {

                                AppVars.listOfAddress.Insert(0, new stp_GetFullAddressesResult { AddressLine1 = objAddress.AddressLine1, PostalCode = objAddress.PostalCode, ZoneId = objAddress.ZoneId });

                            }

                           
                        }
                }


                General.RefreshListWithoutSelected<frmLocationList>("frmLocationList1");

            }
            catch (Exception ex)
            {
                if (objMaster.Errors.Count > 0)
                    ENUtils.ShowMessage(objMaster.ShowErrors());
                else
                {
                    ENUtils.ShowMessage(ex.Message);

                }
            }
        }

        

        #endregion

        private void chkShortKey_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            try
            {
                ShortcutKey(args.ToggleState);
            }
            catch (Exception ex)
            {
            }
        }
        private void ShortcutKey(ToggleState toggle)
        {
            if (toggle == ToggleState.On)
            {
                txtShortCutKey.Enabled = true;
            }
            else
            {
                txtShortCutKey.Enabled = false;
                txtShortCutKey.Text = "";
            }
        }
        void frmLocations_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose(true);

            GC.Collect();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string searchVal = txtLocName.Text.Trim();

            //    if (txtAddress.Text.Trim() != searchVal)
            //    {
            //        searchVal = (txtLocName.Text.Trim() + " " + txtAddress.Text.Trim()).Trim();
            //    }

            //    GetDistance.Coords? cls = GetDistance.PostCodeToLongLat(searchVal, "GB");
            //    if (cls != null)
            //    {
            //        txtLat.Text = cls.Value.Latitude.ToStr();
            //        txtLng.Text = cls.Value.Longitude.ToStr();

            //    }
            //}
            //catch (Exception ex)
            //{
            //    ENUtils.ShowMessage(ex.Message);

            //}

            try
            {
                string locName = txtLocName.Text.Trim().ToUpper();
                string address = txtAddress.Text.Trim().ToUpper();
                string search = string.Empty;
                if (locName == address)
                {

                    search = address;
                }
                else
                {

                    string postcode = General.GetPostCodeMatch(address);

                    if (postcode.Length > 0)
                    {
                        address = address.Replace(postcode, "").Trim();

                    }
                    else
                    {
                        postcode = txtPostCode.Text.Trim();

                    }


                    if (locName == address)
                    {

                        search = (locName + " "+ address + " " + postcode).Trim();
                    }
                    else
                    {
                        search = (locName + " " + address).Trim();

                    }
               
                         

                }


             





                frmSearchLocation frm = new frmSearchLocation();
                frm.LocationName = search;
                frm.DefaultLat =txtLat.Text.Length >0 ?Convert.ToDouble(txtLat.Text):0;
                frm.DefaultLng =txtLng.Text.Length >0 ? Convert.ToDouble(txtLng.Text):0;

                frm.Address = search;
                frm.PostCode = txtPostCode.Text;
                frm.ShowDialog();

                if (frm.IsPick)
                {

                    txtLat.Text = frm.Lat.ToString();
                    txtLng.Text = frm.Lng.ToString();
                }
                //this.Latitude = frm.Lat;
                //this.Longitude = frm.Lng;

                frm.Dispose();
            }
            catch (Exception ex)
            {
                ENUtils.ShowMessage(ex.Message);
            }


        }


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

    }



}
