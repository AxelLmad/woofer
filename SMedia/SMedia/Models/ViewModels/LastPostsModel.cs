using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models.ViewModels
{
    public class LastPostsModel
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public long AuthorId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public long CommunityId { get; set; }
        public string CommunityName { get; set; }
        public string Color { get; set; }
        public long? LastPostId { get; set; }
        public bool Active { get; set; }
        public string? LastPostContent { get; set; }
        public string? LastPostAuthorName { get; set; }
        public DateTime? LastPostCreationDate { get; set; }
        public string? LastPostCommunityName { get; set; }
        public bool? LastPostActive { get; set; }
    }
    public class ResponsePostModel
    {
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public long CommunityId { get; set; }
        public string CommunityName { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
    }
}
