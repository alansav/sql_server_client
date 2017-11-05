using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbSession : IDisposable
    {
        Task<object> ExecuteScalarAsync<T>(T sqlCommand) where T : ISqlCommand;

        Task<RowsAffectedResultSet> ExecuteNonQueryAsync<T>(T sqlCommand) where T : ISqlCommand;

        Task<IEnumerable<IResultSetRow<T>>> ExecuteReaderAsync<T>(T sqlCommand, IDataReaderHandler<T> handler) where T : ISqlCommand;

        void Commit();
        void Rollback();
    }
}
