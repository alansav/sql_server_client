using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Savage.Data.MySqlClient
{
    public class DbClient : IDbClient
    {
        public ICommandExecutor CommandExecutor { get; private set; }

        public DbClient()
        {
            CommandExecutor = new MySqlCommandExecutor();
        }

        public async Task OpenConnectionAsync(IDbConnection connection, CancellationToken cancellationToken = default(CancellationToken))
        {
            await ((MySqlConnection)connection).OpenAsync(cancellationToken);
        }

        public IDbSession CreateDbSession(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            var connection = new MySqlConnection(connectionString);

            return new DbSession(this, connection);
        }
    }
}
