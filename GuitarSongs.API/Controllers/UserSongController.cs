using BusinesContract.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GuitarSongs.API.Controllers
{
    public class UserSongController : ApiController
    {
        protected IUserSongService UserSongService;

        public UserSongController(IUserSongService userSongService)
        {
            UserSongService = userSongService;
        }

        [HttpGet]
        [Authorize]
        [Route("usersong/mysongs")]
        public IHttpActionResult GetUserSongs()
        {
            var userId = new AuthRepository().GetUserIdByName(HttpContext.Current.User.Identity.Name);
            var result = UserSongService.GetSongsForUser(userId);
            if(result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Errors[0]);
            }
            
        }

        [HttpGet]
        [Authorize]
        [Route("usersong/songinfo/{id}")]
        public IHttpActionResult GetSongInfo(int id)
        {
            var result = UserSongService.GetSongById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Errors[0]);
            }

        }
    }
}
