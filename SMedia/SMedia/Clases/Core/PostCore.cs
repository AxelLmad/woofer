using SMedia.Models;
using SMedia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMedia.Clases.Core
{
    public class PostCore
    {
        SMediaDbContext dbContext;
        public PostCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public LastPostsModel ById(long id)
        {
            try
            {
                bool AnyCommunity = dbContext.Post.Any(x=> x.Active && x.Id == id);
                if (AnyCommunity != null)
                {
                    LastPostsModel Comm = (from P in dbContext.Post
                                           join U in dbContext.User on P.AuthorId equals U.Id
                                           join C in dbContext.Community on P.CommunityId equals C.Id
                                           where (P.Active && P.Id == id)
                                           select new LastPostsModel()
                                           {
                                               Id = P.Id,
                                               Content = P.Content,
                                               CreationDate = P.CreationDate,
                                               AuthorId = P.AuthorId,
                                               Name = U.Name,
                                               LastName = U.LastName,
                                               NickName = U.NickName,
                                               CommunityId = P.CommunityId,
                                               CommunityName = C.Name,
                                               Color = C.Color,
                                               LastPostId = P.LastPostId,
                                               Active = P.Active
                                           }).FirstOrDefault();
                    return Comm;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<LastPostsModel> GetLastPosts(long id)
        {
            try
            {
                int typeFeed = TypeFeed(id);
                bool anyUser = dbContext.Post.Any(user => user.Id == id && user.Active);
                if (typeFeed != -1)
                {
                    var UserIFollow2 = from FU in dbContext.FollowedUser
                                       where FU.FollowerId == id
                                       select FU;
                    var CommIFollow2 = from FC in dbContext.FollowedCommunity
                                       where FC.FollowerId == id
                                       select FC;
                    List<LastPostsModel> LastPosts2 = (
                        from LP in dbContext.Post
                        join FC in CommIFollow2 on LP.CommunityId equals FC.CommunityId
                        join C in dbContext.Community on FC.CommunityId equals C.Id
                        join U in dbContext.User on LP.AuthorId equals U.Id
                        where LP.Active
                        
                        orderby LP.CreationDate
                        
                        select new LastPostsModel()
                        {
                            Id = LP.Id,
                            Content = LP.Content,
                            CreationDate = LP.CreationDate,
                            AuthorId = LP.AuthorId,
                            Name = U.Name,
                            LastName = U.LastName,
                            NickName = U.NickName,
                            CommunityId = LP.CommunityId,
                            CommunityName = C.Name,
                            Color = C.Color,
                            LastPostId = LP.LastPostId,
                            Active = LP.Active
                        }
                        ).Take(20).ToList();
                    List<LastPostsModel> UserPosts = (
                        from LP in dbContext.Post
                        join FU in UserIFollow2 on LP.AuthorId equals FU.FollowedId
                        join U in dbContext.User on FU.FollowedId equals U.Id
                        join C in dbContext.Community on LP.CommunityId equals C.Id
                        where LP.Active
                        orderby LP.CreationDate
                        select new LastPostsModel()
                        {
                            Id = LP.Id,
                            Content = LP.Content,
                            CreationDate = LP.CreationDate,
                            AuthorId = LP.AuthorId,
                            Name = U.Name,
                            LastName = U.LastName,
                            NickName = U.NickName,
                            CommunityId = LP.CommunityId,
                            CommunityName = C.Name,
                            Color = C.Color,
                            LastPostId = LP.LastPostId,
                            Active = LP.Active
                        }).Take(10).ToList();
                    List<LastPostsModel> lp = LastPosts2.Union(UserPosts).ToList();

                    List<long> insertedIds = new List<long>();

                    List<LastPostsModel> finalList = new List<LastPostsModel>();

                    foreach (LastPostsModel post in lp)
                    {

                        bool isInserted = false;

                        foreach (long insertedId in insertedIds)
                        {

                            

                            if(post.Id == insertedId)
                            {

                                isInserted = true;
                                break;

                            }

                        }

                        if (!isInserted)
                        {
                            insertedIds.Add(post.Id);
                            finalList.Add(post);

                        }

                    }

                    return finalList;
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
                    newPost = dbContext.Post.OrderBy(x=>x.Id).Last();
                    PostPicture picture = new();
                    picture.ServerPath = post.ServerPathImg;
                    picture.PostId = newPost.Id;
                    dbContext.Add(picture);
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
