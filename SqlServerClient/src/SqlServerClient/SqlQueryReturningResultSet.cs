using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Savage.SqlServerClient.ResultSets;

namespace Savage.SqlServerClient
{
    public class SqlQueryReturningResultSet<T> where T : ISqlQuery
    {
        private readonly T _query = default(T);
        private readonly IDataReaderHandler<T> _handler = null;

        public SqlQueryReturningResultSet(T query, IDataReaderHandler<T> handler)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            _query = query;
            _handler = handler;
        }

        public async Task<IResultSet<T>> ExecuteAsync(SqlTransaction transaction, IParameters<T> parameters)
        {
            var broker = new Broker<T>();
            return await broker.ExecuteDataReaderAsync(transaction, _query, parameters, _handler);
        }
    }
}
