using Microsoft.AspNetCore.SignalR;

namespace BlogPTC.Application.WebSockets.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<WebSocketHandler> _hubContext;

        public NotificationService(IHubContext<WebSocketHandler> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendNotificationAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync("PostNotification", message);
        }
    }
}
