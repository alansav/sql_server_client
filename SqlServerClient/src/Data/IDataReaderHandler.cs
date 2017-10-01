using System.Collections.Generic;

namespace Savage.Data
{
    public interface IDataReaderHandler<T> where T : IStoredProcedure
    {
        IEnumerable<IResultSetRow<T>> Handle(IOptimizedDataReader optimizedDataReader);
    }
}
