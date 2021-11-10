using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Services
{
    public class BasicEmailService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public BasicEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage) //email just represents the email address
        {
            var newEmail = new MimeMessage(); //represents the whole message
            //MIME stands for Multipurpose Internet Mail Extensions
            
            //First needs to talk to appsettings.json by injecting an instance of iConfig (above)
            var emailAddress = _configuration["SmtpSettings:Email"];
            newEmail.Sender = MailboxAddress.Parse(emailAddress);
            newEmail.To.Add(MailboxAddress.Parse(email));
            newEmail.Subject = subject;

            var body = new BodyBuilder();
            body.HtmlBody = htmlMessage;
            newEmail.Body = body.ToMessageBody();

            //Configure the SMTP server to send the newEmail
            using SmtpClient smtpClient = new();
            var host = _configuration["SmtpSettings:Host"];
            var port = Convert.ToInt32(_configuration["SmtpSettings:Port"]);
            smtpClient.Connect(host, port, MailKit.Security.SecureSocketOptions.StartTls);
            smtpClient.Authenticate(emailAddress, _configuration["SmtpSettings:Password"]);
            await smtpClient.SendAsync(newEmail);

            smtpClient.Disconnect(true);
        }
    }
}
