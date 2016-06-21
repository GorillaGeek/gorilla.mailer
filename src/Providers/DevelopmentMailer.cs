using Gorilla.Mailer.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Gorilla.Mailer.Providers
{
    public class DevelopmentMailer : IMailer
    {
        private readonly string _outputPath;
        private readonly bool _autoStart;

        public DevelopmentMailer(string outputPath, bool autoStart)
        {
            _outputPath = outputPath;
            _autoStart = autoStart;
        }

        public async Task<string> Send(IMessage message)
        {
            if (!Directory.Exists(_outputPath))
            {
                Directory.CreateDirectory(_outputPath);
            }

            var fileName = Path.Combine(_outputPath, DateTime.Now.ToString("yyyyMMddhhmmss-") + (new Random().Next(1, int.MaxValue)) + ".htm");
            File.WriteAllText(fileName, message.Body);

            if (_autoStart)
            {
                System.Diagnostics.Process.Start(fileName);
            }

            return Guid.NewGuid().ToString();
        }
    }
}
