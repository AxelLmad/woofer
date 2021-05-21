using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class UserPicture
    {
        public long Id { get; set; }
        public string ServerPath { get; set; }
        public long UserId { get; set; }
        public bool Active { get; set; }

        public virtual User User { get; set; }
    }
}
