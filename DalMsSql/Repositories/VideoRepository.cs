using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.Common;
using DalContracts.Models;
using System.Data;
using DalContracts.RepositoriesInterfaces;

namespace DalMsSql.Repositories
{
    public class VideoRepository
        : BaseRepository, IVideoRepository
    {
        public VideoRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public IList<Video> GetBaseSongVideo(int baseSongId)
        {
            var video = Connection.Query<Video>
                (string.Format(@"SELECT VideoId, VideoUrl, VideoName
                                FROM Video
                                WHERE BaseSongId={0}", baseSongId), transaction: Transaction)
                    .ToList();

            return video;
        }

        public void AddVideoToBaseSong(int baseSongId, Video video)
        {
            Connection.Execute(@"INSERT INTO Video(VideoUrl, BaseSongId, VideoName) VALUES ('" + video.VideoUrl + "'," + baseSongId + ",'" + video.VideoName + "')", transaction: Transaction);
        }

        public void AddToUserVideo(int videoId, int songId)
        {
            Connection.Execute(@"INSERT INTO UserSongVideo(UserSongId, VideoId) 
                               VALUES (" + songId + ",'" + videoId + "')", transaction: Transaction);
        }

        public IList<Video> GetUserSongVideo(int userSongId)
        {
            var video = Connection.Query<Video>
                (string.Format(@"SELECT Video.VideoId, VideoUrl, VideoName
                                FROM
                                (SELECT VideoId FROM UserSongVideo
                                WHERE UserSongId={0}) AS v
                                JOIN Video ON Video.VideoId=v.VideoId", userSongId), transaction: Transaction)
                    .ToList();

            return video;
        }

        public void DeleteVideoOfUserSong(int userSongId)
        {
            var songsId = GetUserSongVideo(userSongId).Select(x => x.VideoId).ToList();

            if (songsId != null && songsId.Count != 0)
            {
                Connection.Execute(string.Format(@"DELETE FROM UserSongVideo WHERE UserSongId={0}", userSongId), transaction: Transaction);
                Connection.Execute(string.Format(@"DELETE FROM Video WHERE VideoId IN ({0}) AND BaseSongId=NULL", string.Join(", ", songsId.Select(x => x.ToString()).ToArray())), transaction: Transaction);
            }
        }

        public int? AddUserVideo(Video video, int userSongId)
        {
            var videoId = Connection.Query<int>(string.Format(@"INSERT INTO Video(VideoName, VideoUrl) VALUES('{0}', '{1}') SELECT CAST(SCOPE_IDENTITY() as int)",
                video.VideoName, video.VideoUrl),
                transaction: Transaction)
                .SingleOrDefault();

            if (videoId != null)
            {
                var userVideoId = Connection.Query<int>(string.Format(@"INSERT INTO UserSongVideo(VideoId, UserSongId) VALUES({0}, {1}) SELECT CAST(SCOPE_IDENTITY() as int)",
                    videoId, userSongId), transaction: Transaction)
                    .SingleOrDefault();

                return userVideoId;
            }
            return null;
        }

        public void DeleteUserVideo(Video video, int userSongId)
        {
            Connection.Execute(string.Format(@"DELETE FROM UserSongVideo WHERE VideoId={0} AND UserSongId={1}",
                video.VideoId, userSongId), transaction: Transaction);

            Connection.Execute(string.Format(@"DELETE FROM Video WHERE VideoId={0} AND BaseSongId IS NULL",
                video.VideoId), transaction: Transaction);
        }
    }
}
