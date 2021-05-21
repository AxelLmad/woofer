using SMedia.Models;
using SMedia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class UserPictureCore
    {
        SMediaDbContext dbContext;
        public UserPictureCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<UserPictureModel>GetUserPictures(long id)
        {
            try
            {
                List<UserPictureModel> userPicture = (from UP in dbContext.UserPicture
                                                      where UP.UserId == id
                                                      select new UserPictureModel() { 
                                                            Id = UP.Id,
                                                            ServerPath = UP.ServerPath,
                                                            UserId = UP.UserId,
                                                            Active = UP.Active
                                                      }  ).ToList();
                return userPicture;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public UserPictureModel GetPicture(long id)
        {
            try
            {
                UserPictureModel userPicture = (from UP in dbContext.UserPicture
                                                      where UP.Id == id
                                                      select new UserPictureModel()
                                                      {
                                                          Id = UP.Id,
                                                          ServerPath = UP.ServerPath,
                                                          UserId = UP.UserId,
                                                          Active = UP.Active
                                                      }).FirstOrDefault();
                return userPicture;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserPictureModel GetCurrentPicture(long id)
        {
            try
            {
                UserPictureModel userPicture = (from UP in dbContext.UserPicture
                                                where (UP.UserId == id && UP.Active)
                                                select new UserPictureModel()
                                                {
                                                    Id = UP.Id,
                                                    ServerPath = UP.ServerPath,
                                                    UserId = UP.UserId,
                                                    Active = UP.Active
                                                }).FirstOrDefault();
                return userPicture;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserPictureModel Picture(UserPictureModel picture)
        {
            try
            {
                bool ValidPic = ValidatePicture(picture);
                UserPicture newPic = new();
                newPic.UserId = picture.UserId;
                newPic.ServerPath = picture.ServerPath;
                newPic.Active = picture.Active;
                dbContext.Add(newPic);
                dbContext.SaveChanges();
                return picture;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidatePicture(UserPictureModel picture)
        {
            bool anyUser = dbContext.User.Any(u => u.Id == picture.UserId);
            if (string.IsNullOrEmpty(picture.ServerPath) || !anyUser)
                return false;
            return true;
        }
    }
}
