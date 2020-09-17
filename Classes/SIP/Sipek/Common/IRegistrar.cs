namespace Sipek.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class IRegistrar
    {
        private IConfiguratorInterface _config = new NullConfigurator();

        public event DAccountStateChanged AccountStateChanged;

        protected IRegistrar()
        {
        }

        protected void BaseAccountStateChanged(int accountId, int accState)
        {
            if (this.AccountStateChanged != null)
            {
                this.AccountStateChanged(accountId, accState);
            }
        }

        public abstract int registerAccounts();
        public abstract int unregisterAccounts();

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
    }
}

