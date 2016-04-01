using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using DalContracts.Models;
using DalContracts.RepositoriesInterfaces;

namespace DalMsSql.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        protected DbConnection Connection;

        public GenreRepository(DbConnection connection)
        {
            Connection = connection;
        }

        public IList<Genre> GetAllGenres()
        {
            var genres = Connection.Query<Genre>(@"SELECT GenreId, GenreName FROM Genre").ToList();
            return genres;
        }
    }
}
