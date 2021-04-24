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
    public class PostPictureController : Controller
    {
        SMediaDbContext dbContext;
        public PostPictureController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public IActionResult ByPostId([FromRoute] long id)
        {
            try
            {
                PostPictureCore core = new PostPictureCore(dbContext);
                return Ok(core.GetByPostId(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }



    }
}
