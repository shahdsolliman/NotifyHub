using MediatR;
using NotifyHub.Domain.Enums;
using System;

namespace NotifyHub.Application.Commands
{
    public record CreateNotificationCommand(
        Guid UserId,
        Guid? RecipientId,
        string Recipient,
        string Subject,
        string Content,
        NotificationType Type) : IRequest<Guid>;
}
