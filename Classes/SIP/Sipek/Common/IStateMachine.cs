namespace Sipek.Common
{
    using System;

    public abstract class IStateMachine
    {
        protected IStateMachine()
        {
        }

        internal abstract void activatePendingAction();
        public abstract void changeState(EStateId stateId);
        public abstract void destroy();
        internal abstract bool startTimer(ETimerType ttype);
        internal abstract void stopAllTimers();
        internal abstract bool stopTimer(ETimerType ttype);

        public abstract string CallingName { get; set; }

        public abstract string CallingNumber { get; set; }

        internal abstract ICallProxyInterface CallProxy { get; }

        public abstract string Codec { get; }

        internal abstract IConfiguratorInterface Config { get; }

        internal abstract bool Counting { get; set; }

        internal abstract bool DisableStateNotifications { get; set; }

        public abstract TimeSpan Duration { get; set; }

        internal abstract bool HoldRequested { get; set; }

        public abstract bool Incoming { get; set; }

        public abstract bool Is3Pty { get; set; }

        public abstract bool IsHeld { get; set; }

        public abstract bool IsNull { get; }

        internal abstract IMediaProxyInterface MediaProxy { get; }

        internal abstract int NumberOfCalls { get; }

        internal abstract bool RetrieveRequested { get; set; }

        public abstract TimeSpan RuntimeDuration { get; }

        public abstract int Session { get; set; }

        internal abstract IAbstractState State { get; }

        public abstract EStateId StateId { get; }

        internal abstract DateTime Time { get; set; }

        internal abstract ECallType Type { get; set; }
    }
}

