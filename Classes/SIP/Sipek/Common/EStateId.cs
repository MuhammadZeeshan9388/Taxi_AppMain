namespace Sipek.Common
{
    using System;

    public enum EStateId
    {
        ACTIVE = 8,
        ALERTING = 4,
        CONNECTING = 2,
        HOLDING = 0x40,
        IDLE = 1,
        INCOMING = 0x20,
        NULL = 0,
        RELEASED = 0x10,
        TERMINATED = 0x80
    }
}

