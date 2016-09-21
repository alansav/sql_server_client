using System;
using System.Data;
using System.Data.SqlClient;

namespace Savage.SqlServerClient
{
    public class CommandBuilder
    {
        public static SqlCommand BuildSqlCommand(IStoredProcedure storedprocedure)
        {
            if (storedprocedure == null)
                throw new ArgumentNullException(nameof(storedprocedure));

            return BuildSqlCommand(storedprocedure.StoredProcedureName, storedprocedure.Parameters);
        }

        public static SqlCommand BuildSqlCommand(string storedProcedureName, SqlParameter[] parameters = null)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentNullException(nameof(storedProcedureName));

            var command = new SqlCommand
            {
                CommandText = storedProcedureName,
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }
    }
}
