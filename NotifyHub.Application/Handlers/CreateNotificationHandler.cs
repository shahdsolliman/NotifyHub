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
    public class CreateNotificationHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;

        public CreateNotificationHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var outbox = new NotificationOutbox
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Recipient = request.Recipient,
                Subject = request.Subject,
                Content = request.Content,
                Type = request.Type,
                Status = DeliveryStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.NotificationOutboxes.Add(outbox);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return outbox.Id;
        }
    }
}
