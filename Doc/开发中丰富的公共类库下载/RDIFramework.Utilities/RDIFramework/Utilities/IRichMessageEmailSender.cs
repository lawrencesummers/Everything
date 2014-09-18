namespace RDIFramework.Utilities
{
    using System;

    public interface IRichMessageEmailSender : IEmailSender
    {
        void Send(Message message);
        void Send(Message[] messages);
    }
}

