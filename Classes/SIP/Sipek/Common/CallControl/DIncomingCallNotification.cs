namespace Sipek.Common.CallControl
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void DIncomingCallNotification(int sessionId, string number, string info);

    public delegate void DAnswerCallNotification(int sessionId, string number, string info);
}

