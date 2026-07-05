using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotifyHub.Application.Interfaces;
using NotifyHub.Domain.Enums;
using NotifyHub.Domain.Entities;
using NotifyHub.Application.Options;

namespace NotifyHub.Infrastructure.NotificationSenders
{
    public class FcmNotificationSender : INotificationSender
    {
        private readonly HttpClient _httpClient;
        private readonly FcmOptions _options;
        private readonly ILogger<FcmNotificationSender> _logger;

        public FcmNotificationSender(HttpClient httpClient, IOptions<FcmOptions> options, ILogger<FcmNotificationSender> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;
        }

        public async Task SendAsync(NotificationMessage message)
        {
            if (message.Type != NotificationType.Push)
            {
                _logger.LogWarning("Attempted to send non-push notification via FCM sender.");
                return;
            }

            var payload = new
            {
                to = message.Recipient,
                notification = new
                {
                    title = message.Subject,
                    body = message.Content
                }
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send")
            {
                Content = JsonContent.Create(payload)
            };
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", _options.ServerKey);

            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                _logger.LogError("FCM send failed: {Status} {Error}", response.StatusCode, error);
                throw new InvalidOperationException($"FCM send failed: {error}");
            }
            _logger.LogInformation("FCM notification sent to {Recipient}", message.Recipient);
        }
    }
}
