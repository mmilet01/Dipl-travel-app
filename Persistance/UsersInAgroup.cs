using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class UsersInAgroup
    {
        public int UsersGroupId { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Users User { get; set; }
    }
}
