using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbSession : IDisposable
    {
        Task<object> ExecuteScalarAsync(IDbCommand dbCommand, CancellationToken cancellationToken = default);
        Task<object> ExecuteScalarAsync(string sql, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default);
        Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default);

        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand dbCommand, CancellationToken cancellationToken = default);
        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(string sql, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default);
        Task<RowsAffectedResultSet> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default);
        
        Task<IResultSets> ExecuteReaderAsync(IDbCommand dbCommand, IDataReaderHandler handler, CancellationToken cancellationToken = default);

        Task ExecuteBatchSqlAsync(IEnumerable<IDbCommand> dbCommands, CancellationToken cancellationToken = default);
        Task ExecuteBatchSqlAsync(IEnumerable<string> sqlStatements, CancellationToken cancellationToken = default);
        Task ExecuteBatchSqlAsync(string sql, CancellationToken cancellationToken = default);

        void Commit();
        void Rollback();
    }
}
