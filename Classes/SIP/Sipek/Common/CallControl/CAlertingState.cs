namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    internal class CAlertingState : IAbstractState
    {
        public CAlertingState(CStateMachine sm) : base(sm)
        {
            base.Id = EStateId.ALERTING;
        }

        public override bool endCall()
        {
            base._smref.changeState(EStateId.TERMINATED);
            base.CallProxy.endCall();
            return base.endCall();
        }

        public override void onConnect()
        {
            base._smref.Time = DateTime.Now;
            base._smref.changeState(EStateId.ACTIVE);
        }

        public override void onEntry()
        {
            base.MediaProxy.playTone(ETones.EToneRingback);
        }

        public override void onExit()
        {
            base.MediaProxy.stopTone();
        }

        public override void onReleased()
        {
            base._smref.changeState(EStateId.RELEASED);
        }
    }
}

