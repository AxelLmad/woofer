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
                Community AnyCommunity = dbContext.Community.FirstOrDefault(com => com.Id == id);
                if(AnyCommunity!=null)
                    return AnyCommunity;
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Community> UserCreatedCommunity(long id)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(u => u.Id == id && u.Active);
                if (AnyUser)
                {
                    List<Community> UserCommunities = (from C in dbContext.Community
                                                       where (C.OwnerId == id && C.Active)
                                                       select C).ToList();
                    return UserCommunities;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CommunityModel> GetRandomCommunity()
        {
            try
            {
                bool AnyCommunity = dbContext.Community.Any(x=>x.Active);
                if (AnyCommunity) {
                    List<CommunityModel> UserCommunities = (from C in dbContext.Community
                                                      join U in dbContext.User on C.OwnerId equals U.Id
                                                      orderby Guid.NewGuid()
                                                      where (C.Active)
                                                      select new CommunityModel ()
                                                      {
                                                         Id= C.Id,
                                                         Name = C.Name,
                                                         Color = C.Color,
                                                         Description = C.Description,
                                                         Picture = C.Picture,
                                                         CreationDate = C.CreationDate,
                                                         OwnerId = C.OwnerId,
                                                         OwnerName = $"{U.Name} {U.LastName}",
                                                         OwnerNickName = U.NickName,
                                                         Active = C.Active
                                                      }).Take(6).ToList();
                    return UserCommunities;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public long Create(CreationCommunity cCommunity)
        {

            try
            {
                bool ValidCommunity = validateCommunity(cCommunity);
                bool ExistCommunity = dbContext.Community.Any(c => c.Name == cCommunity.Name
                                                                && c.Active == true);
                if (ValidCommunity && !ExistCommunity) {
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
                return -1;
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
                   ).FirstOrDefault();
                if (community != null)
                {
                    community.Name = cCommunity.Name;
                    community.Color = cCommunity.Color;
                    community.Description = cCommunity.Description;
                    community.Picture = cCommunity.Picture;
                    community.OwnerId = cCommunity.OwnerId;
                    dbContext.SaveChanges();
                    return community.Id;
                }
                return -1;
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
                   ).FirstOrDefault();
                if (community != null)
                {
                    community.Active = false;
                    dbContext.Update(community);
                    dbContext.SaveChanges();
                    return community;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        bool validateCommunity(CreationCommunity cCommunity)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(u => u.Id == cCommunity.OwnerId);
                if(string.IsNullOrEmpty(cCommunity.Name) || string.IsNullOrEmpty(cCommunity.Color)
                    || string.IsNullOrEmpty(cCommunity.Description) || !AnyUser)
                    return false;
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



    }
}
