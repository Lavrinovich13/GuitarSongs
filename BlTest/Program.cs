using System.Configuration;
using System.Data.SqlClient;

namespace BlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Bl.Configuration.MapperConfig.Config();

            var service = new Bl.Services.SingerService(new DalMsSql.Repositories.SingerRepository(
                new SqlConnection(
                        ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString())));

            var song = service.GetAllSingers();
        }
    }
}
