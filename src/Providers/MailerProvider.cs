using Gorilla.Mailer.Interfaces;
using Ninject.Activation;
using System;

namespace Gorilla.Mailer.Providers
{
    public class MailerProvider
    {
        protected IMailer CreateInstance(IContext context, enEmailProvider provider, string key)
        {
            switch (provider)
            {
                case enEmailProvider.Mandrill:
                    return new MandrillMailer(key);
                case enEmailProvider.SendGrid:
                    return new SendGridMailer(key);
                case enEmailProvider.Development:
                    return new DevelopmentMailer();
                default:
                    throw new NotSupportedException("Invalid Email Provider : " + provider);
            }
        }
    }
}