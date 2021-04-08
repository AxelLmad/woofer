using Microsoft.AspNetCore.Mvc;
using SMedia.Clases.Core;
using SMedia.Models;
using SMedia.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMedia.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SMediaController : ControllerBase
    {
        SMediaDbContext dbContext;
        public SMediaController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        [HttpPost("{id}")]
        public IActionResult GetProfileUser([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.GetProfileModel(id));
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult GetLastPosts()
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.GetLastPosts());
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        // PUT api/<SMediaController>/5
        [HttpPut]
        public IActionResult SignIn([FromBody] User user)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                sMediaCore.SignIn(user);
                return Ok("User created succesfully!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
