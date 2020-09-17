namespace Sipek.Common
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate void DCallNotification(int callId, ECallNotification notFlag, string text);
}

