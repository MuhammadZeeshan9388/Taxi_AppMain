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
using Telerik.WinControls.UI;
using Telerik.WinControls;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmMap : Form
    {
        string Distance = string.Empty;
      

        private void SetFormatting()
        {
            grdLister.TableElement.RowHeight = 60;

        }


        object[] obj = null;
        delegate void UIDelegate();
        public frmMap(string origin,string[] via, string destination)
        {
            

            InitializeComponent();
          //  ping = new Ping();
          //  ping.PingCompleted += new PingCompletedEventHandler(ping_PingCompleted);
         
            
            SetFormatting();




             obj = new object[3];
            obj[0] = origin;
            obj[1] = via;
            obj[2] = destination;

          

         //   ping.SendAsync("www.google.com", obj);

            this.FormClosed += new FormClosedEventHandler(frmMap_FormClosed);
           // this.FormClosing += new FormClosingEventHandler(frmMap_FormClosing);
          


            ShowGoogleMap_RouteDirections(origin, via, destination);

             new Thread(new ThreadStart(ShowMapData)).Start();
           // ShowMapData();
             this.webBrowser1.ScriptErrorsSuppressed = true;
        }

        void frmMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                IsOnClosed = true;
                webBrowser1.Visible = false;
                //   this.WindowState = FormWindowState.Minimized;

                webBrowser1.Controls.Clear();
                webBrowser1.Dispose();


                grdLister.Font.Dispose();
                grdLister.Dispose();
                this.Dispose(true);

                GC.Collect();
            }
            catch 
            {


            }
        }


        private void ShowMapData()
        {

            try
            {
            

            string origin = obj[0].ToStr();
            string[] via = (string[])obj[1];
            string destination = obj[2].ToStr();


           
            //webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);


            CalculateTotalDistance(origin, via, destination);


                if (this.InvokeRequired)
                {

                    this.BeginInvoke(new UIDelegate(ShowHeaderData));
                }
                else
                {
                    ShowHeaderData();
                }

            }
            catch(Exception ex)
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

                grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
                grdLister.Visible = true;

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("A", typeof(string)));

                DataRow row = dt.NewRow();

                row["A"] = origin;


                int unicode = 65;
                char character = (char)unicode;


                for (int i = 0; i < via.Count(); i++)
                {


                    unicode++;
                    character = (char)unicode;


                    dt.Columns.Add(new DataColumn(character.ToStr(), typeof(string)));
                    row[character.ToStr()] = via[i];
                }



                if (via.Count() == 0)
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




                row[character.ToStr()] = destination;
                row["Distance"] = Distance;


                dt.Rows.Add(row);
                DataView dv = new DataView(dt);
                grdLister.DataSource = dv;


                grdLister.CurrentRow = null;
            }
            catch
            {

            }

        }

        //void frmMap_FormClosing(object sender, FormClosingEventArgs e)
        //{

        //    try
        //    {
        //        IsOnClosed = true;
        //        webBrowser1.Visible = false;
        //     //   this.WindowState = FormWindowState.Minimized;

        //        webBrowser1.Controls.Clear();
        //        webBrowser1.Dispose();


        //        grdLister.Font.Dispose();
        //        grdLister.Dispose();
        //        this.Dispose(true);

        //        GC.Collect();
        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //}


        private bool IsOnClosed;

        void ping_PingCompleted(object sender, PingCompletedEventArgs e)
        {
            try
            {
                if (IsOnClosed==true || e.Reply == null || e.UserState == null)
                    return;



                object[] objArr = (object[])e.UserState;

                string origin = objArr[0].ToStr();
                string[] via =(string[]) objArr[1];
                string destination = objArr[2].ToStr();

              
                ShowGoogleMap_RouteDirections(origin, via, destination);
                //webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);


                CalculateTotalDistance(origin, via, destination);
                grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;

                grdLister.Visible = true;

                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("A", typeof(string)));

                DataRow row = dt.NewRow();

                row["A"] = origin;


                int unicode = 65;
                char character = (char)unicode;


                for (int i = 0; i < via.Count(); i++)
                {


                    unicode++;
                    character = (char)unicode;


                    dt.Columns.Add(new DataColumn(character.ToStr(), typeof(string)));
                    row[character.ToStr()] = via[i];
                }



                if (via.Count() == 0)
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




                row[character.ToStr()] = destination;
                row["Distance"] = Distance;


                dt.Rows.Add(row);
                DataView dv = new DataView(dt);
                grdLister.DataSource = dv;


                grdLister.CurrentRow = null;
            }
            catch 
            {


            }
            
        }



        public  void ShowGoogleMap_RouteDirections(string PickupPoint, string[] ViaLocs, string DestinationPoint)
        {
            try
            {

                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
                {

                    PickupPoint = General.GetPostCodeMatch(PickupPoint);
                    DestinationPoint = General.GetPostCodeMatch(DestinationPoint);


                    bool IsPickupAlpha = PickupPoint.IsAlpha();
                    bool IsDestAlpha = DestinationPoint.IsAlpha();

                    if (IsPickupAlpha || IsDestAlpha)
                    {
                        return;

                    }

                }


                if (PickupPoint == string.Empty || DestinationPoint == string.Empty)
                {

                    return;
                }



                const string BROWSER_EMULATION_KEY = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";


                const int browserEmulationMode = 11000;
                //const int browserEmulationMode = 9999;

                RegistryKey browserEmulationKey =
                    Registry.CurrentUser.OpenSubKey(BROWSER_EMULATION_KEY, RegistryKeyPermissionCheck.ReadWriteSubTree) ??
                    Registry.CurrentUser.CreateSubKey(BROWSER_EMULATION_KEY);

                if (browserEmulationKey != null)
                {
                    String appname = Process.GetCurrentProcess().ProcessName + ".exe";


                    string val = browserEmulationKey.GetValue(appname).ToStr();

                    if (val != "11000")
                    {

                        browserEmulationKey.SetValue(appname, browserEmulationMode, RegistryValueKind.DWord);
                        browserEmulationKey.Close();
                    }
                }


                if (PickupPoint.EndsWith(", UK") == false)
                    PickupPoint = PickupPoint + ", UK";

                if (DestinationPoint.EndsWith(", UK") == false)
                    DestinationPoint = DestinationPoint + ", UK";



                string via = string.Empty;
                if (ViaLocs.Count() > 0)
                {
                    if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
                    {

                        ViaLocs = ViaLocs.Select(c => General.GetPostCodeMatch(c)).Where(c => c.Length > 0).ToArray<string>();
                        if (ViaLocs.Count() == 0)
                        {
                            return;
                        }
                        via = string.Join("|", ViaLocs.Select(c => General.GetPostCodeMatch(c) + ", UK").ToArray<string>());
                    }
                    else
                    {
                        ViaLocs = ViaLocs.Select(c => c).Where(c => c.Length > 0).ToArray<string>();
                        if (ViaLocs.Count() == 0)
                        {
                            return;
                        }
                        via = string.Join("|", ViaLocs.Select(c => c + ", UK").ToArray<string>());

                    }
                }



                PickupPoint = PickupPoint.Replace("to:", "|").Trim();


                int mapApiType = AppVars.objPolicyConfiguration.NoofMilesStartRate.ToInt();

                StringBuilder src = new StringBuilder();

                //if (via.Length > 0)
                //{

                //    if (mapApiType == 0 || mapApiType == 1)
                //    {

                //        src.Append("<!DOCTYPE html><html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" />");
                //        src.Append("<script src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>");
                //        src.Append("<script src=\"http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.1.min.js\"></script>");

                //        src.Append("</head><body><div id=\"map\" style=\"width: 960px; height: 710px;Left:-10px;Top:-13px\"></div>");

                //        src.Append("<script type=\"text/javascript\">");


                //        src.Append("var map = new google.maps.Map(document.getElementById('map'), {  zoom: 14,     mapTypeId: google.maps.MapTypeId.ROADMAP,   mapTypeControl: false,   streetViewControl: false,   panControl: false,   zoomControlOptions: {      position: google.maps.ControlPosition.LEFT_BOTTOM    }  }); ");



                //        src.Append(" var directionsDisplay= new google.maps.DirectionsRenderer(); ");

                //        src.Append(" var directionsService = new google.maps.DirectionsService();");

                //        src.Append(" directionsDisplay.setMap(map);");



                //        src.Append("function AutoCenter() { ");

                //        src.Append("  var start = \"" + PickupPoint + "\";  ");

                //        src.Append(" var end = \"" + DestinationPoint + "\"; ");




                //        //src.Append("  var start = 'HA2 0DU UK';  ");

                //        //src.Append(" var end = 'TW6 2GA UK'; ");

                //        src.Append("  var wpoints=[");
                //        string appendComma = ",";
                //        for (int i = 0; i < ViaLocs.Count(); i++)
                //        {

                //            if ((i + 1) != ViaLocs.Count())
                //            {
                //                appendComma = ",";
                //            }
                //            else
                //                appendComma = "";
                //            src.Append("   { location: \"" + ViaLocs[i] + "\", stopover: false}" + appendComma);
                //        }


                //        src.Append("];");



                //        src.Append("   var request = {      origin: start,     destination: end, waypoints: wpoints,  optimizeWaypoints: true,  travelMode: google.maps.TravelMode.DRIVING    }; ");



                //        src.Append("   directionsService.route(request, function(response, status) ");

                //        src.Append("{ ");

                //        src.Append(" if (status == google.maps.DirectionsStatus.OK)");

                //        src.Append(" { ");

                //        src.Append("  directionsDisplay.setDirections(response); ");

                //        src.Append(" }  ");
                //        src.Append(" });");
                //        src.Append(" }; ");


                //        src.Append(" AutoCenter(); ");


                //        src.Append("   </script>  ");



                //        src.Append("</body>");
                //        src.Append("</html>");
                //    }
                //    else
                //    {


                //        PickupPoint = PickupPoint.Replace(" ", "+");
                //        DestinationPoint = DestinationPoint.Replace(" ", "+");
                //        via = via.Replace(" ", "+");

                //        src.Append("<iframe" +
                //                             " width=\"960\"" +
                //                             " height=\"710\"" +
                //                             " \frameborder=\"0\" style=\"border:0;margin-left:-10;margin-top:-10;margin-right:-10\"" +
                //                             " src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyAFkZHqTas4EKYEEsk8J3aQh0zQJ-tsWmY&origin=" +
                //                                PickupPoint + "&waypoints=" + via + "&destination=" + DestinationPoint + "&avoid=tolls|highways" + "\">" +
                //                           "</iframe>");
                //    }
                //}
                //else
                //{


                //    if (mapApiType == 0 || mapApiType == 1)
                //    {

                //        src.Append("<!DOCTYPE html><html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" />");
                //        src.Append("<script src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>");
                //        src.Append("<script src=\"http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.1.min.js\"></script>");

                //        src.Append("</head><body><div id=\"map\" style=\"width: 960px; height: 710px;Left:-10px;Top:-13px\"></div>");

                //        src.Append("<script type=\"text/javascript\">");


                //        src.Append("var map = new google.maps.Map(document.getElementById('map'), {  zoom: 14,     mapTypeId: google.maps.MapTypeId.ROADMAP,   mapTypeControl: false,   streetViewControl: false,   panControl: false,   zoomControlOptions: {      position: google.maps.ControlPosition.LEFT_BOTTOM    }  }); ");



                //        src.Append(" var directionsDisplay= new google.maps.DirectionsRenderer(); ");

                //        src.Append(" var directionsService = new google.maps.DirectionsService();");

                //        src.Append(" directionsDisplay.setMap(map);");




                //        src.Append("function AutoCenter() { ");


                //        src.Append("  var start = \"" + PickupPoint + "\";  ");

                //        src.Append(" var end = \"" + DestinationPoint + "\"; ");

                //        //  src.Append("  var wpoints=[{ location: \"UB6 0LY, UK\", stopover: false},{ location: \"RH8 9EU, UK\", stopover: false}];");


                //        src.Append("   var request = {      origin: start,     destination: end,   travelMode: google.maps.TravelMode.DRIVING    }; ");



                //        src.Append("   directionsService.route(request, function(response, status) ");

                //        src.Append("{ ");

                //        src.Append(" if (status == google.maps.DirectionsStatus.OK)");

                //        src.Append(" { ");

                //        src.Append("  directionsDisplay.setDirections(response); ");

                //        src.Append(" }  ");
                //        src.Append(" });");
                //        src.Append(" }; ");

                //        src.Append(" AutoCenter(); ");
                //        src.Append("   </script>  ");

                //        src.Append("</body>");
                //        src.Append("</html>");
                //    }
                //    else
                //    {

                //        PickupPoint = PickupPoint.Replace(" ", "+");
                //        DestinationPoint = DestinationPoint.Replace(" ", "+");
                //        via = via.Replace(" ", "+");

                //        src.Append("<html><head></head><body><iframe" +
                //                           " width=\"960\"" +
                //                           " height=\"710\"" +
                //                           " \frameborder=\"0\" style=\"border:0;margin-left:-10;margin-top:-10;margin-right:-10\"" +
                //                           " src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyAFkZHqTas4EKYEEsk8J3aQh0zQJ-tsWmY&origin=" +
                //                              PickupPoint + "&destination=" + DestinationPoint + "&avoid=tolls|highways" + "\">" +
                //                         "</iframe></body></html>");
                //    }

                //}



                if (via.Length > 0)
                {

                    if (mapApiType == 0 || mapApiType == 1)
                    {

                        src.Append("<!DOCTYPE html><html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" /><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
                        src.Append("<script src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>");
                        src.Append("<script src=\"http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.1.min.js\"></script>");

                        src.Append("</head><body><div id=\"map\" style=\"width: 960px; height: 710px;Left:-10px;Top:-13px\"></div>");

                        src.Append("<script type=\"text/javascript\">");


                        src.Append("var map = new google.maps.Map(document.getElementById('map'), {  zoom: 14,     mapTypeId: google.maps.MapTypeId.ROADMAP,   mapTypeControl: false,   streetViewControl: false,   panControl: false,   zoomControlOptions: {      position: google.maps.ControlPosition.LEFT_BOTTOM    }  }); ");



                        src.Append(" var directionsDisplay= new google.maps.DirectionsRenderer(); ");

                        src.Append(" var directionsService = new google.maps.DirectionsService();");

                        src.Append(" directionsDisplay.setMap(map);");



                        src.Append("function AutoCenter() { ");

                        src.Append("  var start = \"" + PickupPoint + "\";  ");

                        src.Append(" var end = \"" + DestinationPoint + "\"; ");




                        //src.Append("  var start = 'HA2 0DU UK';  ");

                        //src.Append(" var end = 'TW6 2GA UK'; ");

                        src.Append("  var wpoints=[");
                        string appendComma = ",";


                        if (ViaLocs != null)
                        {

                            for (int i = 0; i < ViaLocs.Count(); i++)
                            {

                                if ((i + 1) != ViaLocs.Count())
                                {
                                    appendComma = ",";
                                }
                                else
                                    appendComma = "";
                                src.Append("   { location: \"" + ViaLocs[i] + "\", stopover: false}" + appendComma);
                            }
                        }


                        src.Append("];");



                        src.Append("   var request = {      origin: start,     destination: end, waypoints: wpoints,  optimizeWaypoints: true,  travelMode: google.maps.TravelMode.DRIVING    }; ");



                        src.Append("   directionsService.route(request, function(response, status) ");

                        src.Append("{ ");

                        src.Append(" if (status == google.maps.DirectionsStatus.OK)");

                        src.Append(" { ");

                        src.Append("  directionsDisplay.setDirections(response); ");

                        src.Append(" }  ");
                        src.Append(" });");
                        src.Append(" }; ");


                        src.Append(" AutoCenter(); ");


                        src.Append("   </script>  ");



                        src.Append("</body>");
                        src.Append("</html>");
                    }
                    else
                    {


                        PickupPoint = PickupPoint.Replace(" ", "+");
                        DestinationPoint = DestinationPoint.Replace(" ", "+");
                        via = via.Replace(" ", "+");

                        //src.Append("<iframe" +
                        //                     " width=\"960\"" +
                        //                     " height=\"710\"" +
                        //                     " \frameborder=\"0\" style=\"border:0;margin-left:-10;margin-top:-10;margin-right:-10\"" +
                        //                     " src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyAFkZHqTas4EKYEEsk8J3aQh0zQJ-tsWmY&origin=" +
                        //                        PickupPoint + "&waypoints=" + via + "&destination=" + DestinationPoint + "&avoid=tolls|highways" + "\">" +
                        //                   "</iframe>");


                        src.Append("<html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"></head><body><iframe" +
                                            " width=\"960\"" +
                                            " height=\"710\"" +
                                            " \frameborder=\"0\" style=\"border:0;margin-left:-10;margin-top:-10;margin-right:-10\"" +
                                            " src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyAFkZHqTas4EKYEEsk8J3aQh0zQJ-tsWmY&origin=" +
                                               PickupPoint + "&waypoints=" + via + "&destination=" + DestinationPoint + "&avoid=tolls|highways" + "\">" +
                                          "</iframe>></body></html>");
                    }
                }
                else
                {


                    if (mapApiType == 0 || mapApiType == 1)
                    {

                        src.Append("<!DOCTYPE html><html><head><meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\" /><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">");
                        src.Append("<script src=\"http://maps.google.com/maps/api/js?sensor=false\"></script>");
                        src.Append("<script src=\"http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.1.min.js\"></script>");

                        src.Append("</head><body><div id=\"map\" style=\"width: 960px; height: 710px;Left:-10px;Top:-13px\"></div>");

                        src.Append("<script type=\"text/javascript\">");


                        src.Append("var map = new google.maps.Map(document.getElementById('map'), {  zoom: 14,     mapTypeId: google.maps.MapTypeId.ROADMAP,   mapTypeControl: false,   streetViewControl: false,   panControl: false,   zoomControlOptions: {      position: google.maps.ControlPosition.LEFT_BOTTOM    }  }); ");



                        src.Append(" var directionsDisplay= new google.maps.DirectionsRenderer(); ");

                        src.Append(" var directionsService = new google.maps.DirectionsService();");

                        src.Append(" directionsDisplay.setMap(map);");




                        src.Append("function AutoCenter() { ");


                        src.Append("  var start = \"" + PickupPoint + "\";  ");

                        src.Append(" var end = \"" + DestinationPoint + "\"; ");

                        //  src.Append("  var wpoints=[{ location: \"UB6 0LY, UK\", stopover: false},{ location: \"RH8 9EU, UK\", stopover: false}];");


                        src.Append("   var request = {      origin: start,     destination: end,   travelMode: google.maps.TravelMode.DRIVING    }; ");



                        src.Append("   directionsService.route(request, function(response, status) ");

                        src.Append("{ ");

                        src.Append(" if (status == google.maps.DirectionsStatus.OK)");

                        src.Append(" { ");

                        src.Append("  directionsDisplay.setDirections(response); ");

                        src.Append(" }  ");
                        src.Append(" });");
                        src.Append(" }; ");

                        src.Append(" AutoCenter(); ");
                        src.Append("   </script>  ");

                        src.Append("</body>");
                        src.Append("</html>");
                    }
                    else
                    {

                        PickupPoint = PickupPoint.Replace(" ", "+");
                        DestinationPoint = DestinationPoint.Replace(" ", "+");
                        via = via.Replace(" ", "+");

                        src.Append("<html><head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\"></head><body><iframe" +
                                           " width=\"960\"" +
                                           " height=\"710\"" +
                                           " \frameborder=\"0\" style=\"border:0;margin-left:-10;margin-top:-10;margin-right:-10\"" +
                                           " src=\"https://www.google.com/maps/embed/v1/directions?key=AIzaSyAFkZHqTas4EKYEEsk8J3aQh0zQJ-tsWmY&origin=" +
                                              PickupPoint + "&destination=" + DestinationPoint + "&avoid=tolls|highways" + "\">" +
                                         "</iframe></body></html>");
                    }

                }



                webBrowser1.DocumentText = src.ToStr();
              //  webBrowser1.Refresh();

                // map.Navigate(url);


                //  }

            }
            catch 
            {


            }
        }

     

        //public frmMap(string origin, string[] via, string destination,bool ChangeUri)
        //{


        //    InitializeComponent();
        //    SetFormatting();
        //    webBrowser1.SendToBack();

        //   // CalculateDistance(origin, destination);
        //    General.ShowGoogleMap_RouteDirections(webBrowser1, origin, via, destination, "");
        //    webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);

        //    grdLister.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;


        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn("A", typeof(string)));
        //    DataRow row = dt.NewRow();
        //    row["A"] = origin;


        //    int unicode = 65;
        //    char character = (char)unicode;

        //    for (int i = 0; i < via.Count(); i++)
        //    {
        //        unicode++;
        //        character = (char)unicode;

        //        dt.Columns.Add(new DataColumn(character.ToStr(), typeof(string)));
        //        row[character.ToStr()] = via[i];
        //    }


        //    if (via.Count() == 0)
        //    {
        //        character = (char)66;


        //    }
        //    else
        //    {
        //        unicode++;
        //        character = (char)unicode;


        //    }

        //    dt.Columns.Add(new DataColumn(character.ToStr(), typeof(string)));


        //    dt.Columns.Add(new DataColumn("Distance", typeof(string)));




        //    row[character.ToStr()] = destination;
        //    row["Distance"] = Distance;


        //    dt.Rows.Add(row);
        //    DataView dv = new DataView(dt);
        //    grdLister.DataSource = dv;


        //    grdLister.CurrentRow = null;






        //    //for (int index = 0; index < via.Count(); index++)
        //    //{
        //    //    //row1["To Address"] = via;
        //    //    dt.Rows.Add(index);
        //    //}



        //}


        //int cnt = 1;

        //void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //  //  this.WindowState = FormWindowState.Normal;
        //    //try
        //    //{
        //    //    if (cnt > 1)
        //    //    {

        //    //        HtmlElement loBtn = (HtmlElement)webBrowser1.Document.GetElementById("panelarrow2");
        //    //        loBtn.InvokeMember("click");

        //    //    }
        //    //    cnt++;
        //    //    GetDistance();
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //}
        //}
        //void GetDistance()
        //{
        //    try
        //    {
        //        string innerHtml = webBrowser1.Document.Body.InnerHtml;

        //        string body = webBrowser1.Document.Body.InnerHtml;



        //        string htmlText = webBrowser1.Document.Body.InnerHtml;

        //        HtmlDocument doc = webBrowser1.Document;


        //        int Spoint = htmlText.LastIndexOf("dir_altroutes_header_container") - 120;
        //        int Epoint = htmlText.LastIndexOf("dir_title") - 9;

        //        Epoint = Epoint - Spoint;

        //        string RoutsData = webBrowser1.Document.Body.InnerHtml.Substring(Spoint, Epoint);




        //        int first = RoutsData.ToLower().IndexOf("<div class=\"altroute-rcol altroute-info\">");



        //        if (first == -1)
        //        {
        //            first = RoutsData.ToLower().IndexOf("<div class=altroute-rcol altroute-info>");
        //        }

        //        int last = RoutsData.ToLower().IndexOf("<div class=\"dir-altroute-clear\">");


        //        if (last == -1)
        //        {
        //            last = RoutsData.ToLower().IndexOf("<div class=dir-altroute-clear>");
        //        }

        //        last = last - first;
        //        string newtext = RoutsData.Substring(first, last);


        //        String result = Regex.Replace(newtext, @"<[^>]*>", String.Empty);
        //        String result2 = Regex.Replace(RoutsData, @"<[^>]*>", String.Empty);


        //        result2 = result2.Substring(result2.IndexOf("Suggested routes"), result2.Length - result2.IndexOf("Suggested routes"));


        //        result2 = result2.Replace("Suggested routes", "").Trim();
        //        result2 = result2.Replace("s=dir jstrack=\"1\" oi=\"dir_d\">", "").Trim();
        //        result2 = result2.Replace("bpanel>", "").Trim();

        //        result2 = result2.Replace("        ", "\r\n");

        //        string[] rows = result2.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


        //        //grdLister.CurrentRow.Cells["Id"].Value
        //        if (rows.Length > 0)
        //        {
        //            grdLister.Rows[0].Cells["Distance"].Value = rows[0].ToStr();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        ////private decimal CalculateDistance(string origin,string[] via, string destination)
        //{
        //    decimal miles = 0.00m;

        //    try
        //    {

        //        origin = General.GetPostCodeMatch(origin.ToStr().ToUpper());
        //        destination = General.GetPostCodeMatch(destination.ToStr().ToUpper());


        //        origin += ", UK";
        //        destination += ", UK";

        //        string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + "&sensor=false";



        //       // string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "+UK&destination=" + destination + "+UK&sensor=false";



        //         XmlTextReader reader = new XmlTextReader(url2);
        //        reader.WhitespaceHandling = WhitespaceHandling.Significant;
        //        System.Data.DataSet ds = new System.Data.DataSet();
        //        ds.ReadXml(reader);
        //        DataTable dt = ds.Tables["distance"];

             


        //        if (dt != null)
        //        {
        //            //var rows = dt.Rows.OfType<DataRow>().Where(c => c[0].ToStr().Trim() == c[1].ToStr().Strip("m").Trim()).ToList();

        //            decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
        //            decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

        //            decimal milKM = 0.621m;
        //            decimal milMeter = 0.00062137119m;

        //            miles = (milKM * distanceKm) + (milMeter * distanceMeter);


        //            DataTable dt1 = ds.Tables["duration"];

        //            DataRow row = dt1.Rows.OfType<DataRow>().LastOrDefault();
        //            string time = row.ItemArray[1].ToString();

        //            //lblMiles.Text = " "+ string.Format("{0:#.##}", miles) + " miles. Time :" + time + "";
        //            Distance = " " + string.Format("{0:#.##}", miles) + " miles. Time :" + time + "";
        //        }
        //        else
        //        {

        //            //mileageError = true;

        //        }

        //    }
        //    catch
        //    {

        //        //mileageError = true;
        //    }


        //    return miles;
        //}





        private string CalculateTotalDistance(string origin, string[] via, string destination)
        {

            string distanceString = string.Empty;

            decimal miles = 0.00m;

            try
            {

                if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
                {

                    origin = General.GetPostCodeMatch(origin.ToStr().ToUpper());
                    destination = General.GetPostCodeMatch(destination.ToStr().ToUpper());
                }

                origin += ", UK";
                destination += ", UK";




                string viaP = string.Empty;

                if (via != null && via.Count() > 0)
                {

                    viaP = "&waypoints=";

                    if (AppVars.objPolicyConfiguration.AddFareCalculationType.ToInt() == 1)
                    {
                        viaP += string.Join("|", via.Select(c => General.GetPostCodeMatch(c.ToUpper()) + ", UK").ToArray<string>());
                    }
                    else
                    {
                        viaP += string.Join("|", via.Select(c => c.ToUpper() + ", UK").ToArray<string>());

                    }

                }


                string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "&destination=" + destination + viaP+ "&sensor=false";

                // string url2 = "http://maps.googleapis.com/maps/api/directions/xml?origin=" + origin + "+UK&destination=" + destination + "+UK&sensor=false";
                XmlTextReader reader = new XmlTextReader(url2);
                reader.WhitespaceHandling = WhitespaceHandling.Significant;
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
        

                if (ds.Tables[0].Rows[0]["status"].ToString() == "ZERO_RESULTS" || ds.Tables[0].Rows[0]["status"].ToString() == "INVALID_REQUEST" || ds.Tables[0].Rows[0]["status"].ToString() == "NOT_FOUND")
                {
                    return "";
                }
                else
                {

             

                        string time = string.Empty;

                        if (string.IsNullOrEmpty(viaP))
                        {
                            DataTable dt = ds.Tables["distance"];
                            var rows = dt.Rows.OfType<DataRow>().Where(c => c[0].ToStr().Trim() == c[1].ToStr().Strip("m").Trim()).ToList();

                            decimal distanceKm = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains("km")).Sum(c => c[1].ToStr().Strip("km").Trim().ToDecimal()).ToDecimal() / 2;
                            decimal distanceMeter = dt.Rows.OfType<DataRow>().Where(c => c[1].ToStr().Contains(" m")).Sum(c => c[1].ToStr().Strip("m").Trim().ToDecimal()).ToDecimal() / 2;

                            decimal milKM = 0.621m;
                            decimal milMeter = 0.00062137119m;

                            miles = (milKM * distanceKm) + (milMeter * distanceMeter);

                            dt = ds.Tables["duration"];
                            DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
                            time = row.ItemArray[1].ToString();



                        }
                        else
                        {
                            var milesRows = ds.Tables["distance"].Rows.OfType<DataRow>().Where(c => c[2].ToStr() == string.Empty);

                            decimal distanceKm = 0.00m;
                            decimal distanceMeter = 0.00m;


                            foreach (DataRow row in milesRows)
                            {
                                if (row[1].ToStr().Contains("km"))
                                {
                                    distanceKm = row[1].ToStr().Strip("km").Trim().ToDecimal();
                                    distanceMeter = 0.00m;

                                }
                                else if (row[1].ToStr().Contains("m"))
                                {
                                    distanceKm = 0.00m;
                                    distanceMeter = row[1].ToStr().Strip("m").Trim().ToDecimal();

                                }


                                decimal milKM = 0.621m;
                                decimal milMeter = 0.00062137119m;

                                miles += (milKM * distanceKm) + (milMeter * distanceMeter);

                            //    time = (Math.Round((rows.Sum(c => Convert.ToDouble(c[0])) / 60), 0)).ToStr();
                           //     time += " mins";


                            }


                            miles = Math.Round(miles, 1);
                            
                            
                            var rows = ds.Tables["duration"].Rows.OfType<DataRow>().Where(c => c[2].ToStr() == string.Empty);

                            time = (Math.Round((rows.Sum(c => Convert.ToDouble(c[0])) / 60), 0)).ToStr();
                            time += " mins";

                            // DataRow row = dt.Rows.OfType<DataRow>().LastOrDefault();
                            // time = row.ItemArray[1].ToString();
                        }


                      

                        Distance = "Distance : " + string.Format("{0:#.##}", miles) + " miles. Time :" + time + "";
                    
                }



            }
            catch
            {

                //mileageError = true;
            }


            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            HtmlElement loBtn = (HtmlElement)webBrowser1.Document.GetElementById("panelarrow2");
            loBtn.InvokeMember("click");
          
        }

        private void grdLister_CellFormatting(object sender, CellFormattingEventArgs e)
        {
               e.CellElement.TextWrap = true;                
        }

      

    }
}
