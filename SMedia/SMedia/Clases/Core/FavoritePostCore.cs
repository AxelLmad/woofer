using SMedia.Models;
using SMedia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class FavoritePostCore
    {
        SMediaDbContext dbContext;
        public FavoritePostCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Post> GetFavoritePosts(long id)
        {
            try
            {
                bool anyUser = dbContext.Post.Any(user => user.Id == id);
                if (anyUser)
                {
                    bool anyFavorite = dbContext.FavoritePost.Any(fav => fav.UserId == id);
                    if (anyFavorite)
                    {
                        var FavPost = from FP in dbContext.FavoritePost
                                      where FP.UserId == id
                                      select FP;
                        List<Post> FavoritePosts = (
                            from LP in dbContext.Post
                            join FU in FavPost on LP.Id equals FU.PostId
                            orderby LP.CreationDate
                            select LP
                            ).Take(10).ToList();
                        return FavoritePosts;
                    }
                    else { return null; }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveFavoritePost(FavoritePost favoritePost)
        {
            try
            {
                bool validUser = dbContext.User.Any(user => user.Id == favoritePost.UserId);
                bool validPost = dbContext.Post.Any(post => post.Id == favoritePost.PostId);
                if (validUser && validPost)
                {
                    bool anyPost = dbContext.FavoritePost.Any(
                        favPost => favPost.UserId == favoritePost.UserId && favPost.PostId == favoritePost.PostId);
                    if (!anyPost)
                    {
                        FavoritePost favPost = new FavoritePost
                        {
                            PostId = favoritePost.PostId,
                            UserId = favoritePost.UserId
                        };
                        dbContext.Add(favPost);
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


        public bool DeleteFavoritePost(long id)
        {
            try
            {
                FavoritePost Favorite = dbContext.FavoritePost.FirstOrDefault(
                    Fav => Fav.Id == id);
                if (Favorite != null)
                {
                    dbContext.Remove(Favorite);
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
    }
}
