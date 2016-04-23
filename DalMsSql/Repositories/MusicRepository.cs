using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using DalContracts.RepositoriesInterfaces;

namespace DalMsSql.Repositories
{
    public class MusicRepository
        : BaseRepository, IMusicRepository
    {
        public MusicRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public IList<Music> GetBaseSongMusic(int baseSongId)
        {
            var music = Connection.Query<Music>
                (string.Format(@"SELECT MusicId, MusicUrl, MusicName
                                FROM Music
                                WHERE BaseSongId={0}", baseSongId), transaction: Transaction)
                    .ToList();

            return music;
        }

        public IList<Music> GetUserSongMusic(int userSongId)
        {
            var music = Connection.Query<Music>
                (string.Format(@"SELECT Music.MusicId, MusicUrl, MusicName
                                FROM
                                (SELECT MusicId FROM UserSongMusic
                                WHERE UserSongId={0}) AS m
                                JOIN Music ON Music.MusicId=m.MusicId", userSongId), transaction: Transaction)
                    .ToList();

            return music;
        }

        public void AddMusicToBaseSong(int baseSongId, Music music)
        {
            Connection.Execute(@"INSERT INTO Music(MusicUrl, BaseSongId, MusicName) VALUES ('" + music.MusicUrl + "'," + baseSongId + ",'" + music.MusicName + "')", transaction: Transaction);
        }


        public void AddToUserMusic(int musicId, int songId)
        {
            Connection.Execute(@"INSERT INTO UserSongMusic(UserSongId, MusicId) 
                               VALUES (" + songId + ",'" + musicId + "')", transaction: Transaction);
        }

        public void DeleteMusicOfUserSong(int userSongId)
        {
            var musicsId = GetUserSongMusic(userSongId).Select(x => x.MusicId).ToList();

            if (musicsId != null && musicsId.Count != 0)
            {
                Connection.Execute(string.Format(@"DELETE FROM UserSongMusic WHERE UserSongId={0}", userSongId), transaction: Transaction);
                Connection.Execute(string.Format(@"DELETE FROM Music WHERE MusicId IN ({0}) AND BaseSongId=NULL", string.Join(", ", musicsId.Select(x => x.ToString()).ToArray())), transaction: Transaction);
            }
        }

        public int? AddUserMusic(Music music, int userSongId)
        {
            var musicId = Connection.Query<int>(string.Format(@"INSERT INTO Music(MusicName, MusicUrl) VALUES('{0}', '{1}') SELECT CAST(SCOPE_IDENTITY() as int)",
                music.MusicName, music.MusicUrl),
                transaction: Transaction)
                .SingleOrDefault();

            if (musicId != null)
            {
                var userMusicId = Connection.Query<int>(string.Format(@"INSERT INTO UserSongMusic(MusicId, UserSongId) VALUES({0}, {1}) SELECT CAST(SCOPE_IDENTITY() as int)",
                    musicId, userSongId), transaction: Transaction)
                    .SingleOrDefault();

                return userMusicId;
            }
            return null;
        }

        public void DeleteUserMusic(Music music, int userSongId)
        {
            Connection.Execute(string.Format(@"DELETE FROM UserSongMusic WHERE MusicId={0} AND UserSongId={1}",
                music.MusicId, userSongId), transaction: Transaction);

            Connection.Execute(string.Format(@"DELETE FROM Music WHERE MusicId={0} AND BaseSongId IS NULL",
                music.MusicId), transaction: Transaction);
        }
    }
}
