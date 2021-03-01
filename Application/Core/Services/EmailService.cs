using Core.Interfaces;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public bool Send(string from, string to, string subject, string html)
        {
            // introduce some validation maybe?
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // sending email
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetValue<string>("MailSettings:Host"), _config.GetValue<int>("MailSettings:Port"), SecureSocketOptions.StartTls); ;
            smtp.Authenticate(_config.GetValue<string>("MailSettings:Mail"), _config.GetValue<string>("MailSettings:Password"));
            smtp.Send(email);
            smtp.Disconnect(true);

            return true;
        }
    }
}
