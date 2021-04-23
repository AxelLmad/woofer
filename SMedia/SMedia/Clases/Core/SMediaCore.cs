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
                   ).ToList();
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
                bool IdExists = dbContext.User.Any(user => user.Id == id);
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
                bool anyUser = dbContext.Post.Any(user => user.Id == id);
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
                                orderby LP.CreationDate
                                select LP
                                ).ToList();
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

        public List<Message> GetMessages(int id)
        {
            try
            {
                bool anyUser = dbContext.User.Any(user => user.Id == id);
                bool anyMessage = dbContext.Message.Any(message => message.ReceiverId == id);
                if (anyUser && anyMessage) {
                    List<Message> messages = (from M in dbContext.Message
                                             where (M.ReceiverId == id || M.SenderId == id)
                                             select M).ToList();
                    return messages;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void CreatePost(Post newpost)
        {
            try
            {

                bool validUser = ValidatePost(newpost);
                if (validUser)
                {
                    newpost.CreationDate = DateTime.Now;
                    newpost.Active = true;
                    dbContext.Add(newpost);
                    dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
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
