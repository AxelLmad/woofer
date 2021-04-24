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

        [HttpPut("{idPost},{idUser},{typeReaction}")]
        public IActionResult SetReactPost([FromRoute] int idPost, [FromRoute] int idUser, byte typeReaction)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool newReaction = sMediaCore.SetReactPost(idPost, idUser, typeReaction);
                if (newReaction)
                    return Ok(idUser + " Reaccionó con " + typeReaction + " a la publicación " + idPost);
                return Ok("No se pudo reaccionar al Post" + idPost +
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
