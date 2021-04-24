using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class CreationFollowedUser
    {
        public CreationFollowedUser(long FollowerId, long FollowedId)
        {

            this.FollowerId = FollowerId;
            this.FollowedId = FollowedId;

        }
        public long FollowerId { get; set; }
        public long FollowedId { get; set; }

    }
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
