using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class CommunityCore
    {
        SMediaDbContext dbContext;
        public CommunityCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Community ById(long id)
        {
            try
            {
                Community community = (
                   from s in dbContext.Community
                   where s.Id == id
                   && s.Active == true
                   select s
                   ).First();
                return community;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public long Create(CreationCommunity cCommunity)
        {

            try
            {
                Community community = new();

                community.Name = cCommunity.Name;
                community.Color = cCommunity.Color;
                community.Description = cCommunity.Description;
                community.Picture = cCommunity.Picture;
                community.OwnerId = cCommunity.OwnerId;
                community.CreationDate = DateTime.Now;
                community.Active = true;

                dbContext.Community.Add(community);

                dbContext.SaveChanges();

                return community.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public long Edit(CreationCommunity cCommunity)
        {

            try
            {
                Community community = (
                   from s in dbContext.Community
                   where s.Id == cCommunity.Id
                   && s.Active == true
                   select s
                   ).First();

                community.Name = cCommunity.Name;
                community.Color = cCommunity.Color;
                community.Description = cCommunity.Description;
                community.Picture = cCommunity.Picture;
                community.OwnerId = cCommunity.OwnerId;

                dbContext.SaveChanges();

                return community.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Community Delete(long id)
        {

            try
            {
                Community community = (
                   from s in dbContext.Community
                   where s.Id == id
                   && s.Active == true
                   select s
                   ).First();

                community.Active = false;
                dbContext.Update(community);
                dbContext.SaveChanges();
                return community;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
