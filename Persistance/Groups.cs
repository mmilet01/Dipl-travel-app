using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class Groups
    {
        public Groups()
        {
            Messages = new HashSet<Messages>();
            Notifications = new HashSet<Notifications>();
            UsersInAgroup = new HashSet<UsersInAgroup>();
        }

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<Messages> Messages { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<UsersInAgroup> UsersInAgroup { get; set; }
    }
}
