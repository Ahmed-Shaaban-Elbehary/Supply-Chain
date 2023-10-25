using Microsoft.AspNetCore.SignalR;

namespace SupplyChain.App.Notification
{
    public class NotificationHub : Hub
    {
        // This method will be used for broadcasting to all users.
        public async Task SendNotificationToAll(object message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        // This method will be used for sending to a specific user.
        public async Task SendNotificationToUser(string userId, object message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
