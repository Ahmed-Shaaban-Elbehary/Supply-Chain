using Microsoft.AspNetCore.SignalR;

namespace SupplyChain.App.Notification
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
