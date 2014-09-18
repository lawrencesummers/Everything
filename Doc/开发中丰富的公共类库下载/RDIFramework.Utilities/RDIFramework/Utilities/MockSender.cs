namespace RDIFramework.Utilities
{
    using System;

    public class MockSender : IEmailSender, IRichMessageEmailSender
    {
        public virtual void Send(Message message)
        {
        }

        public virtual void Send(Message[] messages)
        {
        }

        public virtual void Send(string from, string to, string subject, string messageText)
        {
        }
    }
}

