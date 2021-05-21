using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Models
{

    /**
     * Refers to a User data encapsulation needed to sign up
     */
    public class SignUpUser
    {
        public long? Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string ServerPath { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

    }

    /**
     * Refers to a User data encapsulation needed to login
     */
    public class LoginUser
    {
        public string NickName { get; set; }
        
        public string Password { get; set; }



    }
    /**
     * Refers to a User data encapsulation without password
     */
    public class FixedUser
    {

        public FixedUser(long Id, string NickName, string Email, string Name, string LastName, string Picture, DateTime RegisterDate, DateTime LastLogin)
        {

            this.Id = Id;
            this.NickName = NickName;
            this.Email = Email;
            this.Name = Name;
            this.LastName = LastName;
            this.Picture = Picture;
            this.RegisterDate = RegisterDate;
            this.LastLogin = LastLogin;

        }

        public long Id { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? Picture { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLogin { get; set; }

    }
    public class User
    {
        public long Id { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Community> MyCommunity { get; set; }
        public virtual ICollection<FavoritePost> FavoritePost { get; set; }
        public virtual ICollection<FollowedCommunity> FollowedCommunity { get; set; }
        public virtual ICollection<FollowedUser> Follower { get; set; }
        public virtual ICollection<FollowedUser> Followed { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<UserPicture> UserPicture { get; set; }
        public virtual ICollection<Reaction> Reaction { get; set; }
        public virtual ICollection<Viewed> Viewed { get; set; }
    }
}
