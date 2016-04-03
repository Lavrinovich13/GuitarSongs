using GuitarSongs.API;
using GuitarSongs.API.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;


//
namespace GuitarSongs.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //TODO Config where?
            Bl.Configuration.MapperConfig.Config();

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
