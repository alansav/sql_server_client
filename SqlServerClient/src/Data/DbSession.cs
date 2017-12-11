using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
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

        private async Task OpenConnectionIfClosed(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (DbConnection.State == ConnectionState.Closed)
                await DbClient.OpenConnectionAsync(DbConnection, cancellationToken);
        }

        private void AddCommandToTransaction(IDbCommand command)
        {
            if (_transaction == null)
                _transaction = DbConnection.BeginTransaction();

            command.Transaction = _transaction;
            command.Connection = _transaction.Connection;
        }

        private async Task PrepareCommand(IDbCommand dbCommand, CancellationToken cancellationToken = default(CancellationToken))
        {
            SetNullParametersToDbNull(dbCommand);
            await OpenConnectionIfClosed(cancellationToken).ConfigureAwait(false);
            AddCommandToTransaction(dbCommand);
        }

        public async Task<IResultSets> ExecuteReaderAsync(IDbCommand dbCommand, IDataReaderHandler handler, CancellationToken cancellationToken = default(CancellationToken))
        {
            await PrepareCommand(dbCommand);
            return await DbClient.CommandExecutor.ExecuteReaderAsync(dbCommand, handler, cancellationToken).ConfigureAwait(false);
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(IDbCommand dbCommand, CancellationToken cancellationToken = default(CancellationToken))
        {
            await PrepareCommand(dbCommand);
            return await DbClient.CommandExecutor.ExecuteNonQueryAsync(dbCommand, cancellationToken).ConfigureAwait(false);
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync(string sql, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbCommand = CreateCommand(sql, parameters);
            return await ExecuteNonQueryAsync(dbCommand, cancellationToken).ConfigureAwait(false);
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbCommand = CreateCommand(storedProcedureName, parameters);
            dbCommand.CommandType = CommandType.StoredProcedure;
            return await ExecuteNonQueryAsync(dbCommand, cancellationToken).ConfigureAwait(false);
        }

        public async Task<object> ExecuteScalarAsync(IDbCommand dbCommand, CancellationToken cancellationToken = default(CancellationToken))
        {
            await PrepareCommand(dbCommand);
            return await DbClient.CommandExecutor.ExecuteScalarAsync(dbCommand, cancellationToken).ConfigureAwait(false);
        }

        public async Task<object> ExecuteScalarAsync(string sql, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbCommand = CreateCommand(sql, parameters);
            return await ExecuteScalarAsync(dbCommand, cancellationToken).ConfigureAwait(false);
        }

        public async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, IEnumerable<IDbDataParameter> parameters = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbCommand = CreateCommand(storedProcedureName, parameters);
            dbCommand.CommandType = CommandType.StoredProcedure;
            return await ExecuteScalarAsync(dbCommand, cancellationToken).ConfigureAwait(false);
        }

        public async Task ExecuteBatchSqlAsync(IEnumerable<IDbCommand> dbCommands, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var dbCommand in dbCommands)
            {
                await ExecuteNonQueryAsync(dbCommand, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task ExecuteBatchSqlAsync(IEnumerable<string> sqlStatements, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var sql in sqlStatements)
            {
                await ExecuteNonQueryAsync(sql, null, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task ExecuteBatchSqlAsync(string sql, CancellationToken cancellationToken = default(CancellationToken))
        {
            var sqlStatements = DbClient.ToSqlStatements(sql);
            await ExecuteBatchSqlAsync(sqlStatements, cancellationToken);
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
