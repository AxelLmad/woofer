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

        public bool SetViewOnPost(setView view)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(user => user.Id == view.UserId);
                bool AnyPost = dbContext.Post.Any(post => post.Id == view.PostId);
                if (AnyUser && AnyUser)
                {
                    bool anyViewed = dbContext.Viewed.Any(viewed => viewed.UserId == view.UserId
                                                            && viewed.PostId == view.PostId);
                    if (!anyViewed)
                    {
                        Viewed viewed = new Viewed
                        {
                            UserId = view.UserId,
                            PostId = view.PostId
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

        public long GetPostViewes(long id)
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
