using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public interface ISqlQuery
    {
        SqlCommand CreateSqlCommand();
    }
}
