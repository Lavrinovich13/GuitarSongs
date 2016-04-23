using DalContracts.Models;
using System.Data.Common;
using Dapper;
using System.Linq;
using DalContracts.RepositoriesInterfaces;
using System.Collections.Generic;
using System.Transactions;
using System.Data;

namespace DalMsSql.Repositories
{
    public class BaseSongRepository 
        : BaseRepository, IBaseSongRepository
    {
        public BaseSongRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public BaseSongInfo GetSongInfoById(int id)
        {
            return Connection
                .Query<BaseSongInfo, Singer, Genre, BaseSongInfo>
                (string.Format(@"SELECT BaseSongId, BaseSongName,
                                   (SELECT COUNT(MusicId) FROM Music WHERE BaseSongId={0}) AS MusicNum,
                                   (SELECT COUNT(VideoId) FROM Video WHERE BaseSongId={0}) AS VideoNum,
                                   (SELECT COUNT(TextId) FROM Text WHERE BaseSongId={0}) AS TextNum,
                                   (SELECT COUNT(UserSongId) FROM UserSong WHERE BaseSongId={0}) AS LinkNum,
                                   Singer.SingerId, SingerName, 
                                   Genre.GenreId, GenreName 
                                FROM BaseSong
                                JOIN Singer ON BaseSong.SingerId=Singer.SingerId
                                JOIN Genre ON Genre.GenreId=BaseSong.GenreId
                                WHERE BaseSongId={0}", id),
                    (song, singer, genre) =>
                    {
                        song.Genre = genre;
                        song.Singer = singer;
                        return song;
                    }, splitOn: "SingerId, GenreId", transaction: Transaction)
                    .SingleOrDefault();
        }

        public BaseSong GetSongById(int id)
        {
            var baseSong = Connection.Query<BaseSong, Singer, Genre, BaseSong>
                (string.Format(@"SELECT BaseSongId, BaseSongName,
                                   Singer.SingerId, SingerName, 
                                   Genre.GenreId, GenreName 
                                FROM BaseSong
                                JOIN Singer ON BaseSong.SingerId=Singer.SingerId
                                JOIN Genre ON Genre.GenreId=BaseSong.GenreId
                                WHERE BaseSongId={0}", id),
                    (song, singer, genre) =>
                    {
                        song.Genre = genre;
                        song.Singer = singer;
                        return song;
                    }, splitOn: "SingerId, GenreId", transaction: Transaction)
                    .SingleOrDefault();

            return baseSong;
        }

        public int? AddSong(BaseSong song)
        {
            int? songId = null;

            songId = Connection
            .Query<int>(@"INSERT INTO BaseSong(BaseSongName, GenreId, SingerId, CreationDate) 
                        VALUES ('" + song.BaseSongName + "'," + song.Genre.GenreId + "," + song.Singer.SingerId + ",'" + song.CreationDate.ToString() + "') " +
                            "SELECT CAST(SCOPE_IDENTITY() as int)", transaction: Transaction)
            .SingleOrDefault();

            return songId;
        }

        public IList<BaseSongInfo> GetRecentSongs(int num)
        {
            var ids =  Connection
                .Query<int>
                (@"SELECT TOP " + num + " BaseSongId FROM BaseSong ORDER BY CreationDate", transaction: Transaction).ToList();

            return ids.Select(x => GetSongInfoById(x)).ToList();
        }

//        public IList<BaseSongInfo> GetPopularSongs()
//        {
//            //check
//            var ids = Connection
//                .Query<int>
//                (@"SELECT TOP 10 BaseSongId FROM 
//                    (SELECT BaseSongId, COUNT(UserSongId) AS LinksCount
//                     FROM UserSong GROUP BY BaseSongId) ORDER BY LinksCount").ToList();


//            return ids.Select(x => GetSongInfoById(x)).ToList();
//        }

        public IList<BaseSongInfo> SearchFor(string text)
        {
            var songsIds = Connection.Query<int>(@"SELECT BaseSongId FROM BaseSongView
                        WHERE freetext(*,'" + text + "')", transaction: Transaction).ToList();

            return songsIds.Select(x => GetSongInfoById(x)).ToList();
        }

        public int AddBaseSongToFavorite(string userId, int baseSongId)
        {
            var newSongId = Connection.Query<int>(@"INSERT INTO UserSong(BaseSongId, UserId) 
                               VALUES (" + baseSongId + ",'" + userId + "') SELECT CAST(SCOPE_IDENTITY() as int)", transaction: Transaction)
                    .SingleOrDefault();

            return newSongId;
        }


        public bool IsUserHasSong(string userId, int baseSongId)
        {
            var songId = Connection
                .Query<int>(string.Format(@"SELECT UserSongId FROM UserSong WHERE UserId='{0}' AND BaseSongId={1}", userId, baseSongId),
                transaction: Transaction).SingleOrDefault();

            return songId == null;
        }
    }
}
