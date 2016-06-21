using System.Threading.Tasks;

namespace Gorilla.Mailer.Interfaces
{
    public interface IMailer
    {

        Task<string> Send(IMessage message);

        Task<string> Send(string subject, string from, string to, string body);

    }
}
