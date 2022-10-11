using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.DataAccess.ConnectionManager
{
    public class DataBaseConnection : IDataBaseConnection
    {
        public DataBaseConnection(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public IDbConnection Connection { get; }
    }
}
