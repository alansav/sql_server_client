using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public static class Client
    {
        public static SqlCommand BuildStoredProcedureCommand(string procedureName)
        {
            if (procedureName == null)
                throw new ArgumentNullException(nameof(procedureName));
            if (String.IsNullOrWhiteSpace(procedureName))
                throw new ArgumentException(
                    $"Argument cannot be an null, an empty string or consist of white space: {nameof(procedureName)}");

            return new SqlCommand(procedureName) {CommandType = CommandType.StoredProcedure};
        }

        public static DbDataReader ExecuteReader(string connectionString, DbCommand command, CommandBehavior behavior = CommandBehavior.Default)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return ExecuteReader(connection, command, behavior);
        }

        public static DbDataReader ExecuteReader(DbConnection connection, DbCommand command, CommandBehavior behavior = CommandBehavior.Default)
        {
            ConvertNullValuesToDbNull(command);
            command.Connection = connection;
            
            if (connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            return command.ExecuteReader(behavior);
        }

        public static int ExecuteNonQuery(string connectionString, DbCommand command)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return ExecuteNonQuery(connection, command);
        }

        public static int ExecuteNonQuery(DbConnection connection, DbCommand command)
        {
            ConvertNullValuesToDbNull(command);
            command.Connection = connection;

            if (connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            return command.ExecuteNonQuery();
        }

        public static object ExecuteScalar(string connectionString, SqlCommand command)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return ExecuteScalar(connection, command);
        }

        public static object ExecuteScalar(SqlConnection connection, SqlCommand command)
        {
            ConvertNullValuesToDbNull(command);
            command.Connection = connection;

            if (connection.State != ConnectionState.Open)
            {
                command.Connection.Open();
            }

            return command.ExecuteScalar();
        }

        private static void ConvertNullValuesToDbNull(DbCommand command)
        {
            if (command.Parameters == null)
                return;

            foreach (DbParameter p in command.Parameters)
            {
                if (p.Value == null)
                    p.Value = DBNull.Value;
            }
        }
    }
}
