using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Savage.Data.MySqlClient
{
    public class DbClient : IDbClient
    {
        public ICommandBuilder CommandBuilder { get; private set; }
        public ICommandExecutor CommandExecutor { get; private set; }

        public DbClient()
        {
            CommandBuilder = new MySqlCommandBuilder();
            CommandExecutor = new MySqlCommandExecutor();
        }

        public async Task OpenConnectionAsync(IDbConnection connection)
        {
            await ((MySqlConnection)connection).OpenAsync();
        }

        public IDbSession CreateDbSession(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            var connection = new MySqlConnection(connectionString);

            return new DbSession(new DbClient(), connection);
        }
    }
}
