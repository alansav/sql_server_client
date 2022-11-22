using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Savage.Data.SqlServerClient
{
    public class SqlCommandExecutor : ICommandExecutor
    {
        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand command, CancellationToken cancellationToken = default)
        {
            var rowsAffected = await ((SqlCommand)command).ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
            return new RowsAffectedResultSet(rowsAffected);
        }
        
        public async Task<object> ExecuteScalarAsync(IDbCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await ((SqlCommand)command).ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IResultSets> ExecuteReaderAsync(IDbCommand command, IDataReaderHandler handler, CancellationToken cancellationToken = default)
        {
            using (var reader = await ((SqlCommand)command).ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
            {
                return handler.Handle(new OptimizedDataReader(reader));
            }
        }
    }
}
