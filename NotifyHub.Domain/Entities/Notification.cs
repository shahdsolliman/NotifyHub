using NotifyHub.Domain.Common;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Domain.Entities;

public class Notification : AuditableEntity
{
    public Guid UserId { get; private set; }
    public NotificationType Type { get; private set; }
    public DeliveryStatus Status { get; private set; }
    public string Subject { get; private set; } = default!;
    public string Content { get; private set; } = default!;
    public string? RecipientContact { get; private set; } // Email address or phone number
    public string? ErrorMessage { get; private set; }
    public DateTime? SentAt { get; private set; }
    
    // For EF Core
    private Notification() { }
    
    public Notification(Guid userId, NotificationType type, string subject, string content, string? recipientContact = null)
    {
        UserId = userId;
        Type = type;
        Subject = subject;
        Content = content;
        RecipientContact = recipientContact;
        Status = DeliveryStatus.Pending;
    }
    
    public void MarkAsProcessing()
    {
        if (Status != DeliveryStatus.Pending)
        {
            throw new InvalidOperationException("Only pending notifications can be processed.");
        }
        Status = DeliveryStatus.Processing;
    }
    
    public void MarkAsSent(DateTime sentAt)
    {
        Status = DeliveryStatus.Sent;
        SentAt = sentAt;
    }
    
    public void MarkAsFailed(string error)
    {
        Status = DeliveryStatus.Failed;
        ErrorMessage = error;
    }
}
