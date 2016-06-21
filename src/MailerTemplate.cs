using Gorilla.Mailer.Interfaces;
using Gorilla.Mailer.Templates.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gorilla.Mailer
{
    public class MailerTemplate
    {
        private readonly ITemplateSource _template;
        public virtual Dictionary<string, object> Params { get; set; }

        public MailerTemplate(ITemplateSource template)
        {
            _template = template;
            Params = new Dictionary<string, object>();
        }

        //Methods
        public string Render()
        {
            if (_template == null) throw new NullReferenceException("Template not defined");

            var result = Params.Aggregate(_template.GetContent(), (current, item) =>
            {
                return current.Replace(string.Format("[{0}]", item.Key), (item.Value ?? "").ToString());
            });

            result = RemoveBlankLinkTags(result);

            return result;
        }

        protected string RemoveBlankLinkTags(string content)
        {
            return Regex.Replace(content, @"<(\w+)\b(?:\s+[\w\-.:]+(?:\s*=\s*(?:""[^""]*""|'[^']*'|[\w\-.:]+))?)*\s*/?>\s*</\1\s*>", string.Empty, RegexOptions.Multiline);
        }

        //Statics
        public static MailerTemplate CreateFromString(string templateString)
        {
            return new MailerTemplate(new StringTemplate(templateString));
        }

        public static MailerTemplate CreateFromFileSystem(string path)
        {
            return new MailerTemplate(new FileSystemTemplate(path));
        }

        public static MailerTemplate CreateFromManifestResourceStream(string streamPath)
        {
            var templateSource = new ManifestResourceStreamTemplate(streamPath);
            return new MailerTemplate(templateSource);
        }
    }
}