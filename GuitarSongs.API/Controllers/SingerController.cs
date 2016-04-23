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

        public SingerController(ISingerService singerService)
        {
            SingerService = singerService;
        }

        [HttpGet]
        [Route("singers")]
        public IHttpActionResult GetAllSingers()
        {
            var result = SingerService.GetAllSingers();
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
