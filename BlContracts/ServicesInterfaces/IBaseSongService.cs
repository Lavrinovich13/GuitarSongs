using BlContracts.Models;
using System.Collections.Generic;

namespace BlContracts.ServicesInterfaces
{
    public interface IBaseSongService
    {
        BaseSongInfo GetBaseSongInfoById(int id);

        int? AddBaseSong(BaseSong baseSong);

        IList<BaseSongInfo> GetRecentSongs();

        BaseSong GetSongById(int id);
    }
}
