using System;
using MediatR;
using NotifyHub.Application.Commands;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Api.DTOs
{
    public class CreateNotificationRequest
    {
        public Guid UserId { get; set; }
        public Guid? RecipientId { get; set; }
        public string Recipient { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
    }
}
