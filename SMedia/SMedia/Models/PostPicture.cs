using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class PostPicture
    {
        public long Id { get; set; }
        public string ServerPath { get; set; }
        public long PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
