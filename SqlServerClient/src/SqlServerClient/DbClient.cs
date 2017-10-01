using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Savage.Data.SqlServerClient
{
    public class DbClient : IDbClient
    {
        public ICommandBuilder CommandBuilder { get; private set; }
        public ICommandExecutor CommandExecutor { get; private set; }

        public DbClient()
        {
            CommandBuilder = new SqlCommandBuilder();
            CommandExecutor = new SqlCommandExecutor();
        }
        
        public async Task OpenConnectionAsync(IDbConnection connection)
        {
            await ((SqlConnection)connection).OpenAsync();
        }

        public IDbSession CreateDbSession(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));

            var connection = new SqlConnection(connectionString);

            return new DbSession(new DbClient(), connection);
        }
    }
}
