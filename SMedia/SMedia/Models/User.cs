using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{
    public class User
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Picture { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<FavoritePost> FavoritePost { get; set; }
        public virtual ICollection<FollowedCommunity> FollowedCommunity { get; set; }
        public virtual ICollection<UsuarioSeguido> Follower { get; set; }
        public virtual ICollection<UsuarioSeguido> Followed { get; set; }
        public virtual ICollection<Message> Sender { get; set; }
        public virtual ICollection<Message> Receiver { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Reaction> Reaction { get; set; }
        public virtual ICollection<Viewed> Viewed { get; set; }
    }
}
