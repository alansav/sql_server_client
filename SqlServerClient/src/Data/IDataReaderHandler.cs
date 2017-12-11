using System.Collections.Generic;

namespace Savage.Data
{
    public interface IDataReaderHandler
    {
        IResultSets Handle(IOptimizedDataReader optimizedDataReader);
    }
}
