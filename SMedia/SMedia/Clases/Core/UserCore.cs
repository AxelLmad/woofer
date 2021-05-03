using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class UserCore
    {
        SMediaDbContext dbContext;
        public UserCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<User> GetAll()
        {

            try
            {
                List<User> users = (
                   from s in dbContext.User
                   where s.Active == true
                   select s
                   ).ToList();
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public User ById(long id)
        {
            try
            {
                User user = (
                   from s in dbContext.User
                   where s.Id == id
                   && s.Active == true
                   select s
                   ).FirstOrDefault();
                if(user != null)
                    return user;
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public FixedUser Edit(SignUpUser sUser)
        {

            try
            {
                User user = (
                        from s in dbContext.User
                        where s.Id == sUser.Id
                        && s.Active == true
                        select s
                        ).FirstOrDefault();
                if (user != null) {
                    user.NickName = sUser.NickName;
                    user.Password = sUser.Password;
                    user.Email = sUser.Email;
                    user.Name = sUser.Name;
                    user.LastName = sUser.LastName;
                    user.Picture = sUser.Picture;

                    dbContext.SaveChanges();

                    return new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, user.Picture, user.RegisterDate, user.LastLogin);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public FixedUser Delete(long id)
        {

            try
            {
                User user = (
                   from s in dbContext.User
                   where s.Id == id
                   && s.Active == true
                   select s
                   ).FirstOrDefault();
                if (user != null) {
                    user.Active = false;
                    dbContext.Update(user);
                    dbContext.SaveChanges();
                    return new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, user.Picture, user.RegisterDate, user.LastLogin);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public FixedUser Login(LoginUser loginUser)
        {
            try
            {
                User user = (
                         from s in dbContext.User
                         where s.NickName == loginUser.NickName
                         && s.Password == loginUser.Password
                         && s.Active == true
                         select s
                         ).FirstOrDefault();
                if (user != null)
                {
                    FixedUser fUser = new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, user.Picture, user.RegisterDate, user.LastLogin);
                    return fUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long SignUp(SignUpUser sUser)
        {
            try
            {
                if (validUser(sUser))
                {
                    User user = new();

                    user.NickName = sUser.NickName;
                    user.Password = sUser.Password;
                    user.Email = sUser.Email;
                    user.Name = sUser.Name;
                    user.LastName = sUser.LastName;
                    user.Picture = sUser.Picture;
                    user.RegisterDate = DateTime.Now;
                    user.LastLogin = DateTime.Now;
                    user.Active = true;

                    dbContext.User.Add(user);

                    dbContext.SaveChanges();

                    return user.Id;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool validUser(SignUpUser user)
        {
            bool anyUser = dbContext.User.Any(u => u.Name == user.Name);
            if (string.IsNullOrEmpty(user.NickName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email)
                || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.LastName) || anyUser)
                return false;
            return true;
        }

    }
}
