using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace BlogPTC.Application.WebSockets
{
    public class WebSocketHandler : Hub
    {
        private static ConcurrentDictionary<string, string> ConnectedUsers = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            string username = Context.User.Identity.Name;

            ConnectedUsers.TryAdd(connectionId, username);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            ConnectedUsers.TryRemove(connectionId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        public static ConcurrentDictionary<string, string> GetConnectedUsers()
        {
            return ConnectedUsers;
        }
    }
}
