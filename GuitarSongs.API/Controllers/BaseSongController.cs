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

        public BaseSongController()
        {
            BaseSongService = new BaseSongService(new BaseSongRepository(new SqlConnection(
                        ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString())));
        }

        //public BaseSongController(IBaseSongService baseSongService)
        //{
        //    BaseSongService = baseSongService;
        //}

        [HttpGet]
        public IHttpActionResult GetBaseSongInfoById(int id)
        {
            var song = BaseSongService.GetBaseSongInfoById(id);
            return Ok(song);
        }

        [HttpPost]
        public IHttpActionResult AddBaseSong(BaseSong baseSong)
        {
            var id = BaseSongService.AddBaseSong(baseSong);

            if(id == null)
            {
                return BadRequest("Song can not be added");
            }
            return Ok(id);
        }
    }
}
