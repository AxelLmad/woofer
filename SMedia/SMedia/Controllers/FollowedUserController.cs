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
    public class FollowedUserController : Controller
    {
        SMediaDbContext dbContext;
        public FollowedUserController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public IActionResult FollowedUsers([FromRoute] long id)
        {
            try
            {
                FollowedUserCore core = new FollowedUserCore(dbContext);
                return Ok(core.GetFollowedUsers(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Followers([FromRoute] long id)
        {
            try
            {
                FollowedUserCore core = new FollowedUserCore(dbContext);
                return Ok(core.GetFollowers(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult Create(CreationFollowedUser followedUser)
        {
            try
            {
                FollowedUserCore core = new FollowedUserCore(dbContext);
                return Ok(core.Create(followedUser));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] long id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.DeleteCommunity(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
