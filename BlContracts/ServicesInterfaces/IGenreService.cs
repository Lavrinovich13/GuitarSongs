using System.Collections.Generic;

using BlContracts.Models;

namespace BlContracts.ServicesInterfaces
{
    public interface IGenreService
    {
        IList<Genre> GetAllGenres();
    }
}
