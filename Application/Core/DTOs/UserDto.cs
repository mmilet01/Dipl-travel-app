using Persistance;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual ICollection<MemoryDto> Memories { get; set; }

        //public virtual ICollection<UserImagesDto> UsersImages { get; set; }
        //public virtual ICollection<UsersRelationship> UsersRelationshipFirstUser { get; set; }
        //public virtual ICollection<UsersRelationship> UsersRelationshipSecondUser { get; set; }
    }
}
