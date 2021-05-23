using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class FollowedCommunityCore
    {
        SMediaDbContext dbContext;
        public FollowedCommunityCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Community> GetByUser(long id)
        {
            try
            {
                List<Community> communities = (
                   from s in dbContext.FollowedCommunity
                   where s.FollowerId == id
                   select s.Community
                   ).ToList();
                return communities;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public FollowedCommunity Create(CreationFollowedCommunity cFollowedCommunity)
        {

            try
            {
                bool validFollow = validateFollow(cFollowedCommunity);
                if (validFollow)
                {
                    FollowedCommunity followedCommunity = new();
                    followedCommunity.CommunityId = cFollowedCommunity.CommunityId;
                    followedCommunity.FollowerId = cFollowedCommunity.FollowerId;
                    followedCommunity.DateOfFollow = DateTime.Now;
                    dbContext.FollowedCommunity.Add(followedCommunity);
                    dbContext.SaveChanges();
                    return followedCommunity;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Delete(CreationFollowedCommunity follow)
        {

            try
            {
                FollowedCommunity followedCommunity = (
                   from s in dbContext.FollowedCommunity
                   where (s.FollowerId == follow.FollowerId && s.CommunityId == follow.CommunityId)
                   select s
                   ).FirstOrDefault();
                if (followedCommunity != null)
                {
                    dbContext.FollowedCommunity.Remove(followedCommunity);
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

        public bool validateFollow(CreationFollowedCommunity follow)
        {
            

            bool anyUser = dbContext.User.Any(u => u.Id == follow.FollowerId);
            bool anyCommunity = dbContext.Community.Any(c => c.Id == follow.CommunityId);
            bool anyFollowedCommunity = dbContext.FollowedCommunity.Any(fc => fc.CommunityId == follow.CommunityId
                                                                        && fc.FollowerId == follow.FollowerId);
            if (anyUser && anyCommunity && !anyFollowedCommunity)
                return true;
            return false;
        }

    }
}
