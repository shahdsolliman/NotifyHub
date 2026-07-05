using MediatR;
using NotifyHub.Application.Common.Interfaces;
using NotifyHub.Domain.Entities;

namespace NotifyHub.Application.Features.Notifications.Commands.SendNotification;

public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    
    // In a real scenario, we might also inject an IBackgroundJobClient to enqueue the actual sending,
    // or publish a domain event that is intercepted.

    public SendNotificationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var entity = new Notification(
            request.UserId,
            request.Type,
            request.Subject,
            request.Content,
            request.RecipientContact
        );

        _context.Notifications.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        // Example: The entity now has a generated ID. We can return it.
        return entity.Id;
    }
}
