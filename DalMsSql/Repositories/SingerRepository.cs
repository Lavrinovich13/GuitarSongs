using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalContracts.RepositoriesInterfaces;
using DalContracts.Models;
using Dapper;
using System.Data;

namespace DalMsSql.Repositories
{
    public class SingerRepository 
        :BaseRepository, ISingerRepository
    {
        public SingerRepository(IDbTransaction transaction)
            : base(transaction)
        { }

        public IList<Singer> GetAllSingers()
        {
            var singers = Connection.Query<Singer>(@"SELECT SingerId, SingerName FROM Singer", transaction: Transaction).ToList();
            return singers;
        }

        public int? GetIdBySingerName(string singerName)
        {
            var id = Connection
                .Query<int?>(string.Format(@"SELECT SingerId FROM Singer WHERE upper(SingerName)=upper('{0}')", singerName), transaction: Transaction)
                .SingleOrDefault();
            return id;
        }

        public Singer GetSingerById(int id)
        {
            var singer = Connection
                            .Query<Singer>(string.Format(@"SELECT SingerId, SingerName FROM Singer WHERE SingerId={0}", id), transaction: Transaction)
                            .SingleOrDefault();
            return singer;
        }

        public int AddSinger(Singer singer)
        {
            var singerId = Connection
                .Query<int>(string.Format(@"INSERT INTO Singer(SingerName) VALUES('{0}') SELECT CAST(SCOPE_IDENTITY() as int)", singer.SingerName), transaction: Transaction)
                .SingleOrDefault();
            return singerId;
        }
    }
}
