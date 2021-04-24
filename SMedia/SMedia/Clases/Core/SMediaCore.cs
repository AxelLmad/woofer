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
                   ).ToList();
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
                bool IdExists = dbContext.User.Any(user => user.Id == id);
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
        public Post GetLastPosts()
        {
            try
            {
                bool anyPost = dbContext.Post.Any(post => post.Id == 1);
                if (anyPost)
                {
                    Post lastPosts = (
                        from LP in dbContext.Post where LP.Id == 1 
                        orderby LP.CreationDate
                        select LP
                        ).First();
                    return lastPosts;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void CreatePost(Post newpost)
        {
            try
            {

                bool validUser = ValidatePost(newpost);
                if (validUser)
                {
                    newpost.CreationDate = DateTime.Now;
                    newpost.Active = true;
                    dbContext.Add(newpost);
                    dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidatePost(Post post)
        {
            try
            {

                if (string.IsNullOrEmpty(post.Content))
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
