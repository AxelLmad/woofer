using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class Viewed
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
    public class setView
    {
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
}
