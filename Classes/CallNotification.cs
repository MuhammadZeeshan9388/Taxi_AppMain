﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAPI3Lib;
using System.Windows.Forms;
using Taxi_Model;

namespace Taxi_AppMain
{
    public delegate void TAPIEventHandler(string number,string line,bool IsAnswered);
    public delegate void BeforeCallerIdEventHandler(string notification);

      public class callnotification : TAPI3Lib.ITTAPIEventNotification
    {
        public delegate void listshow(string str);
        public listshow addtolist;


        public bool ReceiveNotificationOnConnected;


        public event TAPIEventHandler OnInComingCallNotification;


        private ITCallNotificationEvent CallNotificationObject;
        private void CallNotificationEvent()
        {
            // here we should check to see various notifications of new and ended calls

            switch (CallNotificationObject.Event)
            {

                case CALL_NOTIFICATION_EVENT.CNE_MONITOR:
                    break;
                // the notification is for a monitored call

                case CALL_NOTIFICATION_EVENT.CNE_OWNER:
                    break;
                // the notification is for an owned call
            }

        }

        private ITCallStateEvent CallStateObject;
        private void CallStateEvent()
        {
            // here we should check to see call state and handle connects and disconnects

            try
            {
                switch (CallStateObject.State)
                {
                    case CALL_STATE.CS_IDLE:

                        break;
                    case CALL_STATE.CS_INPROGRESS:
                        //TAPI3Lib.ITCallInfo bi = CallStateObject.Call;
                        //if (bi.CallState == TAPI3Lib.CALL_STATE.CS_INPROGRESS)
                        //{


                        //}
                        break;
                    case CALL_STATE.CS_OFFERING:
                        //     break;
                        // a call is offering so if you dont want it then pass it 


                        if (ReceiveNotificationOnConnected == false)
                        {


                            TAPI3Lib.ITCallInfo b = CallStateObject.Call;

                            if (b.CallState == TAPI3Lib.CALL_STATE.CS_OFFERING)
                            {

                                System.Threading.Thread.Sleep(2000);


                                string callerid = b.get_CallInfoString(CALLINFO_STRING.CIS_CALLERIDNUMBER);
                                string line = b.Address.AddressName;


                                if (OnInComingCallNotification != null)
                                {
                                    OnInComingCallNotification(callerid, line, false);
                                }


                            }
                        }
                        break;


                    case CALL_STATE.CS_CONNECTED:


                        //if (ReceiveNotificationOnConnected)
                        //{
                        //    try
                        //    {

                        //        //TAPI3Lib.ITCallInfo bi3 = CallStateObject.Call;

                        //        new TaxiDataContext().stp_AddLog("Last Call Connected on : " + CallStateObject.Call.Address.AddressName.ToString(),"","");

                        //        //if (bi3.CallState == TAPI3Lib.CALL_STATE.CS_CONNECTED)
                        //        //{
                        //        //    System.Threading.Thread.Sleep(500);


                        //        //    string callerid = bi3.get_CallInfoString(CALLINFO_STRING.CIS_CALLERIDNUMBER);
                        //        //    string line = bi3.Address.AddressName;
                        //        //    if (OnInComingCallNotification != null)
                        //        //    {
                        //        //        OnInComingCallNotification(callerid, line, true);
                        //        //    }

                        //        //}
                        //    }
                        //    catch (Exception ex)
                        //    {


                        //    }
                        //}
                        break;

                        
                    // call is connected 

                    case CALL_STATE.CS_QUEUED:
                        break;
                    // call is beeing queued

                    case CALL_STATE.CS_HOLD:
                        break;
                    // call is on hold 

                    case CALL_STATE.CS_DISCONNECTED:

                        if (ReceiveNotificationOnConnected)
                        {
                            try
                            {

                                TAPI3Lib.ITCallInfo bi4 = CallStateObject.Call;

                                //   System.Threading.Thread.Sleep(500);


                                string callerid = bi4.get_CallInfoString(CALLINFO_STRING.CIS_CALLERIDNUMBER);
                                string line = bi4.Address.AddressName;
                                if (OnInComingCallNotification != null)
                                {
                                    OnInComingCallNotification(callerid, line, true);
                                }

                            }
                            catch (Exception ex)
                            {


                            }
                        }
                        break;
                    // call is disconnected

                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
               //   Application.Exit();

            }

        }


        private ITCallInfoChangeEvent CallInfoObject;
        private void CallInfoEvent()
        {
            // here you can extract information from the call 

            //the code to extract the caller ID
            // >>> put the following code in a try block and swallow the exception if it gives errors
            string CallerID = null;
            CallerID = CallInfoObject.Call.get_CallInfoString(CALLINFO_STRING.CIS_CALLERIDNAME);

        }


        #region ITTAPIEventNotification Members

        public void Event(TAPI_EVENT TapiEvent, object pEvent)
        {
            // making a thread to asynchronosly process the event
            System.Threading.Thread thAsyncCall = default(System.Threading.Thread);

            switch (TapiEvent)
            {
                case TAPI_EVENT.TE_CALLNOTIFICATION:
                    //Call Notification Arrived

                    // assigning our sub's delegate to the thread
                    thAsyncCall = new System.Threading.Thread(CallNotificationEvent);
                    //passing the variable for the thread
                    CallNotificationObject = (ITCallNotificationEvent)pEvent;
                    // starting the thread
                    thAsyncCall.Start();

                    break;
                case TAPI_EVENT.TE_CALLSTATE:
                    //Call State Changes

                    // assigning our sub's delegate to the thread
                    thAsyncCall = new System.Threading.Thread(CallStateEvent);
                    //passing the variable for the thread
                    CallStateObject = (ITCallStateEvent)pEvent;
                    // starting the thread
                    thAsyncCall.Start();

                    break;
                case TAPI_EVENT.TE_CALLINFOCHANGE:
                    //Call Info Changes

                    // assigning our sub's delegate to the thread
                    thAsyncCall = new System.Threading.Thread(CallInfoEvent);
                    //passing the variable for the thread
                    CallInfoObject = (ITCallInfoChangeEvent)pEvent;
                    // starting the thread
                    thAsyncCall.Start();

                    break;
            }

        }

        #endregion
    }

}
