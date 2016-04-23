using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts.RepositoriesInterfaces
{
    public interface IUserSongRepository
    {
        IList<UserSongInfo> GetSongsForUser(string userId);
        UserSong GetSongById(int userSongId);
        void DeleteUserSong(int userSongId);
    }
}
