using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using NotifyHub.Application.Common.Interfaces;
using NotifyHub.Domain.Entities;
using NotifyHub.Application.Commands;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Application.Handlers
{
    public class MarkAsProcessingHandler : IRequestHandler<MarkAsProcessingCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public MarkAsProcessingHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(MarkAsProcessingCommand request, CancellationToken cancellationToken)
        {
            var outbox = await _dbContext.NotificationOutboxes.FindAsync(new object[] { request.NotificationOutboxId }, cancellationToken);
            if (outbox == null)
                throw new InvalidOperationException($"Outbox entry {request.NotificationOutboxId} not found.");

            outbox.Status = DeliveryStatus.Processing;
            outbox.LastAttemptAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
