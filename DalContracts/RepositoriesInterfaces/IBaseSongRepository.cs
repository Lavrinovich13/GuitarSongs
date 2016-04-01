using DalContracts.Models;

namespace DalContracts.RepositoriesInterfaces
{
    public interface IBaseSongRepository
    {
        BaseSongInfo GetSongInfoById(int id);

        BaseSong GetSongById(int id);

        int? AddSong(BaseSong song);
    }
}
