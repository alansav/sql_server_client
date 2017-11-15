using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Savage.Data.SqlServerClient
{
    public class DbClient : IDbClient
    {
        private readonly string ConnectionString;
        public ICommandExecutor CommandExecutor { get; private set; }

        public DbClient(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

            CommandExecutor = new SqlCommandExecutor();
        }
        
        public async Task OpenConnectionAsync(IDbConnection connection, CancellationToken cancellationToken = default(CancellationToken))
        {
            await ((SqlConnection)connection).OpenAsync(cancellationToken).ConfigureAwait(false);
        }

        public IDbSession CreateDbSession()
        {
            var connection = new SqlConnection(ConnectionString);

            return new DbSession(this, connection);
        }
    }
}
