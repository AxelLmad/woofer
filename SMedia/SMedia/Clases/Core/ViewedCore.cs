using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class ViewedCore
    {
        SMediaDbContext dbContext;
        public ViewedCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool SetViewOnPost(int idUser, int idPost)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(user => user.Id == idUser);
                bool AnyPost = dbContext.Post.Any(post => post.Id == idPost);
                if (AnyUser && AnyUser)
                {
                    bool anyViewed = dbContext.Viewed.Any(viewed => viewed.UserId == idUser
                                                            && viewed.PostId == idPost);
                    if (!anyViewed)
                    {
                        Viewed viewed = new Viewed
                        {
                            UserId = idUser,
                            PostId = idPost
                        };
                        dbContext.Add(viewed);
                        dbContext.SaveChanges();
                        return true;
                    }
                    return false;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetPostViewes(int id)
        {
            try
            {
                bool AnyPost = dbContext.Post.Any(post => post.Id == id);
                if (AnyPost)
                {
                    IEnumerable<Viewed> viewsPost = (from V in dbContext.Viewed
                                                     where V.PostId == id
                                                     select V);
                    int viewes = viewsPost.Count();
                    return viewes;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
