namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    public class CStateMachine : IStateMachine
    {
        private string _callingName = "";
        private string _callingNumber = "";
        private ECallType _callType;
        private bool _counting;
        private bool _disableStateNotifications;
        private TimeSpan _duration;
        private bool _holdRequested;
        private bool _incoming;
        private bool _is3Pty;
        private bool _isHeld;
        private CCallManager _manager = CCallManager.Instance;
        protected ITimer _noreplyTimer;
        protected ITimer _noresponseTimer;
        protected ITimer _releasedTimer;
        private bool _retrieveRequested;
        private int _session = -1;
        private ICallProxyInterface _sigProxy;
        private IAbstractState _state;
        private CActiveState _stateActive;
        private CAlertingState _stateAlerting;
        private CConnectingState _stateCalling;
        private CHoldingState _stateHolding;
        private CIdleState _stateIdle;
        private CIncomingState _stateIncoming;
        private CReleasedState _stateReleased;
        private CTerminatedState _stateTerminated;
        private DateTime _timestamp;

        public CStateMachine()
        {
            this._sigProxy = this._manager.StackProxy.createCallProxy();
            this._stateIdle = new CIdleState(this);
            this._stateAlerting = new CAlertingState(this);
            this._stateActive = new CActiveState(this);
            this._stateCalling = new CConnectingState(this);
            this._stateReleased = new CReleasedState(this);
            this._stateIncoming = new CIncomingState(this);
            this._stateHolding = new CHoldingState(this);
            this._stateTerminated = new CTerminatedState(this);
            this._state = this._stateIdle;
            this.Time = DateTime.Now;
            this.Duration = TimeSpan.Zero;
            if (this._manager != null)
            {
                this._noreplyTimer = this._manager.Factory.createTimer();
                this._noreplyTimer.Interval = 0x3a98;
                this._noreplyTimer.Elapsed = new TimerExpiredCallback(this._noreplyTimer_Elapsed);
                this._releasedTimer = this._manager.Factory.createTimer();
                this._releasedTimer.Interval = 0x1388;
                this._releasedTimer.Elapsed = new TimerExpiredCallback(this._releasedTimer_Elapsed);
                this._noresponseTimer = this._manager.Factory.createTimer();
                this._noresponseTimer.Interval = 0xea60;
                this._noresponseTimer.Elapsed = new TimerExpiredCallback(this._noresponseTimer_Elapsed);
            }
        }

        private void _noreplyTimer_Elapsed(object sender, EventArgs e)
        {
            this.State.noReplyTimerExpired(this.Session);
        }

        private void _noresponseTimer_Elapsed(object sender, EventArgs e)
        {
            this.State.noResponseTimerExpired(this.Session);
        }

        private void _releasedTimer_Elapsed(object sender, EventArgs e)
        {
            this.State.releasedTimerExpired(this.Session);
        }

        internal override void activatePendingAction()
        {
            this.Manager.activatePendingAction();
        }

        public override void changeState(EStateId stateId)
        {
            switch (stateId)
            {
                case EStateId.INCOMING:
                    this.changeState(this._stateIncoming);
                    break;

                case EStateId.HOLDING:
                    this.changeState(this._stateHolding);
                    break;

                case EStateId.TERMINATED:
                    this.changeState(this._stateTerminated);
                    break;

                case EStateId.IDLE:
                    this.changeState(this._stateIdle);
                    break;

                case EStateId.CONNECTING:
                    this.changeState(this._stateCalling);
                    break;

                case EStateId.ALERTING:
                    this.changeState(this._stateAlerting);
                    break;

                case EStateId.ACTIVE:
                    this.changeState(this._stateActive);
                    break;

                case EStateId.RELEASED:
                    this.changeState(this._stateReleased);
                    break;
            }
            if (((this._manager != null) && (this.Session != -1)) && !this.DisableStateNotifications)
            {
                this._manager.updateGui(this.Session);
            }
        }

        private void changeState(IAbstractState state)
        {
            this._state.onExit();
            this._state = state;
            this._state.onEntry();
        }

        public override void destroy()
        {
            this.stopAllTimers();
            this.MediaProxy.stopTone();
            if (this.Counting)
            {
                this.Duration = DateTime.Now.Subtract(this.Time);
            }
            if (((this.Type != ECallType.EDialed) || (this.CallingNumber.Length > 0)) && (this.Type != ECallType.EUndefined))
            {
                this.CallLoger.addCall(this.Type, this.CallingNumber, this.CallingName, this.Time, this.Duration);
                this.CallLoger.save();
            }
            this.CallingNumber = "";
            this.Incoming = false;
            this.changeState(EStateId.IDLE);
            if (this._manager != null)
            {
                this._manager.destroySession(this.Session);
            }
        }

        internal override bool startTimer(ETimerType ttype)
        {
            switch (ttype)
            {
                case ETimerType.ENOREPLY:
                    return this._noreplyTimer.Start();

                case ETimerType.ERELEASED:
                    return this._releasedTimer.Start();

                case ETimerType.ENORESPONSE:
                    return this._noresponseTimer.Start();
            }
            return false;
        }

        internal override void stopAllTimers()
        {
            this._noreplyTimer.Stop();
            this._releasedTimer.Stop();
            this._noresponseTimer.Stop();
        }

        internal override bool stopTimer(ETimerType ttype)
        {
            switch (ttype)
            {
                case ETimerType.ENOREPLY:
                    return this._noreplyTimer.Stop();

                case ETimerType.ERELEASED:
                    return this._releasedTimer.Stop();

                case ETimerType.ENORESPONSE:
                    return this._noresponseTimer.Stop();
            }
            return false;
        }

        public override string CallingName
        {
            get
            {
                return this._callingName;
            }
            set
            {
                this._callingName = value;
            }
        }

        public override string CallingNumber
        {
            get
            {
                return this._callingNumber;
            }
            set
            {
                this._callingNumber = value;
            }
        }

        protected ICallLogInterface CallLoger
        {
            get
            {
                return this._manager.CallLogger;
            }
        }

        internal override ICallProxyInterface CallProxy
        {
            get
            {
                return this._sigProxy;
            }
        }

        public override string Codec
        {
            get
            {
                return this._sigProxy.getCurrentCodec();
            }
        }

        internal override IConfiguratorInterface Config
        {
            get
            {
                return this._manager.Config;
            }
        }

        internal override bool Counting
        {
            get
            {
                return this._counting;
            }
            set
            {
                this._counting = value;
            }
        }

        internal override bool DisableStateNotifications
        {
            get
            {
                return this._disableStateNotifications;
            }
            set
            {
                this._disableStateNotifications = value;
            }
        }

        public override TimeSpan Duration
        {
            get
            {
                return this._duration;
            }
            set
            {
                this._duration = value;
            }
        }

        internal override bool HoldRequested
        {
            get
            {
                return this._holdRequested;
            }
            set
            {
                this._holdRequested = value;
            }
        }

        public override bool Incoming
        {
            get
            {
                return this._incoming;
            }
            set
            {
                this._incoming = value;
            }
        }

        public override bool Is3Pty
        {
            get
            {
                return this._is3Pty;
            }
            set
            {
                this._is3Pty = value;
            }
        }

        public override bool IsHeld
        {
            get
            {
                return this._isHeld;
            }
            set
            {
                this._isHeld = value;
            }
        }

        public override bool IsNull
        {
            get
            {
                return false;
            }
        }

        public CCallManager Manager
        {
            get
            {
                return this._manager;
            }
        }

        internal override IMediaProxyInterface MediaProxy
        {
            get
            {
                return this._manager.MediaProxy;
            }
        }

        internal override int NumberOfCalls
        {
            get
            {
                return this.Manager.Count;
            }
        }

        internal override bool RetrieveRequested
        {
            get
            {
                return this._retrieveRequested;
            }
            set
            {
                this._retrieveRequested = value;
            }
        }

        public override TimeSpan RuntimeDuration
        {
            get
            {
                if (this.Counting)
                {
                    return DateTime.Now.Subtract(this.Time);
                }
                return TimeSpan.Zero;
            }
        }

        public override int Session
        {
            get
            {
                return this._session;
            }
            set
            {
                this._session = value;
                this.CallProxy.SessionId = value;
            }
        }

        internal override IAbstractState State
        {
            get
            {
                return this._state;
            }
        }

        public override EStateId StateId
        {
            get
            {
                return this._state.Id;
            }
        }

        internal override DateTime Time
        {
            get
            {
                return this._timestamp;
            }
            set
            {
                this._timestamp = value;
            }
        }

        internal override ECallType Type
        {
            get
            {
                return this._callType;
            }
            set
            {
                this._callType = value;
            }
        }
    }
}

