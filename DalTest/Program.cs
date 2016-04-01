using DalContracts.Models;
using DalMsSql;
using DalMsSql.Repositories;
using System.Configuration;
using System.Data.SqlClient;

namespace DalTest
{
    class Program
    {

        static void Main(string[] args)
        {
            var repository = 
                new GenreRepository(
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["GuitarDb"].ConnectionString.ToString()));

            //var newSong = new BaseSong() { BaseSongName = "newName", Genre = new Genre() { GenreId = 1 }, Singer = new Singer() { SingerId = 1 } };
            //var songID = repository.AddSong(newSong);

            var genres = repository.GetAllGenres();
        }
    }
}
