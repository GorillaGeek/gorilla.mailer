using System;

namespace Gorilla.Mailer.Exceptions
{
    public class MailerException : Exception
    {
        public enum enReason
        {
            InvalidConfiguration = 1,
            RejectByProvider = 2
        }


        public MailerException(string message, enReason reason) : base(message)
        {
            Reason = reason;
        }

        public enReason Reason { get; set; }
    }
}
