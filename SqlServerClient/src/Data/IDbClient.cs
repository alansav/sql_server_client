using System.Data;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IDbClient
    {
        ICommandExecutor CommandExecutor { get; }

        Task OpenConnectionAsync(IDbConnection connection);        
        IDbSession CreateDbSession(string connectionString);
    }
}
