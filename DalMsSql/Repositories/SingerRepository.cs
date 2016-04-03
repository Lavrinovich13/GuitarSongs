using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalContracts.RepositoriesInterfaces;
using DalContracts.Models;
using Dapper;

namespace DalMsSql.Repositories
{
    public class SingerRepository : ISingerRepository
    {
        protected DbConnection Connection;

        public SingerRepository(DbConnection connection)
        {
            Connection = connection;
        }

        public IList<Singer> GetAllSingers()
        {
            var singers = Connection.Query<Singer>(@"SELECT SingerId, SingerName FROM Singer").ToList();
            return singers;
        }

        public int? GetIdBySingerName(string singerName)
        {
            var id = Connection
                .Query<int?>(string.Format(@"SELECT SingerId FROM Singer WHERE upper(SingerName)=upper('{0}')", singerName))
                .SingleOrDefault();
            return id;
        }
    }
}
