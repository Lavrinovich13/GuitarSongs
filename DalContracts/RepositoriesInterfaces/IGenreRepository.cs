using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts.RepositoriesInterfaces
{
    public interface IGenreRepository
    {
        IList<Genre> GetAllGenres();
        int? GetIdByGenreName(string genreName);
        int AddGenre(Genre genre);
    }
}
