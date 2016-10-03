using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Gorilla.Mailer.Interfaces;

namespace Gorilla.Mailer.Providers
{
    public class SmtpMailer : IMailer
    {
        private readonly SmtpClient _client = new SmtpClient();

        public async Task<string> Send(IMessage message)
        {
            try
            {
                var smtpMessage = new MailMessage()
                {
                    IsBodyHtml = true,
                    Body = message.Body,
                    Subject = message.Subject,
                    Priority = MailPriority.High,
                    From = new MailAddress(message.From),
                    BodyEncoding = Encoding.GetEncoding("iso-8859-1")
                };

                smtpMessage.To.Add(message.To);

                await _client.SendMailAsync(smtpMessage);

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
