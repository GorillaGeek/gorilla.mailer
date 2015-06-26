namespace Gorilla.Mailer.Interfaces
{
    public interface IMessage
    {
        string Subject { get; }
        string From { get; }
        string To { get; }
        string Body { get; }
    }
}
