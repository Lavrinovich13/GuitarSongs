using DalContracts.Models;
using System.Data.Common;
using Dapper;
using System.Linq;
using DalContracts.RepositoriesInterfaces;
using System.Collections.Generic;
using System.Transactions;

namespace DalMsSql.Repositories
{
    public class BaseSongRepository 
        : IBaseSongRepository
    {
        protected DbConnection Connection;

        public BaseSongRepository(DbConnection connection)
        {
            Connection = connection;
        }

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
                    }, splitOn: "SingerId, GenreId")
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
                    }, splitOn: "SingerId, GenreId")
                    .SingleOrDefault();

            if (baseSong == null)
                return baseSong;

            var text = Connection.Query<Text>
                (string.Format(@"SELECT TextId, TextContent, TextName
                                FROM Text
                                WHERE BaseSongId={0}", id))
                    .ToList();

            var video = Connection.Query<Video>
                (string.Format(@"SELECT VideoId, VideoUrl, VideoName
                                FROM Video
                                WHERE BaseSongId={0}", id))
                    .ToList();

            var music = Connection.Query<Music>
                (string.Format(@"SELECT MusicId, MusicUrl, MusicName
                                FROM Music
                                WHERE BaseSongId={0}", id))
                    .ToList();

            baseSong.Text = text;
            baseSong.Video = video;
            baseSong.Music = music;

            return baseSong;
        }

        public int? AddSong(BaseSong song)
        {
            int? songId = null;
            using (var transaction = new TransactionScope())
            {
                if (song.Singer.SingerId == null)
                {
                    song.Singer.SingerId = Connection
                    .Query<int>(@"INSERT INTO Singer(SingerName) 
                               VALUES ('" + song.Singer.SingerName + "') " +
                                   "SELECT CAST(SCOPE_IDENTITY() as int)")
                    .SingleOrDefault();
                }

                if (song.Genre.GenreId == null)
                {
                    song.Genre.GenreId = Connection
                    .Query<int>(@"INSERT INTO Genre(GenreName) 
                               VALUES ('" + song.Genre.GenreName + "') " +
                                   "SELECT CAST(SCOPE_IDENTITY() as int)")
                    .SingleOrDefault();
                }

                    songId = Connection
                    .Query<int>(@"INSERT INTO BaseSong(BaseSongName, GenreId, SingerId, CreationDate) 
                               VALUES ('" + song.BaseSongName + "'," + song.Genre.GenreId + "," + song.Singer.SingerId + ",'" + song.CreationDate.ToString("yyyy-MM-dd HH:mm:ss") + "') " +
                                   "SELECT CAST(SCOPE_IDENTITY() as int)")
                    .SingleOrDefault();

                if (song.Music != null && song.Music.Count != 0)
                    foreach (var music in song.Music)
                    {
                        Connection.Execute(@"INSERT INTO Music(MusicUrl, BaseSongId, MusicName) VALUES ('" + music.MusicUrl + "'," + songId + ",'" + music.MusicName + "')");
                    }

                if (song.Video != null && song.Video.Count != 0)
                    foreach (var video in song.Video)
                    {
                        Connection.Execute(@"INSERT INTO Video(VideoUrl, BaseSongId, VideoName) VALUES ('" + video.VideoUrl + "'," + songId + ",'" + video.VideoName + "')");
                    }

                if (song.Text != null && song.Text.Count != 0)
                    foreach (var text in song.Text)
                    {
                        Connection.Execute(@"INSERT INTO Text(TextContent, BaseSongId, TextName) VALUES ('" + text.TextContent + "'," + songId + ",'" + text.TextName + "')");
                    }

                transaction.Complete();
            }

            return songId;
        }

        public IList<BaseSongInfo> GetRecentSongs(int num)
        {
            var ids =  Connection
                .Query<int>
                (@"SELECT TOP " + num +" BaseSongId FROM BaseSong ORDER BY CreationDate").ToList();

            return ids.Select(x => GetSongInfoById(x)).ToList();
        }

        public IList<BaseSongInfo> GetPopularSongs()
        {
            //check
            var ids = Connection
                .Query<int>
                (@"SELECT TOP 10 BaseSongId FROM 
                    (SELECT BaseSongId, COUNT(UserSongId) AS LinksCount
                     FROM UserSong GROUP BY BaseSongId) ORDER BY LinksCount").ToList();


            return ids.Select(x => GetSongInfoById(x)).ToList();
        }

        public IList<BaseSongInfo> SearchFor(string text)
        {
            var songsIds = Connection.Query<int>(@"SELECT BaseSongId FROM BaseSongView
                        WHERE freetext(*,'" + text + "')").ToList();

            return songsIds.Select(x => GetSongInfoById(x)).ToList();
        }
    }
}
