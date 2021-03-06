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
        public ReturnUser ById(long id)
        {
            try
            {
                ReturnUser user = (
                   from s in dbContext.User
                   join f in dbContext.UserPicture on s.Id equals f.UserId
                   where s.Id == id
                   && s.Active == true
                   select new ReturnUser
                   {
                       Id = s.Id,
                       NickName = s.NickName,
                       Email = s.Email,
                       Name = s.Name,
                       LastName = s.LastName,
                       Picture = f.ServerPath,
                       RegisterDate = s.RegisterDate,
                       LastLogin = s.LastLogin
                   }).FirstOrDefault();
                if(user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<User> GetRandomUser()
        {
            try
            {
                bool AnyUser = dbContext.User.Any(x=> x.Active);
                if (AnyUser)
                {
                   List<User> user = (  from s in dbContext.User
                                        orderby Guid.NewGuid()
                                        where s.Active
                                        select s
                                        ).Take(6).ToList();
                    foreach(User u in user)
                    {
                        u.Password = null;
                    }
                    return user;
                }
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
                UserPicture pic = null;
                if (sUser.ServerPath != null)
                {
                    pic = (from s in dbContext.UserPicture
                                       where (s.Id == user.Id && s.ServerPath == sUser.ServerPath)
                                       select s).FirstOrDefault();
                }
                if (user != null) {
                    user.NickName = sUser.NickName;
                    if(sUser.Password != null)
                    {
                        user.Password = sUser.Password;
                    }
                    user.Email = sUser.Email;
                    user.Name = sUser.Name;
                    user.LastName = sUser.LastName;
                    dbContext.SaveChanges();
                    if(sUser.ServerPath != null)
                    {
                        if (pic == null)
                        {
                            pic = new UserPicture();
                            pic.ServerPath = sUser.ServerPath;
                            pic.Active = true;
                            pic.UserId = user.Id;
                            dbContext.Add(pic);
                            List<UserPicture> pictures = (from s in dbContext.UserPicture
                                                          where s.UserId == user.Id
                                                          select s).ToList();
                            foreach (UserPicture up in pictures)
                            {
                                up.Active = false;
                            }
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            List<UserPicture> pictures = (from s in dbContext.UserPicture
                                                          where s.UserId == user.Id
                                                          select s).ToList();
                            foreach (UserPicture up in pictures)
                            {
                                up.Active = false;
                            }
                            pic.Active = true;
                            dbContext.SaveChanges();
                        }
                    }
                    

                    return new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, "", user.RegisterDate, user.LastLogin);
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
                UserPicture pic = (from s in dbContext.UserPicture
                                   where (s.UserId == user.Id && s.Active)
                                   select s).FirstOrDefault();
                if (pic == null)
                {
                    pic = new UserPicture();
                    pic.ServerPath = null;
                }
                if (user != null) {
                    user.Active = false;
                    dbContext.Update(user);
                    dbContext.SaveChanges();
                    return new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, pic.ServerPath ,user.RegisterDate, user.LastLogin);
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
                UserPicture pic = (from s in dbContext.UserPicture
                                   where (s.UserId == user.Id && s.Active)
                                   select s).FirstOrDefault();
                if (pic == null)
                {
                    pic = new UserPicture();
                    pic.ServerPath = null;
                }
                if (user != null)
                {
                    FixedUser fUser = new FixedUser(user.Id, user.NickName, user.Email, user.Name, user.LastName, pic.ServerPath , user.RegisterDate, user.LastLogin);
                    return fUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ReturnUser> SearchUser(string Nick)
        {
            try
            {
                List<ReturnUser> Mathches = (from U in dbContext.User
                                             join P in dbContext.UserPicture on U.Id equals P.UserId
                                            where (U.NickName.Contains(Nick) && U.Active && P.Active)
                                            select new ReturnUser() 
                                            { 
                                                Id = U.Id,
                                                NickName = U.NickName,
                                                Email = U.NickName,
                                                Name = U.Name,
                                                LastName = U.LastName,
                                                Picture = P.ServerPath,
                                                RegisterDate = U.RegisterDate,
                                                LastLogin = U.LastLogin
                                            }).ToList();
                return Mathches;
            }
            catch(Exception ex)
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
            bool anyUser = dbContext.User.Any(u => u.NickName == user.NickName);
            bool anyEmail = dbContext.User.Any(u => u.Email == user.Email);
            if (string.IsNullOrEmpty(user.NickName) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Email)
                || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.LastName) || anyUser || anyEmail)
            {
                return false;
            }
                
            return true;
        }

    }
}
