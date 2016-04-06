using BlContracts.ServicesInterfaces;
using DalContracts.RepositoriesInterfaces;

using DalModels = DalContracts.Models;
using BlModels = BlContracts.Models;

using ExpressMapper;
using System.Collections.Generic;
using System;

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
            baseSong.CreationDate = DateTime.Now;
            baseSong.Genre.GenreId = GenreRepository.GetIdByGenreName(baseSong.Genre.GenreName);
            baseSong.Singer.SingerId = SingerRepository.GetIdBySingerName(baseSong.Singer.SingerName);

            return BaseSongRepository.AddSong(Mapper.Map<BlModels.BaseSong, DalModels.BaseSong>(baseSong));
        }

        public IList<BlModels.BaseSongInfo> GetRecentSongs()
        {
            return Mapper.Map<IList<DalModels.BaseSongInfo>, IList<BlModels.BaseSongInfo>>
                (BaseSongRepository.GetRecentSongs());
        }


        public BlModels.BaseSong GetSongById(int id)
        {
            return Mapper.Map<DalModels.BaseSong, BlModels.BaseSong>(BaseSongRepository.GetSongById(id));
        }
    }
}
