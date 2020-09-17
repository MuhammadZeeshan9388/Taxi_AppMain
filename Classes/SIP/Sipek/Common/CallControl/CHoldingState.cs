namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    internal class CHoldingState : IAbstractState
    {
        public CHoldingState(CStateMachine sm) : base(sm)
        {
            base.Id = EStateId.HOLDING;
        }

        public override bool endCall()
        {
            base.CallProxy.endCall();
            base._smref.changeState(EStateId.TERMINATED);
            return base.endCall();
        }

        public override void onEntry()
        {
        }

        public override void onExit()
        {
        }

        public override void onReleased()
        {
            base._smref.changeState(EStateId.RELEASED);
        }

        public override bool retrieveCall()
        {
            base._smref.RetrieveRequested = true;
            base.CallProxy.retrieveCall();
            base._smref.changeState(EStateId.ACTIVE);
            return true;
        }
    }
}

