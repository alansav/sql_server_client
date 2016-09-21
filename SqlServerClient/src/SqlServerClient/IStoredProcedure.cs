using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Savage.SqlServerClient
{
    public interface IStoredProcedure
    {
        string StoredProcedureName { get; }
        SqlParameter[] Parameters { get; }
    }
}
