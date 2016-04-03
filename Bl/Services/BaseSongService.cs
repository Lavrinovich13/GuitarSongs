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
        protected IGenreRepository GenreRepository;
        protected ISingerRepository SingerRepository;

        public BaseSongService(IBaseSongRepository baseSongRepository, IGenreRepository genreRepository, ISingerRepository singerRepository)
        {
            GenreRepository = genreRepository;
            SingerRepository = singerRepository;
            BaseSongRepository = baseSongRepository;
        }

        public BlModels.BaseSongInfo GetBaseSongInfoById(int id)
        {
            return Mapper.Map<DalModels.BaseSongInfo, BlModels.BaseSongInfo>(BaseSongRepository.GetSongInfoById(id));
        }

        public int? AddBaseSong(BlModels.BaseSong baseSong)
        {
            baseSong.Genre.GenreId = GenreRepository.GetIdByGenreName(baseSong.Genre.GenreName);
            baseSong.Singer.SingerId = SingerRepository.GetIdBySingerName(baseSong.Singer.SingerName);

            return BaseSongRepository.AddSong(Mapper.Map<BlModels.BaseSong, DalModels.BaseSong>(baseSong));
        }
    }
}
