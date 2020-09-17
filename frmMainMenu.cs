using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using UI;
using Taxi_Model;
using Taxi_BLL;
using Telerik.WinControls.UI;
using CallerIdData;
using Sipek.Common;
using Sipek.Common.CallControl;
using System.Diagnostics;
using System.Threading;
using System.Data.Linq;
using System.IO.Ports;

using System.Net;
using System.Xml.Linq;
using System.IO;
using System.Runtime.InteropServices;
using Taxi_AppMain.Forms;
using Telerik.WinControls.UI.Docking;
using System.Xml;
using System.Net.NetworkInformation;
using Telerik.WinControls;

using Asterisk.NET.Manager;
using Asterisk.NET.Manager.Event;
using Taxi_AppMain.Classes;
using System.Net.Sockets;
using System.Collections;
using System.Text.RegularExpressions;
using DotNetCoords;

using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using System.Net.Http;

namespace Taxi_AppMain
{
    public enum eCallerIdType { Analog = 1, TAPI_Digital = 2, VOIP_SIP = 3, VOIP_TAPI = 4, FILE_CLI = 5, VOIP_ASTERISK, VOIPBT = 7, VOIP_TAPI_CTI = 8 };


    public struct RefreshTypes
    {
        public static string DESPATCH_TERMINAL = "despatch terminal";

        public static string REFRESH_MANUALLOGINDESPATCHJOB = "refresh manuallogindespatchjob";
        public static string REFRESH_SAVEQUOTATION = "refresh savequotation";

        public static string REFRESH_DESPATCHJOB = "refresh despatchjob";
        public static string REFRESH_ALLOCATEDRIVER = "refresh allocatedriver";
        public static string REFRESH_HOLDANDRELEASE = "refresh holdandrelease";


        public static string REFRESH_DASHBOARD_DRIVER = "refresh dashboard drivers";

        public static string REFRESH_DASHBOARD = "refresh dashboard";
        public static string REFRESH_ONLY_DASHBOARD = "refresh only dashboard";
       

        public static string REFRESH_ACTIVE_DASHBOARD = "refresh active dashboard";
        public static string REFRESH_REQUIRED_DASHBOARD = "refresh required dashboard";
        public static string REFRESH_ACTIVEBOOKINGS_DASHBOARD = "refresh active booking dashboard";
        public static string REFRESH_SERACTIVEBOOKINGS_DASHBOARD = "refresh seractive booking dashboard";
        public static string REFRESH_SAVEPREBOOKINGS_DASHBOARD = "refresh save prebooking dashboard";
        public static string REFRESH_CANCELBOOKING = "refresh cancelbooking";

        public static string REFRESH_BOOKING_DASHBOARD = "refresh booking dashboard";
        public static string REFRESH_WAITING_AND_DASBOARD = "refresh waiting and dasboard";

        public static string REFRESH_TODAY_AND_PREBOOKING_DASHBOARD = "refresh today and prebooking dashboard";


        public static string REFRESH_WEBBOOKINGS_DASHBOARD = "refresh webbookings dashboard";
        public static string REFRESH_DECLINEDWEBBOOKINGS_DASHBOARD = "refresh declined webbookings dashboard";


        public static string REFRESH_BOOKINGHISTORY_DASHBOARD = "refresh bookinghistory dashboard";


        public static string INCOMING_CALL = "incoming call";

        public static string JOB_LATE = "joblate=";
        public static string SMS = "sms=";
        public static string REFRESH_PLOTS = "refresh plots";
        public static string AUTHORIZE_WEBBOOKING = "authorize web";


    }



    public partial class frmMainMenu : UI.MainMenu
    {


        //   private List<Gen_SubCompany_ContactsNo> listOfSubCompanyContacts = null;
        private ManagerConnection manager = null;


        private string fileCLIDirPath;
        private bool IsFileCLI;
        public BroadCastMessage objAnalog = null;
        //    private DateTime? LastControllerActivityTime = null;

        System.Timers.Timer t2 = new System.Timers.Timer();


        private static string _acknowledgement;
        private bool _ShowAllBookings;

        public bool ShowAllBookings
        {
            get { return _ShowAllBookings; }
            set { _ShowAllBookings = value; }
        }
        private bool _ShowAllDrivers;

        public bool ShowAllDrivers
        {
            get { return _ShowAllDrivers; }
            set { _ShowAllDrivers = value; }
        }


        private bool _ShowBookingFilter;

        public bool ShowBookingFilter
        {
            get { return _ShowBookingFilter; }
            set { _ShowBookingFilter = value; }
        }


        private bool _ShowDriverFilter;

        public bool ShowDriverFilter
        {
            get { return _ShowDriverFilter; }
            set { _ShowDriverFilter = value; }
        }

        public static string Acknowledgement
        {
            get { return _acknowledgement; }
            set { _acknowledgement = value; }
        }

        //Type objClassType = null;
        //MapPoint.Application objApp = null;
        //MapPoint.Map objPDAMap;

        public HubConnection Connection { get; set; }
        public IHubProxy HubProxy { get; set; }
        public  string ServerURI;
        //   public string ServerURI = "http://localhost:55274";

        private bool EnableAutoRefresh = true;

        public frmMainMenu()
        {

            InitializeComponent();


            this.IsTrueStructureMenu = false;
            this.toolWindowLeft.Hide();

            objAnalog = new BroadCastMessage();
            objAnalog.AutoRefreshMessage += new BroadCastMessage.AutoRefreshDataHandler(objAnalog_AutoRefreshMessage);

            this.KeyDown += new KeyEventHandler(frmMainMenu_KeyDown);
            this.KeyUp += new KeyEventHandler(frmMainMenu_KeyUp);
            this.MinimizeBox = false;

            this.radMenu1.Font = new Font("Tahoma", 9, FontStyle.Regular);
            this.Load += new EventHandler(frmMainMenu_Load);
            this.TextChanged += new EventHandler(frmMainMenu_TextChanged);


           
            // File.AppendAllText("log.txt", "onmainmenucontructor" + Environment.NewLine);
        }

       

      

        private bool IsConnected = false;

        private async void ConnectAsync()
        {
            try
            {
                Dictionary<string, string> QuerystringData = new Dictionary<string, string>();

                QuerystringData.Add("SignalRClientsType", "2");
                QuerystringData.Add("SignalRUserType", "2");
                QuerystringData.Add("SignalRClientDomainId", AppVars.LoginObj.LsessionId.ToStr());

                Connection = new HubConnection(ServerURI, QuerystringData);
                // Connection.Protocol= Connection.ve
                Connection.TransportConnectTimeout = new TimeSpan(8, 0, 0);
                HubProxy = Connection.CreateHubProxy("DispatchHub");

                HubProxy.On<string>("cMessageToDesktop", (message) =>
                    this.Invoke((Action)(() => WritePDAMsg(message)))
                );


                await Connection.Start();
                IsConnected = true;
            }
            catch (Exception ex)
            {
                File.AppendAllText(Application.StartupPath+ "\\log.txt", DateTime.Now.ToStr() + ": Exception: " + ex.Message + " Inner Exception: " + ex.InnerException + Environment.NewLine);
            }
        }

        private void WritePDAMsg(string msg)
        {
            RecordDisplay(msg);
            //
        }

        private void RecordDisplay(string message)
        {
            if (message == "ok")
                Acknowledgement = message;
            else if (message.StartsWith("exceptionOccured"))
            {
                File.AppendAllText("log.txt", DateTime.Now.ToStr() + ": Exception: " + message.Replace("exceptionOccured", "") + " Inner Exception: " + message.Replace("exceptionOccured", "") + Environment.NewLine);
            }
            else if (message.StartsWith("**") || message.StartsWith("refresh") || message.StartsWith("sms=") || message.StartsWith("joblate="))
            {
                message = message.Replace("**", "").Trim();
                objAnalog_AutoRefreshMessage(message);
            }

            else
            {
                message = message.Replace("EXTERNAL", "");
                //message=message=message.Replace("$","").Trim();
                message = message.Substring(message.IndexOf("$"));

                string[] arr = message.Split(' ').Where(c => c.Length > 0).ToArray<string>();
                string line = arr[0].Replace("$", "").Trim();
                string phoneNumber = arr[arr.Count() - 1];

                if (phoneNumber.Length > 4)
                {
                    obj_ReceiveData(line, "", phoneNumber);
                }
            }
        }

        void frmMainMenu_TextChanged(object sender, EventArgs e)
        {

            this.StatusStrip_Label3.Text = this.Text;
        }




