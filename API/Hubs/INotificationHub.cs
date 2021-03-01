using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Hubs
{
    public interface INotificationHub
    {
        Task SendNotification(string user, NotificationType notificationType);
    }
}
