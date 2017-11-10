using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Savage.Data
{
    public class DbSession : IDbSession
    {
        private IDbTransaction _transaction;
        private readonly IDbClient DbClient;
        private readonly IDbConnection DbConnection;

        public DbSession(IDbClient dbClient, IDbConnection dbConnection)
        {
            DbClient = dbClient;
            DbConnection = dbConnection;
        }

        #region "Disposing"
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CloseConnection();
            }
        }
        #endregion

        private async Task OpenConnectionIfClosed()
        {
            if (DbConnection.State == ConnectionState.Closed)
                await DbClient.OpenConnectionAsync(DbConnection);
        }

        private void AddCommandToTransaction(IDbCommand command)
        {
            if (_transaction == null)
                _transaction = DbConnection.BeginTransaction();

            command.Transaction = _transaction;
            command.Connection = _transaction.Connection;
        }

        private async Task PrepareCommand(IDbCommand dbCommand)
        {
            SetNullParametersToDbNull(dbCommand);
            await OpenConnectionIfClosed();
            AddCommandToTransaction(dbCommand);
        }

        public async Task<IEnumerable<IResultSetRow>> ExecuteReaderAsync(IDbCommand dbCommand, IDataReaderHandler handler)
        {
            await PrepareCommand(dbCommand);
            return await DbClient.CommandExecutor.ExecuteReaderAsync(dbCommand, handler);
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand dbCommand)
        {
            await PrepareCommand(dbCommand);
            return await DbClient.CommandExecutor.ExecuteNonQueryAsync(dbCommand);
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(string sql, IEnumerable<IDbDataParameter> parameters = null)
        {
            var dbCommand = CreateCommand(sql, parameters);
            return await ExecuteNonQueryAsync(dbCommand);
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null)
        {
            var dbCommand = CreateCommand(storedProcedureName, parameters);
            dbCommand.CommandType = CommandType.StoredProcedure;
            return await ExecuteNonQueryAsync(dbCommand);
        }

        public async Task<object> ExecuteScalarAsync(IDbCommand dbCommand)
        {
            await PrepareCommand(dbCommand);
            return await DbClient.CommandExecutor.ExecuteScalarAsync(dbCommand);
        }

        public async Task<object> ExecuteScalarAsync(string sql, IEnumerable<IDbDataParameter> parameters = null)
        {
            var dbCommand = CreateCommand(sql, parameters);
            return await ExecuteScalarAsync(dbCommand);
        }

        public async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null)
        {
            var dbCommand = CreateCommand(storedProcedureName, parameters);
            dbCommand.CommandType = CommandType.StoredProcedure;
            return await ExecuteScalarAsync(dbCommand);
        }

        public async Task ExecuteBatchSql(IEnumerable<IDbCommand> dbCommands)
        {
            foreach (var dbCommand in dbCommands)
            {
                await ExecuteNonQueryAsync(dbCommand);
            }
        }

        public async Task ExecuteBatchSql(IEnumerable<string> sqlStatements)
        {
            foreach (var sql in sqlStatements)
            {
                await ExecuteNonQueryAsync(sql, null);
            }
        }

        public void Commit()
        {
            _transaction.Commit();
            CloseConnection();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            CloseConnection();
        }

        private IDbCommand CreateCommand(string sql, IEnumerable<IDbDataParameter> parameters = null)
        {
            var dbCommand = DbConnection.CreateCommand();
            dbCommand.CommandText = sql;

            if (parameters == null)
                return dbCommand;

            foreach (var p in parameters)
            {
                dbCommand.Parameters.Add(p);
            }

            return dbCommand;
        }
        
        private void CloseConnection()
        {
            if (_transaction?.Connection != null && _transaction.Connection.State == ConnectionState.Open)
            {
                _transaction.Connection.Close();
            }
        }

        private static void SetNullParametersToDbNull(IDbCommand command)
        {
            if (command.Parameters == null)
                return;

            foreach (IDbDataParameter p in command.Parameters)
            {
                if (p.Value == null)
                    p.Value = DBNull.Value;
            }
        }
    }
}
