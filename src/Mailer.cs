using Gorilla.Mailer.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gorilla.Mailer
{
    /// <summary>
    /// Mailer strategy for 
    /// </summary>
    public class Mailer : IDisposable, IMailer
    {
        private readonly IMailer _mailer;

        public Mailer(IMailer mailer)
        {
            _mailer = mailer;
        }

        public Task<string> Send(IMessage message)
        {
            return this._mailer.Send(message);
        }

        public static Mailer Create(enEmailProvider provider, string options = null)
        {

            Dictionary<string, string> opt = new Dictionary<string, string>();

            if (!String.IsNullOrWhiteSpace(options))
            {
                opt = JsonConvert.DeserializeObject<Dictionary<string, string>>(options);
            }

            switch (provider)
            {
                case enEmailProvider.Mandrill:
                    return new Mailer(new MandrillMailer(opt["key"]));
                default:
                    return new Mailer(new MandrillMailer(System.Configuration.ConfigurationManager.AppSettings["Gorilla.Mailer.APIKey"]));
            }
        }

        public Task<string> Send(string subject, string from, string to, string body)
        {
            var message = Message.Create(subject, from, to, body);
            return this.Send(message);
        }


        public void Dispose()
        {

        }
    }
}
