namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    internal class CActiveState : IAbstractState
    {
        public CActiveState(CStateMachine sm) : base(sm)
        {
            base.Id = EStateId.ACTIVE;
        }

        public override bool conferenceCall()
        {
            return base.CallProxy.conferenceCall();
        }

        public override bool endCall()
        {
            base._smref.Duration = DateTime.Now.Subtract(base._smref.Time);
            base._smref.changeState(EStateId.TERMINATED);
            base.CallProxy.endCall();
            return base.endCall();
        }

        public override bool holdCall()
        {
            base._smref.HoldRequested = true;
            return base.CallProxy.holdCall();
        }

        public override void onEntry()
        {
            base._smref.Counting = true;
            base.MediaProxy.stopTone();
        }

        public override void onExit()
        {
        }

        public override void onHoldConfirm()
        {
            if (base._smref.HoldRequested)
            {
                base._smref.changeState(EStateId.HOLDING);
                base._smref.activatePendingAction();
            }
            base._smref.HoldRequested = false;
        }

        public override void onReleased()
        {
            base._smref.changeState(EStateId.RELEASED);
        }

        public override bool xferCall(string number)
        {
            return base.CallProxy.xferCall(number);
        }

        public override bool xferCallSession(int partnersession)
        {
            return base.CallProxy.xferCallSession(partnersession);
        }
    }
}

