using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class Notifications
    {
        public int NotificationId { get; set; }
        public int FromId { get; set; }
        public int? ToId { get; set; }
        public int? ToGroupId { get; set; }
        public int NotificationTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRecieved { get; set; }

        public virtual Users From { get; set; }
        public virtual NotificationType NotificationType { get; set; }
        public virtual Users To { get; set; }
        public virtual Groups ToGroup { get; set; }
    }
}
