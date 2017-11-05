using System.Collections.Generic;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface ISqlCommandExecutor<T> where T : ISqlCommand
    {
        Task<IEnumerable<IResultSetRow<T>>> Execute(IDbSession dbSession, T storedProcedure);
    }
}
