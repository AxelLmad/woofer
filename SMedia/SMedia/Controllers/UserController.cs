using Microsoft.AspNetCore.Cors;
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
    [EnableCors()]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        SMediaDbContext dbContext;
        public UserController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult ById([FromRoute] long id)
        {
            try
            {
                UserCore core = new UserCore(dbContext);
                return Ok(core.ById(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet]
        public IActionResult GetRandomUser()
        {
            try
            {
                UserCore core = new UserCore(dbContext);
                return Ok(core.GetRandomUser());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{Nick}")]
        public IActionResult SearchUser([FromRoute] string Nick)
        {
            try
            {
                UserCore core = new UserCore(dbContext);
                return Ok(core.SearchUser(Nick));
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult Login(LoginUser user)
        {
            try
            {
                UserCore core = new UserCore(dbContext);
                return Ok(core.Login(user));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult SignUp(SignUpUser user)
        {
            try
            {
                UserCore core = new UserCore(dbContext);
                return Ok(core.SignUp(user));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        public IActionResult Edit(SignUpUser user)
        {
            try
            {
                UserCore core = new UserCore(dbContext);
                return Ok(core.Edit(user));
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
                UserCore core = new UserCore(dbContext);
                return Ok(core.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
