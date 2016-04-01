using ExpressMapper;
using BlModels = BlContracts.Models;
using DalModels = DalContracts.Models;

namespace Bl.Configuration
{
    public class MapperConfig
    {
        public static void Config()
        {
            Mapper.Register<DalModels.BaseSongInfo, BlModels.BaseSongInfo>();
            Mapper.Register<DalModels.Genre, BlModels.Genre>();
            Mapper.Register<DalModels.Singer, BlModels.Singer>();
            Mapper.Register<DalModels.Music, BlModels.Music>();
            Mapper.Register<DalModels.Video, BlModels.Video>();
            Mapper.Register<DalModels.Text, BlModels.Text>();
            Mapper.Register<DalModels.BaseSong, BlModels.BaseSong>();

            Mapper.Register<BlModels.BaseSongInfo, DalModels.BaseSongInfo>();
            Mapper.Register<BlModels.Genre, DalModels.Genre>();
            Mapper.Register<BlModels.Singer, DalModels.Singer>();
            Mapper.Register<BlModels.Music, DalModels.Music>();
            Mapper.Register<BlModels.Video, DalModels.Video>();
            Mapper.Register<BlModels.Text, DalModels.Text>();
            Mapper.Register<BlModels.BaseSong, DalModels.BaseSong>();
        }
    }
}
