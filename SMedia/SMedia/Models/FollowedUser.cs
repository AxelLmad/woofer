using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class FollowedUser
    {
        public long Id { get; set; }
        public long FollowerId { get; set; }
        public long FollowedId { get; set; }
        public DateTime DateOfFollow { get; set; }

        public virtual User Follower { get; set; }
        public virtual User Followed { get; set; }
    }
}
