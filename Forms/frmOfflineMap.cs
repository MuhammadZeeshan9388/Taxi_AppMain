using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utils;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading;
using System.IO;
using Taxi_Model;

namespace Taxi_AppMain
{
    public partial class frmOfflineMap : Form
    {
        string Distance = string.Empty;


        BackgroundWorker worker = null;
        Thread thread = null;
        object[] obj = null;
        string time = string.Empty;
        string Origen = string.Empty;
        string Destination = string.Empty;
        public frmOfflineMap(string origin, string[] via, string destination)
        {

            InitializeComponent();
            SetFormatting();

            obj = new object[3];
            obj[0] = origin;
            obj[1] = via;
            obj[2] = destination;
            Origen = origin;
            Destination = destination;

            worker = new BackgroundWorker();
            axMappointControl1.Visible = false;

            thread = new Thread(new ThreadStart(loadFile));
            thread.Start();

            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);



            this.FormClosing += new FormClosingEventHandler(frmOfflineMap_FormClosing);
        }

        void frmOfflineMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            axMappointControl1.ActiveMap.Saved = true;

            axMappointControl1.ActiveMap.Application.Quit();

            IsOnClosed = true;

            grdLister.Font.Dispose();
            grdLister.Dispose();
            this.Dispose(true);

            GC.Collect();

        }

        private void SetFormatting()
        {
            grdLister.RowTemplate.Height = 60;
        }
        public void loadFile()
        {
            string template = System.Windows.Forms.Application.StartupPath + "\\Map.ptt";
            axMappointControl1.NewMap(template);
            axMappointControl1.Units = MapPoint.GeoUnits.geoMiles;


            this.Invoke(new MethodInvoker(delegate
            {
                worker.RunWorkerAsync();
            }));

        }


        delegate void UIDelegate(string ori, string des, string time);
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (this.InvokeRequired)
            {
                UIDelegate del = new UIDelegate(GetDistanceAndTime);
                this.BeginInvoke(del, Origen, Destination, time);
            }
            else
            {
                GetDistanceAndTime(Origen, Destination, time);
            }


        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowMapData();
            axMappointControl1.Visible = true;
        }



      
        Type objClassType = null;
        string gettime = string.Empty;
        decimal distance = 0.00m;



        private object GetLocation(string postCode)
        {


            if (postCode.StartsWith(">"))
            {
                postCode = postCode.Substring(1);

            }


            MapPoint.FindResults objRes = axMappointControl1.ActiveMap.FindAddressResults("", "", "", "", postCode, MapPoint.GeoCountry.geoCountryUnitedKingdom);


            MapPoint.Location loc = null;

            if (objRes.Count == 0)
            {
                var objCoord = new TaxiDataContext().Gen_Coordinates.FirstOrDefault(c => c.PostCode == postCode);

                if (objCoord != null)
                {

                    loc = axMappointControl1.ActiveMap.GetLocation(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude), 1.00);
                }



            }
            else
            {
                object item = 1;
                loc = (MapPoint.Location)objRes.get_Item(ref item);



            }


            return (object)loc;

        }

        public void GetDistanceAndTime(string origin, string destination, string estimatedTime)
        {

            try
            {



            
                string orignpostcode = General.GetPostCodeMatch(origin);
                string destinationpostcode = General.GetPostCodeMatch(destination);


               

                MapPoint.Route objRoute = axMappointControl1.ActiveMap.ActiveRoute;



                    objRoute.Waypoints.Add(GetLocation(orignpostcode), "A");
                    object item = 1;
                
                objRoute.Waypoints.get_Item(ref item).SegmentPreferences = MapPoint.GeoSegmentPreferences.geoSegmentShortest;


                  

               // }
                string[] arrayvia = (string[])obj[1];

                object viaItem = 2;
                int viaVal = 2;
                if (arrayvia != null)
                {
                   
                  
                    foreach (var itemvia in arrayvia)
                    {

                      

                        objRoute.Waypoints.Add(GetLocation(General.GetPostCodeMatch(itemvia)), "B");
                            objRoute.Waypoints.get_Item(ref viaItem).SegmentPreferences = MapPoint.GeoSegmentPreferences.geoSegmentShortest;

                            viaVal++;
                            viaItem =(object) viaVal;
                    
                    }


                }

             


                object item2 = viaItem;

                 objRoute.Waypoints.Add(GetLocation(destinationpostcode), "B");
           //     objRoute.Waypoints.Optimize();
                
                objRoute.Waypoints.get_Item(ref item2).SegmentPreferences = MapPoint.GeoSegmentPreferences.geoSegmentShortest;


                objRoute.ZoomTo();

                objRoute.Calculate();



                distance = Convert.ToDecimal(objRoute.Distance);
                gettime = (objRoute.DrivingTime * 24 * 60).ToInt().ToStr() + " mins";

                axMappointControl1.ItineraryVisible = false;

            }
            catch (Exception ex)
            {

                string[] via=(string[])obj[1];
                string error = string.Empty;


                if (via != null && via.Count() > 0)
                {
                    error = ex.Message;

                    foreach (var itemVia in via)
	                {
                        error += Environment.NewLine + itemVia;
	                }
                  

                }
                

                File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\errorLog.txt", error);
                // MessageBox.Show(ex.Message);

            }

        }



        private void ShowMapData()
        {

            try
            {
                pictureBox1.Visible = false;
                string[] via = (string[])obj[1];

                ShowHeaderData();



            }
            catch (Exception ex)
            {


            }

        }


        private void ShowHeaderData()
        {
            try
            {

                if (IsOnClosed)
                    return;

                string origin = obj[0].ToStr();
                string[] via = (string[])obj[1];
                string destination = obj[2].ToStr();

                grdLister.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                grdLister.Visible = true;

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("A", typeof(string)));

                DataRow row = dt.NewRow();

                row["A"] = origin;


                int unicode = 65;
                char character = (char)unicode;



                if (via != null)
                {

                    for (int i = 0; i < via.Count(); i++)
                    {


                        unicode++;
                        character = (char)unicode;


                        dt.Columns.Add(new DataColumn(character.ToStr(), typeof(string)));
                        row[character.ToStr()] = via[i];
                    }
                }



                if (via == null || via.Count() == 0)
                {
                    character = (char)66;


                }
                else
                {
                    unicode++;
                    character = (char)unicode;


                }

                dt.Columns.Add(new DataColumn(character.ToStr(), typeof(string)));


                dt.Columns.Add(new DataColumn("Distance", typeof(string)));


                Distance = Math.Round(distance, 1).ToStr();

                row[character.ToStr()] = destination;
                row["Distance"] = Distance + " Miles : Time " + gettime;


                dt.Rows.Add(row);
                DataView dv = new DataView(dt);
                grdLister.DataSource = dv;


            }
            catch (Exception ex)
            {
                File.WriteAllText(System.Windows.Forms.Application.StartupPath + "\\errorLogheader.txt", ex.Message);

            }

        }

        private bool IsOnClosed;



        private void button1_Click(object sender, EventArgs e)
        {

            //  HtmlElement loBtn = (HtmlElement)webBrowser1.Document.GetElementById("panelarrow2");
            //  loBtn.InvokeMember("click");

        }

        //private void grdLister_CellFormatting(object sender, CellFormattingEventArgs e)
        //{
        //       e.CellElement.TextWrap = true;                
        //}



    }
}
