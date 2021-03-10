using System.Net;
using System.Net.Mail;

using MEOT.lib.Objects;

namespace MEOT.lib.Helpers
{
    public class Email
    {
        private readonly Settings _settings;

        public Email(Settings settings)
        {
            _settings = settings;
        }

        public void Send(string receiver, string subject, string body)
        {
            var smtpClient = new SmtpClient(_settings.SMTPAddress)
            {
                Port = _settings.SMTPPort,
                Credentials = new NetworkCredential(_settings.SMTPUser, _settings.SMTPPassword),
                EnableSsl = true,
            };

            smtpClient.Send("no-reply@meot", receiver, subject, body);
        }
    }
}