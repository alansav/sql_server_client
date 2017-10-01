using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Savage.Data.SqlServerClient
{
    public class SqlCommandBuilder : ICommandBuilder
    {
        public IDbCommand BuildCommand(IStoredProcedure storedProcedure)
        {
            if (storedProcedure == null)
                throw new ArgumentNullException(nameof(storedProcedure));
            
            var command = new SqlCommand
            {
                CommandText = storedProcedure.StoredProcedureName,
                CommandType = CommandType.StoredProcedure
            };

            if (storedProcedure.Parameters != null)
            {
                command.Parameters.AddRange(storedProcedure.Parameters.ToArray());
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
