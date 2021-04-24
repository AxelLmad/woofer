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
                   ).Take(10).ToList();
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
                bool IdExists = dbContext.User.Any(user => user.Id == id && user.Active);
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
        public long Login(string NickName, string Password)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(user => user.NickName == NickName && user.Password == Password && user.Active);
                if (AnyUser)
                {
                    User user = (
                        from U in dbContext.User
                        where (U.NickName == NickName && U.Password == Password)
                        select U        
                        ).First();
                    return user.Id;
                }
                return -1;
            }
            catch(Exception ex)
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






        public List<User> GetFollowedUsers(int id)
        {
            try
            {
                bool anyFolloweUser = dbContext.FollowedUser.Any(follUser => follUser.FollowerId == id);
                if (anyFolloweUser) {
                    var UserIFollow = (from FU in dbContext.FollowedUser
                                       where FU.FollowerId == id
                                       select FU);
                    List<User> FollowedUsers = (from U in dbContext.User
                                                join FU in UserIFollow on U.Id equals FU.FollowedId
                                                select U).ToList();
                    return FollowedUsers;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<Community> GetFollowedCommunities(int id)
        {
            try
            {
                bool anyFollowedCommunity = dbContext.FollowedCommunity.Any(follComm => follComm.FollowerId == id);
                if (anyFollowedCommunity)
                {
                    var communityIFollow = (from FU in dbContext.FollowedCommunity
                                            where FU.FollowerId == id
                                            select FU);
                    List<Community> FollowedCommunities = (from U in dbContext.Community
                                                join FU in communityIFollow on U.Id equals FU.CommunityId
                                                select U).ToList();
                    return FollowedCommunities;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool FollowUser(int idFollower, int idToFollow)
        {
            try
            {
                bool anyFollowedUser = dbContext.FollowedUser.Any(follUser => follUser.FollowerId == idFollower 
                                                                    && follUser.FollowedId == idToFollow);
                if (!anyFollowedUser && idFollower != idToFollow)
                {
                    FollowedUser follUser = new FollowedUser
                    {
                        FollowerId = idFollower,
                        FollowedId = idToFollow
                    };
                    follUser.DateOfFollow = DateTime.Now;
                    dbContext.Add(follUser);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool FollowCommunity(int idFollower, int idToFollow)
        {
            try
            {
                bool anyFollow = dbContext.FollowedCommunity.Any(follow => follow.FollowerId == idFollower
                                                                    && follow.CommunityId == idToFollow);
                bool AnyUser = dbContext.User.Any(user => user.Id == idFollower);
                bool AnyCommunity = dbContext.Community.Any(community => community.Id == idToFollow);
                if (!anyFollow && AnyUser && AnyCommunity)
                {
                    FollowedCommunity Follow = new FollowedCommunity
                    {
                        FollowerId = idFollower,
                        CommunityId = idToFollow
                    };
                    Follow.DateOfFollow = DateTime.Now;
                    dbContext.Add(Follow);
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




        public bool CreateCommunity(Community community)
        {
            try
            {
                bool validCommunity = ValidateCommunity(community);
                if (validCommunity)
                {
                    community.CreationDate = DateTime.Now;
                    community.Active = true;
                    dbContext.Add(community);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public bool DisableUser(int id)
        {
            try
            {
                User AnyUser = dbContext.User.FirstOrDefault(user => user.Id == id && user.Active);
                if (AnyUser != null)
                {
                    AnyUser.Active = false;
                    dbContext.Update(AnyUser);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool DisableCommunity(int id)
        {
            try
            {
                Community AnyCommunity = dbContext.Community.FirstOrDefault(comm => comm.Id == id && comm.Active);
                if (AnyCommunity != null)
                {
                    AnyCommunity.Active = false;
                    dbContext.Update(AnyCommunity);
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

        public bool UnfollowUser(int idFollower, int idFollowed)
        {
            try
            {
                FollowedUser follow = dbContext.FollowedUser.FirstOrDefault(
                    follow => follow.FollowerId == idFollower && follow.FollowedId == idFollowed);
                if(follow != null)
                {
                    dbContext.Remove(follow);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool UnfollowCommunity(int idFollower, int idCommunity)
        {
            try
            {
                FollowedCommunity follow = dbContext.FollowedCommunity.FirstOrDefault(
                    follow => follow.FollowerId == idFollower && follow.CommunityId == idCommunity);
                if (follow != null)
                {
                    dbContext.Remove(follow);
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


        public bool ValidateCommunity(Community community)
        {
            try
            {
                if(string.IsNullOrEmpty(community.Name) || string.IsNullOrEmpty(community.Color)
                    || string.IsNullOrEmpty(community.Description) || community?.OwnerId == null || community.OwnerId < 1)
                {
                    return false;
                }
                else
                {
                    bool AnyUser = dbContext.User.Any(user => user.Id == community.OwnerId);
                    if (!AnyUser)
                        return false;
                }
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}
