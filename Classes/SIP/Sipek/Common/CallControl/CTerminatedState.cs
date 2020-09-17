namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    internal class CTerminatedState : IAbstractState
    {
        public CTerminatedState(CStateMachine sm) : base(sm)
        {
            base.Id = EStateId.TERMINATED;
        }

        public override bool endCall()
        {
            base.CallProxy.endCall();
            return true;
        }

        public override void onAlerting()
        {
            base.CallProxy.endCall();
        }

        public override void onConnect()
        {
            base.CallProxy.endCall();
        }

        public override void onEntry()
        {
            if (!base._smref.startTimer(ETimerType.ERELEASED))
            {
                base._smref.destroy();
            }
        }

        public override void onExit()
        {
            base._smref.stopAllTimers();
        }

        public override void onReleased()
        {
            base._smref.destroy();
        }

        public override bool releasedTimerExpired(int sessionId)
        {
            base._smref.destroy();
            return true;
        }
    }
}

