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

        [HttpPut("{idUser},{idPost}")]
        public IActionResult SetViewOnPost([FromRoute] int idUser, int idPost)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool viewed = sMediaCore.SetViewOnPost(idUser, idPost);
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
        public IActionResult GetPostViewes([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                int viewes = sMediaCore.GetPostViewes(id);
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
