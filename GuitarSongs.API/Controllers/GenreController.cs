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

        public GenreController()
        {
            GenreService = new GenreService(new GenreRepository(new SqlConnection(
                        ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString())));
        }

        [HttpGet]
        public IHttpActionResult GetAllGenres()
        {
            return Ok(GenreService.GetAllGenres());
        }
    }
}
