namespace Sipek.Common
{
    using System;

    public interface IAccount
    {
        string AccountName { get; set; }

        string DisplayName { get; set; }

        string DomainName { get; set; }

        string HostName { get; set; }

        string Id { get; set; }

        int Index { get; set; }

        string Password { get; set; }

        string ProxyAddress { get; set; }

        int RegState { get; set; }

        ETransportMode TransportMode { get; set; }

        string UserName { get; set; }
    }
}

