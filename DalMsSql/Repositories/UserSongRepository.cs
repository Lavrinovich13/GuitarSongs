using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DalContracts.RepositoriesInterfaces;

namespace DalMsSql.Repositories
{
    public class UserSongRepository
        : BaseRepository, IUserSongRepository
    {
        public UserSongRepository(IDbTransaction transaction)
            : base(transaction)
        { }


        public IList<UserSongInfo> GetSongsForUser(string userId)
        {
            var usersSongs = Connection
                .Query<UserSongInfo>(string.Format(@"SELECT UserSongId, BaseSongId, IsReady FROM UserSong WHERE UserId='{0}'", userId), transaction: Transaction)
                .ToList();

            return usersSongs;
        }

        public UserSong GetSongById(int userSongId)
        {
            var userSong = Connection
                .Query<UserSong, Singer, Genre, UserSong>
                 (string.Format(@"SELECT
	                                    UserSongId, BaseSongName,
                                        Singer.SingerId, SingerName, 
                                        Genre.GenreId, GenreName 
                                FROM
                                (SELECT UserSongId
                                      ,BaseSong.BaseSongId
	                                  ,BaseSongName
                                      ,IsReady
	                                  ,SingerId
	                                  ,GenreId
                                FROM
                                (SELECT UserSongId
                                      ,BaseSongId
                                      ,IsReady
                                  FROM UserSong
                                  WHERE UserSongId={0}) AS u
                                  JOIN BaseSong ON BaseSong.BaseSongId=u.BaseSongId) AS s
                                  JOIN Genre ON Genre.GenreId=s.GenreId
                                  JOIN Singer ON Singer.SingerId=s.SingerId
                                ", userSongId),
                    (song, singer, genre) =>
                    {
                        song.Genre = genre;
                        song.Singer = singer;
                        return song;
                    }, splitOn: "SingerId, GenreId", transaction: Transaction)
                .SingleOrDefault();

            return userSong;
        }

        public void DeleteUserSong(int userSongId)
        {
            Connection.Execute(string.Format(@"DELETE FROM UserSong WHERE UserSongId={0}", userSongId)
                , transaction: Transaction);
        }
    }
}
