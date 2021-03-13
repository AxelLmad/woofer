using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public int AuthorId { get; set; }
        public int CommunityId { get; set; }
        public int? LastPostId { get; set; }
        public bool Active { get; set; }
        
        public virtual User Author { get; set; }
        public virtual Community Community { get; set; }
        public virtual Post LastPost { get; set; }

        public virtual ICollection<FavoritePost> FavoritePost { get; set; }
        public virtual ICollection<PostPicture> PostPicture { get; set; }
        public virtual ICollection<Reaction> Reaction { get; set; }
        public virtual ICollection<Viewed> Viewed { get; set; }
    }
}
