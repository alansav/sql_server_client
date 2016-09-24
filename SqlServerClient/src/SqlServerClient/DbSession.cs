using System;
using System.Data;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public class DbSession : IDbSession
    {
        private readonly SqlTransaction _transaction;

        public DbSession(SqlConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            OpenConnectionIfNotAlreadyOpen(connection);
            _transaction = connection.BeginTransaction();
        }

        public DbSession(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            var connection = new SqlConnection(connectionString);
            OpenConnectionIfNotAlreadyOpen(connection);
            _transaction = connection.BeginTransaction();
        }

        public void AddCommandToSession(SqlCommand command)
        {
            command.Transaction = _transaction;
            command.Connection = _transaction.Connection;
        }

        public void Commit()
        {
            _transaction.Commit();
            CloseConnection();
        }

        public void Dispose()
        {
            CloseConnection();
        }

        public void Rollback()
        {
            _transaction.Rollback();
            CloseConnection();
        }

        private void CloseConnection()
        {
            if (_transaction?.Connection != null && _transaction.Connection.State == ConnectionState.Open)
            {
                _transaction.Connection.Close();
            }
        }

        private void OpenConnectionIfNotAlreadyOpen(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }
    }
}
