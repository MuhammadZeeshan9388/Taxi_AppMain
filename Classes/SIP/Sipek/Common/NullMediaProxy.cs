namespace Sipek.Common
{
    using System;

    internal class NullMediaProxy : IMediaProxyInterface
    {
        public int playTone(ETones toneId)
        {
            return 1;
        }

        public int stopTone()
        {
            return 1;
        }
    }
}

