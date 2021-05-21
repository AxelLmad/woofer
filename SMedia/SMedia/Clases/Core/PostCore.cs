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

        public List<Post> GetLastPosts(long id)
        {
            try
            {
                int typeFeed = TypeFeed(id);
                bool anyUser = dbContext.User.Any(user => user.Id == id && user.Active);
                if (anyUser)
                {
                    switch (typeFeed)
                    {
                        case 2: // User follows both a page and user minimum
                            var UserIFollow2 = from FU in dbContext.FollowedUser
                                              where FU.FollowerId == id
                                              select FU;
                            var CommIFollow2 = from FC in dbContext.FollowedCommunity
                                              where FC.FollowerId == id
                                              select FC;
                            List<Post> LastPosts2 = (
                                from LP in dbContext.Post
                                join FC in CommIFollow2 on LP.CommunityId equals FC.CommunityId
                                where LP.Active
                                orderby LP.CreationDate
                                select LP
                                ).Take(10).ToList();
                            List<Post> UserPosts = (
                                from LP in dbContext.Post
                                join FU in UserIFollow2 on LP.AuthorId equals FU.FollowedId
                                where LP.Active
                                orderby LP.CreationDate
                                select LP).Take(10).ToList();
                            LastPosts2.AddRange(UserPosts);
                            return LastPosts2;
                        case 1: // User follows a User minimun
                            var UserIFollow1 = from FU in dbContext.FollowedUser
                                              where FU.FollowerId == id
                                              select FU;
                            List<Post> LastPosts1 = (
                                from LP in dbContext.Post
                                join FU in UserIFollow1 on LP.AuthorId equals FU.FollowedId
                                where LP.Active
                                orderby LP.CreationDate
                                select LP).Take(10).ToList();
                            return LastPosts1;
                        case 0: // User follows a community minimum
                            var CommIFollow0 = from FC in dbContext.FollowedCommunity
                                              where FC.FollowerId == id
                                              select FC;
                            List<Post> LastPosts0 = (
                                from LP in dbContext.Post
                                join FC in CommIFollow0 on LP.CommunityId equals FC.CommunityId
                                where LP.Active
                                orderby LP.CreationDate
                                select LP
                                ).Take(10).ToList();
                            return LastPosts0;
                        case -1:
                            return null;
                        default:
                            return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CreatePost(CreationPost post)
        {
            try
            {

                bool validPost = ValidatePost(post);
                if (validPost)
                {
                    Post newPost = new();
                    newPost.AuthorId = post.AuthorId;
                    newPost.CommunityId = post.CommunityId;
                    newPost.Content = post.Content;
                    newPost.CreationDate = DateTime.Now;
                    newPost.Active = true;
                    newPost.LastPostId = post.lastPostId;
                    dbContext.Add(newPost);
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

        public bool DisablePost(long id)
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
        public int TypeFeed(long id)
        {
            bool anyFollower = dbContext.FollowedUser.Any(follower => follower.FollowerId == id);
            bool anyCommunity = dbContext.FollowedCommunity.Any(comm => comm.FollowerId == id);
            if (anyCommunity && anyFollower)
                return 2;
            if (anyFollower)
                return 1;
            if (anyCommunity)
                return 0;
            return -1;
        }
        public bool ValidatePost(CreationPost post)
        {
            try
            {

                if (string.IsNullOrEmpty(post.Content) || post?.AuthorId == null || post?.CommunityId == null)
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
