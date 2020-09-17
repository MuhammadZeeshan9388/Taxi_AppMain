using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Taxi_Model;
using System.Runtime.InteropServices;

namespace Taxi_AppMain.Forms
{
    public partial class frmStreetView : Form
    {
        private int _DriverId;

        public int DriverId
        {
            get { return _DriverId; }
            set { _DriverId = value; }
        }




        private double _Latitude;

        public double Latitude
        {
            get { return _Latitude; }
            set { _Latitude = value; }
        }
        private double _Longitude;

        public double Longitude
        {
            get { return _Longitude; }
            set { _Longitude = value; }
        }


        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(String lpClassName, String lpWindowName);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void bringToFront(string title)
        {
            // Get a handle to the Calculator application.
            IntPtr handle = FindWindow(null, title);

            // Verify that Calculator is a running process.
            if (handle == IntPtr.Zero)
            {
                return;
            }

            // Make Calculator the foreground application
            SetForegroundWindow(handle);
        }




        public frmStreetView(int driverId,Fleet_Driver_Location objLoc)
        {
            InitializeComponent();
           
            this.DriverId = driverId;
            timer1.Tick += new EventHandler(timer1_Tick);
            this.FormClosing += new FormClosingEventHandler(frmStreetView_FormClosing);
            RefreshView(objLoc);
            this.KeyDown += new KeyEventHandler(frmStreetView_KeyDown);
      
            this.Shown += new EventHandler(frmStreetView_Shown);
        }

        void frmStreetView_Shown(object sender, EventArgs e)
        {
            try
            {
                SetForegroundWindow(this.Handle);
            }
            catch (Exception ex)
            {


            }
        }

        void frmStreetView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();

            }
        }

        void frmStreetView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                timer1.Stop();
                timer1.Dispose();

                webBrowser1.Visible = false;
                this.WindowState = FormWindowState.Minimized;

                webBrowser1.Controls.Clear();
                webBrowser1.Dispose();


              //  dataGridView1.Font.Dispose();
               // dataGridView1.Dispose();
                this.Dispose(true);
                GC.Collect();
            }
            catch (Exception ex)
            {


            }
        }


        public void SetLocation(Fleet_Driver_Location objLoc)
        {
            RefreshView(objLoc);

        }

        void timer1_Tick(object sender, EventArgs e)
        {

            RefreshView(GetLocation());

        }

        private Fleet_Driver_Location GetLocation()
        {
            return General.GetObject<Fleet_Driver_Location>(c => c.DriverId == this.DriverId && c.UpdateDate.Date == DateTime.Now.Date);

        }

        private void RefreshView(Fleet_Driver_Location objLocation)
        {

            try
            {


                if (objLocation == null || objLocation.Latitude==Latitude)
                    return;


                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add();

                dataGridView1.Rows[0].Cells["Driver"].Value = objLocation.Fleet_Driver.DriverNo;
                dataGridView1.Rows[0].Cells["Location"].Value = objLocation.LocationName;
                dataGridView1.Rows[0].Cells["colDateTime"].Value =string.Format("{0:dd/MM/yyyy HH:mm:ss}",objLocation.UpdateDate);
                dataGridView1.Rows[0].Cells["Speed"].Value =Math.Round(objLocation.Speed,2);

                Latitude = objLocation.Latitude;
                Longitude = objLocation.Longitude;

                string text = "<!DOCTYPE html>" +
                       "<html>" +
                        "<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">" +
                     "<meta charset=\"utf-8\">" +
                     "<link href=\"/maps/documentation/javascript/examples/default.css\" rel=\"stylesheet\">" +
                    "<script src=\"https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false\"></script>" +
                    "<script>" +
                       "function initialize() {" +
                         "var fenway = new google.maps.LatLng(" + Latitude + "," + Longitude + ");" +
                         "var mapOptions = {" +
                           "center: fenway," +
                           "zoom: 14," +
                           "mapTypeId: google.maps.MapTypeId.ROADMAP" +
                         "};" +
                         "var map = new google.maps.Map(" +
                             "document.getElementById('map-canvas'), mapOptions);" +
                         "var panoramaOptions = {" +
                           "position: fenway," +
                           "pov: {" +
                             "heading: 34," +
                             "pitch: 10" +
                           "}" +
                        "};" +

                          "var panorama = new  google.maps.StreetViewPanorama(document.getElementById('pano'),panoramaOptions);" +
                          "map.setStreetView(panorama);" +
                       "}" +

                       "google.maps.event.addDomListener(window, 'load', initialize);" +

                   "</script>" +
                 "</head>" +
                " <body>" +
                   "<div id=\"map-canvas\" style=\"display:none\"></div>" +
                   "<div id=\"pano\" style=\"position:absolute;  top: 1px;left:1px; width: 1020px; height: 760px;\"></div>" +
                " </body>" +
              " </html>";





                webBrowser1.DocumentText = @text;
            }
            catch (Exception ex)
            {


            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    RefreshView(GetLocation());

                }
            }
            catch (Exception ex)
            {


            }
        }
    }
}
