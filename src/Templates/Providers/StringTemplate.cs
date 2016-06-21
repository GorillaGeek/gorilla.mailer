using Gorilla.Mailer.Interfaces;

namespace Gorilla.Mailer.Templates.Providers
{
    public class StringTemplate : ITemplateSource
    {
        private readonly string _content;

        public StringTemplate(string content)
        {
            _content = content;
        }

        public string GetContent()
        {
            return this._content;
        }
    }
}
