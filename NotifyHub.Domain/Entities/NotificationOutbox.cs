using System;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Domain.Entities
{
    public class NotificationOutbox
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string? Recipient { get; set; } // email or phone based on provider
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int RetryCount { get; set; } = 0;
        public DateTime? LastAttemptAt { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
