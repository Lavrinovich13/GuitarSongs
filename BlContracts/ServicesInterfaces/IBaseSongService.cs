using BlContracts.Models;

namespace BlContracts.ServicesInterfaces
{
    public interface IBaseSongService
    {
        BaseSongInfo GetBaseSongInfoById(int id);

        int? AddBaseSong(BaseSong baseSong);
    }
}
