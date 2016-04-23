using BlContracts.ServicesInterfaces;
using DalContracts.RepositoriesInterfaces;

using DalModels = DalContracts.Models;
using BlModels = BlContracts.Models;

using ExpressMapper;
using System.Collections.Generic;
using System;
using BusinesContract.UnitOfWorkInterface;
using BusinesContract.ServicesInterfaces;
using System.Data.SqlClient;
using Business.Services;
using BlContracts.Models;

namespace Bl.Services
{
    public class UserSongService
        : IUserSongService
    {
        protected IUnitOfWork UnitOfWork;

        public UserSongService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IServiceResult GetSongsForUser(string userId)
        {
            try
            {
                if (userId == null)
                    return new Result { Success = false, Errors = new[] { "Bad request" } };
                var userSongs = UnitOfWork.UserSongRepository.GetSongsForUser(userId);

                var songs = new List<BlModels.UserSongInfo>();

                foreach(var userSong in userSongs)
                {
                    var baseSong = Mapper.Map<DalModels.BaseSongInfo, BlModels.BaseSongInfo>
                        (UnitOfWork.BaseSongRepository.GetSongInfoById(userSong.BaseSongId));
                    var song = Mapper.Map<DalModels.UserSongInfo, BlModels.UserSongInfo>(userSong);
                    song.BaseSong = baseSong;

                    songs.Add(song);
                }

                return new Result { Success = true, Data = songs };
            }
            catch (SqlException)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult GetSongById(int userSongId)
        {
            try
            {
                var song = Mapper.Map<DalModels.UserSong, BlModels.UserSong>(UnitOfWork.UserSongRepository.GetSongById(userSongId));
                if (song == null)
                    return new Result { Success = false, Errors = new[] { "Song was not found" } };

                song.Music = Mapper.Map<IList<DalModels.Music>, IList<BlModels.Music>>(UnitOfWork.MusicRepository.GetUserSongMusic(song.UserSongId ?? 0));
                song.Video = Mapper.Map<IList<DalModels.Video>, IList<BlModels.Video>>(UnitOfWork.VideoRepository.GetUserSongVideo(song.UserSongId ?? 0));
                song.Text = Mapper.Map<IList<DalModels.Text>, IList<BlModels.Text>>(UnitOfWork.TextRepository.GetUserSongText(song.UserSongId ?? 0));

                return new Result { Success = true, Data = song };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult DeleteUserSong(int userSongId)
        {
            try
            {
                UnitOfWork.VideoRepository.DeleteVideoOfUserSong(userSongId);
                UnitOfWork.MusicRepository.DeleteMusicOfUserSong(userSongId);
                UnitOfWork.TextRepository.DeleteTextOfUserSong(userSongId);

                UnitOfWork.UserSongRepository.DeleteUserSong(userSongId);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = userSongId };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult AddText(Text text, int userSongId)
        {
            try
            {
                var textId = UnitOfWork.TextRepository.AddUserText(Mapper.Map<BlModels.Text, DalModels.Text>(text), userSongId);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = textId };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult UpdateText(Text text, int userSongId)
        {
            try
            {
                UnitOfWork.TextRepository.UpdateUserText(Mapper.Map<BlModels.Text, DalModels.Text>(text), userSongId);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = text.TextId };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult DeleteText(Text text, int userSongId)
        {
            try
            {
                UnitOfWork.TextRepository.DeleteUserText(Mapper.Map<BlModels.Text, DalModels.Text>(text), userSongId);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = text.TextId };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult DeleteVideo(Video video, int userSongId)
        {
            try
            {
                UnitOfWork.VideoRepository.DeleteUserVideo(Mapper.Map<BlModels.Video, DalModels.Video>(video), userSongId);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = video.VideoId };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult AddVideo(Video video, int userSongId)
        {
            try
            {
                var newVideoId = UnitOfWork.VideoRepository.AddUserVideo(Mapper.Map<BlModels.Video, DalModels.Video>(video), userSongId);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = newVideoId };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult AddMusic(Music music, int userSongId)
        {
            try
            {
                var newMusicId = UnitOfWork.MusicRepository.AddUserMusic(Mapper.Map<BlModels.Music, DalModels.Music>(music), userSongId);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = newMusicId };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult DeleteMusic(Music music, int userSongId)
        {
            try
            {
                UnitOfWork.MusicRepository.AddUserMusic(Mapper.Map<BlModels.Music, DalModels.Music>(music), userSongId);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = music.MusicId };
            }
            catch (SqlException ex)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }
    }
}
