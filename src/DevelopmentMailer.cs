using Gorilla.Mailer.Interfaces;
using System;
using System.Threading.Tasks;

namespace Gorilla.Mailer
{
    public class DevelopmentMailer : IMailer
    {
        public async Task<string> Send(IMessage message)
        {
            System.Diagnostics.Debug.WriteLine("Email Message : ");
            System.Diagnostics.Debug.WriteLine(message.ToString());

            return Guid.NewGuid().ToString();
        }
    }
}
