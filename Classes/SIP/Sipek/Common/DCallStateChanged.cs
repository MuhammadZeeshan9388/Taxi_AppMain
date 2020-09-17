namespace Sipek.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void DCallStateChanged(int callId, ESessionState callState, string info);
}

