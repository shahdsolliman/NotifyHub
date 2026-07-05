using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NotifyHub.Application.Interfaces;
using NotifyHub.Application.Options;

namespace NotifyHub.Infrastructure.NotificationSenders
{
    public class EmailNotificationSender : INotificationSender
    {
        private readonly EmailOptions _options;

        public EmailNotificationSender(IOptions<EmailOptions> options)
        {
            _options = options.Value;
        }

        public async Task SendAsync(NotificationMessage message)
        {
            var mail = new MailMessage(_options.FromAddress, message.Recipient ?? string.Empty)
            {
                Subject = message.Subject,
                Body = message.Content,
                IsBodyHtml = false
            };

            using var client = new SmtpClient(_options.Host, _options.Port)
            {
                EnableSsl = _options.EnableSsl,
                Credentials = new System.Net.NetworkCredential(_options.Username, _options.Password)
            };

            await client.SendMailAsync(mail);
        }
    }
}
