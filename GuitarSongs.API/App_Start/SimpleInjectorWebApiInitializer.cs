//[assembly: WebActivator.PostApplicationStartMethod(typeof(GuitarSongs.API.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace GuitarSongs.API.App_Start
{
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;
    using BlContracts.ServicesInterfaces;
    using Bl.Services;
    using DalMsSql.Repositories;
    using System.Data.SqlClient;
    using System.Configuration;
    using DalContracts.RepositoriesInterfaces;
    using System.Data.Common;
    
    public static class SimpleInjectorWebApiInitializer
    {
        public static Container InitializeContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<DbConnection>(() => new SqlConnection(
                                   ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString()), Lifestyle.Scoped);

            container.Register<IBaseSongRepository, BaseSongRepository>(Lifestyle.Scoped);
            container.Register<IGenreRepository, GenreRepository>(Lifestyle.Scoped);
            container.Register<ISingerRepository, SingerRepository>(Lifestyle.Scoped);

            container.Register<IBaseSongService, BaseSongService>(Lifestyle.Scoped);
            container.Register<ISingerService, SingerService>(Lifestyle.Scoped);
            container.Register<IGenreService, GenreService>(Lifestyle.Scoped);

            //container.Register<IBaseSongService>(() =>
            //               new BaseSongService(new BaseSongRepository(new SqlConnection(
            //                       ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString()))), Lifestyle.Scoped);
        }
    }
}