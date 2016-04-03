using DalContracts.Models;
using System.Data.Common;
using Dapper;
using System.Linq;
using DalContracts.RepositoriesInterfaces;

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
                                JOIN Genre ON Genre.GenreId=BaseSong.GenreID
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
            return null;
        }

        public int? AddSong(BaseSong song)
        {
            if(song.Singer.SingerId == null)
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

            var songId = Connection
                .Query<int>(@"INSERT INTO BaseSong(BaseSongName, GenreId, SingerId) 
                               VALUES ('" + song.BaseSongName + "'," + song.Genre.GenreId + "," + song.Singer.SingerId + ") " +
                               "SELECT CAST(SCOPE_IDENTITY() as int)")
                .SingleOrDefault();

            if(song.Music != null && song.Music.Count != 0)
            foreach(var music in song.Music)
            {
                Connection.Execute(@"INSERT INTO Music(MusicUrl, BaseSongId) VALUES ('" + music.MusicUrl + "'," + songId + ") " );
            }

            if (song.Video != null && song.Video.Count != 0)
            foreach(var video in song.Video)
            {
                Connection.Execute(@"INSERT INTO Video(VideoUrl, BaseSongId) VALUES ('" + video.VideoUrl + "'," + songId + ") ");
            }

            if (song.Text != null && song.Text.Count != 0)
            foreach(var text in song.Text)
            {
                Connection.Execute(@"INSERT INTO Text(TextContent, BaseSongId) VALUES ('" + text.TextContent + "'," + songId + ") ");
            }

            return songId;
        }
    }
}
