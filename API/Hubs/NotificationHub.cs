using Core.Enums;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Hubs
{
    public class NotificationHub : Hub, INotificationHub
    {
        protected IHubContext<NotificationHub> _context;

        public NotificationHub(IHubContext<NotificationHub> context)
        {
            this._context = context;

        }

        public async Task SendNotification(string user, NotificationType notificationType) // Enum? NotificationType
        {

            //switch notification type -> ?
            await _context.Clients.Group(user).SendAsync("NotificationRecieved", notificationType);
        }

        public override Task OnConnectedAsync()
        {
            // lock (_connections)
            //  {
            //      _connections.Add("asd", Context.ConnectionId);
            // 
            var groupName = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            return base.OnConnectedAsync();
        }
    }
}
