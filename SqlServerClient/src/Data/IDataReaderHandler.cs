using System.Collections.Generic;

namespace Savage.Data
{
    public interface IDataReaderHandler
    {
        IEnumerable<IResultSet> Handle(IOptimizedDataReader optimizedDataReader);
    }
}
