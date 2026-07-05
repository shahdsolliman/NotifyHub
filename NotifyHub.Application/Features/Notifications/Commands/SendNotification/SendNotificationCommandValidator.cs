using FluentValidation;

namespace NotifyHub.Application.Features.Notifications.Commands.SendNotification;

public class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(v => v.Type)
            .IsInEnum().WithMessage("A valid NotificationType is required.");

        RuleFor(v => v.Subject)
            .MaximumLength(200).WithMessage("Subject must not exceed 200 characters.")
            .NotEmpty().WithMessage("Subject is required.");

        RuleFor(v => v.Content)
            .NotEmpty().WithMessage("Content is required.");
            
        RuleFor(v => v.RecipientContact)
            .NotEmpty().When(v => v.Type == Domain.Enums.NotificationType.Email || v.Type == Domain.Enums.NotificationType.Sms)
            .WithMessage("Recipient contact (Email or Phone) is required for Email and SMS notifications.");
    }
}
