using System.Threading.Tasks;

namespace Savage.SqlServerClient
{
    public interface ICommandExecutor
    {
        Task<RowsAffectedResultSet> ExecuteNonQueryAsync();
        Task<object> ExecuteScalarAsync();
        Task<IResultSet> ExecuteReaderAsync(IDataReaderHandler handler);
    }
}
