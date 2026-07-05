using System;
using System.Threading.Tasks;
using NotifyHub.Domain.Enums;

namespace NotifyHub.Application.Interfaces
{
    public interface INotificationSender
    {
        Task SendAsync(NotificationMessage message);
    }

    // NotificationMessage moved to separate file


}
