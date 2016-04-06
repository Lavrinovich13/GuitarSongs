using Bl.Services;
using BlContracts.Models;
using BlContracts.ServicesInterfaces;
using DalMsSql.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [Route("basesong/recentsongs")]
        public IHttpActionResult GetRecentSongs()
        {
            var songs = BaseSongService.GetRecentSongs();
            return Ok(songs);
        }

        [HttpPost]
        [Route("basesong/addnewsong")]
        public IHttpActionResult AddBaseSong(BaseSong baseSong)
        {
            var id = BaseSongService.AddBaseSong(baseSong);

            if(id == null)
            {
                return BadRequest("Song can not be added");
            }
            return Ok(id);
        }

        [HttpGet]
        [Route("basesong/songfullinfo/{id}")]
        public IHttpActionResult GetBaseSongById(int id)
        {
            var song = BaseSongService.GetSongById(id);

            if (song == null)
            {
                return BadRequest("Song was not found");
            }
            return Ok(song);
        }
    }
}
