namespace Sipek.Common.CallControl
{
    using Sipek.Common;
    using System;

    internal class CIncomingState : IAbstractState
    {
        public CIncomingState(CStateMachine sm) : base(sm)
        {
            base.Id = EStateId.INCOMING;
        }

        public override bool acceptCall()
        {
            base._smref.Type = ECallType.EReceived;
            base._smref.Time = DateTime.Now;
            base.CallProxy.acceptCall();
            base._smref.changeState(EStateId.ACTIVE);
            return true;
        }

        public override bool endCall()
        {
            base._smref.changeState(EStateId.TERMINATED);
            base.CallProxy.endCall();
            return base.endCall();
        }

        public override bool noReplyTimerExpired(int sessionId)
        {
            base.CallProxy.serviceRequest(2, base._smref.Config.CFUNumber);
            return true;
        }

        public override bool noResponseTimerExpired(int sessionId)
        {
            base._smref.changeState(EStateId.TERMINATED);
            base.CallProxy.endCall();
            return true;
        }

        public override void onEntry()
        {
            base._smref.Incoming = true;
            int sessionId = this.SessionId;
            base._smref.startTimer(ETimerType.ENORESPONSE);
            base.CallProxy.alerted();
            base._smref.Type = ECallType.EMissed;
            base.MediaProxy.playTone(ETones.EToneRing);
            if (base._smref.Config.CFNRFlag)
            {
                base._smref.startTimer(ETimerType.ENOREPLY);
            }
            if (base._smref.Config.AAFlag && (base._smref.NumberOfCalls == 1))
            {
                this.acceptCall();
            }
        }

        public override void onExit()
        {
            base.MediaProxy.stopTone();
            base._smref.stopAllTimers();
        }

        public override void onReleased()
        {
            base._smref.changeState(EStateId.RELEASED);
        }

        public override bool xferCall(string number)
        {
            return base.CallProxy.serviceRequest(0, number);
        }
    }
}

