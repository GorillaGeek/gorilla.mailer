using Gorilla.Mailer.Interfaces;
using System.IO;
using System.Reflection;

namespace Gorilla.Mailer.Templates
{
    public class ManifestResourceStreamTemplate : ITemplateSource
    {
        private readonly string _resourcePath;

        public ManifestResourceStreamTemplate(string resourcePath)
        {
            _resourcePath = resourcePath;
        }

        public string GetContent()
        {
            string result;
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(_resourcePath))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
