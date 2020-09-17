namespace Sipek.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class IVoipProxy
    {
        private IConfiguratorInterface _config = new NullConfigurator();

        public event DCallReplaced CallReplaced;

        public event DDtmfDigitReceived DtmfDigitReceived;

        public event DMessageWaitingNotification MessageWaitingIndication;

        protected IVoipProxy()
        {
        }

        protected void BaseCallReplacedCallback(int oldid, int newid)
        {
            if (this.CallReplaced != null)
            {
                this.CallReplaced(oldid, newid);
            }
        }

        protected void BaseDtmfDigitReceived(int callId, int digit)
        {
            if (this.DtmfDigitReceived != null)
            {
                this.DtmfDigitReceived(callId, digit);
            }
        }

        protected void BaseMessageWaitingIndication(int mwi, string text)
        {
            if (this.MessageWaitingIndication != null)
            {
                this.MessageWaitingIndication(mwi, text);
            }
        }

        public abstract ICallProxyInterface createCallProxy();
        public abstract string getCodec(int i);
        public abstract int getNoOfCodecs();
        public abstract int initialize();
        public abstract void setCodecPriority(string item, int p);
        public virtual int shutdown()
        {
            this.DtmfDigitReceived = null;
            this.MessageWaitingIndication = null;
            return 1;
        }

        public IConfiguratorInterface Config
        {
            get
            {
                return this._config;
            }
            set
            {
                this._config = value;
            }
        }

        public abstract bool IsInitialized { get; set; }
    }
}

