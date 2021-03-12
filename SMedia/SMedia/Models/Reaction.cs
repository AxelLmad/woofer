﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public byte Type { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}
