using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    // ReSharper disable once UnusedTypeParameter
    public interface IParameters<T> where T : ISqlQuery
    {
        SqlParameter[] ToSqlParameters();
    }
}
