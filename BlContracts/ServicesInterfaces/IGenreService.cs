using System.Collections.Generic;

using BlContracts.Models;
using BusinesContract.ServicesInterfaces;

namespace BlContracts.ServicesInterfaces
{
    public interface IGenreService
    {
        IServiceResult GetAllGenres();
    }
}
