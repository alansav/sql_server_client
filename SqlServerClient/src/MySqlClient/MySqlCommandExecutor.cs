using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Savage.Data.MySqlClient
{
    public class MySqlCommandExecutor : ICommandExecutor
    {
        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            var rowsAffected = await ((MySqlCommand)command).ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);

            return new RowsAffectedResultSet(rowsAffected);
        }
        
        public async Task<IEnumerable<IResultSetRow>> ExecuteReaderAsync(IDbCommand command, IDataReaderHandler handler, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var reader = await ((MySqlCommand)command).ExecuteReaderAsync(cancellationToken).ConfigureAwait(false))
            {
                return handler.Handle(new OptimizedDataReader(reader));
            }
        }

        public async Task<object> ExecuteScalarAsync(IDbCommand command, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await ((MySqlCommand)command).ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
