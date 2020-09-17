namespace Sipek.Common
{
    using System;

    internal class NullTimer : ITimer
    {
        public bool Start()
        {
            return false;
        }

        public bool Stop()
        {
            return false;
        }

        public TimerExpiredCallback Elapsed
        {
            set
            {
            }
        }

        public int Interval
        {
            get
            {
                return 100;
            }
            set
            {
            }
        }
    }
}

