using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbSession : IDisposable
    {
        Task<object> ExecuteScalarAsync(IDbCommand dbCommand, CancellationToken cancellationToken = default(CancellationToken));
        Task<object> ExecuteScalarAsync(string sql, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default(CancellationToken));

        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand dbCommand, CancellationToken cancellationToken = default(CancellationToken));
        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(string sql, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<RowsAffectedResultSet> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default(CancellationToken));
        
        Task<IEnumerable<IResultSetRow>> ExecuteReaderAsync(IDbCommand dbCommand, IDataReaderHandler handler, CancellationToken cancellationToken = default(CancellationToken));

        Task ExecuteBatchSqlAsync(IEnumerable<IDbCommand> dbCommands, CancellationToken cancellationToken = default(CancellationToken));
        Task ExecuteBatchSqlAsync(IEnumerable<string> sqlStatements, CancellationToken cancellationToken = default(CancellationToken));

        void Commit();
        void Rollback();
    }
}
