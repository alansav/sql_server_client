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

        public DbSession(IDbClient dbClient, IDbConnection connection)
        {
            DbClient = dbClient ?? throw new ArgumentNullException(nameof(dbClient));
            DbConnection = connection;
        }

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

        private void AddCommandToTransaction(IDbCommand command)
        {
            if (_transaction == null)
                _transaction = DbConnection.BeginTransaction();

            command.Transaction = _transaction;
            command.Connection = _transaction.Connection;
        }

        public async Task<IEnumerable<IResultSetRow<T>>> ExecuteReaderAsync<T>(T sqlCommand, IDataReaderHandler<T> handler) where T : ISqlCommand
        {
            IDbCommand command = await BuildCommand(sqlCommand);

            return await DbClient.CommandExecutor.ExecuteReaderAsync(command, handler);
        }

        public async Task<RowsAffectedResultSet> ExecuteNonQueryAsync<T>(T sqlCommand) where T : ISqlCommand
        {
            IDbCommand command = await BuildCommand(sqlCommand);

            return await DbClient.CommandExecutor.ExecuteNonQueryAsync(command);
        }

        public async Task<object> ExecuteScalarAsync<T>(T sqlCommand) where T : ISqlCommand
        {
            IDbCommand command = await BuildCommand(sqlCommand);

            return await DbClient.CommandExecutor.ExecuteScalarAsync(command);
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

        private async Task<IDbCommand> BuildCommand(ISqlCommand sqlCommand)
        {
            if (DbConnection.State == ConnectionState.Closed)
                await DbClient.OpenConnectionAsync(DbConnection);

            var command = DbClient.CommandBuilder.BuildCommand(sqlCommand);
            AddCommandToTransaction(command);
            return command;
        }

        private void CloseConnection()
        {
            if (_transaction?.Connection != null && _transaction.Connection.State == ConnectionState.Open)
            {
                _transaction.Connection.Close();
            }
        }
    }
}
