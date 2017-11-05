using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Savage.Data.SqlServerClient
{
    public class SqlCommandBuilder : ICommandBuilder
    {
        public IDbCommand BuildCommand(ISqlCommand sqlCommand)
        {
            if (sqlCommand == null)
                throw new ArgumentNullException(nameof(sqlCommand));
            
            var command = new SqlCommand
            {
                CommandText = sqlCommand.CommandText,
                CommandType = sqlCommand.CommandType
            };

            if (sqlCommand.Parameters != null)
            {
                command.Parameters.AddRange(sqlCommand.Parameters.ToArray());
                SetNullParametersToDbNull(command);
            }

            return command;
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
