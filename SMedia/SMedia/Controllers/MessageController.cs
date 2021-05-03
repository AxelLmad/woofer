﻿using Microsoft.AspNetCore.Mvc;
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
    public class MessageController : Controller
    {
        SMediaDbContext dbContext;
        public MessageController(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetMessages([FromRoute] long id)
        {
            try
            {
                MessageCore sMediaCore = new MessageCore(dbContext);
                List<Message> messages = sMediaCore.GetMessages(id);
                if (messages != null)
                    return Ok(messages);
                return Ok("No hay mensajes!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult SendMessage([FromBody] Message message)
        {
            try
            {
                MessageCore sMessageCore = new MessageCore(dbContext);
                bool sended = sMessageCore.SendMessage(message);
                if(sended)
                    return Ok("Message created succesfully!");
                return Ok("Couldn't send message");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage([FromRoute] long id)
        {
            try
            {
                MessageCore sMediaCore = new MessageCore(dbContext);
                bool Unfollow = sMediaCore.DeleteMessage(id);
                if (Unfollow)
                    return Ok(id + " ha sido eliminado");
                return Ok("No se pudo eliminar");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
