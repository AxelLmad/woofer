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

      

        [HttpGet("{id}")]
        public IActionResult ById([FromRoute] long id)
        {
            try
            {
                CommunityCore core = new CommunityCore(dbContext);
                return Ok(core.ById(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult UserCreatedCommunity([FromRoute] long id)
        {
            try
            {
                CommunityCore core = new CommunityCore(dbContext);
                List<Community> communities = core.UserCreatedCommunity(id);
                if(communities != null)
                    return Ok(core.UserCreatedCommunity(id));
                return Ok("No hay comunidades");
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult Create(CreationCommunity community)
        {
            try
            {
                CommunityCore core = new CommunityCore(dbContext);
                return Ok(core.Create(community));
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
                CommunityCore core = new CommunityCore(dbContext);
                return Ok(core.Edit(community));
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
                CommunityCore core = new CommunityCore(dbContext);
                return Ok(core.Delete(id));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
