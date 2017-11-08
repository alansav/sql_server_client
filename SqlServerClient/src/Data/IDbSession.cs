using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbSession : IDisposable
    {
        Task<object> ExecuteScalarAsync<T>(T dbCommand) where T : IDbCommand;
        Task<object> ExecuteScalarAsync(string sql, IEnumerable<IDbDataParameter> parameters = null);
        Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null);

        Task<RowsAffectedResultSet> ExecuteNonQueryAsync<T>(T dbCommand) where T : IDbCommand;
        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(string sql, IEnumerable<IDbDataParameter> parameters = null);
        Task<RowsAffectedResultSet> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null);
        
        Task<IEnumerable<IResultSetRow<T>>> ExecuteReaderAsync<T>(T dbCommand, IDataReaderHandler<T> handler) where T : IDbCommand;

        Task ExecuteBatchSql<T>(IEnumerable<T> dbCommands) where T : IDbCommand;
        Task ExecuteBatchSql(IEnumerable<string> sqlStatements);

        void Commit();
        void Rollback();
    }
}
