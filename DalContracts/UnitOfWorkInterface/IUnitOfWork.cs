using DalContracts.RepositoriesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesContract.UnitOfWorkInterface
{
    public interface IUnitOfWork
       : IDisposable
    {
        IBaseSongRepository BaseSongRepository { get; }

        IGenreRepository GenreRepository { get; }

        IMusicRepository MusicRepository { get; }

        ISingerRepository SingerRepository { get; }

        ITextRepository TextRepository { get; }

        IVideoRepository VideoRepository { get; }

        IUserSongRepository UserSongRepository { get; }

        void Commit();
    }
}
