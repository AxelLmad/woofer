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
    public class UserController : Controller
    {
        SMediaDbContext dbContext;
        public UserController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
      
        [HttpGet]
        public IActionResult All()
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.GetAllUsers());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ById([FromRoute] long id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.GetUser(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult Login(LoginUser user)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.Login(user));
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
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.SignUp(user));
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
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.EditUser(user));
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
                return Ok(sMediaCore.DeleteUser(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
