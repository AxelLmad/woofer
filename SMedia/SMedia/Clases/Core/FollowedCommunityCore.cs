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
                   where s.Id == id
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
                FollowedCommunity followedCommunity = new();

                followedCommunity.CommunityId = cFollowedCommunity.CommunityId;
                followedCommunity.FollowerId = cFollowedCommunity.FollowerId;
                followedCommunity.DateOfFollow = DateTime.Now;

                dbContext.FollowedCommunity.Add(followedCommunity);

                dbContext.SaveChanges();

                return followedCommunity;
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
                FollowedCommunity followedCommunity = (
                   from s in dbContext.FollowedCommunity
                   where s.Id == id
                   select s
                   ).First();

                dbContext.FollowedCommunity.Remove(followedCommunity);
                dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
