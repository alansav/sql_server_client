using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbSession : IDisposable
    {
        Task<object> ExecuteScalarAsync<T>(T storedProcedure) where T : IStoredProcedure;

        Task<RowsAffectedResultSet> ExecuteNonQueryAsync<T>(T storedProcedure) where T : IStoredProcedure;

        Task<IEnumerable<IResultSetRow<T>>> ExecuteReaderAsync<T>(T storedProcedure, IDataReaderHandler<T> handler) where T : IStoredProcedure;

        void Commit();
        void Rollback();
    }
}
