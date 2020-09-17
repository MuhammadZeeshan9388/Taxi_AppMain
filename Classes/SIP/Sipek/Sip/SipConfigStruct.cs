namespace Sipek.Sip
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public class SipConfigStruct
    {
        private static SipConfigStruct _instance;
        public int listenPort = 0x13c4;
        [MarshalAs(UnmanagedType.I1)]
        public bool noUDP;
        [MarshalAs(UnmanagedType.I1)]
        public bool noTCP = true;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0xff)]
        public string stunServer;
        [MarshalAs(UnmanagedType.I1)]
        public bool publishEnabled;
        public int expires = 0xe10;
        [MarshalAs(UnmanagedType.I1)]
        public bool VADEnabled = true;
        public int ECTail = 200;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=0xff)]
        public string nameServer;
        [MarshalAs(UnmanagedType.I1)]
        public bool pollingEventsEnabled;
        [MarshalAs(UnmanagedType.I1)]
        public bool imsEnabled;
        [MarshalAs(UnmanagedType.I1)]
        public bool imsIPSecHeaders;
        [MarshalAs(UnmanagedType.I1)]
        public bool imsIPSecTransport;
        public static SipConfigStruct Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SipConfigStruct();
                }
                return _instance;
            }
        }
    }
}

