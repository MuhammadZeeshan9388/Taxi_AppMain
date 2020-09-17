namespace Sipek.Sip
{
    using Sipek.Common;
    using System;
    using System.Runtime.CompilerServices;

    internal delegate int OnCallStateChanged(int callId, ESessionState stateId);
}

