using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using NotifyHub.Application.Common.Interfaces;
using NotifyHub.Application.Interfaces;
using NotifyHub.Domain.Entities;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Infrastructure.HostedServices
{
    public class NotificationDispatcherService : BackgroundService
    {
        private readonly ILogger<NotificationDispatcherService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _pollInterval = TimeSpan.FromSeconds(30);

        public NotificationDispatcherService(IServiceProvider serviceProvider, ILogger<NotificationDispatcherService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("NotificationDispatcherService started.");
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessPendingNotifications(stoppingToken);
                await Task.Delay(_pollInterval, stoppingToken);
            }
            _logger.LogInformation("NotificationDispatcherService stopping.");
        }

        private async Task ProcessPendingNotifications(CancellationToken token)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
            var senders = scope.ServiceProvider.GetServices<INotificationSender>();

            var pending = await dbContext.NotificationOutboxes
                .Where(o => o.Status == DeliveryStatus.Pending)
                .OrderBy(o => o.CreatedAt)
                .Take(20)
                .ToListAsync(token);

            foreach (var outbox in pending)
            {
                // Mark as processing
                outbox.Status = DeliveryStatus.Processing;
                outbox.LastAttemptAt = DateTime.UtcNow;
                await dbContext.SaveChangesAsync(token);

                var message = new NotificationMessage
                {
                    Recipient = outbox.Recipient,
                    Subject = outbox.Subject,
                    Content = outbox.Content,
                    Type = outbox.Type,
                    UserId = outbox.UserId
                };

                var sender = senders.FirstOrDefault(s =>
                {
                    var type = s.GetType().Name switch
                    {
                        string name when name.Contains("Email") => NotificationType.Email,
                        string name when name.Contains("Sms") => NotificationType.Sms,
                        string name when name.Contains("Fcm") => NotificationType.Push,
                        _ => NotificationType.InApp
                    };
                    return type == outbox.Type;
                });

                try
                {
                    if (sender == null)
                        throw new InvalidOperationException($"No sender registered for type {outbox.Type}");

                    await sender.SendAsync(message);
                    outbox.Status = DeliveryStatus.Sent;
                    outbox.LastAttemptAt = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    outbox.Status = DeliveryStatus.Failed;
                    outbox.ErrorMessage = ex.Message;
                    outbox.RetryCount++;
                    outbox.LastAttemptAt = DateTime.UtcNow;
                    _logger.LogError(ex, "Failed to dispatch notification {Id}", outbox.Id);
                }

                await dbContext.SaveChangesAsync(token);
            }
        }
    }
}
