using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Savage.Data.SqlServerClient
{
    public class SqlCommandExecutor : ICommandExecutor
    {
        public  async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand command)
        {
            var rowsAffected = await ((SqlCommand)command).ExecuteNonQueryAsync();

            return new RowsAffectedResultSet(rowsAffected);
        }

        public async Task<object> ExecuteScalarAsync(IDbCommand command)
        {
            var obj = await ((SqlCommand)command).ExecuteScalarAsync();

            return obj;
        }
        
        public async Task<IEnumerable<IResultSetRow<T>>> ExecuteReaderAsync<T>(IDbCommand command, IDataReaderHandler<T> handler) where T : ISqlCommand
        {
            using (var reader = await ((SqlCommand)command).ExecuteReaderAsync())
            {
                return handler.Handle(new OptimizedDataReader(reader));
            }
        }
    }
}
