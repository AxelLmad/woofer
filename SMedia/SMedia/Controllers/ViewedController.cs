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
    public class ViewedController : Controller
    {
        SMediaDbContext dbContext;
        public ViewedController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPut]
        public IActionResult SetViewOnPost([FromBody]setView view)
        {
            try
            {
                ViewedCore sMediaCore = new ViewedCore(dbContext);
                bool viewed = sMediaCore.SetViewOnPost(view);
                if (viewed)
                    return Ok("Vista agregada al Post!");
                return Ok("Vista no se pudo agregar");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPostViewes([FromRoute] long id)
        {
            try
            {
                ViewedCore sMediaCore = new ViewedCore(dbContext);
                long viewes = sMediaCore.GetPostViewes(id);
                if (viewes != -1)
                    return Ok(viewes);
                return Ok("No hay vistas! Algo salió mal");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
