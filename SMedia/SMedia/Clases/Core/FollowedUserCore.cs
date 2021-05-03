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
                bool anyUser = dbContext.User.Any(u => u.Id == followerId);
                if (anyUser) {                 
                    List<User> followedUsers = (
                        from s in dbContext.FollowedUser
                        where s.FollowerId == followerId
                        select s.Followed
                        ).ToList();
                    return followedUsers;
                }
                return null;
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
                bool validFollow = validateFollow(cFollowedUser);
                if (validFollow) 
                {
                    FollowedUser followedUser = new();
                    followedUser.FollowedId = cFollowedUser.FollowedId;
                    followedUser.FollowerId = cFollowedUser.FollowerId;
                    followedUser.DateOfFollow = DateTime.Now;
                    dbContext.FollowedUser.Add(followedUser);
                    dbContext.SaveChanges();
                    return followedUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Delete(long id)
        {

            try
            {
                FollowedUser followedUser = (
                   from s in dbContext.FollowedUser
                   where s.Id == id
                   select s
                   ).FirstOrDefault();
                if (followedUser != null)
                {
                    dbContext.FollowedUser.Remove(followedUser);
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

        public bool validateFollow(CreationFollowedUser cFollowedUser)
        {
            bool validUser = dbContext.User.Any(u => u.Id == cFollowedUser.FollowedId);
            bool validUser2 = dbContext.User.Any(u => u.Id == cFollowedUser.FollowerId);
            bool anyFollow = dbContext.FollowedUser.Any(fu => fu.FollowedId == cFollowedUser.FollowedId
                                                            && fu.FollowerId == cFollowedUser.FollowerId);
            if (validUser && validUser2 && !anyFollow)
                return true;
            return false;
        }

    }
}
