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

        public int? GetIdByGenreName(string genreName)
        {
            var id = Connection
                .Query<int?>(string.Format(@"SELECT GenreId FROM Genre WHERE upper(GenreName)=upper('{0}')", genreName))
                .SingleOrDefault();
            return id;
        }
    }
}
