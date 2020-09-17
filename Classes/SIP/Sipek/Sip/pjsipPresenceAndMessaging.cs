namespace Sipek.Sip
{
    using Sipek.Common;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class pjsipPresenceAndMessaging : IPresenceAndMessaging
    {
        private static pjsipPresenceAndMessaging _instance = null;
        private static OnBuddyStatusChangedCallback bscdel = new OnBuddyStatusChangedCallback(pjsipPresenceAndMessaging.onBuddyStatusChanged);
        private static OnMessageReceivedCallback mrdel = new OnMessageReceivedCallback(pjsipPresenceAndMessaging.onMessageReceived);
        internal const string PJSIP_DLL = "pjsipDll.dll";

        private pjsipPresenceAndMessaging()
        {
            onBuddyStatusChangedCallback(bscdel);
            onMessageReceivedCallback(mrdel);
        }

        public override int addBuddy(string name, bool presence, int accId)
        {
            string sipuri = "";
            if (!pjsipStackProxy.Instance.IsInitialized)
            {
                return -1;
            }
            if (name.IndexOf("sip:") == 0)
            {
                sipuri = name;
            }
            else
            {
                sipuri = "sip:" + name + "@" + base.Config.Accounts[accId].HostName;
            }
            return dll_addBuddy(pjsipStackProxy.Instance.SetTransport(accId, sipuri), presence);
        }

        public override int delBuddy(int buddyId)
        {
            return dll_removeBuddy(buddyId);
        }

        [DllImport("pjsipDll.dll")]
        private static extern int dll_addBuddy(string uri, bool subscribe);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_removeBuddy(int buddyId);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_sendMessage(int buddyId, string uri, string message);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_setStatus(int accId, int presence_state);
        private static int onBuddyStatusChanged(int buddyId, int status, string text)
        {
            Instance.BaseBuddyStatusChanged(buddyId, status, text.ToString());
            return 1;
        }

        [DllImport("pjsipDll.dll")]
        private static extern int onBuddyStatusChangedCallback(OnBuddyStatusChangedCallback cb);
        private static int onMessageReceived(string from, string text)
        {
            Instance.BaseMessageReceived(from.ToString(), text.ToString());
            return 1;
        }

        [DllImport("pjsipDll.dll")]
        private static extern int onMessageReceivedCallback(OnMessageReceivedCallback cb);
        public override int sendMessage(string destAddress, string message)
        {
            return this.sendMessage(destAddress, message, base.Config.Accounts[base.Config.DefaultAccountIndex].Index);
        }

        public override int sendMessage(string destAddress, string message, int accId)
        {
            if (!pjsipStackProxy.Instance.IsInitialized)
            {
                return -1;
            }
            string sipuri = "";
            if (destAddress.IndexOf("sip:") == 0)
            {
                sipuri = destAddress;
            }
            else
            {
                sipuri = "sip:" + destAddress + "@" + base.Config.Accounts[accId].HostName;
            }
            sipuri = pjsipStackProxy.Instance.SetTransport(accId, sipuri);
            return dll_sendMessage(base.Config.Accounts[accId].Index, sipuri, message);
        }

        public override int setStatus(int accId, EUserStatus status)
        {
            if (!pjsipStackProxy.Instance.IsInitialized || (accId < 0))
            {
                return -1;
            }
            if ((base.Config.Accounts.Count > 0) && (base.Config.Accounts[accId].RegState != 200))
            {
                return -1;
            }
            if (!base.Config.PublishEnabled)
            {
                return -1;
            }
            return dll_setStatus(base.Config.Accounts[accId].Index, (int) status);
        }

        public static pjsipPresenceAndMessaging Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new pjsipPresenceAndMessaging();
                }
                return _instance;
            }
        }

        private delegate int OnBuddyStatusChangedCallback(int buddyId, int status, string statusText);

        private delegate int OnMessageReceivedCallback(string from, string message);
    }
}

