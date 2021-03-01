using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class UsersTaggedOnMemory
    {
        public int TagId { get; set; }
        public int MemoryId { get; set; }
        public int UserId { get; set; }

        public virtual Memories Memory { get; set; }
        public virtual Users User { get; set; }
    }
}