        void frmMainMenu_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {

                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).SetFocusOnPendingGrid();

                }
            }
            catch (Exception ex)
            {


            }

        }

        void objAnalog_AutoRefreshMessage(string message)
        {

            try
            {

                if (message.Equals(RefreshTypes.REFRESH_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshDashBoard), null);
                    }
                    else
                    {
                        RefreshDashBoard();
                    }
                }
                else if (message.Equals(RefreshTypes.REFRESH_DASHBOARD_DRIVER))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshDashboardDrivers), null);
                    }
                    else
                    {

                        RefreshDashboardDrivers();
                    }
                }
                else if (message.Equals(RefreshTypes.REFRESH_ONLY_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshOnlyDashBoard), null);
                    }
                    else
                    {

                        RefreshOnlyDashBoard();
                    }
                }
                else if (message.Equals(RefreshTypes.REFRESH_ACTIVE_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshActiveDashBoard), null);
                    }
                    else
                    {

                        RefreshActiveDashBoard();
                    }
                }

                else if (message.StartsWith(RefreshTypes.REFRESH_MANUALLOGINDESPATCHJOB + ">>"))
                {




                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new SingleDelegate(RefreshManualLoginDespatchBooking), new object[] { message });
                    }
                    else
                    {

                        RefreshManualLoginDespatchBooking(message);
                    }
                }



                else if (message.StartsWith(RefreshTypes.REFRESH_DESPATCHJOB+">>"))
                {




                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new SingleDelegate(RefreshDespatchBooking), new object[] { message });
                    }
                    else
                    {

                        RefreshDespatchBooking(message);

                    }
                }
                else if (message.StartsWith(RefreshTypes.REFRESH_SAVEQUOTATION))
                {




                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new SingleDelegate(RefreshQuotationOnDemand), new object[] { message });
                    }
                    else
                    {

                        RefreshQuotationOnDemand(message);
                    }
                }

                else if (message.StartsWith(RefreshTypes.REFRESH_ALLOCATEDRIVER + ">>"))
                {




                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new SingleDelegate(RefreshAllocateBooking), new object[] { message });
                    }
                    else
                    {

                        RefreshAllocateBooking(message);
                    }
                }

                else if (message.Equals(RefreshTypes.REFRESH_ACTIVEBOOKINGS_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshActiveBookingDashBoard), null);
                    }
                    else
                    {

                        RefreshActiveBookingDashBoard();
                    }
                }
                else if (message.StartsWith(RefreshTypes.REFRESH_SERACTIVEBOOKINGS_DASHBOARD))
                {

                   
                    


                        if (this.InvokeRequired)
                        {

                            this.BeginInvoke(new SingleDelegate(RefreshSerActiveBookingDashBoard), message);
                        }
                        else
                        {

                            RefreshSerActiveBookingDashBoard(message);
                        }
                   
                }
                else if (message.StartsWith(RefreshTypes.REFRESH_SAVEPREBOOKINGS_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new SingleDelegate(RefreshSavePreBookingDashBoard), message);
                    }
                    else
                    {

                        RefreshSavePreBookingDashBoard(message);
                    }
                }
                else if (message.StartsWith(RefreshTypes.REFRESH_CANCELBOOKING))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new SingleDelegate(RefreshCancelBookingDashBoard), message);
                    }
                    else
                    {

                        RefreshCancelBookingDashBoard(message);
                    }
                }
                else if (message.Equals(RefreshTypes.REFRESH_REQUIRED_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshRequiredDashBoard), null);
                    }
                    else
                    {

                        RefreshRequiredDashBoard();
                    }
                }
                else if (message.Equals(RefreshTypes.REFRESH_BOOKING_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshDashBoardBookings), null);
                    }
                    else
                    {

                        RefreshDashBoardBookings();
                    }
                }
                else if (message.StartsWith(RefreshTypes.REFRESH_HOLDANDRELEASE + ">>"))
                {




                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new SingleDelegate(RefreshHoldAndReleaseBooking), new object[] { message });
                    }
                    else
                    {

                        RefreshHoldAndReleaseBooking(message);
                    }
                }


                else if (message.Equals(RefreshTypes.REFRESH_WAITING_AND_DASBOARD))
                {
                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshDashboardDrivers), null);
                    }
                    else
                    {

                        RefreshDashboardDrivers();
                    }


                    //if (this.InvokeRequired)
                    //{

                    //    this.BeginInvoke(new Refreshdelegate(RefreshWaitingDrivers), null);
                    //}
                    //else
                    //{

                    //    RefreshWaitingDrivers();
                    //}
                }
                else if (message.Equals(RefreshTypes.REFRESH_TODAY_AND_PREBOOKING_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshTodayAnPreDashboard), null);
                    }
                    else
                    {

                        RefreshTodayAnPreDashboard();
                    }
                }
                else if (message.Equals(RefreshTypes.REFRESH_PLOTS))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshPlots), null);
                    }
                    else
                    {

                        RefreshPlots();
                    }
                }


                else if (message.Equals(RefreshTypes.REFRESH_WEBBOOKINGS_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshWebBookings), null);
                    }
                    else
                    {

                        RefreshWebBookings();
                    }
                }
                else if (message.Equals(RefreshTypes.REFRESH_DECLINEDWEBBOOKINGS_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshDeclinedWebBookings), null);
                    }
                    else
                    {

                        RefreshDeclinedWebBookings();
                    }
                }

                else if (message.Equals(RefreshTypes.REFRESH_BOOKINGHISTORY_DASHBOARD))
                {



                    if (this.InvokeRequired)
                    {

                        this.BeginInvoke(new Refreshdelegate(RefreshDashboardBookingHistory), null);
                    }
                    else
                    {

                        RefreshDashboardBookingHistory();
                    }
                }

                else if (message.StartsWith(RefreshTypes.JOB_LATE))
                {


                    string[] values = message.Split(new char[] { '=' });

                    string content = "Pickup : " + values[4] + Environment.NewLine + "Destination : " + values[5] + Environment.NewLine + "Time : " + values[6].ToStr()
                                     + Environment.NewLine + "Driver : " + values[3].ToStr();

                    MethodInvoker mi = new MethodInvoker(delegate() { this.CreateAndShowAlert("Job is Late! Driver has not Arrived yet", content, null, System.Media.SystemSounds.Asterisk, false, "", "", ""); });
                    this.Invoke(mi);


                    if (IsServer)
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.stp_BookingLog(values[1].ToLong(), this.ObjLoginUser.UserName.ToStr(), "Job is Late! Driver (" + values[3].ToStr() + ") has not arrived yet " + string.Format("{0:hh:mm:ss}", DateTime.Now));
                        }

                        // (new TaxiDataContext()).stp_SendMessage(values[1].ToInt(), AppVars.LoginObj.LuserId.ToInt(), values[3].ToStr(), "", values[4].ToStr(), values[5].ToStr());
                       // (new TaxiDataContext()).stp_BookingLog(values[1].ToLong(), this.ObjLoginUser.UserName.ToStr(), "Job is Late! Driver (" + values[3].ToStr() + ") has not arrived yet " + string.Format("{0:hh:mm:ss}", DateTime.Now));
                    }

                }

                else if (message.StartsWith(RefreshTypes.SMS))
                {
                    if (IsServer && AppVars.enableSMSService==true )
                    {


                        string[] values = message.Split(new char[] { '=' });

                        if (values.Count() == 6)
                        {



                            new Thread(delegate()
                            {
                                SendSMS(values[4].ToStr().Trim(), values[5].ToStr().Trim());

                            }).Start();




                            MethodInvoker mi = new MethodInvoker(delegate() { this.CreateAndShowAlert("Driver(" + values[3].ToStr() + ") To Customer(" + values[4].ToStr() + ") Conversation", values[5].ToStr(), null, System.Media.SystemSounds.Asterisk, false, "", "", ""); });
                            this.Invoke(mi);
                            (new TaxiDataContext()).stp_BookingLog(values[1].ToLong(), this.ObjLoginUser.UserName.ToStr(), values[5].ToStr());
                        }
                    }
                }

                else if (message.StartsWith(RefreshTypes.DESPATCH_TERMINAL))
                {



                    if (IsServer && AppVars.enableSMSService == true)
                    {


                        try
                        {
                            string text = message;

                            string[] valu = new string[6];

                            valu = text.Split(new string[] { ">>" }, StringSplitOptions.None).ToArray<string>();


                            if (valu.Count() >= 3)
                            {
                              


                               
                                new Thread(delegate()
                                {
                                    
                                    SendSMS(valu[2].ToStr().Trim(), valu[3].ToStr());

                                }).Start();

                            }
                        }
                        catch (Exception ex)
                        {

                            MethodInvoker mi2 = new MethodInvoker(delegate() { this.CreateAndShowAlert("Message Failed", "Failed", null, System.Media.SystemSounds.Asterisk, false, "", "", ""); });
                            this.Invoke(mi2);
                        }
                    }


                }
                else
                {
                    if (message.ToStr().StartsWith("incomingcall"))
                    {

                        string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);

                        string number = values[1].ToStr();
                        string line = values[2].ToStr();


                        string machineName = Environment.MachineName.ToStr();

                        if (ListOfExtensions != null && ListOfExtensions.Count(c => c.UserMachineName.ToStr().ToLower() == machineName.ToStr().ToLower() && c.CLIExtension == line.ToStr()) > 0)
                        {
                            if (this.InvokeRequired)
                            {


                                Record_delegate d = new Record_delegate(RecordDisplay);
                                this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI, line, "", number, line });

                            }
                            else
                            {

                                RecordDisplay(eCallerIdType.VOIP_TAPI, line, "", number, line);

                            }

                        }
                    }
                    else if (message.ToStr().StartsWith("cti_incomingcall"))
                    {

                        string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);

                        string number = values[1].ToStr();
                        string line = values[2].ToStr();
                        string callRefNo = "";
                        string calledNumber = string.Empty;

                        if (values.Count() >= 7)
                        {
                            calledNumber = values[6].ToStr();

                            try
                            {
                                callRefNo = values[7].ToStr();

                            }
                            catch
                            {


                            }
                        }

                        //try
                        //{

                        //    File.AppendAllText(Application.StartupPath + "\\log_cti_incomingcall.txt", DateTime.Now + " : " + message.ToStr() + Environment.NewLine);
                        //}
                        //catch
                        //{


                        //}


                        if (PopupOnAnswerCTE)
                        {

                            string machineName = Environment.MachineName.ToStr();

                            if ( ListOfExtensions != null && ListOfExtensions.Count(c => c.UserMachineName.ToStr().ToLower() == machineName.ToStr().ToLower() && line.ToStr().Contains(c.CLIExtension) == true) > 0)
                            {
                                if (this.InvokeRequired)
                                {

                                    Record_delegate d = new Record_delegate(RecordDisplay);
                                    this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI_CTI, callRefNo, calledNumber, number, line });

                                }
                                else
                                {

                                    RecordDisplay(eCallerIdType.VOIP_TAPI_CTI, callRefNo, calledNumber, number, line);

                                }

                            }
                            else
                            {

                                if (objCallerId.ReceiveVOIPCallFromPhone.ToBool())
                                {

                                    if (ListOfExtensions.Count == 0 || ListOfExtensions.Count(c => c.UserId == 0) > 0)
                                    {
                                        ListOfExtensions.Clear();

                                        foreach (var item in General.GetQueryable<UM_UserExtension>(c => c.UserId == AppVars.LoginObj.LuserId))
                                        {
                                            ListOfExtensions.Add(new ClsCallerIdExtensions { UserId = item.UserId.ToInt(), CLIExtension = item.UserExtension.ToStr().Trim() });

                                        }

                                        if (ListOfExtensions.Count == 0)
                                        {
                                            ListOfExtensions.Add(new ClsCallerIdExtensions { UserId = AppVars.LoginObj.LuserId.ToInt(), CLIExtension = "XXX" });

                                        }
                                    }


                                    if (ListOfExtensions != null && ListOfExtensions.Count(c => c.UserId == AppVars.LoginObj.LuserId.ToInt() && line.ToStr().Contains(c.CLIExtension) == true) > 0)
                                    {

                                        if (this.InvokeRequired)
                                        {

                                            Record_delegate d = new Record_delegate(RecordDisplay);
                                            this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI_CTI, callRefNo, calledNumber, number, line });

                                        }
                                        else
                                        {

                                            RecordDisplay(eCallerIdType.VOIP_TAPI_CTI, callRefNo, calledNumber, number, line);

                                        }
                                    }
                                }




                            }
                        }
                        else
                        {

                            string machine = values[4].ToStr().ToLower().Trim();

                            if (Environment.MachineName.ToStr().ToLower().Trim() != machine)
                            {

                                if (this.InvokeRequired)
                                {


                                    Record_delegate d = new Record_delegate(RecordDisplay);
                                    this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI_CTI, line, "", number, line });

                                }
                                else
                                {

                                    RecordDisplay(eCallerIdType.VOIP_TAPI_CTI, line, "", number, line);

                                }
                            }


                        }
                    }
                    else if (message.ToStr().StartsWith("cti_remoteincomingcall"))
                    {

                        string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);

                        string number = values[1].ToStr();
                        string line = values[2].ToStr();
                        string callType = values[3].ToStr();
                        string itemorMachineName = values[4].ToStr();
                        string vpnMachine = string.Empty;


                        if (values.Count() >= 6)
                        {
                            vpnMachine = values[5].ToStr().Trim();
                        }


                        if (callType == "ring")
                        {
                            if (line == "XXX")
                            {
                                  if (this.InvokeRequired)
                                  {

                                      this.BeginInvoke(new SingleDelegate(AddFileCliCall), new object[] { itemorMachineName.ToStr() });
                                  }
                                  else
                                  {

                                      AddFileCliCall(itemorMachineName.ToStr());
                                  }


                            }

                            //if ((IsServer && vpnMachine == "vpn") || IsServer == false)
                            //{

                            //    if (IsServer == false || itemorMachineName == "XXX")
                            //    {

                            //        string name = GetCustomerNameFromCall(number);

                            //        itemorMachineName = number + "-" + string.Format("{0:HH:mm}", DateTime.Now);

                            //        if (!string.IsNullOrEmpty(name))
                            //        {

                            //            itemorMachineName = name + " - " + number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                            //        }
                            //        else
                            //        {
                            //            itemorMachineName = number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                            //        }

                            //        if (this.InvokeRequired)
                            //        {

                            //            this.BeginInvoke(new SingleDelegate(AddFileCliCall), new object[] { itemorMachineName.ToStr() });
                            //        }
                            //        else
                            //        {

                            //            AddFileCliCall(itemorMachineName.ToStr());
                            //        }


                            //        if (IsServer && IsSingleMachineCTI && vpnMachine == "vpn")
                            //        {

                            //            new BroadcasterData().BroadCastToAll("**cti_remoteincomingcall>>" + number + ">>" + "XXX" + ">>ring>>" + itemorMachineName);
                            //            CreateLog(name, number, DateTime.Now, "00:00:00", line,"");

                            //        }

                            //    }

                            //}


                        }
                        else if (callType == "answer")
                        {


                            // string machineName = Environment.MachineName.ToStr();

                            if (ListOfExtensions != null && ListOfExtensions.Count(c => line.ToStr().Contains(c.CLIExtension) == true) > 0)
                            {
                                if (this.InvokeRequired)
                                {

                                    Record_delegate d = new Record_delegate(RecordDisplay);
                                    this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI_CTI, line, "", number, line });

                                }
                                else
                                {

                                    RecordDisplay(eCallerIdType.VOIP_TAPI_CTI, line, "", number, line);

                                }


                                if (IsServer == false && IsSingleMachineCTI == false)
                                {

                                    UpdateLog("", number, DateTime.Now, "00:00:00", line, line, "ANSWER");
                                }

                            }
                            else
                            {

                                if (IsServer && IsSingleMachineCTI && vpnMachine == "vpn")
                                {
                                    new BroadcasterData().BroadCastToAll("**cti_incomingcall>>" + number.ToStr() + ">>" + line.ToStr().Trim() + ">>answer>>" + Environment.MachineName.ToStr().ToLower());


                                }

                            }

                            if (IsServer && IsSingleMachineCTI && vpnMachine == "vpn")
                            {

                                if (itemorMachineName == "XXX")
                                {

                                    UpdateLog(GetCustomerNameFromCall(number), number, DateTime.Now, "00:00:00", line, line, "ANSWER");

                                }
                                else
                                {

                                    CreateVPNCTICallLog(number, line);

                                }



                            }

                        }





                    }
                    else if (message.ToStr().StartsWith("internalmessage"))
                    {

                        string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);

                        if (values[1].ToStr().StartsWith("close clipopup") )
                        {
                            if (values[3].ToStr().ToLower() != Environment.MachineName.ToLower())
                            {

                                var frms = Application.OpenForms.Cast<Form>().Where(c => c.Name.Equals("CallerIdPopup"));
                                foreach (Form frm in frms)
                                {

                                    if ((frm as CallerIdPopup).CallerIdNumber == values[2].ToStr())
                                    {
                                        (frm as CallerIdPopup).StartAutoCloseTimer();

                                    }
                                }
                            }

                        } 
                        else if (values[1].ToStr().StartsWith("request canceljob"))
                        {


                            if (this.InvokeRequired)
                            {

                                this.BeginInvoke(new SingleDelegate(ShowCancelBookingByCustomerForm), message);
                            }
                            else
                            {

                                ShowCancelBookingByCustomerForm(message);

                            }                              

                        }


                        else if (values[1].ToStr().StartsWith("request updatejob"))
                        {


                            if (this.InvokeRequired)
                            {

                                this.BeginInvoke(new SingleDelegate(ShowUpdateBookingByCustomerForm), message);
                            }
                            else
                            {

                                ShowUpdateBookingByCustomerForm(message);

                            }

                        }
                        else if (values[1].ToStr().StartsWith("received text"))
                        {

                            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).ReceivedText(values[2].ToStr(), values[3].ToStr());

                            
                        }
                        else if (values[1].ToStr().StartsWith("ivrnotification"))
                        {
                            try
                            {
                              


                                IVRNotificationClient objIVR = Newtonsoft.Json.JsonConvert.DeserializeObject<IVRNotificationClient>(values[2]);
                            
                                string notificationmsg = objIVR.NotificationMessage.ToStr();

                                IVRNotification objnotify = Newtonsoft.Json.JsonConvert.DeserializeObject<IVRNotification>(notificationmsg);

                                

                                string content = "<html><span><color=Black><b>Ref #:</b>" + objnotify.BookingNumber.ToStr() + Environment.NewLine +
                                                     "<b>Customer :</b>" + objnotify.Name.ToStr() + "(" + objnotify.Phone.ToStr() + ")" + Environment.NewLine +
                                                      "<b>Pickup :</b>" + objnotify.PickUp + Environment.NewLine +
                                                      "<b>Pickup Date/Time :</b>" + objnotify.PickUpDateTime +

                                                  "</span></html>";

                                MethodInvoker mi = new MethodInvoker(delegate()
                                    {
                                        ShowNotification("Booking created from IVR", content, objnotify.BookingId);
                                
                                });
                                this.Invoke(mi);


                                if (EnableAutoRefresh)
                                {

                                    if (this.InvokeRequired)
                                    {
                                        DateTime dtX;

                                        if (DateTime.TryParse(objnotify.PickUpDateTime, out dtX))
                                        {
                                            if (objnotify.PickUpDateTime.ToDate() == DateTime.Now.ToDate())
                                            {
                                                //if (values.Count() >= 4)
                                                //{
                                                //   this.BeginInvoke(new SingleDelegate(RefreshSerActiveBookingDashBoard), RefreshTypes.REFRESH_SERACTIVEBOOKINGS_DASHBOARD + ">>>"+values[3].ToStr());

                                                //}
                                                //else
                                                //{
                                                this.BeginInvoke(new Refreshdelegate(RefreshTodaysBookingsDashboard), null);
                                                // }
                                                try
                                                {

                                                    File.AppendAllText(Application.StartupPath + "\\ivrlogs.txt", DateTime.Now.ToStr() + Environment.NewLine);
                                                }
                                                catch
                                                {

                                                }
                                            }
                                            else
                                            {
                                                //if (values.Count() >= 4)
                                                //{
                                                //    this.BeginInvoke(new SingleDelegate(RefreshSavePreBookingDashBoard), RefreshTypes.REFRESH_SAVEPREBOOKINGS_DASHBOARD + ">>>" + values[3].ToStr());

                                                //}
                                                //else
                                                //{
                                                this.BeginInvoke(new Refreshdelegate(RefreshTodayAnPreDashboard), null);
                                                //  }
                                            }


                                        }
                                        else
                                        {


                                            this.BeginInvoke(new Refreshdelegate(RefreshTodayAnPreDashboard), null);
                                        }
                                    }
                                    else
                                    {

                                        DateTime dtX;

                                        if (DateTime.TryParse(objnotify.PickUpDateTime, out dtX))
                                        {
                                            if (objnotify.PickUpDateTime.ToDate() == DateTime.Now.ToDate())
                                            {
                                                RefreshTodaysBookingsDashboard();

                                            }
                                            else
                                            {
                                                RefreshTodayAnPreDashboard();
                                            }
                                        }
                                        else
                                        {
                                            RefreshTodayAnPreDashboard();
                                        }
                                    }
                                }

                               
                            }
                            catch(Exception ex)
                            {

                                try
                                {
                                    MethodInvoker mi = new MethodInvoker(delegate ()
                                    {
                                        ShowNotification("Booking created from IVR", "",null);

                                    });
                                    this.Invoke(mi);


                                    if (this.InvokeRequired)
                                    {

                                        this.BeginInvoke(new Refreshdelegate(RefreshTodayAnPreDashboard), null);
                                    }
                                    else
                                    {

                                        RefreshTodayAnPreDashboard();
                                    }
                                }
                                catch
                                {


                                }

                            }

                        }
                        else if (values[1].ToStr() == "auth allocateddrvautodespatch")
                        {
                            string jobIds = values[2].ToStr();

                            if (this.InvokeRequired)
                            {
                                appendTextCallback d = new appendTextCallback(ShowAuthAllocDrv);
                                this.BeginInvoke(d, jobIds);
                                
                            }
                            else
                            {

                                ShowAuthAllocDrv(ref jobIds);
                            }                          

                          

                        }
                        else if (values[1].ToStr().StartsWith("request jobreceived"))
                        {

                            if (this.InvokeRequired)
                            {

                                this.BeginInvoke(new SingleDelegate(JobReceived), message);
                            }
                            else
                            {

                                JobReceived(message);

                            }
                        }

                        else if (values[1].ToStr().StartsWith("request updatepooljobstatus"))
                        {

                            if (this.InvokeRequired)
                            {

                                this.BeginInvoke(new SingleDelegate(RefreshJobPool), message);
                            }
                            else
                            {

                                RefreshJobPool(message);

                            }
                        }


                        else if (values[1].ToStr().StartsWith("request jobpool"))
                        {


                            if (this.InvokeRequired)
                            {

                                this.BeginInvoke(new SingleDelegate(ShowJobPoolAccepterForm), message);
                            }
                            else
                            {

                                ShowJobPoolAccepterForm(message);

                            }

                        }
                        else if (values[3].ToStr() == "tcprestart")
                        {


                            try
                            {

                                if (AppVars.enableSMSService && IsServer)
                                {

                                    bool isKilled = false;

                                    Process tcplistener = Process.GetProcesses().FirstOrDefault(c => c.ProcessName.ToLower().Contains("tcs_tcplistener"));
                                    if (tcplistener != null)
                                    {
                                        try
                                        {

                                            tcplistener.Kill();
                                       //     tcplistener.Close();
                                        }
                                        catch
                                        {


                                        }
                                        isKilled = true;
                                    }
                                    else
                                    {
                                        isKilled = true;

                                    }

                                    if (isKilled)
                                    {

                                        try
                                        {
                                            string path = System.Windows.Forms.Application.StartupPath.Replace("Treasure Cab System", "MapAppSetup") + "\\TCS_TcpListener.exe";

                                            if (File.Exists(path))
                                            {
                                                Process.Start(path);

                                                File.AppendAllText("kill_terminaltcplistener.txt", DateTime.Now.ToStr() + ":" + Environment.NewLine);

                                             
                                            }
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
                        }
                        else
                        {


                            int controllerId = values[1].ToInt();
                            bool IsAll = values[2].ToBool();
                            string msg = values[3].ToStr().Trim();

                            string senderName = string.Empty;
                            if(values.Count()>=6)
                            {
                                senderName = values[5].ToStr();
                                msg += "sender>>" + senderName;
                            }

                            if (values.Count() >= 5)
                            {
                                int userid = values[4].ToInt();
                                if (userid != this.ObjLoginUser.LuserId.ToInt())
                                {



                                    if (IsAll || controllerId == this.ObjLoginUser.LuserId.ToInt())
                                    {

                                        if (this.InvokeRequired)
                                        {
                                            appendTextCallback d = new appendTextCallback(ShowControllerMessagePopup);
                                            this.BeginInvoke(d, msg);
                                        }
                                        else
                                        {
                                            ShowControllerMessagePopup(ref msg);

                                        }

                                    }
                                }

                            }


                        }


                    }
                    else if (message.ToStr().StartsWith("autodespatchmode"))
                    {
                        string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);

                        bool mode = values[1].ToBool();


                        if (Environment.MachineName.ToLower() != values[2].ToLower())
                        {

                            RefreshAutoDespOtherPC = true;
                            chkEnableAutoDespatch.Checked = mode;

                        }


                    }
                    else if (message.ToStr().StartsWith("biddingmode"))
                    {
                        string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);

                        bool mode = values[1].ToBool();


                        if (Environment.MachineName.ToLower() != values[2].ToLower())
                        {


                            RefreshAutoDespOtherPC = true;
                            chkEnableBidding.Checked = mode;

                        }


                    }
                    else if (message.ToStr().StartsWith("onbreakmode"))
                    {
                        string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);

                        bool mode = values[1].ToBool();


                        if (Environment.MachineName.ToLower() != values[2].ToLower())
                        {

                            RefreshOnBreakOtherPC = true;
                            chkEnableOnBreak.Checked = mode;

                        }


                    }
                    else if (message.ToStr().StartsWith("selectroutemileage"))
                    {
                        string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);




                        if (Environment.MachineName.ToLower() == values[1].ToLower())
                        {
                            if (System.Windows.Forms.Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBooking") == 1)
                            {

                                frmBooking objBookingForm = (frmBooking)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBooking");

                                if (objBookingForm != null)
                                {

                                    objBookingForm.SelectMileageFromRouteSugg(values[2].ToDecimal(), values[3].ToStr());

                                }
                            }

                        }


                    }
                    else
                    {
                        ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).ShowAlertMessage(message);
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }


        private void ShowUpdateBookingByCustomerForm(string message)
        {
            string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);


            frmUpdateBookingByCustomer frmcancelCustomer = new frmUpdateBookingByCustomer(values[2].ToLong(), values[3].ToStr(), values[4].ToStr(), values[5], values[6], values[7]);
            frmcancelCustomer.StartPosition = FormStartPosition.CenterScreen;
            frmcancelCustomer.Show();
            frmcancelCustomer.BringToFront();

            new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);

        }


        private void ShowJobPoolAccepterForm(string message)
        {
            try
            {

                try
                {

                    File.AppendAllText(Application.StartupPath + "\\jobpool.txt", DateTime.Now + " : " + message.ToStr() + Environment.NewLine);
                }
                catch
                {


                }

                string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);


                PoolBooking objBooking = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PoolBooking>(values[7].ToStr());

                objBooking.PickupDateTime = objBooking.PickupDateTime.Value.AddHours(1);
                bool IsAvailable=true;

                //if (objBooking.FromLocTypeId.ToInt() != Enums.LOCATION_TYPES.AIRPORT && objBooking.PickupDateTime.Value<= DateTime.Now.AddMinutes(15) && objBooking.FromPostCode.ToStr().Length > 0 && objBooking.FromPostCode.ToStr().Trim().Contains(" "))
                //{






                //    using (TaxiDataContext db = new TaxiDataContext())
                //    {

                //        var objCoord=  db.stp_getCoordinatesByAddress(objBooking.FromPostCode,objBooking.FromPostCode).FirstOrDefault();

                //        if(objCoord!=null)
                //        {
                //        db.CommandTimeout = 6;

                //        var list = db.stp_GetLoginDriverPlotsUpdated().Where(c => c.driverworkstatusid == Enums.Driver_WORKINGSTATUS.AVAILABLE).ToList();


                //        var availabledrivers = (from a in list
                //                                select new
                //                                {
                //                                    a.driverid,
                //                                    a.driverno,
                //                                    Distance = new LatLng(a.latitude, a.longitude).DistanceMiles(new LatLng(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longtiude))),



                //                                }
                //                              ).OrderBy(c => c.Distance).FirstOrDefault();



                //            if(availabledrivers.Distance>3)
                //            {

                //               IsAvailable=false;


                //            }


                //        }

                //    }



                //}


                if(IsAvailable)
                {

                if (objBooking.CompanyId != null)
                {
                    using(TaxiDataContext db=new TaxiDataContext())
                    {
                        db.CommandTimeout=5;
                        int comId=  db.Gen_Companies.FirstOrDefault(c=>c.CompanyName.ToLower()==objBooking.CompanyCreditCardDetails.ToStr().ToLower()).DefaultIfEmpty().Id;


                        if(comId > 0)
                        {
                            objBooking.CompanyId=comId;                        
                        }
                        
                    }
                      

                }



                frmJobAcceptor frmAccepter = new frmJobAcceptor(objBooking);
                frmAccepter.StartPosition = FormStartPosition.CenterScreen;
                frmAccepter.Show();
                frmAccepter.BringToFront();
                }
                //new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);
            }
            catch
            {


            }

        }


        private void JobReceived(string message)
        {
            try
            {
                string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);


              //  alertSound = System.Media.SystemSounds.Question;
               // contentText = "<html> <b><span style=font-size:medium><color=Blue>Driver " + values[2].ToStr() + " is Login</span></b></html>";


                RadDesktopAlert alert = new RadDesktopAlert();
                alert.SoundToPlay = System.Media.SystemSounds.Beep;
                alert.ContentImage = Resources.Resource1._operator;

                alert.CaptionText = values[2].ToStr();
                alert.ContentText = "<html> <b><span style=font-size:medium><color=Blue>Ref # " + values[3].ToStr() + " </span></b></html>";
                alert.Show();
                


                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_TODAY_AND_PREBOOKING_DASHBOARD);
            }
            catch
            {


            }

        }



        private void ShowCancelBookingByCustomerForm(string message)
        {
            try
            {
                if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == false)
                {
                    string[] values = message.Split(new string[] { ">>" }, StringSplitOptions.None);


                    frmCancelBookingByCustomer frmcancelCustomer = new frmCancelBookingByCustomer(values[2].ToLong(), values[3].ToStr(), values[4].ToStr(), values[5], values[6], values[7], values[8]);
                    frmcancelCustomer.StartPosition = FormStartPosition.CenterScreen;
                    frmcancelCustomer.Show();
                    frmcancelCustomer.BringToFront();
                }

                new BroadcasterData().BroadCastToAll(RefreshTypes.REFRESH_DASHBOARD);
            }
            catch
            {


            }
        }


        private string GetCustomerNameFromCall(string number)
        {


            string name = string.Empty;
            try
            {

                using (TaxiDataContext db = new TaxiDataContext())
                {
                    name = db.stp_GetCallerInfo(number,"").FirstOrDefault().DefaultIfEmpty().Name.ToStr().Trim();

                }

                //Customer objCustomer = GeneralBLL.GetQueryable<Customer>(c => c.TelephoneNo == number || c.MobileNo == number).OrderByDescending(C => C.Id).FirstOrDefault();
                //if (objCustomer != null)
                //{
                //    name = objCustomer.Name.ToStr();
                //}
            }
            catch (Exception ex)
            {


            }

            return name;
        }

        private void CreateVPNCTICallLog(string number, string line)
        {
            try
            {
                string name = string.Empty;
                using (TaxiDataContext db = new TaxiDataContext())
                {

                    name= db.stp_GetCallerInfo(number,"").FirstOrDefault().DefaultIfEmpty().Name.ToStr().Trim();


                    //Customer objCustomer = db.GetTable<Customer>().Where(c => c.TelephoneNo == number || c.MobileNo == number).OrderByDescending(C => C.Id).FirstOrDefault();
                    //if (objCustomer != null)
                    //{
                    //    name = objCustomer.Name.ToStr();
                    //}


                    CallHistory objCall = new CallHistory();
                    objCall.Name = name;
                    objCall.PhoneNumber = number;
                    objCall.Line = line;
                    objCall.STN = line;
                    objCall.CallDateTime = DateTime.Now;
                    objCall.AnsweredDateTime = DateTime.Now;
                    objCall.Sno = 1;
                    objCall.ControllerId = this.ObjLoginUser.LuserId.ToIntorNull();
                    objCall.IsAccepted = true;
                    db.CallHistories.InsertOnSubmit(objCall);
                    db.SubmitChanges();

                }


            }
            catch (Exception ex)
            {



            }
        }


        private bool RefreshAutoDespOtherPC = false;

        private void ShowControllerMessagePopup(ref string msg)
        {

            frmPopupInternalMessage frmPop = new frmPopupInternalMessage(msg, false, 0);
            frmPop.Show();
        }


        private void RefreshWebBookings()
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).PopulateWebBookingsGrid();

        }


        private void RefreshDeclinedWebBookings()
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).PopulateDeclinedWebBookings();


        }

        private void RefreshDashboardBookingHistory()
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshBookingList();

        }


        private void SendSMS(string mobileNo, string message)
        {
          //  Thread.Sleep(2000);

            string rtnMsg = string.Empty;
            EuroSMS objSMS = new EuroSMS();
            objSMS.Message = message;


            string mobNo = mobileNo;


            if (mobileNo.StartsWith("+44") == false)
            {

                if (Debugger.IsAttached == false)
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

                        if (mobNo.StartsWith("044") == false || mobNo.StartsWith("+44") == false)
                            mobNo = mobNo.Insert(0, "+44");
                    }
                }
            }

            objSMS.ToNumber = mobNo.Trim();
            objSMS.Send(ref rtnMsg);
           

            //try
            //{
            //    File.AppendAllText(Application.StartupPath + "\\remotesms2.txt", DateTime.Now.ToStr() + ":" + mobNo.ToStr() + " : " + message);
            //}
            //catch
            //{


            //}


            MethodInvoker mi2 = new MethodInvoker(delegate() { this.CreateAndShowAlert(mobNo, message, null, System.Media.SystemSounds.Asterisk, false, "", "", ""); });
            this.Invoke(mi2);

        }







        public void RefreshDashboardDrivers()
        {
            try
            {
                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshDashBoardDrivers();
                // dashBoard.LoadDriverWaitingGrid();
            }
            catch (Exception ex)
            {


            }

        }

        public void RefreshTodayAnPreDashboard()
        {
            try
            {
                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshTodayAndPreData();
                // dashBoard.LoadDriverWaitingGrid();
            }
            catch (Exception ex)
            {


            }

        }

        public void RefreshTodaysBookingsDashboard()
        {
            try
            {
                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshTodayBookingData();
                // dashBoard.LoadDriverWaitingGrid();
            }
            catch (Exception ex)
            {


            }

        }

        //public void RefreshWaitingDrivers()
        //{
        //    try
        //    {
        //        ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshWaitingDrivers();

        //    }
        //    catch (Exception ex)
        //    {


        //    }

        //}

        private void CreateAndShowAlert(string caption, string content, Image contentImg, System.Media.SystemSound sound, bool HasOptionButton, string body, string senderId, string senderName)
        {

            if (AppVars.objPolicyConfiguration.DisablePopupNotifications.ToBool() == true)
                return;


            RadDesktopAlert desktopAlert = new Telerik.WinControls.UI.RadDesktopAlert();



            desktopAlert.FixedSize = new Size(380, 100);
            desktopAlert.AutoCloseDelay = 10;
            //  desktopAlert.FadeAnimationSpeed = 1;
            // desktopAlert.FadeAnimationType = FadeAnimationType.None;
            desktopAlert.FadeAnimationType = FadeAnimationType.None;
            desktopAlert.Popup.AlertElement.Opacity = 100;



            desktopAlert.ButtonItems.Clear();

            desktopAlert.ShowOptionsButton = false;
            desktopAlert.ShowPinButton = false;

            desktopAlert.Show();

            desktopAlert.CaptionText = caption;
            desktopAlert.ContentText = content;
            desktopAlert.ContentImage = contentImg;



        }


        public void CloseApplication()
        {
            try
            {
                this.FormClosing -= new FormClosingEventHandler(frmMainMenu_FormClosing);
                DisposeData();

                AppVars.IsLogout = true;

                Application.Exit();

            }
            catch (Exception ex)
            {



            }

        }




        private void frmMainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Home || e.KeyCode== Keys.Escape)
                {

                    ShowFormInDock("frmBookingDashBoard");
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).GetMainDashBoard();

                }

                if (e.Control)
                {

                    if (e.KeyCode == Keys.N)
                    {
                        ShowNewBooking();
                      //  General.ShowBookingForm(false);
                    }

                    else if (e.KeyCode == Keys.L)
                    {

                        DockWindow doc = UI.MainMenuForm.MainMenuFrm.GetDockByName("frmBookingsList1");

                        if (doc != null)
                        {
                            radDock1.ActiveWindow = doc;
                        }

                        else
                        {
                            ShowFormInDock("frmBookingsList");

                        }
                    }
                }
                else if ((e.KeyCode == Keys.N || e.KeyCode == Keys.B || e.KeyCode == Keys.J || e.KeyCode == Keys.S) && IsTodayBookingActiveTabWOFilter() == true
                    && (AppVars.frmMDI.ActiveControl != null && AppVars.frmMDI.ActiveControl.Name.Equals("frmBookingDashBoard1") == true))
                {
                    if (e.KeyCode == Keys.N)
                    {
                        ShowNewBooking();
                       // General.ShowBookingForm(false);

                    }
                    else if (e.KeyCode == Keys.B)
                    {
                        SetDasbBoardSelectedTab("Pg_PreBookings");
                    }
                    else if (e.KeyCode == Keys.J)
                    {
                        SetDasbBoardSelectedTab("Pg_AllJobs");

                    }
                    else if (e.KeyCode ==  Keys.S)
                    {
                        SetDasbBoardSelectedTab("Pg_RecentJobs");

                    }

                }
                else
                {
                    if (AppVars.frmMDI.ActiveControl != null 
                        && (AppVars.frmMDI.ActiveControl.Name.Equals("rptfrmJobsListReceipts1") == true
                        || AppVars.frmMDI.ActiveControl.Name.Equals("frmBookingsList1") == true)
                        )
                        return;

                    if (e.KeyCode == Keys.F1)
                    {
                        frmTrackDriver frmTrack = new frmTrackDriver();
                        frmTrack.ShowDialog();
                        frmTrack.Dispose();

                    }
                    else if (e.KeyCode == Keys.F2)
                    {
                        frmDrvStreetView frmTrack = new frmDrvStreetView();
                        frmTrack.ShowDialog();
                        frmTrack.Dispose();

                    }
                    else if (e.KeyCode == Keys.F3)
                    {
                        frmOnBreakDrivers frmbreak = new frmOnBreakDrivers();
                        frmbreak.StartPosition = FormStartPosition.CenterScreen;
                        frmbreak.ShowDialog();
                        frmbreak.Dispose();

                    }
                    else if (e.KeyCode == Keys.F4)
                    {
                        frmSearchDriver frm = new frmSearchDriver();
                        frm.ShowDialog();
                        frm.Dispose();
                    }

                    else if (e.KeyCode == Keys.F6)
                    {

                        (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).ShowInboxMessages("Pda");

                    }
                    else if (e.KeyCode == Keys.F7)
                    {

                        frmRecallJob frmRecall = new frmRecallJob();
                        frmRecall.StartPosition = FormStartPosition.CenterScreen;
                        frmRecall.ShowDialog();
                        frmRecall.Dispose();

                    }
                    else if (e.KeyCode == Keys.F8)
                    {
                        frmDriverEarning frm = new frmDriverEarning();
                        frm.ShowDialog();
                        frm.Dispose();


                    }
                    else if (e.KeyCode == Keys.F9)
                    {
                        frmMessageAllDrivers frm = new frmMessageAllDrivers();
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.ShowDialog();
                        frm.Dispose();
                        //frmDriverShiftEarning frm = new frmDriverShiftEarning();
                        //frm.ShowDialog();
                        //frm.Dispose();
                    }
                    //else if (e.KeyCode == Keys.F10)
                    //{
                    //    frmViewDriverRent frm = new frmViewDriverRent();
                    //    frm.ShowDialog();
                    //    frm.Dispose();

                    //}

                    else if (e.KeyCode == Keys.F11)
                    {

                        if (this.ListofUserRights.Count(c => c.functionId == "CONTROLLER MESSAGING") > 0)
                        {

                            frmControllerInternalMessages frm = new frmControllerInternalMessages();
                            frm.StartPosition = FormStartPosition.CenterScreen;
                            frm.ShowDialog();
                            frm.Dispose();
                        }
                    }
                    else if (e.KeyCode == Keys.F12)
                    {

                        if (chkEnableAutoDespatch.Visibility == ElementVisibility.Visible)
                        {

                            chkEnableAutoDespatch.Checked = !chkEnableAutoDespatch.Checked;
                        }
                    }
                
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void SetDasbBoardSelectedTab(string tabName)
        {

            (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).SetSelectedTabName(tabName);

        }

        private string GetDashboardSelectedTabName()
        {

            return (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).GetSelectedTabName();
        }


        private bool IsTodayBookingActiveTabWOFilter()
        {

            return (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).IsActiveTodayTabWOFilter();
        }



        CDRRecordDelegate RecordCB = null;
        CDRStopDelegate RecordStopCB = null;


        short Handle_Renamed;
        public delegate void appendTextCallback(ref string msg);



        public delegate int CDRRecordDelegate(string bStrMsg, int applicationData);
        public delegate int CDRStopDelegate(short Reason, int applicationData);
        [DllImport("CDRClient.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short CdrOpenConnection(string IPAddressOrMachineName, string usernameOfCDRUserGroup, string Password, CDRRecordDelegate CDRRecord, CDRStopDelegate CDRStop, int applicationData);
        [DllImport("CDRClient.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short VBCdrOpenConnection(string IPAddressOrMachineName, string usernameOfCDRUserGroup, string Password, CDRRecordDelegate CDRRecord, CDRStopDelegate CDRStop, int applicationData);
        [DllImport("CDRClient.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short CdrCloseConnection(short Handle);
        [DllImport("CDRClient.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern short CdrClipFile(short Handle);


        delegate void Refreshdelegate();

        delegate void Record_delegate(eCallerIdType callerIdType, string line, string name, string phone, string stn);



        CIDSipLauncher objSip = null;
        CIDTapiLauncher objTapi = null;
        CIDCTITapiLauncher objTapiCTI = null;



        private object GetLocation(string postCode)
        {

            //MapPoint.FindResults objRes = objPDAMap.FindAddressResults("", "", "", "", postCode, MapPoint.GeoCountry.geoCountryUnitedKingdom);


            //MapPoint.Location loc = null;

            //if (objRes.Count == 0)
            //{
            //    var objCoord = new TaxiDataContext().Gen_Coordinates.FirstOrDefault(c => c.PostCode == postCode);


            //    if (objCoord == null)
            //    {
            //        var objC = GetDistance.PostCodeToLongLat(postCode, "GB");

            //        if (objC != null)
            //        {

            //            objCoord = new Gen_Coordinate();
            //            objCoord.Latitude = objC.Value.Latitude;
            //            objCoord.Longitude = objC.Value.Longitude;

            //        }
            //    }


            //    if (objCoord != null)
            //    {

            //        loc = objPDAMap.GetLocation(Convert.ToDouble(objCoord.Latitude), Convert.ToDouble(objCoord.Longitude), 1.00);
            //    }


            //}
            //else
            //{
            //    object item = 1;
            //    loc = (MapPoint.Location)objRes.get_Item(ref item);



            //}


            //return (object)loc;

            return null;
        }


        public decimal GetDistanceAndTime(string origin, string destination, ref string estimatedTime)
        {
            decimal distance = 0.00m;
           // try
            //{


            //    MapPoint.Route objRoute = objPDAMap.ActiveRoute;
              

            //    object item = 1;

              
            //    objApp.ItineraryVisible = false;
            //    objRoute.Clear();

              

            //    object objVal = GetLocation(origin);


           

            //    objRoute.Waypoints.Add(objVal, "A");

              

            //    objRoute.Waypoints.Add(GetLocation(destination), "B");
            //    objRoute.Waypoints.Optimize();

            //    if (AppVars.objPolicyConfiguration.PreferredShortestDistance.ToBool())
            //    {
            //        objRoute.Waypoints.get_Item(ref item).SegmentPreferences = MapPoint.GeoSegmentPreferences.geoSegmentShortest;

            //        object item2 = 2;
            //        objRoute.Waypoints.get_Item(ref item2).SegmentPreferences = MapPoint.GeoSegmentPreferences.geoSegmentShortest;
            //    }


            //    objRoute.ZoomTo();

            //    objRoute.Calculate();


             

            //    distance = Convert.ToDecimal(objRoute.Distance);





            //    if (estimatedTime.Length > 0 && estimatedTime.IsNumeric())
            //    {
            //        estimatedTime = (estimatedTime.ToInt() + ((objRoute.DrivingTime * 24 * 60).ToInt())).ToStr();


            //    }
            //    else
            //    {

            //        estimatedTime = (objRoute.DrivingTime * 24 * 60).ToInt().ToStr();
            //    }
            //}
            //catch
            //{


            //}
            return distance;

        }

        private void LoadAddresses()
        {

            try
            {

                General.LoadZoneList();

                AppVars.keyLocations = (from a in AppVars.BLData.GetAll<Gen_Location>(c => c.ShortCutKey != string.Empty)
                                        select a.ShortCutKey).Distinct().ToList();

                //if (Debugger.IsAttached)
                //{
                //    AppVars.listOfAddress = new List<stp_GetFullAddressesResult>();

                //}
                //else
                //{
                    LoadGeneralAddresses();
             //   }

                //if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
                //{
                //    AppVars.listofPOI = AppVars.BLData.GetAll<POITable>(null).ToList();
                //}


                InitializeOfflineDistance();

            }
            catch (Exception ex)
            {


            }

        }


        private void InitializeOfflineDistance()
        {
            try
            {


                if (AppVars.objPolicyConfiguration.EnableOfflineDistance.ToBool())
                {

                    Process[] processes = Process.GetProcessesByName("MapPoint");


                    if (processes.Count() > 6)
                    {

                        foreach (Process pc in processes)
                        {
                            pc.Kill();


                        }
                    }


                    //objClassType = Type.GetTypeFromProgID("MapPoint.Application.EU");
                    //objApp = (MapPoint.Application)Activator.CreateInstance(objClassType);

                    //string template = System.Windows.Forms.Application.StartupPath + "\\Map.ptt";
                    //objPDAMap = objApp.NewMap(template);
                    //objApp.Units = MapPoint.GeoUnits.geoMiles;
                }

            }
            catch (Exception ex)
            {
                ShowNotification("MapPoint not Installed", "You need to install MapPoint for Offline Distance",null);


            }


        }


        private bool IsVPNMachine = false;

        private void LoadGeneralAddresses()
        {
            try
            {

                if (AppVars.objPolicyConfiguration.EnablePOI.ToBool())
                {
                    AppVars.listOfAddress = new List<stp_GetFullAddressesResult>();


                    using (TaxiDataContext db = new TaxiDataContext())
                    {
                        db.stp_GetByRoadLevelData(General.GetPostCodeMatch(AppVars.objPolicyConfiguration.BaseAddress.ToStr()), "", "", "");

                     //  AppVars.listofVertices = db.Gen_Zone_PolyVertices.ToList();
                    }


                }
                else
                {


                    // For Desktop Clients
                    if (AppVars.AccessFrom == "desktop")
                    {

                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            AppVars.listOfAddress = db.stp_GetFullAddresses("", "", 3).ToList();


                           // AppVars.listofVertices = db.Gen_Zone_PolyVertices.ToList();
                        }
                    }
                    else if (AppVars.AccessFrom == "remote")
                    {
                        if (this.IsVPNMachine)
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                AppVars.listOfAddress = db.stp_GetFullAddresses("", "", 3).ToList();
                             //   AppVars.listofVertices = db.Gen_Zone_PolyVertices.ToList();
                            }
                        }
                        else
                        {

                            // For Remote Clients
                            string path = Application.StartupPath + "\\address.xml";
                            if (File.Exists(path))
                            {
                                XDocument doc = XDocument.Load(path);

                                IEnumerable<XElement> listDes = doc.Descendants("Address");

                                AppVars.listOfAddress = (from a in listDes
                                                         select new stp_GetFullAddressesResult
                                                         {
                                                             AddressLine1 = a.Element("AddressLine1").Value.ToStr(),
                                                             PostalCode = a.Element("PostalCode").Value.ToStr(),


                                                         }).OrderBy(b => b.AddressLine1).ToList();

                                //using (TaxiDataContext db = new TaxiDataContext())
                                //{
                                   
                                //    AppVars.listofVertices = db.Gen_Zone_PolyVertices.ToList();
                                //}
                              //  AppVars.listofVertices = db.Gen_Zone_PolyVertices.ToList();
                            }
                        }
                    }

                }
            }
            catch
            {

                if(AppVars.listOfAddress==null)
                     AppVars.listOfAddress = new List<stp_GetFullAddressesResult>();

            }
            

        }




        private void LoadFormSettings()
        {


            try
            {
              //  File.AppendAllText("log.txt", "onloadformsettings"+Environment.NewLine);

                RadMenuItem item = null;
                RadMenuItem itemInner = null;
                RadMenuItem itemSubInner = null;

                Font headingFont = new Font("Tahoma", 10, FontStyle.Bold);
                Font contentFont = new Font("Tahoma", 10, FontStyle.Bold);

                AppVars.objPolicyConfiguration = General.GetObject<Gen_SysPolicy_Configuration>(v => v.SysPolicyId == 1);

                //  ServerURI =Application. "http://88.208.220.41/GreenMetroCars";
                ServerURI = System.Configuration.ConfigurationManager.AppSettings["huburl"].ToStr();
            

                InitializeSettings();

                //    LogoutTime = AppVars.objPolicyConfiguration.LogoutInActivityElapsedTime.ToInt();

                //if (LogoutTime > 0 && this.ObjLoginUser.LgroupId.ToInt() == 2)
                //{
                //    Gma.UserActivityMonitor.GlobalEventProvider globalEventProvider1 = new Gma.UserActivityMonitor.GlobalEventProvider();
                //   globalEventProvider1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseMove);
                //    globalEventProvider1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.globalEventProvider1_MouseClick);
                //    globalEventProvider1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.globalEventProvider1_KeyPress);
                //}

                // Booking

                if (this.ListofUserRights.Count(c => c.formName == "frmSysPolicy" && c.moduleId == 1) > 0)
                {
                    frmSysPolicy.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmCallHistory" && c.moduleId == 10) > 0)
                {
                    btnCallerId.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    btnCallHistory.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                }


                if (this.ListofUserRights.Count(c => c.formName == "frmBooking" || c.formName == "frmBookingsList") > 0)
                {
                    item = new RadMenuItem();
                    item.Font = headingFont;
                    item.Text = "Booking";
                    menu_Booking.Visibility = Telerik.WinControls.ElementVisibility.Visible;



                    if (this.ListofUserRights.Count(c => c.formName == "frmBooking") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add New Booking";
                        itemInner.Name = "frmBooking";
                        itemInner.Tag = "false";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                        btnAddBooking.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        frmBooking.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmBookingsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Booking List";
                        itemInner.Name = "frmBookingsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                        frmBookingsList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmInCompleteBookingsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "InComplete Bookings";
                        itemInner.Name = "frmInCompleteBookingsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                        RadMenuItem mnuItem = new RadMenuItem();
                        mnuItem.Text = itemInner.Text;
                        mnuItem.Name = itemInner.Name;
                        mnuItem.Font = frmBookingsList.Font;

                        mnuItem.Click += new EventHandler(itemInner_Click);
                        menu_Booking.Items.Add(mnuItem);

                        //frmBookingsList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmIVRBookingsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "IVR Bookings List";
                        itemInner.Name = "frmIVRBookingsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                        RadMenuItem mnuItem = new RadMenuItem();
                        mnuItem.Text = itemInner.Text;
                        mnuItem.Name = itemInner.Name;
                        mnuItem.Font = frmBookingsList.Font;
                        mnuItem.Click += new EventHandler(itemInner_Click);
                        menu_Booking.Items.Add(mnuItem);

                        //frmBookingsList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmWebBookingsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "WebBookings List";
                        itemInner.Name = "frmWebBookingsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                        RadMenuItem mnuItem = new RadMenuItem();
                        mnuItem.Text = itemInner.Text;
                        mnuItem.Name = itemInner.Name;
                        mnuItem.Font = frmBookingsList.Font;

                        mnuItem.Click += new EventHandler(itemInner_Click);
                        menu_Booking.Items.Add(mnuItem);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmRejectedWebBookingsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Rejected WebBookings List";
                        itemInner.Name = "frmRejectedWebBookingsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                        RadMenuItem mnuItem = new RadMenuItem();
                        mnuItem.Text = itemInner.Text;
                        mnuItem.Name = itemInner.Name;
                        mnuItem.Font = frmBookingsList.Font;

                        mnuItem.Click += new EventHandler(itemInner_Click);
                        menu_Booking.Items.Add(mnuItem);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmAdvanceBookingsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Multi Booking List";
                        itemInner.Name = "frmAdvanceBookingsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                        RadMenuItem mnuItem = new RadMenuItem();
                        mnuItem.Text = itemInner.Text;
                        mnuItem.Name = itemInner.Name;
                        mnuItem.Font = frmBookingsList.Font;

                        mnuItem.Click += new EventHandler(itemInner_Click);
                        menu_Booking.Items.Add(mnuItem);

                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmAdvanceMultiVehicleList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Multi Vehicle List";
                        itemInner.Name = "frmAdvanceMultiVehicleList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                        RadMenuItem mnuItem = new RadMenuItem();
                        mnuItem.Text = itemInner.Text;
                        mnuItem.Name = itemInner.Name;
                        mnuItem.Font = frmBookingsList.Font;

                        mnuItem.Click += new EventHandler(itemInner_Click);
                        menu_Booking.Items.Add(mnuItem);

                    }








                    if (this.ListofUserRights.Count(c => c.formName == "frmBookingGroupsList") > 0)
                    {
                        //itemInner = new RadMenuItem();
                        //itemInner.Font = contentFont;
                        //itemInner.Text = "Booking Groups List";
                        //itemInner.Name = "frmBookingGroupsList";
                        //itemInner.Tag = "true";
                        //itemInner.Click += new EventHandler(itemInner_Click);
                        //item.Items.Add(itemInner);


                        RadMenuItem mnuItem = new RadMenuItem();
                        mnuItem.Text = "Booking Groups List";
                        mnuItem.Name = "frmBookingGroupsList";
                        mnuItem.Font = frmBookingsList.Font;
                        mnuItem.Tag = "true";
                        mnuItem.Click += new EventHandler(itemInner_Click);
                        menu_Booking.Items.Add(mnuItem);

                    }




                    if (this.ListofUserRights.Count(c => c.formName == "frmArchiveBookingsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Archive Bookings List";
                        itemInner.Name = "frmArchiveBookingsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                        RadMenuItem mnuItem = new RadMenuItem();
                        mnuItem.Text = itemInner.Text;
                        mnuItem.Name = itemInner.Name;
                        mnuItem.Font = frmBookingsList.Font;

                        mnuItem.Click += new EventHandler(itemInner_Click);
                        menu_Booking.Items.Add(mnuItem);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmTrashBooking") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Trash Bookings";
                        itemInner.Name = "frmTrashBooking";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                        frmTrashBooking.Click += new EventHandler(itemInner_Click);
                        //frmTrashBooking.Tag = null;

                        frmTrashBooking.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    }




                    if (this.ListofUserRights.Count(c => c.formName == "frmUnProcessedJobsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "UnProcessed Bookings";
                        itemInner.Name = "frmUnProcessedJobsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                        RadMenuItem menItem = new RadMenuItem();
                        menItem.DescriptionFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        menItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        menItem.Name = "frmUnProcessedJobsList";
                        menItem.Text = "UnProcessed Bookings";
                        menItem.Click += new EventHandler(itemInner_Click);
                        menItem.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        this.menu_Booking.Items.Add(menItem);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmProcessedJobsList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Processed Bookings";
                        itemInner.Name = "frmProcessedJobsList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                        RadMenuItem menItem = new RadMenuItem();
                        menItem.DescriptionFont = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        menItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        menItem.Name = "frmProcessedJobsList";
                        menItem.Text = "Processed Bookings";
                        menItem.Click += new EventHandler(itemInner_Click);
                        menItem.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        this.menu_Booking.Items.Add(menItem);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmBiddingTimeSettings") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Bidding Time Settings";
                        itemInner.Name = "frmBiddingTimeSettings";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = "true";
                        item.Items.Add(itemInner);

                    }


                    if (item.Items.Count > 0)
                    {
                        radMenu1.Items.Add(item);
                    }

                   
                }




                // Management Menu

                if (this.ListofUserRights.Count(c =>
                                               c.formName == "frmCompany" || c.formName == "frmCompanyList" || c.formName == "frmSysPolicy"
                                               || c.formName == "frmCustomer" || c.formName == "frmCustomersList"
                                                || c.formName == "frmDriver" || c.formName == "frmDriversList"
                                                 || c.formName == "frmFares" || c.formName == "frmFaresList"
                                                  || c.formName == "frmVehicleType" || c.formName == "frmVehicleTypeList"
                    // || c.formName == "frmCompanyFareSettings"
                                                ) > 0)
                {

                    item = new RadMenuItem();
                    item.Font = headingFont;
                    item.Text = "Account Management";







                    if (this.ListofUserRights.Count(c => c.formName == "frmLocations" || c.formName == "frmLocationsList"
                            || c.formName == "frmZones" || c.formName == "frmZonesList") > 0)
                    {
                        menu_Locations.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        if (this.ListofUserRights.Count(c => c.formName == "frmLocations") > 0)
                        {
                            frmLocations.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        }

                        if (this.ListofUserRights.Count(c => c.formName == "frmLocationList") > 0)
                        {
                            frmLocationList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                            frmLocationList.Tag = null;

                        }


                        //New Code
                        if (this.ListofUserRights.Count(c => c.formName == "frmAddressList") > 0)
                        {
                            frmAddressList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        }
                        if (this.ListofUserRights.Count(c => c.formName == "frmAddNewAddress") > 0)
                        {
                            frmAddNewAddress.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        }
                        //



                        if (this.ListofUserRights.Count(c => c.formName == "frmZones") > 0)
                        {
                            frmZones.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        }

                        if (this.ListofUserRights.Count(c => c.formName == "frmZonesList") > 0)
                        {
                            frmZonesList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                            frmZonesList.Tag = null;

                        }


                        if (this.ListofUserRights.Count(c => c.formName == "frmShuttleZones") > 0)
                        {
                            itemInner = new RadMenuItem();
                            itemInner.Font = contentFont;
                            itemInner.Text = "Shuttle Zones";
                            itemInner.Name = "frmShuttleZones";
                            // itemInner.Tag = "true";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            item.Items.Add(itemInner);
                            menu_Locations.Items.Add(itemInner);
                            // frmZones.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        }
                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmInvoice") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create Single Account Invoice";
                        itemInner.Name = "frmInvoice";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmAddMultipleCompanyInvoice") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create All Account Invoice";
                        itemInner.Name = "frmAddMultipleCompanyInvoice";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmInvoiceList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "All Accounts Invoice List";
                        itemInner.Name = "frmInvoiceList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmCompanyInvoiceList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Accounts Invoice History";
                        itemInner.Name = "frmCompanyInvoiceList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmEscortInvoice") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create Escort Invoice";
                        itemInner.Name = "frmEscortInvoice";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmEscortInvoiceList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Escort Invoice History";
                        itemInner.Name = "frmEscortInvoiceList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }




                    if (this.ListofUserRights.Count(c => c.formName == "frmCustomerInvoice") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create Single Cash Invoice";
                        itemInner.Name = "frmCustomerInvoice";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmCustomerInvoiceList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Cash Invoice History";
                        itemInner.Name = "frmCustomerInvoiceList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmCompanyPendingInvoice") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Unpaid Invoice List";
                        itemInner.Name = "frmCompanyPendingInvoice";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmCompanyInvoicePaymentList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Paid Invoice List";
                        itemInner.Name = "frmCompanyInvoicePaymentList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }






                    // PRE-INVOICE

                    if (this.ListofUserRights.Count(c => c.formName == "frmPreAccInvoice") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create Company Pre-Invoice";
                        itemInner.Name = "frmPreAccInvoice";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmPreAccInvoiceList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Company Pre-Invoice List";
                        itemInner.Name = "frmPreAccInvoiceList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }




                    if (this.ListofUserRights.Count(c => c.formName == "frmPreCustomerInvoice") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create Customer Pre-Invoice";
                        itemInner.Name = "frmPreCustomerInvoice";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmPreCustomerInvoiceList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Customer Pre-Invoice List";
                        itemInner.Name = "frmPreCustomerInvoiceList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }




                    if (this.ListofUserRights.Count(c => c.formName == "frmCompany") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create New Account";
                        itemInner.Name = "frmCompany";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmCompanyList") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Accounts List";
                        itemInner.Name = "frmCompanyList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmCompanyGroup") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Account Groups";
                        itemInner.Name = "frmCompanyGroup";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);


                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmEscort") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create New Escort";
                        itemInner.Name = "frmEscort";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmEscortList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Escort List";
                        itemInner.Name = "frmEscortList";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }








                    if (this.ListofUserRights.Count(c => c.formName == "frmSubCompany") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Sub Companies List";
                        itemInner.Name = "frmSubCompany";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }



                    //if (this.ListofUserRights.Count(c => c.formName == "frmThirdPartySubCompany") > 0)
                    //{
                    //    itemInner = new RadMenuItem();
                    //    itemInner.Font = contentFont;
                    //    itemInner.Text = "Third Party Companies List";
                    //    itemInner.Name = "frmThirdPartySubCompany";
                    //    itemInner.Click += new EventHandler(itemInner_Click);
                    //    item.Items.Add(itemInner);
                    //}







                    // Customer
                    if (this.ListofUserRights.Count(c => c.formName == "frmCustomer" || c.formName == "frmCustomersList"
                        //|| c.formName == "frmComplaint" 

                                                ) > 0)
                    {


                        menu_Customer.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        if (this.ListofUserRights.Count(c => c.formName == "frmCustomer") > 0)
                        {



                            frmCustomer.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        }

                        if (this.ListofUserRights.Count(c => c.formName == "frmCustomersList") > 0)
                        {


                            frmCustomersList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        }


                        if (this.ListofUserRights.Count(c => c.formName == "frmLostProperty") > 0)
                        {



                            frmLostProperty.Click += new EventHandler(itemInner_Click);
                            frmLostProperty.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        }
                        else
                        {
                            frmLostProperty.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;


                        }


                        if (this.ListofUserRights.Count(c => c.formName == "frmLostPropertyList") > 0)
                        {

                            frmLostPropertyList.Click += new EventHandler(itemInner_Click);

                            frmLostPropertyList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        }
                        else
                        {
                            frmLostPropertyList.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;


                        }

                    }



                    if (this.ListofUserRights.Count(c => (c.formName == "frmFares" && c.functionId != "DISABLE PLOT WISE FARE LIST") || c.formName == "frmFaresList") > 0

                        )
                    {
                        // Fares

                        menu_Fares.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        if (this.ListofUserRights.Count(c => c.formName == "frmFares") > 0)
                            frmFares.Visibility = Telerik.WinControls.ElementVisibility.Visible;


                        if (this.ListofUserRights.Count(c => c.formName == "frmFaresList") > 0)
                            frmFaresList.Visibility = Telerik.WinControls.ElementVisibility.Visible;


                        if (this.ListofUserRights.Count(c => c.formName == "frmFareMeterSetting") > 0)
                        {
                            itemInner = new RadMenuItem();
                            //    itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.));
                            itemInner.Font = contentFont;
                            itemInner.Text = "Fare Meter Settings";
                            itemInner.Name = "frmFareMeterSetting";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            itemInner.Tag = "true";

                            menu_Fares.Items.Add(itemInner);
                        }



                        if (this.ListofUserRights.Count(c => c.formName == "frmFareIncrement") > 0)
                        {
                            itemInner = new RadMenuItem();
                            itemInner.Font = contentFont;
                            itemInner.Text = "Fare Increment Settings";
                            itemInner.Name = "frmFareIncrement";
                            itemInner.Tag = "true";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            item.Items.Add(itemInner);
                            menu_Fares.Items.Add(itemInner);
                            // frmZones.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        }

                        if (this.ListofUserRights.Count(c => c.formName == "frmServiceCharges") > 0)
                        {
                            itemInner = new RadMenuItem();
                            itemInner.Font = contentFont;
                            itemInner.Text = "Service Charges";
                            itemInner.Name = "frmServiceCharges";
                            itemInner.Tag = "true";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            item.Items.Add(itemInner);

                            menu_Fares.Items.Add(itemInner);
                        }


                        if (this.ListofUserRights.Count(c => c.formName == "frmZoneTypePricing") > 0)
                        {

                            itemInner = new RadMenuItem();
                            //    itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.));
                            itemInner.Font = contentFont;
                            itemInner.Text = "Plot Type Pricing";
                            itemInner.Name = "frmZoneTypePricing";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            itemInner.Tag = "true";
                            //  item.Items.Add(itemInner);


                            menu_Fares.Items.Add(itemInner);
                        }
                        if (this.ListofUserRights.Count(c => c.formName == "frmCompanyFareSettings") > 0)
                        {
                            itemInner = new RadMenuItem();
                            itemInner.Font = contentFont;
                            itemInner.Text = "Company Fare Settings";
                            itemInner.Name = "frmCompanyFareSettings";

                            itemInner.Click += new EventHandler(itemInner_Click);
                            item.Items.Add(itemInner);
                            menu_Fares.Items.Add(itemInner);
                            // frmZones.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        }


                        if (this.ListofUserRights.Count(c => c.formName == "frmJourneyTime") > 0)
                        {
                            itemInner = new RadMenuItem();
                            itemInner.Font = contentFont;
                            itemInner.Text = "Journey Time";
                            itemInner.Name = "frmJourneyTime";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            itemInner.Tag = "true";
                            menu_Fares.Items.Add(itemInner);
                        }



                        if (this.ListofUserRights.Count(c => c.formName == "frmPeakOffPeakTimeSettings") > 0)
                        {

                            itemInner = new RadMenuItem();
                            //    itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.));
                            itemInner.Font = contentFont;
                            itemInner.Text = "Peak Time Settings";
                            itemInner.Name = "frmPeakOffPeakTimeSettings";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            itemInner.Tag = "true";
                            // item.Items.Add(itemInner);


                            menu_Fares.Items.Add(itemInner);
                        }


                        if (this.ListofUserRights.Count(c => c.formName == "frmSpecialDayFares") > 0)
                        {
                            itemInner = new RadMenuItem();
                            //    itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.));
                            itemInner.Font = contentFont;
                            itemInner.Text = "Fare Settings";
                            itemInner.Name = "frmSpecialDayFares";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            itemInner.Tag = "true";

                            menu_Fares.Items.Add(itemInner);
                        }


                        if (this.ListofUserRights.Count(c => c.formName == "frmShowSetFares") > 0)
                        {

                            itemInner = new RadMenuItem();
                            //    itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.));
                            itemInner.Font = contentFont;
                            itemInner.Text = "Custom Fares List";
                            itemInner.Name = "frmShowSetFares";
                            itemInner.Click += new EventHandler(itemInner_Click);
                            // itemInner.Tag = "true";
                            // item.Items.Add(itemInner);


                            menu_Fares.Items.Add(itemInner);
                        }



                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmVehicleType" || c.formName == "frmVehicleTypeList"

                                               ) > 0)
                    {
                        // Vehicle

                        menu_Vehicle.Visibility = Telerik.WinControls.ElementVisibility.Visible;

                        if (this.ListofUserRights.Count(c => c.formName == "frmVehicleType") > 0)
                            frmVehicleType.Visibility = Telerik.WinControls.ElementVisibility.Visible;


                        if (this.ListofUserRights.Count(c => c.formName == "frmVehicleTypeList") > 0)
                            frmVehicleTypeList.Visibility = Telerik.WinControls.ElementVisibility.Visible;



                        if (this.ListofUserRights.Count(c => c.formName == "frmCompanyVehcile") > 0)
                        {

                            frmCompanyVehcile.Click += new EventHandler(itemInner_Click);

                            frmCompanyVehcile.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        }
                        if (this.ListofUserRights.Count(c => c.formName == "frmCompanyVehcileList") > 0)
                        {


                            frmCompanyVehcileList.Click += new EventHandler(itemInner_Click);

                            frmCompanyVehcileList.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                        }
                    }


                    if (item.Items.Count > 0)
                    {
                        radMenu1.Items.Add(item);
                    }
                   
                }


                // Driver Management

                // Utilities
                // NEED TO UNCOMMENT
                if (this.ListofUserRights.Count(c => c.formName == "frmDriver" || c.formName == "frmDriversList") > 0)
                {


                    item = new RadMenuItem();
                    item.Font = headingFont;
                    item.Text = "Driver Management";


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriver" && c.functionId == "EXECUTE") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add New Driver";
                        itemInner.Name = "frmDriver";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriversList") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Drivers List";
                        itemInner.Name = "frmDriversList";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmInActiveDriversList") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "InActive Driver List";
                        itemInner.Name = "frmInActiveDriversList";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverLogin") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Login";
                        itemInner.Name = "frmDriverLogin";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(frmDriverLogin_Click);
                        item.Items.Add(itemInner);

                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverLoginList") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Login List";
                        itemInner.Name = "frmDriverLoginList";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }


                    //frmAssignShifts
                    if (this.ListofUserRights.Count(c => c.formName == "frmAssignShifts" && c.functionId == "EXECUTE") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Assign Shifts";
                        itemInner.Name = "frmAssignShifts";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverShiftsList" && c.functionId == "EXECUTE") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Shifts List";
                        itemInner.Name = "frmDriverShiftsList";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    //if (this.ListofUserRights.Count(c => c.formName == "frmShifts") > 0)
                    //{

                    //    itemInner = new RadMenuItem();
                    //    itemInner.Font = contentFont;
                    //    itemInner.Text = "Drivers Shifts";
                    //    itemInner.Name = "frmShifts";

                    //    itemInner.Click += new EventHandler(itemInner_Click);
                    //    item.Items.Add(itemInner);
                    //}


                    if (this.ListofUserRights.Count(c => c.formName == "frmAttributes") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Attributes";
                        itemInner.Name = "frmAttributes";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }




                    // Rent Expense 1

                    if (this.ListofUserRights.Count(c => c.formName == "frmAddAllDriverRentExpenses") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add All Driver Rent (Expenses)";
                        itemInner.Name = "frmAddAllDriverRentExpenses";
                        itemInner.Tag = true;

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentDebitCredit") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add Driver Rent (Expenses)";
                        itemInner.Name = "frmDriverRentDebitCredit";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Rent List";

                        itemInner.Name = "frmDriverRentList";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentPayExpenses") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Rent Pay (Expenses)";
                        itemInner.Name = "frmDriverRentPayExpenses";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }






                    // Rent Expense 2


                    if (this.ListofUserRights.Count(c => c.formName == "frmAddAllDriverRentExpenses2") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add All Driver Rent (Expenses)";
                        itemInner.Name = "frmAddAllDriverRentExpenses2";
                        itemInner.Tag = true;

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentDebitCredit2") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add Driver Rent (Expenses)";
                        itemInner.Name = "frmDriverRentDebitCredit2";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentList2") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Rent List";

                        itemInner.Name = "frmDriverRentList2";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentPayExpenses2") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Rent Pay (Expenses)";
                        itemInner.Name = "frmDriverRentPayExpenses2";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }



                    //



                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionDebitCredit") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Generate Driver Commission";
                        itemInner.Name = "frmDriverCommissionDebitCredit";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmAddAllDriverCommissionExpenses") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create All Driver Commission";
                        itemInner.Name = "frmAddAllDriverCommissionExpenses";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmAddAllDriverCommissionExpenses2") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create All Driver Commission (Expenses)";
                        itemInner.Name = "frmAddAllDriverCommissionExpenses2";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommisionList") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Commission List";

                        itemInner.Name = "frmDriverCommisionList";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }




                    // Rent Expense 3

                    if (this.ListofUserRights.Count(c => c.formName == "frmAddAllDriverRentExpenses3") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "All Driver Rent (Expenses)";
                        itemInner.Name = "frmAddAllDriverRentExpenses3";
                        itemInner.Tag = true;

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentDebitCredit3") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Generate Driver Rent";
                        itemInner.Name = "frmDriverRentDebitCredit3";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentList3") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Rent List";
                        itemInner.Name = "frmDriverRentList3";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverRentPayExpenses3") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Rent Pay (Expenses)";
                        itemInner.Name = "frmDriverRentPayExpenses3";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }



                    //







                    //if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionPay") > 0)
                    //{

                    //    itemInner = new RadMenuItem();
                    //    itemInner.Font = contentFont;
                    //    itemInner.Text = "Commission Pay";
                    //    itemInner.Name = "frmDriverCommissionPay";

                    //    itemInner.Click += new EventHandler(itemInner_Click);
                    //    itemInner.Tag = true;
                    //    item.Items.Add(itemInner);
                    //}

                    if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverCommissionPaymentSummary") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Commission Payment Summary";
                        itemInner.Name = "rptfrmDriverCommissionPaymentSummary";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        //itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionPayExpenses") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Commission Pay";
                        itemInner.Name = "frmDriverCommissionPayExpenses";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionDebitCredit2") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add Driver Commission (Expenses)";
                        itemInner.Name = "frmDriverCommissionDebitCredit2";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        //itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }
                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommisionList2") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Commission List";
                        itemInner.Name = "frmDriverCommisionList2";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }
                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionPayExpenses2") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Commission Pay";
                        itemInner.Name = "frmDriverCommissionPayExpenses2";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }







                    // Driver commission 3


                    if (this.ListofUserRights.Count(c => c.formName == "frmAddAllDriverCommissionExpenses3") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create All Driver Commission (Expenses)";
                        itemInner.Name = "frmAddAllDriverCommissionExpenses3";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionDebitCredit3") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add Driver Commission (Expenses)";
                        itemInner.Name = "frmDriverCommissionDebitCredit3";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        //itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }
                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommisionList3") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Commission List";
                        itemInner.Name = "frmDriverCommisionList3";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }
                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionPayExpenses3") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Commission Pay";
                        itemInner.Name = "frmDriverCommissionPayExpenses3";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmAddAllDriverCommissionExpenses4") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create All Driver Commission (Expenses)";
                        itemInner.Name = "frmAddAllDriverCommissionExpenses4";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionDebitCredit4") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Add Driver Commission (Expenses)";
                        itemInner.Name = "frmDriverCommissionDebitCredit4";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        //itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }
                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommisionList4") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Commission List";
                        itemInner.Name = "frmDriverCommisionList4";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }
                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionPayExpenses4") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Commission Pay";
                        itemInner.Name = "frmDriverCommissionPayExpenses4";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }





                    // Driver commission 3


                    if (this.ListofUserRights.Count(c => c.formName == "frmAddAllDriverCommissionExpenses5") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Create All Driver Commission (Expenses)";
                        itemInner.Name = "frmAddAllDriverCommissionExpenses5";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionDebitCredit5") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Generate Driver Commission";
                        itemInner.Name = "frmDriverCommissionDebitCredit5";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        //itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }
                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommisionList5") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Commission List";
                        itemInner.Name = "frmDriverCommisionList5";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }
                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionPayExpenses5") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Commission Pay";
                        itemInner.Name = "frmDriverCommissionPayExpenses5";

                        itemInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Tag = true;
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverPDASettings") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver PDA Settings";
                        itemInner.Name = "frmDriverPDASettings";
                        itemInner.Tag = true;
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (item.Items.Count > 0)
                    {
                        radMenu1.Items.Add(item);
                    }
                }






                // User Managment

                if (this.ListofUserRights.Count(c => c.formName == "frmUsers" || c.formName == "frmSecGroupAuthorities") > 0)
                {


                    item = new RadMenuItem();
                    item.Font = headingFont;
                    item.Text = "User Management";
                    // NEED TO UNCOMMENT
                    if (this.ListofUserRights.Count(c => c.formName == "frmUsers") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Users";
                        itemInner.Name = "frmUsers";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmSecGroupAuthorities") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Security Group Authorities";
                        itemInner.Name = "frmSecGroupAuthorities";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }
                    radMenu1.Items.Add(item);
                }






                if (AppVars.objPolicyConfiguration.EnablePDA.ToBool() && AppVars.IsTelephonist.ToBool() == false)
                {

                    // Plotting Menu

                    item = new RadMenuItem();
                    item.Font = headingFont;
                    item.Text = "Plotting";
                    item.Font = headingFont;

                    if (this.ListofUserRights.Count(c => c.formName == "frmSinBin") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Sin Bin Driver";
                        itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.U));
                        itemInner.Name = "frmSinBin";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }



                   


                    // NEED TO UNCOMMENT
                    //itemInner = new RadMenuItem();
                    //itemInner.Font = contentFont;
                    //itemInner.Text = "Plot Driver";
                    //itemInner.Name = "frmPlotDriver";
                    //itemInner.Tag = "false";
                    //itemInner.Click += new EventHandler(itemMenuPlotDrv_Click);
                    //itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.P));
                    //item.Items.Add(itemInner);



                    //itemInner = new RadMenuItem();
                    //itemInner.Font = contentFont;
                    //itemInner.Text = "Un-Plot Driver";
                    //itemInner.Name = "frmPlotDriver";
                    //itemInner.Click += new EventHandler(itemMenuUnPlotDrv_Click);
                    //item.Items.Add(itemInner);



                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Plot Ordering";
                    itemInner.Name = "frmPlotZone";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Tag = "true";
                    item.Items.Add(itemInner);


                    itemInner = new RadMenuItem();
                   // itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.M));

                    itemInner.Font = contentFont;
                    itemInner.Text = "Manage Plots";
                    itemInner.Name = "frmPlotTiming";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Tag = "true";
                    item.Items.Add(itemInner);



                    if (this.ListofUserRights.Count(c => c.formName == "frmDriverPlotEntrance") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Driver Plot Entrance";
                        itemInner.Name = "frmDriverPlotEntrance";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmPlotMovements") > 0)
                    {

                        itemInner = new RadMenuItem();
                        //itemInner.Shortcuts.Add(new RadShortcut(Keys.Control, Keys.M));

                        itemInner.Font = contentFont;
                        itemInner.Text = "Plots Movement";
                        itemInner.Name = "frmPlotMovements";
                        itemInner.Click += new EventHandler(itemInner_Click);

                        item.Items.Add(itemInner);
                    }



                    //itemInner = new RadMenuItem();
                    //itemInner.Font = contentFont;
                    //itemInner.Text = "Check Driver Distance From Base";
                    //itemInner.Name = "frmCheckDriverDistance";
                    //itemInner.Click += new EventHandler(itemInner_Click);
                    //itemInner.Tag = "true";
                    //item.Items.Add(itemInner);


                    if (this.ListofUserRights.Count(c => c.formName == "frmManagePostCodes") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Manage PostCodes";
                        itemInner.Name = "frmManagePostCodes";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }



                    if (this.ListofUserRights.Count(c => c.formName == "frmManageLocations") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Manage Locations";
                        itemInner.Name = "frmManageLocations";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmLocalization") > 0)
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Localization";
                        itemInner.Name = "frmLocalization";
                        itemInner.Tag = "true";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);

                    }


                    if (item.Items.Count > 0)
                    {
                        radMenu1.Items.Add(item);
                    }
                }

                menu_Booking.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                frmBookingsList.Visibility = Telerik.WinControls.ElementVisibility.Visible;






                // Reports Menu

                //if (this.ListofUserRights.Count(c => c.formName == "frmDriverReport" || c.formName == "frmIncomeReport"
                //                            || c.formName == "frmInvoiceReport" || c.formName == "frmDriverCommissionReport"
                //                             || c.formName == "frmCustomerInvoiceList" || c.formName == "rptfrmJobsList") > 0)
                //{

                item = new RadMenuItem();
                item.Font = headingFont;
                item.Text = "Reports";


                // Heading Wise

                //Driver

                itemInner = new RadMenuItem();
                itemInner.Font = contentFont;
                itemInner.Text = "Driver";
                item.Items.Add(itemInner);

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmRentReconciliation") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Rent Reconciliation Report";
                    itemSubInner.Name = "rptfrmRentReconciliation";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "frmDriverReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Rent Report";
                    itemSubInner.Name = "frmDriverReport";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverEarning") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Earning Report";
                    itemSubInner.Name = "rptfrmDriverEarning";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }

               
if (this.ListofUserRights.Count(c => c.formName == "rptfrmCompanyVehicle") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Company Vehicle Report";
                    itemSubInner.Name = "rptfrmCompanyVehicle";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }
                if (this.ListofUserRights.Count(c => c.formName == "rptfrmVehicleLoginHistory") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Vehicle Login History Report";
                    itemSubInner.Name = "rptfrmVehicleLoginHistory";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverRentSummaryReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Rent Summary Report";
                    itemSubInner.Name = "rptfrmDriverRentSummaryReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmDriverMonthlyRentReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Rent History Report";
                    itemSubInner.Name = "frmDriverMonthlyRentReport";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }


                if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionDetailReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Commission Detail Report";
                    itemSubInner.Name = "frmDriverCommissionDetailReport";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }



                if (this.ListofUserRights.Count(c => c.formName == "frmDriverCommissionReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Commission Report";
                    itemSubInner.Name = "frmDriverCommissionReport";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverCommissionSummaryReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Commission Summary Report";
                    itemSubInner.Name = "rptfrmDriverCommissionSummaryReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmDriverMonthlyCommisionReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Commission Weekly/Monthly Report";
                    itemSubInner.Name = "frmDriverMonthlyCommisionReport";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }


                //if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverCollectionList") > 0)
                //{

                //    itemSubInner = new RadMenuItem();
                //    itemSubInner.Font = contentFont;
                //    itemSubInner.Text = "Driver Collection List Report";
                //    itemSubInner.Name = "rptfrmDriverCollectionList";

                //    itemSubInner.Click += new EventHandler(itemInner_Click);
                //    itemInner.Items.Add(itemSubInner);

                //}


                if (this.ListofUserRights.Count(c => c.formName == "frmDriverPaymentCollection") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Drivers Collection List";
                    itemSubInner.Name = "frmDriverPaymentCollection";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }
                if (this.ListofUserRights.Count(c => c.formName == "frmDriverPaymentAccountBookings") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Drivers Weekly Rent Sheet";
                    itemSubInner.Name = "frmDriverPaymentAccountBookings";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "frmDriverPaymentCollectionHistory") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Collection History";
                    itemSubInner.Name = "frmDriverPaymentCollectionHistory";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmDriverPaymentAccountBookingsHistory") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Weekly Rent History";
                    itemSubInner.Name = "frmDriverPaymentAccountBookingsHistory";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }



                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverAccountBookings") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Account Report";
                    itemSubInner.Name = "rptfrmDriverAccountBookings";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }



                if (this.ListofUserRights.Count(c => c.formName == "frmDriverIncomeStatement") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Income Statement";
                    itemSubInner.Name = "frmDriverIncomeStatement";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmDriverTotalSummaryReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Summary Report";
                    itemSubInner.Name = "frmDriverTotalSummaryReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverJobsList") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Booking Stats";
                    itemSubInner.Name = "rptfrmDriverJobsList";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverJobLog") > 0)
                {




                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Log Report";
                    itemSubInner.Name = "frmDriverJobLogReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);


                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverLoginHistory") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Login History Report";
                    itemSubInner.Name = "rptfrmDriverLoginHistory";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }


                if (this.ListofUserRights.Count(c => c.formName == "frmDriverDailyJobSheet") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Daily Job Sheet";
                    itemSubInner.Name = "frmDriverDailyJobSheet";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverPDAReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver PDA Report";
                    itemSubInner.Name = "rptfrmDriverPDAReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverWeeklyCollectionStatement") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Weekly Collection Statement";
                    itemSubInner.Name = "rptfrmDriverWeeklyCollectionStatement";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmSinBin") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Sin Bin Report";
                    itemSubInner.Name = "rptfrmSinBin";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmDriverLoginHour") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Driver Login Hour Report";
                    itemSubInner.Name = "rptfrmDriverLoginHour";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (itemInner.Items.Count == 0)
                    itemInner.Visibility = ElementVisibility.Collapsed;




                //


                //Account

                itemInner = new RadMenuItem();
                itemInner.Font = contentFont;
                itemInner.Text = "Account";
                item.Items.Add(itemInner);

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmAccountInvoiceSummary") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Account Invoice Summary";
                    itemSubInner.Name = "rptfrmAccountInvoiceSummary";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmInvoicePayment") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Invoice Statement Report";
                    itemSubInner.Name = "rptfrmInvoicePayment";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCompanyAccount") > 0)
                {



                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Cash Account Jobs Report";
                    itemSubInner.Name = "rptfrmCompanyAccount";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                //rights
                if (this.ListofUserRights.Count(c => c.formName == "frmCashAccountStatementReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Cash Account Statement Report";
                    itemSubInner.Name = "frmCashAccountStatementReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }




                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCashAccSummary") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Cash Account Statement Details";
                    itemSubInner.Name = "rptfrmCashAccSummary";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }




                if (this.ListofUserRights.Count(c => c.formName == "frmAgentCommissionReport") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Agent Commission Report - To Pay";
                    itemSubInner.Name = "frmAgentCommissionReport";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }
                if (this.ListofUserRights.Count(c => c.formName == "rptfrmAccountJobList") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Account Job List";
                    itemSubInner.Name = "rptfrmAccountJobList";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }
                //rptfrmAccountJobList
                if (this.ListofUserRights.Count(c => c.formName == "rptfrmThirdPartyCompanyJobStatementReport") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Transfer Job Statement Report";
                    itemSubInner.Name = "rptfrmThirdPartyCompanyJobStatementReport";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmAgentCommissionTakenStatement") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = " Agent Commission Report - To Collect";
                    itemSubInner.Name = "rptfrmAgentCommissionTakenStatement";

                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                }







                if (this.ListofUserRights.Count(c => c.formName == "frmDrvRentAccStatmentSummary") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Account Statement Summary For Rent";
                    itemSubInner.Name = "frmDrvRentAccStatmentSummary";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "frmDrvCommAccStatmentSummary") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Account Statement Summary For Commission";
                    itemSubInner.Name = "frmDrvCommAccStatmentSummary";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCustomerAppUsers") > 0)
                {



                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "App Users Report";
                    itemSubInner.Name = "rptfrmCustomerAppUsers";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }





                if (itemInner.Items.Count == 0)
                    itemInner.Visibility = ElementVisibility.Collapsed;




                //Job

                itemInner = new RadMenuItem();
                itemInner.Font = contentFont;
                itemInner.Text = "Job";
                item.Items.Add(itemInner);


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmJobsList") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Jobs List Report";
                    itemSubInner.Name = "rptfrmJobsList";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmQuotationReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Quotation List Report";
                    itemSubInner.Name = "rptfrmQuotationReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmBookingFeesReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Booking Fees Report";
                    itemSubInner.Name = "rptfrmBookingFeesReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCustomerFeedBackReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Customer FeedBack Report";
                    itemSubInner.Name = "rptfrmCustomerFeedBackReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmJobsListReceipts") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Jobs Receipt Report";
                    itemSubInner.Name = "rptfrmJobsListReceipts";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmFutureJobDiarySheet") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Future Job Diary Sheet Report";
                    itemSubInner.Name = "rptfrmFutureJobDiarySheet";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmJobPaymentList") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Job Payment Report";
                    itemSubInner.Name = "rptfrmJobPaymentList";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }



                if (this.ListofUserRights.Count(c => c.formName == "rptfrmJobActivity") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Job Activity Report";
                    itemSubInner.Name = "rptfrmJobActivity";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }




                //rights
                if (this.ListofUserRights.Count(c => c.formName == "frmbookinglog") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Job Log Report";
                    itemSubInner.Name = "frmbookinglog";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }



                if (this.ListofUserRights.Count(c => c.formName == "frmTreasureOperatorReports") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "PCO Reports";
                    itemSubInner.Name = "frmTreasureOperatorReports";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "JobPool Report";
                    itemSubInner.Name = "frmJobPoolReport1";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }



                if (this.ListofUserRights.Count(c => c.formName == "frmJobStatisticsReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Job Statistics Report";
                    itemSubInner.Name = "frmJobStatisticsReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmAutoDispatch") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Auto Despatch Job Report";
                    itemSubInner.Name = "rptfrmAutoDispatch";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (itemInner.Items.Count == 0)
                    itemInner.Visibility = ElementVisibility.Collapsed;


                //Call

                itemInner = new RadMenuItem();
                itemInner.Font = contentFont;
                itemInner.Text = "Call";
                item.Items.Add(itemInner);


                



                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCallHistory") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Call History Report";
                    itemSubInner.Name = "rptfrmCallHistory";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCallHistoryBooking") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Call Summary/Detail Report";
                    itemSubInner.Name = "rptfrmCallHistoryBooking";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }



                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCallStatisticsReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Call Statistics Report";
                    itemSubInner.Name = "rptfrmCallStatisticsReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                //Rights
                if (this.ListofUserRights.Count(c => c.formName == "rptfrmRingBackLog") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "RingBack Log Report";
                    itemSubInner.Name = "rptfrmRingBackLog";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCallDetail") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Call Detail Report";
                    itemSubInner.Name = "rptfrmCallDetail";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }




                if (this.ListofUserRights.Count(c => c.formName == "rptfrmActivityLogReport") > 0)
                {

                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Activity Log Report";
                    itemSubInner.Name = "rptfrmActivityLogReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }





                if (this.ListofUserRights.Count(c => c.formName == "rptfrmAdminReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Controller Activity Report";
                    itemSubInner.Name = "rptfrmAdminReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }
                if (this.ListofUserRights.Count(c => c.formName == "rptfrmOperatorPerformance") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Operator Performance Report";
                    itemSubInner.Name = "rptfrmOperatorPerformance";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (itemInner.Items.Count == 0)
                    itemInner.Visibility = ElementVisibility.Collapsed;


                //



                //Income

                itemInner = new RadMenuItem();
                itemInner.Font = contentFont;
                itemInner.Text = "Income";
                item.Items.Add(itemInner);

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmCashCallCharges") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Cash Call Charges Report";
                    itemSubInner.Name = "rptfrmCashCallCharges";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }





                if (this.ListofUserRights.Count(c => c.formName == "frmIncomeReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Income Report";
                    itemSubInner.Name = "frmIncomeReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmCompanyIncomeReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Company Income Report";
                    itemSubInner.Name = "frmCompanyIncomeReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "frmIncomeSummaryReport") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Income Summary Report";
                    itemSubInner.Name = "frmIncomeSummaryReport";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "rptfrmIncomeStatement") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Income Statement Report";
                    itemSubInner.Name = "rptfrmIncomeStatement";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "rptfrmInvoiceSummary") > 0)
                {


                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Account Invoice Summary Report";
                    itemSubInner.Name = "rptfrmInvoiceSummary";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }



                if (this.ListofUserRights.Count(c => c.formName == "rptfrmAccountInvoiceSummary") > 0)
                {
                    itemSubInner = new RadMenuItem();
                    itemSubInner.Font = contentFont;
                    itemSubInner.Text = "Account Invoice Summary";
                    itemSubInner.Name = "rptfrmAccountInvoiceSummary";
                    itemSubInner.Click += new EventHandler(itemInner_Click);
                    itemInner.Items.Add(itemSubInner);
                }

                //

                if (this.ListofUserRights.Count(c => c.formName == "frmShowGraphs") > 0
             || this.ListofUserRights.Count(c => c.formName == "frmPlotStatistics") > 0)
                {
                    //Graph


                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Graph";
                    item.Items.Add(itemInner);

                    if (this.ListofUserRights.Count(c => c.formName == "frmShowGraphs") > 0)
                    {
                        itemSubInner = new RadMenuItem();
                        itemSubInner.Font = contentFont;
                        itemSubInner.Text = "Graph Reports";
                        itemSubInner.Name = "frmShowGraphs";
                        itemSubInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Items.Add(itemSubInner);
                    }

                    if (this.ListofUserRights.Count(c => c.formName == "frmPlotStatistics") > 0)
                    {

                        itemSubInner = new RadMenuItem();
                        itemSubInner.Font = contentFont;
                        itemSubInner.Text = "Plots Statistics Report";
                        itemSubInner.Name = "frmPlotStatistics";
                        itemSubInner.Click += new EventHandler(itemInner_Click);
                        itemInner.Items.Add(itemSubInner);
                    }

                }

                if (itemInner.Items.Count == 0)
                    itemInner.Visibility = ElementVisibility.Collapsed;





                //

                if (item.Items.Count(c => c.Visibility == ElementVisibility.Collapsed) == item.Items.Count)
                    item.Visibility = ElementVisibility.Collapsed;



                

                radMenu1.Items.Add(item);
                //   }




                #region General

                item = new RadMenuItem();
                item.Font = headingFont;
                item.Text = "General";

                if (this.ListofUserRights.Count(c => c.formName == "frmSinBinSettings") > 0)
                {
                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Sin Bin Settings";
                    itemInner.Name = "frmSinBinSettings";
                    itemInner.Tag = "true";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmAirportColorCodings") > 0)
                {
                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Airports Color Coding";
                    itemInner.Name = "frmAirportColorCodings";
                    itemInner.Tag = "true";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmBookingTypes") > 0)
                {
                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Booking Types Color Coding";
                    itemInner.Name = "frmBookingTypes";
                    itemInner.Tag = "true";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);
                }


                if (this.ListofUserRights.Count(c => c.formName == "frmOnlineBookingSettings") > 0)
                {
                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Online Booking Settings";
                    itemInner.Name = "frmOnlineBookingSettings";
                    itemInner.Tag = "true";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmDriverDocumentExpirySettings") > 0)
                {
                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Driver Document Expiry Setup";
                    itemInner.Name = "frmDriverDocumentExpirySettings";
                    itemInner.Tag = "true";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);
                }

                if (this.ListofUserRights.Count(c => c.formName == "frmLocationTypes") > 0)
                {

                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Location Types Shorcuts";
                    itemInner.Name = "frmLocationTypes";
                    itemInner.Tag = "true";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);

                }




                if (this.ListofUserRights.Count(c => c.formName == "frmSurchargeRateSettings") > 0)
               
                    {
                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Surcharge Rate Settings";
                        itemInner.Name = "frmSurchargeRateSettings";
                        itemInner.Tag = "false";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }
               


                    if (this.ListofUserRights.Count(c => c.formName == "frmFormShortCuts") > 0)
                {
                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Add New ShortCuts";
                    itemInner.Name = "frmFormShortCuts";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);

                }



                if (item.Items.Count > 0)
                {

                    radMenu1.Items.Add(item);
                }
                #endregion

                // Utilities
                // NEED TO UNCOMMENT
                if (this.ListofUserRights.Count(c => c.formName == "frmComcabBooking" || c.formName == "frmJobClearingUtil") > 0)
                {


                    item = new RadMenuItem();
                    item.Font = headingFont;
                    item.Text = "Utilities";


                    if (this.ListofUserRights.Count(c => c.formName == "frmImportBooking") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Import Booking";
                        itemInner.Name = "frmImportBooking";
                        itemInner.Tag = "false";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmImportBookingTemplate2") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Import Booking (Xml)";
                        itemInner.Name = "frmImportBookingTemplate2";
                        itemInner.Tag = "false";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmComcabBooking") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Comcab Booking";
                        itemInner.Name = "frmComcabBooking";
                        itemInner.Tag = "false";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    if (this.ListofUserRights.Count(c => c.formName == "frmJobClearingUtil") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "Job Clearing Utility";
                        itemInner.Name = "frmJobClearingUtil";
                        itemInner.Tag = "false";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    //if (this.ListofUserRights.Count(c => c.formName == "frmDownloadFares") > 0)
                    //{

                    //    itemInner = new RadMenuItem();
                    //    itemInner.Font = contentFont;
                    //    itemInner.Text = "Fare Download Utility";
                    //    itemInner.Name = "frmDownloadFares";
                    //    itemInner.Tag = "false";
                    //    itemInner.Click += new EventHandler(itemInner_Click);
                    //    item.Items.Add(itemInner);
                    //}


                    if (this.ListofUserRights.Count(c => c.formName == "frmGridViewDisplaySettings") > 0)
                    {

                        itemInner = new RadMenuItem();
                        itemInner.Font = contentFont;
                        itemInner.Text = "GridView Display Settings";
                        itemInner.Name = "frmGridViewDisplaySettings";
                        itemInner.Click += new EventHandler(itemInner_Click);
                        item.Items.Add(itemInner);
                    }


                    radMenu1.Items.Add(item);
                }




                item = new RadMenuItem();
                item.Font = headingFont;
                item.Text = "Help";


                // need to uncomment
                itemInner = new RadMenuItem();
                itemInner.Font = contentFont;
                itemInner.Text = "User Manual";
                itemInner.Name = "UserManual";
                itemInner.Tag = "true";
                itemInner.Visibility = ElementVisibility.Collapsed;
                itemInner.Click += new EventHandler(itemTutor_Click);
                item.Items.Add(itemInner);


                if (ListofUserRights.Count(c => c.formName == "frmCabTreasureMobileHelp") > 0)
                {

                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "Cab Treasure Mobile Help";
                    itemInner.Name = "frmCabTreasureMobileHelp";
                    itemInner.Tag = "true";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);
                }


                itemInner = new RadMenuItem();
                itemInner.Font = contentFont;
                itemInner.Text = "Support";
                itemInner.Name = "frmClientEmail";
                itemInner.Tag = "true";
                itemInner.Click += new EventHandler(itemInner_Click);
                item.Items.Add(itemInner);


//                if (this.ListofUserRights.Count(c => c.formName ==

//"frmFormShortCuts") > 0)
//                {
//                    itemInner = new RadMenuItem();
//                    itemInner.Font = contentFont;
//                    itemInner.Text = "ShortCuts List";
//                    itemInner.Name = "frmFormShortCuts";
//                    itemInner.Click += new EventHandler(itemInner_Click);
//                    item.Items.Add(itemInner);

//                }
                //Help
                if (this.ListofUserRights.Count(c => c.formName =="frmShortCuts") > 0)
                {
                    itemInner = new RadMenuItem();
                    itemInner.Font = contentFont;
                    itemInner.Text = "All Short keys List";
                    itemInner.Name = "frmShortCuts";
                    itemInner.Tag = "true";
                    itemInner.Click += new EventHandler(itemInner_Click);
                    item.Items.Add(itemInner);

                }

                itemInner = new RadMenuItem();
                itemInner.Font = contentFont;
                itemInner.Text = "About Treasure Cloud Booking And Dispatch System";
                itemInner.Name = "frmAboutUs";
                itemInner.Tag = "true";
                itemInner.Click += new EventHandler(itemAboutUs_Click);
                item.Items.Add(itemInner);


                radMenu1.Items[0].Font = headingFont;
                (radMenu1.Items[0] as RadMenuItem).Items[0].Font = contentFont;
                (radMenu1.Items[0] as RadMenuItem).Items[1].Font = contentFont;

                radMenu1.Items[1].Font = headingFont;
                (radMenu1.Items[1] as RadMenuItem).Items[1].Font = contentFont;

                radMenu1.Items.Add(item);






                if (AppVars.LoginObj.IsAdmin)
                {

                    RadMenuItem viewAllCompanyItem = new RadMenuItem();
                    viewAllCompanyItem.Click += new EventHandler(viewAllCompanyItem_Click);
                    viewAllCompanyItem.Text = "View All Data";
                    viewAllCompanyItem.Font = contentFont;
                    viewAllCompanyItem.CheckOnClick = true;
                    (radMenu1.Items[1] as RadMenuItem).Items.Add(viewAllCompanyItem);
                    viewAllCompanyItem.IsChecked = true;

                    AddShowAllDriversMenu();
                    AddShowAllBookingsMenu();
                }
                else
                {
                    AddShowAllDriversMenu();
                    AddShowAllBookingsMenu();

                }



                //  AppVars.DefaultSubCompanyId = 0;


                RadMenuItem itemViewMenu = new RadMenuItem();
             //   itemViewMenu.Shortcuts.Add(new RadShortcut(Keys.Alt, Keys.F));
                itemViewMenu.Click += new EventHandler(itemViewMenu_Click);
                itemViewMenu.Text = "Full Screen";
                itemViewMenu.Font = contentFont;
                itemViewMenu.CheckOnClick = true;
                (radMenu1.Items[1] as RadMenuItem).Items.Add(itemViewMenu);



                itemViewMenu = new RadMenuItem();
                //  itemViewMenu.Shortcuts.Add(new RadShortcut(Keys.Alt, Keys.F));
                itemViewMenu.Click += new EventHandler(itemViewEmailMenu_Click);
                itemViewMenu.Text = "Sent Email Items";
                itemViewMenu.Font = contentFont;
                //  itemViewMenu.CheckOnClick = true;
                (radMenu1.Items[1] as RadMenuItem).Items.Add(itemViewMenu);


                itemViewMenu = new RadMenuItem();
                //itemViewMenu.Shortcuts.Add(new RadShortcut(Keys.Alt, Keys.F));
                itemViewMenu.Click += new EventHandler(itemViewSMSMenu_Click);
                itemViewMenu.Text = "Sent SMS Items";
                itemViewMenu.Font = contentFont;
                // itemViewMenu.CheckOnClick = true;
                (radMenu1.Items[1] as RadMenuItem).Items.Add(itemViewMenu);


                this.ShowDefaultToolbar = false;

                this.LoginFormName = "frmLogin";
                this.AppTitle =  this.ObjLoginUser.UserName + " - Treasure Cloud Booking And Dispatch System (v"+FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).FileVersion+")";
                this.Icon = Resources.Resource1.taxiicon;





                AppVars.listofSMSTags = AppVars.BLData.GetAll<SMSTag>().ToList();

                string conn = General.DecryptConnectionString(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToStr());
                string ip = conn.Remove(conn.IndexOf(';'));
                ip = ip.Substring(ip.IndexOf('=') + 1);
                if (!ip.IsValidIPAddress() && ip.Contains(".") == false)
                {



                    IsServer = true;

                    AppVars.objSMSConfiguration = General.GetObject<Gen_SysPolicy_SMSConfiguration>(v => v.SysPolicyId == 1);


                    if (AppVars.objSMSConfiguration != null)
                    {

                       

                        if (AppVars.objSMSConfiguration.SMSAccountType == Enums.SMSACCOUNT_TYPE.MODEMSMS)
                        {

                            try
                            {
                                AppVars.enableSMSService = true;


                                StartSMSService();

                            }
                            catch (Exception ex)
                            {

                                ShowNotification("SMS", "GSM Modem : " + ex.Message + Environment.NewLine + " Please check your device services", null);

                             

                                //DisposeData();
                                //AppVars.IsLogout = true;

                                //this.FormClosing -= new FormClosingEventHandler(frmMainMenu_FormClosing);
                                //this.Close();
                                //Application.Exit();
                                //return;

                            }
                        }
                    }
                }
                else
                {

                    AppVars.objSMSConfiguration = new Gen_SysPolicy_SMSConfiguration();
                    AppVars.objSMSConfiguration.SMSAccountType = Enums.SMSACCOUNT_TYPE.MODEMSMS;

                   


                    try
                    {
                        if (AppVars.AccessFrom != "remote" && ip.ToStr().Trim().ToLower() == AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim().ToLower())
                        {
                            AppVars.AccessFrom = "remote";
                        }
                    }
                    catch
                    {


                    }
                }


                if (AppVars.objPolicyConfiguration.UseMultipleSMSGateways.ToBool())
                {
                    AppVars.objSMSConfiguration.SMSAccountType = Enums.SMSACCOUNT_TYPE.CLICKATELLANDMODEMSMS;

                }


                if (AppVars.objPolicyConfiguration.AutoCloseDrvPopup.ToBool() && this.ListofUserRights.Count(c => c.functionId == "HIDE AUTODESPATCH MODE") == 0)
                {
                    txtCurrentTimer.Font = new Font("Tahoma", 10, FontStyle.Bold);
                    chkEnableAutoDespatch.Visibility = ElementVisibility.Visible;
                    chkEnableAutoDespatch.Checked = AppVars.objPolicyConfiguration.EnableAutoDespatch.ToBool();
                    chkEnableAutoDespatch.ForeColor = chkEnableAutoDespatch.Checked ? Color.Green : Color.Black;
                    this.chkEnableAutoDespatch.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkEnableAutoDespatch_ToggleStateChanged);


                    if (this.ListofUserRights.Count(c => c.functionId == "SHOW BIDDING MODE") > 0)
                    {
                        chkEnableBidding.Visibility = ElementVisibility.Visible;

                        chkEnableBidding.Checked = AppVars.objPolicyConfiguration.EnableBidding.ToBool();
                        chkEnableBidding.ForeColor = chkEnableBidding.Checked ? Color.Green : Color.Black;
                        this.chkEnableBidding.ToggleStateChanged += new StateChangedEventHandler(chkEnableBidding_ToggleStateChanged);
                    }

                }


                if (this.ListofUserRights.Count(c => c.functionId == "SHOW ONBREAK MODE") > 0)
                {

                    chkEnableOnBreak.Visibility = ElementVisibility.Visible;
                    chkEnableOnBreak.Checked = AppVars.objPolicyConfiguration.EnableOnBreak.ToBool();
                    chkEnableOnBreak.ForeColor = chkEnableOnBreak.Checked ? Color.Green : Color.Black;
                    this.chkEnableOnBreak.ToggleStateChanged += new StateChangedEventHandler(chkEnableOnBreak_ToggleStateChanged);

                }
                else
                {
                    chkEnableOnBreak.Checked = true;

                }


                Thread address_Thread = new Thread(new ThreadStart(LoadAddresses));
                address_Thread.IsBackground = true;
                address_Thread.Priority = ThreadPriority.Highest;
                address_Thread.Start();


                LoadCallerIdSettings();


                menu_DashBoard.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                this.ShowForm("frmBookingDashBoard");


                AppVars.frmMDI = this;

                btnCallHistory.Click += new EventHandler(btnCallHistory_Click);


                if (AppVars.objPolicyConfiguration.EnablePeakOffPeakFares.ToBool() && AppVars.objPolicyConfiguration.FareMeterType.ToInt() == 2)
                {
                    frmFares.Name = "frmSpecialDayFares";
                    frmFaresList.Name = "frmSpecialDayFares";

                    frmFares.Tag = "true";
                    frmFaresList.Tag = "true";

                    //frmFares.Click += new EventHandler(FaresFormInner_Click);
                    //frmFaresList.Click += new EventHandler(FaresFormInner_Click);

                }
                //else
                //{

                frmFares.Click += new EventHandler(itemInner_Click);
                frmFaresList.Click += new EventHandler(itemInner_Click);
                //  }

                frmVehicleType.Click += new EventHandler(itemInner_Click);
                frmVehicleTypeList.Click += new EventHandler(itemInner_Click);

                frmCustomer.Click += new EventHandler(itemInner_Click);
                frmCustomersList.Click += new EventHandler(itemInner_Click);

                frmBooking.Click += new EventHandler(itemInner_Click);
                frmBookingsList.Click += new EventHandler(itemInner_Click);

                frmLocations.Click += new EventHandler(itemInner_Click);
                frmLocationList.Click += new EventHandler(itemInner_Click);

                //New Code for Address
                frmAddNewAddress.Click += new EventHandler(itemInner_Click);
                //Address List
                frmAddressList.Click += new EventHandler(itemInner_Click);

                frmZones.Click += new EventHandler(itemInner_Click);
                frmZonesList.Click += new EventHandler(itemInner_Click);


                frmSysPolicy.Click += new EventHandler(frmSysPolicy_Click);



                this.IsFormLoaded = true;


                if (System.Configuration.ConfigurationManager.AppSettings["autorefresh"] != null)
                    EnableAutoRefresh = false;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);


            }




            try
            {

                this.StatusStrip_label1.Image = Resources.Resource1.time;
                this.StatusStrip_label1.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();


           

                this.StatusStrip_Label2.Text = "Operator : " + this.ObjLoginUser.UserName + (AppVars.IsTelephonist ? " [Telephonist]" : "");


               


              //  this.StatusStrip_Label2.Image = Resources.Resource1._operator;



                string cli = string.Empty;


                if (this.objCallerId.IsAnalog.ToBool())
                    cli += "Analog,";

                if (this.objCallerId.IsDigital.ToBool())
                    cli += "Digital,";


                if (this.objCallerId.IsVOIP.ToBool())
                    cli += "Voip";




                this.StatusStrip_Label3.Image = Resources.Resource1.answer_call;


                if (cli.Length > 0 && cli[cli.Length - 1] == ',')
                {
                    cli = cli.Remove(cli.Length - 1);

                }





                if (ListOfExtensions == null)
                {
                    this.StatusStrip_Label3.Text = "CLI : " + cli;
                }
                else
                {

                    if (objCallerId.ReceiveVOIPCallFromPhone.ToBool())
                    {
                       

                        this.StatusStrip_Label3.Text = "CLI : " + cli + ", Ext-";
                        this.StatusStrip_Label3.Tag = "CLI : " + cli;


                        try
                        {
                            using (TaxiDataContext db = new TaxiDataContext())
                            {
                                db.CommandTimeout = 2;

                                string exti = string.Join(",", db.UM_UserExtensions.Where(c => c.UserId == AppVars.LoginObj.LuserId).Select(c => c.UserExtension).ToArray<string>());
                                this.StatusStrip_Label3.Text = "CLI : " + cli + ", Ext-" + exti;





                            }
                        }
                        catch
                        {

                        }



                    }
                    else
                    {

                        var ext = string.Join(",", ListOfExtensions.Select(c => c.CLIExtension.ToStr()).ToArray<string>());

                        this.StatusStrip_Label3.Text = "CLI : " + cli + ", Ext-" + ext;
                        this.StatusStrip_Label3.Tag = "CLI : " + cli;

                        if ( ext.ToStr().Length > 0 && ListOfExtensions.Count > 0 && Environment.MachineName.ToStr().ToLower() != ListOfExtensions[0].UserMachineName.ToStr().ToLower())
                        {
                            this.StatusStrip_Label3.ForeColor = Color.Red;
                            //this.StatusStrip_Label3.IsLink=true;

                        }
                    }

                    
                }

               


            }
            catch
            {


            }

        }

        public bool RefreshOnBreakOtherPC = false;
        void chkEnableOnBreak_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {

            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                chkEnableOnBreak.ForeColor = Color.Green;

                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).SetOnBreakMode(true, RefreshOnBreakOtherPC);

            }
            else
            {
                chkEnableOnBreak.ForeColor = Color.Black;

                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).SetOnBreakMode(false, RefreshOnBreakOtherPC);
            }

            RefreshOnBreakOtherPC = false;
        }




        private bool IsServer = false;


        private void AddShowAllDriversMenu()
        {

            RadMenuItem viewAllDrvItem = new RadMenuItem();
            viewAllDrvItem.Click += new EventHandler(viewAllDriverItem_Click);
            viewAllDrvItem.Text = "View All Drivers";
            viewAllDrvItem.Font = new Font("Tahoma", 10, FontStyle.Bold);
            viewAllDrvItem.CheckOnClick = true;
            (radMenu1.Items[1] as RadMenuItem).Items.Add(viewAllDrvItem);


            if (this.ShowAllDrivers && this.ShowDriverFilter.ToBool())
            {
                viewAllDrvItem.Visibility = ElementVisibility.Visible;
                viewAllDrvItem.IsChecked = true;
            }
            else
            {

                viewAllDrvItem.Visibility = ElementVisibility.Collapsed;
                viewAllDrvItem.IsChecked = false;
            }

            AppVars.DefaultDriverSubCompanyId = viewAllDrvItem.IsChecked ? 0 : AppVars.objSubCompany.Id;

            if (ShowAllDrivers)
                AppVars.DefaultDriverSubCompanyId = 0;

        }


        private void AddShowAllBookingsMenu()
        {

            RadMenuItem viewAllBookingItem = new RadMenuItem();
            viewAllBookingItem.Click += new EventHandler(viewAllBookingItem_Click);
            viewAllBookingItem.Text = "View All Booking";
            viewAllBookingItem.Font = new Font("Tahoma", 10, FontStyle.Bold);
            viewAllBookingItem.CheckOnClick = true;
            (radMenu1.Items[1] as RadMenuItem).Items.Add(viewAllBookingItem);




            if (this.ShowAllBookings && this.ShowBookingFilter.ToBool())
            {
                viewAllBookingItem.Visibility = ElementVisibility.Visible;
                viewAllBookingItem.IsChecked = true;

            }
            else
            {
                viewAllBookingItem.Visibility = ElementVisibility.Collapsed;
                viewAllBookingItem.IsChecked = false;
            }



            AppVars.DefaultBookingSubCompanyId = viewAllBookingItem.IsChecked ? 0 : AppVars.objSubCompany.Id;

            if (ShowAllBookings.ToBool())
            {
                AppVars.DefaultBookingSubCompanyId = 0;

            }
        }



        void itemViewEmailMenu_Click(object sender, EventArgs e)
        {
            try
            {
                frmSentEmail frmE = new frmSentEmail();
                frmE.ShowDialog();
                frmE.Dispose();
            }
            catch (Exception ex)
            {


            }
        }

        void itemViewSMSMenu_Click(object sender, EventArgs e)
        {

            try
            {
                frmSentSMS frmS = new frmSentSMS();
                frmS.ShowDialog();
                frmS.Dispose();
            }
            catch (Exception ex)
            {


            }


        }

        void itemViewMenu_Click(object sender, EventArgs e)
        {


            ShowFullScreen((sender as RadMenuItem).IsChecked);
            if (radPanel1.Visible)
            {
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).SetNormalScreen();

            }
            else
            {
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).SetFullScreen();


            }

        }

        private delegate void ConnectedHandler(bool connected);

        private DateTime? GSMLastDisconnectOn = null;
        private void OnPhoneConnectionChange(bool connected)
        {
            if (connected)
            {
                if (StatusStrip_Label3.Text.Contains(", GSM Modem : Connected"))
                    StatusStrip_Label3.Text = StatusStrip_Label3.Text.Replace(",GSM Modem : Connected", "").Trim();

                this.StatusStrip_Label3.Text = this.StatusStrip_Label3.Tag.ToStr() + ",GSM Modem : Connected";
                GSMLastDisconnectOn = null;
            }
            else
            {
                if (StatusStrip_Label3.Text.Contains(", GSM Modem : Disconnected"))
                    StatusStrip_Label3.Text = StatusStrip_Label3.Text.Replace(",GSM Modem : Disconnected", "").Trim();

                this.StatusStrip_Label3.Text = this.StatusStrip_Label3.Tag.ToStr() + ",GSM Modem : Disconnected";
                GSMLastDisconnectOn = DateTime.Now;
            }

        }


        private void comm_PhoneConnected(object sender, EventArgs e)
        {
            this.Invoke(new ConnectedHandler(OnPhoneConnectionChange), new object[] { true });
        }

        private void comm_PhoneDisconnected(object sender, EventArgs e)
        {
            if (!this.IsDisposed)
                this.Invoke(new ConnectedHandler(OnPhoneConnectionChange), new object[] { false });
        }


        void frmDriverLogin_Click(object sender, EventArgs e)
        {
            frmDriverLogin frm = new frmDriverLogin();
            frm.Show();
        }


      

        void frmMainMenu_Load(object sender, EventArgs e)
        {



            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;


            LoadFormSettings();
            timer1.Start();


            VerifySettings();
            ConnectAsync();

            t2.Interval = 5000;
            t2.Elapsed += T2_Elapsed;
            t2.Start();
        }

        private DateTime? DisConnectedSince = null;

        private void T2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(IsConnected==true)
            {

                if(Connection.State==  Microsoft.AspNet.SignalR.Client.ConnectionState.Connected)
                {

                    try
                    {

                        this.StatusStrip_Label2.Text = "Operator : " + this.ObjLoginUser.UserName + (AppVars.IsTelephonist ? " [Telephonist]" : "") + " [Server Connected]";
                        DisConnectedSince = null;

                        //try
                        //{
                        //    File.AppendAllText("serverlog.txt", DateTime.Now.ToStr() + " : CONNECTED" + Environment.NewLine);
                        //}
                        //catch
                        //{

                        //}

                    }
                    catch
                    {


                    }


                }

                if (Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
                {
                    try
                    {
                        this.StatusStrip_Label2.Text = "Operator : " + this.ObjLoginUser.UserName + (AppVars.IsTelephonist ? " [Telephonist]" : "") + " [Server Disconnected]";

                        try
                        {
                            File.AppendAllText("serverlog.txt", DateTime.Now.ToStr()+" : DISCONNECTED"+Environment.NewLine);
                        }
                        catch
                        {

                        }

                        if (DisConnectedSince != null )
                        {
                            if (DateTime.Now.Subtract(DisConnectedSince.Value).TotalSeconds >= 15)
                            {

                                IsConnected = false;
                                DisConnectedSince = null;
                                ConnectAsync();
                                IsConnected = true;
                            }
                        }
                        else
                        {



                            DisConnectedSince = DateTime.Now;


                        }
                    }
                    catch
                    {


                    }
               

               
                 
                }

            }
        }

        private void VerifySettings()
        {
            try
            {

                if (IsServer && (AppVars.objPolicyConfiguration.EnablePDA.ToBool()) && showWarning == true)
                {
                    string officeIP=string.Empty;
                    bool IsOk = true;


                    if (Debugger.IsAttached)
                        SHOWWARNINGS = "no";

                    if (SHOWWARNINGS != "no" && AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim().Length > 0
                        && AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim().IsValidIPAddress())
                    {
                        officeIP = getExternalIp();


                        if (officeIP.ToStr().Trim().Length > 0 && officeIP.ToStr().Trim().IsValidIPAddress() && officeIP != AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim())
                        {
                            frmInvalidPDAIPWarning frmInvalidIP = new frmInvalidPDAIPWarning(officeIP);
                            frmInvalidIP.ShowDialog();
                            frmInvalidIP.Dispose();

                            IsOk = false;
                        }
                    }
                    else if(AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim().Length==0)
                    {

                        frmInvalidPDAIPWarning frmInvalidIP = new frmInvalidPDAIPWarning();
                        frmInvalidIP.ShowDialog();
                        frmInvalidIP.Dispose();

                        IsOk = false;
                    }



                    if (   IsOk  && Process.GetProcessesByName("MapApp").Count() == 0)
                    {

                        frmWarning frmWarning = new frmWarning();
                        frmWarning.ShowDialog();
                        frmWarning.Dispose();

                    }



                    if (IsOk && Process.GetProcessesByName("IVRNotificationReceiver").Count() == 0 && File.Exists(Application.StartupPath + "\\IVRNotificationReceiver.exe"))
                    {

                        try
                        {
                            System.Diagnostics.Process p = new System.Diagnostics.Process();
                            if (Environment.OSVersion.Version.Major >= 6)
                            {
                                p.StartInfo.Verb = "runas";
                            }

                            p.StartInfo.FileName = Application.StartupPath + "\\IVRNotificationReceiver.exe";                          

                            p.StartInfo.UseShellExecute = true;
                            p.Start();

                           
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }

                    }
                 


                }

                if (System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToStr() != "dd/MM/yyyy")
                {
                    frmDateFormatWarning frmWarning = new frmDateFormatWarning();
                    frmWarning.ShowDialog();

                    frmWarning.Dispose();

                }
            }
            catch
            {


            }
           

        }

        private  string getExternalIp()
        {
            string externalIP = string.Empty;

            try
            {

                using (WebClient wc = new WebClient())
                {
                    externalIP = wc.DownloadString("http://icanhazip.com/").Replace("\n", "").Trim();
                }

            }
            catch
            {
                try
                {

                    externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                    externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                              .Matches(externalIP)[0].ToString();
                }
                catch
                {


                }

            }

            return externalIP;
        }




        private string ReminderString = string.Empty;
        private string ReminderTime = string.Empty;
        private void CheckReminder()
        {
            if (ReminderString.ToStr().Trim().Length > 0)
            {

                tmrReminder = new System.Windows.Forms.Timer();

                string[] arr=   ReminderTime.Split(new char[] { ':' });
                int hour = arr[0].ToInt();
                int min = arr[1].ToInt();


                TimeSpan t = new TimeSpan(hour, min, 0);

                TimeSpan currentTime = DateTime.Now.TimeOfDay;

                int seconds = 0;
                int interval = 0;

                if (currentTime > t && currentTime.Subtract(t).TotalSeconds.ToInt() <= 900)
                {
                    interval = 10;

                }
                else
                {

                    if (currentTime > t)
                    {
                        seconds = currentTime.Subtract(t).TotalSeconds.ToInt();
                        interval = 86400 - seconds;
                    }
                    else
                    {
                        seconds = t.Subtract(currentTime).TotalSeconds.ToInt();
                        interval = seconds;
                    }
                }


                tmrReminder.Interval = (interval * 1000);
                tmrReminder.Enabled = true;
                tmrReminder.Start();
                tmrReminder.Tick += new EventHandler(tmrReminder_Tick);
               
            }
        }

        void tmrReminder_Tick(object sender, EventArgs e)
        {

            tmrReminder.Interval = (86400 * 1000);

            frmReminder frmRem = new frmReminder(ReminderString);
            frmRem.StartPosition = FormStartPosition.CenterScreen;
            frmRem.Show();
        }


        private System.Windows.Forms.Timer tmrReminder = null;
        private string SHOWWARNINGS = "";
        public bool showWarning = true;
        private void InitializeSettings()
        {

            try
            {
                if (File.Exists(Application.StartupPath + "\\Configuration.xml"))
                {

                    XmlDocument d = new XmlDocument();
                    d.Load(Application.StartupPath + "\\Configuration.xml");

                    if (d.GetElementsByTagName("ACCESSFROM").Count > 0)
                    {

                        AppVars.AccessFrom = d.GetElementsByTagName("ACCESSFROM")[0].InnerText.ToStr().ToLower().Trim();


                        if (d.GetElementsByTagName("ISVPNMACHINE").Count > 0)
                        {

                            this.IsVPNMachine = true;

                        }
                    }


                    if (AppVars.objPolicyConfiguration.RemoteIPs.ToStr().Trim().Length == 0)
                    {

                        if (IsServer)
                        {

                            if (d.GetElementsByTagName("SOCKETCLIIP").Count > 0)
                            {
                                SocketCLIIP = d.GetElementsByTagName("SOCKETCLIIP")[0].InnerText.ToStr().ToLower().Trim();

                            }

                        }
                        //else if (IsSingleMachineCTI == false)
                        //{


                        //    if (d.GetElementsByTagName("SOCKETCLI").Count > 0)
                        //    {
                        //        //   IsSocketCLI = true;

                        //        RunSocket();
                        //    }

                        //}
                    }



                    if (d.GetElementsByTagName("REMINDERTIME").Count > 0)
                    {

                        ReminderTime = d.GetElementsByTagName("REMINDERTIME")[0].InnerText.ToStr().ToLower().Trim();


                        if (d.GetElementsByTagName("REMINDERTEXT").Count > 0)
                        {

                            ReminderString = d.GetElementsByTagName("REMINDERTEXT")[0].InnerText.ToStr().ToLower().Trim();


                        }
                        CheckReminder();
                    }


                    if (d.GetElementsByTagName("SHOWWARNINGS").Count > 0)
                    {
                        SHOWWARNINGS = d.GetElementsByTagName("SHOWWARNINGS")[0].InnerText.ToStr().ToLower().Trim();

                    }


                    //if (d.GetElementsByTagName("ENABLESMSSERVICE").Count > 0)
                    //{
                    //    AppVars.enableSMSService = true;
                    //}

                }
            }
            catch
            {



            }

        }

      



        void frmSysPolicy_Click(object sender, EventArgs e)
        {



            ShowFormInDock(frmSysPolicy.Name);


        }

        void itemTutor_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.StartupPath + "\\User manual treasure.chm");


            }
            catch (Exception ex)
            {


            }

        }


        void itemAboutUs_Click(object sender, EventArgs e)
        {
            frmAboutUs frm = new frmAboutUs();
            frm.ShowDialog();
        }

        void itemInner_Click(object sender, EventArgs e)
        {
            try
            {
                RadMenuItem item = (RadMenuItem)sender;

                if (item.Tag.ToStr() == "")
                {
                    ShowMenuItemForm(item.Name, true, false);
                }
                else
                {
                    if (item.Name == "frmBooking")
                    {
                        General.ShowBookingForm(false);
                    }
                    else
                    {

                        ShowMenuItemForm(item.Name, false, item.Tag.ToBool());
                    }

                }
            }
            catch (Exception ex)
            {


            }


        }



        void FaresFormInner_Click(object sender, EventArgs e)
        {
            try
            {
                RadMenuItem item = (RadMenuItem)sender;

                if (item.Tag.ToStr() == "")
                {
                    ShowMenuItemForm(item.Name, true, false);
                }
                else
                {
                    

                   ShowMenuItemForm(item.Name, false, item.Tag.ToBool());
                    

                }
            }
            catch (Exception ex)
            {


            }


        }


        void itemMenuPlotDrv_Click(object sender, EventArgs e)
        {
            frmPlotDriver frm = new frmPlotDriver(true);
            frm.Show();
            //            AppVars.frmDashBoard.ShowFormOnKey(Keys.P);

        }

        void itemMenuUnPlotDrv_Click(object sender, EventArgs e)
        {
            frmPlotDriver frm = new frmPlotDriver(false);
            frm.Show();
            // AppVars.frmDashBoard.ShowFormOnKey(Keys.U);

        }

        private void ShowMenuItemForm(string formName, bool showInDock, bool showInDialog)
        {
            if (showInDock)
            {

                ShowFormInDock(formName);

            }
            else
            {
                ShowFormInDialog(formName, showInDialog);
            }

        }




        private bool IsFormLoaded = false;



        // The main control for communicating through the RS-232 port
        private SerialPort comport = new SerialPort();

        string serialData = string.Empty;
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (!comport.IsOpen) return;


                // Read all the data waiting in the buffer
                serialData += comport.ReadExisting();

                string testdata = string.Empty;

                if (serialData.Contains("AM") || serialData.Contains("PM"))
                {

                    if ((serialData.Contains("AM")))
                    {
                        testdata = serialData.Substring(serialData.IndexOf("AM") + 2).ToStr().Trim().Strip(' ').ToStr().Trim();
                    }
                    else if ((serialData.Contains("PM")))
                    {
                        testdata = serialData.Substring(serialData.IndexOf("PM") + 2).ToStr().Trim().Strip(' ').ToStr().Trim();
                    }
                }


                if (testdata.Length >= 11)
                {

                    if (this.InvokeRequired)
                    {

                        Record_delegate d = new Record_delegate(RecordDisplay);
                        this.BeginInvoke(d, new object[] { eCallerIdType.Analog, "5", "", testdata, "" });
                    }
                    else
                    {

                        RecordDisplay(eCallerIdType.Analog, "5", "", testdata, "");
                    }

                    serialData = string.Empty;

                }
            }
            catch (Exception ex)
            {


            }
        }


        CallerIdType_Configuration objCallerId = null;
        static string SocketCLIIP = string.Empty;

        private bool IsSingleMachineCTI;
        private bool CallerIdPopupOnFront;
        private int CallerIdPopupPosition;
        public void LoadCallerIdSettings()
        {

            string rtnMsg = string.Empty;
            this.objCallerId = GeneralBLL.GetObject<CallerIdType_Configuration>(c => c.SysPolicyId == 1);

            bool status = false;

            if (objCallerId != null)
            {
                CallerIdPopupOnFront = objCallerId.CallerIdPopupOnFront.ToBool();
                CallerIdPopupPosition = objCallerId.CallerIdPopupPosition.ToInt();
                // For Analog Caller Id
                if (objCallerId.IsAnalog.ToBool())
                {

                    if (objCallerId.AnalogCLIType == Enums.ANALOG_CLITYPE.ETHERNET)
                    {

                        // objAnalog = new BroadCastMessage();
                        objAnalog.ReceiveMessage += new BroadCastMessage.MessageReceiveDataHandler(obj_ReceiveData);
                        // objAnalog = new CIDLauncher();
                        //  objAnalog.ReceiveData += new CIDLauncher.ReceiveDataHandler(obj_ReceiveData);
                    }
                    else if (objCallerId.AnalogCLIType == Enums.ANALOG_CLITYPE.SERIALPORT)
                    {
                        try
                        {

                            bool error = false;

                            if (comport.IsOpen) comport.Close();
                            else
                            {
                                comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                                comport.BaudRate = int.Parse(objCallerId.SerialBaudRate.ToStr());
                                comport.DataBits = int.Parse(objCallerId.SerialDataBits.ToStr());
                                comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), objCallerId.SerialStopBits.ToStr());
                                comport.Parity = (Parity)Enum.Parse(typeof(Parity), objCallerId.SerialParityBits.ToStr());
                                comport.PortName = objCallerId.SerialComPort.ToStr();

                                try
                                {
                                    comport.Open();
                                }
                                catch (UnauthorizedAccessException) { error = true; }
                                catch (IOException) { error = true; }
                                catch (ArgumentException) { error = true; }

                                if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                            }
                        }
                        catch (Exception ex)
                        {
                            ShowNotification("Exception on Analog Serial Port CLI", ex.Message, "");
                           // MessageBox.Show( + ex.Message);


                        }
                    }
                }

                if (objCallerId.IsVOIP.ToBool())
                {

                    if (objCallerId.VOIPCLIType.ToInt() == 1)
                    {

                        // For VOIP SIP Caller Id
                        objSip = new CIDSipLauncher();
                        objSip.OnInComingCallNotification += new Sipek.Common.CallControl.DIncomingCallNotification(objSip_OnInComingCallNotification);


                        objSip.OnCallStateRefresh += new Sipek.Common.CallControl.DCallStateRefresh(objSip_OnCallStateRefresh);
                        objSip.OnAccountStateChanged += new Sipek.Common.DAccountStateChanged(objSip_OnAccountStateChanged);

                        List<CallerIdVOIP_Configuration> listofVoip = new List<CallerIdVOIP_Configuration>();

                        //if (AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool())
                        //{

                        //    var list = General.GetQueryable<CallerIdVOIP_Configuration>(null).ToList();
                        //    foreach (var item in list)
                        //    {
                        //        listofVoip.Add(item);                              
                        //    }
                        //}
                        //else
                        //{
                        listofVoip = General.GetQueryable<CallerIdVOIP_Configuration>(c => c.UserId == AppVars.LoginObj.LuserId).ToList();
                        //     }


                        List<AccountConfig> listofAccounts = new List<AccountConfig>();
                        foreach (var item in listofVoip)
                        {


                            AccountConfig objAccount = new AccountConfig();
                            objAccount.AccountName = item.AccountId.ToStr();
                            objAccount.DisplayName = item.AccountId.ToStr();

                            objAccount.Id = item.AccountId.ToStr();
                            objAccount.Password = item.Password;
                            objAccount.HostName = item.Host.ToStr();

                            objAccount.UserName = item.AccountId.ToStr();
                            objAccount.ProxyAddress = item.ProxyAddress.ToStr();

                            listofAccounts.Add(objAccount);

                        }

                        if (listofAccounts.Count > 0)
                        {
                            objSip.SetConfiguration(listofAccounts);
                        }

                        if (this.ListofUserRights.Count(c => c.functionId == "DISCARD WAITING CALL ON BUSY") > 0)
                        {
                            DiscardwaitingCalls = true;

                        }



                    }
                    else if (objCallerId.VOIPCLIType.ToInt() == 2)
                    {
                        PopupOnAnswerCTE = AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool();


                        GetExtensions(true);



                        try
                        {

                            //   if (Debugger.IsAttached == false)
                            //    {

                            if (IsServer)
                            {
                                //string path = Application.StartupPath + "\\EurosoftCallerID.exe";


                                //if (File.Exists(path))
                                //{

                                //    Process.Start("EurosoftCallerID.exe");

                                //    try
                                //    {
                                //        File.AppendAllText("clistartlog.txt", "isserver=" + IsServer.ToStr() + ",time:" + DateTime.Now.ToStr());
                                //    }
                                //    catch
                                //    {

                                //    }
                                //}


                                string path = Application.StartupPath + "\\EurosoftCallerID.exe";


                                if (File.Exists(path))
                                {

                                    Process.Start("EurosoftCallerID.exe");

                                    try
                                    {
                                        File.AppendAllText("clistartlog.txt", "isserver=" + IsServer.ToStr() + ",time:" + DateTime.Now.ToStr());
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    path = Application.StartupPath + "\\CallerId\\EurosoftCallerID.exe";


                                    if (File.Exists(path))
                                    {

                                        Process.Start(path);

                                        try
                                        {
                                            File.AppendAllText("newclistartlog.txt", "isserver=" + IsServer.ToStr() + ",time:" + DateTime.Now.ToStr());
                                        }
                                        catch
                                        {

                                        }
                                    }


                                }


                            }
                            //    }
                        }
                        catch
                        {


                        }





                        //  if (IsServer)
                        //    {

                        //if (PopupOnAnswerCTE == false || ListOfExtensions != null)
                        //{
                        //    try
                        //    {

                        //        var objAsterik = General.GetObject<CallerIdVOIP_Configuration>(c => c.Port != null);

                        //        if (objAsterik != null)
                        //        {

                        //            manager = new ManagerConnection(objAsterik.Host.ToStr(), objAsterik.Port.ToInt(), objAsterik.UserName.ToStr(), objAsterik.Password.ToStr());

                        //            manager.NewCallerId += new NewCallerIdEventHandler(manager_NewCallerId);




                        //            manager.NewState += new NewStateEventHandler(manager_NewState);

                        //            try
                        //            {

                        //                manager.Login();

                        //                ShowNotification("VOIP", "VOIP Asterisk is Connected..", null);
                        //            }
                        //            catch (Exception ex)
                        //            {
                        //                manager.Logoff();
                        //                ShowNotification("VOIP", "Asterik Voip Connection Failed : " + ex.Message, null);


                        //            }

                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ShowNotification("Exception on Askterik Voip CLI", ex.Message, null);

                        //    }
                        //}






                    }
                    else if (objCallerId.VOIPCLIType.ToInt() == 4)
                    {

                        PopupOnAnswerCTE = AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool();


                        GetExtensions(true);



                        try
                        {



                            if (IsServer)
                            {



                                string path = Application.StartupPath + "\\EmeraldCallerId\\CallerId.exe";




                                if (File.Exists(path))
                                {

                                    Process.Start(path);

                                    try
                                    {
                                        File.AppendAllText("newclistartlog.txt", "isserver=" + IsServer.ToStr() + ",time:" + DateTime.Now.ToStr());
                                    }
                                    catch
                                    {

                                    }
                                }


                            }



                            //    }
                        }
                        catch
                        {


                        }


                    }

                }
              
                    //else if (objCallerId.VOIPCLIType.ToInt() == 3)
                    //{

                    //    GetExtensions(true);

                    //    string accountUser = ListOfExtensions.FirstOrDefault(c => c.UserMachineName.ToLower().Trim() == Environment.MachineName.ToLower().Trim()).DefaultIfEmpty().CLIExtension.ToStr().Trim();

                    //    if (accountUser.ToStr().Trim().Length == 0)
                    //    {
                    //        objBTVoip = General.GetQueryable<CallerIdVOIP_Configuration>(c => c.ProxyAddress != null && c.Port != null).FirstOrDefault();
                    //    }
                    //    else
                    //    {
                    //        objBTVoip = General.GetQueryable<CallerIdVOIP_Configuration>(c => c.UserName == accountUser && c.ProxyAddress != null && c.Port != null).FirstOrDefault();
                    //    }

                    //    if (objBTVoip != null)
                    //    {
                    //        InitializeBTVOIPConfig();
                    //    }

                     
                    //}


            //    }

                if (objCallerId.IsDigital.ToBool())
                {
                    rtnMsg = string.Empty;

                    // For  Tapi LAN CTE CallerId
                    if (objCallerId.DigitalCLIType.ToInt() == 1)
                    {
                        if (AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool())
                        {
                            GetExtensions(true);

                        }
                        PopupOnAnswerCTE = AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool();

                        objTapi = new CIDTapiLauncher(AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool());
                        objTapi.OnInComingCallNotification += new TAPIEventHandler(objTapi_OnInComingCallNotification);



                        TAPIAccountConfig objTapiAccount = new TAPIAccountConfig();
                        objTapiAccount.Line = objCallerId.TapiDriverName.ToStr();
                        status = objTapi.SetConfiguration(objTapiAccount, ref rtnMsg);
                    }
                    else if (objCallerId.DigitalCLIType.ToInt() == 2)
                    {
                        try
                        {

                            RecordCB = new CDRRecordDelegate(CDRRecord);

                            RecordStopCB = default(CDRStopDelegate);

                            Handle_Renamed = CdrOpenConnection(objCallerId.CdrIP.ToStr().Trim(), objCallerId.CdrUserName.ToStr().Trim()
                                                        , objCallerId.CdrPassword.ToStr().Trim(), RecordCB, RecordStopCB, 0);
                            if (Handle_Renamed == -1)
                            {
                                rtnMsg = "CDR Connection Failed to " + objCallerId.CdrIP.ToStr().Trim();
                                ShowNotification("CDR", rtnMsg, null);
                            }
                            else
                            {
                                ShowNotification("CDR", "CDR is Connected", null);
                            }

                            GetExtensions(true);
                        }
                        catch (Exception ex)
                        {
                            rtnMsg = "CDR Connection Failed to " + ex.Message;
                            ShowNotification("CDR", rtnMsg,null);
                        }

                    }
                    else if (objCallerId.DigitalCLIType.ToInt() == 3)
                    {
                        if (AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool())
                        {
                            GetExtensions(true);
                        }

                        PopupOnAnswerCTE = AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool();
                        IsSingleMachineCTI = objCallerId.IsSingleMachineCTI.ToBool();


                        if (IsSingleMachineCTI == false || (IsServer == true))
                        {
                            string cliPath = Application.StartupPath.Replace("Treasure Cab System", "CallerID") + "\\CALLERIDSERVICE.exe";


                            //try
                            //{
                            //    File.AppendAllText("clipath.txt", cliPath);
                            //}
                            //catch
                            //{

                            //}

                            if (File.Exists(cliPath) == false)
                            {

                                cliPath = Application.StartupPath.Replace("Treasure Cab System", "CallerID") + "\\Debug\\CALLERIDSERVICE.exe";

                            }

                            //try
                            //{
                            //    File.AppendAllText("clipath.txt", cliPath);
                            //}
                            //catch
                            //{

                            //}

                            if (File.Exists(cliPath))
                            {
                                try
                                {
                                    if (System.Diagnostics.Process.GetProcessesByName("CALLERIDSERVICE").Count() == 0)
                                    {
                                        Process.Start(cliPath);

                                        try
                                        {
                                            File.AppendAllText("clistartlog.txt", "isserver=" + IsServer.ToStr() + ",time:" + DateTime.Now.ToStr());
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                catch
                                {


                                }


                            }
                            else
                            {


                                if (IsServer || AppVars.AccessFrom != "remote")
                                {

                                    if (objCallerId.IsVpnCli.ToBool() == false)
                                    {
                                        if (objCallerId.TapiDriverName.ToStr().Trim().Length > 0)
                                        {

                                            objTapiCTI = new CIDCTITapiLauncher(AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool());
                                            objTapiCTI.OnInComingCallNotification += new TAPICTIEventHandler(objTapiCTI_OnInComingCallNotification);

                                            TAPIAccountConfig objTapiAccount = new TAPIAccountConfig();
                                            objTapiAccount.Line = objCallerId.TapiDriverName.ToStr();
                                            status = objTapiCTI.SetConfiguration(objTapiAccount, ref rtnMsg);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (objCallerId.IsFileCLI.ToBool())
                {
                    IsFileCLI = objCallerId.IsFileCLI.ToBool();
                    fileCLIDirPath = objCallerId.FileCliDirPath.ToStr().Trim();

                    GetExtensions(true);


                    listOfAvailableExtensions = General.GetQueryable<CallerId_AvailableExtension>(c => c.ExtensionNo != null).ToList();
                }
            }
        }


       // OzekiSoftPhone.SoftPhone objBTSoftphone = null;

        private string ctiRingNumber = string.Empty;

        private void AddCTIRingCall()
        {

            try
            {

                string name =GetCustomerNameFromCall(ctiRingNumber);
                //Customer objCustomer = GeneralBLL.GetQueryable<Customer>(c => c.TelephoneNo == ctiRingNumber || c.MobileNo == ctiRingNumber)
                //                        .OrderByDescending(C => C.Id).FirstOrDefault();
                //if (objCustomer != null)
                //{
                //    name = objCustomer.Name.ToStr();
                //}

                CreateLog(name, ctiRingNumber, dt.ToDateTime(), "00:00:00", "","");

                string item = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {

                    item = name + " - " + ctiRingNumber + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                }
                else
                {
                    item = ctiRingNumber + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                }


                if (this.InvokeRequired)
                {

                    this.BeginInvoke(new SingleDelegate(AddFileCliCall), new object[] { item });
                }
                else
                {

                    AddFileCliCall(item);
                }
            }
            catch
            {


            }
        }


        private void AddCTIRingCallWithoutLog()
        {

            try
            {

                string name = GetCustomerNameFromCall(ctiRingNumber);
                //Customer objCustomer = GeneralBLL.GetQueryable<Customer>(c => c.TelephoneNo == ctiRingNumber || c.MobileNo == ctiRingNumber)
                //                        .OrderByDescending(C => C.Id).FirstOrDefault();
                //if (objCustomer != null)
                //{
                //    name = objCustomer.Name.ToStr();
                //}

                if (IsServer)
                {
                    CreateLog(name, ctiRingNumber, dt.ToDateTime(), "00:00:00", "","");
                }

                string item = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {

                    item = name + " - " + ctiRingNumber + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                }
                else
                {
                    item = ctiRingNumber + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                }


                if (this.InvokeRequired)
                {

                    this.BeginInvoke(new SingleDelegate(AddFileCliCall), new object[] { item });
                }
                else
                {

                    AddFileCliCall(item);
                }




                BroadcastRemoteCTICall("**cti_remoteincomingcall>>" + ctiRingNumber.ToStr() + ">>" + "XXX" + ">>ring>>" + item, "ring=" + item + "=" + ctiRingNumber);



            }
            catch
            {


            }
        }


        private void BroadcastRemoteCTICall(string message, string socketMessage)
        {

            if (IsServer)
            {


                if (AppVars.objPolicyConfiguration.RemoteIPs.ToStr().Length > 0)
                {


                    new BroadcasterData().BroadCastToAllRemoteIP(message);
                }
                else
                {
                    if (SocketCLIIP.Length > 0)
                    {
                        SendSocketMessage(socketMessage);

                    }
                }


            }
        }


        private DateTime? LastRingCallOn = null;
        private string LastRingNo = string.Empty;
        string socketCTIValue = string.Empty;

        void objTapiCTI_OnInComingCallNotification(string number, string line, bool IsAnswered, string callType, string calledNumber)
        {

            try
            {
                if (number.ToStr().Length > 8)
                {
                    if (number.StartsWith("9"))
                        number = number.Substring(number.Length > 1 ? 1 : number.Length);


                    if (number.StartsWith("00"))
                    {
                        number = number.Substring(number.Length > 1 ? 1 : number.Length);
                    }

                    if (calledNumber.ToStr().Trim().Length > 0)
                    {
                        if (calledNumber.StartsWith("9"))
                            calledNumber = calledNumber.Substring(calledNumber.Length > 1 ? 1 : calledNumber.Length);


                        if (calledNumber.StartsWith("00"))
                        {
                            calledNumber = calledNumber.Substring(calledNumber.Length > 1 ? 1 : calledNumber.Length);
                        }
                    }

                    if (PopupOnAnswerCTE == false)
                    {

                        if (this.InvokeRequired)
                        {
                            Record_delegate d = new Record_delegate(RecordDisplay);
                            this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI, line, calledNumber, number, line });
                        }
                        else
                        {
                            RecordDisplay(eCallerIdType.VOIP_TAPI, line,calledNumber, number, line);
                        }

                        if (IsSingleMachineCTI)
                        {
                            new BroadcasterData().BroadCastToAll("**cti_incomingcall>>" + number.ToStr() + ">>" + line.ToStr().Trim() + ">>ringonly>>" + Environment.MachineName.ToLower());
                        }

                    }
                    else
                    {
                        if (callType == "ring")
                        {
                            ctiRingNumber = number.ToStr();

                            if (IsSingleMachineCTI)
                            {

                                if (LastRingCallOn == null || LastRingNo != ctiRingNumber || (DateTime.Now.Subtract(LastRingCallOn.Value).TotalSeconds >= 2 && ctiRingNumber == LastRingNo))
                                {
                                    LastRingCallOn = DateTime.Now;
                                    LastRingNo = ctiRingNumber;
                                    new Thread(new ThreadStart(AddCTIRingCall)).Start();
                                }
                            }
                            else
                            {
                                new Thread(new ThreadStart(AddCTIRingCallWithoutLog)).Start();

                            }

                        }
                        else if (callType == "answer")
                        {

                            string machineName = Environment.MachineName.ToStr();

                            if (ListOfExtensions != null && ListOfExtensions.Count(c => c.UserMachineName.ToStr().ToLower() == machineName.ToStr().ToLower() && line.ToStr().Contains(c.CLIExtension) == true) > 0)
                            {
                                if (this.InvokeRequired)
                                {
                                    Record_delegate d = new Record_delegate(RecordDisplay);
                                    this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI_CTI, line,calledNumber, number, line });

                                }
                                else
                                {
                                    RecordDisplay(eCallerIdType.VOIP_TAPI_CTI, line,calledNumber, number, line);
                                }

                                if (IsSingleMachineCTI == false && line.ToStr().Trim().Length > 0)
                                {
                                    UpdateLog("", number.ToStr(), DateTime.Now, "00:00:00", line, line, "ANSWER");
                                }

                            }
                            else
                            {
                                if (IsSingleMachineCTI)
                                {

                                    new BroadcasterData().BroadCastToAll("**cti_incomingcall>>" + number.ToStr() + ">>" + line.ToStr().Trim() + ">>answer>>" + Environment.MachineName.ToStr().ToLower());
                                }
                                else
                                {

                                    BroadcastRemoteCTICall(("**cti_remoteincomingcall>>" + number.ToStr() + ">>" + line.ToStr().Trim() + ">>answer>>" + Environment.MachineName.ToStr().ToLower()), "answer=" + number.ToStr() + "=" + line.ToStr().Trim());


                                    //if (IsServer)
                                    //{
                                    //if (line.ToStr().Trim().Length == 0)
                                    //    line = "251";

                                    // AddCTIAnswerSocketCall("answer=" + number.ToStr() + "=" + line.ToStr().Trim());


                                    //if (SocketCLIExtension.ToStr().Trim() == line.ToStr().Trim())
                                    //{
                                    //   ThreadPool.QueueUserWorkItem(new WaitCallback(AddCTIAnswerSocketCall), ("answer=" + number.ToStr() + "=" + line.ToStr().Trim()));



                                    //if (AppVars.objPolicyConfiguration.RemoteIPs.ToStr().Length > 0)
                                    //{

                                    //    new BroadcasterData().BroadCastToAllRemoteIP("**cti_remoteincomingcall>>" + number.ToStr() + ">>" + line.ToStr().Trim() + ">>answer>>" + Environment.MachineName.ToStr().ToLower());
                                    //}


                                    //   }

                                    // ThreadPool.QueueUserWorkItem(new WaitCallback(AddCTIAnswerSocketCall), ("answer=" + number.ToStr() + "=" + line.ToStr().Trim()));

                                    //  }

                                }
                            }


                            if (IsSingleMachineCTI == true && line.ToStr().Trim().Length > 0)
                            {
                                UpdateLog("", number.ToStr(), DateTime.Now, "00:00:00", line, line, "ANSWER");


                            }


                        }

                    }
                }
            }
            catch
            {

            }
        }



        private void AddCTIAnswerSocketCall(object val)
        {

            string v = val.ToStr();
            SendSocketMessage(v);


        }


        //CallerIdVOIP_Configuration objBTVoip = null;
        //private void InitializeBTVOIPConfig()
        //{
        //    try
        //    {
        //        PopupOnAnswerCTE = AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool();

        //        objBTSoftphone = new OzekiSoftPhone.SoftPhone();

        //        var registrationRequired = true;
        //        var userName = objBTVoip.UserName.ToStr().Trim();
        //        var displayName = objBTVoip.UserName.ToStr().Trim();
        //        var authenticationId = objBTVoip.AccountId.ToStr().Trim();
        //        var registerPassword = objBTVoip.Password.ToStr().Trim();
        //        var domainHost = objBTVoip.Host.ToStr().Trim();
        //        var domainPort = objBTVoip.Port.ToInt();
        //        var outboundproxy = objBTVoip.ProxyAddress.ToStr().Trim();

        //        objBTSoftphone.InitializeSoftPhone(registrationRequired, displayName, userName, authenticationId, registerPassword, domainHost, domainPort, outboundproxy, "");

        //        objBTSoftphone.IncomingCall += new OzekiSoftPhone.SoftPhone.IncomingCallDelegate(c_IncomingCall);

        //        objBTSoftphone.PhoneLineStateChange += new OzekiSoftPhone.SoftPhone.PhoneLineStateChangeDelegate(c_PhoneLineStateChange);
        //        objBTSoftphone.CallStateChange += new OzekiSoftPhone.SoftPhone.CallStateChangeDelegate(c_CallStateChange);
        //    }
        //    catch (Exception ex)
        //    {
        //        MethodInvoker invoker = new MethodInvoker(delegate()
        //        {

        //            NotifyMsg("Voip CLI", ex.Message);

        //        });

        //        this.Invoke(invoker);

        //    }
        //}

        private void NotifyMsg(string caption, string content)
        {

            RadDesktopAlert alertErr = new RadDesktopAlert();
            alertErr.CaptionText = caption;
            alertErr.ContentText = content;
            alertErr.AutoCloseDelay = 1000;
            alertErr.Show();
        }

        //void c_CallStateChange(Ozeki.VoIP.CallState cs, Ozeki.VoIP.IPhoneCall c, bool a, int cc)
        //{
        //    if (c.CallState == Ozeki.VoIP.CallState.Ringing)
        //    {

        //        if (PopupOnAnswerCTE == false)
        //        {

        //            if (this.InvokeRequired)
        //            {
        //                Record_delegate d = new Record_delegate(RecordDisplay);
        //                this.BeginInvoke(d, new object[] { eCallerIdType.VOIPBT, "0", "", c.From.UserName, "" });
        //            }
        //            else
        //            {

        //                RecordDisplay(eCallerIdType.VOIPBT, "0", "", c.From.UserName, "");
        //            }
        //        }
        //        else
        //        {
        //            string item = c.From.UserName.ToStr() + "-" + string.Format("{0:HH:mm}", DateTime.Now);

        //            if (this.InvokeRequired)
        //            {

        //                this.BeginInvoke(new SingleDelegate(AddFileCliCall), new object[] { item });
        //            }
        //            else
        //            {

        //                AddFileCliCall(item);
        //            }

        //        }

        //    }
        //    else
        //    {
        //        if (PopupOnAnswerCTE)
        //        {


        //            string extension = c.To.UserName.ToStr().Trim();


        //            if (ListOfExtensions == null)
        //                GetExtensions(false);

                    

        //            if (this.InvokeRequired)
        //            {

        //                Record_delegate d = new Record_delegate(RecordDisplay);
        //                this.BeginInvoke(d, new object[] { eCallerIdType.VOIPBT, "0", "", c.From.UserName, "" });
        //            }
        //            else
        //            {

        //                RecordDisplay(eCallerIdType.VOIPBT, "0", "", c.From.UserName, "");
        //            }
                  

        //        }

        //    }
        //}

        //void c_PhoneLineStateChange(Ozeki.VoIP.PhoneLineState p)
        //{
        //    if (p == Ozeki.VoIP.PhoneLineState.RegistrationSucceeded)
        //    {

        //        //MethodInvoker invoker = new MethodInvoker(delegate()
        //        //{

        //        //    NotifyMsg("Voip CLI", "Connected Successfully");

        //        //});

        //        //this.Invoke(invoker);

        //    }
        //    else
        //    {
        //        //MethodInvoker invoker = new MethodInvoker(delegate()
        //        //{

        //        //    NotifyMsg("Voip CLI", "Connecting...");

        //        //});
        //        //this.Invoke(invoker);
        //    }


        //}

        //void c_IncomingCall(Ozeki.VoIP.IPhoneCall i)
        //{

        //}





        void manager_NewState(object sender, NewStateEvent e)
        {
            string number = e.CallerIdNum;
            try
            {

                string extension = string.Empty;

                if (number.ToStr().Length > 6)
                {

                    string desc = e.ChannelStateDesc;

                    string connectedLineNum = string.Empty;

                    try
                    {


                        connectedLineNum = e.AccountCode.ToStr().Trim();

                        e.Attributes.TryGetValue("exten", out extension);

                        if (connectedLineNum.IsNumeric())
                        {
                            if (connectedLineNum.StartsWith("44"))
                            {
                                connectedLineNum = connectedLineNum.Remove(0, 2);

                                connectedLineNum = connectedLineNum.Insert(0, "0");
                            }
                        }
                        else
                            connectedLineNum = string.Empty;
                        //attributes.TryGetValue("connectedlinename", out connectedLineName);
                        //Console.WriteLine("Connected Number : " + connectedLineNum);
                        //Console.WriteLine("Connected Name : " + connectedLineName);
                    }
                    catch
                    {

                    }




                    if (desc.ToStr().ToUpper() == "UP")
                    {


                        //if (number != "anonymous")
                        //{

                        if (PopupOnAnswerCTE)
                        {
                            Console.WriteLine(e.UniqueId);
                            try
                            {

                                File.AppendAllText(Application.StartupPath + "\\calleridlog.txt", e.ToStr()+Environment.NewLine);

                            }
                            catch
                            {


                            }

                            if (extension.ToStr().Length != 3)
                            {
                                extension = e.CallerIdName.ToStr().ToLower().Replace("ext", "").Trim();
                            }
                            
                            



                            //  string extension = e.CallerIdName.ToStr().ToLower().Replace("ext", "").Trim();

                            if (!string.IsNullOrEmpty(extension))
                            {
                                if (ListOfExtensions != null && ListOfExtensions.Count(c => c.CLIExtension == extension) > 0)
                                {


                                    if (this.InvokeRequired)
                                    {
                                        Record_delegate d = new Record_delegate(RecordDisplay);
                                        this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_ASTERISK, extension, connectedLineNum, number, extension });
                                    }
                                    else
                                    {

                                        RecordDisplay(eCallerIdType.VOIP_ASTERISK, extension, connectedLineNum, number, extension);
                                    }
                                }
                            }


                        }


                        //   }




                    }

                }
            }

            catch (Exception ex)
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {

                   db.stp_AddLog("Ph :" + number,"",Environment.MachineName);

                }
              

            }
           
        }



        #region BT VOIP


        //public SIPPhoneCfgWrap cfgWrap;
        //public bool phoneRunning;
        //public MyGTAPIEnv env;



        //public void On_RecvOffered(int ch, string sCaller, string sCallee, string sDestAddr, string sViaAddr, string sFromIP, ushort nFromPort)
        //{
        //    //  callcnt++;

        //    try
        //    {

        //        //  if (StatusStrip_Label2.Text == "Success")
        //        //  {

        //        Thread th = new Thread(new ThreadStart(RestartBTVOIP));
        //        th.Priority = ThreadPriority.Highest;
        //        th.Start();
        //        //  }

        //        if (sCaller.StartsWith("<sip:"))
        //        {
        //            sCaller = sCaller.Substring(5, sCaller.IndexOf('@') - 5);


        //        }

        //        if (this.InvokeRequired)
        //        {
        //            Record_delegate d = new Record_delegate(RecordDisplay);
        //            this.BeginInvoke(d, new object[] { eCallerIdType.VOIPBT, "0", "", sCaller, "" });
        //        }
        //        else
        //        {

        //            RecordDisplay(eCallerIdType.VOIPBT, "0", "", sCaller, "");
        //        }


        //    }
        //    catch (Exception ex)
        //    {


        //    }

        //}


        //public void OnRestartBTVOIPCLI()
        //{

        //    if (objBTVoip != null)
        //    {

        //        IsRestartingBTVoip = false;

        //        Thread th = new Thread(new ThreadStart(RestartBTVOIP));
        //        th.Start();
        //    }
        //}


        //private bool IsRestartingBTVoip;

        //public void RestartBTVOIP()
        //{
        //    //     timerBTVOIP.Stop();

        //    if (IsRestartingBTVoip)
        //        return;



        //    IsRestartingBTVoip = true;

        //    FreeSIPServer();

        //    InitSIPServer();
        //    IsRestartingBTVoip = false;



        //}


        //public void On_RecvRegStatus(int user_id, int status, int regtime)
        //{

        //    if (env != null)
        //    {

        //        string lbl = env.CFG_GetValue("gtsrv.sip.reg1.username", "");
        //        if (lbl != null && lbl.Length > 0)
        //        {
        //            if (status == 0)
        //                StatusStrip_Label2.Text = "Success.";
        //            else
        //            {
        //                StatusStrip_Label2.Text = "Success";

        //                //  MethodInvoker mi = new MethodInvoker(delegate() { this.CreateAndShowAlert("Registered", "VOIP Success", null, System.Media.SystemSounds.Asterisk, false, "", "", ""); });
        //                //   this.Invoke(mi);



        //            }
        //        }
        //    }

        //}



        //public void InitSIPServer()
        //{
        //    if (phoneRunning)
        //        return;

        //    try
        //    {



        //        //GTAPIASM.GTAPIEnv.g_MixerType = 1; //GTAPI directx API
        //        GTAPIASM.GTAPIEnv.g_MixerType = 0; //C# MIXER API




        //        env = new MyGTAPIEnv();
        //        //    env.mainForm = this;

        //        //    env.SetMainWnd(Handle.ToInt32());


        //        env.CreateEnv();

        //        env.CFG_SetValue("gtsrv.sip.server.model", "0");

        //        //SIP IP Address you want to use on local
        //        //Leave it unset if you want to listen on all the network interface
        //        //CFG_SetValue("gtsrv.sip.ip.address", "");

        //        //SIP Port, default 5060
        //        env.CFG_SetValue("gtsrv.sip.ip.port", cfgWrap.SIPPort.ToString());

        //        //RTP PORT
        //        env.CFG_SetValue("gtsrv.sip.rtpstartrange", cfgWrap.RTPPort.ToString());
        //        //env.CFG_SetValue("gtsrv.sip.rtpendrange", "18800")

        //        //Log
        //        // env.CFG_SetValue("gtsrv.log.level", "4");
        //        //  env.CFG_SetValue("gtsrv.log.filename", "csharpsipphone.txt");

        //        //Prefered codec list
        //        env.CFG_SetValue("gtsrv.sip.prefered.codec", cfgWrap.AudioCodecs);
        //        //env.CFG_SetValue("gtsrv.sip.prefered.video.codec", "34,115,124")

        //        //NAT or STUN
        //        if (cfgWrap.StunServer.Length > 0)
        //        {
        //            env.CFG_SetValue("gtsrv.sip.stun.server", cfgWrap.StunServer);
        //        }
        //        else
        //        {
        //            env.CFG_SetValue("gtsrv.sip.use.nat.addr", cfgWrap.UseNATAddr ? "1" : "0");
        //        }


        //        //   string delay = env.CFG_GetValue("gtsrv.sip.jb.max.delay", "1");

        //        //    env.CFG_SetValue("gtsrv.sip.jb.min.delay", "0");
        //        //  env.CFG_SetValue("gtsrv.sip.jb.max.delay", "1");


        //        //channnel numbers, here we only use 1 channel
        //        env.CFG_SetValue("gtsrv.sip.boardnum.per.server", "1");
        //        env.CFG_SetValue("gtsrv.sip.spannum.per.board", "1");
        //        env.CFG_SetValue("gtsrv.sip.channum.per.span", "1");


        //        //Internal communication port
        //        env.CFG_SetValue("gtsrv.net.port", "9712"); //any port you like



        //        if (cfgWrap.DisplayName.Length > 0 && cfgWrap.UserName.Length > 0 && cfgWrap.Domain.Length > 0 && cfgWrap.Password.Length > 0)
        //        {
        //            env.CFG_SetValue("gtsrv.sip.reg.client.num", "1");
        //            env.CFG_SetValue("gtsrv.sip.reg1.displayname", cfgWrap.DisplayName);
        //            env.CFG_SetValue("gtsrv.sip.reg1.username", cfgWrap.UserName);
        //            env.CFG_SetValue("gtsrv.sip.reg1.domain", objBTVoip.Host.ToStr());
        //            env.CFG_SetValue("gtsrv.sip.reg1.proxy", cfgWrap.Proxy.ToStr());
        //            env.CFG_SetValue("gtsrv.sip.reg1.authorization", cfgWrap.UserName);
        //            env.CFG_SetValue("gtsrv.sip.reg1.password", cfgWrap.Password);
        //            env.CFG_SetValue("gtsrv.sip.reg1.expire", "3600");
        //            if (cfgWrap.RegisterDomain)
        //                env.CFG_SetValue("gtsrv.sip.reg1.register", "1");
        //            else
        //                env.CFG_SetValue("gtsrv.sip.reg1.register", "0");
        //        }

        //        string lbl = string.Empty;

        //        if (cfgWrap.UserName.Length > 0)
        //        {
        //            lbl = env.CFG_GetValue("gtsrv.sip.reg1.username", "") + " XXX";

        //        }
        //        else
        //        {
        //            lbl = "No SIP Account";
        //        }

        //        if (env.StartServer())
        //        {
        //            //env.Send_GetRegStatus(0);

        //            // lbLineStatus.Text = "IDLE";
        //            // btnDial.Text = "Dial";
        //            //  btnDial.Enabled = true;
        //            // btnHungup.Enabled = false;
        //            // btnHold.Enabled = false;
        //            // btnTransfer.Enabled = false;

        //            phoneRunning = true;

        //            //   MethodInvoker mi = new MethodInvoker(delegate() { this.CreateAndShowAlert("Registering","VOIP", null, System.Media.SystemSounds.Asterisk, false, "", "", ""); });
        //            //     this.Invoke(mi);


        //            //  timer1.Enabled = true;

        //            //    return true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //  MethodInvoker mi = new MethodInvoker(delegate() { this.CreateAndShowAlert("Failed", ex.Message, null, System.Media.SystemSounds.Asterisk, false, "", "", ""); });
        //        //  this.Invoke(mi);


        //    }
        //}

        //public void FreeSIPServer()
        //{
        //    try
        //    {
        //        if (env != null && phoneRunning)
        //        {
        //            // timer1.Enabled = false;
        //            phoneRunning = false;

        //            env.StopServer();
        //            env.DestroyEnv();
        //            //  env = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}



        #endregion



        #region ASTERISK VOIP

        //void manager_Hangup(object sender, HangupEvent e)
        //{
        //    string number = e.CallerIdNum;


        //    if (number != "anonymous")
        //    {

        //        string extension = e.CallerIdName.ToStr().ToLower().Replace("ext", "").Trim();


        //        if (ListOfExtensions != null && ListOfExtensions.Count(c => c.CLIExtension == extension) > 0)
        //        {


        //            if (this.InvokeRequired)
        //            {
        //                Record_delegate d = new Record_delegate(RecordDisplay);
        //                this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_ASTERISK, extension, "", number, extension });
        //            }
        //            else
        //            {

        //                RecordDisplay(eCallerIdType.VOIP_ASTERISK, extension, "", number, extension);
        //            }
        //        }
        //    }
        //}


        delegate void AsteriskRingingDelegate(object sender, NewCallerIdEvent e);


        void manager_NewCallerId(object sender, NewCallerIdEvent e)
        {

            try
            {
              
              

                //if (e.CallerIdNum.ToStr() != "anonymous")
                //{


                    if (this.InvokeRequired)
                    {

                        AsteriskRingingDelegate d = new AsteriskRingingDelegate(manager_NewCallerId);
                        this.BeginInvoke(d, sender, e);
                    }

                    else
                    {
                        string name = string.Empty;
                        string number = e.CallerIdNum.ToStr();

                        if (number.StartsWith("9"))
                            number = number.Substring(number.Length > 1 ? 1 : number.Length);

                        if (number.Length > 7)
                        {

                            if (PopupOnAnswerCTE == false)
                            {


                                if (this.InvokeRequired)
                                {
                                    Record_delegate d = new Record_delegate(RecordDisplay);
                                    this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_ASTERISK, "", "", number, "" });
                                }
                                else
                                {

                                    RecordDisplay(eCallerIdType.VOIP_ASTERISK, "", "", number, "");
                                }


                            }
                            else
                            {
                                string connectedLineNum = string.Empty;
                                string callType = string.Empty;
                                try
                                {

                                 
                                  
                              

                                     e.Attributes.TryGetValue("accountcode", out connectedLineNum);
                                     e.Attributes.TryGetValue("channelstatedesc", out callType);

                                    if (connectedLineNum.IsNumeric())
                                    {
                                        if (connectedLineNum.StartsWith("44"))
                                        {
                                            connectedLineNum = connectedLineNum.Remove(0, 2);

                                            connectedLineNum = connectedLineNum.Insert(0, "0");
                                        }
                                    }
                                    else
                                        connectedLineNum = string.Empty;
                                  
                                }
                                catch
                                {

                                }


                                if (callType.ToStr().ToLower() == "ring")
                                {

                                    name = GetCustomerNameFromCall(number);

                                    string item = string.Empty;
                                    if (!string.IsNullOrEmpty(name))
                                    {
                                        item = name + " - " + number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                                    }
                                    else
                                    {
                                        item = number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                                    }





                                    if (connectedLineNum.Length > 6)
                                    {

                                        if (listofSubcompanyNumbers == null)
                                        {
                                            using (TaxiDataContext db = new TaxiDataContext())
                                            {
                                                listofSubcompanyNumbers = new List<Gen_SubCompany>();

                                                foreach (var itemI in db.Gen_SubCompanies)
                                                {
                                                    listofSubcompanyNumbers.Add(new Gen_SubCompany { Id = itemI.Id, CompanyName = itemI.CompanyName, ConnectionString = itemI.ConnectionString });


                                                }

                                            }
                                        }


                                        if (listofSubcompanyNumbers != null)
                                        {

                                            connectedLineNum = listofSubcompanyNumbers.FirstOrDefault(c => c.ConnectionString.Contains(connectedLineNum)).DefaultIfEmpty().CompanyName.ToStr();


                                            if (connectedLineNum.ToStr().Length > 0)
                                            {
                                                item += "(" + connectedLineNum + ")";


                                            }


                                        }


                                    }


                                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).AddCall(item);
                                    
                                }
                            }
                        }

                    }
              //  }
            }
            catch (Exception ex)
            {


            }

        }

        List<Gen_SubCompany> listofSubcompanyNumbers = null;


        #endregion


        private void ShowNotification(string caption, string content,object tag)
        {

            try
            {
                RadDesktopAlert alertNotify = new RadDesktopAlert();
                alertNotify.CaptionText = caption;
                alertNotify.ContentText = content;
                alertNotify.PopupAnimationEasing = RadEasingType.InCubic;
                //alertNotify.AutoCloseDelay = 25;


                if (tag.ToStr().Length > 0)
                {

                    int width = 300;
                    int height = 150;

                    alertNotify.FixedSize = new Size(width, height);

                    alertNotify.ContentImage = Resources.Resource1.ivr;

                    alertNotify.ShowOptionsButton = true;

                    if (tag != null)
                    {
                        RadButtonElement bookingItem = new RadButtonElement();
                        bookingItem.Tag = tag;
                        bookingItem.Click += new EventHandler(bookingItem_Click);
                        bookingItem.Text = "View Booking";

                        alertNotify.ButtonItems.Add(bookingItem);
                    }

                    try
                    {
                        using (System.Media.SoundPlayer sp = new System.Media.SoundPlayer(System.Windows.Forms.Application.StartupPath + "\\sound\\ivrconfirmbooking.wav"))
                        {

                            sp.Play();

                        }

                    }
                    catch
                    {

                    }

                }

                alertNotify.Show();

            }
            catch
            {


            }

        }

        void bookingItem_Click(object sender, EventArgs e)
        {
            try
            {
                RadButtonElement obj = (RadButtonElement)sender;

                if (obj.Tag.ToStr().Length > 0)
                {

                    General.ShowBookingForm(obj.Tag.ToInt(), true, "", "", 13);


                }
            }
            catch
            {


            }


        }


       


        private void GetExtensions(bool all)
        {
            this.ListOfExtensions = GetCLIExtensions(all);
        }


        List<ClsCallerIdExtensions> ListOfExtensions = null;

        public static List<ClsCallerIdExtensions> GetCLIExtensions(bool all)
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\Service.xml";
                if (File.Exists(path))
                {

                    string machineName = System.Environment.MachineName.ToStr().ToLower();

                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    XmlNode ParentNode = doc.GetElementsByTagName("extensions").OfType<XmlNode>().FirstOrDefault();

                    if (all)
                    {
                        if (ParentNode.ChildNodes.Count > 2)
                        {

                            return ParentNode.ChildNodes.OfType<XmlNode>()
                                       .Select(c => new ClsCallerIdExtensions { UserMachineName = c.FirstChild.InnerText, CLIExtension = c.ChildNodes[1].InnerText, ForwardNumber = c.LastChild.InnerText }).ToList();

                        }

                        else
                        {


                            return ParentNode.ChildNodes.OfType<XmlNode>()
                                       .Select(c => new ClsCallerIdExtensions { UserMachineName = c.FirstChild.InnerText, CLIExtension = c.LastChild.InnerText }).ToList();
                        }
                    }
                    else
                    {
                        if (ParentNode.ChildNodes.Count > 2)
                        {

                            return ParentNode.ChildNodes.OfType<XmlNode>().Where(c => c.FirstChild.InnerText.ToStr().ToLower().Trim() == machineName)
                                        .Select(c => new ClsCallerIdExtensions { UserMachineName = c.FirstChild.InnerText, CLIExtension = c.ChildNodes[1].InnerText, ForwardNumber = c.LastChild.InnerText }).ToList();


                        }
                        else
                        {
                            return ParentNode.ChildNodes.OfType<XmlNode>().Where(c => c.FirstChild.InnerText.ToStr().ToLower().Trim() == machineName)
                                        .Select(c => new ClsCallerIdExtensions { UserMachineName = c.FirstChild.InnerText, CLIExtension = c.LastChild.InnerText }).ToList();
                        }
                    }



                }
                else
                    return null;

            }
            catch (Exception ex)
            {

                return null;

            }


        }




        void btnCallHistory_Click(object sender, EventArgs e)
        {
            if (this.objCallerId != null)
            {
                //ClsDataTransfer polObj = new ClsDataTransfer();

                //foreach (System.Reflection.PropertyInfo item in AppVars.objPolicyConfiguration.GetType().GetProperties())
                //{
                //    try
                //    {

                //        if (polObj.GetType().GetProperty(item.Name) != null)
                //            polObj.GetType().GetProperty(item.Name).SetValue(polObj, item.GetValue(AppVars.objPolicyConfiguration, null), null);
                //    }
                //    catch
                //    {


                //    }
                //}
                

                //polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");
                //string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");
                //string s = "XXX";
                //Process pp = new Process();
                //pp.StartInfo.FileName = Application.StartupPath + "\\Booking\\TreasureBooking.exe";
                //pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");
                //pp.StartInfo.Arguments += " " + "frmCallHistory" + " " + 0 + " " + "true" + " " + 0;
                //pp.Start();

                frmCallHistory frm = new frmCallHistory(this.objCallerId);
                frm.ControlBox = true;
                frm.MaximizeBox = false;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.StartPosition = FormStartPosition.CenterScreen;
                frm.ShowDialog();

                frm.Dispose();
                GC.Collect();
            }
        }

        void btnConfiguration_Click(object sender, EventArgs e)
        {
            frmSysPolicy frm = new frmSysPolicy(true);
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            frm.ControlBox = true;
            frm.MaximizeBox = false;
            frm.Size = new Size(750, 600);
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.ShowDialog();

            frm.Dispose();
        }





        #region ANALOG CALLERID


        void obj_ReceiveData(string line, string name, string phone)
        {



            


            if (this.InvokeRequired)
            {
                Record_delegate d = new Record_delegate(RecordDisplay);
                this.BeginInvoke(d, new object[] { eCallerIdType.Analog, line, name, phone, "" });
            }
            else
            {

                RecordDisplay(eCallerIdType.Analog, line, name, phone, "");


            }

            // SocketCLIIP = "86.148.115.168";
            //////SocketCLIIP = AppVars.objPolicyConfiguration.ListenerIP.ToStr().Trim();

            //ctiRingNumber = "07411330306";

            //ThreadPool.QueueUserWorkItem(new WaitCallback(AddCTIAnswerSocketCall), ("answer=" + phone.ToStr() + "==" + line.ToStr().Trim()));

            // new Thread(new ThreadStart(AddCTIRingCallWithoutLog)).Start();

            //            SendSocketMessage("ring=" + phone + "=" + line);
            //  ThreadPool.QueueUserWorkItem(new WaitCallback(AddCTIAnswerSocketCall), ("answer=" + phone.ToStr() + "=" + line.ToStr().Trim()));

        }

        public static bool SendSocketMessage(string msg)
        {


            bool rtn = false;



            byte[] data = Encoding.UTF8.GetBytes(msg);
            try
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    try
                    {

                        tcpClient.SendTimeout = 3000;
                        tcpClient.ReceiveTimeout = 2000;


                        IPAddress ip = null;

                        if (IPAddress.TryParse(SocketCLIIP, out ip))
                            tcpClient.Connect(new IPEndPoint(ip, 1102));
                        else
                            tcpClient.Connect(SocketCLIIP, 1102);


                        tcpClient.GetStream().Write(data, 0, data.Length);

                        Byte[] inputBuffer = new Byte[200];
                        int bytes = tcpClient.GetStream().Read(inputBuffer, 0, inputBuffer.Length);
                        string dataValue = Encoding.UTF8.GetString(inputBuffer, 0, bytes);
                        tcpClient.Close();



                        //   GC.Collect();

                        if (dataValue.ToStr() == "ok" || dataValue.ToStr().StartsWith("ok"))
                        {
                            rtn = true;
                        }
                        else
                            rtn = false;
                    }
                    catch (Exception ex)
                    {

                        rtn = false;
                        using (TcpClient tcpClient2 = new TcpClient())
                        {
                            try
                            {

                                tcpClient2.SendTimeout = 3000;
                                tcpClient2.ReceiveTimeout = 2000;


                                IPAddress ip = null;

                                if (IPAddress.TryParse(SocketCLIIP, out ip))
                                    tcpClient2.Connect(new IPEndPoint(ip, 1102));
                                else
                                    tcpClient2.Connect(SocketCLIIP, 1102);




                                tcpClient2.GetStream().Write(data, 0, data.Length);

                                Byte[] inputBuffer = new Byte[200];
                                int bytes = tcpClient2.GetStream().Read(inputBuffer, 0, inputBuffer.Length);
                                string dataValue = Encoding.UTF8.GetString(inputBuffer, 0, bytes);
                                tcpClient2.Close();


                                //   GC.Collect();

                                if (dataValue.ToStr() == "ok" || dataValue.ToStr().StartsWith("ok"))
                                {
                                    rtn = true;
                                }
                                else
                                    rtn = false;
                            }
                            catch (Exception ee)
                            {

                                rtn = false;

                            }

                        }

                    }

                }

                GC.Collect();
            }
            catch (Exception eex)
            {
                rtn = false;
            }

            return rtn;


        }

        #endregion

        #region VOIP_SIP CALLERID

        void objSip_OnAccountStateChanged(int accountId, int accState)
        {
            // MUST synchronize threads
            if (InvokeRequired)
                this.BeginInvoke(new DAccountStateChanged(OnRegistrationUpdate), new object[] { accountId, accState });
            else
                OnRegistrationUpdate(accountId, accState);
        }

        void objSip_OnCallStateRefresh(int sessionId)
        {
            // MUST synchronize threads
            if (InvokeRequired)
                this.BeginInvoke(new DCallStateRefresh(OnStateUpdate), new object[] { sessionId });
            else
                OnStateUpdate(sessionId);
        }

        #region Synhronized callbacks
        public virtual void OnRegistrationUpdate(int accountId, int accState)
        {
            string status = "Connecting...";
            int accountState = accState.ToInt();
            if (accountState == 200)
            {
                status = "Success";
                this.StatusStrip_Label2.ForeColor = Color.Green;
            }
            else if (accountState == 408 || accountState == 503)
            {
                status = "Failed";
                this.StatusStrip_Label2.ForeColor = Color.Red;
            }
            this.StatusStrip_Label2.Text = status;

        }

        public virtual void OnStateUpdate(int sessionId)
        {


        }

        #endregion



        private bool DiscardwaitingCalls = false;
        private bool DiscardWaitingCallOnBusyAgent()
        {
            if (DiscardwaitingCalls && Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBooking") > 0
                && Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBooking" && c.Tag != null) > 0)
            {
                return true;

            }
            else
                return false;

        }

        void objSip_OnInComingCallNotification(int sessionId, string number, string info)
        {
            try
            {
                if (DiscardWaitingCallOnBusyAgent())
                    return;



                if (number.StartsWith("00"))
                {
                    number = number.Substring(1);

                }

                if (info.ToStr() == "Answer")
                {
                    if (AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool())
                    {

                        string extension = objSip.Config.Accounts[0].AccountName;


                        if (extension.Length > 5)
                        {
                            extension = extension.Substring(0, 4);

                        }

                        if (this.InvokeRequired)
                        {
                            Record_delegate d = new Record_delegate(RecordDisplay);
                            this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_SIP, extension, "", number, extension });
                        }
                        else
                        {

                            RecordDisplay(eCallerIdType.VOIP_SIP, extension, "", number, extension);
                        }
                    }
                }
                else
                {

                    if (AppVars.objPolicyConfiguration.ShowCLIPopupOnAnswer.ToBool() == false)
                    {

                        if (this.InvokeRequired)
                        {
                            Record_delegate d = new Record_delegate(RecordDisplay);
                            this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_SIP, sessionId.ToStr(), info, number, "" });
                        }
                        else
                        {

                            RecordDisplay(eCallerIdType.VOIP_SIP, sessionId.ToStr(), info, number, "");
                        }
                    }

                }
            }
            catch (Exception ex)
            {


            }

        }


        #endregion


        #region VOIP_TAPI CALLERID

        void objTapi_OnInComingCallNotification(string number, string line, bool IsAnswered)
        {

            if (PopupOnAnswerCTE == false)
            {

                if (this.InvokeRequired)
                {
                    Record_delegate d = new Record_delegate(RecordDisplay);
                    this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI, line, "", number, line });
                }
                else
                {

                    RecordDisplay(eCallerIdType.VOIP_TAPI, line, "", number, line);
                }
            }
            else
            {


                string machineName = Environment.MachineName.ToStr();

                if (ListOfExtensions != null && ListOfExtensions.Count(c => c.UserMachineName.ToStr().ToLower() == machineName.ToStr().ToLower() && line.Contains(c.CLIExtension) == true) > 0)
                {
                    if (this.InvokeRequired)
                    {

                        if (IsAnswered)
                        {
                            Record_delegate d = new Record_delegate(RecordDisplay);
                            this.BeginInvoke(d, new object[] { eCallerIdType.VOIP_TAPI, line, "", number, line });
                        }
                        else
                        {
                            /// create log

                        }
                    }
                    else
                    {
                        if (IsAnswered)
                        {
                            RecordDisplay(eCallerIdType.VOIP_TAPI, line, "", number, line);
                        }
                        else
                        {
                            // create log

                        }
                    }

                }
                else
                {

                    new BroadcasterData().BroadCastToAll("**incomingcall>>" + number.ToStr() + ">>" + line.ToStr().Trim());

                }






            }
        }


        //private void CreateCTELog(string number,string line)
        //{

        //    string name = string.Empty;

        //    if (number.StartsWith("9"))
        //        number = number.Substring(number.Length > 1 ? 1 : number.Length);

        //    Customer objCustomer = GeneralBLL.GetQueryable<Customer>(c => c.TelephoneNo == number || c.MobileNo == number).OrderByDescending(C => C.Id).FirstOrDefault();
        //    if (objCustomer != null)
        //    {
        //        name = objCustomer.Name.ToStr();
        //    }



        //    DateTime callDate = DateTime.Now;
        //    CreateLog(name, number, callDate, "00:00:00", line);
        //    //  new TaxiDataContext().stp_AddCallLog(name, number, callDate, "00:00:00", line);


        //    string item = string.Empty;
        //    if (!string.IsNullOrEmpty(name))
        //    {

        //        item = name + " - " + number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
        //    }
        //    else
        //    {
        //        item = number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
        //    }

        //    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).AddCall(item);


        //}

        #endregion



        #region LAN CTE CALLERID

        public int CDRRecord(string bStrMsg, int applicationData)
        {
            GC.Collect();
            appendText(ref bStrMsg);
            return 0;
        }



        bool PopupOnAnswerCTE;


        public void appendText(ref string msg)
        {
            try
            {
                GC.KeepAlive(RecordCB);

                if (this.InvokeRequired)
                {
                    appendTextCallback d = new appendTextCallback(appendText);
                    this.BeginInvoke(d, msg);
                }
                else
                {


                    if (msg != null && msg.Length > 0 && (msg.Contains("U A")))
                    {
                        string[] arr = msg.Split(new string[] { "\r\n", "  " }, StringSplitOptions.None);

                        string number = arr[4].ToStr().Trim();

                        string stn = arr[2].ToStr().Trim();


                        if (ListOfExtensions != null)
                        {
                            string line = string.Empty;
                            int stIdx = arr[0].LastIndexOf(" ");
                            if (stIdx > 0)
                            {
                                line = arr[0].Substring(stIdx);
                            }

                            if (ListOfExtensions.Count(c => c.UserMachineName.Trim().ToLower() == Environment.MachineName.Trim().ToLower() && c.CLIExtension == stn) == 0)
                            {
                                ClsCallerIdExtensions objExt = ListOfExtensions.FirstOrDefault(c => c.CLIExtension == stn);
                                if (objExt != null && (new Ping().Send(objExt.UserMachineName, 1000).Status != IPStatus.Success))
                                {

                                    RecordDisplay(eCallerIdType.TAPI_Digital, line, "", number, stn);

                                }
                            }
                            else
                            {


                                RecordDisplay(eCallerIdType.TAPI_Digital, line, "", number, stn);
                            }

                        }

                    }
                    else if (msg != null && msg.Length > 0 && msg.Contains("U G"))
                    {
                        try
                        {
                            string[] arr = msg.Split(new string[] { "\r\n", "  " }, StringSplitOptions.None);

                            int idx = 0;
                            string number = arr[6].ToStr().Trim();


                            string line = msg.Remove(idx);
                            int stIdx = arr[0].LastIndexOf(" ");
                            if (stIdx > 0)
                            {
                                line = arr[0].Substring(stIdx);

                            }

                            string name = GetCustomerNameFromCall(number);

                            if (number.StartsWith("9"))
                                number = number.Substring(number.Length > 1 ? 1 : number.Length);

                            //Customer objCustomer = GeneralBLL.GetQueryable<Customer>(c => c.TelephoneNo == number || c.MobileNo == number).OrderByDescending(C => C.Id).FirstOrDefault();
                            //if (objCustomer != null)
                            //{
                            //    name = objCustomer.Name.ToStr();
                            //}



                            DateTime callDate = DateTime.Now;
                            CreateLog(name, number, callDate, "00:00:00", line,"");
                            //  new TaxiDataContext().stp_AddCallLog(name, number, callDate, "00:00:00", line);


                            string item = string.Empty;
                            if (!string.IsNullOrEmpty(name))
                            {

                                item = name + " - " + number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                            }
                            else
                            {
                                item = number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                            }

                            (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).AddCall(item);

                        }
                        catch (Exception ex)
                        {


                        }
                    }


                    else if (msg != null && msg.Length > 0 && msg.Contains("U R"))
                    {
                        try
                        {

                            string[] arr = msg.Split(new string[] { "\r\n", "  " }, StringSplitOptions.None);

                            int idx = 0;
                            string number = arr[4].ToStr().Trim();


                            string line = msg.Remove(idx);
                            int stIdx = arr[0].LastIndexOf(" ");
                            if (stIdx > 0)
                            {
                                line = arr[0].Substring(stIdx);

                            }

                            string duration = arr[59].ToStr();

                            string stn = arr[17].Replace("STN =", "").Trim();

                            if (number.StartsWith("9"))
                                number = number.Substring(number.Length > 1 ? 1 : number.Length);

                            UpdateLog("", number, DateTime.Now, duration, line, stn, "HANGUP");


                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (msg != null && msg.Length > 0 && msg.Contains("U N"))
                    {
                        try
                        {

                            string[] arr = msg.Split(new string[] { "\r\n", "  " }, StringSplitOptions.None);


                            string number = arr[6].ToStr().Trim();

                            if (number.StartsWith("9"))
                                number = number.Substring(number.Length > 1 ? 1 : number.Length);

                            MissedCallLog(number);

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

            }
            catch (Exception ex)
            {


            }

        }



        #endregion





        private void RecordDisplay(eCallerIdType callerIdType, string line, string calledNumber, string phone, string stn)
        {
            try
            {

                if (phone.StartsWith("9"))
                    phone = phone.Substring(phone.Length > 1 ? 1 : phone.Length);


                if (phone.StartsWith("00"))
                {
                    phone = phone.Substring(phone.Length > 1 ? 1 : phone.Length);
                }

                if (phone.Length < 8) return;


                //if (callerIdType != eCallerIdType.VOIP_TAPI_CTI)
                //{

                //    if (AppVars.openedPhoneNo == phone && callerIdType != eCallerIdType.VOIPBT) return;
                //}

                //if (callerIdType != eCallerIdType.VOIP_ASTERISK)
                //{
                //    AppVars.openedPhoneNo = phone;
                //}



               
                    if (Application.OpenForms.OfType<Form>().Count(c => c.Tag.ToStr() == phone) > 0)
                        return;
               

                string custName = string.Empty;
                string address = "";
                string doorNo = "";
                string email = "";
                string notes="";
                Color clrBack = Color.LightGoldenrodYellow;
                string BlackListReason = string.Empty;
                int? AccountId = null;
                bool IsAccountCall = false;
                string subCompanyName = string.Empty;
                int? subCompanyId=null;
                string excludedDriverIds = "";

             
                stp_GetCallerInfoResult objCustomer=null;
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    try
                    {
                        db.CommandTimeout = 4;
                        objCustomer = db.stp_GetCallerInfo(phone, calledNumber).FirstOrDefault();
                    }
                    catch
                    {

                    }
                }

              
                if (objCustomer != null)
                {

                    if (objCustomer.Name.ToStr().ToLower().StartsWith("driver "))
                        return;


                    custName = objCustomer.Name.ToStr();
                    address = objCustomer.Address1.ToStr().Trim();
                    doorNo = objCustomer.DoorNo.ToStr().Trim();
                    email = objCustomer.Email.ToStr().Trim();
                    notes= objCustomer.Notes.ToStr().Trim();
                    // If Customer is Black Listed
                    if (objCustomer.BlackList.ToBool())
                    {
                        clrBack = Color.LightGray;
                        BlackListReason = objCustomer.BlackListResion.ToStr().Trim();
                    }

                    AccountId = objCustomer.AccountId;
                    IsAccountCall = objCustomer.IsAccount.ToBool();

                    subCompanyId = objCustomer.SubCompanyId.ToInt();
                    subCompanyName = objCustomer.SubCompanyName.ToStr();
                    excludedDriverIds = objCustomer.ExcludedDriverIds.ToStr().Trim();


                    if (excludedDriverIds.ToStr().Trim().Length > 0 && excludedDriverIds.ToStr().Trim().Contains(",") == false)
                        excludedDriverIds = "," + excludedDriverIds + ",";
                }



                if (subCompanyId == null)
                {
                    subCompanyId = AppVars.objSubCompany.Id;
                    subCompanyName = AppVars.objSubCompany.CompanyName.ToStr();

                }

                DateTime callDate = DateTime.Now;

                if (callerIdType != eCallerIdType.TAPI_Digital && callerIdType != eCallerIdType.FILE_CLI && callerIdType != eCallerIdType.VOIP_TAPI_CTI)
                {
                    CreateLog(custName, phone, callDate, "00:00:00", stn,calledNumber);
                }

              




                    CallerIdPopup pWindow = new CallerIdPopup(callerIdType, custName, phone, doorNo, address, email, callDate, true, line, IsAccountCall, AccountId);
                
                    pWindow.ShowWindowOnFront = CallerIdPopupOnFront;
                    pWindow.WindowPositionType = CallerIdPopupPosition;
                    pWindow.BlackListReason = BlackListReason;
                    pWindow.lblPopuptext.BackColor = clrBack;
                    pWindow.pictureBox1.BackColor = clrBack;
                    pWindow.txtAddress.BackColor = clrBack;
                    pWindow.txtDoorNo.BackColor = clrBack;
                    pWindow.pnlBottom.BackColor = clrBack;
                    pWindow.CalledToSubcompanyId = subCompanyId;
                    pWindow.customerNotes = notes;
                    pWindow.excludedDriversList = excludedDriverIds;

                    pWindow.PopupText = custName;
                    if (phone != custName) //sometimes the name and phone are the same (Private, Outofarea, etc.)
                    {
                        pWindow.PopupText = custName + " " + phone;
                    }
                    else
                    {
                        pWindow.PopupText = custName + " " + phone;
                    }


                    if (clrBack == Color.LightGray)
                    {
                        pWindow.Text += " <<Black Listed Customer>> ";

                    }


                    if (subCompanyId != null)
                    {
                        if (pWindow.Text.Length == 0)
                        {
                            pWindow.Text = subCompanyName;
                        }
                        else
                        {

                            pWindow.Text += " " + subCompanyName;
                        }
                    }

                    if (CallerIdPopupOnFront)
                    {
                        if (CallerIdPopupPosition == 2)
                        {
                            pWindow.StartPosition = FormStartPosition.CenterParent;
                        }
                        
                        pWindow.ShowDialog();
                        pWindow.Dispose();

                        GC.Collect();
                    }
                    else
                    {
                        pWindow.Show();
                    }
              //  }

            }
            catch
            {


            }
        }

        public void CreateLog(string name, string phoneNumber, DateTime date, string duration, string line,string calledNumber)
        {
            try
            {
                using (TaxiDataContext db = new TaxiDataContext())
                {
                    db.CommandTimeout = 4;
                    db.stp_AddCallLog(name, phoneNumber, date, duration, line, this.ObjLoginUser.LuserId.ToIntorNull(), calledNumber);
                }
            }
            catch
            {

            }
       
        }

        public void UpdateLog(string name, string phoneNumber, DateTime date, string duration, string line, string stn, string callType)
        {

            using (TaxiDataContext db = new TaxiDataContext())
            {
                try
                {
                    db.CommandTimeout = 4;

                    var obj = db.GetTable<CallHistory>().Where(c => c.PhoneNumber == phoneNumber).OrderByDescending(c => c.Id).FirstOrDefault();

                    if (obj != null)
                    {

                        if (!string.IsNullOrEmpty(line.ToStr()))
                        {
                            obj.Line = line.ToStr();
                        }

                        obj.Line = line;


                        if (!string.IsNullOrEmpty(stn.ToStr()))
                        {
                            obj.STN = stn.ToStr();
                        }

                        obj.CallDuration = string.Format("{0:hh:mm:ss}", DateTime.Now.Subtract(obj.CallDateTime.Value.AddSeconds(2).ToDateTime()));

                        if (obj.CallDuration.Contains('.'))
                            obj.CallDuration = obj.CallDuration.Remove(obj.CallDuration.IndexOf('.'));


                        if (listOfAvailableExtensions != null && callType.ToStr() == "ANSWERED")
                        {
                            obj.IsAccepted = false;
                            obj.AnsweredDateTime = DateTime.Now;

                        }


                        if (name.ToStr().Trim().Length > 0)
                        {
                            obj.Name = name;

                        }

                        db.SubmitChanges();
                    }

                }
                catch (Exception ex)
                {


                }


            }


            //CallHistory call = General.GetQueryable<CallHistory>(c => c.PhoneNumber == phoneNumber).OrderByDescending(c => c.Id).FirstOrDefault();

            //if (call != null)
            //{
            //    CallHistoryBO obj = new CallHistoryBO();

            //    obj.GetByPrimaryKey(call.Id);
            //    if (obj.Current != null)
            //    {
            //        if (!string.IsNullOrEmpty(line.ToStr()))
            //        {
            //            obj.Current.Line = line.ToStr();
            //        }

            //        obj.Current.Line = line;


            //        if (!string.IsNullOrEmpty(stn.ToStr()))
            //        {
            //            obj.Current.STN = stn.ToStr();
            //        }


            //        obj.Current.CallDuration = string.Format("{0:hh:mm:ss}", DateTime.Now.Subtract(obj.Current.CallDateTime.Value.AddSeconds(2).ToDateTime()));

            //        if (obj.Current.CallDuration.Contains('.'))
            //            obj.Current.CallDuration = obj.Current.CallDuration.Remove(obj.Current.CallDuration.IndexOf('.'));


            //        if (listOfAvailableExtensions != null && callType.ToStr() == "ANSWERED")
            //        {
            //            obj.Current.IsAccepted = false;
            //            obj.Current.AnsweredDateTime = DateTime.Now;

            //        }


            //        obj.Save();
            //    }
            //}


        }

        public void MissedCallLog(string phoneNumber)
        {
            try
            {
                CallHistoryBO obj = new CallHistoryBO();

                CallHistory call = General.GetQueryable<CallHistory>(c => c.PhoneNumber == phoneNumber).OrderByDescending(c => c.Id).FirstOrDefault();

                if (call != null)
                {
                    obj.GetByPrimaryKey(call.Id);
                    if (obj.Current != null)
                    {

                        obj.Current.IsAccepted = true;

                        obj.Save();
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }

        public override void OnLoadFormWithRights()
        {


            var rights = General.GetQueryable<UM_SecurityGroup_Permission>(c => c.SecurityGroupId == this.ObjLoginUser.LgroupId);

            this.ListofUserRights = (from a in rights
                                     select new UI.UserRights
                                     {
                                         formFunctionId = a.FormFunctionId,
                                         formId = a.UM_FormFunction.FormId,
                                         formName = a.UM_FormFunction.UM_Form.FormName,
                                         formTitle = a.UM_FormFunction.UM_Form.FormTitle,
                                         functionId = a.UM_FormFunction.UM_Function.FunctionName,
                                         moduleId = a.UM_FormFunction.UM_Form.ModuleId,
                                         moduleName = a.UM_FormFunction.UM_Form.UM_Module.ModuleName,
                                         formType = a.UM_FormFunction.UM_Form.FormType

                                     }).ToList();


            AppVars.listUserRights = this.ListofUserRights;
        }

        private void menu_Item_Click(object sender, EventArgs e)
        {
            RadSplitButtonElement btn = (RadSplitButtonElement)sender;
            if (btn.Tag == null) return;

            ShowFormInDock(btn.Tag.ToStr());

        }


        private void ShowFormInDock(string formName)
        {

            this.ShowForm(formName);
            this.radDock1.ActiveWindow = this.GetDockByName(formName + "1");

            if (formName == "frmBookingDashBoard")
            {
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).GetMainDashBoard();
            }

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            OnLogout();
        }

        protected override bool OnLogout()
        {

            try
            {

                var logout = base.OnLogout();
                if (logout)
                {
                    AppVars.IsLogout = true;
                    this.FormClosing -= new FormClosingEventHandler(frmMainMenu_FormClosing);
                    DisposeData();


                    try
                    {
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            db.CommandTimeout = 3;
                            db.stp_ControlerLogins(1, AppVars.LoginObj.LsessionId.ToInt(), null, Environment.MachineName);
                        }
                    }
                    catch
                    {

                    }
                   
                    
                  //  Application.Exit();

                    Process p = Process.GetCurrentProcess();
                    p.Kill();
                    p.Close();
                   
                  
                }



                return logout;
            }
            catch (Exception ex)
            {
              
                try
                {
                    Thread.Sleep(1000);

                    Application.Exit();
                }
                catch (Exception ee)
                {


                }

                return false;

            }

        }

        private void frmMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var logout = base.OnLogout();
                if (logout)
                {
                    AppVars.IsLogout = true;

                    this.FormClosing -= new FormClosingEventHandler(frmMainMenu_FormClosing);


                    DisposeData();


                    Process p=  Process.GetCurrentProcess();
                    p.Close();
                    p.Kill();
                  
                  //  Application.Exit();
                }
                else
                    e.Cancel = true;
            }
            catch (Exception ex)
            {


                try
                {
                    new TaxiDataContext().stp_AddLog(ex.Message + ",Stack Trace :" + ex.StackTrace, "MainMenu", "OnLogout");

                    Thread.Sleep(1000);

                    Application.Exit();
                }
                catch (Exception ee)
                {


                }
                //e.Cancel = true;
                // new TaxiDataContext().stp_AddLog(ex.Message + ",Stack Trace :" + ex.StackTrace, "MainMenu", "OnLogout");

            }
        }


        private void DisposeData()
        {


          


            if (comport != null && comport.IsOpen)
            {
                comport.Close();
            }

            if (manager != null)
                manager.Logoff();



            //if (objApp != null)
            //{
            //    objPDAMap.Saved = true;
            //    objApp.Quit();

            //}


            timer1.Stop();


            (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).CloseFormOnLogout();

        }

        private void btn_RefreshForm_Click(object sender, EventArgs e)
        {
            btn_RefreshForm.Enabled = false;
            // this.radDock1.activec
            OnClickRefresh();


            if (EnableAutoRefresh)
            {
                bool rtn = General.SendMessageToPDA("request refreshzones").Result.ToBool();
            }

          

        }

        private void OnClickRefresh()
        {

            try
            {

                if (this.objSetupBase != null)
                {

                    if (this.radDock1.ActiveWindow != null)
                    {
                        if (this.radDock1.ActiveWindow.Name.Equals("frmBookingDashBoard1") == false && this.radDock1.ActiveWindow.Name.Equals("frmBookingsList1") == false)
                        {


                            DockWindow dock = GetDockByName(this.radDock1.ActiveWindow.Name);
                            if (dock != null)
                            {
                                if (dock.Controls.Count == 1 && dock.Controls[0] != null)
                                {
                                    SetupBase frm = (SetupBase)dock.Controls[0];
                                    frm.RefreshData();
                                }
                            }


                            //else
                            //{

                            //    this.objSetupBase.RefreshData();
                            //}


                        }

                        else if (this.radDock1.ActiveWindow.Name.Equals("frmBookingsList1") == true)
                        {
                            this.objSetupBase.RefreshData();
                        }
                        else if ((this.radDock1.ActiveWindow.Name.Equals("frmBookingDashBoard1") == true))
                        {



                            if(EnableAutoRefresh)
                               RefreshOnlyDashBoard();

                            //  RefreshDashBoard();

                        }
                    }
                    else
                    {


                        RefreshDashBoard();

                    }



                }
                else
                {

                    RefreshDashBoard();

                }
            }
            catch (Exception ex)
            {


            }

        }

        public void RefreshDashBoard()
        {

            try
            {

                RefreshBookingList();

                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshData();

            }
            catch (Exception ex)
            {


            }
        }


        private void RefreshBookingList()
        {
            try
            {

                if (Application.OpenForms.OfType<Form>().Count(c => c.Name == "frmBookingsList") > 0)
                {
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingsList") as frmBookingsList).SetRefreshWhenActive("");
                }

            }
            catch (Exception ex)
            {


            }

        }


        private void RefreshOnlyDashBoard()
        {

            try
            {
                if (EnableAutoRefresh)
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshData();
           //     //General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
            }
            catch (Exception ex)
            {


            }
        }

     

        public void RefreshRequiredDashBoard()
        {

            try
            {
                if(EnableAutoRefresh)
                   (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshAllRequiredData();
                //General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
            }
            catch (Exception ex)
            {


            }
        }

        public void RefreshQuotationOnDemand(string message)
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).UpdateRefreshQuotationGrid(message, "");



        }


        public void RefreshManualLoginDespatchBooking(string message)
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).UpdateManualLoginDespatchedBooking(message, "");



        }


        public void RefreshDespatchBooking(string message)
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).UpdateBookingStatusInGrid(message,"");



        }

        public void RefreshAllocateBooking(string message)
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).UpdateAllocatedBookingInGrid(message, "");



        }

        public void RefreshHoldAndReleaseBooking(string message)
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).UpdateHoldAndReleaseBookingInGrid(message, "");



        }

        public void RefreshActiveDashBoard()
        {

            try
            {
                if (EnableAutoRefresh)
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshAllActiveData();
                //General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
            }
            catch (Exception ex)
            {


            }
        }


        public void RefreshDashBoardBookings()
        {

            try
            {
                if (EnableAutoRefresh)
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshBookingData();
                //General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
            }
            catch (Exception ex)
            {


            }
        }


        private void RefreshActiveBookingDashBoard()
        {

            try
            {
                if (EnableAutoRefresh)
                    (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshActiveData();


                //(Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).ShowActiveData();
                //General.RefreshListWithoutSelected<frmBookingDashBoard>("frmBookingDashBoard1");
            }
            catch (Exception ex)
            {


            }
        }

        private void RefreshSerActiveBookingDashBoard(string msg)
        {

            try
            {
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshSerActiveData(msg);


               
            }
            catch (Exception ex)
            {


            }
        }

        private void RefreshSavePreBookingDashBoard(string msg)
        {

            try
            {
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshSavePreBookingData(msg);



            }
            catch (Exception ex)
            {


            }
        }

        private void RefreshCancelBookingDashBoard(string msg)
        {

            try
            {
                (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).RefreshCancelBookingGrid(msg);



            }
            catch (Exception ex)
            {


            }
        }



        private void FillDataset()
        {
            try
            {

                if (Program.dtCombos == null)
                {

                    Program.dtCombos = new DataSet();
                    using (System.Data.SqlClient.SqlConnection sqlconn = new System.Data.SqlClient.SqlConnection(Cryptography.Decrypt(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToStr(), "softeuroconnskey", true)))
                    {

                        sqlconn.Open();

                        using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                        {

                            cmd.Connection = sqlconn;

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "stp_fillbookingcombos";

                            using (System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd))
                            {
                                // Fill the DataSet using default values for DataTable names, etc
                                da.Fill(Program.dtCombos);
                            }

                        }

                        Program.dtCombos.WriteXml(Application.StartupPath + "\\Booking\\booking.xml", XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch
            {


            }
        }


        private void btnAddBooking_Click(object sender, EventArgs e)
        {

            ShowNewBooking();

        }


        private void ShowNewBooking()
        {
            try
            {

                if (File.Exists(Application.StartupPath + "\\Booking\\TreasureBooking.exe"))
                {
                    try
                    {
                        FillDataset();



                        var s = Newtonsoft.Json.JsonConvert.SerializeObject(Program.dtCombos, Newtonsoft.Json.Formatting.Indented);

                        s = s.Replace(" ", "").Trim();

                        s = s.Replace(Environment.NewLine, "").Trim();
                        s = s.Replace("\"", "*");


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


                        //polObj.CallerId_PhoneNo = phone;
                        //polObj.CallerId_CustomerName = name;

                        //polObj.CallerId_IsAccountCall = IsAccountCall;

                        //polObj.CallerId_CompanyId = this.AccountId;

                        //polObj.CallerId_SubCompanyId = this.CalledToSubcompanyId;

                        polObj.CallerId_SubCompanyId = AppVars.objSubCompany.Id;
                        polObj.CanTransferJob = AppVars.CanTransferJob;
                        polObj.DataString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"].ToStr().Replace(" ", "**");
                        // polObj.PermanentNotes = this.customerNotes;

                        string pol = Newtonsoft.Json.JsonConvert.SerializeObject(polObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "~").Replace(Environment.NewLine, "").Replace("\"", "*");

                        Process pp = new Process();
                        pp.StartInfo.FileName = Application.StartupPath + "\\Booking\\TreasureBooking.exe";
                        pp.StartInfo.Arguments = s + " " + pol + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.LoginObj, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.keyLocations, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*") + " " + Newtonsoft.Json.JsonConvert.SerializeObject(AppVars.zonesList, Newtonsoft.Json.Formatting.Indented).Replace(" ", "").Replace(Environment.NewLine, "").Replace("\"", "*");

                        pp.Start();
                      //  Thread.Sleep(200);
                    }
                    catch (Exception ex)
                    {
                        frmBooking frm = new frmBooking();
                        frm.ControlBox = true;
                        frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        frm.MaximizeBox = false;
                        frm.Show();


                        File.AppendAllText(Application.StartupPath + "\\exception_openingexebooking.txt", "frmmainmenu :" + ex.Message);
                    }

                }
                else
                {

                    if (AppVars.objPolicyConfiguration.BookingFormType.ToInt() == 2)
                    {
                        frmBooking2 frm = new frmBooking2();
                        frm.ControlBox = true;
                        frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        frm.MaximizeBox = false;
                        frm.Show();


                    }
                    else
                    {
                        frmBooking frm = new frmBooking();
                        frm.ControlBox = true;
                        frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                        frm.MaximizeBox = false;
                        frm.Show();
                    }




                }



                //frmBooking frm = new frmBooking();
                //frm.ControlBox = true;
                //frm.FormBorderStyle = FormBorderStyle.FixedSingle;
                //frm.MaximizeBox = false;
                //frm.Show();


            }
            catch (Exception ex)
            {


            }

        }








        private void btnCallerId_ChildrenChanged(object sender, Telerik.WinControls.ChildrenChangedEventArgs e)
        {
            var child = e.Child;
        }

        DateTime? dt = null;

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {




                dt = DateTime.Now;
                // need to uncomment
                this.StatusStrip_label1.Text = dt.Value.ToLongDateString() + " " + string.Format("{0:HH:mm:ss}", dt.Value);
                this.txtCurrentTimer.Text = string.Format("{0:dddd, MMMM dd, HH:mm:ss}", dt.Value);




                if (this.StatisStrip_LicenseLabel.Image == null)
                {
              
                    this.StatisStrip_LicenseLabel.Size = new Size((this.Width - (StatusStrip_label1.Width + StatusStrip_Label2.Width + StatusStrip_Label3.Width) - 60), 20);


                  //  this.StatisStrip_LicenseLabel.Size = new Size((this.Width - (StatusStrip_label1.Width + StatusStrip_Label2.Width + StatusStrip_Label3.Width) - 30), 20);

                //    this.StatisStrip_LicenseLabel.Font = new Font("Tahoma", 7, FontStyle.Bold);
                    this.StatisStrip_LicenseLabel.Image = Resources.Resource1.expire;
                    this.StatisStrip_LicenseLabel.TextImageRelation = TextImageRelation.TextBeforeImage;
                    this.StatisStrip_LicenseLabel.TextAlign = ContentAlignment.MiddleRight;
                    this.StatisStrip_LicenseLabel.ImageAlign = ContentAlignment.MiddleRight;

                }

                if (StatisStrip_LicenseLabel.Width < 0)
                {
                    this.StatisStrip_LicenseLabel.Size = new Size((this.Width - (StatusStrip_label1.Width + StatusStrip_Label2.Width + StatusStrip_Label3.Width) - 60), 20);


                }

                this.StatisStrip_LicenseLabel.Text = AppVars.LicenseExpiryDate;


                if (btn_RefreshForm.Enabled == false)
                    btn_RefreshForm.Enabled = true;





                //if (LogoutTime > 0 && LastControllerActivityTime != null && DateTime.Now.Subtract(LastControllerActivityTime.Value).Minutes >= LogoutTime)
                //{

                //    AppVars.IsLogout = true;
                //    this.FormClosing -= new FormClosingEventHandler(frmMainMenu_FormClosing);
                //    DisposeData();


                //    new TaxiDataContext().stp_ControlerLogins(1, AppVars.LoginObj.LsessionId.ToInt(), "Auto Logout , No Activity was Performed from Last " + LogoutTime.ToStr() + " Mins", Environment.MachineName).FirstOrDefault().Id.ToInt();

                //    Application.Exit();

                //}



                if (Program.onrestartArgs != null && Program.onrestartArgs.Count() > 0 && AppVars.LoginObj.UserName.ToStr().Trim().Length == 0)
                {




                    try
                    {
                        AppVars.LoginObj = this.ObjLoginUser;
                        File.AppendAllText(Application.StartupPath + "\\USERLOGS.TXT", DateTime.Now + " : " + this.ObjLoginUser.UserName.ToStr());
                    }
                    catch
                    {


                    }
                }


                //if (!IsFileCLI)
                //    return;

                //if (Directory.Exists(fileCLIDirPath))
                //{

                //    if (dt.Value.Subtract(Directory.GetLastWriteTime(fileCLIDirPath)).TotalSeconds < 2)
                //    {
                //        new Thread(new ThreadStart(ReadFileCLICalls)).Start();

                //    }
                //}

            }
            catch (Exception ex)
            {
              

            }

        }






        private List<CallerId_AvailableExtension> listOfAvailableExtensions = null;


        private void ReadFileCLICalls()
        {
            try
            {

                string fileName = Directory.GetFiles(fileCLIDirPath).FirstOrDefault();


                string name = string.Empty;
                string number = string.Empty;

                if (File.Exists(fileName))
                {

                    string text = string.Empty;

                    if (fileName.EndsWith("ring.csv") == false)
                    {
                        text = File.ReadAllText(fileName);
                    }


                    File.Delete(fileName);

                    number = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    number = number.Substring(0, number.IndexOf('_'));

                    if (number.Length < 5)
                        return;

                    if (number.StartsWith("9"))
                        number = number.Substring(number.Length > 1 ? 1 : number.Length);



                    if (number == "anonymous")
                    {
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }

                        return;
                    }


                    if (fileName.EndsWith("ring.csv") || fileName.EndsWith("hangup.csv"))
                    {
                    
                        using (TaxiDataContext db = new TaxiDataContext())
                        {
                            name = db.stp_GetCallerInfo(number,"").FirstOrDefault().DefaultIfEmpty().Name.ToStr().Trim();

                        }


                        //  Customer objCustomer = GeneralBLL.GetQueryable<Customer>(c => c.TelephoneNo == number || c.MobileNo == number).OrderByDescending(C => C.Id).FirstOrDefault();
                        //if (name.ToStr().Length > 0)
                        //{
                            name = name.ToStr().Trim();
                      //  }
                    }

                    if (fileName.EndsWith("ring.csv"))
                    {

                        CreateLog(name, number, dt.ToDateTime(), "00:00:00", "","");

                        string item = string.Empty;
                        if (!string.IsNullOrEmpty(name))
                        {

                            item = name + " - " + number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                        }
                        else
                        {
                            item = number + "-" + string.Format("{0:HH:mm}", DateTime.Now);
                        }


                        if (this.InvokeRequired)
                        {


                            this.BeginInvoke(new SingleDelegate(AddFileCliCall), new object[] { item });
                        }
                        else
                        {

                            AddFileCliCall(item);
                        }




                    }
                    else if (fileName.EndsWith("answer.csv"))
                    {



                        string callRefNo = text.Substring(0, text.IndexOf('|'));


                        text = text.Substring(text.IndexOf('|'));
                        string stn = text.Remove(text.IndexOf(number)).Replace("||", "").Replace("|", "").ToStr().Trim();

                        if (ListOfExtensions != null && ListOfExtensions.Count(c => c.CLIExtension == stn) > 0)
                        {

                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke(new Record_delegate(RecordDisplay), new object[] { eCallerIdType.FILE_CLI, callRefNo, "", number, "" });
                            }
                            else
                            {
                                RecordDisplay(eCallerIdType.FILE_CLI, callRefNo, "", number, "");
                            }

                            if (listOfAvailableExtensions != null)
                            {
                                UpdateLog(name, number, DateTime.Now, "", "", stn, "ANSWERED");
                                UpdateExtensionStatus(stn, true);
                            }

                        }
                    }
                    else if (fileName.EndsWith("hangup.csv"))
                    {



                        text = text.Substring(text.IndexOf('|'));
                        string stn = text.Remove(text.IndexOf(number)).Replace("||", "").Replace("|", "").ToStr().Trim();

                        UpdateLog(name, number, DateTime.Now, "", "", stn, "HANGUP");

                        UpdateExtensionStatus(stn, false);
                    }

                    //if (File.Exists(fileName))
                    //{
                    //    File.Delete(fileName);
                    //}



                    //if (File.Exists(fileName))
                    //{

                    //    File.Delete(fileName);
                    //}

                }

                //    break;
                //}
            }
            catch (Exception ex)
            {


            }

        }

        private List<Gen_SubCompany_ContactsNo> GetSubCompanyContactList()
        {

            return General.GetQueryable<Gen_SubCompany_ContactsNo>(c => c.SubCompanyId != null).ToList();

        }


        private void UpdateExtensionStatus(string extension, bool val)
        {
            if (listOfAvailableExtensions != null && listOfAvailableExtensions.Count > 0)
            {



                new TaxiDataContext().stp_UpdateExtensionStatus(extension.ToStr().Trim(), val, Environment.MachineName);

            }

        }

        private void AddFileCliCall(string item)
        {

            (Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard") as frmBookingDashBoard).AddCall(item);

        }

        delegate void SingleDelegate(string message);

        //private void globalEventProvider1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    UpdateControllerActivity();
        //}

        //private void globalEventProvider1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    UpdateControllerActivity();
        //}

        //private void globalEventProvider1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    UpdateControllerActivity();
        //}

        //private void UpdateControllerActivity()
        //{

        //    LastControllerActivityTime = DateTime.Now;
        //    //this.StatusStrip_label1.Text = LastControllerActivityTime.Value.ToLongDateString() + " " + string.Format("{0:HH:mm:ss}", LastControllerActivityTime);


        //}


        private void RefreshPlots()
        {

            try
            {
                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).RefreshDashBoardDrivers();
                // dashBoard.LoadDriverWaitingGrid();
            }
            catch (Exception ex)
            {


            }

        }


        void viewAllCompanyItem_Click(object sender, EventArgs e)
        {

            if ((sender as RadMenuItem).IsChecked)
            {
                AppVars.DefaultSubCompanyId = 0;

            }
            else
            {
                AppVars.DefaultSubCompanyId = AppVars.objSubCompany.Id;

            }

            RefreshDashBoard();


        }


        void viewAllDriverItem_Click(object sender, EventArgs e)
        {

            if ((sender as RadMenuItem).IsChecked)
            {
                AppVars.DefaultDriverSubCompanyId = 0;

            }
            else
            {
                AppVars.DefaultDriverSubCompanyId = AppVars.objSubCompany.Id;

            }

            RefreshDashboardDrivers();


        }


        void viewAllBookingItem_Click(object sender, EventArgs e)
        {

            if ((sender as RadMenuItem).IsChecked)
            {
                AppVars.DefaultBookingSubCompanyId = 0;

            }
            else
            {
                AppVars.DefaultBookingSubCompanyId = AppVars.objSubCompany.Id;

            }

            RefreshDashBoard();

        }




        private void chkEnableAutoDespatch_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                chkEnableAutoDespatch.ForeColor = Color.Green;

                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).SetAutoDespatchMode(true, RefreshAutoDespOtherPC);

            }
            else
            {
                chkEnableAutoDespatch.ForeColor = Color.Black;

                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).SetAutoDespatchMode(false, RefreshAutoDespOtherPC);
            }

            RefreshAutoDespOtherPC = false;



        }


        void chkEnableBidding_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On)
            {
                chkEnableBidding.ForeColor = Color.Green;

                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).SetBiddingMode(true, RefreshAutoDespOtherPC);

            }
            else
            {
                chkEnableBidding.ForeColor = Color.Black;

                ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).SetBiddingMode(false, RefreshAutoDespOtherPC);
            }

            RefreshAutoDespOtherPC = false;
        }



        private void AutoReconnectGSM()
        {
            //if (AppVars.objSMSConfiguration != null && AppVars.comm.IsConnected()==false)
            //{
            //    try
            //    {
            //        AppVars.comm.Close();

            //        AppVars.comm = null;

            //        string portName = AppVars.objSMSConfiguration.ModemSMSPortName.ToStr().Trim();
            //        string PORTFound = SerialPort.GetPortNames().FirstOrDefault(C => C.Equals(portName)).ToStr();

            //        if (!string.IsNullOrEmpty(PORTFound))
            //        {
            //            SerialPort P = new SerialPort(PORTFound);
            //            if (P.IsOpen)
            //            {
            //                P.Close();
            //            }

            //            AppVars.comm = new GsmCommMain(portName, 9600, 300);

            //            AppVars.comm.PhoneConnected += new EventHandler(comm_PhoneConnected);
            //            AppVars.comm.PhoneDisconnected += new EventHandler(comm_PhoneDisconnected);

            //            AppVars.comm.Open();
            //        }
            //    }
            //    catch (Exception ex)
            //    {



            //    }
            //}

        }

        //void comm_MessageReceived(object sender, MessageReceivedEventArgs e)
        //{
        //    if (GSMLastDisconnectOn == null)
        //    {
        //        Thread th = new Thread(new ThreadStart(ReadSMS));
        //        th.IsBackground = true;
        //        th.Start();


        //    }
        //}

 


        private void StartSMSService()
        {  
            //foreach (var item in Process.GetProcesses().Where(c => c.ProcessName.ToLower() == "tcs_sms"))
            //{
            //    item.Kill();

            //    item.Close();

            //}


            Process.Start("TCS_SMS.exe");



        }


      //  frmAuthorizeAutoDespAllocDrvs frmAlloc = null;
        private void ShowAuthAllocDrv(ref string jobIds)
        {
            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).ShowAuthAllocDrv(ref  jobIds);


            //if (frmAlloc != null && frmAlloc.IsDisposed==false)
            //{
            //   // frmAlloc.jobs = jobIds;
            //    frmAlloc.newjobs = jobIds;
            //    frmAlloc.LoadData();
            //}
            //else
            //{


            //    if (jobIds.ToStr().Trim().Length > 0)
            //    {

            //        frmAlloc = new frmAuthorizeAutoDespAllocDrvs(jobIds);
            //        frmAlloc.StartPosition = FormStartPosition.CenterScreen;
            //        frmAlloc.Show();
            //    }
            //}
        }

        private void RefreshJobPool(string msg)
        {

            ((frmBookingDashBoard)System.Windows.Forms.Application.OpenForms.OfType<Form>().FirstOrDefault(c => c.Name == "frmBookingDashBoard")).PopulateJobsPool();


        }


        string extensionNo=null;

        public void ClickACall(string number,string number2,string calleridToShow)
        {
            
            if(extensionNo==null)
            {
                if(ListOfExtensions.Count > 0)
                extensionNo = ListOfExtensions.FirstOrDefault().CLIExtension.ToStr();


            }

            if (extensionNo.ToStr().Trim().Length > 0)
            {

                TCS.Call.MakeCall objcls = new TCS.Call.MakeCall();
                var s=  objcls.YESTECH_MakeCall(AppVars.objPolicyConfiguration.CallRecordingToken.ToStr(), number, number2, extensionNo.ToStr().Trim(), calleridToShow);
            }
            else
            {
                MessageBox.Show("Extension is not defined in settings");

            }
        }

        private void radDock1_Click(object sender, EventArgs e)
        {

        }

        private void frmMainMenu_Load_1(object sender, EventArgs e)
        {

        }

        private void btn_JobPool_Click(object sender, EventArgs e)
        {
           //jobpoolreport_p_c_o
        }
    }
}
