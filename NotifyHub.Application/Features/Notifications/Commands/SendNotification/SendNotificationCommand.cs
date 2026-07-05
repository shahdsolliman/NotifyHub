using MediatR;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Application.Features.Notifications.Commands.SendNotification;

public class SendNotificationCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? RecipientContact { get; set; }
}
