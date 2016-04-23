using DalContracts.Models;
using System.Collections.Generic;

namespace DalContracts.RepositoriesInterfaces
{
    public interface IBaseSongRepository
    {
        BaseSongInfo GetSongInfoById(int id);
        BaseSong GetSongById(int id);
        int? AddSong(BaseSong song);
        IList<BaseSongInfo> GetRecentSongs(int num);
        IList<BaseSongInfo> SearchFor(string text);
        int AddBaseSongToFavorite(string userId, int baseSongId);

        bool IsUserHasSong(string userId, int baseSongId);
    }
}
