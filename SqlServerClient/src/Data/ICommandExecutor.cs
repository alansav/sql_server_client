using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface ICommandExecutor
    {
        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand command, CancellationToken cancellationToken = default(CancellationToken));
        Task<object> ExecuteScalarAsync(IDbCommand command, CancellationToken cancellationToken = default(CancellationToken));
        Task<IResultSets> ExecuteReaderAsync(IDbCommand command, IDataReaderHandler handler, CancellationToken cancellationToken = default(CancellationToken));
    }
}
