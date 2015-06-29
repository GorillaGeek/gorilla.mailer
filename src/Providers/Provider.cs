using Gorilla.Mailer.Interfaces;
using Ninject.Activation;
using System;
using System.Configuration;

namespace Gorilla.Mailer.Providers
{
    public class MailerProvider : Provider<IMailer>
    {
        protected override IMailer CreateInstance(IContext context)
        {
            var providerName = ConfigurationManager.AppSettings["Gorilla.Mailer.Provider"];
            var key = ConfigurationManager.AppSettings["Gorilla.Mailer.Key"];
            //var secret = ConfigurationManager.AppSettings["Gorilla.Mailer.Secret"];

            var provider = (enEmailProvider)Enum.Parse(typeof(enEmailProvider), providerName);

            switch (provider)
            {
                case enEmailProvider.Mandrill:
                    return new MandrillMailer(key);
                case enEmailProvider.Development:
                    return new DevelopmentMailer();
                default:
                    throw new NotSupportedException("Invalid Email Provider : " + provider);
            }
        }
    }
}