using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbSession : IDisposable
    {
        Task<object> ExecuteScalarAsync<T>(T dbCommand) where T : IDbCommand;

        Task<RowsAffectedResultSet> ExecuteNonQueryAsync<T>(T dbCommand) where T : IDbCommand;

        Task<IEnumerable<IResultSetRow<T>>> ExecuteReaderAsync<T>(T dbCommand, IDataReaderHandler<T> handler) where T : IDbCommand;

        Task ExecuteBatchSql<T>(IEnumerable<T> dbCommands) where T : IDbCommand;

        void Commit();
        void Rollback();
    }
}
