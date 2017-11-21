using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Savage.Data.MySqlClient
{
    public class DbClient : IDbClient
    {
        private readonly string ConnectionString;
        public ICommandExecutor CommandExecutor { get; private set; }

        public DbClient(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));

            CommandExecutor = new MySqlCommandExecutor();
        }

        public async Task OpenConnectionAsync(IDbConnection connection, CancellationToken cancellationToken = default(CancellationToken))
        {
            await ((MySqlConnection)connection).OpenAsync(cancellationToken);
        }

        public IDbSession CreateDbSession()
        {
            var connection = new MySqlConnection(ConnectionString);

            return new DbSession(this, connection);
        }

        public IEnumerable<string> ToSqlStatements(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
