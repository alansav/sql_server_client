using System;
using System.Data;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public class CommandBuilder<T> where T : ISqlQuery
    {
        public SqlCommand BuildSqlCommand(SqlTransaction transaction, T query, IParameters<T> parameters)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var command = query.CreateSqlCommand();
            command.Connection = transaction.Connection;
            command.Transaction = transaction;
            
            AddParameters(command, parameters);

            return command;
        }

        private static void AddParameters(SqlCommand command, IParameters<T> parameters)
        {
            if (parameters == null)
                return;

            command.Parameters.AddRange(parameters.ToSqlParameters());

            foreach (SqlParameter p in command.Parameters)
            {
                if (p.Value == null)
                    p.Value = DBNull.Value;
            }
        }
    }
}
