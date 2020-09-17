using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sipek.Common.CallControl;
using Sipek.Common;
using Sipek.Sip;

namespace CallerIdData
{
    public class CIDSipLauncher
    {
        // Get call manager instance
       public  CCallManager CallManager
        {
            get { return CCallManager.Instance; }
        }


      

        private PhoneConfig _config = null;
        internal PhoneConfig Config
        {
            get { return _config; }
        }



        public void Initialize()
        {
        }

        private int _SessionId;

        public int SessionId
        {
            get { return _SessionId; }
            set { _SessionId = value; }
        }

       


        public event DIncomingCallNotification OnInComingCallNotification;
        public event Sipek.Common.DAccountStateChanged OnAccountStateChanged;
        public event DCallStateRefresh OnCallStateRefresh;

        public void SetConfiguration(AccountConfig objAccount)
        {
            _config = new PhoneConfig(objAccount);           

            CallManager.CallStateRefresh += new DCallStateRefresh(CallManager_CallStateRefresh);
            pjsipRegistrar.Instance.AccountStateChanged += new Sipek.Common.DAccountStateChanged(Instance_AccountStateChanged);

             CallManager.StackProxy = pjsipStackProxy.Instance;
         
      
            CallManager.Config = Config;
            pjsipStackProxy.Instance.Config = Config;
            pjsipRegistrar.Instance.Config = Config;

            CallManager.Initialize();
            CallManager.IncomingCallNotification += new DIncomingCallNotification(CallManager_IncomingCallNotification);


         //   CallManager.CallAnswerNotification += new CCallManager.OnAnswerCall(CallManager_CallAnswerNotification);

            pjsipRegistrar.Instance.registerAccounts();
            CCallManager.Instance.MediaProxy = new CMediaPlayerProxy();     
  
      

           

        }

   

        public void SetConfiguration(List<AccountConfig> listofAccounts)
        {
            _config = new PhoneConfig();
            _config.Accounts.Clear();
            foreach (var acc in listofAccounts)
            {
                _config.Accounts.Add(acc);
            }
         //   _config.PublishEnabled = true;


            CallManager.CallStateRefresh += new DCallStateRefresh(CallManager_CallStateRefresh);
            pjsipRegistrar.Instance.AccountStateChanged += new Sipek.Common.DAccountStateChanged(Instance_AccountStateChanged);

            CallManager.StackProxy = pjsipStackProxy.Instance;


            CallManager.Config = Config;
            pjsipStackProxy.Instance.Config = Config;
            pjsipRegistrar.Instance.Config = Config;

            CallManager.Initialize();
            CallManager.IncomingCallNotification += new DIncomingCallNotification(CallManager_IncomingCallNotification);

            pjsipRegistrar.Instance.registerAccounts();
            CCallManager.Instance.MediaProxy = new CMediaPlayerProxy();
        
          



        }



        public virtual  void CallManager_IncomingCallNotification(int sessionId, string number, string info)
        {
            if (OnInComingCallNotification != null)
            {
                this.SessionId = sessionId;
                OnInComingCallNotification(sessionId, number, info);
            }

            // MUST synchronize threads

            //if (this.InvokeRequired)
            //{
               

            //    Record_delegate d = new Record_delegate(onCallRinging);
            //    this.BeginInvoke(d, new object[] { sessionId, number, info });
            //}
            //else
            //{

            //    onCallRinging(sessionId, number, info);
            //}        
        }



        #region Callbacks
       public virtual  void Instance_AccountStateChanged(int accountId, int accState)
        {
            if (OnAccountStateChanged != null)
            {

                OnAccountStateChanged(accountId, accState);
            }
            // MUST synchronize threads
            //if (InvokeRequired)
            //    this.BeginInvoke(new DAccountStateChanged(OnRegistrationUpdate), new object[] { accountId, accState });
            //else
            //    OnRegistrationUpdate(accountId, accState);
        }



       public virtual void CallManager_CallStateRefresh(int sessionId)
        {
            if (OnCallStateRefresh != null)
            {
                OnCallStateRefresh(sessionId);

            }
            // MUST synchronize threads
            //if (InvokeRequired)
            //    this.BeginInvoke(new DCallStateRefresh(OnStateUpdate), new object[] { sessionId });
            //else
            //    OnStateUpdate(sessionId);
        }
        #endregion

        #region Synhronized callbacks
       public virtual void OnRegistrationUpdate(int accountId, int accState)
        {
            //string status = "Connecting...";
            //int accountState = accState.ToInt();
            //if (accountState == 200)
            //    status = "Success";
            //else if (accountState == 408)
            //    status = "Failed";

            //textBoxRegState.Text = status;
        }

       public virtual void OnStateUpdate(int sessionId)
        {
            //this.CurrentSessionId = sessionId;
            //textBoxCallState.Text = CallManager.getCall(sessionId).StateId.ToStr().Replace("NULL", "");

            //GridViewRowInfo row = grdCalls.Rows.FirstOrDefault(c => c.Cells[COLS.ID].Value.ToInt() == callscnter - 1);
            //if (row != null)
            //{
            //    string callStatus = textBoxCallState.Text.ToStr().ToLower();

            //    if (callStatus == "alerting")
            //    {

            //        //onCallRinging(sessionId, txtDialNumber.Text, "");
            //    }
            //    else if (callStatus == "incoming")
            //    {


            //    }

            //    else if (callStatus == "active")
            //    {
            //        timer1.Start();
            //        ApplyOnCallReceivedSettings();
            //        ApplyAnswerCallSettings();
            //    }

            //    else if (callStatus == "holding")
            //    {
            //        pic_ringingCall.Visible = false;
            //        pic_AnsweredCall.Visible = true;
            //        btnRecvCall.Enabled = false;

            //        txtCallLabel.Text = "";

            //    }
            //    else
            //    {
            //        ApplyCallTerminateSettings();
            //    }
            //    if (callStatus != "incoming")
            //        ChangeRowStatus(callscnter - 1);
            }

        }

        #endregion





    }
