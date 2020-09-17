namespace Sipek.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class IPresenceAndMessaging
    {
        private IConfiguratorInterface _config = new NullConfigurator();

        public event DBuddyStatusChanged BuddyStatusChanged;

        public event DMessageReceived MessageReceived;

        protected IPresenceAndMessaging()
        {
        }

        public int addBuddy(string ident, bool presence)
        {
            return this.addBuddy(ident, presence, this.Config.DefaultAccountIndex);
        }

        public abstract int addBuddy(string ident, bool presence, int accId);
        protected void BaseBuddyStatusChanged(int buddyId, int status, string text)
        {
            if (this.BuddyStatusChanged != null)
            {
                this.BuddyStatusChanged(buddyId, status, text);
            }
        }

        protected void BaseMessageReceived(string from, string text)
        {
            if (this.MessageReceived != null)
            {
                this.MessageReceived(from, text);
            }
        }

        public abstract int delBuddy(int buddyId);
        public abstract int sendMessage(string destAddress, string message);
        public abstract int sendMessage(string destAddress, string message, int accId);
        public abstract int setStatus(int accId, EUserStatus presence_state);

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

