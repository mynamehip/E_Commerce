using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EC.Utility.Email
{
    public class EmailSender : IEmailSender
    {
        private IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email address is required", nameof(email));
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config["Email:SmtpUser"]),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            using (var smtpClient = new SmtpClient(_config["Email:SmtpServer"], int.Parse(_config["Email:SmtpPort"])))
            {
                smtpClient.Credentials = new NetworkCredential(_config["Email:SmtpUser"], _config["Email:SmtpPass"]);
                smtpClient.EnableSsl = true;

                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
