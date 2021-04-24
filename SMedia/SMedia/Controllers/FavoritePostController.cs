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
    public class FavoritePostController : Controller
    {
        SMediaDbContext dbContext;
        public FavoritePostController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetFavoritePosts([FromRoute] int id)
        {
            try
            {
                FavoritePostCore sMediaCore = new FavoritePostCore(dbContext);
                List<Post> favPosts = sMediaCore.GetFavoritePosts(id);
                if (favPosts != null)
                    return Ok(favPosts);
                return Ok("No tienes posts guardados!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut("{idUser},{idPost}")]
        public IActionResult SaveFavoritePost([FromRoute] int idUser, [FromRoute] int idPost)
        {
            try
            {
                FavoritePostCore sMediaCore = new FavoritePostCore(dbContext);
                bool favPosts = sMediaCore.SaveFavoritePost(idUser, idPost);
                if (favPosts)
                    return Ok("Post guardador satisfactoriamente!");
                return Ok("No se guardó el post. Se debe enviar id del usuairio y id del post, comprobar tambien: " +
                    "\n El id del post y del usuario existen" +
                    "\n No existe ya esa combinación de post y id. No duplicar en BD");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFavoritePost([FromRoute] int id)
        {
            try
            {
                FavoritePostCore sMediaCore = new FavoritePostCore(dbContext);
                bool Deleted = sMediaCore.DeleteFavoritePost(id);
                if (Deleted)
                    return Ok(id + " ha sido eliminado");
                return Ok("No se pudo eliminar");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
