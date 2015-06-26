
namespace Gorilla.Mailer.Interfaces
{
    public interface IMessage
    {

        string Subject { get; set; }
        string From { get; set; }
        string To { get; set; }
        string Body { get; set; }

    }
}
