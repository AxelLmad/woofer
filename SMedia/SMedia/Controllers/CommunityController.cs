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
    public class CommunityController : Controller
    {
        SMediaDbContext dbContext;
        public CommunityController(SMediaDbContext dbContext)
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
                return Ok(sMediaCore.GetCommunity(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult Create(CreationCommunity community)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.CreateCommunity(community));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        public IActionResult Edit(CreationCommunity community)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                return Ok(sMediaCore.EditCommunity(community));
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
