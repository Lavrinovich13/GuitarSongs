using BusinesContract.UnitOfWorkInterface;
using DalContracts.RepositoriesInterfaces;
using DalMsSql.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMsSql.UnitOfWork
{
    public class UnitOfWork 
        : IUnitOfWork
    {
        private IBaseSongRepository _baseSongRepository;
        private IGenreRepository _genreRepository;
        private IMusicRepository _musicRepository;
        private ISingerRepository _singerRepository;
        private ITextRepository _textRepository;
        private IVideoRepository _videoRepository;
        private IUserSongRepository _userSongRepository;

        protected IDbConnection Connection;
        protected IDbTransaction Transaction;

        public UnitOfWork(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
            Connection.Open();
            Transaction = Connection.BeginTransaction();
        }

        public void Dispose()
        {
            Transaction.Dispose();
            Connection.Dispose();
        }

        public IBaseSongRepository BaseSongRepository
        {
            get{return _baseSongRepository != null ? _baseSongRepository : _baseSongRepository = new BaseSongRepository(Transaction);}
        }

        public IGenreRepository GenreRepository
        {
            get { return _genreRepository != null ? _genreRepository : _genreRepository = new GenreRepository(Transaction); }
        }

        public IMusicRepository MusicRepository
        {
            get { return _musicRepository != null ? _musicRepository : _musicRepository = new MusicRepository(Transaction); }
        }

        public ISingerRepository SingerRepository
        {
            get { return _singerRepository != null ? _singerRepository : _singerRepository = new SingerRepository(Transaction); }
        }

        public ITextRepository TextRepository
        {
            get { return _textRepository != null ? _textRepository : _textRepository = new TextRepository(Transaction); }
        }

        public IVideoRepository VideoRepository
        {
            get { return _videoRepository != null ? _videoRepository : _videoRepository = new VideoRepository(Transaction); }
        }

        public IUserSongRepository UserSongRepository
        {
            get { return _userSongRepository != null ? _userSongRepository : _userSongRepository = new UserSongRepository(Transaction); }
        }

        public void Commit()
        {
            try
            {
                Transaction.Commit();
            }
            catch
            {
                Transaction.Rollback();
                throw;
            }
            finally
            {
                Transaction.Dispose();
                Transaction = Connection.BeginTransaction();
            }
        }
    }
}
