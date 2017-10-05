using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Savage.Data.MySqlClient
{
    public class MySqlCommandBuilder : ICommandBuilder
    {
        public IDbCommand BuildCommand(IStoredProcedure storedProcedure)
        {
            if (storedProcedure == null)
                throw new ArgumentNullException(nameof(storedProcedure));

            var command = new MySqlCommand
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
