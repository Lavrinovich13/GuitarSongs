using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts.RepositoriesInterfaces
{
    public interface ITextRepository
    {
        IList<Text> GetBaseSongText(int baseSongId);
        IList<Text> GetUserSongText(int userSongId);
        void AddTextToBaseSong(int baseSongId, Text text);
        void AddToUserText(int textId, int songId);
        void DeleteTextOfUserSong(int userSongId);
        int? AddUserText(Text text, int userSongId);
        void UpdateUserText(Text text, int userSongId);
        void DeleteUserText(Text text, int userSongId);
    }
}
