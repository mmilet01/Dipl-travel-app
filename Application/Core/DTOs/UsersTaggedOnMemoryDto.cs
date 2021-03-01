using Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class UsersTaggedOnMemoryDto
    {
        public int TagId { get; set; }
        public int MemoryId { get; set; }
        public int UserId { get; set; }
        public virtual UserData User { get; set; }
    }
}
