using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbClient
    {
        ICommandExecutor CommandExecutor { get; }

        Task OpenConnectionAsync(IDbConnection connection, CancellationToken cancellationToken = default (CancellationToken));        
        IDbSession CreateDbSession();
        IEnumerable<string> ToSqlStatements(string sql);
    }
}
