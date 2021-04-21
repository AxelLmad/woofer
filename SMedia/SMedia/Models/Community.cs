using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class Community
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public DateTime CreationDate { get; set; }
        public long OwnerId { get; set; }
        public bool Active { get; set; }

        public virtual User Owner { get; set; }
        public virtual ICollection<FollowedCommunity> Follower { get; set; }
        public virtual ICollection<Post> Post { get; set; }
    }
}
