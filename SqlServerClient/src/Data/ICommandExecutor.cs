using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface ICommandExecutor
    {
        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand command);
        Task<object> ExecuteScalarAsync(IDbCommand command);
        Task<IEnumerable<IResultSetRow>> ExecuteReaderAsync(IDbCommand command, IDataReaderHandler handler);
    }
}
