namespace Sipek.Sip
{
    using Sipek.Common;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class pjsipCallProxy : ICallProxyInterface
    {
        private IConfiguratorInterface _config = new NullConfigurator();
        private int _sessionId;
        private static OnCallHoldConfirm chDel = new OnCallHoldConfirm(pjsipCallProxy.onCallHoldConfirm);
        private static OnCallIncoming ciDel = new OnCallIncoming(pjsipCallProxy.onCallIncoming);
        private static OnCallStateChanged csDel = new OnCallStateChanged(pjsipCallProxy.onCallStateChanged);
        internal const string PJSIP_DLL = "pjsipDll.dll";

        internal pjsipCallProxy(IConfiguratorInterface config)
        {
            this._config = config;
        }

        public override bool acceptCall()
        {
            dll_answerCall(this.SessionId, 200);
            return true;
        }

        public override bool alerted()
        {
            dll_answerCall(this.SessionId, 180);
            return true;
        }

        public override bool conferenceCall()
        {
            if (dll_makeConference(this.SessionId) != 1)
            {
                return false;
            }
            return true;
        }

        public override bool dialDtmf(string digits, EDtmfMode mode)
        {
            dll_dialDtmf(this.SessionId, digits, (int) mode);
            return true;
        }

        [DllImport("pjsipDll.dll")]
        private static extern int dll_answerCall(int callId, int code);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_dialDtmf(int callId, string digits, int mode);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_getCurrentCodec(int callId, StringBuilder codec);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_holdCall(int callId);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_makeCall(int accountId, string uri);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_makeConference(int callId);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_releaseCall(int callId);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_retrieveCall(int callId);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_serviceReq(int callId, int serviceCode, string destUri);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_xferCall(int callId, string uri);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_xferCallWithReplaces(int callId, int dstSession);
        public override bool endCall()
        {
            dll_releaseCall(this.SessionId);
            return true;
        }

        public override string getCurrentCodec()
        {
            StringBuilder codec = new StringBuilder(0x100);
            dll_getCurrentCodec(this.SessionId, codec);
            return codec.ToString();
        }

        public override bool holdCall()
        {
            dll_holdCall(this.SessionId);
            return true;
        }

        public static void initialize()
        {
            onCallIncoming(ciDel);
            onCallStateCallback(csDel);
            onCallHoldConfirmCallback(chDel);
        }

        public override int makeCall(string dialedNo, int accountId)
        {
            string sipuri = "";
            if (dialedNo.IndexOf("sip:") == 0)
            {
                sipuri = dialedNo;
            }
            else
            {
                sipuri = "sip:" + dialedNo + "@" + this.Config.Accounts[accountId].HostName;
            }
            sipuri = pjsipStackProxy.Instance.SetTransport(accountId, sipuri);
            this.SessionId = dll_makeCall(this.Config.Accounts[accountId].Index, sipuri);
            return this.SessionId;
        }

        private static int onCallHoldConfirm(int callId)
        {
            ICallProxyInterface.BaseCallNotification(callId, ECallNotification.CN_HOLDCONFIRM, "");
            return 1;
        }

        [DllImport("pjsipDll.dll")]
        private static extern int onCallHoldConfirmCallback(OnCallHoldConfirm cb);
        [DllImport("pjsipDll.dll")]
        private static extern int onCallIncoming(OnCallIncoming cb);
        private static int onCallIncoming(int callId, string sturi)
        {
            string str = sturi;
            string info = "";
            string number = "";
            if (str != null)
            {
                int index = str.IndexOf("<sip:");
                int num2 = str.IndexOf('@');
                if ((index >= 0) && (num2 > index))
                {
                    number = str.Substring(index + 5, (num2 - index) - 5);
                }
                if (index >= 0)
                {
                    info = str.Remove(index, str.Length - index).Trim();
                }
                else
                {
                    int startIndex = info.IndexOf(';');
                    if (startIndex >= 0)
                    {
                        info = info.Remove(startIndex, info.Length - startIndex);
                    }
                    else
                    {
                        int num4 = info.IndexOf(':');
                        if (num4 >= 0)
                        {
                            info = info.Remove(num4, info.Length - num4);
                        }
                    }
                }
            }
            ICallProxyInterface.BaseIncomingCall(callId, number, info);
            return 1;
        }

        [DllImport("pjsipDll.dll")]
        private static extern int onCallStateCallback(OnCallStateChanged cb);
        private static int onCallStateChanged(int callId, ESessionState callState)
        {
            ICallProxyInterface.BaseCallStateChanged(callId, callState, "");
            return 0;
        }

        public override bool retrieveCall()
        {
            dll_retrieveCall(this.SessionId);
            return true;
        }

        public override bool serviceRequest(int code, string dest)
        {
            string destUri = "<sip:" + dest + "@" + this.Config.Accounts[this.Config.DefaultAccountIndex].HostName + ">";
            dll_serviceReq(this.SessionId, code, destUri);
            return true;
        }

        public override bool threePtyCall(int session)
        {
            dll_serviceReq(this.SessionId, 4, "");
            return true;
        }

        public override bool xferCall(string number)
        {
            string uri = "sip:" + number + "@" + this.Config.Accounts[this.Config.DefaultAccountIndex].HostName;
            dll_xferCall(this.SessionId, uri);
            return true;
        }

        public override bool xferCallSession(int session)
        {
            dll_xferCallWithReplaces(this.SessionId, session);
            return true;
        }

        private IConfiguratorInterface Config
        {
            get
            {
                return this._config;
            }
        }

        public override int SessionId
        {
            get
            {
                return this._sessionId;
            }
            set
            {
                this._sessionId = value;
            }
        }
    }
}

