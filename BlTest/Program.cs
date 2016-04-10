using System.Configuration;
using System.Data.SqlClient;

namespace BlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Bl.Configuration.MapperConfig.Config();

           // var service = new Bl.Services.BaseSongService(new DalMsSql.Repositories.BaseSongRepository(
           //     new SqlConnection(
           //             ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString())));

           //// var song = service.();
        }
    }
}
