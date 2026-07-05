using Microsoft.AspNetCore.SignalR;

namespace NotifyHub.Infrastructure.RealTime;

public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        // e.g., Group users by UserId from claims
        await base.OnConnectedAsync();
    }

    // This hub can be used to push messages to connected clients
}
