using Gorilla.Mailer.Exceptions;
using Gorilla.Mailer.Interfaces;
using Gorilla.Mailer.Providers;
using System;

namespace Gorilla.Mailer
{
    public class MailerProvider
    {
        public static IMailer Create(enEmailProvider provider, string apiKey = null)
        {
            switch (provider)
            {
                case enEmailProvider.SendGrid:
                    return new SendGridMailer(apiKey);
                case enEmailProvider.Development:
                    throw new MailerException("Use CreateForDevelopement method for development", MailerException.enReason.InvalidConfiguration);
                default:
                    throw new NotSupportedException("Invalid Email Provider : " + provider);
            }
        }

        public static IMailer CreateForDevelopement(string outputPath, bool autoStart = true)
        {
            return new DevelopmentMailer(outputPath, autoStart);
        }
    }
}