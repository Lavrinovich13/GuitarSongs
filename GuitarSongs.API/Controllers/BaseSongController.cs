using Bl.Services;
using BlContracts.Models;
using BlContracts.ServicesInterfaces;
using DalMsSql.Repositories;
using GuitarSongs.API;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace AngularJSAuthentication.API.Controllers
{
    public class BaseSongController : ApiController
    {
        protected IBaseSongService BaseSongService;

        public BaseSongController(IBaseSongService baseSongService)
        {
            BaseSongService = baseSongService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("basesong/recentsongs")]
        public IHttpActionResult GetRecentSongs()
        {
            var result = BaseSongService.GetRecentSongs();
            if(result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Errors[0]);
            }
            
        }

        [HttpPost]
        [Authorize]
        [Route("basesong/addnewsong")]
        public IHttpActionResult AddBaseSong(BaseSong baseSong)
        {
            var userId = new AuthRepository().GetUserIdByName(HttpContext.Current.User.Identity.Name);
            var result = BaseSongService.AddBaseSong(userId, baseSong);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Errors[0]);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("basesong/songfullinfo/{id}")]
        public IHttpActionResult GetBaseSongById(int id)
        {
            var result = BaseSongService.GetSongById(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Errors[0]);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("basesong/search/{text}")]
        public IHttpActionResult BaseSongsSearch(string text)
        {
            var result = BaseSongService.SearchFor(text);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Errors[0]);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("basesong/addtofavourite/{basesongid}")]
        public IHttpActionResult AddBaseSongToFavourite(int baseSongId)
        {
            var userId = new AuthRepository().GetUserIdByName(HttpContext.Current.User.Identity.Name);

            var result = BaseSongService.AddBaseSongToFavorite(userId, baseSongId);

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
