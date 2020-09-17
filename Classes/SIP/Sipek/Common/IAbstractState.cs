namespace Sipek.Common
{
    using System;

    internal abstract class IAbstractState : ICallProxyInterface
    {
        protected IStateMachine _smref;
        private EStateId _stateId = EStateId.IDLE;

        public IAbstractState(IStateMachine sm)
        {
            this._smref = sm;
        }

        public override bool acceptCall()
        {
            return true;
        }

        public override bool alerted()
        {
            return true;
        }

        public override bool conferenceCall()
        {
            return false;
        }

        public override bool dialDtmf(string digits, EDtmfMode mode)
        {
            this.CallProxy.dialDtmf(digits, mode);
            return true;
        }

        public override bool endCall()
        {
            return true;
        }

        public override string getCurrentCodec()
        {
            return "";
        }

        public override bool holdCall()
        {
            return true;
        }

        public virtual void incomingCall(string callingNo, string display)
        {
        }

        public override int makeCall(string dialedNo, int accountId)
        {
            return -1;
        }

        public virtual bool noReplyTimerExpired(int sessionId)
        {
            return false;
        }

        public virtual bool noResponseTimerExpired(int sessionId)
        {
            return false;
        }

        public virtual void onAlerting()
        {
        }

        public virtual void onConnect()
        {
        }

        public abstract void onEntry();
        public abstract void onExit();
        public virtual void onHoldConfirm()
        {
        }

        public virtual void onReleased()
        {
        }

        public virtual bool releasedTimerExpired(int sessionId)
        {
            return false;
        }

        public override bool retrieveCall()
        {
            return true;
        }

        public override bool serviceRequest(int code, string dest)
        {
            this.CallProxy.serviceRequest(code, dest);
            return true;
        }

        public override bool threePtyCall(int partnersession)
        {
            return true;
        }

        public override string ToString()
        {
            return this._stateId.ToString();
        }

        public override bool xferCall(string number)
        {
            return true;
        }

        public override bool xferCallSession(int partnersession)
        {
            return true;
        }

        public ICallProxyInterface CallProxy
        {
            get
            {
                return this._smref.CallProxy;
            }
        }

        public EStateId Id
        {
            get
            {
                return this._stateId;
            }
            set
            {
                this._stateId = value;
            }
        }

        public IMediaProxyInterface MediaProxy
        {
            get
            {
                return this._smref.MediaProxy;
            }
        }

        public override int SessionId
        {
            get
            {
                return this._smref.Session;
            }
            set
            {
            }
        }
    }
}

