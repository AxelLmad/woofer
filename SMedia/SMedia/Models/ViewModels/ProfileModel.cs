using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models.ViewModels
{
    public class ProfileModel
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Picture { get; set; }
    }
}
