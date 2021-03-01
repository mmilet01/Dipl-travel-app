using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class NotificationType
    {
        public NotificationType()
        {
            Notifications = new HashSet<Notifications>();
        }

        public int NotificationTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Notifications> Notifications { get; set; }
    }
}
