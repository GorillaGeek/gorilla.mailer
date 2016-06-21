using Gorilla.Mailer.Interfaces;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Gorilla.Mailer
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
            return await Send(message.Subject, message.From, message.To, message.Body);
        }

        public async Task<string> Send(string subject, string from, string to, string body)
        {
            var msg = new SendGrid.SendGridMessage();

            msg.Subject = subject;
            msg.From = new MailAddress(from);
            msg.Html = body;

            msg.AddTo(to);

            msg.EnableClickTracking();
            msg.EnableOpenTracking();

            var transportWeb = new SendGrid.Web(_apiKey);

            try
            {
                await transportWeb.DeliverAsync(msg);
            }
            catch (Exception ex)
            {
                throw new MailerException(ex.Message, 1);
            }

            return null;
        }

    }
}
