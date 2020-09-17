namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    internal class CConnectingState : IAbstractState
    {
        public CConnectingState(CStateMachine sm) : base(sm)
        {
            base.Id = EStateId.CONNECTING;
        }

        public override bool endCall()
        {
            base._smref.changeState(EStateId.TERMINATED);
            base.CallProxy.endCall();
            return base.endCall();
        }

        public override void onAlerting()
        {
            base._smref.changeState(EStateId.ALERTING);
        }

        public override void onConnect()
        {
            base._smref.changeState(EStateId.ACTIVE);
        }

        public override void onEntry()
        {
            base._smref.Type = ECallType.EDialed;
        }

        public override void onExit()
        {
        }

        public override void onReleased()
        {
            base._smref.changeState(EStateId.RELEASED);
        }
    }
}

