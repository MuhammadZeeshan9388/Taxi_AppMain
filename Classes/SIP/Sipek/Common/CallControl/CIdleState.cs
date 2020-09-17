namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    internal class CIdleState : IAbstractState
    {
        public CIdleState(IStateMachine sm) : base(sm)
        {
            base.Id = EStateId.IDLE;
        }

        public override bool endCall()
        {
            base._smref.destroy();
            base.CallProxy.endCall();
            return base.endCall();
        }

        public override void incomingCall(string callingNo, string display)
        {
            if (base._smref.Config.CFUFlag && (base._smref.Config.CFUNumber.Length > 0))
            {
                base._smref.DisableStateNotifications = true;
                base.CallProxy.serviceRequest(1, base._smref.Config.CFUNumber);
                this.endCall();
            }
            else if (base._smref.Config.DNDFlag)
            {
                base._smref.DisableStateNotifications = true;
                base.CallProxy.serviceRequest(3, "");
                this.endCall();
            }
            else
            {
                base._smref.CallingNumber = callingNo;
                base._smref.CallingName = display;
                base._smref.changeState(EStateId.INCOMING);
            }
        }

        public override int makeCall(string dialedNo, int accountId)
        {
            base._smref.CallingNumber = dialedNo;
            base._smref.changeState(EStateId.CONNECTING);
            base._smref.Session = base.CallProxy.makeCall(dialedNo, accountId);
            return base._smref.Session;
        }

        public override void onEntry()
        {
        }

        public override void onExit()
        {
        }
    }
}

