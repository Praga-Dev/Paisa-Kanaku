using System.Data;

namespace Praga.PaisaKanaku.Core.DataAccess.ConnectionManager
{
    public interface IDataBaseConnection
    {
        IDbConnection Connection { get; }
    }
}
