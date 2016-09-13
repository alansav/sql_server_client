using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Savage.SqlServerClient.ResultSets;

namespace Savage.SqlServerClient
{
    public class SqlQueryReturningRowsAffected<T> where T : ISqlQuery
    {
        private readonly T _query = default(T);

        public SqlQueryReturningRowsAffected(T query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            _query = query;
        }

        public async Task<RowsAffectedResultSet> ExecuteAsync(SqlTransaction transaction, IParameters<T> parameters)
        {
            var broker = new Broker<T>();
            return await broker.ExecuteNonQueryAsync(transaction, _query, parameters);
        }
    }
}
