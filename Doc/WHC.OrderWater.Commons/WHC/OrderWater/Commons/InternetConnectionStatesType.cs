namespace WHC.OrderWater.Commons
{
    using System;

    [Flags]
    public enum InternetConnectionStatesType
    {
        ConnectionConfigured = 0x40,
        LANConnection = 2,
        ModemConnection = 1,
        Offline = 0x20,
        ProxyConnection = 4,
        RASInstalled = 0x10
    }
}

