using Gorilla.Mailer.Interfaces;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var msg = new SendGridMessage();

            msg.Subject = message.Subject;
            msg.From = new MailAddress(message.From);
            msg.Html = message.Body;

            msg.AddTo(message.To);

            msg.EnableClickTracking();
            msg.EnableOpenTracking();

            var transportWeb = new SendGrid.Web(_apiKey);

            try
            {
                await transportWeb.DeliverAsync(msg);
            }
            catch (Exception ex)
            {
                throw new MailerException(ex.Message, (byte)EmailResultStatus.Rejected);
            }

            return null;
        }

    }
}
