using Gorilla.Mailer.Interfaces;
using System.IO;

namespace Gorilla.Mailer.Templates
{
    public class FileSystemTemplate : ITemplateSource
    {
        private readonly string _content;

        public FileSystemTemplate(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            _content = File.ReadAllText(path);
        }

        public string GetContent()
        {
            return this._content;
        }
    }
}
