using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class PostPictureCore
    {
        SMediaDbContext dbContext;
        public PostPictureCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<PostPicture> GetByPostId(long id)
        {
            try
            {
                List<PostPicture> pictures = (
                   from s in dbContext.PostPicture
                   where s.PostId == id
                   select s
                   ).ToList();
                return pictures;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
