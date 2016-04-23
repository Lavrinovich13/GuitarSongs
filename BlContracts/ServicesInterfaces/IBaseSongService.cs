using BlContracts.Models;
using BusinesContract.ServicesInterfaces;
using System.Collections.Generic;

namespace BlContracts.ServicesInterfaces
{
    public interface IBaseSongService
    {
        IServiceResult GetBaseSongInfoById(int id);

        IServiceResult AddBaseSong(string userId, BaseSong baseSong);

        IServiceResult GetRecentSongs();

        IServiceResult GetSongById(int id);

        IServiceResult SearchFor(string text);

        IServiceResult AddBaseSongToFavorite(string userId, int baseSongId);
    }
}
