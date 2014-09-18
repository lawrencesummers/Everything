namespace RDIFramework.Utilities
{
    using System;

    public interface IConfiguration
    {
        string GetValue(string key);
    }
}

