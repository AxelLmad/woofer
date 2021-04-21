using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class PostPicture
    {
        public int Id { get; set; }
        public string ServerPath { get; set; }
        public int PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
