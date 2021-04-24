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

        [HttpGet("{id}")]
        public IActionResult GetProfileUser([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                ProfileModel profileModel = sMediaCore.GetProfileModel(id);
                if (profileModel != null)
                    return Ok(profileModel);
                else
                    return Ok("No se encontró usuario con: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLastPosts([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                List<Post> lastposts = sMediaCore.GetLastPosts(id);
                if (lastposts != null)
                    return Ok(lastposts);
                return Ok("No hay posts!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFavoritePosts([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                List<Post> favPosts = sMediaCore.GetFavoritePosts(id);
                if (favPosts != null)
                    return Ok(favPosts);
                return Ok("No tienes posts guardados!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFollowedUsers([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                List<User> followedUsers = sMediaCore.GetFollowedUsers(id);
                if (followedUsers != null)
                    return Ok(followedUsers);
                return Ok("No hay nadie quien siga este usuario guardados");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetFollowedCommunities([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                List<Community> followedCommunities = sMediaCore.GetFollowedCommunities(id);
                if (followedCommunities != null)
                    return Ok(followedCommunities);
                return Ok("No hay ninguna comunidad que siga este usuario guardados");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetMessages([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
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
        [HttpPost]
        [Route("{NickName}")]
        public IActionResult Login([FromRoute] string NickName,[FromBody] string Password)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                long idUser = sMediaCore.Login(NickName, Password);
                if (idUser != -1)
                    return Ok(idUser);
                return Ok("No hay usuario con: " + idUser);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
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

        [HttpPut("{idUser},{idPost}")]
        public IActionResult SaveFavoritePost([FromRoute] int idUser, [FromRoute] int idPost)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool favPosts = sMediaCore.SaveFavoritePost(idUser, idPost);
                if (favPosts)
                    return Ok("Post guardador satisfactoriamente!");
                return Ok("No se guardó el post. Se debe enviar id del usuairio y id del post, comprobar tambien: " +
                    "\n El id del post y del usuario existen" +
                    "\n No existe ya esa combinación de post y id. No duplicar en BD");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut("{idFollower},{idToFollow}")]
        public IActionResult FollowUser([FromRoute] int idFollower, [FromRoute] int idToFollow)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool newFollow = sMediaCore.FollowUser(idFollower, idToFollow);
                if (newFollow)
                    return Ok(idFollower + " Ahora sigue a " + idToFollow);
                return Ok("No se pudo seguir al usuario" + idToFollow +
                    ". Se debe enviar id del usuairio seguidor y id del usuario a seguir, comprobar tambien: " +
                    "\n Que no sean el mismo usuario/id" +
                    "\n No existe ya esa combinación de seguidor y seguido. No duplicar en BD!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut("{idFollower},{idToFollow}")]
        public IActionResult FollowCommunity([FromRoute] int idFollower, [FromRoute] int idToFollow)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool newFollow = sMediaCore.FollowCommunity(idFollower, idToFollow);
                if (newFollow)
                    return Ok(idFollower + " Ahora sigue a " + idToFollow);
                return Ok("No se pudo seguir a la comunidad" + idToFollow +
                    ". Se debe enviar id del usuario seguidor y id de la comunidad a seguir, comprobar tambien: " +
                    "\n No existe ya esa combinación de seguidor y comunidad. No duplicar en BD!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut("{idPost},{idUser},{typeReaction}")]
        public IActionResult SetReactPost([FromRoute] int idPost, [FromRoute] int idUser, byte typeReaction)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool newReaction = sMediaCore.SetReactPost(idPost, idUser, typeReaction);
                if (newReaction)
                    return Ok(idUser + " Reaccionó con " + typeReaction + " a la publicación " + idPost);
                return Ok("No se pudo reaccionar al Post" + idPost +
                    ". Se debe enviar id del usuario que reacciona y id del post a reaccionar, comprobar tambien: " +
                    "\n No existe ya esa combinación de seguidor y comunidad. No se puede reaccionar 2 veces en BD!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

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

        [HttpPut]
        public IActionResult CreatePost([FromBody] Post post)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool exito = sMediaCore.CreatePost(post);
                if (exito)
                    return Ok("Post created succesfully!");
                return Ok("Post no valido");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut]
        public IActionResult SendMessage([FromBody] Message message)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                sMediaCore.SendMessage(message);
                return Ok("Message created succesfully!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPut]
        public IActionResult CreateCommunity([FromBody] Community community)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool communityCreated = sMediaCore.CreateCommunity(community);
                if (communityCreated)
                    return Ok("Comunidad creada de manera exitosa");
                return Ok("Comunidad NO creada. Algo sucedió mal!");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult DisableUser([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool DisabledUser = sMediaCore.DisableUser(id);
                if (DisabledUser)
                    return Ok("Usuario eliminado!");
                return Ok("No se pudo eliminar al usuario con id: " + id);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult DisablePost([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool DisabledPost = sMediaCore.DisablePost(id);
                if (DisabledPost)
                    return Ok("Post eliminado!");
                return Ok("No se pudo eliminar el post con id: " + id);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
        [HttpPut("{id}")]
        public IActionResult DisableCommunity([FromRoute] int id)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool DisabledCommunity = sMediaCore.DisableCommunity(id);
                if (DisabledCommunity)
                    return Ok("Comunidad eliminada!");
                return Ok("No se pudo eliminar la communidad con id: " + id);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete("{idFollower},{idFollowed}")]
        public IActionResult UnfollowUser([FromRoute] int idFollower, int idFollowed)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool Unfollow = sMediaCore.UnfollowUser(idFollower, idFollowed);
                if (Unfollow)
                    return Ok(idFollower + " ha dejado de seguir al usuario: " + idFollowed);
                return Ok("No se pudo dar unfollow");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpDelete("{idFollower},{idCommunity}")]
        public IActionResult UnfollowCommunity([FromRoute] int idFollower, int idCommunity)
        {
            try
            {
                SMediaCore sMediaCore = new SMediaCore(dbContext);
                bool Unfollow = sMediaCore.UnfollowCommunity(idFollower, idCommunity);
                if (Unfollow)
                    return Ok(idFollower + " ha dejado de seguir al usuario: " + idCommunity);
                return Ok("No se pudo dar unfollow");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        public IActionResult all()
        {
            try
            {
                bool IdExists = dbContext.Post.Any();
                if (IdExists)
                {
                    Post comms = dbContext.Post.First();
                    return Ok(comms);
                }
                else
                {
                    return Ok("No hay nada!");
                }
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
