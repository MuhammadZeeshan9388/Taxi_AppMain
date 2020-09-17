using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Utils;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Taxi_AppMain
{
    public partial class frmTrackDriver : Form
    {
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

        System.Windows.Forms.Timer tmr = null;

        public frmTrackDriver()
        {
            InitializeComponent();
            ComboFunctions.FillPDADLoginriverNoCombo(ddl_Driver);
            ddl_Driver.KeyUp += new KeyEventHandler(ddl_Driver_KeyUp);
            ddl_Driver.KeyDown += new KeyEventHandler(ddl_Driver_KeyDown);
            this.ddl_Driver.SelectedValueChanged += new System.EventHandler(this.ddl_Driver_SelectedValueChanged);
            tmr = new System.Windows.Forms.Timer();
           tmr.Interval = 1000;
            tmr.Tick += new EventHandler(tmr_Tick);
            this.ddl_Driver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;

            this.ddl_Driver.TextChanged+=new EventHandler(ddl_Driver_TextChanged);
          //  this.ddl_Driver.TextChanged += new EventHandler(ddl_Driver_TextChanged);
        }

        int cnter = 0;
        void ddl_Driver_TextChanged(object sender, EventArgs e)
        {


            tmr.Start();
            cnter = 0;
           
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            cnter++;

            try
            {

                if ( ddl_Driver.SelectedText.Length>1 && cnter >= 2 && (ddl_Driver.Items.Count(c => c.Text.StartsWith(ddl_Driver.Text)) > 0))
                {
                    ddl_Driver.SelectedItem = ddl_Driver.Items.FirstOrDefault(c => c.Text.StartsWith(ddl_Driver.Text));
                    if (ddl_Driver.SelectedValue != null)
                    {
                        cnter = 0;
                       
                        TrackDriver();
                    }
                }
            }
            catch
            {
                cnter = 0;

            }

        }


      private void DisposeTimer()
    {
        try
        {
            tmr.Stop();
            tmr.Dispose();
            tmr = null;
        }
        catch
        {


        }
    }

        void ddl_Driver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                TrackDriver();
            }
        }

        void ddl_Driver_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                TrackDriver();
            }
        }

        private void btnTrack_Click(object sender, EventArgs e)
        {

            TrackDriver();
        }


        private void TrackDriver()
        {
            try
            {

                if(string.IsNullOrEmpty(AppVars.objPolicyConfiguration.ListenerIP.ToStr()))
                {
                    ENUtils.ShowMessage("Server IP is not configured in Settings.");
                    return;
                }
             
                string driverNo = ddl_Driver.SelectedValue.ToStr().Trim();
                if(string.IsNullOrEmpty(driverNo))
                    return;



                DisposeTimer();

               // Thread smsThread = new Thread(delegate()
               // {
               //     new BroadcasterData().BroadCastToPort("**track driver=" + driverNo+"="+Environment.MachineName,3530);
               //     //if (this.IsDisposed == false)
               //     //{
               //     //    SendMessage("request tracking=" + driverNo);
               //     //}
               // });


               // smsThread.Start();


                     
             
               //System.Threading.Thread.Sleep(1000);
                int driverId = 0;
                

                    using (Taxi_Model.TaxiDataContext db = new Taxi_Model.TaxiDataContext())
                    {
                       driverId= db.Fleet_Drivers.FirstOrDefault(c => c.DriverNo == driverNo && c.IsActive == true).DefaultIfEmpty().Id;
                    if (driverId > 0)
                    {


                        long jobId = db.Fleet_DriverQueueLists.FirstOrDefault(c => c.Status == true && c.DriverId == driverId).DefaultIfEmpty().CurrentJobId.ToLong();


                        if (System.IO.File.Exists(Application.StartupPath + "\\TreasureBooking.exe"))
                        {
                            ClsDataTransfer polObj = new ClsDataTransfer();

                            foreach (System.Reflection.PropertyInfo item in AppVars.objPolicyConfiguration.GetType().GetProperties())
                            {
                                try
                                {

                                    if (polObj.GetType().GetProperty(item.Name) != null)
                                        polObj.GetType().GetProperty(item.Name).SetValue(polObj, item.GetValue(AppVars.objPolicyConfiguration, null), null);
                                }
                                catch
                                {


                                }
                            }


                            polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");
                            string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");
                            string s = "XXX";
                            Process pp = new Process();
                            pp.StartInfo.FileName = Application.StartupPath + "\\TreasureBooking.exe";
                            pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");
                            pp.StartInfo.Arguments += " " + "rptJobRouthPathGoogle" + " " + jobId + " " + "true" + " " + driverId;
                            pp.Start();
                            Thread.Sleep(500);
                            //   pp.WaitForExit();
                        }
                        else
                        {





                            rptJobRouthPathGoogle rpt = new rptJobRouthPathGoogle(jobId > 0 ? db.Bookings.First(c => c.Id == jobId) : null, true, driverId);
                            rpt.Show();
                        }
                    }
                    }
                    //rpt.Dispose();
               
                




                this.Close();

              

             
             }
             catch (Exception ex)
             {
                 //   ENUtils.ShowMessage(ex.Message);
             }

        }

        private void SendMessage(string msg)
        {
            try
            {

                byte[] data = Encoding.UTF8.GetBytes(msg);

               

                TcpClient tcpClient = new TcpClient();

                if (IPAddress.Parse(AppVars.objPolicyConfiguration.ListenerIP.ToStr()) != null)
                    tcpClient.Connect(new IPEndPoint(IPAddress.Parse(AppVars.objPolicyConfiguration.ListenerIP.ToStr()), 1101));
                else
                    tcpClient.Connect(AppVars.objPolicyConfiguration.ListenerIP.ToStr(), 1101);

                
                
                
                tcpClient.SendTimeout = 3000;
                tcpClient.ReceiveTimeout = 3000;
                tcpClient.GetStream().Write(data, 0, data.Length);

                tcpClient.Close();
            }
            catch (Exception ex)
            {


            }
        }

        private void frmTrackDriver_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
          
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //SendMessage("request tracking= ");  
            General.SendMessageToPDA("request tracking= ");
        }

        private void ddl_Driver_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }
    }
}
