using Persistance;
using System;
using System.Collections.Generic;

namespace Core.DTOs
{
    public class MemoryDto
    {
        public int MemoryId { get; set; }
        public string Title { get; set; }
        public string MemoryDescription { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; } 
        // user id?
        // public virtual ICollection<MemoryImageDto> MemoryImages { get; set; }
        // only a list of memory images paths?
        public virtual IEnumerable<UsersTaggedOnMemoryDto> UsersTaggedOnMemory { get; set; }
    }
}