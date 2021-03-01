using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class Users
    {
        public Users()
        {
            Memories = new HashSet<Memories>();
            MessagesFrom = new HashSet<Messages>();
            MessagesTo = new HashSet<Messages>();
            NotificationsFrom = new HashSet<Notifications>();
            NotificationsTo = new HashSet<Notifications>();
            UsersImages = new HashSet<UsersImages>();
            UsersInAgroup = new HashSet<UsersInAgroup>();
            UsersRelationshipFirstUser = new HashSet<UsersRelationship>();
            UsersRelationshipSecondUser = new HashSet<UsersRelationship>();
            UsersTaggedOnMemory = new HashSet<UsersTaggedOnMemory>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordField { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastActivityTimeStamp { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Memories> Memories { get; set; }
        public virtual ICollection<Messages> MessagesFrom { get; set; }
        public virtual ICollection<Messages> MessagesTo { get; set; }
        public virtual ICollection<Notifications> NotificationsFrom { get; set; }
        public virtual ICollection<Notifications> NotificationsTo { get; set; }
        public virtual ICollection<UsersImages> UsersImages { get; set; }
        public virtual ICollection<UsersInAgroup> UsersInAgroup { get; set; }
        public virtual ICollection<UsersRelationship> UsersRelationshipFirstUser { get; set; }
        public virtual ICollection<UsersRelationship> UsersRelationshipSecondUser { get; set; }
        public virtual ICollection<UsersTaggedOnMemory> UsersTaggedOnMemory { get; set; }
    }
}
