using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Bl.Services;
using BlContracts.ServicesInterfaces;
using DalMsSql.Repositories;
using System.Data.SqlClient;
using System.Configuration;

namespace GuitarSongs.API.Controllers
{
    public class GenreController : ApiController
    {
        protected IGenreService GenreService;

        public GenreController(IGenreService genreService)
        {
            GenreService = genreService;
        }

        [HttpGet]
        [Route("genres")]
        public IHttpActionResult GetAllGenres()
        {
            var result = GenreService.GetAllGenres();
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
