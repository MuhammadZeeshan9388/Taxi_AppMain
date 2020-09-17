namespace Sipek.Sip
{
    using Sipek.Common;
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class pjsipStackProxy : IVoipProxy
    {
        private bool _initialized;
        private static pjsipStackProxy _instance = null;
        public SipConfigStruct ConfigMore = SipConfigStruct.Instance;
        private static OnCallReplacedCallback crepdel = new OnCallReplacedCallback(pjsipStackProxy.onCallReplacedCallback);
        private static OnDtmfDigitCallback dtdel = new OnDtmfDigitCallback(pjsipStackProxy.onDtmfDigitCallback);
        private static OnMessageWaitingCallback mwidel = new OnMessageWaitingCallback(pjsipStackProxy.onMessageWaitingCallback);
        internal const string PJSIP_DLL = "pjsipDll.dll";

        protected pjsipStackProxy()
        {
        }

        public override ICallProxyInterface createCallProxy()
        {
            return new pjsipCallProxy(base.Config);
        }

        [DllImport("pjsipDll.dll")]
        private static extern int dll_getCodec(int index, StringBuilder codec);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_getNumOfCodecs();
        [DllImport("pjsipDll.dll")]
        private static extern int dll_init();
        [DllImport("pjsipDll.dll")]
        private static extern int dll_main();
        [DllImport("pjsipDll.dll")]
        private static extern int dll_setCodecPriority(string name, int prio);
        [DllImport("pjsipDll.dll")]
        private static extern void dll_setSipConfig(SipConfigStruct config);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_setSoundDevice(string playbackDeviceId, string recordingDeviceId);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_shutdown();
        public override string getCodec(int index)
        {
            if (!this.IsInitialized)
            {
                return "";
            }
            StringBuilder codec = new StringBuilder(0x100);
            dll_getCodec(index, codec);
            return codec.ToString();
        }

        public override int getNoOfCodecs()
        {
            if (!this.IsInitialized)
            {
                return 0;
            }
            return dll_getNumOfCodecs();
        }

        public override int initialize()
        {
            this.shutdown();
            onDtmfDigitCallback(dtdel);
            onMessageWaitingCallback(mwidel);
            onCallReplacedCallback(crepdel);
            pjsipCallProxy.initialize();
            int num = this.start();
            this.IsInitialized = num == 0;
            return num;
        }

        [DllImport("pjsipDll.dll", EntryPoint="onCallReplaced")]
        private static extern int onCallReplacedCallback(OnCallReplacedCallback cb);
        private static int onCallReplacedCallback(int oldid, int newid)
        {
            Instance.BaseCallReplacedCallback(oldid, newid);
            return 1;
        }

        [DllImport("pjsipDll.dll")]
        private static extern int onDtmfDigitCallback(OnDtmfDigitCallback cb);
        private static int onDtmfDigitCallback(int callId, int digit)
        {
            Instance.BaseDtmfDigitReceived(callId, digit);
            return 1;
        }

        [DllImport("pjsipDll.dll")]
        private static extern int onMessageWaitingCallback(OnMessageWaitingCallback cb);
        private static int onMessageWaitingCallback(int mwi, string info)
        {
            if (info == null)
            {
                return -1;
            }
            Instance.BaseMessageWaitingIndication(mwi, info.ToString());
            return 1;
        }

        public override void setCodecPriority(string codecname, int priority)
        {
            if (this.IsInitialized)
            {
                dll_setCodecPriority(codecname, priority);
            }
        }

        public void setSoundDevice(string playbackDeviceId, string recordingDeviceId)
        {
            dll_setSoundDevice(playbackDeviceId, recordingDeviceId);
        }

        internal string SetTransport(int accountId, string sipuri)
        {
            string str = sipuri;
            try
            {
                switch (base.Config.Accounts[accountId].TransportMode)
                {
                    case ETransportMode.TM_TCP:
                        return (sipuri + ";transport=tcp");

                    case ETransportMode.TM_TLS:
                        return (sipuri + ";transport=tls");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            return str;
        }

        public override int shutdown()
        {
            return dll_shutdown();
        }

        private int start()
        {
            int num = -1;
            if (!base.Config.IsNull)
            {
                this.ConfigMore.listenPort = base.Config.SIPPort;
            }
            dll_setSipConfig(this.ConfigMore);
            num = dll_init();
            if (num != 0)
            {
                return num;
            }
            return (num | dll_main());
        }

        public static pjsipStackProxy Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new pjsipStackProxy();
                }
                return _instance;
            }
        }

        public override bool IsInitialized
        {
            get
            {
                return this._initialized;
            }
            set
            {
                this._initialized = value;
            }
        }
    }
}

