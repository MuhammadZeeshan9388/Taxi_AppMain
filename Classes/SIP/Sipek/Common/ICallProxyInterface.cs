namespace Sipek.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ICallProxyInterface
    {
        public static  event DCallIncoming CallIncoming;

        public static  event DCallNotification CallNotification;

        public static  event DCallStateChanged CallStateChanged;

        protected ICallProxyInterface()
        {
        }

        public abstract bool acceptCall();
        public abstract bool alerted();
        protected static void BaseCallNotification(int callId, ECallNotification notifFlag, string text)
        {
            if (CallNotification != null)
            {
                CallNotification(callId, notifFlag, text);
            }
        }

        protected static void BaseCallStateChanged(int callId, ESessionState callState, string info)
        {
            if (CallStateChanged != null)
            {
                CallStateChanged(callId, callState, info);
            }
        }

        protected static void BaseIncomingCall(int callId, string number, string info)
        {
            if (CallIncoming != null)
            {
                CallIncoming(callId, number, info);
            }
        }

        public abstract bool conferenceCall();
        public abstract bool dialDtmf(string digits, EDtmfMode mode);
        public abstract bool endCall();
        public abstract string getCurrentCodec();
        public abstract bool holdCall();
        public abstract int makeCall(string dialedNo, int accountId);
        public abstract bool retrieveCall();
        public abstract bool serviceRequest(int code, string dest);
        public abstract bool threePtyCall(int partnersession);
        public abstract bool xferCall(string number);
        public abstract bool xferCallSession(int partnersession);

        public abstract int SessionId { get; set; }
    }
}

