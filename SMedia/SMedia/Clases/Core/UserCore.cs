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
                   ).First();
                return user;
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
                        ).First();

                user.NickName = sUser.NickName;
                user.Password = sUser.Password;
                user.Email = sUser.Email;
                user.Name = sUser.Name;
                user.LastName = sUser.LastName;
                user.Picture = sUser.Picture;

                dbContext.SaveChanges();

                return new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, user.Picture, user.RegisterDate, user.LastLogin);
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
                   ).First();

                user.Active = false;

                return new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, user.Picture, user.RegisterDate, user.LastLogin);
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
                         ).First();
                FixedUser fUser = new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, user.Picture, user.RegisterDate, user.LastLogin);
                return fUser;
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
                User user = new();

                user.NickName = sUser.NickName;
                user.Password = sUser.Password;
                user.Email = sUser.Email;
                user.Name = sUser.Name;
                user.LastName = sUser.LastName;
                user.Picture = sUser.Picture;

                dbContext.User.Add(user);

                dbContext.SaveChanges();

                return user.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
