using System.Collections.Generic;
using System.Data;

namespace Savage.Data
{
    public interface IDataReaderHandler<T> where T : IDbCommand
    {
        IEnumerable<IResultSetRow<T>> Handle(IOptimizedDataReader optimizedDataReader);
    }
}
