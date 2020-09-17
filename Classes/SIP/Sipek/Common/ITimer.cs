namespace Sipek.Common
{
    using System;

    public interface ITimer
    {
        bool Start();
        bool Stop();

        TimerExpiredCallback Elapsed { set; }

        int Interval { get; set; }
    }
}

