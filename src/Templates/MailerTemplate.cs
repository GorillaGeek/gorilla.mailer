using Gorilla.Mailer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gorilla.Mailer.Templates
{
    public class MailerTemplate
    {
        private ITemplateSource _template;

        public virtual Dictionary<string, object> Params { get; set; }

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

            var result = Params.Aggregate(_template.GetContent(), (current, item) =>
                current.Replace(string.Format("[{0}]", item.Key), (item.Value ?? "").ToString())
                );

            result = this.RemoveBlankLinkTags(result);

            return result;
        }

        protected string RemoveBlankLinkTags(string content)
        {
            var result = Regex.Replace(content, @"<(\w+)\b(?:\s+[\w\-.:]+(?:\s*=\s*(?:""[^""]*""|'[^']*'|[\w\-.:]+))?)*\s*/?>\s*</\1\s*>", string.Empty, RegexOptions.Multiline);

            System.Diagnostics.Debug.WriteLine("HTML Replaced. \n Source {0} \n\n Result {1}", content, result);

            return result;
        }

        public static MailerTemplate CreateFromString(string templateString)
        {
            return (new MailerTemplate()).SetTemplate(new StringTemplate(templateString));
        }

        public static MailerTemplate CreateFromFileSystem(string path)
        {
            return (new MailerTemplate()).SetTemplate(new FileSystemTemplate(path));
        }

        public static MailerTemplate CreateFromManifestResourceStream(string streamPath)
        {
            var template = new ManifestResourceStreamTemplate(streamPath);
            return (new MailerTemplate()).SetTemplate(template);
        }
    }
}