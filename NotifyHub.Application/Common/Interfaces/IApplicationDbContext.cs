using Microsoft.EntityFrameworkCore;
using NotifyHub.Domain.Entities;

namespace NotifyHub.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Notification> Notifications { get; }
    DbSet<NotificationOutbox> NotificationOutboxes { get; }
    // DbSet<Template> Templates { get; }
    // DbSet<User> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
