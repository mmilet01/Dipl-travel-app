using System;
using System.Collections.Generic;

namespace Persistance
{
    public partial class Messages
    {
        public int MessageId { get; set; }
        public int FromId { get; set; }
        public int? ToId { get; set; }
        public int? ToGroupId { get; set; }
        public string MessageBody { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? ReadAt { get; set; }

        public virtual Users From { get; set; }
        public virtual Users To { get; set; }
        public virtual Groups ToGroup { get; set; }
    }
}
