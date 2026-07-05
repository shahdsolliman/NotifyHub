using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotifyHub.Application.Interfaces;
using NotifyHub.Domain.Enums;
using NotifyHub.Application.Options;

namespace NotifyHub.Infrastructure.NotificationSenders
{
    public class SmsNotificationSender : INotificationSender
    {
        private readonly SmsOptions _options;
        private readonly ILogger<SmsNotificationSender> _logger;
        // Placeholder for actual SMS client; in production replace with Twilio or other provider.
        public SmsNotificationSender(IOptions<SmsOptions> options, ILogger<SmsNotificationSender> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public Task SendAsync(NotificationMessage message)
        {
            if (message.Type != NotificationType.Sms)
            {
                _logger.LogWarning("Attempted to send non‑SMS notification via SmsNotificationSender.");
                return Task.CompletedTask;
            }

            // Simulate sending SMS – in real implementation call provider SDK.
            _logger.LogInformation("Sending SMS to {Recipient}: {Content}", message.Recipient, message.Content);
            return Task.CompletedTask;
        }
    }
}
