using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Savage.Data.MySqlClient
{
    public class MySqlCommandExecutor : ICommandExecutor
    {
        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand command)
        {
            var rowsAffected = await ((MySqlCommand)command).ExecuteNonQueryAsync();

            return new RowsAffectedResultSet(rowsAffected);
        }

        public async Task<IEnumerable<IResultSetRow<T>>> ExecuteReaderAsync<T>(IDbCommand command, IDataReaderHandler<T> handler) where T : IStoredProcedure
        {
            using (var reader = await ((MySqlCommand)command).ExecuteReaderAsync())
            {
                return handler.Handle(new OptimizedDataReader(reader));
            }
        }

        public async Task<object> ExecuteScalarAsync(IDbCommand command)
        {
            var obj = await ((MySqlCommand)command).ExecuteScalarAsync();

            return obj;
        }
    }
}
