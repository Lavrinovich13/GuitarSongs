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

namespace Bl.Services
{
    public class BaseSongService : IBaseSongService
    {
        protected const int SONG_PORTION = 20;
        protected IUnitOfWork UnitOfWork;

        public BaseSongService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IServiceResult GetBaseSongInfoById(int id)
        {
            try
            {
                var songs = Mapper.Map<DalModels.BaseSongInfo, BlModels.BaseSongInfo>
                (UnitOfWork.BaseSongRepository.GetSongInfoById(id));

                return new Result { Success = true, Data = songs };
            }
            catch(SqlException)
            {
                return new Result { Success = false, Errors = new[] { "Song was not found" } };
            }
        }

        public IServiceResult AddBaseSong(string userId, BlModels.BaseSong baseSong)
        {
            if (baseSong == null)
                return new Result { Success = false, Errors = new[] { "Bad request" } };

            try
            {
                baseSong.CreationDate = DateTime.Now;
                baseSong.Genre.GenreId = UnitOfWork.GenreRepository.GetIdByGenreName(baseSong.Genre.GenreName);
                if (baseSong.Genre.GenreId == null)
                {
                    baseSong.Genre.GenreId = UnitOfWork.GenreRepository
                        .AddGenre(Mapper.Map<BlModels.Genre, DalModels.Genre>(baseSong.Genre));
                }

                baseSong.Singer.SingerId = UnitOfWork.SingerRepository.GetIdBySingerName(baseSong.Singer.SingerName);
                if (baseSong.Singer.SingerId == null)
                {
                    baseSong.Singer.SingerId = UnitOfWork.SingerRepository
                        .AddSinger(Mapper.Map<BlModels.Singer, DalModels.Singer>(baseSong.Singer));
                }

                var newSongId = UnitOfWork.BaseSongRepository.AddSong(Mapper.Map<BlModels.BaseSong, DalModels.BaseSong>(baseSong));

                if (newSongId == null)
                    return new Result { Success = false, Errors = new[] { "Database is not available" } };

                if (baseSong.Music != null)
                {
                    foreach (var music in baseSong.Music)
                    {
                        UnitOfWork.MusicRepository.AddMusicToBaseSong(newSongId ?? 0, Mapper.Map<BlModels.Music, DalModels.Music>(music));
                    }
                }
                if (baseSong.Video != null)
                {
                    foreach (var video in baseSong.Video)
                    {
                        UnitOfWork.VideoRepository.AddVideoToBaseSong(newSongId ?? 0, Mapper.Map<BlModels.Video, DalModels.Video>(video));
                    }
                }
                if (baseSong.Text != null)
                {
                    foreach (var text in baseSong.Text)
                    {
                        UnitOfWork.TextRepository.AddTextToBaseSong(newSongId ?? 0, Mapper.Map<BlModels.Text, DalModels.Text>(text));
                    }
                }

                AddBaseSongToFavorite(userId, newSongId??0);

                UnitOfWork.Commit();

                return new Result { Success = true, Data = newSongId };
            }
            catch (Exception)
            {
                return new Result { Success = false, Errors = new[] { "Problems with BaseSong service" } };
            }
        }

        public IServiceResult GetRecentSongs()
        {
            try
            {
                var songs = Mapper.Map<IList<DalModels.BaseSongInfo>, IList<BlModels.BaseSongInfo>>
                    (UnitOfWork.BaseSongRepository.GetRecentSongs(SONG_PORTION));

                return new Result { Success = true, Data = songs };
            }
            catch(Exception ex)
            {
                return new Result { Success = false, Errors = new[] { "Problems with BaseSong service" } };
            }
        }

        public IServiceResult GetSongById(int id)
        {
            try
            {
                var song  = Mapper.Map<DalModels.BaseSong, BlModels.BaseSong>(UnitOfWork.BaseSongRepository.GetSongById(id));
                if(song == null)
                    return new Result { Success = false, Errors = new[] { "Song was not found" } };

                song.Music = Mapper.Map<IList<DalModels.Music>, IList<BlModels.Music>>(UnitOfWork.MusicRepository.GetBaseSongMusic(song.BaseSongId??0));
                song.Video = Mapper.Map<IList<DalModels.Video>, IList<BlModels.Video>>(UnitOfWork.VideoRepository.GetBaseSongVideo(song.BaseSongId??0));
                song.Text = Mapper.Map<IList<DalModels.Text>, IList<BlModels.Text>>(UnitOfWork.TextRepository.GetBaseSongText(song.BaseSongId??0));

                return new Result { Success = true, Data = song };
            }
            catch(Exception ex)
            {
                return new Result { Success = false, Errors = new[] { "Problems with BaseSong service" } };
            }
        }

        public IServiceResult SearchFor(string text)
        {
            try 
            {
                if(text == null)
                    return new Result { Success = false, Errors = new[] { "Bad request" } };

                var songs = Mapper.Map<IList<DalModels.BaseSongInfo>, IList<BlModels.BaseSongInfo>>
                (UnitOfWork.BaseSongRepository.SearchFor(text));

                return new Result{Success = true, Data = songs};
            }
            catch(Exception ex)
            {
                return new Result { Success = false, Errors = new[] { "Problems with BaseSong service" } };
            }
        }

        public IServiceResult AddBaseSongToFavorite(string userId, int baseSongId)
        {
            try
            {
                if(userId == null)
                    return new Result { Success = false, Errors = new[] { "Bad request" } };

                if(UnitOfWork.BaseSongRepository.IsUserHasSong(userId, baseSongId))
                    return new Result { Success = false, Errors = new[] { "User already has such song" } };

                var songId = UnitOfWork.BaseSongRepository.AddBaseSongToFavorite(userId, baseSongId);

                if(songId == null)
                    return new Result { Success = false, Errors = new[] { "Song was not found" } };

                var baseSongVideo = UnitOfWork.VideoRepository.GetBaseSongVideo(baseSongId);
                if (baseSongVideo != null)
                {
                    foreach(var video in baseSongVideo)
                    {
                        UnitOfWork.VideoRepository.AddToUserVideo(video.VideoId??0, songId);
                    }
                }
                var baseSongMusic = UnitOfWork.MusicRepository.GetBaseSongMusic(baseSongId);
                if (baseSongMusic != null)
                {
                    foreach (var music in baseSongMusic)
                    {
                        UnitOfWork.MusicRepository.AddToUserMusic(music.MusicId??0, songId);
                    }
                }
                var baseSongText = UnitOfWork.TextRepository.GetBaseSongText(baseSongId);
                if (baseSongText != null)
                {
                    foreach (var text in baseSongText)
                    {
                        UnitOfWork.TextRepository.AddToUserText(text.TextId??0, songId);
                    }
                }
                UnitOfWork.Commit();

                return new Result { Success = true, Data = songId };
            }
            catch(Exception ex)
            {
                return new Result { Success = false, Errors = new[] { "Problems with BaseSong service" } };
            }
        }
    }
}
