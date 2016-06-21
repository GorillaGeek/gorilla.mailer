
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
            Subject = subject;
            From = from;
            To = to;
            Body = body;
        }

        public static IMessage Create(string subject, string from, string to, string body)
        {
            return new Message(subject, from, to, body);
        }

    }
}
