using BlContracts.ServicesInterfaces;
using DalContracts.RepositoriesInterfaces;

using DalModels = DalContracts.Models;
using BlModels = BlContracts.Models;

using ExpressMapper;

namespace Bl.Services
{
    public class BaseSongService : IBaseSongService
    {
        protected IBaseSongRepository BaseSongRepository;

        public BaseSongService(IBaseSongRepository baseSongRepository)
        {
            BaseSongRepository = baseSongRepository;
        }

        public BlModels.BaseSongInfo GetBaseSongInfoById(int id)
        {
            return Mapper.Map<DalModels.BaseSongInfo, BlModels.BaseSongInfo>(BaseSongRepository.GetSongInfoById(id));
        }

        public int? AddBaseSong(BlModels.BaseSong baseSong)
        {
            return BaseSongRepository.AddSong(Mapper.Map<BlModels.BaseSong, DalModels.BaseSong>(baseSong));
        }
    }
}
