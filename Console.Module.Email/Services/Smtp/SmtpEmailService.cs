using Console.Module.Email.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Console.Module.Email.Services.Smtp
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpConfig _smtpConfig;

        public SmtpEmailService(EmailConfig emailConfig)
        {
            _smtpConfig = emailConfig.Smtp;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            if (to == null || to.Trim().Length == 0)
                throw new ArgumentNullException(nameof(to));

            var message = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_smtpConfig.Email)
            };

            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var builder = new BodyBuilder()
            {
                HtmlBody = body
            };
            message.Body = builder.ToMessageBody();
            using var client = new SmtpClient();

            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(_smtpConfig.Host, _smtpConfig.Port, SecureSocketOptions.SslOnConnect);

            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync(_smtpConfig.Email, _smtpConfig.Password);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}
