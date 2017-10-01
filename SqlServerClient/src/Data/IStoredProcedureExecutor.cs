using System.Collections.Generic;
using System.Threading.Tasks;

namespace Savage.Data
{
    public interface IStoredProcedureExecutor<T> where T : IStoredProcedure
    {
        Task<IEnumerable<IResultSetRow<T>>> Execute(IDbSession dbSession, T storedProcedure);
    }
}
