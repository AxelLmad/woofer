using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class CreationPost
    {
        public string Content { get; set; }
        public long AuthorId { get; set; }
        public long CommunityId { get; set; }
        public long? lastPostId { get; set; }
    }
    public class Post
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public long AuthorId { get; set; }
        public long CommunityId { get; set; }
        public long? LastPostId { get; set; }
        public bool Active { get; set; }
        
        public virtual User Author { get; set; }
        public virtual Community Community { get; set; }
        public virtual Post LastPost { get; set; }
        public virtual Post BeforePost { get; set; }


        public virtual ICollection<FavoritePost> FavoritePost { get; set; }
        public virtual ICollection<PostPicture> PostPicture { get; set; }
        public virtual ICollection<Reaction> Reaction { get; set; }
        public virtual ICollection<Viewed> Viewed { get; set; }
    }
}
