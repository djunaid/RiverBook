using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;

namespace RiverBooks.EmailSending
{
    internal class MimeKitEmailSender : ISendEmail
    {
        private readonly ILogger<MimeKitEmailSender> _logger;

        public MimeKitEmailSender(ILogger<MimeKitEmailSender> logger)
        {
            _logger = logger;
        }
        public async Task SendEmail(string to, string from, string subject, string body)
        {
            _logger.LogInformation($"Attempting to send email {to} from {from}");

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect("localhost", 25, false);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(from, from));
                message.To.Add(new MailboxAddress(to, to));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                await smtpClient.SendAsync(message);
            }            
        }
    }
}
