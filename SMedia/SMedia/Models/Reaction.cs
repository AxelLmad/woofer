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
    public class GetReaction
    {
        public byte Type { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
    }
    public class ReactionType
    {
        public int Type1 { get; set; }
        public int Type2 { get; set; }
        public int Type3 { get; set; }
        public long IdPost { get; set; }
    }
}
