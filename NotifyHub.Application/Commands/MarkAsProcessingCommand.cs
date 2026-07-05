using MediatR;
using System;

namespace NotifyHub.Application.Commands
{
    public record MarkAsProcessingCommand(Guid NotificationOutboxId) : IRequest<Unit>;
}
