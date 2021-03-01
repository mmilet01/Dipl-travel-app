using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class UserRelationshipStatus
    {
        public UserRelationshipStatus()
        {
            UsersRelationship = new HashSet<UsersRelationship>();
        }

        public int StatusId { get; set; }
        public string StatusText { get; set; }

        public virtual ICollection<UsersRelationship> UsersRelationship { get; set; }
    }
}
