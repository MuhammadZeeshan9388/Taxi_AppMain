namespace Sipek.Common
{
    using System;
    using System.Collections.Generic;

    internal class NullConfigurator : IConfiguratorInterface
    {
        private List<IAccount> _accountList = new List<IAccount>();

        public void Save()
        {
        }

        public bool AAFlag
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public List<IAccount> Accounts
        {
            get
            {
                return this._accountList;
            }
        }

        public bool CFBFlag
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public string CFBNumber
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public bool CFNRFlag
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public string CFNRNumber
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public bool CFUFlag
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public string CFUNumber
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public List<string> CodecList
        {
            get
            {
                return null;
            }
            set
            {
            }
        }

        public int DefaultAccountIndex
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public bool DNDFlag
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public bool IsNull
        {
            get
            {
                return true;
            }
        }

        public bool PublishEnabled
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public int SIPPort
        {
            get
            {
                return 0x13c4;
            }
            set
            {
            }
        }

        public class NullAccount : IAccount
        {
            public string AccountName
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }

            public string DisplayName
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }

            public string DomainName
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }

            public string HostName
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }

            public string Id
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }

            public int Index
            {
                get
                {
                    return 0;
                }
                set
                {
                }
            }

            public string Password
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }

            public int Port
            {
                get
                {
                    return 0;
                }
                set
                {
                }
            }

            public string ProxyAddress
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }

            public int RegState
            {
                get
                {
                    return 0;
                }
                set
                {
                }
            }

            public ETransportMode TransportMode
            {
                get
                {
                    return ETransportMode.TM_UDP;
                }
                set
                {
                }
            }

            public string UserName
            {
                get
                {
                    return "";
                }
                set
                {
                }
            }
        }
    }
}

