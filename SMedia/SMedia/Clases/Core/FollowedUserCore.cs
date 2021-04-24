using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class FollowedUserCore
    {
        SMediaDbContext dbContext;
        public FollowedUserCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<User> GetFollowedUsers(long followerId)
        {
            try
            {
                List<User> followedUsers = (
                   from s in dbContext.FollowedUser
                   where s.FollowerId == followerId
                   select s.Followed
                   ).ToList();
                return followedUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<User> GetFollowers(long followedId)
        {
            try
            {
                List<User> followers = (
                   from s in dbContext.FollowedUser
                   where s.FollowedId == followedId
                   select s.Follower
                   ).ToList();
                return followers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public FollowedUser Create(CreationFollowedUser cFollowedUser)
        {

            try
            {
                FollowedUser followedUser = new();

                followedUser.FollowedId = cFollowedUser.FollowedId;
                followedUser.FollowerId = cFollowedUser.FollowerId;
                followedUser.DateOfFollow = DateTime.Now;
                dbContext.FollowedUser.Add(followedUser);

                dbContext.SaveChanges();

                return followedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Delete(long id)
        {

            try
            {
                FollowedUser followedUser = (
                   from s in dbContext.FollowedUser
                   where s.Id == id
                   select s
                   ).First();

                dbContext.FollowedUser.Remove(followedUser);
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
