using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NotifyHub.Application.Common.Interfaces;
using NotifyHub.Domain.Entities;

namespace NotifyHub.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<NotificationOutbox> NotificationOutboxes => Set<NotificationOutbox>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<NotifyHub.Domain.Common.DomainEvent>();
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Here we could add logic to intercept and set AuditableEntity properties (CreatedAt, CreatedBy, etc.)
        // or dispatch domain events using MediatR.
        return await base.SaveChangesAsync(cancellationToken);
    }
}
