using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Savage.SqlServerClient
{
    public class SqlQueryReturningScalar<T> where T : ISqlQuery
    {
        private readonly T _query = default(T);

        public SqlQueryReturningScalar(T query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            _query = query;
        }

        public async Task<object> ExecuteAsync(SqlTransaction transaction, IParameters<T> parameters)
        {
            var broker = new Broker<T>();
            return await broker.ExecuteScalarAsync(transaction, _query, parameters);
        }
    }
}
