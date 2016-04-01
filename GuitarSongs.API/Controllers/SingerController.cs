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
    public class SingerController : ApiController
    {
        protected ISingerService SingerService;

        public SingerController()
        {
            SingerService = new SingerService(new SingerRepository(new SqlConnection(
                        ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString())));
        }

        [HttpGet]
        public IHttpActionResult GetAllSingers()
        {
            return Ok(SingerService.GetAllSingers());
        }
    }
}
