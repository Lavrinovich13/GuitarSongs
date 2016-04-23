using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts.RepositoriesInterfaces
{
    public interface IMusicRepository
    {
        IList<Music> GetBaseSongMusic(int baseSongId);
        IList<Music> GetUserSongMusic(int userSongId);
        void AddMusicToBaseSong(int baseSongId, Music music);
        void AddToUserMusic(int musicId, int songId);
        void DeleteMusicOfUserSong(int userSongId);
        int? AddUserMusic(Music music, int userSongId);
        void DeleteUserMusic(Music music, int userSongId);
    }
}
