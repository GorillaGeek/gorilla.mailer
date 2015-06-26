using Gorilla.Mailer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gorilla.Mailer.Templates
{
    public class MailerTemplate
    {
        private ITemplateSource _template;

        public Dictionary<string, object> Params { get; set; }

        public MailerTemplate()
        {
            Params = new Dictionary<string, object>();
        }

        public MailerTemplate SetTemplate(ITemplateSource template)
        {
            _template = template;
            return this;
        }

        public string Render()
        {
            if (_template == null)
            {
                throw new NullReferenceException("Template not defined");
            }

            return Params.Aggregate(_template.GetContent(), (current, item) => current.Replace(string.Format("[{0}]", item.Key), item.Value.ToString()));
        }

        public static MailerTemplate CreateFromFileSystem(string path)
        {
            return (new MailerTemplate()).SetTemplate(new FileSystemTemplate(path));
        }
    }
}