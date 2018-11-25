
using DotNetCore2018.Business.Services.Interfaces;
using DotNetCore2018.Business.Services.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DotNetCore2018.Business.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public async Task SendEmailAsync(string email, string loginUrl)
            => await SendEmailAsync(email, Subject(), Body(loginUrl));

        private Task SendEmailAsync(string email, string subject, string message)
            => Execute(Options.SendGridKey, subject, message, email);

        private Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("alexandr.navitskiy@gmail.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }

        private string Body(string loginUrl)
            => $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(loginUrl)}'>clicking here</a>.";

        private string Subject()
            => "Confirm your email";
    }
}