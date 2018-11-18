using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Services.Models;
using Microsoft.Extensions.Configuration;

namespace DotNetCore2018.Business.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderConfiguration _config;

        public EmailSender(IConfiguration configuration)
        {
            _config = configuration.GetSection("Mail").Get<EmailSenderConfiguration>();
        }

        public Task SendEmailAsync(string email, string subject, string body)
        {
            var credentials = new NetworkCredential(_config.UserName, _config.Password);
            using (var client = new SmtpClient(_config.Host, _config.Port) { Credentials = credentials, EnableSsl = _config.EnableSSL })
            {
                var message = new MailMessage(_config.UserName, email, subject, body)
                {
                    IsBodyHtml = true
                };

                return client.SendMailAsync(message);
            }
        }
    }
}