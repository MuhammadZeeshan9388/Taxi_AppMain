using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sipek.Common;

namespace CallerIdData
{
    public class AccountConfig : IAccount
    {


        #region IAccount Members

        private string _AccountName;

        public string AccountName
        {
            get
            {
                return this._AccountName;
            }
            set
            {
                this._AccountName = value;
            }
        }

        private string _DisplaytName;
        public string DisplayName
        {
            get
            {
                return this._DisplaytName;
            }
            set
            {
                this._DisplaytName = value;
            }
        }

        public string DomainName
        {
            get
            {
                return "*";
            }
            set { }
        }


        private string _HostName;
        public string HostName
        {
            get
            {
                return this._HostName;

            }
            set
            {
                this._HostName = value;
            }
        }


        private string _Id;

        public string Id
        {
            get
            {
                return this._Id;

            }
            set
            {
                this._Id = value;
            }
        }



        public int Index
        {
            get
            {
                return 0;
            }
            set { }
        }


        private string _Password;
        public string Password
        {
            get
            {
                return this._Password;

            }
            set
            {
                this._Password = value;
            }
        }


        private string _ProxyAddress;

        public string ProxyAddress
        {
            get
            {
                return this._ProxyAddress;

            }
            set
            {
                this._ProxyAddress = value;

            }
        }

        public int RegState
        {
            get
            {
                return 0;
            }
            set { }
        }

        public ETransportMode TransportMode
        {
            get
            {
                return ETransportMode.TM_UDP;
            }
            set { }
        }


        private string _UserName;

        public string UserName
        {
            get
            {
                return this._UserName;


            }
            set
            {
                this._UserName = value;
            }
        }


        private string _BookingUrl;

        public string BookingUrl
        {
            get { return _BookingUrl; }
            set { _BookingUrl = value; }
        }

        #endregion

        #region IAccount Members


        public bool Enabled
        {
            get
            {
                return true;
            }
            set
            {
                //   this
            }
        }

        #endregion
    }

}
