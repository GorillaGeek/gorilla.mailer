using Gorilla.Mailer.Interfaces;
using System.IO;
using System.Reflection;

namespace Gorilla.Mailer.Templates.Providers
{
    public class ManifestResourceStreamTemplate : ITemplateSource
    {
        private readonly string _resourcePath;
        private readonly Assembly _assembly;

        public ManifestResourceStreamTemplate(string resourcePath, Assembly assembly)
        {
            _resourcePath = resourcePath;
            _assembly = assembly;
        }

        public string GetContent()
        {
            string result;

            using (Stream stream = _assembly.GetManifestResourceStream(_resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
