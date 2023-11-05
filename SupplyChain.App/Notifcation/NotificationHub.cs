using Microsoft.AspNetCore.SignalR;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;
using System.Collections.Concurrent;

namespace SupplyChain.App.Notification
{
    public class NotificationHub : Hub
    {
        // This dictionary associates user IDs with their connection IDs.
        private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new ConcurrentDictionary<string, string>();

        private readonly IUserSessionService _userSessionService;
        public NotificationHub(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }
        public override async Task OnConnectedAsync()
        {
            // Get the user ID after authentication (replace with your own logic)
            string userId = _userSessionService.CurrentUser.UserId.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                ConnectedUsers.TryAdd(userId, Context.ConnectionId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Remove the association when a user disconnects
            var userId = _userSessionService.CurrentUser.UserId.ToString();

            if (!string.IsNullOrEmpty(userId))
            {
                ConnectedUsers.TryRemove(userId, out _);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotification(string userId, string message)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                if (ConnectedUsers.TryGetValue(userId, out var connectionId))
                {
                    await Clients.Client(connectionId).SendAsync("ReceiveNotification", message);
                }
            }
            else
            {
                await Clients.All.SendAsync("ReceiveNotification", message);
            }
        }
    }
}
