namespace Sipek.Sip
{
    using Sipek.Common;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class pjsipRegistrar : IRegistrar
    {
        private static pjsipRegistrar _instance = null;
        internal const string PJSIP_DLL = "pjsipDll.dll";
        private static OnRegStateChanged rsDel = new OnRegStateChanged(pjsipRegistrar.onRegStateChanged);

        private pjsipRegistrar()
        {
            onRegStateCallback(rsDel);
        }

        [DllImport("pjsipDll.dll")]
        private static extern int dll_registerAccount(string uri, string reguri, string domain, string username, string password, string proxy, bool isdefault);
        [DllImport("pjsipDll.dll")]
        private static extern int dll_removeAccounts();
        [DllImport("pjsipDll.dll")]
        private static extern int onRegStateCallback(OnRegStateChanged cb);
        private static int onRegStateChanged(int accId, int regState)
        {
            for (int i = 0; i < Instance.Config.Accounts.Count; i++)
            {
                IAccount account = Instance.Config.Accounts[i];
                if (account.Index == accId)
                {
                    Instance.Config.Accounts[i].RegState = regState;
                    Instance.BaseAccountStateChanged(i, regState);
                    return 1;
                }
            }
            return -1;
        }

        public override int registerAccounts()
        {
            if (!pjsipStackProxy.Instance.IsInitialized)
            {
                return -1;
            }
            if (base.Config.Accounts.Count <= 0)
            {
                return 0;
            }
            dll_removeAccounts();
            for (int i = 0; i < base.Config.Accounts.Count; i++)
            {
                IAccount account = base.Config.Accounts[i];
                if (account == null)
                {
                    return -1;
                }
                base.Config.Accounts[i].Index = -1;
                base.BaseAccountStateChanged(i, 0);
                if ((account.Id.Length > 0) && (account.HostName.Length > 0))
                {
                    string displayName = account.DisplayName;
                    string uri = "sip:" + account.UserName;
                    if (account.UserName.IndexOf("@") < 0)
                    {
                        uri = uri + "@" + account.HostName;
                    }
                    string sipuri = "sip:" + account.HostName;
                    sipuri = pjsipStackProxy.Instance.SetTransport(i, sipuri);
                    string domainName = account.DomainName;
                    string userName = account.UserName;
                    string password = account.Password;
                    string proxy = "";


                    account.ProxyAddress = "proxy-em-genband.com:5060";

                    if (account.ProxyAddress.Length > 0)
                    {
                        proxy = "sip:" + account.ProxyAddress;
                    }
                    int num2 = dll_registerAccount(uri, sipuri, domainName, userName, password, proxy, i == base.Config.DefaultAccountIndex);
                    base.Config.Accounts[i].Index = num2;

               
                }
            }
            return 1;
        }

        public override int unregisterAccounts()
        {
            return dll_removeAccounts();
        }

        public static pjsipRegistrar Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new pjsipRegistrar();
             
                }
                return _instance;
            }
        }

        private delegate int OnRegStateChanged(int accountId, int regState);
    }
}

