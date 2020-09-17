using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taxi_AppMain
{
    public class SIPPhoneCfgWrap
    {
        //sip account info
        public string DisplayName;
        public string UserName;
        public string Password;
        public string Domain;
        public string Proxy;


        public bool RegisterDomain;

        //network
        public ushort SIPPort;
        public ushort RTPPort;
        public string StunServer;
        public bool UseNATAddr;

        //audio codec
        public string AudioCodecs;

        //audio device
        public string Speaker;
        public string Microphone;
        public bool RecordCalls;
        public string RecordRoot;

        public SIPPhoneCfgWrap()
        {
            DisplayName = "";
            UserName = "";
            Password = "";
            Domain = "";
            Proxy = "";
            RegisterDomain = true;

            SIPPort = 5066;
            RTPPort = 12400;
            StunServer = "";
            UseNATAddr = false;

            AudioCodecs = "0,8,3";

            Speaker = "";
            Microphone = "";
            RecordCalls = false;
            RecordRoot = "";
        }

    }
}
