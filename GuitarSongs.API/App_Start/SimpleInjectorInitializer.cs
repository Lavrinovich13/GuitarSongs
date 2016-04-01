using GuitarSongs.API.App_Start;
using SimpleInjector;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;
using DalMsSql;
using System.Data.SqlClient;
using System.Configuration;
using BlContracts.ServicesInterfaces;
using DalContracts.RepositoriesInterfaces;
using DalMsSql.Repositories;
using Bl.Services;

namespace GuitarSongs.API.App_Start
{
    public class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            InitializeContainer(container);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            //TODO split registers
            container.Register<IBaseSongService>(() => 
                new BaseSongService(new BaseSongRepository(new SqlConnection(
                        ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString()))), Lifestyle.Scoped);
         }
    }
}