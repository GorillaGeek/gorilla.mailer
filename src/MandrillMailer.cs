using Gorilla.Mailer.Interfaces;
using Mandrill;
using Mandrill.Models;
using Mandrill.Requests.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gorilla.Mailer
{
    public class MandrillMailer : IMailer
    {

        private readonly MandrillApi _api;

        public MandrillMailer(string key)
        {
            _api = new MandrillApi(key);
        }

        public async Task<string> Send(IMessage message)
        {

            //var info = _api.UserInfo();

            var msg = new EmailMessage();

            msg.Subject = message.Subject;
            msg.FromEmail = message.From;
            msg.Html = message.Body;

            msg.To = new List<EmailAddress> { new EmailAddress() { Email = message.To } };

            msg.TrackClicks = true;
            msg.TrackOpens = true;

            var request = new SendMessageRequest(msg);

            var result = (await this._api.SendMessage(request)).FirstOrDefault();

            if (result == null)
            {
                return null;
            }

            if (result.Status == EmailResultStatus.Rejected)
            {
                throw new MailerException(result.RejectReason, (byte)result.Status);
            }

            if (result.Status != EmailResultStatus.Sent)
            {
                throw new MailerException("Sending email failed", (byte)result.Status);
            }

            return result.Id;
        }

    }
}
