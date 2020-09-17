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
    public  class CIDCTITapiLauncher
    {
        private TAPIClass tobj;
        private ITAddress[] ia = new TAPI3Lib.ITAddress[10];
        private cticallnotification cn;

        public event TAPICTIEventHandler OnInComingCallNotification;

        int[] registertoken = new int[10];
        private bool _ReceiveNotificationOnAns;

        public bool ReceiveNotificationOnAns
        {
            get { return _ReceiveNotificationOnAns; }
            set { _ReceiveNotificationOnAns = value; }
        }


        public CIDCTITapiLauncher(bool recvOnAnswer)
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

                cn = new cticallnotification();
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
              

            
           

                cn.OnInComingCallNotification += new TAPICTIEventHandler(cn_OnInComingCallNotification);    
                
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
            }
        }

         void cn_OnInComingCallNotification(string number,string line,bool IsAnswered,string callType,string calledNumber)
        {
            if (OnInComingCallNotification != null)
            {
                OnInComingCallNotification(number, line, IsAnswered, callType, calledNumber);

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
                     if (ln.DialableAddress.ToStr().Trim() != string.Empty)
                     {
                         rtnList.Add(ita[i].DialableAddress.ToStr().Trim());
                     }
                 }
                 else
                     break;
             }


             return rtnList.Distinct();



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

           //  ListViewItem[] rtnList = new ListViewItem[cnt];


             List<ListViewItem> itemList = new List<ListViewItem>();

             for (int i = 0; i < cnt; i++)
             {
                 ea.Next(1, out ln, ref arg3);
                 ita[i] = ln;
                 if (ln != null)
                 {
                     if (ln.DialableAddress.ToStr().Trim() != string.Empty)
                     {

                         if (itemList.Count(c => c.Text == ita[i].DialableAddress) == 0)
                         {

                             itemList.Add(new ListViewItem(ita[i].DialableAddress));
                             //rtnList[i] = new ListViewItem();
                             //rtnList[i].Text = ita[i].DialableAddress;
                         }
                     }
                 }
                 else
                     break;
             }


             return itemList.ToArray<ListViewItem>();



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

                                if (ln != null && ln.DialableAddress!=null && lines.Contains(ln.DialableAddress.ToStr().Trim()))
                                {
                                    tobj.RegisterCallNotifications(ln, true, true, TapiConstants.LINEADDRESSTYPE_DOMAINNAME, 2);
                                    success += ln.DialableAddress.ToStr().Trim() + ",";
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
