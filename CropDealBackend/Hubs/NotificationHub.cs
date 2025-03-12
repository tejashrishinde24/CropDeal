using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class NotificationHub : Hub
{
    public async Task SendNotification(string dealerId, string message)
    {
        await Clients.User(dealerId).SendAsync("ReceiveNotification", message);
    }
}
