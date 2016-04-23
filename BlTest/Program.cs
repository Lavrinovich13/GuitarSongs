using System.Configuration;
using System.Data.SqlClient;

namespace BlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Bl.Configuration.MapperConfig.Config();

            var service = new Bl.Services.UserSongService(new DalMsSql.UnitOfWork.UnitOfWork(
                        ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString()));

            var song = service.DeleteVideo(new BlContracts.Models.Video { VideoId=1016 }, 6);
        }
    }
}
