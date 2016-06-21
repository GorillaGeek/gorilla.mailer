using Gorilla.Mailer.Interfaces;
using Gorilla.Mailer.Templates.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Gorilla.Mailer
{
    public class Template
    {
        private readonly ITemplateSource _template;
        public virtual Dictionary<string, object> Params { get; set; }

        public Template(ITemplateSource template)
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
        public static Template CreateFromString(string templateString)
        {
            return new Template(new StringTemplate(templateString));
        }

        public static Template CreateFromFileSystem(string path)
        {
            return new Template(new FileSystemTemplate(path));
        }

        public static Template CreateFromManifestResourceStream(string streamPath, Assembly assembly)
        {
            var templateSource = new ManifestResourceStreamTemplate(streamPath, assembly);
            return new Template(templateSource);
        }
    }
}