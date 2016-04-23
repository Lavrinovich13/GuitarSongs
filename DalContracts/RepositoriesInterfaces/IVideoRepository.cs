using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts.RepositoriesInterfaces
{
    public interface IVideoRepository
    {
        IList<Video> GetBaseSongVideo(int baseSongId);
        IList<Video> GetUserSongVideo(int userSongId);
        void AddVideoToBaseSong(int baseSongId, Video video);
        void AddToUserVideo(int videoId, int songId);
        void DeleteVideoOfUserSong(int userSongId);
        int? AddUserVideo(Video video, int userSongId);
        void DeleteUserVideo(Video video, int userSongId);
    }
}
