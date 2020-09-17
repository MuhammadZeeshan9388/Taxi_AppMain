namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    internal class CReleasedState : IAbstractState
    {
        public CReleasedState(CStateMachine sm) : base(sm)
        {
            base.Id = EStateId.RELEASED;
        }

        public override bool endCall()
        {
            base.CallProxy.endCall();
            base._smref.destroy();
            return true;
        }

        public override void onEntry()
        {
            base.MediaProxy.playTone(ETones.EToneCongestion);
            if (!base._smref.startTimer(ETimerType.ERELEASED))
            {
                base._smref.destroy();
            }
        }

        public override void onExit()
        {
            base.MediaProxy.stopTone();
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

