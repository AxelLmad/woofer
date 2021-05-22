using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SMedia.Clases.Core;
using SMedia.Models;
using SMedia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SMedia.Controllers
{
    [EnableCors()]
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
        public IActionResult ById([FromRoute] long id)
        {
            try
            {
                PostCore sMediaCore = new PostCore(dbContext);
                LastPostsModel post = sMediaCore.ById(id);
                if (post != null)
                    return Ok(post);
                return Ok("No existe el post!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLastPosts([FromRoute] long id)
        {
            try
            {
                PostCore sMediaCore = new PostCore(dbContext);
                List<LastPostsModel> lastposts = sMediaCore.GetLastPosts(id);
                if (lastposts != null)
                    return Ok(lastposts);
                return Ok("No hay posts!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPostUser([FromRoute] long id)
        {
            try
            {
                PostCore sMediaCore = new PostCore(dbContext);
                List<LastPostsModel> lastPosts = sMediaCore.GetPostUser(id);
                if(lastPosts != null)
                    return Ok(lastPosts);
                return Ok("No hay posts!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetResponsePost([FromRoute] long id)
        {
            try
            {
                PostCore postCore = new PostCore(dbContext);
                return Ok(postCore.GetResponsePost(id));
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody] CreationPost post)
        {
            try
            {
                PostCore sMediaCore = new PostCore(dbContext);
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
        public IActionResult DisablePost([FromRoute] long id)
        {
            try
            {
                PostCore sMediaCore = new PostCore(dbContext);
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
