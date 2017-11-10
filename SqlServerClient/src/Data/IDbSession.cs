using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbSession : IDisposable
    {
        Task<object> ExecuteScalarAsync(IDbCommand dbCommand);
        Task<object> ExecuteScalarAsync(string sql, IEnumerable<IDbDataParameter> parameters = null);
        Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null);

        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand dbCommand);
        Task<RowsAffectedResultSet> ExecuteNonQueryAsync(string sql, IEnumerable<IDbDataParameter> parameters = null);
        Task<RowsAffectedResultSet> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null);
        
        Task<IEnumerable<IResultSetRow>> ExecuteReaderAsync(IDbCommand dbCommand, IDataReaderHandler handler);

        Task ExecuteBatchSql(IEnumerable<IDbCommand> dbCommands);
        Task ExecuteBatchSql(IEnumerable<string> sqlStatements);

        void Commit();
        void Rollback();
    }
}
