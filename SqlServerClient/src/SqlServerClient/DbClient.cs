using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
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
        
        public async Task OpenConnectionAsync(IDbConnection connection, CancellationToken cancellationToken = default)
        {
            await ((SqlConnection)connection).OpenAsync(cancellationToken).ConfigureAwait(false);
        }

        public IDbSession CreateDbSession()
        {
            var connection = new SqlConnection(ConnectionString);

            return new DbSession(this, connection);
        }

        public IEnumerable<string> ToSqlStatements(string sql)
        {
            var statements = Regex.Split(sql, @"^\s*GO\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            return statements.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();
        }
    }
}
