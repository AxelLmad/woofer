using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class Reaction
    {
        public long Id { get; set; }
        public byte Type { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
