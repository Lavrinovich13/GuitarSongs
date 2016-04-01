using System.Collections.Generic;

using BlContracts.Models;

namespace BlContracts.ServicesInterfaces
{
    public interface ISingerService
    {
        IList<Singer> GetAllSingers();
    }
}
