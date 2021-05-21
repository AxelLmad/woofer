using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class ReactionCore
    {
        SMediaDbContext dbContext;
        public ReactionCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public bool SetReactPost(Reaction reaction)
        {
            try
            {
                bool AnyUser = dbContext.User.Any(user => user.Id == reaction.UserId);
                bool AnyPost = dbContext.Post.Any(post => post.Id == reaction.PostId);
                if (AnyUser && AnyPost)
                {
                    Reaction AnyReaction = dbContext.Reaction.FirstOrDefault(reaction => reaction.PostId == reaction.PostId
                                            && reaction.UserId == reaction.UserId);
                    if (AnyReaction == null)
                    {
                        Reaction newReaction = new Reaction
                        {
                            UserId = reaction.UserId,
                            PostId = reaction.PostId,
                            Type = reaction.Type
                        };
                        dbContext.Add(newReaction);
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        AnyReaction.Type = reaction.Type;
                        dbContext.Update(AnyReaction);
                        dbContext.SaveChanges();
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ReactionType GetReaction(long PostId)
        {
            try
            {
                bool anyPost = dbContext.Post.Any(p => p.Id == PostId);
                if (anyPost) 
                {
                    var reactionType1 = (from R in dbContext.Reaction
                                                 where (R.PostId == PostId && R.Type == 1)
                                                 select R);
                    var reactionType2 = (from R in dbContext.Reaction
                                         where (R.PostId == PostId && R.Type == 2)
                                         select R);
                    var reactionType3 = (from R in dbContext.Reaction
                                         where (R.PostId == PostId && R.Type == 3)
                                         select R);
                    int count1 = reactionType1.Count();
                    int count2 = reactionType2.Count();
                    int count3 = reactionType3.Count();
                    ReactionType Reaction = new ReactionType();
                    Reaction.Type1 = count1;
                    Reaction.Type2 = count2;
                    Reaction.Type3 = count3;
                    Reaction.IdPost = PostId;
                    return Reaction;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
