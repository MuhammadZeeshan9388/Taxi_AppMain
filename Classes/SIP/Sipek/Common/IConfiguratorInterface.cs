namespace Sipek.Common
{
    using System;
    using System.Collections.Generic;

    public interface IConfiguratorInterface
    {
        void Save();

        bool AAFlag { get; set; }

        List<IAccount> Accounts { get; }

        bool CFBFlag { get; set; }

        string CFBNumber { get; set; }

        bool CFNRFlag { get; set; }

        string CFNRNumber { get; set; }

        bool CFUFlag { get; set; }

        string CFUNumber { get; set; }

        List<string> CodecList { get; set; }

        int DefaultAccountIndex { get; }

        bool DNDFlag { get; set; }

        bool IsNull { get; }

        bool PublishEnabled { get; set; }

        int SIPPort { get; set; }
    }
}

