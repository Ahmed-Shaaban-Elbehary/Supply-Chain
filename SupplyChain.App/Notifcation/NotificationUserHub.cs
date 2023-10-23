using Microsoft.AspNetCore.SignalR;

namespace SupplyChain.App.Notifcation
{
    public class NotificationUserHub : Hub
    {
        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public async Task SendNotificationToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveNotification", message);
        }
    }
}
