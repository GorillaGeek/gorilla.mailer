using System;

namespace Gorilla.Mailer
{
    class MailerException : Exception
    {

        public byte RejectionCode { get; set; }

        public MailerException(string message, byte RejectionCode)
            : base(message)
        {
            this.RejectionCode = RejectionCode;
        }

    }
}
