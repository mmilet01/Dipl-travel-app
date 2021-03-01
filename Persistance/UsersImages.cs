using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class UsersImages
    {
        public int UserImageId { get; set; }
        public string PhotoPath { get; set; }
        public int BelongsTo { get; set; }

        public virtual Users BelongsToNavigation { get; set; }
    }
}
