using Gorilla.Mailer.Interfaces;
using System;

namespace Gorilla.Mailer.Providers
{
    public class MailerProvider
    {
        protected IMailer Create(enEmailProvider provider, string apiKey = null)
        {
            switch (provider)
            {
                case enEmailProvider.SendGrid:
                    return new SendGridMailer(apiKey);
                case enEmailProvider.Development:
                    throw new MailerException("Use CreateForDevelopement method for development", 0);
                default:
                    throw new NotSupportedException("Invalid Email Provider : " + provider);
            }
        }

        protected IMailer CreateForDevelopement(string outputPath, bool autoStart = true)
        {
            return new DevelopmentMailer(outputPath, autoStart);
        }
    }
}