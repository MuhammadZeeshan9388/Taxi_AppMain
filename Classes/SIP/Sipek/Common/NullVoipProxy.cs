namespace Sipek.Common
{
    using System;

    public class NullVoipProxy : IVoipProxy
    {
        public override ICallProxyInterface createCallProxy()
        {
            return new NullCallProxy();
        }

        public override string getCodec(int i)
        {
            return "";
        }

        public override int getNoOfCodecs()
        {
            return 0;
        }

        public override int initialize()
        {
            return 1;
        }

        public override void setCodecPriority(string item, int p)
        {
        }

        public override int shutdown()
        {
            return 1;
        }

        public override bool IsInitialized
        {
            get
            {
                return false;
            }
            set
            {
            }
        }
    }
}

