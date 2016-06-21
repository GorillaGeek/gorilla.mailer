using Gorilla.Mailer.Exceptions;
using Gorilla.Mailer.Interfaces;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Gorilla.Mailer.Providers
{
    public class SendGridMailer : IMailer
    {
        private readonly string _apiKey;

        public SendGridMailer(string key)
        {
            _apiKey = key;
        }

        public async Task<string> Send(IMessage message)
        {
            var msg = new SendGrid.SendGridMessage();

            msg.Subject = message.Subject;
            msg.From = new MailAddress(message.From);
            msg.Html = message.Body;

            msg.AddTo(message.To);

            msg.EnableClickTracking();
            msg.EnableOpenTracking();

            try
            {
                var transportWeb = new SendGrid.Web(_apiKey);
                await transportWeb.DeliverAsync(msg);
            }
            catch (Exception ex)
            {
                throw new MailerException(ex.Message, MailerException.enReason.RejectByProvider);
            }

            return null;
        }

    }
}
