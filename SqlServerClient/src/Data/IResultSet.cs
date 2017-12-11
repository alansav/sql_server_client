using System.Collections.Generic;

namespace Savage.Data
{
    public interface IResultSet<T> where T : IResultSetRow
    {
        IEnumerable<T> Rows { get; }
    }
}
