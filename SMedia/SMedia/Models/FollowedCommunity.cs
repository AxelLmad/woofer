﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class FollowedCommunity
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int CommunityId { get; set; }
        public DateTime DateOfFollow { get; set; }

        public virtual User Follower { get; set; }
        public virtual Community Community { get; set; }
    }
}