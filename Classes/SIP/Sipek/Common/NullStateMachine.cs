namespace Sipek.Common
{
    using System;

    internal class NullStateMachine : IStateMachine
    {
        internal override void activatePendingAction()
        {
        }

        public override void changeState(EStateId stateId)
        {
        }

        public override void destroy()
        {
        }

        internal override bool startTimer(ETimerType ttype)
        {
            return false;
        }

        internal override void stopAllTimers()
        {
        }

        internal override bool stopTimer(ETimerType ttype)
        {
            return false;
        }

        public override string CallingName
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        public override string CallingNumber
        {
            get
            {
                return "";
            }
            set
            {
            }
        }

        internal override ICallProxyInterface CallProxy
        {
            get
            {
                return new NullCallProxy();
            }
        }

        public override string Codec
        {
            get
            {
                return "PCMA";
            }
        }

        internal override IConfiguratorInterface Config
        {
            get
            {
                return new NullConfigurator();
            }
        }

        internal override bool Counting
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        internal override bool DisableStateNotifications
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        public override TimeSpan Duration
        {
            get
            {
                return new TimeSpan();
            }
            set
            {
            }
        }

        internal override bool HoldRequested
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override bool Incoming
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override bool Is3Pty
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override bool IsHeld
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override bool IsNull
        {
            get
            {
                return true;
            }
        }

        internal override IMediaProxyInterface MediaProxy
        {
            get
            {
                return new NullMediaProxy();
            }
        }

        internal override int NumberOfCalls
        {
            get
            {
                return 0;
            }
        }

        internal override bool RetrieveRequested
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        public override TimeSpan RuntimeDuration
        {
            get
            {
                return new TimeSpan();
            }
        }

        public override int Session
        {
            get
            {
                return -1;
            }
            set
            {
            }
        }

        internal override IAbstractState State
        {
            get
            {
                return new NullState();
            }
        }

        public override EStateId StateId
        {
            get
            {
                return EStateId.NULL;
            }
        }

        internal override DateTime Time
        {
            get
            {
                return new DateTime();
            }
            set
            {
            }
        }

        internal override ECallType Type
        {
            get
            {
                return ECallType.EDialed;
            }
            set
            {
            }
        }
    }
}

