using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class UserImagesDto
    {
        public int UserImageId { get; set; }
        public string PhotoPath { get; set; }
        public int BelongsTo { get; set; }
    }
}
