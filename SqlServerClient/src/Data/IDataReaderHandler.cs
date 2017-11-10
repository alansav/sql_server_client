using System.Collections.Generic;

namespace Savage.Data
{
    public interface IDataReaderHandler
    {
        IEnumerable<IResultSetRow> Handle(IOptimizedDataReader optimizedDataReader);
    }
}
