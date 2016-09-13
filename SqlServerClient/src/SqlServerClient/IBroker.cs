using System.Data.SqlClient;
using System.Threading.Tasks;
using Savage.SqlServerClient.ResultSets;

namespace Savage.SqlServerClient
{
    public interface IBroker<T> where T : ISqlQuery
    {
        Task<object> ExecuteScalarAsync(SqlTransaction transaction, T query, IParameters<T> parameters);
        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(SqlTransaction transaction, T query, IParameters<T> parameters);
        Task<IResultSet<T>> ExecuteDataReaderAsync(SqlTransaction transaction, T query, IParameters<T> parameters, IDataReaderHandler<T> handler);
    }
}
