using System.Collections.Generic;
using System.Data;

namespace Savage.Data
{
    public interface IStoredProcedure
    {
        string StoredProcedureName { get; }
        IEnumerable<IDbDataParameter> Parameters { get; }
    }
}
