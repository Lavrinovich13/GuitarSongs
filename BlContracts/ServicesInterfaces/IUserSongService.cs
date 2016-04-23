using BlContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesContract.ServicesInterfaces
{
    public interface IUserSongService
    {
        IServiceResult GetSongsForUser(string userId);
        IServiceResult GetSongById(int userSongId);
        IServiceResult DeleteUserSong(int userSongId);
        IServiceResult AddText(Text text, int userSongId);
        IServiceResult DeleteText(Text text, int userSongId);
        IServiceResult UpdateText(Text text, int userSongId);
        IServiceResult DeleteVideo(Video video, int userSongId);
        IServiceResult AddVideo(Video video, int userSongId);
        IServiceResult AddMusic(Music music, int userSongId);
        IServiceResult DeleteMusic(Music music, int userSongId);
    }
}
