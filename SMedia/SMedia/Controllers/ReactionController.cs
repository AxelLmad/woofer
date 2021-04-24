using Microsoft.AspNetCore.Mvc;
using SMedia.Clases.Core;
using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SMedia.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReactionController : Controller
    {
        SMediaDbContext dbContext;
        public ReactionController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult SetReactPost([FromBody] Reaction reaction)
        {
            try
            {
                ReactionCore sMediaCore = new ReactionCore(dbContext);
                bool newReaction = sMediaCore.SetReactPost(reaction);
                if (newReaction)
                    return Ok(reaction.UserId + " Reaccionó con " + reaction.Type + " a la publicación " + reaction.PostId);
                return Ok("No se pudo reaccionar al Post" + reaction.PostId +
                    ". Se debe enviar id del usuario que reacciona y id del post a reaccionar, comprobar tambien: " +
                    "\n No existe ya esa combinación de seguidor y comunidad. No se puede reaccionar 2 veces en BD!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
