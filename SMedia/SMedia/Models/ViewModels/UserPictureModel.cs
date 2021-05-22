using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models.ViewModels
{
    public class UserPictureModel
    {
        public long Id { get; set; }
        public string ServerPath { get; set; }
        public long UserId { get; set; }
        public bool Active { get; set; }
    }
}
