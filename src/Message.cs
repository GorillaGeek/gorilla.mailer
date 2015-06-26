
using Gorilla.Mailer.Interfaces;

namespace Gorilla.Mailer
{
    public class Message : IMessage
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }

        private Message(string subject, string from, string to, string body)
        {
            this.Subject = subject;
            this.From = from;
            this.To = to;
            this.Body = body;
        }

        public static IMessage Create(string subject, string from, string to, string body)
        {
            return new Message(subject, from, to, body);
        }

    }
}
