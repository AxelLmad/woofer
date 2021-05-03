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
        public ReactionType GetReaction(GetReaction reaction)
        {
            try
            {
                bool anyPost = dbContext.Post.Any(p => p.Id == reaction.PostId && p.Active);
                bool anyUser = dbContext.User.Any(u => u.Id == reaction.UserId && u.Active);
                if (anyPost && anyUser && reaction.Type >= 0 && reaction.Type <= 3) 
                {
                    ReactionType reactionType = new();
                    var items = (from R in dbContext.Reaction
                                 where (R.PostId == reaction.PostId && R.Type == reaction.Type)
                                 select R);
                    int count = items.Count();
                    reactionType.Amount = count;
                    reactionType.CurrentUser = false;
                    reactionType.Type = reaction.Type;
                    bool anyReactionType = dbContext.Reaction.Any(r => r.UserId == reaction.UserId);
                    if (anyReactionType)
                        reactionType.CurrentUser = true;
                    return reactionType;
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
