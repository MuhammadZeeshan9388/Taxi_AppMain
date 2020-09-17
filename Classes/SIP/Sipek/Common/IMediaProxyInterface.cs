namespace Sipek.Common
{
    using System;

    public interface IMediaProxyInterface
    {
        int playTone(ETones toneId);
        int stopTone();
    }
}

