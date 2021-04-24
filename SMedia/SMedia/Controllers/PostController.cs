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
    public class PostController : Controller
    {
        SMediaDbContext dbContext;
        public PostController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetLastPosts([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                List<Post> lastposts = sMediaCore.GetLastPosts(id);
                if (lastposts != null)
                    return Ok(lastposts);
                return Ok("No hay posts!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        public IActionResult CreatePost([FromBody] Post post)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool exito = sMediaCore.CreatePost(post);
                if (exito)
                    return Ok("Post created succesfully!");
                return Ok("Post no valido");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult DisablePost([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool DisabledPost = sMediaCore.DisablePost(id);
                if (DisabledPost)
                    return Ok("Post eliminado!");
                return Ok("No se pudo eliminar el post con id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
