using SMedia.Models;
using SMedia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class SMediaCore
    {
        SMediaDbContext dbContext;
        public SMediaCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Post> GetAll()
        {
            try
            {
                List<Post> Posts = (
                   from s in dbContext.Post
                   where s.Active == true
                   select s
                   ).Take(10).ToList();
                return Posts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProfileModel GetProfileModel(int id)
        {
            try
            {
                bool IdExists = dbContext.User.Any(user => user.Id == id && user.Active);
                if (IdExists) {
                    var cosulta = (from u in dbContext.User where u.Id == id
                                   select new { 
                                   Nickname = u.NickName,
                                   Name = u.Name,
                                   LastName = u.LastName,
                                   Picture = u.Picture
                                   }).ToList();
                    ProfileModel profile = cosulta.Select(x => new ProfileModel
                    {
                        Nickname = x.Nickname,
                        Name = x.Name,
                        LastName = x.LastName,
                        Picture = x.Picture
                    }).First();
                    return profile;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void SignIn(User user)
        {
            try
            {
                bool validUser = ValidateUser(user);
                if (validUser)
                {
                    user.RegisterDate = DateTime.Now;
                    user.LastLogin = DateTime.Now;
                    user.Active = true;
                    dbContext.Add(user);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long Login(string NickName, string Password)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(user => user.NickName == NickName && user.Password == Password && user.Active);
                if (AnyUser)
                {
                    User user = (
                        from U in dbContext.User
                        where (U.NickName == NickName && U.Password == Password)
                        select U        
                        ).First();
                    return user.Id;
                }
                return -1;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidateUser(User user)
        {
            try
            {
                
                if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.NickName) ||
                    string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Post> GetLastPosts(int id)
        {
            try
            {
                bool anyUser = dbContext.Post.Any(user => user.Id == id && user.Active);
                if (anyUser)
                {
                    bool anyFollower = dbContext.FollowedUser.Any(follower => follower.FollowerId == id);
                    if (anyFollower)
                    {
                        var UserIFollow = from FU in dbContext.FollowedUser
                                          where FU.FollowerId == id
                                          select FU;
                        bool anyCommunity = dbContext.FollowedCommunity.Any(comm => comm.FollowerId == id);
                        if (anyCommunity)
                        {
                            var CommIFollow = from FC in dbContext.FollowedCommunity
                                              where FC.FollowerId == id
                                              select FC;
                            List<Post> LastPosts = (
                                from LP in dbContext.Post
                                join FU in UserIFollow on LP.AuthorId equals FU.FollowedId
                                join FC in CommIFollow on LP.CommunityId equals FC.CommunityId
                                where LP.Active
                                orderby LP.CreationDate
                                select LP
                                ).Take(10).ToList();
                            return LastPosts;
                        }
                        else { return null; }
                    }
                    else { return null; }
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Post> GetFavoritePosts(int id)
        {
            try
            {
                bool anyUser = dbContext.Post.Any(user => user.Id == id);
                if (anyUser)
                {
                    bool anyFavorite = dbContext.FavoritePost.Any(fav => fav.UserId == id);
                    if (anyFavorite)
                    {
                        var FavPost = from FP in dbContext.FavoritePost
                                          where FP.UserId == id
                                          select FP;
                            List<Post> FavoritePosts = (
                                from LP in dbContext.Post
                                join FU in FavPost on LP.Id equals FU.PostId
                                orderby LP.CreationDate
                                select LP
                                ).Take(10).ToList();
                            return FavoritePosts;
                    }
                    else { return null; }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Message> GetMessages(int id)
        {
            try
            {
                bool anyUser = dbContext.User.Any(user => user.Id == id);
                bool anyMessage = dbContext.Message.Any(message => message.ReceiverId == id);
                if (anyUser && anyMessage) {
                    List<Message> messages = (from M in dbContext.Message
                                             where (M.ReceiverId == id || M.SenderId == id)
                                             select M).Take(20).ToList();
                    return messages;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public int GetPostViewes(int id)
        {
            try
            {
                bool AnyPost = dbContext.Post.Any(post => post.Id == id);
                if (AnyPost)
                {
                    IEnumerable<Viewed> viewsPost = ( from V in dbContext.Viewed
                                                    where V.PostId == id
                                                    select V);
                    int viewes = viewsPost.Count();
                    return viewes;
                }
                return -1;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool SetViewOnPost(int idUser, int idPost)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(user => user.Id == idUser);
                bool AnyPost = dbContext.Post.Any(post => post.Id == idPost);
                if(AnyUser && AnyUser)
                {
                    bool anyViewed = dbContext.Viewed.Any(viewed => viewed.UserId == idUser
                                                            && viewed.PostId == idPost);
                    if (!anyViewed)
                    {
                        Viewed viewed = new Viewed
                        {
                            UserId = idUser,
                            PostId = idPost
                        };
                        dbContext.Add(viewed);
                        dbContext.SaveChanges();
                        return true;
                    }
                    return false;
                }

                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool CreatePost(Post newpost)
        {
            try
            {

                bool validPost = ValidatePost(newpost);
                if (validPost)
                {
                    newpost.CreationDate = DateTime.Now;
                    newpost.Active = true;
                    dbContext.Add(newpost);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveFavoritePost(int idUser, int idPost)
        {
            try
            {
                bool validUser = dbContext.User.Any(user => user.Id == idUser);
                bool validPost = dbContext.Post.Any(post => post.Id == idPost);
                if(validUser && validPost)
                {
                    bool anyPost = dbContext.FavoritePost.Any(favPost => favPost.UserId == idUser && favPost.PostId == idPost);
                    if (!anyPost)
                    {
                        FavoritePost favPost = new FavoritePost
                        {
                            PostId = idPost,
                            UserId = idUser
                        };
                        dbContext.Add(favPost);
                        dbContext.SaveChanges();
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetFollowedUsers(int id)
        {
            try
            {
                bool anyFolloweUser = dbContext.FollowedUser.Any(follUser => follUser.FollowerId == id);
                if (anyFolloweUser) {
                    var UserIFollow = (from FU in dbContext.FollowedUser
                                       where FU.FollowerId == id
                                       select FU);
                    List<User> FollowedUsers = (from U in dbContext.User
                                                join FU in UserIFollow on U.Id equals FU.FollowedId
                                                select U).ToList();
                    return FollowedUsers;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Community> GetFollowedCommunities(int id)
        {
            try
            {
                bool anyFollowedCommunity = dbContext.FollowedCommunity.Any(follComm => follComm.FollowerId == id);
                if (anyFollowedCommunity)
                {
                    var communityIFollow = (from FU in dbContext.FollowedCommunity
                                            where FU.FollowerId == id
                                            select FU);
                    List<Community> FollowedCommunities = (from U in dbContext.Community
                                                join FU in communityIFollow on U.Id equals FU.CommunityId
                                                select U).ToList();
                    return FollowedCommunities;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool FollowUser(int idFollower, int idToFollow)
        {
            try
            {
                bool anyFollowedUser = dbContext.FollowedUser.Any(follUser => follUser.FollowerId == idFollower 
                                                                    && follUser.FollowedId == idToFollow);
                if (!anyFollowedUser && idFollower != idToFollow)
                {
                    FollowedUser follUser = new FollowedUser
                    {
                        FollowerId = idFollower,
                        FollowedId = idToFollow
                    };
                    follUser.DateOfFollow = DateTime.Now;
                    dbContext.Add(follUser);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool FollowCommunity(int idFollower, int idToFollow)
        {
            try
            {
                bool anyFollow = dbContext.FollowedCommunity.Any(follow => follow.FollowerId == idFollower
                                                                    && follow.CommunityId == idToFollow);
                bool AnyUser = dbContext.User.Any(user => user.Id == idFollower);
                bool AnyCommunity = dbContext.Community.Any(community => community.Id == idToFollow);
                if (!anyFollow && AnyUser && AnyCommunity)
                {
                    FollowedCommunity Follow = new FollowedCommunity
                    {
                        FollowerId = idFollower,
                        CommunityId = idToFollow
                    };
                    Follow.DateOfFollow = DateTime.Now;
                    dbContext.Add(Follow);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetReactPost(int idPost, int idUser, byte typeReaction)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(user => user.Id == idUser);
                bool AnyPost = dbContext.Post.Any(post => post.Id == idPost);
                if (AnyUser && AnyPost)
                {
                    Reaction AnyReaction = dbContext.Reaction.FirstOrDefault(reaction => reaction.PostId == idPost
                                            && reaction.UserId == idUser);
                    if (AnyReaction == null)
                    {
                        Reaction newReaction = new Reaction
                        {
                            UserId = idUser,
                            PostId = idPost,
                            Type = typeReaction
                        };
                        dbContext.Add(newReaction);
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        AnyReaction.Type = typeReaction;
                        dbContext.Update(AnyReaction);
                        dbContext.SaveChanges();
                        return true;
                    }
                }

                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void SendMessage(Message message)
        {
            try
            {
                bool validUser = ValidateMessage(message);
                if (validUser)
                {
                    message.Date = DateTime.Now;
                    dbContext.Add(message);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateCommunity(Community community)
        {
            try
            {
                bool validCommunity = ValidateCommunity(community);
                if (validCommunity)
                {
                    community.CreationDate = DateTime.Now;
                    community.Active = true;
                    dbContext.Add(community);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DisableUser(int id)
        {
            try
            {
                User AnyUser = dbContext.User.FirstOrDefault(user => user.Id == id && user.Active);
                if (AnyUser != null)
                {
                    AnyUser.Active = false;
                    dbContext.Update(AnyUser);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DisablePost(int id)
        {
            try
            {
                Post AnyPost = dbContext.Post.FirstOrDefault(post => post.Id == id && post.Active);
                if (AnyPost != null)
                {
                    AnyPost.Active = false;
                    dbContext.Update(AnyPost);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DisableCommunity(int id)
        {
            try
            {
                Community AnyCommunity = dbContext.Community.FirstOrDefault(comm => comm.Id == id && comm.Active);
                if (AnyCommunity != null)
                {
                    AnyCommunity.Active = false;
                    dbContext.Update(AnyCommunity);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UnfollowUser(int idFollower, int idFollowed)
        {
            try
            {
                FollowedUser follow = dbContext.FollowedUser.FirstOrDefault(
                    follow => follow.FollowerId == idFollower && follow.FollowedId == idFollowed);
                if(follow != null)
                {
                    dbContext.Remove(follow);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UnfollowCommunity(int idFollower, int idCommunity)
        {
            try
            {
                FollowedCommunity follow = dbContext.FollowedCommunity.FirstOrDefault(
                    follow => follow.FollowerId == idFollower && follow.CommunityId == idCommunity);
                if (follow != null)
                {
                    dbContext.Remove(follow);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidatePost(Post post)
        {
            try
            {

                if (string.IsNullOrEmpty(post.Content) || post?.AuthorId != null || post?.CommunityId != null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidateCommunity(Community community)
        {
            try
            {
                if(string.IsNullOrEmpty(community.Name) || string.IsNullOrEmpty(community.Color)
                    || string.IsNullOrEmpty(community.Description) || community?.OwnerId == null || community.OwnerId < 1)
                {
                    return false;
                }
                else
                {
                    bool AnyUser = dbContext.User.Any(user => user.Id == community.OwnerId);
                    if (!AnyUser)
                        return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidateMessage(Message message)
        {
            try
            {

                if (string.IsNullOrEmpty(message.Content) || message?.Sender != null || message?.Receiver != null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
