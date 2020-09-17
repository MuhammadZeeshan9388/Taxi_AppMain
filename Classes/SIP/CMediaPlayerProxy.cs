using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sipek.Common;
using System.Media;

using System.IO;

namespace CallerIdData
{
    public class CMediaPlayerProxy : IMediaProxyInterface
    {
        SoundPlayer player = new SoundPlayer();

        public int playTone(ETones toneId)
        {
           

            string fname;

            switch (toneId)
            {
                case ETones.EToneDial:
                    fname = "Sounds/dial.wav";
                    break;
                case ETones.EToneCongestion:
                    fname = "Sounds/congestion.wav";
                    break;
                case ETones.EToneRingback:
                    fname = "Sounds/ringback.wav";
                    break;
                case ETones.EToneRing:
                    fname = Environment.CurrentDirectory + @"\Sounds\ring.wav";
                    break;
                default:
                    fname = "";
                    break;
            }

            if (System.IO.File.Exists(fname))
            {
                player.SoundLocation = fname;

                player.Load();
                player.PlayLooping();
            }
            return 1;
        }



        #region IMediaProxyInterface Members


        public int stopTone()
        {
            player.Stop();
            return 0;
        }

        #endregion
    }

}
