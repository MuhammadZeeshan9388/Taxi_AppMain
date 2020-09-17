using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sipek.Common;

namespace CallerIdData
{
    public class PhoneConfig : IConfiguratorInterface
    {

        List<IAccount> _acclist = new List<IAccount>();

        internal PhoneConfig()
        {
            _acclist.Add(new AccountConfig());
        }

        internal PhoneConfig(AccountConfig account)
        {
            _acclist.Clear();
            _acclist.Add(account);

        
            //AccountConfig c = new AccountConfig();
            //c.AccountName = "105";
            //c.DisplayName = "105";
            //c.UserName = "105";
            //c.Id = "105";
            //c.Password = "t105";
            //c.HostName = "192.168.0.16";
            //c.TransportMode = ETransportMode.TM_UDP;
            //c.ProxyAddress = "";

            //_acclist.Add(c);
        }

        #region IConfiguratorInterface Members

        public bool AAFlag
        {
            get
            {
                return false;
            }
            set { }
        }

        public List<IAccount> Accounts
        {
            get { return _acclist; }
        }

        public bool CFBFlag
        {
            get
            {
                return false;
            }
            set { }
        }

        public string CFBNumber
        {
            get
            {
                return "";
            }
            set { }
        }

        public bool CFNRFlag
        {
            get
            {
                return false;
            }
            set { }
        }

        public string CFNRNumber
        {
            get
            {
                return "";
            }
            set { }
        }

        public bool CFUFlag
        {
            get
            {
                return false;
            }
            set { }
        }

        public string CFUNumber
        {
            get
            {
                return "";
            }
            set { }
        }

        public List<string> CodecList
        {
            get
            {
                List<String> cl = new List<string>();
                cl.Add("PCMA");
                return cl;
            }
            set { }
        }

        public bool DNDFlag
        {
            get
            {
                return false;
            }
            set { }
        }

        public int DefaultAccountIndex
        {
            get { return 0; }
        }

        public bool IsNull
        {
            get { return false; }
        }

        public bool PublishEnabled
        {
            get
            {
                return false;
            }
            set { }
        }

        public int SIPPort
        {
            get
            {
                return 5060;
            }
            set { }
        }

        public void Save()
        {
            this.Save();
            //TODO;
        }

        #endregion
    }

}
