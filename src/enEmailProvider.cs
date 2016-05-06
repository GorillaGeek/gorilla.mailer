
namespace Gorilla.Mailer
{
    public enum enEmailProvider
    {
        /// <summary>
        /// Default mailer strategy
        /// </summary>
        Default = 0,

        /// <summary>
        /// Mandrill strategy
        /// </summary>
        Mandrill = 1,

        Development = 2,

        SendGrid = 3
    }
}
