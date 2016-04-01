using System.Collections.Generic;

using BlContracts.ServicesInterfaces;
using DalContracts.RepositoriesInterfaces;

using DalModels = DalContracts.Models;
using BlModels = BlContracts.Models;

using ExpressMapper;

namespace Bl.Services
{
    public class SingerService : ISingerService
    {
        protected ISingerRepository SingerRepository;

        public SingerService(ISingerRepository singerRepository)
        {
            SingerRepository = singerRepository;
        }

        public IList<BlContracts.Models.Singer> GetAllSingers()
        {
            return Mapper.Map<IList<DalModels.Singer>, IList<BlModels.Singer>>(SingerRepository.GetAllSingers());
        }
    }
}
