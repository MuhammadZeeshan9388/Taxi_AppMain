using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TAPI3Lib;
using Utils;
using System.Collections;
using System.Windows.Forms;

namespace Taxi_AppMain
{
    public  class CIDTapiLauncher
    {
        private TAPIClass tobj;
        private ITAddress[] ia = new TAPI3Lib.ITAddress[10];
        private callnotification cn;

        public event TAPIEventHandler OnInComingCallNotification;

        int[] registertoken = new int[10];
        private bool _ReceiveNotificationOnAns;

        public bool ReceiveNotificationOnAns
        {
            get { return _ReceiveNotificationOnAns; }
            set { _ReceiveNotificationOnAns = value; }
        }


        public CIDTapiLauncher(bool recvOnAnswer)
        {
            this.ReceiveNotificationOnAns = recvOnAnswer;

            InitializeTapi();
        }

        void InitializeTapi()
        {
            try
            {
                tobj = new TAPIClass();
                tobj.Initialize();
                IEnumAddress ea = tobj.EnumerateAddresses();

                cn = new callnotification();
                cn.ReceiveNotificationOnConnected = this.ReceiveNotificationOnAns;

             //   cn.addtolist = new callnotification.listshow(this.status);
                tobj.ITTAPIEventNotification_Event_Event += new TAPI3Lib.ITTAPIEventNotification_EventEventHandler(cn.Event);
                tobj.EventFilter = (int)(TAPI_EVENT.TE_CALLNOTIFICATION |
                    TAPI_EVENT.TE_DIGITEVENT |
                    TAPI_EVENT.TE_PHONEEVENT |
                    TAPI_EVENT.TE_CALLSTATE |
                    TAPI_EVENT.TE_GENERATEEVENT |
                    TAPI_EVENT.TE_GATHERDIGITS |
                    TAPI_EVENT.TE_REQUEST);
              

            
           

                cn.OnInComingCallNotification += new TAPIEventHandler(cn_OnInComingCallNotification);    
                
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }

         void cn_OnInComingCallNotification(string number,string line,bool IsAnswered)
        {
            if (OnInComingCallNotification != null)
            {
                OnInComingCallNotification(number, line,IsAnswered);

            }
        }

         public static IEnumerable<string> GetDriversList()
         {
             List<string> rtnList = new List<string>();

             TAPIClass tobj = new TAPIClass();
             tobj.Initialize();
             IEnumAddress ea = tobj.EnumerateAddresses();
             ITAddress ln;
             uint arg3 = 0;
    


             ITCollection addresses = (ITCollection)tobj.Addresses;

             int cnt = addresses.Count;
             ITAddress[] ita = new TAPI3Lib.ITAddress[cnt];


             for (int i = 0; i < cnt; i++)
             {
                 ea.Next(1, out ln, ref arg3);
                 ita[i] = ln;
                 if (ln != null)
                 {
                     rtnList.Add(ita[i].AddressName);

                 }
                 else
                     break;
             }


             return rtnList;



         }


         public static ListViewItem[] GetDriversListArray()
         {
            
             
             TAPIClass tobj = new TAPIClass();
             tobj.Initialize();
             IEnumAddress ea = tobj.EnumerateAddresses();
             ITAddress ln;
             uint arg3 = 0;

             ITCollection addresses = (ITCollection)tobj.Addresses;

             int cnt = addresses.Count;
             ITAddress[] ita = new TAPI3Lib.ITAddress[cnt];

             ListViewItem[] rtnList = new ListViewItem[cnt];

             for (int i = 0; i < cnt; i++)
             {
                 ea.Next(1, out ln, ref arg3);
                 ita[i] = ln;
                 if (ln != null)
                 {
                     rtnList[i] = new ListViewItem();
                     rtnList[i].Text=ita[i].AddressName;

                 }
                 else
                     break;
             }


             return rtnList;



         }

        public bool SetConfiguration(TAPIAccountConfig objAccount,ref string registrationMsg)
        {
            bool rtn = true;
            try
            {
                IEnumerable<string> lines = objAccount.Line.SplitTo<string>(new char[]{','});

                if (lines.Count() == 0)
                {
                    rtn = false;

                }
                else
                {

                    IEnumAddress ea = tobj.EnumerateAddresses();
                    //txtLineName.Text = ">> " + line;

                    uint arg3 = 0;
                    ITAddress ln = null;
                    ITCollection addresses = (ITCollection)tobj.Addresses;
                    string success = "Success : [ ";
                    string failed = "Failed : [ ";

                    int cnt = addresses.Count;

                    for (int i = 0; i < cnt; i++)
                        {
                             try
                             {
                                ea.Next(1, out ln, ref arg3);

                                if (ln != null && lines.Contains(ln.AddressName))
                                {
                                    tobj.RegisterCallNotifications(ln, true, true, TapiConstants.LINEADDRESSTYPE_DOMAINNAME, 2);
                                    success += ln.AddressName + ",";
                                }
                             }
                             catch (Exception ex)
                             {
                                 failed += ln.AddressName + ",";
                                 continue;

                             }
                        }
                    
                   

                  

                    char[] trimChar=new char[]{','};
                    registrationMsg = success.TrimEnd(trimChar).Insert(success.Length - 1, " ]") + "|" +
                                       failed.TrimEnd(trimChar).Insert(failed.Length - 1, " ]");
                    
                }
              
            }
            catch (Exception ein)
            {
                rtn = false;
            }

            return rtn;
        }



     
     
    }
}
