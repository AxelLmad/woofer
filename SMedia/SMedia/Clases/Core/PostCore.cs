using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class PostCore
    {
        SMediaDbContext dbContext;
        public PostCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
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
            catch (Exception ex)
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
    }
}
