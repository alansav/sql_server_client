using System.Collections.Generic;

namespace Savage.Data
{
    public interface IResultSet
    {
        IEnumerable<IResultSetRow> Rows { get; }
    }
}
