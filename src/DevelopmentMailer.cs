using Gorilla.Mailer.Interfaces;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Gorilla.Mailer
{
    /// <summary>
    /// Mailer for developmento purpose
    /// </summary>
    public class DevelopmentMailer : IMailer
    {
        public async Task<string> Send(IMessage message)
        {
            System.Diagnostics.Debug.WriteLine("Email Message : ");
            System.Diagnostics.Debug.WriteLine(message.ToString());

            var outputPath = ConfigurationManager.AppSettings["Gorilla.Mailer.DevelopmentOutputPath"];

            if (!string.IsNullOrWhiteSpace(outputPath))
            {
                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                var fileName = Path.Combine(outputPath, Guid.NewGuid() + ".htm");
                File.WriteAllText(fileName, message.Body);

                System.Diagnostics.Process.Start(fileName);
            }

            return Guid.NewGuid().ToString();
        }
    }
}
