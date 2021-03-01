using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class MemoryImages
    {
        public int MemoryImageId { get; set; }
        public string PhotoPath { get; set; }
        public int BelongsTo { get; set; }

        public virtual Memories BelongsToNavigation { get; set; }
    }
}
