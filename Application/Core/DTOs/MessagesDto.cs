using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.DTOs
{
    public class MessagesDto
    {
        public int FromId { get; set; }
        public int? ToId { get; set; }
        public int? ToGroupId { get; set; }
        public string MessageBody { get; set; }
        public DateTime SentAt { get; set; }
    }
}
