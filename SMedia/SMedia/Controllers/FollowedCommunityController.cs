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
    public class FollowedCommunityController : Controller
    {
        SMediaDbContext dbContext;
        public FollowedCommunityController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public IActionResult ByUserId([FromRoute] long id)
        {
            try
            {
                FollowedCommunityCore core = new FollowedCommunityCore(dbContext);
                return Ok(core.GetByUser(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult Create(CreationFollowedCommunity followedCommunity)
        {
            try
            {
                FollowedCommunityCore core = new FollowedCommunityCore(dbContext);
                return Ok(core.Create(followedCommunity));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]CreationFollowedCommunity follow)
        {
            try
            {
                FollowedCommunityCore core = new FollowedCommunityCore(dbContext);
                bool succes = core.Delete(follow);
                if(succes)
                    return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
