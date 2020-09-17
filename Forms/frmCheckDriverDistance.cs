using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using Taxi_Model;
using System.Xml;
using Utils;

namespace Taxi_AppMain
{
    public partial class frmCheckDriverDistance : UI.SetupBase
    {


        BackgroundWorker worker = null;

        public frmCheckDriverDistance()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmCheckDriverDistance_FormClosing);
            FillCombo();
        }

        private bool IsClosed = false;

        void frmCheckDriverDistance_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosed = true;

            if (worker != null )
            {
                if(worker.IsBusy)
                  worker.CancelAsync();

                 worker.Dispose();
            }


           

            Dispose(true);
        }

        private void FillCombo()
        {
            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    //var listofZones = db.Gen_Zones.Where(c => c.MaxLatitude != null).OrderBy(c => c.OrderNo).ToList();
                    //ComboFunctions.FillCombo(listofZones, ddlZone, "ZoneName", "Id");


                    var listofDrvs = (from a in db.Fleet_DriverQueueLists
                                      where a.Status != null && a.Status == true && a.DriverId != null && a.Fleet_Driver.HasPDA == true
                                      select new
                                          {

                                              Id = a.DriverId,
                                              DriverName = a.Fleet_Driver.DriverNo + " - " + a.Fleet_Driver.DriverName

                                          }).ToList();

                    ComboFunctions.FillCombo(listofDrvs, ddlDriver, "DriverName", "Id");



                }
            }
            catch (Exception ex)
            {


            }

         




        }

        private void btnExitForm_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCheckDistance_Click(object sender, EventArgs e)
        {
            CheckDistance();
        }

        private void CheckDistance()
        {

            try
            {

                int driverId = ddlDriver.SelectedValue.ToInt();

                if (driverId == 0)
                {
                    txtDistance.Text = string.Empty;
                    txtError.Text = "Warning!. Please select a driver";
                    return;
                }


                var objLoc = General.GetObject<Fleet_Driver_Location>(c => c.DriverId == driverId);

                if (objLoc == null || (objLoc.Latitude==0 && objLoc.Longitude==0))
                {
                    txtDistance.Text = string.Empty;
                    txtError.Text = "Warning!. Driver Current Location not found";
                    return;
                }

                double latitude = objLoc.Latitude;
                double longitude = objLoc.Longitude;


                string distance = string.Empty;

                string origin = string.Empty;


                string baseAddress = General.GetObject<Gen_SysPolicy_Configuration>(c => c.Id != 0).BaseAddress.ToStr();

                origin = General.GetPostCodeMatch(baseAddress.ToUpper().Trim());


                if (string.IsNullOrEmpty(origin))
                {
                    txtDistance.Text = string.Empty;
                    txtError.Text = "Warning!. Base Coordinates are not defined in Settings";
                    return;


                }


                txtError.Text = string.Empty;
                btnCheckDistance.Text = "Please Wait...";
                btnCheckDistance.Enabled = false;

                if (worker == null)
                {
                    worker = new BackgroundWorker();
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                    worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                    worker.WorkerSupportsCancellation = true;
                }


                this.origin = origin;
                this.distance = distance;
                this.latitude = latitude;
                this.longitude = longitude;

                worker.RunWorkerAsync();
                //var objCoord = General.GetObject<Gen_Coordinate>(c => c.PostCode == origin);

                //if (objCoord == null)
                //    return;


               
            



            }
            catch (Exception ex)
            {


            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(origin) && origin.Contains(' '))
                {
                    string url = "http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + latitude + "," + longitude + ", UK&mode=driving&sensor=false";


                    System.Data.DataSet ds = new System.Data.DataSet();
                    ds.ReadXml(new XmlTextReader(url));
                    DataTable dt = ds.Tables["duration"];

                    if (dt != null)
                    {
                        distance = dt.Rows.OfType<DataRow>().LastOrDefault()[1].ToStr().Trim();

                        dt = ds.Tables["distance"];

                        if (dt != null)
                        {
                            string dist = dt.Rows.OfType<DataRow>().LastOrDefault()[1].ToStr().Trim();
                            if (dist.EndsWith("km"))
                            {
                                dist = dist.Replace("km", "").Trim();
                                double miles = Math.Round((Convert.ToDouble(dist) * 0.62137), 2);
                                distance += " , " + miles + " mi";
                            }
                        }
                    }
                    ds.Dispose();

                    // bing
                    if (dt == null || string.IsNullOrEmpty(distance))
                    {


                        ds = new System.Data.DataSet();
                        // ds.Tables.Clear();

                        ds.ReadXml(new XmlTextReader("http://dev.virtualearth.net/REST/V1/Routes/Driving?o=xml&wp.0=" + origin + ", UK" + "&wp.1=" + latitude + "," + longitude + "&DistanceUnit=Mile&key=At9uWeg3Sk_C611VLD0cc6i9oYu7IioNxvjNUN6-blcjKIX_L2n5G2ObOgEVlNZ_"));
                        dt = ds.Tables["SubLegSummary"];

                        if (dt != null)
                        {
                            distance = dt.Rows.OfType<DataRow>().LastOrDefault()[1].ToStr().Trim();

                            if (!string.IsNullOrEmpty(distance))
                                distance = (distance.ToLong() / 60).ToStr() + " mins";

                        }
                        else
                        {
                            dt = ds.Tables["RouteLeg"];

                            if (dt != null)
                            {
                                DataRow dRow = dt.Rows.OfType<DataRow>().FirstOrDefault();

                                if (dRow != null)
                                {

                                    distance = dRow["TravelDuration"].ToStr();


                                    if (!string.IsNullOrEmpty(distance))
                                    {
                                        distance = (distance.ToLong() / 60).ToStr() + " mins";

                                        distance += " , " + Math.Round(Convert.ToDouble(dRow["TravelDistance"].ToStr()), 2) + " mi";
                                    }
                                }



                            }

                        }
                        ds.Dispose();


                    }

                    if (dt == null || string.IsNullOrEmpty(distance))
                    {


                        ds = new System.Data.DataSet();
                        // ds.Tables.Clear();

                        ds.ReadXml(new XmlTextReader("http://www.mapquestapi.com/directions/v2/route?key=Fmjtd%7Cluua25uz20%2Can%3Do5-96255z&callback=renderAdvancedNarrative&ambiguities=ignore&outFormat=xml&inFormat=xml&xml=<route><locations><location>" + origin + ", UK" + "</location><location>" + latitude + "," + longitude + "</location></locations><options><avoids></avoids><avoidTimedConditions>false</avoidTimedConditions><doReverseGeocode>false</doReverseGeocode><routeType>fastest</routeType><timeType>0</timeType><locale>en_GB</locale><unit>m</unit><enhancedNarrative>false</enhancedNarrative><drivingStyle>2</drivingStyle><highwayEfficiency>21.0</highwayEfficiency></options></route>"));

                        dt = ds.Tables["leg"];

                        if (dt != null)
                        {
                            distance = dt.Rows.OfType<DataRow>().FirstOrDefault()[1].ToStr().Trim();

                            if (!string.IsNullOrEmpty(distance))
                                distance = (distance.ToLong() / 60).ToStr() + " mins";

                        }
                        ds.Dispose();


                    }

                }


               

            }
            catch (Exception ex)
            {


            }
        }


        private string origin;
        private string distance;
        private double latitude;
        private double longitude;

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (IsClosed)
                    return;

                txtDistance.Text = distance.ToStr();
                txtError.Text = string.Empty;
                btnCheckDistance.Text = "Calculate Distance";
                btnCheckDistance.Enabled = true;
            }
            catch (Exception ex)
            {

            }
        }
       
    }
}
