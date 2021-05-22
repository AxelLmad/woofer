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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserPictureController : Controller
    {
        SMediaDbContext dbContext;
        public UserPictureController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet("{id}")]
        public IActionResult GetUserPictures(long id)
        {
            try
            {
                UserPictureCore sMediaCore = new UserPictureCore(dbContext);
                return Ok(sMediaCore.GetUserPictures(id));
            }
            catch(Exception ex)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPicture(long id)
        {
            try
            {
                UserPictureCore sMediaCore = new UserPictureCore(dbContext);
                return Ok(sMediaCore.GetPicture(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCurrentPicture(long id)
        {
            try
            {
                UserPictureCore sMediaCore = new UserPictureCore(dbContext);
                return Ok(sMediaCore.GetCurrentPicture(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult Picture([FromBody] UserPictureModel userPicture)
        {
            try
            {
                UserPictureCore sMediaCore = new UserPictureCore(dbContext);
                return Ok(sMediaCore.Picture(userPicture));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
