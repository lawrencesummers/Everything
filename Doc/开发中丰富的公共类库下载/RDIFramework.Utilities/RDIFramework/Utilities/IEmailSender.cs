namespace RDIFramework.Utilities
{
    using System;

    public interface IEmailSender
    {
        void Send(string from, string to, string subject, string messageText);
    }
}

