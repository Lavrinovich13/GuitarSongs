using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;
using DalContracts.Models;
using DalContracts.RepositoriesInterfaces;
using System.Data;

namespace DalMsSql.Repositories
{
    public class GenreRepository 
        : BaseRepository, IGenreRepository
    {
        public GenreRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public IList<Genre> GetAllGenres()
        {
            var genres = Connection.Query<Genre>(@"SELECT GenreId, GenreName FROM Genre", transaction: Transaction).ToList();
            return genres;
        }

        public int? GetIdByGenreName(string genreName)
        {
            var id = Connection
                .Query<int?>(string.Format(@"SELECT GenreId FROM Genre WHERE upper(GenreName)=upper('{0}')", genreName), transaction: Transaction)
                .SingleOrDefault();
            return id;
        }

        public Genre GetGenreById(int id)
        {
            var genre = Connection
                .Query<Genre>(string.Format(@"SELECT GenreId, GenreName FROM Genre WHERE GenreId={0}", id), transaction: Transaction)
                .SingleOrDefault();
            return genre;
        }

        public int AddGenre(Genre genre)
        {
            var genreId = Connection
                .Query<int>(string.Format(@"INSERT INTO Genre(GenreName) VALUES('{0}') SELECT CAST(SCOPE_IDENTITY() as int)", genre.GenreName), transaction: Transaction)
                .SingleOrDefault();
            return genreId;
        }
    }
}
