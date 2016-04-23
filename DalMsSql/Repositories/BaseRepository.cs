using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMsSql.Repositories
{
    public class BaseRepository
    {
        protected IDbConnection Connection;
        protected IDbTransaction Transaction { get; set; }

        protected BaseRepository(IDbTransaction transaction)
        {
            Transaction = transaction;
            Connection = Transaction.Connection;
        }
    }
}
