using System.Collections.Generic;
using System.Data;

namespace Savage.Data
{
    public interface ISqlCommand
    {
        string CommandText { get; }
        CommandType CommandType { get; }
        IEnumerable<IDbDataParameter> Parameters { get; }
    }
}
