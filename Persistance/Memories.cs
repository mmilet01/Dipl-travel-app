using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class Memories
    {
        public Memories()
        {
            MemoryImages = new HashSet<MemoryImages>();
            UsersTaggedOnMemory = new HashSet<UsersTaggedOnMemory>();
        }

        public int MemoryId { get; set; }
        public string Title { get; set; }
        public string MemoryDescription { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual ICollection<MemoryImages> MemoryImages { get; set; }
        public virtual ICollection<UsersTaggedOnMemory> UsersTaggedOnMemory { get; set; }
    }
}
